// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Order
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Order : ISerializable
  {
    private int _AmountRequested;
    private int _MaximumFulfillmentDistance;
    private long _ExpiryDate;
    private int _MinimumContractSize;
    public bool IsStateOrder;
    public OrderType Type;
    private Galaxy _Galaxy;
    public Habitat RequestingColony;
    public BuiltObject RequestingBuiltObject;
    private ContractList _Contracts = new ContractList();
    private Component _Component;
    private Resource _Resource;

    public Order()
    {
    }

    public Order(SerializationInfo info, StreamingContext context)
      : this()
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this._AmountRequested = binaryReader.ReadInt32();
          byte num1 = binaryReader.ReadByte();
          if (num1 > (byte) 2)
            num1 = (byte) 0;
          int num2 = (int) binaryReader.ReadByte();
          int num3 = (int) binaryReader.ReadByte();
          int num4 = (int) binaryReader.ReadByte();
          this.Type = (OrderType) num1;
          this._ExpiryDate = binaryReader.ReadInt64();
          int componentID = (int) binaryReader.ReadInt16();
          if (componentID >= 0)
            this._Component = new Component(componentID);
          int resourceId = (int) binaryReader.ReadInt16();
          if (resourceId >= 0)
            this._Resource = new Resource((byte) resourceId);
          binaryReader.Close();
        }
      }
      this._Galaxy = (Galaxy) info.GetValue("Gx", typeof (Galaxy));
      this.RequestingColony = (Habitat) info.GetValue("Ha", typeof (Habitat));
      this.RequestingBuiltObject = (BuiltObject) info.GetValue("BO", typeof (BuiltObject));
      this._Contracts = (ContractList) info.GetValue("Cn", typeof (ContractList));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this._AmountRequested);
          binaryWriter.Write((byte) this.Type);
          binaryWriter.Write((byte) 0);
          binaryWriter.Write((byte) 0);
          binaryWriter.Write((byte) 0);
          binaryWriter.Write(this._ExpiryDate);
          if (this._Component != null)
            binaryWriter.Write((short) this._Component.ComponentID);
          else
            binaryWriter.Write((short) -1);
          if (this._Resource != null)
            binaryWriter.Write((short) this._Resource.ResourceID);
          else
            binaryWriter.Write((short) -1);
          binaryWriter.Flush();
          binaryWriter.Close();
          info.AddValue("D", (object) output.ToArray());
        }
      }
      info.AddValue("Gx", (object) this._Galaxy);
      info.AddValue("Ha", (object) this.RequestingColony);
      info.AddValue("BO", (object) this.RequestingBuiltObject);
      info.AddValue("Cn", (object) this._Contracts);
    }

    public Order(
      Galaxy galaxy,
      BuiltObject requestingBuiltObject,
      Resource commodity,
      int amountRequested,
      long expiryDate,
      int maximumFulfillmentDistance)
    {
      this._Galaxy = galaxy;
      this.RequestingBuiltObject = requestingBuiltObject;
      this.RequestingColony = (Habitat) null;
      this.CommodityResource = commodity;
      this._AmountRequested = amountRequested;
      this._ExpiryDate = expiryDate;
      this._MaximumFulfillmentDistance = maximumFulfillmentDistance;
    }

    public Order(
      Galaxy galaxy,
      BuiltObject requestingBuiltObject,
      Component commodity,
      int amountRequested,
      long expiryDate,
      int maximumFulfillmentDistance)
    {
      this._Galaxy = galaxy;
      this.RequestingBuiltObject = requestingBuiltObject;
      this.RequestingColony = (Habitat) null;
      this.CommodityComponent = commodity;
      this._AmountRequested = amountRequested;
      this._ExpiryDate = expiryDate;
      this._MaximumFulfillmentDistance = maximumFulfillmentDistance;
    }

    public Order(
      Galaxy galaxy,
      Habitat requestingColony,
      Resource commodity,
      int amountRequested,
      long expiryDate,
      int maximumFulfillmentDistance)
    {
      this._Galaxy = galaxy;
      this.RequestingBuiltObject = (BuiltObject) null;
      this.RequestingColony = requestingColony;
      this.CommodityResource = commodity;
      this._AmountRequested = amountRequested;
      this._ExpiryDate = expiryDate;
      this._MaximumFulfillmentDistance = maximumFulfillmentDistance;
    }

    public Order(
      Galaxy galaxy,
      Habitat requestingColony,
      Component commodity,
      int amountRequested,
      long expiryDate,
      int maximumFulfillmentDistance)
    {
      this._Galaxy = galaxy;
      this.RequestingBuiltObject = (BuiltObject) null;
      this.RequestingColony = requestingColony;
      this.CommodityComponent = commodity;
      this._AmountRequested = amountRequested;
      this._ExpiryDate = expiryDate;
      this._MaximumFulfillmentDistance = maximumFulfillmentDistance;
    }

    public int AmountToFulfill
    {
      get
      {
        int amountToFulfill = 0;
        for (int index = 0; index < this._Contracts.Count; ++index)
        {
          Contract contract = this._Contracts[index];
          if (contract != null)
            amountToFulfill += contract.AmountToFulfill;
        }
        return amountToFulfill;
      }
    }

    public int AmountDelivered
    {
      get
      {
        int amountDelivered = 0;
        for (int index = 0; index < this._Contracts.Count; ++index)
        {
          Contract contract = this._Contracts[index];
          if (contract != null)
            amountDelivered += contract.AmountDelivered;
        }
        return amountDelivered;
      }
    }

    public int AmountStillToArrive => this.AmountToFulfill - this.AmountDelivered;

    public int AmountOutstandingToContract => this.AmountRequested - this.AmountToFulfill;

    public ContractList Contracts
    {
      get => this._Contracts;
      set => this._Contracts = value;
    }

    public int MinimumContractSize
    {
      get => this._MinimumContractSize;
      set => this._MinimumContractSize = value;
    }

    public Resource CommodityResource
    {
      get => this._Resource;
      set
      {
        this._Resource = value;
        this._Component = (Component) null;
      }
    }

    public Component CommodityComponent
    {
      get => this._Component;
      set
      {
        this._Resource = (Resource) null;
        this._Component = value;
      }
    }

    public int AmountRequested
    {
      get => this._AmountRequested;
      set => this._AmountRequested = value;
    }

    public int MaximumFulfillmentDistance
    {
      get => this._MaximumFulfillmentDistance;
      set => this._MaximumFulfillmentDistance = value;
    }

    public long ExpiryDate
    {
      get => this._ExpiryDate;
      set => this._ExpiryDate = value;
    }
  }
}
