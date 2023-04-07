// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DeclinedTaskList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DeclinedTaskList : SyncList<DeclinedTask>
  {
    public bool CheckTaskTargetValid(object taskTarget, long starDate)
    {
      bool flag = true;
      if (taskTarget != null)
      {
        int index = this.IndexOf(taskTarget);
        if (index >= 0 && this[index].ExpiryDate >= starDate)
          flag = false;
      }
      return flag;
    }

    public bool CheckAttackEmpireTargetValid(Empire attackEmpireTarget, long starDate)
    {
      bool flag = true;
      if (attackEmpireTarget != null)
      {
        int attackEmpireTarget1 = this.FindIndexForAttackEmpireTarget(attackEmpireTarget);
        if (attackEmpireTarget1 >= 0 && this[attackEmpireTarget1].ExpiryDate >= starDate)
          flag = false;
      }
      return flag;
    }

    public int FindIndexForAttackEmpireTarget(Empire attackEmpireTarget)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].AttackEmpireTarget == attackEmpireTarget)
            return index;
        }
      }
      return -1;
    }

    public int IndexOf(object taskTarget)
    {
      if (taskTarget is BuiltObject)
      {
        BuiltObject builtObject = (BuiltObject) taskTarget;
        lock (this._LockObject)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].TaskTarget is BuiltObject && (BuiltObject) this[index].TaskTarget == builtObject)
              return index;
          }
        }
      }
      if (taskTarget is ShipGroup)
      {
        ShipGroup shipGroup = (ShipGroup) taskTarget;
        lock (this._LockObject)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].TaskTarget is ShipGroup && (ShipGroup) this[index].TaskTarget == shipGroup)
              return index;
          }
        }
      }
      if (taskTarget is Habitat)
      {
        Habitat habitat = (Habitat) taskTarget;
        lock (this._LockObject)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].TaskTarget is Habitat && (Habitat) this[index].TaskTarget == habitat)
              return index;
          }
        }
      }
      if (taskTarget is IntelligenceMission)
      {
        IntelligenceMission intelligenceMission = (IntelligenceMission) taskTarget;
        lock (this._LockObject)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].TaskTarget is IntelligenceMission)
            {
              IntelligenceMission taskTarget1 = (IntelligenceMission) this[index].TaskTarget;
              if (taskTarget1.TargetEmpire == intelligenceMission.TargetEmpire && taskTarget1.Type == intelligenceMission.Type && (taskTarget1.Target != null && taskTarget1.Target == intelligenceMission.Target || taskTarget1.Target == null))
                return index;
            }
          }
        }
      }
      if (taskTarget is Empire)
      {
        Empire empire = (Empire) taskTarget;
        lock (this._LockObject)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].TaskTarget is Empire && (Empire) this[index].TaskTarget == empire)
              return index;
          }
        }
      }
      return -1;
    }
  }
}
