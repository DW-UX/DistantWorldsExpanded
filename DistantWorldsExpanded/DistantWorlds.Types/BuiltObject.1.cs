using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    public partial class BuiltObject
    {
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
            ShipGroup shipGroup = ShipGroup;
            if (shipGroup != null)
            {
                ShipGroup = null;
                bool flag = false;
                if (shipGroup.LeadShip == this)
                {
                    flag = true;
                }
                shipGroup.Ships?.Remove(this);
                if (flag)
                {
                    shipGroup.Update();
                }
                if (shipGroup.Ships.Count <= 0)
                {
                    Empire?.DisbandShipGroup(shipGroup);
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
            if (Empire == null || Empire.PirateEmpireBaseHabitat == null || (SubRole != BuiltObjectSubRole.Outpost && SubRole != BuiltObjectSubRole.SmallSpacePort && SubRole != BuiltObjectSubRole.MediumSpacePort && SubRole != BuiltObjectSubRole.LargeSpacePort))
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
            //double num2 = 0.0;
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
            //double num2 = 0.0;
            if (WarpSpeed > 0)
            {
                return currentFuel / (((double)WarpSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)WarpSpeedWithBonuses;
            }
            return currentFuel / (((double)CruiseSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)CruiseSpeed;
        }

        public double MaximumFuelRange()
        {
            double num = FuelUnitPerEnergyUnit();
            //double num2 = 0.0;
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
                                                    case BuiltObjectSubRole.Outpost:
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
                                                    case BuiltObjectSubRole.Outpost:
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
                                //float num8 = 2f;
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
                                                        case BuiltObjectSubRole.Outpost:
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
                                        case BuiltObjectSubRole.Outpost:
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
                                            case BuiltObjectSubRole.Outpost:
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
    }
}
