// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireSummaryBonuses
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
    public class EmpireSummaryBonuses : Panel
    {
        private Main main_0;

        private Galaxy galaxy_0;

        private Empire empire_0;

        private CharacterImageCache characterImageCache_0;

        private int int_0;

        private int int_1;

        private int int_2;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private SolidBrush solidBrush_2;

        private SolidBrush solidBrush_3;

        private Font font_0;

        private Font font_1;

        public EmpireSummaryBonuses():base()
        {
            
            int_0 = 15;
            int_1 = 10;
            int_2 = 10;
            solidBrush_0 = new SolidBrush(Color.FromArgb(170, 170, 170));
            solidBrush_1 = new SolidBrush(Color.Black);
            solidBrush_2 = new SolidBrush(Color.Red);
            solidBrush_3 = new SolidBrush(Color.Green);
            Font = new Font("Verdana", 8f);
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        public void ClearData()
        {
            galaxy_0 = null;
            empire_0 = null;
            characterImageCache_0 = null;
        }

        public void Ignite(Main parentForm, Galaxy galaxy, Empire empire, CharacterImageCache characterImageCache)
        {
            main_0 = parentForm;
            galaxy_0 = galaxy;
            empire_0 = empire;
            characterImageCache_0 = characterImageCache;
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawBonuses(e.Graphics);
        }

        public void DrawBonuses(Graphics graphics)
        {
            if (empire_0 == null)
            {
                return;
            }
            int_1 = 0;
            int_2 = 0;
            int num = int_2 + int_0 + 5;
            int num2 = 5;
            int int_ = 0;
            int num3 = int_1;
            SizeF sizeF_ = new SizeF(base.Width - (int_0 + 5 + int_2 * 2), base.Height);
            RaceList bonusRaces = new RaceList();
            List<string> list = empire_0.ResolveEmpireAbilityBonusDescriptions(includeDominantRaceInDescriptions: true, out bonusRaces);
            for (int i = 0; i < list.Count; i++)
            {
                if (bonusRaces[i] != null)
                {
                    main_0.method_112(graphics, GraphicsQuality.High);
                    Bitmap raceImage = main_0.raceImageCache_0.GetRaceImage(bonusRaces[i].PictureRef);
                    Rectangle srcRect = new Rectangle(0, 0, raceImage.Width, raceImage.Height);
                    Rectangle destRect = new Rectangle(int_2, num3, int_0, int_0);
                    graphics.DrawImage(raceImage, destRect, srcRect, GraphicsUnit.Pixel);
                    main_0.method_112(graphics, GraphicsQuality.Low);
                }
                method_7(graphics, list[i], Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                num3 += int_ + num2;
            }
            num3 += num2;
            string empty = string.Empty;
            empty = method_1(empire_0.SpecialBonusWealth, TextResolver.GetText("Wealth"), empire_0, empire_0.SpecialBonusWealthRuin, empire_0.SpecialBonusWealthWonder);
            if (!string.IsNullOrEmpty(empty))
            {
                giOnDsfNtU(graphics, empire_0.SpecialBonusWealthRuin, empire_0.SpecialBonusWealthWonder, int_2, num3);
                method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                num3 += int_ + num2;
            }
            empty = method_1(empire_0.SpecialBonusHappiness, TextResolver.GetText("Happiness"), empire_0, empire_0.SpecialBonusHappinessRuin, empire_0.SpecialBonusHappinessWonder);
            if (!string.IsNullOrEmpty(empty))
            {
                giOnDsfNtU(graphics, empire_0.SpecialBonusHappinessRuin, empire_0.SpecialBonusHappinessWonder, int_2, num3);
                method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                num3 += int_ + num2;
            }
            empty = method_1(empire_0.SpecialBonusDiplomacy, TextResolver.GetText("Diplomacy"), empire_0, empire_0.SpecialBonusDiplomacyRuin, null);
            if (!string.IsNullOrEmpty(empty))
            {
                giOnDsfNtU(graphics, empire_0.SpecialBonusDiplomacyRuin, null, int_2, num3);
                method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                num3 += int_ + num2;
            }
            empty = method_1(empire_0.SpecialBonusResearchEnergy, TextResolver.GetText("Energy Research"), empire_0, empire_0.SpecialBonusResearchEnergyRuin, empire_0.SpecialBonusResearchEnergyWonder);
            if (!string.IsNullOrEmpty(empty))
            {
                giOnDsfNtU(graphics, empire_0.SpecialBonusResearchEnergyRuin, empire_0.SpecialBonusResearchEnergyWonder, int_2, num3);
                method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                num3 += int_ + num2;
            }
            empty = method_1(empire_0.SpecialBonusResearchHighTech, TextResolver.GetText("HighTech Research"), empire_0, empire_0.SpecialBonusResearchHighTechRuin, empire_0.SpecialBonusResearchHighTechWonder);
            if (!string.IsNullOrEmpty(empty))
            {
                giOnDsfNtU(graphics, empire_0.SpecialBonusResearchHighTechRuin, empire_0.SpecialBonusResearchHighTechWonder, int_2, num3);
                method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                num3 += int_ + num2;
            }
            empty = method_1(empire_0.SpecialBonusResearchWeapons, TextResolver.GetText("Weapons Research"), empire_0, empire_0.SpecialBonusResearchWeaponsRuin, empire_0.SpecialBonusResearchWeaponsWonder);
            if (!string.IsNullOrEmpty(empty))
            {
                giOnDsfNtU(graphics, empire_0.SpecialBonusResearchWeaponsRuin, empire_0.SpecialBonusResearchWeaponsWonder, int_2, num3);
                method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                num3 += int_ + num2;
            }
            empty = method_1(empire_0.SpecialBonusPopulationGrowth, TextResolver.GetText("Population Growth"), empire_0, null, empire_0.SpecialBonusPopulationGrowthWonder);
            if (!string.IsNullOrEmpty(empty))
            {
                giOnDsfNtU(graphics, null, empire_0.SpecialBonusPopulationGrowthWonder, int_2, num3);
                method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                num3 += int_ + num2;
            }
            if (empire_0.PirateEmpireBaseHabitat != null)
            {
                double num4 = empire_0.CalculatePirateResearchBonusFromFacilities();
                if (num4 > 1.0)
                {
                    empty = string.Format(TextResolver.GetText("Bonus from Pirate Bases and Fortresses at Controlled Colonies"), (num4 - 1.0).ToString("+#%"));
                    PlanetaryFacilityDefinition planetaryFacilityDefinition = Galaxy.PlanetaryFacilityDefinitionsStatic[25];
                    method_0(graphics, -1, planetaryFacilityDefinition.PictureRef, int_2, num3);
                    method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                    num3 += int_ + num2;
                }
            }
            if (empire_0.Leader != null)
            {
                empty = Galaxy.GenerateLeaderBonusDescription(empire_0.Leader);
                if (!string.IsNullOrEmpty(empty))
                {
                    main_0.method_112(graphics, GraphicsQuality.High);
                    Bitmap bitmap = characterImageCache_0.ObtainCharacterImageSmall(empire_0.Leader);
                    if (bitmap != null)
                    {
                        Rectangle srcRect2 = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                        Rectangle destRect2 = new Rectangle(int_2, num3, int_0, int_0);
                        graphics.DrawImage(bitmap, destRect2, srcRect2, GraphicsUnit.Pixel);
                        main_0.method_112(graphics, GraphicsQuality.Low);
                    }
                    method_7(graphics, empty, Font, new Point(num, num3), solidBrush_0, sizeF_, out int_);
                    num3 += int_ + num2;
                }
            }
            if (base.Height < num3)
            {
                base.Height = num3;
            }
        }

        private void giOnDsfNtU(Graphics graphics_0, Ruin ruin_0, PlanetaryFacility planetaryFacility_0, int int_3, int int_4)
        {
            int int_5 = -1;
            int int_6 = -1;
            if (ruin_0 != null)
            {
                int_5 = ruin_0.PictureRef;
            }
            if (planetaryFacility_0 != null)
            {
                int_6 = planetaryFacility_0.PictureRef;
            }
            method_0(graphics_0, int_5, int_6, int_3, int_4);
        }

        private void method_0(Graphics graphics_0, int int_3, int int_4, int int_5, int int_6)
        {
            if (int_3 >= 0)
            {
                Bitmap bitmap = method_2(main_0.bitmap_2[int_3], int_0 + 1, int_0 + 1, 1f);
                int num = int_6 + (int_0 - bitmap.Height) / 2;
                graphics_0.DrawImage(bitmap, new Point(int_5, num));
            }
            else if (int_4 >= 0)
            {
                Bitmap bitmap2 = method_2(main_0.bitmap_8[int_4], int_0 + 1, int_0 + 1, 1f);
                int num2 = int_6 + (int_0 - bitmap2.Height) / 2;
                graphics_0.DrawImage(bitmap2, new Point(int_5, num2));
            }
        }

        private string method_1(double double_0, string string_0, Empire empire_1, Ruin ruin_0, PlanetaryFacility planetaryFacility_0)
        {
            string result = string.Empty;
            if (double_0 > 0.0)
            {
                result = string.Format(TextResolver.GetText("Ruins Bonus Description"), double_0.ToString("+0%"), string_0);
                if (ruin_0 != null)
                {
                    Habitat habitat = galaxy_0.IdentifyRuinHabitat(ruin_0);
                    if (habitat != null)
                    {
                        Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                        result = string.Format(TextResolver.GetText("Ruins Bonus Description Ruins"), double_0.ToString("+0%"), string_0, ruin_0.Name, habitat.Name, habitat2.Name);
                    }
                }
                else if (planetaryFacility_0 != null)
                {
                    Habitat habitat3 = galaxy_0.IdentifyWonderHabitat(empire_1, planetaryFacility_0);
                    if (habitat3 != null)
                    {
                        Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat3);
                        result = string.Format(TextResolver.GetText("Ruins Bonus Description Ruins"), double_0.ToString("+0%"), string_0, planetaryFacility_0.Name, habitat3.Name, habitat4.Name);
                    }
                }
            }
            return result;
        }

        internal Bitmap method_2(Bitmap bitmap_0, int int_3, int int_4, float float_0)
        {
            //Bitmap bitmap = null;
            double val = (double)int_3 / (double)bitmap_0.Width;
            double val2 = (double)int_4 / (double)bitmap_0.Height;
            double num = Math.Min(val, val2);
            if (num < 1.0)
            {
                int int_5 = (int)((double)bitmap_0.Width * num);
                int int_6 = (int)((double)bitmap_0.Height * num);
                return method_3(bitmap_0, int_5, int_6, float_0);
            }
            return method_3(bitmap_0, bitmap_0.Width, bitmap_0.Height, float_0);
        }

        internal Bitmap method_3(Bitmap bitmap_0, int int_3, int int_4, float float_0)
        {
            return method_4(bitmap_0, int_3, int_4, float_0, bool_0: false);
        }

        internal Bitmap method_4(Bitmap bitmap_0, int int_3, int int_4, float float_0, bool bool_0)
        {
            if (int_3 < 1)
            {
                int_3 = 1;
            }
            if (int_4 < 1)
            {
                int_4 = 1;
            }
            ImageAttributes imageAttr = main_0.method_20(float_0);
            Bitmap bitmap = new Bitmap(int_3, int_4, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            if (bool_0)
            {
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.SmoothingMode = SmoothingMode.None;
            }
            else
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
            Rectangle rectangle = new Rectangle(0, 0, bitmap_0.Width, bitmap_0.Height);
            Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            graphics.DrawImage(bitmap_0, destRect, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttr);
            return bitmap;
        }

        private void method_5(Graphics graphics_0, string string_0, Font font_2, Point point_0)
        {
            method_6(graphics_0, string_0, font_2, point_0, solidBrush_0);
        }

        private void method_6(Graphics graphics_0, string string_0, Font font_2, Point point_0, SolidBrush solidBrush_4)
        {
            point_0 = new Point(point_0.X + 1, point_0.Y + 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0, StringFormat.GenericTypographic);
            point_0 = new Point(point_0.X - 1, point_0.Y - 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_4, point_0, StringFormat.GenericTypographic);
        }

        private void method_7(Graphics graphics_0, string string_0, Font font_2, Point point_0, Brush brush_0, SizeF sizeF_0, out int int_3)
        {
            int_3 = 0;
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(layoutRectangle: new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height), s: string_0, font: font_2, brush: solidBrush_1, format: StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                RectangleF layoutRectangle2 = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_2, brush_0, layoutRectangle2, StringFormat.GenericTypographic);
                int_3 = (int)graphics_0.MeasureString(string_0, font_2, (int)sizeF_0.Width, StringFormat.GenericTypographic).Height;
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0, StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_2, brush_0, point_0, StringFormat.GenericTypographic);
            }
        }
    }
}
