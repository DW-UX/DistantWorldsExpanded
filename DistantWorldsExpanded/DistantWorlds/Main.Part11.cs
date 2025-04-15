// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// DistantWorlds.Main
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Management;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using BaconDistantWorlds;
using ExpansionMod;
using DistantWorlds.Controls;
using DistantWorlds.Types;
//using Ionic.Zlib;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Graphics;
//using SlimDX.DirectSound;
using ExpansionMod.HotKeyMapping;
using ExpansionMod.Objects;
using System.Collections.Concurrent;

namespace DistantWorlds {

  public partial class Main {

        internal Bitmap vBqtbUygo3(BuiltObject builtObject_8, Bitmap bitmap_225, Size size_1, Bitmap bitmap_226, List<Point> lightPoints, GraphicsQuality graphicsQuality_0, out bool bool_28)
        {
            bool_28 = false;
            builtObject_8.LightChanged = false;
            if (lightPoints != null && lightPoints.Count > 0)
            {
                bool_28 = true;
                float val = 1f;
                double num = 2.0;
                float val2 = (float)((double)bitmap_226.Width / num / Math.Sqrt(double_0));
                float val3 = (float)((double)bitmap_226.Width / num / Math.Sqrt(double_0));
                val2 = Math.Max(val, val2);
                val3 = Math.Max(val, val3);
                bitmap_226 = ((graphicsQuality_0 != GraphicsQuality.Low) ? PrecacheScaledBitmap(bitmap_226, (int)(val2 + 0.5f), (int)(val3 + 0.5f)) : PrecacheScaledBitmap(bitmap_226, (int)(val2 + 0.5f), (int)(val3 + 0.5f), InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.HighSpeed));
                bitmap_226 = mainView.method_218(bitmap_226, builtObject_8.Heading, graphicsQuality_0);
                Bitmap bitmap = new Bitmap(bitmap_225.Width + bitmap_226.Width, bitmap_225.Height + bitmap_226.Width, PixelFormat.Format32bppPArgb);
                using Graphics graphics = Graphics.FromImage(bitmap);
                method_112(graphics, graphicsQuality_0);
                graphics.DrawImageUnscaled(bitmap_225, new Point(bitmap_226.Width / 2, bitmap_226.Height / 2));
                if (builtObject_8.LightsOn)
                {
                    float num2 = (float)bitmap_225.Width / (float)size_1.Width;
                    method_112(graphics, GraphicsQuality.Medium);
                    {
                        foreach (Point lightPoint in lightPoints)
                        {
                            float num3 = (float)lightPoint.X * num2;
                            float num4 = (float)lightPoint.Y * num2;
                            new PointF(num3, num4);
                            graphics.DrawImage(bitmap_226, num3, num4);
                        }
                        return bitmap;
                    }
                }
                return bitmap;
            }
            return bitmap_225;
        }

        internal void method_112(Graphics graphics_3, GraphicsQuality graphicsQuality_0)
        {
            switch (graphicsQuality_0)
            {
                case GraphicsQuality.Low:
                    graphics_3.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics_3.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics_3.SmoothingMode = SmoothingMode.None;
                    break;
                case GraphicsQuality.Undefined:
                case GraphicsQuality.Medium:
                    graphics_3.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics_3.InterpolationMode = InterpolationMode.Bilinear;
                    graphics_3.SmoothingMode = SmoothingMode.HighSpeed;
                    break;
                case GraphicsQuality.High:
                    graphics_3.CompositingQuality = CompositingQuality.HighQuality;
                    graphics_3.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics_3.SmoothingMode = SmoothingMode.AntiAlias;
                    break;
            }
        }

        internal Bitmap method_113(Bitmap bitmap_225, Bitmap bitmap_226, Brush brush_0, int int_64, Random random_0)
        {
            Bitmap bitmap = new Bitmap(bitmap_225.Width, bitmap_225.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.DrawImageUnscaled(bitmap_225, 0, 0);
            List<Rectangle> list = new List<Rectangle>();
            new List<Color>();
            while (int_64 > 0)
            {
                int num = 0;
                int num2 = random_0.Next(0, 6);
                int num3 = random_0.Next(0, bitmap.Width - 1);
                int num4 = random_0.Next(0, bitmap.Height - 1);
                switch (num2)
                {
                    case 0:
                        list.Add(new Rectangle(num3, num4, 3, 3));
                        list.Add(new Rectangle(num3 + 3, num4, 1, 2));
                        num = 11;
                        break;
                    case 1:
                        list.Add(new Rectangle(num3, num4, 2, 2));
                        list.Add(new Rectangle(num3 + 1, num4 + 2, 2, 2));
                        num = 8;
                        break;
                    case 2:
                        list.Add(new Rectangle(num3, num4, 3, 5));
                        list.Add(new Rectangle(num3 - 1, num4 + 2, 2, 2));
                        num = 19;
                        break;
                    case 3:
                        list.Add(new Rectangle(num3, num4, 2, 2));
                        list.Add(new Rectangle(num3 - 1, num4 - 1, 2, 2));
                        num = 7;
                        break;
                    case 4:
                        list.Add(new Rectangle(num3, num4, 2, 2));
                        list.Add(new Rectangle(num3 - 1, num4 - 1, 1, 1));
                        list.Add(new Rectangle(num3 + 1, num4 + 2, 1, 1));
                        num = 6;
                        break;
                    case 5:
                        list.Add(new Rectangle(num3, num4, 3, 3));
                        list.Add(new Rectangle(num3 - 2, num4, 2, 2));
                        num = 13;
                        break;
                }
                int_64 -= num;
            }
            if (list.Count > 0)
            {
                graphics.FillRectangles(brush_0, list.ToArray());
            }
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.SmoothingMode = SmoothingMode.None;
            Rectangle srcRect = new Rectangle(0, 0, bitmap_226.Width, bitmap_226.Height);
            Rectangle destRect = new Rectangle(0, 0, bitmap_225.Width, bitmap_225.Height);
            SolidBrush brush = new SolidBrush(Color.Black);
            graphics.FillRectangle(brush, new Rectangle(0, 0, bitmap_225.Width, 1));
            graphics.FillRectangle(brush, new Rectangle(0, 0, 1, bitmap_225.Height));
            graphics.FillRectangle(brush, new Rectangle(bitmap_225.Width - 1, 0, 1, bitmap_225.Height));
            graphics.FillRectangle(brush, new Rectangle(0, bitmap_225.Height - 1, bitmap_225.Width, 1));
            graphics.DrawImage(bitmap_226, destRect, srcRect, GraphicsUnit.Pixel);
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        private Bitmap method_114(Bitmap bitmap_225, int int_64, Random random_0)
        {
            Bitmap bitmap = new Bitmap(bitmap_225.Width, bitmap_225.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.DrawImageUnscaled(bitmap_225, 0, 0);
                List<Rectangle> list = new List<Rectangle>();
                while (int_64 > 0)
                {
                    int num = 0;
                    int num2 = random_0.Next(0, 6);
                    int num3 = random_0.Next(0, bitmap.Width - 1);
                    int num4 = random_0.Next(0, bitmap.Height - 1);
                    switch (num2)
                    {
                        case 0:
                            list.Add(new Rectangle(num3, num4, 1, 2));
                            num = 2;
                            break;
                        case 1:
                            list.Add(new Rectangle(num3, num4, 2, 1));
                            num = 2;
                            break;
                        case 2:
                            list.Add(new Rectangle(num3, num4, 2, 1));
                            list.Add(new Rectangle(num3, num4 - 1, 1, 1));
                            list.Add(new Rectangle(num3 + 1, num4 + 1, 1, 1));
                            num = 4;
                            break;
                        case 3:
                            list.Add(new Rectangle(num3, num4, 2, 2));
                            list.Add(new Rectangle(num3 - 1, num4 - 1, 2, 2));
                            num = 7;
                            break;
                        case 4:
                            list.Add(new Rectangle(num3, num4, 2, 2));
                            list.Add(new Rectangle(num3 - 1, num4 - 1, 1, 1));
                            list.Add(new Rectangle(num3 + 1, num4 + 2, 1, 1));
                            num = 6;
                            break;
                        case 5:
                            list.Add(new Rectangle(num3, num4, 3, 3));
                            list.Add(new Rectangle(num3 - 2, num4, 2, 2));
                            num = 13;
                            break;
                    }
                    int_64 -= num;
                }
                if (list.Count > 0)
                {
                    graphics.FillRectangles(solidBrush_27, list.ToArray());
                }
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        internal Bitmap method_115(BuiltObject builtObject_8, Bitmap bitmap_225)
        {
            return method_116(builtObject_8, bitmap_225, 0f);
        }

        internal Bitmap method_116(BuiltObject builtObject_8, Bitmap bitmap_225, float float_2)
        {
            double val = 1.0 - (double)builtObject_8.UnbuiltComponentCount / (double)builtObject_8.Components.Count;
            val = Math.Max(val, float_2);
            return method_117(builtObject_8, bitmap_225, val);
        }

        internal Bitmap method_117(BuiltObject builtObject_8, Bitmap bitmap_225, double double_7)
        {
            if (builtObject_8.UnbuiltComponentCount <= 0)
            {
                return bitmap_225;
            }
            Bitmap bitmap = new Bitmap(bitmap_225.Width, bitmap_225.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.DrawImageUnscaled(bitmap_225, 0, 0);
                int num = (int)((double)bitmap.Width * double_7);
                Random random = new Random(num);
                int i = 0;
                int num2 = Math.Max(3, builtObject_8.Size / 200);
                int num3 = bitmap.Width - num;
                int num6;
                for (; i < bitmap.Height - 1; i += num6)
                {
                    int num4 = random.Next(-num2, num2);
                    int num5 = Math.Max(0, Math.Min(bitmap.Width - 1, num3 + num4));
                    num6 = random.Next(3, 6);
                    int val = i + num6;
                    val = Math.Min(val, bitmap.Height - 1);
                    num6 = Math.Max(1, val - i);
                    graphics.FillRectangle(rect: new Rectangle(0, i, num5, num6), brush: solidBrush_27);
                }
                int j = 0;
                int maxValue = Math.Max(8, builtObject_8.Size / 80);
                List<Rectangle> list = new List<Rectangle>();
                int num8;
                for (; j < bitmap.Height; j += num8)
                {
                    int num7 = random.Next(2, maxValue);
                    graphics.DrawLine(pen_9, 0, j, bitmap.Width - num + num7, j);
                    Rectangle item = new Rectangle(bitmap.Width - num, j, bitmap.Width - num + num7 - (bitmap.Width - num), 1);
                    list.Add(item);
                    num8 = Math.Max(1, random.Next(1, 3));
                }
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        public Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, double width, double height)
        {
            return PrecacheScaledBitmap(unscaledBitmap, (int)(width + 1.0), (int)(height + 1.0));
        }

        public Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, int width, int height)
        {
            return PrecacheScaledBitmap(unscaledBitmap, width, height, InterpolationMode.HighQualityBilinear, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
        }

        public Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, int width, int height, InterpolationMode interpolation, CompositingQuality compositing, SmoothingMode smoothing)
        {
            return PrecacheScaledBitmap(unscaledBitmap, width, height, interpolation, compositing, smoothing, PixelFormat.Format32bppPArgb);
        }

        public Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, int width, int height, InterpolationMode interpolation, CompositingQuality compositing, SmoothingMode smoothing, PixelFormat pixelFormat)
        {
            try
            {
                if (width < 1)
                {
                    width = 1;
                }
                if (height < 1)
                {
                    height = 1;
                }
                Bitmap bitmap = new Bitmap(width, height, pixelFormat);
                if (unscaledBitmap != null && unscaledBitmap.PixelFormat != 0)
                {
                    bitmap.SetResolution(unscaledBitmap.HorizontalResolution, unscaledBitmap.VerticalResolution);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.InterpolationMode = interpolation;
                    graphics.CompositingQuality = compositing;
                    graphics.SmoothingMode = smoothing;
                    graphics.DrawImage(unscaledBitmap, new Rectangle(0, 0, width, height));
                    graphics.Dispose();
                }
                return bitmap;
            }
            catch
            {
                return new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
            }
        }

        internal Bitmap method_118(Empire empire_5, Race race_1, int int_64, int int_65, Bitmap bitmap_225, int int_66, bool bool_28)
        {
            return method_119(empire_5, race_1, int_64, int_65, bitmap_225, int_66, bool_28, PiratePlayStyle.Undefined);
        }

        internal Bitmap method_119(Empire empire_5, Race race_1, int int_64, int int_65, Bitmap bitmap_225, int int_66, bool bool_28, PiratePlayStyle piratePlayStyle_0)
        {
            Bitmap bitmap = new Bitmap(int_64, int_65, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            if (race_1 != null)
            {
                okQtJmsUqH(graphics);
                int int_67 = int_64 - int_66 * 2;
                int int_68 = int_65 - int_66 * 2;
                Bitmap bitmap2 = bitmap_29[method_121(race_1)];
                if (bool_28)
                {
                    bitmap2 = bitmap_189;
                }
                Rectangle destRect = method_120(bitmap2.Width, bitmap2.Height, int_67, int_68);
                Rectangle srcRect = new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
                destRect.Offset(int_66, int_66);
                graphics.DrawImage(bitmap2, destRect, srcRect, GraphicsUnit.Pixel);
                Bitmap bitmap3 = null;
                if (empire_5 != null)
                {
                    bitmap3 = raceImageCache_0.GetEmpireDominantRaceImage(empire_5, useSmallSize: false, useAlternate: true, useRaceWhenPirate: false);
                    if (bitmap3 == null)
                    {
                        bitmap3 = raceImageCache_0.GetEmpireDominantRaceImage(empire_5, useSmallSize: false, useAlternate: false, useRaceWhenPirate: false);
                    }
                }
                else if (bool_28 && piratePlayStyle_0 != 0)
                {
                    bitmap3 = raceImageCache_0.GetPirateImage(piratePlayStyle_0);
                    if (bitmap3 == null)
                    {
                        bitmap3 = raceImageCache_0.GetRaceImage(race_1.PictureRef, useSmall: false, useAlternate: true);
                        if (bitmap3 == null)
                        {
                            bitmap3 = raceImageCache_0.GetRaceImage(race_1.PictureRef);
                        }
                    }
                }
                else
                {
                    bitmap3 = raceImageCache_0.GetRaceImage(race_1.PictureRef, useSmall: false, useAlternate: true);
                    if (bitmap3 == null)
                    {
                        bitmap3 = raceImageCache_0.GetRaceImage(race_1.PictureRef);
                    }
                }
                destRect = method_120(bitmap3.Width, bitmap3.Height, int_67, int_68);
                srcRect = new Rectangle(0, 0, bitmap3.Width, bitmap3.Height);
                destRect.Offset(int_66, int_66);
                graphics.DrawImage(bitmap3, destRect, srcRect, GraphicsUnit.Pixel);
                if (bitmap_225 != null)
                {
                    destRect = new Rectangle(0, 0, int_64, int_65);
                    srcRect = new Rectangle(0, 0, bitmap_225.Width, bitmap_225.Height);
                    graphics.DrawImage(bitmap_225, destRect, srcRect, GraphicsUnit.Pixel);
                    return bitmap;
                }
                return bitmap;
            }
            return bitmap;
        }

        private Rectangle method_120(int int_64, int int_65, int int_66, int int_67)
        {
            double val = (double)int_66 / (double)int_64;
            double val2 = (double)int_67 / (double)int_65;
            double num = Math.Max(val, val2);
            int num2 = (int)((double)int_64 * num);
            int num3 = (int)((double)int_65 * num);
            return new Rectangle((int_66 - num2) / 2, (int_67 - num3) / 2, num2, num3);
        }

        private int method_121(Race race_1)
        {
            if (race_1 != null)
            {
                switch (race_1.NativeHabitatType)
                {
                    case HabitatType.Volcanic:
                        return GalaxyImages.LandscapeImageOffsetVolcanic + race_1.PictureRef % GalaxyImages.LandscapeImageCountVolcanic;
                    case HabitatType.Desert:
                        return GalaxyImages.LandscapeImageOffsetDesert + race_1.PictureRef % GalaxyImages.LandscapeImageCountDesert;
                    case HabitatType.MarshySwamp:
                        return GalaxyImages.LandscapeImageOffsetMarshySwamp + race_1.PictureRef % GalaxyImages.LandscapeImageCountMarshySwamp;
                    case HabitatType.Continental:
                        return GalaxyImages.LandscapeImageOffsetContinental + race_1.PictureRef % GalaxyImages.LandscapeImageCountContinental;
                    case HabitatType.Ocean:
                        return GalaxyImages.LandscapeImageOffsetOcean + race_1.PictureRef % GalaxyImages.LandscapeImageCountOcean;
                    case HabitatType.BarrenRock:
                        return GalaxyImages.LandscapeImageOffsetBarrenRock + race_1.PictureRef % GalaxyImages.LandscapeImageCountBarrenRock;
                    case HabitatType.Ice:
                        return GalaxyImages.LandscapeImageOffsetIce + race_1.PictureRef % GalaxyImages.LandscapeImageCountIce;
                }
            }
            return 0;
        }

        private void method_122()
        {
            if (int_28 < 0)
            {
                return;
            }
            int num = (int)(_Game.Galaxy.Habitats[int_28].Xpos - (double)int_13) / int_35 + picSystem.ClientRectangle.Width / 2;
            int num2 = (int)(_Game.Galaxy.Habitats[int_28].Ypos - (double)int_14) / int_35 + picSystem.ClientRectangle.Height / 2;
            for (int i = int_28 + 1; i <= int_29; i++)
            {
                if (_Game.Galaxy.Habitats[i].Category == HabitatCategoryType.Planet)
                {
                    int num3 = _Game.Galaxy.Habitats[i].OrbitDistance / int_35;
                    graphics_1.DrawEllipse(pen_0, num - num3, num2 - num3, num3 * 2, num3 * 2);
                }
            }
        }

        public double CalculateShipZoomFactor(double actualZoomFactor, out double maxWidth)
        {
            double result = actualZoomFactor;
            maxWidth = 1200.0;
            if (actualZoomFactor > 3.0)
            {
                result = Math.Max(3.0, actualZoomFactor / 3.0);
                maxWidth /= actualZoomFactor;
            }
            return result;
        }

        public double CalculateCreatureZoomFactor(double actualZoomFactor, out double maxWidth)
        {
            double result = actualZoomFactor;
            maxWidth = 240.0;
            if (actualZoomFactor > 3.0)
            {
                result = Math.Max(3.0, actualZoomFactor / 2.0);
                maxWidth /= actualZoomFactor;
            }
            return result;
        }

        public double CalculatePlanetZoomFactor(double actualZoomFactor)
        {
            double result = actualZoomFactor;
            if (actualZoomFactor > 10.0)
            {
                result = Math.Max(10.0, actualZoomFactor / 1.25);
            }
            return result;
        }

        public double CalculateMoonZoomFactor(double actualZoomFactor)
        {
            double result = actualZoomFactor;
            if (actualZoomFactor > 10.0)
            {
                result = Math.Max(10.0, actualZoomFactor / 1.1);
            }
            return result;
        }

        private BuiltObjectList method_123()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            int num = -25000;
            int num2 = mainView.Width + 25000;
            int num3 = -25000;
            int num4 = mainView.Height + 25000;
            int num5 = (int)((double)base.ClientRectangle.Width * double_0);
            num5 += Galaxy.MaxSolarSystemSize * 2;
            BuiltObjectList builtObjectsAtLocation = _Game.Galaxy.GetBuiltObjectsAtLocation(int_13, int_14, num5);
            for (int i = 0; i < builtObjectsAtLocation.Count; i++)
            {
                BuiltObject builtObject = builtObjectsAtLocation[i];
                if (builtObject != null)
                {
                    int num6 = (int)builtObject.Xpos - int_13 + mainView.Width / 2;
                    int num7 = (int)builtObject.Ypos - int_14 + mainView.Height / 2;
                    if (num6 >= num && num6 <= num2 && num7 >= num3 && num7 <= num4 && !builtObjectList.Contains(builtObject))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        public void ProcessMain(DateTime time, long starDate, BuiltObjectList builtObjectsInView)
        {
            if (builtObjectsInView == null)
            {
                builtObjectsInView = new BuiltObjectList();
            }
            int num = int_13 / Galaxy.IndexSize;
            int num2 = int_14 / Galaxy.IndexSize;
            Galaxy.CorrectIndexCoords(ref num, ref num2);
            if (int_28 >= 0)
            {
                if (int_28 >= _Game.Galaxy.Habitats.Count || int_29 >= _Game.Galaxy.Habitats.Count)
                {
                    if (int_13 < 0 || int_14 < 0)
                    {
                        int_13 = 10000000;
                        int_14 = 10000000;
                    }
                    method_149();
                }
                for (int i = int_28; i <= int_29; i++)
                {
                    if (i < _Game.Galaxy.Habitats.Count)
                    {
                        _Game.Galaxy.Habitats[i]?.DoTasks(time);
                    }
                }
            }
            if (bool_19)
            {
                picSystem.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: true, int_28, int_35, drawViewIndicator: true, erasePrevious: true, clearFirst: false, showIndicatorLines: false, string.Empty);
                picSystem.ShowFleetPostures = true;
                picSystem.ShowBuiltObjects = true;
                mainView.animationSystem_0.ClearAnimations();
                mainView.animationSystem_1.ClearAnimations();
                mainView.bitmap_2 = null;
                mainView.bitmap_3 = null;
                mainView.rectangleF_0 = Rectangle.Empty;
                mainView.ClearNebulaeImages();
                mainView.list_35 = null;
                bool_19 = false;
            }
            if (bool_20)
            {
                mainView.ClearPrecachedHabitatBitmaps();
                mainView.ClearPreprocessedBuiltObjectImages();
                mainView.ClearPreprocessedFighterImages();
                mainView.ClearPreprocessedCreatureImages();
                mainView.FadeGalaxyBackground();
                mainView.InvalidateMain();
                picSystem.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: true, int_28, int_35, drawViewIndicator: true, erasePrevious: true, clearFirst: false, showIndicatorLines: false, string.Empty);
                picSystem.ShowBuiltObjects = true;
                picSystem.ShowFleetPostures = true;
                bool_20 = false;
            }
            for (int j = 0; j < builtObjectsInView.Count; j++)
            {
                if (builtObjectsInView[j] != null)
                {
                    builtObjectsInView[j].DoTasks(time, starDate, inView: true);
                }
            }
            if (habitat_6 != null)
            {
                for (int k = 0; k < _Game.Galaxy.Systems[habitat_6.SystemIndex].Creatures.Count; k++)
                {
                    if (_Game.Galaxy.Systems[habitat_6.SystemIndex].Creatures[k] != null)
                    {
                        _Game.Galaxy.Systems[habitat_6.SystemIndex].Creatures[k].DoTasks(time);
                    }
                }
            }
            if (UhvLmNjli7 && _Game.SelectedObject != null)
            {
                if (_Game.SelectedObject is Habitat)
                {
                    int_13 = (int)((Habitat)_Game.SelectedObject).Xpos;
                    int_14 = (int)((Habitat)_Game.SelectedObject).Ypos;
                    method_149();
                }
                else if (_Game.SelectedObject is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)_Game.SelectedObject;
                    int_13 = (int)builtObject.Xpos;
                    int_14 = (int)builtObject.Ypos;
                    if (builtObject.Mission != null && builtObject.Mission.ShowCurrentCommand() != null && builtObject.Mission.ShowCurrentCommand().Action == CommandAction.HyperTo)
                    {
                        method_149();
                    }
                }
                else if (_Game.SelectedObject is Fighter)
                {
                    Fighter fighter = (Fighter)_Game.SelectedObject;
                    int_13 = (int)fighter.Xpos;
                    int_14 = (int)fighter.Ypos;
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.Mission != null && fighter.ParentBuiltObject.Mission.ShowCurrentCommand() != null && fighter.ParentBuiltObject.Mission.ShowCurrentCommand().Action == CommandAction.HyperTo)
                    {
                        method_149();
                    }
                }
                else if (_Game.SelectedObject is Creature)
                {
                    Creature creature = (Creature)_Game.SelectedObject;
                    int_13 = (int)creature.Xpos;
                    int_14 = (int)creature.Ypos;
                    method_149();
                }
                else if (_Game.SelectedObject is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)_Game.SelectedObject;
                    if (shipGroup.LeadShip != null)
                    {
                        int_13 = (int)shipGroup.LeadShip.Xpos;
                        int_14 = (int)shipGroup.LeadShip.Ypos;
                        if (shipGroup.LeadShip.Mission != null && shipGroup.LeadShip.Mission.ShowCurrentCommand() != null && shipGroup.LeadShip.Mission.ShowCurrentCommand().Action == CommandAction.HyperTo)
                        {
                            method_149();
                        }
                    }
                }
            }
            if (Control.MouseButtons == MouseButtons.Left)
            {
                Point point = PointToClient(MouseHelper.GetCursorPosition());
                itemListCollectionPanel_0.MouseDown(point);
            }
            if (time.Subtract(dateTime_2).TotalMilliseconds >= double_2)
            {
                if (time.Subtract(dateTime_3).TotalMilliseconds >= double_3)
                {
                    method_76(itemListCollectionPanel_0.ActivePanel);
                    method_390();
                    pnlDetailInfo.Hotspots.Clear();
                    pnlDetailInfo.AddHotspots = true;
                    pnlDetailInfo.ReDraw();
                    if (_Game != null && _Game.SelectedObject != null && btnSelectionAction1.Tag != null && btnSelectionAction1.Tag is ShipAction)
                    {
                        ShipAction shipAction = (ShipAction)btnSelectionAction1.Tag;
                        if (shipAction.ActionType != 0)
                        {
                            switch (shipAction.ActionType)
                            {
                                case ShipActionType.FighterBuildFighter:
                                case ShipActionType.FighterBuildBomber:
                                case ShipActionType.FighterLaunchFighters:
                                case ShipActionType.FighterLaunchBombers:
                                case ShipActionType.FighterRetrieveFighters:
                                case ShipActionType.FighterRetrieveBombers:
                                    if (shipAction.Target is Fighter)
                                    {
                                        method_592();
                                    }
                                    else
                                    {
                                        method_593(new ShipAction(ShipActionType.FighterOptions, _Game.SelectedObject));
                                    }
                                    break;
                                default:
                                    method_592();
                                    break;
                                case ShipActionType.BuildPlanetaryFacility:
                                    if (shipAction.Target is PlanetaryFacilityDefinition)
                                    {
                                        PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)shipAction.Target;
                                        if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.Wonder)
                                        {
                                            method_593(new ShipAction(ShipActionType.ColonyBuildWonder, _Game.SelectedObject));
                                        }
                                        else
                                        {
                                            method_593(new ShipAction(ShipActionType.ColonyBuildOptions, _Game.SelectedObject));
                                        }
                                    }
                                    break;
                            }
                        }
                        else if (shipAction.MissionType != 0)
                        {
                            BuiltObjectMissionType missionType = shipAction.MissionType;
                            if (missionType == BuiltObjectMissionType.Build)
                            {
                                if (!(shipAction.Target is BuiltObject) || shipAction.Target != _Game.SelectedObject)
                                {
                                    method_593(new ShipAction(ShipActionType.BuildOptions, null));
                                }
                            }
                            else
                            {
                                method_592();
                            }
                        }
                        else
                        {
                            method_592();
                        }
                    }
                    dateTime_3 = time;
                }
                else
                {
                    pnlDetailInfo.ReDraw();
                }
                dateTime_2 = time;
            }
            method_219();
            if (pnlColonyInvasionContainer.Visible)
            {
                pnlColonyInvasionContainer.Invalidate();
            }
        }

        private void method_124(DateTime dateTime_7)
        {
            TimeSpan timeSpan = dateTime_7.Subtract(dateTime_1);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan2 = now.Subtract(dateTime_4);
            if (timeSpan.TotalMilliseconds < 0.0)
            {
                timeSpan = new TimeSpan(0, 0, 0, 0, (int)double_1);
            }
            if (timeSpan.TotalMilliseconds >= double_1)
            {
                method_127();
                method_126();
                dateTime_1 = dateTime_7;
            }
            if (timeSpan2.TotalMilliseconds >= double_1)
            {
                picSystem.Invalidate();
                dateTime_4 = now;
            }
        }

