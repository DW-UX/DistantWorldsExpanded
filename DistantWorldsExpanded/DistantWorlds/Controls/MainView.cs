// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MainView
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

//using BaconDistantWorlds;

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
using System.ComponentModel;

namespace DistantWorlds.Controls
{
    public partial class MainView : Panel
    {
        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 13369376u,
            SRCPAINT = 15597702u,
            SRCAND = 8913094u,
            SRCINVERT = 6684742u,
            SRCERASE = 4457256u,
            NOTSRCCOPY = 3342344u,
            NOTSRCERASE = 1114278u,
            MERGECOPY = 12583114u,
            MERGEPAINT = 12255782u,
            PATCOPY = 15728673u,
            PATPAINT = 16452105u,
            PATINVERT = 5898313u,
            DSTINVERT = 5570569u,
            BLACKNESS = 66u,
            WHITENESS = 16711778u
        }

        internal Main main_0;

        private Galaxy galaxy_0;

        internal bool bool_0;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private SolidBrush solidBrush_2;

        private SolidBrush solidBrush_3;

        private SolidBrush solidBrush_4;

        private SolidBrush solidBrush_5;

        private Font font_0;

        private Font font_1;

        private Font font_2;

        private Font font_3;

        private SpriteFont spriteFont_0;

        private SpriteFont spriteFont_1;

        private SpriteFont spriteFont_2;

        private SpriteFont spriteFont_3;

        private SpriteFont spriteFont_4;

        private GraphicsPath graphicsPath_0;

        private GraphicsPath graphicsPath_1;

        private GraphicsPath graphicsPath_2;

        private List<int> list_0;

        private List<Bitmap> list_1;

        private List<Texture2D> list_2;

        private List<System.Drawing.Rectangle> list_3;

        private List<System.Drawing.Rectangle> list_4;

        private System.Drawing.Rectangle[] rectangle_0;

        internal List<Bitmap> list_5;

        internal List<Texture2D> list_6;

        internal List<DateTime> list_7;

        internal List<Bitmap> list_8;

        internal int int_0;

        internal Bitmap[] bitmap_0;

        internal Texture2D[] texture2D_0;

        private List<Bitmap> list_9;

        private List<Bitmap> list_10;

        private List<Texture2D> list_11;

        private List<Bitmap> list_12;

        private List<Texture2D> list_13;

        private List<int> list_14;

        private List<BuiltObjectImageSize> list_15;

        private List<Bitmap> list_16;

        private List<Bitmap> list_17;

        private List<Texture2D> list_18;

        private List<Bitmap> list_19;

        private List<Texture2D> list_20;

        private List<int> list_21;

        private List<Bitmap[]> list_22;

        private List<Bitmap[]> list_23;

        private List<int> list_24;

        private List<System.Drawing.Rectangle> list_25;

        private Bitmap[] bitmap_1;

        private Texture2D[] texture2D_1;

        private Texture2D[] texture2D_2;

        private Texture2D[] texture2D_3;

        private Texture2D[] texture2D_4;

        private Texture2D[] texture2D_5;

        private Texture2D texture2D_6;

        private Texture2D[] texture2D_7;

        private Texture2D[][] texture2D_8;

        private Texture2D texture2D_9;

        private Texture2D texture2D_10;

        private Texture2D texture2D_11;

        private Texture2D[] texture2D_12;

        private Texture2D[] texture2D_13;

        private Texture2D[] texture2D_14;

        private Texture2D[] texture2D_15;

        private Texture2D[] texture2D_16;

        private Texture2D texture2D_17;

        private Texture2D[] texture2D_18;

        private Texture2D[] texture2D_19;

        private Texture2D[] texture2D_20;

        private Texture2D[] texture2D_21;

        private Texture2D texture2D_22;

        private Texture2D texture2D_23;

        private Texture2D texture2D_24;

        private Texture2D texture2D_25;

        private Texture2D[] texture2D_26;

        private Texture2D texture2D_27;

        private Texture2D texture2D_28;

        private Texture2D texture2D_29;

        private Texture2D[] texture2D_30;

        private Texture2D[] texture2D_31;

        private Texture2D[] texture2D_32;

        private Texture2D[] texture2D_33;

        private Texture2D[] texture2D_34;

        public List<Texture2D[]> list_26;

        public List<Texture2D[]> list_27;

        public Texture2D texture2D_35;

        private Texture2D texture2D_36;

        private Texture2D texture2D_37;

        private Texture2D texture2D_38;

        private Texture2D texture2D_39;

        private Texture2D texture2D_40;

        private Texture2D texture2D_41;

        private Texture2D texture2D_42;

        private Texture2D texture2D_43;

        private Texture2D texture2D_44;

        private Texture2D texture2D_45;

        private Texture2D texture2D_46;

        private Texture2D texture2D_47;

        private Texture2D texture2D_48;

        private Texture2D texture2D_49;

        private Texture2D texture2D_50;

        private Texture2D texture2D_51;

        private Texture2D texture2D_52;

        private System.Drawing.Point[] point_0;

        private System.Drawing.Point[] point_1;

        private Pen pen_0;

        private Pen pen_1;

        private Pen pen_2;

        private Pen pen_3;

        private Pen pen_4;

        private Pen pen_5;

        private Pen pen_6;

        private Pen pen_7;

        private Pen pen_8;

        private System.Drawing.Color color_0;

        private System.Drawing.Color color_1;

        private StarFieldItemList starFieldItemList_0;

        private StarFieldItemList starFieldItemList_1;

        private StarFieldItemList starFieldItemList_2;

        private StarFieldItemList starFieldItemList_3;

        private int int_1;

        private int int_2;

        private int int_3;

        private int int_4;

        private Texture2D texture2D_53;

        private Texture2D texture2D_54;

        private Texture2D texture2D_55;

        private System.Drawing.Rectangle[] rectangle_1;

        private System.Drawing.Rectangle[] rectangle_2;

        private System.Drawing.Rectangle[] rectangle_3;

        private System.Drawing.Rectangle[] rectangle_4;

        private SolidBrush solidBrush_6;

        private SolidBrush solidBrush_7;

        private SolidBrush solidBrush_8;

        private SolidBrush solidBrush_9;

        private SolidBrush solidBrush_10;

        private SolidBrush solidBrush_11;

        private SolidBrush solidBrush_12;

        private SolidBrush solidBrush_13;

        private SolidBrush solidBrush_14;

        private SolidBrush solidBrush_15;

        private SolidBrush solidBrush_16;

        private SolidBrush solidBrush_17;

        private SolidBrush solidBrush_18;

        private SolidBrush MlpRhYjTejC;

        private SolidBrush solidBrush_19;

        private SolidBrush solidBrush_20;

        private System.Drawing.Color color_2;

        private System.Drawing.Color color_3;

        private System.Drawing.Color color_4;

        private Habitat habitat_0;

        private BuiltObject builtObject_0;

        private List<Control> list_28;

        private SystemInfo systemInfo_0;

        private ShipGroup shipGroup_0;

        private bool bool_1;

        private bool bool_2;

        private bool bool_3;

        private bool bool_4;

        private bool bool_5;

        private System.Drawing.Color color_5;

        private System.Drawing.Color color_6;

        private System.Drawing.Color color_7;

        private System.Drawing.Color color_8;

        private System.Drawing.Color color_9;

        private System.Drawing.Color color_10;

        private Pen pen_9;

        private Pen pen_10;

        private System.Drawing.Color color_11;

        private System.Drawing.Color color_12;

        private System.Drawing.Color color_13;

        private System.Drawing.Color color_14;

        private double pWyRhJbxOto;

        private double double_0;

        private double double_1;

        private DateTime dateTime_0;

        internal double double_2;

        private NebulaCloudGenerator nebulaCloudGenerator_0;

        internal NebulaCloudGenerator nebulaCloudGenerator_1;

        private SectorCloudGenerator sectorCloudGenerator_0;

        internal Bitmap bitmap_2;

        internal Bitmap bitmap_3;

        internal Texture2D texture2D_56;

        internal Texture2D texture2D_57;

        internal RectangleF rectangleF_0;

        internal double double_3;

        internal bool bool_6;

        internal Bitmap bitmap_4;

        private SectorCloudGenerator sectorCloudGenerator_1;

        //private PlanetaryRingsGenerator planetaryRingsGenerator_0;

        //private GalaxyNebulaeGenerator galaxyNebulaeGenerator_0;

        private LightningGenerator lightningGenerator_0;

        private System.Drawing.Color color_15;

        private int int_5;

        private DateTime dateTime_1;

        private bool bool_7;

        private int int_6;

        private DateTime dateTime_2;

        private double double_4;

        private double double_5;

        //private double double_6;

        public DistantWorlds.AnimationSystem animationSystem_0;

        internal DistantWorlds.AnimationSystem animationSystem_1;

        private double double_7;

        private double double_8;

        private Bitmap[] bitmap_5;

        private int int_7;

        private int int_8;

        private Bitmap[] bitmap_6;

        private double double_9;

        private long long_0;

        internal bool bool_8;

        private List<EventPing> list_29;

        private double double_10;

        private DateTime dateTime_3;

        private double double_11;

        private System.Drawing.Color color_16;

        private int int_9;

        private double double_12;

        private DateTime dateTime_4;

        private double double_13;

        private System.Drawing.Color color_17;

        private int int_10;

        private System.Drawing.Point point_2;

        private System.Drawing.Point point_3;

        private System.Drawing.Point point_4;

        internal bool bool_9;

        private List<Bitmap> list_30;

        private List<Texture2D> list_31;

        private object object_0;

        internal double double_14;

        private Texture2D texture2D_58;

        private Texture2D texture2D_59;

        private Texture2D texture2D_60;

        public BuiltObjectList SpecialHighlightBuiltObjects;

        private bool bool_10;

        internal List<Bitmap> list_32;

        internal List<Texture2D> list_33;

        internal List<Bitmap> list_34;

        internal List<System.Drawing.Point> list_35;

        internal bool bool_11;

        internal volatile bool bool_12;

        internal SpriteBatch spriteBatch_0;

        internal SpriteBatch spriteBatch_1;

        private PresentationParameters presentationParameters_0;

        private Class1 class1_0;

        private ServiceContainer serviceContainer_0;

        private ContentManager contentManager_0;

