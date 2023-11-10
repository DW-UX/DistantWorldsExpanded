// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CaptionDraggingEventArgs
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;

namespace DistantWorlds.Controls
{
  public class CaptionDraggingEventArgs : EventArgs
  {
    private int width;
    private int height;

    public CaptionDraggingEventArgs()
    {
    }

    public CaptionDraggingEventArgs(int width, int height)
    {
      this.width = width;
      this.height = height;
    }

    public int Width
    {
      get => this.width;
      set => this.width = value;
    }

    public int Height
    {
      get => this.height;
      set => this.height = value;
    }
  }
}
