// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObject
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public partial class BuiltObject : StellarObject, IComparable, IComparable<StellarObject>, IComparable<BuiltObject>, IComparable<Habitat>, IComparable<Creature>, ISerializable
    {
        public object _LockObject = new object();

        public int BuiltObjectID;

        public bool InView;

        private bool _IsPlanetDestroyer;

        [NonSerialized]
        public SpaceBattleStats BattleStats;

        private float _DeployProgress;

        private bool _IsDeployed;

        public bool FirstExecutionOfCommand = true;

        private bool _IsIndependentOrPirate;

        private bool _LastWithdrawalEvaluation;

        private short _TopSpeedBase;

        public short TopSpeedFuelBurn;

        private short _CruiseSpeedBase;

        public short CruiseSpeed;

        public short CruiseSpeedFuelBurn;

        public int WarpSpeed;

        public int WarpSpeedFuelBurn;

        public short HyperjumpInitiate;

        public short ImpulseSpeedFuelBurn;

        public EngineType EngineType;

        public float AccelerationRate;

        public double CurrentFuel;

        public bool _FuelHandicapped;

        private bool _MovementSlowedLocation;

        private bool _HyperjumpDisabledLocation;

        private bool _ShieldsReducedLocation;

        private float _ShipPullAmountLocation;

        private float _ShipPullAngleLocation;

        private float _ShipDamageAmountLocation;

        public int FuelCapacity;

        public float AttackRangeSquared;

        public double CurrentEnergy;

        public int StaticEnergyConsumption;

        public int ReactorPowerOutput;

        public int ReactorStorageCapacity;

        public int ReactorCycleFuelConsumption;

        public double CurrentReactorStorage;

        public bool TargetSpeedChanged;

        public float PreferredSpeed;

        private int _CargoCapacity;

        public int TroopCapacity;

        public BuiltObjectRole Role;

        public BuiltObjectSubRole SubRole;

        private float _Heading;

        public bool HeadingChanged = true;

        public float TurnRate;

        private TurnDirection _TurnDirection = TurnDirection.StraightAhead;

        public bool OverlayChanged = true;

        public bool LightChanged = true;

        public bool LightsOn;

        public BuiltObjectStance Stance;

        public double PurchasePrice;

        public double CurrentYearsIncome;

        public long DateOfLastIncome;

        public int ConsecutiveUnprofitableYears;

        public bool Scrap;

        private int _AnnualSupportCost;

        private float _SupportCostFactor = 1f;

        public BattleTactics TacticsStrongerShips;

        public BattleTactics TacticsWeakerShips;

        public InvasionTactics TacticsInvasion;

        public BuiltObjectFleeWhen FleeWhen;

        public long DateBuilt;

        public long DateRetrofit;

        public float CurrentShields;

        public int ShieldsCapacity;

        public float ShieldRechargeRate;

        public short ShieldAreaRechargeRange;

        public short ShieldAreaRechargeCapacity;

        public short ShieldAreaRechargeEnergyRequired;

        public BuiltObject ShieldAreaRechargeTarget;

        public DateTime ShieldAreaRechargeStartTime = DateTime.MinValue;

        public short Armor;

        public short ArmorReactive;

        public short ArmorReinforcingFactor;

        public short TargettingModifier;

        public short CountermeasureModifier;

        public short FleetTargettingModifier;

        public short FleetCountermeasureModifier;

        public short FleetTargettingBonus;

        public short FleetCountermeasureBonus;

        public float MaintenanceSavings;

        public float TradeBonuses;

        public int PictureRef;

        public bool IsSpacePort;

        public bool IsColony;

        public bool IsResearchLab;

        public int ResearchWeapons;

        public int ResearchEnergy;

        public int ResearchHighTech;

        public bool IsResourceExtractor;

        public short ExtractionMine;

        public short ExtractionGas;

        public short ExtractionLuxury;

        public short EnergyToFuelRate;

        public bool IsManufacturer;

        public bool IsEnergyCollector;

        private int EnergyCollection;

        public int SensorProximityArrayRange;

        public byte SensorJumpIntercept;

        public int SensorResourceProfileSensorRange;

        public int SensorLongRange;

        public short SensorTraceScannerRange;

        public short SensorTraceScannerPower;

        public short SensorTraceScannerJamming;

        public int MaxPopulation;

        public int PopulationCapacity;

        public int MedicalCapacity;

        public int RecreationCapacity;

        public double ParentOffsetX = -2000000001.0;

        public double ParentOffsetY = -2000000001.0;

        public bool SuppressAutoRetrofit;

        public bool HyperDenyActive;

        public int WeaponHyperDenyRange;

        public short HyperStopRange;

        public bool CanHyperJump = true;

        private double OptimalMinimumAttackRange;

        private double OptimalMaximumAttackRange;

        public int MaximumWeaponsRange;

        public int MinimumWeaponsRange;

        public int PointDefenseWeaponsRange;

        public int PlanetDestroyerWeaponsRange;

        public int BombardWeaponPower;

        public int BombardRange;

        public int IonWeaponPower;

        public int IonWeaponRange;

        public int IonDefense;

        public short TractorBeamRange;

        public short AssaultStrength;

        public short AssaultRange;

        public short AssaultShieldPenetration;

        public short AssaultAttackValue;

        public short AssaultDefenseValue;

        public byte AssaultAttackEmpireId;

        public bool AssaultIsRaid;

        [NonSerialized]
        public short AssaultDefenseValueDefault;

        [NonSerialized]
        public short AssaultDefenseValueFixed;

        [NonSerialized]
        public short AssaultOwnershipChangeCounter;

        public byte PirateEmpireId;

        private float _LastHyperjumpDistance;

        private float _DamageReduction;

        private short _DamageRepair;

        public long LastRepair;

        public BuiltObjectEncounterAction PlayerEmpireEncounterAction;

        public BuiltObjectEncounterEventType EncounterEventType;

        public short EncounterExplorationBonus;

        public int EncounterMoneyBonus;

        public byte EncounterGovernmentTypeId = byte.MaxValue;

        public int StandoffWeaponsMaxRange;

        public int BeamWeaponsMinRange;

        public int CurrentEscortForceAssigned;

        private bool _ExecutingShipGroupCommand;

        public bool RefuelForNextMission;

        public bool RetireForNextMission;

        public bool RetrofitForNextMission;

        public bool RepairForNextMission;

        public bool StrandedMessageSent;

        private bool _MissionCompleteMessageSent;

        private bool _IsBlockaded;

        public bool IsAutoControlled = true;

        public bool InBattle;

        public float LastShieldStrikeDirection = float.MinValue;

        public float LastTractorStrikeDirection = float.MinValue;

        private int _TargetSpeed;

        private bool _DoingConstruction;

        private bool _DoingMining;

        private bool _DoingGasMining;

        public long NextSoundTimeConstruction;

        public long NextSoundTimeMining;

        public long NextSoundTimeGasMining;

        public int ScanHabitatIndex = -1;

        public long LastScanTime;

        public byte TroopLoadoutInfantry;

        public byte TroopLoadoutArmored;

        public byte TroopLoadoutArtillery;

        public byte TroopLoadoutSpecialForces;

        public bool _HyperjumpAboutToEnter;

        private bool _HyperjumpAboutToEnterSoundPlayed;

        public bool _HyperjumpJustExited;

        private long _HyperjumpCountdown;

        public bool _HyperjumpPrepare;

        private double _HyperjumpX = -1.0;

        private double _HyperjumpY = -1.0;

        public bool _FirstHyperjumpExecution;

        private double _LastHyperDistance = 100000000.0;

        private float _Angle;

        private double _LastDockDistance = 100000000.0;

        private double _LastDistance = 536870911.0;

        private double _LastInvasionDistance = 536870911.0;

        public double _LastPositionX;

        public double _LastPositionY;

        public bool HyperExitStartAnimation;

        public bool HyperEnterStartAnimation;

        public int UnbuiltComponentCount;

        public int DamagedComponentCount;

        public int UndamagedComponentSize;

        private bool _DockedAtHabitat;

        private bool _BuiltAtHabitat;

        public Galaxy _Galaxy;

        private StellarObject _DockedAt;

        private StellarObject _BuiltAt;

        public ConstructionQueue RetrofitBaseConstructionQueue;

        public ManufacturingQueue RetrofitBaseManufacturingQueue;

        public Resource FuelType;

        private byte _RefuelResourceId = byte.MaxValue;

        private short _RefuelAmount;

        private bool _RefuelLocationIsBuiltObject;

        private int _RefuelLocationId = -1;

        private byte _CaptainTargetingBonus = 100;

        private byte _CaptainCountermeasuresBonus = 100;

        private byte _CaptainShipManeuveringBonus = 100;

        private byte _CaptainFightersBonus = 100;

        private byte _CaptainShipEnergyUsageBonus = 100;

        private byte _CaptainWeaponsDamageBonus = 100;

        private byte _CaptainWeaponsRangeBonus = 100;

        private byte _CaptainShieldRechargeRateBonus = 100;

        private byte _CaptainDamageControlBonus = 100;

        private byte _CaptainRepairBonus = 100;

        private byte _CaptainHyperjumpSpeedBonus = 100;

        public BuiltObjectMissionList _SubsequentMissions = new BuiltObjectMissionList();

        private ManufacturingQueue _ManufacturingQueue;

        private object _redefineLock = new object();
        private object _disabledListLock = new object();

        [OptionalField]
        public FighterList Fighters;

        [OptionalField]
        public int FighterCapacity;

        [OptionalField]
        public int FighterRepairRate;

        [NonSerialized]
        public StellarObject[] _Threats;

        [NonSerialized]
        public int[] _ThreatLevels;

        [NonSerialized]
        public int _TotalThreatLevel;

        public Design Design;

        public ShipGroup ShipGroup;

        private DateTime _LastTouch;

        private DateTime _LastIntermediateTouch;

        private DateTime _LastPeriodicTouch;

        private DateTime _LastLongTouch;

        public DateTime _tempNow;

        private DateTime _LastLocationEffectTouch;

        private Habitat _ColonyToAttack;

        public Race NativeRace;

        public string EncounterDescription;

        public int EncounterTechAdvanceCount;

        public ExplosionList Explosions = new ExplosionList();

        private ContractList _ContractsToFulfill;

        public DateTime LastShieldStrike = DateTime.MinValue;

        public DateTime LastIonStrike = DateTime.MinValue;

        public bool IonStrikeSoundPlayed;

        public DateTime LastTractorStrike = DateTime.MinValue;

        public Design RetrofitDesign;

        public Habitat NearestSystemStar;

        [OptionalField]
        public BuiltObjectMission RevertMission;

        public BuiltObjectMission Mission;

        public BuiltObjectComponentList Components;

        public List<short> DisabledComponentIndexes;

        public List<short> DisabledComponentDurations;

        public WeaponList Weapons;

        [OptionalField]
        public string CaptainName = string.Empty;

        [NonSerialized]
        public List<GalaxyLocationEffectType> LocationEffects = new List<GalaxyLocationEffectType>();

        [NonSerialized]
        private int _WeaponsAvailablePointDefenseCount = int.MaxValue;

        [NonSerialized]
        private int _WeaponsAvailableBeamCount = int.MaxValue;

        [NonSerialized]
        private short _TractorBeamFiringCounter;

        [NonSerialized]
        private short _FighterFiringCounter;

        [NonSerialized]
        private StellarObject _RefuellingLocation;

        [NonSerialized]
        private StellarObjectList _SecondaryTargets = new StellarObjectList();

        [NonSerialized]
        private List<double> _SecondaryThreatLevels = new List<double>();

        [NonSerialized]
        public short _AssaultPodFiringCounter;

        public SpaceBattleStats CareerBattleStats;

        public Dictionary<string, object> BaconValues;

        public int WarpSpeedWithBonuses
        {
            get
            {
                double num = WarpSpeed;
                if (ShipGroup != null)
                {
                    num *= ShipGroup.HyperjumpSpeedBonus;
                }
                num *= CaptainHyperjumpSpeedBonus;
                return (int)num;
            }
        }

        public float LastHyperjumpDistance => _LastHyperjumpDistance;

        public double CaptainTargetingBonus => (double)(int)_CaptainTargetingBonus / 100.0;

        public double CaptainCountermeasuresBonus => (double)(int)_CaptainCountermeasuresBonus / 100.0;

        public double CaptainShipManeuveringBonus => (double)(int)_CaptainShipManeuveringBonus / 100.0;

        public double CaptainFightersBonus => (double)(int)_CaptainFightersBonus / 100.0;

        public double CaptainShipEnergyUsageBonus => (double)(int)_CaptainShipEnergyUsageBonus / 100.0;

        public double CaptainWeaponsDamageBonus => (double)(int)_CaptainWeaponsDamageBonus / 100.0;

        public double CaptainWeaponsRangeBonus => (double)(int)_CaptainWeaponsRangeBonus / 100.0;

        public double CaptainShieldRechargeRateBonus => (double)(int)_CaptainShieldRechargeRateBonus / 100.0;

        public double CaptainDamageControlBonus => (double)(int)_CaptainDamageControlBonus / 100.0;

        public double CaptainRepairBonus => (double)(int)_CaptainRepairBonus / 100.0;

        public double CaptainHyperjumpSpeedBonus => (double)(int)_CaptainHyperjumpSpeedBonus / 100.0;

        public int CargoCapacity => _CargoCapacity;

        public float SupportCostFactor
        {
            get
            {
                return _SupportCostFactor;
            }
            set
            {
                _SupportCostFactor = value;
            }
        }

        public TurnDirection TurnDirection => _TurnDirection;

        public ManufacturingQueue ManufacturingQueue => _ManufacturingQueue;

        public double HyperjumpX => _HyperjumpX;

        public double HyperjumpY => _HyperjumpY;

        public long HyperjumpCountdown => _HyperjumpCountdown;

        public int UnbuiltOrDamagedComponentCount => UnbuiltComponentCount + DamagedComponentCount;

        public bool IsBlockaded
        {
            get
            {
                return _IsBlockaded;
            }
            set
            {
                _IsBlockaded = value;
            }
        }

        public ContractList ContractsToFulfill
        {
            get
            {
                return _ContractsToFulfill;
            }
            set
            {
                _ContractsToFulfill = value;
            }
        }

        public bool DoingConstruction
        {
            get
            {
                return _DoingConstruction;
            }
            set
            {
                _DoingConstruction = value;
            }
        }

        public bool DoingMining => _DoingMining;

        public bool DoingGasMining => _DoingGasMining;

        public DateTime LastTouch => _LastTouch;

        public bool HyperjumpAboutToEnter => _HyperjumpAboutToEnter;

        public bool HyperjumpAboutToEnterSoundPlayed
        {
            get
            {
                return _HyperjumpAboutToEnterSoundPlayed;
            }
            set
            {
                _HyperjumpAboutToEnterSoundPlayed = value;
            }
        }

        public bool HyperjumpJustExited => _HyperjumpJustExited;

        public bool HyperjumpPrepare => _HyperjumpPrepare;

        public float Heading
        {
            get
            {
                return _Heading;
            }
            set
            {
                if (_Heading != value)
                {
                    HeadingChanged = true;
                }
                _Heading = value;
            }
        }

        public bool IsPlanetDestroyer => _IsPlanetDestroyer;

        public double DeployProgress => _DeployProgress;

        public bool IsDeployed => _IsDeployed;

        public double DamageReduction => _DamageReduction;

        public int DamageRepair => _DamageRepair;

        public int TargetSpeed
        {
            get
            {
                return _TargetSpeed;
            }
            set
            {
                if (_TargetSpeed != value)
                {
                    TargetSpeedChanged = true;
                }
                _TargetSpeed = value;
            }
        }

        public bool MovementSlowedLocation => _MovementSlowedLocation;

        public bool HyperjumpDisabledLocation => _HyperjumpDisabledLocation;

        public bool ShieldsReducedLocation => _ShieldsReducedLocation;

        public float ShipPullAmountLocation => _ShipPullAmountLocation;

        public float ShipPullAngleLocation => _ShipPullAngleLocation;

        public float ShipDamageAmountLocation => _ShipDamageAmountLocation;

        public BuiltObjectMissionList SubsequentMissions => _SubsequentMissions;

        public int TotalThreatLevel => _TotalThreatLevel;

        public int PopulationCapacityRemaining
        {
            get
            {
                int num = 0;
                if (Population != null)
                {
                    num = (int)Population.TotalAmount;
                }
                int val = PopulationCapacity - num;
                return Math.Max(0, val);
            }
        }

        public override int TroopCapacityRemaining
        {
            get
            {
                int num = 0;
                if (Troops != null)
                {
                    num = Troops.TotalSize;
                }
                int val = TroopCapacity - num;
                return Math.Max(0, val);
            }
        }

        public int AnnualSupportCost
        {
            get
            {
                double num = Galaxy.ShipMaintenanceCostPerSizeUnit * (double)Size;
                double num2 = (double)_AnnualSupportCost + num;
                if (Role != BuiltObjectRole.Base && Design != null && Design.WarpSpeed <= 0)
                {
                    num2 *= 0.8;
                }
                Empire actualEmpire = ActualEmpire;
                double num3 = 0.0;
                if (Role == BuiltObjectRole.Base && ParentHabitat != null && actualEmpire != null && actualEmpire != _Galaxy.IndependentEmpire && ParentHabitat.Empire == actualEmpire && ParentHabitat.Population != null && ParentHabitat.Population.Count > 0 && ParentHabitat.ResourceBonuses != null)
                {
                    num3 = ParentHabitat.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.BaseMaintenanceReduction) / 100.0;
                }
                double num4 = 0.0;
                if (actualEmpire != null && actualEmpire.DominantRace != null && actualEmpire.DominantRace.ChangePeriodActive && actualEmpire.DominantRace.PeriodicRaceEvent == RaceEventType.StrengthInNumbersMaintenanceLowerForSmallShips && Size <= 200)
                {
                    num4 = 0.25;
                }
                int characterMaintenanceBonuses = GetCharacterMaintenanceBonuses();
                double num5 = (double)characterMaintenanceBonuses / 100.0;
                double num6 = Math.Min(1.0, (double)MaintenanceSavings + num3 + num4 + num5);
                double num7 = num6 * num2;
                double num8 = 1.0;
                if (Empire != null && Empire.GovernmentAttributes != null)
                {
                    num8 = Empire.GovernmentAttributes.MaintenanceCosts;
                }
                if (actualEmpire != null && actualEmpire.PirateEmpireBaseHabitat != null)
                {
                    double d = (double)_Galaxy.BaseTechCost / 120000.0;
                    d = Math.Sqrt(d);
                    num8 *= _Galaxy.PirateShipMaintenanceFactor * d;
                }
                if (actualEmpire != null)
                {
                    num8 = ((!_Galaxy.DetermineBuiltObjectIsState(SubRole)) ? (num8 * actualEmpire.ShipMaintenancePrivateFactor) : (num8 * actualEmpire.ShipMaintenanceStateFactor));
                }
                double num9 = (num2 - num7) * num8;
                num9 *= (double)_SupportCostFactor;
                return (int)num9;
            }
            set
            {
                _AnnualSupportCost = value;
            }
        }

        public override int CargoSpace
        {
            get
            {
                if (Role == BuiltObjectRole.Base && ParentHabitat != null && ParentHabitat.Population.TotalAmount > 0)
                {
                    return 536870911;
                }
                if (Empire != null && (SubRole == BuiltObjectSubRole.SmallSpacePort || SubRole == BuiltObjectSubRole.MediumSpacePort || SubRole == BuiltObjectSubRole.LargeSpacePort))
                {
                    return 536870911;
                }
                int num = 0;
                if (Cargo != null)
                {
                    for (int i = 0; i < Cargo.Count; i++)
                    {
                        num += Cargo[i].Amount;
                    }
                }
                num = Math.Max(0, num);
                int num2 = CargoCapacity - num;
                if (num2 < 0)
                {
                    num2 = 0;
                }
                return num2;
            }
        }

        public Empire ActualEmpire
        {
            get
            {
                Empire empire = Empire;
                if (PirateEmpireId > 0 && empire != null && empire.EmpireId != PirateEmpireId)
                {
                    empire = _Galaxy.PirateEmpires.GetByEmpireId(PirateEmpireId);
                }
                return empire;
            }
        }

        public StellarObject CachedRefuellingLocation => _RefuellingLocation;

        public StellarObject[] Threats => _Threats;

        public int[] ThreatLevels => _ThreatLevels;

        public StellarObject DockedAt
        {
            get
            {
                return _DockedAt;
            }
            set
            {
                if (value == null)
                {
                    _DockedAt = null;
                }
                else if (value is BuiltObject)
                {
                    _DockedAtHabitat = false;
                    _DockedAt = (BuiltObject)value;
                }
                else if (value is Habitat)
                {
                    _DockedAtHabitat = true;
                    _DockedAt = (Habitat)value;
                }
            }
        }

        public StellarObject BuiltAt
        {
            get
            {
                return _BuiltAt;
            }
            set
            {
                if (value == null)
                {
                    _BuiltAt = null;
                }
                else if (value is BuiltObject)
                {
                    _BuiltAtHabitat = false;
                    _BuiltAt = (BuiltObject)value;
                }
                else if (value is Habitat)
                {
                    _BuiltAtHabitat = true;
                    _BuiltAt = (Habitat)value;
                }
            }
        }

        public BuiltObject()
        {
        }

        public BuiltObject(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            byte[] buffer = (byte[])info.GetValue("BO_D", typeof(byte[]));
            using (MemoryStream input = new MemoryStream(buffer))
            {
                using BinaryReader binaryReader = new BinaryReader(input);
                BuiltObjectID = binaryReader.ReadInt32();
                InView = binaryReader.ReadBoolean();
                _IsPlanetDestroyer = binaryReader.ReadBoolean();
                _DeployProgress = binaryReader.ReadSingle();
                _IsDeployed = binaryReader.ReadBoolean();
                FirstExecutionOfCommand = binaryReader.ReadBoolean();
                _IsIndependentOrPirate = binaryReader.ReadBoolean();
                _LastWithdrawalEvaluation = binaryReader.ReadBoolean();
                _TopSpeedBase = binaryReader.ReadInt16();
                TopSpeedFuelBurn = binaryReader.ReadInt16();
                _CruiseSpeedBase = binaryReader.ReadInt16();
                CruiseSpeed = binaryReader.ReadInt16();
                CruiseSpeedFuelBurn = binaryReader.ReadInt16();
                WarpSpeed = binaryReader.ReadInt32();
                WarpSpeedFuelBurn = binaryReader.ReadInt32();
                HyperjumpInitiate = binaryReader.ReadInt16();
                ImpulseSpeedFuelBurn = binaryReader.ReadInt16();
                EngineType = (EngineType)binaryReader.ReadByte();
                AccelerationRate = binaryReader.ReadSingle();
                CurrentFuel = binaryReader.ReadDouble();
                _FuelHandicapped = binaryReader.ReadBoolean();
                _MovementSlowedLocation = binaryReader.ReadBoolean();
                _HyperjumpDisabledLocation = binaryReader.ReadBoolean();
                _ShieldsReducedLocation = binaryReader.ReadBoolean();
                _ShipPullAmountLocation = binaryReader.ReadSingle();
                _ShipPullAngleLocation = binaryReader.ReadSingle();
                _ShipDamageAmountLocation = binaryReader.ReadSingle();
                FuelCapacity = binaryReader.ReadInt32();
                _RefuelResourceId = binaryReader.ReadByte();
                _RefuelAmount = binaryReader.ReadInt16();
                _RefuelLocationIsBuiltObject = binaryReader.ReadBoolean();
                _RefuelLocationId = binaryReader.ReadInt32();
                AttackRangeSquared = binaryReader.ReadSingle();
                CurrentEnergy = binaryReader.ReadDouble();
                StaticEnergyConsumption = binaryReader.ReadInt32();
                ReactorPowerOutput = binaryReader.ReadInt32();
                ReactorStorageCapacity = binaryReader.ReadInt32();
                ReactorCycleFuelConsumption = binaryReader.ReadInt32();
                CurrentReactorStorage = binaryReader.ReadDouble();
                TargetSpeedChanged = binaryReader.ReadBoolean();
                PreferredSpeed = binaryReader.ReadSingle();
                _CargoCapacity = binaryReader.ReadInt32();
                TroopCapacity = binaryReader.ReadInt32();
                Role = (BuiltObjectRole)binaryReader.ReadByte();
                SubRole = (BuiltObjectSubRole)binaryReader.ReadByte();
                _Heading = binaryReader.ReadSingle();
                HeadingChanged = binaryReader.ReadBoolean();
                TurnRate = binaryReader.ReadSingle();
                _TurnDirection = (TurnDirection)binaryReader.ReadByte();
                OverlayChanged = binaryReader.ReadBoolean();
                LightChanged = binaryReader.ReadBoolean();
                LightsOn = binaryReader.ReadBoolean();
                Stance = (BuiltObjectStance)binaryReader.ReadByte();
                PurchasePrice = binaryReader.ReadDouble();
                CurrentYearsIncome = binaryReader.ReadDouble();
                DateOfLastIncome = binaryReader.ReadInt64();
                ConsecutiveUnprofitableYears = binaryReader.ReadInt32();
                Scrap = binaryReader.ReadBoolean();
                _AnnualSupportCost = binaryReader.ReadInt32();
                _SupportCostFactor = binaryReader.ReadSingle();
                TacticsStrongerShips = (BattleTactics)binaryReader.ReadByte();
                TacticsWeakerShips = (BattleTactics)binaryReader.ReadByte();
                TacticsInvasion = (InvasionTactics)binaryReader.ReadByte();
                FleeWhen = (BuiltObjectFleeWhen)binaryReader.ReadByte();
                DateBuilt = binaryReader.ReadInt64();
                DateRetrofit = binaryReader.ReadInt64();
                CurrentShields = binaryReader.ReadSingle();
                ShieldsCapacity = binaryReader.ReadInt32();
                ShieldRechargeRate = binaryReader.ReadSingle();
                ShieldAreaRechargeRange = binaryReader.ReadInt16();
                ShieldAreaRechargeCapacity = binaryReader.ReadInt16();
                ShieldAreaRechargeEnergyRequired = binaryReader.ReadInt16();
                Armor = binaryReader.ReadInt16();
                ArmorReactive = binaryReader.ReadInt16();
                ArmorReinforcingFactor = binaryReader.ReadInt16();
                TargettingModifier = binaryReader.ReadInt16();
                CountermeasureModifier = binaryReader.ReadInt16();
                FleetTargettingModifier = binaryReader.ReadInt16();
                FleetCountermeasureModifier = binaryReader.ReadInt16();
                FleetTargettingBonus = binaryReader.ReadInt16();
                FleetCountermeasureBonus = binaryReader.ReadInt16();
                MaintenanceSavings = binaryReader.ReadSingle();
                TradeBonuses = binaryReader.ReadSingle();
                PictureRef = binaryReader.ReadInt32();
                IsSpacePort = binaryReader.ReadBoolean();
                IsColony = binaryReader.ReadBoolean();
                IsResearchLab = binaryReader.ReadBoolean();
                ResearchWeapons = binaryReader.ReadInt32();
                ResearchEnergy = binaryReader.ReadInt32();
                ResearchHighTech = binaryReader.ReadInt32();
                IsResourceExtractor = binaryReader.ReadBoolean();
                ExtractionMine = binaryReader.ReadInt16();
                ExtractionGas = binaryReader.ReadInt16();
                ExtractionLuxury = binaryReader.ReadInt16();
                EnergyToFuelRate = binaryReader.ReadInt16();
                IsManufacturer = binaryReader.ReadBoolean();
                IsEnergyCollector = binaryReader.ReadBoolean();
                EnergyCollection = binaryReader.ReadInt32();
                SensorProximityArrayRange = binaryReader.ReadInt32();
                SensorJumpIntercept = binaryReader.ReadByte();
                SensorResourceProfileSensorRange = binaryReader.ReadInt32();
                SensorLongRange = binaryReader.ReadInt32();
                SensorTraceScannerRange = binaryReader.ReadInt16();
                SensorTraceScannerPower = binaryReader.ReadInt16();
                SensorTraceScannerJamming = binaryReader.ReadInt16();
                MaxPopulation = binaryReader.ReadInt32();
                PopulationCapacity = binaryReader.ReadInt32();
                MedicalCapacity = binaryReader.ReadInt32();
                RecreationCapacity = binaryReader.ReadInt32();
                ParentOffsetX = binaryReader.ReadDouble();
                ParentOffsetY = binaryReader.ReadDouble();
                SuppressAutoRetrofit = binaryReader.ReadBoolean();
                HyperDenyActive = binaryReader.ReadBoolean();
                WeaponHyperDenyRange = binaryReader.ReadInt32();
                HyperStopRange = binaryReader.ReadInt16();
                CanHyperJump = binaryReader.ReadBoolean();
                OptimalMinimumAttackRange = binaryReader.ReadDouble();
                OptimalMaximumAttackRange = binaryReader.ReadDouble();
                MaximumWeaponsRange = binaryReader.ReadInt32();
                MinimumWeaponsRange = binaryReader.ReadInt32();
                PlanetDestroyerWeaponsRange = binaryReader.ReadInt32();
                BombardWeaponPower = binaryReader.ReadInt32();
                BombardRange = binaryReader.ReadInt32();
                PointDefenseWeaponsRange = binaryReader.ReadInt32();
                IonWeaponPower = binaryReader.ReadInt32();
                IonWeaponRange = binaryReader.ReadInt32();
                IonDefense = binaryReader.ReadInt32();
                TractorBeamRange = binaryReader.ReadInt16();
                AssaultStrength = binaryReader.ReadInt16();
                AssaultRange = binaryReader.ReadInt16();
                AssaultShieldPenetration = binaryReader.ReadInt16();
                AssaultAttackValue = binaryReader.ReadInt16();
                AssaultDefenseValue = binaryReader.ReadInt16();
                AssaultAttackEmpireId = binaryReader.ReadByte();
                AssaultIsRaid = binaryReader.ReadBoolean();
                _LastHyperjumpDistance = binaryReader.ReadSingle();
                _DamageReduction = binaryReader.ReadSingle();
                _DamageRepair = binaryReader.ReadInt16();
                LastRepair = binaryReader.ReadInt64();
                PlayerEmpireEncounterAction = (BuiltObjectEncounterAction)binaryReader.ReadByte();
                EncounterEventType = (BuiltObjectEncounterEventType)binaryReader.ReadByte();
                EncounterExplorationBonus = binaryReader.ReadInt16();
                EncounterMoneyBonus = binaryReader.ReadInt32();
                EncounterGovernmentTypeId = binaryReader.ReadByte();
                StandoffWeaponsMaxRange = binaryReader.ReadInt32();
                BeamWeaponsMinRange = binaryReader.ReadInt32();
                CurrentEscortForceAssigned = binaryReader.ReadInt32();
                _ExecutingShipGroupCommand = binaryReader.ReadBoolean();
                RefuelForNextMission = binaryReader.ReadBoolean();
                RetireForNextMission = binaryReader.ReadBoolean();
                RetrofitForNextMission = binaryReader.ReadBoolean();
                RepairForNextMission = binaryReader.ReadBoolean();
                StrandedMessageSent = binaryReader.ReadBoolean();
                _MissionCompleteMessageSent = binaryReader.ReadBoolean();
                _IsBlockaded = binaryReader.ReadBoolean();
                IsAutoControlled = binaryReader.ReadBoolean();
                InBattle = binaryReader.ReadBoolean();
                LastShieldStrikeDirection = binaryReader.ReadSingle();
                _TargetSpeed = binaryReader.ReadInt32();
                _DoingConstruction = binaryReader.ReadBoolean();
                _DoingMining = binaryReader.ReadBoolean();
                _DoingGasMining = binaryReader.ReadBoolean();
                NextSoundTimeConstruction = binaryReader.ReadInt64();
                NextSoundTimeMining = binaryReader.ReadInt64();
                NextSoundTimeGasMining = binaryReader.ReadInt64();
                ScanHabitatIndex = binaryReader.ReadInt32();
                LastScanTime = binaryReader.ReadInt64();
                TroopLoadoutInfantry = binaryReader.ReadByte();
                TroopLoadoutArmored = binaryReader.ReadByte();
                TroopLoadoutArtillery = binaryReader.ReadByte();
                TroopLoadoutSpecialForces = binaryReader.ReadByte();
                _HyperjumpAboutToEnter = binaryReader.ReadBoolean();
                _HyperjumpAboutToEnterSoundPlayed = binaryReader.ReadBoolean();
                _HyperjumpJustExited = binaryReader.ReadBoolean();
                _HyperjumpCountdown = binaryReader.ReadInt64();
                _HyperjumpPrepare = binaryReader.ReadBoolean();
                _HyperjumpX = binaryReader.ReadDouble();
                _HyperjumpY = binaryReader.ReadDouble();
                _FirstHyperjumpExecution = binaryReader.ReadBoolean();
                _LastHyperDistance = binaryReader.ReadDouble();
                _Angle = binaryReader.ReadSingle();
                _LastDockDistance = binaryReader.ReadDouble();
                _LastDistance = binaryReader.ReadDouble();
                _LastInvasionDistance = binaryReader.ReadDouble();
                _LastPositionX = binaryReader.ReadDouble();
                _LastPositionY = binaryReader.ReadDouble();
                HyperExitStartAnimation = binaryReader.ReadBoolean();
                HyperEnterStartAnimation = binaryReader.ReadBoolean();
                UnbuiltComponentCount = binaryReader.ReadInt32();
                DamagedComponentCount = binaryReader.ReadInt32();
                UndamagedComponentSize = binaryReader.ReadInt32();
                _LastTouch = new DateTime(binaryReader.ReadInt64());
                _LastIntermediateTouch = new DateTime(binaryReader.ReadInt64());
                _LastPeriodicTouch = new DateTime(binaryReader.ReadInt64());
                _LastLongTouch = new DateTime(binaryReader.ReadInt64());
                _tempNow = new DateTime(binaryReader.ReadInt64());
                _LastLocationEffectTouch = new DateTime(binaryReader.ReadInt64());
                LastShieldStrike = new DateTime(binaryReader.ReadInt64());
                LastIonStrike = new DateTime(binaryReader.ReadInt64());
                IonStrikeSoundPlayed = binaryReader.ReadBoolean();
                LastTractorStrike = new DateTime(binaryReader.ReadInt64());
                LastTractorStrikeDirection = binaryReader.ReadSingle();
                _DockedAtHabitat = binaryReader.ReadBoolean();
                _BuiltAtHabitat = binaryReader.ReadBoolean();
                FighterCapacity = binaryReader.ReadInt32();
                FighterRepairRate = binaryReader.ReadInt32();
                EncounterTechAdvanceCount = binaryReader.ReadInt32();
                PirateEmpireId = binaryReader.ReadByte();
                _CaptainCountermeasuresBonus = binaryReader.ReadByte();
                _CaptainDamageControlBonus = binaryReader.ReadByte();
                _CaptainFightersBonus = binaryReader.ReadByte();
                _CaptainHyperjumpSpeedBonus = binaryReader.ReadByte();
                _CaptainRepairBonus = binaryReader.ReadByte();
                _CaptainShieldRechargeRateBonus = binaryReader.ReadByte();
                _CaptainShipEnergyUsageBonus = binaryReader.ReadByte();
                _CaptainShipManeuveringBonus = binaryReader.ReadByte();
                _CaptainTargetingBonus = binaryReader.ReadByte();
                _CaptainWeaponsDamageBonus = binaryReader.ReadByte();
                _CaptainWeaponsRangeBonus = binaryReader.ReadByte();
                binaryReader.Close();
            }
            _Galaxy = (Galaxy)info.GetValue("Gx", typeof(Galaxy));
            FuelType = (Resource)info.GetValue("Ft", typeof(Resource));
            _SubsequentMissions = (BuiltObjectMissionList)info.GetValue("SqM", typeof(BuiltObjectMissionList));
            _ManufacturingQueue = (ManufacturingQueue)info.GetValue("MQ", typeof(ManufacturingQueue));
            Design = (Design)info.GetValue("Ds", typeof(Design));
            ShipGroup = (ShipGroup)info.GetValue("SG", typeof(ShipGroup));
            _ColonyToAttack = (Habitat)info.GetValue("CAtk", typeof(Habitat));
            NativeRace = (Race)info.GetValue("Ra", typeof(Race));
            EncounterDescription = (string)info.GetValue("EncDesc", typeof(string));
            Explosions = (ExplosionList)info.GetValue("Exp", typeof(ExplosionList));
            _ContractsToFulfill = (ContractList)info.GetValue("CntL", typeof(ContractList));
            RetrofitDesign = (Design)info.GetValue("RetD", typeof(Design));
            NearestSystemStar = (Habitat)info.GetValue("NSS", typeof(Habitat));
            RevertMission = (BuiltObjectMission)info.GetValue("RevMs", typeof(BuiltObjectMission));
            Mission = (BuiltObjectMission)info.GetValue("Ms", typeof(BuiltObjectMission));
            Components = (BuiltObjectComponentList)info.GetValue("Comps", typeof(BuiltObjectComponentList));
            DisabledComponentIndexes = (List<short>)info.GetValue("DsCmI", typeof(List<short>));
            DisabledComponentDurations = (List<short>)info.GetValue("DsCmD", typeof(List<short>));
            Weapons = (WeaponList)info.GetValue("Weap", typeof(WeaponList));
            CaptainName = (string)info.GetValue("Cpt", typeof(string));
            _DockedAt = (StellarObject)info.GetValue("Dck", typeof(StellarObject));
            _BuiltAt = (StellarObject)info.GetValue("Blt", typeof(StellarObject));
            Fighters = (FighterList)info.GetValue("FtL", typeof(FighterList));
            RetrofitBaseConstructionQueue = (ConstructionQueue)info.GetValue("RtBCQ", typeof(ConstructionQueue));
            RetrofitBaseManufacturingQueue = (ManufacturingQueue)info.GetValue("RtBMQ", typeof(ManufacturingQueue));
            BaconBuiltObject.DeserializeExtraFields(this, info);
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(BuiltObjectID);
                binaryWriter.Write(InView);
                binaryWriter.Write(_IsPlanetDestroyer);
                binaryWriter.Write(_DeployProgress);
                binaryWriter.Write(_IsDeployed);
                binaryWriter.Write(FirstExecutionOfCommand);
                binaryWriter.Write(_IsIndependentOrPirate);
                binaryWriter.Write(_LastWithdrawalEvaluation);
                binaryWriter.Write(_TopSpeedBase);
                binaryWriter.Write(TopSpeedFuelBurn);
                binaryWriter.Write(_CruiseSpeedBase);
                binaryWriter.Write(CruiseSpeed);
                binaryWriter.Write(CruiseSpeedFuelBurn);
                binaryWriter.Write(WarpSpeed);
                binaryWriter.Write(WarpSpeedFuelBurn);
                binaryWriter.Write(HyperjumpInitiate);
                binaryWriter.Write(ImpulseSpeedFuelBurn);
                binaryWriter.Write((byte)EngineType);
                binaryWriter.Write(AccelerationRate);
                binaryWriter.Write(CurrentFuel);
                binaryWriter.Write(_FuelHandicapped);
                binaryWriter.Write(_MovementSlowedLocation);
                binaryWriter.Write(_HyperjumpDisabledLocation);
                binaryWriter.Write(_ShieldsReducedLocation);
                binaryWriter.Write(_ShipPullAmountLocation);
                binaryWriter.Write(_ShipPullAngleLocation);
                binaryWriter.Write(_ShipDamageAmountLocation);
                binaryWriter.Write(FuelCapacity);
                binaryWriter.Write(_RefuelResourceId);
                binaryWriter.Write(_RefuelAmount);
                binaryWriter.Write(_RefuelLocationIsBuiltObject);
                binaryWriter.Write(_RefuelLocationId);
                binaryWriter.Write(AttackRangeSquared);
                binaryWriter.Write(CurrentEnergy);
                binaryWriter.Write(StaticEnergyConsumption);
                binaryWriter.Write(ReactorPowerOutput);
                binaryWriter.Write(ReactorStorageCapacity);
                binaryWriter.Write(ReactorCycleFuelConsumption);
                binaryWriter.Write(CurrentReactorStorage);
                binaryWriter.Write(TargetSpeedChanged);
                binaryWriter.Write(PreferredSpeed);
                binaryWriter.Write(_CargoCapacity);
                binaryWriter.Write(TroopCapacity);
                binaryWriter.Write((byte)Role);
                binaryWriter.Write((byte)SubRole);
                binaryWriter.Write(_Heading);
                binaryWriter.Write(HeadingChanged);
                binaryWriter.Write(TurnRate);
                binaryWriter.Write((byte)_TurnDirection);
                binaryWriter.Write(OverlayChanged);
                binaryWriter.Write(LightChanged);
                binaryWriter.Write(LightsOn);
                binaryWriter.Write((byte)Stance);
                binaryWriter.Write(PurchasePrice);
                binaryWriter.Write(CurrentYearsIncome);
                binaryWriter.Write(DateOfLastIncome);
                binaryWriter.Write(ConsecutiveUnprofitableYears);
                binaryWriter.Write(Scrap);
                binaryWriter.Write(_AnnualSupportCost);
                binaryWriter.Write(_SupportCostFactor);
                binaryWriter.Write((byte)TacticsStrongerShips);
                binaryWriter.Write((byte)TacticsWeakerShips);
                binaryWriter.Write((byte)TacticsInvasion);
                binaryWriter.Write((byte)FleeWhen);
                binaryWriter.Write(DateBuilt);
                binaryWriter.Write(DateRetrofit);
                binaryWriter.Write(CurrentShields);
                binaryWriter.Write(ShieldsCapacity);
                binaryWriter.Write(ShieldRechargeRate);
                binaryWriter.Write(ShieldAreaRechargeRange);
                binaryWriter.Write(ShieldAreaRechargeCapacity);
                binaryWriter.Write(ShieldAreaRechargeEnergyRequired);
                binaryWriter.Write(Armor);
                binaryWriter.Write(ArmorReactive);
                binaryWriter.Write(ArmorReinforcingFactor);
                binaryWriter.Write(TargettingModifier);
                binaryWriter.Write(CountermeasureModifier);
                binaryWriter.Write(FleetTargettingModifier);
                binaryWriter.Write(FleetCountermeasureModifier);
                binaryWriter.Write(FleetTargettingBonus);
                binaryWriter.Write(FleetCountermeasureBonus);
                binaryWriter.Write(MaintenanceSavings);
                binaryWriter.Write(TradeBonuses);
                binaryWriter.Write(PictureRef);
                binaryWriter.Write(IsSpacePort);
                binaryWriter.Write(IsColony);
                binaryWriter.Write(IsResearchLab);
                binaryWriter.Write(ResearchWeapons);
                binaryWriter.Write(ResearchEnergy);
                binaryWriter.Write(ResearchHighTech);
                binaryWriter.Write(IsResourceExtractor);
                binaryWriter.Write(ExtractionMine);
                binaryWriter.Write(ExtractionGas);
                binaryWriter.Write(ExtractionLuxury);
                binaryWriter.Write(EnergyToFuelRate);
                binaryWriter.Write(IsManufacturer);
                binaryWriter.Write(IsEnergyCollector);
                binaryWriter.Write(EnergyCollection);
                binaryWriter.Write(SensorProximityArrayRange);
                binaryWriter.Write(SensorJumpIntercept);
                binaryWriter.Write(SensorResourceProfileSensorRange);
                binaryWriter.Write(SensorLongRange);
                binaryWriter.Write(SensorTraceScannerRange);
                binaryWriter.Write(SensorTraceScannerPower);
                binaryWriter.Write(SensorTraceScannerJamming);
                binaryWriter.Write(MaxPopulation);
                binaryWriter.Write(PopulationCapacity);
                binaryWriter.Write(MedicalCapacity);
                binaryWriter.Write(RecreationCapacity);
                binaryWriter.Write(ParentOffsetX);
                binaryWriter.Write(ParentOffsetY);
                binaryWriter.Write(SuppressAutoRetrofit);
                binaryWriter.Write(HyperDenyActive);
                binaryWriter.Write(WeaponHyperDenyRange);
                binaryWriter.Write(HyperStopRange);
                binaryWriter.Write(CanHyperJump);
                binaryWriter.Write(OptimalMinimumAttackRange);
                binaryWriter.Write(OptimalMaximumAttackRange);
                binaryWriter.Write(MaximumWeaponsRange);
                binaryWriter.Write(MinimumWeaponsRange);
                binaryWriter.Write(PlanetDestroyerWeaponsRange);
                binaryWriter.Write(BombardWeaponPower);
                binaryWriter.Write(BombardRange);
                binaryWriter.Write(PointDefenseWeaponsRange);
                binaryWriter.Write(IonWeaponPower);
                binaryWriter.Write(IonWeaponRange);
                binaryWriter.Write(IonDefense);
                binaryWriter.Write(TractorBeamRange);
                binaryWriter.Write(AssaultStrength);
                binaryWriter.Write(AssaultRange);
                binaryWriter.Write(AssaultShieldPenetration);
                binaryWriter.Write(AssaultAttackValue);
                binaryWriter.Write(AssaultDefenseValue);
                binaryWriter.Write(AssaultAttackEmpireId);
                binaryWriter.Write(AssaultIsRaid);
                binaryWriter.Write(_LastHyperjumpDistance);
                binaryWriter.Write(_DamageReduction);
                binaryWriter.Write(_DamageRepair);
                binaryWriter.Write(LastRepair);
                binaryWriter.Write((byte)PlayerEmpireEncounterAction);
                binaryWriter.Write((byte)EncounterEventType);
                binaryWriter.Write(EncounterExplorationBonus);
                binaryWriter.Write(EncounterMoneyBonus);
                binaryWriter.Write(EncounterGovernmentTypeId);
                binaryWriter.Write(StandoffWeaponsMaxRange);
                binaryWriter.Write(BeamWeaponsMinRange);
                binaryWriter.Write(CurrentEscortForceAssigned);
                binaryWriter.Write(_ExecutingShipGroupCommand);
                binaryWriter.Write(RefuelForNextMission);
                binaryWriter.Write(RetireForNextMission);
                binaryWriter.Write(RetrofitForNextMission);
                binaryWriter.Write(RepairForNextMission);
                binaryWriter.Write(StrandedMessageSent);
                binaryWriter.Write(_MissionCompleteMessageSent);
                binaryWriter.Write(_IsBlockaded);
                binaryWriter.Write(IsAutoControlled);
                binaryWriter.Write(InBattle);
                binaryWriter.Write(LastShieldStrikeDirection);
                binaryWriter.Write(_TargetSpeed);
                binaryWriter.Write(_DoingConstruction);
                binaryWriter.Write(_DoingMining);
                binaryWriter.Write(_DoingGasMining);
                binaryWriter.Write(NextSoundTimeConstruction);
                binaryWriter.Write(NextSoundTimeMining);
                binaryWriter.Write(NextSoundTimeGasMining);
                binaryWriter.Write(ScanHabitatIndex);
                binaryWriter.Write(LastScanTime);
                binaryWriter.Write(TroopLoadoutInfantry);
                binaryWriter.Write(TroopLoadoutArmored);
                binaryWriter.Write(TroopLoadoutArtillery);
                binaryWriter.Write(TroopLoadoutSpecialForces);
                binaryWriter.Write(_HyperjumpAboutToEnter);
                binaryWriter.Write(_HyperjumpAboutToEnterSoundPlayed);
                binaryWriter.Write(_HyperjumpJustExited);
                binaryWriter.Write(_HyperjumpCountdown);
                binaryWriter.Write(_HyperjumpPrepare);
                binaryWriter.Write(_HyperjumpX);
                binaryWriter.Write(_HyperjumpY);
                binaryWriter.Write(_FirstHyperjumpExecution);
                binaryWriter.Write(_LastHyperDistance);
                binaryWriter.Write(_Angle);
                binaryWriter.Write(_LastDockDistance);
                binaryWriter.Write(_LastDistance);
                binaryWriter.Write(_LastInvasionDistance);
                binaryWriter.Write(_LastPositionX);
                binaryWriter.Write(_LastPositionY);
                binaryWriter.Write(HyperExitStartAnimation);
                binaryWriter.Write(HyperEnterStartAnimation);
                binaryWriter.Write(UnbuiltComponentCount);
                binaryWriter.Write(DamagedComponentCount);
                binaryWriter.Write(UndamagedComponentSize);
                binaryWriter.Write(_LastTouch.Ticks);
                binaryWriter.Write(_LastIntermediateTouch.Ticks);
                binaryWriter.Write(_LastPeriodicTouch.Ticks);
                binaryWriter.Write(_LastLongTouch.Ticks);
                binaryWriter.Write(_tempNow.Ticks);
                binaryWriter.Write(_LastLocationEffectTouch.Ticks);
                binaryWriter.Write(LastShieldStrike.Ticks);
                binaryWriter.Write(LastIonStrike.Ticks);
                binaryWriter.Write(IonStrikeSoundPlayed);
                binaryWriter.Write(LastTractorStrike.Ticks);
                binaryWriter.Write(LastTractorStrikeDirection);
                binaryWriter.Write(_DockedAtHabitat);
                binaryWriter.Write(_BuiltAtHabitat);
                binaryWriter.Write(FighterCapacity);
                binaryWriter.Write(FighterRepairRate);
                binaryWriter.Write(EncounterTechAdvanceCount);
                binaryWriter.Write(PirateEmpireId);
                binaryWriter.Write(_CaptainCountermeasuresBonus);
                binaryWriter.Write(_CaptainDamageControlBonus);
                binaryWriter.Write(_CaptainFightersBonus);
                binaryWriter.Write(_CaptainHyperjumpSpeedBonus);
                binaryWriter.Write(_CaptainRepairBonus);
                binaryWriter.Write(_CaptainShieldRechargeRateBonus);
                binaryWriter.Write(_CaptainShipEnergyUsageBonus);
                binaryWriter.Write(_CaptainShipManeuveringBonus);
                binaryWriter.Write(_CaptainTargetingBonus);
                binaryWriter.Write(_CaptainWeaponsDamageBonus);
                binaryWriter.Write(_CaptainWeaponsRangeBonus);
                binaryWriter.Flush();
                binaryWriter.Close();
                info.AddValue("BO_D", memoryStream.ToArray());
            }
            info.AddValue("Gx", _Galaxy);
            info.AddValue("Ft", FuelType);
            info.AddValue("SqM", _SubsequentMissions);
            info.AddValue("MQ", _ManufacturingQueue);
            info.AddValue("Ds", Design);
            info.AddValue("SG", ShipGroup);
            info.AddValue("CAtk", _ColonyToAttack);
            info.AddValue("Ra", NativeRace);
            info.AddValue("EncDesc", EncounterDescription);
            info.AddValue("Exp", Explosions);
            info.AddValue("CntL", _ContractsToFulfill);
            info.AddValue("RetD", RetrofitDesign);
            info.AddValue("NSS", NearestSystemStar);
            info.AddValue("RevMs", RevertMission);
            info.AddValue("Ms", Mission);
            info.AddValue("Comps", Components);
            info.AddValue("DsCmI", DisabledComponentIndexes);
            info.AddValue("DsCmD", DisabledComponentDurations);
            info.AddValue("Weap", Weapons);
            info.AddValue("Cpt", CaptainName);
            info.AddValue("Dck", _DockedAt);
            info.AddValue("Blt", _BuiltAt);
            info.AddValue("FtL", Fighters);
            info.AddValue("RtBCQ", RetrofitBaseConstructionQueue);
            info.AddValue("RtBMQ", RetrofitBaseManufacturingQueue);
            BaconBuiltObject.SerializeExtraFields(this, info);
        }

        public void ReviewCaptainBonuses()
        {
            byte captainTargetingBonus = 100;
            byte captainCountermeasuresBonus = 100;
            byte captainShipManeuveringBonus = 100;
            byte captainFightersBonus = 100;
            byte captainShipEnergyUsageBonus = 100;
            byte captainWeaponsDamageBonus = 100;
            byte captainWeaponsRangeBonus = 100;
            byte captainShieldRechargeRateBonus = 100;
            byte captainDamageControlBonus = 100;
            byte captainRepairBonus = 100;
            byte captainHyperjumpSpeedBonus = 100;
            if (Characters != null && Characters.Count > 0)
            {
                captainTargetingBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.Targeting)));
                captainCountermeasuresBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.Countermeasures)));
                captainShipManeuveringBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.ShipManeuvering)));
                captainFightersBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.Fighters)));
                captainShipEnergyUsageBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.ShipEnergyUsage)));
                captainWeaponsDamageBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.WeaponsDamage)));
                captainWeaponsRangeBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.WeaponsRange)));
                captainShieldRechargeRateBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.ShieldRechargeRate)));
                captainDamageControlBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.DamageControl)));
                captainRepairBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.RepairBonus)));
                captainHyperjumpSpeedBonus = (byte)Math.Max(0, Math.Min(200, 100 + Characters.GetHighestSkillLevel(CharacterSkillType.HyperjumpSpeed)));
            }
            _CaptainTargetingBonus = captainTargetingBonus;
            _CaptainCountermeasuresBonus = captainCountermeasuresBonus;
            _CaptainShipManeuveringBonus = captainShipManeuveringBonus;
            _CaptainFightersBonus = captainFightersBonus;
            _CaptainShipEnergyUsageBonus = captainShipEnergyUsageBonus;
            _CaptainWeaponsDamageBonus = captainWeaponsDamageBonus;
            _CaptainWeaponsRangeBonus = captainWeaponsRangeBonus;
            _CaptainShieldRechargeRateBonus = captainShieldRechargeRateBonus;
            _CaptainDamageControlBonus = captainDamageControlBonus;
            _CaptainRepairBonus = captainRepairBonus;
            _CaptainHyperjumpSpeedBonus = captainHyperjumpSpeedBonus;
        }

        public int GetCharacterMaintenanceBonuses()
        {
            int num = 0;
            Empire actualEmpire = ActualEmpire;
            if (actualEmpire != null)
            {
                Character leader = actualEmpire.Leader;
                CharacterSkillType characterSkillType = CharacterSkillType.CivilianBaseMaintenance;
                switch (Role)
                {
                    case BuiltObjectRole.Military:
                        characterSkillType = CharacterSkillType.MilitaryShipMaintenance;
                        if (leader != null)
                        {
                            num += leader.MilitaryShipMaintenance;
                        }
                        break;
                    case BuiltObjectRole.Base:
                        switch (SubRole)
                        {
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.DefensiveBase:
                                characterSkillType = CharacterSkillType.MilitaryBaseMaintenance;
                                if (leader != null)
                                {
                                    num += leader.MilitaryBaseMaintenance;
                                }
                                break;
                            default:
                                characterSkillType = CharacterSkillType.CivilianBaseMaintenance;
                                if (leader != null)
                                {
                                    num += leader.CivilianBaseMaintenance;
                                }
                                break;
                        }
                        break;
                    default:
                        characterSkillType = CharacterSkillType.CivilianShipMaintenance;
                        if (leader != null)
                        {
                            num += leader.CivilianShipMaintenance;
                        }
                        break;
                }
                int val = 0;
                int val2 = 0;
                if (Characters != null && Characters.Count > 0)
                {
                    val = Characters.GetHighestSkillLevel(characterSkillType);
                }
                if (ParentHabitat != null && ParentHabitat.Characters != null && ParentHabitat.Characters.Count > 0)
                {
                    val2 = ParentHabitat.Characters.GetHighestSkillLevelExcludeLeaders(characterSkillType);
                }
                num += Math.Max(val, val2);
            }
            return num;
        }

        public BuiltObject(Design design, string name, Galaxy galaxy)
            : this(design, name, galaxy, fullyBuilt: false)
        {
        }

        public BuiltObject(Design design, string name, Galaxy galaxy, bool fullyBuilt)
            : this(design, name, galaxy, fullyBuilt, doNotAssignEmpire: false)
        {
        }

        public BuiltObject(Design design, string name, Galaxy galaxy, bool fullyBuilt, bool doNotAssignEmpire)
        {
            _Galaxy = galaxy;
            Design = design;
            Components = new BuiltObjectComponentList();
            if (fullyBuilt)
            {
                for (int i = 0; i < design.Components.Count; i++)
                {
                    Component component = design.Components[i];
                    BuiltObjectComponent component2 = new BuiltObjectComponent(component.ComponentID, ComponentStatus.Normal);
                    Components.Add(component2);
                }
            }
            else
            {
                for (int j = 0; j < design.Components.Count; j++)
                {
                    Component component3 = design.Components[j];
                    BuiltObjectComponent component4 = new BuiltObjectComponent(component3.ComponentID, ComponentStatus.Unbuilt);
                    Components.Add(component4);
                }
            }
            DockingBays = null;
            DockingBayWaitQueue = null;
            ConstructionQueue = null;
            _ManufacturingQueue = null;
            Weapons = new WeaponList();
            Attackers = new StellarObjectList();
            Pursuers = new StellarObjectList();
            Troops = null;
            Characters = new CharacterList();
            _ContractsToFulfill = new ContractList();
            PurchasePrice = design.CalculateCurrentPurchasePrice(_Galaxy);
            Name = name;
            if (!doNotAssignEmpire)
            {
                Empire = design.Empire;
                if (design.Empire != null && design.Empire.PirateEmpireBaseHabitat != null)
                {
                    PirateEmpireId = (byte)design.Empire.EmpireId;
                }
            }
            FirstExecutionOfCommand = true;
            PictureRef = design.PictureRef;
            Role = design.Role;
            SubRole = design.SubRole;
            Stance = design.Stance;
            FleeWhen = design.FleeWhen;
            if ((Role == BuiltObjectRole.Military || Role == BuiltObjectRole.Base) && Empire != null)
            {
                if (Empire.AttackRangeOther < 0)
                {
                    AttackRangeSquared = 2.304E+09f;
                }
                else
                {
                    AttackRangeSquared = (float)Empire.AttackRangeOther * (float)Empire.AttackRangeOther;
                }
            }
            CurrentFuel = 0.0;
            Heading = -(float)Math.PI / 2f;
            TargetHeading = Heading;
            if (Empire == _Galaxy.IndependentEmpire || _Galaxy.PirateEmpires.Contains(Empire))
            {
                _IsIndependentOrPirate = true;
            }
            SuppressAutoRetrofit = !Design.AllowAutoRetrofit;
        }

        public void SetTroopLoadoutsFromPolicy(EmpirePolicy policy)
        {
            if (policy == null)
            {
                return;
            }
            if (policy.TroopUseDefaultTransportLoadout)
            {
                int num = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Infantry));
                int num2 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Armored));
                int num3 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Artillery));
                int num4 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.SpecialForces));
                int troopCapacity = TroopCapacity;
                float num5 = policy.TroopDefaultTransportLoadoutInfantry + policy.TroopDefaultTransportLoadoutArmor + policy.TroopDefaultTransportLoadoutArtillery + policy.TroopDefaultTransportLoadoutSpecialForces;
                float num6 = 1f / num5;
                int num7 = (int)((float)troopCapacity * policy.TroopDefaultTransportLoadoutInfantry * num6);
                int num8 = (int)((float)troopCapacity * policy.TroopDefaultTransportLoadoutArmor * num6);
                int num9 = (int)((float)troopCapacity * policy.TroopDefaultTransportLoadoutArtillery * num6);
                int num10 = (int)((float)troopCapacity * policy.TroopDefaultTransportLoadoutSpecialForces * num6);
                if (num10 < num4)
                {
                    num7 += num10;
                    num10 = 0;
                }
                if (num9 < num3)
                {
                    num7 += num9;
                    num9 = 0;
                }
                if (num8 < num2)
                {
                    num7 += num8;
                    num8 = 0;
                }
                num7 = num7 / num * num;
                num8 = num8 / num2 * num2;
                num9 = num9 / num3 * num3;
                num10 = num10 / num4 * num4;
                int num11 = num7 + num9 + num8 + num10;
                int num12 = troopCapacity - num11;
                if (num12 >= 100 && Empire != null)
                {
                    if (num12 >= num2 && Empire.TroopCanRecruitArmored && TroopLoadoutArmored >= TroopLoadoutInfantry && TroopLoadoutArmored >= TroopLoadoutArtillery && TroopLoadoutArmored >= TroopLoadoutSpecialForces)
                    {
                        int num13 = num12 / num2;
                        num8 += num13 * num2;
                        num12 -= num13 * num2;
                    }
                    if (num12 >= num4 && Empire.TroopCanRecruitSpecialForces && TroopLoadoutSpecialForces >= TroopLoadoutInfantry && TroopLoadoutSpecialForces >= TroopLoadoutArtillery && TroopLoadoutSpecialForces >= TroopLoadoutArmored)
                    {
                        int num14 = num12 / num4;
                        num10 += num14 * num4;
                        num12 -= num14 * num4;
                    }
                    if (num12 >= num3 && Empire.TroopCanRecruitArtillery && TroopLoadoutArtillery >= TroopLoadoutInfantry && TroopLoadoutArtillery >= TroopLoadoutArmored && TroopLoadoutArtillery >= TroopLoadoutSpecialForces)
                    {
                        int num15 = num12 / num3;
                        num9 += num15 * num3;
                        num12 -= num15 * num3;
                    }
                    if (num12 >= num && Empire.TroopCanRecruitInfantry && TroopLoadoutInfantry >= TroopLoadoutArmored && TroopLoadoutInfantry >= TroopLoadoutArtillery && TroopLoadoutInfantry >= TroopLoadoutSpecialForces)
                    {
                        int num16 = num12 / num;
                        num7 += num16 * num;
                        num12 -= num16 * num;
                    }
                    if (num12 >= 100)
                    {
                        int num17 = num12 / num;
                        num7 += num17 * num;
                        num12 -= num17 * num;
                    }
                }
                TroopLoadoutInfantry = (byte)(num7 / num);
                TroopLoadoutArmored = (byte)(num8 / num2);
                TroopLoadoutArtillery = (byte)(num9 / num3);
                TroopLoadoutSpecialForces = (byte)(num10 / num4);
            }
            else
            {
                TroopLoadoutInfantry = byte.MaxValue;
                TroopLoadoutArmored = byte.MaxValue;
                TroopLoadoutArtillery = byte.MaxValue;
                TroopLoadoutSpecialForces = byte.MaxValue;
            }
        }

        public void GetTroopLoadoutTargetAmounts(out int infantryAmount, out int artilleryAmount, out int armorAmount, out int specialForcesAmount)
        {
            infantryAmount = TroopLoadoutInfantry;
            artilleryAmount = TroopLoadoutArtillery;
            armorAmount = TroopLoadoutArmored;
            specialForcesAmount = TroopLoadoutSpecialForces;
            if (infantryAmount == 0 && artilleryAmount == 0 && armorAmount == 0 && specialForcesAmount == 0)
            {
                infantryAmount = TroopCapacity / 100;
            }
            else if (infantryAmount == 255 && artilleryAmount == 255 && armorAmount == 255 && specialForcesAmount == 255)
            {
                infantryAmount = TroopCapacity / 100;
                artilleryAmount = 0;
                armorAmount = 0;
                specialForcesAmount = 0;
            }
            if (Empire != null)
            {
                int num = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Infantry));
                int num2 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Armored));
                int num3 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Artillery));
                int num4 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.SpecialForces));
                if (!Empire.TroopCanRecruitSpecialForces)
                {
                    armorAmount += specialForcesAmount * num4 / num2;
                    specialForcesAmount = 0;
                }
                if (!Empire.TroopCanRecruitArmored)
                {
                    infantryAmount += armorAmount * num2 / num;
                    armorAmount = 0;
                }
                if (!Empire.TroopCanRecruitArtillery)
                {
                    infantryAmount += artilleryAmount * num3 / num;
                    artilleryAmount = 0;
                }
            }
        }

        public void InitiateUndeploy()
        {
            if (SubRole == BuiltObjectSubRole.ResupplyShip && Empire != null)
            {
                if (Empire.ShipGroups != null)
                {
                    for (int i = 0; i < Empire.ShipGroups.Count; i++)
                    {
                        ShipGroup shipGroup = Empire.ShipGroups[i];
                        if (shipGroup.Mission != null && (shipGroup.Mission.TargetBuiltObject == this || shipGroup.Mission.SecondaryTargetBuiltObject == this))
                        {
                            shipGroup.ForceCompleteMission();
                        }
                    }
                }
                for (int j = 0; j < Empire.BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject = Empire.BuiltObjects[j];
                    if (builtObject.Mission != null && (builtObject.Mission.TargetBuiltObject == this || builtObject.Mission.SecondaryTargetBuiltObject == this))
                    {
                        builtObject.ClearPreviousMissionRequirements();
                    }
                }
            }
            _IsDeployed = false;
            _DeployProgress = -0.01f;
        }

        public void LaunchAvailableFighters()
        {
            if (Fighters == null || Fighters.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < Fighters.Count; i++)
            {
                Fighter fighter = Fighters[i];
                if (fighter.Specification.Type == FighterType.Interceptor && fighter.Health >= 1f && !fighter.UnderConstruction)
                {
                    LaunchFighter(fighter);
                }
            }
        }

        public void LaunchAvailableBombers()
        {
            if (Fighters == null || Fighters.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < Fighters.Count; i++)
            {
                Fighter fighter = Fighters[i];
                if (fighter.Specification.Type == FighterType.Bomber && fighter.Health >= 1f && !fighter.UnderConstruction)
                {
                    LaunchFighter(fighter);
                }
            }
        }

        private void LaunchAllFighters()
        {
            if (FighterCapacity <= 0 || Fighters == null || Fighters.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < Fighters.Count; i++)
            {
                Fighter fighter = Fighters[i];
                if (fighter.Health >= 1f && !fighter.UnderConstruction)
                {
                    LaunchFighter(fighter);
                }
            }
        }

        public void LaunchFighter(Fighter fighter)
        {
            if (fighter.OnboardCarrier)
            {
                fighter.Xpos = Xpos;
                fighter.Ypos = Ypos;
                fighter.OnboardCarrier = false;
                fighter.MissionType = FighterMissionType.Patrol;
                float heading = Heading;
                heading = (fighter.Heading = ((Galaxy.Rnd.Next(0, 2) != 1) ? (heading - ((float)Math.PI / 2f + fighter.RandomHeadingOffset(_Galaxy))) : (heading + ((float)Math.PI / 2f + fighter.RandomHeadingOffset(_Galaxy)))));
                fighter.CurrentSpeed = (float)fighter.TopSpeed * 0.3f;
                fighter.EvaluateThreats(_Galaxy);
            }
        }

        public void ReturnFighters()
        {
            if (Fighters == null || Fighters.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < Fighters.Count; i++)
            {
                if (Fighters[i].Specification.Type == FighterType.Interceptor && !Fighters[i].OnboardCarrier)
                {
                    Fighters[i].ReturnToCarrier();
                }
            }
        }

        public void ReturnBombers()
        {
            if (Fighters == null || Fighters.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < Fighters.Count; i++)
            {
                if (Fighters[i].Specification.Type == FighterType.Bomber && !Fighters[i].OnboardCarrier)
                {
                    Fighters[i].ReturnToCarrier();
                }
            }
        }

        public void BuildNewFighter()
        {
            FighterSpecification fighterSpecification = Empire.Research.IdentifyLatestFighterSpecification();
            BuildFighter(fighterSpecification);
        }

        public void BuildNewBomber()
        {
            FighterSpecification fighterSpecification = Empire.Research.IdentifyLatestBomberSpecification();
            BuildFighter(fighterSpecification);
        }

        public void BuildFighter(FighterSpecification fighterSpecification)
        {
            BaconBuiltObject.BuildFighter(this, fighterSpecification);
        }

        private void BuildNewFighters()
        {
            BaconBuiltObject.BuildNewFighters(this);
        }

        private void ManufactureRepairFighters(double timePassed)
        {
            BaconBuiltObject.ManufactureRepairFighters(this, timePassed);
        }

        private void ReviewFleetBonuses()
        {
            short num = 0;
            short num2 = 0;
            if (ShipGroup != null && ShipGroup.Ships != null)
            {
                for (int i = 0; i < ShipGroup.Ships.Count; i++)
                {
                    BuiltObject builtObject = ShipGroup.Ships[i];
                    if (builtObject.FleetTargettingModifier > num || builtObject.FleetCountermeasureModifier > num2)
                    {
                        double num3 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject.Xpos, builtObject.Ypos);
                        if (num3 <= 4000000.0)
                        {
                            num = Math.Max(num, builtObject.FleetTargettingModifier);
                            num2 = Math.Max(num2, builtObject.FleetCountermeasureModifier);
                        }
                    }
                }
            }
            FleetTargettingBonus = num;
            FleetCountermeasureBonus = num2;
        }

        private void CheckShieldAreaRechargeReset(DateTime time)
        {
            if (ShieldAreaRechargeRange > 0 && ShieldAreaRechargeStartTime > DateTime.MinValue && ShieldAreaRechargeTarget != null)
            {
                if (ShieldAreaRechargeStartTime.Subtract(time).TotalSeconds > 3.0)
                {
                    ShieldAreaRechargeStartTime = DateTime.MinValue;
                    ShieldAreaRechargeTarget = null;
                }
                else if (ShieldAreaRechargeTarget != null && ShieldAreaRechargeTarget.CurrentSpeed >= (float)ShieldAreaRechargeTarget.WarpSpeed && ShieldAreaRechargeTarget.WarpSpeed > 0)
                {
                    ShieldAreaRechargeStartTime = DateTime.MinValue;
                    ShieldAreaRechargeTarget = null;
                }
            }
        }

        private void CheckNearbyBuiltObjectsForShieldAreaRecharge(DateTime time)
        {
            if (ShieldAreaRechargeRange <= 0 || !IsFunctional || !(CurrentEnergy > (double)(ReactorStorageCapacity / 3)) || !(CurrentEnergy > (double)(ShieldAreaRechargeEnergyRequired / 3)))
            {
                return;
            }
            ShieldAreaRechargeTarget = null;
            double num = (double)ShieldAreaRechargeRange * (double)ShieldAreaRechargeRange;
            BuiltObjectList builtObjectsAtLocation = _Galaxy.GetBuiltObjectsAtLocation(Xpos, Ypos, ShieldAreaRechargeRange);
            int num2 = Galaxy.Rnd.Next(0, builtObjectsAtLocation.Count);
            bool flag = false;
            for (int i = num2; i < builtObjectsAtLocation.Count; i++)
            {
                BuiltObject builtObject = builtObjectsAtLocation[i];
                if (builtObject != null && builtObject.Empire == Empire && builtObject.CurrentShields < (float)((double)builtObject.ShieldsCapacity * 0.67) && builtObject != this && builtObject.CurrentSpeed < (float)builtObject.WarpSpeed)
                {
                    double num3 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num3 <= num)
                    {
                        int val = builtObject.ShieldsCapacity - (int)builtObject.CurrentShields;
                        val = Math.Min(val, ShieldAreaRechargeCapacity);
                        double num4 = val / ShieldAreaRechargeCapacity;
                        double num5 = (double)ShieldAreaRechargeEnergyRequired * num4;
                        builtObject.CurrentShields += val;
                        CurrentEnergy -= num5;
                        ShieldAreaRechargeTarget = builtObject;
                        ShieldAreaRechargeStartTime = time;
                        flag = true;
                        break;
                    }
                }
            }
            if (flag)
            {
                return;
            }
            for (int j = 0; j < num2; j++)
            {
                BuiltObject builtObject2 = builtObjectsAtLocation[j];
                if (builtObject2 != null && builtObject2.Empire == Empire && builtObject2.CurrentShields < (float)(builtObject2.ShieldsCapacity / 2) && builtObject2 != this)
                {
                    double num6 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject2.Xpos, builtObject2.Ypos);
                    if (num6 <= num)
                    {
                        int val2 = builtObject2.ShieldsCapacity - (int)builtObject2.CurrentShields;
                        val2 = Math.Min(val2, ShieldAreaRechargeCapacity);
                        double num7 = val2 / ShieldAreaRechargeCapacity;
                        double num8 = (double)ShieldAreaRechargeEnergyRequired * num7;
                        builtObject2.CurrentShields += val2;
                        CurrentEnergy -= num8;
                        ShieldAreaRechargeTarget = builtObject2;
                        ShieldAreaRechargeStartTime = time;
                        flag = true;
                        break;
                    }
                }
            }
        }

        public void SendCharactersHome()
        {
            if ((DamagedComponentCount > 0 && InBattle) || Characters == null)
            {
                return;
            }
            Character[] array = ListHelper.ToArrayThreadSafe(Characters);
            foreach (Character character in array)
            {
                if (character == null || character.Empire == null)
                {
                    continue;
                }
                if (character.Empire.Capital != null)
                {
                    character.CompleteLocationTransfer(character.Empire.Capital, _Galaxy);
                }
                else if (character.Empire.PirateEmpireBaseHabitat != null)
                {
                    BuiltObject builtObject = _Galaxy.IdentifyPirateSpaceport(character.Empire);
                    if (builtObject != null)
                    {
                        character.CompleteLocationTransfer(builtObject, _Galaxy);
                    }
                }
            }
        }

        public void ReviewWeaponsComponentValues()
        {
            if (Weapons != null && Weapons.Count > 0)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapons[i]?.ReviewValues(Empire);
                }
            }
        }

        public void ReDefine()
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            bool flag9 = false;
            bool flag10 = false;
            int sensorLongRange = SensorLongRange;
            ReactorPowerOutput = 0;
            ReactorStorageCapacity = 0;
            ReactorCycleFuelConsumption = 0;
            StaticEnergyConsumption = 0;
            SensorProximityArrayRange = 0;
            SensorResourceProfileSensorRange = 0;
            SensorLongRange = 0;
            SensorTraceScannerRange = 0;
            SensorTraceScannerPower = 0;
            SensorTraceScannerJamming = 0;
            WeaponHyperDenyRange = 0;
            HyperStopRange = 0;
            MaximumWeaponsRange = 0;
            MinimumWeaponsRange = 100000;
            StandoffWeaponsMaxRange = 0;
            BeamWeaponsMinRange = 100000;
            PointDefenseWeaponsRange = 0;
            IonDefense = 0;
            IonWeaponPower = 0;
            IonWeaponRange = 0;
            Armor = 0;
            ArmorReactive = 0;
            ArmorReinforcingFactor = 0;
            Stealth = 1f;
            _DamageReduction = 0f;
            _DamageRepair = 0;
            IsColony = false;
            IsEnergyCollector = false;
            IsFunctional = false;
            IsManufacturer = false;
            IsRefuellingDepot = false;
            IsResearchLab = false;
            IsResourceExtractor = false;
            IsShipYard = false;
            IsSpacePort = false;
            HyperDenyActive = false;
            _IsPlanetDestroyer = false;
            ResearchWeapons = 0;
            ResearchEnergy = 0;
            ResearchHighTech = 0;
            ExtractionMine = 0;
            ExtractionGas = 0;
            ExtractionLuxury = 0;
            EnergyToFuelRate = 0;
            EnergyCollection = 0;
            MedicalCapacity = 0;
            RecreationCapacity = 0;
            CountermeasureModifier = 0;
            TargettingModifier = 0;
            FleetTargettingModifier = 0;
            FleetCountermeasureModifier = 0;
            MaintenanceSavings = 0f;
            TradeBonuses = 0f;
            ShieldAreaRechargeRange = 0;
            ShieldAreaRechargeCapacity = 0;
            ShieldAreaRechargeEnergyRequired = 0;
            ShieldAreaRechargeTarget = null;
            TractorBeamRange = 0;
            AssaultStrength = 0;
            AssaultRange = 0;
            AssaultShieldPenetration = 0;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 10000;
            int num8 = 0;
            int num9 = 0;
            float num10 = 0f;
            short num11 = 0;
            short num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            short num22 = short.MaxValue;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            int num26 = 0;
            int num27 = 0;
            double num28 = 0.0;
            int num29 = 0;
            int num30 = 0;
            short num31 = 0;
            int num32 = 0;
            int num33 = 0;
            int num34 = 0;
            int num35 = 0;
            int num36 = 0;
            int num37 = 0;
            int num38 = 0;
            int num39 = 0;
            int num40 = 0;
            int num41 = 0;
            lock (_redefineLock)
            {
                WeaponList weaponList = new WeaponList();
                for (int i = 0; i < Components.Count; i++)
                {
                    num28 += _Galaxy.ComponentCurrentPrices[Components[i].ComponentID];
                    num23 += Components[i].Size;
                    StaticEnergyConsumption += Components[i].EnergyUsed;
                    if (Components[i].Status == ComponentStatus.Normal)
                    {
                        num32 += Components[i].Size;
                        bool flag11 = false;
                        if (DisabledComponentIndexes != null && DisabledComponentIndexes.Count > 0 && DisabledComponentIndexes.Contains((short)i))
                        {
                            flag11 = true;
                        }
                        if (flag11)
                        {
                            continue;
                        }
                        ComponentImprovement componentImprovement = null;
                        Empire actualEmpire = ActualEmpire;
                        componentImprovement = ((actualEmpire == null || actualEmpire.Research == null) ? new ComponentImprovement(Components[i]) : actualEmpire.Research.ResolveImprovedComponentValues(Components[i]));
                        switch (componentImprovement.ImprovedComponent.Category)
                        {
                            case ComponentCategoryType.AssaultPod:
                                {
                                    if (componentImprovement.ImprovedComponent.Type != ComponentType.AssaultPod)
                                    {
                                        break;
                                    }
                                    AssaultStrength += (short)componentImprovement.Value1;
                                    if (componentImprovement.Value2 > 0)
                                    {
                                        if (AssaultRange > 0)
                                        {
                                            AssaultRange = Math.Min(AssaultRange, (short)componentImprovement.Value2);
                                        }
                                        else
                                        {
                                            AssaultRange = (short)componentImprovement.Value2;
                                        }
                                    }
                                    if (componentImprovement.Value5 > 0)
                                    {
                                        if (AssaultShieldPenetration > 0)
                                        {
                                            AssaultShieldPenetration = Math.Max(AssaultShieldPenetration, (short)componentImprovement.Value5);
                                        }
                                        else
                                        {
                                            AssaultShieldPenetration = (short)componentImprovement.Value5;
                                        }
                                    }
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    break;
                                }
                            case ComponentCategoryType.Fighter:
                                if (componentImprovement.ImprovedComponent.Type == ComponentType.FighterBay)
                                {
                                    num33 += componentImprovement.Value1;
                                    num34 += componentImprovement.Value2;
                                }
                                break;
                            case ComponentCategoryType.Engine:
                                switch (componentImprovement.ImprovedComponent.Type)
                                {
                                    case ComponentType.EngineMainThrust:
                                        num14 += componentImprovement.Value1;
                                        num16 += componentImprovement.Value2;
                                        num15 += componentImprovement.Value3;
                                        num17 += componentImprovement.Value4;
                                        switch (componentImprovement.ImprovedComponent.SpecialImageIndex)
                                        {
                                            case 0:
                                                num35++;
                                                break;
                                            case 1:
                                                num36++;
                                                break;
                                            case 2:
                                                num37++;
                                                break;
                                            case 3:
                                                num38++;
                                                break;
                                            case 4:
                                                num39++;
                                                break;
                                            case 5:
                                                num40++;
                                                break;
                                        }
                                        break;
                                    case ComponentType.EngineVectoring:
                                        num18 += componentImprovement.Value1;
                                        num19 += componentImprovement.Value2;
                                        break;
                                }
                                break;
                            case ComponentCategoryType.HyperDrive:
                                if (componentImprovement.Value1 > num20)
                                {
                                    num20 = componentImprovement.Value1;
                                }
                                if (componentImprovement.Value2 > num21)
                                {
                                    num21 = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value3 < num22)
                                {
                                    num22 = (short)componentImprovement.Value3;
                                }
                                break;
                            case ComponentCategoryType.HyperDisrupt:
                                if (componentImprovement.ImprovedComponent.Type == ComponentType.HyperDeny)
                                {
                                    if (componentImprovement.Value2 > WeaponHyperDenyRange)
                                    {
                                        WeaponHyperDenyRange = componentImprovement.Value2;
                                    }
                                }
                                else if (componentImprovement.ImprovedComponent.Type == ComponentType.HyperStop && componentImprovement.Value2 > HyperStopRange)
                                {
                                    HyperStopRange = (short)componentImprovement.Value2;
                                }
                                break;
                            case ComponentCategoryType.ShieldRecharge:
                                if ((short)componentImprovement.Value1 > ShieldAreaRechargeRange)
                                {
                                    ShieldAreaRechargeRange = (short)componentImprovement.Value1;
                                    ShieldAreaRechargeCapacity = (short)componentImprovement.Value2;
                                    ShieldAreaRechargeEnergyRequired = (short)componentImprovement.Value3;
                                }
                                break;
                            case ComponentCategoryType.Shields:
                                num9 += componentImprovement.Value1;
                                num10 += (float)componentImprovement.Value2 / 10f;
                                break;
                            case ComponentCategoryType.Reactor:
                                ReactorPowerOutput += componentImprovement.Value1;
                                ReactorStorageCapacity += componentImprovement.Value2;
                                ReactorCycleFuelConsumption += componentImprovement.Value3;
                                FuelType = new Resource((byte)componentImprovement.Value4);
                                break;
                            case ComponentCategoryType.WeaponArea:
                            case ComponentCategoryType.WeaponSuperArea:
                                {
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    if (item.RawDamage > 0)
                                    {
                                        num5 += componentImprovement.Value1;
                                        if (componentImprovement.Value2 > StandoffWeaponsMaxRange)
                                        {
                                            StandoffWeaponsMaxRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > MaximumWeaponsRange)
                                        {
                                            MaximumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < MinimumWeaponsRange)
                                        {
                                            MinimumWeaponsRange = componentImprovement.Value2;
                                        }
                                    }
                                    if (item.BombardDamage > 0)
                                    {
                                        num6 += componentImprovement.Value7;
                                        if (componentImprovement.Value2 < num7)
                                        {
                                            num7 = componentImprovement.Value2;
                                        }
                                    }
                                    break;
                                }
                            case ComponentCategoryType.WeaponPointDefense:
                                {
                                    if (componentImprovement.Value1 > 0 && componentImprovement.Value2 > PointDefenseWeaponsRange)
                                    {
                                        PointDefenseWeaponsRange = componentImprovement.Value2;
                                    }
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    break;
                                }
                            case ComponentCategoryType.WeaponBeam:
                            case ComponentCategoryType.WeaponSuperBeam:
                                {
                                    if (componentImprovement.Value1 > 0)
                                    {
                                        num5 += componentImprovement.Value1;
                                        if (componentImprovement.Value2 < BeamWeaponsMinRange)
                                        {
                                            BeamWeaponsMinRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > MaximumWeaponsRange)
                                        {
                                            MaximumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < MinimumWeaponsRange)
                                        {
                                            MinimumWeaponsRange = componentImprovement.Value2;
                                        }
                                    }
                                    if (componentImprovement.Value7 > 0)
                                    {
                                        num6 += componentImprovement.Value7;
                                        if (componentImprovement.Value2 < num7)
                                        {
                                            num7 = componentImprovement.Value2;
                                        }
                                    }
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    if ((componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperBeam || componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperTorpedo || componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperMissile || componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperRailGun || componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperPhaser) && componentImprovement.ImprovedComponent.Value1 >= 10000)
                                    {
                                        PlanetDestroyerWeaponsRange = (int)((double)componentImprovement.Value2 * 0.9);
                                        if (Role == BuiltObjectRole.Military)
                                        {
                                            _IsPlanetDestroyer = true;
                                        }
                                    }
                                    break;
                                }
                            case ComponentCategoryType.WeaponTorpedo:
                            case ComponentCategoryType.WeaponSuperTorpedo:
                                {
                                    if (componentImprovement.Value1 > 0)
                                    {
                                        num5 += componentImprovement.Value1;
                                        if (componentImprovement.Value2 > StandoffWeaponsMaxRange)
                                        {
                                            StandoffWeaponsMaxRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > MaximumWeaponsRange)
                                        {
                                            MaximumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < MinimumWeaponsRange)
                                        {
                                            MinimumWeaponsRange = componentImprovement.Value2;
                                        }
                                    }
                                    if (componentImprovement.Value7 > 0)
                                    {
                                        num6 += componentImprovement.Value7;
                                        if (componentImprovement.Value2 < num7)
                                        {
                                            num7 = componentImprovement.Value2;
                                        }
                                    }
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    if ((componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperBeam || componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperTorpedo || componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperMissile || componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperRailGun || componentImprovement.ImprovedComponent.Type == ComponentType.WeaponSuperPhaser) && componentImprovement.ImprovedComponent.Value1 >= 10000)
                                    {
                                        PlanetDestroyerWeaponsRange = (int)((double)componentImprovement.Value2 * 0.9);
                                        if (Role == BuiltObjectRole.Military)
                                        {
                                            _IsPlanetDestroyer = true;
                                        }
                                    }
                                    break;
                                }
                            case ComponentCategoryType.Labs:
                                flag6 = true;
                                switch (componentImprovement.ImprovedComponent.Type)
                                {
                                    case ComponentType.LabsEnergyLab:
                                        ResearchEnergy += componentImprovement.Value1;
                                        break;
                                    case ComponentType.LabsHighTechLab:
                                        ResearchHighTech += componentImprovement.Value1;
                                        break;
                                    case ComponentType.LabsWeaponsLab:
                                        ResearchWeapons += componentImprovement.Value1;
                                        break;
                                }
                                break;
                            case ComponentCategoryType.Extractor:
                                flag8 = true;
                                switch (componentImprovement.ImprovedComponent.Type)
                                {
                                    case ComponentType.ExtractorGasExtractor:
                                        ExtractionGas += (short)componentImprovement.Value1;
                                        break;
                                    case ComponentType.ExtractorMine:
                                        ExtractionMine += (short)componentImprovement.Value1;
                                        break;
                                    case ComponentType.ExtractorLuxury:
                                        ExtractionLuxury += (short)componentImprovement.Value1;
                                        break;
                                }
                                break;
                            case ComponentCategoryType.Manufacturer:
                                flag9 = true;
                                _ = componentImprovement.ImprovedComponent.Type;
                                break;
                            case ComponentCategoryType.EnergyCollector:
                                flag10 = true;
                                switch (componentImprovement.ImprovedComponent.Type)
                                {
                                    case ComponentType.EnergyCollector:
                                        EnergyCollection += componentImprovement.Value1;
                                        break;
                                    case ComponentType.EnergyToFuel:
                                        {
                                            int val = EnergyToFuelRate + componentImprovement.Value1;
                                            EnergyToFuelRate = (short)Math.Min(32767, val);
                                            break;
                                        }
                                }
                                break;
                            case ComponentCategoryType.Armor:
                                {
                                    ComponentType type = componentImprovement.ImprovedComponent.Type;
                                    if (type == ComponentType.Armor)
                                    {
                                        Armor += (short)componentImprovement.Value1;
                                        if (componentImprovement.Value2 > ArmorReactive)
                                        {
                                            ArmorReactive = (short)componentImprovement.Value2;
                                        }
                                    }
                                    break;
                                }
                            case ComponentCategoryType.Construction:
                                {
                                    ComponentType type = componentImprovement.ImprovedComponent.Type;
                                    if (type != ComponentType.ConstructionBuild && type == ComponentType.DamageControl)
                                    {
                                        if (componentImprovement.Value1 > num26)
                                        {
                                            num26 = componentImprovement.Value1;
                                        }
                                        if (componentImprovement.Value2 > 0 && (num31 == 0 || componentImprovement.Value2 < num31))
                                        {
                                            num31 = (short)componentImprovement.Value2;
                                        }
                                    }
                                    break;
                                }
                            case ComponentCategoryType.Computer:
                                switch (componentImprovement.ImprovedComponent.Type)
                                {
                                    case ComponentType.ComputerTargettingFleet:
                                        if (componentImprovement.Value1 > TargettingModifier)
                                        {
                                            TargettingModifier = (short)componentImprovement.Value1;
                                        }
                                        if (componentImprovement.Value2 > FleetTargettingModifier)
                                        {
                                            FleetTargettingModifier = (short)componentImprovement.Value2;
                                        }
                                        break;
                                    case ComponentType.ComputerTargetting:
                                        if (componentImprovement.Value1 > TargettingModifier)
                                        {
                                            TargettingModifier = (short)componentImprovement.Value1;
                                        }
                                        break;
                                    case ComponentType.ComputerCountermeasuresFleet:
                                        if (componentImprovement.Value1 > CountermeasureModifier)
                                        {
                                            CountermeasureModifier = (short)componentImprovement.Value1;
                                        }
                                        if (componentImprovement.Value2 > FleetCountermeasureModifier)
                                        {
                                            FleetCountermeasureModifier = (short)componentImprovement.Value2;
                                        }
                                        break;
                                    case ComponentType.ComputerCountermeasures:
                                        if (componentImprovement.Value1 > CountermeasureModifier)
                                        {
                                            CountermeasureModifier = (short)componentImprovement.Value1;
                                        }
                                        break;
                                    case ComponentType.ComputerCommandCenter:
                                        {
                                            float num43 = (float)componentImprovement.Value1 / 100f;
                                            if (num43 > MaintenanceSavings)
                                            {
                                                MaintenanceSavings = num43;
                                            }
                                            break;
                                        }
                                    case ComponentType.ComputerCommerceCenter:
                                        {
                                            float num42 = (float)componentImprovement.Value1 / 1000f;
                                            if (num42 > TradeBonuses)
                                            {
                                                TradeBonuses = num42;
                                            }
                                            break;
                                        }
                                }
                                break;
                            case ComponentCategoryType.Sensor:
                                switch (componentImprovement.ImprovedComponent.Type)
                                {
                                    case ComponentType.SensorStealth:
                                        if (componentImprovement.Value1 > num27)
                                        {
                                            num27 = componentImprovement.Value1;
                                        }
                                        break;
                                    case ComponentType.SensorProximityArray:
                                        if (componentImprovement.Value1 > SensorProximityArrayRange)
                                        {
                                            SensorProximityArrayRange = componentImprovement.Value1;
                                        }
                                        if ((byte)componentImprovement.Value2 > SensorJumpIntercept)
                                        {
                                            SensorJumpIntercept = (byte)componentImprovement.Value2;
                                        }
                                        break;
                                    case ComponentType.SensorResourceProfileSensor:
                                        if (componentImprovement.Value1 > SensorResourceProfileSensorRange)
                                        {
                                            SensorResourceProfileSensorRange = componentImprovement.Value1;
                                        }
                                        break;
                                    case ComponentType.SensorLongRange:
                                        if (componentImprovement.Value1 > SensorLongRange)
                                        {
                                            SensorLongRange = componentImprovement.Value1;
                                        }
                                        break;
                                    case ComponentType.SensorTraceScanner:
                                        if (componentImprovement.Value1 > SensorTraceScannerRange)
                                        {
                                            SensorTraceScannerRange = (short)componentImprovement.Value1;
                                            SensorTraceScannerPower = (short)componentImprovement.Value2;
                                        }
                                        break;
                                    case ComponentType.SensorScannerJammer:
                                        if (componentImprovement.Value2 > SensorTraceScannerJamming)
                                        {
                                            SensorTraceScannerJamming = (short)componentImprovement.Value1;
                                        }
                                        break;
                                }
                                break;
                            case ComponentCategoryType.Habitation:
                                switch (componentImprovement.ImprovedComponent.Type)
                                {
                                    case ComponentType.HabitationHabModule:
                                        num25 += componentImprovement.Value1;
                                        break;
                                    case ComponentType.HabitationLifeSupport:
                                        num24 += componentImprovement.Value1;
                                        break;
                                    case ComponentType.HabitationMedicalCenter:
                                        if (componentImprovement.Value1 > MedicalCapacity)
                                        {
                                            MedicalCapacity = componentImprovement.Value1;
                                        }
                                        break;
                                    case ComponentType.HabitationRecreationCenter:
                                        if (componentImprovement.Value1 > RecreationCapacity)
                                        {
                                            RecreationCapacity = componentImprovement.Value1;
                                        }
                                        break;
                                    case ComponentType.HabitationColonization:
                                        IsColony = true;
                                        break;
                                }
                                break;
                        }
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.WeaponTractorBeam:
                                {
                                    if (componentImprovement.Value1 > 0)
                                    {
                                        if (componentImprovement.Value2 > MaximumWeaponsRange)
                                        {
                                            MaximumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < MinimumWeaponsRange)
                                        {
                                            MinimumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > TractorBeamRange)
                                        {
                                            TractorBeamRange = (short)componentImprovement.Value2;
                                        }
                                    }
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    break;
                                }
                            case ComponentType.WeaponGravityBeam:
                                {
                                    if (componentImprovement.Value1 > 0)
                                    {
                                        num5 += componentImprovement.Value1;
                                        if (componentImprovement.Value2 < BeamWeaponsMinRange)
                                        {
                                            BeamWeaponsMinRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > MaximumWeaponsRange)
                                        {
                                            MaximumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < MinimumWeaponsRange)
                                        {
                                            MinimumWeaponsRange = componentImprovement.Value2;
                                        }
                                    }
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    break;
                                }
                            case ComponentType.WeaponAreaGravity:
                                {
                                    if (componentImprovement.Value1 > 0)
                                    {
                                        num5 += componentImprovement.Value1;
                                        if (componentImprovement.Value2 > StandoffWeaponsMaxRange)
                                        {
                                            StandoffWeaponsMaxRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > MaximumWeaponsRange)
                                        {
                                            MaximumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < MinimumWeaponsRange)
                                        {
                                            MinimumWeaponsRange = componentImprovement.Value2;
                                        }
                                    }
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    break;
                                }
                            case ComponentType.WeaponIonDefense:
                                if (componentImprovement.Value1 > IonDefense)
                                {
                                    IonDefense = componentImprovement.Value1;
                                }
                                break;
                            case ComponentType.WeaponIonPulse:
                                {
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    if (item.RawDamage > 0)
                                    {
                                        num8 += componentImprovement.Value1;
                                        num5 += componentImprovement.Value1;
                                        if (componentImprovement.Value2 > IonWeaponRange)
                                        {
                                            IonWeaponRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > StandoffWeaponsMaxRange)
                                        {
                                            StandoffWeaponsMaxRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > MaximumWeaponsRange)
                                        {
                                            MaximumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < MinimumWeaponsRange)
                                        {
                                            MinimumWeaponsRange = componentImprovement.Value2;
                                        }
                                    }
                                    break;
                                }
                            case ComponentType.WeaponIonCannon:
                                {
                                    if (componentImprovement.Value1 > 0)
                                    {
                                        num8 += componentImprovement.Value1;
                                        num5 += componentImprovement.Value1;
                                        if (componentImprovement.Value2 > IonWeaponRange)
                                        {
                                            IonWeaponRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < BeamWeaponsMinRange)
                                        {
                                            BeamWeaponsMinRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 > MaximumWeaponsRange)
                                        {
                                            MaximumWeaponsRange = componentImprovement.Value2;
                                        }
                                        if (componentImprovement.Value2 < MinimumWeaponsRange)
                                        {
                                            MinimumWeaponsRange = componentImprovement.Value2;
                                        }
                                    }
                                    Weapon item = new Weapon(componentImprovement);
                                    weaponList.Add(item);
                                    break;
                                }
                            case ComponentType.ConstructionBuild:
                                flag7 = true;
                                break;
                            case ComponentType.ComputerCommerceCenter:
                                flag = true;
                                break;
                            case ComponentType.StorageFuel:
                                num4 += componentImprovement.Value1;
                                break;
                            case ComponentType.StorageDockingBay:
                                flag2 = true;
                                break;
                            case ComponentType.StoragePassenger:
                                num2 += componentImprovement.Value1;
                                break;
                            case ComponentType.StorageCargo:
                                num += componentImprovement.Value1;
                                break;
                            case ComponentType.StorageTroop:
                                num3 += componentImprovement.Value1;
                                break;
                            case ComponentType.ComputerCommandCenter:
                                flag3 = true;
                                break;
                            case ComponentType.HabitationLifeSupport:
                                flag4 = true;
                                break;
                            case ComponentType.HabitationHabModule:
                                flag5 = true;
                                break;
                        }
                    }
                    else if (Components[i].Status == ComponentStatus.Unbuilt)
                    {
                        num29++;
                    }
                    else if (Components[i].Status == ComponentStatus.Damaged)
                    {
                        num30++;
                        switch (Components[i].Category)
                        {
                            case ComponentCategoryType.WeaponBeam:
                            case ComponentCategoryType.WeaponTorpedo:
                            case ComponentCategoryType.WeaponArea:
                            case ComponentCategoryType.WeaponPointDefense:
                            case ComponentCategoryType.WeaponIon:
                            case ComponentCategoryType.WeaponGravity:
                            case ComponentCategoryType.WeaponSuperBeam:
                            case ComponentCategoryType.WeaponSuperArea:
                            case ComponentCategoryType.WeaponSuperTorpedo:
                                num41++;
                                break;
                        }
                    }
                }
                if (Weapons == null && weaponList != null && weaponList.Count > 0)
                {
                    Weapons = new WeaponList();
                }
                if (!Weapons.QuickCompareEquivalent(weaponList))
                {
                    WeaponList weaponList2 = weaponList.DetermineWeaponsNotInSuppliedList(Weapons);
                    WeaponList weaponList3 = Weapons.DetermineWeaponsNotInSuppliedList(weaponList);
                    for (int j = 0; j < weaponList3.Count; j++)
                    {
                        Weapons.RemoveAndResetFirstMatchingWeaponById(weaponList3[j]);
                    }
                    for (int k = 0; k < weaponList2.Count; k++)
                    {
                        Weapons.Add(weaponList2[k]);
                    }
                }
                UndamagedComponentSize = num32;
                if (num2 > 0)
                {
                    PopulationCapacity = num2;
                    if (Population == null)
                    {
                        Population = new PopulationList();
                    }
                }
                else
                {
                    PopulationCapacity = 0;
                    Population = null;
                }
                if (num > 0)
                {
                    if (Cargo == null)
                    {
                        Cargo = new CargoList();
                    }
                }
                else
                {
                    Cargo = null;
                }
                if (num3 > 0)
                {
                    if (Troops == null)
                    {
                        Troops = new TroopList();
                    }
                }
                else
                {
                    if (Troops != null && Troops.Count > 0)
                    {
                        for (int l = 0; l < Troops.Count; l++)
                        {
                            if (Troops[l].Empire != null && Troops[l].Empire.Troops != null)
                            {
                                Troops[l].Empire.Troops.Remove(Troops[l]);
                            }
                            Troops[l].BuiltObject = null;
                            Troops[l].Colony = null;
                            Troops[l].Empire = null;
                        }
                        Troops.Clear();
                    }
                    Troops = null;
                }
                if (Armor > 0 && (SubRole == BuiltObjectSubRole.SmallSpacePort || SubRole == BuiltObjectSubRole.MediumSpacePort || SubRole == BuiltObjectSubRole.LargeSpacePort) && ParentHabitat != null && ParentHabitat.Population != null && ParentHabitat.Population.DominantRace != null)
                {
                    Race dominantRace = ParentHabitat.Population.DominantRace;
                    if (dominantRace.SpaceportArmorStrengthFactor != 0.0)
                    {
                        ArmorReinforcingFactor = (short)(100.0 * dominantRace.SpaceportArmorStrengthFactor);
                    }
                }
                FighterCapacity = num33;
                FighterRepairRate = num34;
                if (num33 > 0)
                {
                    if (Fighters == null)
                    {
                        Fighters = new FighterList();
                    }
                }
                else if (Fighters != null && Fighters.Count <= 0)
                {
                    Fighters = null;
                }
                else if (Fighters != null && Fighters.Count > 0)
                {
                    FighterList fighterList = new FighterList();
                    fighterList.AddRange(Fighters);
                    for (int m = 0; m < fighterList.Count; m++)
                    {
                        fighterList[m].CompleteTeardown(_Galaxy);
                    }
                }
                if (ParentHabitat != null && Role == BuiltObjectRole.Base && ParentHabitat.Population.Count > 0 && ParentHabitat.Empire == ActualEmpire)
                {
                    if (ParentHabitat.Cargo == null)
                    {
                        ParentHabitat.Cargo = new CargoList();
                    }
                    Cargo = ParentHabitat.Cargo;
                }
                if (ParentHabitat != null)
                {
                    if (Role == BuiltObjectRole.Base && ParentHabitat.Population.Count > 0 && ParentHabitat.Empire == ActualEmpire)
                    {
                        num = 536870911;
                    }
                    else if (Role == BuiltObjectRole.Base)
                    {
                        num *= 4;
                    }
                }
                else if (Role == BuiltObjectRole.Base)
                {
                    num *= 4;
                }
                _CargoCapacity = num;
                if (flag3 && flag4 && flag5)
                {
                    IsFunctional = true;
                }
                if (IsFunctional && CargoCapacity > 0 && flag2)
                {
                    IsRefuellingDepot = true;
                }
                if (IsRefuellingDepot && flag && CargoCapacity > 0)
                {
                    IsSpacePort = true;
                }
                if (IsFunctional && flag6)
                {
                    IsResearchLab = true;
                }
                if (IsFunctional && flag8)
                {
                    IsResourceExtractor = true;
                }
                if (IsFunctional && flag9)
                {
                    IsManufacturer = true;
                }
                if (IsFunctional && flag7 && flag9)
                {
                    IsShipYard = true;
                }
                if (IsFunctional && flag10)
                {
                    IsEnergyCollector = true;
                }
                if (IsResourceExtractor)
                {
                    _ = Role;
                    _ = 8;
                }
                if (num27 > 0)
                {
                    double num44 = (double)num27 / (double)num23;
                    Stealth = (float)Math.Min(1.0, 0.5 / num44);
                }
                if (num26 > 0)
                {
                    _DamageReduction = (float)num26 / 1000f;
                }
                ReviewWeaponsComponentValues();
                double num45 = (double)ReactorPowerOutput - (double)StaticEnergyConsumption;
                num13 = num20;
                num11 = (short)(num14 / Math.Max(1, num23));
                num12 = (short)(num15 / Math.Max(1, num23));
                double num46;
                double num47;
                double num48;
                if (ReactorPowerOutput > 0)
                {
                    num46 = (double)num21 / num45;
                    num47 = (double)num16 / num45;
                    num48 = (double)num17 / num45;
                }
                else
                {
                    num46 = 1000000.0;
                    num47 = 1000000.0;
                    num48 = 1000000.0;
                }
                if (num46 > 1.0)
                {
                    num13 = (int)((double)num13 / num46);
                    if (num20 > 0 && ReactorPowerOutput > 0)
                    {
                        num13 = Math.Max(num13, 600);
                    }
                    num21 = (int)((double)num21 / num46);
                }
                if (num47 > 1.0)
                {
                    num11 = (short)((double)num11 / num47);
                    num16 = (short)((double)num16 / num47);
                }
                if (num48 > 1.0)
                {
                    num12 = (short)((double)num12 / num48);
                    num17 = (short)((double)num17 / num48);
                }
                List<int> list = new List<int>();
                list.Add(num35);
                list.Add(num36);
                list.Add(num37);
                list.Add(num38);
                list.Add(num39);
                list.Add(num40);
                list.Sort();
                list.Reverse();
                if (list[0] == num35)
                {
                    EngineType = EngineType.Proton;
                }
                else if (list[0] == num36)
                {
                    EngineType = EngineType.Quantum;
                }
                else if (list[0] == num37)
                {
                    EngineType = EngineType.Acceleros;
                }
                else if (list[0] == num38)
                {
                    EngineType = EngineType.Vortex;
                }
                else if (list[0] == num39)
                {
                    EngineType = EngineType.StarBurner;
                }
                else if (list[0] == num40)
                {
                    EngineType = EngineType.TurboThruster;
                }
                else
                {
                    EngineType = EngineType.Proton;
                }
                Size = num23;
                TroopCapacity = num3;
                FuelCapacity = num4;
                FirepowerRaw = num5;
                IonWeaponPower = num8;
                BombardWeaponPower = num6;
                if (num7 < 10000)
                {
                    BombardRange = num7;
                }
                else
                {
                    BombardRange = 0;
                }
                ShieldsCapacity = num9;
                ShieldRechargeRate = num10;
                MaxPopulation = Math.Min(num25, num24);
                TopSpeed = num11;
                _TopSpeedBase = num11;
                TopSpeedFuelBurn = (short)num16;
                CruiseSpeed = num12;
                _CruiseSpeedBase = num12;
                CruiseSpeedFuelBurn = (short)num17;
                WarpSpeed = num13;
                WarpSpeedFuelBurn = num21;
                ImpulseSpeedFuelBurn = (short)(CruiseSpeedFuelBurn / 4);
                HyperjumpInitiate = Math.Min((short)15, num22);
                _DamageRepair = num31;
                TurnRate = 0.1f + (float)num18 * 2f / (float)num23;
                double num49 = (double)ReactorPowerOutput - (double)StaticEnergyConsumption;
                double num50 = num49 / (double)num16;
                if (num50 > 1.0)
                {
                    num50 = Math.Sqrt(Math.Sqrt(num50));
                    num50 = Math.Min(num50, 2.0);
                }
                AccelerationRate = ((float)num11 / 8f + 0.5f) * (float)num50;
                if (Role != BuiltObjectRole.Base)
                {
                    AccelerationRate = Math.Min(AccelerationRate, TopSpeed);
                    AccelerationRate = Math.Max(AccelerationRate, 1f);
                }
                _FuelHandicapped = false;
                if (CurrentSpeed > (float)TopSpeed || TargetSpeed > TopSpeed || PreferredSpeed > (float)TopSpeed)
                {
                    TargetSpeed = TopSpeed;
                    PreferredSpeed = TopSpeed;
                    UpdatePosition();
                }
                if (MinimumWeaponsRange >= 100000)
                {
                    MinimumWeaponsRange = 0;
                }
                if (BeamWeaponsMinRange >= 100000)
                {
                    BeamWeaponsMinRange = 0;
                }
                for (int n = 0; n < Components.Count; n++)
                {
                    BuiltObjectComponent builtObjectComponent = Components[n];
                    if (builtObjectComponent.Type != ComponentType.StorageDockingBay)
                    {
                        continue;
                    }
                    if (DockingBays == null)
                    {
                        DockingBays = new DockingBayList();
                    }
                    if (DockingBayWaitQueue == null)
                    {
                        DockingBayWaitQueue = new BuiltObjectList();
                    }
                    var dockingBay = DockingBays.GetObjIndexOf(builtObjectComponent);
                    if (builtObjectComponent.Status == ComponentStatus.Damaged || builtObjectComponent.Status == ComponentStatus.Unbuilt)
                    {
                        if (dockingBay == null)
                        {
                            continue;
                        }
                        if (dockingBay.DockedShip != null)
                        {
                            bool flag12 = false;
                            var otherBay = DockingBays.FirstOrDefault(x =>
                            {
                                var comp = Components.FindComponentByBuiltObjectComponentId(dockingBay.ParentBuiltObjectComponentId);
                                if (comp != null && comp.Status == ComponentStatus.Normal && x.DockedShip == null)
                                {
                                    x.DockedShip = dockingBay.DockedShip;
                                    flag12 = true;
                                    return true;
                                }
                                return false;
                            });
                            if (!flag12)
                            {
                                dockingBay.DockedShip.ClearPreviousMissionRequirements();
                            }
                            dockingBay.DockedShip = null;
                        }
                        DockingBays.Remove(dockingBay);
                    }
                    else if (dockingBay == null)
                    {
                        DockingBay item2 = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, builtObjectComponent.Value1 * BaconBuiltObject.CargoBayCapacityMultiplier(this));
                        DockingBays.Add(item2);
                    }
                }
                if (UnbuiltComponentCount != num29 || DamagedComponentCount != num30)
                {
                    OverlayChanged = true;
                }
                UnbuiltComponentCount = num29;
                DamagedComponentCount = num30;
                if (DamagedComponentCount == 0)
                {
                    StrandedMessageSent = false;
                }
                if (flag7)
                {
                    if (ConstructionQueue == null)
                    {
                        ConstructionQueue = new ConstructionQueue(this, _Galaxy);
                    }
                    if (!ConstructionQueue.Redefine(this))
                    {
                        ConstructionQueue = null;
                    }
                }
                else if (ConstructionQueue != null && !ConstructionQueue.Redefine(this))
                {
                    ConstructionQueue = null;
                }
                if (flag9)
                {
                    if (ManufacturingQueue == null)
                    {
                        _ManufacturingQueue = new ManufacturingQueue(this, _Galaxy);
                    }
                    if (!ManufacturingQueue.Redefine(this))
                    {
                        _ManufacturingQueue = null;
                    }
                }
                else if (ManufacturingQueue != null && !ManufacturingQueue.Redefine(this))
                {
                    _ManufacturingQueue = null;
                }
                AnnualSupportCost = (int)num28;
                Empire actualEmpire2 = ActualEmpire;
                if (actualEmpire2 == null)
                {
                    return;
                }
                if (IsManufacturer && !HasBeenDestroyed)
                {
                    if (!actualEmpire2.Manufacturers.Contains(this))
                    {
                        actualEmpire2.Manufacturers.Add(this);
                    }
                }
                else
                {
                    int num53 = Empire.Manufacturers.IndexOf(this);
                    if (num53 >= 0)
                    {
                        Empire.Manufacturers.Remove(this);
                    }
                }
                if (IsRefuellingDepot && !HasBeenDestroyed && DockingBays != null && DockingBays.Count > 0)
                {
                    if (!actualEmpire2.RefuellingDepots.Contains(this))
                    {
                        actualEmpire2.RefuellingDepots.Add(this);
                    }
                }
                else
                {
                    int num54 = actualEmpire2.RefuellingDepots.IndexOf(this);
                    if (num54 >= 0)
                    {
                        actualEmpire2.RefuellingDepots.Remove(this);
                    }
                }
                if (IsResourceExtractor && !HasBeenDestroyed)
                {
                    if (!actualEmpire2.ResourceExtractors.Contains(this))
                    {
                        actualEmpire2.ResourceExtractors.Add(this);
                    }
                    if (Role == BuiltObjectRole.Base && !actualEmpire2.MiningStations.Contains(this))
                    {
                        actualEmpire2.MiningStations.Add(this);
                    }
                }
                else
                {
                    int num55 = actualEmpire2.ResourceExtractors.IndexOf(this);
                    if (num55 >= 0)
                    {
                        actualEmpire2.ResourceExtractors.Remove(this);
                    }
                    if (Role == BuiltObjectRole.Base)
                    {
                        num55 = actualEmpire2.MiningStations.IndexOf(this);
                        if (num55 >= 0)
                        {
                            actualEmpire2.MiningStations.Remove(this);
                        }
                    }
                }
                if (IsSpacePort && !HasBeenDestroyed && DockingBays != null && DockingBays.Count > 0)
                {
                    if (ParentHabitat != null && (SubRole == BuiltObjectSubRole.SmallSpacePort || SubRole == BuiltObjectSubRole.MediumSpacePort || SubRole == BuiltObjectSubRole.LargeSpacePort) && !actualEmpire2.SpacePorts.Contains(this))
                    {
                        actualEmpire2.SpacePorts.Add(this);
                    }
                }
                else
                {
                    int num56 = actualEmpire2.SpacePorts.IndexOf(this);
                    if (num56 >= 0)
                    {
                        actualEmpire2.SpacePorts.Remove(this);
                    }
                }
                if (IsShipYard && !HasBeenDestroyed)
                {
                    if (!actualEmpire2.ConstructionYards.Contains(this))
                    {
                        actualEmpire2.ConstructionYards.Add(this);
                    }
                }
                else
                {
                    int num57 = actualEmpire2.ConstructionYards.IndexOf(this);
                    if (num57 >= 0)
                    {
                        actualEmpire2.ConstructionYards.Remove(this);
                    }
                }
                if (Role == BuiltObjectRole.Freight && !HasBeenDestroyed)
                {
                    if (!actualEmpire2.Freighters.Contains(this))
                    {
                        actualEmpire2.Freighters.Add(this);
                    }
                }
                else
                {
                    int num58 = actualEmpire2.Freighters.IndexOf(this);
                    if (num58 >= 0)
                    {
                        actualEmpire2.Freighters.Remove(this);
                    }
                }
                if (SubRole == BuiltObjectSubRole.ConstructionShip && !HasBeenDestroyed)
                {
                    if (!actualEmpire2.ConstructionShips.Contains(this))
                    {
                        actualEmpire2.ConstructionShips.Add(this);
                    }
                }
                else
                {
                    int num59 = actualEmpire2.ConstructionShips.IndexOf(this);
                    if (num59 >= 0)
                    {
                        actualEmpire2.ConstructionShips.Remove(this);
                    }
                }
                if (SensorLongRange > 0 && !HasBeenDestroyed)
                {
                    if (!actualEmpire2.LongRangeScanners.Contains(this))
                    {
                        actualEmpire2.LongRangeScanners.Add(this);
                        if (actualEmpire2 == _Galaxy.PlayerEmpire)
                        {
                            _Galaxy.OnRefreshView(new RefreshViewEventArgs(Xpos, Ypos, null, onlyGalaxyBackdrops: true));
                        }
                    }
                    else if (sensorLongRange != SensorLongRange && actualEmpire2 == _Galaxy.PlayerEmpire)
                    {
                        _Galaxy.OnRefreshView(new RefreshViewEventArgs(Xpos, Ypos, null, onlyGalaxyBackdrops: true));
                    }
                }
                else
                {
                    int num60 = actualEmpire2.LongRangeScanners.IndexOf(this);
                    if (num60 >= 0)
                    {
                        actualEmpire2.LongRangeScanners.Remove(this);
                        if (actualEmpire2 == _Galaxy.PlayerEmpire)
                        {
                            _Galaxy.OnRefreshView(new RefreshViewEventArgs(Xpos, Ypos, null, onlyGalaxyBackdrops: true));
                        }
                    }
                }
                if (SubRole == BuiltObjectSubRole.ResupplyShip && IsFunctional && DockingBays != null && DockingBays.Count > 0 && CargoCapacity > 0 && ExtractionGas > 0)
                {
                    if (!actualEmpire2.ResupplyShips.Contains(this))
                    {
                        actualEmpire2.ResupplyShips.Add(this);
                    }
                }
                else
                {
                    int num61 = actualEmpire2.ResupplyShips.IndexOf(this);
                    if (num61 >= 0)
                    {
                        actualEmpire2.ResupplyShips.Remove(this);
                    }
                }
                if (IsFunctional && RecreationCapacity > 0 && SubRole == BuiltObjectSubRole.ResortBase)
                {
                    if (!actualEmpire2.ResortBases.Contains(this))
                    {
                        actualEmpire2.ResortBases.Add(this);
                    }
                }
                else
                {
                    int num62 = actualEmpire2.ResortBases.IndexOf(this);
                    if (num62 >= 0)
                    {
                        actualEmpire2.ResortBases.Remove(this);
                    }
                }
                if ((ResearchEnergy > 0 || ResearchHighTech > 0 || ResearchWeapons > 0) && !HasBeenDestroyed)
                {
                    if (!actualEmpire2.ResearchFacilities.Contains(this))
                    {
                        actualEmpire2.ResearchFacilities.Add(this);
                    }
                }
                else
                {
                    int num63 = actualEmpire2.ResearchFacilities.IndexOf(this);
                    if (num63 >= 0)
                    {
                        actualEmpire2.ResearchFacilities.Remove(this);
                    }
                }
                if (IsPlanetDestroyer && IsFunctional && TopSpeed > 0 && WarpSpeed > 0 && !HasBeenDestroyed)
                {
                    if (!actualEmpire2.PlanetDestroyers.Contains(this))
                    {
                        actualEmpire2.PlanetDestroyers.Add(this);
                        return;
                    }
                }
                else
                {
                    int num64 = actualEmpire2.PlanetDestroyers.IndexOf(this);
                    if (num64 >= 0)
                    {
                        actualEmpire2.PlanetDestroyers.Remove(this);
                    }
                }
                BaconBuiltObject.ModMyShip(this);
            }
        }

        public void PerformFinancialTransaction(double amount, long date, bool incomeFromTax)
        {
            long num = date % (Galaxy.RealSecondsInGalacticYear * 1000);
            long num2 = date - num;
            if (DateOfLastIncome < num2)
            {
                if (CurrentYearsIncome < (double)(AnnualSupportCost * 2))
                {
                    ConsecutiveUnprofitableYears++;
                }
                CurrentYearsIncome = 0.0;
            }
            double num3 = 0.0;
            if (incomeFromTax)
            {
                if (Empire != null)
                {
                    num3 = amount * (double)TradeBonuses;
                    Empire.StateMoney += num3;
                }
            }
            else
            {
                num3 = amount;
            }
            CurrentYearsIncome += num3;
            DateOfLastIncome = date;
            if (CurrentYearsIncome >= (double)(AnnualSupportCost * 2))
            {
                ConsecutiveUnprofitableYears = 0;
            }
        }

        private void DoLocationEffects(double timePassed, DateTime time)
        {
            if (_ShipDamageAmountLocation > 0f)
            {
                double hitPower = (double)_ShipDamageAmountLocation * timePassed;
                InflictDamage(this, null, hitPower, time, _Galaxy, 0f, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
            }
            if (_ShipPullAmountLocation > 0f)
            {
                double num = (double)_ShipPullAmountLocation * timePassed;
                Xpos += Math.Cos(_ShipPullAngleLocation) * num;
                Ypos += Math.Sin(_ShipPullAngleLocation) * num;
                if (ParentHabitat != null)
                {
                    ParentOffsetX += Math.Cos(_ShipPullAngleLocation) * num;
                    ParentOffsetY += Math.Sin(_ShipPullAngleLocation) * num;
                }
                if (Role != BuiltObjectRole.Base && TopSpeed > 0)
                {
                    TargetHeading = _ShipPullAngleLocation + (float)Math.PI;
                    TargetSpeed = TopSpeed;
                }
            }
        }

        private void DoDeployment(double timePassed)
        {
            if (_DeployProgress > 0f)
            {
                _DeployProgress += (float)timePassed / 30f;
                if ((double)_DeployProgress >= 1.0)
                {
                    _IsDeployed = true;
                    _DeployProgress = 0f;
                }
            }
            else if (_DeployProgress < 0f)
            {
                if (IsDeployed)
                {
                    InitiateUndeploy();
                }
                _DeployProgress -= (float)timePassed / 30f;
                if ((double)_DeployProgress <= -1.0)
                {
                    _DeployProgress = 0f;
                }
            }
        }

        private void DoRepairs(double timePassed)
        {
            BaconBuiltObject.DoRepairs(this, timePassed);
        }

        private void PerformFleetTasks()
        {
            ShipGroup shipGroup = ShipGroup;
            if (shipGroup == null || Empire == null)
            {
                return;
            }
            BuiltObjectMission mission = Mission;
            if ((mission == null || mission.Type == BuiltObjectMissionType.Undefined) && BuiltAt == null && RetrofitDesign == null && (!IsAutoControlled || Troops == null || TroopCapacity <= 0 || TroopCapacityRemaining < 100 || this == shipGroup.LeadShip || !Empire.AssignLoadTroopsMission(this)) && shipGroup.LeadShip != null && shipGroup.LeadShip != this)
            {
                Point point = Point.Empty;
                BuiltObjectMission mission2 = shipGroup.Mission;
                BuiltObjectMission builtObjectMission = null;
                if (shipGroup.LeadShip != null)
                {
                    builtObjectMission = shipGroup.LeadShip.Mission;
                }
                if (mission2 != null && mission2.Type != 0)
                {
                    point = mission2.ResolveTargetCoordinates(mission2);
                }
                else if (builtObjectMission != null && builtObjectMission.Type != 0)
                {
                    point = builtObjectMission.ResolveTargetCoordinates(builtObjectMission);
                }
                if (point.IsEmpty && shipGroup.LeadShip != null)
                {
                    point = new Point((int)shipGroup.LeadShip.Xpos, (int)shipGroup.LeadShip.Ypos);
                }
                double num = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, point.X, point.Y);
                if (num > 25000000.0)
                {
                    double x = 0.0;
                    double y = 0.0;
                    _Galaxy.SelectRelativePoint(300.0, out x, out y);
                    AssignMission(BuiltObjectMissionType.Move, null, null, (double)point.X + x, (double)point.Y + y, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                }
            }
        }

        public bool CheckHyperjumpPending()
        {
            return BaconBuiltObject.CheckHyperjumpPending(this);
        }

        private void CheckFightersNeedUpgrading()
        {
            BaconBuiltObject.CheckFightersNeedUpgrading(this);
        }

        public long CalculateTimeToArrivalAtDestination()
        {
            long result = 0L;
            if (Mission != null && Mission.Type != 0)
            {
                Point point = Mission.ResolveTargetCoordinates(Mission);
                if (point.X > 0 && point.Y > 0)
                {
                    double num = _Galaxy.CalculateDistance(Xpos, Ypos, point.X, point.Y);
                    result = (long)(num / (double)WarpSpeed * 1000.0);
                }
            }
            return result;
        }

        private void CheckForUnownedCargo()
        {
            if (Cargo == null || Cargo.Count <= 0)
            {
                return;
            }
            Empire empire = Empire;
            if (empire == null)
            {
                return;
            }
            CargoList cargoList = new CargoList();
            CargoList cargoList2 = new CargoList();
            for (int i = 0; i < Cargo.Count; i++)
            {
                Cargo cargo = Cargo[i];
                if (cargo != null && cargo.EmpireId < 0)
                {
                    Cargo cargo2 = null;
                    if (cargo.CommodityIsComponent)
                    {
                        cargo2 = new Cargo(cargo.Component, cargo.Amount, empire, 0);
                    }
                    else if (cargo.CommodityIsResource)
                    {
                        cargo2 = new Cargo(cargo.Resource, cargo.Amount, empire, 0);
                    }
                    cargoList.Add(cargo2);
                    cargoList2.Add(cargo);
                }
            }
            for (int j = 0; j < cargoList2.Count; j++)
            {
                Cargo.Remove(cargoList2[j]);
            }
            foreach (Cargo item in cargoList)
            {
                Cargo.Add(item);
            }
        }

        public bool DoTasks(DateTime time, long starDate)
        {
            return DoTasks(time, starDate, inView: false);
        }

        public bool DoTasks(DateTime time, long starDate, bool inView)
        {
            InView = inView;
            _tempNow = time;
            if (_LastTouch == DateTime.MinValue)
            {
                _LastTouch = _tempNow;
            }
            if (_LastIntermediateTouch == DateTime.MinValue)
            {
                _LastIntermediateTouch = _tempNow.Subtract(Galaxy.IntermediateProcessingSpan);
            }
            if (_LastPeriodicTouch == DateTime.MinValue)
            {
                _LastPeriodicTouch = _tempNow.Subtract(Galaxy.PeriodicProcessingSpan);
            }
            if (_LastLongTouch == DateTime.MinValue)
            {
                _LastLongTouch = _tempNow.Subtract(Galaxy.LongProcessingSpan);
            }
            if (_tempNow.Subtract(_LastTouch).TotalMilliseconds < 0.0)
            {
                _LastTouch = _tempNow;
            }
            if (_tempNow.Subtract(_LastIntermediateTouch).TotalMilliseconds < 0.0)
            {
                _LastIntermediateTouch = _tempNow;
            }
            if (_tempNow.Subtract(_LastPeriodicTouch).TotalMilliseconds < 0.0)
            {
                _LastPeriodicTouch = _tempNow;
            }
            if (_tempNow.Subtract(_LastLongTouch).TotalMilliseconds < 0.0)
            {
                _LastLongTouch = _tempNow;
            }
            double num = (double)_tempNow.Subtract(_LastTouch).Ticks / 10000000.0;
            if (double.IsNaN(CurrentFuel))
            {
                CurrentFuel = 0.0;
            }
            if (CurrentFuel < 0.0)
            {
                CurrentFuel = 0.0;
            }
            DoDeployment(num);
            RechargeShields(num);
            GalaxyIndex galaxyIndex = _Galaxy.ResolveIndex(Xpos, Ypos);
            UpdateIndexesForMovement(galaxyIndex.X, galaxyIndex.Y, _Galaxy, performIndexCheck: false);
            if (_Threats == null)
            {
                _Threats = new BuiltObject[20];
                _ThreatLevels = new int[20];
            }
            if ((Attackers != null && Attackers.Count > 0 && Attackers.CheckForNonDestroyedObjects()) || (Mission != null && (Mission.Type == BuiltObjectMissionType.Attack || Mission.Type == BuiltObjectMissionType.Escape || Mission.Type == BuiltObjectMissionType.Bombard || Mission.Type == BuiltObjectMissionType.Capture || Mission.Type == BuiltObjectMissionType.Raid)))
            {
                InBattle = true;
            }
            else
            {
                InBattle = false;
            }
            _HyperjumpAboutToEnter = false;
            _HyperjumpJustExited = false;
            if (ScanHabitatIndex >= 0 && LastScanTime < starDate - 2000)
            {
                ScanHabitatIndex = -1;
                LastScanTime = 0L;
            }
            if (Mission == null || Mission.Type == BuiltObjectMissionType.Undefined)
            {
                if (CurrentSpeed > (float)Math.Max(TopSpeed, CruiseSpeed))
                {
                    CurrentSpeed = CruiseSpeed;
                    CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(time);
                }
                if (_ShipPullAmountLocation <= 0f)
                {
                    TargetSpeed = 0;
                    PreferredSpeed = 0f;
                }
            }
            double num2 = num;
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(num2 > 0.0, 50, ref iterationCount))
            {
                num2 = ExecuteCommands(_Galaxy, num2, time, starDate);
                if (num2 > num)
                {
                    num2 = 0.0;
                }
            }
            if (Empire != null)
            {
                HandleWeaponsFiring(num, time, _Galaxy);
                HandleAssaultPodMovement(num);
            }
            if (Fighters != null && Fighters.Count > 0)
            {
                for (int i = 0; i < Fighters.Count; i++)
                {
                    Fighters[i].DoTasks(_Galaxy, time, inView);
                }
            }
            if (Explosions.Count > 0)
            {
                DoExplosions(_Galaxy);
                if (HasBeenDestroyed)
                {
                    _ShipPullAmountLocation /= 2f;
                }
            }
            DoLocationEffects(num, time);
            if (AssaultOwnershipChangeCounter > 0)
            {
                double num3 = num * 1000.0;
                int val = (int)((double)AssaultOwnershipChangeCounter - num3);
                val = Math.Min(3000, Math.Max(0, val));
                AssaultOwnershipChangeCounter = (short)val;
            }
            if (_tempNow.Subtract(_LastIntermediateTouch) >= Galaxy.IntermediateProcessingSpan)
            {
                BaconBuiltObject.CheckNearTarget(this);
                if (Empire != null)
                {
                    ScanArea(_Galaxy);
                }
                if (InBattle && !CheckHyperjumpPending())
                {
                    LaunchAllFighters();
                }
                SetAttackRangeWhenNoMission();
                CheckShieldAreaRechargeReset(time);
                double timePassed = (double)_tempNow.Subtract(_LastIntermediateTouch).Ticks / 10000000.0;
                ApplyLocationEffects(timePassed, time);
                ReviewDisabledComponents(timePassed);
                ThreatEvaluation(_Galaxy, time);
                ModifyAttackRangeByTargetSpeed();
                ScanForNewOwner();
                ScanForLocations();
                if (Empire != null && InBattle)
                {
                    CheckForAttack(_Galaxy);
                }
                if (!CanHyperJump && WarpSpeed > 0 && !DetectHyperDeny(_Galaxy))
                {
                    CanHyperJump = true;
                }
                if (IsAutoControlled)
                {
                    CheckForRandomAttackTargets();
                }
                DoRepairs(timePassed);
                ProcessBoardingAssault(time, timePassed);
                _LastIntermediateTouch = _tempNow;
            }
            if (_tempNow.Subtract(_LastPeriodicTouch) >= Galaxy.PeriodicProcessingSpan)
            {
                double timePassed2 = (double)_tempNow.Subtract(_LastPeriodicTouch).Ticks / 10000000.0;
                CheckRepairMissionStillValid();
                CheckClearDocking();
                CheckWhetherStillBeingBuilt();
                if (Empire != null)
                {
                    FleeFromHopelessBattle();
                    ReviewFleetBonuses();
                    CheckNearbyBuiltObjectsForShieldAreaRecharge(time);
                    CheckFightersNeedUpgrading();
                    BuildNewFighters();
                    ManufactureRepairFighters(timePassed2);
                    IndustrialProcessing(timePassed2, _Galaxy, time);
                    ReviewRetrofitConstructionQueue(time, starDate);
                    CheckForRefuelling(useCachedRefuellingLocation: true);
                    if (IsAutoControlled)
                    {
                        CheckForRepairs();
                    }
                    CheckForFuelOrdering();
                    HealTroops(timePassed2);
                    PirateBaseDiscovery();
                    PerformFleetTasks();
                }
                _LastPeriodicTouch = _tempNow;
            }
            if (_tempNow.Subtract(_LastLongTouch) >= Galaxy.LongProcessingSpan)
            {
                BaconBuiltObject.HugeProcessingSpanActions(this);
                CheckForShipsNoLongerDocking();
                CheckForUnownedCargo();
                ReviewCaptainBonuses();
                CheckForRefuelling(useCachedRefuellingLocation: false);
                AnnualSupportCost = (int)(Design.CalculateCurrentPurchasePrice(_Galaxy) / Galaxy.ShipMarkupFactor) + 1;
                ReviewSystemVisibilityForPreWarpShip();
                double timePassed3 = (double)_tempNow.Subtract(_LastLongTouch).Ticks / 10000000.0;
                UpdateRaidCountdown(timePassed3);
                _Galaxy.CheckRemoveInvalidDockingShipsFromWaitQueue(this);
                CheckSelfDestruct();
                BaconBuiltObject.ResetAssaultPods(this);
                _LastLongTouch = _tempNow;
            }
            if (Empire != null)
            {
                DefendBase(time);
                DefendShipFromAttackers(time);
                FireAtAssaultPods(time, inView);
                BaconBuiltObject.InterceptMissiles(this, time, inView);
                FireAtNearbyFighters(time, inView);
                FireTractorBeamsAtInvadingTroopTransports(time, inView);
            }
            if (Empire != null)
            {
                double num4 = (double)StaticEnergyConsumption * num;
                if (ShipGroup != null)
                {
                    num4 /= ShipGroup.ShipEnergyUsageBonus;
                }
                num4 /= CaptainShipEnergyUsageBonus;
                CurrentEnergy -= num4;
                PerformEnergyCollection(num);
                RechargeReactors(num);
            }
            if (double.IsNaN(CurrentFuel))
            {
                CurrentFuel = 0.0;
            }
            if (CurrentFuel < 0.0)
            {
                CurrentFuel = 0.0;
            }
            if (float.IsNaN(TargetHeading))
            {
                TargetHeading = 0f;
            }
            if (float.IsNaN(Heading))
            {
                Heading = 0f;
            }
            _LastTouch = _tempNow;
            return true;
        }

        private void ReviewSystemVisibilityForPreWarpShip()
        {
            if (WarpSpeed > 0 || ActualEmpire == null)
            {
                return;
            }
            Habitat habitat = _Galaxy.FastFindNearestSystem(Xpos, Ypos);
            if (habitat != null)
            {
                double num = _Galaxy.CalculateDistance(Xpos, Ypos, habitat.Xpos, habitat.Ypos);
                if (num < (double)Galaxy.MaxSolarSystemSize + 500.0)
                {
                    NearestSystemStar = habitat;
                }
                else
                {
                    NearestSystemStar = null;
                }
            }
            ActualEmpire.ResolveSystemVisibility(this, excludeBuiltObject: false);
        }

        private void CheckWhetherStillBeingBuilt()
        {
            if (BuiltAt == null || !BuiltAt.HasBeenDestroyed)
            {
                return;
            }
            StellarObject builtAt = BuiltAt;
            if (builtAt.ConstructionQueue != null)
            {
                ConstructionQueue constructionQueue = builtAt.ConstructionQueue;
                for (int i = 0; i < constructionQueue.ConstructionYards.Count; i++)
                {
                    ConstructionYard constructionYard = constructionQueue.ConstructionYards[i];
                    if (constructionYard.ShipUnderConstruction != null && constructionYard.ShipUnderConstruction == this)
                    {
                        constructionYard.ShipUnderConstruction = null;
                    }
                }
                if (constructionQueue.ConstructionWaitQueue != null && constructionQueue.ConstructionWaitQueue.Contains(this))
                {
                    constructionQueue.ConstructionWaitQueue.Remove(this);
                }
            }
            BuiltAt = null;
        }

        private void CheckForShipsNoLongerDocking()
        {
            if (DockingBayWaitQueue != null && DockingBayWaitQueue.Count > 0)
            {
                BuiltObjectList builtObjectList = new BuiltObjectList();
                for (int i = 0; i < DockingBayWaitQueue.Count; i++)
                {
                    BuiltObject builtObject = DockingBayWaitQueue[i];
                    if (builtObject.ParentBuiltObject != this)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
                for (int j = 0; j < builtObjectList.Count; j++)
                {
                    DockingBayWaitQueue.Remove(builtObjectList[j]);
                }
            }
            if (DockingBays == null)
            {
                return;
            }
            for (int k = 0; k < DockingBays.Count; k++)
            {
                if (DockingBays[k].DockedShip != null && DockingBays[k].DockedShip.DockedAt != this)
                {
                    DockingBays[k].DockedShip = null;
                }
            }
        }

        private void ApplyLocationEffects(double timePassed, DateTime time)
        {
            GalaxyLocationList locations = null;
            bool flag = _Galaxy.DetermineGalaxyLocationsAtPoint(Xpos, Ypos, GalaxyLocationType.Undefined, ref locations);
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool hyperjumpDisabledLocation = false;
            double num = 0.0;
            bool flag6 = false;
            double num2 = 0.0;
            double num3 = 0.0;
            LocationEffects.Clear();
            if (flag && locations != null)
            {
                for (int i = 0; i < locations.Count; i++)
                {
                    GalaxyLocation galaxyLocation = locations[i];
                    switch (galaxyLocation.Effect)
                    {
                        case GalaxyLocationEffectType.LightningDamage:
                            LocationEffects.Add(GalaxyLocationEffectType.LightningDamage);
                            flag2 = true;
                            break;
                        case GalaxyLocationEffectType.MovementSlowed:
                            LocationEffects.Add(GalaxyLocationEffectType.MovementSlowed);
                            flag3 = true;
                            break;
                        case GalaxyLocationEffectType.ShieldReduction:
                            LocationEffects.Add(GalaxyLocationEffectType.ShieldReduction);
                            flag4 = true;
                            break;
                        case GalaxyLocationEffectType.HyperjumpDisabled:
                            LocationEffects.Add(GalaxyLocationEffectType.HyperjumpDisabled);
                            hyperjumpDisabledLocation = true;
                            break;
                        case GalaxyLocationEffectType.ShipDamage:
                            LocationEffects.Add(GalaxyLocationEffectType.ShipDamage);
                            flag5 = true;
                            num = galaxyLocation.EffectAmount;
                            break;
                        case GalaxyLocationEffectType.ShipPull:
                            {
                                LocationEffects.Add(GalaxyLocationEffectType.ShipPull);
                                flag6 = true;
                                double x = (double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0;
                                double y = (double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0;
                                double num4 = _Galaxy.CalculateDistance(Xpos, Ypos, x, y);
                                double num5 = (double)galaxyLocation.Width / 2.0 / num4;
                                num2 = galaxyLocation.EffectAmount * num5;
                                double num6 = Galaxy.DetermineAngle(Xpos, Ypos, x, y);
                                num3 = num6;
                                break;
                            }
                    }
                }
            }
            if (flag2 && CurrentSpeed <= (float)TopSpeed)
            {
                TimeSpan timeSpan = time.Subtract(_LastLocationEffectTouch);
                double num7 = Galaxy.Rnd.NextDouble() * timeSpan.TotalSeconds;
                if (num7 > 7.0)
                {
                    double num8 = 20.0 + Galaxy.Rnd.NextDouble() * 70.0;
                    if ((double)CurrentShields <= num8)
                    {
                        CurrentShields = 0f;
                        num8 = Galaxy.Rnd.NextDouble() * 5.0;
                    }
                    InflictDamage(this, null, num8, time, _Galaxy, 0f, allowRecursion: false, double.MinValue, allowArmorInvulnerability: true);
                    _LastLocationEffectTouch = time;
                }
            }
            if (flag5)
            {
                _ShipDamageAmountLocation = (float)num;
            }
            else
            {
                _ShipDamageAmountLocation = 0f;
            }
            if (flag6)
            {
                _ShipPullAmountLocation = (float)num2;
                _ShipPullAngleLocation = (float)num3;
            }
            else
            {
                _ShipPullAmountLocation = 0f;
                _ShipPullAngleLocation = 0f;
            }
            _HyperjumpDisabledLocation = hyperjumpDisabledLocation;
            if (flag3 && !_FuelHandicapped && CurrentSpeed < (float)WarpSpeed)
            {
                CruiseSpeed = (short)((double)_CruiseSpeedBase * 0.75);
                TopSpeed = (short)((double)_TopSpeedBase * 0.75);
            }
            else if (!flag3 && _MovementSlowedLocation)
            {
                CruiseSpeed = _CruiseSpeedBase;
                TopSpeed = _TopSpeedBase;
            }
            _MovementSlowedLocation = flag3;
            if (flag4)
            {
                double val = (3.0 + Galaxy.Rnd.NextDouble() * 0.5) * timePassed;
                val = Math.Min(CurrentShields, val);
                CurrentShields -= (float)val;
            }
            _ShieldsReducedLocation = flag4;
        }

        private void HealTroops(double timePassed)
        {
            if (Troops == null || Troops.Count <= 0 || MedicalCapacity <= 0)
            {
                return;
            }
            double num = (double)MedicalCapacity / 500.0 * timePassed;
            if (Empire != null && Empire.GovernmentAttributes != null)
            {
                num *= Empire.GovernmentAttributes.TroopRecruitment;
                if (Empire.Leader != null)
                {
                    num *= 1.0 + (double)Empire.Leader.TroopRecoveryRate / 100.0;
                }
            }
            if (Characters != null && Characters.Count > 0)
            {
                int highestSkillLevel = Characters.GetHighestSkillLevel(CharacterSkillType.TroopRecoveryRate);
                num *= 1.0 + (double)highestSkillLevel / 100.0;
            }
            foreach (Troop troop in Troops)
            {
                troop.Readiness += (float)num;
                if (troop.Readiness > 100f)
                {
                    troop.Readiness = 100f;
                }
            }
        }

        private void CheckForRepairs()
        {
            if (BuiltAt != null)
            {
                return;
            }
            if (Role == BuiltObjectRole.Base)
            {
                if (UnbuiltOrDamagedComponentCount > 0 && ParentHabitat != null && ParentHabitat.Population.TotalAmount > 0 && ParentHabitat.Empire == Empire && ParentHabitat.ConstructionQueue != null && !ParentHabitat.ConstructionQueue.ConstructionWaitQueue.Contains(this) && ParentHabitat.ConstructionQueue.AddBuiltObjectToConstruct(this))
                {
                    BuiltAt = ParentHabitat;
                }
            }
            else if (!RepairForNextMission && UnbuiltOrDamagedComponentCount > 0 && BuiltAt == null)
            {
                RepairForNextMission = true;
            }
        }

        private void FireWeaponsAtFighter(Galaxy galaxy, Fighter fighter, DateTime time, out bool allWeaponsAssigned)
        {
            allWeaponsAssigned = false;
            if (Weapons == null)
            {
                return;
            }
            double num = galaxy.CalculateDistance(Xpos, Ypos, fighter.Xpos, fighter.Ypos);
            int num2 = 0;
            int num3 = (int)((float)(fighter.Size + (int)fighter.CurrentShields + 1) * Empire.AttackOvermatchFactor);
            int num4 = 0;
            if (PointDefenseWeaponsRange > 0)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    if (num2 >= num3)
                    {
                        return;
                    }
                    Weapon weapon = Weapons[i];
                    if (weapon.Component == null || weapon.Component.Category != ComponentCategoryType.WeaponPointDefense || !weapon.IsAvailable(this, time))
                    {
                        continue;
                    }
                    num4++;
                    double num5 = weapon.Range;
                    if (ShipGroup != null)
                    {
                        num5 *= ShipGroup.WeaponsRangeBonus;
                    }
                    num5 *= CaptainWeaponsRangeBonus;
                    if (num5 >= num)
                    {
                        TimeSpan timeSpan = time.Subtract(weapon.LastFired);
                        double num6 = Galaxy.Rnd.NextDouble() * 300.0 - 150.0;
                        if (timeSpan.TotalMilliseconds >= (double)weapon.FireRate + num6)
                        {
                            double hitRangeChance = 0.0;
                            bool willHit = DetermineHitTarget(_Galaxy, weapon, fighter, num, out hitRangeChance);
                            weapon.Fire(_Galaxy, this, fighter, num, time, willHit, hitRangeChance);
                            num2 += weapon.RawDamage;
                            num4--;
                        }
                    }
                }
                if (num4 <= 0)
                {
                    allWeaponsAssigned = true;
                }
            }
            else
            {
                if (BeamWeaponsMinRange <= 0)
                {
                    return;
                }
                for (int j = 0; j < Weapons.Count; j++)
                {
                    if (num2 >= num3)
                    {
                        return;
                    }
                    Weapon weapon2 = Weapons[j];
                    if (weapon2.Component == null || weapon2.Component.Category != ComponentCategoryType.WeaponBeam || !weapon2.IsAvailable(this, time))
                    {
                        continue;
                    }
                    num4++;
                    double num7 = weapon2.Range;
                    if (ShipGroup != null)
                    {
                        num7 *= ShipGroup.WeaponsRangeBonus;
                    }
                    num7 *= CaptainWeaponsRangeBonus;
                    if (num7 >= num)
                    {
                        TimeSpan timeSpan2 = time.Subtract(weapon2.LastFired);
                        double num8 = Galaxy.Rnd.NextDouble() * 500.0 - 250.0;
                        if (timeSpan2.TotalMilliseconds >= (double)weapon2.FireRate + num8)
                        {
                            double hitRangeChance2 = 0.0;
                            bool willHit2 = DetermineHitTarget(_Galaxy, weapon2, fighter, num, out hitRangeChance2);
                            weapon2.Fire(_Galaxy, this, fighter, num, time, willHit2, hitRangeChance2);
                            num2 += weapon2.RawDamage;
                            num4--;
                        }
                    }
                }
                if (num4 <= 0)
                {
                    allWeaponsAssigned = true;
                }
            }
        }

        private void FireTractorBeamsAtInvadingTroopTransports(DateTime time, bool inView)
        {
            if (Role != BuiltObjectRole.Base || ParentHabitat == null || ParentHabitat.Empire != Empire || TractorBeamRange <= 0 || (SubRole != BuiltObjectSubRole.SmallSpacePort && SubRole != BuiltObjectSubRole.MediumSpacePort && SubRole != BuiltObjectSubRole.LargeSpacePort))
            {
                return;
            }
            if (_TractorBeamFiringCounter >= 32766)
            {
                _TractorBeamFiringCounter = 0;
            }
            _TractorBeamFiringCounter++;
            if ((inView && _TractorBeamFiringCounter % 10 != 0) || !(CurrentEnergy / (double)ReactorStorageCapacity > 0.2) || Threats.Length <= 0)
            {
                return;
            }
            BuiltObjectList builtObjectList = new BuiltObjectList();
            double num = TractorBeamRange * TractorBeamRange;
            double num2 = SensorTraceScannerRange * SensorTraceScannerRange;
            int ourLongRangeWeaponsDamage = 0;
            if (Weapons != null)
            {
                ourLongRangeWeaponsDamage = Weapons.CalculateRawDamageOfWeaponsAboveRange(TractorBeamRange);
            }
            BuiltObject builtObject = null;
            double num3 = double.MaxValue;
            for (int i = 0; i < Threats.Length; i++)
            {
                StellarObject stellarObject = Threats[i];
                if (stellarObject == null || !(stellarObject is BuiltObject))
                {
                    continue;
                }
                BuiltObject builtObject2 = (BuiltObject)stellarObject;
                if (builtObject2 == null || builtObject2.TroopCapacity <= 0 || !ShouldAttack(stellarObject, time, includeBoardingCheck: false))
                {
                    continue;
                }
                double num4 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject2.Xpos, builtObject2.Ypos);
                if (!(num4 < num) || builtObject2.ActualEmpire == ActualEmpire || DetermineTractorBeamShouldPullTarget(builtObject2, ourLongRangeWeaponsDamage, Math.Sqrt(num4)))
                {
                    continue;
                }
                bool flag = true;
                if (num4 < num2 && SensorTraceScannerPower > builtObject2.SensorTraceScannerJamming)
                {
                    flag = ((builtObject2.Troops != null && builtObject2.Troops.Count > 0) ? true : false);
                }
                if (flag)
                {
                    builtObjectList.Add(builtObject2);
                    if (num4 < num3)
                    {
                        builtObject = builtObject2;
                        num3 = num4;
                    }
                }
            }
            if (builtObject == null)
            {
                return;
            }
            double maximumPortion = 1.0;
            if (builtObjectList.Count > 0)
            {
                maximumPortion = 0.5;
            }
            FireWeaponTypeAtTarget(ComponentType.WeaponTractorBeam, Math.Sqrt(num3), builtObject, time, mayModifyDiplomacy: false, maximumPortion);
            if (builtObjectList.Count <= 0)
            {
                return;
            }
            maximumPortion = 0.5 / (double)builtObjectList.Count;
            for (int j = 0; j < builtObjectList.Count; j++)
            {
                BuiltObject builtObject3 = builtObjectList[j];
                if (builtObject3 != null)
                {
                    double distanceToTarget = _Galaxy.CalculateDistance(Xpos, Ypos, builtObject3.Xpos, builtObject3.Ypos);
                    FireWeaponTypeAtTarget(ComponentType.WeaponTractorBeam, distanceToTarget, builtObject3, time, mayModifyDiplomacy: false, maximumPortion);
                }
            }
        }

        private void FireAtNearbyFighters(DateTime time, bool inView)
        {
            if (PointDefenseWeaponsRange <= 0 && BeamWeaponsMinRange <= 0 && MaximumWeaponsRange <= 0)
            {
                return;
            }
            if (_FighterFiringCounter >= 32766)
            {
                _FighterFiringCounter = 0;
            }
            _FighterFiringCounter++;
            if ((inView && _FighterFiringCounter % 10 != 0) || !(CurrentEnergy / (double)ReactorStorageCapacity > 0.3) || Threats.Length <= 0)
            {
                return;
            }
            bool allWeaponsAssigned = false;
            bool flag = CheckConventionalWeaponsAvailableToFireAtPassingThreats(time);
            int num = PointDefenseWeaponsRange;
            if (num <= 0)
            {
                num = BeamWeaponsMinRange;
            }
            double num2 = num * num;
            for (int i = 0; i < Threats.Length; i++)
            {
                if (allWeaponsAssigned)
                {
                    break;
                }
                if (Threats[i] == null || !(Threats[i] is BuiltObject) || !ShouldAttack(Threats[i], time, includeBoardingCheck: false))
                {
                    continue;
                }
                BuiltObject builtObject = (BuiltObject)Threats[i];
                if (flag && builtObject != CurrentTarget)
                {
                    double num3 = _Galaxy.CalculateDistance(Xpos, Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num3 < (double)MaximumWeaponsRange && !Empire.CheckOurEmpireBoarding(builtObject, this))
                    {
                        bool mayModifyDiplomacy = true;
                        if (builtObject.Attackers != null)
                        {
                            for (int j = 0; j < builtObject.Attackers.Count; j++)
                            {
                                StellarObject stellarObject = builtObject.Attackers[j];
                                if (stellarObject != null && stellarObject.Empire == Empire)
                                {
                                    mayModifyDiplomacy = false;
                                    break;
                                }
                            }
                        }
                        FireWeaponsAtTarget(num3, builtObject, time, mayModifyDiplomacy);
                        flag = false;
                    }
                }
                if (builtObject.Fighters == null || builtObject.Fighters.Count <= 0)
                {
                    continue;
                }
                for (int k = 0; k < builtObject.Fighters.Count; k++)
                {
                    Fighter fighter = builtObject.Fighters[k];
                    if (!_Galaxy.CheckWithinDistancePotential(num, Xpos, Ypos, fighter.Xpos, fighter.Ypos))
                    {
                        continue;
                    }
                    double num4 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, fighter.Xpos, fighter.Ypos);
                    if (num4 <= num2)
                    {
                        bool flag2 = true;
                        if (fighter.Empire == Empire)
                        {
                            fighter.AbandonAttackTarget();
                            flag2 = false;
                        }
                        if (flag2)
                        {
                            FireWeaponsAtFighter(_Galaxy, fighter, time, out allWeaponsAssigned);
                        }
                        if (allWeaponsAssigned)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public bool CheckFightersAvailableForLaunch()
        {
            if (Fighters != null && Fighters.Count > 0)
            {
                for (int i = 0; i < Fighters.Count; i++)
                {
                    if (Fighters[i].OnboardCarrier && Fighters[i].Health >= 1f && Fighters[i].Specification.Type == FighterType.Interceptor)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckFightersNeedReturning()
        {
            if (Fighters != null && Fighters.Count > 0)
            {
                for (int i = 0; i < Fighters.Count; i++)
                {
                    if (!Fighters[i].OnboardCarrier && !Fighters[i].HasBeenDestroyed && Fighters[i].Specification.Type == FighterType.Interceptor)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckBombersAvailableForLaunch()
        {
            if (Fighters != null && Fighters.Count > 0)
            {
                for (int i = 0; i < Fighters.Count; i++)
                {
                    if (Fighters[i].OnboardCarrier && Fighters[i].Health >= 1f && Fighters[i].Specification.Type == FighterType.Bomber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckBombersNeedReturning()
        {
            if (Fighters != null && Fighters.Count > 0)
            {
                for (int i = 0; i < Fighters.Count; i++)
                {
                    if (!Fighters[i].OnboardCarrier && !Fighters[i].HasBeenDestroyed && Fighters[i].Specification.Type == FighterType.Bomber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckPirateRelationOk(Empire pirateEmpire)
        {
            bool result = true;
            if (PirateEmpireId > 0 && PirateEmpireId != pirateEmpire.EmpireId)
            {
                if (pirateEmpire != null && pirateEmpire.PirateEmpireBaseHabitat != null && pirateEmpire.PirateRelations != null)
                {
                    PirateRelation relationByOtherEmpireId = pirateEmpire.PirateRelations.GetRelationByOtherEmpireId(PirateEmpireId);
                    if (relationByOtherEmpireId != null && relationByOtherEmpireId.Type != PirateRelationType.Protection)
                    {
                        result = false;
                    }
                }
            }
            else if (Empire != null && pirateEmpire != null && pirateEmpire.PirateEmpireBaseHabitat != null && pirateEmpire.PirateRelations != null)
            {
                PirateRelation relationByOtherEmpire = pirateEmpire.PirateRelations.GetRelationByOtherEmpire(Empire);
                if (relationByOtherEmpire != null && relationByOtherEmpire.Type != PirateRelationType.Protection)
                {
                    result = false;
                }
            }
            return result;
        }

        private void DefendShipFromAttackers(DateTime time)
        {
            if (Role == BuiltObjectRole.Base || (SubRole == BuiltObjectSubRole.ResupplyShip && IsDeployed) || FirepowerRaw <= 0 || Attackers == null || Attackers.Count <= 0)
            {
                return;
            }
            bool allWeaponsAssigned = false;
            int num = PointDefenseWeaponsRange;
            if (num <= 0)
            {
                num = BeamWeaponsMinRange;
            }
            StellarObjectList stellarObjectList = new StellarObjectList();
            for (int i = 0; i < Attackers.Count; i++)
            {
                StellarObject stellarObject = Attackers[i];
                if (stellarObject == null)
                {
                    continue;
                }
                if (stellarObject.HasBeenDestroyed)
                {
                    if (Attackers.Contains(stellarObject))
                    {
                        Attackers.Remove(stellarObject);
                    }
                    stellarObjectList.Add(stellarObject);
                }
                else if (stellarObject is Fighter && !allWeaponsAssigned)
                {
                    if (!_Galaxy.CheckWithinDistancePotential(num, Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos))
                    {
                        continue;
                    }
                    double num2 = _Galaxy.CalculateDistance(Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos);
                    if (!(num2 <= (double)num))
                    {
                        continue;
                    }
                    bool flag = true;
                    Fighter fighter = (Fighter)stellarObject;
                    if (fighter.Empire == Empire)
                    {
                        fighter.AbandonAttackTarget();
                        flag = false;
                    }
                    if (flag)
                    {
                        if (ShipGroup != null && ShipGroup.BattleStats == null)
                        {
                            ShipGroup.BattleStats = new SpaceBattleStats();
                        }
                        FireWeaponsAtFighter(_Galaxy, fighter, time, out allWeaponsAssigned);
                    }
                }
                else
                {
                    if (stellarObject is Fighter || !_Galaxy.CheckWithinDistancePotential(MaximumWeaponsRange, Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos))
                    {
                        continue;
                    }
                    double num3 = _Galaxy.CalculateDistance(Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos);
                    if (!(num3 <= (double)MaximumWeaponsRange))
                    {
                        continue;
                    }
                    bool flag2 = true;
                    if (stellarObject is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)stellarObject;
                        BuiltObjectMission mission = Mission;
                        if (builtObject.Empire == Empire || (PirateEmpireId > 0 && builtObject.PirateEmpireId == PirateEmpireId))
                        {
                            builtObject.ClearPreviousMissionRequirements();
                            stellarObjectList.Add(builtObject);
                            flag2 = false;
                        }
                        else if ((CurrentTarget == builtObject && mission != null && (mission.Type == BuiltObjectMissionType.Capture || mission.Type == BuiltObjectMissionType.Raid)) || (builtObject.AssaultAttackValue > 0 && builtObject.AssaultAttackEmpireId == Empire.EmpireId))
                        {
                            flag2 = false;
                        }
                        else if (Empire != null)
                        {
                            if (Empire.CheckOurEmpireOverwhelmingBoarding(builtObject))
                            {
                                flag2 = false;
                            }
                            else if (builtObject.CurrentShields < (float)Math.Max(15, (int)AssaultShieldPenetration) && Empire != null && Empire.CheckOurEmpireBoarding(builtObject, this))
                            {
                                flag2 = false;
                            }
                        }
                    }
                    if (flag2)
                    {
                        if (ShipGroup != null && ShipGroup.BattleStats == null)
                        {
                            ShipGroup.BattleStats = new SpaceBattleStats();
                        }
                        FireWeaponsAtTarget(num3, stellarObject, time, mayModifyDiplomacy: false);
                    }
                }
            }
            for (int j = 0; j < stellarObjectList.Count; j++)
            {
                Attackers.Remove(stellarObjectList[j]);
            }
        }

        private void DefendBase(DateTime time)
        {
            bool hyperDenyActive = false;
            bool flag = false;
            if (Role != BuiltObjectRole.Base && (SubRole != BuiltObjectSubRole.ResupplyShip || !IsDeployed))
            {
                return;
            }
            if ((FirepowerRaw > 0 || FighterCapacity > 0) && _Threats != null && _Threats.Length > 0)
            {
                bool allWeaponsAssigned = false;
                int num = PointDefenseWeaponsRange;
                if (num <= 0)
                {
                    num = BeamWeaponsMinRange;
                }
                for (int i = 0; i < _Threats.Length; i++)
                {
                    StellarObject stellarObject = _Threats[i];
                    if (stellarObject == null)
                    {
                        continue;
                    }
                    if (stellarObject is Fighter && !allWeaponsAssigned)
                    {
                        if (stellarObject.ParentBuiltObject == null || !ShouldAttack(stellarObject.ParentBuiltObject, time, includeBoardingCheck: false))
                        {
                            continue;
                        }
                        flag = true;
                        if (_Galaxy.CheckWithinDistancePotential(num, Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos))
                        {
                            double num2 = _Galaxy.CalculateDistance(Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos);
                            if (num2 <= (double)num)
                            {
                                Fighter fighter = (Fighter)stellarObject;
                                FireWeaponsAtFighter(_Galaxy, fighter, time, out allWeaponsAssigned);
                            }
                        }
                    }
                    else
                    {
                        if (stellarObject is Fighter || !ShouldAttack(stellarObject, time))
                        {
                            continue;
                        }
                        hyperDenyActive = true;
                        flag = true;
                        int num3 = MaximumWeaponsRange;
                        BuiltObjectMissionType builtObjectMissionType = BuiltObjectMissionType.Attack;
                        BuiltObject builtObject = null;
                        if (stellarObject is BuiltObject)
                        {
                            builtObject = (BuiltObject)stellarObject;
                            builtObjectMissionType = Empire.DetermineDestroyOrCaptureTarget(this, builtObject, attackingAsGroup: false);
                            if (builtObjectMissionType == BuiltObjectMissionType.Capture)
                            {
                                num3 = Math.Max(num3, AssaultRange);
                            }
                        }
                        if (_Galaxy.CheckWithinDistancePotential(num3, Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos))
                        {
                            double num4 = _Galaxy.CalculateDistance(Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos);
                            if (num4 <= (double)num3)
                            {
                                if (builtObjectMissionType == BuiltObjectMissionType.Capture)
                                {
                                    if (builtObject.CurrentShields < (float)AssaultShieldPenetration)
                                    {
                                        int num5 = CalculateAvailableAssaultPodAttackStrength(time);
                                        if (num5 > 0)
                                        {
                                            int num6 = 0;
                                            if (CurrentShields < (float)(ShieldsCapacity / 2) && Attackers != null && Attackers.Count > 0)
                                            {
                                                num6 = Attackers.TotalMobileMilitaryFirepower();
                                            }
                                            if (num6 < FirepowerRaw / 2)
                                            {
                                                CheckLaunchAssaultPodsAtTarget(time, builtObject);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        FireWeaponsAtTarget(num4, stellarObject, time, mayModifyDiplomacy: false);
                                    }
                                }
                                else
                                {
                                    FireWeaponsAtTarget(num4, stellarObject, time, mayModifyDiplomacy: false);
                                }
                            }
                        }
                        if (!(stellarObject is BuiltObject) || allWeaponsAssigned)
                        {
                            continue;
                        }
                        builtObject = (BuiltObject)stellarObject;
                        if (builtObject.Fighters == null || builtObject.Fighters.Count <= 0)
                        {
                            continue;
                        }
                        for (int j = 0; j < builtObject.Fighters.Count; j++)
                        {
                            Fighter fighter2 = builtObject.Fighters[j];
                            if (!fighter2.OnboardCarrier && !fighter2.HasBeenDestroyed && _Galaxy.CheckWithinDistancePotential(num, Xpos, Ypos, fighter2.Xpos, fighter2.Ypos))
                            {
                                double num7 = _Galaxy.CalculateDistance(Xpos, Ypos, fighter2.Xpos, fighter2.Ypos);
                                if (num7 <= (double)num)
                                {
                                    FireWeaponsAtFighter(_Galaxy, fighter2, time, out allWeaponsAssigned);
                                }
                            }
                        }
                    }
                }
            }
            if (Attackers != null && Attackers.Count > 0)
            {
                StellarObjectList stellarObjectList = new StellarObjectList();
                for (int k = 0; k < Attackers.Count; k++)
                {
                    if (Attackers[k].HasBeenDestroyed || Attackers[k].Empire == Empire || (PirateEmpireId > 0 && Attackers[k] is BuiltObject && ((BuiltObject)Attackers[k]).PirateEmpireId == PirateEmpireId))
                    {
                        stellarObjectList.Add(Attackers[k]);
                    }
                }
                for (int l = 0; l < stellarObjectList.Count; l++)
                {
                    Attackers.Remove(stellarObjectList[l]);
                }
            }
            if (flag)
            {
                LaunchAllFighters();
            }
            HyperDenyActive = hyperDenyActive;
        }

        private void CheckForFuelOrdering()
        {
            if (Empire == null || Empire == _Galaxy.IndependentEmpire || Empire.PirateEmpireBaseHabitat != null || (SubRole != BuiltObjectSubRole.SmallSpacePort && SubRole != BuiltObjectSubRole.MediumSpacePort && SubRole != BuiltObjectSubRole.LargeSpacePort))
            {
                return;
            }
            int num = Galaxy.MinimumLevelForRefuellingPoint * 4;
            if (SubRole == BuiltObjectSubRole.LargeSpacePort)
            {
                num *= 3;
            }
            if (SubRole == BuiltObjectSubRole.MediumSpacePort)
            {
                num *= 2;
            }
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            if (ParentHabitat != null)
            {
                OrderList orders2 = _Galaxy.Orders.GetOrders(ParentHabitat);
                if (orders2.Count > 0)
                {
                    orders.AddRange(orders2);
                }
            }
            for (int i = 0; i < _Galaxy.ResourceSystem.FuelResources.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.FuelResources[i];
                if (resourceDefinition == null)
                {
                    continue;
                }
                int num2 = 0;
                Resource resource = new Resource(resourceDefinition.ResourceID);
                int num3 = -1;
                if (Cargo != null)
                {
                    num3 = Cargo.IndexOf(resource, Empire);
                }
                if (num3 >= 0)
                {
                    if (Cargo[num3].Available < num)
                    {
                        num2 = num + (num - Math.Max(0, Cargo[num3].Available));
                    }
                }
                else
                {
                    num2 = num * 2;
                }
                for (int num4 = orders.IndexOf(resourceDefinition.ResourceID, 0); num4 >= 0; num4 = orders.IndexOf(resourceDefinition.ResourceID, num4 + 1))
                {
                    num2 -= orders[num4].AmountRequested;
                }
                if (num2 > 0)
                {
                    num2 = Math.Max(200, num2);
                }
                if (num2 <= 0)
                {
                    continue;
                }
                resource = new Resource(resourceDefinition.ResourceID);
                double num5 = (double)num2 * _Galaxy.ResourceCurrentPrices[resource.ResourceID];
                if (num5 <= Empire.GetPrivateFunds())
                {
                    if (IsFunctional && IsSpacePort && DockingBays != null && DockingBays.Count > 0)
                    {
                        Empire.CreateOrder(this, resource, num2, isState: false, OrderType.Standard);
                    }
                    else if (ParentHabitat != null)
                    {
                        Empire.CreateOrder(ParentHabitat, resource, num2, isState: false, OrderType.Standard);
                    }
                }
            }
        }

        public void SetupRefuelling()
        {
            if (Role == BuiltObjectRole.Base)
            {
                if (Empire == null || Empire == _Galaxy.IndependentEmpire || _Galaxy.PirateEmpires.Contains(Empire))
                {
                    return;
                }
                if (FuelType != null)
                {
                    int num = 0;
                    OrderList orders = _Galaxy.Orders.GetOrders(this);
                    foreach (Order item in orders)
                    {
                        if (item.CommodityResource != null)
                        {
                            Resource commodityResource = item.CommodityResource;
                            if (commodityResource.ResourceID == FuelType.ResourceID)
                            {
                                num += item.AmountRequested;
                            }
                        }
                    }
                    int num2 = -1;
                    if (Cargo != null)
                    {
                        num2 = Cargo.IndexOf(FuelType, Empire);
                    }
                    if (num2 >= 0)
                    {
                        num += Cargo[num2].Available;
                    }
                    int num3 = (int)((double)FuelCapacity - CurrentFuel);
                    num3 -= num;
                    int val = 0;
                    if (CargoSpace >= Galaxy.MinimumLevelForRefuellingPoint)
                    {
                        val = Galaxy.MinimumLevelForRefuellingPoint;
                    }
                    if (num3 > 0)
                    {
                        num3 = Math.Max(num3, val);
                    }
                    if (num3 > 0)
                    {
                        double num4 = (double)num3 * _Galaxy.ResourceCurrentPrices[FuelType.ResourceID];
                        if (Owner == null)
                        {
                            if (num4 <= Empire.GetPrivateFunds())
                            {
                                Empire.CreateOrder(this, FuelType, num3, isState: false, OrderType.Standard);
                            }
                        }
                        else if (num4 <= Owner.GetPrivateFunds())
                        {
                            Empire.CreateOrder(this, FuelType, num3, isState: false, OrderType.Standard);
                        }
                    }
                }
            }
            else
            {
                StellarObject stellarObject = null;
                ResourceList fuelTypes = DetermineFuelRequired(setFuelLevelToZero: false);
                stellarObject = ((Role == BuiltObjectRole.Military) ? ((Empire == null) ? _Galaxy.FastFindNearestRefuellingPoint(Xpos, Ypos, fuelTypes, ActualEmpire, this, includeResupplyShips: true, null) : Empire.UltraFastFindNearestRefuellingLocation(Xpos, Ypos, fuelTypes, this, mustHaveActualSupply: true, includeResupplyShips: true)) : ((Empire == null) ? _Galaxy.FastFindNearestRefuellingPoint(Xpos, Ypos, fuelTypes, ActualEmpire, this) : Empire.UltraFastFindNearestRefuellingLocation(Xpos, Ypos, fuelTypes, this, mustHaveActualSupply: true)));
                if (stellarObject != null && CheckRefuelLocationRangeAcceptable(stellarObject))
                {
                    if (stellarObject is Habitat)
                    {
                        Habitat target = (Habitat)stellarObject;
                        AssignMission(BuiltObjectMissionType.Refuel, target, null, BuiltObjectMissionPriority.Unavailable);
                    }
                    else if (stellarObject is BuiltObject)
                    {
                        BuiltObject target2 = (BuiltObject)stellarObject;
                        AssignMission(BuiltObjectMissionType.Refuel, target2, null, BuiltObjectMissionPriority.Unavailable);
                    }
                }
            }
            RefuelForNextMission = false;
        }

        public bool CheckRefuelLocationRangeAcceptable(StellarObject refuellingLocation)
        {
            if (refuellingLocation != null)
            {
                if (WarpSpeed <= 0)
                {
                    double rangeFactor = 0.0;
                    if (!WithinFuelRange(refuellingLocation.Xpos, refuellingLocation.Ypos, 0.0, out rangeFactor) && rangeFactor > 1.2)
                    {
                        double num = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, refuellingLocation.Xpos, refuellingLocation.Ypos);
                        if (num > 2304000000.0)
                        {
                            return false;
                        }
                    }
                }
                else if (WarpSpeed < 2500)
                {
                    double rangeFactor2 = 0.0;
                    if (!WithinFuelRange(refuellingLocation.Xpos, refuellingLocation.Ypos, 0.0, out rangeFactor2) && rangeFactor2 > 2.0 && NearestSystemStar != null)
                    {
                        double num2 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, refuellingLocation.Xpos, refuellingLocation.Ypos);
                        if (num2 > 2304000000.0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public ResourceList DetermineFuelRequired()
        {
            return DetermineFuelRequired(setFuelLevelToZero: true);
        }

        public ResourceList DetermineFuelRequired(bool setFuelLevelToZero)
        {
            ResourceList resourceList = new ResourceList();
            int num = 1;
            if (!setFuelLevelToZero)
            {
                num = FuelCapacity - (int)CurrentFuel;
            }
            Resource fuelType = FuelType;
            if (fuelType != null)
            {
                Resource resource = new Resource(fuelType.ResourceID);
                resource.SortTag = num;
                resourceList.Add(resource);
            }
            return resourceList;
        }

        private bool CheckBaseCargoForFuel()
        {
            if (FuelType != null)
            {
                Cargo cargo = null;
                if (Cargo != null)
                {
                    cargo = Cargo.GetCargo(FuelType, Empire);
                }
                if (cargo != null && cargo.Available > 0)
                {
                    int num = (int)((double)FuelCapacity - CurrentFuel);
                    int available = cargo.Available;
                    if (num > available)
                    {
                        num = available;
                    }
                    CurrentFuel += num;
                    cargo.Amount -= num;
                    if (cargo.Amount <= 0)
                    {
                        Cargo.Remove(cargo);
                    }
                    return true;
                }
            }
            return false;
        }

        private void CheckForRefuelling(bool useCachedRefuellingLocation)
        {
            double num = 0.05;
            if (Role == BuiltObjectRole.Base)
            {
                num = 0.4;
            }
            else
            {
                switch (SubRole)
                {
                    case BuiltObjectSubRole.SmallFreighter:
                    case BuiltObjectSubRole.MediumFreighter:
                    case BuiltObjectSubRole.LargeFreighter:
                    case BuiltObjectSubRole.PassengerShip:
                    case BuiltObjectSubRole.GasMiningShip:
                    case BuiltObjectSubRole.MiningShip:
                        {
                            num = 0.3;
                            BuiltObjectMission mission = Mission;
                            if (mission == null || mission.Type == BuiltObjectMissionType.Undefined)
                            {
                                num = 0.6;
                            }
                            break;
                        }
                    default:
                        {
                            StellarObject refuellingLocation = null;
                            if (useCachedRefuellingLocation && _RefuellingLocation != null && !_RefuellingLocation.HasBeenDestroyed)
                            {
                                refuellingLocation = _RefuellingLocation;
                                num = CalculateFuelPortionMarginFromNearbyRefuellingPoints(Xpos, Ypos, refuellingLocation);
                            }
                            else
                            {
                                num = CalculateRefuellingPortion(out refuellingLocation);
                                _RefuellingLocation = refuellingLocation;
                            }
                            if ((Mission == null || Mission.Type == BuiltObjectMissionType.Undefined) && refuellingLocation != null)
                            {
                                double num2 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, refuellingLocation.Xpos, refuellingLocation.Ypos);
                                double num3 = 9000000.0;
                                if (WarpSpeed > 0)
                                {
                                    num3 = 2304000000.0;
                                }
                                if (num2 < num3)
                                {
                                    num = Math.Max(0.6, num);
                                }
                            }
                            break;
                        }
                }
            }
            _ = ShipGroup;
            if (Mission != null && (Mission.Type == BuiltObjectMissionType.Attack || Mission.Type == BuiltObjectMissionType.Bombard))
            {
                num *= 0.9;
            }
            int num4 = (int)((double)FuelCapacity * num);
            switch (SubRole)
            {
                case BuiltObjectSubRole.ResupplyShip:
                    if ((IsDeployed || DeployProgress != 0.0) && IsResourceExtractor && ParentHabitat != null && FuelType != null && ParentHabitat.Resources.IndexOf(FuelType.ResourceID, 0) >= 0)
                    {
                        num4 = 0;
                    }
                    else if (Mission != null && Mission.Type == BuiltObjectMissionType.Deploy && Mission.TargetHabitat != null && FuelType != null && Mission.TargetHabitat.Resources.IndexOf(FuelType.ResourceID, 0) >= 0)
                    {
                        num4 = 0;
                    }
                    break;
            }
            if (Role != BuiltObjectRole.Base && WarpSpeed <= 0)
            {
                num = Math.Max(num, 0.5);
            }
            int num5 = (int)((double)FuelCapacity * 0.9);
            if ((!(CurrentFuel <= (double)num4) && (SubRole != BuiltObjectSubRole.ResupplyShip || !(CurrentFuel < (double)num5) || !IsDeployed || CheckBaseCargoForFuel())) || DockedAt != null || BuiltAt != null)
            {
                return;
            }
            if (Role == BuiltObjectRole.Base || SubRole == BuiltObjectSubRole.ResupplyShip)
            {
                if (!CheckBaseCargoForFuel() && SubRole != BuiltObjectSubRole.SmallSpacePort && SubRole != BuiltObjectSubRole.MediumSpacePort && SubRole != BuiltObjectSubRole.LargeSpacePort && IsAutoControlled && (Mission == null || (Mission.Type != BuiltObjectMissionType.Escape && Mission.Type != BuiltObjectMissionType.Refuel)))
                {
                    SetupRefuelling();
                }
            }
            else if (Mission != null)
            {
                switch (Mission.Type)
                {
                    case BuiltObjectMissionType.Refuel:
                        break;
                    case BuiltObjectMissionType.Undefined:
                    case BuiltObjectMissionType.Explore:
                    case BuiltObjectMissionType.Patrol:
                    case BuiltObjectMissionType.Escort:
                    case BuiltObjectMissionType.Blockade:
                    case BuiltObjectMissionType.Attack:
                        if (IsAutoControlled)
                        {
                            SetupRefuelling();
                        }
                        else if (!AutoRefuelRepairShip(useCachedRefuellingLocation))
                        {
                            if (!RefuelForNextMission && Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null)
                            {
                                string description3 = string.Format(TextResolver.GetText("SHIPTYPE NAME requires refuelling"), Galaxy.ResolveDescription(SubRole), Name);
                                Empire.SendMessageToEmpire(Empire, EmpireMessageType.ShipNeedsRefuelling, this, description3);
                            }
                            RefuelForNextMission = true;
                        }
                        break;
                    case BuiltObjectMissionType.MoveAndWait:
                        if (!(CurrentSpeed <= 0f))
                        {
                            break;
                        }
                        if (IsAutoControlled)
                        {
                            SetupRefuelling();
                        }
                        else if (!AutoRefuelRepairShip(useCachedRefuellingLocation))
                        {
                            if (!RefuelForNextMission && Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null)
                            {
                                string description2 = string.Format(TextResolver.GetText("SHIPTYPE NAME requires refuelling"), Galaxy.ResolveDescription(SubRole), Name);
                                Empire.SendMessageToEmpire(Empire, EmpireMessageType.ShipNeedsRefuelling, this, description2);
                            }
                            RefuelForNextMission = true;
                        }
                        break;
                    default:
                        if (!RefuelForNextMission && !IsAutoControlled && Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null)
                        {
                            string description = string.Format(TextResolver.GetText("SHIPTYPE NAME requires refuelling"), Galaxy.ResolveDescription(SubRole), Name);
                            Empire.SendMessageToEmpire(Empire, EmpireMessageType.ShipNeedsRefuelling, this, description);
                        }
                        RefuelForNextMission = true;
                        break;
                }
            }
            else if (IsAutoControlled)
            {
                SetupRefuelling();
            }
            else
            {
                if (!RefuelForNextMission && Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null)
                {
                    string description4 = string.Format(TextResolver.GetText("SHIPTYPE NAME requires refuelling"), Galaxy.ResolveDescription(SubRole), Name);
                    Empire.SendMessageToEmpire(Empire, EmpireMessageType.ShipNeedsRefuelling, this, description4);
                }
                RefuelForNextMission = true;
            }
        }


    }
}
