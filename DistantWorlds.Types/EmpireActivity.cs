// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireActivity
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireActivity
  {
    private Empire _TargetEmpire;
    private Empire _RequestingEmpire;
    private long _ExpiryDate;
    private EmpireActivityType _Type;
    private StellarObject _Target;
    public byte ResourceId = byte.MaxValue;
    public Order RelatedOrder;
    public double Price;
    public Empire AssignedEmpire;
    public long BidTimeRemaining = -1;
    [NonSerialized]
    public int DisplayExtraData = -1;
    public double PlayerIncomeEarned;
    public int PlayerAmountDelivered;

    public EmpireActivity(
      Empire targetEmpire,
      Empire requestingEmpire,
      long expiryDate,
      EmpireActivityType type)
    {
      this._TargetEmpire = targetEmpire;
      this._RequestingEmpire = requestingEmpire;
      this._ExpiryDate = expiryDate;
      this._Type = type;
    }

    public EmpireActivity(
      Empire targetEmpire,
      StellarObject attackTarget,
      double attackPrice,
      Empire requestingEmpire,
      long expiryDate,
      EmpireActivityType type)
      : this(targetEmpire, requestingEmpire, expiryDate, type)
    {
      this._Target = attackTarget;
      this.Price = attackPrice;
    }

    public bool ResolveTargetCoordinates(out double x, out double y)
    {
      x = 0.0;
      y = 0.0;
      if (this._Target == null)
        return false;
      x = this._Target.Xpos;
      y = this._Target.Ypos;
      return true;
    }

    public bool CheckEquivalent(EmpireActivity otherActivity) => otherActivity != null && otherActivity.TargetEmpire == this._TargetEmpire && otherActivity.RequestingEmpire == this._RequestingEmpire && otherActivity.Type == this._Type && otherActivity.Target == this._Target;

    public bool CheckEquivalent(StellarObject target, EmpireActivityType type) => target != null && target == this._Target && type == this._Type;

    public Empire TargetEmpire
    {
      get => this._TargetEmpire;
      set => this._TargetEmpire = value;
    }

    public StellarObject Target
    {
      get => this._Target;
      set => this._Target = value;
    }

    public Empire RequestingEmpire
    {
      get => this._RequestingEmpire;
      set => this._RequestingEmpire = value;
    }

    public long ExpiryDate
    {
      get => this._ExpiryDate;
      set => this._ExpiryDate = value;
    }

    public EmpireActivityType Type
    {
      get => this._Type;
      set => this._Type = value;
    }
  }
}
