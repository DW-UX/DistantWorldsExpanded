// Decompiled with JetBrains decompiler
// Type: DistantWorlds.HoverPanel
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Controls;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DistantWorlds
{
    public class HoverPanel
    {
        private Game game_0;

        private Main main_0;

        private Bitmap[] bitmap_0;

        private Bitmap[] bitmap_1;

        private Bitmap[] bitmap_2;

        private Bitmap[] bitmap_3;

        private Bitmap[] bitmap_4;

        private Bitmap[] bitmap_5;

        private Bitmap bitmap_6;

        private int int_0;

        private int int_1;

        private int int_2;

        private Bitmap bitmap_7;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private SolidBrush solidBrush_2;

        private Font font_0;

        private Font font_1;

        private Font font_2;

        private int int_3;

        private BuiltObject builtObject_0;

        private Fighter fighter_0;

        private Habitat habitat_0;

        private Creature creature_0;

        private bool bool_0;

        public DateTime LastRefresh;

        protected IFontCache _FontCache;

        //private float float_0;

        //private bool bool_1;

        public Game Game
        {
            get
            {
                return game_0;
            }
            set
            {
                game_0 = value;
            }
        }

        public virtual void SetFontCache(IFontCache fontCache)
        {
            _FontCache = fontCache;
        }

        private void method_0()
        {
            if (solidBrush_0 != null)
            {
                solidBrush_0.Dispose();
            }
            solidBrush_0 = new SolidBrush(Color.FromArgb(32, 64, 64, 64));
            bitmap_7 = null;
            Color mainColor = Color.Gray;
            Color secondaryColor = Color.Gray;
            if ((builtObject_0.Empire != null && builtObject_0.Empire != game_0.Galaxy.IndependentEmpire) || builtObject_0.ActualEmpire == game_0.PlayerEmpire)
            {
                bitmap_7 = method_16(builtObject_0.ActualEmpire.LargeFlagPicture, 23, 14);
                if (solidBrush_0 != null)
                {
                    solidBrush_0.Dispose();
                }
                solidBrush_0 = new SolidBrush(Color.FromArgb(32, builtObject_0.ActualEmpire.MainColor.R, builtObject_0.ActualEmpire.MainColor.G, builtObject_0.ActualEmpire.MainColor.B));
                mainColor = builtObject_0.ActualEmpire.MainColor;
                secondaryColor = builtObject_0.ActualEmpire.SecondaryColor;
            }
            Bitmap image = main_0.builtObjectImageCache_0.ObtainImage(builtObject_0);
            int pictureRef = builtObject_0.PictureRef;
            if (pictureRef >= bitmap_1.Length)
            {
                bitmap_1 = GraphicsHelper.EnlargeImageArray(bitmap_1, bitmap_1.Length - pictureRef + 1);
            }
            image = main_0.PrepareBuiltObjectImage(builtObject_0, image, mainColor, secondaryColor, 1.0, 1, allowPreRotate: false);
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            int int_ = int_0 * 2;
            double num = (double)image.Width / (double)image.Height;
            int int_2 = (int)((double)int_0 * 2.0 * num);
            image = method_16(image, int_2, int_);
            if (pictureRef >= 0 && pictureRef < bitmap_1.Length)
            {
                if (bitmap_1[pictureRef] != null && bitmap_1[pictureRef].PixelFormat != 0)
                {
                    bitmap_1[pictureRef].Dispose();
                }
                bitmap_1[pictureRef] = image;
            }
        }

        private void method_1()
        {
            if (solidBrush_0 != null)
            {
                solidBrush_0.Dispose();
            }
            solidBrush_0 = new SolidBrush(Color.FromArgb(32, 64, 64, 64));
            bitmap_7 = null;
            Color mainColor = Color.Gray;
            Color secondaryColor = Color.Gray;
            if (fighter_0.Empire != null && fighter_0.Empire != game_0.Galaxy.IndependentEmpire)
            {
                bitmap_7 = method_16(fighter_0.Empire.LargeFlagPicture, 23, 14);
                if (solidBrush_0 != null)
                {
                    solidBrush_0.Dispose();
                }
                solidBrush_0 = new SolidBrush(Color.FromArgb(32, fighter_0.Empire.MainColor.R, fighter_0.Empire.MainColor.G, fighter_0.Empire.MainColor.B));
                mainColor = fighter_0.Empire.MainColor;
                secondaryColor = fighter_0.Empire.SecondaryColor;
            }
            int pictureRef = fighter_0.PictureRef;
            Bitmap image = main_0.bitmap_6[pictureRef];
            image = main_0.PrepareFighterImage(fighter_0, image, mainColor, secondaryColor, 1.0, 1, 1.0);
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            int int_ = int_0 * 2;
            double num = (double)image.Width / (double)image.Height;
            int int_2 = (int)((double)int_0 * 2.0 * num);
            image = method_16(image, int_2, int_);
            if (pictureRef >= 0 && pictureRef < bitmap_2.Length)
            {
                if (bitmap_2[pictureRef] != null && bitmap_2[pictureRef].PixelFormat != 0)
                {
                    bitmap_2[pictureRef].Dispose();
                }
                bitmap_2[pictureRef] = image;
            }
        }

        public Bitmap DrawHoverInfoToImage(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            DrawHoverInfo(graphics, new Rectangle(0, 0, width, height));
            return bitmap;
        }

        public void DrawHoverInfo(Graphics graphics, Rectangle rectangle)
        {
            if (game_0 == null)
            {
                return;
            }
            if (builtObject_0 != null)
            {
                if (bitmap_7 == null && builtObject_0.Empire != null && builtObject_0.Empire != game_0.Galaxy.IndependentEmpire)
                {
                    method_0();
                }
                method_6(graphics, builtObject_0, rectangle);
            }
            else if (fighter_0 != null)
            {
                if (bitmap_7 == null && fighter_0.Empire != null && fighter_0.Empire != game_0.Galaxy.IndependentEmpire)
                {
                    method_1();
                }
                method_5(graphics, fighter_0, rectangle);
            }
            else if (habitat_0 != null)
            {
                if (bitmap_7 == null && habitat_0.Empire != null && habitat_0.Empire != game_0.Galaxy.IndependentEmpire)
                {
                    method_13();
                }
                method_3(graphics, habitat_0, rectangle);
            }
            else if (creature_0 != null)
            {
                method_2(graphics, creature_0, rectangle);
            }
        }

        private void method_2(Graphics graphics_0, Creature creature_1, Rectangle rectangle_0)
        {
            int x = rectangle_0.X;
            int y = rectangle_0.Y;
            Point point = new Point(x, y);
            graphics_0.FillRectangle(solidBrush_0, rectangle_0);
            rectangle_0 = new Rectangle(rectangle_0.X + int_2, rectangle_0.Y + int_2, rectangle_0.Width - int_2 * 2, rectangle_0.Height - int_2 * 2);
            y += int_2;
            x = rectangle_0.X;
            graphics_0.DrawImageUnscaled(point: new Point(x, y), image: bitmap_3[creature_1.PictureRef]);
            x += bitmap_3[creature_1.PictureRef].Width;
            x += int_1;
            SizeF sizeF_ = graphics_0.MeasureString(creature_1.Name, font_2, rectangle_0.Width - x);
            method_11(point_0: new Point(x, y + (int_0 * 2 - (int)sizeF_.Height) / 2), graphics_0: graphics_0, string_0: creature_1.Name, font_3: font_2, brush_0: solidBrush_1, sizeF_0: sizeF_);
            y += int_0 * 2;
            y += int_1;
            x = rectangle_0.X;
            point = new Point(x, y);
            double num = ((double)creature_1.DamageKillThreshhold - creature_1.Damage) / (double)creature_1.DamageKillThreshhold * 100.0;
            string text = TextResolver.GetText("Size") + ": " + creature_1.Size;
            string text2 = text;
            text = text2 + ", " + TextResolver.GetText("Strength") + ": " + creature_1.AttackStrength;
            string text3 = text;
            text = text3 + ", " + TextResolver.GetText("Health") + ": " + num.ToString("#0") + "%";
            method_9(graphics_0, text, font_0, point);
        }

        private void method_3(Graphics graphics_0, Habitat habitat_1, Rectangle rectangle_0)
        {
            int x = rectangle_0.X;
            int y = rectangle_0.Y;
            int num = int_3 + 2;
            Point point = new Point(x, y);
            SystemVisibilityStatus systemVisibilityStatus = game_0.PlayerEmpire.CheckSystemVisibilityStatus(habitat_1.SystemIndex);
            bool flag = false;
            if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                flag = true;
            }
            graphics_0.FillRectangle(solidBrush_0, rectangle_0);
            rectangle_0 = new Rectangle(rectangle_0.X + int_2, rectangle_0.Y + int_2, rectangle_0.Width - int_2 * 2, rectangle_0.Height - int_2 * 2);
            y += int_2;
            x = rectangle_0.X;
            graphics_0.DrawImageUnscaled(point: new Point(x, y), image: bitmap_6);
            x += bitmap_6.Width;
            x += int_1;
            string text = habitat_1.Name;
            if (!flag)
            {
                text = "(" + string.Format(TextResolver.GetText("UnexploredLocation"), Galaxy.ResolveDescription(habitat_1.Category)) + ")";
            }
            SizeF sizeF_ = graphics_0.MeasureString(text, font_2, rectangle_0.Width - x);
            method_11(point_0: new Point(x, y + (int_0 * 2 - (int)sizeF_.Height) / 2), graphics_0: graphics_0, string_0: text, font_3: font_2, brush_0: solidBrush_1, sizeF_0: sizeF_);
            y += int_0 * 2;
            y += int_1;
            x = rectangle_0.X;
            point = new Point(x, y);
            string text2 = Galaxy.ResolveDescription(habitat_1.Type);
            if (habitat_1.Type != HabitatType.BlackHole)
            {
                text2 = text2 + " " + Galaxy.ResolveDescription(habitat_1.Category);
            }
            method_9(graphics_0, text2, font_0, point);
            y += num;
            y += int_1;
            if (flag && (habitat_1.Category == HabitatCategoryType.Planet || habitat_1.Category == HabitatCategoryType.Moon))
            {
                x = rectangle_0.X;
                point = new Point(x, y);
                if (habitat_1.Empire != null && habitat_1.Empire != game_0.Galaxy.IndependentEmpire)
                {
                    graphics_0.DrawImageUnscaled(bitmap_7, point);
                    x += bitmap_7.Width;
                    x += int_1;
                    point = new Point(x, y);
                    using SolidBrush brush_ = new SolidBrush(habitat_1.Empire.MainColor);
                    method_10(point_0: new Point(point.X, point.Y - 2), graphics_0: graphics_0, string_0: habitat_1.Empire.Name, font_3: font_1, brush_0: brush_);
                }
                else
                {
                    point = new Point(x, y);
                    Point point_3 = new Point(point.X, point.Y - 2);
                    if (habitat_1.Empire == game_0.Galaxy.IndependentEmpire && habitat_1.Population.Count > 0)
                    {
                        method_9(graphics_0, TextResolver.GetText("Independent"), font_1, point_3);
                    }
                    else if (habitat_1.Empire == null && habitat_1.Population.Count > 0)
                    {
                        method_9(graphics_0, TextResolver.GetText("Lost Colony"), font_1, point_3);
                    }
                    else
                    {
                        method_9(graphics_0, TextResolver.GetText("Unoccupied"), font_1, point_3);
                    }
                }
                y += int_0;
                y += int_1;
                x = rectangle_0.X;
                point = new Point(x, y);
                if (habitat_1.Population != null && habitat_1.Population.Count > 0)
                {
                    for (int i = 0; i < habitat_1.Population.Count; i++)
                    {
                        Population population = habitat_1.Population[i];
                        point = new Point(x, y);
                        string name = population.Race.Name;
                        int num2 = (int)graphics_0.MeasureString(name, font_0, 200).Width;
                        if (x + num2 + bitmap_4[population.Race.PictureRef].Width <= rectangle_0.Right)
                        {
                            graphics_0.DrawImageUnscaled(bitmap_4[population.Race.PictureRef], point);
                            x += bitmap_4[population.Race.PictureRef].Width;
                            method_9(point_0: new Point(x, y + 2), graphics_0: graphics_0, string_0: name, font_3: font_0);
                            x += num2;
                            x += int_0 / 2;
                            continue;
                        }
                        method_9(graphics_0, "...", font_0, point);
                        break;
                    }
                    y += int_0;
                    y += int_1;
                }
            }
            if (habitat_1.Category == HabitatCategoryType.Planet || habitat_1.Category == HabitatCategoryType.Moon || habitat_1.Category == HabitatCategoryType.Asteroid || habitat_1.Category == HabitatCategoryType.GasCloud)
            {
                x = rectangle_0.X;
                point = new Point(x, y);
                if (habitat_1.Resources != null && habitat_1.Resources.Count > 0)
                {
                    method_4(graphics_0, habitat_1, point, rectangle_0.Width);
                }
            }
        }

        private void method_4(Graphics graphics_0, Habitat habitat_1, Point point_0, int int_4)
        {
            int num = 0;
            Point point_ = new Point(point_0.X + 0, point_0.Y);
            if (game_0.PlayerEmpire.ResourceMap != null && !game_0.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat_1))
            {
                method_9(graphics_0, "(" + TextResolver.GetText("Unknown resources") + ")", font_0, point_);
            }
            else if (habitat_1.Resources != null && habitat_1.Resources.Count != 0)
            {
                int num2 = int_0 / 2;
                HabitatResourceList habitatResourceList = habitat_1.Resources.Clone();
                int num3 = 0;
                while (true)
                {
                    if (num3 < habitatResourceList.Count)
                    {
                        int width = bitmap_5[habitatResourceList[num3].PictureRef].Width;
                        string text = ((double)habitatResourceList[num3].Abundance / 1000.0).ToString("0%");
                        int num4 = (int)graphics_0.MeasureString(text, font_0).Width;
                        point_ = new Point(point_0.X + num, point_0.Y);
                        if (num + width + num4 + num2 > int_4)
                        {
                            break;
                        }
                        graphics_0.DrawImageUnscaled(bitmap_5[habitatResourceList[num3].PictureRef], point_);
                        num += width;
                        method_9(point_0: new Point(point_0.X + num, point_0.Y), graphics_0: graphics_0, string_0: text, font_3: font_0);
                        num += num4;
                        num += num2;
                        num3++;
                        continue;
                    }
                    return;
                }
                method_9(graphics_0, "...", font_0, point_);
            }
            else
            {
                method_9(graphics_0, "(" + TextResolver.GetText("No resources") + ")", font_0, point_);
            }
        }

        private void method_5(Graphics graphics_0, Fighter fighter_1, Rectangle rectangle_0)
        {
            int x = rectangle_0.X;
            int y = rectangle_0.Y;
            Point point = new Point(x, y);
            graphics_0.FillRectangle(solidBrush_0, rectangle_0);
            rectangle_0 = new Rectangle(rectangle_0.X + int_2, rectangle_0.Y + int_2, rectangle_0.Width - int_2 * 2, rectangle_0.Height - int_2 * 2);
            y += int_2;
            x = rectangle_0.X;
            graphics_0.DrawImageUnscaled(point: new Point(x, y), image: bitmap_2[fighter_1.PictureRef]);
            x += bitmap_2[fighter_1.PictureRef].Width;
            x += int_1;
            SizeF sizeF = graphics_0.MeasureString(fighter_1.Name, font_2, rectangle_0.Width - x, StringFormat.GenericTypographic);
            sizeF = new SizeF(sizeF.Width, sizeF.Height + 2f);
            method_11(point_0: new Point(x, y + (int_0 * 2 - (int)sizeF.Height) / 2), graphics_0: graphics_0, string_0: fighter_1.Name, font_3: font_2, brush_0: solidBrush_1, sizeF_0: sizeF);
            y += int_0 * 2;
            y += int_1;
            x = rectangle_0.X;
            point = new Point(x, y);
            if (fighter_1.Empire != null && fighter_1.Empire != game_0.Galaxy.IndependentEmpire)
            {
                graphics_0.DrawImageUnscaled(bitmap_7, point);
                x += bitmap_7.Width;
                x += int_1;
                point = new Point(x, y);
                Color color = fighter_1.Empire.MainColor;
                if (fighter_1.Empire.PirateEmpireBaseHabitat != null && fighter_1.Empire.MainColor == Color.FromArgb(1, 1, 1))
                {
                    color = Color.FromArgb(96, 96, 96);
                }
                using SolidBrush brush_ = new SolidBrush(color);
                method_10(point_0: new Point(point.X, point.Y - 2), graphics_0: graphics_0, string_0: fighter_1.Empire.Name, font_3: font_1, brush_0: brush_);
            }
            else
            {
                point = new Point(x, y);
                Point point_3 = new Point(point.X, point.Y - 2);
                if (fighter_1.Empire == game_0.Galaxy.IndependentEmpire)
                {
                    method_9(graphics_0, TextResolver.GetText("Independent"), font_1, point_3);
                }
                else
                {
                    method_9(graphics_0, TextResolver.GetText("Abandoned"), font_1, point_3);
                }
            }
            y += int_0;
            y += int_1;
            x = rectangle_0.X;
            point = new Point(x, y);
            method_7(graphics_0, fighter_1, point);
        }

        private void method_6(Graphics graphics_0, BuiltObject builtObject_1, Rectangle rectangle_0)
        {
            int x = rectangle_0.X;
            int y = rectangle_0.Y;
            int num = int_3 + 2;
            Point point = new Point(x, y);
            graphics_0.FillRectangle(solidBrush_0, rectangle_0);
            rectangle_0 = new Rectangle(rectangle_0.X + int_2, rectangle_0.Y + int_2, rectangle_0.Width - int_2 * 2, rectangle_0.Height - int_2 * 2);
            y += int_2;
            Bitmap bitmap = null;
            if (builtObject_1.PictureRef >= 0 && builtObject_1.PictureRef < bitmap_1.Length)
            {
                bitmap = bitmap_1[builtObject_1.PictureRef];
            }
            x = rectangle_0.X;
            point = new Point(x, y);
            if (bitmap != null && bitmap.PixelFormat != 0)
            {
                graphics_0.DrawImageUnscaled(bitmap, point);
                x += bitmap.Width;
                x += int_1;
            }
            SizeF sizeF = graphics_0.MeasureString(builtObject_1.Name, font_2, rectangle_0.Width - x, StringFormat.GenericTypographic);
            sizeF = new SizeF(sizeF.Width, sizeF.Height + 2f);
            method_11(point_0: new Point(x, y + (int_0 * 2 - (int)sizeF.Height) / 2), graphics_0: graphics_0, string_0: builtObject_1.Name, font_3: font_2, brush_0: solidBrush_1, sizeF_0: sizeF);
            y += int_0 * 2;
            y += int_1;
            x = rectangle_0.X;
            point = new Point(x, y);
            string text = Galaxy.ResolveDescription(builtObject_1.SubRole);
            if (builtObject_1.Empire == game_0.PlayerEmpire && builtObject_1.ShipGroup != null && game_0.SelectedObject != builtObject_1.ShipGroup)
            {
                text = text + " (" + TextResolver.GetText("double-click to select fleet") + ")";
            }
            method_9(graphics_0, text, font_0, point);
            y += num;
            y += int_1;
            x = rectangle_0.X;
            point = new Point(x, y);
            if ((builtObject_1.Empire != null && builtObject_1.Empire != game_0.Galaxy.IndependentEmpire) || builtObject_1.ActualEmpire == game_0.PlayerEmpire)
            {
                if (bitmap_7 != null && bitmap_7.PixelFormat != 0)
                {
                    graphics_0.DrawImageUnscaled(bitmap_7, point);
                    x += bitmap_7.Width;
                    x += int_1;
                }
                point = new Point(x, y);
                Color color = builtObject_1.ActualEmpire.MainColor;
                if (builtObject_1.ActualEmpire.PirateEmpireBaseHabitat != null && builtObject_1.ActualEmpire.MainColor == Color.FromArgb(1, 1, 1))
                {
                    color = Color.FromArgb(96, 96, 96);
                }
                using SolidBrush brush_ = new SolidBrush(color);
                method_10(point_0: new Point(point.X, point.Y - 2), graphics_0: graphics_0, string_0: builtObject_1.ActualEmpire.Name, font_3: font_1, brush_0: brush_);
            }
            else
            {
                point = new Point(x, y);
                Point point_3 = new Point(point.X, point.Y - 2);
                if (builtObject_1.Empire == game_0.Galaxy.IndependentEmpire)
                {
                    method_9(graphics_0, TextResolver.GetText("Independent"), font_1, point_3);
                }
                else
                {
                    method_9(graphics_0, TextResolver.GetText("Abandoned"), font_1, point_3);
                }
            }
            y += int_0;
            y += int_1;
            x = rectangle_0.X;
            point = new Point(x, y);
            method_8(graphics_0, builtObject_1, point);
        }

        private void method_7(Graphics graphics_0, Fighter fighter_1, Point point_0)
        {
            bool flag = false;
            if (fighter_1.Empire != game_0.PlayerEmpire && game_0.PlayerEmpire.EmpiresViewable.Contains(fighter_1.Empire))
            {
                flag = true;
            }
            if (fighter_1.Empire != game_0.PlayerEmpire && !game_0.GodMode && !flag)
            {
                method_9(graphics_0, "(" + TextResolver.GetText("Unknown mission") + ")", font_0, point_0);
                return;
            }
            Empire empire = fighter_1.Empire;
            if (empire == null)
            {
                empire = game_0.Galaxy.IndependentEmpire;
            }
            string text = Galaxy.ResolveMissionDescription(fighter_1);
            Rectangle rectangle_ = main_0.rectangle_0;
            SizeF sizeF = graphics_0.MeasureString(text, font_0, rectangle_.Width - point_0.X, StringFormat.GenericTypographic);
            method_11(sizeF_0: new SizeF(sizeF.Width, sizeF.Height + 2f), graphics_0: graphics_0, string_0: text, font_3: font_0, point_0: point_0, brush_0: solidBrush_1);
        }

        private void method_8(Graphics graphics_0, BuiltObject builtObject_1, Point point_0)
        {
            if (builtObject_1.Role == BuiltObjectRole.Base)
            {
                return;
            }
            bool flag = false;
            if (builtObject_1.ActualEmpire != game_0.PlayerEmpire && game_0.PlayerEmpire.EmpiresViewable.Contains(builtObject_1.ActualEmpire))
            {
                flag = true;
            }
            bool flag2 = false;
            if (builtObject_1.ActualEmpire != game_0.PlayerEmpire && builtObject_1.Role != BuiltObjectRole.Military && builtObject_1.Mission != null)
            {
                if (builtObject_1.Mission.TargetBuiltObject != null && builtObject_1.Mission.TargetBuiltObject.Empire == game_0.PlayerEmpire)
                {
                    flag2 = true;
                }
                if (builtObject_1.Mission.TargetHabitat != null && builtObject_1.Mission.TargetHabitat.Empire == game_0.PlayerEmpire)
                {
                    flag2 = true;
                }
                if (builtObject_1.Mission.SecondaryTargetBuiltObject != null && builtObject_1.Mission.SecondaryTargetBuiltObject.Empire == game_0.PlayerEmpire)
                {
                    flag2 = true;
                }
                if (builtObject_1.Mission.SecondaryTargetHabitat != null && builtObject_1.Mission.SecondaryTargetHabitat.Empire == game_0.PlayerEmpire)
                {
                    flag2 = true;
                }
            }
            if (builtObject_1.ActualEmpire != game_0.PlayerEmpire && !game_0.GodMode && !flag && !flag2 && !bool_0)
            {
                method_9(graphics_0, "(" + TextResolver.GetText("Unknown mission") + ")", font_0, point_0);
                return;
            }
            Empire empire = builtObject_1.ActualEmpire;
            if (empire == null)
            {
                empire = game_0.Galaxy.IndependentEmpire;
            }
            string text = Galaxy.ResolveDescription(empire, builtObject_1.Mission);
            Rectangle rectangle_ = main_0.rectangle_0;
            SizeF sizeF = graphics_0.MeasureString(text, font_0, rectangle_.Width - point_0.X, StringFormat.GenericTypographic);
            sizeF = new SizeF(sizeF.Width, sizeF.Height + 2f);
            if (builtObject_1.Mission != null && builtObject_1.Mission.Type != 0)
            {
                method_11(graphics_0, text, font_0, point_0, solidBrush_1, sizeF);
            }
            else
            {
                method_9(graphics_0, "(" + TextResolver.GetText("No mission") + ")", font_0, point_0);
            }
            if (builtObject_1.Role == BuiltObjectRole.Military)
            {
                if (builtObject_1.AttackRangeSquared == 0f)
                {
                    text = text + " (" + TextResolver.GetText("Engage when attacked") + ")";
                }
                else if (builtObject_1.AttackRangeSquared == 4000000f)
                {
                    text = text + " (" + TextResolver.GetText("Engage nearby targets") + ")";
                }
                else if (builtObject_1.AttackRangeSquared == 2.304E+09f)
                {
                    text = text + " (" + TextResolver.GetText("Engage system targets") + ")";
                }
                else
                {
                    text = text + " (" + TextResolver.GetText("Engage detected targets") + ")";
                }
            }
        }

        private void method_9(Graphics graphics_0, string string_0, Font font_3, Point point_0)
        {
            method_10(graphics_0, string_0, font_3, point_0, solidBrush_1);
        }

        private void method_10(Graphics graphics_0, string string_0, Font font_3, Point point_0, Brush brush_0)
        {
            method_11(graphics_0, string_0, font_3, point_0, brush_0, SizeF.Empty);
        }

        private void method_11(Graphics graphics_0, string string_0, Font font_3, Point point_0, Brush brush_0, SizeF sizeF_0)
        {
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(layoutRectangle: new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height), s: string_0, font: font_3, brush: solidBrush_2, format: StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                RectangleF layoutRectangle2 = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_3, brush_0, layoutRectangle2, StringFormat.GenericTypographic);
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_3, solidBrush_2, point_0);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_3, brush_0, point_0);
            }
        }

        public void ClearData()
        {
            game_0 = null;
            builtObject_0 = null;
            habitat_0 = null;
            creature_0 = null;
            fighter_0 = null;
        }

        private void method_12()
        {
            if (_FontCache != null)
            {
                Font font = font_0;
                Font font2 = font_2;
                font_0 = _FontCache.GenerateFont(18.67f, isBold: false);
                font_1 = _FontCache.GenerateFont(18.67f, isBold: true);
                font_2 = _FontCache.GenerateFont(22.67f, isBold: true);
                int_3 = font_0.Height;
                font?.Dispose();
                font2?.Dispose();
            }
        }

        public void SetData(Game game, BuiltObject builtObject)
        {
            method_12();
            game_0 = game;
            builtObject_0 = builtObject;
            fighter_0 = null;
            habitat_0 = null;
            creature_0 = null;
            bool_0 = game_0.Galaxy.CheckBuiltObjectScanned(builtObject_0);
            method_0();
        }

        public void SetData(Game game, Fighter fighter)
        {
            method_12();
            game_0 = game;
            builtObject_0 = null;
            fighter_0 = fighter;
            habitat_0 = null;
            creature_0 = null;
            method_0();
        }

        public void SetData(Game game, Habitat habitat, Bitmap habitatImage)
        {
            method_12();
            game_0 = game;
            builtObject_0 = null;
            fighter_0 = null;
            habitat_0 = habitat;
            creature_0 = null;
            method_13();
            if (habitatImage == null || habitatImage.PixelFormat == PixelFormat.Undefined || (habitatImage.Width < int_0 && habitat_0.Category != 0))
            {
                habitatImage = main_0.habitatImageCache_0.ObtainImage(habitat);
            }
            double num = (double)habitatImage.Width / (double)habitatImage.Height;
            int int_ = (int)((double)int_0 * 2.0 * num);
            bitmap_6 = method_16(habitatImage, int_, int_0 * 2);
        }

        private void method_13()
        {
            if (solidBrush_0 != null)
            {
                solidBrush_0.Dispose();
            }
            solidBrush_0 = new SolidBrush(Color.FromArgb(32, 64, 64, 64));
            bitmap_7 = null;
            if (habitat_0.Empire != null && habitat_0.Empire != game_0.Galaxy.IndependentEmpire)
            {
                bitmap_7 = method_16(habitat_0.Empire.LargeFlagPicture, 23, 14);
                if (solidBrush_0 != null)
                {
                    solidBrush_0.Dispose();
                }
                solidBrush_0 = new SolidBrush(Color.FromArgb(32, habitat_0.Empire.MainColor.R, habitat_0.Empire.MainColor.G, habitat_0.Empire.MainColor.B));
            }
        }

        public void SetData(Game game, Creature creature)
        {
            method_12();
            game_0 = game;
            builtObject_0 = null;
            fighter_0 = null;
            habitat_0 = null;
            creature_0 = creature;
            if (solidBrush_0 != null)
            {
                solidBrush_0.Dispose();
            }
            solidBrush_0 = new SolidBrush(Color.FromArgb(32, 64, 64, 64));
            bitmap_7 = null;
        }

        private void method_14()
        {
            method_15(bitmap_0);
            method_15(bitmap_1);
            method_15(bitmap_3);
            method_15(bitmap_4);
            method_15(bitmap_5);
        }

        private void method_15(Bitmap[] bitmap_8)
        {
            if (bitmap_8 == null)
            {
                return;
            }
            for (int i = 0; i < bitmap_8.Length; i++)
            {
                if (bitmap_8[i] != null)
                {
                    bitmap_8[i].Dispose();
                    bitmap_8[i] = null;
                }
            }
        }

        public void InitializeImages(Main parentForm, Bitmap[] builtObjectImages, Bitmap[] habitatImages, Bitmap[][] creatureImages, Bitmap[] raceImages, Bitmap[] resourceImages)
        {
            main_0 = parentForm;
            double num = 0.0;
            int num2 = 0;
            method_14();
            bitmap_1 = new Bitmap[builtObjectImages.Length];
            for (int i = 0; i < builtObjectImages.Length; i++)
            {
                num = (double)builtObjectImages[i].Width / (double)builtObjectImages[i].Height;
                num2 = (int)((double)int_0 * num);
                bitmap_1[i] = method_16(builtObjectImages[i], num2, int_0);
            }
            bitmap_0 = new Bitmap[habitatImages.Length];
            for (int j = 0; j < habitatImages.Length; j++)
            {
                num = (double)habitatImages[j].Width / (double)habitatImages[j].Height;
                num2 = (int)((double)int_0 * 2.0 * num);
                bitmap_0[j] = method_16(habitatImages[j], num2, int_0 * 2);
            }
            bitmap_3 = new Bitmap[creatureImages.Length];
            for (int k = 0; k < creatureImages.Length; k++)
            {
                if (creatureImages[k] != null && creatureImages[k].Length > 0)
                {
                    num = (double)creatureImages[k][0].Width / (double)creatureImages[k][0].Height;
                    num2 = (int)((double)int_0 * 2.0 * num);
                    bitmap_3[k] = method_16(creatureImages[k][0], num2, int_0 * 2);
                    bitmap_3[k].RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
            }
            bitmap_4 = new Bitmap[raceImages.Length];
            for (int l = 0; l < raceImages.Length; l++)
            {
                num = (double)raceImages[l].Width / (double)raceImages[l].Height;
                num2 = (int)((double)int_0 * num);
                bitmap_4[l] = method_16(raceImages[l], num2, int_0);
            }
            bitmap_5 = new Bitmap[resourceImages.Length];
            for (int m = 0; m < resourceImages.Length; m++)
            {
                num = (double)resourceImages[m].Width / (double)resourceImages[m].Height;
                num2 = (int)((double)int_0 * num);
                bitmap_5[m] = method_16(resourceImages[m], num2, int_0);
            }
        }

        private Bitmap method_16(Bitmap bitmap_8, int int_4, int int_5)
        {
            if (int_4 < 1)
            {
                int_4 = 1;
            }
            if (int_5 < 1)
            {
                int_5 = 1;
            }
            Bitmap bitmap = new Bitmap(int_4, int_5, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            new Rectangle(0, 0, bitmap_8.Width, bitmap_8.Height);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            graphics.DrawImage(bitmap_8, rect);
            return bitmap;
        }

        public HoverPanel():base()
        {
            
            int_0 = 16;
            int_1 = 5;
            int_2 = 6;
            solidBrush_1 = new SolidBrush(Color.White);
            solidBrush_2 = new SolidBrush(Color.Black);
            font_0 = new Font("Verdana", 8f);
            font_1 = new Font("Verdana", 8f);
            font_2 = new Font("Verdana", 10f, FontStyle.Bold);
            LastRefresh = DateTime.MinValue;
        }
    }

}
