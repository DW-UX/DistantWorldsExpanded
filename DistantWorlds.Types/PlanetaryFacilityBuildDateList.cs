// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PlanetaryFacilityBuildDateList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PlanetaryFacilityBuildDateList : List<PlanetaryFacilityBuildDate>
  {
    public bool CheckBuildDate(Habitat colony, int planetaryFacilityId, out long buildDate)
    {
      buildDate = long.MinValue;
      for (int index = 0; index < this.Count; ++index)
      {
        PlanetaryFacilityBuildDate facilityBuildDate = this[index];
        if (facilityBuildDate != null && facilityBuildDate.Colony == colony && facilityBuildDate.FacilityId == planetaryFacilityId)
        {
          buildDate = facilityBuildDate.BuildDate;
          return true;
        }
      }
      return false;
    }

    public void RemoveBuildDate(Habitat colony, int planetaryFacilityId)
    {
      PlanetaryFacilityBuildDateList facilityBuildDateList = new PlanetaryFacilityBuildDateList();
      for (int index = 0; index < this.Count; ++index)
      {
        PlanetaryFacilityBuildDate facilityBuildDate = this[index];
        if (facilityBuildDate != null && facilityBuildDate.Colony == colony && facilityBuildDate.FacilityId == planetaryFacilityId)
          facilityBuildDateList.Add(facilityBuildDate);
      }
      for (int index = 0; index < facilityBuildDateList.Count; ++index)
        this.Remove(facilityBuildDateList[index]);
    }

    public void AddUpdateBuildDate(Habitat colony, int planetaryFacilityId, long buildDate)
    {
      this.RemoveBuildDate(colony, planetaryFacilityId);
      this.Add(new PlanetaryFacilityBuildDate()
      {
        Colony = colony,
        FacilityId = planetaryFacilityId,
        BuildDate = buildDate
      });
    }
  }
}
