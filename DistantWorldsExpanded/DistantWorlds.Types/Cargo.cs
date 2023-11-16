// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Cargo
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Cargo : ISerializable
  {
    private Resource _Resource;
    private Component _Component;
    public int Amount;
    public int EmpireId;
    public int Reserved;
    public bool CommodityIsComponent;
    public bool CommodityIsResource;

    public Cargo(Resource resource, int amount, Empire empire, int reserved)
      : this(resource, amount, empire)
    {
      this.Reserved = reserved;
    }

    public Cargo(Resource resource, int amount, int empireId, int reserved)
      : this(resource, amount, empireId)
    {
      this.Reserved = reserved;
    }

    public Cargo(Resource resource, int amount, Empire empire)
      : this(resource, amount, -1)
    {
      if (empire != null)
        this.EmpireId = empire.EmpireId;
      else
        this.EmpireId = -1;
    }

    public Cargo(Resource resource, int amount, int empireId)
    {
      this._Resource = resource;
      this._Component = (Component) null;
      this.CommodityIsResource = true;
      this.CommodityIsComponent = false;
      this.Amount = amount;
      this.EmpireId = empireId;
    }

    public Cargo(Component component, int amount, Empire empire, int reserved)
      : this(component, amount, empire)
    {
      this.Reserved = reserved;
    }

    public Cargo(Component component, int amount, int empireId, int reserved)
      : this(component, amount, empireId)
    {
      this.Reserved = reserved;
    }

    public Cargo(Component component, int amount, Empire empire)
      : this(component, amount, -1)
    {
      if (empire != null)
        this.EmpireId = empire.EmpireId;
      else
        this.EmpireId = -1;
    }

    public Cargo(Component component, int amount, int empireId)
    {
      this._Resource = (Resource) null;
      this._Component = component;
      this.CommodityIsResource = false;
      this.CommodityIsComponent = true;
      this.Amount = amount;
      this.EmpireId = empireId;
    }

    public Component Component => this._Component;

    public Resource Resource => this._Resource;

    public int Available => this.Amount - this.Reserved;

    public Resource CommodityResource
    {
      get => this._Resource;
      set
      {
        this._Resource = value;
        this._Component = (Component) null;
        this.CommodityIsResource = true;
        this.CommodityIsComponent = false;
      }
    }

    public Component CommodityComponent
    {
      get => this._Component;
      set
      {
        this._Resource = (Resource) null;
        this._Component = value;
        this.CommodityIsResource = false;
        this.CommodityIsComponent = true;
      }
    }

    protected Cargo(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this.Amount = binaryReader.ReadInt32();
          this.Reserved = binaryReader.ReadInt32();
          this.CommodityIsComponent = binaryReader.ReadBoolean();
          this.CommodityIsResource = binaryReader.ReadBoolean();
          this.EmpireId = binaryReader.ReadInt32();
          binaryReader.Close();
        }
      }
      this._Resource = (Resource) info.GetValue("Rs", typeof (Resource));
      this._Component = (Component) info.GetValue("Cm", typeof (Component));
    }

    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this.Amount);
          binaryWriter.Write(this.Reserved);
          binaryWriter.Write(this.CommodityIsComponent);
          binaryWriter.Write(this.CommodityIsResource);
          binaryWriter.Write(this.EmpireId);
          binaryWriter.Flush();
          binaryWriter.Close();
          byte[] array = output.ToArray();
          info.AddValue("D", (object) array);
        }
      }
      info.AddValue("Rs", (object) this._Resource);
      info.AddValue("Cm", (object) this._Component);
    }
  }
}
