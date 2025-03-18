// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FastBitmap
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DistantWorlds.Types
{
    public class FastBitmap : IDisposable
    {
        private Bitmap _Image;
        private int _ImageWidth;
        private BitmapData _BitmapData;
        private unsafe byte* _BaseOffset = (byte*)null;
        private bool _Disposed;

        public FastBitmap(Bitmap image)
        {
            this._Image = image;
            this.LockBitmap();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        protected virtual unsafe void Dispose(bool disposing)
        {
            if (this._Disposed)
                return;
            if (disposing)
                this.UnlockBitmap();
            this._Image = (Bitmap)null;
            this._BitmapData = (BitmapData)null;
            this._BaseOffset = (byte*)null;
            this._Disposed = true;
        }

        ~FastBitmap() => this.Dispose(false);

        public Bitmap Bitmap => this._Image;

        public void Release()
        {
            ReleaseImpl();
        }

        public unsafe void SetPixel(ref int X, ref int Y, Color Colour)
        {
            FastBitmap.PixelData* pixelDataPtr = this.PixelAt(X, Y);
            pixelDataPtr->red = Colour.R;
            pixelDataPtr->green = Colour.G;
            pixelDataPtr->blue = Colour.B;
            pixelDataPtr->alpha = Colour.A;
        }

        public unsafe Color GetPixel(ref int X, ref int Y)
        {
            FastBitmap.PixelData* pixelDataPtr = this.PixelAt(X, Y);
            return Color.FromArgb(pixelDataPtr->alpha, pixelDataPtr->red, pixelDataPtr->green, pixelDataPtr->blue);
        }
        public void Release()
        { ReleaseImpl(); }

        private unsafe void ReleaseImpl()
        {
            _Image.UnlockBits(_BitmapData);
            _BitmapData = null;
            _BaseOffset = null;
        }
        private unsafe void LockBitmap()
        {
            GraphicsUnit pageUnit = GraphicsUnit.Pixel;
            RectangleF bounds = this._Image.GetBounds(ref pageUnit);
            Rectangle rect = new Rectangle((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height);
            this._ImageWidth = (int)bounds.Width * sizeof(FastBitmap.PixelData);
            if (this._ImageWidth % 4 != 0)
                this._ImageWidth = 4 * (this._ImageWidth / 4 + 1);
            this._BitmapData = this._Image.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            this._BaseOffset = (byte*)this._BitmapData.Scan0.ToPointer();
        }

        private unsafe void ReleaseImpl()
        {
            _Image.UnlockBits(_BitmapData);
            _BitmapData = null;
            _BaseOffset = null;
        }
        private unsafe FastBitmap.PixelData* PixelAt(int x, int y)
        {
            //return (FastBitmap.PixelData*)(this._BaseOffset + ((IntPtr)y * this._ImageWidth).ToInt64() + ((IntPtr)x * sizeof(FastBitmap.PixelData)).ToInt64());

            return (PixelData*)(this._BaseOffset + (nint)y * (nint)this._ImageWidth + (nint)x * (nint)sizeof(PixelData));
        }

        private unsafe void UnlockBitmap()
        {
            this._Image.UnlockBits(this._BitmapData);
            this._BitmapData = (BitmapData)null;
            this._BaseOffset = (byte*)null;
        }

        public struct PixelData
        {
            public byte blue;
            public byte green;
            public byte red;
            public byte alpha;
        }
    }
}
