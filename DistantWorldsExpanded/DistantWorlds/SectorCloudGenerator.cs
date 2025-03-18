// Decompiled with JetBrains decompiler
// Type: DistantWorlds.SectorCloudGenerator
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DistantWorlds
{
    public class SectorCloudGenerator
    {
        //private int int_0;

        private Random random_0;

        //private int DilRgjtCoOD;

        private FbmNoise fbmNoise_0;

        private byte[][] byte_0;

        //private int int_1;

        private FbmNoise.CFractal cfractal_0;

        private PerlinNoise perlinNoise_0;

        private Bitmap bitmap_0;

        public Bitmap CloudImage
        {
            get
            {
                return bitmap_0;
            }
            set
            {
                bitmap_0 = value;
            }
        }

        public byte[][] Noise => byte_0;

        public SectorCloudGenerator(int randomSeed, int cloudSize):base()
        {
            
            //DilRgjtCoOD = 96;
            //int_0 = randomSeed;
            //random_0 = new Random(int_0);
            random_0 = new Random(randomSeed);
            //int_1 = cloudSize;
            fbmNoise_0 = new FbmNoise(random_0.Next(0, 1000000));
            cfractal_0 = new FbmNoise.CFractal(randomSeed, 0.35f, 2.6f);
            byte_0 = fbmNoise_0.MakeNoise(cloudSize, 0.35, 2.6);
            perlinNoise_0 = new PerlinNoise(randomSeed);
            bitmap_0 = new Bitmap(cloudSize, cloudSize, PixelFormat.Format32bppPArgb);
        }

        public void ApplyNoiseToImage(Bitmap image, Rectangle imageRectangle, byte[][] noise)
        {
            ApplyNoiseToImage(image, imageRectangle, noise, 0, 0);
        }

        public void ApplyNoiseToImage(Bitmap image, Rectangle imageRectangle, byte[][] noise, int noiseOffsetX, int noiseOffsetY)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            int top = imageRectangle.Top;
            int bottom = imageRectangle.Bottom;
            int left = imageRectangle.Left;
            int right = imageRectangle.Right;
            for (int i = top; i < bottom; i++)
            {
                for (int j = left; j < right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0)
                    {
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(noise[j + noiseOffsetX][i + noiseOffsetY], pixel.R, pixel.G, pixel.B));
                    }
                }
            }
            fastBitmap.Release();
        }

        public void PerlinImageTransparent(Bitmap image, Rectangle imageRectangle, float skip, float startX, float startY)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            double num = startX;
            double num2 = startY;
            double num3 = skip;
            int top = imageRectangle.Top;
            int bottom = imageRectangle.Bottom;
            int left = imageRectangle.Left;
            int right = imageRectangle.Right;
            for (int i = top; i < bottom; i++)
            {
                num = startX;
                for (int j = left; j < right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0)
                    {
                        double num4 = perlinNoise_0.PerlinNoise2d(num, num2);
                        double num5 = (num4 + 1.0) / 2.0;
                        int alpha = (int)((double)(int)pixel.A * num5);
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(alpha, pixel.R, pixel.G, pixel.B));
                    }
                    num += num3;
                }
                num2 += num3;
            }
            fastBitmap.Release();
        }

        public void FbmImageTransparent(Bitmap image, Rectangle imageRectangle, float skip, float startX, float startY)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            float num = startX;
            float y = startY;
            int top = imageRectangle.Top;
            int bottom = imageRectangle.Bottom;
            int left = imageRectangle.Left;
            int right = imageRectangle.Right;
            for (int i = top; i < bottom; i++)
            {
                num = startX;
                for (int j = left; j < right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0)
                    {
                        float num2 = cfractal_0.fBm(ref num, ref y, 6);
                        float num3 = (num2 + 1f) / 2f;
                        int alpha = (int)((float)(int)pixel.A * num3);
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(alpha, pixel.R, pixel.G, pixel.B));
                    }
                    num += skip;
                }
                y += skip;
            }
            fastBitmap.Release();
        }

        public void FbmImage(Bitmap image, Rectangle imageRectangle, float skip, float startX, float startY)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            float num = startX;
            float y = startY;
            int top = imageRectangle.Top;
            int bottom = imageRectangle.Bottom;
            int left = imageRectangle.Left;
            int right = imageRectangle.Right;
            for (int i = top; i < bottom; i++)
            {
                num = startX;
                for (int j = left; j < right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0)
                    {
                        float num2 = cfractal_0.fBm(ref num, ref y, 6);
                        int alpha = (int)Math.Max(0f, Math.Min(255f, (num2 + 1f) * 128f));
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(alpha, pixel.R, pixel.G, pixel.B));
                    }
                    num += skip;
                }
                y += skip;
            }
            fastBitmap.Release();
        }

        private Point[] method_0(int int_2, int int_3)
        {
            double num = random_0.NextDouble() * Math.PI * 2.0;
            double num2 = num + Math.PI * 2.0;
            double num3 = num;
            double num4 = int_2;
            double num5 = (double)int_2 * 1.25;
            int num6 = 8;
            double num7 = Math.PI / 4.0;
            List<Point> list = new List<Point>();
            for (int i = 0; i < num6; i++)
            {
                double num8 = num4;
                double num9 = num5 + Math.Sin(num3) * num8;
                double num10 = num5 + Math.Cos(num3) * num8;
                Point item = new Point((int)num9, (int)num10);
                list.Add(item);
                double num11 = num7 * 0.7 + num7 * random_0.NextDouble() * 0.3;
                num3 += num11;
                if (num3 > num2 - num7 / 2.0)
                {
                    break;
                }
            }
            return list.ToArray();
        }

        private Point[] method_1(Point[] point_0, int int_2, int int_3)
        {
            Point[] array = new Point[point_0.Length];
            for (int i = 0; i < point_0.Length; i++)
            {
                ref Point reference = ref array[i];
                reference = new Point(point_0[i].X + int_2, point_0[i].Y + int_3);
            }
            return array;
        }

        private GraphicsPath method_2(Point[] point_0)
        {
            GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
            graphicsPath.AddClosedCurve(point_0, 0.5f);
            return graphicsPath;
        }

        private PathGradientBrush method_3(GraphicsPath graphicsPath_0, Color color_0)
        {
            PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath_0);
            pathGradientBrush.CenterColor = color_0;
            if (graphicsPath_0.PointCount > 0)
            {
                Color[] array = new Color[graphicsPath_0.PointCount];
                for (int i = 0; i < array.Length; i++)
                {
                    ref Color reference = ref array[i];
                    reference = Color.Transparent;
                }
                pathGradientBrush.SurroundColors = array;
            }
            return pathGradientBrush;
        }

        private Color method_4(Types.FastBitmap fastBitmap_0, int int_2, int int_3, int int_4)
        {
            Color pixel = fastBitmap_0.GetPixel(ref int_2, ref int_3);
            int X = int_2 + int_4;
            Color pixel2 = fastBitmap_0.GetPixel(ref X, ref int_3);
            int Y = int_3 + int_4;
            Color pixel3 = fastBitmap_0.GetPixel(ref X, ref Y);
            Color pixel4 = fastBitmap_0.GetPixel(ref int_2, ref Y);
            X = int_2 + int_4 / 2;
            Y = int_3 + int_4 / 2;
            Color pixel5 = fastBitmap_0.GetPixel(ref X, ref Y);
            int num = (pixel.R + pixel2.R + pixel3.R + pixel4.R + pixel5.R) / 4;
            int num2 = (pixel.G + pixel2.G + pixel3.G + pixel4.G + pixel5.G) / 4;
            int num3 = (pixel.B + pixel2.B + pixel3.B + pixel4.B + pixel5.B) / 4;
            int alpha = Math.Min(255, Math.Max(0, (int)((double)((num + num2 + num3) / 3) * 1.3)));
            return Color.FromArgb(alpha, num, num2, num3);
        }

        public Bitmap GenerateCloudFromImage(Bitmap sourceImage, Rectangle sourceRectangle)
        {
            Graphics graphics = Graphics.FromImage(bitmap_0);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, bitmap_0.Width, bitmap_0.Height));
            graphics.DrawImage(sourceImage, new Rectangle(0, 0, bitmap_0.Width, bitmap_0.Height), sourceRectangle, GraphicsUnit.Pixel);
            ApplyNoiseToImage(bitmap_0, new Rectangle(0, 0, bitmap_0.Width, bitmap_0.Height), byte_0);
            return bitmap_0;
        }

        public Bitmap GenerateSectorCloud(Bitmap sourceImage, Rectangle sourceRectangle)
        {
            FastBitmap fastBitmap = new FastBitmap(sourceImage);
            double num = (double)sourceRectangle.Width / (double)sourceRectangle.Height;
            int height = (int)(600.0 / num) + 1;
            bitmap_0 = new Bitmap(600, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap_0);
            int num2 = 2;
            double num3 = (double)(sourceRectangle.Width - 2) / 2.0;
            double num4 = (double)(sourceRectangle.Height - 2) / 2.0;
            double num5 = (double)bitmap_0.Width / num3;
            double num6 = (double)bitmap_0.Height / num4;
            int int_ = (int)(num5 * 1.0);
            int int_2 = (int)(num5 * 1.4);
            Point[] point_ = method_0(int_2, int_);
            for (int i = 0; (double)i < num3; i++)
            {
                for (int j = 0; (double)j < num4; j++)
                {
                    int int_3 = sourceRectangle.X + (int)((double)i * (double)num2);
                    int int_4 = sourceRectangle.Y + (int)((double)j * (double)num2);
                    Color color_ = method_4(fastBitmap, int_3, int_4, num2);
                    int int_5 = (int)((double)i * num5);
                    int int_6 = (int)((double)j * num6);
                    Point[] point_2 = method_1(point_, int_5, int_6);
                    GraphicsPath graphicsPath = method_2(point_2);
                    PathGradientBrush brush = method_3(graphicsPath, color_);
                    graphics.FillPath(brush, graphicsPath);
                }
            }
            fastBitmap.Release();
            ApplyNoiseToImage(bitmap_0, new Rectangle(0, 0, bitmap_0.Width, bitmap_0.Height), byte_0);
            return bitmap_0;
        }
    }
}
