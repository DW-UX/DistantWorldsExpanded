// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.HabitatResource
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class HabitatResource : Resource, ISerializable
  {
    public short Abundance;

    public HabitatResource(byte resourceId, int abundance)
      : base(resourceId)
    {
      this.Abundance = (short) abundance;
    }

    public int Extract(double extractionVolume) => Math.Max(1, (int) (extractionVolume / (1000.0 / (double) this.Abundance)));

    protected HabitatResource(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this.ResourceID = binaryReader.ReadByte();
          this.Abundance = binaryReader.ReadInt16();
        }
      }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this.ResourceID);
          binaryWriter.Write(this.Abundance);
          binaryWriter.Flush();
          binaryWriter.Close();
          byte[] array = output.ToArray();
          info.AddValue("D", (object) array);
        }
      }
    }
  }
}
