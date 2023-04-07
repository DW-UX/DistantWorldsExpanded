// Decompiled with JetBrains decompiler
// Type: DistantWorlds.FastBitmap
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DistantWorlds
{
    public class FastBitmap
    {
        public struct PixelData
        {
            public byte blue;

            public byte green;

            public byte red;

            public byte alpha;
        }

        private Bitmap bitmap_0;

        private int int_0;

        private BitmapData bitmapData_0;

        private unsafe byte* pByte_0;

        public Bitmap Bitmap => bitmap_0;

        public unsafe FastBitmap(Bitmap image):base()
        {
            Class7.VEFSJNszvZKMZ();
            pByte_0 = null;
            bitmap_0 = image;
            try
            {
                method_0();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Release()
        {
            try
            {
                method_2();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public unsafe void SetPixel(ref int X, ref int Y, Color Colour)
        {
            try
            {
                PixelData* ptr = method_1(X, Y);
                ptr->red = Colour.R;
                ptr->green = Colour.G;
                ptr->blue = Colour.B;
                ptr->alpha = Colour.A;
            }
            catch (AccessViolationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public unsafe Color GetPixel(ref int X, ref int Y)
        {
            try
            {
                PixelData* ptr = method_1(X, Y);
                return Color.FromArgb(ptr->alpha, ptr->red, ptr->green, ptr->blue);
            }
            catch (AccessViolationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private unsafe void method_0()
        {
            GraphicsUnit pageUnit = GraphicsUnit.Pixel;
            RectangleF bounds = bitmap_0.GetBounds(ref pageUnit);
            Rectangle rect = new Rectangle((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height);
            int_0 = (int)bounds.Width * sizeof(PixelData);
            if (int_0 % 4 != 0)
            {
                int_0 = 4 * (int_0 / 4 + 1);
            }
            bitmapData_0 = bitmap_0.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            pByte_0 = (byte*)bitmapData_0.Scan0.ToPointer();
        }

        private unsafe PixelData* method_1(int int_1, int int_2)
        {
            return (PixelData*)(pByte_0 + (nint)int_2 * (nint)int_0 + (nint)int_1 * (nint)sizeof(PixelData));
        }

        private unsafe void method_2()
        {
            bitmap_0.UnlockBits(bitmapData_0);
            bitmapData_0 = null;
            pByte_0 = null;
        }
    }
}
