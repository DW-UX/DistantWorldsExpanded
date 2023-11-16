// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.HotspotList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class HotspotList : List<Hotspot>
  {
    public Hotspot ResolveHotspotAtPoint(int x, int y)
    {
      foreach (Hotspot hotspot in (List<Hotspot>) this)
      {
        if (hotspot.Region.Contains(x, y))
          return hotspot;
      }
      return (Hotspot) null;
    }
  }
}
