// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.SystemView
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class SystemView : GradientPanel
    {
        private Main main_0;

        private bool bool_0;

        private int int_0;

        private int int_1;

        private int int_2;

        private bool bool_1;

        private bool bool_2;

        private bool bool_3;

        private bool bool_4;

        private string string_0;

        private bool bool_5;

        private bool bool_6;

        private double double_0;

        private double double_1;

        private double double_2;

        private object object_0;

        private Bitmap bitmap_0;

        private Bitmap bitmap_1;

        private Bitmap bitmap_2;

        private Bitmap bitmap_3;

        private Bitmap bitmap_4;

        private Bitmap bitmap_5;

        private bool bool_7;

        private HabitatList habitatList_0;

        private Galaxy galaxy_0;

        public bool ShowFleetPostures
        {
            get
            {
                return bool_6;
            }
            set
            {
                bool_6 = value;
            }
        }

        public bool ShowBuiltObjects
        {
            get
            {
                return bool_7;
            }
            set
            {
                bool_7 = value;
            }
        }

        public HabitatList SelectedHabitats => habitatList_0;

        public void Ignite(Main parentForm, Bitmap nebulaeSource, Bitmap backgroundSource, Bitmap systemInfluenceImage, Galaxy galaxy, bool relativeToView, int solIndexStart, int scaleFactor, bool drawViewIndicator, bool erasePrevious, bool clearFirst, bool showIndicatorLines, string systemName)
        {
            SetFont(15.33f);
            Ignite(parentForm, nebulaeSource, backgroundSource, systemInfluenceImage, galaxy, relativeToView, solIndexStart, scaleFactor, drawViewIndicator, erasePrevious, clearFirst, showIndicatorLines, systemName, showNebulae: true);
        }

        public void Ignite(Main parentForm, Bitmap nebulaeSource, Bitmap backgroundSource, Bitmap systemInfluenceImage, Galaxy galaxy, bool relativeToView, int solIndexStart, int scaleFactor, bool drawViewIndicator, bool erasePrevious, bool clearFirst, bool showIndicatorLines, string systemName, bool showNebulae)
        {
            Ignite(parentForm, nebulaeSource, backgroundSource, systemInfluenceImage, galaxy, relativeToView, solIndexStart, scaleFactor, drawViewIndicator, erasePrevious, clearFirst, showIndicatorLines, systemName, showNebulae, showFleetPostures: false);
        }

        public void Ignite(Main parentForm, Bitmap nebulaeSource, Bitmap backgroundSource, Bitmap systemInfluenceImage, Galaxy galaxy, bool relativeToView, int solIndexStart, int scaleFactor, bool drawViewIndicator, bool erasePrevious, bool clearFirst, bool showIndicatorLines, string systemName, bool showNebulae, bool showFleetPostures)
        {
            main_0 = parentForm;
            bool_0 = relativeToView;
            int_0 = solIndexStart;
            int_2 = scaleFactor;
            bool_1 = drawViewIndicator;
            bool_2 = erasePrevious;
            bool_3 = clearFirst;
            bool_4 = showIndicatorLines;
            string_0 = systemName;
            bool_5 = showNebulae;
            bool_6 = showFleetPostures;
            if (bitmap_5 == null)
            {
                bitmap_5 = new Bitmap(systemInfluenceImage);
            }
            if (bitmap_0 == null)
            {
                bitmap_0 = main_0.PrecacheScaledBitmap(nebulaeSource, 500, 500, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
            }
            if (bitmap_1 == null)
            {
                bitmap_1 = main_0.PrecacheScaledBitmap(backgroundSource, 500, 500, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
            }
            RegenNebulae(main_0.double_0);
            galaxy_0 = galaxy;
            int_1 = galaxy_0.Habitats.Count - 1;
            for (int i = int_0 + 1; i < galaxy_0.Habitats.Count; i++)
            {
                if (galaxy_0.Habitats[i].Parent == null)
                {
                    int_1 = i - 1;
                    break;
                }
            }
            if (clearFirst)
            {
                Clear(CreateGraphics());
            }
            Invalidate();
        }

        public void ClearNebulae()
        {
            if (bitmap_2 != null)
            {
                bitmap_2.Dispose();
            }
            if (bitmap_3 != null)
            {
                bitmap_3.Dispose();
            }
            if (bitmap_4 != null)
            {
                bitmap_4.Dispose();
            }
            bitmap_2 = null;
            bitmap_3 = null;
            bitmap_4 = null;
            if (main_0 != null)
            {
                RegenNebulae(main_0.double_0);
            }
        }

        public void RegenNebulae(double zoomFactor)
        {
            Thread thread = new Thread(method_0);
            thread.Start(zoomFactor);
        }

        private void method_0(object object_1)
        {
            lock (object_0)
            {
                double num = (double)object_1;
                if (!bool_5 || galaxy_0 == null)
                {
                    return;
                }
                double num2 = 0.0;
                num2 = ((!(num < double_0)) ? Math.Min(double_2, Math.Max(double_1, num * 10.0)) : double_1);
                int num3 = (int)((double)Galaxy.SizeX / num2);
                int num4 = (int)((double)Galaxy.SizeY / num2);
                if (bitmap_2 == null || bitmap_2.Width != num3 || bitmap_2.Height != num4)
                {
                    if (bitmap_2 != null)
                    {
                        bitmap_2.Dispose();
                    }
                    bitmap_2 = method_3(num);
                }
                if (bitmap_3 == null || bitmap_3.Width != num3 || bitmap_3.Height != num4)
                {
                    if (bitmap_3 != null)
                    {
                        bitmap_3.Dispose();
                    }
                    bitmap_3 = method_2(num);
                }
                if (bitmap_4 == null || bitmap_4.Width != num3 || bitmap_4.Height != num4)
                {
                    if (bitmap_4 != null)
                    {
                        bitmap_4.Dispose();
                    }
                    bitmap_4 = method_1(num);
                }
            }
        }

        private Bitmap method_1(double double_3)
        {
            double num = 0.0;
            num = ((!(double_3 < double_0)) ? Math.Min(double_2, Math.Max(double_1, double_3 * 10.0)) : double_1);
            int val = (int)((double)Galaxy.SizeX / num);
            int val2 = (int)((double)Galaxy.SizeY / num);
            val = Math.Max(1, val);
            val2 = Math.Max(1, val2);
            Rectangle galaxySection = new Rectangle(0, 0, Galaxy.SizeX, Galaxy.SizeY);
            double systemInfluenceSizeFactor = 1.0 + double_3 / 3000.0;
            Bitmap bitmap = null;
            bitmap = ((!main_0.gameOptions_0.MapOverlayEmpireTerritory) ? EmpireTerritory.CalculateEmpireSystemTerritory(galaxy_0, galaxySection, val, val2, galaxy_0.PlayerEmpire, main_0._Game.GodMode, bitmap_5, systemInfluenceSizeFactor) : EmpireTerritory.CalculateEmpireTerritoryGrid(galaxy_0, galaxySection, val, val2, galaxy_0.PlayerEmpire, main_0._Game.GodMode));
            if (bitmap != null)
            {
                Bitmap bitmap2 = bitmap;
                bitmap = GraphicsHelper.TransparentImage(bitmap, 0.4f);
                bitmap2.Dispose();
            }
            return bitmap;
        }

        private Bitmap method_2(double double_3)
        {
            double num = 0.0;
            num = ((!(double_3 < double_0)) ? Math.Min(double_2, Math.Max(double_1, double_3 * 10.0)) : double_1);
            int val = (int)((double)Galaxy.SizeX / num);
            int val2 = (int)((double)Galaxy.SizeY / num);
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5] { 0f, 0f, 0f, 1f, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            val = Math.Max(1, val);
            val2 = Math.Max(1, val2);
            Bitmap bitmap = new Bitmap(val, val2, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Black);
                if (bitmap_1 != null)
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.SmoothingMode = SmoothingMode.None;
                    Rectangle destRect = new Rectangle(0, 0, val, val2);
                    Rectangle rectangle = new Rectangle(0, 0, bitmap_1.Width, bitmap_1.Height);
                    graphics.DrawImage(bitmap_1, destRect, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
                }
            }
            imageAttributes.Dispose();
            return bitmap;
        }

        private Bitmap method_3(double double_3)
        {
            double num = 0.0;
            num = ((!(double_3 < double_0)) ? Math.Min(double_2, Math.Max(double_1, double_3 * 10.0)) : double_1);
            int val = (int)((double)Galaxy.SizeX / num);
            int val2 = (int)((double)Galaxy.SizeY / num);
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5] { 0f, 0f, 0f, 1f, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            val = Math.Max(1, val);
            val2 = Math.Max(1, val2);
            Bitmap bitmap = new Bitmap(val, val2, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                if (bitmap_0 != null)
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.SmoothingMode = SmoothingMode.None;
                    Rectangle destRect = new Rectangle(0, 0, val, val2);
                    Rectangle rectangle = new Rectangle(0, 0, bitmap_0.Width, bitmap_0.Height);
                    graphics.DrawImage(bitmap_0, destRect, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
                }
            }
            imageAttributes.Dispose();
            return bitmap;
        }

        private void method_4()
        {
            if (galaxy_0 != null)
            {
                if (bitmap_4 != null)
                {
                    bitmap_4.Dispose();
                }
                if (main_0 != null)
                {
                    bitmap_4 = method_1(main_0.double_0);
                }
            }
        }

        public void SetSelectedHabitat(Habitat selectedHabitat)
        {
            method_4();
            habitatList_0 = null;
            if (selectedHabitat != null)
            {
                habitatList_0 = new HabitatList();
                habitatList_0.Add(selectedHabitat);
            }
        }

        public void SetSelectedHabitats(HabitatList selectedHabitats)
        {
            method_4();
            habitatList_0 = selectedHabitats;
        }

        public void Extinguish()
        {
            lock (object_0)
            {
                galaxy_0 = null;
                habitatList_0 = null;
                int_0 = 0;
                int_1 = 1;
                if (bitmap_2 != null)
                {
                    bitmap_2.Dispose();
                }
                if (bitmap_1 != null)
                {
                    bitmap_1.Dispose();
                }
                if (bitmap_0 != null)
                {
                    bitmap_0.Dispose();
                }
                if (bitmap_4 != null)
                {
                    bitmap_4.Dispose();
                }
                bitmap_2 = null;
                bitmap_1 = null;
                bitmap_0 = null;
                bitmap_4 = null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (galaxy_0 == null)
            {
                return;
            }
            if (bool_0)
            {
                if (main_0.double_0 < 100.0)
                {
                    method_5(bool_0, int_0, e.Graphics, int_2, bool_1, bool_2, bool_4, string_0);
                }
                else
                {
                    method_9(main_0.int_13, main_0.int_14, base.ClientRectangle.Width, base.ClientRectangle.Height, main_0.double_0, e.Graphics);
                }
            }
            else
            {
                method_5(bool_0, int_0, e.Graphics, int_2, bool_1, bool_2, bool_4, string_0);
            }
        }

        private void method_5(bool bool_8, int int_3, Graphics graphics_0, int int_4, bool bool_9, bool bool_10, bool bool_11, string string_1)
        {
            double num = 0.0;
            if (bool_8)
            {
                if (main_0.double_0 < 10.0)
                {
                    num = int_4;
                }
                else
                {
                    int num2 = 100 - int_4;
                    num = main_0.double_0 * 10.0 - (double)num2;
                }
            }
            else
            {
                num = int_4;
            }
            DrawPanelBackground(graphics_0);
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            SystemVisibilityStatus systemVisibilityStatus = main_0._Game.PlayerEmpire.CheckSystemVisibilityStatus(galaxy_0.Habitats[int_3].SystemIndex);
            if (main_0._Game.GodMode)
            {
                systemVisibilityStatus = SystemVisibilityStatus.Visible;
            }
            int num3 = 0;
            int num4 = 0;
            if (bool_8)
            {
                num3 = main_0.int_13;
                num4 = main_0.int_14;
            }
            else
            {
                num3 = (int)galaxy_0.Habitats[int_3].Xpos;
                num4 = (int)galaxy_0.Habitats[int_3].Ypos;
            }
            if (bool_11 && main_0.habitat_8 != null)
            {
                int num5 = ((int)main_0.habitat_8.Xpos - (int)main_0.habitat_7.Xpos) / (int)num + base.Width / 2 + 1;
                int num6 = ((int)main_0.habitat_8.Ypos - (int)main_0.habitat_7.Ypos) / (int)num + base.Height / 2 + 1;
                graphics_0.DrawLine(main_0.pen_2, num5, 0, num5, base.Height);
                graphics_0.DrawLine(main_0.pen_2, 0, num6, base.Width, num6);
            }
            if (int_3 >= 0)
            {
                for (int i = int_3; i <= int_1; i++)
                {
                    if (i > int_3 && systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                    {
                        continue;
                    }
                    if (i < galaxy_0.Habitats.Count)
                    {
                        Habitat habitat = galaxy_0.Habitats[i];
                        if (habitat == null)
                        {
                            continue;
                        }
                        int num7 = ((habitat.Category == HabitatCategoryType.GasCloud || habitat.Type == HabitatType.BlackHole) ? ((int)((double)habitat.Diameter / num)) : ((habitat.Diameter > 1250) ? 11 : ((habitat.Diameter > 1000) ? 9 : ((habitat.Diameter > 750) ? 7 : ((habitat.Diameter > 300) ? 6 : ((habitat.Diameter >= 180) ? 5 : ((habitat.Diameter >= 80) ? 4 : ((habitat.Diameter < 30) ? 2 : 3))))))));
                        int num8 = (int)(habitat.Xpos - (double)num3) / (int)num + base.ClientRectangle.Width / 2;
                        int num9 = (int)(habitat.Ypos - (double)num4) / (int)num + base.ClientRectangle.Height / 2;
                        if (i == int_3)
                        {
                            if (systemVisibilityStatus != SystemVisibilityStatus.Unexplored)
                            {
                                for (int j = int_3 + 1; j <= int_1; j++)
                                {
                                    if (j < galaxy_0.Habitats.Count)
                                    {
                                        Habitat habitat2 = galaxy_0.Habitats[j];
                                        if (habitat2.Category == HabitatCategoryType.Planet)
                                        {
                                            int num10 = habitat2.OrbitDistance / (int)num;
                                            graphics_0.DrawEllipse(main_0.pen_1, num8 - num10, num9 - num10, num10 * 2, num10 * 2);
                                        }
                                        continue;
                                    }
                                    int_1 = galaxy_0.Habitats.Count - 1;
                                    break;
                                }
                            }
                            if (bool_9)
                            {
                                int num11 = base.ClientRectangle.Width / 2;
                                int num12 = base.ClientRectangle.Height / 2;
                                int num13 = (int)((double)main_0.cxjxlkqlKe.ClientRectangle.Width / (num / main_0.double_0));
                                int num14 = (int)((double)main_0.cxjxlkqlKe.ClientRectangle.Height / (num / main_0.double_0));
                                graphics_0.SmoothingMode = SmoothingMode.None;
                                Rectangle rect = new Rectangle(num11 - num13 / 2, num12 - num14 / 2, num13, num14);
                                graphics_0.FillRectangle(new SolidBrush(Color.FromArgb(48, main_0.color_7)), rect);
                                graphics_0.DrawRectangle(main_0.pen_2, rect);
                                graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
                            }
                        }
                        Rectangle rectangle = new Rectangle(num8 - num7 / 2, num9 - num7 / 2, num7, num7);
                        if (bool_10)
                        {
                            graphics_0.FillEllipse(main_0.solidBrush_0, main_0.rectangle_1[i - int_3]);
                            main_0.rectangle_1[i - int_3] = rectangle;
                        }
                        if (habitatList_0 != null)
                        {
                            SolidBrush solidBrush = null;
                            if (habitatList_0.Contains(habitat))
                            {
                                rectangle = new Rectangle(num8 - 2, num9 - 2, 4, 4);
                                solidBrush = new SolidBrush(Color.Yellow);
                            }
                            else
                            {
                                solidBrush = new SolidBrush(Color.FromArgb(80, 80, 80));
                            }
                            graphics_0.FillEllipse(solidBrush, rectangle);
                            solidBrush.Dispose();
                            continue;
                        }
                        if (habitat.Empire != null && habitat.Empire != galaxy_0.IndependentEmpire)
                        {
                            Rectangle rect2 = new Rectangle(rectangle.X - 2, rectangle.Y - 2, rectangle.Width + 4, rectangle.Height + 4);
                            using Pen pen = new Pen(habitat.Empire.MainColor, 2f);
                            graphics_0.DrawEllipse(pen, rect2);
                        }
                        switch (habitat.Type)
                        {
                            case HabitatType.MainSequence:
                                graphics_0.FillEllipse(main_0.solidBrush_17, rectangle);
                                break;
                            case HabitatType.RedGiant:
                                graphics_0.FillEllipse(main_0.solidBrush_18, rectangle);
                                break;
                            case HabitatType.SuperGiant:
                                graphics_0.FillEllipse(main_0.solidBrush_19, rectangle);
                                break;
                            case HabitatType.WhiteDwarf:
                                graphics_0.FillEllipse(main_0.solidBrush_20, rectangle);
                                break;
                            case HabitatType.Neutron:
                                graphics_0.FillEllipse(main_0.solidBrush_21, rectangle);
                                break;
                            case HabitatType.BlackHole:
                                graphics_0.FillEllipse(main_0.solidBrush_23, rectangle);
                                break;
                            case HabitatType.SuperNova:
                                graphics_0.FillEllipse(main_0.solidBrush_22, rectangle);
                                break;
                            case HabitatType.Volcanic:
                                graphics_0.FillEllipse(main_0.solidBrush_8, rectangle);
                                break;
                            case HabitatType.Desert:
                                graphics_0.FillEllipse(main_0.solidBrush_9, rectangle);
                                break;
                            case HabitatType.MarshySwamp:
                                graphics_0.FillEllipse(main_0.solidBrush_10, rectangle);
                                break;
                            case HabitatType.Continental:
                                graphics_0.FillEllipse(main_0.solidBrush_11, rectangle);
                                break;
                            case HabitatType.Ocean:
                                graphics_0.FillEllipse(main_0.solidBrush_12, rectangle);
                                break;
                            case HabitatType.Ice:
                                if (habitat.Category == HabitatCategoryType.Asteroid)
                                {
                                    graphics_0.FillEllipse(main_0.solidBrush_7, rectangle);
                                }
                                else
                                {
                                    graphics_0.FillEllipse(main_0.solidBrush_13, rectangle);
                                }
                                break;
                            case HabitatType.GasGiant:
                                graphics_0.FillEllipse(main_0.solidBrush_14, rectangle);
                                break;
                            case HabitatType.FrozenGasGiant:
                                graphics_0.FillEllipse(main_0.solidBrush_15, rectangle);
                                break;
                            case HabitatType.Hydrogen:
                            case HabitatType.Helium:
                            case HabitatType.Argon:
                            case HabitatType.Ammonia:
                            case HabitatType.CarbonDioxide:
                            case HabitatType.Oxygen:
                            case HabitatType.NitrogenOxygen:
                            case HabitatType.Chlorine:
                                {
                                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(96, main_0.solidBrush_24.Color.R, main_0.solidBrush_24.Color.G, main_0.solidBrush_24.Color.B)))
                                    {
                                        graphics_0.FillEllipse(brush, rectangle);
                                    }
                                    break;
                                }
                            case HabitatType.BarrenRock:
                            case HabitatType.Metal:
                                graphics_0.FillEllipse(main_0.solidBrush_7, rectangle);
                                break;
                        }
                        continue;
                    }
                    int_1 = galaxy_0.Habitats.Count - 1;
                    return;
                }
            }
            if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored && galaxy_0.Habitats[int_3].Category != HabitatCategoryType.GasCloud)
            {
                SizeF sizeF = graphics_0.MeasureString("(" + TextResolver.GetText("Not Explored") + ")", main_0.font_2, 300, StringFormat.GenericTypographic);
                int num15 = (int)sizeF.Width;
                int num16 = (int)sizeF.Height;
                Point point = new Point(base.ClientRectangle.Width / 2 - num15 / 2, base.ClientRectangle.Height / 3 * 2 - num16 / 2);
                graphics_0.DrawString(point: new Point(point.X + 1, point.Y + 1), s: "(" + TextResolver.GetText("Not Explored") + ")", font: main_0.font_2, brush: new SolidBrush(Color.Black));
                graphics_0.DrawString("(" + TextResolver.GetText("Not Explored") + ")", main_0.font_2, new SolidBrush(Color.FromArgb(128, 128, 128, 128)), point);
            }
            if (bool_7)
            {
                method_7(graphics_0, num, main_0._Game.PlayerEmpire);
                Creature[] array = null;
                if (galaxy_0.Habitats[int_3].SystemIndex < galaxy_0.Systems.Count)
                {
                    SystemInfo systemInfo = galaxy_0.Systems[galaxy_0.Habitats[int_3].SystemIndex];
                    if (systemInfo != null)
                    {
                        array = ListHelper.ToArrayThreadSafe(systemInfo.Creatures);
                    }
                }
                if (array == null)
                {
                    GalaxyLocationList galaxyLocationList = galaxy_0.DetermineGalaxyLocationsAtPoint(main_0.int_13, main_0.int_14, GalaxyLocationType.RestrictedArea);
                    if (galaxyLocationList != null && galaxyLocationList.Count == 1 && galaxyLocationList[0].RelatedCreatures != null && galaxyLocationList[0].RelatedCreatures.Count > 0)
                    {
                        array = ListHelper.ToArrayThreadSafe(galaxyLocationList[0].RelatedCreatures);
                    }
                }
                if (array != null)
                {
                    method_6(graphics_0, num, main_0._Game.PlayerEmpire, array);
                }
            }
            if (!string.IsNullOrEmpty(string_1) && systemVisibilityStatus != SystemVisibilityStatus.Unexplored)
            {
                graphics_0.DrawString(string_1, main_0.font_2, new SolidBrush(Color.Black), new PointF(9f, 9f));
                graphics_0.DrawString(string_1, main_0.font_2, new SolidBrush(Color.White), new PointF(8f, 8f));
            }
        }

        private void method_6(Graphics graphics_0, double double_3, Empire empire_0, Creature[] creature_0)
        {
            double maxWidth = 10.0;
            double num = main_0.CalculateCreatureZoomFactor(main_0.double_0, out maxWidth);
            num = double_3;
            maxWidth /= 10.0;
            int num2 = (int)((double)base.ClientRectangle.Width * double_3);
            int num3 = (int)((double)base.ClientRectangle.Height * double_3);
            int num4 = main_0.int_13 - num2 / 2;
            int num5 = main_0.int_13 + num2 / 2;
            int num6 = main_0.int_14 - num3 / 2;
            int num7 = main_0.int_14 + num3 / 2;
            foreach (Creature creature in creature_0)
            {
                if (creature.HasBeenDestroyed)
                {
                    continue;
                }
                int num8 = 50;
                int num9 = 50;
                num8 = (int)(50.0 / num);
                num9 = (int)(50.0 / num);
                num8 = Math.Min(num8, (int)maxWidth);
                num9 = Math.Min(num9, (int)maxWidth);
                num8 = Math.Max(7, num8);
                num9 = Math.Max(7, num9);
                if (creature.Xpos >= (double)num4 && creature.Xpos <= (double)num5 && creature.Ypos >= (double)num6 && creature.Ypos <= (double)num7 && (main_0._Game.GodMode || empire_0.IsObjectVisibleToThisEmpire(creature)))
                {
                    float num10 = (float)((creature.Xpos - (double)num4) / double_3);
                    float num11 = (float)((creature.Ypos - (double)num6) / double_3);
                    float num12 = (float)num8 / 2f;
                    float num13 = (float)num9 / 2f;
                    Color color = Color.FromArgb(192, 160, 64, 32);
                    using Pen pen = new Pen(color, 2f);
                    graphics_0.DrawLine(pen, num10 - num12, num11 - num13, num10 + num12, num11 + num13);
                    graphics_0.DrawLine(pen, num10 + num12, num11 - num13, num10 - num12, num11 + num13);
                }
            }
        }

        private void method_7(Graphics graphics_0, double double_3, Empire empire_0)
        {
            int num = (int)((double)base.ClientRectangle.Width * main_0.double_0);
            num += Galaxy.MaxSolarSystemSize * 2;
            BuiltObjectList builtObjectsAtLocation = galaxy_0.GetBuiltObjectsAtLocation(main_0.int_13, main_0.int_14, num);
            double maxWidth = 10.0;
            double num2 = main_0.CalculateShipZoomFactor(main_0.double_0, out maxWidth);
            num2 = double_3;
            maxWidth /= 10.0;
            int num3 = (int)((double)base.ClientRectangle.Width * double_3);
            int num4 = (int)((double)base.ClientRectangle.Height * double_3);
            int num5 = main_0.int_13 - num3 / 2;
            int num6 = main_0.int_13 + num3 / 2;
            int num7 = main_0.int_14 - num4 / 2;
            int num8 = main_0.int_14 + num4 / 2;
            for (int i = 0; i < builtObjectsAtLocation.Count; i++)
            {
                BuiltObject builtObject = builtObjectsAtLocation[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed)
                {
                    int num9 = 50;
                    int num10 = 50;
                    num9 = (int)(50.0 / num2);
                    num10 = (int)(50.0 / num2);
                    num9 = Math.Min(num9, (int)maxWidth);
                    num10 = Math.Min(num10, (int)maxWidth);
                    num9 = Math.Max(5, num9);
                    num10 = Math.Max(5, num10);
                    if (builtObject.Xpos >= (double)num5 && builtObject.Xpos <= (double)num6 && builtObject.Ypos >= (double)num7 && builtObject.Ypos <= (double)num8 && (main_0._Game.GodMode || empire_0.IsObjectVisibleToThisEmpire(builtObject)))
                    {
                        float num11 = (float)((builtObject.Xpos - (double)num5) / double_3);
                        float num12 = (float)((builtObject.Ypos - (double)num7) / double_3);
                        Color color = main_0.cxjxlkqlKe.ResolveShipSymbolColor(builtObject);
                        color = Color.FromArgb(Math.Min(255, color.A + 48), color.R, color.G, color.B);
                        main_0.cxjxlkqlKe.DrawShipSymbol(graphics_0, builtObject, color, num11, num12, num9, num10, num9, num10, fillInterior: true, double_3);
                    }
                }
            }
        }

        private void method_8(Graphics graphics_0, Empire empire_0, double double_3, int int_3, int int_4, int int_5, int int_6)
        {
            Color color = Color.FromArgb(48, 255, 0, 0);
            Color color2 = Color.FromArgb(48, 0, 0, 255);
            using SolidBrush brush = new SolidBrush(color);
            using Pen pen = new Pen(color);
            pen.Width = 1f;
            using SolidBrush brush2 = new SolidBrush(color2);
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
                        num = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_3);
                    }
                    float num2 = (float)((shipGroup.AttackPoint.Xpos - (double)int_5) / double_3);
                    float num3 = (float)((shipGroup.AttackPoint.Ypos - (double)int_6) / double_3);
                    if (num > 0f && num2 + num > 0f && num2 - num < (float)int_3 && num3 + num > 0f && num3 - num < (float)int_4)
                    {
                        RectangleF rect = new RectangleF(num2 - num, num3 - num, num * 2f, num * 2f);
                        graphics_0.FillEllipse(brush, rect);
                        graphics_0.DrawEllipse(pen, rect);
                    }
                    if (shipGroup.GatherPoint != null)
                    {
                        float x = (float)((shipGroup.GatherPoint.Xpos - (double)int_5) / double_3);
                        float y = (float)((shipGroup.GatherPoint.Ypos - (double)int_6) / double_3);
                        using Pen pen3 = new Pen(color);
                        pen3.Width = 1f;
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
                        num4 = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_3);
                    }
                    float num5 = (float)((shipGroup.GatherPoint.Xpos - (double)int_5) / double_3);
                    float num6 = (float)((shipGroup.GatherPoint.Ypos - (double)int_6) / double_3);
                    if (num4 > 0f && num5 + num4 > 0f && num5 - num4 < (float)int_3 && num6 + num4 > 0f && num6 - num4 < (float)int_4)
                    {
                        RectangleF rect2 = new RectangleF(num5 - num4, num6 - num4, num4 * 2f, num4 * 2f);
                        graphics_0.FillEllipse(brush2, rect2);
                        graphics_0.DrawEllipse(pen2, rect2);
                    }
                }
            }
        }

        private void method_9(int int_3, int int_4, int int_5, int int_6, double double_3, Graphics graphics_0)
        {
            if (galaxy_0 == null)
            {
                return;
            }
            double num = 0.0;
            num = ((!(double_3 < double_0)) ? Math.Min(double_2, Math.Max(double_1, double_3 * 10.0)) : double_1);
            double num2 = (double)int_5 * num;
            double num3 = (double)int_6 * num;
            int int_7 = 0;
            int int_8 = 0;
            int int_9 = 0;
            int int_10 = 0;
            method_11(int_3, int_4, num2, num3, ref int_7, ref int_8, ref int_9, ref int_10);
            if (bool_5)
            {
                graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
                graphics_0.InterpolationMode = InterpolationMode.Low;
                graphics_0.SmoothingMode = SmoothingMode.None;
                int num4 = (int)((0.0 - (double)int_7) / num);
                int num5 = (int)((0.0 - (double)int_9) / num);
                lock (object_0)
                {
                    if (bitmap_2 != null && bitmap_2.PixelFormat != 0)
                    {
                        new Rectangle(0, 0, bitmap_2.Width, bitmap_2.Height);
                        Rectangle rect = new Rectangle(num4, num5, bitmap_2.Width, bitmap_2.Height);
                        if (bitmap_3 != null && bitmap_3.PixelFormat != 0)
                        {
                            graphics_0.DrawImageUnscaledAndClipped(bitmap_3, rect);
                        }
                        graphics_0.DrawImageUnscaledAndClipped(bitmap_2, rect);
                        if (bitmap_4 != null && bitmap_4.PixelFormat != 0)
                        {
                            graphics_0.DrawImageUnscaledAndClipped(bitmap_4, rect);
                        }
                    }
                }
                graphics_0.CompositingQuality = CompositingQuality.HighQuality;
                graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            }
            int num6 = Galaxy.SectorSize * 8;
            if (int_7 < num6 * -1)
            {
                int_3 = (int)num2 / 2 - num6;
                method_11(int_3, int_4, num2, num3, ref int_7, ref int_8, ref int_9, ref int_10);
            }
            if (int_8 > Galaxy.SizeX + num6)
            {
                int_3 = Galaxy.SizeX + num6 - (int)num2 / 2;
                method_11(int_3, int_4, num2, num3, ref int_7, ref int_8, ref int_9, ref int_10);
            }
            if (int_9 < num6 * -1)
            {
                int_4 = (int)num3 / 2 - num6;
                method_11(int_3, int_4, num2, num3, ref int_7, ref int_8, ref int_9, ref int_10);
            }
            if (int_10 > Galaxy.SizeY + num6)
            {
                int_4 = Galaxy.SizeY + num6 - (int)num3 / 2;
                method_11(int_3, int_4, num2, num3, ref int_7, ref int_8, ref int_9, ref int_10);
            }
            int num7 = galaxy_0.ResolveSector(int_7, int_4).X;
            int num8 = galaxy_0.ResolveSector(int_8, int_4).X;
            int num9 = galaxy_0.ResolveSector(int_3, int_9).Y;
            int num10 = galaxy_0.ResolveSector(int_3, int_10).Y;
            float num11 = 0f;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            num11 = ((!(main_0.double_0 < 6000.0)) ? 2f : 2f);
            bool flag = false;
            bool flag2 = false;
            int y = 0;
            int y2 = int_6;
            if (num9 <= 1)
            {
                y = (int)((double)(num9 * Galaxy.SectorSize - int_9) / num);
            }
            if (num10 >= Galaxy.SectorMaxY - 1)
            {
                y2 = (int)((double)((num10 + 1) * Galaxy.SectorSize - int_9) / num);
                flag2 = true;
            }
            int x = 0;
            int x2 = int_5;
            if (num7 <= 1)
            {
                x = (int)((double)(num7 * Galaxy.SectorSize - int_7) / num);
            }
            if (num8 >= Galaxy.SectorMaxY - 1)
            {
                x2 = (int)((double)((num8 + 1) * Galaxy.SectorSize - int_7) / num);
                flag = true;
            }
            graphics_0.SmoothingMode = SmoothingMode.None;
            for (int i = num7; i <= num8; i++)
            {
                int num12 = (int)((double)(i * Galaxy.SectorSize - int_7) / num);
                graphics_0.DrawLine(main_0.pen_1, num12, y, num12, y2);
            }
            if (flag)
            {
                int num13 = (int)((double)((num8 + 1) * Galaxy.SectorSize - int_7) / num);
                graphics_0.DrawLine(main_0.pen_1, num13, y, num13, y2);
            }
            for (int j = num9; j <= num10; j++)
            {
                int num14 = (int)((double)(j * Galaxy.SectorSize - int_9) / num);
                graphics_0.DrawLine(main_0.pen_1, x, num14, x2, num14);
            }
            if (flag2)
            {
                int num15 = (int)((double)((num10 + 1) * Galaxy.SectorSize - int_9) / num);
                graphics_0.DrawLine(main_0.pen_1, x, num15, x2, num15);
            }
            int num16 = int_5 / 2;
            int num17 = int_6 / 2;
            int num18 = (int)((double)main_0.cxjxlkqlKe.ClientRectangle.Width / (num / main_0.double_0));
            int num19 = (int)((double)main_0.cxjxlkqlKe.ClientRectangle.Height / (num / main_0.double_0));
            Rectangle rect2 = new Rectangle(num16 - num18 / 2, num17 - num19 / 2, num18, num19);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(24, main_0.color_7)))
            {
                graphics_0.FillRectangle(brush, rect2);
                graphics_0.DrawRectangle(main_0.pen_2, rect2);
            }
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            Empire playerEmpire = main_0._Game.PlayerEmpire;
            if (playerEmpire == null)
            {
                return;
            }
            if (bool_6)
            {
                method_8(graphics_0, playerEmpire, num, int_5, int_6, int_7, int_9);
            }
            for (int k = 0; k < galaxy_0.Systems.Count; k++)
            {
                SystemInfo systemInfo = galaxy_0.Systems[k];
                if (systemInfo == null || systemInfo.Sector == null || systemInfo.SystemStar == null || systemInfo.Sector.X < num7 || systemInfo.Sector.X > num8 || systemInfo.Sector.Y < num9 || systemInfo.Sector.Y > num10)
                {
                    continue;
                }
                float num20 = (float)((systemInfo.SystemStar.Xpos - (double)int_7) / num);
                float num21 = (float)((systemInfo.SystemStar.Ypos - (double)int_9) / num);
                if (!(num20 >= 0f) || !(num20 <= (float)int_5) || !(num21 >= 0f) || !(num21 <= (float)int_6))
                {
                    continue;
                }
                SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Unexplored;
                if (playerEmpire != null)
                {
                    systemVisibilityStatus = playerEmpire.CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
                }
                if (systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                {
                    float num22 = 0f;
                    float num23 = 0f;
                    HabitatList linkSystemStars = systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars;
                    if (linkSystemStars != null && linkSystemStars.Count > 0)
                    {
                        for (int l = 0; l < linkSystemStars.Count; l++)
                        {
                            Habitat habitat = linkSystemStars[l];
                            if (habitat == null)
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus2 = playerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                            if (systemVisibilityStatus2 == SystemVisibilityStatus.Visible || systemVisibilityStatus2 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                            {
                                using Pen pen = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 1f);
                                num22 = (float)((habitat.Xpos - (double)int_7) / num);
                                num23 = (float)((habitat.Ypos - (double)int_9) / num);
                                pen.DashPattern = new float[2] { 2f, 1.5f };
                                graphics_0.DrawLine(pen, num20, num21, num22, num23);
                            }
                        }
                    }
                    if (systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars != null && systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count > 0)
                    {
                        for (int m = 0; m < systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; m++)
                        {
                            Habitat habitat2 = systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[m];
                            if (linkSystemStars.Contains(habitat2) || habitat2 == null)
                            {
                                continue;
                            }
                            SystemInfo systemInfo2 = galaxy_0.Systems[habitat2.SystemIndex];
                            if (systemInfo2.Sector.X >= num7 && systemInfo2.Sector.X <= num8 && systemInfo2.Sector.Y >= num9 && systemInfo2.Sector.Y <= num10)
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus3 = playerEmpire.CheckSystemVisibilityStatus(habitat2.SystemIndex);
                            if (systemVisibilityStatus3 == SystemVisibilityStatus.Visible || systemVisibilityStatus3 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                            {
                                using Pen pen2 = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 1f);
                                num22 = (float)((habitat2.Xpos - (double)int_7) / num);
                                num23 = (float)((habitat2.Ypos - (double)int_9) / num);
                                pen2.DashPattern = new float[2] { 2f, 1.5f };
                                graphics_0.DrawLine(pen2, num20, num21, num22, num23);
                            }
                        }
                    }
                    if (systemInfo.OtherEmpires != null)
                    {
                        for (int n = 0; n < systemInfo.OtherEmpires.Count; n++)
                        {
                            EmpireSystemSummary empireSystemSummary = systemInfo.OtherEmpires[n];
                            if (empireSystemSummary == null)
                            {
                                continue;
                            }
                            Empire empire = empireSystemSummary.Empire;
                            if (empire == null)
                            {
                                continue;
                            }
                            linkSystemStars = empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars;
                            if (linkSystemStars.Count > 0)
                            {
                                for (int num24 = 0; num24 < linkSystemStars.Count; num24++)
                                {
                                    Habitat habitat3 = linkSystemStars[num24];
                                    if (habitat3 == null)
                                    {
                                        continue;
                                    }
                                    SystemVisibilityStatus systemVisibilityStatus4 = playerEmpire.CheckSystemVisibilityStatus(habitat3.SystemIndex);
                                    if (systemVisibilityStatus4 == SystemVisibilityStatus.Visible || systemVisibilityStatus4 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                    {
                                        using Pen pen3 = new Pen(empire.MainColor, 1f);
                                        num22 = (float)((habitat3.Xpos - (double)int_7) / num);
                                        num23 = (float)((habitat3.Ypos - (double)int_9) / num);
                                        pen3.DashPattern = new float[2] { 2f, 1.5f };
                                        graphics_0.DrawLine(pen3, num20, num21, num22, num23);
                                    }
                                }
                            }
                            if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars == null || empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count <= 0)
                            {
                                continue;
                            }
                            for (int num25 = 0; num25 < empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; num25++)
                            {
                                Habitat habitat4 = empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[num25];
                                if (linkSystemStars.Contains(habitat4) || habitat4 == null)
                                {
                                    continue;
                                }
                                SystemInfo systemInfo3 = galaxy_0.Systems[habitat4.SystemIndex];
                                if (systemInfo3 == null || systemInfo3.Sector == null || (systemInfo3.Sector.X >= num7 && systemInfo3.Sector.X <= num8 && systemInfo3.Sector.Y >= num9 && systemInfo3.Sector.Y <= num10))
                                {
                                    continue;
                                }
                                SystemVisibilityStatus systemVisibilityStatus5 = playerEmpire.CheckSystemVisibilityStatus(habitat4.SystemIndex);
                                if (systemVisibilityStatus5 == SystemVisibilityStatus.Visible || systemVisibilityStatus5 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                {
                                    using Pen pen4 = new Pen(empire.MainColor, 1f);
                                    num22 = (float)((habitat4.Xpos - (double)int_7) / num);
                                    num23 = (float)((habitat4.Ypos - (double)int_9) / num);
                                    pen4.DashPattern = new float[2] { 2f, 1.5f };
                                    graphics_0.DrawLine(pen4, num20, num21, num22, num23);
                                }
                            }
                        }
                    }
                    if (systemInfo != null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null)
                    {
                        RectangleF rect3 = new RectangleF(num20 - (num11 + 4f) / 2f, num21 - (num11 + 4f) / 2f, num11 + 4f, num11 + 4f);
                        using Pen pen5 = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 2f);
                        if (systemInfo.OtherEmpires != null && systemInfo.OtherEmpires.Count > 0)
                        {
                            pen5.DashStyle = DashStyle.Dash;
                        }
                        graphics_0.DrawEllipse(pen5, rect3);
                    }
                }
                using Brush brush2 = method_10(systemInfo);
                RectangleF rect4 = new RectangleF(num20 - num11 / 2f, num21 - num11 / 2f, num11, num11);
                graphics_0.FillEllipse(brush2, rect4);
            }
        }

        private Brush method_10(SystemInfo systemInfo_0)
        {
            Brush result = null;
            if (systemInfo_0.SystemStar.Category == HabitatCategoryType.Star)
            {
                switch (systemInfo_0.SystemStar.Type)
                {
                    case HabitatType.MainSequence:
                        result = new SolidBrush(main_0.solidBrush_17.Color);
                        break;
                    case HabitatType.RedGiant:
                        result = new SolidBrush(main_0.solidBrush_18.Color);
                        break;
                    case HabitatType.SuperGiant:
                        result = new SolidBrush(main_0.solidBrush_19.Color);
                        break;
                    case HabitatType.WhiteDwarf:
                        result = new SolidBrush(main_0.solidBrush_20.Color);
                        break;
                    case HabitatType.Neutron:
                        result = new SolidBrush(main_0.solidBrush_21.Color);
                        break;
                    case HabitatType.BlackHole:
                        result = new SolidBrush(main_0.solidBrush_23.Color);
                        break;
                    case HabitatType.SuperNova:
                        result = new SolidBrush(main_0.solidBrush_22.Color);
                        break;
                }
            }
            else if (systemInfo_0.SystemStar.Category == HabitatCategoryType.GasCloud)
            {
                result = new SolidBrush(main_0.solidBrush_24.Color);
            }
            return result;
        }

        private void method_11(int int_3, int int_4, double double_3, double double_4, ref int int_5, ref int int_6, ref int int_7, ref int int_8)
        {
            int_5 = (int)((double)int_3 - double_3 / 2.0);
            int_6 = (int)((double)int_3 + double_3 / 2.0);
            int_7 = (int)((double)int_4 - double_4 / 2.0);
            int_8 = (int)((double)int_4 + double_4 / 2.0);
        }

        public SystemView()
        {
            Class7.VEFSJNszvZKMZ();
            bool_5 = true;
            double_0 = 2500.0;
            double_1 = 25000.0;
            double_2 = 75000.0;
            object_0 = new object();
        }
    }
}
