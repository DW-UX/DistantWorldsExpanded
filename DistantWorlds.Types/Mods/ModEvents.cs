using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.Types.Mods
{
    public class CheckEmpireTerritoryCanBuildAtHabitatModsArgs : EventArgs
    {
        public Galaxy Galaxy { get; }
        public Empire Empire { get; }
        public Habitat Habitat { get; }
        public bool Result { get; set; }
        public CheckEmpireTerritoryCanBuildAtHabitatModsArgs(Galaxy galaxy, Empire empire, Habitat habitat)
        {
            Galaxy = galaxy;
            Empire = empire;
            Habitat = habitat;
        }

    }

    public class ResolveDescriptionModsArgs : EventArgs
    {
        public CharacterEvent CharacterEvent { get; }
        public Empire CallingEmpire { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ResolveDescriptionModsArgs(CharacterEvent characterEvent, Empire callingEmpire)
        {
            CharacterEvent = characterEvent;
            CallingEmpire = callingEmpire;
        }

    }

    public class RefactorValueForEmpireModsArgs : EventArgs
    {
        public Galaxy Galaxy { get; }
        public int Value { get; }
        public Empire RequestingEmpire { get; }
        public Empire OfferingEmpire { get; }
        public int Result { get; set; }
        public RefactorValueForEmpireModsArgs(Galaxy galaxy, int value, Empire requestingEmpire, Empire offeringEmpire)
        {
            Galaxy = galaxy;
            Value = value;
            RequestingEmpire = requestingEmpire;
            OfferingEmpire = offeringEmpire;
        }

    }
    public class ResolveTradeableItemsColoniesBasesModsArgs : EventArgs
    {
        public TradeableItemList Result { get; private set; }
        public Empire Giver { get; }
        public Empire Receiver { get; }
        public bool IncludeNearestColony { get; }
        public bool RefactorValuesForEmpire { get; }
        public Galaxy Galaxy { get; }

        public ResolveTradeableItemsColoniesBasesModsArgs(Galaxy galaxy, Empire giver, Empire receiver, bool includeNearestColony, bool refactorValuesForEmpire)
        {
            Galaxy = galaxy;
            Giver = giver;
            Receiver = receiver;
            IncludeNearestColony = includeNearestColony;
            RefactorValuesForEmpire = refactorValuesForEmpire;
            this.Result = new TradeableItemList();
        }

    }
    public class ResolveComponentDescriptionDetailedModsArgs : EventArgs
    {
        public string[] Values { get; set; }
        public string[] Description { get; set; }
        public Galaxy Galaxy { get; }
        public Empire Empire { get; }
        public Component Component { get; }
        public ComponentImprovement Improvement { get; }
        public ResearchNode Project { get; }

        public ResolveComponentDescriptionDetailedModsArgs(Galaxy galaxy, Empire empire, Component component, ComponentImprovement improvement, ResearchNode project)
        {
            Galaxy = galaxy;
            Empire = empire;
            Component = component;
            Improvement = improvement;
            Project = project;
        }

    }
    public class ShipDoubleValueModsArgs : EventArgs
    {
        public double Result { get; set; }
        public BuiltObject BuiltObject { get; }

        public ShipDoubleValueModsArgs(BuiltObject builtObject)
        {
            BuiltObject = builtObject;
        }

    }
    public class ExecuteEventActionModsArgs : EventArgs
    {
        public bool Result { get; set; }
        public Galaxy Galaxy { get; }
        public EventAction EventAction { get; }
        public Empire TargetEmpire { get; }
        public GameEvent GameEvent { get; }

        public ExecuteEventActionModsArgs(Galaxy galaxy, EventAction eventAction, Empire targetEmpire, GameEvent gameEvent, bool flag)
        {
            Galaxy = galaxy;
            EventAction = eventAction;
            TargetEmpire = targetEmpire;
            GameEvent = gameEvent;
            Result = flag;
        }
    }
    public class ExtraFieldsModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public SerializationInfo Info { get; }

        public ExtraFieldsModsArgs(BuiltObject ship, SerializationInfo info)
        {
            Ship = ship;
            Info = info;
        }
    }
    public class BuildNewFightersModsArgs : EventArgs
    {
        public BuiltObject Carrier { get; }

        public BuildNewFightersModsArgs(BuiltObject carrier)
        {
            Carrier = carrier;
        }
    }
    public class BuildFightersModsArgs : EventArgs
    {
        public BuiltObject Carrier { get; }
        public FighterSpecification FighterSpecification { get; }

        public BuildFightersModsArgs(BuiltObject carrier, FighterSpecification fighterSpecification)
        {
            Carrier = carrier;
            FighterSpecification = fighterSpecification;
        }
    }
    public class RepairModsArgs : EventArgs
    {
        public BuiltObject Carrier { get; }
        public double TimePassed { get; }

        public RepairModsArgs(BuiltObject carrier, double timePassed)
        {
            Carrier = carrier;
            TimePassed = timePassed;
        }
    }
    public class ShipIntValueModsArgs : EventArgs
    {
        public int Result { get; set; }
        public BuiltObject Target { get; }

        public ShipIntValueModsArgs(BuiltObject target)
        {
            Target = target;
        }
    }
    public class ShipBoolValueModsArgs : EventArgs
    {
        public bool Result { get; set; }
        public BuiltObject Target { get; }

        public ShipBoolValueModsArgs(BuiltObject target)
        {
            Target = target;
        }
    }
    public class InterceptMissilesModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public DateTime Time { get; }
        public bool InView { get; }

        public InterceptMissilesModsArgs(BuiltObject ship, DateTime time, bool inView)
        {
            Ship = ship;
            Time = time;
            InView = inView;
        }
    }
    public class IdentifySystemThreatsToUsModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public Habitat SystemStar { get; }
        public int[] ThreatLevels { get; set; }
        public int TotalThreatLevel { get; set; }
        public StellarObject[] Result { get; set; }

        public IdentifySystemThreatsToUsModsArgs(BuiltObject ship, Habitat systemStar)
        {
            Ship = ship;
            SystemStar = systemStar;
        }
    }
    public class AddScientificDataModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public Habitat Planet { get; }
        public string EventType { get; }

        public AddScientificDataModsArgs(BuiltObject ship, Habitat planet, string eventType)
        {
            Ship = ship;
            Planet = planet;
            EventType = eventType;
        }
    }
    public class WeaponRangeIncrementForDamageLossModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public float Result { get; }

        public WeaponRangeIncrementForDamageLossModsArgs(BuiltObject ship)
        {
            Ship = ship;
        }
    }
    public class WeaponDamageDropoffModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public Weapon Weapon { get; }
        public float RawDamage { get; }
        public float Result { get; }

        public WeaponDamageDropoffModsArgs(BuiltObject ship, Weapon weapon, float rawDamage)
        {
            Ship = ship;
            Weapon = weapon;
            RawDamage = rawDamage;
        }
    }
    public class CheckForNegativeRefuelingModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public int Result { get; }
        public int RefuelAmount { get; }

        public CheckForNegativeRefuelingModsArgs(BuiltObject ship, int refuelAmount)
        {
            Ship = ship;
            RefuelAmount = refuelAmount;
        }
    }
    public class PrivateSectorBuildOrRefitInvestInInfrastructureModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public double Result { get; }
        public double Cost { get; }

        public PrivateSectorBuildOrRefitInvestInInfrastructureModsArgs(BuiltObject ship, double cost)
        {
            Ship = ship;
            Cost = cost;
        }
    }
    public class AssignQueuedMissionModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public bool Result { get; }
        public bool AllowReprocessing { get; }

        public AssignQueuedMissionModsArgs(BuiltObject ship, bool allowReprocessing)
        {
            Ship = ship;
            AllowReprocessing = allowReprocessing;
        }
    }
    public class CollectScrapFromDestroyedBuiltObjectsModsArgs : EventArgs
    {
        public BuiltObject Attacker { get; }
        public BuiltObject Target { get; }

        public CollectScrapFromDestroyedBuiltObjectsModsArgs(BuiltObject attacker, BuiltObject target)
        {
            Attacker = attacker;
            Target = target;
        }

    }
    public class MaxResourceExtractionRateModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public int ResourceType { get; }
        public double Result { get; set; }

        public MaxResourceExtractionRateModsArgs(BuiltObject ship, int resourceType)
        {
            Ship = ship;
            ResourceType = resourceType;
        }

    }
    public class AddLatestCombatStatsModsArgs : EventArgs
    {
        public BuiltObject Ship { get; }
        public SpaceBattleStats BattleStats { get; }

        public AddLatestCombatStatsModsArgs(BuiltObject ship, SpaceBattleStats battleStats)
        {
            Ship = ship;
            BattleStats = battleStats;
        }
    }
    public class AddMoreImagesModsArgs : EventArgs
    {
        public BuiltObjectImageCache ImageCache { get; }
        public int Index { get; }
        public string RegularPath { get; }
        public string ModPath { get; }

        public AddMoreImagesModsArgs(BuiltObjectImageCache imageCache, int index, string regularPath, string modPath)
        {
            ImageCache = imageCache;
            Index = index;
            RegularPath = regularPath;
            ModPath = modPath;
        }
    }
    public class FindShortestConstructionWaitQueueModsArgs : EventArgs
    {
        public BuiltObjectList Bol { get; }
        public BuiltObject Ship { get; }
        public BuiltObject Result { get; }
        public double ShortestWaitQueueTime { get; }
        public bool IncludeVerySmallYards { get; }
        public int MaximumQueueDepth { get; }

        public FindShortestConstructionWaitQueueModsArgs(BuiltObjectList bol, BuiltObject ship, bool includeVerySmallYards, int maximumQueueDepth)
        {
            Bol = bol;
            Ship = ship;
            IncludeVerySmallYards = includeVerySmallYards;
            MaximumQueueDepth = maximumQueueDepth;
        }
    }
    public class IncrementSkillProgressModsArgs : EventArgs
    {
        public Character Character { get; }
        public int Result { get; set; }
        public IncrementSkillProgressModsArgs(Character character)
        {
            Character = character;
        }
    }
    public class KillModsArgs : EventArgs
    {
        public Character Character { get; }
        public bool Result { get; set; }
        public KillModsArgs(Character character)
        {
            Character = character;
        }
    }
    public class ConstructionSpeedMultiplierModsArgs : EventArgs
    {
        public double Result { get; set; }
        public ConstructionQueue Queue { get; }

        public ConstructionSpeedMultiplierModsArgs(ConstructionQueue queue)
        {
            Queue = queue;
        }
    }
    public class MoveCargoFromBuilderToBuiltBaseModsArgs : EventArgs
    {
        public BuiltObject ConstructionShip { get; }
        public BuiltObject BuiltObject { get; }

        public MoveCargoFromBuilderToBuiltBaseModsArgs(BuiltObject constructionShip, BuiltObject builtObject)
        {
            ConstructionShip = constructionShip;
            BuiltObject = builtObject;
        }
    }
    public class SetPictureRefModsArgs : EventArgs
    {
        public Design Design { get; }
        public int OriginalValue { get; }
        public int Result { get; set; }

        public SetPictureRefModsArgs(Design design, int originalValue)
        {
            Design = design;
            OriginalValue = originalValue;
        }
    }
    public class CalculateMaintenanceCostsModsArgs : EventArgs
    {
        public Design Design { get; }
        public Galaxy Galaxy { get; }
        public Empire Empire { get; }
        public double Result { get; set; }

        public CalculateMaintenanceCostsModsArgs(Design design, Galaxy galaxy, Empire empire)
        {
            Design = design;
            Galaxy = galaxy;
            Empire = empire;
        }
    }
    public class DeserializeExtraFieldsFighterModsArgs : EventArgs
    {
        public Fighter Fighter { get; }
        public SerializationInfo Info { get; }

        public DeserializeExtraFieldsFighterModsArgs(Fighter fighter, SerializationInfo info)
        {
            Fighter = fighter;
            Info = info;
        }
    }
    public class DeserializeExtraFieldsHabitatModsArgs : EventArgs
    {
        public Habitat Planet { get; }
        public SerializationInfo Info { get; }

        public DeserializeExtraFieldsHabitatModsArgs(Habitat planet, SerializationInfo info)
        {
            Planet = planet;
            Info = info;
        }
    }
    public class DoTasksModsArgs : EventArgs
    {
        public Fighter Fighter { get; }
        public Galaxy Galaxy { get; }
        public DateTime Time { get; }
        public bool InView { get; }

        public DoTasksModsArgs(Fighter fighter, Galaxy galaxy, DateTime time, bool inView)
        {
            Fighter = fighter;
            Galaxy = galaxy;
            Time = time;
            InView = inView;
        }
    }
    public class ArgType1ModsArgs : EventArgs
    {
        public Fighter Fighter { get; }
        public Galaxy Galaxy { get; }

        public ArgType1ModsArgs(Fighter fighter, Galaxy galaxy)
        {
            Fighter = fighter;
            Galaxy = galaxy;
        }
    }
    public class EvaluateAdequateAttackersModsArgs : EventArgs
    {
        public Fighter Fighter { get; }
        public Galaxy Galaxy { get; }
        public StellarObject PotentialTarget { get; }
        public bool Result { get; set; }

        public EvaluateAdequateAttackersModsArgs(Fighter fighter, Galaxy galaxy, StellarObject potentialTarget)
        {
            Fighter = fighter;
            Galaxy = galaxy;
            PotentialTarget = potentialTarget;
        }
    }
    public class InflictDamageFighterModsArgs : EventArgs
    {
        public Fighter Fighter { get; }
        public double Result { get; set; }

        public InflictDamageFighterModsArgs(Fighter fighter)
        {
            Fighter = fighter;
        }
    }
    public class CheckForLevelGainModsArgs : EventArgs
    {
        public Fighter Fighter { get; }
        public double DamageInflicted { get; }

        public CheckForLevelGainModsArgs(Fighter fighter, double damageInflicted)
        {
            Fighter = fighter;
            DamageInflicted = damageInflicted;
        }
    }
    public class FireWeaponsAtTargetModsArgs : EventArgs
    {
        public Fighter FiringFighter { get; }
        public Galaxy Galaxy { get; }
        public StellarObject Target { get; }
        public double DistanceToTarget { get; }
        public DateTime Time { get; }

        public FireWeaponsAtTargetModsArgs(Fighter firingFighter, Galaxy galaxy, StellarObject target, double distanceToTarget, DateTime time)
        {
            FiringFighter = firingFighter;
            Galaxy = galaxy;
            Target = target;
            DistanceToTarget = distanceToTarget;
            Time = time;
        }
    }
    public class PursueTargetModsArgs : EventArgs
    {
        public Fighter FiringFighter { get; }
        public Galaxy Galaxy { get; }
        public DateTime Time { get; }

        public PursueTargetModsArgs(Fighter firingFighter, Galaxy galaxy, DateTime time)
        {
            FiringFighter = firingFighter;
            Galaxy = galaxy;
            Time = time;
        }
    }
    public class ResolveNewFighterImageIndexModsArgs : EventArgs
    {
        public FighterSpecification FighterSpec { get; }
        public Race EmpireRace { get; }
        public bool IsPirates { get; }
        public int Result { get; set; }

        public ResolveNewFighterImageIndexModsArgs(FighterSpecification fighterSpec, Race empireRace, bool isPirates)
        {
            FighterSpec = fighterSpec;
            EmpireRace = empireRace;
            IsPirates = isPirates;
        }
    }
    public class TerritoryMultiplerModsArgs : EventArgs
    {
        public Habitat Planet { get; }
        public float Result { get; set; }

        public TerritoryMultiplerModsArgs(Habitat planet)
        {
            Planet = planet;
        }
    }
    public class HabitatType1ModsArgs : EventArgs
    {
        public Habitat Planet { get; }
        public double TimePassed { get; }

        public HabitatType1ModsArgs(Habitat planet, double timePassed)
        {
            Planet = planet;
            TimePassed = timePassed;
        }
    }
    public class GenerateDefensivePirateRaidersModsArgs : EventArgs
    {
        public Habitat Planet { get; }
        public Empire DefendingPirateFaction { get; }
        public bool CurrentDefendingTroopsInvade { get; }

        public GenerateDefensivePirateRaidersModsArgs(Habitat planet, Empire defendingPirateFaction, bool currentDefendingTroopsInvade)
        {
            Planet = planet;
            DefendingPirateFaction = defendingPirateFaction;
            CurrentDefendingTroopsInvade = currentDefendingTroopsInvade;
        }
    }
    public class CalculateSpaceControlStrengthsModsArgs : EventArgs
    {
        public Habitat Planet { get; }
        public Empire DefendingEmpire { get; }
        public Empire AttackingEmpire { get; }
        public int SpaceControlStrengthDefenders { get; set; }
        public int SpaceControlStrengthAttackers { get; set; }

        public CalculateSpaceControlStrengthsModsArgs(Habitat planet, Empire defendingEmpire, Empire attackingEmpire)
        {
            Planet = planet;
            DefendingEmpire = defendingEmpire;
            AttackingEmpire = attackingEmpire;
        }
    }
    public class CalculateForceStrengthsModsArgs : EventArgs
    {
        public Habitat Planet { get; }
        public Empire Defender { get; }
        public Empire Attacker { get; }
        public TroopList DefendingTroops { get; }
        public CharacterList DefendingCharacters { get; }
        public TroopList AttackingTroops { get; }
        public CharacterList AttackingCharacters { get; }
        public int DefendingStrength { get; set; }
        public int AttackingStrength { get; set; }
        public double TotalDefendModifier { get; set; }
        public double TotalAttackModifier { get; set; }
        public List<double> ModifierAmountsDefense { get; set; }
        public List<string> ModifierReasonsDefense { get; set; }
        public List<double> ModifierAmountsAttack { get; set; }
        public List<string> ModifierReasonsAttack { get; set; }

        public CalculateForceStrengthsModsArgs(Habitat planet,
                                              Empire defender,
                                              Empire attacker,
                                              TroopList defendingTroops,
                                              CharacterList defendingCharacters,
                                              TroopList attackingTroops,
                                              CharacterList attackingCharacters)
        {
            Planet = planet;
            Defender = defender;
            Attacker = attacker;
            DefendingTroops = defendingTroops;
            DefendingCharacters = defendingCharacters;
            AttackingTroops = attackingTroops;
            AttackingCharacters = attackingCharacters;
        }
    }
    public class GetDevelopmentLevelModsArgs : EventArgs
    {
        public Habitat Planet { get; }
        public int Result { get; set; }

        public GetDevelopmentLevelModsArgs(Habitat planet)
        {
            Planet = planet;
        }
    }
    public class LoadMoreCargoModsArgs : EventArgs
    {
        public BuiltObjectMission Mission { get; }
        public BuiltObject Ship { get; }

        public LoadMoreCargoModsArgs(BuiltObjectMission mission, BuiltObject ship)
        {
            Mission = mission;
            Ship = ship;
        }
    }
    public class ResolveCommandsForMissionModsArgs : EventArgs
    {
        public BuiltObjectMission TheThis { get; }
        public BuiltObjectMission Mission { get; }
        public bool AllowReprocessing { get; }
        public bool SpecifiedAsFleetMission { get; }
        public bool CouldResolveCommands { get; set; }
        public CommandQueue Result { get; set; }

        public ResolveCommandsForMissionModsArgs(BuiltObjectMission theThis,
                                                  BuiltObjectMission mission,
                                                  bool allowReprocessing,
                                                  bool specifiedAsFleetMission)
        {
            TheThis = theThis;
            Mission = mission;
            AllowReprocessing = allowReprocessing;
            SpecifiedAsFleetMission = specifiedAsFleetMission;
        }
    }
    public class SetPirateFactionModifiersModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public PiratePlayStyle PiratePlayStyle { get; }

        public SetPirateFactionModifiersModsArgs(Empire empire, PiratePlayStyle piratePlayStyle)
        {
            Empire = empire;
            PiratePlayStyle = piratePlayStyle;
        }
    }
    public class EmpireDoubleModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public double Result{ get; }

        public EmpireDoubleModsArgs(Empire empire)
        {
            Empire = empire;
        }
    }
    public class DestroyUnreservedCargoOfEmpireModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public CargoList CargoList { get; }

        public DestroyUnreservedCargoOfEmpireModsArgs(CargoList cargoList, Empire empire)
        {
            Empire = empire;
            CargoList = cargoList;
        }
    }
    public class TakePossessionOfBuiltObjectModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public BuiltObject ItemReceived { get; }
        public Empire ReceivingEmpire { get; }

        public TakePossessionOfBuiltObjectModsArgs(Empire empire,
      BuiltObject itemReceived,
      Empire receivingEmpire)
        {
            Empire = empire;
            ItemReceived = itemReceived;
            ReceivingEmpire = receivingEmpire;
        }
    }
    public class CheckResourceMeetsMinimumLevelBaseNotAtColonyModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public Resource Resource { get; }
        public int MinimumResourceLevel { get; }
        public int MaximumResourceLevel { get; }
        public BuiltObject BaseNotAtColony { get; }
        public OrderList BaseOrders { get; }
        public int AmountToOrder { get; set; }
        public bool Result { get; set; }

        public CheckResourceMeetsMinimumLevelBaseNotAtColonyModsArgs(Empire empire,
                                                                      Resource resource,
                                                                      int minimumResourceLevel,
                                                                      int maximumResourceLevel,
                                                                      BuiltObject baseNotAtColony,
                                                                      OrderList baseOrders)
        {
            Empire = empire;
            Resource = resource;
            MinimumResourceLevel = minimumResourceLevel;
            MaximumResourceLevel = maximumResourceLevel;
            BaseNotAtColony = baseNotAtColony;
            BaseOrders = baseOrders;
        }
    }
    public class EmpireFloatModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public float Result { get; set; }

        public EmpireFloatModsArgs(Empire empire)
        {
            Empire = empire;
        }
    }
    public class RemoveStateShipsModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public BuiltObjectList Ships { get; }
        public BuiltObjectList Result { get; }

        public RemoveStateShipsModsArgs(Empire empire, BuiltObjectList ships)
        {
            Empire = empire;
            Ships = ships;
        }
    }
    public class EmpireType1ModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public double TimePassed { get; }

        public EmpireType1ModsArgs(Empire empire, double timePassed)
        {
            Empire = empire;
            TimePassed = timePassed;
        }
    }
    public class DetermineResettleDestinationModsArgs : EventArgs
    {
        public Empire EmpireBeingProcessed { get; }
        public Race Race { get; }
        public BuiltObject PassengerShip { get; }
        public Habitat SourceColony { get; }
        public Habitat Result { get; set; }

        public DetermineResettleDestinationModsArgs(Empire empireBeingProcessed,
                                                  Race race,
                                                  BuiltObject passengerShip,
                                                  Habitat sourceColony)
        {
            EmpireBeingProcessed = empireBeingProcessed;
            Race = race;
            PassengerShip = passengerShip;
            SourceColony = sourceColony;
        }
    }
    public class DetermineMigrationDestinationsModsArgs : EventArgs
    {
        public Empire EmpireBeingProcessed { get; }
        public PrioritizedTargetList Result { get; set; }

        public DetermineMigrationDestinationsModsArgs(Empire empireBeingProcessed)
        {
            EmpireBeingProcessed = empireBeingProcessed;
        }
    }
    public class EmpireBoolModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public bool Result { get; }

        public EmpireBoolModsArgs(Empire empire)
        {
            Empire = empire;
        }
    }
    public class ResetSpyMissionModsArgs : EventArgs
    {
        public CharacterList SpiesToBeKilledOrCaptured { get; }
        public Character Spy { get; }

        public ResetSpyMissionModsArgs(CharacterList spiesToBeKilledOrCaptured, Character spy)
        {
            SpiesToBeKilledOrCaptured = spiesToBeKilledOrCaptured;
            Spy = spy;
        }
    }
    public class DetermineIntelligenceMissionOutcomeModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public IntelligenceMission Mission { get; }
        public Character Agent { get; }
        public IntelligenceMissionOutcome Result { get; set; }

        public DetermineIntelligenceMissionOutcomeModsArgs(Empire empire,      IntelligenceMission mission,      Character agent)
        {
            Empire = empire;
            Mission = mission;
            Agent = agent;
        }
    }
    public class OverrideTimeForMissionModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public IntelligenceMission Mission { get; }
        public IntelligenceMission Result { get; set; }

        public OverrideTimeForMissionModsArgs(Empire empire, IntelligenceMission mission)
        {
            Empire = empire;
            Mission = mission;
        }
    }
    public class EmpirePrivateConstructionAddToInfrastructureModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public double Cost { get; }
        public double Result { get; }

        public EmpirePrivateConstructionAddToInfrastructureModsArgs(Empire empire, double cost)
        {
            Empire = empire;
            Cost = cost;
        }
    }
    public class EnhanceCharacterModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public Character Character { get; }

        public EnhanceCharacterModsArgs(Empire empire, Character character)
        {
            Empire = empire;
            Character = character;
        }
    }
    public class CalculateSystemCompetitionModsArgs : EventArgs
    {
        public Empire MyEmpire { get; }
        public Empire OtherEmpire { get; }
        public HabitatList OurSystemStars { get; }
        public int Result { get; set; }

        public CalculateSystemCompetitionModsArgs(Empire myEmpire,      Empire otherEmpire,      HabitatList ourSystemStars)
        {
            MyEmpire = myEmpire;
            OtherEmpire = otherEmpire;
            OurSystemStars = ourSystemStars;
        }
    }
    public class CalculateEnvyModsArgs : EventArgs
    {
        public Empire MyEmpire { get; }
        public Empire OtherEmpire { get; }
        public int Envy { get; }
        public int Result { get; set; }

        public CalculateEnvyModsArgs(Empire myEmpire, Empire otherEmpire, int envy)
        {
            MyEmpire = myEmpire;
            OtherEmpire = otherEmpire;
            Envy = envy;
        }
    }
    public class CreateNewDesignsModsArgs : EventArgs
    {
        public Empire Empire { get; }
        public long DesignDate { get; }
        public bool ForceUpdate { get; }

        public CreateNewDesignsModsArgs(Empire empire, long designDate, bool forceUpdate)
        {
            Empire = empire;
            DesignDate = designDate;
            ForceUpdate = forceUpdate;
        }
    }
}
