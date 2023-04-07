// Decompiled with JetBrains decompiler
// Type: DistantWorlds.NebulaCloudGenerator
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
    public class NebulaCloudGenerator
    {
        private int int_0;

        private Random random_0;

        private int int_1;

        private bool bool_0;

        public double NebulaCloudScaleFactor;

        private int hQjZroaYr7;

        private int int_2;

        private int int_3;

        private int int_4;

        private PerlinNoise perlinNoise_0;

        private byte[][] byte_0;

        private DistantWorlds.FbmNoise fbmNoise_0;

        private byte[][] byte_1;

        public int TransparencyLevel
        {
            get
            {
                return int_3;
            }
            set
            {
                int_3 = value;
            }
        }

        public int BoundarySearchPixelSkip
        {
            get
            {
                return int_4;
            }
            set
            {
                int_4 = value;
            }
        }

        public NebulaCloudGenerator(int randomSeed):this(randomSeed, -1)
        {
            Class7.VEFSJNszvZKMZ();
        }

        public NebulaCloudGenerator(int randomSeed, int colorScheme):base()
        {
            Class7.VEFSJNszvZKMZ();
            NebulaCloudScaleFactor = 4.5;
            hQjZroaYr7 = 170;
            int_2 = 200;
            int_3 = 48;
            int_4 = 8;
            int_0 = randomSeed;
            random_0 = new Random(int_0);
            if (colorScheme == -1)
            {
                int_1 = random_0.Next(0, 8);
            }
            else
            {
                int_1 = colorScheme;
            }
            perlinNoise_0 = new PerlinNoise(random_0.Next(0, 1000000));
            fbmNoise_0 = new DistantWorlds.FbmNoise(int_0);
        }

        private RectangleF method_0(GraphicsPath graphicsPath_0)
        {
            PointF location = new Point(1000000, 1000000);
            PointF pointF = new Point(-1000000, -1000000);
            PointF[] pathPoints = graphicsPath_0.PathPoints;
            for (int i = 0; i < pathPoints.Length; i++)
            {
                PointF pointF2 = pathPoints[i];
                location = new PointF((pointF2.X < location.X) ? pointF2.X : location.X, (pointF2.Y < location.Y) ? pointF2.Y : location.Y);
                pointF = new PointF((pointF2.X > pointF.X) ? pointF2.X : pointF.X, (pointF2.Y > pointF.Y) ? pointF2.Y : pointF.Y);
            }
            return new RectangleF(location, new SizeF(pointF.X - location.X, pointF.Y - location.Y));
        }

        private RectangleF method_1(GraphicsPath graphicsPath_0)
        {
            float num = 1000000f;
            float num2 = -1000000f;
            float num3 = 1000000f;
            float num4 = -1000000f;
            PointF[] pathPoints = graphicsPath_0.PathPoints;
            for (int i = 0; i < pathPoints.Length; i++)
            {
                PointF pointF = pathPoints[i];
                if (pointF.X < num)
                {
                    num = pointF.X;
                }
                if (pointF.Y < num3)
                {
                    num3 = pointF.Y;
                }
                if (pointF.X > num2)
                {
                    num2 = pointF.X;
                }
                if (pointF.Y > num4)
                {
                    num4 = pointF.Y;
                }
            }
            return new RectangleF(num, num3, num2 - num, num4 - num3);
        }

        private void method_2(ref GraphicsPath graphicsPath_0, float float_0, float float_1)
        {
            for (int i = 0; i < graphicsPath_0.PathPoints.Length; i++)
            {
                ref PointF reference = ref graphicsPath_0.PathPoints[i];
                reference = new PointF(graphicsPath_0.PathPoints[i].X + float_0, graphicsPath_0.PathPoints[i].Y + float_1);
            }
        }

        private List<Color> method_3(int int_5)
        {
            List<Color> list = new List<Color>();
            int num = int_3;
            switch (int_5)
            {
                case 0:
                    num = Math.Max(0, int_3 - 32);
                    list.Add(Color.FromArgb(num, 232, 64, 16));
                    list.Add(Color.FromArgb(num, 208, 112, 8));
                    list.Add(Color.FromArgb(num, 208, 160, 0));
                    list.Add(Color.FromArgb(num, 224, 128, 24));
                    list.Add(Color.FromArgb(num, 240, 16, 0));
                    break;
                case 1:
                    num = Math.Max(0, int_3 - 16);
                    list.Add(Color.FromArgb(num, 80, 128, 192));
                    list.Add(Color.FromArgb(num, 32, 88, 156));
                    list.Add(Color.FromArgb(num, 96, 112, 200));
                    list.Add(Color.FromArgb(num, 88, 144, 208));
                    list.Add(Color.FromArgb(num, 72, 104, 200));
                    break;
                case 2:
                    num = Math.Max(0, int_3 - 32);
                    list.Add(Color.FromArgb(num, 96, 224, 32));
                    list.Add(Color.FromArgb(num, 107, 92, 51));
                    list.Add(Color.FromArgb(num, 241, 166, 70));
                    list.Add(Color.FromArgb(num, 128, 204, 16));
                    list.Add(Color.FromArgb(num, 150, 120, 70));
                    break;
                case 3:
                    list.Add(Color.FromArgb(int_3, 255, 64, 128));
                    list.Add(Color.FromArgb(int_3, 224, 96, 192));
                    list.Add(Color.FromArgb(int_3, 160, 48, 240));
                    list.Add(Color.FromArgb(int_3, 192, 32, 96));
                    list.Add(Color.FromArgb(int_3, 160, 96, 255));
                    break;
                case 4:
                    num = Math.Max(0, int_3 - 32);
                    list.Add(Color.FromArgb(num, 208, 152, 64));
                    list.Add(Color.FromArgb(num, 224, 112, 72));
                    list.Add(Color.FromArgb(num, 156, 96, 16));
                    list.Add(Color.FromArgb(num, 192, 96, 44));
                    list.Add(Color.FromArgb(num, 112, 56, 0));
                    break;
                case 5:
                    list.Add(Color.FromArgb(int_3, 104, 104, 160));
                    list.Add(Color.FromArgb(int_3, 80, 80, 160));
                    list.Add(Color.FromArgb(int_3, 176, 128, 128));
                    list.Add(Color.FromArgb(int_3, 128, 128, 184));
                    list.Add(Color.FromArgb(int_3, 96, 96, 136));
                    break;
                case 6:
                    list.Add(Color.FromArgb(int_3, 255, 64, 48));
                    list.Add(Color.FromArgb(int_3, 255, 96, 128));
                    list.Add(Color.FromArgb(int_3, 255, 0, 0));
                    list.Add(Color.FromArgb(int_3, 224, 0, 128));
                    list.Add(Color.FromArgb(int_3, 192, 64, 32));
                    break;
                case 7:
                    num = Math.Max(0, int_3 - 16);
                    list.Add(Color.FromArgb(num, 192, 64, 255));
                    list.Add(Color.FromArgb(num, 224, 32, 224));
                    list.Add(Color.FromArgb(num, 144, 80, 250));
                    list.Add(Color.FromArgb(num, 255, 80, 160));
                    break;
                case 8:
                    list.Add(Color.FromArgb(int_3, 16, 16, 192));
                    list.Add(Color.FromArgb(int_3, 8, 8, 160));
                    list.Add(Color.FromArgb(int_3, 0, 0, 255));
                    list.Add(Color.FromArgb(int_3, 140, 0, 240));
                    list.Add(Color.FromArgb(int_3, 120, 0, 160));
                    break;
                case 9:
                    num = Math.Max(0, int_3 - 32);
                    list.Add(Color.FromArgb(num, 80, 180, 32));
                    list.Add(Color.FromArgb(num, 16, 224, 96));
                    list.Add(Color.FromArgb(num, 8, 160, 80));
                    list.Add(Color.FromArgb(num, 0, 128, 0));
                    list.Add(Color.FromArgb(num, 80, 128, 192));
                    break;
                case 10:
                    list.Add(Color.FromArgb(int_3, 80, 128, 192));
                    list.Add(Color.FromArgb(int_3, 64, 136, 176));
                    list.Add(Color.FromArgb(int_3, 96, 112, 200));
                    list.Add(Color.FromArgb(int_3, 216, 144, 80));
                    list.Add(Color.FromArgb(int_3, 176, 84, 48));
                    break;
                case 11:
                    num = Math.Max(0, int_3 - 16);
                    list.Add(Color.FromArgb(num, 232, 136, 160));
                    list.Add(Color.FromArgb(num, 180, 64, 96));
                    list.Add(Color.FromArgb(num, 96, 48, 56));
                    list.Add(Color.FromArgb(num, 240, 104, 160));
                    list.Add(Color.FromArgb(num, 153, 102, 51));
                    break;
                case 12:
                    list.Add(Color.FromArgb(int_3, 236, 61, 84));
                    list.Add(Color.FromArgb(int_3, 177, 39, 56));
                    list.Add(Color.FromArgb(int_3, 255, 32, 64));
                    list.Add(Color.FromArgb(int_3, 90, 101, 171));
                    list.Add(Color.FromArgb(int_3, 60, 67, 114));
                    break;
                case 13:
                    list.Add(Color.FromArgb(int_3, 34, 151, 137));
                    list.Add(Color.FromArgb(int_3, 111, 152, 76));
                    list.Add(Color.FromArgb(int_3, 56, 162, 165));
                    list.Add(Color.FromArgb(int_3, 0, 24, 0));
                    break;
                case 14:
                    list.Add(Color.FromArgb(int_3, 90, 78, 154));
                    list.Add(Color.FromArgb(int_3, 43, 43, 85));
                    list.Add(Color.FromArgb(int_3, 46, 39, 101));
                    list.Add(Color.FromArgb(int_3, 24, 12, 64));
                    break;
                case 15:
                    list.Add(Color.FromArgb(int_3, 0, 182, 245));
                    list.Add(Color.FromArgb(int_3, 0, 78, 187));
                    list.Add(Color.FromArgb(int_3, 0, 64, 131));
                    list.Add(Color.FromArgb(int_3, 0, 0, 46));
                    break;
                case 16:
                    list.Add(Color.FromArgb(int_3, 211, 46, 179));
                    list.Add(Color.FromArgb(int_3, 114, 21, 102));
                    list.Add(Color.FromArgb(int_3, 109, 0, 56));
                    list.Add(Color.FromArgb(int_3, 64, 0, 48));
                    break;
                case 20:
                    list.Add(Color.FromArgb(int_3, 116, 32, 8));
                    list.Add(Color.FromArgb(int_3, 104, 56, 4));
                    list.Add(Color.FromArgb(int_3, 104, 80, 0));
                    list.Add(Color.FromArgb(int_3, 112, 64, 12));
                    list.Add(Color.FromArgb(int_3, 120, 8, 0));
                    break;
                case 21:
                    list.Add(Color.FromArgb(int_3, 40, 64, 96));
                    list.Add(Color.FromArgb(int_3, 32, 68, 88));
                    list.Add(Color.FromArgb(int_3, 48, 56, 100));
                    list.Add(Color.FromArgb(int_3, 44, 72, 104));
                    list.Add(Color.FromArgb(int_3, 36, 52, 100));
                    break;
                case 22:
                    list.Add(Color.FromArgb(int_3, 48, 120, 16));
                    list.Add(Color.FromArgb(int_3, 53, 46, 25));
                    list.Add(Color.FromArgb(int_3, 120, 83, 35));
                    list.Add(Color.FromArgb(int_3, 64, 102, 8));
                    list.Add(Color.FromArgb(int_3, 75, 60, 35));
                    break;
                case 23:
                    list.Add(Color.FromArgb(int_3, 120, 32, 72));
                    list.Add(Color.FromArgb(int_3, 112, 48, 96));
                    list.Add(Color.FromArgb(int_3, 80, 24, 120));
                    list.Add(Color.FromArgb(int_3, 96, 16, 48));
                    list.Add(Color.FromArgb(int_3, 80, 48, 128));
                    break;
                case 24:
                    list.Add(Color.FromArgb(int_3, 120, 83, 35));
                    list.Add(Color.FromArgb(int_3, 128, 64, 16));
                    list.Add(Color.FromArgb(int_3, 78, 56, 16));
                    list.Add(Color.FromArgb(int_3, 112, 90, 22));
                    list.Add(Color.FromArgb(int_3, 112, 105, 16));
                    break;
                case 25:
                    list.Add(Color.FromArgb(int_3, 52, 52, 72));
                    list.Add(Color.FromArgb(int_3, 40, 40, 64));
                    list.Add(Color.FromArgb(int_3, 80, 64, 64));
                    list.Add(Color.FromArgb(int_3, 64, 64, 88));
                    list.Add(Color.FromArgb(int_3, 56, 56, 56));
                    break;
                case 26:
                    list.Add(Color.FromArgb(int_3, 128, 32, 24));
                    list.Add(Color.FromArgb(int_3, 128, 48, 64));
                    list.Add(Color.FromArgb(int_3, 128, 0, 0));
                    list.Add(Color.FromArgb(int_3, 112, 0, 64));
                    list.Add(Color.FromArgb(int_3, 96, 32, 16));
                    break;
                case 27:
                    list.Add(Color.FromArgb(int_3, 102, 72, 102));
                    list.Add(Color.FromArgb(int_3, 112, 112, 86));
                    list.Add(Color.FromArgb(int_3, 116, 116, 32));
                    list.Add(Color.FromArgb(int_3, 128, 80, 102));
                    break;
                case 28:
                    list.Add(Color.FromArgb(int_3, 16, 16, 64));
                    list.Add(Color.FromArgb(int_3, 8, 8, 32));
                    list.Add(Color.FromArgb(int_3, 0, 0, 16));
                    list.Add(Color.FromArgb(int_3, 0, 0, 80));
                    break;
                case 29:
                    list.Add(Color.FromArgb(int_3, 32, 128, 32));
                    list.Add(Color.FromArgb(int_3, 16, 64, 16));
                    list.Add(Color.FromArgb(int_3, 8, 48, 8));
                    list.Add(Color.FromArgb(int_3, 0, 32, 0));
                    break;
            }
            return list;
        }

        public void GenerateNebulaBackdrop(int seed, out List<Bitmap> cloudImages, out List<Point> cloudPositions, bool useLowQuality)
        {
            GenerateNebulaBackdrop(seed, out cloudImages, out cloudPositions, int_3, -1, hQjZroaYr7, int_2, transparentBackground: false, isGasCloud: false, useLowQuality);
        }

        public void GenerateNebulaBackdrop(int seed, out List<Bitmap> cloudImages, out List<Point> cloudPositions, double scaleFactor, bool useLowQuality)
        {
            if (scaleFactor != NebulaCloudScaleFactor)
            {
                double num = NebulaCloudScaleFactor / scaleFactor;
                NebulaCloudScaleFactor = scaleFactor;
                hQjZroaYr7 = (int)((double)hQjZroaYr7 * num);
                int_2 = (int)((double)int_2 * num);
                byte_1 = null;
            }
            GenerateNebulaBackdrop(seed, out cloudImages, out cloudPositions, int_3, -1, hQjZroaYr7, int_2, transparentBackground: false, isGasCloud: false, useLowQuality);
        }

        public void GenerateNebulaBackdrop(int seed, out List<Bitmap> cloudImages, out List<Point> cloudPositions, int transparencyLevel, int colorScheme, int minimumSize, int maximumSize, bool transparentBackground, bool isGasCloud, bool useLowQuality)
        {
            bool_0 = useLowQuality;
            random_0 = new Random(seed);
            int num = int_3;
            int_3 = transparencyLevel;
            cloudImages = new List<Bitmap>();
            cloudPositions = new List<Point>();
            int num2 = 1;
            for (int i = 0; i < num2; i++)
            {
                if (colorScheme == -1)
                {
                    colorScheme = random_0.Next(0, 17);
                }
                Bitmap bitmap = null;
                bitmap = ((!isGasCloud) ? GenerateNebulaCloud(minimumSize, maximumSize, colorScheme, transparentBackground) : method_9(minimumSize, maximumSize, colorScheme, transparentBackground));
                int num3 = 0;
                int num4 = 0;
                int num5 = 10;
                int num6 = 0;
                do
                {
                    double num7 = Math.PI * 2.0 * random_0.NextDouble();
                    double num8 = (double)(num5 / 3) * random_0.NextDouble();
                    num3 = (int)(Math.Sin(num7) * num8);
                    num4 = (int)(Math.Cos(num7) * num8);
                    num3 -= (int)((double)bitmap.Width * NebulaCloudScaleFactor / 2.0);
                    num4 -= (int)((double)bitmap.Height * NebulaCloudScaleFactor / 2.0);
                    num6++;
                }
                while (method_4(bitmap, num3, num4, cloudImages, cloudPositions) && num6 < 10);
                if (!method_4(bitmap, num3, num4, cloudImages, cloudPositions))
                {
                    Point item = new Point(num3, num4);
                    cloudPositions.Add(item);
                    cloudImages.Add(bitmap);
                }
            }
            int_3 = num;
        }

        private bool method_4(Bitmap bitmap_0, int int_5, int int_6, List<Bitmap> clouds, List<Point> positions)
        {
            if (positions.Count > 0)
            {
                int int_7 = (int)((double)bitmap_0.Width * 1.05);
                int int_8 = (int)((double)bitmap_0.Height * 1.05);
                Rectangle rectangle = method_5(int_5, int_6, int_7, int_8, NebulaCloudScaleFactor);
                for (int i = 0; i < positions.Count; i++)
                {
                    int int_9 = (int)((double)clouds[i].Width * 1.05);
                    int int_10 = (int)((double)clouds[i].Height * 1.05);
                    Rectangle rect = method_5(positions[i].X, positions[i].Y, int_9, int_10, NebulaCloudScaleFactor);
                    if (rectangle.IntersectsWith(rect))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Rectangle method_5(int int_5, int int_6, int int_7, int int_8, double double_0)
        {
            int width = (int)((double)int_7 * double_0);
            int height = (int)((double)int_8 * double_0);
            return new Rectangle(int_5, int_6, width, height);
        }

        private void method_6(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            graphics_0.InterpolationMode = InterpolationMode.Low;
            graphics_0.SmoothingMode = SmoothingMode.None;
        }

        private void method_7(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics_0.SmoothingMode = SmoothingMode.None;
        }

        private void method_8(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
        }

        private Bitmap method_9(int int_5, int int_6, int int_7, bool bool_1)
        {
            List<Color> list = method_3(int_7);
            int num = random_0.Next(3, 5);
            List<int> list2 = new List<int>();
            for (int i = 0; i < num; i++)
            {
                int item = random_0.Next(0, list.Count);
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit(list2.Contains(item), 30, ref iterationCount))
                {
                    item = random_0.Next(0, list.Count);
                }
                list2.Add(item);
            }
            List<Bitmap> list3 = new List<Bitmap>();
            int num2 = 0;
            int num3 = 0;
            for (int j = 0; j < num; j++)
            {
                int index = list2[j];
                Color color = list[index];
                Bitmap bitmap = GenerateNebulaCloudImage(color, int_5, int_6);
                if (bitmap.Width > num2)
                {
                    num2 = bitmap.Width;
                }
                if (bitmap.Height > num3)
                {
                    num3 = bitmap.Height;
                }
                list3.Add(bitmap);
            }
            int val = (int)((double)num2 * 1.5);
            int val2 = (int)((double)num3 * 1.5);
            val = Math.Max(1, val);
            val2 = Math.Max(1, val2);
            Bitmap bitmap2 = new Bitmap(val, val2, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap2))
            {
                if (bool_0)
                {
                    method_7(graphics);
                }
                else
                {
                    method_6(graphics);
                }
                if (bool_1)
                {
                    graphics.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, bitmap2.Width, bitmap2.Height);
                }
                else
                {
                    graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, bitmap2.Width, bitmap2.Height);
                }
                for (int k = 0; k < num; k++)
                {
                    int num4 = val - list3[k].Width;
                    int num5 = val2 - list3[k].Height;
                    int x = (int)(random_0.NextDouble() * (double)num4);
                    int y = (int)(random_0.NextDouble() * (double)num5);
                    graphics.DrawImage(point: new Point(x, y), image: list3[k]);
                }
            }
            bitmap2 = ((!bool_1) ? method_13(bitmap2) : method_15(bitmap2, 0));
            double roughness = 0.4;
            double lacunarity = 2.6;
            if (byte_0 == null)
            {
                byte_0 = fbmNoise_0.MakeTurbulence((int)((double)int_2 * 3.0), roughness, lacunarity);
            }
            if (byte_0.GetUpperBound(0) <= bitmap2.Width)
            {
                byte_0 = fbmNoise_0.MakeTurbulence((int)((double)bitmap2.Width * 1.2), roughness, lacunarity);
            }
            if (byte_0[0].GetUpperBound(0) <= bitmap2.Height)
            {
                byte_0 = fbmNoise_0.MakeTurbulence((int)((double)bitmap2.Height * 1.2), roughness, lacunarity);
            }
            int num6 = 0;
            int num7 = 0;
            num6 = 0;
            num7 = 0;
            if (bool_1)
            {
                perlinNoise_0.ApplyNoiseToImageOpaque(bitmap2, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), byte_0, out num6, out num7);
            }
            else
            {
                perlinNoise_0.ApplyNoiseToImageOpaque(bitmap2, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), byte_0, out num6, out num7);
            }
            for (int l = 0; l < list3.Count; l++)
            {
                Bitmap bitmap3 = list3[l];
                if (bitmap3 != null && bitmap3.PixelFormat != 0)
                {
                    bitmap3.Dispose();
                }
            }
            return bitmap2;
        }

        private Bitmap method_10(int int_5, int int_6, int int_7)
        {
            return GenerateNebulaCloud(int_5, int_6, int_7, transparentBackground: false);
        }

        public Bitmap GenerateNebulaCloud(int minimumSize, int maximumSize, int colorScheme, bool transparentBackground)
        {
            List<Color> list = method_3(colorScheme);
            int num = random_0.Next(3, 5);
            List<int> list2 = new List<int>();
            for (int i = 0; i < num; i++)
            {
                int item = random_0.Next(0, list.Count);
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit(list2.Contains(item), 30, ref iterationCount))
                {
                    item = random_0.Next(0, list.Count);
                }
                list2.Add(item);
            }
            List<Bitmap> list3 = new List<Bitmap>();
            int num2 = 0;
            int num3 = 0;
            for (int j = 0; j < num; j++)
            {
                int index = list2[j];
                Color color = list[index];
                Bitmap bitmap = GenerateNebulaCloudImage(color, minimumSize, maximumSize);
                if (bitmap.Width > num2)
                {
                    num2 = bitmap.Width;
                }
                if (bitmap.Height > num3)
                {
                    num3 = bitmap.Height;
                }
                list3.Add(bitmap);
            }
            int val = (int)((double)num2 * 1.5);
            int val2 = (int)((double)num3 * 1.5);
            val = Math.Max(1, val);
            val2 = Math.Max(1, val2);
            Bitmap bitmap2 = new Bitmap(val, val2, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap2))
            {
                if (bool_0)
                {
                    method_7(graphics);
                }
                else
                {
                    method_6(graphics);
                }
                if (transparentBackground)
                {
                    graphics.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, bitmap2.Width, bitmap2.Height);
                }
                else
                {
                    graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, bitmap2.Width, bitmap2.Height);
                }
                for (int k = 0; k < num; k++)
                {
                    int num4 = val - list3[k].Width;
                    int num5 = val2 - list3[k].Height;
                    int x = (int)(random_0.NextDouble() * (double)num4);
                    int y = (int)(random_0.NextDouble() * (double)num5);
                    graphics.DrawImage(point: new Point(x, y), image: list3[k]);
                }
            }
            bitmap2 = ((!transparentBackground) ? method_13(bitmap2) : method_15(bitmap2, 0));
            double roughness = 0.93;
            double lacunarity = 2.0;
            if (byte_0 == null)
            {
                byte_0 = fbmNoise_0.MakeNoise((int)((double)int_2 * 3.0), roughness, lacunarity);
            }
            if (byte_0.GetUpperBound(0) <= bitmap2.Width)
            {
                byte_0 = fbmNoise_0.MakeNoise((int)((double)bitmap2.Width * 1.2), roughness, lacunarity);
            }
            if (byte_0[0].GetUpperBound(0) <= bitmap2.Height)
            {
                byte_0 = fbmNoise_0.MakeNoise((int)((double)bitmap2.Height * 1.2), roughness, lacunarity);
            }
            int maxAlpha = 0;
            int num6 = 0;
            float xOffset = (float)(random_0.NextDouble() * 500.0);
            float yOffset = (float)(random_0.NextDouble() * 500.0);
            int num7 = (int)((double)int_2 * 3.0);
            if (byte_1 == null)
            {
                byte_1 = new byte[num7][];
                for (int l = 0; l < num7; l++)
                {
                    byte_1[l] = new byte[num7];
                }
            }
            fbmNoise_0.MakeRidgedMultifractal(ref byte_1, num7, roughness, lacunarity, 0.95f, 0.6f, xOffset, yOffset, NebulaCloudScaleFactor);
            if (transparentBackground)
            {
                perlinNoise_0.ApplyNoiseToImageTransparent(bitmap2, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), byte_1, 64.0);
            }
            else
            {
                perlinNoise_0.ApplyNoiseToImage(bitmap2, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), byte_1, out maxAlpha, out num6, 0.25);
            }
            maxAlpha = 0;
            num6 = 0;
            if (transparentBackground)
            {
                perlinNoise_0.ApplyNoiseToImageTransparent(bitmap2, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), byte_0, 64.0);
            }
            else
            {
                perlinNoise_0.ApplyNoiseToImage(bitmap2, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), byte_0, out maxAlpha, out num6, 0.7);
            }
            int alphaThreshhold = Math.Max(0, maxAlpha - 30);
            int colorThreshhold = Math.Max(0, num6 - 10);
            perlinNoise_0.IntensifyImageColor(bitmap2, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), maxAlpha, alphaThreshhold, num6, colorThreshhold, 3.0, 1.0, 32);
            for (int m = 0; m < list3.Count; m++)
            {
                Bitmap bitmap3 = list3[m];
                if (bitmap3 != null && bitmap3.PixelFormat != 0)
                {
                    bitmap3.Dispose();
                }
            }
            return bitmap2;
        }

        public void PopulateNoise(int noiseSize)
        {
            double roughness = 2.5;
            double lacunarity = 2.5;
            byte_0 = fbmNoise_0.MakeNoise(noiseSize, roughness, lacunarity);
        }

        private Rectangle method_11(Bitmap bitmap_0, int int_5)
        {
            return method_12(bitmap_0, int_5, Color.Empty, 0);
        }

        private Rectangle method_12(Bitmap bitmap_0, int int_5, Color color_0, int int_6)
        {
            int num = int_4;
            Rectangle rectangle = default(Rectangle);
            DistantWorlds.FastBitmap fastBitmap = new DistantWorlds.FastBitmap(bitmap_0);
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int width = bitmap_0.Width;
            int height = bitmap_0.Height;
            bool flag = false;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height - num; j += num)
                {
                    if (!color_0.IsEmpty)
                    {
                        Color pixel = fastBitmap.GetPixel(ref i, ref j);
                        if (pixel.R > color_0.R + int_6 || pixel.G > color_0.G + int_6 || pixel.B > color_0.B + int_6)
                        {
                            num2 = i;
                            flag = true;
                            break;
                        }
                    }
                    else if (fastBitmap.GetPixel(ref i, ref j).A > int_5)
                    {
                        num2 = i;
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
            int num6 = width - 1;
            int num7 = height - num;
            for (int X = num6; X >= 0; X--)
            {
                for (int k = 0; k < height - num; k += num)
                {
                    if (!color_0.IsEmpty)
                    {
                        Color pixel2 = fastBitmap.GetPixel(ref X, ref k);
                        if (pixel2.R > color_0.R + int_6 || pixel2.G > color_0.G + int_6 || pixel2.B > color_0.B + int_6)
                        {
                            num3 = X;
                            flag = true;
                            break;
                        }
                    }
                    else if (fastBitmap.GetPixel(ref X, ref k).A > int_5)
                    {
                        num3 = X;
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
            num6 = width - num;
            for (int l = 0; l < height; l++)
            {
                for (int m = 0; m < num6; m += num)
                {
                    if (!color_0.IsEmpty)
                    {
                        Color pixel3 = fastBitmap.GetPixel(ref m, ref l);
                        if (pixel3.R > color_0.R + int_6 || pixel3.G > color_0.G + int_6 || pixel3.B > color_0.B + int_6)
                        {
                            num4 = l;
                            flag = true;
                            break;
                        }
                    }
                    else if (fastBitmap.GetPixel(ref m, ref l).A > int_5)
                    {
                        num4 = l;
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
            num7 = height - 1;
            num6 = width - num;
            for (int Y = num7; Y >= 0; Y--)
            {
                for (int n = 0; n < num6; n += num)
                {
                    if (!color_0.IsEmpty)
                    {
                        Color pixel4 = fastBitmap.GetPixel(ref n, ref Y);
                        if (pixel4.R > color_0.R + int_6 || pixel4.G > color_0.G + int_6 || pixel4.B > color_0.B + int_6)
                        {
                            num5 = Y;
                            flag = true;
                            break;
                        }
                    }
                    else if (fastBitmap.GetPixel(ref n, ref Y).A > int_5)
                    {
                        num5 = Y;
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
            return new Rectangle(num2, num4, num3 - num2, num5 - num4);
        }

        public Bitmap GenerateNebulaCloudImage(Color color, int lowerSizeLimit, int upperSizeLimit)
        {
            int num = random_0.Next(lowerSizeLimit, upperSizeLimit);
            int int_ = (int)((double)num * 0.8);
            Bitmap bitmap = null;
            using (GraphicsPath graphicsPath = method_16(num, int_))
            {
                RectangleF rectangleF = method_1(graphicsPath);
                _ = rectangleF.X;
                _ = rectangleF.Y;
                int int_2 = (int)rectangleF.Width;
                int int_3 = (int)rectangleF.Height;
                int val = (int)rectangleF.Width + (int)rectangleF.X + 1;
                int val2 = (int)rectangleF.Height + (int)rectangleF.Y + 1;
                val = Math.Max(1, val);
                val2 = Math.Max(1, val2);
                bitmap = new Bitmap(val, val2, PixelFormat.Format32bppPArgb);
                using Graphics graphics = Graphics.FromImage(bitmap);
                if (bool_0)
                {
                    method_7(graphics);
                }
                else
                {
                    method_6(graphics);
                }
                graphics.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, bitmap.Width, bitmap.Height);
                method_8(graphics);
                using PathGradientBrush brush = method_17(graphicsPath, int_2, int_3, color);
                graphics.FillPath(brush, graphicsPath);
            }
            return method_15(bitmap, 0);
        }

        private Bitmap method_13(Bitmap bitmap_0)
        {
            Rectangle rectangle_ = method_12(bitmap_0, 0, Color.Black, 0);
            Bitmap result = method_14(bitmap_0, rectangle_);
            bitmap_0.Dispose();
            return result;
        }

        private Bitmap method_14(Bitmap bitmap_0, Rectangle rectangle_0)
        {
            int width = Math.Max(1, rectangle_0.Width);
            int height = Math.Max(1, rectangle_0.Height);
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            if (bool_0)
            {
                method_7(graphics);
            }
            Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Rectangle srcRect = new Rectangle(rectangle_0.X, rectangle_0.Y, rectangle_0.Width, rectangle_0.Height);
            graphics.DrawImage(bitmap_0, destRect, srcRect, GraphicsUnit.Pixel);
            return bitmap;
        }

        private Bitmap method_15(Bitmap bitmap_0, int int_5)
        {
            Rectangle rectangle_ = method_11(bitmap_0, int_5);
            Bitmap result = method_14(bitmap_0, rectangle_);
            bitmap_0.Dispose();
            return result;
        }

        private GraphicsPath method_16(int int_5, int int_6)
        {
            GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
            double num = random_0.NextDouble() * Math.PI * 2.0;
            double num2 = num + Math.PI * 2.0;
            double num3 = num;
            double num4 = int_5;
            double num5 = int_6;
            double num6 = (double)int_5 * 1.25;
            int num7 = random_0.Next(6, 12);
            double num8 = Math.PI * 2.0 / (double)num7;
            List<Point> list = new List<Point>();
            for (int i = 0; i < num7; i++)
            {
                double num9 = num5 + (num4 - num5) * random_0.NextDouble();
                double num10 = num6 + Math.Sin(num3) * num9;
                double num11 = num6 + Math.Cos(num3) * num9;
                Point item = new Point((int)num10, (int)num11);
                list.Add(item);
                double num12 = num8 * 0.7 + num8 * random_0.NextDouble() * 0.3;
                num3 += num12;
                if (num3 > num2 - num8 / 2.0)
                {
                    break;
                }
            }
            graphicsPath.AddClosedCurve(list.ToArray(), 0.5f);
            return graphicsPath;
        }

        private PathGradientBrush method_17(GraphicsPath graphicsPath_0, int int_5, int int_6, Color color_0)
        {
            PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath_0);
            pathGradientBrush.CenterColor = color_0;
            pathGradientBrush.SetSigmaBellShape(1f, 1f);
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
    }
}
