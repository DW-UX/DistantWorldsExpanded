// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EventAction
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EventAction
  {
    public EventActionType Type;
    public StellarObject Target;
    public BuiltObjectSubRole BuiltObjectSubRole;
    public int TechLevel;
    public double MoneyAmount;
    public int Value = -1;
    public GalaxyLocation Location;
    public Race Race;
    public Race RaceOther;
    public Empire Empire;
    public Empire EmpireOther;
    public DiplomaticRelationType DiplomaticRelationType = DiplomaticRelationType.None;
    public bool LockedAlliance;
    public CreatureType CreatureType;
    public string MessageTitle;
    public string MessageText;
    public string ImageFilename;
    public string AllianceName;
    public EventActionExecutionType ExecutionType;
    public long ExecutionDate = -1;
    public short DelayDaysMinimum = -1;
    public short DelayDaysMaximum = -1;
    [OptionalField]
    public Character Character;
    [OptionalField]
    public CharacterRole CharacterRole;
    public List<EventActionType> ValidActionTypes = new List<EventActionType>();

    public EventAction(StellarObject target, EventActionType type)
    {
      this.Target = target;
      this.Type = type;
      this.ValidActionTypes = this.ResolveValidActionTypes(target);
    }

    public List<EventActionType> ResolveValidActionTypes(StellarObject targetObject)
    {
      List<EventActionType> eventActionTypeList = new List<EventActionType>();
      switch (targetObject)
      {
        case null:
          eventActionTypeList.Add(EventActionType.ChangeEmpireGovernment);
          eventActionTypeList.Add(EventActionType.EmpireDeclaresWarOnTriggerEmpire);
          eventActionTypeList.Add(EventActionType.EndPlague);
          eventActionTypeList.Add(EventActionType.EnemyFleetDefectsToTriggerEmpire);
          eventActionTypeList.Add(EventActionType.FindMoneyTreasure);
          eventActionTypeList.Add(EventActionType.InterceptResource);
          eventActionTypeList.Add(EventActionType.LearnAboutSpecialLocation);
          eventActionTypeList.Add(EventActionType.LearnExplorationInfo);
          eventActionTypeList.Add(EventActionType.LearnGovernmentType);
          eventActionTypeList.Add(EventActionType.LearnTech);
          eventActionTypeList.Add(EventActionType.MakeEmpireContact);
          eventActionTypeList.Add(EventActionType.PirateFactionJoinsTriggerEmpire);
          eventActionTypeList.Add(EventActionType.SplitEmpireCivilWar);
          eventActionTypeList.Add(EventActionType.SplitEmpirePeacefully);
          eventActionTypeList.Add(EventActionType.UnlockTech);
          eventActionTypeList.Add(EventActionType.ChangeRaceBias);
          eventActionTypeList.Add(EventActionType.ChangeEmpireReputation);
          eventActionTypeList.Add(EventActionType.ChangeEmpireEvaluation);
          eventActionTypeList.Add(EventActionType.InitiateTreaty);
          eventActionTypeList.Add(EventActionType.BreakTreaty);
          eventActionTypeList.Add(EventActionType.StartTradingSuperLuxuryResources);
          eventActionTypeList.Add(EventActionType.StopTradingSuperLuxuryResources);
          eventActionTypeList.Add(EventActionType.GeneralMessageToEmpire);
          eventActionTypeList.Add(EventActionType.EmpireMessageToEmpire);
          eventActionTypeList.Add(EventActionType.ResearchBonusInProject);
          eventActionTypeList.Add(EventActionType.UnlockTechForEmpire);
          eventActionTypeList.Add(EventActionType.EmpireDeclaresWarOnOtherEmpire);
          eventActionTypeList.Add(EventActionType.VictoryConditionBonus);
          eventActionTypeList.Add(EventActionType.IntergalacticConvoyMilitary);
          eventActionTypeList.Add(EventActionType.IntergalacticConvoyCivilian);
          eventActionTypeList.Add(EventActionType.CharacterKill);
          eventActionTypeList.Add(EventActionType.CharacterChangeEmpire);
          eventActionTypeList.Add(EventActionType.CharacterChangeRole);
          eventActionTypeList.Add(EventActionType.CharacterChangeImage);
          break;
        case BuiltObject _:
          eventActionTypeList.Add(EventActionType.AcquireBuiltObject);
          eventActionTypeList.Add(EventActionType.DestroyBuiltObject);
          eventActionTypeList.Add(EventActionType.RevealObject);
          eventActionTypeList.Add(EventActionType.SendFleetAttack);
          eventActionTypeList.Add(EventActionType.CharacterGenerate);
          break;
        case Habitat _:
          eventActionTypeList.Add(EventActionType.AcquireHabitat);
          eventActionTypeList.Add(EventActionType.BuildPlanetaryFacility);
          eventActionTypeList.Add(EventActionType.DestroyPlanetaryFacility);
          eventActionTypeList.Add(EventActionType.DisasterAtColony);
          eventActionTypeList.Add(EventActionType.EndPlague);
          eventActionTypeList.Add(EventActionType.GenerateBuiltObject);
          eventActionTypeList.Add(EventActionType.GenerateCreatureSwarm);
          eventActionTypeList.Add(EventActionType.GenerateNewEmpire);
          eventActionTypeList.Add(EventActionType.GenerateNewPirateFaction);
          eventActionTypeList.Add(EventActionType.GeneratePirateAmbush);
          eventActionTypeList.Add(EventActionType.GenerateRefugeeFleet);
          eventActionTypeList.Add(EventActionType.GenerateResourceAtHabitat);
          eventActionTypeList.Add(EventActionType.LearnAboutLostColony);
          eventActionTypeList.Add(EventActionType.RemoveResourceAtHabitat);
          eventActionTypeList.Add(EventActionType.SleepingRaceAwokenAtHabitat);
          eventActionTypeList.Add(EventActionType.StartPlague);
          eventActionTypeList.Add(EventActionType.RevealObject);
          eventActionTypeList.Add(EventActionType.SendFleetAttack);
          eventActionTypeList.Add(EventActionType.SendPlanetDestroyerAttack);
          eventActionTypeList.Add(EventActionType.CharacterGenerate);
          break;
      }
      return eventActionTypeList;
    }
  }
}
