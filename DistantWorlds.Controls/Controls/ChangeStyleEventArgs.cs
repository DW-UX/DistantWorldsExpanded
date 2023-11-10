// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ChangeStyleEventArgs
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;

namespace DistantWorlds.Controls
{
  public class ChangeStyleEventArgs : EventArgs
  {
    private DirectionStyle oldStyle;
    private DirectionStyle newStyle;

    public ChangeStyleEventArgs()
    {
    }

    public ChangeStyleEventArgs(DirectionStyle oldStyle, DirectionStyle newStyle)
    {
      this.oldStyle = oldStyle;
      this.newStyle = newStyle;
    }

    public DirectionStyle Old
    {
      get => this.oldStyle;
      set => this.oldStyle = value;
    }

    public DirectionStyle New
    {
      get => this.newStyle;
      set => this.newStyle = value;
    }
  }
}
