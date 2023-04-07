// Decompiled with JetBrains decompiler
// Type: DistantWorlds.PlanetaryRingsGenerator
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DistantWorlds
{
    public class PlanetaryRingsGenerator
    {
        private int int_0;

        private Random random_0;

        private Color method_0(int int_1)
        {
            Color result = Color.FromArgb(0, 0, 0);
            switch (int_1)
            {
                case 0:
                    result = Color.FromArgb(150, 135, 127);
                    break;
                case 1:
                    result = Color.FromArgb(144, 110, 125);
                    break;
                case 2:
                    result = Color.FromArgb(96, 64, 32);
                    break;
                case 3:
                    result = Color.FromArgb(112, 108, 128);
                    break;
                case 4:
                    result = Color.FromArgb(128, 84, 32);
                    break;
                case 5:
                    result = Color.FromArgb(96, 76, 104);
                    break;
                case 6:
                    result = Color.FromArgb(76, 96, 128);
                    break;
                case 7:
                    result = Color.FromArgb(120, 105, 97);
                    break;
            }
            return result;
        }

        public Bitmap GenerateRings(int seed, int planetDiameter)
        {
            int_0 = seed;
            random_0 = new Random(int_0);
            Color color_ = method_0(random_0.Next(0, 8));
            List<Color> list = new List<Color>();
            int num = random_0.Next(8, 12);
            for (int i = 0; i < num; i++)
            {
                int num2 = random_0.Next(1, 30);
                if (random_0.Next(0, 5) > 1)
                {
                    num2 *= -1;
                }
                Color item = method_1(color_, num2);
                list.Add(item);
            }
            int num3 = (int)((double)planetDiameter * (1.45 + random_0.NextDouble() * 0.35));
            double num4 = random_0.NextDouble() * (Math.PI / 24.0);
            if (random_0.Next(0, 2) == 1)
            {
                num4 -= Math.PI / 24.0;
            }
            num4 += -Math.PI;
            int int_ = (int)((double)num3 * (0.14 + random_0.NextDouble() * 0.037));
            return method_2(list.ToArray(), num4, num3, int_, -1);
        }

        private Color method_1(Color color_0, int int_1)
        {
            int red = Math.Max(0, Math.Min(255, color_0.R + int_1));
            int green = Math.Max(0, Math.Min(255, color_0.G + int_1));
            int blue = Math.Max(0, Math.Min(255, color_0.B + int_1));
            return Color.FromArgb(red, green, blue);
        }

        private Bitmap method_2(Color[] color_0, double double_0, int int_1, int int_2, int int_3)
        {
            if (int_3 != -1)
            {
                int_0 = int_3;
                random_0 = new Random(int_0);
            }
            int num = (int)((double)int_1 * 0.22);
            Bitmap bitmap = new Bitmap(int_1, int_1, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                SolidBrush brush = new SolidBrush(Color.Transparent);
                graphics.FillRectangle(brush, 0, 0, int_2, num);
                int num2 = random_0.Next(0, color_0.Length);
                int num3 = (int)((double)int_2 * 0.5 + (double)int_2 * random_0.NextDouble() * 0.2);
                Rectangle rect = method_3(int_1, num, int_2, num3);
                Pen pen = new Pen(color_0[num2], num3);
                graphics.DrawEllipse(pen, rect);
                int num4 = random_0.Next(2, 6);
                for (int i = 0; i < num4; i++)
                {
                    num2 = random_0.Next(0, color_0.Length);
                    num3 = (int)((double)int_2 * 0.5 * random_0.NextDouble());
                    rect = method_3(int_1, num, int_2, num3);
                    pen = new Pen(color_0[num2], num3);
                    graphics.DrawEllipse(pen, rect);
                }
                int num5 = random_0.Next(30, 50);
                for (int j = 0; j < num5; j++)
                {
                    num2 = random_0.Next(0, color_0.Length);
                    num3 = 1 + random_0.Next(0, 2);
                    Color color = color_0[num2];
                    rect = method_3(int_1, num, int_2, num3);
                    pen = new Pen(color, num3);
                    graphics.DrawEllipse(pen, rect);
                }
            }
            Bitmap bitmap2 = new Bitmap(int_1, num, PixelFormat.Format32bppPArgb);
            using (Graphics graphics2 = Graphics.FromImage(bitmap2))
            {
                graphics2.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics2.SmoothingMode = SmoothingMode.HighSpeed;
                graphics2.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, num), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
            }
            int num6 = Math.Max(bitmap2.Width, bitmap2.Height);
            Bitmap bitmap3 = new Bitmap(num6, num6, PixelFormat.Format32bppPArgb);
            using (Graphics graphics3 = Graphics.FromImage(bitmap3))
            {
                int x = (num6 - bitmap2.Width) / 2;
                int y = (num6 - bitmap2.Height) / 2;
                graphics3.CompositingQuality = CompositingQuality.HighSpeed;
                graphics3.InterpolationMode = InterpolationMode.Bilinear;
                graphics3.SmoothingMode = SmoothingMode.HighSpeed;
                graphics3.DrawImage(bitmap2, x, y);
            }
            bitmap.Dispose();
            bitmap2.Dispose();
            bitmap3 = method_4(bitmap3, (float)double_0);
            return method_5(bitmap3, 0);
        }

        private Rectangle method_3(int int_1, int int_2, int int_3, int int_4)
        {
            int num = int_1 / 2;
            int num2 = num;
            int num3 = num;
            double num4 = (double)int_3 - (double)int_4;
            int num5 = 2 + int_4 / 2 + (int)(num4 * random_0.NextDouble());
            int num6 = (int)((double)int_1 - (double)num5 * 2.0);
            int num7 = num6;
            int x = num2 - num6 / 2;
            int y = num3 - num7 / 2;
            return new Rectangle(x, y, num6, num7);
        }

        private Bitmap method_4(Image image_0, float float_0)
        {
            if (image_0 == null)
            {
                throw new ArgumentNullException("image");
            }
            float num = image_0.Width;
            float y = image_0.Height;
            float_0 *= -1f;
            float_0 *= 57.29578f;
            float_0 %= 360f;
            if ((double)float_0 < 0.0)
            {
                float_0 += 360f;
            }
            PointF[] array = new PointF[4];
            array[1].X = num;
            array[2].X = num;
            array[2].Y = y;
            array[3].Y = y;
            Matrix matrix = new Matrix();
            matrix.Rotate(float_0);
            matrix.TransformPoints(array);
            double num2 = double.MinValue;
            double num3 = double.MinValue;
            double num4 = double.MaxValue;
            double num5 = double.MaxValue;
            PointF[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                PointF pointF = array2[i];
                num2 = Math.Max(num2, pointF.X);
                num4 = Math.Min(num4, pointF.X);
                num3 = Math.Max(num3, pointF.Y);
                num5 = Math.Min(num5, pointF.Y);
            }
            double num6 = Math.Ceiling(num2 - num4);
            double num7 = Math.Ceiling(num3 - num5);
            Bitmap bitmap = new Bitmap((int)num6, (int)num7);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                PointF point = new PointF((float)(num6 / 2.0), (float)(num7 / 2.0));
                PointF point2 = new PointF(point.X - num / 2f, point.Y - num / 2f);
                matrix.Reset();
                matrix.RotateAt(float_0, point);
                graphics.Transform = matrix;
                graphics.DrawImage(image_0, point2);
            }
            image_0.Dispose();
            return bitmap;
        }

        private Bitmap method_5(Bitmap bitmap_0, int int_1)
        {
            Rectangle rectangle_ = method_7(bitmap_0, int_1);
            Bitmap result = method_6(bitmap_0, rectangle_);
            bitmap_0.Dispose();
            return result;
        }

        private Bitmap method_6(Bitmap bitmap_0, Rectangle rectangle_0)
        {
            Bitmap bitmap = new Bitmap(rectangle_0.Width, rectangle_0.Height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Rectangle srcRect = new Rectangle(rectangle_0.X, rectangle_0.Y, rectangle_0.Width, rectangle_0.Height);
            graphics.DrawImage(bitmap_0, destRect, srcRect, GraphicsUnit.Pixel);
            return bitmap;
        }

        private Rectangle method_7(Bitmap bitmap_0, int int_1)
        {
            return method_8(bitmap_0, int_1, Color.Empty, 0, 4);
        }

        private Rectangle method_8(Bitmap bitmap_0, int int_1, Color color_0, int int_2, int int_3)
        {
            Rectangle rectangle = default(Rectangle);
            FastBitmap fastBitmap = new FastBitmap(bitmap_0);
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            bool flag = false;
            for (int i = 0; i < bitmap_0.Width; i += int_3)
            {
                for (int j = 0; j < bitmap_0.Height; j += int_3)
                {
                    if (!color_0.IsEmpty)
                    {
                        Color pixel = fastBitmap.GetPixel(ref i, ref j);
                        if (pixel.R > color_0.R + int_2 || pixel.G > color_0.G + int_2 || pixel.B > color_0.B + int_2)
                        {
                            num = Math.Max(0, i - int_3);
                            flag = true;
                            break;
                        }
                    }
                    else if (fastBitmap.GetPixel(ref i, ref j).A > int_1)
                    {
                        num = Math.Max(0, i - int_3);
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            flag = false;
            for (int X = bitmap_0.Width - 1; X >= 0; X -= int_3)
            {
                for (int k = 0; k < bitmap_0.Height; k += int_3)
                {
                    if (!color_0.IsEmpty)
                    {
                        Color pixel2 = fastBitmap.GetPixel(ref X, ref k);
                        if (pixel2.R > color_0.R + int_2 || pixel2.G > color_0.G + int_2 || pixel2.B > color_0.B + int_2)
                        {
                            num2 = Math.Min(bitmap_0.Width - 1, X + int_3);
                            flag = true;
                            break;
                        }
                    }
                    else if (fastBitmap.GetPixel(ref X, ref k).A > int_1)
                    {
                        num2 = Math.Min(bitmap_0.Width - 1, X + int_3);
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            flag = false;
            for (int l = 0; l < bitmap_0.Height; l += int_3)
            {
                for (int m = 0; m < bitmap_0.Width; m += int_3)
                {
                    if (!color_0.IsEmpty)
                    {
                        Color pixel3 = fastBitmap.GetPixel(ref m, ref l);
                        if (pixel3.R > color_0.R + int_2 || pixel3.G > color_0.G + int_2 || pixel3.B > color_0.B + int_2)
                        {
                            num3 = Math.Max(0, l - int_3);
                            flag = true;
                            break;
                        }
                    }
                    else if (fastBitmap.GetPixel(ref m, ref l).A > int_1)
                    {
                        num3 = Math.Max(0, l - int_3);
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            flag = false;
            for (int Y = bitmap_0.Height - 1; Y >= 0; Y -= int_3)
            {
                for (int n = 0; n < bitmap_0.Width; n += int_3)
                {
                    if (!color_0.IsEmpty)
                    {
                        Color pixel4 = fastBitmap.GetPixel(ref n, ref Y);
                        if (pixel4.R > color_0.R + int_2 || pixel4.G > color_0.G + int_2 || pixel4.B > color_0.B + int_2)
                        {
                            num4 = Math.Min(bitmap_0.Height - 1, Y + int_3);
                            flag = true;
                            break;
                        }
                    }
                    else if (fastBitmap.GetPixel(ref n, ref Y).A > int_1)
                    {
                        num4 = Math.Min(bitmap_0.Height - 1, Y + int_3);
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            fastBitmap.Release();
            return new Rectangle(num, num3, num2 - num, num4 - num3);
        }

        public PlanetaryRingsGenerator():base()
        {
            Class7.VEFSJNszvZKMZ();
        }
    }
}
