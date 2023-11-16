// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireMessage
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireMessage : IComparable<EmpireMessage>
  {
    private string _Description;
    private EmpireMessageType _MessageType;
    private int _Priority;
    private Empire _Sender;
    private int _Money;
    [OptionalField]
    private AdvisorMessageType _AdvisorMessageType;
    [OptionalField]
    private object _AdvisorMessageData;
    [OptionalField]
    private object _AdvisorMessageData2;
    private Habitat _HabitatSubject;
    private BuiltObject _BuiltObjectSubject;
    [OptionalField]
    private BuiltObjectList _BuiltObjectListSubject;
    private Empire _EmpireSubject;
    private Point _PointSubject;
    private Character _CharacterSubject;
    private DiplomaticRelationType? _DiplomaticRelationTypeSubject;
    private Component _ComponentSubject;
    private GalaxyLocation _LocationSubject;
    private Resource _ResourceSubject;
    private TradeableItem _TradeableItemSubject;
    private object[] _TradeOfferSubject;
    private ShipGroup _ShipGroupSubject;
    private HabitatType _HabitatTypeSubject;
    private FighterSpecification _FighterSpecificationSubject;
    private PlanetaryFacilityDefinition _PlanetaryFacilityDefinitionSubject;
    private ResearchNode _ResearchNodeSubject;
    private Race _RaceSubject;
    [OptionalField]
    private IntelligenceMission _IntelligenceMissionSubject;
    [OptionalField]
    private EmpireActivity _PirateMissionSubject;
    [OptionalField]
    private PlanetaryFacility _PlanetaryFacilitySubject;
    [OptionalField]
    public string Hint;
    private string _Title;
    private long _StarDate;
    private bool _SupressPopup;

    public bool SupressPopup
    {
      get => this._SupressPopup;
      set => this._SupressPopup = value;
    }

    public AdvisorMessageType AdvisorMessageType
    {
      get => this._AdvisorMessageType;
      set => this._AdvisorMessageType = value;
    }

    public object AdvisorMessageData
    {
      get => this._AdvisorMessageData;
      set => this._AdvisorMessageData = value;
    }

    public object AdvisorMessageData2
    {
      get => this._AdvisorMessageData2;
      set => this._AdvisorMessageData2 = value;
    }

    public bool CheckAdvisorMessageEquivalence(EmpireMessage otherMessage) => otherMessage != null && this.MessageType == EmpireMessageType.AdvisorSuggestion && otherMessage.MessageType == EmpireMessageType.AdvisorSuggestion && otherMessage.Subject == this.Subject && otherMessage.AdvisorMessageType == this.AdvisorMessageType && otherMessage.AdvisorMessageData == this.AdvisorMessageData;

    public bool CheckAdvisorMessageEquivalenceAbbreviated(EmpireMessage otherMessage) => otherMessage != null && this.MessageType == EmpireMessageType.AdvisorSuggestion && otherMessage.MessageType == EmpireMessageType.AdvisorSuggestion && otherMessage.Subject == this.Subject && otherMessage.AdvisorMessageType == this.AdvisorMessageType;

    public Empire ResolveTargetEmpireFromSubject()
    {
      Empire empire = (Empire) null;
      if (this._HabitatSubject != null)
        empire = this._HabitatSubject.Empire;
      if (this._BuiltObjectSubject != null)
        empire = this._BuiltObjectSubject.Empire;
      if (this._BuiltObjectListSubject != null)
      {
        if (this._BuiltObjectListSubject.Count > 0)
        {
          BuiltObject builtObject = this._BuiltObjectListSubject[0];
          if (builtObject != null)
            empire = builtObject.Empire;
        }
      }
      if (this._ShipGroupSubject != null)
        empire = this._ShipGroupSubject.Empire;
      if (this._EmpireSubject != null)
        empire = this._EmpireSubject;
      if (this._CharacterSubject != null)
        empire = this._CharacterSubject.Empire;
      if (this._IntelligenceMissionSubject != null)
        empire = this._IntelligenceMissionSubject.TargetEmpire;
      if (this._PirateMissionSubject != null)
        empire = this._PirateMissionSubject.TargetEmpire;
      return empire;
    }

    private void SetSubject(object subject)
    {
      if (subject is IntelligenceMission)
        this._IntelligenceMissionSubject = (IntelligenceMission) subject;
      if (subject is Race)
        this._RaceSubject = (Race) subject;
      if (subject is Habitat)
        this._HabitatSubject = (Habitat) subject;
      if (subject is BuiltObject)
        this._BuiltObjectSubject = (BuiltObject) subject;
      if (subject is BuiltObjectList)
        this._BuiltObjectListSubject = (BuiltObjectList) subject;
      if (subject is ShipGroup)
        this._ShipGroupSubject = (ShipGroup) subject;
      if (subject is Empire)
        this._EmpireSubject = (Empire) subject;
      if (subject is Point point)
        this._PointSubject = point;
      if (subject is Character)
        this._CharacterSubject = (Character) subject;
      if (subject is DiplomaticRelationType diplomaticRelationType)
        this._DiplomaticRelationTypeSubject = new DiplomaticRelationType?(diplomaticRelationType);
      if (subject is Component)
        this._ComponentSubject = (Component) subject;
      if (subject is GalaxyLocation)
        this._LocationSubject = (GalaxyLocation) subject;
      if (subject is Resource)
        this._ResourceSubject = (Resource) subject;
      if (subject is TradeableItem)
        this._TradeableItemSubject = (TradeableItem) subject;
      if (subject is object[])
        this._TradeOfferSubject = (object[]) subject;
      if (subject is FighterSpecification)
        this._FighterSpecificationSubject = (FighterSpecification) subject;
      if (subject is HabitatType habitatType)
        this._HabitatTypeSubject = habitatType;
      if (subject is PlanetaryFacilityDefinition)
        this._PlanetaryFacilityDefinitionSubject = (PlanetaryFacilityDefinition) subject;
      if (subject is PlanetaryFacility)
        this._PlanetaryFacilitySubject = (PlanetaryFacility) subject;
      if (subject is ResearchNode)
        this._ResearchNodeSubject = (ResearchNode) subject;
      if (!(subject is EmpireActivity))
        return;
      this._PirateMissionSubject = (EmpireActivity) subject;
    }

    public EmpireMessage(Empire sender, EmpireMessageType messageType, object subject)
    {
      this._Sender = sender;
      this._MessageType = messageType;
      this._Priority = 0;
      this._Money = 0;
      this._Description = string.Empty;
      this.SetSubject(subject);
    }

    public string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    public string Title
    {
      get => this._Title;
      set => this._Title = value;
    }

    public long StarDate
    {
      get => this._StarDate;
      set => this._StarDate = value;
    }

    public Empire Sender => this._Sender;

    public EmpireMessageType MessageType => this._MessageType;

    public int Money
    {
      get => this._Money;
      set => this._Money = value;
    }

    public int Priority
    {
      get => this._Priority;
      set => this._Priority = value;
    }

    public Point Location
    {
      get => this._PointSubject;
      set => this._PointSubject = value;
    }

    public object Subject
    {
      get
      {
        if (this._IntelligenceMissionSubject != null)
          return (object) this._IntelligenceMissionSubject;
        if (this._RaceSubject != null)
          return (object) this._RaceSubject;
        if (this._EmpireSubject != null)
          return (object) this._EmpireSubject;
        if (this._HabitatSubject != null)
          return (object) this._HabitatSubject;
        if (this._BuiltObjectSubject != null)
          return (object) this._BuiltObjectSubject;
        if (this._BuiltObjectListSubject != null)
          return (object) this._BuiltObjectListSubject;
        if (this._ShipGroupSubject != null)
          return (object) this._ShipGroupSubject;
        if (this._CharacterSubject != null)
          return (object) this._CharacterSubject;
        if (this._DiplomaticRelationTypeSubject.HasValue)
          return (object) this._DiplomaticRelationTypeSubject;
        if (this._ComponentSubject != null)
          return (object) this._ComponentSubject;
        if (this._LocationSubject != null)
          return (object) this._LocationSubject;
        if (this._ResourceSubject != null)
          return (object) this._ResourceSubject;
        if (this._TradeableItemSubject != null)
          return (object) this._TradeableItemSubject;
        if (this._TradeOfferSubject != null)
          return (object) this._TradeOfferSubject;
        if (this._HabitatTypeSubject != HabitatType.Undefined)
          return (object) this._HabitatTypeSubject;
        if (this._FighterSpecificationSubject != null)
          return (object) this._FighterSpecificationSubject;
        if (this._PlanetaryFacilityDefinitionSubject != null)
          return (object) this._PlanetaryFacilityDefinitionSubject;
        if (this._PlanetaryFacilitySubject != null)
          return (object) this._PlanetaryFacilitySubject;
        if (this._ResearchNodeSubject != null)
          return (object) this._ResearchNodeSubject;
        if (this._PirateMissionSubject != null)
          return (object) this._PirateMissionSubject;
        return !this._PointSubject.IsEmpty ? (object) this._PointSubject : (object) null;
      }
      set => this.SetSubject(value);
    }

    int IComparable<EmpireMessage>.CompareTo(EmpireMessage other) => this.StarDate.CompareTo(other.StarDate);
  }
}
