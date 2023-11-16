// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectComponent
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class BuiltObjectComponent : Component, ISerializable
  {
    public ComponentStatus Status;
    public short BuiltObjectComponentId = -1;

    public BuiltObjectComponent(int componentID, ComponentStatus componentStatus)
      : base(componentID)
    {
      this.Status = componentStatus;
    }

    protected BuiltObjectComponent(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this.ComponentID = (int) binaryReader.ReadByte();
          this.Status = (ComponentStatus) binaryReader.ReadByte();
          this.BuiltObjectComponentId = binaryReader.ReadInt16();
          binaryReader.Close();
        }
      }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write((byte) this.ComponentID);
          binaryWriter.Write((byte) this.Status);
          binaryWriter.Write(this.BuiltObjectComponentId);
          binaryWriter.Flush();
          binaryWriter.Close();
          byte[] array = output.ToArray();
          info.AddValue("D", (object) array);
        }
      }
    }

    public void Damage() => this.Status = ComponentStatus.Damaged;

    public void Repair() => this.Status = ComponentStatus.Normal;
  }
}
