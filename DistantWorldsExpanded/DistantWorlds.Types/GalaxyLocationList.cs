// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxyLocationList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GalaxyLocationList : SyncList<GalaxyLocation>
  {
    public GalaxyLocationList ResolveSpecialLocations() => this.FindLocations(new List<GalaxyLocationType>()
    {
      GalaxyLocationType.DebrisField,
      GalaxyLocationType.PlanetDestroyer,
      GalaxyLocationType.RestrictedArea
    });

    public GalaxyLocationList FindLocations(List<GalaxyLocationType> locationTypes)
    {
      GalaxyLocationList locations = new GalaxyLocationList();
      foreach (GalaxyLocation galaxyLocation in (SyncList<GalaxyLocation>) this)
      {
        if (locationTypes.Contains(galaxyLocation.Type))
          locations.Add(galaxyLocation);
      }
      return locations;
    }

    public GalaxyLocationList FindLocations(GalaxyLocationType locationType)
    {
      GalaxyLocationList locations = new GalaxyLocationList();
      foreach (GalaxyLocation galaxyLocation in (SyncList<GalaxyLocation>) this)
      {
        if (galaxyLocation.Type == locationType)
          locations.Add(galaxyLocation);
      }
      return locations;
    }
  }
}
