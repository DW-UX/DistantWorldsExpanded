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
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class BuiltObject : StellarObject, IComparable, IComparable<StellarObject>, IComparable<BuiltObject>, IComparable<Habitat>, IComparable<Creature>, ISerializable
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
                    Weapons[i].ReviewValues(Empire);
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
                int num51 = DockingBays.IndexOf(builtObjectComponent);
                if (builtObjectComponent.Status == ComponentStatus.Damaged || builtObjectComponent.Status == ComponentStatus.Unbuilt)
                {
                    if (num51 < 0)
                    {
                        continue;
                    }
                    if (DockingBays[num51].DockedShip != null)
                    {
                        bool flag12 = false;
                        for (int num52 = 0; num52 < DockingBays.Count; num52++)
                        {
                            DockingBay dockingBay = DockingBays[num52];
                            BuiltObjectComponent builtObjectComponent2 = Components.FindComponentByBuiltObjectComponentId(dockingBay.ParentBuiltObjectComponentId);
                            if (builtObjectComponent2 != null && builtObjectComponent2.Status == ComponentStatus.Normal && dockingBay.DockedShip == null)
                            {
                                dockingBay.DockedShip = DockingBays[num51].DockedShip;
                                flag12 = true;
                                break;
                            }
                        }
                        if (!flag12)
                        {
                            DockingBays[num51].DockedShip.ClearPreviousMissionRequirements();
                        }
                        DockingBays[num51].DockedShip = null;
                    }
                    DockingBays.RemoveAt(num51);
                }
                else if (num51 < 0)
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
                    Empire.Manufacturers.RemoveAt(num53);
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
                    actualEmpire2.RefuellingDepots.RemoveAt(num54);
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
                    actualEmpire2.ResourceExtractors.RemoveAt(num55);
                }
                if (Role == BuiltObjectRole.Base)
                {
                    num55 = actualEmpire2.MiningStations.IndexOf(this);
                    if (num55 >= 0)
                    {
                        actualEmpire2.MiningStations.RemoveAt(num55);
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
                    actualEmpire2.SpacePorts.RemoveAt(num56);
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
                    actualEmpire2.ConstructionYards.RemoveAt(num57);
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
                    actualEmpire2.Freighters.RemoveAt(num58);
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
                    actualEmpire2.ConstructionShips.RemoveAt(num59);
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
                    actualEmpire2.LongRangeScanners.RemoveAt(num60);
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
                    actualEmpire2.ResupplyShips.RemoveAt(num61);
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
                    actualEmpire2.ResortBases.RemoveAt(num62);
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
                    actualEmpire2.ResearchFacilities.RemoveAt(num63);
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
                    actualEmpire2.PlanetDestroyers.RemoveAt(num64);
                }
            }
            BaconBuiltObject.ModMyShip(this);
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

        private void DoExplosions(Galaxy galaxy)
        {
            ExplosionList explosionList = new ExplosionList();
            for (int i = 0; i < Explosions.Count; i++)
            {
                Explosion explosion = Explosions[i];
                double num = (double)_tempNow.Subtract(explosion.ExplosionStart).Ticks / 10000000.0;
                explosion.ExplosionProgression = (float)Math.Max(0.0, num * 60.0);
                double num2 = Math.Min(100.0, Math.Max(50.0, explosion.ExplosionSize / 2));
                explosion.ExplosionCurrentImage = Math.Min((short)(Galaxy.ExplosionImageCount - 1), (short)((double)explosion.ExplosionProgression / num2 * (double)Galaxy.ExplosionImageCount));
                if ((double)explosion.ExplosionProgression > num2)
                {
                    explosion.ExplosionSize = 0;
                    explosion.ExplosionProgression = 0f;
                    explosion.ExplosionSoundPlayed = false;
                    explosionList.Add(explosion);
                    if (explosion.ExplosionWillDestroy)
                    {
                        CompleteTeardown(galaxy);
                    }
                }
            }
            foreach (Explosion item in explosionList)
            {
                Explosions.Remove(item);
            }
        }

        public void LeaveShipGroup()
        {
            if (ShipGroup != null)
            {
                ShipGroup shipGroup = ShipGroup;
                bool flag = false;
                if (ShipGroup.LeadShip == this)
                {
                    flag = true;
                }
                ShipGroup.Ships.Remove(this);
                ShipGroup = null;
                if (flag)
                {
                    shipGroup.Update();
                }
                if (shipGroup.Ships.Count <= 0)
                {
                    Empire.DisbandShipGroup(shipGroup);
                }
            }
        }

        private void ScanForNewOwner()
        {
            ScanForNewOwner(null);
        }

        public void ScanForNewOwner(BuiltObject preferredDiscoverer)
        {
            if (Empire != null || DamagedComponentCount != 0 || UnbuiltComponentCount != 0 || _Threats == null)
            {
                return;
            }
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < _Threats.Length; i++)
            {
                if (!(_Threats[i] is BuiltObject))
                {
                    continue;
                }
                if (Empire != null)
                {
                    break;
                }
                BuiltObject builtObject = (BuiltObject)_Threats[i];
                if (builtObject == null || builtObject.Empire == null || builtObject.Empire == _Galaxy.IndependentEmpire || (builtObject.WarpSpeed > 0 && !(builtObject.CurrentSpeed < (float)builtObject.WarpSpeed)))
                {
                    continue;
                }
                double num = _Galaxy.CalculateDistance(Xpos, Ypos, builtObject.Xpos, builtObject.Ypos);
                if (num < 500.0)
                {
                    bool flag = true;
                    if (_Galaxy.StoryClueLocations.Contains(this) && builtObject.Empire != _Galaxy.PlayerEmpire)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            builtObjectList = _Galaxy.SortBuiltObjectsByDistance(Xpos, Ypos, builtObjectList);
            if (builtObjectList != null)
            {
                if (BuiltAt != null)
                {
                    int num2 = -1;
                    for (int j = 0; j < builtObjectList.Count; j++)
                    {
                        if (builtObjectList[j] != null && builtObjectList[j] == BuiltAt)
                        {
                            num2 = j;
                            break;
                        }
                    }
                    if (num2 >= 0)
                    {
                        BuiltObject builtObject2 = builtObjectList[num2];
                        if (builtObject2 != null)
                        {
                            builtObjectList.RemoveAt(num2);
                            builtObjectList.Insert(0, builtObject2);
                        }
                    }
                }
                if (preferredDiscoverer != null)
                {
                    builtObjectList.Insert(0, preferredDiscoverer);
                }
            }
            if (builtObjectList == null)
            {
                return;
            }
            for (int k = 0; k < builtObjectList.Count; k++)
            {
                if (Empire != null)
                {
                    break;
                }
                BuiltObject builtObject3 = builtObjectList[k];
                BuiltObjectEncounterAction builtObjectEncounterAction = BuiltObjectEncounterAction.Notify;
                if (builtObject3.Empire == _Galaxy.PlayerEmpire)
                {
                    builtObjectEncounterAction = PlayerEmpireEncounterAction;
                    if (builtObjectEncounterAction != BuiltObjectEncounterAction.Notify)
                    {
                        PlayerEmpireEncounterAction = BuiltObjectEncounterAction.None;
                    }
                }
                switch (builtObjectEncounterAction)
                {
                    case BuiltObjectEncounterAction.Prompt:
                        {
                            if (builtObject3.Empire == _Galaxy.PlayerEmpire && _Galaxy.PlayerEmpire.DiscoveryActionAbandonedShipBase > 0)
                            {
                                _Galaxy.InvestigateAbandonedBuiltObject(builtObject3.Empire, this);
                                break;
                            }
                            bool flag2 = false;
                            if (Name.Contains(TextResolver.GetText("Refugee")))
                            {
                                flag2 = true;
                            }
                            string empty = string.Empty;
                            string arg = string.Empty;
                            if (builtObject3.NearestSystemStar != null)
                            {
                                arg = builtObject3.NearestSystemStar.Name;
                            }
                            empty = ((!flag2) ? (empty + string.Format(TextResolver.GetText("We have encountered an abandoned SHIPTYPE"), Galaxy.ResolveDescription(SubRole), arg)) : (empty + string.Format(TextResolver.GetText("We have encountered a refugee SHIPTYPE"), Galaxy.ResolveDescription(SubRole), arg)));
                            empty += ".\n\n";
                            if (!string.IsNullOrEmpty(EncounterDescription))
                            {
                                empty += EncounterDescription;
                                empty += "\n\n";
                            }
                            string text = TextResolver.GetText("Abandoned Ship Encountered");
                            if (Role == BuiltObjectRole.Base)
                            {
                                empty += TextResolver.GetText("Should we investigate the base?");
                                text = TextResolver.GetText("Abandoned Base Encountered");
                            }
                            else
                            {
                                empty += TextResolver.GetText("Should we investigate the ship?");
                            }
                            builtObject3.Empire.SendEventMessageToEmpire(EventMessageType.EncounterBuiltObject, text, empty, this, this);
                            break;
                        }
                    case BuiltObjectEncounterAction.Notify:
                        if (builtObject3.Empire != null && !builtObject3.Empire.Reclusive)
                        {
                            _Galaxy.InvestigateAbandonedBuiltObject(builtObject3.Empire, this);
                        }
                        break;
                    default:
                        _ = 2;
                        break;
                }
            }
        }

        private void PerformThreatEvaluation(DateTime time)
        {
            if (Empire == null)
            {
                _Threats = _Galaxy.EvaluateThreats(this, out _ThreatLevels, 100);
            }
            else
            {
                if (Role != BuiltObjectRole.Base && CurrentSpeed == (float)WarpSpeedWithBonuses && WarpSpeedWithBonuses > 0)
                {
                    return;
                }
                if (NearestSystemStar == null)
                {
                    _Threats = _Galaxy.EvaluateThreats(this, out _ThreatLevels);
                    return;
                }
                DateTime dateTime = time.AddSeconds(-5.0);
                if (Empire.SystemVisibility.Count > NearestSystemStar.SystemIndex && Empire.SystemVisibility[NearestSystemStar.SystemIndex].LatestThreatEvaluation < dateTime)
                {
                    Empire.SystemVisibility[NearestSystemStar.SystemIndex].LatestThreatEvaluation = time;
                    List<int> threatLevels = new List<int>();
                    BuiltObjectList threats = _Galaxy.EvaluateSystemThreats(NearestSystemStar, Empire, out threatLevels);
                    Empire.SystemVisibility[NearestSystemStar.SystemIndex].Threats = threats;
                    Empire.SystemVisibility[NearestSystemStar.SystemIndex].ThreatLevels = threatLevels;
                }
                _Threats = IdentifySystemThreatsToUs(NearestSystemStar, out _ThreatLevels, out _TotalThreatLevel);
            }
        }

        private StellarObject[] IdentifySystemThreatsToUs(Habitat systemStar, out int[] threatLevels, out int totalThreatLevel)
        {
            return BaconBuiltObject.IdentifySystemThreatsToUs(this, systemStar, out threatLevels, out totalThreatLevel);
        }

        private void ThreatEvaluation(Galaxy galaxy, DateTime time)
        {
            if (Role != BuiltObjectRole.Base && CurrentSpeed == (float)WarpSpeedWithBonuses && WarpSpeedWithBonuses > 0)
            {
                return;
            }
            PerformThreatEvaluation(time);
            bool flag = true;
            double currentTargetEmphasis = 1.0;
            BuiltObjectMission mission = Mission;
            if (!IsAutoControlled)
            {
                if (mission != null && (mission.Type == BuiltObjectMissionType.Patrol || mission.Type == BuiltObjectMissionType.Escort || mission.Type == BuiltObjectMissionType.Attack || mission.Type == BuiltObjectMissionType.Bombard || mission.Type == BuiltObjectMissionType.WaitAndAttack || mission.Type == BuiltObjectMissionType.WaitAndBombard || mission.Type == BuiltObjectMissionType.Capture || mission.Type == BuiltObjectMissionType.Raid || mission.Type == BuiltObjectMissionType.Blockade || mission.Type == BuiltObjectMissionType.Explore || mission.Type == BuiltObjectMissionType.Undefined))
                {
                    if (mission.CheckCommandsForHyperjump())
                    {
                        return;
                    }
                    currentTargetEmphasis = 1.0;
                }
                else if (mission != null && (mission.Type == BuiltObjectMissionType.Refuel || mission.Type == BuiltObjectMissionType.Move || mission.Type == BuiltObjectMissionType.MoveAndWait || mission.Type == BuiltObjectMissionType.UnloadTroops || mission.Type == BuiltObjectMissionType.Undefined || mission.Type == BuiltObjectMissionType.Build || mission.Type == BuiltObjectMissionType.ExtractResources))
                {
                    if (mission.CheckCommandsForUndock())
                    {
                        flag = false;
                    }
                    currentTargetEmphasis = 1.0;
                }
                else if (mission != null && mission.Type != 0)
                {
                    return;
                }
                if (mission != null && mission.Type != 0 && mission.ManuallyAssigned)
                {
                    switch (mission.Type)
                    {
                        case BuiltObjectMissionType.Escape:
                        case BuiltObjectMissionType.Retire:
                        case BuiltObjectMissionType.Retrofit:
                        case BuiltObjectMissionType.Refuel:
                        case BuiltObjectMissionType.LoadTroops:
                        case BuiltObjectMissionType.UnloadTroops:
                        case BuiltObjectMissionType.Deploy:
                        case BuiltObjectMissionType.Undeploy:
                        case BuiltObjectMissionType.Repair:
                        case BuiltObjectMissionType.Move:
                            return;
                    }
                }
            }
            if (Role == BuiltObjectRole.Base)
            {
                return;
            }
            StellarObject stellarObject = ShouldFleeFrom(galaxy);
            if (stellarObject != null)
            {
                if (BuiltAt != null || (mission != null && (mission.Type == BuiltObjectMissionType.Escape || HyperjumpPrepare)))
                {
                    return;
                }
                CheckColonyShipMissionCancelled(0);
                RecordRevertMission(BuiltObjectMissionType.Escape);
                ClearPreviousMissionRequirements();
                if (stellarObject is Fighter)
                {
                    Fighter fighter = (Fighter)stellarObject;
                    if (fighter.ParentBuiltObject != null && !fighter.ParentBuiltObject.HasBeenDestroyed)
                    {
                        stellarObject = fighter.ParentBuiltObject;
                    }
                }
                AssignMission(BuiltObjectMissionType.Escape, stellarObject, null, BuiltObjectMissionPriority.High);
            }
            else
            {
                if ((SubRole == BuiltObjectSubRole.ResupplyShip && IsDeployed) || !flag)
                {
                    return;
                }
                if (ShouldInvadeColony() && _ColonyToAttack != null && BuiltAt == null && CurrentFuel > 0.0 && CurrentEnergy > 0.0 && (mission == null || (mission.Type != BuiltObjectMissionType.Escape && mission.Type != BuiltObjectMissionType.Refuel)))
                {
                    bool flag2 = false;
                    if (mission != null && mission.Type == BuiltObjectMissionType.Raid && mission.TargetHabitat == _ColonyToAttack)
                    {
                        flag2 = true;
                    }
                    if (!flag2 && WithinFuelRange(_ColonyToAttack.Xpos, _ColonyToAttack.Ypos, 0.0))
                    {
                        if (ShipGroup != null && ShipGroup.BattleStats == null)
                        {
                            ShipGroup.BattleStats = new SpaceBattleStats();
                        }
                        RecordRevertMission(BuiltObjectMissionType.Attack, evenWhenAutomated: true);
                        AssignMission(BuiltObjectMissionType.Attack, _ColonyToAttack, null, BuiltObjectMissionPriority.High);
                        return;
                    }
                }
                bool flag3 = true;
                if (CurrentTarget != null && !CurrentTarget.HasBeenDestroyed && mission != null && (mission.Type == BuiltObjectMissionType.Attack || mission.Type == BuiltObjectMissionType.Bombard || mission.Type == BuiltObjectMissionType.Capture || mission.Type == BuiltObjectMissionType.Raid) && mission.ManuallyAssigned)
                {
                    flag3 = false;
                }
                if (!flag3)
                {
                    return;
                }
                _SecondaryTargets.Clear();
                _SecondaryThreatLevels.Clear();
                for (int i = 0; i < _Threats.Length; i++)
                {
                    StellarObject stellarObject2 = _Threats[i];
                    double num = _ThreatLevels[i];
                    if (stellarObject2 == null || !ShouldAttack(stellarObject2, time) || stellarObject2.HasBeenDestroyed || BuiltAt != null || !(CurrentFuel > 0.0) || !(CurrentEnergy > 0.0))
                    {
                        continue;
                    }
                    int currentAssignedFirepower = 0;
                    if (!EvaluateAdequateAttackers(stellarObject2, out currentAssignedFirepower))
                    {
                        if (CheckAssignAttackOnThreat(stellarObject2, mission, currentTargetEmphasis, num))
                        {
                            return;
                        }
                    }
                    else
                    {
                        _SecondaryTargets.Add(stellarObject2);
                        _SecondaryThreatLevels.Add(num);
                    }
                }
                if (_SecondaryTargets.Count <= 0)
                {
                    return;
                }
                for (int j = 0; j < _SecondaryTargets.Count; j++)
                {
                    StellarObject stellarObject3 = _SecondaryTargets[j];
                    double threatLevel = _SecondaryThreatLevels[j];
                    if (stellarObject3 != null && CheckAssignAttackOnThreat(stellarObject3, mission, currentTargetEmphasis, threatLevel))
                    {
                        break;
                    }
                }
            }
        }

        private bool CheckAssignAttackOnThreat(StellarObject threat, BuiltObjectMission mission, double currentTargetEmphasis, double threatLevel)
        {
            if (EvaluateAttackStrongBase(threat))
            {
                bool flag = false;
                if (threat is BuiltObject)
                {
                    CheckBattleOverwhelming((BuiltObject)threat);
                    flag = _LastWithdrawalEvaluation;
                }
                if (!flag && (float)_Galaxy.CalculateDistanceSquared(Xpos, Ypos, threat.Xpos, threat.Ypos) < AttackRangeSquared)
                {
                    bool flag2 = true;
                    if (mission != null && mission.Type == BuiltObjectMissionType.Refuel && mission.CheckCommandsForUndock())
                    {
                        flag2 = false;
                    }
                    if (flag2 && (mission == null || mission.Type != BuiltObjectMissionType.Escape))
                    {
                        StellarObject stellarObject = CurrentTarget;
                        if (stellarObject == null && mission != null)
                        {
                            if (mission.TargetBuiltObject != null)
                            {
                                stellarObject = mission.TargetBuiltObject;
                            }
                            else if (mission.TargetCreature != null)
                            {
                                stellarObject = mission.TargetCreature;
                            }
                        }
                        bool flag3 = true;
                        if (mission != null && mission.Type != 0 && ShipGroup != null && ShipGroup.Mission != null && ShipGroup.Mission.Type != 0 && mission.IsShipGroupMission && (!ShipGroup.AllowImmediateThreatEvaluation || mission.CheckCommandsForHyperjumpOrConditionalJump()))
                        {
                            flag3 = false;
                            if (mission.Type == BuiltObjectMissionType.Blockade)
                            {
                                Command command = mission.FastPeekCurrentCommand();
                                if (command != null && command.Action == CommandAction.Blockade)
                                {
                                    flag3 = true;
                                }
                            }
                            else if (mission.Type == BuiltObjectMissionType.WaitAndAttack || mission.Type == BuiltObjectMissionType.WaitAndBombard)
                            {
                                Command command2 = mission.FastPeekCurrentCommand();
                                if (command2 != null && command2.Action == CommandAction.HoldSyncFleet)
                                {
                                    flag3 = true;
                                }
                            }
                            else if (mission.Type == BuiltObjectMissionType.MoveAndWait)
                            {
                                if (threat is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)threat;
                                    if (builtObject.NearestSystemStar == NearestSystemStar)
                                    {
                                        flag3 = true;
                                    }
                                }
                                else if (threat is Creature)
                                {
                                    Creature creature = (Creature)threat;
                                    if (creature.NearestSystemStar == NearestSystemStar)
                                    {
                                        flag3 = true;
                                    }
                                }
                            }
                            else if (mission.Type == BuiltObjectMissionType.Patrol)
                            {
                                if (threat is BuiltObject)
                                {
                                    BuiltObject builtObject2 = (BuiltObject)threat;
                                    if (builtObject2.NearestSystemStar == NearestSystemStar)
                                    {
                                        flag3 = true;
                                    }
                                }
                                else if (threat is Creature)
                                {
                                    Creature creature2 = (Creature)threat;
                                    if (creature2.NearestSystemStar == NearestSystemStar)
                                    {
                                        flag3 = true;
                                    }
                                }
                            }
                            else if (mission.Priority == BuiltObjectMissionPriority.Low)
                            {
                                flag3 = true;
                            }
                            else if (ShipGroup.Mission.TargetShipGroup != null)
                            {
                                if (threat is BuiltObject)
                                {
                                    BuiltObject builtObject3 = (BuiltObject)threat;
                                    if (builtObject3.ShipGroup != null && builtObject3.ShipGroup == ShipGroup.Mission.TargetShipGroup)
                                    {
                                        flag3 = true;
                                    }
                                }
                            }
                            else if (ShipGroup.Mission.TargetBuiltObject != null)
                            {
                                if (threat is BuiltObject)
                                {
                                    BuiltObject builtObject4 = (BuiltObject)threat;
                                    if (ShipGroup.Mission.TargetBuiltObject == builtObject4)
                                    {
                                        flag3 = true;
                                    }
                                }
                            }
                            else if (ShipGroup.Mission.TargetHabitat != null && threat is BuiltObject)
                            {
                                BuiltObject item = (BuiltObject)threat;
                                if (ShipGroup.Mission.TargetHabitat.BasesAtHabitat.Contains(item))
                                {
                                    flag3 = true;
                                }
                            }
                        }
                        bool flag4 = true;
                        if (IsPlanetDestroyer && _ColonyToAttack != null)
                        {
                            flag4 = false;
                        }
                        if (flag3 && flag4 && mission != null && (mission.Type == BuiltObjectMissionType.Attack || mission.Type == BuiltObjectMissionType.Capture || mission.Type == BuiltObjectMissionType.Raid) && stellarObject != null)
                        {
                            int num = _Galaxy.DetermineThreatLevel(stellarObject, this);
                            int num2 = (int)threatLevel;
                            if (threat is BuiltObject)
                            {
                                BuiltObject builtObject5 = (BuiltObject)threat;
                                if (builtObject5.Role == BuiltObjectRole.Base)
                                {
                                    num2 = (int)((double)num2 / 6.0);
                                }
                            }
                            double num3 = 2.5;
                            if (stellarObject.TopSpeed <= 0)
                            {
                                num3 = 2.5;
                            }
                            if ((double)num * num3 * currentTargetEmphasis < (double)num2 && stellarObject != threat)
                            {
                                bool flag5 = false;
                                if (stellarObject is BuiltObject)
                                {
                                    BuiltObject builtObject6 = (BuiltObject)stellarObject;
                                    if (builtObject6.CurrentShields <= 0f || (double)builtObject6.CurrentShields <= (double)builtObject6.ShieldsCapacity / 2.0)
                                    {
                                        flag5 = true;
                                    }
                                }
                                if (!flag5 && WithinFuelRangeAndRefuel(threat.Xpos, threat.Ypos, 0.0))
                                {
                                    if (ShipGroup != null && ShipGroup.BattleStats == null)
                                    {
                                        ShipGroup.BattleStats = new SpaceBattleStats();
                                    }
                                    BuiltObjectMissionType builtObjectMissionType = BuiltObjectMissionType.Attack;
                                    if (threat is BuiltObject)
                                    {
                                        builtObjectMissionType = Empire.DetermineDestroyOrCaptureTarget(this, (BuiltObject)threat, attackingAsGroup: false);
                                    }
                                    RecordRevertMission(builtObjectMissionType, evenWhenAutomated: true);
                                    ClearPreviousMissionRequirements();
                                    AssignMission(builtObjectMissionType, threat, null, BuiltObjectMissionPriority.Normal);
                                    return true;
                                }
                            }
                        }
                        else if (mission != null && mission.TargetHabitat != null && ((mission.TargetHabitat.Empire != Empire && mission.Type == BuiltObjectMissionType.Attack) || (mission.TargetHabitat.Empire == Empire && mission.Type == BuiltObjectMissionType.UnloadTroops)) && Troops != null && Troops.TotalAttackStrength > 0)
                        {
                            double num4 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, mission.TargetHabitat.Xpos, mission.TargetHabitat.Ypos);
                            if (!(num4 < 1000000.0))
                            {
                            }
                        }
                        else
                        {
                            bool flag6 = false;
                            if (mission != null && (mission.Type == BuiltObjectMissionType.Attack || mission.Type == BuiltObjectMissionType.Capture || mission.Type == BuiltObjectMissionType.Raid) && mission.Target == threat)
                            {
                                flag6 = true;
                            }
                            if (!flag6 && flag4)
                            {
                                bool flag7 = false;
                                if (mission != null && mission.Type == BuiltObjectMissionType.Bombard && _ColonyToAttack != null)
                                {
                                    flag7 = true;
                                }
                                bool flag8 = false;
                                if (mission != null && mission.Type == BuiltObjectMissionType.Raid && _ColonyToAttack != null)
                                {
                                    flag8 = true;
                                }
                                if (!flag7 && !flag8 && flag3)
                                {
                                    if (ShipGroup != null && ShipGroup.BattleStats == null)
                                    {
                                        ShipGroup.BattleStats = new SpaceBattleStats();
                                    }
                                    BuiltObjectMissionType builtObjectMissionType2 = BuiltObjectMissionType.Attack;
                                    if (threat is BuiltObject)
                                    {
                                        builtObjectMissionType2 = Empire.DetermineDestroyOrCaptureTarget(this, (BuiltObject)threat, attackingAsGroup: false);
                                    }
                                    RecordRevertMission(builtObjectMissionType2, evenWhenAutomated: true);
                                    ClearPreviousMissionRequirements();
                                    AssignMission(builtObjectMissionType2, threat, null, BuiltObjectMissionPriority.Normal);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CheckFleetsTravellingToLocation(double x, double y, out int fleetStrength)
        {
            fleetStrength = 0;
            bool result = false;
            if (Empire != null)
            {
                int num = (int)(x - 2000.0);
                int num2 = (int)(x + 2000.0);
                int num3 = (int)(y - 2000.0);
                int num4 = (int)(y + 2000.0);
                for (int i = 0; i < Empire.ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = Empire.ShipGroups[i];
                    if (shipGroup.Mission != null && shipGroup.Mission.Type != 0)
                    {
                        Point point = shipGroup.Mission.ResolveTargetCoordinates(shipGroup.Mission);
                        if (point.X > num && point.X < num2 && point.Y > num3 && point.Y < num4)
                        {
                            fleetStrength += shipGroup.TotalOverallStrengthFactor;
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        private void FleeFromHopelessBattle()
        {
            if (!InBattle || !CheckBattleOverwhelming(null) || BuiltAt != null)
            {
                return;
            }
            if (ShipGroup != null)
            {
                if (ShipGroup.LeadShip == null || ShipGroup.LeadShip != this || _HyperjumpPrepare)
                {
                    return;
                }
                ShipGroup.CompleteMission();
                _LastWithdrawalEvaluation = false;
                if (ShipGroup != null && (ShipGroup.Mission == null || ShipGroup.Mission.Type == BuiltObjectMissionType.Undefined))
                {
                    StellarObject stellarObject = null;
                    if (Attackers.Count > 0)
                    {
                        stellarObject = Attackers[0];
                    }
                    else if (Pursuers.Count > 0)
                    {
                        stellarObject = Pursuers[0];
                    }
                    else if (CurrentTarget != null)
                    {
                        stellarObject = CurrentTarget;
                    }
                    if (stellarObject != null)
                    {
                        ShipGroup.AssignMission(BuiltObjectMissionType.Escape, stellarObject, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                    }
                }
            }
            else if (Mission == null || (Mission.Type != BuiltObjectMissionType.Escape && !HyperjumpPrepare))
            {
                StellarObject stellarObject2 = null;
                if (Attackers.Count > 0)
                {
                    stellarObject2 = Attackers[0];
                }
                else if (Pursuers.Count > 0)
                {
                    stellarObject2 = Pursuers[0];
                }
                else if (CurrentTarget != null)
                {
                    stellarObject2 = CurrentTarget;
                }
                if (stellarObject2 != null)
                {
                    CheckColonyShipMissionCancelled(0);
                    ClearPreviousMissionRequirements();
                    AssignMission(BuiltObjectMissionType.Escape, stellarObject2, null, BuiltObjectMissionPriority.High);
                }
            }
        }

        private bool CheckBattleOverwhelming(BuiltObject targetThreat)
        {
            if (InBattle && IsAutoControlled && Mission != null && (Mission.Type == BuiltObjectMissionType.Attack || Mission.Type == BuiltObjectMissionType.Bombard) && Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null)
            {
                BuiltObject builtObject = targetThreat;
                if (builtObject == null)
                {
                    for (int i = 0; i < _Threats.Length; i++)
                    {
                        if (_Threats[i] is BuiltObject)
                        {
                            builtObject = (BuiltObject)_Threats[i];
                            break;
                        }
                    }
                }
                if (builtObject != null)
                {
                    int totalThreatLevel = builtObject.TotalThreatLevel;
                    int totalThreatLevel2 = _TotalThreatLevel;
                    double num = (double)totalThreatLevel2 / (double)totalThreatLevel;
                    double num2 = (double)Empire.DominantRace.AggressionLevel / 100.0;
                    double num3 = (double)Empire.DominantRace.CautionLevel / 100.0;
                    double val = num2 / num3;
                    val = Math.Max(0.8, Math.Min(val, 1.25));
                    val *= 2.0;
                    val /= num;
                    if (Empire != null && Empire.Policy != null)
                    {
                        val /= Empire.Policy.ShipBattleCautionFactor;
                    }
                    if (val < 1.0)
                    {
                        int fleetStrength = 0;
                        if (CheckFleetsTravellingToLocation(Xpos, Ypos, out fleetStrength))
                        {
                            val *= num;
                            num = (double)totalThreatLevel2 / (double)(totalThreatLevel + fleetStrength);
                            val /= num;
                        }
                        if (builtObject != null)
                        {
                            double num4 = (double)builtObject.CurrentShields / (double)builtObject.ShieldsCapacity;
                            if (num4 < 0.3)
                            {
                                val /= num4 / 0.3;
                            }
                        }
                        if (val < 1.0)
                        {
                            Habitat habitat = _Galaxy.FastFindNearestColony((int)Xpos, (int)Ypos, Empire, 50000);
                            double num5 = 536870911.0;
                            if (habitat != null)
                            {
                                num5 = _Galaxy.CalculateDistance(Xpos, Ypos, habitat.Xpos, habitat.Ypos);
                            }
                            if (num5 < 1500.0)
                            {
                                _LastWithdrawalEvaluation = false;
                                return false;
                            }
                            if (_LastWithdrawalEvaluation)
                            {
                                _LastWithdrawalEvaluation = false;
                                return true;
                            }
                            _LastWithdrawalEvaluation = true;
                            return false;
                        }
                        _LastWithdrawalEvaluation = false;
                        return false;
                    }
                    _LastWithdrawalEvaluation = false;
                    return false;
                }
                _LastWithdrawalEvaluation = false;
                return false;
            }
            _LastWithdrawalEvaluation = false;
            return false;
        }

        private BattleTactics DetermineTacticsAgainstTarget(StellarObject abstractTarget)
        {
            BattleTactics battleTactics = BattleTactics.Undefined;
            if (abstractTarget is Creature)
            {
                Creature creature = (Creature)abstractTarget;
                double num = (double)(creature.AttackStrength * 5) / (double)FirepowerRaw;
                battleTactics = ((!(num > 1.3)) ? Design.TacticsWeakerShips : Design.TacticsStrongerShips);
            }
            else if (abstractTarget is Habitat)
            {
                _ = (Habitat)abstractTarget;
                battleTactics = Design.TacticsWeakerShips;
            }
            else
            {
                BuiltObject builtObject = (BuiltObject)abstractTarget;
                double num2 = (double)builtObject.FirepowerRaw / (double)FirepowerRaw;
                battleTactics = ((!(num2 > 1.3)) ? Design.TacticsWeakerShips : Design.TacticsStrongerShips);
            }
            if (battleTactics == BattleTactics.PointBlank && FirepowerRaw <= 0)
            {
                battleTactics = BattleTactics.Evade;
            }
            if (battleTactics == BattleTactics.Standoff && StandoffWeaponsMaxRange <= 0)
            {
                battleTactics = ((BeamWeaponsMinRange <= 0) ? BattleTactics.Evade : BattleTactics.AllWeapons);
            }
            if (battleTactics == BattleTactics.AllWeapons && BeamWeaponsMinRange <= 0)
            {
                battleTactics = BattleTactics.Standoff;
            }
            return battleTactics;
        }

        private int DetermineAttackingFirepower(StellarObject potentialTarget, out double closestAttackerDistance)
        {
            int num = 0;
            closestAttackerDistance = 536870911.0;
            for (int i = 0; i < potentialTarget.Pursuers.Count; i++)
            {
                StellarObject stellarObject = potentialTarget.Pursuers[i];
                if (stellarObject == null || stellarObject.HasBeenDestroyed || stellarObject.FirepowerRaw <= 0 || stellarObject.TopSpeed <= 0 || !stellarObject.IsFunctional)
                {
                    continue;
                }
                bool flag = true;
                if (stellarObject is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)stellarObject;
                    BuiltObjectMission mission = builtObject.Mission;
                    if (mission != null)
                    {
                        switch (mission.Type)
                        {
                            default:
                                flag = false;
                                break;
                            case BuiltObjectMissionType.Attack:
                            case BuiltObjectMissionType.WaitAndAttack:
                            case BuiltObjectMissionType.WaitAndBombard:
                            case BuiltObjectMissionType.Bombard:
                            case BuiltObjectMissionType.Capture:
                            case BuiltObjectMissionType.Raid:
                                break;
                        }
                    }
                }
                if (!flag)
                {
                    continue;
                }
                double num2 = _Galaxy.CalculateDistance(stellarObject.Xpos, stellarObject.Ypos, potentialTarget.Xpos, potentialTarget.Ypos);
                if (num2 < Galaxy.AttackEvaluationRangeFactor)
                {
                    int num3 = stellarObject.FirepowerRaw;
                    if (stellarObject is BuiltObject)
                    {
                        BuiltObject builtObject2 = (BuiltObject)stellarObject;
                        num3 = builtObject2.CalculateOverallStrengthFactor();
                    }
                    else if (stellarObject is Creature)
                    {
                        Creature creature = (Creature)stellarObject;
                        num3 = creature.AttackStrength * 5;
                    }
                    num += num3;
                }
                if (num2 < closestAttackerDistance)
                {
                    closestAttackerDistance = num2;
                }
            }
            return num;
        }

        private bool EvaluateAttackStrongBase(StellarObject target)
        {
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                int num = CalculateOverallStrengthFactor();
                if (builtObject.Role == BuiltObjectRole.Base && builtObject.CalculateOverallStrengthFactor() > num)
                {
                    int num2 = CalculateOverallStrengthFactor();
                    if (Empire != null && NearestSystemStar != null)
                    {
                        BuiltObjectList threats = Empire.SystemVisibility[NearestSystemStar.SystemIndex].Threats;
                        num2 = _Galaxy.CalculateNearbyOverallStrength(builtObject.Xpos, builtObject.Ypos, builtObject.Empire, 800.0, threats);
                    }
                    if (num > num2)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return true;
        }

        private bool EvaluateAdequateAttackers(StellarObject potentialTarget)
        {
            int currentAssignedFirepower = 0;
            return EvaluateAdequateAttackers(potentialTarget, out currentAssignedFirepower);
        }

        private bool EvaluateAdequateAttackers(StellarObject potentialTarget, out int currentAssignedFirepower)
        {
            double closestAttackerDistance = 0.0;
            currentAssignedFirepower = DetermineAttackingFirepower(potentialTarget, out closestAttackerDistance);
            int num = 1;
            int num2 = potentialTarget.FirepowerRaw;
            if (potentialTarget is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)potentialTarget;
                num2 = builtObject.CalculateOverallStrengthFactor();
            }
            else if (potentialTarget is Creature)
            {
                Creature creature = (Creature)potentialTarget;
                num2 = creature.AttackStrength * 5;
                if (creature.Type == CreatureType.SilverMist)
                {
                    num2 *= 4;
                }
            }
            num = ((Empire == null) ? ((int)((double)num2 * Galaxy.AttackOvermatchFactor) + 1) : ((int)((double)num2 * (double)Empire.AttackOvermatchFactor) + 1));
            if (currentAssignedFirepower < num)
            {
                return false;
            }
            if (closestAttackerDistance * closestAttackerDistance > Galaxy.StrikeRangeSquared)
            {
                double num3 = _Galaxy.CalculateDistanceSquared(potentialTarget.Xpos, potentialTarget.Ypos, Xpos, Ypos);
                if (num3 < Galaxy.StrikeRangeSquared)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        private bool ShouldAttack(StellarObject potentialTarget, DateTime time)
        {
            return ShouldAttack(potentialTarget, time, includeBoardingCheck: true);
        }

        private bool ShouldAttack(StellarObject potentialTarget, DateTime time, bool includeBoardingCheck)
        {
            if (Empire == null)
            {
                return false;
            }
            if (Mission != null && Mission.Type == BuiltObjectMissionType.Refuel)
            {
                if (Mission.CheckCommandsForUndock())
                {
                    return false;
                }
            }
            else if (Mission != null && (Mission.Type == BuiltObjectMissionType.Retire || Mission.Type == BuiltObjectMissionType.Retrofit || Mission.Type == BuiltObjectMissionType.Repair || Mission.Type == BuiltObjectMissionType.Escape))
            {
                return false;
            }
            bool flag = true;
            if (WarpSpeed <= 0)
            {
                double num = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, potentialTarget.Xpos, potentialTarget.Ypos);
                if (num > 9000000.0)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                if (potentialTarget is Creature)
                {
                    Creature creature = (Creature)potentialTarget;
                    if (creature.IsVisible)
                    {
                        if ((FirepowerRaw > 0 || FighterCapacity > 0) && IsFunctional)
                        {
                            bool flag2 = false;
                            switch (Design.Stance)
                            {
                                case BuiltObjectStance.DoNotAttack:
                                    flag2 = false;
                                    break;
                                case BuiltObjectStance.AttackIfAttacked:
                                    flag2 = (Attackers.Contains(potentialTarget) ? true : false);
                                    break;
                                case BuiltObjectStance.AttackUnallied:
                                case BuiltObjectStance.AttackEnemies:
                                    flag2 = true;
                                    break;
                            }
                            if (flag2)
                            {
                                if (Role == BuiltObjectRole.Base)
                                {
                                    return true;
                                }
                                if (TopSpeed > 0)
                                {
                                    bool flag3 = true;
                                    if (IsPlanetDestroyer)
                                    {
                                        flag3 = false;
                                    }
                                    else if (ShipGroup != null)
                                    {
                                        if (ShipGroup.Mission != null && ShipGroup.Mission.Type != 0 && ShipGroup.Mission.Type != BuiltObjectMissionType.Patrol && ShipGroup.Mission.Type != BuiltObjectMissionType.MoveAndWait && ShipGroup.Mission.Priority != BuiltObjectMissionPriority.Low)
                                        {
                                            flag3 = false;
                                        }
                                        if (!flag3 && (Mission == null || Mission.Type == BuiltObjectMissionType.Undefined))
                                        {
                                            flag3 = true;
                                        }
                                    }
                                    if (flag3)
                                    {
                                        return true;
                                    }
                                    if (creature.CurrentTarget is BuiltObject && (BuiltObject)creature.CurrentTarget == this)
                                    {
                                        return true;
                                    }
                                    return false;
                                }
                                return false;
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
                if (potentialTarget is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)potentialTarget;
                    if (Empire == builtObject.Empire || builtObject.Empire == null)
                    {
                        return false;
                    }
                    if (builtObject.Empire == _Galaxy.IndependentEmpire)
                    {
                        return false;
                    }
                    if (builtObject.PirateEmpireId > 0 && builtObject.PirateEmpireId == PirateEmpireId)
                    {
                        return false;
                    }
                    if (FirepowerRaw <= 0 && FighterCapacity <= 0)
                    {
                        return false;
                    }
                    if (!IsFunctional || (TopSpeed <= 0 && Role != BuiltObjectRole.Base))
                    {
                        return false;
                    }
                    if (builtObject.NearestSystemStar != NearestSystemStar)
                    {
                        return false;
                    }
                    if (builtObject.Empire.PirateEmpireBaseHabitat != null && Empire != null && builtObject.Empire.ObtainPirateRelation(Empire).Type == PirateRelationType.Protection)
                    {
                        return false;
                    }
                    if (Empire.PirateEmpireBaseHabitat != null && builtObject.Empire != null && Empire.ObtainPirateRelation(builtObject.Empire).Type == PirateRelationType.Protection)
                    {
                        return false;
                    }
                    if (includeBoardingCheck)
                    {
                        if (Empire.CheckOurEmpireOverwhelmingBoarding(builtObject))
                        {
                            return false;
                        }
                        if (builtObject.CurrentShields < (float)Math.Max(15, (int)AssaultShieldPenetration) && CalculateAvailableAssaultPodAttackStrength(time) <= 0 && Empire.CheckOurEmpireBoarding(builtObject, this))
                        {
                            return false;
                        }
                    }
                    bool flag4 = false;
                    if (Empire.Outlaws.Contains(builtObject))
                    {
                        if (Empire == builtObject.Empire)
                        {
                            Empire.Outlaws.Remove(builtObject);
                            return false;
                        }
                        flag4 = true;
                    }
                    switch (Design.Stance)
                    {
                        case BuiltObjectStance.DoNotAttack:
                            return false;
                        case BuiltObjectStance.AttackIfAttacked:
                            if (Attackers.ContainsFighterOrBuiltObject(builtObject))
                            {
                                return true;
                            }
                            return false;
                        case BuiltObjectStance.AttackUnallied:
                            {
                                if (Attackers.ContainsFighterOrBuiltObject(builtObject))
                                {
                                    return true;
                                }
                                DiplomaticRelation diplomaticRelation = Empire.DiplomaticRelations[builtObject.Empire];
                                if (diplomaticRelation != null)
                                {
                                    if (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation.Type == DiplomaticRelationType.Protectorate || diplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion || diplomaticRelation.Type == DiplomaticRelationType.Truce)
                                    {
                                        return false;
                                    }
                                    return true;
                                }
                                return true;
                            }
                        case BuiltObjectStance.AttackEnemies:
                            {
                                if (Attackers.ContainsFighterOrBuiltObject(builtObject))
                                {
                                    return true;
                                }
                                if ((Empire != null && Empire.PirateEmpireBaseHabitat != null) || (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null))
                                {
                                    PirateRelation pirateRelation = null;
                                    if (builtObject.Empire != null)
                                    {
                                        pirateRelation = Empire.ObtainPirateRelation(builtObject.Empire);
                                    }
                                    if (pirateRelation.Type != PirateRelationType.Protection)
                                    {
                                        return true;
                                    }
                                    if (flag4)
                                    {
                                        return true;
                                    }
                                    return false;
                                }
                                DiplomaticRelation diplomaticRelation = Empire.DiplomaticRelations[builtObject.Empire];
                                if (builtObject.Empire.PirateEmpireBaseHabitat != null)
                                {
                                    return true;
                                }
                                if (Empire.PirateEmpireBaseHabitat != null && builtObject.Empire != Empire)
                                {
                                    return true;
                                }
                                if (flag4)
                                {
                                    return true;
                                }
                                if (diplomaticRelation != null)
                                {
                                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                                    {
                                        return true;
                                    }
                                    if (Mission != null && (Mission.Type == BuiltObjectMissionType.Attack || Mission.Type == BuiltObjectMissionType.Bombard || Mission.Type == BuiltObjectMissionType.WaitAndAttack || Mission.Type == BuiltObjectMissionType.WaitAndBombard))
                                    {
                                        Empire empire = BuiltObjectMission.ResolveMissionTargetEmpire(Mission);
                                        if (potentialTarget.Empire == empire)
                                        {
                                            return true;
                                        }
                                    }
                                    return false;
                                }
                                return false;
                            }
                    }
                }
            }
            return false;
        }

        private void CheckForAttack(Galaxy galaxy)
        {
            if (Attackers.Count <= 0 || Role == BuiltObjectRole.Base)
            {
                return;
            }
            bool flag = true;
            if (!IsAutoControlled && Mission != null && Mission.Type == BuiltObjectMissionType.Move)
            {
                flag = false;
            }
            if (Mission != null && (Mission.Type == BuiltObjectMissionType.Escape || Mission.Type == BuiltObjectMissionType.Attack || Mission.Type == BuiltObjectMissionType.Bombard || Mission.Type == BuiltObjectMissionType.Refuel || Mission.Type == BuiltObjectMissionType.Repair))
            {
                flag = false;
            }
            if (Mission != null && (Mission.Priority == BuiltObjectMissionPriority.High || Mission.Priority == BuiltObjectMissionPriority.VeryHigh))
            {
                flag = false;
            }
            if (SubRole == BuiltObjectSubRole.ResupplyShip && IsDeployed)
            {
                flag = false;
            }
            if (flag)
            {
                if (!ShouldCounterAttack() || !(CurrentFuel > 0.0) || !(CurrentEnergy > 0.0) || BuiltAt != null)
                {
                    return;
                }
                double num = (double)SensorProximityArrayRange * (double)SensorProximityArrayRange;
                StellarObject stellarObject = null;
                for (int i = 0; i < _Threats.Length; i++)
                {
                    if (_Threats[i] == null || Attackers.IndexOf(_Threats[i]) < 0)
                    {
                        continue;
                    }
                    stellarObject = _Threats[i];
                    if (stellarObject == null || stellarObject.HasBeenDestroyed || stellarObject == CurrentTarget)
                    {
                        continue;
                    }
                    double num2 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, stellarObject.Xpos, stellarObject.Ypos);
                    if (!(num2 <= num) || !WithinFuelRangeAndRefuel(stellarObject.Xpos, stellarObject.Ypos, 0.0))
                    {
                        continue;
                    }
                    bool flag2 = true;
                    if (stellarObject is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)stellarObject;
                        if (builtObject.NearestSystemStar != NearestSystemStar || (builtObject.WarpSpeed > 0 && builtObject.CurrentSpeed > (float)builtObject.TopSpeed))
                        {
                            flag2 = false;
                        }
                    }
                    if (flag2)
                    {
                        BuiltObjectMissionType builtObjectMissionType = BuiltObjectMissionType.Attack;
                        if (Empire != null && stellarObject is BuiltObject)
                        {
                            builtObjectMissionType = Empire.DetermineDestroyOrCaptureTarget(this, (BuiltObject)stellarObject, attackingAsGroup: false);
                        }
                        RecordRevertMission(builtObjectMissionType, evenWhenAutomated: true);
                        ClearPreviousMissionRequirements();
                        AssignMission(builtObjectMissionType, stellarObject, null, BuiltObjectMissionPriority.Normal);
                        break;
                    }
                }
                return;
            }
            StellarObject stellarObject2 = ShouldFleeFrom(galaxy);
            if (stellarObject2 == null || BuiltAt != null || (Mission != null && (Mission.Type == BuiltObjectMissionType.Escape || HyperjumpPrepare)))
            {
                return;
            }
            CheckColonyShipMissionCancelled(0);
            RecordRevertMission(BuiltObjectMissionType.Escape);
            ClearPreviousMissionRequirements();
            if (stellarObject2 is Fighter)
            {
                Fighter fighter = (Fighter)stellarObject2;
                if (fighter.ParentBuiltObject != null && !fighter.ParentBuiltObject.HasBeenDestroyed)
                {
                    stellarObject2 = fighter.ParentBuiltObject;
                }
            }
            AssignMission(BuiltObjectMissionType.Escape, stellarObject2, null, BuiltObjectMissionPriority.High);
        }

        public bool CheckClearDocking()
        {
            if (WarpSpeed > 0 && CurrentSpeed >= (float)WarpSpeed)
            {
                return CheckClearDocking(forceUndock: true);
            }
            return false;
        }

        public bool CheckClearDocking(bool forceUndock)
        {
            bool result = false;
            if (forceUndock)
            {
                if (DockedAt != null)
                {
                    if (DockedAt.DockingBayWaitQueue != null && DockedAt.DockingBayWaitQueue.Contains(this))
                    {
                        DockedAt.DockingBayWaitQueue.Remove(this);
                        result = true;
                    }
                    if (DockedAt.DockingBays != null)
                    {
                        for (int i = 0; i < DockedAt.DockingBays.Count; i++)
                        {
                            DockingBay dockingBay = DockedAt.DockingBays[i];
                            if (dockingBay.DockedShip == this)
                            {
                                dockingBay.DockedShip = null;
                                result = true;
                            }
                        }
                    }
                }
                DockedAt = null;
                Habitat parentHabitat = ParentHabitat;
                if (parentHabitat != null)
                {
                    if (parentHabitat.DockingBayWaitQueue != null && parentHabitat.DockingBayWaitQueue.Contains(this))
                    {
                        parentHabitat.DockingBayWaitQueue.Remove(this);
                        result = true;
                    }
                    if (parentHabitat.DockingBays != null)
                    {
                        for (int j = 0; j < parentHabitat.DockingBays.Count; j++)
                        {
                            DockingBay dockingBay2 = parentHabitat.DockingBays[j];
                            if (dockingBay2.DockedShip == this)
                            {
                                dockingBay2.DockedShip = null;
                                result = true;
                            }
                        }
                    }
                }
                BuiltObject parentBuiltObject = ParentBuiltObject;
                if (parentBuiltObject != null)
                {
                    if (parentBuiltObject.DockingBayWaitQueue != null && parentBuiltObject.DockingBayWaitQueue.Contains(this))
                    {
                        parentBuiltObject.DockingBayWaitQueue.Remove(this);
                        result = true;
                    }
                    if (parentBuiltObject.DockingBays != null)
                    {
                        for (int k = 0; k < parentBuiltObject.DockingBays.Count; k++)
                        {
                            DockingBay dockingBay3 = parentBuiltObject.DockingBays[k];
                            if (dockingBay3.DockedShip == this)
                            {
                                dockingBay3.DockedShip = null;
                                result = true;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public void ClearPreviousMissionRequirements()
        {
            ClearPreviousMissionRequirements(manuallyAssigned: false);
        }

        public void ClearPreviousMissionRequirements(bool manuallyAssigned)
        {
            BuiltObjectMission mission = Mission;
            if (mission != null)
            {
                BuiltObject targetBuiltObject = mission.TargetBuiltObject;
                Creature targetCreature = mission.TargetCreature;
                TroopList troops = mission.Troops;
                switch (mission.Type)
                {
                    case BuiltObjectMissionType.Patrol:
                    case BuiltObjectMissionType.Escort:
                    case BuiltObjectMissionType.Attack:
                    case BuiltObjectMissionType.WaitAndAttack:
                    case BuiltObjectMissionType.WaitAndBombard:
                    case BuiltObjectMissionType.Bombard:
                    case BuiltObjectMissionType.Capture:
                    case BuiltObjectMissionType.Raid:
                        if (targetBuiltObject != null && targetBuiltObject.Pursuers != null)
                        {
                            targetBuiltObject.Pursuers.Remove(this);
                        }
                        if (targetCreature != null && targetCreature.Pursuers != null)
                        {
                            targetCreature.Pursuers.Remove(this);
                        }
                        _ColonyToAttack = null;
                        break;
                    case BuiltObjectMissionType.LoadTroops:
                        if (troops != null && troops.Count > 0)
                        {
                            for (int i = 0; i < troops.Count; i++)
                            {
                            }
                        }
                        break;
                    case BuiltObjectMissionType.Refuel:
                        CheckCancelRefuelData();
                        break;
                }
                if (Role == BuiltObjectRole.Freight || SubRole == BuiltObjectSubRole.ConstructionShip)
                {
                    CheckCancelContracts();
                    if (Role == BuiltObjectRole.Freight && Cargo != null)
                    {
                        BaconBuiltObject.ClearCargo(this);
                    }
                }
                StellarObject stellarObject = null;
                Command command = mission.FastPeekCurrentCommand();
                if (command != null && command.Action == CommandAction.Dock)
                {
                    if (command.TargetBuiltObject != null)
                    {
                        stellarObject = command.TargetBuiltObject;
                    }
                    else if (command.TargetHabitat != null)
                    {
                        stellarObject = command.TargetHabitat;
                    }
                }
                if (stellarObject == null)
                {
                    if (ParentBuiltObject != null)
                    {
                        stellarObject = ParentBuiltObject;
                    }
                    else if (ParentHabitat != null)
                    {
                        stellarObject = ParentHabitat;
                    }
                }
                if (stellarObject != null && stellarObject.DockingBayWaitQueue != null)
                {
                    while (stellarObject.DockingBayWaitQueue.Contains(this))
                    {
                        stellarObject.DockingBayWaitQueue.Remove(this);
                    }
                }
            }
            if (Role != BuiltObjectRole.Base)
            {
                if (ManufacturingQueue != null)
                {
                    ManufacturingQueue.Clear();
                }
                if (ConstructionQueue != null)
                {
                    ConstructionQueue.Clear();
                }
            }
            if (Role != BuiltObjectRole.Base || ParentHabitat == null)
            {
                ParentOffsetX = -2000000001.0;
                ParentOffsetY = -2000000001.0;
                ParentBuiltObject = null;
                ParentHabitat = null;
            }
            StellarObject dockedAt = DockedAt;
            if (dockedAt != null)
            {
                if (dockedAt.DockingBayWaitQueue != null && dockedAt.DockingBayWaitQueue.Contains(this))
                {
                    dockedAt.DockingBayWaitQueue.Remove(this);
                }
                DockingBayList dockingBays = dockedAt.DockingBays;
                if (dockingBays != null)
                {
                    for (int j = 0; j < dockingBays.Count; j++)
                    {
                        DockingBay dockingBay = dockingBays[j];
                        if (dockingBay != null && dockingBay.DockedShip == this)
                        {
                            dockingBay.DockedShip = null;
                        }
                    }
                }
            }
            DockedAt = null;
            StellarObject builtAt = BuiltAt;
            if (builtAt == null && RetrofitDesign != null)
            {
                RetrofitDesign = null;
            }
            if (builtAt != null)
            {
                ConstructionQueue constructionQueue = builtAt.ConstructionQueue;
                if (constructionQueue != null)
                {
                    ConstructionYardList constructionYards = constructionQueue.ConstructionYards;
                    if (constructionYards != null)
                    {
                        for (int k = 0; k < constructionYards.Count; k++)
                        {
                            ConstructionYard constructionYard = constructionYards[k];
                            if (constructionYard != null && constructionYard.ShipUnderConstruction == this)
                            {
                                constructionYard.ShipUnderConstruction = null;
                            }
                        }
                    }
                }
            }
            BuiltAt = null;
            if (manuallyAssigned)
            {
                RevertMission = null;
            }
            mission?.Clear();
            FirstExecutionOfCommand = true;
            if (CurrentSpeed > (float)TopSpeed)
            {
                CurrentSpeed = CruiseSpeed;
                TargetSpeed = CruiseSpeed;
                UpdatePosition();
                CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(_Galaxy.CurrentDateTime);
            }
        }

        private StellarObject ShouldFleeFrom(Galaxy galaxy)
        {
            if (!IsFunctional || TopSpeed <= 0 || Empire == null)
            {
                return null;
            }
            if (Attackers.Count > 0)
            {
                StellarObject stellarObject = null;
                for (int i = 0; i < Attackers.Count; i++)
                {
                    if (!Attackers[i].HasBeenDestroyed)
                    {
                        double num = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, Attackers[i].Xpos, Attackers[i].Ypos);
                        if (num < 2304000000.0)
                        {
                            stellarObject = Attackers[i];
                            break;
                        }
                    }
                }
                if (stellarObject != null)
                {
                    if (stellarObject is BuiltObject && Attackers.Count <= 1)
                    {
                        BuiltObject builtObject = (BuiltObject)stellarObject;
                        if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Escape)
                        {
                            return null;
                        }
                    }
                    if ((DamagedComponentCount > 0 || CurrentFuel <= 0.0) && Design.FleeWhen != BuiltObjectFleeWhen.Never && Design.FleeWhen != BuiltObjectFleeWhen.Armor50)
                    {
                        return stellarObject;
                    }
                    switch (Design.FleeWhen)
                    {
                        case BuiltObjectFleeWhen.EnemyMilitarySighted:
                            return stellarObject;
                        case BuiltObjectFleeWhen.Attacked:
                            return stellarObject;
                        case BuiltObjectFleeWhen.Shields50:
                            if (CurrentShields <= (float)(ShieldsCapacity / 2))
                            {
                                return stellarObject;
                            }
                            break;
                        case BuiltObjectFleeWhen.Shields20:
                            if (CurrentShields <= (float)(int)((double)ShieldsCapacity * 0.2))
                            {
                                return stellarObject;
                            }
                            break;
                        case BuiltObjectFleeWhen.Armor50:
                            {
                                if (CurrentShields <= (float)(int)((double)ShieldsCapacity * 0.2))
                                {
                                    return stellarObject;
                                }
                                if (Design == null)
                                {
                                    break;
                                }
                                if (DamagedComponentCount > 0)
                                {
                                    for (int j = 0; j < Components.Count; j++)
                                    {
                                        BuiltObjectComponent builtObjectComponent = Components[j];
                                        if (builtObjectComponent != null && builtObjectComponent.Status == ComponentStatus.Damaged && builtObjectComponent.Type != ComponentType.Armor)
                                        {
                                            return stellarObject;
                                        }
                                    }
                                }
                                float num2 = (float)Armor / (float)Math.Max(1, Design.Armor);
                                if (num2 <= 0.5f)
                                {
                                    return stellarObject;
                                }
                                break;
                            }
                        case BuiltObjectFleeWhen.Never:
                            return null;
                    }
                }
            }
            else if (Design.FleeWhen == BuiltObjectFleeWhen.EnemyMilitarySighted)
            {
                for (int k = 0; k < _Threats.Length; k++)
                {
                    if (_Threats[k] == null)
                    {
                        continue;
                    }
                    if (_Threats[k] is Creature)
                    {
                        Creature creature = (Creature)_Threats[k];
                        if (galaxy.CheckWithinCreatureAttackRange(Xpos, Ypos, creature))
                        {
                            return _Threats[k];
                        }
                        continue;
                    }
                    BuiltObject builtObject2 = (BuiltObject)_Threats[k];
                    if (builtObject2.Empire == Empire || builtObject2.Empire == null || !builtObject2.IsFunctional || (builtObject2.FirepowerRaw <= 0 && builtObject2.FighterCapacity <= 0))
                    {
                        continue;
                    }
                    if (builtObject2.TopSpeed <= 0)
                    {
                        double num3 = _Galaxy.CalculateDistance(builtObject2.Xpos, builtObject2.Ypos, Xpos, Ypos);
                        if (num3 > (double)builtObject2.MaximumWeaponsRange)
                        {
                            continue;
                        }
                    }
                    BuiltObject builtObject3 = null;
                    if (builtObject2.Empire.PirateEmpireBaseHabitat != null)
                    {
                        if (Empire != null && builtObject2.Empire.ObtainPirateRelation(Empire).Type != PirateRelationType.Protection)
                        {
                            builtObject3 = builtObject2;
                        }
                    }
                    else if (Empire.Outlaws.Contains(builtObject2))
                    {
                        builtObject3 = builtObject2;
                    }
                    if (builtObject3 != null && builtObject3.Role == BuiltObjectRole.Military && !builtObject3.HasBeenDestroyed)
                    {
                        double num4 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject3.Xpos, builtObject3.Ypos);
                        if (num4 < 4000000.0)
                        {
                            return builtObject3;
                        }
                    }
                    if (builtObject2.Empire.PirateEmpireBaseHabitat == null && Empire.PirateEmpireBaseHabitat == null)
                    {
                        DiplomaticRelation diplomaticRelation = Empire.DiplomaticRelations[builtObject2.Empire];
                        if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.War && builtObject2.Role == BuiltObjectRole.Military && !builtObject2.HasBeenDestroyed)
                        {
                            double num5 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject2.Xpos, builtObject2.Ypos);
                            if (num5 < 4000000.0)
                            {
                                return builtObject2;
                            }
                        }
                    }
                    else
                    {
                        if (builtObject2.Empire == null)
                        {
                            continue;
                        }
                        PirateRelation pirateRelation = Empire.ObtainPirateRelation(builtObject2.Empire);
                        if (pirateRelation != null && pirateRelation.Type != PirateRelationType.Protection && builtObject2.Role == BuiltObjectRole.Military && !builtObject2.HasBeenDestroyed)
                        {
                            double num6 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject2.Xpos, builtObject2.Ypos);
                            if (num6 < 4000000.0)
                            {
                                return builtObject2;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private bool ShouldCounterAttack()
        {
            if (FirepowerRaw <= 0 && FighterCapacity <= 0)
            {
                return false;
            }
            if (!IsFunctional || TopSpeed <= 0)
            {
                return false;
            }
            switch (Design.Stance)
            {
                case BuiltObjectStance.DoNotAttack:
                    return false;
                case BuiltObjectStance.AttackUnallied:
                case BuiltObjectStance.AttackEnemies:
                case BuiltObjectStance.AttackIfAttacked:
                    if (Attackers.Count > 0)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        private bool CanFlee()
        {
            bool result = true;
            if (TopSpeed == 0)
            {
                result = false;
            }
            if (BuiltAt != null)
            {
                result = false;
            }
            for (int i = 0; i < ConstructionQueue.ConstructionYards.Count; i++)
            {
                if (ConstructionQueue.ConstructionYards[i].ShipUnderConstruction != null)
                {
                    result = false;
                }
            }
            return result;
        }

        private bool DetectHyperDeny(Galaxy galaxy)
        {
            GalaxyLocationList galaxyLocationList = _Galaxy.DetermineGalaxyLocationsAtPoint(Xpos, Ypos);
            for (int i = 0; i < galaxyLocationList.Count; i++)
            {
                GalaxyLocation galaxyLocation = galaxyLocationList[i];
                if (galaxyLocation.Effect == GalaxyLocationEffectType.HyperjumpDisabled)
                {
                    _HyperjumpDisabledLocation = true;
                    return true;
                }
            }
            _HyperjumpDisabledLocation = false;
            int num = 1200;
            num += Galaxy.MaxSolarSystemSize * 2;
            List<BuiltObject[]> builtObjectsAtLocationByArrays = _Galaxy.GetBuiltObjectsAtLocationByArrays(Xpos, Ypos, num);
            for (int j = 0; j < builtObjectsAtLocationByArrays.Count; j++)
            {
                int num2 = builtObjectsAtLocationByArrays[j].Length;
                for (int k = 0; k < num2; k++)
                {
                    BuiltObject builtObject = builtObjectsAtLocationByArrays[j][k];
                    if (builtObject != null && builtObject.HyperDenyActive && _Galaxy.CheckWithinDistancePotential(builtObject.WeaponHyperDenyRange, Xpos, Ypos, builtObject.Xpos, builtObject.Ypos))
                    {
                        double num3 = galaxy.CalculateDistance(Xpos, Ypos, builtObject.Xpos, builtObject.Ypos);
                        if ((double)builtObject.WeaponHyperDenyRange >= num3)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private BuiltObject CheckForHyperExitGravityWell(double x, double y)
        {
            List<BuiltObject[]> builtObjectsAtLocationByArrays = _Galaxy.GetBuiltObjectsAtLocationByArrays(x, y, 4000);
            for (int i = 0; i < builtObjectsAtLocationByArrays.Count; i++)
            {
                int num = builtObjectsAtLocationByArrays[i].Length;
                for (int j = 0; j < num; j++)
                {
                    BuiltObject builtObject = builtObjectsAtLocationByArrays[i][j];
                    if (builtObject == null || builtObject.HyperStopRange <= 0 || builtObject.Empire == null || builtObject.Empire == Empire)
                    {
                        continue;
                    }
                    bool flag = false;
                    if (builtObject.Empire.PirateEmpireBaseHabitat != null)
                    {
                        PirateRelation pirateRelation = Empire.ObtainPirateRelation(builtObject.Empire);
                        if (pirateRelation.Type == PirateRelationType.None)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        DiplomaticRelation diplomaticRelation = Empire.ObtainDiplomaticRelation(builtObject.Empire);
                        if (diplomaticRelation.Type == DiplomaticRelationType.War)
                        {
                            flag = true;
                        }
                    }
                    if (flag || (Empire != null && Empire.PirateEmpireBaseHabitat != null))
                    {
                        double num2 = _Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, x, y);
                        if (num2 < (double)builtObject.HyperStopRange)
                        {
                            return builtObject;
                        }
                    }
                }
            }
            return null;
        }

        private bool CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(DateTime time)
        {
            if (IsPlanetDestroyer && Weapons != null)
            {
                WeaponList allPlanetDestroyerWeapons = Weapons.GetAllPlanetDestroyerWeapons();
                for (int i = 0; i < allPlanetDestroyerWeapons.Count; i++)
                {
                    Weapon weapon = allPlanetDestroyerWeapons[i];
                    if (weapon != null)
                    {
                        int num = weapon.FireRate / 1000;
                        int seconds = num - 10;
                        DateTime dateTime = time.Subtract(new TimeSpan(0, 0, 0, seconds));
                        if (weapon.LastFired < dateTime)
                        {
                            weapon.LastFired = dateTime;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public bool CheckForHyperExitGravityWells(ref double hyperExitX, ref double hyperExitY)
        {
            BuiltObject builtObject = CheckForHyperExitGravityWell(hyperExitX, hyperExitY);
            if (builtObject != null)
            {
                double num = Galaxy.DetermineAngle(builtObject.Xpos, builtObject.Ypos, hyperExitX, hyperExitY);
                hyperExitX = builtObject.Xpos + (double)builtObject.HyperStopRange * Math.Cos(num);
                hyperExitY = builtObject.Ypos + (double)builtObject.HyperStopRange * Math.Sin(num);
                return true;
            }
            return false;
        }

        private void CheckForRandomAttackTargets()
        {
            if (Empire == null || SubRole == BuiltObjectSubRole.ResupplyShip || Empire.EmpiresToAttack == null || Empire.EmpiresToAttack.Count <= 0 || BuiltAt != null || (FirepowerRaw <= 0 && FighterCapacity <= 0))
            {
                return;
            }
            double num = (double)Galaxy.MaxSolarSystemSize * 2.0 + 500.0;
            double num2 = num * num;
            int x = (int)(Xpos / (double)Galaxy.IndexSize);
            int y = (int)(Ypos / (double)Galaxy.IndexSize);
            Galaxy.CorrectIndexCoords(ref x, ref y);
            BuiltObject[] array = ListHelper.ToArrayThreadSafe(_Galaxy.BuiltObjectIndex[x][y]);
            int num3 = (int)Xpos - Galaxy.MaxSolarSystemSize * 2 + 500;
            int num4 = (int)Xpos + Galaxy.MaxSolarSystemSize * 2 + 500;
            int num5 = (int)Ypos - Galaxy.MaxSolarSystemSize * 2 + 500;
            int num6 = (int)Ypos + Galaxy.MaxSolarSystemSize * 2 + 500;
            foreach (BuiltObject builtObject in array)
            {
                if (builtObject == null || !(builtObject.Xpos >= (double)num3) || !(builtObject.Xpos <= (double)num4) || !(builtObject.Ypos >= (double)num5) || !(builtObject.Ypos <= (double)num6) || builtObject.Role == BuiltObjectRole.Military || !Empire.EmpiresToAttack.Contains(builtObject.Empire))
                {
                    continue;
                }
                double num7 = _Galaxy.CalculateDistanceSquared(builtObject.Xpos, builtObject.Ypos, Xpos, Ypos);
                if (num7 <= num2 && (Mission == null || (Mission != null && (Mission.Priority == BuiltObjectMissionPriority.Undefined || Mission.Priority == BuiltObjectMissionPriority.Low))) && WithinFuelRangeAndRefuel(builtObject.Xpos, builtObject.Ypos, 0.1))
                {
                    BuiltObjectMissionType missionType = BuiltObjectMissionType.Attack;
                    if (Empire != null)
                    {
                        missionType = Empire.DetermineDestroyOrCaptureTarget(this, builtObject, attackingAsGroup: false);
                    }
                    AssignMission(missionType, builtObject, null, BuiltObjectMissionPriority.Normal);
                    Empire.EmpiresToAttack.Remove(builtObject.Empire);
                    break;
                }
            }
        }

        private void PirateBaseDiscovery()
        {
            if (Empire == null || Empire.PirateEmpireBaseHabitat == null || (SubRole != BuiltObjectSubRole.SmallSpacePort && SubRole != BuiltObjectSubRole.MediumSpacePort && SubRole != BuiltObjectSubRole.LargeSpacePort))
            {
                return;
            }
            int range = Galaxy.MaxSolarSystemSize * 2 + 500;
            BuiltObjectList builtObjectsAtLocation = _Galaxy.GetBuiltObjectsAtLocation(Xpos, Ypos, range);
            for (int i = 0; i < builtObjectsAtLocation.Count; i++)
            {
                BuiltObject builtObject = builtObjectsAtLocation[i];
                if (builtObject != null && builtObject.NearestSystemStar == NearestSystemStar && builtObject != this && builtObject.Empire != null && builtObject.Empire != _Galaxy.IndependentEmpire && !builtObject.Empire.KnownPirateBases.Contains(this))
                {
                    builtObject.Empire.KnownPirateBases.Add(this);
                }
            }
        }

        private void CheckForPirateBases()
        {
            if (Empire.PirateEmpireBaseHabitat != null || NearestSystemStar == null)
            {
                return;
            }
            int num = Galaxy.MaxSolarSystemSize * 2 + 500;
            if (SensorProximityArrayRange > num)
            {
                num = SensorProximityArrayRange;
            }
            double num2 = (double)num * (double)num;
            for (int i = 0; i < _Galaxy.PirateEmpires.Count; i++)
            {
                for (int j = 0; j < _Galaxy.PirateEmpires[i].BuiltObjects.Count; j++)
                {
                    if (_Galaxy.PirateEmpires[i].BuiltObjects[j].NearestSystemStar == NearestSystemStar && _Galaxy.PirateEmpires[i].BuiltObjects[j].Role == BuiltObjectRole.Base && !Empire.KnownPirateBases.Contains(_Galaxy.PirateEmpires[i].BuiltObjects[j]))
                    {
                        double num3 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, _Galaxy.PirateEmpires[i].BuiltObjects[j].Xpos, _Galaxy.PirateEmpires[i].BuiltObjects[j].Ypos);
                        if (num3 <= num2)
                        {
                            Empire.KnownPirateBases.Add(_Galaxy.PirateEmpires[i].BuiltObjects[j]);
                        }
                    }
                }
            }
        }

        private void ScanForLocations()
        {
            if (Empire == null || Empire == _Galaxy.IndependentEmpire)
            {
                return;
            }
            if (Empire.LocationHints.Count > 0)
            {
                lock (Empire.LocationHintLock)
                {
                    List<int> list = new List<int>();
                    for (int i = 0; i < Empire.LocationHints.Count; i++)
                    {
                        double num = 25000000.0;
                        if (NearestSystemStar != null)
                        {
                            num = 2116000000.0;
                        }
                        double num2 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, Empire.LocationHints[i].X, Empire.LocationHints[i].Y);
                        if (num2 < num)
                        {
                            list.Add(i);
                        }
                    }
                    if (list.Count > 0)
                    {
                        for (int num3 = list.Count - 1; num3 >= 0; num3--)
                        {
                            Empire.LocationHints.RemoveAt(list[num3]);
                        }
                    }
                }
            }
            GalaxyLocationList galaxyLocationList = _Galaxy.DetermineGalaxyLocationsInRangeAtPoint(Xpos, Ypos, 500.0, GalaxyLocationType.Undefined);
            for (int j = 0; j < galaxyLocationList.Count; j++)
            {
                GalaxyLocation galaxyLocation = galaxyLocationList[j];
                if ((CurrentSpeed > (float)TopSpeed && galaxyLocation.Type != GalaxyLocationType.NebulaCloud && galaxyLocation.Type != GalaxyLocationType.SuperNova && galaxyLocation.Type != GalaxyLocationType.RaceRegion) || Empire.KnownGalaxyLocations.Contains(galaxyLocation))
                {
                    continue;
                }
                double val = Math.Max(SensorProximityArrayRange, SensorLongRange);
                val = Math.Max(val, Galaxy.ThreatRange);
                double num4 = (double)galaxyLocation.Xpos - val;
                double num5 = (double)galaxyLocation.Xpos + ((double)galaxyLocation.Width + val);
                double num6 = (double)galaxyLocation.Ypos - val;
                double num7 = (double)galaxyLocation.Ypos + ((double)galaxyLocation.Height + val);
                if (!(Xpos > num4) || !(Xpos < num5) || !(Ypos > num6) || !(Ypos < num7))
                {
                    continue;
                }
                Empire.KnownGalaxyLocations.Add(galaxyLocation);
                Point point = new Point((int)(galaxyLocation.Xpos + galaxyLocation.Width / 2f), (int)(galaxyLocation.Ypos + galaxyLocation.Height / 2f));
                _ = string.Empty;
                Habitat habitat = _Galaxy.FastFindNearestSystem(galaxyLocation.Xpos, galaxyLocation.Ypos);
                string text = string.Empty;
                if (habitat != null)
                {
                    text = habitat.Name;
                }
                string empty = string.Empty;
                string empty2 = string.Empty;
                switch (galaxyLocation.Type)
                {
                    case GalaxyLocationType.RestrictedArea:
                        empty = galaxyLocation.Name;
                        empty2 = string.Format(TextResolver.GetText("Restricted Area Encounter"), galaxyLocation.Name, text);
                        empty2 += ".\n\n";
                        if (galaxyLocation.Effect == GalaxyLocationEffectType.HyperjumpDisabled)
                        {
                            empty2 += TextResolver.GetText("HyperjumpDisabledArea");
                        }
                        if (!string.IsNullOrEmpty(galaxyLocation.Message))
                        {
                            empty2 = empty2 + TextResolver.GetText("A broadcast message announces") + ": '";
                            empty2 += galaxyLocation.Message;
                            empty2 += "'";
                            empty2 += "\n\n";
                        }
                        empty2 = empty2 + TextResolver.GetText("Perhaps it would be wise to leave this area") + ".";
                        Empire.SendEventMessageToEmpire(EventMessageType.SpecialArea, empty, empty2, galaxyLocation, point);
                        break;
                    case GalaxyLocationType.DebrisField:
                        empty = TextResolver.GetText("Space Battle Debris Discovered");
                        empty2 = string.Format(TextResolver.GetText("Debris Field Encounter"), text);
                        empty2 += ". ";
                        empty2 += TextResolver.GetText("Debris Field Detail");
                        Empire.SendEventMessageToEmpire(EventMessageType.AncientBattleDebrisField, empty, empty2, galaxyLocation, point);
                        break;
                    case GalaxyLocationType.PlanetDestroyer:
                        empty2 = string.Format(TextResolver.GetText("Planet Destroyer Encounter"), text);
                        empty2 += ".\n\n";
                        empty2 += string.Format(TextResolver.GetText("Planet Destroyer Detail"), galaxyLocation.RelatedBuiltObject.Name);
                        Empire.SendEventMessageToEmpire(EventMessageType.FreeSuperShip, TextResolver.GetText("Secret Construction Project Discovered"), empty2, galaxyLocation.RelatedBuiltObject, galaxyLocation.RelatedBuiltObject);
                        break;
                }
            }
        }

        private void ScanArea(Galaxy galaxy)
        {
            if (SensorResourceProfileSensorRange <= 0)
            {
                return;
            }
            int sensorResourceProfileSensorRange = SensorResourceProfileSensorRange;
            sensorResourceProfileSensorRange += Galaxy.MaxSolarSystemSize * 2;
            HabitatList habitatsAtLocation = galaxy.GetHabitatsAtLocation(Xpos, Ypos, sensorResourceProfileSensorRange);
            double num = (double)SensorResourceProfileSensorRange * (double)SensorResourceProfileSensorRange;
            int num2 = (int)Xpos - SensorResourceProfileSensorRange;
            int num3 = (int)Xpos + SensorResourceProfileSensorRange;
            int num4 = (int)Ypos - SensorResourceProfileSensorRange;
            int num5 = (int)Ypos + SensorResourceProfileSensorRange;
            for (int i = 0; i < habitatsAtLocation.Count; i++)
            {
                if (!(habitatsAtLocation[i].Xpos >= (double)num2) || !(habitatsAtLocation[i].Xpos <= (double)num3) || !(habitatsAtLocation[i].Ypos >= (double)num4) || !(habitatsAtLocation[i].Ypos <= (double)num5))
                {
                    continue;
                }
                Habitat habitat = habitatsAtLocation[i];
                Empire empire = habitat.Empire;
                if (empire != null && empire != Empire)
                {
                    _Galaxy.DoEmpireEncounter(Empire, empire, habitat);
                }
                if (Empire.ResourceMap == null || Empire.ResourceMap.CheckResourcesKnown(habitat) || !(galaxy.CalculateDistanceSquared(habitat.Xpos, habitat.Ypos, Xpos, Ypos) <= num))
                {
                    continue;
                }
                BaconBuiltObject.AddScientificData(this, habitat, "scanArea");
                Empire.ResourceMap.SetResourcesKnown(habitat, known: true);
                ScanHabitatIndex = habitat.HabitatIndex;
                LastScanTime = _Galaxy.CurrentStarDate;
                if (habitat.Empire == null || habitat.Empire == _Galaxy.IndependentEmpire)
                {
                    foreach (HabitatResource resource in habitat.Resources)
                    {
                        if (resource.IsRestrictedResource)
                        {
                            Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                            string text = string.Format(TextResolver.GetText("Valuable Discovery ENVIRONMENT PLANETTYPE NAME SYSTEM"), Galaxy.ResolveDescription(habitat.Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture), habitat.Name, habitat2.Name);
                            text += ".\n\n";
                            text = resource.Name.ToLower(CultureInfo.InvariantCulture) switch
                            {
                                "korabbian spice" => text + string.Format(TextResolver.GetText("Restricted Resource Discovery - Korabbian Spice"), Galaxy.ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture)),
                                "zentabia fluid" => text + string.Format(TextResolver.GetText("Restricted Resource Discovery - Zentabia Fluid"), Galaxy.ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture)),
                                "loros fruit" => text + string.Format(TextResolver.GetText("Restricted Resource Discovery - Loros Fruit"), Galaxy.ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture)),
                                _ => text + string.Format(TextResolver.GetText("Restricted Resource Discovery - General"), Galaxy.ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture), resource.Name),
                            } + "\n\n";
                            text += TextResolver.GetText("Restricted Resource Benefits");
                            string title = string.Format(TextResolver.GetText("X Discovered"), resource.Name);
                            Empire.SendEventMessageToEmpire(EventMessageType.RestrictedResourceDiscovered, title, text, resource, habitat);
                        }
                    }
                }
                if (empire == _Galaxy.IndependentEmpire)
                {
                    if (habitat.Population != null && habitat.Population.DominantRace != null)
                    {
                        Race dominantRace = habitat.Population.DominantRace;
                        string message = _Galaxy.GenerateIndependentColonyReport(Empire, habitat, dominantRace);
                        string text2 = TextResolver.GetText("Independent Colony Discovered");
                        Empire.SendEventMessageToEmpire(EventMessageType.IndependentPopulation, text2, message, dominantRace, habitat);
                    }
                }
                else
                {
                    if (Galaxy.Rnd.Next(0, 800) != 1 || (habitat.Category != HabitatCategoryType.Planet && habitat.Category != HabitatCategoryType.Moon) || habitat.Empire != null)
                    {
                        continue;
                    }
                    bool flag = false;
                    Habitat habitat3 = Galaxy.DetermineHabitatSystemStar(habitat);
                    if (habitat3 != null)
                    {
                        SystemInfo systemInfo = _Galaxy.Systems[habitat3];
                        if (systemInfo != null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire != Empire)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        continue;
                    }
                    int num6 = 1 + Empire.Colonies.Count / 6;
                    List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                    list.Add(BuiltObjectSubRole.Cruiser);
                    list.Add(BuiltObjectSubRole.CapitalShip);
                    BuiltObjectList builtObjectsBySubRole = Empire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                    if (builtObjectsBySubRole.Count < num6)
                    {
                        DesignSpecification designSpecification = null;
                        string text3 = _Galaxy.SelectRandomUniqueMilitaryShipName(habitat);
                        switch (Galaxy.Rnd.Next(0, 5))
                        {
                            case 0:
                            case 1:
                                designSpecification = Empire.ObtainDesignSpec(BuiltObjectSubRole.Cruiser);
                                break;
                            case 2:
                                designSpecification = Empire.ObtainDesignSpec(BuiltObjectSubRole.CapitalShip);
                                break;
                            case 3:
                            case 4:
                                designSpecification = Empire.ObtainDesignSpec(BuiltObjectSubRole.ColonyShip);
                                text3 = _Galaxy.SelectRandomUniqueStandardShipName(habitat);
                                break;
                        }
                        int pictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.FreedomAllianceFamily, designSpecification.SubRole, aged: true);
                        Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat);
                        string text4 = string.Format(TextResolver.GetText("Strange Discovery ENVIRONMENT PLANETTYPE NAME SYSTEM"), Galaxy.ResolveDescription(habitat.Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture), habitat.Name, habitat4.Name);
                        text4 += ".\n\n";
                        switch (habitat.Type)
                        {
                            case HabitatType.GasGiant:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Gas Giant"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                            case HabitatType.FrozenGasGiant:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Frozen Gas Giant"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                            case HabitatType.BarrenRock:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Barren Rock"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                            case HabitatType.Continental:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Continental"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                            case HabitatType.Ice:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Ice"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                            case HabitatType.MarshySwamp:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Marshy Swamp"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                            case HabitatType.Ocean:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Ocean"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                            case HabitatType.Desert:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Desert"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                            case HabitatType.Volcanic:
                                text4 += string.Format(TextResolver.GetText("Deserted Ship Volcanic"), Galaxy.ResolveDescription(designSpecification.SubRole), text3);
                                pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(designSpecification.SubRole, largeShips: true);
                                break;
                        }
                        if (designSpecification.SubRole == BuiltObjectSubRole.ColonyShip)
                        {
                            pictureRef = ShipImageHelper.ResolveMinorShipImageIndex(BuiltObjectSubRole.ColonyShip, largeShips: true);
                        }
                        Design design = Empire.GenerateDesignFromSpec(designSpecification, 3.0);
                        design.PictureRef = pictureRef;
                        design.BuildCount++;
                        BuiltObject builtObject = Empire.GenerateBuiltObjectFromDesign(design, text3, isState: true, habitat.Xpos, habitat.Ypos);
                        design.IsObsolete = true;
                        builtObject.ParentHabitat = habitat;
                        builtObject.DateBuilt = _Galaxy.CurrentStarDate;
                        builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                        _Galaxy.SelectRelativeParkingPoint(habitat.Diameter / 2, out var x, out var y);
                        builtObject.ParentOffsetX = x;
                        builtObject.ParentOffsetY = y;
                        builtObject.Heading = _Galaxy.SelectRandomHeading();
                        builtObject.TargetHeading = builtObject.Heading;
                        builtObject.SupportCostFactor = 0.5f;
                        if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip && builtObject.NativeRace != null)
                        {
                            text4 += "\n\n";
                            text4 += string.Format(TextResolver.GetText("Colony Ship Race"), builtObject.NativeRace.Name);
                            text4 += ".\n\n";
                            text4 += _Galaxy.GenerateRaceReport(builtObject.NativeRace);
                        }
                        double num7 = Galaxy.ResolveTechBonusFactor(Empire, _Galaxy, builtObject);
                        if (num7 > 1.0)
                        {
                            text4 += "\n\n";
                            text4 += TextResolver.GetText("Disassembling the advanced technology in this ship");
                        }
                        string text5 = TextResolver.GetText("Deserted Ship Discovered");
                        Empire.SendEventMessageToEmpire(EventMessageType.FreeSuperShip, text5, text4, habitat, builtObject);
                    }
                }
            }
        }

        private void RechargeShields(double timePassed)
        {
            if (CurrentShields < (float)ShieldsCapacity)
            {
                double num = ShieldRechargeRate;
                if (ShipGroup != null)
                {
                    num *= ShipGroup.ShieldRechargeRateBonus;
                }
                num *= CaptainShieldRechargeRateBonus;
                double num2 = Math.Min(num * timePassed, (double)ShieldsCapacity - (double)CurrentShields);
                if (num2 > CurrentEnergy)
                {
                    num2 = CurrentEnergy;
                }
                if (num2 < 0.0)
                {
                    num2 = 0.0;
                }
                double num3 = num2;
                if (ShipGroup != null)
                {
                    num3 /= ShipGroup.ShipEnergyUsageBonus;
                }
                num3 /= CaptainShipEnergyUsageBonus;
                CurrentEnergy -= num3;
                CurrentShields += (float)num2;
            }
            if (CurrentShields < 0f)
            {
                CurrentShields = 0f;
            }
            if (CurrentShields > (float)ShieldsCapacity)
            {
                CurrentShields = ShieldsCapacity;
            }
        }

        public int CalculateShieldStrengthFactor()
        {
            return (int)(CurrentShields / 20f);
        }

        public int CalculateOverallStrengthFactor()
        {
            int num = CalculateShieldStrengthFactor();
            int num2 = CalculateFirepowerFactor();
            int num3 = CalculateFighterFactor();
            return num + num2 + num3;
        }

        public int CalculateOverallStrengthFactorWithoutShields()
        {
            return BaconBuiltObject.CalculateOverallStrengthFactorWithoutShields(this);
        }

        public int CalculateFighterFactor()
        {
            int num = 0;
            if (Fighters != null)
            {
                for (int i = 0; i < Fighters.Count; i++)
                {
                    Fighter fighter = Fighters[i];
                    if (fighter != null && !fighter.HasBeenDestroyed && !fighter.UnderConstruction)
                    {
                        num += fighter.FirepowerRaw;
                    }
                }
            }
            return num;
        }

        public int CalculateFirepowerFactor()
        {
            float num = 0f;
            if (Weapons != null)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapon weapon = Weapons[i];
                    if (weapon != null)
                    {
                        num += (float)weapon.RawDamage / ((float)weapon.FireRate / 1000f);
                    }
                }
            }
            return (int)num;
        }

        public double ReducedRange(double fuelReservePortion)
        {
            double num = FuelUnitPerEnergyUnit();
            double num2 = 0.0;
            if (WarpSpeed > 0)
            {
                return Math.Max(0.0, CurrentFuel - (double)FuelCapacity * fuelReservePortion) / (((double)WarpSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)WarpSpeedWithBonuses;
            }
            return Math.Max(0.0, CurrentFuel - (double)FuelCapacity * fuelReservePortion) / (((double)CruiseSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)CruiseSpeed;
        }

        public bool WithinReducedFuelRange(double destinationX, double destinationY, double fuelReservePortion)
        {
            double num = ReducedRange(fuelReservePortion);
            double num2 = num * num;
            double num3 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, destinationX, destinationY);
            if (num3 <= num2)
            {
                return true;
            }
            return false;
        }

        public double FuelUnitPerEnergyUnit()
        {
            return (double)ReactorCycleFuelConsumption / 1000.0 / ((double)ReactorStorageCapacity + 1.0);
        }

        public double CurrentRange()
        {
            return CurrentRange(0.0);
        }

        public double CurrentRange(double fuelPortionMargin)
        {
            double num = FuelUnitPerEnergyUnit();
            double currentFuel = CurrentFuel;
            currentFuel -= (double)FuelCapacity * fuelPortionMargin;
            currentFuel = Math.Max(0.0, currentFuel);
            double num2 = 0.0;
            if (WarpSpeed > 0)
            {
                return currentFuel / (((double)WarpSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)WarpSpeedWithBonuses;
            }
            return currentFuel / (((double)CruiseSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)CruiseSpeed;
        }

        public double MaximumFuelRange()
        {
            double num = FuelUnitPerEnergyUnit();
            double num2 = 0.0;
            if (WarpSpeed > 0)
            {
                return (double)FuelCapacity / (((double)WarpSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)WarpSpeedWithBonuses;
            }
            return (double)FuelCapacity / (((double)CruiseSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)CruiseSpeed;
        }

        public bool WithinFuelRange(double destinationX, double destinationY, double fuelPortionMargin)
        {
            double rangeFactor = 0.0;
            return WithinFuelRange(destinationX, destinationY, fuelPortionMargin, out rangeFactor);
        }

        public bool WithinFuelRange(double destinationX, double destinationY, double fuelPortionMargin, out double rangeFactor)
        {
            double num = CurrentRange(fuelPortionMargin);
            double num2 = num * num;
            double num3 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, destinationX, destinationY);
            rangeFactor = num3 / num2;
            if (num3 <= num2)
            {
                return true;
            }
            return false;
        }

        public bool WithinFuelRangeSupplyingCurrentRange(double destinationX, double destinationY, double currentRange, double fuelPortionMargin)
        {
            currentRange -= currentRange * fuelPortionMargin;
            double num = currentRange * currentRange;
            double num2 = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, destinationX, destinationY);
            if (num2 <= num)
            {
                return true;
            }
            return false;
        }

        public double CalculateFuelPortionMarginFromNearbyRefuellingPoints(double x, double y)
        {
            StellarObject refuellingLocation = null;
            return CalculateFuelPortionMarginFromNearbyRefuellingPointsInformLocation(x, y, out refuellingLocation);
        }

        public double CalculateFuelPortionMarginFromNearbyRefuellingPointsInformLocation(double x, double y, out StellarObject refuellingLocation)
        {
            ResourceList resourceList = new ResourceList();
            Resource fuelType = FuelType;
            if (fuelType != null)
            {
                Resource resource = new Resource(fuelType.ResourceID);
                double num = (resource.SortTag = (double)FuelCapacity - CurrentFuel);
                resourceList.Add(resource);
            }
            bool includeResupplyShips = false;
            if (Role == BuiltObjectRole.Military)
            {
                includeResupplyShips = true;
            }
            if (Empire != null)
            {
                refuellingLocation = Empire.UltraFastFindNearestRefuellingLocation(x, y, resourceList, this, mustHaveActualSupply: false, includeResupplyShips);
            }
            else
            {
                refuellingLocation = _Galaxy.FastFindNearestRefuellingPoint(x, y, resourceList, Empire, this, includeResupplyShips, null);
            }
            if (CheckRefuelLocationRangeAcceptable(refuellingLocation))
            {
                return CalculateFuelPortionMarginFromNearbyRefuellingPoints(x, y, refuellingLocation);
            }
            return 0.0;
        }

        public double CalculateFuelPortionMarginFromNearbyRefuellingPoints(double x, double y, StellarObject refuellingPoint)
        {
            double num = 0.0;
            if (refuellingPoint != null)
            {
                num = _Galaxy.CalculateDistance(x, y, refuellingPoint.Xpos, refuellingPoint.Ypos);
            }
            double num2 = MaximumFuelRange();
            return num / num2;
        }

        public double CalculateRefuellingPortion(StellarObject refuellingLocation)
        {
            double val = CalculateFuelPortionMarginFromNearbyRefuellingPoints(Xpos, Ypos, refuellingLocation);
            if (IsAutoControlled)
            {
                return Math.Max(0.05, val);
            }
            val = Math.Max(0.05, val);
            return Math.Min(0.5, val);
        }

        public double CalculateRefuellingPortion()
        {
            StellarObject refuellingLocation = null;
            return CalculateRefuellingPortion(out refuellingLocation);
        }

        public double CalculateRefuellingPortion(out StellarObject refuellingLocation)
        {
            double val = CalculateFuelPortionMarginFromNearbyRefuellingPointsInformLocation(Xpos, Ypos, out refuellingLocation);
            if (IsAutoControlled)
            {
                return Math.Max(0.05, val);
            }
            val = Math.Max(0.05, val);
            return Math.Min(0.5, val);
        }

        public bool WithinFuelRangeAndRefuel(double destinationX, double destinationY, double extraFuelPortionMargin)
        {
            double currentRange = CurrentRange();
            if (WithinFuelRangeSupplyingCurrentRange(destinationX, destinationY, currentRange, extraFuelPortionMargin))
            {
                double num = CalculateFuelPortionMarginFromNearbyRefuellingPoints(destinationX, destinationY);
                return WithinFuelRangeSupplyingCurrentRange(destinationX, destinationY, currentRange, num + extraFuelPortionMargin);
            }
            return false;
        }

        public bool WithinFuelRangeAndRefuel(double destinationX, double destinationY, double extraFuelPortionMargin, StellarObject refuellingPoint)
        {
            double num = CalculateFuelPortionMarginFromNearbyRefuellingPoints(destinationX, destinationY, refuellingPoint);
            return WithinFuelRange(destinationX, destinationY, num + extraFuelPortionMargin);
        }

        public bool DistanceWithinRange(double startX, double startY, double endX, double endY, double extraFuelPortionMargin)
        {
            double num = MaximumFuelRange();
            num -= num * extraFuelPortionMargin;
            num *= num;
            double num2 = _Galaxy.CalculateDistanceSquared(startX, startY, endX, endY);
            if (num2 <= num)
            {
                return true;
            }
            return false;
        }

        private void RechargeReactors(double timePassed)
        {
            if (CurrentEnergy < (double)ReactorStorageCapacity)
            {
                double num = FuelUnitPerEnergyUnit();
                double num2 = Math.Min((double)ReactorPowerOutput * timePassed, (double)ReactorStorageCapacity - CurrentEnergy);
                double num3 = num2 * num;
                if (num3 > CurrentFuel)
                {
                    num3 = CurrentFuel;
                    num2 = num3 / num;
                }
                CurrentFuel -= num3;
                CurrentEnergy += num2;
            }
        }

        private bool DetectBattleStalemate()
        {
            return false;
        }

        private void CheckLaunchAssaultPodsAtTarget(DateTime time, Habitat target)
        {
            bool flag = false;
            if (Mission != null && Mission.Type == BuiltObjectMissionType.Raid && target.Population != null && target.Population.Count > 0 && target.Empire != Empire)
            {
                flag = true;
            }
            if (!flag || AssaultAttackValue > 0 || target == null || target.HasBeenDestroyed || target.Empire == Empire || target.PlanetaryShieldPresent)
            {
                return;
            }
            double num = _Galaxy.CalculateDistance(Xpos, Ypos, target.Xpos, target.Ypos);
            if (!(num < (double)(float)AssaultRange) || Weapons == null)
            {
                return;
            }
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapon weapon = Weapons[i];
                if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod && (double)weapon.Range >= num && weapon.IsAvailable(this, time) && Galaxy.Rnd.Next(0, 5) == 1)
                {
                    weapon.Fire(_Galaxy, this, target, num, time, willHit: true, 1.0);
                    weapon.X += Galaxy.Rnd.NextDouble() * 20.0 - 10.0;
                    weapon.Y += Galaxy.Rnd.NextDouble() * 20.0 - 10.0;
                    if (target.Empire != null && target.Empire != _Galaxy.IndependentEmpire && target.Empire != Empire && target.Attackers != null && !target.Attackers.Contains(this))
                    {
                        ModifyDiplomacyFromAttack(target.Empire, attackAffectsRelationship: true, attackAffectsReputation: false, 2, 2.0);
                    }
                    if (target.Attackers != null && !target.Attackers.Contains(this))
                    {
                        target.Attackers.Add(this);
                    }
                }
            }
        }

        private void CheckLaunchAssaultPodsAtTarget(DateTime time, BuiltObject target)
        {
            bool flag = false;
            bool assaultIsRaid = false;
            if (Mission != null)
            {
                if (Mission.Type == BuiltObjectMissionType.Capture)
                {
                    flag = true;
                }
                else if (Mission.Type == BuiltObjectMissionType.Raid)
                {
                    flag = true;
                    assaultIsRaid = true;
                }
            }
            if (Role == BuiltObjectRole.Base && target.Role != BuiltObjectRole.Base)
            {
                flag = true;
            }
            if (target.Empire == null)
            {
                flag = false;
            }
            if (!flag || AssaultAttackValue > 0 || target == null || target.HasBeenDestroyed || target.Empire == Empire || !(target.CurrentShields < (float)AssaultShieldPenetration))
            {
                return;
            }
            double num = _Galaxy.CalculateDistance(Xpos, Ypos, target.Xpos, target.Ypos);
            if (!(num < (double)(float)AssaultRange) || Weapons == null)
            {
                return;
            }
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapon weapon = Weapons[i];
                if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod && (double)weapon.Range >= num && weapon.IsAvailable(this, time) && Galaxy.Rnd.Next(0, 5) == 1)
                {
                    weapon.Fire(_Galaxy, this, target, num, time, willHit: true, 1.0);
                    weapon.X += Galaxy.Rnd.NextDouble() * 20.0 - 10.0;
                    weapon.Y += Galaxy.Rnd.NextDouble() * 20.0 - 10.0;
                    ModifyDiplomacyFromAttack(target);
                    if (target.Attackers != null && !target.Attackers.Contains(this))
                    {
                        target.Attackers.Add(this);
                    }
                    if (target.AssaultAttackValue == 0)
                    {
                        target.AssaultIsRaid = assaultIsRaid;
                    }
                    if (AssaultAttackValue <= 0)
                    {
                        int fixedDefenseValue = 0;
                        AssaultDefenseValue = (short)CalculateBoardingDefenseValue(time, out fixedDefenseValue);
                    }
                }
            }
        }

        private void HandleAssaultPodMovement(double timePassed)
        {
            if (Weapons == null)
            {
                return;
            }
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapon weapon = Weapons[i];
                if (weapon == null || weapon.Component == null || weapon.Component.Type != ComponentType.AssaultPod)
                {
                    continue;
                }
                StellarObject target = weapon.Target;
                if (!(weapon.DistanceTravelled >= 0f) || target == null || target.HasBeenDestroyed)
                {
                    continue;
                }
                float distanceFromTarget = weapon.DistanceFromTarget;
                double num = Galaxy.DetermineAngle(weapon.X, weapon.Y, target.Xpos, target.Ypos);
                weapon.X += Math.Cos(num) * (double)weapon.Speed * timePassed;
                weapon.Y += Math.Sin(num) * (double)weapon.Speed * timePassed;
                float num2 = (weapon.DistanceFromTarget = (float)_Galaxy.CalculateDistance(weapon.X, weapon.Y, target.Xpos, target.Ypos));
                weapon.Heading = (float)num;
                if (!(distanceFromTarget + 1f < num2) && !((double)num2 < 10.0))
                {
                    continue;
                }
                if (target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)target;
                    double num3 = 1.0;
                    double num4 = 1.0;
                    if (Empire != null)
                    {
                        num4 = (double)Empire.BoardingAttackFactor * BaconBuiltObject.AssaultPodStrengthMultiplier(this);
                        num4 *= Empire.RaidStrengthFactor;
                        if (Empire.DominantRace != null)
                        {
                            num3 = (double)Empire.DominantRace.TroopStrength / 100.0;
                        }
                    }
                    if (builtObject.Empire == Empire)
                    {
                        builtObject.AssaultDefenseValue += (short)((double)weapon.RawDamage * num3 * num4);
                    }
                    else
                    {
                        if (Empire != null && builtObject.AssaultAttackValue == 0)
                        {
                            builtObject.AssaultAttackEmpireId = (byte)Empire.EmpireId;
                        }
                        if (builtObject.AssaultIsRaid)
                        {
                            _Galaxy.DoCharacterEvent(CharacterEventType.Raid, builtObject, Characters);
                        }
                        else
                        {
                            _Galaxy.DoCharacterEvent(CharacterEventType.Boarding, builtObject, Characters);
                        }
                        short num5 = (short)((double)weapon.RawDamage * num3 * num4);
                        if (SensorTraceScannerPower > 0)
                        {
                            num5 = (short)((double)num5 * (1.0 + (double)SensorTraceScannerPower / 100.0));
                        }
                        if (Characters != null && Characters.Count > 0)
                        {
                            double num6 = 1.0 + 0.01 * (double)Characters.GetHighestSkillLevel(CharacterSkillType.BoardingAssault);
                            num5 = (short)((double)num5 * num6);
                        }
                        builtObject.AssaultAttackValue += num5;
                    }
                }
                else if (target is Habitat)
                {
                    Habitat habitat = (Habitat)target;
                    bool flag = true;
                    if (habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0)
                    {
                        Troop[] array = ListHelper.ToArrayThreadSafe(habitat.InvadingTroops);
                        bool flag2 = false;
                        foreach (Troop troop in array)
                        {
                            if (troop != null && troop.Empire != null && troop.Type != TroopType.PirateRaider && troop.Empire == Empire)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                        if (flag2)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        double num7 = 1.0;
                        double num8 = 1.0;
                        if (Empire != null)
                        {
                            num8 = Empire.RaidStrengthFactor;
                            if (Empire.DominantRace != null)
                            {
                                num7 = (double)Empire.DominantRace.TroopStrength / 100.0 * BaconBuiltObject.AssaultPodStrengthMultiplier(this);
                            }
                        }
                        int attackStrength = (int)((double)weapon.RawDamage * 1.0 * num7 * num8);
                        _Galaxy.DoCharacterEvent(CharacterEventType.Raid, habitat, Characters);
                        _Galaxy.DoCharacterEvent(CharacterEventType.Raid, habitat, habitat.Characters);
                        PerformRaidColonyInvasion(habitat, attackStrength);
                    }
                }
                weapon.Reset();
            }
        }

        private void PerformRaidColonyInvasion(Habitat targetColony, int attackStrength)
        {
            if (targetColony == null || targetColony.Owner == null || Empire == null || Empire.DominantRace == null)
            {
                return;
            }
            _Galaxy.NotifyOfAttack(this, Empire, targetColony, bombarded: false, isNewAttack: true, notifyIndependent: true);
            targetColony.Owner.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstPirateRaid, targetColony, Empire);
            string name = Empire.GenerateTroopDescription(string.Format(TextResolver.GetText("RACE Pirate Raider"), Empire.DominantRace.Name));
            Troop troop = new Troop(name, TroopType.PirateRaider, attackStrength, attackStrength, 100, 100f, Empire, Empire.DominantRace);
            if (troop == null)
            {
                return;
            }
            if (Empire.DominantRace != null)
            {
                troop.PictureRef = Empire.DominantRace.PictureRef;
            }
            Empire.Troops.Add(troop);
            targetColony.PiratesDefendAgainstRaid(Empire);
            List<object> list = new List<object>();
            int num = 0;
            int num2 = 0;
            if (targetColony.BasesAtHabitat != null && targetColony.BasesAtHabitat.Count > 0)
            {
                for (int i = 0; i < targetColony.BasesAtHabitat.Count; i++)
                {
                    if (targetColony.BasesAtHabitat[i].FirepowerRaw > 0)
                    {
                        num += targetColony.BasesAtHabitat[i].FirepowerRaw;
                        list.Add(targetColony.BasesAtHabitat[i]);
                        num2++;
                    }
                }
            }
            if (targetColony.PlanetaryShieldPresent)
            {
                num += 1000;
                num2++;
            }
            int num3 = 0;
            int num4 = 0;
            int num5 = num;
            TroopList byType = targetColony.Troops.GetByType(TroopType.Artillery);
            num3 = byType.TotalDefendStrength;
            if (byType.Count > 0)
            {
                if (targetColony.Empire != null)
                {
                    num3 = (int)((float)num3 * targetColony.Empire.TroopAttackStrengthBonusFactorArtillery);
                    num4 = (int)((float)num3 * targetColony.Empire.TroopPlanetaryDefenseInterceptBonusFactor);
                }
                num += num3 / 50;
                list.AddRange(byType);
                num2 += byType.Count;
            }
            num = Math.Min(3000, num);
            num2 = Math.Min(10, num2);
            double val = Math.Sqrt(num) * Math.Sqrt(num2);
            val = Math.Min(90.0, val);
            int num6 = num4 / 50 + num5;
            double val2 = Math.Sqrt(num6) * Math.Sqrt(num2);
            val2 = Math.Min(95.0, val2);
            if (Empire != null && (targetColony.InvadingTroops == null || targetColony.InvadingTroops.Count <= 0))
            {
                PirateColonyControlList pirateControl = targetColony.GetPirateControl();
                for (int j = 0; j < pirateControl.Count; j++)
                {
                    PirateColonyControl pirateColonyControl = pirateControl[j];
                    if (pirateColonyControl != null && pirateColonyControl.ControlLevel >= 0.5f && pirateColonyControl.EmpireId != Empire.EmpireId)
                    {
                        Empire empireById = _Galaxy.GetEmpireById(pirateColonyControl.EmpireId);
                        if (empireById != null)
                        {
                            float num7 = -5f;
                            num7 -= (pirateColonyControl.ControlLevel - 0.5f) * 10f;
                            empireById.ChangePirateEvaluation(Empire, num7, PirateRelationEvaluationType.RaidsAgainstOurColonies);
                        }
                    }
                }
                if (targetColony.Empire != null && targetColony.Empire != _Galaxy.IndependentEmpire && targetColony.Empire.PirateRelations != null)
                {
                    for (int k = 0; k < targetColony.Empire.PirateRelations.Count; k++)
                    {
                        PirateRelation pirateRelation = targetColony.Empire.PirateRelations[k];
                        if (pirateRelation != null && pirateRelation.OtherEmpire != null && pirateRelation.Type == PirateRelationType.Protection && pirateRelation.Evaluation >= 5f && pirateRelation.OtherEmpire != Empire)
                        {
                            pirateRelation.OtherEmpire.ChangePirateEvaluation(Empire, -5f, PirateRelationEvaluationType.RaidsAgainstOurColonies);
                        }
                    }
                    targetColony.Empire.ChangePirateEvaluation(Empire, -10f, PirateRelationEvaluationType.RaidsAgainstOurColonies);
                }
            }
            troop.Colony = targetColony;
            if (targetColony.InvadingTroops == null)
            {
                targetColony.InvadingTroops = new TroopList();
            }
            targetColony.InvadingTroops.Add(troop);
            if (targetColony.ColonyInvasion != null)
            {
                targetColony.ColonyInvasion.AddInvaderLanding(troop);
            }
            if (Galaxy.Rnd.NextDouble() * 100.0 < val2)
            {
                double val3 = val * Galaxy.Rnd.NextDouble();
                val3 = Math.Min(troop.Readiness * 0.9f, val3);
                troop.Readiness -= (float)val3;
                if (targetColony.InvasionStats == null)
                {
                    targetColony.InvasionStats = new InvasionStats(targetColony, Empire, targetColony.Empire);
                }
                if (targetColony.InvasionStats != null)
                {
                    targetColony.InvasionStats.TroopsDamageToInvaders += (float)val3;
                }
                if (targetColony.ColonyInvasion != null)
                {
                    object firer = null;
                    if (list != null && list.Count > 0)
                    {
                        firer = list[Galaxy.Rnd.Next(0, list.Count)];
                    }
                    targetColony.ColonyInvasion.AddInvaderLandingExplosion(troop, firer, Galaxy.Rnd);
                }
            }
            if (Characters != null)
            {
                foreach (Character character in Characters)
                {
                    if (character.Role == CharacterRole.TroopGeneral)
                    {
                        character.CompleteLocationTransfer(targetColony, _Galaxy, invadingDestination: true);
                        if (targetColony.ColonyInvasion != null)
                        {
                            targetColony.ColonyInvasion.AddInvaderLanding(character);
                        }
                    }
                }
            }
            if (targetColony.Empire != _Galaxy.IndependentEmpire && targetColony.Empire != Empire)
            {
                double num8 = Math.Max(1.0, Math.Min(4.0, (double)targetColony.StrategicValue / 250000.0));
                int evaluationImpact = Math.Min(20, Math.Max(2, (int)((double)troop.AttackStrength / 30.0 * num8)));
                ModifyDiplomacyFromAttack(targetColony.Empire, evaluationImpact);
                if ((targetColony.StrategicValue > 50000 || (targetColony.Empire != null && targetColony.Empire.Capitals != null && targetColony.Empire.Capitals.Contains(targetColony))) && Empire != null && Empire.PirateEmpireBaseHabitat == null && targetColony.Empire.PirateEmpireBaseHabitat == null && targetColony.Empire.ControlDiplomacyOffense == AutomationLevel.FullyAutomated)
                {
                    targetColony.Empire.DeclareWar(Empire);
                }
            }
        }

        private void UpdateRaidCountdown(double timePassed)
        {
            if (RaidCountdown > 0)
            {
                int num = (int)(timePassed / 10.0);
                int val = RaidCountdown - num;
                val = Math.Min(255, Math.Max(0, val));
                RaidCountdown = (byte)val;
            }
        }

        private void FireAtAssaultPods(DateTime time, bool inView)
        {
            if (PointDefenseWeaponsRange <= 0)
            {
                return;
            }
            if (_AssaultPodFiringCounter >= 32766)
            {
                _AssaultPodFiringCounter = 0;
            }
            _AssaultPodFiringCounter++;
            if (inView && _AssaultPodFiringCounter % 5 != 0)
            {
                return;
            }
            for (int i = 0; i < Attackers.Count; i++)
            {
                StellarObject stellarObject = Attackers[i];
                if (stellarObject == null || !(stellarObject is BuiltObject))
                {
                    continue;
                }
                BuiltObject builtObject = (BuiltObject)stellarObject;
                if (builtObject.AssaultStrength <= 0 || builtObject.Weapons == null)
                {
                    continue;
                }
                for (int j = 0; j < builtObject.Weapons.Count; j++)
                {
                    Weapon weapon = builtObject.Weapons[j];
                    if (weapon == null || weapon.Component == null || weapon.Component.Type != ComponentType.AssaultPod || !(weapon.DistanceTravelled >= 0f) || weapon.Target == null || weapon.Target != this || !(weapon.DistanceFromTarget < (float)PointDefenseWeaponsRange) || Weapons == null)
                    {
                        continue;
                    }
                    for (int k = 0; k < Weapons.Count; k++)
                    {
                        Weapon weapon2 = Weapons[k];
                        if (weapon2 != null && weapon2.Component != null && weapon2.Component.Type == ComponentType.WeaponPointDefense && weapon.DistanceFromTarget <= (float)weapon2.Range && weapon2.IsAvailable(this, time))
                        {
                            double hitRangeChance = 0.0;
                            bool willHit = DetermineHitTarget(_Galaxy, weapon2, weapon, weapon.DistanceFromTarget, out hitRangeChance);
                            weapon2.Fire(_Galaxy, this, weapon, weapon.DistanceFromTarget, time, willHit, hitRangeChance);
                            break;
                        }
                    }
                }
            }
        }

        private void ProcessBoardingAssault(DateTime time, double timePassed)
        {
            if (AssaultAttackValue > 0)
            {
                if (AssaultDefenseValue == 0)
                {
                    int fixedDefenseValue = 0;
                    AssaultDefenseValue = (short)CalculateBoardingDefenseValue(time, out fixedDefenseValue);
                }
                double num = Math.Max(0.5, Math.Min(2.0, (double)AssaultAttackValue / (double)AssaultDefenseValue));
                double num2 = timePassed * (2.0 + Galaxy.Rnd.NextDouble() * 2.0) / num;
                double num3 = timePassed * (2.0 + Galaxy.Rnd.NextDouble() * 2.0) * num;
                AssaultAttackValue = Math.Max((short)0, (short)((double)AssaultAttackValue - num2));
                AssaultDefenseValue = Math.Max((short)0, (short)((double)AssaultDefenseValue - num3));
                if (num2 + num3 > Galaxy.Rnd.NextDouble() * 10.0 * timePassed)
                {
                    short durationInMilliseconds = (short)Galaxy.Rnd.Next(20000, 30000);
                    DisableRandomComponent(durationInMilliseconds);
                }
                if (AssaultAttackValue == 0)
                {
                    int fixedDefenseValue2 = 0;
                    AssaultDefenseValue = (short)CalculateBoardingDefenseValue(time, out fixedDefenseValue2);
                    AssaultAttackEmpireId = 0;
                    AssaultIsRaid = false;
                }
                else
                {
                    if (AssaultDefenseValue != 0)
                    {
                        return;
                    }
                    Empire empireById = _Galaxy.GetEmpireById(AssaultAttackEmpireId);
                    if (empireById == null || !empireById.Active)
                    {
                        return;
                    }
                    if (AssaultIsRaid)
                    {
                        double raidBonusFactor = empireById.RaidBonusFactor;
                        _Galaxy.DoRaidBonuses(empireById, this, raidBonusFactor);
                        RaidCountdown = 60;
                        AssaultAttackValue = 0;
                        AssaultAttackEmpireId = 0;
                        AssaultIsRaid = false;
                        Empire actualEmpire = ActualEmpire;
                        if (actualEmpire != null && actualEmpire.PirateMissions != null)
                        {
                            EmpireActivity firstByTargetAndTypeAssigned = actualEmpire.PirateMissions.GetFirstByTargetAndTypeAssigned(this, EmpireActivityType.Defend, actualEmpire);
                            if (firstByTargetAndTypeAssigned != null && firstByTargetAndTypeAssigned.AssignedEmpire != null && firstByTargetAndTypeAssigned.BidTimeRemaining == 0)
                            {
                                PirateRelation pirateRelation = actualEmpire.ObtainPirateRelation(firstByTargetAndTypeAssigned.AssignedEmpire);
                                pirateRelation.EvaluationPirateMissionsFail -= 20f;
                                string description = string.Format(TextResolver.GetText("Pirate Defend Mission Failed Pirate"), firstByTargetAndTypeAssigned.RequestingEmpire.Name, firstByTargetAndTypeAssigned.Target.Name, firstByTargetAndTypeAssigned.Price.ToString("0"));
                                firstByTargetAndTypeAssigned.AssignedEmpire.SendMessageToEmpire(firstByTargetAndTypeAssigned.AssignedEmpire, EmpireMessageType.PirateDefendMissionFailed, firstByTargetAndTypeAssigned.Target, description);
                                description = string.Format(TextResolver.GetText("Pirate Defend Mission Failed Other"), firstByTargetAndTypeAssigned.AssignedEmpire.Name, firstByTargetAndTypeAssigned.Target.Name, firstByTargetAndTypeAssigned.Price.ToString("0"));
                                firstByTargetAndTypeAssigned.RequestingEmpire.SendMessageToEmpire(firstByTargetAndTypeAssigned.RequestingEmpire, EmpireMessageType.PirateDefendMissionFailed, firstByTargetAndTypeAssigned.Target, description);
                                firstByTargetAndTypeAssigned.RequestingEmpire.PirateMissions.RemoveEquivalent(firstByTargetAndTypeAssigned);
                                firstByTargetAndTypeAssigned.AssignedEmpire.PirateMissions.RemoveEquivalent(firstByTargetAndTypeAssigned);
                                actualEmpire.PirateMissions.RemoveEquivalent(firstByTargetAndTypeAssigned);
                            }
                        }
                        return;
                    }
                    string description2 = string.Empty;
                    Empire actualEmpire2 = ActualEmpire;
                    if (actualEmpire2 != null)
                    {
                        string empty = string.Empty;
                        if (Role == BuiltObjectRole.Base)
                        {
                            empty = string.Format(TextResolver.GetText("BASE has been boarded and captured"), Name, empireById.Name);
                        }
                        else
                        {
                            string arg = Galaxy.ResolveDescription(SubRole).ToLower(CultureInfo.InvariantCulture);
                            empty = string.Format(TextResolver.GetText("Our ship X has been boarded and captured"), arg, Name, empireById.Name);
                        }
                        actualEmpire2.SendMessageToEmpire(ActualEmpire, EmpireMessageType.ShipBaseBoardedLost, this, empty);
                        description2 = ((Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("We have boarded and captured the ship X"), Name, actualEmpire2.Name) : string.Format(TextResolver.GetText("We have boarded and captured BASE"), Name, actualEmpire2.Name));
                    }
                    if (Characters != null && Characters.Count > 0)
                    {
                        Character[] array = ListHelper.ToArrayThreadSafe(Characters);
                        foreach (Character character in array)
                        {
                            if (character != null)
                            {
                                if (Role == BuiltObjectRole.Base)
                                {
                                    character.SendDeathMessage(CharacterDeathType.BaseCaptured, _Galaxy);
                                }
                                else
                                {
                                    character.SendDeathMessage(CharacterDeathType.ShipCaptured, _Galaxy);
                                }
                                character.Kill(_Galaxy);
                            }
                        }
                    }
                    if (actualEmpire2 != null && actualEmpire2.Characters != null)
                    {
                        for (int j = 0; j < actualEmpire2.Characters.Count; j++)
                        {
                            Character character2 = actualEmpire2.Characters[j];
                            if (character2 != null && character2.Active && character2.TransferDestination != null && character2.TransferDestination == this && character2.Location != this)
                            {
                                character2.ResetTransfer();
                            }
                        }
                    }
                    if (Troops != null && Troops.Count > 0)
                    {
                        for (int k = 0; k < Troops.Count; k++)
                        {
                            Troop troop = Troops[k];
                            if (troop != null && troop.Empire != null && troop.Empire.Troops.Contains(troop))
                            {
                                troop.Empire.Troops.Remove(troop);
                            }
                        }
                        Troops.Clear();
                    }
                    if (PirateEmpireId > 0)
                    {
                        Empire empireById2 = _Galaxy.GetEmpireById(PirateEmpireId);
                        if (empireById2 != null && empireById2.PirateEmpireBaseHabitat != null && Galaxy.Rnd.Next(0, 7) == 1)
                        {
                            BuiltObject builtObject = _Galaxy.IdentifyPirateSpaceport(empireById2);
                            if (builtObject != null && builtObject.NearestSystemStar != null && empireById != null && empireById.KnownPirateBases != null && !empireById.KnownPirateBases.Contains(builtObject))
                            {
                                empireById.KnownPirateBases.Add(builtObject);
                                Habitat habitat = Galaxy.DetermineHabitatSystemStar(builtObject.NearestSystemStar);
                                string text = _Galaxy.ResolveSectorDescription(habitat.Xpos, habitat.Ypos);
                                string message = string.Format(TextResolver.GetText("Ship Capture Reveals Pirate Base"), Name, empireById2.Name, builtObject.Name, habitat.Name, text);
                                empireById.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Ship Capture Reveals Pirate Base Title"), message, builtObject, null);
                            }
                        }
                    }
                    bool flag = false;
                    if (Role == BuiltObjectRole.Base && ParentHabitat != null && ParentHabitat.Empire != null && ParentHabitat.Population != null && ParentHabitat.Population.Count > 0)
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(NearestSystemStar);
                        if (empireById.PirateEmpireBaseHabitat != null)
                        {
                            double num4 = 2.0 * Galaxy.CalculateBuiltObjectLootingValue(this);
                            num4 *= empireById.ColonyIncomeFactor;
                            num4 = empireById.ApplyCorruptionToIncome(num4);
                            empireById.StateMoney += num4;
                            empireById.PirateEconomy.PerformIncome(num4, PirateIncomeType.ScrapCapturedShips, _Galaxy.CurrentStarDate);
                            string message2 = string.Format(TextResolver.GetText("Boarded Base Self Destructs Capture Loot"), Name, ParentHabitat.Name, habitat2.Name, num4.ToString("###,##0"));
                            empireById.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Boarded Base Self Destructs Title"), message2, this, ParentHabitat);
                        }
                        else
                        {
                            string message3 = string.Format(TextResolver.GetText("Boarded Base Self Destructs Capture"), Name, ParentHabitat.Name, habitat2.Name);
                            empireById.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Boarded Base Self Destructs Title"), message3, this, ParentHabitat);
                        }
                        string message4 = string.Format(TextResolver.GetText("Boarded Base Self Destructs Loss"), empireById.Name, Name, ParentHabitat.Name, habitat2.Name);
                        Empire.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Boarded Base Self Destructs Title"), message4, this, ParentHabitat);
                        InflictDamage(this, null, 1000000.0, time, _Galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                    }
                    else
                    {
                        _Galaxy.CheckCancelAttackMissionsForBuiltObject(this, empireById);
                        empireById.TakeOwnershipOfBuiltObject(this, empireById, setDesignAsObsolete: true, removeFromFleet: true);
                        empireById.SendMessageToEmpire(empireById, EmpireMessageType.ShipBaseBoardedCaptured, this, description2);
                        empireById.Counters.CaptureShipCount++;
                        BuiltObject nearestBuiltObject = empireById.BuiltObjects.GetNearestBuiltObject(Xpos, Ypos, BuiltObjectRole.Military, this);
                        if (nearestBuiltObject != null && !_Galaxy.ChanceNewShipCaptain(this, empireById, nearestBuiltObject, targetCaptured: true, smuggler: false))
                        {
                            _Galaxy.ChanceNewFleetAdmiral(this, empireById, nearestBuiltObject, targetCaptured: true);
                        }
                        int fixedDefenseValue3 = 0;
                        int num5 = CalculateBoardingDefenseValue(time, includeAllAssaultPods: true, out fixedDefenseValue3);
                        AssaultDefenseValue = Math.Min((short)num5, AssaultAttackValue);
                        AssaultAttackValue = 0;
                        AssaultOwnershipChangeCounter = 3000;
                    }
                    if (empireById.PirateEmpireBaseHabitat != null)
                    {
                        EmpireActivity byAttackTarget = empireById.PirateMissions.GetByAttackTarget(this, empireById);
                        if (byAttackTarget != null)
                        {
                            empireById.CompletePirateMission(byAttackTarget);
                        }
                    }
                    if (flag || empireById.Policy == null)
                    {
                        return;
                    }
                    if (Role == BuiltObjectRole.Base)
                    {
                        bool flag2 = false;
                        switch (empireById.Policy.CaptureEnlistBase)
                        {
                            case 0:
                                flag2 = false;
                                break;
                            case 1:
                                flag2 = ((SubRole != BuiltObjectSubRole.EnergyResearchStation && SubRole != BuiltObjectSubRole.HighTechResearchStation && SubRole != BuiltObjectSubRole.WeaponsResearchStation) ? true : false);
                                break;
                            case 2:
                                flag2 = true;
                                break;
                        }
                        if (flag2)
                        {
                            double num6 = 2.0 * Galaxy.CalculateBuiltObjectLootingValue(this);
                            num6 *= empireById.ColonyIncomeFactor;
                            num6 = empireById.ApplyCorruptionToIncome(num6);
                            empireById.StateMoney += num6;
                            empireById.PirateEconomy.PerformIncome(num6, PirateIncomeType.ScrapCapturedShips, _Galaxy.CurrentStarDate);
                            string empty2 = string.Empty;
                            empty2 = ((Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("Captured Ship Scrapped Description"), Name, num6.ToString("0")) : string.Format(TextResolver.GetText("Captured Base Scrapped Description"), Name, num6.ToString("0")));
                            empireById.SendMessageToEmpire(empireById, EmpireMessageType.ShipBaseScrapped, this, empty2);
                            DoingConstruction = true;
                            NextSoundTimeConstruction = 0L;
                            InflictDamage(this, null, 1000000.0, time, _Galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                        }
                        return;
                    }
                    int num7 = 0;
                    if (Role == BuiltObjectRole.Military)
                    {
                        num7 = empireById.Policy.CaptureEnlistMilitaryShip;
                    }
                    else if (Role != BuiltObjectRole.Base)
                    {
                        num7 = empireById.Policy.CaptureEnlistCivilianShip;
                    }
                    bool flag3 = true;
                    double num8 = Galaxy.ResolveTechBonusFactor(empireById, _Galaxy, this);
                    switch (num7)
                    {
                        case 0:
                            flag3 = true;
                            break;
                        case 1:
                            if (Size <= empireById.MaximumConstructionSize(SubRole) && num8 <= 1.0)
                            {
                                flag3 = false;
                            }
                            break;
                        case 2:
                            if (Size > empireById.MaximumConstructionSize(SubRole) || num8 > 1.0)
                            {
                                flag3 = false;
                            }
                            break;
                        case 3:
                            flag3 = false;
                            break;
                    }
                    if (flag3)
                    {
                        if (empireById.PirateEmpireBaseHabitat != null && Role == BuiltObjectRole.Freight)
                        {
                            _Galaxy.ChanceNewShipCaptain(this, empireById, this, targetCaptured: true, smuggler: true);
                        }
                        bool flag4 = false;
                        if (Role == BuiltObjectRole.Military)
                        {
                            flag4 = empireById.Policy.UpgradeEnlistedMilitaryShips;
                        }
                        else if (Role != BuiltObjectRole.Base)
                        {
                            flag4 = empireById.Policy.UpgradeEnlistedCivilianShips;
                        }
                        if (flag4)
                        {
                            ClearPreviousMissionRequirements();
                            Design design = empireById.Designs.FindNewestCanBuild(SubRole);
                            if (design != null)
                            {
                                empireById.AssignRetrofitMission(this, design, null, forceUseOfYard: true);
                            }
                        }
                        return;
                    }
                    int num9 = 0;
                    if (Role == BuiltObjectRole.Military)
                    {
                        num9 = empireById.Policy.CaptureDisassembleMilitaryShip;
                    }
                    else if (Role != BuiltObjectRole.Base)
                    {
                        num9 = empireById.Policy.CaptureDisassembleCivilianShip;
                    }
                    bool flag5 = true;
                    switch (num9)
                    {
                        case 0:
                            flag5 = true;
                            break;
                        case 1:
                            if (Size > empireById.MaximumConstructionSize(SubRole) || num8 > 1.0)
                            {
                                flag5 = false;
                            }
                            break;
                        case 2:
                            flag5 = false;
                            break;
                    }
                    int num10 = Components.CountNormalComponentsByCategory(ComponentCategoryType.HyperDrive);
                    if (!flag5)
                    {
                        if (TopSpeed <= 0)
                        {
                            flag5 = true;
                        }
                        else if (WarpSpeed <= 0 && num10 <= 0)
                        {
                            flag5 = true;
                        }
                    }
                    if (flag5)
                    {
                        double num11 = 2.0 * Galaxy.CalculateBuiltObjectLootingValue(this);
                        num11 *= empireById.ColonyIncomeFactor;
                        num11 = empireById.ApplyCorruptionToIncome(num11);
                        empireById.StateMoney += num11;
                        empireById.PirateEconomy.PerformIncome(num11, PirateIncomeType.ScrapCapturedShips, _Galaxy.CurrentStarDate);
                        string empty3 = string.Empty;
                        empty3 = ((Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("Captured Ship Scrapped Description"), Name, num11.ToString("0")) : string.Format(TextResolver.GetText("Captured Base Scrapped Description"), Name, num11.ToString("0")));
                        empireById.SendMessageToEmpire(empireById, EmpireMessageType.ShipBaseScrapped, this, empty3);
                        DoingConstruction = true;
                        NextSoundTimeConstruction = 0L;
                        InflictDamage(this, null, 1000000.0, time, _Galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                    }
                    else if (!empireById.AssignScrapMission(this, allowImmediateScrappingIfYardsFull: false, num10 <= 0))
                    {
                        InflictDamage(this, null, 1000000.0, time, _Galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                    }
                }
            }
            else if (AssaultDefenseValueDefault <= 0 || AssaultDefenseValueFixed <= 0 || InView || InBattle)
            {
                int fixedDefenseValue4 = 0;
                int num12 = CalculateBoardingDefenseValue(time, includeAllAssaultPods: true, out fixedDefenseValue4);
                if (AssaultDefenseValue > num12)
                {
                    double num13 = Math.Max(1.0, 1.0 * timePassed);
                    AssaultDefenseValue = (short)((double)AssaultDefenseValue - num13);
                }
                else if (AssaultDefenseValue < num12)
                {
                    int num14 = CalculateBoardingDefenseValue(time, out fixedDefenseValue4);
                    AssaultDefenseValue = (short)num14;
                }
                AssaultDefenseValueDefault = (short)num12;
                AssaultDefenseValueFixed = (short)fixedDefenseValue4;
            }
        }

        public int CalculateAvailableAssaultPodAttackStrength(DateTime time)
        {
            int num = 0;
            if (AssaultRange > 0 && AssaultStrength > 0 && Weapons != null)
            {
                double num2 = 1.0;
                double num3 = 1.0;
                if (Empire != null)
                {
                    num3 = Empire.BoardingAttackFactor;
                    num3 *= Empire.RaidStrengthFactor;
                    if (Empire.DominantRace != null)
                    {
                        num2 = (double)Empire.DominantRace.TroopStrength / 100.0;
                    }
                }
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapon weapon = Weapons[i];
                    if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod && weapon.IsAvailableWithoutEnergyConsideration(time))
                    {
                        num += (int)((double)weapon.RawDamage * num2 * num3);
                    }
                }
            }
            return num;
        }

        public int CalculateAssaultPodAttackValues(DateTime time, out int assaultPodCount, out int assaultPodsAvailable)
        {
            int num = 0;
            assaultPodCount = 0;
            assaultPodsAvailable = 0;
            if (AssaultRange > 0 && AssaultStrength > 0 && Weapons != null)
            {
                double num2 = 1.0;
                double num3 = 1.0;
                if (Empire != null)
                {
                    num3 = Empire.BoardingAttackFactor;
                    num3 *= Empire.RaidStrengthFactor;
                    if (Empire.DominantRace != null)
                    {
                        num2 = (double)Empire.DominantRace.TroopStrength / 100.0;
                    }
                }
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapon weapon = Weapons[i];
                    if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod)
                    {
                        assaultPodCount++;
                        if (weapon.IsAvailableWithoutEnergyConsideration(time))
                        {
                            assaultPodsAvailable++;
                            num += (int)((double)weapon.RawDamage * num2 * num3);
                        }
                    }
                }
            }
            return num;
        }

        public int CountAssaultPods()
        {
            int num = 0;
            if (AssaultRange > 0 && AssaultStrength > 0 && Weapons != null)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapon weapon = Weapons[i];
                    if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int CalculateBoardingDefenseValue(DateTime time, out int fixedDefenseValue)
        {
            return CalculateBoardingDefenseValue(time, includeAllAssaultPods: false, out fixedDefenseValue);
        }

        public int CalculateBoardingDefenseValue(DateTime time, bool includeAllAssaultPods, out int fixedDefenseValue)
        {
            int num = 0;
            fixedDefenseValue = 0;
            double num2 = 1.0;
            double num3 = 1.0;
            if (Empire != null)
            {
                num3 = Empire.BoardingDefenseFactor;
                num3 *= Empire.RaidStrengthFactor;
                if (Empire.DominantRace != null)
                {
                    num2 = (double)Empire.DominantRace.TroopStrength / 100.0;
                }
            }
            if (Components != null)
            {
                for (int i = 0; i < Components.Count; i++)
                {
                    BuiltObjectComponent builtObjectComponent = Components[i];
                    if (builtObjectComponent == null)
                    {
                        continue;
                    }
                    ComponentType type = builtObjectComponent.Type;
                    if (type == ComponentType.HabitationHabModule)
                    {
                        int num4 = (int)(20.0 * num2 * num3);
                        if (builtObjectComponent.Status == ComponentStatus.Normal)
                        {
                            num += num4;
                        }
                        fixedDefenseValue += num4;
                    }
                }
            }
            if (Weapons != null)
            {
                for (int j = 0; j < Weapons.Count; j++)
                {
                    Weapon weapon = Weapons[j];
                    if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod && (includeAllAssaultPods || weapon.IsAvailableWithoutEnergyConsideration(time)))
                    {
                        num += (int)((double)weapon.RawDamage * num2 * num3);
                    }
                }
            }
            if (Troops != null)
            {
                for (int k = 0; k < Troops.Count; k++)
                {
                    Troop troop = Troops[k];
                    if (troop != null)
                    {
                        int num5 = (int)(troop.OverallDefendStrength / 100.0 * num3);
                        num += num5;
                        fixedDefenseValue += num5;
                    }
                }
            }
            return num;
        }

        public BuiltObjectComponent DisableRandomComponent(short durationInMilliseconds)
        {
            if (DisabledComponentIndexes == null)
            {
                DisabledComponentIndexes = new List<short>();
            }
            if (DisabledComponentDurations == null)
            {
                DisabledComponentDurations = new List<short>();
            }
            for (int i = 0; i < Components.Count; i++)
            {
                BuiltObjectComponent builtObjectComponent = Components[i];
                if (builtObjectComponent != null && builtObjectComponent.Status == ComponentStatus.Normal)
                {
                    ComponentType type = builtObjectComponent.Type;
                    if (type != ComponentType.Armor && type != ComponentType.ComputerCommandCenter && !DisabledComponentIndexes.Contains((short)i))
                    {
                        DisabledComponentIndexes.Add((short)i);
                        DisabledComponentDurations.Add(durationInMilliseconds);
                        ReDefine();
                        return builtObjectComponent;
                    }
                }
            }
            return null;
        }

        private void DestroyHabitat(Habitat habitat)
        {
            Explosion explosion = new Explosion();
            explosion.ExplosionStart = _Galaxy.CurrentDateTime;
            explosion.ExplosionSize = (short)((double)habitat.Diameter * 4.0);
            explosion.ExplosionProgression = 0f;
            explosion.ExplosionOffsetX = 0;
            explosion.ExplosionOffsetY = 0;
            explosion.ExplosionImageIndex = (short)Galaxy.Rnd.Next(0, 10);
            explosion.ExplosionWillDestroy = true;
            habitat.Explosion = explosion;
            habitat.HasBeenDestroyed = true;
            if (Empire != null && habitat.Population != null && habitat.Population.TotalAmount > 0)
            {
                double num = Galaxy.PlanetDestroyReputationImpact;
                if (habitat.Owner != null && habitat.Owner != _Galaxy.IndependentEmpire && habitat.Owner.PirateEmpireBaseHabitat == null)
                {
                    if (habitat.Owner.CivilityRating > 0.0)
                    {
                        double num2 = 1.0 + habitat.Owner.CivilityRating / 30.0;
                        num *= num2;
                    }
                    else
                    {
                        double val = 1.0 + habitat.Owner.CivilityRating / 50.0;
                        val = Math.Max(0.01, val);
                        num *= val;
                    }
                }
                if (habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire)
                {
                    if (habitat.Empire.PirateEmpireBaseHabitat != null)
                    {
                        if (Empire != null && habitat.Empire.ObtainPirateRelation(Empire).Type == PirateRelationType.Protection)
                        {
                            habitat.Empire.ChangePirateRelation(Empire, PirateRelationType.None, _Galaxy.CurrentStarDate);
                        }
                    }
                    else if (Empire != null && Empire.PirateEmpireBaseHabitat == null && habitat.Empire.ControlDiplomacyOffense == AutomationLevel.FullyAutomated && habitat.Empire.PirateEmpireBaseHabitat == null)
                    {
                        habitat.Empire.DeclareWar(Empire);
                    }
                }
                Empire.CivilityRating -= num;
            }
            _Galaxy.InflictWarDamage(Empire, habitat);
            InflictHabitatDestructionAreaDamage(habitat);
            Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
            SystemInfo systemInfo = _Galaxy.Systems[habitat2.SystemIndex];
            if (systemInfo.Habitats == null || systemInfo.Habitats.Count <= 0)
            {
                return;
            }
            HabitatList habitatList = new HabitatList();
            foreach (Habitat habitat3 in systemInfo.Habitats)
            {
                if (habitat3.Parent == habitat)
                {
                    habitatList.Add(habitat3);
                }
            }
            foreach (Habitat item in habitatList)
            {
                DestroyHabitat(item);
            }
        }

        private void InflictHabitatDestructionAreaDamage(Habitat habitat)
        {
            double num = 620.0;
            double num2 = (double)habitat.Diameter / 2.0;
            double num3 = num - num2;
            int num4 = (int)(habitat.Xpos / (double)Galaxy.IndexSize);
            int num5 = (int)(habitat.Ypos / (double)Galaxy.IndexSize);
            for (int i = 0; i < _Galaxy.BuiltObjectIndex[num4][num5].Count; i++)
            {
                BuiltObject builtObject = _Galaxy.BuiltObjectIndex[num4][num5][i];
                if (_Galaxy.CheckWithinDistancePotential((int)num, habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos))
                {
                    double num6 = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                    double num7 = (double)habitat.Diameter * 40.0 * ((num3 - (num6 - num2)) / num3);
                    if (num7 > 0.0)
                    {
                        InflictDamage(builtObject, null, num7, _Galaxy.CurrentDateTime, _Galaxy, 0f, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
                    }
                }
            }
        }

        private void HandlePlanetDestroyerFiring(Weapon weapon, double timePassed)
        {
            if (weapon.Target == null)
            {
                weapon.ResetNext = true;
            }
            else
            {
                if (!(weapon.Target is Habitat))
                {
                    return;
                }
                if (weapon.Target != null && weapon.Target.HasBeenDestroyed)
                {
                    weapon.ResetNext = true;
                }
                else
                {
                    if (!((double)weapon.DistanceTravelled >= 0.0) || HasBeenDestroyed || weapon.Component.Status != ComponentStatus.Normal)
                    {
                        return;
                    }
                    float val = (float)((double)_tempNow.Subtract(weapon.LastFired).Ticks / 10000000.0);
                    val = Math.Min(val, (float)timePassed);
                    double num = Galaxy.TorpedoWeaponHitRange * 2.0;
                    if (InView)
                    {
                        num = Galaxy.TorpedoWeaponHitRange;
                    }
                    float num2 = ((!(weapon.DistanceTravelled <= 1f)) ? ((float)weapon.Speed * val) : 10f);
                    weapon.DistanceTravelled += num2;
                    float distanceFromTarget = weapon.DistanceFromTarget;
                    weapon.X += Math.Cos(weapon.Heading) * (double)num2;
                    weapon.Y += Math.Sin(weapon.Heading) * (double)num2;
                    weapon.DistanceFromTarget = (float)_Galaxy.CalculateDistance(weapon.X, weapon.Y, weapon.Target.Xpos, weapon.Target.Ypos);
                    float num3 = weapon.RawDamage;
                    if (ShipGroup != null)
                    {
                        num3 *= (float)ShipGroup.WeaponsDamageBonus;
                    }
                    num3 *= (float)CaptainWeaponsDamageBonus;
                    weapon.Power = num3 - weapon.DistanceTravelled / 100f * (float)weapon.DamageLoss;
                    if (weapon.WillHitTarget)
                    {
                        bool flag = false;
                        if (distanceFromTarget < weapon.DistanceFromTarget)
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            Habitat habitat = (Habitat)weapon.Target;
                            habitat.TeardownEmpire = ActualEmpire;
                            DestroyHabitat(habitat);
                            weapon.Target = null;
                            weapon.ResetNext = true;
                        }
                    }
                    float num4 = weapon.Range;
                    if (ShipGroup != null)
                    {
                        num4 *= (float)ShipGroup.WeaponsRangeBonus;
                    }
                    num4 *= (float)CaptainWeaponsRangeBonus;
                    if (weapon.DistanceTravelled > num4)
                    {
                        weapon.ResetNext = true;
                    }
                }
            }
        }

        private bool DetermineTractorBeamShouldPullGeneral()
        {
            bool result = true;
            if (Mission != null && Mission.Type == BuiltObjectMissionType.Escape)
            {
                result = false;
            }
            return result;
        }

        private bool DetermineTractorBeamShouldPullTarget(StellarObject target, int ourLongRangeWeaponsDamage, double distance)
        {
            bool result = true;
            if (target != null)
            {
                if (target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)target;
                    double num = OptimalMaximumAttackRange;
                    if (Role == BuiltObjectRole.Base && builtObject.TroopCapacity > 0 && ParentHabitat != null && ParentHabitat.Population != null && ParentHabitat.Empire == Empire && (SubRole == BuiltObjectSubRole.SmallSpacePort || SubRole == BuiltObjectSubRole.MediumSpacePort || SubRole == BuiltObjectSubRole.LargeSpacePort))
                    {
                        num = Math.Max(Math.Max(200.0, (double)MinimumWeaponsRange * 0.9), num);
                    }
                    switch (DetermineTacticsAgainstTarget(target))
                    {
                        case BattleTactics.Standoff:
                        case BattleTactics.AllWeapons:
                        case BattleTactics.PointBlank:
                            result = ((distance > num) ? true : false);
                            break;
                        case BattleTactics.Evade:
                            result = false;
                            break;
                    }
                    if (builtObject.FirepowerRaw > FirepowerRaw)
                    {
                        int num2 = builtObject.Weapons.CalculateRawDamageOfWeaponsAboveRange(TractorBeamRange);
                        if (ourLongRangeWeaponsDamage > num2)
                        {
                            result = false;
                        }
                    }
                }
                else if (target is Creature)
                {
                    Creature creature = (Creature)target;
                    if (creature.CurrentTarget != null && creature.CurrentTarget == this)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        private bool CheckFireAreaWeaponAtTarget(Weapon weapon, StellarObject target)
        {
            if (target != null && weapon != null)
            {
                double num = 0.0;
                bool flag = false;
                if (weapon.Component != null && weapon.Component.Type == ComponentType.WeaponAreaGravity)
                {
                    flag = true;
                }
                num = ((!flag) ? ((double)(weapon.Range * weapon.Range)) : ((double)(weapon.DamageLoss * weapon.DamageLoss)));
                num *= 0.7;
                GalaxyIndex galaxyIndex = _Galaxy.ResolveIndex(target.Xpos, target.Ypos);
                int x = galaxyIndex.X;
                int y = galaxyIndex.Y;
                for (int i = 0; i < _Galaxy.BuiltObjectIndex[x][y].Count; i++)
                {
                    BuiltObject builtObject = _Galaxy.BuiltObjectIndex[x][y][i];
                    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Empire != null && builtObject.Empire == Empire && builtObject != this)
                    {
                        double num2 = _Galaxy.CalculateDistanceSquared(target.Xpos, target.Ypos, builtObject.Xpos, builtObject.Ypos);
                        if (num2 < num)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void HandleWeaponsFiring(double timePassed, DateTime time, Galaxy galaxy)
        {
            float num = BaconBuiltObject.WeaponRangeIncrementForDamageLoss(this);
            bool flag = true;
            int ourLongRangeWeaponsDamage = 0;
            if (TractorBeamRange > 0)
            {
                flag = DetermineTractorBeamShouldPullGeneral();
                ourLongRangeWeaponsDamage = Weapons.CalculateRawDamageOfWeaponsAboveRange(TractorBeamRange);
            }
            ShipGroup shipGroup = ShipGroup;
            for (int i = 0; i < Weapons.Count && !HasBeenDestroyed; i++)
            {
                Weapon weapon = Weapons[i];
                if (weapon == null || (weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod))
                {
                    continue;
                }
                if (weapon.ResetNext)
                {
                    weapon.Reset();
                    continue;
                }
                if (weapon.IsPlanetDestroyer)
                {
                    if (weapon.Target != null && weapon.Target is Habitat)
                    {
                        HandlePlanetDestroyerFiring(weapon, timePassed);
                        continue;
                    }
                    if (weapon.Target == null)
                    {
                        if (weapon.DistanceTravelled > 0f)
                        {
                            weapon.ResetNext = true;
                        }
                        continue;
                    }
                }
                if (weapon.Target == null && weapon.TargetWeapon == null)
                {
                    weapon.ResetNext = true;
                    continue;
                }
                if (weapon.Target != null && weapon.Target.HasBeenDestroyed)
                {
                    weapon.ResetNext = true;
                    continue;
                }
                if (weapon.TargetWeapon != null && weapon.TargetWeapon.DistanceTravelled <= 0f)
                {
                    weapon.ResetNext = true;
                    continue;
                }
                double num2 = Xpos;
                double num3 = Ypos;
                int num4 = 0;
                if (weapon.Target != null)
                {
                    num2 = weapon.Target.Xpos;
                    num3 = weapon.Target.Ypos;
                    num4 = weapon.Target.Size;
                }
                else if (weapon.TargetWeapon != null)
                {
                    num2 = weapon.TargetWeapon.X;
                    num3 = weapon.TargetWeapon.Y;
                    num4 = (int)weapon.TargetWeapon.Power;
                }
                if (!(weapon.DistanceTravelled >= 0f))
                {
                    continue;
                }
                float val = (float)((double)_tempNow.Subtract(weapon.LastFired).Ticks / 10000000.0);
                val = Math.Min(val, (float)timePassed);
                bool flag2 = false;
                double num5 = Galaxy.TorpedoWeaponHitRange * 3.0;
                if (InView)
                {
                    num5 = Galaxy.TorpedoWeaponHitRange;
                }
                float num6 = weapon.RawDamage;
                float num7 = weapon.Range;
                if (shipGroup != null)
                {
                    num6 *= (float)shipGroup.WeaponsDamageBonus;
                    num7 *= (float)shipGroup.WeaponsRangeBonus;
                }
                num6 *= (float)CaptainWeaponsDamageBonus;
                num7 *= (float)CaptainWeaponsRangeBonus;
                if (SensorTraceScannerPower > 0)
                {
                    num6 *= 1f + (float)SensorTraceScannerPower / 100f;
                }
                switch (weapon.Component.Type)
                {
                    case ComponentType.WeaponBeam:
                    case ComponentType.WeaponPointDefense:
                    case ComponentType.WeaponIonCannon:
                    case ComponentType.WeaponSuperBeam:
                    case ComponentType.WeaponPhaser:
                    case ComponentType.WeaponRailGun:
                    case ComponentType.WeaponSuperPhaser:
                    case ComponentType.WeaponSuperRailGun:
                        {
                            float num8;
                            if (weapon.DistanceTravelled <= 1f)
                            {
                                flag2 = true;
                                num8 = 2f;
                            }
                            else
                            {
                                num8 = (float)weapon.Speed * val;
                            }
                            weapon.DistanceTravelled += num8;
                            float distanceFromTarget = weapon.DistanceFromTarget;
                            weapon.X += Math.Cos(weapon.Heading) * (double)num8;
                            weapon.Y += Math.Sin(weapon.Heading) * (double)num8;
                            weapon.DistanceFromTarget = (float)galaxy.CalculateDistance(weapon.X, weapon.Y, num2, num3);
                            weapon.Power = BaconBuiltObject.WeaponDamageDropoff(this, weapon, num6);
                            if (weapon.WillHitTarget && !flag2)
                            {
                                bool flag7 = false;
                                if (InView)
                                {
                                    if ((double)weapon.DistanceFromTarget <= num5)
                                    {
                                        flag7 = true;
                                    }
                                }
                                else if (distanceFromTarget < weapon.DistanceFromTarget)
                                {
                                    flag7 = true;
                                }
                                if (flag7)
                                {
                                    if (weapon.TargetWeapon != null)
                                    {
                                        weapon.TargetWeapon.Power = float.MaxValue;
                                        weapon.TargetWeapon.ResetNext = true;
                                    }
                                    else if (weapon.Component.Type == ComponentType.WeaponIonCannon)
                                    {
                                        if (weapon.Target != null && weapon.Target.HasBeenDestroyed)
                                        {
                                            weapon.Target = null;
                                        }
                                        else
                                        {
                                            InflictIonDamage(weapon.Target, weapon, weapon.Power, time, galaxy, weapon.Heading);
                                        }
                                    }
                                    else if (weapon.Target is Habitat && weapon.BombardDamage > 0)
                                    {
                                        InflictBombardDamage((Habitat)weapon.Target, weapon.BombardDamage);
                                        weapon.Target = null;
                                    }
                                    else if (InflictDamage(weapon.Target, weapon, weapon.Power, time, galaxy, weapon.DistanceTravelled, weapon.Heading))
                                    {
                                        if (weapon.Target != null && weapon.Target is BuiltObject)
                                        {
                                            BuiltObject builtObject6 = (BuiltObject)weapon.Target;
                                            if (builtObject6.Empire != null && builtObject6.Empire.PirateEmpireBaseHabitat != null && Empire != null && Empire.PirateEmpireBaseHabitat != null && Empire.PirateEmpireSuperPirates)
                                            {
                                                bool flag8 = false;
                                                switch (builtObject6.SubRole)
                                                {
                                                    case BuiltObjectSubRole.SmallSpacePort:
                                                    case BuiltObjectSubRole.MediumSpacePort:
                                                    case BuiltObjectSubRole.LargeSpacePort:
                                                        flag8 = true;
                                                        break;
                                                }
                                                if (flag8 && Galaxy.Rnd.Next(0, 4) == 1)
                                                {
                                                    _Galaxy.FearfulPirateFactionJoinsPlayer(Empire, builtObject6.Empire);
                                                }
                                            }
                                            ProvideBonusFromPirateBase(Empire, builtObject6);
                                            if (Empire != null && Empire.PirateEmpireBaseHabitat != null && _Galaxy.PirateEmpires.Contains(Empire))
                                            {
                                                double num28 = Galaxy.CalculateBuiltObjectLootingValue(builtObject6);
                                                num28 *= Empire.ColonyIncomeFactor;
                                                num28 *= Empire.LootingFactor;
                                                num28 = Empire.ApplyCorruptionToIncome(num28);
                                                Empire.StateMoney += num28;
                                                Empire.PirateEconomy.PerformIncome(num28, PirateIncomeType.Looting, galaxy.CurrentStarDate);
                                                EmpireActivity byAttackTarget4 = Empire.PirateMissions.GetByAttackTarget(builtObject6, Empire);
                                                if (byAttackTarget4 != null)
                                                {
                                                    Empire.CompletePirateMission(byAttackTarget4);
                                                }
                                            }
                                        }
                                        weapon.Target = null;
                                    }
                                    weapon.ResetNext = true;
                                }
                            }
                            if (weapon.DistanceTravelled > num7)
                            {
                                if (BattleStats != null)
                                {
                                    BattleStats.WeaponMissEnemy();
                                }
                                if (shipGroup != null && shipGroup.BattleStats != null)
                                {
                                    shipGroup.BattleStats.WeaponMissEnemy();
                                }
                                weapon.ResetNext = true;
                            }
                            break;
                        }
                    case ComponentType.WeaponGravityBeam:
                        {
                            float num8;
                            if (weapon.DistanceTravelled <= 1f)
                            {
                                flag2 = true;
                                num8 = 2f;
                                if (!weapon.WillHitTarget && !weapon.HasMissed && weapon.Target != null)
                                {
                                    double num35 = galaxy.CalculateDistance(Xpos, Ypos, num2, num3);
                                    num35 += 100.0 * (Galaxy.Rnd.NextDouble() - 0.5);
                                    num35 = Math.Min(num35, weapon.Range);
                                    double num36 = Galaxy.DetermineAngle(Xpos, Ypos, num2, num3);
                                    num36 += 0.5 * (Galaxy.Rnd.NextDouble() - 0.5);
                                    weapon.X = Xpos + Math.Cos(num36) * num35;
                                    weapon.Y = Ypos + Math.Sin(num36) * num35;
                                    weapon.DistanceFromTarget = (float)galaxy.CalculateDistance(weapon.X, weapon.Y, num2, num3);
                                    weapon.HasMissed = true;
                                }
                            }
                            else
                            {
                                num8 = (float)weapon.Speed * val;
                            }
                            weapon.DistanceTravelled += num8;
                            weapon.Power = BaconBuiltObject.WeaponDamageDropoff(this, weapon, num6);
                            if (weapon.WillHitTarget)
                            {
                                weapon.X = num2;
                                weapon.Y = num3;
                                double num37 = galaxy.CalculateDistance(Xpos, Ypos, num2, num3);
                                double num38 = Math.Min(1.0, Math.Max(0.05, 1.0 - num37 / (double)weapon.Range));
                                double num39 = Math.Min(60.0, Math.Max(20.0, (double)weapon.RawDamage / ((double)num4 / 1000.0) * num38));
                                double num40 = Math.Min(1.0, timePassed * num39);
                                if (weapon.Target != null)
                                {
                                    if (time.Millisecond % 100 < 50)
                                    {
                                        num40 = 0.0 - num40;
                                    }
                                    weapon.Target.Xpos += num40;
                                    weapon.Target.Ypos += num40;
                                    weapon.DistanceFromTarget = (float)galaxy.CalculateDistance(weapon.X, weapon.Y, num2, num3);
                                    double hitPower2 = Math.Max(1.0, (double)weapon.Power * num38 * Math.Max(0.05, 100.0 / (double)num4));
                                    if (flag2 && InflictDamage(weapon.Target, weapon, hitPower2, time, galaxy, weapon.DistanceTravelled, weapon.Heading))
                                    {
                                        if (weapon.Target != null && weapon.Target is BuiltObject)
                                        {
                                            BuiltObject builtObject7 = (BuiltObject)weapon.Target;
                                            if (builtObject7.Empire != null && builtObject7.Empire.PirateEmpireBaseHabitat != null && Empire != null && Empire.PirateEmpireBaseHabitat != null && Empire.PirateEmpireSuperPirates)
                                            {
                                                bool flag9 = false;
                                                switch (builtObject7.SubRole)
                                                {
                                                    case BuiltObjectSubRole.SmallSpacePort:
                                                    case BuiltObjectSubRole.MediumSpacePort:
                                                    case BuiltObjectSubRole.LargeSpacePort:
                                                        flag9 = true;
                                                        break;
                                                }
                                                if (flag9 && Galaxy.Rnd.Next(0, 4) == 1)
                                                {
                                                    _Galaxy.FearfulPirateFactionJoinsPlayer(Empire, builtObject7.Empire);
                                                }
                                            }
                                            ProvideBonusFromPirateBase(Empire, builtObject7);
                                            if (Empire != null && Empire.PirateEmpireBaseHabitat != null && _Galaxy.PirateEmpires.Contains(Empire))
                                            {
                                                double num41 = Galaxy.CalculateBuiltObjectLootingValue(builtObject7);
                                                num41 *= Empire.ColonyIncomeFactor;
                                                num41 *= Empire.LootingFactor;
                                                num41 = Empire.ApplyCorruptionToIncome(num41);
                                                Empire.StateMoney += num41;
                                                Empire.PirateEconomy.PerformIncome(num41, PirateIncomeType.Looting, galaxy.CurrentStarDate);
                                                EmpireActivity byAttackTarget5 = Empire.PirateMissions.GetByAttackTarget(builtObject7, Empire);
                                                if (byAttackTarget5 != null)
                                                {
                                                    Empire.CompletePirateMission(byAttackTarget5);
                                                }
                                            }
                                        }
                                        weapon.Target = null;
                                    }
                                }
                            }
                            double totalSeconds2 = time.Subtract(weapon.LastFired).TotalSeconds;
                            if (totalSeconds2 > 3.0)
                            {
                                weapon.ResetNext = true;
                            }
                            break;
                        }
                    case ComponentType.WeaponTractorBeam:
                        {
                            if (weapon.DistanceTravelled <= 1f)
                            {
                                flag2 = true;
                                float num8 = 2f;
                                if (!weapon.WillHitTarget && !weapon.HasMissed && weapon.Target != null)
                                {
                                    double num29 = galaxy.CalculateDistance(Xpos, Ypos, num2, num3);
                                    num29 += 100.0 * (Galaxy.Rnd.NextDouble() - 0.5);
                                    num29 = Math.Min(num29, weapon.Range);
                                    double num30 = Galaxy.DetermineAngle(Xpos, Ypos, num2, num3);
                                    num30 += 0.5 * (Galaxy.Rnd.NextDouble() - 0.5);
                                    weapon.X = Xpos + Math.Cos(num30) * num29;
                                    weapon.Y = Ypos + Math.Sin(num30) * num29;
                                    weapon.DistanceFromTarget = (float)galaxy.CalculateDistance(weapon.X, weapon.Y, num2, num3);
                                    weapon.HasMissed = true;
                                }
                            }
                            else
                            {
                                float num8 = (float)weapon.Speed * val;
                            }
                            if (weapon.WillHitTarget)
                            {
                                weapon.X = num2;
                                weapon.Y = num3;
                                double num31 = galaxy.CalculateDistance(Xpos, Ypos, num2, num3);
                                double num32 = Math.Min(1.0, Math.Max(0.01, 1.0 - num31 / (double)weapon.Range));
                                double num33 = timePassed * ((double)weapon.RawDamage / ((double)num4 / 1000.0)) * num32;
                                if (weapon.Target != null)
                                {
                                    double num34 = 0.0;
                                    num34 = ((flag && DetermineTractorBeamShouldPullTarget(weapon.Target, ourLongRangeWeaponsDamage, num31)) ? Galaxy.DetermineAngle(num2, num3, Xpos, Ypos) : Galaxy.DetermineAngle(Xpos, Ypos, num2, num3));
                                    weapon.Target.Xpos += Math.Cos(num34) * num33;
                                    weapon.Target.Ypos += Math.Sin(num34) * num33;
                                    weapon.DistanceFromTarget = (float)galaxy.CalculateDistance(weapon.X, weapon.Y, num2, num3);
                                }
                            }
                            double totalSeconds = time.Subtract(weapon.LastFired).TotalSeconds;
                            if (totalSeconds > 2.0)
                            {
                                weapon.ResetNext = true;
                            }
                            break;
                        }
                    case ComponentType.WeaponTorpedo:
                    case ComponentType.WeaponBombard:
                    case ComponentType.WeaponMissile:
                    case ComponentType.WeaponSuperTorpedo:
                    case ComponentType.WeaponSuperMissile:
                        {
                            float num8;
                            if (weapon.DistanceTravelled <= 1f)
                            {
                                flag2 = true;
                                num8 = 10f;
                            }
                            else if (weapon.Component.Type == ComponentType.WeaponMissile || weapon.Component.Type == ComponentType.WeaponSuperMissile)
                            {
                                float num13 = weapon.Speed;
                                float distanceTravelled = weapon.DistanceTravelled;
                                if (distanceTravelled < 120f)
                                {
                                    float num14 = distanceTravelled / 120f;
                                    num13 = Math.Max(3f, num13 * num14);
                                }
                                num8 = num13 * val;
                            }
                            else
                            {
                                num8 = (float)weapon.Speed * val;
                            }
                            float heading = weapon.Heading;
                            if (!weapon.HasMissed && !(weapon.Target is Habitat))
                            {
                                weapon.Heading = weapon.HeadingMissFactor + (float)Galaxy.DetermineAngle(weapon.X, weapon.Y, num2, num3);
                            }
                            weapon.DistanceTravelled += num8;
                            float distanceFromTarget = weapon.DistanceFromTarget;
                            weapon.X += Math.Cos(weapon.Heading) * (double)num8;
                            weapon.Y += Math.Sin(weapon.Heading) * (double)num8;
                            weapon.DistanceFromTarget = (float)galaxy.CalculateDistance(weapon.X, weapon.Y, num2, num3);
                            weapon.Power = BaconBuiltObject.WeaponDamageDropoff(this, weapon, num6);
                            if (weapon.WillHitTarget)
                            {
                                if (!flag2)
                                {
                                    bool flag4 = false;
                                    if (InView && (double)weapon.DistanceFromTarget <= num5)
                                    {
                                        flag4 = true;
                                    }
                                    else if (distanceFromTarget < weapon.DistanceFromTarget || num8 > distanceFromTarget)
                                    {
                                        flag4 = true;
                                    }
                                    if (flag4 && weapon.Target != null)
                                    {
                                        StellarObject target = weapon.Target;
                                        if (target is Habitat && weapon.BombardDamage > 0)
                                        {
                                            InflictBombardDamage((Habitat)target, weapon.BombardDamage);
                                            weapon.Target = null;
                                        }
                                        else if (InflictDamage(weapon.Target, weapon, weapon.Power, time, galaxy, weapon.DistanceTravelled, weapon.Heading))
                                        {
                                            if (weapon.Target is BuiltObject)
                                            {
                                                BuiltObject builtObject3 = (BuiltObject)weapon.Target;
                                                if (builtObject3.Empire != null && builtObject3.Empire.PirateEmpireBaseHabitat != null && Empire != null && Empire.PirateEmpireBaseHabitat != null && Empire.PirateEmpireSuperPirates)
                                                {
                                                    bool flag5 = false;
                                                    switch (builtObject3.SubRole)
                                                    {
                                                        case BuiltObjectSubRole.SmallSpacePort:
                                                        case BuiltObjectSubRole.MediumSpacePort:
                                                        case BuiltObjectSubRole.LargeSpacePort:
                                                            flag5 = true;
                                                            break;
                                                    }
                                                    if (flag5 && Galaxy.Rnd.Next(0, 4) == 1)
                                                    {
                                                        _Galaxy.FearfulPirateFactionJoinsPlayer(Empire, builtObject3.Empire);
                                                    }
                                                }
                                                ProvideBonusFromPirateBase(Empire, builtObject3);
                                                if (Empire != null && Empire.PirateEmpireBaseHabitat != null && _Galaxy.PirateEmpires.Contains(Empire))
                                                {
                                                    double num15 = Galaxy.CalculateBuiltObjectLootingValue(builtObject3);
                                                    num15 *= Empire.ColonyIncomeFactor;
                                                    num15 *= Empire.LootingFactor;
                                                    num15 = Empire.ApplyCorruptionToIncome(num15);
                                                    Empire.StateMoney += num15;
                                                    Empire.PirateEconomy.PerformIncome(num15, PirateIncomeType.Looting, galaxy.CurrentStarDate);
                                                    EmpireActivity byAttackTarget2 = Empire.PirateMissions.GetByAttackTarget(builtObject3, Empire);
                                                    if (byAttackTarget2 != null)
                                                    {
                                                        Empire.CompletePirateMission(byAttackTarget2);
                                                    }
                                                }
                                            }
                                            weapon.Target = null;
                                        }
                                        weapon.ResetNext = true;
                                    }
                                }
                            }
                            else if (weapon.HasMissed)
                            {
                                weapon.Heading = heading;
                            }
                            else if (distanceFromTarget < weapon.DistanceFromTarget || num8 > distanceFromTarget)
                            {
                                weapon.HasMissed = true;
                                weapon.Heading = heading;
                            }
                            if (weapon.DistanceTravelled > num7)
                            {
                                if (BattleStats != null)
                                {
                                    BattleStats.WeaponMissEnemy();
                                }
                                if (shipGroup != null && shipGroup.BattleStats != null)
                                {
                                    shipGroup.BattleStats.WeaponMissEnemy();
                                }
                                weapon.ResetNext = true;
                            }
                            break;
                        }
                    case ComponentType.WeaponAreaGravity:
                        {
                            float num8;
                            if (weapon.DistanceTravelled <= 1f)
                            {
                                flag2 = true;
                                num8 = 2f;
                                if (weapon.Target != null)
                                {
                                    weapon.X = weapon.Target.Xpos;
                                    weapon.Y = weapon.Target.Ypos;
                                }
                            }
                            else
                            {
                                num8 = (float)weapon.Speed * val;
                            }
                            weapon.DistanceTravelled += num8;
                            weapon.Power = BaconBuiltObject.WeaponDamageDropoff(this, weapon, num6);
                            if (weapon.DistanceTravelled > num7)
                            {
                                weapon.ResetNext = true;
                            }
                            double num16 = weapon.DamageLoss * weapon.DamageLoss;
                            double num17 = weapon.BombardDamage * weapon.BombardDamage;
                            GalaxyIndex galaxyIndex2 = _Galaxy.ResolveIndex(Xpos, Ypos);
                            int x2 = galaxyIndex2.X;
                            int y2 = galaxyIndex2.Y;
                            for (int m = 0; m < galaxy.BuiltObjectIndex[x2][y2].Count; m++)
                            {
                                BuiltObject builtObject4 = galaxy.BuiltObjectIndex[x2][y2][m];
                                if (builtObject4 == null)
                                {
                                    continue;
                                }
                                double num18 = galaxy.CalculateDistanceSquared(weapon.X, weapon.Y, builtObject4.Xpos, builtObject4.Ypos);
                                if (num18 < num16 && builtObject4.Role != BuiltObjectRole.Base)
                                {
                                    double num19 = Math.Min(1.0, Math.Max(0.01, 1.0 - num18 / num16));
                                    double num20 = timePassed * ((double)weapon.RawDamage * (50.0 / (double)builtObject4.Size) * num19);
                                    double num21 = Galaxy.DetermineAngle(builtObject4.Xpos, builtObject4.Ypos, weapon.X, weapon.Y);
                                    double num22 = Math.Cos(num21) * num20;
                                    double num23 = Math.Sin(num21) * num20;
                                    builtObject4.Xpos += num22;
                                    builtObject4.Ypos += num23;
                                    if (builtObject4.ParentBuiltObject != null || builtObject4.ParentHabitat != null)
                                    {
                                        builtObject4.ParentOffsetX += num22;
                                        builtObject4.ParentOffsetY += num23;
                                    }
                                }
                                if (!(num18 < num17))
                                {
                                    continue;
                                }
                                double num24 = Math.Min(1.0, Math.Max(0.01, 1.0 - num18 / num17));
                                double num25 = Math.Min(60.0, Math.Max(20.0, (double)weapon.RawDamage / ((double)builtObject4.Size / 1000.0) * num24));
                                double num26 = Math.Min(1.0, timePassed * num25);
                                if (time.Millisecond % 100 < 50)
                                {
                                    num26 = 0.0 - num26;
                                }
                                builtObject4.Xpos += num26;
                                builtObject4.Ypos += num26;
                                if (!weapon.ResetNext)
                                {
                                    continue;
                                }
                                double hitPower = Math.Max(1.0, (double)weapon.RawDamage * num24 * Math.Max(0.05, 10.0 / Math.Sqrt(builtObject4.Size)));
                                if (!InflictDamage(builtObject4, weapon, hitPower, time, galaxy, weapon.DistanceTravelled, 0.0) || builtObject4 == null)
                                {
                                    continue;
                                }
                                BuiltObject builtObject5 = builtObject4;
                                if (builtObject5.Empire != null && builtObject5.Empire.PirateEmpireBaseHabitat != null && Empire != null && Empire.PirateEmpireBaseHabitat != null && Empire.PirateEmpireSuperPirates)
                                {
                                    bool flag6 = false;
                                    switch (builtObject5.SubRole)
                                    {
                                        case BuiltObjectSubRole.SmallSpacePort:
                                        case BuiltObjectSubRole.MediumSpacePort:
                                        case BuiltObjectSubRole.LargeSpacePort:
                                            flag6 = true;
                                            break;
                                    }
                                    if (flag6 && Galaxy.Rnd.Next(0, 4) == 1)
                                    {
                                        _Galaxy.FearfulPirateFactionJoinsPlayer(Empire, builtObject5.Empire);
                                    }
                                }
                                ProvideBonusFromPirateBase(Empire, builtObject5);
                                if (Empire != null && Empire.PirateEmpireBaseHabitat != null && _Galaxy.PirateEmpires.Contains(Empire))
                                {
                                    double num27 = Galaxy.CalculateBuiltObjectLootingValue(builtObject5);
                                    num27 *= Empire.ColonyIncomeFactor;
                                    num27 *= Empire.LootingFactor;
                                    num27 = Empire.ApplyCorruptionToIncome(num27);
                                    Empire.StateMoney += num27;
                                    Empire.PirateEconomy.PerformIncome(num27, PirateIncomeType.Looting, galaxy.CurrentStarDate);
                                    EmpireActivity byAttackTarget3 = Empire.PirateMissions.GetByAttackTarget(builtObject5, Empire);
                                    if (byAttackTarget3 != null)
                                    {
                                        Empire.CompletePirateMission(byAttackTarget3);
                                    }
                                }
                            }
                            break;
                        }
                    case ComponentType.WeaponIonPulse:
                    case ComponentType.WeaponAreaDestruction:
                    case ComponentType.WeaponSuperArea:
                        {
                            float num8;
                            if (weapon.DistanceTravelled <= 0f)
                            {
                                flag2 = true;
                                num8 = 1f;
                                if (weapon.Target != null)
                                {
                                    weapon.X = weapon.Target.Xpos;
                                    weapon.Y = weapon.Target.Ypos;
                                }
                                else
                                {
                                    weapon.X = Xpos;
                                    weapon.Y = Ypos;
                                }
                            }
                            else
                            {
                                num8 = (float)weapon.Speed * val;
                            }
                            if (weapon.DistanceTravelled > num7)
                            {
                                weapon.ResetNext = true;
                                break;
                            }
                            double num9 = weapon.DistanceTravelled;
                            weapon.DistanceTravelled += num8;
                            weapon.Power = BaconBuiltObject.WeaponDamageDropoff(this, weapon, num6);
                            GalaxyIndex galaxyIndex = _Galaxy.ResolveIndex(weapon.X, weapon.Y);
                            int x = galaxyIndex.X;
                            int y = galaxyIndex.Y;
                            for (int j = 0; j < galaxy.BuiltObjectIndex[x][y].Count; j++)
                            {
                                BuiltObject builtObject = galaxy.BuiltObjectIndex[x][y][j];
                                if (builtObject == null || builtObject == this)
                                {
                                    continue;
                                }
                                double num10 = galaxy.CalculateDistance(weapon.X, weapon.Y, builtObject.Xpos, builtObject.Ypos);
                                if (!(num10 >= num9) || !(num10 < (double)weapon.DistanceTravelled))
                                {
                                    continue;
                                }
                                double strikeAngle = Galaxy.DetermineAngle(weapon.X, weapon.Y, builtObject.Xpos, builtObject.Ypos);
                                if (weapon.Component.Type == ComponentType.WeaponIonPulse)
                                {
                                    InflictIonDamage(builtObject, weapon, weapon.Power, time, galaxy, weapon.Heading);
                                }
                                else
                                {
                                    if (!InflictDamage(builtObject, weapon, weapon.Power, time, galaxy, weapon.DistanceTravelled, strikeAngle) || !(weapon.Target is BuiltObject))
                                    {
                                        continue;
                                    }
                                    BuiltObject builtObject2 = (BuiltObject)weapon.Target;
                                    if (builtObject2.Empire != null && builtObject2.Empire.PirateEmpireBaseHabitat != null && Empire != null && Empire.PirateEmpireBaseHabitat != null && Empire.PirateEmpireSuperPirates)
                                    {
                                        bool flag3 = false;
                                        switch (builtObject2.SubRole)
                                        {
                                            case BuiltObjectSubRole.SmallSpacePort:
                                            case BuiltObjectSubRole.MediumSpacePort:
                                            case BuiltObjectSubRole.LargeSpacePort:
                                                flag3 = true;
                                                break;
                                        }
                                        if (flag3 && Galaxy.Rnd.Next(0, 4) == 1)
                                        {
                                            _Galaxy.FearfulPirateFactionJoinsPlayer(Empire, builtObject2.Empire);
                                        }
                                    }
                                    ProvideBonusFromPirateBase(Empire, builtObject2);
                                    if (Empire != null && Empire.PirateEmpireBaseHabitat != null && _Galaxy.PirateEmpires.Contains(Empire))
                                    {
                                        double num11 = Galaxy.CalculateBuiltObjectLootingValue(builtObject2);
                                        num11 *= Empire.ColonyIncomeFactor;
                                        num11 *= Empire.LootingFactor;
                                        num11 = Empire.ApplyCorruptionToIncome(num11);
                                        Empire.StateMoney += num11;
                                        Empire.PirateEconomy.PerformIncome(num11, PirateIncomeType.Looting, galaxy.CurrentStarDate);
                                        EmpireActivity byAttackTarget = Empire.PirateMissions.GetByAttackTarget(builtObject2, Empire);
                                        if (byAttackTarget != null)
                                        {
                                            Empire.CompletePirateMission(byAttackTarget);
                                        }
                                    }
                                }
                            }
                            CreatureList creatureList = null;
                            if (NearestSystemStar != null)
                            {
                                if (_Galaxy.Systems.Count > NearestSystemStar.SystemIndex)
                                {
                                    creatureList = _Galaxy.Systems[NearestSystemStar.SystemIndex].Creatures;
                                }
                            }
                            else
                            {
                                GalaxyLocationList galaxyLocationList = _Galaxy.DetermineGalaxyLocationsInRangeAtPoint(Xpos, Ypos, MaximumWeaponsRange, GalaxyLocationType.RestrictedArea);
                                if (galaxyLocationList != null && galaxyLocationList.Count > 0)
                                {
                                    creatureList = new CreatureList();
                                    for (int k = 0; k < galaxyLocationList.Count; k++)
                                    {
                                        creatureList.AddRange(galaxyLocationList[k].RelatedCreatures);
                                    }
                                }
                            }
                            if (creatureList == null || creatureList.Count <= 0)
                            {
                                break;
                            }
                            for (int l = 0; l < creatureList.Count; l++)
                            {
                                Creature creature = creatureList[l];
                                if (creature == null)
                                {
                                    continue;
                                }
                                double num12 = galaxy.CalculateDistance(weapon.X, weapon.Y, creature.Xpos, creature.Ypos);
                                if (!(num12 >= num9) || !(num12 < (double)weapon.DistanceTravelled))
                                {
                                    continue;
                                }
                                double strikeAngle2 = Galaxy.DetermineAngle(weapon.X, weapon.Y, creature.Xpos, creature.Ypos);
                                if (weapon.Component.Type == ComponentType.WeaponIonPulse)
                                {
                                    if (creature.Type == CreatureType.SilverMist)
                                    {
                                        InflictIonDamage(creature, weapon, weapon.Power, time, galaxy, weapon.Heading);
                                    }
                                }
                                else
                                {
                                    InflictDamage(creature, weapon, weapon.Power, time, galaxy, weapon.DistanceTravelled, strikeAngle2);
                                }
                            }
                            break;
                        }
                }
                if (!HasBeenDestroyed && (weapon.Component == null || weapon.Component.Status != ComponentStatus.Damaged))
                {
                    continue;
                }
                break;
            }
        }

        private bool CheckRepairMissionStillValid()
        {
            if (SubRole == BuiltObjectSubRole.ConstructionShip && IsAutoControlled && Mission != null && Mission.Type == BuiltObjectMissionType.BuildRepair)
            {
                BuiltObject secondaryTargetBuiltObject = Mission.SecondaryTargetBuiltObject;
                if (secondaryTargetBuiltObject != null)
                {
                    if (secondaryTargetBuiltObject.Mission != null && secondaryTargetBuiltObject.Mission.Type == BuiltObjectMissionType.Repair && (secondaryTargetBuiltObject.TopSpeed > 0 || secondaryTargetBuiltObject.WarpSpeed > 0))
                    {
                        ClearPreviousMissionRequirements();
                        return false;
                    }
                    if (secondaryTargetBuiltObject.CurrentSpeed > 0f)
                    {
                        ClearPreviousMissionRequirements();
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CheckMissionStillValid(DateTime time)
        {
            bool flag = true;
            if (Mission != null && (Mission.Type == BuiltObjectMissionType.Refuel || Mission.Type == BuiltObjectMissionType.Transport || Mission.Type == BuiltObjectMissionType.Build))
            {
                if (Mission.Type == BuiltObjectMissionType.Refuel && Mission.TargetBuiltObject != null && Mission.TargetBuiltObject.SubRole == BuiltObjectSubRole.ResupplyShip && !Mission.TargetBuiltObject.IsDeployed)
                {
                    ClearPreviousMissionRequirements();
                    return false;
                }
                Command nextDockCommand = Mission.GetNextDockCommand();
                if (nextDockCommand != null && nextDockCommand.Action == CommandAction.Dock && (nextDockCommand.TargetHabitat != null || nextDockCommand.TargetBuiltObject != null || nextDockCommand.TargetCreature != null || nextDockCommand.TargetShipGroup != null))
                {
                    DiplomaticRelation diplomaticRelation = null;
                    Empire empire = null;
                    Blockade blockade = null;
                    int x = 0;
                    int y = 0;
                    Habitat habitat = null;
                    if (nextDockCommand.TargetBuiltObject != null)
                    {
                        BuiltObject targetBuiltObject = nextDockCommand.TargetBuiltObject;
                        int num = -1;
                        if (targetBuiltObject.Cargo != null)
                        {
                            num = targetBuiltObject.Cargo.IndexOf(FuelType, targetBuiltObject.Empire);
                        }
                        if (num >= 0)
                        {
                            _ = targetBuiltObject.Cargo[num].Available;
                        }
                        empire = targetBuiltObject.Empire;
                        diplomaticRelation = Empire.ObtainDiplomaticRelation(targetBuiltObject.Empire);
                        if (targetBuiltObject.IsBlockaded)
                        {
                            blockade = _Galaxy.Blockades[targetBuiltObject];
                        }
                        x = (int)targetBuiltObject.Xpos;
                        y = (int)targetBuiltObject.Ypos;
                    }
                    else if (nextDockCommand.TargetHabitat != null)
                    {
                        Habitat targetHabitat = nextDockCommand.TargetHabitat;
                        int num2 = -1;
                        if (targetHabitat.Cargo != null)
                        {
                            num2 = targetHabitat.Cargo.IndexOf(FuelType, targetHabitat.Empire);
                        }
                        if (num2 >= 0)
                        {
                            _ = targetHabitat.Cargo[num2].Available;
                        }
                        empire = targetHabitat.Empire;
                        diplomaticRelation = Empire.ObtainDiplomaticRelation(targetHabitat.Empire);
                        if (targetHabitat.IsBlockaded)
                        {
                            blockade = _Galaxy.Blockades[targetHabitat];
                        }
                        x = (int)targetHabitat.Xpos;
                        y = (int)targetHabitat.Ypos;
                        habitat = nextDockCommand.TargetHabitat;
                    }
                    if (PirateEmpireId > 0 && ActualEmpire != null && ActualEmpire.PirateEmpireBaseHabitat != null && empire != _Galaxy.IndependentEmpire && Empire != _Galaxy.IndependentEmpire)
                    {
                        PirateRelation pirateRelation = null;
                        if (empire != null)
                        {
                            pirateRelation = ActualEmpire.ObtainPirateRelation(empire);
                        }
                        if (pirateRelation == null || pirateRelation.Type != PirateRelationType.Protection)
                        {
                            ClearPreviousMissionRequirements();
                            RefuelForNextMission = true;
                            flag = false;
                        }
                    }
                    if (habitat != null && (habitat.Population == null || habitat.Population.Count <= 0 || habitat.Population.TotalAmount <= 0))
                    {
                        ClearPreviousMissionRequirements();
                        RefuelForNextMission = true;
                        flag = false;
                    }
                    if (diplomaticRelation != null && (diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions))
                    {
                        bool flag2 = false;
                        if (Mission != null && Mission.Type == BuiltObjectMissionType.LoadTroops && TroopCapacityRemaining >= 100 && habitat != null && habitat.Empire != Empire && habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0 && habitat.InvadingTroops[0].Empire == Empire)
                        {
                            flag2 = true;
                        }
                        if (!flag2)
                        {
                            ClearPreviousMissionRequirements();
                            ThreatEvaluation(_Galaxy, time);
                            flag = false;
                        }
                    }
                    if (blockade != null)
                    {
                        flag = false;
                        DiplomaticRelation diplomaticRelation2 = Empire.ObtainDiplomaticRelation(blockade.Initiator);
                        if (diplomaticRelation2.Type == DiplomaticRelationType.War || diplomaticRelation2.Type == DiplomaticRelationType.TradeSanctions || diplomaticRelation2.Type == DiplomaticRelationType.NotMet || diplomaticRelation2.Type == DiplomaticRelationType.None)
                        {
                            BuiltObjectList ships = null;
                            int num3 = _Galaxy.DetermineBuiltObjectStrengthAtLocation(x, y, blockade.Initiator, 0, includeAllies: false, out ships);
                            int num4 = _Galaxy.DetermineBuiltObjectStrengthAtLocation(x, y, Empire, 0, includeAllies: false, out ships);
                            if (num3 <= num4)
                            {
                                double num5 = 1.0;
                                if (Empire != null && Empire != _Galaxy.IndependentEmpire)
                                {
                                    num5 = Empire.CalculateCautionFactor();
                                }
                                if (Galaxy.Rnd.NextDouble() + 0.7 > num5)
                                {
                                    flag = true;
                                }
                            }
                        }
                        if (!flag)
                        {
                            ClearPreviousMissionRequirements();
                        }
                    }
                }
            }
            else if (Mission != null && (Mission.Type == BuiltObjectMissionType.LoadTroops || Mission.Type == BuiltObjectMissionType.UnloadTroops))
            {
                Command nextDockCommand2 = Mission.GetNextDockCommand();
                if (nextDockCommand2.Action == CommandAction.Dock)
                {
                    if (nextDockCommand2.TargetHabitat != null)
                    {
                        Habitat targetHabitat2 = nextDockCommand2.TargetHabitat;
                        bool flag3 = false;
                        if (Mission != null && Mission.Type == BuiltObjectMissionType.LoadTroops && TroopCapacityRemaining >= 100 && targetHabitat2 != null && targetHabitat2.Empire != Empire && targetHabitat2.InvadingTroops != null && targetHabitat2.InvadingTroops.Count > 0 && targetHabitat2.InvadingTroops[0].Empire == Empire)
                        {
                            flag3 = true;
                        }
                        if (!flag3 && targetHabitat2.Empire != Empire)
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    ClearPreviousMissionRequirements();
                }
            }
            return flag;
        }

        private void ModifyDiplomacyFromAttack(Empire targetEmpire, int evaluationImpact)
        {
            ModifyDiplomacyFromAttack(targetEmpire, attackAffectsRelationship: true, attackAffectsReputation: true, evaluationImpact, 0.0);
        }

        public void ModifyDiplomacyFromAttack(BuiltObject targetBuiltObject)
        {
            if (targetBuiltObject == null)
            {
                return;
            }
            bool flag = true;
            bool attackAffectsReputation = true;
            if (targetBuiltObject.Attackers.ContainsFighterOrBuiltObject(this))
            {
                flag = false;
                attackAffectsReputation = false;
            }
            else if (Attackers.ContainsFighterOrBuiltObject(targetBuiltObject))
            {
                flag = false;
                attackAffectsReputation = false;
            }
            else
            {
                if (targetBuiltObject.Role == BuiltObjectRole.Military && targetBuiltObject.NearestSystemStar != null)
                {
                    SystemInfo systemInfo = _Galaxy.Systems[targetBuiltObject.NearestSystemStar.SystemIndex];
                    if (systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire == Empire && targetBuiltObject.Empire != null)
                    {
                        if (targetBuiltObject.Empire.PirateEmpireBaseHabitat != null)
                        {
                            if (targetBuiltObject.Empire != null)
                            {
                                PirateRelation pirateRelation = Empire.ObtainPirateRelation(targetBuiltObject.Empire);
                                if (pirateRelation.Type != PirateRelationType.Protection)
                                {
                                    attackAffectsReputation = false;
                                }
                            }
                        }
                        else
                        {
                            DiplomaticRelation diplomaticRelation = Empire.ObtainDiplomaticRelation(targetBuiltObject.Empire);
                            if (diplomaticRelation.Type != DiplomaticRelationType.FreeTradeAgreement && diplomaticRelation.Type != DiplomaticRelationType.Protectorate && diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact)
                            {
                                attackAffectsReputation = false;
                            }
                        }
                    }
                }
                flag = ((!Empire.Outlaws.Contains(targetBuiltObject)) ? true : false);
            }
            double reputationImpact = 0.0;
            if (targetBuiltObject.SubRole == BuiltObjectSubRole.PassengerShip)
            {
                reputationImpact = 1.0;
            }
            else if (targetBuiltObject.SubRole == BuiltObjectSubRole.ResortBase)
            {
                reputationImpact = 4.0;
            }
            if (!flag)
            {
                attackAffectsReputation = false;
            }
            ModifyDiplomacyFromAttack(targetBuiltObject.Empire, flag, attackAffectsReputation, 0, reputationImpact);
        }

        private void ModifyDiplomacyFromAttack(Empire targetEmpire, bool attackAffectsRelationship, bool attackAffectsReputation, int evaluationImpact, double reputationImpact)
        {
            if (_Galaxy.PirateEmpires.Contains(targetEmpire))
            {
                if (!attackAffectsRelationship || Empire == null)
                {
                    return;
                }
                PirateRelation pirateRelation = targetEmpire.ObtainPirateRelation(Empire);
                if (pirateRelation.Type != PirateRelationType.None)
                {
                    targetEmpire.ChangePirateRelation(Empire, PirateRelationType.None, _Galaxy.CurrentStarDate);
                }
                targetEmpire.ChangePirateEvaluation(Empire, -5f, PirateRelationEvaluationType.ShipAttacks);
                EmpireActivityList empireActivityList = new EmpireActivityList();
                for (int i = 0; i < targetEmpire.PirateMissions.Count; i++)
                {
                    EmpireActivity empireActivity = targetEmpire.PirateMissions[i];
                    if (empireActivity != null && empireActivity.RequestingEmpire == Empire && (empireActivity.Type == EmpireActivityType.Attack || empireActivity.Type == EmpireActivityType.Defend) && empireActivity.BidTimeRemaining <= 0)
                    {
                        empireActivityList.Add(empireActivity);
                    }
                }
                for (int j = 0; j < empireActivityList.Count; j++)
                {
                    targetEmpire.CancelPirateMission(empireActivityList[j]);
                }
            }
            else
            {
                if (targetEmpire == null || Empire == targetEmpire)
                {
                    return;
                }
                if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
                {
                    if (!attackAffectsRelationship)
                    {
                        return;
                    }
                    PirateRelation pirateRelation2 = targetEmpire.ObtainPirateRelation(Empire);
                    if (pirateRelation2.Type != PirateRelationType.None)
                    {
                        targetEmpire.ChangePirateRelation(Empire, PirateRelationType.None, _Galaxy.CurrentStarDate);
                    }
                    targetEmpire.ChangePirateEvaluation(Empire, -5f, PirateRelationEvaluationType.ShipAttacks);
                    if (Empire.PirateMissions == null)
                    {
                        return;
                    }
                    EmpireActivityList empireActivityList2 = new EmpireActivityList();
                    for (int k = 0; k < Empire.PirateMissions.Count; k++)
                    {
                        EmpireActivity empireActivity2 = Empire.PirateMissions[k];
                        if (empireActivity2 != null && empireActivity2.RequestingEmpire == targetEmpire && (empireActivity2.Type == EmpireActivityType.Attack || empireActivity2.Type == EmpireActivityType.Defend) && empireActivity2.BidTimeRemaining <= 0)
                        {
                            empireActivityList2.Add(empireActivity2);
                        }
                    }
                    for (int l = 0; l < empireActivityList2.Count; l++)
                    {
                        targetEmpire.CancelPirateMission(empireActivityList2[l]);
                    }
                    return;
                }
                DiplomaticRelationType diplomaticRelationType = Empire.DiplomaticRelations[targetEmpire]?.Type ?? DiplomaticRelationType.None;
                if (diplomaticRelationType == DiplomaticRelationType.War)
                {
                    return;
                }
                if (targetEmpire != _Galaxy.IndependentEmpire && attackAffectsRelationship)
                {
                    int num = targetEmpire.Outlaws.IndexOf(this);
                    if (num < 0)
                    {
                        targetEmpire.Outlaws.Add(this);
                    }
                }
                if (attackAffectsReputation)
                {
                    if (reputationImpact > 0.0)
                    {
                        Empire.CivilityRating -= reputationImpact;
                    }
                    else
                    {
                        switch (diplomaticRelationType)
                        {
                            case DiplomaticRelationType.MutualDefensePact:
                            case DiplomaticRelationType.Protectorate:
                                Empire.CivilityRating -= 5.0;
                                break;
                            case DiplomaticRelationType.FreeTradeAgreement:
                                Empire.CivilityRating -= 3.0;
                                break;
                            case DiplomaticRelationType.Truce:
                                Empire.CivilityRating -= 2.0;
                                break;
                            case DiplomaticRelationType.None:
                            case DiplomaticRelationType.SubjugatedDominion:
                                Empire.CivilityRating -= 1.0;
                                break;
                            case DiplomaticRelationType.TradeSanctions:
                                Empire.CivilityRating -= 0.3;
                                break;
                        }
                    }
                }
                if (!attackAffectsRelationship)
                {
                    return;
                }
                if (targetEmpire != null && targetEmpire != _Galaxy.IndependentEmpire && Empire != null && Empire.PirateEmpireBaseHabitat == null && Empire != _Galaxy.IndependentEmpire && !targetEmpire.RecentAttackingEmpires.Contains(Empire))
                {
                    targetEmpire.RecentAttackingEmpires.Add(Empire);
                }
                if (Empire.PirateEmpireBaseHabitat == null && Empire != _Galaxy.IndependentEmpire && targetEmpire != _Galaxy.IndependentEmpire)
                {
                    EmpireEvaluation empireEvaluation = targetEmpire.ObtainEmpireEvaluation(Empire);
                    if (evaluationImpact <= 0)
                    {
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 10.0;
                    }
                    else
                    {
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - (double)evaluationImpact;
                    }
                }
            }
        }

        private void FirePlanetDestroyerAtHabitat(double distanceToTarget, Habitat target)
        {
            if (_FuelHandicapped)
            {
                return;
            }
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (!Weapons[i].IsPlanetDestroyer)
                {
                    continue;
                }
                double num = Weapons[i].Range;
                if (ShipGroup != null)
                {
                    num *= ShipGroup.WeaponsRangeBonus;
                }
                num *= CaptainWeaponsRangeBonus;
                if (num >= distanceToTarget && Weapons[i].DistanceTravelled < 0f && (double)Weapons[i].EnergyRequired <= CurrentEnergy && _tempNow.Subtract(Weapons[i].LastFired).TotalMilliseconds >= (double)Weapons[i].FireRate)
                {
                    if (target.Empire != null && target.Empire != _Galaxy.IndependentEmpire)
                    {
                        ModifyDiplomacyFromAttack(target.Empire, attackAffectsRelationship: true, attackAffectsReputation: true, 80, 0.0);
                        Habitat habitat = Galaxy.DetermineHabitatSystemStar(target);
                        string description = string.Format(TextResolver.GetText("The EMPIRE have destroyed your colony COLONY in the SYSTEM system"), Empire.Name, target.Name, habitat.Name);
                        target.Empire.SendMessageToEmpire(target.Empire, EmpireMessageType.ColonyDestroyed, target, description);
                    }
                    _Galaxy.CheckTriggerEvent(target.GameEventId, ActualEmpire, EventTriggerType.Destroy, null);
                    Weapons[i].WillHitTarget = true;
                    Weapons[i].Heading = (float)Galaxy.DetermineAngle(Xpos, Ypos, target.Xpos, target.Ypos);
                    Weapons[i].LastFired = _tempNow;
                    Weapons[i].X = Xpos;
                    Weapons[i].Y = Ypos;
                    Weapons[i].DistanceTravelled = 1f;
                    Weapons[i].DistanceFromTarget = (float)distanceToTarget;
                    Weapons[i].Target = target;
                    double num2 = Weapons[i].EnergyRequired;
                    if (ShipGroup != null)
                    {
                        num2 /= ShipGroup.ShipEnergyUsageBonus;
                    }
                    num2 /= CaptainShipEnergyUsageBonus;
                    CurrentEnergy -= num2;
                }
            }
        }

        private void BombardTarget(double distanceToTarget, Habitat habitat)
        {
            if (_FuelHandicapped || habitat.HasBeenDestroyed)
            {
                return;
            }
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i].BombardDamage <= 0 || !(Weapons[i].DistanceTravelled < 0f))
                {
                    continue;
                }
                double num = Weapons[i].Range;
                if (ShipGroup != null)
                {
                    num *= ShipGroup.WeaponsRangeBonus;
                }
                num *= CaptainWeaponsRangeBonus;
                if (!(num >= distanceToTarget) || !((double)Weapons[i].EnergyRequired <= CurrentEnergy) || Weapons[i].Component.Type == ComponentType.HyperDeny || Weapons[i].Component.Type == ComponentType.HyperStop)
                {
                    continue;
                }
                TimeSpan timeSpan = _tempNow.Subtract(Weapons[i].LastFired);
                double num2 = Galaxy.Rnd.NextDouble() * 500.0 - 250.0;
                if (timeSpan.TotalMilliseconds >= (double)Weapons[i].FireRate + num2)
                {
                    if (habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire && habitat.Empire.PirateEmpireBaseHabitat == null)
                    {
                        ModifyDiplomacyFromAttack(habitat.Empire, attackAffectsRelationship: true, attackAffectsReputation: false, 1, 0.0);
                    }
                    Weapons[i].WillHitTarget = true;
                    Weapons[i].Heading = (float)Galaxy.DetermineAngle(Xpos, Ypos, habitat.Xpos, habitat.Ypos);
                    double num3 = Galaxy.Rnd.NextDouble() * 0.2;
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num3 *= -1.0;
                    }
                    Weapons[i].Heading += (float)num3;
                    Weapons[i].LastFired = _tempNow;
                    Weapons[i].X = Xpos;
                    Weapons[i].Y = Ypos;
                    Weapons[i].DistanceTravelled = 1f;
                    Weapons[i].DistanceFromTarget = (float)distanceToTarget;
                    Weapons[i].Target = habitat;
                    double num4 = Weapons[i].EnergyRequired;
                    if (ShipGroup != null)
                    {
                        num4 /= ShipGroup.ShipEnergyUsageBonus;
                    }
                    num4 /= CaptainShipEnergyUsageBonus;
                    CurrentEnergy -= num4;
                }
            }
        }

        public bool CheckConventionalWeaponsAvailableToFireAtPassingThreats(DateTime time)
        {
            bool result = false;
            if (MaximumWeaponsRange > 0 && CurrentEnergy > (double)ReactorStorageCapacity * 0.25)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapon weapon = Weapons[i];
                    if (weapon != null && weapon.Component != null)
                    {
                        bool flag = true;
                        switch (weapon.Component.Type)
                        {
                            case ComponentType.WeaponBombard:
                            case ComponentType.WeaponPointDefense:
                            case ComponentType.WeaponIonDefense:
                            case ComponentType.WeaponTractorBeam:
                            case ComponentType.AssaultPod:
                            case ComponentType.HyperDeny:
                            case ComponentType.HyperStop:
                                flag = false;
                                break;
                        }
                        if (flag && CurrentEnergy > (double)weapon.EnergyRequired && _tempNow.Subtract(weapon.LastFired).TotalMilliseconds >= (double)weapon.FireRate)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private void FireWeaponTypeAtTarget(ComponentType weaponType, double distanceToTarget, StellarObject target, DateTime time, bool mayModifyDiplomacy)
        {
            FireWeaponTypeAtTarget(weaponType, distanceToTarget, target, time, mayModifyDiplomacy, 1.0);
        }

        private void FireWeaponTypeAtTarget(ComponentType weaponType, double distanceToTarget, StellarObject target, DateTime time, bool mayModifyDiplomacy, double maximumPortion)
        {
            WeaponList weaponList = new WeaponList();
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapon weapon = Weapons[i];
                if (weapon != null && weapon.Component != null && weapon.Component.Type == weaponType)
                {
                    weaponList.Add(weapon);
                }
            }
            FireWeaponSetAtTarget(weaponList, distanceToTarget, target, time, mayModifyDiplomacy, maximumPortion);
        }

        private void FireWeaponSetAtTarget(WeaponList weapons, double distanceToTarget, StellarObject target, DateTime time, bool mayModifyDiplomacy, double maximumPortion)
        {
            if (_FuelHandicapped || target.HasBeenDestroyed)
            {
                return;
            }
            int num = Math.Max(1, (int)((double)weapons.Count * maximumPortion + 0.5));
            int num2 = 0;
            for (int i = 0; i < weapons.Count; i++)
            {
                Weapon weapon = weapons[i];
                if (weapon != null && weapon.DistanceTravelled < 0f)
                {
                    double num3 = weapon.Range;
                    if (ShipGroup != null)
                    {
                        num3 *= ShipGroup.WeaponsRangeBonus;
                    }
                    num3 *= CaptainWeaponsRangeBonus;
                    if (num3 >= distanceToTarget && (double)weapon.EnergyRequired <= CurrentEnergy)
                    {
                        ComponentType type = weapon.Component.Type;
                        if (type != ComponentType.HyperDeny && type != ComponentType.HyperStop && type != ComponentType.WeaponPointDefense && type != ComponentType.AssaultPod)
                        {
                            bool flag = true;
                            if (IsPlanetDestroyer && (type == ComponentType.WeaponSuperBeam || type == ComponentType.WeaponSuperTorpedo || type == ComponentType.WeaponSuperMissile || type == ComponentType.WeaponSuperRailGun || type == ComponentType.WeaponSuperPhaser) && _ColonyToAttack != null)
                            {
                                double num4 = _Galaxy.CalculateDistance(Xpos, Ypos, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos);
                                if (num4 <= num3 + 300.0)
                                {
                                    flag = false;
                                }
                            }
                            if (type == ComponentType.WeaponTractorBeam)
                            {
                                if (target is Creature)
                                {
                                    if (target.CurrentTarget != this)
                                    {
                                    }
                                }
                                else if (target is BuiltObject && ((BuiltObject)target).Role == BuiltObjectRole.Base)
                                {
                                    flag = false;
                                }
                            }
                            ComponentType componentType = type;
                            if (componentType == ComponentType.WeaponIonPulse || componentType == ComponentType.WeaponAreaGravity || componentType == ComponentType.WeaponAreaDestruction)
                            {
                                flag = CheckFireAreaWeaponAtTarget(weapon, target);
                            }
                            if (flag)
                            {
                                TimeSpan timeSpan = _tempNow.Subtract(weapon.LastFired);
                                double num5 = Galaxy.Rnd.NextDouble() * 800.0 - 400.0;
                                if (timeSpan.TotalMilliseconds >= (double)weapon.FireRate + num5)
                                {
                                    if (mayModifyDiplomacy && target is BuiltObject)
                                    {
                                        ModifyDiplomacyFromAttack((BuiltObject)target);
                                    }
                                    double hitRangeChance = 0.0;
                                    bool willHit = DetermineHitTarget(_Galaxy, weapon, target, distanceToTarget, out hitRangeChance);
                                    weapon.Fire(_Galaxy, this, target, distanceToTarget, time, willHit, hitRangeChance);
                                    num2++;
                                }
                            }
                        }
                    }
                }
                if (num2 >= num)
                {
                    break;
                }
            }
        }

        private void FireWeaponsAtTarget(double distanceToTarget, StellarObject target, DateTime time)
        {
            FireWeaponsAtTarget(distanceToTarget, target, time, mayModifyDiplomacy: true);
        }

        private void FireWeaponsAtTarget(double distanceToTarget, StellarObject target, DateTime time, bool mayModifyDiplomacy)
        {
            if (_FuelHandicapped || target.HasBeenDestroyed)
            {
                return;
            }
            bool flag = false;
            if (IonWeaponPower > 0 && target is Creature)
            {
                Creature creature = (Creature)target;
                if (creature.Type == CreatureType.SilverMist)
                {
                    flag = true;
                    for (int i = 0; i < Weapons.Count; i++)
                    {
                        Weapon weapon = Weapons[i];
                        if (weapon == null || weapon.Component == null || (weapon.Component.Type != ComponentType.WeaponIonCannon && weapon.Component.Type != ComponentType.WeaponIonPulse) || !(weapon.DistanceTravelled < 0f))
                        {
                            continue;
                        }
                        double num = weapon.Range;
                        if (ShipGroup != null)
                        {
                            num *= ShipGroup.WeaponsRangeBonus;
                        }
                        num *= CaptainWeaponsRangeBonus;
                        if (num >= distanceToTarget && (double)weapon.EnergyRequired <= CurrentEnergy)
                        {
                            TimeSpan timeSpan = _tempNow.Subtract(weapon.LastFired);
                            double num2 = Galaxy.Rnd.NextDouble() * 800.0 - 400.0;
                            if (timeSpan.TotalMilliseconds >= (double)weapon.FireRate + num2)
                            {
                                double hitRangeChance = 0.0;
                                bool willHit = DetermineHitTarget(_Galaxy, weapon, target, distanceToTarget, out hitRangeChance);
                                weapon.Fire(_Galaxy, this, target, distanceToTarget, time, willHit, hitRangeChance);
                            }
                        }
                    }
                }
            }
            int num3 = 0;
            if (flag)
            {
                num3 = (int)((double)ReactorStorageCapacity * 0.5);
            }
            for (int j = 0; j < Weapons.Count; j++)
            {
                Weapon weapon2 = Weapons[j];
                if (weapon2 == null || !(weapon2.DistanceTravelled < 0f))
                {
                    continue;
                }
                double num4 = weapon2.Range;
                if (ShipGroup != null)
                {
                    num4 *= ShipGroup.WeaponsRangeBonus;
                }
                num4 *= CaptainWeaponsRangeBonus;
                if (!(num4 >= distanceToTarget) || !((double)(weapon2.EnergyRequired + num3) <= CurrentEnergy))
                {
                    continue;
                }
                ComponentType type = weapon2.Component.Type;
                if (type == ComponentType.HyperDeny || type == ComponentType.HyperStop || type == ComponentType.WeaponPointDefense || type == ComponentType.AssaultPod)
                {
                    continue;
                }
                bool flag2 = true;
                if (IsPlanetDestroyer && (type == ComponentType.WeaponSuperBeam || type == ComponentType.WeaponSuperTorpedo || type == ComponentType.WeaponSuperMissile || type == ComponentType.WeaponSuperRailGun || type == ComponentType.WeaponSuperPhaser) && _ColonyToAttack != null)
                {
                    double num5 = _Galaxy.CalculateDistance(Xpos, Ypos, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos);
                    if (num5 <= num4 + 300.0)
                    {
                        flag2 = false;
                    }
                }
                if (type == ComponentType.WeaponTractorBeam)
                {
                    if (target is Creature)
                    {
                        if (target.CurrentTarget != this)
                        {
                        }
                    }
                    else if (target is BuiltObject && ((BuiltObject)target).Role == BuiltObjectRole.Base)
                    {
                        flag2 = false;
                    }
                }
                ComponentType componentType = type;
                if (componentType == ComponentType.WeaponIonPulse || componentType == ComponentType.WeaponAreaGravity || componentType == ComponentType.WeaponAreaDestruction)
                {
                    flag2 = CheckFireAreaWeaponAtTarget(weapon2, target);
                }
                if (!flag2)
                {
                    continue;
                }
                TimeSpan timeSpan2 = _tempNow.Subtract(weapon2.LastFired);
                double num6 = Galaxy.Rnd.NextDouble() * 800.0 - 400.0;
                if (timeSpan2.TotalMilliseconds >= (double)weapon2.FireRate + num6)
                {
                    if (mayModifyDiplomacy && target is BuiltObject)
                    {
                        ModifyDiplomacyFromAttack((BuiltObject)target);
                    }
                    double hitRangeChance2 = 0.0;
                    bool willHit2 = DetermineHitTarget(_Galaxy, weapon2, target, distanceToTarget, out hitRangeChance2);
                    weapon2.Fire(_Galaxy, this, target, distanceToTarget, time, willHit2, hitRangeChance2);
                }
            }
        }

        private bool DetermineHitTarget(Galaxy galaxy, Weapon weapon, Weapon targetWeaponBlast, double distanceToTarget, out double hitRangeChance)
        {
            double num = weapon.Range;
            if (ShipGroup != null)
            {
                num *= ShipGroup.WeaponsRangeBonus;
            }
            num *= CaptainWeaponsRangeBonus;
            double num2 = num - distanceToTarget;
            hitRangeChance = 0.15 + Math.Max(0.0, num2 / num);
            double val = 10.0 / Math.Max(1.0, targetWeaponBlast.Speed);
            val = Math.Max(0.7, Math.Min(val, 5.0));
            val *= 2.0;
            double num3 = 0.0;
            double num4 = TargettingModifier + FleetTargettingBonus;
            if (Empire != null && Empire.RaceEventType == RaceEventType.PredictiveHistory)
            {
                num4 += 20.0;
            }
            double num5 = (num4 - num3) / 100.0;
            double num6 = val * (hitRangeChance + Galaxy.Rnd.NextDouble() + num5);
            if (ShipGroup != null)
            {
                num6 *= ShipGroup.TargetingBonus;
            }
            num6 *= CaptainTargetingBonus;
            if (num6 > 0.5 && Galaxy.Rnd.Next(0, 15) == 7)
            {
                num6 = 0.0;
            }
            else if (num6 <= 0.5 && num2 > 0.0 && Galaxy.Rnd.Next(0, 15) == 7)
            {
                num6 = 1.0;
            }
            if (num6 > 0.5)
            {
                return true;
            }
            return false;
        }

        private bool DetermineHitTarget(Galaxy galaxy, Weapon weapon, StellarObject target, double distanceToTarget, out double hitRangeChance)
        {
            double num = weapon.Range;
            if (ShipGroup != null)
            {
                num *= ShipGroup.WeaponsRangeBonus;
            }
            num *= CaptainWeaponsRangeBonus;
            double num2 = num - distanceToTarget;
            hitRangeChance = 0.15 + Math.Max(0.0, num2 / num);
            double val = 10.0 / Math.Max(1.0, target.CurrentSpeed);
            val = Math.Max(0.7, Math.Min(val, 5.0));
            if (target is Fighter)
            {
                val = ((weapon.Component.Type != ComponentType.WeaponPointDefense) ? (val / 1.1) : (val * 2.0));
            }
            double num3 = 0.0;
            ShipGroup shipGroup = null;
            double num4 = 1.0;
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                num3 = builtObject.CountermeasureModifier + builtObject.FleetCountermeasureBonus;
                num4 = builtObject.CaptainCountermeasuresBonus;
                if (builtObject.Empire != null)
                {
                    num3 += (builtObject.Empire.CountermeasuresFactor - 1.0) * 100.0;
                    if (builtObject.Empire.RaceEventType == RaceEventType.PredictiveHistory)
                    {
                        num3 += 20.0;
                    }
                }
                shipGroup = builtObject.ShipGroup;
            }
            else if (target is Fighter)
            {
                Fighter fighter = (Fighter)target;
                num3 = fighter.Specification.CountermeasureModifier;
                if (fighter.ParentBuiltObject != null && !fighter.ParentBuiltObject.HasBeenDestroyed)
                {
                    num3 += (double)fighter.ParentBuiltObject.FleetCountermeasureBonus;
                }
                if (fighter.Empire != null)
                {
                    num3 += (fighter.Empire.CountermeasuresFactor - 1.0) * 100.0;
                    if (fighter.Empire.RaceEventType == RaceEventType.PredictiveHistory)
                    {
                        num3 += 20.0;
                    }
                }
            }
            double num5 = TargettingModifier + FleetTargettingBonus;
            if (Empire != null)
            {
                num5 += (Empire.TargettingFactor - 1.0) * 100.0;
                if (Empire.RaceEventType == RaceEventType.PredictiveHistory)
                {
                    num5 += 20.0;
                }
            }
            switch (weapon.Component.Type)
            {
                case ComponentType.WeaponTractorBeam:
                    num5 += 50.0;
                    break;
                case ComponentType.WeaponPhaser:
                case ComponentType.WeaponSuperPhaser:
                    num5 += 10.0;
                    break;
                case ComponentType.WeaponRailGun:
                case ComponentType.WeaponSuperRailGun:
                    num5 -= 10.0;
                    break;
                case ComponentType.WeaponGravityBeam:
                    num5 -= 10.0;
                    break;
            }
            double num6 = (num5 - num3) / 100.0;
            double num7 = val * (hitRangeChance + Galaxy.Rnd.NextDouble() + num6);
            if (ShipGroup != null)
            {
                num7 *= ShipGroup.TargetingBonus;
            }
            num7 *= CaptainTargetingBonus;
            if (shipGroup != null)
            {
                num7 /= shipGroup.CountermeasuresBonus;
            }
            num7 /= num4;
            if (num7 > 0.5 && Galaxy.Rnd.Next(0, 12) == 0)
            {
                num7 = 0.0;
            }
            else if (num7 <= 0.5 && num2 > 0.0 && Galaxy.Rnd.Next(0, 12) == 0)
            {
                num7 = 1.0;
            }
            if (num7 > 0.5)
            {
                return true;
            }
            return false;
        }

        private BuiltObjectList DetectShipsDockingAtSpacePort(BuiltObject spacePort)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < spacePort.DockingBayWaitQueue.Count; i++)
            {
                BuiltObject builtObject = spacePort.DockingBayWaitQueue[i];
                if (builtObject.Mission != null)
                {
                    Command command = builtObject.Mission.FastPeekCurrentCommand();
                    if (command != null && command.Action == CommandAction.Dock && command.TargetBuiltObject != null && command.TargetBuiltObject == spacePort)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        private BuiltObjectList DetectShipsDockingAtHabitat(Habitat habitat)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < habitat.DockingBayWaitQueue.Count; i++)
            {
                BuiltObject builtObject = habitat.DockingBayWaitQueue[i];
                if (builtObject.Mission != null)
                {
                    Command command = builtObject.Mission.FastPeekCurrentCommand();
                    if (command != null && command.Action == CommandAction.Dock && command.TargetHabitat != null && command.TargetHabitat == habitat)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        private bool ShouldInvadeColony()
        {
            return ShouldInvadeColony(BuiltObjectMissionType.Attack);
        }

        private bool ShouldInvadeColony(BuiltObjectMissionType missionType)
        {
            bool flag = false;
            if (Mission != null && (Mission.Type == BuiltObjectMissionType.Refuel || Mission.Type == BuiltObjectMissionType.Retire || Mission.Type == BuiltObjectMissionType.Retrofit || Mission.Type == BuiltObjectMissionType.Repair || Mission.Type == BuiltObjectMissionType.Escape))
            {
                return false;
            }
            if (_ColonyToAttack != null && !IsPlanetDestroyer)
            {
                if (missionType == BuiltObjectMissionType.Bombard || missionType == BuiltObjectMissionType.Raid)
                {
                    flag = true;
                }
                else
                {
                    switch (Design.TacticsInvasion)
                    {
                        case InvasionTactics.DoNotInvade:
                            _ColonyToAttack = null;
                            flag = false;
                            break;
                        case InvasionTactics.InvadeWhenClear:
                            {
                                int num = _Galaxy.DetermineDefendingBaseStrengthAtColony(_ColonyToAttack);
                                if (num <= 0)
                                {
                                    flag = true;
                                }
                                break;
                            }
                        case InvasionTactics.InvadeImmediately:
                            flag = true;
                            break;
                    }
                }
                if (flag)
                {
                    if (missionType == BuiltObjectMissionType.Attack && Mission != null && Mission.Type == BuiltObjectMissionType.Bombard && Mission.TargetHabitat != null && Mission.TargetHabitat == _ColonyToAttack)
                    {
                        flag = false;
                    }
                    if (missionType != BuiltObjectMissionType.Raid && (_ColonyToAttack.Owner == null || _ColonyToAttack.Empire == Empire))
                    {
                        _ColonyToAttack = null;
                        flag = false;
                    }
                }
            }
            return flag;
        }

        private BuiltObject DetermineShipGroupTarget(ShipGroup targettedShipGroup, DateTime time)
        {
            PerformThreatEvaluation(time);
            BuiltObjectList builtObjectList = new BuiltObjectList();
            if (_Threats.Length > 0)
            {
                for (int i = 0; i < _Threats.Length; i++)
                {
                    StellarObject stellarObject = _Threats[i];
                    if (!(stellarObject is BuiltObject))
                    {
                        continue;
                    }
                    BuiltObject builtObject = (BuiltObject)stellarObject;
                    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.ShipGroup == targettedShipGroup)
                    {
                        if (!EvaluateAdequateAttackers(builtObject))
                        {
                            return builtObject;
                        }
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            if (builtObjectList.Count > 0)
            {
                for (int j = 0; j < builtObjectList.Count; j++)
                {
                    BuiltObject builtObject2 = builtObjectList[j];
                    if (builtObject2 != null)
                    {
                        return builtObject2;
                    }
                }
            }
            return null;
        }

        private float DetermineTargetSpeed(StellarObject target)
        {
            float num = target.CurrentSpeed;
            if (target.ParentHabitat != null)
            {
                num = ((target.ParentHabitat.Parent == null) ? (num + (float)(int)target.ParentHabitat.OrbitSpeed) : (num + ((float)(int)target.ParentHabitat.OrbitSpeed + (float)(int)target.ParentHabitat.Parent.OrbitSpeed)));
            }
            else if (target.ParentBuiltObject != null)
            {
                num = ((target.ParentBuiltObject.ParentHabitat == null) ? (num + target.ParentBuiltObject.CurrentSpeed) : ((target.ParentBuiltObject.ParentHabitat.Parent == null) ? (num + (target.ParentBuiltObject.CurrentSpeed + (float)(int)target.ParentBuiltObject.ParentHabitat.OrbitSpeed)) : (num + (target.ParentBuiltObject.CurrentSpeed + (float)(int)target.ParentBuiltObject.ParentHabitat.OrbitSpeed + (float)(int)target.ParentBuiltObject.ParentHabitat.Parent.OrbitSpeed))));
            }
            return num;
        }

        private void SetOptimalAttackRangesBoarding()
        {
            if (AssaultRange > 0)
            {
                OptimalMaximumAttackRange = AssaultRange;
                OptimalMinimumAttackRange = (double)AssaultRange * 0.7;
            }
        }

        private void SetOptimalAttackRanges(StellarObject target)
        {
            SetOptimalAttackRanges(target, CommandAction.Attack);
        }

        private void SetOptimalAttackRanges(StellarObject target, CommandAction actionType)
        {
            if (target == null)
            {
                return;
            }
            BattleTactics battleTactics = DetermineTacticsAgainstTarget(target);
            bool flag = false;
            if ((actionType == CommandAction.Capture || actionType == CommandAction.Raid) && AssaultStrength > 0)
            {
                if (target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)target;
                    if (builtObject.CurrentShields < (float)AssaultShieldPenetration && CalculateAvailableAssaultPodAttackStrength(_Galaxy.CurrentDateTime) > 0)
                    {
                        flag = true;
                    }
                }
                else if (target is Habitat)
                {
                    Habitat habitat = (Habitat)target;
                    if (!habitat.PlanetaryShieldPresent && CalculateAvailableAssaultPodAttackStrength(_Galaxy.CurrentDateTime) > 0)
                    {
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                SetOptimalAttackRangesBoarding();
            }
            else
            {
                switch (battleTactics)
                {
                    case BattleTactics.Evade:
                        {
                            int num = 200;
                            if (target is BuiltObject)
                            {
                                num = ((BuiltObject)target).MaximumWeaponsRange;
                            }
                            OptimalMinimumAttackRange = (int)((double)num * 1.4);
                            OptimalMaximumAttackRange = (int)((double)num * 1.8);
                            break;
                        }
                    case BattleTactics.Standoff:
                        OptimalMinimumAttackRange = (int)((double)StandoffWeaponsMaxRange * 0.65);
                        OptimalMaximumAttackRange = (int)((double)StandoffWeaponsMaxRange * 0.9);
                        break;
                    case BattleTactics.AllWeapons:
                        OptimalMinimumAttackRange = (int)((double)BeamWeaponsMinRange * 0.65);
                        OptimalMaximumAttackRange = (int)((double)BeamWeaponsMinRange * 0.9);
                        break;
                    case BattleTactics.PointBlank:
                        OptimalMinimumAttackRange = (int)((double)Galaxy.PointBlankWeaponsRange * 0.7);
                        OptimalMaximumAttackRange = Galaxy.PointBlankWeaponsRange;
                        break;
                }
            }
            float num2 = DetermineTargetSpeed(target);
            OptimalMaximumAttackRange -= num2;
            OptimalMaximumAttackRange = Math.Max(Galaxy.PointBlankWeaponsRange, Math.Max(OptimalMaximumAttackRange, OptimalMinimumAttackRange));
            OptimalMinimumAttackRange = Math.Max(0.0, Math.Min(OptimalMaximumAttackRange - 10.0, OptimalMinimumAttackRange));
        }

        private void ModifyAttackRangeByTargetSpeed()
        {
            if (CurrentTarget != null)
            {
                ModifyAttackRangeByTargetSpeed(CurrentTarget);
            }
        }

        private void ModifyAttackRangeByTargetSpeed(StellarObject target)
        {
            double num = 0.0;
            if (!(target.CurrentSpeed > 0f) || !(target is BuiltObject))
            {
                return;
            }
            BuiltObject builtObject = (BuiltObject)target;
            double num2 = Galaxy.DetermineAngle(builtObject.Xpos, builtObject.Ypos, Xpos, Ypos);
            double num3 = Math.Abs((double)builtObject.Heading - num2);
            if (num3 > Math.PI / 2.0)
            {
                BattleTactics battleTactics = DetermineTacticsAgainstTarget(target);
                Weapon weapon = null;
                switch (battleTactics)
                {
                    case BattleTactics.Standoff:
                        {
                            if (Weapons == null)
                            {
                                break;
                            }
                            for (int k = 0; k < Weapons.Count; k++)
                            {
                                if (Weapons[k].Component != null && Weapons[k].Component.Category == ComponentCategoryType.WeaponTorpedo)
                                {
                                    weapon = Weapons[k];
                                    break;
                                }
                            }
                            if (weapon != null)
                            {
                                break;
                            }
                            for (int l = 0; l < Weapons.Count; l++)
                            {
                                if (Weapons[l].Component != null && Weapons[l].Component.Category == ComponentCategoryType.WeaponBeam)
                                {
                                    weapon = Weapons[l];
                                    break;
                                }
                            }
                            break;
                        }
                    case BattleTactics.AllWeapons:
                        {
                            if (Weapons == null)
                            {
                                break;
                            }
                            for (int i = 0; i < Weapons.Count; i++)
                            {
                                if (Weapons[i].Component != null && Weapons[i].Component.Category == ComponentCategoryType.WeaponBeam)
                                {
                                    weapon = Weapons[i];
                                    break;
                                }
                            }
                            if (weapon != null)
                            {
                                break;
                            }
                            for (int j = 0; j < Weapons.Count; j++)
                            {
                                if (Weapons[j].Component != null && Weapons[j].Component.Category == ComponentCategoryType.WeaponTorpedo)
                                {
                                    weapon = Weapons[j];
                                    break;
                                }
                            }
                            break;
                        }
                }
                if (weapon != null)
                {
                    double num4 = weapon.Range;
                    if (ShipGroup != null)
                    {
                        num4 *= ShipGroup.WeaponsRangeBonus;
                    }
                    num4 *= CaptainWeaponsRangeBonus;
                    double num5 = num4 / (double)weapon.Speed;
                    double num6 = (double)weapon.Speed - (double)target.CurrentSpeed;
                    num = num6 * num5;
                }
                if (num > 0.0)
                {
                    if (OptimalMaximumAttackRange > num)
                    {
                        OptimalMinimumAttackRange = (int)(num * 0.65);
                        OptimalMaximumAttackRange = (int)(num * 0.9);
                    }
                }
                else
                {
                    SetOptimalAttackRanges(target);
                }
            }
            else
            {
                SetOptimalAttackRanges(target);
            }
        }

        private bool CheckFightersOnboardAndRetrieve()
        {
            return BaconBuiltObject.CheckFightersOnboardAndRetrieve(this);
        }

        private void CheckColonyShipMissionCancelled(int cancelReasonCode)
        {
            if (SubRole == BuiltObjectSubRole.ColonyShip && Mission != null && Mission.Type == BuiltObjectMissionType.Colonize && Mission.TargetHabitat != null)
            {
                string failureReason = string.Empty;
                switch (cancelReasonCode)
                {
                    case 0:
                        failureReason = TextResolver.GetText("Colony Failure Under Attack");
                        break;
                    case 1:
                        failureReason = string.Format(TextResolver.GetText("Colony Failure Already Colonized"), Galaxy.ResolveDescription(Mission.TargetHabitat.Category).ToLower(CultureInfo.InvariantCulture));
                        break;
                    case 2:
                        failureReason = string.Format(TextResolver.GetText("Colony Failure Cannot Colonize"), Galaxy.ResolveDescription(Mission.TargetHabitat.Category).ToLower(CultureInfo.InvariantCulture));
                        break;
                    case 3:
                        failureReason = string.Format(TextResolver.GetText("Colony Failure Colony Destroyed"), Galaxy.ResolveDescription(Mission.TargetHabitat.Category).ToLower(CultureInfo.InvariantCulture));
                        break;
                }
                SendMessageCannotColonize(Mission.TargetHabitat, failureReason);
            }
        }

        private void SendMessageCannotColonize(Habitat colonizationTarget, string failureReason)
        {
            if (colonizationTarget != null && Empire != null)
            {
                string empty = string.Empty;
                Habitat habitat = Galaxy.DetermineHabitatSystemStar(colonizationTarget);
                empty += string.Format(TextResolver.GetText("Colony Failure Message"), Name, Galaxy.ResolveDescription(colonizationTarget.Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(colonizationTarget.Category).ToLower(CultureInfo.InvariantCulture), colonizationTarget.Name, habitat.Name);
                empty = empty + ". " + failureReason;
                Empire.SendMessageToEmpire(Empire, EmpireMessageType.ColonyShipMissionCancelled, this, empty, new Point((int)colonizationTarget.Xpos, (int)colonizationTarget.Ypos), string.Empty);
            }
        }

        private void FinalizeContractsNotPresentAtLoad(StellarObject dockedAt)
        {
            if (dockedAt != null && dockedAt.Cargo != null && ContractsToFulfill != null)
            {
                for (int i = 0; i < ContractsToFulfill.Count; i++)
                {
                    Contract contract = ContractsToFulfill[i];
                    if (contract == null)
                    {
                        continue;
                    }
                    int num = contract.AmountToFulfill - contract.AmountPickedUp;
                    if (num <= 0)
                    {
                        continue;
                    }
                    if (contract.ResourceId >= 0)
                    {
                        Resource resource = new Resource((byte)contract.ResourceId);
                        Cargo cargo = dockedAt.Cargo.GetCargo(resource, contract.BuyerEmpireId);
                        if (cargo != null)
                        {
                            cargo.Reserved -= num;
                        }
                    }
                    else if (contract.ComponentId >= 0)
                    {
                        Component component = new Component(contract.ComponentId);
                        Cargo cargo2 = dockedAt.Cargo.GetCargo(component, contract.BuyerEmpireId);
                        if (cargo2 != null)
                        {
                            cargo2.Reserved -= num;
                        }
                    }
                    contract.AmountToFulfill -= num;
                    contract.AmountPickedUp = contract.AmountToFulfill;
                }
            }
            if (SubRole == BuiltObjectSubRole.ConstructionShip && _ContractsToFulfill != null)
            {
                _ContractsToFulfill.Clear();
            }
        }

        public void ExitHyperjump()
        {
            BaconBuiltObject.ExitHyperjump(this);
        }

        private double ExecuteCommands(Galaxy galaxy, double timePassed, DateTime time, long starDate)
        {
            double result = 0.0;
            BuiltObjectMission mission = Mission;
            Command command = null;
            if (mission != null)
            {
                command = mission.FastPeekCurrentCommand();
            }
            _ExecutingShipGroupCommand = false;
            if (command == null || command.Action != CommandAction.Attack)
            {
                HyperDenyActive = false;
            }
            double parentXPos = -2000000001.0;
            double parentYPos = -2000000001.0;
            double targetArrivalDistance = 0.0;
            if (EvaluateRelativeToParent(ref parentXPos, ref parentYPos, out targetArrivalDistance, galaxy))
            {
                if (command != null)
                {
                    if (command.Action != CommandAction.ImpulseTo && command.Action != CommandAction.MoveTo && command.Action != CommandAction.SprintTo && command.Action != CommandAction.Escort && command.Action != CommandAction.ConditionalHyperTo && command.Action != CommandAction.HyperTo)
                    {
                        Xpos = parentXPos + ParentOffsetX;
                        Ypos = parentYPos + ParentOffsetY;
                    }
                }
                else
                {
                    Xpos = parentXPos + ParentOffsetX;
                    Ypos = parentYPos + ParentOffsetY;
                }
            }
            if (command != null)
            {
                if (DockedAt != null && command.Action != CommandAction.Dock && ParentOffsetX > -2000000001.0 && ParentOffsetY > -2000000001.0)
                {
                    Xpos = DockedAt.Xpos + ParentOffsetX;
                    Ypos = DockedAt.Ypos + ParentOffsetY;
                }
            }
            else if (DockedAt != null && ParentOffsetX > -2000000001.0 && ParentOffsetY > -2000000001.0)
            {
                Xpos = DockedAt.Xpos + ParentOffsetX;
                Ypos = DockedAt.Ypos + ParentOffsetY;
            }
            double xpos = Xpos;
            double ypos = Ypos;
            GalaxyIndex galaxyIndex = _Galaxy.ResolveIndex(xpos, ypos);
            int x = galaxyIndex.X;
            int y = galaxyIndex.Y;
            if (Empire == null)
            {
                return 0.0;
            }
            if (BuiltAt != null && (Role != BuiltObjectRole.Base || ParentHabitat == null))
            {
                Xpos = BuiltAt.Xpos;
                Ypos = BuiltAt.Ypos;
                UpdateIndexesForMovement(x, y, galaxy, performIndexCheck: false);
            }
            else if (command != null)
            {
                double num = -2000000001.0;
                double num2 = -2000000001.0;
                if (command.TargetBuiltObject != null || command.TargetHabitat != null || command.TargetShipGroup != null || command.TargetCreature != null)
                {
                    if (command.TargetBuiltObject != null)
                    {
                        BuiltObject targetBuiltObject = command.TargetBuiltObject;
                        if (targetBuiltObject.HasBeenDestroyed)
                        {
                            if (Attackers.Contains(CurrentTarget))
                            {
                                Attackers.Remove(CurrentTarget);
                            }
                            CurrentTarget = null;
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            return timePassed;
                        }
                        num = targetBuiltObject.Xpos;
                        num2 = targetBuiltObject.Ypos;
                    }
                    else if (command.TargetCreature != null)
                    {
                        Creature targetCreature = command.TargetCreature;
                        if (targetCreature.HasBeenDestroyed)
                        {
                            if (Attackers.Contains(CurrentTarget))
                            {
                                Attackers.Remove(CurrentTarget);
                            }
                            CurrentTarget = null;
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            return timePassed;
                        }
                        num = targetCreature.Xpos;
                        num2 = targetCreature.Ypos;
                    }
                    else if (command.TargetHabitat != null)
                    {
                        Habitat targetHabitat = command.TargetHabitat;
                        if (targetHabitat.Category == HabitatCategoryType.Moon && targetHabitat.Parent != null)
                        {
                            targetHabitat.Parent.DoTasks(time);
                        }
                        num = targetHabitat.Xpos;
                        num2 = targetHabitat.Ypos;
                    }
                    else if (command.TargetShipGroup != null)
                    {
                        ShipGroup targetShipGroup = command.TargetShipGroup;
                        if (targetShipGroup.Ships.Count > 0)
                        {
                            num = targetShipGroup.LeadShip.Xpos;
                            num2 = targetShipGroup.LeadShip.Ypos;
                        }
                        else if (!_ExecutingShipGroupCommand)
                        {
                            CurrentTarget = null;
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            return timePassed;
                        }
                    }
                    if (command.TargetRelativeXpos > -2E+09f && command.TargetRelativeYpos > -2E+09f && command.TargetRelativeXpos != 0f && command.TargetRelativeYpos != 0f)
                    {
                        num += (double)command.TargetRelativeXpos;
                        num2 += (double)command.TargetRelativeYpos;
                    }
                }
                if (command.Xpos > -2E+09f && command.Ypos > -2E+09f)
                {
                    num = command.Xpos;
                    num2 = command.Ypos;
                }
                HyperDenyActive = false;
                switch (command.Action)
                {
                    case CommandAction.Blockade:
                        {
                            if (FirstExecutionOfCommand)
                            {
                                double num26 = _Galaxy.CalculateDistance(Xpos, Ypos, num, num2);
                                if (num26 > (double)(Galaxy.ParentRelativeRange * 2))
                                {
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                    break;
                                }
                                if (Empire != null)
                                {
                                    if (command.TargetHabitat != null)
                                    {
                                        Habitat targetHabitat3 = command.TargetHabitat;
                                        Empire.SetupBlockade(targetHabitat3);
                                    }
                                    else if (command.TargetBuiltObject != null)
                                    {
                                        BuiltObject targetBuiltObject3 = command.TargetBuiltObject;
                                        Empire.SetupBlockade(targetBuiltObject3);
                                    }
                                }
                                PreferredSpeed = 0f;
                                TargetSpeed = 0;
                                FirstExecutionOfCommand = false;
                            }
                            BuiltObjectList builtObjectList = null;
                            if (command.TargetHabitat != null)
                            {
                                Habitat targetHabitat4 = command.TargetHabitat;
                                builtObjectList = DetectShipsDockingAtHabitat(targetHabitat4);
                                BuiltObjectList builtObjectList2 = null;
                                if (targetHabitat4.Empire != null && targetHabitat4.BasesAtHabitat != null)
                                {
                                    for (int l = 0; l < targetHabitat4.BasesAtHabitat.Count; l++)
                                    {
                                        BuiltObject builtObject3 = targetHabitat4.BasesAtHabitat[l];
                                        if (builtObject3 != null && builtObject3.ParentHabitat == targetHabitat4)
                                        {
                                            builtObjectList2 = DetectShipsDockingAtSpacePort(builtObject3);
                                        }
                                    }
                                }
                                if (builtObjectList2 != null && builtObjectList2.Count > 0)
                                {
                                    builtObjectList.AddRange(builtObjectList2);
                                }
                            }
                            else if (command.TargetBuiltObject != null)
                            {
                                BuiltObject targetBuiltObject4 = command.TargetBuiltObject;
                                builtObjectList = DetectShipsDockingAtSpacePort(targetBuiltObject4);
                            }
                            if (builtObjectList.Count > 0)
                            {
                                foreach (BuiltObject item in builtObjectList)
                                {
                                    if (item.Empire != Empire && !EvaluateAdequateAttackers(item))
                                    {
                                        Command command2 = new Command(CommandAction.Attack, item);
                                        Mission.InsertCommandAtTop(command2);
                                        FirstExecutionOfCommand = true;
                                        result = timePassed;
                                        break;
                                    }
                                }
                            }
                            AccelerateToTargetSpeed(timePassed);
                            if (CurrentSpeed > 0f)
                            {
                                double num27 = (double)CurrentSpeed * timePassed;
                                Xpos += Math.Cos(Heading) * num27;
                                Ypos += Math.Sin(Heading) * num27;
                            }
                            result = 0.0;
                            break;
                        }
                    case CommandAction.Repair:
                        {
                            if (!FirstExecutionOfCommand)
                            {
                                break;
                            }
                            bool flag31 = false;
                            if (DamagedComponentCount == 0 && UnbuiltComponentCount == 0)
                            {
                                ClearPreviousMissionRequirements();
                                result = timePassed;
                                break;
                            }
                            if (Role == BuiltObjectRole.Base)
                            {
                                if (ParentHabitat == null || ParentHabitat.ConstructionQueue == null)
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                if (!ParentHabitat.ConstructionQueue.AddBuiltObjectToConstruct(this))
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                flag31 = true;
                                FirstExecutionOfCommand = false;
                            }
                            else if (DockedAt != null)
                            {
                                if (!DockedAt.IsShipYard || DockedAt.ConstructionQueue == null)
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                if (!DockedAt.ConstructionQueue.AddBuiltObjectToRepair(this))
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                flag31 = true;
                                FirstExecutionOfCommand = false;
                            }
                            if (Role != BuiltObjectRole.Base && DockedAt == null)
                            {
                                ClearPreviousMissionRequirements();
                            }
                            else
                            {
                                if (!flag31)
                                {
                                    break;
                                }
                                if (DockedAt is BuiltObject)
                                {
                                    ParentBuiltObject = (BuiltObject)DockedAt;
                                    ParentOffsetX = Xpos - ParentBuiltObject.Xpos;
                                    ParentOffsetY = Ypos - ParentBuiltObject.Ypos;
                                }
                                else if (DockedAt is Habitat)
                                {
                                    ParentHabitat = (Habitat)DockedAt;
                                    ParentOffsetX = Xpos - ParentHabitat.Xpos;
                                    ParentOffsetY = Ypos - ParentHabitat.Ypos;
                                }
                                BuiltAt = DockedAt;
                                PreferredSpeed = 0f;
                                CurrentSpeed = 0f;
                                if (DockedAt.DockingBayWaitQueue != null && DockedAt.DockingBayWaitQueue.Contains(this))
                                {
                                    DockedAt.DockingBayWaitQueue.Remove(this);
                                }
                                if (DockedAt.DockingBays != null)
                                {
                                    int num101 = DockedAt.DockingBays.IndexOf(this);
                                    if (num101 >= 0)
                                    {
                                        DockedAt.DockingBays[num101].DockedShip = null;
                                    }
                                }
                                DockedAt = null;
                            }
                            break;
                        }
                    case CommandAction.Retrofit:
                        {
                            if (!FirstExecutionOfCommand)
                            {
                                break;
                            }
                            bool flag = false;
                            if (mission.Design == null)
                            {
                                ClearPreviousMissionRequirements();
                                result = timePassed;
                                break;
                            }
                            if (Role == BuiltObjectRole.Base)
                            {
                                if (ParentHabitat == null || ParentHabitat.ConstructionQueue == null)
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                if (!ParentHabitat.ConstructionQueue.AddBuiltObjectToRetrofit(this, mission.Design))
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                flag = true;
                                mission.Design.BuildCount++;
                                FirstExecutionOfCommand = false;
                            }
                            else if (DockedAt != null)
                            {
                                if (!DockedAt.IsShipYard || DockedAt.ConstructionQueue == null)
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                if (!DockedAt.ConstructionQueue.AddBuiltObjectToRetrofit(this, mission.Design))
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                flag = true;
                                mission.Design.BuildCount++;
                                FirstExecutionOfCommand = false;
                            }
                            if (Role != BuiltObjectRole.Base && DockedAt == null)
                            {
                                ClearPreviousMissionRequirements();
                            }
                            else
                            {
                                if (!flag)
                                {
                                    break;
                                }
                                if (DockedAt is BuiltObject)
                                {
                                    ParentBuiltObject = (BuiltObject)DockedAt;
                                    ParentHabitat = null;
                                    ParentOffsetX = Xpos - ParentBuiltObject.Xpos;
                                    ParentOffsetY = Ypos - ParentBuiltObject.Ypos;
                                }
                                else if (DockedAt is Habitat)
                                {
                                    ParentHabitat = (Habitat)DockedAt;
                                    ParentBuiltObject = null;
                                    ParentOffsetX = Xpos - ParentHabitat.Xpos;
                                    ParentOffsetY = Ypos - ParentHabitat.Ypos;
                                }
                                BuiltAt = DockedAt;
                                PreferredSpeed = 0f;
                                CurrentSpeed = 0f;
                                if (DockedAt.DockingBayWaitQueue != null && DockedAt.DockingBayWaitQueue.Contains(this))
                                {
                                    DockedAt.DockingBayWaitQueue.Remove(this);
                                }
                                if (DockedAt.DockingBays != null)
                                {
                                    int num6 = DockedAt.DockingBays.IndexOf(this);
                                    if (num6 >= 0)
                                    {
                                        DockedAt.DockingBays[num6].DockedShip = null;
                                    }
                                }
                                DockedAt = null;
                            }
                            break;
                        }
                    case CommandAction.EvaluateThreats:
                        mission.CompleteCommand();
                        FirstExecutionOfCommand = true;
                        result = 0.0;
                        ThreatEvaluation(_Galaxy, time);
                        break;
                    case CommandAction.ScanArea:
                        ScanArea(_Galaxy);
                        mission.CompleteCommand();
                        FirstExecutionOfCommand = true;
                        result = 0.0;
                        break;
                    case CommandAction.Escort:
                        {
                            if (FirstExecutionOfCommand)
                            {
                                if (command.TargetBuiltObject == null)
                                {
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                    break;
                                }
                                FirstExecutionOfCommand = false;
                            }
                            BuiltObject targetBuiltObject5 = command.TargetBuiltObject;
                            if (targetBuiltObject5 == null || targetBuiltObject5.HasBeenDestroyed)
                            {
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                                break;
                            }
                            double num39 = _Galaxy.CalculateDistance(Xpos, Ypos, targetBuiltObject5.Xpos, targetBuiltObject5.Ypos);
                            if (num39 > (double)Galaxy.HyperJumpThreshhold && WarpSpeed > 0)
                            {
                                BuiltObjectMission mission2 = targetBuiltObject5.Mission;
                                Command command3 = null;
                                if (mission2 != null)
                                {
                                    command3 = mission2.ShowCurrentCommand();
                                }
                                if (mission2 != null && mission2.Type != 0 && command3 != null && command3.Action == CommandAction.HyperTo)
                                {
                                    double num40 = targetBuiltObject5.Xpos;
                                    double num41 = targetBuiltObject5.Ypos;
                                    if (command3.TargetHabitat != null)
                                    {
                                        Habitat targetHabitat6 = command3.TargetHabitat;
                                        num40 = targetHabitat6.Xpos;
                                        num41 = targetHabitat6.Ypos;
                                    }
                                    else if (command3.TargetBuiltObject != null)
                                    {
                                        BuiltObject targetBuiltObject6 = command3.TargetBuiltObject;
                                        num40 = targetBuiltObject6.Xpos;
                                        num41 = targetBuiltObject6.Ypos;
                                    }
                                    else if (command3.TargetCreature != null)
                                    {
                                        Creature targetCreature2 = command3.TargetCreature;
                                        num40 = targetCreature2.Xpos;
                                        num41 = targetCreature2.Ypos;
                                    }
                                    else if (command3.TargetShipGroup != null)
                                    {
                                        ShipGroup targetShipGroup2 = command3.TargetShipGroup;
                                        num40 = targetShipGroup2.Ships[0].Xpos;
                                        num41 = targetShipGroup2.Ships[0].Ypos;
                                    }
                                    if (command3.Xpos > -2E+09f && command3.Ypos > -2E+09f)
                                    {
                                        num40 = command3.Xpos;
                                        num41 = command3.Ypos;
                                    }
                                    double num42 = _Galaxy.CalculateDistance(Xpos, Ypos, num40, num41);
                                    if (num42 > (double)Galaxy.HyperJumpThreshhold && WarpSpeed > 0)
                                    {
                                        Command command4 = new Command(CommandAction.ConditionalHyperTo, num40, num41);
                                        Mission.InsertCommandAtTop(command4);
                                        FirstExecutionOfCommand = true;
                                        result = timePassed;
                                    }
                                }
                                else
                                {
                                    Command command5 = new Command(CommandAction.ConditionalHyperTo, targetBuiltObject5);
                                    Mission.InsertCommandAtTop(command5);
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                }
                                break;
                            }
                            if (num39 > (double)Galaxy.EscortRange)
                            {
                                PreferredSpeed = TopSpeed;
                                DoMovement(timePassed, targetBuiltObject5.Xpos, targetBuiltObject5.Ypos, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: false, manageHeading: true, manageDeceleration: false);
                                ParentBuiltObject = null;
                                ParentHabitat = null;
                                ParentOffsetX = -2000000001.0;
                                ParentOffsetY = -2000000001.0;
                            }
                            else
                            {
                                if (targetBuiltObject5.ParentHabitat != null && ParentHabitat == null)
                                {
                                    ParentHabitat = targetBuiltObject5.ParentHabitat;
                                    ParentBuiltObject = null;
                                    ParentOffsetX = Xpos - targetBuiltObject5.ParentHabitat.Xpos;
                                    ParentOffsetY = Ypos - targetBuiltObject5.ParentHabitat.Ypos;
                                }
                                if (targetBuiltObject5.ParentBuiltObject != null && ParentBuiltObject == null)
                                {
                                    ParentBuiltObject = targetBuiltObject5.ParentBuiltObject;
                                    ParentHabitat = null;
                                    ParentOffsetX = Xpos - targetBuiltObject5.ParentBuiltObject.Xpos;
                                    ParentOffsetY = Ypos - targetBuiltObject5.ParentBuiltObject.Ypos;
                                }
                                if (targetBuiltObject5.ParentBuiltObject == null && targetBuiltObject5.ParentHabitat == null)
                                {
                                    ParentBuiltObject = null;
                                    ParentHabitat = null;
                                    ParentOffsetX = -2000000001.0;
                                    ParentOffsetY = -2000000001.0;
                                }
                                if (TopSpeed >= (int)targetBuiltObject5.CurrentSpeed)
                                {
                                    PreferredSpeed = (int)targetBuiltObject5.CurrentSpeed;
                                }
                                else
                                {
                                    PreferredSpeed = TopSpeed;
                                }
                                TargetHeading = targetBuiltObject5.Heading;
                                DoMovement(timePassed, targetBuiltObject5.Xpos, targetBuiltObject5.Ypos, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: false, manageHeading: false, manageDeceleration: false);
                                ThreatEvaluation(galaxy, time);
                            }
                            if (targetBuiltObject5.Mission == null || targetBuiltObject5.Mission.Type == BuiltObjectMissionType.Undefined)
                            {
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                            }
                            result = 0.0;
                            break;
                        }
                    case CommandAction.Colonize:
                        if (command.TargetHabitat != null && !command.TargetHabitat.HasBeenDestroyed)
                        {
                            Habitat targetHabitat10 = command.TargetHabitat;
                            double num104 = _Galaxy.CalculateDistance(targetHabitat10.Xpos, targetHabitat10.Ypos, Xpos, Ypos);
                            double num105 = Galaxy.MovementPrecision * 4;
                            if (InView)
                            {
                                num105 = Galaxy.MovementPrecision + Galaxy.ImpulseMargin;
                            }
                            if (num104 <= num105)
                            {
                                int newPopulationAmount = 0;
                                if (Empire.CanBuiltObjectColonizeHabitat(this, targetHabitat10, out newPopulationAmount))
                                {
                                    if (targetHabitat10.Owner == null || targetHabitat10.Owner == _Galaxy.IndependentEmpire)
                                    {
                                        string empty2 = string.Empty;
                                        string text2 = string.Empty;
                                        bool flag32 = true;
                                        targetHabitat10.Population.RecalculateTotalAmount();
                                        Race dominantRace = targetHabitat10.Population.DominantRace;
                                        if (targetHabitat10.Population.TotalAmount > 0 && dominantRace != null)
                                        {
                                            Race race = NativeRace;
                                            if (race == null)
                                            {
                                                race = Empire.DominantRace;
                                            }
                                            int num106 = _Galaxy.CheckColonizationLikeliness(targetHabitat10, race);
                                            int num107 = Galaxy.Rnd.Next(0, 80) - 40;
                                            if (num106 <= 0 && num106 < num107 && Galaxy.Rnd.Next(0, 20) != 1)
                                            {
                                                flag32 = false;
                                            }
                                            if (Galaxy.Rnd.Next(0, 20) == 8)
                                            {
                                                flag32 = false;
                                            }
                                            text2 = ((!flag32) ? (" " + string.Format(TextResolver.GetText("The existing population repelled colonization"), dominantRace.Name) + ".") : (" " + string.Format(TextResolver.GetText("The existing population joined our empire"), dominantRace.Name) + "."));
                                        }
                                        if (flag32)
                                        {
                                            bool flag33 = false;
                                            if (Empire != null && Empire.PirateEmpireBaseHabitat != null && !Empire.CheckEmpireHasColonizationTech(Empire) && !Empire.CheckPirateEmpireHasCriminalNetwork(Empire))
                                            {
                                                flag33 = true;
                                            }
                                            if (flag33)
                                            {
                                                if (NativeRace != null)
                                                {
                                                    Empire.MakeHabitatIntoColony(targetHabitat10, _Galaxy.IndependentEmpire, NativeRace, newPopulationAmount);
                                                }
                                                else
                                                {
                                                    Empire.MakeHabitatIntoColony(targetHabitat10, _Galaxy.IndependentEmpire, Empire.DominantRace, newPopulationAmount);
                                                }
                                            }
                                            else if (NativeRace != null)
                                            {
                                                Empire.MakeHabitatIntoColony(targetHabitat10, Empire, NativeRace, newPopulationAmount);
                                            }
                                            else
                                            {
                                                Empire.MakeHabitatIntoColony(targetHabitat10, Empire, Empire.DominantRace, newPopulationAmount);
                                            }
                                            _Galaxy.ReviewEmpireTerritory(onlySystems: true);
                                            empty2 = empty2 + string.Format(TextResolver.GetText("NAME colonized"), targetHabitat10.Name) + "." + text2;
                                            if (!flag33 && Galaxy.Rnd.Next(0, 3) > 0 && dominantRace != null)
                                            {
                                                if (dominantRace.AggressionLevel > 110)
                                                {
                                                    _ = dominantRace.TroopStrength;
                                                    Troop troop4 = Galaxy.GenerateNewTroop(Empire.GenerateTroopDescription(dominantRace.TroopName), TroopType.Infantry, 100, Empire, dominantRace);
                                                    troop4.Colony = targetHabitat10;
                                                    targetHabitat10.Troops.Add(troop4);
                                                    Empire.Troops.Add(troop4);
                                                    empty2 = empty2 + " " + TextResolver.GetText("They have trained some new troops for us");
                                                    Empire.ReviewColonyTroopGarrison(targetHabitat10);
                                                }
                                                else if (dominantRace.IntelligenceLevel > 110)
                                                {
                                                    ResearchNode researchNode = Empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(_Galaxy);
                                                    if (researchNode != null)
                                                    {
                                                        float num108 = Galaxy.Rnd.Next(30000, 70000);
                                                        researchNode.Progress += num108;
                                                        if (researchNode.Progress >= researchNode.Cost)
                                                        {
                                                            Empire.DoResearchBreakthrough(researchNode, selfResearched: true, blockMessages: true, suppressUpdate: false);
                                                            empty2 = empty2 + " " + string.Format(TextResolver.GetText("They have advanced our understanding of X breakthrough"), researchNode.Name);
                                                        }
                                                        else
                                                        {
                                                            empty2 = empty2 + " " + string.Format(TextResolver.GetText("They have advanced our understanding of X"), researchNode.Name);
                                                        }
                                                    }
                                                }
                                                else if (dominantRace.LoyaltyLevel > 110)
                                                {
                                                    double num109 = Galaxy.Rnd.Next(7000, 20000);
                                                    Empire.StateMoney += num109;
                                                    empty2 = empty2 + " " + string.Format(TextResolver.GetText("They have presented us with a gift of X credits"), num109.ToString());
                                                }
                                            }
                                            Empire.SendMessageToEmpire(Empire, EmpireMessageType.NewColony, targetHabitat10, empty2);
                                            CompleteTeardown(_Galaxy);
                                            if (flag33)
                                            {
                                                break;
                                            }
                                            Race race2 = dominantRace;
                                            if (race2 == null)
                                            {
                                                race2 = NativeRace;
                                            }
                                            if (race2 != null)
                                            {
                                                RaceList newAbilityRaces = new RaceList();
                                                Race raceChanged = null;
                                                List<string> list2 = Empire.ReviewEmpireAbilityBonuses(out newAbilityRaces, out raceChanged);
                                                if (list2 != null && list2.Count > 0 && raceChanged != null)
                                                {
                                                    bool flag34 = false;
                                                    if (targetHabitat10.Population != null && targetHabitat10.Population.Count > 0)
                                                    {
                                                        for (int num110 = 0; num110 < targetHabitat10.Population.Count; num110++)
                                                        {
                                                            if (targetHabitat10.Population[num110].Race == raceChanged)
                                                            {
                                                                flag34 = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    if (flag34)
                                                    {
                                                        string text3 = string.Format(TextResolver.GetText("Colonization Race Ability Bonus"), Galaxy.ResolveDescription(targetHabitat10.Category).ToLower(CultureInfo.InvariantCulture), targetHabitat10.Name, raceChanged.Name);
                                                        text3 += ":\n";
                                                        foreach (string item2 in list2)
                                                        {
                                                            text3 = text3 + "\n" + item2;
                                                        }
                                                        string text4 = TextResolver.GetText("New Ability for our Empire");
                                                        Empire.SendEventMessageToEmpire(EventMessageType.NewEmpireRaceAbility, text4, text3, raceChanged, targetHabitat10);
                                                    }
                                                }
                                            }
                                            _Galaxy.ChanceNewColonyGovernor(Empire, targetHabitat10);
                                            if (Empire == null || Empire.Policy == null || targetHabitat10 == null)
                                            {
                                                break;
                                            }
                                            if (Empire.Policy.ColonyActionForNewTroopRecruitment)
                                            {
                                                Troop troop5 = targetHabitat10.GenerateNewTroop();
                                                if (troop5 != null)
                                                {
                                                    troop5.Readiness = 100f;
                                                    if (!targetHabitat10.Troops.Contains(troop5))
                                                    {
                                                        targetHabitat10.Troops.Add(troop5);
                                                    }
                                                    if (Empire != null && Empire.Troops != null && !Empire.Troops.Contains(troop5))
                                                    {
                                                        Empire.Troops.Add(troop5);
                                                    }
                                                    Empire.ReviewColonyTroopGarrison(targetHabitat10);
                                                }
                                            }
                                            if (Empire.Policy.ColonyActionForNewBuildDesign != null && Empire.CanBuildDesign(Empire.Policy.ColonyActionForNewBuildDesign, includeSizeCheck: false) && Empire.Policy.ColonyActionForNewBuildDesign.Role == BuiltObjectRole.Base)
                                            {
                                                bool isStateOwned = _Galaxy.DetermineBuiltObjectIsState(Empire.Policy.ColonyActionForNewBuildDesign.SubRole);
                                                Empire.PurchaseNewBuiltObject(Empire.Policy.ColonyActionForNewBuildDesign, targetHabitat10, isStateOwned, isAutoControlled: true);
                                            }
                                        }
                                        else
                                        {
                                            empty2 += string.Format(TextResolver.GetText("Colonization attempt failed"), targetHabitat10.Name);
                                            empty2 = empty2 + "." + text2;
                                            Empire.SendMessageToEmpire(Empire, EmpireMessageType.NewColonyFailed, targetHabitat10, empty2);
                                            CompleteTeardown(_Galaxy);
                                        }
                                    }
                                    else
                                    {
                                        CheckColonyShipMissionCancelled(1);
                                        mission.CompleteCommand();
                                        FirstExecutionOfCommand = true;
                                    }
                                }
                                else
                                {
                                    CheckColonyShipMissionCancelled(2);
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                }
                            }
                            else
                            {
                                mission.CompleteCommand();
                                Command command13 = new Command(CommandAction.MoveTo, targetHabitat10);
                                Mission.InsertCommandAtTop(command13);
                                if (num105 > (double)Galaxy.HyperJumpThreshhold && WarpSpeed > 0)
                                {
                                    Command command14 = new Command(CommandAction.ConditionalHyperTo, targetHabitat10);
                                    Mission.InsertCommandAtTop(command14);
                                }
                                FirstExecutionOfCommand = true;
                                result = timePassed;
                            }
                        }
                        else
                        {
                            if (command.TargetHabitat != null && command.TargetHabitat.HasBeenDestroyed)
                            {
                                CheckColonyShipMissionCancelled(3);
                            }
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                        }
                        break;
                    case CommandAction.HoldSyncFleet:
                        {
                            ShipGroup shipGroup = ShipGroup;
                            if (shipGroup != null && shipGroup.Ships != null)
                            {
                                bool flag30 = true;
                                for (int num99 = 0; num99 < shipGroup.Ships.Count; num99++)
                                {
                                    BuiltObject builtObject15 = shipGroup.Ships[num99];
                                    if (builtObject15 == null || builtObject15.HasBeenDestroyed || builtObject15.ShipGroup != shipGroup)
                                    {
                                        continue;
                                    }
                                    BuiltObjectMission mission3 = builtObject15.Mission;
                                    if (mission3 != null)
                                    {
                                        Command command11 = mission3.FastPeekCurrentCommand();
                                        if (command11 != null && command11.Action != CommandAction.HoldSyncFleet && mission3.CheckCommandsForAction(CommandAction.HoldSyncFleet))
                                        {
                                            flag30 = false;
                                            break;
                                        }
                                    }
                                }
                                if (flag30)
                                {
                                    for (int num100 = 0; num100 < shipGroup.Ships.Count; num100++)
                                    {
                                        BuiltObject builtObject16 = shipGroup.Ships[num100];
                                        if (builtObject16 != null && !builtObject16.HasBeenDestroyed && builtObject16.ShipGroup == shipGroup)
                                        {
                                            BuiltObjectMission mission4 = builtObject16.Mission;
                                            if (mission4 != null && mission4.CompleteCommandIfMatchesAction(CommandAction.HoldSyncFleet))
                                            {
                                                builtObject16.FirstExecutionOfCommand = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                            }
                            result = 0.0;
                            break;
                        }
                    case CommandAction.Hold:
                        if (starDate >= command.StarDate)
                        {
                            long num102 = starDate - command.StarDate;
                            result = (double)num102 / 1000.0;
                            if (command.StarDate < 0)
                            {
                                result = 0.0;
                            }
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                        }
                        AccelerateToTargetSpeed(timePassed);
                        if (CurrentSpeed > 0f)
                        {
                            double num103 = (double)CurrentSpeed * timePassed;
                            Xpos += Math.Cos(Heading) * num103;
                            Ypos += Math.Sin(Heading) * num103;
                        }
                        break;
                    case CommandAction.ExtractResources:
                        {
                            BaconBuiltObject.CommandActionExtractResources(this);
                            if (FirstExecutionOfCommand)
                            {
                                PreferredSpeed = 0f;
                                CurrentSpeed = 0f;
                                FirstExecutionOfCommand = false;
                            }
                            int num30 = 0;
                            if (Cargo != null)
                            {
                                foreach (Cargo item3 in Cargo)
                                {
                                    num30 += item3.Amount;
                                }
                            }
                            if (num30 >= CargoCapacity)
                            {
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                            }
                            else
                            {
                                if (command.TargetHabitat == null)
                                {
                                    break;
                                }
                                Habitat targetHabitat5 = command.TargetHabitat;
                                HabitatResourceList habitatResourceList = new HabitatResourceList();
                                if (targetHabitat5.Resources != null)
                                {
                                    habitatResourceList = targetHabitat5.Resources.Clone();
                                }
                                bool flag15 = false;
                                switch (SubRole)
                                {
                                    case BuiltObjectSubRole.GasMiningShip:
                                        if (habitatResourceList.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag15 = true;
                                        }
                                        break;
                                    case BuiltObjectSubRole.MiningShip:
                                        if (habitatResourceList.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag15 = true;
                                        }
                                        break;
                                    default:
                                        if (ExtractionMine > 0 && habitatResourceList.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag15 = true;
                                        }
                                        if (ExtractionGas > 0 && habitatResourceList.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag15 = true;
                                        }
                                        break;
                                }
                                if (ExtractionLuxury > 0)
                                {
                                    foreach (HabitatResource item4 in habitatResourceList)
                                    {
                                        if (item4.IsLuxuryResource)
                                        {
                                            flag15 = true;
                                            break;
                                        }
                                    }
                                }
                                if (!flag15)
                                {
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                }
                                else if (targetHabitat5.Empire != null && targetHabitat5.Empire != _Galaxy.IndependentEmpire)
                                {
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                }
                            }
                            break;
                        }
                    case CommandAction.Scrap:
                        {
                            if (!FirstExecutionOfCommand)
                            {
                                break;
                            }
                            bool flag14 = false;
                            if (Role == BuiltObjectRole.Base)
                            {
                                if (ParentHabitat == null || ParentHabitat.ConstructionQueue == null)
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                if (!ParentHabitat.ConstructionQueue.AddBuiltObjectToScrap(this))
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                flag14 = true;
                                FirstExecutionOfCommand = false;
                            }
                            else if (DockedAt != null)
                            {
                                if (!DockedAt.IsShipYard || DockedAt.ConstructionQueue == null)
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                if (!DockedAt.ConstructionQueue.AddBuiltObjectToScrap(this))
                                {
                                    ClearPreviousMissionRequirements();
                                    break;
                                }
                                flag14 = true;
                                Scrap = true;
                                FirstExecutionOfCommand = false;
                            }
                            if (DockedAt == null)
                            {
                                ClearPreviousMissionRequirements();
                                break;
                            }
                            if (Role != BuiltObjectRole.Base && Cargo != null && Cargo.Count > 0 && DockedAt.CargoSpace > 0)
                            {
                                foreach (Cargo item5 in Cargo)
                                {
                                    int num28 = item5.Amount;
                                    if (num28 > DockedAt.CargoSpace)
                                    {
                                        num28 = DockedAt.CargoSpace;
                                    }
                                    Cargo cargo5 = null;
                                    if (item5.CommodityComponent != null)
                                    {
                                        cargo5 = new Cargo(item5.CommodityComponent, num28, item5.EmpireId);
                                    }
                                    else if (item5.CommodityResource != null)
                                    {
                                        cargo5 = new Cargo(item5.CommodityResource, num28, item5.EmpireId);
                                    }
                                    if (DockedAt.Cargo != null)
                                    {
                                        DockedAt.Cargo.Add(cargo5);
                                    }
                                    if (DockedAt.CargoSpace <= 0)
                                    {
                                        break;
                                    }
                                }
                                Cargo.Clear();
                            }
                            if (Troops != null && Troops.Count > 0)
                            {
                                Habitat habitat6 = null;
                                if (DockedAt is BuiltObject)
                                {
                                    if (((BuiltObject)DockedAt).ParentHabitat != null)
                                    {
                                        habitat6 = ((BuiltObject)DockedAt).ParentHabitat;
                                    }
                                }
                                else if (DockedAt is Habitat)
                                {
                                    habitat6 = (Habitat)DockedAt;
                                }
                                if (Troops != null && Troops.Count > 0 && habitat6 != null && habitat6.Troops != null)
                                {
                                    foreach (Troop troop6 in Troops)
                                    {
                                        troop6.BuiltObject = null;
                                        troop6.Colony = habitat6;
                                        habitat6.Troops.Add(troop6);
                                    }
                                    Troops.Clear();
                                }
                            }
                            if (!flag14)
                            {
                                break;
                            }
                            if (DockedAt is BuiltObject)
                            {
                                ParentBuiltObject = (BuiltObject)DockedAt;
                                ParentHabitat = null;
                                ParentOffsetX = Xpos - ParentBuiltObject.Xpos;
                                ParentOffsetY = Ypos - ParentBuiltObject.Ypos;
                            }
                            else if (DockedAt is Habitat)
                            {
                                ParentHabitat = (Habitat)DockedAt;
                                ParentBuiltObject = null;
                                ParentOffsetX = Xpos - ParentHabitat.Xpos;
                                ParentOffsetY = Ypos - ParentHabitat.Ypos;
                            }
                            BuiltAt = DockedAt;
                            PreferredSpeed = 0f;
                            CurrentSpeed = 0f;
                            if (DockedAt.DockingBayWaitQueue != null && DockedAt.DockingBayWaitQueue.Contains(this))
                            {
                                DockedAt.DockingBayWaitQueue.Remove(this);
                            }
                            if (DockedAt.DockingBays != null)
                            {
                                int num29 = DockedAt.DockingBays.IndexOf(this);
                                if (num29 >= 0)
                                {
                                    DockedAt.DockingBays[num29].DockedShip = null;
                                }
                            }
                            DockedAt = null;
                            break;
                        }
                    case CommandAction.Build:
                        {
                            bool flag2 = true;
                            if (ConstructionQueue != null)
                            {
                                if (FirstExecutionOfCommand)
                                {
                                    if (mission.Design != null)
                                    {
                                        if ((mission.Design.SubRole == BuiltObjectSubRole.GasMiningStation || mission.Design.SubRole == BuiltObjectSubRole.MiningStation || mission.Design.SubRole == BuiltObjectSubRole.ResortBase) && ParentHabitat != null)
                                        {
                                            bool flag3 = true;
                                            if (mission.Design.SubRole == BuiltObjectSubRole.GasMiningStation || mission.Design.SubRole == BuiltObjectSubRole.MiningStation)
                                            {
                                                if (ParentHabitat.Owner != null && ParentHabitat.Owner != _Galaxy.IndependentEmpire)
                                                {
                                                    flag3 = false;
                                                }
                                            }
                                            else if (mission.Design.SubRole == BuiltObjectSubRole.ResortBase && ParentHabitat.Owner != null && ParentHabitat.Owner != _Galaxy.IndependentEmpire && ParentHabitat.Owner != Empire)
                                            {
                                                flag3 = false;
                                            }
                                            bool flag4 = false;
                                            if (mission.Design.SubRole == BuiltObjectSubRole.GasMiningStation || mission.Design.SubRole == BuiltObjectSubRole.MiningStation)
                                            {
                                                flag4 = _Galaxy.CheckAlreadyHaveMiningStationAtHabitat(ParentHabitat, Empire);
                                            }
                                            bool flag5 = _Galaxy.CheckForeignBaseAtHabitat(ParentHabitat, Empire);
                                            if (flag4 || flag5)
                                            {
                                                flag3 = false;
                                            }
                                            if (ActualEmpire.PirateEmpireBaseHabitat == null && !_Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(Empire, ParentHabitat))
                                            {
                                                flag3 = false;
                                            }
                                            if (!flag3)
                                            {
                                                ParentOffsetX = -2000000001.0;
                                                ParentOffsetY = -2000000001.0;
                                                mission.CompleteCommand();
                                                FirstExecutionOfCommand = true;
                                                result = timePassed;
                                                break;
                                            }
                                        }
                                        if ((mission.Design.SubRole == BuiltObjectSubRole.WeaponsResearchStation || mission.Design.SubRole == BuiltObjectSubRole.EnergyResearchStation || mission.Design.SubRole == BuiltObjectSubRole.HighTechResearchStation) && mission.TargetHabitat != null && Empire.CheckResearchStationAtLocation(mission.TargetHabitat))
                                        {
                                            ParentOffsetX = -2000000001.0;
                                            ParentOffsetY = -2000000001.0;
                                            mission.CompleteCommand();
                                            FirstExecutionOfCommand = true;
                                            result = timePassed;
                                            break;
                                        }
                                        bool flag6 = false;
                                        mission.Design.BuildCount++;
                                        string empty = string.Empty;
                                        if ((Empire != null && Empire.PirateEmpireBaseHabitat != null && mission.Design.SubRole == BuiltObjectSubRole.SmallSpacePort) || mission.Design.SubRole == BuiltObjectSubRole.MediumSpacePort || mission.Design.SubRole == BuiltObjectSubRole.LargeSpacePort)
                                        {
                                            empty = ((ParentHabitat == null) ? _Galaxy.GenerateBuiltObjectName(mission.Design) : _Galaxy.GeneratePirateBaseName(ParentHabitat));
                                        }
                                        else if (mission.Design.SubRole == BuiltObjectSubRole.GasMiningStation || mission.Design.SubRole == BuiltObjectSubRole.MiningStation || mission.Design.SubRole == BuiltObjectSubRole.ResortBase || mission.Design.SubRole == BuiltObjectSubRole.EnergyResearchStation || mission.Design.SubRole == BuiltObjectSubRole.WeaponsResearchStation || mission.Design.SubRole == BuiltObjectSubRole.HighTechResearchStation || mission.Design.SubRole == BuiltObjectSubRole.MonitoringStation || mission.Design.SubRole == BuiltObjectSubRole.DefensiveBase || mission.Design.SubRole == BuiltObjectSubRole.GenericBase)
                                        {
                                            empty = ((ParentHabitat == null) ? _Galaxy.GenerateBuiltObjectName(mission.Design) : _Galaxy.SelectUniqueBuiltObjectName(mission.Design, ParentHabitat));
                                        }
                                        else if (mission.Design.Role != BuiltObjectRole.Base && mission.Design.IsPlanetDestroyer)
                                        {
                                            empty = _Galaxy.GeneratePlanetDestroyerName();
                                            flag6 = true;
                                        }
                                        else
                                        {
                                            empty = _Galaxy.GenerateBuiltObjectName(mission.Design);
                                        }
                                        BuiltObject builtObject = new BuiltObject(mission.Design, empty, _Galaxy);
                                        double num10 = (builtObject.PurchasePrice = mission.Design.CalculateCurrentPurchasePrice(_Galaxy));
                                        builtObject.ParentBuiltObject = ParentBuiltObject;
                                        builtObject.ParentHabitat = ParentHabitat;
                                        builtObject.ParentOffsetX = ParentOffsetX;
                                        builtObject.ParentOffsetY = ParentOffsetY;
                                        builtObject.BuiltAt = this;
                                        builtObject.Xpos = Xpos;
                                        builtObject.Ypos = Ypos;
                                        if (builtObject.Role == BuiltObjectRole.Base || flag6)
                                        {
                                            builtObject.Heading = _Galaxy.SelectRandomHeading();
                                        }
                                        builtObject.TargetHeading = builtObject.Heading;
                                        mission.SecondaryTargetBuiltObject = builtObject;
                                        PreferredSpeed = 0f;
                                        CurrentSpeed = 0f;
                                        if (ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                                        {
                                            object parent = null;
                                            if (ParentHabitat != null)
                                            {
                                                parent = ParentHabitat;
                                            }
                                            else if (ParentBuiltObject != null)
                                            {
                                                parent = ParentBuiltObject;
                                            }
                                            bool flag7 = false;
                                            bool flag8 = false;
                                            switch (builtObject.SubRole)
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
                                                    flag7 = true;
                                                    break;
                                                case BuiltObjectSubRole.ResortBase:
                                                    flag7 = true;
                                                    if (ActualEmpire.PirateEmpireBaseHabitat != null)
                                                    {
                                                        flag8 = true;
                                                    }
                                                    break;
                                            }
                                            if (flag6)
                                            {
                                                flag7 = true;
                                            }
                                            Empire.AddBuiltObjectToGalaxy(builtObject, parent, offsetLocationFromParent: false, flag7, (int)ParentOffsetX, (int)ParentOffsetY);
                                            if (flag8)
                                            {
                                                builtObject.Empire = _Galaxy.IndependentEmpire;
                                                if (!_Galaxy.IndependentEmpire.PrivateBuiltObjects.Contains(builtObject))
                                                {
                                                    _Galaxy.IndependentEmpire.PrivateBuiltObjects.Add(builtObject);
                                                }
                                            }
                                            double num11 = mission.Design.CalculateCurrentPurchasePrice(_Galaxy);
                                            if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
                                            {
                                                Empire.StateMoney -= num11;
                                                Empire.PirateEconomy.PerformExpense(num11, PirateExpenseType.Construction, starDate);
                                            }
                                            else if (!flag7)
                                            {
                                                builtObject.Empire.PerformPrivateTransaction(0.0 - num11);
                                                Empire.StateMoney += BaconBuiltObject.PrivateSectorBuildOrRefitInvestInInfrastructure(this, num11);
                                            }
                                            Empire.ObtainBuildResourcesForConstructionShip(this, builtObject);
                                            if (flag6)
                                            {
                                                double x2 = Xpos - 600.0;
                                                double y2 = Ypos - 600.0;
                                                string name = string.Format(TextResolver.GetText("X Project"), builtObject.Name);
                                                GalaxyLocation galaxyLocation = new GalaxyLocation(name, GalaxyLocationType.PlanetDestroyer, x2, y2, 1200.0, 1200.0, -1);
                                                galaxyLocation.RelatedBuiltObject = builtObject;
                                                galaxyLocation.ShowName = true;
                                                _Galaxy.GalaxyLocations.Add(galaxyLocation);
                                                _Galaxy.AddGalaxyLocationIndex(galaxyLocation);
                                                Empire.KnownGalaxyLocations.Add(galaxyLocation);
                                            }
                                            FirstExecutionOfCommand = false;
                                        }
                                        else
                                        {
                                            ParentOffsetX = -2000000001.0;
                                            ParentOffsetY = -2000000001.0;
                                            mission.CompleteCommand();
                                            result = timePassed;
                                            FirstExecutionOfCommand = true;
                                        }
                                    }
                                    else if (mission.SecondaryTargetBuiltObject != null)
                                    {
                                        BuiltObject secondaryTargetBuiltObject = mission.SecondaryTargetBuiltObject;
                                        if (secondaryTargetBuiltObject.BuiltAt != null)
                                        {
                                            ClearPreviousMissionRequirements();
                                            result = timePassed;
                                        }
                                        else
                                        {
                                            PreferredSpeed = 0f;
                                            CurrentSpeed = 0f;
                                            secondaryTargetBuiltObject.PreferredSpeed = 0f;
                                            secondaryTargetBuiltObject.CurrentSpeed = 0f;
                                            if (secondaryTargetBuiltObject.Role != BuiltObjectRole.Base && secondaryTargetBuiltObject.ParentHabitat != null)
                                            {
                                                if (ParentHabitat == secondaryTargetBuiltObject.ParentHabitat)
                                                {
                                                    ParentOffsetX = -2000000001.0;
                                                    ParentOffsetY = -2000000001.0;
                                                    ParentHabitat = null;
                                                }
                                                secondaryTargetBuiltObject.ParentOffsetX = -2000000001.0;
                                                secondaryTargetBuiltObject.ParentOffsetY = -2000000001.0;
                                                secondaryTargetBuiltObject.ParentHabitat = null;
                                            }
                                            if (secondaryTargetBuiltObject.ParentHabitat != null)
                                            {
                                                ParentHabitat = secondaryTargetBuiltObject.ParentHabitat;
                                                ParentOffsetX = secondaryTargetBuiltObject.ParentOffsetX;
                                                ParentOffsetY = secondaryTargetBuiltObject.ParentOffsetY;
                                            }
                                            if (ConstructionQueue.AddBuiltObjectToRepair(secondaryTargetBuiltObject))
                                            {
                                                if (secondaryTargetBuiltObject.Role != BuiltObjectRole.Base)
                                                {
                                                    secondaryTargetBuiltObject.ClearPreviousMissionRequirements();
                                                    secondaryTargetBuiltObject.RevertMission = null;
                                                }
                                                secondaryTargetBuiltObject.BuiltAt = this;
                                                Empire.ObtainBuildResourcesForConstructionShip(this, secondaryTargetBuiltObject);
                                                FirstExecutionOfCommand = false;
                                            }
                                            else
                                            {
                                                ClearPreviousMissionRequirements();
                                                result = timePassed;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ClearPreviousMissionRequirements();
                                        result = timePassed;
                                    }
                                }
                                if (ConstructionQueue.ConstructionWaitQueue.Count > 0)
                                {
                                    flag2 = false;
                                }
                                for (int j = 0; j < ConstructionQueue.ConstructionYards.Count; j++)
                                {
                                    if (ConstructionQueue.ConstructionYards[j].ShipUnderConstruction != null)
                                    {
                                        flag2 = false;
                                    }
                                }
                            }
                            else
                            {
                                flag2 = true;
                            }
                            if (flag2)
                            {
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                            }
                            break;
                        }
                    case CommandAction.Attack:
                    case CommandAction.Bombard:
                    case CommandAction.Capture:
                    case CommandAction.Raid:
                        {
                            HyperDenyActive = true;
                            if (FirstExecutionOfCommand)
                            {
                                _LastInvasionDistance = 536870911.0;
                                if (command.TargetBuiltObject == null && command.TargetHabitat == null && command.TargetShipGroup == null && command.TargetCreature == null)
                                {
                                    if (mission.TargetBuiltObject == null && mission.TargetHabitat == null && mission.TargetCreature == null && mission.TargetShipGroup == null)
                                    {
                                        CurrentTarget = null;
                                        Mission.CompleteCommand();
                                        FirstExecutionOfCommand = true;
                                        result = timePassed;
                                        break;
                                    }
                                    if (mission.TargetHabitat != null)
                                    {
                                        if (mission.TargetHabitat.Empire != Empire)
                                        {
                                            if ((mission.TargetHabitat.Empire == null && command.Action != CommandAction.Bombard) || mission.TargetHabitat.HasBeenDestroyed)
                                            {
                                                CurrentTarget = null;
                                                Mission.CompleteCommand();
                                                FirstExecutionOfCommand = true;
                                                result = timePassed;
                                                break;
                                            }
                                            bool flag20 = true;
                                            if (ShipGroup != null && ShipGroup.Mission != null && ShipGroup.Mission.Type == BuiltObjectMissionType.Bombard && BombardWeaponPower <= 0 && Troops != null && Troops.Count > 0)
                                            {
                                                flag20 = false;
                                            }
                                            if (flag20)
                                            {
                                                command.TargetHabitat = mission.TargetHabitat;
                                                _ColonyToAttack = mission.TargetHabitat;
                                            }
                                        }
                                        else if (_Threats.Length > 0)
                                        {
                                            int num60 = 0;
                                            while (_Threats[num60] == null || _Threats[num60].HasBeenDestroyed || _Threats[num60].Empire == Empire || (PirateEmpireId > 0 && _Threats[num60] is BuiltObject && ((BuiltObject)_Threats[num60]).PirateEmpireId == PirateEmpireId))
                                            {
                                                _Threats[num60] = null;
                                                num60++;
                                                if (num60 >= _Threats.Length)
                                                {
                                                    num60 = 0;
                                                    break;
                                                }
                                            }
                                            if (_Threats[num60] == null || _Threats[num60].HasBeenDestroyed || _Threats[num60].Empire == Empire || (PirateEmpireId > 0 && _Threats[num60] is BuiltObject && ((BuiltObject)_Threats[num60]).PirateEmpireId == PirateEmpireId))
                                            {
                                                result = 0.0;
                                                break;
                                            }
                                            if (ShouldAttack(_Threats[num60], time))
                                            {
                                                if (_Threats[num60] is BuiltObject)
                                                {
                                                    command.TargetBuiltObject = (BuiltObject)_Threats[num60];
                                                }
                                                else if (_Threats[num60] is Creature && command.Action != CommandAction.Capture && command.Action != CommandAction.Raid)
                                                {
                                                    command.TargetCreature = (Creature)_Threats[num60];
                                                }
                                            }
                                        }
                                    }
                                    else if (_Threats.Length > 0)
                                    {
                                        int num61 = 0;
                                        while (_Threats[num61] == null || _Threats[num61].HasBeenDestroyed || _Threats[num61].Empire == Empire || (PirateEmpireId > 0 && _Threats[num61] is BuiltObject && ((BuiltObject)_Threats[num61]).PirateEmpireId == PirateEmpireId))
                                        {
                                            _Threats[num61] = null;
                                            num61++;
                                            if (num61 >= _Threats.Length)
                                            {
                                                num61 = 0;
                                                break;
                                            }
                                        }
                                        if (_Threats[num61] == null || _Threats[num61].HasBeenDestroyed || _Threats[num61].Empire == Empire || (PirateEmpireId > 0 && _Threats[num61] is BuiltObject && ((BuiltObject)_Threats[num61]).PirateEmpireId == PirateEmpireId))
                                        {
                                            result = 0.0;
                                            break;
                                        }
                                        if (ShouldAttack(_Threats[num61], time))
                                        {
                                            if (_Threats[num61] is BuiltObject)
                                            {
                                                command.TargetBuiltObject = (BuiltObject)_Threats[num61];
                                            }
                                            else if (_Threats[num61] is Creature && command.Action != CommandAction.Capture && command.Action != CommandAction.Raid)
                                            {
                                                command.TargetCreature = (Creature)_Threats[num61];
                                            }
                                        }
                                    }
                                }
                                if (command.TargetBuiltObject == null && command.TargetHabitat == null && command.TargetCreature == null && command.TargetShipGroup == null)
                                {
                                    CurrentTarget = null;
                                    Mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                    break;
                                }
                                Empire empire = null;
                                if (command.TargetBuiltObject != null)
                                {
                                    CurrentTarget = command.TargetBuiltObject;
                                    _Galaxy.NotifyOfAttack(this, Empire, command.TargetBuiltObject, isNewAttack: true);
                                    empire = command.TargetBuiltObject.Empire;
                                }
                                else if (command.TargetCreature != null)
                                {
                                    CurrentTarget = command.TargetCreature;
                                }
                                else if (command.TargetShipGroup != null)
                                {
                                    ShipGroup targetShipGroup3 = command.TargetShipGroup;
                                    CurrentTarget = DetermineShipGroupTarget(targetShipGroup3, time);
                                    if (CurrentTarget != null && CurrentTarget is BuiltObject)
                                    {
                                        _Galaxy.NotifyOfAttack(this, Empire, (BuiltObject)CurrentTarget, isNewAttack: true);
                                    }
                                    empire = targetShipGroup3.Empire;
                                }
                                else
                                {
                                    if (command.TargetHabitat == null)
                                    {
                                        throw new ApplicationException("Invalid attack target");
                                    }
                                    bool flag21 = true;
                                    if (ShipGroup != null && ShipGroup.Mission != null && ShipGroup.Mission.TargetHabitat == command.TargetHabitat && (ShipGroup.Mission.Type == BuiltObjectMissionType.Bombard || ShipGroup.Mission.Type == BuiltObjectMissionType.WaitAndBombard) && ((Troops != null && Troops.Count > 0) || (Characters != null && Characters.Count > 0 && Characters.CountCharactersByRole(CharacterRole.TroopGeneral) > 0)) && (command.Action == CommandAction.Attack || command.Action == CommandAction.Bombard) && BombardRange <= 0)
                                    {
                                        flag21 = false;
                                    }
                                    if ((Troops == null || Troops.TotalAttackStrength <= 0) && (Characters == null || Characters.Count == 0 || Characters.CountCharactersByRole(CharacterRole.TroopGeneral) == 0) && (!IsPlanetDestroyer || command.TargetHabitat.HasBeenDestroyed) && command.Action != CommandAction.Bombard && command.Action != CommandAction.Raid)
                                    {
                                        flag21 = false;
                                    }
                                    if (!flag21)
                                    {
                                        _ColonyToAttack = null;
                                        Habitat targetHabitat8 = command.TargetHabitat;
                                        BuiltObject builtObject7 = _Galaxy.DetermineSpacePortAtColony(targetHabitat8);
                                        if (builtObject7 != null)
                                        {
                                            CurrentTarget = builtObject7;
                                            _Galaxy.NotifyOfAttack(this, Empire, builtObject7, isNewAttack: true);
                                            empire = builtObject7.Empire;
                                        }
                                        else if (_Threats.Length > 0)
                                        {
                                            int num62 = 0;
                                            while (_Threats[num62] == null || _Threats[num62].HasBeenDestroyed || _Threats[num62].Empire == Empire || (PirateEmpireId > 0 && _Threats[num62] is BuiltObject && ((BuiltObject)_Threats[num62]).PirateEmpireId == PirateEmpireId))
                                            {
                                                _Threats[num62] = null;
                                                num62++;
                                                if (num62 >= _Threats.Length)
                                                {
                                                    num62 = 0;
                                                    break;
                                                }
                                            }
                                            if (_Threats[num62] == null || _Threats[num62].HasBeenDestroyed || _Threats[num62].Empire == Empire || (PirateEmpireId > 0 && _Threats[num62] is BuiltObject && ((BuiltObject)_Threats[num62]).PirateEmpireId == PirateEmpireId))
                                            {
                                                result = 0.0;
                                                break;
                                            }
                                            if (ShouldAttack(_Threats[num62], time))
                                            {
                                                CurrentTarget = _Threats[num62];
                                                SetOptimalAttackRanges(CurrentTarget, command.Action);
                                                if (_Threats[num62] is BuiltObject)
                                                {
                                                    BuiltObject builtObject8 = (BuiltObject)_Threats[num62];
                                                    _Galaxy.NotifyOfAttack(this, Empire, builtObject8, isNewAttack: true);
                                                    empire = builtObject8.Empire;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (command.TargetHabitat.Empire == Empire)
                                        {
                                            _ColonyToAttack = null;
                                            CurrentTarget = null;
                                            Mission.CompleteCommand();
                                            FirstExecutionOfCommand = true;
                                            result = timePassed;
                                            break;
                                        }
                                        _Galaxy.NotifyOfAttack(this, Empire, command.TargetHabitat, bombarded: false, isNewAttack: true, notifyIndependent: false);
                                        empire = command.TargetHabitat.Empire;
                                    }
                                }
                                PreferredSpeed = TopSpeed;
                                TargetSpeed = (int)PreferredSpeed;
                                if (CurrentTarget != null)
                                {
                                    SetOptimalAttackRanges(CurrentTarget, command.Action);
                                }
                                if (empire != null && !empire.PirateExtortionOfferMade && Empire != null && Empire.PirateEmpireBaseHabitat != null)
                                {
                                    if (empire == _Galaxy.PlayerEmpire && empire.PirateEmpireBaseHabitat == null)
                                    {
                                        string text = TextResolver.GetText("Pirate Protection Extortion");
                                        EmpireMessage empireMessage = new EmpireMessage(Empire, EmpireMessageType.PirateOfferProtection, null);
                                        empireMessage.Description = text;
                                        empireMessage.Hint = "extort";
                                        Empire.SendMessageToEmpire(empireMessage, empire);
                                    }
                                    empire.PirateExtortionOfferMade = true;
                                }
                                FirstExecutionOfCommand = false;
                            }
                            if (CurrentTarget == null)
                            {
                                if ((command.TargetHabitat == null || command.Action != CommandAction.Raid || CalculateAvailableAssaultPodAttackStrength(time) <= 0) && (command.TargetHabitat == null || ((Troops == null || Troops.TotalAttackStrength <= 0) && (Characters == null || Characters.Count <= 0 || Characters.CountCharactersByRole(CharacterRole.TroopGeneral) <= 0) && !IsPlanetDestroyer && command.Action != CommandAction.Bombard) || command.TargetHabitat.Owner == Empire))
                                {
                                    if (mission.TargetShipGroup != null)
                                    {
                                        ShipGroup targetShipGroup4 = mission.TargetShipGroup;
                                        CurrentTarget = DetermineShipGroupTarget(targetShipGroup4, time);
                                        command.TargetShipGroup = targetShipGroup4;
                                        if (CurrentTarget != null)
                                        {
                                            FirstExecutionOfCommand = true;
                                            result = timePassed;
                                            break;
                                        }
                                        CurrentTarget = null;
                                        Mission.CompleteCommand();
                                        result = timePassed;
                                        FirstExecutionOfCommand = true;
                                    }
                                    else
                                    {
                                        CurrentTarget = null;
                                        Mission.CompleteCommand();
                                        FirstExecutionOfCommand = true;
                                        result = timePassed;
                                    }
                                    break;
                                }
                            }
                            else if (CurrentTarget.HasBeenDestroyed || CurrentTarget.Empire == Empire || (PirateEmpireId > 0 && CurrentTarget is BuiltObject && ((BuiltObject)CurrentTarget).PirateEmpireId == PirateEmpireId))
                            {
                                if (mission.TargetShipGroup != null)
                                {
                                    ShipGroup targetShipGroup5 = mission.TargetShipGroup;
                                    CurrentTarget = DetermineShipGroupTarget(targetShipGroup5, time);
                                    command.TargetShipGroup = targetShipGroup5;
                                    if (CurrentTarget != null)
                                    {
                                        FirstExecutionOfCommand = true;
                                        result = timePassed;
                                        break;
                                    }
                                    CurrentTarget = null;
                                    Mission.CompleteCommand();
                                    result = timePassed;
                                    FirstExecutionOfCommand = true;
                                }
                                else
                                {
                                    CurrentTarget = null;
                                    if (_ColonyToAttack == null && command.TargetHabitat == null)
                                    {
                                        Mission.CompleteCommand();
                                        result = timePassed;
                                    }
                                    else
                                    {
                                        result = 0.0;
                                    }
                                    FirstExecutionOfCommand = true;
                                }
                                break;
                            }
                            double num63 = 0.0;
                            if (CurrentTarget != null && !CurrentTarget.HasBeenDestroyed)
                            {
                                num63 = _Galaxy.CalculateDistance(Xpos, Ypos, CurrentTarget.Xpos, CurrentTarget.Ypos);
                            }
                            if (num63 > (double)Galaxy.HyperJumpThreshhold)
                            {
                                if (WarpSpeed > 0)
                                {
                                    bool flag22 = false;
                                    BuiltObject builtObject9 = CheckForHyperExitGravityWell(CurrentTarget.Xpos, CurrentTarget.Ypos);
                                    if (builtObject9 != null)
                                    {
                                        double num64 = _Galaxy.CalculateDistance(Xpos, Ypos, builtObject9.Xpos, builtObject9.Ypos);
                                        double num65 = num64 / (double)builtObject9.HyperStopRange;
                                        if (num65 < 2.0)
                                        {
                                            flag22 = true;
                                        }
                                    }
                                    if (!flag22)
                                    {
                                        if (num63 > (double)SensorProximityArrayRange || (mission != null && mission.Type == BuiltObjectMissionType.Blockade))
                                        {
                                            if (mission.TargetShipGroup != null)
                                            {
                                                ShipGroup targetShipGroup6 = mission.TargetShipGroup;
                                                CurrentTarget = DetermineShipGroupTarget(targetShipGroup6, time);
                                                command.TargetShipGroup = targetShipGroup6;
                                                if (CurrentTarget != null)
                                                {
                                                    FirstExecutionOfCommand = true;
                                                    result = 0.0;
                                                    break;
                                                }
                                            }
                                            CurrentTarget = null;
                                            if (mission.TargetHabitat != null && !mission.TargetHabitat.HasBeenDestroyed && mission.TargetHabitat.Empire != Empire && ((Troops != null && Troops.TotalAttackStrength > 0) || mission.Type == BuiltObjectMissionType.Raid))
                                            {
                                                result = 0.0;
                                            }
                                            else
                                            {
                                                Mission.CompleteCommand();
                                                result = timePassed;
                                            }
                                            FirstExecutionOfCommand = true;
                                            break;
                                        }
                                        bool flag23 = true;
                                        if (CurrentTarget is BuiltObject)
                                        {
                                            BuiltObject builtObject10 = (BuiltObject)CurrentTarget;
                                            if (builtObject10.NearestSystemStar != NearestSystemStar || (builtObject10.WarpSpeed > 0 && builtObject10.CurrentSpeed > (float)builtObject10.TopSpeed))
                                            {
                                                flag23 = false;
                                                if (mission.ManuallyAssigned)
                                                {
                                                    flag23 = true;
                                                }
                                            }
                                        }
                                        double num66 = (double)(int)SensorJumpIntercept / 100.0;
                                        if (!flag23 || !(num66 > Galaxy.Rnd.NextDouble()))
                                        {
                                            if (mission.TargetShipGroup != null)
                                            {
                                                ShipGroup targetShipGroup7 = mission.TargetShipGroup;
                                                CurrentTarget = DetermineShipGroupTarget(targetShipGroup7, time);
                                                command.TargetShipGroup = targetShipGroup7;
                                                if (CurrentTarget != null)
                                                {
                                                    FirstExecutionOfCommand = true;
                                                    result = 0.0;
                                                    break;
                                                }
                                            }
                                            CurrentTarget = null;
                                            if (mission.TargetHabitat != null && !mission.TargetHabitat.HasBeenDestroyed && mission.TargetHabitat.Empire != Empire && Troops != null && Troops.TotalAttackStrength > 0)
                                            {
                                                result = 0.0;
                                            }
                                            else
                                            {
                                                Mission.CompleteCommand();
                                                result = timePassed;
                                            }
                                            FirstExecutionOfCommand = true;
                                            break;
                                        }
                                        BuiltObjectMission builtObjectMission = null;
                                        if (CurrentTarget is BuiltObject)
                                        {
                                            builtObjectMission = ((BuiltObject)CurrentTarget).Mission;
                                        }
                                        if (builtObjectMission != null && builtObjectMission.Type != 0 && builtObjectMission.ShowCurrentCommand() != null && builtObjectMission.ShowCurrentCommand().Action == CommandAction.HyperTo)
                                        {
                                            double num67 = CurrentTarget.Xpos;
                                            double num68 = CurrentTarget.Ypos;
                                            Command command6 = builtObjectMission.ShowCurrentCommand();
                                            if (command6 != null)
                                            {
                                                if (command6.TargetHabitat != null)
                                                {
                                                    Habitat targetHabitat9 = command6.TargetHabitat;
                                                    num67 = targetHabitat9.Xpos;
                                                    num68 = targetHabitat9.Ypos;
                                                }
                                                else if (command6.TargetBuiltObject != null)
                                                {
                                                    BuiltObject targetBuiltObject8 = command6.TargetBuiltObject;
                                                    num67 = targetBuiltObject8.Xpos;
                                                    num68 = targetBuiltObject8.Ypos;
                                                }
                                                else if (command6.TargetShipGroup != null)
                                                {
                                                    ShipGroup targetShipGroup8 = command6.TargetShipGroup;
                                                    num67 = targetShipGroup8.LeadShip.Xpos;
                                                    num68 = targetShipGroup8.LeadShip.Ypos;
                                                }
                                                else if (command6.TargetCreature != null)
                                                {
                                                    Creature targetCreature3 = command6.TargetCreature;
                                                    num67 = targetCreature3.Xpos;
                                                    num68 = targetCreature3.Ypos;
                                                }
                                                if (command6.Xpos > -2E+09f && command6.Ypos > -2E+09f)
                                                {
                                                    num67 = command6.Xpos;
                                                    num68 = command6.Ypos;
                                                }
                                            }
                                            if (num67 < -2000000000.0 || num68 < -2000000000.0)
                                            {
                                                CurrentTarget = null;
                                                Mission.CompleteCommand();
                                                FirstExecutionOfCommand = true;
                                                result = timePassed;
                                                break;
                                            }
                                            if (NearestSystemStar != null && !mission.ManuallyAssigned)
                                            {
                                                double num69 = _Galaxy.CalculateDistance(NearestSystemStar.Xpos, NearestSystemStar.Ypos, num67, num68);
                                                if (num69 > (double)Galaxy.MaxSolarSystemSize + 500.0)
                                                {
                                                    num67 = CurrentTarget.Xpos;
                                                    num68 = CurrentTarget.Ypos;
                                                }
                                            }
                                            double num70 = _Galaxy.CalculateDistance(Xpos, Ypos, num67, num68);
                                            if (num70 > (double)Galaxy.HyperJumpThreshhold)
                                            {
                                                if (WarpSpeed > 0)
                                                {
                                                    Command command7 = new Command(CommandAction.ConditionalHyperTo, num67, num68);
                                                    Mission.InsertCommandAtTop(command7);
                                                    FirstExecutionOfCommand = true;
                                                    result = timePassed;
                                                }
                                                else if (!WithinFuelRange(num67, num68, 0.0) && !mission.ManuallyAssigned)
                                                {
                                                    CurrentTarget = null;
                                                    Mission.CompleteCommand();
                                                    FirstExecutionOfCommand = true;
                                                    result = timePassed;
                                                }
                                            }
                                            break;
                                        }
                                        if (CurrentTarget != null)
                                        {
                                            if (WarpSpeed > 0)
                                            {
                                                Command command8 = new Command(CommandAction.ConditionalHyperTo, CurrentTarget.Xpos, CurrentTarget.Ypos);
                                                Mission.InsertCommandAtTop(command8);
                                                FirstExecutionOfCommand = true;
                                                result = timePassed;
                                                break;
                                            }
                                            if (!WithinFuelRange(CurrentTarget.Xpos, CurrentTarget.Ypos, 0.0) && !mission.ManuallyAssigned)
                                            {
                                                CurrentTarget = null;
                                                Mission.CompleteCommand();
                                                FirstExecutionOfCommand = true;
                                                result = timePassed;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else if (!WithinFuelRange(CurrentTarget.Xpos, CurrentTarget.Ypos, 0.0) && !mission.ManuallyAssigned)
                                {
                                    CurrentTarget = null;
                                    Mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                    break;
                                }
                            }
                            if (command.TargetHabitat != null && ((Troops != null && Troops.TotalAttackStrength > 0) || (Characters != null && Characters.Count > 0 && Characters.CountCharactersByRole(CharacterRole.TroopGeneral) > 0) || IsPlanetDestroyer || command.Action == CommandAction.Bombard || (command.Action == CommandAction.Raid && AssaultStrength > 0)) && command.TargetHabitat.Owner != Empire)
                            {
                                Habitat habitat10 = (_ColonyToAttack = command.TargetHabitat);
                            }
                            if (command.Action == CommandAction.Bombard && BombardWeaponPower > 0 && _ColonyToAttack != null && !_ColonyToAttack.HasBeenDestroyed)
                            {
                                DoMovement(timePassed, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos, x, y, 0.0, 0.0, _Galaxy, manageArrival: false, manageHeading: true, manageDeceleration: false);
                                double num71 = galaxy.CalculateDistance(Xpos, Ypos, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos);
                                double num72 = BombardRange;
                                if (num71 <= num72)
                                {
                                    _Galaxy.NotifyOfAttack(this, Empire, _ColonyToAttack, bombarded: true, isNewAttack: false, notifyIndependent: true);
                                    if (_ColonyToAttack.Parent != null)
                                    {
                                        PreferredSpeed = (int)((double)(_ColonyToAttack.OrbitSpeed + _ColonyToAttack.Parent.OrbitSpeed) * 1.2) + 2;
                                    }
                                    else
                                    {
                                        PreferredSpeed = (int)((double)(int)_ColonyToAttack.OrbitSpeed * 1.2) + 2;
                                    }
                                    if (num71 < num72 / 1.3)
                                    {
                                        PreferredSpeed = 0f;
                                    }
                                    PreferredSpeed = Math.Min(PreferredSpeed, TopSpeed);
                                    TargetSpeed = (int)PreferredSpeed;
                                    BombardTarget(num71, _ColonyToAttack);
                                    if (_ColonyToAttack.Population == null || _ColonyToAttack.Population.Count == 0 || _ColonyToAttack.Population.TotalAmount <= 0)
                                    {
                                        _ColonyToAttack = null;
                                        CurrentTarget = null;
                                        Mission.CompleteCommand();
                                        result = timePassed;
                                        FirstExecutionOfCommand = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    PreferredSpeed = TopSpeed;
                                    TargetSpeed = (int)PreferredSpeed;
                                }
                            }
                            else if (command.Action == CommandAction.Raid && ShouldInvadeColony(BuiltObjectMissionType.Raid))
                            {
                                if (_ColonyToAttack != null)
                                {
                                    _ColonyToAttack.TargetInvadingShips(this, time);
                                    DoMovement(timePassed, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos, x, y, 0.0, 0.0, _Galaxy, manageArrival: false, manageHeading: true, manageDeceleration: false);
                                    int num73 = CalculateAvailableAssaultPodAttackStrength(time);
                                    if (num73 > 0)
                                    {
                                        double num74 = _Galaxy.CalculateDistance(Xpos, Ypos, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos);
                                        SetOptimalAttackRangesBoarding();
                                        if (num74 < (double)AssaultRange)
                                        {
                                            CheckLaunchAssaultPodsAtTarget(time, _ColonyToAttack);
                                        }
                                    }
                                    else
                                    {
                                        SetOptimalAttackRanges(_ColonyToAttack);
                                    }
                                }
                            }
                            else if (command.Action != CommandAction.Bombard && ShouldInvadeColony())
                            {
                                _ColonyToAttack.TargetInvadingShips(this, time);
                                DoMovement(timePassed, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos, x, y, 0.0, 0.0, _Galaxy, manageArrival: false, manageHeading: true, manageDeceleration: false);
                                double lastInvasionDistance = _LastInvasionDistance;
                                double num75 = (_LastInvasionDistance = galaxy.CalculateDistance(Xpos, Ypos, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos));
                                double num76 = Galaxy.MovementDecelerationRangeInvasion;
                                double num77 = Galaxy.InvasionDropoffRange;
                                if (!InView)
                                {
                                    num76 *= 2.0;
                                    num77 *= 3.0;
                                }
                                if (num75 < num76)
                                {
                                    if (num75 < num77 || lastInvasionDistance < num75)
                                    {
                                        if (_ColonyToAttack.Owner != null)
                                        {
                                            _Galaxy.InvasionAttempts++;
                                            _Galaxy.NotifyOfAttack(this, Empire, _ColonyToAttack, bombarded: false, isNewAttack: true, notifyIndependent: true);
                                            if (_ColonyToAttack.Owner != ActualEmpire)
                                            {
                                                _ColonyToAttack.StopRebelling();
                                            }
                                            PreferredSpeed = 0f;
                                            TargetSpeed = (int)PreferredSpeed;
                                            if ((Troops != null && Troops.Count > 0) || (Characters != null && Characters.Count > 0))
                                            {
                                                if (_ColonyToAttack.InvadingTroops != null && _ColonyToAttack.InvadingTroops.Count > 0)
                                                {
                                                    Troop[] array2 = ListHelper.ToArrayThreadSafe(_ColonyToAttack.InvadingTroops);
                                                    TroopList troopList4 = new TroopList();
                                                    foreach (Troop troop3 in array2)
                                                    {
                                                        if (troop3 != null && troop3.Empire != null && troop3.Type == TroopType.PirateRaider && troop3.Empire == Empire)
                                                        {
                                                            troopList4.Add(troop3);
                                                        }
                                                    }
                                                    for (int num79 = 0; num79 < troopList4.Count; num79++)
                                                    {
                                                        _ColonyToAttack.InvadingTroops.Remove(troopList4[num79]);
                                                    }
                                                }
                                                List<object> list = new List<object>();
                                                int num80 = 0;
                                                int num81 = 0;
                                                if (_ColonyToAttack.BasesAtHabitat != null && _ColonyToAttack.BasesAtHabitat.Count > 0)
                                                {
                                                    for (int num82 = 0; num82 < _ColonyToAttack.BasesAtHabitat.Count; num82++)
                                                    {
                                                        if (_ColonyToAttack.BasesAtHabitat[num82].FirepowerRaw > 0)
                                                        {
                                                            num80 += _ColonyToAttack.BasesAtHabitat[num82].FirepowerRaw;
                                                            list.Add(_ColonyToAttack.BasesAtHabitat[num82]);
                                                            num81++;
                                                        }
                                                    }
                                                }
                                                if (_ColonyToAttack.PlanetaryShieldPresent)
                                                {
                                                    num80 += 1000;
                                                    num81++;
                                                }
                                                int num83 = 0;
                                                int num84 = 0;
                                                int num85 = num80;
                                                TroopList byType = _ColonyToAttack.Troops.GetByType(TroopType.Artillery);
                                                num83 = byType.TotalDefendStrength;
                                                if (byType.Count > 0)
                                                {
                                                    if (_ColonyToAttack.Empire != null)
                                                    {
                                                        num83 = (int)((float)num83 * _ColonyToAttack.Empire.TroopAttackStrengthBonusFactorArtillery);
                                                        num84 = (int)((float)num83 * _ColonyToAttack.Empire.TroopPlanetaryDefenseInterceptBonusFactor);
                                                    }
                                                    num80 += num83 / 20;
                                                    list.AddRange(byType);
                                                    num81 += byType.Count;
                                                }
                                                num80 = Math.Min(3000, num80);
                                                num81 = Math.Min(10, num81);
                                                double val = Math.Sqrt(num80) * Math.Sqrt(num81);
                                                val = Math.Min(90.0, val);
                                                int num86 = num84 / 20 + num85;
                                                double val2 = Math.Sqrt(num86) * Math.Sqrt(num81);
                                                val2 = Math.Min(95.0, val2);
                                                if (Troops != null)
                                                {
                                                    foreach (Troop troop7 in Troops)
                                                    {
                                                        troop7.Colony = _ColonyToAttack;
                                                        if (_ColonyToAttack.InvadingTroops == null)
                                                        {
                                                            _ColonyToAttack.InvadingTroops = new TroopList();
                                                        }
                                                        _ColonyToAttack.InvadingTroops.Add(troop7);
                                                        if (_ColonyToAttack.ColonyInvasion != null)
                                                        {
                                                            _ColonyToAttack.ColonyInvasion.AddInvaderLanding(troop7);
                                                        }
                                                        double num87 = val;
                                                        double num88 = val2;
                                                        if (troop7.Type == TroopType.SpecialForces)
                                                        {
                                                            num87 /= 3.0;
                                                            num88 /= 3.0;
                                                        }
                                                        if (!(Galaxy.Rnd.NextDouble() * 100.0 < val2))
                                                        {
                                                            continue;
                                                        }
                                                        double val3 = num87 * Galaxy.Rnd.NextDouble();
                                                        val3 = Math.Min(troop7.Readiness * 0.9f, val3);
                                                        troop7.Readiness -= (float)val3;
                                                        if (_ColonyToAttack.InvasionStats == null)
                                                        {
                                                            _ColonyToAttack.InvasionStats = new InvasionStats(_ColonyToAttack, Empire, _ColonyToAttack.Empire);
                                                        }
                                                        if (_ColonyToAttack.InvasionStats != null)
                                                        {
                                                            _ColonyToAttack.InvasionStats.TroopsDamageToInvaders += (float)val3;
                                                        }
                                                        if (_ColonyToAttack.ColonyInvasion != null)
                                                        {
                                                            object firer = null;
                                                            if (list != null && list.Count > 0)
                                                            {
                                                                firer = list[Galaxy.Rnd.Next(0, list.Count)];
                                                            }
                                                            _ColonyToAttack.ColonyInvasion.AddInvaderLandingExplosion(troop7, firer, Galaxy.Rnd);
                                                        }
                                                    }
                                                    Troops.Clear();
                                                }
                                                if (Characters != null)
                                                {
                                                    foreach (Character character2 in Characters)
                                                    {
                                                        if (character2.Role == CharacterRole.TroopGeneral)
                                                        {
                                                            character2.CompleteLocationTransfer(_ColonyToAttack, _Galaxy, invadingDestination: true);
                                                            if (_ColonyToAttack.ColonyInvasion != null)
                                                            {
                                                                _ColonyToAttack.ColonyInvasion.AddInvaderLanding(character2);
                                                            }
                                                        }
                                                    }
                                                }
                                                if (_ColonyToAttack.Empire != _Galaxy.IndependentEmpire)
                                                {
                                                    int evaluationImpact = Math.Min(200, Math.Max(70, _ColonyToAttack.StrategicValue / 1000));
                                                    ModifyDiplomacyFromAttack(_ColonyToAttack.Empire, evaluationImpact);
                                                    if (_ColonyToAttack.Empire.PirateEmpireBaseHabitat != null)
                                                    {
                                                        if (Empire != null && _ColonyToAttack.Empire.ObtainPirateRelation(Empire).Type == PirateRelationType.Protection)
                                                        {
                                                            _ColonyToAttack.Empire.ChangePirateRelation(Empire, PirateRelationType.None, starDate);
                                                        }
                                                    }
                                                    else if ((_ColonyToAttack.StrategicValue > 50000 || (_ColonyToAttack.Empire != null && _ColonyToAttack.Empire.Capitals != null && _ColonyToAttack.Empire.Capitals.Contains(_ColonyToAttack))) && Empire != null && Empire.PirateEmpireBaseHabitat == null && _ColonyToAttack.Empire.ControlDiplomacyOffense == AutomationLevel.FullyAutomated && _ColonyToAttack.Empire.PirateEmpireBaseHabitat == null)
                                                    {
                                                        _ColonyToAttack.Empire.DeclareWar(Empire);
                                                    }
                                                }
                                            }
                                        }
                                        _LastInvasionDistance = 536870911.0;
                                        CurrentTarget = null;
                                        _ColonyToAttack = null;
                                        FirstExecutionOfCommand = true;
                                    }
                                    PreferredSpeed = (int)(num75 / num77 * (double)TopSpeed);
                                    if (PreferredSpeed < (float)Galaxy.MovementImpulseSpeed)
                                    {
                                        PreferredSpeed = Galaxy.MovementImpulseSpeed;
                                    }
                                    TargetSpeed = (int)PreferredSpeed;
                                }
                                else
                                {
                                    PreferredSpeed = TopSpeed;
                                    TargetSpeed = (int)PreferredSpeed;
                                }
                            }
                            else if (IsPlanetDestroyer && _ColonyToAttack != null && _Galaxy.CanDestroyHabitat(this, _ColonyToAttack) && !_ColonyToAttack.HasBeenDestroyed)
                            {
                                double num89 = galaxy.CalculateDistance(Xpos, Ypos, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos);
                                if (num89 <= (double)PlanetDestroyerWeaponsRange)
                                {
                                    PreferredSpeed = 0f;
                                    FirePlanetDestroyerAtHabitat(num89, _ColonyToAttack);
                                }
                                else
                                {
                                    TargetHeading = (float)Galaxy.DetermineAngle(Xpos, Ypos, _ColonyToAttack.Xpos, _ColonyToAttack.Ypos);
                                    PreferredSpeed = TopSpeed;
                                }
                                TargetSpeed = (int)PreferredSpeed;
                                DoMovement(timePassed, num, num2, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: false, manageHeading: false, manageDeceleration: false);
                            }
                            else
                            {
                                if (CurrentTarget == null && _Threats.Length > 0 && _Threats[0] != null && !_Threats[0].HasBeenDestroyed && ShouldAttack(_Threats[0], time))
                                {
                                    CurrentTarget = _Threats[0];
                                    SetOptimalAttackRanges(CurrentTarget);
                                }
                                StellarObject stellarObject3 = CurrentTarget;
                                int num90 = 0;
                                if (stellarObject3 != null)
                                {
                                    if (stellarObject3.Empire != null)
                                    {
                                        num90 = stellarObject3.Empire.EmpireId;
                                    }
                                    if (stellarObject3 is BuiltObject)
                                    {
                                        BuiltObject builtObject11 = (BuiltObject)stellarObject3;
                                        if (builtObject11.PirateEmpireId > 0)
                                        {
                                            num90 = builtObject11.PirateEmpireId;
                                        }
                                    }
                                }
                                double num91 = 536870911.0;
                                if (stellarObject3 != null && !stellarObject3.HasBeenDestroyed)
                                {
                                    num91 = galaxy.CalculateDistance(Xpos, Ypos, stellarObject3.Xpos, stellarObject3.Ypos);
                                    if (num91 >= OptimalMinimumAttackRange && num91 <= OptimalMaximumAttackRange)
                                    {
                                        if (stellarObject3.ParentBuiltObject != null)
                                        {
                                            TargetHeading = stellarObject3.ParentBuiltObject.Heading;
                                            if (TopSpeed >= (int)((double)stellarObject3.ParentBuiltObject.CurrentSpeed * 1.2) + 2)
                                            {
                                                PreferredSpeed = (int)((double)stellarObject3.ParentBuiltObject.CurrentSpeed * 1.2) + 2;
                                            }
                                            else
                                            {
                                                PreferredSpeed = TopSpeed;
                                            }
                                            TargetSpeed = (int)PreferredSpeed;
                                        }
                                        else if (stellarObject3.ParentHabitat != null)
                                        {
                                            TargetHeading = (float)Galaxy.DetermineAngle(Xpos, Ypos, stellarObject3.Xpos, stellarObject3.Ypos);
                                            if (stellarObject3.ParentHabitat.Parent != null)
                                            {
                                                PreferredSpeed = (int)((double)(stellarObject3.ParentHabitat.OrbitSpeed + stellarObject3.ParentHabitat.Parent.OrbitSpeed) * 1.2) + 2;
                                            }
                                            else
                                            {
                                                PreferredSpeed = (int)((double)(int)stellarObject3.ParentHabitat.OrbitSpeed * 1.2) + 2;
                                            }
                                            PreferredSpeed = Math.Min(PreferredSpeed, TopSpeed);
                                            TargetSpeed = (int)PreferredSpeed;
                                        }
                                        else
                                        {
                                            TargetHeading = stellarObject3.TargetHeading;
                                            if (stellarObject3.CurrentTarget == this && stellarObject3.FirepowerRaw < (int)((double)FirepowerRaw * 0.9))
                                            {
                                                PreferredSpeed = Galaxy.MovementImpulseSpeed * 2;
                                            }
                                            else
                                            {
                                                PreferredSpeed = (int)stellarObject3.CurrentSpeed;
                                                PreferredSpeed = Math.Min(PreferredSpeed, TopSpeed);
                                            }
                                            TargetSpeed = (int)PreferredSpeed;
                                        }
                                    }
                                    else
                                    {
                                        if (stellarObject3 is Creature && num91 < OptimalMaximumAttackRange + 50.0 && num91 > OptimalMaximumAttackRange)
                                        {
                                            TargetHeading = (float)Galaxy.DetermineAngle(Xpos, Ypos, stellarObject3.Xpos, stellarObject3.Ypos);
                                            PreferredSpeed = TopSpeed;
                                        }
                                        else if (stellarObject3.TopSpeed <= 0 && num91 < OptimalMaximumAttackRange + 50.0 && num91 > OptimalMaximumAttackRange)
                                        {
                                            TargetHeading = (float)Galaxy.DetermineAngle(Xpos, Ypos, stellarObject3.Xpos, stellarObject3.Ypos);
                                            PreferredSpeed = CruiseSpeed / 2;
                                            if (stellarObject3.ParentHabitat != null)
                                            {
                                                PreferredSpeed += (int)stellarObject3.ParentHabitat.OrbitSpeed;
                                            }
                                            else if (stellarObject3.ParentBuiltObject != null && stellarObject3.ParentBuiltObject.ParentHabitat != null)
                                            {
                                                PreferredSpeed += (int)stellarObject3.ParentBuiltObject.ParentHabitat.OrbitSpeed;
                                            }
                                        }
                                        else if (num91 > OptimalMaximumAttackRange)
                                        {
                                            TargetHeading = (float)Galaxy.DetermineAngle(Xpos, Ypos, stellarObject3.Xpos, stellarObject3.Ypos);
                                            PreferredSpeed = TopSpeed;
                                        }
                                        else if (num91 < OptimalMinimumAttackRange)
                                        {
                                            TargetHeading = (float)(Math.PI + Galaxy.DetermineAngle(Xpos, Ypos, stellarObject3.Xpos, stellarObject3.Ypos));
                                            PreferredSpeed = TopSpeed;
                                        }
                                        TargetSpeed = (int)PreferredSpeed;
                                    }
                                }
                                else
                                {
                                    if (mission.TargetShipGroup != null)
                                    {
                                        ShipGroup targetShipGroup9 = mission.TargetShipGroup;
                                        CurrentTarget = DetermineShipGroupTarget(targetShipGroup9, time);
                                        SetOptimalAttackRanges(CurrentTarget);
                                        command.TargetShipGroup = targetShipGroup9;
                                        stellarObject3 = CurrentTarget;
                                        FirstExecutionOfCommand = true;
                                        result = timePassed;
                                    }
                                    if (mission.TargetHabitat != null && _ColonyToAttack != null)
                                    {
                                        CurrentTarget = null;
                                        stellarObject3 = null;
                                        result = 0.0;
                                        break;
                                    }
                                    if (CurrentTarget == null || CurrentTarget.HasBeenDestroyed || CurrentTarget.Empire == Empire || (PirateEmpireId > 0 && CurrentTarget is BuiltObject && ((BuiltObject)CurrentTarget).PirateEmpireId == PirateEmpireId))
                                    {
                                        if (_ColonyToAttack == null)
                                        {
                                            CurrentTarget = null;
                                            stellarObject3 = null;
                                            Mission.CompleteCommand();
                                            result = timePassed;
                                            FirstExecutionOfCommand = true;
                                        }
                                        break;
                                    }
                                }
                                DoMovement(timePassed, num, num2, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: false, manageHeading: false, manageDeceleration: false);
                                if (stellarObject3 != null)
                                {
                                    if (num91 <= (double)MaximumWeaponsRange)
                                    {
                                        bool flag24 = true;
                                        if (stellarObject3 is BuiltObject)
                                        {
                                            BuiltObject builtObject12 = (BuiltObject)stellarObject3;
                                            if (builtObject12.Empire == Empire || (PirateEmpireId > 0 && builtObject12.PirateEmpireId == PirateEmpireId))
                                            {
                                                if (Attackers.Contains(builtObject12))
                                                {
                                                    Attackers.Remove(builtObject12);
                                                }
                                                CurrentTarget = null;
                                                builtObject12.Attackers.Remove(this);
                                                builtObject12.Pursuers.Remove(this);
                                                stellarObject3 = null;
                                                flag24 = false;
                                            }
                                            else if ((command.Action == CommandAction.Capture || command.Action == CommandAction.Raid) && ((Empire != null && Empire.CheckOurEmpireOverwhelmingBoarding(builtObject12)) || builtObject12.CurrentShields < (float)Math.Max(15, (int)AssaultShieldPenetration)))
                                            {
                                                flag24 = false;
                                            }
                                        }
                                        if (flag24)
                                        {
                                            bool mayModifyDiplomacy = true;
                                            if (stellarObject3.Attackers != null)
                                            {
                                                for (int num92 = 0; num92 < stellarObject3.Attackers.Count; num92++)
                                                {
                                                    StellarObject stellarObject4 = stellarObject3.Attackers[num92];
                                                    if (stellarObject4 != null && stellarObject4.Empire == Empire)
                                                    {
                                                        mayModifyDiplomacy = false;
                                                        break;
                                                    }
                                                }
                                            }
                                            FireWeaponsAtTarget(num91, stellarObject3, time, mayModifyDiplomacy);
                                        }
                                    }
                                    if (command.Action == CommandAction.Capture || command.Action == CommandAction.Raid)
                                    {
                                        bool flag25 = false;
                                        if (AssaultStrength > 0 && AssaultRange > 0)
                                        {
                                            flag25 = true;
                                        }
                                        bool flag26 = false;
                                        if (ShipGroup != null && ShipGroup.TotalAvailableBoardingAssaultStrengthCapturingTarget(time, stellarObject3) > 0)
                                        {
                                            flag26 = true;
                                        }
                                        if ((flag25 || flag26) && num90 != ActualEmpire.EmpireId && stellarObject3 is BuiltObject)
                                        {
                                            BuiltObject builtObject13 = (BuiltObject)stellarObject3;
                                            if (builtObject13.CurrentShields < (float)Math.Max(15, (int)AssaultShieldPenetration))
                                            {
                                                int num93 = CalculateAvailableAssaultPodAttackStrength(time);
                                                if (num93 > 0)
                                                {
                                                    SetOptimalAttackRangesBoarding();
                                                    if (num91 < (double)AssaultRange)
                                                    {
                                                        CheckLaunchAssaultPodsAtTarget(time, builtObject13);
                                                    }
                                                }
                                                else if (!flag26)
                                                {
                                                    if (Attackers.Contains(CurrentTarget))
                                                    {
                                                        Attackers.Remove(CurrentTarget);
                                                    }
                                                    CurrentTarget = null;
                                                    if (_ColonyToAttack == null)
                                                    {
                                                        command.TargetHabitat = null;
                                                    }
                                                    mission.CompleteCommand();
                                                    FirstExecutionOfCommand = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Attackers.Contains(CurrentTarget))
                                            {
                                                Attackers.Remove(CurrentTarget);
                                            }
                                            CurrentTarget = null;
                                            if (_ColonyToAttack == null)
                                            {
                                                command.TargetHabitat = null;
                                            }
                                            mission.CompleteCommand();
                                            FirstExecutionOfCommand = true;
                                        }
                                    }
                                }
                                else if ((int)CurrentEnergy > ReactorStorageCapacity / 4 && _Threats != null)
                                {
                                    for (int num94 = 0; num94 < _Threats.Length && _ThreatLevels[num94] > 0 && _Threats[num94] != null; num94++)
                                    {
                                        double num95 = _Galaxy.CalculateDistance(Xpos, Ypos, _Threats[num94].Xpos, _Threats[num94].Ypos);
                                        if (num95 <= (double)MaximumWeaponsRange && ShouldAttack(_Threats[num94], time))
                                        {
                                            bool mayModifyDiplomacy2 = false;
                                            if (!_Threats[num94].Attackers.Contains(this))
                                            {
                                                mayModifyDiplomacy2 = true;
                                            }
                                            FireWeaponsAtTarget(num95, _Threats[num94], time, mayModifyDiplomacy2);
                                            break;
                                        }
                                    }
                                }
                            }
                            if (CurrentTarget != null && CurrentTarget.HasBeenDestroyed)
                            {
                                if (Attackers.Contains(CurrentTarget))
                                {
                                    Attackers.Remove(CurrentTarget);
                                }
                                CurrentTarget = null;
                                if (_ColonyToAttack == null)
                                {
                                    command.TargetHabitat = null;
                                }
                                FirstExecutionOfCommand = true;
                            }
                            if (CurrentFuel <= 0.0 && CurrentEnergy <= 0.0 && _ColonyToAttack == null)
                            {
                                CurrentTarget = null;
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                            }
                            break;
                        }
                    case CommandAction.Dock:
                        {
                            if (!CheckMissionStillValid(time))
                            {
                                break;
                            }
                            DockingBayList dockingBayList = null;
                            BuiltObjectList builtObjectList3 = null;
                            StellarObject stellarObject2 = null;
                            bool flag17 = false;
                            short num43 = 0;
                            if (command.TargetBuiltObject != null)
                            {
                                stellarObject2 = command.TargetBuiltObject;
                                dockingBayList = stellarObject2.DockingBays;
                                builtObjectList3 = stellarObject2.DockingBayWaitQueue;
                                num43 = command.TargetBuiltObject.SensorTraceScannerPower;
                            }
                            else
                            {
                                if (command.TargetHabitat == null)
                                {
                                    throw new ApplicationException("Docking target type is invalid");
                                }
                                stellarObject2 = command.TargetHabitat;
                                dockingBayList = stellarObject2.DockingBays;
                                builtObjectList3 = stellarObject2.DockingBayWaitQueue;
                                if (command.TargetHabitat.BasesAtHabitat != null)
                                {
                                    for (int m = 0; m < command.TargetHabitat.BasesAtHabitat.Count; m++)
                                    {
                                        BuiltObject builtObject5 = command.TargetHabitat.BasesAtHabitat[m];
                                        if (builtObject5 != null && !builtObject5.HasBeenDestroyed && builtObject5.Empire == command.TargetHabitat.Empire && builtObject5.SensorTraceScannerPower > num43)
                                        {
                                            num43 = builtObject5.SensorTraceScannerPower;
                                            flag17 = true;
                                        }
                                    }
                                }
                            }
                            if (FirstExecutionOfCommand)
                            {
                                if (dockingBayList == null || builtObjectList3 == null)
                                {
                                    mission.CompleteCommand(ignoreRepeatCommands: true);
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                    break;
                                }
                                if (DockedAt != null)
                                {
                                    mission.CompleteCommand(ignoreRepeatCommands: true);
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                    break;
                                }
                                if (command.TargetBuiltObject != null)
                                {
                                    ParentHabitat = null;
                                    ParentBuiltObject = command.TargetBuiltObject;
                                    ParentOffsetX = Xpos - ParentBuiltObject.Xpos;
                                    ParentOffsetY = Ypos - ParentBuiltObject.Ypos;
                                }
                                else if (command.TargetHabitat != null)
                                {
                                    ParentBuiltObject = null;
                                    ParentHabitat = command.TargetHabitat;
                                    ParentOffsetX = Xpos - ParentHabitat.Xpos;
                                    ParentOffsetY = Ypos - ParentHabitat.Ypos;
                                }
                                PreferredSpeed = 0f;
                                _LastDockDistance = 536870911.0;
                                if (PirateEmpireId > 0 && Owner == null && stellarObject2 != null && stellarObject2.Empire != null && stellarObject2.Empire.PirateEmpireBaseHabitat == null && stellarObject2.Empire != _Galaxy.IndependentEmpire && flag17 && stellarObject2.Empire.PirateRelations != null)
                                {
                                    PirateRelation relationByOtherEmpireId = stellarObject2.Empire.PirateRelations.GetRelationByOtherEmpireId(PirateEmpireId);
                                    if (relationByOtherEmpireId != null && relationByOtherEmpireId.Type == PirateRelationType.None && num43 >= SensorTraceScannerJamming && Characters != null)
                                    {
                                        bool flag18 = false;
                                        Empire actualEmpire = ActualEmpire;
                                        if (Role == BuiltObjectRole.Freight && actualEmpire != null && actualEmpire.PirateMissions != null && actualEmpire.PirateMissions.ContainsEquivalent(stellarObject2, EmpireActivityType.Smuggle))
                                        {
                                            flag18 = true;
                                        }
                                        if (!flag18)
                                        {
                                            double num44 = 0.2;
                                            double d = 0.01 * (double)Characters.GetHighestSkillLevel(CharacterSkillType.SmugglingEvasion);
                                            num44 -= num44 * Math.Sqrt(d);
                                            num44 = Math.Max(0.01, num44);
                                            if (Galaxy.Rnd.NextDouble() < num44)
                                            {
                                                Empire = relationByOtherEmpireId.OtherEmpire;
                                                string description = string.Format(TextResolver.GetText("Pirate Smuggler Detected Ours"), Name, stellarObject2.Empire.Name, stellarObject2.Name);
                                                relationByOtherEmpireId.OtherEmpire.SendMessageToEmpire(relationByOtherEmpireId.OtherEmpire, EmpireMessageType.PirateSmugglerDetected, this, description);
                                                string description2 = string.Format(TextResolver.GetText("Pirate Smuggler Detected Other"), Name, relationByOtherEmpireId.OtherEmpire.Name, stellarObject2.Name);
                                                stellarObject2.Empire.SendMessageToEmpire(stellarObject2.Empire, EmpireMessageType.PirateSmugglerDetected, this, description2);
                                                _Galaxy.DoCharacterEvent(CharacterEventType.SmugglingDetection, stellarObject2, Characters);
                                                ClearPreviousMissionRequirements();
                                                AssignMission(BuiltObjectMissionType.Escape, stellarObject2, null, BuiltObjectMissionPriority.High);
                                                result = timePassed;
                                                break;
                                            }
                                        }
                                    }
                                }
                                FirstExecutionOfCommand = false;
                            }
                            if ((dockingBayList == null || dockingBayList.Count <= 0) && stellarObject2 != null && stellarObject2 is BuiltObject)
                            {
                                BuiltObject builtObject6 = (BuiltObject)stellarObject2;
                                if (builtObject6.ParentHabitat != null && builtObject6.ParentHabitat.Population != null && builtObject6.ParentHabitat.Population.TotalAmount > 0 && builtObject6.ParentHabitat.Empire != null)
                                {
                                    CheckClearDocking(forceUndock: true);
                                    stellarObject2 = builtObject6.ParentHabitat;
                                    dockingBayList = stellarObject2.DockingBays;
                                    builtObjectList3 = stellarObject2.DockingBayWaitQueue;
                                    ParentHabitat = builtObject6.ParentHabitat;
                                    ParentBuiltObject = null;
                                    ParentOffsetX = Xpos - ParentHabitat.Xpos;
                                    ParentOffsetY = Ypos - ParentHabitat.Ypos;
                                    PreferredSpeed = 0f;
                                    _LastDockDistance = 536870911.0;
                                }
                            }
                            if (DockedAt == null)
                            {
                                PreferredSpeed = 0f;
                                if (builtObjectList3.IndexOf(this) < 0)
                                {
                                    builtObjectList3.Add(this);
                                }
                                for (int n = 0; n < dockingBayList.Count; n++)
                                {
                                    if (dockingBayList[n].DockedShip == null)
                                    {
                                        if (builtObjectList3.Count <= 0)
                                        {
                                            dockingBayList[n].DockedShip = this;
                                            DockedAt = stellarObject2;
                                            PreferredSpeed = CruiseSpeed;
                                            break;
                                        }
                                        if (builtObjectList3[0] == this)
                                        {
                                            dockingBayList[n].DockedShip = this;
                                            DockedAt = stellarObject2;
                                            builtObjectList3.Remove(this);
                                            PreferredSpeed = CruiseSpeed;
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                            double num45 = -1.0;
                            double num46 = -1.0;
                            if (command.TargetBuiltObject != null)
                            {
                                BuiltObject targetBuiltObject7 = command.TargetBuiltObject;
                                num45 = targetBuiltObject7.Xpos;
                                num46 = targetBuiltObject7.Ypos;
                            }
                            else if (command.TargetHabitat != null)
                            {
                                Habitat targetHabitat7 = command.TargetHabitat;
                                num45 = targetHabitat7.Xpos;
                                num46 = targetHabitat7.Ypos;
                            }
                            double num47 = (double)Math.Max((short)1, CruiseSpeed) / (double)AccelerationRate * ((double)Math.Max((short)1, CruiseSpeed) * 0.5) + (double)CurrentSpeed;
                            double num48 = galaxy.CalculateDistance(num45, num46, Xpos, Ypos);
                            if (num48 < num47)
                            {
                                PreferredSpeed = Math.Max(Galaxy.MovementImpulseSpeed, (int)((double)CruiseSpeed * (num48 / num47) - (double)Galaxy.MovementImpulseSpeed));
                            }
                            else
                            {
                                PreferredSpeed = CruiseSpeed;
                            }
                            bool arrived = false;
                            if (DockedAt == null)
                            {
                                DoMovement(timePassed, num45, num46, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: false, manageHeading: true, manageDeceleration: true, out arrived);
                            }
                            else
                            {
                                DoMovement(timePassed, num45, num46, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: true, manageHeading: true, manageDeceleration: true, out arrived);
                            }
                            double num49 = galaxy.CalculateDistance(num45, num46, Xpos, Ypos);
                            if (arrived)
                            {
                                result = ((!(_LastDockDistance < num49) || !(CurrentSpeed > 0f)) ? 0.0 : ((num49 - _LastDockDistance) / (double)CurrentSpeed));
                                PreferredSpeed = 0f;
                                CurrentSpeed = 0f;
                            }
                            _LastDockDistance = num48;
                            break;
                        }
                    case CommandAction.ConditionalHyperTo:
                        {
                            double num96 = _Galaxy.CalculateDistance(num, num2, Xpos, Ypos);
                            if (num96 > (double)Galaxy.HyperJumpThreshhold)
                            {
                                if (WarpSpeed > 0)
                                {
                                    bool flag27 = false;
                                    BuiltObject builtObject14 = CheckForHyperExitGravityWell(num, num2);
                                    if (builtObject14 != null)
                                    {
                                        double num97 = _Galaxy.CalculateDistance(Xpos, Ypos, builtObject14.Xpos, builtObject14.Ypos);
                                        double num98 = num97 / (double)builtObject14.HyperStopRange;
                                        if (num98 < 1.5)
                                        {
                                            flag27 = true;
                                        }
                                    }
                                    if (flag27)
                                    {
                                        HyperEnterStartAnimation = false;
                                        _HyperjumpAboutToEnter = false;
                                        _HyperjumpPrepare = false;
                                        mission.CompleteCommand();
                                        FirstExecutionOfCommand = true;
                                        result = 0.0;
                                        break;
                                    }
                                    if (!BaconBuiltObject.SendShipTowardsEdgeOfGravityWell(this))
                                    {
                                        Command command9 = command.Clone();
                                        command9.Action = CommandAction.HyperTo;
                                        Mission.CompleteCommand();
                                        Mission.InsertCommandAtTop(command9);
                                        mission = Mission;
                                        command = Mission.FastPeekCurrentCommand();
                                        FirstExecutionOfCommand = true;
                                    }
                                    goto case CommandAction.HyperTo;
                                }
                                if (WithinFuelRange(num, num2, 0.0) || mission.ManuallyAssigned)
                                {
                                    HyperEnterStartAnimation = false;
                                    _HyperjumpAboutToEnter = false;
                                    _HyperjumpPrepare = false;
                                    Command command10 = command.Clone();
                                    bool flag28 = false;
                                    if (mission != null)
                                    {
                                        switch (mission.Type)
                                        {
                                            case BuiltObjectMissionType.Attack:
                                            case BuiltObjectMissionType.Bombard:
                                            case BuiltObjectMissionType.Capture:
                                            case BuiltObjectMissionType.Raid:
                                                flag28 = true;
                                                break;
                                        }
                                    }
                                    Mission.CompleteCommand();
                                    bool flag29 = false;
                                    if (!flag28 && !Mission.CheckCommandsForAction(CommandAction.MoveTo, 2))
                                    {
                                        command10.Action = CommandAction.MoveTo;
                                        Mission.InsertCommandAtTop(command10);
                                        flag29 = true;
                                    }
                                    mission = Mission;
                                    command = Mission.FastPeekCurrentCommand();
                                    FirstExecutionOfCommand = true;
                                    if (flag28 || !flag29)
                                    {
                                        break;
                                    }
                                    goto case CommandAction.MoveTo;
                                }
                                HyperEnterStartAnimation = false;
                                _HyperjumpAboutToEnter = false;
                                _HyperjumpPrepare = false;
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                                result = timePassed;
                                break;
                            }
                            HyperEnterStartAnimation = false;
                            _HyperjumpAboutToEnter = false;
                            _HyperjumpPrepare = false;
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                            break;
                        }
                    case CommandAction.HyperTo:
                        {
                            if (FirstExecutionOfCommand)
                            {
                                _HyperjumpAboutToEnterSoundPlayed = false;
                                HyperEnterStartAnimation = true;
                                _LastHyperjumpDistance = 0f;
                                long num7 = Math.Max(0L, HyperjumpInitiate * 1000 + (Galaxy.Rnd.Next(0, 2000) - 1000));
                                _HyperjumpCountdown = galaxy.CurrentStarDate + num7;
                                double baseHyperJumpAccuracy = Galaxy.BaseHyperJumpAccuracy;
                                galaxy.SelectHyperJumpExitPoint(out _HyperjumpX, out _HyperjumpY, baseHyperJumpAccuracy);
                                _LastHyperDistance = 536870911.0;
                                _LastPositionX = Xpos;
                                _LastPositionY = Ypos;
                                _Angle = (float)Galaxy.DetermineAngle(Xpos, Ypos, num + _HyperjumpX, num2 + _HyperjumpY);
                                if (mission.IsShipGroupMission && ShipGroup != null)
                                {
                                    PreferredSpeed = Math.Min(ShipGroup.CruiseSpeed, CruiseSpeed);
                                    TargetSpeed = Math.Min(ShipGroup.CruiseSpeed, CruiseSpeed);
                                }
                                else
                                {
                                    PreferredSpeed = CruiseSpeed;
                                    TargetSpeed = CruiseSpeed;
                                }
                                if (CurrentSpeed > (float)TopSpeed)
                                {
                                    CurrentSpeed = TargetSpeed;
                                    UpdatePosition();
                                    CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(time);
                                }
                                if (Fighters != null && Fighters.Count > 0 && BaconBuiltObject.IsOutsideStarGravityWell(this))
                                {
                                    for (int i = 0; i < Fighters.Count; i++)
                                    {
                                        Fighter fighter = Fighters[i];
                                        if (!fighter.OnboardCarrier && !fighter.HasBeenDestroyed)
                                        {
                                            fighter.ReturnToCarrier();
                                        }
                                    }
                                }
                                TargetHeading = _Angle;
                                FirstExecutionOfCommand = false;
                                _FirstHyperjumpExecution = true;
                            }
                            if (WarpSpeed <= 0)
                            {
                                ClearPreviousMissionRequirements();
                                break;
                            }
                            double num8;
                            if (starDate >= _HyperjumpCountdown && CanHyperJump && WarpSpeed > 0 && CheckFightersOnboardAndRetrieve())
                            {
                                _HyperjumpPrepare = false;
                                HyperEnterStartAnimation = false;
                                if (_FirstHyperjumpExecution)
                                {
                                    if (DetectHyperDeny(_Galaxy))
                                    {
                                        CanHyperJump = false;
                                        result = 0.0;
                                        break;
                                    }
                                    CanHyperJump = true;
                                    if (ActualEmpire != null)
                                    {
                                        ActualEmpire.ResolveSystemVisibility(this, excludeBuiltObject: true);
                                    }
                                    Attackers.Clear();
                                    _FirstHyperjumpExecution = false;
                                    CheckClearDocking(forceUndock: true);
                                    if (Empire != null)
                                    {
                                        Empire.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstHyperjump, this);
                                    }
                                }
                                if (mission.IsShipGroupMission && ShipGroup != null)
                                {
                                    ShipGroup.RemoveShipsWithoutHyperdrive();
                                    if (ShipGroup.WarpSpeed > 0)
                                    {
                                        PreferredSpeed = Math.Min(WarpSpeedWithBonuses, ShipGroup.WarpSpeed);
                                    }
                                    else
                                    {
                                        PreferredSpeed = WarpSpeedWithBonuses;
                                    }
                                    CurrentSpeed = PreferredSpeed;
                                }
                                else if (mission.Type == BuiltObjectMissionType.Escort && mission.TargetBuiltObject != null)
                                {
                                    if (mission.TargetBuiltObject.WarpSpeedWithBonuses > 0)
                                    {
                                        PreferredSpeed = Math.Min(WarpSpeedWithBonuses, mission.TargetBuiltObject.WarpSpeedWithBonuses);
                                    }
                                    else
                                    {
                                        PreferredSpeed = WarpSpeedWithBonuses;
                                    }
                                    CurrentSpeed = PreferredSpeed;
                                }
                                else
                                {
                                    PreferredSpeed = WarpSpeedWithBonuses;
                                    CurrentSpeed = WarpSpeedWithBonuses;
                                }
                                NearestSystemStar = null;
                                _Angle = (float)Galaxy.DetermineAngle(Xpos, Ypos, num + _HyperjumpX, num2 + _HyperjumpY);
                                TargetHeading = _Angle;
                                float heading = Heading;
                                Heading = TargetHeading;
                                num8 = (double)CurrentSpeed * timePassed;
                                double hyperExitX = num + _HyperjumpX;
                                double hyperExitY = num2 + _HyperjumpY;
                                galaxy.CalculateDistance(Xpos, Ypos, hyperExitX, hyperExitY);
                                _LastHyperjumpDistance += (float)num8;
                                _LastHyperDistance = galaxy.CalculateDistance(Xpos, Ypos, hyperExitX, hyperExitY);
                                ConsumeFuel(timePassed);
                                Xpos += Math.Cos(Heading) * num8;
                                Ypos += Math.Sin(Heading) * num8;
                                CheckFuelHandicap();
                                if (CheckWhetherArrived(Xpos, Ypos, hyperExitX, hyperExitY, 0.0))
                                {
                                    _HyperjumpJustExited = true;
                                    HyperExitStartAnimation = true;
                                    _HyperjumpPrepare = false;
                                    HyperEnterStartAnimation = false;
                                    CheckForHyperExitGravityWells(ref hyperExitX, ref hyperExitY);
                                    CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(time);
                                    Xpos = hyperExitX;
                                    Ypos = hyperExitY;
                                    if (ParentHabitat != null)
                                    {
                                        ParentOffsetX = Xpos - ParentHabitat.Xpos;
                                        ParentOffsetY = Ypos - ParentHabitat.Ypos;
                                    }
                                    else if (ParentBuiltObject != null)
                                    {
                                        ParentOffsetX = Xpos - ParentBuiltObject.Xpos;
                                        ParentOffsetY = Ypos - ParentBuiltObject.Ypos;
                                    }
                                    _LastPositionX = Xpos;
                                    _LastPositionY = Ypos;
                                    if (ShipGroup == null)
                                    {
                                        _Galaxy.DoCharacterEvent(CharacterEventType.HyperjumpExit, this, Characters);
                                    }
                                    else
                                    {
                                        _Galaxy.DoCharacterEvent(CharacterEventType.HyperjumpExit, this, ShipGroup.ObtainCharacters());
                                    }
                                    Heading = heading;
                                    Habitat habitat = _Galaxy.FastFindNearestSystem(Xpos, Ypos);
                                    if (habitat != null)
                                    {
                                        double num9 = _Galaxy.CalculateDistance(Xpos, Ypos, habitat.Xpos, habitat.Ypos);
                                        if (num9 < (double)Galaxy.MaxSolarSystemSize + 1000.0)
                                        {
                                            NearestSystemStar = habitat;
                                        }
                                    }
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                    if (mission.IsShipGroupMission && ShipGroup != null)
                                    {
                                        CurrentSpeed = ShipGroup.CruiseSpeed;
                                    }
                                    else
                                    {
                                        CurrentSpeed = CruiseSpeed;
                                    }
                                    if (ActualEmpire != null)
                                    {
                                        ActualEmpire.ResolveSystemVisibility(this, excludeBuiltObject: false);
                                    }
                                    CheckMissionStillValid(time);
                                }
                                UpdateIndexesForMovement(x, y, galaxy, performIndexCheck: true);
                                break;
                            }
                            _HyperjumpPrepare = true;
                            FirstExecutionOfCommand = false;
                            if (starDate + 300 > _HyperjumpCountdown && CanHyperJump)
                            {
                                if (DetectHyperDeny(_Galaxy))
                                {
                                    CanHyperJump = false;
                                    result = 0.0;
                                    break;
                                }
                                CanHyperJump = true;
                                _HyperjumpAboutToEnter = true;
                            }
                            AccelerateToTargetSpeed(timePassed);
                            num8 = (double)CurrentSpeed * timePassed;
                            _Angle = (float)Galaxy.DetermineAngle(Xpos, Ypos, num, num2);
                            TargetHeading = _Angle;
                            CalculateCurrentHeading(timePassed);
                            Xpos += Math.Cos(Heading) * num8;
                            Ypos += Math.Sin(Heading) * num8;
                            UpdateIndexesForMovement(x, y, galaxy, performIndexCheck: false);
                            break;
                        }
                    case CommandAction.ImpulseTo:
                        if (FirstExecutionOfCommand)
                        {
                            PreferredSpeed = Galaxy.MovementImpulseSpeed;
                            FirstExecutionOfCommand = false;
                        }
                        result = DoMovement(timePassed, num, num2, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: true, manageHeading: true, manageDeceleration: true);
                        break;
                    case CommandAction.Load:
                        if (DockedAt == null)
                        {
                            CheckCancelContracts();
                            mission.CompleteCommand(ignoreRepeatCommands: true);
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                            break;
                        }
                        if (FirstExecutionOfCommand)
                        {
                            FirstExecutionOfCommand = false;
                        }
                        if (command.Commodities != null && command.Commodities.Count > 0)
                        {
                            Cargo cargo6 = command.Commodities[0];
                            Cargo cargo7 = null;
                            Contract contractForCargoWithRemainingPickup = ContractsToFulfill.GetContractForCargoWithRemainingPickup(cargo6);
                            if (cargo6.CommodityResource != null)
                            {
                                Resource commodityResource2 = cargo6.CommodityResource;
                                if (DockedAt.Cargo != null)
                                {
                                    cargo7 = DockedAt.Cargo.GetCargo(commodityResource2, cargo6.EmpireId);
                                }
                            }
                            else if (cargo6.CommodityComponent != null)
                            {
                                Component commodityComponent2 = cargo6.CommodityComponent;
                                if (DockedAt.Cargo != null)
                                {
                                    cargo7 = DockedAt.Cargo.GetCargo(commodityComponent2, cargo6.EmpireId);
                                }
                            }
                            if (cargo7 != null)
                            {
                                if (cargo7.Amount < 0 || cargo7.Amount > 1073741823)
                                {
                                    cargo7.Amount = 0;
                                }
                                if (cargo6.Amount < 0 || cargo6.Amount > 1073741823)
                                {
                                    command.Commodities.Remove(cargo6);
                                    result = timePassed;
                                    break;
                                }
                                int num50 = -1;
                                if (DockedAt.DockingBays != null)
                                {
                                    num50 = DockedAt.DockingBays.IndexOf(this);
                                }
                                if (num50 >= 0)
                                {
                                    int capacity2 = DockedAt.DockingBays[num50].Capacity;
                                    int num51 = (int)((double)capacity2 * timePassed);
                                    if (num51 < 1)
                                    {
                                        num51 = 1;
                                    }
                                    if (num51 > cargo7.Amount)
                                    {
                                        num51 = cargo7.Amount;
                                    }
                                    if (num51 > cargo6.Amount)
                                    {
                                        num51 = cargo6.Amount;
                                    }
                                    if (num51 > CargoSpace)
                                    {
                                        num51 = CargoSpace;
                                        cargo6.Amount = 0;
                                    }
                                    num51 = Math.Max(0, Math.Min(num51, cargo6.Amount));
                                    cargo6.Amount -= num51;
                                    if (num51 <= 0 && cargo7.Amount <= 0)
                                    {
                                        num51 = 0;
                                        cargo7.Amount = 0;
                                        cargo6.Amount = 0;
                                    }
                                    if (contractForCargoWithRemainingPickup != null)
                                    {
                                        int num52 = contractForCargoWithRemainingPickup.AmountToFulfill - contractForCargoWithRemainingPickup.AmountPickedUp;
                                        int num53 = Math.Min(num52, num51);
                                        contractForCargoWithRemainingPickup.AmountPickedUp += num53;
                                        if (num51 > num52)
                                        {
                                            contractForCargoWithRemainingPickup = ContractsToFulfill.GetContractForCargoWithRemainingPickup(cargo6);
                                            if (contractForCargoWithRemainingPickup != null)
                                            {
                                                contractForCargoWithRemainingPickup.AmountPickedUp += num51 - num52;
                                            }
                                        }
                                        cargo7.Reserved -= num51;
                                        cargo7.Reserved = Math.Max(0, cargo7.Reserved);
                                    }
                                    cargo7.Amount -= num51;
                                    if (cargo7.Amount <= 0 && cargo7.Reserved <= 0)
                                    {
                                        DockedAt.Cargo.Remove(cargo7);
                                    }
                                    if (cargo6.Amount <= 0)
                                    {
                                        cargo6.Amount = 0;
                                        command.Commodities.Remove(cargo6);
                                    }
                                    if (cargo6.CommodityResource != null)
                                    {
                                        Cargo cargo8 = new Cargo(cargo6.CommodityResource, num51, cargo6.EmpireId);
                                        if (Cargo != null)
                                        {
                                            Cargo.Add(cargo8);
                                        }
                                    }
                                    else if (cargo6.CommodityComponent != null)
                                    {
                                        Cargo cargo9 = new Cargo(cargo6.CommodityComponent, num51, cargo6.EmpireId);
                                        if (Cargo != null)
                                        {
                                            Cargo.Add(cargo9);
                                        }
                                    }
                                    result = Math.Max(0.0, ((double)capacity2 * timePassed - (double)num51) / (double)capacity2);
                                }
                                else
                                {
                                    FinalizeContractsNotPresentAtLoad(DockedAt);
                                    mission.CompleteCommand(ignoreRepeatCommands: true);
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                }
                            }
                            else
                            {
                                command.Commodities.Remove(cargo6);
                                result = timePassed;
                            }
                        }
                        else if (command.Troops != null && command.Troops.Count > 0)
                        {
                            int num54 = -1;
                            if (DockedAt.DockingBays != null)
                            {
                                num54 = DockedAt.DockingBays.IndexOf(this);
                            }
                            if (num54 < 0 || TroopCapacityRemaining < 100)
                            {
                                break;
                            }
                            TroopList troopList3 = DockedAt.Troops;
                            CharacterList characterList = DockedAt.Characters;
                            bool flag19 = false;
                            if (DockedAt.Empire != Empire && DockedAt is Habitat)
                            {
                                Habitat habitat8 = (Habitat)DockedAt;
                                if (habitat8.InvadingTroops != null && habitat8.InvadingTroops.Count > 0 && habitat8.InvadingTroops[0].Empire == Empire)
                                {
                                    flag19 = true;
                                    troopList3 = habitat8.InvadingTroops;
                                    characterList = habitat8.InvadingCharacters;
                                }
                            }
                            else if (DockedAt.Empire == Empire && DockedAt is Habitat)
                            {
                                Habitat habitat9 = (Habitat)DockedAt;
                                if (habitat9.InvadingTroops != null && habitat9.InvadingTroops.Count > 0 && habitat9.InvadingTroops[0].Empire == Empire)
                                {
                                    flag19 = true;
                                    troopList3 = habitat9.InvadingTroops;
                                    characterList = habitat9.InvadingCharacters;
                                }
                            }
                            troopList3.Sort();
                            troopList3.Reverse();
                            foreach (Troop troop8 in command.Troops)
                            {
                                _ = troop8;
                                Troop troop = null;
                                if (troopList3 != null && troopList3.Count > 0 && Troops != null)
                                {
                                    if (flag19)
                                    {
                                        for (int num55 = 0; num55 < troopList3.Count; num55++)
                                        {
                                            Troop troop2 = troopList3[num55];
                                            if (troop2 != null && troop2.Empire == Empire && troop2.Size <= TroopCapacityRemaining)
                                            {
                                                troop = troop2;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int infantryAmount = 0;
                                        int armorAmount = 0;
                                        int artilleryAmount = 0;
                                        int specialForcesAmount = 0;
                                        if (ShipGroup != null)
                                        {
                                            ShipGroup.GetTroopLoadoutTargetAmounts(out infantryAmount, out artilleryAmount, out armorAmount, out specialForcesAmount);
                                        }
                                        else
                                        {
                                            GetTroopLoadoutTargetAmounts(out infantryAmount, out artilleryAmount, out armorAmount, out specialForcesAmount);
                                        }
                                        troop = null;
                                        if (ShipGroup == null && TroopLoadoutInfantry == byte.MaxValue && TroopLoadoutArmored == byte.MaxValue && TroopLoadoutArtillery == byte.MaxValue && TroopLoadoutSpecialForces == byte.MaxValue)
                                        {
                                            troop = troopList3.GetFirstNonGarrisonedWithinSize(TroopType.Undefined, TroopCapacityRemaining);
                                        }
                                        else if (ShipGroup != null && ShipGroup.TroopLoadoutInfantry == byte.MaxValue && ShipGroup.TroopLoadoutArmored == byte.MaxValue && ShipGroup.TroopLoadoutArtillery == byte.MaxValue && ShipGroup.TroopLoadoutSpecialForces == byte.MaxValue)
                                        {
                                            troop = troopList3.GetFirstNonGarrisonedWithinSize(TroopType.Undefined, TroopCapacityRemaining);
                                        }
                                        else
                                        {
                                            int infantryCount = Troops.CountByType(TroopType.Infantry);
                                            int armorCount = Troops.CountByType(TroopType.Armored);
                                            int artilleryCount = Troops.CountByType(TroopType.Artillery);
                                            int specialForcesCount = Troops.CountByType(TroopType.SpecialForces);
                                            if (ShipGroup != null)
                                            {
                                                ShipGroup.GetTroopCountsByType(out infantryCount, out artilleryCount, out armorCount, out specialForcesCount);
                                            }
                                            if (troop == null && infantryAmount > 0 && infantryCount < infantryAmount)
                                            {
                                                troop = troopList3.GetFirstNonGarrisoned(TroopType.Infantry);
                                            }
                                            if (troop == null && armorAmount > 0 && armorCount < armorAmount)
                                            {
                                                troop = troopList3.GetFirstNonGarrisoned(TroopType.Armored);
                                            }
                                            if (troop == null && artilleryAmount > 0 && artilleryCount < artilleryAmount)
                                            {
                                                troop = troopList3.GetFirstNonGarrisoned(TroopType.Artillery);
                                            }
                                            if (troop == null && specialForcesAmount > 0 && specialForcesCount < specialForcesAmount)
                                            {
                                                troop = troopList3.GetFirstNonGarrisoned(TroopType.SpecialForces);
                                            }
                                            if (troop == null && specialForcesAmount > 0 && specialForcesCount < specialForcesAmount)
                                            {
                                                troop = troopList3.GetFirstNonGarrisonedWithinSize(TroopType.Armored, TroopCapacityRemaining);
                                            }
                                            if (troop == null && artilleryAmount > 0 && artilleryCount < artilleryAmount)
                                            {
                                                troop = troopList3.GetFirstNonGarrisoned(TroopType.Infantry);
                                            }
                                            if (troop == null && armorAmount > 0 && armorCount < armorAmount)
                                            {
                                                troop = troopList3.GetFirstNonGarrisoned(TroopType.Infantry);
                                            }
                                            if (troop == null)
                                            {
                                                troop = troopList3.GetFirstNonGarrisoned(TroopType.Infantry);
                                            }
                                        }
                                    }
                                    if (troop != null && troop.Size <= TroopCapacityRemaining)
                                    {
                                        troop.Garrisoned = false;
                                        troopList3.Remove(troop);
                                        troop.BuiltObject = this;
                                        Troops.Add(troop);
                                        if (!flag19 || characterList == null || characterList.Count <= 0)
                                        {
                                            continue;
                                        }
                                        Character[] array = ListHelper.ToArrayThreadSafe(characterList);
                                        foreach (Character character in array)
                                        {
                                            if (character != null && character.Empire == Empire)
                                            {
                                                character.CompleteLocationTransfer(this, _Galaxy);
                                            }
                                        }
                                        continue;
                                    }
                                    command.Troops.Clear();
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                    break;
                                }
                                command.Troops.Clear();
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                                result = timePassed;
                                break;
                            }
                            command.Troops.Clear();
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        else if (command.Population != null && command.Population.Count > 0)
                        {
                            int num57 = -1;
                            if (DockedAt.DockingBays != null)
                            {
                                num57 = DockedAt.DockingBays.IndexOf(this);
                            }
                            if (num57 < 0)
                            {
                                break;
                            }
                            if (Population != null)
                            {
                                Population.Clear();
                                Population.RecalculateTotalAmount();
                            }
                            if (PopulationCapacityRemaining - command.Population.TotalAmount >= 0)
                            {
                                foreach (Population item6 in command.Population)
                                {
                                    if (DockedAt.Population != null && DockedAt.Population.Count > 0 && Population != null)
                                    {
                                        if (DockedAt.Population.TotalAmount > 30000000 + item6.Amount)
                                        {
                                            if (DockedAt.Population[item6.Race] != null && DockedAt.Population[item6.Race].Amount >= item6.Amount)
                                            {
                                                Population.Add(new Population(item6.Race, item6.Amount));
                                                DockedAt.Population[item6.Race].Amount -= item6.Amount;
                                                DockedAt.Population.RecalculateTotalAmount();
                                            }
                                            continue;
                                        }
                                        command.Population.Clear();
                                        mission.CompleteCommand();
                                        FirstExecutionOfCommand = true;
                                        result = timePassed;
                                        break;
                                    }
                                    command.Population.Clear();
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                    break;
                                }
                                if (Population != null)
                                {
                                    Population.RecalculateTotalAmount();
                                }
                                command.Population.Clear();
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                                result = timePassed;
                            }
                            else
                            {
                                mission.CompleteCommand();
                                FirstExecutionOfCommand = true;
                                result = timePassed;
                            }
                        }
                        else
                        {
                            FinalizeContractsNotPresentAtLoad(DockedAt);
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        break;
                    case CommandAction.MoveTo:
                        if (FirstExecutionOfCommand)
                        {
                            if (_ExecutingShipGroupCommand && ShipGroup != null)
                            {
                                PreferredSpeed = ShipGroup.CruiseSpeed;
                            }
                            else
                            {
                                PreferredSpeed = CruiseSpeed;
                            }
                            FirstExecutionOfCommand = false;
                        }
                        if (WarpSpeed > 0 && num > -1.0 && num2 > -1.0 && Math.Abs(Xpos - num) > (double)Galaxy.HyperJumpThreshhold && !BaconBuiltObject.ShouldSendShipTowardEdgeOfGravityWell(this) && Math.Abs(Ypos - num2) > (double)Galaxy.HyperJumpThreshhold)
                        {
                            Command command12 = new Command(CommandAction.ConditionalHyperTo, num, num2);
                            Mission.InsertCommandAtTop(command12);
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        if (mission != null && mission.Type == BuiltObjectMissionType.Explore && Empire != null && command.TargetHabitat != null && !_Galaxy.CheckShouldExplore(Empire, command.TargetHabitat))
                        {
                            mission.CompleteCommand(ignoreRepeatCommands: true);
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        else
                        {
                            result = DoMovement(timePassed, num, num2, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: true, manageHeading: true, manageDeceleration: true);
                        }
                        break;
                    case CommandAction.SprintTo:
                        if (FirstExecutionOfCommand)
                        {
                            if (_ExecutingShipGroupCommand && ShipGroup != null)
                            {
                                PreferredSpeed = ShipGroup.TopSpeed;
                            }
                            else
                            {
                                PreferredSpeed = TopSpeed;
                            }
                            FirstExecutionOfCommand = false;
                        }
                        result = DoMovement(timePassed, num, num2, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: true, manageHeading: true, manageDeceleration: true);
                        break;
                    case CommandAction.Undock:
                        {
                            if (DockedAt == null)
                            {
                                mission.CompleteCommand(ignoreRepeatCommands: true);
                                FirstExecutionOfCommand = true;
                                result = timePassed;
                                break;
                            }
                            if (FirstExecutionOfCommand)
                            {
                                if (DockedAt is BuiltObject)
                                {
                                    ParentBuiltObject = (BuiltObject)DockedAt;
                                }
                                else if (DockedAt is Habitat)
                                {
                                    ParentHabitat = (Habitat)DockedAt;
                                }
                                ParentOffsetX = Xpos - DockedAt.Xpos;
                                ParentOffsetY = Ypos - DockedAt.Ypos;
                                command.TargetRelativeXpos = (float)(Math.Cos(Heading) * (double)Galaxy.UndockRange);
                                command.TargetRelativeYpos = (float)(Math.Sin(Heading) * (double)Galaxy.UndockRange);
                                CurrentSpeed = Galaxy.MovementImpulseSpeed;
                                PreferredSpeed = Galaxy.MovementImpulseSpeed;
                                FirstExecutionOfCommand = false;
                            }
                            DoMovement(timePassed, num, num2, x, y, command.TargetRelativeXpos, command.TargetRelativeYpos, galaxy, manageArrival: false, manageHeading: true, manageDeceleration: true);
                            double num58 = galaxy.CalculateDistance(DockedAt.Xpos, DockedAt.Ypos, Xpos, Ypos);
                            if (num58 >= (double)Galaxy.UndockRange)
                            {
                                int num59 = -1;
                                if (DockedAt.DockingBays != null)
                                {
                                    num59 = DockedAt.DockingBays.IndexOf(this);
                                }
                                if (num59 >= 0)
                                {
                                    DockedAt.DockingBays[num59].DockedShip = null;
                                }
                                DockedAt = null;
                                command = mission.ShowNextCommand();
                                if (command != null && (command.Action == CommandAction.MoveTo || command.Action == CommandAction.SprintTo || command.Action == CommandAction.HyperTo))
                                {
                                    PreferredSpeed = CruiseSpeed;
                                }
                                ParentBuiltObject = null;
                                ParentHabitat = null;
                                ParentOffsetX = -2000000001.0;
                                ParentOffsetY = -2000000001.0;
                                mission.CompleteCommand();
                                result = ((!(CurrentSpeed > 0f)) ? 0.0 : ((num58 - (double)Galaxy.UndockRange) / (double)CurrentSpeed));
                                FirstExecutionOfCommand = true;
                            }
                            break;
                        }
                    case CommandAction.Unload:
                        if (DockedAt == null)
                        {
                            CheckCancelContracts();
                            mission.CompleteCommand(ignoreRepeatCommands: true);
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                            break;
                        }
                        if (DockedAt.CargoSpace <= 0 && command.Commodities != null && command.Commodities.Count > 0)
                        {
                            CheckCancelContracts();
                            if (Role == BuiltObjectRole.Freight && Cargo != null && Cargo.Count > 0)
                            {
                                Cargo.Clear();
                            }
                            mission.CompleteCommand(ignoreRepeatCommands: true);
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                            break;
                        }
                        if (FirstExecutionOfCommand)
                        {
                            if (mission.Type == BuiltObjectMissionType.ExtractResources && Cargo != null)
                            {
                                if (command.Commodities == null)
                                {
                                    command.Commodities = new CargoList();
                                }
                                foreach (Cargo item7 in Cargo)
                                {
                                    command.Commodities.Add(item7);
                                }
                            }
                            FirstExecutionOfCommand = false;
                        }
                        if (command.Commodities != null && command.Commodities.Count > 0)
                        {
                            Cargo cargo = command.Commodities[0];
                            Cargo cargo2 = null;
                            Contract contractForCargoWithRemainingDelivery = ContractsToFulfill.GetContractForCargoWithRemainingDelivery(cargo);
                            if (cargo.CommodityResource != null)
                            {
                                Resource commodityResource = cargo.CommodityResource;
                                if (Cargo != null)
                                {
                                    cargo2 = Cargo.GetCargo(commodityResource, cargo.EmpireId);
                                }
                            }
                            else if (cargo.CommodityComponent != null)
                            {
                                Component commodityComponent = cargo.CommodityComponent;
                                if (Cargo != null)
                                {
                                    cargo2 = Cargo.GetCargo(commodityComponent, cargo.EmpireId);
                                }
                            }
                            if (cargo2 != null)
                            {
                                if (cargo2.Amount < 0 || cargo2.Amount > 1073741823)
                                {
                                    cargo2.Amount = 0;
                                }
                                if (cargo.Amount < 0 || cargo.Amount > 1073741823)
                                {
                                    command.Commodities.Remove(cargo);
                                    result = timePassed;
                                    if (Role == BuiltObjectRole.Freight && cargo2 != null && cargo2.Amount > 0)
                                    {
                                        cargo2.Amount = 0;
                                        Cargo.Remove(cargo2);
                                    }
                                    break;
                                }
                                int num12 = -1;
                                if (DockedAt.DockingBays != null)
                                {
                                    num12 = DockedAt.DockingBays.IndexOf(this);
                                }
                                if (num12 >= 0)
                                {
                                    int capacity = DockedAt.DockingBays[num12].Capacity;
                                    int num13 = (int)((double)capacity * timePassed);
                                    if (num13 < 1)
                                    {
                                        num13 = 1;
                                    }
                                    if (num13 > cargo2.Amount)
                                    {
                                        num13 = cargo2.Amount;
                                    }
                                    if (num13 > cargo.Amount)
                                    {
                                        num13 = cargo.Amount;
                                    }
                                    if (num13 > DockedAt.CargoSpace)
                                    {
                                        num13 = DockedAt.CargoSpace;
                                        cargo.Amount = 0;
                                    }
                                    cargo.Amount -= num13;
                                    if (contractForCargoWithRemainingDelivery != null)
                                    {
                                        contractForCargoWithRemainingDelivery.AmountDelivered += num13;
                                    }
                                    if (cargo.Amount <= 0)
                                    {
                                        command.Commodities.Remove(cargo);
                                        if (Role == BuiltObjectRole.Freight && cargo2 != null)
                                        {
                                            cargo2.Amount = 0;
                                        }
                                    }
                                    if (cargo.CommodityResource != null)
                                    {
                                        Cargo cargo3 = new Cargo(cargo.CommodityResource, num13, cargo.EmpireId);
                                        if (DockedAt.Cargo != null)
                                        {
                                            DockedAt.Cargo.Add(cargo3);
                                        }
                                    }
                                    else if (cargo.CommodityComponent != null)
                                    {
                                        Cargo cargo4 = new Cargo(cargo.CommodityComponent, num13, cargo.EmpireId);
                                        if (DockedAt.Cargo != null)
                                        {
                                            DockedAt.Cargo.Add(cargo4);
                                        }
                                    }
                                    cargo2.Amount -= num13;
                                    StellarObject stellarObject = DockedAt;
                                    if (DockedAt.ParentHabitat != null)
                                    {
                                        stellarObject = DockedAt.ParentHabitat;
                                    }
                                    double num14 = 0.0;
                                    if (stellarObject != null && stellarObject.Empire != null && stellarObject.Empire.PirateMissions != null && stellarObject.Empire.PirateMissions.Count > 0)
                                    {
                                        EmpireActivity firstByTargetAndType = stellarObject.Empire.PirateMissions.GetFirstByTargetAndType(stellarObject, EmpireActivityType.Smuggle);
                                        if (firstByTargetAndType != null && firstByTargetAndType.RequestingEmpire != ActualEmpire && cargo2.CommodityIsResource && (firstByTargetAndType.ResourceId == byte.MaxValue || firstByTargetAndType.ResourceId == cargo2.Resource.ResourceID))
                                        {
                                            num14 = firstByTargetAndType.Price * (double)num13;
                                            if (stellarObject.Empire == galaxy.PlayerEmpire || DockedAt.Empire == galaxy.PlayerEmpire || ActualEmpire == galaxy.PlayerEmpire)
                                            {
                                                firstByTargetAndType.PlayerAmountDelivered += num13;
                                                firstByTargetAndType.PlayerIncomeEarned += num14;
                                            }
                                            stellarObject.Empire.PerformPrivateTransaction(0.0 - num14);
                                        }
                                    }
                                    bool flag9 = false;
                                    if (PirateEmpireId > 0 && cargo.EmpireId != PirateEmpireId)
                                    {
                                        Empire byEmpireId = galaxy.PirateEmpires.GetByEmpireId(PirateEmpireId);
                                        if (byEmpireId != null)
                                        {
                                            double num15 = 1.0;
                                            if (Characters != null && Characters.Count > 0)
                                            {
                                                num15 += 0.01 * (double)Characters.GetHighestSkillLevel(CharacterSkillType.SmugglingIncome);
                                            }
                                            double num16 = galaxy.CalculateCurrentCargoValue(cargo2, num13);
                                            double num17 = num16 * 0.25 * byEmpireId.ColonyIncomeFactor * byEmpireId.SmugglingIncomeFactor;
                                            double num18 = num14 + num17;
                                            num18 *= num15;
                                            num18 = byEmpireId.ApplyCorruptionToIncome(num18);
                                            byEmpireId.StateMoney += num18;
                                            byEmpireId.PirateEconomy.PerformIncome(num18, PirateIncomeType.Smuggling, starDate);
                                            byEmpireId.Counters.PirateSmugglingIncome += num18;
                                            flag9 = true;
                                        }
                                    }
                                    if (flag9 && cargo.Amount <= 0)
                                    {
                                        _Galaxy.DoCharacterEvent(CharacterEventType.SmugglingSuccess, DockedAt, Characters);
                                    }
                                    if (cargo2.Amount <= 0)
                                    {
                                        Cargo.Remove(cargo2);
                                    }
                                    result = Math.Max(0.0, ((double)capacity * timePassed - (double)num13) / (double)capacity);
                                }
                                else
                                {
                                    CheckCancelContracts();
                                    mission.CompleteCommand(ignoreRepeatCommands: true);
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                }
                            }
                            else
                            {
                                command.Commodities.Remove(cargo);
                                result = timePassed;
                            }
                        }
                        else if (command.Troops != null && command.Troops.Count > 0)
                        {
                            int num19 = -1;
                            if (DockedAt.DockingBays != null)
                            {
                                num19 = DockedAt.DockingBays.IndexOf(this);
                            }
                            if (num19 < 0)
                            {
                                break;
                            }
                            TroopList troopList = DockedAt.Troops;
                            _ = DockedAt.Characters;
                            if (DockedAt.Empire == Empire && DockedAt is Habitat)
                            {
                                Habitat habitat2 = (Habitat)DockedAt;
                                if (habitat2.InvadingTroops != null && habitat2.InvadingTroops.Count > 0 && habitat2.InvadingTroops[0].Empire == Empire)
                                {
                                    troopList = habitat2.InvadingTroops;
                                    _ = habitat2.InvadingCharacters;
                                }
                            }
                            if (troopList == null || Troops == null || DockedAt.TroopCapacityRemaining - command.Troops.TotalSize < 0)
                            {
                                break;
                            }
                            TroopList troopList2 = new TroopList();
                            foreach (Troop troop9 in command.Troops)
                            {
                                int num20 = (num20 = Troops.IndexOf(troop9));
                                if (num20 >= 0)
                                {
                                    troopList2.Add(troop9);
                                }
                                if (DockedAt is BuiltObject)
                                {
                                    troop9.BuiltObject = (BuiltObject)DockedAt;
                                }
                                else if (DockedAt is Habitat)
                                {
                                    troop9.Colony = (Habitat)DockedAt;
                                }
                                troopList.Add(troop9);
                            }
                            foreach (Troop item8 in troopList2)
                            {
                                Troops.Remove(item8);
                            }
                            command.Troops.Clear();
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        else if (command.Population != null && command.Population.Count > 0)
                        {
                            int num21 = -1;
                            if (DockedAt.DockingBays != null)
                            {
                                num21 = DockedAt.DockingBays.IndexOf(this);
                            }
                            if (num21 >= 0)
                            {
                                if (DockedAt.Population != null && Population != null)
                                {
                                    foreach (Population item9 in Population)
                                    {
                                        if (DockedAt is BuiltObject || item9.Amount >= 1000000)
                                        {
                                            bool flag10 = true;
                                            if (DockedAt is Habitat && (DockedAt.Population.Count <= 0 || DockedAt.Population.TotalAmount <= 0))
                                            {
                                                flag10 = false;
                                            }
                                            if (flag10)
                                            {
                                                DockedAt.Population.Add(item9);
                                                DockedAt.Population.RecalculateTotalAmount();
                                            }
                                        }
                                        ProcessTourists(item9);
                                    }
                                }
                                if (Population != null)
                                {
                                    Population.Clear();
                                    Population.RecalculateTotalAmount();
                                }
                            }
                            command.Population.Clear();
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        else
                        {
                            CheckCancelContracts();
                            if (Role == BuiltObjectRole.Freight && Cargo != null && Cargo.Count > 0)
                            {
                                Cargo.Clear();
                            }
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        break;
                    case CommandAction.ReassignMission:
                        if (!RevertToPreviousMission())
                        {
                            if (Mission != null && Mission.TargetSector != null)
                            {
                                Habitat habitat3 = _Galaxy.FastFindNearestUnexploredHabitatInSector(Xpos, Ypos, ActualEmpire, Mission.TargetSector);
                                if (habitat3 == null)
                                {
                                    ClearPreviousMissionRequirements();
                                    FirstExecutionOfCommand = true;
                                    if (!AssignQueuedMission())
                                    {
                                        if (Empire.PirateEmpireBaseHabitat == null)
                                        {
                                            Empire.AssignMissionToBuiltObject(this, atWar: false, null);
                                        }
                                        else
                                        {
                                            Empire.PirateAssignShipMission(this, starDate);
                                        }
                                    }
                                }
                                else
                                {
                                    CommandQueue commandQueue = new CommandQueue();
                                    commandQueue.Enqueue(new Command(CommandAction.ClearParent));
                                    if (habitat3.Type == HabitatType.BlackHole)
                                    {
                                        double num22 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                                        double num23 = (double)habitat3.Diameter * 0.7 + Galaxy.Rnd.NextDouble() * 500.0;
                                        double x3 = habitat3.Xpos + num23 * Math.Sin(num22);
                                        double y3 = habitat3.Ypos + num23 * Math.Cos(num22);
                                        commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, x3, y3));
                                        commandQueue.Enqueue(new Command(CommandAction.MoveTo, x3, y3));
                                    }
                                    else
                                    {
                                        commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, habitat3));
                                        commandQueue.Enqueue(new Command(CommandAction.SetParent, habitat3));
                                        commandQueue.Enqueue(new Command(CommandAction.MoveTo, habitat3));
                                    }
                                    commandQueue.Enqueue(new Command(CommandAction.ScanArea));
                                    commandQueue.Enqueue(new Command(CommandAction.ClearParent));
                                    commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
                                    Mission.ReplaceCommandStack(commandQueue);
                                }
                            }
                            else if (Mission != null && Mission.Type == BuiltObjectMissionType.Explore && Mission.TargetHabitat != null && Mission.TargetHabitat.Category == HabitatCategoryType.Star)
                            {
                                Habitat targetHabitat2 = Mission.TargetHabitat;
                                bool flag11 = false;
                                if (Empire != null)
                                {
                                    GalaxyIndex galaxyIndex2 = _Galaxy.ResolveIndex(Xpos, Ypos);
                                    if (_Galaxy.BuiltObjectIndex[galaxyIndex2.X][galaxyIndex2.Y].Count > 0)
                                    {
                                        for (int k = 0; k < _Galaxy.BuiltObjectIndex[galaxyIndex2.X][galaxyIndex2.Y].Count; k++)
                                        {
                                            BuiltObject builtObject2 = _Galaxy.BuiltObjectIndex[galaxyIndex2.X][galaxyIndex2.Y][k];
                                            if (builtObject2 == null || builtObject2.SubRole != BuiltObjectSubRole.ExplorationShip || builtObject2.Empire != Empire || builtObject2 == this || builtObject2.Mission == null || builtObject2.Mission.Type != BuiltObjectMissionType.Explore || builtObject2.Mission.TargetHabitat == null)
                                            {
                                                continue;
                                            }
                                            Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(builtObject2.Mission.TargetHabitat);
                                            if (habitat4 != NearestSystemStar)
                                            {
                                                continue;
                                            }
                                            ClearPreviousMissionRequirements();
                                            FirstExecutionOfCommand = true;
                                            if (!AssignQueuedMission())
                                            {
                                                if (Empire.PirateEmpireBaseHabitat == null)
                                                {
                                                    Empire.AssignMissionToBuiltObject(this, atWar: false, null);
                                                }
                                                else
                                                {
                                                    Empire.PirateAssignShipMission(this, starDate);
                                                }
                                            }
                                            flag11 = true;
                                        }
                                    }
                                }
                                if (flag11)
                                {
                                    result = 0.0;
                                    break;
                                }
                                Habitat habitat5 = _Galaxy.FindNearestUnexploredHabitatInSystem((int)targetHabitat2.Xpos, (int)targetHabitat2.Ypos, targetHabitat2, ActualEmpire, includeAsteroids: true);
                                if (habitat5 != null)
                                {
                                    CommandQueue commandQueue2 = new CommandQueue();
                                    commandQueue2.Enqueue(new Command(CommandAction.ClearParent));
                                    if (habitat5.Type == HabitatType.BlackHole)
                                    {
                                        double num24 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                                        double num25 = (double)habitat5.Diameter * 0.7 + Galaxy.Rnd.NextDouble() * 500.0;
                                        double x4 = habitat5.Xpos + num25 * Math.Sin(num24);
                                        double y4 = habitat5.Ypos + num25 * Math.Cos(num24);
                                        commandQueue2.Enqueue(new Command(CommandAction.ConditionalHyperTo, x4, y4));
                                        commandQueue2.Enqueue(new Command(CommandAction.MoveTo, x4, y4));
                                    }
                                    else
                                    {
                                        commandQueue2.Enqueue(new Command(CommandAction.ConditionalHyperTo, habitat5));
                                        commandQueue2.Enqueue(new Command(CommandAction.SetParent, habitat5));
                                        commandQueue2.Enqueue(new Command(CommandAction.MoveTo, habitat5));
                                    }
                                    commandQueue2.Enqueue(new Command(CommandAction.ScanArea));
                                    commandQueue2.Enqueue(new Command(CommandAction.ClearParent));
                                    commandQueue2.Enqueue(new Command(CommandAction.ReassignMission));
                                    Mission.ReplaceCommandStack(commandQueue2);
                                }
                                else
                                {
                                    ClearPreviousMissionRequirements();
                                    FirstExecutionOfCommand = true;
                                    if (!AssignQueuedMission())
                                    {
                                        if (Empire.PirateEmpireBaseHabitat == null)
                                        {
                                            Empire.AssignMissionToBuiltObject(this, atWar: false, null);
                                        }
                                        else
                                        {
                                            Empire.PirateAssignShipMission(this, starDate);
                                        }
                                    }
                                }
                            }
                            else if (Mission != null && Mission.Type == BuiltObjectMissionType.Escort && !IsAutoControlled)
                            {
                                if (_SubsequentMissions != null && _SubsequentMissions.Count > 0)
                                {
                                    ClearPreviousMissionRequirements();
                                    FirstExecutionOfCommand = true;
                                    if (AssignQueuedMission())
                                    {
                                    }
                                }
                                else if (!AutoRefuelRepairShip(useCachedRefuellingLocation: true))
                                {
                                    bool flag12 = true;
                                    if (Mission.TargetBuiltObject != null && Mission.TargetBuiltObject.HasBeenDestroyed)
                                    {
                                        flag12 = false;
                                    }
                                    if (!flag12)
                                    {
                                        ClearPreviousMissionRequirements();
                                        FirstExecutionOfCommand = true;
                                        result = 0.0;
                                        break;
                                    }
                                    AssignMission(Mission.Type, Mission.Target, Mission.SecondaryTarget, Mission.Priority);
                                }
                            }
                            else if (Mission != null && Mission.Type == BuiltObjectMissionType.Patrol && !IsAutoControlled)
                            {
                                if (_SubsequentMissions != null && _SubsequentMissions.Count > 0)
                                {
                                    ClearPreviousMissionRequirements();
                                    FirstExecutionOfCommand = true;
                                    if (AssignQueuedMission())
                                    {
                                    }
                                }
                                else if (!AutoRefuelRepairShip(useCachedRefuellingLocation: true))
                                {
                                    bool flag13 = true;
                                    if (Mission.TargetBuiltObject != null && Mission.TargetBuiltObject.HasBeenDestroyed)
                                    {
                                        flag13 = false;
                                    }
                                    if (Mission.TargetHabitat != null && Mission.TargetHabitat.HasBeenDestroyed)
                                    {
                                        flag13 = false;
                                    }
                                    if (Mission.TargetCreature != null && Mission.TargetCreature.HasBeenDestroyed)
                                    {
                                        flag13 = false;
                                    }
                                    if (!flag13)
                                    {
                                        ClearPreviousMissionRequirements();
                                        FirstExecutionOfCommand = true;
                                        result = 0.0;
                                        break;
                                    }
                                    AssignMission(Mission.Type, Mission.Target, Mission.SecondaryTarget, Mission.Priority);
                                }
                            }
                            else if (_SubsequentMissions != null && _SubsequentMissions.Count > 0)
                            {
                                if (!AssignQueuedMission() && Empire != null)
                                {
                                    if (Empire.PirateEmpireBaseHabitat == null)
                                    {
                                        Empire.AssignMissionToBuiltObject(this, atWar: false, null);
                                    }
                                    else
                                    {
                                        Empire.PirateAssignShipMission(this, starDate);
                                    }
                                }
                            }
                            else if (!RevertToPreviousMission())
                            {
                                ClearPreviousMissionRequirements();
                                FirstExecutionOfCommand = true;
                                if (Empire != null)
                                {
                                    if (Empire.PirateEmpireBaseHabitat == null)
                                    {
                                        Empire.AssignMissionToBuiltObject(this, atWar: false, null);
                                    }
                                    else
                                    {
                                        Empire.PirateAssignShipMission(this, starDate);
                                    }
                                }
                            }
                        }
                        result = timePassed;
                        break;
                    case CommandAction.Refuel:
                        if (DockedAt != null)
                        {
                            if (DockedAt.IsRefuellingDepot)
                            {
                                int num31 = -1;
                                if (DockedAt.Cargo != null)
                                {
                                    num31 = BaconBuiltObject.GetCargoIndex(this);
                                }
                                if (num31 >= 0)
                                {
                                    bool flag16 = false;
                                    int num32 = 0;
                                    int refuelAmount = DockedAt.Cargo[num31].Available;
                                    if (_RefuelAmount > 0)
                                    {
                                        refuelAmount = DockedAt.Cargo[num31].Amount;
                                    }
                                    refuelAmount = BaconBuiltObject.CheckForNegativeRefueling(this, refuelAmount);
                                    num32 = Math.Max(1, (int)((double)Galaxy.RefuelRate * timePassed));
                                    if (refuelAmount < num32)
                                    {
                                        num32 = refuelAmount;
                                        flag16 = true;
                                    }
                                    double num33 = FuelCapacity - (int)CurrentFuel;
                                    if (num33 < (double)num32)
                                    {
                                        num32 = (int)num33;
                                        flag16 = true;
                                    }
                                    double num34 = 1.0;
                                    if (FuelType != null)
                                    {
                                        num34 = _Galaxy.ResourceCurrentPrices[FuelType.ResourceID];
                                    }
                                    int num35 = (int)((double)num32 * num34);
                                    DockedAt.Cargo[num31].Amount -= num32;
                                    if (_RefuelAmount > 0)
                                    {
                                        if (_RefuelLocationIsBuiltObject && DockedAt is BuiltObject)
                                        {
                                            BuiltObject builtObject4 = (BuiltObject)DockedAt;
                                            if (builtObject4 != null && builtObject4.BuiltObjectID == _RefuelLocationId && DockedAt.Cargo[num31].CommodityIsResource && DockedAt.Cargo[num31].Resource.ResourceID == _RefuelResourceId)
                                            {
                                                int num36 = Math.Min(DockedAt.Cargo[num31].Reserved, Math.Min(_RefuelAmount, num32));
                                                DockedAt.Cargo[num31].Reserved -= num36;
                                                _RefuelAmount -= (short)num36;
                                            }
                                        }
                                        else if (DockedAt is Habitat)
                                        {
                                            Habitat habitat7 = (Habitat)DockedAt;
                                            if (habitat7 != null && habitat7.HabitatIndex == _RefuelLocationId && DockedAt.Cargo[num31].CommodityIsResource && DockedAt.Cargo[num31].Resource.ResourceID == _RefuelResourceId)
                                            {
                                                int num37 = Math.Min(DockedAt.Cargo[num31].Reserved, Math.Min(_RefuelAmount, num32));
                                                DockedAt.Cargo[num31].Reserved -= num37;
                                                _RefuelAmount -= (short)num37;
                                            }
                                        }
                                    }
                                    CurrentFuel += num32;
                                    CurrentEnergy = Math.Max(CurrentEnergy, 0.0);
                                    double num38 = (double)num35 * 1.0;
                                    if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
                                    {
                                        if (Owner != null)
                                        {
                                            Owner.StateMoney -= num38;
                                            Owner.PirateEconomy.PerformExpense(num38, PirateExpenseType.Fuel, starDate);
                                            Owner.PurchaseStateFuel(num38);
                                        }
                                        else
                                        {
                                            Empire.PurchasePrivateFuel(num38);
                                        }
                                    }
                                    else if (Owner != null)
                                    {
                                        Owner.StateMoney -= num38;
                                        Owner.PurchaseStateFuel(num38);
                                    }
                                    else
                                    {
                                        Empire.PerformPrivateTransaction(0.0 - num38);
                                        Empire.PurchasePrivateFuel(num38);
                                    }
                                    DockedAt.Empire.PerformPrivateTransaction(num38);
                                    if (flag16)
                                    {
                                        CheckCancelRefuelData();
                                        result = Math.Max(0.0, ((double)Galaxy.RefuelRate * timePassed - (double)num32) / (double)Galaxy.RefuelRate);
                                        if (_FuelHandicapped)
                                        {
                                            ReDefine();
                                            _FuelHandicapped = false;
                                        }
                                        mission.CompleteCommand();
                                        FirstExecutionOfCommand = true;
                                    }
                                }
                                else
                                {
                                    CheckCancelRefuelData();
                                    mission.CompleteCommand();
                                    FirstExecutionOfCommand = true;
                                    result = timePassed;
                                }
                            }
                            else
                            {
                                CheckCancelRefuelData();
                                mission.CompleteCommand(ignoreRepeatCommands: true);
                                FirstExecutionOfCommand = true;
                                result = timePassed;
                            }
                        }
                        else
                        {
                            CheckCancelRefuelData();
                            mission.CompleteCommand(ignoreRepeatCommands: true);
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        break;
                    case CommandAction.Deploy:
                        if (ParentHabitat != null)
                        {
                            _DeployProgress = 0.01f;
                        }
                        mission.CompleteCommand();
                        FirstExecutionOfCommand = true;
                        result = timePassed;
                        break;
                    case CommandAction.RepeatSubsequentCommands:
                        mission.RepeatCommands = true;
                        mission.CompleteCommand(ignoreRepeatCommands: true);
                        FirstExecutionOfCommand = true;
                        result = timePassed;
                        break;
                    case CommandAction.SetParent:
                        if (command.TargetHabitat != null)
                        {
                            ParentHabitat = command.TargetHabitat;
                            ParentBuiltObject = null;
                            ParentOffsetX = Xpos - ParentHabitat.Xpos;
                            ParentOffsetY = Ypos - ParentHabitat.Ypos;
                        }
                        else if (command.TargetBuiltObject != null)
                        {
                            BuiltObject targetBuiltObject2 = command.TargetBuiltObject;
                            if (!targetBuiltObject2.HasBeenDestroyed)
                            {
                                ParentBuiltObject = targetBuiltObject2;
                                ParentHabitat = null;
                                ParentOffsetX = Xpos - ParentBuiltObject.Xpos;
                                ParentOffsetY = Ypos - ParentBuiltObject.Ypos;
                            }
                        }
                        mission.CompleteCommand();
                        FirstExecutionOfCommand = true;
                        result = timePassed;
                        break;
                    case CommandAction.Undeploy:
                        if (IsDeployed || DeployProgress < 0.0)
                        {
                            if (_DeployProgress == 0f)
                            {
                                InitiateUndeploy();
                            }
                            result = 0.0;
                        }
                        else
                        {
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        break;
                    case CommandAction.ClearParent:
                        if (DockedAt != null || BuiltAt != null)
                        {
                            if (DockedAt != null)
                            {
                                if (DockedAt.DockingBayWaitQueue != null)
                                {
                                    while (DockedAt.DockingBayWaitQueue.Contains(this))
                                    {
                                        DockedAt.DockingBayWaitQueue.Remove(this);
                                    }
                                }
                                if (DockedAt.DockingBays != null)
                                {
                                    for (int num3 = DockedAt.DockingBays.IndexOf(this); num3 >= 0; num3 = DockedAt.DockingBays.IndexOf(this))
                                    {
                                        DockedAt.DockingBays[num3].DockedShip = null;
                                    }
                                }
                            }
                            if (BuiltAt != null)
                            {
                                if (BuiltAt.DockingBayWaitQueue != null)
                                {
                                    while (BuiltAt.DockingBayWaitQueue.Contains(this))
                                    {
                                        BuiltAt.DockingBayWaitQueue.Remove(this);
                                    }
                                }
                                if (BuiltAt.DockingBays != null)
                                {
                                    for (int num4 = BuiltAt.DockingBays.IndexOf(this); num4 >= 0; num4 = BuiltAt.DockingBays.IndexOf(this))
                                    {
                                        BuiltAt.DockingBays[num4].DockedShip = null;
                                    }
                                }
                                if (BuiltAt.ConstructionQueue != null)
                                {
                                    if (BuiltAt.ConstructionQueue.ConstructionWaitQueue != null)
                                    {
                                        while (BuiltAt.ConstructionQueue.ConstructionWaitQueue.Contains(this))
                                        {
                                            BuiltAt.ConstructionQueue.ConstructionWaitQueue.Remove(this);
                                        }
                                    }
                                    if (BuiltAt.ConstructionQueue.ConstructionYards != null)
                                    {
                                        for (int num5 = BuiltAt.ConstructionQueue.ConstructionYards.IndexOf(this); num5 >= 0; num5 = BuiltAt.ConstructionQueue.ConstructionYards.IndexOf(this))
                                        {
                                            BuiltAt.ConstructionQueue.ConstructionYards[num5].ShipUnderConstruction = null;
                                            BuiltAt.ConstructionQueue.ConstructionYards[num5].IncrementalProgress = 0f;
                                        }
                                    }
                                }
                            }
                            DockedAt = null;
                            BuiltAt = null;
                        }
                        if (DeployProgress > 0.0)
                        {
                            InitiateUndeploy();
                        }
                        if (IsDeployed || DeployProgress < 0.0)
                        {
                            if (_DeployProgress == 0f)
                            {
                                InitiateUndeploy();
                            }
                            result = 0.0;
                        }
                        else
                        {
                            ParentHabitat = null;
                            ParentBuiltObject = null;
                            ParentOffsetX = -2000000001.0;
                            ParentOffsetY = -2000000001.0;
                            mission.CompleteCommand();
                            FirstExecutionOfCommand = true;
                            result = timePassed;
                        }
                        break;
                    case CommandAction.ClearAttackers:
                        Attackers.Clear();
                        mission.CompleteCommand();
                        FirstExecutionOfCommand = true;
                        result = timePassed;
                        break;
                }
            }
            else
            {
                if (mission != null && (mission.Type == BuiltObjectMissionType.Attack || mission.Type == BuiltObjectMissionType.WaitAndAttack || mission.Type == BuiltObjectMissionType.Bombard || mission.Type == BuiltObjectMissionType.WaitAndBombard || mission.Type == BuiltObjectMissionType.Capture || mission.Type == BuiltObjectMissionType.Raid || mission.PreviousType == BuiltObjectMissionType.Attack || mission.PreviousType == BuiltObjectMissionType.WaitAndAttack || mission.PreviousType == BuiltObjectMissionType.Bombard || mission.PreviousType == BuiltObjectMissionType.WaitAndBombard || mission.PreviousType == BuiltObjectMissionType.Capture || mission.PreviousType == BuiltObjectMissionType.Raid) && mission.Target != null && BattleStats != null)
                {
                    BaconSpaceBattleStats.AddLatestCombatStats(this, BattleStats);
                    bool nearby = true;
                    StellarObject attackedTarget = null;
                    if (mission.TargetBuiltObject != null)
                    {
                        attackedTarget = mission.TargetBuiltObject;
                    }
                    else if (mission.TargetHabitat != null)
                    {
                        attackedTarget = mission.TargetHabitat;
                    }
                    else if (mission.TargetCreature != null)
                    {
                        attackedTarget = mission.TargetCreature;
                    }
                    BattleStats.Location = _Galaxy.ResolveNearestLocation(attackedTarget, this, out nearby);
                    BattleStats.NearLocation = nearby;
                    _Galaxy.DoCharacterEvent(CharacterEventType.SpaceBattle, BattleStats, Characters);
                    BattleStats = null;
                }
                if (BattleStats != null)
                {
                    BaconSpaceBattleStats.AddLatestCombatStats(this, BattleStats);
                    bool nearby2 = true;
                    BattleStats.Location = _Galaxy.ResolveNearestLocation(null, this, out nearby2);
                    BattleStats.NearLocation = nearby2;
                    _Galaxy.DoCharacterEvent(CharacterEventType.SpaceBattle, BattleStats, Characters);
                }
                BattleStats = null;
                CheckCancelContracts();
                if (AssignQueuedMission())
                {
                    result = timePassed;
                }
                else if (RevertToPreviousMission())
                {
                    result = timePassed;
                }
                else
                {
                    if (!IsAutoControlled && Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null && Role != BuiltObjectRole.Base && ShipGroup == null && !_MissionCompleteMessageSent)
                    {
                        string description3 = string.Format(TextResolver.GetText("SHIPTYPE NAME has completed its mission"), Galaxy.ResolveDescription(SubRole), Name);
                        Empire.SendMessageToEmpire(Empire, EmpireMessageType.ShipMissionComplete, this, description3);
                        _MissionCompleteMessageSent = true;
                    }
                    if (_ShipPullAmountLocation <= 0f)
                    {
                        PreferredSpeed = 0f;
                        TargetSpeed = 0;
                    }
                    double num111 = (double)_tempNow.Subtract(_LastTouch).Ticks / 10000000.0;
                    AccelerateToTargetSpeed(num111);
                    if (Role != BuiltObjectRole.Base)
                    {
                        CalculateCurrentHeading(num111);
                    }
                    GalaxyIndex galaxyIndex3 = _Galaxy.ResolveIndex(Xpos, Ypos);
                    if (CurrentSpeed > 0f)
                    {
                        galaxyIndex3 = _Galaxy.ResolveIndex(Xpos, Ypos);
                        double num112 = (double)CurrentSpeed * num111;
                        Xpos += Math.Cos(Heading) * num112;
                        Ypos += Math.Sin(Heading) * num112;
                        if (num112 > 1000.0)
                        {
                            UpdateIndexesForMovement(galaxyIndex3.X, galaxyIndex3.Y, galaxy, performIndexCheck: true);
                        }
                        else
                        {
                            UpdateIndexesForMovement(galaxyIndex3.X, galaxyIndex3.Y, galaxy, performIndexCheck: false);
                        }
                    }
                }
            }
            if (InView)
            {
                result = 0.0;
            }
            return result;
        }

        public void RecordRevertMission(BuiltObjectMissionType newMissionType)
        {
            RecordRevertMission(newMissionType, evenWhenAutomated: false);
        }

        public void RecordRevertMission(BuiltObjectMissionType newMissionType, bool evenWhenAutomated)
        {
            if (!evenWhenAutomated && IsAutoControlled)
            {
                return;
            }
            BuiltObjectMission builtObjectMission = null;
            if (Mission == null)
            {
                return;
            }
            builtObjectMission = Mission.Clone();
            switch (newMissionType)
            {
                case BuiltObjectMissionType.Attack:
                case BuiltObjectMissionType.Escape:
                case BuiltObjectMissionType.Retrofit:
                case BuiltObjectMissionType.Refuel:
                case BuiltObjectMissionType.Repair:
                case BuiltObjectMissionType.Capture:
                    if (RevertMission == null)
                    {
                        if (builtObjectMission.Type == BuiltObjectMissionType.WaitAndAttack)
                        {
                            builtObjectMission = new BuiltObjectMission(_Galaxy, this, BuiltObjectMissionType.Attack, builtObjectMission.Target, null, BuiltObjectMissionPriority.High);
                        }
                        else if (builtObjectMission.Type == BuiltObjectMissionType.WaitAndBombard)
                        {
                            builtObjectMission = new BuiltObjectMission(_Galaxy, this, BuiltObjectMissionType.Bombard, builtObjectMission.Target, null, BuiltObjectMissionPriority.High);
                        }
                        RevertMission = builtObjectMission;
                    }
                    break;
            }
        }

        private bool RevertToPreviousMission()
        {
            if (AutoRefuelRepairShip(useCachedRefuellingLocation: true))
            {
                return true;
            }
            BuiltObjectMission revertMission = RevertMission;
            if (revertMission != null)
            {
                if (revertMission.Type == BuiltObjectMissionType.Explore || revertMission.Target != null || revertMission.SecondaryTarget != null || !(revertMission.X <= -2.00000013E+09f) || !(revertMission.Y <= -2.00000013E+09f))
                {
                    bool flag = true;
                    if (revertMission.Type == BuiltObjectMissionType.Explore)
                    {
                        if (revertMission.TargetHabitat != null && Empire != null && Empire.ResourceMap != null && Empire.ResourceMap.CheckResourcesKnown(revertMission.TargetHabitat))
                        {
                            flag = false;
                        }
                    }
                    else if (revertMission.Type == BuiltObjectMissionType.Build && revertMission.TargetHabitat != null && Empire != null && revertMission.Design != null)
                    {
                        switch (revertMission.Design.SubRole)
                        {
                            case BuiltObjectSubRole.GasMiningStation:
                            case BuiltObjectSubRole.MiningStation:
                                {
                                    HabitatList habitatList4 = Empire.DetermineHabitatsBeingMinedIncludingBuildingMiningStations(includeMiningShips: false);
                                    if (habitatList4.Contains(revertMission.TargetHabitat))
                                    {
                                        flag = false;
                                    }
                                    break;
                                }
                            case BuiltObjectSubRole.EnergyResearchStation:
                            case BuiltObjectSubRole.WeaponsResearchStation:
                            case BuiltObjectSubRole.HighTechResearchStation:
                                {
                                    HabitatList habitatList2 = Empire.DetermineHabitatsWithBasesIncludingBuilding(new List<BuiltObjectSubRole>
                        {
                            BuiltObjectSubRole.EnergyResearchStation,
                            BuiltObjectSubRole.HighTechResearchStation,
                            BuiltObjectSubRole.WeaponsResearchStation
                        });
                                    if (habitatList2.Contains(revertMission.TargetHabitat))
                                    {
                                        flag = false;
                                    }
                                    break;
                                }
                            case BuiltObjectSubRole.MonitoringStation:
                                {
                                    HabitatList habitatList3 = Empire.DetermineHabitatsWithBasesIncludingBuilding(new List<BuiltObjectSubRole> { BuiltObjectSubRole.MonitoringStation });
                                    if (habitatList3.Contains(revertMission.TargetHabitat))
                                    {
                                        flag = false;
                                    }
                                    break;
                                }
                            case BuiltObjectSubRole.ResortBase:
                                {
                                    HabitatList habitatList = Empire.DetermineHabitatsWithBasesIncludingBuilding(new List<BuiltObjectSubRole> { BuiltObjectSubRole.ResortBase });
                                    if (habitatList.Contains(revertMission.TargetHabitat))
                                    {
                                        flag = false;
                                    }
                                    break;
                                }
                        }
                    }
                    if (flag)
                    {
                        AssignMission(revertMission.Type, revertMission.Target, revertMission.SecondaryTarget, revertMission.Cargo, revertMission.Troops, revertMission.Population, revertMission.Design, revertMission.X, revertMission.Y, revertMission.StarDate, revertMission.Priority, allowReprocessing: false);
                        if (Mission != null && revertMission.TargetSector != null)
                        {
                            Mission.SetTargetSector(revertMission.TargetSector);
                        }
                    }
                }
                RevertMission = null;
                return true;
            }
            return false;
        }

        private bool AutoRefuelRepairShip(bool useCachedRefuellingLocation)
        {
            if (ShipGroup == null && Role != BuiltObjectRole.Base && Owner != null && Owner.AutoRefuelStateShips)
            {
                if (Mission != null && Mission.Type == BuiltObjectMissionType.Colonize)
                {
                    return false;
                }
                BuiltObjectMission builtObjectMission = null;
                if (Mission != null && Mission.Type != 0)
                {
                    builtObjectMission = Mission.Clone();
                    if (builtObjectMission.Type == BuiltObjectMissionType.Undefined)
                    {
                        builtObjectMission.Type = Mission.PreviousType;
                    }
                }
                Empire actualEmpire = ActualEmpire;
                if (DamagedComponentCount > 0 && actualEmpire != null)
                {
                    if (DockedAt == null && BuiltAt == null && (Mission == null || Mission.Type != BuiltObjectMissionType.Repair))
                    {
                        if (actualEmpire.AssignRepairMission(this))
                        {
                            if (builtObjectMission != null)
                            {
                                switch (builtObjectMission.Type)
                                {
                                    default:
                                        if (RevertMission == null)
                                        {
                                            RevertMission = builtObjectMission;
                                        }
                                        break;
                                    case BuiltObjectMissionType.Undefined:
                                    case BuiltObjectMissionType.Build:
                                    case BuiltObjectMissionType.BuildRepair:
                                    case BuiltObjectMissionType.Transport:
                                    case BuiltObjectMissionType.Escape:
                                    case BuiltObjectMissionType.Retrofit:
                                    case BuiltObjectMissionType.Hold:
                                    case BuiltObjectMissionType.Refuel:
                                    case BuiltObjectMissionType.LoadTroops:
                                    case BuiltObjectMissionType.UnloadTroops:
                                    case BuiltObjectMissionType.Undeploy:
                                    case BuiltObjectMissionType.Repair:
                                        break;
                                }
                            }
                            return true;
                        }
                        RevertMission = null;
                    }
                }
                else
                {
                    if (Mission != null)
                    {
                        if (Mission.Type == BuiltObjectMissionType.Build || Mission.Type == BuiltObjectMissionType.BuildRepair || Mission.Type == BuiltObjectMissionType.Colonize || Mission.Type == BuiltObjectMissionType.Deploy || Mission.Type == BuiltObjectMissionType.Escape || Mission.Type == BuiltObjectMissionType.ExtractResources || Mission.Type == BuiltObjectMissionType.LoadTroops || Mission.Type == BuiltObjectMissionType.Refuel || Mission.Type == BuiltObjectMissionType.Repair || Mission.Type == BuiltObjectMissionType.Retire || Mission.Type == BuiltObjectMissionType.Retrofit || Mission.Type == BuiltObjectMissionType.Transport || Mission.Type == BuiltObjectMissionType.UnloadTroops)
                        {
                            return false;
                        }
                        if (Mission.Type == BuiltObjectMissionType.Attack && _ColonyToAttack != null && Troops != null && Troops.Count > 0)
                        {
                            return false;
                        }
                    }
                    double num = 0.0;
                    num = ((!useCachedRefuellingLocation || _RefuellingLocation == null || _RefuellingLocation.HasBeenDestroyed) ? CalculateRefuellingPortion() : CalculateRefuellingPortion(_RefuellingLocation));
                    if (ShipGroup == null && ((SubRole == BuiltObjectSubRole.ResupplyShip && IsDeployed) || DeployProgress != 0.0))
                    {
                        num = 0.0;
                    }
                    double num2 = (double)FuelCapacity * num;
                    if (CurrentFuel <= num2 && DockedAt == null && BuiltAt == null)
                    {
                        SetupRefuelling();
                        if (Mission != null && Mission.Type == BuiltObjectMissionType.Refuel)
                        {
                            if (builtObjectMission != null)
                            {
                                switch (builtObjectMission.Type)
                                {
                                    default:
                                        if (RevertMission == null)
                                        {
                                            RevertMission = builtObjectMission;
                                        }
                                        break;
                                    case BuiltObjectMissionType.Undefined:
                                    case BuiltObjectMissionType.Transport:
                                    case BuiltObjectMissionType.Escape:
                                    case BuiltObjectMissionType.Retrofit:
                                    case BuiltObjectMissionType.Hold:
                                    case BuiltObjectMissionType.Refuel:
                                    case BuiltObjectMissionType.LoadTroops:
                                    case BuiltObjectMissionType.UnloadTroops:
                                    case BuiltObjectMissionType.Undeploy:
                                    case BuiltObjectMissionType.Repair:
                                        break;
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void CheckSelfDestruct()
        {
            if (DamagedComponentCount > 0 && Empire != null && DockedAt == null && BuiltAt == null && (Mission == null || Mission.Type != BuiltObjectMissionType.Repair) && _Galaxy.DetermineScrapDamagedShip(this))
            {
                double hitPower = Math.Max(CurrentShields + (float)Size + 1f, 1000.0);
                InflictDamage(this, null, hitPower, _Galaxy.CurrentDateTime, _Galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
            }
        }

        private void ProcessTourists(Population tourists)
        {
            if (DockedAt == null || DockedAt.Owner == null)
            {
                return;
            }
            if (DockedAt is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)DockedAt;
                if (builtObject.SubRole != BuiltObjectSubRole.ResortBase)
                {
                    return;
                }
                double num = (double)tourists.Amount / 8.0;
                if (builtObject.Empire != null)
                {
                    if (builtObject.Empire.Leader != null)
                    {
                        num *= 1.0 + (double)builtObject.Empire.Leader.TourismIncome / 100.0;
                    }
                    if (builtObject.Characters != null && builtObject.Characters.Count > 0)
                    {
                        int highestSkillLevel = builtObject.Characters.GetHighestSkillLevel(CharacterSkillType.TourismIncome);
                        num *= 1.0 + (double)highestSkillLevel / 100.0;
                    }
                    if (builtObject.Empire.DominantRace != null)
                    {
                        num *= builtObject.Empire.DominantRace.TourismIncomeFactor;
                    }
                    builtObject.Empire.Counters.ProcessTourismIncome(num);
                    builtObject.Empire.AddResortIncome(num);
                    CharacterList ambassadorsForEmpire = builtObject.Empire.Characters.GetAmbassadorsForEmpire(Empire);
                    ambassadorsForEmpire.AddRange(builtObject.Characters);
                    _Galaxy.DoCharacterEvent(CharacterEventType.TourismIncome, null, ambassadorsForEmpire, includeLeader: true, builtObject.Empire);
                }
                if (PirateEmpireId > 0)
                {
                    Empire byEmpireId = _Galaxy.PirateEmpires.GetByEmpireId(PirateEmpireId);
                    if (byEmpireId != null)
                    {
                        double num2 = 1.0;
                        if (Characters != null && Characters.Count > 0)
                        {
                            num2 += 0.01 * (double)Characters.GetHighestSkillLevel(CharacterSkillType.SmugglingIncome);
                        }
                        double num3 = num * 0.25 * byEmpireId.ColonyIncomeFactor * byEmpireId.SmugglingIncomeFactor;
                        num3 *= num2;
                        num3 = byEmpireId.ApplyCorruptionToIncome(num3);
                        byEmpireId.StateMoney += num3;
                        byEmpireId.PirateEconomy.PerformIncome(num3, PirateIncomeType.Smuggling, _Galaxy.CurrentStarDate);
                        byEmpireId.Counters.PirateSmugglingIncome += num3;
                    }
                }
                num = builtObject.Owner.ApplyCorruptionToIncome(num);
                builtObject.Owner.StateMoney += num;
                builtObject.Owner.PirateEconomy.PerformIncome(num, PirateIncomeType.Resort, _Galaxy.CurrentStarDate);
            }
            else
            {
                if (!(DockedAt is Habitat))
                {
                    return;
                }
                Habitat habitat = (Habitat)DockedAt;
                double num4 = habitat.CalculateScenicFactorIncludingRuinsWonders();
                if (!(num4 > 0.0) || tourists.Amount >= 1000000)
                {
                    return;
                }
                double num5 = (double)tourists.Amount / 8.0;
                if (habitat.Empire != null)
                {
                    if (habitat.Empire.Leader != null)
                    {
                        num5 *= 1.0 + (double)habitat.Empire.Leader.TourismIncome / 100.0;
                    }
                    if (habitat.Characters != null && habitat.Characters.Count > 0)
                    {
                        int highestSkillLevelExcludeLeaders = habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TourismIncome);
                        num5 *= 1.0 + (double)highestSkillLevelExcludeLeaders / 100.0;
                    }
                    if (habitat.Empire.DominantRace != null)
                    {
                        num5 *= habitat.Empire.DominantRace.TourismIncomeFactor;
                    }
                    habitat.Empire.Counters.ProcessTourismIncome(num5);
                    habitat.Empire.AddResortIncome(num5);
                    CharacterList ambassadorsForEmpire2 = habitat.Empire.Characters.GetAmbassadorsForEmpire(Empire);
                    ambassadorsForEmpire2.AddRange(habitat.Characters);
                    _Galaxy.DoCharacterEvent(CharacterEventType.TourismIncome, null, ambassadorsForEmpire2, includeLeader: true, habitat.Empire);
                }
                if (PirateEmpireId > 0)
                {
                    Empire byEmpireId2 = _Galaxy.PirateEmpires.GetByEmpireId(PirateEmpireId);
                    if (byEmpireId2 != null)
                    {
                        double num6 = 1.0;
                        if (Characters != null && Characters.Count > 0)
                        {
                            num6 += 0.01 * (double)Characters.GetHighestSkillLevel(CharacterSkillType.SmugglingIncome);
                        }
                        double num7 = num5 * 0.25 * byEmpireId2.ColonyIncomeFactor * byEmpireId2.SmugglingIncomeFactor;
                        num7 *= num6;
                        num7 = byEmpireId2.ApplyCorruptionToIncome(num7);
                        byEmpireId2.StateMoney += num7;
                        byEmpireId2.PirateEconomy.PerformIncome(num7, PirateIncomeType.Smuggling, _Galaxy.CurrentStarDate);
                        byEmpireId2.Counters.PirateSmugglingIncome += num7;
                    }
                }
                num5 = habitat.Owner.ApplyCorruptionToIncome(num5);
                habitat.Owner.StateMoney += num5;
                habitat.Owner.PirateEconomy.PerformIncome(num5, PirateIncomeType.Resort, _Galaxy.CurrentStarDate);
            }
        }

        private bool AssignQueuedMission()
        {
            return AssignQueuedMission(allowReprocessing: true);
        }

        private bool AssignQueuedMission(bool allowReprocessing)
        {
            return BaconBuiltObject.AssignQueuedMission(this, allowReprocessing);
        }

        private void ProvideBonusFromPirateBase(Empire destroyingEmpire, BuiltObject pirateBase)
        {
            if (destroyingEmpire == null || pirateBase.Role != BuiltObjectRole.Base || pirateBase.Empire == null || pirateBase.Empire.PirateEmpireBaseHabitat == null)
            {
                return;
            }
            if (pirateBase.SubRole == BuiltObjectSubRole.GenericBase && pirateBase.ParentHabitat != null && pirateBase.ParentHabitat == pirateBase.Empire.PirateEmpireBaseHabitat && pirateBase.Empire.PirateEmpireSuperPirates && destroyingEmpire.PirateEmpireBaseHabitat == null)
            {
                string text = string.Format(TextResolver.GetText("Phantom Pirate Base Destroyed"), pirateBase.Name, pirateBase.Empire.Name);
                text = text + "\n\n" + string.Format(TextResolver.GetText("Destroyed Ship Acquire Tech Multiple"), TextResolver.GetText("Base").ToLower(CultureInfo.InvariantCulture));
                text += " ";
                for (int i = 0; i < 5; i++)
                {
                    ResearchNode researchNode = Empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(_Galaxy);
                    if (researchNode != null)
                    {
                        Empire.DoResearchBreakthrough(researchNode, selfResearched: true, blockMessages: true, suppressUpdate: false);
                        text = text + researchNode.Name + ", ";
                    }
                }
                Empire.Research.Update(Empire.DominantRace);
                Empire.ReviewDesignsBuiltObjectsImprovedComponents();
                Empire.ReviewResearchAbilities();
                text = text.Substring(0, text.Length - 2);
                text = text + "\n\n" + string.Format(TextResolver.GetText("Great Victory"), pirateBase.Empire.Name);
                string title = string.Format(TextResolver.GetText("TARGET Destroyed"), pirateBase.Name) + "!";
                destroyingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, title, text, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                destroyingEmpire.DefeatedLegendaryPiratesCount++;
            }
            else
            {
                if (Galaxy.Rnd.Next(0, 5) <= 1)
                {
                    return;
                }
                if (destroyingEmpire.PirateEmpireBaseHabitat == null && destroyingEmpire.CheckEmpireHasHyperDriveTech(destroyingEmpire))
                {
                    string empty = string.Empty;
                    string empty2 = string.Empty;
                    string empty3 = string.Empty;
                    switch (Galaxy.Rnd.Next(0, 4))
                    {
                        case 0:
                            {
                                Habitat habitat3 = _Galaxy.FindLonelyColonyLocation(destroyingEmpire);
                                Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat3);
                                int num = Galaxy.Rnd.Next(0, 4);
                                DesignSpecification designSpecification = null;
                                Design design = null;
                                switch (num)
                                {
                                    case 0:
                                        designSpecification = destroyingEmpire.ObtainDesignSpec(BuiltObjectSubRole.Destroyer);
                                        design = destroyingEmpire.GenerateDesignFromSpec(designSpecification, 4.0);
                                        break;
                                    case 1:
                                        designSpecification = destroyingEmpire.ObtainDesignSpec(BuiltObjectSubRole.Cruiser);
                                        design = destroyingEmpire.GenerateDesignFromSpec(designSpecification, 4.0);
                                        break;
                                    case 2:
                                        designSpecification = destroyingEmpire.GetMonitoringStationDesignSpec();
                                        design = destroyingEmpire.GenerateDesignFromSpec(designSpecification, 4.0);
                                        break;
                                    case 3:
                                        designSpecification = destroyingEmpire.ObtainDesignSpec(BuiltObjectSubRole.CapitalShip);
                                        design = destroyingEmpire.GenerateDesignFromSpec(designSpecification, 4.0);
                                        break;
                                }
                                if (design != null && habitat3 != null && habitat4 != null)
                                {
                                    design.PictureRef = ShipImageHelper.ResolveMinorShipImageIndex(design.SubRole, largeShips: true);
                                    BuiltObject builtObject = _Galaxy.GenerateAbandonedBuiltObject(habitat3, design);
                                    empty3 = _Galaxy.ResolveSectorDescription(builtObject.Xpos, builtObject.Ypos);
                                    empty2 = string.Format(TextResolver.GetText("Pirate Base Bonus Abandoned Ship"), pirateBase.Name, Galaxy.ResolveDescription(builtObject.SubRole).ToLower(CultureInfo.InvariantCulture), builtObject.Name, habitat4.Name, empty3);
                                    empty = TextResolver.GetText("Lost Ship Location Revealed");
                                    if (destroyingEmpire == _Galaxy.PlayerEmpire)
                                    {
                                        Point location2 = new Point((int)builtObject.Xpos, (int)builtObject.Ypos);
                                        _Galaxy.PlayerEmpire.AddLocationHint(location2);
                                    }
                                    destroyingEmpire.SendEventMessageToEmpire(EventMessageType.LostBuiltObjectCoordinates, empty, empty2, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                                }
                                break;
                            }
                        case 1:
                            {
                                double num5 = 2000.0 + Galaxy.Rnd.NextDouble() * 6000.0;
                                num5 *= Empire.ColonyIncomeFactor;
                                num5 *= Empire.LootingFactor;
                                num5 = destroyingEmpire.ApplyCorruptionToIncome(num5);
                                destroyingEmpire.StateMoney += num5;
                                destroyingEmpire.PirateEconomy.PerformIncome(num5, PirateIncomeType.Looting, _Galaxy.CurrentStarDate);
                                empty2 = string.Format(TextResolver.GetText("Pirate Base Bonus Money"), pirateBase.Name, num5.ToString("#0"));
                                empty = TextResolver.GetText("Valuable Treasure Discovered");
                                destroyingEmpire.SendEventMessageToEmpire(EventMessageType.TreasureFound, empty, empty2, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                                break;
                            }
                        case 2:
                            {
                                if (pirateBase.Empire == null || Empire == null)
                                {
                                    break;
                                }
                                bool flag = false;
                                switch (pirateBase.SubRole)
                                {
                                    case BuiltObjectSubRole.SmallSpacePort:
                                    case BuiltObjectSubRole.MediumSpacePort:
                                    case BuiltObjectSubRole.LargeSpacePort:
                                        flag = true;
                                        break;
                                }
                                if (flag)
                                {
                                    Empire empire = pirateBase.Empire;
                                    int num2 = Math.Max(1, Empire.BuiltObjects.TotalMobileMilitaryFirepower());
                                    int num3 = Math.Max(1, empire.BuiltObjects.TotalMobileMilitaryFirepower());
                                    double num4 = (double)num2 / (double)num3;
                                    if (num4 > 2.0 && num3 < 400 && empire.SpacePorts.Count <= 1 && empire != null && !empire.PirateEmpireSuperPirates && empire != _Galaxy.PlayerEmpire)
                                    {
                                        _Galaxy.PirateFactionJoinsEmpire(destroyingEmpire, empire);
                                        empty2 = string.Format(TextResolver.GetText("Pirate Base Bonus Targeted Faction Joins"), pirateBase.Name, empire.Name);
                                        empty = TextResolver.GetText("Pirate Faction Joins Your Empire");
                                        destroyingEmpire.SendEventMessageToEmpire(EventMessageType.PirateFactionJoinsYou, empty, empty2, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                Habitat habitat = _Galaxy.FastFindNearestIndependentHabitat(pirateBase.Xpos, pirateBase.Ypos);
                                SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Undefined;
                                if (habitat != null)
                                {
                                    systemVisibilityStatus = destroyingEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                                }
                                if (systemVisibilityStatus != SystemVisibilityStatus.Unexplored)
                                {
                                    break;
                                }
                                Race race = null;
                                if (habitat.Population != null && habitat.Population.Count > 0 && habitat.Population.DominantRace != null)
                                {
                                    race = habitat.Population.DominantRace;
                                }
                                if (race != null)
                                {
                                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                                    empty3 = _Galaxy.ResolveSectorDescription(habitat.Xpos, habitat.Ypos);
                                    empty2 = string.Format(TextResolver.GetText("Pirate Base Bonus Exploration"), pirateBase.Name, race.Name, habitat2.Name, empty3);
                                    empty = string.Format(TextResolver.GetText("Independent Colony of RACE"), race.Name);
                                    if (destroyingEmpire == _Galaxy.PlayerEmpire)
                                    {
                                        Point location = new Point((int)habitat.Xpos, (int)habitat.Ypos);
                                        _Galaxy.PlayerEmpire.AddLocationHint(location);
                                    }
                                    destroyingEmpire.SendEventMessageToEmpire(EventMessageType.IndependentPopulation, empty, empty2, race, pirateBase.Empire.PirateEmpireBaseHabitat);
                                }
                                break;
                            }
                    }
                }
                else
                {
                    if (pirateBase.SubRole != BuiltObjectSubRole.SmallSpacePort && pirateBase.SubRole != BuiltObjectSubRole.MediumSpacePort && pirateBase.SubRole != BuiltObjectSubRole.LargeSpacePort)
                    {
                        return;
                    }
                    Empire empire2 = pirateBase.Empire;
                    if (empire2 == null || empire2.BuiltObjects == null)
                    {
                        return;
                    }
                    int num6 = Math.Max(1, Empire.BuiltObjects.TotalMobileMilitaryFirepower());
                    int num7 = Math.Max(1, empire2.BuiltObjects.TotalMobileMilitaryFirepower());
                    double num8 = (double)num6 / (double)num7;
                    bool flag2 = true;
                    if (empire2.SpacePorts != null && empire2.SpacePorts.Count > 0)
                    {
                        for (int j = 0; j < empire2.SpacePorts.Count; j++)
                        {
                            BuiltObject builtObject2 = empire2.SpacePorts[j];
                            if (builtObject2 != null && builtObject2 != pirateBase)
                            {
                                flag2 = false;
                            }
                        }
                    }
                    if (flag2 && num8 > 2.0 && num7 < 200 && !empire2.PirateEmpireSuperPirates && empire2 != destroyingEmpire && empire2 != _Galaxy.PlayerEmpire)
                    {
                        string message = string.Format(TextResolver.GetText("Pirate Base Destroyed Faction Joins"), pirateBase.Name, empire2.Name);
                        _Galaxy.EliminatePirateFaction(empire2, destroyingEmpire);
                        string text2 = TextResolver.GetText("Pirate Faction Joins Your Empire");
                        destroyingEmpire.SendEventMessageToEmpire(EventMessageType.PirateFactionJoinsYou, text2, message, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                    }
                }
            }
        }

        public void CheckCancelContracts()
        {
            if (_ContractsToFulfill == null || _ContractsToFulfill.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < _ContractsToFulfill.Count; i++)
            {
                Contract contract = _ContractsToFulfill[i];
                if (contract != null)
                {
                    _Galaxy.CancelContract(contract);
                }
            }
            _ContractsToFulfill.Clear();
        }

        public void CompleteTeardown(Galaxy galaxy)
        {
            CompleteTeardown(galaxy, removeFromEmpire: true);
        }

        public void CompleteTeardown(Galaxy galaxy, bool removeFromEmpire)
        {
            HasBeenDestroyed = true;
            if (_ContractsToFulfill.Count > 0)
            {
                CheckCancelContracts();
            }
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            if (orders.Count > 0)
            {
                lock (_Galaxy.Orders._LockObject)
                {
                    for (int i = 0; i < orders.Count; i++)
                    {
                        Order order = orders[i];
                        _Galaxy.Orders.Remove(order);
                    }
                }
            }
            int num = 0;
            for (int j = 0; j < galaxy.Empires.Count; j++)
            {
                if (galaxy.Empires[j].Outlaws != null)
                {
                    num = galaxy.Empires[j].Outlaws.IndexOf(this);
                    if (num >= 0)
                    {
                        galaxy.Empires[j].Outlaws.RemoveAt(num);
                    }
                }
            }
            if (Troops != null && Troops.Count > 0)
            {
                for (int k = 0; k < Troops.Count; k++)
                {
                    Troop troop = Troops[k];
                    if (Empire != null && Empire.Troops != null)
                    {
                        Empire.Troops.Remove(troop);
                    }
                    troop.Empire = null;
                    troop.Colony = null;
                    troop.BuiltObject = null;
                }
                Troops.Clear();
            }
            if (Characters != null && Characters.Count > 0)
            {
                Character[] array = ListHelper.ToArrayThreadSafe(Characters);
                foreach (Character character in array)
                {
                    if (Role == BuiltObjectRole.Base)
                    {
                        character.SendDeathMessage(CharacterDeathType.BaseDestroyed, _Galaxy);
                    }
                    else
                    {
                        character.SendDeathMessage(CharacterDeathType.ShipDestroyed, _Galaxy);
                    }
                    character.Kill(_Galaxy);
                }
            }
            if (CurrentTarget != null)
            {
                num = CurrentTarget.Attackers.IndexOf(this);
                if (num >= 0)
                {
                    CurrentTarget.Attackers.RemoveAt(num);
                }
                num = CurrentTarget.Pursuers.IndexOf(this);
                if (num >= 0)
                {
                    CurrentTarget.Pursuers.RemoveAt(num);
                }
            }
            if (ShipGroup != null)
            {
                LeaveShipGroup();
            }
            if (DockingBayWaitQueue != null)
            {
                for (int m = 0; m < DockingBayWaitQueue.Count; m++)
                {
                    if (DockingBayWaitQueue[m] != null)
                    {
                        DockingBayWaitQueue[m].DockedAt = null;
                    }
                }
                DockingBayWaitQueue.Clear();
            }
            if (DockingBays != null)
            {
                for (int n = 0; n < DockingBays.Count; n++)
                {
                    DockingBay dockingBay = DockingBays[n];
                    if (dockingBay.DockedShip != null)
                    {
                        dockingBay.DockedShip.DockedAt = null;
                    }
                    dockingBay.DockedShip = null;
                }
            }
            if (ConstructionQueue != null)
            {
                for (int num2 = 0; num2 < ConstructionQueue.ConstructionYards.Count; num2++)
                {
                    ConstructionYard constructionYard = ConstructionQueue.ConstructionYards[num2];
                    if (constructionYard.ShipUnderConstruction != null)
                    {
                        constructionYard.ShipUnderConstruction.BuiltAt = null;
                    }
                    constructionYard.ShipUnderConstruction = null;
                }
                for (int num3 = 0; num3 < ConstructionQueue.ConstructionWaitQueue.Count; num3++)
                {
                    BuiltObject builtObject = ConstructionQueue.ConstructionWaitQueue[num3];
                    builtObject.BuiltAt = null;
                }
                ConstructionQueue.ConstructionWaitQueue.Clear();
            }
            if (ParentHabitat != null)
            {
                if (ParentHabitat.ConstructionQueue != null)
                {
                    for (int num4 = 0; num4 < ParentHabitat.ConstructionQueue.ConstructionYards.Count; num4++)
                    {
                        ConstructionYard constructionYard2 = ParentHabitat.ConstructionQueue.ConstructionYards[num4];
                        if (constructionYard2.ShipUnderConstruction == this)
                        {
                            constructionYard2.ShipUnderConstruction = null;
                            break;
                        }
                    }
                    while (ParentHabitat.ConstructionQueue.ConstructionWaitQueue.Contains(this))
                    {
                        ParentHabitat.ConstructionQueue.ConstructionWaitQueue.Remove(this);
                    }
                }
                if (ParentHabitat.BasesAtHabitat.Contains(this))
                {
                    ParentHabitat.BasesAtHabitat.Remove(this);
                }
                ParentHabitat = null;
            }
            if (DockedAt != null && DockedAt.DockingBays != null && DockedAt.DockingBayWaitQueue != null)
            {
                for (int num5 = 0; num5 < DockedAt.DockingBays.Count; num5++)
                {
                    DockingBay dockingBay2 = DockedAt.DockingBays[num5];
                    if (dockingBay2.DockedShip == this)
                    {
                        dockingBay2.DockedShip = null;
                    }
                }
                if (DockedAt.DockingBayWaitQueue.Contains(this))
                {
                    DockedAt.DockingBayWaitQueue.Remove(this);
                }
            }
            DockedAt = null;
            if (Mission != null)
            {
                Command command = Mission.FastPeekCurrentCommand();
                if (command != null && command.Action == CommandAction.Dock)
                {
                    if (command.TargetBuiltObject != null)
                    {
                        if (command.TargetBuiltObject.DockingBayWaitQueue != null && command.TargetBuiltObject.DockingBayWaitQueue.Contains(this))
                        {
                            command.TargetBuiltObject.DockingBayWaitQueue.Remove(this);
                        }
                    }
                    else if (command.TargetHabitat != null && command.TargetHabitat.DockingBayWaitQueue != null && command.TargetHabitat.DockingBayWaitQueue.Contains(this))
                    {
                        command.TargetHabitat.DockingBayWaitQueue.Remove(this);
                    }
                }
            }
            if (BuiltAt != null && BuiltAt.ConstructionQueue != null)
            {
                for (int num6 = 0; num6 < BuiltAt.ConstructionQueue.ConstructionYards.Count; num6++)
                {
                    ConstructionYard constructionYard3 = BuiltAt.ConstructionQueue.ConstructionYards[num6];
                    if (constructionYard3.ShipUnderConstruction == this)
                    {
                        constructionYard3.ShipUnderConstruction = null;
                    }
                }
                if (BuiltAt.ConstructionQueue.ConstructionWaitQueue.Contains(this))
                {
                    BuiltAt.ConstructionQueue.ConstructionWaitQueue.Remove(this);
                }
                BuiltAt = null;
            }
            if (Fighters != null && Fighters.Count > 0)
            {
                DateTime currentDateTime = galaxy.CurrentDateTime;
                for (int num7 = 0; num7 < Fighters.Count; num7++)
                {
                    Fighter abstractTarget = Fighters[num7];
                    InflictDamage(abstractTarget, null, 1000.0, currentDateTime, galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                }
            }
            for (int num8 = 0; num8 < _Galaxy.Empires.Count; num8++)
            {
                Empire empire = _Galaxy.Empires[num8];
                if (empire.ShipGroups == null)
                {
                    continue;
                }
                for (int num9 = 0; num9 < empire.ShipGroups.Count; num9++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[num9];
                    if (shipGroup.Mission != null)
                    {
                        if (shipGroup.Mission != null && shipGroup.Mission.TargetBuiltObject != null && shipGroup.Mission != null && shipGroup.Mission.TargetBuiltObject == this)
                        {
                            shipGroup.ForceCompleteMission();
                        }
                        if (shipGroup.Mission != null && shipGroup.Mission.SecondaryTargetBuiltObject != null && shipGroup.Mission != null && shipGroup.Mission.SecondaryTargetBuiltObject == this)
                        {
                            shipGroup.ForceCompleteMission();
                        }
                    }
                }
            }
            Empire actualEmpire = ActualEmpire;
            if (actualEmpire != null)
            {
                if (Role == BuiltObjectRole.Base && actualEmpire.PirateEmpireBaseHabitat != null)
                {
                    for (int num10 = 0; num10 < _Galaxy.Empires.Count; num10++)
                    {
                        if (_Galaxy.Empires[num10] != null && _Galaxy.Empires[num10].KnownPirateBases != null && _Galaxy.Empires[num10].KnownPirateBases.Contains(this))
                        {
                            _Galaxy.Empires[num10].KnownPirateBases.Remove(this);
                        }
                    }
                }
                num = actualEmpire.Manufacturers.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.Manufacturers.RemoveAt(num);
                }
                num = actualEmpire.RefuellingDepots.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.RefuellingDepots.RemoveAt(num);
                }
                num = actualEmpire.ResourceExtractors.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ResourceExtractors.RemoveAt(num);
                }
                num = actualEmpire.MiningStations.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.MiningStations.RemoveAt(num);
                }
                num = actualEmpire.SpacePorts.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.SpacePorts.RemoveAt(num);
                }
                num = actualEmpire.ConstructionYards.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ConstructionYards.RemoveAt(num);
                }
                num = actualEmpire.Freighters.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.Freighters.RemoveAt(num);
                }
                num = actualEmpire.ConstructionShips.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ConstructionShips.RemoveAt(num);
                }
                num = actualEmpire.ResearchFacilities.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ResearchFacilities.RemoveAt(num);
                }
                num = actualEmpire.ResortBases.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ResortBases.RemoveAt(num);
                }
                num = actualEmpire.PlanetDestroyers.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.PlanetDestroyers.RemoveAt(num);
                }
                num = actualEmpire.LongRangeScanners.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.LongRangeScanners.RemoveAt(num);
                    if (actualEmpire == _Galaxy.PlayerEmpire)
                    {
                        _Galaxy.OnRefreshView(new RefreshViewEventArgs(Xpos, Ypos, null, onlyGalaxyBackdrops: true));
                    }
                }
            }
            for (int num11 = 0; num11 < galaxy.BuiltObjects.Count; num11++)
            {
                if (galaxy.BuiltObjects[num11] == null)
                {
                    continue;
                }
                if (galaxy.BuiltObjects[num11].ParentBuiltObject == this)
                {
                    if (ParentHabitat != null)
                    {
                        galaxy.BuiltObjects[num11].ParentHabitat = ParentHabitat;
                        galaxy.BuiltObjects[num11].ParentOffsetX = galaxy.BuiltObjects[num11].Xpos - ParentHabitat.Xpos;
                        galaxy.BuiltObjects[num11].ParentOffsetY = galaxy.BuiltObjects[num11].Ypos - ParentHabitat.Ypos;
                    }
                    else
                    {
                        galaxy.BuiltObjects[num11].ParentBuiltObject = null;
                        galaxy.BuiltObjects[num11].ParentOffsetX = -2000000001.0;
                        galaxy.BuiltObjects[num11].ParentOffsetY = -2000000001.0;
                    }
                }
                ClearAllMissionsForTarget(galaxy.BuiltObjects[num11], this);
            }
            int x = (int)Xpos / Galaxy.IndexSize;
            int y = (int)Ypos / Galaxy.IndexSize;
            Galaxy.CorrectIndexCoords(ref x, ref y);
            GalaxyIndexList galaxyIndexList = new GalaxyIndexList();
            galaxyIndexList.Add(new GalaxyIndex(x, y));
            int num12 = x;
            int num13 = y;
            num12 = ((!(Xpos % (double)Galaxy.IndexSize > (double)(Galaxy.IndexSize / 2))) ? (x - 1) : (x + 1));
            num13 = ((!(Ypos % (double)Galaxy.IndexSize > (double)(Galaxy.IndexSize / 2))) ? (y - 1) : (y + 1));
            num12 = Math.Max(0, Math.Min(Galaxy.IndexMaxX - 1, num12));
            num13 = Math.Max(0, Math.Min(Galaxy.IndexMaxX - 1, num13));
            galaxyIndexList.Add(new GalaxyIndex(x, num13));
            galaxyIndexList.Add(new GalaxyIndex(num12, y));
            galaxyIndexList.Add(new GalaxyIndex(num12, num13));
            for (int num14 = 0; num14 < galaxyIndexList.Count; num14++)
            {
                while (galaxy.BuiltObjectIndex[galaxyIndexList[num14].X][galaxyIndexList[num14].Y].Contains(this))
                {
                    galaxy.BuiltObjectIndex[galaxyIndexList[num14].X][galaxyIndexList[num14].Y].Remove(this);
                }
            }
            int num15 = _Galaxy.BuiltObjects.IndexOf(this);
            if (num15 >= 0)
            {
                _Galaxy.BuiltObjects[num15] = null;
            }
            if (removeFromEmpire && Empire != null)
            {
                if (Empire.BuiltObjects != null && Empire.BuiltObjects.Contains(this))
                {
                    Empire.BuiltObjects.Remove(this);
                }
                else if (Empire.PrivateBuiltObjects != null && Empire.PrivateBuiltObjects.Contains(this))
                {
                    Empire.PrivateBuiltObjects.Remove(this);
                }
                if (ActualEmpire != null && ActualEmpire != Empire)
                {
                    if (ActualEmpire.PrivateBuiltObjects != null && ActualEmpire.PrivateBuiltObjects.Contains(this))
                    {
                        ActualEmpire.PrivateBuiltObjects.Remove(this);
                    }
                    if (ActualEmpire.BuiltObjects != null && ActualEmpire.BuiltObjects.Contains(this))
                    {
                        ActualEmpire.BuiltObjects.Remove(this);
                    }
                }
            }
            _Galaxy.CheckCancelAttackMissionsForBuiltObject(this, null);
            if (Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null)
            {
                Empire.ResolveSystemVisibility(this, excludeBuiltObject: true);
            }
            else if (ActualEmpire != null && ActualEmpire != _Galaxy.IndependentEmpire)
            {
                ActualEmpire.ResolveSystemVisibility(this, excludeBuiltObject: true);
            }
        }

        public void ClearAllMissionsForTarget(BuiltObject builtObject, Empire target)
        {
            ClearAllMissionsForTarget(builtObject, target, BuiltObjectMissionType.Undefined, dropOutOfHyperspace: false);
        }

        public void ClearAllMissionsForTarget(BuiltObject builtObject, Empire target, BuiltObjectMissionType missionType, bool dropOutOfHyperspace)
        {
            Empire empire = null;
            if (builtObject.Mission != null && (missionType == BuiltObjectMissionType.Undefined || builtObject.Mission.Type == missionType))
            {
                empire = BuiltObjectMission.ResolveMissionTargetEmpire(builtObject.Mission);
                if (empire == target)
                {
                    builtObject.ClearPreviousMissionRequirements();
                    if (dropOutOfHyperspace && builtObject.CurrentSpeed > (float)builtObject.TopSpeed)
                    {
                        builtObject.CurrentSpeed = builtObject.CruiseSpeed;
                        builtObject.TargetSpeed = builtObject.CruiseSpeed;
                        UpdatePosition();
                        CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(_Galaxy.CurrentDateTime);
                    }
                }
                empire = BuiltObjectMission.ResolveMissionSecondaryTargetEmpire(builtObject.Mission);
                if (empire == target)
                {
                    builtObject.ClearPreviousMissionRequirements();
                    if (dropOutOfHyperspace && builtObject.CurrentSpeed > (float)builtObject.TopSpeed)
                    {
                        builtObject.CurrentSpeed = builtObject.CruiseSpeed;
                        builtObject.TargetSpeed = builtObject.CruiseSpeed;
                        UpdatePosition();
                        CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(_Galaxy.CurrentDateTime);
                    }
                }
            }
            BuiltObjectMissionList builtObjectMissionList = new BuiltObjectMissionList();
            for (int i = 0; i < builtObject.SubsequentMissions.Count; i++)
            {
                BuiltObjectMission builtObjectMission = builtObject.SubsequentMissions[i];
                if (builtObjectMission != null && (missionType == BuiltObjectMissionType.Undefined || builtObjectMission.Type == missionType))
                {
                    empire = BuiltObjectMission.ResolveMissionTargetEmpire(builtObjectMission);
                    if (empire == target)
                    {
                        builtObjectMissionList.Add(builtObjectMission);
                    }
                    empire = BuiltObjectMission.ResolveMissionSecondaryTargetEmpire(builtObjectMission);
                    if (empire == target)
                    {
                        builtObjectMissionList.Add(builtObjectMission);
                    }
                }
            }
            for (int j = 0; j < builtObjectMissionList.Count; j++)
            {
                builtObject.SubsequentMissions.Remove(builtObjectMissionList[j]);
            }
            if (builtObject.Fighters == null || builtObject.Fighters.Count <= 0)
            {
                return;
            }
            for (int k = 0; k < builtObject.Fighters.Count; k++)
            {
                Fighter fighter = builtObject.Fighters[k];
                if (fighter.CurrentTarget != null && fighter.CurrentTarget.Empire == target)
                {
                    fighter.AbandonAttackTarget();
                    fighter.EvaluateThreats(_Galaxy);
                    if (fighter.MissionType == FighterMissionType.Undefined)
                    {
                        fighter.MissionType = FighterMissionType.Patrol;
                    }
                }
            }
        }

        public void ClearAllMissionsForTarget(BuiltObject builtObject, BuiltObject target)
        {
            ClearAllMissionsForTarget(builtObject, target, BuiltObjectMissionType.Undefined, dropOutOfHyperspace: false);
        }

        public void ClearAllMissionsForTarget(BuiltObject builtObject, BuiltObject target, BuiltObjectMissionType missionType, bool dropOutOfHyperspace)
        {
            if (builtObject == null)
            {
                return;
            }
            BuiltObjectMission mission = builtObject.Mission;
            if (mission != null && mission.Type != 0 && (missionType == BuiltObjectMissionType.Undefined || mission.Type == missionType))
            {
                BuiltObject targetBuiltObject = mission.TargetBuiltObject;
                if (targetBuiltObject != null && targetBuiltObject == target)
                {
                    bool flag = true;
                    BuiltObjectMissionType type = mission.Type;
                    if (type == BuiltObjectMissionType.Transport)
                    {
                        flag = !mission.CheckCommandsPastPrimaryTarget(targetBuiltObject);
                    }
                    if (flag)
                    {
                        builtObject.ClearPreviousMissionRequirements();
                        if (dropOutOfHyperspace && builtObject.CurrentSpeed > (float)builtObject.TopSpeed)
                        {
                            builtObject.CurrentSpeed = builtObject.CruiseSpeed;
                            builtObject.TargetSpeed = builtObject.CruiseSpeed;
                            CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(_Galaxy.CurrentDateTime);
                        }
                    }
                }
                targetBuiltObject = mission.SecondaryTargetBuiltObject;
                if (targetBuiltObject != null && targetBuiltObject == target)
                {
                    builtObject.ClearPreviousMissionRequirements();
                    if (dropOutOfHyperspace && builtObject.CurrentSpeed > (float)builtObject.TopSpeed)
                    {
                        builtObject.CurrentSpeed = builtObject.CruiseSpeed;
                        builtObject.TargetSpeed = builtObject.CruiseSpeed;
                        CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(_Galaxy.CurrentDateTime);
                    }
                }
            }
            BuiltObjectMission revertMission = builtObject.RevertMission;
            if (revertMission != null && revertMission.Type != 0 && (missionType == BuiltObjectMissionType.Undefined || revertMission.Type == missionType))
            {
                if (revertMission.TargetBuiltObject != null && revertMission.TargetBuiltObject == target)
                {
                    builtObject.RevertMission = null;
                }
                else if (revertMission.SecondaryTargetBuiltObject != null && revertMission.SecondaryTargetBuiltObject == target)
                {
                    builtObject.RevertMission = null;
                }
            }
            BuiltObjectMissionList subsequentMissions = builtObject.SubsequentMissions;
            BuiltObjectMissionList builtObjectMissionList = null;
            for (int i = 0; i < subsequentMissions.Count; i++)
            {
                BuiltObjectMission builtObjectMission = subsequentMissions[i];
                if (builtObjectMission == null || builtObjectMission.Type == BuiltObjectMissionType.Undefined || (missionType != 0 && builtObjectMission.Type != missionType))
                {
                    continue;
                }
                BuiltObject targetBuiltObject2 = builtObjectMission.TargetBuiltObject;
                if (targetBuiltObject2 != null && targetBuiltObject2 == target)
                {
                    if (builtObjectMissionList == null)
                    {
                        builtObjectMissionList = new BuiltObjectMissionList();
                    }
                    builtObjectMissionList.Add(builtObjectMission);
                }
                targetBuiltObject2 = builtObjectMission.SecondaryTargetBuiltObject;
                if (targetBuiltObject2 != null && targetBuiltObject2 == target)
                {
                    if (builtObjectMissionList == null)
                    {
                        builtObjectMissionList = new BuiltObjectMissionList();
                    }
                    builtObjectMissionList.Add(builtObjectMission);
                }
            }
            if (builtObjectMissionList != null)
            {
                for (int j = 0; j < builtObjectMissionList.Count; j++)
                {
                    builtObject.SubsequentMissions.Remove(builtObjectMissionList[j]);
                }
            }
            FighterList fighters = builtObject.Fighters;
            if (fighters == null || fighters.Count <= 0)
            {
                return;
            }
            for (int k = 0; k < fighters.Count; k++)
            {
                Fighter fighter = fighters[k];
                if (fighter.CurrentTarget == target)
                {
                    fighter.AbandonAttackTarget();
                    fighter.EvaluateThreats(_Galaxy);
                    if (fighter.MissionType == FighterMissionType.Undefined)
                    {
                        fighter.MissionType = FighterMissionType.Patrol;
                    }
                }
            }
        }

        public void ClearAllMissionsForTarget(BuiltObject builtObject, Habitat target)
        {
            ClearAllMissionsForTarget(builtObject, target, BuiltObjectMissionType.Undefined, dropOutOfHyperspace: false);
        }

        public void ClearAllMissionsForTarget(BuiltObject builtObject, Habitat target, BuiltObjectMissionType missionType, bool dropOutOfHyperspace)
        {
            if (builtObject == null)
            {
                return;
            }
            BuiltObjectMission mission = builtObject.Mission;
            if (mission != null && (missionType == BuiltObjectMissionType.Undefined || mission.Type == missionType))
            {
                if (mission.TargetHabitat != null && mission.TargetHabitat == target)
                {
                    builtObject.ClearPreviousMissionRequirements();
                    if (dropOutOfHyperspace && builtObject.CurrentSpeed > (float)builtObject.TopSpeed)
                    {
                        builtObject.CurrentSpeed = builtObject.CruiseSpeed;
                        builtObject.TargetSpeed = builtObject.CruiseSpeed;
                        CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(_Galaxy.CurrentDateTime);
                    }
                }
                if (mission.SecondaryTargetHabitat != null && mission.SecondaryTargetHabitat == target)
                {
                    builtObject.ClearPreviousMissionRequirements();
                    if (dropOutOfHyperspace && builtObject.CurrentSpeed > (float)builtObject.TopSpeed)
                    {
                        builtObject.CurrentSpeed = builtObject.CruiseSpeed;
                        builtObject.TargetSpeed = builtObject.CruiseSpeed;
                        CheckForPlanetDestroyerWeaponFiringDelayOnHyperExit(_Galaxy.CurrentDateTime);
                    }
                }
            }
            BuiltObjectMissionList subsequentMissions = builtObject.SubsequentMissions;
            BuiltObjectMissionList builtObjectMissionList = null;
            for (int i = 0; i < subsequentMissions.Count; i++)
            {
                BuiltObjectMission builtObjectMission = subsequentMissions[i];
                if (builtObjectMission == null || (missionType != 0 && builtObjectMission.Type != missionType))
                {
                    continue;
                }
                if (builtObjectMission.TargetHabitat != null && builtObjectMission.TargetHabitat == target)
                {
                    if (builtObjectMissionList == null)
                    {
                        builtObjectMissionList = new BuiltObjectMissionList();
                    }
                    builtObjectMissionList.Add(builtObjectMission);
                }
                if (builtObjectMission.SecondaryTargetHabitat != null && builtObjectMission.SecondaryTargetHabitat == target)
                {
                    if (builtObjectMissionList == null)
                    {
                        builtObjectMissionList = new BuiltObjectMissionList();
                    }
                    builtObjectMissionList.Add(builtObjectMission);
                }
            }
            if (builtObjectMissionList != null)
            {
                for (int j = 0; j < builtObjectMissionList.Count; j++)
                {
                    builtObject.SubsequentMissions.Remove(builtObjectMissionList[j]);
                }
            }
        }

        private void InflictBombardDamage(Habitat habitat, int bombardPower)
        {
            Empire empire = habitat.Empire;
            if (!habitat.PlanetaryShieldPresent)
            {
                if (bombardPower > 1 && habitat.Troops != null)
                {
                    double num = habitat.Troops.GetArtilleryTroopDefendStrength();
                    if (num > 0.0)
                    {
                        if (empire != null)
                        {
                            num *= (double)empire.TroopPlanetaryDefenseInterceptBonusFactor;
                        }
                        num /= 7500.0;
                        num = Math.Sqrt(num);
                        num = 0.5 + num;
                        num = Math.Max(1.0, num);
                        bombardPower = Math.Max(1, (int)((double)bombardPower / num));
                    }
                }
                float num2 = (float)bombardPower / 8000f;
                habitat.Damage += num2;
                habitat.Damage = Math.Min(1f, habitat.Damage);
                habitat.RecalculateQuality();
                if (habitat.Troops != null && habitat.Troops.Count > 0)
                {
                    habitat.InflictTroopLosses(Empire, Empire, (double)bombardPower * 1.5, habitat.Troops, null, _Galaxy);
                }
                if (habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0)
                {
                    habitat.InflictTroopLosses(Empire, Empire, (double)bombardPower * 1.5, habitat.InvadingTroops, null, _Galaxy);
                }
                if (habitat.Characters != null && habitat.Characters.Count > 0 && Galaxy.Rnd.Next(0, 1000) < bombardPower)
                {
                    Character character = habitat.Characters[Galaxy.Rnd.Next(0, habitat.Characters.Count)];
                    if (Empire != null && Empire.Counters != null)
                    {
                        Empire.Counters.ProcessCharacterDeath(character);
                    }
                    character.SendDeathMessage(CharacterDeathType.ColonyBombardment, _Galaxy);
                    character.Kill(_Galaxy);
                }
                if (habitat.InvadingCharacters != null && habitat.InvadingCharacters.Count > 0 && Galaxy.Rnd.Next(0, 1000) < bombardPower)
                {
                    Character character2 = habitat.InvadingCharacters[Galaxy.Rnd.Next(0, habitat.InvadingCharacters.Count)];
                    if (Empire != null && Empire.Counters != null)
                    {
                        Empire.Counters.ProcessCharacterDeath(character2);
                    }
                    character2.SendDeathMessage(CharacterDeathType.ColonyBombardment, _Galaxy);
                    character2.Kill(_Galaxy);
                }
                if (habitat.Empire != null && habitat.Empire.Troops != null && habitat.TroopsToRecruit != null && habitat.TroopsToRecruit.Count > 0)
                {
                    for (int i = 0; i < habitat.TroopsToRecruit.Count; i++)
                    {
                        habitat.Empire.Troops.Remove(habitat.TroopsToRecruit[i]);
                    }
                    habitat.TroopsToRecruit.Clear();
                }
                if (habitat.Facilities != null && habitat.Facilities.Count > 0 && Galaxy.Rnd.Next(0, 3000) < bombardPower)
                {
                    PlanetaryFacility planetaryFacility = habitat.Facilities.SelectRandomFacility(PlanetaryFacilityType.PirateCriminalNetwork);
                    if (planetaryFacility != null)
                    {
                        bool flag = true;
                        if ((planetaryFacility.Type == PlanetaryFacilityType.PirateBase || planetaryFacility.Type == PlanetaryFacilityType.PirateFortress || planetaryFacility.Type == PlanetaryFacilityType.PirateCriminalNetwork) && Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            flag = false;
                        }
                        if (flag)
                        {
                            habitat.Facilities.Remove(planetaryFacility);
                            habitat.CheckRemoveFacilityTracking(planetaryFacility);
                            if (planetaryFacility.Type == PlanetaryFacilityType.PirateBase || planetaryFacility.Type == PlanetaryFacilityType.PirateFortress || planetaryFacility.Type == PlanetaryFacilityType.PirateCriminalNetwork)
                            {
                                PirateColonyControl byFacilityControl = habitat.GetPirateControl().GetByFacilityControl();
                                if (byFacilityControl != null)
                                {
                                    PlanetaryFacility planetaryFacility2 = habitat.Facilities.FindBestPirateFacility(includeCriminalNetwork: true);
                                    if (planetaryFacility2 == null)
                                    {
                                        byFacilityControl.HasFacilityControl = false;
                                        float num3 = (byFacilityControl.ControlLevel = Math.Min(0.49f, Math.Max(0.01f, byFacilityControl.ControlLevel - 0.2f)));
                                    }
                                    Empire empireById = _Galaxy.GetEmpireById(byFacilityControl.EmpireId);
                                    if (empireById != null)
                                    {
                                        string description = string.Format(TextResolver.GetText("Bombardment Destroys Facility Description"), planetaryFacility.Name, Name);
                                        empireById.SendMessageToEmpire(empireById, EmpireMessageType.PlanetaryFacilityDestroyed, planetaryFacility, description);
                                    }
                                }
                            }
                            else if (habitat.Empire != null)
                            {
                                string description2 = string.Format(TextResolver.GetText("Bombardment Destroys Facility Description"), planetaryFacility.Name, Name);
                                habitat.Empire.SendMessageToEmpire(habitat.Empire, EmpireMessageType.PlanetaryFacilityDestroyed, planetaryFacility, description2);
                            }
                        }
                    }
                }
                if (habitat.Population != null && habitat.Population.Count > 0 && habitat.Population.TotalAmount > 0)
                {
                    long totalAmount = habitat.Population.TotalAmount;
                    long num4 = bombardPower * 250000;
                    PopulationList populationList = new PopulationList();
                    for (int j = 0; j < habitat.Population.Count; j++)
                    {
                        Population population = habitat.Population[j];
                        double num5 = (double)population.Amount / (double)totalAmount;
                        long num6 = (long)((double)num4 * num5);
                        population.Amount -= num6;
                        if (population.Amount <= 0)
                        {
                            populationList.Add(population);
                        }
                    }
                    for (int k = 0; k < populationList.Count; k++)
                    {
                        habitat.Population.Remove(populationList[k]);
                    }
                    habitat.Population.RecalculateTotalAmount();
                    if (habitat.Population.Count <= 0 || habitat.Population.TotalAmount <= 0)
                    {
                        Character[] array = ListHelper.ToArrayThreadSafe(habitat.Characters);
                        for (int l = 0; l < array.Length; l++)
                        {
                            if (Empire != null && Empire.Counters != null)
                            {
                                Empire.Counters.ProcessCharacterDeath(array[l]);
                            }
                            array[l].SendDeathMessage(CharacterDeathType.ColonyBombardment, _Galaxy);
                            array[l].Kill(_Galaxy);
                        }
                        Character[] array2 = ListHelper.ToArrayThreadSafe(habitat.InvadingCharacters);
                        for (int m = 0; m < array2.Length; m++)
                        {
                            if (Empire != null && Empire.Counters != null)
                            {
                                Empire.Counters.ProcessCharacterDeath(array2[m]);
                            }
                            array2[m].SendDeathMessage(CharacterDeathType.ColonyBombardment, _Galaxy);
                            array2[m].Kill(_Galaxy);
                        }
                        habitat.ClearColony(ActualEmpire);
                    }
                    habitat.Population.RecalculateTotalAmount();
                    if (Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null)
                    {
                        double num7 = (double)num4 / 50000000.0;
                        if (empire != null && empire != _Galaxy.IndependentEmpire && empire.PirateEmpireBaseHabitat == null)
                        {
                            if (empire.CivilityRating > 0.0)
                            {
                                double num8 = 1.0 + empire.CivilityRating / 30.0;
                                num7 *= num8;
                            }
                            else
                            {
                                double val = 1.0 + empire.CivilityRating / 50.0;
                                val = Math.Max(0.01, val);
                                num7 *= val;
                            }
                        }
                        num7 = Math.Max(num7, 0.0);
                        Empire.CivilityRating -= num7;
                        if (empire != null && empire != _Galaxy.IndependentEmpire && empire.PirateEmpireBaseHabitat == null)
                        {
                            EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(Empire);
                            if (empireEvaluation != null)
                            {
                                empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - (double)bombardPower / 1.0;
                            }
                        }
                    }
                }
            }
            Explosion explosion = new Explosion();
            explosion.ExplosionStart = _tempNow;
            explosion.ExplosionSize = (short)(Math.Sqrt(bombardPower) * (Math.PI / 4.0) * 40.0);
            if (explosion.ExplosionSize < 10)
            {
                explosion.ExplosionSize = 10;
            }
            explosion.ExplosionProgression = 0f;
            explosion.ExplosionImageIndex = (short)Galaxy.Rnd.Next(0, 10);
            int num9 = Galaxy.Rnd.Next(0, (int)((double)habitat.Diameter * 0.15));
            if (Galaxy.Rnd.Next(0, 2) == 0)
            {
                num9 *= -1;
            }
            int num10 = Galaxy.Rnd.Next(0, (int)((double)habitat.Diameter * 0.15));
            if (Galaxy.Rnd.Next(0, 2) == 0)
            {
                num10 *= -1;
            }
            explosion.ExplosionOffsetX = (short)num9;
            explosion.ExplosionOffsetY = (short)num10;
            explosion.ExplosionWillDestroy = false;
            if (habitat.Explosions == null)
            {
                habitat.Explosions = new ExplosionList();
            }
            habitat.Explosions.Add(explosion);
        }

        private void ReviewRetrofitConstructionQueue(DateTime time, long starDate)
        {
            ManufacturingQueue retrofitBaseManufacturingQueue = RetrofitBaseManufacturingQueue;
            if (retrofitBaseManufacturingQueue != null)
            {
                bool flag = false;
                if (retrofitBaseManufacturingQueue.ComponentWaitQueue != null && retrofitBaseManufacturingQueue.ComponentWaitQueue.Count > 0)
                {
                    flag = true;
                }
                if (retrofitBaseManufacturingQueue.Manufacturers != null && retrofitBaseManufacturingQueue.Manufacturers.Count > 0)
                {
                    for (int i = 0; i < retrofitBaseManufacturingQueue.Manufacturers.Count; i++)
                    {
                        Manufacturer manufacturer = retrofitBaseManufacturingQueue.Manufacturers[i];
                        if (manufacturer != null && manufacturer.Component != null)
                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    retrofitBaseManufacturingQueue.DoManufacturing(_Galaxy, time, starDate);
                }
                else
                {
                    RetrofitBaseManufacturingQueue = null;
                }
            }
            ConstructionQueue retrofitBaseConstructionQueue = RetrofitBaseConstructionQueue;
            if (retrofitBaseConstructionQueue == null)
            {
                return;
            }
            bool flag2 = false;
            if (retrofitBaseConstructionQueue.ConstructionWaitQueue != null && retrofitBaseConstructionQueue.ConstructionWaitQueue.Count > 0)
            {
                flag2 = true;
            }
            if (retrofitBaseConstructionQueue.ConstructionYards != null && retrofitBaseConstructionQueue.ConstructionYards.Count > 0)
            {
                for (int j = 0; j < retrofitBaseConstructionQueue.ConstructionYards.Count; j++)
                {
                    ConstructionYard constructionYard = retrofitBaseConstructionQueue.ConstructionYards[j];
                    if (constructionYard != null && constructionYard.ShipUnderConstruction != null)
                    {
                        flag2 = true;
                    }
                }
            }
            if (flag2)
            {
                retrofitBaseConstructionQueue.DoConstruction(_Galaxy, time);
            }
            else
            {
                RetrofitBaseConstructionQueue = null;
            }
        }

        private void ReviewDisabledComponents(double timePassed)
        {
            if (DisabledComponentIndexes == null || DisabledComponentIndexes.Count <= 0)
            {
                return;
            }
            bool flag = false;
            List<int> list = new List<int>();
            for (int i = 0; i < DisabledComponentIndexes.Count; i++)
            {
                _ = DisabledComponentIndexes[i];
                short num = 0;
                if (DisabledComponentDurations.Count > i)
                {
                    num = DisabledComponentDurations[i];
                }
                num = (short)(num - (short)(timePassed * 1000.0));
                if (num <= 0)
                {
                    flag = true;
                    list.Add(i);
                }
                DisabledComponentDurations[i] = num;
            }
            if (list.Count > 0)
            {
                for (int num2 = list.Count - 1; num2 >= 0; num2--)
                {
                    DisabledComponentIndexes.RemoveAt(list[num2]);
                    DisabledComponentDurations.RemoveAt(list[num2]);
                }
            }
            if (DisabledComponentIndexes.Count <= 0)
            {
                DisabledComponentIndexes = null;
                DisabledComponentDurations = null;
            }
            if (flag)
            {
                ReDefine();
            }
        }

        private double InflictIonDamage(StellarObject target, Weapon weapon, double hitPower, DateTime time, Galaxy galaxy, double strikeAngle)
        {
            hitPower *= BaconBuiltObject.InflictDamage(this);
            if (target is Creature)
            {
                Creature creature = (Creature)target;
                if (creature.Type == CreatureType.SilverMist && creature.DamageCreature(this, (int)hitPower, weapon))
                {
                    _Galaxy.CheckTriggerEvent(creature.GameEventId, Empire, EventTriggerType.Destroy, null);
                    if (creature.Type == CreatureType.SilverMist && Empire != null)
                    {
                        Empire.CivilityRating += Galaxy.DestroySilverMistReputationBonus;
                    }
                    creature.CompleteTeardown();
                }
            }
            else if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                builtObject.IonStrikeSoundPlayed = false;
                if (builtObject.DisabledComponentIndexes == null)
                {
                    builtObject.DisabledComponentIndexes = new List<short>();
                }
                if (builtObject.DisabledComponentDurations == null)
                {
                    builtObject.DisabledComponentDurations = new List<short>();
                }
                if (builtObject.IonDefense > 0)
                {
                    hitPower -= (double)builtObject.IonDefense;
                }
                if (hitPower > 0.0)
                {
                    if (Galaxy.Rnd.Next(0, 2) == 1 && (builtObject.FirepowerRaw > 0 || builtObject.BombardWeaponPower > 0))
                    {
                        for (int i = 0; i < builtObject.Components.Count; i++)
                        {
                            if (hitPower <= 0.0)
                            {
                                return hitPower;
                            }
                            switch (builtObject.Components[i].Category)
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
                                    if (builtObject.Components[i].Status == ComponentStatus.Normal && !builtObject.DisabledComponentIndexes.Contains((short)i))
                                    {
                                        builtObject.DisabledComponentIndexes.Add((short)i);
                                        builtObject.DisabledComponentDurations.Add((short)Galaxy.Rnd.Next(15000, 25000));
                                        builtObject.ReDefine();
                                        builtObject.LastIonStrike = time;
                                        hitPower -= 10.0;
                                    }
                                    break;
                            }
                        }
                    }
                    else if (builtObject.TopSpeed > 0 || builtObject.TurnRate > 0.1f)
                    {
                        for (int j = 0; j < builtObject.Components.Count; j++)
                        {
                            if (hitPower <= 0.0)
                            {
                                return hitPower;
                            }
                            ComponentCategoryType category = builtObject.Components[j].Category;
                            if (category == ComponentCategoryType.Engine && builtObject.Components[j].Status == ComponentStatus.Normal && !builtObject.DisabledComponentIndexes.Contains((short)j))
                            {
                                builtObject.DisabledComponentIndexes.Add((short)j);
                                builtObject.DisabledComponentDurations.Add((short)Galaxy.Rnd.Next(15000, 25000));
                                builtObject.ReDefine();
                                builtObject.LastIonStrike = time;
                                hitPower -= 10.0;
                            }
                        }
                    }
                }
            }
            return hitPower;
        }

        private bool InflictDamage(StellarObject target, Weapon weapon, double hitPower, DateTime time, Galaxy galaxy, float weaponDistanceTravelled, double strikeAngle)
        {
            return InflictDamage(target, weapon, hitPower, time, galaxy, weaponDistanceTravelled, allowRecursion: true, strikeAngle, allowArmorInvulnerability: false);
        }

        public bool InflictDamage(StellarObject abstractTarget, Weapon weapon, double hitPower, DateTime time, Galaxy galaxy, float weaponDistanceTravelled, bool allowRecursion, double strikeAngle, bool allowArmorInvulnerability)
        {
            hitPower *= BaconBuiltObject.InflictDamage(this);
            if (abstractTarget is Creature)
            {
                Creature creature = (Creature)abstractTarget;
                if (creature.DamageCreature(this, (int)hitPower, weapon))
                {
                    if (creature.Type == CreatureType.SilverMist && Empire != null)
                    {
                        Empire.CivilityRating += Galaxy.DestroySilverMistReputationBonus;
                    }
                    _Galaxy.CheckTriggerEvent(creature.GameEventId, ActualEmpire, EventTriggerType.Destroy, null);
                    creature.CompleteTeardown();
                    return true;
                }
            }
            else if (abstractTarget is Fighter)
            {
                Fighter fighter = (Fighter)abstractTarget;
                if (BattleStats != null)
                {
                    BattleStats.WeaponHitEnemy((float)hitPower, weaponDistanceTravelled);
                }
                if (ShipGroup != null && ShipGroup.BattleStats != null)
                {
                    ShipGroup.BattleStats.WeaponHitEnemy((float)hitPower, weaponDistanceTravelled);
                }
                if ((double)fighter.CurrentShields >= hitPower)
                {
                    fighter.CurrentShields -= (float)hitPower;
                    fighter.LastShieldStrike = time;
                    fighter.LastShieldStrikeDirection = (float)strikeAngle;
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.BattleStats.ShieldsStruckUs((float)hitPower);
                    }
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.ShipGroup != null && fighter.ParentBuiltObject.ShipGroup.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.ShipGroup.BattleStats.ShieldsStruckUs((float)hitPower);
                    }
                }
                else
                {
                    int num = (int)((float)hitPower - fighter.CurrentShields + 0.5f);
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.BattleStats.ShieldsStruckUs(fighter.CurrentShields);
                    }
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.ShipGroup != null && fighter.ParentBuiltObject.ShipGroup.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.ShipGroup.BattleStats.ShieldsStruckUs(fighter.CurrentShields);
                    }
                    fighter.CurrentShields = 0f;
                    if (num > fighter.Size)
                    {
                        num = fighter.Size;
                    }
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.BattleStats.DamageHullUs(num);
                    }
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.ShipGroup != null && fighter.ParentBuiltObject.ShipGroup.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.ShipGroup.BattleStats.DamageHullUs(num);
                    }
                    if ((float)fighter.Size * fighter.Health <= (float)num && !fighter.HasBeenDestroyed)
                    {
                        fighter.Health = 0f;
                        fighter.HasBeenDestroyed = true;
                        if (BattleStats != null)
                        {
                            BattleStats.FighterDestroyedEnemy();
                        }
                        if (ShipGroup != null && ShipGroup.BattleStats != null)
                        {
                            ShipGroup.BattleStats.FighterDestroyedEnemy();
                        }
                        if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.BattleStats != null)
                        {
                            fighter.ParentBuiltObject.BattleStats.FighterDestroyedFriendly();
                        }
                        if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.ShipGroup != null && fighter.ParentBuiltObject.ShipGroup.BattleStats != null)
                        {
                            fighter.ParentBuiltObject.ShipGroup.BattleStats.FighterDestroyedFriendly();
                        }
                        if (Empire != null && Empire != galaxy.IndependentEmpire && fighter.Empire != null && fighter.Empire.PirateEmpireBaseHabitat != null)
                        {
                            double num2 = 0.015;
                            Empire.CivilityRating += num2;
                        }
                        Explosion explosion = new Explosion();
                        explosion.ExplosionStart = time;
                        explosion.ExplosionSize = (short)(Math.Sqrt((double)fighter.Size * 0.3) * (Math.PI / 4.0) * 30.0);
                        explosion.ExplosionProgression = 0f;
                        explosion.ExplosionOffsetX = 0;
                        explosion.ExplosionOffsetY = 0;
                        explosion.ExplosionImageIndex = (short)Galaxy.Rnd.Next(10, 20);
                        explosion.ExplosionWillDestroy = true;
                        fighter.Explosions.Add(explosion);
                        galaxy.InflictWarDamage(Empire, fighter);
                        if (fighter.Empire != null)
                        {
                            fighter.Empire.ResolveSystemVisibility(fighter.Xpos, fighter.Ypos, null, null);
                        }
                        return true;
                    }
                    fighter.Health -= (float)((double)num / (double)fighter.Size);
                    fighter.OverlayChanged = true;
                    Explosion explosion2 = new Explosion();
                    explosion2.ExplosionStart = time;
                    if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.WeaponMissile)
                    {
                        explosion2.ExplosionSize = (short)(Math.Sqrt((double)num * 0.7) * (Math.PI / 4.0) * 30.0);
                    }
                    else
                    {
                        explosion2.ExplosionSize = (short)(Math.Sqrt((double)num * 0.3) * (Math.PI / 4.0) * 30.0);
                    }
                    if (explosion2.ExplosionSize < 5)
                    {
                        explosion2.ExplosionSize = 5;
                    }
                    explosion2.ExplosionProgression = 0f;
                    explosion2.ExplosionImageIndex = (short)Galaxy.Rnd.Next(0, 10);
                    int num3 = Galaxy.Rnd.Next(0, (int)(Math.Sqrt(fighter.Size) * 0.7));
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num3 *= -1;
                    }
                    int num4 = Galaxy.Rnd.Next(0, (int)(Math.Sqrt(fighter.Size) * 0.7));
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num4 *= -1;
                    }
                    explosion2.ExplosionOffsetX = (short)num3;
                    explosion2.ExplosionOffsetY = (short)num4;
                    explosion2.ExplosionWillDestroy = false;
                    fighter.Explosions.Add(explosion2);
                }
            }
            else if (abstractTarget is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)abstractTarget;
                if (BattleStats != null)
                {
                    BattleStats.WeaponHitEnemy((float)hitPower, weaponDistanceTravelled);
                }
                if (ShipGroup != null && ShipGroup.BattleStats != null)
                {
                    ShipGroup.BattleStats.WeaponHitEnemy((float)hitPower, weaponDistanceTravelled);
                }
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                if (weapon != null && weapon.Component != null)
                {
                    switch (weapon.Component.Type)
                    {
                        case ComponentType.WeaponRailGun:
                        case ComponentType.WeaponSuperRailGun:
                            flag = true;
                            break;
                        case ComponentType.WeaponPhaser:
                        case ComponentType.WeaponSuperPhaser:
                            flag2 = true;
                            break;
                        case ComponentType.WeaponGravityBeam:
                        case ComponentType.WeaponAreaGravity:
                            flag3 = true;
                            break;
                    }
                }
                if ((double)builtObject.CurrentShields >= hitPower && !flag && !flag3)
                {
                    builtObject.CurrentShields -= (float)hitPower;
                    if (!flag2)
                    {
                        builtObject.LastShieldStrike = time;
                    }
                    builtObject.LastShieldStrikeDirection = (float)strikeAngle;
                    if (builtObject.BattleStats != null)
                    {
                        builtObject.BattleStats.ShieldsStruckUs((float)hitPower);
                    }
                    if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                    {
                        builtObject.ShipGroup.BattleStats.ShieldsStruckUs((float)hitPower);
                    }
                    _Galaxy.ChanceAttackedPirateFactionJoinsPhantomPirates(Empire, builtObject);
                }
                else
                {
                    int num5 = (int)((float)hitPower - builtObject.CurrentShields + 0.5f);
                    if (flag3)
                    {
                        num5 = (int)((float)hitPower + 0.5f);
                    }
                    else if (flag)
                    {
                        double num6 = 0.25 + Galaxy.Rnd.NextDouble() * 0.5;
                        double num7 = hitPower * num6;
                        double num8 = hitPower - num7;
                        if (num8 > 0.0)
                        {
                            builtObject.CurrentShields -= (float)num8;
                            if (!flag2)
                            {
                                builtObject.LastShieldStrike = time;
                            }
                            builtObject.LastShieldStrikeDirection = (float)strikeAngle;
                            if (builtObject.BattleStats != null)
                            {
                                builtObject.BattleStats.ShieldsStruckUs((float)num8);
                            }
                            if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                            {
                                builtObject.ShipGroup.BattleStats.ShieldsStruckUs((float)num8);
                            }
                            _Galaxy.ChanceAttackedPirateFactionJoinsPhantomPirates(Empire, builtObject);
                        }
                        num5 = (int)num7;
                    }
                    else
                    {
                        if (builtObject.BattleStats != null)
                        {
                            builtObject.BattleStats.ShieldsStruckUs(builtObject.CurrentShields);
                        }
                        if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                        {
                            builtObject.ShipGroup.BattleStats.ShieldsStruckUs(builtObject.CurrentShields);
                        }
                        builtObject.CurrentShields = 0f;
                    }
                    if (builtObject.DamageRepair > 0 && builtObject.DamagedComponentCount == 0)
                    {
                        long num9 = (builtObject.LastRepair = _Galaxy.CurrentStarDate);
                    }
                    if (builtObject.Armor > 0 && !flag3 && builtObject.Components != null)
                    {
                        BuiltObjectComponent builtObjectComponent = builtObject.Components[ComponentCategoryType.Armor, ComponentStatus.Normal];
                        int iterationCount = 0;
                        while (Galaxy.ConditionCheckLimit(builtObjectComponent != null && num5 > 0, 500, ref iterationCount))
                        {
                            if (num5 < 1073741823 && builtObjectComponent.Value2 > 0)
                            {
                                int num10 = builtObjectComponent.Value2 * BaconBuiltObject.ArmorReactivityMultiplier(builtObject);
                                if (builtObject.ArmorReinforcingFactor > 0)
                                {
                                    num10 = (int)((double)num10 * ((double)builtObject.ArmorReinforcingFactor / 100.0));
                                }
                                if (weapon != null && weapon.Component != null)
                                {
                                    ComponentType type = weapon.Component.Type;
                                    if ((type == ComponentType.WeaponPhaser || type == ComponentType.WeaponSuperPhaser) && num10 > 0)
                                    {
                                        num10 = Math.Max(1, num10 / 2);
                                    }
                                }
                                if (num5 <= num10)
                                {
                                    if (allowArmorInvulnerability)
                                    {
                                        num5 = 0;
                                    }
                                    else
                                    {
                                        double num11 = (double)num10 / (double)num5;
                                        double num12 = Galaxy.Rnd.NextDouble() * num11;
                                        num5 = ((num12 < 0.2) ? 1 : 0);
                                    }
                                }
                                else
                                {
                                    num5 -= num10;
                                }
                            }
                            if (num5 <= 0)
                            {
                                continue;
                            }
                            if (weapon != null && weapon.Component != null)
                            {
                                switch (weapon.Component.Type)
                                {
                                    case ComponentType.WeaponMissile:
                                    case ComponentType.WeaponRailGun:
                                    case ComponentType.WeaponSuperMissile:
                                    case ComponentType.WeaponSuperRailGun:
                                        num5 = Math.Max(1, num5 / 2);
                                        break;
                                }
                            }
                            int num13 = builtObjectComponent.Value1;
                            if (builtObject.ArmorReinforcingFactor > 0)
                            {
                                num13 = (int)((double)num13 * ((double)builtObject.ArmorReinforcingFactor / 100.0));
                            }
                            double val = (double)num5 / (double)num13;
                            val = Math.Max(0.1, val);
                            if (Galaxy.Rnd.NextDouble() < val)
                            {
                                builtObjectComponent.Status = ComponentStatus.Damaged;
                            }
                            num5 -= num13;
                            builtObjectComponent = builtObject.Components[ComponentCategoryType.Armor, ComponentStatus.Normal];
                        }
                    }
                    double num14 = builtObject.DamageReduction;
                    if (builtObject.ShipGroup != null)
                    {
                        num14 *= builtObject.ShipGroup.DamageControlBonus;
                    }
                    num14 *= builtObject.CaptainDamageControlBonus;
                    num5 = (int)((double)num5 + 0.49 - (double)num5 * num14);
                    if (num5 > builtObject.Size)
                    {
                        num5 = builtObject.Size;
                    }
                    if (builtObject.BattleStats != null)
                    {
                        builtObject.BattleStats.DamageHullUs(num5);
                    }
                    if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                    {
                        builtObject.ShipGroup.BattleStats.DamageHullUs(num5);
                    }
                    BaconBuiltObject.SaveShipInfoBeforeDestruction(builtObject);
                    if (builtObject.UndamagedComponentSize <= num5 && !builtObject.HasBeenDestroyed)
                    {
                        if (BattleStats != null)
                        {
                            BattleStats.TargetDestroyedEnemy(builtObject);
                        }
                        if (ShipGroup != null && ShipGroup.BattleStats != null)
                        {
                            ShipGroup.BattleStats.TargetDestroyedEnemy(builtObject);
                        }
                        if (builtObject.BattleStats != null)
                        {
                            builtObject.BattleStats.TargetDestroyedFriendly(builtObject);
                        }
                        if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                        {
                            builtObject.ShipGroup.BattleStats.TargetDestroyedFriendly(builtObject);
                        }
                        _Galaxy.CheckTriggerEvent(builtObject.GameEventId, ActualEmpire, EventTriggerType.Destroy, null);
                        if (Empire != null && Empire != _Galaxy.IndependentEmpire && builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                        {
                            double num15 = 0.05;
                            switch (builtObject.SubRole)
                            {
                                case BuiltObjectSubRole.SmallSpacePort:
                                    num15 = 0.25;
                                    break;
                                case BuiltObjectSubRole.MediumSpacePort:
                                    num15 = 0.35;
                                    break;
                                case BuiltObjectSubRole.LargeSpacePort:
                                    num15 = 0.5;
                                    break;
                            }
                            Empire.CivilityRating += num15;
                        }
                        if (Role != BuiltObjectRole.Base)
                        {
                            _Galaxy.ChanceRaceEvent(builtObject, this);
                            if (!_Galaxy.ChanceNewShipCaptain(builtObject, Empire, this))
                            {
                                _Galaxy.ChanceNewFleetAdmiral(builtObject, Empire, this);
                            }
                        }
                        if (Empire != null && Empire.Counters != null)
                        {
                            Empire.Counters.ProcessBuiltObjectDestruction(builtObject);
                        }
                        Explosion explosion3 = new Explosion();
                        explosion3.ExplosionStart = _tempNow;
                        explosion3.ExplosionSize = (short)(Math.Sqrt(builtObject.Components.Count) * (Math.PI / 4.0) * 30.0);
                        explosion3.ExplosionProgression = 0f;
                        explosion3.ExplosionOffsetX = 0;
                        explosion3.ExplosionOffsetY = 0;
                        explosion3.ExplosionImageIndex = (short)Galaxy.Rnd.Next(10, 20);
                        explosion3.ExplosionWillDestroy = true;
                        builtObject.Explosions.Add(explosion3);
                        BaconBuiltObject.CollectScrapFromDestroyedBuiltObjects(this, builtObject);
                        builtObject.HasBeenDestroyed = true;
                        _Galaxy.InflictWarDamage(Empire, builtObject);
                        if (allowRecursion)
                        {
                            GalaxyIndex galaxyIndex = _Galaxy.ResolveIndex(builtObject.Xpos, builtObject.Ypos);
                            for (int i = 0; i < galaxy.BuiltObjectIndex[galaxyIndex.X][galaxyIndex.Y].Count; i++)
                            {
                                BuiltObject builtObject2 = galaxy.BuiltObjectIndex[galaxyIndex.X][galaxyIndex.Y][i];
                                if (builtObject2 != null && builtObject2 != builtObject && galaxy.CheckWithinDistancePotential(400.0, builtObject.Xpos, builtObject.Ypos, builtObject2.Xpos, builtObject2.Ypos))
                                {
                                    double num16 = galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                                    double num17 = (double)builtObject.Size * 0.25 - num16 * 2.0;
                                    if (num17 > 0.0)
                                    {
                                        InflictDamage(builtObject2, null, num17, time, galaxy, weaponDistanceTravelled, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
                                    }
                                }
                            }
                        }
                        if (builtObject.Empire != null)
                        {
                            builtObject.Empire.ResolveSystemVisibility(builtObject.Xpos, builtObject.Ypos, builtObject, null);
                        }
                        if (builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionYards != null && builtObject.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                        {
                            foreach (ConstructionYard constructionYard in builtObject.ConstructionQueue.ConstructionYards)
                            {
                                BuiltObject shipUnderConstruction = constructionYard.ShipUnderConstruction;
                                shipUnderConstruction?.InflictDamage(shipUnderConstruction, null, double.MaxValue, time, _Galaxy, weaponDistanceTravelled, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
                            }
                        }
                        builtObject.ReDefine();
                        return true;
                    }
                    int num18 = num5;
                    if (builtObject.Components != null)
                    {
                        int iterationCount2 = 0;
                        while (Galaxy.ConditionCheckLimit(num5 > 0, 500, ref iterationCount2))
                        {
                            int num19 = 0;
                            int num20 = 0;
                            do
                            {
                                num19 = Galaxy.Rnd.Next(0, builtObject.Components.Count);
                                num20++;
                            }
                            while (num20 <= 30 && num19 < builtObject.Components.Count && builtObject.Components[num19].Status == ComponentStatus.Damaged);
                            if (num19 >= builtObject.Components.Count)
                            {
                                continue;
                            }
                            if (builtObject.Components[num19].Status == ComponentStatus.Damaged)
                            {
                                _Galaxy.ReseedRandom();
                            }
                            builtObject.Components[num19].Status = ComponentStatus.Damaged;
                            if (builtObject.Role != BuiltObjectRole.Base)
                            {
                                switch (builtObject.Components[num19].Type)
                                {
                                    case ComponentType.StorageCargo:
                                        {
                                            int num22 = builtObject.Components[num19].Value1;
                                            if (builtObject.Cargo == null || builtObject.Cargo.Count <= 0)
                                            {
                                                break;
                                            }
                                            CargoList cargoList = new CargoList();
                                            for (int j = 0; j < builtObject.Cargo.Count; j++)
                                            {
                                                Cargo cargo = builtObject.Cargo[j];
                                                if (num22 <= 0)
                                                {
                                                    break;
                                                }
                                                if (cargo.Amount > num22)
                                                {
                                                    cargo.Amount -= num22;
                                                    num22 = 0;
                                                    break;
                                                }
                                                if (cargo.Amount > 0)
                                                {
                                                    num22 -= cargo.Amount;
                                                    cargoList.Add(cargo);
                                                }
                                            }
                                            foreach (Cargo item in cargoList)
                                            {
                                                builtObject.Cargo.Remove(item);
                                            }
                                            break;
                                        }
                                    case ComponentType.StorageFuel:
                                        {
                                            int value2 = builtObject.Components[num19].Value1;
                                            if (builtObject.CurrentFuel > 0.0)
                                            {
                                                builtObject.CurrentFuel -= value2;
                                                if (builtObject.CurrentFuel < 0.0)
                                                {
                                                    builtObject.CurrentFuel = 0.0;
                                                }
                                            }
                                            break;
                                        }
                                    case ComponentType.StorageTroop:
                                        {
                                            int value = builtObject.Components[num19].Value1;
                                            if (builtObject.Troops == null || builtObject.TroopCapacity <= 0 || builtObject.Troops.TotalSize <= 0)
                                            {
                                                break;
                                            }
                                            builtObject.TroopCapacity -= value;
                                            if (builtObject.Troops.TotalSize <= builtObject.TroopCapacity)
                                            {
                                                break;
                                            }
                                            int num21 = Galaxy.Rnd.Next(0, builtObject.Troops.Count);
                                            if (num21 < builtObject.Troops.Count)
                                            {
                                                if (builtObject.Empire != null && builtObject.Empire.Troops != null)
                                                {
                                                    builtObject.Empire.Troops.Remove(builtObject.Troops[num21]);
                                                }
                                                builtObject.Troops.RemoveAt(num21);
                                            }
                                            break;
                                        }
                                }
                            }
                            num5 -= builtObject.Components[num19].Size;
                        }
                    }
                    builtObject.ReDefine();
                    if (builtObject.Role != BuiltObjectRole.Base && builtObject.DamagedComponentCount > 0)
                    {
                        builtObject.RepairForNextMission = true;
                    }
                    Explosion explosion4 = new Explosion();
                    explosion4.ExplosionStart = _tempNow;
                    explosion4.ExplosionSize = (short)(Math.Sqrt(num18) * (Math.PI / 4.0) * 30.0);
                    if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.WeaponMissile)
                    {
                        explosion4.ExplosionSize = (short)(Math.Sqrt((double)num18 * 2.3) * (Math.PI / 4.0) * 30.0);
                    }
                    else
                    {
                        explosion4.ExplosionSize = (short)(Math.Sqrt(num18) * (Math.PI / 4.0) * 30.0);
                    }
                    if (explosion4.ExplosionSize < 10)
                    {
                        explosion4.ExplosionSize = 10;
                    }
                    explosion4.ExplosionProgression = 0f;
                    explosion4.ExplosionImageIndex = (short)Galaxy.Rnd.Next(0, 10);
                    int num23 = Galaxy.Rnd.Next(0, (int)(Math.Sqrt(builtObject.Size) * 0.7));
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num23 *= -1;
                    }
                    int num24 = Galaxy.Rnd.Next(0, (int)(Math.Sqrt(builtObject.Size) * 0.7));
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num24 *= -1;
                    }
                    explosion4.ExplosionOffsetX = (short)num23;
                    explosion4.ExplosionOffsetY = (short)num24;
                    explosion4.ExplosionWillDestroy = false;
                    builtObject.Explosions.Add(explosion4);
                }
            }
            return false;
        }

        private bool EvaluateRelativeToParent(ref double parentXPos, ref double parentYPos, out double targetArrivalDistance, Galaxy galaxy)
        {
            bool result = false;
            targetArrivalDistance = 0.0;
            BuiltObject parentBuiltObject = ParentBuiltObject;
            if (parentBuiltObject != null && !parentBuiltObject.HasBeenDestroyed)
            {
                if (parentBuiltObject.DockedAt == this || parentBuiltObject.BuiltAt == this || parentBuiltObject.ParentBuiltObject == this)
                {
                    return false;
                }
                if (ParentOffsetX <= -2000000001.0 && ParentOffsetY <= -2000000001.0)
                {
                    if (galaxy.CalculateDistanceSquared(Xpos, Ypos, parentBuiltObject.Xpos, parentBuiltObject.Ypos) <= (double)Galaxy.ParentRelativeRangeSquared && parentBuiltObject.Empire == Empire)
                    {
                        ParentOffsetX = Xpos - parentBuiltObject.Xpos;
                        ParentOffsetY = Ypos - parentBuiltObject.Ypos;
                        result = true;
                        parentXPos = parentBuiltObject.Xpos;
                        parentYPos = parentBuiltObject.Ypos;
                    }
                }
                else
                {
                    result = true;
                    parentXPos = parentBuiltObject.Xpos;
                    parentYPos = parentBuiltObject.Ypos;
                }
            }
            Habitat parentHabitat = ParentHabitat;
            if (parentHabitat != null)
            {
                if (ParentOffsetX <= -2000000001.0 && ParentOffsetY <= -2000000001.0)
                {
                    if (galaxy.CalculateDistanceSquared(Xpos, Ypos, parentHabitat.Xpos, parentHabitat.Ypos) <= (double)Galaxy.ParentRelativeRangeSquared)
                    {
                        ParentOffsetX = Xpos - parentHabitat.Xpos;
                        ParentOffsetY = Ypos - parentHabitat.Ypos;
                        result = true;
                        parentXPos = parentHabitat.Xpos;
                        parentYPos = parentHabitat.Ypos;
                    }
                }
                else
                {
                    if (parentHabitat.Type == HabitatType.BlackHole)
                    {
                        targetArrivalDistance = parentHabitat.Diameter * 3;
                    }
                    else if (parentHabitat.Category == HabitatCategoryType.GasCloud)
                    {
                        targetArrivalDistance = parentHabitat.Diameter / 3;
                    }
                    result = true;
                    parentXPos = parentHabitat.Xpos;
                    parentYPos = parentHabitat.Ypos;
                }
            }
            return result;
        }

        private double DoMovement(double timePassed, double targetX, double targetY, int oldIndexX, int oldIndexY, double parentRelativeX, double parentRelativeY, Galaxy galaxy, bool manageArrival, bool manageHeading, bool manageDeceleration)
        {
            bool arrived = false;
            return DoMovement(timePassed, targetX, targetY, oldIndexX, oldIndexY, parentRelativeX, parentRelativeY, galaxy, manageArrival, manageHeading, manageDeceleration, out arrived);
        }

        private double DoMovement(double timePassed, double targetX, double targetY, int oldIndexX, int oldIndexY, double parentRelativeX, double parentRelativeY, Galaxy galaxy, bool manageArrival, bool manageHeading, bool manageDeceleration, out bool arrived)
        {
            double result = 0.0;
            arrived = false;
            bool flag = false;
            double parentXPos = 0.0;
            double parentYPos = 0.0;
            double targetArrivalDistance = 1.0;
            flag = EvaluateRelativeToParent(ref parentXPos, ref parentYPos, out targetArrivalDistance, galaxy);
            TargetSpeed = (int)PreferredSpeed;
            double num = 0.0;
            double num2 = 0.0;
            if (flag)
            {
                num = parentXPos + parentRelativeX;
                num2 = parentYPos + parentRelativeY;
            }
            else
            {
                num = targetX;
                num2 = targetY;
            }
            double num3 = (double)CurrentSpeed * timePassed;
            if (manageHeading)
            {
                if (flag)
                {
                    _Angle = (float)Galaxy.DetermineAngle(Xpos, Ypos, parentXPos + parentRelativeX, parentYPos + parentRelativeY);
                }
                else
                {
                    _Angle = (float)Galaxy.DetermineAngle(Xpos, Ypos, targetX, targetY);
                }
                TargetHeading = _Angle;
            }
            CalculateCurrentHeading(timePassed);
            double num4;
            double targetX2;
            double targetY2;
            if (flag)
            {
                if (parentRelativeX != 0.0 && parentRelativeY != 0.0)
                {
                    targetArrivalDistance = 1.0;
                }
                num4 = galaxy.CalculateDistance(Xpos, Ypos, parentXPos + parentRelativeX, parentYPos + parentRelativeY);
                targetX2 = parentXPos + parentRelativeX;
                targetY2 = parentYPos + parentRelativeY;
            }
            else
            {
                num4 = galaxy.CalculateDistance(Xpos, Ypos, targetX, targetY);
                targetX2 = targetX;
                targetY2 = targetY;
            }
            if (manageDeceleration)
            {
                CheckForDeceleration(num4, num3);
            }
            if (!WillMeetDestination(num, num2, TargetSpeed))
            {
                if (TargetSpeed > Galaxy.MovementImpulseSpeed * 2)
                {
                    TargetSpeed = Galaxy.MovementImpulseSpeed * 2;
                }
                else
                {
                    TargetSpeed /= 2;
                }
                if (num4 > 10.0 && TargetSpeed < Galaxy.MovementImpulseSpeed)
                {
                    TargetSpeed = Galaxy.MovementImpulseSpeed;
                }
            }
            AccelerateToTargetSpeed(timePassed);
            if (manageArrival)
            {
                double distanceNotTravelled = 0.0;
                if (CheckForArrival(num4, num3, flag, parentXPos, parentYPos, targetX2, targetY2, targetArrivalDistance, out distanceNotTravelled))
                {
                    arrived = true;
                    result = ((!(CurrentSpeed > 0f) || !(distanceNotTravelled > 0.0)) ? 0.0 : (distanceNotTravelled / (double)CurrentSpeed));
                }
                else
                {
                    ConsumeFuel(timePassed);
                    if (flag)
                    {
                        ParentOffsetX += Math.Cos(Heading) * num3;
                        ParentOffsetY += Math.Sin(Heading) * num3;
                        Xpos = parentXPos + ParentOffsetX;
                        Ypos = parentYPos + ParentOffsetY;
                    }
                    else
                    {
                        Xpos += Math.Cos(Heading) * num3;
                        Ypos += Math.Sin(Heading) * num3;
                    }
                }
            }
            else
            {
                ConsumeFuel(timePassed);
                if (flag)
                {
                    ParentOffsetX += Math.Cos(Heading) * num3;
                    ParentOffsetY += Math.Sin(Heading) * num3;
                    Xpos = parentXPos + ParentOffsetX;
                    Ypos = parentYPos + ParentOffsetY;
                }
                else
                {
                    Xpos += Math.Cos(Heading) * num3;
                    Ypos += Math.Sin(Heading) * num3;
                }
            }
            CheckFuelHandicap();
            UpdateIndexesForMovement(oldIndexX, oldIndexY, galaxy, performIndexCheck: false);
            if ((num <= -2000000000.0 || num2 <= -2000000000.0) && !_ExecutingShipGroupCommand)
            {
                TargetSpeed = 0;
                PreferredSpeed = 0f;
                ClearPreviousMissionRequirements();
            }
            return result;
        }

        private void CheckFuelHandicap()
        {
            BaconBuiltObject.CheckFuelHandicap(this);
        }

        private void EnsureWithinGalaxy()
        {
            if (Xpos < 0.0)
            {
                Xpos = 0.0;
            }
            if (Xpos > (double)(Galaxy.SizeX - 1))
            {
                Xpos = Galaxy.SizeX - 1;
            }
            if (Ypos < 0.0)
            {
                Ypos = 0.0;
            }
            if (Ypos > (double)(Galaxy.SizeY - 1))
            {
                Ypos = Galaxy.SizeY - 1;
            }
            bool flag = false;
            if (double.IsNaN(Xpos) || double.IsNaN(Ypos))
            {
                flag = true;
                if (ParentHabitat != null)
                {
                    ParentOffsetX = 0.0;
                    ParentOffsetY = 0.0;
                    Xpos = ParentHabitat.Xpos + ParentOffsetX;
                    Ypos = ParentHabitat.Ypos + ParentOffsetY;
                }
                else if (ParentBuiltObject != null)
                {
                    ParentOffsetX = 0.0;
                    ParentOffsetY = 0.0;
                    Xpos = ParentBuiltObject.Xpos + ParentOffsetX;
                    Ypos = ParentBuiltObject.Ypos + ParentOffsetY;
                }
            }
            if (flag)
            {
                Xpos = Math.Max(Xpos, 0.0);
                Xpos = Math.Min(Xpos, Galaxy.SizeX - 1);
                Ypos = Math.Max(Ypos, 0.0);
                Ypos = Math.Min(Ypos, Galaxy.SizeY - 1);
            }
        }

        private bool CheckCancelRefuelData()
        {
            bool result = false;
            short refuelAmount = _RefuelAmount;
            int refuelLocationId = _RefuelLocationId;
            byte refuelResourceId = _RefuelResourceId;
            bool refuelLocationIsBuiltObject = _RefuelLocationIsBuiltObject;
            _RefuelAmount = 0;
            _RefuelLocationId = -1;
            _RefuelResourceId = byte.MaxValue;
            if ((refuelAmount > 0 || refuelLocationId >= 0) && refuelResourceId >= 0 && refuelResourceId < byte.MaxValue && refuelResourceId < Galaxy.ResourceSystemStatic.Resources.Count)
            {
                if (refuelLocationIsBuiltObject)
                {
                    BuiltObject builtObject = _Galaxy.BuiltObjects.FindBuiltObjectById(refuelLocationId);
                    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Cargo != null && builtObject.Empire != null)
                    {
                        int num = builtObject.Cargo.IndexOf(new Resource(refuelResourceId), builtObject.Empire.EmpireId);
                        if (num >= 0)
                        {
                            Cargo cargo = builtObject.Cargo[num];
                            if (cargo != null)
                            {
                                if (cargo.Reserved >= refuelAmount)
                                {
                                    cargo.Reserved -= refuelAmount;
                                }
                                else
                                {
                                    cargo.Reserved = 0;
                                }
                                result = true;
                            }
                        }
                    }
                }
                else
                {
                    Habitat habitat = null;
                    if (refuelLocationId >= 0 && refuelLocationId < _Galaxy.Habitats.Count)
                    {
                        habitat = _Galaxy.Habitats[refuelLocationId];
                    }
                    if (habitat != null && !habitat.HasBeenDestroyed && habitat.Cargo != null && habitat.Empire != null)
                    {
                        int num2 = habitat.Cargo.IndexOf(new Resource(refuelResourceId), habitat.Empire.EmpireId);
                        if (num2 >= 0)
                        {
                            Cargo cargo2 = habitat.Cargo[num2];
                            if (cargo2 != null)
                            {
                                if (cargo2.Reserved >= refuelAmount)
                                {
                                    cargo2.Reserved -= refuelAmount;
                                }
                                else
                                {
                                    cargo2.Reserved = 0;
                                }
                                result = true;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public int InitiateRefuelData(StellarObject refuelLocation)
        {
            CheckCancelRefuelData();
            if (refuelLocation != null && refuelLocation.Cargo != null && refuelLocation.Empire != null && Empire != null && FuelType != null)
            {
                int num = refuelLocation.Cargo.IndexOf(FuelType, refuelLocation.Empire.EmpireId);
                if (num >= 0)
                {
                    Cargo cargo = refuelLocation.Cargo[num];
                    if (cargo != null && cargo.Available > 0)
                    {
                        int num2 = Math.Min(cargo.Available, FuelCapacity);
                        cargo.Reserved += num2;
                        _RefuelResourceId = FuelType.ResourceID;
                        _RefuelAmount = (short)num2;
                        if (refuelLocation is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)refuelLocation;
                            _RefuelLocationId = builtObject.BuiltObjectID;
                            _RefuelLocationIsBuiltObject = true;
                        }
                        else if (refuelLocation is Habitat)
                        {
                            Habitat habitat = (Habitat)refuelLocation;
                            _RefuelLocationId = habitat.HabitatIndex;
                            _RefuelLocationIsBuiltObject = false;
                        }
                        return num2;
                    }
                }
            }
            return 0;
        }

        private void ConsumeFuel(double timePassed)
        {
            double num = 0.0;
            if (CurrentSpeed > (float)TopSpeed)
            {
                num = (double)WarpSpeedFuelBurn * timePassed;
            }
            else if (TargetSpeed > CruiseSpeed && TargetSpeed <= TopSpeed)
            {
                num = (double)TopSpeedFuelBurn * timePassed;
            }
            else if (TargetSpeed >= Galaxy.MovementImpulseSpeed && TargetSpeed <= CruiseSpeed)
            {
                num = (double)CruiseSpeedFuelBurn * timePassed;
            }
            else if (TargetSpeed > 0 && TargetSpeed <= Galaxy.MovementImpulseSpeed)
            {
                num = (double)ImpulseSpeedFuelBurn * timePassed;
            }
            if (ShipGroup != null)
            {
                num /= ShipGroup.ShipEnergyUsageBonus;
            }
            num /= CaptainShipEnergyUsageBonus;
            CurrentEnergy -= num;
        }

        private void UpdatePosition()
        {
            BaconBuiltObject.UpdatePosition(this);
        }

        public void UpdateIndexesForMovement(int oldIndexX, int oldIndexY, Galaxy galaxy, bool performIndexCheck)
        {
            EnsureWithinGalaxy();
            int x = (int)Xpos / Galaxy.IndexSize;
            int y = (int)Ypos / Galaxy.IndexSize;
            Galaxy.CorrectIndexCoords(ref x, ref y);
            if (oldIndexX != x || oldIndexY != y)
            {
                while (galaxy.BuiltObjectIndex[oldIndexX][oldIndexY].Contains(this))
                {
                    galaxy.BuiltObjectIndex[oldIndexX][oldIndexY].Remove(this);
                }
                galaxy.BuiltObjectIndex[x][y].Add(this);
            }
            if (performIndexCheck && !HasBeenDestroyed && !galaxy.BuiltObjectIndex[x][y].Contains(this))
            {
                galaxy.BuiltObjectIndex[x][y].Add(this);
            }
        }

        private bool CheckForArrival(double currentDistance, double distanceTravelled, bool relativeToParent, double parentXPos, double parentYPos, double targetX, double targetY, double targetSize, out double distanceNotTravelled)
        {
            distanceNotTravelled = 0.0;
            double num = ((DockedAt == null) ? (targetSize + (double)Galaxy.MovementPrecision) : (targetSize + (double)Galaxy.ImpulseMargin));
            double num2 = currentDistance - distanceTravelled;
            double currentPositionX = Xpos + Math.Cos(Heading) * distanceTravelled;
            double currentPositionY = Ypos + Math.Sin(Heading) * distanceTravelled;
            if (CheckWhetherArrived(currentPositionX, currentPositionY, targetX, targetY, num))
            {
                if (relativeToParent)
                {
                    distanceNotTravelled = num - num2;
                    distanceTravelled = ((!(num2 < num * -1.0)) ? (currentDistance - num2) : (distanceTravelled - distanceNotTravelled));
                    ParentOffsetX += Math.Cos(Heading) * distanceTravelled;
                    ParentOffsetY += Math.Sin(Heading) * distanceTravelled;
                    Xpos = parentXPos + ParentOffsetX;
                    Ypos = parentYPos + ParentOffsetY;
                    if (!_ExecutingShipGroupCommand)
                    {
                        Mission.CompleteCommand();
                        FirstExecutionOfCommand = true;
                        Command command = Mission.FastPeekCurrentCommand();
                        if (command != null)
                        {
                            if (command.Action != CommandAction.MoveTo && command.Action != CommandAction.Attack && command.Action != CommandAction.HyperTo && command.Action != CommandAction.ConditionalHyperTo && command.Action != CommandAction.SprintTo)
                            {
                                TargetSpeed = 0;
                                PreferredSpeed = 0f;
                            }
                        }
                        else
                        {
                            TargetSpeed = 0;
                            PreferredSpeed = 0f;
                        }
                    }
                    return true;
                }
                distanceNotTravelled = num - num2;
                distanceTravelled = ((!(num2 < num * -1.0)) ? (currentDistance - num2) : (distanceTravelled - distanceNotTravelled));
                Xpos += Math.Cos(Heading) * distanceTravelled;
                Ypos += Math.Sin(Heading) * distanceTravelled;
                if (!_ExecutingShipGroupCommand)
                {
                    Mission.CompleteCommand();
                    FirstExecutionOfCommand = true;
                    Command command2 = Mission.FastPeekCurrentCommand();
                    if (command2 != null)
                    {
                        if (command2.Action != CommandAction.MoveTo && command2.Action != CommandAction.Attack && command2.Action != CommandAction.HyperTo && command2.Action != CommandAction.ConditionalHyperTo && command2.Action != CommandAction.SprintTo)
                        {
                            TargetSpeed = 0;
                            PreferredSpeed = 0f;
                        }
                    }
                    else
                    {
                        TargetSpeed = 0;
                        PreferredSpeed = 0f;
                    }
                }
                return true;
            }
            return false;
        }

        private void CheckForDeceleration(double currentDistance, double distanceTravelled)
        {
            int num = (int)((double)((float)Math.Max((short)1, CruiseSpeed) / AccelerationRate) * ((double)Math.Max((short)1, CruiseSpeed) * 0.5) + (double)CurrentSpeed);
            if (currentDistance <= distanceTravelled + (double)num + (double)Galaxy.MovementPrecision)
            {
                Command command = Mission.FastPeekCurrentCommand();
                Command command2 = Mission.ShowNextCommand();
                if (command2 != null)
                {
                    if (command2.Action == CommandAction.MoveTo || command2.Action == CommandAction.Attack || command2.Action == CommandAction.HyperTo || command2.Action == CommandAction.ConditionalHyperTo || command2.Action == CommandAction.SprintTo)
                    {
                        return;
                    }
                    TargetSpeed = (int)((currentDistance - (distanceTravelled + (double)Galaxy.MovementPrecision)) / (double)num * (double)CruiseSpeed);
                    if (TargetSpeed < Galaxy.MovementImpulseSpeed)
                    {
                        TargetSpeed = Galaxy.MovementImpulseSpeed;
                    }
                    if (command == null)
                    {
                        return;
                    }
                    if (command.TargetHabitat != null && ParentHabitat == null)
                    {
                        int num2 = command.TargetHabitat.OrbitSpeed;
                        if (command.TargetHabitat.Parent != null)
                        {
                            num2 += command.TargetHabitat.Parent.OrbitSpeed;
                        }
                        TargetSpeed = Math.Max(TargetSpeed, num2 + Galaxy.MovementImpulseSpeed);
                    }
                    else if (command.TargetBuiltObject != null && command.TargetBuiltObject.ParentHabitat != null && ParentBuiltObject == null)
                    {
                        int num3 = command.TargetBuiltObject.ParentHabitat.OrbitSpeed;
                        if (command.TargetBuiltObject.ParentHabitat.Parent != null)
                        {
                            num3 += command.TargetBuiltObject.ParentHabitat.Parent.OrbitSpeed;
                        }
                        TargetSpeed = Math.Max(TargetSpeed, num3 + Galaxy.MovementImpulseSpeed);
                    }
                    else if (command.TargetBuiltObject != null && command.TargetBuiltObject.ParentBuiltObject != null && command.TargetBuiltObject.ParentBuiltObject.ParentHabitat != null && ParentBuiltObject == null)
                    {
                        int num4 = command.TargetBuiltObject.ParentBuiltObject.ParentHabitat.OrbitSpeed;
                        if (command.TargetBuiltObject.ParentBuiltObject.ParentHabitat.Parent != null)
                        {
                            num4 += command.TargetBuiltObject.ParentBuiltObject.ParentHabitat.Parent.OrbitSpeed;
                        }
                        TargetSpeed = Math.Max(TargetSpeed, num4 + Galaxy.MovementImpulseSpeed);
                    }
                    return;
                }
                TargetSpeed = (int)((currentDistance - (distanceTravelled + (double)Galaxy.MovementPrecision)) / (double)num * (double)CruiseSpeed);
                if (TargetSpeed < Galaxy.MovementImpulseSpeed)
                {
                    TargetSpeed = Galaxy.MovementImpulseSpeed;
                }
                if (command == null)
                {
                    return;
                }
                if (command.TargetHabitat != null && ParentHabitat == null)
                {
                    int num5 = command.TargetHabitat.OrbitSpeed;
                    if (command.TargetHabitat.Parent != null)
                    {
                        num5 += command.TargetHabitat.Parent.OrbitSpeed;
                    }
                    TargetSpeed = Math.Max(TargetSpeed, num5 + Galaxy.MovementImpulseSpeed);
                }
                else if (command.TargetBuiltObject != null && command.TargetBuiltObject.ParentHabitat != null && ParentBuiltObject == null)
                {
                    int num6 = command.TargetBuiltObject.ParentHabitat.OrbitSpeed;
                    if (command.TargetBuiltObject.ParentHabitat.Parent != null)
                    {
                        num6 += command.TargetBuiltObject.ParentHabitat.Parent.OrbitSpeed;
                    }
                    TargetSpeed = Math.Max(TargetSpeed, num6 + Galaxy.MovementImpulseSpeed);
                }
            }
            else
            {
                TargetSpeed = CruiseSpeed;
            }
        }

        private void AccelerateToTargetSpeed(double timePassed)
        {
            if ((double)TargetSpeed > (double)CurrentSpeed)
            {
                double num = (double)AccelerationRate * timePassed;
                if ((double)CurrentSpeed + num >= (double)TargetSpeed)
                {
                    CurrentSpeed = TargetSpeed;
                }
                else
                {
                    CurrentSpeed += (float)num;
                }
            }
            else if ((double)TargetSpeed < (double)CurrentSpeed)
            {
                double num2 = Math.Max(1f, AccelerationRate);
                double num3 = num2 * timePassed;
                if ((double)CurrentSpeed - num3 < (double)TargetSpeed)
                {
                    CurrentSpeed = TargetSpeed;
                }
                else
                {
                    CurrentSpeed -= (float)num3;
                }
            }
            if (CurrentSpeed < 0f)
            {
                CurrentSpeed = 0f;
            }
        }

        private double GetCurrentTurnRate()
        {
            return GetCurrentTurnRate(CurrentSpeed);
        }

        private double GetCurrentTurnRate(double speed)
        {
            double num = TurnRate;
            if (speed <= (double)(Galaxy.MovementImpulseSpeed * 2))
            {
                num *= 3.0;
            }
            else if (speed <= (double)(Galaxy.MovementImpulseSpeed * 3))
            {
                num *= 2.3;
            }
            else if (speed <= (double)(Galaxy.MovementImpulseSpeed * 4))
            {
                num *= 1.6;
            }
            if (ShipGroup != null)
            {
                num *= ShipGroup.ShipManeuveringBonus;
            }
            return num * CaptainShipManeuveringBonus;
        }

        private bool WillMeetDestination(double destinationX, double destinationY, double speed)
        {
            if (TurnDirection == TurnDirection.StraightAhead)
            {
                return true;
            }
            double currentTurnRate = GetCurrentTurnRate(speed);
            double num = Math.PI / currentTurnRate * speed;
            double num2 = num / Math.PI;
            double num3 = 0.0;
            switch (TurnDirection)
            {
                case TurnDirection.Left:
                    num3 = (double)Heading - Math.PI / 2.0;
                    break;
                case TurnDirection.Right:
                    num3 = (double)Heading + Math.PI / 2.0;
                    break;
            }
            double num4 = Math.Cos(num3) * num2;
            double num5 = Math.Tan(num3) * num4;
            double x = Xpos + num4;
            double y = Ypos + num5;
            double num6 = _Galaxy.CalculateDistance(x, y, destinationX, destinationY);
            if (num6 < num2)
            {
                return false;
            }
            double num7 = Galaxy.DetermineAngle(Xpos, Ypos, destinationX, destinationY);
            double num8 = (double)Heading - num7;
            double num9 = Math.Abs(num8 / currentTurnRate * speed);
            double num10 = _Galaxy.CalculateDistance(Xpos, Ypos, destinationX, destinationY);
            if (num9 > num10 * 1.4)
            {
                return false;
            }
            return true;
        }

        private void CalculateCurrentHeading(double timePassed)
        {
            if (Heading == TargetHeading)
            {
                return;
            }
            HeadingChanged = true;
            double num = GetCurrentTurnRate() * timePassed;
            double num2 = TargetHeading - Heading;
            if (num2 > Math.PI)
            {
                num2 -= Math.PI * 2.0;
            }
            else if (num2 < -Math.PI)
            {
                num2 += Math.PI * 2.0;
            }
            if ((num2 < 0.0 && num2 > -Math.PI) || (num2 >= Math.PI && num2 < Math.PI * 2.0))
            {
                if (Math.Abs(num2) < Math.Abs(num))
                {
                    Heading = TargetHeading;
                    _TurnDirection = TurnDirection.StraightAhead;
                }
                else
                {
                    Heading -= (float)num;
                    _TurnDirection = TurnDirection.Left;
                }
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit((double)Heading <= -Math.PI, 20, ref iterationCount))
                {
                    Heading = (float)IncreaseAngle(Heading);
                }
            }
            else
            {
                if (Math.Abs(num2) < Math.Abs(num))
                {
                    Heading = TargetHeading;
                    _TurnDirection = TurnDirection.StraightAhead;
                }
                else
                {
                    Heading += (float)num;
                    _TurnDirection = TurnDirection.Right;
                }
                int iterationCount2 = 0;
                while (Galaxy.ConditionCheckLimit((double)Heading >= Math.PI, 20, ref iterationCount2))
                {
                    Heading = (float)ReduceAngle(Heading);
                }
            }
        }

        private double ReduceAngle(double currentangle)
        {
            if (currentangle >= Math.PI)
            {
                currentangle -= Math.PI * 2.0;
            }
            return currentangle;
        }

        private double IncreaseAngle(double currentangle)
        {
            if (currentangle <= -Math.PI)
            {
                currentangle += Math.PI * 2.0;
            }
            return currentangle;
        }

        public void QueueMission(BuiltObjectMissionType missionType, object target, object target2, BuiltObjectMissionPriority priority)
        {
            QueueMission(missionType, target, target2, null, null, null, null, -2000000001.0, -2000000001.0, -1L, priority);
        }

        public void QueueMission(BuiltObjectMissionType missionType, object target, object target2, TroopList troops, BuiltObjectMissionPriority priority)
        {
            QueueMission(missionType, target, target2, null, troops, null, null, -2000000001.0, -2000000001.0, -1L, priority);
        }

        public void QueueMission(BuiltObjectMissionType missionType, object target, object target2, PopulationList population, BuiltObjectMissionPriority priority)
        {
            QueueMission(missionType, target, target2, null, null, population, null, -2000000001.0, -2000000001.0, -1L, priority);
        }

        public void QueueMission(BuiltObjectMissionType missionType, object target, object target2, double x, double y, BuiltObjectMissionPriority priority)
        {
            QueueMission(missionType, target, target2, null, null, null, null, x, y, -1L, priority);
        }

        public void QueueMission(BuiltObjectMissionType missionType, object target, object target2, Design design, double x, double y, BuiltObjectMissionPriority priority)
        {
            QueueMission(missionType, target, target2, null, null, null, design, x, y, -1L, priority);
        }

        public void QueueMission(BuiltObjectMissionType missionType, object target, object target2, Design design, BuiltObjectMissionPriority priority)
        {
            QueueMission(missionType, target, target2, null, null, null, design, -2000000001.0, -2000000001.0, -1L, priority);
        }

        public void QueueMission(BuiltObjectMissionType missionType, object target, object target2, double x, double y, long starDate, BuiltObjectMissionPriority priority)
        {
            QueueMission(missionType, target, target2, null, null, null, null, x, y, starDate, priority);
        }

        public void QueueMission(BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, TroopList troops, PopulationList population, Design design, double x, double y, long starDate, BuiltObjectMissionPriority priority)
        {
            if (Role != BuiltObjectRole.Base)
            {
                BuiltObjectMission item = new BuiltObjectMission(_Galaxy, this, missionType, target, target2, cargo, troops, population, design, x, y, starDate, priority, allowReprocessing: true, allowBuiltObjectChanges: false);
                _SubsequentMissions.Add(item);
            }
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, BuiltObjectMissionPriority priority)
        {
            AssignMission(missionType, target, target2, null, null, null, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: true);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, BuiltObjectMissionPriority priority, bool manuallyAssigned)
        {
            AssignMission(missionType, target, target2, null, null, null, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: true, manuallyAssigned);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, Design design, BuiltObjectMissionPriority priority)
        {
            AssignMission(missionType, target, target2, null, null, null, design, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: true);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, Design design, BuiltObjectMissionPriority priority, bool manuallyAssigned)
        {
            AssignMission(missionType, target, target2, null, null, null, design, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: true, manuallyAssigned);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, Design design, double x, double y, BuiltObjectMissionPriority priority)
        {
            AssignMission(missionType, target, target2, null, null, null, design, x, y, -1L, priority, allowReprocessing: true);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, Design design, double x, double y, BuiltObjectMissionPriority priority, bool manuallyAssigned)
        {
            AssignMission(missionType, target, target2, null, null, null, design, x, y, -1L, priority, allowReprocessing: true, manuallyAssigned);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, BuiltObjectMissionPriority priority)
        {
            AssignMission(missionType, target, target2, cargo, null, null, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: true);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, PopulationList population, BuiltObjectMissionPriority priority)
        {
            AssignMission(missionType, target, target2, null, null, population, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: true);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, TroopList troops, BuiltObjectMissionPriority priority)
        {
            AssignMission(missionType, target, target2, null, troops, null, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: true);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, TroopList troops, BuiltObjectMissionPriority priority, bool manuallyAssigned)
        {
            AssignMission(missionType, target, target2, null, troops, null, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: true, manuallyAssigned);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, double x, double y, BuiltObjectMissionPriority priority)
        {
            AssignMission(missionType, target, target2, null, null, null, null, x, y, -1L, priority, allowReprocessing: true);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, double x, double y, BuiltObjectMissionPriority priority, bool manuallyAssigned)
        {
            AssignMission(missionType, target, target2, null, null, null, null, x, y, -1L, priority, allowReprocessing: true, manuallyAssigned);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, double x, double y, long starDate, BuiltObjectMissionPriority priority, bool allowReprocessing)
        {
            AssignMission(missionType, target, target2, null, null, null, null, x, y, starDate, priority, allowReprocessing);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, TroopList troops, PopulationList population, Design design, double x, double y, long starDate, BuiltObjectMissionPriority priority, bool allowReprocessing)
        {
            AssignMission(missionType, target, target2, cargo, troops, population, design, x, y, starDate, priority, allowReprocessing, manuallyAssigned: false);
        }

        public void AssignMission(BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, TroopList troops, PopulationList population, Design design, double x, double y, long starDate, BuiltObjectMissionPriority priority, bool allowReprocessing, bool manuallyAssigned)
        {
            if (Role == BuiltObjectRole.Base || !BaconBuiltObject.AssignMissionCheckPreconditions(this))
            {
                return;
            }
            if (manuallyAssigned)
            {
                RevertMission = null;
            }
            _MissionCompleteMessageSent = false;
            HyperEnterStartAnimation = false;
            HyperExitStartAnimation = false;
            _HyperjumpAboutToEnter = false;
            _HyperjumpPrepare = false;
            switch (missionType)
            {
                case BuiltObjectMissionType.Attack:
                case BuiltObjectMissionType.WaitAndAttack:
                case BuiltObjectMissionType.WaitAndBombard:
                case BuiltObjectMissionType.Bombard:
                case BuiltObjectMissionType.Capture:
                case BuiltObjectMissionType.Raid:
                    BaconSpaceBattleStats.AddLatestCombatStats(this, BattleStats);
                    BattleStats = new SpaceBattleStats();
                    break;
            }
            if (Mission != null && (Mission.Type == BuiltObjectMissionType.Attack || Mission.Type == BuiltObjectMissionType.Capture || Mission.Type == BuiltObjectMissionType.Raid))
            {
                if (Mission.TargetBuiltObject != null)
                {
                    Mission.TargetBuiltObject.Pursuers.Remove(this);
                }
                if (Mission.TargetCreature != null)
                {
                    Mission.TargetCreature.Pursuers.Remove(this);
                }
                _ColonyToAttack = null;
            }
            if (IsDeployed)
            {
                InitiateUndeploy();
            }
            if (missionType == BuiltObjectMissionType.Attack || missionType == BuiltObjectMissionType.Capture || missionType == BuiltObjectMissionType.Raid)
            {
                if (target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)target;
                    if (!builtObject.Pursuers.Contains(this))
                    {
                        builtObject.Pursuers.Add(this);
                    }
                }
                else if (target is Creature)
                {
                    Creature creature = (Creature)target;
                    if (!creature.Pursuers.Contains(this))
                    {
                        creature.Pursuers.Add(this);
                    }
                }
                else if (target is Fighter)
                {
                    Fighter fighter = (Fighter)target;
                    if (!fighter.Pursuers.Contains(this))
                    {
                        fighter.Pursuers.Add(this);
                    }
                }
            }
            if (missionType == BuiltObjectMissionType.Refuel && target != null && target is StellarObject)
            {
                InitiateRefuelData((StellarObject)target);
            }
            BuiltObjectMission builtObjectMission = new BuiltObjectMission(_Galaxy, this, missionType, target, target2, cargo, troops, population, design, x, y, starDate, priority, allowReprocessing);
            builtObjectMission.ManuallyAssigned = manuallyAssigned;
            Mission = builtObjectMission;
            FirstExecutionOfCommand = true;
            if (Empire != null)
            {
                int num = Empire.AttackRangePatrol;
                int num2 = Empire.AttackRangeEscort;
                int num3 = Empire.AttackRangeAttack;
                int num4 = Empire.AttackRangeOther;
                if (!IsAutoControlled || manuallyAssigned)
                {
                    num = Empire.AttackRangePatrolManual;
                    num2 = Empire.AttackRangeEscortManual;
                    num3 = Empire.AttackRangeAttackManual;
                    num4 = Empire.AttackRangeOtherManual;
                }
                if (missionType == BuiltObjectMissionType.Patrol && num >= 0)
                {
                    AttackRangeSquared = (float)num * (float)num;
                }
                else if (missionType == BuiltObjectMissionType.Escort && num2 >= 0)
                {
                    AttackRangeSquared = (float)num2 * (float)num2;
                }
                else if ((missionType == BuiltObjectMissionType.Attack || missionType == BuiltObjectMissionType.Bombard || missionType == BuiltObjectMissionType.WaitAndAttack || missionType == BuiltObjectMissionType.WaitAndBombard || missionType == BuiltObjectMissionType.Capture || missionType == BuiltObjectMissionType.Raid) && num3 >= 0)
                {
                    AttackRangeSquared = (float)num3 * (float)num3;
                }
                else if (num4 >= 0)
                {
                    AttackRangeSquared = (float)num4 * (float)num4;
                }
                else if (AttackRangeSquared < 0f && IsAutoControlled)
                {
                    AttackRangeSquared = 2.304E+09f;
                }
            }
        }

        private void SetAttackRangeWhenNoMission()
        {
            if ((Mission == null || Mission.Type == BuiltObjectMissionType.Undefined) && Empire != null)
            {
                int num = Empire.AttackRangeOther;
                if (!IsAutoControlled)
                {
                    num = Empire.AttackRangeOtherManual;
                }
                if (num >= 0)
                {
                    AttackRangeSquared = (float)num * (float)num;
                }
                else if (AttackRangeSquared < 0f && IsAutoControlled)
                {
                    AttackRangeSquared = 2.304E+09f;
                }
            }
        }

        public StellarObject SelectTargetToAttack(Galaxy galaxy, object objectToDefend)
        {
            if (!(objectToDefend is BuiltObject) && !(objectToDefend is Habitat) && !(objectToDefend is ShipGroup))
            {
                throw new ApplicationException("ObjectToDefend type is invalid");
            }
            int[] threatLevel;
            StellarObject[] array = galaxy.EvaluateThreats(objectToDefend, out threatLevel);
            return array[0];
        }

        private void PerformEnergyCollection(double timePassed)
        {
            if (!(CurrentSpeed <= 0f) || !IsEnergyCollector)
            {
                return;
            }
            if (NearestSystemStar != null)
            {
                double num = (double)Galaxy.MaxSolarSystemSize + 500.0;
                if (NearestSystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    num = (double)(NearestSystemStar.Diameter / 2) + 500.0;
                }
                double num2 = _Galaxy.CalculateDistance(Xpos, Ypos, NearestSystemStar.Xpos, NearestSystemStar.Ypos);
                if (num2 <= num)
                {
                    double num3 = (double)EnergyCollection * (double)(int)NearestSystemStar.SolarRadiation * 10.0 * timePassed / 100.0;
                    double num4 = (double)EnergyCollection * (double)(int)NearestSystemStar.MicrowaveRadiation * 10.0 * timePassed / 100.0;
                    double num5 = (double)EnergyCollection * (double)(int)NearestSystemStar.XrayRadiation * 10.0 * timePassed / 100.0;
                    double num6 = num3 + num4 + num5;
                    num6 *= (num - num2 + 2000.0) / num;
                    CurrentEnergy += num6;
                    if (CurrentEnergy > (double)ReactorStorageCapacity)
                    {
                        CurrentEnergy = ReactorStorageCapacity;
                    }
                }
            }
            else if (_HyperjumpDisabledLocation)
            {
                double num7 = 100.0;
                double num8 = (double)EnergyCollection * num7 * timePassed / 100.0;
                CurrentEnergy += num8;
                if (CurrentEnergy > (double)ReactorStorageCapacity)
                {
                    CurrentEnergy = ReactorStorageCapacity;
                }
            }
        }

        private void IndustrialProcessing(double timePassed, Galaxy galaxy, DateTime time)
        {
            if (Empire == null)
            {
                return;
            }
            Empire actualEmpire = ActualEmpire;
            if (actualEmpire == null)
            {
                return;
            }
            _DoingMining = false;
            _DoingGasMining = false;
            _DoingConstruction = false;
            int num = 0;
            if (Role == BuiltObjectRole.Base)
            {
                num = 500;
            }
            if (CurrentSpeed == 0f)
            {
                if (EnergyToFuelRate > 0 && NearestSystemStar != null)
                {
                    double num2 = 0.01 * ((double)(int)NearestSystemStar.SolarRadiation + (double)(int)NearestSystemStar.MicrowaveRadiation + (double)(int)NearestSystemStar.XrayRadiation);
                    double num3 = _Galaxy.CalculateDistance(Xpos, Ypos, NearestSystemStar.Xpos, NearestSystemStar.Ypos);
                    double num4 = 1.0 - num3 / (double)Galaxy.MaxSolarSystemSize;
                    num4 *= num4;
                    double num5 = num2 * num4;
                    double num6 = EnergyToFuelRate;
                    double num7 = num6 * num5;
                    double num8 = num7 * timePassed;
                    if (Cargo != null)
                    {
                        double num9 = (double)CargoSpace / (double)CargoCapacity;
                        if (num9 > 0.25)
                        {
                            _ = _Galaxy.ResourceSystem.FuelResources.Count;
                            int num10 = Math.Min(120000, CargoCapacity / 4);
                            int amount = (int)(num8 / (double)_Galaxy.ResourceSystem.FuelResources.Count);
                            for (int i = 0; i < _Galaxy.ResourceSystem.FuelResources.Count; i++)
                            {
                                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.FuelResources[i];
                                if (resourceDefinition != null)
                                {
                                    Resource resource = new Resource(resourceDefinition.ResourceID);
                                    int totalResourceAmount = Cargo.GetTotalResourceAmount(resource, actualEmpire.EmpireId);
                                    if (totalResourceAmount < num10)
                                    {
                                        Cargo.Add(new Cargo(resource, amount, actualEmpire.EmpireId));
                                    }
                                }
                            }
                        }
                    }
                }
                if ((ExtractionGas > 0 || ExtractionLuxury > 0 || ExtractionMine > 0) && ParentHabitat != null && (ParentHabitat.Empire == null || ParentHabitat.Empire == _Galaxy.IndependentEmpire))
                {
                    HabitatResourceList habitatResourceList = new HabitatResourceList();
                    if (ParentHabitat.Resources != null)
                    {
                        habitatResourceList = ParentHabitat.Resources.Clone();
                    }
                    if (IsResourceExtractor && (SubRole != BuiltObjectSubRole.ResupplyShip || IsDeployed))
                    {
                        int num11 = CargoCapacity - num;
                        if (habitatResourceList != null && habitatResourceList.Count > 0)
                        {
                            num11 = Math.Min(120000, (int)((double)(CargoCapacity - num) / (double)habitatResourceList.Count + 0.99999));
                            int num12 = 0;
                            if (ExtractionLuxury <= 0)
                            {
                                num12 += habitatResourceList.CountLuxuryResources();
                            }
                            if (ExtractionGas <= 0)
                            {
                                num12 += habitatResourceList.CountGasResources();
                            }
                            if (ExtractionMine <= 0)
                            {
                                num12 += habitatResourceList.CountMineralResources();
                            }
                            if (num12 > 0)
                            {
                                int num13 = Math.Max(0, habitatResourceList.Count - num12);
                                num11 = ((num13 > 0) ? Math.Min(120000, (CargoCapacity - num) / num13) : 0);
                            }
                        }
                        if (Cargo != null && CargoCapacity > 0 && CargoSpace <= num)
                        {
                            bool flag = false;
                            int num14 = CargoCapacity / 8;
                            byte item = byte.MaxValue;
                            for (int j = 0; j < _Galaxy.ResourceSystem.FuelResources.Count; j++)
                            {
                                ResourceDefinition resourceDefinition2 = _Galaxy.ResourceSystem.FuelResources[j];
                                if (resourceDefinition2 != null && habitatResourceList.IndexOf(resourceDefinition2.ResourceID, 0) >= 0)
                                {
                                    int num15 = 0;
                                    int num16 = Cargo.IndexOf(new Resource(resourceDefinition2.ResourceID), actualEmpire);
                                    if (num16 >= 0)
                                    {
                                        num15 = Cargo[num16].Amount;
                                    }
                                    if (num15 < num14)
                                    {
                                        flag = true;
                                        item = resourceDefinition2.ResourceID;
                                    }
                                }
                            }
                            if (flag)
                            {
                                List<byte> list = new List<byte>();
                                list.Add(item);
                                for (int k = 0; k < Cargo.Count; k++)
                                {
                                    if (Cargo[k].CommodityIsResource && !list.Contains(Cargo[k].CommodityResource.ResourceID) && Cargo[k].Available > 0)
                                    {
                                        Cargo[k].Amount -= Cargo[k].Available / 2;
                                    }
                                }
                            }
                        }
                        if (Cargo != null && CargoSpace > num)
                        {
                            double num17 = 1.0;
                            if (actualEmpire != null && actualEmpire != _Galaxy.IndependentEmpire)
                            {
                                num17 *= 1.0 + Empire.ResourceExtractionBonus;
                                num17 *= actualEmpire.MiningRate;
                            }
                            if (actualEmpire != null && actualEmpire.Leader != null)
                            {
                                num17 *= 1.0 + (double)actualEmpire.Leader.MiningRate / 100.0;
                            }
                            if (Characters != null && Characters.Count > 0)
                            {
                                int highestSkillLevel = Characters.GetHighestSkillLevel(CharacterSkillType.MiningRate);
                                num17 *= 1.0 + (double)highestSkillLevel / 100.0;
                            }
                            bool flag2 = false;
                            double num18 = 1.0;
                            double num19 = (double)(CargoSpace - num) / (double)CargoCapacity;
                            if (num19 < 0.25 && (SubRole == BuiltObjectSubRole.GasMiningStation || SubRole == BuiltObjectSubRole.MiningStation))
                            {
                                if (ExtractionGas > 0)
                                {
                                    double val = (double)ExtractionGas * num17 * timePassed;
                                    val = Math.Min(val, timePassed * BaconEmpire.MaxResourceExtractionRate(this, 2));
                                    for (int l = 0; l < _Galaxy.ResourceSystem.FuelResources.Count; l++)
                                    {
                                        ResourceDefinition resourceDefinition3 = _Galaxy.ResourceSystem.FuelResources[l];
                                        if (resourceDefinition3 == null || resourceDefinition3.Group != ResourceGroup.Gas)
                                        {
                                            continue;
                                        }
                                        int num20 = habitatResourceList.IndexOf(resourceDefinition3.ResourceID, 0);
                                        if (num20 >= 0 && _Galaxy.ResourceCurrentPrices[resourceDefinition3.ResourceID] > num18)
                                        {
                                            Resource resource2 = new Resource(resourceDefinition3.ResourceID);
                                            int num21 = Cargo.IndexOf(resource2, actualEmpire);
                                            double num22 = 0.0;
                                            if (num21 >= 0)
                                            {
                                                num22 = (double)Cargo[num21].Amount / (double)(CargoCapacity - 200);
                                            }
                                            if (num22 < 0.3)
                                            {
                                                int num23 = habitatResourceList[num20].Extract(val);
                                                actualEmpire.Counters.MiningExtractionGas += num23;
                                                Cargo cargo = new Cargo(resource2, num23, actualEmpire);
                                                Cargo.Add(cargo);
                                                flag2 = true;
                                                _DoingGasMining = true;
                                            }
                                        }
                                    }
                                }
                                if (ExtractionMine > 0)
                                {
                                    double val2 = (double)ExtractionMine * num17 * timePassed;
                                    val2 = Math.Min(val2, timePassed * BaconEmpire.MaxResourceExtractionRate(this, 0));
                                    for (int m = 0; m < _Galaxy.ResourceSystem.FuelResources.Count; m++)
                                    {
                                        ResourceDefinition resourceDefinition4 = _Galaxy.ResourceSystem.FuelResources[m];
                                        if (resourceDefinition4 == null || resourceDefinition4.Group != ResourceGroup.Mineral)
                                        {
                                            continue;
                                        }
                                        int num24 = habitatResourceList.IndexOf(resourceDefinition4.ResourceID, 0);
                                        if (num24 < 0 || !(_Galaxy.ResourceCurrentPrices[resourceDefinition4.ResourceID] > num18))
                                        {
                                            continue;
                                        }
                                        Resource resource3 = new Resource(resourceDefinition4.ResourceID);
                                        int num25 = Cargo.IndexOf(resource3, actualEmpire);
                                        double num26 = 0.0;
                                        if (num25 >= 0)
                                        {
                                            num26 = (double)Cargo[num25].Amount / (double)(CargoCapacity - 200);
                                        }
                                        if (num26 < 0.3)
                                        {
                                            int num27 = habitatResourceList[num24].Extract(val2);
                                            actualEmpire.Counters.MiningExtractionStrategic += num27;
                                            if (habitatResourceList[num24].ColonyManufacturingLevel > 0)
                                            {
                                                actualEmpire.Counters.MiningExtractionColonyManufactured += num27;
                                            }
                                            Cargo cargo = new Cargo(resource3, num27, actualEmpire);
                                            Cargo.Add(cargo);
                                            flag2 = true;
                                            _DoingMining = true;
                                        }
                                    }
                                }
                            }
                            if (!flag2)
                            {
                                if (ExtractionLuxury > 0)
                                {
                                    double val3 = (double)ExtractionLuxury * num17 * timePassed;
                                    val3 = Math.Min(val3, timePassed * BaconEmpire.MaxResourceExtractionRate(this, 1));
                                    for (int n = 0; n < habitatResourceList.Count; n++)
                                    {
                                        HabitatResource habitatResource = habitatResourceList[n];
                                        if (habitatResource != null && habitatResource.Group == ResourceGroup.Luxury)
                                        {
                                            Resource resource4 = new Resource(habitatResource.ResourceID);
                                            if (Cargo.GetTotalResourceAmount(resource4, actualEmpire.EmpireId) < num11)
                                            {
                                                int num28 = habitatResource.Extract(val3);
                                                actualEmpire.Counters.MiningExtractionLuxury += num28;
                                                Cargo cargo = new Cargo(resource4, num28, actualEmpire);
                                                Cargo.Add(cargo);
                                            }
                                        }
                                    }
                                }
                                if (ExtractionGas > 0)
                                {
                                    _DoingGasMining = true;
                                    double val4 = (double)ExtractionGas * num17 * timePassed;
                                    val4 = Math.Min(val4, timePassed * BaconEmpire.MaxResourceExtractionRate(this, 2));
                                    if (SubRole == BuiltObjectSubRole.ResupplyShip)
                                    {
                                        for (int num29 = 0; num29 < habitatResourceList.Count; num29++)
                                        {
                                            HabitatResource habitatResource2 = habitatResourceList[num29];
                                            if (habitatResource2 != null && habitatResource2.IsFuel)
                                            {
                                                Resource resource5 = new Resource(habitatResource2.ResourceID);
                                                if (Cargo.GetTotalResourceAmount(resource5, actualEmpire.EmpireId) < num11)
                                                {
                                                    int num30 = habitatResource2.Extract(val4);
                                                    actualEmpire.Counters.MiningExtractionGas += num30;
                                                    Cargo cargo = new Cargo(resource5, num30, actualEmpire);
                                                    Cargo.Add(cargo);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int num31 = 0; num31 < habitatResourceList.Count; num31++)
                                        {
                                            HabitatResource habitatResource3 = habitatResourceList[num31];
                                            if (habitatResource3 != null && habitatResource3.Group == ResourceGroup.Gas)
                                            {
                                                Resource resource6 = new Resource(habitatResource3.ResourceID);
                                                if (Cargo.GetTotalResourceAmount(resource6, actualEmpire.EmpireId) < num11)
                                                {
                                                    int num32 = habitatResource3.Extract(val4);
                                                    actualEmpire.Counters.MiningExtractionGas += num32;
                                                    Cargo cargo = new Cargo(resource6, num32, actualEmpire);
                                                    Cargo.Add(cargo);
                                                }
                                            }
                                        }
                                    }
                                }
                                if (ExtractionMine > 0)
                                {
                                    _DoingMining = true;
                                    double val5 = (double)ExtractionMine * num17 * timePassed;
                                    val5 = Math.Min(val5, timePassed * BaconEmpire.MaxResourceExtractionRate(this, 0));
                                    for (int num33 = 0; num33 < habitatResourceList.Count; num33++)
                                    {
                                        HabitatResource habitatResource4 = habitatResourceList[num33];
                                        if (habitatResource4 == null || habitatResource4.Group != ResourceGroup.Mineral)
                                        {
                                            continue;
                                        }
                                        Resource resource7 = new Resource(habitatResource4.ResourceID);
                                        if (Cargo.GetTotalResourceAmount(resource7, actualEmpire.EmpireId) < num11)
                                        {
                                            int num34 = habitatResource4.Extract(val5);
                                            actualEmpire.Counters.MiningExtractionStrategic += num34;
                                            if (resource7.ColonyManufacturingLevel > 0)
                                            {
                                                actualEmpire.Counters.MiningExtractionColonyManufactured += num34;
                                            }
                                            Cargo cargo = new Cargo(resource7, num34, actualEmpire);
                                            Cargo.Add(cargo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (IsManufacturer && _ManufacturingQueue != null)
                {
                    _ManufacturingQueue.DoManufacturing(galaxy, time, galaxy.CurrentStarDate);
                }
                if (IsShipYard && ConstructionQueue != null)
                {
                    ConstructionQueue.DoConstruction(galaxy, time);
                }
            }
            else if (ConstructionQueue != null)
            {
                ConstructionQueue.ResetProcessTime(time);
            }
        }

        private bool CheckWhetherArrived(double currentPositionX, double currentPositionY, double targetPositionX, double targetPositionY, double allowance)
        {
            double num = _Galaxy.CalculateDistance(currentPositionX, currentPositionY, targetPositionX, targetPositionY);
            if (num <= allowance)
            {
                _LastPositionX = currentPositionX;
                _LastPositionY = currentPositionY;
                return true;
            }
            double num2 = _Galaxy.CalculateDistance(_LastPositionX, _LastPositionY, targetPositionX, targetPositionY);
            double num3 = _Galaxy.CalculateDistance(_LastPositionX, _LastPositionY, currentPositionX, currentPositionY);
            _LastPositionX = currentPositionX;
            _LastPositionY = currentPositionY;
            if (num2 <= num3)
            {
                return true;
            }
            return false;
        }

        int IComparable<StellarObject>.CompareTo(StellarObject other)
        {
            return SortTag.CompareTo(other.SortTag);
        }

        int IComparable<BuiltObject>.CompareTo(BuiltObject other)
        {
            return SortTag.CompareTo(other.SortTag);
        }

        int IComparable<Habitat>.CompareTo(Habitat other)
        {
            return SortTag.CompareTo(other.SortTag);
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == this)
            {
                return 0;
            }
            if (obj == null)
            {
                return 1;
            }
            if (obj is BuiltObject)
            {
                return SortTag.CompareTo(((BuiltObject)obj).SortTag);
            }
            if (obj is Habitat)
            {
                return SortTag.CompareTo(((Habitat)obj).SortTag);
            }
            if (obj is Creature)
            {
                return SortTag.CompareTo(((Creature)obj).SortTag);
            }
            return 0;
        }

        public override string ToString()
        {
            return Name;
        }

        int IComparable<Creature>.CompareTo(Creature other)
        {
            return SortTag.CompareTo(other.SortTag);
        }
    }
}
