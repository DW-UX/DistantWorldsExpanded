// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourcePrevalence
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourcePrevalence
  {
    public bool HabitatIsAsteroid;
    public bool HabitatIsGasCloud;
    public HabitatType HabitatType;
    public float Prevalence;
    public float AbundanceMinimum;
    public float AbundanceMaximum = 1f;

    public ResourcePrevalence(
      bool isAsteroid,
      bool isGasCloud,
      HabitatType type,
      float prevalence,
      float abundanceMinimum,
      float abundanceMaximum)
    {
      this.HabitatIsAsteroid = isAsteroid;
      this.HabitatIsGasCloud = isGasCloud;
      this.HabitatType = type;
      this.Prevalence = prevalence;
      this.AbundanceMinimum = abundanceMinimum;
      this.AbundanceMaximum = abundanceMaximum;
    }
  }
}