        private void method_125()
        {
            string text = "Habitats: " + _Game.Galaxy.Habitats.Count;
            text += "\n";
            text = text + "Ruins: " + _Game.Galaxy.RuinCount;
            text += "\n";
            text = text + "Creatures: " + _Game.Galaxy.Creatures.Count;
            text += "\n";
            text = text + "Empires: " + _Game.Galaxy.Empires.Count;
            text += "\n";
            text = text + "Colonies: " + _Game.Galaxy.ColonyCount;
            text += "\n";
            text = text + "BuiltObjects: " + _Game.Galaxy.BuiltObjects.Count + " (incl dest)";
            text += "\n";
            text = text + "    Abandoned: " + _Game.Galaxy.AbandonedShipCount;
            text += "\n";
            List<BuiltObjectRole> list = new List<BuiltObjectRole>();
            list.Add(BuiltObjectRole.Freight);
            text = text + "    Freighters: " + _Game.Galaxy.BuiltObjects.GetBuiltObjectsByRole(list).Count;
            text += "\n";
            list.Clear();
            list.Add(BuiltObjectRole.Military);
            text = text + "    Military: " + _Game.Galaxy.BuiltObjects.GetBuiltObjectsByRole(list).Count;
            text += "\n";
            list.Clear();
            list.Add(BuiltObjectRole.Colony);
            text = text + "    Colony: " + _Game.Galaxy.BuiltObjects.GetBuiltObjectsByRole(list).Count;
            text += "\n";
            List<BuiltObjectSubRole> list2 = new List<BuiltObjectSubRole>();
            list2.Add(BuiltObjectSubRole.GasMiningShip);
            list2.Add(BuiltObjectSubRole.MiningShip);
            text = text + "    Mining Ships: " + _Game.Galaxy.BuiltObjects.GetBuiltObjectsBySubRole(list2).Count;
            text += "\n";
            list.Clear();
            list.Add(BuiltObjectRole.Exploration);
            text = text + "    Exploration: " + _Game.Galaxy.BuiltObjects.GetBuiltObjectsByRole(list).Count;
            text += "\n";
            list.Clear();
            list.Add(BuiltObjectRole.Base);
            text = text + "    Bases: " + _Game.Galaxy.BuiltObjects.GetBuiltObjectsByRole(list).Count;
            text += "\n";
            text = text + "Independent BuiltObjects: " + _Game.Galaxy.IndependentEmpire.PrivateBuiltObjects.Count;
            text += "\n";
            list.Clear();
            list.Add(BuiltObjectRole.Freight);
            text = text + "    Freighters: " + _Game.Galaxy.IndependentEmpire.PrivateBuiltObjects.GetBuiltObjectsByRole(list).Count;
            text += "\n";
            text = text + "Pirate Empires: " + _Game.Galaxy.PirateEmpires.Count;
            text += "\n";
            int num = 0;
            for (int i = 0; i < _Game.Galaxy.PirateEmpires.Count; i++)
            {
                Empire empire = _Game.Galaxy.PirateEmpires[i];
                num += empire.BuiltObjects.Count;
            }
            text = text + "    Pirate BuiltObjects: " + num;
            text += "\n";
            text = text + "Orders: " + _Game.Galaxy.Orders.Count;
            text += "\n";
            string text2 = text;
            text = text2 + "Invasions: " + _Game.Galaxy.InvasionAttempts + ", " + _Game.Galaxy.InvasionSuccesses + " S, " + _Game.Galaxy.InvasionFailures + " F";
            text += "\n";
            text = text + "Running Time: " + _Game.Galaxy.TotalRunningTime.ToString();
            lblGodData.Text = text;
        }

        private void method_126()
        {
            if (_Game.PlayerEmpire == null)
            {
                return;
            }
            string_23 = _Game.PlayerEmpire.StateMoney.ToString("#,###,###,##0");
            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
            {
                _Game.PlayerEmpire.CheckAgeVariableIncome();
                _Game.PlayerEmpire.ObtainAveragedVariableIncome();
                double num = _Game.PlayerEmpire.AnnualTaxRevenue + _Game.PlayerEmpire.CalculateAnnualSubjugationTributeIncome();
                double num2 = _Game.PlayerEmpire.ThisYearsForeignTradeBonuses + _Game.PlayerEmpire.ThisYearsResortIncome + _Game.PlayerEmpire.ThisYearsSpacePortIncome;
                double num3 = _Game.PlayerEmpire.AnnualStateMaintenanceExcludingUnderConstruction + _Game.PlayerEmpire.AnnualTroopMaintenance + _Game.PlayerEmpire.AnnualFacilityMaintenance + _Game.PlayerEmpire.AnnualSubjugationTribute + _Game.PlayerEmpire.ThisYearsStateFuelCosts + _Game.PlayerEmpire.AnnualPirateProtection;
                AheLexjQsu = num - num3;
                string_24 = AheLexjQsu.ToString("+##,###,##0;-##,###,##0");
                string_25 = num2.ToString("+##,###,##0;-##,###,##0");
            }
            else if (_Game.PlayerEmpire.PirateEconomy != null)
            {
                PirateEconomyYear pirateEconomyYear = _Game.PlayerEmpire.PirateEconomy.LastYear;
                if (pirateEconomyYear == null)
                {
                    pirateEconomyYear = _Game.PlayerEmpire.PirateEconomy.ThisYear;
                }
                AheLexjQsu = pirateEconomyYear.StableCashflow;
                string_24 = AheLexjQsu.ToString("+##,###,##0;-##,###,##0");
                string_25 = _Game.PlayerEmpire.PirateEconomy.ThisYear.BonusIncome.ToString("+##,###,##0;-##,###,##0");
            }
        }

        private void method_127()
        {
            string_21 = Galaxy.ResolveStarDateDescription(_Game.Galaxy.CurrentStarDate);
        }

        private void method_128(bool bool_28, int int_64, ref Graphics graphics_3, ref GradientPanel gradientPanel_0, int int_65, bool bool_29, bool bool_30, bool bool_31, bool bool_32)
        {
            gradientPanel_0.DrawPanelBackground(graphics_3);
            int num = _Game.Galaxy.Habitats.Count - 1;
            for (int i = int_64 + 1; i < _Game.Galaxy.Habitats.Count; i++)
            {
                if (_Game.Galaxy.Habitats[i].Parent == null)
                {
                    num = i - 1;
                    break;
                }
            }
            int num2 = 0;
            int num3 = 0;
            if (bool_28)
            {
                num2 = int_13;
                num3 = int_14;
            }
            else
            {
                num2 = (int)_Game.Galaxy.Habitats[int_64].Xpos;
                num3 = (int)_Game.Galaxy.Habitats[int_64].Ypos;
            }
            if (bool_31)
            {
                gradientPanel_0.Clear(graphics_3);
            }
            if (bool_32 && habitat_8 != null)
            {
                int num4 = ((int)habitat_8.Xpos - (int)habitat_7.Xpos) / int_65 + gradientPanel_0.Width / 2 + 1;
                int num5 = ((int)habitat_8.Ypos - (int)habitat_7.Ypos) / int_65 + gradientPanel_0.Height / 2 + 1;
                graphics_3.DrawLine(pen_2, num4, 0, num4, gradientPanel_0.Height);
                graphics_3.DrawLine(pen_2, 0, num5, gradientPanel_0.Width, num5);
            }
            if (int_64 < 0)
            {
                return;
            }
            for (int j = int_64; j <= num; j++)
            {
                int num6 = ((_Game.Galaxy.Habitats[j].Diameter > 1250) ? 7 : ((_Game.Galaxy.Habitats[j].Diameter > 1000) ? 6 : ((_Game.Galaxy.Habitats[j].Diameter > 700) ? 5 : ((_Game.Galaxy.Habitats[j].Diameter >= 300) ? 4 : ((_Game.Galaxy.Habitats[j].Diameter >= 160) ? 3 : ((_Game.Galaxy.Habitats[j].Diameter < 50) ? 1 : 2))))));
                int num7 = (int)(_Game.Galaxy.Habitats[j].Xpos - (double)num2) / int_65 + gradientPanel_0.ClientRectangle.Width / 2;
                int num8 = (int)(_Game.Galaxy.Habitats[j].Ypos - (double)num3) / int_65 + gradientPanel_0.ClientRectangle.Height / 2;
                if (j == int_64)
                {
                    for (int k = int_64 + 1; k <= num; k++)
                    {
                        if (_Game.Galaxy.Habitats[k].Category == HabitatCategoryType.Planet)
                        {
                            int num9 = _Game.Galaxy.Habitats[k].OrbitDistance / int_65;
                            graphics_3.DrawEllipse(pen_1, num7 - num9, num8 - num9, num9 * 2, num9 * 2);
                        }
                    }
                    if (bool_29)
                    {
                        int num10 = gradientPanel_0.ClientRectangle.Width / 2;
                        int num11 = gradientPanel_0.ClientRectangle.Height / 2;
                        int num12 = mainView.ClientRectangle.Width / (int)((double)int_65 / double_0);
                        int num13 = mainView.ClientRectangle.Height / (int)((double)int_65 / double_0);
                        graphics_3.DrawLine(pen_2, num10 - num12 / 2, num11 - num13 / 2, num10 + num12 / 2, num11 - num13 / 2);
                        graphics_3.DrawLine(pen_2, num10 - num12 / 2, num11 - num13 / 2, num10 - num12 / 2, num11 + num13 / 2);
                        graphics_3.DrawLine(pen_2, num10 + num12 / 2, num11 - num13 / 2, num10 + num12 / 2, num11 + num13 / 2);
                        graphics_3.DrawLine(pen_2, num10 - num12 / 2, num11 + num13 / 2, num10 + num12 / 2, num11 + num13 / 2);
                    }
                }
                Rectangle rectangle = new Rectangle(num7 - num6 / 2, num8 - num6 / 2, num6, num6);
                if (bool_30)
                {
                    graphics_3.FillRectangle(solidBrush_0, rectangle_1[j - int_64]);
                    rectangle_1[j - int_64] = rectangle;
                }
                switch (_Game.Galaxy.Habitats[j].Type)
                {
                    case HabitatType.MainSequence:
                        graphics_3.FillRectangle(solidBrush_17, rectangle);
                        break;
                    case HabitatType.RedGiant:
                        graphics_3.FillRectangle(solidBrush_18, rectangle);
                        break;
                    case HabitatType.SuperGiant:
                        graphics_3.FillRectangle(solidBrush_19, rectangle);
                        break;
                    case HabitatType.WhiteDwarf:
                        graphics_3.FillRectangle(solidBrush_20, rectangle);
                        break;
                    case HabitatType.Neutron:
                        graphics_3.FillRectangle(solidBrush_21, rectangle);
                        break;
                    case HabitatType.BlackHole:
                        graphics_3.FillRectangle(solidBrush_23, rectangle);
                        break;
                    case HabitatType.SuperNova:
                        graphics_3.FillRectangle(solidBrush_22, rectangle);
                        break;
                    case HabitatType.Volcanic:
                        graphics_3.FillRectangle(solidBrush_8, rectangle);
                        break;
                    case HabitatType.Desert:
                        graphics_3.FillRectangle(solidBrush_9, rectangle);
                        break;
                    case HabitatType.MarshySwamp:
                        graphics_3.FillRectangle(solidBrush_10, rectangle);
                        break;
                    case HabitatType.Continental:
                        graphics_3.FillRectangle(solidBrush_11, rectangle);
                        break;
                    case HabitatType.Ocean:
                        graphics_3.FillRectangle(solidBrush_12, rectangle);
                        break;
                    case HabitatType.BarrenRock:
                        graphics_3.FillRectangle(solidBrush_7, rectangle);
                        break;
                    case HabitatType.Ice:
                        graphics_3.FillEllipse(solidBrush_13, rectangle);
                        break;
                    case HabitatType.GasGiant:
                        graphics_3.FillRectangle(solidBrush_14, rectangle);
                        break;
                    case HabitatType.FrozenGasGiant:
                        graphics_3.FillRectangle(solidBrush_15, rectangle);
                        break;
                    case HabitatType.Hydrogen:
                    case HabitatType.Helium:
                    case HabitatType.Argon:
                    case HabitatType.Ammonia:
                    case HabitatType.CarbonDioxide:
                    case HabitatType.Oxygen:
                    case HabitatType.NitrogenOxygen:
                    case HabitatType.Chlorine:
                        graphics_3.FillRectangle(solidBrush_24, rectangle);
                        break;
                }
            }
        }

