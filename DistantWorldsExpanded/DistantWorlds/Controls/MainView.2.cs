using BaconDistantWorlds;
using DistantWorlds.Types;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public partial class MainView
    {
        private Bitmap method_146(int int_11, Bitmap bitmap_7, RectangleF rectangleF_1, int int_12, int int_13, float float_0, float float_1, float float_2, Bitmap bitmap_8, out double double_15, out RectangleF rectangleF_2, bool bool_13)
        {
            _ = rectangleF_1.Height / rectangleF_1.Width;
            double_15 = 1.0;
            rectangleF_2 = new RectangleF(0f, 0f, rectangleF_1.Width, rectangleF_1.Height);
            double val = (double)int_13 / (double)rectangleF_1.Width;
            val = Math.Min(10.0, val);
            if (val > 1.0)
            {
                float num = (float)val * rectangleF_2.Width;
                float num2 = (float)val * rectangleF_2.Height;
                rectangleF_2 = new RectangleF(0f, 0f, num, num2);
            }
            else
            {
                val = 1.0;
            }
            double_15 = val;
            float num3 = (float)int_12 / float_0;
            float num4 = num3 / (float)bitmap_7.Width;
            float num5 = num3 / (float)((double)bitmap_7.Width * double_15);
            float num6 = 1f;
            float num7 = num5 * num6;
            float num8 = rectangleF_1.X * num4;
            float num9 = rectangleF_1.Y * num4;
            int num10 = Math.Min(2000, Math.Max(1, (int)rectangleF_2.Height));
            int num11 = Math.Min(2000, Math.Max(1, (int)rectangleF_2.Width));
            Bitmap bitmap = new Bitmap(num11, num10, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                method_177(graphics);
                if (bool_13)
                {
                    graphics.Clear(System.Drawing.Color.Transparent);
                }
                else
                {
                    graphics.Clear(System.Drawing.Color.Black);
                }
                graphics.DrawImage(bitmap_7, rectangleF_2, rectangleF_1, GraphicsUnit.Pixel);
            }
            System.Drawing.Rectangle imageRectangle = new System.Drawing.Rectangle(0, 0, (int)rectangleF_2.Width, (int)rectangleF_2.Height);
            num8 = num8 / num3 * float_1;
            num9 = num9 / num3 * float_1;
            num7 = num7 / num3 * float_1;
            if (sectorCloudGenerator_0 == null)
            {
                sectorCloudGenerator_0 = new SectorCloudGenerator(int_11, 16);
            }
            if (bool_13)
            {
                sectorCloudGenerator_0.FbmImageTransparent(bitmap, imageRectangle, num7, num8, num9);
            }
            else
            {
                sectorCloudGenerator_0.FbmImage(bitmap, imageRectangle, num7, num8, num9);
            }
            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics2 = Graphics.FromImage(bitmap2);
            method_176(graphics2);
            graphics2.Clear(System.Drawing.Color.Transparent);
            graphics2.DrawImageUnscaled(bitmap, new System.Drawing.Point(0, 0));
            method_21(bitmap);
            if (float_2 == 1f)
            {
                graphics2.DrawImage(bitmap_8, rectangleF_2, rectangleF_1, GraphicsUnit.Pixel);
                return bitmap2;
            }
            if (float_2 > 0f)
            {
                Bitmap bitmap3 = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
                using (Graphics graphics3 = Graphics.FromImage(bitmap3))
                {
                    method_176(graphics3);
                    graphics3.Clear(System.Drawing.Color.Transparent);
                    graphics3.DrawImage(bitmap_8, rectangleF_2, rectangleF_1, GraphicsUnit.Pixel);
                }
                bitmap3 = method_16(bitmap3, float_2);
                graphics2.DrawImageUnscaled(bitmap3, new System.Drawing.Point(0, 0));
                method_21(bitmap3);
                return bitmap2;
            }
            return bitmap2;
        }

        private void method_147()
        {
            RectangleF rectangleF_ = method_126(1);
            RectangleF rect = method_123(rectangleF_, 1.2f);
            if (rect.X < 0f)
            {
                rect.Width += rect.X + 1f;
                rect.X = 0f;
            }
            if (rect.Y < 0f)
            {
                rect.Height += rect.Y + 1f;
                rect.Y = 0f;
            }
            if (rect.Right >= (float)main_0.bitmap_176.Width)
            {
                rect.Width -= rect.Right - (float)main_0.bitmap_176.Width + 1f;
            }
            if (rect.Bottom >= (float)main_0.bitmap_176.Height)
            {
                rect.Height -= rect.Bottom - (float)main_0.bitmap_176.Height + 1f;
            }
            if (main_0.rectangleF_2.Contains(rect) && main_0.bitmap_185 != null && main_0.bitmap_185.PixelFormat != 0 && !main_0.bool_15)
            {
                return;
            }
            rectangleF_ = method_123(rectangleF_, 2f);
            float val = ((float)main_0.double_0 - 1600f) / 1000f;
            val = Math.Min(1f, Math.Max(0f, val));
            int num = 500;
            _ = rectangleF_.Height / rectangleF_.Width;
            float num2 = (float)Galaxy.SizeX / 200f;
            float num3 = num2 / (float)main_0.bitmap_176.Width;
            if (rectangleF_.Right >= (float)main_0.bitmap_176.Width)
            {
                rectangleF_.Width -= rectangleF_.Right - (float)main_0.bitmap_176.Width;
            }
            if (rectangleF_.Bottom >= (float)main_0.bitmap_176.Height)
            {
                rectangleF_.Height -= rectangleF_.Bottom - (float)main_0.bitmap_176.Height;
            }
            if (rectangleF_.X < 0f)
            {
                rectangleF_.Width += rectangleF_.X;
                rectangleF_.X = 0f;
            }
            if (rectangleF_.Y < 0f)
            {
                rectangleF_.Height += rectangleF_.Y;
                rectangleF_.Y = 0f;
            }
            if (rectangleF_.Width < 1f || rectangleF_.Height < 1f)
            {
                rectangleF_ = new RectangleF(rectangleF_.X, rectangleF_.Y, Math.Max(1f, rectangleF_.Width), Math.Max(1f, rectangleF_.Height));
            }
            RectangleF rectangleF = new RectangleF(0f, 0f, rectangleF_.Width, rectangleF_.Height);
            double num4 = (double)num / (double)rectangleF_.Width;
            num4 = 1.0;
            float num5 = 1f;
            float num6 = num3 * num5 * 1f;
            float num7 = rectangleF_.X * num3;
            float num8 = rectangleF_.Y * num3;
            int num9 = Math.Max(1, (int)rectangleF.Width);
            int num10 = Math.Max(1, (int)rectangleF.Height);
            Bitmap bitmap = new Bitmap(num9, num10, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (main_0.ZoomStatus != ZoomStatus.Stabilizing && main_0.ZoomStatus != 0)
                {
                    method_175(graphics);
                }
                else
                {
                    method_176(graphics);
                }
                graphics.Clear(System.Drawing.Color.Black);
                new System.Drawing.Rectangle(0, 0, (int)rectangleF.Width, (int)rectangleF.Height);
                graphics.DrawImage(main_0.bitmap_176, rectangleF, rectangleF_, GraphicsUnit.Pixel);
                method_176(graphics);
            }
            if (val < 1f)
            {
                if (sectorCloudGenerator_1 == null)
                {
                    sectorCloudGenerator_1 = new SectorCloudGenerator(1, 32);
                }
                float num11 = 20f;
                num7 = num7 / num2 * num11;
                num8 = num8 / num2 * num11;
                float num12 = num2 * (float)num4;
                num6 = num6 / num12 * num11;
            }
            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics2 = Graphics.FromImage(bitmap2))
            {
                method_175(graphics2);
                graphics2.Clear(System.Drawing.Color.Black);
                graphics2.DrawImageUnscaled(bitmap, new System.Drawing.Point(0, 0));
                method_21(bitmap);
                if (val == 1f)
                {
                    graphics2.DrawImage(main_0.bitmap_176, rectangleF, rectangleF_, GraphicsUnit.Pixel);
                }
                else if (val > 0f)
                {
                    Bitmap bitmap3 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics3 = Graphics.FromImage(bitmap3))
                    {
                        if (main_0.ZoomStatus != ZoomStatus.Stabilizing && main_0.ZoomStatus != 0)
                        {
                            method_175(graphics3);
                        }
                        else
                        {
                            method_176(graphics3);
                        }
                        graphics3.Clear(System.Drawing.Color.Transparent);
                        graphics3.DrawImage(main_0.bitmap_176, rectangleF, rectangleF_, GraphicsUnit.Pixel);
                    }
                    bitmap3 = method_16(bitmap3, val);
                    graphics2.DrawImageUnscaled(bitmap3, new System.Drawing.Point(0, 0));
                    method_21(bitmap3);
                }
                if (main_0.ZoomStatus != ZoomStatus.Stabilizing && main_0.ZoomStatus != 0)
                {
                    method_175(graphics2);
                }
                else
                {
                    method_176(graphics2);
                }
                Bitmap bitmap4 = method_150(rectangleF, rectangleF_, (float)num4, num2);
                graphics2.DrawImage(bitmap4, 0, 0);
                method_21(bitmap4);
                if (main_0.ZoomStatus == ZoomStatus.Stabilizing || main_0.ZoomStatus == ZoomStatus.Stable || (bool_11 && !main_0.gameOptions_0.CleanGalaxyView))
                {
                    using ImageAttributes imageAttr = method_236(0.25);
                    RectangleF rectangleF2 = method_123(method_125(), 2f);
                    if (rectangleF2.Right >= (float)Galaxy.SizeX)
                    {
                        rectangleF2.Width -= rectangleF2.Right - (float)Galaxy.SizeX;
                    }
                    if (rectangleF2.Bottom >= (float)Galaxy.SizeY)
                    {
                        rectangleF2.Height -= rectangleF2.Bottom - (float)Galaxy.SizeY;
                    }
                    if (rectangleF2.X < 0f)
                    {
                        rectangleF2.Width += rectangleF2.X;
                        rectangleF2.X = 0f;
                    }
                    if (rectangleF2.Y < 0f)
                    {
                        rectangleF2.Height += rectangleF2.Y;
                        rectangleF2.Y = 0f;
                    }
                    System.Drawing.Rectangle galaxySection = new System.Drawing.Rectangle((int)rectangleF2.X, (int)rectangleF2.Y, (int)rectangleF2.Width, (int)rectangleF2.Height);
                    double systemInfluenceSizeFactor = 1.0 + main_0.double_0 / 10000.0;
                    Bitmap bitmap5 = null;
                    if (main_0.gameOptions_0.MapOverlayEmpireTerritory)
                    {
                        bitmap5 = EmpireTerritory.CalculateEmpireTerritoryGrid(main_0._Game.Galaxy, galaxySection, bitmap.Width, bitmap.Height, galaxy_0.PlayerEmpire, main_0._Game.GodMode);
                        bitmap5 = GraphicsHelper.SmoothImage(bitmap5);
                    }
                    else
                    {
                        bitmap5 = EmpireTerritory.CalculateEmpireSystemTerritory(main_0._Game.Galaxy, galaxySection, bitmap.Width, bitmap.Height, galaxy_0.PlayerEmpire, main_0._Game.GodMode, bitmap_4, systemInfluenceSizeFactor);
                    }
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    graphics2.DrawImage(bitmap5, destRect, 0, 0, bitmap5.Width, bitmap5.Height, GraphicsUnit.Pixel, imageAttr);
                    method_21(bitmap5);
                }
                if (main_0.gameOptions_0.MapOverlayLongRangeScanners)
                {
                    bool flag = false;
                    Bitmap bitmap6 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics4 = Graphics.FromImage(bitmap6))
                    {
                        method_175(graphics4);
                        graphics4.Clear(System.Drawing.Color.Transparent);
                        RectangleF rectangleF3 = method_123(method_125(), 2f);
                        float num13 = rectangleF3.Width / rectangleF_.Width;
                        Empire playerEmpire = galaxy_0.PlayerEmpire;
                        for (int i = 0; i < playerEmpire.LongRangeScanners.Count; i++)
                        {
                            BuiltObject builtObject = playerEmpire.LongRangeScanners[i];
                            if (builtObject == null)
                            {
                                continue;
                            }
                            int sensorLongRange = builtObject.SensorLongRange;
                            if (builtObject.Role != BuiltObjectRole.Base && builtObject.CurrentSpeed != 0f)
                            {
                                continue;
                            }
                            int num14 = (int)builtObject.Xpos - sensorLongRange;
                            int num15 = (int)builtObject.Xpos + sensorLongRange;
                            int num16 = (int)builtObject.Ypos - sensorLongRange;
                            int num17 = (int)builtObject.Ypos + sensorLongRange;
                            RectangleF rect2 = new RectangleF(num14, num16, sensorLongRange * 2, sensorLongRange * 2);
                            if (rectangleF3.IntersectsWith(rect2))
                            {
                                flag = true;
                                num13 = (float)((double)Galaxy.SizeX / 2000.0);
                                float num18 = ((float)num14 - Math.Max(0f, rectangleF3.Left)) / num13;
                                float num19 = ((float)num15 - Math.Max(0f, rectangleF3.Left)) / num13;
                                float num20 = ((float)num16 - Math.Max(0f, rectangleF3.Top)) / num13;
                                float num21 = ((float)num17 - Math.Max(0f, rectangleF3.Top)) / num13;
                                RectangleF rectangleF4 = new RectangleF(num18, num20, num19 - num18, num21 - num20);
                                rectangleF4.Inflate(rectangleF4.Width * 0.1f, rectangleF4.Width * 0.1f);
                                System.Drawing.Color white = System.Drawing.Color.White;
                                using ImageAttributes imageAttr2 = method_221(white);
                                graphics4.DrawImage(main_0.bitmap_188, new System.Drawing.Rectangle((int)rectangleF4.X, (int)rectangleF4.Y, (int)rectangleF4.Width, (int)rectangleF4.Height), 0, 0, main_0.bitmap_188.Width, main_0.bitmap_188.Height, GraphicsUnit.Pixel, imageAttr2);
                            }
                        }
                    }
                    if (flag)
                    {
                        using (ImageAttributes imageAttr3 = method_236(0.13))
                        {
                            method_175(graphics2);
                            graphics2.DrawImage(bitmap6, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr3);
                        }
                        method_21(bitmap6);
                    }
                }
            }
            Bitmap bitmap_ = main_0.bitmap_185;
            main_0.bitmap_185 = bitmap2;
            method_21(bitmap_);
            RectangleF rectangleF_2 = new RectangleF(rectangleF_.X, rectangleF_.Y, rectangleF.Width, rectangleF.Height);
            main_0.rectangleF_2 = rectangleF_2;
            main_0.float_1 = (float)num4;
            main_0.bool_15 = false;
            FadeSectorBackground();
        }

        internal void method_148(ref Bitmap bitmap_7, ref Bitmap bitmap_8, Galaxy galaxy_1, double double_15)
        {
            if (galaxy_1 == null)
            {
                return;
            }
            if (bitmap_8 == null || bitmap_8.PixelFormat == PixelFormat.Undefined)
            {
                bitmap_8 = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
            }
            if (main_0.gameOptions_0.MapOverlayEmpireTerritory)
            {
                EmpireTerritory.CalculateEmpireTerritoryGrid(ref bitmap_8, galaxy_1, new System.Drawing.Rectangle(0, 0, Galaxy.SizeX, Galaxy.SizeY), bitmap_7.Width, bitmap_7.Height, galaxy_1.PlayerEmpire, main_0._Game.GodMode);
                bitmap_8 = GraphicsHelper.SmoothImage(bitmap_8);
            }
            else
            {
                EmpireTerritory.CalculateEmpireSystemTerritory(ref bitmap_8, galaxy_1, new System.Drawing.Rectangle(0, 0, Galaxy.SizeX, Galaxy.SizeY), bitmap_7.Width, bitmap_7.Height, galaxy_1.PlayerEmpire, main_0._Game.GodMode, bitmap_4, double_15);
            }
            using Graphics graphics = Graphics.FromImage(bitmap_7);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.None;
            using ImageAttributes imageAttr = method_236(0.25);
            graphics.DrawImage(destRect: new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height), image: bitmap_8, srcX: 0, srcY: 0, srcWidth: bitmap_8.Width, srcHeight: bitmap_8.Height, srcUnit: GraphicsUnit.Pixel, imageAttr: imageAttr);
        }

        internal void method_149(Bitmap bitmap_7, ref Bitmap bitmap_8, Bitmap bitmap_9)
        {
            using Graphics graphics = Graphics.FromImage(bitmap_7);
            method_175(graphics);
            bool flag = false;
            if (bitmap_8 != null && bitmap_8.PixelFormat != 0)
            {
                if (bitmap_8.Width != bitmap_7.Width || bitmap_8.Height != bitmap_7.Height)
                {
                    method_21(bitmap_8);
                    bitmap_8 = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
                }
            }
            else
            {
                bitmap_8 = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
            }
            using (Graphics graphics2 = Graphics.FromImage(bitmap_8))
            {
                method_175(graphics2);
                graphics2.Clear(System.Drawing.Color.Transparent);
                RectangleF rectangleF = new RectangleF(0f, 0f, Galaxy.SizeX, Galaxy.SizeY);
                float num = rectangleF.Width / (float)bitmap_7.Width;
                Empire playerEmpire = galaxy_0.PlayerEmpire;
                if (playerEmpire.LongRangeScanners.Count > 0)
                {
                    System.Drawing.Color white = System.Drawing.Color.White;
                    using ImageAttributes imageAttr = method_221(white);
                    for (int i = 0; i < playerEmpire.LongRangeScanners.Count; i++)
                    {
                        BuiltObject builtObject = playerEmpire.LongRangeScanners[i];
                        int sensorLongRange = builtObject.SensorLongRange;
                        if (builtObject.Role == BuiltObjectRole.Base || builtObject.CurrentSpeed == 0f)
                        {
                            int num2 = (int)builtObject.Xpos - sensorLongRange;
                            int num3 = (int)builtObject.Xpos + sensorLongRange;
                            int num4 = (int)builtObject.Ypos - sensorLongRange;
                            int num5 = (int)builtObject.Ypos + sensorLongRange;
                            RectangleF rect = new RectangleF(num2, num4, sensorLongRange * 2, sensorLongRange * 2);
                            if (rectangleF.IntersectsWith(rect))
                            {
                                flag = true;
                                float num6 = (float)(((double)num2 - (double)rectangleF.Left) / (double)num);
                                float num7 = (float)(((double)num3 - (double)rectangleF.Left) / (double)num);
                                float num8 = (float)(((double)num4 - (double)rectangleF.Top) / (double)num);
                                float num9 = (float)(((double)num5 - (double)rectangleF.Top) / (double)num);
                                RectangleF rectangleF2 = new RectangleF(num6, num8, num7 - num6, num9 - num8);
                                rectangleF2.Inflate(rectangleF2.Width * 0.1f, rectangleF2.Width * 0.1f);
                                graphics2.DrawImage(bitmap_9, new System.Drawing.Rectangle((int)rectangleF2.X, (int)rectangleF2.Y, (int)rectangleF2.Width, (int)rectangleF2.Height), 0, 0, bitmap_9.Width, bitmap_9.Height, GraphicsUnit.Pixel, imageAttr);
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                using (ImageAttributes imageAttr2 = method_236(0.13))
                {
                    graphics.DrawImage(bitmap_8, new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height), 0, 0, bitmap_7.Width, bitmap_7.Height, GraphicsUnit.Pixel, imageAttr2);
                    return;
                }
            }
        }

        private Bitmap method_150(RectangleF rectangleF_1, RectangleF rectangleF_2, float float_0, float float_1)
        {
            int num = Math.Max(1, (int)rectangleF_1.Width);
            int num2 = Math.Max(1, (int)rectangleF_1.Height);
            Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (main_0.ZoomStatus != ZoomStatus.Stabilizing && main_0.ZoomStatus != 0)
                {
                    method_175(graphics);
                }
                else
                {
                    method_176(graphics);
                }
                graphics.Clear(System.Drawing.Color.Transparent);
                graphics.DrawImage(main_0.bitmap_182, rectangleF_1, rectangleF_2, GraphicsUnit.Pixel);
            }
            float num3 = 1f;
            float num4 = float_1 / (float)main_0.bitmap_182.Width;
            float num5 = num4 * num3 * float_0;
            float num6 = rectangleF_2.X * num4;
            float num7 = rectangleF_2.Y * num4;
            if (sectorCloudGenerator_1 == null)
            {
                sectorCloudGenerator_1 = new SectorCloudGenerator(1, 16);
            }
            float num8 = 20f;
            num6 = num6 / float_1 * num8;
            num7 = num7 / float_1 * num8;
            float num9 = float_1 * float_0;
            num5 = num5 / num9 * num8;
            new System.Drawing.Rectangle((int)rectangleF_1.X, (int)rectangleF_1.Y, (int)rectangleF_1.Width, (int)rectangleF_1.Height);
            return bitmap;
        }

        private bool method_151(System.Drawing.Rectangle rectangle_5, System.Drawing.Rectangle rectangle_6)
        {
            return rectangle_5.IntersectsWith(rectangle_6);
        }

        private bool method_152(StarFieldItem starFieldItem_0, System.Drawing.Rectangle rectangle_5)
        {
            return rectangle_5.Contains(starFieldItem_0.X, starFieldItem_0.Y);
        }

        private void method_153(Habitat habitat_1, int int_11, int int_12, Graphics graphics_0)
        {
            Race dominantRace = habitat_1.Population.DominantRace;
            if (dominantRace != null)
            {
                int num = 25;
                int num2 = 15;
                num = (int)(25.0 / main_0.double_0);
                num2 = (int)(15.0 / main_0.double_0);
                if (num < 15)
                {
                    num = 15;
                    num2 = 10;
                }
                int_11 += 5;
                int_12 += num2 + 5;
                if (habitat_1.Owner != null && habitat_1.Owner != main_0._Game.Galaxy.IndependentEmpire)
                {
                    int_12 += num2 + 3;
                }
                graphics_0.DrawImageUnscaled(rect: new System.Drawing.Rectangle(int_11, int_12, num, num), image: main_0.raceImageCache_0.GetRaceImage(dominantRace.PictureRef, useSmall: true, useAlternate: false));
            }
        }

        private void method_154(Habitat habitat_1, int int_11, int int_12, Graphics graphics_0)
        {
            int num = 25;
            int num2 = 15;
            num = (int)(25.0 / main_0.double_0);
            num2 = (int)(15.0 / main_0.double_0);
            if (num < 15)
            {
                num = 15;
                num2 = 10;
            }
            int_11 += 4;
            if (habitat_1.Owner != null && habitat_1.Owner != main_0._Game.Galaxy.IndependentEmpire)
            {
                int_12 += num2 + 3;
            }
            int num3 = num / 5;
            int num4 = num2 / 5;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    graphics_0.FillRectangle(main_0.solidBrush_2, int_11 + i * num3, int_12 + (5 - j) * num4, num3 - 1, num4 - 1);
                }
            }
            int num5 = 0;
            int num6 = 0;
            if (habitat_1.Population.TotalAmount > 2500000000L)
            {
                num5 = 5;
            }
            else if (habitat_1.Population.TotalAmount > 500000000L)
            {
                num5 = 4;
            }
            else if (habitat_1.Population.TotalAmount > 100000000L)
            {
                num5 = 3;
            }
            else if (habitat_1.Population.TotalAmount > 20000000L)
            {
                num5 = 2;
            }
            else if (habitat_1.Population.TotalAmount > 0L)
            {
                num5 = 1;
            }
            if (habitat_1.DevelopmentLevel > 80)
            {
                num6 = 5;
            }
            else if (habitat_1.DevelopmentLevel > 60)
            {
                num6 = 4;
            }
            else if (habitat_1.DevelopmentLevel > 40)
            {
                num6 = 3;
            }
            else if (habitat_1.DevelopmentLevel > 20)
            {
                num6 = 2;
            }
            else if (habitat_1.DevelopmentLevel > 0)
            {
                num6 = 1;
            }
            for (int k = 0; k < num5; k++)
            {
                for (int l = 0; l < num6; l++)
                {
                    graphics_0.FillRectangle(main_0.solidBrush_3, int_11 + k * num3, int_12 + (5 - l) * num4, num3 - 1, num4 - 1);
                }
            }
            if (habitat_0 != null && habitat_1 == habitat_0 && bool_2)
            {
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(int_11 - 3, int_12 - 1, 31, 20);
                method_200(rectangle_, graphics_0);
            }
        }

        private void method_155(Habitat habitat_1, int int_11, int int_12, Graphics graphics_0)
        {
            int num = 4;
            int num2 = 3;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    graphics_0.FillRectangle(main_0.solidBrush_2, int_11 + i * num, int_12 + (4 - j) * num2, num - 1, num2 - 1);
                }
            }
            int num3 = 0;
            int num4 = 0;
            if (habitat_1.Population.TotalAmount > 2500000000L)
            {
                num3 = 5;
            }
            else if (habitat_1.Population.TotalAmount > 500000000L)
            {
                num3 = 4;
            }
            else if (habitat_1.Population.TotalAmount > 100000000L)
            {
                num3 = 3;
            }
            else if (habitat_1.Population.TotalAmount > 20000000L)
            {
                num3 = 2;
            }
            else if (habitat_1.Population.TotalAmount > 0L)
            {
                num3 = 1;
            }
            if (habitat_1.DevelopmentLevel > 80)
            {
                num4 = 5;
            }
            else if (habitat_1.DevelopmentLevel > 60)
            {
                num4 = 4;
            }
            else if (habitat_1.DevelopmentLevel > 40)
            {
                num4 = 3;
            }
            else if (habitat_1.DevelopmentLevel > 20)
            {
                num4 = 2;
            }
            else if (habitat_1.DevelopmentLevel > 0)
            {
                num4 = 1;
            }
            for (int k = 0; k < num3; k++)
            {
                for (int l = 0; l < num4; l++)
                {
                    graphics_0.FillRectangle(main_0.solidBrush_3, int_11 + k * num, int_12 + (4 - l) * num2, num - 1, num2 - 1);
                }
            }
            if (habitat_0 != null && habitat_1 == habitat_0 && bool_2)
            {
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(int_11 - 3, int_12 - 1, 26, 20);
                method_200(rectangle_, graphics_0);
            }
        }

        private void method_156(Texture2D texture2D_61, StellarObject stellarObject_0, FighterWeapon fighterWeapon_0, out int int_11, out int int_12, out float float_0, out float float_1, out float float_2)
        {
            double num = 18.0;
            num = Math.Min(300.0, 10.0 * Math.Sqrt(fighterWeapon_0.RawDamage));
            double num2 = num / (double)texture2D_61.Height;
            _ = texture2D_61.Width;
            num2 /= main_0.double_0;
            double num3 = num2;
            double num4 = num2;
            double num5 = fighterWeapon_0.Heading;
            double num6 = fighterWeapon_0.X;
            double num7 = fighterWeapon_0.Y;
            double num8 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, fighterWeapon_0.X, fighterWeapon_0.Y);
            num8 /= main_0.double_0;
            if (num8 < num)
            {
                double num9 = num / num8;
                num3 /= num9;
            }
            double num10 = (num6 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num11 = (num7 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)num10;
            int_12 = (int)num11;
            float_0 = (float)num3;
            float_1 = (float)num4;
            float_2 = (float)num5;
        }

        private Bitmap method_157(Bitmap bitmap_7, Fighter fighter_0, FighterWeapon fighterWeapon_0, out int int_11, out int int_12)
        {
            double num = Math.Min(8.0, Math.Max(0.5, Math.Sqrt(Math.Max(0.5, fighterWeapon_0.Power)) / 2.5));
            double num2 = Math.Min(3.0, Math.Max(0.7, Math.Sqrt(Math.Max(0.5, fighterWeapon_0.Power)) / 3.0));
            num2 /= main_0.double_0;
            num /= main_0.double_0;
            double num3 = (double)fighterWeapon_0.Heading * -1.0;
            double num4 = fighterWeapon_0.X;
            double num5 = fighterWeapon_0.Y;
            int num6 = (int)((double)bitmap_7.Width * num);
            int num7 = (int)((double)bitmap_7.Height * num2);
            double num8 = galaxy_0.CalculateDistance(fighter_0.Xpos, fighter_0.Ypos, fighterWeapon_0.X, fighterWeapon_0.Y);
            num8 /= main_0.double_0;
            if (num8 < (double)num6)
            {
                num6 = (int)num8;
            }
            int num9 = 0;
            int num10 = 0;
            if (num6 > num7)
            {
                num10 = (num6 - num7) / 2;
            }
            if (num7 > num6)
            {
                num9 = (num7 - num6) / 2;
            }
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(num9, num10, num6, num7);
            int num11 = Math.Max(1, num6 + num9 * 2);
            int num12 = Math.Max(1, num7 + num10 * 2);
            Bitmap bitmap = new Bitmap(num11, num12, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                method_176(graphics);
                graphics.DrawImage(bitmap_7, destRect, srcRect, GraphicsUnit.Pixel);
            }
            bitmap = method_218(bitmap, (float)num3, GraphicsQuality.Medium);
            double num13 = (num4 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num14 = (num5 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)(num13 - (double)bitmap.Width / 2.0);
            int_12 = (int)(num14 - (double)bitmap.Height / 2.0);
            Math.Cos((double)fighterWeapon_0.Heading + Math.PI);
            _ = (double)bitmap.Width / 2.0;
            Math.Sin((double)fighterWeapon_0.Heading + Math.PI);
            _ = (double)bitmap.Width / 2.0;
            return bitmap;
        }

        private Bitmap method_158(Bitmap bitmap_7, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12)
        {
            StellarObject target = weapon_0.Target;
            double num = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target.Xpos, target.Ypos);
            double num2 = num / (double)bitmap_7.Width;
            double num3 = 1.0;
            num2 /= main_0.double_0;
            num3 /= main_0.double_0;
            int num4 = Math.Max(1, (int)((double)bitmap_7.Width * num2));
            int num5 = Math.Max(1, (int)((double)bitmap_7.Height * num3));
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, num4, num5);
            Bitmap bitmap = new Bitmap(num4, num5, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                method_176(graphics);
                graphics.DrawImage(bitmap_7, destRect, srcRect, GraphicsUnit.Pixel);
            }
            double num6 = (double)weapon_0.Heading * -1.0;
            bitmap = method_218(bitmap, (float)num6, GraphicsQuality.Medium);
            double num7 = Math.Min(stellarObject_0.Xpos, target.Xpos);
            double num8 = Math.Min(stellarObject_0.Ypos, target.Ypos);
            double num9 = (num7 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num10 = (num8 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)num9;
            int_12 = (int)num10;
            return bitmap;
        }

        private void method_159(Texture2D texture2D_61, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12, out float float_0, out float float_1, out float float_2)
        {
            method_160(texture2D_61, stellarObject_0, weapon_0, out int_11, out int_12, out float_0, out float_1, out float_2, bool_13: false);
        }

        private void method_160(Texture2D texture2D_61, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12, out float float_0, out float float_1, out float float_2, bool bool_13)
        {
            double num = 18.0;
            double num2 = 3.0;
            num = Math.Min(300.0, 10.0 * Math.Sqrt(weapon_0.RawDamage));
            if (weapon_0.Component != null && (weapon_0.Component.Type == ComponentType.WeaponRailGun || weapon_0.Component.Type == ComponentType.WeaponSuperRailGun))
            {
                num = Math.Min(300.0, 7.0 * Math.Sqrt(weapon_0.RawDamage));
            }
            else if (bool_13)
            {
                num = 18.0;
            }
            double num3 = num / (double)texture2D_61.Height;
            num2 = num3 * (double)texture2D_61.Width;
            method_161(texture2D_61, stellarObject_0, weapon_0, num, num2, out int_11, out int_12, out float_0, out float_1, out float_2, bool_13);
        }

        private void method_161(Texture2D texture2D_61, StellarObject stellarObject_0, Weapon weapon_0, double double_15, double double_16, out int int_11, out int int_12, out float float_0, out float float_1, out float float_2, bool bool_13)
        {
            double num = double_15 / (double)texture2D_61.Height;
            num /= main_0.double_0;
            double num2 = num;
            double num3 = num;
            double num4 = weapon_0.Heading;
            double num5 = weapon_0.X;
            double num6 = weapon_0.Y;
            if (!bool_13)
            {
                double num7 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, weapon_0.X, weapon_0.Y);
                num7 /= main_0.double_0;
                if (num7 < double_15)
                {
                    double num8 = double_15 / num7;
                    num2 /= num8;
                }
            }
            double num9 = (num5 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num10 = (num6 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)num9;
            int_12 = (int)num10;
            float_0 = (float)num2;
            float_1 = (float)num3;
            float_2 = (float)num4;
        }

        private Bitmap method_162(Bitmap bitmap_7, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12)
        {
            return method_163(bitmap_7, stellarObject_0, weapon_0, out int_11, out int_12, bool_13: false);
        }

        private Bitmap method_163(Bitmap bitmap_7, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12, bool bool_13)
        {
            double num = Math.Min(8.0, Math.Max(0.5, Math.Sqrt(Math.Max(0.5, weapon_0.Power)) / 2.5));
            double num2 = Math.Min(3.0, Math.Max(0.7, Math.Sqrt(Math.Max(0.5, weapon_0.Power)) / 3.0));
            num2 /= main_0.double_0;
            num /= main_0.double_0;
            if (weapon_0.Component.Type == ComponentType.WeaponRailGun)
            {
                num2 /= 2.5;
                num /= 2.5;
            }
            else if (weapon_0.Component.Type == ComponentType.WeaponPointDefense)
            {
                num2 /= 2.5;
                num /= 2.5;
            }
            else if (weapon_0.Component.Type == ComponentType.WeaponIonCannon)
            {
                num2 /= 2.2;
                num /= 2.2;
            }
            double num3 = (double)weapon_0.Heading * -1.0;
            double num4 = weapon_0.X;
            double num5 = weapon_0.Y;
            int num6 = (int)((double)bitmap_7.Width * num);
            int num7 = (int)((double)bitmap_7.Height * num2);
            if (!bool_13)
            {
                double num8 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, weapon_0.X, weapon_0.Y);
                num8 /= main_0.double_0;
                if (num8 < (double)num6)
                {
                    num6 = (int)num8;
                }
            }
            int num9 = 0;
            int num10 = 0;
            if (num6 > num7)
            {
                num10 = (num6 - num7) / 2;
            }
            if (num7 > num6)
            {
                num9 = (num7 - num6) / 2;
            }
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(num9, num10, num6, num7);
            int num11 = Math.Max(1, num6 + num9 * 2);
            int num12 = Math.Max(1, num7 + num10 * 2);
            Bitmap bitmap = new Bitmap(num11, num12, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                method_176(graphics);
                graphics.DrawImage(bitmap_7, destRect, srcRect, GraphicsUnit.Pixel);
            }
            bitmap = method_218(bitmap, (float)num3, GraphicsQuality.Medium);
            double num13 = (num4 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num14 = (num5 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)(num13 - (double)bitmap.Width / 2.0);
            int_12 = (int)(num14 - (double)bitmap.Height / 2.0);
            return bitmap;
        }

        private void method_164(Fighter fighter_0, DateTime dateTime_5, int int_11, int int_12, Graphics graphics_0)
        {
            method_176(graphics_0);
            for (int i = 0; i < fighter_0.Weapons.Count; i++)
            {
                if (!(fighter_0.Weapons[i].DistanceTravelled >= 0f))
                {
                    continue;
                }
                if (!fighter_0.Weapons[i].SoundEffectPlayed)
                {
                    double double_ = 0.0;
                    double double_2 = 0.0;
                    method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                    fighter_0.Weapons[i].SoundEffectPlayed = true;
                    main_0.method_0(main_0.EffectsPlayer.ResolveFighterWeapon(fighter_0.Specification.WeaponSoundEffectFilename, fighter_0.Weapons[i].Type, double_, double_2));
                }
                FighterWeapon fighterWeapon = fighter_0.Weapons[i];
                System.Drawing.Color.FromArgb(255, 255, 255);
                System.Drawing.Color.FromArgb(255, 255, 255);
                double num = Math.Min(1.0, (double)fighterWeapon.DistanceTravelled / (double)fighterWeapon.Range);
                double num2 = 1.0;
                double num3 = 0.6;
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        num3 = 0.75;
                        break;
                    case ComponentCategoryType.WeaponTorpedo:
                        num3 = 0.6;
                        break;
                }
                if (num > num3)
                {
                    num2 = (1.0 - num) / (1.0 - num3);
                }
                ImageAttributes imageAttributes = null;
                if (num2 < 1.0)
                {
                    imageAttributes = method_236(num2);
                }
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        {
                            Bitmap bitmap2 = null;
                            bitmap2 = ((fighterWeapon.SpecialImageIndex < 0 || fighterWeapon.SpecialImageIndex >= main_0.bitmap_13.Length) ? main_0.bitmap_13[0] : main_0.bitmap_13[fighterWeapon.SpecialImageIndex]);
                            int int_13 = 0;
                            int int_14 = 0;
                            using (Bitmap bitmap3 = method_157(bitmap2, fighter_0, fighterWeapon, out int_13, out int_14))
                            {
                                method_176(graphics_0);
                                if (num2 < 1.0 && imageAttributes != null)
                                {
                                    graphics_0.DrawImage(bitmap3, new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height), 0, 0, bitmap3.Width, bitmap3.Height, GraphicsUnit.Pixel, imageAttributes);
                                }
                                else
                                {
                                    graphics_0.DrawImage(bitmap3, new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height), 0, 0, bitmap3.Width, bitmap3.Height, GraphicsUnit.Pixel);
                                }
                                method_177(graphics_0);
                                System.Drawing.Rectangle item = new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height);
                                list_3.Add(item);
                            }
                            break;
                        }
                    case ComponentCategoryType.WeaponTorpedo:
                        {
                            float num4 = (float)(((double)fighterWeapon.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                            float num5 = (float)(((double)fighterWeapon.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                            int num6 = -1;
                            double num7 = 0.0;
                            double num8 = fighterWeapon.Power;
                            double num9 = -1000.0;
                            float val = (float)(num8 / 9.0) + 5f;
                            val = Math.Min(val, 18f);
                            float num10 = val / (float)main_0.double_0;
                            if (num10 < 1f)
                            {
                                num10 = 1f;
                            }
                            float num11 = num10;
                            float num12 = num4 - num10 / 2f;
                            float num13 = num5 - num11 / 2f;
                            double num14 = 1.0;
                            if (num6 < 0)
                            {
                                switch (fighterWeapon.Type)
                                {
                                    case ComponentType.WeaponTorpedo:
                                        num6 = ((fighterWeapon.SpecialImageIndex >= 0 && fighterWeapon.SpecialImageIndex < main_0.bitmap_12.Length) ? fighterWeapon.SpecialImageIndex : 0);
                                        num7 = Math.PI;
                                        break;
                                    case ComponentType.WeaponMissile:
                                        num6 = ((fighterWeapon.SpecialImageIndex >= 0 && fighterWeapon.SpecialImageIndex < main_0.bitmap_12.Length) ? fighterWeapon.SpecialImageIndex : 0);
                                        num7 = 0.0;
                                        num9 = fighterWeapon.Heading;
                                        num9 *= -1.0;
                                        num14 = 2.0;
                                        break;
                                }
                            }
                            double totalSeconds = dateTime_5.Subtract(fighterWeapon.LastFired).TotalSeconds;
                            float float_ = (float)(totalSeconds * num7);
                            if (num9 > -1000.0)
                            {
                                float_ = (float)num9;
                            }
                            using (Bitmap bitmap = method_218(main_0.bitmap_12[num6], float_, GraphicsQuality.High))
                            {
                                double num15 = num14 * ((double)bitmap.Width / (double)main_0.bitmap_12[num6].Width);
                                float num16 = (float)((double)num10 * num15);
                                float num17 = (float)((double)num11 * num15);
                                num12 += (num10 - num16) / 2f;
                                num13 += (num11 - num17) / 2f;
                                if (num2 < 1.0 && imageAttributes != null)
                                {
                                    graphics_0.DrawImage(bitmap, new System.Drawing.Rectangle((int)num12, (int)num13, (int)num16, (int)num17), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
                                }
                                else
                                {
                                    graphics_0.DrawImage(bitmap, new System.Drawing.Rectangle((int)num12, (int)num13, (int)num16, (int)num17), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
                                }
                                System.Drawing.Rectangle item = new System.Drawing.Rectangle((int)num12 - 1, (int)num13 - 1, (int)num10 + 2, (int)num11 + 2);
                                list_3.Add(item);
                            }
                            break;
                        }
                }
                imageAttributes?.Dispose();
            }
            method_177(graphics_0);
        }

        private void method_165(SpriteBatch spriteBatch_2, Fighter fighter_0, DateTime dateTime_5, int int_11, int int_12)
        {
            int weaponImageIndex = fighter_0.Specification.WeaponImageIndex;
            for (int i = 0; i < fighter_0.Weapons.Count; i++)
            {
                if (!(fighter_0.Weapons[i].DistanceTravelled >= 0f))
                {
                    continue;
                }
                if (!fighter_0.Weapons[i].SoundEffectPlayed)
                {
                    double double_ = 0.0;
                    double double_2 = 0.0;
                    method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                    fighter_0.Weapons[i].SoundEffectPlayed = true;
                    main_0.method_0(main_0.EffectsPlayer.ResolveFighterWeapon(fighter_0.Specification.WeaponSoundEffectFilename, fighter_0.Weapons[i].Type, double_, double_2));
                }
                FighterWeapon fighterWeapon = fighter_0.Weapons[i];
                System.Drawing.Color.FromArgb(255, 255, 255);
                System.Drawing.Color.FromArgb(255, 255, 255);
                double num = Math.Min(1.0, (double)fighterWeapon.DistanceTravelled / (double)fighterWeapon.Range);
                double num2 = 1.0;
                double num3 = 0.6;
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        num3 = 0.75;
                        break;
                    case ComponentCategoryType.WeaponTorpedo:
                        num3 = 0.6;
                        break;
                }
                if (num > num3)
                {
                    num2 = (1.0 - num) / (1.0 - num3);
                }
                System.Drawing.Color color = System.Drawing.Color.White;
                if (num2 < 1.0)
                {
                    color = System.Drawing.Color.FromArgb((int)(num2 * 255.0), 255, 255, 255);
                }
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        {
                            Texture2D texture2D2 = null;
                            texture2D2 = ((weaponImageIndex < 0 || weaponImageIndex >= texture2D_2.Length) ? texture2D_2[0] : texture2D_2[weaponImageIndex]);
                            int int_13 = 0;
                            int int_14 = 0;
                            float float_ = 1f;
                            float float_2 = 1f;
                            float float_3 = 0f;
                            method_156(texture2D2, fighter_0, fighterWeapon, out int_13, out int_14, out float_, out float_2, out float_3);
                            XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D2, int_13, int_14, float_3, new SizeF(float_2, float_), color, offsetPositionByCenter: false);
                            break;
                        }
                    case ComponentCategoryType.WeaponTorpedo:
                        {
                            float num4 = (float)(((double)fighterWeapon.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                            float num5 = (float)(((double)fighterWeapon.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                            int num6 = -1;
                            double num7 = 0.0;
                            double num8 = fighterWeapon.Power;
                            double num9 = -1000.0;
                            float num10 = (float)(num8 / 0.75) + 5f;
                            num10 = ((fighterWeapon.Type != ComponentType.WeaponMissile) ? ((float)(num8 / 4.0) + 7f) : ((float)(num8 / 0.6) + 5f));
                            num10 = Math.Min(num10, 18f);
                            float num11 = num10 / (float)main_0.double_0;
                            if (num11 < 1f)
                            {
                                num11 = 1f;
                            }
                            double num12 = 1.0;
                            if (num6 < 0)
                            {
                                switch (fighterWeapon.Type)
                                {
                                    case ComponentType.WeaponTorpedo:
                                        num6 = ((weaponImageIndex >= 0 && weaponImageIndex < texture2D_1.Length) ? weaponImageIndex : 0);
                                        num7 = Math.PI;
                                        break;
                                    case ComponentType.WeaponMissile:
                                        num6 = ((weaponImageIndex >= 0 && weaponImageIndex < texture2D_1.Length) ? weaponImageIndex : 0);
                                        num7 = 0.0;
                                        num9 = fighterWeapon.Heading;
                                        num12 = 2.0;
                                        break;
                                }
                            }
                            double totalSeconds = dateTime_5.Subtract(fighterWeapon.LastFired).TotalSeconds;
                            float rotationAngle = (float)(totalSeconds * num7);
                            if (num9 > -1000.0)
                            {
                                rotationAngle = (float)num9;
                            }
                            Texture2D texture2D = texture2D_1[num6];
                            num12 = (double)num11 / (double)texture2D.Width;
                            XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D, num4, num5, rotationAngle, (float)num12, color, offsetPositionByCenter: false);
                            break;
                        }
                }
            }
        }

        private void method_166(BuiltObject builtObject_1, DateTime dateTime_5, int int_11, int int_12, Graphics graphics_0)
        {
            method_176(graphics_0);
            for (int i = 0; i < builtObject_1.Weapons.Count; i++)
            {
                method_170(builtObject_1.Weapons[i], builtObject_1, dateTime_5, int_11, int_12, graphics_0);
            }
            method_177(graphics_0);
        }

        private void method_167(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, DateTime dateTime_5, int int_11, int int_12)
        {
            for (int i = 0; i < builtObject_1.Weapons.Count; i++)
            {
                method_171(spriteBatch_2, builtObject_1.Weapons[i], builtObject_1, dateTime_5, int_11, int_12);
            }
        }

        private void method_168(Habitat habitat_1, DateTime dateTime_5, int int_11, int int_12, Graphics graphics_0)
        {
            if (habitat_1.GiantIonCannonPresent && habitat_1.GiantIonCannon != null)
            {
                method_176(graphics_0);
                method_170(habitat_1.GiantIonCannon, habitat_1, dateTime_5, int_11, int_12, graphics_0);
                method_177(graphics_0);
            }
        }

        private void method_169(SpriteBatch spriteBatch_2, Habitat habitat_1, DateTime dateTime_5, int int_11, int int_12)
        {
            if (habitat_1.GiantIonCannonPresent && habitat_1.GiantIonCannon != null)
            {
                method_171(spriteBatch_2, habitat_1.GiantIonCannon, habitat_1, dateTime_5, int_11, int_12);
            }
        }

        private void method_170(Weapon weapon_0, StellarObject stellarObject_0, DateTime dateTime_5, int int_11, int int_12, Graphics graphics_0)
        {
            if (!(weapon_0.DistanceTravelled >= 0f))
            {
                return;
            }
            if (!weapon_0.SoundEffectPlayed)
            {
                double double_ = 0.0;
                double double_2 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                weapon_0.SoundEffectPlayed = true;
                main_0.method_0(main_0.EffectsPlayer.ResolveWeapon(weapon_0.Component, double_, double_2));
            }
            Component component = weapon_0.Component;
            System.Drawing.Color.FromArgb(255, 255, 255);
            System.Drawing.Color.FromArgb(255, 255, 255);
            double num = Math.Min(1.0, (double)weapon_0.DistanceTravelled / (double)weapon_0.Range);
            double num2 = 1.0;
            double num3 = 0.6;
            switch (component.Type)
            {
                case ComponentType.WeaponPhaser:
                    num3 = 0.97;
                    break;
                case ComponentType.WeaponTorpedo:
                    num3 = 0.6;
                    break;
                case ComponentType.WeaponMissile:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponRailGun:
                    num3 = 0.9;
                    break;
                case ComponentType.WeaponTractorBeam:
                    num3 = 0.95;
                    break;
                case ComponentType.AssaultPod:
                    num3 = 1.0;
                    break;
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponSuperBeam:
                    num3 = 0.75;
                    break;
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperArea:
                    num3 = 0.8;
                    break;
            }
            if (num > num3)
            {
                num2 = (1.0 - num) / (1.0 - num3);
            }
            ImageAttributes imageAttributes = null;
            if (num2 < 1.0)
            {
                imageAttributes = method_236(num2);
            }
            switch (component.Type)
            {
                case ComponentType.WeaponTorpedo:
                case ComponentType.WeaponMissile:
                    {
                        float num4 = (float)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                        float num5 = (float)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                        int num11 = -1;
                        double num12 = 0.0;
                        double num13 = weapon_0.Power;
                        double num14 = -1000.0;
                        if (weapon_0.BombardDamage > 0 && weapon_0.Target is Habitat)
                        {
                            num13 = (double)weapon_0.BombardDamage * 2.5;
                            num13 = Math.Min(num13, 60.0);
                            num11 = 0;
                            switch (component.ComponentID)
                            {
                                case 9:
                                    num11 = 1;
                                    num12 = 4.0840704496667311;
                                    break;
                                case 11:
                                    num14 = weapon_0.Heading;
                                    num14 *= -1.0;
                                    num11 = 4;
                                    num3 = 1.0;
                                    imageAttributes = method_236(1.0);
                                    break;
                                case 12:
                                    num14 = weapon_0.Heading;
                                    num14 *= -1.0;
                                    num11 = 5;
                                    num3 = 1.0;
                                    imageAttributes = method_236(1.0);
                                    break;
                            }
                        }
                        float val = (float)(num13 / 7.0) + 7f;
                        val = Math.Min(val, 18f);
                        float num9 = val / (float)main_0.double_0;
                        if (num9 < 1f)
                        {
                            num9 = 1f;
                        }
                        float num10 = num9;
                        float num8 = num4 - num9 / 2f;
                        float num7 = num5 - num10 / 2f;
                        double num15 = 1.0;
                        if (num11 < 0)
                        {
                            switch (weapon_0.Component.Type)
                            {
                                case ComponentType.WeaponTorpedo:
                                    num11 = ((weapon_0.Component.SpecialImageIndex >= 0 && weapon_0.Component.SpecialImageIndex < main_0.bitmap_12.Length) ? weapon_0.Component.SpecialImageIndex : 0);
                                    num12 = Math.PI;
                                    break;
                                case ComponentType.WeaponMissile:
                                    num11 = ((weapon_0.Component.SpecialImageIndex >= 0 && weapon_0.Component.SpecialImageIndex < main_0.bitmap_12.Length) ? weapon_0.Component.SpecialImageIndex : 0);
                                    num12 = 0.0;
                                    num14 = weapon_0.Heading;
                                    num14 *= -1.0;
                                    num15 = 3.5;
                                    break;
                            }
                        }
                        double totalSeconds = dateTime_5.Subtract(weapon_0.LastFired).TotalSeconds;
                        float float_ = (float)(totalSeconds * num12);
                        if (num14 > -1000.0)
                        {
                            float_ = (float)num14;
                        }
                        using (Bitmap bitmap2 = method_218(main_0.bitmap_12[num11], float_, GraphicsQuality.High))
                        {
                            double num16 = num15 * ((double)bitmap2.Width / (double)main_0.bitmap_12[num11].Width);
                            float num17 = (float)((double)num9 * num16);
                            float num18 = (float)((double)num10 * num16);
                            num8 += (num9 - num17) / 2f;
                            num7 += (num10 - num18) / 2f;
                            method_176(graphics_0);
                            if (num2 < 1.0 && imageAttributes != null)
                            {
                                graphics_0.DrawImage(bitmap2, new System.Drawing.Rectangle((int)num8, (int)num7, (int)num17, (int)num18), 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttributes);
                            }
                            else
                            {
                                graphics_0.DrawImage(bitmap2, new System.Drawing.Rectangle((int)num8, (int)num7, (int)num17, (int)num18), 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel);
                            }
                            method_177(graphics_0);
                        }
                        System.Drawing.Rectangle item2 = new System.Drawing.Rectangle((int)num8 - 1, (int)num7 - 1, (int)num9 + 2, (int)num10 + 2);
                        list_3.Add(item2);
                        break;
                    }
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponTractorBeam:
                case ComponentType.AssaultPod:
                case ComponentType.WeaponSuperBeam:
                case ComponentType.WeaponPhaser:
                case ComponentType.WeaponRailGun:
                    {
                        Bitmap bitmap_ = null;
                        switch (component.Type)
                        {
                            case ComponentType.WeaponBeam:
                            case ComponentType.WeaponPointDefense:
                            case ComponentType.WeaponIonCannon:
                            case ComponentType.WeaponSuperBeam:
                            case ComponentType.WeaponRailGun:
                                bitmap_ = ((component.SpecialImageIndex < 0 || component.SpecialImageIndex >= main_0.bitmap_13.Length) ? main_0.bitmap_13[0] : main_0.bitmap_13[component.SpecialImageIndex]);
                                break;
                            case ComponentType.AssaultPod:
                                bitmap_ = main_0.bitmap_15[0];
                                break;
                        }
                        int int_13 = 0;
                        int int_14 = 0;
                        if (component.Type == ComponentType.WeaponTractorBeam)
                        {
                            StellarObject target = weapon_0.Target;
                            if (target == null)
                            {
                                break;
                            }
                            double num19 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target.Xpos, target.Ypos);
                            if (num19 < (double)weapon_0.Range * 1.2)
                            {
                                method_177(graphics_0);
                                if (main_0.double_0 < 2.0)
                                {
                                    pen_7.Width = 3f;
                                    pen_8.Width = 1f;
                                }
                                else if (main_0.double_0 < 5.0)
                                {
                                    pen_7.Width = 2f;
                                    pen_8.Width = 1f;
                                }
                                else
                                {
                                    pen_7.Width = 1f;
                                    pen_8.Width = 0f;
                                }
                                int x = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                                int y = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                                double num20 = weapon_0.HeadingMissFactor;
                                if (!weapon_0.WillHitTarget)
                                {
                                    num20 *= 3.0;
                                }
                                double num21 = weapon_0.X;
                                double num22 = weapon_0.Y;
                                int x2 = (int)((num21 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                                int y2 = (int)((num22 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                                graphics_0.DrawLine(pen_7, x, y, x2, y2);
                                graphics_0.DrawLine(pen_8, x, y, x2, y2);
                                if (weapon_0.DistanceTravelled <= 3f && weapon_0.WillHitTarget && target is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)target;
                                    builtObject.LastTractorStrike = dateTime_5;
                                    builtObject.LastTractorStrikeDirection = weapon_0.Heading;
                                }
                            }
                            break;
                        }
                        if (component.Type == ComponentType.WeaponPhaser)
                        {
                            StellarObject target2 = weapon_0.Target;
                            if (target2 == null)
                            {
                                break;
                            }
                            double num23 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target2.Xpos, target2.Ypos);
                            if (!(num23 < (double)weapon_0.Range * 1.2))
                            {
                                break;
                            }
                            method_177(graphics_0);
                            if (main_0.double_0 < 2.0)
                            {
                                pen_5.Width = 3f;
                                pen_6.Width = 1f;
                            }
                            else if (main_0.double_0 < 5.0)
                            {
                                pen_5.Width = 2f;
                                pen_6.Width = 1f;
                            }
                            else
                            {
                                pen_5.Width = 1f;
                                pen_6.Width = 0f;
                            }
                            int x3 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                            int y3 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                            double num24 = weapon_0.HeadingMissFactor;
                            if (!weapon_0.WillHitTarget)
                            {
                                num24 *= 3.0;
                            }
                            double num25 = target2.Xpos + num24 * num23;
                            double num26 = target2.Ypos + num24 * num23;
                            int x4 = (int)((num25 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                            int y4 = (int)((num26 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                            graphics_0.DrawLine(pen_5, x3, y3, x4, y4);
                            graphics_0.DrawLine(pen_6, x3, y3, x4, y4);
                            if (weapon_0.DistanceTravelled <= 3f && weapon_0.WillHitTarget && target2 is BuiltObject)
                            {
                                BuiltObject builtObject2 = (BuiltObject)target2;
                                if (builtObject2.CurrentShields <= 5f)
                                {
                                    int val2 = (int)(Math.Sqrt(weapon_0.Power) * 10.0);
                                    val2 = Math.Min(35, val2);
                                    double rotationAngle = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                                    double num27 = 0.0;
                                    double num28 = 0.0;
                                    DistantWorlds.Animation animation = new DistantWorlds.Animation(main_0.bitmap_212, galaxy_0.CurrentDateTime, 100, target2.Xpos + num27, target2.Ypos + num28, val2, val2, rotationAngle, System.Drawing.Color.Empty);
                                    animationSystem_1.AddAnimation(animation);
                                }
                                else
                                {
                                    builtObject2.LastShieldStrike = dateTime_5;
                                    builtObject2.LastShieldStrikeDirection = weapon_0.Heading;
                                }
                            }
                            break;
                        }
                        if (component.Type == ComponentType.AssaultPod)
                        {
                            using (Bitmap bitmap3 = method_163(bitmap_, stellarObject_0, weapon_0, out int_13, out int_14, bool_13: true))
                            {
                                method_175(graphics_0);
                                graphics_0.DrawImage(bitmap3, new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height), 0, 0, bitmap3.Width, bitmap3.Height, GraphicsUnit.Pixel);
                                method_177(graphics_0);
                                System.Drawing.Rectangle item2 = new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height);
                                list_3.Add(item2);
                            }
                            break;
                        }
                        using (Bitmap bitmap4 = method_162(bitmap_, stellarObject_0, weapon_0, out int_13, out int_14))
                        {
                            method_175(graphics_0);
                            if (num2 < 1.0 && imageAttributes != null)
                            {
                                graphics_0.DrawImage(bitmap4, new System.Drawing.Rectangle(int_13, int_14, bitmap4.Width, bitmap4.Height), 0, 0, bitmap4.Width, bitmap4.Height, GraphicsUnit.Pixel, imageAttributes);
                            }
                            else
                            {
                                graphics_0.DrawImage(bitmap4, new System.Drawing.Rectangle(int_13, int_14, bitmap4.Width, bitmap4.Height), 0, 0, bitmap4.Width, bitmap4.Height, GraphicsUnit.Pixel);
                            }
                            method_177(graphics_0);
                            System.Drawing.Rectangle item2 = new System.Drawing.Rectangle(int_13, int_14, bitmap4.Width, bitmap4.Height);
                            list_3.Add(item2);
                        }
                        break;
                    }
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperArea:
                    {
                        Bitmap bitmap = null;
                        switch (component.Type)
                        {
                            case ComponentType.WeaponAreaDestruction:
                                bitmap = ((weapon_0.Component.ComponentID != 19) ? main_0.edLqkLkgAx[2] : main_0.edLqkLkgAx[0]);
                                break;
                            case ComponentType.WeaponSuperArea:
                                bitmap = main_0.edLqkLkgAx[1];
                                break;
                            case ComponentType.WeaponIonPulse:
                                bitmap = main_0.edLqkLkgAx[3];
                                break;
                        }
                        float num4 = (float)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                        float num5 = (float)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                        float num6 = (float)((double)weapon_0.DistanceTravelled / main_0.double_0);
                        float num7 = num5 - num6;
                        float num8 = num4 - num6;
                        float num9 = (float)((double)num6 * 2.0);
                        float num10 = (float)((double)num6 * 2.0);
                        method_175(graphics_0);
                        System.Drawing.Rectangle destRect = new System.Drawing.Rectangle((int)num8, (int)num7, (int)num9, (int)num10);
                        if (num2 < 1.0 && imageAttributes != null)
                        {
                            graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
                        }
                        else
                        {
                            graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
                        }
                        System.Drawing.Rectangle item = new System.Drawing.Rectangle((int)num8 - 2, (int)num7 - 2, (int)num9 + 4, (int)num10 + 4);
                        list_3.Add(item);
                        break;
                    }
            }
            imageAttributes?.Dispose();
        }

        private void method_171(SpriteBatch spriteBatch_2, Weapon weapon_0, StellarObject stellarObject_0, DateTime dateTime_5, int int_11, int int_12)
        {
            if (!(weapon_0.DistanceTravelled >= 0f))
            {
                return;
            }
            if (!weapon_0.SoundEffectPlayed)
            {
                double double_ = 0.0;
                double double_2 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                weapon_0.SoundEffectPlayed = true;
                main_0.method_0(main_0.EffectsPlayer.ResolveWeapon(weapon_0.Component, double_, double_2));
            }
            Component component = weapon_0.Component;
            if (weapon_0.Power == float.MaxValue)
            {
                int num = Galaxy.Rnd.Next(0, texture2D_8.Length);
                int num2 = Galaxy.Rnd.Next(10, 16);
                DistantWorlds.Animation animation = new DistantWorlds.Animation(texture2D_8[num], dateTime_5, 20, int_11, int_12, num2, num2);
                animation.DisposeTexturesWhenComplete = false;
                animationSystem_1.AddAnimation(animation);
                return;
            }
            System.Drawing.Color.FromArgb(255, 255, 255);
            System.Drawing.Color.FromArgb(255, 255, 255);
            double num3 = Math.Min(1.0, (double)weapon_0.DistanceTravelled / (double)weapon_0.Range);
            double num4 = 1.0;
            double num5 = 0.6;
            switch (component.Type)
            {
                case ComponentType.WeaponPhaser:
                case ComponentType.WeaponSuperPhaser:
                    num5 = 0.97;
                    break;
                case ComponentType.WeaponTorpedo:
                case ComponentType.WeaponSuperTorpedo:
                    num5 = 0.6;
                    break;
                case ComponentType.WeaponBombard:
                case ComponentType.WeaponMissile:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponRailGun:
                case ComponentType.WeaponSuperMissile:
                case ComponentType.WeaponSuperRailGun:
                    num5 = 0.9;
                    break;
                case ComponentType.WeaponTractorBeam:
                case ComponentType.WeaponGravityBeam:
                    num5 = 0.95;
                    break;
                case ComponentType.WeaponAreaGravity:
                    num5 = 0.9;
                    break;
                case ComponentType.AssaultPod:
                    num5 = 1.0;
                    break;
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponSuperBeam:
                    num5 = 0.75;
                    break;
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperArea:
                    num5 = 0.8;
                    break;
            }
            if (num3 > num5)
            {
                num4 = (1.0 - num3) / (1.0 - num5);
            }
            System.Drawing.Color color = System.Drawing.Color.FromArgb((int)(num4 * 255.0), 255, 255, 255);
            switch (component.Type)
            {
                case ComponentType.WeaponTorpedo:
                case ComponentType.WeaponBombard:
                case ComponentType.WeaponMissile:
                case ComponentType.WeaponSuperTorpedo:
                case ComponentType.WeaponSuperMissile:
                    {
                        float num9 = (float)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                        float num10 = (float)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                        int num43 = -1;
                        double num44 = 0.0;
                        double num45 = weapon_0.Power;
                        double num46 = -1000.0;
                        if (weapon_0.BombardDamage > 0 && weapon_0.Target is Habitat)
                        {
                            num45 = (double)weapon_0.BombardDamage * 2.5;
                            num45 = Math.Min(num45, 60.0);
                            num43 = 0;
                            num43 = ((component.SpecialImageIndex >= 0 && component.SpecialImageIndex < texture2D_1.Length) ? component.SpecialImageIndex : 0);
                            switch (component.Type)
                            {
                                default:
                                    num46 = weapon_0.Heading;
                                    num5 = 1.0;
                                    color = System.Drawing.Color.White;
                                    break;
                                case ComponentType.WeaponBeam:
                                case ComponentType.WeaponTorpedo:
                                case ComponentType.WeaponSuperTorpedo:
                                    num44 = 4.0840704496667311;
                                    break;
                            }
                        }
                        float num47 = 10f;
                        float val4 = 18f;
                        if (component.Type == ComponentType.WeaponMissile)
                        {
                            num47 = (float)(num45 / 5.0) + 5f;
                        }
                        else if (component.Type == ComponentType.WeaponSuperMissile)
                        {
                            num47 = (float)(num45 / 10.0) + 5f;
                            val4 = 38f;
                        }
                        else if (component.Type == ComponentType.WeaponSuperTorpedo)
                        {
                            num47 = (float)(num45 / 10.0) + 5f;
                            val4 = 48f;
                        }
                        else if (component.Type == ComponentType.WeaponBombard)
                        {
                            num47 = (float)(num45 / 0.5) + 6f;
                            val4 = 26f;
                        }
                        else
                        {
                            num47 = (float)(num45 / 3.0) + 7f;
                        }
                        num47 = Math.Min(num47, val4);
                        float num14 = num47 / (float)main_0.double_0;
                        if (num14 < 1f)
                        {
                            num14 = 1f;
                        }
                        float num15 = num14;
                        float num13 = num9 - num14 / 2f;
                        float num12 = num10 - num15 / 2f;
                        double num48 = 1.0;
                        if (num43 < 0)
                        {
                            num43 = ((weapon_0.Component.SpecialImageIndex >= 0 && weapon_0.Component.SpecialImageIndex < texture2D_1.Length) ? weapon_0.Component.SpecialImageIndex : 0);
                            switch (weapon_0.Component.Type)
                            {
                                case ComponentType.WeaponTorpedo:
                                case ComponentType.WeaponSuperTorpedo:
                                    num44 = Math.PI;
                                    break;
                                case ComponentType.WeaponBombard:
                                    num44 = 0.0;
                                    num46 = weapon_0.Heading;
                                    num48 = 3.5;
                                    break;
                                case ComponentType.WeaponMissile:
                                case ComponentType.WeaponSuperMissile:
                                    num44 = 0.0;
                                    num46 = weapon_0.Heading;
                                    num48 = 3.5;
                                    break;
                            }
                        }
                        double totalSeconds = dateTime_5.Subtract(weapon_0.LastFired).TotalSeconds;
                        float rotationAngle2 = (float)(totalSeconds * num44);
                        if (num46 > -1000.0)
                        {
                            rotationAngle2 = (float)num46;
                        }
                        Texture2D texture2D3 = texture2D_1[num43];
                        num48 *= (double)(num14 / (float)texture2D3.Width);
                        XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D3, num9, num10, rotationAngle2, (float)num48, color, offsetPositionByCenter: false);
                        System.Drawing.Rectangle item2 = new System.Drawing.Rectangle((int)num13 - 1, (int)num12 - 1, (int)num14 + 2, (int)num15 + 2);
                        list_3.Add(item2);
                        break;
                    }
                case ComponentType.WeaponAreaGravity:
                    {
                        float num36 = weapon_0.DistanceTravelled / (float)weapon_0.Range;
                        int val3 = 255;
                        if (num36 < 0.02f)
                        {
                            val3 = (int)(num36 * 50f * 255f);
                        }
                        else if (num36 > 0.9f)
                        {
                            val3 = (int)((0.1f - (num36 - 0.9f)) * 10f * 255f);
                        }
                        val3 = Math.Max(0, Math.Min(255, val3));
                        int num37 = 5;
                        num37 = ((main_0.double_0 < 2.0) ? 5 : ((!(main_0.double_0 < 5.0)) ? 1 : 3));
                        int x3 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int y3 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        int num38 = (int)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int num39 = (int)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        int alpha = (int)((double)val3 * 0.6);
                        System.Drawing.Color color6 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(alpha, 0, 176, 164), System.Drawing.Color.FromArgb(alpha, 0, 64, 56), dateTime_5);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, x3, y3, num38, num39, color6, num37);
                        GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(alpha, 0, 255, 240), System.Drawing.Color.FromArgb(alpha, 0, 96, 88), dateTime_5);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, x3, y3, num38, num39, color6, 1);
                        int num40 = (int)((double)weapon_0.DamageLoss * 2.0 / main_0.double_0);
                        int int_15 = num38 - num40 / 2;
                        int int_16 = num39 - num40 / 2;
                        System.Drawing.Color color_ = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(val3, 0, 0, 108), System.Drawing.Color.FromArgb(val3, 54, 0, 108), dateTime_5);
                        double num41 = (double)dateTime_5.Second + (double)dateTime_5.Millisecond / 1000.0;
                        double double_3 = Math.PI / 10.0 * (num41 % 20.0);
                        method_118(spriteBatch_2, int_15, int_16, num40, num40, texture2D_12[0], dateTime_5, double_3, color_);
                        int num42 = (int)((double)weapon_0.BombardDamage * 2.0 / main_0.double_0);
                        int int_17 = num38 - num42 / 2;
                        int int_18 = num39 - num42 / 2;
                        method_117(spriteBatch_2, int_17, int_18, num42, num42, texture2D_13, dateTime_5, 25, 0.0, 0.0, System.Drawing.Color.FromArgb(val3, 255, 255, 255));
                        break;
                    }
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponTractorBeam:
                case ComponentType.WeaponGravityBeam:
                case ComponentType.AssaultPod:
                case ComponentType.WeaponSuperBeam:
                case ComponentType.WeaponPhaser:
                case ComponentType.WeaponRailGun:
                case ComponentType.WeaponSuperPhaser:
                case ComponentType.WeaponSuperRailGun:
                    {
                        Texture2D texture2D2 = null;
                        switch (component.Type)
                        {
                            case ComponentType.AssaultPod:
                                texture2D2 = texture2D_5[0];
                                break;
                            case ComponentType.WeaponBeam:
                            case ComponentType.WeaponPointDefense:
                            case ComponentType.WeaponIonCannon:
                            case ComponentType.WeaponTractorBeam:
                            case ComponentType.WeaponGravityBeam:
                            case ComponentType.WeaponSuperBeam:
                            case ComponentType.WeaponPhaser:
                            case ComponentType.WeaponRailGun:
                            case ComponentType.WeaponSuperPhaser:
                            case ComponentType.WeaponSuperRailGun:
                                texture2D2 = ((component.SpecialImageIndex < 0 || component.SpecialImageIndex >= texture2D_2.Length) ? texture2D_2[0] : texture2D_2[component.SpecialImageIndex]);
                                break;
                        }
                        int int_13 = 0;
                        int int_14 = 0;
                        if (component.Type == ComponentType.WeaponGravityBeam)
                        {
                            StellarObject target = weapon_0.Target;
                            if (target == null)
                            {
                                break;
                            }
                            double num16 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target.Xpos, target.Ypos);
                            if (num16 < (double)weapon_0.Range * 1.2)
                            {
                                int num17 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                                int num18 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                                double num19 = weapon_0.HeadingMissFactor;
                                if (!weapon_0.WillHitTarget)
                                {
                                    num19 *= 3.0;
                                }
                                float firingAngle = (float)Galaxy.DetermineAngle(stellarObject_0.Xpos, stellarObject_0.Ypos, weapon_0.X, weapon_0.Y);
                                _ = base.Width / 2;
                                _ = base.Height / 2;
                                float num20 = (float)(num16 / main_0.double_0 / (double)texture2D2.Width);
                                float num21 = (float)(1.0 / main_0.double_0);
                                SizeF scaleFactor = new SizeF(num20, num21);
                                System.Drawing.Color color3 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(144, 255, 255, 255), System.Drawing.Color.FromArgb(255, 255, 255, 255), dateTime_5);
                                XnaDrawingHelper.DrawTextureStretchedBeam(spriteBatch_2, texture2D2, num17, num18, firingAngle, scaleFactor, color3);
                            }
                            break;
                        }
                        if (component.Type == ComponentType.WeaponTractorBeam)
                        {
                            StellarObject target2 = weapon_0.Target;
                            if (target2 == null)
                            {
                                break;
                            }
                            double num22 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target2.Xpos, target2.Ypos);
                            if (num22 < (double)weapon_0.Range * 1.2)
                            {
                                int num23 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                                int num24 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                                double num25 = weapon_0.HeadingMissFactor;
                                if (!weapon_0.WillHitTarget)
                                {
                                    num25 *= 3.0;
                                }
                                float firingAngle2 = (float)Galaxy.DetermineAngle(stellarObject_0.Xpos, stellarObject_0.Ypos, target2.Xpos, target2.Ypos);
                                _ = base.Width / 2;
                                _ = base.Height / 2;
                                float num26 = (float)(num22 / main_0.double_0 / (double)texture2D2.Width);
                                float num27 = (float)(1.0 / main_0.double_0);
                                SizeF scaleFactor2 = new SizeF(num26, num27);
                                System.Drawing.Color color4 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(144, 255, 255, 255), System.Drawing.Color.FromArgb(255, 255, 255, 255), dateTime_5);
                                XnaDrawingHelper.DrawTextureStretchedBeam(spriteBatch_2, texture2D2, num23, num24, firingAngle2, scaleFactor2, color4);
                                if (weapon_0.DistanceTravelled <= 3f && weapon_0.WillHitTarget && target2 is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)target2;
                                    builtObject.LastTractorStrike = dateTime_5;
                                    builtObject.LastTractorStrikeDirection = weapon_0.Heading;
                                }
                            }
                            break;
                        }
                        if (component.Type != ComponentType.WeaponPhaser && component.Type != ComponentType.WeaponSuperPhaser)
                        {
                            if (component.Type == ComponentType.AssaultPod)
                            {
                                float float_ = 1f;
                                float float_2 = 1f;
                                float float_3 = 0f;
                                method_160(texture2D2, stellarObject_0, weapon_0, out int_13, out int_14, out float_, out float_2, out float_3, bool_13: true);
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D2, int_13, int_14, float_3, new SizeF(float_2, float_), color, offsetPositionByCenter: false);
                            }
                            else
                            {
                                float float_4 = 1f;
                                float float_5 = 1f;
                                float float_6 = 0f;
                                method_159(texture2D2, stellarObject_0, weapon_0, out int_13, out int_14, out float_4, out float_5, out float_6);
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D2, int_13, int_14, float_6, new SizeF(float_5, float_4), color, offsetPositionByCenter: false);
                            }
                            break;
                        }
                        StellarObject target3 = weapon_0.Target;
                        if (target3 == null)
                        {
                            break;
                        }
                        double num28 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target3.Xpos, target3.Ypos);
                        if (!(num28 < (double)weapon_0.Range * 1.2))
                        {
                            break;
                        }
                        int num29 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int num30 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        double num31 = weapon_0.HeadingMissFactor;
                        if (!weapon_0.WillHitTarget)
                        {
                            num31 *= 3.0;
                        }
                        float firingAngle3 = (float)Galaxy.DetermineAngle(stellarObject_0.Xpos, stellarObject_0.Ypos, target3.Xpos, target3.Ypos);
                        _ = base.Width / 2;
                        _ = base.Height / 2;
                        float num32 = (float)(num28 / main_0.double_0 / (double)texture2D2.Width);
                        float num33 = (float)(1.0 / main_0.double_0);
                        SizeF scaleFactor3 = new SizeF(num32, num33);
                        System.Drawing.Color color5 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(144, 255, 255, 255), System.Drawing.Color.FromArgb(255, 255, 255, 255), dateTime_5);
                        XnaDrawingHelper.DrawTextureStretchedBeam(spriteBatch_2, texture2D2, num29, num30, firingAngle3, scaleFactor3, color5);
                        if (weapon_0.DistanceTravelled <= 3f && weapon_0.WillHitTarget && target3 is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)target3;
                            if (builtObject2.CurrentShields <= 5f)
                            {
                                int val2 = (int)(Math.Sqrt(weapon_0.Power) * 10.0);
                                val2 = Math.Min(35, val2);
                                double rotationAngle = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                                double num34 = 0.0;
                                double num35 = 0.0;
                                DistantWorlds.Animation animation2 = new DistantWorlds.Animation(texture2D_33, galaxy_0.CurrentDateTime, 100, target3.Xpos + num34, target3.Ypos + num35, val2, val2, rotationAngle, System.Drawing.Color.Empty);
                                animation2.DisposeTexturesWhenComplete = false;
                                animationSystem_1.AddAnimation(animation2);
                            }
                            else
                            {
                                builtObject2.LastShieldStrike = dateTime_5;
                                builtObject2.LastShieldStrikeDirection = weapon_0.Heading;
                            }
                        }
                        break;
                    }
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperArea:
                    {
                        Texture2D texture2D = null;
                        int num6 = 0;
                        num6 = ((component.SpecialImageIndex >= 0 && component.SpecialImageIndex < texture2D_3.Length) ? component.SpecialImageIndex : 0);
                        texture2D = texture2D_3[num6];
                        float num7 = weapon_0.DistanceTravelled / (float)weapon_0.Range;
                        int val = 255;
                        if (num7 < 0.02f)
                        {
                            val = (int)(num7 * 50f * 255f);
                        }
                        else if (num7 > 0.9f)
                        {
                            val = (int)((0.1f - (num7 - 0.9f)) * 10f * 255f);
                        }
                        val = Math.Max(0, Math.Min(255, val));
                        int num8 = 5;
                        num8 = ((main_0.double_0 < 2.0) ? 5 : ((!(main_0.double_0 < 5.0)) ? 1 : 3));
                        int x = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int y = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        int x2 = (int)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int y2 = (int)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        System.Drawing.Color color2 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(val, 144, 0, 176), System.Drawing.Color.FromArgb(val, 48, 0, 64), dateTime_5);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, x, y, x2, y2, color2, num8);
                        GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(val, 232, 0, 255), System.Drawing.Color.FromArgb(val, 80, 0, 96), dateTime_5);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, x, y, x2, y2, color2, 1);
                        float num9 = (float)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                        float num10 = (float)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                        float num11 = (float)((double)weapon_0.DistanceTravelled / main_0.double_0);
                        float num12 = num10 - num11;
                        float num13 = num9 - num11;
                        float num14 = (float)((double)num11 * 2.0);
                        float num15 = (float)((double)num11 * 2.0);
                        XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle((int)num13, (int)num12, (int)num14, (int)num15), spriteBatch: spriteBatch_0, texture: texture2D, rotationAngle: 0f, tintColor: color);
                        System.Drawing.Rectangle item = new System.Drawing.Rectangle((int)num13 - 2, (int)num12 - 2, (int)num14 + 4, (int)num15 + 4);
                        list_3.Add(item);
                        break;
                    }
            }
        }

        private void method_172(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, DateTime dateTime_5)
        {
            if (habitat_1 != null && habitat_1.PlanetaryShieldPresent)
            {
                double num = 0.0;
                int second = dateTime_5.Second;
                int num2 = dateTime_5.Millisecond;
                if (second % 2 == 1)
                {
                    num2 += 1000;
                }
                num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
                float num3 = 0.07f + (float)(num / 35.0);
                int num4 = (int)(26.0 / main_0.double_0);
                int_11 -= num4;
                int_12 -= num4;
                int_13 += num4 * 2;
                int_14 += num4 * 2;
                using ImageAttributes imageAttr = method_236(num3);
                Bitmap bitmap = main_0.bitmap_18[0];
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
                graphics_0.DrawImage(bitmap, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttr);
            }
        }

        private void method_173(SpriteBatch spriteBatch_2, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, DateTime dateTime_5)
        {
            if (habitat_1 != null && habitat_1.PlanetaryShieldPresent)
            {
                double num = 0.0;
                int second = dateTime_5.Second;
                int num2 = dateTime_5.Millisecond;
                if (second % 2 == 1)
                {
                    num2 += 1000;
                }
                num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
                float num3 = 0.6f + (float)(num * 0.20000000298023224);
                int num4 = (int)(26.0 / main_0.double_0);
                int_11 -= num4;
                int_12 -= num4;
                int_13 += num4 * 2;
                int_14 += num4 * 2;
                System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
                System.Drawing.Color tintColor = System.Drawing.Color.FromArgb((int)(num3 * 255f), 255, 255, 255);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_7[0], destination, 0f, tintColor);
            }
        }

        public void DrawCreatureToMain(Creature creature, Bitmap ToDraw, int x, int y, Graphics graphics, double zoomFactor, int maxWidth, int preRotatedWidth, int preRotatedHeight)
        {
            float num = (float)maxWidth / (float)preRotatedWidth;
            float num2 = (float)ToDraw.Width * num;
            float num3 = (float)ToDraw.Height * num;
            float num4 = (num2 - (float)preRotatedWidth) / 2f;
            float num5 = (num3 - (float)preRotatedHeight) / 2f;
            num4 *= num;
            num5 *= num;
            RectangleF destRect = new RectangleF(x, y, num2, num3);
            RectangleF srcRect = new RectangleF(0f, 0f, ToDraw.Width, ToDraw.Height);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.DrawImage(ToDraw, destRect, srcRect, GraphicsUnit.Pixel);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
        }

        public void DrawCreatureToMainXna(SpriteBatch spriteBatch, Creature creature, Texture2D ToDraw, int x, int y, int drawWidth, int drawHeight, float heading)
        {
            XnaDrawingHelper.DrawTexture(spriteBatch, ToDraw, x, y, drawWidth, drawHeight, heading);
        }

        public void DrawFighterToMain(Bitmap ToDraw, int x, int y, Graphics graphics)
        {
            graphics.DrawImageUnscaled(ToDraw, x, y);
        }

        public void DrawFighterToMainXna(SpriteBatch spriteBatch, Texture2D ToDraw, Fighter fighter, System.Drawing.Rectangle destination)
        {
            float rotationAngle = fighter.Heading;
            XnaDrawingHelper.DrawTexture(spriteBatch, ToDraw, destination, rotationAngle);
        }

        public void DrawBuiltObjectToMain(Bitmap ToDraw, int x, int y, Graphics graphics)
        {
            graphics.DrawImageUnscaled(ToDraw, x, y);
        }

        public void DrawBuiltObjectToMainXna(SpriteBatch spriteBatch, Texture2D ToDraw, BuiltObject builtObject, System.Drawing.Rectangle destination, bool fadeCivilianShips)
        {
            float rotationAngle = builtObject.Heading;
            System.Drawing.Color tintColor = System.Drawing.Color.White;
            if (fadeCivilianShips && builtObject.Owner == null)
            {
                tintColor = System.Drawing.Color.FromArgb(144, 255, 255, 255);
            }
            XnaDrawingHelper.DrawTexture(spriteBatch, ToDraw, destination, rotationAngle, tintColor);
        }

        private RectangleF method_174(Bitmap bitmap_7, double double_15)
        {
            float num = 0f;
            float num2 = 0f;
            float num3 = (float)bitmap_7.Width * (float)double_15;
            float num4 = (float)bitmap_7.Height * (float)double_15;
            return new RectangleF(num, num2, num3, num4);
        }

        public void DrawGasCloudToMain(Bitmap gasCloudImage, Habitat gasCloud, int x, int y, int width, int height, Graphics graphics)
        {
            method_136(gasCloud);
            if (bitmap_2 == null)
            {
                return;
            }
            RectangleF rectangleF_ = method_174(bitmap_2, 1.0);
            RectangleF rectangleF = method_137(gasCloud, rectangleF_);
            if (rectangleF != System.Drawing.Rectangle.Empty)
            {
                float num = 0f;
                if (rectangleF.X > 0f)
                {
                    num = rectangleF.X * (float)double_3;
                }
                float num2 = 0f;
                if (rectangleF.Y > 0f)
                {
                    num2 = rectangleF.Y * (float)double_3;
                }
                float num3 = rectangleF.Width * (float)double_3;
                float num4 = rectangleF.Height * (float)double_3;
                rectangleF = new RectangleF(num, num2, num3, num4);
                rectangleF.Offset(rectangleF_0.X * -1f, rectangleF_0.Y * -1f);
                RectangleF destRect = method_140(gasCloud, bitmap_2);
                method_176(graphics);
                graphics.DrawImage(bitmap_3, destRect, rectangleF, GraphicsUnit.Pixel);
                method_177(graphics);
            }
        }

        public void DrawGasCloudToMainXna(SpriteBatch spriteBatch, Bitmap gasCloudImage, Habitat gasCloud, int x, int y, int width, int height)
        {
            method_136(gasCloud);
            if (bitmap_2 == null || texture2D_56 == null)
            {
                return;
            }
            RectangleF rectangleF_ = method_174(bitmap_2, 1.0);
            RectangleF rectangleF = method_137(gasCloud, rectangleF_);
            if (rectangleF != System.Drawing.Rectangle.Empty)
            {
                float num = 0f;
                if (rectangleF.X > 0f)
                {
                    num = rectangleF.X * (float)double_3;
                }
                float num2 = 0f;
                if (rectangleF.Y > 0f)
                {
                    num2 = rectangleF.Y * (float)double_3;
                }
                float num3 = rectangleF.Width * (float)double_3;
                float num4 = rectangleF.Height * (float)double_3;
                rectangleF = new RectangleF(num, num2, num3, num4);
                rectangleF.Offset(rectangleF_0.X * -1f, rectangleF_0.Y * -1f);
                RectangleF rectangleF2 = method_140(gasCloud, bitmap_2);
                XnaDrawingHelper.DrawTexture(source: new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), destination: new System.Drawing.Rectangle((int)rectangleF2.X, (int)rectangleF2.Y, (int)rectangleF2.Width, (int)rectangleF2.Height), spriteBatch: spriteBatch, texture: texture2D_57);
            }
        }

        public void DrawHabToMain(Bitmap ToDraw, Bitmap rings, int x, int y, int width, int height, Graphics graphics, Habitat habitat)
        {
            int num = x;
            int num2 = y;
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            int num3 = width;
            int num4 = height;
            num3 = (int)((double)num3 / main_0.double_0);
            num4 = (int)((double)num4 / main_0.double_0);
            if (x + num3 > base.Width)
            {
                num3 = base.Width;
            }
            if (y + num4 > base.Height)
            {
                num4 = base.Height;
            }
            method_175(graphics);
            if (ToDraw != null)
            {
                graphics.DrawImageUnscaledAndClipped(ToDraw, new System.Drawing.Rectangle(num, num2, ToDraw.Width, ToDraw.Height));
            }
            method_177(graphics);
        }

        private void method_175(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics_0.SmoothingMode = SmoothingMode.None;
            graphics_0.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
        }

        private void method_176(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            graphics_0.InterpolationMode = InterpolationMode.Low;
            graphics_0.SmoothingMode = SmoothingMode.None;
            graphics_0.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics_0.PixelOffsetMode = PixelOffsetMode.None;
        }

        private void method_177(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        public System.Drawing.Color ResolveShipSymbolColor(BuiltObject builtObject)
        {
            System.Drawing.Color result = System.Drawing.Color.Gray;
            bool flag = true;
            if (builtObject.Empire == null || (builtObject.Empire == galaxy_0.IndependentEmpire && (builtObject.PirateEmpireId <= 0 || builtObject.PirateEmpireId != galaxy_0.PlayerEmpire.EmpireId)))
            {
                flag = false;
            }
            if (flag && builtObject.Empire != null)
            {
                result = builtObject.Empire.MainColor;
                double val = Math.Max(0.6, main_0.double_0 / 3.0);
                val = Math.Min(1.0, val);
                if (builtObject.Empire.PirateEmpireBaseHabitat != null && builtObject.Empire.MainColor == System.Drawing.Color.FromArgb(1, 1, 1))
                {
                    result = System.Drawing.Color.FromArgb(48, 48, 48);
                }
                result = System.Drawing.Color.FromArgb((int)(val * 255.0), result.R, result.G, result.B);
            }
            return result;
        }

        public void DrawShipSymbol(Graphics graphics, BuiltObject builtObject, System.Drawing.Color color, float x, float y, float width, float height, float originalWidth, float originalHeight, bool fillInterior, double zoomFactor)
        {
            Pen pen = new Pen(color, 2f);
            SolidBrush solidBrush = new SolidBrush(color);
            if (fillInterior)
            {
                System.Drawing.Color color2 = method_226(color, 64);
                pen = new Pen(color2, 2f);
            }
            if (zoomFactor <= 50.0 && builtObject.Owner == null && builtObject.Empire != galaxy_0.IndependentEmpire && builtObject.Empire != null)
            {
                pen.DashStyle = DashStyle.Dot;
            }
            List<PointF> list = new List<PointF>();
            float num = 1.2f;
            if (builtObject.Role == BuiltObjectRole.Military)
            {
                num = 1.5f;
            }
            else if (builtObject.Role == BuiltObjectRole.Exploration || builtObject.Role == BuiltObjectRole.Colony)
            {
                num = 1.5f;
            }
            float num2 = originalWidth * num;
            float num3 = originalHeight * num;
            float num4 = x - (num2 - width) / 2f;
            float num5 = y - (num3 - height) / 2f;
            x = num4;
            y = num5;
            width = num2;
            height = num3;
            if (width > height)
            {
                y -= (width - height) / 2f;
                height = width;
            }
            switch (builtObject.Role)
            {
                case BuiltObjectRole.Military:
                    {
                        float num24 = width / 1.15470052f;
                        list.Add(new PointF(x + width / 2f, y));
                        list.Add(new PointF(x + width, y + num24));
                        list.Add(new PointF(x, y + num24));
                        if (fillInterior)
                        {
                            method_239(graphics, solidBrush, list.ToArray(), width);
                        }
                        method_238(graphics, pen, list.ToArray(), width);
                        break;
                    }
                case BuiltObjectRole.Freight:
                    if (fillInterior)
                    {
                        method_243(graphics, solidBrush, new RectangleF(x, y, width, height));
                    }
                    method_241(graphics, pen, new RectangleF(x, y, width, height));
                    break;
                case BuiltObjectRole.Passenger:
                    {
                        if (fillInterior)
                        {
                            method_243(graphics, solidBrush, new RectangleF(x, y, width, height));
                        }
                        method_241(graphics, pen, new RectangleF(x, y, width, height));
                        float num21 = width / 5f;
                        float num22 = x + width / 2f;
                        float num23 = y + height / 2f;
                        graphics.DrawLine(pen, num22, y - num21, num22, y);
                        graphics.DrawLine(pen, x + width, num23, x + width + num21, num23);
                        graphics.DrawLine(pen, num22, y + height + num21, num22, y + height);
                        graphics.DrawLine(pen, x - num21, num23, x, num23);
                        break;
                    }
                case BuiltObjectRole.Exploration:
                case BuiltObjectRole.Colony:
                    list.Add(new PointF(x + width / 2f, y));
                    list.Add(new PointF(x + width, y + height / 2f));
                    list.Add(new PointF(x + width / 2f, y + height));
                    list.Add(new PointF(x, y + height / 2f));
                    if (fillInterior)
                    {
                        method_239(graphics, solidBrush, list.ToArray(), width);
                    }
                    method_238(graphics, pen, list.ToArray(), width);
                    break;
                case BuiltObjectRole.Build:
                    if (fillInterior)
                    {
                        graphics.FillRectangle(solidBrush, new RectangleF(x, y, width, height));
                    }
                    graphics.DrawRectangle(pen, x, y, width, height);
                    break;
                case BuiltObjectRole.Resource:
                    {
                        if (fillInterior)
                        {
                            method_243(graphics, solidBrush, new RectangleF(x, y, width, height));
                        }
                        method_241(graphics, pen, new RectangleF(x, y, width, height));
                        float num19 = 0.707106769f;
                        float num20 = width / 2f - width / 2f * num19;
                        graphics.DrawLine(pen, x + num20, y + num20, x, y);
                        graphics.DrawLine(pen, x + width - num20, y + num20, x + width, y);
                        graphics.DrawLine(pen, x + width - num20, y + height - num20, x + width, y + height);
                        graphics.DrawLine(pen, x + num20, y + height - num20, x, y + height);
                        break;
                    }
                case BuiltObjectRole.Base:
                    {
                        float num6 = height * 1.15470052f;
                        x -= height * 0.154700533f / 2f;
                        float num7 = num6 / 2f;
                        float num8 = (num6 - num7) / 2f;
                        list.Add(new PointF(x + num8, y));
                        list.Add(new PointF(x + num8 + num7, y));
                        list.Add(new PointF(x + num8 + num7 + num8, y + height / 2f));
                        list.Add(new PointF(x + num8 + num7, y + height));
                        list.Add(new PointF(x + num8, y + height));
                        list.Add(new PointF(x, y + height / 2f));
                        if (builtObject.SubRole != BuiltObjectSubRole.MiningStation && builtObject.SubRole != BuiltObjectSubRole.GasMiningStation)
                        {
                            if (fillInterior)
                            {
                                method_239(graphics, solidBrush, list.ToArray(), width);
                            }
                            method_238(graphics, pen, list.ToArray(), width);
                            break;
                        }
                        if (fillInterior)
                        {
                            method_239(graphics, solidBrush, list.ToArray(), width);
                        }
                        method_238(graphics, pen, list.ToArray(), width);
                        float num9 = 0.577350259f;
                        float num10 = num9 * num7;
                        float num11 = num10 + num7 * 0.3f;
                        float num12 = 0.9999582f;
                        float num13 = 0.5f;
                        float num14 = num12 * num10;
                        float num15 = num13 * num10;
                        float num16 = num12 * num11;
                        float num17 = num13 * num11;
                        num14 = num8 / 2f;
                        num15 = height / 4f;
                        num16 = 0f;
                        num17 = num15 - num14 / 2f;
                        float num18 = height / 6f;
                        graphics.DrawLine(pen, x + num14, y + num15, x + num16, y + num17);
                        graphics.DrawLine(pen, x + num6 - num14, y + num15, x + num6 - num16, y + num17);
                        graphics.DrawLine(pen, x + num6 - num14, y + height - num15, x + num6 - num16, y + height - num17);
                        graphics.DrawLine(pen, x + num14, y + height - num15, x + num16, y + height - num17);
                        graphics.DrawLine(pen, x + num6 / 2f, y, x + num6 / 2f, y - num18);
                        graphics.DrawLine(pen, x + num6 / 2f, y + height, x + num6 / 2f, y + height + num18);
                        break;
                    }
            }
            solidBrush.Dispose();
            pen.Dispose();
        }

        private Texture2D method_178(BuiltObject builtObject_1, bool bool_13)
        {
            Texture2D result = null;
            if (bool_13)
            {
                switch (builtObject_1.Role)
                {
                    case BuiltObjectRole.Military:
                        result = texture2D_40;
                        break;
                    case BuiltObjectRole.Freight:
                        result = texture2D_41;
                        break;
                    case BuiltObjectRole.Passenger:
                        result = texture2D_43;
                        break;
                    case BuiltObjectRole.Exploration:
                    case BuiltObjectRole.Colony:
                        result = texture2D_39;
                        break;
                    case BuiltObjectRole.Build:
                        result = texture2D_38;
                        break;
                    case BuiltObjectRole.Resource:
                        result = texture2D_42;
                        break;
                    case BuiltObjectRole.Base:
                        switch (builtObject_1.SubRole)
                        {
                            case BuiltObjectSubRole.GasMiningStation:
                            case BuiltObjectSubRole.MiningStation:
                                result = texture2D_37;
                                break;
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.ResortBase:
                            case BuiltObjectSubRole.GenericBase:
                            case BuiltObjectSubRole.EnergyResearchStation:
                            case BuiltObjectSubRole.WeaponsResearchStation:
                            case BuiltObjectSubRole.HighTechResearchStation:
                            case BuiltObjectSubRole.MonitoringStation:
                            case BuiltObjectSubRole.DefensiveBase:
                                result = texture2D_36;
                                break;
                        }
                        break;
                }
            }
            else
            {
                switch (builtObject_1.Role)
                {
                    case BuiltObjectRole.Military:
                        result = texture2D_48;
                        break;
                    case BuiltObjectRole.Freight:
                        result = texture2D_49;
                        break;
                    case BuiltObjectRole.Passenger:
                        result = texture2D_51;
                        break;
                    case BuiltObjectRole.Exploration:
                    case BuiltObjectRole.Colony:
                        result = texture2D_47;
                        break;
                    case BuiltObjectRole.Build:
                        result = texture2D_46;
                        break;
                    case BuiltObjectRole.Resource:
                        result = texture2D_50;
                        break;
                    case BuiltObjectRole.Base:
                        switch (builtObject_1.SubRole)
                        {
                            case BuiltObjectSubRole.GasMiningStation:
                            case BuiltObjectSubRole.MiningStation:
                                result = texture2D_45;
                                break;
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.ResortBase:
                            case BuiltObjectSubRole.GenericBase:
                            case BuiltObjectSubRole.EnergyResearchStation:
                            case BuiltObjectSubRole.WeaponsResearchStation:
                            case BuiltObjectSubRole.HighTechResearchStation:
                            case BuiltObjectSubRole.MonitoringStation:
                            case BuiltObjectSubRole.DefensiveBase:
                                result = texture2D_44;
                                break;
                        }
                        break;
                }
            }
            return result;
        }

        public void DrawShipSymbolXna(SpriteBatch spriteBatch, BuiltObject builtObject, System.Drawing.Color color, float x, float y, float width, float height, float originalWidth, float originalHeight, bool galaxyLevel, DateTime time)
        {
            color = method_226(color, 48);
            if (builtObject.AssaultOwnershipChangeCounter > 0)
            {
                System.Drawing.Color color_ = System.Drawing.Color.FromArgb(240, 240, 240);
                color = method_215(color, color_, time);
            }
            Texture2D texture2D = method_178(builtObject, galaxyLevel);
            float num = 1.2f;
            if (builtObject.Role == BuiltObjectRole.Military)
            {
                num = 1.4f;
            }
            else if (builtObject.Role != BuiltObjectRole.Exploration && builtObject.Role != BuiltObjectRole.Colony)
            {
                if (builtObject.SubRole != BuiltObjectSubRole.MiningShip && builtObject.SubRole != BuiltObjectSubRole.GasMiningShip)
                {
                    if (builtObject.SubRole == BuiltObjectSubRole.PassengerShip)
                    {
                        num = 1.6f;
                    }
                    else if (builtObject.SubRole == BuiltObjectSubRole.MiningStation || builtObject.SubRole == BuiltObjectSubRole.GasMiningStation)
                    {
                        num = 1.8f;
                    }
                }
                else
                {
                    num = 1.3f;
                }
            }
            else
            {
                num = 1.5f;
            }
            if (galaxyLevel)
            {
                num *= 1.2f;
                color = System.Drawing.Color.FromArgb(255, color);
            }
            float num2 = originalWidth * num;
            float num3 = originalHeight * num;
            float num4 = (float)texture2D.Height / (float)texture2D.Width;
            num2 /= num4;
            float num5 = x - (num2 - width) / 2f;
            float num6 = y - (num3 - height) / 2f;
            x = num5;
            y = num6;
            width = num2;
            height = num3;
            int num7 = (int)(x + 0.5f);
            int num8 = (int)(y + 0.5f);
            int num9 = (int)(width + 0.5f);
            int num10 = (int)(height + 0.5f);
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(num7, num8, num9, num10);
            XnaDrawingHelper.DrawTexture(spriteBatch, texture2D, destination, 0f, color);
        }

        public void DrawShipSymbolXna_OLD(SpriteBatch spriteBatch, BuiltObject builtObject, System.Drawing.Color color, float x, float y, float width, float height, float originalWidth, float originalHeight, bool fillInterior, double zoomFactor)
        {
            color = method_226(color, 64);
            bool dashed = false;
            if (zoomFactor <= 50.0 && builtObject.Owner == null && builtObject.Empire != galaxy_0.IndependentEmpire && builtObject.Empire != null)
            {
                dashed = true;
            }
            List<PointF> list = new List<PointF>();
            float num = 1.2f;
            if (builtObject.Role == BuiltObjectRole.Military)
            {
                num = 1.5f;
            }
            else if (builtObject.Role == BuiltObjectRole.Exploration || builtObject.Role == BuiltObjectRole.Colony)
            {
                num = 1.5f;
            }
            float num2 = originalWidth * num;
            float num3 = originalHeight * num;
            float num4 = x - (num2 - width) / 2f;
            float num5 = y - (num3 - height) / 2f;
            x = num4;
            y = num5;
            width = num2;
            height = num3;
            if (width > height)
            {
                y -= (width - height) / 2f;
                height = width;
            }
            switch (builtObject.Role)
            {
                case BuiltObjectRole.Military:
                    {
                        float num19 = width / 1.15470052f;
                        list.Add(new PointF(x + width / 2f, y));
                        list.Add(new PointF(x + width, y + num19));
                        list.Add(new PointF(x, y + num19));
                        XnaDrawingHelper.DrawPolygon(spriteBatch, list.ToArray(), color, 2);
                        break;
                    }
                case BuiltObjectRole.Freight:
                    XnaDrawingHelper.DrawCircle(spriteBatch, new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height), 60, color, 2, dashed);
                    break;
                case BuiltObjectRole.Passenger:
                    {
                        XnaDrawingHelper.DrawCircle(spriteBatch, new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height), 60, color, 2, dashed);
                        float num20 = width / 5f;
                        float num21 = x + width / 2f;
                        float num22 = y + height / 2f;
                        XnaDrawingHelper.DrawLine(spriteBatch, num21, y - num20, num21, y, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + width, num22, x + width + num20, num22, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, num21, y + height + num20, num21, y + height, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x - num20, num22, x, num22, color, 2);
                        break;
                    }
                case BuiltObjectRole.Exploration:
                case BuiltObjectRole.Colony:
                    list.Add(new PointF(x + width / 2f, y));
                    list.Add(new PointF(x + width, y + height / 2f));
                    list.Add(new PointF(x + width / 2f, y + height));
                    list.Add(new PointF(x, y + height / 2f));
                    XnaDrawingHelper.DrawPolygon(spriteBatch, list.ToArray(), color, 2);
                    break;
                case BuiltObjectRole.Build:
                    XnaDrawingHelper.DrawRectangle(spriteBatch, (int)x, (int)y, (int)width, (int)height, color, 2);
                    break;
                case BuiltObjectRole.Resource:
                    {
                        XnaDrawingHelper.DrawCircle(spriteBatch, new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height), 60, color, 2, dashed);
                        float num23 = 0.707106769f;
                        float num24 = width / 2f - width / 2f * num23;
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num24, y + num24, x, y, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + width - num24, y + num24, x + width, y, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + width - num24, y + height - num24, x + width, y + height, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num24, y + height - num24, x, y + height, color, 2);
                        break;
                    }
                case BuiltObjectRole.Base:
                    {
                        float num6 = height * 1.15470052f;
                        x -= height * 0.154700533f / 2f;
                        float num7 = num6 / 2f;
                        float num8 = (num6 - num7) / 2f;
                        list.Add(new PointF(x + num8, y));
                        list.Add(new PointF(x + num8 + num7, y));
                        list.Add(new PointF(x + num8 + num7 + num8, y + height / 2f));
                        list.Add(new PointF(x + num8 + num7, y + height));
                        list.Add(new PointF(x + num8, y + height));
                        list.Add(new PointF(x, y + height / 2f));
                        if (builtObject.SubRole != BuiltObjectSubRole.MiningStation && builtObject.SubRole != BuiltObjectSubRole.GasMiningStation)
                        {
                            XnaDrawingHelper.DrawPolygon(spriteBatch, list.ToArray(), color, 2);
                            break;
                        }
                        XnaDrawingHelper.DrawPolygon(spriteBatch, list.ToArray(), color, 2);
                        float num9 = 0.577350259f;
                        float num10 = num9 * num7;
                        float num11 = num10 + num7 * 0.3f;
                        float num12 = 0.9999582f;
                        float num13 = 0.5f;
                        float num14 = num12 * num10;
                        float num15 = num13 * num10;
                        float num16 = num12 * num11;
                        float num17 = num13 * num11;
                        num14 = num8 / 2f;
                        num15 = height / 4f;
                        num16 = 0f;
                        num17 = num15 - num14 / 2f;
                        float num18 = height / 6f;
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num14, y + num15, x + num16, y + num17, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num6 - num14, y + num15, x + num6 - num16, y + num17, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num6 - num14, y + height - num15, x + num6 - num16, y + height - num17, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num14, y + height - num15, x + num16, y + height - num17, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num6 / 2f, y, x + num6 / 2f, y - num18, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num6 / 2f, y + height, x + num6 / 2f, y + height + num18, color, 2);
                        break;
                    }
            }
        }

        public void InvalidateMain()
        {
            if (!bool_11)
            {
                Invalidate();
            }
        }

        public void ClearMain()
        {
            if (!bool_11)
            {
                using (Graphics graphics = CreateGraphics())
                {
                    graphics.Clear(main_0.color_3);
                }
            }
        }

        private void method_179(Habitat habitat_1, Graphics graphics_0, double double_15)
        {
            if (habitat_1.Explosions == null || habitat_1.Explosions.Count <= 0)
            {
                return;
            }
            Explosion[] array = habitat_1.Explosions.ToArray();
            Explosion[] array2 = array;
            foreach (Explosion explosion in array2)
            {
                int explosionSize = explosion.ExplosionSize;
                int num = explosionSize;
                int int_ = (int)((double)((int)(habitat_1.Xpos + (double)explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
                int int_2 = (int)((double)((int)(habitat_1.Ypos + (double)explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
                explosionSize = (int)((double)explosionSize / double_15);
                num = (int)((double)num / double_15);
                Bitmap bitmap = main_0.bitmap_19[explosion.ExplosionImageIndex][explosion.ExplosionCurrentImage];
                System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
                if (explosionSize > 80)
                {
                    method_176(graphics_0);
                }
                else
                {
                    method_175(graphics_0);
                }
                graphics_0.DrawImage(bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
                list_3.Add(rectangle);
                if (!explosion.ExplosionSoundPlayed)
                {
                    double double_16 = 0.0;
                    double double_17 = 0.0;
                    method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                    main_0.method_0(main_0.EffectsPlayer.ResolveExplosion(explosion.ExplosionSize, double_16, double_17));
                    explosion.ExplosionSoundPlayed = true;
                    if (explosion.ExplosionSize > 150)
                    {
                        int val = explosion.ExplosionSize / 50;
                        val = Math.Min(val, 8);
                        main_0.method_217(val);
                    }
                }
            }
            method_177(graphics_0);
        }

        private void method_180(SpriteBatch spriteBatch_2, Habitat habitat_1, double double_15)
        {
            if (habitat_1.Explosions == null || habitat_1.Explosions.Count <= 0)
            {
                return;
            }
            Explosion[] array = habitat_1.Explosions.ToArray();
            Explosion[] array2 = array;
            foreach (Explosion explosion in array2)
            {
                int explosionSize = explosion.ExplosionSize;
                int num = explosionSize;
                int int_ = (int)((double)((int)(habitat_1.Xpos + (double)explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
                int int_2 = (int)((double)((int)(habitat_1.Ypos + (double)explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
                explosionSize = (int)((double)explosionSize / double_15);
                num = (int)((double)num / double_15);
                Texture2D texture = texture2D_8[explosion.ExplosionImageIndex][explosion.ExplosionCurrentImage];
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, rectangle, 0f);
                list_3.Add(rectangle);
                if (!explosion.ExplosionSoundPlayed)
                {
                    double double_16 = 0.0;
                    double double_17 = 0.0;
                    method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                    main_0.method_0(main_0.EffectsPlayer.ResolveExplosion(explosion.ExplosionSize, double_16, double_17));
                    explosion.ExplosionSoundPlayed = true;
                    if (explosion.ExplosionSize > 150)
                    {
                        int val = explosion.ExplosionSize / 50;
                        val = Math.Min(val, 8);
                        main_0.method_217(val);
                    }
                }
            }
        }

        private void method_181(BuiltObject builtObject_1, Graphics graphics_0, double double_15)
        {
            Explosion[] explosion_ = builtObject_1.Explosions.ToArray();
            GwpRjOqBrJa(builtObject_1, explosion_, graphics_0, double_15);
        }

        private void method_182(Fighter fighter_0, Graphics graphics_0, double double_15)
        {
            Explosion[] explosion_ = fighter_0.Explosions.ToArray();
            GwpRjOqBrJa(fighter_0, explosion_, graphics_0, double_15);
        }

        private void GwpRjOqBrJa(StellarObject stellarObject_0, Explosion[] explosion_0, Graphics graphics_0, double double_15)
        {
            foreach (Explosion explosion in explosion_0)
            {
                int explosionSize = explosion.ExplosionSize;
                int num = explosionSize;
                int int_ = (int)((double)((int)(stellarObject_0.Xpos + (double)explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
                int int_2 = (int)((double)((int)(stellarObject_0.Ypos + (double)explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
                explosionSize = (int)((double)explosionSize / double_15);
                num = (int)((double)num / double_15);
                Bitmap bitmap = main_0.bitmap_19[explosion.ExplosionImageIndex][explosion.ExplosionCurrentImage];
                System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
                if (explosionSize > 50)
                {
                    method_176(graphics_0);
                }
                else
                {
                    method_175(graphics_0);
                }
                graphics_0.DrawImage(bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
                list_3.Add(rectangle);
                if (!explosion.ExplosionSoundPlayed)
                {
                    double double_16 = 0.0;
                    double double_17 = 0.0;
                    method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                    main_0.method_0(main_0.EffectsPlayer.ResolveExplosion(explosion.ExplosionSize, double_16, double_17));
                    explosion.ExplosionSoundPlayed = true;
                    if (explosion.ExplosionSize > 150)
                    {
                        int val = explosion.ExplosionSize / 50;
                        val = Math.Min(val, 8);
                        main_0.method_217(val);
                    }
                }
            }
            method_177(graphics_0);
        }

        private void method_183(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, double double_15)
        {
            Explosion[] explosion_ = builtObject_1.Explosions.ToArray();
            method_185(spriteBatch_2, builtObject_1, explosion_, double_15);
        }

        private void method_184(SpriteBatch spriteBatch_2, Fighter fighter_0, double double_15)
        {
            Explosion[] explosion_ = fighter_0.Explosions.ToArray();
            method_185(spriteBatch_2, fighter_0, explosion_, double_15);
        }

        private void method_185(SpriteBatch spriteBatch_2, StellarObject stellarObject_0, Explosion[] explosion_0, double double_15)
        {
            foreach (Explosion explosion in explosion_0)
            {
                int explosionSize = explosion.ExplosionSize;
                int num = explosionSize;
                int int_ = (int)((double)((int)(stellarObject_0.Xpos + (double)explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
                int int_2 = (int)((double)((int)(stellarObject_0.Ypos + (double)explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
                explosionSize = (int)((double)explosionSize / double_15);
                num = (int)((double)num / double_15);
                Texture2D texture = texture2D_8[explosion.ExplosionImageIndex][explosion.ExplosionCurrentImage];
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, rectangle, 0f);
                list_3.Add(rectangle);
                if (!explosion.ExplosionSoundPlayed)
                {
                    double double_16 = 0.0;
                    double double_17 = 0.0;
                    method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                    main_0.method_0(main_0.EffectsPlayer.ResolveExplosion(explosion.ExplosionSize, double_16, double_17));
                    explosion.ExplosionSoundPlayed = true;
                    if (explosion.ExplosionSize > 150)
                    {
                        int val = explosion.ExplosionSize / 50;
                        val = Math.Min(val, 8);
                        main_0.method_217(val);
                    }
                }
            }
        }

        private void method_186(Habitat habitat_1, Graphics graphics_0, double double_15)
        {
            if (habitat_1.Explosion == null)
            {
                return;
            }
            int explosionSize = habitat_1.Explosion.ExplosionSize;
            int num = explosionSize;
            int int_ = (int)((double)((int)(habitat_1.Xpos + (double)habitat_1.Explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
            int int_2 = (int)((double)((int)(habitat_1.Ypos + (double)habitat_1.Explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
            explosionSize = (int)((double)explosionSize / double_15);
            num = (int)((double)num / double_15);
            Bitmap bitmap = null;
            bitmap = ((habitat_1.Diameter > 50) ? main_0.bitmap_20[habitat_1.Explosion.ExplosionCurrentImage] : main_0.bitmap_19[0][habitat_1.Explosion.ExplosionCurrentImage]);
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
            method_176(graphics_0);
            graphics_0.DrawImage(bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
            method_177(graphics_0);
            list_3.Add(rectangle);
            if (!habitat_1.Explosion.ExplosionSoundPlayed)
            {
                double double_16 = 0.0;
                double double_17 = 0.0;
                method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                main_0.method_0(main_0.EffectsPlayer.ResolvePlanetExplosion(habitat_1.Explosion.ExplosionSize, double_16, double_17));
                habitat_1.Explosion.ExplosionSoundPlayed = true;
                if (habitat_1.Explosion.ExplosionSize > 100)
                {
                    main_0.method_218(9, 25);
                }
            }
        }

        private void method_187(SpriteBatch spriteBatch_2, Habitat habitat_1, double double_15)
        {
            if (habitat_1.Explosion == null)
            {
                return;
            }
            int explosionSize = habitat_1.Explosion.ExplosionSize;
            int num = explosionSize;
            int int_ = (int)((double)((int)(habitat_1.Xpos + (double)habitat_1.Explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
            int int_2 = (int)((double)((int)(habitat_1.Ypos + (double)habitat_1.Explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
            explosionSize = (int)((double)explosionSize / double_15);
            num = (int)((double)num / double_15);
            Texture2D texture2D = null;
            bool flag = true;
            if (habitat_1.Diameter <= 50)
            {
                texture2D = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_19[0][habitat_1.Explosion.ExplosionCurrentImage]);
            }
            else
            {
                texture2D = main_0.texture2D_1[habitat_1.Explosion.ExplosionCurrentImage];
                flag = false;
            }
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, rectangle, 0f);
            if (flag)
            {
                method_22(texture2D);
            }
            list_3.Add(rectangle);
            if (!habitat_1.Explosion.ExplosionSoundPlayed)
            {
                double double_16 = 0.0;
                double double_17 = 0.0;
                method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                main_0.method_0(main_0.EffectsPlayer.ResolvePlanetExplosion(habitat_1.Explosion.ExplosionSize, double_16, double_17));
                habitat_1.Explosion.ExplosionSoundPlayed = true;
                if (habitat_1.Explosion.ExplosionSize > 100)
                {
                    main_0.method_218(9, 25);
                }
            }
        }

        private void method_188(int int_11, int int_12, Empire empire_0, Graphics graphics_0)
        {
            graphics_0.DrawImageUnscaled(empire_0.SmallFlagPicture, int_11, int_12);
        }

        private void method_189(int int_11, int int_12, Graphics graphics_0)
        {
            graphics_0.DrawImageUnscaled(main_0.bitmap_44, int_11, int_12);
        }

        private void method_190(int int_11, int int_12, Graphics graphics_0)
        {
            graphics_0.DrawImageUnscaled(main_0.bitmap_43, int_11, int_12);
        }

        private void method_191(SpriteBatch spriteBatch_2, int int_11, int int_12)
        {
            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_10, int_11, int_12, 0f, 1f);
        }

        private void method_192(int int_11, int int_12, Empire empire_0, Graphics graphics_0)
        {
            int num = 24;
            int num2 = 15;
            num = (int)(24.0 / main_0.double_0);
            num2 = (int)(15.0 / main_0.double_0);
            if (num < 15)
            {
                num = 15;
                num2 = 10;
            }
            graphics_0.DrawImage(rect: new System.Drawing.Rectangle(int_11 + 4, int_12 + 4, num, num2), image: empire_0.LargeFlagPicture);
        }

        private void method_193(int int_11, int int_12, int int_13, int int_14, int int_15, Graphics graphics_0)
        {
            int num = (int)((double)int_15 / (double)int_14 * (double)int_13);
            if (int_15 < int_14)
            {
                graphics_0.DrawLine(pen_9, int_11 + num, int_12, int_11 + int_13, int_12);
            }
            graphics_0.DrawLine(pen_10, int_11, int_12, int_11 + num, int_12);
        }

        private void method_194(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, int int_15)
        {
            int num = (int)((double)int_15 / (double)int_14 * (double)int_13);
            if (int_15 < int_14)
            {
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num, int_12, int_11 + int_13, int_12, color_10, 2);
            }
            XnaDrawingHelper.DrawLine(spriteBatch_2, int_11, int_12, int_11 + num, int_12, color_9, 2);
        }

        private void method_195(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17)
        {
            if (int_17 > 0)
            {
                int num = int_15 + int_16 + int_17;
                int num2 = (int)(Math.Min(1.0, (double)int_15 / (double)num) * (double)int_13);
                int num3 = (int)(Math.Min(1.0, (double)int_16 / (double)num) * (double)int_13);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11, int_12, int_11 + num2, int_12, color_11, 2);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num2, int_12, int_11 + num2 + num3, int_12, color_12, 2);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num2 + num3, int_12, int_11 + int_13, int_12, color_14, 2);
            }
            else if (int_14 > 0)
            {
                int num4 = (int)(Math.Min(1.0, (double)int_15 / (double)int_14) * (double)int_13);
                int num5 = (int)(Math.Min(1.0, (double)int_16 / (double)int_14) * (double)int_13);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11, int_12, int_11 + num4, int_12, color_11, 2);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num4, int_12, int_11 + num4 + num5, int_12, color_12, 2);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num4 + num5, int_12, int_11 + int_13, int_12, color_13, 2);
            }
        }

        private void method_196(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            rectangle_5 = new System.Drawing.Rectangle(rectangle_5.X - 12, rectangle_5.Y - 12, rectangle_5.Width + 24, rectangle_5.Height + 24);
            graphics_0.DrawEllipse(pen, rectangle_5);
        }

        private void method_197(SpriteBatch spriteBatch_2, System.Drawing.Rectangle rectangle_5)
        {
            using (new Pen(color_2, 4f))
            {
                rectangle_5 = new System.Drawing.Rectangle(rectangle_5.X - 12, rectangle_5.Y - 12, rectangle_5.Width + 24, rectangle_5.Height + 24);
                XnaDrawingHelper.DrawCircle(spriteBatch_2, rectangle_5, color_2, 4);
            }
        }

        private void method_198(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_199(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_200(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_201(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_202(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_203(SpriteBatch spriteBatch_2, System.Drawing.Rectangle rectangle_5)
        {
            XnaDrawingHelper.DrawRectangle(spriteBatch_2, rectangle_5, color_2, 4);
        }

        private void method_204(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_205(SpriteBatch spriteBatch_2, System.Drawing.Rectangle rectangle_5)
        {
            XnaDrawingHelper.DrawRectangle(spriteBatch_2, rectangle_5, color_2, 4);
        }

        private void method_206(RectangleF rectangleF_1, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            RectangleF rect = new RectangleF(rectangleF_1.X - 8f, rectangleF_1.Y - 8f, rectangleF_1.Width + 16f, rectangleF_1.Height + 16f);
            graphics_0.DrawEllipse(pen, rect);
        }

        private void method_207(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            RectangleF rectangleF_ = new RectangleF(rectangle_5.X, rectangle_5.Y, rectangle_5.Width, rectangle_5.Height);
            method_206(rectangleF_, graphics_0);
        }

        private void method_208(SpriteBatch spriteBatch_2, RectangleF rectangleF_1)
        {
            RectangleF rectangleF_2 = new RectangleF(rectangleF_1.X - 8f, rectangleF_1.Y - 8f, rectangleF_1.Width + 16f, rectangleF_1.Height + 16f);
            XnaDrawingHelper.DrawCircle(spriteBatch_2, method_249(rectangleF_2), color_2, 4);
        }

        private void method_209(SpriteBatch spriteBatch_2, System.Drawing.Rectangle rectangle_5)
        {
            RectangleF rectangleF_ = new RectangleF(rectangle_5.X, rectangle_5.Y, rectangle_5.Width, rectangle_5.Height);
            method_208(spriteBatch_2, rectangleF_);
        }

        private void method_210(int int_11, int int_12, int int_13, int int_14, Graphics graphics_0)
        {
            method_211(int_11, int_12, int_13, int_14, graphics_0);
        }

        private void method_211(float float_0, float float_1, float float_2, float float_3, Graphics graphics_0)
        {
            method_213();
            Pen pen = new Pen(color_5, 5f);
            pen.EndCap = LineCap.Round;
            pen.StartCap = LineCap.Round;
            double num = 0.0;
            int second = DateTime.Now.ToUniversalTime().Second;
            int num2 = DateTime.Now.ToUniversalTime().Millisecond;
            if (second % 3 == 1)
            {
                num2 += 1000;
            }
            else if (second % 3 == 2)
            {
                num2 += 2000;
            }
            num = (double)num2 / 3000.0;
            int num3 = (int)(num * 90.0);
            int num4 = num3;
            int num5 = 90 + num3;
            int num6 = 180 + num3;
            int num7 = 270 + num3;
            if (num4 > 360)
            {
                num4 -= 360;
            }
            if (num5 > 360)
            {
                num5 -= 360;
            }
            if (num6 > 360)
            {
                num6 -= 360;
            }
            if (num7 > 360)
            {
                num7 -= 360;
            }
            float num8 = float_2 - float_0;
            float num9 = float_3 - float_1;
            if (num8 < 12f)
            {
                float_0 -= (12f - num8) / 2f;
                num8 = 12f;
            }
            if (num9 < 12f)
            {
                float_1 -= (12f - num9) / 2f;
                num9 = 12f;
            }
            RectangleF rect = new RectangleF(float_0, float_1, num8, num9);
            graphics_0.DrawArc(pen, rect, num4, 70f);
            graphics_0.DrawArc(pen, rect, num5, 70f);
            graphics_0.DrawArc(pen, rect, num6, 70f);
            graphics_0.DrawArc(pen, rect, num7, 70f);
            Pen pen2 = new Pen(color_6, 2f);
            pen2.EndCap = LineCap.Round;
            pen2.StartCap = LineCap.Round;
            graphics_0.DrawArc(pen2, rect, num4, 70f);
            graphics_0.DrawArc(pen2, rect, num5, 70f);
            graphics_0.DrawArc(pen2, rect, num6, 70f);
            graphics_0.DrawArc(pen2, rect, num7, 70f);
            pen2.Dispose();
            pen.Dispose();
        }

        private void method_212(SpriteBatch spriteBatch_2, float float_0, float float_1, float float_2, float float_3)
        {
            method_213();
            float num = float_2 - float_0;
            float num2 = float_3 - float_1;
            XnaDrawingHelper.DrawCircle(spriteBatch_2, float_0, float_1, num, num2, color_5, 5, 50);
        }

        private void method_213()
        {
            double num = 0.0;
            int second = DateTime.Now.ToUniversalTime().Second;
            int num2 = DateTime.Now.ToUniversalTime().Millisecond;
            if (second % 2 == 1)
            {
                num2 += 1000;
            }
            num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
            byte alpha = (byte)(color_7.A - (byte)((double)(color_7.A - color_8.A) * num));
            byte red = (byte)(color_7.R - (byte)((double)(color_7.R - color_8.R) * num));
            byte green = (byte)(color_7.G - (byte)((double)(color_7.G - color_8.G) * num));
            byte blue = (byte)(color_7.B - (byte)((double)(color_7.B - color_8.B) * num));
            color_5 = System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        private System.Drawing.Color method_214(System.Drawing.Color color_18, System.Drawing.Color color_19, DateTime dateTime_5)
        {
            double num = 0.0;
            int second = dateTime_5.Second;
            int num2 = dateTime_5.Millisecond;
            if (second % 2 == 1)
            {
                num2 += 1000;
            }
            num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
            byte alpha = (byte)(color_18.A - (byte)((double)(color_18.A - color_19.A) * num));
            byte red = (byte)(color_18.R - (byte)((double)(color_18.R - color_19.R) * num));
            byte green = (byte)(color_18.G - (byte)((double)(color_18.G - color_19.G) * num));
            byte blue = (byte)(color_18.B - (byte)((double)(color_18.B - color_19.B) * num));
            return System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        private System.Drawing.Color method_215(System.Drawing.Color color_18, System.Drawing.Color color_19, DateTime dateTime_5)
        {
            double num = 0.0;
            int millisecond = dateTime_5.Millisecond;
            num = ((millisecond <= 500) ? ((double)Math.Abs(500 - millisecond) / 500.0) : ((double)(millisecond - 500) / 500.0));
            byte alpha = (byte)(color_18.A - (byte)((double)(color_18.A - color_19.A) * num));
            byte red = (byte)(color_18.R - (byte)((double)(color_18.R - color_19.R) * num));
            byte green = (byte)(color_18.G - (byte)((double)(color_18.G - color_19.G) * num));
            byte blue = (byte)(color_18.B - (byte)((double)(color_18.B - color_19.B) * num));
            return System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        internal Bitmap method_216(Bitmap bitmap_7, float float_0)
        {
            double num = (double)float_0 * -1.0;
            double num2 = Math.Cos(num);
            double num3 = Math.Sin(num);
            int num4 = bitmap_7.Height;
            int num5 = 0;
            int num6 = bitmap_7.Width;
            Bitmap bitmap = new Bitmap(num6, num4);
            DistantWorlds.FastBitmap fastBitmap = new DistantWorlds.FastBitmap(bitmap_7);
            DistantWorlds.FastBitmap fastBitmap2 = new DistantWorlds.FastBitmap(bitmap);
            double num7 = 0.0;
            double num8 = 0.0;
            double num9 = bitmap_7.Width / 2;
            double num10 = bitmap_7.Height / 2;
            double num11 = bitmap_7.Width / 2;
            double num12 = bitmap_7.Height / 2;
            for (int i = 0; i < num4; i++)
            {
                num7 = ((double)num5 - num11) * num2 + ((double)i - num12) * num3 + num9;
                num8 = ((double)i - num12) * num2 - ((double)num5 - num11) * num3 + num10;
                for (int j = num5; j < num6; j++)
                {
                    int X = (int)num7;
                    int Y = (int)num8;
                    if (X >= 0 && X < num6 && Y >= 0 && Y < num4)
                    {
                        fastBitmap2.SetPixel(ref j, ref i, fastBitmap.GetPixel(X, Y));
                    }
                    num7 += num2;
                    num8 -= num3;
                }
            }
            fastBitmap2.Dispose();
            fastBitmap.Dispose();
            return bitmap;
        }

        internal Bitmap method_217(Bitmap bitmap_7, float float_0)
        {
            return method_218(bitmap_7, float_0, GraphicsQuality.Medium);
        }

        internal Bitmap method_218(Bitmap bitmap_7, float float_0, GraphicsQuality graphicsQuality_0)
        {
            if (bitmap_7 == null)
            {
                throw new ArgumentNullException("image");
            }
            float num = bitmap_7.Width;
            float num2 = bitmap_7.Height;
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
            array[2].Y = num2;
            array[3].Y = num2;
            Bitmap bitmap = null;
            using System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            matrix.Rotate(float_0);
            matrix.TransformPoints(array);
            double num3 = double.MinValue;
            double num4 = double.MinValue;
            double num5 = double.MaxValue;
            double num6 = double.MaxValue;
            PointF[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                PointF pointF = array2[i];
                num3 = Math.Max(num3, pointF.X);
                num5 = Math.Min(num5, pointF.X);
                num4 = Math.Max(num4, pointF.Y);
                num6 = Math.Min(num6, pointF.Y);
            }
            double num7 = Math.Ceiling(num3 - num5);
            double num8 = Math.Ceiling(num4 - num6);
            if (double.IsNaN(num7))
            {
                num7 = ((!float.IsNaN(num)) ? Math.Max(1.0, bitmap_7.Width) : 50.0);
            }
            if (double.IsNaN(num8))
            {
                num8 = ((!float.IsNaN(num2)) ? Math.Max(1.0, bitmap_7.Height) : 50.0);
            }
            num7 = Math.Max(1.0, num7);
            num8 = Math.Max(1.0, num8);
            bitmap = new Bitmap((int)num7, (int)num8, PixelFormat.Format32bppPArgb);
            bitmap.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
            using Graphics graphics = Graphics.FromImage(bitmap);
            main_0.method_112(graphics, graphicsQuality_0);
            PointF point = new PointF((float)(num7 / 2.0), (float)(num8 / 2.0));
            PointF point2 = new PointF(point.X - num / 2f, point.Y - num2 / 2f);
            matrix.Reset();
            matrix.RotateAt(float_0, point);
            graphics.Transform = matrix;
            graphics.DrawImage(bitmap_7, point2);
            return bitmap;
        }

        internal ImageAttributes method_219(System.Drawing.Color color_18)
        {
            float num = (float)(int)color_18.R / 255f;
            float num2 = (float)(int)color_18.G / 255f;
            float num3 = (float)(int)color_18.B / 255f;
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { num, num2, num3, 0f, 0f },
            new float[5] { num, num2, num3, 0f, 0f },
            new float[5] { num, num2, num3, 0f, 0f },
            new float[5] { 0f, 0f, 0f, 1f, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        internal ImageAttributes method_220(System.Drawing.Color color_18)
        {
            float num = (float)(int)color_18.R / 255f;
            float num2 = (float)(int)color_18.G / 255f;
            float num3 = (float)(int)color_18.B / 255f;
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { num, 0f, 0f, 0f, 0f },
            new float[5] { 0f, num2, 0f, 0f, 0f },
            new float[5] { 0f, 0f, num3, 0f, 0f },
            new float[5] { 0f, 0f, 0f, 1f, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        internal ImageAttributes method_221(System.Drawing.Color color_18)
        {
            float num = (float)(int)color_18.R / 255f;
            float num2 = (float)(int)color_18.G / 255f;
            float num3 = (float)(int)color_18.B / 255f;
            float[][] array = new float[5][];
            float[] array2 = (array[0] = new float[5]);
            float[] array3 = (array[1] = new float[5]);
            float[] array4 = (array[2] = new float[5]);
            array[3] = new float[5] { 0f, 0f, 0f, 1f, 0f };
            array[4] = new float[5] { num, num2, num3, 0f, 1f };
            float[][] newColorMatrix = array;
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        internal Bitmap method_222(Bitmap bitmap_7, System.Drawing.Color color_18)
        {
            ImageAttributes imageAttributes = null;
            imageAttributes = method_219(color_18);
            Bitmap bitmap = new Bitmap(bitmap_7);
            using Graphics graphics = Graphics.FromImage(bitmap);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            graphics.DrawImage(bitmap_7, destRect, 0, 0, bitmap_7.Width, bitmap_7.Height, GraphicsUnit.Pixel, imageAttributes);
            return bitmap;
        }

        internal Bitmap method_223(Bitmap bitmap_7, System.Drawing.Color color_18)
        {
            return method_224(bitmap_7, color_18, bool_13: false);
        }

        internal Bitmap method_224(Bitmap bitmap_7, System.Drawing.Color color_18, bool bool_13)
        {
            ImageAttributes imageAttributes = null;
            imageAttributes = ((!bool_13) ? method_220(color_18) : method_221(color_18));
            Bitmap bitmap = new Bitmap(bitmap_7);
            bitmap.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing)
                {
                    method_175(graphics);
                }
                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
                graphics.DrawImage(bitmap_7, destRect, 0, 0, bitmap_7.Width, bitmap_7.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            imageAttributes?.Dispose();
            return bitmap;
        }

        private void method_225(int int_11, int int_12, double double_15, double double_16, ref int int_13, ref int int_14, ref int int_15, ref int int_16)
        {
            int_13 = (int)((double)int_11 - double_15 / 2.0);
            int_14 = (int)((double)int_11 + double_15 / 2.0);
            int_15 = (int)((double)int_12 - double_16 / 2.0);
            int_16 = (int)((double)int_12 + double_16 / 2.0);
        }

        private System.Drawing.Color method_226(System.Drawing.Color color_18, int int_11)
        {
            int red = Math.Max(0, Math.Min(255, color_18.R + int_11));
            int green = Math.Max(0, Math.Min(255, color_18.G + int_11));
            int blue = Math.Max(0, Math.Min(255, color_18.B + int_11));
            return System.Drawing.Color.FromArgb(color_18.A, red, green, blue);
        }

        private void method_227(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14)
        {
            DateTime now = DateTime.Now;
            double_12 += now.Subtract(dateTime_4).TotalSeconds;
            if (double_12 > double_13)
            {
                if (double_12 > double_13 * 2.0)
                {
                    double_12 = 0.0;
                }
                double_12 -= double_13;
            }
            for (int i = 0; i < galaxy_0.PlayerEmpire.LocationHints.Count; i++)
            {
                System.Drawing.Point point = galaxy_0.PlayerEmpire.LocationHints[i];
                method_228(graphics_0, main_0.double_0, point.X, point.Y, main_0.int_13, main_0.int_14, int_11, int_12, int_13, int_14);
            }
            dateTime_4 = now;
        }

        private void method_228(Graphics graphics_0, double double_15, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18)
        {
            int num = (int)((double)(int_11 - int_15) / double_15);
            int num2 = (int)((double)(int_12 - int_17) / double_15);
            if (num >= -40 && num <= base.ClientRectangle.Width + 40 && num2 >= -40 && num2 <= base.ClientRectangle.Height + 40)
            {
                double num3 = double_13 * 0.47;
                double num4 = Math.Min(1.0, Math.Max(0.0, (double_12 - num3) / num3));
                int alpha = 255 - (int)(num4 * 250.0);
                System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, color_17);
                int num5 = 1 + (int)(double_12 / double_13 * (double)int_10);
                using Pen pen = new Pen(color, 1.5f);
                pen.DashStyle = DashStyle.Dot;
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
                graphics_0.DrawEllipse(pen, rect);
            }
        }

        private void method_229(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14)
        {
            DateTime now = DateTime.Now;
            double_12 += now.Subtract(dateTime_4).TotalSeconds;
            if (double_12 > double_13)
            {
                if (double_12 > double_13 * 2.0)
                {
                    double_12 = 0.0;
                }
                double_12 -= double_13;
            }
            for (int i = 0; i < galaxy_0.PlayerEmpire.LocationHints.Count; i++)
            {
                System.Drawing.Point point = galaxy_0.PlayerEmpire.LocationHints[i];
                method_230(spriteBatch_2, main_0.double_0, point.X, point.Y, main_0.int_13, main_0.int_14, int_11, int_12, int_13, int_14);
            }
            dateTime_4 = now;
        }

        private void method_230(SpriteBatch spriteBatch_2, double double_15, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18)
        {
            int num = (int)((double)(int_11 - int_15) / double_15);
            int num2 = (int)((double)(int_12 - int_17) / double_15);
            if (num >= -40 && num <= base.ClientRectangle.Width + 40 && num2 >= -40 && num2 <= base.ClientRectangle.Height + 40)
            {
                double num3 = double_13 * 0.47;
                double num4 = Math.Min(1.0, Math.Max(0.0, (double_12 - num3) / num3));
                int alpha = 255 - (int)(num4 * 250.0);
                System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, color_17);
                int num5 = 1 + (int)(double_12 / double_13 * (double)int_10);
                System.Drawing.Rectangle area = new System.Drawing.Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
                XnaDrawingHelper.DrawCircle(spriteBatch_2, area, color, 1, dashed: true);
            }
        }

        private void method_231(DateTime dateTime_5, Graphics graphics_0, double double_15, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18)
        {
            double_11 = 1.6;
            int num = (int)((double)(int_11 - int_15) / double_15);
            int num2 = (int)((double)(int_12 - int_17) / double_15);
            if (num < -40 || num > base.ClientRectangle.Width + 40 || num2 < -40 || num2 > base.ClientRectangle.Height + 40)
            {
                return;
            }
            double_10 += dateTime_5.Subtract(dateTime_3).TotalSeconds;
            if (double_10 > double_11)
            {
                if (double_10 > double_11 * 2.0)
                {
                    double_10 = 0.0;
                }
                double_10 -= double_11;
            }
            double num3 = double_11 * 0.5;
            double num4 = Math.Min(1.0, Math.Max(0.0, (double_10 - num3) / num3));
            int alpha = 255 - (int)(num4 * 250.0);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, color_16);
            int num5 = 1 + (int)(double_10 / double_11 * (double)int_9);
            using Pen pen = new Pen(color, 3f);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
            graphics_0.DrawEllipse(pen, rect);
        }

        private void method_232(SpriteBatch spriteBatch_2, DateTime dateTime_5, double double_15, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18)
        {
            double_11 = 1.6;
            int num = (int)((double)(int_11 - int_15) / double_15);
            int num2 = (int)((double)(int_12 - int_17) / double_15);
            if (num < -40 || num > base.ClientRectangle.Width + 40 || num2 < -40 || num2 > base.ClientRectangle.Height + 40)
            {
                return;
            }
            double_10 += dateTime_5.Subtract(dateTime_3).TotalSeconds;
            if (double_10 > double_11)
            {
                if (double_10 > double_11 * 2.0)
                {
                    double_10 = 0.0;
                }
                double_10 -= double_11;
            }
            double num3 = double_11 * 0.5;
            double num4 = Math.Min(1.0, Math.Max(0.0, (double_10 - num3) / num3));
            int alpha = 255 - (int)(num4 * 250.0);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, color_16);
            int num5 = 1 + (int)(double_10 / double_11 * (double)int_9);
            System.Drawing.Rectangle area = new System.Drawing.Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
            XnaDrawingHelper.DrawCircle(spriteBatch_2, area, color, 3);
        }

        private System.Drawing.Color method_233(System.Drawing.Color color_18, float float_0)
        {
            int red = Math.Max(0, Math.Min(255, (int)((float)(int)color_18.R * float_0)));
            int green = Math.Max(0, Math.Min(255, (int)((float)(int)color_18.G * float_0)));
            int blue = Math.Max(0, Math.Min(255, (int)((float)(int)color_18.B * float_0)));
            Math.Max(0, Math.Min(255, (int)((float)(int)color_18.A * float_0)));
            return System.Drawing.Color.FromArgb(color_18.A, red, green, blue);
        }

        private void method_234(SpriteBatch spriteBatch_2, Galaxy galaxy_1, long long_1, Habitat habitat_1, System.Drawing.Color color_18, System.Drawing.Rectangle rectangle_5, double double_15, double double_16)
        {
            color_18 = method_233(color_18, 1.3f);
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(rectangle_5.Location, rectangle_5.Size);
            destination.Inflate((int)((double)destination.Width * (double_15 / 2.0)), (int)((double)destination.Height * (double_15 / 2.0)));
            long num = 700L;
            _ = (double)habitat_1.HabitatIndex / ((double)galaxy_1.Habitats.Count / 700.0);
            int num2 = (int)((long_1 - habitat_1.HabitatIndex) % 700L);
            if (num2 < 0)
            {
                num2 += (int)num;
            }
            double num3 = Math.Min(1.0, Math.Max(0.0, (double)num2 / (double)num));
            int num4 = (int)((long_1 - habitat_1.HabitatIndex) % (num * texture2D_19.Length) / num);
            int num5 = num4 + 1;
            if (num5 >= texture2D_19.Length)
            {
                num5 -= texture2D_19.Length;
            }
            Texture2D texture = texture2D_19[num4];
            Texture2D texture2 = texture2D_19[num5];
            if (num3 >= 0.0 && num3 < 0.2)
            {
                color_18 = System.Drawing.Color.FromArgb(Math.Max(0, Math.Min(255, (int)(255.0 * double_16))), color_18);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, destination, 0f, color_18);
            }
            else if (num3 >= 0.2 && num3 < 0.8)
            {
                double num6 = (0.8 - num3) * 1.666;
                num6 += 0.2;
                num6 = Math.Min(1.0, num6);
                num6 *= double_16;
                double num7 = (num3 - 0.2) * 1.666;
                num7 += 0.2;
                num7 = Math.Min(1.0, num7);
                num7 *= double_16;
                if (num3 > 0.5)
                {
                    System.Drawing.Color tintColor = System.Drawing.Color.FromArgb((int)(num7 * 255.0), color_18);
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2, destination, 0f, tintColor);
                    tintColor = System.Drawing.Color.FromArgb((int)(num6 * 255.0), color_18);
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, destination, 0f, tintColor);
                }
                else
                {
                    System.Drawing.Color tintColor2 = System.Drawing.Color.FromArgb((int)(num6 * 255.0), color_18);
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, destination, 0f, tintColor2);
                    tintColor2 = System.Drawing.Color.FromArgb((int)(num7 * 255.0), color_18);
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2, destination, 0f, tintColor2);
                }
            }
            else
            {
                color_18 = System.Drawing.Color.FromArgb(Math.Max(0, Math.Min(255, (int)(255.0 * double_16))), color_18);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2, destination, 0f, color_18);
            }
        }

        private void method_235(Graphics graphics_0, Galaxy galaxy_1, Habitat habitat_1, System.Drawing.Color color_18, System.Drawing.Rectangle rectangle_5, double double_15, bool bool_13, bool bool_14)
        {
            Bitmap[] array = main_0.bitmap_197;
            if (bool_14)
            {
                array = main_0.bitmap_198;
            }
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(rectangle_5.Location, rectangle_5.Size);
            destRect.Inflate((int)((double)destRect.Width * (double_15 / 2.0)), (int)((double)destRect.Height * (double_15 / 2.0)));
            long num = 700L;
            _ = (double)habitat_1.HabitatIndex / ((double)galaxy_1.Habitats.Count / 700.0);
            int num2 = (int)((galaxy_1.CurrentStarDate - habitat_1.HabitatIndex) % 700L);
            if (num2 < 0)
            {
                num2 += (int)num;
            }
            double num3 = Math.Min(1.0, Math.Max(0.0, (double)num2 / (double)num));
            int num4 = (int)((galaxy_1.CurrentStarDate - habitat_1.HabitatIndex) % (num * array.Length) / num);
            int num5 = num4 + 1;
            if (num5 >= array.Length)
            {
                num5 -= array.Length;
            }
            Bitmap bitmap = array[num4];
            Bitmap bitmap2 = array[num5];
            method_176(graphics_0);
            if (!bool_13)
            {
                using ImageAttributes imageAttr = method_237(color_18, 1.0);
                graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr);
            }
            else if (num3 >= 0.0 && num3 < 0.2)
            {
                using ImageAttributes imageAttr2 = method_237(color_18, 1.0);
                graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr2);
            }
            else if (num3 >= 0.2 && num3 < 0.8)
            {
                double num6 = (0.8 - num3) * 1.666;
                num6 += 0.2;
                num6 = Math.Min(1.0, num6);
                double num7 = (num3 - 0.2) * 1.666;
                num7 += 0.2;
                num7 = Math.Min(1.0, num7);
                if (num3 > 0.5)
                {
                    using (ImageAttributes imageAttr3 = method_237(color_18, num7))
                    {
                        graphics_0.DrawImage(bitmap2, destRect, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttr3);
                    }
                    using ImageAttributes imageAttr4 = method_237(color_18, num6);
                    graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr4);
                }
                else
                {
                    using (ImageAttributes imageAttr5 = method_237(color_18, num6))
                    {
                        graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr5);
                    }
                    using ImageAttributes imageAttr6 = method_237(color_18, num7);
                    graphics_0.DrawImage(bitmap2, destRect, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttr6);
                }
            }
            else
            {
                using ImageAttributes imageAttr7 = method_237(color_18, 1.0);
                graphics_0.DrawImage(bitmap2, destRect, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttr7);
            }
            method_177(graphics_0);
        }

        private ImageAttributes method_236(double double_15)
        {
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5]
            {
                0f,
                0f,
                0f,
                (float)double_15,
                0f
            },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        private ImageAttributes method_237(System.Drawing.Color color_18, double double_15)
        {
            float num = (float)(int)color_18.R / 255f;
            float num2 = (float)(int)color_18.G / 255f;
            float num3 = (float)(int)color_18.B / 255f;
            float[][] array = new float[5][];
            float[] array2 = (array[0] = new float[5]);
            float[] array3 = (array[1] = new float[5]);
            float[] array4 = (array[2] = new float[5]);
            array[3] = new float[5]
            {
            0f,
            0f,
            0f,
            (float)double_15,
            0f
            };
            array[4] = new float[5] { num, num2, num3, 0f, 1f };
            float[][] newColorMatrix = array;
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        private void method_238(Graphics graphics_0, Pen pen_11, PointF[] pointF_0, float float_0)
        {
            if (float_0 < 2f)
            {
                if (pointF_0 != null && pointF_0.Length > 0)
                {
                    graphics_0.DrawRectangle(pen_11, new System.Drawing.Rectangle((int)pointF_0[0].X, (int)pointF_0[0].Y, 2, 2));
                }
            }
            else
            {
                graphics_0.DrawPolygon(pen_11, pointF_0);
            }
        }

        private void method_239(Graphics graphics_0, SolidBrush solidBrush_21, PointF[] pointF_0, float float_0)
        {
            if (float_0 < 2f)
            {
                if (pointF_0 != null && pointF_0.Length > 0)
                {
                    graphics_0.FillRectangle(solidBrush_21, new System.Drawing.Rectangle((int)pointF_0[0].X, (int)pointF_0[0].Y, 2, 2));
                }
            }
            else
            {
                graphics_0.FillPolygon(solidBrush_21, pointF_0);
            }
        }

        private void method_240(Graphics graphics_0, Pen pen_11, System.Drawing.Rectangle rectangle_5)
        {
            if (rectangle_5.Width >= 2 && rectangle_5.Height >= 2)
            {
                graphics_0.DrawEllipse(pen_11, rectangle_5);
            }
            else
            {
                graphics_0.DrawRectangle(pen_11, rectangle_5);
            }
        }

        private void method_241(Graphics graphics_0, Pen pen_11, RectangleF rectangleF_1)
        {
            if (!(rectangleF_1.Width < 2f) && rectangleF_1.Height >= 2f)
            {
                graphics_0.DrawEllipse(pen_11, rectangleF_1);
            }
            else
            {
                graphics_0.DrawRectangle(pen_11, rectangleF_1.X, rectangleF_1.Y, rectangleF_1.Width, rectangleF_1.Height);
            }
        }

        private void method_242(Graphics graphics_0, Brush brush_0, System.Drawing.Rectangle rectangle_5)
        {
            if (rectangle_5.Width >= 2 && rectangle_5.Height >= 2)
            {
                graphics_0.FillEllipse(brush_0, rectangle_5);
            }
            else
            {
                graphics_0.FillRectangle(brush_0, rectangle_5);
            }
        }

        private void method_243(Graphics graphics_0, Brush brush_0, RectangleF rectangleF_1)
        {
            if (!(rectangleF_1.Width < 2f) && rectangleF_1.Height >= 2f)
            {
                graphics_0.FillEllipse(brush_0, rectangleF_1);
            }
            else
            {
                graphics_0.FillRectangle(brush_0, rectangleF_1.X, rectangleF_1.Y, rectangleF_1.Width, rectangleF_1.Height);
            }
        }

        private void method_244(int int_11, int int_12, int int_13, int int_14, double double_15)
        {
            double num = (double)int_13 * double_15;
            double num2 = (double)int_14 * double_15;
            int int_15 = 0;
            int int_16 = 0;
            int int_17 = 0;
            int int_18 = 0;
            method_225(int_11, int_12, num, num2, ref int_15, ref int_16, ref int_17, ref int_18);
            int val = (int)(num * 0.55);
            int val2 = (int)(num2 * 0.55);
            int val3 = (int)(num * 0.9 * (double_15 / 15000.0));
            int val4 = (int)(num2 * 0.9 * (double_15 / 15000.0));
            val3 = Math.Max(val, val3);
            val4 = Math.Max(val2, val4);
            if (int_15 < val3 * -1)
            {
                int_11 = (int)num / 2 - val3;
                main_0.int_13 = int_11;
            }
            if (int_16 > Galaxy.SizeX + val3)
            {
                int_11 = Galaxy.SizeX + val3 - (int)num / 2;
                main_0.int_13 = int_11;
            }
            if (int_17 < val4 * -1)
            {
                int_12 = (int)num2 / 2 - val4;
                main_0.int_14 = int_12;
            }
            if (int_18 > Galaxy.SizeY + val4)
            {
                int_12 = Galaxy.SizeY + val4 - (int)num2 / 2;
                main_0.int_14 = int_12;
            }
        }

        private void method_245(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, double double_15)
        {
            if (main_0 == null || main_0.list_6 == null || main_0.list_6.Count <= 0)
            {
                return;
            }
            using Pen pen = new Pen(System.Drawing.Color.White, 2f);
            for (int i = 0; i < main_0.list_6.Count; i++)
            {
                object obj = main_0.list_6[i];
                double num = 0.0;
                double num2 = 0.0;
                if (obj is Habitat)
                {
                    Habitat habitat = (Habitat)obj;
                    num = habitat.Xpos;
                    num2 = habitat.Ypos;
                }
                if (num >= (double)int_11 && num <= (double)int_12 && num2 >= (double)int_13 && num2 <= (double)int_14)
                {
                    int num3 = (int)((num - (double)int_11) / double_15);
                    int num4 = (int)((num2 - (double)int_13) / double_15);
                    graphics_0.DrawEllipse(pen, new System.Drawing.Rectangle(num3 - 4, num4 - 4, 9, 9));
                }
            }
        }

        private void method_246(Graphics graphics_0, Empire empire_0, double double_15, int int_11, int int_12, int int_13, int int_14)
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(128, 255, 0, 64);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(128, 64, 0, 255);
            using HatchBrush brush = new HatchBrush(HatchStyle.Percent20, color, System.Drawing.Color.Transparent);
            using Pen pen = new Pen(color);
            pen.Width = 1f;
            using HatchBrush brush2 = new HatchBrush(HatchStyle.Percent20, color2, System.Drawing.Color.Transparent);
            using Pen pen2 = new Pen(color2);
            pen2.Width = 1f;
            for (int i = 0; i < empire_0.ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = empire_0.ShipGroups[i];
                if (shipGroup == null || shipGroup.LeadShip == null)
                {
                    continue;
                }
                if (shipGroup.Posture == FleetPosture.Attack)
                {
                    if (shipGroup.AttackPoint == null)
                    {
                        continue;
                    }
                    float num = 0f;
                    if (shipGroup.PostureRangeSquared > 2250000.0 && shipGroup.PostureRangeSquared < 3.4028234663852886E+38)
                    {
                        num = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_15);
                    }
                    float num2 = (float)((shipGroup.AttackPoint.Xpos - (double)int_13) / double_15);
                    float num3 = (float)((shipGroup.AttackPoint.Ypos - (double)int_14) / double_15);
                    if (num > 0f && num2 + num > 0f && num2 - num < (float)int_11 && num3 + num > 0f && num3 - num < (float)int_12)
                    {
                        RectangleF rect = new RectangleF(num2 - num, num3 - num, num * 2f, num * 2f);
                        graphics_0.FillEllipse(brush, rect);
                        graphics_0.DrawEllipse(pen, rect);
                    }
                    if (shipGroup.GatherPoint != null)
                    {
                        float x = (float)((shipGroup.GatherPoint.Xpos - (double)int_13) / double_15);
                        float y = (float)((shipGroup.GatherPoint.Ypos - (double)int_14) / double_15);
                        using Pen pen3 = new Pen(color);
                        pen3.Width = 3f;
                        pen3.DashStyle = DashStyle.Dot;
                        pen3.EndCap = LineCap.ArrowAnchor;
                        graphics_0.DrawLine(pen3, x, y, num2, num3);
                    }
                }
                else if (shipGroup.Posture == FleetPosture.Defend && shipGroup.GatherPoint != null)
                {
                    float num4 = 0f;
                    if (shipGroup.PostureRangeSquared > 2250000.0 && shipGroup.PostureRangeSquared < 3.4028234663852886E+38)
                    {
                        num4 = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_15);
                    }
                    float num5 = (float)((shipGroup.GatherPoint.Xpos - (double)int_13) / double_15);
                    float num6 = (float)((shipGroup.GatherPoint.Ypos - (double)int_14) / double_15);
                    if (num4 > 0f && num5 + num4 > 0f && num5 - num4 < (float)int_11 && num6 + num4 > 0f && num6 - num4 < (float)int_12)
                    {
                        RectangleF rect2 = new RectangleF(num5 - num4, num6 - num4, num4 * 2f, num4 * 2f);
                        graphics_0.FillEllipse(brush2, rect2);
                        graphics_0.DrawEllipse(pen2, rect2);
                    }
                }
            }
        }

        private void method_247(SpriteBatch spriteBatch_2, Empire empire_0, double double_15, int int_11, int int_12, int int_13, int int_14)
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(96, 255, 0, 64);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(96, 64, 0, 255);
            for (int i = 0; i < empire_0.ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = empire_0.ShipGroups[i];
                if (shipGroup == null || shipGroup.LeadShip == null)
                {
                    continue;
                }
                if (shipGroup.Posture == FleetPosture.Attack)
                {
                    if (shipGroup.AttackPoint == null)
                    {
                        continue;
                    }
                    float num = 0f;
                    if (shipGroup.PostureRangeSquared > 2250000.0 && shipGroup.PostureRangeSquared < 3.4028234663852886E+38)
                    {
                        num = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_15);
                    }
                    float num2 = (float)((shipGroup.AttackPoint.Xpos - (double)int_13) / double_15);
                    float num3 = (float)((shipGroup.AttackPoint.Ypos - (double)int_14) / double_15);
                    if (num > 0f && num2 + num > 0f && num2 - num < (float)int_11 && num3 + num > 0f && num3 - num < (float)int_12)
                    {
                        RectangleF rectangleF = new RectangleF(num2 - num, num3 - num, num * 2f, num * 2f);
                        System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_17, rectangle, 0f, color);
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, rectangle, 100, color, 1);
                    }
                    if (shipGroup.GatherPoint != null)
                    {
                        float x = (float)((shipGroup.GatherPoint.Xpos - (double)int_13) / double_15);
                        float y = (float)((shipGroup.GatherPoint.Ypos - (double)int_14) / double_15);
                        using (new Pen(color))
                        {
                            XnaDrawingHelper.DrawLine(spriteBatch_2, x, y, num2, num3, color, 3, dashed: true, texture2D_35);
                        }
                    }
                }
                else if (shipGroup.Posture == FleetPosture.Defend && shipGroup.GatherPoint != null)
                {
                    float num4 = 0f;
                    if (shipGroup.PostureRangeSquared > 2250000.0 && shipGroup.PostureRangeSquared < 3.4028234663852886E+38)
                    {
                        num4 = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_15);
                    }
                    float num5 = (float)((shipGroup.GatherPoint.Xpos - (double)int_13) / double_15);
                    float num6 = (float)((shipGroup.GatherPoint.Ypos - (double)int_14) / double_15);
                    if (num4 > 0f && num5 + num4 > 0f && num5 - num4 < (float)int_11 && num6 + num4 > 0f && num6 - num4 < (float)int_12)
                    {
                        RectangleF rectangleF2 = new RectangleF(num5 - num4, num6 - num4, num4 * 2f, num4 * 2f);
                        System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle((int)rectangleF2.X, (int)rectangleF2.Y, (int)rectangleF2.Width, (int)rectangleF2.Height);
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_17, rectangle2, 0f, color2);
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, rectangle2, 100, color2, 1);
                    }
                }
            }
        }

        private void method_248(int int_11, int int_12, int int_13, int int_14, double double_15, Graphics graphics_0, int int_15)
        {
            int_15 = 255;
            Empire empire = main_0._Game.PlayerEmpire;
            bool flag = false;
            if (main_0.empire_1 != null)
            {
                empire = main_0.empire_1;
                flag = true;
            }
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            bool flag5 = true;
            bool flag6 = true;
            bool flag7 = true;
            bool flag8 = true;
            bool flag9 = true;
            bool flag10 = true;
            bool flag11 = true;
            bool flag12 = true;
            bool flag13 = true;
            if (main_0.gameOptions_0 != null && !main_0._Game.GodMode && double_15 > 3500.0)
            {
                flag2 = main_0.gameOptions_0.GalaxyViewDisplayFleets;
                flag3 = main_0.gameOptions_0.GalaxyViewDisplayResupplyShips;
                flag4 = main_0.gameOptions_0.GalaxyViewDisplayMilitaryShips;
                flag5 = main_0.gameOptions_0.GalaxyViewDisplaySpacePorts;
                flag6 = main_0.gameOptions_0.GalaxyViewDisplayOtherBases;
                flag7 = main_0.gameOptions_0.GalaxyViewDisplayExplorationShips;
                flag8 = main_0.gameOptions_0.GalaxyViewDisplayColonyShips;
                flag9 = main_0.gameOptions_0.GalaxyViewDisplayConstructionShips;
                flag10 = main_0.gameOptions_0.GalaxyViewDisplayCivilianShips;
                flag11 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets;
                flag12 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips;
                flag13 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysPirates;
            }
            bool flag14 = false;
            bool flag15 = false;
            bool flag16 = false;
            bool flag17 = false;
            bool flag18 = false;
            bool flag19 = false;
            bool flag20 = false;
            if (main_0.gameOptions_0 != null)
            {
                flag14 = main_0.gameOptions_0.MapOverlayFleetPostures;
                flag15 = main_0.gameOptions_0.MapOverlayTravelVectorsState;
                flag16 = main_0.gameOptions_0.MapOverlayTravelVectorsPrivate;
                flag17 = main_0.gameOptions_0.MapOverlayPotentialColonies;
                flag18 = main_0.gameOptions_0.MapOverlayScenicLocations;
                flag19 = main_0.gameOptions_0.MapOverlayResearchLocations;
                flag20 = main_0.gameOptions_0.MapOverlayLongRangeScanners;
            }
            Pen pen = new Pen(System.Drawing.Color.FromArgb(int_15, 112, 112, 112), 1.5f);
            pen.DashStyle = DashStyle.Dash;
            int num = (int)((double)Galaxy.SizeX / double_15);
            double double_16 = (double)int_13 * double_15;
            double double_17 = (double)int_14 * double_15;
            int int_16 = 0;
            int int_17 = 0;
            int int_18 = 0;
            int int_19 = 0;
            method_225(int_11, int_12, double_16, double_17, ref int_16, ref int_17, ref int_18, ref int_19);
            double num2 = (double)num / (double)Galaxy.SectorMaxX;
            SizeF sizeF = graphics_0.MeasureString("H", font_3, 200, StringFormat.GenericTypographic);
            float num3 = (float)(num2 / 2.0 - (double)sizeF.Width / 2.0);
            float num4 = (float)(num2 / 2.0 - (double)sizeF.Height / 2.0);
            SolidBrush solidBrush = new SolidBrush(main_0.color_7);
            int num5 = galaxy_0.ResolveSector(int_16, int_12).X;
            int num6 = galaxy_0.ResolveSector(int_17, int_12).X;
            int num7 = galaxy_0.ResolveSector(int_11, int_18).Y;
            int num8 = galaxy_0.ResolveSector(int_11, int_19).Y;
            int num9 = galaxy_0.ResolveIndex(int_16, int_12).X;
            int num10 = galaxy_0.ResolveIndex(int_17, int_12).X;
            int num11 = galaxy_0.ResolveIndex(int_11, int_18).Y;
            int num12 = galaxy_0.ResolveIndex(int_11, int_19).Y;
            double num13 = 150.0;
            if (double_15 > num13)
            {
                graphics_0.SmoothingMode = SmoothingMode.None;
                graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
                bool flag21 = false;
                bool flag22 = false;
                float num14 = 0f;
                float num15 = int_14;
                if (num7 <= 1)
                {
                    num14 = (float)((double)(num7 * Galaxy.SectorSize - int_18) / double_15);
                }
                if (num8 >= Galaxy.SectorMaxX - 1)
                {
                    num15 = (float)((double)((num8 + 1) * Galaxy.SectorSize - int_18) / double_15);
                    flag22 = true;
                }
                float num16 = 0f;
                float num17 = int_13;
                if (num5 <= 1)
                {
                    num16 = (float)((double)(num5 * Galaxy.SectorSize - int_16) / double_15);
                }
                if (num6 >= Galaxy.SectorMaxY - 1)
                {
                    num17 = (float)((double)((num6 + 1) * Galaxy.SectorSize - int_16) / double_15);
                    flag21 = true;
                }
                for (int i = num5; i <= num6; i++)
                {
                    float num18 = (float)((double)(i * Galaxy.SectorSize - int_16) / double_15);
                    graphics_0.DrawLine(main_0.pen_1, num18, num14, num18, num15);
                    string s = ((char)(i + 65)).ToString();
                    float val = num14 - 1f;
                    val = Math.Max(0f, val);
                    graphics_0.DrawString(point: new PointF(num18 + num3, val), s: s, font: font_2, brush: solidBrush);
                    val = num15 - 14f;
                    val = Math.Min((float)base.ClientRectangle.Height - 11f, val);
                    graphics_0.DrawString(point: new PointF(num18 + num3, val), s: s, font: font_2, brush: solidBrush);
                }
                if (flag21)
                {
                    float num19 = (float)((double)((num6 + 1) * Galaxy.SectorSize - int_16) / double_15);
                    graphics_0.DrawLine(main_0.pen_1, num19, num14, num19, num15);
                }
                for (int j = num7; j <= num8; j++)
                {
                    float num20 = (float)((double)(j * Galaxy.SectorSize - int_18) / double_15);
                    graphics_0.DrawLine(main_0.pen_1, num16, num20, num17, num20);
                    string s2 = (j + 1).ToString();
                    float val2 = num16 - 2f;
                    val2 = Math.Max(0f, val2);
                    graphics_0.DrawString(point: new PointF(val2, num20 + num4), s: s2, font: font_2, brush: solidBrush);
                    val2 = num17 - 14f;
                    val2 = Math.Min((float)base.ClientRectangle.Width - 8f, val2);
                    graphics_0.DrawString(point: new PointF(val2, num20 + num4), s: s2, font: font_2, brush: solidBrush);
                }
                if (flag22)
                {
                    float num21 = (float)((double)((num8 + 1) * Galaxy.SectorSize - int_18) / double_15);
                    graphics_0.DrawLine(main_0.pen_1, num16, num21, num17, num21);
                }
                graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
                graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
                if (main_0._Game.SelectedObject != null)
                {
                    BuiltObject builtObject = null;
                    double num22 = 0.0;
                    if (main_0._Game.SelectedObject is BuiltObject)
                    {
                        builtObject = (BuiltObject)main_0._Game.SelectedObject;
                        num22 = builtObject.CurrentRange();
                    }
                    else if (main_0._Game.SelectedObject is ShipGroup)
                    {
                        ShipGroup shipGroup = (ShipGroup)main_0._Game.SelectedObject;
                        builtObject = shipGroup.LeadShip;
                        num22 = shipGroup.CurrentRange();
                    }
                    if (builtObject != null && builtObject.Role != BuiltObjectRole.Base && builtObject.Empire == empire)
                    {
                        if (num22 < (double)Galaxy.SizeX)
                        {
                            double num23 = builtObject.Xpos - num22;
                            double num24 = builtObject.Ypos - num22;
                            float num25 = (float)((num23 - (double)int_16) / double_15);
                            float num26 = (float)((num24 - (double)int_18) / double_15);
                            float num27 = (float)(num22 / double_15 * 2.0);
                            using Pen pen2 = new Pen(main_0.VuqPpUtdZU, 1f);
                            pen2.DashPattern = new float[2] { 2f, 10f };
                            RectangleF rectangleF_ = new RectangleF(num25, num26, num27, num27);
                            method_241(graphics_0, pen2, rectangleF_);
                        }
                        int int_20 = (int)((builtObject.Xpos - (double)int_16) / double_15);
                        int int_21 = (int)((builtObject.Ypos - (double)int_18) / double_15);
                        method_255(graphics_0, builtObject, int_20, int_21, int_16, int_18, double_15, bool_13: true, pen_3);
                    }
                }
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_16, int_18, int_17 - int_16, int_19 - int_18);
                method_175(graphics_0);
                if (flag20)
                {
                    for (int k = 0; k < empire.LongRangeScanners.Count; k++)
                    {
                        BuiltObject builtObject2 = empire.LongRangeScanners[k];
                        int sensorLongRange = builtObject2.SensorLongRange;
                        if (builtObject2.Role != BuiltObjectRole.Base)
                        {
                            int num28 = (int)builtObject2.Xpos - sensorLongRange;
                            int num29 = (int)builtObject2.Xpos + sensorLongRange;
                            int num30 = (int)builtObject2.Ypos - sensorLongRange;
                            int num31 = (int)builtObject2.Ypos + sensorLongRange;
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num28, num30, sensorLongRange * 2, sensorLongRange * 2);
                            if (rectangle.IntersectsWith(rect))
                            {
                                float num32 = (float)(((double)num28 - (double)int_16) / main_0.double_0);
                                float num33 = (float)(((double)num29 - (double)int_16) / main_0.double_0);
                                float num34 = (float)(((double)num30 - (double)int_18) / main_0.double_0);
                                float num35 = (float)(((double)num31 - (double)int_18) / main_0.double_0);
                                RectangleF rectangleF = new RectangleF(num32, num34, num33 - num32, num35 - num34);
                                rectangleF.Inflate(rectangleF.Width * 0.1f, rectangleF.Width * 0.1f);
                                System.Drawing.Color white = System.Drawing.Color.White;
                                ImageAttributes imageAttr = method_237(white, 0.1);
                                graphics_0.DrawImage(main_0.bitmap_188, new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), 0, 0, main_0.bitmap_188.Width, main_0.bitmap_188.Height, GraphicsUnit.Pixel, imageAttr);
                            }
                        }
                    }
                }
            }
            method_177(graphics_0);
            if (flag14)
            {
                method_246(graphics_0, empire, double_15, int_13, int_14, int_16, int_18);
            }
            for (int l = 0; l < galaxy_0.Systems.Count; l++)
            {
                SystemInfo systemInfo = galaxy_0.Systems[l];
                if (systemInfo.Sector.X < num5 || systemInfo.Sector.X > num6 || systemInfo.Sector.Y < num7 || systemInfo.Sector.Y > num8)
                {
                    continue;
                }
                float num36 = (float)((systemInfo.SystemStar.Xpos - (double)int_16) / double_15);
                float num37 = (float)((systemInfo.SystemStar.Ypos - (double)int_18) / double_15);
                SystemVisibilityStatus systemVisibilityStatus = empire.CheckSystemVisibilityStatus(l);
                int num38 = 0;
                float val3 = 0f;
                bool flag23 = false;
                bool flag24 = false;
                if (double_15 > num13 && !flag && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode) && systemInfo.DominantEmpire != null)
                {
                    num38 = Math.Min(systemInfo.DominantEmpire.TotalStrategicValue, 1500000);
                    val3 = (float)(Math.Pow(num38, 0.35) * 600.0);
                    float num39 = 0f;
                    float num40 = 0f;
                    HabitatList linkSystemStars = systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars;
                    if (linkSystemStars.Count > 0)
                    {
                        for (int m = 0; m < linkSystemStars.Count; m++)
                        {
                            Habitat habitat = linkSystemStars[m];
                            if (habitat == null)
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus2 = empire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                            if (systemVisibilityStatus2 == SystemVisibilityStatus.Visible || systemVisibilityStatus2 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                            {
                                using Pen pen3 = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 1f);
                                num39 = (float)((habitat.Xpos - (double)int_16) / double_15);
                                num40 = (float)((habitat.Ypos - (double)int_18) / double_15);
                                pen3.DashPattern = new float[2] { 3f, 5f };
                                graphics_0.DrawLine(pen3, num36, num37, num39, num40);
                            }
                        }
                    }
                    if (systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count > 0)
                    {
                        for (int n = 0; n < systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; n++)
                        {
                            Habitat habitat2 = systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[n];
                            if (linkSystemStars.Contains(habitat2) || habitat2 == null)
                            {
                                continue;
                            }
                            SystemInfo systemInfo2 = galaxy_0.Systems[habitat2.SystemIndex];
                            if (systemInfo2.Sector.X >= num5 && systemInfo2.Sector.X <= num6 && systemInfo2.Sector.Y >= num7 && systemInfo2.Sector.Y <= num8)
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus3 = empire.CheckSystemVisibilityStatus(habitat2.SystemIndex);
                            if (systemVisibilityStatus3 == SystemVisibilityStatus.Visible || systemVisibilityStatus3 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                            {
                                using Pen pen4 = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 1f);
                                num39 = (float)((habitat2.Xpos - (double)int_16) / double_15);
                                num40 = (float)((habitat2.Ypos - (double)int_18) / double_15);
                                pen4.DashPattern = new float[2] { 3f, 5f };
                                graphics_0.DrawLine(pen4, num36, num37, num39, num40);
                            }
                        }
                    }
                    EmpireSystemSummaryList otherEmpires = systemInfo.OtherEmpires;
                    if (otherEmpires != null)
                    {
                        for (int num41 = 0; num41 < otherEmpires.Count; num41++)
                        {
                            EmpireSystemSummary empireSystemSummary = otherEmpires[num41];
                            Empire empire2 = empireSystemSummary.Empire;
                            linkSystemStars = empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars;
                            if (linkSystemStars.Count > 0)
                            {
                                for (int num42 = 0; num42 < linkSystemStars.Count; num42++)
                                {
                                    Habitat habitat3 = linkSystemStars[num42];
                                    if (habitat3 == null)
                                    {
                                        continue;
                                    }
                                    SystemVisibilityStatus systemVisibilityStatus4 = empire.CheckSystemVisibilityStatus(habitat3.SystemIndex);
                                    if (systemVisibilityStatus4 == SystemVisibilityStatus.Visible || systemVisibilityStatus4 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                    {
                                        using Pen pen5 = new Pen(empire2.MainColor, 1f);
                                        num39 = (float)((habitat3.Xpos - (double)int_16) / double_15);
                                        num40 = (float)((habitat3.Ypos - (double)int_18) / double_15);
                                        pen5.DashPattern = new float[2] { 3f, 5f };
                                        graphics_0.DrawLine(pen5, num36, num37, num39, num40);
                                    }
                                }
                            }
                            if (empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count <= 0)
                            {
                                continue;
                            }
                            for (int num43 = 0; num43 < empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; num43++)
                            {
                                Habitat habitat4 = empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[num43];
                                if (linkSystemStars.Contains(habitat4) || habitat4 == null)
                                {
                                    continue;
                                }
                                SystemInfo systemInfo3 = galaxy_0.Systems[habitat4.SystemIndex];
                                if (systemInfo3.Sector.X >= num5 && systemInfo3.Sector.X <= num6 && systemInfo3.Sector.Y >= num7 && systemInfo3.Sector.Y <= num8)
                                {
                                    continue;
                                }
                                SystemVisibilityStatus systemVisibilityStatus5 = empire.CheckSystemVisibilityStatus(habitat4.SystemIndex);
                                if (systemVisibilityStatus5 == SystemVisibilityStatus.Visible || systemVisibilityStatus5 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                {
                                    using Pen pen6 = new Pen(empire2.MainColor, 1f);
                                    num39 = (float)((habitat4.Xpos - (double)int_16) / double_15);
                                    num40 = (float)((habitat4.Ypos - (double)int_18) / double_15);
                                    pen6.DashPattern = new float[2] { 3f, 5f };
                                    graphics_0.DrawLine(pen6, num36, num37, num39, num40);
                                }
                            }
                        }
                    }
                }
                val3 = Math.Max(Galaxy.MaxSolarSystemSize, val3);
                if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    val3 = systemInfo.SystemStar.NovaProgression;
                }
                val3 = (int)((double)val3 / double_15);
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    val3 = (int)((double)val3 * 0.7);
                }
                if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    val3 = (int)((double)val3 * 2.0);
                }
                if (!(num36 + val3 >= 0f) || !(num36 - val3 <= (float)int_13) || !(num37 + val3 >= 0f) || !(num37 - val3 <= (float)int_14))
                {
                    continue;
                }
                Pen pen7 = method_268(empire, flag, systemInfo, int_15);
                Brush brush = method_269(empire, flag, systemInfo, int_15);
                int val4 = (int)((double)systemInfo.SystemStar.Diameter / (double_15 / 70.0));
                val4 = Math.Min(Math.Max(8, val4), 25);
                if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    val4 = (int)((double)systemInfo.SystemStar.NovaProgression * 2.0 / double_15);
                }
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    val3 = Math.Max(6f, val3);
                }
                else if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    val3 = Math.Max(5f, val3);
                }
                else if (val3 * 2f - 4f <= (float)val4)
                {
                    val3 = val4 / 2 + 3;
                }
                if (double_15 > num13 && (systemInfo.PlayerPotentialColonies || ((systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored) && systemInfo.DominantEmpire == null && systemInfo.IndependentColonyCount > 0)))
                {
                    float num44 = (float)((double)Galaxy.MaxSolarSystemSize / double_15);
                    num44 *= 0.75f;
                    int num45 = (int)((double)val4 * 0.7);
                    if (num44 * 2f - 4f <= (float)num45)
                    {
                        num44 = num45 / 2 + 3;
                    }
                    RectangleF rectangleF_2 = new RectangleF(num36 - num44, num37 - num44, num44 * 2f, num44 * 2f);
                    method_241(graphics_0, pen, rectangleF_2);
                }
                RectangleF rectangleF_3 = new RectangleF(num36 - val3, num37 - val3, val3 * 2f, val3 * 2f);
                if (double_15 > num13)
                {
                    if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                    {
                        val3 *= 0.7f;
                        graphics_0.DrawLine(pen7, num36, num37 - val3, num36, num37 + val3);
                        graphics_0.DrawLine(pen7, num36 - val3, num37, num36 + val3, num37);
                    }
                    else if ((systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode) && (systemInfo.DominantEmpire != null || systemInfo.IndependentColonyCount > 0))
                    {
                        method_241(graphics_0, pen7, rectangleF_3);
                    }
                }
                Bitmap bitmap = null;
                bitmap = ((systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud) ? bitmap_1[0] : ((systemInfo.SystemStar.Type != HabitatType.SuperNova) ? bitmap_1[systemInfo.SystemStar.MapPictureRef] : main_0.bitmap_206[systemInfo.SystemStar.NovaImageIndexMajor]));
                double num46 = (double)bitmap.Height / (double)bitmap.Width;
                int num47 = val4;
                int num48 = (int)((double)val4 * num46);
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle((int)num36 - num47 / 2, (int)num37 - num48 / 2, num47, num48);
                double num49 = 0.6;
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    num49 = 1.0;
                }
                if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    num49 = 1.3;
                }
                else if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    num49 = 1.3;
                }
                int val5 = (int)((double)val4 * num49);
                int val6 = (int)((double)val4 * num49 * num46);
                val5 = Math.Max(8, val5);
                val6 = Math.Max(8, val6);
                System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle((int)num36 - val5 / 2, (int)num37 - val6 / 2, val5, val6);
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    rectangle_ = new System.Drawing.Rectangle((int)num36 - (int)val3, (int)num37 - (int)val3, (int)val3 * 2, (int)val3 * 2);
                }
                else
                {
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova && systemInfo.SystemStar.Type != HabitatType.BlackHole && double_15 < 150.0)
                    {
                        int val7 = (int)((double)systemInfo.SystemStar.Diameter / double_15);
                        int num50 = (int)((systemInfo.SystemStar.Xpos - (double)int_16) / double_15) - 1;
                        int num51 = (int)((systemInfo.SystemStar.Ypos - (double)int_18) / double_15) - 1;
                        val7 = Math.Max(10, val7);
                        rectangle2 = new System.Drawing.Rectangle(num50 - val7 / 2, num51 - val7 / 2, val7 + 2, val7 + 2);
                    }
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova || rectangle2.Width >= 400)
                    {
                        method_175(graphics_0);
                    }
                    if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                    {
                        if (double_15 < 5100.0)
                        {
                            val3 = Math.Max(11f, val3);
                            val3 = Math.Min(17f, val3);
                        }
                        rectangle2 = new System.Drawing.Rectangle((int)num36 - (int)val3, (int)num37 - (int)val3, (int)val3 * 2, (int)val3 * 2);
                        rectangle_ = new System.Drawing.Rectangle(rectangle2.Location, rectangle2.Size);
                        rectangleF_3 = new System.Drawing.Rectangle(rectangle2.Location, rectangle2.Size);
                    }
                    if (double_15 > method_60(systemInfo.SystemStar.Type) && (double_15 < 5100.0 || systemInfo.SystemStar.Type == HabitatType.BlackHole || systemInfo.SystemStar.Type == HabitatType.SuperNova))
                    {
                        graphics_0.DrawImage(bitmap, rectangle2);
                    }
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova && systemInfo.SystemStar.Type != HabitatType.BlackHole)
                    {
                        System.Drawing.Color color_ = method_120(bitmap);
                        double double_18 = 2.0;
                        if (double_15 < 5100.0)
                        {
                            double double_19 = 30.0;
                            double num52 = method_61(systemInfo.SystemStar.Type, out double_19);
                            if (double_15 > double_19)
                            {
                                if (double_15 < num52)
                                {
                                    double num53 = num52 - double_19;
                                    double_18 = 1.0 + (double_15 - double_19) / num53 * 1.0;
                                }
                                method_235(graphics_0, galaxy_0, systemInfo.SystemStar, color_, rectangle2, double_18, bool_13: true, bool_14: false);
                            }
                        }
                        else
                        {
                            double_18 = 1.3;
                            method_235(graphics_0, galaxy_0, systemInfo.SystemStar, color_, rectangle2, double_18, bool_13: false, bool_14: true);
                        }
                        method_177(graphics_0);
                    }
                }
                method_177(graphics_0);
                if (double_15 > num13)
                {
                    if (main_0._Game.SelectedObject == systemInfo)
                    {
                        if ((float)rectangle_.Width > rectangleF_3.Width)
                        {
                            method_210(rectangle_.X - 3, rectangle_.Y - 3, rectangle_.X + rectangle_.Width + 4, rectangle_.Y + rectangle_.Height + 4, graphics_0);
                        }
                        else
                        {
                            method_211(rectangleF_3.X - 3f, rectangleF_3.Y - 3f, rectangleF_3.X + rectangleF_3.Width + 4f, rectangleF_3.Y + rectangleF_3.Height + 4f, graphics_0);
                        }
                    }
                    bool flag25 = false;
                    bool flag26 = false;
                    Font font = font_2;
                    float num54 = 0f;
                    float num55 = 3f;
                    if (double_15 < 4000.0)
                    {
                        if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible || main_0._Game.GodMode)
                        {
                            flag25 = true;
                            if (systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                            {
                                if (systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire.Capital != null)
                                {
                                    Habitat habitat5 = Galaxy.DetermineHabitatSystemStar(systemInfo.DominantEmpire.Empire.Capital);
                                    if (habitat5 == systemInfo.SystemStar)
                                    {
                                        flag23 = true;
                                    }
                                    else if (systemInfo.DominantEmpire.Empire.CapitalSystemStars.Contains(systemInfo.SystemStar))
                                    {
                                        flag24 = true;
                                    }
                                }
                                flag26 = false;
                                font = font_1;
                                num54 = 3f;
                            }
                        }
                    }
                    else
                    {
                        num55 = 2f;
                        flag26 = false;
                        if (systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                        {
                            flag25 = true;
                            font = font_3;
                        }
                    }
                    if (flag25)
                    {
                        int num56 = 0;
                        float num57 = 0f;
                        float num58 = 0f;
                        if (flag23 || flag24)
                        {
                            num57 = font.Height - 8;
                            num56 = (int)num57;
                        }
                        if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].IsRefuellingPoint)
                        {
                            num58 = font.Height - 4;
                            num56 += (int)num58;
                        }
                        string name = systemInfo.SystemStar.Name;
                        int num59 = (int)(num36 + 1f + (float)num56 + val3);
                        int num60 = (int)num37 - (font.Height + 1);
                        System.Drawing.Point point_ = new System.Drawing.Point(num59, num60);
                        method_267(graphics_0, name, font, point_, brush);
                        if (flag23)
                        {
                            graphics_0.DrawImage(srcRect: new System.Drawing.Rectangle(0, 0, main_0.bitmap_44.Width, main_0.bitmap_44.Height), destRect: new System.Drawing.Rectangle((int)(num36 + 1f + val3), (int)(num37 - (float)(font.Height - 1)), (int)num57, (int)num57), image: main_0.bitmap_44, srcUnit: GraphicsUnit.Pixel);
                        }
                        else if (flag24)
                        {
                            graphics_0.DrawImage(srcRect: new System.Drawing.Rectangle(0, 0, main_0.bitmap_43.Width, main_0.bitmap_43.Height), destRect: new System.Drawing.Rectangle((int)(num36 + 1f + val3), (int)(num37 - (float)(font.Height - 1)), (int)num57, (int)num57), image: main_0.bitmap_43, srcUnit: GraphicsUnit.Pixel);
                        }
                        if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].IsRefuellingPoint)
                        {
                            graphics_0.DrawImage(srcRect: new System.Drawing.Rectangle(0, 0, main_0.bitmap_55.Width, main_0.bitmap_55.Height), destRect: new System.Drawing.Rectangle((int)(num36 + 1f + val3 + num57), (int)(num37 - (float)(font.Height - 1)), (int)num58, (int)num58), image: main_0.bitmap_55, srcUnit: GraphicsUnit.Pixel);
                        }
                        float num61 = graphics_0.MeasureString(name, font, 300, StringFormat.GenericDefault).Width;
                        if (systemInfo.HasRuins)
                        {
                            float num62 = num36 + val3 + 1f + (float)num56 + num61;
                            float num63 = num37 - ((float)font.Height - 1f) + num54;
                            RectangleF rectangleF_4 = new RectangleF(num62, num63 + 5f, num55, num55);
                            method_243(graphics_0, brush, rectangleF_4);
                            rectangleF_4 = new RectangleF(num62 + 6f, num63 + 5f, num55, num55);
                            method_243(graphics_0, brush, rectangleF_4);
                            rectangleF_4 = new RectangleF(num62 + 3f, num63, num55, num55);
                            method_243(graphics_0, brush, rectangleF_4);
                        }
                        if (flag26 && systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                        {
                            int num64 = 0;
                            int num65 = (int)num37 + 1;
                            System.Drawing.Point point_2 = new System.Drawing.Point(num59 + 0, num65);
                            string string_ = systemInfo.DominantEmpire.TotalStrategicValue.ToString("0,K");
                            int num66 = (int)graphics_0.MeasureString(string_, Font, 200, StringFormat.GenericTypographic).Width;
                            method_266(graphics_0, string_, Font, point_2);
                            num64 = 0 + num66;
                            num64 += 10;
                            graphics_0.DrawImageUnscaled(point: new System.Drawing.Point(num59 + num64, num65), image: main_0.bitmap_37);
                            num64 += main_0.bitmap_37.Width;
                            point_2 = new System.Drawing.Point(num59 + num64, num65);
                            string string_2 = systemInfo.DominantEmpire.ColonyCount.ToString();
                            int num67 = (int)graphics_0.MeasureString(string_2, Font, 200, StringFormat.GenericTypographic).Width;
                            method_266(graphics_0, string_2, Font, point_2);
                            num64 += num67;
                            num64 += 8;
                            graphics_0.DrawImageUnscaled(point: new System.Drawing.Point(num59 + num64, num65), image: main_0.bitmap_38);
                            num64 += main_0.bitmap_38.Width - 2;
                            point_2 = new System.Drawing.Point(num59 + num64, num65);
                            string string_3 = systemInfo.DominantEmpire.Firepower.ToString();
                            if (systemVisibilityStatus != SystemVisibilityStatus.Visible && !main_0._Game.GodMode && !empire.EmpiresViewable.Contains(systemInfo.DominantEmpire.Empire))
                            {
                                string_3 = "?";
                            }
                            _ = graphics_0.MeasureString(string_3, Font, 200, StringFormat.GenericTypographic).Width;
                            method_266(graphics_0, string_3, Font, point_2);
                        }
                    }
                    if (systemInfo_0 != null && systemInfo == systemInfo_0)
                    {
                        if ((float)rectangle2.Width > rectangleF_3.Width)
                        {
                            method_207(rectangle_, graphics_0);
                        }
                        else
                        {
                            method_206(rectangleF_3, graphics_0);
                        }
                    }
                    else if (systemInfo.SystemStar != null && empire.CheckSystemExplored(systemInfo.SystemStar))
                    {
                        bool flag27 = false;
                        if (flag17)
                        {
                            if (systemInfo.PlayerPotentialColonies)
                            {
                                flag27 = true;
                            }
                            if (systemInfo.IndependentColonyCount > 0)
                            {
                                Empire empire3 = galaxy_0.CheckSystemOwnership(systemInfo.SystemStar);
                                if (empire3 == null || empire3 == empire)
                                {
                                    flag27 = true;
                                }
                            }
                        }
                        if (flag18 && systemInfo.HasScenery)
                        {
                            if (systemInfo.SystemStar != null && systemInfo.SystemStar.ScenicFactor > 0f)
                            {
                                flag27 = true;
                            }
                            if (!flag27 && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                            {
                                for (int num68 = 0; num68 < systemInfo.Habitats.Count; num68++)
                                {
                                    Habitat habitat6 = systemInfo.Habitats[num68];
                                    if (habitat6 != null && habitat6.ScenicFactor > 0f && !galaxy_0.CheckAlreadyHaveMiningStationAtHabitat(habitat6, empire))
                                    {
                                        flag27 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag19 && systemInfo.HasResearchBonus)
                        {
                            if (systemInfo.SystemStar != null && systemInfo.SystemStar.ResearchBonus > 0)
                            {
                                flag27 = true;
                            }
                            if (!flag27 && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                            {
                                for (int num69 = 0; num69 < systemInfo.Habitats.Count; num69++)
                                {
                                    Habitat habitat7 = systemInfo.Habitats[num69];
                                    if (habitat7 != null && habitat7.ResearchBonus > 0 && !empire.CheckResearchStationAtLocation(habitat7))
                                    {
                                        flag27 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag27)
                        {
                            if ((float)rectangle2.Width > rectangleF_3.Width)
                            {
                                method_207(rectangle_, graphics_0);
                            }
                            else
                            {
                                method_206(rectangleF_3, graphics_0);
                            }
                        }
                    }
                }
                brush.Dispose();
                pen7.Dispose();
            }
            if (main_0.double_0 > 70.0 && main_0.double_0 <= main_0.double_5)
            {
                for (int num70 = 0; num70 < galaxy_0.GalaxyLocations.Count; num70++)
                {
                    GalaxyLocation galaxyLocation = galaxy_0.GalaxyLocations[num70];
                    if (main_0._Game.GodMode || empire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        int num71 = (int)(((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0 - (double)int_16) / double_15);
                        int num72 = (int)(((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0 - (double)int_18) / double_15);
                        if (num71 > -100 && num71 < base.ClientRectangle.Width + 100 && num72 > -100 && num72 < base.ClientRectangle.Height + 100 && galaxyLocation.ShowName)
                        {
                            Font font2 = font_0;
                            font2 = ((galaxyLocation.Type == GalaxyLocationType.NebulaCloud) ? ((double_15 > 4000.0) ? font_2 : ((!(double_15 > 1000.0)) ? font_1 : font_0)) : ((double_15 > 4000.0) ? font_3 : ((!(double_15 > 1000.0)) ? font_0 : font_2)));
                            SizeF sizeF2 = graphics_0.MeasureString(galaxyLocation.Name, font2, 250, StringFormat.GenericTypographic);
                            int num73 = num71 - (int)(sizeF2.Width / 2f);
                            int num74 = num72 - (int)(sizeF2.Height / 2f);
                            if (galaxyLocation.Type != GalaxyLocationType.NebulaCloud)
                            {
                                num73 -= 5;
                                num74 -= 20;
                            }
                            method_267(graphics_0, galaxyLocation.Name, font2, new System.Drawing.Point(num73, num74), solidBrush_0);
                        }
                    }
                    if (galaxyLocation.Effect == GalaxyLocationEffectType.LightningDamage)
                    {
                        int num75 = (int)(((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0 - (double)int_16) / double_15);
                        int num76 = (int)(((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0 - (double)int_18) / double_15);
                        if (num75 > -100 && num75 < base.ClientRectangle.Width + 100 && num76 > -100 && num76 < base.ClientRectangle.Height + 100)
                        {
                            method_261(graphics_0, num75, num76, galaxyLocation, double_15);
                        }
                    }
                }
            }
            DateTime now = DateTime.Now;
            for (int num77 = 0; num77 < list_29.Count; num77++)
            {
                EventPing eventPing = list_29[num77];
                method_231(now, graphics_0, main_0.double_0, eventPing.Point.X, eventPing.Point.Y, main_0.int_13, main_0.int_14, int_16, int_17, int_18, int_19);
            }
            dateTime_3 = now;
            method_227(graphics_0, int_16, int_17, int_18, int_19);
            System.Drawing.Color.FromArgb(int_15, 0, 0, 255);
            System.Drawing.Color.FromArgb(int_15, 96, 96, 255);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(int_15, 0, 0, 255);
            System.Drawing.Color.FromArgb(int_15, 96, 96, 255);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(int_15, 192, 192, 0);
            System.Drawing.Color.FromArgb(int_15, 192, 192, 96);
            System.Drawing.Color color3 = System.Drawing.Color.FromArgb(int_15, 255, 0, 0);
            System.Drawing.Color.FromArgb(int_15, 255, 96, 96);
            DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
            for (int num78 = 0; num78 < empire.DiplomaticRelations.Count; num78++)
            {
                DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations[num78];
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            if (main_0.double_0 <= main_0.double_5)
            {
                for (int num79 = num9; num79 <= num10; num79++)
                {
                    for (int num80 = num11; num80 <= num12; num80++)
                    {
                        BuiltObjectList builtObjectList = galaxy_0.BuiltObjectIndex[num79][num80];
                        for (int num81 = 0; num81 < builtObjectList.Count; num81++)
                        {
                            BuiltObject builtObject3 = null;
                            if (builtObjectList.Count > num81)
                            {
                                builtObject3 = builtObjectList[num81];
                                if (builtObject3 == null || builtObject3.HasBeenDestroyed)
                                {
                                    builtObject3 = null;
                                }
                            }
                            if (builtObject3 == null)
                            {
                                continue;
                            }
                            bool flag28 = false;
                            bool flag29 = false;
                            bool flag30 = false;
                            if (builtObject3.Empire == empire)
                            {
                                _ = builtObject3.Empire.MainColor;
                            }
                            else if (galaxy_0.PirateEmpires.Contains(builtObject3.Empire))
                            {
                                System.Drawing.Color.FromArgb(48, 48, 48);
                                flag29 = true;
                            }
                            else if (empireList.Contains(builtObject3.Empire))
                            {
                                _ = builtObject3.Empire.MainColor;
                                flag29 = true;
                            }
                            else if (builtObject3.Empire != null)
                            {
                                _ = builtObject3.Empire.MainColor;
                            }
                            switch (builtObject3.SubRole)
                            {
                                case BuiltObjectSubRole.Escort:
                                case BuiltObjectSubRole.Frigate:
                                case BuiltObjectSubRole.Destroyer:
                                case BuiltObjectSubRole.Cruiser:
                                case BuiltObjectSubRole.CapitalShip:
                                case BuiltObjectSubRole.TroopTransport:
                                case BuiltObjectSubRole.Carrier:
                                    flag28 = flag4;
                                    if (flag29 && !flag28 && flag12)
                                    {
                                        flag28 = true;
                                    }
                                    if (flag30 && !flag28 && flag13)
                                    {
                                        flag28 = true;
                                    }
                                    break;
                                case BuiltObjectSubRole.ResupplyShip:
                                    flag28 = flag3;
                                    if (flag29 && !flag28 && flag12)
                                    {
                                        flag28 = true;
                                    }
                                    break;
                                case BuiltObjectSubRole.ExplorationShip:
                                    flag28 = flag7;
                                    break;
                                case BuiltObjectSubRole.ColonyShip:
                                    flag28 = flag8;
                                    break;
                                case BuiltObjectSubRole.ConstructionShip:
                                    flag28 = flag9;
                                    break;
                                case BuiltObjectSubRole.SmallFreighter:
                                case BuiltObjectSubRole.MediumFreighter:
                                case BuiltObjectSubRole.LargeFreighter:
                                case BuiltObjectSubRole.PassengerShip:
                                case BuiltObjectSubRole.GasMiningShip:
                                case BuiltObjectSubRole.MiningShip:
                                    flag28 = flag10;
                                    break;
                                case BuiltObjectSubRole.SmallSpacePort:
                                case BuiltObjectSubRole.MediumSpacePort:
                                case BuiltObjectSubRole.LargeSpacePort:
                                    flag28 = flag5;
                                    break;
                                case BuiltObjectSubRole.GasMiningStation:
                                case BuiltObjectSubRole.MiningStation:
                                case BuiltObjectSubRole.ResortBase:
                                case BuiltObjectSubRole.GenericBase:
                                case BuiltObjectSubRole.EnergyResearchStation:
                                case BuiltObjectSubRole.WeaponsResearchStation:
                                case BuiltObjectSubRole.HighTechResearchStation:
                                case BuiltObjectSubRole.MonitoringStation:
                                case BuiltObjectSubRole.DefensiveBase:
                                    flag28 = flag6;
                                    break;
                            }
                            bool flag31 = false;
                            if (flag28)
                            {
                                if (builtObject3.Empire == empire)
                                {
                                    flag31 = true;
                                }
                                else if (empire.IsObjectVisibleToThisEmpireImprecise(builtObject3))
                                {
                                    flag31 = true;
                                }
                            }
                            if (builtObject3.ShipGroup != null && builtObject3.ShipGroup.LeadShip == builtObject3)
                            {
                                flag31 = false;
                            }
                            if (!flag31)
                            {
                                continue;
                            }
                            int num82 = (int)((builtObject3.Xpos - (double)int_16) / double_15);
                            int num83 = (int)((builtObject3.Ypos - (double)int_18) / double_15);
                            if (num82 < 0 || num82 > int_13 || num83 < 0 || num83 > int_14)
                            {
                                continue;
                            }
                            if (flag15 || flag16)
                            {
                                bool flag32 = false;
                                flag32 = ((builtObject3.Owner == null) ? flag16 : flag15);
                                if (flag32 && builtObject3.Empire == empire)
                                {
                                    method_254(graphics_0, builtObject3, num82, num83, int_16, int_18, double_15, bool_13: false);
                                }
                            }
                            int num84 = 10;
                            if (double_15 < 400.0)
                            {
                                num84 = (int)((double)num84 * Math.Sqrt(400.0 / double_15));
                            }
                            if (double_15 > 4000.0)
                            {
                                num84 = (int)((double)num84 / (double_15 / 4000.0));
                            }
                            if (builtObject3.Owner == null)
                            {
                                System.Drawing.Color.FromArgb(160, 160, 160);
                            }
                            if (num84 < 6)
                            {
                                num84 = 6;
                            }
                            int num85 = 12;
                            if (builtObject3.Role == BuiltObjectRole.Base)
                            {
                                num85 = 15;
                            }
                            if (num84 > num85)
                            {
                                num84 = num85;
                            }
                            if (builtObject3.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject3.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject3.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                DrawShipSymbol(graphics_0, builtObject3, ResolveShipSymbolColor(builtObject3), num82 - num84 / 2, num83 - num84 / 2, num84, num84, num84, num84, fillInterior: true, main_0.double_0);
                                if (main_0._Game.SelectedObject == builtObject3)
                                {
                                    num84 = (int)((double)num84 * 1.6);
                                    method_210(num82 - num84 / 2, num83 - num84 / 2, num82 + num84 / 2, num83 + num84 / 2, graphics_0);
                                }
                                else if (main_0._Game.SelectedObject is BuiltObjectList)
                                {
                                    BuiltObjectList builtObjectList2 = (BuiltObjectList)main_0._Game.SelectedObject;
                                    if (builtObjectList2.Contains(builtObject3))
                                    {
                                        num84 = (int)((double)num84 * 1.6);
                                        method_210(num82 - num84 / 2, num83 - num84 / 2, num82 + num84 / 2, num83 + num84 / 2, graphics_0);
                                    }
                                }
                            }
                            if ((!flag29 && galaxy_0.FastTestShipInColonizedSystem(builtObject3)) || builtObject3.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject3.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject3.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                continue;
                            }
                            DrawShipSymbol(graphics_0, builtObject3, ResolveShipSymbolColor(builtObject3), num82 - num84 / 2, num83 - num84 / 2, num84, num84, num84, num84, fillInterior: true, main_0.double_0);
                            if (main_0._Game.SelectedObject == builtObject3)
                            {
                                num84 = (int)((double)num84 * 1.6);
                                method_210(num82 - num84 / 2, num83 - num84 / 2, num82 + num84 / 2, num83 + num84 / 2, graphics_0);
                            }
                            else if (main_0._Game.SelectedObject is BuiltObjectList)
                            {
                                BuiltObjectList builtObjectList3 = (BuiltObjectList)main_0._Game.SelectedObject;
                                if (builtObjectList3.Contains(builtObject3))
                                {
                                    num84 = (int)((double)num84 * 1.6);
                                    method_210(num82 - num84 / 2, num83 - num84 / 2, num82 + num84 / 2, num83 + num84 / 2, graphics_0);
                                }
                            }
                        }
                    }
                }
            }
            if (double_15 > num13)
            {
                BuiltObjectList builtObjectList4 = empire.KnownPirateBases;
                if (main_0._Game.GodMode)
                {
                    builtObjectList4 = new BuiltObjectList();
                    for (int num86 = 0; num86 < galaxy_0.PirateEmpires.Count; num86++)
                    {
                        Empire empire4 = galaxy_0.PirateEmpires[num86];
                        if (empire4.PirateEmpireBaseHabitat != null && empire4.PirateEmpireBaseHabitat.BasesAtHabitat != null && empire4.PirateEmpireBaseHabitat.BasesAtHabitat.Count > 0)
                        {
                            builtObjectList4.Add(empire4.PirateEmpireBaseHabitat.BasesAtHabitat[0]);
                        }
                    }
                }
                for (int num87 = 0; num87 < builtObjectList4.Count; num87++)
                {
                    BuiltObject builtObject4 = builtObjectList4[num87];
                    int num88 = (int)((builtObject4.Xpos - (double)int_16) / double_15);
                    int num89 = (int)((builtObject4.Ypos - (double)int_18) / double_15);
                    if (num88 >= 0 && num88 <= int_13 && num89 >= 0 && num89 <= int_14)
                    {
                        int num90 = 27;
                        int num91 = 16;
                        if (double_15 < 600.0)
                        {
                            num90 = 35;
                            num91 = 21;
                        }
                        else if (double_15 > 4000.0)
                        {
                            num90 = 20;
                            num91 = 12;
                        }
                        System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(num88 - num90 / 2, num89 - num91 / 2, num90, num91);
                        if (builtObject4.Empire != null && builtObject4.Empire.SmallFlagPicture != null)
                        {
                            graphics_0.DrawImage(builtObject4.Empire.SmallFlagPicture, rect2);
                        }
                        else
                        {
                            graphics_0.DrawImage(main_0.bitmap_49, rect2);
                        }
                        System.Drawing.Color color4 = System.Drawing.Color.FromArgb(80, 80, 80);
                        using Pen pen8 = new Pen(color4, 1f);
                        graphics_0.DrawRectangle(pen8, rect2);
                    }
                }
                int num92 = 22;
                if (double_15 < 400.0)
                {
                    num92 = (int)((double)num92 * Math.Sqrt(400.0 / double_15));
                }
                if (double_15 > 4000.0)
                {
                    num92 = (int)((double)num92 / (double_15 / 4000.0));
                }
                if (num92 < 12)
                {
                    num92 = 12;
                }
                SolidBrush solidBrush2 = new SolidBrush(color);
                if (empire != null)
                {
                    if (flag2)
                    {
                        method_256(graphics_0, solidBrush2, empire, double_15, num92, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                    }
                    SolidBrush solidBrush3 = new SolidBrush(color2);
                    SolidBrush solidBrush4 = new SolidBrush(color3);
                    for (int num93 = 0; num93 < galaxy_0.Empires.Count; num93++)
                    {
                        Empire empire5 = galaxy_0.Empires[num93];
                        if (empire5 == empire)
                        {
                            continue;
                        }
                        if (empireList.Contains(empire5))
                        {
                            if (flag2 || flag11)
                            {
                                method_256(graphics_0, solidBrush4, empire5, double_15, num92, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                            }
                        }
                        else if (flag2)
                        {
                            method_256(graphics_0, solidBrush3, empire5, double_15, num92, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                        }
                    }
                    solidBrush3?.Dispose();
                    solidBrush4?.Dispose();
                }
                solidBrush2?.Dispose();
            }
            solidBrush?.Dispose();
            pen.Dispose();
        }

        private System.Drawing.Rectangle method_249(RectangleF rectangleF_1)
        {
            int num = (int)(rectangleF_1.X + 0.5f);
            int num2 = (int)(rectangleF_1.Y + 0.5f);
            int num3 = (int)(rectangleF_1.Width + 0.5f);
            int num4 = (int)(rectangleF_1.Height + 0.5f);
            return new System.Drawing.Rectangle(num, num2, num3, num4);
        }

        public EventPing[] ToArrayThreadSafe(List<EventPing> list)
        {
            int num = 0;
            if (list != null)
            {
                num = list.Count;
            }
            EventPing[] array = new EventPing[num];
            try
            {
                for (int i = 0; i < num; i++)
                {
                    array[i] = list[i];
                }
                return array;
            }
            catch
            {
                return array;
            }
        }

        private void method_250(SpriteBatch spriteBatch_2, SpriteBatch spriteBatch_3, int int_11, int int_12, int int_13, int int_14, double double_15, int int_15)
        {
            int_15 = 255;
            bool flag = false;
            if (main_0.gameOptions_0 != null)
            {
                flag = main_0.gameOptions_0.CleanGalaxyView;
            }
            DateTime currentDateTime = galaxy_0.CurrentDateTime;
            long currentStarDate = galaxy_0.CurrentStarDate;
            Empire empire = main_0._Game.PlayerEmpire;
            bool flag2 = false;
            if (main_0.empire_1 != null)
            {
                empire = main_0.empire_1;
                flag2 = true;
            }
            bool flag3 = true;
            bool flag4 = true;
            bool flag5 = true;
            bool flag6 = true;
            bool flag7 = true;
            bool flag8 = true;
            bool flag9 = true;
            bool flag10 = true;
            bool flag11 = true;
            bool flag12 = true;
            bool flag13 = true;
            bool flag14 = true;
            if (main_0.gameOptions_0 != null && !main_0._Game.GodMode && double_15 > 3500.0)
            {
                flag3 = main_0.gameOptions_0.GalaxyViewDisplayFleets;
                flag4 = main_0.gameOptions_0.GalaxyViewDisplayResupplyShips;
                flag5 = main_0.gameOptions_0.GalaxyViewDisplayMilitaryShips;
                flag6 = main_0.gameOptions_0.GalaxyViewDisplaySpacePorts;
                flag7 = main_0.gameOptions_0.GalaxyViewDisplayOtherBases;
                flag8 = main_0.gameOptions_0.GalaxyViewDisplayExplorationShips;
                flag9 = main_0.gameOptions_0.GalaxyViewDisplayColonyShips;
                flag10 = main_0.gameOptions_0.GalaxyViewDisplayConstructionShips;
                flag11 = main_0.gameOptions_0.GalaxyViewDisplayCivilianShips;
                flag12 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets;
                flag13 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips;
                flag14 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysPirates;
            }
            bool flag15 = false;
            bool flag16 = false;
            bool flag17 = false;
            bool flag18 = false;
            bool flag19 = false;
            bool flag20 = false;
            if (main_0.gameOptions_0 != null)
            {
                flag15 = main_0.gameOptions_0.MapOverlayFleetPostures;
                flag16 = main_0.gameOptions_0.MapOverlayTravelVectorsState;
                flag17 = main_0.gameOptions_0.MapOverlayTravelVectorsPrivate;
                flag18 = main_0.gameOptions_0.MapOverlayPotentialColonies;
                flag19 = main_0.gameOptions_0.MapOverlayScenicLocations;
                flag20 = main_0.gameOptions_0.MapOverlayResearchLocations;
            }
            System.Drawing.Color color = System.Drawing.Color.FromArgb(int_15, 112, 112, 112);
            double num = 150000.0;
            float num2 = (float)((num / double_15 + 0.5) * 1.1);
            float num3 = num2 / 2f;
            int num4 = (int)((double)Galaxy.SizeX / double_15);
            double double_16 = (double)int_13 * double_15;
            double double_17 = (double)int_14 * double_15;
            int int_16 = 0;
            int int_17 = 0;
            int int_18 = 0;
            int int_19 = 0;
            method_225(int_11, int_12, double_16, double_17, ref int_16, ref int_17, ref int_18, ref int_19);
            double num5 = (double)num4 / (double)Galaxy.SectorMaxX;
            Vector2 vector = spriteFont_0.MeasureString("H");
            float num6 = (float)(num5 / 2.0 - (double)vector.X / 2.0);
            float num7 = (float)(num5 / 2.0 - (double)vector.Y / 2.0);
            SolidBrush solidBrush = new SolidBrush(main_0.color_7);
            int num8 = galaxy_0.ResolveSector(int_16, int_12).X;
            int num9 = galaxy_0.ResolveSector(int_17, int_12).X;
            int num10 = galaxy_0.ResolveSector(int_11, int_18).Y;
            int num11 = galaxy_0.ResolveSector(int_11, int_19).Y;
            int num12 = galaxy_0.ResolveIndex(int_16, int_12).X;
            int num13 = galaxy_0.ResolveIndex(int_17, int_12).X;
            int num14 = galaxy_0.ResolveIndex(int_11, int_18).Y;
            int num15 = galaxy_0.ResolveIndex(int_11, int_19).Y;
            double num16 = 150.0;
            if (double_15 > num16)
            {
                if (!flag)
                {
                    bool flag21 = false;
                    bool flag22 = false;
                    float num17 = 0f;
                    float num18 = int_14;
                    if (num10 <= 1)
                    {
                        num17 = (float)((double)(num10 * Galaxy.SectorSize - int_18) / double_15);
                    }
                    if (num11 >= Galaxy.SectorMaxX - 1)
                    {
                        num18 = (float)((double)((num11 + 1) * Galaxy.SectorSize - int_18) / double_15);
                        flag22 = true;
                    }
                    float num19 = 0f;
                    float num20 = int_13;
                    if (num8 <= 1)
                    {
                        num19 = (float)((double)(num8 * Galaxy.SectorSize - int_16) / double_15);
                    }
                    if (num9 >= Galaxy.SectorMaxY - 1)
                    {
                        num20 = (float)((double)((num9 + 1) * Galaxy.SectorSize - int_16) / double_15);
                        flag21 = true;
                    }
                    for (int i = num8; i <= num9; i++)
                    {
                        float num21 = (float)((double)(i * Galaxy.SectorSize - int_16) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num21, num17, num21, num18, main_0.color_5, 1);
                        string text = ((char)(i + 65)).ToString();
                        float val = num17 - 1f;
                        val = Math.Max(0f, val);
                        XnaDrawingHelper.DrawString(point: new PointF(num21 + num6, val), spriteBatch: spriteBatch_2, text: text, font: spriteFont_1, color: solidBrush.Color);
                        val = num18 - 14f;
                        val = Math.Min((float)base.ClientRectangle.Height - 11f, val);
                        XnaDrawingHelper.DrawString(point: new PointF(num21 + num6, val), spriteBatch: spriteBatch_2, text: text, font: spriteFont_1, color: solidBrush.Color);
                    }
                    if (flag21)
                    {
                        float num22 = (float)((double)((num9 + 1) * Galaxy.SectorSize - int_16) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num22, num17, num22, num18, main_0.color_5, 1);
                    }
                    for (int j = num10; j <= num11; j++)
                    {
                        float num23 = (float)((double)(j * Galaxy.SectorSize - int_18) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num19, num23, num20, num23, main_0.color_5, 1);
                        string text2 = (j + 1).ToString();
                        float val2 = num19 - 2f;
                        val2 = Math.Max(0f, val2);
                        XnaDrawingHelper.DrawString(point: new PointF(val2, num23 + num7), spriteBatch: spriteBatch_2, text: text2, font: spriteFont_1, color: solidBrush.Color);
                        val2 = num20 - 14f;
                        val2 = Math.Min((float)base.ClientRectangle.Width - 8f, val2);
                        XnaDrawingHelper.DrawString(point: new PointF(val2, num23 + num7), spriteBatch: spriteBatch_2, text: text2, font: spriteFont_1, color: solidBrush.Color);
                    }
                    if (flag22)
                    {
                        float num24 = (float)((double)((num11 + 1) * Galaxy.SectorSize - int_18) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num19, num24, num20, num24, main_0.color_5, 1);
                    }
                }
                new System.Drawing.Rectangle(int_16, int_18, int_17 - int_16, int_19 - int_18);
            }
            if (flag15)
            {
                method_247(spriteBatch_2, empire, double_15, int_13, int_14, int_16, int_18);
            }
            for (int k = 0; k < galaxy_0.Systems.Count; k++)
            {
                SystemInfo systemInfo = galaxy_0.Systems[k];
                if (systemInfo.Sector.X < num8 || systemInfo.Sector.X > num9 || systemInfo.Sector.Y < num10 || systemInfo.Sector.Y > num11)
                {
                    continue;
                }
                float num25 = (float)((systemInfo.SystemStar.Xpos - (double)int_16) / double_15);
                float num26 = (float)((systemInfo.SystemStar.Ypos - (double)int_18) / double_15);
                SystemVisibilityStatus systemVisibilityStatus = empire.CheckSystemVisibilityStatus(k);
                int num27 = 0;
                float val3 = 0f;
                bool flag23 = false;
                bool flag24 = false;
                if (double_15 > num16 && !flag2 && !flag && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode) && systemInfo.DominantEmpire != null)
                {
                    num27 = Math.Min(systemInfo.DominantEmpire.TotalStrategicValue, 1500000);
                    val3 = (float)(Math.Pow(num27, 0.35) * 600.0);
                    float num28 = 0f;
                    float num29 = 0f;
                    HabitatList habitatList = new HabitatList();
                    habitatList.AddRange(systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars);
                    if (habitatList.Count > 0)
                    {
                        for (int l = 0; l < habitatList.Count; l++)
                        {
                            Habitat habitat = habitatList[l];
                            if (habitat != null)
                            {
                                SystemVisibilityStatus systemVisibilityStatus2 = empire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                                if (systemVisibilityStatus2 == SystemVisibilityStatus.Visible || systemVisibilityStatus2 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                {
                                    num28 = (float)((habitat.Xpos - (double)int_16) / double_15);
                                    num29 = (float)((habitat.Ypos - (double)int_18) / double_15);
                                    XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26, num28, num29, systemInfo.DominantEmpire.Empire.MainColor, 1, dashed: true);
                                }
                            }
                        }
                    }
                    if (systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count > 0)
                    {
                        Habitat[] array = ListHelper.ToArrayThreadSafe(systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars);
                        foreach (Habitat habitat2 in array)
                        {
                            if (habitatList.Contains(habitat2) || habitat2 == null)
                            {
                                continue;
                            }
                            SystemInfo systemInfo2 = galaxy_0.Systems[habitat2.SystemIndex];
                            if (systemInfo2.Sector.X < num8 || systemInfo2.Sector.X > num9 || systemInfo2.Sector.Y < num10 || systemInfo2.Sector.Y > num11)
                            {
                                SystemVisibilityStatus systemVisibilityStatus3 = empire.CheckSystemVisibilityStatus(habitat2.SystemIndex);
                                if (systemVisibilityStatus3 == SystemVisibilityStatus.Visible || systemVisibilityStatus3 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                {
                                    num28 = (float)((habitat2.Xpos - (double)int_16) / double_15);
                                    num29 = (float)((habitat2.Ypos - (double)int_18) / double_15);
                                    XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26, num28, num29, systemInfo.DominantEmpire.Empire.MainColor, 1, dashed: true);
                                }
                            }
                        }
                    }
                    EmpireSystemSummaryList otherEmpires = systemInfo.OtherEmpires;
                    if (otherEmpires != null)
                    {
                        for (int n = 0; n < otherEmpires.Count; n++)
                        {
                            EmpireSystemSummary empireSystemSummary = otherEmpires[n];
                            Empire empire2 = empireSystemSummary.Empire;
                            habitatList.Clear();
                            habitatList.AddRange(empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars);
                            if (habitatList.Count > 0)
                            {
                                for (int num30 = 0; num30 < habitatList.Count; num30++)
                                {
                                    Habitat habitat3 = habitatList[num30];
                                    if (habitat3 != null)
                                    {
                                        SystemVisibilityStatus systemVisibilityStatus4 = empire.CheckSystemVisibilityStatus(habitat3.SystemIndex);
                                        if (systemVisibilityStatus4 == SystemVisibilityStatus.Visible || systemVisibilityStatus4 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                        {
                                            num28 = (float)((habitat3.Xpos - (double)int_16) / double_15);
                                            num29 = (float)((habitat3.Ypos - (double)int_18) / double_15);
                                            XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26, num28, num29, empire2.MainColor, 1, dashed: true);
                                        }
                                    }
                                }
                            }
                            if (empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count <= 0)
                            {
                                continue;
                            }
                            ListHelper.ToArrayThreadSafe(empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars);
                            for (int num31 = 0; num31 < empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; num31++)
                            {
                                Habitat habitat4 = empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[num31];
                                if (habitatList.Contains(habitat4) || habitat4 == null)
                                {
                                    continue;
                                }
                                SystemInfo systemInfo3 = galaxy_0.Systems[habitat4.SystemIndex];
                                if (systemInfo3.Sector.X < num8 || systemInfo3.Sector.X > num9 || systemInfo3.Sector.Y < num10 || systemInfo3.Sector.Y > num11)
                                {
                                    SystemVisibilityStatus systemVisibilityStatus5 = empire.CheckSystemVisibilityStatus(habitat4.SystemIndex);
                                    if (systemVisibilityStatus5 == SystemVisibilityStatus.Visible || systemVisibilityStatus5 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                    {
                                        num28 = (float)((habitat4.Xpos - (double)int_16) / double_15);
                                        num29 = (float)((habitat4.Ypos - (double)int_18) / double_15);
                                        XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26, num28, num29, empire2.MainColor, 1, dashed: true);
                                    }
                                }
                            }
                        }
                    }
                }
                val3 = Math.Max(Galaxy.MaxSolarSystemSize, val3);
                if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    val3 = systemInfo.SystemStar.NovaProgression;
                }
                val3 = (int)((double)val3 / double_15);
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    val3 = (int)((double)val3 * 0.7);
                }
                if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    val3 = (int)((double)val3 * 2.0);
                }
                if (!(num25 + val3 >= 0f) || !(num25 - val3 <= (float)int_13) || !(num26 + val3 >= 0f) || !(num26 - val3 <= (float)int_14))
                {
                    continue;
                }
                Pen pen = method_268(empire, flag2, systemInfo, int_15);
                SolidBrush solidBrush2 = method_269(empire, flag2, systemInfo, int_15);
                int val4 = (int)((double)systemInfo.SystemStar.Diameter / (double_15 / 70.0));
                val4 = Math.Min(Math.Max(8, val4), 25);
                if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    val4 = (int)((double)systemInfo.SystemStar.NovaProgression * 2.0 / double_15);
                }
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    val3 = Math.Max(6f, val3);
                }
                else if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    val3 = Math.Max(5f, val3);
                }
                else if (val3 * 2f - 4f <= (float)val4)
                {
                    val3 = val4 / 2 + 3;
                }
                if (double_15 > num16 && !flag && (systemInfo.PlayerPotentialColonies || ((systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored) && systemInfo.DominantEmpire == null && systemInfo.IndependentColonyCount > 0)))
                {
                    float num32 = (float)((double)Galaxy.MaxSolarSystemSize / double_15);
                    num32 *= 0.75f;
                    int num33 = (int)((double)val4 * 0.7);
                    if (num32 * 2f - 4f <= (float)num33)
                    {
                        num32 = num33 / 2 + 3;
                    }
                    RectangleF rectangleF = new RectangleF(num25 - num32, num26 - num32, num32 * 2f, num32 * 2f);
                    System.Drawing.Rectangle area = new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
                    XnaDrawingHelper.DrawCircle(spriteBatch_2, area, color, 1, dashed: true);
                }
                RectangleF rectangleF_ = new RectangleF(num25 - val3, num26 - val3, val3 * 2f, val3 * 2f);
                if (double_15 > num16)
                {
                    if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                    {
                        val3 *= 0.7f;
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26 - val3, num25, num26 + val3, pen.Color, (int)pen.Width);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num25 - val3, num26, num25 + val3, num26, pen.Color, (int)pen.Width);
                    }
                    else if (!flag && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode) && (systemInfo.DominantEmpire != null || systemInfo.IndependentColonyCount > 0))
                    {
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, method_249(rectangleF_), pen.Color, (int)pen.Width, pen.DashStyle == DashStyle.Dash);
                    }
                }
                if (!flag && empire.PirateEmpireBaseHabitat != null && empire.PirateInfluenceSystemIds.Contains(systemInfo.SystemStar.SystemIndex))
                {
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle((int)(num25 - num3), (int)(num26 - num3), (int)num2, (int)num2);
                    int alpha = 160;
                    int alpha2 = 128;
                    if (double_15 < 500.0)
                    {
                        alpha = Math.Max(0, Math.Min(255, (int)(double_15 / 500.0 * 160.0)));
                        alpha2 = Math.Max(0, Math.Min(255, (int)(double_15 / 500.0 * 128.0)));
                    }
                    System.Drawing.Color tintColor = System.Drawing.Color.FromArgb(alpha, empire.MainColor);
                    if (empire.MainColor == System.Drawing.Color.FromArgb(255, 1, 1, 1))
                    {
                        tintColor = System.Drawing.Color.FromArgb(alpha2, 255, 255, 255);
                    }
                    if (texture2D_24 != null && !texture2D_24.IsDisposed)
                    {
                        XnaDrawingHelper.DrawTexture(spriteBatch_3, texture2D_24, method_249(rectangle), 0f, 1f, tintColor);
                    }
                }
                Texture2D texture2D = null;
                Bitmap bitmap = null;
                bool flag25 = false;
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    texture2D = texture2D_18[0];
                    bitmap = bitmap_1[0];
                }
                else if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    bitmap = main_0.bitmap_206[systemInfo.SystemStar.NovaImageIndexMajor];
                    texture2D = texture2D_20[systemInfo.SystemStar.NovaImageIndexMajor];
                }
                else
                {
                    texture2D = texture2D_18[systemInfo.SystemStar.MapPictureRef];
                    bitmap = bitmap_1[systemInfo.SystemStar.MapPictureRef];
                }
                double num34 = (double)texture2D.Height / (double)texture2D.Width;
                int num35 = val4;
                int num36 = (int)((double)val4 * num34);
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle((int)num25 - num35 / 2, (int)num26 - num36 / 2, num35, num36);
                double num37 = 0.6;
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    num37 = 1.0;
                }
                if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    num37 = 1.3;
                }
                else if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    num37 = 1.3;
                }
                int val5 = (int)((double)val4 * num37);
                int val6 = (int)((double)val4 * num37 * num34);
                val5 = Math.Max(8, val5);
                val6 = Math.Max(8, val6);
                System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle((int)num25 - val5 / 2, (int)num26 - val6 / 2, val5, val6);
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    rectangle_ = new System.Drawing.Rectangle((int)num25 - (int)val3, (int)num26 - (int)val3, (int)val3 * 2, (int)val3 * 2);
                }
                else
                {
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova && systemInfo.SystemStar.Type != HabitatType.BlackHole && double_15 < 150.0)
                    {
                        int val7 = (int)((double)systemInfo.SystemStar.Diameter / double_15);
                        int num38 = (int)((systemInfo.SystemStar.Xpos - (double)int_16) / double_15) - 1;
                        int num39 = (int)((systemInfo.SystemStar.Ypos - (double)int_18) / double_15) - 1;
                        val7 = Math.Max(10, val7);
                        rectangle2 = new System.Drawing.Rectangle(num38 - val7 / 2, num39 - val7 / 2, val7 + 2, val7 + 2);
                    }
                    if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                    {
                        _ = rectangle2.Width;
                    }
                    if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                    {
                        if (double_15 < 5100.0)
                        {
                            val3 = Math.Max(11f, val3);
                            val3 = Math.Min(17f, val3);
                        }
                        rectangle2 = new System.Drawing.Rectangle((int)num25 - (int)val3, (int)num26 - (int)val3, (int)val3 * 2, (int)val3 * 2);
                        rectangle_ = new System.Drawing.Rectangle(rectangle2.Location, rectangle2.Size);
                        rectangleF_ = new System.Drawing.Rectangle(rectangle2.Location, rectangle2.Size);
                    }
                    if (double_15 > method_60(systemInfo.SystemStar.Type) && (double_15 < 5100.0 || systemInfo.SystemStar.Type == HabitatType.BlackHole || systemInfo.SystemStar.Type == HabitatType.SuperNova))
                    {
                        if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                        {
                            XnaDrawingHelper.DrawTexture(spriteBatch_3, texture2D, rectangle2, 0f);
                        }
                        else
                        {
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, rectangle2, 0f);
                        }
                    }
                    if (flag25)
                    {
                        method_22(texture2D);
                    }
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova && systemInfo.SystemStar.Type != HabitatType.BlackHole)
                    {
                        System.Drawing.Color color_ = method_120(bitmap);
                        double double_18 = 2.0;
                        double double_19 = 1.0;
                        if (double_15 < 5100.0)
                        {
                            double double_20 = 20.0;
                            double num40 = method_61(systemInfo.SystemStar.Type, out double_20);
                            if (double_15 > double_20)
                            {
                                if (double_15 < num40)
                                {
                                    double num41 = num40 - double_20;
                                    double_18 = 1.0 + (double_15 - double_20) / num41 * 1.0;
                                    double_19 = (double_15 - double_20) / num41;
                                }
                                method_234(spriteBatch_2, galaxy_0, currentStarDate, systemInfo.SystemStar, color_, rectangle2, double_18, double_19);
                            }
                        }
                        else
                        {
                            double_18 = 1.3;
                            method_234(spriteBatch_2, galaxy_0, currentStarDate, systemInfo.SystemStar, color_, rectangle2, double_18, double_19);
                        }
                    }
                }
                BaconMain.DrawWeaponRanges(spriteBatch_2, int_16, int_18, double_15);
                if (double_15 > num16)
                {
                    if (main_0._Game.SelectedObject == systemInfo)
                    {
                        if ((float)rectangle_.Width > rectangleF_.Width)
                        {
                            method_212(spriteBatch_2, rectangle_.X - 3, rectangle_.Y - 3, rectangle_.X + rectangle_.Width + 4, rectangle_.Y + rectangle_.Height + 4);
                        }
                        else
                        {
                            method_212(spriteBatch_2, rectangleF_.X - 3f, rectangleF_.Y - 3f, rectangleF_.X + rectangleF_.Width + 4f, rectangleF_.Y + rectangleF_.Height + 4f);
                        }
                    }
                    bool flag26 = false;
                    SpriteFont spriteFont = spriteFont_1;
                    float num42 = 0f;
                    float num43 = 3f;
                    bool flag27 = false;
                    if (double_15 < 4000.0)
                    {
                        flag27 = true;
                        if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible || main_0._Game.GodMode)
                        {
                            flag26 = true;
                            if (systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                            {
                                if (systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire.Capital != null)
                                {
                                    Habitat habitat5 = Galaxy.DetermineHabitatSystemStar(systemInfo.DominantEmpire.Empire.Capital);
                                    if (habitat5 == systemInfo.SystemStar)
                                    {
                                        flag23 = true;
                                    }
                                    else if (systemInfo.DominantEmpire.Empire.CapitalSystemStars.Contains(systemInfo.SystemStar))
                                    {
                                        flag24 = true;
                                    }
                                }
                                spriteFont = spriteFont_2;
                                num42 = 3f;
                            }
                        }
                    }
                    else
                    {
                        num43 = 2f;
                        if ((systemInfo.IndependentColonyCount > 0 || systemInfo.DominantEmpire != null) && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                        {
                            flag26 = true;
                            spriteFont = spriteFont_0;
                        }
                    }
                    if (flag26 && !flag)
                    {
                        if (systemInfo.PlagueId >= 0)
                        {
                            Plague plague = Galaxy.PlaguesStatic[systemInfo.PlagueId];
                            if (plague != null && plague.PictureRef >= 0 && plague.PictureRef < texture2D_26.Length)
                            {
                                Texture2D texture2D2 = ((!flag27) ? texture2D_30[plague.PictureRef] : texture2D_26[plague.PictureRef]);
                                _ = spriteFont.LineSpacing;
                                System.Drawing.Rectangle destination = new System.Drawing.Rectangle((int)(num25 - (val3 + (float)texture2D2.Width / 2f)), (int)(num26 - (val3 + (float)texture2D2.Width / 2f)), texture2D2.Width, texture2D2.Height);
                                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D2, destination, 0f);
                            }
                        }
                        int num44 = 0;
                        float num45 = 0f;
                        float num46 = 0f;
                        Texture2D texture2D3;
                        Texture2D texture2D4;
                        Texture2D texture2D5;
                        if (flag27)
                        {
                            texture2D3 = texture2D_9;
                            texture2D4 = texture2D_10;
                            texture2D5 = texture2D_11;
                        }
                        else
                        {
                            texture2D3 = texture2D_27;
                            texture2D4 = texture2D_28;
                            texture2D5 = texture2D_29;
                        }
                        if (flag23 || flag24)
                        {
                            num45 = texture2D3.Height;
                            num44 = (int)num45;
                        }
                        if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].IsRefuellingPoint)
                        {
                            num46 = texture2D5.Height;
                            num44 += (int)num46;
                        }
                        string name = systemInfo.SystemStar.Name;
                        int num47 = (int)(num25 + 1f + (float)num44 + val3);
                        int num48 = (int)num26 - (spriteFont.LineSpacing + 1);
                        XnaDrawingHelper.DrawStringDropShadow(point: new System.Drawing.Point(num47, num48), spriteBatch: spriteBatch_2, text: name, font: spriteFont, foreColor: solidBrush2.Color);
                        if (flag23)
                        {
                            System.Drawing.Rectangle destination2 = new System.Drawing.Rectangle((int)(num25 + 1f + val3), (int)(num26 - (float)(texture2D3.Height - 1)), texture2D3.Width, texture2D3.Height);
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D3, destination2, 0f);
                        }
                        else if (flag24)
                        {
                            System.Drawing.Rectangle destination3 = new System.Drawing.Rectangle((int)(num25 + 1f + val3), (int)(num26 - (float)(texture2D4.Height - 1)), texture2D4.Width, texture2D4.Height);
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D4, destination3, 0f);
                        }
                        if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].IsRefuellingPoint)
                        {
                            System.Drawing.Rectangle destination4 = new System.Drawing.Rectangle((int)(num25 + 1f + val3 + num45), (int)(num26 - (float)(texture2D5.Height - 1)), texture2D5.Width, texture2D5.Height);
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D5, destination4, 0f);
                        }
                        float num49 = spriteFont.MeasureString(name).X;
                        if (systemInfo.HasRuins)
                        {
                            float num50 = num25 + val3 + 1f + (float)num44 + num49;
                            float num51 = num26 - ((float)spriteFont.LineSpacing - 1f) + num42;
                            RectangleF rectangleF_2 = new RectangleF(num50, num51 + 5f, num43, num43);
                            XnaDrawingHelper.FillRectangle(spriteBatch_2, method_249(rectangleF_2), solidBrush2.Color);
                            rectangleF_2 = new RectangleF(num50 + 6f, num51 + 5f, num43, num43);
                            XnaDrawingHelper.FillRectangle(spriteBatch_2, method_249(rectangleF_2), solidBrush2.Color);
                            rectangleF_2 = new RectangleF(num50 + 3f, num51, num43, num43);
                            XnaDrawingHelper.FillRectangle(spriteBatch_2, method_249(rectangleF_2), solidBrush2.Color);
                        }
                    }
                    if (systemInfo_0 != null && systemInfo == systemInfo_0)
                    {
                        if ((float)rectangle2.Width > rectangleF_.Width)
                        {
                            method_209(spriteBatch_2, rectangle_);
                        }
                        else
                        {
                            method_208(spriteBatch_2, rectangleF_);
                        }
                    }
                    else if (systemInfo.SystemStar != null && empire.CheckSystemExplored(systemInfo.SystemStar))
                    {
                        bool flag28 = false;
                        if (flag18)
                        {
                            if (systemInfo.PlayerPotentialColonies)
                            {
                                flag28 = true;
                            }
                            if (systemInfo.IndependentColonyCount > 0)
                            {
                                Empire empire3 = galaxy_0.CheckSystemOwnership(systemInfo.SystemStar);
                                if (empire3 == null || empire3 == empire)
                                {
                                    flag28 = true;
                                }
                            }
                        }
                        if (flag19 && systemInfo.HasScenery)
                        {
                            if (systemInfo.SystemStar != null && systemInfo.SystemStar.ScenicFactor > 0f)
                            {
                                flag28 = true;
                            }
                            if (!flag28 && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                            {
                                for (int num52 = 0; num52 < systemInfo.Habitats.Count; num52++)
                                {
                                    Habitat habitat6 = systemInfo.Habitats[num52];
                                    if (habitat6 != null && habitat6.ScenicFactor > 0f && !galaxy_0.CheckAlreadyHaveMiningStationAtHabitat(habitat6, empire))
                                    {
                                        flag28 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag20 && systemInfo.HasResearchBonus)
                        {
                            if (systemInfo.SystemStar != null && systemInfo.SystemStar.ResearchBonus > 0)
                            {
                                flag28 = true;
                            }
                            if (!flag28 && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                            {
                                for (int num53 = 0; num53 < systemInfo.Habitats.Count; num53++)
                                {
                                    Habitat habitat7 = systemInfo.Habitats[num53];
                                    if (habitat7 != null && habitat7.ResearchBonus > 0 && !empire.CheckResearchStationAtLocation(habitat7))
                                    {
                                        flag28 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag28)
                        {
                            if ((float)rectangle2.Width > rectangleF_.Width)
                            {
                                method_209(spriteBatch_2, rectangle_);
                            }
                            else
                            {
                                method_208(spriteBatch_2, rectangleF_);
                            }
                        }
                    }
                }
                solidBrush2.Dispose();
                pen.Dispose();
            }
            if (main_0.double_0 > 70.0 && main_0.double_0 <= main_0.double_5)
            {
                for (int num54 = 0; num54 < galaxy_0.GalaxyLocations.Count; num54++)
                {
                    GalaxyLocation galaxyLocation = galaxy_0.GalaxyLocations[num54];
                    if (main_0._Game.GodMode || empire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        int num55 = (int)(((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0 - (double)int_16) / double_15);
                        int num56 = (int)(((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0 - (double)int_18) / double_15);
                        if (num55 > -100 && num55 < base.ClientRectangle.Width + 100 && num56 > -100 && num56 < base.ClientRectangle.Height + 100 && galaxyLocation.ShowName && !flag)
                        {
                            SpriteFont spriteFont2 = spriteFont_3;
                            spriteFont2 = ((galaxyLocation.Type == GalaxyLocationType.NebulaCloud) ? ((double_15 > 4000.0) ? spriteFont_1 : ((!(double_15 > 1000.0)) ? spriteFont_2 : spriteFont_3)) : ((double_15 > 4000.0) ? spriteFont_0 : ((!(double_15 > 1000.0)) ? spriteFont_3 : spriteFont_1)));
                            Vector2 vector2 = spriteFont2.MeasureString(galaxyLocation.Name);
                            int num57 = num55 - (int)(vector2.X / 2f);
                            int num58 = num56 - (int)(vector2.Y / 2f);
                            if (galaxyLocation.Type != GalaxyLocationType.NebulaCloud)
                            {
                                num57 -= 5;
                                num58 -= 20;
                            }
                            XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, galaxyLocation.Name, spriteFont2, solidBrush_0.Color, num57, num58);
                        }
                    }
                    if (galaxyLocation.Effect == GalaxyLocationEffectType.LightningDamage)
                    {
                        int num59 = (int)(((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0 - (double)int_16) / double_15);
                        int num60 = (int)(((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0 - (double)int_18) / double_15);
                        if (num59 > -100 && num59 < base.ClientRectangle.Width + 100 && num60 > -100 && num60 < base.ClientRectangle.Height + 100)
                        {
                            method_262(spriteBatch_2, num59, num60, galaxyLocation, double_15);
                        }
                    }
                }
            }
            DateTime now = DateTime.Now;
            EventPing[] array2 = ToArrayThreadSafe(list_29);
            foreach (EventPing eventPing in array2)
            {
                method_232(spriteBatch_2, now, main_0.double_0, eventPing.Point.X, eventPing.Point.Y, main_0.int_13, main_0.int_14, int_16, int_17, int_18, int_19);
            }
            dateTime_3 = now;
            method_229(spriteBatch_2, int_16, int_17, int_18, int_19);
            System.Drawing.Color.FromArgb(int_15, 0, 0, 255);
            System.Drawing.Color.FromArgb(int_15, 96, 96, 255);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(int_15, 0, 0, 255);
            System.Drawing.Color.FromArgb(int_15, 96, 96, 255);
            System.Drawing.Color color3 = System.Drawing.Color.FromArgb(int_15, 192, 192, 0);
            System.Drawing.Color.FromArgb(int_15, 192, 192, 96);
            System.Drawing.Color color4 = System.Drawing.Color.FromArgb(int_15, 255, 0, 0);
            System.Drawing.Color.FromArgb(int_15, 255, 96, 96);
            DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
            DiplomaticRelation[] array3 = empire.DiplomaticRelations.ToArray();
            foreach (DiplomaticRelation diplomaticRelation in array3)
            {
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            if (main_0.double_0 <= main_0.double_5 && !flag)
            {
                for (int num63 = num12; num63 <= num13; num63++)
                {
                    for (int num64 = num14; num64 <= num15; num64++)
                    {
                        if (galaxy_0.BuiltObjectIndex.Length <= num63 || galaxy_0.BuiltObjectIndex[num63].Length <= num64)
                        {
                            continue;
                        }
                        BuiltObject[] array4 = ListHelper.ToArrayThreadSafe(galaxy_0.BuiltObjectIndex[num63][num64]);
                        for (int num65 = 0; num65 < array4.Length; num65++)
                        {
                            BuiltObject builtObject = null;
                            if (array4.Length > num65)
                            {
                                builtObject = array4[num65];
                                if (builtObject == null || builtObject.HasBeenDestroyed)
                                {
                                    builtObject = null;
                                }
                            }
                            if (builtObject == null)
                            {
                                continue;
                            }
                            bool flag29 = false;
                            bool flag30 = false;
                            bool flag31 = false;
                            if (builtObject.Empire == empire)
                            {
                                _ = builtObject.Empire.MainColor;
                            }
                            else if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                            {
                                _ = builtObject.Empire.MainColor;
                                flag30 = true;
                                if (builtObject.Empire.MainColor == System.Drawing.Color.FromArgb(1, 1, 1))
                                {
                                    System.Drawing.Color.FromArgb(48, 48, 48);
                                }
                            }
                            else if (empireList.Contains(builtObject.Empire))
                            {
                                _ = builtObject.Empire.MainColor;
                                flag30 = true;
                            }
                            else if (builtObject.Empire != null)
                            {
                                _ = builtObject.Empire.MainColor;
                            }
                            switch (builtObject.SubRole)
                            {
                                case BuiltObjectSubRole.Escort:
                                case BuiltObjectSubRole.Frigate:
                                case BuiltObjectSubRole.Destroyer:
                                case BuiltObjectSubRole.Cruiser:
                                case BuiltObjectSubRole.CapitalShip:
                                case BuiltObjectSubRole.TroopTransport:
                                case BuiltObjectSubRole.Carrier:
                                    flag29 = flag5;
                                    if (flag30 && !flag29 && flag13)
                                    {
                                        flag29 = true;
                                    }
                                    if (flag31 && !flag29 && flag14)
                                    {
                                        flag29 = true;
                                    }
                                    break;
                                case BuiltObjectSubRole.ResupplyShip:
                                    flag29 = flag4;
                                    if (flag30 && !flag29 && flag13)
                                    {
                                        flag29 = true;
                                    }
                                    break;
                                case BuiltObjectSubRole.ExplorationShip:
                                    flag29 = flag8;
                                    break;
                                case BuiltObjectSubRole.ColonyShip:
                                    flag29 = flag9;
                                    break;
                                case BuiltObjectSubRole.ConstructionShip:
                                    flag29 = flag10;
                                    break;
                                case BuiltObjectSubRole.SmallFreighter:
                                case BuiltObjectSubRole.MediumFreighter:
                                case BuiltObjectSubRole.LargeFreighter:
                                case BuiltObjectSubRole.PassengerShip:
                                case BuiltObjectSubRole.GasMiningShip:
                                case BuiltObjectSubRole.MiningShip:
                                    flag29 = flag11;
                                    break;
                                case BuiltObjectSubRole.SmallSpacePort:
                                case BuiltObjectSubRole.MediumSpacePort:
                                case BuiltObjectSubRole.LargeSpacePort:
                                    flag29 = flag6;
                                    break;
                                case BuiltObjectSubRole.GasMiningStation:
                                case BuiltObjectSubRole.MiningStation:
                                case BuiltObjectSubRole.ResortBase:
                                case BuiltObjectSubRole.GenericBase:
                                case BuiltObjectSubRole.EnergyResearchStation:
                                case BuiltObjectSubRole.WeaponsResearchStation:
                                case BuiltObjectSubRole.HighTechResearchStation:
                                case BuiltObjectSubRole.MonitoringStation:
                                case BuiltObjectSubRole.DefensiveBase:
                                    flag29 = flag7;
                                    break;
                            }
                            bool flag32 = false;
                            if (flag29)
                            {
                                if (builtObject.Empire == empire)
                                {
                                    flag32 = true;
                                }
                                else if (empire.IsObjectVisibleToThisEmpireImprecise(builtObject))
                                {
                                    flag32 = true;
                                }
                            }
                            if (builtObject.ShipGroup != null && builtObject.ShipGroup.LeadShip == builtObject)
                            {
                                flag32 = false;
                            }
                            if (!flag32)
                            {
                                continue;
                            }
                            int num66 = (int)((builtObject.Xpos - (double)int_16) / double_15);
                            int num67 = (int)((builtObject.Ypos - (double)int_18) / double_15);
                            if (num66 < 0 || num66 > int_13 || num67 < 0 || num67 > int_14)
                            {
                                continue;
                            }
                            if (flag16 || flag17)
                            {
                                bool flag33 = false;
                                flag33 = ((builtObject.Owner == null) ? flag17 : flag16);
                                if (flag33 && builtObject.ActualEmpire == empire)
                                {
                                    if (SpecialHighlightBuiltObjects.Count > 0 && SpecialHighlightBuiltObjects.Contains(builtObject))
                                    {
                                        method_253(spriteBatch_2, builtObject, num66, num67, int_16, int_18, double_15, bool_13: false, System.Drawing.Color.FromArgb(255, 0, 0), 2);
                                    }
                                    else
                                    {
                                        method_251(spriteBatch_2, builtObject, num66, num67, int_16, int_18, double_15, bool_13: false);
                                    }
                                }
                            }
                            int num68 = 10;
                            if (double_15 < 400.0)
                            {
                                num68 = (int)((double)num68 * Math.Sqrt(400.0 / double_15));
                            }
                            if (double_15 > 4000.0)
                            {
                                num68 = (int)((double)num68 / (double_15 / 4000.0));
                            }
                            if (builtObject.Owner == null)
                            {
                                System.Drawing.Color.FromArgb(160, 160, 160);
                            }
                            if (num68 < 6)
                            {
                                num68 = 6;
                            }
                            int num69 = 12;
                            if (builtObject.Role == BuiltObjectRole.Base)
                            {
                                num69 = 15;
                            }
                            if (num68 > num69)
                            {
                                num68 = num69;
                            }
                            if (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                DrawShipSymbolXna(spriteBatch_2, builtObject, ResolveShipSymbolColor(builtObject), num66 - num68 / 2, num67 - num68 / 2, num68, num68, num68, num68, galaxyLevel: true, currentDateTime);
                                if (main_0._Game.SelectedObject == builtObject)
                                {
                                    num68 = (int)((double)num68 * 1.6);
                                    method_212(spriteBatch_2, num66 - num68 / 2, num67 - num68 / 2, num66 + num68 / 2, num67 + num68 / 2);
                                }
                                else if (main_0._Game.SelectedObject is BuiltObjectList)
                                {
                                    BuiltObjectList builtObjectList = (BuiltObjectList)main_0._Game.SelectedObject;
                                    if (builtObjectList.Contains(builtObject))
                                    {
                                        num68 = (int)((double)num68 * 1.6);
                                        method_212(spriteBatch_2, num66 - num68 / 2, num67 - num68 / 2, num66 + num68 / 2, num67 + num68 / 2);
                                    }
                                }
                            }
                            if ((!flag30 && galaxy_0.FastTestShipInColonizedSystem(builtObject)) || builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                continue;
                            }
                            DrawShipSymbolXna(spriteBatch_2, builtObject, ResolveShipSymbolColor(builtObject), num66 - num68 / 2, num67 - num68 / 2, num68, num68, num68, num68, galaxyLevel: true, currentDateTime);
                            if (main_0._Game.SelectedObject == builtObject)
                            {
                                num68 = (int)((double)num68 * 1.6);
                                method_212(spriteBatch_2, num66 - num68 / 2, num67 - num68 / 2, num66 + num68 / 2, num67 + num68 / 2);
                            }
                            else if (main_0._Game.SelectedObject is BuiltObjectList)
                            {
                                BuiltObjectList builtObjectList2 = (BuiltObjectList)main_0._Game.SelectedObject;
                                if (builtObjectList2.Contains(builtObject))
                                {
                                    num68 = (int)((double)num68 * 1.6);
                                    method_212(spriteBatch_2, num66 - num68 / 2, num67 - num68 / 2, num66 + num68 / 2, num67 + num68 / 2);
                                }
                            }
                        }
                    }
                }
            }
            if (main_0._Game.SelectedObject != null && !flag)
            {
                BuiltObject builtObject2 = null;
                double num70 = 0.0;
                if (main_0._Game.SelectedObject is BuiltObject)
                {
                    builtObject2 = (BuiltObject)main_0._Game.SelectedObject;
                    num70 = builtObject2.CurrentRange();
                }
                else if (main_0._Game.SelectedObject is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)main_0._Game.SelectedObject;
                    builtObject2 = shipGroup.LeadShip;
                    num70 = shipGroup.CurrentRange();
                }
                BaconMain.DrawGravityWellRange(spriteBatch_2, int_16, int_18, double_15);
                if (builtObject2 != null && builtObject2.Role != BuiltObjectRole.Base && builtObject2.ActualEmpire == empire)
                {
                    double num71 = builtObject2.Xpos - num70;
                    double num72 = builtObject2.Ypos - num70;
                    float num73 = (float)((num71 - (double)int_16) / double_15);
                    float num74 = (float)((num72 - (double)int_18) / double_15);
                    float num75 = (float)(num70 / double_15 * 2.0);
                    if ((double)num73 < (double)Galaxy.SizeX)
                    {
                        new RectangleF((float)num70, num74, num75, num75);
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, num73, num74, num75, num75, main_0.VuqPpUtdZU, 1, 200, dashed: true);
                    }
                    int int_20 = (int)((builtObject2.Xpos - (double)int_16) / double_15);
                    int int_21 = (int)((builtObject2.Ypos - (double)int_18) / double_15);
                    method_252(spriteBatch_2, builtObject2, int_20, int_21, int_16, int_18, double_15, bool_13: true, System.Drawing.Color.Yellow);
                }
            }
            if (double_15 > num16 && !flag)
            {
                BuiltObject[] array5 = ListHelper.ToArrayThreadSafe(empire.KnownPirateBases);
                if (main_0._Game.GodMode)
                {
                    BuiltObjectList builtObjectList3 = new BuiltObjectList();
                    for (int num76 = 0; num76 < galaxy_0.PirateEmpires.Count; num76++)
                    {
                        Empire empire4 = galaxy_0.PirateEmpires[num76];
                        if (empire4.PirateEmpireBaseHabitat != null && empire4.PirateEmpireBaseHabitat.BasesAtHabitat != null && empire4.PirateEmpireBaseHabitat.BasesAtHabitat.Count > 0)
                        {
                            builtObjectList3.Add(empire4.PirateEmpireBaseHabitat.BasesAtHabitat[0]);
                        }
                    }
                    array5 = ListHelper.ToArrayThreadSafe(builtObjectList3);
                }
                foreach (BuiltObject builtObject3 in array5)
                {
                    if (builtObject3 == null || builtObject3.HasBeenDestroyed)
                    {
                        continue;
                    }
                    int num78 = (int)((builtObject3.Xpos - (double)int_16) / double_15);
                    int num79 = (int)((builtObject3.Ypos - (double)int_18) / double_15);
                    if (num78 < 0 || num78 > int_13 || num79 < 0 || num79 > int_14)
                    {
                        continue;
                    }
                    int num80 = 27;
                    int num81 = 16;
                    if (double_15 < 600.0)
                    {
                        num80 = 35;
                        num81 = 21;
                    }
                    else if (double_15 > 4000.0)
                    {
                        num80 = 20;
                        num81 = 12;
                    }
                    System.Drawing.Rectangle rectangle3 = new System.Drawing.Rectangle(num78 - num80 / 2, num79 - num81 / 2, num80, num81);
                    if (builtObject3.Empire != null && builtObject3.Empire.MediumFlagPicture != null)
                    {
                        if (texture2D_21.Length > builtObject3.Empire.EmpireId)
                        {
                            Texture2D texture2D6 = texture2D_21[builtObject3.Empire.EmpireId];
                            if (texture2D6 == null || texture2D6.IsDisposed)
                            {
                                texture2D6 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, builtObject3.Empire.MediumFlagPicture);
                                texture2D_21[builtObject3.Empire.EmpireId] = texture2D6;
                            }
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D6, rectangle3, 0f);
                        }
                        else
                        {
                            Texture2D texture2D7 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, builtObject3.Empire.MediumFlagPicture);
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D7, rectangle3, 0f);
                            method_22(texture2D7);
                        }
                    }
                    else
                    {
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_22, rectangle3, 0f);
                    }
                    System.Drawing.Color color5 = System.Drawing.Color.FromArgb(80, 80, 80);
                    XnaDrawingHelper.DrawRectangle(spriteBatch_2, rectangle3, color5, 1);
                }
                int num82 = 22;
                if (double_15 < 400.0)
                {
                    num82 = (int)((double)num82 * Math.Sqrt(400.0 / double_15));
                }
                if (double_15 > 4000.0)
                {
                    num82 = (int)((double)num82 / (double_15 / 4000.0));
                }
                if (num82 < 12)
                {
                    num82 = 12;
                }
                SolidBrush solidBrush3 = new SolidBrush(color2);
                if (empire != null)
                {
                    if (flag3)
                    {
                        method_258(spriteBatch_2, solidBrush3, empire, double_15, num82, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                    }
                    SolidBrush solidBrush4 = new SolidBrush(color3);
                    SolidBrush solidBrush5 = new SolidBrush(color4);
                    Empire[] array6 = ListHelper.ToArrayThreadSafe(galaxy_0.Empires);
                    foreach (Empire empire5 in array6)
                    {
                        if (empire5 == empire)
                        {
                            continue;
                        }
                        if (empireList.Contains(empire5))
                        {
                            if (flag3 || flag12)
                            {
                                method_258(spriteBatch_2, solidBrush5, empire5, double_15, num82, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                            }
                        }
                        else if (flag3)
                        {
                            method_258(spriteBatch_2, solidBrush4, empire5, double_15, num82, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                        }
                    }
                    solidBrush4?.Dispose();
                    solidBrush5?.Dispose();
                }
                solidBrush3?.Dispose();
            }
            solidBrush?.Dispose();
        }

        private void method_251(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13)
        {
            method_252(spriteBatch_2, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, System.Drawing.Color.FromArgb(170, 170, 170));
        }

        private void method_252(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, System.Drawing.Color color_18)
        {
            method_253(spriteBatch_2, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, color_18, 1);
        }

        private void method_253(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, System.Drawing.Color color_18, int int_15)
        {
            BaconMainView.method_253(this, spriteBatch_2, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, color_18, int_15);
        }

        private void method_254(Graphics graphics_0, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13)
        {
            method_255(graphics_0, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, pen_2);
        }

        private void method_255(Graphics graphics_0, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, Pen pen_11)
        {
            if (builtObject_1 != null && !builtObject_1.HasBeenDestroyed && builtObject_1.Role != BuiltObjectRole.Base && builtObject_1.TopSpeed > 0 && builtObject_1.WarpSpeed > 0 && (builtObject_1.CurrentSpeed > (float)builtObject_1.TopSpeed || builtObject_1.HyperjumpPrepare) && builtObject_1.Mission != null && builtObject_1.Mission.Type != 0 && (bool_13 || builtObject_1.ShipGroup == null || builtObject_1.ShipGroup.LeadShip == builtObject_1))
            {
                System.Drawing.Point point = builtObject_1.Mission.ResolveTargetCoordinatesCurrentCommand();
                int x = (int)(((double)point.X - (double)int_13) / double_15);
                int y = (int)(((double)point.Y - (double)int_14) / double_15);
                double num = Math.Abs((double)point.X - builtObject_1.Xpos) + Math.Abs((double)point.Y - builtObject_1.Ypos);
                num *= 1500.0 / double_15;
                if (num > 40000.0)
                {
                    graphics_0.DrawLine(pen_11, int_11, int_12, x, y);
                }
            }
        }

        private void method_256(Graphics graphics_0, SolidBrush solidBrush_21, Empire empire_0, double double_15, int int_11, int int_12, int int_13, double double_16, double double_17, double double_18, double double_19, Empire empire_1)
        {
            using SolidBrush brush_ = new SolidBrush(empire_0.MainColor);
            for (int i = 0; i < empire_0.ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = empire_0.ShipGroups[i];
                if (!empire_1.IsObjectVisibleToThisEmpire(shipGroup.LeadShip))
                {
                    continue;
                }
                int num = (int)((shipGroup.LeadShip.Xpos - double_16) / double_15);
                int num2 = (int)((shipGroup.LeadShip.Ypos - double_18) / double_15);
                if (num < 0 || num > int_12 || num2 < 0 || num2 > int_13)
                {
                    continue;
                }
                if (main_0.gameOptions_0 != null && main_0.gameOptions_0.MapOverlayTravelVectorsState)
                {
                    method_254(graphics_0, shipGroup.LeadShip, num, num2, (int)double_16, (int)double_18, double_15, bool_13: false);
                }
                int num3 = empire_1.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(shipGroup);
                if (num3 >= 0)
                {
                    FleetAttack fleetAttack = empire_1.IncomingEnemyFleetsAndPlanetDestroyers[num3];
                    bool flag = true;
                    if (fleetAttack.Fleet.Mission != null && (fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.Attack || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.WaitAndAttack || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.Bombard || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.WaitAndBombard))
                    {
                        if (BuiltObjectMission.ResolveMissionTargetEmpire(fleetAttack.Fleet.Mission) != empire_1)
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        using Pen pen = new Pen(System.Drawing.Color.FromArgb(128, 255, 0, 0), 2f);
                        float num4 = 7f;
                        float num5 = 12f;
                        if (double_15 > 5000.0)
                        {
                            num4 = 4f;
                            num5 = 7f;
                        }
                        using AdjustableArrowCap customEndCap = new AdjustableArrowCap(num4, num5);
                        pen.EndCap = LineCap.Custom;
                        pen.CustomEndCap = customEndCap;
                        pen.DashStyle = DashStyle.Dash;
                        System.Drawing.Point point = fleetAttack.Fleet.Mission.ResolveTargetCoordinates(fleetAttack.Fleet.Mission);
                        int x = (int)(((double)point.X - double_16) / double_15);
                        int y = (int)(((double)point.Y - double_18) / double_15);
                        graphics_0.DrawLine(pen, num, num2, x, y);
                    }
                }
                method_257(shipGroup, num, num2, int_11, solidBrush_21, graphics_0);
                if (main_0._Game.SelectedObject == shipGroup)
                {
                    method_210(num - int_11 / 2, num2 - int_11 / 2, num + int_11 / 2, num2 + int_11 / 2, graphics_0);
                }
                if (shipGroup_0 != null && shipGroup == shipGroup_0)
                {
                    System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(num - (int_11 / 2 + 4), num2 - (int_11 / 2 + 4), int_11 + 8, int_11 + 8);
                    method_204(rectangle_, graphics_0);
                }
                Font font = font_1;
                Habitat habitat = galaxy_0.FastFindNearestColony((int)shipGroup.LeadShip.Xpos, (int)shipGroup.LeadShip.Ypos, shipGroup.Empire, 0);
                if (habitat != null)
                {
                    double num6 = galaxy_0.CalculateDistance(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, habitat.Xpos, habitat.Ypos);
                    if (num6 > (double)Galaxy.MaxSolarSystemSize * 2.1)
                    {
                        if (double_15 > 4000.0)
                        {
                            font = font_3;
                        }
                        string name = shipGroup.Name;
                        System.Drawing.Point point_ = new System.Drawing.Point(num - int_11 / 2, num2 - (font.Height + int_11 / 2));
                        method_267(graphics_0, name, font, point_, brush_);
                    }
                }
                else
                {
                    if (double_15 > 4000.0)
                    {
                        font = font_3;
                    }
                    string name2 = shipGroup.Name;
                    System.Drawing.Point point_2 = new System.Drawing.Point(num - int_11 / 2, num2 - (font.Height + int_11 / 2));
                    method_267(graphics_0, name2, font, point_2, brush_);
                }
            }
        }

        private void method_257(ShipGroup shipGroup_1, float float_0, float float_1, int int_11, Brush brush_0, Graphics graphics_0)
        {
            float_0 -= (float)int_11 / 2f;
            float_1 -= (float)int_11 / 3f;
            System.Drawing.Color color = System.Drawing.Color.Gray;
            Pen pen = null;
            SolidBrush solidBrush = null;
            if (shipGroup_1.Empire != null)
            {
                color = shipGroup_1.Empire.MainColor;
                System.Drawing.Color color2 = method_226(color, 64);
                pen = new Pen(color2, 1.5f);
                solidBrush = new SolidBrush(color);
            }
            List<PointF> list = new List<PointF>();
            float num = (float)((double)int_11 / 1.154700538379);
            list.Add(new PointF(float_0, float_1));
            list.Add(new PointF(float_0 + (float)int_11, float_1));
            list.Add(new PointF(float_0 + (float)int_11 / 2f, float_1 + num));
            graphics_0.FillPolygon(solidBrush, list.ToArray());
            graphics_0.DrawPolygon(pen, list.ToArray());
            if (main_0.double_0 < 6000.0)
            {
                System.Drawing.Color color3 = method_263(color);
                string s = shipGroup_1.Ships.Count.ToString();
                float num2 = float_0 + ((float)int_11 - graphics_0.MeasureString(s, font_3, 100, StringFormat.GenericTypographic).Width) / 2f;
                float num3 = float_1 + (float)int_11 * 0.1f;
                graphics_0.DrawString(point: new PointF(num2, num3), s: s, font: font_3, brush: new SolidBrush(color3), format: StringFormat.GenericTypographic);
            }
            pen?.Dispose();
            solidBrush?.Dispose();
        }

        private void method_258(SpriteBatch spriteBatch_2, SolidBrush solidBrush_21, Empire empire_0, double double_15, int int_11, int int_12, int int_13, double double_16, double double_17, double double_18, double double_19, Empire empire_1)
        {
            for (int i = 0; i < empire_0.ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = empire_0.ShipGroups[i];
                if (shipGroup.Empire != empire_1 && !empire_1.IsObjectVisibleToThisEmpire(shipGroup.LeadShip, includeLongRangeScanners: true, includeShipsOutsideSystems: false))
                {
                    continue;
                }
                int num = (int)((shipGroup.LeadShip.Xpos - double_16) / double_15);
                int num2 = (int)((shipGroup.LeadShip.Ypos - double_18) / double_15);
                if (num < 0 || num > int_12 || num2 < 0 || num2 > int_13)
                {
                    continue;
                }
                if (main_0.gameOptions_0 != null && main_0.gameOptions_0.MapOverlayTravelVectorsState)
                {
                    bool flag = false;
                    if (main_0._Game.SelectedObject is ShipGroup)
                    {
                        ShipGroup shipGroup2 = (ShipGroup)main_0._Game.SelectedObject;
                        if (shipGroup2 == shipGroup)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        method_252(spriteBatch_2, shipGroup.LeadShip, num, num2, (int)double_16, (int)double_18, double_15, bool_13: false, System.Drawing.Color.Yellow);
                    }
                    else
                    {
                        method_251(spriteBatch_2, shipGroup.LeadShip, num, num2, (int)double_16, (int)double_18, double_15, bool_13: false);
                    }
                }
                int num3 = empire_1.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(shipGroup);
                if (num3 >= 0)
                {
                    FleetAttack fleetAttack = empire_1.IncomingEnemyFleetsAndPlanetDestroyers[num3];
                    bool flag2 = true;
                    if (fleetAttack.Fleet.Mission != null && (fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.Attack || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.WaitAndAttack || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.Bombard || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.WaitAndBombard))
                    {
                        if (BuiltObjectMission.ResolveMissionTargetEmpire(fleetAttack.Fleet.Mission) != empire_1)
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        flag2 = false;
                    }
                    if (flag2)
                    {
                        System.Drawing.Point point = fleetAttack.Fleet.Mission.ResolveTargetCoordinates(fleetAttack.Fleet.Mission);
                        int x = (int)(((double)point.X - double_16) / double_15);
                        int y = (int)(((double)point.Y - double_18) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num, num2, x, y, System.Drawing.Color.FromArgb(128, 255, 0, 0), 2, dashed: true);
                    }
                }
                method_259(spriteBatch_2, shipGroup, num, num2, int_11);
                if (main_0._Game.SelectedObject == shipGroup)
                {
                    method_212(spriteBatch_2, num - int_11 / 2, num2 - int_11 / 2, num + int_11 / 2, num2 + int_11 / 2);
                }
                if (shipGroup_0 != null && shipGroup == shipGroup_0)
                {
                    System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(num - (int_11 / 2 + 4), num2 - (int_11 / 2 + 4), int_11 + 8, int_11 + 8);
                    method_205(spriteBatch_2, rectangle_);
                }
                SpriteFont spriteFont = spriteFont_2;
                bool flag3 = true;
                if (shipGroup.LeadShip != null && shipGroup.LeadShip.NearestSystemStar != null)
                {
                    SystemInfo systemInfo = galaxy_0.Systems[shipGroup.LeadShip.NearestSystemStar];
                    if (systemInfo != null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire == shipGroup.Empire)
                    {
                        flag3 = false;
                    }
                }
                if (flag3)
                {
                    if (double_15 > 4000.0)
                    {
                        spriteFont = spriteFont_0;
                    }
                    string name = shipGroup.Name;
                    System.Drawing.Point point2 = new System.Drawing.Point(num - int_11 / 2, num2 - (spriteFont.LineSpacing + int_11 / 2));
                    System.Drawing.Color foreColor = empire_0.MainColor;
                    if (foreColor.R == 1 && foreColor.G == 1 && foreColor.B == 1)
                    {
                        foreColor = System.Drawing.Color.FromArgb(128, 128, 128);
                    }
                    XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, name, spriteFont, foreColor, point2);
                }
            }
        }

        private void method_259(SpriteBatch spriteBatch_2, ShipGroup shipGroup_1, float float_0, float float_1, int int_11)
        {
            System.Drawing.Color color = System.Drawing.Color.Gray;
            method_226(color, 48);
            if (shipGroup_1.Empire != null)
            {
                color = shipGroup_1.Empire.MainColor;
                method_226(color, 48);
                if (color.R == 1 && color.G == 1 && color.B == 1)
                {
                    color = System.Drawing.Color.FromArgb(8, 8, 8);
                    System.Drawing.Color.FromArgb(48, 48, 48);
                }
            }
            float num = (float)texture2D_52.Width / (float)texture2D_52.Height;
            int num2 = (int)((float)int_11 * num);
            int num3 = (int)float_0 - num2 / 2;
            int num4 = (int)float_1 - int_11 / 2;
            XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num3, num4, num2, int_11), spriteBatch: spriteBatch_2, texture: texture2D_52, rotationAngle: 0f, tintColor: color);
            if (main_0.double_0 < 6000.0)
            {
                System.Drawing.Color color2 = method_263(color);
                string text = shipGroup_1.Ships.Count.ToString();
                float num5 = (float)num3 + ((float)num2 - spriteFont_1.MeasureString(text).X) / 2f;
                float num6 = (float)num4 + (float)int_11 * 0.1f;
                XnaDrawingHelper.DrawString(point: new PointF(num5, num6), spriteBatch: spriteBatch_2, text: text, font: spriteFont_1, color: color2);
            }
        }

        private void method_260(SpriteBatch spriteBatch_2, ShipGroup shipGroup_1, float float_0, float float_1, int int_11)
        {
            float_0 -= (float)int_11 / 2f;
            float_1 -= (float)int_11 / 3f;
            System.Drawing.Color color_ = System.Drawing.Color.Gray;
            System.Drawing.Color color = method_226(color_, 64);
            if (shipGroup_1.Empire != null)
            {
                color_ = shipGroup_1.Empire.MainColor;
                color = method_226(color_, 64);
            }
            List<PointF> list = new List<PointF>();
            float num = (float)((double)int_11 / 1.154700538379);
            list.Add(new PointF(float_0, float_1));
            list.Add(new PointF(float_0 + (float)int_11, float_1));
            list.Add(new PointF(float_0 + (float)int_11 / 2f, float_1 + num));
            XnaDrawingHelper.DrawPolygon(spriteBatch_2, list.ToArray(), color, 2);
            if (main_0.double_0 < 6000.0)
            {
                System.Drawing.Color color2 = method_263(color_);
                string text = shipGroup_1.Ships.Count.ToString();
                float num2 = float_0 + ((float)int_11 - spriteFont_0.MeasureString(text).X) / 2f;
                float num3 = float_1 + (float)int_11 * 0.1f;
                XnaDrawingHelper.DrawString(point: new PointF(num2, num3), spriteBatch: spriteBatch_2, text: text, font: spriteFont_0, color: color2);
            }
        }

        private void method_261(Graphics graphics_0, int int_11, int int_12, GalaxyLocation galaxyLocation_0, double double_15)
        {
            if (!(galaxyLocation_0.EffectAmount > 0.0) && Galaxy.Rnd.Next(0, 60) != 1)
            {
                return;
            }
            TimeSpan timeSpan = new TimeSpan(galaxy_0.CurrentDateTime.Ticks);
            if (galaxyLocation_0.EffectAmount <= 0.0)
            {
                galaxyLocation_0.EffectAmount = timeSpan.TotalMilliseconds;
                galaxyLocation_0.EffectRandomSeed = Galaxy.Rnd.Next(0, 50000);
            }
            double num = timeSpan.TotalMilliseconds - galaxyLocation_0.EffectAmount;
            if (num > 500.0)
            {
                galaxyLocation_0.EffectAmount = 0.0;
                return;
            }
            double double_16 = 1.0 - num % 250.0 / 250.0 * 0.95;
            Random random = new Random(galaxyLocation_0.EffectRandomSeed);
            double num2 = (double)galaxyLocation_0.Width / double_15;
            double num3 = (double)galaxyLocation_0.Height / double_15;
            double num4 = num2 / 2.5;
            double num5 = num2 * 0.15;
            double num6 = num3 * 0.15;
            double num7 = num2 * 0.3 * random.NextDouble();
            double num8 = num3 * 0.3 * random.NextDouble();
            using Bitmap bitmap = lightningGenerator_0.GenerateLightning(galaxyLocation_0.EffectRandomSeed, (int)num4);
            int num9 = (int)((double)int_11 - num2 / 2.0 + num5 + num7);
            int num10 = (int)((double)int_12 - num3 / 2.0 + num6 + num8);
            using ImageAttributes imageAttrs = method_236(double_16);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(num9, num10, bitmap.Width, bitmap.Height);
            graphics_0.DrawImage(bitmap, destRect, 0f, 0f, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttrs);
        }

        private void method_262(SpriteBatch spriteBatch_2, int int_11, int int_12, GalaxyLocation galaxyLocation_0, double double_15)
        {
            if (!(galaxyLocation_0.EffectAmount > 0.0) && Galaxy.Rnd.Next(0, 200) != 1)
            {
                return;
            }
            TimeSpan timeSpan = new TimeSpan(galaxy_0.CurrentDateTime.Ticks);
            if (galaxyLocation_0.EffectAmount <= 0.0)
            {
                galaxyLocation_0.EffectAmount = timeSpan.TotalMilliseconds;
                galaxyLocation_0.EffectRandomSeed = Galaxy.Rnd.Next(0, 50000);
            }
            double num = timeSpan.TotalMilliseconds - galaxyLocation_0.EffectAmount;
            if (num > 500.0)
            {
                galaxyLocation_0.EffectAmount = 0.0;
                return;
            }
            double num2 = 1.0 - num % 250.0 / 250.0 * 0.95;
            Random random = new Random(galaxyLocation_0.EffectRandomSeed);
            double num3 = (double)galaxyLocation_0.Width / double_15;
            double num4 = (double)galaxyLocation_0.Height / double_15;
            double num5 = num3 / 2.5;
            double num6 = num3 * 0.15;
            double num7 = num4 * 0.15;
            double num8 = num3 * 0.3 * random.NextDouble();
            double num9 = num4 * 0.3 * random.NextDouble();
            using Bitmap bitmap = lightningGenerator_0.GenerateLightning(galaxyLocation_0.EffectRandomSeed, (int)num5);
            int num10 = (int)((double)int_11 - num3 / 2.0 + num6 + num8);
            int num11 = (int)((double)int_12 - num4 / 2.0 + num7 + num9);
            Texture2D texture2D = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap);
            System.Drawing.Color tintColor = System.Drawing.Color.FromArgb((int)(num2 * 255.0), 255, 255, 255);
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(num10, num11, bitmap.Width, bitmap.Height);
            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, destination, 0f, tintColor);
            method_22(texture2D);
        }

        private System.Drawing.Color method_263(System.Drawing.Color color_18)
        {
            System.Drawing.Color gray = System.Drawing.Color.Gray;
            int num = (color_18.R + color_18.G + color_18.B) / 3;
            if (num > 127)
            {
                return System.Drawing.Color.Black;
            }
            return System.Drawing.Color.White;
        }

        private void method_264(BuiltObjectRole builtObjectRole_0, int int_11, int int_12, int int_13, Brush brush_0, Pen pen_11, Graphics graphics_0)
        {
            int_11 -= int_13 / 2;
            int_12 -= int_13 / 2;
            graphics_0.FillEllipse(brush_0, int_11, int_12, int_13, int_13);
            graphics_0.DrawEllipse(pen_11, int_11, int_12, int_13, int_13);
            float num = int_11;
            float num2 = int_12;
            float num3 = int_13;
            float num4 = num3 / 2f;
            float num5 = num3 * 0.17f;
            float num6 = num3 * 0.83f;
            switch (builtObjectRole_0)
            {
                case BuiltObjectRole.Military:
                    graphics_0.FillRectangle(new SolidBrush(pen_11.Color), num + num4 - 1f, num2 + num4 - 1f, 2f, 2f);
                    break;
                case BuiltObjectRole.Exploration:
                    graphics_0.DrawLine(pen_11, num + num5, num2 + num5, num + num6, num2 + num6);
                    break;
                case BuiltObjectRole.Colony:
                    graphics_0.DrawLine(pen_11, num, num2 + num4, num + num3, num2 + num4);
                    break;
                case BuiltObjectRole.Build:
                    graphics_0.DrawLine(pen_11, num + num5, num2 + num5, num + num6, num2 + num6);
                    graphics_0.DrawLine(pen_11, num + num5, num2 + num6, num + num6, num2 + num5);
                    break;
                case BuiltObjectRole.Freight:
                case BuiltObjectRole.Passenger:
                case BuiltObjectRole.Resource:
                    break;
                case BuiltObjectRole.Base:
                    graphics_0.DrawLine(pen_11, num + num4, num2, num + num4, num2 + num3);
                    graphics_0.DrawLine(pen_11, num, num2 + num4, num + num3, num2 + num4);
                    break;
            }
        }

        private Bitmap method_265(SystemInfo systemInfo_1, int int_11, System.Drawing.Rectangle rectangle_5)
        {
            int num = 0;
            int num2 = 0;
            if (systemInfo_1.DominantEmpire != null)
            {
                num = systemInfo_1.DominantEmpire.Empire.LargeFlagPicture.Width;
                num2 = systemInfo_1.DominantEmpire.Empire.LargeFlagPicture.Height;
            }
            Bitmap bitmap = new Bitmap(num, num, PixelFormat.Format32bppPArgb);
            using (GraphicsPath graphicsPath = new GraphicsPath())
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, num - 1, num - 1);
                graphicsPath.AddEllipse(rect);
                using (new Region(graphicsPath))
                {
                    Bitmap bitmap2 = new Bitmap(num, num, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics = Graphics.FromImage(bitmap2))
                    {
                        SolidBrush brush = new SolidBrush(systemInfo_1.DominantEmpire.Empire.SecondaryColor);
                        System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(0, 0, num, num);
                        graphics.FillRectangle(brush, rect2);
                        graphics.DrawImage(systemInfo_1.DominantEmpire.Empire.LargeFlagPicture, 0, (num - num2) / 2, num, num2);
                        if (int_11 < 255)
                        {
                            bitmap2 = method_16(bitmap2, (float)int_11 / 255f);
                        }
                    }
                    TextureBrush brush2 = new TextureBrush(bitmap2);
                    using Graphics graphics2 = Graphics.FromImage(bitmap);
                    graphics2.InterpolationMode = InterpolationMode.High;
                    graphics2.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics2.FillPath(brush2, graphicsPath);
                }
            }
            return main_0.PrecacheScaledBitmap(bitmap, rectangle_5.Width, rectangle_5.Height);
        }

        private void method_266(Graphics graphics_0, string string_0, Font font_4, System.Drawing.Point point_5)
        {
            method_267(graphics_0, string_0, font_4, point_5, solidBrush_0);
        }

        private void method_267(Graphics graphics_0, string string_0, Font font_4, System.Drawing.Point point_5, Brush brush_0)
        {
            point_5 = new System.Drawing.Point(point_5.X + 1, point_5.Y + 1);
            graphics_0.DrawString(string_0, font_4, solidBrush_2, point_5);
            point_5 = new System.Drawing.Point(point_5.X - 1, point_5.Y - 1);
            graphics_0.DrawString(string_0, font_4, brush_0, point_5);
        }

        private Pen method_268(Empire empire_0, bool bool_13, SystemInfo systemInfo_1, int int_11)
        {
            Pen pen = null;
            SystemVisibilityStatus systemVisibilityStatus = empire_0.CheckSystemVisibilityStatus(systemInfo_1.SystemStar.SystemIndex);
            if (!bool_13 && systemInfo_1.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible || main_0._Game.GodMode))
            {
                System.Drawing.Color color = System.Drawing.Color.FromArgb(int_11, systemInfo_1.DominantEmpire.Empire.MainColor.R, systemInfo_1.DominantEmpire.Empire.MainColor.G, systemInfo_1.DominantEmpire.Empire.MainColor.B);
                pen = new Pen(color, 3f);
                if (systemInfo_1.IsDisputed)
                {
                    pen.DashStyle = DashStyle.Dash;
                }
            }
            else
            {
                switch (systemVisibilityStatus)
                {
                    case SystemVisibilityStatus.Unexplored:
                        pen = new Pen(System.Drawing.Color.FromArgb(int_11, 60, 60, 120), 1.5f);
                        break;
                    case SystemVisibilityStatus.Explored:
                    case SystemVisibilityStatus.Visible:
                        pen = new Pen(System.Drawing.Color.FromArgb(int_11, 112, 112, 112), 1.5f);
                        break;
                }
            }
            return pen;
        }

        private SolidBrush method_269(Empire empire_0, bool bool_13, SystemInfo systemInfo_1, int int_11)
        {
            SolidBrush result = null;
            SystemVisibilityStatus systemVisibilityStatus = empire_0.CheckSystemVisibilityStatus(systemInfo_1.SystemStar.SystemIndex);
            if (!bool_13 && systemInfo_1.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible || main_0._Game.GodMode))
            {
                System.Drawing.Color color = System.Drawing.Color.FromArgb(int_11, systemInfo_1.DominantEmpire.Empire.MainColor.R, systemInfo_1.DominantEmpire.Empire.MainColor.G, systemInfo_1.DominantEmpire.Empire.MainColor.B);
                result = new SolidBrush(color);
            }
            else
            {
                switch (systemVisibilityStatus)
                {
                    case SystemVisibilityStatus.Unexplored:
                        result = new SolidBrush(System.Drawing.Color.FromArgb(int_11, 48, 48, 48));
                        break;
                    case SystemVisibilityStatus.Explored:
                        result = new SolidBrush(System.Drawing.Color.FromArgb(int_11, 112, 112, 112));
                        break;
                    case SystemVisibilityStatus.Visible:
                        result = new SolidBrush(System.Drawing.Color.FromArgb(int_11, 112, 112, 112));
                        break;
                }
            }
            return result;
        }

        public static SamplerState[] SwitchSamplerStatesToPointClamp(GraphicsDevice graphics)
        {
            SamplerState[] array = new SamplerState[16];
            for (int i = 0; i < 16; i++)
            {
                array[i] = graphics.SamplerStates[i];
                graphics.SamplerStates[i] = SamplerState.PointClamp;
                graphics.SamplerStates[i] = SamplerState.LinearClamp;
                graphics.SamplerStates[i] = SamplerState.PointClamp;
            }
            return array;
        }

        public static void RevertSamplerStates(GraphicsDevice graphics, SamplerState[] previousSamplerStates)
        {
            for (int i = 0; i < 16; i++)
            {
                if (previousSamplerStates[i] == null)
                {
                    graphics.SamplerStates[i] = SamplerState.PointClamp;
                }
                else
                {
                    graphics.SamplerStates[i] = previousSamplerStates[i];
                }
            }
        }

    }
}
