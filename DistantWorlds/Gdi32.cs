// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Gdi32
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Runtime.InteropServices;

namespace DistantWorlds
{
  public class Gdi32
  {
    public const int SRCCOPY = 13369376;

    [DllImport("GDI32.dll")]
    public static extern IntPtr AddFontMemResourceEx(
      IntPtr font,
      int size,
      IntPtr res,
      ref int cnt);

    [DllImport("GDI32.dll")]
    public static extern bool RemoveFontMemResourceEx(IntPtr hdl);

    [DllImport("GDI32.dll")]
    public static extern bool BitBlt(
      int hdcDestination,
      int destinationX,
      int destinationY,
      int width,
      int height,
      int hdcSource,
      int sourceX,
      int sourceY,
      uint rop);

    [DllImport("GDI32.dll")]
    public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);

    [DllImport("GDI32.dll")]
    public static extern int CreateCompatibleDC(int hdc);

    [DllImport("GDI32.dll")]
    public static extern bool DeleteDC(int hdc);

    [DllImport("GDI32.dll")]
    public static extern bool DeleteObject(int hObject);

    [DllImport("GDI32.dll")]
    public static extern int GetDeviceCaps(int hdc, int nIndex);

    [DllImport("GDI32.dll")]
    public static extern int SelectObject(int hdc, int hgdiobj);

    [DllImport("GDI32.dll")]
    public static extern bool StretchBlt(
      int hdcDestination,
      int destinationX,
      int destinationY,
      int destinationWidth,
      int destinationHeight,
      int hdcSource,
      int sourceX,
      int sourceY,
      int sourceWidth,
      int sourceHeight,
      uint rop);

    [DllImport("GDI32.dll")]
    public static extern int SetStretchBltMode(int hdc, int stretchMode);

    public static int CreateCompatibleDC() => Gdi32.CreateCompatibleDC((int) new IntPtr(0));

    public Gdi32()
    {
      Class7.VEFSJNszvZKMZ();
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }
  }
}