        private void method_129()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlGalaxyMapKey.Size = new Size(300, 418);
            pnlGalaxyMapKey.Location = new Point((mainView.Width - pnlGalaxyMapKey.Width) / 2, (mainView.Height - pnlGalaxyMapKey.Height) / 2);
            lblGalaxyMapKeyTitle.Location = new Point(10, 10);
            lblGalaxyMapKeyTitle.BackColor = Color.Transparent;
            lblGalaxyMapKeyTitle.ForeColor = color_0;
            lblGalaxyMapKeyTitle.Font = font_1;
            pnlGalaxyMapKeyActual.BringToFront();
            pnlGalaxyMapKeyActual.Location = new Point(10, 40);
            pnlGalaxyMapKeyActual.Size = new Size(280, 368);
            btnGalaxyMapKeyClose.Location = new Point(140, 10);
            pnlGalaxyMapKey.BringToFront();
            pnlGalaxyMapKey.Visible = true;
        }

        private void method_130()
        {
            pnlGalaxyMapKey.SendToBack();
            pnlGalaxyMapKey.Visible = false;
        }

        private void method_131(Habitat habitat_9)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            CaLkaMyrMQ.Size = new Size(945, 760);
            CaLkaMyrMQ.Location = new Point((mainView.Width - CaLkaMyrMQ.Width) / 2, (mainView.Height - CaLkaMyrMQ.Height) / 2);
            CaLkaMyrMQ.DoLayout();
            gmapMain.Size = new Size(650, 650);
            gmapMain.Location = new Point(10, 40);
            omjYcxcvXH.SelectedIndex = 0;
            omjYcxcvXH.Size = new Size(115, 20);
            omjYcxcvXH.Location = new Point(205, 8);
            omjYcxcvXH.Font = font_3;
            omjYcxcvXH.BackColor = Color.FromArgb(48, 48, 64);
            omjYcxcvXH.ForeColor = Color.FromArgb(170, 170, 170);
            cmbGalaxyMapHabitatType.SelectedIndex = 0;
            cmbGalaxyMapHabitatType.Size = new Size(115, 20);
            cmbGalaxyMapHabitatType.Location = new Point(205, 8);
            cmbGalaxyMapHabitatType.Font = font_3;
            cmbGalaxyMapHabitatType.BackColor = Color.FromArgb(48, 48, 64);
            cmbGalaxyMapHabitatType.ForeColor = Color.FromArgb(170, 170, 170);
            lblGalaxyMapViewModeLabel.Font = new Font(font_6, FontStyle.Bold);
            lblGalaxyMapViewModeLabel.ForeColor = color_2;
            lblGalaxyMapViewModeLabel.BackColor = Color.Transparent;
            lblGalaxyMapViewModeLabel.Location = new Point(10, 10);
            lblGalaxyMapViewModeLabel.Text = TextResolver.GetText("View");
            lblGalaxyMapViewModeLabel.BringToFront();
            cmbGalaxyMapViewMode.Size = new Size(130, 20);
            cmbGalaxyMapViewMode.Location = new Point(65, 8);
            cmbGalaxyMapViewMode.Font = font_3;
            cmbGalaxyMapViewMode.BackColor = Color.FromArgb(48, 48, 64);
            cmbGalaxyMapViewMode.ForeColor = Color.FromArgb(170, 170, 170);
            btnGalaxyMapBack.Size = new Size(40, 30);
            btnGalaxyMapForward.Size = new Size(40, 30);
            btnGalaxyMapBack.ImageAlign = ContentAlignment.MiddleCenter;
            btnGalaxyMapForward.ImageAlign = ContentAlignment.MiddleCenter;
            btnGalaxyMapBack.Location = new Point(575, 6);
            btnGalaxyMapForward.Location = new Point(620, 6);
            btnGalaxyMapKey.Location = new Point(330, 8);
            btnGalaxyMapKey.Size = new Size(75, 25);
            btnGalaxyMapGoto.Location = new Point(410, 8);
            btnGalaxyMapGoto.Size = new Size(140, 25);
            picSystemMap.Size = new Size(250, 250);
            picSystemMap.Location = new Point(670, 8);
            pnlHabitatInfo.Size = new Size(250, 240);
            pnlHabitatInfo.Location = new Point(670, 267);
            pnlHabitatInfo.CurveMode = CornerCurveMode.BottomRight_TopLeft;
            pnlHabitatInfo.Reset();
            pnlGalaxyMapHabitatPicture.Size = new Size(250, 174);
            pnlGalaxyMapHabitatPicture.Location = new Point(670, 516);
            pnlGalaxyMapHabitatPicture.BackgroundImageLayout = ImageLayout.Center;
            txtHabitatSearch.Visible = false;
            lvwHabitats.Visible = false;
            habitatList_1 = null;
            habitatList_2 = null;
            if (habitat_9 == null && _Game != null && _Game.SelectedObject != null)
            {
                if (_Game.SelectedObject is Habitat)
                {
                    habitat_9 = (Habitat)_Game.SelectedObject;
                }
                else if (_Game.SelectedObject is SystemInfo)
                {
                    SystemInfo systemInfo = (SystemInfo)_Game.SelectedObject;
                    habitat_9 = systemInfo.SystemStar;
                }
            }
            if (habitat_9 != null)
            {
                method_152(habitat_9);
            }
            else
            {
                picSystemMap.SetSelectedHabitat(null);
                gmapMain.SetSystem(null);
                gmapMain.Invalidate();
            }
            int selectedIndex = cmbGalaxyMapViewMode.SelectedIndex;
            cmbGalaxyMapViewMode.SelectedIndex = 0;
            if (selectedIndex >= 0)
            {
                cmbGalaxyMapViewMode.SelectedIndex = selectedIndex;
            }
            method_215();
            CaLkaMyrMQ.BringToFront();
            CaLkaMyrMQ.Visible = true;
        }

        private void method_132()
        {
            method_130();
            habitatList_0.Clear();
            int_24 = 0;
            CaLkaMyrMQ.SendToBack();
            CaLkaMyrMQ.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private ShipGroupList method_133(double double_7, double double_8, int int_64)
        {
            ShipGroupList shipGroupList = new ShipGroupList();
            for (int i = 0; i < _Game.Galaxy.Empires.Count; i++)
            {
                Empire empire = _Game.Galaxy.Empires[i];
                for (int j = 0; j < empire.ShipGroups.Count; j++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[j];
                    if (_Game.Galaxy.CheckWithinDistancePotential(int_64, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, double_7, double_8))
                    {
                        double num = _Game.Galaxy.CalculateDistance(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, double_7, double_8);
                        if ((int)num <= int_64 && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(shipGroup.LeadShip))
                        {
                            shipGroupList.Add(shipGroup);
                        }
                    }
                }
            }
            shipGroupList.Sort();
            return shipGroupList;
        }

        private BuiltObjectList method_134(double double_7, double double_8, int int_64)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            GalaxyIndex galaxyIndex = _Game.Galaxy.ResolveIndex(double_7, double_8);
            for (int i = 0; i < _Game.Galaxy.BuiltObjectIndex[galaxyIndex.X][galaxyIndex.Y].Count; i++)
            {
                BuiltObject builtObject = _Game.Galaxy.BuiltObjectIndex[galaxyIndex.X][galaxyIndex.Y][i];
                if (builtObject != null && builtObject.Role != BuiltObjectRole.Base && _Game.Galaxy.CheckWithinDistancePotential(int_64, builtObject.Xpos, builtObject.Ypos, double_7, double_8))
                {
                    double num = _Game.Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, double_7, double_8);
                    if ((int)num <= int_64)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        private BuiltObjectList method_135(double double_7, double double_8, int int_64)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            GalaxyIndex galaxyIndex = _Game.Galaxy.ResolveIndex(double_7, double_8);
            for (int i = 0; i < _Game.Galaxy.BuiltObjectIndex[galaxyIndex.X][galaxyIndex.Y].Count; i++)
            {
                BuiltObject builtObject = _Game.Galaxy.BuiltObjectIndex[galaxyIndex.X][galaxyIndex.Y][i];
                if (builtObject != null && builtObject.Role == BuiltObjectRole.Base && _Game.Galaxy.CheckWithinDistancePotential(int_64, builtObject.Xpos, builtObject.Ypos, double_7, double_8))
                {
                    double num = _Game.Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, double_7, double_8);
                    if ((int)num <= int_64 && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        private HabitatList method_136(int int_64, int int_65, int int_66)
        {
            return method_137(int_64, int_65, int_66, bool_28: true);
        }

        private HabitatList method_137(int int_64, int int_65, int int_66, bool bool_28)
        {
            HabitatList habitatList = new HabitatList();
            Habitat habitat = _Game.Galaxy.FastFindNearestSystem(int_64, int_65);
            double num = _Game.Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, int_64, int_65);
            if ((int)num <= int_66)
            {
                SystemVisibilityStatus systemVisibilityStatus = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                if (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored)
                {
                    SystemInfo systemInfo = _Game.Galaxy.Systems[habitat.SystemIndex];
                    if (systemInfo != null)
                    {
                        for (int i = 0; i < systemInfo.Habitats.Count; i++)
                        {
                            Habitat habitat2 = systemInfo.Habitats[i];
                            if (habitat2.Owner != null && habitat2.Owner != _Game.Galaxy.IndependentEmpire && habitat2.Population.TotalAmount > 0L)
                            {
                                habitatList.Add(habitat2);
                            }
                            else if (bool_28 && habitat2.Owner == _Game.Galaxy.IndependentEmpire && habitat2.Population.TotalAmount > 0L)
                            {
                                habitatList.Add(habitat2);
                            }
                        }
                    }
                }
            }
            return habitatList;
        }

        private HabitatList method_138(int int_64, int int_65, int int_66, Empire empire_5)
        {
            HabitatList habitatList = new HabitatList();
            Habitat habitat = _Game.Galaxy.FastFindNearestSystem(int_64, int_65);
            double num = _Game.Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, int_64, int_65);
            if ((int)num <= int_66)
            {
                SystemInfo systemInfo = _Game.Galaxy.Systems[habitat.SystemIndex];
                if (systemInfo != null)
                {
                    for (int i = 0; i < systemInfo.Habitats.Count; i++)
                    {
                        Habitat habitat2 = systemInfo.Habitats[i];
                        if (habitat2 != null && habitat2.Population != null && habitat2.Population.Count > 0 && (empire_5 == null || habitat2.Empire != empire_5))
                        {
                            habitatList.Add(habitat2);
                        }
                    }
                }
            }
            return habitatList;
        }

        private HabitatList method_139(BuiltObject builtObject_8, int int_64, int int_65, int int_66)
        {
            HabitatList habitatList = new HabitatList();
            Habitat habitat = _Game.Galaxy.FastFindNearestSystem(int_64, int_65);
            double num = _Game.Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, int_64, int_65);
            if ((int)num <= int_66)
            {
                SystemInfo systemInfo = _Game.Galaxy.Systems[habitat.SystemIndex];
                if (systemInfo != null)
                {
                    for (int i = 0; i < systemInfo.Habitats.Count; i++)
                    {
                        Habitat habitat2 = systemInfo.Habitats[i];
                        if (habitat2.Owner == null || habitat2.Owner == _Game.Galaxy.IndependentEmpire)
                        {
                            int newPopulationAmount = 0;
                            if (_Game.PlayerEmpire.CanBuiltObjectColonizeHabitat(builtObject_8, habitat2, out newPopulationAmount) && _Game.PlayerEmpire.CanEmpireColonizeHabitatRange(_Game.PlayerEmpire, habitat2))
                            {
                                habitatList.Add(habitat2);
                            }
                        }
                    }
                }
            }
            return habitatList;
        }

        private bool method_140(BuiltObject builtObject_8)
        {
            if (builtObject_8.Empire != _Game.PlayerEmpire)
            {
                return false;
            }
            if (builtObject_8.Role == BuiltObjectRole.Base)
            {
                return false;
            }
            if (builtObject_8.Owner == null)
            {
                return false;
            }
            if (builtObject_8.UnbuiltComponentCount > 0)
            {
                return false;
            }
            return true;
        }

        private BuiltObjectList method_141(int int_64, int int_65, int int_66, int int_67)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            int num = Math.Min(int_64, int_66);
            int num2 = Math.Min(int_65, int_67);
            int num3 = Math.Abs(int_64 - int_66);
            int num4 = Math.Abs(int_65 - int_67);
            BuiltObjectList builtObjectsInRectangle = _Game.Galaxy.GetBuiltObjectsInRectangle(num, num2, num3, num4);
            for (int i = 0; i < builtObjectsInRectangle.Count; i++)
            {
                if (builtObjectsInRectangle[i].Xpos >= (double)num && builtObjectsInRectangle[i].Xpos <= (double)(num + num3) && builtObjectsInRectangle[i].Ypos >= (double)num2 && builtObjectsInRectangle[i].Ypos <= (double)(num2 + num4))
                {
                    builtObjectList.Add(builtObjectsInRectangle[i]);
                }
            }
            return builtObjectList;
        }

        private object method_142(int int_64, int int_65)
        {
            BuiltObjectList builtObjectList_ = null;
            return method_145(int_64, int_65, bool_28: true, out builtObjectList_);
        }

        public object method_143(int int_64, int int_65, bool bool_28)
        {
            BuiltObjectList builtObjectList_ = null;
            return method_145(int_64, int_65, bool_28, out builtObjectList_);
        }

        private object method_144(int int_64, int int_65, out BuiltObjectList builtObjectList_1)
        {
            return method_145(int_64, int_65, bool_28: false, out builtObjectList_1);
        }

        private object method_145(int int_64, int int_65, bool bool_28, out BuiltObjectList builtObjectList_1)
        {
            builtObjectList_1 = null;
            if (double_0 > 100.0)
            {
                if (_Game.PlayerEmpire != null)
                {
                    int num = 10;
                    if (double_0 < 400.0)
                    {
                        num = (int)((double)num * Math.Sqrt(400.0 / double_0));
                    }
                    if (double_0 > 4000.0)
                    {
                        num = (int)((double)num / (double_0 / 4000.0));
                    }
                    if (num < 6)
                    {
                        num = 6;
                    }
                    int num2 = 10;
                    if (double_0 < 400.0)
                    {
                        num2 = (int)((double)num2 * Math.Sqrt(400.0 / double_0));
                    }
                    if (double_0 > 4000.0)
                    {
                        num2 = (int)((double)num2 / (double_0 / 4000.0));
                    }
                    if (num2 < 6)
                    {
                        num2 = 6;
                    }
                    double num3 = (double)num * double_0;
                    double num4 = (double)num2 * double_0;
                    int num5 = (int)((double)mainView.ClientRectangle.Width * double_0);
                    num5 += Galaxy.MaxSolarSystemSize * 2;
                    List<BuiltObject[]> builtObjectsAtLocationByArrays = _Game.Galaxy.GetBuiltObjectsAtLocationByArrays(int_64, int_65, num5);
                    double num6 = double.MaxValue;
                    ShipGroup shipGroup = null;
                    for (int i = 0; i < builtObjectsAtLocationByArrays.Count; i++)
                    {
                        int num7 = builtObjectsAtLocationByArrays[i].Length;
                        for (int j = 0; j < num7; j++)
                        {
                            BuiltObject builtObject = builtObjectsAtLocationByArrays[i][j];
                            if (builtObject != null && builtObject.ShipGroup != null && builtObject.ShipGroup.LeadShip == builtObject && (_Game.GodMode || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject)))
                            {
                                double num8 = _Game.Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, int_64, int_65);
                                if (num8 < num6)
                                {
                                    num6 = num8;
                                    shipGroup = builtObject.ShipGroup;
                                }
                            }
                        }
                    }
                    if (shipGroup != null && shipGroup.LeadShip != null && num6 <= num3 / 1.4)
                    {
                        if (builtObjectList_1 == null)
                        {
                            builtObjectList_1 = new BuiltObjectList();
                        }
                        if (!builtObjectList_1.Contains(shipGroup.LeadShip))
                        {
                            builtObjectList_1.Add(shipGroup.LeadShip);
                        }
                        return shipGroup;
                    }
                    DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
                    if (_Game.PlayerEmpire.DiplomaticRelations != null)
                    {
                        for (int k = 0; k < _Game.PlayerEmpire.DiplomaticRelations.Count; k++)
                        {
                            DiplomaticRelation diplomaticRelation = _Game.PlayerEmpire.DiplomaticRelations[k];
                            if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.War)
                            {
                                empireList.Add(diplomaticRelation.OtherEmpire);
                            }
                        }
                    }
                    if (double_0 <= 10000.0 || bool_28)
                    {
                        num6 = double.MaxValue;
                        BuiltObject result = null;
                        for (int l = 0; l < builtObjectsAtLocationByArrays.Count; l++)
                        {
                            int num9 = builtObjectsAtLocationByArrays[l].Length;
                            for (int m = 0; m < num9; m++)
                            {
                                BuiltObject builtObject2 = builtObjectsAtLocationByArrays[l][m];
                                if (builtObject2 == null || (!_Game.GodMode && !_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject2)))
                                {
                                    continue;
                                }
                                bool flag = true;
                                if (builtObject2.SubRole != BuiltObjectSubRole.Outpost && builtObject2.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject2.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject2.SubRole != BuiltObjectSubRole.LargeSpacePort && _Game.Galaxy.FastTestShipInColonizedSystem(builtObject2) && _Game.Galaxy.PirateEmpires != null && !_Game.Galaxy.PirateEmpires.Contains(builtObject2.Empire) && !empireList.Contains(builtObject2.Empire))
                                {
                                    flag = false;
                                }
                                if (!flag)
                                {
                                    continue;
                                }
                                double num10 = _Game.Galaxy.CalculateDistance(builtObject2.Xpos, builtObject2.Ypos, int_64, int_65);
                                if (num10 < num6)
                                {
                                    num6 = num10;
                                    result = builtObject2;
                                }
                                if (num10 <= num4 / 1.4)
                                {
                                    if (builtObjectList_1 == null)
                                    {
                                        builtObjectList_1 = new BuiltObjectList();
                                    }
                                    if (!builtObjectList_1.Contains(builtObject2))
                                    {
                                        builtObjectList_1.Add(builtObject2);
                                    }
                                }
                            }
                        }
                        if (num6 <= num4 / 1.4)
                        {
                            return result;
                        }
                    }
                }
                Habitat habitat = _Game.Galaxy.FastFindNearestSystem(int_64, int_65);
                if (habitat != null)
                {
                    for (int n = 0; n < _Game.Galaxy.Systems.Count; n++)
                    {
                        SystemInfo systemInfo = _Game.Galaxy.Systems[n];
                        if (systemInfo != null && systemInfo.SystemStar != null && systemInfo.SystemStar == habitat)
                        {
                            int num11 = 0;
                            int val = 0;
                            EmpireSystemSummary dominantEmpire = systemInfo.DominantEmpire;
                            if (dominantEmpire != null)
                            {
                                num11 = Math.Min(dominantEmpire.TotalStrategicValue, 1500000);
                                val = (int)(Math.Pow(num11, 0.35) * 600.0);
                            }
                            val = Math.Max(Galaxy.MaxSolarSystemSize, val);
                            if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                            {
                                val = (int)systemInfo.SystemStar.NovaProgression;
                            }
                            double num12 = _Game.Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, int_64, int_65);
                            if ((int)num12 <= val + (int)(5.0 * double_0))
                            {
                                return systemInfo;
                            }
                        }
                    }
                }
            }
            else
            {
                double num13 = 0.0;
                Creature creature = null;
                int num14 = 536870911;
                Habitat habitat2 = _Game.Galaxy.FindNearestSystemGasCloudAsteroid(int_64, int_65);
                if (habitat2 != null)
                {
                    double num15 = _Game.Galaxy.CalculateDistance(habitat2.Xpos, habitat2.Ypos, int_64, int_65);
                    CreatureList creatureList = new CreatureList();
                    if (num15 <= (double)(Galaxy.MaxSolarSystemSize + 5000))
                    {
                        creatureList.AddRange(ListHelper.ToArrayThreadSafe(_Game.Galaxy.Systems[habitat2.SystemIndex].Creatures));
                    }
                    else
                    {
                        GalaxyLocationList galaxyLocationList = _Game.Galaxy.DetermineGalaxyLocationsAtPoint(int_64, int_65, GalaxyLocationType.RestrictedArea);
                        for (int num16 = 0; num16 < galaxyLocationList.Count; num16++)
                        {
                            GalaxyLocation galaxyLocation = galaxyLocationList[num16];
                            if (galaxyLocation != null && galaxyLocation.RelatedCreatures != null)
                            {
                                creatureList.AddRange(ListHelper.ToArrayThreadSafe(galaxyLocation.RelatedCreatures));
                            }
                        }
                    }
                    if (creatureList.Count > 0)
                    {
                        double maxWidth = 0.0;
                        double num17 = CalculateCreatureZoomFactor(double_0, out maxWidth);
                        num13 = double_0 / num17;
                        for (int num18 = 0; num18 < creatureList.Count; num18++)
                        {
                            Creature creature2 = creatureList[num18];
                            if (creature2 != null)
                            {
                                double num19 = (double)sveqhmNacy[creature2.PictureRef] / Galaxy.CreatureDrawResizeFactor;
                                double d = (double)creature2.Size / num19;
                                d = Math.Sqrt(d);
                                int num20 = (int)((double)bitmap_10[creature2.PictureRef][0].Width * d * num13);
                                int num21 = (int)((double)bitmap_10[creature2.PictureRef][0].Height * d * num13);
                                int num22 = (int)creature2.Xpos;
                                int num23 = (int)creature2.Ypos;
                                int num24 = (int)(double_0 * 1.3);
                                int num25 = num22 - num20 / 2 - num24;
                                int num26 = num22 + num20 / 2 + num24;
                                int num27 = num23 - num21 / 2 - num24;
                                int num28 = num23 + num21 / 2 + num24;
                                if (int_64 >= num25 && int_64 <= num26 && int_65 >= num27 && int_65 <= num28 && (_Game.GodMode || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(creature2)) && creature2.Size < num14)
                                {
                                    creature = creature2;
                                    num14 = creature2.Size;
                                }
                            }
                        }
                    }
                }
                if (creature != null)
                {
                    return creature;
                }
                int num29 = (int)((double)mainView.ClientRectangle.Width * double_0);
                num29 += Galaxy.MaxSolarSystemSize * 2;
                List<BuiltObject[]> builtObjectsAtLocationByArrays2 = _Game.Galaxy.GetBuiltObjectsAtLocationByArrays(int_64, int_65, num29);
                double maxWidth2 = 0.0;
                double num30 = CalculateShipZoomFactor(double_0, out maxWidth2);
                num13 = double_0 / num30;
                BuiltObject builtObject3 = null;
                int num31 = 536870911;
                for (int num32 = 0; num32 < builtObjectsAtLocationByArrays2.Count; num32++)
                {
                    int num33 = builtObjectsAtLocationByArrays2[num32].Length;
                    for (int num34 = 0; num34 < num33; num34++)
                    {
                        BuiltObject builtObject4 = builtObjectsAtLocationByArrays2[num32][num34];
                        if (builtObject4 == null)
                        {
                            continue;
                        }
                        if (double_0 < 50.0 && builtObject4.Fighters != null && builtObject4.Fighters.Count > 0)
                        {
                            for (int num35 = 0; num35 < builtObject4.Fighters.Count; num35++)
                            {
                                Fighter fighter = builtObject4.Fighters[num35];
                                if (fighter != null && !fighter.OnboardCarrier)
                                {
                                    double num36 = (double)int_5[fighter.PictureRef] / Galaxy.BuiltObjectDrawResizeFactor;
                                    double d2 = (double)fighter.Size / num36;
                                    d2 = Math.Sqrt(d2);
                                    int num37 = (int)((double)bitmap_6[fighter.PictureRef].Width * d2 * num13);
                                    int num38 = (int)((double)bitmap_6[fighter.PictureRef].Height * d2 * num13);
                                    int num39 = (int)fighter.Xpos;
                                    int num40 = (int)fighter.Ypos;
                                    int num41 = (int)(double_0 * 1.3);
                                    int num42 = num39 - num37 / 2 - num41;
                                    int num43 = num39 + num37 / 2 + num41;
                                    int num44 = num40 - num38 / 2 - num41;
                                    int num45 = num40 + num38 / 2 + num41;
                                    if (int_64 >= num42 && int_64 <= num43 && int_65 >= num44 && int_65 <= num45 && (_Game.GodMode || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(fighter)))
                                    {
                                        return fighter;
                                    }
                                }
                            }
                        }
                        BuiltObjectImageData builtObjectImageData = builtObjectImageCache_0.ObtainImageDataSmall(builtObject4);
                        if (builtObjectImageData == null || builtObjectImageData.Image == null)
                        {
                            continue;
                        }
                        double num46 = (double)builtObjectImageData.Size / Galaxy.BuiltObjectDrawResizeFactor;
                        double d3 = (double)builtObject4.Size / num46;
                        d3 = Math.Sqrt(d3);
                        int num47 = (int)((double)builtObjectImageData.Image.Width * d3 * num13);
                        int num48 = (int)((double)builtObjectImageData.Image.Height * d3 * num13);
                        if (builtObject4.Design != null && builtObject4.Design.ImageScalingType != 0)
                        {
                            switch (builtObject4.Design.ImageScalingType)
                            {
                                case DesignImageScalingMode.Absolute:
                                    num47 = (int)(builtObject4.Design.ImageScalingFactor * (float)num13);
                                    num48 = num47;
                                    break;
                                case DesignImageScalingMode.Scaled:
                                    num47 = (int)((float)num47 * builtObject4.Design.ImageScalingFactor);
                                    num48 = (int)((float)num48 * builtObject4.Design.ImageScalingFactor);
                                    break;
                            }
                        }
                        int num49 = (int)builtObject4.Xpos;
                        int num50 = (int)builtObject4.Ypos;
                        int num51 = (int)(double_0 * 1.3);
                        int num52 = num49 - num47 / 2 - num51;
                        int num53 = num49 + num47 / 2 + num51;
                        int num54 = num50 - num48 / 2 - num51;
                        int num55 = num50 + num48 / 2 + num51;
                        if (int_64 >= num52 && int_64 <= num53 && int_65 >= num54 && int_65 <= num55 && (_Game.GodMode || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject4)))
                        {
                            if (builtObjectList_1 == null)
                            {
                                builtObjectList_1 = new BuiltObjectList();
                            }
                            if (!builtObjectList_1.Contains(builtObject4))
                            {
                                builtObjectList_1.Add(builtObject4);
                            }
                            if (builtObject4.Size < num31)
                            {
                                builtObject3 = builtObject4;
                                num31 = builtObject4.Size;
                            }
                        }
                    }
                }
                if (builtObject3 != null)
                {
                    return builtObject3;
                }
                Habitat habitat3 = null;
                if (habitat2 != null)
                {
                    SystemInfo systemInfo2 = _Game.Galaxy.Systems[habitat2.SystemIndex];
                    if (systemInfo2 != null)
                    {
                        habitat3 = _Game.Galaxy.FindNearestHabitatInSystem(systemInfo2, int_64, int_65);
                    }
                }
                if (habitat3 != null)
                {
                    int diameter = habitat3.Diameter;
                    int diameter2 = habitat3.Diameter;
                    int num56 = (int)habitat3.Xpos;
                    int num57 = (int)habitat3.Ypos;
                    int num58 = num56 - diameter / 2;
                    int num59 = num56 + diameter / 2;
                    int num60 = num57 - diameter2 / 2;
                    int num61 = num57 + diameter2 / 2;
                    if (int_64 >= num58 && int_64 <= num59 && int_65 >= num60 && int_65 <= num61)
                    {
                        return habitat3;
                    }
                }
            }
            return null;
        }

        private Habitat method_146(double double_7, double double_8)
        {
            if (double_0 <= 100.0)
            {
                Habitat habitat = _Game.Galaxy.FindNearestSystemGasCloudAsteroid(double_7, double_8);
                Habitat habitat2 = null;
                if (habitat != null)
                {
                    SystemInfo systemInfo = _Game.Galaxy.Systems[habitat.SystemIndex];
                    if (systemInfo != null)
                    {
                        habitat2 = _Game.Galaxy.FindNearestHabitatInSystem(systemInfo, double_7, double_8);
                    }
                }
                if (habitat2 != null)
                {
                    int diameter = habitat2.Diameter;
                    int diameter2 = habitat2.Diameter;
                    int num = (int)habitat2.Xpos;
                    int num2 = (int)habitat2.Ypos;
                    int num3 = num - diameter / 2;
                    int num4 = num + diameter / 2;
                    int num5 = num2 - diameter2 / 2;
                    int num6 = num2 + diameter2 / 2;
                    if (double_7 >= (double)num3 && double_7 <= (double)num4 && double_8 >= (double)num5 && double_8 <= (double)num6)
                    {
                        return habitat2;
                    }
                }
            }
            return null;
        }

        private void method_147(object object_7)
        {
            if (object_7 is Habitat)
            {
                Habitat habitat = (Habitat)object_7;
                GalaxyHabitatResourceListView.BindData(habitat.Resources, _uiResourcesBitmaps);
            }
            else
            {
                GalaxyHabitatResourceListView.BindData(null, _uiResourcesBitmaps);
            }
        }

        private Bitmap method_148(Image image_0, int int_64, int int_65)
        {
            int num = image_0.Height * int_64 / image_0.Width;
            int num2 = image_0.Width * int_65 / image_0.Height;
            Size newSize = ((num < int_65) ? new Size(int_64, num) : new Size(num2, int_65));
            Bitmap bitmap = new Bitmap(image_0, newSize);
            bitmap.SetResolution(72f, 72f);
            List<PixelFormat> list = new List<PixelFormat>();
            list.Add(PixelFormat.Format1bppIndexed);
            list.Add(PixelFormat.Format4bppIndexed);
            list.Add(PixelFormat.Format8bppIndexed);
            list.Add(PixelFormat.Undefined);
            list.Add(PixelFormat.Undefined);
            list.Add(PixelFormat.Format16bppArgb1555);
            list.Add(PixelFormat.Format16bppGrayScale);
            if (list.Contains(bitmap.PixelFormat))
            {
                return bitmap;
            }
            Graphics graphics;
            try
            {
                graphics = Graphics.FromImage(bitmap);
            }
            catch
            {
                return bitmap;
            }
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.DrawImage(bitmap, 0, 0);
            graphics.Dispose();
            return bitmap;
        }

        internal Habitat method_149()
        {
            habitat_6 = _Game.Galaxy.FastFindNearestSystem(int_13, int_14);
            int num = int_28;
            if (habitat_6 != null)
            {
                num = habitat_6.HabitatIndex;
                int num2 = num;
                do
                {
                    num2++;
                }
                while (num2 < _Game.Galaxy.Habitats.Count && _Game.Galaxy.Habitats[num2].Parent != null);
                if (num != int_28)
                {
                    int_28 = num;
                    int_29 = num2 - 1;
                    bool_19 = true;
                    bool_20 = true;
                }
                if (_Game.Galaxy.CalculateDistance(habitat_6.Xpos, habitat_6.Ypos, int_13, int_14) <= (double)(Galaxy.MaxSolarSystemSize + 500))
                {
                    SystemVisibilityStatus systemVisibilityStatus = _Game.Galaxy.PlayerEmpire.CheckSystemVisibilityStatus(habitat_6.SystemIndex);
                    if (systemVisibilityStatus != SystemVisibilityStatus.Explored && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                    {
                        if (habitat_6.Category == HabitatCategoryType.GasCloud)
                        {
                            string_22 = "(" + TextResolver.GetText("Unexplored Gas Cloud") + ")";
                        }
                        else
                        {
                            string_22 = "(" + TextResolver.GetText("Unexplored System") + ")";
                        }
                    }
                    else
                    {
                        string_22 = habitat_6.Name;
                        switch (habitat_6.Type)
                        {
                            case HabitatType.MainSequence:
                            case HabitatType.RedGiant:
                            case HabitatType.SuperGiant:
                            case HabitatType.WhiteDwarf:
                            case HabitatType.Neutron:
                                string_22 += " system";
                                break;
                            case HabitatType.BlackHole:
                                string_22 += " Black Hole";
                                break;
                            case HabitatType.SuperNova:
                                string_22 += " Nova";
                                break;
                            case HabitatType.Hydrogen:
                            case HabitatType.Helium:
                            case HabitatType.Argon:
                            case HabitatType.Ammonia:
                            case HabitatType.CarbonDioxide:
                            case HabitatType.Oxygen:
                            case HabitatType.NitrogenOxygen:
                            case HabitatType.Chlorine:
                                string_22 += " Gas Cloud";
                                break;
                        }
                    }
                }
                else
                {
                    string value = string.Empty;
                    GalaxyLocationList galaxyLocationList = _Game.Galaxy.DetermineGalaxyLocationsAtPoint(int_13, int_14);
                    for (int i = 0; i < galaxyLocationList.Count; i++)
                    {
                        GalaxyLocation galaxyLocation = galaxyLocationList[i];
                        if (galaxyLocation.Type == GalaxyLocationType.SuperNova && string.IsNullOrEmpty(value) && _Game.Galaxy.PlayerEmpire.KnownGalaxyLocations.Contains(galaxyLocation))
                        {
                            value = galaxyLocation.Name;
                        }
                        else if (galaxyLocation.Type == GalaxyLocationType.RestrictedArea && _Game.Galaxy.PlayerEmpire.KnownGalaxyLocations.Contains(galaxyLocation))
                        {
                            value = galaxyLocation.Name;
                        }
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        string_22 = value;
                    }
                    else
                    {
                        string_22 = "(" + TextResolver.GetText("Deep Space") + ")";
                    }
                }
                return habitat_6;
            }
            int_28 = -1;
            int_29 = -1;
            bool_19 = true;
            string_22 = "(" + TextResolver.GetText("Deep Space") + ")";
            return null;
        }

        private void method_150(ref int int_64, ref int int_65, double double_7)
        {
            int_64 = (int)((double)(int_64 - mainView.Width / 2) * double_7) + int_13;
            int_65 = (int)((double)(int_65 - mainView.Height / 2) * double_7) + int_14;
        }

        private void method_151(ref int int_64, ref int int_65)
        {
            int_64 = (int)((double)(int_64 - mainView.Width / 2) * double_0) + int_13;
            int_65 = (int)((double)(int_65 - mainView.Height / 2) * double_0) + int_14;
        }

        private void picSystem_MouseUp(object sender, MouseEventArgs e)
        {
            if (double_0 < 100.0)
            {
                if (int_28 >= 0)
                {
                    double num = 0.0;
                    if (double_0 < 10.0)
                    {
                        num = int_35;
                    }
                    else
                    {
                        int num2 = 100 - int_35;
                        num = double_0 * 10.0 - (double)num2;
                    }
                    int_13 += (e.X - picSystem.Width / 2) * (int)num;
                    int_14 += (e.Y - picSystem.Height / 2) * (int)num;
                    mainView.ClearMain();
                }
            }
            else
            {
                double num3 = 0.0;
                num3 = ((!(double_0 < 3500.0)) ? (double_0 * 10.0) : 35000.0);
                int_13 += (int)((double)(e.X - picSystem.Width / 2) * num3);
                int_14 += (int)((double)(e.Y - picSystem.Height / 2) * num3);
                mainView.ClearMain();
            }
            if (UhvLmNjli7 && _Game.SelectedObject != null)
            {
                method_203();
            }
            else
            {
                string_20 = string.Empty;
            }
            dateTime_1 = _Game.Galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, 60));
            dateTime_4 = DateTime.Now.Subtract(new TimeSpan(0, 0, 60));
        }

        private void method_152(Habitat habitat_9)
        {
            picSystemMap.Update();
            switch (habitat_9.Category)
            {
                case HabitatCategoryType.Moon:
                    habitat_7 = habitat_9.Parent.Parent;
                    habitat_8 = habitat_9;
                    break;
                case HabitatCategoryType.Planet:
                case HabitatCategoryType.Asteroid:
                    habitat_7 = habitat_9.Parent;
                    habitat_8 = habitat_9;
                    break;
                case HabitatCategoryType.Star:
                case HabitatCategoryType.GasCloud:
                    habitat_7 = habitat_9;
                    habitat_8 = habitat_9;
                    break;
            }
            int num = _Game.Galaxy.Habitats.IndexOf(habitat_7);
            int num2 = _Game.Galaxy.Habitats.Count - 1;
            for (int i = num + 1; i < _Game.Galaxy.Habitats.Count; i++)
            {
                if (_Game.Galaxy.Habitats[i].Parent == null)
                {
                    num2 = i - 1;
                    break;
                }
            }
            for (int j = num; j < num2; j++)
            {
                _Game.Galaxy.Habitats[j].DoTasks(_Game.Galaxy.CurrentDateTime);
            }
            int scaleFactor = Galaxy.MaxSolarSystemSize * 2 / picSystemMap.ClientRectangle.Width;
            picSystemMap.Clear(graphics_2);
            picSystemMap.SetSelectedHabitat(habitat_9);
            picSystemMap.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: false, num, scaleFactor, drawViewIndicator: false, erasePrevious: false, clearFirst: false, showIndicatorLines: true, habitat_7.Name);
            Bitmap backgroundPicture = mainView.method_54(habitat_8, pnlHabitatInfo.ClientSize.Width);
            pnlHabitatInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat_8);
            method_147(habitat_8);
            gmapMain.SetPosition(habitat_7.Xpos, habitat_7.Ypos);
            gmapMain.Invalidate();
            habitatList_2 = null;
            picSystemMap.SetSelectedHabitats(habitatList_2);
            if (habitat_8 != null)
            {
                if (habitat_8.LandscapePictureRef >= 0)
                {
                    pnlGalaxyMapHabitatPicture.BackgroundImage = bitmap_29[habitat_8.LandscapePictureRef];
                }
                else
                {
                    pnlGalaxyMapHabitatPicture.BackgroundImage = null;
                }
            }
            else
            {
                pnlGalaxyMapHabitatPicture.BackgroundImage = null;
            }
            gmapMain.SetSystems(habitatList_1);
        }

        private void gmapMain_MouseUp(object sender, MouseEventArgs e)
        {
            double num = (double)Galaxy.SizeX / (double)gmapMain.ClientRectangle.Width;
            _ = (double)gmapMain.Width / (double)Galaxy.IndexMaxX;
            int num2 = (int)((double)e.X * num);
            int num3 = (int)((double)e.Y * num);
            Habitat habitat = _Game.Galaxy.FindNearestSystemGasCloudAsteroid(num2, num3);
            if (habitat == null)
            {
                return;
            }
            int num4 = _Game.Galaxy.Habitats.IndexOf(habitat);
            int num5 = _Game.Galaxy.Habitats.Count - 1;
            for (int i = num4 + 1; i < _Game.Galaxy.Habitats.Count; i++)
            {
                if (_Game.Galaxy.Habitats[i].Parent == null)
                {
                    num5 = i - 1;
                    break;
                }
            }
            for (int j = num4; j < num5; j++)
            {
                _Game.Galaxy.Habitats[j].DoTasks(_Game.Galaxy.CurrentDateTime);
            }
            int scaleFactor = Galaxy.MaxSolarSystemSize * 2 / picSystemMap.ClientRectangle.Width;
            habitat_7 = habitat;
            gmapMain.SetPosition(habitat.Xpos, habitat.Ypos);
            gmapMain.Invalidate();
            if (gmapMain.SelectedSystems != null && gmapMain.SelectedSystems.Contains(habitat))
            {
                for (int k = 0; k < picSystemMap.SelectedHabitats.Count; k++)
                {
                    if (picSystemMap.SelectedHabitats[k].SystemIndex == habitat.SystemIndex)
                    {
                        habitat_8 = picSystemMap.SelectedHabitats[k];
                    }
                }
            }
            else
            {
                habitat_8 = habitat;
            }
            method_213(habitat_8);
            picSystemMap.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: false, num4, scaleFactor, drawViewIndicator: false, erasePrevious: false, clearFirst: true, showIndicatorLines: true, habitat.Name);
            Bitmap backgroundPicture = mainView.method_54(habitat_8, pnlHabitatInfo.ClientSize.Width);
            pnlHabitatInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat_8);
            method_147(habitat_8);
            if (habitat_8 != null)
            {
                if (habitat_8.LandscapePictureRef >= 0)
                {
                    pnlGalaxyMapHabitatPicture.BackgroundImage = bitmap_29[habitat_8.LandscapePictureRef];
                }
                else
                {
                    pnlGalaxyMapHabitatPicture.BackgroundImage = null;
                }
            }
            else
            {
                pnlGalaxyMapHabitatPicture.BackgroundImage = null;
            }
        }

        private void method_153(string string_30)
        {
            graphics_2.DrawString(string_30, font_2, new SolidBrush(Color.Black), new PointF(9f, 9f));
            graphics_2.DrawString(string_30, font_2, new SolidBrush(Color.White), new PointF(8f, 8f));
        }

        private void gmapMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int num = Galaxy.SizeX / gmapMain.Width;
            _ = (double)gmapMain.Width / (double)Galaxy.IndexMaxX;
            int num2 = e.X * num;
            int num3 = e.Y * num;
            Habitat habitat = _Game.Galaxy.FindNearestSystemGasCloudAsteroid(num2, num3);
            if (habitat != null)
            {
                int solIndexStart = _Game.Galaxy.Habitats.IndexOf(habitat);
                int scaleFactor = Galaxy.MaxSolarSystemSize * 2 / picSystemMap.ClientRectangle.Width;
                picSystemMap.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: false, solIndexStart, scaleFactor, drawViewIndicator: false, erasePrevious: false, clearFirst: true, showIndicatorLines: true, habitat_7.Name);
                int_13 = (int)habitat.Xpos;
                int_14 = (int)habitat.Ypos;
                method_149();
                bool_20 = true;
                method_132();
            }
        }

        private void picSystemMap_MouseUp(object sender, MouseEventArgs e)
        {
            int num = Galaxy.MaxSolarSystemSize * 2 / picSystemMap.Width;
            if (habitat_7 == null)
            {
                return;
            }
            int num2 = (e.X - picSystemMap.Width / 2) * num + (int)habitat_7.Xpos;
            int num3 = (e.Y - picSystemMap.Height / 2) * num + (int)habitat_7.Ypos;
            Habitat habitat = _Game.Galaxy.FindNearestHabitat(num2, num3);
            SystemVisibilityStatus systemVisibilityStatus = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
            if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
            {
                return;
            }
            Bitmap backgroundPicture = mainView.method_54(habitat, pnlHabitatInfo.ClientSize.Width);
            pnlHabitatInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat);
            method_147(habitat);
            picSystemMap.Clear(graphics_2);
            habitat_8 = habitat;
            int solIndexStart = _Game.Galaxy.Habitats.IndexOf(habitat_7);
            int scaleFactor = Galaxy.MaxSolarSystemSize * 2 / picSystemMap.ClientRectangle.Width;
            picSystemMap.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: false, solIndexStart, scaleFactor, drawViewIndicator: false, erasePrevious: false, clearFirst: false, showIndicatorLines: true, habitat_7.Name);
            if (habitat != null)
            {
                if (habitat.LandscapePictureRef >= 0)
                {
                    pnlGalaxyMapHabitatPicture.BackgroundImage = bitmap_29[habitat.LandscapePictureRef];
                }
                else
                {
                    pnlGalaxyMapHabitatPicture.BackgroundImage = null;
                }
            }
            else
            {
                pnlGalaxyMapHabitatPicture.BackgroundImage = null;
            }
            method_213(habitat);
        }

        private void picSystemMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int num = Galaxy.MaxSolarSystemSize * 2 / picSystemMap.Width;
            int num2 = (e.X - picSystemMap.Width / 2) * num + (int)habitat_7.Xpos;
            int num3 = (e.Y - picSystemMap.Height / 2) * num + (int)habitat_7.Ypos;
            Habitat habitat = _Game.Galaxy.FindNearestHabitat(num2, num3);
            SystemVisibilityStatus systemVisibilityStatus = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
            if (systemVisibilityStatus != SystemVisibilityStatus.Unexplored)
            {
                Bitmap backgroundPicture = mainView.method_54(habitat, pnlHabitatInfo.ClientSize.Width);
                pnlHabitatInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat);
                method_147(habitat);
                if (habitat != null)
                {
                    habitat_8 = habitat;
                    _Game.Galaxy.Habitats.IndexOf(habitat_7);
                    int_13 = (int)habitat.Xpos;
                    int_14 = (int)habitat.Ypos;
                    method_149();
                    method_132();
                    dateTime_1 = _Game.Galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, 60));
                    bool_20 = true;
                }
            }
        }

        private void lvwHabitats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwHabitats.SelectedItems.Count <= 0)
            {
                return;
            }
            int index = (int)lvwHabitats.SelectedItems[0].Tag;
            switch (_Game.Galaxy.Habitats[index].Category)
            {
                case HabitatCategoryType.Moon:
                    habitat_7 = _Game.Galaxy.Habitats[index].Parent.Parent;
                    habitat_8 = _Game.Galaxy.Habitats[index];
                    break;
                case HabitatCategoryType.Planet:
                case HabitatCategoryType.Asteroid:
                    habitat_7 = _Game.Galaxy.Habitats[index].Parent;
                    habitat_8 = _Game.Galaxy.Habitats[index];
                    break;
                case HabitatCategoryType.Star:
                case HabitatCategoryType.GasCloud:
                    habitat_7 = _Game.Galaxy.Habitats[index];
                    habitat_8 = _Game.Galaxy.Habitats[index];
                    break;
            }
            int num = _Game.Galaxy.Habitats.IndexOf(habitat_7);
            int num2 = _Game.Galaxy.Habitats.Count - 1;
            for (int i = num + 1; i < _Game.Galaxy.Habitats.Count; i++)
            {
                if (_Game.Galaxy.Habitats[i].Parent == null)
                {
                    num2 = i - 1;
                    break;
                }
            }
            for (int j = num; j < num2; j++)
            {
                _Game.Galaxy.Habitats[j].DoTasks(_Game.Galaxy.CurrentDateTime);
            }
            int scaleFactor = Galaxy.MaxSolarSystemSize * 2 / picSystemMap.ClientRectangle.Width;
            picSystemMap.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: false, num, scaleFactor, drawViewIndicator: false, erasePrevious: false, clearFirst: true, showIndicatorLines: true, habitat_7.Name);
            Bitmap backgroundPicture = mainView.method_54(habitat_8, pnlHabitatInfo.ClientSize.Width);
            pnlHabitatInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat_8);
            method_147(habitat_8);
            gmapMain.SetPosition(habitat_7.Xpos, habitat_7.Ypos);
            gmapMain.Invalidate();
        }

        private void zXdTibErym(object sender, MouseEventArgs e)
        {
            if (lvwHabitats.SelectedItems.Count > 0)
            {
                int index = (int)lvwHabitats.SelectedItems[0].Tag;
                int_13 = (int)_Game.Galaxy.Habitats[index].Xpos;
                int_14 = (int)_Game.Galaxy.Habitats[index].Ypos;
                method_149();
                method_132();
                dateTime_1 = _Game.Galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, 60));
                bool_20 = true;
            }
        }

        private void txtHabitatSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void picSystem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            method_131(null);
        }

        private void method_154()
        {
            method_127();
            method_126();
            bool_21 = true;
            btnPlayPause.Image = bitmap_46;
            _Game.Galaxy.Pause();
        }

        private void method_155()
        {
            bool_21 = false;
            btnPlayPause.Image = bitmap_45;
            BaconMain.BaconInitialize(this);
            _ExpModMain.ModInitialize(this);
            _Game.Galaxy.Resume();
        }

        private void method_156(double double_7, double double_8)
        {
            if (_Game.SelectedObject != null && UhvLmNjli7)
            {
                UhvLmNjli7 = false;
            }
            int_13 = (int)double_7;
            int_14 = (int)double_8;
            method_149();
            bool_20 = true;
        }

        private void method_157(object object_7)
        {
            if (object_7 == null)
            {
                return;
            }
            if (_Game.SelectedObject != null && UhvLmNjli7)
            {
                UhvLmNjli7 = false;
            }
            if (object_7 is Habitat)
            {
                int_13 = (int)((Habitat)object_7).Xpos;
                int_14 = (int)((Habitat)object_7).Ypos;
                method_149();
                bool_20 = true;
            }
            else if (object_7 is Character)
            {
                Character character = (Character)object_7;
                if (character.Location != null)
                {
                    int_13 = (int)character.Location.Xpos;
                    int_14 = (int)character.Location.Ypos;
                    method_149();
                    bool_20 = true;
                }
            }
            else if (object_7 is BuiltObject)
            {
                int_13 = (int)((BuiltObject)object_7).Xpos;
                int_14 = (int)((BuiltObject)object_7).Ypos;
                method_149();
                bool_20 = true;
            }
            else if (object_7 is Fighter)
            {
                int_13 = (int)((Fighter)object_7).Xpos;
                int_14 = (int)((Fighter)object_7).Ypos;
                method_149();
                bool_20 = true;
            }
            else if (object_7 is Creature)
            {
                int_13 = (int)((Creature)object_7).Xpos;
                int_14 = (int)((Creature)object_7).Ypos;
                method_149();
                bool_20 = true;
            }
            else if (object_7 is SystemInfo)
            {
                int_13 = (int)((SystemInfo)object_7).SystemStar.Xpos;
                int_14 = (int)((SystemInfo)object_7).SystemStar.Ypos;
                method_149();
                bool_20 = true;
            }
            else if (object_7 is ShipGroup)
            {
                int_13 = (int)((ShipGroup)object_7).LeadShip.Xpos;
                int_14 = (int)((ShipGroup)object_7).LeadShip.Ypos;
                method_149();
                bool_20 = true;
            }
            else if (object_7 is BuiltObjectList)
            {
                BuiltObjectList builtObjectList = (BuiltObjectList)object_7;
                if (builtObjectList.Count > 0)
                {
                    int_13 = (int)builtObjectList[0].Xpos;
                    int_14 = (int)builtObjectList[0].Ypos;
                    method_149();
                    bool_20 = true;
                }
            }
        }

        private void pnlDetailInfo_DoubleClick(object sender, EventArgs e)
        {
            if (_Game.SelectedObject is Habitat)
            {
                Habitat habitat = (Habitat)_Game.SelectedObject;
                if (_Game.PlayerEmpire.Colonies.Contains(habitat))
                {
                    method_166(habitat);
                }
                else
                {
                    method_131(habitat);
                }
            }
            else if (_Game.SelectedObject is BuiltObject)
            {
                BuiltObject builtObject_ = (BuiltObject)_Game.SelectedObject;
                int_60 = 1;
                method_177(builtObject_);
            }
            else if (_Game.SelectedObject is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)_Game.SelectedObject;
                if (shipGroup != null && shipGroup.Empire == _Game.PlayerEmpire)
                {
                    method_268(shipGroup);
                }
            }
        }

        private void method_158(Color color_43, Color color_44, Control control_1, Type type_0)
        {
            foreach (Control control in control_1.Controls)
            {
                if (control.GetType() == type_0)
                {
                    control.ForeColor = color_43;
                    control.BackColor = color_44;
                }
            }
        }

        private void method_159()
        {
            method_160("colonies");
        }

        private void method_160(string string_30)
        {
            string_30 = BaconMain.method_160_SetExpansionPlannerCombobox(this);
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            picExpansionPlannerImage.Size = new Size(265, 232);
            picExpansionPlannerImage.Location = new Point(675, 14);
            lblExpansionPlannerGalaxyMap.Font = font_3;
            lblExpansionPlannerGalaxyMap.Location = new Point(670, 265);
            gmapExpansionPlanner.Size = new Size(275, 275);
            gmapExpansionPlanner.Location = new Point(670, 282);
            gmapExpansionPlanner.ShowEmpireTerritory = true;
            gmapExpansionPlanner.Invalidate();
            gmapBuiltObject.BringToFront();
            gmapExpansionPlanner.SetPosition(0.0, 0.0);
            method_537(string_30);
            pnlExpansionPlanner.Size = new Size(970, 720);
            pnlExpansionPlanner.Location = new Point((mainView.Width - pnlExpansionPlanner.Width) / 2, (mainView.Height - pnlExpansionPlanner.Height) / 2);
            pnlExpansionPlanner.DoLayout();
            lblExpansionPlannerResources.Font = font_7;
            lblExpansionPlannerResources.Location = new Point(10, 11);
            btnExpansionPlannerSortResources.Size = new Size(200, 25);
            btnExpansionPlannerSortResources.Location = new Point(460, 5);
            ctlExpansionPlannerResources.Size = new Size(650, 180);
            ctlExpansionPlannerResources.Location = new Point(10, 32);
            ResourceList resources = _Game.PlayerEmpire.IdentifyDeficientEmpireResources(includeLuxuryResources: true, 0.001);
            ctlExpansionPlannerResources.BindData(_Game.Galaxy, resources, _uiResourcesBitmaps);
            ctlExpansionPlannerResources.Grid.Columns["Picture"].Width = 30;
            ctlExpansionPlannerResources.Grid.Columns["Name"].Width = 110;
            ctlExpansionPlannerResources.Grid.Columns["Type"].Width = 56;
            ctlExpansionPlannerResources.Grid.Columns["Price"].Width = 42;
            ctlExpansionPlannerResources.Grid.Columns["Sources"].Width = 62;
            ctlExpansionPlannerResources.Grid.Columns["Sources"].ToolTipText = TextResolver.GetText("Number of sources in your empire");
            ctlExpansionPlannerResources.Grid.Columns["Stock - Your Empire"].Width = 58;
            ctlExpansionPlannerResources.Grid.Columns["Stock - Your Empire"].ToolTipText = TextResolver.GetText("Available supply in your empire");
            ctlExpansionPlannerResources.Grid.Columns["In Transit - Your Empire"].Width = 58;
            ctlExpansionPlannerResources.Grid.Columns["In Transit - Your Empire"].ToolTipText = TextResolver.GetText("Amount in transit to fulfill demand in your empire");
            ctlExpansionPlannerResources.Grid.Columns["Unfulfilled Demand - Your Empire"].Width = 58;
            ctlExpansionPlannerResources.Grid.Columns["Unfulfilled Demand - Your Empire"].ToolTipText = TextResolver.GetText("Outstanding requests in your empire");
            ctlExpansionPlannerResources.Grid.Columns["Stock - Galaxy"].Width = 58;
            ctlExpansionPlannerResources.Grid.Columns["Stock - Galaxy"].ToolTipText = TextResolver.GetText("Available supply in the galaxy");
            ctlExpansionPlannerResources.Grid.Columns["In Transit - Galaxy"].Width = 58;
            ctlExpansionPlannerResources.Grid.Columns["In Transit - Galaxy"].ToolTipText = TextResolver.GetText("Amount in transit to fulfill demand in the galaxy");
            ctlExpansionPlannerResources.Grid.Columns["Unfulfilled Demand - Galaxy"].Width = 58;
            ctlExpansionPlannerResources.Grid.Columns["Unfulfilled Demand - Galaxy"].ToolTipText = TextResolver.GetText("Outstanding requests in the galaxy");
            ctlExpansionPlannerResources.BringToFront();
            pnlExpansionPlannerTargetGroup.Size = new Size(660, 404);
            pnlExpansionPlannerTargetGroup.Location = new Point(5, 242);
            pnlExpansionPlannerTargetGroup.SendToBack();
            lblExpansionPlannerCurrentlyShowing.Font = font_7;
            lblExpansionPlannerCurrentlyShowing.Location = new Point(8, 239);
            lblExpansionPlannerCurrentlyShowing.Visible = false;
            cmbExpansionPlannerMode.Font = font_7;
            cmbExpansionPlannerMode.Size = new Size(340, 24);
            cmbExpansionPlannerMode.Location = new Point(20, 230);
            cmbExpansionPlannerMode.BackColor = Color.FromArgb(21, 22, 26);
            cmbExpansionPlannerMode.Visible = true;
            cmbExpansionPlannerMode.BringToFront();
            LrufvZylIl.Text = TextResolver.GetText("Show low-quality colonies");
            LrufvZylIl.Location = new Point(15, 18);
            LrufvZylIl.Font = font_3;
            LrufvZylIl.BackColor = Color.Transparent;
            chkExpansionPlannerToggleAsteroids.Text = TextResolver.GetText("Show asteroids");
            chkExpansionPlannerToggleAsteroids.Location = new Point(15, 18);
            chkExpansionPlannerToggleAsteroids.Font = font_3;
            chkExpansionPlannerToggleAsteroids.BackColor = Color.Transparent;
            lblExpansionPlannerResourceFilter.Text = TextResolver.GetText("Filter by");
            method_404(lblExpansionPlannerResourceFilter, 415, new Size(45, 21));
            lblExpansionPlannerResourceFilter.BackColor = Color.Transparent;
            lblExpansionPlannerResourceFilter.BringToFront();
            cmbExpansionPlannerResourceFilter.BindData(font_3, _Game.Galaxy.ResourceSystem.Resources, _uiResourcesBitmaps, allowNullResource: true, allowCriticalResources: true);
            cmbExpansionPlannerResourceFilter.Size = new Size(180, 22);
            cmbExpansionPlannerResourceFilter.Location = new Point(465, 11);
            cmbExpansionPlannerResourceFilter.SelectedIndex = 0;

            _chkUseResourcePercentFilter.Location = new Point(310, 18);
            _chkUseResourcePercentFilter.Checked = false;
            _numResourcePercentFilter.Location = new Point(375, 18);
            _numResourcePercentFilter.Value = 0;
            _numResourcePercentFilter.Width = 30;

            ctlExpansionPlannerTargets.Size = new Size(640, 275);
            ctlExpansionPlannerTargets.Location = new Point(10, 40);
            ctlExpansionPlannerTargets.Grid.Columns["Picture"].Width = 30;
            ctlExpansionPlannerTargets.Grid.Columns["Name"].Width = 100;
            ctlExpansionPlannerTargets.Grid.Columns["Type"].Width = 98;
            ctlExpansionPlannerTargets.Grid.Columns["Size"].Width = 45;
            ctlExpansionPlannerTargets.Grid.Columns["Quality"].Width = 37;
            ctlExpansionPlannerTargets.Grid.Columns["Distance"].Width = 60;
            ctlExpansionPlannerTargets.Grid.Columns["Race"].Width = 40;
            ctlExpansionPlannerTargets.Grid.Columns["TotalPopulation"].Width = 50;
            ctlExpansionPlannerTargets.Grid.Columns["Resources"].Width = 95;
            ctlExpansionPlannerTargets.Grid.Columns["Ruins"].Width = 35;
            ctlExpansionPlannerTargets.Grid.Columns["ShipAssigned"].Width = 50;
            ctlExpansionPlannerTargets.Grid.Columns["ResourceRarity"].Width = 50;
            btnExpansionPlannerSelectTarget.Size = new Size(132, 70);
            btnExpansionPlannerSelectTarget.Location = new Point(670, 567);
            btnExpansionPlannerGotoTarget.Size = new Size(132, 70);
            btnExpansionPlannerGotoTarget.Location = new Point(813, 567);
            lblExpansionPlannerAvailableBuiltObjects.Location = new Point(20, 330);
            cmbExpansionPlannerAvailableBuiltObjects.Font = font_7;
            cmbExpansionPlannerAvailableBuiltObjects.Size = new Size(250, 30);
            cmbExpansionPlannerAvailableBuiltObjects.Location = new Point(20, 345);
            cmbExpansionPlannerAvailableBuiltObjects.BringToFront();
            btnExpansionPlannerAction.Size = new Size(170, 70);
            btnExpansionPlannerAction.Location = new Point(280, 325);
            btnExpansionPlannerAction.BringToFront();
            btnExpansionPlannerBuildColonyShip.Size = new Size(170, 70);
            btnExpansionPlannerBuildColonyShip.Location = new Point(470, 325);
            btnExpansionPlannerBuildColonyShip.BringToFront();
            method_161();
            pnlExpansionPlanner.Visible = true;
            pnlExpansionPlanner.BringToFront();
            ctlExpansionPlannerTargets.Focus();
        }

        private void method_161()
        {
            string text = method_538().ToLower(CultureInfo.InvariantCulture);
            double num = 0.0;
            Design design = _Game.PlayerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
            if (design != null)
            {
                num = design.CalculateCurrentPurchasePrice(_Game.Galaxy);
            }
            HabitatPrioritization selectedHabitatPrioritization = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
            if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.AssignedShip != null)
            {
                if (text == "colonies")
                {
                    btnExpansionPlannerBuildColonyShip.Text = "(" + TextResolver.GetText("Colony Ship already assigned") + ")";
                }
                else if (text == "resourcessupply")
                {
                    btnExpansionPlannerBuildColonyShip.Text = string.Empty;
                }
                else
                {
                    btnExpansionPlannerBuildColonyShip.Text = "(" + TextResolver.GetText("Construction Ship already assigned") + ")";
                }
                btnExpansionPlannerBuildColonyShip.Enabled = false;
            }
            else if (text == "colonies")
            {
                if (_Game.PlayerEmpire.StateMoney < num)
                {
                    btnExpansionPlannerBuildColonyShip.Text = TextResolver.GetText("Build and Send Colony Ship") + " (" + TextResolver.GetText("not enough money") + ")";
                    btnExpansionPlannerBuildColonyShip.Enabled = false;
                    return;
                }
                List<HabitatType> colonizableHabitatTypes = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpire(_Game.PlayerEmpire);
                design = _Game.PlayerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null && !_Game.PlayerEmpire.CanEmpireColonizeHabitat(_Game.PlayerEmpire, selectedHabitatPrioritization.Habitat, colonizableHabitatTypes, design))
                {
                    btnExpansionPlannerBuildColonyShip.Text = TextResolver.GetText("Cannot Build Colony Ship for this target");
                    btnExpansionPlannerBuildColonyShip.Enabled = false;
                    return;
                }
                btnExpansionPlannerBuildColonyShip.Text = TextResolver.GetText("Build and Send Colony Ship") + " (" + num.ToString("#") + " " + TextResolver.GetText("credits") + ")";
                if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null)
                {
                    btnExpansionPlannerBuildColonyShip.Enabled = true;
                }
                else
                {
                    btnExpansionPlannerBuildColonyShip.Enabled = false;
                }
            }
            else if (text == "resourcessupply")
            {
                btnExpansionPlannerBuildColonyShip.Text = string.Empty;
                btnExpansionPlannerBuildColonyShip.Enabled = false;
            }
            else
            {
                btnExpansionPlannerBuildColonyShip.Text = TextResolver.GetText("Queue nearest Construction Ship to build Mining Station here");
                if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null)
                {
                    btnExpansionPlannerBuildColonyShip.Enabled = true;
                }
                else
                {
                    btnExpansionPlannerBuildColonyShip.Enabled = false;
                }
            }
        }

        private void method_162()
        {
            pnlExpansionPlanner.SendToBack();
            pnlExpansionPlanner.Visible = false;
            method_553();
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_163()
        {
            if (itemListCollectionPanel_0 != null)
            {
                itemListCollectionPanel_0.Reset();
                Bitmap bitmap = new Bitmap(builtObjectImageCache_0.ObtainImageSmall(ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.LargeSpacePort, _Game.PlayerEmpire.DominantRace, _Game.PlayerEmpire.PirateEmpireBaseHabitat != null)));
                Bitmap bitmap2 = new Bitmap(builtObjectImageCache_0.ObtainImageSmall(ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.ExplorationShip, _Game.PlayerEmpire.DominantRace, _Game.PlayerEmpire.PirateEmpireBaseHabitat != null)));
                Bitmap bitmap3 = new Bitmap(builtObjectImageCache_0.ObtainImageSmall(ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.MiningStation, _Game.PlayerEmpire.DominantRace, _Game.PlayerEmpire.PirateEmpireBaseHabitat != null)));
                Bitmap bitmap4 = new Bitmap(builtObjectImageCache_0.ObtainImageSmall(ShipImageHelper.PlanetDestroyer));
                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                bitmap2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                bitmap3.RotateFlip(RotateFlipType.Rotate270FlipNone);
                bitmap4.RotateFlip(RotateFlipType.Rotate270FlipNone);
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Colonies"), bitmap_37, typeof(Habitat));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Characters"), raceImageCache_0.GetEmpireDominantRaceImage(_Game.PlayerEmpire), typeof(Character));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Space Ports") + " / " + TextResolver.GetText("Construction Yards"), bitmap, typeof(BuiltObject));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Mining Stations"), bitmap3, typeof(BuiltObject));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Construction Ships"), bitmap_72, typeof(BuiltObject));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Exploration Ships"), bitmap2, typeof(BuiltObject));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Enemy Targets"), bitmap_82, typeof(PrioritizedTarget));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Fleets"), bitmap_43, typeof(ShipGroup));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Military Ships"), bitmap_38, typeof(BuiltObject), new List<string[]> { new string[2]
            {
                TextResolver.GetText("Excluding Ships in Fleets"),
                TextResolver.GetText("Including Ships in Fleets")
            } });
                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                {
                    itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Potential Colonies"), bitmap_71, typeof(Habitat), new List<string[]> { new string[2]
                {
                    TextResolver.GetText("Hiding low-quality colonies"),
                    TextResolver.GetText("Showing low-quality colonies")
                } });
                }
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Pirate Missions"), bitmap_49, typeof(EmpireActivity), new List<string[]>
            {
                new string[3]
                {
                    TextResolver.GetText("Pirate Missions List Status All"),
                    TextResolver.GetText("Pirate Missions List Status Accepted"),
                    TextResolver.GetText("Pirate Missions List Status Open")
                },
                new string[4]
                {
                    TextResolver.GetText("Pirate Missions List Type All"),
                    TextResolver.GetText("Pirate Missions List Type Smuggling"),
                    TextResolver.GetText("Pirate Missions List Type Attack"),
                    TextResolver.GetText("Pirate Missions List Type Defend")
                }
            }, 3.3175f);
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Potential Mining Locations"), bitmap_81, typeof(Habitat), new List<string[]> { new string[2]
            {
                TextResolver.GetText("Excluding Asteroids"),
                TextResolver.GetText("Including Asteroids")
            } });
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Potential Research Locations"), bitmap_92, typeof(Habitat));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Potential Resort Locations"), bitmap_93, typeof(Habitat));
                itemListCollectionPanel_0.AddPanel(TextResolver.GetText("Special Locations"), bitmap4, typeof(GalaxyLocation));
                itemListCollectionPanel_0.ActivePanelArea = new Rectangle(itemListCollectionPanel_0.Area.Left + itemListCollectionPanel_0.SelectionButtonWidth, itemListCollectionPanel_0.Area.Top, itemListCollectionPanel_0.Area.Width - itemListCollectionPanel_0.SelectionButtonWidth, itemListCollectionPanel_0.Area.Height);
            }
        }

        private void cmbColonyPopulationPolicyRaceFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat != null)
            {
                ColonyPopulationPolicy selectedPolicy = cmbColonyPopulationPolicyRaceFamily.SelectedPolicy;
                if (selectedPolicy != 0 || selectedHabitat.RaceEventType != RaceEventType.XenophobiaNoAssimilate)
                {
                    selectedHabitat.ColonyPopulationPolicyRaceFamily = selectedPolicy;
                }
                ctlColonyPopulation.BindData(selectedHabitat.Population, editable: false, selectedHabitat);
            }
        }

        private void ItjTatWsXr(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat != null)
            {
                ColonyPopulationPolicy selectedPolicy = cmbColonyPopulationPolicyAllOthers.SelectedPolicy;
                if (selectedPolicy != 0 || selectedHabitat.RaceEventType != RaceEventType.XenophobiaNoAssimilate)
                {
                    selectedHabitat.ColonyPopulationPolicy = selectedPolicy;
                }
                ctlColonyPopulation.BindData(selectedHabitat.Population, editable: false, selectedHabitat);
            }
        }

        private void btnColonyPopulationApplyPolicyToAll_Click(object sender, EventArgs e)
        {
            string string_ = TextResolver.GetText("Apply Population Policy warning");
            MessageBoxEx messageBoxEx = method_372(string_, TextResolver.GetText("Apply Population Policy title"));
            if (!(messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes"))
            {
                return;
            }
            ColonyPopulationPolicy selectedPolicy = cmbColonyPopulationPolicyRaceFamily.SelectedPolicy;
            ColonyPopulationPolicy selectedPolicy2 = cmbColonyPopulationPolicyAllOthers.SelectedPolicy;
            if (_Game == null || _Game.PlayerEmpire == null || _Game.PlayerEmpire.Colonies == null)
            {
                return;
            }
            for (int i = 0; i < _Game.PlayerEmpire.Colonies.Count; i++)
            {
                Habitat habitat = _Game.PlayerEmpire.Colonies[i];
                if (habitat != null)
                {
                    habitat.ColonyPopulationPolicyRaceFamily = selectedPolicy;
                    habitat.ColonyPopulationPolicy = selectedPolicy2;
                }
            }
        }

        private void method_164(Habitat habitat_9)
        {
            pnlColonyInvasionContainer.BindData(_Game.Galaxy, habitat_9, font_6, font_7, font_2, font_0, base.ClientSize);
            int num = 555;
            int num2 = 520;
            if (pnlColonyInvasionContainer.ColonyInvasion != null)
            {
                switch (pnlColonyInvasionContainer.ColonyInvasion.PanelSize)
                {
                    case 0:
                        num = 555;
                        num2 = 520;
                        break;
                    case 1:
                        num = 725;
                        num2 = 647;
                        break;
                    case 2:
                        num = 910;
                        num2 = 786;
                        break;
                }
            }
            pnlColonyInvasion.Size = new Size(num + 25, num2 + 70);
            pnlColonyInvasion.Location = new Point(pnlInfoPanel.Right + 20, base.ClientRectangle.Height - (num2 + 70 + 10));
            if (habitat_9 != null)
            {
                pnlColonyInvasion.HeaderTitle = TextResolver.GetText("Ground Report") + ": " + habitat_9.Name;
                pnlColonyInvasion.HeaderIcon = habitatImageCache_0.ObtainImageSmall(habitat_9);
            }
            else
            {
                pnlColonyInvasion.HeaderTitle = TextResolver.GetText("Ground Report") + ": (" + TextResolver.GetText("None") + ")";
                pnlColonyInvasion.HeaderIcon = null;
            }
            pnlColonyInvasion.DoLayout();
            pnlColonyInvasionContainer.Location = new Point(5, 5);
            pnlColonyInvasionContainer.Size = new Size(num, num2);
            pnlColonyInvasionContainer.BringToFront();
            pnlColonyInvasionContainer.Visible = true;
            pnlColonyInvasion.BringToFront();
            pnlColonyInvasion.Visible = true;
            pnlColonyInvasion.Invalidate();
        }

        private void pnlColonyInvasion_CloseButtonClicked(object sender, EventArgs e)
        {
            method_165();
        }

        private void method_165()
        {
            pnlColonyInvasion.Visible = false;
            pnlColonyInvasion.SendToBack();
            pnlColonyInvasionContainer.ClearColony();
        }

        private void ctlColonyFacilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnColonyFacilityScrap.Enabled = true;
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            PlanetaryFacility selectedFacility = ctlColonyFacilities.SelectedFacility;
            if (selectedHabitat != null && selectedHabitat.Facilities != null && selectedFacility != null && selectedHabitat.Facilities.Contains(selectedFacility))
            {
                if (selectedHabitat.CheckFacilityOwnedByColonyOwner(selectedFacility))
                {
                    btnColonyFacilityScrap.Text = TextResolver.GetText("Scrap");
                    return;
                }
                btnColonyFacilityScrap.Text = TextResolver.GetText("Attack");
                if (!selectedHabitat.CheckCanInitiateAttackAgainstPirateFacilities(_Game.PlayerEmpire, selectedFacility))
                {
                    btnColonyFacilityScrap.Enabled = false;
                }
            }
            else
            {
                btnColonyFacilityScrap.Text = TextResolver.GetText("Scrap");
            }
        }

        private void method_166(Habitat habitat_9)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlColonyInfo.Size = new Size(1015, 760);
            pnlColonyInfo.Location = new Point((mainView.Width - pnlColonyInfo.Width) / 2, (mainView.Height - pnlColonyInfo.Height) / 2);
            pnlColonyInfo.DoLayout();
            lblColonyHabitats.Visible = false;
            UnlxwvByxj.Height = 315;
            UnlxwvByxj.Width = 670;
            UnlxwvByxj.Location = new Point(10, 30);
            HabitatList colonies = _Game.PlayerEmpire.Colonies;
            HabitatList habitatList = _Game.PlayerEmpire.DetermineEmpireSystems(_Game.PlayerEmpire);
            UnlxwvByxj.BindData(colonies);
            UnlxwvByxj.BringToFront();
            if (habitat_9 != null)
            {
                UnlxwvByxj.SelectHabitat(habitat_9);
            }
            lblColonyCount.Text = string.Format(TextResolver.GetText("Your empire has X colonies in Y systems"), colonies.Count.ToString(), habitatList.Count.ToString());
            lblColonyCount.Location = new Point(10, 8);
            btnColonyShowExpansionPlanner.Text = TextResolver.GetText("Show Expansion Planner");
            btnColonyShowExpansionPlanner.Size = new Size(200, 21);
            btnColonyShowExpansionPlanner.Location = new Point(740, 6);
            lnkColonyGrowth.Text = TextResolver.GetText("How can you help your colonies grow?...");
            lnkColonyGrowth.Location = new Point(485, 8);
            method_403(lnkColonyGrowth, 430, new Size(250, 21));
            UnlxwvByxj.Grid.Columns["Empire"].Width = 30;
            UnlxwvByxj.Grid.Columns["Picture"].Width = 25;
            UnlxwvByxj.Grid.Columns["Name"].Width = 105;
            UnlxwvByxj.Grid.Columns["Type"].Width = 109;
            UnlxwvByxj.Grid.Columns["System"].Width = 80;
            UnlxwvByxj.Grid.Columns["Facilities"].Width = 30;
            UnlxwvByxj.Grid.Columns["Quality"].Width = 37;
            UnlxwvByxj.Grid.Columns["DevelopmentLevel"].Width = 30;
            UnlxwvByxj.Grid.Columns["TotalPopulation"].Width = 55;
            UnlxwvByxj.Grid.Columns["Approval"].Width = 25;
            UnlxwvByxj.Grid.Columns["StrategicValue"].Width = 50;
            UnlxwvByxj.Grid.Columns["TaxRate"].Width = 40;
            UnlxwvByxj.Grid.Columns["AnnualRevenue"].Width = 55;
            lblColonyGalaxyMapTitle.Location = new Point(690, 27);
            gmapColony.BringToFront();
            gmapColony.Size = new Size(300, 300);
            gmapColony.Location = new Point(690, 45);
            btnColonySelect.Location = new Point(10, 348);
            btnColonySelect.Size = new Size(120, 39);
            btnColonyGotoHabitat.Location = new Point(135, 348);
            btnColonyGotoHabitat.Size = new Size(120, 39);
            btnColonyShowOnGalaxyMap.Location = new Point(260, 348);
            btnColonyShowOnGalaxyMap.Size = new Size(150, 39);
            btnColonyMakeCapital.Location = new Point(415, 348);
            btnColonyMakeCapital.Size = new Size(130, 39);
            btnColonyShowRuin.Location = new Point(550, 348);
            btnColonyShowRuin.Size = new Size(130, 39);
            tabColonyData.Size = new Size(670, 300);
            tabColonyData.Location = new Point(10, 390);
            tabColony_Cargo.Text = TextResolver.GetText("Cargo");
            tabColony_Resources.Text = TextResolver.GetText("Resources");
            tabColony_Population.Text = TextResolver.GetText("Population");
            tabColony_Troops.Text = TextResolver.GetText("Troops");
            tabColony_ConstructionYard.Text = TextResolver.GetText("Construction Yard");
            tabColony_DockingBay.Text = TextResolver.GetText("Docking Bay");
            tabColony_Facilities.Text = TextResolver.GetText("Facilities");
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            string text5 = string.Empty;
            string text6 = string.Empty;
            string text7 = string.Empty;
            if (habitat_9 != null)
            {
                if (habitat_9.Cargo != null && habitat_9.Cargo.Count > 0)
                {
                    text = " (" + habitat_9.Cargo.Count + ")";
                }
                if (habitat_9.Resources.Count > 0)
                {
                    text2 = " (" + habitat_9.Resources.Count + ")";
                }
                if (habitat_9.ConstructionQueue != null && habitat_9.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                {
                    text5 = " (" + habitat_9.ConstructionQueue.ConstructionYards.CountUnderConstruction + ")";
                }
                if (habitat_9.DockingBays != null && habitat_9.DockingBays.CountDocked > 0)
                {
                    text6 = " (" + habitat_9.DockingBays.CountDocked + ")";
                }
                if (habitat_9.Troops != null && habitat_9.Troops.Count > 0)
                {
                    text4 = " (" + habitat_9.Troops.Count + ")";
                }
                if (habitat_9.Population.Count > 0)
                {
                    text3 = " (" + habitat_9.Population.Count + ")";
                }
                if (habitat_9.Facilities != null && habitat_9.Facilities.Count > 0)
                {
                    text7 = " (" + habitat_9.Facilities.Count + ")";
                }
                tabColony_Cargo.Text = TextResolver.GetText("Cargo") + text;
                tabColony_Resources.Text = TextResolver.GetText("Resources") + text2;
                tabColony_Population.Text = TextResolver.GetText("Population") + text3;
                tabColony_Troops.Text = TextResolver.GetText("Troops") + text4;
                tabColony_ConstructionYard.Text = TextResolver.GetText("Construction Yard") + text5;
                tabColony_DockingBay.Text = TextResolver.GetText("Docking Bay") + text6;
                tabColony_Facilities.Text = TextResolver.GetText("Facilities") + text7;
            }
            lblColonyName.Text = TextResolver.GetText("Name");
            lblColonyName.Font = font_2;
            lblColonyName.ForeColor = color_1;
            lblColonyName.BackColor = Color.Transparent;
            lblColonyName.Location = new Point(690, 362);
            txtColonyName.Font = font_7;
            txtColonyName.BackColor = Color.FromArgb(48, 48, 64);
            txtColonyName.ForeColor = Color.FromArgb(170, 170, 170);
            txtColonyName.Size = new Size(155, 20);
            txtColonyName.Location = new Point(740, 360);
            txtColonyName.BringToFront();
            lblColonyTaxRate.Text = TextResolver.GetText("Tax");
            lblColonyTaxRate.Font = font_2;
            lblColonyTaxRate.ForeColor = color_1;
            lblColonyTaxRate.BackColor = Color.Transparent;
            lblColonyTaxRate.Location = new Point(903, 362);
            numColonyTaxRate.Font = font_3;
            numColonyTaxRate.BackColor = Color.FromArgb(48, 48, 64);
            numColonyTaxRate.ForeColor = Color.FromArgb(170, 170, 170);
            numColonyTaxRate.Size = new Size(40, 23);
            numColonyTaxRate.Location = new Point(936, 362);
            numColonyTaxRate.BringToFront();
            lblColonyTaxPercent.Text = "%";
            lblColonyTaxPercent.Font = font_2;
            lblColonyTaxPercent.ForeColor = color_1;
            lblColonyTaxPercent.BackColor = Color.Transparent;
            lblColonyTaxPercent.Location = new Point(975, 362);
            pnlColonyHabitatInfo.Size = new Size(300, 300);
            pnlColonyHabitatInfo.Location = new Point(690, 390);
            pnlColonyHabitatInfo.Font = font_3;
            pnlColonyHabitatInfo.CurveMode = CornerCurveMode.BottomRight_TopLeft;
            pnlColonyHabitatInfo.ShowExtendedInfo = true;
            pnlColonyHabitatInfo.Reset();
            ctlColonyCargo.Size = new Size(350, 275);
            ctlColonyCargo.Location = new Point(0, 0);
            ctlColonyCargo.BringToFront();
            ctlColonyCargo.Grid.Columns["Empire"].Width = 30;
            ctlColonyCargo.Grid.Columns["Picture"].Width = 50;
            ctlColonyCargo.Grid.Columns["Name"].Width = 160;
            ctlColonyCargo.Grid.Columns["Amount"].Width = 60;
            ctlColonyCargo.Grid.Columns["Reserved"].Width = 60;
            lblColonyCargoStrategicResourcesLow.Location = new Point(365, 5);
            lblColonyCargoStrategicResourcesLow.MaximumSize = new Size(290, 140);
            lblColonyCargoStrategicResourcesLow.Size = new Size(290, 140);
            lblColonyCargoStrategicResourcesLow.Font = font_6;
            lblColonyCargoStrategicResourcesLow.ForeColor = Color.Yellow;
            lblColonyCargoConstructionResourceShortage.Location = new Point(365, 150);
            lblColonyCargoConstructionResourceShortage.MaximumSize = new Size(290, 125);
            lblColonyCargoConstructionResourceShortage.Size = new Size(290, 125);
            lblColonyCargoConstructionResourceShortage.Font = font_6;
            lblColonyCargoConstructionResourceShortage.ForeColor = Color.FromArgb(255, 128, 0);
            ctlColonyResources.Size = new Size(300, 275);
            ctlColonyResources.Location = new Point(0, 0);
            ctlColonyResources.BringToFront();
            ctlColonyResources.Grid.Columns["Type"].Width = 190;
            ctlColonyResources.Grid.Columns["Picture"].Width = 50;
            ctlColonyResources.Grid.Columns["Abundance"].Width = 60;
            ctlColonyPopulation.Size = new Size(270, 185);
            ctlColonyPopulation.Location = new Point(0, 0);
            ctlColonyPopulation.BringToFront();
            ctlColonyPopulation.Grid.Columns["Picture"].Width = 40;
            ctlColonyPopulation.Grid.Columns["Name"].Width = 120;
            ctlColonyPopulation.Grid.Columns["Amount"].Width = 55;
            ctlColonyPopulation.Grid.Columns["GrowthRate"].Width = 55;
            pnlColonyPopulationAttitudeSummaryBackground.Size = new Size(385, 240);
            pnlColonyPopulationAttitudeSummaryBackground.Location = new Point(274, 5);
            pnlColonyPopulationAttitudeSummaryContainer.Size = new Size(370, 220);
            pnlColonyPopulationAttitudeSummaryContainer.Location = new Point(8, 8);
            pnlColonyPopulationAttitudeSummaryContainer.AutoScroll = true;
            pnlColonyPopulationAttitudeSummaryContainer.SetAutoScrollMargin(0, 0);
            pnlColonyPopulationAttitudeSummaryContainer.AutoScrollPosition = new Point(0, 0);
            pnlColonyPopulationAttitudeSummary.Size = new Size(350, 0);
            pnlColonyPopulationAttitudeSummary.Location = new Point(0, 0);
            pnlColonyPopulationAttitudeSummary.MaximumSize = new Size(350, 1000);
            pnlColonyPopulationAttitudeSummary.MinimumSize = new Size(350, 200);
            pnlColonyPopulationAttitudeSummary.AutoSize = true;
            pnlColonyPopulationAttitudeSummary.BindData(_Game.Galaxy, habitat_9);
            method_404(lblColonyPopulationPolicyRaceFamily, 0, new Size(185, 15));
            lblColonyPopulationPolicyRaceFamily.Text = TextResolver.GetText("Population Policy: Same Family");
            lblColonyPopulationPolicyRaceFamily.Location = new Point(0, 193);
            cmbColonyPopulationPolicyRaceFamily.BindData();
            cmbColonyPopulationPolicyRaceFamily.Size = new Size(85, 21);
            cmbColonyPopulationPolicyRaceFamily.Location = new Point(185, 188);
            method_404(lblColonyPopulationPolicyAllOthers, 0, new Size(185, 15));
            lblColonyPopulationPolicyAllOthers.Text = TextResolver.GetText("Population Policy: All Other Races");
            lblColonyPopulationPolicyAllOthers.Location = new Point(0, 220);
            cmbColonyPopulationPolicyAllOthers.BindData();
            cmbColonyPopulationPolicyAllOthers.Size = new Size(85, 21);
            cmbColonyPopulationPolicyAllOthers.Location = new Point(185, 215);
            btnColonyPopulationApplyPolicyToAll.Size = new Size(265, 25);
            btnColonyPopulationApplyPolicyToAll.Location = new Point(5, 244);
            btnColonyPopulationApplyPolicyToAll.Text = TextResolver.GetText("Apply this Policy to All Colonies");
            lnkColonyApproval.Location = new Point(505, 250);
            lnkColonyApproval.Text = TextResolver.GetText("Learn about Colony Approval...");
            method_403(lnkColonyApproval, 455, new Size(200, 21));
            ctlColonyCharacterTroops.Size = new Size(540, 275);
            ctlColonyCharacterTroops.Location = new Point(0, 0);
            ctlColonyCharacterTroops.BringToFront();
            cmbColonyTroopsRecruitType.Size = new Size(115, 21);
            cmbColonyTroopsRecruitType.Location = new Point(545, 10);
            cmbColonyTroopsRecruitType.Visible = true;
            btnColonyTroopsRecruit.Location = new Point(545, 35);
            btnColonyTroopsDisband.Location = new Point(545, 70);
            btnColonyTroopsRecruit.Size = new Size(110, 25);
            btnColonyTroopsDisband.Size = new Size(110, 25);
            btnColonyTroopGarrison.Size = new Size(110, 25);
            btnColonyTroopGarrison.Location = new Point(545, 120);
            btnColonyTroopsUngarrison.Size = new Size(110, 25);
            btnColonyTroopsUngarrison.Location = new Point(545, 150);
            cmbColonyTroopTransferTransport.Size = new Size(115, 21);
            cmbColonyTroopTransferTransport.Location = new Point(545, 200);
            cmbColonyTroopTransferTransport.Visible = true;
            btnColonyTroopTransferTransport.Size = new Size(110, 25);
            btnColonyTroopTransferTransport.Location = new Point(545, 225);
            method_167(habitat_9);
            method_427();
            ctlColonyConstructionYard.Size = new Size(390, 50);
            ctlColonyConstructionYard.Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            ctlColonyConstructionYard.Location = new Point(0, 0);
            ctlColonyConstructionYard.BringToFront();
            ctlColonyConstructionYard.Grid.Columns["ComponentPicture"].Width = 30;
            ctlColonyConstructionYard.Grid.Columns["ShipEmpire"].Width = 30;
            ctlColonyConstructionYard.Grid.Columns["ShipPicture"].Width = 40;
            ctlColonyConstructionYard.Grid.Columns["ShipName"].Width = 162;
            ctlColonyConstructionYard.Grid.Columns["Progress"].Width = 70;
            ctlColonyConstructionYard.Grid.Columns["Speed"].Width = 58;
            pnlColonyConstructionYardPurchaser.Size = new Size(262, 90);
            pnlColonyConstructionYardPurchaser.Location = new Point(395, 3);
            lblColonyMaximumSize.Location = new Point(5, 55);
            lblColonyMaximumSize.MaximumSize = new Size(190, 50);
            lblColonyMaximumSize.Text = method_305();
            btnColonyConstructionScrap.Size = new Size(200, 22);
            btnColonyConstructionScrap.Location = new Point(190, 54);
            btnColonyConstructionShowSummary.Size = new Size(200, 22);
            btnColonyConstructionShowSummary.Location = new Point(190, 77);
            lblColonyConstructionYardWaitQueue.Text = TextResolver.GetText("Ships waiting to be constructed");
            lblColonyConstructionYardWaitQueue.Location = new Point(0, 115);
            lblColonyConstructionYardWaitQueue.Font = font_2;
            lblColonyConstructionYardWaitQueue.ForeColor = color_1;
            lblColonyConstructionYardWaitQueue.BackColor = Color.Transparent;
            ctlColonyConstructionYardWaitQueue.Size = new Size(390, 140);
            ctlColonyConstructionYardWaitQueue.Location = new Point(0, 135);
            ctlColonyConstructionYardWaitQueue.BringToFront();
            ctlColonyConstructionYardWaitQueue.Grid.Columns["Empire"].Width = 30;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["Picture"].Width = 40;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["Name"].Width = 170;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["Role"].Width = 150;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["Mission"].Visible = false;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["System"].Visible = false;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["Mission"].Width = 0;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["System"].Width = 0;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["Fleet"].Visible = false;
            ctlColonyConstructionYardWaitQueue.Grid.Columns["Automated"].Visible = false;
            btnColonyConstructionYardMoveToTop.Visible = false;
            btnColonyConstructionYardMoveToBottom.Visible = false;
            btnColonyConstructionYardMoveUp.Size = new Size(110, 25);
            btnColonyConstructionYardMoveDown.Size = new Size(110, 25);
            btnColonyConstructionYardMoveUp.Location = new Point(395, 135);
            btnColonyConstructionYardMoveDown.Location = new Point(395, 165);
            btnColonyConstructionRemoveFromQueue.Size = new Size(110, 40);
            btnColonyConstructionRemoveFromQueue.Location = new Point(395, 225);
            lnkColonyConstruction.Text = TextResolver.GetText("Learn about Construction") + "...";
            lnkColonyConstruction.Location = new Point(525, 228);
            method_403(lnkColonyConstruction, 505, new Size(150, 42));
            lnkColonyConstruction.TextAlign = ContentAlignment.BottomRight;
            ctlColonyDockingBay.Size = new Size(660, 70);
            ctlColonyDockingBay.Location = new Point(0, 0);
            ctlColonyDockingBay.BringToFront();
            ctlColonyDockingBay.Grid.Columns["ComponentPicture"].Width = 40;
            ctlColonyDockingBay.Grid.Columns["ShipEmpire"].Width = 30;
            ctlColonyDockingBay.Grid.Columns["ShipPicture"].Width = 40;
            ctlColonyDockingBay.Grid.Columns["ShipName"].Width = 400;
            ctlColonyDockingBay.Grid.Columns["ShipCommand"].Width = 150;
            lblColonyDockingBayWaitQueue.Text = TextResolver.GetText("Ships waiting to dock");
            lblColonyDockingBayWaitQueue.Location = new Point(0, 75);
            lblColonyDockingBayWaitQueue.Font = font_2;
            lblColonyDockingBayWaitQueue.ForeColor = color_1;
            lblColonyDockingBayWaitQueue.BackColor = Color.Transparent;
            ctlColonyDockingBayWaitQueue.Size = new Size(660, 180);
            ctlColonyDockingBayWaitQueue.Location = new Point(0, 95);
            ctlColonyDockingBayWaitQueue.BringToFront();
            ctlColonyDockingBayWaitQueue.Grid.Columns["Empire"].Width = 30;
            ctlColonyDockingBayWaitQueue.Grid.Columns["Picture"].Width = 40;
            ctlColonyDockingBayWaitQueue.Grid.Columns["Name"].Width = 210;
            ctlColonyDockingBayWaitQueue.Grid.Columns["Role"].Width = 200;
            ctlColonyDockingBayWaitQueue.Grid.Columns["Mission"].Width = 80;
            ctlColonyDockingBayWaitQueue.Grid.Columns["System"].Width = 100;
            ctlColonyDockingBayWaitQueue.Grid.Columns["Fleet"].Visible = false;
            ctlColonyDockingBayWaitQueue.Grid.Columns["Automated"].Visible = false;
            ctlColonyFacilities.Size = new Size(450, 275);
            ctlColonyFacilities.Location = new Point(0, 0);
            ctlColonyFacilities.BringToFront();
            btnColonyFacilityBuild.Size = new Size(190, 25);
            btnColonyFacilityBuild.Location = new Point(460, 38);
            btnColonyFacilityScrap.Size = new Size(190, 25);
            btnColonyFacilityScrap.Location = new Point(460, 84);
            cmbColonyFacilitiesToBuild.Size = new Size(190, 21);
            cmbColonyFacilitiesToBuild.Location = new Point(460, 10);
            btnColonyFacilityScrap.Text = TextResolver.GetText("Scrap");
            gmapColony.Invalidate();
            UnlxwvByxj_SelectionChanged(null, null);
            pnlColonyInfo.Visible = true;
            pnlColonyInfo.BringToFront();
            UnlxwvByxj.Focus();
        }

        private void method_167(Habitat habitat_9)
        {
            if (cmbColonyTroopsRecruitType.Items != null)
            {
                cmbColonyTroopsRecruitType.Items.Clear();
            }
            if (habitat_9 != null)
            {
                TroopList troopList = habitat_9.ResolveRecruitableTroopsForColony();
                for (int i = 0; i < troopList.Count; i++)
                {
                    string name = troopList[i].Name;
                    name = name + " (" + Galaxy.ResolveDescription(troopList[i].Type) + ")";
                    cmbColonyTroopsRecruitType.Items.Add(name);
                }
                if (troopList.Count > 0)
                {
                    cmbColonyTroopsRecruitType.SelectedIndex = 0;
                }
            }
            cmbColonyTroopsRecruitType.DropDownWidth = 180;
        }

        private Troop method_168(out bool bool_28, out bool bool_29, out bool bool_30)
        {
            Troop result = null;
            bool_28 = false;
            bool_29 = false;
            bool_30 = false;
            int selectedIndex = cmbColonyTroopsRecruitType.SelectedIndex;
            if (selectedIndex >= 0 && UnlxwvByxj.SelectedHabitat != null)
            {
                int cloneIndex = -1;
                int roboticIndex = -1;
                int eliteIndex = -1;
                TroopList troopList = UnlxwvByxj.SelectedHabitat.ResolveRecruitableTroopsForColony(out cloneIndex, out roboticIndex, out eliteIndex);
                if (selectedIndex < troopList.Count)
                {
                    result = troopList[selectedIndex];
                }
                if (selectedIndex == cloneIndex)
                {
                    bool_28 = true;
                }
                else if (selectedIndex == roboticIndex)
                {
                    bool_29 = true;
                }
                else if (selectedIndex == eliteIndex)
                {
                    bool_30 = true;
                }
            }
            return result;
        }

        private void UnlxwvByxj_SelectionChanged(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            tabColony_Cargo.Text = TextResolver.GetText("Cargo");
            tabColony_Resources.Text = TextResolver.GetText("Resources");
            tabColony_Population.Text = TextResolver.GetText("Population");
            tabColony_Troops.Text = TextResolver.GetText("Troops & Characters");
            tabColony_ConstructionYard.Text = TextResolver.GetText("Construction Yard");
            tabColony_DockingBay.Text = TextResolver.GetText("Docking Bay");
            tabColony_Facilities.Text = TextResolver.GetText("Facilities");
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            string text5 = string.Empty;
            string text6 = string.Empty;
            string text7 = string.Empty;
            if (selectedHabitat != null)
            {
                if (selectedHabitat.Cargo != null && selectedHabitat.Cargo.Count > 0)
                {
                    text = " (" + selectedHabitat.Cargo.Count + ")";
                }
                if (selectedHabitat.Resources.Count > 0)
                {
                    text2 = " (" + selectedHabitat.Resources.Count + ")";
                }
                if (selectedHabitat.ConstructionQueue != null && selectedHabitat.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                {
                    text5 = " (" + selectedHabitat.ConstructionQueue.ConstructionYards.CountUnderConstruction + ")";
                }
                if (selectedHabitat.DockingBays != null && selectedHabitat.DockingBays.CountDocked > 0)
                {
                    text6 = " (" + selectedHabitat.DockingBays.CountDocked + ")";
                }
                if ((selectedHabitat.Troops != null && selectedHabitat.Troops.Count > 0) || (selectedHabitat.Characters != null && selectedHabitat.Characters.Count > 0))
                {
                    int num = 0;
                    if (selectedHabitat.Troops != null)
                    {
                        num += selectedHabitat.Troops.Count;
                    }
                    if (selectedHabitat.Characters != null)
                    {
                        num += selectedHabitat.Characters.Count;
                    }
                    text4 = " (" + num + ")";
                }
                if (selectedHabitat.Population.Count > 0)
                {
                    text3 = " (" + selectedHabitat.Population.Count + ")";
                }
                if (selectedHabitat.Facilities != null && selectedHabitat.Facilities.Count > 0)
                {
                    text7 = " (" + selectedHabitat.Facilities.Count + ")";
                }
                tabColony_Cargo.Text = TextResolver.GetText("Cargo") + text;
                tabColony_Resources.Text = TextResolver.GetText("Resources") + text2;
                tabColony_Population.Text = TextResolver.GetText("Population") + text3;
                tabColony_Troops.Text = TextResolver.GetText("Troops & Characters") + text4;
                tabColony_ConstructionYard.Text = TextResolver.GetText("Construction Yard") + text5;
                tabColony_DockingBay.Text = TextResolver.GetText("Docking Bay") + text6;
                tabColony_Facilities.Text = TextResolver.GetText("Facilities") + text7;
            }
            if (selectedHabitat != null)
            {
                Bitmap backgroundPicture = mainView.method_54(selectedHabitat, pnlColonyHabitatInfo.ClientSize.Width);
                pnlColonyHabitatInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, selectedHabitat);
            }
            else
            {
                pnlColonyHabitatInfo.ClearData();
            }
            if (selectedHabitat != null)
            {
                ctlColonyCharacterTroops.BindData(selectedHabitat.Empire, selectedHabitat.Characters, selectedHabitat.InvadingCharacters, selectedHabitat.Troops, selectedHabitat.TroopsToRecruit, selectedHabitat.InvadingTroops, characterImageCache_0);
                ctlColonyResources.BindData(selectedHabitat.Resources, _uiResourcesBitmaps);
                ctlColonyCargo.BindData(selectedHabitat.Cargo, bitmap_21, _uiResourcesBitmaps, builtObjectImageCache_0.GetImagesSmall(), _Game.Galaxy);
                if (selectedHabitat.CalculateStrategicResourceSupplyGrowthFactor() < 1.0)
                {
                    lblColonyCargoStrategicResourcesLow.Text = TextResolver.GetText("Insufficient Strategic Resources For Growth");
                }
                else
                {
                    lblColonyCargoStrategicResourcesLow.Text = string.Empty;
                }
                if (selectedHabitat.ManufacturingQueue != null && selectedHabitat.ManufacturingQueue.DeficientResources != null && selectedHabitat.ManufacturingQueue.DeficientResources.Count > 0)
                {
                    ResourceDatePair[] array = selectedHabitat.ManufacturingQueue.DeficientResources.ToArray();
                    string text8 = string.Empty;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i > 0)
                        {
                            text8 += ", ";
                        }
                        text8 += new Resource(array[i].ResourceId).Name;
                    }
                    string text9 = string.Format(TextResolver.GetText("Construction Resource Shortage Message"), selectedHabitat.Name, text8);
                    lblColonyCargoConstructionResourceShortage.Text = text9;
                }
                else
                {
                    lblColonyCargoConstructionResourceShortage.Text = string.Empty;
                }
                ctlColonyPopulation.BindData(selectedHabitat.Population, editable: false, selectedHabitat);
                pnlColonyPopulationAttitudeSummary.BindData(_Game.Galaxy, selectedHabitat);
                pnlColonyPopulationAttitudeSummaryContainer.AutoScrollPosition = new Point(0, 0);
                cmbColonyPopulationPolicyRaceFamily.SetSelectedPolicy(selectedHabitat.ColonyPopulationPolicyRaceFamily);
                cmbColonyPopulationPolicyAllOthers.SetSelectedPolicy(selectedHabitat.ColonyPopulationPolicy);
                if (selectedHabitat.RaceEventType != RaceEventType.AntiXenoRiotsExterminate && selectedHabitat.RaceEventType != RaceEventType.DeathCultExterminate)
                {
                    cmbColonyPopulationPolicyRaceFamily.Enabled = true;
                    cmbColonyPopulationPolicyAllOthers.Enabled = true;
                }
                else
                {
                    cmbColonyPopulationPolicyRaceFamily.Enabled = false;
                    cmbColonyPopulationPolicyAllOthers.Enabled = false;
                }
                if (selectedHabitat.ConstructionQueue != null)
                {
                    ctlColonyConstructionYard.BindData(_Game.Galaxy, selectedHabitat.ConstructionQueue.ConstructionYards, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                    ctlColonyConstructionYardWaitQueue.BindData(selectedHabitat.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                }
                else
                {
                    ctlColonyConstructionYard.BindData(_Game.Galaxy, null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                    ctlColonyConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
                }
                Empire empire = selectedHabitat.Empire;
                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && selectedHabitat.GetPirateControl().CheckFactionHasControl(_Game.PlayerEmpire))
                {
                    empire = _Game.PlayerEmpire;
                }
                pnlColonyConstructionYardPurchaser.BindData(empire, selectedHabitat.ConstructionQueue, selectedHabitat, _Game.Galaxy, allowPrivateConstruction: false);
                ctlColonyDockingBay.BindData(selectedHabitat.DockingBays, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                ctlColonyDockingBayWaitQueue.BindData(selectedHabitat.DockingBayWaitQueue, _Game.Galaxy);
                ctlColonyFacilities.BindData(_Game.Galaxy, selectedHabitat.Facilities, selectedHabitat);
                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = selectedHabitat.ResolveBuildableFacilities();
                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && selectedHabitat.Empire != _Game.PlayerEmpire)
                {
                    planetaryFacilityDefinitionList = selectedHabitat.ResolveBuildableFacilitiesPirates(_Game.PlayerEmpire);
                }
                planetaryFacilityDefinitionList.AddRange(selectedHabitat.ResolveBuildableWonders());
                cmbColonyFacilitiesToBuild.BindData(_Game.PlayerEmpire, planetaryFacilityDefinitionList, bitmap_8);
                txtColonyName.Text = selectedHabitat.Name;
                numColonyTaxRate.Maximum = Math.Max(100m, numColonyTaxRate.Value);
                numColonyTaxRate.Value = (decimal)(Math.Max(0f, selectedHabitat.TaxRate) * 100f);
                if (selectedHabitat.Ruin != null)
                {
                    btnColonyShowRuin.Enabled = true;
                }
                else
                {
                    btnColonyShowRuin.Enabled = false;
                }
                Habitat system = Galaxy.DetermineHabitatSystemStar(selectedHabitat);
                gmapColony.SetSystem(system);
                gmapColony.Invalidate();
            }
            else
            {
                ctlColonyCharacterTroops.BindData(null, null, null, null, null, null, characterImageCache_0);
                ctlColonyResources.BindData(null, _uiResourcesBitmaps);
                ctlColonyCargo.BindData(null, bitmap_21, _uiResourcesBitmaps, builtObjectImageCache_0.GetImagesSmall(), _Game.Galaxy);
                lblColonyCargoStrategicResourcesLow.Text = string.Empty;
                lblColonyCargoConstructionResourceShortage.Text = string.Empty;
                ctlColonyPopulation.BindData(null);
                cmbColonyPopulationPolicyRaceFamily.SetSelectedPolicy(ColonyPopulationPolicy.Assimilate);
                cmbColonyPopulationPolicyAllOthers.SetSelectedPolicy(ColonyPopulationPolicy.Assimilate);
                ctlColonyConstructionYard.BindData(_Game.Galaxy, null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                pnlColonyConstructionYardPurchaser.BindData(null, null, null, _Game.Galaxy, allowPrivateConstruction: false);
                ctlColonyConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
                ctlColonyDockingBay.BindData(null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                ctlColonyDockingBayWaitQueue.BindData(null, _Game.Galaxy);
                ctlColonyFacilities.BindData(_Game.Galaxy, null, null);
                cmbColonyFacilitiesToBuild.BindData(_Game.PlayerEmpire, null, bitmap_8);
                txtColonyName.Text = string.Empty;
                numColonyTaxRate.Value = 0m;
                btnColonyShowRuin.Enabled = false;
                gmapColony.SetSystem(null);
                gmapColony.Invalidate();
            }
            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && (selectedHabitat == null || selectedHabitat.Empire != _Game.PlayerEmpire))
            {
                pnlColonyConstructionYardPurchaser.Enabled = false;
                cmbColonyTroopsRecruitType.Enabled = false;
                btnColonyTroopsRecruit.Enabled = false;
                btnColonyTroopsDisband.Enabled = false;
                btnColonyTroopGarrison.Enabled = false;
                btnColonyTroopsUngarrison.Enabled = false;
                cmbColonyTroopTransferTransport.Enabled = false;
                btnColonyTroopTransferTransport.Enabled = false;
                cmbColonyPopulationPolicyAllOthers.Enabled = false;
                cmbColonyPopulationPolicyRaceFamily.Enabled = false;
                btnColonyPopulationApplyPolicyToAll.Enabled = false;
                btnColonyMakeCapital.Enabled = false;
                btnColonyConstructionRemoveFromQueue.Enabled = false;
                btnColonyConstructionScrap.Enabled = false;
                btnColonyConstructionShowSummary.Enabled = false;
                btnColonyConstructionYardMoveDown.Enabled = false;
                btnColonyConstructionYardMoveUp.Enabled = false;
                numColonyTaxRate.Enabled = false;
            }
            else
            {
                pnlColonyConstructionYardPurchaser.Enabled = true;
                cmbColonyTroopsRecruitType.Enabled = true;
                btnColonyTroopsRecruit.Enabled = true;
                btnColonyTroopsDisband.Enabled = true;
                btnColonyTroopGarrison.Enabled = true;
                btnColonyTroopsUngarrison.Enabled = true;
                cmbColonyTroopTransferTransport.Enabled = true;
                btnColonyTroopTransferTransport.Enabled = true;
                cmbColonyPopulationPolicyAllOthers.Enabled = true;
                cmbColonyPopulationPolicyRaceFamily.Enabled = true;
                btnColonyPopulationApplyPolicyToAll.Enabled = true;
                btnColonyMakeCapital.Enabled = true;
                btnColonyConstructionRemoveFromQueue.Enabled = true;
                btnColonyConstructionScrap.Enabled = true;
                btnColonyConstructionShowSummary.Enabled = true;
                btnColonyConstructionYardMoveDown.Enabled = true;
                btnColonyConstructionYardMoveUp.Enabled = true;
                numColonyTaxRate.Enabled = true;
            }
            method_167(selectedHabitat);
        }

        private void method_169(object object_7, bool bool_28)
        {
            ctlConstructionYards.Size = new Size(390, 150);
            ctlConstructionYards.Location = new Point(0, 0);
            ctlConstructionYards.BringToFront();
            ctlConstructionYards.Grid.Columns["ComponentPicture"].Width = 30;
            ctlConstructionYards.Grid.Columns["ShipEmpire"].Width = 30;
            ctlConstructionYards.Grid.Columns["ShipPicture"].Width = 40;
            ctlConstructionYards.Grid.Columns["ShipName"].Width = 167;
            ctlConstructionYards.Grid.Columns["Progress"].Width = 70;
            ctlConstructionYards.Grid.Columns["Speed"].Width = 53;
            lblConstructionYardWaitQueue.Text = TextResolver.GetText("Ships waiting to be constructed");
            lblConstructionYardWaitQueue.Font = font_2;
            lblConstructionYardWaitQueue.ForeColor = color_1;
            lblConstructionYardWaitQueue.BackColor = Color.Transparent;
            lblConstructionYardWaitQueue.Location = new Point(0, 160);
            ctlConstructionYardWaitQueue.Size = new Size(390, 95);
            ctlConstructionYardWaitQueue.Location = new Point(0, 180);
            ctlConstructionYardWaitQueue.BringToFront();
            pnlBuiltObjectConstructionYardPurchaser.Size = new Size(270, 90);
            pnlBuiltObjectConstructionYardPurchaser.Location = new Point(395, 3);
            btnBuiltObjectConstructionScrap.Size = new Size(230, 25);
            btnBuiltObjectConstructionScrap.Location = new Point(395, 99);
            btnBuiltObjectConstructionShowSummary.Size = new Size(230, 25);
            btnBuiltObjectConstructionShowSummary.Location = new Point(395, 126);
            btnBuiltObjectConstructionYardMoveToBottom.Visible = false;
            btnBuiltObjectConstructionYardMoveToTop.Visible = true;
            btnBuiltObjectConstructionYardMoveToTop.Size = new Size(120, 22);
            btnBuiltObjectConstructionYardMoveUp.Size = new Size(120, 22);
            awqrtdiblI.Size = new Size(120, 22);
            btnBuiltObjectConstructionYardMoveToTop.Location = new Point(395, 180);
            btnBuiltObjectConstructionYardMoveUp.Location = new Point(395, 203);
            awqrtdiblI.Location = new Point(395, 226);
            btnBuiltObjectConstructionRemoveFromQueue.Text = TextResolver.GetText("Remove Ship");
            btnBuiltObjectConstructionRemoveFromQueue.Size = new Size(120, 22);
            btnBuiltObjectConstructionRemoveFromQueue.Location = new Point(395, 249);
            btnBuiltObjectConstructionYardMoveToTop.BringToFront();
            pnlColonyConstructionYardPurchaser.Size = new Size(230, 90);
            pnlColonyConstructionYardPurchaser.Location = new Point(430, 3);
            lblBuiltObjectMaximumSize.Location = new Point(530, 180);
            lblBuiltObjectMaximumSize.MaximumSize = new Size(140, 80);
            lblBuiltObjectMaximumSize.Text = method_305();
            lnkBuiltObjectConstruction.Location = new Point(530, 250);
            ctlConstructionYardWaitQueue.Grid.Columns["Empire"].Width = 30;
            ctlConstructionYardWaitQueue.Grid.Columns["Picture"].Width = 40;
            ctlConstructionYardWaitQueue.Grid.Columns["Name"].Width = 163;
            ctlConstructionYardWaitQueue.Grid.Columns["Role"].Width = 160;
            ctlConstructionYardWaitQueue.Grid.Columns["Mission"].Width = 0;
            ctlConstructionYardWaitQueue.Grid.Columns["System"].Width = 0;
            ctlConstructionYardWaitQueue.Grid.Columns["Mission"].Visible = false;
            ctlConstructionYardWaitQueue.Grid.Columns["System"].Visible = false;
            ctlConstructionYardWaitQueue.Grid.Columns["Fleet"].Visible = false;
            ctlConstructionYardWaitQueue.Grid.Columns["Automated"].Visible = false;
            lblBuiltObjectMaximumSize.Location = new Point(515, 180);
            lblBuiltObjectMaximumSize.MaximumSize = new Size(160, 80);
            lblBuiltObjectMaximumSize.Text = method_305();
            lnkBuiltObjectConstruction.Location = new Point(515, 250);
            lnkBuiltObjectConstruction.Text = TextResolver.GetText("Learn about Construction") + "...";
            method_403(lnkBuiltObjectConstruction, 495, new Size(170, 21));
            lblConstructionYardManufacturers.Text = TextResolver.GetText("Manufacturing Plants");
            lblConstructionYardManufacturers.Font = font_2;
            lblConstructionYardManufacturers.ForeColor = color_1;
            lblConstructionYardManufacturers.BackColor = Color.Transparent;
            lblConstructionYardManufacturers.Location = new Point(0, 345);
            duExoPvEoA.Size = new Size(555, 150);
            duExoPvEoA.Location = new Point(0, 360);
            duExoPvEoA.BringToFront();
            lblConstructionYardManufacturerWaitQueue.Text = TextResolver.GetText("Components waiting to be manufactured");
            lblConstructionYardManufacturerWaitQueue.Font = font_2;
            lblConstructionYardManufacturerWaitQueue.ForeColor = color_1;
            lblConstructionYardManufacturerWaitQueue.BackColor = Color.Transparent;
            lblConstructionYardManufacturerWaitQueue.Location = new Point(0, 520);
            ctlConstructionYardManufacturerWaitQueue.Size = new Size(555, 150);
            ctlConstructionYardManufacturerWaitQueue.Location = new Point(0, 535);
            ctlConstructionYardManufacturerWaitQueue.BringToFront();
            if (object_7 != null)
            {
                if (bool_28)
                {
                    StellarObject stellarObject = null;
                    if (object_7 is BuiltObject)
                    {
                        stellarObject = (BuiltObject)object_7;
                    }
                    else if (object_7 is Habitat)
                    {
                        stellarObject = (Habitat)object_7;
                    }
                    if (stellarObject.Empire != _Game.PlayerEmpire)
                    {
                        return;
                    }
                    Habitat colony = null;
                    if (stellarObject is Habitat)
                    {
                        colony = (Habitat)stellarObject;
                    }
                    if (stellarObject is BuiltObject && ((BuiltObject)stellarObject).TopSpeed > 0)
                    {
                        pnlBuiltObjectConstructionYardPurchaser.Enabled = false;
                    }
                    else
                    {
                        pnlBuiltObjectConstructionYardPurchaser.Enabled = true;
                        Empire empire = stellarObject.Empire;
                        bool allowPrivateConstruction = false;
                        if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && stellarObject is Habitat && ((Habitat)stellarObject).GetPirateControl().CheckFactionHasControl(_Game.PlayerEmpire))
                        {
                            empire = _Game.PlayerEmpire;
                        }
                        if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && stellarObject is BuiltObject && ((BuiltObject)stellarObject).Role == BuiltObjectRole.Base)
                        {
                            allowPrivateConstruction = true;
                        }
                        pnlBuiltObjectConstructionYardPurchaser.BindData(empire, stellarObject.ConstructionQueue, colony, _Game.Galaxy, allowPrivateConstruction);
                    }
                    if (stellarObject is BuiltObject && ((BuiltObject)stellarObject).ManufacturingQueue != null)
                    {
                        duExoPvEoA.BindData(((BuiltObject)stellarObject).ManufacturingQueue.Manufacturers, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                        ctlConstructionYardManufacturerWaitQueue.BindData(((BuiltObject)stellarObject).ManufacturingQueue.ComponentWaitQueue, bitmap_21);
                    }
                    else
                    {
                        duExoPvEoA.BindData(null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                        ctlConstructionYardManufacturerWaitQueue.BindData(null, bitmap_21);
                    }
                    if (stellarObject.ConstructionQueue != null)
                    {
                        ctlConstructionYards.BindData(_Game.Galaxy, stellarObject.ConstructionQueue.ConstructionYards, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                        ctlConstructionYardWaitQueue.BindData(stellarObject.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    }
                    else
                    {
                        ctlConstructionYards.BindData(_Game.Galaxy, null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                        ctlConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
                    }
                }
                else if (object_7 is Habitat)
                {
                    Habitat habitat = (Habitat)object_7;
                    Empire empire2 = habitat.Empire;
                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && habitat.GetPirateControl().CheckFactionHasControl(_Game.PlayerEmpire))
                    {
                        empire2 = _Game.PlayerEmpire;
                    }
                    pnlColonyConstructionYardPurchaser.BindData(empire2, habitat.ConstructionQueue, habitat, _Game.Galaxy, allowPrivateConstruction: false);
                    if (habitat.ConstructionQueue != null)
                    {
                        ctlConstructionYards.BindData(_Game.Galaxy, habitat.ConstructionQueue.ConstructionYards, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                        ctlConstructionYardWaitQueue.BindData(habitat.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    }
                    else
                    {
                        ctlConstructionYards.BindData(_Game.Galaxy, null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                        ctlConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
                    }
                    if (habitat.ManufacturingQueue != null)
                    {
                        duExoPvEoA.BindData(habitat.ManufacturingQueue.Manufacturers, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                        ctlConstructionYardManufacturerWaitQueue.BindData(habitat.ManufacturingQueue.ComponentWaitQueue, bitmap_21);
                    }
                    else
                    {
                        duExoPvEoA.BindData(null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                        ctlConstructionYardManufacturerWaitQueue.BindData(null, bitmap_21);
                    }
                }
                else
                {
                    ctlConstructionYards.BindData(_Game.Galaxy, null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                    pnlBuiltObjectConstructionYardPurchaser.BindData(null, null, null, _Game.Galaxy, allowPrivateConstruction: false);
                    pnlColonyConstructionYardPurchaser.BindData(null, null, null, _Game.Galaxy, allowPrivateConstruction: false);
                    duExoPvEoA.BindData(null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                    ctlConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
                    ctlConstructionYardManufacturerWaitQueue.BindData(null, bitmap_21);
                }
            }
            else
            {
                ctlConstructionYards.BindData(_Game.Galaxy, null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                pnlBuiltObjectConstructionYardPurchaser.BindData(null, null, null, _Game.Galaxy, allowPrivateConstruction: false);
                pnlColonyConstructionYardPurchaser.BindData(null, null, null, _Game.Galaxy, allowPrivateConstruction: false);
                duExoPvEoA.BindData(null, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                ctlConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
                ctlConstructionYardManufacturerWaitQueue.BindData(null, bitmap_21);
            }
        }

        private void method_170(BuiltObject builtObject_8)
        {
            ctlBuiltObjectComponents.Size = new Size(455, 275);
            ctlBuiltObjectComponents.Location = new Point(0, 0);
            ctlBuiltObjectComponents.BringToFront();
            ctlBuiltObjectComponents.Grid.Columns["Picture"].Width = 40;
            ctlBuiltObjectComponents.Grid.Columns["Name"].Width = 275;
            ctlBuiltObjectComponents.Grid.Columns["Category"].Width = 100;
            ctlBuiltObjectComponents.Grid.Columns["Size"].Width = 40;
            if (ctlBuiltObjectComponents.SelectedComponent != null)
            {
                lblBuiltObjectComponentsResources.Text = string.Format(TextResolver.GetText("Resources for"), ctlBuiltObjectComponents.SelectedComponent.Name);
            }
            else
            {
                lblBuiltObjectComponentsResources.Text = TextResolver.GetText("Resources");
            }
            lblBuiltObjectComponentsResources.Font = font_7;
            lblBuiltObjectComponentsResources.ForeColor = color_1;
            lblBuiltObjectComponentsResources.BackColor = Color.Transparent;
            lblBuiltObjectComponentsResources.Location = new Point(465, 0);
            lblBuiltObjectComponentsResources.Size = new Size(205, 45);
            lblBuiltObjectComponentsResources.TextAlign = ContentAlignment.BottomLeft;
            lblBuiltObjectComponentsResources.AutoSize = false;
            ctlBuiltObjectComponentsResources.Size = new Size(205, 165);
            ctlBuiltObjectComponentsResources.Location = new Point(465, 45);
            ctlBuiltObjectComponentsResources.BringToFront();
            ctlBuiltObjectComponentsResources.Grid.Columns["Picture"].Width = 40;
            ctlBuiltObjectComponentsResources.Grid.Columns["Type"].Width = 125;
            ctlBuiltObjectComponentsResources.Grid.Columns["Quantity"].Width = 40;
            lblBuiltObjectAutoRetrofit.Text = TextResolver.GetText("Retrofit Stance");
            lblBuiltObjectAutoRetrofit.Font = font_6;
            lblBuiltObjectAutoRetrofit.Location = new Point(465, 220);
            cmbBuiltObjectAutoRetrofit.Size = new Size(200, 21);
            cmbBuiltObjectAutoRetrofit.Location = new Point(465, 240);
            cmbBuiltObjectAutoRetrofit.BringToFront();
            cmbBuiltObjectAutoRetrofit.Items.Clear();
            cmbBuiltObjectAutoRetrofit.Items.Add(TextResolver.GetText("Auto Retrofit (including advisor suggestions)"));
            cmbBuiltObjectAutoRetrofit.Items.Add(TextResolver.GetText("Only Retrofit When Manually Ordered"));
            ctlBuiltObjectComponents_SelectionChanged(null, null);
        }

        private void ctlBuiltObjectComponents_SelectionChanged(object sender, EventArgs e)
        {
            BuiltObjectComponent selectedComponent = ctlBuiltObjectComponents.SelectedComponent;
            if (selectedComponent != null)
            {
                lblBuiltObjectComponentsResources.Text = string.Format(TextResolver.GetText("Resources for"), selectedComponent.Name);
                ctlBuiltObjectComponentsResources.BindData(selectedComponent.RequiredResources, _uiResourcesBitmaps);
            }
            else
            {
                lblBuiltObjectComponentsResources.Text = TextResolver.GetText("Resources");
                ctlBuiltObjectComponentsResources.BindData(null, _uiResourcesBitmaps);
            }
        }

        private string method_171(TroopList troopList_0)
        {
            string result = string.Empty;
            if (troopList_0 != null)
            {
                result = troopList_0.Count + " " + TextResolver.GetText("troops");
                int infantryCount = 0;
                int artilleryCount = 0;
                int armorCount = 0;
                int specialForcesCount = 0;
                troopList_0.GetTroopCountsByType(out infantryCount, out artilleryCount, out armorCount, out specialForcesCount);
                string text = Galaxy.ResolveTroopCompositionDescription(infantryCount, artilleryCount, armorCount, specialForcesCount);
                if (!string.IsNullOrEmpty(text))
                {
                    result = result + ": " + text;
                }
                result += "\n";
                string text2 = result;
                result = text2 + TextResolver.GetText("Total Attack Strength") + ": " + troopList_0.TotalAttackStrength.ToString("0,K") + "\n";
                string text3 = result;
                result = text3 + TextResolver.GetText("Total Defend Strength") + ": " + troopList_0.TotalDefendStrength.ToString("0,K") + "\n";
                result = result + TextResolver.GetText("Annual Maintenance Costs") + ": " + troopList_0.AnnualTroopMaintenance(_Game.PlayerEmpire).ToString("0,K");
            }
            return result;
        }

        private void cmbTroopFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            TroopList troopList = new TroopList();
            ShipGroup selectedFleet = cmbTroopFilter.SelectedFleet;
            Habitat selectedHabitat = cmbTroopFilter.SelectedHabitat;
            if (selectedFleet != null)
            {
                if (selectedFleet.Ships != null)
                {
                    for (int i = 0; i < selectedFleet.Ships.Count; i++)
                    {
                        BuiltObject builtObject = selectedFleet.Ships[i];
                        if (builtObject != null && builtObject.Troops != null && builtObject.Troops.Count > 0)
                        {
                            troopList.AddRange(builtObject.Troops);
                        }
                    }
                }
            }
            else if (selectedHabitat != null)
            {
                if (selectedHabitat.Empire == _Game.PlayerEmpire)
                {
                    selectedHabitat.ResolveInvasionEmpires(out var defender, out var invader);
                    if (defender != null && defender == _Game.PlayerEmpire)
                    {
                        troopList.AddRange(selectedHabitat.Troops);
                        troopList.AddRange(selectedHabitat.TroopsToRecruit);
                    }
                    else if (invader != null && invader == _Game.PlayerEmpire)
                    {
                        troopList.AddRange(selectedHabitat.InvadingTroops);
                    }
                }
            }
            else
            {
                troopList.AddRange(_Game.PlayerEmpire.Troops);
            }
            ctlTroopList.BindData(troopList);
            lblTroopSummary.Text = method_171(troopList);
            if (ctlTroopList.SelectedTroop != null)
            {
                txtTroopInfoName.Text = ctlTroopList.SelectedTroop.Name;
            }
        }

        private void btnTroopUngarrison_Click(object sender, EventArgs e)
        {
            TroopList selectedTroops = ctlTroopList.SelectedTroops;
            if (selectedTroops == null || selectedTroops.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < selectedTroops.Count; i++)
            {
                Troop troop = selectedTroops[i];
                if (troop != null && troop.AtColony && troop.Colony != null && troop.Colony.Empire == _Game.PlayerEmpire)
                {
                    troop.Garrisoned = false;
                    DataGridViewRow dataGridViewRow = ctlTroopList.ResolveRow(troop);
                    if (dataGridViewRow != null)
                    {
                        dataGridViewRow.Cells[1].Style = null;
                        dataGridViewRow.Cells[1].ToolTipText = string.Empty;
                    }
                }
            }
        }

        private void btnTroopGarrison_Click(object sender, EventArgs e)
        {
            TroopList selectedTroops = ctlTroopList.SelectedTroops;
            if (selectedTroops == null || selectedTroops.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < selectedTroops.Count; i++)
            {
                Troop troop = selectedTroops[i];
                if (troop != null && troop.AtColony && troop.Colony != null && troop.Colony.Empire == _Game.PlayerEmpire)
                {
                    troop.Garrisoned = true;
                    DataGridViewRow dataGridViewRow = ctlTroopList.ResolveRow(troop);
                    if (dataGridViewRow != null)
                    {
                        dataGridViewRow.Cells[1].Style = ctlTroopList.GarrisonStyle;
                        dataGridViewRow.Cells[1].ToolTipText = TextResolver.GetText("This troop is garrisoned at this location");
                    }
                }
            }
        }

        private void method_172(Troop troop_0)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlTroopInfo.Size = new Size(1065, 700);
            pnlTroopInfo.Location = new Point((mainView.Width - pnlTroopInfo.Width) / 2, (mainView.Height - pnlTroopInfo.Height) / 2);
            pnlTroopInfo.DoLayout();
            lblTroopList.Visible = false;
            lnkTroops.Location = new Point(10, 9);
            lnkTroops.Text = TextResolver.GetText("Learn about Troops") + "...";
            lblTroopFilter.Location = new Point(780, 14);
            lblTroopFilter.Text = TextResolver.GetText("Filter by");
            lblTroopFilter.Font = font_6;
            cmbTroopFilter.Size = new Size(200, 21);
            cmbTroopFilter.Location = new Point(840, 10);
            cmbTroopFilter.Font = font_6;
            cmbTroopFilter.BindData(_Game.PlayerEmpire.ShipGroups, _Game.PlayerEmpire.Colonies, provideNullSelection: true, _Game.Galaxy);
            cmbTroopFilter.SelectedIndex = 0;
            lblTroopInfoName.Text = TextResolver.GetText("Name");
            lblTroopInfoName.Font = font_2;
            lblTroopInfoName.ForeColor = color_1;
            lblTroopInfoName.BackColor = Color.Transparent;
            lblTroopInfoName.Location = new Point(10, 46);
            txtTroopInfoName.Font = font_7;
            txtTroopInfoName.BackColor = Color.FromArgb(48, 48, 64);
            txtTroopInfoName.ForeColor = Color.FromArgb(170, 170, 170);
            txtTroopInfoName.Size = new Size(250, 20);
            txtTroopInfoName.Location = new Point(60, 43);
            txtTroopInfoName.BringToFront();
            ctlTroopList.Size = new Size(720, 519);
            ctlTroopList.Location = new Point(10, 73);
            ctlTroopList.Grid.Columns["Empire"].Width = 30;
            ctlTroopList.Grid.Columns["Name"].Width = 160;
            ctlTroopList.Grid.Columns["Experience"].Width = 80;
            ctlTroopList.Grid.Columns["Type"].Width = 100;
            ctlTroopList.Grid.Columns["Size"].Width = 50;
            ctlTroopList.Grid.Columns["AttackStrength"].Width = 50;
            ctlTroopList.Grid.Columns["DefendStrength"].Width = 50;
            ctlTroopList.Grid.Columns["Maintenance"].Width = 80;
            ctlTroopList.Grid.Columns["Location"].Width = 120;
            ctlTroopList.BringToFront();
            ctlTroopList.Grid.MultiSelect = true;
            if (troop_0 != null)
            {
                ctlTroopList.SelectTroop(troop_0);
            }
            if (ctlTroopList.SelectedTroop != null)
            {
                txtTroopInfoName.Text = ctlTroopList.SelectedTroop.Name;
            }
            lblTroopSummary.Font = font_3;
            lblTroopSummary.ForeColor = color_2;
            lblTroopSummary.BackColor = Color.Transparent;
            lblTroopSummary.Location = new Point(380, 9);
            lblTroopsGalaxyMapTitle.Location = new Point(740, 70);
            dboYnQplv3.BringToFront();
            dboYnQplv3.Size = new Size(300, 300);
            dboYnQplv3.Location = new Point(740, 88);
            btnTroopGoto.Size = new Size(130, 25);
            btnTroopGoto.Location = new Point(10, 602);
            btnTroopDisband.Location = new Point(150, 602);
            btnTroopDisband.Size = new Size(180, 25);
            btnTroopGarrison.Size = new Size(180, 25);
            btnTroopGarrison.Location = new Point(340, 602);
            btnTroopGarrison.Text = TextResolver.GetText("Garrison selected troops");
            btnTroopUngarrison.Size = new Size(200, 25);
            btnTroopUngarrison.Location = new Point(530, 602);
            btnTroopUngarrison.Text = TextResolver.GetText("Ungarrison selected troops");
            ctlTroopList_SelectionChanged(null, null);
            pnlTroopInfo.Visible = true;
            pnlTroopInfo.BringToFront();
            ctlTroopList.Focus();
        }

        private void ctlTroopList_SelectionChanged(object sender, EventArgs e)
        {
            Troop selectedTroop = ctlTroopList.SelectedTroop;
            if (selectedTroop != null)
            {
                txtTroopInfoName.Text = selectedTroop.Name;
                if (selectedTroop.AtColony)
                {
                    dboYnQplv3.SetPosition(selectedTroop.Colony.Xpos, selectedTroop.Colony.Ypos);
                }
                else if (selectedTroop.BuiltObject != null)
                {
                    dboYnQplv3.SetPosition(selectedTroop.BuiltObject.Xpos, selectedTroop.BuiltObject.Ypos);
                }
                dboYnQplv3.SetSystem(null);
                dboYnQplv3.Invalidate();
            }
            else
            {
                txtTroopInfoName.Text = string.Empty;
                dboYnQplv3.SetPosition(0.0, 0.0);
                dboYnQplv3.SetSystem(null);
                dboYnQplv3.Invalidate();
            }
        }

        private void method_173(Character character_0)
        {
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlCharacterInfo.Size = new Size(380, 325);
            pnlCharacterInfo.Location = new Point((mainView.Width - pnlCharacterInfo.Width) / 2, (mainView.Height - pnlCharacterInfo.Height) / 2);
            lblCharacterInfoTitle.Font = font_1;
            lblCharacterInfoTitle.ForeColor = color_0;
            lblCharacterInfoTitle.BackColor = Color.Transparent;
            lblCharacterInfoTitle.Text = "Persons";
            lblCharacterInfoTitle.Location = new Point(10, 8);
            lblCharacterList.Visible = false;
            ctlCharacterList.Size = new Size(360, 250);
            ctlCharacterList.Location = new Point(10, 30);
            ctlCharacterList.BringToFront();
            ctlCharacterList.BindData(characterImageCache_0, _Game.PlayerEmpire.Characters, _Game.Galaxy, bitmap_91);
            btnCharacterInfoClose.Size = new Size(150, 25);
            btnCharacterInfoClose.Location = new Point(220, 290);
            method_174(null, null);
            pnlCharacterInfo.Visible = true;
            pnlCharacterInfo.BringToFront();
        }

        private void method_174(object sender, EventArgs e)
        {
            _ = ctlCharacterList.SelectedCharacter;
        }

        private void ctlBuiltObjectList_SelectionChanged(object sender, EventArgs e)
        {
            StellarObject selectedStellarObject = ctlBuiltObjectList.SelectedStellarObject;
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            if (selectedStellarObject != null)
            {
                if (selectedStellarObject is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)selectedStellarObject;
                    BuiltObjectImageData builtObjectImageData = builtObjectImageCache_0.ObtainImageData(builtObject);
                    Bitmap image = new Bitmap(builtObjectImageData.Image);
                    image = PrepareBuiltObjectImage(builtObject, image, builtObject.Empire.MainColor, builtObject.Empire.SecondaryColor, 1.0, 1);
                    pnlBuiltObjectDetail.SetData(_Game, _Game.Galaxy, image, new Bitmap(builtObjectImageData.MaskImage), builtObject);
                }
                else if (selectedStellarObject is Habitat)
                {
                    Habitat habitat = (Habitat)selectedStellarObject;
                    Bitmap backgroundPicture = mainView.method_54(habitat, pnlBuiltObjectDetail.ClientSize.Width);
                    pnlBuiltObjectDetail.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat);
                }
            }
            else if (selectedBuiltObjects != null && selectedBuiltObjects.Count > 0)
            {
                pnlBuiltObjectDetail.SetData(_Game, _Game.Galaxy, selectedBuiltObjects);
            }
            else
            {
                pnlBuiltObjectDetail.ClearData();
            }
            tabBuiltObject_Cargo.Text = TextResolver.GetText("Cargo");
            kuvxnccAc2.Text = TextResolver.GetText("Components");
            tabBuiltObject_ConstructionYards.Text = TextResolver.GetText("Construction Yards");
            tabBuiltObject_DockingBays.Text = TextResolver.GetText("Docking Bays");
            tabBuiltObject_Troops.Text = TextResolver.GetText("Troops & Characters");
            tabBuiltObject_Weapons.Text = TextResolver.GetText("Weapons");
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            string text5 = string.Empty;
            string text6 = string.Empty;
            if (selectedStellarObject != null)
            {
                if (selectedStellarObject.Cargo != null && selectedStellarObject.Cargo.Count > 0)
                {
                    text = " (" + selectedStellarObject.Cargo.Count + ")";
                }
                if (selectedStellarObject is BuiltObject && ((BuiltObject)selectedStellarObject).DamagedComponentCount > 0)
                {
                    text2 = " (" + ((BuiltObject)selectedStellarObject).DamagedComponentCount + " " + TextResolver.GetText("damaged") + ")";
                }
                if (selectedStellarObject.ConstructionQueue != null && selectedStellarObject.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                {
                    text3 = " (" + selectedStellarObject.ConstructionQueue.ConstructionYards.CountUnderConstruction + ")";
                }
                if (selectedStellarObject.DockingBays != null && selectedStellarObject.DockingBays.CountDocked > 0)
                {
                    text4 = " (" + selectedStellarObject.DockingBays.CountDocked + ")";
                }
                if ((selectedStellarObject.Troops != null && selectedStellarObject.Troops.Count > 0) || (selectedStellarObject.Characters != null && selectedStellarObject.Characters.Count > 0))
                {
                    int num = 0;
                    if (selectedStellarObject.Troops != null)
                    {
                        num += selectedStellarObject.Troops.Count;
                    }
                    if (selectedStellarObject.Characters != null)
                    {
                        num += selectedStellarObject.Characters.Count;
                    }
                    text5 = " (" + num + ")";
                }
                if (selectedStellarObject is BuiltObject && ((BuiltObject)selectedStellarObject).Weapons.Count > 0)
                {
                    text6 = " (" + ((BuiltObject)selectedStellarObject).Weapons.Count + ")";
                }
                tabBuiltObject_Cargo.Text = TextResolver.GetText("Cargo") + text;
                kuvxnccAc2.Text = TextResolver.GetText("Components") + text2;
                tabBuiltObject_ConstructionYards.Text = TextResolver.GetText("Construction Yards") + text3;
                tabBuiltObject_DockingBays.Text = TextResolver.GetText("Docking Bays") + text4;
                tabBuiltObject_Troops.Text = TextResolver.GetText("Troops & Characters") + text5;
                tabBuiltObject_Weapons.Text = TextResolver.GetText("Weapons") + text6;
            }
            if (selectedStellarObject != null)
            {
                if (selectedStellarObject is BuiltObject)
                {
                    btnBuiltObjectGoto.Enabled = true;
                    btnBuiltObjectSelect.Enabled = true;
                    btnBuiltObjectViewDesign.Enabled = true;
                    if (((BuiltObject)selectedStellarObject).ShipGroup == null)
                    {
                        btnBuiltObjectViewShipGroup.Enabled = false;
                    }
                    else
                    {
                        btnBuiltObjectViewShipGroup.Enabled = true;
                    }
                    if (((BuiltObject)selectedStellarObject).DamagedComponentCount > 0 && ((BuiltObject)selectedStellarObject).TopSpeed > 0 && ((BuiltObject)selectedStellarObject).WarpSpeed > 0 && ((BuiltObject)selectedStellarObject).Owner != null)
                    {
                        btnBuiltObjectRepairSelected.Enabled = true;
                    }
                    else
                    {
                        btnBuiltObjectRepairSelected.Enabled = false;
                    }
                    if (((BuiltObject)selectedStellarObject).Role != BuiltObjectRole.Base && ((BuiltObject)selectedStellarObject).Owner != null)
                    {
                        btnBuiltObjectRefuelSelected.Enabled = true;
                    }
                    else
                    {
                        btnBuiltObjectRefuelSelected.Enabled = false;
                    }
                    if (((BuiltObject)selectedStellarObject).Owner != null && ((BuiltObject)selectedStellarObject).Role != BuiltObjectRole.Base)
                    {
                        btnBuiltObjectRetireSelected.Enabled = true;
                    }
                    else
                    {
                        btnBuiltObjectRetireSelected.Enabled = false;
                    }
                    if (((BuiltObject)selectedStellarObject).Role == BuiltObjectRole.Military)
                    {
                        cmbBuiltObjectSetFleet.Enabled = true;
                    }
                    else
                    {
                        cmbBuiltObjectSetFleet.Enabled = false;
                    }
                }
                else
                {
                    btnBuiltObjectGoto.Enabled = false;
                    btnBuiltObjectSelect.Enabled = false;
                    btnBuiltObjectViewDesign.Enabled = false;
                }
                ctlBuiltObjectCargo.BindData(selectedStellarObject.Cargo, bitmap_21, _uiResourcesBitmaps, builtObjectImageCache_0.GetImagesSmall(), _Game.Galaxy);
                if (selectedStellarObject is BuiltObject)
                {
                    BuiltObject builtObject2 = (BuiltObject)selectedStellarObject;
                    ctlBuiltObjectCharactersTroops.BindData(selectedStellarObject.Empire, selectedStellarObject.Characters, null, selectedStellarObject.Troops, null, null, characterImageCache_0);
                    method_179(builtObject2);
                    ctlBuiltObjectComponents.BindData(builtObject2);
                    if (cmbBuiltObjectAutoRetrofit.Items != null && cmbBuiltObjectAutoRetrofit.Items.Count == 2)
                    {
                        if (builtObject2.SuppressAutoRetrofit)
                        {
                            cmbBuiltObjectAutoRetrofit.SelectedIndex = 1;
                        }
                        else
                        {
                            cmbBuiltObjectAutoRetrofit.SelectedIndex = 0;
                        }
                    }
                    ResourceDatePairList resourceDatePairList = null;
                    if (builtObject2.ManufacturingQueue != null)
                    {
                        resourceDatePairList = builtObject2.ManufacturingQueue.DeficientResources;
                    }
                    else if (builtObject2.RetrofitBaseManufacturingQueue != null)
                    {
                        resourceDatePairList = builtObject2.RetrofitBaseManufacturingQueue.DeficientResources;
                    }
                    if (resourceDatePairList != null && resourceDatePairList.Count > 0)
                    {
                        ResourceDatePair[] array = resourceDatePairList.ToArray();
                        string text7 = string.Empty;
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (i > 0)
                            {
                                text7 += ", ";
                            }
                            text7 += new Resource(array[i].ResourceId).Name;
                        }
                        string text8 = string.Format(TextResolver.GetText("Construction Resource Shortage Message"), builtObject2.Name, text7);
                        lblBuiltObjectCargoConstructionResourceShortage.Text = text8;
                    }
                    else
                    {
                        lblBuiltObjectCargoConstructionResourceShortage.Text = string.Empty;
                    }
                    switch (builtObject2.SubRole)
                    {
                        default:
                            cmbBuiltObjectAutoRetrofit.Enabled = true;
                            break;
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                        case BuiltObjectSubRole.PassengerShip:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                            cmbBuiltObjectAutoRetrofit.Enabled = false;
                            break;
                    }
                }
                else
                {
                    lblBuiltObjectCargoConstructionResourceShortage.Text = string.Empty;
                    ctlBuiltObjectCharactersTroops.BindData(selectedStellarObject.Empire, selectedStellarObject.Characters, ((Habitat)selectedStellarObject).InvadingCharacters, selectedStellarObject.Troops, null, null, characterImageCache_0);
                    method_179(null);
                    ctlBuiltObjectComponents.BindData(null);
                    if (cmbBuiltObjectAutoRetrofit.Items != null && cmbBuiltObjectAutoRetrofit.Items.Count > 0)
                    {
                        cmbBuiltObjectAutoRetrofit.SelectedIndex = 0;
                    }
                    cmbBuiltObjectAutoRetrofit.Enabled = false;
                }
                hvhxxedjqS.Text = selectedStellarObject.Name;
                method_169(selectedStellarObject, bool_28: true);
                method_176(selectedStellarObject);
                if (selectedStellarObject is BuiltObject)
                {
                    method_175((BuiltObject)selectedStellarObject);
                }
                else
                {
                    method_175(null);
                }
                gmapBuiltObject.SetPosition(selectedStellarObject.Xpos, selectedStellarObject.Ypos);
                gmapBuiltObject.Invalidate();
                if (selectedStellarObject is BuiltObject && ((BuiltObject)selectedStellarObject).Weapons.Count == 0)
                {
                    tabBuiltObject_Weapons.Enabled = false;
                }
                else
                {
                    tabBuiltObject_Weapons.Enabled = true;
                }
                int num2 = 0;
                if (selectedStellarObject.DockingBays != null)
                {
                    for (int j = 0; j < selectedStellarObject.DockingBays.Count; j++)
                    {
                        DockingBay dockingBay = selectedStellarObject.DockingBays[j];
                        if (dockingBay.DockedShip != null)
                        {
                            num2++;
                        }
                    }
                }
                if (selectedStellarObject.DockingBays != null && selectedStellarObject.DockingBays.Count != 0)
                {
                    tabBuiltObject_DockingBays.Enabled = true;
                }
                else
                {
                    tabBuiltObject_DockingBays.Enabled = false;
                }
            }
            else
            {
                ctlBuiltObjectCargo.BindData(null, bitmap_21, _uiResourcesBitmaps, builtObjectImageCache_0.GetImagesSmall(), _Game.Galaxy);
                lblBuiltObjectCargoConstructionResourceShortage.Text = string.Empty;
                ctlBuiltObjectCharactersTroops.BindData(null, null, null, null, null, null, characterImageCache_0);
                method_179(null);
                ctlBuiltObjectComponents.BindData(null);
                if (cmbBuiltObjectAutoRetrofit.Items != null && cmbBuiltObjectAutoRetrofit.Items.Count > 0)
                {
                    cmbBuiltObjectAutoRetrofit.SelectedIndex = 0;
                }
                hvhxxedjqS.Text = string.Empty;
                method_169(null, bool_28: true);
                method_176(null);
                method_175(null);
                gmapBuiltObject.SetPosition(0.0, 0.0);
                gmapBuiltObject.Invalidate();
                string text9 = TextResolver.GetText("View Docking Bays") + "\n";
                text9 = text9 + "0 " + TextResolver.GetText("docked") + ", ";
                text9 = text9 + "0 " + TextResolver.GetText("waiting");
            }
        }

        private void mUwHhIdjxs(object sender, EventArgs e)
        {
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            if (cmbBuiltObjectAutoRetrofit.Items != null && cmbBuiltObjectAutoRetrofit.Items.Count > 0 && selectedBuiltObjects != null && selectedBuiltObjects.Count == 1)
            {
                switch (cmbBuiltObjectAutoRetrofit.SelectedIndex)
                {
                    case 0:
                        selectedBuiltObjects[0].SuppressAutoRetrofit = false;
                        break;
                    case 1:
                        selectedBuiltObjects[0].SuppressAutoRetrofit = true;
                        break;
                }
            }
        }

        private void method_175(BuiltObject builtObject_8)
        {
            if (builtObject_8 != null)
            {
                ctlWeapons.BindData(builtObject_8.Weapons, bitmap_21);
            }
            else
            {
                ctlWeapons.BindData(null, bitmap_21);
            }
            ctlWeapons.Size = new Size(600, 275);
            ctlWeapons.Location = new Point(0, 0);
            ctlWeapons.BringToFront();
            ctlWeapons.Grid.Columns["Picture"].Width = 40;
            ctlWeapons.Grid.Columns["Name"].Width = 135;
            ctlWeapons.Grid.Columns["Speed"].Width = 50;
            ctlWeapons.Grid.Columns["EnergyRequired"].Width = 50;
            ctlWeapons.Grid.Columns["FireRate"].Width = 50;
            ctlWeapons.Grid.Columns["DamageGraph"].Width = 275;
        }

        private void method_176(object object_7)
        {
            DockingBayList dockingBays = null;
            BuiltObjectList builtObjects = null;
            if (object_7 != null)
            {
                if (object_7 is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)object_7;
                    dockingBays = builtObject.DockingBays;
                    builtObjects = builtObject.DockingBayWaitQueue;
                }
                else if (object_7 is Habitat)
                {
                    Habitat habitat = (Habitat)object_7;
                    dockingBays = habitat.DockingBays;
                    builtObjects = habitat.DockingBayWaitQueue;
                }
            }
            ctlDockingBays.Size = new Size(555, 130);
            ctlDockingBays.Location = new Point(0, 0);
            ctlDockingBays.BindData(dockingBays, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
            ctlDockingBays.BringToFront();
            ctlDockingBays.Grid.Columns["ComponentPicture"].Width = 40;
            ctlDockingBays.Grid.Columns["ShipEmpire"].Width = 30;
            ctlDockingBays.Grid.Columns["ShipPicture"].Width = 40;
            ctlDockingBays.Grid.Columns["ShipName"].Width = 300;
            ctlDockingBays.Grid.Columns["ShipCommand"].Width = 145;
            lblDockingBayWaitQueue.Text = TextResolver.GetText("Ships waiting for a Docking Bay");
            lblDockingBayWaitQueue.Font = font_2;
            lblDockingBayWaitQueue.ForeColor = color_1;
            lblDockingBayWaitQueue.BackColor = Color.Transparent;
            lblDockingBayWaitQueue.Location = new Point(0, 135);
            ctlDockingWaitQueue.Size = new Size(555, 120);
            ctlDockingWaitQueue.Location = new Point(0, 155);
            ctlDockingWaitQueue.BindData(builtObjects, _Game.Galaxy);
            ctlDockingWaitQueue.BringToFront();
            ctlDockingWaitQueue.Grid.Columns["Empire"].Width = 30;
            ctlDockingWaitQueue.Grid.Columns["Picture"].Width = 40;
            ctlDockingWaitQueue.Grid.Columns["Name"].Width = 155;
            ctlDockingWaitQueue.Grid.Columns["Role"].Width = 150;
            ctlDockingWaitQueue.Grid.Columns["Mission"].Width = 80;
            ctlDockingWaitQueue.Grid.Columns["System"].Width = 100;
            ctlDockingWaitQueue.Grid.Columns["Fleet"].Visible = false;
            ctlDockingWaitQueue.Grid.Columns["Automated"].Visible = false;
        }

        private void method_177(BuiltObject builtObject_8)
        {
            method_178(builtObject_8, int_60);
        }

        private void method_178(BuiltObject builtObject_8, int int_64)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlBuiltObjectInfo.Size = new Size(1024, 756);
            pnlBuiltObjectInfo.Location = new Point((mainView.Width - pnlBuiltObjectInfo.Width) / 2, (mainView.Height - pnlBuiltObjectInfo.Height) / 2);
            pnlBuiltObjectInfo.DoLayout();
            cmbBuiltObjectFilter.Parent = pnlBuiltObjectInfo.pnlHeader;
            cmbBuiltObjectFilter.Size = new Size(210, 21);
            cmbBuiltObjectFilter.Location = new Point(380, 12);
            cmbBuiltObjectFilter.MaxDropDownItems = 18;
            if (int_64 >= 0)
            {
                cmbBuiltObjectFilter.SelectedIndex = int_64;
            }
            else if (builtObject_8 != null && builtObject_8.ActualEmpire == _Game.PlayerEmpire)
            {
                cmbBuiltObjectFilter.SelectedIndex = 1;
            }
            else
            {
                cmbBuiltObjectFilter.SelectedIndex = 0;
            }
            cmbBuiltObjectFilter.Visible = true;
            btnBuiltObjectShowMiningPlanner.Visible = false;
            ctlBuiltObjectList.ShowBuiltObjectDetail = true;
            ctlBuiltObjectList.Height = 288;
            ctlBuiltObjectList.Width = 680;
            ctlBuiltObjectList.Location = new Point(10, 10);
            StellarObjectList stellarObjects = method_423(builtObject_8);
            ctlBuiltObjectList.BindDataGeneric(stellarObjects, _Game.Galaxy, showDetails: true);
            ctlBuiltObjectList.BringToFront();
            ctlBuiltObjectList.SelectBuiltObject(builtObject_8);
            ctlBuiltObjectList.Grid.Columns["Empire"].Width = 25;
            ctlBuiltObjectList.Grid.Columns["Picture"].Width = 35;
            ctlBuiltObjectList.Grid.Columns["Name"].Width = 140;
            ctlBuiltObjectList.Grid.Columns["Role"].Width = 130;
            ctlBuiltObjectList.Grid.Columns["Mission"].Width = 65;
            ctlBuiltObjectList.Grid.Columns["System"].Width = 70;
            ctlBuiltObjectList.Grid.Columns["Firepower"].Width = 30;
            ctlBuiltObjectList.Grid.Columns["Speed"].Width = 30;
            ctlBuiltObjectList.Grid.Columns["Maintenance"].Width = 40;
            ctlBuiltObjectList.Grid.Columns["Fleet"].Width = 65;
            ctlBuiltObjectList.Grid.Columns["Automated"].Width = 50;
            method_158(Color.FromArgb(0, 0, 64), Color.FromArgb(0, 0, 64), ctlBuiltObjectList.Grid, typeof(VScrollBar));
            lblBuiltObjectGalaxyMapTitle.Location = new Point(700, 7);
            gmapBuiltObject.BringToFront();
            gmapBuiltObject.Location = new Point(700, 25);
            gmapBuiltObject.Size = new Size(300, 300);
            btnBuiltObjectSelect.Location = new Point(10, 308);
            btnBuiltObjectSelect.Size = new Size(133, 40);
            btnBuiltObjectGoto.Location = new Point(145, 308);
            btnBuiltObjectGoto.Size = new Size(133, 40);
            btnBuiltObjectViewDesign.Location = new Point(280, 308);
            btnBuiltObjectViewDesign.Size = new Size(133, 40);
            btnBuiltObjectViewShipGroup.Location = new Point(415, 308);
            btnBuiltObjectViewShipGroup.Size = new Size(133, 40);
            cmbBuiltObjectSetFleet.Location = new Point(550, 308);
            cmbBuiltObjectSetFleet.Visible = true;
            cmbBuiltObjectSetFleet.Size = new Size(140, 18);
            cmbBuiltObjectSetFleet.BringToFront();
            btnBuiltObjectRefuelSelected.Size = new Size(133, 25);
            btnBuiltObjectRefuelSelected.Location = new Point(10, 350);
            btnBuiltObjectRefuelSelected.Text = TextResolver.GetText("Refuel");
            btnBuiltObjectRepairSelected.Location = new Point(145, 350);
            btnBuiltObjectRepairSelected.Size = new Size(133, 25);
            btnBuiltObjectRepairSelected.Text = TextResolver.GetText("Repair");
            btnBuiltObjectRetrofitSelected.Location = new Point(280, 350);
            btnBuiltObjectRetrofitSelected.Size = new Size(133, 25);
            btnBuiltObjectRetrofitSelected.Text = TextResolver.GetText("Retrofit");
            btnBuiltObjectRetireSelected.Size = new Size(133, 25);
            btnBuiltObjectRetireSelected.Location = new Point(415, 350);
            btnBuiltObjectRetireSelected.Text = TextResolver.GetText("Retire");
            btnBuiltObjectScrapSelected.Size = new Size(140, 25);
            btnBuiltObjectScrapSelected.Location = new Point(550, 350);
            btnBuiltObjectScrapSelected.Text = TextResolver.GetText("Scrap");
            lblBuiltObjectName.Text = TextResolver.GetText("Name");
            lblBuiltObjectName.Font = font_2;
            lblBuiltObjectName.ForeColor = color_1;
            lblBuiltObjectName.BackColor = Color.Transparent;
            lblBuiltObjectName.Location = new Point(700, 357);
            hvhxxedjqS.Font = font_7;
            hvhxxedjqS.BackColor = Color.FromArgb(48, 48, 64);
            hvhxxedjqS.ForeColor = Color.FromArgb(170, 170, 170);
            hvhxxedjqS.Size = new Size(250, 20);
            hvhxxedjqS.Location = new Point(750, 355);
            hvhxxedjqS.BringToFront();
            pnlBuiltObjectDetail.Size = new Size(300, 300);
            pnlBuiltObjectDetail.Location = new Point(700, 385);
            pnlBuiltObjectDetail.Font = font_3;
            pnlBuiltObjectDetail.CurveMode = CornerCurveMode.BottomRight_TopLeft;
            pnlBuiltObjectDetail.Reset();
            tabBuiltObjectData.Location = new Point(10, 385);
            tabBuiltObjectData.Size = new Size(680, 300);
            tabBuiltObject_Cargo.Text = TextResolver.GetText("Cargo");
            kuvxnccAc2.Text = TextResolver.GetText("Components");
            tabBuiltObject_ConstructionYards.Text = TextResolver.GetText("Construction Yards");
            tabBuiltObject_DockingBays.Text = TextResolver.GetText("Docking Bays");
            tabBuiltObject_Troops.Text = TextResolver.GetText("Troops");
            tabBuiltObject_Weapons.Text = TextResolver.GetText("Weapons");
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            string text5 = string.Empty;
            string text6 = string.Empty;
            if (builtObject_8 != null)
            {
                if (builtObject_8.Cargo != null && builtObject_8.Cargo.Count > 0)
                {
                    text = " (" + builtObject_8.Cargo.Count + ")";
                }
                if (builtObject_8.DamagedComponentCount > 0)
                {
                    text2 = " (" + builtObject_8.DamagedComponentCount + " " + TextResolver.GetText("damaged") + ")";
                }
                if (builtObject_8.ConstructionQueue != null && builtObject_8.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                {
                    text3 = " (" + builtObject_8.ConstructionQueue.ConstructionYards.CountUnderConstruction + ")";
                }
                if (builtObject_8.DockingBays != null && builtObject_8.DockingBays.CountDocked > 0)
                {
                    text4 = " (" + builtObject_8.DockingBays.CountDocked + ")";
                }
                if (builtObject_8.Troops != null && builtObject_8.Troops.Count > 0)
                {
                    text5 = " (" + builtObject_8.Troops.Count + ")";
                }
                if (builtObject_8.Weapons.Count > 0)
                {
                    text6 = " (" + builtObject_8.Weapons.Count + ")";
                }
                tabBuiltObject_Cargo.Text = TextResolver.GetText("Cargo") + text;
                kuvxnccAc2.Text = TextResolver.GetText("Components") + text2;
                tabBuiltObject_ConstructionYards.Text = TextResolver.GetText("Construction Yards") + text3;
                tabBuiltObject_DockingBays.Text = TextResolver.GetText("Docking Bays") + text4;
                tabBuiltObject_Troops.Text = TextResolver.GetText("Troops") + text5;
                tabBuiltObject_Weapons.Text = TextResolver.GetText("Weapons") + text6;
            }
            ctlBuiltObjectCharactersTroops.Size = new Size(455, 275);
            ctlBuiltObjectCharactersTroops.Location = new Point(0, 0);
            ctlBuiltObjectCharactersTroops.BringToFront();
            grpUseTroopLoadouts.Size = new Size(205, 175);
            grpUseTroopLoadouts.Location = new Point(460, 10);
            chkUseTroopLoadouts.Location = new Point(470, 7);
            chkUseTroopLoadouts.Font = font_7;
            chkUseTroopLoadouts.BringToFront();
            numTroopLoadoutInfantry.Location = new Point(10, 23);
            numTroopLoadoutArmored.Location = new Point(10, 53);
            numTroopLoadoutArtillery.Location = new Point(10, 83);
            numTroopLoadoutSpecialForces.Location = new Point(10, 113);
            lblTroopLoadoutInfantry.Location = new Point(50, 28);
            lblTroopLoadoutArmored.Location = new Point(50, 58);
            lblTroopLoadoutArtillery.Location = new Point(50, 88);
            lblTroopLoadoutSpecialForces.Location = new Point(50, 118);
            lblTroopLoadoutTotal.Location = new Point(10, 148);
            lblBuiltObjectTroopLoadoutPartOfFleet.Location = new Point(460, 195);
            lblBuiltObjectTroopLoadoutPartOfFleet.Font = font_7;
            lblBuiltObjectTroopLoadoutPartOfFleet.Size = new Size(205, 140);
            lblBuiltObjectTroopLoadoutPartOfFleet.MaximumSize = new Size(205, 140);
            lblBuiltObjectTroopLoadoutPartOfFleet.ForeColor = Color.Yellow;
            lblTroopLoadoutInfantry.Text = TextResolver.GetText("TroopType Infantry");
            lblTroopLoadoutArmored.Text = TextResolver.GetText("TroopType Armored");
            lblTroopLoadoutArtillery.Text = TextResolver.GetText("TroopType Artillery");
            lblTroopLoadoutSpecialForces.Text = TextResolver.GetText("TroopType SpecialForces");
            numTroopLoadoutInfantry.Font = font_6;
            numTroopLoadoutArmored.Font = font_6;
            numTroopLoadoutArtillery.Font = font_6;
            numTroopLoadoutSpecialForces.Font = font_6;
            lblTroopLoadoutInfantry.Font = font_6;
            lblTroopLoadoutArmored.Font = font_6;
            lblTroopLoadoutArtillery.Font = font_6;
            lblTroopLoadoutSpecialForces.Font = font_6;
            lblTroopLoadoutTotal.Font = font_6;
            ctlBuiltObjectCargo.Size = new Size(350, 275);
            ctlBuiltObjectCargo.Location = new Point(0, 0);
            ctlBuiltObjectCargo.BringToFront();
            ctlBuiltObjectCargo.Grid.Columns["Empire"].Width = 30;
            ctlBuiltObjectCargo.Grid.Columns["Picture"].Width = 40;
            ctlBuiltObjectCargo.Grid.Columns["Name"].Width = 160;
            ctlBuiltObjectCargo.Grid.Columns["Amount"].Width = 60;
            ctlBuiltObjectCargo.Grid.Columns["Reserved"].Width = 60;
            lblBuiltObjectCargoConstructionResourceShortage.Location = new Point(365, 5);
            lblBuiltObjectCargoConstructionResourceShortage.MaximumSize = new Size(300, 200);
            lblBuiltObjectCargoConstructionResourceShortage.Size = new Size(300, 200);
            lblBuiltObjectCargoConstructionResourceShortage.Font = font_6;
            lblBuiltObjectCargoConstructionResourceShortage.ForeColor = Color.FromArgb(255, 128, 0);
            method_182();
            method_170(builtObject_8);
            method_169(builtObject_8, bool_28: true);
            method_176(builtObject_8);
            method_175(builtObject_8);
            gmapBuiltObject.Invalidate();
            ctlBuiltObjectList_SelectionChanged(null, null);
            pnlBuiltObjectInfo.Visible = true;
            pnlBuiltObjectInfo.BringToFront();
            ctlBuiltObjectList.Focus();
        }

        private void method_179(BuiltObject builtObject_8)
        {
            chkUseTroopLoadouts.CheckedChanged -= chkUseTroopLoadouts_CheckedChanged;
            numTroopLoadoutInfantry.ValueChanged -= numTroopLoadoutInfantry_ValueChanged;
            numTroopLoadoutArmored.ValueChanged -= numTroopLoadoutArmored_ValueChanged;
            numTroopLoadoutArtillery.ValueChanged -= numTroopLoadoutArtillery_ValueChanged;
            numTroopLoadoutSpecialForces.ValueChanged -= numTroopLoadoutSpecialForces_ValueChanged;
            if (builtObject_8 != null)
            {
                if (builtObject_8.TroopLoadoutInfantry == byte.MaxValue && builtObject_8.TroopLoadoutArmored == byte.MaxValue && builtObject_8.TroopLoadoutArtillery == byte.MaxValue && builtObject_8.TroopLoadoutSpecialForces == byte.MaxValue)
                {
                    chkUseTroopLoadouts.Checked = false;
                    grpUseTroopLoadouts.Enabled = false;
                    numTroopLoadoutInfantry.Value = 0m;
                    numTroopLoadoutArmored.Value = 0m;
                    numTroopLoadoutArtillery.Value = 0m;
                    numTroopLoadoutSpecialForces.Value = 0m;
                    method_181(builtObject_8);
                }
                else
                {
                    chkUseTroopLoadouts.Checked = true;
                    grpUseTroopLoadouts.Enabled = true;
                    numTroopLoadoutInfantry.Value = builtObject_8.TroopLoadoutInfantry;
                    numTroopLoadoutArmored.Value = builtObject_8.TroopLoadoutArmored;
                    numTroopLoadoutArtillery.Value = builtObject_8.TroopLoadoutArtillery;
                    numTroopLoadoutSpecialForces.Value = builtObject_8.TroopLoadoutSpecialForces;
                    lblTroopLoadoutTotal.Text = string.Empty;
                    method_181(builtObject_8);
                }
                if (builtObject_8.ShipGroup != null)
                {
                    lblBuiltObjectTroopLoadoutPartOfFleet.Text = TextResolver.GetText("Ship Fleet Troop Loadout override");
                }
                else
                {
                    lblBuiltObjectTroopLoadoutPartOfFleet.Text = string.Empty;
                }
            }
            else
            {
                chkUseTroopLoadouts.Checked = false;
                grpUseTroopLoadouts.Enabled = false;
                numTroopLoadoutInfantry.Value = 0m;
                numTroopLoadoutArmored.Value = 0m;
                numTroopLoadoutArtillery.Value = 0m;
                numTroopLoadoutSpecialForces.Value = 0m;
                lblTroopLoadoutTotal.Text = string.Empty;
            }
            chkUseTroopLoadouts.CheckedChanged += chkUseTroopLoadouts_CheckedChanged;
            numTroopLoadoutInfantry.ValueChanged += numTroopLoadoutInfantry_ValueChanged;
            numTroopLoadoutArmored.ValueChanged += numTroopLoadoutArmored_ValueChanged;
            numTroopLoadoutArtillery.ValueChanged += numTroopLoadoutArtillery_ValueChanged;
            numTroopLoadoutSpecialForces.ValueChanged += numTroopLoadoutSpecialForces_ValueChanged;
        }

        private void chkUseTroopLoadouts_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUseTroopLoadouts.Checked)
            {
                grpUseTroopLoadouts.Enabled = false;
                numTroopLoadoutInfantry.Value = 0m;
                numTroopLoadoutArmored.Value = 0m;
                numTroopLoadoutArtillery.Value = 0m;
                numTroopLoadoutSpecialForces.Value = 0m;
                lblTroopLoadoutTotal.Text = string.Empty;
                BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
                if (selectedBuiltObject != null)
                {
                    selectedBuiltObject.TroopLoadoutInfantry = byte.MaxValue;
                    selectedBuiltObject.TroopLoadoutArmored = byte.MaxValue;
                    selectedBuiltObject.TroopLoadoutArtillery = byte.MaxValue;
                    selectedBuiltObject.TroopLoadoutSpecialForces = byte.MaxValue;
                    method_181(selectedBuiltObject);
                }
                return;
            }
            grpUseTroopLoadouts.Enabled = true;
            BuiltObject selectedBuiltObject2 = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject2 != null)
            {
                byte b = (byte)(selectedBuiltObject2.TroopCapacity / 100);
                numTroopLoadoutInfantry.Value = b;
                numTroopLoadoutArmored.Value = 0m;
                numTroopLoadoutArtillery.Value = 0m;
                numTroopLoadoutSpecialForces.Value = 0m;
                lblTroopLoadoutTotal.Text = string.Empty;
                selectedBuiltObject2.TroopLoadoutInfantry = b;
                selectedBuiltObject2.TroopLoadoutArmored = 0;
                selectedBuiltObject2.TroopLoadoutArtillery = 0;
                selectedBuiltObject2.TroopLoadoutSpecialForces = 0;
                method_181(selectedBuiltObject2);
            }
        }

        private int method_180()
        {
            int num = 0;
            num = 0 + (int)numTroopLoadoutInfantry.Value * 100;
            num += (int)numTroopLoadoutArmored.Value * 200;
            num += (int)numTroopLoadoutArtillery.Value * 400;
            return num + (int)numTroopLoadoutSpecialForces.Value * 100;
        }

        private void method_181(BuiltObject builtObject_8)
        {
            if (builtObject_8 != null)
            {
                string text = string.Format(arg0: method_180().ToString("0"), format: TextResolver.GetText("Troop Loadout Description"), arg1: builtObject_8.TroopCapacity.ToString("0"));
                lblTroopLoadoutTotal.Text = text;
            }
        }

        private void numTroopLoadoutInfantry_ValueChanged(object sender, EventArgs e)
        {
            int num = method_180();
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                if (selectedBuiltObject.TroopCapacity < num)
                {
                    int num2 = num - selectedBuiltObject.TroopCapacity;
                    int num3 = (int)(0.999 + (double)num2 / 100.0);
                    int num4 = Math.Max(0, (int)numTroopLoadoutInfantry.Value - num3);
                    numTroopLoadoutInfantry.Value = num4;
                }
                else
                {
                    selectedBuiltObject.TroopLoadoutInfantry = (byte)numTroopLoadoutInfantry.Value;
                    method_181(selectedBuiltObject);
                }
            }
        }

        private void numTroopLoadoutArmored_ValueChanged(object sender, EventArgs e)
        {
            int num = method_180();
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                if (selectedBuiltObject.TroopCapacity < num)
                {
                    int num2 = num - selectedBuiltObject.TroopCapacity;
                    int num3 = (int)(0.999 + (double)num2 / 100.0);
                    int num4 = Math.Max(0, (int)numTroopLoadoutArmored.Value - num3);
                    numTroopLoadoutArmored.Value = num4;
                }
                else
                {
                    selectedBuiltObject.TroopLoadoutArmored = (byte)numTroopLoadoutArmored.Value;
                    method_181(selectedBuiltObject);
                }
            }
        }

        private void numTroopLoadoutArtillery_ValueChanged(object sender, EventArgs e)
        {
            int num = method_180();
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                if (selectedBuiltObject.TroopCapacity < num)
                {
                    int num2 = num - selectedBuiltObject.TroopCapacity;
                    int num3 = (int)(0.999 + (double)num2 / 100.0);
                    int num4 = Math.Max(0, (int)numTroopLoadoutArtillery.Value - num3);
                    numTroopLoadoutArtillery.Value = num4;
                }
                else
                {
                    selectedBuiltObject.TroopLoadoutArtillery = (byte)numTroopLoadoutArtillery.Value;
                    method_181(selectedBuiltObject);
                }
            }
        }

        private void numTroopLoadoutSpecialForces_ValueChanged(object sender, EventArgs e)
        {
            int num = method_180();
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                if (selectedBuiltObject.TroopCapacity < num)
                {
                    int num2 = num - selectedBuiltObject.TroopCapacity;
                    int num3 = (int)(0.999 + (double)num2 / 100.0);
                    int num4 = Math.Max(0, (int)numTroopLoadoutSpecialForces.Value - num3);
                    numTroopLoadoutSpecialForces.Value = num4;
                }
                else
                {
                    selectedBuiltObject.TroopLoadoutSpecialForces = (byte)numTroopLoadoutSpecialForces.Value;
                    method_181(selectedBuiltObject);
                }
            }
        }

        private void method_182()
        {
            cmbBuiltObjectSetFleet.SelectedIndexChanged -= cmbBuiltObjectSetFleet_SelectedIndexChanged;
            cmbBuiltObjectSetFleet.Items.Clear();
            cmbBuiltObjectSetFleet.Items.Add(TextResolver.GetText("Set Fleet") + "...");
            cmbBuiltObjectSetFleet.Items.Add("(" + TextResolver.GetText("None") + ")");
            cmbBuiltObjectSetFleet.Items.Add("(" + TextResolver.GetText("New Fleet") + ")");
            for (int i = 0; i < _Game.PlayerEmpire.ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = _Game.PlayerEmpire.ShipGroups[i];
                cmbBuiltObjectSetFleet.Items.Add(shipGroup.Name);
            }
            if (cmbBuiltObjectSetFleet.Items != null && cmbBuiltObjectSetFleet.Items.Count > 0)
            {
                cmbBuiltObjectSetFleet.SelectedIndex = 0;
            }
            cmbBuiltObjectSetFleet.SelectedIndexChanged += cmbBuiltObjectSetFleet_SelectedIndexChanged;
        }

        private void method_183()
        {
            pnlCharacterInfo.SendToBack();
            pnlCharacterInfo.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
            SetMainFocus();
        }

        private void method_184()
        {
            pnlTroopInfo.SendToBack();
            pnlTroopInfo.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_185()
        {
            pnlBuiltObjectInfo.SendToBack();
            pnlBuiltObjectInfo.Visible = false;
            method_549();
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_186()
        {
            pnlColonyInfo.SendToBack();
            pnlColonyInfo.Visible = false;
            method_549();
            method_551();
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_187(object sender, EventArgs e)
        {
            method_166(null);
        }

        private void method_188(object sender, EventArgs e)
        {
            method_177(null);
        }

        private void method_189(object sender, EventArgs e)
        {
            pnlBuiltObjectInfo.Enabled = false;
            method_170(ctlBuiltObjectList.SelectedBuiltObject);
        }

        private void method_190(object sender, EventArgs e)
        {
            pnlBuiltObjectInfo.Enabled = false;
            method_169(ctlBuiltObjectList.SelectedStellarObject, bool_28: true);
        }

        private void method_191(object sender, EventArgs e)
        {
            pnlColonyInfo.Enabled = false;
            method_169(UnlxwvByxj.SelectedHabitat, bool_28: false);
        }

        private void method_192(object sender, EventArgs e)
        {
            method_172(null);
        }

        private void method_193(object sender, EventArgs e)
        {
            method_173(null);
        }

        private void btnCharacterInfoClose_Click(object sender, EventArgs e)
        {
            method_183();
        }

        private void btnBuiltObjectGoto_Click(object sender, EventArgs e)
        {
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                method_157(selectedBuiltObject);
            }
            method_185();
        }

        private void btnColonyGotoHabitat_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat != null)
            {
                method_157(selectedHabitat);
            }
            method_186();
        }

        private void txtColonyName_Leave(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat != null && !string.IsNullOrEmpty(txtColonyName.Text.Trim()))
            {
                string name = selectedHabitat.Name;
                selectedHabitat.Name = txtColonyName.Text;
                UnlxwvByxj.Grid.SelectedRows[0].Cells[2].Value = selectedHabitat.Name;
                if (name != selectedHabitat.Name)
                {
                    mainView.ClearPrecachedColonyBars();
                }
            }
        }

        private void hvhxxedjqS_Leave(object sender, EventArgs e)
        {
            StellarObject selectedStellarObject = ctlBuiltObjectList.SelectedStellarObject;
            if (selectedStellarObject != null && !string.IsNullOrEmpty(hvhxxedjqS.Text.Trim()))
            {
                selectedStellarObject.Name = hvhxxedjqS.Text;
                ctlBuiltObjectList.Grid.SelectedRows[0].Cells[2].Value = selectedStellarObject.Name;
            }
        }

        private void numColonyTaxRate_Leave(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat == null)
            {
                return;
            }
            double num = (double)numColonyTaxRate.Value / 100.0;
            if (num != (double)selectedHabitat.TaxRate)
            {
                if (_Game.PlayerEmpire.ControlColonyTaxRates && GenerateAutomationMessageBox(TextResolver.GetText("Colony Tax Rates")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                {
                    _Game.PlayerEmpire.ControlColonyTaxRates = false;
                }
                selectedHabitat.TaxRate = (float)num;
            }
            UnlxwvByxj.Grid.SelectedRows[0].Cells["TaxRate"].Value = num;
            Bitmap backgroundPicture = mainView.method_54(selectedHabitat, pnlColonyHabitatInfo.ClientSize.Width);
            pnlColonyHabitatInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, selectedHabitat);
        }

        private void method_194()
        {
            pnlEmpireInfo.SendToBack();
            pnlEmpireInfo.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_195(Empire empire_5)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            Size size = new Size(1040, 760);
            Size size2 = new Size(380, 680);
            int num = 680;
            int num2 = 420;
            int num3 = 820;
            if (pnlEmpireDetailInfo.LargeSize)
            {
                size = new Size(1180, 900);
                size2 = new Size(520, 820);
                num = 820;
                num2 = 420;
                num3 = 960;
            }
            pnlEmpireInfo.Size = size;
            pnlEmpireInfo.Location = new Point((mainView.Width - pnlEmpireInfo.Width) / 2, (mainView.Height - pnlEmpireInfo.Height) / 2);
            pnlEmpireInfo.DoLayout();
            pnlEmpireDetailInfo.Size = size2;
            pnlEmpireDetailInfo.Location = new Point(430, 10);
            pnlEmpireDetailInfo.Font = font_3;
            pnlEmpireDetailInfo.BorderStyle = BorderStyle.FixedSingle;
            pnlEmpireDetailInfo.GradientMode = DistantWorlds.Controls.LinearGradientMode.Vertical;
            pnlEmpireDetailInfo.CurveMode = CornerCurveMode.BottomRight_TopRight;
            pnlEmpireDetailInfo.Curvature = 20;
            pnlEmpireDetailInfo.BorderWidth = 3;
            ctlEmpireDiplomaticRelationList.Height = num;
            ctlEmpireDiplomaticRelationList.Width = num2;
            ctlEmpireDiplomaticRelationList.RelationViewWidth = 180;
            ctlEmpireDiplomaticRelationList.Location = new Point(10, 10);
            ctlEmpireDiplomaticRelationList.BindData(_Game, _Game.PlayerEmpire, font_5, raceImageCache_0, bitmap_48, bitmap_51, bitmap_55, bitmap_81);
            ctlEmpireDiplomaticRelationList.BringToFront();
            btnEmpireTalk.Size = new Size(195, 50);
            btnEmpireTalk.Text = TextResolver.GetText("Speak");
            btnEmpireTalk.Location = new Point(num3, 30);
            lblDiplomacyGalaxyMapTitle.Location = new Point(num3, 97);
            gmapEmpireDetail.BringToFront();
            gmapEmpireDetail.Size = new Size(195, 195);
            gmapEmpireDetail.Location = new Point(num3, 115);
            pnlDiplomaticRelationColorKey.Size = new Size(195, 168);
            pnlDiplomaticRelationColorKey.Location = new Point(num3, 320);
            NuSppwjfQh.Location = new Point(num3, 530);
            NuSppwjfQh.Text = TextResolver.GetText("Learn about Diplomatic Relations...");
            lnkDiplomacyReputation.Location = new Point(num3, 555);
            lnkDiplomacyReputation.Text = TextResolver.GetText("Learn about Empire Reputation...");
            lnkDiplomacyPirates.Location = new Point(num3, 580);
            lnkDiplomacyPirates.Text = TextResolver.GetText("Learn about Pirates...");
            if (empire_5 == null)
            {
                ctlEmpireDiplomaticRelationList_SelectionChanged(_Game.PlayerEmpire, null);
            }
            else
            {
                ctlEmpireDiplomaticRelationList_SelectionChanged(empire_5, null);
            }
            pnlEmpireInfo.Visible = true;
            pnlEmpireInfo.BringToFront();
            ctlEmpireDiplomaticRelationList.Focus();
        }

        private void ctlEmpireDiplomaticRelationList_SelectionChanged(object sender, EventArgs e)
        {
            Empire empire = null;
            if (sender is Empire)
            {
                empire = (Empire)sender;
                ctlEmpireDiplomaticRelationList.SelectEmpire(empire);
            }
            empire = ctlEmpireDiplomaticRelationList.SelectedEmpire;
            method_196(empire, pnlEmpireDetailInfo);
        }

        private void method_196(Empire empire_5, Panel panel_1)
        {
            pnlEmpireDetailInfo.BindData(_Game, empire_5, _Game.PlayerEmpire, characterImageCache_0);
            pnlEmpireDetailInfo.Invalidate();
            if (empire_5 == _Game.PlayerEmpire)
            {
                btnEmpireTalk.Enabled = false;
                btnEmpireTalk.Text = "(" + TextResolver.GetText("Your Empire") + ")";
            }
            else if (empire_5 == null)
            {
                btnEmpireTalk.Enabled = false;
                btnEmpireTalk.Text = TextResolver.GetText("Speak");
            }
            else
            {
                btnEmpireTalk.Enabled = true;
                btnEmpireTalk.Text = string.Format(TextResolver.GetText("Speak with"), empire_5.Name);
            }
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = new HabitatList();
            if (empire_5.PirateEmpireBaseHabitat != null)
            {
                for (int i = 0; i < _Game.PlayerEmpire.KnownPirateBases.Count; i++)
                {
                    BuiltObject builtObject = _Game.PlayerEmpire.KnownPirateBases[i];
                    if (builtObject.Empire == empire_5 && builtObject.ParentHabitat != null)
                    {
                        habitatList.Add(builtObject.ParentHabitat);
                        Habitat item = Galaxy.DetermineHabitatSystemStar(builtObject.ParentHabitat);
                        if (!habitatList2.Contains(item))
                        {
                            habitatList2.Add(item);
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < empire_5.Colonies.Count; j++)
                {
                    Habitat habitat = empire_5.Colonies[j];
                    Galaxy.DetermineHabitatSystemStar(habitat);
                    SystemVisibilityStatus systemVisibilityStatus = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                    if (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored)
                    {
                        habitatList.Add(habitat);
                    }
                }
                for (int k = 0; k < habitatList.Count; k++)
                {
                    Habitat habitat2 = habitatList[k];
                    Habitat item2 = Galaxy.DetermineHabitatSystemStar(habitat2);
                    if (!habitatList2.Contains(item2))
                    {
                        habitatList2.Add(item2);
                    }
                }
            }
            gmapEmpireDetail.SetSystems(habitatList2);
            gmapEmpireDetail.Invalidate();
        }

        private void method_197(object sender, EventArgs e)
        {
            method_195(null);
        }

        private void btnEmpireTalk_Click(object sender, EventArgs e)
        {
            Empire selectedEmpire = ctlEmpireDiplomaticRelationList.SelectedEmpire;
            if (selectedEmpire != null && selectedEmpire != _Game.PlayerEmpire)
            {
                method_295(selectedEmpire);
            }
        }

        private void method_198(object sender, EventArgs e)
        {
            UhvLmNjli7 = true;
        }

        private Habitat method_199(double double_7, double double_8)
        {
            return method_200(double_7, double_8, Galaxy.MouseHoverHabitatProximityRange);
        }

        private Habitat method_200(double double_7, double double_8, double double_9)
        {
            Habitat habitat = _Game.Galaxy.FindNearestHabitat(double_7, double_8);
            if (habitat != null)
            {
                double num = _Game.Galaxy.CalculateDistance(double_7, double_8, habitat.Xpos, habitat.Ypos);
                num -= (double)(habitat.Diameter / 2);
                if (num > 0.0 && num < double_9)
                {
                    return habitat;
                }
            }
            return null;
        }

        private Habitat method_201(double double_7, double double_8)
        {
            object obj = method_143((int)double_7, (int)double_8, bool_28: false);
            if (obj is Habitat)
            {
                return (Habitat)obj;
            }
            return null;
        }

        private BuiltObject method_202(double double_7, double double_8)
        {
            object obj = method_143((int)double_7, (int)double_8, bool_28: false);
            if (obj is BuiltObject)
            {
                return (BuiltObject)obj;
            }
            return null;
        }

        private void method_203()
        {
            string arg = TextResolver.GetText("Selected Item");
            if (_Game.SelectedObject is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)_Game.SelectedObject;
                arg = ((builtObject.Role != BuiltObjectRole.Base) ? (Galaxy.ResolveDescription(builtObject.SubRole) + " " + builtObject.Name) : builtObject.Name);
            }
            else if (_Game.SelectedObject is Habitat)
            {
                Habitat habitat = (Habitat)_Game.SelectedObject;
                arg = Galaxy.ResolveDescription(habitat.Type) + " " + Galaxy.ResolveDescription(habitat.Category) + " " + habitat.Name;
            }
            else if (_Game.SelectedObject is SystemInfo)
            {
                SystemInfo systemInfo = (SystemInfo)_Game.SelectedObject;
                arg = systemInfo.SystemStar.Name + " " + TextResolver.GetText("system");
            }
            else if (_Game.SelectedObject is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)_Game.SelectedObject;
                arg = shipGroup.Name;
            }
            else if (_Game.SelectedObject is BuiltObjectList)
            {
                arg = TextResolver.GetText("selected ships");
            }
            else if (_Game.SelectedObject is Creature)
            {
                Creature creature = (Creature)_Game.SelectedObject;
                arg = creature.Name;
            }
            string_20 = string.Format(TextResolver.GetText("View locked on SELECTED"), arg);
        }

        private bool method_204(int int_64, int int_65)
        {
            if (int_64 == 0 && int_65 == 0)
            {
                string_20 = string.Empty;
            }
            else if (UhvLmNjli7 && _Game.SelectedObject != null)
            {
                method_203();
            }
            else
            {
                string_20 = string.Empty;
                int_13 += int_64;
                int_14 += int_65;
                if (double_0 > 30.0)
                {
                    method_149();
                }
                int_39 = 0;
                int_40 = 0;
                if (int_64 <= 0)
                {
                }
                bool_22 = true;
            }
            return bool_22;
        }

        public object[] method_205()
        {
            PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
            if (_Game != null && _Game.PlayerEmpire != null)
            {
                DistantWorlds.Types.EmpireList empireList = _Game.PlayerEmpire.DetermineEmpiresAtWarWith();
                for (int i = 0; i < empireList.Count; i++)
                {
                    prioritizedTargetList.AddRange(_Game.PlayerEmpire.IdentifyEmpireStrikePoints(empireList[i]));
                }
                for (int j = 0; j < _Game.PlayerEmpire.PirateRelations.Count; j++)
                {
                    PirateRelation pirateRelation = _Game.PlayerEmpire.PirateRelations[j];
                    if (pirateRelation == null || pirateRelation.Type != PirateRelationType.None || pirateRelation.OtherEmpire == null || pirateRelation.OtherEmpire == _Game.Galaxy.IndependentEmpire)
                    {
                        continue;
                    }
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    builtObjectList.AddRange(pirateRelation.OtherEmpire.BuiltObjects);
                    builtObjectList.AddRange(pirateRelation.OtherEmpire.PrivateBuiltObjects);
                    for (int k = 0; k < builtObjectList.Count; k++)
                    {
                        BuiltObject builtObject = builtObjectList[k];
                        if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == BuiltObjectRole.Base && (_Game.PlayerEmpire.KnownPirateBases.Contains(builtObject) || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject)))
                        {
                            double distance = 0.0;
                            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                            {
                                distance = _Game.Galaxy.CalculateDistance(_Game.PlayerEmpire.PirateEmpireBaseHabitat.Xpos, _Game.PlayerEmpire.PirateEmpireBaseHabitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                            }
                            else if (_Game.PlayerEmpire.Capital != null)
                            {
                                distance = _Game.Galaxy.CalculateDistance(_Game.PlayerEmpire.Capital.Xpos, _Game.PlayerEmpire.Capital.Ypos, builtObject.Xpos, builtObject.Ypos);
                            }
                            int priority = 1 + (int)(500000.0 / Galaxy.CalculateDistanceFactor(distance));
                            prioritizedTargetList.Add(new PrioritizedTarget(builtObject, priority));
                        }
                    }
                }
                prioritizedTargetList.Sort();
                prioritizedTargetList.Reverse();
            }
            return prioritizedTargetList.ToArray();
        }


  }

}