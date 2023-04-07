// Decompiled with JetBrains decompiler
// Type: DistantWorlds.User32
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Runtime.InteropServices;

namespace DistantWorlds
{
  public class User32
  {
    public const int SM_CXSCREEN = 0;
    public const int SM_CYSCREEN = 1;

    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr ptr);

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int abc);

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(int ptr);

    [DllImport("user32.dll")]
    public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

    public User32()
    {
      Class7.VEFSJNszvZKMZ();
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }
  }
}
