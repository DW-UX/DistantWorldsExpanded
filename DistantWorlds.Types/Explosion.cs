// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Explosion
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Explosion : ISerializable
  {
    public DateTime ExplosionStart;
    public bool ExplosionSoundPlayed;
    public short ExplosionSize;
    public short ExplosionOffsetX;
    public short ExplosionOffsetY;
    public float ExplosionProgression;
    public short ExplosionImageIndex;
    public short ExplosionCurrentImage;
    public bool ExplosionWillDestroy;

    public Explosion()
    {
    }

    public Explosion(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this.ExplosionStart = new DateTime(binaryReader.ReadInt64());
          this.ExplosionSoundPlayed = binaryReader.ReadBoolean();
          this.ExplosionSize = binaryReader.ReadInt16();
          this.ExplosionOffsetX = binaryReader.ReadInt16();
          this.ExplosionOffsetY = binaryReader.ReadInt16();
          this.ExplosionProgression = binaryReader.ReadSingle();
          this.ExplosionImageIndex = binaryReader.ReadInt16();
          this.ExplosionCurrentImage = binaryReader.ReadInt16();
          this.ExplosionWillDestroy = binaryReader.ReadBoolean();
        }
      }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this.ExplosionStart.Ticks);
          binaryWriter.Write(this.ExplosionSoundPlayed);
          binaryWriter.Write(this.ExplosionSize);
          binaryWriter.Write(this.ExplosionOffsetX);
          binaryWriter.Write(this.ExplosionOffsetY);
          binaryWriter.Write(this.ExplosionProgression);
          binaryWriter.Write(this.ExplosionImageIndex);
          binaryWriter.Write(this.ExplosionCurrentImage);
          binaryWriter.Write(this.ExplosionWillDestroy);
          binaryWriter.Flush();
          binaryWriter.Close();
          info.AddValue("D", (object) output.ToArray());
        }
      }
    }
  }
}
