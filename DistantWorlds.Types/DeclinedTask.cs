// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DeclinedTask
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DeclinedTask
  {
    private long _ExpiryDate;
    private object _TaskTarget;
    private Empire _AttackEmpireTarget;

    public DeclinedTask(BuiltObject taskTarget, long expiryDate)
    {
      this._TaskTarget = (object) taskTarget;
      this._ExpiryDate = expiryDate;
    }

    public DeclinedTask(Habitat taskTarget, long expiryDate)
    {
      this._TaskTarget = (object) taskTarget;
      this._ExpiryDate = expiryDate;
    }

    public DeclinedTask(IntelligenceMission taskTarget, long expiryDate)
    {
      this._TaskTarget = (object) taskTarget;
      this._ExpiryDate = expiryDate;
    }

    public DeclinedTask(Empire taskTarget, long expiryDate)
    {
      this._TaskTarget = (object) taskTarget;
      this._ExpiryDate = expiryDate;
    }

    public DeclinedTask(long expiryDate, Empire attackEmpireTarget)
    {
      this._AttackEmpireTarget = attackEmpireTarget;
      this._ExpiryDate = expiryDate;
    }

    public long ExpiryDate
    {
      get => this._ExpiryDate;
      set => this._ExpiryDate = value;
    }

    public Empire AttackEmpireTarget
    {
      get => this._AttackEmpireTarget;
      set => this._AttackEmpireTarget = value;
    }

    public object TaskTarget => this._TaskTarget;
  }
}
