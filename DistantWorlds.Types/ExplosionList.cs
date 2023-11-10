// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ExplosionList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ExplosionList : SyncList<Explosion>, ISerializable
  {
    public ExplosionList()
    {
    }

    public ExplosionList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num = buffer.Length / 24;
          for (int index = 0; index < num; ++index)
            this.Add(new Explosion()
            {
              ExplosionStart = new DateTime(binaryReader.ReadInt64()),
              ExplosionSoundPlayed = binaryReader.ReadBoolean(),
              ExplosionSize = binaryReader.ReadInt16(),
              ExplosionOffsetX = binaryReader.ReadInt16(),
              ExplosionOffsetY = binaryReader.ReadInt16(),
              ExplosionProgression = binaryReader.ReadSingle(),
              ExplosionImageIndex = binaryReader.ReadInt16(),
              ExplosionCurrentImage = binaryReader.ReadInt16(),
              ExplosionWillDestroy = binaryReader.ReadBoolean()
            });
        }
      }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          if (this.Count > 0)
          {
            for (int index = 0; index < this.Count; ++index)
            {
              binaryWriter.Write(this[index].ExplosionStart.Ticks);
              binaryWriter.Write(this[index].ExplosionSoundPlayed);
              binaryWriter.Write(this[index].ExplosionSize);
              binaryWriter.Write(this[index].ExplosionOffsetX);
              binaryWriter.Write(this[index].ExplosionOffsetY);
              binaryWriter.Write(this[index].ExplosionProgression);
              binaryWriter.Write(this[index].ExplosionImageIndex);
              binaryWriter.Write(this[index].ExplosionCurrentImage);
              binaryWriter.Write(this[index].ExplosionWillDestroy);
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            info.AddValue("D", (object) output.ToArray());
          }
          else
            info.AddValue("D", (object) new byte[0]);
        }
      }
    }
  }
}
