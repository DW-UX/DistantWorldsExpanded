﻿// Decompiled with JetBrains decompiler
// Type: DistantWorlds.User32
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

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
  }
}
