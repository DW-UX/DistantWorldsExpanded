// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireSummaryEconomy
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireSummaryEconomy : GradientPanel
    {
        private Main main_0;

        private Galaxy abbeuQmfU;

        private Empire empire_0;

        private int int_0;

        private int int_1;

        private int GqYieCfHe;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private SolidBrush solidBrush_2;

        private Font font_0;

        private LinkLabel lnkEconomy;

        private Font font_1;

        public EmpireSummaryEconomy() : base()
        {
            Class7.VEFSJNszvZKMZ();
            int_0 = 13;
            int_1 = 10;
            GqYieCfHe = 10;
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
            abbeuQmfU = null;
            empire_0 = null;
        }

        public void Ignite(Main parentForm, Galaxy galaxy, Empire empire)
        {
            main_0 = parentForm;
            abbeuQmfU = galaxy;
            empire_0 = empire;
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
            lnkEconomy.Font = Font;
            lnkEconomy.Location = new Point(323, int_1 - 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (empire_0 != null)
            {
                if (empire_0.PirateEmpireBaseHabitat != null && empire_0.PirateEconomy != null)
                {
                    method_2(e.Graphics, empire_0.PirateEconomy);
                }
                else
                {
                    method_6(e.Graphics);
                }
            }
        }

        private int method_0(Graphics graphics_0, string string_0, Font font_2, int int_2)
        {
            int num = (int)graphics_0.MeasureString(string_0, font_2, int_2, StringFormat.GenericTypographic).Width;
            return (int_2 - num) / 2;
        }

        private int method_1(Graphics graphics_0, string string_0, Font font_2, int int_2)
        {
            int num = (int)graphics_0.MeasureString(string_0, font_2, int_2, StringFormat.GenericTypographic).Width;
            return int_2 - num;
        }

        private void lnkEconomy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            main_0.method_456(TextResolver.GetText("Economy Tips"));
        }

        private void method_2(Graphics graphics_0, PirateEconomy pirateEconomy_0)
        {
            int num = 17;
            int num2 = 10;
            int int_ = 20;
            int num3 = 150;
            int num4 = 210;
            int num5 = 270;
            int num6 = 290;
            int int_2 = 300;
            int num7 = 420;
            int num8 = 480;
            int num9 = 540;
            ecrlHwNiM(point_0: new Point(GqYieCfHe, int_1), graphics_0: graphics_0, string_0: TextResolver.GetText("Pirate Economy"), font_2: font_1);
            int num10 = 40;
            int num11 = -8;
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(56, 128, 128, 128)))
            {
                Rectangle rect = new Rectangle(num3, num10 + num11, num4 - num3, 253);
                graphics_0.FillRectangle(brush, rect);
                Rectangle rect2 = new Rectangle(num7, num10 + num11, num8 - num7, 203);
                graphics_0.FillRectangle(brush, rect2);
            }
            using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(128, 128, 128, 128)))
            {
                Rectangle rect3 = new Rectangle(num4, num10 + num11, num5 - num4, 253);
                graphics_0.FillRectangle(brush2, rect3);
                Rectangle rect4 = new Rectangle(num8, num10 + num11, num9 - num8, 203);
                graphics_0.FillRectangle(brush2, rect4);
            }
            string string_ = TextResolver.GetText("Income").ToUpper(CultureInfo.InvariantCulture);
            ecrlHwNiM(graphics_0, string_, font_0, new Point(num2, num10));
            string string_2 = TextResolver.GetText("Last Year");
            int num12 = num3 + method_0(graphics_0, string_2, font_0, num4 - num3);
            ecrlHwNiM(graphics_0, string_2, font_0, new Point(num12, num10 + num11));
            string string_3 = TextResolver.GetText("This Year");
            int num13 = num4 + method_0(graphics_0, string_3, font_0, num5 - num4);
            ecrlHwNiM(graphics_0, string_3, font_0, new Point(num13, num10 + num11));
            string string_4 = TextResolver.GetText("Expenses").ToUpper(CultureInfo.InvariantCulture);
            ecrlHwNiM(graphics_0, string_4, font_0, new Point(num6, num10));
            num12 = num7 + method_0(graphics_0, string_2, font_0, num8 - num7);
            ecrlHwNiM(graphics_0, string_2, font_0, new Point(num12, num10 + num11));
            num13 = num8 + method_0(graphics_0, string_3, font_0, num9 - num8);
            ecrlHwNiM(graphics_0, string_3, font_0, new Point(num13, num10 + num11));
            num10 += 20;
            int num14 = num10;
            PirateEconomyYear pirateEconomyYear = pirateEconomy_0.LastYear;
            PirateEconomyYear thisYear = pirateEconomy_0.ThisYear;
            if (pirateEconomyYear == null)
            {
                pirateEconomyYear = new PirateEconomyYear(abbeuQmfU.CurrentStarDate);
            }
            ecrlHwNiM(graphics_0, TextResolver.GetText("Stable Income"), font_0, new Point(num2, num14));
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Protection Agreements"), pirateEconomyYear.ProtectionAgreementIncome, thisYear.ProtectionAgreementIncome, num14, int_, num3, num4, num5, 0);
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Controlled Colonies"), pirateEconomyYear.ControlColonyIncome, thisYear.ControlColonyIncome, num14, int_, num3, num4, num5, 1);
            num14 += num;
            ecrlHwNiM(graphics_0, TextResolver.GetText("Variable Income"), font_0, new Point(num2, num14));
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Pirate Missions"), pirateEconomyYear.MissionIncome, thisYear.MissionIncome, num14, int_, num3, num4, num5, 0);
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Looting Destroyed Ships"), pirateEconomyYear.LootingIncome, thisYear.LootingIncome, num14, int_, num3, num4, num5, 1);
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Scrapping Captured Ships"), pirateEconomyYear.ScrapCapturedShipIncome, thisYear.ScrapCapturedShipIncome, num14, int_, num3, num4, num5, 0);
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Smuggling"), pirateEconomyYear.SmugglingIncome, thisYear.SmugglingIncome, num14, int_, num3, num4, num5, 1);
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Mining"), pirateEconomyYear.MiningIncome, thisYear.MiningIncome, num14, int_, num3, num4, num5, 0);
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Selling Information"), pirateEconomyYear.SellInfoIncome, thisYear.SellInfoIncome, num14, int_, num3, num4, num5, 1);
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Resorts"), pirateEconomyYear.ResortIncome, thisYear.ResortIncome, num14, int_, num3, num4, num5, 0);
            num14 += num;
            method_3(graphics_0, TextResolver.GetText("Raids and Other"), pirateEconomyYear.OtherIncome, thisYear.OtherIncome, num14, int_, num3, num4, num5, 1);
            num14 += num;
            method_5(graphics_0, TextResolver.GetText("TOTAL"), pirateEconomyYear.TotalIncome, thisYear.TotalIncome, num14, num2, num3, num4, num5, 0, solidBrush_0, bool_0: true);
            num14 += num;
            num14 = num10;
            ecrlHwNiM(graphics_0, TextResolver.GetText("Stable Expenses"), font_0, new Point(num6, num14));
            num14 += num;
            method_4(graphics_0, TextResolver.GetText("Ship Maintenance"), pirateEconomyYear.ShipMaintenanceExpenses, thisYear.ShipMaintenanceExpenses, num14, int_2, num7, num8, num9, 0, solidBrush_2);
            num14 += num;
            ecrlHwNiM(graphics_0, TextResolver.GetText("Variable Expenses"), font_0, new Point(num6, num14));
            num14 += num;
            method_4(graphics_0, TextResolver.GetText("Construction"), pirateEconomyYear.ConstructionExpenses, thisYear.ConstructionExpenses, num14, int_2, num7, num8, num9, 1, solidBrush_2);
            num14 += num;
            method_4(graphics_0, TextResolver.GetText("Resource Purchases"), pirateEconomyYear.PurchaseResourcesExpenses, thisYear.PurchaseResourcesExpenses, num14, int_2, num7, num8, num9, 0, solidBrush_2);
            num14 += num;
            method_4(graphics_0, TextResolver.GetText("Crash Research"), pirateEconomyYear.CrashResearchExpenses, thisYear.CrashResearchExpenses, num14, int_2, num7, num8, num9, 1, solidBrush_2);
            num14 += num;
            method_4(graphics_0, TextResolver.GetText("Facility Building"), pirateEconomyYear.FacilityConstructionExpenses, thisYear.FacilityConstructionExpenses, num14, int_2, num7, num8, num9, 0, solidBrush_2);
            num14 += num;
            method_4(graphics_0, TextResolver.GetText("Fuel"), pirateEconomyYear.FuelExpenses, thisYear.FuelExpenses, num14, int_2, num7, num8, num9, 1, solidBrush_2);
            num14 += num;
            method_4(graphics_0, TextResolver.GetText("Other"), pirateEconomyYear.OtherExpenses, thisYear.OtherExpenses, num14, int_2, num7, num8, num9, 0, solidBrush_2);
            num14 += num;
            method_5(graphics_0, TextResolver.GetText("TOTAL"), pirateEconomyYear.TotalExpenses, thisYear.TotalExpenses, num14, num6, num7, num8, num9, 1, solidBrush_2, bool_0: true);
            num14 += num;
            int num15 = 295;
            string string_5 = TextResolver.GetText("Cash on hand") + ":";
            SizeF sizeF = graphics_0.MeasureString(string_5, font_1);
            ecrlHwNiM(graphics_0, string_5, font_1, new Point(num2, 295));
            if (empire_0.StateMoney < 0.0)
            {
                atRsyCrma(graphics_0, empire_0.StateMoney.ToString("0"), font_1, new Point(num2 + (int)sizeF.Width, num15), solidBrush_2);
            }
            else
            {
                ecrlHwNiM(graphics_0, empire_0.StateMoney.ToString("0"), font_1, new Point(num2 + (int)sizeF.Width, num15));
            }
            double stableCashflow = pirateEconomy_0.ThisYear.StableCashflow;
            if (pirateEconomy_0.LastYear != null)
            {
                stableCashflow = pirateEconomy_0.LastYear.StableCashflow;
            }
            string string_6 = TextResolver.GetText("Stable Cashflow") + ":";
            SizeF sizeF2 = graphics_0.MeasureString(string_6, font_1);
            ecrlHwNiM(graphics_0, string_6, font_1, new Point(num6, num15));
            if (stableCashflow < 0.0)
            {
                atRsyCrma(graphics_0, stableCashflow.ToString("0"), font_1, new Point(num6 + (int)sizeF2.Width, num15), solidBrush_2);
            }
            else
            {
                ecrlHwNiM(graphics_0, stableCashflow.ToString("0"), font_1, new Point(num6 + (int)sizeF2.Width, num15));
            }
        }

        private void method_3(Graphics graphics_0, string string_0, double double_0, double double_1, int int_2, int int_3, int int_4, int int_5, int int_6, int int_7)
        {
            method_5(graphics_0, string_0, double_0, double_1, int_2, int_3, int_4, int_5, int_6, int_7, solidBrush_0, bool_0: false);
        }

        private void method_4(Graphics graphics_0, string string_0, double double_0, double double_1, int int_2, int int_3, int int_4, int int_5, int int_6, int int_7, SolidBrush solidBrush_3)
        {
            method_5(graphics_0, string_0, double_0, double_1, int_2, int_3, int_4, int_5, int_6, int_7, solidBrush_3, bool_0: false);
        }

        private void method_5(Graphics graphics_0, string string_0, double double_0, double double_1, int int_2, int int_3, int int_4, int int_5, int int_6, int int_7, SolidBrush solidBrush_3, bool bool_0)
        {
            switch (int_7)
            {
                case 0:
                    {
                        using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(96, 96, 96, 96)))
                        {
                            Rectangle rect2 = new Rectangle(int_3, int_2, int_6 - int_3, 17);
                            graphics_0.FillRectangle(brush2, rect2);
                        }
                        break;
                    }
                case 1:
                    {
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(96, 64, 64, 64)))
                        {
                            Rectangle rect = new Rectangle(int_3, int_2, int_6 - int_3, 17);
                            graphics_0.FillRectangle(brush, rect);
                        }
                        break;
                    }
            }
            if (bool_0)
            {
                ecrlHwNiM(graphics_0, string_0, font_0, new Point(int_3, int_2));
            }
            else
            {
                ecrlHwNiM(graphics_0, string_0, Font, new Point(int_3, int_2));
            }
            int num = int_5 - int_4;
            int num2 = int_6 - int_5;
            num -= 5;
            num2 -= 5;
            string string_ = double_0.ToString("0");
            int num3 = int_4 + method_1(graphics_0, string_, font_0, num);
            atRsyCrma(graphics_0, string_, font_0, new Point(num3, int_2), solidBrush_3);
            string string_2 = double_1.ToString("0");
            int num4 = int_5 + method_1(graphics_0, string_2, font_0, num2);
            atRsyCrma(graphics_0, string_2, font_0, new Point(num4, int_2), solidBrush_3);
        }

        private void method_6(Graphics graphics_0)
        {
            int_0 = 15;
            SolidBrush solidBrush = null;
            int num = 135;
            int num2 = 120;
            int num3 = 60;
            int num4 = 255;
            int num5 = 135;
            int num6 = 335;
            int num7 = 60;
            int num8 = 470;
            int num9 = int_1 + int_0 * 4;
            int num10 = int_1 + int_0 * 6;
            int num11 = int_1 + int_0 * 9;
            int num12 = int_1 + int_0 * 16;
            int num13 = int_1 + int_0 * 18;
            int num14 = 4;
            int num15 = 2;
            if (empire_0 != null)
            {
                empire_0.PurchaseStateFuel(0.0);
                empire_0.PurchasePrivateFuel(0.0);
            }
            SolidBrush brush = new SolidBrush(Color.FromArgb(32, 80, 80, 255));
            SolidBrush brush2 = new SolidBrush(Color.FromArgb(40, 80, 80, 255));
            SolidBrush brush3 = new SolidBrush(Color.FromArgb(32, 80, 80, 80));
            SolidBrush brush4 = new SolidBrush(Color.FromArgb(50, 80, 80, 80));
            SolidBrush brush5 = new SolidBrush(Color.FromArgb(80, 100, 100, 100));
            graphics_0.FillRectangle(brush, num2 - num14, num9 - (int_0 * 2 + num14), num + num3 + num14 * 3, num13 - num9 + int_0 * 5 + num14 * 2);
            graphics_0.FillRectangle(brush3, num6 - num14, num9 - (int_0 * 2 + num14), num5 + num7 + num14 * 3, num13 - num9 + int_0 * 5 + num14 * 2);
            graphics_0.FillRectangle(brush2, num4 + num14 * 2, num9 - (int_0 * 2 + num14), num3, num13 - num9 + int_0 * 5 + num14 * 2);
            graphics_0.FillRectangle(brush4, num8 + num14 * 2, num9 - (int_0 * 2 + num14), num7, num13 - num9 + int_0 * 5 + num14 * 2);
            int num16 = int_0 - (int)((double)num14 * 1.5);
            int num17 = int_0 * 2 + num14;
            graphics_0.FillRectangle(brush5, GqYieCfHe, num9 - num16, num8 + num7 + num14 * 2 - GqYieCfHe, num17);
            graphics_0.FillRectangle(brush5, GqYieCfHe, num11 - num14, num8 + num7 + num14 * 2 - GqYieCfHe, int_0 * 6 + num14 * 2);
            graphics_0.FillRectangle(brush5, GqYieCfHe, num13 - num14, num8 + num7 + num14 * 2 - GqYieCfHe, int_0 * 3 + num14 * 2);
            using (Pen pen = new Pen(Color.FromArgb(255, 170, 170, 170), 2f))
            {
                graphics_0.DrawRectangle(pen, num4 + num14 * 2, num9 - num16, num3, num17);
                graphics_0.DrawRectangle(pen, num4 + num14 * 2, num12 - num16, num3, num17);
            }
            ecrlHwNiM(point_0: new Point(GqYieCfHe, int_1), graphics_0: graphics_0, string_0: TextResolver.GetText("Economy"), font_2: font_1);
            ecrlHwNiM(point_0: new Point(num2 + method_0(graphics_0, TextResolver.GetText("STATE"), font_0, num + num3), int_1 + int_0 * 2), graphics_0: graphics_0, string_0: TextResolver.GetText("STATE"), font_2: font_0);
            ecrlHwNiM(point_0: new Point(num6 + method_0(graphics_0, TextResolver.GetText("PRIVATE"), font_0, num5 + num7), int_1 + int_0 * 2), graphics_0: graphics_0, string_0: TextResolver.GetText("PRIVATE"), font_2: font_0);
            SizeF sizeF_ = new SizeF(90f, 100f);
            method_8(point_0: new Point(GqYieCfHe + 10, num9 - num15), graphics_0: graphics_0, string_0: TextResolver.GetText("Cash on hand"), font_2: font_0, brush_0: solidBrush_0, sizeF_0: sizeF_);
            method_8(point_0: new Point(GqYieCfHe + 10, num10 - num15), graphics_0: graphics_0, string_0: TextResolver.GetText("Annual Income"), font_2: font_0, brush_0: solidBrush_0, sizeF_0: sizeF_);
            method_8(point_0: new Point(GqYieCfHe + 10, num11 - num15), graphics_0: graphics_0, string_0: TextResolver.GetText("Annual Expenses"), font_2: font_0, brush_0: solidBrush_0, sizeF_0: sizeF_);
            method_8(point_0: new Point(GqYieCfHe + 10, num12 - num15), graphics_0: graphics_0, string_0: TextResolver.GetText("Cashflow"), font_2: font_0, brush_0: solidBrush_0, sizeF_0: sizeF_);
            method_8(point_0: new Point(GqYieCfHe + 10, num13 - num15), graphics_0: graphics_0, string_0: TextResolver.GetText("This Year's Bonus Income"), font_2: font_0, brush_0: solidBrush_0, sizeF_0: sizeF_);
            if (empire_0 != null)
            {
                string string_ = empire_0.StateMoney.ToString("0,K");
                Point point_9 = new Point(num4 + method_1(graphics_0, string_, font_1, num3), num9 - num15);
                solidBrush = method_7(empire_0.StateMoney);
                atRsyCrma(graphics_0, string_, font_1, point_9, solidBrush);
                string string_2 = TextResolver.GetText("Colony Tax Revenue");
                point_9 = new Point(num2 + method_1(graphics_0, string_2, Font, num), num10);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_2, Font, point_9, solidBrush);
                string string_3 = empire_0.AnnualTaxRevenue.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_3, font_0, num3), num10 - num15);
                solidBrush = method_7(empire_0.AnnualTaxRevenue);
                atRsyCrma(graphics_0, string_3, font_0, point_9, solidBrush);
                string string_4 = TextResolver.GetText("Tribute From Others");
                point_9 = new Point(num2 + method_1(graphics_0, string_4, Font, num), num10 + int_0);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_4, Font, point_9, solidBrush);
                string string_5 = empire_0.CalculateAnnualSubjugationTributeIncome().ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_5, font_0, num3), num10 + int_0 - num15);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_5, font_0, point_9, solidBrush);
                string string_6 = TextResolver.GetText("Ship & Base Maintenance");
                point_9 = new Point(num2 + method_1(graphics_0, string_6, Font, num), num11);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_6, Font, point_9, solidBrush);
                string string_7 = empire_0.AnnualStateMaintenanceExcludingUnderConstruction.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_7, font_0, num3), num11 - num15);
                solidBrush = solidBrush_2;
                atRsyCrma(graphics_0, string_7, font_0, point_9, solidBrush);
                string string_8 = TextResolver.GetText("Troop Maintenance");
                point_9 = new Point(num2 + method_1(graphics_0, string_8, Font, num), num11 + int_0);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_8, Font, point_9, solidBrush);
                string string_9 = empire_0.AnnualTroopMaintenance.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_9, font_0, num3), num11 + int_0 - num15);
                solidBrush = solidBrush_2;
                atRsyCrma(graphics_0, string_9, font_0, point_9, solidBrush);
                string string_10 = TextResolver.GetText("Facility Maintenance");
                point_9 = new Point(num2 + method_1(graphics_0, string_10, Font, num), num11 + int_0 * 2);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_10, Font, point_9, solidBrush);
                string string_11 = empire_0.AnnualFacilityMaintenance.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_11, font_0, num3), num11 + int_0 * 2 - num15);
                solidBrush = solidBrush_2;
                atRsyCrma(graphics_0, string_11, font_0, point_9, solidBrush);
                string string_12 = TextResolver.GetText("Fuel Costs");
                point_9 = new Point(num2 + method_1(graphics_0, string_12, Font, num), num11 + int_0 * 3);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_12, Font, point_9, solidBrush);
                string string_13 = empire_0.ThisYearsStateFuelCosts.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_13, font_0, num3), num11 + int_0 * 3 - num15);
                solidBrush = solidBrush_2;
                atRsyCrma(graphics_0, string_13, font_0, point_9, solidBrush);
                string string_14 = TextResolver.GetText("Subjugation Tribute");
                point_9 = new Point(num2 + method_1(graphics_0, string_14, Font, num), num11 + int_0 * 4);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_14, Font, point_9, solidBrush);
                string string_15 = empire_0.AnnualSubjugationTribute.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_15, font_0, num3), num11 + int_0 * 4 - num15);
                solidBrush = solidBrush_2;
                atRsyCrma(graphics_0, string_15, font_0, point_9, solidBrush);
                string string_16 = TextResolver.GetText("Pirate Protection");
                point_9 = new Point(num2 + method_1(graphics_0, string_16, Font, num), num11 + int_0 * 5);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_16, Font, point_9, solidBrush);
                string string_17 = empire_0.AnnualPirateProtection.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_17, font_0, num3), num11 + int_0 * 5 - num15);
                solidBrush = solidBrush_2;
                atRsyCrma(graphics_0, string_17, font_0, point_9, solidBrush);
                double num18 = empire_0.AnnualTaxRevenue + empire_0.CalculateAnnualSubjugationTributeIncome();
                double num19 = empire_0.AnnualStateMaintenanceExcludingUnderConstruction + empire_0.ThisYearsStateFuelCosts + empire_0.AnnualTroopMaintenance + empire_0.AnnualSubjugationTribute + empire_0.AnnualPirateProtection + empire_0.AnnualFacilityMaintenance;
                double double_ = num18 - num19;
                string string_18 = double_.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_18, font_1, num3), num12 - num15);
                solidBrush = method_7(double_);
                atRsyCrma(graphics_0, string_18, font_1, point_9, solidBrush);
                string string_19 = TextResolver.GetText("Space Port Income");
                point_9 = new Point(num2 + method_1(graphics_0, string_19, Font, num), num13);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_19, Font, point_9, solidBrush);
                string string_20 = empire_0.ThisYearsSpacePortIncome.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_20, font_0, num3), num13 - num15);
                solidBrush = method_7(empire_0.ThisYearsSpacePortIncome);
                atRsyCrma(graphics_0, string_20, font_0, point_9, solidBrush);
                string string_21 = TextResolver.GetText("Foreign Trade Bonuses");
                point_9 = new Point(num2 + method_1(graphics_0, string_21, Font, num), num13 + int_0);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_21, Font, point_9, solidBrush);
                string string_22 = empire_0.ThisYearsForeignTradeBonuses.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_22, font_0, num3), num13 + int_0 - num15);
                solidBrush = method_7(empire_0.ThisYearsForeignTradeBonuses);
                atRsyCrma(graphics_0, string_22, font_0, point_9, solidBrush);
                string string_23 = TextResolver.GetText("Resort Income");
                point_9 = new Point(num2 + method_1(graphics_0, string_23, Font, num), num13 + int_0 * 2);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_23, Font, point_9, solidBrush);
                string string_24 = empire_0.ThisYearsResortIncome.ToString("0,K");
                point_9 = new Point(num4 + method_1(graphics_0, string_24, font_0, num3), num13 + int_0 * 2 - num15);
                solidBrush = method_7(empire_0.ThisYearsResortIncome);
                atRsyCrma(graphics_0, string_24, font_0, point_9, solidBrush);
                string string_25 = empire_0.PrivateMoney.ToString("0,K");
                point_9 = new Point(num8 + method_1(graphics_0, string_25, font_0, num7), num9 - num15);
                solidBrush = method_7(empire_0.PrivateMoney);
                atRsyCrma(graphics_0, string_25, font_0, point_9, solidBrush);
                string string_26 = TextResolver.GetText("Colony Revenue");
                point_9 = new Point(num6 + method_1(graphics_0, string_26, Font, num5), num10);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_26, Font, point_9, solidBrush);
                string string_27 = empire_0.PrivateAnnualRevenue.ToString("0,K");
                point_9 = new Point(num8 + method_1(graphics_0, string_27, font_0, num7), num10 - num15);
                solidBrush = method_7(empire_0.PrivateAnnualRevenue);
                atRsyCrma(graphics_0, string_27, font_0, point_9, solidBrush);
                string string_28 = TextResolver.GetText("Colony Taxes");
                point_9 = new Point(num6 + method_1(graphics_0, string_28, Font, num5), num11);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_28, Font, point_9, solidBrush);
                string string_29 = empire_0.AnnualTaxRevenue.ToString("0,K");
                point_9 = new Point(num8 + method_1(graphics_0, string_29, font_0, num7), num11 - num15);
                solidBrush = method_7(empire_0.AnnualTaxRevenue * -1.0);
                atRsyCrma(graphics_0, string_29, font_0, point_9, solidBrush);
                string string_30 = TextResolver.GetText("Ship & Base Maintenance");
                point_9 = new Point(num6 + method_1(graphics_0, string_30, Font, num5), num11 + int_0);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_30, Font, point_9, solidBrush);
                string string_31 = empire_0.AnnualPrivateMaintenanceExcludingUnderConstruction.ToString("0,K");
                point_9 = new Point(num8 + method_1(graphics_0, string_31, font_0, num7), num11 + int_0 - num15);
                solidBrush = solidBrush_2;
                atRsyCrma(graphics_0, string_31, font_0, point_9, solidBrush);
                string string_32 = TextResolver.GetText("Fuel Costs");
                point_9 = new Point(num6 + method_1(graphics_0, string_32, Font, num5), num11 + int_0 * 2);
                solidBrush = solidBrush_0;
                atRsyCrma(graphics_0, string_32, Font, point_9, solidBrush);
                string string_33 = empire_0.ThisYearsPrivateFuelCosts.ToString("0,K");
                point_9 = new Point(num8 + method_1(graphics_0, string_33, font_0, num7), num11 + int_0 * 2 - num15);
                solidBrush = solidBrush_2;
                atRsyCrma(graphics_0, string_33, font_0, point_9, solidBrush);
                double double_2 = empire_0.PrivateAnnualRevenue - (empire_0.AnnualPrivateMaintenanceExcludingUnderConstruction + empire_0.AnnualTaxRevenue + empire_0.ThisYearsPrivateFuelCosts);
                string string_34 = double_2.ToString("0,K");
                point_9 = new Point(num8 + method_1(graphics_0, string_34, font_0, num7), num12 - num15);
                solidBrush = method_7(double_2);
                atRsyCrma(graphics_0, string_34, font_0, point_9, solidBrush);
            }
        }

        private SolidBrush method_7(double double_0)
        {
            SolidBrush result = solidBrush_0;
            if (double_0 < 0.0)
            {
                result = solidBrush_2;
            }
            return result;
        }

        private void ecrlHwNiM(Graphics graphics_0, string string_0, Font font_2, Point point_0)
        {
            atRsyCrma(graphics_0, string_0, font_2, point_0, solidBrush_0);
        }

        private void atRsyCrma(Graphics graphics_0, string string_0, Font font_2, Point point_0, SolidBrush solidBrush_3)
        {
            point_0 = new Point(point_0.X + 1, point_0.Y + 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0, StringFormat.GenericTypographic);
            point_0 = new Point(point_0.X - 1, point_0.Y - 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_3, point_0, StringFormat.GenericTypographic);
        }

        private void method_8(Graphics graphics_0, string string_0, Font font_2, Point point_0, Brush brush_0, SizeF sizeF_0)
        {
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(layoutRectangle: new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height), s: string_0, font: font_2, brush: solidBrush_1, format: StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                RectangleF layoutRectangle2 = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_2, brush_0, layoutRectangle2, StringFormat.GenericTypographic);
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0, StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_2, brush_0, point_0, StringFormat.GenericTypographic);
            }
        }

        private void method_9()
        {
            lnkEconomy = new LinkLabel();
            SuspendLayout();
            lnkEconomy.ActiveLinkColor = Color.FromArgb(255, 128, 0);
            lnkEconomy.AutoSize = true;
            lnkEconomy.BackColor = Color.Transparent;
            lnkEconomy.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkEconomy.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkEconomy.LinkColor = Color.FromArgb(255, 192, 0);
            lnkEconomy.Location = new Point(-16, 53);
            lnkEconomy.Name = "lnkEconomy";
            lnkEconomy.Size = new Size(151, 13);
            lnkEconomy.TabIndex = 79;
            lnkEconomy.TabStop = true;
            lnkEconomy.Text = TextResolver.GetText("How does my empire make money?...");
            lnkEconomy.LinkClicked += lnkEconomy_LinkClicked;
            base.Controls.Add(lnkEconomy);
            ResumeLayout(performLayout: false);
        }
    }
}
