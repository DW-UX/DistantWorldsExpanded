// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireSummaryBuiltObject
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireSummaryBuiltObject : GradientPanel
    {
        private Main main_0;

        private Galaxy galaxy_0;

        private Empire empire_0;

        private int int_0;

        private int int_1;

        private int int_2;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private SolidBrush solidBrush_2;

        private Font font_0;

        private LinkLabel lnkShipCosts;

        private Font font_1;

        public EmpireSummaryBuiltObject():base()
        {
            
            int_0 = 15;
            int_1 = 10;
            int_2 = 10;
            solidBrush_0 = new SolidBrush(Color.FromArgb(170, 170, 170));
            solidBrush_1 = new SolidBrush(Color.Black);
            solidBrush_2 = new SolidBrush(Color.Red);
            method_9();
            Font = new Font("Verdana", 8f);
            SetFont(16.67f);
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        public void ClearData()
        {
            galaxy_0 = null;
            empire_0 = null;
        }

        public void Ignite(Main parentForm, Galaxy galaxy, Empire empire)
        {
            main_0 = parentForm;
            galaxy_0 = galaxy;
            empire_0 = empire;
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
            lnkShipCosts.Font = Font;
            lnkShipCosts.Location = new Point(443, int_1 + 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            method_2(e.Graphics);
        }

        private int method_0(Graphics graphics_0, string string_0, Font font_2, int int_3)
        {
            int num = (int)graphics_0.MeasureString(string_0, font_2, int_3, StringFormat.GenericTypographic).Width;
            return (int_3 - num) / 2;
        }

        private int method_1(Graphics graphics_0, string string_0, Font font_2, int int_3)
        {
            int num = (int)graphics_0.MeasureString(string_0, font_2, int_3, StringFormat.GenericTypographic).Width;
            return int_3 - num;
        }

        private void lnkShipCosts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            main_0.method_456(TextResolver.GetText("Ship Costs"));
        }

        private void method_2(Graphics graphics_0)
        {
            if (empire_0 != null && galaxy_0 != null)
            {
                int num = int_0;
                int num2 = 8;
                int num3 = int_1;
                jcqXwriUgM(point_0: new Point(int_2, num3), graphics_0: graphics_0, string_0: TextResolver.GetText("Ships & Bases"), font_2: font_1);
                int num4 = -5;
                int num5 = 234;
                int num6 = 432;
                num3 = int_1 + 25;
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(52, 80, 80, 255)))
                {
                    Rectangle rect = new Rectangle(num4 + 15, num3, 215, 235);
                    graphics_0.FillRectangle(brush, rect);
                }
                using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(28, 80, 80, 255)))
                {
                    Rectangle rect2 = new Rectangle(num5, num3, 188, 235);
                    graphics_0.FillRectangle(brush2, rect2);
                }
                using (SolidBrush brush3 = new SolidBrush(Color.FromArgb(50, 80, 80, 80)))
                {
                    Rectangle rect3 = new Rectangle(num6, num3, 189, 235);
                    graphics_0.FillRectangle(brush3, rect3);
                }
                jcqXwriUgM(graphics_0, TextResolver.GetText("Ship Role Military").ToUpper(CultureInfo.InvariantCulture), font_0, new Point(num4 + 20, num3 + 4));
                num3 += num;
                method_3(graphics_0, num4, num3, bool_0: true);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.Escort, num4, num3, empire_0.BuiltObjects, bool_0: true);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.Frigate, num4, num3, empire_0.BuiltObjects, bool_0: true);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.Destroyer, num4, num3, empire_0.BuiltObjects, bool_0: true);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.Cruiser, num4, num3, empire_0.BuiltObjects, bool_0: true);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.CapitalShip, num4, num3, empire_0.BuiltObjects, bool_0: true);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.TroopTransport, num4, num3, empire_0.BuiltObjects, bool_0: true);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.Carrier, num4, num3, empire_0.BuiltObjects, bool_0: true);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.ResupplyShip, num4, num3, empire_0.BuiltObjects, bool_0: true);
                num3 += num + num2;
                method_4(graphics_0, empire_0.BuiltObjects.GetBuiltObjectsBySubRole(new List<BuiltObjectSubRole>
            {
                BuiltObjectSubRole.Escort,
                BuiltObjectSubRole.Frigate,
                BuiltObjectSubRole.Destroyer,
                BuiltObjectSubRole.Cruiser,
                BuiltObjectSubRole.CapitalShip,
                BuiltObjectSubRole.TroopTransport,
                BuiltObjectSubRole.Carrier,
                BuiltObjectSubRole.ResupplyShip
            }), num4, num3, bool_0: true);
                num3 += int_0;
                num3 = int_1 + 25;
                jcqXwriUgM(graphics_0, TextResolver.GetText("Other State").ToUpper(CultureInfo.InvariantCulture), font_0, new Point(num5 + 5, num3 + 4));
                num3 += num;
                method_3(graphics_0, num5, num3, bool_0: false);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.ExplorationShip, num5, num3, empire_0.BuiltObjects, bool_0: false);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.ConstructionShip, num5, num3, empire_0.BuiltObjects, bool_0: false);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.ColonyShip, num5, num3, empire_0.BuiltObjects, bool_0: false);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.SmallSpacePort, num5, num3, empire_0.BuiltObjects, bool_0: false);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.MediumSpacePort, num5, num3, empire_0.BuiltObjects, bool_0: false);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.LargeSpacePort, num5, num3, empire_0.BuiltObjects, bool_0: false);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.DefensiveBase, num5, num3, empire_0.BuiltObjects, bool_0: false);
                num3 += num;
                method_7(graphics_0, new List<BuiltObjectSubRole>
            {
                BuiltObjectSubRole.EnergyResearchStation,
                BuiltObjectSubRole.HighTechResearchStation,
                BuiltObjectSubRole.WeaponsResearchStation
            }, num5, num3, empire_0.BuiltObjects, bool_0: false, TextResolver.GetText("Research Station"));
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.ResortBase, num5, num3, empire_0.BuiltObjects, bool_0: false);
                num3 += num;
                method_7(graphics_0, new List<BuiltObjectSubRole>
            {
                BuiltObjectSubRole.GenericBase,
                BuiltObjectSubRole.MonitoringStation
            }, num5, num3, empire_0.BuiltObjects, bool_0: false, TextResolver.GetText("Other Bases"));
                num3 += num + num2;
                method_4(graphics_0, empire_0.BuiltObjects.GetBuiltObjectsBySubRole(new List<BuiltObjectSubRole>
            {
                BuiltObjectSubRole.ExplorationShip,
                BuiltObjectSubRole.ConstructionShip,
                BuiltObjectSubRole.ColonyShip,
                BuiltObjectSubRole.SmallSpacePort,
                BuiltObjectSubRole.MediumSpacePort,
                BuiltObjectSubRole.LargeSpacePort,
                BuiltObjectSubRole.DefensiveBase,
                BuiltObjectSubRole.EnergyResearchStation,
                BuiltObjectSubRole.HighTechResearchStation,
                BuiltObjectSubRole.WeaponsResearchStation,
                BuiltObjectSubRole.ResortBase,
                BuiltObjectSubRole.GenericBase,
                BuiltObjectSubRole.MonitoringStation
            }), num5, num3, bool_0: false);
                num3 += int_0;
                num3 = int_1 + 25;
                jcqXwriUgM(graphics_0, TextResolver.GetText("PRIVATE"), font_0, new Point(num6 + 5, num3 + 4));
                num3 += num;
                method_3(graphics_0, num6, num3, bool_0: false);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.SmallFreighter, num6, num3, empire_0.PrivateBuiltObjects, bool_0: false);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.MediumFreighter, num6, num3, empire_0.PrivateBuiltObjects, bool_0: false);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.LargeFreighter, num6, num3, empire_0.PrivateBuiltObjects, bool_0: false);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.PassengerShip, num6, num3, empire_0.PrivateBuiltObjects, bool_0: false);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.MiningShip, num6, num3, empire_0.PrivateBuiltObjects, bool_0: false);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.GasMiningShip, num6, num3, empire_0.PrivateBuiltObjects, bool_0: false);
                num3 += num + num2;
                method_5(graphics_0, BuiltObjectSubRole.MiningStation, num6, num3, empire_0.PrivateBuiltObjects, bool_0: false);
                num3 += num;
                method_5(graphics_0, BuiltObjectSubRole.GasMiningStation, num6, num3, empire_0.PrivateBuiltObjects, bool_0: false);
                num3 += num + num2;
                method_4(graphics_0, empire_0.PrivateBuiltObjects.GetBuiltObjectsBySubRole(new List<BuiltObjectSubRole>
            {
                BuiltObjectSubRole.SmallFreighter,
                BuiltObjectSubRole.MediumFreighter,
                BuiltObjectSubRole.LargeFreighter,
                BuiltObjectSubRole.PassengerShip,
                BuiltObjectSubRole.MiningShip,
                BuiltObjectSubRole.GasMiningShip,
                BuiltObjectSubRole.MiningStation,
                BuiltObjectSubRole.GasMiningStation
            }), num6, num3, bool_0: false);
                num3 += int_0;
            }
        }

        private void method_3(Graphics graphics_0, int int_3, int int_4, bool bool_0)
        {
            if (main_0 != null)
            {
                int num = int_3 + 105;
                int int_5 = 35;
                int num2 = int_3 + 134;
                int int_6 = 40;
                int num3 = int_3 + 180;
                int int_7 = 20;
                if (!bool_0)
                {
                    num3 = int_3 + 134 + 10;
                }
                jcqXwriUgM(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Amount Abbreviation"), font_0, int_5), int_4), graphics_0: graphics_0, string_0: TextResolver.GetText("Amount Abbreviation"), font_2: font_0);
                if (bool_0)
                {
                    graphics_0.DrawImageUnscaled(point: new Point(num2 + method_1(graphics_0, "_", font_0, int_6), int_4 + 2), image: main_0.bitmap_38);
                }
                jcqXwriUgM(point_0: new Point(num3 + method_1(graphics_0, TextResolver.GetText("Maintenance Abbreviation"), font_0, int_7), int_4), graphics_0: graphics_0, string_0: TextResolver.GetText("Maintenance Abbreviation"), font_2: font_0);
            }
        }

        private void method_4(Graphics graphics_0, BuiltObjectList builtObjectList_0, int int_3, int int_4, bool bool_0)
        {
            int int_5 = 105;
            int num = int_3 + 105;
            int int_6 = 35;
            int num2 = int_3 + 140;
            int int_7 = 40;
            int num3 = int_3 + 180;
            int int_8 = 35;
            if (!bool_0)
            {
                num3 = int_3 + 140;
            }
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            foreach (BuiltObject item in builtObjectList_0)
            {
                num5++;
                if (item.UnbuiltComponentCount <= 0)
                {
                    num4 += item.FirepowerRaw;
                    num6 += item.AnnualSupportCost;
                }
            }
            double num7 = (double)num6 * empire_0.ShipMaintenanceSavings;
            num6 -= (int)num7;
            string string_ = TextResolver.GetText("TOTAL");
            jcqXwriUgM(point_0: new Point(int_3 + method_1(graphics_0, string_, font_0, int_5), int_4 - 2), graphics_0: graphics_0, string_0: string_, font_2: font_0);
            string string_2 = num5.ToString();
            jcqXwriUgM(point_0: new Point(num + method_1(graphics_0, string_2, font_0, int_6), int_4 - 2), graphics_0: graphics_0, string_0: string_2, font_2: font_0);
            if (bool_0)
            {
                string string_3 = num4.ToString();
                if (num4 >= 100000)
                {
                    string_3 = num4.ToString("0,K");
                }
                jcqXwriUgM(point_0: new Point(num2 + method_1(graphics_0, string_3, Font, int_7), int_4), graphics_0: graphics_0, string_0: string_3, font_2: Font);
            }
            string string_4 = num6.ToString("0,K");
            jcqXwriUgM(point_0: new Point(num3 + method_1(graphics_0, string_4, Font, int_8), int_4), graphics_0: graphics_0, string_0: string_4, font_2: Font);
        }

        private void method_5(Graphics graphics_0, BuiltObjectSubRole builtObjectSubRole_0, int int_3, int int_4, BuiltObjectList builtObjectList_0, bool bool_0)
        {
            method_6(graphics_0, builtObjectSubRole_0, int_3, int_4, builtObjectList_0, bool_0, string.Empty);
        }

        private void method_6(Graphics graphics_0, BuiltObjectSubRole builtObjectSubRole_0, int int_3, int int_4, BuiltObjectList builtObjectList_0, bool bool_0, string string_0)
        {
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            list.Add(builtObjectSubRole_0);
            method_7(graphics_0, list, int_3, int_4, builtObjectList_0, bool_0, string_0);
        }

        private void method_7(Graphics graphics_0, List<BuiltObjectSubRole> subRoles, int int_3, int int_4, BuiltObjectList builtObjectList_0, bool bool_0, string string_0)
        {
            int int_5 = 105;
            int num = int_3 + 105;
            int int_6 = 35;
            int num2 = int_3 + 140;
            int int_7 = 40;
            int num3 = int_3 + 180;
            int int_8 = 35;
            if (!bool_0)
            {
                num3 = int_3 + 140;
            }
            BuiltObjectList builtObjectsBySubRole = builtObjectList_0.GetBuiltObjectsBySubRole(subRoles);
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            foreach (BuiltObject item in builtObjectsBySubRole)
            {
                num5++;
                if (item.UnbuiltComponentCount <= 0)
                {
                    num4 += item.FirepowerRaw;
                    num6 += item.AnnualSupportCost;
                }
            }
            double num7 = (double)num6 * empire_0.ShipMaintenanceSavings;
            num6 -= (int)num7;
            if (string.IsNullOrEmpty(string_0))
            {
                string_0 = Galaxy.ResolveDescription(subRoles[0]);
                if (subRoles.Count > 1)
                {
                    string_0 = TextResolver.GetText("Other Bases");
                }
            }
            jcqXwriUgM(point_0: new Point(int_3 + method_1(graphics_0, string_0, Font, int_5), int_4), graphics_0: graphics_0, string_0: string_0, font_2: Font);
            string string_ = num5.ToString();
            jcqXwriUgM(point_0: new Point(num + method_1(graphics_0, string_, font_0, int_6), int_4 - 2), graphics_0: graphics_0, string_0: string_, font_2: font_0);
            if (bool_0)
            {
                string string_2 = num4.ToString();
                if (num4 >= 100000)
                {
                    string_2 = num4.ToString("0,K");
                }
                jcqXwriUgM(point_0: new Point(num2 + method_1(graphics_0, string_2, Font, int_7), int_4), graphics_0: graphics_0, string_0: string_2, font_2: Font);
            }
            string string_3 = num6.ToString("0,K");
            jcqXwriUgM(point_0: new Point(num3 + method_1(graphics_0, string_3, Font, int_8), int_4), graphics_0: graphics_0, string_0: string_3, font_2: Font);
        }

        private SolidBrush method_8(double double_0)
        {
            SolidBrush result = solidBrush_0;
            if (double_0 < 0.0)
            {
                result = solidBrush_2;
            }
            return result;
        }

        private void jcqXwriUgM(Graphics graphics_0, string string_0, Font font_2, Point point_0)
        {
            point_0 = new Point(point_0.X + 1, point_0.Y + 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0);
            point_0 = new Point(point_0.X - 1, point_0.Y - 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_0, point_0);
        }

        private void method_9()
        {
            lnkShipCosts = new LinkLabel();
            SuspendLayout();
            lnkShipCosts.ActiveLinkColor = Color.FromArgb(255, 128, 0);
            lnkShipCosts.AutoSize = true;
            lnkShipCosts.BackColor = Color.Transparent;
            lnkShipCosts.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkShipCosts.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkShipCosts.LinkColor = Color.FromArgb(255, 192, 0);
            lnkShipCosts.Location = new Point(-16, 53);
            lnkShipCosts.Name = "lnkShipCosts";
            lnkShipCosts.Size = new Size(151, 13);
            lnkShipCosts.TabIndex = 79;
            lnkShipCosts.TabStop = true;
            lnkShipCosts.Text = TextResolver.GetText("About ship maintenance costs...");
            lnkShipCosts.LinkClicked += lnkShipCosts_LinkClicked;
            base.Controls.Add(lnkShipCosts);
            ResumeLayout(performLayout: false);
        }
    }
}
