// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FastBitmap
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace DistantWorlds {

  public class FastBitmap : IDisposable {

    private readonly Bitmap _Image;

    private readonly int _ImageWidth;

    private readonly BitmapData _BitmapData;

    private readonly unsafe byte* _BaseOffset;

    [MethodImpl(MethodImplOptions.AggressiveInlining), SkipLocalsInit]
    public unsafe FastBitmap(Bitmap image) {
      _Image = image;
      GraphicsUnit pageUnit = GraphicsUnit.Pixel;
      RectangleF bounds = _Image.GetBounds(ref pageUnit);
      Rectangle rect = new Rectangle(
        (int)bounds.X,
        (int)bounds.Y,
        (int)bounds.Width,
        (int)bounds.Height
        );
      Debug.Assert(rect.X == 0);
      _ImageWidth = (int)bounds.Width * sizeof(PixelData);
      if (_ImageWidth % 4 != 0)
        _ImageWidth = 4 * (_ImageWidth / 4 + 1);
      _BitmapData = _Image.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      _BaseOffset = (byte*)_BitmapData.Scan0;
    }

    public void Dispose() {
      _Image.UnlockBits(_BitmapData);
      GC.SuppressFinalize(this);
    }

    ~FastBitmap() => Dispose();

    public Bitmap Bitmap => _Image;

    [MethodImpl(MethodImplOptions.AggressiveInlining), SkipLocalsInit]
    public unsafe void SetPixel(ref int x, ref int y, Color colour)
      => *PixelAt(x, y) = new() {
        alpha = colour.A,
        blue = colour.B,
        green = colour.G,
        red = colour.R
      };

    [MethodImpl(MethodImplOptions.AggressiveInlining), SkipLocalsInit]
    public unsafe Color GetPixel(int x, int y) {
      PixelData* pixelDataPtr = PixelAt(x, y);
      return Color.FromArgb(
        pixelDataPtr->alpha,
        pixelDataPtr->red,
        pixelDataPtr->green,
        pixelDataPtr->blue
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), SkipLocalsInit]
    private unsafe PixelData* PixelAt(int x, int y)
      => (PixelData*)(
        _BaseOffset + y * (nint)_ImageWidth
        + x * (nint)sizeof(PixelData)
      );

    public struct PixelData {

      public byte blue;

      public byte green;

      public byte red;

      public byte alpha;

    }

  }

}