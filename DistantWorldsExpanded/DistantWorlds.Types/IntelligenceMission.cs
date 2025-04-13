// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.IntelligenceMission
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class IntelligenceMission
  {
    private Empire _OriginatingEmpire;
    private Character _Agent;
    private IntelligenceMissionType _Type;
    private long _StartDate;
    private long _TimeLength;
    private IntelligenceMissionOutcome _Outcome;
    private Empire _TargetEmpire;
    private Habitat _TargetHabitat;
    private BuiltObject _TargetBuiltObject;
    private ResearchNode _TargetResearchNode;
    private Character _TargetCharacter;
    private bool _TargetIsEmpire;
    private bool _TargetIsHabitat;
    private bool _TargetIsBuiltObject;
    private bool _TargetIsResearch;
    private bool _TargetIsCharacter;

    public IntelligenceMission(
      Empire originatingEmpire,
      Character agent,
      IntelligenceMissionType type,
      long startDate,
      Empire target)
    {
      this._OriginatingEmpire = originatingEmpire;
      this._Agent = agent;
      switch (type)
      {
        case IntelligenceMissionType.StealGalaxyMap:
        case IntelligenceMissionType.StealOperationsMap:
        case IntelligenceMissionType.DeepCover:
        case IntelligenceMissionType.InciteRevolution:
        case IntelligenceMissionType.StealTerritoryMap:
          this._Type = type;
          this._StartDate = startDate;
          this._TimeLength = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 12);
          this._TargetEmpire = target;
          this._TargetIsEmpire = true;
          break;
        default:
          throw new ApplicationException("Invalid mission type");
      }
    }

    public IntelligenceMission(
      Empire originatingEmpire,
      Character agent,
      long startDate,
      Empire target,
      ResearchNode targetResearchProject)
    {
      this._OriginatingEmpire = originatingEmpire;
      this._Agent = agent;
      this._Type = IntelligenceMissionType.StealTechData;
      this._StartDate = startDate;
      this._TimeLength = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 12);
      this._TargetEmpire = target;
      this._TargetResearchNode = targetResearchProject;
      this._TargetIsResearch = true;
    }

    public IntelligenceMission(Empire originatingEmpire, Character agent, long startDate)
    {
      this._OriginatingEmpire = originatingEmpire;
      this._Agent = agent;
      this._Type = IntelligenceMissionType.CounterIntelligence;
      this._StartDate = startDate;
      this._TimeLength = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 12);
      this._TargetEmpire = originatingEmpire;
      this._TargetIsEmpire = true;
    }

    public IntelligenceMission(
      Empire originatingEmpire,
      Character agent,
      IntelligenceMissionType type,
      long startDate,
      BuiltObject targetBuiltObject)
    {
      switch (type)
      {
        case IntelligenceMissionType.SabotageConstruction:
        case IntelligenceMissionType.DestroyBase:
          this._Type = type;
          this._OriginatingEmpire = originatingEmpire;
          this._Agent = agent;
          this._Type = type;
          this._StartDate = startDate;
          this._TimeLength = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 12);
          this._TargetEmpire = targetBuiltObject.Empire;
          this._TargetBuiltObject = targetBuiltObject;
          this._TargetIsBuiltObject = true;
          break;
        default:
          throw new ApplicationException("Invalid mission type");
      }
    }

    public IntelligenceMission(
      Empire originatingEmpire,
      Character agent,
      IntelligenceMissionType type,
      long startDate,
      Habitat targetHabitat)
    {
      this._OriginatingEmpire = originatingEmpire;
      this._Agent = agent;
      switch (type)
      {
        case IntelligenceMissionType.SabotageConstruction:
        case IntelligenceMissionType.SabotageColony:
          this._Type = type;
          this._StartDate = startDate;
          this._TimeLength = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 12);
          this._TargetEmpire = targetHabitat.Empire;
          this._TargetHabitat = targetHabitat;
          this._TargetIsHabitat = true;
          break;
        default:
          throw new ApplicationException("Invalid mission type");
      }
    }

    public IntelligenceMission(
      Empire originatingEmpire,
      Character agent,
      IntelligenceMissionType type,
      long startDate,
      Character targetCharacter)
    {
      this._OriginatingEmpire = originatingEmpire;
      this._Agent = agent;
      this._Type = type == IntelligenceMissionType.AssassinateCharacter ? type : throw new ApplicationException("Invalid mission type");
      this._StartDate = startDate;
      this._TimeLength = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 12);
      this._TargetEmpire = targetCharacter.Empire;
      this._TargetCharacter = targetCharacter;
      this._TargetIsCharacter = true;
    }

    public Empire OriginatingEmpire => this._OriginatingEmpire;

    public Character Agent
    {
      get => this._Agent;
      set => this._Agent = value;
    }

    public IntelligenceMissionType Type => this._Type;

    public int Difficulty
    {
      get
      {
        int difficulty = 0;
        int num1 = 20;
        double num2 = 1.0;
        if (this._TargetEmpire != null && this._TargetEmpire.PirateEmpireBaseHabitat != null && this._OriginatingEmpire != null && this._OriginatingEmpire.PirateEmpireBaseHabitat == null)
          num2 = 2.0;
        else if (this._TargetEmpire.Reclusive)
          num2 = 3.0;
        switch (this._Type)
        {
          case IntelligenceMissionType.Undefined:
            difficulty = 0;
            break;
          case IntelligenceMissionType.SabotageConstruction:
            int num3 = (int) ((double) num1 * 1.0);
            double d1 = 1.0;
            if (this._TargetIsBuiltObject)
              d1 = (double) this._TargetBuiltObject.Size / 1000.0;
            else if (this._TargetIsHabitat)
              d1 = (double) this._TargetHabitat.StrategicValue / 30000.0;
            double num4 = Math.Sqrt(d1);
            difficulty = (int) ((double) num3 * num4 * num2);
            break;
          case IntelligenceMissionType.StealGalaxyMap:
            difficulty = (int) ((double) (int) ((double) num1 * 3.5) * num2);
            break;
          case IntelligenceMissionType.StealOperationsMap:
            difficulty = (int) ((double) (int) ((double) num1 * 2.8) * num2);
            break;
          case IntelligenceMissionType.StealTechData:
            int num5 = (int) ((double) num1 * 3.2);
            if (this._TargetIsResearch && this._TargetResearchNode != null && this._TargetResearchNode.AllowedRaces != null && this._TargetResearchNode.AllowedRaces.Count > 0 && this._Agent != null && this._Agent.Empire != null && this._Agent.Empire.DominantRace != null && !this._TargetResearchNode.AllowedRaces.Contains(this._Agent.Empire.DominantRace))
              num5 = (int) ((double) num5 * 2.0);
            difficulty = (int) ((double) num5 * num2);
            break;
          case IntelligenceMissionType.SabotageColony:
            double num6 = 1.0;
            if (this._TargetEmpire != null && this._TargetEmpire.GovernmentAttributes != null)
              num6 = Math.Sqrt(this._TargetEmpire.GovernmentAttributes.Stability);
            difficulty = (int) ((double) (int) ((double) num1 * 3.6 * num6) * ((double) this._TargetHabitat.StrategicValue / 200000.0) * num2);
            break;
          case IntelligenceMissionType.DeepCover:
            difficulty = (int) ((double) (int) ((double) num1 * 8.0) * num2);
            break;
          case IntelligenceMissionType.InciteRevolution:
            double num7 = 1.0;
            if (this._TargetEmpire != null && this._TargetEmpire.GovernmentAttributes != null)
              num7 = this._TargetEmpire.GovernmentAttributes.Stability;
            difficulty = (int) ((double) (int) ((double) num1 * 2.5 * num7) * Math.Max(1.0, Math.Min(5.0, Math.Sqrt(Math.Sqrt((double) this._TargetEmpire.TotalColonyStrategicValue / 10000.0)))));
            break;
          case IntelligenceMissionType.CounterIntelligence:
            difficulty = 0;
            break;
          case IntelligenceMissionType.StealTerritoryMap:
            difficulty = (int) ((double) (int) ((double) num1 * 2.2) * num2);
            break;
          case IntelligenceMissionType.AssassinateCharacter:
            double num8 = 1.0;
            if (this._TargetIsCharacter && this._TargetCharacter != null)
            {
              switch (this._TargetCharacter.Role)
              {
                case CharacterRole.Leader:
                case CharacterRole.PirateLeader:
                  num8 = 2.0;
                  break;
                case CharacterRole.Ambassador:
                case CharacterRole.ColonyGovernor:
                  num8 = 1.5;
                  break;
                case CharacterRole.FleetAdmiral:
                case CharacterRole.TroopGeneral:
                case CharacterRole.IntelligenceAgent:
                case CharacterRole.Scientist:
                case CharacterRole.ShipCaptain:
                  num8 = 1.0;
                  break;
              }
            }
            difficulty = (int) ((double) num1 * 8.0 * num8 * num2);
            break;
          case IntelligenceMissionType.DestroyBase:
            double num9 = 4.0;
            if (this._TargetIsBuiltObject && this._TargetBuiltObject != null && this._TargetBuiltObject.Empire != null && this._TargetBuiltObject.Empire.PirateEmpireBaseHabitat != null && (this._TargetBuiltObject.SubRole == BuiltObjectSubRole.Outpost || this._TargetBuiltObject.SubRole == BuiltObjectSubRole.SmallSpacePort || this._TargetBuiltObject.SubRole == BuiltObjectSubRole.MediumSpacePort || this._TargetBuiltObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
            {
              num9 = 4.5;
              if (this._TargetBuiltObject.Empire.PirateEmpireBaseHabitat == this._TargetBuiltObject.ParentHabitat)
                num9 = 5.0;
            }
            int num10 = (int) ((double) num1 * num9 * num2);
            double d2 = 1.0;
            if (this._TargetIsBuiltObject && this._TargetBuiltObject != null)
              d2 = (double) this._TargetBuiltObject.Size / 300.0;
            double num11 = Math.Sqrt(d2);
            difficulty = (int) ((double) num10 * num11);
            break;
        }
        return difficulty;
      }
    }

    public void ResetStartDate(long startDate) => this._StartDate = startDate;

    public long StartDate => this._StartDate;

    public long TimeLength
    {
      get => this._TimeLength;
      set => this._TimeLength = value;
    }

    public IntelligenceMissionOutcome Outcome
    {
      get => this._Outcome;
      set => this._Outcome = value;
    }

    public Empire TargetEmpire => this._TargetEmpire;

    public void ResetResearchProject(ResearchNode researchProject)
    {
      if (!this._TargetIsResearch)
        return;
      this._TargetResearchNode = researchProject;
    }

    public object Target
    {
      get
      {
        if (this._TargetIsEmpire)
          return (object) this._TargetEmpire;
        if (this._TargetIsBuiltObject)
          return (object) this._TargetBuiltObject;
        if (this._TargetIsHabitat)
          return (object) this._TargetHabitat;
        if (this._TargetIsResearch)
          return (object) this._TargetResearchNode;
        return this._TargetIsCharacter ? (object) this._TargetCharacter : (object) null;
      }
    }
  }
}
