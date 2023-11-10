// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxyIndex
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GalaxyIndex : IComparable<GalaxyIndex>
  {
    public int X;
    public int Y;
    public int SortTag;

    public GalaxyIndex(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    int IComparable<GalaxyIndex>.CompareTo(GalaxyIndex other) => this.SortTag.CompareTo(other.SortTag);
  }
}
