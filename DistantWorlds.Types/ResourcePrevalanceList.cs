// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourcePrevalanceList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourcePrevalanceList : List<ResourcePrevalence>
  {
    public ResourcePrevalanceList GetByPlanetOrMoonType(HabitatType type)
    {
      ResourcePrevalanceList planetOrMoonType = new ResourcePrevalanceList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResourcePrevalence resourcePrevalence = this[index];
        if (resourcePrevalence != null && resourcePrevalence.HabitatType == type && !resourcePrevalence.HabitatIsAsteroid && !resourcePrevalence.HabitatIsGasCloud)
          planetOrMoonType.Add(resourcePrevalence);
      }
      return planetOrMoonType;
    }

    public ResourcePrevalanceList GetByAsteroidType(HabitatType type)
    {
      ResourcePrevalanceList byAsteroidType = new ResourcePrevalanceList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResourcePrevalence resourcePrevalence = this[index];
        if (resourcePrevalence != null && resourcePrevalence.HabitatType == type && resourcePrevalence.HabitatIsAsteroid)
          byAsteroidType.Add(resourcePrevalence);
      }
      return byAsteroidType;
    }

    public ResourcePrevalanceList GetByGasCloudType(HabitatType type)
    {
      ResourcePrevalanceList byGasCloudType = new ResourcePrevalanceList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResourcePrevalence resourcePrevalence = this[index];
        if (resourcePrevalence != null && resourcePrevalence.HabitatType == type && resourcePrevalence.HabitatIsGasCloud)
          byGasCloudType.Add(resourcePrevalence);
      }
      return byGasCloudType;
    }
  }
}
