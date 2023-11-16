// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ShipGroup
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ShipGroup : IComparable<ShipGroup>
  {
    private BuiltObjectList _Ships;
    private BuiltObjectRole _Role;
    private string _Name;
    private Empire _Empire;
    private BuiltObject _LeadShip;
    private StellarObject _AttackPoint;
    private StellarObject _GatherPoint;
    public float AttackRangeSquared;
    public int ShipTargetAmount;
    public int TroopTargetStrength;
    private BuiltObjectMission _Mission;
    private BuiltObjectMissionList _SubsequentMissions = new BuiltObjectMissionList();
    public bool AllowImmediateThreatEvaluation;
    public FleetPosture Posture;
    public double PostureRangeSquared = double.MaxValue;
    private double _TargetingBonus = 1.0;
    private double _CountermeasuresBonus = 1.0;
    private double _ShipManeuveringBonus = 1.0;
    private double _FightersBonus = 1.0;
    private double _ShipEnergyUsageBonus = 1.0;
    private double _WeaponsDamageBonus = 1.0;
    private double _WeaponsRangeBonus = 1.0;
    private double _ShieldRechargeRateBonus = 1.0;
    private double _DamageControlBonus = 1.0;
    private double _RepairBonus = 1.0;
    private double _HyperjumpSpeedBonus = 1.0;
    private double _TargetingBonusExtra;
    private double _CountermeasuresBonusExtra;
    private double _ShipManeuveringBonusExtra;
    private double _FightersBonusExtra;
    private double _ShipEnergyUsageBonusExtra;
    private double _WeaponsDamageBonusExtra;
    private double _WeaponsRangeBonusExtra;
    private double _ShieldRechargeRateBonusExtra;
    private double _DamageControlBonusExtra;
    private double _RepairBonusExtra;
    private double _HyperjumpSpeedBonusExtra;
    public byte TroopLoadoutInfantry;
    public byte TroopLoadoutArmored;
    public byte TroopLoadoutArtillery;
    public byte TroopLoadoutSpecialForces;
    public SpaceBattleStats BattleStats;
    private Galaxy _Galaxy;
    private DateTime _LastTouch;
    private DateTime _LastPeriodicTouch;
    [NonSerialized]
    public double SortTag;

    public bool LocalDefenseTacticsApply => this._TargetingBonusExtra > 0.0 || this._CountermeasuresBonusExtra > 0.0;

    public double TargetingBonus => this._TargetingBonus + this._TargetingBonusExtra;

    public double CountermeasuresBonus => this._CountermeasuresBonus + this._CountermeasuresBonusExtra;

    public double ShipManeuveringBonus => this._ShipManeuveringBonus + this._ShipManeuveringBonusExtra;

    public double FightersBonus => this._FightersBonus + this._FightersBonusExtra;

    public double ShipEnergyUsageBonus => this._ShipEnergyUsageBonus + this._ShipEnergyUsageBonusExtra;

    public double WeaponsDamageBonus => this._WeaponsDamageBonus + this._WeaponsDamageBonusExtra;

    public double WeaponsRangeBonus => this._WeaponsRangeBonus + this._WeaponsRangeBonusExtra;

    public double ShieldRechargeRateBonus => this._ShieldRechargeRateBonus + this._ShieldRechargeRateBonusExtra;

    public double DamageControlBonus => this._DamageControlBonus + this._DamageControlBonusExtra;

    public double RepairBonus => this._RepairBonus + this._RepairBonusExtra;

    public double HyperjumpSpeedBonus => this._HyperjumpSpeedBonus + this._HyperjumpSpeedBonusExtra;

    public ShipGroup(Galaxy galaxy)
    {
      this._Galaxy = galaxy;
      this._Ships = new BuiltObjectList();
    }

    public BuiltObjectMissionList SubsequentMissions => this._SubsequentMissions;

    public void DoTasks(DateTime time)
    {
      if (this._LastTouch == DateTime.MinValue)
        this._LastTouch = time;
      if (time.Subtract(this._LastTouch) >= Galaxy.IntermediateProcessingSpan)
      {
        this.CheckForMissionCompletion();
        this.CheckForCompletedBattle();
        this._LastTouch = time;
      }
      if (!(time.Subtract(this._LastPeriodicTouch) >= Galaxy.PeriodicProcessingSpan))
        return;
      this.CheckRefuelManual();
      this.CheckRefuelRepairAttack(false, (Empire) null);
      this.CheckSendForAttack();
      this.ReviewCharacterLocationBonuses();
      if (this.LeadShip != null && this.LeadShip.IsAutoControlled)
      {
        bool flag = false;
        if (this.Empire != null)
          flag = this.Empire.CheckAssignUnloadTroopsAtColonyNeedingThemMission(this);
        if (!flag)
          this.LoadTroopsIfNecessaryAndPossible();
      }
      this._LastPeriodicTouch = time;
    }

    public void LoadTroopsIfNecessaryAndPossible()
    {
      BuiltObjectMission mission = this.Mission;
      if (mission != null && mission.Type != BuiltObjectMissionType.Undefined || this.CheckAnyShipsInBattle())
        return;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null && ship.IsAutoControlled && (ship.Mission == null || ship.Mission.Type == BuiltObjectMissionType.Undefined) && this.IsShipAvailable(ship) && ship.Troops != null && ship.TroopCapacity - ship.Troops.TotalSize >= 100 && Galaxy.Rnd.Next(0, 2) == 1)
          this.Empire.AssignLoadTroopsMission(ship, (Habitat) null, false, false, false);
      }
    }

    public void SetTroopLoadoutsFromPolicy(EmpirePolicy policy)
    {
      if (policy == null)
        return;
      if (policy.TroopUseDefaultTransportLoadout)
      {
        float num = 1f / (policy.TroopDefaultTransportLoadoutInfantry + policy.TroopDefaultTransportLoadoutArmor + policy.TroopDefaultTransportLoadoutArtillery + policy.TroopDefaultTransportLoadoutSpecialForces);
        this.TroopLoadoutInfantry = (byte) (100.0 * (double) policy.TroopDefaultTransportLoadoutInfantry * (double) num);
        this.TroopLoadoutArmored = (byte) (100.0 * (double) policy.TroopDefaultTransportLoadoutArmor * (double) num);
        this.TroopLoadoutArtillery = (byte) (100.0 * (double) policy.TroopDefaultTransportLoadoutArtillery * (double) num);
        this.TroopLoadoutSpecialForces = (byte) (100.0 * (double) policy.TroopDefaultTransportLoadoutSpecialForces * (double) num);
      }
      else
      {
        this.TroopLoadoutInfantry = byte.MaxValue;
        this.TroopLoadoutArmored = byte.MaxValue;
        this.TroopLoadoutArtillery = byte.MaxValue;
        this.TroopLoadoutSpecialForces = byte.MaxValue;
      }
    }

    public long CalculateTimeToArrivalAtDestination()
    {
      long arrivalAtDestination = 0;
      if (this.Mission != null && this.Mission.Type != BuiltObjectMissionType.Undefined && this.LeadShip != null)
      {
        Point point = this.Mission.ResolveTargetCoordinates(this.Mission);
        if (point.X > 0 && point.Y > 0)
          arrivalAtDestination = (long) (this._Galaxy.CalculateDistance(this.LeadShip.Xpos, this.LeadShip.Ypos, (double) point.X, (double) point.Y) / (double) Math.Max(30, this.WarpSpeed) * 1000.0);
      }
      return arrivalAtDestination;
    }

    private bool CheckSendForAttack()
    {
      if (this.Posture == FleetPosture.Attack && this.AttackPoint != null)
      {
        StellarObject attackPoint = this.AttackPoint;
        if (this.Mission == null || this.Mission.Type == BuiltObjectMissionType.Undefined || this.Mission.Priority == BuiltObjectMissionPriority.Low)
        {
          Empire empire = attackPoint.Empire;
          if (empire != null && this.Empire != null && empire != this.Empire && this.Empire.CheckAtWarWithEmpire(empire) && this.CheckFleetTargetWithinFuelRangeAndRefuel(attackPoint.Xpos, attackPoint.Ypos, 0.1))
          {
            BuiltObjectMissionType missionType = BuiltObjectMissionType.Attack;
            if (attackPoint is Habitat && this._Empire.CheckBombardEnemyColony((Habitat) attackPoint, this))
              missionType = BuiltObjectMissionType.Bombard;
            this.AssignMission(missionType, (object) attackPoint, (object) null, BuiltObjectMissionPriority.High, false);
            return true;
          }
        }
      }
      return false;
    }

    private bool CheckForCompletedBattle()
    {
      SpaceBattleStats battleStats = this.BattleStats;
      if (battleStats != null)
      {
        bool flag = false;
        if (this.Mission != null && (this.Mission.Type == BuiltObjectMissionType.Attack || this.Mission.Type == BuiltObjectMissionType.WaitAndAttack || this.Mission.Type == BuiltObjectMissionType.Bombard || this.Mission.Type == BuiltObjectMissionType.WaitAndBombard || this.Mission.Type == BuiltObjectMissionType.Capture || this.Mission.Type == BuiltObjectMissionType.Raid))
          flag = true;
        if (!flag)
        {
          for (int index = 0; index < this.Ships.Count; ++index)
          {
            BuiltObject ship = this.Ships[index];
            if (ship != null && ship.Mission != null && (ship.Mission.Type == BuiltObjectMissionType.Attack || ship.Mission.Type == BuiltObjectMissionType.WaitAndAttack || ship.Mission.Type == BuiltObjectMissionType.Bombard || ship.Mission.Type == BuiltObjectMissionType.WaitAndBombard || ship.Mission.Type == BuiltObjectMissionType.Capture || ship.Mission.Type == BuiltObjectMissionType.Raid))
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag && this.LeadShip != null)
        {
          bool nearby = true;
          battleStats.Location = this._Galaxy.ResolveNearestLocation((StellarObject) null, this.LeadShip, out nearby);
          battleStats.NearLocation = nearby;
          this._Galaxy.DoCharacterEvent(CharacterEventType.SpaceBattle, (object) battleStats, this.ObtainCharacters());
          this.BattleStats = (SpaceBattleStats) null;
          return true;
        }
      }
      return false;
    }

    private bool CheckShouldClearBattleStats()
    {
      bool flag = true;
      if (this.LeadShip != null && this.LeadShip.TotalThreatLevel > 0)
        flag = false;
      return flag;
    }

    public void ReviewAdmiralBonuses()
    {
      if (this.Empire != null && this.Empire.Characters != null)
      {
        int val1_1 = -100;
        int val1_2 = -100;
        int val1_3 = -100;
        int val1_4 = -100;
        int val1_5 = -100;
        int val1_6 = -100;
        int val1_7 = -100;
        int val1_8 = -100;
        int val1_9 = -100;
        int val1_10 = -100;
        int val1_11 = -100;
        CharacterList admiralsAndGenerals = this.Empire.Characters.GetFleetAdmiralsAndGenerals(this);
        for (int index = 0; index < admiralsAndGenerals.Count; ++index)
        {
          Character character = admiralsAndGenerals[index];
          if (character != null && (character.Role == CharacterRole.FleetAdmiral || character.Role == CharacterRole.PirateLeader) && character.BonusesKnown)
          {
            val1_1 = Math.Max(val1_1, character.Targeting);
            val1_2 = Math.Max(val1_2, character.Countermeasures);
            val1_3 = Math.Max(val1_3, character.ShipManeuvering);
            val1_4 = Math.Max(val1_4, character.Fighters);
            val1_5 = Math.Max(val1_5, character.ShipEnergyUsage);
            val1_6 = Math.Max(val1_6, character.WeaponsDamage);
            val1_7 = Math.Max(val1_7, character.WeaponsRange);
            val1_8 = Math.Max(val1_8, character.ShieldRechargeRate);
            val1_9 = Math.Max(val1_9, character.DamageControl);
            val1_10 = Math.Max(val1_10, character.RepairBonus);
            val1_11 = Math.Max(val1_11, character.HyperjumpSpeed);
          }
        }
        if (val1_1 <= -100)
          val1_1 = 0;
        if (val1_2 <= -100)
          val1_2 = 0;
        if (val1_3 <= -100)
          val1_3 = 0;
        if (val1_4 <= -100)
          val1_4 = 0;
        if (val1_5 <= -100)
          val1_5 = 0;
        if (val1_6 <= -100)
          val1_6 = 0;
        if (val1_7 <= -100)
          val1_7 = 0;
        if (val1_8 <= -100)
          val1_8 = 0;
        if (val1_9 <= -100)
          val1_9 = 0;
        if (val1_10 <= -100)
          val1_10 = 0;
        if (val1_11 <= -100)
          val1_11 = 0;
        this._TargetingBonus = 1.0 + (double) val1_1 / 100.0;
        this._CountermeasuresBonus = 1.0 + (double) val1_2 / 100.0;
        this._ShipManeuveringBonus = 1.0 + (double) val1_3 / 100.0;
        this._FightersBonus = 1.0 + (double) val1_4 / 100.0;
        this._ShipEnergyUsageBonus = 1.0 + (double) val1_5 / 100.0;
        this._WeaponsDamageBonus = 1.0 + (double) val1_6 / 100.0;
        this._WeaponsRangeBonus = 1.0 + (double) val1_7 / 100.0;
        this._ShieldRechargeRateBonus = 1.0 + (double) val1_8 / 100.0;
        this._DamageControlBonus = 1.0 + (double) val1_9 / 100.0;
        this._RepairBonus = 1.0 + (double) val1_10 / 100.0;
        this._HyperjumpSpeedBonus = 1.0 + (double) val1_11 / 100.0;
      }
      else
      {
        this._TargetingBonus = 1.0;
        this._CountermeasuresBonus = 1.0;
        this._ShipManeuveringBonus = 1.0;
        this._FightersBonus = 1.0;
        this._ShipEnergyUsageBonus = 1.0;
        this._WeaponsDamageBonus = 1.0;
        this._WeaponsRangeBonus = 1.0;
        this._ShieldRechargeRateBonus = 1.0;
        this._DamageControlBonus = 1.0;
        this._RepairBonus = 1.0;
        this._HyperjumpSpeedBonus = 1.0;
      }
    }

    public void ReviewCharacterLocationBonuses()
    {
      bool flag1 = false;
      if (this.Empire != null && this.Empire.Characters != null)
      {
        bool flag2 = false;
        CharacterList admiralsAndGenerals = this.Empire.Characters.GetFleetAdmiralsAndGenerals(this);
        for (int index = 0; index < admiralsAndGenerals.Count; ++index)
        {
          Character character = admiralsAndGenerals[index];
          if ((character.Role == CharacterRole.FleetAdmiral || character.Role == CharacterRole.PirateLeader) && character.Traits.Contains(CharacterTraitType.LocalDefenseTactics))
          {
            flag2 = true;
            break;
          }
        }
        if (flag2)
        {
          bool flag3 = false;
          if (this.LeadShip != null)
          {
            Habitat nearestColony = this._Galaxy.FastFindNearestColony(this.LeadShip.Xpos, this.LeadShip.Ypos, this.Empire, 0);
            if (nearestColony != null && this._Galaxy.CalculateDistanceSquared(this.LeadShip.Xpos, this.LeadShip.Ypos, nearestColony.Xpos, nearestColony.Ypos) < 4000000.0)
              flag3 = true;
            if (!flag3 && this.LeadShip.NearestSystemStar != null)
            {
              BuiltObjectList basesInSystem = this._Galaxy.FastFindBasesInSystem(this.Empire, this.LeadShip.NearestSystemStar);
              for (int index = 0; index < basesInSystem.Count; ++index)
              {
                BuiltObject builtObject = basesInSystem[index];
                if (this._Galaxy.CalculateDistanceSquared(this.LeadShip.Xpos, this.LeadShip.Ypos, builtObject.Xpos, builtObject.Ypos) < 4000000.0)
                {
                  flag3 = true;
                  break;
                }
              }
            }
          }
          if (flag3)
            flag1 = true;
        }
      }
      if (flag1)
      {
        this._TargetingBonusExtra = 0.2;
        this._CountermeasuresBonusExtra = 0.2;
      }
      else
      {
        this._TargetingBonusExtra = 0.0;
        this._CountermeasuresBonusExtra = 0.0;
      }
    }

    public void AddShipToFleet(BuiltObject ship)
    {
      ship.AttackRangeSquared = this.AttackRangeSquared;
      if (ship.ShipGroup != null)
      {
        if (ship.ShipGroup == this)
          return;
        ship.LeaveShipGroup();
      }
      ship.ShipGroup = this;
      this.Ships.Add(ship);
      if (this.LeadShip != null)
        return;
      this.LeadShip = ship;
    }

    private string ResolveAttackWarningDescription(FleetAttack fleetAttack, Empire targetEmpire)
    {
      string str1 = string.Empty;
      if (fleetAttack.Fleet != null && fleetAttack.Fleet.Mission != null)
      {
        string str2 = string.Empty;
        if (fleetAttack.Fleet.Mission.TargetBuiltObject != null)
          str2 = fleetAttack.Fleet.Mission.TargetBuiltObject.Name;
        else if (fleetAttack.Fleet.Mission.TargetCreature != null)
          str2 = fleetAttack.Fleet.Mission.TargetCreature.Name;
        else if (fleetAttack.Fleet.Mission.TargetHabitat != null)
        {
          Habitat targetHabitat = fleetAttack.Fleet.Mission.TargetHabitat;
          Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(targetHabitat);
          string str3 = string.Empty;
          if (habitatSystemStar != null)
            str3 = habitatSystemStar.Name;
          string str4 = Galaxy.ResolveSectorDescription(this._Galaxy.ResolveSector(targetHabitat.Xpos, targetHabitat.Ypos));
          str2 = string.Format(TextResolver.GetText("Location Planet"), (object) Galaxy.ResolveDescription(targetHabitat.Type).ToLower(CultureInfo.InvariantCulture), (object) Galaxy.ResolveDescription(targetHabitat.Category).ToLower(CultureInfo.InvariantCulture), (object) targetHabitat.Name, (object) str3, (object) str4);
        }
        else if (fleetAttack.Fleet.Mission.TargetShipGroup != null)
          str2 = fleetAttack.Fleet.Mission.TargetShipGroup.Name;
        str1 = string.Format(TextResolver.GetText("Incoming Enemy Fleet"), (object) fleetAttack.Fleet.Name, (object) fleetAttack.Fleet.Empire.Name, (object) str2);
      }
      return str1;
    }

    private void WarnIncomingFleet()
    {
      if (this.Mission == null || this.LeadShip == null)
        return;
      bool flag = false;
      if (this.Mission.Type == BuiltObjectMissionType.Attack || this.Mission.Type == BuiltObjectMissionType.Bombard)
        flag = true;
      else if (this.Mission.Type == BuiltObjectMissionType.WaitAndAttack)
      {
        Command command = this.LeadShip.Mission.FastPeekCurrentCommand();
        if (command != null && command.Action == CommandAction.Attack)
          flag = true;
      }
      else if (this.Mission.Type == BuiltObjectMissionType.WaitAndBombard)
      {
        Command command = this.LeadShip.Mission.FastPeekCurrentCommand();
        if (command != null && command.Action == CommandAction.Bombard)
          flag = true;
      }
      if (!flag)
        return;
      Empire empire = BuiltObjectMission.ResolveMissionTargetEmpire(this.Mission);
      int index = empire.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(this);
      if (index >= 0)
      {
        FleetAttack andPlanetDestroyer = empire.IncomingEnemyFleetsAndPlanetDestroyers[index];
        if (andPlanetDestroyer.Target == this.Mission.Target || this.Mission.Target == null || empire.ObtainDiplomaticRelation(this.Empire).Type != DiplomaticRelationType.War || !empire.IsObjectVisibleToThisEmpire((StellarObject) this.LeadShip))
          return;
        andPlanetDestroyer.Target = this.Mission.Target;
        andPlanetDestroyer.WarningDate = this._Galaxy.CurrentStarDate;
        string description = this.ResolveAttackWarningDescription(andPlanetDestroyer, empire);
        empire.SendMessageToEmpire(empire, EmpireMessageType.IncomingEnemyFleet, (object) this, description);
      }
      else
      {
        if (empire.ObtainDiplomaticRelation(this.Empire).Type != DiplomaticRelationType.War || !empire.IsObjectVisibleToThisEmpire((StellarObject) this.LeadShip))
          return;
        FleetAttack fleetAttack = new FleetAttack(this, this.Mission.Target, this._Galaxy.CurrentStarDate);
        empire.IncomingEnemyFleetsAndPlanetDestroyers.Add(fleetAttack);
        string description = this.ResolveAttackWarningDescription(fleetAttack, empire);
        empire.SendMessageToEmpire(empire, EmpireMessageType.IncomingEnemyFleet, (object) this, description);
      }
    }

    private bool CheckCloseToTargetAndStillTravellingToTarget(BuiltObjectMission mission)
    {
      bool flag = true;
      Point point1 = mission.ResolveTargetCoordinates(mission);
      if (this.LeadShip != null && this._Galaxy.CalculateDistance(this.LeadShip.Xpos, this.LeadShip.Ypos, (double) point1.X, (double) point1.Y) > (double) Galaxy.MaxSolarSystemSize * 2.1)
        flag = false;
      if (flag)
        return true;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if ((double) ship.CurrentSpeed > 0.0 && (double) ship.CurrentSpeed > (double) ship.TopSpeed && ship.Mission != null)
        {
          Point point2 = ship.Mission.ResolveTargetCoordinates(ship.Mission);
          if (this._Galaxy.CalculateDistance((double) point1.X, (double) point1.Y, (double) point2.X, (double) point2.Y) < 1000.0)
            return false;
        }
      }
      return true;
    }

    private void CheckForMissionCompletion_NEW()
    {
      if (this.Mission == null || this.Mission.Type == BuiltObjectMissionType.Undefined)
        return;
      bool flag = true;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship.Mission != null && ship.Mission.Type == this.Mission.Type && ship.Mission.Target == this.Mission.Target)
        {
          flag = false;
          break;
        }
        switch (this.Mission.Type)
        {
          case BuiltObjectMissionType.Patrol:
          case BuiltObjectMissionType.Escort:
          case BuiltObjectMissionType.Blockade:
            if ((ship.Mission.Type == BuiltObjectMissionType.Attack || ship.Mission.Type == BuiltObjectMissionType.Escape || ship.Mission.Type == BuiltObjectMissionType.Refuel || ship.Mission.Type == BuiltObjectMissionType.Repair) && ship.RevertMission != null && ship.RevertMission.Type == this.Mission.Type && ship.RevertMission.Target == this.Mission.Target)
            {
              flag = false;
              break;
            }
            break;
          case BuiltObjectMissionType.Attack:
            if ((ship.Mission.Type == BuiltObjectMissionType.Escape || ship.Mission.Type == BuiltObjectMissionType.Refuel || ship.Mission.Type == BuiltObjectMissionType.Repair) && ship.RevertMission != null && ship.RevertMission.Type == this.Mission.Type && ship.RevertMission.Target == this.Mission.Target)
            {
              flag = false;
              break;
            }
            break;
        }
      }
      if (!flag)
        return;
      this.CompleteMission(false);
    }

    private void CheckForMissionCompletion()
    {
      try
      {
        BuiltObjectMission mission1 = this.Mission;
        if (mission1 == null || mission1.Type == BuiltObjectMissionType.Undefined)
          return;
        double x2 = -2000000001.0;
        double y2 = -2000000001.0;
        int num1 = 0;
        bool flag1 = true;
        double parentRelativeRange = (double) Galaxy.ParentRelativeRange;
        bool flag2 = false;
        Empire empire1 = (Empire) null;
        StellarObject attackedTarget = (StellarObject) null;
        BuiltObject targetBuiltObject = mission1.TargetBuiltObject;
        Habitat targetHabitat = mission1.TargetHabitat;
        switch (mission1.Type)
        {
          case BuiltObjectMissionType.Patrol:
            int num2 = 0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null)
              {
                BuiltObjectMission mission2 = ship.Mission;
                if (mission2 != null && mission2.Type == BuiltObjectMissionType.Patrol)
                  ++num2;
              }
            }
            if (num2 == 0 && this.CompleteMission())
              return;
            break;
          case BuiltObjectMissionType.Attack:
          case BuiltObjectMissionType.WaitAndAttack:
            if (mission1.TargetBuiltObject != null || mission1.TargetHabitat != null || mission1.TargetShipGroup != null)
            {
              if (targetBuiltObject != null)
              {
                BuiltObject builtObject = targetBuiltObject;
                if (builtObject.HasBeenDestroyed)
                {
                  if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                  {
                    flag2 = true;
                    empire1 = builtObject.Empire;
                    attackedTarget = (StellarObject) builtObject;
                    break;
                  }
                  break;
                }
                if (builtObject.Empire == this.Empire)
                {
                  if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                  {
                    flag2 = true;
                    attackedTarget = (StellarObject) builtObject;
                    break;
                  }
                  break;
                }
              }
              if (mission1.TargetCreature != null)
              {
                if (mission1.TargetCreature.HasBeenDestroyed)
                {
                  if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                  {
                    this.ForceCompleteMission();
                    break;
                  }
                  break;
                }
              }
              else if (targetHabitat != null)
              {
                Habitat habitat = targetHabitat;
                if (habitat.Owner == this.Empire || habitat.Owner == null)
                {
                  if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                  {
                    flag2 = true;
                    attackedTarget = (StellarObject) habitat;
                    break;
                  }
                  break;
                }
                if (habitat.InvadingTroops == null || habitat.InvadingTroops.TotalAttackStrength == 0)
                {
                  bool flag3 = false;
                  bool flag4 = false;
                  for (int index = 0; index < this.Ships.Count; ++index)
                  {
                    BuiltObject ship = this.Ships[index];
                    if (ship != null)
                    {
                      BuiltObjectMission mission3 = ship.Mission;
                      if (mission3 != null && (mission3.Type == BuiltObjectMissionType.Attack || mission3.Type == BuiltObjectMissionType.WaitAndAttack) && mission3.TargetHabitat == habitat && ship.Troops != null && ship.Troops.TotalAttackStrength > 0)
                      {
                        flag3 = true;
                        break;
                      }
                      if (this.CheckShipMissionContainsHyperAndOtherAction(ship, CommandAction.Attack, (StellarObject) mission1.TargetHabitat))
                      {
                        flag4 = true;
                        break;
                      }
                    }
                  }
                  if (!flag3 && !flag4)
                  {
                    flag2 = true;
                    empire1 = habitat.Empire;
                    attackedTarget = (StellarObject) habitat;
                    break;
                  }
                  if (this.TotalTroopAttackStrength == 0 && !flag4)
                  {
                    if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                    {
                      flag2 = true;
                      empire1 = habitat.Empire;
                      attackedTarget = (StellarObject) habitat;
                      break;
                    }
                    break;
                  }
                }
              }
              else if (mission1.TargetShipGroup != null)
              {
                bool flag5 = false;
                for (int index = 0; index < this.Ships.Count; ++index)
                {
                  BuiltObject ship = this.Ships[index];
                  if (ship != null)
                  {
                    BuiltObjectMission mission4 = ship.Mission;
                    if (mission4 != null && (mission4.Type == BuiltObjectMissionType.Attack || mission4.Type == BuiltObjectMissionType.WaitAndAttack) && (mission4.TargetShipGroup == mission1.TargetShipGroup || mission4.TargetBuiltObject != null && mission4.TargetBuiltObject.ShipGroup == mission1.TargetShipGroup))
                    {
                      flag5 = true;
                      break;
                    }
                  }
                }
                if (!flag5)
                {
                  flag2 = true;
                  empire1 = mission1.TargetShipGroup.Empire;
                  break;
                }
              }
            }
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null && ship.IsFunctional && ship.TopSpeed > (short) 0 && ship.DockedAt == null && ship.BuiltAt == null)
              {
                BuiltObjectMission mission5 = ship.Mission;
                if (mission5 != null && (mission5.Type == BuiltObjectMissionType.Attack || mission5.Type == BuiltObjectMissionType.WaitAndAttack))
                {
                  if (targetHabitat != null)
                  {
                    if (targetHabitat == mission5.TargetHabitat)
                      ++num1;
                    else if (mission5.TargetBuiltObject != null && targetHabitat.BasesAtHabitat != null && targetHabitat.BasesAtHabitat.Contains(mission5.TargetBuiltObject))
                      ++num1;
                  }
                  if (targetBuiltObject != null && targetBuiltObject == mission5.TargetBuiltObject)
                    ++num1;
                  if (mission1.TargetCreature != null && mission1.TargetCreature == mission5.TargetCreature)
                    ++num1;
                  if (mission1.TargetShipGroup != null)
                  {
                    if (mission5.TargetBuiltObject != null)
                    {
                      if (mission5.TargetBuiltObject.ShipGroup == mission1.TargetShipGroup)
                        ++num1;
                    }
                    else if (mission1.TargetShipGroup == mission5.TargetShipGroup)
                      ++num1;
                  }
                }
              }
            }
            if (num1 == 0)
            {
              flag2 = true;
              if (mission1.TargetHabitat != null)
                attackedTarget = (StellarObject) mission1.TargetHabitat;
              if (mission1.TargetBuiltObject != null)
              {
                attackedTarget = (StellarObject) mission1.TargetBuiltObject;
                break;
              }
              break;
            }
            break;
          case BuiltObjectMissionType.Escape:
            int num3 = 0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null)
              {
                BuiltObjectMission mission6 = ship.Mission;
                if (mission6 != null && mission6.Type == BuiltObjectMissionType.Escape)
                  ++num3;
              }
            }
            if (num3 == 0 && this.CompleteMission())
              return;
            break;
          case BuiltObjectMissionType.Retrofit:
            int num4 = 0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null)
              {
                BuiltObjectMission mission7 = ship.Mission;
                if (mission7 != null && (mission7.Type == BuiltObjectMissionType.Retrofit || mission7.Type == BuiltObjectMissionType.Refuel) || ship.DockedAt != null || ship.BuiltAt != null)
                  ++num4;
              }
            }
            if (num4 <= 0 && this.CompleteMission())
              return;
            break;
          case BuiltObjectMissionType.Waypoint:
            if (targetBuiltObject != null || targetHabitat != null || mission1.TargetShipGroup != null)
            {
              if (targetBuiltObject != null)
              {
                x2 = targetBuiltObject.Xpos;
                y2 = targetBuiltObject.Ypos;
              }
              else if (targetHabitat != null)
              {
                x2 = targetHabitat.Xpos;
                y2 = targetHabitat.Ypos;
              }
              else if (mission1.TargetShipGroup != null)
              {
                ShipGroup targetShipGroup = mission1.TargetShipGroup;
                if (targetShipGroup.LeadShip != null)
                {
                  x2 = targetShipGroup.LeadShip.Xpos;
                  y2 = targetShipGroup.LeadShip.Ypos;
                }
                else
                {
                  this.CompleteMission();
                  break;
                }
              }
            }
            if ((double) mission1.X > -2000000000.0 && (double) mission1.Y > -2000000000.0)
            {
              x2 = (double) mission1.X;
              y2 = (double) mission1.Y;
            }
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null && this.IsShipAvailable(ship))
              {
                double distance = this._Galaxy.CalculateDistance(ship.Xpos, ship.Ypos, x2, y2);
                BuiltObjectMission mission8 = ship.Mission;
                if (distance > parentRelativeRange && mission8 != null && mission8.Type == BuiltObjectMissionType.Waypoint)
                {
                  flag1 = false;
                  break;
                }
              }
            }
            if (flag1 && this.CompleteMission())
              return;
            break;
          case BuiltObjectMissionType.Hold:
            if (this._Galaxy.CurrentStarDate >= mission1.StarDate && this.CompleteMission())
              return;
            break;
          case BuiltObjectMissionType.WaitAndBombard:
          case BuiltObjectMissionType.Bombard:
            if (targetHabitat != null)
            {
              Habitat habitat = targetHabitat;
              if (habitat.Owner == this.Empire || habitat.Owner == null)
              {
                if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                {
                  flag2 = true;
                  attackedTarget = (StellarObject) habitat;
                  break;
                }
                break;
              }
              if (habitat.Population == null || habitat.Population.Count == 0 || habitat.Population.TotalAmount <= 0L)
              {
                if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                {
                  flag2 = true;
                  attackedTarget = (StellarObject) habitat;
                  break;
                }
                break;
              }
            }
            bool flag6 = false;
            int num5 = 0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null && ship.BombardWeaponPower > 0)
              {
                BuiltObjectMission mission9 = ship.Mission;
                if (mission9 != null && mission9.TargetHabitat == targetHabitat && (mission9.Type == BuiltObjectMissionType.Bombard || mission9.Type == BuiltObjectMissionType.WaitAndBombard))
                  num5 += ship.BombardWeaponPower;
              }
              else if (this.CheckShipMissionContainsHyperAndOtherAction(ship, CommandAction.Bombard, (StellarObject) targetHabitat))
              {
                flag6 = true;
                break;
              }
            }
            if (num5 == 0 && !flag6)
            {
              flag2 = true;
              break;
            }
            break;
          case BuiltObjectMissionType.MoveAndWait:
            if (this._Galaxy.CurrentStarDate >= mission1.StarDate && this.CompleteMission())
              return;
            break;
          case BuiltObjectMissionType.Refuel:
            int num6 = 0;
            int num7 = 0;
            BuiltObjectList builtObjectList1 = new BuiltObjectList();
            Point point = mission1.ResolveTargetCoordinates(mission1);
            double num8 = 2304000000.0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null)
              {
                BuiltObjectMission mission10 = ship.Mission;
                if (mission10 != null && mission10.Type == BuiltObjectMissionType.Refuel && mission10.Target == mission1.Target)
                {
                  if (this._Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, (double) point.X, (double) point.Y) > num8)
                    builtObjectList1.Add(ship);
                  foreach (Command showAllCommand in mission10.ShowAllCommands())
                  {
                    if (showAllCommand.Action == CommandAction.Undock)
                    {
                      ++num6;
                      break;
                    }
                  }
                }
                else
                  ++num7;
              }
            }
            if (num7 > this.Ships.Count / 2 && builtObjectList1.Count > 0)
            {
              double num9 = 0.26;
              if (this.Ships.Count > 10)
                num9 = 0.15;
              if ((double) builtObjectList1.Count / (double) this.Ships.Count < num9)
                num6 = 0;
            }
            if (num6 == 0 && this.CompleteMission(false))
              return;
            break;
          case BuiltObjectMissionType.LoadTroops:
            int num10 = 0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null)
              {
                BuiltObjectMission mission11 = ship.Mission;
                if (mission11 != null && mission11.Type == BuiltObjectMissionType.LoadTroops)
                {
                  foreach (Command showAllCommand in mission11.ShowAllCommands())
                  {
                    if (showAllCommand != null && showAllCommand.Action == CommandAction.Undock)
                    {
                      ++num10;
                      break;
                    }
                  }
                }
              }
            }
            if (num10 == 0 && this.CompleteMission(false))
              return;
            break;
          case BuiltObjectMissionType.UnloadTroops:
            int num11 = 0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null)
              {
                BuiltObjectMission mission12 = ship.Mission;
                if (mission12 != null && mission12.Type == BuiltObjectMissionType.UnloadTroops)
                {
                  foreach (Command showAllCommand in mission12.ShowAllCommands())
                  {
                    if (showAllCommand != null && showAllCommand.Action == CommandAction.Undock)
                    {
                      ++num11;
                      break;
                    }
                  }
                }
              }
            }
            if (num11 == 0 && this.CompleteMission(false))
              return;
            break;
          case BuiltObjectMissionType.Repair:
            int num12 = 0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null)
              {
                BuiltObjectMission mission13 = ship.Mission;
                if (mission13 != null && (mission13.Type == BuiltObjectMissionType.Repair || mission13.Type == BuiltObjectMissionType.Refuel) || ship.DockedAt != null || ship.BuiltAt != null)
                  ++num12;
              }
            }
            if (num12 <= 0 && this.CompleteMission())
              return;
            break;
          case BuiltObjectMissionType.Move:
            if (targetBuiltObject != null || targetHabitat != null || mission1.TargetShipGroup != null)
            {
              if (targetBuiltObject != null)
              {
                x2 = targetBuiltObject.Xpos;
                y2 = targetBuiltObject.Ypos;
              }
              else if (targetHabitat != null)
              {
                x2 = targetHabitat.Xpos;
                y2 = targetHabitat.Ypos;
              }
              else if (mission1.TargetShipGroup != null)
              {
                ShipGroup targetShipGroup = mission1.TargetShipGroup;
                if (targetShipGroup.LeadShip != null)
                {
                  x2 = targetShipGroup.LeadShip.Xpos;
                  y2 = targetShipGroup.LeadShip.Ypos;
                }
                else
                {
                  this.CompleteMission();
                  break;
                }
              }
            }
            if ((double) mission1.X > -2000000000.0 && (double) mission1.Y > -2000000000.0)
            {
              x2 = (double) mission1.X;
              y2 = (double) mission1.Y;
            }
            int num13 = 0;
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            double num14 = 48000.0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null && this.IsShipAvailable(ship))
              {
                double distance = this._Galaxy.CalculateDistance(ship.Xpos, ship.Ypos, x2, y2);
                if (distance > num14)
                  builtObjectList2.Add(ship);
                else
                  ++num13;
                BuiltObjectMission mission14 = ship.Mission;
                if (distance > parentRelativeRange && mission14 != null && mission14.Type == BuiltObjectMissionType.Move)
                  flag1 = false;
              }
            }
            if (num13 > this.Ships.Count / 2 && builtObjectList2.Count > 0)
            {
              double num15 = 0.26;
              if (this.Ships.Count > 10)
                num15 = 0.15;
              if ((double) builtObjectList2.Count / (double) this.Ships.Count < num15 && !builtObjectList2.Contains(this.LeadShip))
                flag1 = true;
            }
            if (flag1 && this.CompleteMission(false))
              return;
            break;
          case BuiltObjectMissionType.Capture:
            if (targetBuiltObject != null)
            {
              if (targetBuiltObject.HasBeenDestroyed)
              {
                if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                {
                  flag2 = true;
                  empire1 = targetBuiltObject.Empire;
                  attackedTarget = (StellarObject) targetBuiltObject;
                  break;
                }
                break;
              }
              if (targetBuiltObject.Empire == this.Empire)
              {
                if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                {
                  flag2 = true;
                  attackedTarget = (StellarObject) targetBuiltObject;
                  break;
                }
                break;
              }
            }
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null && ship.IsFunctional && ship.TopSpeed > (short) 0 && ship.DockedAt == null && ship.BuiltAt == null)
              {
                BuiltObjectMission mission15 = ship.Mission;
                if (mission15 != null && mission15.Type == BuiltObjectMissionType.Capture && mission1.TargetBuiltObject != null && mission1.TargetBuiltObject == mission15.TargetBuiltObject)
                  ++num1;
              }
            }
            if (num1 == 0)
            {
              flag2 = true;
              if (targetBuiltObject != null)
              {
                attackedTarget = (StellarObject) targetBuiltObject;
                break;
              }
              break;
            }
            break;
          case BuiltObjectMissionType.Raid:
            if (targetBuiltObject != null)
            {
              if (targetBuiltObject.HasBeenDestroyed)
              {
                if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                {
                  flag2 = true;
                  empire1 = targetBuiltObject.Empire;
                  attackedTarget = (StellarObject) targetBuiltObject;
                  break;
                }
                break;
              }
              if (targetBuiltObject.Empire == this.Empire)
              {
                if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                {
                  flag2 = true;
                  attackedTarget = (StellarObject) targetBuiltObject;
                  break;
                }
                break;
              }
            }
            else if (targetHabitat != null)
            {
              if (targetHabitat.HasBeenDestroyed)
              {
                if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                {
                  flag2 = true;
                  empire1 = targetHabitat.Empire;
                  attackedTarget = (StellarObject) targetHabitat;
                  break;
                }
                break;
              }
              if (targetHabitat.Empire == this.Empire)
              {
                if (this.CheckCloseToTargetAndStillTravellingToTarget(mission1))
                {
                  flag2 = true;
                  attackedTarget = (StellarObject) targetHabitat;
                  break;
                }
                break;
              }
            }
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null && ship.IsFunctional && ship.TopSpeed > (short) 0 && ship.DockedAt == null && ship.BuiltAt == null)
              {
                BuiltObjectMission mission16 = ship.Mission;
                if (mission16 != null && mission16.Type == BuiltObjectMissionType.Raid)
                {
                  if (targetBuiltObject != null)
                  {
                    if (targetBuiltObject == mission16.TargetBuiltObject)
                      ++num1;
                  }
                  else if (targetHabitat != null && targetHabitat == mission16.TargetHabitat)
                    ++num1;
                }
              }
            }
            if (num1 == 0)
            {
              flag2 = true;
              if (targetBuiltObject != null)
                attackedTarget = (StellarObject) targetBuiltObject;
              if (targetHabitat != null)
              {
                attackedTarget = (StellarObject) targetHabitat;
                break;
              }
              break;
            }
            break;
        }
        bool anotherMissionAssigned = false;
        if (flag2)
          this.ForceCompleteMission(out anotherMissionAssigned);
        if (flag2)
        {
          SpaceBattleStats battleStats = this.BattleStats;
          if (battleStats != null)
          {
            bool nearby = true;
            battleStats.Location = this._Galaxy.ResolveNearestLocation(attackedTarget, this.LeadShip, out nearby);
            battleStats.NearLocation = nearby;
            this._Galaxy.DoCharacterEvent(CharacterEventType.SpaceBattle, (object) battleStats, this.ObtainCharacters());
            this.BattleStats = (SpaceBattleStats) null;
          }
        }
        if (anotherMissionAssigned)
          return;
        ResourceList requiredFuel = new ResourceList();
        BuiltObject leadShip = this.LeadShip;
        double refuellingPortion = this.CalculateRefuellingPortion(false);
        if ((mission1 == null || mission1.Type == BuiltObjectMissionType.Undefined) && leadShip != null && leadShip.IsAutoControlled && this.CheckShipsRequiringRefuelling(refuellingPortion, out requiredFuel) > (int) ((double) this.Ships.Count * 0.0))
        {
          requiredFuel = this.CalculateRequiredFuel();
          this._Empire.AssignFleetRefuelling(this, requiredFuel);
        }
        else
        {
          if (flag2 && (this.Posture == FleetPosture.Attack || leadShip != null && leadShip.IsAutoControlled))
          {
            if (this.Empire != null && this.Empire.PirateEmpireBaseHabitat != null)
            {
              if (this.Empire.PirateEmpireSuperPirates)
              {
                BuiltObject builtObject = this._Galaxy.IdentifyPirateBase(this.Empire);
                if (builtObject != null)
                {
                  BuiltObject baseForPirateAttack = this._Galaxy.FindNearestBaseForPirateAttack(builtObject.Xpos, builtObject.Ypos, this.Empire);
                  if (baseForPirateAttack != null)
                  {
                    this.AssignMission(BuiltObjectMissionType.Attack, (object) baseForPirateAttack, (object) null, BuiltObjectMissionPriority.High, false);
                    return;
                  }
                }
              }
              else if (empire1 != null && empire1 != this.Empire && leadShip != null)
              {
                BuiltObject empireForPirateAttack = this._Galaxy.FindNearestKnownBaseOfEmpireForPirateAttack(this.Empire, leadShip.Xpos, leadShip.Ypos, empire1, this.TotalOverallStrengthFactor);
                if (empireForPirateAttack != null && !empireForPirateAttack.HasBeenDestroyed && this._Galaxy.CalculateDistance(empireForPirateAttack.Xpos, empireForPirateAttack.Ypos, leadShip.Xpos, leadShip.Ypos) < (double) Galaxy.MaxSolarSystemSize * 2.1)
                {
                  this.AssignMission(this.Empire.DetermineDestroyOrCaptureTarget(this, empireForPirateAttack), (object) empireForPirateAttack, (object) null, BuiltObjectMissionPriority.High, false);
                  return;
                }
              }
            }
            else
            {
              if (empire1 == null && leadShip != null)
              {
                int militaryStrength = 0;
                EmpireList empiresAtWarWith = this._Empire.DetermineEmpiresAtWarWith(out militaryStrength);
                if (empiresAtWarWith != null && empiresAtWarWith.Count > 0)
                {
                  Empire empire2 = (Empire) null;
                  double num16 = double.MaxValue;
                  for (int index = 0; index < empiresAtWarWith.Count; ++index)
                  {
                    Empire empire3 = empiresAtWarWith[index];
                    if (empire3 != null && empire3.Capital != null)
                    {
                      double distance = this._Galaxy.CalculateDistance(empire3.Capital.Xpos, empire3.Capital.Ypos, leadShip.Xpos, leadShip.Ypos);
                      if (distance < num16)
                      {
                        num16 = distance;
                        empire2 = empire3;
                      }
                    }
                  }
                  empire1 = empire2;
                }
              }
              if (empire1 != null && empire1 != this.Empire)
              {
                bool waypointing = false;
                StellarObject stellarObject = this._Empire.SelectFleetWarAttackTarget(this, empire1, out waypointing);
                if (stellarObject != null && !waypointing)
                {
                  BuiltObjectMissionType missionType = BuiltObjectMissionType.Attack;
                  if (stellarObject is Habitat && this._Empire.CheckBombardEnemyColony((Habitat) stellarObject, this))
                    missionType = BuiltObjectMissionType.Bombard;
                  this.AssignMission(missionType, (object) stellarObject, (object) null, BuiltObjectMissionPriority.High, false);
                  return;
                }
                if (!waypointing)
                {
                  PrioritizedTargetList targets = this._Empire.IdentifyEmpireStrikePoints(empire1);
                  int refusalCount = 0;
                  this._Empire.AssignFleetAttackMission(this, ref targets, ref refusalCount);
                  return;
                }
              }
            }
          }
          if (leadShip == null || !leadShip.IsAutoControlled || mission1 != null && mission1.Type != BuiltObjectMissionType.Undefined)
            return;
          StellarObject gatherPoint = this.GatherPoint;
          if (gatherPoint == null || this._Galaxy.CalculateDistanceSquared(leadShip.Xpos, leadShip.Ypos, gatherPoint.Xpos, gatherPoint.Ypos) <= 9000000.0 && this.CheckAllShipsHaveParent(gatherPoint))
            return;
          int num17 = 0;
          int num18 = 0;
          for (int index = 0; index < this.Ships.Count; ++index)
          {
            BuiltObject ship = this.Ships[index];
            if (ship != null)
            {
              BuiltObjectMission mission17 = ship.Mission;
              if (mission17 != null)
              {
                if (mission17.Type == BuiltObjectMissionType.Attack || mission17.Type == BuiltObjectMissionType.Bombard)
                  ++num17;
                else if (mission17.Type == BuiltObjectMissionType.Move && mission17.TargetHabitat == gatherPoint)
                  ++num18;
                else if (mission17.Type == BuiltObjectMissionType.Move && mission17.TargetBuiltObject == gatherPoint)
                  ++num18;
              }
            }
          }
          if (num17 > 0 || num18 > 0 || gatherPoint == null)
            return;
          this.AssignMission(BuiltObjectMissionType.Move, (object) gatherPoint, (object) null, BuiltObjectMissionPriority.Normal, false);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public bool CheckAllShipsHaveParent(StellarObject parent)
    {
      if (this.Ships != null)
      {
        switch (parent)
        {
          case Habitat _:
            Habitat habitat = (Habitat) parent;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null && !ship.HasBeenDestroyed && ship.BuiltAt == null && ship.ParentHabitat != habitat)
                return false;
            }
            break;
          case BuiltObject _:
            BuiltObject builtObject = (BuiltObject) parent;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null && !ship.HasBeenDestroyed && ship.BuiltAt == null && ship.ParentBuiltObject != builtObject)
                return false;
            }
            break;
        }
      }
      return true;
    }

    public CharacterList ObtainCharacters()
    {
      CharacterList characters = new CharacterList();
      if (this.Ships != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship != null && ship.Characters != null && ship.Characters.Count > 0)
            characters.AddRange((IEnumerable<Character>) ListHelper.ToArrayThreadSafe(ship.Characters));
        }
      }
      return characters;
    }

    private bool CheckShipMissionContainsHyperAndOtherAction(
      BuiltObject ship,
      CommandAction otherAction,
      StellarObject actionTarget)
    {
      if (ship != null && ship.Mission != null)
      {
        Command[] commandArray = ship.Mission.ShowAllCommands();
        bool flag1 = false;
        bool flag2 = false;
        for (int index = 0; index < commandArray.Length; ++index)
        {
          if (commandArray[index].Action == CommandAction.HyperTo || commandArray[index].Action == CommandAction.ConditionalHyperTo)
            flag1 = true;
          else if (commandArray[index].Action == otherAction)
          {
            if (actionTarget != null && actionTarget is Habitat)
            {
              if (commandArray[index].TargetHabitat != null && commandArray[index].TargetHabitat == (Habitat) actionTarget)
                flag2 = true;
            }
            else if (actionTarget != null && actionTarget is BuiltObject)
            {
              if (commandArray[index].TargetBuiltObject != null && commandArray[index].TargetBuiltObject == (BuiltObject) actionTarget)
                flag2 = true;
            }
            else if (actionTarget != null && actionTarget is Creature && commandArray[index].TargetCreature != null && commandArray[index].TargetCreature == (Creature) actionTarget)
              flag2 = true;
          }
          if (flag1 && flag2)
            return true;
        }
      }
      return false;
    }

    public int CheckShipsRequiringRefuelling(
      double requiredFuelLevel,
      out ResourceList requiredFuel)
    {
      return this.CheckShipsRequiringRefuelling(requiredFuelLevel, out requiredFuel, false);
    }

    public int CheckShipsRequiringRefuelling(
      double requiredFuelLevel,
      out ResourceList requiredFuel,
      bool includeShipsAlreadyRefuelling)
    {
      int num1 = 0;
      requiredFuel = new ResourceList();
      for (int index1 = 0; index1 < this.Ships.Count; ++index1)
      {
        BuiltObject ship = this.Ships[index1];
        if ((ship.Mission == null || ship.Mission.Type != BuiltObjectMissionType.Refuel && ship.Mission.Type != BuiltObjectMissionType.Repair || includeShipsAlreadyRefuelling) && ship.BuiltAt == null)
        {
          int num2 = ship.FuelCapacity - (int) ship.CurrentFuel;
          int index2 = -1;
          if (ship.FuelType != null)
            index2 = requiredFuel.IndexOf(ship.FuelType.ResourceID);
          if (index2 >= 0)
            requiredFuel[index2].SortTag += (double) num2;
          else if (ship.FuelType != null)
            requiredFuel.Add(new Resource(ship.FuelType.ResourceID)
            {
              SortTag = (double) num2
            });
          if (ship.CurrentFuel < (double) ship.FuelCapacity * requiredFuelLevel)
            ++num1;
        }
      }
      return num1;
    }

    public ResourceList CalculateRequiredFuel()
    {
      ResourceList requiredFuel = new ResourceList();
      for (int index1 = 0; index1 < this.Ships.Count; ++index1)
      {
        BuiltObject ship = this.Ships[index1];
        if (ship != null && ship.BuiltAt == null)
        {
          int num = ship.FuelCapacity - (int) ship.CurrentFuel;
          int index2 = -1;
          if (ship.FuelType != null)
            index2 = requiredFuel.IndexOf(ship.FuelType.ResourceID);
          if (index2 >= 0)
            requiredFuel[index2].SortTag += (double) num;
          else if (ship.FuelType != null)
            requiredFuel.Add(new Resource(ship.FuelType.ResourceID)
            {
              SortTag = (double) num
            });
        }
      }
      return requiredFuel;
    }

    public void ForceCompleteMission() => this.ForceCompleteMission(out bool _);

    public void ForceCompleteMission(out bool anotherMissionAssigned)
    {
      anotherMissionAssigned = false;
      if (this.Mission != null)
      {
        int type = (int) this.Mission.Type;
      }
      if (this.Mission != null)
        this.Mission.Clear();
      this.ForceClearMissionFromShips();
      if (!this.AssignQueuedMission())
        return;
      anotherMissionAssigned = true;
    }

    private bool AssignQueuedMission()
    {
      if (this._SubsequentMissions == null || this._SubsequentMissions.Count <= 0)
        return false;
      BuiltObjectMission subsequentMission = this._SubsequentMissions[0];
      if (subsequentMission != null)
      {
        switch (subsequentMission.Type)
        {
          case BuiltObjectMissionType.Blockade:
            if (subsequentMission.TargetBuiltObject != null)
            {
              this.Empire.ImplementBlockade(subsequentMission.TargetBuiltObject, false, false);
              this.AssignMission(BuiltObjectMissionType.Blockade, (object) subsequentMission.TargetBuiltObject, (object) null, BuiltObjectMissionPriority.High, false);
              break;
            }
            if (subsequentMission.TargetHabitat != null)
            {
              this.Empire.ImplementBlockade(subsequentMission.TargetHabitat, false, false);
              this.AssignMission(BuiltObjectMissionType.Blockade, (object) subsequentMission.TargetHabitat, (object) null, BuiltObjectMissionPriority.High, false);
              break;
            }
            break;
          case BuiltObjectMissionType.Retrofit:
            if (this.Empire != null)
            {
              this.Empire.AssignFleetRetrofit(this, !subsequentMission.ManuallyAssigned);
              break;
            }
            break;
          default:
            this.AssignMission(subsequentMission.Type, subsequentMission.Target, subsequentMission.SecondaryTarget, subsequentMission.Cargo, subsequentMission.Design, (double) subsequentMission.X, (double) subsequentMission.Y, subsequentMission.StarDate, subsequentMission.Priority, false);
            break;
        }
        this._SubsequentMissions.Remove(subsequentMission);
      }
      return true;
    }

    public void ClearAllMissionsForTarget(ShipGroup shipGroup, Empire target) => this.ClearAllMissionsForTarget(shipGroup, target, BuiltObjectMissionType.Undefined);

    public void ClearAllMissionsForTarget(
      ShipGroup shipGroup,
      Empire target,
      BuiltObjectMissionType missionType)
    {
      if (shipGroup == null)
        return;
      BuiltObjectMission mission = shipGroup.Mission;
      if (mission != null && (missionType == BuiltObjectMissionType.Undefined || mission.Type == missionType))
      {
        if (BuiltObjectMission.ResolveMissionTargetEmpire(mission) == target)
          shipGroup.ForceCompleteMission();
        if (BuiltObjectMission.ResolveMissionSecondaryTargetEmpire(mission) == target)
          shipGroup.ForceCompleteMission();
      }
      BuiltObjectMissionList objectMissionList = new BuiltObjectMissionList();
      if (shipGroup.SubsequentMissions == null)
        return;
      for (int index = 0; index < shipGroup.SubsequentMissions.Count; ++index)
      {
        BuiltObjectMission subsequentMission = shipGroup.SubsequentMissions[index];
        if (subsequentMission != null && (missionType == BuiltObjectMissionType.Undefined || subsequentMission.Type == missionType))
        {
          if (BuiltObjectMission.ResolveMissionTargetEmpire(subsequentMission) == target)
            objectMissionList.Add(subsequentMission);
          if (BuiltObjectMission.ResolveMissionSecondaryTargetEmpire(subsequentMission) == target)
            objectMissionList.Add(subsequentMission);
        }
      }
      foreach (BuiltObjectMission builtObjectMission in (SyncList<BuiltObjectMission>) objectMissionList)
        shipGroup.SubsequentMissions.Remove(builtObjectMission);
    }

    public void ClearAllMissionsForTarget(ShipGroup shipGroup, BuiltObject target) => this.ClearAllMissionsForTarget(shipGroup, target, BuiltObjectMissionType.Undefined);

    public void ClearAllMissionsForTarget(
      ShipGroup shipGroup,
      BuiltObject target,
      BuiltObjectMissionType missionType)
    {
      if (shipGroup == null)
        return;
      BuiltObjectMission mission = shipGroup.Mission;
      if (mission != null && (missionType == BuiltObjectMissionType.Undefined || mission.Type == missionType))
      {
        if (mission.TargetBuiltObject != null && mission.TargetBuiltObject == target)
          shipGroup.ForceCompleteMission();
        if (mission.SecondaryTargetBuiltObject != null && mission.SecondaryTargetBuiltObject == target)
          shipGroup.ForceCompleteMission();
      }
      BuiltObjectMissionList objectMissionList = new BuiltObjectMissionList();
      if (shipGroup.SubsequentMissions == null)
        return;
      for (int index = 0; index < shipGroup.SubsequentMissions.Count; ++index)
      {
        BuiltObjectMission subsequentMission = shipGroup.SubsequentMissions[index];
        if (subsequentMission != null && (missionType == BuiltObjectMissionType.Undefined || subsequentMission.Type == missionType))
        {
          if (subsequentMission.TargetBuiltObject != null && subsequentMission.TargetBuiltObject == target)
            objectMissionList.Add(subsequentMission);
          if (subsequentMission.SecondaryTargetBuiltObject != null && subsequentMission.SecondaryTargetBuiltObject == target)
            objectMissionList.Add(subsequentMission);
        }
      }
      foreach (BuiltObjectMission builtObjectMission in (SyncList<BuiltObjectMission>) objectMissionList)
        shipGroup.SubsequentMissions.Remove(builtObjectMission);
    }

    public void ClearAllMissionsForTarget(ShipGroup shipGroup, Habitat target) => this.ClearAllMissionsForTarget(shipGroup, target, BuiltObjectMissionType.Undefined);

    public void ClearAllMissionsForTarget(
      ShipGroup shipGroup,
      Habitat target,
      BuiltObjectMissionType missionType)
    {
      if (shipGroup == null)
        return;
      BuiltObjectMission mission = shipGroup.Mission;
      if (mission != null && (missionType == BuiltObjectMissionType.Undefined || mission.Type == missionType))
      {
        if (mission.TargetHabitat != null && mission.TargetHabitat == target)
          shipGroup.ForceCompleteMission();
        if (mission.SecondaryTargetHabitat != null && mission.SecondaryTargetHabitat == target)
          shipGroup.ForceCompleteMission();
      }
      BuiltObjectMissionList objectMissionList = new BuiltObjectMissionList();
      if (shipGroup.SubsequentMissions == null)
        return;
      for (int index = 0; index < shipGroup.SubsequentMissions.Count; ++index)
      {
        BuiltObjectMission subsequentMission = shipGroup.SubsequentMissions[index];
        if (subsequentMission != null && (missionType == BuiltObjectMissionType.Undefined || subsequentMission.Type == missionType))
        {
          if (subsequentMission.TargetHabitat != null && subsequentMission.TargetHabitat == target)
            objectMissionList.Add(subsequentMission);
          if (subsequentMission.SecondaryTargetHabitat != null && subsequentMission.SecondaryTargetHabitat == target)
            objectMissionList.Add(subsequentMission);
        }
      }
      foreach (BuiltObjectMission builtObjectMission in (SyncList<BuiltObjectMission>) objectMissionList)
        shipGroup.SubsequentMissions.Remove(builtObjectMission);
    }

    public ResourceList DetermineFuelTypes()
    {
      ResourceList fuelTypes = new ResourceList();
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null && !ship.HasBeenDestroyed && ship.FuelType != null && !fuelTypes.Contains(ship.FuelType))
          fuelTypes.Add(new Resource(ship.FuelType.ResourceID));
      }
      return fuelTypes;
    }

    private bool CheckRefuelManual() => this.LeadShip != null && !this.LeadShip.IsAutoControlled && this.CheckRefuelManual(Math.Max(0.3, this.CalculateRefuellingPortion(false)));

    private bool CheckRefuelManual(double requiredFuelLevel)
    {
      if (this.LeadShip != null && !this.LeadShip.IsAutoControlled)
      {
        ResourceList requiredFuel = new ResourceList();
        bool flag = false;
        if (this.Mission == null)
        {
          flag = true;
        }
        else
        {
          switch (this.Mission.Type)
          {
            case BuiltObjectMissionType.Undefined:
            case BuiltObjectMissionType.Patrol:
            case BuiltObjectMissionType.Escort:
            case BuiltObjectMissionType.Blockade:
            case BuiltObjectMissionType.Hold:
              flag = true;
              break;
          }
          if (this.Mission.Priority == BuiltObjectMissionPriority.Low)
            flag = true;
        }
        if (flag && this.CheckShipsRequiringRefuelling(requiredFuelLevel, out requiredFuel) > 0)
        {
          requiredFuel = this.CalculateRequiredFuel();
          StellarObject nearestRefuellingPoint1 = this._Galaxy.FastFindNearestRefuellingPoint(this.LeadShip.Xpos, this.LeadShip.Ypos, requiredFuel, this.Empire, this.LeadShip, true, (Empire) null, this.Ships.Count);
          switch (nearestRefuellingPoint1)
          {
            case BuiltObject _:
              BuiltObject target1 = (BuiltObject) nearestRefuellingPoint1;
              if (target1.NearestSystemStar == this.LeadShip.NearestSystemStar)
              {
                this.AssignMission(BuiltObjectMissionType.Refuel, (object) target1, (object) null, BuiltObjectMissionPriority.Unavailable, false);
                return true;
              }
              break;
            case Habitat _:
              Habitat habitat1 = (Habitat) nearestRefuellingPoint1;
              if (Galaxy.DetermineHabitatSystemStar(habitat1) == this.LeadShip.NearestSystemStar)
              {
                this.AssignMission(BuiltObjectMissionType.Refuel, (object) habitat1, (object) null, BuiltObjectMissionPriority.Unavailable, false);
                return true;
              }
              break;
          }
          StellarObject nearestRefuellingPoint2 = this._Galaxy.FastFindNearestRefuellingPoint(this.LeadShip.Xpos, this.LeadShip.Ypos, requiredFuel, this.Empire, this.LeadShip, true, (Empire) null, 1);
          switch (nearestRefuellingPoint2)
          {
            case BuiltObject _:
              BuiltObject target2 = (BuiltObject) nearestRefuellingPoint2;
              if (target2.NearestSystemStar == this.LeadShip.NearestSystemStar)
              {
                this.AssignMission(BuiltObjectMissionType.Refuel, (object) target2, (object) null, BuiltObjectMissionPriority.Unavailable, false);
                return true;
              }
              break;
            case Habitat _:
              Habitat habitat2 = (Habitat) nearestRefuellingPoint2;
              if (Galaxy.DetermineHabitatSystemStar(habitat2) == this.LeadShip.NearestSystemStar)
              {
                this.AssignMission(BuiltObjectMissionType.Refuel, (object) habitat2, (object) null, BuiltObjectMissionPriority.Unavailable, false);
                return true;
              }
              break;
          }
        }
      }
      return false;
    }

    private bool CheckRefuelRepairAttack(bool completedAttackMission, Empire attackEmpire)
    {
      BuiltObject leadShip1 = this.LeadShip;
      if (leadShip1 != null && leadShip1.IsAutoControlled && this.Ships != null)
      {
        BuiltObjectList builtObjectList = new BuiltObjectList();
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship != null && ship.DamagedComponentCount > 0 && this.Empire != null && this.Empire.ControlMilitaryFleets)
          {
            if (ship.TopSpeed <= (short) 0 || ship.WarpSpeed <= 0 && ship.Design.WarpSpeed > 0)
              builtObjectList.Add(ship);
            else if ((ship.Mission == null || ship.Mission.Type != BuiltObjectMissionType.Repair) && ship.IsAutoControlled)
            {
              if (this.LeadShip == ship)
              {
                this.DetermineLeadShip(ship);
                leadShip1 = this.LeadShip;
              }
              builtObjectList.Add(ship);
              this.Empire.AssignRepairMission(ship);
            }
          }
        }
        for (int index = 0; index < builtObjectList.Count; ++index)
          builtObjectList[index].LeaveShipGroup();
        ResourceList requiredFuel = new ResourceList();
        double refuellingPortion = this.CalculateRefuellingPortion();
        bool flag = false;
        BuiltObjectMission mission1 = this.Mission;
        if (mission1 == null)
          flag = true;
        else if (mission1.Priority == BuiltObjectMissionPriority.Unavailable)
        {
          flag = false;
        }
        else
        {
          switch (mission1.Type)
          {
            case BuiltObjectMissionType.Undefined:
            case BuiltObjectMissionType.Patrol:
            case BuiltObjectMissionType.Escort:
            case BuiltObjectMissionType.Blockade:
            case BuiltObjectMissionType.Hold:
              flag = true;
              break;
          }
          if (mission1.Priority == BuiltObjectMissionPriority.Low)
            flag = true;
        }
        if (flag && leadShip1 != null && leadShip1.IsAutoControlled && this.CheckShipsRequiringRefuelling(refuellingPortion, out requiredFuel) > (int) ((double) this.Ships.Count * 0.0))
        {
          requiredFuel = this.CalculateRequiredFuel();
          this._Empire.AssignFleetRefuelling(this, requiredFuel);
          return true;
        }
        if (completedAttackMission && (this.Posture == FleetPosture.Attack || leadShip1 != null && leadShip1.IsAutoControlled))
        {
          if (leadShip1 != null && attackEmpire == null)
          {
            int militaryStrength = 0;
            EmpireList empiresAtWarWith = this._Empire.DetermineEmpiresAtWarWith(out militaryStrength);
            if (empiresAtWarWith != null && empiresAtWarWith.Count > 0)
            {
              Empire empire1 = (Empire) null;
              double num = double.MaxValue;
              for (int index = 0; index < empiresAtWarWith.Count; ++index)
              {
                Empire empire2 = empiresAtWarWith[index];
                if (empire2 != null)
                {
                  double distance = this._Galaxy.CalculateDistance(empire2.Capital.Xpos, empire2.Capital.Ypos, leadShip1.Xpos, leadShip1.Ypos);
                  if (distance < num)
                  {
                    num = distance;
                    empire1 = empire2;
                  }
                }
              }
              attackEmpire = empire1;
            }
          }
          if (attackEmpire != null && this.Empire != null && this.Empire.CheckAtWarWithEmpire(attackEmpire))
          {
            bool waypointing = false;
            StellarObject stellarObject = this._Empire.SelectFleetWarAttackTarget(this, attackEmpire, out waypointing);
            if (stellarObject != null && !waypointing)
            {
              BuiltObjectMissionType missionType = BuiltObjectMissionType.Attack;
              if (stellarObject is Habitat && this._Empire.CheckBombardEnemyColony((Habitat) stellarObject, this))
                missionType = BuiltObjectMissionType.Bombard;
              if (this.CheckFleetTargetWithinFuelRangeAndRefuel(stellarObject.Xpos, stellarObject.Ypos, 0.1))
                this.AssignMission(missionType, (object) stellarObject, (object) null, BuiltObjectMissionPriority.High, false);
            }
            else if (!waypointing)
            {
              PrioritizedTargetList targets = this._Empire.IdentifyEmpireStrikePoints(attackEmpire);
              int refusalCount = 0;
              if (this._Empire.AssignFleetAttackMission(this, ref targets, ref refusalCount))
                return true;
            }
          }
        }
        if (this.Empire != null && (this.Ships != null && this.Posture == FleetPosture.Attack && this.LeadShip != null && this.LeadShip.IsAutoControlled && this.Empire.CoordinateFleetAttacksWithAllies(this, this.Empire, (EmpireList) null) || this.Empire.CoordinateFleetAttacksWithAllies(this)))
          return true;
        BuiltObjectMission mission2 = this.Mission;
        if (mission2 == null || mission2.Type == BuiltObjectMissionType.Undefined)
        {
          StellarObject gatherPoint = this.GatherPoint;
          BuiltObject leadShip2 = this.LeadShip;
          if (gatherPoint != null && leadShip2 != null && (this._Galaxy.CalculateDistanceSquared(leadShip2.Xpos, leadShip2.Ypos, gatherPoint.Xpos, gatherPoint.Ypos) > 9000000.0 || !this.CheckAllShipsHaveParent(gatherPoint)))
          {
            int num1 = 0;
            int num2 = 0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship != null)
              {
                BuiltObjectMission mission3 = ship.Mission;
                if (mission3 != null)
                {
                  if (mission3.Type == BuiltObjectMissionType.Attack || mission3.Type == BuiltObjectMissionType.Bombard || mission3.Type == BuiltObjectMissionType.Capture || mission3.Type == BuiltObjectMissionType.Raid)
                    ++num1;
                  else if (mission3.Type == BuiltObjectMissionType.Move && mission3.TargetHabitat == gatherPoint)
                    ++num2;
                  else if (mission3.Type == BuiltObjectMissionType.Move && mission3.TargetBuiltObject == gatherPoint)
                    ++num2;
                }
              }
            }
            if (num1 <= 0 && num2 <= 0 && gatherPoint != null && this.CheckFleetTargetWithinFuelRange(gatherPoint.Xpos, gatherPoint.Ypos, 0.0))
            {
              this.AssignMission(BuiltObjectMissionType.Move, (object) gatherPoint, (object) null, BuiltObjectMissionPriority.Low, false);
              return true;
            }
          }
        }
      }
      return false;
    }

    public bool CheckRefuelLocationRangeAcceptable(StellarObject refuellingLocation)
    {
      if (refuellingLocation != null)
      {
        if (this.WarpSpeed <= 0)
        {
          if (!this.CheckFleetTargetWithinFuelRange(refuellingLocation.Xpos, refuellingLocation.Ypos, 0.0))
          {
            BuiltObject leadShip = this.LeadShip;
            if (leadShip != null && this._Galaxy.CalculateDistanceSquared(leadShip.Xpos, leadShip.Ypos, refuellingLocation.Xpos, refuellingLocation.Ypos) > 2304000000.0)
              return false;
          }
        }
        else if (this.WarpSpeed < 2500 && !this.CheckFleetTargetWithinFuelRange(refuellingLocation.Xpos, refuellingLocation.Ypos, 0.0))
        {
          BuiltObject leadShip = this.LeadShip;
          if (leadShip != null && this._Galaxy.CalculateDistanceSquared(leadShip.Xpos, leadShip.Ypos, refuellingLocation.Xpos, refuellingLocation.Ypos) > 2304000000.0)
            return false;
        }
      }
      return true;
    }

    public bool CompleteMission() => this.CompleteMission(true);

    public bool CompleteMission(bool clearShipMissions)
    {
      BuiltObjectMissionType objectMissionType = BuiltObjectMissionType.Undefined;
      if (this.Mission != null)
      {
        objectMissionType = this.Mission.Type;
        this.Mission.ResolveMissionTargetHabitatIfPossible();
        this.Mission.Clear();
      }
      if (clearShipMissions)
        this.ClearMissionFromShips();
      if (this.AssignQueuedMission())
        return true;
      if (objectMissionType == BuiltObjectMissionType.Attack)
      {
        if (this.CheckRefuelRepairAttack(true, (Empire) null))
          return true;
      }
      else if (this.CheckRefuelRepairAttack(false, (Empire) null))
        return true;
      return false;
    }

    private void CompleteCommand()
    {
      this.Mission.CompleteCommand(true);
      if (this.Mission.Type != BuiltObjectMissionType.Undefined)
        return;
      this.ClearMissionFromShips();
    }

    private void ForceClearMissionFromShips()
    {
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null && ship.BuiltAt == null)
        {
          ship.ClearPreviousMissionRequirements();
          ship.RevertMission = (BuiltObjectMission) null;
        }
      }
    }

    private void ClearMissionFromShips()
    {
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (this.IsShipAvailable(ship))
          ship.ClearPreviousMissionRequirements();
      }
    }

    private void CheckForCommandCompletion()
    {
      if (this.Mission == null)
        return;
      Command command = this.Mission.FastPeekCurrentCommand();
      switch (command.Action)
      {
        case CommandAction.Hold:
          long currentStarDate = this._Galaxy.CurrentStarDate;
          if (command.StarDate > currentStarDate)
            break;
          this.CompleteCommand();
          break;
        case CommandAction.ImpulseTo:
        case CommandAction.MoveTo:
        case CommandAction.SprintTo:
        case CommandAction.HyperTo:
          double x2 = -2000000001.0;
          double y2 = -2000000001.0;
          if (command.TargetBuiltObject != null && command.TargetHabitat != null && command.TargetCreature != null && command.TargetShipGroup != null)
          {
            if (command.TargetBuiltObject != null)
            {
              x2 = command.TargetBuiltObject.Xpos;
              y2 = command.TargetBuiltObject.Ypos;
            }
            else if (command.TargetHabitat != null)
            {
              x2 = command.TargetHabitat.Xpos;
              y2 = command.TargetHabitat.Ypos;
            }
            else if (command.TargetCreature != null)
            {
              x2 = command.TargetCreature.Xpos;
              y2 = command.TargetCreature.Ypos;
            }
            else if (command.TargetShipGroup != null)
            {
              ShipGroup targetShipGroup = command.TargetShipGroup;
              if (targetShipGroup.LeadShip != null)
              {
                x2 = targetShipGroup.LeadShip.Xpos;
                y2 = targetShipGroup.LeadShip.Ypos;
              }
              else
                this.CompleteCommand();
            }
          }
          if ((double) command.Xpos > -2000000000.0 && (double) command.Ypos > -2000000000.0)
          {
            x2 = (double) command.Xpos;
            y2 = (double) command.Ypos;
          }
          bool flag = true;
          double num1 = (double) Galaxy.ParentRelativeRange;
          if (command.Action == CommandAction.HyperTo)
            num1 = Galaxy.BaseHyperJumpAccuracy * 3.0;
          for (int index = 0; index < this.Ships.Count; ++index)
          {
            BuiltObject ship = this.Ships[index];
            if (this._Galaxy.CalculateDistance(ship.Xpos, ship.Ypos, x2, y2) > num1)
            {
              flag = false;
              break;
            }
            if ((double) ship.CurrentSpeed > (double) ship.CruiseSpeed)
            {
              flag = false;
              break;
            }
          }
          if (!flag)
            break;
          this.CompleteCommand();
          break;
        case CommandAction.Escort:
          if (command.TargetBuiltObject != null)
          {
            BuiltObject targetBuiltObject = command.TargetBuiltObject;
            if (targetBuiltObject.Mission != null && targetBuiltObject.Mission.Type != BuiltObjectMissionType.Undefined)
              break;
            this.CompleteCommand();
            break;
          }
          this.CompleteCommand();
          break;
        case CommandAction.Attack:
          if (command.TargetHabitat != null)
          {
            Habitat targetHabitat = command.TargetHabitat;
            if (targetHabitat.Owner == this.Empire)
            {
              this.CompleteCommand();
              break;
            }
            if (targetHabitat.InvadingTroops != null && targetHabitat.InvadingTroops.TotalAttackStrength > 0)
              break;
            double num2 = 0.0;
            for (int index = 0; index < this.Ships.Count; ++index)
            {
              BuiltObject ship = this.Ships[index];
              if (ship.Troops != null)
                num2 += (double) ship.Troops.TotalAttackStrength;
            }
            if (num2 > 0.0)
              break;
            this.CompleteCommand();
            break;
          }
          if (command.TargetShipGroup != null)
          {
            if (command.TargetShipGroup.Ships.Count != 0)
              break;
            this.CompleteCommand();
            break;
          }
          if (command.TargetBuiltObject != null)
          {
            if (!command.TargetBuiltObject.HasBeenDestroyed)
              break;
            this.CompleteCommand();
            break;
          }
          if (command.TargetCreature != null)
          {
            if (!command.TargetCreature.HasBeenDestroyed)
              break;
            this.CompleteCommand();
            break;
          }
          this.CompleteCommand();
          break;
      }
    }

    public bool AssignMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      BuiltObjectMissionPriority priority,
      bool manuallyAssigned)
    {
      return this.AssignMission(missionType, target, target2, (CargoList) null, (Design) null, -2000000001.0, -2000000001.0, priority, manuallyAssigned);
    }

    public bool AssignMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      long starDate,
      BuiltObjectMissionPriority priority,
      bool manuallyAssigned)
    {
      return this.AssignMission(missionType, target, target2, (CargoList) null, (Design) null, -2000000001.0, -2000000001.0, starDate, priority, manuallyAssigned);
    }

    public bool AssignMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      Design design,
      BuiltObjectMissionPriority priority,
      bool manuallyAssigned)
    {
      return this.AssignMission(missionType, target, target2, (CargoList) null, design, -2000000001.0, -2000000001.0, priority, manuallyAssigned);
    }

    public bool AssignMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      CargoList cargo,
      BuiltObjectMissionPriority priority,
      bool manuallyAssigned)
    {
      return this.AssignMission(missionType, target, target2, cargo, (Design) null, -2000000001.0, -2000000001.0, priority, manuallyAssigned);
    }

    public bool AssignMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      double x,
      double y,
      BuiltObjectMissionPriority priority,
      bool manuallyAssigned)
    {
      return this.AssignMission(missionType, target, target2, (CargoList) null, (Design) null, x, y, priority, manuallyAssigned);
    }

    public bool AssignMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      CargoList cargo,
      Design design,
      double x,
      double y,
      BuiltObjectMissionPriority priority,
      bool manuallyAssigned)
    {
      return this.AssignMission(missionType, target, target2, cargo, design, x, y, -1L, priority, manuallyAssigned);
    }

    public bool AssignMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      CargoList cargo,
      Design design,
      double x,
      double y,
      long starDate,
      BuiltObjectMissionPriority priority,
      bool manuallyAssigned)
    {
      this.AllowImmediateThreatEvaluation = false;
      BuiltObject builtObject = this.LeadShip;
      if (builtObject == null)
      {
        if (this.Ships.Count <= 0)
          return false;
        builtObject = this.Ships[0];
      }
      if (missionType == BuiltObjectMissionType.Move && target != null && target is Habitat)
      {
        Habitat habitat = (Habitat) target;
        if (habitat.Category == HabitatCategoryType.Star)
        {
          double xpos = habitat.Xpos;
          double ypos = habitat.Ypos;
          double num1;
          double num2;
          if (x > -2000000001.0 && y > -2000000001.0)
          {
            num1 = x;
            num2 = y;
          }
          else
          {
            double num3 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
            double num4 = habitat.Type != HabitatType.BlackHole ? (habitat.Type != HabitatType.SuperNova ? (habitat.Category != HabitatCategoryType.Star ? (double) habitat.Diameter * 0.67 + Galaxy.Rnd.NextDouble() * 150.0 : (double) habitat.Diameter * 0.67 + Galaxy.Rnd.NextDouble() * 300.0) : Galaxy.Rnd.NextDouble() * 300.0) : (double) habitat.Diameter * 0.7 + Galaxy.Rnd.NextDouble() * 500.0;
            num1 = num4 * Math.Sin(num3);
            num2 = num4 * Math.Cos(num3);
          }
          x = num1;
          y = num2;
        }
      }
      if (target == null && target2 == null && x <= -2000000001.0 && y <= -2000000001.0)
        return false;
      BuiltObjectMission mission = new BuiltObjectMission(this._Galaxy, builtObject, missionType, target, target2, cargo, (TroopList) null, (PopulationList) null, design, x, y, starDate, priority, false, false, true);
      this.Mission = mission;
      if (!this.AssignMissionToShips(mission, manuallyAssigned))
      {
        this.Mission = (BuiltObjectMission) null;
        return false;
      }
      this.SetAttackRange(missionType, manuallyAssigned);
      if (missionType == BuiltObjectMissionType.Attack || missionType == BuiltObjectMissionType.Bombard || missionType == BuiltObjectMissionType.WaitAndAttack || missionType == BuiltObjectMissionType.WaitAndBombard || missionType == BuiltObjectMissionType.Capture || missionType == BuiltObjectMissionType.Raid)
      {
        this.CheckForCompletedBattle();
        if (this.BattleStats == null)
          this.BattleStats = new SpaceBattleStats();
      }
      return true;
    }

    public bool CheckNeedGatherBeforeAttack(
      object target,
      out int targetGatherRange,
      out double targetX,
      out double targetY)
    {
      targetX = 0.0;
      targetY = 0.0;
      targetGatherRange = 0;
      int num1 = 0;
      switch (target)
      {
        case BuiltObject _:
          BuiltObject builtObject = (BuiltObject) target;
          targetX = builtObject.Xpos;
          targetY = builtObject.Ypos;
          Empire empire1 = builtObject.Empire;
          num1 = builtObject.CalculateOverallStrengthFactor();
          targetGatherRange = builtObject.Role == BuiltObjectRole.Base || builtObject.TopSpeed <= (short) 0 ? builtObject.MaximumWeaponsRange + 100 : 5000;
          break;
        case Habitat _:
          Habitat habitat = (Habitat) target;
          targetX = habitat.Xpos;
          targetY = habitat.Ypos;
          Empire empire2 = habitat.Empire;
          if (habitat.BasesAtHabitat != null && habitat.BasesAtHabitat.Count > 0)
          {
            for (int index = 0; index < habitat.BasesAtHabitat.Count; ++index)
              num1 += habitat.BasesAtHabitat[index].CalculateOverallStrengthFactor();
          }
          targetGatherRange = 600;
          break;
        case Creature _:
          Creature creature = (Creature) target;
          targetX = creature.Xpos;
          targetY = creature.Ypos;
          num1 += creature.AttackStrength * 5;
          targetGatherRange = 1000;
          break;
        case ShipGroup _:
          ShipGroup shipGroup = (ShipGroup) target;
          if (shipGroup.LeadShip != null)
          {
            targetX = shipGroup.LeadShip.Xpos;
            targetY = shipGroup.LeadShip.Ypos;
            Empire empire3 = shipGroup.Empire;
            num1 += shipGroup.TotalOverallStrengthFactor;
          }
          targetGatherRange = 1000;
          break;
      }
      int num2 = 0;
      int num3 = 0;
      if (this.LeadShip != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (this.IsShipAvailable(ship))
          {
            if (this._Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, this.LeadShip.Xpos, this.LeadShip.Ypos) > 2304000000.0)
              ++num2;
            else
              num3 += ship.CalculateOverallStrengthFactor();
          }
        }
        if ((double) num2 / (double) this.Ships.Count > (double) this.Empire.FleetAttackGatherPortion && num3 < num1)
          return true;
      }
      return false;
    }

    public void IdentifyFleetGatherLocation(
      double targetX,
      double targetY,
      int targetGatherRange,
      out double gatherX,
      out double gatherY)
    {
      gatherX = this.LeadShip.Xpos;
      gatherY = this.LeadShip.Ypos;
      if (this._Galaxy.CalculateDistance(targetX, targetY, gatherX, gatherY) >= (double) targetGatherRange)
        return;
      if (this.LeadShip.NearestSystemStar != null)
      {
        SystemInfo system = this._Galaxy.Systems[this.LeadShip.NearestSystemStar.SystemIndex];
        if (system == null || system.Habitats == null || system.Habitats.Count <= 0)
          return;
        Habitat habitat = system.Habitats[system.Habitats.Count - 1];
        if (habitat == null)
          return;
        double x = 0.0;
        double y = 0.0;
        this._Galaxy.SelectRelativeParkingPoint(800.0, out x, out y);
        gatherX = habitat.Xpos + x;
        gatherY = habitat.Ypos + y;
      }
      else
      {
        double x = 0.0;
        double y = 0.0;
        this._Galaxy.SelectRelativeParkingPoint(30000.0, out x, out y);
        gatherX = targetX + x;
        gatherY = targetY + y;
      }
    }

    public bool CheckNeedRefuelBeforeAttack(object target)
    {
      double x = 0.0;
      double y = 0.0;
      int num = 0;
      switch (target)
      {
        case BuiltObject _:
          BuiltObject builtObject = (BuiltObject) target;
          x = builtObject.Xpos;
          y = builtObject.Ypos;
          Empire empire1 = builtObject.Empire;
          num = builtObject.CalculateOverallStrengthFactor();
          if (builtObject.Role == BuiltObjectRole.Base || builtObject.TopSpeed <= (short) 0)
          {
            int maximumWeaponsRange = builtObject.MaximumWeaponsRange;
            break;
          }
          break;
        case Habitat _:
          Habitat habitat = (Habitat) target;
          x = habitat.Xpos;
          y = habitat.Ypos;
          Empire empire2 = habitat.Empire;
          if (habitat.BasesAtHabitat != null && habitat.BasesAtHabitat.Count > 0)
          {
            for (int index = 0; index < habitat.BasesAtHabitat.Count; ++index)
              num += habitat.BasesAtHabitat[index].CalculateOverallStrengthFactor();
            break;
          }
          break;
        case Creature _:
          Creature creature = (Creature) target;
          x = creature.Xpos;
          y = creature.Ypos;
          num += creature.AttackStrength * 5;
          break;
        case ShipGroup _:
          ShipGroup shipGroup = (ShipGroup) target;
          if (shipGroup.LeadShip != null)
          {
            x = shipGroup.LeadShip.Xpos;
            y = shipGroup.LeadShip.Ypos;
            Empire empire3 = shipGroup.Empire;
            num += shipGroup.TotalOverallStrengthFactor;
            break;
          }
          break;
      }
      int firepowerWithinRange = 0;
      return (double) (this.Ships.Count - this.CountShipsWithinFuelRangeAndRefuel(x, y, 0.05, out firepowerWithinRange)) / (double) this.Ships.Count > (double) this.Empire.FleetAttackRefuelPortion && firepowerWithinRange < num || !this.CheckFleetTargetWithinFuelRange(x, y, 0.0);
    }

    private void SetAttackRange(BuiltObjectMissionType missionType, bool manuallyAssigned)
    {
      if (this.Empire == null)
        return;
      int num1 = this.Empire.AttackRangePatrol;
      int num2 = this.Empire.AttackRangeEscort;
      int num3 = this.Empire.AttackRangeAttack;
      int num4 = this.Empire.AttackRangeOther;
      BuiltObject leadShip = this.LeadShip;
      if (leadShip != null && !leadShip.IsAutoControlled || manuallyAssigned)
      {
        num1 = this.Empire.AttackRangePatrolManual;
        num2 = this.Empire.AttackRangeEscortManual;
        num3 = this.Empire.AttackRangeAttackManual;
        num4 = this.Empire.AttackRangeOtherManual;
      }
      if (missionType == BuiltObjectMissionType.Patrol && num1 >= 0)
      {
        this.AttackRangeSquared = (float) num1 * (float) num1;
        this.SetShipAttackRanges(this.AttackRangeSquared);
      }
      else if (missionType == BuiltObjectMissionType.Escort && num2 >= 0)
      {
        this.AttackRangeSquared = (float) num2 * (float) num2;
        this.SetShipAttackRanges(this.AttackRangeSquared);
      }
      else if ((missionType == BuiltObjectMissionType.Attack || missionType == BuiltObjectMissionType.Bombard || missionType == BuiltObjectMissionType.WaitAndAttack || missionType == BuiltObjectMissionType.WaitAndBombard || missionType == BuiltObjectMissionType.Capture || missionType == BuiltObjectMissionType.Raid) && num3 >= 0)
      {
        this.AttackRangeSquared = (float) num3 * (float) num3;
        this.SetShipAttackRanges(this.AttackRangeSquared);
      }
      else if (num4 >= 0)
      {
        this.AttackRangeSquared = (float) num4 * (float) num4;
        this.SetShipAttackRanges(this.AttackRangeSquared);
      }
      else
      {
        if ((double) this.AttackRangeSquared >= 0.0 || leadShip == null || !leadShip.IsAutoControlled)
          return;
        this.AttackRangeSquared = 2.304E+09f;
        this.SetShipAttackRanges(this.AttackRangeSquared);
      }
    }

    private void SetShipAttackRanges(float attackRangeSquared)
    {
      for (int index = 0; index < this.Ships.Count; ++index)
        this.Ships[index].AttackRangeSquared = attackRangeSquared;
    }

    public BuiltObject FindNearestAvailableShipToTarget(double x, double y)
    {
      double num = double.MaxValue;
      BuiltObject availableShipToTarget = (BuiltObject) null;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (this.IsShipAvailable(ship))
        {
          double distanceSquared = this._Galaxy.CalculateDistanceSquared(x, y, ship.Xpos, ship.Ypos);
          if (distanceSquared < num)
          {
            num = distanceSquared;
            availableShipToTarget = ship;
          }
        }
      }
      return availableShipToTarget;
    }

    public bool QueueMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      BuiltObjectMissionPriority priority)
    {
      return this.QueueMission(missionType, target, target2, (CargoList) null, (Design) null, -2000000001.0, -2000000001.0, priority);
    }

    public bool QueueMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      Design design,
      BuiltObjectMissionPriority priority)
    {
      return this.QueueMission(missionType, target, target2, (CargoList) null, design, -2000000001.0, -2000000001.0, priority);
    }

    public bool QueueMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      double x,
      double y,
      BuiltObjectMissionPriority priority)
    {
      return this.QueueMission(missionType, target, target2, (CargoList) null, (Design) null, x, y, priority);
    }

    public bool QueueMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      CargoList cargo,
      Design design,
      double x,
      double y,
      BuiltObjectMissionPriority priority)
    {
      return this.QueueMission(missionType, target, target2, cargo, design, x, y, -1L, priority);
    }

    public bool QueueMission(
      BuiltObjectMissionType missionType,
      object target,
      object target2,
      CargoList cargo,
      Design design,
      double x,
      double y,
      long starDate,
      BuiltObjectMissionPriority priority)
    {
      BuiltObject builtObject = this.LeadShip;
      if (builtObject == null)
      {
        if (this.Ships.Count <= 0)
          return false;
        builtObject = this.Ships[0];
      }
      this._SubsequentMissions.Add(new BuiltObjectMission(this._Galaxy, builtObject, missionType, target, target2, cargo, (TroopList) null, (PopulationList) null, design, x, y, starDate, priority, false, false, true));
      return true;
    }

    public BuiltObjectMission Mission
    {
      get => this._Mission;
      set => this._Mission = value;
    }

    public StellarObject AttackPoint
    {
      get => this._AttackPoint;
      set => this._AttackPoint = value;
    }

    public BuiltObjectList Ships
    {
      get => this._Ships;
      set => this._Ships = value;
    }

    public BuiltObject LeadShip
    {
      get => this._LeadShip;
      set => this._LeadShip = value;
    }

    public StellarObject GatherPoint
    {
      get => this._GatherPoint;
      set => this._GatherPoint = value;
    }

    public Habitat IdentifyFleetSystem()
    {
      StellarObject stellarObject = this.IdentifyFleetLocation();
      switch (stellarObject)
      {
        case Habitat _:
          return Galaxy.DetermineHabitatSystemStar((Habitat) stellarObject);
        case BuiltObject _:
          return ((BuiltObject) stellarObject).NearestSystemStar;
        case Creature _:
          return ((Creature) stellarObject).NearestSystemStar;
        default:
          return (Habitat) null;
      }
    }

    public StellarObject IdentifyFleetLocation()
    {
      if (this.Mission != null && (this.Mission.Type == BuiltObjectMissionType.Attack || this.Mission.Type == BuiltObjectMissionType.Blockade || this.Mission.Type == BuiltObjectMissionType.Bombard || this.Mission.Type == BuiltObjectMissionType.Hold || this.Mission.Type == BuiltObjectMissionType.Move || this.Mission.Type == BuiltObjectMissionType.MoveAndWait || this.Mission.Type == BuiltObjectMissionType.Patrol || this.Mission.Type == BuiltObjectMissionType.Refuel || this.Mission.Type == BuiltObjectMissionType.Repair || this.Mission.Type == BuiltObjectMissionType.Retrofit || this.Mission.Type == BuiltObjectMissionType.Capture || this.Mission.Type == BuiltObjectMissionType.Raid || this.Mission.Type == BuiltObjectMissionType.WaitAndAttack || this.Mission.Type == BuiltObjectMissionType.WaitAndBombard))
      {
        if (this.Mission.TargetBuiltObject != null)
        {
          if (this.Mission.TargetBuiltObject.ParentHabitat != null)
            return (StellarObject) this.Mission.TargetBuiltObject.ParentHabitat;
          return this.Mission.TargetBuiltObject.NearestSystemStar != null ? (StellarObject) this.Mission.TargetBuiltObject.NearestSystemStar : (StellarObject) this.Mission.TargetBuiltObject;
        }
        if (this.Mission.TargetCreature != null)
        {
          if (this.Mission.TargetCreature.ParentHabitat != null)
            return (StellarObject) this.Mission.TargetCreature.ParentHabitat;
          return this.Mission.TargetCreature.NearestSystemStar != null ? (StellarObject) this.Mission.TargetCreature.NearestSystemStar : (StellarObject) this.Mission.TargetCreature;
        }
        if (this.Mission.TargetShipGroup != null)
        {
          if (this.Mission.TargetShipGroup.LeadShip != null)
          {
            if (this.Mission.TargetShipGroup.LeadShip.ParentHabitat != null)
              return (StellarObject) this.Mission.TargetShipGroup.LeadShip.ParentHabitat;
            return this.Mission.TargetShipGroup.LeadShip.NearestSystemStar != null ? (StellarObject) this.Mission.TargetShipGroup.LeadShip.NearestSystemStar : (StellarObject) this.Mission.TargetShipGroup.LeadShip;
          }
        }
        else if (this.Mission.TargetHabitat != null)
          return (StellarObject) this.Mission.TargetHabitat;
      }
      if (this.LeadShip == null)
        return (StellarObject) null;
      if (this.LeadShip.Mission != null && (this.LeadShip.Mission.Type == BuiltObjectMissionType.Attack || this.LeadShip.Mission.Type == BuiltObjectMissionType.Blockade || this.LeadShip.Mission.Type == BuiltObjectMissionType.Bombard || this.LeadShip.Mission.Type == BuiltObjectMissionType.Hold || this.LeadShip.Mission.Type == BuiltObjectMissionType.Move || this.LeadShip.Mission.Type == BuiltObjectMissionType.MoveAndWait || this.LeadShip.Mission.Type == BuiltObjectMissionType.Patrol || this.LeadShip.Mission.Type == BuiltObjectMissionType.Refuel || this.LeadShip.Mission.Type == BuiltObjectMissionType.Repair || this.LeadShip.Mission.Type == BuiltObjectMissionType.Retrofit || this.LeadShip.Mission.Type == BuiltObjectMissionType.Capture || this.LeadShip.Mission.Type == BuiltObjectMissionType.Raid || this.LeadShip.Mission.Type == BuiltObjectMissionType.WaitAndAttack || this.LeadShip.Mission.Type == BuiltObjectMissionType.WaitAndBombard))
      {
        if (this.LeadShip.Mission.TargetBuiltObject != null)
        {
          if (this.LeadShip.Mission.TargetBuiltObject.ParentHabitat != null)
            return (StellarObject) this.LeadShip.Mission.TargetBuiltObject.ParentHabitat;
          return this.LeadShip.Mission.TargetBuiltObject.NearestSystemStar != null ? (StellarObject) this.LeadShip.Mission.TargetBuiltObject.NearestSystemStar : (StellarObject) this.LeadShip.Mission.TargetBuiltObject;
        }
        if (this.LeadShip.Mission.TargetCreature != null)
        {
          if (this.LeadShip.Mission.TargetCreature.ParentHabitat != null)
            return (StellarObject) this.LeadShip.Mission.TargetCreature.ParentHabitat;
          return this.LeadShip.Mission.TargetCreature.NearestSystemStar != null ? (StellarObject) this.LeadShip.Mission.TargetCreature.NearestSystemStar : (StellarObject) this.LeadShip.Mission.TargetCreature;
        }
        if (this.LeadShip.Mission.TargetShipGroup != null)
        {
          if (this.LeadShip.Mission.TargetShipGroup.LeadShip != null)
          {
            if (this.LeadShip.Mission.TargetShipGroup.LeadShip.ParentHabitat != null)
              return (StellarObject) this.LeadShip.Mission.TargetShipGroup.LeadShip.ParentHabitat;
            return this.LeadShip.Mission.TargetShipGroup.LeadShip.NearestSystemStar != null ? (StellarObject) this.LeadShip.Mission.TargetShipGroup.LeadShip.NearestSystemStar : (StellarObject) this.LeadShip.Mission.TargetShipGroup.LeadShip;
          }
        }
        else if (this.LeadShip.Mission.TargetHabitat != null)
          return (StellarObject) this.LeadShip.Mission.TargetHabitat;
      }
      if (this.LeadShip.ParentHabitat != null)
        return (StellarObject) this.LeadShip.ParentHabitat;
      if (this.LeadShip.ParentBuiltObject != null)
        return (StellarObject) this.LeadShip.ParentBuiltObject;
      return this.LeadShip.NearestSystemStar != null ? (StellarObject) this.LeadShip.NearestSystemStar : (StellarObject) this.LeadShip;
    }

    public Point DetermineApproximateActualFleetLocation()
    {
      double x = this.LeadShip.Xpos;
      double y = this.LeadShip.Ypos;
      double proportionInIndex = 0.0;
      GalaxyIndex ofMostFleetShips = this.DetermineIndexOfMostFleetShips(out proportionInIndex);
      GalaxyIndex galaxyIndex = this._Galaxy.ResolveIndex(this.LeadShip.Xpos, this.LeadShip.Ypos);
      if ((ofMostFleetShips.X != galaxyIndex.X || ofMostFleetShips.Y != galaxyIndex.Y) && proportionInIndex > 0.66)
      {
        x = (double) (ofMostFleetShips.X * Galaxy.IndexSize - Galaxy.IndexSize / 2);
        y = (double) (ofMostFleetShips.Y * Galaxy.IndexSize - Galaxy.IndexSize / 2);
      }
      return new Point((int) x, (int) y);
    }

    public Point DetermineActualFleetLocation()
    {
      double proportionInIndex = 0.0;
      GalaxyIndex ofMostFleetShips = this.DetermineIndexOfMostFleetShips(out proportionInIndex);
      if (proportionInIndex <= 0.66)
        return new Point((int) this.LeadShip.Xpos, (int) this.LeadShip.Ypos);
      BuiltObject builtObject = (BuiltObject) null;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        GalaxyIndex galaxyIndex = this._Galaxy.ResolveIndex(ship.Xpos, ship.Ypos);
        if (galaxyIndex.X == ofMostFleetShips.X && galaxyIndex.Y == ofMostFleetShips.Y && (builtObject == null || ship.Size > builtObject.Size))
          builtObject = ship;
      }
      return builtObject != null ? new Point((int) builtObject.Xpos, (int) builtObject.Ypos) : new Point((int) this.LeadShip.Xpos, (int) this.LeadShip.Ypos);
    }

    public GalaxyIndex DetermineIndexOfMostFleetShips(out double proportionInIndex)
    {
      proportionInIndex = 0.0;
      GalaxyIndexList galaxyIndexList = new GalaxyIndexList();
      for (int index1 = 0; index1 < this.Ships.Count; ++index1)
      {
        BuiltObject ship = this.Ships[index1];
        GalaxyIndex index2 = this._Galaxy.ResolveIndex(ship.Xpos, ship.Ypos);
        if (!galaxyIndexList.Contains(index2))
        {
          index2.SortTag = 1;
          galaxyIndexList.Add(index2);
        }
        else
        {
          int index3 = galaxyIndexList.IndexOf(index2);
          if (index3 >= 0)
            ++galaxyIndexList[index3].SortTag;
        }
      }
      if (galaxyIndexList.Count <= 0)
        return (GalaxyIndex) null;
      galaxyIndexList.Sort();
      galaxyIndexList.Reverse();
      proportionInIndex = (double) galaxyIndexList[0].SortTag / (double) this.Ships.Count;
      return galaxyIndexList[0];
    }

    public void Update() => this.DetermineLeadShip();

    public void DetermineLeadShip() => this.DetermineLeadShip((BuiltObject) null);

    public void DetermineLeadShip(BuiltObject shipToExclude)
    {
      bool useFleetIndexing = true;
      double proportionInIndex = 0.0;
      GalaxyIndex ofMostFleetShips = this.DetermineIndexOfMostFleetShips(out proportionInIndex);
      if (proportionInIndex < 0.4)
        useFleetIndexing = false;
      BuiltObject strongestShip = this.DetermineStrongestShip(shipToExclude, useFleetIndexing, ofMostFleetShips);
      if (strongestShip != null)
      {
        this.LeadShip = strongestShip;
      }
      else
      {
        if (this.Ships.Count <= 0)
          return;
        this.LeadShip = this.Ships[0];
      }
    }

    public BuiltObject DetermineLargestShip(
      BuiltObject shipToExclude,
      bool useFleetIndexing,
      GalaxyIndex fleetIndex)
    {
      BuiltObject largestShip = (BuiltObject) null;
      int num = 0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if ((shipToExclude == null || ship != shipToExclude) && ship.Size > num)
        {
          if (useFleetIndexing)
          {
            GalaxyIndex galaxyIndex = this._Galaxy.ResolveIndex(ship.Xpos, ship.Ypos);
            if (galaxyIndex.X == fleetIndex.X && galaxyIndex.Y == fleetIndex.Y)
            {
              num = ship.Size;
              largestShip = ship;
            }
          }
          else
          {
            num = ship.Size;
            largestShip = ship;
          }
        }
      }
      return largestShip;
    }

    public BuiltObject DetermineStrongestShip(
      BuiltObject shipToExclude,
      bool useFleetIndexing,
      GalaxyIndex fleetIndex)
    {
      BuiltObject strongestShip = (BuiltObject) null;
      int num = 0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if ((shipToExclude == null || ship != shipToExclude) && ship.FirepowerRaw > num)
        {
          if (useFleetIndexing)
          {
            GalaxyIndex galaxyIndex = this._Galaxy.ResolveIndex(ship.Xpos, ship.Ypos);
            if (galaxyIndex.X == fleetIndex.X && galaxyIndex.Y == fleetIndex.Y)
            {
              num = ship.FirepowerRaw;
              strongestShip = ship;
            }
          }
          else
          {
            num = ship.FirepowerRaw;
            strongestShip = ship;
          }
        }
      }
      return strongestShip;
    }

    public BuiltObject DetermineStrongestTroopTransport()
    {
      BuiltObject strongestTroopTransport = (BuiltObject) null;
      int num = 0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship.Troops != null && ship.Troops.TotalAttackStrength > num)
        {
          strongestTroopTransport = ship;
          num = ship.Troops.TotalAttackStrength;
        }
      }
      return strongestTroopTransport;
    }

    public BuiltObjectRole Role
    {
      get => this._Role;
      set => this._Role = value;
    }

    public string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    public Empire Empire
    {
      get => this._Empire;
      set
      {
        this._Empire = value;
        if (this._Empire == null)
          return;
        this.AttackRangeSquared = this._Empire.AttackRangeOther < 0 ? (this.LeadShip == null ? 2.304E+09f : (float) this.LeadShip.SensorProximityArrayRange * (float) this.LeadShip.SensorProximityArrayRange) : (float) this._Empire.AttackRangeOther * (float) this._Empire.AttackRangeOther;
        if (this._Empire.Policy == null)
          return;
        this.SetTroopLoadoutsFromPolicy(this._Empire.Policy);
      }
    }

    public int TotalFuelCapacity
    {
      get
      {
        int totalFuelCapacity = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          totalFuelCapacity += ship.FuelCapacity;
        }
        return totalFuelCapacity;
      }
    }

    public int TotalTroopSpaceUsed
    {
      get
      {
        int totalTroopSpaceUsed = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship != null && ship.Troops != null)
            totalTroopSpaceUsed += ship.Troops.TotalSize;
        }
        return totalTroopSpaceUsed;
      }
    }

    public int TotalTroopSpaceRemaining
    {
      get
      {
        int troopSpaceRemaining = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          int capacityRemaining = this.Ships[index].TroopCapacityRemaining;
          if (capacityRemaining >= 100)
            troopSpaceRemaining += capacityRemaining;
        }
        return troopSpaceRemaining;
      }
    }

    public void GetTroopLoadoutTargetAmounts(
      out int infantryAmount,
      out int artilleryAmount,
      out int armorAmount,
      out int specialForcesAmount)
    {
      this.GetTroopLoadoutTargetAmounts(true, out infantryAmount, out artilleryAmount, out armorAmount, out specialForcesAmount);
    }

    public void GetTroopLoadoutTargetAmounts(
      bool refactorForDisabledTroopTypes,
      out int infantryAmount,
      out int artilleryAmount,
      out int armorAmount,
      out int specialForcesAmount)
    {
      infantryAmount = 0;
      artilleryAmount = 0;
      armorAmount = 0;
      specialForcesAmount = 0;
      int num1 = (int) (100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Infantry));
      int num2 = (int) (100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Armored));
      int num3 = (int) (100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Artillery));
      int num4 = (int) (100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.SpecialForces));
      int totalTroopCapacity = this.TotalTroopCapacity;
      if (this.TroopLoadoutInfantry == byte.MaxValue && this.TroopLoadoutArtillery == byte.MaxValue && this.TroopLoadoutArmored == byte.MaxValue && this.TroopLoadoutSpecialForces == byte.MaxValue)
      {
        infantryAmount = totalTroopCapacity / num1;
        artilleryAmount = totalTroopCapacity / num3;
        armorAmount = totalTroopCapacity / num2;
        specialForcesAmount = totalTroopCapacity / num4;
      }
      else
      {
        float num5 = (float) this.TroopLoadoutInfantry + (float) this.TroopLoadoutArtillery + (float) this.TroopLoadoutArmored + (float) this.TroopLoadoutSpecialForces;
        if ((double) num5 <= 0.0)
          num5 = 100f;
        infantryAmount = (int) ((double) this.TroopLoadoutInfantry / (double) num5 * (double) totalTroopCapacity) / num1;
        artilleryAmount = (int) ((double) this.TroopLoadoutArtillery / (double) num5 * (double) totalTroopCapacity) / num3;
        armorAmount = (int) ((double) this.TroopLoadoutArmored / (double) num5 * (double) totalTroopCapacity) / num2;
        specialForcesAmount = (int) ((double) this.TroopLoadoutSpecialForces / (double) num5 * (double) totalTroopCapacity) / num4;
        if (refactorForDisabledTroopTypes && this.Empire != null)
        {
          if (!this.Empire.TroopCanRecruitSpecialForces)
          {
            armorAmount += specialForcesAmount * num4 / num2;
            specialForcesAmount = 0;
          }
          if (!this.Empire.TroopCanRecruitArmored)
          {
            infantryAmount += armorAmount * num2 / num1;
            armorAmount = 0;
          }
          if (!this.Empire.TroopCanRecruitArtillery)
          {
            infantryAmount += artilleryAmount * num3 / num1;
            artilleryAmount = 0;
          }
        }
        int num6 = infantryAmount * num1 + artilleryAmount * num3 + armorAmount * num2 + specialForcesAmount * num4;
        int num7 = totalTroopCapacity - num6;
        if (num7 < 100 || this.Empire == null)
          return;
        if (num7 >= num2 && this.Empire.TroopCanRecruitArmored && (int) this.TroopLoadoutArmored >= (int) this.TroopLoadoutInfantry && (int) this.TroopLoadoutArmored >= (int) this.TroopLoadoutArtillery && (int) this.TroopLoadoutArmored >= (int) this.TroopLoadoutSpecialForces)
        {
          int num8 = num7 / num2;
          armorAmount += num8;
          num7 -= num8 * num2;
        }
        if (num7 >= num4 && this.Empire.TroopCanRecruitSpecialForces && (int) this.TroopLoadoutSpecialForces >= (int) this.TroopLoadoutInfantry && (int) this.TroopLoadoutSpecialForces >= (int) this.TroopLoadoutArtillery && (int) this.TroopLoadoutSpecialForces >= (int) this.TroopLoadoutArmored)
        {
          int num9 = num7 / num4;
          specialForcesAmount += num9;
          num7 -= num9 * num4;
        }
        if (num7 >= num3 && this.Empire.TroopCanRecruitArtillery && (int) this.TroopLoadoutArtillery >= (int) this.TroopLoadoutInfantry && (int) this.TroopLoadoutArtillery >= (int) this.TroopLoadoutArmored && (int) this.TroopLoadoutArtillery >= (int) this.TroopLoadoutSpecialForces)
        {
          int num10 = num7 / num3;
          artilleryAmount += num10;
          num7 -= num10 * num3;
        }
        if (num7 >= num1 && this.Empire.TroopCanRecruitInfantry && (int) this.TroopLoadoutInfantry >= (int) this.TroopLoadoutArmored && (int) this.TroopLoadoutInfantry >= (int) this.TroopLoadoutArtillery && (int) this.TroopLoadoutInfantry >= (int) this.TroopLoadoutSpecialForces)
        {
          int num11 = num7 / num1;
          infantryAmount += num11;
          num7 -= num11 * num1;
        }
        if (num7 < 100)
          return;
        int num12 = num7 / num1;
        infantryAmount += num12;
        int num13 = num7 - num12 * num1;
      }
    }

    public void GetTroopCountsByType(
      out int infantryCount,
      out int artilleryCount,
      out int armorCount,
      out int specialForcesCount)
    {
      infantryCount = 0;
      artilleryCount = 0;
      armorCount = 0;
      specialForcesCount = 0;
      for (int index1 = 0; index1 < this.Ships.Count; ++index1)
      {
        BuiltObject ship = this.Ships[index1];
        if (ship != null && ship.Troops != null && ship.Troops.Count > 0)
        {
          for (int index2 = 0; index2 < ship.Troops.Count; ++index2)
          {
            Troop troop = ship.Troops[index2];
            if (troop != null)
            {
              switch (troop.Type)
              {
                case TroopType.Infantry:
                  ++infantryCount;
                  continue;
                case TroopType.Armored:
                  ++armorCount;
                  continue;
                case TroopType.Artillery:
                  ++artilleryCount;
                  continue;
                case TroopType.SpecialForces:
                  ++specialForcesCount;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }

    public int TotalTroopCount
    {
      get
      {
        int totalTroopCount = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship.Troops != null && ship.Troops.Count > 0)
            totalTroopCount += ship.Troops.Count;
        }
        return totalTroopCount;
      }
    }

    public int TotalTroopCapacity
    {
      get
      {
        int totalTroopCapacity = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship.TroopCapacity > 0)
            totalTroopCapacity += ship.TroopCapacity;
        }
        return totalTroopCapacity;
      }
    }

    public int TotalTroopAttackStrength
    {
      get
      {
        int troopAttackStrength = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship.Troops != null && ship.Troops.TotalAttackStrength > 0)
            troopAttackStrength += ship.Troops.TotalAttackStrength;
        }
        return troopAttackStrength;
      }
    }

    public int TotalTroopAttackStrengthNearby(double requiredFuelLevel)
    {
      int num = 0;
      if (this.LeadShip != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship.Troops != null && ship.Troops.TotalAttackStrength > 0 && this._Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, this.LeadShip.Xpos, this.LeadShip.Ypos) < 2304000000.0 && ship.CurrentFuel / Math.Max(1.0, (double) ship.FuelCapacity) >= requiredFuelLevel)
            num += ship.Troops.TotalAttackStrength;
        }
      }
      else
        num = this.TotalTroopAttackStrength;
      return num;
    }

    public int TotalTroopDefendStrength
    {
      get
      {
        int troopDefendStrength = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship.Troops != null && ship.Troops.TotalDefendStrength > 0)
            troopDefendStrength += ship.Troops.TotalDefendStrength;
        }
        return troopDefendStrength;
      }
    }

    public int TotalTroopDefendStrengthNearby(double requiredFuelLevel)
    {
      int num = 0;
      if (this.LeadShip != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship.Troops != null && ship.Troops.TotalDefendStrength > 0 && this._Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, this.LeadShip.Xpos, this.LeadShip.Ypos) < 2304000000.0 && ship.CurrentFuel / Math.Max(1.0, (double) ship.FuelCapacity) >= requiredFuelLevel)
            num += ship.Troops.TotalDefendStrength;
        }
      }
      else
        num = this.TotalTroopDefendStrength;
      return num;
    }

    public int TotalDamage
    {
      get
      {
        int totalDamage = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          totalDamage += ship.DamagedComponentCount;
        }
        return totalDamage;
      }
    }

    public int TotalFighterCount
    {
      get
      {
        int totalFighterCount = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          if (this.Ships[index].Fighters != null)
            totalFighterCount += this.Ships[index].Fighters.Count;
        }
        return totalFighterCount;
      }
    }

    public int TotalOverallStrengthFactor
    {
      get
      {
        int overallStrengthFactor = 0;
        foreach (BuiltObject builtObject in ListHelper.ToArrayThreadSafe(this.Ships))
        {
          if (builtObject != null)
            overallStrengthFactor += builtObject.CalculateOverallStrengthFactor();
        }
        return overallStrengthFactor;
      }
    }

    public int TotalFirepower
    {
      get
      {
        int totalFirepower = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          totalFirepower += ship.FirepowerRaw;
        }
        return totalFirepower;
      }
    }

    public int TotalFirepowerNearby(double requiredFuelLevel)
    {
      int num = 0;
      if (this.LeadShip != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (this._Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, this.LeadShip.Xpos, this.LeadShip.Ypos) < 2304000000.0 && ship.CurrentFuel / Math.Max(1.0, (double) ship.FuelCapacity) >= requiredFuelLevel)
            num += ship.FirepowerRaw;
        }
      }
      else
        num = this.TotalFirepower;
      return num;
    }

    public int TotalBombardPower
    {
      get
      {
        int totalBombardPower = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          totalBombardPower += ship.BombardWeaponPower;
        }
        return totalBombardPower;
      }
    }

    public int TotalBombardPowerNearby(double requiredFuelLevel)
    {
      int num = 0;
      if (this.LeadShip != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (this._Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, this.LeadShip.Xpos, this.LeadShip.Ypos) < 2304000000.0 && ship.CurrentFuel / Math.Max(1.0, (double) ship.FuelCapacity) >= requiredFuelLevel)
            num += ship.BombardWeaponPower;
        }
      }
      else
        num = this.TotalBombardPower;
      return num;
    }

    public int TotalAssaultStrength
    {
      get
      {
        int totalAssaultStrength = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship != null && !ship.HasBeenDestroyed)
            totalAssaultStrength += (int) ship.AssaultStrength;
        }
        return totalAssaultStrength;
      }
    }

    public int TotalAssaultPodCount
    {
      get
      {
        int totalAssaultPodCount = 0;
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship != null && !ship.HasBeenDestroyed)
            totalAssaultPodCount += ship.CountAssaultPods();
        }
        return totalAssaultPodCount;
      }
    }

    public int TotalAvailableBoardingAssaultStrengthCapturingTarget(
      DateTime time,
      StellarObject target)
    {
      int num = 0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null && !ship.HasBeenDestroyed)
        {
          BuiltObjectMission mission = ship.Mission;
          if (mission != null && mission.Target == target && (mission.Type == BuiltObjectMissionType.Raid || mission.Type == BuiltObjectMissionType.Capture))
          {
            int assaultPodCount = 0;
            int assaultPodsAvailable = 0;
            num += ship.CalculateAssaultPodAttackValues(time, out assaultPodCount, out assaultPodsAvailable);
          }
        }
      }
      return num;
    }

    public int TotalAvailableBoardingAssaultStrength(DateTime time)
    {
      int num = 0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null && !ship.HasBeenDestroyed)
        {
          int assaultPodCount = 0;
          int assaultPodsAvailable = 0;
          num += ship.CalculateAssaultPodAttackValues(time, out assaultPodCount, out assaultPodsAvailable);
        }
      }
      return num;
    }

    public int CalculateAssaultPodAttackValues(
      DateTime time,
      out int assaultPodCount,
      out int assaultPodsAvailable)
    {
      int assaultPodAttackValues = 0;
      assaultPodCount = 0;
      assaultPodsAvailable = 0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null && !ship.HasBeenDestroyed)
        {
          int assaultPodCount1 = 0;
          int assaultPodsAvailable1 = 0;
          assaultPodAttackValues += ship.CalculateAssaultPodAttackValues(time, out assaultPodCount1, out assaultPodsAvailable1);
          assaultPodCount += assaultPodCount1;
          assaultPodsAvailable += assaultPodsAvailable1;
        }
      }
      return assaultPodAttackValues;
    }

    public void RemoveShipsWithoutHyperdrive()
    {
      if (this.WarpSpeed <= 0)
        return;
      BuiltObjectList builtObjectList = new BuiltObjectList();
      for (int index = 0; index < this._Ships.Count; ++index)
      {
        BuiltObject ship = this._Ships[index];
        if (ship.WarpSpeed <= 0 && ship.BuiltAt == null)
          builtObjectList.Add(ship);
      }
      foreach (BuiltObject builtObject in (SyncList<BuiltObject>) builtObjectList)
        builtObject.LeaveShipGroup();
    }

    public int WarpSpeed
    {
      get
      {
        if (this._Ships.Count <= 0)
          return 0;
        int num = 536870911;
        for (int index = 0; index < this._Ships.Count; ++index)
        {
          BuiltObject ship = this._Ships[index];
          if (ship.BuiltAt == null && ship.WarpSpeed < num)
            num = ship.WarpSpeed;
        }
        return (int) ((double) num * this.HyperjumpSpeedBonus);
      }
    }

    public int CruiseSpeed
    {
      get
      {
        if (this._Ships.Count <= 0)
          return 0;
        int cruiseSpeed = 536870911;
        for (int index = 0; index < this._Ships.Count; ++index)
        {
          BuiltObject ship = this._Ships[index];
          if ((int) ship.CruiseSpeed < cruiseSpeed)
            cruiseSpeed = (int) ship.CruiseSpeed;
        }
        return cruiseSpeed;
      }
    }

    public int TopSpeed
    {
      get
      {
        if (this._Ships.Count <= 0)
          return 0;
        int topSpeed = 536870911;
        for (int index = 0; index < this._Ships.Count; ++index)
        {
          BuiltObject ship = this._Ships[index];
          if ((int) ship.TopSpeed < topSpeed)
            topSpeed = (int) ship.TopSpeed;
        }
        return topSpeed;
      }
    }

    private bool AssignMissionToShips(BuiltObjectMission mission, bool manuallyAssigned)
    {
      bool ships = false;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        bool flag = this.IsShipAvailable(ship);
        if (manuallyAssigned && ship.BuiltAt == null)
          flag = true;
        if (flag || this.IsShipAvailableAfterMission(ship))
        {
          mission.ResolveTargetCoordinates(mission);
          double x1 = (double) mission.X;
          double y1 = (double) mission.Y;
          if ((double) mission.X > -2000000000.0 && (double) mission.Y > -2000000000.0)
          {
            if (mission.Target == null && mission.SecondaryTarget == null)
            {
              double x2;
              double y2;
              this._Galaxy.SelectRelativePoint(300.0, out x2, out y2);
              x1 += x2;
              y1 += y2;
            }
            else
            {
              double x3;
              double y3;
              this._Galaxy.SelectRelativePoint(300.0, out x3, out y3);
              x1 += x3;
              y1 += y3;
            }
          }
          else if (mission.Type == BuiltObjectMissionType.Move)
          {
            double x4;
            double y4;
            this._Galaxy.SelectRelativePoint(300.0, out x4, out y4);
            x1 = x4;
            y1 = y4;
          }
          object target = (object) null;
          object target2 = (object) null;
          if (mission.TargetBuiltObject != null)
            target = (object) mission.TargetBuiltObject;
          else if (mission.TargetHabitat != null)
            target = (object) mission.TargetHabitat;
          else if (mission.TargetCreature != null)
            target = (object) mission.TargetCreature;
          else if (mission.TargetShipGroup != null)
            target = (object) mission.TargetShipGroup;
          if (mission.SecondaryTargetBuiltObject != null)
            target2 = (object) mission.SecondaryTargetBuiltObject;
          else if (mission.SecondaryTargetHabitat != null)
            target2 = (object) mission.SecondaryTargetHabitat;
          else if (mission.SecondaryTargetCreature != null)
            target2 = (object) mission.SecondaryTargetCreature;
          else if (mission.SecondaryTargetShipGroup != null)
            target2 = (object) mission.SecondaryTargetShipGroup;
          ship.RevertMission = (BuiltObjectMission) null;
          if (flag)
          {
            ship.ClearPreviousMissionRequirements();
            ship.AssignMission(mission.Type, target, target2, mission.Cargo, mission.Troops, mission.Population, mission.Design, x1, y1, mission.StarDate, mission.Priority, true, manuallyAssigned);
            ship.Mission.IsShipGroupMission = true;
          }
          else
            ship.QueueMission(mission.Type, target, target2, x1, y1, mission.StarDate, mission.Priority);
          ships = true;
        }
      }
      return ships;
    }

    private int CountShipsWithinFuelRange(
      double x,
      double y,
      double fuelReservePortion,
      out int firepowerWithinRange)
    {
      int num = 0;
      firepowerWithinRange = 0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship.WithinReducedFuelRange(x, y, fuelReservePortion))
        {
          firepowerWithinRange += ship.FirepowerRaw;
          ++num;
        }
      }
      return num;
    }

    private int CountShipsWithinFuelRangeAndRefuel(
      double x,
      double y,
      double extraFuelPortionMargin,
      out int firepowerWithinRange)
    {
      int num = 0;
      firepowerWithinRange = 0;
      ResourceList fuelTypes = this.DetermineFuelTypes();
      StellarObject nearestRefuellingPoint = this._Galaxy.FastFindNearestRefuellingPoint(x, y, fuelTypes, this.Empire, this.LeadShip, true, (Empire) null, this.Ships.Count);
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship.WithinFuelRangeAndRefuel(x, y, extraFuelPortionMargin, nearestRefuellingPoint))
        {
          firepowerWithinRange += ship.CalculateOverallStrengthFactor();
          ++num;
        }
      }
      return num;
    }

    public double CurrentRange()
    {
      double num1 = double.MaxValue;
      if (this.Ships != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship != null)
          {
            double num2 = ship.CurrentRange();
            if (num2 < num1)
              num1 = num2;
          }
        }
      }
      else
        num1 = 0.0;
      return num1;
    }

    public double MaximumRange()
    {
      double num1 = double.MaxValue;
      if (this.Ships != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          BuiltObject ship = this.Ships[index];
          if (ship != null)
          {
            double num2 = ship.MaximumFuelRange();
            if (num2 < num1)
              num1 = num2;
          }
        }
      }
      else
        num1 = 0.0;
      return num1;
    }

    public bool CheckFleetTargetWithinFuelRange(double x, double y, double fuelPortionMargin)
    {
      int num1 = 0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        if (this.Ships[index].WithinFuelRange(x, y, fuelPortionMargin))
          ++num1;
      }
      int num2 = (int) ((double) this.Ships.Count * 1.0);
      return num1 >= num2;
    }

    public double CalculateRefuellingPortion() => this.CalculateRefuellingPortion(true);

    public double CalculateRefuellingPortion(bool aggressiveRefuelling)
    {
      ResourceList requiredFuel = this.CalculateRequiredFuel();
      StellarObject refuellingPoint = (StellarObject) null;
      BuiltObject leadShip = this.LeadShip;
      if (leadShip != null)
        refuellingPoint = this.Empire == null ? this._Galaxy.FastFindNearestRefuellingPoint(leadShip.Xpos, leadShip.Ypos, requiredFuel, this.Empire, leadShip, true, (Empire) null, this.Ships.Count) : this.Empire.UltraFastFindNearestRefuellingLocation(leadShip.Xpos, leadShip.Ypos, requiredFuel, leadShip, false, true, this.Ships.Count);
      this.CurrentRange();
      double refuellingPortion = 1.0;
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null)
        {
          double refuellingPoints = ship.CalculateFuelPortionMarginFromNearbyRefuellingPoints(ship.Xpos, ship.Ypos, refuellingPoint);
          refuellingPortion = Math.Min(refuellingPortion, refuellingPoints);
        }
      }
      if (aggressiveRefuelling)
      {
        BuiltObjectMission mission = this.Mission;
        if (mission == null || mission.Type == BuiltObjectMissionType.Undefined)
        {
          if (this.CheckAnyShipsInBattle())
          {
            refuellingPortion = Math.Max(refuellingPortion, 0.2);
          }
          else
          {
            refuellingPortion = Math.Max(refuellingPortion, 0.5);
            if (refuellingPoint != null && leadShip != null && this._Galaxy.CalculateDistance(leadShip.Xpos, leadShip.Ypos, refuellingPoint.Xpos, refuellingPoint.Ypos) < 48000.0)
              refuellingPortion = Math.Max(refuellingPortion, 0.67);
          }
        }
      }
      if (this.LeadShip != null)
        refuellingPortion = !this.LeadShip.IsAutoControlled ? Math.Min(0.5, refuellingPortion) : Math.Max(0.05, refuellingPortion);
      return refuellingPortion;
    }

    public bool CheckAnyShipsInBattle()
    {
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null)
        {
          BuiltObjectMission mission = ship.Mission;
          if (mission != null)
          {
            switch (mission.Type)
            {
              case BuiltObjectMissionType.Attack:
              case BuiltObjectMissionType.WaitAndAttack:
              case BuiltObjectMissionType.WaitAndBombard:
              case BuiltObjectMissionType.Bombard:
              case BuiltObjectMissionType.Capture:
                return true;
              default:
                continue;
            }
          }
        }
      }
      return false;
    }

    public bool CheckAnyShipsLoadingTroops()
    {
      for (int index = 0; index < this.Ships.Count; ++index)
      {
        BuiltObject ship = this.Ships[index];
        if (ship != null)
        {
          BuiltObjectMission mission = ship.Mission;
          if (mission != null && mission.Type == BuiltObjectMissionType.LoadTroops)
            return true;
        }
      }
      return false;
    }

    public bool CheckFleetTargetWithinFuelRangeAndRefuel(
      double x,
      double y,
      double extraFuelPortionMargin)
    {
      int num1 = 0;
      ResourceList requiredFuel = this.CalculateRequiredFuel();
      StellarObject refuellingPoint = this.Empire == null ? this._Galaxy.FastFindNearestRefuellingPoint(x, y, requiredFuel, this.Empire, this.LeadShip, true, (Empire) null, this.Ships.Count) : this.Empire.UltraFastFindNearestRefuellingLocation(x, y, requiredFuel, this.LeadShip, false, true, this.Ships.Count);
      if (refuellingPoint != null)
      {
        for (int index = 0; index < this.Ships.Count; ++index)
        {
          if (this.Ships[index].WithinFuelRangeAndRefuel(x, y, extraFuelPortionMargin, refuellingPoint))
            ++num1;
        }
        int num2 = (int) ((double) this.Ships.Count * 1.0);
        if (num1 >= num2)
          return true;
      }
      return false;
    }

    public bool IsShipAvailableAfterMission(BuiltObject ship) => ship.BuiltAt == null;

    public bool IsShipAvailable(BuiltObject ship) => ship.BuiltAt == null && ship.RetrofitDesign == null && (ship.Mission == null || ship.Mission.Type != BuiltObjectMissionType.Repair);

    int IComparable<ShipGroup>.CompareTo(ShipGroup other)
    {
      int num = this.SortTag.CompareTo(other.SortTag);
      return num == 0 ? this.Name.CompareTo(other.Name) : num;
    }
  }
}
