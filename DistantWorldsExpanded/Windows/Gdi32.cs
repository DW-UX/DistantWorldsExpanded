// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Gdi32
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Runtime.InteropServices;

namespace DistantWorlds
{
  public static class Gdi32
  {
    public const int SRCCOPY = 13369376;

    private const string DllName = "GDI32";

    [DllImport(DllName)]
    public static extern IntPtr AddFontMemResourceEx(
      IntPtr font,
      int size,
      IntPtr res,
      ref int cnt);

    [DllImport(DllName)]
    public static extern bool RemoveFontMemResourceEx(IntPtr hdl);

    [DllImport(DllName)]
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

    [DllImport(DllName)]
    public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);

    [DllImport(DllName)]
    public static extern int CreateCompatibleDC(int hdc);

    [DllImport(DllName)]
    public static extern bool DeleteDC(int hdc);

    [DllImport(DllName)]
    public static extern bool DeleteObject(int hObject);

    [DllImport(DllName)]
    public static extern int GetDeviceCaps(int hdc, int nIndex);

    [DllImport(DllName)]
    public static extern int SelectObject(int hdc, int hgdiobj);

    [DllImport(DllName)]
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

    [DllImport(DllName)]
    public static extern int SetStretchBltMode(int hdc, int stretchMode);

    public static int CreateCompatibleDC() => Gdi32.CreateCompatibleDC((int) new IntPtr(0));
  }
}
