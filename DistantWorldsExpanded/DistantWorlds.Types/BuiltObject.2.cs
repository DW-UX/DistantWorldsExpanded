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
                        galaxy.Empires[j].Outlaws.Remove(this);
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
                    CurrentTarget.Attackers.Remove(this);
                }
                num = CurrentTarget.Pursuers.IndexOf(this);
                if (num >= 0)
                {
                    CurrentTarget.Pursuers.Remove(this);
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
                    actualEmpire.Manufacturers.Remove(this);
                }
                num = actualEmpire.RefuellingDepots.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.RefuellingDepots.Remove(this);
                }
                num = actualEmpire.ResourceExtractors.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ResourceExtractors.Remove(this);
                }
                num = actualEmpire.MiningStations.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.MiningStations.Remove(this);
                }
                num = actualEmpire.SpacePorts.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.SpacePorts.Remove(this);
                }
                num = actualEmpire.ConstructionYards.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ConstructionYards.Remove(this);
                }
                num = actualEmpire.Freighters.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.Freighters.Remove(this);
                }
                num = actualEmpire.ConstructionShips.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ConstructionShips.Remove(this);
                }
                num = actualEmpire.ResearchFacilities.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ResearchFacilities.Remove(this);
                }
                num = actualEmpire.ResortBases.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.ResortBases.Remove(this);
                }
                num = actualEmpire.PlanetDestroyers.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.PlanetDestroyers.Remove(this);
                }
                num = actualEmpire.LongRangeScanners.IndexOf(this);
                if (num >= 0)
                {
                    actualEmpire.LongRangeScanners.Remove(this);
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
                lock (_disabledListLock)
                {
                    for (int num2 = list.Count - 1; num2 >= 0; num2--)
                    {
                        DisabledComponentIndexes.RemoveAt(list[num2]);
                        DisabledComponentDurations.RemoveAt(list[num2]);
                    }
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
                                            var randTroop = builtObject.Troops[Galaxy.Rnd.Next(0, builtObject.Troops.Count - 1)];
                                            if (randTroop != null)
                                            {
                                                if (builtObject.Empire != null && builtObject.Empire.Troops != null)
                                                {
                                                    builtObject.Empire.Troops.Remove(randTroop);
                                                }
                                                builtObject.Troops.Remove(randTroop);
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
            throw new NotImplementedException("FIX THIS");
            //return SortTag.CompareTo(other.SortTag);
        }

        int IComparable<BuiltObject>.CompareTo(BuiltObject other)
        {
            throw new NotImplementedException("FIX THIS");
            //return SortTag.CompareTo(other.SortTag);
        }

        int IComparable<Habitat>.CompareTo(Habitat other)
        {
            throw new NotImplementedException("FIX THIS");
            //return SortTag.CompareTo(other.SortTag);
        }

        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException("FIX THIS");
            //if (obj == this)
            //{
            //    return 0;
            //}
            //if (obj == null)
            //{
            //    return 1;
            //}
            //if (obj is BuiltObject)
            //{
            //    return SortTag.CompareTo(((BuiltObject)obj).SortTag);
            //}
            //if (obj is Habitat)
            //{
            //    return SortTag.CompareTo(((Habitat)obj).SortTag);
            //}
            //if (obj is Creature)
            //{
            //    return SortTag.CompareTo(((Creature)obj).SortTag);
            //}
            //return 0;
        }

        public override string ToString()
        {
            return Name;
        }

        int IComparable<Creature>.CompareTo(Creature other)
        {
            throw new NotImplementedException("FIX THIS");
            //return SortTag.CompareTo(other.SortTag);
        }
    }
}