        public List<EventPing> EventLocations => list_29;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public System.Drawing.Point HoverMessageLocation
        {
            get
            {
                return point_2;
            }
            set
            {
                point_2 = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public System.Drawing.Point HoverMessageLocationMap
        {
            get
            {
                return point_3;
            }
            set
            {
                point_3 = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public System.Drawing.Point HoverMessageLocationButtons
        {
            get
            {
                return point_4;
            }
            set
            {
                point_4 = value;
            }
        }

        public GraphicsDevice GraphicsDevice => class1_0.GraphicsDevice;

        public ServiceContainer Services => serviceContainer_0;

        public void Extinguish()
        {
            galaxy_0 = null;
            habitat_0 = null;
            builtObject_0 = null;
            if (list_28 != null)
            {
                list_28.Clear();
            }
            systemInfo_0 = null;
            shipGroup_0 = null;
        }

        public void Kickstart(Main parentForm, Galaxy galaxy)
        {
            main_0 = parentForm;
            galaxy_0 = galaxy;
            if (font_0 != null)
            {
                font_0.Dispose();
            }
            if (font_1 != null)
            {
                font_1.Dispose();
            }
            if (font_2 != null)
            {
                font_2.Dispose();
            }
            if (font_3 != null)
            {
                font_3.Dispose();
            }
            font_0 = ((IFontCache)parentForm).GenerateFont(16.67f, isBold: false);
            font_1 = ((IFontCache)parentForm).GenerateFont(18.67f, isBold: true);
            font_2 = ((IFontCache)parentForm).GenerateFont(15.33f, isBold: false);
            font_3 = ((IFontCache)parentForm).GenerateFont(10.67f, isBold: false);
            Font = ((IFontCache)parentForm).GenerateFont(15.33f, isBold: false);
            contentManager_0 = new ContentManager(serviceContainer_0);
            contentManager_0.RootDirectory = Application.StartupPath;
            spriteFont_0 = contentManager_0.Load<SpriteFont>("TinyFont");
            spriteFont_1 = contentManager_0.Load<SpriteFont>("SmallFont");
            spriteFont_2 = contentManager_0.Load<SpriteFont>("BoldFont");
            spriteFont_3 = contentManager_0.Load<SpriteFont>("NormalFont");
            spriteFont_4 = contentManager_0.Load<SpriteFont>("TitleFont");
            if (main_0._Game != null)
            {
                method_14(main_0._Game.StarFieldSize);
            }
            solidBrush_13 = new SolidBrush(System.Drawing.Color.FromArgb(104, 104, 112));
            solidBrush_14 = new SolidBrush(System.Drawing.Color.FromArgb(72, 72, 80));
            solidBrush_15 = new SolidBrush(System.Drawing.Color.FromArgb(72, 72, 80));
            solidBrush_16 = new SolidBrush(System.Drawing.Color.FromArgb(112, 112, 120));
            solidBrush_17 = new SolidBrush(System.Drawing.Color.FromArgb(88, 88, 96));
            solidBrush_18 = new SolidBrush(System.Drawing.Color.FromArgb(88, 88, 96));
            MlpRhYjTejC = new SolidBrush(System.Drawing.Color.FromArgb(128, 128, 136));
            solidBrush_19 = new SolidBrush(System.Drawing.Color.FromArgb(104, 104, 112));
            solidBrush_20 = new SolidBrush(System.Drawing.Color.FromArgb(104, 104, 112));
            pen_9 = new Pen(color_10, 2f);
            pen_10 = new Pen(color_9, 2f);
            pen_2 = new Pen(System.Drawing.Color.FromArgb(170, 170, 170), 1f);
            pen_2.DashPattern = new float[2] { 1f, 5f };
            AdjustableArrowCap customEndCap = new AdjustableArrowCap(3f, 5f);
            pen_2.CustomEndCap = customEndCap;
            pen_3 = new Pen(System.Drawing.Color.Yellow, 1f);
            pen_3.DashPattern = new float[2] { 4f, 2f };
            AdjustableArrowCap customEndCap2 = new AdjustableArrowCap(5f, 7f);
            pen_3.CustomEndCap = customEndCap2;
            pen_4 = new Pen(System.Drawing.Color.FromArgb(220, 220, 220), 3f);
            pen_5.StartCap = LineCap.Round;
            pen_5.EndCap = LineCap.Round;
            pen_6.StartCap = LineCap.Round;
            pen_6.EndCap = LineCap.Round;
            method_17();
            bitmap_4 = new Bitmap(main_0.bitmap_187);
            nebulaCloudGenerator_1.PopulateNoise(700);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
            SetStyle(ControlStyles.UserPaint, value: true);
            SetStyle(ControlStyles.Opaque, value: true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
            UpdateStyles();
            double_9 = Math.Sqrt(Math.Pow(base.Width / 2, 2.0) + Math.Pow(base.Height / 2, 2.0));
            point_0 = yItRuxAmaVX(Math.Max(base.Width, base.Height));
            point_1 = method_47(Math.Max(base.Width, base.Height));
            method_0();
            animationSystem_0 = new DistantWorlds.AnimationSystem(this);
            animationSystem_1 = new DistantWorlds.AnimationSystem(this);
            color_15 = System.Drawing.Color.Empty;
            int_5 = 0;
            dateTime_1 = DateTime.MinValue;
            bool_0 = true;
        }

        private void method_0()
        {
            if (main_0.bitmap_196 == null)
            {
                return;
            }
            if (bitmap_1 != null)
            {
                for (int i = 0; i < bitmap_1.Length; i++)
                {
                    if (bitmap_1[i] != null)
                    {
                        bitmap_1[i].Dispose();
                        bitmap_1[i] = null;
                    }
                }
            }
            bitmap_1 = new Bitmap[main_0.bitmap_196.Length];
            for (int j = 0; j < main_0.bitmap_196.Length; j++)
            {
                double num = (double)main_0.bitmap_196[j].Width / (double)main_0.bitmap_196[j].Height;
                int num2 = (int)(60.0 / num);
                bitmap_1[j] = main_0.PrecacheScaledBitmap(main_0.bitmap_196[j], 60, num2, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias, PixelFormat.Format32bppPArgb);
            }
        }

        private void method_1()
        {
            if (double_1 < 0.0)
            {
                return;
            }
            int num = (int)((pWyRhJbxOto - (double)main_0.int_13) / main_0.double_0) + base.Width / 2;
            int num2 = (int)((double_0 - (double)main_0.int_14) / main_0.double_0) + base.Height / 2;
            TimeSpan timeSpan = DateTime.Now.ToUniversalTime().Subtract(dateTime_0);
            dateTime_0 = DateTime.Now.ToUniversalTime();
            double num3 = 2.4 * timeSpan.TotalSeconds;
            double num4 = 3000.0;
            if ((num > 20 && num < base.ClientRectangle.Width - 20 && num2 > 20 && num2 < base.ClientRectangle.Height - 20) || main_0.double_0 >= num4)
            {
                if (main_0.int_13 == (int)pWyRhJbxOto && main_0.int_14 == (int)double_0)
                {
                    if (!(double_1 >= 0.0))
                    {
                        return;
                    }
                    if (main_0.double_0 <= 1.0 && double_1 <= 1.0)
                    {
                        pWyRhJbxOto = -1.0;
                        double_0 = -1.0;
                        double_1 = -1.0;
                    }
                    else if (main_0.double_0 > double_1)
                    {
                        double num5 = main_0.double_0;
                        num5 -= num5 * num3;
                        main_0.method_4(num5);
                        main_0.bool_19 = true;
                        if (main_0.double_0 <= double_1)
                        {
                            main_0.method_4(double_1);
                            pWyRhJbxOto = -1.0;
                            double_0 = -1.0;
                            double_1 = -1.0;
                        }
                    }
                    else if (main_0.double_0 < double_1)
                    {
                        double num6 = main_0.double_0;
                        num6 += num6 * num3;
                        main_0.method_4(num6);
                        main_0.bool_19 = true;
                        if (main_0.double_0 >= double_1)
                        {
                            main_0.method_4(double_1);
                            pWyRhJbxOto = -1.0;
                            double_0 = -1.0;
                            double_1 = -1.0;
                        }
                    }
                }
                else
                {
                    double num7 = Math.Min(1.0, Math.Abs(((double)main_0.int_13 - pWyRhJbxOto) / ((double)main_0.int_14 - double_0)));
                    double num8 = Math.Min(1.0, Math.Abs(((double)main_0.int_14 - double_0) / ((double)main_0.int_13 - pWyRhJbxOto)));
                    double num9 = (int)(500.0 * main_0.double_0 * timeSpan.TotalSeconds * num7);
                    double num10 = (int)(500.0 * main_0.double_0 * timeSpan.TotalSeconds * num8);
                    if ((double)main_0.int_13 > pWyRhJbxOto)
                    {
                        num9 *= -1.0;
                    }
                    if ((double)main_0.int_14 > double_0)
                    {
                        num10 *= -1.0;
                    }
                    if (num > base.Width / 2 - 80 && num < base.Width / 2 + 80 && num2 > base.Height / 2 - 80 && num2 < base.Height / 2 + 80)
                    {
                        num9 = Math.Min(15.0 * main_0.double_0, num9);
                        num10 = Math.Min(15.0 * main_0.double_0, num10);
                    }
                    main_0.int_13 += (int)num9;
                    main_0.int_14 += (int)num10;
                    num = (int)((pWyRhJbxOto - (double)main_0.int_13) / main_0.double_0) + base.Width / 2;
                    num2 = (int)((double_0 - (double)main_0.int_14) / main_0.double_0) + base.Height / 2;
                    if (num > base.Width / 2 - 20 && num < base.Width / 2 + 20 && num2 > base.Height / 2 - 20 && num2 < base.Height / 2 + 20)
                    {
                        main_0.int_13 = (int)pWyRhJbxOto;
                        main_0.int_14 = (int)double_0;
                        main_0.method_149();
                    }
                }
            }
            else
            {
                double num11 = main_0.double_0;
                num11 += num11 * num3;
                if (num11 >= num4)
                {
                    num11 = num4;
                }
                main_0.method_4(num11);
                main_0.bool_19 = true;
            }
        }

        internal void method_2(double double_15, double double_16, double double_17)
        {
            pWyRhJbxOto = double_15;
            double_0 = double_16;
            double_1 = double_17;
            dateTime_0 = DateTime.Now.ToUniversalTime();
        }

        internal void method_3()
        {
            habitat_0 = null;
            builtObject_0 = null;
            list_28 = null;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_4 = false;
            bool_2 = false;
            bool_3 = false;
            bool_1 = false;
            bool_5 = false;
        }

        internal void method_4(Habitat habitat_1, bool bool_13, bool bool_14, bool bool_15, bool bool_16)
        {
            habitat_0 = habitat_1;
            builtObject_0 = null;
            list_28 = null;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_4 = bool_13;
            bool_2 = bool_14;
            bool_3 = bool_15;
            bool_1 = bool_16;
            bool_5 = false;
        }

        internal void method_5(Habitat habitat_1)
        {
            habitat_0 = habitat_1;
            builtObject_0 = null;
            list_28 = null;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_4 = false;
            bool_2 = false;
            bool_3 = false;
            bool_1 = false;
            bool_5 = false;
        }

        internal void method_6(List<object> items)
        {
            habitat_0 = null;
            builtObject_0 = null;
            list_28 = null;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_5 = false;
            foreach (object item in items)
            {
                if (item is BuiltObject)
                {
                    builtObject_0 = (BuiltObject)item;
                }
                else if (item is Habitat)
                {
                    habitat_0 = (Habitat)item;
                }
                else if (item is Control)
                {
                    if (list_28 == null)
                    {
                        list_28 = new List<Control>();
                    }
                    list_28.Add((Control)item);
                }
                else if (item is SystemInfo)
                {
                    systemInfo_0 = (SystemInfo)item;
                }
            }
        }

        internal void method_7(BuiltObject builtObject_1)
        {
            habitat_0 = null;
            builtObject_0 = builtObject_1;
            list_28 = null;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_5 = false;
        }

        internal void method_8(BuiltObject builtObject_1, Control control_0)
        {
            habitat_0 = null;
            builtObject_0 = builtObject_1;
            List<Control> list = new List<Control>();
            list.Add(control_0);
            list_28 = list;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_5 = false;
        }

        internal void method_9(SystemInfo systemInfo_1)
        {
            habitat_0 = null;
            builtObject_0 = null;
            list_28 = null;
            systemInfo_0 = systemInfo_1;
            shipGroup_0 = null;
            bool_5 = false;
        }

        internal void method_10(ShipGroup shipGroup_1)
        {
            habitat_0 = null;
            builtObject_0 = null;
            list_28 = null;
            systemInfo_0 = null;
            shipGroup_0 = shipGroup_1;
            bool_5 = false;
        }

        internal void method_11(List<Control> actionButtons)
        {
            habitat_0 = null;
            builtObject_0 = null;
            list_28 = actionButtons;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_5 = true;
        }

        internal void method_12(Control control_0)
        {
            habitat_0 = null;
            builtObject_0 = null;
            List<Control> list = new List<Control>();
            list.Add(control_0);
            list_28 = list;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_5 = false;
        }

        internal void method_13(List<Control> controls)
        {
            habitat_0 = null;
            builtObject_0 = null;
            list_28 = controls;
            systemInfo_0 = null;
            shipGroup_0 = null;
            bool_5 = false;
        }

        internal void method_14(int int_11)
        {
            int num = Math.Max(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            int_1 = num + 400;
            if (bool_9)
            {
                int_11 /= 4;
                int_2 = num / 2 + 50;
                int_3 = num / 2 + 50;
                int_4 = num / 2 + 50;
            }
            else
            {
                int_2 = num + 400;
                int_3 = num + 400;
                int_4 = num + 400;
            }
            int_2 = Math.Min(2000, int_2);
            int_3 = Math.Min(2000, int_3);
            int_4 = Math.Min(2000, int_4);
            double d = 1.0;
            if (!bool_9)
            {
                d = Math.Max(1.0, main_0.double_0);
            }
            double num2 = Math.Sqrt(Math.Sqrt(d));
            int_11 = (int)((double)int_11 / num2);
            int num3 = int_11 / 4;
            int num4 = int_11 * 4;
            int num5 = num4 * 4;
            if (bool_11)
            {
                starFieldItemList_0 = method_101(int_1, num3, 20);
                rectangle_1 = method_15(num3, 20);
                if (bool_9)
                {
                    starFieldItemList_1 = method_101(int_2, int_11, 11);
                    rectangle_2 = method_15(int_11, 11);
                    starFieldItemList_2 = method_101(int_3, num4, 7);
                    rectangle_3 = method_15(num4, 7);
                    starFieldItemList_3 = method_101(int_4, num5, 6);
                    rectangle_4 = method_15(num5, 6);
                }
                else
                {
                    starFieldItemList_1 = method_101(int_2, int_11, 2);
                    rectangle_2 = method_15(int_11, 2);
                    starFieldItemList_2 = method_101(int_3, num4, 2);
                    rectangle_3 = method_15(num4, 2);
                    starFieldItemList_3 = method_101(int_4, num5, 1);
                    rectangle_4 = method_15(num5, 1);
                }
            }
            else
            {
                starFieldItemList_0 = method_101(int_1, num3, 2);
                rectangle_1 = method_15(num3, 2);
                starFieldItemList_1 = method_101(int_2, int_11, 2);
                rectangle_2 = method_15(int_11, 2);
                starFieldItemList_2 = method_101(int_3, num4, 2);
                rectangle_3 = method_15(num4, 2);
                starFieldItemList_3 = method_101(int_4, num5, 1);
                rectangle_4 = method_15(num5, 1);
            }
            if (texture2D_53 != null && !texture2D_53.IsDisposed)
            {
                method_22(texture2D_53);
            }
            if (texture2D_54 != null && !texture2D_54.IsDisposed)
            {
                method_22(texture2D_54);
            }
            if (texture2D_55 != null && !texture2D_55.IsDisposed)
            {
                method_22(texture2D_55);
            }
            if (bool_9)
            {
                method_107(int_2, int_2, out texture2D_53, out texture2D_54, out texture2D_55);
            }
        }

        private System.Drawing.Rectangle[] method_15(int int_11, int int_12)
        {
            System.Drawing.Rectangle[] array = new System.Drawing.Rectangle[int_11];
            for (int i = 0; i < int_11; i++)
            {
                ref System.Drawing.Rectangle reference = ref array[i];
                reference = new System.Drawing.Rectangle(0, 0, int_12, int_12);
            }
            return array;
        }

        private Bitmap method_16(Bitmap bitmap_7, float float_0)
        {
            Bitmap bitmap = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            method_175(graphics);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5] { 0f, 0f, 0f, float_0, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(bitmap_7, destRect, 0f, 0f, bitmap_7.Width, bitmap_7.Height, GraphicsUnit.Pixel, imageAttributes);
            imageAttributes.Dispose();
            return bitmap;
        }

        private void method_17()
        {
            bitmap_6 = new Bitmap[main_0._uiResourcesBitmaps.Length];
            for (int i = 0; i < main_0._uiResourcesBitmaps.Length; i++)
            {
                Bitmap bitmap = main_0._uiResourcesBitmaps[i];
                Bitmap bitmap2 = new Bitmap(16, 16, PixelFormat.Format32bppPArgb);
                double val = (double)bitmap.Width / 16.0;
                double val2 = (double)bitmap.Height / 16.0;
                double num = Math.Max(val, val2);
                Size size = new Size((int)((double)bitmap.Width / num), (int)((double)bitmap.Height / num));
                using (Graphics graphics = Graphics.FromImage(bitmap2))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    System.Drawing.Point location = new System.Drawing.Point((16 - size.Width) / 2, (16 - size.Height) / 2);
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(location, size);
                    graphics.DrawImage(bitmap, rect);
                }
                bitmap_6[i] = bitmap2;
            }
        }

        private void method_18(SpriteBatch spriteBatch_2)
        {
            if (main_0 == null || !main_0.pnlSystemMap.Visible)
            {
                return;
            }
            string text = string.Empty;
            if (main_0._Game != null && main_0._Game.Galaxy != null && main_0._Game.Galaxy.TimeSpeed != 1.0)
            {
                text = "  (" + main_0._Game.Galaxy.TimeSpeed.ToString("0.##") + "x)";
            }
            XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, main_0.string_21 + text, spriteFont_2, solidBrush_0.Color, 12, 120);
            if (main_0.double_0 < 100.0)
            {
                Vector2 vector = spriteFont_2.MeasureString(main_0.string_22);
                int num = base.Width - ((int)vector.X + 40);
                XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, main_0.string_22, spriteFont_2, solidBrush_0.Color, num, 67);
            }
            int num2 = base.Size.Width - 95;
            System.Drawing.Color color = System.Drawing.Color.FromArgb(144, 144, 144);
            if (main_0._Game != null && main_0._Game.PlayerEmpire != null)
            {
                if (texture2D_23 != null)
                {
                    XnaDrawingHelper.DrawTexture(spriteBatch_1, texture2D_23, num2 - (texture2D_23.Width + 73), 12, 0f, 1f);
                }
                XnaDrawingHelper.DrawString(spriteBatch_2, TextResolver.GetText("Money"), spriteFont_3, color, num2 - 57, 7);
                if (main_0._Game.PlayerEmpire.StateMoney >= 0.0)
                {
                    XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, main_0.string_23, spriteFont_2, solidBrush_0.Color, num2, 4);
                }
                else
                {
                    XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, main_0.string_23, spriteFont_2, solidBrush_5.Color, num2, 4);
                }
                XnaDrawingHelper.DrawString(spriteBatch_2, TextResolver.GetText("Cashflow"), spriteFont_3, color, num2 - 71, 27);
                if (main_0.AheLexjQsu >= 0.0)
                {
                    XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, "(" + main_0.string_24 + ")", spriteFont_3, solidBrush_0.Color, num2 - 4, 27);
                }
                else
                {
                    XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, "(" + main_0.string_24 + ")", spriteFont_3, solidBrush_5.Color, num2 - 4, 27);
                }
                XnaDrawingHelper.DrawString(spriteBatch_2, TextResolver.GetText("Bonus Income"), spriteFont_3, color, num2 - 103, 47);
                XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, "(" + main_0.string_25 + ")", spriteFont_3, solidBrush_0.Color, num2 - 4, 45);
            }
        }

        private void DrawUPS(SpriteBatch spriteBatch, int currentUps)
        {
            int num2 = base.Size.Width - 250;
            XnaDrawingHelper.DrawString(spriteBatch, "UPS", spriteFont_3, System.Drawing.Color.FromArgb(144, 144, 144), num2 - 40, 7);
            XnaDrawingHelper.DrawStringDropShadow(spriteBatch, currentUps.ToString(), spriteFont_2, solidBrush_0.Color, num2, 4);
        }

        private void method_19(Graphics graphics_0)
        {
            if (main_0 == null || !main_0.pnlSystemMap.Visible)
            {
                return;
            }
            string text = string.Empty;
            if (main_0._Game != null && main_0._Game.Galaxy != null && main_0._Game.Galaxy.TimeSpeed != 1.0)
            {
                text = "  (" + main_0._Game.Galaxy.TimeSpeed.ToString("0.##") + "x)";
            }
            graphics_0.DrawString(main_0.string_21 + text, font_1, solidBrush_0, new System.Drawing.Point(10, 120));
            if (main_0.double_0 < 100.0)
            {
                SizeF sizeF = graphics_0.MeasureString(main_0.string_22, font_1, 500, StringFormat.GenericTypographic);
                int num = base.Width - ((int)sizeF.Width + 40);
                graphics_0.DrawString(main_0.string_22, font_1, solidBrush_0, new System.Drawing.Point(num, 67));
            }
            int num2 = base.Size.Width - 85;
            using SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(144, 144, 144));
            if (main_0._Game != null && main_0._Game.PlayerEmpire != null)
            {
                if (main_0.bitmap_52 != null)
                {
                    graphics_0.DrawImageUnscaled(main_0.bitmap_52, new System.Drawing.Point(num2 - (main_0.bitmap_52.Width + 52), 17));
                }
                graphics_0.DrawString(TextResolver.GetText("Money"), font_2, brush, new System.Drawing.Point(num2 - 40, 13));
                if (main_0._Game.PlayerEmpire.StateMoney >= 0.0)
                {
                    graphics_0.DrawString(main_0.string_23, font_1, solidBrush_0, new System.Drawing.Point(num2, 10));
                }
                else
                {
                    graphics_0.DrawString(main_0.string_23, font_1, solidBrush_5, new System.Drawing.Point(num2, 10));
                }
                graphics_0.DrawString(TextResolver.GetText("Cashflow"), font_2, brush, new System.Drawing.Point(num2 - 53, 30));
                if (main_0.AheLexjQsu >= 0.0)
                {
                    graphics_0.DrawString("(" + main_0.string_24 + ")", font_2, solidBrush_0, new System.Drawing.Point(num2, 30));
                }
                else
                {
                    graphics_0.DrawString("(" + main_0.string_24 + ")", font_2, solidBrush_5, new System.Drawing.Point(num2, 30));
                }
                graphics_0.DrawString(TextResolver.GetText("Bonus Income"), font_2, brush, new System.Drawing.Point(num2 - 75, 45));
                graphics_0.DrawString("(" + main_0.string_25 + ")", font_2, solidBrush_0, new System.Drawing.Point(num2, 45));
            }
        }

        private void method_20()
        {
            if (list_30.Count <= 0 && list_31.Count <= 0)
            {
                return;
            }
            lock (object_0)
            {
                for (int i = 0; i < list_30.Count; i++)
                {
                    if (list_30[i] != null && list_30[i].PixelFormat != 0)
                    {
                        list_30[i].Dispose();
                    }
                }
                list_30.Clear();
                for (int j = 0; j < list_31.Count; j++)
                {
                    if (list_31[j] != null && !list_31[j].IsDisposed)
                    {
                        list_31[j].Dispose();
                    }
                }
                list_31.Clear();
            }
        }

        internal void method_21(Bitmap bitmap_7)
        {
            lock (object_0)
            {
                list_30.Add(bitmap_7);
            }
        }

        internal void method_22(Texture2D texture2D_61)
        {
            lock (object_0)
            {
                list_31.Add(texture2D_61);
            }
        }

        private void method_23()
        {
            if (main_0 != null && main_0.double_0 != double_14)
            {
                main_0.method_4(double_14);
            }
        }

        private string method_24()
        {
            if (bool_11 && bool_12 && GraphicsDevice != null)
            {
                string text = method_26();
                if (!string.IsNullOrEmpty(text))
                {
                    return text;
                }
                Viewport viewport = default(Viewport);
                viewport.X = 0;
                viewport.Y = 0;
                viewport.Width = base.ClientSize.Width;
                viewport.Height = base.ClientSize.Height;
                viewport.MinDepth = 0f;
                viewport.MaxDepth = 1f;
                GraphicsDevice.Viewport = viewport;
            }
            return null;
        }

        private void method_25()
        {
            try
            {
                //Microsoft.Xna.Framework.Rectangle value = new Microsoft.Xna.Framework.Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
                //GraphicsDevice.Present(value, null, base.Handle);
                GraphicsDevice.Present();
            }
            catch
            {
            }
        }

        private string method_26()
        {
            bool flag = false;
            switch (GraphicsDevice.GraphicsDeviceStatus)
            {
                default:
                    {
                        PresentationParameters presentationParameters = GraphicsDevice.PresentationParameters;
                        flag = base.ClientSize.Width > presentationParameters.BackBufferWidth || base.ClientSize.Height > presentationParameters.BackBufferHeight;
                        break;
                    }
                case GraphicsDeviceStatus.Lost:
                    flag = true;
                    break;
                case GraphicsDeviceStatus.NotReset:
                    flag = true;
                    break;
            }
            if (flag)
            {
                try
                {
                    method_27(base.ClientSize.Width, base.ClientSize.Height);
                }
                catch (Exception ex)
                {
                    return "Graphics device reset failed\n\n" + ex;
                }
            }
            return null;
        }

        private void method_27(int int_11, int int_12)
        {
            if (Main.ApplicationIsActivated())
            {
                presentationParameters_0.BackBufferWidth = Math.Max(presentationParameters_0.BackBufferWidth, int_11);
                presentationParameters_0.BackBufferHeight = Math.Max(presentationParameters_0.BackBufferHeight, int_12);
                Thread.Sleep(100);
                class1_0.method_1(presentationParameters_0.BackBufferWidth, presentationParameters_0.BackBufferHeight);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        public MainView() : base()
        {

            solidBrush_0 = new SolidBrush(System.Drawing.Color.White);
            solidBrush_1 = new SolidBrush(System.Drawing.Color.Yellow);
            solidBrush_2 = new SolidBrush(System.Drawing.Color.Black);
            solidBrush_3 = new SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40));
            solidBrush_4 = new SolidBrush(System.Drawing.Color.FromArgb(112, 112, 112));
            solidBrush_5 = new SolidBrush(System.Drawing.Color.Red);
            graphicsPath_0 = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding);
            graphicsPath_1 = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding);
            graphicsPath_2 = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding);
            list_0 = new List<int>();
            list_1 = new List<Bitmap>();
            list_2 = new List<Texture2D>();
            list_3 = new List<System.Drawing.Rectangle>();
            list_4 = new List<System.Drawing.Rectangle>();
            rectangle_0 = new System.Drawing.Rectangle[2500];
            list_5 = new List<Bitmap>(600);
            list_6 = new List<Texture2D>(600);
            list_7 = new List<DateTime>();
            list_8 = new List<Bitmap>(600);
            int_0 = 249;
            list_9 = new List<Bitmap>();
            list_10 = new List<Bitmap>();
            list_11 = new List<Texture2D>();
            list_12 = new List<Bitmap>();
            list_13 = new List<Texture2D>();
            list_14 = new List<int>();
            list_15 = new List<BuiltObjectImageSize>();
            list_16 = new List<Bitmap>();
            list_17 = new List<Bitmap>();
            list_18 = new List<Texture2D>();
            list_19 = new List<Bitmap>();
            list_20 = new List<Texture2D>();
            list_21 = new List<int>();
            list_22 = new List<Bitmap[]>();
            list_23 = new List<Bitmap[]>();
            list_24 = new List<int>();
            list_25 = new List<System.Drawing.Rectangle>();
            bitmap_1 = new Bitmap[94];
            texture2D_1 = new Texture2D[8];
            texture2D_2 = new Texture2D[9];
            texture2D_3 = new Texture2D[4];
            texture2D_4 = new Texture2D[1];
            texture2D_5 = new Texture2D[1];
            texture2D_7 = new Texture2D[2];
            texture2D_8 = new Texture2D[20][];
            texture2D_12 = new Texture2D[2];
            texture2D_13 = new Texture2D[100];
            texture2D_14 = new Texture2D[3];
            texture2D_15 = new Texture2D[100];
            texture2D_16 = new Texture2D[100];
            texture2D_26 = new Texture2D[5];
            texture2D_30 = new Texture2D[5];
            list_26 = new List<Texture2D[]>();
            list_27 = new List<Texture2D[]>();
            pen_0 = new Pen(System.Drawing.Color.FromArgb(20, 20, 20), 1f);
            pen_1 = new Pen(System.Drawing.Color.FromArgb(0, 0, 48), 1f);
            pen_2 = new Pen(System.Drawing.Color.FromArgb(170, 170, 170), 1f);
            pen_3 = new Pen(System.Drawing.Color.FromArgb(224, 224, 224), 1f);
            pen_4 = new Pen(System.Drawing.Color.FromArgb(220, 220, 220), 3f);
            pen_5 = new Pen(System.Drawing.Color.FromArgb(255, 102, 0), 3f);
            pen_6 = new Pen(System.Drawing.Color.FromArgb(255, 161, 140), 1f);
            pen_7 = new Pen(System.Drawing.Color.FromArgb(0, 118, 107), 3f);
            pen_8 = new Pen(System.Drawing.Color.FromArgb(0, 255, 224), 1f);
            color_0 = System.Drawing.Color.FromArgb(255, 80, 80, 80);
            color_1 = System.Drawing.Color.FromArgb(255, 160, 160, 160);
            int_1 = 2500;
            int_2 = 2500;
            int_3 = 2500;
            int_4 = 2500;
            solidBrush_10 = new SolidBrush(System.Drawing.Color.FromArgb(192, 192, 192));
            solidBrush_11 = new SolidBrush(System.Drawing.Color.FromArgb(208, 208, 208));
            solidBrush_12 = new SolidBrush(System.Drawing.Color.FromArgb(224, 224, 224));
            solidBrush_13 = new SolidBrush(System.Drawing.Color.FromArgb(160, 160, 160));
            solidBrush_14 = new SolidBrush(System.Drawing.Color.FromArgb(112, 112, 112));
            solidBrush_15 = new SolidBrush(System.Drawing.Color.FromArgb(72, 72, 72));
            solidBrush_16 = new SolidBrush(System.Drawing.Color.FromArgb(176, 176, 176));
            solidBrush_17 = new SolidBrush(System.Drawing.Color.FromArgb(128, 128, 128));
            solidBrush_18 = new SolidBrush(System.Drawing.Color.FromArgb(88, 88, 88));
            MlpRhYjTejC = new SolidBrush(System.Drawing.Color.FromArgb(192, 192, 192));
            solidBrush_19 = new SolidBrush(System.Drawing.Color.FromArgb(144, 144, 144));
            solidBrush_20 = new SolidBrush(System.Drawing.Color.FromArgb(104, 104, 104));
            color_2 = System.Drawing.Color.FromArgb(255, 255, 255, 0);
            color_3 = System.Drawing.Color.FromArgb(255, 255, 255, 0);
            color_4 = System.Drawing.Color.FromArgb(128, 255, 48, 96);
            color_5 = System.Drawing.Color.FromArgb(255, 255, 24, 128);
            color_6 = System.Drawing.Color.FromArgb(255, 255, 64, 176);
            color_7 = System.Drawing.Color.FromArgb(128, 112, 0, 160);
            color_8 = System.Drawing.Color.FromArgb(224, 255, 32, 112);
            color_9 = System.Drawing.Color.FromArgb(64, 0, 255);
            color_10 = System.Drawing.Color.FromArgb(255, 0, 96);
            color_11 = System.Drawing.Color.FromArgb(64, 255, 0);
            color_12 = System.Drawing.Color.FromArgb(36, 144, 0);
            color_13 = System.Drawing.Color.FromArgb(8, 32, 0);
            color_14 = System.Drawing.Color.FromArgb(255, 96, 0);
            pWyRhJbxOto = -1.0;
            double_0 = -1.0;
            double_1 = -1.0;
            dateTime_0 = DateTime.MinValue;
            double_2 = 4.5;
            nebulaCloudGenerator_0 = new NebulaCloudGenerator(1);
            nebulaCloudGenerator_1 = new NebulaCloudGenerator(2);
            double_3 = 1.0;
            //planetaryRingsGenerator_0 = new PlanetaryRingsGenerator();
            lightningGenerator_0 = new LightningGenerator();
            color_15 = System.Drawing.Color.Empty;
            dateTime_1 = DateTime.MinValue;
            bool_7 = true;
            int_6 = 20;
            dateTime_2 = DateTime.Now;
            double_5 = 0.2;
            //double_6 = 0.4;
            double_7 = 1.5;
            double_8 = 1.0;
            bitmap_5 = new Bitmap[3];
            int_7 = -1;
            int_8 = 400;
            list_29 = new List<EventPing>();
            dateTime_3 = DateTime.MinValue;
            double_11 = 2.4;
            color_16 = System.Drawing.Color.FromArgb(255, 255, 0);
            int_9 = 100;
            dateTime_4 = DateTime.MinValue;
            double_13 = 2.4;
            color_17 = System.Drawing.Color.FromArgb(96, 64, 255);
            int_10 = 40;
            bool_9 = true;
            list_30 = new List<Bitmap>();
            list_31 = new List<Texture2D>();
            object_0 = new object();
            double_14 = 1.0;
            SpecialHighlightBuiltObjects = new BuiltObjectList();
            bool_11 = true;
            serviceContainer_0 = new ServiceContainer();
        }

        public void DrawMainViewXna(int upsCounter)
        {
            DateTime now = DateTime.Now;
            bool_11 = true;
            if (bool_11 && !bool_12)
            {
                method_79();
            }
            string text = method_24();
            if (string.IsNullOrEmpty(text))
            {
                method_23();
                method_1();
                if (!bool_0 || galaxy_0 == null)
                {
                    return;
                }
                Habitat habitat = null;
                if (main_0.int_28 >= 0 && main_0.int_28 < galaxy_0.Habitats.Count)
                {
                    habitat = galaxy_0.Habitats[main_0.int_28];
                }
                else if (habitat == null)
                {
                    habitat = galaxy_0.FastFindNearestSystem(main_0.int_13, main_0.int_14);
                }
                method_244(main_0.int_13, main_0.int_14, base.Width, base.Height, main_0.double_0);
                GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
                if (main_0.double_0 > 500.0)
                {
                    spriteBatch_0.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone);
                    spriteBatch_1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone);
                }
                else
                {
                    spriteBatch_0.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone);
                    spriteBatch_1.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone);
                }
                bool_7 = false;
                if (main_0._Game != null && main_0._Game.ShowSystemNebulae && habitat != null && habitat.Category == HabitatCategoryType.Star && habitat.Type != HabitatType.BlackHole && habitat.Type != HabitatType.SuperNova && main_0.double_0 < 210.0)
                {
                    method_42(null, spriteBatch_0, (int)habitat.Xpos, (int)habitat.Ypos, habitat.SystemIndex);
                }
                if (main_0.double_0 > 70.0)
                {
                    if (main_0.bool_24)
                    {
                        method_132(null, spriteBatch_0, main_0.texture2D_5);
                    }
                    else
                    {
                        method_132(null, spriteBatch_0, main_0.texture2D_3);
                    }
                }
                if (main_0.double_0 < 1000.0)
                {
                    method_76();
                }
                if (main_0.double_0 > BaconMain.minZoomLevelForWeaponsCircles)
                {
                    int num = (int)((main_0.double_0 - 70.0) * 1.2);
                    if (num < 0)
                    {
                        num = 0;
                    }
                    if (num > 255)
                    {
                        num = 255;
                    }
                    method_250(spriteBatch_0, spriteBatch_1, main_0.int_13, main_0.int_14, base.Width, base.Height, main_0.double_0, num);
                }
                if (bool_0)
                {
                    if (main_0.itemListCollectionPanel_0 != null && main_0.itemListCollectionPanel_0.Visible)
                    {
                        if (bool_5)
                        {
                            int num2 = main_0.itemListCollectionPanel_0.SelectionButtonHeight * main_0.itemListCollectionPanel_0.Panels.Count;
                            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(main_0.itemListCollectionPanel_0.AreaMaximum.X - 2, main_0.itemListCollectionPanel_0.AreaMaximum.Y + (main_0.itemListCollectionPanel_0.AreaMaximum.Height - num2) / 2 - 2, main_0.itemListCollectionPanel_0.SelectionButtonWidth + 4, num2 + 4);
                            if (main_0.itemListCollectionPanel_0.ActivePanel != null)
                            {
                                rectangle = main_0.itemListCollectionPanel_0.ActivePanelArea;
                            }
                            XnaDrawingHelper.DrawRectangle(spriteBatch_0, rectangle, color_2, 4);
                        }
                        if (main_0.itemListCollectionPanel_0.NeedsRefresh || now.Subtract(main_0.itemListCollectionPanel_0.LastRefresh).TotalSeconds > 0.5)
                        {
                            Texture2D texture2D_ = texture2D_58;
                            Bitmap bitmap = main_0.itemListCollectionPanel_0.DrawPanelToImage();
                            if (bitmap != null && bitmap.PixelFormat != 0)
                            {
                                texture2D_58 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap);
                                method_21(bitmap);
                                method_22(texture2D_);
                            }
                            main_0.itemListCollectionPanel_0.LastRefresh = now;
                            main_0.itemListCollectionPanel_0.NeedsRefresh = false;
                        }
                        if (texture2D_58 != null && !texture2D_58.IsDisposed)
                        {
                            XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_58, main_0.itemListCollectionPanel_0.AreaMaximum.X, main_0.itemListCollectionPanel_0.AreaMaximum.Y, 0f, 1f);
                        }
                    }
                    if (!main_0.selectionMenu.Visible && !main_0.actionMenu.Visible && !main_0.bool_9)
                    {
                        if (!main_0.bool_10)
                        {
                            if (now.Subtract(main_0.hoverPanel_0.LastRefresh).TotalSeconds > 0.5)
                            {
                                Texture2D texture2D_2 = texture2D_59;
                                Bitmap bitmap2 = main_0.hoverPanel_0.DrawHoverInfoToImage(main_0.rectangle_0.Width, main_0.rectangle_0.Height);
                                texture2D_59 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap2);
                                main_0.hoverPanel_0.LastRefresh = now;
                                method_21(bitmap2);
                                method_22(texture2D_2);
                            }
                            XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_59, main_0.rectangle_0, 0f);
                        }
                        if (!string.IsNullOrEmpty(main_0.string_17))
                        {
                            XnaDrawingHelper.DrawStringDropShadow(spriteBatch_0, main_0.string_17, spriteFont_3, solidBrush_1.Color, point_2);
                        }
                        if (!string.IsNullOrEmpty(main_0.string_18))
                        {
                            XnaDrawingHelper.DrawStringDropShadow(spriteBatch_0, main_0.string_18, spriteFont_3, solidBrush_1.Color, point_3);
                        }
                        if (!string.IsNullOrEmpty(main_0.string_19))
                        {
                            Vector2 vector = spriteFont_3.MeasureString(main_0.string_19);
                            XnaDrawingHelper.DrawStringDropShadow(point: new System.Drawing.Point(point_4.X - (int)(vector.X / 2f), point_4.Y), spriteBatch: spriteBatch_0, text: main_0.string_19, font: spriteFont_3, foreColor: solidBrush_1.Color);
                        }
                    }
                    if (main_0.diplomaticMessageQueue_0 != null && main_0.diplomaticMessageQueue_0.Visible)
                    {
                        if (main_0.diplomaticMessageQueue_0.NeedsRefresh || now.Subtract(main_0.diplomaticMessageQueue_0.LastRefresh).TotalSeconds > 0.5)
                        {
                            Texture2D texture2D_3 = texture2D_60;
                            main_0.diplomaticMessageQueue_0.DrawMessagesToImage();
                            texture2D_60 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.diplomaticMessageQueue_0.PanelImage);
                            method_22(texture2D_3);
                        }
                        System.Drawing.Rectangle destination = new System.Drawing.Rectangle(main_0.diplomaticMessageQueue_0.Area.X, main_0.diplomaticMessageQueue_0.Area.Y, main_0.diplomaticMessageQueue_0.Area.Width, main_0.diplomaticMessageQueue_0.Area.Height + 3);
                        XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_60, destination, 0f);
                    }
                    if (galaxy_0.TimeState == GalaxyTimeState.Paused && !main_0.pnlGameEditor.Visible && !bool_8)
                    {
                        string text2 = TextResolver.GetText("Game Paused");
                        Vector2 vector2 = spriteFont_4.MeasureString(text2);
                        XnaDrawingHelper.DrawStringDropShadow(point: new System.Drawing.Point((base.Width - (int)vector2.X) / 2, 205), spriteBatch: spriteBatch_0, text: text2, font: spriteFont_4, foreColor: solidBrush_1.Color);
                        string text3 = "(" + TextResolver.GetText("Press Spacebar or Pause key to resume") + ")";
                        Vector2 vector3 = spriteFont_3.MeasureString(text3);
                        XnaDrawingHelper.DrawStringDropShadow(point: new System.Drawing.Point((base.Width - (int)vector3.X) / 2, 230), spriteBatch: spriteBatch_0, text: text3, font: spriteFont_3, foreColor: solidBrush_1.Color);
                    }
                    if (!string.IsNullOrEmpty(main_0.string_20))
                    {
                        Vector2 vector4 = spriteFont_4.MeasureString(main_0.string_20);
                        XnaDrawingHelper.DrawStringDropShadow(point: new System.Drawing.Point((base.Width - (int)vector4.X) / 2, 260), spriteBatch: spriteBatch_0, text: main_0.string_20, font: spriteFont_4, foreColor: solidBrush_1.Color);
                    }
                    method_18(spriteBatch_0);
                    DrawUPS(spriteBatch_0, upsCounter);
                    method_32(spriteBatch_0);
                }
                spriteBatch_0.End();
                spriteBatch_1.End();
                method_25();
                method_20();
                return;
            }
            throw new ApplicationException(text);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (bool_11)
            {
                return;
            }
            method_23();
            method_1();
            if (bool_0 && galaxy_0 != null)
            {
                Habitat habitat = null;
                if (main_0.int_28 >= 0)
                {
                    habitat = galaxy_0.Habitats[main_0.int_28];
                }
                else if (habitat == null)
                {
                    habitat = galaxy_0.FastFindNearestSystem(main_0.int_13, main_0.int_14);
                }
                e.Graphics.SetClip(base.ClientRectangle, CombineMode.Replace);
                method_244(main_0.int_13, main_0.int_14, base.Width, base.Height, main_0.double_0);
                bool_7 = false;
                if (main_0._Game != null && main_0._Game.ShowSystemNebulae && habitat != null && habitat.Category == HabitatCategoryType.Star && habitat.Type != HabitatType.BlackHole && habitat.Type != HabitatType.SuperNova && main_0.double_0 < 210.0)
                {
                    method_42(e.Graphics, null, (int)habitat.Xpos, (int)habitat.Ypos, habitat.SystemIndex);
                }
                if (main_0.double_0 > 70.0)
                {
                    method_132(e.Graphics, null, null);
                }
                if (main_0.double_0 < 1000.0)
                {
                    method_74(e.Graphics);
                }
                if (main_0.double_0 > 5.0)
                {
                    int num = (int)((main_0.double_0 - 70.0) * 1.2);
                    if (num < 0)
                    {
                        num = 0;
                    }
                    if (num > 255)
                    {
                        num = 255;
                    }
                    method_248(main_0.int_13, main_0.int_14, base.Width, base.Height, main_0.double_0, e.Graphics, num);
                }
                if (bool_0)
                {
                    if (main_0.diplomaticMessageQueue_0 != null)
                    {
                        main_0.diplomaticMessageQueue_0.DrawMessages(e.Graphics);
                    }
                    if (main_0.itemListCollectionPanel_0 != null)
                    {
                        if (bool_5)
                        {
                            using Pen pen = new Pen(color_2, 4f);
                            int num2 = main_0.itemListCollectionPanel_0.SelectionButtonHeight * main_0.itemListCollectionPanel_0.Panels.Count;
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(main_0.itemListCollectionPanel_0.AreaMaximum.X - 2, main_0.itemListCollectionPanel_0.AreaMaximum.Y + (main_0.itemListCollectionPanel_0.AreaMaximum.Height - num2) / 2 - 2, main_0.itemListCollectionPanel_0.SelectionButtonWidth + 4, num2 + 4);
                            if (main_0.itemListCollectionPanel_0.ActivePanel != null)
                            {
                                rect = main_0.itemListCollectionPanel_0.ActivePanelArea;
                            }
                            e.Graphics.DrawRectangle(pen, rect);
                        }
                        main_0.itemListCollectionPanel_0.DrawPanel(e.Graphics);
                    }
                    if (!main_0.selectionMenu.Visible && !main_0.actionMenu.Visible && !main_0.bool_9)
                    {
                        if (!main_0.bool_10)
                        {
                            main_0.hoverPanel_0.DrawHoverInfo(e.Graphics, main_0.rectangle_0);
                        }
                        if (!string.IsNullOrEmpty(main_0.string_17))
                        {
                            System.Drawing.Point point = new System.Drawing.Point(point_2.X + 1, point_2.Y + 1);
                            e.Graphics.DrawString(main_0.string_17, main_0.font_6, solidBrush_2, point);
                            e.Graphics.DrawString(main_0.string_17, main_0.font_6, solidBrush_1, point_2);
                        }
                        if (!string.IsNullOrEmpty(main_0.string_18))
                        {
                            System.Drawing.Point point2 = new System.Drawing.Point(point_3.X + 1, point_3.Y + 1);
                            e.Graphics.DrawString(main_0.string_18, main_0.font_6, solidBrush_2, point2);
                            e.Graphics.DrawString(main_0.string_18, main_0.font_6, solidBrush_1, point_3);
                        }
                        if (!string.IsNullOrEmpty(main_0.string_19))
                        {
                            SizeF sizeF = e.Graphics.MeasureString(main_0.string_19, main_0.font_6, 600, StringFormat.GenericDefault);
                            System.Drawing.Point point3 = new System.Drawing.Point(point_4.X - (int)(sizeF.Width / 2f), point_4.Y);
                            System.Drawing.Point point4 = new System.Drawing.Point(point3.X + 1, point3.Y + 1);
                            e.Graphics.DrawString(main_0.string_19, main_0.font_6, solidBrush_2, point4);
                            e.Graphics.DrawString(main_0.string_19, main_0.font_6, solidBrush_1, point3);
                        }
                    }
                    if (galaxy_0.TimeState == GalaxyTimeState.Paused && !main_0.pnlGameEditor.Visible && !bool_8)
                    {
                        e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                        string s = TextResolver.GetText("Game Paused");
                        SizeF sizeF2 = e.Graphics.MeasureString(s, main_0.font_1, 600, StringFormat.GenericDefault);
                        System.Drawing.Point point5 = new System.Drawing.Point((base.Width - (int)sizeF2.Width) / 2, 205);
                        System.Drawing.Point point6 = new System.Drawing.Point(point5.X + 1, point5.Y + 1);
                        e.Graphics.DrawString(s, main_0.font_1, solidBrush_2, point6);
                        e.Graphics.DrawString(s, main_0.font_1, solidBrush_1, point5);
                        string s2 = "(" + TextResolver.GetText("Press Spacebar or Pause key to resume") + ")";
                        SizeF sizeF3 = e.Graphics.MeasureString(s2, main_0.font_6, 600, StringFormat.GenericDefault);
                        System.Drawing.Point point7 = new System.Drawing.Point((base.Width - (int)sizeF3.Width) / 2, 230);
                        System.Drawing.Point point8 = new System.Drawing.Point(point7.X + 1, point7.Y + 1);
                        e.Graphics.DrawString(s2, main_0.font_6, solidBrush_2, point8);
                        e.Graphics.DrawString(s2, main_0.font_6, solidBrush_1, point7);
                        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    }
                    if (!string.IsNullOrEmpty(main_0.string_20))
                    {
                        e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                        SizeF sizeF4 = e.Graphics.MeasureString(main_0.string_20, main_0.font_1, 600, StringFormat.GenericDefault);
                        System.Drawing.Point point9 = new System.Drawing.Point((base.Width - (int)sizeF4.Width) / 2, 260);
                        System.Drawing.Point point10 = new System.Drawing.Point(point9.X + 1, point9.Y + 1);
                        e.Graphics.DrawString(main_0.string_20, main_0.font_1, solidBrush_2, point10);
                        e.Graphics.DrawString(main_0.string_20, main_0.font_1, solidBrush_1, point9);
                        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    }
                }
                switch (main_0.ZoomStatus)
                {
                    case ZoomStatus.Zooming:
                        main_0.ZoomStatus = ZoomStatus.Stabilizing;
                        break;
                    case ZoomStatus.Stabilizing:
                        main_0.ZoomStatus = ZoomStatus.Stable;
                        main_0.method_5();
                        bool_0 = true;
                        break;
                }
            }
            method_19(e.Graphics);
            method_31(e.Graphics);
            method_20();
        }

        private System.Drawing.Color method_28(DateTime dateTime_5)
        {
            System.Drawing.Color result = System.Drawing.Color.Empty;
            TimeSpan timeSpan = dateTime_5.Subtract(dateTime_1);
            if (int_5 == 0)
            {
                double num = Galaxy.Rnd.NextDouble() * timeSpan.TotalSeconds;
                if (num > 10.0)
                {
                    int_5 = 1;
                    dateTime_1 = dateTime_5;
                    double double_ = 0.0;
                    double double_2 = 0.0;
                    method_90(base.Width / 2, base.Height / 2, main_0.double_0, out double_, out double_2);
                    main_0.method_0(main_0.EffectsPlayer.ResolveThunder(double_, double_2));
                }
            }
            else if (int_5 % 4 != 0)
            {
                result = System.Drawing.Color.FromArgb(192, 224, 192, 224);
                if (timeSpan.TotalSeconds > 0.08 + Galaxy.Rnd.NextDouble() * 0.08)
                {
                    int_5++;
                    dateTime_1 = dateTime_5;
                    result = System.Drawing.Color.Empty;
                    if (int_5 > 1 && Galaxy.Rnd.Next(0, 5) > 1)
                    {
                        int_5 = 0;
                    }
                }
            }
            else if (timeSpan.TotalSeconds > 0.08 + Galaxy.Rnd.NextDouble() * 0.04)
            {
                int_5++;
                dateTime_1 = dateTime_5;
                double double_3 = 0.0;
                double double_4 = 0.0;
                method_90(base.Width / 2, base.Height / 2, main_0.double_0, out double_3, out double_4);
                main_0.method_0(main_0.EffectsPlayer.ResolveThunder(double_3, double_4));
            }
            return result;
        }

        private System.Drawing.Color method_29()
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(120, 255, 96, 64);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(160, 232, 160, 0);
            double num = 0.0;
            int second = DateTime.Now.ToUniversalTime().Second;
            int num2 = DateTime.Now.ToUniversalTime().Millisecond;
            if (second % 2 == 1)
            {
                num2 += 1000;
            }
            num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
            byte alpha = (byte)(color.A - (byte)((double)(color.A - color2.A) * num));
            byte red = (byte)(color.R - (byte)((double)(color.R - color2.R) * num));
            byte green = (byte)(color.G - (byte)((double)(color.G - color2.G) * num));
            byte blue = (byte)(color.B - (byte)((double)(color.B - color2.B) * num));
            return System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        private void method_30()
        {
            double num = 0.0;
            int second = DateTime.Now.ToUniversalTime().Second;
            int num2 = DateTime.Now.ToUniversalTime().Millisecond;
            if (second % 2 == 1)
            {
                num2 += 1000;
            }
            num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
            byte alpha = (byte)(color_3.A - (byte)((double)(color_3.A - color_4.A) * num));
            byte red = (byte)(color_3.R - (byte)((double)(color_3.R - color_4.R) * num));
            byte green = (byte)(color_3.G - (byte)((double)(color_3.G - color_4.G) * num));
            byte blue = (byte)(color_3.B - (byte)((double)(color_3.B - color_4.B) * num));
            color_2 = System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        private void method_31(Graphics graphics_0)
        {
            method_30();
            if (list_28 == null)
            {
                return;
            }
            using Pen pen = new Pen(color_2, 4f);
            foreach (Control item in list_28)
            {
                if (item.Parent == main_0)
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(item.Location.X - 3, item.Location.Y - 3, item.Size.Width + 6, item.Size.Height + 6);
                    graphics_0.DrawRectangle(pen, rect);
                }
                if (item == this)
                {
                    System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(item.Location.X, item.Location.Y, item.Size.Width, item.Size.Height);
                    graphics_0.DrawRectangle(pen, rect2);
                }
            }
        }

        private void method_32(SpriteBatch spriteBatch_2)
        {
            method_30();
            if (list_28 == null)
            {
                return;
            }
            foreach (Control item in list_28)
            {
                if (item.Parent == main_0)
                {
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(item.Location.X - 3, item.Location.Y - 3, item.Size.Width + 6, item.Size.Height + 6);
                    XnaDrawingHelper.DrawRectangle(spriteBatch_2, rectangle, color_2, 4);
                }
                if (item == this)
                {
                    System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(item.Location.X, item.Location.Y, item.Size.Width, item.Size.Height);
                    XnaDrawingHelper.DrawRectangle(spriteBatch_2, rectangle2, color_2, 4);
                }
            }
        }

        public void ClearPreprocessedCreatureImages()
        {
            for (int i = 0; i < list_22.Count; i++)
            {
                if (list_22[i] == null)
                {
                    continue;
                }
                for (int j = 0; j < list_22[i].Length; j++)
                {
                    if (list_22[i][j] != null)
                    {
                        method_21(list_22[i][j]);
                        list_22[i][j] = null;
                    }
                }
            }
            list_22.Clear();
            for (int k = 0; k < list_23.Count; k++)
            {
                if (list_23[k] == null)
                {
                    continue;
                }
                for (int l = 0; l < list_23[k].Length; l++)
                {
                    if (list_23[k][l] != null)
                    {
                        method_21(list_23[k][l]);
                        list_23[k][l] = null;
                    }
                }
            }
            list_23.Clear();
            list_24.Clear();
        }

        public void ClearPreprocessedBuiltObjectImages()
        {
            for (int i = 0; i < list_9.Count; i++)
            {
                if (list_9[i] != null)
                {
                    method_21(list_9[i]);
                    list_9[i] = null;
                }
            }
            list_9.Clear();
            for (int j = 0; j < list_10.Count; j++)
            {
                if (list_10[j] != null)
                {
                    method_21(list_10[j]);
                    list_10[j] = null;
                }
            }
            list_10.Clear();
            for (int k = 0; k < list_11.Count; k++)
            {
                if (list_11[k] != null)
                {
                    method_22(list_11[k]);
                    list_11[k] = null;
                }
            }
            list_11.Clear();
            for (int l = 0; l < list_12.Count; l++)
            {
                if (list_12[l] != null)
                {
                    method_21(list_12[l]);
                    list_12[l] = null;
                }
            }
            list_12.Clear();
            for (int m = 0; m < list_13.Count; m++)
            {
                if (list_13[m] != null)
                {
                    method_22(list_13[m]);
                    list_13[m] = null;
                }
            }
            list_13.Clear();
            list_14.Clear();
            list_15.Clear();
        }

        public void ClearPreprocessedFighterImages()
        {
            for (int i = 0; i < list_16.Count; i++)
            {
                if (list_16[i] != null)
                {
                    method_21(list_16[i]);
                    list_16[i] = null;
                }
            }
            list_16.Clear();
            for (int j = 0; j < list_17.Count; j++)
            {
                if (list_17[j] != null)
                {
                    method_21(list_17[j]);
                    list_17[j] = null;
                }
            }
            list_17.Clear();
            for (int k = 0; k < list_18.Count; k++)
            {
                if (list_18[k] != null)
                {
                    method_22(list_18[k]);
                    list_18[k] = null;
                }
            }
            list_18.Clear();
            for (int l = 0; l < list_19.Count; l++)
            {
                if (list_19[l] != null)
                {
                    method_21(list_19[l]);
                    list_19[l] = null;
                }
            }
            list_19.Clear();
            for (int m = 0; m < list_20.Count; m++)
            {
                if (list_20[m] != null)
                {
                    method_22(list_20[m]);
                    list_20[m] = null;
                }
            }
            list_20.Clear();
            list_21.Clear();
        }

        public void ClearNebulaeImages()
        {
            if (list_32 != null)
            {
                for (int i = 0; i < list_32.Count; i++)
                {
                    if (list_32[i] != null)
                    {
                        method_21(list_32[i]);
                        list_32[i] = null;
                    }
                }
                list_32.Clear();
                list_32 = null;
            }
            if (list_33 == null)
            {
                return;
            }
            for (int j = 0; j < list_33.Count; j++)
            {
                if (list_33[j] != null)
                {
                    method_22(list_33[j]);
                    list_33[j] = null;
                }
            }
            list_33.Clear();
            list_33 = null;
        }

        public void ClearNebulaeImagesScaled()
        {
            if (list_34 == null)
            {
                return;
            }
            for (int i = 0; i < list_34.Count; i++)
            {
                if (list_34[i] != null)
                {
                    method_21(list_34[i]);
                    list_34[i] = null;
                }
            }
            list_34.Clear();
            list_34 = null;
        }

        public void ClearPrecachedColonyBars()
        {
            for (int i = 0; i < list_1.Count; i++)
            {
                if (list_1[i] != null)
                {
                    method_21(list_1[i]);
                    list_1[i] = null;
                }
            }
            list_1.Clear();
            for (int j = 0; j < list_2.Count; j++)
            {
                if (list_2[j] != null)
                {
                    method_22(list_2[j]);
                    list_2[j] = null;
                }
            }
            list_2.Clear();
            list_0.Clear();
        }

        public void ClearBackdropImages()
        {
            main_0.bool_15 = true;
            main_0.bool_13 = true;
            bool_6 = true;
        }

        public void ClearPrecachedHabitatBitmaps()
        {
            if (main_0.double_0 > 70.0)
            {
                PrepareGalaxyBackdrop();
            }
            ClearBackdropImages();
            for (int i = 0; i < list_5.Count; i++)
            {
                if (list_5[i] != null)
                {
                    method_21(list_5[i]);
                    list_5[i] = null;
                }
            }
            for (int j = 0; j < list_6.Count; j++)
            {
                if (list_6[j] != null)
                {
                    method_22(list_6[j]);
                    list_6[j] = null;
                }
            }
            for (int k = 0; k < list_8.Count; k++)
            {
                if (list_8[k] != null)
                {
                    method_21(list_8[k]);
                    list_8[k] = null;
                }
            }
            ClearPrecachedColonyBars();
            for (int l = 0; l < list_7.Count; l++)
            {
                _ = list_7[l];
                list_7[l] = DateTime.MinValue;
            }
        }

        private System.Drawing.Rectangle method_33(int int_11, int int_12, int int_13, int int_14, double double_15)
        {
            int num = (int)((double)int_13 / double_15);
            int num2 = (int)((double)int_14 / double_15);
            int num3 = base.Width / 2 + ((int)((double)int_11 / double_15) + num / 2) + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = base.Height / 2 + ((int)((double)int_12 / double_15) + num2 / 2) + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            return new System.Drawing.Rectangle(num3, num4, num, num2);
        }

        internal System.Drawing.Rectangle method_34(int int_11, int int_12, int int_13, int int_14, double double_15)
        {
            return method_35(int_11, int_12, int_13, int_14, double_15, 0);
        }

        internal System.Drawing.Rectangle method_35(int int_11, int int_12, int int_13, int int_14, double double_15, int int_15)
        {
            int val = (int)((double)int_13 / double_15);
            int val2 = (int)((double)int_14 / double_15);
            val = Math.Max(int_15, val);
            val2 = Math.Max(int_15, val2);
            int num = (int)(((double)int_11 - ((double)(main_0.int_13 + main_0.int_21) + (double)val * main_0.double_0 / 2.0)) / main_0.double_0) + base.Width / 2;
            int num2 = (int)(((double)int_12 - ((double)(main_0.int_14 + main_0.vhadzRiecM) + (double)val2 * main_0.double_0 / 2.0)) / main_0.double_0) + base.Height / 2;
            return new System.Drawing.Rectangle(num, num2, val, val2);
        }

        private void method_36(Graphics graphics_0, Bitmap bitmap_7, System.Drawing.Rectangle rectangle_5, int int_11, int int_12)
        {
            method_37(graphics_0, bitmap_7, rectangle_5, int_11, int_12, TernaryRasterOperations.SRCCOPY);
        }

        private void BtdRuuistp9(Graphics graphics_0, Bitmap bitmap_7, System.Drawing.Rectangle rectangle_5, int int_11, int int_12)
        {
            method_37(graphics_0, bitmap_7, rectangle_5, int_11, int_12, TernaryRasterOperations.SRCPAINT);
        }

        private void method_37(Graphics graphics_0, Bitmap bitmap_7, System.Drawing.Rectangle rectangle_5, int int_11, int int_12, TernaryRasterOperations ternaryRasterOperations_0)
        {
            int num = (int)graphics_0.GetHdc();
            int num2 = Gdi32.CreateCompatibleDC(num);
            int hgdiobj = Gdi32.SelectObject(num2, (int)bitmap_7.GetHbitmap());
            Gdi32.BitBlt(num, int_11, int_12, rectangle_5.Width, rectangle_5.Height, num2, rectangle_5.Left, rectangle_5.Top, (uint)ternaryRasterOperations_0);
            int hObject = Gdi32.SelectObject(num2, hgdiobj);
            Gdi32.DeleteObject(hObject);
            Gdi32.DeleteDC(num2);
            graphics_0.ReleaseHdc((IntPtr)num);
        }

        private void method_38(Graphics graphics_0, Bitmap bitmap_7, System.Drawing.Rectangle rectangle_5, System.Drawing.Rectangle rectangle_6)
        {
            int num = (int)graphics_0.GetHdc();
            int num2 = Gdi32.CreateCompatibleDC(num);
            int hgdiobj = Gdi32.SelectObject(num2, (int)bitmap_7.GetHbitmap());
            Gdi32.SetStretchBltMode(num, 3);
            Gdi32.StretchBlt(num, rectangle_6.X, rectangle_6.Y, rectangle_6.Width, rectangle_6.Height, num2, rectangle_5.Left, rectangle_5.Top, rectangle_5.Width, rectangle_5.Height, 13369376u);
            int hObject = Gdi32.SelectObject(num2, hgdiobj);
            Gdi32.DeleteObject(hObject);
            Gdi32.DeleteDC(num2);
            graphics_0.ReleaseHdc((IntPtr)num);
        }

        private Bitmap method_39(Bitmap bitmap_7, int int_11, int int_12)
        {
            try
            {
                if (double.IsNaN(int_11))
                {
                    int_11 = 500;
                }
                if (double.IsNaN(int_12))
                {
                    int_12 = 500;
                }
                int_11 = Math.Max(1, Math.Min(int_11, 2400));
                int_12 = Math.Max(1, Math.Min(int_12, 2400));
                Bitmap bitmap = new Bitmap(int_11, int_12, PixelFormat.Format32bppPArgb);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(System.Drawing.Color.Transparent);
                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawImage(bitmap_7, rect);
                graphics.Dispose();
                return bitmap;
            }
            catch (Exception)
            {
                return new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
            }
        }

        private void method_40(int int_11)
        {
            ThreadPool.QueueUserWorkItem(method_41, int_11);
        }

        private void method_41(object object_1)
        {
            bool_10 = true;
            int seed = (int)object_1;
            nebulaCloudGenerator_0.TransparencyLevel = 114 + (int)(64.0 / double_2);
            bool useLowQuality = false;
            if (main_0.ZoomStatus == ZoomStatus.Zooming)
            {
                useLowQuality = true;
            }
            nebulaCloudGenerator_0.GenerateNebulaBackdrop(seed, out var cloudImages, out var cloudPositions, double_2, useLowQuality);
            ClearNebulaeImages();
            if (bool_11 && bool_12)
            {
                list_33 = XnaDrawingHelper.ConvertBitmapsToTextures(GraphicsDevice, cloudImages, useAlternateBuffer: true);
            }
            list_32 = cloudImages;
            list_35 = cloudPositions;
            ClearNebulaeImagesScaled();
            bool_10 = false;
        }

        private void method_42(Graphics graphics_0, SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13)
        {
            int num = 25;
            double num2 = 1.0 + (main_0.double_0 - 1.0) / 50.0;
            if (double.IsNaN(num2))
            {
                num2 = 1.0;
            }
            num2 = Math.Max(1.0, Math.Min(num2, 5.0));
            if (list_32 == null && !bool_10)
            {
                bool_10 = true;
                method_40(int_13);
            }
            if (bool_10)
            {
                return;
            }
            if (!bool_11 && list_34 == null && list_32 != null)
            {
                ClearNebulaeImagesScaled();
                list_34 = new List<Bitmap>();
                for (int i = 0; i < list_32.Count; i++)
                {
                    int int_14 = (int)((double)list_32[i].Width / (num2 / double_2));
                    int int_15 = (int)((double)list_32[i].Height / (num2 / double_2));
                    list_34.Add(method_39(list_32[i], int_14, int_15));
                }
            }
            if ((list_34 == null && !bool_11) || list_32 == null || list_35 == null)
            {
                return;
            }
            int count = list_32.Count;
            for (int j = 0; j < count; j++)
            {
                if (list_32 == null || list_32.Count <= j)
                {
                    continue;
                }
                int int_16 = (int)((double)(int_11 - main_0.int_13) / (double)num + (double)(list_35[j].X - (int)((double)list_32[j].Width / 2.0 * double_2)));
                int int_17 = (int)((double)(int_12 - main_0.int_14) / (double)num + (double)(list_35[j].Y - (int)((double)list_32[j].Width / 2.0 * double_2)));
                System.Drawing.Rectangle rectangle = method_33(int_16, int_17, (int)((double)list_32[j].Width * double_2), (int)((double)list_32[j].Height * double_2), num2);
                if (!rectangle.IntersectsWith(base.ClientRectangle))
                {
                    continue;
                }
                bool_7 = true;
                if (bool_11)
                {
                    if (bool_12 && spriteBatch_2 != null && list_33 != null && list_33.Count > j && list_33[j] != null && !list_33[j].IsDisposed)
                    {
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, list_33[j], rectangle, 0f);
                    }
                }
                else
                {
                    method_175(graphics_0);
                    new System.Drawing.Rectangle(0, 0, list_34[j].Width, list_34[j].Height);
                    graphics_0.DrawImageUnscaledAndClipped(list_34[j], rectangle);
                    method_177(graphics_0);
                }
            }
        }

        private void method_43(Empire empire_0, int int_11, int int_12, int int_13, int int_14, double double_15, Graphics graphics_0)
        {
            if (empire_0 != null)
            {
                int num = empire_0.SmallFlagPicture.Width;
                int num2 = empire_0.SmallFlagPicture.Height;
                if (double_15 > 2.0)
                {
                    double num3 = 1.0 + (double_15 - 2.0) / 4.0;
                    num = (int)((double)num / num3);
                    num2 = (int)((double)num2 / num3);
                    num = Math.Max(8, num);
                    num2 = Math.Max(5, num2);
                }
                graphics_0.DrawImage(rect: new System.Drawing.Rectangle(int_11 - (num - 2), int_12 - (num2 - 2), num, num2), image: empire_0.SmallFlagPicture);
            }
        }

        private void method_44(Empire empire_0, int int_11, int int_12, int int_13, int int_14, double double_15, Graphics graphics_0)
        {
            int num = 24;
            int num2 = 15;
            if (double_15 > 3.0)
            {
                double num3 = 1.0 + (double_15 - 3.0) / 6.0;
                num = (int)((double)num / num3);
                num2 = (int)((double)num2 / num3);
                num = Math.Max(13, num);
                num2 = Math.Max(8, num2);
            }
            graphics_0.DrawImage(rect: new System.Drawing.Rectangle(int_11 - (num - 3), int_12 - (num2 - 3), num, num2), image: empire_0.LargeFlagPicture);
        }

        private float method_45()
        {
            return Math.Max(0f, Math.Min(1f, 1f / (float)Math.Sqrt(Math.Sqrt(main_0.double_0))));
        }

        private void method_46()
        {
            if (bool_9)
            {
                solidBrush_6 = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));
                solidBrush_7 = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));
                solidBrush_8 = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));
                solidBrush_9 = new SolidBrush(System.Drawing.Color.FromArgb(127, 127, 127));
                return;
            }
            solidBrush_13 = new SolidBrush(System.Drawing.Color.FromArgb(160, 160, 160));
            solidBrush_14 = new SolidBrush(System.Drawing.Color.FromArgb(112, 112, 112));
            solidBrush_15 = new SolidBrush(System.Drawing.Color.FromArgb(72, 72, 72));
            solidBrush_16 = new SolidBrush(System.Drawing.Color.FromArgb(176, 176, 176));
            solidBrush_17 = new SolidBrush(System.Drawing.Color.FromArgb(128, 128, 128));
            solidBrush_18 = new SolidBrush(System.Drawing.Color.FromArgb(88, 88, 88));
            MlpRhYjTejC = new SolidBrush(System.Drawing.Color.FromArgb(192, 192, 192));
            solidBrush_19 = new SolidBrush(System.Drawing.Color.FromArgb(144, 144, 144));
            solidBrush_20 = new SolidBrush(System.Drawing.Color.FromArgb(104, 104, 104));
            if (main_0.double_0 <= 20.0)
            {
                solidBrush_6 = solidBrush_10;
                solidBrush_7 = solidBrush_13;
                solidBrush_8 = solidBrush_14;
                solidBrush_9 = solidBrush_15;
            }
            else if (main_0.double_0 <= 80.0)
            {
                solidBrush_6 = solidBrush_11;
                solidBrush_7 = solidBrush_16;
                solidBrush_8 = solidBrush_17;
                solidBrush_9 = solidBrush_18;
            }
            else
            {
                solidBrush_6 = solidBrush_12;
                solidBrush_7 = MlpRhYjTejC;
                solidBrush_8 = solidBrush_19;
                solidBrush_9 = solidBrush_20;
            }
        }

        private System.Drawing.Point[] method_47(int int_11)
        {
            List<System.Drawing.Point> list = new List<System.Drawing.Point>();
            int num = 24;
            int i = int_11 * -1;
            int num2 = 0;
            for (; i < int_11; i += num)
            {
                System.Drawing.Point item = new System.Drawing.Point(-1, i);
                System.Drawing.Point item2 = new System.Drawing.Point(int_11, i + int_11);
                if (num2 % 2 == 1)
                {
                    list.Add(item2);
                    list.Add(item);
                }
                else
                {
                    list.Add(item);
                    list.Add(item2);
                }
                num2++;
            }
            return list.ToArray();
        }

        private System.Drawing.Point[] yItRuxAmaVX(int int_11)
        {
            List<System.Drawing.Point> list = new List<System.Drawing.Point>();
            int_11 *= 2;
            int num = 16;
            int i = 16;
            int num2 = 0;
            for (; i < int_11; i += num)
            {
                System.Drawing.Point item = new System.Drawing.Point(i, -1);
                System.Drawing.Point item2 = new System.Drawing.Point(-1, i);
                if (num2 % 2 == 1)
                {
                    list.Add(item);
                    list.Add(item2);
                }
                else
                {
                    list.Add(item2);
                    list.Add(item);
                }
                num2++;
            }
            return list.ToArray();
        }

        private void method_48(Graphics graphics_0, GraphicsPath graphicsPath_3, GraphicsPath graphicsPath_4)
        {
            graphics_0.SmoothingMode = SmoothingMode.HighSpeed;
            if (graphicsPath_3.PointCount > 0)
            {
                graphics_0.SetClip(graphicsPath_3);
            }
            if (graphicsPath_4.PointCount > 0)
            {
                graphics_0.ExcludeClip(new Region(graphicsPath_4));
            }
            graphics_0.DrawLines(pen_0, point_0);
            graphics_0.SetClip(base.ClientRectangle);
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
        }

        private void method_49(Graphics graphics_0, GraphicsPath graphicsPath_3)
        {
            graphics_0.SmoothingMode = SmoothingMode.HighSpeed;
            if (graphicsPath_3.PointCount > 0)
            {
                graphics_0.SetClip(graphicsPath_3, CombineMode.Exclude);
            }
            graphics_0.DrawLines(pen_1, point_1);
            graphics_0.SetClip(base.ClientRectangle);
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
        }

        private Bitmap method_50(Habitat habitat_1, Bitmap bitmap_7)
        {
            if (habitat_1.Type == HabitatType.Volcanic)
            {
                int num = (int)((double)bitmap_7.Width / 1.025);
                int num2 = (int)(((double)bitmap_7.Width - (double)num) / 2.0);
                int num3 = (int)((double)bitmap_7.Height / 1.025);
                int num4 = (int)(((double)bitmap_7.Height - (double)num3) / 2.0);
                Bitmap bitmap = null;
                int num5 = habitat_1.PictureRef - GalaxyImages.HabitatImageOffsetVolcanic;
                if (num5 >= 0 && num5 < main_0.bitmap_195.Length)
                {
                    bitmap = main_0.bitmap_195[habitat_1.PictureRef - GalaxyImages.HabitatImageOffsetVolcanic];
                }
                if (bitmap != null)
                {
                    Bitmap bitmap2 = null;
                    bitmap2 = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(bitmap, num, num3, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(bitmap, num, num3));
                    bitmap2.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
                    Bitmap bitmap3 = bitmap2;
                    bitmap2 = method_224(bitmap2, System.Drawing.Color.FromArgb(255, 64, 0), bool_13: true);
                    bitmap3.Dispose();
                    bitmap2.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
                    using (Graphics graphics = Graphics.FromImage(bitmap_7))
                    {
                        if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                        {
                            method_176(graphics);
                        }
                        else
                        {
                            method_175(graphics);
                        }
                        float num6 = num2;
                        if (bitmap_7.Width != bitmap2.Width)
                        {
                            num6 = (float)(bitmap_7.Width - bitmap2.Width) / 2f;
                        }
                        float num7 = num4;
                        if (bitmap_7.Width != bitmap2.Width)
                        {
                            num7 = (float)(bitmap_7.Height - bitmap2.Height) / 2f;
                        }
                        graphics.DrawImage(bitmap2, new PointF(num6, num7));
                    }
                    bitmap2.Dispose();
                }
            }
            return bitmap_7;
        }

        private Bitmap method_51(Habitat habitat_1, Bitmap bitmap_7)
        {
            try
            {
                if (habitat_1.Category == HabitatCategoryType.Asteroid)
                {
                    return bitmap_7;
                }
                if (habitat_1.PictureRef >= GalaxyImages.HabitatImageOffsetOTHER)
                {
                    return bitmap_7;
                }
                if (habitat_1.Type == HabitatType.Continental || habitat_1.Type == HabitatType.MarshySwamp || habitat_1.Type == HabitatType.Ocean || habitat_1.Type == HabitatType.Desert || habitat_1.Type == HabitatType.Ice)
                {
                    Random random = new Random(habitat_1.HabitatIndex);
                    int num = 0;
                    if ((habitat_1.Type == HabitatType.Desert || habitat_1.Type == HabitatType.Ice || habitat_1.Type == HabitatType.Volcanic) && random.Next(0, 4) == 1)
                    {
                        return bitmap_7;
                    }
                    int num2 = (int)((double)bitmap_7.Width / 1.025);
                    int num3 = (int)(((double)bitmap_7.Width - (double)num2) / 2.0);
                    int num4 = (int)((double)bitmap_7.Height / 1.025);
                    int num5 = (int)(((double)bitmap_7.Height - (double)num4) / 2.0);
                    Bitmap bitmap = null;
                    num2 = Math.Max(1, num2);
                    num4 = Math.Max(1, num4);
                    switch (habitat_1.Type)
                    {
                        case HabitatType.Volcanic:
                        case HabitatType.MarshySwamp:
                        case HabitatType.Continental:
                        case HabitatType.Ocean:
                            num = random.Next(0, 5);
                            bitmap = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.bitmap_192[num], num2, num4, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.bitmap_192[num], num2, num4));
                            break;
                        case HabitatType.Desert:
                        case HabitatType.Ice:
                            num = random.Next(0, 2);
                            bitmap = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.bitmap_193[num], num2, num4, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.bitmap_193[num], num2, num4));
                            break;
                    }
                    int num6 = 0;
                    Bitmap bitmap2 = bitmap;
                    bool flag = false;
                    switch (habitat_1.Type)
                    {
                        case HabitatType.Volcanic:
                            switch (random.Next(0, 4))
                            {
                                case 0:
                                    bitmap = method_224(bitmap, System.Drawing.Color.FromArgb(144, 0, 0), bool_13: true);
                                    flag = true;
                                    break;
                                case 1:
                                    bitmap = method_224(bitmap, System.Drawing.Color.FromArgb(160, 80, 32), bool_13: true);
                                    flag = true;
                                    break;
                                case 2:
                                    bitmap = method_224(bitmap, System.Drawing.Color.FromArgb(160, 160, 32), bool_13: true);
                                    flag = true;
                                    break;
                                case 3:
                                    bitmap = method_224(bitmap, System.Drawing.Color.FromArgb(96, 48, 24), bool_13: true);
                                    flag = true;
                                    break;
                            }
                            break;
                        case HabitatType.Desert:
                            num6 = random.Next(0, 2);
                            if (num6 == 1)
                            {
                                bitmap = method_224(bitmap, System.Drawing.Color.FromArgb(192, 64, 64), bool_13: true);
                                flag = true;
                            }
                            break;
                        case HabitatType.MarshySwamp:
                            num6 = random.Next(0, 2);
                            if (num6 == 1)
                            {
                                bitmap = method_224(bitmap, System.Drawing.Color.FromArgb(192, 255, 192), bool_13: true);
                                flag = true;
                            }
                            break;
                        case HabitatType.Ice:
                            switch (random.Next(0, 3))
                            {
                                case 1:
                                    bitmap = method_224(bitmap, System.Drawing.Color.FromArgb(160, 224, 255), bool_13: true);
                                    flag = true;
                                    break;
                                case 2:
                                    bitmap = method_224(bitmap, System.Drawing.Color.FromArgb(232, 176, 255), bool_13: true);
                                    flag = true;
                                    break;
                            }
                            break;
                    }
                    if (flag)
                    {
                        bitmap2.Dispose();
                    }
                    bitmap.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
                    int num7 = bitmap.Width;
                    int num8 = bitmap.Height;
                    float float_ = (float)(random.NextDouble() * Math.PI);
                    bitmap2 = bitmap;
                    bitmap = method_217(bitmap, float_);
                    bitmap2.Dispose();
                    bitmap.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
                    num3 -= (bitmap.Width - num7) / 2;
                    num5 -= (bitmap.Height - num8) / 2;
                    using (Graphics graphics = Graphics.FromImage(bitmap_7))
                    {
                        if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                        {
                            method_176(graphics);
                        }
                        else
                        {
                            method_175(graphics);
                        }
                        float num9 = num3;
                        if (bitmap_7.Width != bitmap.Width)
                        {
                            num9 = (float)(bitmap_7.Width - bitmap.Width) / 2f;
                        }
                        float num10 = num5;
                        if (bitmap_7.Width != bitmap.Width)
                        {
                            num10 = (float)(bitmap_7.Height - bitmap.Height) / 2f;
                        }
                        graphics.DrawImage(bitmap, new PointF(num9, num10));
                    }
                    bitmap.Dispose();
                    return bitmap_7;
                }
                return bitmap_7;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Bitmap method_52(Habitat habitat_1, Bitmap bitmap_7, out bool bool_13)
        {
            bool_13 = false;
            if (habitat_1.Damage == 0f)
            {
                return bitmap_7;
            }
            if (habitat_1.Category == HabitatCategoryType.Asteroid)
            {
                return bitmap_7;
            }
            if (habitat_1.Type == HabitatType.Continental || habitat_1.Type == HabitatType.MarshySwamp || habitat_1.Type == HabitatType.Ocean || habitat_1.Type == HabitatType.Desert || habitat_1.Type == HabitatType.Ice || habitat_1.Type == HabitatType.Volcanic)
            {
                int num = (int)((double)bitmap_7.Width / 1.025);
                int num2 = (int)((double)bitmap_7.Height / 1.025);
                int val = (int)((double)habitat_1.Damage * 8.0);
                val = Math.Min(7, Math.Max(val, 0));
                Bitmap bitmap = null;
                bitmap = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.bitmap_194[val], num, num2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.bitmap_194[val], num, num2));
                switch (habitat_1.HabitatIndex % 4)
                {
                    case 0:
                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                        break;
                    case 1:
                        bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 2:
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 3:
                        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                bitmap.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
                using (Graphics graphics = Graphics.FromImage(bitmap_7))
                {
                    if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                    {
                        method_176(graphics);
                    }
                    else
                    {
                        method_175(graphics);
                    }
                    System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
                    graphics.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
                }
                bitmap.Dispose();
            }
            return bitmap_7;
        }

        private Bitmap GetPlanetShadow(Habitat habitat_1, Bitmap bitmap_7)
        {
            int num = 0;
            num = (int)((double)bitmap_7.Width / 150.0);
            int num2 = bitmap_7.Width - num;
            int num3 = bitmap_7.Height - num;
            Bitmap bitmap = null;
            bitmap = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.bitmap_191, num2, num3, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.bitmap_191, num2, num3, InterpolationMode.Low, CompositingQuality.HighSpeed, SmoothingMode.None));
            Habitat systemStar = galaxy_0.Systems[habitat_1.SystemIndex].SystemStar;
            float num4 = (float)Galaxy.DetermineAngle(habitat_1.Xpos, habitat_1.Ypos, systemStar.Xpos, systemStar.Ypos);
            num4 *= -1f;
            Bitmap bitmap2 = bitmap;
            bitmap = method_217(bitmap, num4);
            bitmap2.Dispose();
            using (Graphics graphics = Graphics.FromImage(bitmap_7))
            {
                if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                {
                    method_176(graphics);
                }
                else
                {
                    method_175(graphics);
                }
                float num5 = 0f;
                if (bitmap_7.Width != bitmap.Width)
                {
                    num5 = ((float)bitmap_7.Width - (float)bitmap.Width) / 2f;
                }
                float num6 = 0f;
                if (bitmap_7.Width != bitmap.Width)
                {
                    num6 = ((float)bitmap_7.Height - (float)bitmap.Height) / 2f;
                }
                graphics.DrawImage(bitmap, new PointF(num5, num6));
            }
            bitmap.Dispose();
            return bitmap_7;
        }

        internal Bitmap method_54(Habitat habitat_1, int int_11)
        {
            Bitmap bitmap_ = null;
            if (habitat_1 != null)
            {
                if (habitat_1.HasRings)
                {
                    bitmap_ = GetRescaledGasGiantRigns(habitat_1, int_11);
                }
                Bitmap bitmap = null;
                if (habitat_1.Type == HabitatType.SuperNova && main_0 != null && main_0.bitmap_206 != null)
                {
                    bitmap = main_0.bitmap_206[habitat_1.NovaImageIndexMajor];
                }
                else if (habitat_1.Category == HabitatCategoryType.Star && main_0 != null && main_0.bitmap_196 != null)
                {
                    bitmap = main_0.bitmap_196[habitat_1.MapPictureRef];
                }
                else if (main_0 != null && main_0.habitatImageCache_0 != null)
                {
                    bitmap = main_0.habitatImageCache_0.ObtainImage(habitat_1);
                }
                if (habitat_1.Category == HabitatCategoryType.GasCloud)
                {
                    bitmap = method_135(habitat_1);
                }
                if (bitmap != null && bitmap.PixelFormat != 0 && main_0 != null)
                {
                    double num = (double)bitmap.Height / (double)bitmap.Width;
                    int num2 = (int)((double)int_11 * num);
                    Bitmap bitmap2 = main_0.PrecacheScaledBitmap(bitmap, int_11, num2);
                    bool bool_ = false;
                    Bitmap bitmap3 = method_52(habitat_1, bitmap2, out bool_);
                    if (bool_)
                    {
                        bitmap2.Dispose();
                    }
                    bitmap3 = method_51(habitat_1, bitmap2);
                    bitmap3 = method_50(habitat_1, bitmap3);
                    Bitmap result = CombinePlaneWithRings(habitat_1, bitmap3, bitmap_);
                    bitmap3.Dispose();
                    return result;
                }
            }
            return new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
        }

        internal Bitmap GetRescaledGasGiantRigns(Habitat habitat_1, int int_11)
        {
            if (habitat_1 != null && main_0 != null && main_0.bitmap_1 != null)
            {
                int num = habitat_1.HabitatIndex % 10;
                Bitmap bitmap = main_0.bitmap_1[num];
                double num2 = (double)int_11 / (double)habitat_1.Diameter;
                double num3 = (double)habitat_1.Diameter / 500.00000000000006;
                num2 *= num3;
                int resultWidth = (int)((double)bitmap.Width * num2);
                int resultHeight = (int)((double)bitmap.Height * num2);
                if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                {
                    return main_0.PrecacheScaledBitmap(bitmap, resultWidth, resultHeight, InterpolationMode.HighQualityBilinear, CompositingQuality.HighSpeed, SmoothingMode.HighSpeed);
                }
                return main_0.PrecacheScaledBitmap(bitmap, resultWidth, resultHeight, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None);
            }
            return null;
        }

        internal Bitmap CombinePlaneWithRings(Habitat habitat_1, Bitmap bitmap_7, Bitmap bitmap_8)
        {
            if (bitmap_7 != null && bitmap_7.PixelFormat != 0)
            {
                int val = bitmap_7.Width;
                int val2 = bitmap_7.Height;
                if (bitmap_8 != null && bitmap_8.PixelFormat != 0)
                {
                    val = Math.Max(bitmap_8.Width, bitmap_7.Width);
                    val2 = Math.Max(bitmap_8.Height, bitmap_7.Height);
                }
                val = Math.Max(1, Math.Min(val, 2000));
                val2 = Math.Max(1, Math.Min(val2, 2000));
                int num = (val - bitmap_7.Width) / 2;
                int num2 = (val2 - bitmap_7.Height) / 2;
                val = Math.Max(1, val);
                val2 = Math.Max(1, val2);
                Bitmap bitmap = new Bitmap(val, val2, PixelFormat.Format32bppPArgb);
                bitmap.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
                using Graphics graphics = Graphics.FromImage(bitmap);
                if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                }
                else
                {
                    method_175(graphics);
                }
                int num3 = 0;
                int num4 = 0;
                if (bitmap_8 != null && bitmap_8.PixelFormat != 0)
                {
                    int num5 = num - (bitmap_8.Width - bitmap_7.Width) / 2;
                    int num6 = num2 - (bitmap_8.Height - bitmap_7.Height) / 2;
                    num3 = bitmap_8.Height / 2;
                    num4 = bitmap_8.Height - num3;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(num5, num6, bitmap_8.Width, bitmap_8.Height - num3);
                    System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap_8.Width, bitmap_8.Height - num3);
                    graphics.DrawImage(bitmap_8, destRect, srcRect, GraphicsUnit.Pixel);
                }
                graphics.DrawImageUnscaled(bitmap_7, num, num2);
                if (habitat_1.Ruin != null)
                {
                    int num7 = Math.Max(main_0.bitmap_2[habitat_1.Ruin.PictureRef].Width, main_0.bitmap_2[habitat_1.Ruin.PictureRef].Height);
                    double num8 = main_0.double_0 * ((double)num7 / 45.0);
                    int num9 = (int)((double)main_0.bitmap_2[habitat_1.Ruin.PictureRef].Width / num8);
                    int num10 = (int)((double)main_0.bitmap_2[habitat_1.Ruin.PictureRef].Height / num8);
                    int num11 = num + bitmap_7.Width / 2 + (int)(habitat_1.Ruin.ParentX / num8) - num9 / 2;
                    int num12 = num2 + bitmap_7.Height / 2 + (int)(habitat_1.Ruin.ParentY / num8) - num10 / 2;
                    graphics.DrawImage(rect: new System.Drawing.Rectangle(num11, num12, num9, num10), image: main_0.bitmap_2[habitat_1.Ruin.PictureRef]);
                }
                if (bitmap_8 != null)
                {
                    if (bitmap_8.PixelFormat != 0)
                    {
                        int num13 = num - (bitmap_8.Width - bitmap_7.Width) / 2;
                        int num14 = num2 - (bitmap_8.Height - bitmap_7.Height) / 2;
                        System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle(num13, num14 + num4, bitmap_8.Width, num4);
                        System.Drawing.Rectangle srcRect2 = new System.Drawing.Rectangle(0, bitmap_8.Height - num3, bitmap_8.Width, bitmap_8.Height - num3);
                        graphics.DrawImage(bitmap_8, destRect2, srcRect2, GraphicsUnit.Pixel);
                        return bitmap;
                    }
                    return bitmap;
                }
                return bitmap;
            }
            return new Bitmap(2, 2, PixelFormat.Format32bppPArgb);
        }

        public Bitmap GetBitmapAtIndexSafely(List<Bitmap> bitmaps, int index)
        {
            try
            {
                if (bitmaps != null && index >= 0 && index < bitmaps.Count)
                {
                    return bitmaps[index];
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        internal Bitmap method_57(int int_11)
        {
            int num = int_11 + main_0.int_28;
            if (galaxy_0.Habitats.Count > num && galaxy_0.Habitats[num].Category == HabitatCategoryType.GasCloud)
            {
                return bitmap_2;
            }
            if (int_11 >= 0 && int_11 < list_5.Count)
            {
                return GetBitmapAtIndexSafely(list_5, int_11);
            }
            return null;
        }

        internal Texture2D method_58(int int_11)
        {
            int num = int_11 + main_0.int_28;
            if (galaxy_0.Habitats.Count > num && galaxy_0.Habitats[num].Category == HabitatCategoryType.GasCloud && bitmap_2 != null && bitmap_2.PixelFormat != 0)
            {
                return XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_2);
            }
            if (int_11 >= 0 && int_11 < list_6.Count)
            {
                return list_6[int_11];
            }
            return null;
        }

        private float lVxRufUsger(Texture2D texture2D_61, int int_11, double double_15)
        {
            float result = 1f;
            if (texture2D_61 != null)
            {
                result = (float)int_11 / (float)texture2D_61.Height / (float)double_15;
            }
            return result;
        }

        private void method_59(Habitat habitat_1, double double_15, int int_11, out int int_12, out int int_13)
        {
            int_12 = 1;
            int_13 = 1;
            Bitmap bitmap = main_0.habitatImageCache_0.ObtainImageSmall(habitat_1);
            if (bitmap != null && bitmap.PixelFormat != 0)
            {
                double num = (double)bitmap.Width / (double)bitmap.Height;
                if (bitmap.Width > bitmap.Height)
                {
                    int_12 = (int)((double)habitat_1.Diameter * num);
                    int_13 = habitat_1.Diameter;
                }
                else
                {
                    int_12 = habitat_1.Diameter;
                    int_13 = (int)((double)habitat_1.Diameter / num);
                }
                System.Drawing.Rectangle rectangle = method_35((int)habitat_1.Xpos, (int)habitat_1.Ypos, int_12, int_13, double_15, int_11);
                int_12 = rectangle.Width;
                int_13 = rectangle.Height;
            }
        }

        private double method_60(HabitatType habitatType_0)
        {
            double double_ = 30.0;
            return method_61(habitatType_0, out double_);
        }

        private double method_61(HabitatType habitatType_0, out double double_15)
        {
            double result = 100.0;
            double_15 = 20.0;
            switch (habitatType_0)
            {
                case HabitatType.MainSequence:
                    result = 60.0;
                    double_15 = 25.0;
                    break;
                case HabitatType.RedGiant:
                case HabitatType.SuperGiant:
                    result = 75.0;
                    double_15 = 30.0;
                    break;
                case HabitatType.WhiteDwarf:
                    result = 40.0;
                    double_15 = 9.0;
                    break;
                case HabitatType.Neutron:
                    result = 30.0;
                    double_15 = 6.0;
                    break;
                case HabitatType.BlackHole:
                    result = 150.0;
                    double_15 = 30.0;
                    break;
                case HabitatType.SuperNova:
                    result = 150.0;
                    double_15 = 30.0;
                    break;
            }
            return result;
        }

        private System.Drawing.Color method_62(Habitat habitat_1)
        {
            System.Drawing.Color color_ = method_120(main_0.bitmap_196[habitat_1.MapPictureRef]);
            color_ = method_63(color_, habitat_1.Type);
            return method_226(color_, 48);
        }

        private System.Drawing.Color method_63(System.Drawing.Color color_18, HabitatType habitatType_0)
        {
            System.Drawing.Color empty = System.Drawing.Color.Empty;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            switch (habitatType_0)
            {
                case HabitatType.MainSequence:
                    num = 32;
                    num2 = -32;
                    num3 = -16;
                    break;
                case HabitatType.RedGiant:
                case HabitatType.SuperGiant:
                    num = 32;
                    num2 = -16;
                    num3 = -16;
                    break;
                case HabitatType.WhiteDwarf:
                    num = -48;
                    num2 = -48;
                    num3 = 32;
                    break;
                case HabitatType.Neutron:
                    num = 0;
                    num2 = -8;
                    num3 = 24;
                    break;
            }
            return System.Drawing.Color.FromArgb(Math.Max(0, Math.Min(255, color_18.R + num)), Math.Max(0, Math.Min(255, color_18.G + num2)), Math.Max(0, Math.Min(255, color_18.B + num3)));
        }

        private System.Drawing.Color method_64(System.Drawing.Color color_18, double double_15)
        {
            int val = color_18.R + (int)((double)(255 - color_18.R) * double_15);
            int val2 = color_18.G + (int)((double)(255 - color_18.G) * double_15);
            int val3 = color_18.B + (int)((double)(255 - color_18.B) * double_15);
            val = Math.Max(0, Math.Min(255, val));
            val2 = Math.Max(0, Math.Min(255, val2));
            val3 = Math.Max(0, Math.Min(255, val3));
            return System.Drawing.Color.FromArgb(val, val2, val3);
        }

        private System.Drawing.Color method_65(System.Drawing.Color color_18, double double_15)
        {
            int val = (int)((double)(int)color_18.R * double_15);
            int val2 = (int)((double)(int)color_18.G * double_15);
            int val3 = (int)((double)(int)color_18.B * double_15);
            val = Math.Max(0, Math.Min(255, val));
            val2 = Math.Max(0, Math.Min(255, val2));
            val3 = Math.Max(0, Math.Min(255, val3));
            return System.Drawing.Color.FromArgb(val, val2, val3);
        }

        private void method_66(Habitat habitat_1)
        {
            if (int_7 == habitat_1.MapPictureRef)
            {
                return;
            }
            System.Drawing.Color empty = System.Drawing.Color.Empty;
            System.Drawing.Color empty2 = System.Drawing.Color.Empty;
            System.Drawing.Color color_ = method_120(main_0.bitmap_196[habitat_1.MapPictureRef]);
            color_ = method_63(color_, habitat_1.Type);
            empty = method_65(color_, 0.5);
            empty2 = method_65(empty, 1.15);
            method_65(empty, 0.85);
            Bitmap bitmap = main_0.bitmap_204[1];
            switch (habitat_1.Type)
            {
                default:
                    bitmap = main_0.bitmap_204[1];
                    break;
                case HabitatType.WhiteDwarf:
                case HabitatType.Neutron:
                    bitmap = main_0.bitmap_204[2];
                    break;
            }
            if (habitat_1.Diameter > int_8)
            {
                bitmap_5[0] = method_222(main_0.bitmap_204[0], empty);
                Bitmap bitmap2 = method_222(bitmap, empty2);
                Bitmap bitmap3 = new Bitmap(bitmap2.Width, bitmap2.Height, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage(bitmap3))
                {
                    method_177(graphics);
                    graphics.DrawImage(srcRect: new System.Drawing.Rectangle(0, 0, bitmap_5[0].Width, bitmap_5[0].Height), destRect: new System.Drawing.Rectangle(0, 0, bitmap3.Width, bitmap3.Height), image: bitmap_5[0], srcUnit: GraphicsUnit.Pixel);
                    System.Drawing.Rectangle srcRect2 = new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
                    System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle(2, 2, bitmap3.Width - 4, bitmap3.Height - 4);
                    graphics.DrawImage(bitmap2, destRect2, srcRect2, GraphicsUnit.Pixel);
                }
                bitmap_5[0] = bitmap3;
            }
            else
            {
                bitmap_5[0] = method_222(main_0.bitmap_204[0], empty);
                bitmap_5[1] = method_222(bitmap, empty2);
            }
            int_7 = habitat_1.MapPictureRef;
        }

        private void method_67(BuiltObject builtObject_1)
        {
            int num = list_14.IndexOf(builtObject_1.BuiltObjectID);
            if (num >= 0)
            {
                list_9.RemoveAt(num);
                list_10.RemoveAt(num);
                list_11.RemoveAt(num);
                list_12.RemoveAt(num);
                list_13.RemoveAt(num);
                list_14.RemoveAt(num);
                list_15.RemoveAt(num);
            }
        }

        private bool method_68(Fighter fighter_0)
        {
            if (fighter_0.OverlayChanged)
            {
                return true;
            }
            return false;
        }

        private bool method_69(Fighter fighter_0)
        {
            if (!fighter_0.HeadingChanged && !fighter_0.OverlayChanged)
            {
                return false;
            }
            return true;
        }

        private Bitmap method_70(Fighter fighter_0, Bitmap bitmap_7, bool bool_13)
        {
            GraphicsQuality graphicsQuality_ = GraphicsQuality.Medium;
            if (bool_13 && fighter_0.Heading != fighter_0.TargetHeading)
            {
                graphicsQuality_ = GraphicsQuality.Low;
            }
            Bitmap bitmap = new Bitmap(bitmap_7);
            if (fighter_0.Health < 1f && !fighter_0.UnderConstruction)
            {
                Bitmap bitmap_8 = bitmap;
                bitmap = main_0.method_107(fighter_0, bitmap, main_0.bitmap_7[fighter_0.PictureRef]);
                method_21(bitmap_8);
            }
            fighter_0.OverlayChanged = false;
            if (bool_13)
            {
                bitmap = method_218(bitmap, fighter_0.Heading * -1f, graphicsQuality_);
            }
            fighter_0.HeadingChanged = false;
            return bitmap;
        }

        private bool method_71(BuiltObject builtObject_1)
        {
            if (!builtObject_1.LightChanged && !builtObject_1.OverlayChanged)
            {
                return false;
            }
            return true;
        }

        private bool method_72(BuiltObject builtObject_1)
        {
            if (!builtObject_1.HeadingChanged && !builtObject_1.LightChanged && !builtObject_1.OverlayChanged)
            {
                return false;
            }
            return true;
        }

        private Bitmap method_73(BuiltObject builtObject_1, Bitmap bitmap_7, Size size_0, Bitmap bitmap_8, List<System.Drawing.Point> lightPoints, bool bool_13)
        {
            GraphicsQuality graphicsQuality_ = GraphicsQuality.Medium;
            if (bool_13 && builtObject_1.Heading != builtObject_1.TargetHeading)
            {
                graphicsQuality_ = GraphicsQuality.Low;
            }
            Bitmap bitmap = new Bitmap(bitmap_7);
            if (builtObject_1.DamagedComponentCount > 0)
            {
                Bitmap bitmap_9 = bitmap;
                bitmap = main_0.method_106(builtObject_1, bitmap, bitmap_8);
                method_21(bitmap_9);
            }
            if (builtObject_1.UnbuiltComponentCount > 0)
            {
                Bitmap bitmap_10 = bitmap;
                bitmap = main_0.method_115(builtObject_1, bitmap);
                method_21(bitmap_10);
            }
            builtObject_1.OverlayChanged = false;
            if (builtObject_1.BuiltAt == null && builtObject_1.Empire != null)
            {
                Bitmap bitmap_11 = main_0.method_111(builtObject_1);
                bool bool_14 = false;
                Bitmap bitmap_12 = bitmap;
                bitmap = main_0.vBqtbUygo3(builtObject_1, bitmap, size_0, bitmap_11, lightPoints, graphicsQuality_, out bool_14);
                if (bool_14)
                {
                    method_21(bitmap_12);
                }
            }
            builtObject_1.LightChanged = false;
            if (bool_13)
            {
                bitmap = method_218(bitmap, builtObject_1.Heading * -1f, graphicsQuality_);
            }
            builtObject_1.HeadingChanged = false;
            return bitmap;
        }

        private void method_74(Graphics graphics_0)
        {
            int num = main_0.int_13 + main_0.int_21;
            int num2 = main_0.int_14 + main_0.vhadzRiecM;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
            graphics_0.InterpolationMode = InterpolationMode.High;
            if (main_0._Game == null || main_0._Game.Galaxy == null)
            {
                return;
            }
            Empire empire = main_0._Game.PlayerEmpire;
            if (main_0.empire_1 != null)
            {
                empire = main_0.empire_1;
            }
            DateTime currentDateTime = main_0._Game.Galaxy.CurrentDateTime;
            long currentStarDate = main_0._Game.Galaxy.CurrentStarDate;
            list_4.Clear();
            list_3.Clear();
            int num3 = (int)((double)base.ClientRectangle.Width * main_0.double_0);
            num3 += Galaxy.MaxSolarSystemSize * 2;
            BuiltObjectList builtObjectsAtLocation = galaxy_0.GetBuiltObjectsAtLocation(num, num2, num3);
            FighterList fightersForBuiltObjects = galaxy_0.GetFightersForBuiltObjects(builtObjectsAtLocation);
            Habitat habitat = null;
            if (main_0.int_28 >= 0)
            {
                habitat = galaxy_0.Habitats[main_0.int_28];
            }
            if (main_0.double_0 < 150.0)
            {
                GalaxyLocationList galaxyLocationList = galaxy_0.DetermineGalaxyLocationsInRangeAtPoint(num, num2, double_9 * main_0.double_0, GalaxyLocationType.NebulaCloud);
                GalaxyLocationList galaxyLocationList2 = galaxy_0.DetermineGalaxyLocationsInRangeAtPoint(num, num2, double_9 * main_0.double_0, GalaxyLocationType.SuperNova);
                bool flag = false;
                if (galaxyLocationList.Count > 0)
                {
                    for (int i = 0; i < galaxyLocationList.Count; i++)
                    {
                        GalaxyLocation galaxyLocation = galaxyLocationList[i];
                        if (galaxyLocation.Effect == GalaxyLocationEffectType.LightningDamage)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (galaxyLocationList.Count <= 0 && galaxyLocationList2.Count <= 0 && !flag)
                {
                    color_15 = System.Drawing.Color.Empty;
                }
                else
                {
                    if (galaxyLocationList2.Count > 0)
                    {
                        color_15 = method_29();
                    }
                    else if (galaxyLocationList.Count > 0)
                    {
                        if (!bool_7)
                        {
                            color_15 = method_122(num, num2);
                        }
                        else
                        {
                            color_15 = System.Drawing.Color.Empty;
                        }
                    }
                    if (flag)
                    {
                        System.Drawing.Color color = method_28(currentDateTime);
                        if (!color.IsEmpty)
                        {
                            color_15 = color;
                        }
                    }
                    if (color_15 != System.Drawing.Color.Empty)
                    {
                        double num4 = 1.0;
                        num4 = ((!(main_0.double_0 < 75.0)) ? ((double)(int)color_15.A / 255.0 * ((150.0 - main_0.double_0) / 167.0 + 0.35)) : ((double)(int)color_15.A / 255.0 * ((main_0.double_0 + 30.0) / 167.0 + 0.35)));
                        int red = (int)((double)(int)color_15.R * num4);
                        int green = (int)((double)(int)color_15.G * num4);
                        int blue = (int)((double)(int)color_15.B * num4);
                        color_15 = System.Drawing.Color.FromArgb(255, red, green, blue);
                    }
                }
                if (color_15 != System.Drawing.Color.Empty)
                {
                    method_175(graphics_0);
                    using (SolidBrush brush = new SolidBrush(color_15))
                    {
                        graphics_0.FillRectangle(brush, base.ClientRectangle);
                    }
                    method_177(graphics_0);
                }
            }
            if (habitat == null)
            {
                habitat = galaxy_0.FastFindNearestSystem(num, num2);
            }
            SystemInfo systemInfo = galaxy_0.Systems[habitat.SystemIndex];
            Empire empire2 = null;
            if (systemInfo.DominantEmpire != null)
            {
                empire2 = systemInfo.DominantEmpire.Empire;
            }
            bool flag2 = false;
            double num5 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, num, num2);
            double num6 = num5 + double_9 * main_0.double_0;
            if ((int)num6 > Galaxy.MaxSolarSystemSize + 500)
            {
                flag2 = true;
            }
            if (flag2)
            {
                BuiltObject builtObject = galaxy_0.FastFindNearestLongRangeScanner(num, num2, empire);
                if (builtObject != null)
                {
                    double num7 = galaxy_0.CalculateDistance(builtObject.Xpos, builtObject.Ypos, num, num2);
                    num6 = num7 + double_9 * main_0.double_0;
                    if ((int)num6 < builtObject.SensorLongRange)
                    {
                        flag2 = false;
                    }
                }
            }
            bool flag3 = false;
            SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Undefined;
            if (habitat != null)
            {
                systemVisibilityStatus = empire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                System.Drawing.Rectangle rectangle;
                if (main_0._Game.GodMode)
                {
                    systemVisibilityStatus = SystemVisibilityStatus.Visible;
                    rectangle = default(System.Drawing.Rectangle);
                    //SystemVisibilityStatus systemVisibilityStatus2 = SystemVisibilityStatus.Visible;
                    goto IL_05d0;
                }
                rectangle = default(System.Drawing.Rectangle);
                switch (systemVisibilityStatus)
                {
                    case SystemVisibilityStatus.Explored:
                        break;
                    case SystemVisibilityStatus.Visible:
                        goto IL_05d0;
                    default:
                        goto IL_0622;
                }
                flag3 = true;
                if (flag2)
                {
                    rectangle = method_34((int)habitat.Xpos, (int)habitat.Ypos, Galaxy.MaxSolarSystemSize * 2 + 1000, Galaxy.MaxSolarSystemSize * 2 + 1000, main_0.double_0);
                    graphicsPath_2.AddEllipse(rectangle);
                    graphicsPath_0.AddEllipse(rectangle);
                }
                else
                {
                    rectangle = new System.Drawing.Rectangle(0, 0, base.Width, base.Height);
                    graphicsPath_0.AddRectangle(rectangle);
                }
            }
            goto IL_0622;
        IL_05d0:
            if (flag2)
            {
                System.Drawing.Rectangle rectangle = method_34((int)habitat.Xpos, (int)habitat.Ypos, Galaxy.MaxSolarSystemSize * 2 + 1000, Galaxy.MaxSolarSystemSize * 2 + 1000, main_0.double_0);
                graphicsPath_2.AddEllipse(rectangle);
            }
            goto IL_0622;
        IL_0622:
            bool flag4 = false;
            for (int j = 0; j < builtObjectsAtLocation.Count; j++)
            {
                BuiltObject builtObject2 = builtObjectsAtLocation[j];
                if (builtObject2 == null || builtObject2.Empire != empire)
                {
                    continue;
                }
                int num8 = Galaxy.ThreatRange;
                if (builtObject2.SensorProximityArrayRange > num8)
                {
                    num8 = builtObject2.SensorProximityArrayRange;
                }
                if (builtObject2.SensorLongRange > num8)
                {
                    num8 = builtObject2.SensorLongRange;
                }
                if (habitat != null)
                {
                    double num9 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                    if ((int)num9 - num8 > Galaxy.MaxSolarSystemSize + 500)
                    {
                        System.Drawing.Rectangle rectangle2 = method_34((int)builtObject2.Xpos, (int)builtObject2.Ypos, num8 * 2, num8 * 2, main_0.double_0);
                        if (rectangle2.Width > 8 && method_151(base.ClientRectangle, rectangle2))
                        {
                            graphicsPath_2.AddEllipse(rectangle2);
                            graphicsPath_1.AddEllipse(rectangle2);
                        }
                    }
                    if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored && (int)num9 - num8 <= Galaxy.MaxSolarSystemSize + 500)
                    {
                        flag4 = true;
                    }
                    continue;
                }
                System.Drawing.Rectangle rectangle3 = method_34((int)builtObject2.Xpos, (int)builtObject2.Ypos, num8 * 2, num8 * 2, main_0.double_0);
                if (rectangle3.Width > 8 && method_151(base.ClientRectangle, rectangle3))
                {
                    graphicsPath_2.AddEllipse(rectangle3);
                    graphicsPath_1.AddEllipse(rectangle3);
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                {
                    double num10 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                    if ((int)num10 - num8 <= Galaxy.MaxSolarSystemSize + 500)
                    {
                        flag4 = true;
                    }
                }
            }
            for (int k = 0; k < empire.LongRangeScanners.Count; k++)
            {
                int sensorLongRange = empire.LongRangeScanners[k].SensorLongRange;
                if (habitat != null)
                {
                    double num11 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, empire.LongRangeScanners[k].Xpos, empire.LongRangeScanners[k].Ypos);
                    if ((int)num11 - sensorLongRange <= Galaxy.MaxSolarSystemSize + 500 && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                    {
                        flag4 = true;
                        System.Drawing.Rectangle rectangle4 = method_34((int)empire.LongRangeScanners[k].Xpos, (int)empire.LongRangeScanners[k].Ypos, sensorLongRange * 2, sensorLongRange * 2, main_0.double_0);
                        if (rectangle4.Width > 8 && method_151(base.ClientRectangle, rectangle4))
                        {
                            flag3 = ((!rectangle4.Contains(base.ClientRectangle)) ? true : false);
                            graphicsPath_2.AddEllipse(rectangle4);
                            graphicsPath_1.AddEllipse(rectangle4);
                        }
                    }
                    continue;
                }
                System.Drawing.Rectangle rectangle5 = method_34((int)empire.LongRangeScanners[k].Xpos, (int)empire.LongRangeScanners[k].Ypos, sensorLongRange * 2, sensorLongRange * 2, main_0.double_0);
                if (rectangle5.Width > 8 && method_151(base.ClientRectangle, rectangle5))
                {
                    flag3 = ((!rectangle5.Contains(base.ClientRectangle)) ? true : false);
                    graphicsPath_2.AddEllipse(rectangle5);
                    graphicsPath_1.AddEllipse(rectangle5);
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                {
                    double num12 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, empire.LongRangeScanners[k].Xpos, empire.LongRangeScanners[k].Ypos);
                    if ((int)num12 + sensorLongRange <= Galaxy.MaxSolarSystemSize + 500)
                    {
                        flag4 = true;
                    }
                }
            }
            int int_;
            int int_2;
            if (main_0.int_28 >= 0)
            {
                if (main_0.double_0 < 500.0)
                {
                    method_175(graphics_0);
                    for (int l = 0; l < builtObjectsAtLocation.Count; l++)
                    {
                        BuiltObject builtObject3 = builtObjectsAtLocation[l];
                        if (builtObject3 == null)
                        {
                            continue;
                        }
                        if (builtObject3.HyperDenyActive)
                        {
                            int num13 = (int)((builtObject3.Xpos - (double)num) / main_0.double_0) + base.Width / 2;
                            int num14 = (int)((builtObject3.Ypos - (double)num2) / main_0.double_0) + base.Height / 2;
                            if (num13 >= -1000 && num13 <= base.Width + 1000 && num14 >= -1000 && num14 <= base.Height + 1000 && (main_0._Game.GodMode || empire.IsObjectVisibleToThisEmpire(builtObject3)))
                            {
                                int num15 = (int)((double)builtObject3.WeaponHyperDenyRange / main_0.double_0);
                                graphics_0.FillEllipse(rect: new System.Drawing.Rectangle(num13 - num15, num14 - num15, num15 * 2, num15 * 2), brush: main_0.hatchBrush_1);
                            }
                        }
                        else
                        {
                            if (builtObject3.HyperStopRange <= 0 || !(main_0.double_0 < 50.0))
                            {
                                continue;
                            }
                            int num16 = (int)((builtObject3.Xpos - (double)num) / main_0.double_0) + base.Width / 2;
                            int num17 = (int)((builtObject3.Ypos - (double)num2) / main_0.double_0) + base.Height / 2;
                            if (num16 >= -3000 && num16 <= base.Width + 3000 && num17 >= -3000 && num17 <= base.Height + 3000 && (main_0._Game.GodMode || empire.IsObjectVisibleToThisEmpire(builtObject3)))
                            {
                                int num18 = (int)((double)builtObject3.HyperStopRange / main_0.double_0);
                                System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(num16 - num18, num17 - num18, num18 * 2, num18 * 2);
                                int val = 255 - (int)(230.0 * (main_0.double_0 / 50.0));
                                val = Math.Min(255, Math.Max(0, val));
                                System.Drawing.Color color_ = System.Drawing.Color.FromArgb(val, 255, 32, 64);
                                System.Drawing.Color color_2 = System.Drawing.Color.FromArgb(val, 64, 32, 192);
                                System.Drawing.Color color2 = method_214(color_, color_2, currentDateTime);
                                float num19 = 1f;
                                if (main_0.double_0 < 1.2)
                                {
                                    num19 = 4f;
                                }
                                else if (main_0.double_0 < 3.0)
                                {
                                    num19 = 3f;
                                }
                                else if (main_0.double_0 < 9.0)
                                {
                                    num19 = 2f;
                                }
                                using Pen pen = new Pen(color2, num19);
                                pen.DashPattern = new float[2]
                                {
                                1f,
                                20f / (float)main_0.double_0
                                };
                                graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
                                graphics_0.DrawEllipse(pen, rect2);
                                graphics_0.SmoothingMode = SmoothingMode.None;
                            }
                        }
                    }
                    GalaxyLocationList galaxyLocationList3 = galaxy_0.DetermineGalaxyLocationsInRangeAtPoint(num, num2, double_9 * main_0.double_0, GalaxyLocationType.RestrictedArea);
                    for (int m = 0; m < galaxyLocationList3.Count; m++)
                    {
                        int num20 = (int)((double)galaxyLocationList3[m].Width / 2.0 / main_0.double_0);
                        int num21 = (int)((double)galaxyLocationList3[m].Width / 2.0);
                        int num22 = num21 * -1;
                        int num23 = (int)(((double)(galaxyLocationList3[m].Xpos + (float)num21) - (double)num) / main_0.double_0) + base.Width / 2;
                        int num24 = (int)(((double)(galaxyLocationList3[m].Ypos + (float)num21) - (double)num2) / main_0.double_0) + base.Height / 2;
                        if (num23 >= num22 && num23 <= base.Width + num21 && num24 >= num22 && num24 <= base.Height + num21)
                        {
                            graphics_0.FillEllipse(rect: new System.Drawing.Rectangle(num23 - num20, num24 - num20, num20 * 2, num20 * 2), brush: main_0.hatchBrush_2);
                        }
                    }
                    method_177(graphics_0);
                }
                if (main_0.double_0 < 300.0)
                {
                    method_46();
                    method_175(graphics_0);
                    method_104(solidBrush_9, starFieldItemList_3, rectangle_4, this.int_4, 38, graphics_0);
                    method_104(solidBrush_8, starFieldItemList_2, rectangle_3, this.int_3, 20, graphics_0);
                    method_104(solidBrush_7, starFieldItemList_1, this.rectangle_2, this.int_2, 11, graphics_0);
                    method_177(graphics_0);
                    method_103(solidBrush_6, starFieldItemList_0, rectangle_1, int_1, 6, graphics_0);
                }
                if (systemVisibilityStatus != SystemVisibilityStatus.Explored && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                {
                    if (flag4 && main_0.double_0 >= 5.0)
                    {
                        int num25 = (int)((galaxy_0.Habitats[main_0.int_28].Xpos - (double)num) / main_0.double_0) + base.ClientRectangle.Width / 2;
                        int num26 = (int)((galaxy_0.Habitats[main_0.int_28].Ypos - (double)num2) / main_0.double_0) + base.ClientRectangle.Height / 2;
                        for (int n = main_0.int_28; n <= main_0.int_29; n++)
                        {
                            if (galaxy_0.Habitats[n].Category == HabitatCategoryType.Planet && empire.IsObjectVisibleToThisEmpire(galaxy_0.Habitats[n]))
                            {
                                int num27 = (int)((double)galaxy_0.Habitats[n].OrbitDistance / main_0.double_0);
                                graphics_0.DrawEllipse(main_0.pen_1, num25 - num27, num26 - num27, num27 * 2, num27 * 2);
                            }
                        }
                    }
                }
                else if (main_0.double_0 >= 5.0)
                {
                    int num28 = (int)((galaxy_0.Habitats[main_0.int_28].Xpos - (double)num) / main_0.double_0) + base.ClientRectangle.Width / 2;
                    int num29 = (int)((galaxy_0.Habitats[main_0.int_28].Ypos - (double)num2) / main_0.double_0) + base.ClientRectangle.Height / 2;
                    for (int num30 = main_0.int_28; num30 <= main_0.int_29; num30++)
                    {
                        if (galaxy_0.Habitats[num30].Category == HabitatCategoryType.Planet)
                        {
                            int num31 = (int)((double)galaxy_0.Habitats[num30].OrbitDistance / main_0.double_0);
                            graphics_0.DrawEllipse(main_0.pen_1, num28 - num31, num29 - num31, num31 * 2, num31 * 2);
                        }
                    }
                }
                if (main_0.double_0 < 500.0)
                {
                    double num32 = main_0.CalculatePlanetZoomFactor(main_0.double_0);
                    double num33 = main_0.CalculateMoonZoomFactor(main_0.double_0);
                    for (int num34 = main_0.int_28; num34 <= main_0.int_29; num34++)
                    {
                        if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored && num34 > main_0.int_28 && !flag4)
                        {
                            continue;
                        }
                        Habitat habitat2 = galaxy_0.Habitats[num34];
                        if (flag4 && systemVisibilityStatus == SystemVisibilityStatus.Unexplored && habitat2.Category != 0 && habitat2.Category != HabitatCategoryType.GasCloud && !empire.IsObjectVisibleToThisEmpire(habitat2))
                        {
                            continue;
                        }
                        double num35 = main_0.double_0;
                        switch (habitat2.Category)
                        {
                            case HabitatCategoryType.Planet:
                                num35 = num32;
                                break;
                            case HabitatCategoryType.Moon:
                                num35 = num33;
                                break;
                        }
                        Bitmap bitmap = method_57(num34 - main_0.int_28);
                        if (bitmap != null)
                        {
                            int_ = (int)((double)bitmap.Width * num35);
                            int_2 = (int)((double)bitmap.Height * num35);
                            if (habitat2.Category == HabitatCategoryType.GasCloud)
                            {
                                method_59(habitat2, num35, 4, out int_, out int_2);
                                int_ = habitat2.Diameter;
                                int_2 = habitat2.Diameter;
                            }
                        }
                        else
                        {
                            method_59(habitat2, num35, 4, out int_, out int_2);
                            if (habitat2.Category == HabitatCategoryType.GasCloud || habitat2.Type == HabitatType.BlackHole)
                            {
                                int_ = habitat2.Diameter;
                                int_2 = habitat2.Diameter;
                            }
                        }
                        int num36 = 4;
                        if (habitat2.Category == HabitatCategoryType.Asteroid)
                        {
                            num36 = 1;
                        }
                        System.Drawing.Rectangle rectangle_ = method_35((int)habitat2.Xpos, (int)habitat2.Ypos, int_, int_2, num35, num36);
                        int num37 = rectangle_.X;
                        int num38 = rectangle_.Y;
                        int_ = rectangle_.Width;
                        int_2 = rectangle_.Height;
                        if (num37 + int_ >= -20 && num37 <= base.Width + 20 && num38 + int_2 >= -20 && num38 <= base.Height + 20)
                        {
                            if ((habitat2.Category == HabitatCategoryType.Moon || habitat2.Category == HabitatCategoryType.Planet) && list_5.Count > num34 - main_0.int_28 && list_5[num34 - main_0.int_28] != null)
                            {
                                DateTime value = list_7[num34 - main_0.int_28];
                                double num39 = (double)habitat2.OrbitDistance / 100.0;
                                if (habitat2.Category == HabitatCategoryType.Moon)
                                {
                                    num39 = (double)habitat2.OrbitDistance / 10.0;
                                }
                                if (habitat2.Damage > 0f)
                                {
                                    num39 /= 10.0;
                                }
                                if (currentDateTime.Subtract(value).TotalSeconds > num39)
                                {
                                    if (list_5[num34 - main_0.int_28] != null)
                                    {
                                        method_21(list_5[num34 - main_0.int_28]);
                                    }
                                    if (list_8[num34 - main_0.int_28] != null)
                                    {
                                        method_21(list_8[num34 - main_0.int_28]);
                                    }
                                    list_5[num34 - main_0.int_28] = null;
                                    list_8[num34 - main_0.int_28] = null;
                                    list_7[num34 - main_0.int_28] = DateTime.MinValue;
                                }
                            }
                            if (habitat2.Category != HabitatCategoryType.GasCloud)
                            {
                                if (list_5.Count <= num34 - main_0.int_28)
                                {
                                    method_59(habitat2, num35, num36, out int_, out int_2);
                                    int num40 = num34 - main_0.int_28 - list_5.Count;
                                    for (int num41 = 0; num41 < num40; num41++)
                                    {
                                        list_5.Add(null);
                                        list_8.Add(null);
                                        list_7.Add(DateTime.MinValue);
                                    }
                                    if (habitat2.Explosion == null || !habitat2.Explosion.ExplosionWillDestroy)
                                    {
                                        Bitmap bitmap2 = null;
                                        if (habitat2.Category != HabitatCategoryType.GasCloud)
                                        {
                                            if (habitat2.Category == HabitatCategoryType.Asteroid)
                                            {
                                                bitmap2 = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.HighQualityBilinear, CompositingQuality.HighSpeed, SmoothingMode.None));
                                            }
                                            else if (habitat2.Type == HabitatType.SuperNova)
                                            {
                                                bitmap2 = null;
                                            }
                                            else if (habitat2.Category != 0)
                                            {
                                                bitmap2 = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2));
                                            }
                                            else if (habitat2.Type != HabitatType.BlackHole)
                                            {
                                                bitmap2 = main_0.PrecacheScaledBitmap(main_0.bitmap_196[habitat2.MapPictureRef], int_, int_2);
                                            }
                                        }
                                        Bitmap item = null;
                                        if (habitat2.HasRings)
                                        {
                                            item = GetRescaledGasGiantRigns(habitat2, int_);
                                        }
                                        if (habitat2.Category != HabitatCategoryType.Planet && habitat2.Category != HabitatCategoryType.Moon)
                                        {
                                            list_5.Add(bitmap2);
                                        }
                                        else
                                        {
                                            bool bool_ = false;
                                            Bitmap bitmap3 = bitmap2;
                                            Bitmap bitmap_ = method_52(habitat2, bitmap2, out bool_);
                                            if (bool_)
                                            {
                                                bitmap3.Dispose();
                                            }
                                            bitmap_ = method_51(habitat2, bitmap_);
                                            bitmap_ = GetPlanetShadow(habitat2, bitmap_);
                                            bitmap_ = method_50(habitat2, bitmap_);
                                            list_5.Add(bitmap_);
                                        }
                                        list_8.Add(item);
                                        if (habitat2.Category != HabitatCategoryType.GasCloud && habitat2.Category != HabitatCategoryType.Asteroid && habitat2.Type != HabitatType.BlackHole && habitat2.Type != HabitatType.SuperNova)
                                        {
                                            list_5[list_5.Count - 1] = CombinePlaneWithRings(habitat2, list_5[list_5.Count - 1], list_8[list_8.Count - 1]);
                                        }
                                        list_7.Add(currentDateTime);
                                    }
                                }
                                else if (list_5[num34 - main_0.int_28] == null)
                                {
                                    method_59(habitat2, num35, num36, out int_, out int_2);
                                    if (habitat2.Explosion == null || !habitat2.Explosion.ExplosionWillDestroy)
                                    {
                                        Bitmap bitmap4 = null;
                                        if (habitat2.Category != HabitatCategoryType.GasCloud)
                                        {
                                            if (habitat2.Category == HabitatCategoryType.Asteroid)
                                            {
                                                bitmap4 = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.HighQualityBilinear, CompositingQuality.HighSpeed, SmoothingMode.None));
                                            }
                                            else if (habitat2.Type == HabitatType.SuperNova)
                                            {
                                                bitmap4 = null;
                                            }
                                            else if (habitat2.Category != 0)
                                            {
                                                bitmap4 = ((main_0.ZoomStatus == ZoomStatus.Zooming || main_0.ZoomStatus == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2));
                                            }
                                            else if (habitat2.Type != HabitatType.BlackHole)
                                            {
                                                bitmap4 = main_0.PrecacheScaledBitmap(main_0.bitmap_196[habitat2.MapPictureRef], int_, int_2);
                                            }
                                        }
                                        Bitmap value2 = null;
                                        if (habitat2.HasRings)
                                        {
                                            value2 = GetRescaledGasGiantRigns(habitat2, int_);
                                        }
                                        if (list_5[num34 - main_0.int_28] != null)
                                        {
                                            method_21(list_5[num34 - main_0.int_28]);
                                        }
                                        if (list_8[num34 - main_0.int_28] != null)
                                        {
                                            method_21(list_8[num34 - main_0.int_28]);
                                        }
                                        if (habitat2.Category != HabitatCategoryType.Planet && habitat2.Category != HabitatCategoryType.Moon)
                                        {
                                            list_5[num34 - main_0.int_28] = bitmap4;
                                            list_8[num34 - main_0.int_28] = value2;
                                        }
                                        else
                                        {
                                            bool bool_2 = false;
                                            Bitmap bitmap5 = bitmap4;
                                            Bitmap bitmap_2 = method_52(habitat2, bitmap4, out bool_2);
                                            if (bool_2)
                                            {
                                                bitmap5.Dispose();
                                            }
                                            bitmap_2 = method_51(habitat2, bitmap_2);
                                            bitmap_2 = GetPlanetShadow(habitat2, bitmap_2);
                                            bitmap_2 = method_50(habitat2, bitmap_2);
                                            list_5[num34 - main_0.int_28] = bitmap_2;
                                            list_8[num34 - main_0.int_28] = value2;
                                        }
                                        if (habitat2.Category != HabitatCategoryType.GasCloud && habitat2.Category != HabitatCategoryType.Asteroid && habitat2.Type != HabitatType.BlackHole && habitat2.Type != HabitatType.SuperNova)
                                        {
                                            list_5[num34 - main_0.int_28] = CombinePlaneWithRings(habitat2, list_5[num34 - main_0.int_28], list_8[num34 - main_0.int_28]);
                                        }
                                        list_7[num34 - main_0.int_28] = currentDateTime;
                                    }
                                }
                            }
                            if (habitat2.Category != HabitatCategoryType.GasCloud && (habitat2.Explosion == null || !habitat2.Explosion.ExplosionWillDestroy))
                            {
                                if (habitat2.Category == HabitatCategoryType.Star)
                                {
                                    int_ = habitat2.Diameter;
                                    int_2 = habitat2.Diameter;
                                }
                                else
                                {
                                    bitmap = list_5[num34 - main_0.int_28];
                                    int_ = (int)((double)bitmap.Width * num35);
                                    int_2 = (int)((double)bitmap.Height * num35);
                                }
                                rectangle_ = method_35((int)habitat2.Xpos, (int)habitat2.Ypos, int_, int_2, num35, num36);
                                num37 = rectangle_.X;
                                num38 = rectangle_.Y;
                                int_ = rectangle_.Width;
                                int_2 = rectangle_.Height;
                            }
                            if (habitat2.Type == HabitatType.BlackHole)
                            {
                                double totalSeconds = currentDateTime.Subtract(dateTime_2).TotalSeconds;
                                this.double_4 -= totalSeconds * 0.4;
                                Bitmap bitmap6 = main_0.bitmap_199[0];
                                new System.Drawing.Rectangle(0, 0, 10, 10);
                                new System.Drawing.Rectangle(0, 0, 10, 10);
                                double num42 = this.double_4 / 7.0;
                                System.Drawing.Color color_3 = System.Drawing.Color.DarkGray;
                                System.Drawing.Color color_4 = System.Drawing.Color.DarkGray;
                                System.Drawing.Color color_5 = System.Drawing.Color.DarkGray;
                                switch (habitat2.HabitatIndex % 4)
                                {
                                    case 0:
                                        color_3 = System.Drawing.Color.FromArgb(0, 0, 72);
                                        color_4 = System.Drawing.Color.FromArgb(0, 0, 46);
                                        color_5 = System.Drawing.Color.FromArgb(0, 0, 28);
                                        break;
                                    case 1:
                                        color_3 = System.Drawing.Color.FromArgb(28, 18, 46);
                                        color_4 = System.Drawing.Color.FromArgb(19, 10, 28);
                                        color_5 = System.Drawing.Color.FromArgb(12, 5, 15);
                                        break;
                                    case 2:
                                        color_3 = System.Drawing.Color.FromArgb(18, 11, 28);
                                        color_4 = System.Drawing.Color.FromArgb(13, 8, 20);
                                        color_5 = System.Drawing.Color.FromArgb(8, 5, 12);
                                        break;
                                    case 3:
                                        color_3 = System.Drawing.Color.FromArgb(28, 10, 32);
                                        color_4 = System.Drawing.Color.FromArgb(21, 7, 24);
                                        color_5 = System.Drawing.Color.FromArgb(14, 4, 16);
                                        break;
                                }
                                method_175(graphics_0);
                                int num43 = (int)((double)int_ * 0.07);
                                int num44 = (int)((double)int_2 * 0.07);
                                int num45 = num37 + (int_ - num43) / 2;
                                int num46 = num38 + (int_2 - num44) / 2;
                                if (color_15 == System.Drawing.Color.Empty)
                                {
                                    using SolidBrush brush2 = new SolidBrush(System.Drawing.Color.Black);
                                    graphics_0.FillEllipse(brush2, new System.Drawing.Rectangle(num45, num46, num43, num44));
                                }
                                num43 = (int)((double)int_ * 1.0);
                                num44 = (int)((double)int_2 * 1.0);
                                num45 = num37 + (int_ - num43) / 2;
                                num46 = num38 + (int_2 - num44) / 2;
                                new System.Drawing.Rectangle(0, 0, bitmap6.Width, bitmap6.Height);
                                new System.Drawing.Rectangle(num45, num46, num43, num44);
                                num42 = this.double_4 / 5.3;
                                method_119(graphics_0, num45, num46, num43, num44, bitmap6, currentDateTime, num42, color_5);
                                num43 = (int)((double)int_ * 0.44);
                                num44 = (int)((double)int_2 * 0.44);
                                num45 = num37 + (int_ - num43) / 2;
                                num46 = num38 + (int_2 - num44) / 2;
                                new System.Drawing.Rectangle(0, 0, bitmap6.Width, bitmap6.Height);
                                new System.Drawing.Rectangle(num45, num46, num43, num44);
                                num42 = this.double_4 / 2.3;
                                method_119(graphics_0, num45, num46, num43, num44, bitmap6, currentDateTime, num42, color_4);
                                num43 = (int)((double)int_ * 0.2);
                                num44 = (int)((double)int_2 * 0.2);
                                num45 = num37 + (int_ - num43) / 2;
                                num46 = num38 + (int_2 - num44) / 2;
                                new System.Drawing.Rectangle(0, 0, bitmap6.Width, bitmap6.Height);
                                new System.Drawing.Rectangle(num45, num46, num43, num44);
                                method_119(graphics_0, num45, num46, num43, num44, bitmap6, currentDateTime, this.double_4, color_3);
                                double_5 -= totalSeconds * 0.5;
                                method_176(graphics_0);
                                num43 = (int)((double)int_ * 0.08);
                                num44 = (int)((double)int_2 * 0.08);
                                num45 = num37 + (int_ - num43) / 2;
                                num46 = num38 + (int_2 - num44) / 2;
                                method_116(graphics_0, num45, num46, num43, num44, main_0.bitmap_200, currentDateTime, 15, 0.0, 0.0, System.Drawing.Color.Empty);
                                method_177(graphics_0);
                                dateTime_2 = currentDateTime;
                            }
                            else if (habitat2.Category == HabitatCategoryType.GasCloud)
                            {
                                DrawGasCloudToMain(this.bitmap_2, habitat2, num37, num38, int_, int_2, graphics_0);
                            }
                            else if (habitat2.Type != HabitatType.MainSequence && habitat2.Type != HabitatType.RedGiant && habitat2.Type != HabitatType.SuperGiant && habitat2.Type != HabitatType.WhiteDwarf && habitat2.Type != HabitatType.Neutron)
                            {
                                if (habitat2.Type == HabitatType.SuperNova)
                                {
                                    method_177(graphics_0);
                                }
                                else
                                {
                                    method_175(graphics_0);
                                    DrawHabToMain(list_5[num34 - main_0.int_28], list_8[num34 - main_0.int_28], num37, num38, int_, int_2, graphics_0, galaxy_0.Habitats[num34]);
                                    method_177(graphics_0);
                                }
                            }
                            else if (main_0.double_0 < method_60(habitat2.Type))
                            {
                                method_66(habitat2);
                                int int_3 = 500;
                                int int_4 = 500;
                                int int_5 = 490;
                                int int_6 = 490;
                                int int_7 = 0;
                                int int_8 = 0;
                                int int_9 = 5;
                                int int_10 = 5;
                                if (habitat2.Diameter <= this.int_8)
                                {
                                    Bitmap bitmap7 = new Bitmap(int_3, int_4, PixelFormat.Format32bppPArgb);
                                    using (Graphics graphics_ = Graphics.FromImage(bitmap7))
                                    {
                                        method_175(graphics_);
                                        double totalSeconds2 = currentDateTime.Subtract(dateTime_2).TotalSeconds;
                                        this.double_4 += totalSeconds2 * 0.03;
                                        method_119(graphics_, int_7, int_8, int_3, int_4, bitmap_5[0], currentDateTime, 0.0, System.Drawing.Color.Empty);
                                        if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                                        {
                                            method_176(graphics_);
                                        }
                                        else
                                        {
                                            method_175(graphics_);
                                        }
                                        double_5 -= totalSeconds2 * 0.02;
                                        method_119(graphics_, int_9, int_10, int_5, int_6, bitmap_5[1], currentDateTime, double_5, System.Drawing.Color.Empty);
                                    }
                                    if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                                    {
                                        method_176(graphics_0);
                                    }
                                    else
                                    {
                                        method_175(graphics_0);
                                    }
                                    graphics_0.DrawImage(bitmap7, new System.Drawing.Rectangle(num37, num38, int_, int_2), new System.Drawing.Rectangle(0, 0, bitmap7.Width, bitmap7.Height), GraphicsUnit.Pixel);
                                }
                                else
                                {
                                    if (main_0.ZoomStatus != ZoomStatus.Zooming && main_0.ZoomStatus != ZoomStatus.Stabilizing)
                                    {
                                        method_176(graphics_0);
                                    }
                                    else
                                    {
                                        method_175(graphics_0);
                                    }
                                    graphics_0.DrawImage(bitmap_5[0], new System.Drawing.Rectangle(num37, num38, int_, int_2), new System.Drawing.Rectangle(0, 0, bitmap_5[0].Width, bitmap_5[0].Height), GraphicsUnit.Pixel);
                                }
                                double num47 = 1.65;
                                Bitmap[] bitmap_3 = main_0.bitmap_201;
                                if (habitat2.Type == HabitatType.Neutron)
                                {
                                    num47 = 2.3;
                                    bitmap_3 = main_0.bitmap_202;
                                }
                                int num48 = (int)((double)int_ * num47);
                                int num49 = (int)((double)int_2 * num47);
                                int num50 = (num48 - int_) / 2;
                                int num51 = (num49 - int_2) / 2;
                                int int_11 = num37 - num50;
                                int int_12 = num38 - num51;
                                System.Drawing.Color color_6 = method_62(habitat2);
                                method_116(graphics_0, int_11, int_12, num48, num49, bitmap_3, currentDateTime, 15, 0.0, 0.0, color_6);
                                method_177(graphics_0);
                            }
                            else
                            {
                                DrawHabToMain(list_5[num34 - main_0.int_28], list_8[num34 - main_0.int_28], num37, num38, int_, int_2, graphics_0, galaxy_0.Habitats[num34]);
                            }
                            if (habitat2.IsBlockaded)
                            {
                                System.Drawing.Rectangle rectangle6 = method_81(num37 - 14, num38 - 14, int_ + 28, int_2 + 28);
                                method_98(rectangle6.X, rectangle6.Y, rectangle6.Width, rectangle6.Height, graphics_0);
                            }
                            if (main_0.int_34 < 2 && main_0.double_0 < 100.0 && !habitat2.HasBeenDestroyed)
                            {
                                int num52 = 28;
                                num52 = (int)(28.0 / main_0.double_0);
                                num52 = Math.Max(6, num52);
                                int num53 = num52 * 2;
                                if (habitat2.Owner != null && habitat2.Owner != main_0._Game.Galaxy.IndependentEmpire)
                                {
                                    using Pen pen2 = new Pen(habitat2.Owner.MainColor, 2f);
                                    System.Drawing.Rectangle rect4 = method_81(num37 - num52, num38 - num52, int_ + num53, int_2 + num53);
                                    graphics_0.DrawEllipse(pen2, rect4);
                                }
                                else if (habitat2.Owner == main_0._Game.Galaxy.IndependentEmpire && habitat2.Population != null && habitat2.Population.Count > 0)
                                {
                                    using Pen pen3 = new Pen(color_1, 2f);
                                    System.Drawing.Rectangle rect5 = method_81(num37 - num52, num38 - num52, int_ + num53, int_2 + num53);
                                    graphics_0.DrawEllipse(pen3, rect5);
                                }
                                else if (habitat2.Category == HabitatCategoryType.Planet)
                                {
                                    int val2 = (int)(48.0 * ((main_0.double_0 - 5.0) / 100.0));
                                    val2 = Math.Max(0, Math.Min(val2, 255));
                                    using Pen pen4 = new Pen(System.Drawing.Color.FromArgb(val2, 255, 255, 255), 2f);
                                    System.Drawing.Rectangle rect6 = new System.Drawing.Rectangle(num37 - num52, num38 - num52, int_ + num53, int_2 + num53);
                                    graphics_0.DrawEllipse(pen4, rect6);
                                }
                            }
                            if (main_0.double_0 <= 1.1)
                            {
                            }
                            if (habitat2 == main_0._Game.SelectedObject)
                            {
                                method_210(num37 - 2, num38 - 2, num37 + int_ + 2, num38 + int_2 + 2, graphics_0);
                            }
                            if (habitat_0 != null && habitat2 == habitat_0 && !bool_4 && !this.bool_2 && !bool_1 && !bool_3)
                            {
                                method_196(rectangle_, graphics_0);
                            }
                            method_92(habitat2, num37 + int_ / 2, num38 + int_2 / 2, bool_13: false);
                        }
                        else if ((num37 + int_ < -800 || num37 > base.Width + 800 || num38 + int_2 < -800 || num38 > base.Height + 800) && list_5.Count > num34 - main_0.int_28)
                        {
                            list_5[num34 - main_0.int_28] = null;
                            list_8[num34 - main_0.int_28] = null;
                            list_7[num34 - main_0.int_28] = DateTime.MinValue;
                        }
                        ref System.Drawing.Rectangle reference = ref rectangle_0[num34 - main_0.int_28];
                        reference = new System.Drawing.Rectangle(num37 - 2, num38 - 2, int_ + 4, int_2 + 4);
                        method_172(graphics_0, habitat2, num37, num38, int_, int_2, currentDateTime);
                        method_168(habitat2, currentDateTime, num37 + int_ / 2, num38 + int_2 / 2, graphics_0);
                        if (habitat2.Explosions != null && habitat2.Explosions.Count > 0)
                        {
                            method_179(habitat2, graphics_0, num35);
                        }
                        if (habitat2.Explosion != null)
                        {
                            method_176(graphics_0);
                            method_186(habitat2, graphics_0, num35);
                            method_177(graphics_0);
                        }
                    }
                }
            }
            animationSystem_0.DoAnimations(graphics_0, currentDateTime);
            if (main_0.double_0 < 500.0)
            {
                double maxWidth = 0.0;
                double num54 = main_0.CalculateShipZoomFactor(main_0.double_0, out maxWidth);
                BuiltObjectList builtObjectList = new BuiltObjectList();
                FighterList fighterList = new FighterList();
                _ = MouseHelper.GetCursorPosition().X;
                _ = MouseHelper.GetCursorPosition().Y;
                for (int num55 = 0; num55 < builtObjectsAtLocation.Count; num55++)
                {
                    BuiltObject builtObject4 = builtObjectsAtLocation[num55];
                    if (builtObject4 == null)
                    {
                        continue;
                    }
                    BuiltObjectImageData builtObjectImageData = main_0.builtObjectImageCache_0.FastGetImageData(builtObject4.PictureRef);
                    if (builtObjectImageData == null)
                    {
                        continue;
                    }
                    int num56 = builtObjectImageData.Image.Width;
                    int num57 = builtObjectImageData.Image.Height;
                    num56 = (int)((double)num56 / num54);
                    num57 = (int)((double)num57 / num54);
                    num56 = Math.Min(num56, (int)maxWidth);
                    num57 = Math.Min(num57, (int)maxWidth);
                    int num58 = (int)((double)((int)builtObject4.Xpos - num) / main_0.double_0) - num56 / 2 + base.Width / 2;
                    int num59 = (int)((double)((int)builtObject4.Ypos - num2) / main_0.double_0) - num57 / 2 + base.Height / 2;
                    if (num58 + num56 < -50 || num58 - num56 > base.Width + 50 || num59 + num57 < -50 || num59 - num57 > base.Height + 50 || (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(builtObject4)))
                    {
                        continue;
                    }
                    builtObjectList.Add(builtObject4);
                    bool flag5 = false;
                    if (builtObject4.TargetSpeedChanged)
                    {
                        flag5 = true;
                        builtObject4.TargetSpeedChanged = false;
                    }
                    Bitmap bitmap8 = null;
                    Bitmap bitmap9 = null;
                    int num60 = list_14.IndexOf(builtObject4.BuiltObjectID);
                    if (num60 >= 0)
                    {
                        if (list_15[num60] == BuiltObjectImageSize.Small && builtObjectImageData.ImageSize == BuiltObjectImageSize.Fullsize)
                        {
                            double size = (double)builtObjectImageData.Size / Galaxy.BuiltObjectDrawResizeFactor;
                            if (builtObject4.Empire != null)
                            {
                                bitmap8 = main_0.PrepareBuiltObjectImage(builtObject4, builtObjectImageData.Image, builtObject4.Empire.MainColor, builtObject4.Empire.SecondaryColor, size, builtObject4.Size, allowPreRotate: false, main_0.double_0);
                                bitmap9 = main_0.PrepareEngineExhaust(builtObject4, bitmap8);
                            }
                            else
                            {
                                bitmap8 = main_0.PrepareBuiltObjectImage(builtObject4, builtObjectImageData.Image, System.Drawing.Color.Gray, System.Drawing.Color.Gray, size, builtObject4.Size, allowPreRotate: false, main_0.double_0);
                                bitmap9 = main_0.PrepareEngineExhaust(builtObject4, bitmap8);
                            }
                            list_9[num60] = bitmap8;
                        }
                        list_15[num60] = builtObjectImageData.ImageSize;
                        if (method_72(builtObject4))
                        {
                            if (list_10[num60] != null)
                            {
                                method_21(list_10[num60]);
                            }
                            list_10[num60] = method_73(builtObject4, list_9[num60], builtObjectImageData.Image.Size, builtObjectImageData.MaskImage, builtObjectImageData.LightPoints, bool_13: true);
                        }
                        bitmap8 = list_10[num60];
                        _ = list_9[num60];
                        bitmap9 = list_12[num60];
                        if (flag5)
                        {
                            bitmap9 = main_0.PrepareEngineExhaust(builtObject4, list_9[num60]);
                            if (list_12[num60] != null)
                            {
                                method_21(list_12[num60]);
                            }
                            list_12[num60] = bitmap9;
                            flag5 = false;
                        }
                    }
                    else
                    {
                        double size2 = (double)builtObjectImageData.Size / Galaxy.BuiltObjectDrawResizeFactor;
                        if (builtObject4.Empire != null)
                        {
                            bitmap8 = main_0.PrepareBuiltObjectImage(builtObject4, builtObjectImageData.Image, builtObject4.Empire.MainColor, builtObject4.Empire.SecondaryColor, size2, builtObject4.Size, allowPreRotate: false, main_0.double_0);
                            bitmap9 = main_0.PrepareEngineExhaust(builtObject4, bitmap8);
                        }
                        else
                        {
                            bitmap8 = main_0.PrepareBuiltObjectImage(builtObject4, builtObjectImageData.Image, System.Drawing.Color.Gray, System.Drawing.Color.Gray, size2, builtObject4.Size, allowPreRotate: false, main_0.double_0);
                            bitmap9 = main_0.PrepareEngineExhaust(builtObject4, bitmap8);
                        }
                        list_14.Add(builtObject4.BuiltObjectID);
                        list_9.Add(bitmap8);
                        bitmap8 = method_73(builtObject4, bitmap8, builtObjectImageData.Image.Size, builtObjectImageData.MaskImage, builtObjectImageData.LightPoints, bool_13: true);
                        list_10.Add(bitmap8);
                        list_12.Add(bitmap9);
                        list_15.Add(builtObjectImageData.ImageSize);
                    }
                    num56 = bitmap8.Width;
                    num57 = bitmap8.Height;
                    num56 = Math.Min(num56, (int)maxWidth);
                    num57 = Math.Min(num57, (int)maxWidth);
                    num58 = (int)((double)((int)builtObject4.Xpos - num) / main_0.double_0) - num56 / 2 + base.Width / 2;
                    num59 = (int)((double)((int)builtObject4.Ypos - num2) / main_0.double_0) - num57 / 2 + base.Height / 2;
                    if (num58 + num56 >= -50 && num58 - num56 <= base.Width + 50 && num59 + num57 >= -50 && num59 - num57 <= base.Height + 50)
                    {
                    }
                }
                for (int num61 = 0; num61 < builtObjectList.Count; num61++)
                {
                    BuiltObject builtObject5 = builtObjectList[num61];
                    if (builtObject5 == null)
                    {
                        continue;
                    }
                    BuiltObjectImageData builtObjectImageData2 = main_0.builtObjectImageCache_0.FastGetImageData(builtObject5.PictureRef);
                    if (builtObjectImageData2 == null)
                    {
                        continue;
                    }
                    Bitmap bitmap10 = null;
                    Bitmap bitmap11 = null;
                    Bitmap bitmap12 = null;
                    int num62 = list_14.IndexOf(builtObject5.BuiltObjectID);
                    if (num62 >= 0)
                    {
                        if (list_15[num62] == BuiltObjectImageSize.Small && builtObjectImageData2.ImageSize == BuiltObjectImageSize.Fullsize)
                        {
                            double size3 = (double)builtObjectImageData2.Size / Galaxy.BuiltObjectDrawResizeFactor;
                            if (builtObject5.Empire != null)
                            {
                                bitmap10 = main_0.PrepareBuiltObjectImage(builtObject5, builtObjectImageData2.Image, builtObject5.Empire.MainColor, builtObject5.Empire.SecondaryColor, size3, builtObject5.Size, allowPreRotate: false, main_0.double_0);
                                bitmap12 = main_0.PrepareEngineExhaust(builtObject5, bitmap10);
                            }
                            else
                            {
                                bitmap10 = main_0.PrepareBuiltObjectImage(builtObject5, builtObjectImageData2.Image, System.Drawing.Color.Gray, System.Drawing.Color.Gray, size3, builtObject5.Size, allowPreRotate: false, main_0.double_0);
                                bitmap12 = main_0.PrepareEngineExhaust(builtObject5, bitmap10);
                            }
                            list_9[num62] = bitmap10;
                        }
                        list_15[num62] = builtObjectImageData2.ImageSize;
                        if (method_72(builtObject5))
                        {
                            if (list_10[num62] != null)
                            {
                                method_21(list_10[num62]);
                            }
                            list_10[num62] = method_73(builtObject5, list_9[num62], builtObjectImageData2.Image.Size, builtObjectImageData2.MaskImage, builtObjectImageData2.LightPoints, bool_13: true);
                        }
                        bitmap10 = list_10[num62];
                        bitmap11 = list_9[num62];
                        bitmap12 = list_12[num62];
                    }
                    else
                    {
                        double size4 = (double)builtObjectImageData2.Size / Galaxy.BuiltObjectDrawResizeFactor;
                        bitmap10 = main_0.PrepareBuiltObjectImage(builtObject5, builtObjectImageData2.Image, builtObject5.Empire.MainColor, builtObject5.Empire.SecondaryColor, size4, builtObject5.Size, allowPreRotate: false, main_0.double_0);
                        bitmap12 = main_0.PrepareEngineExhaust(builtObject5, bitmap10);
                        list_14.Add(builtObject5.BuiltObjectID);
                        list_9.Add(bitmap10);
                        bitmap11 = bitmap10;
                        bitmap10 = method_73(builtObject5, bitmap10, builtObjectImageData2.Image.Size, builtObjectImageData2.MaskImage, builtObjectImageData2.LightPoints, bool_13: true);
                        list_10.Add(bitmap10);
                        list_12.Add(bitmap12);
                        list_15.Add(builtObjectImageData2.ImageSize);
                    }
                    int val3 = bitmap10.Width;
                    int val4 = bitmap10.Height;
                    val3 = Math.Min(val3, (int)maxWidth);
                    val4 = Math.Min(val4, (int)maxWidth);
                    int num63 = (int)((double)((int)builtObject5.Xpos - num) / main_0.double_0) - val3 / 2 + base.Width / 2;
                    int num64 = (int)((double)((int)builtObject5.Ypos - num2) / main_0.double_0) - val4 / 2 + base.Height / 2;
                    if (num63 + val3 < -50 || num63 - val3 > base.Width + 50 || num64 + val4 < -50 || num64 - val4 > base.Height + 50)
                    {
                        continue;
                    }
                    int num65 = bitmap11.Width;
                    int num66 = bitmap11.Height;
                    int num67 = num63 + (val3 - num65) / 2;
                    int num68 = num64 + (val4 - num66) / 2;
                    Bitmap bitmap13 = bitmap10;
                    double totalSeconds3 = currentDateTime.TimeOfDay.TotalSeconds;
                    double num69 = (double)(builtObject5.BuiltObjectID % 20) / 10.0;
                    totalSeconds3 += num69;
                    double num70 = totalSeconds3 % (double_7 + double_8);
                    if (num70 < double_7)
                    {
                        if (!builtObject5.LightsOn)
                        {
                            builtObject5.LightsOn = true;
                            builtObject5.LightChanged = true;
                        }
                    }
                    else if (builtObject5.LightsOn)
                    {
                        builtObject5.LightsOn = false;
                        builtObject5.LightChanged = true;
                    }
                    if (bitmap12 != null && builtObject5.TargetSpeed > 0)
                    {
                        bitmap12 = method_217(bitmap12, builtObject5.Heading * -1f);
                    }
                    int num71 = (int)((double)num65 * 1.2);
                    int num72 = (int)((double)num66 * 1.2);
                    val3 = bitmap13.Width;
                    val4 = bitmap13.Height;
                    val3 = Math.Min(val3, (int)maxWidth);
                    val4 = Math.Min(val4, (int)maxWidth);
                    int num73 = (int)((double)((int)builtObject5.Xpos - num) / main_0.double_0) - val3 / 2 + base.Width / 2;
                    int num74 = (int)((double)((int)builtObject5.Ypos - num2) / main_0.double_0) - val4 / 2 + base.Height / 2;
                    int num75 = num73 - (num71 - val3) / 2;
                    int num76 = num74 - (num72 - val4) / 2;
                    if (!builtObject5.HasBeenDestroyed)
                    {
                        if (main_0.int_34 < 2 && builtObject5.Empire != null)
                        {
                            bool flag6 = true;
                            if (main_0.double_0 > 20.0)
                            {
                                if (builtObject5.Role != BuiltObjectRole.Base && builtObject5.Owner == null && empire2 == empire)
                                {
                                    flag6 = false;
                                }
                            }
                            else if (main_0.double_0 > 35.0 && builtObject5.Role != BuiltObjectRole.Base && empire2 == empire)
                            {
                                flag6 = false;
                            }
                            if (flag6)
                            {
                                int num77 = 8;
                                if (builtObject5.Role == BuiltObjectRole.Base)
                                {
                                    num77 = 10;
                                }
                                int num78 = Math.Max(num77, num65);
                                int num79 = Math.Max(num77, num66);
                                Math.Max(0, num77 - num65);
                                Math.Max(0, num77 - num66);
                                int num80 = num73 - (num78 - val3) / 2;
                                int num81 = num74 - (num79 - val4) / 2;
                                num65 = Math.Max(num77, num65);
                                num66 = Math.Max(num77, num66);
                                System.Drawing.Color color3 = ResolveShipSymbolColor(builtObject5);
                                if (builtObject5.Role != BuiltObjectRole.Base && builtObject5.Empire != galaxy_0.IndependentEmpire)
                                {
                                    color3 = System.Drawing.Color.FromArgb(Math.Min(255, color3.A + 48), color3.R, color3.G, color3.B);
                                    method_175(graphics_0);
                                    graphics_0.SmoothingMode = SmoothingMode.HighSpeed;
                                }
                                DrawShipSymbol(graphics_0, builtObject5, color3, num80, num81, num78, num79, num65, num66, fillInterior: false, main_0.double_0);
                                method_177(graphics_0);
                            }
                        }
                        if (bitmap12 != null && builtObject5.TargetSpeed > 0)
                        {
                            double num82 = bitmap12.Width;
                            double num83 = bitmap12.Height;
                            int num84 = num73 - (int)((num82 - (double)val3) / 2.0);
                            int num85 = num74 - (int)((num83 - (double)val4) / 2.0);
                            method_176(graphics_0);
                            graphics_0.DrawImageUnscaled(bitmap12, num84, num85);
                            method_177(graphics_0);
                        }
                        method_175(graphics_0);
                        DrawBuiltObjectToMain(bitmap13, num73, num74, graphics_0);
                        method_177(graphics_0);
                        if (builtObject5.IsBlockaded)
                        {
                            method_98(num63 - 3, num64 - 3, (int)(double)bitmap13.Width + 6, (int)(double)bitmap13.Height + 6, graphics_0);
                        }
                        if (main_0._Game.SelectedObject is ShipGroup)
                        {
                            ShipGroup shipGroup = (ShipGroup)main_0._Game.SelectedObject;
                            if (builtObject5.ShipGroup == shipGroup)
                            {
                                method_210(num75, num76, num75 + num71, num76 + num72, graphics_0);
                            }
                        }
                        else if (main_0._Game.SelectedObject is BuiltObjectList)
                        {
                            BuiltObjectList builtObjectList2 = (BuiltObjectList)main_0._Game.SelectedObject;
                            if (builtObjectList2.Contains(builtObject5))
                            {
                                method_210(num75, num76, num75 + num71, num76 + num72, graphics_0);
                            }
                        }
                        else if (builtObject5 == main_0._Game.SelectedObject)
                        {
                            method_210(num75, num76, num75 + num71, num76 + num72, graphics_0);
                        }
                        if (builtObject5.LastIonStrike > DateTime.MinValue)
                        {
                            TimeSpan timeSpan = currentDateTime.Subtract(builtObject5.LastIonStrike);
                            if (timeSpan.TotalMilliseconds < 1400.0)
                            {
                                if (!builtObject5.IonStrikeSoundPlayed)
                                {
                                    builtObject5.IonStrikeSoundPlayed = true;
                                    double double_ = 0.0;
                                    double double_2 = 0.0;
                                    method_90(num73, num74, main_0.double_0, out double_, out double_2);
                                    main_0.method_0(main_0.EffectsPlayer.ResolveIonStrike(double_, double_2));
                                }
                                int num86 = (int)((double)num65 * 1.4);
                                int num87 = currentDateTime.Second / 3;
                                Bitmap bitmap14 = lightningGenerator_0.GenerateLightning(builtObject5.BuiltObjectID + num87, num86);
                                int num88 = num67 - (num86 - num65) / 2;
                                int num89 = num68 - (num86 - num66) / 2;
                                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(num88, num89, num86, num86);
                                new System.Drawing.Rectangle(0, 0, bitmap14.Width, bitmap14.Height);
                                method_176(graphics_0);
                                double num90 = 1.0 - timeSpan.TotalMilliseconds % 250.0 / 250.0 * 0.95;
                                if (num90 < 1.0)
                                {
                                    ImageAttributes imageAttrs = method_236(num90);
                                    graphics_0.DrawImage(bitmap14, destRect, 0f, 0f, bitmap14.Width, bitmap14.Height, GraphicsUnit.Pixel, imageAttrs);
                                }
                                else
                                {
                                    graphics_0.DrawImage(bitmap14, destRect, 0f, 0f, bitmap14.Width, bitmap14.Height, GraphicsUnit.Pixel);
                                }
                                method_177(graphics_0);
                            }
                            else
                            {
                                builtObject5.LastIonStrike = DateTime.MinValue;
                            }
                        }
                        if (builtObject5.ShieldAreaRechargeStartTime > DateTime.MinValue && builtObject5.ShieldAreaRechargeTarget != null)
                        {
                            int int_13 = num67 + num65 / 2;
                            int int_14 = num68 + num66 / 2;
                            int int_15 = (int)((double)((int)builtObject5.ShieldAreaRechargeTarget.Xpos - num) / main_0.double_0) + base.Width / 2;
                            int int_16 = (int)((double)((int)builtObject5.ShieldAreaRechargeTarget.Ypos - num2) / main_0.double_0) + base.Height / 2;
                            method_93(graphics_0, currentDateTime, builtObject5, int_13, int_14, num65 / 2, int_15, int_16);
                            if (builtObject5.ShieldAreaRechargeStartTime.Subtract(currentDateTime).TotalSeconds > 3.0)
                            {
                                builtObject5.ShieldAreaRechargeStartTime = DateTime.MinValue;
                                builtObject5.ShieldAreaRechargeTarget = null;
                            }
                        }
                        if (builtObject5.LastShieldStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(builtObject5.LastShieldStrike).TotalMilliseconds < 200.0 && (double)builtObject5.LastShieldStrikeDirection > double.MinValue)
                            {
                                Bitmap bitmap15 = method_217(main_0.bitmap_17, (float)((double)builtObject5.LastShieldStrikeDirection * -1.0 + Math.PI));
                                bitmap15.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                double num91 = (double)bitmap15.Width / (double)main_0.bitmap_17.Width;
                                int num92 = (int)(num91 * (double)num65);
                                int num93 = (int)(num91 * (double)num66);
                                int num94 = num67 - (num92 - num65) / 2;
                                int num95 = num68 - (num93 - num66) / 2;
                                System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle(num94, num95, num92, num93);
                                System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap15.Width, bitmap15.Height);
                                method_176(graphics_0);
                                graphics_0.DrawImage(bitmap15, destRect2, srcRect, GraphicsUnit.Pixel);
                                method_177(graphics_0);
                            }
                            else
                            {
                                builtObject5.LastShieldStrike = DateTime.MinValue;
                            }
                        }
                        if (builtObject5.LastTractorStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(builtObject5.LastTractorStrike).TotalMilliseconds < 2000.0 && (double)builtObject5.LastTractorStrikeDirection > double.MinValue)
                            {
                                double num96 = 1.0;
                                int num97 = (int)(num96 * (double)num65);
                                int num98 = (int)(num96 * (double)num66);
                                int int_17 = num67 - (num97 - num65) / 2;
                                int int_18 = num68 - (num98 - num66) / 2;
                                method_176(graphics_0);
                                double double_3 = (double)builtObject5.LastTractorStrikeDirection * -1.0 + Math.PI;
                                method_116(graphics_0, int_17, int_18, num97, num98, main_0.bitmap_215, currentDateTime, 10, double_3, 0.0, System.Drawing.Color.Empty);
                                method_177(graphics_0);
                            }
                            else
                            {
                                builtObject5.LastTractorStrike = DateTime.MinValue;
                            }
                        }
                        if (main_0.double_0 <= 3.0)
                        {
                            if (main_0.int_34 < 1 && builtObject5.InBattle && builtObject5.ShieldsCapacity > 0)
                            {
                                int int_19 = (int)((builtObject5.Xpos - (double)num) / main_0.double_0) - bitmap11.Width / 2 + base.Width / 2;
                                int num99 = (int)((builtObject5.Ypos - (double)num2) / main_0.double_0) - bitmap11.Height / 2 + base.Height / 2;
                                method_193(int_19, num99 - 8, bitmap11.Width, builtObject5.ShieldsCapacity, (int)builtObject5.CurrentShields, graphics_0);
                            }
                            if (main_0.int_34 < 2 && builtObject5.ShipGroup != null && builtObject5.ShipGroup.LeadShip == builtObject5)
                            {
                                int num100 = (int)((builtObject5.Xpos - (double)num) / main_0.double_0) - bitmap11.Width / 2 + base.Width / 2;
                                int num101 = (int)((builtObject5.Ypos - (double)num2) / main_0.double_0) - bitmap11.Height / 2 + base.Height / 2;
                                method_190(num100 + (int)(double)bitmap11.Width + 2 - main_0.bitmap_43.Width, num101 - 2, graphics_0);
                            }
                        }
                        if ((builtObject_0 != null && builtObject5 == builtObject_0) || (shipGroup_0 != null && builtObject5.ShipGroup == shipGroup_0))
                        {
                            System.Drawing.Rectangle rectangle_2 = new System.Drawing.Rectangle(num75, num76, num71, num72);
                            method_202(rectangle_2, graphics_0);
                        }
                    }
                    method_97(builtObject5, num63, num64, currentStarDate, bool_13: false);
                    method_95(builtObject5, num63, num64, bool_13: false);
                    if (builtObject5.Explosions.Count > 0)
                    {
                        method_181(builtObject5, graphics_0, num54);
                    }
                    if (builtObject5.Weapons.Count > 0)
                    {
                        method_166(builtObject5, currentDateTime, num73, num74, graphics_0);
                    }
                }
                animationSystem_1.DoAnimations(graphics_0, currentDateTime);
                for (int num102 = 0; num102 < fightersForBuiltObjects.Count; num102++)
                {
                    Fighter fighter = fightersForBuiltObjects[num102];
                    int num103 = main_0.bitmap_6[fighter.PictureRef].Width;
                    int num104 = main_0.bitmap_6[fighter.PictureRef].Height;
                    num103 = (int)((double)num103 / num54);
                    num104 = (int)((double)num104 / num54);
                    num103 = Math.Min(num103, (int)maxWidth);
                    num104 = Math.Min(num104, (int)maxWidth);
                    int num105 = (int)((double)((int)fighter.Xpos - num) / main_0.double_0) - num103 / 2 + base.Width / 2;
                    int num106 = (int)((double)((int)fighter.Ypos - num2) / main_0.double_0) - num104 / 2 + base.Height / 2;
                    if (num105 + num103 < -50 || num105 - num103 > base.Width + 50 || num106 + num104 < -50 || num106 - num104 > base.Height + 50 || (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(fighter)) || fighter.OnboardCarrier)
                    {
                        continue;
                    }
                    fighterList.Add(fighter);
                    bool flag7 = false;
                    if (fighter.TargetSpeedChanged)
                    {
                        flag7 = true;
                        fighter.TargetSpeedChanged = false;
                    }
                    Bitmap bitmap16 = null;
                    Bitmap bitmap17 = null;
                    int num107 = list_21.IndexOf(fighter.FighterID);
                    if (num107 >= 0)
                    {
                        if (method_69(fighter))
                        {
                            if (list_17[num107] != null)
                            {
                                method_21(list_17[num107]);
                            }
                            list_17[num107] = method_70(fighter, list_16[num107], bool_13: true);
                        }
                        bitmap16 = list_17[num107];
                        _ = list_16[num107];
                        bitmap17 = list_19[num107];
                        if (flag7)
                        {
                            bitmap17 = main_0.PrepareEngineExhaust(fighter, list_16[num107]);
                            if (list_19[num107] != null)
                            {
                                method_21(list_19[num107]);
                            }
                            list_19[num107] = bitmap17;
                            flag7 = false;
                        }
                    }
                    else
                    {
                        double size5 = (double)main_0.int_5[fighter.PictureRef] / Galaxy.BuiltObjectDrawResizeFactor;
                        if (fighter.Empire != null)
                        {
                            bitmap16 = main_0.PrepareFighterImage(fighter, main_0.bitmap_6[fighter.PictureRef], fighter.Empire.MainColor, fighter.Empire.SecondaryColor, size5, fighter.Size, main_0.double_0);
                            bitmap17 = main_0.PrepareEngineExhaust(fighter, bitmap16);
                        }
                        else
                        {
                            bitmap16 = main_0.PrepareFighterImage(fighter, main_0.bitmap_6[fighter.PictureRef], System.Drawing.Color.Gray, System.Drawing.Color.Gray, size5, fighter.Size, main_0.double_0);
                            bitmap17 = main_0.PrepareEngineExhaust(fighter, bitmap16);
                        }
                        list_21.Add(fighter.FighterID);
                        list_16.Add(bitmap16);
                        bitmap16 = method_70(fighter, bitmap16, bool_13: true);
                        list_17.Add(bitmap16);
                        list_19.Add(bitmap17);
                    }
                    num103 = bitmap16.Width;
                    num104 = bitmap16.Height;
                    num103 = Math.Min(num103, (int)maxWidth);
                    num104 = Math.Min(num104, (int)maxWidth);
                    num105 = (int)((double)((int)fighter.Xpos - num) / main_0.double_0) - num103 / 2 + base.Width / 2;
                    num106 = (int)((double)((int)fighter.Ypos - num2) / main_0.double_0) - num104 / 2 + base.Height / 2;
                    if (num105 + num103 >= -50 && num105 - num103 <= base.Width + 50 && num106 + num104 >= -50 && num106 - num104 <= base.Height + 50)
                    {
                    }
                }
                for (int num108 = 0; num108 < fighterList.Count; num108++)
                {
                    Fighter fighter2 = fighterList[num108];
                    Bitmap bitmap18 = null;
                    Bitmap bitmap19 = null;
                    Bitmap bitmap20 = null;
                    int num109 = list_21.IndexOf(fighter2.FighterID);
                    if (num109 >= 0)
                    {
                        if (method_69(fighter2))
                        {
                            if (list_17[num109] != null)
                            {
                                method_21(list_17[num109]);
                            }
                            list_17[num109] = method_70(fighter2, list_16[num109], bool_13: true);
                        }
                        bitmap18 = list_17[num109];
                        bitmap19 = list_16[num109];
                        bitmap20 = list_19[num109];
                    }
                    else
                    {
                        double size6 = (double)main_0.int_5[fighter2.PictureRef] / Galaxy.BuiltObjectDrawResizeFactor;
                        bitmap18 = main_0.PrepareFighterImage(fighter2, main_0.bitmap_6[fighter2.PictureRef], fighter2.Empire.MainColor, fighter2.Empire.SecondaryColor, size6, fighter2.Size, main_0.double_0);
                        bitmap20 = main_0.PrepareEngineExhaust(fighter2, bitmap18);
                        list_21.Add(fighter2.FighterID);
                        list_16.Add(bitmap18);
                        bitmap19 = bitmap18;
                        bitmap18 = method_70(fighter2, bitmap18, bool_13: true);
                        list_17.Add(bitmap18);
                        list_19.Add(bitmap20);
                    }
                    int val5 = bitmap18.Width;
                    int val6 = bitmap18.Height;
                    val5 = Math.Min(val5, (int)maxWidth);
                    val6 = Math.Min(val6, (int)maxWidth);
                    int num110 = (int)((double)((int)fighter2.Xpos - num) / main_0.double_0) - val5 / 2 + base.Width / 2;
                    int num111 = (int)((double)((int)fighter2.Ypos - num2) / main_0.double_0) - val6 / 2 + base.Height / 2;
                    if (num110 + val5 < -50 || num110 - val5 > base.Width + 50 || num111 + val6 < -50 || num111 - val6 > base.Height + 50)
                    {
                        continue;
                    }
                    int num112 = bitmap19.Width;
                    int num113 = bitmap19.Height;
                    int num114 = num110 + (val5 - num112) / 2;
                    int num115 = num111 + (val6 - num113) / 2;
                    Bitmap bitmap21 = bitmap18;
                    if (bitmap20 != null && fighter2.TargetSpeed > 0f)
                    {
                        bitmap20 = method_217(bitmap20, fighter2.Heading * -1f);
                    }
                    int num116 = (int)((double)num112 * 1.2);
                    int num117 = (int)((double)num113 * 1.2);
                    val5 = bitmap21.Width;
                    val6 = bitmap21.Height;
                    val5 = Math.Min(val5, (int)maxWidth);
                    val6 = Math.Min(val6, (int)maxWidth);
                    int num118 = (int)((double)((int)fighter2.Xpos - num) / main_0.double_0) - val5 / 2 + base.Width / 2;
                    int num119 = (int)((double)((int)fighter2.Ypos - num2) / main_0.double_0) - val6 / 2 + base.Height / 2;
                    int num120 = num118 - (num116 - val5) / 2;
                    int num121 = num119 - (num117 - val6) / 2;
                    if (!fighter2.HasBeenDestroyed)
                    {
                        if (main_0.int_34 >= 2)
                        {
                        }
                        if (bitmap20 != null && fighter2.TargetSpeed > 0f)
                        {
                            double num122 = bitmap20.Width;
                            double num123 = bitmap20.Height;
                            int num124 = num118 - (int)((num122 - (double)val5) / 2.0);
                            int num125 = num119 - (int)((num123 - (double)val6) / 2.0);
                            method_176(graphics_0);
                            graphics_0.DrawImageUnscaled(bitmap20, num124, num125);
                            method_177(graphics_0);
                        }
                        method_175(graphics_0);
                        DrawFighterToMain(bitmap21, num118, num119, graphics_0);
                        method_177(graphics_0);
                        if (fighter2 == main_0._Game.SelectedObject)
                        {
                            method_210(num120, num121, num120 + num116, num121 + num117, graphics_0);
                        }
                        if (fighter2.LastShieldStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(fighter2.LastShieldStrike).TotalMilliseconds < 200.0 && (double)fighter2.LastShieldStrikeDirection > double.MinValue)
                            {
                                Bitmap bitmap22 = method_217(main_0.bitmap_17, (float)((double)fighter2.LastShieldStrikeDirection * -1.0 + Math.PI));
                                bitmap22.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                double num126 = (double)bitmap22.Width / (double)main_0.bitmap_17.Width;
                                int num127 = (int)(num126 * (double)num112);
                                int num128 = (int)(num126 * (double)num113);
                                int num129 = num114 - (num127 - num112) / 2;
                                int num130 = num115 - (num128 - num113) / 2;
                                System.Drawing.Rectangle destRect3 = new System.Drawing.Rectangle(num129, num130, num127, num128);
                                System.Drawing.Rectangle srcRect2 = new System.Drawing.Rectangle(0, 0, bitmap22.Width, bitmap22.Height);
                                method_176(graphics_0);
                                graphics_0.DrawImage(bitmap22, destRect3, srcRect2, GraphicsUnit.Pixel);
                                method_177(graphics_0);
                            }
                            else
                            {
                                fighter2.LastShieldStrike = DateTime.MinValue;
                            }
                        }
                        if (main_0.double_0 <= 3.0 && main_0.int_34 < 1 && fighter2.InBattle && fighter2.Specification.ShieldsCapacity > 0)
                        {
                            int int_20 = (int)((fighter2.Xpos - (double)num) / main_0.double_0) - bitmap19.Width / 2 + base.Width / 2;
                            int num131 = (int)((fighter2.Ypos - (double)num2) / main_0.double_0) - bitmap19.Height / 2 + base.Height / 2;
                            method_193(int_20, num131 - 8, bitmap19.Width, fighter2.Specification.ShieldsCapacity, (int)fighter2.CurrentShields, graphics_0);
                        }
                    }
                    if (fighter2.Explosions.Count > 0)
                    {
                        method_182(fighter2, graphics_0, num54);
                    }
                    if (fighter2.Weapons.Count > 0)
                    {
                        method_164(fighter2, currentDateTime, num118, num119, graphics_0);
                    }
                }
            }
            if (main_0.double_0 < 500.0)
            {
                double maxWidth2 = 0.0;
                double num132 = main_0.CalculateCreatureZoomFactor(main_0.double_0, out maxWidth2);
                CreatureList creatureList = new CreatureList();
                new Keyboard();
                _ = MouseHelper.GetCursorPosition().X;
                _ = MouseHelper.GetCursorPosition().Y;
                if (num5 <= (double)(Galaxy.MaxSolarSystemSize + 5000))
                {
                    creatureList.AddRange(galaxy_0.Systems[habitat.SystemIndex].Creatures);
                }
                else
                {
                    GalaxyLocationList galaxyLocationList4 = galaxy_0.DetermineGalaxyLocationsAtPoint(num, num2, GalaxyLocationType.RestrictedArea);
                    for (int num133 = 0; num133 < galaxyLocationList4.Count; num133++)
                    {
                        GalaxyLocation galaxyLocation2 = galaxyLocationList4[num133];
                        creatureList.AddRange(galaxyLocation2.RelatedCreatures);
                        for (int num134 = 0; num134 < galaxyLocation2.RelatedCreatures.Count; num134++)
                        {
                            Creature creature = galaxyLocation2.RelatedCreatures[num134];
                            creature.DoTasks(currentDateTime);
                        }
                    }
                }
                for (int num135 = 0; num135 < creatureList.Count; num135++)
                {
                    Creature creature2 = creatureList[num135];
                    int num136 = main_0.bitmap_10[creature2.PictureRef][0].Width;
                    int num137 = main_0.bitmap_10[creature2.PictureRef][0].Height;
                    num136 = (int)((double)num136 / num132);
                    num137 = (int)((double)num137 / num132);
                    num136 = Math.Min(num136, (int)maxWidth2);
                    num137 = Math.Min(num137, (int)maxWidth2);
                    int num138 = (int)((creature2.Xpos - ((double)num + (double)num136 * main_0.double_0 / 2.0)) / main_0.double_0) + base.Width / 2;
                    int num139 = (int)((creature2.Ypos - ((double)num2 + (double)num137 * main_0.double_0 / 2.0)) / main_0.double_0) + base.Height / 2;
                    if (num138 + num136 < -50 || num138 - num136 > base.Width + 50 || num139 + num137 < -50 || num139 - num137 > base.Height + 50 || (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(creature2)))
                    {
                        continue;
                    }
                    int num140 = list_24.IndexOf(creature2.CreatureID);
                    Bitmap[] array;
                    Bitmap[] array2;
                    if (num140 >= 0)
                    {
                        array = list_22[num140];
                        array2 = list_23[num140];
                    }
                    else
                    {
                        int pictureRef = creature2.PictureRef;
                        int num141 = pictureRef + 5;
                        double size7 = (double)main_0.sveqhmNacy[pictureRef] / Galaxy.CreatureDrawResizeFactor;
                        array = new Bitmap[main_0.bitmap_10[pictureRef].Length];
                        for (int num142 = 0; num142 < main_0.bitmap_10[pictureRef].Length; num142++)
                        {
                            array[num142] = main_0.PrepareCreatureImage(creature2, main_0.bitmap_10[pictureRef][num142], size7, creature2.Size, allowPreRotate: false);
                        }
                        size7 = (double)main_0.sveqhmNacy[num141] / Galaxy.CreatureDrawResizeFactor;
                        array2 = new Bitmap[main_0.bitmap_10[num141].Length];
                        for (int num143 = 0; num143 < main_0.bitmap_10[num141].Length; num143++)
                        {
                            array2[num143] = main_0.PrepareCreatureImage(creature2, main_0.bitmap_10[num141][num143], size7, creature2.Size, allowPreRotate: false);
                        }
                        list_24.Add(creature2.CreatureID);
                        list_22.Add(array);
                        list_23.Add(array2);
                    }
                    num136 = array[0].Width;
                    num137 = array[0].Height;
                    num136 = (int)((double)num136 / num132);
                    num137 = (int)((double)num137 / num132);
                    num136 = Math.Min(num136, (int)maxWidth2);
                    num137 = Math.Min(num137, (int)maxWidth2);
                    num138 = (int)((creature2.Xpos - (double)(num + (int)((double)num136 * num132 / 2.0))) / main_0.double_0) + base.Width / 2;
                    num139 = (int)((creature2.Ypos - (double)(num2 + (int)((double)num137 * num132 / 2.0))) / main_0.double_0) + base.Height / 2;
                    if (num138 + num136 < -50 || num138 - num136 > base.Width + 50 || num139 + num137 < -50 || num139 - num137 > base.Height + 50)
                    {
                        continue;
                    }
                    Bitmap bitmap23 = main_0.method_108(creature2, array[0], main_0.bitmap_11[creature2.PictureRef][0]);
                    int num144 = bitmap23.Width;
                    int num145 = bitmap23.Height;
                    bitmap23 = method_218(bitmap23, creature2.CurrentHeading * -1f, GraphicsQuality.Medium);
                    int num146 = (bitmap23.Width - num144) / 2;
                    int num147 = (bitmap23.Height - num145) / 2;
                    num146 = (int)((double)num146 / num132);
                    num147 = (int)((double)num147 / num132);
                    int num148 = (int)((double)num136 * 1.5);
                    int num149 = (int)((double)num137 * 1.5);
                    int num150 = num138 - (num148 - num136) / 2;
                    int num151 = num139 - (num149 - num137) / 2;
                    if (!creature2.HasBeenDestroyed)
                    {
                        if (creature2.CurrentSpeed > 0f)
                        {
                            method_112(creature2, graphics_0, num138 - num146, num139 - num147, num136, num137, array, array2, currentDateTime, 10, (double)creature2.CurrentHeading * -1.0);
                        }
                        else
                        {
                            DrawCreatureToMain(creature2, bitmap23, num138 - num146, num139 - num147, graphics_0, num132, num136, num144, num145);
                        }
                        _ = ((int)((double)bitmap23.Width / num132) - num136) / 2;
                        _ = ((int)((double)bitmap23.Height / num132) - num137) / 2;
                        if (creature2 == main_0._Game.SelectedObject)
                        {
                            method_210(num150, num151, num150 + num148, num151 + num149, graphics_0);
                        }
                    }
                    list_4.Add(new System.Drawing.Rectangle(num138 - 7, num139 - 7, (int)((double)bitmap23.Width / main_0.double_0) + 14, (int)((double)bitmap23.Height / main_0.double_0) + 14));
                }
            }
            if (Control.MouseButtons == MouseButtons.Left && main_0.method_207(MouseHelper.GetCursorPosition()) == null && !main_0.itemListCollectionPanel_0.Area.Contains(MouseHelper.GetCursorPosition()) && (main_0.int_15 != main_0.int_32 || main_0.int_16 != main_0.int_33))
            {
                int num152 = Math.Min(main_0.int_15, main_0.int_32);
                int num153 = Math.Min(main_0.int_16, main_0.int_33);
                int num154 = Math.Abs(main_0.int_15 - main_0.int_32);
                int num155 = Math.Abs(main_0.int_16 - main_0.int_33);
                num152 = base.Width / 2 + (int)((double)(num152 - num) / main_0.double_0);
                num153 = base.Height / 2 + (int)((double)(num153 - num2) / main_0.double_0);
                num154 = (int)((double)num154 / main_0.double_0);
                num155 = (int)((double)num155 / main_0.double_0);
                graphics_0.DrawRectangle(main_0.pen_1, new System.Drawing.Rectangle(num152, num153, num154, num155));
            }
            if (main_0.double_0 < 500.0)
            {
                double num156 = main_0.CalculatePlanetZoomFactor(main_0.double_0);
                double num157 = main_0.CalculateMoonZoomFactor(main_0.double_0);
                for (int num158 = main_0.int_28; num158 <= main_0.int_29; num158++)
                {
                    if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored && num158 > main_0.int_28 && !flag4)
                    {
                        continue;
                    }
                    Habitat habitat3 = galaxy_0.Habitats[num158];
                    bool flag8 = false;
                    if (flag4)
                    {
                        if (!empire.IsObjectVisibleToThisEmpire(habitat3))
                        {
                            continue;
                        }
                        flag8 = true;
                    }
                    double double_4 = main_0.double_0;
                    switch (habitat3.Category)
                    {
                        case HabitatCategoryType.Planet:
                            double_4 = num156;
                            break;
                        case HabitatCategoryType.Moon:
                            double_4 = num157;
                            break;
                    }
                    Bitmap bitmap24 = method_57(num158 - main_0.int_28);
                    if (habitat3.Category == HabitatCategoryType.GasCloud)
                    {
                        bitmap24 = this.bitmap_2;
                        method_59(habitat3, double_4, 0, out int_, out int_2);
                    }
                    if (bitmap24 == null)
                    {
                        bitmap24 = main_0.habitatImageCache_0.ObtainImage(habitat3);
                    }
                    double num159 = (double)bitmap24.Width / (double)bitmap24.Height;
                    if (bitmap24.Width > bitmap24.Height)
                    {
                        int_ = (int)((double)habitat3.Diameter * num159);
                        int_2 = habitat3.Diameter;
                    }
                    else
                    {
                        int_ = habitat3.Diameter;
                        int_2 = (int)((double)habitat3.Diameter / num159);
                    }
                    int int_21 = 4;
                    if (habitat3.Category == HabitatCategoryType.Asteroid)
                    {
                        int_21 = 1;
                    }
                    System.Drawing.Rectangle rectangle7 = method_35((int)habitat3.Xpos, (int)habitat3.Ypos, int_, int_2, double_4, int_21);
                    int num37 = rectangle7.X;
                    int num38 = rectangle7.Y;
                    int_ = rectangle7.Width;
                    int_2 = rectangle7.Height;
                    if (num37 + int_ < -20 || num37 > base.Width + 20 || num38 + int_2 < -20 || num38 > base.Height + 20)
                    {
                        continue;
                    }
                    bool flag9 = false;
                    bool flag10 = false;
                    bool flag11 = false;
                    if ((main_0.int_34 < 2 && systemVisibilityStatus != SystemVisibilityStatus.Unexplored) || flag8)
                    {
                        if (!habitat3.HasBeenDestroyed)
                        {
                            if (main_0.double_0 < 2.0)
                            {
                                if (habitat3.Owner != null && habitat3.Population != null && habitat3.Population.Count > 0)
                                {
                                    flag10 = true;
                                }
                                if (habitat3.BasesAtHabitat != null && habitat3.BasesAtHabitat.Count > 0)
                                {
                                    flag9 = true;
                                }
                                if (habitat3.Category == HabitatCategoryType.Planet)
                                {
                                    flag9 = true;
                                }
                            }
                            else if (main_0.double_0 < 10.0)
                            {
                                if (habitat3.Owner != null && habitat3.Population != null && habitat3.Population.Count > 0)
                                {
                                    flag9 = true;
                                }
                                if (habitat3.BasesAtHabitat != null && habitat3.BasesAtHabitat.Count > 0)
                                {
                                    flag9 = true;
                                }
                                if (habitat3.Category == HabitatCategoryType.Planet)
                                {
                                    flag9 = true;
                                }
                                if (habitat3.Ruin != null)
                                {
                                    flag11 = true;
                                }
                            }
                            else if (main_0.double_0 < 40.0)
                            {
                                if (habitat3.Owner != null && habitat3.Population != null && habitat3.Population.Count > 0)
                                {
                                    flag9 = true;
                                }
                                if (habitat3.BasesAtHabitat != null && habitat3.BasesAtHabitat.Count > 0)
                                {
                                    flag9 = true;
                                }
                                if (habitat3.Ruin != null)
                                {
                                    flag11 = true;
                                }
                            }
                            else
                            {
                                if (habitat3.Owner != null && habitat3.Population != null && habitat3.Population.Count > 0)
                                {
                                    flag9 = true;
                                }
                                if (habitat3.Ruin != null)
                                {
                                    flag11 = true;
                                }
                            }
                        }
                        if (main_0.int_34 == 2)
                        {
                            flag10 = false;
                            flag9 = false;
                            flag11 = false;
                        }
                    }
                    if (flag10)
                    {
                        System.Drawing.Rectangle rectangle8 = method_81(num37, num38, int_, int_2);
                        Bitmap bitmap25 = null;
                        int num160 = list_0.IndexOf(habitat3.HabitatIndex);
                        if (num160 >= 0 && list_1.Count > num160)
                        {
                            bitmap25 = list_1[num160];
                        }
                        if (bitmap25 == null)
                        {
                            bitmap25 = method_89(habitat3);
                            list_1.Add(bitmap25);
                            list_0.Add(habitat3.HabitatIndex);
                        }
                        method_87(graphics_0, habitat3, rectangle8.X, rectangle8.Y, rectangle8.Width, rectangle8.Height, bitmap25);
                    }
                    else if (flag9 || flag11)
                    {
                        Bitmap bitmap26 = null;
                        int num161 = list_0.IndexOf(habitat3.HabitatIndex);
                        if (num161 >= 0 && list_1.Count > num161)
                        {
                            bitmap26 = list_1[num161];
                        }
                        if (bitmap26 == null)
                        {
                            bitmap26 = method_82(habitat3, main_0.double_0, flag9, flag11);
                            list_1.Add(bitmap26);
                            list_0.Add(habitat3.HabitatIndex);
                        }
                        method_83(graphics_0, habitat3, num37, num38, int_, int_2, bitmap26);
                    }
                }
            }
            method_91(num, num2, main_0.double_0);
            if (main_0.double_0 < 200.0)
            {
                if (flag2)
                {
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Explored && flag3)
                {
                    method_48(graphics_0, graphicsPath_0, graphicsPath_1);
                }
            }
            graphicsPath_0.Reset();
            graphicsPath_1.Reset();
            graphicsPath_2.Reset();
            graphicsPath_0.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            graphicsPath_1.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            graphicsPath_2.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            dateTime_2 = currentDateTime;
        }
    }
}
