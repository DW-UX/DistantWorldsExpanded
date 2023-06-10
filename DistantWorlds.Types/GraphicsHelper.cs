// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GraphicsHelper
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace DistantWorlds.Types
{
    public static class GraphicsHelper
    {
        enum TernaryRasterOperations : uint
        {
            /// <summary>dest = source</summary>
            SRCCOPY = 0x00CC0020,
            /// <summary>dest = source OR dest</summary>
            SRCPAINT = 0x00EE0086,
            /// <summary>dest = source AND dest</summary>
            SRCAND = 0x008800C6,
            /// <summary>dest = source XOR dest</summary>
            SRCINVERT = 0x00660046,
            /// <summary>dest = source AND (NOT dest)</summary>
            SRCERASE = 0x00440328,
            /// <summary>dest = (NOT source)</summary>
            NOTSRCCOPY = 0x00330008,
            /// <summary>dest = (NOT src) AND (NOT dest)</summary>
            NOTSRCERASE = 0x001100A6,
            /// <summary>dest = (source AND pattern)</summary>
            MERGECOPY = 0x00C000CA,
            /// <summary>dest = (NOT source) OR dest</summary>
            MERGEPAINT = 0x00BB0226,
            /// <summary>dest = pattern</summary>
            PATCOPY = 0x00F00021,
            /// <summary>dest = DPSnoo</summary>
            PATPAINT = 0x00FB0A09,
            /// <summary>dest = pattern XOR dest</summary>
            PATINVERT = 0x005A0049,
            /// <summary>dest = (NOT dest)</summary>
            DSTINVERT = 0x00550009,
            /// <summary>dest = BLACK</summary>
            BLACKNESS = 0x00000042,
            /// <summary>dest = WHITE</summary>
            WHITENESS = 0x00FF0062,
            /// <summary>
            /// Capture window as seen on screen.  This includes layered windows 
            /// such as WPF windows with AllowsTransparency="true"
            /// </summary>
            CAPTUREBLT = 0x40000000
        }
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        public static Bitmap CopyGraphicsContent(Graphics source, Rectangle rect)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);

            using (Graphics dest = Graphics.FromImage(bmp))
            {
                IntPtr hdcSource = source.GetHdc();
                IntPtr hdcDest = dest.GetHdc();

                BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSource, rect.X, rect.Y, TernaryRasterOperations.SRCCOPY);

                source.ReleaseHdc(hdcSource);
                dest.ReleaseHdc(hdcDest);
            }

            return bmp;
        }
        public static bool ContainsColor(List<Color> colors, Color color) => GraphicsHelper.IndexOfColor(colors, color) >= 0;

        public static int IndexOfColor(List<Color> colors, Color color)
        {
            for (int index = 0; index < colors.Count; ++index)
            {
                Color color1 = colors[index];
                if ((int)color1.A == (int)color.A && (int)color1.R == (int)color.R && (int)color1.G == (int)color.G && (int)color1.B == (int)color.B)
                    return index;
            }
            return -1;
        }

        public static Color OscillateColor(Color start, Color end, DateTime time)
        {
            int second = time.Second;
            int millisecond = time.Millisecond;
            if (second % 2 == 1)
                millisecond += 1000;
            double num = millisecond <= 1000 ? (double)Math.Abs(1000 - millisecond) / 1000.0 : (double)(millisecond - 1000) / 1000.0;
            return Color.FromArgb((int)(byte)((uint)start.A - (uint)(byte)((double)((int)start.A - (int)end.A) * num)), (int)(byte)((uint)start.R - (uint)(byte)((double)((int)start.R - (int)end.R) * num)), (int)(byte)((uint)start.G - (uint)(byte)((double)((int)start.G - (int)end.G) * num)), (int)(byte)((uint)start.B - (uint)(byte)((double)((int)start.B - (int)end.B) * num)));
        }

        public static Bitmap[] EnlargeImageArray(Bitmap[] images, int positionsToAdd)
        {
            if (positionsToAdd <= 0)
                return images;
            Bitmap[] destinationArray = new Bitmap[images.Length + positionsToAdd];
            Array.Copy((Array)images, (Array)destinationArray, images.Length);
            return destinationArray;
        }

        public static Bitmap ScaleImageMaximum(Bitmap image, int maxWidth, int maxHeight, float alpha)
        {
            double num = Math.Min((double)maxWidth / (double)image.Width, (double)maxHeight / (double)image.Height);
            int width = (int)((double)image.Width * num);
            int height = (int)((double)image.Height * num);
            return GraphicsHelper.ScaleImage(image, width, height, alpha);
        }

        public static Bitmap ScaleLimitImage(Bitmap image, int maxWidth, int maxHeight, float alpha)
        {
            double num = Math.Min((double)maxWidth / (double)image.Width, (double)maxHeight / (double)image.Height);
            Bitmap bitmap;
            if (num < 1.0)
            {
                int width = (int)((double)image.Width * num);
                int height = (int)((double)image.Height * num);
                bitmap = GraphicsHelper.ScaleImage(image, width, height, alpha);
            }
            else
                bitmap = GraphicsHelper.ScaleImage(image, image.Width, image.Height, alpha);
            return bitmap;
        }

        public static Bitmap[] ScaleImages(Bitmap[] images, int width, int height)
        {
            if (images == null)
                return (Bitmap[])null;
            Bitmap[] bitmapArray = new Bitmap[images.Length];
            for (int index = 0; index < images.Length; ++index)
            {
                Bitmap image = images[index];
                if (image != null)
                {
                    Bitmap bitmap = GraphicsHelper.ScaleImage(image, width, height, 1f);
                    bitmapArray[index] = bitmap;
                }
            }
            return bitmapArray;
        }

        public static Bitmap ScaleImage(Bitmap unscaledBitmap, float scaleFactor, float alpha)
        {
            int width = (int)((double)scaleFactor * (double)unscaledBitmap.Width);
            int height = (int)((double)scaleFactor * (double)unscaledBitmap.Height);
            return GraphicsHelper.ScaleImage(unscaledBitmap, width, height, alpha);
        }

        public static Bitmap ScaleImage(Bitmap unscaledBitmap, int width, int height, float alpha) => GraphicsHelper.ScaleImage(unscaledBitmap, width, height, alpha, false);

        public static Bitmap ScaleImage(
          Bitmap unscaledBitmap,
          int width,
          int height,
          float alpha,
          bool lowQuality)
        {
            if (width < 1)
                width = 1;
            if (height < 1)
                height = 1;
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            using (ImageAttributes attributesTransparency = GraphicsHelper.CalculateImageAttributesTransparency(alpha))
            {
                using (Graphics graphics = Graphics.FromImage((Image)bitmap))
                {
                    if (lowQuality)
                    {
                        graphics.InterpolationMode = InterpolationMode.Bilinear;
                        graphics.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics.SmoothingMode = SmoothingMode.None;
                    }
                    else
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    Rectangle rectangle = new Rectangle(0, 0, unscaledBitmap.Width, unscaledBitmap.Height);
                    Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    graphics.DrawImage((Image)unscaledBitmap, destRect, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, attributesTransparency);
                }
            }
            return bitmap;
        }

        public static Bitmap SmoothImage(Bitmap image)
        {
            Bitmap unscaledBitmap = GraphicsHelper.ScaleImage(image, (int)((double)image.Width * 1.1), (int)((double)image.Height * 1.1), 1f, true);
            Bitmap bitmap = GraphicsHelper.ScaleImage(unscaledBitmap, image.Width, image.Height, 1f, true);
            unscaledBitmap.Dispose();
            image.Dispose();
            return bitmap;
        }

        public static void SetGraphicsQualityToLow(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics.PixelOffsetMode = PixelOffsetMode.None;
        }

        public static void SetGraphicsQualityToHigh(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        public static Bitmap TransparentImage(Bitmap image, float alphaLevel)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                using (ImageAttributes attributesTransparency = GraphicsHelper.CalculateImageAttributesTransparency(alphaLevel))
                {
                    GraphicsHelper.SetGraphicsQualityToHigh(graphics);
                    graphics.DrawImage((Image)image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributesTransparency);
                }
            }
            return bitmap;
        }

        public static ImageAttributes CalculateImageAttributesWithTransparency(
          Color tintColor,
          double alpha)
        {
            float num1 = (float)tintColor.R / (float)byte.MaxValue;
            float num2 = (float)tintColor.G / (float)byte.MaxValue;
            float num3 = (float)tintColor.B / (float)byte.MaxValue;
            ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
            {
        new float[5],
        new float[5],
        new float[5],
        new float[5]{ 0.0f, 0.0f, 0.0f, (float) alpha, 0.0f },
        new float[5]{ num1, num2, num3, 0.0f, 1f }
            });
            ImageAttributes withTransparency = new ImageAttributes();
            withTransparency.SetColorMatrix(newColorMatrix);
            return withTransparency;
        }

        public static ImageAttributes CalculateImageAttributesTransparency(float alphaLevel)
        {
            ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
            {
        new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, alphaLevel, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
            });
            ImageAttributes attributesTransparency = new ImageAttributes();
            attributesTransparency.SetColorMatrix(newColorMatrix);
            return attributesTransparency;
        }

        public static Bitmap CreateBitmapSafely(
          int requestedWidth,
          int requestedHeight,
          PixelFormat pixelFormat)
        {
            Bitmap bitmapSafely = (Bitmap)null;
            int num = 0;
            bool flag = false;
            while (!flag)
            {
                if (num < 10)
                {
                    try
                    {
                        bitmapSafely = new Bitmap(requestedWidth, requestedHeight, pixelFormat);
                        flag = true;
                    }
                    catch
                    {
                        requestedWidth /= 2;
                        requestedHeight /= 2;
                        requestedWidth = Math.Max(1, requestedWidth);
                        requestedHeight = Math.Max(1, requestedHeight);
                    }
                    ++num;
                }
                else
                    break;
            }
            return bitmapSafely;
        }

        public static Bitmap LoadImageFromFilePath(string imagePath)
        {
            Bitmap bitmap1 = (Bitmap)null;
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                Bitmap bitmap2 = new Bitmap(imagePath);
                float horizontalResolution = bitmap2.HorizontalResolution;
                float verticalResolution = bitmap2.VerticalResolution;
                try
                {
                    bitmap2.Save((Stream)memoryStream, ImageFormat.Png);
                }
                finally
                {
                    bitmap2.Dispose();
                }
                bitmap1 = new Bitmap(Image.FromStream((Stream)memoryStream));
                memoryStream.Close();
                bitmap1.SetResolution(horizontalResolution, verticalResolution);
            }
            finally
            {
                memoryStream.Dispose();
            }
            return bitmap1;

            //Bitmap bitmap1 = (Bitmap)null;
            //MemoryStream memoryStream = new MemoryStream();
            //try
            //{
            //    bitmap1 = new Bitmap(imagePath);

            //    float horizontalResolution = bitmap1.HorizontalResolution;
            //    float verticalResolution = bitmap1.VerticalResolution;
            //    if (bitmap1.RawFormat.Guid != System.Drawing.Imaging.ImageFormat.Png.Guid)
            //    {
            //        Bitmap bitmap2 = new Bitmap(bitmap1);
            //        try
            //        {
            //            bitmap2.Save((Stream)memoryStream, ImageFormat.Png);
            //        }
            //        finally
            //        {
            //            bitmap2.Dispose();
            //        }
            //        bitmap1 = new Bitmap(Image.FromStream((Stream)memoryStream));
            //        memoryStream.Close();
            //    }
            //    else
            //    {
            //        bitmap1.MakeTransparent(Color.Transparent);
            //    }
            //    bitmap1.SetResolution(horizontalResolution, verticalResolution);
            //}
            //finally
            //{
            //    memoryStream.Dispose();
            //}
            //return bitmap1;
        }

        public static Point MeasureCenterForImage(Bitmap image, int width, int height)
        {
            Point point = Point.Empty;
            if (image != null)
                point = new Point((width - image.Width) / 2, (height - image.Height) / 2);
            return point;
        }

        public static Bitmap RotateImage(Bitmap image, float angle, GraphicsQuality quality)
        {
            float f = image != null ? (float)image.Width : throw new ArgumentNullException(nameof(image));
            float height1 = (float)image.Height;
            angle *= -1f;
            angle *= 57.29578f;
            angle %= 360f;
            if ((double)angle < 0.0)
                angle += 360f;
            PointF[] pts = new PointF[4];
            pts[1].X = f;
            pts[2].X = f;
            pts[2].Y = height1;
            pts[3].Y = height1;
            Bitmap bitmap = (Bitmap)null;
            using (Matrix matrix = new Matrix())
            {
                matrix.Rotate(angle);
                matrix.TransformPoints(pts);
                double val1_1 = double.MinValue;
                double val1_2 = double.MinValue;
                double val1_3 = double.MaxValue;
                double val1_4 = double.MaxValue;
                foreach (PointF pointF in pts)
                {
                    val1_1 = Math.Max(val1_1, (double)pointF.X);
                    val1_3 = Math.Min(val1_3, (double)pointF.X);
                    val1_2 = Math.Max(val1_2, (double)pointF.Y);
                    val1_4 = Math.Min(val1_4, (double)pointF.Y);
                }
                double num1 = Math.Ceiling(val1_1 - val1_3);
                double num2 = Math.Ceiling(val1_2 - val1_4);
                if (double.IsNaN(num1))
                    num1 = !float.IsNaN(f) ? Math.Max(1.0, (double)image.Width) : 50.0;
                if (double.IsNaN(num2))
                    num2 = !float.IsNaN(height1) ? Math.Max(1.0, (double)image.Height) : 50.0;
                double width = Math.Max(1.0, num1);
                double height2 = Math.Max(1.0, num2);
                bitmap = new Bitmap((int)width, (int)height2, PixelFormat.Format32bppPArgb);
                bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (Graphics graphics = Graphics.FromImage((Image)bitmap))
                {
                    GraphicsHelper.SetGraphicsQuality(graphics, quality);
                    PointF point1 = new PointF((float)(width / 2.0), (float)(height2 / 2.0));
                    PointF point2 = new PointF(point1.X - f / 2f, point1.Y - height1 / 2f);
                    matrix.Reset();
                    matrix.RotateAt(angle, point1);
                    graphics.Transform = matrix;
                    graphics.DrawImage((Image)image, point2);
                }
            }
            return bitmap;
        }

        public static void SetGraphicsQuality(Graphics graphics, GraphicsQuality quality)
        {
            switch (quality)
            {
                case GraphicsQuality.Undefined:
                case GraphicsQuality.Medium:
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.Bilinear;
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    break;
                case GraphicsQuality.Low:
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.SmoothingMode = SmoothingMode.None;
                    break;
                case GraphicsQuality.High:
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    break;
            }
        }

        public static void DisposeImageArray(Bitmap[] images)
        {
            if (images == null || images.Length <= 0)
                return;
            for (int index = 0; index < images.Length; ++index)
            {
                Bitmap image = images[index];
                if (image != null && image.PixelFormat != PixelFormat.Undefined)
                    image.Dispose();
                images[index] = (Bitmap)null;
            }
        }

        public static void DrawStringWithDropShadowCentered(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          SizeF maxSize)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170)))
                GraphicsHelper.DrawStringWithDropShadowCentered(graphics, text, font, location, brush, maxSize);
        }

        public static void DrawStringWithDropShadowCentered(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          SolidBrush brush,
          SizeF maxSize)
        {
            if (maxSize.IsEmpty)
            {
                int width = 300;
                SizeF sizeF = graphics.MeasureString(text, font, width);
                int x = location.X - (int)((double)sizeF.Width / 2.0);
                GraphicsHelper.DrawStringWithDropShadow(graphics, text, font, new Point(x, location.Y), brush);
            }
            else
            {
                SizeF sizeF = graphics.MeasureString(text, font, maxSize);
                int x = location.X - (int)((double)sizeF.Width / 2.0);
                GraphicsHelper.DrawStringWithDropShadow(graphics, text, font, new Point(x, location.Y), (Brush)brush, maxSize);
            }
        }

        public static int DrawStringWithDropShadowCentered(
          Graphics graphics,
          string text,
          Font font,
          Rectangle textArea)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170)))
                return GraphicsHelper.DrawStringWithDropShadowCentered(graphics, text, font, brush, textArea);
        }

        public static int DrawStringWithDropShadowCentered(
          Graphics graphics,
          string text,
          Font font,
          SolidBrush brush,
          Rectangle textArea)
        {
            using (SolidBrush dropShadowBrush = new SolidBrush(Color.Black))
                return GraphicsHelper.DrawStringWithDropShadowCentered(graphics, text, font, brush, dropShadowBrush, textArea);
        }

        public static int DrawStringWithDropShadowCentered(
          Graphics graphics,
          string text,
          Font font,
          SolidBrush brush,
          SolidBrush dropShadowBrush,
          Rectangle textArea)
        {
            SizeF sizeF = graphics.MeasureString(text, font, textArea.Width);
            int x = textArea.X + textArea.Width / 2 - (int)((double)sizeF.Width / 2.0);
            GraphicsHelper.DrawStringWithDropShadow(graphics, text, font, new Point(x, textArea.Y), (Brush)brush, (Brush)dropShadowBrush, new SizeF(sizeF.Width + 2f, sizeF.Height + 2f));
            return (int)sizeF.Height + 1;
        }

        public static void DrawStringWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170)))
                GraphicsHelper.DrawStringWithDropShadow(graphics, text, font, location, brush);
        }

        public static void DrawStringWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          SolidBrush brush)
        {
            location = new Point(location.X + 1, location.Y + 1);
            using (SolidBrush solidBrush = new SolidBrush(Color.Black))
                graphics.DrawString(text, font, (Brush)solidBrush, (PointF)location, StringFormat.GenericTypographic);
            location = new Point(location.X - 1, location.Y - 1);
            graphics.DrawString(text, font, (Brush)brush, (PointF)location, StringFormat.GenericTypographic);
        }

        public static void DrawStringWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          Brush brush,
          SizeF maxSize)
        {
            using (SolidBrush dropShadowBrush = new SolidBrush(Color.Black))
                GraphicsHelper.DrawStringWithDropShadow(graphics, text, font, location, brush, (Brush)dropShadowBrush, maxSize);
        }

        public static void DrawStringWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          Brush brush,
          Brush dropShadowBrush,
          SizeF maxSize)
        {
            if (maxSize != SizeF.Empty)
            {
                bool flag = false;
                location = new Point(location.X + 1, location.Y + 1);
                RectangleF layoutRectangle = new RectangleF((float)location.X, (float)location.Y, maxSize.Width, maxSize.Height);
                StringFormat genericTypographic = StringFormat.GenericTypographic;
                //genericTypographic.Trimming = StringTrimming.None;
                //var test2 = new Region();
                //test2.MakeInfinite();
                //graphics.Clip = test2;
                graphics.DrawString(text, font, dropShadowBrush, layoutRectangle, genericTypographic);
                if (flag)
                {
                    Rectangle rect = new Rectangle((int)layoutRectangle.X, (int)layoutRectangle.Y, (int)layoutRectangle.Width, (int)layoutRectangle.Height);
                    var bitmp = CopyGraphicsContent(graphics, rect);
                    bitmp.Save(@"H:\test1.bmp", ImageFormat.Bmp);
                }
                location = new Point(location.X - 1, location.Y - 1);
                layoutRectangle = new RectangleF((float)location.X, (float)location.Y, maxSize.Width, maxSize.Height);
                graphics.DrawString(text, font, brush, layoutRectangle, genericTypographic);

                if (flag)
                {
                    Rectangle rect = new Rectangle((int)layoutRectangle.X, (int)layoutRectangle.Y, (int)layoutRectangle.Width , (int)layoutRectangle.Height);
                    var bitmp = CopyGraphicsContent(graphics, rect);
                    bitmp.Save(@"H:\test2.bmp", ImageFormat.Bmp);
                }
            }
            else
            {
                location = new Point(location.X + 1, location.Y + 1);
                graphics.DrawString(text, font, dropShadowBrush, (PointF)location);
                location = new Point(location.X - 1, location.Y - 1);
                graphics.DrawString(text, font, brush, (PointF)location);
            }
        }
    }
}
