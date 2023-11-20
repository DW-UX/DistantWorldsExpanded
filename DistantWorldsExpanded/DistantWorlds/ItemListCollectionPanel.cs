// Decompiled with JetBrains decompiler
// Type: DistantWorlds.ItemListCollectionPanel
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
using System.Threading;
using System.Windows.Forms;

namespace DistantWorlds
{
    [Serializable]
    public class ItemListCollectionPanel
    {
        public class ItemClickedEventArgs : EventArgs
        {
            public object Item;

            public MouseButtons ButtonClicked;

            public bool IsDoubleClick;

            public bool BidButtonClicked;

            public ItemClickedEventArgs(object item, MouseButtons buttonClicked, bool isDoubleClick, bool bidButtonClicked) : base()
            {
                
                Item = item;
                ButtonClicked = buttonClicked;
                IsDoubleClick = isDoubleClick;
                BidButtonClicked = bidButtonClicked;
            }
        }

        public class BindItemPanelEventArgs : EventArgs
        {
            public ItemListPanel Panel;

            public BindItemPanelEventArgs(ItemListPanel panel):base()
            {
                
                Panel = panel;
            }
        }

        private EventHandler<ItemClickedEventArgs> eventHandler_0;

        private EventHandler<BindItemPanelEventArgs> eventHandler_1;

        private EventHandler<BindItemPanelEventArgs> eventHandler_2;

        private Main main_0;

        public bool Visible;

        public ItemListPanelList Panels;

        public ItemListPanel ActivePanel;

        public Rectangle Area;

        public Rectangle AreaMaximum;

        public Rectangle ActivePanelArea;

        private float float_0;

        public int SelectionButtonHeight;

        public int SelectionButtonWidth;

        public ItemListPanel HoveredPanelButton;

        public Font Font;

        public Font BoldFont;

        public Font SmallFont;

        public Font TinyFont;

        public Font LargeBoldFont;

        public SolidBrush WhiteBrush;

        public SolidBrush BlackBrush;

        public int ImageSize;

        public float AlphaTransparency;

        public bool NeedsRefresh;

        public DateTime LastRefresh;

        public Bitmap[] BuiltObjectImages;

        public Bitmap[] HabitatImages;

        public RaceImageCache RaceImageCache;

        public Bitmap[] ResourceImages;

        public Bitmap[] FacilityImages;

        public Bitmap ScrollUpImage;

        public Bitmap ScrollDownImage;

        public Bitmap GasCloudImage;

        public Bitmap FirepowerImage;

        public Bitmap RefuelImage;

        public Bitmap TroopImage;

        public Bitmap FighterImage;

        public Bitmap AutomateImage;

        public Bitmap FleetRangeTargetImage;

        public Bitmap FleetRangeSystemImage;

        public Bitmap FleetRangeAreaImage;

        public Bitmap FleetRangeSectorImage;

        public Bitmap FleetRangeAnyImage;

        public Bitmap FleetPostureAttackImage;

        public Bitmap FleetPostureDefendImage;

        public Bitmap ConstructionStalledImage;

        public Bitmap DevelopmentImage;

        public Bitmap ApprovalSmileImage;

        public Bitmap ApprovalNeutralImage;

        public Bitmap ApprovalSadImage;

        public Bitmap ApprovalAngryImage;

        protected IFontCache _FontCache;

        //private float float_1;

        //private bool bool_0;

        public float SizeFactor => float_0;

        public Main Parent => main_0;

