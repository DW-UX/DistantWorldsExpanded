// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GalaxyMap
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class GalaxyMap : GradientPanel
    {
        private Main main_0;

        private Galaxy galaxy_0;

        private HabitatList habitatList_0;

        private List<Point> list_0;

        private double double_0;

        private double double_1;

        private double double_2;

        private double double_3;

        private int int_0;

        private int int_1;

        private double double_4;

        private int int_2;

        private int int_3;

        private bool bool_0;

        private bool bool_1;

        private bool bool_2;

        private Font font_0;

        private SolidBrush solidBrush_0;

        private Bitmap bitmap_0;

        private Bitmap bitmap_1;

        private Bitmap bitmap_2;

        private Bitmap bitmap_3;

        public bool ShowFleetPostures
        {
            get
            {
                return bool_1;
            }
            set
            {
                bool_1 = value;
            }
        }

        public bool ShowEmpireTerritory
        {
            get
            {
                return bool_2;
            }
            set
            {
                bool_2 = value;
            }
        }

        public HabitatList SelectedSystems => habitatList_0;

        public List<Point> SelectedLocations => list_0;

        public void Ignite(Main parentForm, Galaxy galaxy, Bitmap systemInfluenceImage, int starViewMode, int positionalViewMode)
        {
            SetFont(7f);
            font_0 = _FontCache.GenerateFont(7f, isBold: false);
            Ignite(parentForm, galaxy, systemInfluenceImage, starViewMode, positionalViewMode, showNebulae: true);
        }

        public void Ignite(Main parentForm, Galaxy galaxy, Bitmap systemInfluenceImage, int starViewMode, int positionalViewMode, bool showNebulae)
        {
            Ignite(parentForm, galaxy, systemInfluenceImage, starViewMode, positionalViewMode, showNebulae, showFleetPostures: false);
        }

        public void Ignite(Main parentForm, Galaxy galaxy, Bitmap systemInfluenceImage, int starViewMode, int positionalViewMode, bool showNebulae, bool showFleetPostures)
        {
            main_0 = parentForm;
            galaxy_0 = galaxy;
            int_0 = Galaxy.SizeX / 2;
            int_1 = Galaxy.SizeY / 2;
            double_4 = Galaxy.SizeX / base.ClientRectangle.Width;
            int_2 = starViewMode;
            int_3 = positionalViewMode;
            bool_0 = showNebulae;
            bool_1 = showFleetPostures;
            bitmap_3 = new Bitmap(systemInfluenceImage);
            if (bitmap_0 != null)
            {
                bitmap_0.Dispose();
            }
            if (bitmap_1 != null)
            {
                bitmap_1.Dispose();
            }
            if (bitmap_2 != null)
            {
                bitmap_2.Dispose();
            }
            bitmap_0 = method_1(main_0.bitmap_182);
            bitmap_1 = method_1(main_0.bitmap_176);
            bitmap_2 = method_0();
            solidBrush_0 = new SolidBrush(main_0.pen_1.Color);
            Invalidate();
        }

        public void ClearData()
        {
            ClearLocations();
            galaxy_0 = null;
        }

        private Bitmap method_0()
        {
            int val = Math.Max(500, base.Width);
            int val2 = Math.Max(500, base.Height);
            val = Math.Max(1, val);
            val2 = Math.Max(1, val2);
            Rectangle galaxySection = new Rectangle(0, 0, Galaxy.SizeX, Galaxy.SizeY);
            double systemInfluenceSizeFactor = 1.0 + double_4 / 20000.0;
            Bitmap bitmap = null;
            if (galaxy_0 != null && galaxy_0.PlayerEmpire != null && main_0 != null && main_0._Game != null)
            {
                bitmap = ((bool_2 || main_0.gameOptions_0.MapOverlayEmpireTerritory) ? EmpireTerritory.CalculateEmpireTerritoryGrid(galaxy_0, galaxySection, val, val2, galaxy_0.PlayerEmpire, main_0._Game.GodMode) : EmpireTerritory.CalculateEmpireSystemTerritory(galaxy_0, galaxySection, val, val2, galaxy_0.PlayerEmpire, main_0._Game.GodMode, bitmap_3, systemInfluenceSizeFactor));
            }
            if (bitmap != null)
            {
                bitmap = GraphicsHelper.TransparentImage(bitmap, 0.4f);
            }
            return bitmap;
        }

        private Bitmap method_1(Bitmap bitmap_4)
        {
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
            int num = Math.Max(500, base.Width);
            int num2 = Math.Max(500, base.Height);
            Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle destRect = new Rectangle(0, 0, num, num2);
            Rectangle rectangle = new Rectangle(0, 0, bitmap_4.Width, bitmap_4.Height);
            graphics.DrawImage(bitmap_4, destRect, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
            return bitmap;
        }

        private void method_2()
        {
            if (bitmap_2 != null)
            {
                bitmap_2.Dispose();
            }
            bitmap_2 = method_0();
        }

        public void SetSystem(Habitat selectedSystem)
        {
            int_0 = Galaxy.SizeX / 2;
            int_1 = Galaxy.SizeY / 2;
            double_4 = Galaxy.SizeX / base.ClientRectangle.Width;
            method_2();
            habitatList_0 = null;
            if (selectedSystem != null)
            {
                habitatList_0 = new HabitatList();
                habitatList_0.Add(selectedSystem);
            }
        }

        public void SetSystems(HabitatList selectedSystems)
        {
            int_0 = Galaxy.SizeX / 2;
            int_1 = Galaxy.SizeY / 2;
            double_4 = Galaxy.SizeX / base.ClientRectangle.Width;
            method_2();
            habitatList_0 = selectedSystems;
        }

        public void SetPosition(double x, double y)
        {
            int_0 = Galaxy.SizeX / 2;
            int_1 = Galaxy.SizeY / 2;
            double_4 = Galaxy.SizeX / base.ClientRectangle.Width;
            method_2();
            double_0 = x;
            double_1 = y;
        }

        public void SetPositionAlt(double x, double y)
        {
            int_0 = Galaxy.SizeX / 2;
            int_1 = Galaxy.SizeY / 2;
            double_4 = Galaxy.SizeX / base.ClientRectangle.Width;
            method_2();
            double_2 = x;
            double_3 = y;
        }

        public void ClearLocations()
        {
            list_0.Clear();
        }

        public void SetLocations(StellarObjectList stellarObjects)
        {
            habitatList_0 = null;
            method_2();
            list_0.Clear();
            if (stellarObjects != null)
            {
                for (int i = 0; i < stellarObjects.Count; i++)
                {
                    StellarObject stellarObject = stellarObjects[i];
                    list_0.Add(new Point((int)stellarObject.Xpos, (int)stellarObject.Ypos));
                }
            }
        }

        public void SetLocations(ShipGroupList shipGroups)
        {
            habitatList_0 = null;
            method_2();
            list_0.Clear();
            if (shipGroups != null)
            {
                for (int i = 0; i < shipGroups.Count; i++)
                {
                    ShipGroup shipGroup = shipGroups[i];
                    list_0.Add(new Point((int)shipGroup.LeadShip.Xpos, (int)shipGroup.LeadShip.Ypos));
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (galaxy_0 != null)
            {
                method_6(int_0, int_1, base.ClientRectangle.Width, base.ClientRectangle.Height, double_4, e.Graphics);
            }
        }

        private void method_3(int int_4, int int_5, double double_5, double double_6, ref int int_6, ref int int_7, ref int int_8, ref int int_9)
        {
            int_6 = (int)((double)int_4 - double_5 / 2.0);
            int_7 = (int)((double)int_4 + double_5 / 2.0);
            int_8 = (int)((double)int_5 - double_6 / 2.0);
            int_9 = (int)((double)int_5 + double_6 / 2.0);
        }

        private Brush method_4(SystemInfo systemInfo_0)
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

        private void method_5(Graphics graphics_0, Empire empire_0, double double_5, int int_4, int int_5, int int_6, int int_7)
        {
            Color color = Color.FromArgb(64, 255, 0, 0);
            Color color2 = Color.FromArgb(64, 0, 0, 255);
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
                        num = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_5);
                    }
                    float num2 = (float)((shipGroup.AttackPoint.Xpos - (double)int_6) / double_5);
                    float num3 = (float)((shipGroup.AttackPoint.Ypos - (double)int_7) / double_5);
                    if (num > 0f && num2 + num > 0f && num2 - num < (float)int_4 && num3 + num > 0f && num3 - num < (float)int_5)
                    {
                        RectangleF rect = new RectangleF(num2 - num, num3 - num, num * 2f, num * 2f);
                        graphics_0.FillEllipse(brush, rect);
                        graphics_0.DrawEllipse(pen, rect);
                    }
                    if (shipGroup.GatherPoint != null)
                    {
                        float x = (float)((shipGroup.GatherPoint.Xpos - (double)int_6) / double_5);
                        float y = (float)((shipGroup.GatherPoint.Ypos - (double)int_7) / double_5);
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
                        num4 = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_5);
                    }
                    float num5 = (float)((shipGroup.GatherPoint.Xpos - (double)int_6) / double_5);
                    float num6 = (float)((shipGroup.GatherPoint.Ypos - (double)int_7) / double_5);
                    if (num4 > 0f && num5 + num4 > 0f && num5 - num4 < (float)int_4 && num6 + num4 > 0f && num6 - num4 < (float)int_5)
                    {
                        RectangleF rect2 = new RectangleF(num5 - num4, num6 - num4, num4 * 2f, num4 * 2f);
                        graphics_0.FillEllipse(brush2, rect2);
                        graphics_0.DrawEllipse(pen2, rect2);
                    }
                }
            }
        }

        private void method_6(int int_4, int int_5, int int_6, int int_7, double double_5, Graphics graphics_0)
        {
            int num = (int)((double)Galaxy.SizeX / double_5);
            double num2 = (double)int_6 * double_5;
            double num3 = (double)int_7 * double_5;
            int int_8 = 0;
            int int_9 = 0;
            int int_10 = 0;
            int int_11 = 0;
            method_3(int_4, int_5, num2, num3, ref int_8, ref int_9, ref int_10, ref int_11);
            int num4 = Galaxy.SectorSize * 8;
            if (int_8 < num4 * -1)
            {
                int_4 = (int)num2 / 2 - num4;
                method_3(int_4, int_5, num2, num3, ref int_8, ref int_9, ref int_10, ref int_11);
            }
            if (int_9 > Galaxy.SizeX + num4)
            {
                int_4 = Galaxy.SizeX + num4 - (int)num2 / 2;
                method_3(int_4, int_5, num2, num3, ref int_8, ref int_9, ref int_10, ref int_11);
            }
            if (int_10 < num4 * -1)
            {
                int_5 = (int)num3 / 2 - num4;
                method_3(int_4, int_5, num2, num3, ref int_8, ref int_9, ref int_10, ref int_11);
            }
            if (int_11 > Galaxy.SizeY + num4)
            {
                int_5 = Galaxy.SizeY + num4 - (int)num3 / 2;
                method_3(int_4, int_5, num2, num3, ref int_8, ref int_9, ref int_10, ref int_11);
            }
            graphics_0.InterpolationMode = InterpolationMode.Low;
            graphics_0.SmoothingMode = SmoothingMode.None;
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            if (bitmap_1 != null)
            {
                float num5 = 0f;
                float num6 = 0f;
                float num7 = bitmap_1.Width;
                float num8 = num7 / (float)Galaxy.SizeX;
                float num9 = (float)num2 * num8;
                float num10 = (float)num3 * num8;
                float num11 = num5 * num8;
                float num12 = num6 * num8;
                graphics_0.DrawImage(srcRect: new RectangleF(num11, num12, num9, num10), destRect: new RectangleF(0f, 0f, base.Width, base.Height), image: bitmap_1, srcUnit: GraphicsUnit.Pixel);
            }
            if (bool_0 && bitmap_0 != null)
            {
                int num13 = (int)((0.0 - (double)int_8) / double_5);
                int num14 = (int)((0.0 - (double)int_10) / double_5);
                int num15 = (int)((double)Galaxy.SizeX / double_5);
                int num16 = (int)((double)Galaxy.SizeY / double_5);
                Bitmap bitmap = bitmap_0;
                Rectangle destRect2 = new Rectangle(num13, num14, num15, num16);
                new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                graphics_0.DrawImage(bitmap, destRect2, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
            }
            if (bitmap_2 != null)
            {
                int num17 = (int)((0.0 - (double)int_8) / double_5);
                int num18 = (int)((0.0 - (double)int_10) / double_5);
                int num19 = (int)((double)Galaxy.SizeX / double_5);
                int num20 = (int)((double)Galaxy.SizeY / double_5);
                Bitmap bitmap2 = bitmap_2;
                Rectangle destRect3 = new Rectangle(num17, num18, num19, num20);
                new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
                graphics_0.DrawImage(bitmap2, destRect3, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel);
            }
            double num21 = (double)num / (double)Galaxy.SectorMaxX;
            SizeF sizeF = graphics_0.MeasureString("H", font_0, 200, StringFormat.GenericTypographic);
            int num22 = (int)(num21 / 2.0 - (double)sizeF.Width / 2.0);
            int num23 = (int)(num21 / 2.0 - (double)sizeF.Height / 2.0);
            int num24 = galaxy_0.ResolveSector(int_8, int_5).X;
            int num25 = galaxy_0.ResolveSector(int_9, int_5).X;
            int num26 = galaxy_0.ResolveSector(int_4, int_10).Y;
            int num27 = galaxy_0.ResolveSector(int_4, int_11).Y;
            int num28 = 2;
            int num29 = 5;
            if (int_6 >= 400 && (habitatList_0 != null || int_2 > 0))
            {
                num28 = 3;
                num29 = 5;
            }
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            bool flag = false;
            bool flag2 = false;
            int num30 = 0;
            int y = int_7;
            if (num26 <= 1)
            {
                num30 = (int)((double)(num26 * Galaxy.SectorSize - int_10) / double_5);
            }
            if (num27 >= Galaxy.SectorMaxY - 1)
            {
                y = (int)((double)((num27 + 1) * Galaxy.SectorSize - int_10) / double_5);
                flag2 = true;
            }
            int num31 = 0;
            int x = int_6;
            if (num24 <= 1)
            {
                num31 = (int)((double)(num24 * Galaxy.SectorSize - int_8) / double_5);
            }
            if (num25 >= Galaxy.SectorMaxY - 1)
            {
                x = (int)((double)((num25 + 1) * Galaxy.SectorSize - int_8) / double_5);
                flag = true;
            }
            for (int i = num24; i <= num25; i++)
            {
                int num32 = (int)((double)(i * Galaxy.SectorSize - int_8) / double_5);
                graphics_0.DrawLine(main_0.pen_1, num32, num30, num32, y);
                string s = ((char)(i + 65)).ToString();
                graphics_0.DrawString(point: new Point(num32 + num22, num30 + 2), s: s, font: font_0, brush: solidBrush_0);
            }
            if (flag)
            {
                int num33 = (int)((double)((num25 + 1) * Galaxy.SectorSize - int_8) / double_5);
                graphics_0.DrawLine(main_0.pen_1, num33, num30, num33, y);
            }
            for (int j = num26; j <= num27; j++)
            {
                int num34 = (int)((double)(j * Galaxy.SectorSize - int_10) / double_5);
                graphics_0.DrawLine(main_0.pen_1, num31, num34, x, num34);
                string s2 = (j + 1).ToString();
                graphics_0.DrawString(point: new Point(num31 + 2, num34 + num23), s: s2, font: font_0, brush: solidBrush_0);
            }
            if (flag2)
            {
                int num35 = (int)((double)((num27 + 1) * Galaxy.SectorSize - int_10) / double_5);
                graphics_0.DrawLine(main_0.pen_1, num31, num35, x, num35);
            }
            Empire playerEmpire = main_0._Game.PlayerEmpire;
            if (bool_1)
            {
                method_5(graphics_0, playerEmpire, double_5, int_6, int_7, int_8, int_10);
            }
            for (int k = 0; k < galaxy_0.Systems.Count; k++)
            {
                SystemInfo systemInfo = galaxy_0.Systems[k];
                if (systemInfo.Sector.X < num24 || systemInfo.Sector.X > num25 || systemInfo.Sector.Y < num26 || systemInfo.Sector.Y > num27)
                {
                    continue;
                }
                float num36 = (float)((systemInfo.SystemStar.Xpos - (double)int_8) / double_5);
                float num37 = (float)((systemInfo.SystemStar.Ypos - (double)int_10) / double_5);
                if (!(num36 >= 0f) || !(num36 <= (float)int_6) || !(num37 >= 0f) || !(num37 <= (float)int_7))
                {
                    continue;
                }
                bool flag3 = false;
                Brush brush = method_4(systemInfo);
                float num38 = num28;
                if (int_2 > 0)
                {
                    brush = new SolidBrush(Color.FromArgb(80, 80, 80));
                }
                else if (habitatList_0 != null)
                {
                    brush = new SolidBrush(Color.FromArgb(80, 80, 80));
                    if (habitatList_0.Contains(systemInfo.SystemStar))
                    {
                        flag3 = true;
                        brush = new SolidBrush(Color.Yellow);
                        num38 = num29;
                    }
                }
                SystemVisibilityStatus systemVisibilityStatus = playerEmpire.CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
                if (systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored))
                {
                    float num39 = 0f;
                    float num40 = 0f;
                    Pen pen = null;
                    HabitatList linkSystemStars = systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars;
                    if (linkSystemStars.Count > 0)
                    {
                        for (int l = 0; l < linkSystemStars.Count; l++)
                        {
                            Habitat habitat = linkSystemStars[l];
                            SystemVisibilityStatus systemVisibilityStatus2 = playerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                            if (systemVisibilityStatus2 == SystemVisibilityStatus.Visible || systemVisibilityStatus2 == SystemVisibilityStatus.Explored)
                            {
                                pen = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 1f);
                                num39 = (float)((habitat.Xpos - (double)int_8) / double_5);
                                num40 = (float)((habitat.Ypos - (double)int_10) / double_5);
                                pen.DashPattern = new float[2] { 2f, 1.5f };
                                graphics_0.DrawLine(pen, num36, num37, num39, num40);
                            }
                        }
                    }
                    if (systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count > 0)
                    {
                        for (int m = 0; m < systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; m++)
                        {
                            Habitat habitat2 = systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[m];
                            if (linkSystemStars.Contains(habitat2))
                            {
                                continue;
                            }
                            SystemInfo systemInfo2 = galaxy_0.Systems[habitat2.SystemIndex];
                            if (systemInfo2.Sector.X < num24 || systemInfo2.Sector.X > num25 || systemInfo2.Sector.Y < num26 || systemInfo2.Sector.Y > num27)
                            {
                                SystemVisibilityStatus systemVisibilityStatus3 = playerEmpire.CheckSystemVisibilityStatus(habitat2.SystemIndex);
                                if (systemVisibilityStatus3 == SystemVisibilityStatus.Visible || systemVisibilityStatus3 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                {
                                    pen = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 1f);
                                    num39 = (float)((habitat2.Xpos - (double)int_8) / double_5);
                                    num40 = (float)((habitat2.Ypos - (double)int_10) / double_5);
                                    pen.DashPattern = new float[2] { 3f, 5f };
                                    graphics_0.DrawLine(pen, num36, num37, num39, num40);
                                }
                            }
                        }
                    }
                    if (systemInfo.OtherEmpires != null)
                    {
                        for (int n = 0; n < systemInfo.OtherEmpires.Count; n++)
                        {
                            EmpireSystemSummary empireSystemSummary = systemInfo.OtherEmpires[n];
                            Empire empire = empireSystemSummary.Empire;
                            linkSystemStars = empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars;
                            if (linkSystemStars.Count > 0)
                            {
                                for (int num41 = 0; num41 < linkSystemStars.Count; num41++)
                                {
                                    Habitat habitat3 = linkSystemStars[num41];
                                    SystemVisibilityStatus systemVisibilityStatus4 = playerEmpire.CheckSystemVisibilityStatus(habitat3.SystemIndex);
                                    if (systemVisibilityStatus4 == SystemVisibilityStatus.Visible || systemVisibilityStatus4 == SystemVisibilityStatus.Explored)
                                    {
                                        pen = new Pen(empire.MainColor, 1f);
                                        num39 = (float)((habitat3.Xpos - (double)int_8) / double_5);
                                        num40 = (float)((habitat3.Ypos - (double)int_10) / double_5);
                                        pen.DashPattern = new float[2] { 2f, 1.5f };
                                        graphics_0.DrawLine(pen, num36, num37, num39, num40);
                                    }
                                }
                            }
                            if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count <= 0)
                            {
                                continue;
                            }
                            for (int num42 = 0; num42 < empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; num42++)
                            {
                                Habitat habitat4 = empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[num42];
                                if (linkSystemStars.Contains(habitat4))
                                {
                                    continue;
                                }
                                SystemInfo systemInfo3 = galaxy_0.Systems[habitat4.SystemIndex];
                                if (systemInfo3.Sector.X < num24 || systemInfo3.Sector.X > num25 || systemInfo3.Sector.Y < num26 || systemInfo3.Sector.Y > num27)
                                {
                                    SystemVisibilityStatus systemVisibilityStatus5 = playerEmpire.CheckSystemVisibilityStatus(habitat4.SystemIndex);
                                    if (systemVisibilityStatus5 == SystemVisibilityStatus.Visible || systemVisibilityStatus5 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                    {
                                        pen = new Pen(empire.MainColor, 1f);
                                        num39 = (float)((habitat4.Xpos - (double)int_8) / double_5);
                                        num40 = (float)((habitat4.Ypos - (double)int_10) / double_5);
                                        pen.DashPattern = new float[2] { 3f, 5f };
                                        graphics_0.DrawLine(pen, num36, num37, num39, num40);
                                    }
                                }
                            }
                        }
                    }
                    RectangleF rect = new RectangleF(num36 - (num38 + 4f) / 2f, num37 - (num38 + 4f) / 2f, num38 + 4f, num38 + 4f);
                    Pen pen2 = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 2f);
                    if (systemInfo.OtherEmpires != null && systemInfo.OtherEmpires.Count > 0)
                    {
                        pen2.DashStyle = DashStyle.Dash;
                    }
                    graphics_0.DrawEllipse(pen2, rect);
                }
                RectangleF rect2 = new RectangleF(num36 - num38 / 2f, num37 - num38 / 2f, num38, num38);
                if (flag3)
                {
                    graphics_0.FillEllipse(brush, rect2);
                }
                else
                {
                    graphics_0.FillEllipse(brush, rect2);
                }
            }
            SolidBrush brush2 = new SolidBrush(Color.Yellow);
            foreach (Point item in list_0)
            {
                int num43 = (int)((double)item.X / double_4);
                int num44 = (int)((double)item.Y / double_4);
                Rectangle rect3 = new Rectangle(num43 - 2, num44 - 2, 5, 5);
                graphics_0.FillEllipse(brush2, rect3);
            }
            if (double_0 > 0.0 && double_1 > 0.0)
            {
                int num45 = (int)(double_0 / double_5) + 1;
                int num46 = (int)(double_1 / double_5) + 1;
                graphics_0.DrawLine(main_0.pen_2, num45, 0, num45, base.ClientRectangle.Height);
                graphics_0.DrawLine(main_0.pen_2, 0, num46, base.ClientRectangle.Width, num46);
            }
            if (double_2 > 0.0 && double_3 > 0.0)
            {
                int num47 = (int)(double_2 / double_5) + 1;
                int num48 = (int)(double_3 / double_5) + 1;
                using Pen pen3 = new Pen(Color.FromArgb(255, 32, 128));
                graphics_0.DrawLine(pen3, num47, 0, num47, base.ClientRectangle.Height);
                graphics_0.DrawLine(pen3, 0, num48, base.ClientRectangle.Width, num48);
            }
        }

        public GalaxyMap():base()
        {
            
            list_0 = new List<Point>();
            bool_0 = true;
            font_0 = new Font("Verdana", 7f);
        }
    }
}
