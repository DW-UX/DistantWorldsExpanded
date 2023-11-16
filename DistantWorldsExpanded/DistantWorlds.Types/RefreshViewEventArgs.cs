// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RefreshViewEventArgs
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RefreshViewEventArgs : EventArgs
  {
    private double _Xpos;
    private double _Ypos;
    private HabitatList _NewHabitats = new HabitatList();
    [OptionalField]
    public bool OnlyGalaxyBackdrops;

    public RefreshViewEventArgs(double x, double y, HabitatList newHabitats)
      : this(x, y, newHabitats, false)
    {
    }

    public RefreshViewEventArgs(
      double x,
      double y,
      HabitatList newHabitats,
      bool onlyGalaxyBackdrops)
    {
      this._Xpos = x;
      this._Ypos = y;
      this._NewHabitats = newHabitats;
      this.OnlyGalaxyBackdrops = onlyGalaxyBackdrops;
    }

    public double Xpos
    {
      get => this._Xpos;
      set => this._Xpos = value;
    }

    public double Ypos
    {
      get => this._Ypos;
      set => this._Ypos = value;
    }

    public HabitatList NewHabitats
    {
      get => this._NewHabitats;
      set => this._NewHabitats = value;
    }
  }
}
