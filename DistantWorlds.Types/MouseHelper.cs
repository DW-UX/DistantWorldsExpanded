// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.MouseHelper
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DistantWorlds.Types
{
  public static class MouseHelper
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetCursorInfo(out MouseHelper.CursorInfo info);

    public static Point GetCursorPosition()
    {
      MouseHelper.CursorInfo info = new MouseHelper.CursorInfo();
      info.Size = Marshal.SizeOf((object) info);
      return MouseHelper.GetCursorInfo(out info) ? info.Point : Point.Empty;
    }

    public struct CursorInfo
    {
      public int Size;
      public int Flags;
      public IntPtr Cursor;
      public Point Point;
    }
  }
}
