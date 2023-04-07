// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Population
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Population : IComparable<Population>, ISerializable
  {
    private Race _Race;
    private long _Amount;
    private long _UnassimilatedAmount;
    private float _GrowthRate;

    public Population()
    {
    }

    public Population(SerializationInfo info, StreamingContext context)
      : this()
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this._Amount = binaryReader.ReadInt64();
          this._UnassimilatedAmount = binaryReader.ReadInt64();
          this._GrowthRate = binaryReader.ReadSingle();
          binaryReader.Close();
        }
      }
      this._Race = (Race) info.GetValue("Ra", typeof (Race));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this._Amount);
          binaryWriter.Write(this._UnassimilatedAmount);
          binaryWriter.Write(this._GrowthRate);
          binaryWriter.Flush();
          binaryWriter.Close();
          info.AddValue("D", (object) output.ToArray());
        }
      }
      info.AddValue("Ra", (object) this._Race);
    }

    public Population(Race race, long amount)
    {
      this._Race = race;
      this._Amount = amount;
      this._GrowthRate = (float) race.ReproductiveRate;
    }

    public Race Race => this._Race;

    public long Amount
    {
      get => this._Amount;
      set => this._Amount = value;
    }

    public long UnassimilatedAmount
    {
      get => this._UnassimilatedAmount;
      set => this._UnassimilatedAmount = value;
    }

    public float GrowthRate
    {
      get => this._GrowthRate;
      set => this._GrowthRate = value;
    }

    int IComparable<Population>.CompareTo(Population other) => this.Amount.CompareTo(other.Amount);
  }
}
