// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FighterWeaponList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class FighterWeaponList : SyncList<FighterWeapon>, ISerializable
  {
    public FighterWeaponList()
    {
    }

    public FighterWeaponList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num = buffer.Length / 54;
          for (int index = 0; index < num; ++index)
            this.Add(new FighterWeapon()
            {
              Power = binaryReader.ReadSingle(),
              HeadingMissFactor = binaryReader.ReadSingle(),
              LastFired = new DateTime(binaryReader.ReadInt64()),
              DistanceTravelled = binaryReader.ReadSingle(),
              DistanceFromTarget = binaryReader.ReadSingle(),
              WillHitTarget = binaryReader.ReadBoolean(),
              X = binaryReader.ReadSingle(),
              Y = binaryReader.ReadSingle(),
              Heading = binaryReader.ReadSingle(),
              HasMissed = binaryReader.ReadBoolean(),
              SoundEffectPlayed = binaryReader.ReadBoolean(),
              ResetNext = binaryReader.ReadBoolean(),
              Category = (ComponentCategoryType) binaryReader.ReadByte(),
              Type = (ComponentType) binaryReader.ReadByte(),
              RawDamage = binaryReader.ReadInt16(),
              Range = binaryReader.ReadInt16(),
              EnergyRequired = binaryReader.ReadInt16(),
              Speed = binaryReader.ReadInt16(),
              DamageLoss = binaryReader.ReadInt16(),
              FireRate = binaryReader.ReadInt16()
            });
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
          if (this.Count > 0)
          {
            for (int index = 0; index < this.Count; ++index)
            {
              binaryWriter.Write(this[index].Power);
              binaryWriter.Write(this[index].HeadingMissFactor);
              binaryWriter.Write(this[index].LastFired.Ticks);
              binaryWriter.Write(this[index].DistanceTravelled);
              binaryWriter.Write(this[index].DistanceFromTarget);
              binaryWriter.Write(this[index].WillHitTarget);
              binaryWriter.Write(this[index].X);
              binaryWriter.Write(this[index].Y);
              binaryWriter.Write(this[index].Heading);
              binaryWriter.Write(this[index].HasMissed);
              binaryWriter.Write(this[index].SoundEffectPlayed);
              binaryWriter.Write(this[index].ResetNext);
              binaryWriter.Write((byte) this[index].Category);
              binaryWriter.Write((byte) this[index].Type);
              binaryWriter.Write(this[index].RawDamage);
              binaryWriter.Write(this[index].Range);
              binaryWriter.Write(this[index].EnergyRequired);
              binaryWriter.Write(this[index].Speed);
              binaryWriter.Write(this[index].DamageLoss);
              binaryWriter.Write(this[index].FireRate);
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
