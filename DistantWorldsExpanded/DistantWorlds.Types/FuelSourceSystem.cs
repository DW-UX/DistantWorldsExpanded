// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FuelSourceSystem
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class FuelSourceSystem
  {
    public Habitat SystemStar;
    public HabitatList KnownFuelSources = new HabitatList();

    public FuelSourceSystem(Habitat systemStar, HabitatList fuelSources)
    {
      this.SystemStar = systemStar;
      this.KnownFuelSources = fuelSources;
    }
  }
}
