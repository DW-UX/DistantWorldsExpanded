// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxyIndexList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GalaxyIndexList : List<GalaxyIndex>
  {
    public new bool Contains(GalaxyIndex index)
    {
      foreach (GalaxyIndex galaxyIndex in (List<GalaxyIndex>) this)
      {
        if (galaxyIndex.X == index.X && galaxyIndex.Y == index.Y)
          return true;
      }
      return false;
    }

    public new int IndexOf(GalaxyIndex index)
    {
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        if (this[index1].X == index.X && this[index1].Y == index.Y)
          return index1;
      }
      return -1;
    }
  }
}