        public event EventHandler<ItemClickedEventArgs> ItemClicked
        {
            add
            {
                EventHandler<ItemClickedEventArgs> eventHandler = eventHandler_0;
                EventHandler<ItemClickedEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<ItemClickedEventArgs> value2 = (EventHandler<ItemClickedEventArgs>)Delegate.Combine(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_0, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
            remove
            {
                EventHandler<ItemClickedEventArgs> eventHandler = eventHandler_0;
                EventHandler<ItemClickedEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<ItemClickedEventArgs> value2 = (EventHandler<ItemClickedEventArgs>)Delegate.Remove(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_0, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
        }

        public event EventHandler<BindItemPanelEventArgs> BindItemPanel
        {
            add
            {
                EventHandler<BindItemPanelEventArgs> eventHandler = eventHandler_1;
                EventHandler<BindItemPanelEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<BindItemPanelEventArgs> value2 = (EventHandler<BindItemPanelEventArgs>)Delegate.Combine(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_1, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
            remove
            {
                EventHandler<BindItemPanelEventArgs> eventHandler = eventHandler_1;
                EventHandler<BindItemPanelEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<BindItemPanelEventArgs> value2 = (EventHandler<BindItemPanelEventArgs>)Delegate.Remove(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_1, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
        }

        public event EventHandler<BindItemPanelEventArgs> ToggleButtonClicked
        {
            add
            {
                EventHandler<BindItemPanelEventArgs> eventHandler = eventHandler_2;
                EventHandler<BindItemPanelEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<BindItemPanelEventArgs> value2 = (EventHandler<BindItemPanelEventArgs>)Delegate.Combine(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_2, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
            remove
            {
                EventHandler<BindItemPanelEventArgs> eventHandler = eventHandler_2;
                EventHandler<BindItemPanelEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<BindItemPanelEventArgs> value2 = (EventHandler<BindItemPanelEventArgs>)Delegate.Remove(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_2, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
        }

        public void InitializeImages(Bitmap[] builtObjectImages, Bitmap[] habitatImages, RaceImageCache raceImageCache, Bitmap[] resourceImages, Bitmap[] facilityImages, Bitmap scrollUpImage, Bitmap scrollDownImage, Bitmap fleetRangeTargetImage, Bitmap fleetRangeSystemImage, Bitmap fleetRangeAreaImage, Bitmap fleetRangeSectorImage, Bitmap fleetRangeAnyImage, Bitmap fleetPostureAttackImage, Bitmap fleetPostureDefendImage, Bitmap constructionStalledImage)
        {
            double num = 0.0;
            int num2 = 0;
            method_1();
            ScrollUpImage = new Bitmap(scrollUpImage);
            ScrollDownImage = new Bitmap(scrollDownImage);
            BuiltObjectImages = new Bitmap[builtObjectImages.Length];
            for (int i = 0; i < builtObjectImages.Length; i++)
            {
                Bitmap bitmap = builtObjectImages[i];
                if (bitmap != null && bitmap.PixelFormat != 0)
                {
                    num = (double)bitmap.Width / (double)bitmap.Height;
                    num2 = (int)((double)ImageSize * num);
                    BuiltObjectImages[i] = method_3(bitmap, num2, ImageSize, AlphaTransparency);
                    BuiltObjectImages[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
            }
            HabitatImages = new Bitmap[habitatImages.Length];
            for (int j = 0; j < habitatImages.Length; j++)
            {
                Bitmap bitmap2 = habitatImages[j];
                if (bitmap2 != null && bitmap2.PixelFormat != 0)
                {
                    num = (double)bitmap2.Width / (double)bitmap2.Height;
                    num2 = (int)((double)ImageSize * num);
                    HabitatImages[j] = method_3(bitmap2, num2, ImageSize, AlphaTransparency);
                }
            }
            int num3 = ImageSize / 2;
            GasCloudImage = ScaleLimitImage(Parent.bitmap_4, ImageSize, ImageSize, AlphaTransparency);
            FirepowerImage = ScaleLimitImage(Parent.bitmap_38, num3, num3, AlphaTransparency);
            RefuelImage = ScaleLimitImage(Parent.bitmap_55, num3, num3, AlphaTransparency);
            AutomateImage = ScaleLimitImage(Parent.bitmap_53, num3, num3, AlphaTransparency);
            DevelopmentImage = ScaleLimitImage(Parent.bitmap_32, num3, num3, AlphaTransparency);
            ApprovalSmileImage = ScaleLimitImage(Parent.bitmap_33, num3, num3, AlphaTransparency);
            ApprovalNeutralImage = ScaleLimitImage(Parent.bitmap_34, num3, num3, AlphaTransparency);
            ApprovalSadImage = ScaleLimitImage(Parent.bitmap_35, num3, num3, AlphaTransparency);
            ApprovalAngryImage = ScaleLimitImage(Parent.bitmap_36, num3, num3, AlphaTransparency);
            FleetRangeTargetImage = ScaleLimitImage(fleetRangeTargetImage, num3, num3, AlphaTransparency);
            FleetRangeSystemImage = ScaleLimitImage(fleetRangeSystemImage, num3, num3, AlphaTransparency);
            FleetRangeAreaImage = ScaleLimitImage(fleetRangeAreaImage, num3, num3, AlphaTransparency);
            FleetRangeSectorImage = ScaleLimitImage(fleetRangeSectorImage, num3, num3, AlphaTransparency);
            FleetRangeAnyImage = ScaleLimitImage(fleetRangeAnyImage, num3, num3, AlphaTransparency);
            FleetPostureAttackImage = ScaleLimitImage(fleetPostureAttackImage, num3, num3, AlphaTransparency);
            FleetPostureDefendImage = ScaleLimitImage(fleetPostureDefendImage, num3, num3, AlphaTransparency);
            ConstructionStalledImage = ScaleLimitImage(constructionStalledImage, num3, num3, AlphaTransparency);
            RaceImageCache = raceImageCache;
            ResourceImages = new Bitmap[resourceImages.Length];
            for (int k = 0; k < resourceImages.Length; k++)
            {
                ResourceImages[k] = ScaleLimitImage(resourceImages[k], num3, num3, AlphaTransparency);
            }
            FacilityImages = new Bitmap[facilityImages.Length];
            for (int l = 0; l < facilityImages.Length; l++)
            {
                FacilityImages[l] = ScaleLimitImage(facilityImages[l], num3, num3, AlphaTransparency);
            }
        }

        public virtual void SetFontCache(IFontCache fontCache)
        {
            _FontCache = fontCache;
        }

        private void method_0()
        {
            if (_FontCache != null)
            {
                TinyFont = _FontCache.GenerateFont(FontSize.Tiny * float_0, isBold: false);
                SmallFont = _FontCache.GenerateFont(FontSize.Small * float_0, isBold: false);
                Font = _FontCache.GenerateFont(FontSize.Large * float_0, isBold: false);
                BoldFont = _FontCache.GenerateFont(FontSize.Small * float_0, isBold: true);
                LargeBoldFont = _FontCache.GenerateFont(FontSize.Title * float_0, isBold: true);
            }
        }

        public void Reset()
        {
            ActivePanel = null;
            HoveredPanelButton = null;
            if (Panels != null)
            {
                for (int i = 0; i < Panels.Count; i++)
                {
                    Panels[i].Items = null;
                    Panels[i].ClearHoverState();
                    Panels[i].Container = null;
                }
            }
            Panels.Clear();
        }

        private void method_1()
        {
            method_2(HabitatImages);
            method_2(BuiltObjectImages);
            RaceImageCache = null;
            method_2(ResourceImages);
            method_2(FacilityImages);
            if (ScrollUpImage != null)
            {
                ScrollUpImage.Dispose();
                ScrollUpImage = null;
            }
            if (ScrollDownImage != null)
            {
                ScrollDownImage.Dispose();
                ScrollDownImage = null;
            }
            if (GasCloudImage != null)
            {
                GasCloudImage.Dispose();
                GasCloudImage = null;
            }
            if (FirepowerImage != null)
            {
                FirepowerImage.Dispose();
                FirepowerImage = null;
            }
            if (RefuelImage != null)
            {
                RefuelImage.Dispose();
                RefuelImage = null;
            }
            if (AutomateImage != null)
            {
                AutomateImage.Dispose();
                AutomateImage = null;
            }
            if (FleetRangeTargetImage != null)
            {
                FleetRangeTargetImage.Dispose();
                FleetRangeTargetImage = null;
            }
            if (FleetRangeSystemImage != null)
            {
                FleetRangeSystemImage.Dispose();
                FleetRangeSystemImage = null;
            }
            if (FleetRangeAreaImage != null)
            {
                FleetRangeAreaImage.Dispose();
                FleetRangeAreaImage = null;
            }
            if (FleetRangeSectorImage != null)
            {
                FleetRangeSectorImage.Dispose();
                FleetRangeSectorImage = null;
            }
            if (FleetRangeAnyImage != null)
            {
                FleetRangeAnyImage.Dispose();
                FleetRangeAnyImage = null;
            }
            if (FleetPostureAttackImage != null)
            {
                FleetPostureAttackImage.Dispose();
                FleetPostureAttackImage = null;
            }
            if (FleetPostureDefendImage != null)
            {
                FleetPostureDefendImage.Dispose();
                FleetPostureDefendImage = null;
            }
            if (DevelopmentImage != null)
            {
                DevelopmentImage.Dispose();
                DevelopmentImage = null;
            }
            if (ApprovalSmileImage != null)
            {
                ApprovalSmileImage.Dispose();
                ApprovalSmileImage = null;
            }
            if (ApprovalNeutralImage != null)
            {
                ApprovalNeutralImage.Dispose();
                ApprovalNeutralImage = null;
            }
            if (ApprovalSadImage != null)
            {
                ApprovalSadImage.Dispose();
                ApprovalSadImage = null;
            }
            if (ApprovalAngryImage != null)
            {
                ApprovalAngryImage.Dispose();
                ApprovalAngryImage = null;
            }
        }

        private void method_2(Bitmap[] bitmap_0)
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

        public Bitmap ScaleLimitImage(Bitmap image, int maxWidth, int maxHeight, float alpha)
        {
            //Bitmap bitmap = null;
            double val = (double)maxWidth / (double)image.Width;
            double val2 = (double)maxHeight / (double)image.Height;
            double num = Math.Min(val, val2);
            int int_ = (int)((double)image.Width * num);
            int int_2 = (int)((double)image.Height * num);
            return method_3(image, int_, int_2, alpha);
        }

        internal Bitmap method_3(Bitmap bitmap_0, int int_0, int int_1, float float_2)
        {
            return method_4(bitmap_0, int_0, int_1, float_2, bool_1: false);
        }

        internal Bitmap method_4(Bitmap bitmap_0, int int_0, int int_1, float float_2, bool bool_1)
        {
            if (int_0 < 1)
            {
                int_0 = 1;
            }
            if (int_1 < 1)
            {
                int_1 = 1;
            }
            ImageAttributes imageAttr = main_0.method_20(float_2);
            Bitmap bitmap = new Bitmap(int_0, int_1, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            if (bool_1)
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

        public void SetSizeFactor(float sizeFactor)
        {
            if (sizeFactor <= 1f)
            {
                float_0 = 1f;
            }
            else if (sizeFactor <= 1.33f)
            {
                float_0 = 1.33f;
            }
            else
            {
                float_0 = 1.77f;
            }
            method_0();
            SelectionButtonWidth = (int)(26f * float_0);
            SelectionButtonHeight = (int)(26f * float_0);
            ImageSize = (int)(30f * float_0);
            for (int i = 0; i < Panels.Count; i++)
            {
                ItemListPanel itemListPanel = Panels[i];
                if (itemListPanel != null)
                {
                    itemListPanel.ItemHeight = (int)((float)itemListPanel.DefaultItemHeight * itemListPanel.ItemHeightOverrideFactor * float_0);
                    itemListPanel.TitleBarHeight = (int)((float)itemListPanel.DefaultTitleBarHeight * float_0);
                    itemListPanel.ToggleButtonHeight = (int)((float)itemListPanel.DefaultToggleButtonHeight * float_0);
                    itemListPanel.ScrollUpHeight = (int)((float)itemListPanel.DefaultScrollUpHeight * float_0);
                    itemListPanel.ScrollDownHeight = (int)((float)itemListPanel.DefaultScrollDownHeight * float_0);
                    itemListPanel.ScrollAmountPerClick = (int)((float)itemListPanel.DefaultScrollAmountPerClick * float_0);
                    int val = Math.Min(SelectionButtonHeight, SelectionButtonWidth) - 4;
                    int val2 = (int)(16f * float_0);
                    val = Math.Min(val2, val);
                    Bitmap bitmap = (itemListPanel.IconImage = ScaleLimitImage(itemListPanel.IconImageOriginal, val, val, AlphaTransparency));
                }
            }
        }

        public void Initialize(Main parent, Rectangle area)
        {
            main_0 = parent;
            method_0();
            Area = area;
        }

        public void AddPanel(string title, Bitmap iconImage, Type itemType)
        {
            AddPanel(title, iconImage, itemType, new List<string[]>());
        }

        public void AddPanel(string title, Bitmap iconImage, Type itemType, float itemHeightOverrideFactor)
        {
            AddPanel(title, iconImage, itemType, new List<string[]>(), itemHeightOverrideFactor);
        }

        public void AddPanel(string title, Bitmap iconImage, Type itemType, List<string[]> toggleButtonText)
        {
            AddPanel(title, iconImage, itemType, toggleButtonText, 1f);
        }

        public void AddPanel(string title, Bitmap iconImage, Type itemType, List<string[]> toggleButtonText, float itemHeightOverrideFactor)
        {
            int val = Math.Min(SelectionButtonHeight, SelectionButtonWidth) - 4;
            int val2 = (int)(16f * float_0);
            val = Math.Min(val2, val);
            Bitmap iconImage2 = ScaleLimitImage(iconImage, val, val, AlphaTransparency);
            ItemListPanel itemListPanel = new ItemListPanel(title, iconImage, itemType, toggleButtonText);
            itemListPanel.IconImage = iconImage2;
            itemListPanel.ItemHeightOverrideFactor = itemHeightOverrideFactor;
            itemListPanel.Initialize(this, itemType);
            if (Panels == null)
            {
                Panels = new ItemListPanelList();
            }
            Panels.Add(itemListPanel);
        }

        public Bitmap DrawPanelToImage()
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = ((!(main_0.double_0 > 500.0)) ? new Bitmap(AreaMaximum.Width + 1, AreaMaximum.Height + 1, PixelFormat.Format32bppArgb) : new Bitmap(AreaMaximum.Width + 1, AreaMaximum.Height + 1, PixelFormat.Format32bppPArgb));
                using Graphics graphics = Graphics.FromImage(bitmap);
                DrawPanel(graphics, 0, 0);
                return bitmap;
            }
            catch (Exception ex)
            {
                Main.CrashDump(ex);
                return bitmap;
            }
        }

        public void DrawPanel(Graphics graphics)
        {
            DrawPanel(graphics, Area.X, Area.Y);
        }

        public void DrawPanel(Graphics graphics, int areaX, int areaY)
        {
            try
            {
                if (!Visible)
                {
                    return;
                }
                Parent.method_112(graphics, GraphicsQuality.Low);
                if (Panels != null)
                {
                    int num = Panels.Count * SelectionButtonHeight;
                    int num2 = areaY + (Area.Height - num) / 2;
                    if (num2 < areaY)
                    {
                        int y = Area.Y + (Area.Height - num) / 2;
                        AreaMaximum = new Rectangle(Area.X, y, Area.Width, num);
                        if (areaY == 0)
                        {
                            num2 = 0;
                        }
                    }
                    else
                    {
                        AreaMaximum = Area;
                    }
                    for (int i = 0; i < Panels.Count; i++)
                    {
                        Color color = Color.FromArgb(80, 170, 170, 170);
                        Color color2 = Color.FromArgb(112, 170, 170, 170);
                        if (Panels[i] == HoveredPanelButton)
                        {
                            color = Color.FromArgb(144, 170, 170, 170);
                            color2 = Color.FromArgb(192, 170, 170, 170);
                        }
                        Rectangle rect = ResolvePanelSelectionButtonArea(i, areaX, areaY);
                        using (SolidBrush brush = new SolidBrush(color))
                        {
                            graphics.FillRectangle(brush, rect);
                        }
                        using (Pen pen = new Pen(color2))
                        {
                            graphics.DrawRectangle(pen, rect);
                        }
                        if (Panels[i].IconImage != null)
                        {
                            graphics.DrawImage(point: new Point(areaX + (SelectionButtonWidth - Panels[i].IconImage.Width) / 2, num2 + (SelectionButtonHeight - Panels[i].IconImage.Height) / 2), image: Panels[i].IconImage);
                        }
                        num2 += SelectionButtonHeight;
                    }
                }
                if (ActivePanel != null)
                {
                    int x = areaX + SelectionButtonWidth + 2;
                    int num3 = Panels.Count * SelectionButtonHeight;
                    int y2 = Math.Max(0, areaY - (Area.Height - num3) / 2);
                    ActivePanel.DrawPanel(graphics, x, y2, ActivePanelArea.Width, ActivePanelArea.Height);
                }
                Parent.method_112(graphics, GraphicsQuality.High);
            }
            catch (Exception)
            {
                ActivePanel = null;
            }
        }

        public Rectangle ResolvePanelSelectionButtonArea(int panelIndex, int areaX, int areaY)
        {
            int num = Panels.Count * SelectionButtonHeight;
            int num2 = areaY + (Area.Height - num) / 2;
            if (areaY == 0)
            {
                num2 = Math.Max(0, num2);
            }
            int y = num2 + panelIndex * SelectionButtonHeight;
            return new Rectangle(areaX, y, SelectionButtonWidth, SelectionButtonHeight);
        }

        public int ResolvePanelIndex(Point point)
        {
            int num = -1;
            if (point.X >= AreaMaximum.Left && point.X < AreaMaximum.Left + SelectionButtonWidth)
            {
                int num2 = Panels.Count * SelectionButtonHeight;
                int num3 = Area.Top + (Area.Height - num2) / 2;
                int num4 = point.Y - num3;
                num = num4 / SelectionButtonHeight;
                if (num >= Panels.Count)
                {
                    num = -1;
                }
            }
            return num;
        }

        public bool DoBindPanel(ItemListPanel panel)
        {
            if (eventHandler_1 != null && panel != null)
            {
                eventHandler_1(this, new BindItemPanelEventArgs(panel));
                return true;
            }
            return false;
        }

        public bool DetectHoveredElement(Point point)
        {
            bool flag = false;
            if (Visible)
            {
                object hoveredItem = null;
                if (ActivePanel != null && ActivePanel.DetectHoveredElement(point, out hoveredItem))
                {
                    flag = true;
                }
                ItemListPanel itemListPanel = null;
                int num = ResolvePanelIndex(point);
                if (num >= 0)
                {
                    itemListPanel = Panels[num];
                    DoBindPanel(itemListPanel);
                    flag = true;
                }
                HoveredPanelButton = itemListPanel;
                if (itemListPanel != null)
                {
                    if (itemListPanel == ActivePanel)
                    {
                        Parent.string_17 = itemListPanel.TitleText + ": " + TextResolver.GetText("click to close items");
                    }
                    else
                    {
                        Parent.string_17 = itemListPanel.TitleText + ": " + TextResolver.GetText("click to show items");
                    }
                }
            }
            if (flag)
            {
                NeedsRefresh = true;
            }
            return flag;
        }

        public bool MouseDown(Point point)
        {
            if (Visible && AreaMaximum.Contains(point) && ActivePanel != null && ActivePanel.CheckMouseDown(point))
            {
                NeedsRefresh = true;
                return true;
            }
            return false;
        }

        public bool ScrollWheel(Point point, int scrollAmount)
        {
            if (Visible && AreaMaximum.Contains(point) && ActivePanel != null && ActivePanel.CheckScrollWheel(point, scrollAmount))
            {
                NeedsRefresh = true;
                return true;
            }
            return false;
        }

        public bool CheckClick(Point point, MouseButtons buttonClicked, bool isDoubleClick)
        {
            if (Visible && AreaMaximum.Contains(point))
            {
                if (DetectHoveredElement(point) && HoveredPanelButton != null)
                {
                    if (ActivePanel == HoveredPanelButton)
                    {
                        ActivePanel = null;
                        NeedsRefresh = true;
                        return true;
                    }
                    ActivePanel = HoveredPanelButton;
                    ActivePanelArea = new Rectangle(Area.Left + SelectionButtonWidth + 2, Area.Top, Area.Width - (SelectionButtonWidth + 2), Area.Height);
                    DoBindPanel(HoveredPanelButton);
                    NeedsRefresh = true;
                    return true;
                }
                if (ActivePanel != null && ActivePanel.CheckClick(point, buttonClicked, isDoubleClick))
                {
                    NeedsRefresh = true;
                    return true;
                }
            }
            return false;
        }

        public bool ClickItem(object item, MouseButtons buttonClicked, bool isDoubleClick, bool bidButtonClicked)
        {
            if (eventHandler_0 != null)
            {
                eventHandler_0(this, new ItemClickedEventArgs(item, buttonClicked, isDoubleClick, bidButtonClicked));
                NeedsRefresh = true;
                return true;
            }
            return false;
        }

        public bool ClickToggleButton(ItemListPanel panel)
        {
            if (eventHandler_2 != null)
            {
                eventHandler_2(this, new BindItemPanelEventArgs(panel));
                NeedsRefresh = true;
                return true;
            }
            return false;
        }

        public void CycleChangeSize()
        {
            int num = 0;
            num = ((float_0 <= 1f) ? 1 : ((float_0 <= 1.33f) ? 2 : 0));
            main_0.ChangeItemPanelSize(num);
        }

        public ShipGroup ResolveAssignedFleet(PrioritizedTarget target, out int missionQueueIndex)
        {
            missionQueueIndex = 0;
            ShipGroupList shipGroups = Parent._Game.PlayerEmpire.ShipGroups;
            if (shipGroups != null)
            {
                for (int i = 0; i < shipGroups.Count; i++)
                {
                    if (shipGroups[i].Mission == null || (shipGroups[i].Mission.Type != BuiltObjectMissionType.Attack && shipGroups[i].Mission.Type != BuiltObjectMissionType.Bombard && shipGroups[i].Mission.Type != BuiltObjectMissionType.WaitAndAttack && shipGroups[i].Mission.Type != BuiltObjectMissionType.WaitAndBombard) || shipGroups[i].Mission.Target != target.Target)
                    {
                        if (shipGroups[i].SubsequentMissions == null || shipGroups[i].SubsequentMissions.Count <= 0)
                        {
                            continue;
                        }
                        for (int j = 0; j < shipGroups[i].SubsequentMissions.Count; j++)
                        {
                            if (shipGroups[i].SubsequentMissions[j] != null && (shipGroups[i].SubsequentMissions[j].Type == BuiltObjectMissionType.Attack || shipGroups[i].SubsequentMissions[j].Type == BuiltObjectMissionType.Bombard || shipGroups[i].SubsequentMissions[j].Type == BuiltObjectMissionType.WaitAndAttack || shipGroups[i].SubsequentMissions[j].Type == BuiltObjectMissionType.WaitAndBombard) && shipGroups[i].SubsequentMissions[j].Target == target.Target)
                            {
                                missionQueueIndex = j + 1;
                                return shipGroups[i];
                            }
                        }
                        continue;
                    }
                    return shipGroups[i];
                }
            }
            return null;
        }

        public ItemListCollectionPanel():base()
        {
            
            Visible = true;
            Panels = new ItemListPanelList();
            float_0 = 1f;
            SelectionButtonHeight = 26;
            SelectionButtonWidth = 26;
            WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
            BlackBrush = new SolidBrush(Color.Black);
            ImageSize = 30;
            AlphaTransparency = 0.6f;
            LastRefresh = DateTime.MinValue;
        }
    }

}
