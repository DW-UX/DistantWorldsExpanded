// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ScenarioObjective
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ScenarioObjective
  {
    private Empire _ObjectiveEmpire;
    private ScenarioObjectiveType _Type;
    private Habitat _Habitat;
    private GalaxyLocation _GalaxyLocation;
    private BuiltObject _BuiltObject;
    private Design _Design;
    private BuiltObjectSubRole _BuiltObjectSubRole;
    private Creature _Creature;
    private Empire _Empire;
    private ShipGroup _Fleet;
    private Character _Character;
    private Ruin _Ruin;
    private Habitat _DestinationHabitat;
    private GalaxyLocation _DestinationLocation;
    private DiplomaticRelation _DiplomaticRelation;
    private Empire _HabitatChangeOwnershipEmpire;

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      Habitat habitat,
      Empire newOwner)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.HabitatChangeOwnership ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be HabitatChangeOwnership");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = habitat;
      this._BuiltObject = (BuiltObject) null;
      this._Creature = (Creature) null;
      this._Empire = (Empire) null;
      this._Fleet = (ShipGroup) null;
      this._DestinationHabitat = (Habitat) null;
      this._DiplomaticRelation = (DiplomaticRelation) null;
      this._HabitatChangeOwnershipEmpire = newOwner;
    }

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      Habitat habitat)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.HabitatColonized || scenarioObjectiveType == ScenarioObjectiveType.HabitatEncountered ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be HabitatColonized or HabitatDiscovery");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = habitat;
      this._BuiltObject = (BuiltObject) null;
      this._Creature = (Creature) null;
      this._Empire = (Empire) null;
      this._Fleet = (ShipGroup) null;
      this._DestinationHabitat = (Habitat) null;
      this._DiplomaticRelation = (DiplomaticRelation) null;
      this._HabitatChangeOwnershipEmpire = (Empire) null;
    }

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      BuiltObject builtObject,
      Habitat arrivalLocation)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.BuiltObjectArriveAtLocation ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be BuiltObjectArriveAtLocation");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = (Habitat) null;
      this._BuiltObject = builtObject;
      this._Creature = (Creature) null;
      this._Empire = (Empire) null;
      this._Fleet = (ShipGroup) null;
      this._DestinationHabitat = arrivalLocation;
      this._DiplomaticRelation = (DiplomaticRelation) null;
      this._HabitatChangeOwnershipEmpire = (Empire) null;
    }

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      BuiltObject builtObject)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.BuiltObjectDestroyed || scenarioObjectiveType == ScenarioObjectiveType.BuiltObjectEncountered ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be BuiltObjectDestroyed or BuiltObjectEncountered");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = (Habitat) null;
      this._BuiltObject = builtObject;
      this._Creature = (Creature) null;
      this._Empire = (Empire) null;
      this._Fleet = (ShipGroup) null;
      this._DestinationHabitat = (Habitat) null;
      this._DiplomaticRelation = (DiplomaticRelation) null;
      this._HabitatChangeOwnershipEmpire = (Empire) null;
    }

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      Creature creature)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.CreatureDestroyed || scenarioObjectiveType == ScenarioObjectiveType.CreatureEncountered ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be CreatureDestroyed or CreatureEncountered");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = (Habitat) null;
      this._BuiltObject = (BuiltObject) null;
      this._Creature = creature;
      this._Empire = (Empire) null;
      this._Fleet = (ShipGroup) null;
      this._DestinationHabitat = (Habitat) null;
      this._DiplomaticRelation = (DiplomaticRelation) null;
      this._HabitatChangeOwnershipEmpire = (Empire) null;
    }

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      ShipGroup fleet,
      Habitat arrivalLocation)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.FleetArriveAtLocation ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be FleetArriveAtLocation");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = (Habitat) null;
      this._BuiltObject = (BuiltObject) null;
      this._Creature = (Creature) null;
      this._Empire = (Empire) null;
      this._Fleet = fleet;
      this._DestinationHabitat = arrivalLocation;
      this._DiplomaticRelation = (DiplomaticRelation) null;
      this._HabitatChangeOwnershipEmpire = (Empire) null;
    }

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      ShipGroup fleet)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.FleetDestroyed ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be FleetDestroyed");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = (Habitat) null;
      this._BuiltObject = (BuiltObject) null;
      this._Creature = (Creature) null;
      this._Empire = (Empire) null;
      this._Fleet = fleet;
      this._DestinationHabitat = (Habitat) null;
      this._DiplomaticRelation = (DiplomaticRelation) null;
      this._HabitatChangeOwnershipEmpire = (Empire) null;
    }

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      Empire empire)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.EmpireDestroyed || scenarioObjectiveType == ScenarioObjectiveType.EmpireEncountered ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be EmpireDestroyed or EmpireEncountered");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = (Habitat) null;
      this._BuiltObject = (BuiltObject) null;
      this._Creature = (Creature) null;
      this._Empire = empire;
      this._Fleet = (ShipGroup) null;
      this._DestinationHabitat = (Habitat) null;
      this._DiplomaticRelation = (DiplomaticRelation) null;
      this._HabitatChangeOwnershipEmpire = (Empire) null;
    }

    public ScenarioObjective(
      Empire objectiveEmpire,
      ScenarioObjectiveType scenarioObjectiveType,
      Empire empire,
      Empire otherEmpire,
      DiplomaticRelationType diplomaticRelationType)
    {
      this._Type = scenarioObjectiveType == ScenarioObjectiveType.EmpireDiplomaticRelationChange ? scenarioObjectiveType : throw new ApplicationException("Invalid scenario Objective type - should be EmpireDiplomaticRelationChange");
      this._ObjectiveEmpire = objectiveEmpire;
      this._Habitat = (Habitat) null;
      this._BuiltObject = (BuiltObject) null;
      this._Creature = (Creature) null;
      this._Empire = empire;
      this._Fleet = (ShipGroup) null;
      this._DestinationHabitat = (Habitat) null;
      this._DiplomaticRelation = new DiplomaticRelation(diplomaticRelationType, empire, empire, otherEmpire, false);
      this._HabitatChangeOwnershipEmpire = (Empire) null;
    }

    public Empire ObjectiveEmpire
    {
      get => this._ObjectiveEmpire;
      set => this._ObjectiveEmpire = value;
    }

    public Habitat Habitat
    {
      get => this._Habitat;
      set => this._Habitat = value;
    }

    public GalaxyLocation GalaxyLocation
    {
      get => this._GalaxyLocation;
      set => this._GalaxyLocation = value;
    }

    public BuiltObject BuiltObject
    {
      get => this._BuiltObject;
      set => this._BuiltObject = value;
    }

    public Creature Creature
    {
      get => this._Creature;
      set => this._Creature = value;
    }

    public Empire Empire
    {
      get => this._Empire;
      set => this._Empire = value;
    }

    public ShipGroup Fleet
    {
      get => this._Fleet;
      set => this._Fleet = value;
    }

    public Habitat DestinationHabitat
    {
      get => this._DestinationHabitat;
      set => this._DestinationHabitat = value;
    }

    public GalaxyLocation DestinationGalaxyLocation
    {
      get => this._DestinationLocation;
      set => this._DestinationLocation = value;
    }

    public Design Design
    {
      get => this._Design;
      set => this._Design = value;
    }

    public Character Character
    {
      get => this._Character;
      set => this._Character = value;
    }

    public BuiltObjectSubRole BuiltObjectSubRole
    {
      get => this._BuiltObjectSubRole;
      set => this._BuiltObjectSubRole = value;
    }

    public Ruin Ruin
    {
      get => this._Ruin;
      set => this._Ruin = value;
    }

    public ScenarioObjectiveType Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    public DiplomaticRelation DiplomaticRelation
    {
      get => this._DiplomaticRelation;
      set => this._DiplomaticRelation = value;
    }

    public Empire HabitatChangeOwnershipEmpire
    {
      get => this._HabitatChangeOwnershipEmpire;
      set => this._HabitatChangeOwnershipEmpire = value;
    }
  }
}
