// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Manufacturer
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Manufacturer : ISerializable
  {
    private Component _Component;
    private float _Progress;
    private int _ManufacturingSpeed;
    private IndustryType _Industry;
    private short _ParentBuiltObjectComponentIndex;
    private short _ParentBuiltObjectComponentId;

    public Manufacturer()
    {
    }

    public Manufacturer(SerializationInfo info, StreamingContext context)
      : this()
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          short componentID = binaryReader.ReadInt16();
          if (componentID >= (short) 0)
            this._Component = new Component((int) componentID);
          this._ParentBuiltObjectComponentIndex = binaryReader.ReadInt16();
          this._ParentBuiltObjectComponentId = binaryReader.ReadInt16();
          this._Industry = (IndustryType) binaryReader.ReadByte();
          this._Progress = binaryReader.ReadSingle();
          this._ManufacturingSpeed = (int) binaryReader.ReadInt16();
          binaryReader.Close();
        }
      }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          if (this._Component != null)
            binaryWriter.Write((short) this._Component.ComponentID);
          else
            binaryWriter.Write((short) -1);
          binaryWriter.Write(this._ParentBuiltObjectComponentIndex);
          binaryWriter.Write(this._ParentBuiltObjectComponentId);
          binaryWriter.Write((byte) this._Industry);
          binaryWriter.Write(this._Progress);
          binaryWriter.Write((short) this._ManufacturingSpeed);
          binaryWriter.Flush();
          binaryWriter.Close();
          info.AddValue("D", (object) output.ToArray());
        }
      }
    }

    public Manufacturer(
      int builtObjectComponentIndex,
      BuiltObjectComponent builtObjectComponent,
      IndustryType industry,
      int manufacturingSpeed)
    {
      this._ParentBuiltObjectComponentIndex = (short) builtObjectComponentIndex;
      this._ParentBuiltObjectComponentId = builtObjectComponent == null ? (short) -1 : (short) builtObjectComponent.ComponentID;
      this._Industry = industry;
      this._ManufacturingSpeed = manufacturingSpeed;
    }

    public int ParentBuiltObjectComponentIndex => (int) this._ParentBuiltObjectComponentIndex;

    public BuiltObjectComponent ParentBuiltObjectComponent => this._ParentBuiltObjectComponentId >= (short) 0 ? new BuiltObjectComponent((int) this._ParentBuiltObjectComponentId, ComponentStatus.Normal) : (BuiltObjectComponent) null;

    public Component Component
    {
      get => this._Component;
      set => this._Component = value;
    }

    public float Progress
    {
      get => this._Progress;
      set => this._Progress = value;
    }

    public int ManufacturingSpeed => this._ManufacturingSpeed;

    public IndustryType Industry => this._Industry;
  }
}
