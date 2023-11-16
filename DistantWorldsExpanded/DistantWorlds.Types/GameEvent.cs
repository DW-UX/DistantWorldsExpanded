// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GameEvent
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GameEvent
  {
    public short GameEventId;
    public EventTriggerType TriggerType;
    public EventActionList Actions = new EventActionList();
    public StellarObject TriggerObject;
    public Ruin TriggerRuin;
    public PlanetaryFacilityDefinition TriggerFacility;
    public BuiltObjectSubRole TriggerBuiltObjectSubRole;
    public Empire Empire;
    public Empire EmpireOther;
    public DiplomaticRelationType DiplomaticRelationType = DiplomaticRelationType.None;
    public int ResearchProjectId = -1;
    public string Title = string.Empty;
    public string Description = string.Empty;
    public bool CanOnlyBeTriggeredByPlayer;
    public bool HasBeenTriggered;
    [OptionalField]
    public Character Character;
    public List<EventTriggerType> ValidTriggerTypes = new List<EventTriggerType>();

    public GameEvent(Galaxy galaxy, short gameEventId, StellarObject triggerObject)
      : this(galaxy, gameEventId, triggerObject, (Ruin) null)
    {
    }

    public GameEvent(
      Galaxy galaxy,
      short gameEventId,
      StellarObject triggerObject,
      Ruin triggerRuin)
    {
      this.GameEventId = gameEventId;
      this.TriggerObject = triggerObject;
      this.TriggerRuin = triggerRuin;
      this.ValidTriggerTypes = this.ResolveValidTriggerTypes(this.TriggerObject, this.TriggerRuin);
      this.TriggerType = this.ValidTriggerTypes[0];
      this.SetDefaultsForEvent(this.TriggerType, galaxy);
    }

    public void SetDefaultsForEvent(EventTriggerType triggerType, Galaxy galaxy)
    {
      if (galaxy == null || this.TriggerObject != null)
        return;
      switch (triggerType)
      {
        case EventTriggerType.DiplomaticRelationChange:
          if (galaxy.Empires == null || galaxy.Empires.Count <= 1)
            break;
          this.Empire = galaxy.Empires[0];
          this.EmpireOther = galaxy.Empires[1];
          this.DiplomaticRelationType = DiplomaticRelationType.FreeTradeAgreement;
          break;
        case EventTriggerType.EmpireEncounter:
          if (galaxy.Empires == null || galaxy.Empires.Count <= 0)
            break;
          this.Empire = galaxy.Empires[0];
          this.EmpireOther = galaxy.Empires[1];
          break;
        case EventTriggerType.ResearchBreakthrough:
          if (galaxy.Empires == null || galaxy.Empires.Count <= 0)
            break;
          this.Empire = galaxy.Empires[0];
          this.ResearchProjectId = 0;
          break;
        case EventTriggerType.PlanetDestroyerConstructionCompleted:
          if (galaxy.Empires == null || galaxy.Empires.Count <= 0)
            break;
          this.Empire = galaxy.Empires[0];
          break;
        case EventTriggerType.EmpireEliminated:
          if (galaxy.Empires != null && galaxy.Empires.Count > 0)
            this.Empire = galaxy.Empires[0];
          this.EmpireOther = (Empire) null;
          break;
        case EventTriggerType.CharacterAppears:
          if (galaxy.Empires == null || galaxy.Empires.Count <= 0)
            break;
          this.Empire = galaxy.Empires[0];
          if (this.Empire.DominantRace == null || this.Empire.DominantRace.AvailableCharacters == null || this.Empire.DominantRace.AvailableCharacters.Count <= 0)
            break;
          this.Character = this.Empire.DominantRace.AvailableCharacters[0];
          break;
        case EventTriggerType.CharacterKilled:
          if (galaxy.Empires == null || galaxy.Empires.Count <= 0)
            break;
          this.Empire = galaxy.Empires[0];
          if (this.Empire.Characters == null || this.Empire.Characters.Count <= 0)
            break;
          this.Character = this.Empire.Characters[0];
          break;
      }
    }

    public List<EventTriggerType> ResolveValidTriggerTypes(
      StellarObject triggerObject,
      Ruin triggerRuin)
    {
      List<EventTriggerType> eventTriggerTypeList = new List<EventTriggerType>();
      switch (triggerObject)
      {
        case null:
          eventTriggerTypeList.Add(EventTriggerType.DiplomaticRelationChange);
          eventTriggerTypeList.Add(EventTriggerType.EmpireEncounter);
          eventTriggerTypeList.Add(EventTriggerType.ResearchBreakthrough);
          eventTriggerTypeList.Add(EventTriggerType.PlanetDestroyerConstructionCompleted);
          eventTriggerTypeList.Add(EventTriggerType.EmpireEliminated);
          eventTriggerTypeList.Add(EventTriggerType.CharacterAppears);
          eventTriggerTypeList.Add(EventTriggerType.CharacterKilled);
          break;
        case Habitat _:
          Habitat habitat = (Habitat) triggerObject;
          eventTriggerTypeList.Add(EventTriggerType.Destroy);
          eventTriggerTypeList.Add(EventTriggerType.Capture);
          eventTriggerTypeList.Add(EventTriggerType.Build);
          if (triggerRuin != null && habitat.Ruin != null && habitat.Ruin == triggerRuin)
          {
            eventTriggerTypeList.Clear();
            eventTriggerTypeList.Add(EventTriggerType.Investigate);
            break;
          }
          break;
        case BuiltObject _:
          BuiltObject builtObject = (BuiltObject) triggerObject;
          eventTriggerTypeList.Add(EventTriggerType.Destroy);
          eventTriggerTypeList.Add(EventTriggerType.Capture);
          if (builtObject.Owner == null)
          {
            eventTriggerTypeList.Add(EventTriggerType.Investigate);
            break;
          }
          break;
        case Creature _:
          eventTriggerTypeList.Add(EventTriggerType.Destroy);
          break;
      }
      return eventTriggerTypeList;
    }
  }
}
