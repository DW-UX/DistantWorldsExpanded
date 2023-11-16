// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Contract
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Contract
  {
    public int AmountToFulfill;
    public int AmountDelivered;
    public int AmountPickedUp;
    public BuiltObject Freighter;
    public StellarObject Supplier;
    private byte _BuyerEmpireId;
    private short _ResourceId = -1;
    private short _ComponentId = -1;

    public int BuyerEmpireId => (int) this._BuyerEmpireId;

    public int ResourceId => (int) this._ResourceId;

    public int ComponentId => (int) this._ComponentId;

    public Contract(
      StellarObject supplier,
      int amountToFulfill,
      int resourceId,
      int componentId,
      int buyerEmpireId)
    {
      this.AmountToFulfill = amountToFulfill;
      this.AmountDelivered = 0;
      this.AmountPickedUp = 0;
      this.Supplier = supplier;
      this._ResourceId = (short) resourceId;
      this._ComponentId = (short) componentId;
      this._BuyerEmpireId = (byte) buyerEmpireId;
    }
  }
}
