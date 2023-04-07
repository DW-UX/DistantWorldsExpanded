// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireActivityList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireActivityList : SyncList<EmpireActivity>
  {
    public EmpireActivity this[Empire targetEmpire]
    {
      get
      {
        foreach (EmpireActivity empireActivity in (SyncList<EmpireActivity>) this)
        {
          if (empireActivity.TargetEmpire == targetEmpire)
            return empireActivity;
        }
        return (EmpireActivity) null;
      }
    }

    public int CountMissionsInSameSystem(
      Habitat systemStar,
      EmpireActivityType type,
      Empire requestingEmpire)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.Type == type && empireActivity.RequestingEmpire == requestingEmpire)
        {
          Habitat habitat = (Habitat) null;
          if (empireActivity.Target is Habitat)
            habitat = Galaxy.DetermineHabitatSystemStar((Habitat) empireActivity.Target);
          else if (empireActivity.Target is BuiltObject)
            habitat = ((BuiltObject) empireActivity.Target).NearestSystemStar;
          if (habitat != null && habitat == systemStar)
            ++num;
        }
      }
      return num;
    }

    public EmpireActivityList ResolveActivitiesByType(EmpireActivityType type)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Type == type)
            empireActivityList.Add(this[index]);
        }
      }
      return empireActivityList;
    }

    public int IndexOfTarget(Empire targetEmpire, EmpireActivityType type)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Type == type && this[index].TargetEmpire == targetEmpire)
            return index;
        }
      }
      return -1;
    }

    public int IndexOfRequester(Empire requestingEmpire, EmpireActivityType type)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Type == type && this[index].RequestingEmpire == requestingEmpire)
            return index;
        }
      }
      return -1;
    }

    public int IndexOf(Empire empire)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].TargetEmpire == empire)
            return index;
        }
      }
      return -1;
    }

    public bool Contains(Empire targetEmpire, EmpireActivityType type)
    {
      foreach (EmpireActivity empireActivity in (SyncList<EmpireActivity>) this)
      {
        if (empireActivity.Type == type && empireActivity.TargetEmpire == targetEmpire)
          return true;
      }
      return false;
    }

    public bool Contains(Empire empire)
    {
      foreach (EmpireActivity empireActivity in (SyncList<EmpireActivity>) this)
      {
        if (empireActivity.TargetEmpire == empire)
          return true;
      }
      return false;
    }

    public void RemoveEquivalent(EmpireActivity empireActivity)
    {
      if (empireActivity == null)
        return;
      this.RemoveEquivalent(empireActivity.Target, empireActivity.Type);
    }

    public void RemoveEquivalent(StellarObject target, EmpireActivityType type)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.CheckEquivalent(target, type))
          empireActivityList.Add(empireActivity);
      }
      for (int index = 0; index < empireActivityList.Count; ++index)
        this.Remove(empireActivityList[index]);
    }

    public void StripMissionsWithTargetEmpire(Empire empire)
    {
      if (empire == null)
        return;
      EmpireActivityList empireActivityList = new EmpireActivityList();
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.TargetEmpire == empire)
          empireActivityList.Add(empireActivity);
      }
      for (int index = 0; index < empireActivityList.Count; ++index)
        this.Remove(empireActivityList[index]);
    }

    public bool ContainsEquivalent(EmpireActivity empireActivity)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity1 = this[index];
        if (empireActivity1 != null && empireActivity1.CheckEquivalent(empireActivity))
          return true;
      }
      return false;
    }

    public bool ContainsEquivalent(StellarObject target, EmpireActivityType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.CheckEquivalent(target, type))
          return true;
      }
      return false;
    }

    public int CountByType(EmpireActivityType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.Type == type)
          ++num;
      }
      return num;
    }

    public double CalculateTotalAttackCosts(Empire requester)
    {
      double totalAttackCosts = 0.0;
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.Type == EmpireActivityType.Attack && empireActivity.AssignedEmpire != requester)
          totalAttackCosts += empireActivity.Price;
      }
      return totalAttackCosts;
    }

    public double CalculateTotalDefendCosts(Empire requester)
    {
      double totalDefendCosts = 0.0;
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.Type == EmpireActivityType.Defend && empireActivity.AssignedEmpire != requester)
          totalDefendCosts += empireActivity.Price;
      }
      return totalDefendCosts;
    }

    public EmpireList ResolveAttackTargettedEmpires()
    {
      EmpireList empireList = new EmpireList();
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.TargetEmpire != null && empireActivity.Type == EmpireActivityType.Attack && !empireList.Contains(empireActivity.TargetEmpire))
          empireList.Add(empireActivity.TargetEmpire);
      }
      return empireList;
    }

    public EmpireActivity GetFirstByTargetAndType(StellarObject target, EmpireActivityType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity firstByTargetAndType = this[index];
        if (firstByTargetAndType != null && firstByTargetAndType.Type == type && firstByTargetAndType.Target == target)
          return firstByTargetAndType;
      }
      return (EmpireActivity) null;
    }

    public EmpireActivity GetFirstByTargetAndTypeAssigned(
      StellarObject target,
      EmpireActivityType type,
      Empire requester)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity targetAndTypeAssigned = this[index];
        if (targetAndTypeAssigned != null && targetAndTypeAssigned.Type == type && targetAndTypeAssigned.Target == target && targetAndTypeAssigned.RequestingEmpire == requester && targetAndTypeAssigned.AssignedEmpire != null && targetAndTypeAssigned.AssignedEmpire != requester)
          return targetAndTypeAssigned;
      }
      return (EmpireActivity) null;
    }

    public EmpireActivity GetByAttackTarget(BuiltObject attackTarget, Empire assignedEmpire)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity byAttackTarget = this[index];
        if (byAttackTarget != null && byAttackTarget.Type == EmpireActivityType.Attack && byAttackTarget.Target == attackTarget && (assignedEmpire == null || byAttackTarget.AssignedEmpire == assignedEmpire))
          return byAttackTarget;
      }
      return (EmpireActivity) null;
    }

    public EmpireActivityList ResolveByType(EmpireActivityType type)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.Type == type)
          empireActivityList.Add(empireActivity);
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveByTypeAndRequester(
      EmpireActivityType type,
      Empire requestingEmpire)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.Type == type && empireActivity.RequestingEmpire == requestingEmpire)
          empireActivityList.Add(empireActivity);
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveByTarget(StellarObject target)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null && empireActivity.Target == target)
          empireActivityList.Add(empireActivity);
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveByTypeAndTarget(EmpireActivityType type, StellarObject target)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      if (target != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          EmpireActivity empireActivity = this[index];
          if (empireActivity != null && (empireActivity.Type == type || type == EmpireActivityType.Undefined) && empireActivity.Target == target)
            empireActivityList.Add(empireActivity);
        }
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveByTypeKnownTarget(EmpireActivityType type, Empire empire)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      if (empire != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          EmpireActivity empireActivity = this[index];
          if (empireActivity != null && empireActivity.Type == type && empire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
            empireActivityList.Add(empireActivity);
        }
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveByAllowedDefendTargetsNotRequestedBy(Empire pirateEmpire)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      if (pirateEmpire != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          EmpireActivity empireActivity = this[index];
          if (empireActivity != null && empireActivity.Type == EmpireActivityType.Defend && empireActivity.RequestingEmpire != pirateEmpire && empireActivity.Target != null && pirateEmpire.IsObjectAreaKnownToThisEmpire(empireActivity.Target) && empireActivity.RequestingEmpire.DetermineOfferPirateDefendMissionToPirateFaction(pirateEmpire))
            empireActivityList.Add(empireActivity);
        }
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveWhereRequestingEmpireNot(Empire empire)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      if (empire != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          EmpireActivity empireActivity = this[index];
          if (empireActivity != null && empireActivity.RequestingEmpire != empire)
            empireActivityList.Add(empireActivity);
        }
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveAssigned()
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null)
        {
          if (empireActivity.Type == EmpireActivityType.Smuggle)
            empireActivityList.Add(empireActivity);
          else if (empireActivity.AssignedEmpire != null && empireActivity.BidTimeRemaining == 0L)
            empireActivityList.Add(empireActivity);
        }
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveUnassigned()
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireActivity empireActivity = this[index];
        if (empireActivity != null)
        {
          if (empireActivity.Type == EmpireActivityType.Smuggle)
            empireActivityList.Add(empireActivity);
          else if (empireActivity.AssignedEmpire == null || empireActivity.BidTimeRemaining != 0L)
            empireActivityList.Add(empireActivity);
        }
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveByTypeWhereRequestingEmpireNot(
      Empire empire,
      EmpireActivityType type)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      if (empire != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          EmpireActivity empireActivity = this[index];
          if (empireActivity != null && empireActivity.Type == type && empireActivity.RequestingEmpire != empire)
            empireActivityList.Add(empireActivity);
        }
      }
      return empireActivityList;
    }

    public EmpireActivityList ResolveByKnownAttackTargetsNotRequestedBy(Empire empire)
    {
      EmpireActivityList empireActivityList = new EmpireActivityList();
      if (empire != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          EmpireActivity empireActivity = this[index];
          if (empireActivity != null && empireActivity.Type == EmpireActivityType.Attack && empireActivity.Target != null && empireActivity.RequestingEmpire != empire && empire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
            empireActivityList.Add(empireActivity);
        }
      }
      return empireActivityList;
    }
  }
}
