// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SectorList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SectorList : SyncList<Sector>
  {
    public new bool Contains(Sector sector)
    {
      foreach (Sector sector1 in (SyncList<Sector>) this)
      {
        if (sector1.X == sector.X && sector1.Y == sector.Y)
          return true;
      }
      return false;
    }
  }
}
