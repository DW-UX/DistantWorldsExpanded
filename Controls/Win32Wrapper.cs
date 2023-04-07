// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.Win32Wrapper
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Runtime.InteropServices;

namespace DistantWorlds.Controls
{
  internal class Win32Wrapper
  {
    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(
      IntPtr hWnd,
      IntPtr hWndInsertAfter,
      int X,
      int Y,
      int cx,
      int cy,
      Win32Wrapper.FlagsSetWindowPos uFlags);

    public enum FlagsSetWindowPos
    {
      SWP_NOSIZE = 1,
      SWP_NOMOVE = 2,
      SWP_NOZORDER = 4,
      SWP_NOREDRAW = 8,
      SWP_NOACTIVATE = 16, // 0x00000010
      SWP_FRAMECHANGED = 32, // 0x00000020
      SWP_SHOWWINDOW = 64, // 0x00000040
      SWP_HIDEWINDOW = 128, // 0x00000080
      SWP_NOCOPYBITS = 256, // 0x00000100
      SWP_NOOWNERZORDER = 512, // 0x00000200
      SWP_NOSENDCHANGING = 1024, // 0x00000400
    }
  }
}
