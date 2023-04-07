// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ConstructionYard
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ConstructionYard : ISerializable
  {
    private BuiltObject _ShipUnderConstruction;
    private int _ConstructionSpeed;
    private int _MaximumShipSize;
    private float _IncrementalProgress;
    private short _ComponentId;
    private short _BuiltObjectComponentId;
    private ComponentList _RetrofitComponentsToBeBuilt;
    private ComponentList _RetrofitComponentsToBeScrapped;
    private float _BuildSpeedModifier = 1f;

    public ConstructionYard()
    {
    }

    public ConstructionYard(SerializationInfo info, StreamingContext context)
      : this()
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this._ConstructionSpeed = binaryReader.ReadInt32();
          this._MaximumShipSize = binaryReader.ReadInt32();
          this._IncrementalProgress = binaryReader.ReadSingle();
          this._BuildSpeedModifier = binaryReader.ReadSingle();
          this._ComponentId = binaryReader.ReadInt16();
          this._BuiltObjectComponentId = binaryReader.ReadInt16();
          int num1 = binaryReader.ReadInt32();
          ComponentList componentList1 = new ComponentList();
          for (int index = 0; index < num1; ++index)
            componentList1.Add(new Component((int) binaryReader.ReadByte()));
          int num2 = binaryReader.ReadInt32();
          ComponentList componentList2 = new ComponentList();
          for (int index = 0; index < num2; ++index)
            componentList2.Add(new Component((int) binaryReader.ReadByte()));
          this.RetrofitComponentsToBeBuilt = componentList1;
          this.RetrofitComponentsToBeScrapped = componentList2;
          binaryReader.Close();
        }
      }
      this._ShipUnderConstruction = (BuiltObject) info.GetValue("BOUC", typeof (BuiltObject));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this._ConstructionSpeed);
          binaryWriter.Write(this._MaximumShipSize);
          binaryWriter.Write(this._IncrementalProgress);
          binaryWriter.Write(this._BuildSpeedModifier);
          binaryWriter.Write(this._ComponentId);
          binaryWriter.Write(this._BuiltObjectComponentId);
          if (this.RetrofitComponentsToBeBuilt != null)
          {
            binaryWriter.Write(this.RetrofitComponentsToBeBuilt.Count);
            for (int index = 0; index < this.RetrofitComponentsToBeBuilt.Count; ++index)
              binaryWriter.Write((byte) this.RetrofitComponentsToBeBuilt[index].ComponentID);
          }
          else
            binaryWriter.Write(0);
          if (this.RetrofitComponentsToBeScrapped != null)
          {
            binaryWriter.Write(this.RetrofitComponentsToBeScrapped.Count);
            for (int index = 0; index < this.RetrofitComponentsToBeScrapped.Count; ++index)
              binaryWriter.Write((byte) this.RetrofitComponentsToBeScrapped[index].ComponentID);
          }
          else
            binaryWriter.Write(0);
          binaryWriter.Flush();
          binaryWriter.Close();
          info.AddValue("D", (object) output.ToArray());
        }
      }
      info.AddValue("BOUC", (object) this._ShipUnderConstruction);
    }

    public ConstructionYard(
      int componentId,
      short builtObjectComponentId,
      int maximumShipSize,
      int constructionSpeed)
    {
      this._ComponentId = (short) componentId;
      this._BuiltObjectComponentId = builtObjectComponentId;
      this._MaximumShipSize = maximumShipSize;
      this._ConstructionSpeed = constructionSpeed;
    }

    public ComponentList RetrofitComponentsToBeBuilt
    {
      get => this._RetrofitComponentsToBeBuilt;
      set => this._RetrofitComponentsToBeBuilt = value;
    }

    public ComponentList RetrofitComponentsToBeScrapped
    {
      get => this._RetrofitComponentsToBeScrapped;
      set => this._RetrofitComponentsToBeScrapped = value;
    }

    public short ComponentId => this._ComponentId;

    public short BuiltObjectComponentId => this._BuiltObjectComponentId;

    public BuiltObject ShipUnderConstruction
    {
      get => this._ShipUnderConstruction;
      set => this._ShipUnderConstruction = value;
    }

    public float IncrementalProgress
    {
      get => this._IncrementalProgress;
      set => this._IncrementalProgress = value;
    }

    public float BuildSpeedModifier
    {
      get => this._BuildSpeedModifier;
      set => this._BuildSpeedModifier = value;
    }

    public int ConstructionSpeed
    {
      get => this._ConstructionSpeed;
      set => this._ConstructionSpeed = value;
    }

    public int MaximumShipSize => this._MaximumShipSize;
  }
}
