// Decompiled with JetBrains decompiler
// Type: DistantWorlds.ItemListPanel
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds
{
    [Serializable]
    public class ItemListPanel
    {
        public ItemListCollectionPanel Container;

        public Type ItemType;

        public string TitleText;

        public Bitmap IconImage;

        public Bitmap IconImageOriginal;

        public int TitleBarHeight;

        public int ToggleButtonHeight;

        public int ScrollUpHeight;

        public int ScrollDownHeight;

        public int ItemHeight;

        public int ItemGap;

        public float ItemHeightOverrideFactor;

        public int DefaultTitleBarHeight;

        public int DefaultToggleButtonHeight;

        public int DefaultScrollUpHeight;

        public int DefaultScrollDownHeight;

        public int DefaultItemHeight;

        private bool bool_0;

        private bool bool_1;

        private bool bool_2;

        private bool bool_3;

        private int int_0;

        private bool bool_4;

        private bool bool_5;

        private bool bool_6;

        private object object_0;

        public bool ToggleButtonEnabled;

        private List<int> list_0;

        private List<string[]> list_1;

        public object[] Items;

        public int ScrollPosition;

        public int ScrollAmountPerClick;

        public int DefaultScrollAmountPerClick;

        public Bitmap[] EmpireFlagImages;

        public Bitmap PirateFlag;

        private bool bool_7;

        private bool bool_8;

        public bool ColonizationFocus;

        public bool MiningFocus;

        public bool BidButtonHoveredForCurrentItem => bool_6;

        public int GetToggleButtonState(int toggleButtonIndex)
        {
            if (toggleButtonIndex >= 0 && toggleButtonIndex < list_0.Count)
            {
                return list_0[toggleButtonIndex];
            }
            return -1;
        }

        public void SetToggleButtons(List<string[]> buttonText)
        {
            list_1 = buttonText;
            list_0.Clear();
            for (int i = 0; i < list_1.Count; i++)
            {
                list_0.Add(0);
            }
            if (buttonText.Count > 0)
            {
                ToggleButtonEnabled = true;
            }
            else
            {
                ToggleButtonEnabled = false;
            }
        }

        public ItemListPanel(string title, Bitmap iconImage, Type itemType) : this(title, iconImage, itemType, new List<string[]>())
        {
            
        }

        public ItemListPanel(string title, Bitmap iconImage, Type itemType, List<string[]> toggleButtonText):base()
        {
            
            TitleBarHeight = 18;
            ToggleButtonHeight = 14;
            ScrollUpHeight = 14;
            ScrollDownHeight = 14;
            ItemHeight = 41;
            ItemGap = 2;
            ItemHeightOverrideFactor = 1f;
            DefaultTitleBarHeight = 18;
            DefaultToggleButtonHeight = 14;
            DefaultScrollUpHeight = 14;
            DefaultScrollDownHeight = 14;
            DefaultItemHeight = 41;
            int_0 = -1;
            list_0 = new List<int>();
            list_1 = new List<string[]>();
            ScrollAmountPerClick = 25;
            DefaultScrollAmountPerClick = 25;
            TitleText = title;
            IconImageOriginal = iconImage;
            IconImage = iconImage;
            ItemType = itemType;
            SetToggleButtons(toggleButtonText);
        }

        public void Initialize(ItemListCollectionPanel container, Type itemType)
        {
            ClearHoverState();
            Container = container;
            ItemType = itemType;
        }

        private void method_0(Bitmap[] bitmap_0)
        {
            if (bitmap_0 == null)
            {
                return;
            }
            for (int i = 0; i < bitmap_0.Length; i++)
            {
                if (bitmap_0[i] != null)
                {
                    bitmap_0[i].Dispose();
                    bitmap_0[i] = null;
                }
            }
        }

        public void BindData(object[] items)
        {
            bool flag = true;
            if (items != null && items.Length > 0)
            {
                flag = false;
                if (items[0].GetType() == ItemType)
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                return;
            }
            bool flag2 = true;
            if (items != null && Items != null && items.Length == Items.Length)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] is PrioritizedTarget)
                    {
                        object target = ((PrioritizedTarget)items[i]).Target;
                        object target2 = ((PrioritizedTarget)Items[i]).Target;
                        if (target != target2)
                        {
                            flag2 = false;
                            break;
                        }
                    }
                    else if (items[i] is HabitatPrioritization)
                    {
                        Habitat habitat = ((HabitatPrioritization)items[i]).Habitat;
                        Habitat habitat2 = ((HabitatPrioritization)Items[i]).Habitat;
                        if (habitat != habitat2)
                        {
                            flag2 = false;
                            break;
                        }
                    }
                    else if (items[i] != Items[i])
                    {
                        flag2 = false;
                        break;
                    }
                }
            }
            else
            {
                flag2 = false;
            }
            if (ItemType == typeof(EmpireActivity) && Items != null)
            {
                for (int j = 0; j < Items.Length; j++)
                {
                    EmpireActivity empireActivity = (EmpireActivity)Items[j];
                    empireActivity.DisplayExtraData = -1;
                }
            }
            if (flag2)
            {
                return;
            }
            ClearHoverState();
            Galaxy galaxy = null;
            if (Container != null && Container.Parent != null && Container.Parent._Game != null)
            {
                galaxy = Container.Parent._Game.Galaxy;
            }
            if (galaxy != null)
            {
                bool_7 = galaxy.PlayerEmpire.CheckEmpireTechCanSurviveStorms();
                bool_8 = galaxy.PlayerEmpire.CheckConstructionShipAndMiningStationCanSurviveStorms();
            }
            Items = items;
            int num = TitleBarHeight + 1 + ScrollUpHeight + 1 + ScrollDownHeight + 4;
            if (ToggleButtonEnabled)
            {
                num += ToggleButtonHeight + 1;
            }
            int val = Math.Max(0, Items.Length * (ItemHeight + ItemGap) - ItemGap - (Container.Area.Height - num));
            ScrollPosition = Math.Min(val, Math.Max(0, ScrollPosition));
            if (!(ItemType == typeof(PrioritizedTarget)) || Container.Parent._Game == null || Container.Parent._Game.Galaxy == null)
            {
                return;
            }
            if (PirateFlag != null)
            {
                PirateFlag.Dispose();
            }
            PirateFlag = null;
            method_0(EmpireFlagImages);
            EmpireFlagImages = new Bitmap[Container.Parent._Game.Galaxy.NextEmpireID + 1];
            int num2 = (int)(50f * Container.SizeFactor);
            int int_ = (int)(30f * Container.SizeFactor);
            PirateFlag = Container.method_3(Container.Parent.bitmap_49, num2, int_, Container.AlphaTransparency);
            for (int k = 0; k < Container.Parent._Game.Galaxy.PirateEmpires.Count; k++)
            {
                Empire empire = Container.Parent._Game.Galaxy.PirateEmpires[k];
                if (empire != null)
                {
                    EmpireFlagImages[empire.EmpireId] = Container.method_3(empire.LargeFlagPicture, num2, int_, Container.AlphaTransparency);
                }
            }
            for (int l = 0; l < Container.Parent._Game.Galaxy.Empires.Count; l++)
            {
                Empire empire2 = Container.Parent._Game.Galaxy.Empires[l];
                if (empire2 != null)
                {
                    EmpireFlagImages[empire2.EmpireId] = Container.method_3(empire2.LargeFlagPicture, num2, int_, Container.AlphaTransparency);
                }
            }
        }

        private Color method_1(Habitat habitat_0, bool bool_9, bool bool_10, Galaxy galaxy_0, out string string_0)
        {
            Color result = Color.FromArgb(170, 170, 170);
            string_0 = string.Empty;
            SystemInfo systemInfo = galaxy_0.Systems[habitat_0.SystemIndex];
            Empire empire = null;
            if (systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null)
            {
                empire = systemInfo.DominantEmpire.Empire;
            }
            bool canColonizeBecauseAtWar = false;
            bool flag = true;
            bool flag2 = true;
            if (bool_9)
            {
                flag = galaxy_0.CheckEmpireTerritoryCanColonizeHabitat(galaxy_0.PlayerEmpire, habitat_0, out canColonizeBecauseAtWar);
                flag2 = galaxy_0.PlayerEmpire.CanEmpireColonizeHabitatRange(galaxy_0.PlayerEmpire, habitat_0);
            }
            else
            {
                flag = galaxy_0.CheckEmpireTerritoryCanBuildAtHabitat(galaxy_0.PlayerEmpire, habitat_0);
            }
            if (!flag2)
            {
                result = Color.FromArgb(255, 0, 0);
                string_0 = TextResolver.GetText("Too far from existing colonies");
            }
            else if (habitat_0.Ruin != null && habitat_0.Ruin.PlayerEmpireEncountered && (habitat_0.Ruin.BonusDefensive > 0.0 || habitat_0.Ruin.BonusDiplomacy > 0.0 || habitat_0.Ruin.BonusHappiness > 0.0 || habitat_0.Ruin.BonusResearchEnergy > 0.0 || habitat_0.Ruin.BonusResearchHighTech > 0.0 || habitat_0.Ruin.BonusResearchWeapons > 0.0 || habitat_0.Ruin.BonusWealth > 0.0))
            {
                result = Color.FromArgb(96, 96, 255);
                string_0 = TextResolver.GetText("Special Ruins");
            }
            else if (habitat_0.Resources != null && habitat_0.Resources.HasSuperLuxuryResources() && galaxy_0 != null && galaxy_0.PlayerEmpire != null && galaxy_0.PlayerEmpire.ResourceMap != null && galaxy_0.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat_0))
            {
                result = Color.FromArgb(96, 96, 255);
                string_0 = TextResolver.GetText("Special Luxury Resources");
            }
            else if (galaxy_0.PlayerEmpire.CheckNearPirateBase(habitat_0, habitat_0.Xpos, habitat_0.Ypos))
            {
                result = Color.FromArgb(255, 255, 0);
                string_0 = TextResolver.GetText("Pirate base in this system");
            }
            else if (bool_9 && empire == galaxy_0.PlayerEmpire && habitat_0.Quality >= 0.5f)
            {
                result = Color.FromArgb(0, 255, 0);
                string_0 = TextResolver.GetText("In our system");
            }
            else if (!bool_9 && empire == galaxy_0.PlayerEmpire)
            {
                result = Color.FromArgb(0, 255, 0);
                string_0 = TextResolver.GetText("In our system");
            }
            else if (bool_9 && habitat_0 != null && canColonizeBecauseAtWar && flag)
            {
                result = Color.FromArgb(255, 0, 0);
                string_0 = TextResolver.GetText("In another empire's system");
            }
            else if (bool_10 && habitat_0 != null && !flag)
            {
                result = Color.FromArgb(255, 0, 0);
                string_0 = TextResolver.GetText("In another empire's system");
            }
            else if (bool_9 && galaxy_0.CheckColonizationLikeliness(habitat_0, galaxy_0.PlayerEmpire.DominantRace) <= -5)
            {
                result = Color.FromArgb(255, 255, 0);
                string_0 = TextResolver.GetText("Hostile population");
            }
            else if (bool_9 && !bool_7 && galaxy_0.CheckInStorm(habitat_0.Xpos, habitat_0.Ypos))
            {
                result = Color.FromArgb(255, 128, 0);
                string_0 = TextResolver.GetText("Galactic storm");
            }
            else if (!bool_9 && !bool_8 && galaxy_0.CheckInStorm(habitat_0.Xpos, habitat_0.Ypos))
            {
                result = Color.FromArgb(255, 128, 0);
                string_0 = TextResolver.GetText("Galactic storm");
            }
            else if (bool_9 && habitat_0.Quality < 0.5f)
            {
                result = Color.FromArgb(255, 128, 0);
                string_0 = TextResolver.GetText("Low quality - poor colonization");
            }
            else if (galaxy_0.PlayerEmpire.CheckWhetherHabitatIsDangerous(habitat_0))
            {
                result = Color.FromArgb(255, 255, 0);
                string_0 = TextResolver.GetText("Nearby pirates or space monsters");
            }
            return result;
        }

        private void method_2(Graphics graphics_0, Point point_0, bool bool_9)
        {
            Color color = Color.FromArgb(160, 170, 170, 170);
            if (bool_9)
            {
                color = Color.FromArgb(220, 220, 220, 220);
            }
            int num = TitleBarHeight - 10;
            using Pen pen = new Pen(color, 2f);
            graphics_0.DrawLine(pen, point_0.X, point_0.Y, point_0.X + num, point_0.Y + num);
            graphics_0.DrawLine(pen, point_0.X, point_0.Y + num, point_0.X + num, point_0.Y);
        }

        private void method_3(Graphics graphics_0, Point point_0, bool bool_9)
        {
            Color color = Color.FromArgb(160, 170, 170, 170);
            if (bool_9)
            {
                color = Color.FromArgb(220, 220, 220, 220);
            }
            int num = TitleBarHeight - 10;
            int num2 = (int)((float)num * 0.6f);
            using Pen pen = new Pen(color, 2f);
            using Pen pen2 = new Pen(color, 1f);
            graphics_0.DrawLine(pen, point_0.X, point_0.Y + num, point_0.X + num, point_0.Y);
            graphics_0.DrawLine(pen2, point_0.X, point_0.Y + num, point_0.X, point_0.Y + num - num2);
            graphics_0.DrawLine(pen2, point_0.X, point_0.Y + num, point_0.X + num2, point_0.Y + num);
            graphics_0.DrawLine(pen2, point_0.X + num, point_0.Y, point_0.X + num - num2, point_0.Y);
            graphics_0.DrawLine(pen2, point_0.X + num, point_0.Y, point_0.X + num, point_0.Y + num2);
        }

        private void method_4(Graphics graphics_0, Point point_0, int int_1, int int_2, Color color_0, Color color_1)
        {
            for (int i = 0; i < list_1.Count; i++)
            {
                Rectangle rect = new Rectangle(point_0.X, point_0.Y + i * (ToggleButtonHeight + 1), int_1 - 1, ToggleButtonHeight);
                Color color = color_0;
                if (int_2 == i)
                {
                    color = color_1;
                }
                using (SolidBrush brush = new SolidBrush(color))
                {
                    graphics_0.FillRectangle(brush, rect);
                }
                int num = list_0[i];
                string text = string.Empty;
                if (list_1.Count > i && num >= 0 && list_1[i].Length > num)
                {
                    text = list_1[i][num];
                }
                SizeF sizeF = graphics_0.MeasureString(text, Container.SmallFont, int_1);
                method_12(point_0: new Point(point_0.X + (int_1 - (int)sizeF.Width) / 2, rect.Y), graphics_0: graphics_0, string_0: text, font_0: Container.SmallFont);
            }
        }

        public void DrawPanel(Graphics graphics)
        {
            DrawPanel(graphics, Container.ActivePanelArea.X, Container.ActivePanelArea.Y, Container.ActivePanelArea.Width, Container.ActivePanelArea.Height);
        }

        public void DrawPanel(Graphics graphics, int x, int y, int width, int height)
        {
            Container.Parent.method_112(graphics, GraphicsQuality.Low);
            Galaxy galaxy_ = null;
            if (Container.Parent._Game != null)
            {
                galaxy_ = Container.Parent._Game.Galaxy;
            }
            Color color = Color.FromArgb(80, 170, 170, 170);
            Color color2 = Color.FromArgb(80, 64, 64, 64);
            Font font_ = Container.Font;
            if (Container.ActivePanel == this || bool_0)
            {
                color = Color.FromArgb(160, 170, 170, 170);
                color2 = Color.FromArgb(160, 64, 64, 64);
                font_ = Container.BoldFont;
            }
            Rectangle rect = new Rectangle(x, y, width - 1, TitleBarHeight);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, color, color2, LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(brush, rect);
            }
            int num = x;
            if (IconImage != null)
            {
                graphics.DrawImage(IconImage, new Point(x + 2, y + 2));
                num += 2 + IconImage.Width + 3;
            }
            string text = TitleText;
            if (Items != null && Items.Length > 0)
            {
                text = text + " (" + Items.Length + ")";
            }
            method_12(graphics, text, font_, new Point(num, y - 1));
            method_3(graphics, new Point(x + width - (TitleBarHeight * 2 - 4), y + 5), bool_2);
            method_2(graphics, new Point(x + width - (TitleBarHeight - 4), y + 5), bool_1);
            if (ToggleButtonEnabled)
            {
                method_4(graphics, new Point(x, y + TitleBarHeight + 1), width, int_0, Color.FromArgb(80, 170, 170, 170), Color.FromArgb(144, 170, 170, 170));
            }
            if (Container.ActivePanel == this)
            {
                int num2 = height - (TitleBarHeight + 1 + ScrollUpHeight + 1 + ScrollDownHeight);
                int num3 = height - (TitleBarHeight + 1 + ScrollUpHeight + 1 + ScrollDownHeight + 3);
                int num4 = y + TitleBarHeight + 1 + ScrollUpHeight + 2;
                int num5 = (ToggleButtonHeight + 1) * list_1.Count;
                if (ToggleButtonEnabled)
                {
                    num2 -= num5;
                    num3 -= num5;
                    num4 += num5;
                }
                Rectangle rectangle_ = new Rectangle(x, num4, width, num3);
                method_5(graphics, rectangle_, galaxy_);
                bool flag = false;
                bool flag2 = false;
                if (ScrollPosition > 0)
                {
                    flag = true;
                }
                if (ScrollPosition < Items.Length * (ItemHeight + ItemGap) - num2)
                {
                    flag2 = true;
                }
                Color color3 = Color.FromArgb(80, 170, 170, 170);
                if (bool_4 && flag)
                {
                    color3 = Color.FromArgb(144, 170, 170, 170);
                }
                int num6 = y + TitleBarHeight + 1;
                if (ToggleButtonEnabled)
                {
                    num6 += num5;
                }
                Rectangle rect2 = new Rectangle(x, num6, width - 1, ScrollUpHeight);
                using (SolidBrush brush2 = new SolidBrush(color3))
                {
                    graphics.FillRectangle(brush2, rect2);
                }
                if (flag)
                {
                    Bitmap scrollUpImage = Container.ScrollUpImage;
                    Point point = new Point(x + (width - scrollUpImage.Width) / 2, num6 + (ScrollUpHeight - scrollUpImage.Height) / 2);
                    graphics.DrawImage(scrollUpImage, point);
                }
                Color color4 = Color.FromArgb(80, 170, 170, 170);
                if (bool_5 && flag2)
                {
                    color4 = Color.FromArgb(144, 170, 170, 170);
                }
                int num7 = y + TitleBarHeight + 1 + ScrollUpHeight + num2;
                if (ToggleButtonEnabled)
                {
                    num7 += num5;
                }
                Rectangle rect3 = new Rectangle(x, num7, width - 1, ScrollDownHeight);
                using (SolidBrush brush3 = new SolidBrush(color4))
                {
                    graphics.FillRectangle(brush3, rect3);
                }
                if (flag2)
                {
                    Bitmap scrollDownImage = Container.ScrollDownImage;
                    Point point2 = new Point(x + (width - scrollDownImage.Width) / 2, num7 + 1 + (ScrollDownHeight - scrollDownImage.Height) / 2);
                    graphics.DrawImage(scrollDownImage, point2);
                }
            }
            Container.Parent.method_112(graphics, GraphicsQuality.High);
        }

        private void method_5(Graphics graphics_0, Rectangle rectangle_0, Galaxy galaxy_0)
        {
            if (Items != null && Items.Length > 0)
            {
                graphics_0.SetClip(rectangle_0);
                int num = method_10(ScrollPosition);
                int num2 = method_10(ScrollPosition + rectangle_0.Height);
                if (num < Items.Length)
                {
                    int int_ = rectangle_0.Left + 1;
                    int num3 = rectangle_0.Top + num * (ItemHeight + ItemGap);
                    num3 -= ScrollPosition;
                    int int_2 = rectangle_0.Width - 2;
                    int itemHeight = ItemHeight;
                    for (int i = num; i < Items.Length && i <= num2; i++)
                    {
                        method_6(graphics_0, Items[i], int_, num3, int_2, itemHeight, galaxy_0);
                        num3 += ItemHeight + ItemGap;
                    }
                }
                graphics_0.ResetClip();
                return;
            }
            string text = "(" + string.Format(TextResolver.GetText("No ITEMS"), TitleText) + ")";
            SizeF sizeF = graphics_0.MeasureString(text, Container.LargeBoldFont, rectangle_0.Width, StringFormat.GenericTypographic);
            int num4 = (rectangle_0.Width - (int)sizeF.Width) / 2;
            int num5 = (rectangle_0.Height - (int)sizeF.Height) / 2;
            Point point_ = new Point(rectangle_0.X + num4, rectangle_0.Y + num5);
            using SolidBrush brush_ = new SolidBrush(Color.FromArgb(96, 170, 170, 170));
            sizeF = new SizeF(sizeF.Width + 2f, sizeF.Height + 2f);
            method_14(graphics_0, text, Container.LargeBoldFont, point_, brush_, sizeF, bool_9: false);
        }

        private void method_6(Graphics graphics_0, object object_1, int int_1, int int_2, int int_3, int int_4, Galaxy galaxy_0)
        {
            int imageSize = Container.ImageSize;
            int num = (int)(3f * Container.SizeFactor);
            int num2 = (int)(4f * Container.SizeFactor);
            int num3 = (int)(5f * Container.SizeFactor);
            int num4 = (int)(6f * Container.SizeFactor);
            int num5 = (int)(8f * Container.SizeFactor);
            int num6 = (int)(10f * Container.SizeFactor);
            int num7 = (int)(11f * Container.SizeFactor);
            int num8 = (int)(12f * Container.SizeFactor);
            int num9 = (int)(14f * Container.SizeFactor);
            int num10 = (int)(15f * Container.SizeFactor);
            int num11 = (int)(16f * Container.SizeFactor);
            int num12 = (int)(19f * Container.SizeFactor);
            int num13 = (int)(20f * Container.SizeFactor);
            int num14 = (int)(22f * Container.SizeFactor);
            int num15 = (int)(24f * Container.SizeFactor);
            int num16 = (int)(35f * Container.SizeFactor);
            int num17 = (int)(38f * Container.SizeFactor);
            int num18 = (int)(40f * Container.SizeFactor);
            int num19 = (int)(42f * Container.SizeFactor);
            int num20 = (int)(55f * Container.SizeFactor);
            int num21 = (int)(90f * Container.SizeFactor);
            int num22 = (int)(111f * Container.SizeFactor);
            int num23 = (int)(165f * Container.SizeFactor);
            int num24 = (int)(176f * Container.SizeFactor);
            int width = (int)(185f * Container.SizeFactor);
            Color color = Color.FromArgb(80, 170, 170, 170);
            if (object_1 == object_0)
            {
                color = Color.FromArgb(144, 170, 170, 170);
            }
            using (SolidBrush brush = new SolidBrush(color))
            {
                graphics_0.FillRectangle(brush, new Rectangle(int_1, int_2, int_3, int_4));
            }
            if (object_1 == object_0)
            {
                using Pen pen = new Pen(Color.FromArgb(224, 170, 170, 170));
                graphics_0.DrawRectangle(pen, new Rectangle(int_1, int_2, int_3, int_4));
            }
            if (object_1 is GalaxyLocation)
            {
                GalaxyLocation galaxyLocation = (GalaxyLocation)object_1;
                Empire empire = null;
                Bitmap bitmap = null;
                string text = string.Empty;
                switch (galaxyLocation.Type)
                {
                    case GalaxyLocationType.RestrictedArea:
                        text = TextResolver.GetText("Restricted Area");
                        if (galaxyLocation.RelatedBuiltObject != null)
                        {
                            bitmap = Container.BuiltObjectImages[galaxyLocation.RelatedBuiltObject.PictureRef];
                            empire = galaxyLocation.RelatedBuiltObject.Empire;
                        }
                        break;
                    case GalaxyLocationType.PlanetDestroyer:
                        text = TextResolver.GetText("Planet Destroyer Project");
                        if (galaxyLocation.RelatedBuiltObject != null)
                        {
                            bitmap = Container.BuiltObjectImages[galaxyLocation.RelatedBuiltObject.PictureRef];
                            empire = galaxyLocation.RelatedBuiltObject.Empire;
                        }
                        break;
                    case GalaxyLocationType.DebrisField:
                        text = TextResolver.GetText("Debris Field");
                        if (galaxyLocation.RelatedBuiltObject != null)
                        {
                            bitmap = Container.BuiltObjectImages[galaxyLocation.RelatedBuiltObject.PictureRef];
                            empire = galaxyLocation.RelatedBuiltObject.Empire;
                        }
                        break;
                }
                int num25 = int_1 + 5;
                if (bitmap != null)
                {
                    graphics_0.DrawImage(bitmap, new Point(num25, int_2 + 5));
                    num25 += bitmap.Width + num3;
                }
                SizeF sizeF = graphics_0.MeasureString(galaxyLocation.Name, Container.BoldFont, int_3, StringFormat.GenericTypographic);
                if (empire != null && empire.PirateEmpireBaseHabitat == null && empire != Container.Parent._Game.Galaxy.IndependentEmpire)
                {
                    using SolidBrush brush_ = new SolidBrush(empire.MainColor);
                    method_13(graphics_0, galaxyLocation.Name, Container.BoldFont, new Point(num25, int_2 + num), brush_);
                    method_13(graphics_0, "(" + text + ")", Container.SmallFont, new Point(num25 + (int)sizeF.Width + num5, int_2 + num4), brush_);
                }
                else
                {
                    method_12(graphics_0, galaxyLocation.Name, Container.BoldFont, new Point(num25, int_2 + num));
                    method_12(graphics_0, "(" + text + ")", Container.SmallFont, new Point(num25 + (int)sizeF.Width + num5, int_2 + num4));
                }
                string text2 = string.Empty;
                Habitat habitat = Container.Parent._Game.Galaxy.FastFindNearestSystem(galaxyLocation.Xpos, galaxyLocation.Ypos);
                if (habitat != null)
                {
                    text2 = string.Format(TextResolver.GetText("Near LOCATION"), habitat.Name) + ", ";
                }
                string arg = Container.Parent._Game.Galaxy.ResolveSectorDescription(galaxyLocation.Xpos, galaxyLocation.Ypos);
                text2 += string.Format(TextResolver.GetText("Sector X"), arg);
                method_12(graphics_0, text2, Container.SmallFont, new Point(num25, int_2 + num15));
            }
            else if (object_1 is Character)
            {
                Character character = (Character)object_1;
                if (character == null || Container.Parent == null || Container.Parent.characterImageCache_0 == null)
                {
                    return;
                }
                Bitmap image = Container.Parent.characterImageCache_0.ObtainCharacterImageSmall(character);
                Rectangle rect = new Rectangle(int_1 + 5, int_2 + 5, imageSize, imageSize);
                graphics_0.DrawImage(image, rect);
                int num26 = int_1 + (5 + num17 + num3);
                SizeF sizeF2 = graphics_0.MeasureString(character.Name, Container.BoldFont, int_3, StringFormat.GenericTypographic);
                method_12(graphics_0, character.Name, Container.BoldFont, new Point(num26, int_2 + num));
                string string_ = "(" + Galaxy.ResolveDescription(character.Role) + ")";
                method_12(graphics_0, string_, Container.SmallFont, new Point(num26 + (int)sizeF2.Width + num5, int_2 + num4));
                string text3 = Galaxy.ResolveDescriptionCharacterTask(character, galaxy_0);
                if (string.IsNullOrEmpty(text3) && character.Location != null)
                {
                    text3 = character.Location.Name;
                    if (character.Location is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)character.Location;
                        if (builtObject.ShipGroup != null)
                        {
                            text3 = text3 + "  (" + builtObject.ShipGroup.Name + ")";
                        }
                    }
                }
                graphics_0.MeasureString(text3, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                method_12(graphics_0, text3, Container.SmallFont, new Point(num26, int_2 + num15));
            }
            else if (object_1 is Habitat)
            {
                Habitat habitat2 = (Habitat)object_1;
                if (galaxy_0.PlayerEmpire.PirateEmpireBaseHabitat != null && habitat2.Empire != galaxy_0.PlayerEmpire && habitat2.Population != null && habitat2.Population.Count > 0)
                {
                    if (Container.HabitatImages[habitat2.PictureRef] != null)
                    {
                        graphics_0.DrawImage(Container.HabitatImages[habitat2.PictureRef], new Point(int_1 + 5, int_2 + 5));
                    }
                    if (habitat2.ManufacturingQueue != null && habitat2.ManufacturingQueue.DeficientResources != null && habitat2.ManufacturingQueue.DeficientResources.Count > 0)
                    {
                        graphics_0.DrawImage(Container.ConstructionStalledImage, new Point(int_1 + 5, int_2 + num13));
                    }
                    if (habitat2.Population != null && habitat2.Population.DominantRace != null)
                    {
                        graphics_0.DrawImage(Container.RaceImageCache.GetRaceImageSize30(habitat2.Population.DominantRace.PictureRef, useTransparent: true), new Point(int_1 + 5 + imageSize + num3, int_2 + num3));
                    }
                    int num27 = int_1 + (5 + imageSize + num3 + imageSize + num3);
                    SizeF sizeF3 = graphics_0.MeasureString(habitat2.Name, Container.BoldFont, int_3, StringFormat.GenericTypographic);
                    Color color2 = Container.WhiteBrush.Color;
                    if (habitat2.Empire != null && habitat2.Empire != galaxy_0.IndependentEmpire)
                    {
                        color2 = habitat2.Empire.MainColor;
                    }
                    using (SolidBrush brush_2 = new SolidBrush(color2))
                    {
                        method_13(graphics_0, habitat2.Name, Container.BoldFont, new Point(num27, int_2 + num), brush_2);
                    }
                    string empty = string.Empty;
                    if (habitat2.Category != 0 && habitat2.Category != HabitatCategoryType.GasCloud)
                    {
                        empty = "(" + TextResolver.GetText("Quality") + ": " + habitat2.Quality.ToString("0%");
                        Habitat habitat3 = Galaxy.DetermineHabitatSystemStar(habitat2);
                        string text4 = empty;
                        empty = text4 + ", " + habitat3.Name + " " + TextResolver.GetText("system") + ")";
                    }
                    else
                    {
                        empty = "(" + TextResolver.GetText("Size") + ": " + ((double)habitat2.Diameter / 10.0).ToString("0.0") + "K)";
                    }
                    using (SolidBrush brush_3 = new SolidBrush(color2))
                    {
                        method_13(graphics_0, empty, Container.SmallFont, new Point(num27 + (int)sizeF3.Width + num5, int_2 + num4), brush_3);
                    }
                    if (habitat2.Population != null && habitat2.Population.DominantRace != null)
                    {
                        string text5 = habitat2.Population.TotalAmount.ToString("0,,M");
                        SizeF sizeF4 = graphics_0.MeasureString(text5, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text5, Container.SmallFont, new Point(num27, int_2 + num15));
                        num27 += (int)sizeF4.Width + num6;
                    }
                    if (Container.Parent._Game.Galaxy.PlayerEmpire.ResourceMap != null && Container.Parent._Game.Galaxy.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat2))
                    {
                        if (habitat2.Resources != null && habitat2.Resources.Count > 0)
                        {
                            HabitatResourceList habitatResourceList = habitat2.Resources.Clone();
                            for (int i = 0; i < habitatResourceList.Count; i++)
                            {
                                HabitatResource habitatResource = habitatResourceList[i];
                                if (habitatResource == null)
                                {
                                    continue;
                                }
                                Bitmap bitmap2 = Container.ResourceImages[habitatResource.PictureRef];
                                ResourceBonusList resourceBonusList = new ResourceBonusList();
                                if (galaxy_0.PlayerEmpire.DominantRace != null)
                                {
                                    resourceBonusList = galaxy_0.PlayerEmpire.DominantRace.CriticalResources;
                                }
                                if (resourceBonusList.GetBonusByResourceType(habitatResource.ResourceID) != null)
                                {
                                    using Pen pen2 = new Pen(Color.Yellow, 1f);
                                    pen2.DashStyle = DashStyle.Dot;
                                    Rectangle rect2 = new Rectangle(num27, int_2 + num14 + (num10 - bitmap2.Height) / 2, bitmap2.Width, bitmap2.Height);
                                    graphics_0.DrawRectangle(pen2, rect2);
                                }
                                Point point = new Point(num27, int_2 + num14 + (num10 - bitmap2.Height) / 2);
                                graphics_0.DrawImage(bitmap2, point);
                                num27 += bitmap2.Width + 1;
                            }
                            num27 += num4;
                        }
                    }
                    else
                    {
                        string text6 = "(" + TextResolver.GetText("Unknown resources") + ")";
                        SizeF sizeF5 = graphics_0.MeasureString(text6, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text6, Container.SmallFont, new Point(num27, int_2 + num15));
                        num27 += (int)sizeF5.Width + num6;
                    }
                    PirateColonyControl byFaction = habitat2.GetPirateControl().GetByFaction(galaxy_0.PlayerEmpire.EmpireId);
                    if (byFaction == null)
                    {
                        return;
                    }
                    string text7 = TextResolver.GetText("Control") + ": " + byFaction.ControlLevel.ToString("0%");
                    SizeF sizeF6 = graphics_0.MeasureString(text7, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                    method_12(graphics_0, text7, Container.SmallFont, new Point(num27, int_2 + num15));
                    num27 += (int)sizeF6.Width + num5;
                    if (!byFaction.HasFacilityControl || habitat2.Facilities == null)
                    {
                        return;
                    }
                    for (int j = 0; j < habitat2.Facilities.Count; j++)
                    {
                        PlanetaryFacility planetaryFacility = habitat2.Facilities[j];
                        if (planetaryFacility != null)
                        {
                            switch (planetaryFacility.Type)
                            {
                                case PlanetaryFacilityType.PirateBase:
                                case PlanetaryFacilityType.PirateFortress:
                                case PlanetaryFacilityType.PirateCriminalNetwork:
                                    {
                                        Bitmap bitmap3 = Container.FacilityImages[planetaryFacility.PictureRef];
                                        graphics_0.DrawImage(bitmap3, new Point(num27, int_2 + num14));
                                        num27 += bitmap3.Width + num3;
                                        break;
                                    }
                            }
                        }
                    }
                    return;
                }
                if (habitat2.Empire != null && habitat2.Empire != Container.Parent._Game.Galaxy.IndependentEmpire && habitat2.Population != null && habitat2.Population.Count > 0)
                {
                    if (Container.HabitatImages[habitat2.PictureRef] != null)
                    {
                        graphics_0.DrawImage(Container.HabitatImages[habitat2.PictureRef], new Point(int_1 + 5, int_2 + 5));
                    }
                    if (habitat2.Empire.Capital == habitat2)
                    {
                        if (Container.Parent != null && Container.Parent.bitmap_44 != null)
                        {
                            graphics_0.DrawImage(Container.Parent.bitmap_44, new Point(int_1 + 2, int_2 + 2));
                        }
                    }
                    else if (habitat2.Empire.Capitals.Contains(habitat2) && Container.Parent != null && Container.Parent.bitmap_43 != null)
                    {
                        graphics_0.DrawImage(Container.Parent.bitmap_43, new Point(int_1 + 2, int_2 + 2));
                    }
                    CharacterList characterList = new CharacterList();
                    if (habitat2.Empire != null && habitat2.Empire.Characters != null)
                    {
                        characterList = habitat2.Empire.Characters.FindCharactersAtLocation(habitat2);
                    }
                    if (characterList.Count > 0 && Container != null && Container.Parent != null && Container.Parent.characterImageCache_0 != null && Container.HabitatImages[habitat2.PictureRef] != null)
                    {
                        int num28 = int_1 + 5 + Container.HabitatImages[habitat2.PictureRef].Width - num7;
                        for (int k = 0; k < characterList.Count; k++)
                        {
                            if (characterList[k].Role == CharacterRole.ColonyGovernor)
                            {
                                Bitmap image2 = Container.Parent.characterImageCache_0.ObtainCharacterImageVerySmall(characterList[k]);
                                graphics_0.DrawImage(image2, new Point(num28, int_2 + num));
                                num28 -= num10;
                            }
                        }
                    }
                    if (habitat2.Population != null && habitat2.Population.DominantRace != null)
                    {
                        graphics_0.DrawImage(rect: new Rectangle(int_1 + 5 + imageSize + num3, int_2 + num3, imageSize, imageSize), image: Container.RaceImageCache.GetRaceImageSize30(habitat2.Population.DominantRace.PictureRef, useTransparent: true));
                    }
                    int num29 = int_1 + (5 + imageSize + num3 + imageSize + num3);
                    SizeF sizeF7 = graphics_0.MeasureString(habitat2.Name, Container.BoldFont, int_3, StringFormat.GenericTypographic);
                    method_12(graphics_0, habitat2.Name, Container.BoldFont, new Point(num29, int_2 + num));
                    string text8 = "(" + ((double)habitat2.Diameter / 10.0).ToString("0.0") + "K";
                    Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat2);
                    string text4 = text8;
                    text8 = text4 + ", " + habitat4.Name + " " + TextResolver.GetText("system") + ")";
                    method_12(graphics_0, text8, Container.SmallFont, new Point(num29 + (int)sizeF7.Width + num5, int_2 + num4));
                    string string_2 = habitat2.Population.TotalAmount.ToString("0,,M");
                    method_12(graphics_0, string_2, Container.SmallFont, new Point(num29, int_2 + num15));
                    graphics_0.DrawImage(Container.DevelopmentImage, new Point(num29 + num19, int_2 + num14));
                    string string_3 = habitat2.DevelopmentLevel.ToString("0") + "%";
                    method_12(graphics_0, string_3, Container.SmallFont, new Point(num29 + num20, int_2 + num15));
                    Bitmap bitmap4 = null;
                    bitmap4 = ((habitat2.EmpireApprovalRating > 15.0) ? Container.ApprovalSmileImage : ((habitat2.EmpireApprovalRating > 0.0) ? Container.ApprovalNeutralImage : ((!(habitat2.EmpireApprovalRating > -15.0)) ? Container.ApprovalAngryImage : Container.ApprovalSadImage)));
                    if (habitat2.Rebelling)
                    {
                        bitmap4 = Container.ApprovalAngryImage;
                    }
                    graphics_0.DrawImage(bitmap4, new Point(num29 + num21, int_2 + num14));
                    string string_4 = TextResolver.GetText("GDP") + ": " + habitat2.AnnualRevenue.ToString("0,K");
                    method_12(graphics_0, string_4, Container.SmallFont, new Point(num29 + num22, int_2 + num15));
                    if (habitat2.ConstructionQueue == null || habitat2.ConstructionQueue.ConstructionYards == null || habitat2.ConstructionQueue.ConstructionYards.Count <= 0)
                    {
                        return;
                    }
                    ConstructionYard constructionYard = habitat2.ConstructionQueue.ConstructionYards[0];
                    if (constructionYard == null)
                    {
                        return;
                    }
                    BuiltObject shipUnderConstruction = constructionYard.ShipUnderConstruction;
                    if (shipUnderConstruction != null)
                    {
                        Container.Parent.method_112(graphics_0, GraphicsQuality.High);
                        Bitmap bitmap5 = Container.BuiltObjectImages[shipUnderConstruction.PictureRef];
                        Rectangle srcRect;
                        Rectangle destRect;
                        if (bitmap5 != null)
                        {
                            srcRect = new Rectangle(0, 0, bitmap5.Width, bitmap5.Height);
                            destRect = new Rectangle(num29 + num24, int_2 + num14, num10, num10);
                            graphics_0.DrawImage(bitmap5, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                        Bitmap bitmap_ = Container.Parent.bitmap_72;
                        srcRect = new Rectangle(0, 0, bitmap_.Width, bitmap_.Height);
                        destRect = new Rectangle(num29 + num23, int_2 + num14, num10, num10);
                        graphics_0.DrawImage(bitmap_, destRect, srcRect, GraphicsUnit.Pixel);
                        if (habitat2.ConstructionQueue.ConstructionWaitQueue != null && habitat2.ConstructionQueue.ConstructionWaitQueue.Count > 0)
                        {
                            string string_5 = " (" + habitat2.ConstructionQueue.ConstructionWaitQueue.Count + " " + TextResolver.GetText("waiting") + ")";
                            method_12(graphics_0, string_5, Container.SmallFont, new Point(num29 + num23 + bitmap_.Width + 2, int_2 + num15));
                        }
                        Container.Parent.method_112(graphics_0, GraphicsQuality.Low);
                    }
                    return;
                }
                if (habitat2.Category == HabitatCategoryType.GasCloud)
                {
                    graphics_0.DrawImage(rect: new Rectangle(int_1 + 5, int_2 + 5, imageSize, imageSize), image: Container.GasCloudImage);
                }
                else if (habitat2.Category == HabitatCategoryType.Star)
                {
                    Bitmap bitmap6 = Container.Parent.bitmap_196[habitat2.MapPictureRef];
                    Rectangle srcRect2 = new Rectangle(0, 0, bitmap6.Width, bitmap6.Height);
                    Rectangle destRect2 = new Rectangle(int_1 + 5, int_2 + 5, Container.ImageSize, Container.ImageSize);
                    Container.Parent.method_112(graphics_0, GraphicsQuality.High);
                    graphics_0.DrawImage(bitmap6, destRect2, srcRect2, GraphicsUnit.Pixel);
                    Container.Parent.method_112(graphics_0, GraphicsQuality.Low);
                }
                else if (Container.HabitatImages[habitat2.PictureRef] != null)
                {
                    graphics_0.DrawImage(Container.HabitatImages[habitat2.PictureRef], new Point(int_1 + 5, int_2 + 5));
                }
                if (habitat2.Population != null && habitat2.Population.DominantRace != null)
                {
                    graphics_0.DrawImage(rect: new Rectangle(int_1 + 5 + imageSize + num3, int_2 + 5, imageSize, imageSize), image: Container.RaceImageCache.GetRaceImageSize30(habitat2.Population.DominantRace.PictureRef, useTransparent: true));
                }
                int num30 = int_1 + (5 + imageSize + num3);
                if (habitat2.Population != null && habitat2.Population.DominantRace != null)
                {
                    num30 += imageSize + num3;
                }
                SizeF sizeF8 = graphics_0.MeasureString(habitat2.Name, Container.BoldFont, int_3, StringFormat.GenericTypographic);
                string string_6 = string.Empty;
                Color color3 = method_1(habitat2, ColonizationFocus, MiningFocus, galaxy_0, out string_6);
                using (SolidBrush brush_4 = new SolidBrush(color3))
                {
                    method_13(graphics_0, habitat2.Name, Container.BoldFont, new Point(num30, int_2 + num), brush_4);
                }
                string empty2 = string.Empty;
                if (!string.IsNullOrEmpty(string_6))
                {
                    empty2 = "(" + string_6 + ")";
                }
                else if (habitat2.Category != 0 && habitat2.Category != HabitatCategoryType.GasCloud)
                {
                    empty2 = "(" + TextResolver.GetText("Quality") + ": " + habitat2.Quality.ToString("0%");
                    Habitat habitat5 = Galaxy.DetermineHabitatSystemStar(habitat2);
                    string text4 = empty2;
                    empty2 = text4 + ", " + habitat5.Name + " " + TextResolver.GetText("system") + ")";
                }
                else
                {
                    empty2 = "(" + TextResolver.GetText("Size") + ": " + ((double)habitat2.Diameter / 10.0).ToString("0.0") + "K)";
                }
                using (SolidBrush brush_5 = new SolidBrush(color3))
                {
                    method_13(graphics_0, empty2, Container.SmallFont, new Point(num30 + (int)sizeF8.Width + num5, int_2 + num4), brush_5);
                }
                if (habitat2.Population != null && habitat2.Population.DominantRace != null)
                {
                    string text9 = habitat2.Population.TotalAmount.ToString("0,,M");
                    SizeF sizeF9 = graphics_0.MeasureString(text9, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                    method_12(graphics_0, text9, Container.SmallFont, new Point(num30, int_2 + num15));
                    num30 += (int)sizeF9.Width + num10;
                }
                if (Container.Parent._Game.Galaxy.PlayerEmpire.ResourceMap != null && Container.Parent._Game.Galaxy.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat2))
                {
                    if (habitat2.Resources != null && habitat2.Resources.Count > 0)
                    {
                        HabitatResourceList habitatResourceList2 = habitat2.Resources.Clone();
                        for (int l = 0; l < habitatResourceList2.Count; l++)
                        {
                            HabitatResource habitatResource2 = habitatResourceList2[l];
                            if (habitatResource2 == null)
                            {
                                continue;
                            }
                            Bitmap bitmap7 = Container.ResourceImages[habitatResource2.PictureRef];
                            ResourceBonusList resourceBonusList2 = new ResourceBonusList();
                            if (galaxy_0.PlayerEmpire.DominantRace != null)
                            {
                                resourceBonusList2 = galaxy_0.PlayerEmpire.DominantRace.CriticalResources;
                            }
                            if (resourceBonusList2.GetBonusByResourceType(habitatResource2.ResourceID) != null)
                            {
                                using Pen pen3 = new Pen(Color.Yellow, 1f);
                                pen3.DashStyle = DashStyle.Dot;
                                Rectangle rect6 = new Rectangle(num30, int_2 + num14 + (num10 - bitmap7.Height) / 2, bitmap7.Width, bitmap7.Height);
                                graphics_0.DrawRectangle(pen3, rect6);
                            }
                            Point point2 = new Point(num30, int_2 + num14 + (num10 - bitmap7.Height) / 2);
                            graphics_0.DrawImage(bitmap7, point2);
                            num30 += bitmap7.Width + 2;
                        }
                        num30 += num6;
                    }
                }
                else
                {
                    string text10 = "(" + TextResolver.GetText("Unknown resources") + ")";
                    SizeF sizeF10 = graphics_0.MeasureString(text10, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                    method_12(graphics_0, text10, Container.SmallFont, new Point(num30, int_2 + num15));
                    num30 += (int)sizeF10.Width + num6;
                }
                string text11 = string.Empty;
                if (habitat2.ResearchBonus > 0 && habitat2.ResearchBonusIndustry != 0)
                {
                    if (!string.IsNullOrEmpty(text11))
                    {
                        text11 += ", ";
                    }
                    string text4 = text11;
                    text11 = text4 + Galaxy.ResolveDescription(habitat2.ResearchBonusIndustry) + " " + TextResolver.GetText("Research") + ": +" + habitat2.ResearchBonus.ToString("0") + "%";
                }
                if (habitat2.ScenicFactor > 0f)
                {
                    if (!string.IsNullOrEmpty(text11))
                    {
                        text11 += ", ";
                    }
                    text11 = (string.IsNullOrEmpty(habitat2.ScenicFeature) ? (text11 + TextResolver.GetText("Scenery Bonus") + ": +" + habitat2.ScenicFactor.ToString("0%")) : (text11 + TextResolver.GetText("Scenery Bonus") + ": +" + string.Format(TextResolver.GetText("BONUSAMOUNT from FEATURE"), habitat2.ScenicFactor.ToString("0%"), habitat2.ScenicFeature)));
                }
                if (!string.IsNullOrEmpty(text11))
                {
                    SizeF sizeF11 = graphics_0.MeasureString(text11, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                    method_12(graphics_0, text11, Container.SmallFont, new Point(num30, int_2 + num15));
                    num30 += (int)sizeF11.Width + num6;
                }
                BuiltObject builtObject2 = Container.Parent._Game.PlayerEmpire.CheckColonizingHabitat(habitat2);
                if (builtObject2 != null)
                {
                    Container.Parent.method_112(graphics_0, GraphicsQuality.High);
                    int num31 = int_1 + (int_3 - num16);
                    Bitmap bitmap8 = Container.BuiltObjectImages[builtObject2.PictureRef];
                    if (bitmap8 != null)
                    {
                        Rectangle srcRect3 = new Rectangle(0, 0, bitmap8.Width, bitmap8.Height);
                        Rectangle destRect3 = new Rectangle(num31 + num10, int_2 + num14, num10, num10);
                        graphics_0.DrawImage(bitmap8, destRect3, srcRect3, GraphicsUnit.Pixel);
                    }
                    Bitmap bitmap9 = Container.ScaleLimitImage(Container.Parent.bitmap_71, num10, num10, Container.AlphaTransparency);
                    graphics_0.DrawImage(bitmap9, new Point(num31, int_2 + num14 + (num10 - bitmap9.Height) / 2));
                    Container.Parent.method_112(graphics_0, GraphicsQuality.Low);
                    return;
                }
                DesignList designList = Container.Parent._Game.PlayerEmpire.CheckBasesToBeBuiltAtHabitat(habitat2);
                if (designList.Count <= 0)
                {
                    return;
                }
                Design design = designList[0];
                if (design != null)
                {
                    Container.Parent.method_112(graphics_0, GraphicsQuality.High);
                    int num32 = int_1 + (int_3 - num16);
                    Bitmap bitmap10 = Container.BuiltObjectImages[design.PictureRef];
                    Rectangle srcRect4;
                    Rectangle destRect4;
                    if (bitmap10 != null)
                    {
                        srcRect4 = new Rectangle(0, 0, bitmap10.Width, bitmap10.Height);
                        destRect4 = new Rectangle(num32 + num10, int_2 + num14, num10, num10);
                        graphics_0.DrawImage(bitmap10, destRect4, srcRect4, GraphicsUnit.Pixel);
                    }
                    Bitmap bitmap_2 = Container.Parent.bitmap_72;
                    srcRect4 = new Rectangle(0, 0, bitmap_2.Width, bitmap_2.Height);
                    destRect4 = new Rectangle(num32, int_2 + num14, num10, num10);
                    graphics_0.DrawImage(bitmap_2, destRect4, srcRect4, GraphicsUnit.Pixel);
                    Container.Parent.method_112(graphics_0, GraphicsQuality.Low);
                }
            }
            else if (object_1 is BuiltObject)
            {
                BuiltObject builtObject3 = (BuiltObject)object_1;
                if (Container.BuiltObjectImages[builtObject3.PictureRef] != null)
                {
                    graphics_0.DrawImage(Container.BuiltObjectImages[builtObject3.PictureRef], new Point(int_1 + 5, int_2 + 5));
                }
                if ((builtObject3.Role == BuiltObjectRole.Base || builtObject3.SubRole == BuiltObjectSubRole.ConstructionShip) && builtObject3.ManufacturingQueue != null && builtObject3.ManufacturingQueue.DeficientResources != null && builtObject3.ManufacturingQueue.DeficientResources.Count > 0)
                {
                    graphics_0.DrawImage(Container.ConstructionStalledImage, new Point(int_1 + 5, int_2 + num13));
                }
                int num33 = int_1 + (5 + imageSize + num3);
                SizeF sizeF12 = graphics_0.MeasureString(builtObject3.Name, Container.BoldFont, int_3, StringFormat.GenericTypographic);
                Color color4 = Color.FromArgb(170, 170, 170);
                if (builtObject3.DamagedComponentCount > 0)
                {
                    color4 = Color.Red;
                }
                else if (builtObject3.UnbuiltComponentCount > 0)
                {
                    color4 = Color.Orange;
                }
                using (SolidBrush brush_6 = new SolidBrush(color4))
                {
                    method_13(graphics_0, builtObject3.Name, Container.BoldFont, new Point(num33, int_2 + num), brush_6);
                }
                string text12 = "(" + Galaxy.ResolveDescription(builtObject3.SubRole) + ")";
                method_12(graphics_0, text12, Container.SmallFont, new Point(num33 + (int)sizeF12.Width + num5, int_2 + num4));
                if (builtObject3.Role != BuiltObjectRole.Base && builtObject3.IsAutoControlled)
                {
                    SizeF sizeF13 = graphics_0.MeasureString(text12, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                    graphics_0.DrawImage(Container.AutomateImage, new Point(num33 + (int)sizeF12.Width + num5 + (int)sizeF13.Width + num5, int_2 + num3));
                }
                if (builtObject3.ShipGroup != null)
                {
                    SizeF sizeF14 = graphics_0.MeasureString(text12, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                    int num34 = num33 + (int)sizeF12.Width + num5 + (int)sizeF14.Width + num5;
                    if (builtObject3.IsAutoControlled)
                    {
                        num34 += num13;
                    }
                    method_12(graphics_0, "(" + builtObject3.ShipGroup.Name + ")", Container.SmallFont, new Point(num34, int_2 + num4));
                }
                if (builtObject3.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject3.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject3.SubRole != BuiltObjectSubRole.LargeSpacePort)
                {
                    if (builtObject3.SubRole == BuiltObjectSubRole.ExplorationShip)
                    {
                        string text13 = Galaxy.ResolveDescription(builtObject3.Empire, builtObject3.Mission);
                        SizeF sizeF15 = graphics_0.MeasureString(text13, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text13, Container.SmallFont, new Point(num33, int_2 + num15));
                        num33 += (int)sizeF15.Width + num10;
                        graphics_0.DrawImage(Container.RefuelImage, new Point(num33, int_2 + num14));
                        string text14 = (builtObject3.CurrentFuel / Math.Max(1.0, builtObject3.FuelCapacity)).ToString("0%");
                        graphics_0.MeasureString(text14, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text14, Container.SmallFont, new Point(num33 + Container.RefuelImage.Width + 2, int_2 + num15));
                    }
                    else if (builtObject3.Role == BuiltObjectRole.Military)
                    {
                        string text15 = Galaxy.ResolveDescription(builtObject3.Empire, builtObject3.Mission);
                        SizeF sizeF16 = graphics_0.MeasureString(text15, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text15, Container.SmallFont, new Point(num33, int_2 + num15));
                        num33 += (int)sizeF16.Width + num6;
                        graphics_0.DrawImage(Container.FirepowerImage, new Point(num33, int_2 + num14));
                        num33 += Container.FirepowerImage.Width;
                        string text16 = builtObject3.FirepowerRaw.ToString();
                        SizeF sizeF17 = graphics_0.MeasureString(text16, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text16, Container.SmallFont, new Point(num33, int_2 + num15));
                        num33 += (int)sizeF17.Width + num6;
                        graphics_0.DrawImage(Container.RefuelImage, new Point(num33, int_2 + num14));
                        num33 += Container.RefuelImage.Width + 2;
                        string text17 = (builtObject3.CurrentFuel / Math.Max(1.0, builtObject3.FuelCapacity)).ToString("0%");
                        SizeF sizeF18 = graphics_0.MeasureString(text17, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text17, Container.SmallFont, new Point(num33, int_2 + num15));
                        num33 += (int)sizeF18.Width + num6;
                        if (builtObject3.Troops != null && builtObject3.Troops.Count > 0)
                        {
                            if (Container.Parent._Game != null && Container.Parent._Game.PlayerEmpire != null)
                            {
                                if (Container.TroopImage == null)
                                {
                                    Container.TroopImage = Container.ScaleLimitImage(Container.Parent.bitmap_23[Container.Parent._Game.PlayerEmpire.TroopPictureRef], num10, num10, Container.AlphaTransparency);
                                }
                                graphics_0.DrawImage(Container.TroopImage, new Point(num33, int_2 + num14 + (num10 - Container.TroopImage.Height) / 2));
                                num33 += Container.TroopImage.Width + 2;
                            }
                            string text18 = builtObject3.Troops.Count.ToString();
                            SizeF sizeF19 = graphics_0.MeasureString(text18, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                            method_12(graphics_0, text18, Container.SmallFont, new Point(num33, int_2 + num15));
                            num33 += (int)sizeF19.Width + num6;
                        }
                        if (builtObject3.Fighters == null || builtObject3.Fighters.Count <= 0)
                        {
                            return;
                        }
                        if (Container.Parent._Game != null && Container.Parent._Game.PlayerEmpire != null)
                        {
                            if (Container.FighterImage == null)
                            {
                                Bitmap bitmap11 = new Bitmap(Container.Parent.bitmap_6[ShipImageHelper.ResolveNewFighterImageIndex(Container.Parent._Game.PlayerEmpire.DominantRace, Container.Parent._Game.PlayerEmpire.PirateEmpireBaseHabitat != null)]);
                                if (bitmap11 != null)
                                {
                                    bitmap11.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    Container.FighterImage = Container.ScaleLimitImage(bitmap11, num10, num10, Container.AlphaTransparency);
                                }
                            }
                            graphics_0.DrawImage(Container.FighterImage, new Point(num33, int_2 + num14));
                            num33 += Container.FighterImage.Width + 2;
                        }
                        string text19 = builtObject3.Fighters.Count.ToString();
                        SizeF sizeF20 = graphics_0.MeasureString(text19, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text19, Container.SmallFont, new Point(num33, int_2 + num15));
                        num33 += (int)sizeF20.Width + num6;
                    }
                    else if (builtObject3.SubRole == BuiltObjectSubRole.ConstructionShip)
                    {
                        string text20 = Galaxy.ResolveDescription(builtObject3.Empire, builtObject3.Mission);
                        SizeF sizeF_ = graphics_0.MeasureString(text20, Container.SmallFont, width, StringFormat.GenericTypographic);
                        method_15(graphics_0, text20, Container.SmallFont, new Point(num33, int_2 + num15), Container.WhiteBrush, sizeF_);
                        num33 += (int)sizeF_.Width + num6;
                        graphics_0.DrawImage(Container.RefuelImage, new Point(num33, int_2 + num14));
                        num33 += Container.RefuelImage.Width + 2;
                        string text21 = (builtObject3.CurrentFuel / Math.Max(1.0, builtObject3.FuelCapacity)).ToString("0%");
                        SizeF sizeF21 = graphics_0.MeasureString(text21, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text21, Container.SmallFont, new Point(num33, int_2 + num15));
                        num33 += (int)sizeF21.Width + num10;
                        if (builtObject3.ConstructionQueue == null || builtObject3.ConstructionQueue.ConstructionYards == null || builtObject3.ConstructionQueue.ConstructionYards.Count <= 0)
                        {
                            return;
                        }
                        int countUnderConstruction = builtObject3.ConstructionQueue.ConstructionYards.CountUnderConstruction;
                        if (countUnderConstruction <= 0)
                        {
                            return;
                        }
                        Container.Parent.method_112(graphics_0, GraphicsQuality.High);
                        Bitmap bitmap_3 = Container.Parent.bitmap_72;
                        Rectangle srcRect5 = new Rectangle(0, 0, bitmap_3.Width, bitmap_3.Height);
                        Rectangle destRect5 = new Rectangle(num33, int_2 + num14, num10, num10);
                        graphics_0.DrawImage(bitmap_3, destRect5, srcRect5, GraphicsUnit.Pixel);
                        num33 += num11;
                        for (int m = 0; m < builtObject3.ConstructionQueue.ConstructionYards.Count; m++)
                        {
                            ConstructionYard constructionYard2 = builtObject3.ConstructionQueue.ConstructionYards[m];
                            if (constructionYard2 == null)
                            {
                                continue;
                            }
                            BuiltObject shipUnderConstruction2 = constructionYard2.ShipUnderConstruction;
                            if (shipUnderConstruction2 != null)
                            {
                                Bitmap bitmap12 = Container.BuiltObjectImages[shipUnderConstruction2.PictureRef];
                                if (bitmap12 != null)
                                {
                                    srcRect5 = new Rectangle(0, 0, bitmap12.Width, bitmap12.Height);
                                    destRect5 = new Rectangle(num33, int_2 + num14, num10, num10);
                                    graphics_0.DrawImage(bitmap12, destRect5, srcRect5, GraphicsUnit.Pixel);
                                }
                                num33 += num11;
                            }
                        }
                        Container.Parent.method_112(graphics_0, GraphicsQuality.Low);
                    }
                    else if (builtObject3.SubRole != BuiltObjectSubRole.MiningStation && builtObject3.SubRole != BuiltObjectSubRole.GasMiningStation)
                    {
                        if (builtObject3.Role == BuiltObjectRole.Base)
                        {
                            graphics_0.DrawImage(Container.FirepowerImage, new Point(num33, int_2 + num14));
                            num33 += Container.FirepowerImage.Width;
                            string text22 = builtObject3.FirepowerRaw.ToString();
                            SizeF sizeF22 = graphics_0.MeasureString(text22, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                            method_12(graphics_0, text22, Container.SmallFont, new Point(num33, int_2 + num15));
                            num33 += (int)sizeF22.Width + num10;
                        }
                        else
                        {
                            string text23 = Galaxy.ResolveDescription(builtObject3.Empire, builtObject3.Mission);
                            SizeF sizeF23 = graphics_0.MeasureString(text23, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                            method_12(graphics_0, text23, Container.SmallFont, new Point(num33, int_2 + num15));
                            num33 += (int)sizeF23.Width + num10;
                            graphics_0.DrawImage(Container.RefuelImage, new Point(num33, int_2 + num14));
                            num33 += Container.RefuelImage.Width + 2;
                            string text24 = (builtObject3.CurrentFuel / Math.Max(1.0, builtObject3.FuelCapacity)).ToString("0%");
                            SizeF sizeF24 = graphics_0.MeasureString(text24, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                            method_12(graphics_0, text24, Container.SmallFont, new Point(num33, int_2 + num15));
                            num33 += (int)sizeF24.Width + num10;
                        }
                    }
                    else if (builtObject3.ParentHabitat != null && builtObject3.ParentHabitat.Resources != null && builtObject3.ParentHabitat.Resources.Count > 0)
                    {
                        HabitatResource[] array = ListHelper.ToArrayThreadSafe(builtObject3.ParentHabitat.Resources);
                        string text25 = TextResolver.GetText("Mining") + ":";
                        SizeF sizeF25 = graphics_0.MeasureString(text25, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                        method_12(graphics_0, text25, Container.SmallFont, new Point(num33, int_2 + num15));
                        num33 += (int)sizeF25.Width + num3;
                        foreach (HabitatResource habitatResource3 in array)
                        {
                            if (habitatResource3 != null)
                            {
                                Bitmap bitmap13 = Container.ResourceImages[habitatResource3.ResourceID];
                                graphics_0.DrawImage(bitmap13, new Point(num33, int_2 + num14 + (num10 - bitmap13.Height) / 2));
                                num33 += bitmap13.Width + 2;
                            }
                        }
                    }
                    else
                    {
                        method_12(graphics_0, TextResolver.GetText("Mining") + ": (" + TextResolver.GetText("None") + ")", Container.SmallFont, new Point(num33, int_2 + num15));
                    }
                    return;
                }
                graphics_0.DrawImage(Container.FirepowerImage, new Point(num33, int_2 + num14));
                num33 += Container.FirepowerImage.Width;
                string text26 = builtObject3.FirepowerRaw.ToString();
                SizeF sizeF26 = graphics_0.MeasureString(text26, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                method_12(graphics_0, text26, Container.SmallFont, new Point(num33, int_2 + num15));
                num33 += (int)sizeF26.Width + num10;
                if (builtObject3.ConstructionQueue == null || builtObject3.ConstructionQueue.ConstructionYards == null || builtObject3.ConstructionQueue.ConstructionYards.Count <= 0)
                {
                    return;
                }
                int countUnderConstruction2 = builtObject3.ConstructionQueue.ConstructionYards.CountUnderConstruction;
                if (countUnderConstruction2 <= 0)
                {
                    return;
                }
                Container.Parent.method_112(graphics_0, GraphicsQuality.High);
                Bitmap bitmap_4 = Container.Parent.bitmap_72;
                Rectangle srcRect6 = new Rectangle(0, 0, bitmap_4.Width, bitmap_4.Height);
                Rectangle destRect6 = new Rectangle(num33, int_2 + num14, num10, num10);
                graphics_0.DrawImage(bitmap_4, destRect6, srcRect6, GraphicsUnit.Pixel);
                num33 += num11;
                for (int num35 = 0; num35 < builtObject3.ConstructionQueue.ConstructionYards.Count; num35++)
                {
                    ConstructionYard constructionYard3 = builtObject3.ConstructionQueue.ConstructionYards[num35];
                    if (constructionYard3 != null && constructionYard3.ShipUnderConstruction != null)
                    {
                        BuiltObject shipUnderConstruction3 = constructionYard3.ShipUnderConstruction;
                        Bitmap bitmap14 = Container.BuiltObjectImages[shipUnderConstruction3.PictureRef];
                        if (bitmap14 != null)
                        {
                            srcRect6 = new Rectangle(0, 0, bitmap14.Width, bitmap14.Height);
                            destRect6 = new Rectangle(num33, int_2 + num14, num10, num10);
                            graphics_0.DrawImage(bitmap14, destRect6, srcRect6, GraphicsUnit.Pixel);
                        }
                        num33 += num9;
                    }
                }
                if (builtObject3.ConstructionQueue.ConstructionWaitQueue != null && builtObject3.ConstructionQueue.ConstructionWaitQueue.Count > 0)
                {
                    string string_7 = " (" + builtObject3.ConstructionQueue.ConstructionWaitQueue.Count + " " + TextResolver.GetText("waiting") + ")";
                    method_12(graphics_0, string_7, Container.SmallFont, new Point(num33, int_2 + num15));
                }
                Container.Parent.method_112(graphics_0, GraphicsQuality.Low);
            }
            else if (object_1 is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)object_1;
                if (Container.BuiltObjectImages[shipGroup.LeadShip.PictureRef] != null)
                {
                    graphics_0.DrawImage(Container.BuiltObjectImages[shipGroup.LeadShip.PictureRef], new Point(int_1 + 5, int_2 + 5));
                }
                CharacterList characterList2 = new CharacterList();
                if (shipGroup.Empire != null && shipGroup.Empire.Characters != null)
                {
                    characterList2 = shipGroup.Empire.Characters.GetFleetAdmiralsAndGenerals(shipGroup);
                }
                if (characterList2.Count > 0 && Container != null && Container.Parent != null && Container.Parent.characterImageCache_0 != null)
                {
                    int num36 = int_2 + num;
                    for (int num37 = 0; num37 < characterList2.Count; num37++)
                    {
                        Bitmap image3 = Container.Parent.characterImageCache_0.ObtainCharacterImageVerySmall(characterList2[num37]);
                        if (Container.BuiltObjectImages[shipGroup.LeadShip.PictureRef] != null)
                        {
                            graphics_0.DrawImage(image3, new Point(int_1 + 5 + Container.BuiltObjectImages[shipGroup.LeadShip.PictureRef].Width - num7, num36));
                        }
                        num36 += num10;
                        if (num36 > int_2 + num12)
                        {
                            break;
                        }
                    }
                }
                int num38 = int_1 + (5 + imageSize + num3);
                SizeF sizeF27 = graphics_0.MeasureString(shipGroup.Name, Container.BoldFont, int_3, StringFormat.GenericTypographic);
                method_12(graphics_0, shipGroup.Name, Container.BoldFont, new Point(num38, int_2 + num));
                string text27 = "(" + shipGroup.Ships.Count + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture) + ")";
                method_12(graphics_0, text27, Container.SmallFont, new Point(num38 + (int)sizeF27.Width + num8, int_2 + num4));
                Bitmap bitmap15 = null;
                if (shipGroup.Posture == FleetPosture.Attack)
                {
                    bitmap15 = Container.FleetPostureAttackImage;
                }
                else if (shipGroup.Posture == FleetPosture.Defend)
                {
                    bitmap15 = Container.FleetPostureDefendImage;
                }
                if (bitmap15 != null)
                {
                    graphics_0.DrawImage(bitmap15, new Point(int_1 + int_3 - num18, int_2 + num2));
                }
                Bitmap bitmap16 = null;
                bitmap16 = ((shipGroup.PostureRangeSquared <= 2250000.0) ? Container.FleetRangeTargetImage : ((shipGroup.PostureRangeSquared <= 2304000000.0) ? Container.FleetRangeSystemImage : ((shipGroup.PostureRangeSquared <= 250000000000.0) ? Container.FleetRangeAreaImage : ((!(shipGroup.PostureRangeSquared <= 1000000000000.0)) ? Container.FleetRangeAnyImage : Container.FleetRangeSectorImage))));
                if (bitmap16 != null)
                {
                    graphics_0.DrawImage(bitmap16, new Point(int_1 + int_3 - num13, int_2 + num2));
                }
                if (shipGroup.LeadShip != null && shipGroup.LeadShip.IsAutoControlled)
                {
                    SizeF sizeF28 = graphics_0.MeasureString(text27, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                    graphics_0.DrawImage(Container.AutomateImage, new Point(num38 + (int)sizeF27.Width + num8 + (int)sizeF28.Width + num6, int_2 + num3));
                }
                string text28 = Galaxy.ResolveDescription(shipGroup.Empire, shipGroup.Mission);
                SizeF sizeF29 = graphics_0.MeasureString(text28, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                method_12(graphics_0, text28, Container.SmallFont, new Point(num38, int_2 + num15));
                num38 += (int)sizeF29.Width + num10;
                graphics_0.DrawImage(Container.FirepowerImage, new Point(num38, int_2 + num14));
                num38 += Container.FirepowerImage.Width;
                string text29 = shipGroup.TotalFirepower.ToString();
                SizeF sizeF30 = graphics_0.MeasureString(text29, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                method_12(graphics_0, text29, Container.SmallFont, new Point(num38, int_2 + num15));
                num38 += (int)sizeF30.Width + num6;
                int totalTroopCount = shipGroup.TotalTroopCount;
                if (totalTroopCount > 0)
                {
                    if (Container.Parent._Game != null && Container.Parent._Game.PlayerEmpire != null)
                    {
                        if (Container.TroopImage == null)
                        {
                            Container.TroopImage = Container.ScaleLimitImage(Container.Parent.bitmap_23[Container.Parent._Game.PlayerEmpire.TroopPictureRef], num10, num10, Container.AlphaTransparency);
                        }
                        graphics_0.DrawImage(Container.TroopImage, new Point(num38, int_2 + num14 + (num10 - Container.TroopImage.Height) / 2));
                        num38 += Container.TroopImage.Width + 2;
                    }
                    string text30 = totalTroopCount.ToString();
                    SizeF sizeF31 = graphics_0.MeasureString(text30, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                    method_12(graphics_0, text30, Container.SmallFont, new Point(num38, int_2 + num15));
                    num38 += (int)sizeF31.Width + num6;
                }
                int totalFighterCount = shipGroup.TotalFighterCount;
                if (totalFighterCount <= 0)
                {
                    return;
                }
                if (Container.Parent._Game != null && Container.Parent._Game.PlayerEmpire != null)
                {
                    if (Container.FighterImage == null)
                    {
                        Bitmap bitmap17 = new Bitmap(Container.Parent.bitmap_6[ShipImageHelper.ResolveNewFighterImageIndex(Container.Parent._Game.PlayerEmpire.DominantRace, Container.Parent._Game.PlayerEmpire.PirateEmpireBaseHabitat != null)]);
                        if (bitmap17 != null)
                        {
                            bitmap17.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            Container.FighterImage = Container.ScaleLimitImage(bitmap17, num10, num10, Container.AlphaTransparency);
                        }
                    }
                    graphics_0.DrawImage(Container.FighterImage, new Point(num38, int_2 + num14));
                    num38 += Container.FighterImage.Width + 2;
                }
                string text31 = totalFighterCount.ToString();
                SizeF sizeF32 = graphics_0.MeasureString(text31, Container.SmallFont, Container.Area.Width, StringFormat.GenericTypographic);
                method_12(graphics_0, text31, Container.SmallFont, new Point(num38, int_2 + num15));
                num38 += (int)sizeF32.Width + num6;
            }
            else
            {
                if (object_1 is HabitatPrioritization)
                {
                    return;
                }
                if (object_1 is PrioritizedTarget)
                {
                    PrioritizedTarget prioritizedTarget = (PrioritizedTarget)object_1;
                    if (prioritizedTarget == null)
                    {
                        return;
                    }
                    int missionQueueIndex = 0;
                    ShipGroup shipGroup2 = Container.ResolveAssignedFleet(prioritizedTarget, out missionQueueIndex);
                    int alpha = 255;
                    if (shipGroup2 != null)
                    {
                        alpha = 72;
                    }
                    Bitmap bitmap18 = null;
                    Color color5 = Color.White;
                    if (prioritizedTarget.Empire != null && galaxy_0 != null && prioritizedTarget.Empire != galaxy_0.IndependentEmpire)
                    {
                        if (prioritizedTarget.Empire != null && prioritizedTarget.Empire.PirateEmpireBaseHabitat == null)
                        {
                            if (prioritizedTarget.Empire.EmpireId >= 0 && prioritizedTarget.Empire.EmpireId < EmpireFlagImages.Length)
                            {
                                bitmap18 = EmpireFlagImages[prioritizedTarget.Empire.EmpireId];
                            }
                            color5 = Color.FromArgb(alpha, prioritizedTarget.Empire.MainColor.R, prioritizedTarget.Empire.MainColor.G, prioritizedTarget.Empire.MainColor.B);
                        }
                        else if (prioritizedTarget.Empire != null && prioritizedTarget.Empire.PirateEmpireBaseHabitat != null)
                        {
                            if (prioritizedTarget.Empire.EmpireId >= 0 && prioritizedTarget.Empire.EmpireId < EmpireFlagImages.Length)
                            {
                                bitmap18 = EmpireFlagImages[prioritizedTarget.Empire.EmpireId];
                            }
                            color5 = Color.FromArgb(alpha, 170, 170, 170);
                        }
                    }
                    if (bitmap18 != null)
                    {
                        int width2 = (int)(50f * Container.SizeFactor);
                        int height = (int)(30f * Container.SizeFactor);
                        Rectangle rect7 = new Rectangle(int_1 + num3, int_2 + num3, width2, height);
                        graphics_0.DrawImage(bitmap18, rect7);
                    }
                    Bitmap bitmap19 = method_9(prioritizedTarget);
                    int num39 = 0;
                    if (bitmap19 != null)
                    {
                        graphics_0.DrawImage(bitmap19, int_1 + 5 + num20, int_2 + 5);
                        num39 = bitmap19.Width + num;
                    }
                    Container.Parent.method_112(graphics_0, GraphicsQuality.Medium);
                    Point point_ = new Point(int_1 + 5 + num20 + num39, int_2 + num);
                    using (SolidBrush brush_7 = new SolidBrush(color5))
                    {
                        int num40 = (int)((float)Container.Area.Width * 0.67f);
                        SizeF sizeF_2 = new SizeF(num40, num13);
                        string name = prioritizedTarget.Name;
                        SizeF sizeF33 = graphics_0.MeasureString(name, Container.BoldFont, num40, StringFormat.GenericTypographic);
                        method_14(graphics_0, name, Container.BoldFont, point_, brush_7, sizeF_2, bool_9: false);
                        if (prioritizedTarget.Empire != null)
                        {
                            method_14(point_0: new Point(point_.X + (int)(sizeF33.Width + (float)num4), point_.Y + num2), graphics_0: graphics_0, string_0: "(" + prioritizedTarget.Empire.Name + ")", font_0: Container.SmallFont, brush_0: brush_7, sizeF_0: sizeF_2, bool_9: false);
                        }
                    }
                    Point point3 = new Point(int_1 + 5 + num20 + num39, int_2 + num15);
                    string s = method_8(prioritizedTarget);
                    Color color6 = Color.FromArgb(alpha, 170, 170, 170);
                    using (SolidBrush brush2 = new SolidBrush(color6))
                    {
                        graphics_0.DrawString(s, Container.SmallFont, brush2, point3);
                    }
                    graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
                    if (shipGroup2 != null)
                    {
                        string text32 = string.Format(TextResolver.GetText("FLEET attacking"), shipGroup2.Name);
                        SizeF sizeF34 = graphics_0.MeasureString(text32, Container.LargeBoldFont, int_3, StringFormat.GenericTypographic);
                        string text33 = "(" + TextResolver.GetText("Right-click to cancel").ToLower(CultureInfo.InvariantCulture) + ")";
                        SizeF sizeF35 = graphics_0.MeasureString(text33, Container.SmallFont, int_3, StringFormat.GenericTypographic);
                        sizeF35 = new SizeF(sizeF35.Width, sizeF35.Height - (float)num2);
                        int num41 = (int_3 - (int)sizeF34.Width) / 2;
                        int num42 = (int_4 - (int)(sizeF34.Height + sizeF35.Height)) / 2;
                        Point point_3 = new Point(int_1 + num41, int_2 + num42);
                        int num43 = (int_3 - (int)sizeF35.Width) / 2;
                        int num44 = num42 + (int)sizeF34.Height - num2;
                        Point point_4 = new Point(int_1 + num43, int_2 + num44);
                        using SolidBrush brush_8 = new SolidBrush(Color.FromArgb(255, 220, 220, 220));
                        method_13(graphics_0, text32, Container.LargeBoldFont, point_3, brush_8);
                        method_13(graphics_0, text33, Container.SmallFont, point_4, brush_8);
                    }
                }
                else if (!(object_1 is SystemInfo) && object_1 is EmpireActivity)
                {
                    EmpireActivity empireActivity_ = (EmpireActivity)object_1;
                    method_7(graphics_0, empireActivity_, int_1, int_2, int_3, int_4, galaxy_0);
                }
            }
        }

        private void method_7(Graphics graphics_0, EmpireActivity empireActivity_0, int int_1, int int_2, int int_3, int int_4, Galaxy galaxy_0)
        {
            if (empireActivity_0 == null || empireActivity_0.TargetEmpire == null || empireActivity_0.Target == null)
            {
                return;
            }
            int num = (int)(60f * Container.SizeFactor);
            int num2 = (int)(2f * Container.SizeFactor);
            int num3 = (int)(3f * Container.SizeFactor);
            int num4 = (int)(5f * Container.SizeFactor);
            int num5 = (int)(10f * Container.SizeFactor);
            int num6 = (int)(16f * Container.SizeFactor);
            int num7 = (int)(17f * Container.SizeFactor);
            int num8 = (int)(18f * Container.SizeFactor);
            int num9 = (int)(19f * Container.SizeFactor);
            int num10 = (int)(20f * Container.SizeFactor);
            int num11 = (int)(30f * Container.SizeFactor);
            int num12 = (int)(36f * Container.SizeFactor);
            int num13 = (int)(39f * Container.SizeFactor);
            int num14 = (int)(40f * Container.SizeFactor);
            string string_ = string.Empty;
            string text = string.Empty;
            string arg = Galaxy.ResolveStarDateDescription(empireActivity_0.ExpiryDate);
            string string_2 = "(" + string.Format(TextResolver.GetText("Pirate Mission Expires DATE"), arg) + ")";
            string text2 = string.Format(arg0: (empireActivity_0.Price * 0.9).ToString("0"), format: TextResolver.GetText("Bid PRICE credits"));
            if (empireActivity_0.AssignedEmpire == null)
            {
                text2 = string.Format(TextResolver.GetText("Bid PRICE credits"), empireActivity_0.Price.ToString("0"));
            }
            string string_3 = string.Empty;
            int num15 = 0;
            bool flag = false;
            if (galaxy_0.PlayerEmpire == empireActivity_0.RequestingEmpire)
            {
                switch (empireActivity_0.Type)
                {
                    case EmpireActivityType.Attack:
                    case EmpireActivityType.Defend:
                        if (galaxy_0.PirateMissions.ContainsEquivalent(empireActivity_0))
                        {
                            flag = true;
                        }
                        break;
                    case EmpireActivityType.Smuggle:
                        flag = true;
                        break;
                }
            }
            bool flag2 = false;
            bool flag3 = false;
            if (!flag)
            {
                flag2 = true;
                flag3 = true;
                if (galaxy_0.PlayerEmpire.PirateEmpireBaseHabitat == null)
                {
                    flag2 = false;
                }
                else if (galaxy_0.PlayerEmpire == empireActivity_0.RequestingEmpire)
                {
                    flag2 = false;
                }
                else if (empireActivity_0.AssignedEmpire == galaxy_0.PlayerEmpire)
                {
                    flag3 = false;
                }
            }
            Bitmap bitmap = null;
            if (empireActivity_0.Target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)empireActivity_0.Target;
                bitmap = Container.BuiltObjectImages[builtObject.PictureRef];
            }
            else if (empireActivity_0.Target is Habitat)
            {
                Habitat habitat = (Habitat)empireActivity_0.Target;
                bitmap = Container.HabitatImages[habitat.PictureRef];
            }
            Bitmap bitmap2 = null;
            switch (empireActivity_0.Type)
            {
                case EmpireActivityType.Attack:
                    string_ = string.Format(TextResolver.GetText("Attack requested by EMPIRE"), empireActivity_0.RequestingEmpire.Name);
                    text = ((empireActivity_0.AssignedEmpire == null) ? string.Format(TextResolver.GetText("Assigned to: EMPIRE"), "(" + TextResolver.GetText("None") + ")", empireActivity_0.Price.ToString("0")) : ((empireActivity_0.BidTimeRemaining > 0L) ? string.Format(TextResolver.GetText("Current bid: EMPIRE"), empireActivity_0.AssignedEmpire.Name, empireActivity_0.Price.ToString("0")) : string.Format(TextResolver.GetText("Assigned to: EMPIRE"), empireActivity_0.AssignedEmpire.Name, empireActivity_0.Price.ToString("0"))));
                    bitmap2 = Container.Parent.bitmap_100;
                    break;
                case EmpireActivityType.Defend:
                    string_ = string.Format(TextResolver.GetText("Defense requested by EMPIRE"), empireActivity_0.RequestingEmpire.Name);
                    text = ((empireActivity_0.AssignedEmpire == null) ? string.Format(TextResolver.GetText("Assigned to: EMPIRE"), "(" + TextResolver.GetText("None") + ")", empireActivity_0.Price.ToString("0")) : ((empireActivity_0.BidTimeRemaining > 0L) ? string.Format(TextResolver.GetText("Current bid: EMPIRE"), empireActivity_0.AssignedEmpire.Name, empireActivity_0.Price.ToString("0")) : string.Format(TextResolver.GetText("Assigned to: EMPIRE"), empireActivity_0.AssignedEmpire.Name, empireActivity_0.Price.ToString("0"))));
                    string_2 = "(" + string.Format(TextResolver.GetText("Pirate Mission Completes DATE"), arg) + ")";
                    bitmap2 = Container.Parent.bitmap_101;
                    break;
                case EmpireActivityType.Smuggle:
                    num15 = galaxy_0.CountPirateFactionsAcceptedSmugglingMission(empireActivity_0.Target);
                    string_ = ((empireActivity_0.RequestingEmpire == galaxy_0.IndependentEmpire) ? ((empireActivity_0.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Smuggling requested by Independent"), new Resource(empireActivity_0.ResourceId).Name, empireActivity_0.Target.Name) : string.Format(TextResolver.GetText("Smuggling requested by Independent All Resources"), empireActivity_0.Target.Name)) : ((empireActivity_0.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Smuggling requested by EMPIRE"), new Resource(empireActivity_0.ResourceId).Name, empireActivity_0.RequestingEmpire.Name) : string.Format(TextResolver.GetText("Smuggling requested by EMPIRE All Resources"), empireActivity_0.RequestingEmpire.Name)));
                    if (galaxy_0.PlayerEmpire.PirateEmpireBaseHabitat != null && galaxy_0.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity_0))
                    {
                        flag2 = false;
                        num15--;
                        string arg3 = num15.ToString("0");
                        if (num15 < 0)
                        {
                            arg3 = "0";
                        }
                        text = string.Format(TextResolver.GetText("You and X other pirate factions accepted"), arg3);
                    }
                    else
                    {
                        text = string.Format(TextResolver.GetText("X pirate factions accepted"), num15.ToString("0"));
                    }
                    text2 = TextResolver.GetText("Accept Smuggling Mission");
                    string_3 = "(" + string.Format(TextResolver.GetText("Smuggling bonus AMOUNT"), (empireActivity_0.Price * 100.0).ToString("0.0")) + ")";
                    string_2 = "(" + string.Format(TextResolver.GetText("Pirate Mission Completes DATE"), arg) + ")";
                    bitmap2 = Container.Parent.bitmap_102;
                    break;
            }
            graphics_0.DrawImage(empireActivity_0.TargetEmpire.SmallFlagPicture, new Point(int_1 + num4, int_2 + num2));
            if (bitmap != null)
            {
                graphics_0.DrawImage(bitmap, new Point(int_1 + num6, int_2 + num2));
            }
            if (bitmap2 != null && bitmap2.PixelFormat != 0)
            {
                int num16 = num10;
                int width = (int)((double)bitmap2.Width / ((double)bitmap2.Height / (double)num16));
                Rectangle rect = new Rectangle(int_1 + num4, int_2 + num5, width, num16);
                graphics_0.DrawImage(bitmap2, rect);
            }
            Color color = empireActivity_0.TargetEmpire.MainColor;
            if (empireActivity_0.TargetEmpire == galaxy_0.IndependentEmpire)
            {
                color = Color.FromArgb(128, 128, 128);
            }
            if (empireActivity_0.TargetEmpire.PirateEmpireBaseHabitat != null)
            {
                color = Color.FromArgb(128, 128, 128);
            }
            using (SolidBrush brush_ = new SolidBrush(color))
            {
                int num17 = 0;
                if (bitmap != null)
                {
                    num17 = bitmap.Width;
                }
                method_13(graphics_0, empireActivity_0.Target.Name, Container.BoldFont, new Point(int_1 + num6 + num17 + 1, int_2 + num2), brush_);
                method_13(graphics_0, "(" + empireActivity_0.TargetEmpire.Name + ")", Container.SmallFont, new Point(int_1 + num6 + num17 + 1, int_2 + num9), brush_);
            }
            int num18 = int_2 + num12;
            graphics_0.DrawImage(empireActivity_0.RequestingEmpire.SmallFlagPicture, new Point(int_1 + num4, int_2 + num13));
            using (SolidBrush brush_2 = new SolidBrush(empireActivity_0.RequestingEmpire.MainColor))
            {
                method_13(graphics_0, string_, Container.BoldFont, new Point(int_1 + num10, num18 - num3), brush_2);
            }
            num18 += num7;
            if (empireActivity_0.Type == EmpireActivityType.Smuggle)
            {
                int num19 = 0;
                if (empireActivity_0.ResourceId != byte.MaxValue)
                {
                    Resource resource = new Resource(empireActivity_0.ResourceId);
                    Bitmap bitmap3 = Container.Parent._uiResourcesBitmaps[resource.PictureRef];
                    float num20 = (float)bitmap3.Width / (float)bitmap3.Height;
                    int num21 = num7;
                    num19 = (int)((float)num21 * num20);
                    graphics_0.DrawImage(bitmap3, new Rectangle(int_1 + num11, num18, num19, num21));
                }
                method_12(graphics_0, string_3, Container.Font, new Point(int_1 + num11 + num19 + 2, num18));
                num18 += num7;
            }
            if (galaxy_0.PlayerEmpire.PirateEmpireBaseHabitat != null)
            {
                switch (empireActivity_0.Type)
                {
                    case EmpireActivityType.Attack:
                        {
                            if (empireActivity_0.AssignedEmpire == galaxy_0.PlayerEmpire && empireActivity_0.BidTimeRemaining <= 0L)
                            {
                                int firepowerAssigned3 = 0;
                                method_12(graphics_0, string.Format(arg0: galaxy_0.PlayerEmpire.CountShipsAssignedToMission(empireActivity_0, out firepowerAssigned3).ToString("0"), format: TextResolver.GetText("X ships are attacking this target"), arg1: firepowerAssigned3.ToString("0")), Container.Font, new Point(int_1 + num10, num18));
                                num18 += num8;
                                break;
                            }
                            int shipCount2 = 0;
                            string string_6 = string.Format(arg1: galaxy_0.PlayerEmpire.BuiltObjects.TotalMobileMilitaryFirepowerNotAttackingDefending(out shipCount2).ToString("0"), format: TextResolver.GetText("Mission Available Forces"), arg0: shipCount2.ToString("0"));
                            method_12(graphics_0, string_6, Container.Font, new Point(int_1 + num10, num18));
                            num18 += num6;
                            method_12(graphics_0, TextResolver.GetText("Target Firepower") + ": " + empireActivity_0.Target.FirepowerRaw.ToString("0"), Container.Font, new Point(int_1 + num10, num18));
                            num18 += num8;
                            break;
                        }
                    case EmpireActivityType.Defend:
                        if (empireActivity_0.AssignedEmpire == galaxy_0.PlayerEmpire && empireActivity_0.BidTimeRemaining <= 0L)
                        {
                            int firepowerAssigned2 = 0;
                            method_12(graphics_0, string.Format(arg0: galaxy_0.PlayerEmpire.CountShipsAssignedToMission(empireActivity_0, out firepowerAssigned2).ToString("0"), format: TextResolver.GetText("X ships are defending this target"), arg1: firepowerAssigned2.ToString("0")), Container.Font, new Point(int_1 + num10, num18));
                            num18 += num8;
                        }
                        else
                        {
                            int shipCount = 0;
                            string string_5 = string.Format(arg1: galaxy_0.PlayerEmpire.BuiltObjects.TotalMobileMilitaryFirepowerNotAttackingDefending(out shipCount).ToString("0"), format: TextResolver.GetText("Mission Available Forces"), arg0: shipCount.ToString("0"));
                            method_12(graphics_0, string_5, Container.Font, new Point(int_1 + num10, num18));
                            num18 += num8;
                        }
                        break;
                    case EmpireActivityType.Smuggle:
                        {
                            if (galaxy_0.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity_0))
                            {
                                int firepowerAssigned = 0;
                                method_12(graphics_0, string.Format(arg0: galaxy_0.PlayerEmpire.CountShipsAssignedToMission(empireActivity_0, out firepowerAssigned).ToString("0"), format: TextResolver.GetText("X smugglers are performing this mission")), Container.Font, new Point(int_1 + num10, num18));
                                num18 += num6;
                                string string_4 = string.Format(TextResolver.GetText("Smuggling Mission Delivery Report"), empireActivity_0.PlayerAmountDelivered.ToString("#,###,##0"), empireActivity_0.PlayerIncomeEarned.ToString("#,###,##0"));
                                method_12(graphics_0, string_4, Container.Font, new Point(int_1 + num10, num18));
                                num18 += num8;
                                break;
                            }
                            int num22 = galaxy_0.PlayerEmpire.CountIdleFreighters();
                            int num23 = 0;
                            if (empireActivity_0.ResourceId != byte.MaxValue)
                            {
                                num23 = galaxy_0.PlayerEmpire.CountResourceSupplyLocations(empireActivity_0.ResourceId, includeIndependentColonies: true);
                            }
                            method_12(graphics_0, string.Format(TextResolver.GetText("We have X smugglers available for this mission"), num22.ToString("0")), Container.Font, new Point(int_1 + num10, num18));
                            num18 += num6;
                            if (empireActivity_0.ResourceId != byte.MaxValue)
                            {
                                method_12(graphics_0, string.Format(TextResolver.GetText("Our empire has access to X sources of this resource"), num23.ToString("0")), Container.Font, new Point(int_1 + num10, num18));
                                num18 += num8;
                            }
                            break;
                        }
                }
            }
            else
            {
                switch (empireActivity_0.Type)
                {
                    case EmpireActivityType.Attack:
                        if (empireActivity_0.AssignedEmpire != null && empireActivity_0.BidTimeRemaining <= 0L)
                        {
                            int firepowerAssigned5 = 0;
                            method_12(graphics_0, string.Format(arg0: empireActivity_0.AssignedEmpire.CountShipsAssignedToMission(empireActivity_0, out firepowerAssigned5).ToString("0"), format: TextResolver.GetText("X ships are attacking this target"), arg1: firepowerAssigned5.ToString("0")), Container.Font, new Point(int_1 + num10, num18));
                            num18 += num8;
                        }
                        break;
                    case EmpireActivityType.Defend:
                        if (empireActivity_0.AssignedEmpire != null && empireActivity_0.BidTimeRemaining <= 0L)
                        {
                            int firepowerAssigned4 = 0;
                            method_12(graphics_0, string.Format(arg0: empireActivity_0.AssignedEmpire.CountShipsAssignedToMission(empireActivity_0, out firepowerAssigned4).ToString("0"), format: TextResolver.GetText("X ships are defending this target"), arg1: firepowerAssigned4.ToString("0")), Container.Font, new Point(int_1 + num10, num18));
                            num18 += num8;
                        }
                        break;
                    case EmpireActivityType.Smuggle:
                        {
                            string string_7 = string.Format(TextResolver.GetText("Smuggling Mission Delivery Report For Requester"), empireActivity_0.PlayerAmountDelivered.ToString("#,###,##0"), empireActivity_0.PlayerIncomeEarned.ToString("#,###,##0"));
                            method_12(graphics_0, string_7, Container.Font, new Point(int_1 + num10, num18));
                            num18 += num8;
                            break;
                        }
                }
            }
            if (empireActivity_0.AssignedEmpire != null && empireActivity_0.BidTimeRemaining <= 0L)
            {
                graphics_0.DrawImage(empireActivity_0.AssignedEmpire.SmallFlagPicture, new Point(int_1 + num4, num18 + num3));
                SizeF sizeF_ = graphics_0.MeasureString(text, Container.Font, int_3 - (num + num10));
                method_14(graphics_0, text, Container.Font, new Point(int_1 + num10, num18), Container.WhiteBrush, sizeF_, bool_9: false);
                num18 += (int)sizeF_.Height;
                if (empireActivity_0.ExpiryDate - galaxy_0.CurrentStarDate < (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * 0.5))
                {
                    using (SolidBrush brush_3 = new SolidBrush(Color.Red))
                    {
                        method_13(graphics_0, string_2, Container.SmallFont, new Point(int_1 + num14, num18), brush_3);
                        return;
                    }
                }
                method_12(graphics_0, string_2, Container.SmallFont, new Point(int_1 + num14, num18));
                return;
            }
            if (empireActivity_0.AssignedEmpire != null)
            {
                int width2 = (int)(10f * Container.SizeFactor);
                int height = (int)(10f * Container.SizeFactor);
                new Rectangle(int_1 + num4, num18 + num3, width2, height);
                graphics_0.DrawImage(empireActivity_0.AssignedEmpire.SmallFlagPicture, new Point(int_1 + num4, num18 + num3));
                SizeF sizeF_2 = graphics_0.MeasureString(text, Container.Font, int_3 - (num + num10));
                method_14(graphics_0, text, Container.Font, new Point(int_1 + num10, num18), Container.WhiteBrush, sizeF_2, bool_9: false);
                num18 += (int)sizeF_2.Height;
            }
            else
            {
                SizeF empty = SizeF.Empty;
                empty = ((empireActivity_0.Type != EmpireActivityType.Smuggle || galaxy_0.PlayerEmpire.PirateEmpireBaseHabitat == null || !galaxy_0.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity_0)) ? graphics_0.MeasureString(text, Container.Font, int_3 - (num + num10)) : graphics_0.MeasureString(text, Container.Font, int_3 - num10));
                method_14(graphics_0, text, Container.Font, new Point(int_1 + num10, num18), Container.WhiteBrush, empty, bool_9: false);
                num18 += (int)empty.Height;
            }
            if (empireActivity_0.DisplayExtraData < 0)
            {
                empireActivity_0.DisplayExtraData = galaxy_0.CountPirateEmpiresConsideringMission(empireActivity_0, galaxy_0.PlayerEmpire);
            }
            string empty2 = string.Empty;
            int num24 = empireActivity_0.DisplayExtraData;
            if (empireActivity_0.Type == EmpireActivityType.Smuggle)
            {
                num24 -= num15;
                num24 = Math.Max(0, num24);
            }
            empty2 = ((galaxy_0.PlayerEmpire.PirateEmpireBaseHabitat != null) ? string.Format(TextResolver.GetText("Other Empires Considering Pirate Mission Description"), num24.ToString("0")) : string.Format(TextResolver.GetText("Empires Considering Pirate Mission Description"), num24.ToString("0")));
            method_12(graphics_0, empty2, Container.SmallFont, new Point(int_1 + num14, num18));
            if (flag)
            {
                Rectangle rect2 = new Rectangle(int_1 + int_3 - num, int_2 + 1, num, int_4 - num2);
                Color color2 = Color.FromArgb(216, 96, 96, 96);
                if (bool_6 && object_0 == empireActivity_0)
                {
                    color2 = Color.FromArgb(255, 128, 128, 128);
                }
                using (SolidBrush brush = new SolidBrush(color2))
                {
                    graphics_0.FillRectangle(brush, rect2);
                }
                using (Pen pen = new Pen(Container.WhiteBrush, 2f))
                {
                    graphics_0.DrawRectangle(pen, rect2);
                }
                string text3 = TextResolver.GetText("Cancel");
                SizeF sizeF = graphics_0.MeasureString(text3, Container.BoldFont, num);
                int x = rect2.X + (rect2.Width - (int)sizeF.Width) / 2;
                int y = rect2.Y + (rect2.Height - (int)sizeF.Height) / 2;
                method_14(graphics_0, text3, Container.BoldFont, new Point(x, y), Container.WhiteBrush, new SizeF(sizeF.Width + 1f, sizeF.Height + 1f), bool_9: true);
            }
            else if (flag2)
            {
                if (!flag3)
                {
                    text2 = "(" + TextResolver.GetText("Already Bidded") + ")";
                }
                Rectangle rect3 = new Rectangle(int_1 + int_3 - num, int_2 + 1, num, int_4 - num2);
                Color color3 = Color.FromArgb(216, 96, 96, 96);
                if (bool_6 && object_0 == empireActivity_0)
                {
                    color3 = Color.FromArgb(255, 128, 128, 128);
                }
                using (SolidBrush brush2 = new SolidBrush(color3))
                {
                    graphics_0.FillRectangle(brush2, rect3);
                }
                using (Pen pen2 = new Pen(Container.WhiteBrush, 2f))
                {
                    graphics_0.DrawRectangle(pen2, rect3);
                }
                SizeF sizeF2 = graphics_0.MeasureString(text2, Container.BoldFont, num);
                int num25 = (int)(empireActivity_0.BidTimeRemaining / 1000L);
                string empty3 = string.Empty;
                empty3 = ((empireActivity_0.Type == EmpireActivityType.Smuggle) ? string.Empty : ((num25 > 0) ? ("(" + string.Format(TextResolver.GetText("X seconds"), num25.ToString("0")) + ")") : ("(" + TextResolver.GetText("No bids yet") + ")")));
                SizeF sizeF3 = graphics_0.MeasureString(empty3, Container.SmallFont, num);
                int x2 = rect3.X + (rect3.Width - (int)sizeF2.Width) / 2;
                int num26 = rect3.Y + (rect3.Height - ((int)sizeF2.Height + num4 + (int)sizeF3.Height)) / 2;
                method_14(graphics_0, text2, Container.BoldFont, new Point(x2, num26), Container.WhiteBrush, new SizeF(sizeF2.Width + 1f, sizeF2.Height + 1f), bool_9: true);
                int x3 = rect3.X + (rect3.Width - (int)sizeF3.Width) / 2;
                int y2 = num26 + (num4 + (int)sizeF2.Height);
                method_12(graphics_0, empty3, Container.SmallFont, new Point(x3, y2));
            }
        }

        private string method_8(PrioritizedTarget prioritizedTarget_0)
        {
            string text = string.Empty;
            if (prioritizedTarget_0 != null && prioritizedTarget_0.Target != null)
            {
                bool flag = false;
                if (prioritizedTarget_0.Target is Habitat)
                {
                    Habitat habitat = (Habitat)prioritizedTarget_0.Target;
                    if (!(flag = Container.Parent._Game.PlayerEmpire.IsObjectVisibleToThisEmpire(habitat)))
                    {
                        text = text + TextResolver.GetText("Estimated") + ": ";
                    }
                    int num = 0;
                    if (flag)
                    {
                        num += Container.Parent._Game.Galaxy.DetermineDefendingFirepower(habitat, habitat.Empire);
                    }
                    else if (habitat.BasesAtHabitat != null)
                    {
                        for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
                        {
                            num += habitat.BasesAtHabitat[i].FirepowerRaw;
                        }
                    }
                    text += num.ToString(TextResolver.GetText("firepower format"));
                    if (habitat.Troops != null)
                    {
                        if (flag)
                        {
                            string text2 = text;
                            text = text2 + ", " + habitat.Troops.Count + " " + TextResolver.GetText("troops");
                        }
                        else
                        {
                            text = text + ", ? " + TextResolver.GetText("troops");
                        }
                    }
                    if (habitat.DefensiveFortressBonus > 0)
                    {
                        text = text + ", " + TextResolver.GetText("Planetary Facility Fortified Bunker");
                    }
                }
                else if (prioritizedTarget_0.Target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)prioritizedTarget_0.Target;
                    flag = Container.Parent._Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject);
                    int num2 = 0;
                    if (flag && builtObject.NearestSystemStar != null)
                    {
                        num2 = Container.Parent._Game.Galaxy.DetermineDefendingStrength(builtObject, builtObject.Empire);
                    }
                    else
                    {
                        if (!flag)
                        {
                            text = text + TextResolver.GetText("Estimated") + ": ";
                        }
                        num2 = builtObject.FirepowerRaw;
                    }
                    text += num2.ToString(TextResolver.GetText("firepower format"));
                }
                else if (prioritizedTarget_0.Target is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)prioritizedTarget_0.Target;
                    if (shipGroup.LeadShip != null)
                    {
                        flag = Container.Parent._Game.PlayerEmpire.IsObjectVisibleToThisEmpire(shipGroup.LeadShip);
                    }
                    if (shipGroup.Ships != null)
                    {
                        string text3 = text;
                        text = text3 + shipGroup.Ships.Count + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture) + ", " + shipGroup.TotalFirepower.ToString(TextResolver.GetText("firepower format"));
                    }
                }
            }
            return text;
        }

        private Bitmap method_9(PrioritizedTarget prioritizedTarget_0)
        {
            Bitmap result = null;
            if (prioritizedTarget_0 != null && prioritizedTarget_0.Target != null)
            {
                if (prioritizedTarget_0.Target is Habitat)
                {
                    Habitat habitat = (Habitat)prioritizedTarget_0.Target;
                    result = Container.HabitatImages[habitat.PictureRef];
                }
                else if (prioritizedTarget_0.Target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)prioritizedTarget_0.Target;
                    result = Container.BuiltObjectImages[builtObject.PictureRef];
                }
                else if (prioritizedTarget_0.Target is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)prioritizedTarget_0.Target;
                    if (shipGroup.LeadShip != null)
                    {
                        result = Container.BuiltObjectImages[shipGroup.LeadShip.PictureRef];
                    }
                }
            }
            return result;
        }

        public bool CheckScrollWheel(Point point, int scrollAmount)
        {
            bool result = false;
            if (Container.ActivePanelArea.Contains(point) && Items != null && Items.Length > 0)
            {
                double num = (double)scrollAmount / 100.0;
                ScrollPosition += (int)((double)ScrollAmountPerClick * num * -1.0);
                int num2 = TitleBarHeight + 1 + ScrollUpHeight + 1 + ScrollDownHeight + 4;
                if (ToggleButtonEnabled)
                {
                    num2 += ToggleButtonHeight * list_1.Count + 1;
                }
                int val = Math.Max(0, Items.Length * (ItemHeight + ItemGap) - ItemGap - (Container.Area.Height - num2));
                ScrollPosition = Math.Min(val, Math.Max(0, ScrollPosition));
                object hoveredItem = null;
                DetectHoveredElement(point, out hoveredItem);
                result = true;
            }
            return result;
        }

        public bool CheckMouseDown(Point point)
        {
            bool result = false;
            object hoveredItem = null;
            if (DetectHoveredElement(point, out hoveredItem))
            {
                if (bool_4)
                {
                    if (Items != null && Items.Length > 0)
                    {
                        ScrollPosition -= ScrollAmountPerClick;
                        ScrollPosition = Math.Max(0, ScrollPosition);
                        DetectHoveredElement(point, out hoveredItem);
                        result = true;
                    }
                }
                else if (bool_5 && Items != null && Items.Length > 0)
                {
                    ScrollPosition += ScrollAmountPerClick;
                    int num = TitleBarHeight + 1 + ScrollUpHeight + 1 + ScrollDownHeight + 4;
                    if (ToggleButtonEnabled)
                    {
                        num += ToggleButtonHeight + 1;
                    }
                    int val = Math.Max(0, Items.Length * (ItemHeight + ItemGap) - ItemGap - (Container.Area.Height - num));
                    ScrollPosition = Math.Min(val, ScrollPosition);
                    DetectHoveredElement(point, out hoveredItem);
                    result = true;
                }
            }
            return result;
        }

        public bool CheckClick(Point point, MouseButtons buttonClicked, bool isDoubleClick)
        {
            bool result = false;
            object hoveredItem = null;
            if (DetectHoveredElement(point, out hoveredItem))
            {
                if (bool_0)
                {
                    if (bool_1)
                    {
                        Container.ActivePanel = null;
                    }
                    if (bool_2)
                    {
                        Container.CycleChangeSize();
                    }
                    result = true;
                }
                else if (bool_3 && int_0 >= 0)
                {
                    if (int_0 < list_1.Count)
                    {
                        int num = list_1[int_0].Length - 1;
                        list_0[int_0]++;
                        if (list_0[int_0] > num)
                        {
                            list_0[int_0] = 0;
                        }
                        Container.ClickToggleButton(this);
                        result = true;
                    }
                }
                else if (bool_4)
                {
                    if (Items != null && Items.Length > 0)
                    {
                        ScrollPosition -= ScrollAmountPerClick;
                        ScrollPosition = Math.Max(0, ScrollPosition);
                        DetectHoveredElement(point, out hoveredItem);
                    }
                    result = true;
                }
                else if (bool_5)
                {
                    if (Items != null && Items.Length > 0)
                    {
                        ScrollPosition += ScrollAmountPerClick;
                        int num2 = TitleBarHeight + 1 + ScrollUpHeight + 1 + ScrollDownHeight + 4;
                        if (ToggleButtonEnabled)
                        {
                            num2 += ToggleButtonHeight + 1;
                        }
                        int val = Math.Max(0, Items.Length * (ItemHeight + ItemGap) - ItemGap - (Container.Area.Height - num2));
                        ScrollPosition = Math.Min(val, ScrollPosition);
                        DetectHoveredElement(point, out hoveredItem);
                    }
                    result = true;
                }
                else if (hoveredItem != null)
                {
                    Container.ClickItem(hoveredItem, buttonClicked, isDoubleClick, bool_6);
                    result = true;
                }
            }
            return result;
        }

        public void ClearHoverState()
        {
            bool_0 = false;
            bool_1 = false;
            bool_2 = false;
            bool_3 = false;
            bool_6 = false;
            bool_4 = false;
            bool_5 = false;
        }

        public bool DetectHoveredElement(Point point, out object hoveredItem)
        {
            bool flag = false;
            hoveredItem = null;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            int num = -1;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            if (Container.ActivePanel == this && Container.ActivePanelArea.Contains(point))
            {
                int num2 = point.X - Container.ActivePanelArea.Left;
                int num3 = point.Y - Container.ActivePanelArea.Top;
                if (num2 >= 0 && num2 < Container.ActivePanelArea.Width)
                {
                    int num4 = 0;
                    int titleBarHeight = TitleBarHeight;
                    int num5 = titleBarHeight;
                    int num6 = titleBarHeight;
                    if (ToggleButtonEnabled)
                    {
                        num6 = num5 + (ToggleButtonHeight * list_1.Count + 1);
                    }
                    int num7 = num6;
                    int num8 = num7 + ScrollUpHeight;
                    int num9 = Container.ActivePanelArea.Bottom - Container.ActivePanelArea.Top - ScrollDownHeight;
                    int num10 = num9 + ScrollDownHeight;
                    if (num3 >= num4 && num3 < titleBarHeight)
                    {
                        flag2 = true;
                        flag = true;
                        if (point.X > Container.ActivePanelArea.Right - TitleBarHeight)
                        {
                            flag3 = true;
                        }
                        if (point.X > Container.ActivePanelArea.Right - TitleBarHeight * 2 && point.X < Container.ActivePanelArea.Right - TitleBarHeight)
                        {
                            flag4 = true;
                        }
                    }
                    else if (ToggleButtonEnabled && num3 >= num5 && num3 < num6)
                    {
                        flag5 = true;
                        num = Math.Max(0, Math.Min(list_1.Count - 1, (num3 - num5) / ToggleButtonHeight));
                        flag = true;
                    }
                    else if (num3 >= num7 && num3 < num8)
                    {
                        flag6 = true;
                        flag = true;
                    }
                    else if (num3 >= num9 && num3 < num10)
                    {
                        flag7 = true;
                        flag = true;
                    }
                    else if (Items != null && Items.Length > 0)
                    {
                        int int_ = ScrollPosition + (num3 - num8);
                        hoveredItem = method_11(int_);
                        if (hoveredItem != null)
                        {
                            flag = true;
                        }
                        if (ItemType == typeof(EmpireActivity) && hoveredItem != null && hoveredItem is EmpireActivity)
                        {
                            EmpireActivity empireActivity = (EmpireActivity)hoveredItem;
                            if ((empireActivity.AssignedEmpire == null || empireActivity.BidTimeRemaining > 0L) && num2 > Container.ActivePanelArea.Width - 60)
                            {
                                flag8 = true;
                                bool flag9 = false;
                                if (empireActivity.RequestingEmpire == Container.Parent._Game.PlayerEmpire)
                                {
                                    flag9 = true;
                                    switch (empireActivity.Type)
                                    {
                                        case EmpireActivityType.Attack:
                                        case EmpireActivityType.Defend:
                                            if (Container.Parent._Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity))
                                            {
                                                flag9 = true;
                                            }
                                            break;
                                        case EmpireActivityType.Smuggle:
                                            flag9 = true;
                                            break;
                                    }
                                }
                                if (!flag9)
                                {
                                    if (Container.Parent._Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                                    {
                                        flag8 = false;
                                    }
                                    else if (Container.Parent._Game.PlayerEmpire == empireActivity.RequestingEmpire)
                                    {
                                        flag8 = false;
                                    }
                                    else if (Container.Parent._Game.PlayerEmpire == empireActivity.AssignedEmpire)
                                    {
                                        flag8 = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (hoveredItem != object_0)
            {
                BuiltObjectList builtObjectList = null;
                if (object_0 != null)
                {
                    if (object_0 is ShipGroup)
                    {
                        ShipGroup shipGroup = (ShipGroup)object_0;
                        Container.Parent.method_245(shipGroup.LeadShip);
                    }
                    else if (object_0 is Character)
                    {
                        Character character = (Character)object_0;
                        Container.Parent.method_245(character.Location);
                    }
                    else if (object_0 is GalaxyLocation)
                    {
                        GalaxyLocation galaxyLocation = (GalaxyLocation)object_0;
                        Point point2 = new Point((int)galaxyLocation.Xpos, (int)galaxyLocation.Ypos);
                        Container.Parent.method_245(point2);
                    }
                    else if (object_0 is EmpireActivity)
                    {
                        EmpireActivity empireActivity2 = (EmpireActivity)object_0;
                        if (empireActivity2.Target != null)
                        {
                            Container.Parent.method_245(empireActivity2.Target);
                        }
                    }
                    else if (object_0 is PrioritizedTarget)
                    {
                        PrioritizedTarget prioritizedTarget = (PrioritizedTarget)object_0;
                        if (prioritizedTarget.Target is ShipGroup)
                        {
                            ShipGroup shipGroup2 = (ShipGroup)prioritizedTarget.Target;
                            Container.Parent.method_245(shipGroup2.LeadShip);
                        }
                        else
                        {
                            Container.Parent.method_245(prioritizedTarget.Target);
                        }
                    }
                    else
                    {
                        Container.Parent.method_245(object_0);
                    }
                }
                object_0 = hoveredItem;
                if (object_0 != null)
                {
                    if (object_0 is ShipGroup)
                    {
                        ShipGroup shipGroup3 = (ShipGroup)object_0;
                        Container.Parent.method_243(shipGroup3.LeadShip);
                    }
                    else if (object_0 is Character)
                    {
                        Character character2 = (Character)object_0;
                        Container.Parent.method_243(character2.Location);
                    }
                    else if (object_0 is GalaxyLocation)
                    {
                        GalaxyLocation galaxyLocation2 = (GalaxyLocation)object_0;
                        Point point3 = new Point((int)galaxyLocation2.Xpos, (int)galaxyLocation2.Ypos);
                        Container.Parent.method_243(point3);
                    }
                    else if (object_0 is EmpireActivity)
                    {
                        EmpireActivity empireActivity3 = (EmpireActivity)object_0;
                        if (empireActivity3.Target != null)
                        {
                            Container.Parent.method_243(empireActivity3.Target);
                        }
                        builtObjectList = Container.Parent._Game.PlayerEmpire.DetermineShipsAssignedToMission(empireActivity3);
                    }
                    else if (object_0 is PrioritizedTarget)
                    {
                        PrioritizedTarget prioritizedTarget2 = (PrioritizedTarget)object_0;
                        if (prioritizedTarget2.Target is ShipGroup)
                        {
                            ShipGroup shipGroup4 = (ShipGroup)prioritizedTarget2.Target;
                            Container.Parent.method_243(shipGroup4.LeadShip);
                        }
                        else
                        {
                            Container.Parent.method_243(prioritizedTarget2.Target);
                        }
                    }
                    else
                    {
                        Container.Parent.method_243(object_0);
                    }
                }
                if ((builtObjectList == null || builtObjectList.Count == 0) && Container.Parent._Game.SelectedObject != null && Container.Parent._Game.SelectedObject is StellarObject)
                {
                    builtObjectList = Container.Parent._Game.PlayerEmpire.DetermineShipsMovingToDestination((StellarObject)Container.Parent._Game.SelectedObject);
                }
                Container.Parent.method_246(builtObjectList);
            }
            if (flag)
            {
                if (ItemType == typeof(PrioritizedTarget))
                {
                    Container.Parent.string_17 = TitleText + ": " + string.Format(TextResolver.GetText("cycle fleets (X key) and click to assign attack"), "F");
                }
                else
                {
                    Container.Parent.string_17 = TitleText + ": " + TextResolver.GetText("click to select item, double-click to move view");
                    if (ItemType == typeof(BuiltObject) && object_0 != null && object_0 is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)object_0;
                        if (builtObject != null && builtObject.Role != BuiltObjectRole.Base)
                        {
                            Main parent = Container.Parent;
                            parent.string_17 = parent.string_17 + ", " + TextResolver.GetText("Shift-click to multi-select items");
                        }
                    }
                }
            }
            bool_0 = flag2;
            bool_1 = flag3;
            bool_2 = flag4;
            bool_3 = flag5;
            int_0 = num;
            bool_4 = flag6;
            bool_5 = flag7;
            bool_6 = flag8;
            object_0 = hoveredItem;
            return flag;
        }

        private int method_10(int int_1)
        {
            return int_1 / (ItemHeight + ItemGap);
        }

        private object method_11(int int_1)
        {
            int num = method_10(int_1);
            int num2 = num * (ItemHeight + ItemGap) + ItemHeight;
            if (int_1 > num2)
            {
                return null;
            }
            if (Items != null && Items.Length > num && num >= 0)
            {
                return Items[num];
            }
            return null;
        }

        private void method_12(Graphics graphics_0, string string_0, Font font_0, Point point_0)
        {
            method_13(graphics_0, string_0, font_0, point_0, Container.WhiteBrush);
        }

        private void method_13(Graphics graphics_0, string string_0, Font font_0, Point point_0, Brush brush_0)
        {
            method_14(graphics_0, string_0, font_0, point_0, brush_0, SizeF.Empty, bool_9: false);
        }

        private void method_14(Graphics graphics_0, string string_0, Font font_0, Point point_0, Brush brush_0, SizeF sizeF_0, bool bool_9)
        {
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                RectangleF layoutRectangle = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                StringFormat stringFormat = StringFormat.GenericTypographic;
                if (bool_9)
                {
                    stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                }
                graphics_0.DrawString(string_0, font_0, Container.BlackBrush, layoutRectangle, stringFormat);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                layoutRectangle = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_0, brush_0, layoutRectangle, stringFormat);
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_0, Container.BlackBrush, point_0);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_0, brush_0, point_0);
            }
        }

        private void method_15(Graphics graphics_0, string string_0, Font font_0, Point point_0, Brush brush_0, SizeF sizeF_0)
        {
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                RectangleF layoutRectangle = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
                stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                graphics_0.DrawString(string_0, font_0, Container.BlackBrush, layoutRectangle, stringFormat);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                layoutRectangle = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_0, brush_0, layoutRectangle, stringFormat);
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_0, Container.BlackBrush, point_0);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_0, brush_0, point_0);
            }
        }
    }
}
