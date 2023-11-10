// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FighterList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class FighterList : SyncList<Fighter>
  {
    public int TotalSize
    {
      get
      {
        int totalSize = 0;
        for (int index = 0; index < this.Count; ++index)
          totalSize += this[index].Size;
        return totalSize;
      }
    }
  }
}
