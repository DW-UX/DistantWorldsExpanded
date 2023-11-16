// Decompiled with JetBrains decompiler
// Type: CryptoRandom
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Security.Cryptography;

public class CryptoRandom : RandomNumberGenerator
{
  private static RandomNumberGenerator _Rnd;

  public CryptoRandom() => CryptoRandom._Rnd = RandomNumberGenerator.Create();

  public override void GetBytes(byte[] buffer) => CryptoRandom._Rnd.GetBytes(buffer);

  public override void GetNonZeroBytes(byte[] data) => CryptoRandom._Rnd.GetNonZeroBytes(data);

  public double NextDouble()
  {
    byte[] data = new byte[4];
    CryptoRandom._Rnd.GetBytes(data);
    return (double) BitConverter.ToUInt32(data, 0) / (double) uint.MaxValue;
  }

  public int Next(int minValue, int maxValue)
  {
    long num = (long) maxValue - (long) minValue;
    return (int) ((long) Math.Floor(this.NextDouble() * (double) num) + (long) minValue);
  }

  public int Next() => this.Next(0, int.MaxValue);

  public int Next(int maxValue) => this.Next(0, maxValue);
}
