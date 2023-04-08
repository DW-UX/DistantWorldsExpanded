// Decompiled with JetBrains decompiler
// Type: DistantWorlds.LightningGenerator
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
    public class LightningGenerator
    {
        private Random random_0;

        private double double_0;

        private double double_1;

        private double double_2;

        private double double_3;

        private double double_4;

        private double double_5;

        public Bitmap GenerateLightning(int seed, int size)
        {
            random_0 = new Random(seed);
            double_2 = 0.97;
            double_1 = (double)size / 50.0;
            double_4 = random_0.NextDouble() * 2.0 * Math.PI;
            double_0 = 8.0;
            double_5 = (double)size / 400.0;
            double double_ = double_4;
            double num = double_0;
            LightningPathNode lightningPathNode_ = method_1(size / 2, size / 2, double_, double_4, num, num * 0.4);
            if (size < 1)
            {
                size = 1;
            }
            else if (size > 2000)
            {
                size = 2000;
            }
            Bitmap bitmap = Galaxy.CreateBitmapSafely(size, size, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            method_0(graphics, Color.FromArgb(208, 128, 128), lightningPathNode_, 1.4);
            method_0(graphics, Color.FromArgb(255, 255, 255), lightningPathNode_, 0.8);
            int num2 = size / 2;
            int num3 = (int)((double)size / 15.0);
            int num4 = Math.Max(1, num3 / 20);
            for (int num5 = num3; num5 > 0; num5 -= num4)
            {
                Rectangle rect = new Rectangle(num2 - num5 / 2, num2 - num5 / 2, num5, num5);
                int val = (int)(64.0 / (double)num3 * ((double)num3 - (double)num5 + 1.0));
                val = Math.Min(64, Math.Max(0, val));
                using SolidBrush brush = new SolidBrush(Color.FromArgb(val, 255, 255, 255));
                graphics.FillEllipse(brush, rect);
            }
            return bitmap;
        }

        private void method_0(Graphics graphics_0, Color color_0, LightningPathNode lightningPathNode_0, double double_6)
        {
            float val = (float)(lightningPathNode_0.Width * double_6 * double_5);
            val = Math.Max(1f, val);
            using Pen pen = new Pen(color_0, val);
            pen.EndCap = LineCap.Triangle;
            pen.StartCap = LineCap.Triangle;
            foreach (LightningPathNode child in lightningPathNode_0.Children)
            {
                graphics_0.DrawLine(pen, (float)lightningPathNode_0.X, (float)lightningPathNode_0.Y, (float)child.X, (float)child.Y);
                double num = 0.99;
                int red = Math.Max(0, (int)((double)(int)color_0.R * num));
                int green = Math.Max(0, (int)((double)(int)color_0.G * num));
                int blue = Math.Max(0, (int)((double)(int)color_0.B * num));
                Color color_ = Color.FromArgb(color_0.A, red, green, blue);
                method_0(graphics_0, color_, child, double_6);
            }
        }

        private LightningPathNode method_1(double double_6, double double_7, double double_8, double double_9, double double_10, double double_11)
        {
            LightningPathNode lightningPathNode = new LightningPathNode(double_6, double_7, double_8, double_10);
            if (double_10 < double_11)
            {
                return lightningPathNode;
            }
            double num = double_1 * 0.5 + double_1 * random_0.NextDouble();
            double num2 = double_3 * -1.0 + double_3 * 2.0 * random_0.NextDouble();
            num2 = ((!(num2 < 0.0)) ? Math.Max(0.3, num2) : Math.Min(-0.3, num2));
            double_8 += num2;
            double num3 = Math.Abs(double_8 - double_9);
            if (num3 > 1.4)
            {
                double_8 -= num2 * 2.0;
            }
            double_10 *= double_2;
            double_6 += Math.Cos(double_8) * num;
            double_7 += Math.Sin(double_8) * num;
            if (random_0.Next(0, 15) == 1)
            {
                lightningPathNode.Children.Add(method_1(double_6, double_7, double_8, double_9, double_10, double_11));
                double num4 = double_3 * -1.0 + double_3 * 2.0 * random_0.NextDouble();
                num4 = ((!(num4 > 0.0)) ? Math.Min(num4, -0.7) : Math.Max(num4, 0.7));
                double num5 = double_8 + num4;
                double double_12 = Math.Max(0.04, double_10 * 0.2);
                lightningPathNode.Children.Add(method_1(double_6, double_7, num5, num5, double_10 * 0.25, double_12));
            }
            else
            {
                lightningPathNode.Children.Add(method_1(double_6, double_7, double_8, double_9, double_10, double_11));
            }
            return lightningPathNode;
        }

        public LightningGenerator():base()
        {
            
            random_0 = new Random((int)DateTime.Now.Ticks);
            double_0 = 8.0;
            double_1 = 40.0;
            double_2 = 0.985;
            double_3 = 1.05;
            double_5 = 1.0;
        }
    }

}
