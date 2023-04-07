// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MainView
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

//using BaconDistantWorlds;
using DistantWorlds.Mods;
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

namespace DistantWorlds.Controls
{
    public class MainView : Panel
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

        private PlanetaryRingsGenerator planetaryRingsGenerator_0;

        private GalaxyNebulaeGenerator galaxyNebulaeGenerator_0;

        private LightningGenerator lightningGenerator_0;

        private System.Drawing.Color color_15;

        private int int_5;

        private DateTime dateTime_1;

        private bool bool_7;

        private int int_6;

        private DateTime dateTime_2;

        private double double_4;

        private double double_5;

        private double double_6;

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

        public static event EventHandler<ReturnDoubleModsArgs> MinZoomLevelForWeaponsCirclesMods;
        public static event EventHandler<ReturnDoubleModsArgs> BackgroundStarsAtZoomLevelMods;
        public static event EventHandler<DrawRangeModsArgs> DrawWeaponRanges;
        public static event EventHandler<DrawRangeModsArgs> DrawGravityWellRange;
        public static event EventHandler<DrawPathLineModsArgs> DrawPathLineMods;



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
            bitmap_6 = new Bitmap[main_0.esJqlOpLpG.Length];
            for (int i = 0; i < main_0.esJqlOpLpG.Length; i++)
            {
                Bitmap bitmap = main_0.esJqlOpLpG[i];
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
                Microsoft.Xna.Framework.Rectangle value = new Microsoft.Xna.Framework.Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
                GraphicsDevice.Present(value, null, base.Handle);
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
            Class7.VEFSJNszvZKMZ();
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
            planetaryRingsGenerator_0 = new PlanetaryRingsGenerator();
            lightningGenerator_0 = new LightningGenerator();
            color_15 = System.Drawing.Color.Empty;
            dateTime_1 = DateTime.MinValue;
            bool_7 = true;
            int_6 = 20;
            dateTime_2 = DateTime.Now;
            double_5 = 0.2;
            double_6 = 0.4;
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

        public void DrawMainViewXna()
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
                //if (main_0.double_0 > BaconMain.minZoomLevelForWeaponsCircles)
                if (main_0.double_0 > MainView.OnMinZoomLevelForWeaponsCirclesMods())
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
                switch (main_0.zEsBtZgRxo)
                {
                    case ZoomStatus.Zooming:
                        main_0.zEsBtZgRxo = ZoomStatus.Stabilizing;
                        break;
                    case ZoomStatus.Stabilizing:
                        main_0.zEsBtZgRxo = ZoomStatus.Stable;
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
            if (main_0.zEsBtZgRxo == ZoomStatus.Zooming)
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
                    bitmap2 = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(bitmap, num, num3, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(bitmap, num, num3));
                    bitmap2.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
                    Bitmap bitmap3 = bitmap2;
                    bitmap2 = method_224(bitmap2, System.Drawing.Color.FromArgb(255, 64, 0), bool_13: true);
                    bitmap3.Dispose();
                    bitmap2.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
                    using (Graphics graphics = Graphics.FromImage(bitmap_7))
                    {
                        if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
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
                            bitmap = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.bitmap_192[num], num2, num4, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.bitmap_192[num], num2, num4));
                            break;
                        case HabitatType.Desert:
                        case HabitatType.Ice:
                            num = random.Next(0, 2);
                            bitmap = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.bitmap_193[num], num2, num4, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.bitmap_193[num], num2, num4));
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
                        if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
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
                bitmap = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.bitmap_194[val], num, num2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.bitmap_194[val], num, num2));
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
                    if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
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

        private Bitmap method_53(Habitat habitat_1, Bitmap bitmap_7)
        {
            int num = 0;
            num = (int)((double)bitmap_7.Width / 150.0);
            int num2 = bitmap_7.Width - num;
            int num3 = bitmap_7.Height - num;
            Bitmap bitmap = null;
            bitmap = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.bitmap_191, num2, num3, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.bitmap_191, num2, num3, InterpolationMode.Low, CompositingQuality.HighSpeed, SmoothingMode.None));
            Habitat systemStar = galaxy_0.Systems[habitat_1.SystemIndex].SystemStar;
            float num4 = (float)Galaxy.DetermineAngle(habitat_1.Xpos, habitat_1.Ypos, systemStar.Xpos, systemStar.Ypos);
            num4 *= -1f;
            Bitmap bitmap2 = bitmap;
            bitmap = method_217(bitmap, num4);
            bitmap2.Dispose();
            using (Graphics graphics = Graphics.FromImage(bitmap_7))
            {
                if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
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
                    bitmap_ = method_55(habitat_1, int_11);
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
                    Bitmap result = method_56(habitat_1, bitmap3, bitmap_);
                    bitmap3.Dispose();
                    return result;
                }
            }
            return new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
        }

        internal Bitmap method_55(Habitat habitat_1, int int_11)
        {
            if (habitat_1 != null && main_0 != null && main_0.bitmap_1 != null)
            {
                int num = habitat_1.HabitatIndex % 10;
                Bitmap bitmap = main_0.bitmap_1[num];
                double num2 = (double)int_11 / (double)habitat_1.Diameter;
                double num3 = (double)habitat_1.Diameter / 500.00000000000006;
                num2 *= num3;
                int num4 = (int)((double)bitmap.Width * num2);
                int num5 = (int)((double)bitmap.Height * num2);
                if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
                {
                    return main_0.PrecacheScaledBitmap(bitmap, num4, num5, InterpolationMode.HighQualityBilinear, CompositingQuality.HighSpeed, SmoothingMode.HighSpeed);
                }
                return main_0.PrecacheScaledBitmap(bitmap, num4, num5, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None);
            }
            return null;
        }

        internal Bitmap method_56(Habitat habitat_1, Bitmap bitmap_7, Bitmap bitmap_8)
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
                if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
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
                    SystemVisibilityStatus systemVisibilityStatus2 = SystemVisibilityStatus.Visible;
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
                                                bitmap2 = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.HighQualityBilinear, CompositingQuality.HighSpeed, SmoothingMode.None));
                                            }
                                            else if (habitat2.Type == HabitatType.SuperNova)
                                            {
                                                bitmap2 = null;
                                            }
                                            else if (habitat2.Category != 0)
                                            {
                                                bitmap2 = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2));
                                            }
                                            else if (habitat2.Type != HabitatType.BlackHole)
                                            {
                                                bitmap2 = main_0.PrecacheScaledBitmap(main_0.bitmap_196[habitat2.MapPictureRef], int_, int_2);
                                            }
                                        }
                                        Bitmap item = null;
                                        if (habitat2.HasRings)
                                        {
                                            item = method_55(habitat2, int_);
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
                                            bitmap_ = method_53(habitat2, bitmap_);
                                            bitmap_ = method_50(habitat2, bitmap_);
                                            list_5.Add(bitmap_);
                                        }
                                        list_8.Add(item);
                                        if (habitat2.Category != HabitatCategoryType.GasCloud && habitat2.Category != HabitatCategoryType.Asteroid && habitat2.Type != HabitatType.BlackHole && habitat2.Type != HabitatType.SuperNova)
                                        {
                                            list_5[list_5.Count - 1] = method_56(habitat2, list_5[list_5.Count - 1], list_8[list_8.Count - 1]);
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
                                                bitmap4 = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.HighQualityBilinear, CompositingQuality.HighSpeed, SmoothingMode.None));
                                            }
                                            else if (habitat2.Type == HabitatType.SuperNova)
                                            {
                                                bitmap4 = null;
                                            }
                                            else if (habitat2.Category != 0)
                                            {
                                                bitmap4 = ((main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing) ? main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2, InterpolationMode.NearestNeighbor, CompositingQuality.HighSpeed, SmoothingMode.None) : main_0.PrecacheScaledBitmap(main_0.habitatImageCache_0.ObtainImage(habitat2), int_, int_2));
                                            }
                                            else if (habitat2.Type != HabitatType.BlackHole)
                                            {
                                                bitmap4 = main_0.PrecacheScaledBitmap(main_0.bitmap_196[habitat2.MapPictureRef], int_, int_2);
                                            }
                                        }
                                        Bitmap value2 = null;
                                        if (habitat2.HasRings)
                                        {
                                            value2 = method_55(habitat2, int_);
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
                                            bitmap_2 = method_53(habitat2, bitmap_2);
                                            bitmap_2 = method_50(habitat2, bitmap_2);
                                            list_5[num34 - main_0.int_28] = bitmap_2;
                                            list_8[num34 - main_0.int_28] = value2;
                                        }
                                        if (habitat2.Category != HabitatCategoryType.GasCloud && habitat2.Category != HabitatCategoryType.Asteroid && habitat2.Type != HabitatType.BlackHole && habitat2.Type != HabitatType.SuperNova)
                                        {
                                            list_5[num34 - main_0.int_28] = method_56(habitat2, list_5[num34 - main_0.int_28], list_8[num34 - main_0.int_28]);
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
                                        if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
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
                                    if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
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
                                    if (main_0.zEsBtZgRxo != ZoomStatus.Zooming && main_0.zEsBtZgRxo != ZoomStatus.Stabilizing)
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

        private System.Drawing.Color method_75(Habitat habitat_1)
        {
            if (habitat_1 != null && habitat_1.Category == HabitatCategoryType.Star)
            {
                int mapPictureRef = habitat_1.MapPictureRef;
                switch (habitat_1.Type)
                {
                    case HabitatType.MainSequence:
                        return (habitat_1.MapPictureRef - main_0.int_6) switch
                        {
                            0 => System.Drawing.Color.FromArgb(255, 255, 200),
                            1 => System.Drawing.Color.FromArgb(255, 248, 160),
                            2 => System.Drawing.Color.FromArgb(255, 232, 96),
                            3 => System.Drawing.Color.FromArgb(255, 200, 48),
                            4 => System.Drawing.Color.FromArgb(255, 192, 0),
                            _ => System.Drawing.Color.FromArgb(255, 176, 0),
                        };
                    case HabitatType.RedGiant:
                        mapPictureRef = habitat_1.MapPictureRef - main_0.DiZcdsnvl0;
                        return System.Drawing.Color.FromArgb(255, 32, 0);
                    case HabitatType.SuperGiant:
                        mapPictureRef = habitat_1.MapPictureRef - main_0.int_7;
                        return System.Drawing.Color.FromArgb(255, 64, 0);
                    case HabitatType.WhiteDwarf:
                        return (habitat_1.MapPictureRef - main_0.int_8) switch
                        {
                            0 => System.Drawing.Color.FromArgb(128, 232, 255),
                            1 => System.Drawing.Color.FromArgb(176, 255, 255),
                            _ => System.Drawing.Color.FromArgb(224, 255, 255),
                        };
                    case HabitatType.Neutron:
                        if (habitat_1.MapPictureRef - main_0.int_9 == 0)
                        {
                            return System.Drawing.Color.FromArgb(80, 80, 255);
                        }
                        return System.Drawing.Color.FromArgb(96, 208, 255);
                }
            }
            return System.Drawing.Color.FromArgb(255, 255, 144);
        }

        private void method_76()
        {
            int num = main_0.int_13 + main_0.int_21;
            int num2 = main_0.int_14 + main_0.vhadzRiecM;
            if (main_0._Game == null || main_0._Game.Galaxy == null)
            {
                return;
            }
            bool fadeCivilianShips = false;
            if (main_0.gameOptions_0 != null)
            {
                fadeCivilianShips = main_0.gameOptions_0.MapOverlayFadeCivilianShips;
            }
            Empire empire = main_0._Game.PlayerEmpire;
            if (main_0.empire_1 != null)
            {
                empire = main_0.empire_1;
            }
            DateTime currentDateTime = main_0._Game.Galaxy.CurrentDateTime;
            long currentStarDate = main_0._Game.Galaxy.CurrentStarDate;
            DateTime now = DateTime.Now;
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
                    XnaDrawingHelper.FillRectangle(spriteBatch_0, base.ClientRectangle, color_15);
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
                    SystemVisibilityStatus systemVisibilityStatus2 = SystemVisibilityStatus.Visible;
                    goto IL_05ae;
                }
                rectangle = default(System.Drawing.Rectangle);
                switch (systemVisibilityStatus)
                {
                    case SystemVisibilityStatus.Explored:
                        break;
                    case SystemVisibilityStatus.Visible:
                        goto IL_05ae;
                    default:
                        goto IL_05fc;
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
            goto IL_05fc;
        IL_05fc:
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
                            flag3 = false;
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
                BuiltObject builtObject3 = empire.LongRangeScanners[k];
                if (builtObject3 == null)
                {
                    continue;
                }
                int sensorLongRange = builtObject3.SensorLongRange;
                if (habitat != null)
                {
                    double num11 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject3.Xpos, builtObject3.Ypos);
                    if ((int)num11 - sensorLongRange <= Galaxy.MaxSolarSystemSize + 500 && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                    {
                        flag4 = true;
                        System.Drawing.Rectangle rectangle4 = method_34((int)builtObject3.Xpos, (int)builtObject3.Ypos, sensorLongRange * 2, sensorLongRange * 2, main_0.double_0);
                        if (rectangle4.Width > 8 && method_151(base.ClientRectangle, rectangle4))
                        {
                            flag3 = ((!rectangle4.Contains(base.ClientRectangle)) ? true : false);
                            graphicsPath_2.AddEllipse(rectangle4);
                            graphicsPath_1.AddEllipse(rectangle4);
                        }
                    }
                    continue;
                }
                System.Drawing.Rectangle rectangle5 = method_34((int)builtObject3.Xpos, (int)builtObject3.Ypos, sensorLongRange * 2, sensorLongRange * 2, main_0.double_0);
                if (rectangle5.Width > 8 && method_151(base.ClientRectangle, rectangle5))
                {
                    flag3 = ((!rectangle5.Contains(base.ClientRectangle)) ? true : false);
                    graphicsPath_2.AddEllipse(rectangle5);
                    graphicsPath_1.AddEllipse(rectangle5);
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                {
                    double num12 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject3.Xpos, builtObject3.Ypos);
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
                    for (int l = 0; l < builtObjectsAtLocation.Count; l++)
                    {
                        BuiltObject builtObject4 = builtObjectsAtLocation[l];
                        if (builtObject4 == null)
                        {
                            continue;
                        }
                        if (builtObject4.HyperDenyActive)
                        {
                            int num13 = (int)((builtObject4.Xpos - (double)num) / main_0.double_0) + base.Width / 2;
                            int num14 = (int)((builtObject4.Ypos - (double)num2) / main_0.double_0) + base.Height / 2;
                            if (num13 >= -1000 && num13 <= base.Width + 1000 && num14 >= -1000 && num14 <= base.Height + 1000 && (main_0._Game.GodMode || empire.IsObjectVisibleToThisEmpire(builtObject4)))
                            {
                                int num15 = (int)((double)builtObject4.WeaponHyperDenyRange / main_0.double_0);
                                XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num13 - num15, num14 - num15, num15 * 2, num15 * 2), spriteBatch: spriteBatch_0, texture: this.texture2D_4[0], rotationAngle: 0f, tintColor: System.Drawing.Color.FromArgb(128, 255, 255, 255));
                            }
                        }
                        else
                        {
                            if (builtObject4.HyperStopRange <= 0 || !(main_0.double_0 < 50.0))
                            {
                                continue;
                            }
                            int num16 = (int)((builtObject4.Xpos - (double)num) / main_0.double_0) + base.Width / 2;
                            int num17 = (int)((builtObject4.Ypos - (double)num2) / main_0.double_0) + base.Height / 2;
                            if (num16 >= -3000 && num16 <= base.Width + 3000 && num17 >= -3000 && num17 <= base.Height + 3000 && (main_0._Game.GodMode || empire.IsObjectVisibleToThisEmpire(builtObject4)))
                            {
                                int num18 = (int)((double)builtObject4.HyperStopRange / main_0.double_0);
                                System.Drawing.Rectangle area = new System.Drawing.Rectangle(num16 - num18, num17 - num18, num18 * 2, num18 * 2);
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
                                XnaDrawingHelper.DrawCircle(spriteBatch_0, area, 120, color2, (int)num19, dashed: true);
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
                            XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num23 - num20, num24 - num20, num20 * 2, num20 * 2), spriteBatch: spriteBatch_0, texture: this.texture2D_4[0], rotationAngle: 0f, tintColor: System.Drawing.Color.FromArgb(128, 255, 255, 255));
                        }
                    }
                }
                //if (main_0.double_0 < BaconMain.backgroundStarsAtZoomLevel)
                if (main_0.double_0 < MainView.OnBackgroundStarsAtZoomLevelMods())
                {
                    if (bool_9)
                    {
                        method_108(spriteBatch_0);
                    }
                    else
                    {
                        method_46();
                        method_106(solidBrush_9.Color, starFieldItemList_3, rectangle_4, this.int_4, 38, spriteBatch_0);
                        method_106(solidBrush_8.Color, starFieldItemList_2, rectangle_3, this.int_3, 20, spriteBatch_0);
                        method_106(solidBrush_7.Color, starFieldItemList_1, this.rectangle_2, this.int_2, 11, spriteBatch_0);
                    }
                    method_102(spriteBatch_0, solidBrush_6, starFieldItemList_0, rectangle_1, int_1, 6);
                }
                if (systemVisibilityStatus != SystemVisibilityStatus.Explored && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                {
                    if (flag4 && main_0.double_0 >= 5.0)
                    {
                        int num25 = (int)((galaxy_0.Habitats[main_0.int_28].Xpos - (double)num) / main_0.double_0) + base.ClientRectangle.Width / 2;
                        int num26 = (int)((galaxy_0.Habitats[main_0.int_28].Ypos - (double)num2) / main_0.double_0) + base.ClientRectangle.Height / 2;
                        for (int n = main_0.int_28; n <= main_0.int_29; n++)
                        {
                            if (n < galaxy_0.Habitats.Count)
                            {
                                Habitat habitat2 = galaxy_0.Habitats[n];
                                if (habitat2 != null && habitat2.Category == HabitatCategoryType.Planet && empire.IsObjectVisibleToThisEmpire(habitat2))
                                {
                                    int num27 = (int)((double)habitat2.OrbitDistance / main_0.double_0);
                                    XnaDrawingHelper.DrawCircle(spriteBatch_0, num25 - num27, num26 - num27, num27 * 2, num27 * 2, main_0.pen_1.Color, (int)main_0.pen_1.Width, 100);
                                }
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
                        if (num30 < galaxy_0.Habitats.Count)
                        {
                            Habitat habitat3 = galaxy_0.Habitats[num30];
                            if (habitat3 != null && habitat3.Category == HabitatCategoryType.Planet)
                            {
                                int num31 = (int)((double)habitat3.OrbitDistance / main_0.double_0);
                                XnaDrawingHelper.DrawCircle(spriteBatch_0, num28 - num31, num29 - num31, num31 * 2, num31 * 2, main_0.pen_1.Color, (int)main_0.pen_1.Width, 100);
                            }
                        }
                    }
                }
                if (main_0.double_0 < 500.0)
                {
                    double num32 = main_0.CalculatePlanetZoomFactor(main_0.double_0);
                    double num33 = main_0.CalculateMoonZoomFactor(main_0.double_0);
                    for (int num34 = main_0.int_28; num34 <= main_0.int_29; num34++)
                    {
                        if ((systemVisibilityStatus == SystemVisibilityStatus.Unexplored && num34 > main_0.int_28 && !flag4) || num34 >= galaxy_0.Habitats.Count)
                        {
                            continue;
                        }
                        Habitat habitat4 = galaxy_0.Habitats[num34];
                        if (habitat4 == null || (flag4 && systemVisibilityStatus == SystemVisibilityStatus.Unexplored && habitat4.Category != 0 && habitat4.Category != HabitatCategoryType.GasCloud && !empire.IsObjectVisibleToThisEmpire(habitat4)))
                        {
                            continue;
                        }
                        double double_ = main_0.double_0;
                        switch (habitat4.Category)
                        {
                            case HabitatCategoryType.Planet:
                                double_ = num32;
                                break;
                            case HabitatCategoryType.Moon:
                                double_ = num33;
                                break;
                        }
                        Texture2D texture2D_ = method_58(num34 - main_0.int_28);
                        lVxRufUsger(texture2D_, habitat4.Diameter, double_);
                        method_59(habitat4, double_, 4, out int_, out int_2);
                        int num35 = 4;
                        if (habitat4.Category == HabitatCategoryType.Asteroid)
                        {
                            num35 = 1;
                        }
                        System.Drawing.Rectangle rectangle6 = method_35((int)habitat4.Xpos, (int)habitat4.Ypos, int_, int_2, double_, num35);
                        int num36 = rectangle6.X;
                        int num37 = rectangle6.Y;
                        int_ = rectangle6.Width;
                        int_2 = rectangle6.Height;
                        int num38 = -20;
                        int num39 = base.Width + 20;
                        int num40 = -20;
                        int num41 = base.Height + 20;
                        if (habitat4.HasRings || habitat4.Category == HabitatCategoryType.Star)
                        {
                            num38 = -500;
                            num39 = base.Width + 500;
                            num40 = -500;
                            num41 = base.Height + 500;
                        }
                        if (num36 + int_ >= num38 && num36 <= num39 && num37 + int_2 >= num40 && num37 <= num41)
                        {
                            Bitmap bitmap = null;
                            if ((habitat4.Category == HabitatCategoryType.Moon || habitat4.Category == HabitatCategoryType.Planet) && list_5.Count > num34 - main_0.int_28 && list_5[num34 - main_0.int_28] != null)
                            {
                                DateTime value = list_7[num34 - main_0.int_28];
                                double num42 = (double)habitat4.OrbitDistance / 100.0;
                                if (habitat4.Category == HabitatCategoryType.Moon)
                                {
                                    num42 = (double)habitat4.OrbitDistance / 10.0;
                                }
                                if (habitat4.Damage > 0f)
                                {
                                    num42 /= 10.0;
                                }
                                if (now.Subtract(value).TotalSeconds > num42)
                                {
                                    if (list_5.Count > num34 - main_0.int_28)
                                    {
                                        if (list_5[num34 - main_0.int_28] != null)
                                        {
                                            method_21(list_5[num34 - main_0.int_28]);
                                        }
                                        list_5[num34 - main_0.int_28] = null;
                                    }
                                    if (list_6.Count > num34 - main_0.int_28)
                                    {
                                        if (list_6[num34 - main_0.int_28] != null)
                                        {
                                            method_22(list_6[num34 - main_0.int_28]);
                                        }
                                        list_6[num34 - main_0.int_28] = null;
                                    }
                                    if (list_8.Count > num34 - main_0.int_28)
                                    {
                                        if (list_8[num34 - main_0.int_28] != null)
                                        {
                                            method_21(list_8[num34 - main_0.int_28]);
                                        }
                                        list_8[num34 - main_0.int_28] = null;
                                    }
                                    if (list_7.Count > num34 - main_0.int_28)
                                    {
                                        list_7[num34 - main_0.int_28] = DateTime.MinValue;
                                    }
                                }
                            }
                            if (habitat4.Category != HabitatCategoryType.GasCloud)
                            {
                                if (list_5.Count <= num34 - main_0.int_28)
                                {
                                    method_59(habitat4, double_, num35, out int_, out int_2);
                                    int num43 = num34 - main_0.int_28 - list_5.Count;
                                    for (int num44 = 0; num44 < num43; num44++)
                                    {
                                        list_5.Add(null);
                                        list_6.Add(null);
                                        list_8.Add(null);
                                        list_7.Add(DateTime.MinValue);
                                    }
                                    if (habitat4.Explosion == null || !habitat4.Explosion.ExplosionWillDestroy)
                                    {
                                        bitmap = null;
                                        bool bool_ = false;
                                        Texture2D item = method_78(habitat4, bool_13: true, out bitmap, out bool_);
                                        list_5.Add(bitmap);
                                        list_8.Add(null);
                                        list_6.Add(item);
                                        if (bool_)
                                        {
                                            double num45 = (double)habitat4.OrbitDistance / 100.0;
                                            if (habitat4.Category == HabitatCategoryType.Moon)
                                            {
                                                num45 = (double)habitat4.OrbitDistance / 10.0;
                                            }
                                            int num46 = (int)(num45 * 1000.0);
                                            int num47 = 200 + Galaxy.Rnd.Next(0, 100);
                                            int num48 = Math.Max(0, num46 - num47);
                                            long ticks = num48 * 10000L;
                                            DateTime item2 = now.Subtract(new TimeSpan(ticks));
                                            list_7.Add(item2);
                                        }
                                        else
                                        {
                                            list_7.Add(now);
                                        }
                                    }
                                }
                                else if (list_5[num34 - main_0.int_28] == null)
                                {
                                    method_59(habitat4, double_, num35, out int_, out int_2);
                                    if (habitat4.Explosion == null || !habitat4.Explosion.ExplosionWillDestroy)
                                    {
                                        bitmap = null;
                                        bool bool_2 = false;
                                        Texture2D value2 = method_78(habitat4, bool_13: false, out bitmap, out bool_2);
                                        if (list_5.Count > num34 - main_0.int_28)
                                        {
                                            if (list_5[num34 - main_0.int_28] != null)
                                            {
                                                method_21(list_5[num34 - main_0.int_28]);
                                            }
                                            list_5[num34 - main_0.int_28] = bitmap;
                                        }
                                        if (list_6.Count > num34 - main_0.int_28)
                                        {
                                            if (list_6[num34 - main_0.int_28] != null)
                                            {
                                                method_22(list_6[num34 - main_0.int_28]);
                                            }
                                            list_6[num34 - main_0.int_28] = value2;
                                        }
                                        if (list_8.Count > num34 - main_0.int_28 && list_8[num34 - main_0.int_28] != null)
                                        {
                                            method_21(list_8[num34 - main_0.int_28]);
                                        }
                                        if (list_7.Count > num34 - main_0.int_28)
                                        {
                                            if (bool_2)
                                            {
                                                double num49 = (double)habitat4.OrbitDistance / 100.0;
                                                if (habitat4.Category == HabitatCategoryType.Moon)
                                                {
                                                    num49 = (double)habitat4.OrbitDistance / 10.0;
                                                }
                                                int num50 = (int)(num49 * 1000.0);
                                                int num51 = 200 + Galaxy.Rnd.Next(0, 100);
                                                int num52 = Math.Max(0, num50 - num51);
                                                long ticks2 = num52 * 10000L;
                                                DateTime value3 = now.Subtract(new TimeSpan(ticks2));
                                                list_7[num34 - main_0.int_28] = value3;
                                            }
                                            else
                                            {
                                                list_7[num34 - main_0.int_28] = now;
                                            }
                                        }
                                    }
                                }
                            }
                            if (habitat4.Category != HabitatCategoryType.GasCloud && (habitat4.Explosion == null || !habitat4.Explosion.ExplosionWillDestroy))
                            {
                                if (habitat4.Category == HabitatCategoryType.Star)
                                {
                                    int_ = habitat4.Diameter;
                                    int_2 = habitat4.Diameter;
                                }
                                else
                                {
                                    bitmap = list_5[num34 - main_0.int_28];
                                    if (bitmap != null && bitmap.PixelFormat != 0)
                                    {
                                        float num53 = (float)bitmap.Width / (float)bitmap.Height;
                                        int_ = (int)((double)habitat4.Diameter * (double)num53);
                                        int_2 = (int)(double)habitat4.Diameter;
                                    }
                                }
                                rectangle6 = method_35((int)habitat4.Xpos, (int)habitat4.Ypos, int_, int_2, double_, num35);
                                num36 = rectangle6.X;
                                num37 = rectangle6.Y;
                                int_ = rectangle6.Width;
                                int_2 = rectangle6.Height;
                            }
                            if (habitat4.Type == HabitatType.BlackHole)
                            {
                                double totalSeconds = currentDateTime.Subtract(dateTime_2).TotalSeconds;
                                this.double_4 += totalSeconds * 0.4;
                                Texture2D texture2D = texture2D_12[0];
                                new System.Drawing.Rectangle(0, 0, 10, 10);
                                new System.Drawing.Rectangle(0, 0, 10, 10);
                                double num54 = this.double_4 / 7.0;
                                System.Drawing.Color color_3 = System.Drawing.Color.DarkGray;
                                System.Drawing.Color color_4 = System.Drawing.Color.DarkGray;
                                System.Drawing.Color color_5 = System.Drawing.Color.DarkGray;
                                switch (habitat4.HabitatIndex % 4)
                                {
                                    case 0:
                                        color_3 = System.Drawing.Color.FromArgb(0, 0, 216);
                                        color_4 = System.Drawing.Color.FromArgb(0, 0, 138);
                                        color_5 = System.Drawing.Color.FromArgb(0, 0, 112);
                                        break;
                                    case 1:
                                        color_3 = System.Drawing.Color.FromArgb(84, 54, 138);
                                        color_4 = System.Drawing.Color.FromArgb(56, 30, 84);
                                        color_5 = System.Drawing.Color.FromArgb(48, 20, 60);
                                        break;
                                    case 2:
                                        color_3 = System.Drawing.Color.FromArgb(54, 32, 84);
                                        color_4 = System.Drawing.Color.FromArgb(38, 24, 60);
                                        color_5 = System.Drawing.Color.FromArgb(32, 20, 48);
                                        break;
                                    case 3:
                                        color_3 = System.Drawing.Color.FromArgb(84, 30, 96);
                                        color_4 = System.Drawing.Color.FromArgb(62, 20, 72);
                                        color_5 = System.Drawing.Color.FromArgb(56, 16, 64);
                                        break;
                                }
                                int num55 = (int)((double)int_ * 0.07);
                                int num56 = (int)((double)int_2 * 0.07);
                                int num57 = num36 + (int_ - num55) / 2;
                                int num58 = num37 + (int_2 - num56) / 2;
                                if (color_15 == System.Drawing.Color.Empty)
                                {
                                    using (new SolidBrush(System.Drawing.Color.Black))
                                    {
                                    }
                                }
                                num55 = (int)((double)int_ * 1.0);
                                num56 = (int)((double)int_2 * 1.0);
                                num57 = num36 + (int_ - num55) / 2;
                                num58 = num37 + (int_2 - num56) / 2;
                                new System.Drawing.Rectangle(0, 0, texture2D.Width, texture2D.Height);
                                new System.Drawing.Rectangle(num57, num58, num55, num56);
                                num54 = this.double_4 / 5.3;
                                method_118(spriteBatch_0, num57, num58, num55, num56, texture2D, currentDateTime, num54, color_5);
                                num55 = (int)((double)int_ * 0.44);
                                num56 = (int)((double)int_2 * 0.44);
                                num57 = num36 + (int_ - num55) / 2;
                                num58 = num37 + (int_2 - num56) / 2;
                                new System.Drawing.Rectangle(0, 0, texture2D.Width, texture2D.Height);
                                new System.Drawing.Rectangle(num57, num58, num55, num56);
                                num54 = this.double_4 / 2.3;
                                method_118(spriteBatch_0, num57, num58, num55, num56, texture2D, currentDateTime, num54, color_4);
                                num55 = (int)((double)int_ * 0.2);
                                num56 = (int)((double)int_2 * 0.2);
                                num57 = num36 + (int_ - num55) / 2;
                                num58 = num37 + (int_2 - num56) / 2;
                                new System.Drawing.Rectangle(0, 0, texture2D.Width, texture2D.Height);
                                new System.Drawing.Rectangle(num57, num58, num55, num56);
                                method_118(spriteBatch_0, num57, num58, num55, num56, texture2D, currentDateTime, this.double_4, color_3);
                                double_5 += totalSeconds * 0.5;
                                num55 = (int)((double)int_ * 0.08);
                                num56 = (int)((double)int_2 * 0.08);
                                num57 = num36 + (int_ - num55) / 2;
                                num58 = num37 + (int_2 - num56) / 2;
                                method_117(spriteBatch_0, num57, num58, num55, num56, texture2D_13, currentDateTime, 15, 0.0, 0.0, System.Drawing.Color.Empty);
                                dateTime_2 = currentDateTime;
                            }
                            else if (habitat4.Category == HabitatCategoryType.GasCloud)
                            {
                                DrawGasCloudToMainXna(spriteBatch_0, bitmap_2, habitat4, num36, num37, int_, int_2);
                            }
                            else if (habitat4.Type != HabitatType.MainSequence && habitat4.Type != HabitatType.RedGiant && habitat4.Type != HabitatType.SuperGiant && habitat4.Type != HabitatType.WhiteDwarf && habitat4.Type != HabitatType.Neutron)
                            {
                                if (habitat4.Type != HabitatType.SuperNova && list_6.Count > num34 - main_0.int_28 && list_6[num34 - main_0.int_28] != null)
                                {
                                    XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num36, num37, int_, int_2), spriteBatch: spriteBatch_0, texture: list_6[num34 - main_0.int_28], rotationAngle: 0f);
                                }
                            }
                            else if (main_0.double_0 < method_60(habitat4.Type))
                            {
                                int num59 = (int)((double)int_ * 0.98);
                                int num60 = (int)((double)int_2 * 0.98);
                                int int_3 = num36 + (int_ - num59) / 2;
                                int int_4 = num37 + (int_2 - num60) / 2;
                                System.Drawing.Color color_6 = method_120(main_0.bitmap_196[habitat4.MapPictureRef]);
                                System.Drawing.Color color3 = method_65(color_6, 1.0);
                                System.Drawing.Color color4 = method_65(color_6, 1.0);
                                System.Drawing.Color baseColor = method_65(color_6, 1.0);
                                color3 = method_64(color_6, 0.99);
                                color4 = method_64(color_6, 0.85);
                                color4 = System.Drawing.Color.FromArgb(128, color4);
                                baseColor = System.Drawing.Color.FromArgb(0, baseColor);
                                double totalSeconds2 = currentDateTime.Subtract(dateTime_2).TotalSeconds;
                                this.double_4 += totalSeconds2 * 0.02;
                                System.Drawing.Rectangle rectangle7 = new System.Drawing.Rectangle(num36, num37, int_, int_2);
                                int num61 = (int)((double)int_ * 0.195);
                                rectangle7.Inflate(num61, num61);
                                double_5 -= totalSeconds2 * 0.02;
                                float num62 = (float)double_5 * -1f;
                                method_65(color_6, 1.0);
                                System.Drawing.Color color_7 = System.Drawing.Color.FromArgb(255, method_65(color_6, 1.2));
                                method_118(spriteBatch_0, int_3, int_4, num59, num60, texture2D_14[2], currentDateTime, num62, color_7);
                                System.Drawing.Color color_8 = System.Drawing.Color.FromArgb(96, method_65(color_6, 1.0));
                                method_118(spriteBatch_0, int_3, int_4, num59, num60, texture2D_14[0], currentDateTime, double_5, color_8);
                                double num63 = 1.65;
                                Texture2D[] texture2D_2 = texture2D_15;
                                int int_5 = 15;
                                if (habitat4.Type == HabitatType.Neutron)
                                {
                                    num63 = 2.3;
                                    texture2D_2 = texture2D_16;
                                    int_5 = 12;
                                }
                                int num64 = (int)((double)int_ * num63);
                                int num65 = (int)((double)int_2 * num63);
                                int num66 = (num64 - int_) / 2;
                                int num67 = (num65 - int_2) / 2;
                                int int_6 = num36 - num66;
                                int int_7 = num37 - num67;
                                System.Drawing.Color color5 = color3;
                                color5 = method_64(color_6, -0.15);
                                color5 = System.Drawing.Color.FromArgb(192, color5);
                                color5 = System.Drawing.Color.FromArgb(160, method_64(color_6, 0.3));
                                color5 = System.Drawing.Color.FromArgb(240, method_65(color_6, 1.2));
                                method_117(spriteBatch_0, int_6, int_7, num64, num65, texture2D_2, currentDateTime, int_5, 0.0, 0.0, color5);
                            }
                            else if (list_6.Count > num34 - main_0.int_28 && list_6[num34 - main_0.int_28] != null)
                            {
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, list_6[num34 - main_0.int_28], num36, num37, int_, int_2, 0f);
                            }
                            if (habitat4.IsBlockaded)
                            {
                                System.Drawing.Rectangle rectangle8 = method_81(num36 - 14, num37 - 14, int_ + 28, int_2 + 28);
                                method_99(spriteBatch_0, rectangle8.X, rectangle8.Y, rectangle8.Width, rectangle8.Height);
                            }
                            if (main_0.int_34 < 2 && main_0.double_0 < 100.0 && !habitat4.HasBeenDestroyed)
                            {
                                int num68 = 28;
                                num68 = (int)(28.0 / main_0.double_0);
                                num68 = Math.Max(6, num68);
                                int num69 = num68 * 2;
                                if (habitat4.Owner != null && habitat4.Owner != main_0._Game.Galaxy.IndependentEmpire)
                                {
                                    System.Drawing.Color color6 = habitat4.Owner.MainColor;
                                    if (habitat4.Owner.PirateEmpireBaseHabitat != null && habitat4.Owner.MainColor == System.Drawing.Color.FromArgb(1, 1, 1))
                                    {
                                        color6 = System.Drawing.Color.FromArgb(48, 48, 48);
                                    }
                                    System.Drawing.Rectangle area2 = method_81(num36 - num68, num37 - num68, int_ + num69, int_2 + num69);
                                    XnaDrawingHelper.DrawCircle(spriteBatch_0, area2, 60, color6, 2);
                                }
                                else if (habitat4.Owner == main_0._Game.Galaxy.IndependentEmpire && habitat4.Population != null && habitat4.Population.Count > 0)
                                {
                                    System.Drawing.Rectangle area3 = method_81(num36 - num68, num37 - num68, int_ + num69, int_2 + num69);
                                    XnaDrawingHelper.DrawCircle(spriteBatch_0, area3, 60, color_1, 2);
                                }
                                else if (habitat4.Category == HabitatCategoryType.Planet)
                                {
                                    int val2 = (int)(48.0 * ((main_0.double_0 - 5.0) / 100.0));
                                    val2 = Math.Max(0, Math.Min(val2, 255));
                                    XnaDrawingHelper.DrawCircle(area: new System.Drawing.Rectangle(num36 - num68, num37 - num68, int_ + num69, int_2 + num69), spriteBatch: spriteBatch_0, sideCount: 60, color: System.Drawing.Color.FromArgb(val2, 255, 255, 255), lineThickness: 2);
                                }
                            }
                            if (main_0.double_0 > 1.1)
                            {
                            }
                            if (habitat4 == main_0._Game.SelectedObject)
                            {
                                method_212(spriteBatch_0, num36 - 10, num37 - 10, num36 + int_ + 10, num37 + int_2 + 10);
                            }
                            if (habitat_0 != null && habitat4 == habitat_0 && !bool_4 && !this.bool_2 && !bool_1 && !bool_3)
                            {
                                method_197(rectangle_5: new System.Drawing.Rectangle(num36 - 10, num37 - (10 + (int_ - int_2) / 2), int_ + 20, int_2 + 20), spriteBatch_2: spriteBatch_0);
                            }
                            method_92(habitat4, num36 + int_ / 2, num37 + int_2 / 2, bool_13: true);
                        }
                        else if ((num36 + int_ < -800 || num36 > base.Width + 800 || num37 + int_2 < -800 || num37 > base.Height + 800) && list_5.Count > num34 - main_0.int_28)
                        {
                            list_5[num34 - main_0.int_28] = null;
                            list_6[num34 - main_0.int_28] = null;
                            list_8[num34 - main_0.int_28] = null;
                            list_7[num34 - main_0.int_28] = DateTime.MinValue;
                        }
                        ref System.Drawing.Rectangle reference = ref rectangle_0[num34 - main_0.int_28];
                        reference = new System.Drawing.Rectangle(num36 - 2, num37 - 2, int_ + 4, int_2 + 4);
                        method_173(spriteBatch_0, habitat4, num36, num37, int_, int_2, currentDateTime);
                        method_169(spriteBatch_0, habitat4, currentDateTime, num36 + int_ / 2, num37 + int_2 / 2);
                        if (habitat4.Explosions != null && habitat4.Explosions.Count > 0)
                        {
                            method_180(spriteBatch_0, habitat4, double_);
                        }
                        if (habitat4.Explosion != null)
                        {
                            method_187(spriteBatch_0, habitat4, double_);
                        }
                    }
                }
            }
            animationSystem_0.DoAnimationsXna(spriteBatch_0, currentDateTime);
            if (main_0.double_0 < 500.0)
            {
                double maxWidth = 0.0;
                double num70 = main_0.CalculateShipZoomFactor(main_0.double_0, out maxWidth);
                BuiltObjectList builtObjectList = new BuiltObjectList();
                FighterList fighterList = new FighterList();
                _ = MouseHelper.GetCursorPosition().X;
                _ = MouseHelper.GetCursorPosition().Y;
                for (int num71 = 0; num71 < builtObjectsAtLocation.Count; num71++)
                {
                    BuiltObject builtObject5 = builtObjectsAtLocation[num71];
                    if (builtObject5 == null)
                    {
                        continue;
                    }
                    BuiltObjectImageData builtObjectImageData = main_0.builtObjectImageCache_0.FastGetImageData(builtObject5.PictureRef);
                    if (builtObjectImageData == null || builtObjectImageData.Image == null)
                    {
                        continue;
                    }
                    int num72 = builtObjectImageData.Image.Width;
                    int num73 = builtObjectImageData.Image.Height;
                    num72 = (int)((double)num72 / num70);
                    num73 = (int)((double)num73 / num70);
                    num72 = Math.Min(num72, (int)maxWidth);
                    num73 = Math.Min(num73, (int)maxWidth);
                    int num74 = (int)((double)((int)builtObject5.Xpos - num) / main_0.double_0) - num72 / 2 + base.Width / 2;
                    int num75 = (int)((double)((int)builtObject5.Ypos - num2) / main_0.double_0) - num73 / 2 + base.Height / 2;
                    if (num74 + num72 < -50 || num74 - num72 > base.Width + 50 || num75 + num73 < -50 || num75 - num73 > base.Height + 50 || (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(builtObject5)))
                    {
                        continue;
                    }
                    builtObjectList.Add(builtObject5);
                    bool flag5 = false;
                    if (builtObject5.TargetSpeedChanged)
                    {
                        flag5 = true;
                        builtObject5.TargetSpeedChanged = false;
                    }
                    Bitmap bitmap2 = null;
                    Bitmap bitmap3 = null;
                    int num76 = list_14.IndexOf(builtObject5.BuiltObjectID);
                    if (num76 >= 0)
                    {
                        if (list_15[num76] == BuiltObjectImageSize.Small && builtObjectImageData.ImageSize == BuiltObjectImageSize.Fullsize)
                        {
                            bitmap2 = ((builtObject5.Empire == null) ? main_0.PrepareBuiltObjectImageNEW(builtObject5, builtObjectImageData, System.Drawing.Color.Gray, System.Drawing.Color.Gray, allowPreRotate: false, 1.0) : main_0.PrepareBuiltObjectImageNEW(builtObject5, builtObjectImageData, builtObject5.Empire.MainColor, builtObject5.Empire.SecondaryColor, allowPreRotate: false, 1.0));
                            list_9[num76] = bitmap2;
                        }
                        list_15[num76] = builtObjectImageData.ImageSize;
                        if (method_71(builtObject5))
                        {
                            if (list_10[num76] != null)
                            {
                                method_21(list_10[num76]);
                            }
                            list_10[num76] = method_73(builtObject5, list_9[num76], builtObjectImageData.Image.Size, builtObjectImageData.MaskImage, builtObjectImageData.LightPoints, bool_13: false);
                            if (list_11[num76] != null)
                            {
                                method_22(list_11[num76]);
                            }
                            list_11[num76] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, list_10[num76]);
                        }
                        bitmap2 = list_10[num76];
                        _ = list_9[num76];
                        bitmap3 = list_12[num76];
                        if (flag5)
                        {
                            bitmap3 = main_0.PrepareEngineExhaust(builtObject5, list_9[num76]);
                            if (list_12[num76] != null)
                            {
                                method_21(list_12[num76]);
                            }
                            list_12[num76] = bitmap3;
                            if (list_13[num76] != null)
                            {
                                method_22(list_13[num76]);
                            }
                            list_13[num76] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap3);
                            flag5 = false;
                        }
                    }
                    else
                    {
                        if (builtObject5.Empire != null)
                        {
                            bitmap2 = main_0.PrepareBuiltObjectImageNEW(builtObject5, builtObjectImageData, builtObject5.Empire.MainColor, builtObject5.Empire.SecondaryColor, allowPreRotate: false, 1.0);
                            bitmap3 = main_0.PrepareEngineExhaust(builtObject5, bitmap2);
                        }
                        else
                        {
                            bitmap2 = main_0.PrepareBuiltObjectImageNEW(builtObject5, builtObjectImageData, System.Drawing.Color.Gray, System.Drawing.Color.Gray, allowPreRotate: false, 1.0);
                            bitmap3 = main_0.PrepareEngineExhaust(builtObject5, bitmap2);
                        }
                        list_14.Add(builtObject5.BuiltObjectID);
                        list_9.Add(bitmap2);
                        bitmap2 = method_73(builtObject5, bitmap2, builtObjectImageData.Image.Size, builtObjectImageData.MaskImage, builtObjectImageData.LightPoints, bool_13: false);
                        list_10.Add(bitmap2);
                        list_11.Add(XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap2));
                        list_12.Add(bitmap3);
                        list_13.Add(XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap3));
                        list_15.Add(builtObjectImageData.ImageSize);
                    }
                    num72 = bitmap2.Width;
                    num73 = bitmap2.Height;
                    num72 = Math.Min(num72, (int)maxWidth);
                    num73 = Math.Min(num73, (int)maxWidth);
                    num74 = (int)((double)((int)builtObject5.Xpos - num) / main_0.double_0) - num72 / 2 + base.Width / 2;
                    num75 = (int)((double)((int)builtObject5.Ypos - num2) / main_0.double_0) - num73 / 2 + base.Height / 2;
                    if (num74 + num72 >= -50 && num74 - num72 <= base.Width + 50 && num75 + num73 >= -50 && num75 - num73 <= base.Height + 50)
                    {
                    }
                }
                for (int num77 = 0; num77 < builtObjectList.Count; num77++)
                {
                    BuiltObject builtObject6 = builtObjectList[num77];
                    if (builtObject6 == null)
                    {
                        continue;
                    }
                    BuiltObjectImageData builtObjectImageData2 = main_0.builtObjectImageCache_0.FastGetImageData(builtObject6.PictureRef);
                    if (builtObjectImageData2 == null)
                    {
                        continue;
                    }
                    Bitmap bitmap4 = null;
                    Bitmap bitmap5 = null;
                    Bitmap bitmap6 = null;
                    Texture2D texture2D2 = null;
                    Texture2D texture2D3 = null;
                    int num78 = list_14.IndexOf(builtObject6.BuiltObjectID);
                    if (num78 >= 0)
                    {
                        bool flag6 = false;
                        if (list_15[num78] == BuiltObjectImageSize.Small && builtObjectImageData2.ImageSize == BuiltObjectImageSize.Fullsize)
                        {
                            bitmap4 = ((builtObject6.Empire == null) ? main_0.PrepareBuiltObjectImageNEW(builtObject6, builtObjectImageData2, System.Drawing.Color.Gray, System.Drawing.Color.Gray, allowPreRotate: false, 1.0) : main_0.PrepareBuiltObjectImageNEW(builtObject6, builtObjectImageData2, builtObject6.Empire.MainColor, builtObject6.Empire.SecondaryColor, allowPreRotate: false, 1.0));
                            list_9[num78] = bitmap4;
                            flag6 = true;
                        }
                        list_15[num78] = builtObjectImageData2.ImageSize;
                        if (flag6 || method_71(builtObject6))
                        {
                            if (list_10[num78] != null)
                            {
                                method_21(list_10[num78]);
                            }
                            list_10[num78] = method_73(builtObject6, list_9[num78], builtObjectImageData2.Image.Size, builtObjectImageData2.MaskImage, builtObjectImageData2.LightPoints, bool_13: false);
                            if (list_11[num78] != null)
                            {
                                method_22(list_11[num78]);
                            }
                            list_11[num78] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, list_10[num78]);
                        }
                        bitmap4 = list_10[num78];
                        texture2D2 = list_11[num78];
                        bitmap5 = list_9[num78];
                        bitmap6 = list_12[num78];
                        texture2D3 = list_13[num78];
                    }
                    else
                    {
                        bitmap4 = main_0.PrepareBuiltObjectImageNEW(builtObject6, builtObjectImageData2, builtObject6.Empire.MainColor, builtObject6.Empire.SecondaryColor, allowPreRotate: false, 1.0);
                        bitmap6 = main_0.PrepareEngineExhaust(builtObject6, bitmap4);
                        list_14.Add(builtObject6.BuiltObjectID);
                        list_9.Add(bitmap4);
                        bitmap5 = bitmap4;
                        bitmap4 = method_73(builtObject6, bitmap4, builtObjectImageData2.Image.Size, builtObjectImageData2.MaskImage, builtObjectImageData2.LightPoints, bool_13: false);
                        texture2D2 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap4);
                        texture2D3 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap6);
                        list_10.Add(bitmap4);
                        list_11.Add(texture2D2);
                        list_12.Add(bitmap6);
                        list_13.Add(texture2D3);
                        list_15.Add(builtObjectImageData2.ImageSize);
                    }
                    int val3 = bitmap4.Width;
                    int val4 = bitmap4.Height;
                    val3 = Math.Min(val3, (int)maxWidth);
                    val4 = Math.Min(val4, (int)maxWidth);
                    int num79 = (int)((double)((int)builtObject6.Xpos - num) / main_0.double_0) - val3 / 2 + base.Width / 2;
                    int num80 = (int)((double)((int)builtObject6.Ypos - num2) / main_0.double_0) - val4 / 2 + base.Height / 2;
                    if (num79 + val3 < -50 || num79 - val3 > base.Width + 50 || num80 + val4 < -50 || num80 - val4 > base.Height + 50)
                    {
                        continue;
                    }
                    int num81 = bitmap5.Width;
                    int num82 = bitmap5.Height;
                    int num83 = num79 + (val3 - num81) / 2;
                    int num84 = num80 + (val4 - num82) / 2;
                    Size size = method_77(builtObject6, builtObjectImageData2, main_0.double_0);
                    num83 = (int)((double)((int)builtObject6.Xpos - num) / main_0.double_0) - num81 / 2 + base.Width / 2;
                    num84 = (int)((double)((int)builtObject6.Ypos - num2) / main_0.double_0) - num82 / 2 + base.Height / 2;
                    num81 = size.Width;
                    num82 = size.Height;
                    double totalSeconds3 = currentDateTime.TimeOfDay.TotalSeconds;
                    double num85 = (double)(builtObject6.BuiltObjectID % 20) / 10.0;
                    totalSeconds3 += num85;
                    double num86 = totalSeconds3 % (double_7 + double_8);
                    if (num86 < double_7)
                    {
                        if (!builtObject6.LightsOn)
                        {
                            builtObject6.LightsOn = true;
                            builtObject6.LightChanged = true;
                        }
                    }
                    else if (builtObject6.LightsOn)
                    {
                        builtObject6.LightsOn = false;
                        builtObject6.LightChanged = true;
                    }
                    int num87 = (int)((double)num81 * 1.3);
                    int num88 = (int)((double)num82 * 1.3);
                    val3 = num81;
                    val4 = num82;
                    val3 = Math.Min(val3, (int)maxWidth);
                    val4 = Math.Min(val4, (int)maxWidth);
                    int num89 = (int)((double)((int)builtObject6.Xpos - num) / main_0.double_0) - val3 / 2 + base.Width / 2;
                    int num90 = (int)((double)((int)builtObject6.Ypos - num2) / main_0.double_0) - val4 / 2 + base.Height / 2;
                    int num91 = num89 - (num87 - val3) / 2;
                    int num92 = num90 - (num88 - val4) / 2;
                    if (!builtObject6.HasBeenDestroyed)
                    {
                        if (main_0.int_34 < 2 && builtObject6.Empire != null)
                        {
                            bool flag7 = true;
                            if (main_0.double_0 > 20.0)
                            {
                                if (builtObject6.Role != BuiltObjectRole.Base && builtObject6.Owner == null && empire2 == empire)
                                {
                                    flag7 = false;
                                }
                            }
                            else if (main_0.double_0 > 35.0 && builtObject6.Role != BuiltObjectRole.Base && empire2 == empire)
                            {
                                flag7 = false;
                            }
                            if (flag7)
                            {
                                int num93 = 8;
                                if (builtObject6.Role == BuiltObjectRole.Base)
                                {
                                    num93 = 10;
                                }
                                int num94 = Math.Max(num93, num81);
                                int num95 = Math.Max(num93, num82);
                                Math.Max(0, num93 - num81);
                                Math.Max(0, num93 - num82);
                                int num96 = num89 - (num94 - val3) / 2;
                                int num97 = num90 - (num95 - val4) / 2;
                                int num98 = Math.Max(num93, num81);
                                int num99 = Math.Max(num93, num82);
                                System.Drawing.Color color7 = ResolveShipSymbolColor(builtObject6);
                                if (builtObject6.Role != BuiltObjectRole.Base && builtObject6.Empire != galaxy_0.IndependentEmpire)
                                {
                                    color7 = System.Drawing.Color.FromArgb(Math.Min(255, color7.A + 48), color7.R, color7.G, color7.B);
                                }
                                bool galaxyLevel = false;
                                if (main_0.double_0 > 10.0)
                                {
                                    galaxyLevel = true;
                                }
                                DrawShipSymbolXna(spriteBatch_0, builtObject6, color7, num96, num97, num94, num95, num98, num99, galaxyLevel, currentDateTime);
                            }
                        }
                        System.Drawing.Rectangle destination4 = new System.Drawing.Rectangle(num89, num90, num81, num82);
                        double num100 = (double)texture2D2.Width / (double)list_9[num78].Width;
                        int num101 = (int)((double)(float)list_9[num78].Width * (num100 - 1.0) / 2.0);
                        int num102 = (int)((double)(float)list_9[num78].Height * (num100 - 1.0) / 2.0);
                        destination4.Inflate(num101, num102);
                        if (texture2D3 != null && builtObject6.TargetSpeed > 0)
                        {
                            float rotationAngle = builtObject6.Heading;
                            float num103 = (float)texture2D3.Width / (float)list_9[num78].Width;
                            int num104 = (int)((float)num81 * num103);
                            int num105 = (int)((float)num82 * num103);
                            int num106 = num89 - (num104 - num81) / 2;
                            int num107 = num90 - (num105 - num82) / 2;
                            XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num106, num107, num104, num105), spriteBatch: spriteBatch_0, texture: texture2D3, rotationAngle: rotationAngle);
                        }
                        DrawBuiltObjectToMainXna(spriteBatch_0, texture2D2, builtObject6, destination4, fadeCivilianShips);
                        if (builtObject6.IsBlockaded)
                        {
                            method_99(spriteBatch_0, destination4.X, destination4.Y, destination4.Width, destination4.Height);
                        }
                        if (main_0._Game.SelectedObject is ShipGroup)
                        {
                            ShipGroup shipGroup = (ShipGroup)main_0._Game.SelectedObject;
                            if (builtObject6.ShipGroup == shipGroup)
                            {
                                method_212(spriteBatch_0, num91, num92, num91 + num87, num92 + num88);
                            }
                        }
                        else if (main_0._Game.SelectedObject is BuiltObjectList)
                        {
                            BuiltObjectList builtObjectList2 = (BuiltObjectList)main_0._Game.SelectedObject;
                            if (builtObjectList2.Contains(builtObject6))
                            {
                                method_212(spriteBatch_0, num91, num92, num91 + num87, num92 + num88);
                            }
                        }
                        else if (builtObject6 == main_0._Game.SelectedObject)
                        {
                            method_212(spriteBatch_0, num91, num92, num91 + num87, num92 + num88);
                        }
                        if (builtObject6.LastIonStrike > DateTime.MinValue)
                        {
                            TimeSpan timeSpan = currentDateTime.Subtract(builtObject6.LastIonStrike);
                            if (timeSpan.TotalMilliseconds < 1400.0)
                            {
                                if (!builtObject6.IonStrikeSoundPlayed)
                                {
                                    builtObject6.IonStrikeSoundPlayed = true;
                                    double double_2 = 0.0;
                                    double double_3 = 0.0;
                                    method_90(num89, num90, main_0.double_0, out double_2, out double_3);
                                    main_0.method_0(main_0.EffectsPlayer.ResolveIonStrike(double_2, double_3));
                                }
                                int num108 = (int)((double)num81 * 1.4);
                                int num109 = currentDateTime.Second / 3;
                                Bitmap bitmap7 = lightningGenerator_0.GenerateLightning(builtObject6.BuiltObjectID + num109, num108);
                                Texture2D texture2D4 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap7);
                                int num110 = num89 - (num108 - num81) / 2;
                                int num111 = num90 - (num108 - num82) / 2;
                                System.Drawing.Rectangle destination6 = new System.Drawing.Rectangle(num110, num111, num108, num108);
                                new System.Drawing.Rectangle(0, 0, bitmap7.Width, bitmap7.Height);
                                double num112 = 1.0 - timeSpan.TotalMilliseconds % 250.0 / 250.0 * 0.95;
                                System.Drawing.Color white = System.Drawing.Color.White;
                                if (num112 < 1.0)
                                {
                                    white = System.Drawing.Color.FromArgb((int)(num112 * 255.0), 255, 255, 255);
                                    XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D4, destination6, 0f, white);
                                }
                                else
                                {
                                    XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D4, destination6, 0f, white);
                                }
                                method_21(bitmap7);
                                method_22(texture2D4);
                            }
                            else
                            {
                                builtObject6.LastIonStrike = DateTime.MinValue;
                            }
                        }
                        if (builtObject6.ShieldAreaRechargeStartTime > DateTime.MinValue && builtObject6.ShieldAreaRechargeTarget != null)
                        {
                            int int_8 = num83 + num81 / 2;
                            int int_9 = num84 + num82 / 2;
                            int int_10 = (int)((double)((int)builtObject6.ShieldAreaRechargeTarget.Xpos - num) / main_0.double_0) + base.Width / 2;
                            int int_11 = (int)((double)((int)builtObject6.ShieldAreaRechargeTarget.Ypos - num2) / main_0.double_0) + base.Height / 2;
                            method_94(spriteBatch_0, currentDateTime, builtObject6, int_8, int_9, num81 / 2, int_10, int_11);
                            if (builtObject6.ShieldAreaRechargeStartTime.Subtract(currentDateTime).TotalSeconds > 3.0)
                            {
                                builtObject6.ShieldAreaRechargeStartTime = DateTime.MinValue;
                                builtObject6.ShieldAreaRechargeTarget = null;
                            }
                        }
                        if (builtObject6.LastShieldStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(builtObject6.LastShieldStrike).TotalMilliseconds < 200.0 && (double)builtObject6.LastShieldStrikeDirection > double.MinValue)
                            {
                                double num113 = 1.0;
                                int num114 = (int)(num113 * (double)num81);
                                int num115 = (int)(num113 * (double)num82);
                                int num116 = num89 - (num114 - num81) / 2;
                                int num117 = num90 - (num115 - num82) / 2;
                                System.Drawing.Rectangle destination7 = new System.Drawing.Rectangle(num116, num117, num114, num115);
                                float rotationAngle2 = (float)((double)builtObject6.LastShieldStrikeDirection - Math.PI / 2.0);
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_6, destination7, rotationAngle2);
                            }
                            else
                            {
                                builtObject6.LastShieldStrike = DateTime.MinValue;
                            }
                        }
                        if (builtObject6.LastTractorStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(builtObject6.LastTractorStrike).TotalMilliseconds < 2000.0 && (double)builtObject6.LastTractorStrikeDirection > double.MinValue)
                            {
                                double num118 = 1.0;
                                int num119 = (int)(num118 * (double)num81);
                                int num120 = (int)(num118 * (double)num82);
                                int int_12 = num89 - (num119 - num81) / 2;
                                int int_13 = num90 - (num120 - num82) / 2;
                                new System.Drawing.Rectangle(int_12, int_13, num119, num120);
                                float num121 = (float)((double)builtObject6.LastTractorStrikeDirection - Math.PI / 2.0);
                                method_117(spriteBatch_0, int_12, int_13, num119, num120, texture2D_34, currentDateTime, 10, num121, 0.0, System.Drawing.Color.White);
                            }
                            else
                            {
                                builtObject6.LastTractorStrike = DateTime.MinValue;
                            }
                        }
                        if (main_0.double_0 <= 3.0)
                        {
                            if (main_0.int_34 < 1 && builtObject6.InBattle)
                            {
                                if (builtObject6.ShieldsCapacity > 0)
                                {
                                    int int_14 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - bitmap5.Width / 2 + base.Width / 2;
                                    int num122 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - bitmap5.Height / 2 + base.Height / 2;
                                    method_194(spriteBatch_0, int_14, num122 - 8, bitmap5.Width, builtObject6.ShieldsCapacity, (int)builtObject6.CurrentShields);
                                }
                                if (builtObject6.AssaultDefenseValue > 0 || builtObject6.AssaultAttackValue > 0)
                                {
                                    if (builtObject6.AssaultAttackValue > 0)
                                    {
                                        int int_15 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - bitmap5.Width / 2 + base.Width / 2;
                                        int num123 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - bitmap5.Height / 2 + base.Height / 2;
                                        int assaultDefenseValueFixed = builtObject6.AssaultDefenseValueFixed;
                                        int int_16 = Math.Max(0, builtObject6.AssaultDefenseValue - assaultDefenseValueFixed);
                                        method_195(spriteBatch_0, int_15, num123 - 4, bitmap5.Width, builtObject6.AssaultDefenseValueDefault, assaultDefenseValueFixed, int_16, builtObject6.AssaultAttackValue);
                                    }
                                    else if (builtObject6.AssaultDefenseValueDefault > 0)
                                    {
                                        Math.Min(1f, Math.Max(0f, (float)builtObject6.AssaultDefenseValue / (float)builtObject6.AssaultDefenseValueDefault));
                                        int int_17 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - bitmap5.Width / 2 + base.Width / 2;
                                        int num124 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - bitmap5.Height / 2 + base.Height / 2;
                                        int assaultDefenseValueFixed2 = builtObject6.AssaultDefenseValueFixed;
                                        int int_18 = Math.Max(0, builtObject6.AssaultDefenseValue - assaultDefenseValueFixed2);
                                        method_195(spriteBatch_0, int_17, num124 - 4, bitmap5.Width, builtObject6.AssaultDefenseValueDefault, assaultDefenseValueFixed2, int_18, builtObject6.AssaultAttackValue);
                                    }
                                }
                                if (builtObject6.AssaultAttackValue > 0 && texture2D_25 != null)
                                {
                                    int num125 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - texture2D_25.Width / 2 + base.Width / 2;
                                    int num126 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - texture2D_25.Height / 2 + base.Height / 2;
                                    System.Drawing.Rectangle destination8 = new System.Drawing.Rectangle(num125, num126, texture2D_25.Width, texture2D_25.Height);
                                    System.Drawing.Color tintColor = method_214(System.Drawing.Color.FromArgb(255, 0, 0), System.Drawing.Color.FromArgb(255, 255, 0), currentDateTime);
                                    XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_25, destination8, 0f, tintColor);
                                }
                            }
                            if (main_0.int_34 < 2 && builtObject6.ShipGroup != null && builtObject6.ShipGroup.LeadShip == builtObject6)
                            {
                                int num127 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - bitmap5.Width / 2 + base.Width / 2;
                                int num128 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - bitmap5.Height / 2 + base.Height / 2;
                                method_191(spriteBatch_0, num127 + (int)(double)bitmap5.Width + 2 - main_0.bitmap_43.Width, num128 - 2);
                            }
                        }
                        if ((builtObject_0 != null && builtObject6 == builtObject_0) || (shipGroup_0 != null && builtObject6.ShipGroup == shipGroup_0))
                        {
                            method_203(rectangle_5: new System.Drawing.Rectangle(num91, num92, num87, num88), spriteBatch_2: spriteBatch_0);
                        }
                    }
                    method_97(builtObject6, num79, num80, currentStarDate, bool_13: true);
                    method_95(builtObject6, num79, num80, bool_13: true);
                    if (builtObject6.Explosions.Count > 0)
                    {
                        method_183(spriteBatch_0, builtObject6, num70);
                    }
                    if (builtObject6.Weapons.Count > 0)
                    {
                        method_167(spriteBatch_0, builtObject6, currentDateTime, num89, num90);
                    }
                }
                animationSystem_1.DoAnimationsXna(spriteBatch_0, currentDateTime);
                for (int num129 = 0; num129 < fightersForBuiltObjects.Count; num129++)
                {
                    Fighter fighter = fightersForBuiltObjects[num129];
                    if (fighter == null)
                    {
                        continue;
                    }
                    if (fighter.PictureRef < 0 || fighter.PictureRef > main_0.bitmap_6.Length)
                    {
                        fighter.PictureRef = 0;
                        if (fighter.Empire != null && fighter.Empire.DominantRace != null)
                        {
                            fighter.PictureRef = (short)ShipImageHelper.ResolveNewFighterImageIndex(fighter.Empire.DominantRace, isPirates: false);
                        }
                    }
                    int num130 = main_0.bitmap_6[fighter.PictureRef].Width;
                    int num131 = main_0.bitmap_6[fighter.PictureRef].Height;
                    num130 = (int)((double)num130 / num70);
                    num131 = (int)((double)num131 / num70);
                    num130 = Math.Min(num130, (int)maxWidth);
                    num131 = Math.Min(num131, (int)maxWidth);
                    int num132 = (int)((double)((int)fighter.Xpos - num) / main_0.double_0) - num130 / 2 + base.Width / 2;
                    int num133 = (int)((double)((int)fighter.Ypos - num2) / main_0.double_0) - num131 / 2 + base.Height / 2;
                    if (num132 + num130 < -50 || num132 - num130 > base.Width + 50 || num133 + num131 < -50 || num133 - num131 > base.Height + 50 || (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(fighter)) || fighter.OnboardCarrier)
                    {
                        continue;
                    }
                    fighterList.Add(fighter);
                    bool flag8 = false;
                    if (fighter.TargetSpeedChanged)
                    {
                        flag8 = true;
                        fighter.TargetSpeedChanged = false;
                    }
                    Bitmap bitmap8 = null;
                    Bitmap bitmap9 = null;
                    Texture2D texture2D5 = null;
                    Texture2D texture2D6 = null;
                    int num134 = list_21.IndexOf(fighter.FighterID);
                    if (num134 >= 0)
                    {
                        if (method_68(fighter))
                        {
                            if (list_17[num134] != null)
                            {
                                method_21(list_17[num134]);
                            }
                            list_17[num134] = method_70(fighter, list_16[num134], bool_13: false);
                            if (list_18[num134] != null)
                            {
                                method_22(list_18[num134]);
                            }
                            list_18[num134] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, list_17[num134]);
                        }
                        bitmap8 = list_17[num134];
                        texture2D5 = list_18[num134];
                        _ = list_16[num134];
                        bitmap9 = list_19[num134];
                        texture2D6 = list_20[num134];
                        if (flag8)
                        {
                            bitmap9 = main_0.PrepareEngineExhaust(fighter, list_16[num134]);
                            if (list_19[num134] != null)
                            {
                                method_21(list_19[num134]);
                            }
                            list_19[num134] = bitmap9;
                            if (list_20[num134] != null)
                            {
                                method_22(list_20[num134]);
                            }
                            texture2D6 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap9);
                            list_20[num134] = texture2D6;
                            flag8 = false;
                        }
                    }
                    else
                    {
                        double size2 = (double)main_0.int_5[fighter.PictureRef] / Galaxy.BuiltObjectDrawResizeFactor;
                        if (fighter.Empire != null)
                        {
                            bitmap8 = main_0.PrepareFighterImage(fighter, main_0.bitmap_6[fighter.PictureRef], fighter.Empire.MainColor, fighter.Empire.SecondaryColor, size2, fighter.Size, main_0.double_0);
                            bitmap9 = main_0.PrepareEngineExhaust(fighter, bitmap8);
                        }
                        else
                        {
                            bitmap8 = main_0.PrepareFighterImage(fighter, main_0.bitmap_6[fighter.PictureRef], System.Drawing.Color.Gray, System.Drawing.Color.Gray, size2, fighter.Size, main_0.double_0);
                            bitmap9 = main_0.PrepareEngineExhaust(fighter, bitmap8);
                        }
                        list_21.Add(fighter.FighterID);
                        list_16.Add(bitmap8);
                        bitmap8 = method_70(fighter, bitmap8, bool_13: false);
                        list_17.Add(bitmap8);
                        list_19.Add(bitmap9);
                        texture2D5 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap8);
                        texture2D6 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap9);
                        list_18.Add(texture2D5);
                        list_20.Add(texture2D6);
                    }
                    num130 = bitmap8.Width;
                    num131 = bitmap8.Height;
                    num130 = Math.Min(num130, (int)maxWidth);
                    num131 = Math.Min(num131, (int)maxWidth);
                    num132 = (int)((double)((int)fighter.Xpos - num) / main_0.double_0) - num130 / 2 + base.Width / 2;
                    num133 = (int)((double)((int)fighter.Ypos - num2) / main_0.double_0) - num131 / 2 + base.Height / 2;
                    if (num132 + num130 >= -50 && num132 - num130 <= base.Width + 50 && num133 + num131 >= -50 && num133 - num131 <= base.Height + 50)
                    {
                    }
                }
                for (int num135 = 0; num135 < fighterList.Count; num135++)
                {
                    Fighter fighter2 = fighterList[num135];
                    if (fighter2 == null)
                    {
                        continue;
                    }
                    Bitmap bitmap10 = null;
                    Bitmap bitmap11 = null;
                    Bitmap bitmap12 = null;
                    Texture2D texture2D7 = null;
                    Texture2D texture2D8 = null;
                    int num136 = list_21.IndexOf(fighter2.FighterID);
                    if (num136 >= 0)
                    {
                        if (method_68(fighter2))
                        {
                            if (list_17[num136] != null)
                            {
                                method_21(list_17[num136]);
                            }
                            list_17[num136] = method_70(fighter2, list_16[num136], bool_13: false);
                            if (list_18[num136] != null)
                            {
                                method_22(list_18[num136]);
                            }
                            list_18[num136] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, list_17[num136]);
                        }
                        bitmap10 = list_17[num136];
                        texture2D7 = list_18[num136];
                        bitmap11 = list_16[num136];
                        bitmap12 = list_19[num136];
                        texture2D8 = list_20[num136];
                    }
                    else
                    {
                        double size3 = (double)main_0.int_5[fighter2.PictureRef] / Galaxy.BuiltObjectDrawResizeFactor;
                        bitmap10 = main_0.PrepareFighterImage(fighter2, main_0.bitmap_6[fighter2.PictureRef], fighter2.Empire.MainColor, fighter2.Empire.SecondaryColor, size3, fighter2.Size, main_0.double_0);
                        bitmap12 = main_0.PrepareEngineExhaust(fighter2, bitmap10);
                        list_21.Add(fighter2.FighterID);
                        list_16.Add(bitmap10);
                        bitmap11 = bitmap10;
                        bitmap10 = method_70(fighter2, bitmap10, bool_13: false);
                        list_17.Add(bitmap10);
                        list_19.Add(bitmap12);
                        texture2D7 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap10);
                        texture2D8 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap12);
                        list_18.Add(texture2D7);
                        list_20.Add(texture2D8);
                    }
                    int val5 = bitmap10.Width;
                    int val6 = bitmap10.Height;
                    val5 = Math.Min(val5, (int)maxWidth);
                    val6 = Math.Min(val6, (int)maxWidth);
                    int num137 = (int)((double)((int)fighter2.Xpos - num) / main_0.double_0) - val5 / 2 + base.Width / 2;
                    int num138 = (int)((double)((int)fighter2.Ypos - num2) / main_0.double_0) - val6 / 2 + base.Height / 2;
                    if (num137 + val5 < -50 || num137 - val5 > base.Width + 50 || num138 + val6 < -50 || num138 - val6 > base.Height + 50)
                    {
                        continue;
                    }
                    int num139 = bitmap11.Width;
                    int num140 = bitmap11.Height;
                    Size size4 = new Size(num139, num140);
                    Bitmap bitmap13 = main_0.bitmap_6[fighter2.PictureRef];
                    if (bitmap13 != null && bitmap13.PixelFormat != 0)
                    {
                        size4 = kgxRubsAau3(fighter2, main_0.int_5[fighter2.PictureRef], bitmap13, main_0.double_0);
                    }
                    num139 = size4.Width;
                    num140 = size4.Height;
                    Texture2D toDraw = texture2D7;
                    int num141 = (int)((double)num139 * 1.3);
                    int num142 = (int)((double)num140 * 1.3);
                    val5 = num139;
                    val6 = num140;
                    val5 = Math.Min(val5, (int)maxWidth);
                    val6 = Math.Min(val6, (int)maxWidth);
                    int num143 = (int)((double)((int)fighter2.Xpos - num) / main_0.double_0) - val5 / 2 + base.Width / 2;
                    int num144 = (int)((double)((int)fighter2.Ypos - num2) / main_0.double_0) - val6 / 2 + base.Height / 2;
                    int num145 = num143 - (num141 - val5) / 2;
                    int num146 = num144 - (num142 - val6) / 2;
                    if (!fighter2.HasBeenDestroyed)
                    {
                        if (main_0.int_34 >= 2)
                        {
                        }
                        if (texture2D8 != null && fighter2.TargetSpeed > 0f)
                        {
                            float rotationAngle3 = fighter2.Heading;
                            float num147 = (float)texture2D8.Width / (float)texture2D7.Width;
                            int num148 = (int)((float)num139 * num147);
                            int num149 = (int)((float)num140 * num147);
                            int num150 = num143 - (num148 - num139) / 2;
                            int num151 = num144 - (num149 - num140) / 2;
                            XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num150, num151, num148, num149), spriteBatch: spriteBatch_0, texture: texture2D8, rotationAngle: rotationAngle3);
                        }
                        DrawFighterToMainXna(destination: new System.Drawing.Rectangle(num143, num144, num139, num140), spriteBatch: spriteBatch_0, ToDraw: toDraw, fighter: fighter2);
                        if (fighter2 == main_0._Game.SelectedObject)
                        {
                            method_212(spriteBatch_0, num145, num146, num145 + num141, num146 + num142);
                        }
                        if (fighter2.LastShieldStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(fighter2.LastShieldStrike).TotalMilliseconds < 200.0 && (double)fighter2.LastShieldStrikeDirection > double.MinValue)
                            {
                                double num152 = 1.0;
                                int num153 = (int)(num152 * (double)num139);
                                int num154 = (int)(num152 * (double)num140);
                                int num155 = num143 - (num153 - num139) / 2;
                                int num156 = num144 - (num154 - num140) / 2;
                                System.Drawing.Rectangle destination11 = new System.Drawing.Rectangle(num155, num156, num153, num154);
                                float rotationAngle4 = (float)((double)fighter2.LastShieldStrikeDirection - Math.PI / 2.0);
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_6, destination11, rotationAngle4);
                            }
                            else
                            {
                                fighter2.LastShieldStrike = DateTime.MinValue;
                            }
                        }
                        if (main_0.double_0 <= 3.0 && main_0.int_34 < 1 && fighter2.InBattle && fighter2.Specification.ShieldsCapacity > 0)
                        {
                            int int_19 = (int)((fighter2.Xpos - (double)num) / main_0.double_0) - bitmap11.Width / 2 + base.Width / 2;
                            int num157 = (int)((fighter2.Ypos - (double)num2) / main_0.double_0) - bitmap11.Height / 2 + base.Height / 2;
                            method_194(spriteBatch_0, int_19, num157 - 8, bitmap11.Width, fighter2.Specification.ShieldsCapacity, (int)fighter2.CurrentShields);
                        }
                    }
                    if (fighter2.Explosions.Count > 0)
                    {
                        method_184(spriteBatch_0, fighter2, num70);
                    }
                    if (fighter2.Weapons.Count > 0)
                    {
                        method_165(spriteBatch_0, fighter2, currentDateTime, num143, num144);
                    }
                }
            }
            if (main_0.double_0 < 500.0)
            {
                double maxWidth2 = 0.0;
                double num158 = main_0.CalculateCreatureZoomFactor(main_0.double_0, out maxWidth2);
                CreatureList creatureList = new CreatureList();
                new Keyboard();
                _ = MouseHelper.GetCursorPosition().X;
                _ = MouseHelper.GetCursorPosition().Y;
                if (num5 <= (double)(Galaxy.MaxSolarSystemSize + 5000))
                {
                    creatureList.AddRange(ListHelper.ToArrayThreadSafe(galaxy_0.Systems[habitat.SystemIndex].Creatures));
                }
                else
                {
                    GalaxyLocationList galaxyLocationList4 = galaxy_0.DetermineGalaxyLocationsAtPoint(num, num2, GalaxyLocationType.RestrictedArea);
                    for (int num159 = 0; num159 < galaxyLocationList4.Count; num159++)
                    {
                        GalaxyLocation galaxyLocation2 = galaxyLocationList4[num159];
                        creatureList.AddRange(ListHelper.ToArrayThreadSafe(galaxyLocation2.RelatedCreatures));
                        for (int num160 = 0; num160 < galaxyLocation2.RelatedCreatures.Count; num160++)
                        {
                            Creature creature = galaxyLocation2.RelatedCreatures[num160];
                            creature.DoTasks(currentDateTime);
                        }
                    }
                }
                for (int num161 = 0; num161 < creatureList.Count; num161++)
                {
                    Creature creature2 = creatureList[num161];
                    if (creature2 == null)
                    {
                        continue;
                    }
                    int num162 = main_0.bitmap_10[creature2.PictureRef][0].Width;
                    int num163 = main_0.bitmap_10[creature2.PictureRef][0].Height;
                    num162 = (int)((double)num162 / num158);
                    num163 = (int)((double)num163 / num158);
                    num162 = Math.Min(num162, (int)maxWidth2);
                    num163 = Math.Min(num163, (int)maxWidth2);
                    int num164 = (int)((creature2.Xpos - ((double)num + (double)num162 * main_0.double_0 / 2.0)) / main_0.double_0) + base.Width / 2;
                    int num165 = (int)((creature2.Ypos - ((double)num2 + (double)num163 * main_0.double_0 / 2.0)) / main_0.double_0) + base.Height / 2;
                    if (num164 + num162 < -50 || num164 - num162 > base.Width + 50 || num165 + num163 < -50 || num165 - num163 > base.Height + 50)
                    {
                        continue;
                    }
                    if (creature2.CurrentSpeed <= (float)creature2.MovementSpeed && creature2.NearestSystemStar != habitat)
                    {
                        creature2.PromptSystemCheck = true;
                    }
                    if (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(creature2))
                    {
                        continue;
                    }
                    int num166 = list_24.IndexOf(creature2.CreatureID);
                    Bitmap[] array;
                    Bitmap[] array2;
                    if (num166 >= 0)
                    {
                        array = list_22[num166];
                        array2 = list_23[num166];
                    }
                    else
                    {
                        int pictureRef = creature2.PictureRef;
                        int num167 = pictureRef + 5;
                        double size5 = (double)main_0.sveqhmNacy[pictureRef] / Galaxy.CreatureDrawResizeFactor;
                        array = new Bitmap[main_0.bitmap_10[pictureRef].Length];
                        for (int num168 = 0; num168 < main_0.bitmap_10[pictureRef].Length; num168++)
                        {
                            array[num168] = main_0.PrepareCreatureImage(creature2, main_0.bitmap_10[pictureRef][num168], size5, creature2.Size, allowPreRotate: false);
                        }
                        size5 = (double)main_0.sveqhmNacy[num167] / Galaxy.CreatureDrawResizeFactor;
                        array2 = new Bitmap[main_0.bitmap_10[num167].Length];
                        for (int num169 = 0; num169 < main_0.bitmap_10[num167].Length; num169++)
                        {
                            array2[num169] = main_0.PrepareCreatureImage(creature2, main_0.bitmap_10[num167][num169], size5, creature2.Size, allowPreRotate: false);
                        }
                        list_24.Add(creature2.CreatureID);
                        list_22.Add(array);
                        list_23.Add(array2);
                    }
                    num162 = array[0].Width;
                    num163 = array[0].Height;
                    num162 = (int)((double)num162 / num158);
                    num163 = (int)((double)num163 / num158);
                    num162 = Math.Min(num162, (int)maxWidth2);
                    num163 = Math.Min(num163, (int)maxWidth2);
                    num164 = (int)((creature2.Xpos - (double)(num + (int)((double)num162 * num158 / 2.0))) / main_0.double_0) + base.Width / 2;
                    num165 = (int)((creature2.Ypos - (double)(num2 + (int)((double)num163 * num158 / 2.0))) / main_0.double_0) + base.Height / 2;
                    if (num164 + num162 < -50 || num164 - num162 > base.Width + 50 || num165 + num163 < -50 || num165 - num163 > base.Height + 50)
                    {
                        continue;
                    }
                    Bitmap bitmap14 = null;
                    Texture2D texture2D9 = null;
                    Texture2D[] texture2D_3 = null;
                    Texture2D[] texture2D_4 = null;
                    bool flag9 = false;
                    if (creature2.Damage > 0.0)
                    {
                        bitmap14 = main_0.method_108(creature2, array[0], main_0.bitmap_11[creature2.PictureRef][0]);
                        texture2D9 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap14);
                        method_21(bitmap14);
                        flag9 = true;
                    }
                    else
                    {
                        bitmap14 = array[0];
                        switch (creature2.Type)
                        {
                            case CreatureType.Kaltor:
                                texture2D9 = main_0.texture2D_0[2][0];
                                texture2D_3 = main_0.texture2D_0[2];
                                texture2D_4 = main_0.texture2D_0[7];
                                break;
                            case CreatureType.RockSpaceSlug:
                                texture2D9 = main_0.texture2D_0[0][0];
                                texture2D_3 = main_0.texture2D_0[0];
                                texture2D_4 = main_0.texture2D_0[5];
                                break;
                            case CreatureType.DesertSpaceSlug:
                                texture2D9 = main_0.texture2D_0[1][0];
                                texture2D_3 = main_0.texture2D_0[1];
                                texture2D_4 = main_0.texture2D_0[6];
                                break;
                            case CreatureType.Ardilus:
                                texture2D9 = main_0.texture2D_0[3][0];
                                texture2D_3 = main_0.texture2D_0[3];
                                texture2D_4 = main_0.texture2D_0[8];
                                break;
                            case CreatureType.SilverMist:
                                texture2D9 = main_0.texture2D_0[4][0];
                                texture2D_3 = main_0.texture2D_0[4];
                                texture2D_4 = main_0.texture2D_0[9];
                                break;
                        }
                    }
                    int num170 = bitmap14.Width;
                    int num171 = bitmap14.Height;
                    int num172 = (bitmap14.Width - num170) / 2;
                    int num173 = (bitmap14.Height - num171) / 2;
                    num172 = (int)((double)num172 / num158);
                    num173 = (int)((double)num173 / num158);
                    int num174 = (int)((double)num162 * 1.5);
                    int num175 = (int)((double)num163 * 1.5);
                    int num176 = num164 - (num174 - num162) / 2;
                    int num177 = num165 - (num175 - num163) / 2;
                    if (!creature2.HasBeenDestroyed)
                    {
                        if (creature2.CurrentSpeed > 0f)
                        {
                            if (creature2.Damage > 0.0)
                            {
                                method_114(creature2, spriteBatch_0, num164 - num172, num165 - num173, num162, num163, array, array2, currentDateTime, 10, creature2.CurrentHeading);
                            }
                            else
                            {
                                method_113(creature2, spriteBatch_0, num164 - num172, num165 - num173, num162, num163, texture2D_3, texture2D_4, currentDateTime, 10, creature2.CurrentHeading);
                            }
                        }
                        else
                        {
                            DrawCreatureToMainXna(spriteBatch_0, creature2, texture2D9, num164 - num172, num165 - num173, num162, num163, creature2.CurrentHeading);
                        }
                        _ = ((int)((double)texture2D9.Width / num158) - num162) / 2;
                        _ = ((int)((double)texture2D9.Height / num158) - num163) / 2;
                        if (creature2 == main_0._Game.SelectedObject)
                        {
                            method_212(spriteBatch_0, num176, num177, num176 + num174, num177 + num175);
                        }
                    }
                    list_4.Add(new System.Drawing.Rectangle(num164 - 7, num165 - 7, (int)((double)texture2D9.Width / main_0.double_0) + 14, (int)((double)texture2D9.Height / main_0.double_0) + 14));
                    if (flag9)
                    {
                        method_22(texture2D9);
                    }
                }
            }
            if (Control.MouseButtons == MouseButtons.Left && main_0.method_207(MouseHelper.GetCursorPosition()) == null && !main_0.itemListCollectionPanel_0.Area.Contains(MouseHelper.GetCursorPosition()) && (main_0.int_15 != main_0.int_32 || main_0.int_16 != main_0.int_33))
            {
                int num178 = Math.Min(main_0.int_15, main_0.int_32);
                int num179 = Math.Min(main_0.int_16, main_0.int_33);
                int num180 = Math.Abs(main_0.int_15 - main_0.int_32);
                int num181 = Math.Abs(main_0.int_16 - main_0.int_33);
                num178 = base.Width / 2 + (int)((double)(num178 - num) / main_0.double_0);
                num179 = base.Height / 2 + (int)((double)(num179 - num2) / main_0.double_0);
                num180 = (int)((double)num180 / main_0.double_0);
                num181 = (int)((double)num181 / main_0.double_0);
                XnaDrawingHelper.DrawRectangle(spriteBatch_0, new System.Drawing.Rectangle(num178, num179, num180, num181), main_0.pen_1.Color, (int)main_0.pen_1.Width);
            }
            if (main_0.double_0 < 500.0)
            {
                double num182 = main_0.CalculatePlanetZoomFactor(main_0.double_0);
                double num183 = main_0.CalculateMoonZoomFactor(main_0.double_0);
                for (int num184 = main_0.int_28; num184 <= main_0.int_29; num184++)
                {
                    if ((systemVisibilityStatus == SystemVisibilityStatus.Unexplored && num184 > main_0.int_28 && !flag4) || num184 >= galaxy_0.Habitats.Count)
                    {
                        continue;
                    }
                    Habitat habitat5 = galaxy_0.Habitats[num184];
                    if (habitat5 == null)
                    {
                        continue;
                    }
                    bool flag10 = false;
                    if (flag4)
                    {
                        if (!empire.IsObjectVisibleToThisEmpire(habitat5))
                        {
                            continue;
                        }
                        flag10 = true;
                    }
                    double double_4 = main_0.double_0;
                    switch (habitat5.Category)
                    {
                        case HabitatCategoryType.Planet:
                            double_4 = num182;
                            break;
                        case HabitatCategoryType.Moon:
                            double_4 = num183;
                            break;
                    }
                    Bitmap bitmap15 = method_57(num184 - main_0.int_28);
                    if (habitat5.Category == HabitatCategoryType.GasCloud)
                    {
                        bitmap15 = bitmap_2;
                        method_59(habitat5, double_4, 0, out int_, out int_2);
                    }
                    if (bitmap15 == null)
                    {
                        bitmap15 = main_0.habitatImageCache_0.ObtainImageSmall(habitat5);
                    }
                    int_ = habitat5.Diameter;
                    int_2 = habitat5.Diameter;
                    if (bitmap15 != null && bitmap15.PixelFormat != 0)
                    {
                        double num185 = (double)bitmap15.Width / (double)bitmap15.Height;
                        if (bitmap15.Width > bitmap15.Height)
                        {
                            int_ = (int)((double)habitat5.Diameter * num185);
                            int_2 = habitat5.Diameter;
                        }
                        else
                        {
                            int_ = habitat5.Diameter;
                            int_2 = (int)((double)habitat5.Diameter / num185);
                        }
                    }
                    int int_20 = 4;
                    if (habitat5.Category == HabitatCategoryType.Asteroid)
                    {
                        int_20 = 1;
                    }
                    System.Drawing.Rectangle rectangle9 = method_35((int)habitat5.Xpos, (int)habitat5.Ypos, int_, int_2, double_4, int_20);
                    int num36 = rectangle9.X;
                    int num37 = rectangle9.Y;
                    int_ = rectangle9.Width;
                    int_2 = rectangle9.Height;
                    if (num36 + int_ < -20 || num36 > base.Width + 20 || num37 + int_2 < -20 || num37 > base.Height + 20)
                    {
                        continue;
                    }
                    bool flag11 = false;
                    bool flag12 = false;
                    bool flag13 = false;
                    if ((main_0.int_34 < 2 && systemVisibilityStatus != SystemVisibilityStatus.Unexplored) || flag10)
                    {
                        if (!habitat5.HasBeenDestroyed)
                        {
                            if (main_0.double_0 < 2.0)
                            {
                                if (habitat5.Owner != null && habitat5.Population != null && habitat5.Population.Count > 0)
                                {
                                    flag12 = true;
                                }
                                if (habitat5.BasesAtHabitat != null && habitat5.BasesAtHabitat.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Category == HabitatCategoryType.Planet)
                                {
                                    flag11 = true;
                                }
                            }
                            else if (main_0.double_0 < 10.0)
                            {
                                if (habitat5.Owner != null && habitat5.Population != null && habitat5.Population.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.BasesAtHabitat != null && habitat5.BasesAtHabitat.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Category == HabitatCategoryType.Planet)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Ruin != null)
                                {
                                    flag13 = true;
                                }
                            }
                            else if (main_0.double_0 < 40.0)
                            {
                                if (habitat5.Owner != null && habitat5.Population != null && habitat5.Population.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.BasesAtHabitat != null && habitat5.BasesAtHabitat.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Ruin != null)
                                {
                                    flag13 = true;
                                }
                            }
                            else
                            {
                                if (habitat5.Owner != null && habitat5.Population != null && habitat5.Population.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Ruin != null)
                                {
                                    flag13 = true;
                                }
                            }
                        }
                        if (main_0.int_34 == 2)
                        {
                            flag12 = false;
                            flag11 = false;
                            flag13 = false;
                        }
                    }
                    if (flag12)
                    {
                        System.Drawing.Rectangle rectangle10 = method_81(num36, num37, int_, int_2);
                        Texture2D texture2D10 = null;
                        int num186 = list_0.IndexOf(habitat5.HabitatIndex);
                        if (num186 >= 0 && list_2.Count > num186)
                        {
                            texture2D10 = list_2[num186];
                        }
                        if (texture2D10 == null)
                        {
                            Bitmap bitmap16 = method_89(habitat5);
                            texture2D10 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap16);
                            method_21(bitmap16);
                            list_2.Add(texture2D10);
                            list_0.Add(habitat5.HabitatIndex);
                        }
                        method_86(spriteBatch_0, habitat5, rectangle10.X, rectangle10.Y, rectangle10.Width, rectangle10.Height, texture2D10);
                    }
                    else if (flag11 || flag13)
                    {
                        method_84(spriteBatch_0, habitat5, num36, num37, int_, int_2, main_0.double_0, flag11, flag13);
                    }
                }
            }
            method_91(num, num2, main_0.double_0);
            if (main_0.double_0 < 100.0)
            {
                if (flag2)
                {
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Explored && flag3)
                {
                    XnaDrawingHelper.DrawPolygon(spriteBatch_0, point_0, System.Drawing.Color.FromArgb(20, 20, 20), 1);
                }
            }
            graphicsPath_0.Reset();
            graphicsPath_1.Reset();
            graphicsPath_2.Reset();
            graphicsPath_0.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            graphicsPath_1.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            graphicsPath_2.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            dateTime_2 = currentDateTime;
            return;
        IL_05ae:
            if (flag2)
            {
                System.Drawing.Rectangle rectangle = method_34((int)habitat.Xpos, (int)habitat.Ypos, Galaxy.MaxSolarSystemSize * 2 + 1000, Galaxy.MaxSolarSystemSize * 2 + 1000, main_0.double_0);
                graphicsPath_2.AddEllipse(rectangle);
            }
            goto IL_05fc;
        }

        private Size method_77(BuiltObject builtObject_1, BuiltObjectImageData builtObjectImageData_0, double double_15)
        {
            return main_0.DetermineBuiltObjectSizeNEW(builtObject_1, builtObjectImageData_0, double_15);
        }

        private Size kgxRubsAau3(Fighter fighter_0, int int_11, Bitmap bitmap_7, double double_15)
        {
            double size = (double)int_11 / Galaxy.BuiltObjectDrawResizeFactor;
            int targetSize = (int)((double)fighter_0.Size / (double_15 * double_15));
            return main_0.DetermineBuiltObjectSize(bitmap_7, size, targetSize);
        }

        private Texture2D method_78(Habitat habitat_1, bool bool_13, out Bitmap bitmap_7, out bool bool_14)
        {
            bitmap_7 = null;
            bool_14 = false;
            try
            {
                if (habitat_1.Category == HabitatCategoryType.Asteroid && main_0.bool_6 && main_0.long_0 >= 4080218929L && (main_0.int_1 > 6 || (main_0.int_1 == 6 && main_0.int_2 >= 1)))
                {
                    int num = habitat_1.PictureRef - int_0;
                    if (num >= 0 && num < bitmap_0.Length)
                    {
                        bitmap_7 = bitmap_0[num];
                        return texture2D_0[num];
                    }
                }
                Bitmap bitmap = null;
                bitmap = ((!bool_13) ? main_0.habitatImageCache_0.ObtainImage(habitat_1) : main_0.habitatImageCache_0.FastGetImage(habitat_1.PictureRef, out bool_14));
                if (bitmap != null && bitmap.PixelFormat != 0)
                {
                    bitmap.SetResolution(72f, 72f);
                    Bitmap bitmap_8 = null;
                    if (habitat_1.HasRings)
                    {
                        bitmap_8 = method_55(habitat_1, bitmap.Width);
                    }
                    if (habitat_1.Category != HabitatCategoryType.Planet && habitat_1.Category != HabitatCategoryType.Moon)
                    {
                        bitmap_7 = bitmap;
                    }
                    else
                    {
                        Bitmap bitmap2 = new Bitmap(bitmap);
                        bitmap2.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
                        bool bool_15 = false;
                        Bitmap bitmap3 = bitmap2;
                        bitmap2 = method_52(habitat_1, bitmap2, out bool_15);
                        if (bool_15)
                        {
                            bitmap3.Dispose();
                        }
                        bitmap2 = method_51(habitat_1, bitmap2);
                        bitmap2 = method_53(habitat_1, bitmap2);
                        bitmap2 = method_50(habitat_1, bitmap2);
                        bitmap_7 = method_56(habitat_1, bitmap2, bitmap_8);
                        bitmap2.Dispose();
                    }
                    return XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_7);
                }
            }
            catch (Exception)
            {
                //BaconMain.CatchMethod_78(this);
            }
            return null;
        }

        protected override void OnCreateControl()
        {
            if (!base.DesignMode)
            {
                class1_0 = Class1.smethod_0(base.Handle, base.ClientSize.Width, base.ClientSize.Height);
                serviceContainer_0.AddService((IGraphicsDeviceService)class1_0);
            }
            base.OnCreateControl();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (class1_0 != null)
            {
                _ = base.ClientSize;
                class1_0.method_1(base.ClientSize.Width, base.ClientSize.Height);
            }
            base.OnSizeChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (spriteBatch_0 != null && !spriteBatch_0.IsDisposed)
            {
                spriteBatch_0.Dispose();
            }
            if (spriteBatch_1 != null && !spriteBatch_1.IsDisposed)
            {
                spriteBatch_1.Dispose();
            }
            if (class1_0 != null)
            {
                class1_0.method_0(disposing);
                class1_0 = null;
            }
            base.Dispose(disposing);
        }

        public void ResetRendering()
        {
            bool_12 = false;
        }

        public void DisposePirateFlagTextures()
        {
            XnaDrawingHelper.DisposeTextureArray(texture2D_21);
        }

        private void method_79()
        {
            if (bool_12)
            {
                return;
            }
            presentationParameters_0 = new PresentationParameters();
            presentationParameters_0.BackBufferWidth = base.Width;
            presentationParameters_0.BackBufferHeight = base.Height;
            presentationParameters_0.BackBufferFormat = SurfaceFormat.Color;
            presentationParameters_0.DepthStencilFormat = DepthFormat.Depth24;
            presentationParameters_0.MultiSampleCount = 4;
            presentationParameters_0.PresentationInterval = PresentInterval.Default;
            presentationParameters_0.IsFullScreen = false;
            presentationParameters_0.DeviceWindowHandle = base.Handle;
            spriteBatch_0 = new SpriteBatch(GraphicsDevice);
            spriteBatch_1 = new SpriteBatch(GraphicsDevice);
            XnaDrawingHelper.Initialize(GraphicsDevice);
            for (int i = 0; i < main_0.bitmap_10.Length; i++)
            {
                for (int j = 0; j < main_0.bitmap_10[i].Length; j++)
                {
                    if (main_0.texture2D_0[i][j] == null)
                    {
                        main_0.texture2D_0[i][j] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_10[i][j]);
                    }
                }
            }
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_12, ref texture2D_1);
            method_80(main_0.bitmap_12);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_13, ref texture2D_2);
            method_80(main_0.bitmap_13);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.edLqkLkgAx, ref texture2D_3);
            method_80(main_0.edLqkLkgAx);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_14, ref texture2D_4);
            method_80(main_0.bitmap_14);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_15, ref texture2D_5);
            method_80(main_0.bitmap_15);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_18, ref texture2D_7);
            method_80(main_0.bitmap_18);
            for (int k = 0; k < texture2D_8.Length; k++)
            {
                XnaDrawingHelper.DisposeTextureArray(texture2D_8[k]);
                texture2D_8[k] = new Texture2D[main_0.bitmap_19[k].Length];
                for (int l = 0; l < texture2D_8[k].Length; l++)
                {
                    texture2D_8[k][l] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_19[k][l]);
                }
            }
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_199, ref texture2D_12);
            method_80(main_0.bitmap_199);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_200, ref texture2D_13);
            method_80(main_0.bitmap_200);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_204, ref texture2D_14);
            method_80(main_0.bitmap_204);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_201, ref texture2D_15);
            method_80(main_0.bitmap_201);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_202, ref texture2D_16);
            method_80(main_0.bitmap_202);
            texture2D_18 = new Texture2D[main_0.bitmap_196.Length];
            for (int m = 0; m < texture2D_18.Length; m++)
            {
                texture2D_18[m] = XnaDrawingHelper.BitmapToTexture(GraphicsDevice, main_0.bitmap_196[m]);
            }
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_197, ref texture2D_19);
            method_80(main_0.bitmap_197);
            texture2D_20 = new Texture2D[main_0.bitmap_206.Length];
            for (int n = 0; n < texture2D_20.Length; n++)
            {
                texture2D_20[n] = XnaDrawingHelper.BitmapToTexture(GraphicsDevice, main_0.bitmap_206[n]);
            }
            XnaDrawingHelper.DisposeTextureArray(texture2D_21);
            texture2D_21 = new Texture2D[255];
            texture2D_6 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_17);
            texture2D_17 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_188);
            texture2D_22 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_49);
            texture2D_23 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_52);
            texture2D_24 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_4);
            texture2D_25 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_88);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_215, ref texture2D_34);
            method_80(main_0.bitmap_215);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_211, ref texture2D_32);
            method_80(main_0.bitmap_211);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_210, ref texture2D_31);
            method_80(main_0.bitmap_210);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_212, ref texture2D_33);
            method_80(main_0.bitmap_212);
            bitmap_0 = main_0.habitatImageCache_0.GetAsteroidImages();
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, bitmap_0, ref texture2D_0);
            for (int num = 0; num < main_0.list_3.Count; num++)
            {
                if (list_26.Count > num)
                {
                    Texture2D[] textures = list_26[num];
                    XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.list_3[num], ref textures);
                    list_26[num] = textures;
                }
                else
                {
                    list_26.Add(XnaDrawingHelper.ConvertBitmapsToTextures(GraphicsDevice, main_0.list_3[num]));
                }
                method_80(main_0.list_3[num]);
            }
            for (int num2 = 0; num2 < main_0.list_4.Count; num2++)
            {
                if (list_27.Count > num2)
                {
                    Texture2D[] textures2 = list_27[num2];
                    XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.list_4[num2], ref textures2);
                    list_27[num2] = textures2;
                }
                else
                {
                    list_27.Add(XnaDrawingHelper.ConvertBitmapsToTextures(GraphicsDevice, main_0.list_4[num2]));
                }
                method_80(main_0.list_4[num2]);
            }
            texture2D_35 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_105);
            texture2D_36 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_110);
            texture2D_37 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_111);
            texture2D_38 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_112);
            texture2D_39 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_113);
            texture2D_40 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_114);
            texture2D_41 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.VnciZycUss);
            texture2D_42 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_115);
            texture2D_43 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_116);
            texture2D_44 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_117);
            texture2D_45 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_118);
            texture2D_46 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_119);
            texture2D_47 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_120);
            texture2D_48 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_121);
            texture2D_49 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_122);
            texture2D_50 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_123);
            texture2D_51 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_124);
            texture2D_52 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_125);
            Bitmap bitmap = GraphicsHelper.ScaleImage(main_0.bitmap_44, 20, 20, 1f);
            Bitmap bitmap2 = GraphicsHelper.ScaleImage(main_0.bitmap_44, 14, 14, 1f);
            texture2D_9 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap);
            texture2D_27 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap2);
            Bitmap bitmap3 = GraphicsHelper.ScaleImage(main_0.bitmap_43, 20, 20, 1f);
            Bitmap bitmap4 = GraphicsHelper.ScaleImage(main_0.bitmap_43, 14, 14, 1f);
            texture2D_10 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap3);
            texture2D_28 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap4);
            Bitmap bitmap5 = GraphicsHelper.ScaleImage(main_0.bitmap_55, 20, 20, 1f);
            Bitmap bitmap6 = GraphicsHelper.ScaleImage(main_0.bitmap_55, 14, 14, 1f);
            texture2D_11 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap5);
            texture2D_29 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap6);
            Bitmap[] images = GraphicsHelper.ScaleImages(main_0.bitmap_9, 40, 40);
            Bitmap[] images2 = GraphicsHelper.ScaleImages(main_0.bitmap_9, 22, 22);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, images, ref texture2D_26);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, images2, ref texture2D_30);
            Texture2D[] textures3 = main_0.texture2D_1;
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_20, ref textures3);
            main_0.texture2D_1 = textures3;
            method_80(main_0.bitmap_20);
            bool_12 = true;
        }

        private void method_80(Bitmap[] bitmap_7)
        {
            for (int i = 0; i < bitmap_7.Length; i++)
            {
                bitmap_7[i]?.Dispose();
            }
        }

        private System.Drawing.Rectangle method_81(int int_11, int int_12, int int_13, int int_14)
        {
            int num = int_13 - int_14;
            if (num != 0)
            {
                int_11 += num / 2;
            }
            int num2 = Math.Min(int_13, int_14);
            return new System.Drawing.Rectangle(int_11, int_12, num2, num2);
        }

        private Bitmap method_82(Habitat habitat_1, double double_15, bool bool_13, bool bool_14)
        {
            Font font = font_3;
            if (double_15 < 3.0)
            {
                font = font_0;
            }
            SizeF sizeF;
            using (Graphics graphics = CreateGraphics())
            {
                method_177(graphics);
                sizeF = graphics.MeasureString(habitat_1.Name, font, 200, StringFormat.GenericDefault);
            }
            if (!bool_13)
            {
                sizeF = new SizeF(1f, sizeF.Height);
            }
            int num = (int)sizeF.Width;
            int num2 = (int)sizeF.Height;
            int num3 = num + 2;
            int num4 = num2 + 2;
            int num5 = 5;
            int num6 = 12;
            if (bool_14)
            {
                num3 += num6 * 2;
            }
            Bitmap bitmap = new Bitmap(num3, num4, PixelFormat.Format32bppPArgb);
            using Graphics graphics2 = Graphics.FromImage(bitmap);
            int num7 = num3 / 2 - num / 2;
            int num8 = num4 / 2 - num2 / 2;
            System.Drawing.Point point_ = new System.Drawing.Point(num7, num8);
            SolidBrush solidBrush = new SolidBrush(color_1);
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                solidBrush = new SolidBrush(habitat_1.Empire.MainColor);
            }
            if (bool_13)
            {
                method_267(graphics2, habitat_1.Name, font, point_, solidBrush);
            }
            solidBrush.Dispose();
            if (bool_14)
            {
                int num9 = num3 / 2 + num / 2 + 1;
                int num10 = num8 + 1;
                if (main_0.double_0 > 3.0)
                {
                    num6 = 8;
                    num5 = 4;
                }
                using SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num9, num10 + (int)((double)num6 * 0.85), num5, num5);
                graphics2.FillEllipse(brush, rect);
                rect = new System.Drawing.Rectangle(num9 + num6, num10 + (int)((double)num6 * 0.85), num5, num5);
                graphics2.FillEllipse(brush, rect);
                rect = new System.Drawing.Rectangle(num9 + num6 / 2, num10, num5, num5);
                graphics2.FillEllipse(brush, rect);
                return bitmap;
            }
            return bitmap;
        }

        private void method_83(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, Bitmap bitmap_7)
        {
            int num = int_11 + int_13 - bitmap_7.Width / 2;
            int num2 = int_12 + int_14 / 2 - bitmap_7.Height / 2;
            graphics_0.DrawImageUnscaled(bitmap_7, num, num2);
        }

        private void method_84(SpriteBatch spriteBatch_2, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, bool bool_14)
        {
            SpriteFont spriteFont = spriteFont_0;
            if (double_15 < 3.0)
            {
                spriteFont = spriteFont_3;
            }
            Vector2 vector = spriteFont.MeasureString(habitat_1.Name);
            if (!bool_13)
            {
                vector = new Vector2(1f, vector.Y);
            }
            int num = (int)vector.X;
            int num2 = (int)vector.Y;
            int num3 = int_11 + int_13 - num / 2;
            int num4 = int_12 + int_14 / 2 - num2 / 2;
            System.Drawing.Point point = new System.Drawing.Point(num3, num4);
            System.Drawing.Color mainColor = color_1;
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                mainColor = habitat_1.Empire.MainColor;
            }
            if (bool_13)
            {
                XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, habitat_1.Name, spriteFont, mainColor, point);
            }
            if (bool_14)
            {
                int num5 = int_11 + int_13 + num / 2 + 1;
                int num6 = num4 + 1;
                int num7 = 5;
                int num8 = 12;
                if (main_0.double_0 > 3.0)
                {
                    num8 = 8;
                    num7 = 4;
                }
                using (new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255)))
                {
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(num5, num6 + (int)((double)num8 * 0.85), num7, num7);
                    XnaDrawingHelper.FillRectangle(spriteBatch_2, rectangle, System.Drawing.Color.White);
                    rectangle = new System.Drawing.Rectangle(num5 + num8, num6 + (int)((double)num8 * 0.85), num7, num7);
                    XnaDrawingHelper.FillRectangle(spriteBatch_2, rectangle, System.Drawing.Color.White);
                    rectangle = new System.Drawing.Rectangle(num5 + num8 / 2, num6, num7, num7);
                    XnaDrawingHelper.FillRectangle(spriteBatch_2, rectangle, System.Drawing.Color.White);
                }
            }
        }

        private void method_85(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, bool bool_14)
        {
            Font font = font_3;
            if (double_15 < 3.0)
            {
                font = font_0;
            }
            SizeF sizeF = graphics_0.MeasureString(habitat_1.Name, font, 200, StringFormat.GenericDefault);
            if (!bool_13)
            {
                sizeF = new SizeF(1f, sizeF.Height);
            }
            int num = (int)sizeF.Width;
            int num2 = (int)sizeF.Height;
            int num3 = int_11 + int_13 - num / 2;
            int num4 = int_12 + int_14 / 2 - num2 / 2;
            System.Drawing.Point point_ = new System.Drawing.Point(num3, num4);
            SolidBrush solidBrush = new SolidBrush(color_1);
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                solidBrush = new SolidBrush(habitat_1.Empire.MainColor);
            }
            if (bool_13)
            {
                method_267(graphics_0, habitat_1.Name, font, point_, solidBrush);
            }
            solidBrush.Dispose();
            if (bool_14)
            {
                int num5 = int_11 + int_13 + num / 2 + 1;
                int num6 = num4 + 1;
                int num7 = 5;
                int num8 = 12;
                if (main_0.double_0 > 3.0)
                {
                    num8 = 8;
                    num7 = 4;
                }
                using SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num5, num6 + (int)((double)num8 * 0.85), num7, num7);
                graphics_0.FillEllipse(brush, rect);
                rect = new System.Drawing.Rectangle(num5 + num8, num6 + (int)((double)num8 * 0.85), num7, num7);
                graphics_0.FillEllipse(brush, rect);
                rect = new System.Drawing.Rectangle(num5 + num8 / 2, num6, num7, num7);
                graphics_0.FillEllipse(brush, rect);
            }
        }

        private void method_86(SpriteBatch spriteBatch_2, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, Texture2D texture2D_61)
        {
            int num = int_11 + int_13 - texture2D_61.Width / 2;
            int num2 = int_12 + int_14 / 2 - texture2D_61.Height / 2;
            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, num, num2, 0f, 1f);
        }

        private void method_87(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, Bitmap bitmap_7)
        {
            int num = int_11 + int_13 - bitmap_7.Width / 2;
            int num2 = int_12 + int_14 / 2 - bitmap_7.Height / 2;
            System.Drawing.Point point = new System.Drawing.Point(num, num2);
            graphics_0.DrawImageUnscaled(bitmap_7, point);
        }

        private void method_88(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14)
        {
            SizeF sizeF = graphics_0.MeasureString(habitat_1.Name, font_0, 300);
            int num = (int)sizeF.Width;
            _ = sizeF.Height;
            int num2 = 0;
            System.Drawing.Color mainColor = color_1;
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                if (habitat_1.Empire.Capital == habitat_1 || habitat_1.Empire.Capitals.Contains(habitat_1))
                {
                    num2 = 16;
                }
                mainColor = habitat_1.Empire.MainColor;
            }
            int num3 = 0;
            int num4 = 0;
            if (habitat_1.Population != null && habitat_1.Population.Count > 0)
            {
                num3 = 20;
                num4 = 20;
            }
            bool flag = false;
            if (galaxy_0.PlayerEmpire.ResourceMap != null)
            {
                flag = galaxy_0.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat_1);
            }
            int num5 = 0;
            if (habitat_1.Resources != null && habitat_1.Resources.Count > 0 && flag)
            {
                num5 = 16 * habitat_1.Resources.Count;
            }
            int num6 = num2 + num;
            int num7 = num3 + num4 + 6 + num5;
            int num8 = Math.Max(num6, num7);
            int num9 = 36;
            int num10 = num8 + 18;
            int num11 = int_11 + int_13 - num10 / 2;
            int num12 = int_12 + int_14 / 2 - 18;
            int num13 = 9;
            int num14 = 18;
            List<System.Drawing.Point> list = new List<System.Drawing.Point>();
            List<System.Drawing.Point> list2 = new List<System.Drawing.Point>();
            list.Add(new System.Drawing.Point(num11, num12 + 18));
            list.Add(new System.Drawing.Point(num11 + 9, num12));
            list.Add(new System.Drawing.Point(num11 + num10 - 9, num12));
            list.Add(new System.Drawing.Point(num11 + num10, num12 + 18));
            list2.AddRange(list);
            list.Add(new System.Drawing.Point(num11 + num10 - 9, num12 + 36));
            list.Add(new System.Drawing.Point(num11 + 9, num12 + 36));
            using (Pen pen = new Pen(mainColor, 1f))
            {
                graphics_0.FillPolygon(solidBrush_2, list.ToArray());
                graphics_0.FillPolygon(new SolidBrush(pen.Color), list2.ToArray());
                graphics_0.DrawPolygon(pen, list.ToArray());
            }
            List<System.Drawing.Point> list3 = new List<System.Drawing.Point>();
            new List<System.Drawing.Point>();
            List<System.Drawing.Point> list4 = new List<System.Drawing.Point>();
            int num15 = 2;
            list3.Add(new System.Drawing.Point(num11 - 1, num12 + num14));
            list3.Add(new System.Drawing.Point(num11 + num13 - 1, num12 - 1));
            list3.Add(new System.Drawing.Point(num11 + num10 - num13 + 1, num12 - 1));
            list4.Add(new System.Drawing.Point(num11 + num10 - num13 + 1, num12 - 1));
            list4.Add(new System.Drawing.Point(num11 + num10 + 1, num12 + num14));
            list4.Add(new System.Drawing.Point(num11 + num10 - num13 + 1, num12 + num9 + 1));
            list4.Add(new System.Drawing.Point(num11 + num13 - 1, num12 + num9 + 1));
            list4.Add(new System.Drawing.Point(num11 - 1, num12 + num14));
            System.Drawing.Color color = ControlPaint.Dark(mainColor);
            System.Drawing.Color color2 = ControlPaint.Light(mainColor);
            using (Pen pen2 = new Pen(color2, 2f))
            {
                graphics_0.DrawLines(pen2, list3.ToArray());
            }
            using (Pen pen3 = new Pen(color, num15))
            {
                graphics_0.DrawLines(pen3, list4.ToArray());
            }
            int num16 = num11 + num13 + (num8 - num6) / 2;
            int num17 = num12 + 2;
            using (Pen pen4 = new Pen(mainColor, 1f))
            {
                if (num2 > 0)
                {
                    if (habitat_1.Empire != null && habitat_1.Empire.Capital == habitat_1)
                    {
                        graphics_0.DrawImage(main_0.bitmap_44, new System.Drawing.Rectangle(num16, num17, 15, 15));
                    }
                    else
                    {
                        graphics_0.DrawImage(main_0.bitmap_43, new System.Drawing.Rectangle(num16, num17, 15, 15));
                    }
                    if (bool_4)
                    {
                        method_199(new System.Drawing.Rectangle(num16 - 5, num17 - 5, 25, 25), graphics_0);
                    }
                    num16 += num2;
                }
                method_266(graphics_0, habitat_1.Name, font_0, new System.Drawing.Point(num16, num17));
                num16 = num11 + num13 + 3 + (num8 - num7) / 2;
                num17 = num12 + num14 + 2;
                graphics_0.DrawLine(pen4, num11, num12 + num14, num11 + num10, num12 + num14);
                if (num3 > 0)
                {
                    if (habitat_1.Population != null && habitat_1.Population.DominantRace != null)
                    {
                        graphics_0.DrawImage(main_0.raceImageCache_0.GetRaceImage(habitat_1.Population.DominantRace.PictureRef), new System.Drawing.Rectangle(num16, num17, 15, 15));
                    }
                    if (bool_3)
                    {
                        method_201(new System.Drawing.Rectangle(num16 - 5, num17 - 5, 25, 25), graphics_0);
                    }
                    num16 += 17;
                }
                if (num4 > 0)
                {
                    SmoothingMode smoothingMode = graphics_0.SmoothingMode;
                    graphics_0.SmoothingMode = SmoothingMode.None;
                    method_155(habitat_1, num16, num17, graphics_0);
                    graphics_0.SmoothingMode = smoothingMode;
                    num16 += num4;
                }
                num16 += 4;
                graphics_0.DrawLine(pen4, num16, num12 + num14, num16, num12 + num9);
                num16 += 5;
            }
            if (num5 <= 0 || !flag)
            {
                return;
            }
            int num18 = num16;
            num17++;
            HabitatResourceList habitatResourceList = habitat_1.Resources.Clone();
            for (int i = 0; i < habitatResourceList.Count; i++)
            {
                Bitmap bitmap = main_0.esJqlOpLpG[habitatResourceList[i].PictureRef];
                double num19 = (double)bitmap.Width / (double)bitmap.Height;
                int num20 = 0;
                int num21 = 0;
                if (num19 < 1.0)
                {
                    num20 = (int)(12.0 * num19);
                    num21 = 12;
                }
                else
                {
                    num20 = 12;
                    num21 = (int)(12.0 / num19);
                }
                Size size = new Size(num20, num21);
                System.Drawing.Point location = new System.Drawing.Point(num16, num17 + (12 - num21) / 2);
                graphics_0.DrawImage(bitmap, new System.Drawing.Rectangle(location, size));
                num16 += size.Width + 2;
            }
            if (bool_1)
            {
                method_198(new System.Drawing.Rectangle(num18 - 5, num17 - 5, num16 - num18 + 10, 22), graphics_0);
            }
        }

        private Bitmap method_89(Habitat habitat_1)
        {
            SizeF sizeF;
            using (Graphics graphics = CreateGraphics())
            {
                method_177(graphics);
                sizeF = graphics.MeasureString(habitat_1.Name, font_0, 300);
            }
            int num = (int)sizeF.Width;
            _ = sizeF.Height;
            int num2 = 0;
            System.Drawing.Color color = color_1;
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                if (habitat_1.Empire.Capital == habitat_1 || habitat_1.Empire.Capitals.Contains(habitat_1))
                {
                    num2 = 16;
                }
                color = ((habitat_1.Empire.PirateEmpireBaseHabitat == null) ? habitat_1.Empire.MainColor : ((!(habitat_1.Empire.MainColor == System.Drawing.Color.FromArgb(1, 1, 1))) ? habitat_1.Empire.MainColor : System.Drawing.Color.FromArgb(48, 48, 48)));
            }
            int num3 = 0;
            int num4 = 0;
            if (habitat_1.Population != null && habitat_1.Population.Count > 0)
            {
                num3 = 20;
                num4 = 20;
            }
            bool flag = false;
            if (galaxy_0.PlayerEmpire.ResourceMap != null)
            {
                flag = galaxy_0.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat_1);
            }
            int num5 = 0;
            if (habitat_1.Resources != null && habitat_1.Resources.Count > 0 && flag)
            {
                num5 = 16 * habitat_1.Resources.Count;
            }
            int num6 = num2 + num;
            int num7 = num3 + num4 + 6 + num5;
            int num8 = Math.Max(num6, num7);
            int num9 = 36;
            int num10 = num8 + 18;
            int num11 = num10 + 2;
            int num12 = num11 / 2 - num10 / 2;
            int num13 = 1;
            Bitmap bitmap = new Bitmap(num11, 38, PixelFormat.Format32bppPArgb);
            using Graphics graphics2 = Graphics.FromImage(bitmap);
            method_177(graphics2);
            int num14 = num9 / 4;
            int num15 = (int)((double)num9 * 0.5);
            List<System.Drawing.Point> list = new List<System.Drawing.Point>();
            List<System.Drawing.Point> list2 = new List<System.Drawing.Point>();
            list.Add(new System.Drawing.Point(num12, num13 + num15));
            list.Add(new System.Drawing.Point(num12 + num14, num13));
            list.Add(new System.Drawing.Point(num12 + num10 - num14, num13));
            list.Add(new System.Drawing.Point(num12 + num10, num13 + num15));
            list2.AddRange(list);
            list.Add(new System.Drawing.Point(num12 + num10 - num14, num13 + num9));
            list.Add(new System.Drawing.Point(num12 + num14, num13 + num9));
            using (Pen pen = new Pen(color, 1f))
            {
                graphics2.FillPolygon(solidBrush_2, list.ToArray());
                graphics2.FillPolygon(new SolidBrush(pen.Color), list2.ToArray());
                graphics2.DrawPolygon(pen, list.ToArray());
            }
            List<System.Drawing.Point> list3 = new List<System.Drawing.Point>();
            new List<System.Drawing.Point>();
            List<System.Drawing.Point> list4 = new List<System.Drawing.Point>();
            int num16 = 2;
            list3.Add(new System.Drawing.Point(num12 - 1, num13 + num15));
            list3.Add(new System.Drawing.Point(num12 + num14 - 1, num13 - 1));
            list3.Add(new System.Drawing.Point(num12 + num10 - num14 + 1, num13 - 1));
            list4.Add(new System.Drawing.Point(num12 + num10 - num14 + 1, num13 - 1));
            list4.Add(new System.Drawing.Point(num12 + num10 + 1, num13 + num15));
            list4.Add(new System.Drawing.Point(num12 + num10 - num14 + 1, num13 + num9 + 1));
            list4.Add(new System.Drawing.Point(num12 + num14 - 1, num13 + num9 + 1));
            list4.Add(new System.Drawing.Point(num12 - 1, num13 + num15));
            System.Drawing.Color color2 = ControlPaint.Dark(color);
            System.Drawing.Color color3 = ControlPaint.Light(color);
            using (Pen pen2 = new Pen(color3, 2f))
            {
                graphics2.DrawLines(pen2, list3.ToArray());
            }
            using (Pen pen3 = new Pen(color2, num16))
            {
                graphics2.DrawLines(pen3, list4.ToArray());
            }
            int num17 = num12 + num14 + (num8 - num6) / 2;
            int num18 = num13 + 2;
            using (Pen pen4 = new Pen(color, 1f))
            {
                if (num2 > 0)
                {
                    if (habitat_1.Empire != null && habitat_1.Empire.Capital == habitat_1)
                    {
                        graphics2.DrawImage(main_0.bitmap_44, new System.Drawing.Rectangle(num17, num18, 15, 15));
                    }
                    else
                    {
                        graphics2.DrawImage(main_0.bitmap_43, new System.Drawing.Rectangle(num17, num18, 15, 15));
                    }
                    if (bool_4)
                    {
                        method_199(new System.Drawing.Rectangle(num17 - 5, num18 - 5, 25, 25), graphics2);
                    }
                    num17 += num2;
                }
                method_266(graphics2, habitat_1.Name, font_0, new System.Drawing.Point(num17, num18));
                num17 = num12 + num14 + 3 + (num8 - num7) / 2;
                num18 = num13 + num15 + 2;
                graphics2.DrawLine(pen4, num12, num13 + num15, num12 + num10, num13 + num15);
                if (num3 > 0)
                {
                    if (habitat_1.Population != null && habitat_1.Population.DominantRace != null)
                    {
                        graphics2.DrawImage(main_0.raceImageCache_0.GetRaceImage(habitat_1.Population.DominantRace.PictureRef), new System.Drawing.Rectangle(num17, num18, 15, 15));
                    }
                    if (bool_3)
                    {
                        method_201(new System.Drawing.Rectangle(num17 - 5, num18 - 5, 25, 25), graphics2);
                    }
                    num17 += 17;
                }
                if (num4 > 0)
                {
                    SmoothingMode smoothingMode = graphics2.SmoothingMode;
                    graphics2.SmoothingMode = SmoothingMode.None;
                    method_155(habitat_1, num17, num18, graphics2);
                    graphics2.SmoothingMode = smoothingMode;
                    num17 += num4;
                }
                num17 += 4;
                graphics2.DrawLine(pen4, num17, num13 + num15, num17, num13 + num9);
                num17 += 5;
            }
            if (num5 > 0)
            {
                if (flag)
                {
                    int num19 = num17;
                    num18++;
                    HabitatResourceList habitatResourceList = habitat_1.Resources.Clone();
                    for (int i = 0; i < habitatResourceList.Count; i++)
                    {
                        Bitmap bitmap2 = main_0.esJqlOpLpG[habitatResourceList[i].PictureRef];
                        double num20 = (double)bitmap2.Width / (double)bitmap2.Height;
                        int num21 = 0;
                        int num22 = 0;
                        if (num20 < 1.0)
                        {
                            num21 = (int)(12.0 * num20);
                            num22 = 12;
                        }
                        else
                        {
                            num21 = 12;
                            num22 = (int)(12.0 / num20);
                        }
                        Size size = new Size(num21, num22);
                        System.Drawing.Point location = new System.Drawing.Point(num17, num18 + (12 - num22) / 2);
                        graphics2.DrawImage(bitmap2, new System.Drawing.Rectangle(location, size));
                        num17 += size.Width + 2;
                    }
                    if (bool_1)
                    {
                        method_198(new System.Drawing.Rectangle(num19 - 5, num18 - 5, num17 - num19 + 10, 22), graphics2);
                        return bitmap;
                    }
                    return bitmap;
                }
                return bitmap;
            }
            return bitmap;
        }

        private void method_90(int int_11, int int_12, double double_15, out double double_16, out double double_17)
        {
            double num = (double)base.ClientRectangle.Width / 2.0;
            double y = (double)base.ClientRectangle.Height / 2.0;
            double_16 = ((double)int_11 - num) / num;
            int num2 = (int)main_0._Game.Galaxy.CalculateDistance(num, y, 0.0, 0.0);
            num2 = (int)((double)num2 * 1.5);
            int num3 = (int)main_0._Game.Galaxy.CalculateDistance(num, y, int_11, int_12);
            double val = (double)(num2 - num3) / (double)num2;
            val = Math.Max(0.02, val);
            double num4 = Math.Max(1.0, Math.Sqrt(double_15));
            double_17 = val / num4;
            if (double_15 > 50.0)
            {
                double_17 = 0.0;
            }
        }

        private void method_91(double double_15, double double_16, double double_17)
        {
            bool flag = false;
            long currentStarDate = galaxy_0.CurrentStarDate;
            if (currentStarDate <= long_0)
            {
                return;
            }
            if (double_17 < 100.0)
            {
                double range = double_17 * double_9;
                GalaxyLocationList galaxyLocationList = galaxy_0.DetermineGalaxyLocationsInRangeAtPoint(double_15, double_16, range, GalaxyLocationType.RestrictedArea);
                if (galaxyLocationList != null && galaxyLocationList.Count > 0)
                {
                    for (int i = 0; i < galaxyLocationList.Count; i++)
                    {
                        GalaxyLocation galaxyLocation = galaxyLocationList[i];
                        if (galaxyLocation.SoundScheme >= 0)
                        {
                            flag = true;
                            double balance = 0.0;
                            double distance = 200.0;
                            int nextEffectOffset = 8000;
                            SoundEffectRequest soundEffectRequest = main_0.EffectsPlayer.ResolveAmbientEffect(galaxyLocation.SoundScheme, balance, distance, out nextEffectOffset);
                            if (soundEffectRequest != null)
                            {
                                main_0.method_0(soundEffectRequest);
                            }
                            long_0 = currentStarDate + nextEffectOffset;
                        }
                    }
                }
            }
            if (flag && main_0.musicPlayer_0.IsPlaying && !main_0.musicPlayer_0.IsInitiatingFade && main_0.double_0 < 100.0)
            {
                main_0.musicPlayer_0.FadePause();
            }
            else if (main_0._Game.Galaxy.TimeState == GalaxyTimeState.Running && (!flag || main_0.double_0 >= 100.0) && (!main_0.musicPlayer_0.IsPlaying || main_0.musicPlayer_0.mediaPlayer_0.Volume <= 0.0) && !main_0.musicPlayer_1.IsPlaying && !main_0.musicPlayer_0.IsInitiatingFade)
            {
                main_0.musicPlayer_0.FadeResume();
            }
        }

        private void method_92(Habitat habitat_1, int int_11, int int_12, bool bool_13)
        {
            long currentStarDate = galaxy_0.CurrentStarDate;
            if (habitat_1.Category == HabitatCategoryType.Star)
            {
                if (currentStarDate > habitat_1.NextSoundTime)
                {
                    double double_ = 0.0;
                    double double_2 = 0.0;
                    method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                    SoundEffectRequest soundEffectRequest = main_0.EffectsPlayer.ResolveStar(habitat_1.Type, double_, double_2);
                    if (soundEffectRequest != null)
                    {
                        main_0.method_0(soundEffectRequest);
                    }
                    habitat_1.NextSoundTime = currentStarDate + 4200L;
                }
            }
            else
            {
                if ((habitat_1.Category != HabitatCategoryType.Planet && habitat_1.Category != HabitatCategoryType.Moon) || currentStarDate <= habitat_1.NextSoundTime || habitat_1.ConstructionQueue == null || habitat_1.ConstructionQueue.ConstructionYards == null || habitat_1.ConstructionQueue.ConstructionYards.CountUnderConstruction <= 0 || !galaxy_0.PlayerEmpire.IsObjectVisibleToThisEmpire(habitat_1))
                {
                    return;
                }
                double double_3 = 0.0;
                double double_4 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_3, out double_4);
                main_0.method_0(main_0.EffectsPlayer.ResolveConstruction(double_3, double_4));
                habitat_1.NextSoundTime = currentStarDate + 4100L;
                int val = (int)Math.Sqrt(habitat_1.Diameter * 10);
                BuiltObject shipUnderConstruction = habitat_1.ConstructionQueue.ConstructionYards[0].ShipUnderConstruction;
                double num = 0.0;
                double num2 = 0.0;
                if (shipUnderConstruction != null)
                {
                    val = (int)Math.Sqrt(shipUnderConstruction.Size * 15);
                    if ((shipUnderConstruction.ParentBuiltObject != null || shipUnderConstruction.ParentHabitat != null) && shipUnderConstruction.ParentOffsetX > -2000000001.0 && shipUnderConstruction.ParentOffsetY > -2000000001.0)
                    {
                        num = shipUnderConstruction.ParentOffsetX;
                        num2 = shipUnderConstruction.ParentOffsetY;
                    }
                }
                val = Math.Min(100, val);
                double num3 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                double double_5 = 0.0;
                double double_6 = 0.0;
                method_96((int)((double)val * 0.5), num3, out double_5, out double_6);
                if (bool_13)
                {
                    DistantWorlds.Animation animation = new DistantWorlds.Animation(texture2D_33, galaxy_0.CurrentDateTime, 30, habitat_1.Xpos + num + double_5, habitat_1.Ypos + num2 + double_6, val, val, num3, System.Drawing.Color.Empty);
                    animation.DisposeTexturesWhenComplete = false;
                    animationSystem_0.AddAnimation(animation);
                }
                else
                {
                    DistantWorlds.Animation animation2 = new DistantWorlds.Animation(main_0.bitmap_212, galaxy_0.CurrentDateTime, 30, habitat_1.Xpos + num + double_5, habitat_1.Ypos + num2 + double_6, val, val, num3, System.Drawing.Color.Empty);
                    animationSystem_0.AddAnimation(animation2);
                }
            }
        }

        private void method_93(Graphics graphics_0, DateTime dateTime_5, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, int int_15)
        {
            if (builtObject_1.ShieldAreaRechargeTarget == null || !(builtObject_1.ShieldAreaRechargeStartTime > DateTime.MinValue))
            {
                return;
            }
            if (builtObject_1.ShieldAreaRechargeTarget.CurrentSpeed >= (float)builtObject_1.ShieldAreaRechargeTarget.WarpSpeed && builtObject_1.ShieldAreaRechargeTarget.WarpSpeed > 0)
            {
                builtObject_1.ShieldAreaRechargeStartTime = DateTime.MinValue;
                builtObject_1.ShieldAreaRechargeTarget = null;
            }
            else
            {
                if (!(dateTime_5 > builtObject_1.ShieldAreaRechargeStartTime))
                {
                    return;
                }
                System.Drawing.Color color = method_214(System.Drawing.Color.FromArgb(56, 80, 32, 192), System.Drawing.Color.FromArgb(8, 32, 192, 255), dateTime_5);
                System.Drawing.Point pt = new System.Drawing.Point(int_11, int_12);
                System.Drawing.Point pt2 = new System.Drawing.Point(int_14, int_15);
                if (pt.X == pt2.X && pt.Y == pt2.Y)
                {
                    pt2 = new System.Drawing.Point(int_14 + 1, int_15 + 1);
                }
                using SolidBrush brush = new SolidBrush(color);
                using Pen pen = new Pen(brush);
                pen.Width = int_13;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
                graphics_0.DrawLine(pen, pt, pt2);
                graphics_0.SmoothingMode = SmoothingMode.None;
            }
        }

        private void method_94(SpriteBatch spriteBatch_2, DateTime dateTime_5, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, int int_15)
        {
            if (builtObject_1.ShieldAreaRechargeTarget == null || !(builtObject_1.ShieldAreaRechargeStartTime > DateTime.MinValue))
            {
                return;
            }
            if (builtObject_1.ShieldAreaRechargeTarget.CurrentSpeed >= (float)builtObject_1.ShieldAreaRechargeTarget.WarpSpeed && builtObject_1.ShieldAreaRechargeTarget.WarpSpeed > 0)
            {
                builtObject_1.ShieldAreaRechargeStartTime = DateTime.MinValue;
                builtObject_1.ShieldAreaRechargeTarget = null;
            }
            else if (dateTime_5 > builtObject_1.ShieldAreaRechargeStartTime)
            {
                System.Drawing.Color color = method_214(System.Drawing.Color.FromArgb(56, 80, 32, 192), System.Drawing.Color.FromArgb(8, 32, 192, 255), dateTime_5);
                System.Drawing.Point start = new System.Drawing.Point(int_11, int_12);
                System.Drawing.Point end = new System.Drawing.Point(int_14, int_15);
                if (start.X == end.X && start.Y == end.Y)
                {
                    end = new System.Drawing.Point(int_14 + 1, int_15 + 1);
                }
                XnaDrawingHelper.DrawLineCustomTexture(spriteBatch_2, start, end, texture2D_17, color, int_13);
            }
        }

        private void method_95(BuiltObject builtObject_1, int int_11, int int_12, bool bool_13)
        {
            long currentStarDate = galaxy_0.CurrentStarDate;
            if (builtObject_1.DoingConstruction && currentStarDate > builtObject_1.NextSoundTimeConstruction)
            {
                double double_ = 0.0;
                double double_2 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                main_0.method_0(main_0.EffectsPlayer.ResolveConstruction(double_, double_2));
                builtObject_1.NextSoundTimeConstruction = currentStarDate + 4100L;
                int val = (int)Math.Sqrt(builtObject_1.Size * 15);
                val = Math.Min(100, val);
                double num = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                double double_3 = 0.0;
                double double_4 = 0.0;
                method_96((int)((double)val * 1.0), num, out double_3, out double_4);
                if (bool_13)
                {
                    DistantWorlds.Animation animation = new DistantWorlds.Animation(texture2D_33, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_3, builtObject_1.Ypos + double_4, val, val, num, System.Drawing.Color.Empty);
                    animation.DisposeTexturesWhenComplete = false;
                    animationSystem_0.AddAnimation(animation);
                }
                else
                {
                    DistantWorlds.Animation animation2 = new DistantWorlds.Animation(main_0.bitmap_212, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_3, builtObject_1.Ypos + double_4, val, val, num, System.Drawing.Color.Empty);
                    animationSystem_0.AddAnimation(animation2);
                }
            }
            if (builtObject_1.DoingMining && currentStarDate > builtObject_1.NextSoundTimeMining)
            {
                double double_5 = 0.0;
                double double_6 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_5, out double_6);
                main_0.method_0(main_0.EffectsPlayer.ResolveMining(double_5, double_6));
                builtObject_1.NextSoundTimeMining = currentStarDate + 3000L;
                if (builtObject_1.ParentHabitat != null && (builtObject_1.ParentHabitat.Category == HabitatCategoryType.Asteroid || builtObject_1.ParentHabitat.Type == HabitatType.BarrenRock || builtObject_1.ParentHabitat.Type == HabitatType.Volcanic))
                {
                    int val2 = (int)Math.Sqrt(builtObject_1.Size * 15);
                    val2 = Math.Min(60, val2);
                    double num2 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                    double double_7 = 0.0;
                    double double_8 = 0.0;
                    method_96((int)((double)val2 * 0.8), num2, out double_7, out double_8);
                    if (bool_13)
                    {
                        DistantWorlds.Animation animation3 = new DistantWorlds.Animation(texture2D_31, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_7, builtObject_1.Ypos + double_8, val2, val2, num2, System.Drawing.Color.Empty);
                        animation3.DisposeTexturesWhenComplete = false;
                        animationSystem_0.AddAnimation(animation3);
                    }
                    else
                    {
                        DistantWorlds.Animation animation4 = new DistantWorlds.Animation(main_0.bitmap_210, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_7, builtObject_1.Ypos + double_8, val2, val2, num2, System.Drawing.Color.Empty);
                        animationSystem_0.AddAnimation(animation4);
                    }
                }
            }
            if (!builtObject_1.DoingGasMining || currentStarDate <= builtObject_1.NextSoundTimeGasMining)
            {
                return;
            }
            double double_9 = 0.0;
            double double_10 = 0.0;
            method_90(int_11, int_12, main_0.double_0, out double_9, out double_10);
            main_0.method_0(main_0.EffectsPlayer.ResolveGasMining(double_9, double_10));
            builtObject_1.NextSoundTimeGasMining = currentStarDate + 5600L;
            int val3 = (int)Math.Sqrt(builtObject_1.Size * 15);
            val3 = Math.Min(80, val3);
            double num3 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
            double double_11 = 0.0;
            double double_12 = 0.0;
            method_96((int)((double)val3 * 0.7), num3, out double_11, out double_12);
            System.Drawing.Color tintColor = System.Drawing.Color.Empty;
            if (builtObject_1.ParentHabitat != null && builtObject_1.ParentHabitat.Resources != null && builtObject_1.ParentHabitat.Resources.Count > 0)
            {
                Resource resource = null;
                int num4 = 0;
                HabitatResourceList habitatResourceList = builtObject_1.ParentHabitat.Resources.Clone();
                for (int i = 0; i < habitatResourceList.Count; i++)
                {
                    HabitatResource habitatResource = habitatResourceList[i];
                    if (habitatResource.Group == ResourceGroup.Gas && habitatResource.Abundance > num4)
                    {
                        num4 = habitatResource.Abundance;
                        resource = habitatResource;
                    }
                }
                if (resource != null)
                {
                    tintColor = resource.Name switch
                    {
                        "Argon" => System.Drawing.Color.FromArgb(255, 255, 0),
                        "Caslon" => System.Drawing.Color.FromArgb(0, 0, 255),
                        "Helium" => System.Drawing.Color.FromArgb(204, 0, 51),
                        "Hydrogen" => System.Drawing.Color.FromArgb(160, 0, 0),
                        "Krypton" => System.Drawing.Color.FromArgb(0, 255, 0),
                        "Tyderios" => System.Drawing.Color.FromArgb(255, 0, 255),
                        _ => System.Drawing.Color.FromArgb(204, 0, 51),
                    };
                }
            }
            if (bool_13)
            {
                DistantWorlds.Animation animation5 = new DistantWorlds.Animation(texture2D_32, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_11, builtObject_1.Ypos + double_12, val3, val3, num3, tintColor);
                animation5.DisposeTexturesWhenComplete = false;
                animationSystem_0.AddAnimation(animation5);
            }
            else
            {
                DistantWorlds.Animation animation6 = new DistantWorlds.Animation(main_0.bitmap_211, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_11, builtObject_1.Ypos + double_12, val3, val3, num3, tintColor);
                animationSystem_0.AddAnimation(animation6);
            }
        }

        private void method_96(int int_11, double double_15, out double double_16, out double double_17)
        {
            double num = (double)int_11 / 2.0;
            double_16 = Math.Cos(double_15) * num;
            double_17 = Math.Sin(double_15) * num;
        }

        private void method_97(BuiltObject builtObject_1, int int_11, int int_12, long long_1, bool bool_13)
        {
            long num = builtObject_1.HyperjumpCountdown - long_1;
            if (num > 0L && builtObject_1.HyperEnterStartAnimation && num < 800L && builtObject_1.CanHyperJump)
            {
                int num2 = (int)Math.Sqrt(builtObject_1.Size * 30);
                double double_ = 0.0;
                double double_2 = 0.0;
                method_96((int)((double)num2 * 0.7), (double)builtObject_1.TargetHeading + Math.PI, out double_, out double_2);
                if (bool_13)
                {
                    DistantWorlds.Animation animation = new DistantWorlds.Animation(list_26[builtObject_1.Design.HyperDriveIndex], galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_, builtObject_1.Ypos + double_2, num2, num2, (double)builtObject_1.TargetHeading * -1.0, System.Drawing.Color.Empty);
                    animation.DisposeTexturesWhenComplete = false;
                    animationSystem_0.AddAnimation(animation);
                }
                else
                {
                    DistantWorlds.Animation animation2 = new DistantWorlds.Animation(main_0.list_3[builtObject_1.Design.HyperDriveIndex], galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_, builtObject_1.Ypos + double_2, num2, num2, builtObject_1.TargetHeading, System.Drawing.Color.Empty);
                    animationSystem_0.AddAnimation(animation2);
                }
                builtObject_1.HyperEnterStartAnimation = false;
            }
            if (builtObject_1.HyperjumpAboutToEnter && !builtObject_1.HyperjumpAboutToEnterSoundPlayed)
            {
                double double_3 = 0.0;
                double double_4 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_3, out double_4);
                main_0.method_0(main_0.EffectsPlayer.ResolveHyperjumpEntry(double_3, double_4));
                builtObject_1.HyperjumpAboutToEnterSoundPlayed = true;
            }
            if (!builtObject_1.HyperjumpJustExited)
            {
                return;
            }
            double double_5 = 0.0;
            double double_6 = 0.0;
            method_90(int_11, int_12, main_0.double_0, out double_5, out double_6);
            main_0.method_0(main_0.EffectsPlayer.ResolveHyperjumpExit(double_5, double_6));
            if (builtObject_1.HyperExitStartAnimation)
            {
                int num3 = (int)Math.Sqrt(builtObject_1.Size * 30);
                double double_7 = 0.0;
                double double_8 = 0.0;
                method_96((int)((double)num3 * 0.7), (double)builtObject_1.TargetHeading + Math.PI, out double_7, out double_8);
                if (bool_13)
                {
                    DistantWorlds.Animation animation3 = new DistantWorlds.Animation(list_27[builtObject_1.Design.HyperDriveIndex], galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_7, builtObject_1.Ypos + double_8, num3, num3, (double)builtObject_1.TargetHeading * -1.0, System.Drawing.Color.Empty);
                    animation3.DisposeTexturesWhenComplete = false;
                    animationSystem_0.AddAnimation(animation3);
                }
                else
                {
                    DistantWorlds.Animation animation4 = new DistantWorlds.Animation(main_0.list_4[builtObject_1.Design.HyperDriveIndex], galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_7, builtObject_1.Ypos + double_8, num3, num3, builtObject_1.TargetHeading, System.Drawing.Color.Empty);
                    animationSystem_0.AddAnimation(animation4);
                }
            }
            builtObject_1.HyperExitStartAnimation = false;
        }

        private void method_98(int int_11, int int_12, int int_13, int int_14, Graphics graphics_0)
        {
            using Pen pen = new Pen(System.Drawing.Color.Orange, 2f);
            pen.DashStyle = DashStyle.Dash;
            graphics_0.DrawEllipse(pen, int_11, int_12, int_13, int_14);
        }

        private void method_99(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14)
        {
            XnaDrawingHelper.DrawCircle(spriteBatch_2, int_11, int_12, int_13, int_14, System.Drawing.Color.Orange, 2, 100, dashed: true);
        }

        private void method_100(Habitat habitat_1, int int_11, int int_12, Graphics graphics_0)
        {
            int num = 0;
            HabitatResourceList habitatResourceList = habitat_1.Resources.Clone();
            for (int i = 0; i < habitatResourceList.Count; i++)
            {
                HabitatResource habitatResource = habitatResourceList[i];
                Bitmap image = bitmap_6[habitatResource.PictureRef];
                int num2 = 0;
                int num3 = 0;
                if (habitat_1.Diameter >= 30)
                {
                    switch (num)
                    {
                        case 0:
                            num2 = -19;
                            num3 = 3;
                            break;
                        case 1:
                            num2 = -37;
                            num3 = 3;
                            break;
                        case 2:
                            num2 = -19;
                            num3 = 21;
                            break;
                        case 3:
                            num2 = -37;
                            num3 = 21;
                            break;
                        case 4:
                            num2 = -19;
                            num3 = 39;
                            break;
                    }
                }
                else
                {
                    switch (num)
                    {
                        case 0:
                            num2 = 2;
                            num3 = -18;
                            break;
                        case 1:
                            num2 = -16;
                            num3 = -18;
                            break;
                        case 2:
                            num2 = 2;
                            num3 = 0;
                            break;
                        case 3:
                            num2 = -34;
                            num3 = -18;
                            break;
                        case 4:
                            num2 = 2;
                            num3 = 18;
                            break;
                    }
                }
                System.Drawing.Point point = new System.Drawing.Point(int_11 + num2, int_12 + num3);
                graphics_0.DrawImageUnscaled(image, point);
                num++;
            }
            if (habitat_0 != null && habitat_1 == habitat_0 && bool_1)
            {
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(int_11 - 41, int_12, 41, 41);
                method_198(rectangle_, graphics_0);
            }
        }

        private StarFieldItemList method_101(int int_11, int int_12, int int_13)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StarFieldItemList starFieldItemList = new StarFieldItemList();
            for (int i = 0; i < int_12; i++)
            {
                StarFieldItem item = default(StarFieldItem);
                item.X = random.Next(0, int_11);
                item.Y = random.Next(0, int_11);
                item.Size = int_13;
                starFieldItemList.Add(item);
            }
            return starFieldItemList;
        }

        private void method_102(SpriteBatch spriteBatch_2, Brush brush_0, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12)
        {
            int num = (int)((double)main_0.int_13 / (double)int_12) % int_11;
            int num2 = (int)((double)main_0.int_14 / (double)int_12) % int_11;
            int num3 = num - base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = num + base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num5 = num2 - base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num6 = num2 + base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            float num7 = method_45();
            new Microsoft.Xna.Framework.Color(1f, 1f, 1f, num7);
            int alpha = Math.Max(0, Math.Min(255, (int)(num7 * 255f)));
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            if (num3 < 0)
            {
                num8 = num3 + int_11;
                num9 = int_11;
            }
            if (num4 > int_11)
            {
                num8 = 0;
                num9 = num4 - int_11;
            }
            if (num5 < 0)
            {
                num10 = num5 + int_11;
                num11 = int_11;
            }
            if (num6 > int_11)
            {
                num10 = 0;
                num11 = num6 - int_11;
            }
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            new System.Drawing.Rectangle(num8, num5, num9 - num8, num6 - num5);
            new System.Drawing.Rectangle(num3, num10, num4 - num3, num11 - num10);
            new System.Drawing.Rectangle(num8, num10, num9 - num8, num11 - num10);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num12 = starFieldItemList_4[i].X - num3;
                int num13 = starFieldItemList_4[i].Y - num5;
                if (num12 > int_11)
                {
                    num12 -= int_11;
                }
                else if (num12 < 0)
                {
                    num12 += int_11;
                }
                if (num13 > int_11)
                {
                    num13 -= int_11;
                }
                else if (num13 < 0)
                {
                    num13 += int_11;
                }
                rectangle_5[i].X = num12;
                rectangle_5[i].Y = num13;
                int num14 = 128;
                int num15 = Galaxy.Rnd.Next(0, 127);
                int num16 = Galaxy.Rnd.Next(0, 127);
                int num17 = Galaxy.Rnd.Next(0, 127);
                using (new SolidBrush(System.Drawing.Color.FromArgb(128 + num15, 128 + num16, 128 + num17)))
                {
                    int num18 = i % texture2D_19.Length;
                    Texture2D texture = texture2D_19[num18];
                    XnaDrawingHelper.DrawTexture(spriteBatch_0, texture, rectangle_5[i], 0f, System.Drawing.Color.FromArgb(alpha, num14 + num15, num14 + num16, num14 + num17));
                }
            }
        }

        private void method_103(Brush brush_0, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12, Graphics graphics_0)
        {
            int num = (int)((double)main_0.int_13 / main_0.double_0 / (double)int_12) % int_11;
            int num2 = (int)((double)main_0.int_14 / main_0.double_0 / (double)int_12) % int_11;
            int num3 = num - base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = num + base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num5 = num2 - base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num6 = num2 + base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            if (num3 < 0)
            {
                num7 = num3 + int_11;
                num8 = int_11;
            }
            if (num4 > int_11)
            {
                num7 = 0;
                num8 = num4 - int_11;
            }
            if (num5 < 0)
            {
                num9 = num5 + int_11;
                num10 = int_11;
            }
            if (num6 > int_11)
            {
                num9 = 0;
                num10 = num6 - int_11;
            }
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            new System.Drawing.Rectangle(num7, num5, num8 - num7, num6 - num5);
            new System.Drawing.Rectangle(num3, num9, num4 - num3, num10 - num9);
            new System.Drawing.Rectangle(num7, num9, num8 - num7, num10 - num9);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num11 = starFieldItemList_4[i].X - num3;
                int num12 = starFieldItemList_4[i].Y - num5;
                if (num11 > int_11)
                {
                    num11 -= int_11;
                }
                else if (num11 < 0)
                {
                    num11 += int_11;
                }
                if (num12 > int_11)
                {
                    num12 -= int_11;
                }
                else if (num12 < 0)
                {
                    num12 += int_11;
                }
                rectangle_5[i].X = num11;
                rectangle_5[i].Y = num12;
                int num13 = Galaxy.Rnd.Next(0, 72);
                int num14 = Galaxy.Rnd.Next(0, 72);
                int num15 = Galaxy.Rnd.Next(0, 72);
                using SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(136 + num13, 136 + num14, 136 + num15));
                graphics_0.FillEllipse(brush, rectangle_5[i]);
            }
        }

        private void method_104(Brush brush_0, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12, Graphics graphics_0)
        {
            int num = (int)((double)main_0.int_13 / main_0.double_0 / (double)int_12) % int_11;
            int num2 = (int)((double)main_0.int_14 / main_0.double_0 / (double)int_12) % int_11;
            int num3 = num - base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = num + base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num5 = num2 - base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num6 = num2 + base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            if (num3 < 0)
            {
                num7 = num3 + int_11;
                num8 = int_11;
            }
            if (num4 > int_11)
            {
                num7 = 0;
                num8 = num4 - int_11;
            }
            if (num5 < 0)
            {
                num9 = num5 + int_11;
                num10 = int_11;
            }
            if (num6 > int_11)
            {
                num9 = 0;
                num10 = num6 - int_11;
            }
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            new System.Drawing.Rectangle(num7, num5, num8 - num7, num6 - num5);
            new System.Drawing.Rectangle(num3, num9, num4 - num3, num10 - num9);
            new System.Drawing.Rectangle(num7, num9, num8 - num7, num10 - num9);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num11 = starFieldItemList_4[i].X - num3;
                int num12 = starFieldItemList_4[i].Y - num5;
                if (num11 > int_11)
                {
                    num11 -= int_11;
                }
                else if (num11 < 0)
                {
                    num11 += int_11;
                }
                if (num12 > int_11)
                {
                    num12 -= int_11;
                }
                else if (num12 < 0)
                {
                    num12 += int_11;
                }
                rectangle_5[i].X = num11;
                rectangle_5[i].Y = num12;
            }
            if (rectangle_5.Length > 0)
            {
                graphics_0.FillRectangles(brush_0, rectangle_5);
            }
        }

        private void method_105(Brush brush_0, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12, Graphics graphics_0)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            if (int_11 > int_11)
            {
                num3 = 0;
                num4 = int_11 - int_11;
            }
            if (num2 < 0)
            {
                num5 = num2 + int_11;
                num6 = int_11;
            }
            if (int_11 > int_11)
            {
                num5 = 0;
                num6 = int_11 - int_11;
            }
            new System.Drawing.Rectangle(num, num2, int_11 - num, int_11 - num2);
            new System.Drawing.Rectangle(num3, num2, num4 - num3, int_11 - num2);
            new System.Drawing.Rectangle(num, num5, int_11 - num, num6 - num5);
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num7 = starFieldItemList_4[i].X - num;
                int num8 = starFieldItemList_4[i].Y - num2;
                if (num7 > int_11)
                {
                    num7 -= int_11;
                }
                else if (num7 < 0)
                {
                    num7 += int_11;
                }
                if (num8 > int_11)
                {
                    num8 -= int_11;
                }
                else if (num8 < 0)
                {
                    num8 += int_11;
                }
                rectangle_5[i].X = num7;
                rectangle_5[i].Y = num8;
            }
            using ImageAttributes imageAttr = method_220(((SolidBrush)brush_0).Color);
            for (int j = 0; j < rectangle_5.Length; j++)
            {
                int num9 = j % main_0.bitmap_198.Length;
                Bitmap bitmap = main_0.bitmap_198[num9];
                graphics_0.DrawImage(bitmap, rectangle_5[j], 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr);
            }
        }

        private void method_106(System.Drawing.Color color_18, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12, SpriteBatch spriteBatch_2)
        {
            int num = (int)((double)main_0.int_13 / main_0.double_0 / (double)int_12) % int_11;
            int num2 = (int)((double)main_0.int_14 / main_0.double_0 / (double)int_12) % int_11;
            int num3 = num - base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = num + base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num5 = num2 - base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num6 = num2 + base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            if (num3 < 0)
            {
                num7 = num3 + int_11;
                num8 = int_11;
            }
            if (num4 > int_11)
            {
                num7 = 0;
                num8 = num4 - int_11;
            }
            if (num5 < 0)
            {
                num9 = num5 + int_11;
                num10 = int_11;
            }
            if (num6 > int_11)
            {
                num9 = 0;
                num10 = num6 - int_11;
            }
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            new System.Drawing.Rectangle(num7, num5, num8 - num7, num6 - num5);
            new System.Drawing.Rectangle(num3, num9, num4 - num3, num10 - num9);
            new System.Drawing.Rectangle(num7, num9, num8 - num7, num10 - num9);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num11 = starFieldItemList_4[i].X - num3;
                int num12 = starFieldItemList_4[i].Y - num5;
                if (num11 > int_11)
                {
                    num11 -= int_11;
                }
                else if (num11 < 0)
                {
                    num11 += int_11;
                }
                if (num12 > int_11)
                {
                    num12 -= int_11;
                }
                else if (num12 < 0)
                {
                    num12 += int_11;
                }
                rectangle_5[i].X = num11;
                rectangle_5[i].Y = num12;
            }
            if (rectangle_5.Length > 0)
            {
                for (int j = 0; j < rectangle_5.Length; j++)
                {
                    XnaDrawingHelper.FillRectangle(spriteBatch_2, rectangle_5[j], color_18);
                }
            }
        }

        private void method_107(int int_11, int int_12, out Texture2D texture2D_61, out Texture2D texture2D_62, out Texture2D texture2D_63)
        {
            texture2D_61 = null;
            texture2D_62 = null;
            texture2D_63 = null;
            method_46();
            using (Bitmap bitmap = new Bitmap(int_11, int_12, PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics_ = Graphics.FromImage(bitmap))
                {
                    method_175(graphics_);
                    method_105(solidBrush_9, starFieldItemList_3, rectangle_4, int_4, 38, graphics_);
                }
                texture2D_61 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, bitmap, useAlternateBuffer: true);
            }
            using (Bitmap bitmap2 = new Bitmap(int_11, int_12, PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics_2 = Graphics.FromImage(bitmap2))
                {
                    method_175(graphics_2);
                    method_105(solidBrush_8, starFieldItemList_2, rectangle_3, int_3, 20, graphics_2);
                }
                texture2D_62 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, bitmap2, useAlternateBuffer: true);
            }
            using Bitmap bitmap3 = new Bitmap(int_11, int_12, PixelFormat.Format32bppArgb);
            using (Graphics graphics_3 = Graphics.FromImage(bitmap3))
            {
                method_175(graphics_3);
                method_105(solidBrush_7, starFieldItemList_1, rectangle_2, int_2, 11, graphics_3);
            }
            texture2D_63 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, bitmap3, useAlternateBuffer: true);
        }

        private void method_108(SpriteBatch spriteBatch_2)
        {
            float float_ = method_45();
            int num = (int)((double)main_0.int_21 / main_0.double_0);
            int num2 = (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int int_ = num + (int)((double)main_0.int_13 / main_0.double_0 / 38.0) % this.int_4;
            int int_2 = num2 + (int)((double)main_0.int_14 / main_0.double_0 / 38.0) % this.int_4;
            method_110(spriteBatch_2, texture2D_53, int_, int_2, base.ClientRectangle.Width, base.ClientRectangle.Height, float_);
            int int_3 = (int)((double)main_0.int_13 / main_0.double_0 / 20.0) % this.int_3;
            int int_4 = (int)((double)main_0.int_14 / main_0.double_0 / 20.0) % this.int_3;
            method_110(spriteBatch_2, texture2D_54, int_3, int_4, base.ClientRectangle.Width, base.ClientRectangle.Height, float_);
            int int_5 = (int)((double)main_0.int_13 / main_0.double_0 / 11.0) % this.int_2;
            int int_6 = (int)((double)main_0.int_14 / main_0.double_0 / 11.0) % this.int_2;
            method_110(spriteBatch_2, texture2D_55, int_5, int_6, base.ClientRectangle.Width, base.ClientRectangle.Height, float_);
        }

        private void method_109(SpriteBatch spriteBatch_2, Texture2D texture2D_61, int int_11, int int_12, int int_13, int int_14)
        {
            method_110(spriteBatch_2, texture2D_61, int_11, int_12, int_13, int_14, 1f);
        }

        private void method_110(SpriteBatch spriteBatch_2, Texture2D texture2D_61, int int_11, int int_12, int int_13, int int_14, float float_0)
        {
            if (texture2D_61 == null || texture2D_61.IsDisposed)
            {
                return;
            }
            int num = 0;
            int num2 = 0;
            int val = Math.Min(int_13 - int_11, texture2D_61.Width - int_11);
            int val2 = Math.Min(int_14 - int_12, texture2D_61.Height - int_12);
            Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(1f, 1f, 1f, float_0);
            int num3 = int_11;
            while (num < int_13)
            {
                int num4 = int_12;
                while (num2 < int_14)
                {
                    Microsoft.Xna.Framework.Rectangle value = new Microsoft.Xna.Framework.Rectangle(num3, num4, val, val2);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle = new Microsoft.Xna.Framework.Rectangle(num, num2, value.Width, value.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle, value, color);
                    num2 += Math.Max(1, val2);
                    num4 = 0;
                    val2 = Math.Min(int_14 - num2, texture2D_61.Height);
                }
                num2 = 0;
                num += Math.Max(1, val);
                num3 = 0;
                val = Math.Min(int_13 - num, texture2D_61.Width);
                val2 = Math.Min(int_14 - 0, texture2D_61.Height - int_12);
            }
        }

        private void method_111(SpriteBatch spriteBatch_2, Texture2D texture2D_61, int int_11, int int_12, int int_13, int int_14)
        {
            if (texture2D_61 == null || texture2D_61.IsDisposed)
            {
                return;
            }
            int num = int_11 + int_13;
            int num2 = int_12 + int_14;
            int num3 = Math.Min(texture2D_61.Width - int_11, num);
            int num4 = Math.Min(texture2D_61.Height - int_12, num2);
            Microsoft.Xna.Framework.Rectangle value = new Microsoft.Xna.Framework.Rectangle(int_11, int_12, num3, num4);
            Microsoft.Xna.Framework.Rectangle destinationRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, value.Width, value.Height);
            spriteBatch_2.Draw(texture2D_61, destinationRectangle, value, Microsoft.Xna.Framework.Color.White);
            num -= texture2D_61.Width;
            num2 -= texture2D_61.Height;
            if (num > 0)
            {
                Microsoft.Xna.Framework.Rectangle value2 = new Microsoft.Xna.Framework.Rectangle(0, int_12, num, num4);
                Microsoft.Xna.Framework.Rectangle destinationRectangle2 = new Microsoft.Xna.Framework.Rectangle(value.Width, 0, value2.Width, value2.Height);
                spriteBatch_2.Draw(texture2D_61, destinationRectangle2, value2, Microsoft.Xna.Framework.Color.White);
                if (num2 > 0)
                {
                    Microsoft.Xna.Framework.Rectangle value3 = new Microsoft.Xna.Framework.Rectangle(int_11, 0, num3, num2);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle3 = new Microsoft.Xna.Framework.Rectangle(0, value.Height, value3.Width, value3.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle3, value3, Microsoft.Xna.Framework.Color.White);
                    Microsoft.Xna.Framework.Rectangle value4 = new Microsoft.Xna.Framework.Rectangle(0, 0, num, num2);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle4 = new Microsoft.Xna.Framework.Rectangle(value.Width, value.Height, value4.Width, value4.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle4, value4, Microsoft.Xna.Framework.Color.White);
                }
            }
            else if (num2 > 0)
            {
                Microsoft.Xna.Framework.Rectangle value5 = new Microsoft.Xna.Framework.Rectangle(int_11, 0, num3, num2);
                Microsoft.Xna.Framework.Rectangle destinationRectangle5 = new Microsoft.Xna.Framework.Rectangle(0, value.Height, value5.Width, value5.Height);
                spriteBatch_2.Draw(texture2D_61, destinationRectangle5, value5, Microsoft.Xna.Framework.Color.White);
                if (num > 0)
                {
                    Microsoft.Xna.Framework.Rectangle value6 = new Microsoft.Xna.Framework.Rectangle(0, int_12, num, num4);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle6 = new Microsoft.Xna.Framework.Rectangle(value.Width, 0, value6.Width, value6.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle6, value6, Microsoft.Xna.Framework.Color.White);
                    Microsoft.Xna.Framework.Rectangle value7 = new Microsoft.Xna.Framework.Rectangle(0, 0, num, num2);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle7 = new Microsoft.Xna.Framework.Rectangle(value.Width, value.Height, value7.Width, value7.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle7, value7, Microsoft.Xna.Framework.Color.White);
                }
            }
        }

        private void method_112(Creature creature_0, Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, Bitmap[] bitmap_7, Bitmap[] bitmap_8, DateTime dateTime_5, int int_15, double double_15)
        {
            Bitmap[] array = bitmap_7;
            int num = 0;
            if (creature_0.CurrentTarget != null && creature_0.DistanceToTarget <= 40.0 && bitmap_8 != null && bitmap_8.Length > 0)
            {
                array = bitmap_8;
                num = 5;
            }
            int num2 = (int)((double)array.Length / (double)int_15 * 1000.0);
            int num3 = Math.Max(1, array.Length - 1);
            int num4 = num2 / num3;
            long num5 = dateTime_5.Ticks / 10000L;
            int num6 = (int)(num5 % num2);
            int num7 = num6 / num4;
            Bitmap bitmap_9 = array[num7];
            bitmap_9 = main_0.method_108(creature_0, bitmap_9, main_0.bitmap_11[creature_0.PictureRef + num][num7]);
            float num8 = bitmap_9.Width;
            float num9 = bitmap_9.Height;
            Bitmap bitmap = null;
            bitmap = ((double_15 == 0.0) ? new Bitmap(bitmap_9) : method_218(bitmap_9, (float)double_15, GraphicsQuality.Medium));
            if (creature_0.Damage > 0.0)
            {
                method_21(bitmap_9);
            }
            float num10 = (float)int_13 / num8;
            float num11 = (float)bitmap.Width * num10;
            float num12 = (float)bitmap.Height * num10;
            float num13 = (num11 - num8) / 2f;
            float num14 = (num12 - num9) / 2f;
            num13 *= num10;
            num14 *= num10;
            RectangleF destRect = new RectangleF(int_11, int_12, num11, num12);
            RectangleF srcRect = new RectangleF(0f, 0f, bitmap.Width, bitmap.Height);
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
            graphics_0.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
        }

        private void method_113(Creature creature_0, SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, Texture2D[] texture2D_61, Texture2D[] texture2D_62, DateTime dateTime_5, int int_15, double double_15)
        {
            if (creature_0 != null && texture2D_61 != null)
            {
                Texture2D[] array = texture2D_61;
                if (creature_0.CurrentTarget != null && creature_0.DistanceToTarget <= 40.0 && texture2D_62 != null && texture2D_62.Length > 0)
                {
                    array = texture2D_62;
                }
                int num = (int)((double)array.Length / (double)int_15 * 1000.0);
                int num2 = Math.Max(1, array.Length - 1);
                int num3 = num / num2;
                long num4 = dateTime_5.Ticks / 10000L;
                int num5 = (int)(num4 % num);
                int num6 = num5 / num3;
                Texture2D texture = array[num6];
                System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, destination, (float)double_15);
            }
        }

        private void method_114(Creature creature_0, SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, Bitmap[] bitmap_7, Bitmap[] bitmap_8, DateTime dateTime_5, int int_15, double double_15)
        {
            if (creature_0 == null || bitmap_7 == null)
            {
                return;
            }
            Bitmap[] array = bitmap_7;
            int num = 0;
            if (creature_0.CurrentTarget != null && creature_0.DistanceToTarget <= 40.0 && bitmap_8 != null && bitmap_8.Length > 0)
            {
                array = bitmap_8;
                num = 5;
            }
            int num2 = (int)((double)array.Length / (double)int_15 * 1000.0);
            int num3 = Math.Max(1, array.Length - 1);
            int num4 = num2 / num3;
            long num5 = dateTime_5.Ticks / 10000L;
            int num6 = (int)(num5 % num2);
            int num7 = num6 / num4;
            Bitmap bitmap = array[num7];
            Texture2D texture2D = null;
            if (bitmap != null && bitmap.PixelFormat != 0)
            {
                bool flag = false;
                if (creature_0.Damage > 0.0)
                {
                    bitmap = main_0.method_108(creature_0, bitmap, main_0.bitmap_11[creature_0.PictureRef + num][num7]);
                    flag = true;
                }
                texture2D = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap);
                if (flag)
                {
                    method_21(bitmap);
                }
            }
            if (texture2D != null)
            {
                System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, destination, (float)double_15);
                method_22(texture2D);
            }
        }

        private void method_115(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, Bitmap[] bitmap_7)
        {
            method_116(graphics_0, int_11, int_12, int_13, int_14, bitmap_7, DateTime.Now, int_6, 0.0, 0.0, System.Drawing.Color.Empty);
        }

        private void method_116(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, Bitmap[] bitmap_7, DateTime dateTime_5, int int_15, double double_15, double double_16, System.Drawing.Color color_18)
        {
            int num = (int)((double)bitmap_7.Length / (double)int_15 * 1000.0);
            int num2 = Math.Max(1, bitmap_7.Length - 1);
            int num3 = num / num2;
            long num4 = dateTime_5.Ticks / 10000L;
            int num5 = (int)(num4 % num);
            int num6 = num5 / num3;
            Bitmap bitmap = bitmap_7[num6];
            ImageAttributes imageAttributes = null;
            if (color_18 != System.Drawing.Color.Empty)
            {
                imageAttributes = method_221(color_18);
            }
            float num7 = bitmap.Width;
            float num8 = bitmap.Height;
            Bitmap bitmap2 = null;
            if (double_15 != 0.0)
            {
                bitmap2 = method_217(bitmap, (float)double_15);
            }
            else if (double_16 != 0.0)
            {
                double num9 = double_16 / (1000.0 / (double)num3);
                double num10 = (double)num6 * num9;
                bitmap2 = method_217(bitmap, (float)num10);
            }
            else
            {
                bitmap2 = new Bitmap(bitmap);
            }
            float num11 = ((float)bitmap2.Width - num7) / 2f;
            float num12 = ((float)bitmap2.Height - num8) / 2f;
            RectangleF srcRect = new RectangleF(num11, num12, num7, num8);
            RectangleF destRect = new RectangleF(int_11, int_12, int_13, int_14);
            graphics_0.InterpolationMode = InterpolationMode.Low;
            graphics_0.SmoothingMode = SmoothingMode.None;
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            if (imageAttributes == null)
            {
                graphics_0.DrawImage(bitmap2, destRect, srcRect, GraphicsUnit.Pixel);
            }
            else
            {
                System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle((int)destRect.X, (int)destRect.Y, (int)destRect.Width, (int)destRect.Height);
                graphics_0.DrawImage(bitmap2, destRect2, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, imageAttributes);
                imageAttributes.Dispose();
            }
            if (bitmap2 != null)
            {
                method_21(bitmap2);
            }
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
        }

        private void method_117(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, Texture2D[] texture2D_61, DateTime dateTime_5, int int_15, double double_15, double double_16, System.Drawing.Color color_18)
        {
            int num = (int)((double)texture2D_61.Length / (double)int_15 * 1000.0);
            int num2 = Math.Max(1, texture2D_61.Length - 1);
            int num3 = num / num2;
            long num4 = dateTime_5.Ticks / 10000L;
            int num5 = (int)(num4 % num);
            int num6 = num5 / num3;
            Texture2D texture2D = texture2D_61[num6];
            _ = (float)int_13 / (float)texture2D.Width;
            float rotationAngle = (float)double_15;
            if (double_16 != 0.0)
            {
                double num7 = double_16 / (1000.0 / (double)num3);
                rotationAngle = (float)((double)num6 * num7);
            }
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
            if (color_18 == System.Drawing.Color.Empty)
            {
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, destination, rotationAngle);
            }
            else
            {
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, destination, rotationAngle, color_18);
            }
        }

        private void method_118(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, Texture2D texture2D_61, DateTime dateTime_5, double double_15, System.Drawing.Color color_18)
        {
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
            if (color_18 == System.Drawing.Color.Empty)
            {
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, destination, (float)double_15);
            }
            else
            {
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, destination, (float)double_15, color_18);
            }
        }

        private void method_119(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, Bitmap bitmap_7, DateTime dateTime_5, double double_15, System.Drawing.Color color_18)
        {
            ImageAttributes imageAttributes = null;
            if (color_18 != System.Drawing.Color.Empty)
            {
                imageAttributes = method_219(color_18);
            }
            float num = bitmap_7.Width;
            float num2 = bitmap_7.Height;
            Bitmap bitmap = null;
            bitmap = ((double_15 == 0.0) ? new Bitmap(bitmap_7) : method_217(bitmap_7, (float)double_15));
            float num3 = ((float)bitmap.Width - num) / 2f;
            float num4 = ((float)bitmap.Height - num2) / 2f;
            RectangleF srcRect = new RectangleF(num3, num4, num, num2);
            RectangleF destRect = new RectangleF(int_11, int_12, int_13, int_14);
            if (imageAttributes == null)
            {
                graphics_0.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
            }
            else
            {
                System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle((int)destRect.X, (int)destRect.Y, (int)destRect.Width, (int)destRect.Height);
                graphics_0.DrawImage(bitmap, destRect2, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, imageAttributes);
                imageAttributes.Dispose();
            }
            if (bitmap != null)
            {
                method_21(bitmap);
            }
        }

        public void PrepareGalaxyBackdrop()
        {
            if (main_0.bool_23)
            {
                return;
            }
            float float_ = 2f;
            RectangleF rectangleF_ = method_126(1);
            RectangleF rect = method_123(rectangleF_, 1.2f);
            float num = main_0.rectangleF_0.Width / 3f;
            if (main_0.rectangleF_0.Contains(rect) && !(rectangleF_.Width < num) && main_0.bitmap_179 != null && main_0.bitmap_179.PixelFormat != 0)
            {
                return;
            }
            lock (main_0.object_4)
            {
                RectangleF rectangleF_2 = method_123(rectangleF_, float_);
                if (!(rectangleF_2.Width > (float)main_0.bitmap_176.Width) && rectangleF_2.Width <= (float)base.Width)
                {
                    main_0.bool_12 = false;
                    RectangleF srcRect = method_124(rectangleF_2, new System.Drawing.Rectangle(0, 0, main_0.bitmap_176.Width, main_0.bitmap_176.Height));
                    main_0.rectangleF_0 = rectangleF_2;
                    float num2 = rectangleF_2.Width;
                    float num3 = rectangleF_2.Height;
                    _ = num2 / rectangleF_2.Width;
                    _ = num3 / rectangleF_2.Height;
                    float num4 = 0f;
                    float num5 = 0f;
                    float num6 = rectangleF_2.Width;
                    float num7 = rectangleF_2.Height;
                    if (rectangleF_2.X < 0f)
                    {
                        num4 = rectangleF_2.X * -1f;
                        num6 -= num4;
                    }
                    if (rectangleF_2.Y < 0f)
                    {
                        num5 = rectangleF_2.Y * -1f;
                        num7 -= num5;
                    }
                    RectangleF destRect = new RectangleF(num4, num5, num6, num7);
                    num2 = Math.Max(1f, num2);
                    num3 = Math.Max(1f, num3);
                    Bitmap bitmap = new Bitmap((int)num2, (int)num3, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        if (main_0.zEsBtZgRxo != ZoomStatus.Stabilizing && main_0.zEsBtZgRxo != 0)
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                            graphics.SmoothingMode = SmoothingMode.None;
                        }
                        else
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.None;
                        }
                        graphics.DrawImage(main_0.bitmap_178, destRect, srcRect, GraphicsUnit.Pixel);
                        if (main_0.zEsBtZgRxo == ZoomStatus.Stabilizing || main_0.zEsBtZgRxo == ZoomStatus.Stable || (bool_11 && !main_0.gameOptions_0.CleanGalaxyView))
                        {
                            using ImageAttributes imageAttr = method_236(0.25);
                            RectangleF rectangleF_3 = method_125();
                            RectangleF rectangleF = method_123(rectangleF_3, float_);
                            System.Drawing.Rectangle galaxySection = new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
                            double systemInfluenceSizeFactor = 1.0 + main_0.double_0 / 10000.0;
                            Bitmap bitmap2 = null;
                            if (main_0.gameOptions_0.MapOverlayEmpireTerritory)
                            {
                                bitmap2 = EmpireTerritory.CalculateEmpireTerritoryGrid(main_0._Game.Galaxy, galaxySection, (int)num2, (int)num3, galaxy_0.PlayerEmpire, main_0._Game.GodMode);
                                bitmap2 = GraphicsHelper.SmoothImage(bitmap2);
                            }
                            else
                            {
                                bitmap2 = EmpireTerritory.CalculateEmpireSystemTerritory(main_0._Game.Galaxy, galaxySection, (int)num2, (int)num3, galaxy_0.PlayerEmpire, main_0._Game.GodMode, bitmap_4, systemInfluenceSizeFactor);
                            }
                            System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle(0, 0, (int)num2, (int)num3);
                            graphics.DrawImage(bitmap2, destRect2, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttr);
                            method_21(bitmap2);
                        }
                        if (main_0.gameOptions_0.MapOverlayLongRangeScanners && !main_0.bool_23)
                        {
                            bool flag = false;
                            Bitmap image = new Bitmap((int)num2, (int)num3, PixelFormat.Format32bppPArgb);
                            using (Graphics graphics2 = Graphics.FromImage(image))
                            {
                                method_175(graphics2);
                                graphics2.Clear(System.Drawing.Color.Transparent);
                                RectangleF rectangleF2 = method_123(method_125(), float_);
                                float num8 = rectangleF2.Width / rectangleF_2.Width;
                                Empire playerEmpire = galaxy_0.PlayerEmpire;
                                for (int i = 0; i < playerEmpire.LongRangeScanners.Count; i++)
                                {
                                    BuiltObject builtObject = playerEmpire.LongRangeScanners[i];
                                    if (builtObject == null)
                                    {
                                        continue;
                                    }
                                    int sensorLongRange = builtObject.SensorLongRange;
                                    if (builtObject.Role != BuiltObjectRole.Base && builtObject.CurrentSpeed != 0f)
                                    {
                                        continue;
                                    }
                                    int num9 = (int)builtObject.Xpos - sensorLongRange;
                                    int num10 = (int)builtObject.Xpos + sensorLongRange;
                                    int num11 = (int)builtObject.Ypos - sensorLongRange;
                                    int num12 = (int)builtObject.Ypos + sensorLongRange;
                                    RectangleF rect2 = new RectangleF(num9, num11, sensorLongRange * 2, sensorLongRange * 2);
                                    if (!rectangleF2.IntersectsWith(rect2))
                                    {
                                        continue;
                                    }
                                    flag = true;
                                    float num13 = (float)(((double)num9 - (double)rectangleF2.Left) / (double)num8);
                                    float num14 = (float)(((double)num10 - (double)rectangleF2.Left) / (double)num8);
                                    float num15 = (float)(((double)num11 - (double)rectangleF2.Top) / (double)num8);
                                    float num16 = (float)(((double)num12 - (double)rectangleF2.Top) / (double)num8);
                                    RectangleF rectangleF3 = new RectangleF(num13, num15, num14 - num13, num16 - num15);
                                    rectangleF3.Inflate(rectangleF3.Width * 0.1f, rectangleF3.Width * 0.1f);
                                    System.Drawing.Color white = System.Drawing.Color.White;
                                    using ImageAttributes imageAttr2 = method_221(white);
                                    if (!main_0.bool_23)
                                    {
                                        graphics2.DrawImage(main_0.bitmap_188, new System.Drawing.Rectangle((int)rectangleF3.X, (int)rectangleF3.Y, (int)rectangleF3.Width, (int)rectangleF3.Height), 0, 0, main_0.bitmap_188.Width, main_0.bitmap_188.Height, GraphicsUnit.Pixel, imageAttr2);
                                    }
                                }
                            }
                            if (flag && !main_0.bool_23)
                            {
                                using ImageAttributes imageAttr3 = method_236(0.13);
                                method_175(graphics);
                                graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, (int)num2, (int)num3), 0, 0, (int)num2, (int)num3, GraphicsUnit.Pixel, imageAttr3);
                            }
                        }
                    }
                    Bitmap bitmap_ = main_0.bitmap_179;
                    main_0.bitmap_179 = bitmap;
                    method_21(bitmap_);
                    Texture2D texture2D_ = main_0.texture2D_2;
                    main_0.texture2D_2 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_179);
                    method_22(texture2D_);
                    FadeGalaxyBackground();
                }
                else
                {
                    main_0.bool_12 = true;
                }
            }
        }

        private System.Drawing.Color method_120(Bitmap bitmap_7)
        {
            int int_ = bitmap_7.Width / 2;
            int int_2 = bitmap_7.Height / 2;
            return method_121(bitmap_7, int_, int_2);
        }

        private System.Drawing.Color method_121(Bitmap bitmap_7, int int_11, int int_12)
        {
            return bitmap_7.GetPixel(int_11, int_12);
        }

        private System.Drawing.Color method_122(double double_15, double double_16)
        {
            System.Drawing.Color result = System.Drawing.Color.Empty;
            if (main_0.bitmap_182 != null && main_0.bitmap_182.Width > 0 && main_0.bitmap_182.Height > 0)
            {
                double num = (double)Galaxy.SizeX / (double)main_0.bitmap_182.Width;
                int val = (int)(double_15 / num);
                int val2 = (int)(double_16 / num);
                val = Math.Min(main_0.bitmap_182.Width - 1, Math.Max(val, 0));
                val2 = Math.Min(main_0.bitmap_182.Height - 1, Math.Max(val2, 0));
                System.Drawing.Color pixel = main_0.bitmap_182.GetPixel(val, val2);
                if (pixel.R != 0 && pixel.G != 0 && pixel.B != 0)
                {
                    result = pixel;
                }
                int red = Math.Min(255, result.R * 2);
                int green = Math.Min(255, result.G * 2);
                int blue = Math.Min(255, result.B * 2);
                result = System.Drawing.Color.FromArgb(red, green, blue);
            }
            return result;
        }

        public void FadeGalaxyNebulae()
        {
            if (main_0.double_0 < 210.0 && main_0.double_0 >= 70.0 && main_0.bitmap_183 != null)
            {
                float val = (float)((main_0.double_0 - 70.0) / 210.0);
                val = Math.Min(Math.Max(val, 0f), 1f);
                main_0.bitmap_184 = method_16(main_0.bitmap_183, val);
            }
            else
            {
                main_0.bitmap_184 = main_0.bitmap_183;
            }
        }

        public void FadeSectorBackground()
        {
            Bitmap bitmap = null;
            if (main_0.double_0 < 210.0 && main_0.double_0 >= 70.0 && main_0.bitmap_185 != null && main_0.bitmap_185.PixelFormat != 0)
            {
                float val = (float)((main_0.double_0 - 70.0) / 210.0);
                val = Math.Min(Math.Max(val, 0f), 1f);
                bitmap = method_16(main_0.bitmap_185, val);
            }
            else
            {
                bitmap = main_0.bitmap_185;
            }
            Bitmap bitmap_ = main_0.bitmap_186;
            main_0.bitmap_186 = bitmap;
            method_21(bitmap_);
            if (bool_11 && bool_12 && GraphicsDevice != null)
            {
                Texture2D texture2D = main_0.texture2D_4;
                main_0.texture2D_4 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_186);
                if (texture2D != null && !texture2D.IsDisposed)
                {
                    method_22(texture2D);
                }
            }
        }

        public void FadeGalaxyBackground()
        {
            if (!bool_11)
            {
                Bitmap bitmap = null;
                if (main_0.double_0 < 350.0 && main_0.double_0 >= 70.0 && main_0.bitmap_179 != null && main_0.bitmap_179.PixelFormat != 0)
                {
                    float val = (float)((main_0.double_0 - 70.0) / 210.0);
                    val = Math.Min(Math.Max(val, 0f), 1f);
                    bitmap = method_16(main_0.bitmap_179, val);
                }
                else
                {
                    bitmap = main_0.bitmap_179;
                }
                Bitmap fyKcynWgNv = main_0.FyKcynWgNv;
                main_0.FyKcynWgNv = bitmap;
                method_21(fyKcynWgNv);
            }
        }

        private RectangleF method_123(RectangleF rectangleF_1, float float_0)
        {
            float num = rectangleF_1.Width * float_0;
            float num2 = rectangleF_1.Height * float_0;
            float num3 = rectangleF_1.X - (num - rectangleF_1.Width) / 2f;
            float num4 = rectangleF_1.Y - (num2 - rectangleF_1.Height) / 2f;
            return new RectangleF(num3, num4, num, num2);
        }

        private RectangleF method_124(RectangleF rectangleF_1, RectangleF rectangleF_2)
        {
            if (rectangleF_1.X < rectangleF_2.X)
            {
                float num = rectangleF_2.X - rectangleF_1.X;
                rectangleF_1.Width -= num;
                rectangleF_1.X = rectangleF_2.X;
            }
            if (rectangleF_1.Y < rectangleF_2.Y)
            {
                float num2 = rectangleF_2.Y - rectangleF_1.Y;
                rectangleF_1.Height -= num2;
                rectangleF_1.Y = rectangleF_2.Y;
            }
            if (rectangleF_1.Width > rectangleF_2.Width)
            {
                _ = rectangleF_1.Width;
                _ = rectangleF_2.Width;
                rectangleF_1.Width = rectangleF_2.Width;
            }
            if (rectangleF_1.Height > rectangleF_2.Height)
            {
                _ = rectangleF_1.Height;
                _ = rectangleF_2.Height;
                rectangleF_1.Height = rectangleF_2.Height;
            }
            return rectangleF_1;
        }

        private RectangleF method_125()
        {
            float num = (float)base.Width * (float)main_0.double_0;
            float num2 = (float)base.Height * (float)main_0.double_0;
            float num3 = (float)main_0.int_13 - num / 2f;
            float num4 = (float)main_0.int_14 - num2 / 2f;
            return new RectangleF(num3, num4, num, num2);
        }

        private RectangleF method_126(int int_11)
        {
            return method_127(int_11, main_0.bitmap_176.Width);
        }

        private RectangleF method_127(int int_11, int int_12)
        {
            RectangleF rectangleF = method_125();
            float num = (float)int_12 / (float)int_11;
            float num2 = num / (float)Galaxy.SizeX;
            float num3 = rectangleF.Width * num2;
            float num4 = rectangleF.Height * num2;
            float num5 = rectangleF.X * num2;
            float num6 = rectangleF.Y * num2;
            return new RectangleF(num5, num6, num3, num4);
        }

        private System.Drawing.Rectangle method_128()
        {
            RectangleF rectangleF = method_125();
            float num = rectangleF.Left / (float)Galaxy.SizeX;
            float num2 = rectangleF.Width / (float)Galaxy.SizeX;
            float num3 = rectangleF.Top / (float)Galaxy.SizeY;
            float num4 = rectangleF.Height / (float)Galaxy.SizeY;
            RectangleF rectangleF2 = new RectangleF(num, num3, num2, num4);
            float num5 = (float)base.ClientRectangle.Width * (float)main_0.double_0;
            float num6 = (float)Galaxy.SizeX / num5 * (float)base.ClientRectangle.Width;
            int num7 = 0;
            int num8 = base.ClientRectangle.Right;
            int num9 = 0;
            int num10 = base.ClientRectangle.Bottom;
            if (rectangleF2.Left < 0f)
            {
                num7 = (int)(Math.Abs(rectangleF2.Left) * num6);
            }
            if (rectangleF2.Right > 1f)
            {
                num8 = (int)((float)base.ClientRectangle.Width - (rectangleF2.Right - 1f) * num6);
            }
            if (rectangleF2.Top < 0f)
            {
                num9 = (int)(Math.Abs(rectangleF2.Top) * num6);
            }
            if (rectangleF2.Bottom > 1f)
            {
                num10 = (int)((float)base.ClientRectangle.Height - (rectangleF2.Bottom - 1f) * num6);
            }
            int num11 = num8 - num7;
            int num12 = num10 - num9;
            return new System.Drawing.Rectangle(num7, num9, num11, num12);
        }

        private System.Drawing.Rectangle method_129(Texture2D texture2D_61)
        {
            RectangleF rectangleF = method_125();
            float num = rectangleF.Left / (float)Galaxy.SizeX;
            float num2 = rectangleF.Width / (float)Galaxy.SizeX;
            float num3 = rectangleF.Top / (float)Galaxy.SizeY;
            float num4 = rectangleF.Height / (float)Galaxy.SizeY;
            RectangleF rectangleF2 = new RectangleF(num, num3, num2, num4);
            float num5 = (float)base.ClientRectangle.Width * (float)main_0.double_0;
            _ = num5 / ((float)Galaxy.SizeX / (float)texture2D_61.Width);
            int num6 = 0;
            int num7 = texture2D_61.Width;
            int num8 = 0;
            int num9 = texture2D_61.Height;
            if (rectangleF2.Left > 0f)
            {
                num6 = (int)(rectangleF2.Left * (float)texture2D_61.Width);
            }
            if (rectangleF2.Right < 1f)
            {
                num7 = (int)(rectangleF2.Right * (float)texture2D_61.Width);
            }
            if (rectangleF2.Top > 0f)
            {
                num8 = (int)(rectangleF2.Top * (float)texture2D_61.Height);
            }
            if (rectangleF2.Bottom < 1f)
            {
                num9 = (int)(rectangleF2.Bottom * (float)texture2D_61.Height);
            }
            int num10 = num7 - num6;
            int num11 = num9 - num8;
            return new System.Drawing.Rectangle(num6, num8, num10, num11);
        }

        private RectangleF method_130(int int_11)
        {
            float num = (float)base.Width * (float)main_0.double_0;
            float num2 = (float)base.Height * (float)main_0.double_0;
            float num3 = (float)main_0.int_13 - num / 2f;
            float num4 = (float)main_0.int_14 - num2 / 2f;
            float float_ = main_0.float_1;
            float num5 = float_ / 2f;
            float num6 = (float)main_0.bitmap_176.Width / (float)int_11;
            num6 *= float_;
            float num7 = num6 / (float)Galaxy.SizeX;
            float num8 = (float)main_0.bitmap_176.Width / (float)int_11;
            num8 *= num5;
            float num9 = num8 / (float)Galaxy.SizeX;
            float num10 = num * num9;
            float num11 = num2 * num9;
            float num12 = num3 * num7;
            float num13 = num4 * num7;
            return new RectangleF(num12, num13, num10, num11);
        }

        private RectangleF method_131(int int_11)
        {
            float num = (float)base.Width * (float)main_0.double_0;
            float num2 = (float)base.Height * (float)main_0.double_0;
            float num3 = (float)main_0.int_13 - num / 2f;
            float num4 = (float)main_0.int_14 - num2 / 2f;
            float ugecxqhvjP = main_0.UgecxqhvjP;
            float num5 = ugecxqhvjP / 2f;
            float num6 = (float)main_0.bitmap_182.Width / (float)int_11;
            num6 *= ugecxqhvjP;
            float num7 = num6 / (float)Galaxy.SizeX;
            float num8 = (float)main_0.bitmap_182.Width / (float)int_11;
            num8 *= num5;
            float num9 = num8 / (float)Galaxy.SizeX;
            float num10 = num * num9;
            float num11 = num2 * num9;
            float num12 = num3 * num7;
            float num13 = num4 * num7;
            return new RectangleF(num12, num13, num10, num11);
        }

        private void method_132(Graphics graphics_0, SpriteBatch spriteBatch_2, Texture2D texture2D_61)
        {
            PrepareGalaxyBackdrop();
            RectangleF srcRect = method_126(1);
            if (main_0.bool_12)
            {
                srcRect = method_126(2);
                if (bool_11)
                {
                    if (bool_12 && spriteBatch_2 != null && texture2D_61 != null && !texture2D_61.IsDisposed)
                    {
                        method_249(srcRect);
                        System.Drawing.Rectangle source = method_129(texture2D_61);
                        System.Drawing.Rectangle destination = method_128();
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, source, destination);
                    }
                    return;
                }
                graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
                graphics_0.SmoothingMode = SmoothingMode.None;
                if (main_0.bitmap_177 != null && main_0.bitmap_177.PixelFormat != 0)
                {
                    graphics_0.DrawImage(main_0.bitmap_177, base.ClientRectangle, srcRect, GraphicsUnit.Pixel);
                }
                method_177(graphics_0);
                return;
            }
            if (main_0.double_0 <= 2500.0)
            {
                if (!bool_11)
                {
                    method_147();
                    srcRect.Offset(main_0.rectangleF_2.X * -1f, main_0.rectangleF_2.Y * -1f);
                }
                if (bool_11)
                {
                    if (bool_12 && spriteBatch_2 != null && texture2D_61 != null && !texture2D_61.IsDisposed)
                    {
                        System.Drawing.Rectangle source2 = method_129(texture2D_61);
                        System.Drawing.Rectangle destination2 = method_128();
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, source2, destination2);
                    }
                    return;
                }
                if (main_0.zEsBtZgRxo != ZoomStatus.Stabilizing && main_0.zEsBtZgRxo != 0)
                {
                    method_175(graphics_0);
                }
                else
                {
                    graphics_0.InterpolationMode = InterpolationMode.Bilinear;
                    graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics_0.SmoothingMode = SmoothingMode.None;
                }
                graphics_0.DrawImage(main_0.bitmap_186, base.ClientRectangle, srcRect, GraphicsUnit.Pixel);
                method_177(graphics_0);
                return;
            }
            srcRect.Offset(main_0.rectangleF_0.X * -1f, main_0.rectangleF_0.Y * -1f);
            if (bool_11)
            {
                if (bool_12 && spriteBatch_2 != null && texture2D_61 != null && !texture2D_61.IsDisposed)
                {
                    System.Drawing.Rectangle source3 = method_129(texture2D_61);
                    System.Drawing.Rectangle destination3 = method_128();
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, source3, destination3);
                }
            }
            else
            {
                graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
                graphics_0.SmoothingMode = SmoothingMode.None;
                graphics_0.DrawImage(main_0.bitmap_179, base.ClientRectangle, srcRect, GraphicsUnit.Pixel);
                method_177(graphics_0);
            }
        }

        private void method_133()
        {
            RectangleF rectangleF_ = method_174(main_0.bitmap_176, 1.0);
            double double_ = Galaxy.SizeX / 2;
            double double_2 = Galaxy.SizeY / 2;
            RectangleF rectangleF_2 = method_139(Galaxy.SizeX, double_, double_2, rectangleF_, 1.0);
            RectangleF rectangleF_3 = method_174(main_0.bitmap_176, main_0.float_1);
            RectangleF rectangleF_4 = method_139(Galaxy.SizeX, double_, double_2, rectangleF_3, 1.0);
            RectangleF rect = method_123(rectangleF_4, 1.2f);
            if (!main_0.rectangleF_2.Contains(rect) || main_0.bitmap_179 == null || main_0.bool_15)
            {
                float val = ((float)main_0.double_0 - 1200f) / 800f;
                val = Math.Min(1f, Math.Max(0f, val));
                double double_3 = 1.0;
                RectangleF rectangleF_5 = default(RectangleF);
                main_0.bitmap_185 = method_146(1, main_0.bitmap_176, rectangleF_2, Galaxy.SizeX, 500, 200f, 20f, val, main_0.bitmap_176, out double_3, out rectangleF_5, bool_13: false);
                main_0.float_1 = (float)double_3;
                method_174(main_0.bitmap_176, main_0.float_1);
                float num = 0f;
                if (rectangleF_2.X > 0f)
                {
                    num = rectangleF_2.X * main_0.float_1;
                }
                float num2 = 0f;
                if (rectangleF_2.Y > 0f)
                {
                    num2 = rectangleF_2.Y * main_0.float_1;
                }
                float num3 = rectangleF_2.Width * main_0.float_1;
                float num4 = rectangleF_2.Height * main_0.float_1;
                RectangleF rectangleF_6 = new RectangleF(num, num2, num3, num4);
                main_0.rectangleF_2 = rectangleF_6;
                main_0.float_1 = (float)double_3;
                main_0.bool_15 = false;
                FadeSectorBackground();
            }
        }

        private void method_134()
        {
            RectangleF rectangleF_ = method_174(main_0.bitmap_182, 1.0);
            double double_ = Galaxy.SizeX / 2;
            double double_2 = Galaxy.SizeY / 2;
            RectangleF rectangleF_2 = method_139(Galaxy.SizeX, double_, double_2, rectangleF_, 1.0);
            RectangleF rectangleF_3 = method_174(main_0.bitmap_182, main_0.UgecxqhvjP);
            RectangleF rectangleF_4 = method_139(Galaxy.SizeX, double_, double_2, rectangleF_3, 1.0);
            RectangleF rect = method_123(rectangleF_4, 1.2f);
            if (!main_0.rectangleF_1.Contains(rect) || main_0.bitmap_183 == null || main_0.bool_13)
            {
                float val = ((float)main_0.double_0 - 1200f) / 800f;
                val = Math.Min(1f, Math.Max(0f, val));
                double double_3 = 1.0;
                RectangleF rectangleF_5 = default(RectangleF);
                Bitmap bitmap_ = main_0.bitmap_183;
                main_0.bitmap_183 = method_146(1, main_0.bitmap_182, rectangleF_2, Galaxy.SizeX, 500, 200f, 20f, val, main_0.bitmap_182, out double_3, out rectangleF_5, bool_13: true);
                method_21(bitmap_);
                main_0.UgecxqhvjP = (float)double_3;
                method_174(main_0.bitmap_182, main_0.UgecxqhvjP);
                float num = 0f;
                if (rectangleF_2.X > 0f)
                {
                    num = rectangleF_2.X * main_0.UgecxqhvjP;
                }
                float num2 = 0f;
                if (rectangleF_2.Y > 0f)
                {
                    num2 = rectangleF_2.Y * main_0.UgecxqhvjP;
                }
                float num3 = rectangleF_2.Width * main_0.UgecxqhvjP;
                float num4 = rectangleF_2.Height * main_0.UgecxqhvjP;
                RectangleF rectangleF_6 = new RectangleF(num, num2, num3, num4);
                main_0.rectangleF_1 = rectangleF_6;
                main_0.UgecxqhvjP = (float)double_3;
                main_0.bool_13 = false;
                FadeGalaxyNebulae();
            }
        }

        private Bitmap method_135(Habitat habitat_1)
        {
            int num = -1;
            switch (habitat_1.Type)
            {
                case HabitatType.Hydrogen:
                    num = 6;
                    break;
                case HabitatType.Helium:
                    num = 0;
                    break;
                case HabitatType.Argon:
                    num = 2;
                    break;
                case HabitatType.Ammonia:
                    num = 4;
                    break;
                case HabitatType.CarbonDioxide:
                    num = 7;
                    break;
                case HabitatType.Oxygen:
                    num = 1;
                    break;
                case HabitatType.NitrogenOxygen:
                    num = 3;
                    break;
                case HabitatType.Chlorine:
                    num = 5;
                    break;
            }
            int num2 = Math.Min(100, (int)habitat_1.Diameter);
            nebulaCloudGenerator_1.GenerateNebulaBackdrop(habitat_1.HabitatIndex, out var cloudImages, out var _, 255, num + 20, num2, num2, transparentBackground: true, isGasCloud: true, useLowQuality: false);
            sectorCloudGenerator_0 = new SectorCloudGenerator(habitat_1.HabitatIndex, 4);
            return cloudImages[0];
        }

        private void method_136(Habitat habitat_1)
        {
            if (bitmap_2 == null)
            {
                bitmap_2 = method_135(habitat_1);
                texture2D_56 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_2);
            }
            RectangleF rectangleF_ = method_174(bitmap_2, 1.0);
            RectangleF rectangleF = method_137(habitat_1, rectangleF_);
            RectangleF rectangleF_2 = method_174(bitmap_2, double_3);
            RectangleF rectangleF_3 = method_137(habitat_1, rectangleF_2);
            RectangleF rectangleF_4 = method_123(rectangleF_3, 1.2f);
            rectangleF_4 = method_143(rectangleF_4, bitmap_2, double_3);
            if (rectangleF == System.Drawing.Rectangle.Empty)
            {
                if (bitmap_3 != null && bitmap_3.PixelFormat != 0)
                {
                    method_21(bitmap_3);
                }
                bitmap_3 = null;
                if (texture2D_57 != null && !texture2D_57.IsDisposed)
                {
                    method_22(texture2D_57);
                }
                texture2D_57 = null;
                rectangleF_0 = System.Drawing.Rectangle.Empty;
                return;
            }
            rectangleF_4 = method_123(rectangleF_4, 0.99f);
            if (bool_6 || !rectangleF_0.Contains(rectangleF_4) || bitmap_3 == null)
            {
                rectangleF = method_123(rectangleF, 2f);
                rectangleF = method_142(rectangleF, bitmap_2);
                RectangleF rectangleF_5 = default(RectangleF);
                bitmap_3 = method_145(habitat_1.HabitatIndex, bitmap_2, rectangleF, habitat_1.Diameter, out double_3, out rectangleF_5, bool_13: true);
                texture2D_57 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_3);
                rectangleF_ = method_174(bitmap_2, double_3);
                float num = 0f;
                if (rectangleF.X > 0f)
                {
                    num = rectangleF.X * (float)double_3;
                }
                float num2 = 0f;
                if (rectangleF.Y > 0f)
                {
                    num2 = rectangleF.Y * (float)double_3;
                }
                float num3 = rectangleF.Width * (float)double_3;
                float num4 = rectangleF.Height * (float)double_3;
                RectangleF rectangleF2 = (rectangleF_0 = new RectangleF(num, num2, num3, num4));
                bool_6 = false;
            }
        }

        private RectangleF method_137(Habitat habitat_1, RectangleF rectangleF_1)
        {
            return method_138(habitat_1, rectangleF_1, 1.0);
        }

        private RectangleF method_138(Habitat habitat_1, RectangleF rectangleF_1, double double_15)
        {
            return method_139(habitat_1.Diameter, habitat_1.Xpos, habitat_1.Ypos, rectangleF_1, double_15);
        }

        private RectangleF method_139(int int_11, double double_15, double double_16, RectangleF rectangleF_1, double double_17)
        {
            float num = rectangleF_1.Height / rectangleF_1.Width;
            float num2 = int_11;
            float num3 = num2 * num;
            System.Drawing.Rectangle rectangle = method_34((int)double_15, (int)double_16, (int)num2, (int)num3, main_0.double_0);
            float num4 = rectangle.X;
            float num5 = rectangle.Y;
            float num6 = rectangle.Width;
            float num7 = rectangle.Height;
            float num8 = 0f;
            float num9 = 0f;
            float num10 = num6;
            float num11 = num7;
            if (num4 + num6 > (float)base.Width)
            {
                num10 -= num4 + num6 - (float)base.Width;
            }
            if (num5 + num7 > (float)base.Height)
            {
                num11 -= num5 + num7 - (float)base.Height;
            }
            if (num4 < 0f)
            {
                num6 += num4;
                num10 += num4;
                num4 = 0f - num4;
                num8 = num4;
            }
            if (num5 < 0f)
            {
                num7 += num5;
                num11 += num5;
                num5 = 0f - num5;
                num9 = num5;
            }
            float num12 = (float)int_11 / (float)main_0.double_0 / (rectangleF_1.Width * (float)double_17);
            num8 /= num12;
            num9 /= num12;
            num10 /= num12;
            num11 /= num12;
            RectangleF result = new RectangleF(num8, num9, num10, num11);
            if (num10 <= 0f || num11 <= 0f)
            {
                return System.Drawing.Rectangle.Empty;
            }
            return result;
        }

        private RectangleF method_140(Habitat habitat_1, Bitmap bitmap_7)
        {
            return method_141(habitat_1.Diameter, habitat_1.Xpos, habitat_1.Ypos, bitmap_7);
        }

        private RectangleF method_141(int int_11, double double_15, double double_16, Bitmap bitmap_7)
        {
            double num = (double)bitmap_7.Height / (double)bitmap_7.Width;
            int int_12 = (int)((double)int_11 * num);
            System.Drawing.Rectangle rectangle = method_34((int)double_15, (int)double_16, int_11, int_12, main_0.double_0);
            int num2 = rectangle.X;
            int num3 = rectangle.Y;
            int num4 = rectangle.Width;
            int num5 = rectangle.Height;
            if (num2 + num4 > base.Width)
            {
                num4 -= num2 + num4 - base.Width;
            }
            if (num3 + num5 > base.Height)
            {
                num5 -= num3 + num5 - base.Height;
            }
            if (num2 < 0)
            {
                num4 += num2;
                num2 = 0;
            }
            if (num3 < 0)
            {
                num5 += num3;
                num3 = 0;
            }
            RectangleF result = new RectangleF(num2, num3, num4, num5);
            if (num4 <= 0 || num5 <= 0)
            {
                return System.Drawing.Rectangle.Empty;
            }
            return result;
        }

        private RectangleF method_142(RectangleF rectangleF_1, Bitmap bitmap_7)
        {
            return method_143(rectangleF_1, bitmap_7, 1.0);
        }

        private RectangleF method_143(RectangleF rectangleF_1, Bitmap bitmap_7, double double_15)
        {
            int num = (int)((double)bitmap_7.Width * double_15);
            int num2 = (int)((double)bitmap_7.Height * double_15);
            if (rectangleF_1.Right >= (float)num)
            {
                rectangleF_1.Width -= rectangleF_1.Right - (float)num;
            }
            if (rectangleF_1.Bottom >= (float)num2)
            {
                rectangleF_1.Height -= rectangleF_1.Bottom - (float)num2;
            }
            if (rectangleF_1.X < 0f)
            {
                rectangleF_1.Width += rectangleF_1.X;
                rectangleF_1.X = 0f;
            }
            if (rectangleF_1.Y < 0f)
            {
                rectangleF_1.Height += rectangleF_1.Y;
                rectangleF_1.Y = 0f;
            }
            return rectangleF_1;
        }

        private Bitmap method_144(int int_11, Bitmap bitmap_7, RectangleF rectangleF_1, int int_12, out double double_15, out RectangleF rectangleF_2)
        {
            return method_146(int_11, bitmap_7, rectangleF_1, int_12, 400, 0.1f, 10f, 0f, null, out double_15, out rectangleF_2, bool_13: false);
        }

        private Bitmap method_145(int int_11, Bitmap bitmap_7, RectangleF rectangleF_1, int int_12, out double double_15, out RectangleF rectangleF_2, bool bool_13)
        {
            return method_146(int_11, bitmap_7, rectangleF_1, int_12, 400, 0.1f, 10f, 0f, null, out double_15, out rectangleF_2, bool_13);
        }

        private Bitmap method_146(int int_11, Bitmap bitmap_7, RectangleF rectangleF_1, int int_12, int int_13, float float_0, float float_1, float float_2, Bitmap bitmap_8, out double double_15, out RectangleF rectangleF_2, bool bool_13)
        {
            _ = rectangleF_1.Height / rectangleF_1.Width;
            double_15 = 1.0;
            rectangleF_2 = new RectangleF(0f, 0f, rectangleF_1.Width, rectangleF_1.Height);
            double val = (double)int_13 / (double)rectangleF_1.Width;
            val = Math.Min(10.0, val);
            if (val > 1.0)
            {
                float num = (float)val * rectangleF_2.Width;
                float num2 = (float)val * rectangleF_2.Height;
                rectangleF_2 = new RectangleF(0f, 0f, num, num2);
            }
            else
            {
                val = 1.0;
            }
            double_15 = val;
            float num3 = (float)int_12 / float_0;
            float num4 = num3 / (float)bitmap_7.Width;
            float num5 = num3 / (float)((double)bitmap_7.Width * double_15);
            float num6 = 1f;
            float num7 = num5 * num6;
            float num8 = rectangleF_1.X * num4;
            float num9 = rectangleF_1.Y * num4;
            int num10 = Math.Min(2000, Math.Max(1, (int)rectangleF_2.Height));
            int num11 = Math.Min(2000, Math.Max(1, (int)rectangleF_2.Width));
            Bitmap bitmap = new Bitmap(num11, num10, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                method_177(graphics);
                if (bool_13)
                {
                    graphics.Clear(System.Drawing.Color.Transparent);
                }
                else
                {
                    graphics.Clear(System.Drawing.Color.Black);
                }
                graphics.DrawImage(bitmap_7, rectangleF_2, rectangleF_1, GraphicsUnit.Pixel);
            }
            System.Drawing.Rectangle imageRectangle = new System.Drawing.Rectangle(0, 0, (int)rectangleF_2.Width, (int)rectangleF_2.Height);
            num8 = num8 / num3 * float_1;
            num9 = num9 / num3 * float_1;
            num7 = num7 / num3 * float_1;
            if (sectorCloudGenerator_0 == null)
            {
                sectorCloudGenerator_0 = new SectorCloudGenerator(int_11, 16);
            }
            if (bool_13)
            {
                sectorCloudGenerator_0.FbmImageTransparent(bitmap, imageRectangle, num7, num8, num9);
            }
            else
            {
                sectorCloudGenerator_0.FbmImage(bitmap, imageRectangle, num7, num8, num9);
            }
            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics2 = Graphics.FromImage(bitmap2);
            method_176(graphics2);
            graphics2.Clear(System.Drawing.Color.Transparent);
            graphics2.DrawImageUnscaled(bitmap, new System.Drawing.Point(0, 0));
            method_21(bitmap);
            if (float_2 == 1f)
            {
                graphics2.DrawImage(bitmap_8, rectangleF_2, rectangleF_1, GraphicsUnit.Pixel);
                return bitmap2;
            }
            if (float_2 > 0f)
            {
                Bitmap bitmap3 = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
                using (Graphics graphics3 = Graphics.FromImage(bitmap3))
                {
                    method_176(graphics3);
                    graphics3.Clear(System.Drawing.Color.Transparent);
                    graphics3.DrawImage(bitmap_8, rectangleF_2, rectangleF_1, GraphicsUnit.Pixel);
                }
                bitmap3 = method_16(bitmap3, float_2);
                graphics2.DrawImageUnscaled(bitmap3, new System.Drawing.Point(0, 0));
                method_21(bitmap3);
                return bitmap2;
            }
            return bitmap2;
        }

        private void method_147()
        {
            RectangleF rectangleF_ = method_126(1);
            RectangleF rect = method_123(rectangleF_, 1.2f);
            if (rect.X < 0f)
            {
                rect.Width += rect.X + 1f;
                rect.X = 0f;
            }
            if (rect.Y < 0f)
            {
                rect.Height += rect.Y + 1f;
                rect.Y = 0f;
            }
            if (rect.Right >= (float)main_0.bitmap_176.Width)
            {
                rect.Width -= rect.Right - (float)main_0.bitmap_176.Width + 1f;
            }
            if (rect.Bottom >= (float)main_0.bitmap_176.Height)
            {
                rect.Height -= rect.Bottom - (float)main_0.bitmap_176.Height + 1f;
            }
            if (main_0.rectangleF_2.Contains(rect) && main_0.bitmap_185 != null && main_0.bitmap_185.PixelFormat != 0 && !main_0.bool_15)
            {
                return;
            }
            rectangleF_ = method_123(rectangleF_, 2f);
            float val = ((float)main_0.double_0 - 1600f) / 1000f;
            val = Math.Min(1f, Math.Max(0f, val));
            int num = 500;
            _ = rectangleF_.Height / rectangleF_.Width;
            float num2 = (float)Galaxy.SizeX / 200f;
            float num3 = num2 / (float)main_0.bitmap_176.Width;
            if (rectangleF_.Right >= (float)main_0.bitmap_176.Width)
            {
                rectangleF_.Width -= rectangleF_.Right - (float)main_0.bitmap_176.Width;
            }
            if (rectangleF_.Bottom >= (float)main_0.bitmap_176.Height)
            {
                rectangleF_.Height -= rectangleF_.Bottom - (float)main_0.bitmap_176.Height;
            }
            if (rectangleF_.X < 0f)
            {
                rectangleF_.Width += rectangleF_.X;
                rectangleF_.X = 0f;
            }
            if (rectangleF_.Y < 0f)
            {
                rectangleF_.Height += rectangleF_.Y;
                rectangleF_.Y = 0f;
            }
            if (rectangleF_.Width < 1f || rectangleF_.Height < 1f)
            {
                rectangleF_ = new RectangleF(rectangleF_.X, rectangleF_.Y, Math.Max(1f, rectangleF_.Width), Math.Max(1f, rectangleF_.Height));
            }
            RectangleF rectangleF = new RectangleF(0f, 0f, rectangleF_.Width, rectangleF_.Height);
            double num4 = (double)num / (double)rectangleF_.Width;
            num4 = 1.0;
            float num5 = 1f;
            float num6 = num3 * num5 * 1f;
            float num7 = rectangleF_.X * num3;
            float num8 = rectangleF_.Y * num3;
            int num9 = Math.Max(1, (int)rectangleF.Width);
            int num10 = Math.Max(1, (int)rectangleF.Height);
            Bitmap bitmap = new Bitmap(num9, num10, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (main_0.zEsBtZgRxo != ZoomStatus.Stabilizing && main_0.zEsBtZgRxo != 0)
                {
                    method_175(graphics);
                }
                else
                {
                    method_176(graphics);
                }
                graphics.Clear(System.Drawing.Color.Black);
                new System.Drawing.Rectangle(0, 0, (int)rectangleF.Width, (int)rectangleF.Height);
                graphics.DrawImage(main_0.bitmap_176, rectangleF, rectangleF_, GraphicsUnit.Pixel);
                method_176(graphics);
            }
            if (val < 1f)
            {
                if (sectorCloudGenerator_1 == null)
                {
                    sectorCloudGenerator_1 = new SectorCloudGenerator(1, 32);
                }
                float num11 = 20f;
                num7 = num7 / num2 * num11;
                num8 = num8 / num2 * num11;
                float num12 = num2 * (float)num4;
                num6 = num6 / num12 * num11;
            }
            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics2 = Graphics.FromImage(bitmap2))
            {
                method_175(graphics2);
                graphics2.Clear(System.Drawing.Color.Black);
                graphics2.DrawImageUnscaled(bitmap, new System.Drawing.Point(0, 0));
                method_21(bitmap);
                if (val == 1f)
                {
                    graphics2.DrawImage(main_0.bitmap_176, rectangleF, rectangleF_, GraphicsUnit.Pixel);
                }
                else if (val > 0f)
                {
                    Bitmap bitmap3 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics3 = Graphics.FromImage(bitmap3))
                    {
                        if (main_0.zEsBtZgRxo != ZoomStatus.Stabilizing && main_0.zEsBtZgRxo != 0)
                        {
                            method_175(graphics3);
                        }
                        else
                        {
                            method_176(graphics3);
                        }
                        graphics3.Clear(System.Drawing.Color.Transparent);
                        graphics3.DrawImage(main_0.bitmap_176, rectangleF, rectangleF_, GraphicsUnit.Pixel);
                    }
                    bitmap3 = method_16(bitmap3, val);
                    graphics2.DrawImageUnscaled(bitmap3, new System.Drawing.Point(0, 0));
                    method_21(bitmap3);
                }
                if (main_0.zEsBtZgRxo != ZoomStatus.Stabilizing && main_0.zEsBtZgRxo != 0)
                {
                    method_175(graphics2);
                }
                else
                {
                    method_176(graphics2);
                }
                Bitmap bitmap4 = method_150(rectangleF, rectangleF_, (float)num4, num2);
                graphics2.DrawImage(bitmap4, 0, 0);
                method_21(bitmap4);
                if (main_0.zEsBtZgRxo == ZoomStatus.Stabilizing || main_0.zEsBtZgRxo == ZoomStatus.Stable || (bool_11 && !main_0.gameOptions_0.CleanGalaxyView))
                {
                    using ImageAttributes imageAttr = method_236(0.25);
                    RectangleF rectangleF2 = method_123(method_125(), 2f);
                    if (rectangleF2.Right >= (float)Galaxy.SizeX)
                    {
                        rectangleF2.Width -= rectangleF2.Right - (float)Galaxy.SizeX;
                    }
                    if (rectangleF2.Bottom >= (float)Galaxy.SizeY)
                    {
                        rectangleF2.Height -= rectangleF2.Bottom - (float)Galaxy.SizeY;
                    }
                    if (rectangleF2.X < 0f)
                    {
                        rectangleF2.Width += rectangleF2.X;
                        rectangleF2.X = 0f;
                    }
                    if (rectangleF2.Y < 0f)
                    {
                        rectangleF2.Height += rectangleF2.Y;
                        rectangleF2.Y = 0f;
                    }
                    System.Drawing.Rectangle galaxySection = new System.Drawing.Rectangle((int)rectangleF2.X, (int)rectangleF2.Y, (int)rectangleF2.Width, (int)rectangleF2.Height);
                    double systemInfluenceSizeFactor = 1.0 + main_0.double_0 / 10000.0;
                    Bitmap bitmap5 = null;
                    if (main_0.gameOptions_0.MapOverlayEmpireTerritory)
                    {
                        bitmap5 = EmpireTerritory.CalculateEmpireTerritoryGrid(main_0._Game.Galaxy, galaxySection, bitmap.Width, bitmap.Height, galaxy_0.PlayerEmpire, main_0._Game.GodMode);
                        bitmap5 = GraphicsHelper.SmoothImage(bitmap5);
                    }
                    else
                    {
                        bitmap5 = EmpireTerritory.CalculateEmpireSystemTerritory(main_0._Game.Galaxy, galaxySection, bitmap.Width, bitmap.Height, galaxy_0.PlayerEmpire, main_0._Game.GodMode, bitmap_4, systemInfluenceSizeFactor);
                    }
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    graphics2.DrawImage(bitmap5, destRect, 0, 0, bitmap5.Width, bitmap5.Height, GraphicsUnit.Pixel, imageAttr);
                    method_21(bitmap5);
                }
                if (main_0.gameOptions_0.MapOverlayLongRangeScanners)
                {
                    bool flag = false;
                    Bitmap bitmap6 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics4 = Graphics.FromImage(bitmap6))
                    {
                        method_175(graphics4);
                        graphics4.Clear(System.Drawing.Color.Transparent);
                        RectangleF rectangleF3 = method_123(method_125(), 2f);
                        float num13 = rectangleF3.Width / rectangleF_.Width;
                        Empire playerEmpire = galaxy_0.PlayerEmpire;
                        for (int i = 0; i < playerEmpire.LongRangeScanners.Count; i++)
                        {
                            BuiltObject builtObject = playerEmpire.LongRangeScanners[i];
                            if (builtObject == null)
                            {
                                continue;
                            }
                            int sensorLongRange = builtObject.SensorLongRange;
                            if (builtObject.Role != BuiltObjectRole.Base && builtObject.CurrentSpeed != 0f)
                            {
                                continue;
                            }
                            int num14 = (int)builtObject.Xpos - sensorLongRange;
                            int num15 = (int)builtObject.Xpos + sensorLongRange;
                            int num16 = (int)builtObject.Ypos - sensorLongRange;
                            int num17 = (int)builtObject.Ypos + sensorLongRange;
                            RectangleF rect2 = new RectangleF(num14, num16, sensorLongRange * 2, sensorLongRange * 2);
                            if (rectangleF3.IntersectsWith(rect2))
                            {
                                flag = true;
                                num13 = (float)((double)Galaxy.SizeX / 2000.0);
                                float num18 = ((float)num14 - Math.Max(0f, rectangleF3.Left)) / num13;
                                float num19 = ((float)num15 - Math.Max(0f, rectangleF3.Left)) / num13;
                                float num20 = ((float)num16 - Math.Max(0f, rectangleF3.Top)) / num13;
                                float num21 = ((float)num17 - Math.Max(0f, rectangleF3.Top)) / num13;
                                RectangleF rectangleF4 = new RectangleF(num18, num20, num19 - num18, num21 - num20);
                                rectangleF4.Inflate(rectangleF4.Width * 0.1f, rectangleF4.Width * 0.1f);
                                System.Drawing.Color white = System.Drawing.Color.White;
                                using ImageAttributes imageAttr2 = method_221(white);
                                graphics4.DrawImage(main_0.bitmap_188, new System.Drawing.Rectangle((int)rectangleF4.X, (int)rectangleF4.Y, (int)rectangleF4.Width, (int)rectangleF4.Height), 0, 0, main_0.bitmap_188.Width, main_0.bitmap_188.Height, GraphicsUnit.Pixel, imageAttr2);
                            }
                        }
                    }
                    if (flag)
                    {
                        using (ImageAttributes imageAttr3 = method_236(0.13))
                        {
                            method_175(graphics2);
                            graphics2.DrawImage(bitmap6, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr3);
                        }
                        method_21(bitmap6);
                    }
                }
            }
            Bitmap bitmap_ = main_0.bitmap_185;
            main_0.bitmap_185 = bitmap2;
            method_21(bitmap_);
            RectangleF rectangleF_2 = new RectangleF(rectangleF_.X, rectangleF_.Y, rectangleF.Width, rectangleF.Height);
            main_0.rectangleF_2 = rectangleF_2;
            main_0.float_1 = (float)num4;
            main_0.bool_15 = false;
            FadeSectorBackground();
        }

        internal void method_148(ref Bitmap bitmap_7, ref Bitmap bitmap_8, Galaxy galaxy_1, double double_15)
        {
            if (galaxy_1 == null)
            {
                return;
            }
            if (bitmap_8 == null || bitmap_8.PixelFormat == PixelFormat.Undefined)
            {
                bitmap_8 = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
            }
            if (main_0.gameOptions_0.MapOverlayEmpireTerritory)
            {
                EmpireTerritory.CalculateEmpireTerritoryGrid(ref bitmap_8, galaxy_1, new System.Drawing.Rectangle(0, 0, Galaxy.SizeX, Galaxy.SizeY), bitmap_7.Width, bitmap_7.Height, galaxy_1.PlayerEmpire, main_0._Game.GodMode);
                bitmap_8 = GraphicsHelper.SmoothImage(bitmap_8);
            }
            else
            {
                EmpireTerritory.CalculateEmpireSystemTerritory(ref bitmap_8, galaxy_1, new System.Drawing.Rectangle(0, 0, Galaxy.SizeX, Galaxy.SizeY), bitmap_7.Width, bitmap_7.Height, galaxy_1.PlayerEmpire, main_0._Game.GodMode, bitmap_4, double_15);
            }
            using Graphics graphics = Graphics.FromImage(bitmap_7);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.None;
            using ImageAttributes imageAttr = method_236(0.25);
            graphics.DrawImage(destRect: new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height), image: bitmap_8, srcX: 0, srcY: 0, srcWidth: bitmap_8.Width, srcHeight: bitmap_8.Height, srcUnit: GraphicsUnit.Pixel, imageAttr: imageAttr);
        }

        internal void method_149(Bitmap bitmap_7, ref Bitmap bitmap_8, Bitmap bitmap_9)
        {
            using Graphics graphics = Graphics.FromImage(bitmap_7);
            method_175(graphics);
            bool flag = false;
            if (bitmap_8 != null && bitmap_8.PixelFormat != 0)
            {
                if (bitmap_8.Width != bitmap_7.Width || bitmap_8.Height != bitmap_7.Height)
                {
                    method_21(bitmap_8);
                    bitmap_8 = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
                }
            }
            else
            {
                bitmap_8 = new Bitmap(bitmap_7.Width, bitmap_7.Height, PixelFormat.Format32bppPArgb);
            }
            using (Graphics graphics2 = Graphics.FromImage(bitmap_8))
            {
                method_175(graphics2);
                graphics2.Clear(System.Drawing.Color.Transparent);
                RectangleF rectangleF = new RectangleF(0f, 0f, Galaxy.SizeX, Galaxy.SizeY);
                float num = rectangleF.Width / (float)bitmap_7.Width;
                Empire playerEmpire = galaxy_0.PlayerEmpire;
                if (playerEmpire.LongRangeScanners.Count > 0)
                {
                    System.Drawing.Color white = System.Drawing.Color.White;
                    using ImageAttributes imageAttr = method_221(white);
                    for (int i = 0; i < playerEmpire.LongRangeScanners.Count; i++)
                    {
                        BuiltObject builtObject = playerEmpire.LongRangeScanners[i];
                        int sensorLongRange = builtObject.SensorLongRange;
                        if (builtObject.Role == BuiltObjectRole.Base || builtObject.CurrentSpeed == 0f)
                        {
                            int num2 = (int)builtObject.Xpos - sensorLongRange;
                            int num3 = (int)builtObject.Xpos + sensorLongRange;
                            int num4 = (int)builtObject.Ypos - sensorLongRange;
                            int num5 = (int)builtObject.Ypos + sensorLongRange;
                            RectangleF rect = new RectangleF(num2, num4, sensorLongRange * 2, sensorLongRange * 2);
                            if (rectangleF.IntersectsWith(rect))
                            {
                                flag = true;
                                float num6 = (float)(((double)num2 - (double)rectangleF.Left) / (double)num);
                                float num7 = (float)(((double)num3 - (double)rectangleF.Left) / (double)num);
                                float num8 = (float)(((double)num4 - (double)rectangleF.Top) / (double)num);
                                float num9 = (float)(((double)num5 - (double)rectangleF.Top) / (double)num);
                                RectangleF rectangleF2 = new RectangleF(num6, num8, num7 - num6, num9 - num8);
                                rectangleF2.Inflate(rectangleF2.Width * 0.1f, rectangleF2.Width * 0.1f);
                                graphics2.DrawImage(bitmap_9, new System.Drawing.Rectangle((int)rectangleF2.X, (int)rectangleF2.Y, (int)rectangleF2.Width, (int)rectangleF2.Height), 0, 0, bitmap_9.Width, bitmap_9.Height, GraphicsUnit.Pixel, imageAttr);
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                using (ImageAttributes imageAttr2 = method_236(0.13))
                {
                    graphics.DrawImage(bitmap_8, new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height), 0, 0, bitmap_7.Width, bitmap_7.Height, GraphicsUnit.Pixel, imageAttr2);
                    return;
                }
            }
        }

        private Bitmap method_150(RectangleF rectangleF_1, RectangleF rectangleF_2, float float_0, float float_1)
        {
            int num = Math.Max(1, (int)rectangleF_1.Width);
            int num2 = Math.Max(1, (int)rectangleF_1.Height);
            Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (main_0.zEsBtZgRxo != ZoomStatus.Stabilizing && main_0.zEsBtZgRxo != 0)
                {
                    method_175(graphics);
                }
                else
                {
                    method_176(graphics);
                }
                graphics.Clear(System.Drawing.Color.Transparent);
                graphics.DrawImage(main_0.bitmap_182, rectangleF_1, rectangleF_2, GraphicsUnit.Pixel);
            }
            float num3 = 1f;
            float num4 = float_1 / (float)main_0.bitmap_182.Width;
            float num5 = num4 * num3 * float_0;
            float num6 = rectangleF_2.X * num4;
            float num7 = rectangleF_2.Y * num4;
            if (sectorCloudGenerator_1 == null)
            {
                sectorCloudGenerator_1 = new SectorCloudGenerator(1, 16);
            }
            float num8 = 20f;
            num6 = num6 / float_1 * num8;
            num7 = num7 / float_1 * num8;
            float num9 = float_1 * float_0;
            num5 = num5 / num9 * num8;
            new System.Drawing.Rectangle((int)rectangleF_1.X, (int)rectangleF_1.Y, (int)rectangleF_1.Width, (int)rectangleF_1.Height);
            return bitmap;
        }

        private bool method_151(System.Drawing.Rectangle rectangle_5, System.Drawing.Rectangle rectangle_6)
        {
            return rectangle_5.IntersectsWith(rectangle_6);
        }

        private bool method_152(StarFieldItem starFieldItem_0, System.Drawing.Rectangle rectangle_5)
        {
            return rectangle_5.Contains(starFieldItem_0.X, starFieldItem_0.Y);
        }

        private void method_153(Habitat habitat_1, int int_11, int int_12, Graphics graphics_0)
        {
            Race dominantRace = habitat_1.Population.DominantRace;
            if (dominantRace != null)
            {
                int num = 25;
                int num2 = 15;
                num = (int)(25.0 / main_0.double_0);
                num2 = (int)(15.0 / main_0.double_0);
                if (num < 15)
                {
                    num = 15;
                    num2 = 10;
                }
                int_11 += 5;
                int_12 += num2 + 5;
                if (habitat_1.Owner != null && habitat_1.Owner != main_0._Game.Galaxy.IndependentEmpire)
                {
                    int_12 += num2 + 3;
                }
                graphics_0.DrawImageUnscaled(rect: new System.Drawing.Rectangle(int_11, int_12, num, num), image: main_0.raceImageCache_0.GetRaceImage(dominantRace.PictureRef, useSmall: true, useAlternate: false));
            }
        }

        private void method_154(Habitat habitat_1, int int_11, int int_12, Graphics graphics_0)
        {
            int num = 25;
            int num2 = 15;
            num = (int)(25.0 / main_0.double_0);
            num2 = (int)(15.0 / main_0.double_0);
            if (num < 15)
            {
                num = 15;
                num2 = 10;
            }
            int_11 += 4;
            if (habitat_1.Owner != null && habitat_1.Owner != main_0._Game.Galaxy.IndependentEmpire)
            {
                int_12 += num2 + 3;
            }
            int num3 = num / 5;
            int num4 = num2 / 5;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    graphics_0.FillRectangle(main_0.solidBrush_2, int_11 + i * num3, int_12 + (5 - j) * num4, num3 - 1, num4 - 1);
                }
            }
            int num5 = 0;
            int num6 = 0;
            if (habitat_1.Population.TotalAmount > 2500000000L)
            {
                num5 = 5;
            }
            else if (habitat_1.Population.TotalAmount > 500000000L)
            {
                num5 = 4;
            }
            else if (habitat_1.Population.TotalAmount > 100000000L)
            {
                num5 = 3;
            }
            else if (habitat_1.Population.TotalAmount > 20000000L)
            {
                num5 = 2;
            }
            else if (habitat_1.Population.TotalAmount > 0L)
            {
                num5 = 1;
            }
            if (habitat_1.DevelopmentLevel > 80)
            {
                num6 = 5;
            }
            else if (habitat_1.DevelopmentLevel > 60)
            {
                num6 = 4;
            }
            else if (habitat_1.DevelopmentLevel > 40)
            {
                num6 = 3;
            }
            else if (habitat_1.DevelopmentLevel > 20)
            {
                num6 = 2;
            }
            else if (habitat_1.DevelopmentLevel > 0)
            {
                num6 = 1;
            }
            for (int k = 0; k < num5; k++)
            {
                for (int l = 0; l < num6; l++)
                {
                    graphics_0.FillRectangle(main_0.solidBrush_3, int_11 + k * num3, int_12 + (5 - l) * num4, num3 - 1, num4 - 1);
                }
            }
            if (habitat_0 != null && habitat_1 == habitat_0 && bool_2)
            {
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(int_11 - 3, int_12 - 1, 31, 20);
                method_200(rectangle_, graphics_0);
            }
        }

        private void method_155(Habitat habitat_1, int int_11, int int_12, Graphics graphics_0)
        {
            int num = 4;
            int num2 = 3;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    graphics_0.FillRectangle(main_0.solidBrush_2, int_11 + i * num, int_12 + (4 - j) * num2, num - 1, num2 - 1);
                }
            }
            int num3 = 0;
            int num4 = 0;
            if (habitat_1.Population.TotalAmount > 2500000000L)
            {
                num3 = 5;
            }
            else if (habitat_1.Population.TotalAmount > 500000000L)
            {
                num3 = 4;
            }
            else if (habitat_1.Population.TotalAmount > 100000000L)
            {
                num3 = 3;
            }
            else if (habitat_1.Population.TotalAmount > 20000000L)
            {
                num3 = 2;
            }
            else if (habitat_1.Population.TotalAmount > 0L)
            {
                num3 = 1;
            }
            if (habitat_1.DevelopmentLevel > 80)
            {
                num4 = 5;
            }
            else if (habitat_1.DevelopmentLevel > 60)
            {
                num4 = 4;
            }
            else if (habitat_1.DevelopmentLevel > 40)
            {
                num4 = 3;
            }
            else if (habitat_1.DevelopmentLevel > 20)
            {
                num4 = 2;
            }
            else if (habitat_1.DevelopmentLevel > 0)
            {
                num4 = 1;
            }
            for (int k = 0; k < num3; k++)
            {
                for (int l = 0; l < num4; l++)
                {
                    graphics_0.FillRectangle(main_0.solidBrush_3, int_11 + k * num, int_12 + (4 - l) * num2, num - 1, num2 - 1);
                }
            }
            if (habitat_0 != null && habitat_1 == habitat_0 && bool_2)
            {
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(int_11 - 3, int_12 - 1, 26, 20);
                method_200(rectangle_, graphics_0);
            }
        }

        private void method_156(Texture2D texture2D_61, StellarObject stellarObject_0, FighterWeapon fighterWeapon_0, out int int_11, out int int_12, out float float_0, out float float_1, out float float_2)
        {
            double num = 18.0;
            num = Math.Min(300.0, 10.0 * Math.Sqrt(fighterWeapon_0.RawDamage));
            double num2 = num / (double)texture2D_61.Height;
            _ = texture2D_61.Width;
            num2 /= main_0.double_0;
            double num3 = num2;
            double num4 = num2;
            double num5 = fighterWeapon_0.Heading;
            double num6 = fighterWeapon_0.X;
            double num7 = fighterWeapon_0.Y;
            double num8 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, fighterWeapon_0.X, fighterWeapon_0.Y);
            num8 /= main_0.double_0;
            if (num8 < num)
            {
                double num9 = num / num8;
                num3 /= num9;
            }
            double num10 = (num6 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num11 = (num7 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)num10;
            int_12 = (int)num11;
            float_0 = (float)num3;
            float_1 = (float)num4;
            float_2 = (float)num5;
        }

        private Bitmap method_157(Bitmap bitmap_7, Fighter fighter_0, FighterWeapon fighterWeapon_0, out int int_11, out int int_12)
        {
            double num = Math.Min(8.0, Math.Max(0.5, Math.Sqrt(Math.Max(0.5, fighterWeapon_0.Power)) / 2.5));
            double num2 = Math.Min(3.0, Math.Max(0.7, Math.Sqrt(Math.Max(0.5, fighterWeapon_0.Power)) / 3.0));
            num2 /= main_0.double_0;
            num /= main_0.double_0;
            double num3 = (double)fighterWeapon_0.Heading * -1.0;
            double num4 = fighterWeapon_0.X;
            double num5 = fighterWeapon_0.Y;
            int num6 = (int)((double)bitmap_7.Width * num);
            int num7 = (int)((double)bitmap_7.Height * num2);
            double num8 = galaxy_0.CalculateDistance(fighter_0.Xpos, fighter_0.Ypos, fighterWeapon_0.X, fighterWeapon_0.Y);
            num8 /= main_0.double_0;
            if (num8 < (double)num6)
            {
                num6 = (int)num8;
            }
            int num9 = 0;
            int num10 = 0;
            if (num6 > num7)
            {
                num10 = (num6 - num7) / 2;
            }
            if (num7 > num6)
            {
                num9 = (num7 - num6) / 2;
            }
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(num9, num10, num6, num7);
            int num11 = Math.Max(1, num6 + num9 * 2);
            int num12 = Math.Max(1, num7 + num10 * 2);
            Bitmap bitmap = new Bitmap(num11, num12, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                method_176(graphics);
                graphics.DrawImage(bitmap_7, destRect, srcRect, GraphicsUnit.Pixel);
            }
            bitmap = method_218(bitmap, (float)num3, GraphicsQuality.Medium);
            double num13 = (num4 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num14 = (num5 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)(num13 - (double)bitmap.Width / 2.0);
            int_12 = (int)(num14 - (double)bitmap.Height / 2.0);
            Math.Cos((double)fighterWeapon_0.Heading + Math.PI);
            _ = (double)bitmap.Width / 2.0;
            Math.Sin((double)fighterWeapon_0.Heading + Math.PI);
            _ = (double)bitmap.Width / 2.0;
            return bitmap;
        }

        private Bitmap method_158(Bitmap bitmap_7, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12)
        {
            StellarObject target = weapon_0.Target;
            double num = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target.Xpos, target.Ypos);
            double num2 = num / (double)bitmap_7.Width;
            double num3 = 1.0;
            num2 /= main_0.double_0;
            num3 /= main_0.double_0;
            int num4 = Math.Max(1, (int)((double)bitmap_7.Width * num2));
            int num5 = Math.Max(1, (int)((double)bitmap_7.Height * num3));
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, num4, num5);
            Bitmap bitmap = new Bitmap(num4, num5, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                method_176(graphics);
                graphics.DrawImage(bitmap_7, destRect, srcRect, GraphicsUnit.Pixel);
            }
            double num6 = (double)weapon_0.Heading * -1.0;
            bitmap = method_218(bitmap, (float)num6, GraphicsQuality.Medium);
            double num7 = Math.Min(stellarObject_0.Xpos, target.Xpos);
            double num8 = Math.Min(stellarObject_0.Ypos, target.Ypos);
            double num9 = (num7 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num10 = (num8 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)num9;
            int_12 = (int)num10;
            return bitmap;
        }

        private void method_159(Texture2D texture2D_61, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12, out float float_0, out float float_1, out float float_2)
        {
            method_160(texture2D_61, stellarObject_0, weapon_0, out int_11, out int_12, out float_0, out float_1, out float_2, bool_13: false);
        }

        private void method_160(Texture2D texture2D_61, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12, out float float_0, out float float_1, out float float_2, bool bool_13)
        {
            double num = 18.0;
            double num2 = 3.0;
            num = Math.Min(300.0, 10.0 * Math.Sqrt(weapon_0.RawDamage));
            if (weapon_0.Component != null && (weapon_0.Component.Type == ComponentType.WeaponRailGun || weapon_0.Component.Type == ComponentType.WeaponSuperRailGun))
            {
                num = Math.Min(300.0, 7.0 * Math.Sqrt(weapon_0.RawDamage));
            }
            else if (bool_13)
            {
                num = 18.0;
            }
            double num3 = num / (double)texture2D_61.Height;
            num2 = num3 * (double)texture2D_61.Width;
            method_161(texture2D_61, stellarObject_0, weapon_0, num, num2, out int_11, out int_12, out float_0, out float_1, out float_2, bool_13);
        }

        private void method_161(Texture2D texture2D_61, StellarObject stellarObject_0, Weapon weapon_0, double double_15, double double_16, out int int_11, out int int_12, out float float_0, out float float_1, out float float_2, bool bool_13)
        {
            double num = double_15 / (double)texture2D_61.Height;
            num /= main_0.double_0;
            double num2 = num;
            double num3 = num;
            double num4 = weapon_0.Heading;
            double num5 = weapon_0.X;
            double num6 = weapon_0.Y;
            if (!bool_13)
            {
                double num7 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, weapon_0.X, weapon_0.Y);
                num7 /= main_0.double_0;
                if (num7 < double_15)
                {
                    double num8 = double_15 / num7;
                    num2 /= num8;
                }
            }
            double num9 = (num5 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num10 = (num6 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)num9;
            int_12 = (int)num10;
            float_0 = (float)num2;
            float_1 = (float)num3;
            float_2 = (float)num4;
        }

        private Bitmap method_162(Bitmap bitmap_7, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12)
        {
            return method_163(bitmap_7, stellarObject_0, weapon_0, out int_11, out int_12, bool_13: false);
        }

        private Bitmap method_163(Bitmap bitmap_7, StellarObject stellarObject_0, Weapon weapon_0, out int int_11, out int int_12, bool bool_13)
        {
            double num = Math.Min(8.0, Math.Max(0.5, Math.Sqrt(Math.Max(0.5, weapon_0.Power)) / 2.5));
            double num2 = Math.Min(3.0, Math.Max(0.7, Math.Sqrt(Math.Max(0.5, weapon_0.Power)) / 3.0));
            num2 /= main_0.double_0;
            num /= main_0.double_0;
            if (weapon_0.Component.Type == ComponentType.WeaponRailGun)
            {
                num2 /= 2.5;
                num /= 2.5;
            }
            else if (weapon_0.Component.Type == ComponentType.WeaponPointDefense)
            {
                num2 /= 2.5;
                num /= 2.5;
            }
            else if (weapon_0.Component.Type == ComponentType.WeaponIonCannon)
            {
                num2 /= 2.2;
                num /= 2.2;
            }
            double num3 = (double)weapon_0.Heading * -1.0;
            double num4 = weapon_0.X;
            double num5 = weapon_0.Y;
            int num6 = (int)((double)bitmap_7.Width * num);
            int num7 = (int)((double)bitmap_7.Height * num2);
            if (!bool_13)
            {
                double num8 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, weapon_0.X, weapon_0.Y);
                num8 /= main_0.double_0;
                if (num8 < (double)num6)
                {
                    num6 = (int)num8;
                }
            }
            int num9 = 0;
            int num10 = 0;
            if (num6 > num7)
            {
                num10 = (num6 - num7) / 2;
            }
            if (num7 > num6)
            {
                num9 = (num7 - num6) / 2;
            }
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(num9, num10, num6, num7);
            int num11 = Math.Max(1, num6 + num9 * 2);
            int num12 = Math.Max(1, num7 + num10 * 2);
            Bitmap bitmap = new Bitmap(num11, num12, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                method_176(graphics);
                graphics.DrawImage(bitmap_7, destRect, srcRect, GraphicsUnit.Pixel);
            }
            bitmap = method_218(bitmap, (float)num3, GraphicsQuality.Medium);
            double num13 = (num4 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2);
            double num14 = (num5 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2);
            int_11 = (int)(num13 - (double)bitmap.Width / 2.0);
            int_12 = (int)(num14 - (double)bitmap.Height / 2.0);
            return bitmap;
        }

        private void method_164(Fighter fighter_0, DateTime dateTime_5, int int_11, int int_12, Graphics graphics_0)
        {
            method_176(graphics_0);
            for (int i = 0; i < fighter_0.Weapons.Count; i++)
            {
                if (!(fighter_0.Weapons[i].DistanceTravelled >= 0f))
                {
                    continue;
                }
                if (!fighter_0.Weapons[i].SoundEffectPlayed)
                {
                    double double_ = 0.0;
                    double double_2 = 0.0;
                    method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                    fighter_0.Weapons[i].SoundEffectPlayed = true;
                    main_0.method_0(main_0.EffectsPlayer.ResolveFighterWeapon(fighter_0.Specification.WeaponSoundEffectFilename, fighter_0.Weapons[i].Type, double_, double_2));
                }
                FighterWeapon fighterWeapon = fighter_0.Weapons[i];
                System.Drawing.Color.FromArgb(255, 255, 255);
                System.Drawing.Color.FromArgb(255, 255, 255);
                double num = Math.Min(1.0, (double)fighterWeapon.DistanceTravelled / (double)fighterWeapon.Range);
                double num2 = 1.0;
                double num3 = 0.6;
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        num3 = 0.75;
                        break;
                    case ComponentCategoryType.WeaponTorpedo:
                        num3 = 0.6;
                        break;
                }
                if (num > num3)
                {
                    num2 = (1.0 - num) / (1.0 - num3);
                }
                ImageAttributes imageAttributes = null;
                if (num2 < 1.0)
                {
                    imageAttributes = method_236(num2);
                }
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        {
                            Bitmap bitmap2 = null;
                            bitmap2 = ((fighterWeapon.SpecialImageIndex < 0 || fighterWeapon.SpecialImageIndex >= main_0.bitmap_13.Length) ? main_0.bitmap_13[0] : main_0.bitmap_13[fighterWeapon.SpecialImageIndex]);
                            int int_13 = 0;
                            int int_14 = 0;
                            using (Bitmap bitmap3 = method_157(bitmap2, fighter_0, fighterWeapon, out int_13, out int_14))
                            {
                                method_176(graphics_0);
                                if (num2 < 1.0 && imageAttributes != null)
                                {
                                    graphics_0.DrawImage(bitmap3, new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height), 0, 0, bitmap3.Width, bitmap3.Height, GraphicsUnit.Pixel, imageAttributes);
                                }
                                else
                                {
                                    graphics_0.DrawImage(bitmap3, new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height), 0, 0, bitmap3.Width, bitmap3.Height, GraphicsUnit.Pixel);
                                }
                                method_177(graphics_0);
                                System.Drawing.Rectangle item = new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height);
                                list_3.Add(item);
                            }
                            break;
                        }
                    case ComponentCategoryType.WeaponTorpedo:
                        {
                            float num4 = (float)(((double)fighterWeapon.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                            float num5 = (float)(((double)fighterWeapon.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                            int num6 = -1;
                            double num7 = 0.0;
                            double num8 = fighterWeapon.Power;
                            double num9 = -1000.0;
                            float val = (float)(num8 / 9.0) + 5f;
                            val = Math.Min(val, 18f);
                            float num10 = val / (float)main_0.double_0;
                            if (num10 < 1f)
                            {
                                num10 = 1f;
                            }
                            float num11 = num10;
                            float num12 = num4 - num10 / 2f;
                            float num13 = num5 - num11 / 2f;
                            double num14 = 1.0;
                            if (num6 < 0)
                            {
                                switch (fighterWeapon.Type)
                                {
                                    case ComponentType.WeaponTorpedo:
                                        num6 = ((fighterWeapon.SpecialImageIndex >= 0 && fighterWeapon.SpecialImageIndex < main_0.bitmap_12.Length) ? fighterWeapon.SpecialImageIndex : 0);
                                        num7 = Math.PI;
                                        break;
                                    case ComponentType.WeaponMissile:
                                        num6 = ((fighterWeapon.SpecialImageIndex >= 0 && fighterWeapon.SpecialImageIndex < main_0.bitmap_12.Length) ? fighterWeapon.SpecialImageIndex : 0);
                                        num7 = 0.0;
                                        num9 = fighterWeapon.Heading;
                                        num9 *= -1.0;
                                        num14 = 2.0;
                                        break;
                                }
                            }
                            double totalSeconds = dateTime_5.Subtract(fighterWeapon.LastFired).TotalSeconds;
                            float float_ = (float)(totalSeconds * num7);
                            if (num9 > -1000.0)
                            {
                                float_ = (float)num9;
                            }
                            using (Bitmap bitmap = method_218(main_0.bitmap_12[num6], float_, GraphicsQuality.High))
                            {
                                double num15 = num14 * ((double)bitmap.Width / (double)main_0.bitmap_12[num6].Width);
                                float num16 = (float)((double)num10 * num15);
                                float num17 = (float)((double)num11 * num15);
                                num12 += (num10 - num16) / 2f;
                                num13 += (num11 - num17) / 2f;
                                if (num2 < 1.0 && imageAttributes != null)
                                {
                                    graphics_0.DrawImage(bitmap, new System.Drawing.Rectangle((int)num12, (int)num13, (int)num16, (int)num17), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
                                }
                                else
                                {
                                    graphics_0.DrawImage(bitmap, new System.Drawing.Rectangle((int)num12, (int)num13, (int)num16, (int)num17), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
                                }
                                System.Drawing.Rectangle item = new System.Drawing.Rectangle((int)num12 - 1, (int)num13 - 1, (int)num10 + 2, (int)num11 + 2);
                                list_3.Add(item);
                            }
                            break;
                        }
                }
                imageAttributes?.Dispose();
            }
            method_177(graphics_0);
        }

        private void method_165(SpriteBatch spriteBatch_2, Fighter fighter_0, DateTime dateTime_5, int int_11, int int_12)
        {
            int weaponImageIndex = fighter_0.Specification.WeaponImageIndex;
            for (int i = 0; i < fighter_0.Weapons.Count; i++)
            {
                if (!(fighter_0.Weapons[i].DistanceTravelled >= 0f))
                {
                    continue;
                }
                if (!fighter_0.Weapons[i].SoundEffectPlayed)
                {
                    double double_ = 0.0;
                    double double_2 = 0.0;
                    method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                    fighter_0.Weapons[i].SoundEffectPlayed = true;
                    main_0.method_0(main_0.EffectsPlayer.ResolveFighterWeapon(fighter_0.Specification.WeaponSoundEffectFilename, fighter_0.Weapons[i].Type, double_, double_2));
                }
                FighterWeapon fighterWeapon = fighter_0.Weapons[i];
                System.Drawing.Color.FromArgb(255, 255, 255);
                System.Drawing.Color.FromArgb(255, 255, 255);
                double num = Math.Min(1.0, (double)fighterWeapon.DistanceTravelled / (double)fighterWeapon.Range);
                double num2 = 1.0;
                double num3 = 0.6;
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        num3 = 0.75;
                        break;
                    case ComponentCategoryType.WeaponTorpedo:
                        num3 = 0.6;
                        break;
                }
                if (num > num3)
                {
                    num2 = (1.0 - num) / (1.0 - num3);
                }
                System.Drawing.Color color = System.Drawing.Color.White;
                if (num2 < 1.0)
                {
                    color = System.Drawing.Color.FromArgb((int)(num2 * 255.0), 255, 255, 255);
                }
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        {
                            Texture2D texture2D2 = null;
                            texture2D2 = ((weaponImageIndex < 0 || weaponImageIndex >= texture2D_2.Length) ? texture2D_2[0] : texture2D_2[weaponImageIndex]);
                            int int_13 = 0;
                            int int_14 = 0;
                            float float_ = 1f;
                            float float_2 = 1f;
                            float float_3 = 0f;
                            method_156(texture2D2, fighter_0, fighterWeapon, out int_13, out int_14, out float_, out float_2, out float_3);
                            XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D2, int_13, int_14, float_3, new SizeF(float_2, float_), color, offsetPositionByCenter: false);
                            break;
                        }
                    case ComponentCategoryType.WeaponTorpedo:
                        {
                            float num4 = (float)(((double)fighterWeapon.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                            float num5 = (float)(((double)fighterWeapon.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                            int num6 = -1;
                            double num7 = 0.0;
                            double num8 = fighterWeapon.Power;
                            double num9 = -1000.0;
                            float num10 = (float)(num8 / 0.75) + 5f;
                            num10 = ((fighterWeapon.Type != ComponentType.WeaponMissile) ? ((float)(num8 / 4.0) + 7f) : ((float)(num8 / 0.6) + 5f));
                            num10 = Math.Min(num10, 18f);
                            float num11 = num10 / (float)main_0.double_0;
                            if (num11 < 1f)
                            {
                                num11 = 1f;
                            }
                            double num12 = 1.0;
                            if (num6 < 0)
                            {
                                switch (fighterWeapon.Type)
                                {
                                    case ComponentType.WeaponTorpedo:
                                        num6 = ((weaponImageIndex >= 0 && weaponImageIndex < texture2D_1.Length) ? weaponImageIndex : 0);
                                        num7 = Math.PI;
                                        break;
                                    case ComponentType.WeaponMissile:
                                        num6 = ((weaponImageIndex >= 0 && weaponImageIndex < texture2D_1.Length) ? weaponImageIndex : 0);
                                        num7 = 0.0;
                                        num9 = fighterWeapon.Heading;
                                        num12 = 2.0;
                                        break;
                                }
                            }
                            double totalSeconds = dateTime_5.Subtract(fighterWeapon.LastFired).TotalSeconds;
                            float rotationAngle = (float)(totalSeconds * num7);
                            if (num9 > -1000.0)
                            {
                                rotationAngle = (float)num9;
                            }
                            Texture2D texture2D = texture2D_1[num6];
                            num12 = (double)num11 / (double)texture2D.Width;
                            XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D, num4, num5, rotationAngle, (float)num12, color, offsetPositionByCenter: false);
                            break;
                        }
                }
            }
        }

        private void method_166(BuiltObject builtObject_1, DateTime dateTime_5, int int_11, int int_12, Graphics graphics_0)
        {
            method_176(graphics_0);
            for (int i = 0; i < builtObject_1.Weapons.Count; i++)
            {
                method_170(builtObject_1.Weapons[i], builtObject_1, dateTime_5, int_11, int_12, graphics_0);
            }
            method_177(graphics_0);
        }

        private void method_167(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, DateTime dateTime_5, int int_11, int int_12)
        {
            for (int i = 0; i < builtObject_1.Weapons.Count; i++)
            {
                method_171(spriteBatch_2, builtObject_1.Weapons[i], builtObject_1, dateTime_5, int_11, int_12);
            }
        }

        private void method_168(Habitat habitat_1, DateTime dateTime_5, int int_11, int int_12, Graphics graphics_0)
        {
            if (habitat_1.GiantIonCannonPresent && habitat_1.GiantIonCannon != null)
            {
                method_176(graphics_0);
                method_170(habitat_1.GiantIonCannon, habitat_1, dateTime_5, int_11, int_12, graphics_0);
                method_177(graphics_0);
            }
        }

        private void method_169(SpriteBatch spriteBatch_2, Habitat habitat_1, DateTime dateTime_5, int int_11, int int_12)
        {
            if (habitat_1.GiantIonCannonPresent && habitat_1.GiantIonCannon != null)
            {
                method_171(spriteBatch_2, habitat_1.GiantIonCannon, habitat_1, dateTime_5, int_11, int_12);
            }
        }

        private void method_170(Weapon weapon_0, StellarObject stellarObject_0, DateTime dateTime_5, int int_11, int int_12, Graphics graphics_0)
        {
            if (!(weapon_0.DistanceTravelled >= 0f))
            {
                return;
            }
            if (!weapon_0.SoundEffectPlayed)
            {
                double double_ = 0.0;
                double double_2 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                weapon_0.SoundEffectPlayed = true;
                main_0.method_0(main_0.EffectsPlayer.ResolveWeapon(weapon_0.Component, double_, double_2));
            }
            Component component = weapon_0.Component;
            System.Drawing.Color.FromArgb(255, 255, 255);
            System.Drawing.Color.FromArgb(255, 255, 255);
            double num = Math.Min(1.0, (double)weapon_0.DistanceTravelled / (double)weapon_0.Range);
            double num2 = 1.0;
            double num3 = 0.6;
            switch (component.Type)
            {
                case ComponentType.WeaponPhaser:
                    num3 = 0.97;
                    break;
                case ComponentType.WeaponTorpedo:
                    num3 = 0.6;
                    break;
                case ComponentType.WeaponMissile:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponRailGun:
                    num3 = 0.9;
                    break;
                case ComponentType.WeaponTractorBeam:
                    num3 = 0.95;
                    break;
                case ComponentType.AssaultPod:
                    num3 = 1.0;
                    break;
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponSuperBeam:
                    num3 = 0.75;
                    break;
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperArea:
                    num3 = 0.8;
                    break;
            }
            if (num > num3)
            {
                num2 = (1.0 - num) / (1.0 - num3);
            }
            ImageAttributes imageAttributes = null;
            if (num2 < 1.0)
            {
                imageAttributes = method_236(num2);
            }
            switch (component.Type)
            {
                case ComponentType.WeaponTorpedo:
                case ComponentType.WeaponMissile:
                    {
                        float num4 = (float)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                        float num5 = (float)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                        int num11 = -1;
                        double num12 = 0.0;
                        double num13 = weapon_0.Power;
                        double num14 = -1000.0;
                        if (weapon_0.BombardDamage > 0 && weapon_0.Target is Habitat)
                        {
                            num13 = (double)weapon_0.BombardDamage * 2.5;
                            num13 = Math.Min(num13, 60.0);
                            num11 = 0;
                            switch (component.ComponentID)
                            {
                                case 9:
                                    num11 = 1;
                                    num12 = 4.0840704496667311;
                                    break;
                                case 11:
                                    num14 = weapon_0.Heading;
                                    num14 *= -1.0;
                                    num11 = 4;
                                    num3 = 1.0;
                                    imageAttributes = method_236(1.0);
                                    break;
                                case 12:
                                    num14 = weapon_0.Heading;
                                    num14 *= -1.0;
                                    num11 = 5;
                                    num3 = 1.0;
                                    imageAttributes = method_236(1.0);
                                    break;
                            }
                        }
                        float val = (float)(num13 / 7.0) + 7f;
                        val = Math.Min(val, 18f);
                        float num9 = val / (float)main_0.double_0;
                        if (num9 < 1f)
                        {
                            num9 = 1f;
                        }
                        float num10 = num9;
                        float num8 = num4 - num9 / 2f;
                        float num7 = num5 - num10 / 2f;
                        double num15 = 1.0;
                        if (num11 < 0)
                        {
                            switch (weapon_0.Component.Type)
                            {
                                case ComponentType.WeaponTorpedo:
                                    num11 = ((weapon_0.Component.SpecialImageIndex >= 0 && weapon_0.Component.SpecialImageIndex < main_0.bitmap_12.Length) ? weapon_0.Component.SpecialImageIndex : 0);
                                    num12 = Math.PI;
                                    break;
                                case ComponentType.WeaponMissile:
                                    num11 = ((weapon_0.Component.SpecialImageIndex >= 0 && weapon_0.Component.SpecialImageIndex < main_0.bitmap_12.Length) ? weapon_0.Component.SpecialImageIndex : 0);
                                    num12 = 0.0;
                                    num14 = weapon_0.Heading;
                                    num14 *= -1.0;
                                    num15 = 3.5;
                                    break;
                            }
                        }
                        double totalSeconds = dateTime_5.Subtract(weapon_0.LastFired).TotalSeconds;
                        float float_ = (float)(totalSeconds * num12);
                        if (num14 > -1000.0)
                        {
                            float_ = (float)num14;
                        }
                        using (Bitmap bitmap2 = method_218(main_0.bitmap_12[num11], float_, GraphicsQuality.High))
                        {
                            double num16 = num15 * ((double)bitmap2.Width / (double)main_0.bitmap_12[num11].Width);
                            float num17 = (float)((double)num9 * num16);
                            float num18 = (float)((double)num10 * num16);
                            num8 += (num9 - num17) / 2f;
                            num7 += (num10 - num18) / 2f;
                            method_176(graphics_0);
                            if (num2 < 1.0 && imageAttributes != null)
                            {
                                graphics_0.DrawImage(bitmap2, new System.Drawing.Rectangle((int)num8, (int)num7, (int)num17, (int)num18), 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttributes);
                            }
                            else
                            {
                                graphics_0.DrawImage(bitmap2, new System.Drawing.Rectangle((int)num8, (int)num7, (int)num17, (int)num18), 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel);
                            }
                            method_177(graphics_0);
                        }
                        System.Drawing.Rectangle item2 = new System.Drawing.Rectangle((int)num8 - 1, (int)num7 - 1, (int)num9 + 2, (int)num10 + 2);
                        list_3.Add(item2);
                        break;
                    }
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponTractorBeam:
                case ComponentType.AssaultPod:
                case ComponentType.WeaponSuperBeam:
                case ComponentType.WeaponPhaser:
                case ComponentType.WeaponRailGun:
                    {
                        Bitmap bitmap_ = null;
                        switch (component.Type)
                        {
                            case ComponentType.WeaponBeam:
                            case ComponentType.WeaponPointDefense:
                            case ComponentType.WeaponIonCannon:
                            case ComponentType.WeaponSuperBeam:
                            case ComponentType.WeaponRailGun:
                                bitmap_ = ((component.SpecialImageIndex < 0 || component.SpecialImageIndex >= main_0.bitmap_13.Length) ? main_0.bitmap_13[0] : main_0.bitmap_13[component.SpecialImageIndex]);
                                break;
                            case ComponentType.AssaultPod:
                                bitmap_ = main_0.bitmap_15[0];
                                break;
                        }
                        int int_13 = 0;
                        int int_14 = 0;
                        if (component.Type == ComponentType.WeaponTractorBeam)
                        {
                            StellarObject target = weapon_0.Target;
                            if (target == null)
                            {
                                break;
                            }
                            double num19 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target.Xpos, target.Ypos);
                            if (num19 < (double)weapon_0.Range * 1.2)
                            {
                                method_177(graphics_0);
                                if (main_0.double_0 < 2.0)
                                {
                                    pen_7.Width = 3f;
                                    pen_8.Width = 1f;
                                }
                                else if (main_0.double_0 < 5.0)
                                {
                                    pen_7.Width = 2f;
                                    pen_8.Width = 1f;
                                }
                                else
                                {
                                    pen_7.Width = 1f;
                                    pen_8.Width = 0f;
                                }
                                int x = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                                int y = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                                double num20 = weapon_0.HeadingMissFactor;
                                if (!weapon_0.WillHitTarget)
                                {
                                    num20 *= 3.0;
                                }
                                double num21 = weapon_0.X;
                                double num22 = weapon_0.Y;
                                int x2 = (int)((num21 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                                int y2 = (int)((num22 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                                graphics_0.DrawLine(pen_7, x, y, x2, y2);
                                graphics_0.DrawLine(pen_8, x, y, x2, y2);
                                if (weapon_0.DistanceTravelled <= 3f && weapon_0.WillHitTarget && target is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)target;
                                    builtObject.LastTractorStrike = dateTime_5;
                                    builtObject.LastTractorStrikeDirection = weapon_0.Heading;
                                }
                            }
                            break;
                        }
                        if (component.Type == ComponentType.WeaponPhaser)
                        {
                            StellarObject target2 = weapon_0.Target;
                            if (target2 == null)
                            {
                                break;
                            }
                            double num23 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target2.Xpos, target2.Ypos);
                            if (!(num23 < (double)weapon_0.Range * 1.2))
                            {
                                break;
                            }
                            method_177(graphics_0);
                            if (main_0.double_0 < 2.0)
                            {
                                pen_5.Width = 3f;
                                pen_6.Width = 1f;
                            }
                            else if (main_0.double_0 < 5.0)
                            {
                                pen_5.Width = 2f;
                                pen_6.Width = 1f;
                            }
                            else
                            {
                                pen_5.Width = 1f;
                                pen_6.Width = 0f;
                            }
                            int x3 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                            int y3 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                            double num24 = weapon_0.HeadingMissFactor;
                            if (!weapon_0.WillHitTarget)
                            {
                                num24 *= 3.0;
                            }
                            double num25 = target2.Xpos + num24 * num23;
                            double num26 = target2.Ypos + num24 * num23;
                            int x4 = (int)((num25 - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                            int y4 = (int)((num26 - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                            graphics_0.DrawLine(pen_5, x3, y3, x4, y4);
                            graphics_0.DrawLine(pen_6, x3, y3, x4, y4);
                            if (weapon_0.DistanceTravelled <= 3f && weapon_0.WillHitTarget && target2 is BuiltObject)
                            {
                                BuiltObject builtObject2 = (BuiltObject)target2;
                                if (builtObject2.CurrentShields <= 5f)
                                {
                                    int val2 = (int)(Math.Sqrt(weapon_0.Power) * 10.0);
                                    val2 = Math.Min(35, val2);
                                    double rotationAngle = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                                    double num27 = 0.0;
                                    double num28 = 0.0;
                                    DistantWorlds.Animation animation = new DistantWorlds.Animation(main_0.bitmap_212, galaxy_0.CurrentDateTime, 100, target2.Xpos + num27, target2.Ypos + num28, val2, val2, rotationAngle, System.Drawing.Color.Empty);
                                    animationSystem_1.AddAnimation(animation);
                                }
                                else
                                {
                                    builtObject2.LastShieldStrike = dateTime_5;
                                    builtObject2.LastShieldStrikeDirection = weapon_0.Heading;
                                }
                            }
                            break;
                        }
                        if (component.Type == ComponentType.AssaultPod)
                        {
                            using (Bitmap bitmap3 = method_163(bitmap_, stellarObject_0, weapon_0, out int_13, out int_14, bool_13: true))
                            {
                                method_175(graphics_0);
                                graphics_0.DrawImage(bitmap3, new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height), 0, 0, bitmap3.Width, bitmap3.Height, GraphicsUnit.Pixel);
                                method_177(graphics_0);
                                System.Drawing.Rectangle item2 = new System.Drawing.Rectangle(int_13, int_14, bitmap3.Width, bitmap3.Height);
                                list_3.Add(item2);
                            }
                            break;
                        }
                        using (Bitmap bitmap4 = method_162(bitmap_, stellarObject_0, weapon_0, out int_13, out int_14))
                        {
                            method_175(graphics_0);
                            if (num2 < 1.0 && imageAttributes != null)
                            {
                                graphics_0.DrawImage(bitmap4, new System.Drawing.Rectangle(int_13, int_14, bitmap4.Width, bitmap4.Height), 0, 0, bitmap4.Width, bitmap4.Height, GraphicsUnit.Pixel, imageAttributes);
                            }
                            else
                            {
                                graphics_0.DrawImage(bitmap4, new System.Drawing.Rectangle(int_13, int_14, bitmap4.Width, bitmap4.Height), 0, 0, bitmap4.Width, bitmap4.Height, GraphicsUnit.Pixel);
                            }
                            method_177(graphics_0);
                            System.Drawing.Rectangle item2 = new System.Drawing.Rectangle(int_13, int_14, bitmap4.Width, bitmap4.Height);
                            list_3.Add(item2);
                        }
                        break;
                    }
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperArea:
                    {
                        Bitmap bitmap = null;
                        switch (component.Type)
                        {
                            case ComponentType.WeaponAreaDestruction:
                                bitmap = ((weapon_0.Component.ComponentID != 19) ? main_0.edLqkLkgAx[2] : main_0.edLqkLkgAx[0]);
                                break;
                            case ComponentType.WeaponSuperArea:
                                bitmap = main_0.edLqkLkgAx[1];
                                break;
                            case ComponentType.WeaponIonPulse:
                                bitmap = main_0.edLqkLkgAx[3];
                                break;
                        }
                        float num4 = (float)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                        float num5 = (float)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                        float num6 = (float)((double)weapon_0.DistanceTravelled / main_0.double_0);
                        float num7 = num5 - num6;
                        float num8 = num4 - num6;
                        float num9 = (float)((double)num6 * 2.0);
                        float num10 = (float)((double)num6 * 2.0);
                        method_175(graphics_0);
                        System.Drawing.Rectangle destRect = new System.Drawing.Rectangle((int)num8, (int)num7, (int)num9, (int)num10);
                        if (num2 < 1.0 && imageAttributes != null)
                        {
                            graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
                        }
                        else
                        {
                            graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
                        }
                        System.Drawing.Rectangle item = new System.Drawing.Rectangle((int)num8 - 2, (int)num7 - 2, (int)num9 + 4, (int)num10 + 4);
                        list_3.Add(item);
                        break;
                    }
            }
            imageAttributes?.Dispose();
        }

        private void method_171(SpriteBatch spriteBatch_2, Weapon weapon_0, StellarObject stellarObject_0, DateTime dateTime_5, int int_11, int int_12)
        {
            if (!(weapon_0.DistanceTravelled >= 0f))
            {
                return;
            }
            if (!weapon_0.SoundEffectPlayed)
            {
                double double_ = 0.0;
                double double_2 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                weapon_0.SoundEffectPlayed = true;
                main_0.method_0(main_0.EffectsPlayer.ResolveWeapon(weapon_0.Component, double_, double_2));
            }
            Component component = weapon_0.Component;
            if (weapon_0.Power == float.MaxValue)
            {
                int num = Galaxy.Rnd.Next(0, texture2D_8.Length);
                int num2 = Galaxy.Rnd.Next(10, 16);
                DistantWorlds.Animation animation = new DistantWorlds.Animation(texture2D_8[num], dateTime_5, 20, int_11, int_12, num2, num2);
                animation.DisposeTexturesWhenComplete = false;
                animationSystem_1.AddAnimation(animation);
                return;
            }
            System.Drawing.Color.FromArgb(255, 255, 255);
            System.Drawing.Color.FromArgb(255, 255, 255);
            double num3 = Math.Min(1.0, (double)weapon_0.DistanceTravelled / (double)weapon_0.Range);
            double num4 = 1.0;
            double num5 = 0.6;
            switch (component.Type)
            {
                case ComponentType.WeaponPhaser:
                case ComponentType.WeaponSuperPhaser:
                    num5 = 0.97;
                    break;
                case ComponentType.WeaponTorpedo:
                case ComponentType.WeaponSuperTorpedo:
                    num5 = 0.6;
                    break;
                case ComponentType.WeaponBombard:
                case ComponentType.WeaponMissile:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponRailGun:
                case ComponentType.WeaponSuperMissile:
                case ComponentType.WeaponSuperRailGun:
                    num5 = 0.9;
                    break;
                case ComponentType.WeaponTractorBeam:
                case ComponentType.WeaponGravityBeam:
                    num5 = 0.95;
                    break;
                case ComponentType.WeaponAreaGravity:
                    num5 = 0.9;
                    break;
                case ComponentType.AssaultPod:
                    num5 = 1.0;
                    break;
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponSuperBeam:
                    num5 = 0.75;
                    break;
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperArea:
                    num5 = 0.8;
                    break;
            }
            if (num3 > num5)
            {
                num4 = (1.0 - num3) / (1.0 - num5);
            }
            System.Drawing.Color color = System.Drawing.Color.FromArgb((int)(num4 * 255.0), 255, 255, 255);
            switch (component.Type)
            {
                case ComponentType.WeaponTorpedo:
                case ComponentType.WeaponBombard:
                case ComponentType.WeaponMissile:
                case ComponentType.WeaponSuperTorpedo:
                case ComponentType.WeaponSuperMissile:
                    {
                        float num9 = (float)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                        float num10 = (float)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                        int num43 = -1;
                        double num44 = 0.0;
                        double num45 = weapon_0.Power;
                        double num46 = -1000.0;
                        if (weapon_0.BombardDamage > 0 && weapon_0.Target is Habitat)
                        {
                            num45 = (double)weapon_0.BombardDamage * 2.5;
                            num45 = Math.Min(num45, 60.0);
                            num43 = 0;
                            num43 = ((component.SpecialImageIndex >= 0 && component.SpecialImageIndex < texture2D_1.Length) ? component.SpecialImageIndex : 0);
                            switch (component.Type)
                            {
                                default:
                                    num46 = weapon_0.Heading;
                                    num5 = 1.0;
                                    color = System.Drawing.Color.White;
                                    break;
                                case ComponentType.WeaponBeam:
                                case ComponentType.WeaponTorpedo:
                                case ComponentType.WeaponSuperTorpedo:
                                    num44 = 4.0840704496667311;
                                    break;
                            }
                        }
                        float num47 = 10f;
                        float val4 = 18f;
                        if (component.Type == ComponentType.WeaponMissile)
                        {
                            num47 = (float)(num45 / 5.0) + 5f;
                        }
                        else if (component.Type == ComponentType.WeaponSuperMissile)
                        {
                            num47 = (float)(num45 / 10.0) + 5f;
                            val4 = 38f;
                        }
                        else if (component.Type == ComponentType.WeaponSuperTorpedo)
                        {
                            num47 = (float)(num45 / 10.0) + 5f;
                            val4 = 48f;
                        }
                        else if (component.Type == ComponentType.WeaponBombard)
                        {
                            num47 = (float)(num45 / 0.5) + 6f;
                            val4 = 26f;
                        }
                        else
                        {
                            num47 = (float)(num45 / 3.0) + 7f;
                        }
                        num47 = Math.Min(num47, val4);
                        float num14 = num47 / (float)main_0.double_0;
                        if (num14 < 1f)
                        {
                            num14 = 1f;
                        }
                        float num15 = num14;
                        float num13 = num9 - num14 / 2f;
                        float num12 = num10 - num15 / 2f;
                        double num48 = 1.0;
                        if (num43 < 0)
                        {
                            num43 = ((weapon_0.Component.SpecialImageIndex >= 0 && weapon_0.Component.SpecialImageIndex < texture2D_1.Length) ? weapon_0.Component.SpecialImageIndex : 0);
                            switch (weapon_0.Component.Type)
                            {
                                case ComponentType.WeaponTorpedo:
                                case ComponentType.WeaponSuperTorpedo:
                                    num44 = Math.PI;
                                    break;
                                case ComponentType.WeaponBombard:
                                    num44 = 0.0;
                                    num46 = weapon_0.Heading;
                                    num48 = 3.5;
                                    break;
                                case ComponentType.WeaponMissile:
                                case ComponentType.WeaponSuperMissile:
                                    num44 = 0.0;
                                    num46 = weapon_0.Heading;
                                    num48 = 3.5;
                                    break;
                            }
                        }
                        double totalSeconds = dateTime_5.Subtract(weapon_0.LastFired).TotalSeconds;
                        float rotationAngle2 = (float)(totalSeconds * num44);
                        if (num46 > -1000.0)
                        {
                            rotationAngle2 = (float)num46;
                        }
                        Texture2D texture2D3 = texture2D_1[num43];
                        num48 *= (double)(num14 / (float)texture2D3.Width);
                        XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D3, num9, num10, rotationAngle2, (float)num48, color, offsetPositionByCenter: false);
                        System.Drawing.Rectangle item2 = new System.Drawing.Rectangle((int)num13 - 1, (int)num12 - 1, (int)num14 + 2, (int)num15 + 2);
                        list_3.Add(item2);
                        break;
                    }
                case ComponentType.WeaponAreaGravity:
                    {
                        float num36 = weapon_0.DistanceTravelled / (float)weapon_0.Range;
                        int val3 = 255;
                        if (num36 < 0.02f)
                        {
                            val3 = (int)(num36 * 50f * 255f);
                        }
                        else if (num36 > 0.9f)
                        {
                            val3 = (int)((0.1f - (num36 - 0.9f)) * 10f * 255f);
                        }
                        val3 = Math.Max(0, Math.Min(255, val3));
                        int num37 = 5;
                        num37 = ((main_0.double_0 < 2.0) ? 5 : ((!(main_0.double_0 < 5.0)) ? 1 : 3));
                        int x3 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int y3 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        int num38 = (int)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int num39 = (int)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        int alpha = (int)((double)val3 * 0.6);
                        System.Drawing.Color color6 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(alpha, 0, 176, 164), System.Drawing.Color.FromArgb(alpha, 0, 64, 56), dateTime_5);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, x3, y3, num38, num39, color6, num37);
                        GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(alpha, 0, 255, 240), System.Drawing.Color.FromArgb(alpha, 0, 96, 88), dateTime_5);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, x3, y3, num38, num39, color6, 1);
                        int num40 = (int)((double)weapon_0.DamageLoss * 2.0 / main_0.double_0);
                        int int_15 = num38 - num40 / 2;
                        int int_16 = num39 - num40 / 2;
                        System.Drawing.Color color_ = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(val3, 0, 0, 108), System.Drawing.Color.FromArgb(val3, 54, 0, 108), dateTime_5);
                        double num41 = (double)dateTime_5.Second + (double)dateTime_5.Millisecond / 1000.0;
                        double double_3 = Math.PI / 10.0 * (num41 % 20.0);
                        method_118(spriteBatch_2, int_15, int_16, num40, num40, texture2D_12[0], dateTime_5, double_3, color_);
                        int num42 = (int)((double)weapon_0.BombardDamage * 2.0 / main_0.double_0);
                        int int_17 = num38 - num42 / 2;
                        int int_18 = num39 - num42 / 2;
                        method_117(spriteBatch_2, int_17, int_18, num42, num42, texture2D_13, dateTime_5, 25, 0.0, 0.0, System.Drawing.Color.FromArgb(val3, 255, 255, 255));
                        break;
                    }
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponTractorBeam:
                case ComponentType.WeaponGravityBeam:
                case ComponentType.AssaultPod:
                case ComponentType.WeaponSuperBeam:
                case ComponentType.WeaponPhaser:
                case ComponentType.WeaponRailGun:
                case ComponentType.WeaponSuperPhaser:
                case ComponentType.WeaponSuperRailGun:
                    {
                        Texture2D texture2D2 = null;
                        switch (component.Type)
                        {
                            case ComponentType.AssaultPod:
                                texture2D2 = texture2D_5[0];
                                break;
                            case ComponentType.WeaponBeam:
                            case ComponentType.WeaponPointDefense:
                            case ComponentType.WeaponIonCannon:
                            case ComponentType.WeaponTractorBeam:
                            case ComponentType.WeaponGravityBeam:
                            case ComponentType.WeaponSuperBeam:
                            case ComponentType.WeaponPhaser:
                            case ComponentType.WeaponRailGun:
                            case ComponentType.WeaponSuperPhaser:
                            case ComponentType.WeaponSuperRailGun:
                                texture2D2 = ((component.SpecialImageIndex < 0 || component.SpecialImageIndex >= texture2D_2.Length) ? texture2D_2[0] : texture2D_2[component.SpecialImageIndex]);
                                break;
                        }
                        int int_13 = 0;
                        int int_14 = 0;
                        if (component.Type == ComponentType.WeaponGravityBeam)
                        {
                            StellarObject target = weapon_0.Target;
                            if (target == null)
                            {
                                break;
                            }
                            double num16 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target.Xpos, target.Ypos);
                            if (num16 < (double)weapon_0.Range * 1.2)
                            {
                                int num17 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                                int num18 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                                double num19 = weapon_0.HeadingMissFactor;
                                if (!weapon_0.WillHitTarget)
                                {
                                    num19 *= 3.0;
                                }
                                float firingAngle = (float)Galaxy.DetermineAngle(stellarObject_0.Xpos, stellarObject_0.Ypos, weapon_0.X, weapon_0.Y);
                                _ = base.Width / 2;
                                _ = base.Height / 2;
                                float num20 = (float)(num16 / main_0.double_0 / (double)texture2D2.Width);
                                float num21 = (float)(1.0 / main_0.double_0);
                                SizeF scaleFactor = new SizeF(num20, num21);
                                System.Drawing.Color color3 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(144, 255, 255, 255), System.Drawing.Color.FromArgb(255, 255, 255, 255), dateTime_5);
                                XnaDrawingHelper.DrawTextureStretchedBeam(spriteBatch_2, texture2D2, num17, num18, firingAngle, scaleFactor, color3);
                            }
                            break;
                        }
                        if (component.Type == ComponentType.WeaponTractorBeam)
                        {
                            StellarObject target2 = weapon_0.Target;
                            if (target2 == null)
                            {
                                break;
                            }
                            double num22 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target2.Xpos, target2.Ypos);
                            if (num22 < (double)weapon_0.Range * 1.2)
                            {
                                int num23 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                                int num24 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                                double num25 = weapon_0.HeadingMissFactor;
                                if (!weapon_0.WillHitTarget)
                                {
                                    num25 *= 3.0;
                                }
                                float firingAngle2 = (float)Galaxy.DetermineAngle(stellarObject_0.Xpos, stellarObject_0.Ypos, target2.Xpos, target2.Ypos);
                                _ = base.Width / 2;
                                _ = base.Height / 2;
                                float num26 = (float)(num22 / main_0.double_0 / (double)texture2D2.Width);
                                float num27 = (float)(1.0 / main_0.double_0);
                                SizeF scaleFactor2 = new SizeF(num26, num27);
                                System.Drawing.Color color4 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(144, 255, 255, 255), System.Drawing.Color.FromArgb(255, 255, 255, 255), dateTime_5);
                                XnaDrawingHelper.DrawTextureStretchedBeam(spriteBatch_2, texture2D2, num23, num24, firingAngle2, scaleFactor2, color4);
                                if (weapon_0.DistanceTravelled <= 3f && weapon_0.WillHitTarget && target2 is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)target2;
                                    builtObject.LastTractorStrike = dateTime_5;
                                    builtObject.LastTractorStrikeDirection = weapon_0.Heading;
                                }
                            }
                            break;
                        }
                        if (component.Type != ComponentType.WeaponPhaser && component.Type != ComponentType.WeaponSuperPhaser)
                        {
                            if (component.Type == ComponentType.AssaultPod)
                            {
                                float float_ = 1f;
                                float float_2 = 1f;
                                float float_3 = 0f;
                                method_160(texture2D2, stellarObject_0, weapon_0, out int_13, out int_14, out float_, out float_2, out float_3, bool_13: true);
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D2, int_13, int_14, float_3, new SizeF(float_2, float_), color, offsetPositionByCenter: false);
                            }
                            else
                            {
                                float float_4 = 1f;
                                float float_5 = 1f;
                                float float_6 = 0f;
                                method_159(texture2D2, stellarObject_0, weapon_0, out int_13, out int_14, out float_4, out float_5, out float_6);
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D2, int_13, int_14, float_6, new SizeF(float_5, float_4), color, offsetPositionByCenter: false);
                            }
                            break;
                        }
                        StellarObject target3 = weapon_0.Target;
                        if (target3 == null)
                        {
                            break;
                        }
                        double num28 = galaxy_0.CalculateDistance(stellarObject_0.Xpos, stellarObject_0.Ypos, target3.Xpos, target3.Ypos);
                        if (!(num28 < (double)weapon_0.Range * 1.2))
                        {
                            break;
                        }
                        int num29 = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int num30 = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        double num31 = weapon_0.HeadingMissFactor;
                        if (!weapon_0.WillHitTarget)
                        {
                            num31 *= 3.0;
                        }
                        float firingAngle3 = (float)Galaxy.DetermineAngle(stellarObject_0.Xpos, stellarObject_0.Ypos, target3.Xpos, target3.Ypos);
                        _ = base.Width / 2;
                        _ = base.Height / 2;
                        float num32 = (float)(num28 / main_0.double_0 / (double)texture2D2.Width);
                        float num33 = (float)(1.0 / main_0.double_0);
                        SizeF scaleFactor3 = new SizeF(num32, num33);
                        System.Drawing.Color color5 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(144, 255, 255, 255), System.Drawing.Color.FromArgb(255, 255, 255, 255), dateTime_5);
                        XnaDrawingHelper.DrawTextureStretchedBeam(spriteBatch_2, texture2D2, num29, num30, firingAngle3, scaleFactor3, color5);
                        if (weapon_0.DistanceTravelled <= 3f && weapon_0.WillHitTarget && target3 is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)target3;
                            if (builtObject2.CurrentShields <= 5f)
                            {
                                int val2 = (int)(Math.Sqrt(weapon_0.Power) * 10.0);
                                val2 = Math.Min(35, val2);
                                double rotationAngle = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                                double num34 = 0.0;
                                double num35 = 0.0;
                                DistantWorlds.Animation animation2 = new DistantWorlds.Animation(texture2D_33, galaxy_0.CurrentDateTime, 100, target3.Xpos + num34, target3.Ypos + num35, val2, val2, rotationAngle, System.Drawing.Color.Empty);
                                animation2.DisposeTexturesWhenComplete = false;
                                animationSystem_1.AddAnimation(animation2);
                            }
                            else
                            {
                                builtObject2.LastShieldStrike = dateTime_5;
                                builtObject2.LastShieldStrikeDirection = weapon_0.Heading;
                            }
                        }
                        break;
                    }
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperArea:
                    {
                        Texture2D texture2D = null;
                        int num6 = 0;
                        num6 = ((component.SpecialImageIndex >= 0 && component.SpecialImageIndex < texture2D_3.Length) ? component.SpecialImageIndex : 0);
                        texture2D = texture2D_3[num6];
                        float num7 = weapon_0.DistanceTravelled / (float)weapon_0.Range;
                        int val = 255;
                        if (num7 < 0.02f)
                        {
                            val = (int)(num7 * 50f * 255f);
                        }
                        else if (num7 > 0.9f)
                        {
                            val = (int)((0.1f - (num7 - 0.9f)) * 10f * 255f);
                        }
                        val = Math.Max(0, Math.Min(255, val));
                        int num8 = 5;
                        num8 = ((main_0.double_0 < 2.0) ? 5 : ((!(main_0.double_0 < 5.0)) ? 1 : 3));
                        int x = (int)((stellarObject_0.Xpos - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int y = (int)((stellarObject_0.Ypos - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        int x2 = (int)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0 + (double)(base.Width / 2));
                        int y2 = (int)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0 + (double)(base.Height / 2));
                        System.Drawing.Color color2 = GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(val, 144, 0, 176), System.Drawing.Color.FromArgb(val, 48, 0, 64), dateTime_5);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, x, y, x2, y2, color2, num8);
                        GraphicsHelper.OscillateColor(System.Drawing.Color.FromArgb(val, 232, 0, 255), System.Drawing.Color.FromArgb(val, 80, 0, 96), dateTime_5);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, x, y, x2, y2, color2, 1);
                        float num9 = (float)((weapon_0.X - (double)(main_0.int_13 + main_0.int_21)) / main_0.double_0) + (float)(base.Width / 2);
                        float num10 = (float)((weapon_0.Y - (double)(main_0.int_14 + main_0.vhadzRiecM)) / main_0.double_0) + (float)(base.Height / 2);
                        float num11 = (float)((double)weapon_0.DistanceTravelled / main_0.double_0);
                        float num12 = num10 - num11;
                        float num13 = num9 - num11;
                        float num14 = (float)((double)num11 * 2.0);
                        float num15 = (float)((double)num11 * 2.0);
                        XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle((int)num13, (int)num12, (int)num14, (int)num15), spriteBatch: spriteBatch_0, texture: texture2D, rotationAngle: 0f, tintColor: color);
                        System.Drawing.Rectangle item = new System.Drawing.Rectangle((int)num13 - 2, (int)num12 - 2, (int)num14 + 4, (int)num15 + 4);
                        list_3.Add(item);
                        break;
                    }
            }
        }

        private void method_172(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, DateTime dateTime_5)
        {
            if (habitat_1 != null && habitat_1.PlanetaryShieldPresent)
            {
                double num = 0.0;
                int second = dateTime_5.Second;
                int num2 = dateTime_5.Millisecond;
                if (second % 2 == 1)
                {
                    num2 += 1000;
                }
                num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
                float num3 = 0.07f + (float)(num / 35.0);
                int num4 = (int)(26.0 / main_0.double_0);
                int_11 -= num4;
                int_12 -= num4;
                int_13 += num4 * 2;
                int_14 += num4 * 2;
                using ImageAttributes imageAttr = method_236(num3);
                Bitmap bitmap = main_0.bitmap_18[0];
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
                graphics_0.DrawImage(bitmap, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttr);
            }
        }

        private void method_173(SpriteBatch spriteBatch_2, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, DateTime dateTime_5)
        {
            if (habitat_1 != null && habitat_1.PlanetaryShieldPresent)
            {
                double num = 0.0;
                int second = dateTime_5.Second;
                int num2 = dateTime_5.Millisecond;
                if (second % 2 == 1)
                {
                    num2 += 1000;
                }
                num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
                float num3 = 0.6f + (float)(num * 0.20000000298023224);
                int num4 = (int)(26.0 / main_0.double_0);
                int_11 -= num4;
                int_12 -= num4;
                int_13 += num4 * 2;
                int_14 += num4 * 2;
                System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
                System.Drawing.Color tintColor = System.Drawing.Color.FromArgb((int)(num3 * 255f), 255, 255, 255);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_7[0], destination, 0f, tintColor);
            }
        }

        public void DrawCreatureToMain(Creature creature, Bitmap ToDraw, int x, int y, Graphics graphics, double zoomFactor, int maxWidth, int preRotatedWidth, int preRotatedHeight)
        {
            float num = (float)maxWidth / (float)preRotatedWidth;
            float num2 = (float)ToDraw.Width * num;
            float num3 = (float)ToDraw.Height * num;
            float num4 = (num2 - (float)preRotatedWidth) / 2f;
            float num5 = (num3 - (float)preRotatedHeight) / 2f;
            num4 *= num;
            num5 *= num;
            RectangleF destRect = new RectangleF(x, y, num2, num3);
            RectangleF srcRect = new RectangleF(0f, 0f, ToDraw.Width, ToDraw.Height);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.DrawImage(ToDraw, destRect, srcRect, GraphicsUnit.Pixel);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
        }

        public void DrawCreatureToMainXna(SpriteBatch spriteBatch, Creature creature, Texture2D ToDraw, int x, int y, int drawWidth, int drawHeight, float heading)
        {
            XnaDrawingHelper.DrawTexture(spriteBatch, ToDraw, x, y, drawWidth, drawHeight, heading);
        }

        public void DrawFighterToMain(Bitmap ToDraw, int x, int y, Graphics graphics)
        {
            graphics.DrawImageUnscaled(ToDraw, x, y);
        }

        public void DrawFighterToMainXna(SpriteBatch spriteBatch, Texture2D ToDraw, Fighter fighter, System.Drawing.Rectangle destination)
        {
            float rotationAngle = fighter.Heading;
            XnaDrawingHelper.DrawTexture(spriteBatch, ToDraw, destination, rotationAngle);
        }

        public void DrawBuiltObjectToMain(Bitmap ToDraw, int x, int y, Graphics graphics)
        {
            graphics.DrawImageUnscaled(ToDraw, x, y);
        }

        public void DrawBuiltObjectToMainXna(SpriteBatch spriteBatch, Texture2D ToDraw, BuiltObject builtObject, System.Drawing.Rectangle destination, bool fadeCivilianShips)
        {
            float rotationAngle = builtObject.Heading;
            System.Drawing.Color tintColor = System.Drawing.Color.White;
            if (fadeCivilianShips && builtObject.Owner == null)
            {
                tintColor = System.Drawing.Color.FromArgb(144, 255, 255, 255);
            }
            XnaDrawingHelper.DrawTexture(spriteBatch, ToDraw, destination, rotationAngle, tintColor);
        }

        private RectangleF method_174(Bitmap bitmap_7, double double_15)
        {
            float num = 0f;
            float num2 = 0f;
            float num3 = (float)bitmap_7.Width * (float)double_15;
            float num4 = (float)bitmap_7.Height * (float)double_15;
            return new RectangleF(num, num2, num3, num4);
        }

        public void DrawGasCloudToMain(Bitmap gasCloudImage, Habitat gasCloud, int x, int y, int width, int height, Graphics graphics)
        {
            method_136(gasCloud);
            if (bitmap_2 == null)
            {
                return;
            }
            RectangleF rectangleF_ = method_174(bitmap_2, 1.0);
            RectangleF rectangleF = method_137(gasCloud, rectangleF_);
            if (rectangleF != System.Drawing.Rectangle.Empty)
            {
                float num = 0f;
                if (rectangleF.X > 0f)
                {
                    num = rectangleF.X * (float)double_3;
                }
                float num2 = 0f;
                if (rectangleF.Y > 0f)
                {
                    num2 = rectangleF.Y * (float)double_3;
                }
                float num3 = rectangleF.Width * (float)double_3;
                float num4 = rectangleF.Height * (float)double_3;
                rectangleF = new RectangleF(num, num2, num3, num4);
                rectangleF.Offset(rectangleF_0.X * -1f, rectangleF_0.Y * -1f);
                RectangleF destRect = method_140(gasCloud, bitmap_2);
                method_176(graphics);
                graphics.DrawImage(bitmap_3, destRect, rectangleF, GraphicsUnit.Pixel);
                method_177(graphics);
            }
        }

        public void DrawGasCloudToMainXna(SpriteBatch spriteBatch, Bitmap gasCloudImage, Habitat gasCloud, int x, int y, int width, int height)
        {
            method_136(gasCloud);
            if (bitmap_2 == null || texture2D_56 == null)
            {
                return;
            }
            RectangleF rectangleF_ = method_174(bitmap_2, 1.0);
            RectangleF rectangleF = method_137(gasCloud, rectangleF_);
            if (rectangleF != System.Drawing.Rectangle.Empty)
            {
                float num = 0f;
                if (rectangleF.X > 0f)
                {
                    num = rectangleF.X * (float)double_3;
                }
                float num2 = 0f;
                if (rectangleF.Y > 0f)
                {
                    num2 = rectangleF.Y * (float)double_3;
                }
                float num3 = rectangleF.Width * (float)double_3;
                float num4 = rectangleF.Height * (float)double_3;
                rectangleF = new RectangleF(num, num2, num3, num4);
                rectangleF.Offset(rectangleF_0.X * -1f, rectangleF_0.Y * -1f);
                RectangleF rectangleF2 = method_140(gasCloud, bitmap_2);
                XnaDrawingHelper.DrawTexture(source: new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), destination: new System.Drawing.Rectangle((int)rectangleF2.X, (int)rectangleF2.Y, (int)rectangleF2.Width, (int)rectangleF2.Height), spriteBatch: spriteBatch, texture: texture2D_57);
            }
        }

        public void DrawHabToMain(Bitmap ToDraw, Bitmap rings, int x, int y, int width, int height, Graphics graphics, Habitat habitat)
        {
            int num = x;
            int num2 = y;
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            int num3 = width;
            int num4 = height;
            num3 = (int)((double)num3 / main_0.double_0);
            num4 = (int)((double)num4 / main_0.double_0);
            if (x + num3 > base.Width)
            {
                num3 = base.Width;
            }
            if (y + num4 > base.Height)
            {
                num4 = base.Height;
            }
            method_175(graphics);
            if (ToDraw != null)
            {
                graphics.DrawImageUnscaledAndClipped(ToDraw, new System.Drawing.Rectangle(num, num2, ToDraw.Width, ToDraw.Height));
            }
            method_177(graphics);
        }

        private void method_175(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics_0.SmoothingMode = SmoothingMode.None;
            graphics_0.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
        }

        private void method_176(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            graphics_0.InterpolationMode = InterpolationMode.Low;
            graphics_0.SmoothingMode = SmoothingMode.None;
            graphics_0.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics_0.PixelOffsetMode = PixelOffsetMode.None;
        }

        private void method_177(Graphics graphics_0)
        {
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        public System.Drawing.Color ResolveShipSymbolColor(BuiltObject builtObject)
        {
            System.Drawing.Color result = System.Drawing.Color.Gray;
            bool flag = true;
            if (builtObject.Empire == null || (builtObject.Empire == galaxy_0.IndependentEmpire && (builtObject.PirateEmpireId <= 0 || builtObject.PirateEmpireId != galaxy_0.PlayerEmpire.EmpireId)))
            {
                flag = false;
            }
            if (flag && builtObject.Empire != null)
            {
                result = builtObject.Empire.MainColor;
                double val = Math.Max(0.6, main_0.double_0 / 3.0);
                val = Math.Min(1.0, val);
                if (builtObject.Empire.PirateEmpireBaseHabitat != null && builtObject.Empire.MainColor == System.Drawing.Color.FromArgb(1, 1, 1))
                {
                    result = System.Drawing.Color.FromArgb(48, 48, 48);
                }
                result = System.Drawing.Color.FromArgb((int)(val * 255.0), result.R, result.G, result.B);
            }
            return result;
        }

        public void DrawShipSymbol(Graphics graphics, BuiltObject builtObject, System.Drawing.Color color, float x, float y, float width, float height, float originalWidth, float originalHeight, bool fillInterior, double zoomFactor)
        {
            Pen pen = new Pen(color, 2f);
            SolidBrush solidBrush = new SolidBrush(color);
            if (fillInterior)
            {
                System.Drawing.Color color2 = method_226(color, 64);
                pen = new Pen(color2, 2f);
            }
            if (zoomFactor <= 50.0 && builtObject.Owner == null && builtObject.Empire != galaxy_0.IndependentEmpire && builtObject.Empire != null)
            {
                pen.DashStyle = DashStyle.Dot;
            }
            List<PointF> list = new List<PointF>();
            float num = 1.2f;
            if (builtObject.Role == BuiltObjectRole.Military)
            {
                num = 1.5f;
            }
            else if (builtObject.Role == BuiltObjectRole.Exploration || builtObject.Role == BuiltObjectRole.Colony)
            {
                num = 1.5f;
            }
            float num2 = originalWidth * num;
            float num3 = originalHeight * num;
            float num4 = x - (num2 - width) / 2f;
            float num5 = y - (num3 - height) / 2f;
            x = num4;
            y = num5;
            width = num2;
            height = num3;
            if (width > height)
            {
                y -= (width - height) / 2f;
                height = width;
            }
            switch (builtObject.Role)
            {
                case BuiltObjectRole.Military:
                    {
                        float num24 = width / 1.15470052f;
                        list.Add(new PointF(x + width / 2f, y));
                        list.Add(new PointF(x + width, y + num24));
                        list.Add(new PointF(x, y + num24));
                        if (fillInterior)
                        {
                            method_239(graphics, solidBrush, list.ToArray(), width);
                        }
                        method_238(graphics, pen, list.ToArray(), width);
                        break;
                    }
                case BuiltObjectRole.Freight:
                    if (fillInterior)
                    {
                        method_243(graphics, solidBrush, new RectangleF(x, y, width, height));
                    }
                    method_241(graphics, pen, new RectangleF(x, y, width, height));
                    break;
                case BuiltObjectRole.Passenger:
                    {
                        if (fillInterior)
                        {
                            method_243(graphics, solidBrush, new RectangleF(x, y, width, height));
                        }
                        method_241(graphics, pen, new RectangleF(x, y, width, height));
                        float num21 = width / 5f;
                        float num22 = x + width / 2f;
                        float num23 = y + height / 2f;
                        graphics.DrawLine(pen, num22, y - num21, num22, y);
                        graphics.DrawLine(pen, x + width, num23, x + width + num21, num23);
                        graphics.DrawLine(pen, num22, y + height + num21, num22, y + height);
                        graphics.DrawLine(pen, x - num21, num23, x, num23);
                        break;
                    }
                case BuiltObjectRole.Exploration:
                case BuiltObjectRole.Colony:
                    list.Add(new PointF(x + width / 2f, y));
                    list.Add(new PointF(x + width, y + height / 2f));
                    list.Add(new PointF(x + width / 2f, y + height));
                    list.Add(new PointF(x, y + height / 2f));
                    if (fillInterior)
                    {
                        method_239(graphics, solidBrush, list.ToArray(), width);
                    }
                    method_238(graphics, pen, list.ToArray(), width);
                    break;
                case BuiltObjectRole.Build:
                    if (fillInterior)
                    {
                        graphics.FillRectangle(solidBrush, new RectangleF(x, y, width, height));
                    }
                    graphics.DrawRectangle(pen, x, y, width, height);
                    break;
                case BuiltObjectRole.Resource:
                    {
                        if (fillInterior)
                        {
                            method_243(graphics, solidBrush, new RectangleF(x, y, width, height));
                        }
                        method_241(graphics, pen, new RectangleF(x, y, width, height));
                        float num19 = 0.707106769f;
                        float num20 = width / 2f - width / 2f * num19;
                        graphics.DrawLine(pen, x + num20, y + num20, x, y);
                        graphics.DrawLine(pen, x + width - num20, y + num20, x + width, y);
                        graphics.DrawLine(pen, x + width - num20, y + height - num20, x + width, y + height);
                        graphics.DrawLine(pen, x + num20, y + height - num20, x, y + height);
                        break;
                    }
                case BuiltObjectRole.Base:
                    {
                        float num6 = height * 1.15470052f;
                        x -= height * 0.154700533f / 2f;
                        float num7 = num6 / 2f;
                        float num8 = (num6 - num7) / 2f;
                        list.Add(new PointF(x + num8, y));
                        list.Add(new PointF(x + num8 + num7, y));
                        list.Add(new PointF(x + num8 + num7 + num8, y + height / 2f));
                        list.Add(new PointF(x + num8 + num7, y + height));
                        list.Add(new PointF(x + num8, y + height));
                        list.Add(new PointF(x, y + height / 2f));
                        if (builtObject.SubRole != BuiltObjectSubRole.MiningStation && builtObject.SubRole != BuiltObjectSubRole.GasMiningStation)
                        {
                            if (fillInterior)
                            {
                                method_239(graphics, solidBrush, list.ToArray(), width);
                            }
                            method_238(graphics, pen, list.ToArray(), width);
                            break;
                        }
                        if (fillInterior)
                        {
                            method_239(graphics, solidBrush, list.ToArray(), width);
                        }
                        method_238(graphics, pen, list.ToArray(), width);
                        float num9 = 0.577350259f;
                        float num10 = num9 * num7;
                        float num11 = num10 + num7 * 0.3f;
                        float num12 = 0.9999582f;
                        float num13 = 0.5f;
                        float num14 = num12 * num10;
                        float num15 = num13 * num10;
                        float num16 = num12 * num11;
                        float num17 = num13 * num11;
                        num14 = num8 / 2f;
                        num15 = height / 4f;
                        num16 = 0f;
                        num17 = num15 - num14 / 2f;
                        float num18 = height / 6f;
                        graphics.DrawLine(pen, x + num14, y + num15, x + num16, y + num17);
                        graphics.DrawLine(pen, x + num6 - num14, y + num15, x + num6 - num16, y + num17);
                        graphics.DrawLine(pen, x + num6 - num14, y + height - num15, x + num6 - num16, y + height - num17);
                        graphics.DrawLine(pen, x + num14, y + height - num15, x + num16, y + height - num17);
                        graphics.DrawLine(pen, x + num6 / 2f, y, x + num6 / 2f, y - num18);
                        graphics.DrawLine(pen, x + num6 / 2f, y + height, x + num6 / 2f, y + height + num18);
                        break;
                    }
            }
            solidBrush.Dispose();
            pen.Dispose();
        }

        private Texture2D method_178(BuiltObject builtObject_1, bool bool_13)
        {
            Texture2D result = null;
            if (bool_13)
            {
                switch (builtObject_1.Role)
                {
                    case BuiltObjectRole.Military:
                        result = texture2D_40;
                        break;
                    case BuiltObjectRole.Freight:
                        result = texture2D_41;
                        break;
                    case BuiltObjectRole.Passenger:
                        result = texture2D_43;
                        break;
                    case BuiltObjectRole.Exploration:
                    case BuiltObjectRole.Colony:
                        result = texture2D_39;
                        break;
                    case BuiltObjectRole.Build:
                        result = texture2D_38;
                        break;
                    case BuiltObjectRole.Resource:
                        result = texture2D_42;
                        break;
                    case BuiltObjectRole.Base:
                        switch (builtObject_1.SubRole)
                        {
                            case BuiltObjectSubRole.GasMiningStation:
                            case BuiltObjectSubRole.MiningStation:
                                result = texture2D_37;
                                break;
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.ResortBase:
                            case BuiltObjectSubRole.GenericBase:
                            case BuiltObjectSubRole.EnergyResearchStation:
                            case BuiltObjectSubRole.WeaponsResearchStation:
                            case BuiltObjectSubRole.HighTechResearchStation:
                            case BuiltObjectSubRole.MonitoringStation:
                            case BuiltObjectSubRole.DefensiveBase:
                                result = texture2D_36;
                                break;
                        }
                        break;
                }
            }
            else
            {
                switch (builtObject_1.Role)
                {
                    case BuiltObjectRole.Military:
                        result = texture2D_48;
                        break;
                    case BuiltObjectRole.Freight:
                        result = texture2D_49;
                        break;
                    case BuiltObjectRole.Passenger:
                        result = texture2D_51;
                        break;
                    case BuiltObjectRole.Exploration:
                    case BuiltObjectRole.Colony:
                        result = texture2D_47;
                        break;
                    case BuiltObjectRole.Build:
                        result = texture2D_46;
                        break;
                    case BuiltObjectRole.Resource:
                        result = texture2D_50;
                        break;
                    case BuiltObjectRole.Base:
                        switch (builtObject_1.SubRole)
                        {
                            case BuiltObjectSubRole.GasMiningStation:
                            case BuiltObjectSubRole.MiningStation:
                                result = texture2D_45;
                                break;
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.ResortBase:
                            case BuiltObjectSubRole.GenericBase:
                            case BuiltObjectSubRole.EnergyResearchStation:
                            case BuiltObjectSubRole.WeaponsResearchStation:
                            case BuiltObjectSubRole.HighTechResearchStation:
                            case BuiltObjectSubRole.MonitoringStation:
                            case BuiltObjectSubRole.DefensiveBase:
                                result = texture2D_44;
                                break;
                        }
                        break;
                }
            }
            return result;
        }

        public void DrawShipSymbolXna(SpriteBatch spriteBatch, BuiltObject builtObject, System.Drawing.Color color, float x, float y, float width, float height, float originalWidth, float originalHeight, bool galaxyLevel, DateTime time)
        {
            color = method_226(color, 48);
            if (builtObject.AssaultOwnershipChangeCounter > 0)
            {
                System.Drawing.Color color_ = System.Drawing.Color.FromArgb(240, 240, 240);
                color = method_215(color, color_, time);
            }
            Texture2D texture2D = method_178(builtObject, galaxyLevel);
            float num = 1.2f;
            if (builtObject.Role == BuiltObjectRole.Military)
            {
                num = 1.4f;
            }
            else if (builtObject.Role != BuiltObjectRole.Exploration && builtObject.Role != BuiltObjectRole.Colony)
            {
                if (builtObject.SubRole != BuiltObjectSubRole.MiningShip && builtObject.SubRole != BuiltObjectSubRole.GasMiningShip)
                {
                    if (builtObject.SubRole == BuiltObjectSubRole.PassengerShip)
                    {
                        num = 1.6f;
                    }
                    else if (builtObject.SubRole == BuiltObjectSubRole.MiningStation || builtObject.SubRole == BuiltObjectSubRole.GasMiningStation)
                    {
                        num = 1.8f;
                    }
                }
                else
                {
                    num = 1.3f;
                }
            }
            else
            {
                num = 1.5f;
            }
            if (galaxyLevel)
            {
                num *= 1.2f;
                color = System.Drawing.Color.FromArgb(255, color);
            }
            float num2 = originalWidth * num;
            float num3 = originalHeight * num;
            float num4 = (float)texture2D.Height / (float)texture2D.Width;
            num2 /= num4;
            float num5 = x - (num2 - width) / 2f;
            float num6 = y - (num3 - height) / 2f;
            x = num5;
            y = num6;
            width = num2;
            height = num3;
            int num7 = (int)(x + 0.5f);
            int num8 = (int)(y + 0.5f);
            int num9 = (int)(width + 0.5f);
            int num10 = (int)(height + 0.5f);
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(num7, num8, num9, num10);
            XnaDrawingHelper.DrawTexture(spriteBatch, texture2D, destination, 0f, color);
        }

        public void DrawShipSymbolXna_OLD(SpriteBatch spriteBatch, BuiltObject builtObject, System.Drawing.Color color, float x, float y, float width, float height, float originalWidth, float originalHeight, bool fillInterior, double zoomFactor)
        {
            color = method_226(color, 64);
            bool dashed = false;
            if (zoomFactor <= 50.0 && builtObject.Owner == null && builtObject.Empire != galaxy_0.IndependentEmpire && builtObject.Empire != null)
            {
                dashed = true;
            }
            List<PointF> list = new List<PointF>();
            float num = 1.2f;
            if (builtObject.Role == BuiltObjectRole.Military)
            {
                num = 1.5f;
            }
            else if (builtObject.Role == BuiltObjectRole.Exploration || builtObject.Role == BuiltObjectRole.Colony)
            {
                num = 1.5f;
            }
            float num2 = originalWidth * num;
            float num3 = originalHeight * num;
            float num4 = x - (num2 - width) / 2f;
            float num5 = y - (num3 - height) / 2f;
            x = num4;
            y = num5;
            width = num2;
            height = num3;
            if (width > height)
            {
                y -= (width - height) / 2f;
                height = width;
            }
            switch (builtObject.Role)
            {
                case BuiltObjectRole.Military:
                    {
                        float num19 = width / 1.15470052f;
                        list.Add(new PointF(x + width / 2f, y));
                        list.Add(new PointF(x + width, y + num19));
                        list.Add(new PointF(x, y + num19));
                        XnaDrawingHelper.DrawPolygon(spriteBatch, list.ToArray(), color, 2);
                        break;
                    }
                case BuiltObjectRole.Freight:
                    XnaDrawingHelper.DrawCircle(spriteBatch, new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height), 60, color, 2, dashed);
                    break;
                case BuiltObjectRole.Passenger:
                    {
                        XnaDrawingHelper.DrawCircle(spriteBatch, new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height), 60, color, 2, dashed);
                        float num20 = width / 5f;
                        float num21 = x + width / 2f;
                        float num22 = y + height / 2f;
                        XnaDrawingHelper.DrawLine(spriteBatch, num21, y - num20, num21, y, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + width, num22, x + width + num20, num22, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, num21, y + height + num20, num21, y + height, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x - num20, num22, x, num22, color, 2);
                        break;
                    }
                case BuiltObjectRole.Exploration:
                case BuiltObjectRole.Colony:
                    list.Add(new PointF(x + width / 2f, y));
                    list.Add(new PointF(x + width, y + height / 2f));
                    list.Add(new PointF(x + width / 2f, y + height));
                    list.Add(new PointF(x, y + height / 2f));
                    XnaDrawingHelper.DrawPolygon(spriteBatch, list.ToArray(), color, 2);
                    break;
                case BuiltObjectRole.Build:
                    XnaDrawingHelper.DrawRectangle(spriteBatch, (int)x, (int)y, (int)width, (int)height, color, 2);
                    break;
                case BuiltObjectRole.Resource:
                    {
                        XnaDrawingHelper.DrawCircle(spriteBatch, new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height), 60, color, 2, dashed);
                        float num23 = 0.707106769f;
                        float num24 = width / 2f - width / 2f * num23;
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num24, y + num24, x, y, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + width - num24, y + num24, x + width, y, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + width - num24, y + height - num24, x + width, y + height, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num24, y + height - num24, x, y + height, color, 2);
                        break;
                    }
                case BuiltObjectRole.Base:
                    {
                        float num6 = height * 1.15470052f;
                        x -= height * 0.154700533f / 2f;
                        float num7 = num6 / 2f;
                        float num8 = (num6 - num7) / 2f;
                        list.Add(new PointF(x + num8, y));
                        list.Add(new PointF(x + num8 + num7, y));
                        list.Add(new PointF(x + num8 + num7 + num8, y + height / 2f));
                        list.Add(new PointF(x + num8 + num7, y + height));
                        list.Add(new PointF(x + num8, y + height));
                        list.Add(new PointF(x, y + height / 2f));
                        if (builtObject.SubRole != BuiltObjectSubRole.MiningStation && builtObject.SubRole != BuiltObjectSubRole.GasMiningStation)
                        {
                            XnaDrawingHelper.DrawPolygon(spriteBatch, list.ToArray(), color, 2);
                            break;
                        }
                        XnaDrawingHelper.DrawPolygon(spriteBatch, list.ToArray(), color, 2);
                        float num9 = 0.577350259f;
                        float num10 = num9 * num7;
                        float num11 = num10 + num7 * 0.3f;
                        float num12 = 0.9999582f;
                        float num13 = 0.5f;
                        float num14 = num12 * num10;
                        float num15 = num13 * num10;
                        float num16 = num12 * num11;
                        float num17 = num13 * num11;
                        num14 = num8 / 2f;
                        num15 = height / 4f;
                        num16 = 0f;
                        num17 = num15 - num14 / 2f;
                        float num18 = height / 6f;
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num14, y + num15, x + num16, y + num17, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num6 - num14, y + num15, x + num6 - num16, y + num17, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num6 - num14, y + height - num15, x + num6 - num16, y + height - num17, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num14, y + height - num15, x + num16, y + height - num17, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num6 / 2f, y, x + num6 / 2f, y - num18, color, 2);
                        XnaDrawingHelper.DrawLine(spriteBatch, x + num6 / 2f, y + height, x + num6 / 2f, y + height + num18, color, 2);
                        break;
                    }
            }
        }

        public void InvalidateMain()
        {
            if (!bool_11)
            {
                Invalidate();
            }
        }

        public void ClearMain()
        {
            if (!bool_11)
            {
                using (Graphics graphics = CreateGraphics())
                {
                    graphics.Clear(main_0.color_3);
                }
            }
        }

        private void method_179(Habitat habitat_1, Graphics graphics_0, double double_15)
        {
            if (habitat_1.Explosions == null || habitat_1.Explosions.Count <= 0)
            {
                return;
            }
            Explosion[] array = habitat_1.Explosions.ToArray();
            Explosion[] array2 = array;
            foreach (Explosion explosion in array2)
            {
                int explosionSize = explosion.ExplosionSize;
                int num = explosionSize;
                int int_ = (int)((double)((int)(habitat_1.Xpos + (double)explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
                int int_2 = (int)((double)((int)(habitat_1.Ypos + (double)explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
                explosionSize = (int)((double)explosionSize / double_15);
                num = (int)((double)num / double_15);
                Bitmap bitmap = main_0.bitmap_19[explosion.ExplosionImageIndex][explosion.ExplosionCurrentImage];
                System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
                if (explosionSize > 80)
                {
                    method_176(graphics_0);
                }
                else
                {
                    method_175(graphics_0);
                }
                graphics_0.DrawImage(bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
                list_3.Add(rectangle);
                if (!explosion.ExplosionSoundPlayed)
                {
                    double double_16 = 0.0;
                    double double_17 = 0.0;
                    method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                    main_0.method_0(main_0.EffectsPlayer.ResolveExplosion(explosion.ExplosionSize, double_16, double_17));
                    explosion.ExplosionSoundPlayed = true;
                    if (explosion.ExplosionSize > 150)
                    {
                        int val = explosion.ExplosionSize / 50;
                        val = Math.Min(val, 8);
                        main_0.method_217(val);
                    }
                }
            }
            method_177(graphics_0);
        }

        private void method_180(SpriteBatch spriteBatch_2, Habitat habitat_1, double double_15)
        {
            if (habitat_1.Explosions == null || habitat_1.Explosions.Count <= 0)
            {
                return;
            }
            Explosion[] array = habitat_1.Explosions.ToArray();
            Explosion[] array2 = array;
            foreach (Explosion explosion in array2)
            {
                int explosionSize = explosion.ExplosionSize;
                int num = explosionSize;
                int int_ = (int)((double)((int)(habitat_1.Xpos + (double)explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
                int int_2 = (int)((double)((int)(habitat_1.Ypos + (double)explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
                explosionSize = (int)((double)explosionSize / double_15);
                num = (int)((double)num / double_15);
                Texture2D texture = texture2D_8[explosion.ExplosionImageIndex][explosion.ExplosionCurrentImage];
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, rectangle, 0f);
                list_3.Add(rectangle);
                if (!explosion.ExplosionSoundPlayed)
                {
                    double double_16 = 0.0;
                    double double_17 = 0.0;
                    method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                    main_0.method_0(main_0.EffectsPlayer.ResolveExplosion(explosion.ExplosionSize, double_16, double_17));
                    explosion.ExplosionSoundPlayed = true;
                    if (explosion.ExplosionSize > 150)
                    {
                        int val = explosion.ExplosionSize / 50;
                        val = Math.Min(val, 8);
                        main_0.method_217(val);
                    }
                }
            }
        }

        private void method_181(BuiltObject builtObject_1, Graphics graphics_0, double double_15)
        {
            Explosion[] explosion_ = builtObject_1.Explosions.ToArray();
            GwpRjOqBrJa(builtObject_1, explosion_, graphics_0, double_15);
        }

        private void method_182(Fighter fighter_0, Graphics graphics_0, double double_15)
        {
            Explosion[] explosion_ = fighter_0.Explosions.ToArray();
            GwpRjOqBrJa(fighter_0, explosion_, graphics_0, double_15);
        }

        private void GwpRjOqBrJa(StellarObject stellarObject_0, Explosion[] explosion_0, Graphics graphics_0, double double_15)
        {
            foreach (Explosion explosion in explosion_0)
            {
                int explosionSize = explosion.ExplosionSize;
                int num = explosionSize;
                int int_ = (int)((double)((int)(stellarObject_0.Xpos + (double)explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
                int int_2 = (int)((double)((int)(stellarObject_0.Ypos + (double)explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
                explosionSize = (int)((double)explosionSize / double_15);
                num = (int)((double)num / double_15);
                Bitmap bitmap = main_0.bitmap_19[explosion.ExplosionImageIndex][explosion.ExplosionCurrentImage];
                System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
                if (explosionSize > 50)
                {
                    method_176(graphics_0);
                }
                else
                {
                    method_175(graphics_0);
                }
                graphics_0.DrawImage(bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
                list_3.Add(rectangle);
                if (!explosion.ExplosionSoundPlayed)
                {
                    double double_16 = 0.0;
                    double double_17 = 0.0;
                    method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                    main_0.method_0(main_0.EffectsPlayer.ResolveExplosion(explosion.ExplosionSize, double_16, double_17));
                    explosion.ExplosionSoundPlayed = true;
                    if (explosion.ExplosionSize > 150)
                    {
                        int val = explosion.ExplosionSize / 50;
                        val = Math.Min(val, 8);
                        main_0.method_217(val);
                    }
                }
            }
            method_177(graphics_0);
        }

        private void method_183(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, double double_15)
        {
            Explosion[] explosion_ = builtObject_1.Explosions.ToArray();
            method_185(spriteBatch_2, builtObject_1, explosion_, double_15);
        }

        private void method_184(SpriteBatch spriteBatch_2, Fighter fighter_0, double double_15)
        {
            Explosion[] explosion_ = fighter_0.Explosions.ToArray();
            method_185(spriteBatch_2, fighter_0, explosion_, double_15);
        }

        private void method_185(SpriteBatch spriteBatch_2, StellarObject stellarObject_0, Explosion[] explosion_0, double double_15)
        {
            foreach (Explosion explosion in explosion_0)
            {
                int explosionSize = explosion.ExplosionSize;
                int num = explosionSize;
                int int_ = (int)((double)((int)(stellarObject_0.Xpos + (double)explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
                int int_2 = (int)((double)((int)(stellarObject_0.Ypos + (double)explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
                explosionSize = (int)((double)explosionSize / double_15);
                num = (int)((double)num / double_15);
                Texture2D texture = texture2D_8[explosion.ExplosionImageIndex][explosion.ExplosionCurrentImage];
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, rectangle, 0f);
                list_3.Add(rectangle);
                if (!explosion.ExplosionSoundPlayed)
                {
                    double double_16 = 0.0;
                    double double_17 = 0.0;
                    method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                    main_0.method_0(main_0.EffectsPlayer.ResolveExplosion(explosion.ExplosionSize, double_16, double_17));
                    explosion.ExplosionSoundPlayed = true;
                    if (explosion.ExplosionSize > 150)
                    {
                        int val = explosion.ExplosionSize / 50;
                        val = Math.Min(val, 8);
                        main_0.method_217(val);
                    }
                }
            }
        }

        private void method_186(Habitat habitat_1, Graphics graphics_0, double double_15)
        {
            if (habitat_1.Explosion == null)
            {
                return;
            }
            int explosionSize = habitat_1.Explosion.ExplosionSize;
            int num = explosionSize;
            int int_ = (int)((double)((int)(habitat_1.Xpos + (double)habitat_1.Explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
            int int_2 = (int)((double)((int)(habitat_1.Ypos + (double)habitat_1.Explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
            explosionSize = (int)((double)explosionSize / double_15);
            num = (int)((double)num / double_15);
            Bitmap bitmap = null;
            bitmap = ((habitat_1.Diameter > 50) ? main_0.bitmap_20[habitat_1.Explosion.ExplosionCurrentImage] : main_0.bitmap_19[0][habitat_1.Explosion.ExplosionCurrentImage]);
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
            method_176(graphics_0);
            graphics_0.DrawImage(bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
            method_177(graphics_0);
            list_3.Add(rectangle);
            if (!habitat_1.Explosion.ExplosionSoundPlayed)
            {
                double double_16 = 0.0;
                double double_17 = 0.0;
                method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                main_0.method_0(main_0.EffectsPlayer.ResolvePlanetExplosion(habitat_1.Explosion.ExplosionSize, double_16, double_17));
                habitat_1.Explosion.ExplosionSoundPlayed = true;
                if (habitat_1.Explosion.ExplosionSize > 100)
                {
                    main_0.method_218(9, 25);
                }
            }
        }

        private void method_187(SpriteBatch spriteBatch_2, Habitat habitat_1, double double_15)
        {
            if (habitat_1.Explosion == null)
            {
                return;
            }
            int explosionSize = habitat_1.Explosion.ExplosionSize;
            int num = explosionSize;
            int int_ = (int)((double)((int)(habitat_1.Xpos + (double)habitat_1.Explosion.ExplosionOffsetX) - (main_0.int_13 + main_0.int_21 + explosionSize / 2)) / main_0.double_0) + base.Width / 2;
            int int_2 = (int)((double)((int)(habitat_1.Ypos + (double)habitat_1.Explosion.ExplosionOffsetY) - (main_0.int_14 + main_0.vhadzRiecM + num / 2)) / main_0.double_0) + base.Height / 2;
            explosionSize = (int)((double)explosionSize / double_15);
            num = (int)((double)num / double_15);
            Texture2D texture2D = null;
            bool flag = true;
            if (habitat_1.Diameter <= 50)
            {
                texture2D = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_19[0][habitat_1.Explosion.ExplosionCurrentImage]);
            }
            else
            {
                texture2D = main_0.texture2D_1[habitat_1.Explosion.ExplosionCurrentImage];
                flag = false;
            }
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_, int_2, explosionSize, num);
            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, rectangle, 0f);
            if (flag)
            {
                method_22(texture2D);
            }
            list_3.Add(rectangle);
            if (!habitat_1.Explosion.ExplosionSoundPlayed)
            {
                double double_16 = 0.0;
                double double_17 = 0.0;
                method_90(int_, int_2, main_0.double_0, out double_16, out double_17);
                main_0.method_0(main_0.EffectsPlayer.ResolvePlanetExplosion(habitat_1.Explosion.ExplosionSize, double_16, double_17));
                habitat_1.Explosion.ExplosionSoundPlayed = true;
                if (habitat_1.Explosion.ExplosionSize > 100)
                {
                    main_0.method_218(9, 25);
                }
            }
        }

        private void method_188(int int_11, int int_12, Empire empire_0, Graphics graphics_0)
        {
            graphics_0.DrawImageUnscaled(empire_0.SmallFlagPicture, int_11, int_12);
        }

        private void method_189(int int_11, int int_12, Graphics graphics_0)
        {
            graphics_0.DrawImageUnscaled(main_0.bitmap_44, int_11, int_12);
        }

        private void method_190(int int_11, int int_12, Graphics graphics_0)
        {
            graphics_0.DrawImageUnscaled(main_0.bitmap_43, int_11, int_12);
        }

        private void method_191(SpriteBatch spriteBatch_2, int int_11, int int_12)
        {
            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_10, int_11, int_12, 0f, 1f);
        }

        private void method_192(int int_11, int int_12, Empire empire_0, Graphics graphics_0)
        {
            int num = 24;
            int num2 = 15;
            num = (int)(24.0 / main_0.double_0);
            num2 = (int)(15.0 / main_0.double_0);
            if (num < 15)
            {
                num = 15;
                num2 = 10;
            }
            graphics_0.DrawImage(rect: new System.Drawing.Rectangle(int_11 + 4, int_12 + 4, num, num2), image: empire_0.LargeFlagPicture);
        }

        private void method_193(int int_11, int int_12, int int_13, int int_14, int int_15, Graphics graphics_0)
        {
            int num = (int)((double)int_15 / (double)int_14 * (double)int_13);
            if (int_15 < int_14)
            {
                graphics_0.DrawLine(pen_9, int_11 + num, int_12, int_11 + int_13, int_12);
            }
            graphics_0.DrawLine(pen_10, int_11, int_12, int_11 + num, int_12);
        }

        private void method_194(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, int int_15)
        {
            int num = (int)((double)int_15 / (double)int_14 * (double)int_13);
            if (int_15 < int_14)
            {
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num, int_12, int_11 + int_13, int_12, color_10, 2);
            }
            XnaDrawingHelper.DrawLine(spriteBatch_2, int_11, int_12, int_11 + num, int_12, color_9, 2);
        }

        private void method_195(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17)
        {
            if (int_17 > 0)
            {
                int num = int_15 + int_16 + int_17;
                int num2 = (int)(Math.Min(1.0, (double)int_15 / (double)num) * (double)int_13);
                int num3 = (int)(Math.Min(1.0, (double)int_16 / (double)num) * (double)int_13);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11, int_12, int_11 + num2, int_12, color_11, 2);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num2, int_12, int_11 + num2 + num3, int_12, color_12, 2);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num2 + num3, int_12, int_11 + int_13, int_12, color_14, 2);
            }
            else if (int_14 > 0)
            {
                int num4 = (int)(Math.Min(1.0, (double)int_15 / (double)int_14) * (double)int_13);
                int num5 = (int)(Math.Min(1.0, (double)int_16 / (double)int_14) * (double)int_13);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11, int_12, int_11 + num4, int_12, color_11, 2);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num4, int_12, int_11 + num4 + num5, int_12, color_12, 2);
                XnaDrawingHelper.DrawLine(spriteBatch_2, int_11 + num4 + num5, int_12, int_11 + int_13, int_12, color_13, 2);
            }
        }

        private void method_196(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            rectangle_5 = new System.Drawing.Rectangle(rectangle_5.X - 12, rectangle_5.Y - 12, rectangle_5.Width + 24, rectangle_5.Height + 24);
            graphics_0.DrawEllipse(pen, rectangle_5);
        }

        private void method_197(SpriteBatch spriteBatch_2, System.Drawing.Rectangle rectangle_5)
        {
            using (new Pen(color_2, 4f))
            {
                rectangle_5 = new System.Drawing.Rectangle(rectangle_5.X - 12, rectangle_5.Y - 12, rectangle_5.Width + 24, rectangle_5.Height + 24);
                XnaDrawingHelper.DrawCircle(spriteBatch_2, rectangle_5, color_2, 4);
            }
        }

        private void method_198(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_199(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_200(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_201(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_202(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_203(SpriteBatch spriteBatch_2, System.Drawing.Rectangle rectangle_5)
        {
            XnaDrawingHelper.DrawRectangle(spriteBatch_2, rectangle_5, color_2, 4);
        }

        private void method_204(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            graphics_0.DrawRectangle(pen, rectangle_5);
        }

        private void method_205(SpriteBatch spriteBatch_2, System.Drawing.Rectangle rectangle_5)
        {
            XnaDrawingHelper.DrawRectangle(spriteBatch_2, rectangle_5, color_2, 4);
        }

        private void method_206(RectangleF rectangleF_1, Graphics graphics_0)
        {
            using Pen pen = new Pen(color_2, 4f);
            RectangleF rect = new RectangleF(rectangleF_1.X - 8f, rectangleF_1.Y - 8f, rectangleF_1.Width + 16f, rectangleF_1.Height + 16f);
            graphics_0.DrawEllipse(pen, rect);
        }

        private void method_207(System.Drawing.Rectangle rectangle_5, Graphics graphics_0)
        {
            RectangleF rectangleF_ = new RectangleF(rectangle_5.X, rectangle_5.Y, rectangle_5.Width, rectangle_5.Height);
            method_206(rectangleF_, graphics_0);
        }

        private void method_208(SpriteBatch spriteBatch_2, RectangleF rectangleF_1)
        {
            RectangleF rectangleF_2 = new RectangleF(rectangleF_1.X - 8f, rectangleF_1.Y - 8f, rectangleF_1.Width + 16f, rectangleF_1.Height + 16f);
            XnaDrawingHelper.DrawCircle(spriteBatch_2, method_249(rectangleF_2), color_2, 4);
        }

        private void method_209(SpriteBatch spriteBatch_2, System.Drawing.Rectangle rectangle_5)
        {
            RectangleF rectangleF_ = new RectangleF(rectangle_5.X, rectangle_5.Y, rectangle_5.Width, rectangle_5.Height);
            method_208(spriteBatch_2, rectangleF_);
        }

        private void method_210(int int_11, int int_12, int int_13, int int_14, Graphics graphics_0)
        {
            method_211(int_11, int_12, int_13, int_14, graphics_0);
        }

        private void method_211(float float_0, float float_1, float float_2, float float_3, Graphics graphics_0)
        {
            method_213();
            Pen pen = new Pen(color_5, 5f);
            pen.EndCap = LineCap.Round;
            pen.StartCap = LineCap.Round;
            double num = 0.0;
            int second = DateTime.Now.ToUniversalTime().Second;
            int num2 = DateTime.Now.ToUniversalTime().Millisecond;
            if (second % 3 == 1)
            {
                num2 += 1000;
            }
            else if (second % 3 == 2)
            {
                num2 += 2000;
            }
            num = (double)num2 / 3000.0;
            int num3 = (int)(num * 90.0);
            int num4 = num3;
            int num5 = 90 + num3;
            int num6 = 180 + num3;
            int num7 = 270 + num3;
            if (num4 > 360)
            {
                num4 -= 360;
            }
            if (num5 > 360)
            {
                num5 -= 360;
            }
            if (num6 > 360)
            {
                num6 -= 360;
            }
            if (num7 > 360)
            {
                num7 -= 360;
            }
            float num8 = float_2 - float_0;
            float num9 = float_3 - float_1;
            if (num8 < 12f)
            {
                float_0 -= (12f - num8) / 2f;
                num8 = 12f;
            }
            if (num9 < 12f)
            {
                float_1 -= (12f - num9) / 2f;
                num9 = 12f;
            }
            RectangleF rect = new RectangleF(float_0, float_1, num8, num9);
            graphics_0.DrawArc(pen, rect, num4, 70f);
            graphics_0.DrawArc(pen, rect, num5, 70f);
            graphics_0.DrawArc(pen, rect, num6, 70f);
            graphics_0.DrawArc(pen, rect, num7, 70f);
            Pen pen2 = new Pen(color_6, 2f);
            pen2.EndCap = LineCap.Round;
            pen2.StartCap = LineCap.Round;
            graphics_0.DrawArc(pen2, rect, num4, 70f);
            graphics_0.DrawArc(pen2, rect, num5, 70f);
            graphics_0.DrawArc(pen2, rect, num6, 70f);
            graphics_0.DrawArc(pen2, rect, num7, 70f);
            pen2.Dispose();
            pen.Dispose();
        }

        private void method_212(SpriteBatch spriteBatch_2, float float_0, float float_1, float float_2, float float_3)
        {
            method_213();
            float num = float_2 - float_0;
            float num2 = float_3 - float_1;
            XnaDrawingHelper.DrawCircle(spriteBatch_2, float_0, float_1, num, num2, color_5, 5, 50);
        }

        private void method_213()
        {
            double num = 0.0;
            int second = DateTime.Now.ToUniversalTime().Second;
            int num2 = DateTime.Now.ToUniversalTime().Millisecond;
            if (second % 2 == 1)
            {
                num2 += 1000;
            }
            num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
            byte alpha = (byte)(color_7.A - (byte)((double)(color_7.A - color_8.A) * num));
            byte red = (byte)(color_7.R - (byte)((double)(color_7.R - color_8.R) * num));
            byte green = (byte)(color_7.G - (byte)((double)(color_7.G - color_8.G) * num));
            byte blue = (byte)(color_7.B - (byte)((double)(color_7.B - color_8.B) * num));
            color_5 = System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        private System.Drawing.Color method_214(System.Drawing.Color color_18, System.Drawing.Color color_19, DateTime dateTime_5)
        {
            double num = 0.0;
            int second = dateTime_5.Second;
            int num2 = dateTime_5.Millisecond;
            if (second % 2 == 1)
            {
                num2 += 1000;
            }
            num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
            byte alpha = (byte)(color_18.A - (byte)((double)(color_18.A - color_19.A) * num));
            byte red = (byte)(color_18.R - (byte)((double)(color_18.R - color_19.R) * num));
            byte green = (byte)(color_18.G - (byte)((double)(color_18.G - color_19.G) * num));
            byte blue = (byte)(color_18.B - (byte)((double)(color_18.B - color_19.B) * num));
            return System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        private System.Drawing.Color method_215(System.Drawing.Color color_18, System.Drawing.Color color_19, DateTime dateTime_5)
        {
            double num = 0.0;
            int millisecond = dateTime_5.Millisecond;
            num = ((millisecond <= 500) ? ((double)Math.Abs(500 - millisecond) / 500.0) : ((double)(millisecond - 500) / 500.0));
            byte alpha = (byte)(color_18.A - (byte)((double)(color_18.A - color_19.A) * num));
            byte red = (byte)(color_18.R - (byte)((double)(color_18.R - color_19.R) * num));
            byte green = (byte)(color_18.G - (byte)((double)(color_18.G - color_19.G) * num));
            byte blue = (byte)(color_18.B - (byte)((double)(color_18.B - color_19.B) * num));
            return System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        internal Bitmap method_216(Bitmap bitmap_7, float float_0)
        {
            double num = (double)float_0 * -1.0;
            double num2 = Math.Cos(num);
            double num3 = Math.Sin(num);
            int num4 = bitmap_7.Height;
            int num5 = 0;
            int num6 = bitmap_7.Width;
            Bitmap bitmap = new Bitmap(num6, num4);
            DistantWorlds.FastBitmap fastBitmap = new DistantWorlds.FastBitmap(bitmap_7);
            DistantWorlds.FastBitmap fastBitmap2 = new DistantWorlds.FastBitmap(bitmap);
            double num7 = 0.0;
            double num8 = 0.0;
            double num9 = bitmap_7.Width / 2;
            double num10 = bitmap_7.Height / 2;
            double num11 = bitmap_7.Width / 2;
            double num12 = bitmap_7.Height / 2;
            for (int i = 0; i < num4; i++)
            {
                num7 = ((double)num5 - num11) * num2 + ((double)i - num12) * num3 + num9;
                num8 = ((double)i - num12) * num2 - ((double)num5 - num11) * num3 + num10;
                for (int j = num5; j < num6; j++)
                {
                    int X = (int)num7;
                    int Y = (int)num8;
                    if (X >= 0 && X < num6 && Y >= 0 && Y < num4)
                    {
                        fastBitmap2.SetPixel(ref j, ref i, fastBitmap.GetPixel(ref X, ref Y));
                    }
                    num7 += num2;
                    num8 -= num3;
                }
            }
            fastBitmap2.Release();
            fastBitmap.Release();
            return bitmap;
        }

        internal Bitmap method_217(Bitmap bitmap_7, float float_0)
        {
            return method_218(bitmap_7, float_0, GraphicsQuality.Medium);
        }

        internal Bitmap method_218(Bitmap bitmap_7, float float_0, GraphicsQuality graphicsQuality_0)
        {
            if (bitmap_7 == null)
            {
                throw new ArgumentNullException("image");
            }
            float num = bitmap_7.Width;
            float num2 = bitmap_7.Height;
            float_0 *= -1f;
            float_0 *= 57.29578f;
            float_0 %= 360f;
            if ((double)float_0 < 0.0)
            {
                float_0 += 360f;
            }
            PointF[] array = new PointF[4];
            array[1].X = num;
            array[2].X = num;
            array[2].Y = num2;
            array[3].Y = num2;
            Bitmap bitmap = null;
            using System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            matrix.Rotate(float_0);
            matrix.TransformPoints(array);
            double num3 = double.MinValue;
            double num4 = double.MinValue;
            double num5 = double.MaxValue;
            double num6 = double.MaxValue;
            PointF[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                PointF pointF = array2[i];
                num3 = Math.Max(num3, pointF.X);
                num5 = Math.Min(num5, pointF.X);
                num4 = Math.Max(num4, pointF.Y);
                num6 = Math.Min(num6, pointF.Y);
            }
            double num7 = Math.Ceiling(num3 - num5);
            double num8 = Math.Ceiling(num4 - num6);
            if (double.IsNaN(num7))
            {
                num7 = ((!float.IsNaN(num)) ? Math.Max(1.0, bitmap_7.Width) : 50.0);
            }
            if (double.IsNaN(num8))
            {
                num8 = ((!float.IsNaN(num2)) ? Math.Max(1.0, bitmap_7.Height) : 50.0);
            }
            num7 = Math.Max(1.0, num7);
            num8 = Math.Max(1.0, num8);
            bitmap = new Bitmap((int)num7, (int)num8, PixelFormat.Format32bppPArgb);
            bitmap.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
            using Graphics graphics = Graphics.FromImage(bitmap);
            main_0.method_112(graphics, graphicsQuality_0);
            PointF point = new PointF((float)(num7 / 2.0), (float)(num8 / 2.0));
            PointF point2 = new PointF(point.X - num / 2f, point.Y - num2 / 2f);
            matrix.Reset();
            matrix.RotateAt(float_0, point);
            graphics.Transform = matrix;
            graphics.DrawImage(bitmap_7, point2);
            return bitmap;
        }

        internal ImageAttributes method_219(System.Drawing.Color color_18)
        {
            float num = (float)(int)color_18.R / 255f;
            float num2 = (float)(int)color_18.G / 255f;
            float num3 = (float)(int)color_18.B / 255f;
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { num, num2, num3, 0f, 0f },
            new float[5] { num, num2, num3, 0f, 0f },
            new float[5] { num, num2, num3, 0f, 0f },
            new float[5] { 0f, 0f, 0f, 1f, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        internal ImageAttributes method_220(System.Drawing.Color color_18)
        {
            float num = (float)(int)color_18.R / 255f;
            float num2 = (float)(int)color_18.G / 255f;
            float num3 = (float)(int)color_18.B / 255f;
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { num, 0f, 0f, 0f, 0f },
            new float[5] { 0f, num2, 0f, 0f, 0f },
            new float[5] { 0f, 0f, num3, 0f, 0f },
            new float[5] { 0f, 0f, 0f, 1f, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        internal ImageAttributes method_221(System.Drawing.Color color_18)
        {
            float num = (float)(int)color_18.R / 255f;
            float num2 = (float)(int)color_18.G / 255f;
            float num3 = (float)(int)color_18.B / 255f;
            float[][] array = new float[5][];
            float[] array2 = (array[0] = new float[5]);
            float[] array3 = (array[1] = new float[5]);
            float[] array4 = (array[2] = new float[5]);
            array[3] = new float[5] { 0f, 0f, 0f, 1f, 0f };
            array[4] = new float[5] { num, num2, num3, 0f, 1f };
            float[][] newColorMatrix = array;
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        internal Bitmap method_222(Bitmap bitmap_7, System.Drawing.Color color_18)
        {
            ImageAttributes imageAttributes = null;
            imageAttributes = method_219(color_18);
            Bitmap bitmap = new Bitmap(bitmap_7);
            using Graphics graphics = Graphics.FromImage(bitmap);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
            graphics.DrawImage(bitmap_7, destRect, 0, 0, bitmap_7.Width, bitmap_7.Height, GraphicsUnit.Pixel, imageAttributes);
            return bitmap;
        }

        internal Bitmap method_223(Bitmap bitmap_7, System.Drawing.Color color_18)
        {
            return method_224(bitmap_7, color_18, bool_13: false);
        }

        internal Bitmap method_224(Bitmap bitmap_7, System.Drawing.Color color_18, bool bool_13)
        {
            ImageAttributes imageAttributes = null;
            imageAttributes = ((!bool_13) ? method_220(color_18) : method_221(color_18));
            Bitmap bitmap = new Bitmap(bitmap_7);
            bitmap.SetResolution(bitmap_7.HorizontalResolution, bitmap_7.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (main_0.zEsBtZgRxo == ZoomStatus.Zooming || main_0.zEsBtZgRxo == ZoomStatus.Stabilizing)
                {
                    method_175(graphics);
                }
                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
                graphics.DrawImage(bitmap_7, destRect, 0, 0, bitmap_7.Width, bitmap_7.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            imageAttributes?.Dispose();
            return bitmap;
        }

        private void method_225(int int_11, int int_12, double double_15, double double_16, ref int int_13, ref int int_14, ref int int_15, ref int int_16)
        {
            int_13 = (int)((double)int_11 - double_15 / 2.0);
            int_14 = (int)((double)int_11 + double_15 / 2.0);
            int_15 = (int)((double)int_12 - double_16 / 2.0);
            int_16 = (int)((double)int_12 + double_16 / 2.0);
        }

        private System.Drawing.Color method_226(System.Drawing.Color color_18, int int_11)
        {
            int red = Math.Max(0, Math.Min(255, color_18.R + int_11));
            int green = Math.Max(0, Math.Min(255, color_18.G + int_11));
            int blue = Math.Max(0, Math.Min(255, color_18.B + int_11));
            return System.Drawing.Color.FromArgb(color_18.A, red, green, blue);
        }

        private void method_227(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14)
        {
            DateTime now = DateTime.Now;
            double_12 += now.Subtract(dateTime_4).TotalSeconds;
            if (double_12 > double_13)
            {
                if (double_12 > double_13 * 2.0)
                {
                    double_12 = 0.0;
                }
                double_12 -= double_13;
            }
            for (int i = 0; i < galaxy_0.PlayerEmpire.LocationHints.Count; i++)
            {
                System.Drawing.Point point = galaxy_0.PlayerEmpire.LocationHints[i];
                method_228(graphics_0, main_0.double_0, point.X, point.Y, main_0.int_13, main_0.int_14, int_11, int_12, int_13, int_14);
            }
            dateTime_4 = now;
        }

        private void method_228(Graphics graphics_0, double double_15, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18)
        {
            int num = (int)((double)(int_11 - int_15) / double_15);
            int num2 = (int)((double)(int_12 - int_17) / double_15);
            if (num >= -40 && num <= base.ClientRectangle.Width + 40 && num2 >= -40 && num2 <= base.ClientRectangle.Height + 40)
            {
                double num3 = double_13 * 0.47;
                double num4 = Math.Min(1.0, Math.Max(0.0, (double_12 - num3) / num3));
                int alpha = 255 - (int)(num4 * 250.0);
                System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, color_17);
                int num5 = 1 + (int)(double_12 / double_13 * (double)int_10);
                using Pen pen = new Pen(color, 1.5f);
                pen.DashStyle = DashStyle.Dot;
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
                graphics_0.DrawEllipse(pen, rect);
            }
        }

        private void method_229(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14)
        {
            DateTime now = DateTime.Now;
            double_12 += now.Subtract(dateTime_4).TotalSeconds;
            if (double_12 > double_13)
            {
                if (double_12 > double_13 * 2.0)
                {
                    double_12 = 0.0;
                }
                double_12 -= double_13;
            }
            for (int i = 0; i < galaxy_0.PlayerEmpire.LocationHints.Count; i++)
            {
                System.Drawing.Point point = galaxy_0.PlayerEmpire.LocationHints[i];
                method_230(spriteBatch_2, main_0.double_0, point.X, point.Y, main_0.int_13, main_0.int_14, int_11, int_12, int_13, int_14);
            }
            dateTime_4 = now;
        }

        private void method_230(SpriteBatch spriteBatch_2, double double_15, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18)
        {
            int num = (int)((double)(int_11 - int_15) / double_15);
            int num2 = (int)((double)(int_12 - int_17) / double_15);
            if (num >= -40 && num <= base.ClientRectangle.Width + 40 && num2 >= -40 && num2 <= base.ClientRectangle.Height + 40)
            {
                double num3 = double_13 * 0.47;
                double num4 = Math.Min(1.0, Math.Max(0.0, (double_12 - num3) / num3));
                int alpha = 255 - (int)(num4 * 250.0);
                System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, color_17);
                int num5 = 1 + (int)(double_12 / double_13 * (double)int_10);
                System.Drawing.Rectangle area = new System.Drawing.Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
                XnaDrawingHelper.DrawCircle(spriteBatch_2, area, color, 1, dashed: true);
            }
        }

        private void method_231(DateTime dateTime_5, Graphics graphics_0, double double_15, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18)
        {
            double_11 = 1.6;
            int num = (int)((double)(int_11 - int_15) / double_15);
            int num2 = (int)((double)(int_12 - int_17) / double_15);
            if (num < -40 || num > base.ClientRectangle.Width + 40 || num2 < -40 || num2 > base.ClientRectangle.Height + 40)
            {
                return;
            }
            double_10 += dateTime_5.Subtract(dateTime_3).TotalSeconds;
            if (double_10 > double_11)
            {
                if (double_10 > double_11 * 2.0)
                {
                    double_10 = 0.0;
                }
                double_10 -= double_11;
            }
            double num3 = double_11 * 0.5;
            double num4 = Math.Min(1.0, Math.Max(0.0, (double_10 - num3) / num3));
            int alpha = 255 - (int)(num4 * 250.0);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, color_16);
            int num5 = 1 + (int)(double_10 / double_11 * (double)int_9);
            using Pen pen = new Pen(color, 3f);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
            graphics_0.DrawEllipse(pen, rect);
        }

        private void method_232(SpriteBatch spriteBatch_2, DateTime dateTime_5, double double_15, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18)
        {
            double_11 = 1.6;
            int num = (int)((double)(int_11 - int_15) / double_15);
            int num2 = (int)((double)(int_12 - int_17) / double_15);
            if (num < -40 || num > base.ClientRectangle.Width + 40 || num2 < -40 || num2 > base.ClientRectangle.Height + 40)
            {
                return;
            }
            double_10 += dateTime_5.Subtract(dateTime_3).TotalSeconds;
            if (double_10 > double_11)
            {
                if (double_10 > double_11 * 2.0)
                {
                    double_10 = 0.0;
                }
                double_10 -= double_11;
            }
            double num3 = double_11 * 0.5;
            double num4 = Math.Min(1.0, Math.Max(0.0, (double_10 - num3) / num3));
            int alpha = 255 - (int)(num4 * 250.0);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, color_16);
            int num5 = 1 + (int)(double_10 / double_11 * (double)int_9);
            System.Drawing.Rectangle area = new System.Drawing.Rectangle(num - num5, num2 - num5, num5 * 2, num5 * 2);
            XnaDrawingHelper.DrawCircle(spriteBatch_2, area, color, 3);
        }

        private System.Drawing.Color method_233(System.Drawing.Color color_18, float float_0)
        {
            int red = Math.Max(0, Math.Min(255, (int)((float)(int)color_18.R * float_0)));
            int green = Math.Max(0, Math.Min(255, (int)((float)(int)color_18.G * float_0)));
            int blue = Math.Max(0, Math.Min(255, (int)((float)(int)color_18.B * float_0)));
            Math.Max(0, Math.Min(255, (int)((float)(int)color_18.A * float_0)));
            return System.Drawing.Color.FromArgb(color_18.A, red, green, blue);
        }

        private void method_234(SpriteBatch spriteBatch_2, Galaxy galaxy_1, long long_1, Habitat habitat_1, System.Drawing.Color color_18, System.Drawing.Rectangle rectangle_5, double double_15, double double_16)
        {
            color_18 = method_233(color_18, 1.3f);
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(rectangle_5.Location, rectangle_5.Size);
            destination.Inflate((int)((double)destination.Width * (double_15 / 2.0)), (int)((double)destination.Height * (double_15 / 2.0)));
            long num = 700L;
            _ = (double)habitat_1.HabitatIndex / ((double)galaxy_1.Habitats.Count / 700.0);
            int num2 = (int)((long_1 - habitat_1.HabitatIndex) % 700L);
            if (num2 < 0)
            {
                num2 += (int)num;
            }
            double num3 = Math.Min(1.0, Math.Max(0.0, (double)num2 / (double)num));
            int num4 = (int)((long_1 - habitat_1.HabitatIndex) % (num * texture2D_19.Length) / num);
            int num5 = num4 + 1;
            if (num5 >= texture2D_19.Length)
            {
                num5 -= texture2D_19.Length;
            }
            Texture2D texture = texture2D_19[num4];
            Texture2D texture2 = texture2D_19[num5];
            if (num3 >= 0.0 && num3 < 0.2)
            {
                color_18 = System.Drawing.Color.FromArgb(Math.Max(0, Math.Min(255, (int)(255.0 * double_16))), color_18);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, destination, 0f, color_18);
            }
            else if (num3 >= 0.2 && num3 < 0.8)
            {
                double num6 = (0.8 - num3) * 1.666;
                num6 += 0.2;
                num6 = Math.Min(1.0, num6);
                num6 *= double_16;
                double num7 = (num3 - 0.2) * 1.666;
                num7 += 0.2;
                num7 = Math.Min(1.0, num7);
                num7 *= double_16;
                if (num3 > 0.5)
                {
                    System.Drawing.Color tintColor = System.Drawing.Color.FromArgb((int)(num7 * 255.0), color_18);
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2, destination, 0f, tintColor);
                    tintColor = System.Drawing.Color.FromArgb((int)(num6 * 255.0), color_18);
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, destination, 0f, tintColor);
                }
                else
                {
                    System.Drawing.Color tintColor2 = System.Drawing.Color.FromArgb((int)(num6 * 255.0), color_18);
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, destination, 0f, tintColor2);
                    tintColor2 = System.Drawing.Color.FromArgb((int)(num7 * 255.0), color_18);
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2, destination, 0f, tintColor2);
                }
            }
            else
            {
                color_18 = System.Drawing.Color.FromArgb(Math.Max(0, Math.Min(255, (int)(255.0 * double_16))), color_18);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2, destination, 0f, color_18);
            }
        }

        private void method_235(Graphics graphics_0, Galaxy galaxy_1, Habitat habitat_1, System.Drawing.Color color_18, System.Drawing.Rectangle rectangle_5, double double_15, bool bool_13, bool bool_14)
        {
            Bitmap[] array = main_0.bitmap_197;
            if (bool_14)
            {
                array = main_0.bitmap_198;
            }
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(rectangle_5.Location, rectangle_5.Size);
            destRect.Inflate((int)((double)destRect.Width * (double_15 / 2.0)), (int)((double)destRect.Height * (double_15 / 2.0)));
            long num = 700L;
            _ = (double)habitat_1.HabitatIndex / ((double)galaxy_1.Habitats.Count / 700.0);
            int num2 = (int)((galaxy_1.CurrentStarDate - habitat_1.HabitatIndex) % 700L);
            if (num2 < 0)
            {
                num2 += (int)num;
            }
            double num3 = Math.Min(1.0, Math.Max(0.0, (double)num2 / (double)num));
            int num4 = (int)((galaxy_1.CurrentStarDate - habitat_1.HabitatIndex) % (num * array.Length) / num);
            int num5 = num4 + 1;
            if (num5 >= array.Length)
            {
                num5 -= array.Length;
            }
            Bitmap bitmap = array[num4];
            Bitmap bitmap2 = array[num5];
            method_176(graphics_0);
            if (!bool_13)
            {
                using ImageAttributes imageAttr = method_237(color_18, 1.0);
                graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr);
            }
            else if (num3 >= 0.0 && num3 < 0.2)
            {
                using ImageAttributes imageAttr2 = method_237(color_18, 1.0);
                graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr2);
            }
            else if (num3 >= 0.2 && num3 < 0.8)
            {
                double num6 = (0.8 - num3) * 1.666;
                num6 += 0.2;
                num6 = Math.Min(1.0, num6);
                double num7 = (num3 - 0.2) * 1.666;
                num7 += 0.2;
                num7 = Math.Min(1.0, num7);
                if (num3 > 0.5)
                {
                    using (ImageAttributes imageAttr3 = method_237(color_18, num7))
                    {
                        graphics_0.DrawImage(bitmap2, destRect, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttr3);
                    }
                    using ImageAttributes imageAttr4 = method_237(color_18, num6);
                    graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr4);
                }
                else
                {
                    using (ImageAttributes imageAttr5 = method_237(color_18, num6))
                    {
                        graphics_0.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr5);
                    }
                    using ImageAttributes imageAttr6 = method_237(color_18, num7);
                    graphics_0.DrawImage(bitmap2, destRect, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttr6);
                }
            }
            else
            {
                using ImageAttributes imageAttr7 = method_237(color_18, 1.0);
                graphics_0.DrawImage(bitmap2, destRect, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttr7);
            }
            method_177(graphics_0);
        }

        private ImageAttributes method_236(double double_15)
        {
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5]
            {
                0f,
                0f,
                0f,
                (float)double_15,
                0f
            },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        private ImageAttributes method_237(System.Drawing.Color color_18, double double_15)
        {
            float num = (float)(int)color_18.R / 255f;
            float num2 = (float)(int)color_18.G / 255f;
            float num3 = (float)(int)color_18.B / 255f;
            float[][] array = new float[5][];
            float[] array2 = (array[0] = new float[5]);
            float[] array3 = (array[1] = new float[5]);
            float[] array4 = (array[2] = new float[5]);
            array[3] = new float[5]
            {
            0f,
            0f,
            0f,
            (float)double_15,
            0f
            };
            array[4] = new float[5] { num, num2, num3, 0f, 1f };
            float[][] newColorMatrix = array;
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        private void method_238(Graphics graphics_0, Pen pen_11, PointF[] pointF_0, float float_0)
        {
            if (float_0 < 2f)
            {
                if (pointF_0 != null && pointF_0.Length > 0)
                {
                    graphics_0.DrawRectangle(pen_11, new System.Drawing.Rectangle((int)pointF_0[0].X, (int)pointF_0[0].Y, 2, 2));
                }
            }
            else
            {
                graphics_0.DrawPolygon(pen_11, pointF_0);
            }
        }

        private void method_239(Graphics graphics_0, SolidBrush solidBrush_21, PointF[] pointF_0, float float_0)
        {
            if (float_0 < 2f)
            {
                if (pointF_0 != null && pointF_0.Length > 0)
                {
                    graphics_0.FillRectangle(solidBrush_21, new System.Drawing.Rectangle((int)pointF_0[0].X, (int)pointF_0[0].Y, 2, 2));
                }
            }
            else
            {
                graphics_0.FillPolygon(solidBrush_21, pointF_0);
            }
        }

        private void method_240(Graphics graphics_0, Pen pen_11, System.Drawing.Rectangle rectangle_5)
        {
            if (rectangle_5.Width >= 2 && rectangle_5.Height >= 2)
            {
                graphics_0.DrawEllipse(pen_11, rectangle_5);
            }
            else
            {
                graphics_0.DrawRectangle(pen_11, rectangle_5);
            }
        }

        private void method_241(Graphics graphics_0, Pen pen_11, RectangleF rectangleF_1)
        {
            if (!(rectangleF_1.Width < 2f) && rectangleF_1.Height >= 2f)
            {
                graphics_0.DrawEllipse(pen_11, rectangleF_1);
            }
            else
            {
                graphics_0.DrawRectangle(pen_11, rectangleF_1.X, rectangleF_1.Y, rectangleF_1.Width, rectangleF_1.Height);
            }
        }

        private void method_242(Graphics graphics_0, Brush brush_0, System.Drawing.Rectangle rectangle_5)
        {
            if (rectangle_5.Width >= 2 && rectangle_5.Height >= 2)
            {
                graphics_0.FillEllipse(brush_0, rectangle_5);
            }
            else
            {
                graphics_0.FillRectangle(brush_0, rectangle_5);
            }
        }

        private void method_243(Graphics graphics_0, Brush brush_0, RectangleF rectangleF_1)
        {
            if (!(rectangleF_1.Width < 2f) && rectangleF_1.Height >= 2f)
            {
                graphics_0.FillEllipse(brush_0, rectangleF_1);
            }
            else
            {
                graphics_0.FillRectangle(brush_0, rectangleF_1.X, rectangleF_1.Y, rectangleF_1.Width, rectangleF_1.Height);
            }
        }

        private void method_244(int int_11, int int_12, int int_13, int int_14, double double_15)
        {
            double num = (double)int_13 * double_15;
            double num2 = (double)int_14 * double_15;
            int int_15 = 0;
            int int_16 = 0;
            int int_17 = 0;
            int int_18 = 0;
            method_225(int_11, int_12, num, num2, ref int_15, ref int_16, ref int_17, ref int_18);
            int val = (int)(num * 0.55);
            int val2 = (int)(num2 * 0.55);
            int val3 = (int)(num * 0.9 * (double_15 / 15000.0));
            int val4 = (int)(num2 * 0.9 * (double_15 / 15000.0));
            val3 = Math.Max(val, val3);
            val4 = Math.Max(val2, val4);
            if (int_15 < val3 * -1)
            {
                int_11 = (int)num / 2 - val3;
                main_0.int_13 = int_11;
            }
            if (int_16 > Galaxy.SizeX + val3)
            {
                int_11 = Galaxy.SizeX + val3 - (int)num / 2;
                main_0.int_13 = int_11;
            }
            if (int_17 < val4 * -1)
            {
                int_12 = (int)num2 / 2 - val4;
                main_0.int_14 = int_12;
            }
            if (int_18 > Galaxy.SizeY + val4)
            {
                int_12 = Galaxy.SizeY + val4 - (int)num2 / 2;
                main_0.int_14 = int_12;
            }
        }

        private void method_245(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, double double_15)
        {
            if (main_0 == null || main_0.list_6 == null || main_0.list_6.Count <= 0)
            {
                return;
            }
            using Pen pen = new Pen(System.Drawing.Color.White, 2f);
            for (int i = 0; i < main_0.list_6.Count; i++)
            {
                object obj = main_0.list_6[i];
                double num = 0.0;
                double num2 = 0.0;
                if (obj is Habitat)
                {
                    Habitat habitat = (Habitat)obj;
                    num = habitat.Xpos;
                    num2 = habitat.Ypos;
                }
                if (num >= (double)int_11 && num <= (double)int_12 && num2 >= (double)int_13 && num2 <= (double)int_14)
                {
                    int num3 = (int)((num - (double)int_11) / double_15);
                    int num4 = (int)((num2 - (double)int_13) / double_15);
                    graphics_0.DrawEllipse(pen, new System.Drawing.Rectangle(num3 - 4, num4 - 4, 9, 9));
                }
            }
        }

        private void method_246(Graphics graphics_0, Empire empire_0, double double_15, int int_11, int int_12, int int_13, int int_14)
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(128, 255, 0, 64);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(128, 64, 0, 255);
            using HatchBrush brush = new HatchBrush(HatchStyle.Percent20, color, System.Drawing.Color.Transparent);
            using Pen pen = new Pen(color);
            pen.Width = 1f;
            using HatchBrush brush2 = new HatchBrush(HatchStyle.Percent20, color2, System.Drawing.Color.Transparent);
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
                        num = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_15);
                    }
                    float num2 = (float)((shipGroup.AttackPoint.Xpos - (double)int_13) / double_15);
                    float num3 = (float)((shipGroup.AttackPoint.Ypos - (double)int_14) / double_15);
                    if (num > 0f && num2 + num > 0f && num2 - num < (float)int_11 && num3 + num > 0f && num3 - num < (float)int_12)
                    {
                        RectangleF rect = new RectangleF(num2 - num, num3 - num, num * 2f, num * 2f);
                        graphics_0.FillEllipse(brush, rect);
                        graphics_0.DrawEllipse(pen, rect);
                    }
                    if (shipGroup.GatherPoint != null)
                    {
                        float x = (float)((shipGroup.GatherPoint.Xpos - (double)int_13) / double_15);
                        float y = (float)((shipGroup.GatherPoint.Ypos - (double)int_14) / double_15);
                        using Pen pen3 = new Pen(color);
                        pen3.Width = 3f;
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
                        num4 = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_15);
                    }
                    float num5 = (float)((shipGroup.GatherPoint.Xpos - (double)int_13) / double_15);
                    float num6 = (float)((shipGroup.GatherPoint.Ypos - (double)int_14) / double_15);
                    if (num4 > 0f && num5 + num4 > 0f && num5 - num4 < (float)int_11 && num6 + num4 > 0f && num6 - num4 < (float)int_12)
                    {
                        RectangleF rect2 = new RectangleF(num5 - num4, num6 - num4, num4 * 2f, num4 * 2f);
                        graphics_0.FillEllipse(brush2, rect2);
                        graphics_0.DrawEllipse(pen2, rect2);
                    }
                }
            }
        }

        private void method_247(SpriteBatch spriteBatch_2, Empire empire_0, double double_15, int int_11, int int_12, int int_13, int int_14)
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(96, 255, 0, 64);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(96, 64, 0, 255);
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
                        num = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_15);
                    }
                    float num2 = (float)((shipGroup.AttackPoint.Xpos - (double)int_13) / double_15);
                    float num3 = (float)((shipGroup.AttackPoint.Ypos - (double)int_14) / double_15);
                    if (num > 0f && num2 + num > 0f && num2 - num < (float)int_11 && num3 + num > 0f && num3 - num < (float)int_12)
                    {
                        RectangleF rectangleF = new RectangleF(num2 - num, num3 - num, num * 2f, num * 2f);
                        System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_17, rectangle, 0f, color);
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, rectangle, 100, color, 1);
                    }
                    if (shipGroup.GatherPoint != null)
                    {
                        float x = (float)((shipGroup.GatherPoint.Xpos - (double)int_13) / double_15);
                        float y = (float)((shipGroup.GatherPoint.Ypos - (double)int_14) / double_15);
                        using (new Pen(color))
                        {
                            XnaDrawingHelper.DrawLine(spriteBatch_2, x, y, num2, num3, color, 3, dashed: true, texture2D_35);
                        }
                    }
                }
                else if (shipGroup.Posture == FleetPosture.Defend && shipGroup.GatherPoint != null)
                {
                    float num4 = 0f;
                    if (shipGroup.PostureRangeSquared > 2250000.0 && shipGroup.PostureRangeSquared < 3.4028234663852886E+38)
                    {
                        num4 = (float)(Math.Sqrt(shipGroup.PostureRangeSquared) / double_15);
                    }
                    float num5 = (float)((shipGroup.GatherPoint.Xpos - (double)int_13) / double_15);
                    float num6 = (float)((shipGroup.GatherPoint.Ypos - (double)int_14) / double_15);
                    if (num4 > 0f && num5 + num4 > 0f && num5 - num4 < (float)int_11 && num6 + num4 > 0f && num6 - num4 < (float)int_12)
                    {
                        RectangleF rectangleF2 = new RectangleF(num5 - num4, num6 - num4, num4 * 2f, num4 * 2f);
                        System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle((int)rectangleF2.X, (int)rectangleF2.Y, (int)rectangleF2.Width, (int)rectangleF2.Height);
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_17, rectangle2, 0f, color2);
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, rectangle2, 100, color2, 1);
                    }
                }
            }
        }

        private void method_248(int int_11, int int_12, int int_13, int int_14, double double_15, Graphics graphics_0, int int_15)
        {
            int_15 = 255;
            Empire empire = main_0._Game.PlayerEmpire;
            bool flag = false;
            if (main_0.empire_1 != null)
            {
                empire = main_0.empire_1;
                flag = true;
            }
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            bool flag5 = true;
            bool flag6 = true;
            bool flag7 = true;
            bool flag8 = true;
            bool flag9 = true;
            bool flag10 = true;
            bool flag11 = true;
            bool flag12 = true;
            bool flag13 = true;
            if (main_0.gameOptions_0 != null && !main_0._Game.GodMode && double_15 > 3500.0)
            {
                flag2 = main_0.gameOptions_0.GalaxyViewDisplayFleets;
                flag3 = main_0.gameOptions_0.GalaxyViewDisplayResupplyShips;
                flag4 = main_0.gameOptions_0.GalaxyViewDisplayMilitaryShips;
                flag5 = main_0.gameOptions_0.GalaxyViewDisplaySpacePorts;
                flag6 = main_0.gameOptions_0.GalaxyViewDisplayOtherBases;
                flag7 = main_0.gameOptions_0.GalaxyViewDisplayExplorationShips;
                flag8 = main_0.gameOptions_0.GalaxyViewDisplayColonyShips;
                flag9 = main_0.gameOptions_0.GalaxyViewDisplayConstructionShips;
                flag10 = main_0.gameOptions_0.GalaxyViewDisplayCivilianShips;
                flag11 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets;
                flag12 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips;
                flag13 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysPirates;
            }
            bool flag14 = false;
            bool flag15 = false;
            bool flag16 = false;
            bool flag17 = false;
            bool flag18 = false;
            bool flag19 = false;
            bool flag20 = false;
            if (main_0.gameOptions_0 != null)
            {
                flag14 = main_0.gameOptions_0.MapOverlayFleetPostures;
                flag15 = main_0.gameOptions_0.MapOverlayTravelVectorsState;
                flag16 = main_0.gameOptions_0.MapOverlayTravelVectorsPrivate;
                flag17 = main_0.gameOptions_0.MapOverlayPotentialColonies;
                flag18 = main_0.gameOptions_0.MapOverlayScenicLocations;
                flag19 = main_0.gameOptions_0.MapOverlayResearchLocations;
                flag20 = main_0.gameOptions_0.MapOverlayLongRangeScanners;
            }
            Pen pen = new Pen(System.Drawing.Color.FromArgb(int_15, 112, 112, 112), 1.5f);
            pen.DashStyle = DashStyle.Dash;
            int num = (int)((double)Galaxy.SizeX / double_15);
            double double_16 = (double)int_13 * double_15;
            double double_17 = (double)int_14 * double_15;
            int int_16 = 0;
            int int_17 = 0;
            int int_18 = 0;
            int int_19 = 0;
            method_225(int_11, int_12, double_16, double_17, ref int_16, ref int_17, ref int_18, ref int_19);
            double num2 = (double)num / (double)Galaxy.SectorMaxX;
            SizeF sizeF = graphics_0.MeasureString("H", font_3, 200, StringFormat.GenericTypographic);
            float num3 = (float)(num2 / 2.0 - (double)sizeF.Width / 2.0);
            float num4 = (float)(num2 / 2.0 - (double)sizeF.Height / 2.0);
            SolidBrush solidBrush = new SolidBrush(main_0.color_7);
            int num5 = galaxy_0.ResolveSector(int_16, int_12).X;
            int num6 = galaxy_0.ResolveSector(int_17, int_12).X;
            int num7 = galaxy_0.ResolveSector(int_11, int_18).Y;
            int num8 = galaxy_0.ResolveSector(int_11, int_19).Y;
            int num9 = galaxy_0.ResolveIndex(int_16, int_12).X;
            int num10 = galaxy_0.ResolveIndex(int_17, int_12).X;
            int num11 = galaxy_0.ResolveIndex(int_11, int_18).Y;
            int num12 = galaxy_0.ResolveIndex(int_11, int_19).Y;
            double num13 = 150.0;
            if (double_15 > num13)
            {
                graphics_0.SmoothingMode = SmoothingMode.None;
                graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
                bool flag21 = false;
                bool flag22 = false;
                float num14 = 0f;
                float num15 = int_14;
                if (num7 <= 1)
                {
                    num14 = (float)((double)(num7 * Galaxy.SectorSize - int_18) / double_15);
                }
                if (num8 >= Galaxy.SectorMaxX - 1)
                {
                    num15 = (float)((double)((num8 + 1) * Galaxy.SectorSize - int_18) / double_15);
                    flag22 = true;
                }
                float num16 = 0f;
                float num17 = int_13;
                if (num5 <= 1)
                {
                    num16 = (float)((double)(num5 * Galaxy.SectorSize - int_16) / double_15);
                }
                if (num6 >= Galaxy.SectorMaxY - 1)
                {
                    num17 = (float)((double)((num6 + 1) * Galaxy.SectorSize - int_16) / double_15);
                    flag21 = true;
                }
                for (int i = num5; i <= num6; i++)
                {
                    float num18 = (float)((double)(i * Galaxy.SectorSize - int_16) / double_15);
                    graphics_0.DrawLine(main_0.pen_1, num18, num14, num18, num15);
                    string s = ((char)(i + 65)).ToString();
                    float val = num14 - 1f;
                    val = Math.Max(0f, val);
                    graphics_0.DrawString(point: new PointF(num18 + num3, val), s: s, font: font_2, brush: solidBrush);
                    val = num15 - 14f;
                    val = Math.Min((float)base.ClientRectangle.Height - 11f, val);
                    graphics_0.DrawString(point: new PointF(num18 + num3, val), s: s, font: font_2, brush: solidBrush);
                }
                if (flag21)
                {
                    float num19 = (float)((double)((num6 + 1) * Galaxy.SectorSize - int_16) / double_15);
                    graphics_0.DrawLine(main_0.pen_1, num19, num14, num19, num15);
                }
                for (int j = num7; j <= num8; j++)
                {
                    float num20 = (float)((double)(j * Galaxy.SectorSize - int_18) / double_15);
                    graphics_0.DrawLine(main_0.pen_1, num16, num20, num17, num20);
                    string s2 = (j + 1).ToString();
                    float val2 = num16 - 2f;
                    val2 = Math.Max(0f, val2);
                    graphics_0.DrawString(point: new PointF(val2, num20 + num4), s: s2, font: font_2, brush: solidBrush);
                    val2 = num17 - 14f;
                    val2 = Math.Min((float)base.ClientRectangle.Width - 8f, val2);
                    graphics_0.DrawString(point: new PointF(val2, num20 + num4), s: s2, font: font_2, brush: solidBrush);
                }
                if (flag22)
                {
                    float num21 = (float)((double)((num8 + 1) * Galaxy.SectorSize - int_18) / double_15);
                    graphics_0.DrawLine(main_0.pen_1, num16, num21, num17, num21);
                }
                graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
                graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
                if (main_0._Game.SelectedObject != null)
                {
                    BuiltObject builtObject = null;
                    double num22 = 0.0;
                    if (main_0._Game.SelectedObject is BuiltObject)
                    {
                        builtObject = (BuiltObject)main_0._Game.SelectedObject;
                        num22 = builtObject.CurrentRange();
                    }
                    else if (main_0._Game.SelectedObject is ShipGroup)
                    {
                        ShipGroup shipGroup = (ShipGroup)main_0._Game.SelectedObject;
                        builtObject = shipGroup.LeadShip;
                        num22 = shipGroup.CurrentRange();
                    }
                    if (builtObject != null && builtObject.Role != BuiltObjectRole.Base && builtObject.Empire == empire)
                    {
                        if (num22 < (double)Galaxy.SizeX)
                        {
                            double num23 = builtObject.Xpos - num22;
                            double num24 = builtObject.Ypos - num22;
                            float num25 = (float)((num23 - (double)int_16) / double_15);
                            float num26 = (float)((num24 - (double)int_18) / double_15);
                            float num27 = (float)(num22 / double_15 * 2.0);
                            using Pen pen2 = new Pen(main_0.VuqPpUtdZU, 1f);
                            pen2.DashPattern = new float[2] { 2f, 10f };
                            RectangleF rectangleF_ = new RectangleF(num25, num26, num27, num27);
                            method_241(graphics_0, pen2, rectangleF_);
                        }
                        int int_20 = (int)((builtObject.Xpos - (double)int_16) / double_15);
                        int int_21 = (int)((builtObject.Ypos - (double)int_18) / double_15);
                        method_255(graphics_0, builtObject, int_20, int_21, int_16, int_18, double_15, bool_13: true, pen_3);
                    }
                }
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(int_16, int_18, int_17 - int_16, int_19 - int_18);
                method_175(graphics_0);
                if (flag20)
                {
                    for (int k = 0; k < empire.LongRangeScanners.Count; k++)
                    {
                        BuiltObject builtObject2 = empire.LongRangeScanners[k];
                        int sensorLongRange = builtObject2.SensorLongRange;
                        if (builtObject2.Role != BuiltObjectRole.Base)
                        {
                            int num28 = (int)builtObject2.Xpos - sensorLongRange;
                            int num29 = (int)builtObject2.Xpos + sensorLongRange;
                            int num30 = (int)builtObject2.Ypos - sensorLongRange;
                            int num31 = (int)builtObject2.Ypos + sensorLongRange;
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num28, num30, sensorLongRange * 2, sensorLongRange * 2);
                            if (rectangle.IntersectsWith(rect))
                            {
                                float num32 = (float)(((double)num28 - (double)int_16) / main_0.double_0);
                                float num33 = (float)(((double)num29 - (double)int_16) / main_0.double_0);
                                float num34 = (float)(((double)num30 - (double)int_18) / main_0.double_0);
                                float num35 = (float)(((double)num31 - (double)int_18) / main_0.double_0);
                                RectangleF rectangleF = new RectangleF(num32, num34, num33 - num32, num35 - num34);
                                rectangleF.Inflate(rectangleF.Width * 0.1f, rectangleF.Width * 0.1f);
                                System.Drawing.Color white = System.Drawing.Color.White;
                                ImageAttributes imageAttr = method_237(white, 0.1);
                                graphics_0.DrawImage(main_0.bitmap_188, new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), 0, 0, main_0.bitmap_188.Width, main_0.bitmap_188.Height, GraphicsUnit.Pixel, imageAttr);
                            }
                        }
                    }
                }
            }
            method_177(graphics_0);
            if (flag14)
            {
                method_246(graphics_0, empire, double_15, int_13, int_14, int_16, int_18);
            }
            for (int l = 0; l < galaxy_0.Systems.Count; l++)
            {
                SystemInfo systemInfo = galaxy_0.Systems[l];
                if (systemInfo.Sector.X < num5 || systemInfo.Sector.X > num6 || systemInfo.Sector.Y < num7 || systemInfo.Sector.Y > num8)
                {
                    continue;
                }
                float num36 = (float)((systemInfo.SystemStar.Xpos - (double)int_16) / double_15);
                float num37 = (float)((systemInfo.SystemStar.Ypos - (double)int_18) / double_15);
                SystemVisibilityStatus systemVisibilityStatus = empire.CheckSystemVisibilityStatus(l);
                int num38 = 0;
                float val3 = 0f;
                bool flag23 = false;
                bool flag24 = false;
                if (double_15 > num13 && !flag && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode) && systemInfo.DominantEmpire != null)
                {
                    num38 = Math.Min(systemInfo.DominantEmpire.TotalStrategicValue, 1500000);
                    val3 = (float)(Math.Pow(num38, 0.35) * 600.0);
                    float num39 = 0f;
                    float num40 = 0f;
                    HabitatList linkSystemStars = systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars;
                    if (linkSystemStars.Count > 0)
                    {
                        for (int m = 0; m < linkSystemStars.Count; m++)
                        {
                            Habitat habitat = linkSystemStars[m];
                            if (habitat == null)
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus2 = empire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                            if (systemVisibilityStatus2 == SystemVisibilityStatus.Visible || systemVisibilityStatus2 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                            {
                                using Pen pen3 = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 1f);
                                num39 = (float)((habitat.Xpos - (double)int_16) / double_15);
                                num40 = (float)((habitat.Ypos - (double)int_18) / double_15);
                                pen3.DashPattern = new float[2] { 3f, 5f };
                                graphics_0.DrawLine(pen3, num36, num37, num39, num40);
                            }
                        }
                    }
                    if (systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count > 0)
                    {
                        for (int n = 0; n < systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; n++)
                        {
                            Habitat habitat2 = systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[n];
                            if (linkSystemStars.Contains(habitat2) || habitat2 == null)
                            {
                                continue;
                            }
                            SystemInfo systemInfo2 = galaxy_0.Systems[habitat2.SystemIndex];
                            if (systemInfo2.Sector.X >= num5 && systemInfo2.Sector.X <= num6 && systemInfo2.Sector.Y >= num7 && systemInfo2.Sector.Y <= num8)
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus3 = empire.CheckSystemVisibilityStatus(habitat2.SystemIndex);
                            if (systemVisibilityStatus3 == SystemVisibilityStatus.Visible || systemVisibilityStatus3 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                            {
                                using Pen pen4 = new Pen(systemInfo.DominantEmpire.Empire.MainColor, 1f);
                                num39 = (float)((habitat2.Xpos - (double)int_16) / double_15);
                                num40 = (float)((habitat2.Ypos - (double)int_18) / double_15);
                                pen4.DashPattern = new float[2] { 3f, 5f };
                                graphics_0.DrawLine(pen4, num36, num37, num39, num40);
                            }
                        }
                    }
                    EmpireSystemSummaryList otherEmpires = systemInfo.OtherEmpires;
                    if (otherEmpires != null)
                    {
                        for (int num41 = 0; num41 < otherEmpires.Count; num41++)
                        {
                            EmpireSystemSummary empireSystemSummary = otherEmpires[num41];
                            Empire empire2 = empireSystemSummary.Empire;
                            linkSystemStars = empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars;
                            if (linkSystemStars.Count > 0)
                            {
                                for (int num42 = 0; num42 < linkSystemStars.Count; num42++)
                                {
                                    Habitat habitat3 = linkSystemStars[num42];
                                    if (habitat3 == null)
                                    {
                                        continue;
                                    }
                                    SystemVisibilityStatus systemVisibilityStatus4 = empire.CheckSystemVisibilityStatus(habitat3.SystemIndex);
                                    if (systemVisibilityStatus4 == SystemVisibilityStatus.Visible || systemVisibilityStatus4 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                    {
                                        using Pen pen5 = new Pen(empire2.MainColor, 1f);
                                        num39 = (float)((habitat3.Xpos - (double)int_16) / double_15);
                                        num40 = (float)((habitat3.Ypos - (double)int_18) / double_15);
                                        pen5.DashPattern = new float[2] { 3f, 5f };
                                        graphics_0.DrawLine(pen5, num36, num37, num39, num40);
                                    }
                                }
                            }
                            if (empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count <= 0)
                            {
                                continue;
                            }
                            for (int num43 = 0; num43 < empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; num43++)
                            {
                                Habitat habitat4 = empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[num43];
                                if (linkSystemStars.Contains(habitat4) || habitat4 == null)
                                {
                                    continue;
                                }
                                SystemInfo systemInfo3 = galaxy_0.Systems[habitat4.SystemIndex];
                                if (systemInfo3.Sector.X >= num5 && systemInfo3.Sector.X <= num6 && systemInfo3.Sector.Y >= num7 && systemInfo3.Sector.Y <= num8)
                                {
                                    continue;
                                }
                                SystemVisibilityStatus systemVisibilityStatus5 = empire.CheckSystemVisibilityStatus(habitat4.SystemIndex);
                                if (systemVisibilityStatus5 == SystemVisibilityStatus.Visible || systemVisibilityStatus5 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                {
                                    using Pen pen6 = new Pen(empire2.MainColor, 1f);
                                    num39 = (float)((habitat4.Xpos - (double)int_16) / double_15);
                                    num40 = (float)((habitat4.Ypos - (double)int_18) / double_15);
                                    pen6.DashPattern = new float[2] { 3f, 5f };
                                    graphics_0.DrawLine(pen6, num36, num37, num39, num40);
                                }
                            }
                        }
                    }
                }
                val3 = Math.Max(Galaxy.MaxSolarSystemSize, val3);
                if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    val3 = systemInfo.SystemStar.NovaProgression;
                }
                val3 = (int)((double)val3 / double_15);
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    val3 = (int)((double)val3 * 0.7);
                }
                if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    val3 = (int)((double)val3 * 2.0);
                }
                if (!(num36 + val3 >= 0f) || !(num36 - val3 <= (float)int_13) || !(num37 + val3 >= 0f) || !(num37 - val3 <= (float)int_14))
                {
                    continue;
                }
                Pen pen7 = method_268(empire, flag, systemInfo, int_15);
                Brush brush = method_269(empire, flag, systemInfo, int_15);
                int val4 = (int)((double)systemInfo.SystemStar.Diameter / (double_15 / 70.0));
                val4 = Math.Min(Math.Max(8, val4), 25);
                if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    val4 = (int)((double)systemInfo.SystemStar.NovaProgression * 2.0 / double_15);
                }
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    val3 = Math.Max(6f, val3);
                }
                else if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    val3 = Math.Max(5f, val3);
                }
                else if (val3 * 2f - 4f <= (float)val4)
                {
                    val3 = val4 / 2 + 3;
                }
                if (double_15 > num13 && (systemInfo.PlayerPotentialColonies || ((systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored) && systemInfo.DominantEmpire == null && systemInfo.IndependentColonyCount > 0)))
                {
                    float num44 = (float)((double)Galaxy.MaxSolarSystemSize / double_15);
                    num44 *= 0.75f;
                    int num45 = (int)((double)val4 * 0.7);
                    if (num44 * 2f - 4f <= (float)num45)
                    {
                        num44 = num45 / 2 + 3;
                    }
                    RectangleF rectangleF_2 = new RectangleF(num36 - num44, num37 - num44, num44 * 2f, num44 * 2f);
                    method_241(graphics_0, pen, rectangleF_2);
                }
                RectangleF rectangleF_3 = new RectangleF(num36 - val3, num37 - val3, val3 * 2f, val3 * 2f);
                if (double_15 > num13)
                {
                    if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                    {
                        val3 *= 0.7f;
                        graphics_0.DrawLine(pen7, num36, num37 - val3, num36, num37 + val3);
                        graphics_0.DrawLine(pen7, num36 - val3, num37, num36 + val3, num37);
                    }
                    else if ((systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode) && (systemInfo.DominantEmpire != null || systemInfo.IndependentColonyCount > 0))
                    {
                        method_241(graphics_0, pen7, rectangleF_3);
                    }
                }
                Bitmap bitmap = null;
                bitmap = ((systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud) ? bitmap_1[0] : ((systemInfo.SystemStar.Type != HabitatType.SuperNova) ? bitmap_1[systemInfo.SystemStar.MapPictureRef] : main_0.bitmap_206[systemInfo.SystemStar.NovaImageIndexMajor]));
                double num46 = (double)bitmap.Height / (double)bitmap.Width;
                int num47 = val4;
                int num48 = (int)((double)val4 * num46);
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle((int)num36 - num47 / 2, (int)num37 - num48 / 2, num47, num48);
                double num49 = 0.6;
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    num49 = 1.0;
                }
                if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    num49 = 1.3;
                }
                else if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    num49 = 1.3;
                }
                int val5 = (int)((double)val4 * num49);
                int val6 = (int)((double)val4 * num49 * num46);
                val5 = Math.Max(8, val5);
                val6 = Math.Max(8, val6);
                System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle((int)num36 - val5 / 2, (int)num37 - val6 / 2, val5, val6);
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    rectangle_ = new System.Drawing.Rectangle((int)num36 - (int)val3, (int)num37 - (int)val3, (int)val3 * 2, (int)val3 * 2);
                }
                else
                {
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova && systemInfo.SystemStar.Type != HabitatType.BlackHole && double_15 < 150.0)
                    {
                        int val7 = (int)((double)systemInfo.SystemStar.Diameter / double_15);
                        int num50 = (int)((systemInfo.SystemStar.Xpos - (double)int_16) / double_15) - 1;
                        int num51 = (int)((systemInfo.SystemStar.Ypos - (double)int_18) / double_15) - 1;
                        val7 = Math.Max(10, val7);
                        rectangle2 = new System.Drawing.Rectangle(num50 - val7 / 2, num51 - val7 / 2, val7 + 2, val7 + 2);
                    }
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova || rectangle2.Width >= 400)
                    {
                        method_175(graphics_0);
                    }
                    if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                    {
                        if (double_15 < 5100.0)
                        {
                            val3 = Math.Max(11f, val3);
                            val3 = Math.Min(17f, val3);
                        }
                        rectangle2 = new System.Drawing.Rectangle((int)num36 - (int)val3, (int)num37 - (int)val3, (int)val3 * 2, (int)val3 * 2);
                        rectangle_ = new System.Drawing.Rectangle(rectangle2.Location, rectangle2.Size);
                        rectangleF_3 = new System.Drawing.Rectangle(rectangle2.Location, rectangle2.Size);
                    }
                    if (double_15 > method_60(systemInfo.SystemStar.Type) && (double_15 < 5100.0 || systemInfo.SystemStar.Type == HabitatType.BlackHole || systemInfo.SystemStar.Type == HabitatType.SuperNova))
                    {
                        graphics_0.DrawImage(bitmap, rectangle2);
                    }
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova && systemInfo.SystemStar.Type != HabitatType.BlackHole)
                    {
                        System.Drawing.Color color_ = method_120(bitmap);
                        double double_18 = 2.0;
                        if (double_15 < 5100.0)
                        {
                            double double_19 = 30.0;
                            double num52 = method_61(systemInfo.SystemStar.Type, out double_19);
                            if (double_15 > double_19)
                            {
                                if (double_15 < num52)
                                {
                                    double num53 = num52 - double_19;
                                    double_18 = 1.0 + (double_15 - double_19) / num53 * 1.0;
                                }
                                method_235(graphics_0, galaxy_0, systemInfo.SystemStar, color_, rectangle2, double_18, bool_13: true, bool_14: false);
                            }
                        }
                        else
                        {
                            double_18 = 1.3;
                            method_235(graphics_0, galaxy_0, systemInfo.SystemStar, color_, rectangle2, double_18, bool_13: false, bool_14: true);
                        }
                        method_177(graphics_0);
                    }
                }
                method_177(graphics_0);
                if (double_15 > num13)
                {
                    if (main_0._Game.SelectedObject == systemInfo)
                    {
                        if ((float)rectangle_.Width > rectangleF_3.Width)
                        {
                            method_210(rectangle_.X - 3, rectangle_.Y - 3, rectangle_.X + rectangle_.Width + 4, rectangle_.Y + rectangle_.Height + 4, graphics_0);
                        }
                        else
                        {
                            method_211(rectangleF_3.X - 3f, rectangleF_3.Y - 3f, rectangleF_3.X + rectangleF_3.Width + 4f, rectangleF_3.Y + rectangleF_3.Height + 4f, graphics_0);
                        }
                    }
                    bool flag25 = false;
                    bool flag26 = false;
                    Font font = font_2;
                    float num54 = 0f;
                    float num55 = 3f;
                    if (double_15 < 4000.0)
                    {
                        if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible || main_0._Game.GodMode)
                        {
                            flag25 = true;
                            if (systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                            {
                                if (systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire.Capital != null)
                                {
                                    Habitat habitat5 = Galaxy.DetermineHabitatSystemStar(systemInfo.DominantEmpire.Empire.Capital);
                                    if (habitat5 == systemInfo.SystemStar)
                                    {
                                        flag23 = true;
                                    }
                                    else if (systemInfo.DominantEmpire.Empire.CapitalSystemStars.Contains(systemInfo.SystemStar))
                                    {
                                        flag24 = true;
                                    }
                                }
                                flag26 = false;
                                font = font_1;
                                num54 = 3f;
                            }
                        }
                    }
                    else
                    {
                        num55 = 2f;
                        flag26 = false;
                        if (systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                        {
                            flag25 = true;
                            font = font_3;
                        }
                    }
                    if (flag25)
                    {
                        int num56 = 0;
                        float num57 = 0f;
                        float num58 = 0f;
                        if (flag23 || flag24)
                        {
                            num57 = font.Height - 8;
                            num56 = (int)num57;
                        }
                        if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].IsRefuellingPoint)
                        {
                            num58 = font.Height - 4;
                            num56 += (int)num58;
                        }
                        string name = systemInfo.SystemStar.Name;
                        int num59 = (int)(num36 + 1f + (float)num56 + val3);
                        int num60 = (int)num37 - (font.Height + 1);
                        System.Drawing.Point point_ = new System.Drawing.Point(num59, num60);
                        method_267(graphics_0, name, font, point_, brush);
                        if (flag23)
                        {
                            graphics_0.DrawImage(srcRect: new System.Drawing.Rectangle(0, 0, main_0.bitmap_44.Width, main_0.bitmap_44.Height), destRect: new System.Drawing.Rectangle((int)(num36 + 1f + val3), (int)(num37 - (float)(font.Height - 1)), (int)num57, (int)num57), image: main_0.bitmap_44, srcUnit: GraphicsUnit.Pixel);
                        }
                        else if (flag24)
                        {
                            graphics_0.DrawImage(srcRect: new System.Drawing.Rectangle(0, 0, main_0.bitmap_43.Width, main_0.bitmap_43.Height), destRect: new System.Drawing.Rectangle((int)(num36 + 1f + val3), (int)(num37 - (float)(font.Height - 1)), (int)num57, (int)num57), image: main_0.bitmap_43, srcUnit: GraphicsUnit.Pixel);
                        }
                        if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].IsRefuellingPoint)
                        {
                            graphics_0.DrawImage(srcRect: new System.Drawing.Rectangle(0, 0, main_0.bitmap_55.Width, main_0.bitmap_55.Height), destRect: new System.Drawing.Rectangle((int)(num36 + 1f + val3 + num57), (int)(num37 - (float)(font.Height - 1)), (int)num58, (int)num58), image: main_0.bitmap_55, srcUnit: GraphicsUnit.Pixel);
                        }
                        float num61 = graphics_0.MeasureString(name, font, 300, StringFormat.GenericDefault).Width;
                        if (systemInfo.HasRuins)
                        {
                            float num62 = num36 + val3 + 1f + (float)num56 + num61;
                            float num63 = num37 - ((float)font.Height - 1f) + num54;
                            RectangleF rectangleF_4 = new RectangleF(num62, num63 + 5f, num55, num55);
                            method_243(graphics_0, brush, rectangleF_4);
                            rectangleF_4 = new RectangleF(num62 + 6f, num63 + 5f, num55, num55);
                            method_243(graphics_0, brush, rectangleF_4);
                            rectangleF_4 = new RectangleF(num62 + 3f, num63, num55, num55);
                            method_243(graphics_0, brush, rectangleF_4);
                        }
                        if (flag26 && systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                        {
                            int num64 = 0;
                            int num65 = (int)num37 + 1;
                            System.Drawing.Point point_2 = new System.Drawing.Point(num59 + 0, num65);
                            string string_ = systemInfo.DominantEmpire.TotalStrategicValue.ToString("0,K");
                            int num66 = (int)graphics_0.MeasureString(string_, Font, 200, StringFormat.GenericTypographic).Width;
                            method_266(graphics_0, string_, Font, point_2);
                            num64 = 0 + num66;
                            num64 += 10;
                            graphics_0.DrawImageUnscaled(point: new System.Drawing.Point(num59 + num64, num65), image: main_0.bitmap_37);
                            num64 += main_0.bitmap_37.Width;
                            point_2 = new System.Drawing.Point(num59 + num64, num65);
                            string string_2 = systemInfo.DominantEmpire.ColonyCount.ToString();
                            int num67 = (int)graphics_0.MeasureString(string_2, Font, 200, StringFormat.GenericTypographic).Width;
                            method_266(graphics_0, string_2, Font, point_2);
                            num64 += num67;
                            num64 += 8;
                            graphics_0.DrawImageUnscaled(point: new System.Drawing.Point(num59 + num64, num65), image: main_0.bitmap_38);
                            num64 += main_0.bitmap_38.Width - 2;
                            point_2 = new System.Drawing.Point(num59 + num64, num65);
                            string string_3 = systemInfo.DominantEmpire.Firepower.ToString();
                            if (systemVisibilityStatus != SystemVisibilityStatus.Visible && !main_0._Game.GodMode && !empire.EmpiresViewable.Contains(systemInfo.DominantEmpire.Empire))
                            {
                                string_3 = "?";
                            }
                            _ = graphics_0.MeasureString(string_3, Font, 200, StringFormat.GenericTypographic).Width;
                            method_266(graphics_0, string_3, Font, point_2);
                        }
                    }
                    if (systemInfo_0 != null && systemInfo == systemInfo_0)
                    {
                        if ((float)rectangle2.Width > rectangleF_3.Width)
                        {
                            method_207(rectangle_, graphics_0);
                        }
                        else
                        {
                            method_206(rectangleF_3, graphics_0);
                        }
                    }
                    else if (systemInfo.SystemStar != null && empire.CheckSystemExplored(systemInfo.SystemStar))
                    {
                        bool flag27 = false;
                        if (flag17)
                        {
                            if (systemInfo.PlayerPotentialColonies)
                            {
                                flag27 = true;
                            }
                            if (systemInfo.IndependentColonyCount > 0)
                            {
                                Empire empire3 = galaxy_0.CheckSystemOwnership(systemInfo.SystemStar);
                                if (empire3 == null || empire3 == empire)
                                {
                                    flag27 = true;
                                }
                            }
                        }
                        if (flag18 && systemInfo.HasScenery)
                        {
                            if (systemInfo.SystemStar != null && systemInfo.SystemStar.ScenicFactor > 0f)
                            {
                                flag27 = true;
                            }
                            if (!flag27 && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                            {
                                for (int num68 = 0; num68 < systemInfo.Habitats.Count; num68++)
                                {
                                    Habitat habitat6 = systemInfo.Habitats[num68];
                                    if (habitat6 != null && habitat6.ScenicFactor > 0f && !galaxy_0.CheckAlreadyHaveMiningStationAtHabitat(habitat6, empire))
                                    {
                                        flag27 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag19 && systemInfo.HasResearchBonus)
                        {
                            if (systemInfo.SystemStar != null && systemInfo.SystemStar.ResearchBonus > 0)
                            {
                                flag27 = true;
                            }
                            if (!flag27 && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                            {
                                for (int num69 = 0; num69 < systemInfo.Habitats.Count; num69++)
                                {
                                    Habitat habitat7 = systemInfo.Habitats[num69];
                                    if (habitat7 != null && habitat7.ResearchBonus > 0 && !empire.CheckResearchStationAtLocation(habitat7))
                                    {
                                        flag27 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag27)
                        {
                            if ((float)rectangle2.Width > rectangleF_3.Width)
                            {
                                method_207(rectangle_, graphics_0);
                            }
                            else
                            {
                                method_206(rectangleF_3, graphics_0);
                            }
                        }
                    }
                }
                brush.Dispose();
                pen7.Dispose();
            }
            if (main_0.double_0 > 70.0 && main_0.double_0 <= main_0.double_5)
            {
                for (int num70 = 0; num70 < galaxy_0.GalaxyLocations.Count; num70++)
                {
                    GalaxyLocation galaxyLocation = galaxy_0.GalaxyLocations[num70];
                    if (main_0._Game.GodMode || empire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        int num71 = (int)(((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0 - (double)int_16) / double_15);
                        int num72 = (int)(((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0 - (double)int_18) / double_15);
                        if (num71 > -100 && num71 < base.ClientRectangle.Width + 100 && num72 > -100 && num72 < base.ClientRectangle.Height + 100 && galaxyLocation.ShowName)
                        {
                            Font font2 = font_0;
                            font2 = ((galaxyLocation.Type == GalaxyLocationType.NebulaCloud) ? ((double_15 > 4000.0) ? font_2 : ((!(double_15 > 1000.0)) ? font_1 : font_0)) : ((double_15 > 4000.0) ? font_3 : ((!(double_15 > 1000.0)) ? font_0 : font_2)));
                            SizeF sizeF2 = graphics_0.MeasureString(galaxyLocation.Name, font2, 250, StringFormat.GenericTypographic);
                            int num73 = num71 - (int)(sizeF2.Width / 2f);
                            int num74 = num72 - (int)(sizeF2.Height / 2f);
                            if (galaxyLocation.Type != GalaxyLocationType.NebulaCloud)
                            {
                                num73 -= 5;
                                num74 -= 20;
                            }
                            method_267(graphics_0, galaxyLocation.Name, font2, new System.Drawing.Point(num73, num74), solidBrush_0);
                        }
                    }
                    if (galaxyLocation.Effect == GalaxyLocationEffectType.LightningDamage)
                    {
                        int num75 = (int)(((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0 - (double)int_16) / double_15);
                        int num76 = (int)(((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0 - (double)int_18) / double_15);
                        if (num75 > -100 && num75 < base.ClientRectangle.Width + 100 && num76 > -100 && num76 < base.ClientRectangle.Height + 100)
                        {
                            method_261(graphics_0, num75, num76, galaxyLocation, double_15);
                        }
                    }
                }
            }
            DateTime now = DateTime.Now;
            for (int num77 = 0; num77 < list_29.Count; num77++)
            {
                EventPing eventPing = list_29[num77];
                method_231(now, graphics_0, main_0.double_0, eventPing.Point.X, eventPing.Point.Y, main_0.int_13, main_0.int_14, int_16, int_17, int_18, int_19);
            }
            dateTime_3 = now;
            method_227(graphics_0, int_16, int_17, int_18, int_19);
            System.Drawing.Color.FromArgb(int_15, 0, 0, 255);
            System.Drawing.Color.FromArgb(int_15, 96, 96, 255);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(int_15, 0, 0, 255);
            System.Drawing.Color.FromArgb(int_15, 96, 96, 255);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(int_15, 192, 192, 0);
            System.Drawing.Color.FromArgb(int_15, 192, 192, 96);
            System.Drawing.Color color3 = System.Drawing.Color.FromArgb(int_15, 255, 0, 0);
            System.Drawing.Color.FromArgb(int_15, 255, 96, 96);
            DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
            for (int num78 = 0; num78 < empire.DiplomaticRelations.Count; num78++)
            {
                DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations[num78];
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            if (main_0.double_0 <= main_0.double_5)
            {
                for (int num79 = num9; num79 <= num10; num79++)
                {
                    for (int num80 = num11; num80 <= num12; num80++)
                    {
                        BuiltObjectList builtObjectList = galaxy_0.BuiltObjectIndex[num79][num80];
                        for (int num81 = 0; num81 < builtObjectList.Count; num81++)
                        {
                            BuiltObject builtObject3 = null;
                            if (builtObjectList.Count > num81)
                            {
                                builtObject3 = builtObjectList[num81];
                                if (builtObject3 == null || builtObject3.HasBeenDestroyed)
                                {
                                    builtObject3 = null;
                                }
                            }
                            if (builtObject3 == null)
                            {
                                continue;
                            }
                            bool flag28 = false;
                            bool flag29 = false;
                            bool flag30 = false;
                            if (builtObject3.Empire == empire)
                            {
                                _ = builtObject3.Empire.MainColor;
                            }
                            else if (galaxy_0.PirateEmpires.Contains(builtObject3.Empire))
                            {
                                System.Drawing.Color.FromArgb(48, 48, 48);
                                flag29 = true;
                            }
                            else if (empireList.Contains(builtObject3.Empire))
                            {
                                _ = builtObject3.Empire.MainColor;
                                flag29 = true;
                            }
                            else if (builtObject3.Empire != null)
                            {
                                _ = builtObject3.Empire.MainColor;
                            }
                            switch (builtObject3.SubRole)
                            {
                                case BuiltObjectSubRole.Escort:
                                case BuiltObjectSubRole.Frigate:
                                case BuiltObjectSubRole.Destroyer:
                                case BuiltObjectSubRole.Cruiser:
                                case BuiltObjectSubRole.CapitalShip:
                                case BuiltObjectSubRole.TroopTransport:
                                case BuiltObjectSubRole.Carrier:
                                    flag28 = flag4;
                                    if (flag29 && !flag28 && flag12)
                                    {
                                        flag28 = true;
                                    }
                                    if (flag30 && !flag28 && flag13)
                                    {
                                        flag28 = true;
                                    }
                                    break;
                                case BuiltObjectSubRole.ResupplyShip:
                                    flag28 = flag3;
                                    if (flag29 && !flag28 && flag12)
                                    {
                                        flag28 = true;
                                    }
                                    break;
                                case BuiltObjectSubRole.ExplorationShip:
                                    flag28 = flag7;
                                    break;
                                case BuiltObjectSubRole.ColonyShip:
                                    flag28 = flag8;
                                    break;
                                case BuiltObjectSubRole.ConstructionShip:
                                    flag28 = flag9;
                                    break;
                                case BuiltObjectSubRole.SmallFreighter:
                                case BuiltObjectSubRole.MediumFreighter:
                                case BuiltObjectSubRole.LargeFreighter:
                                case BuiltObjectSubRole.PassengerShip:
                                case BuiltObjectSubRole.GasMiningShip:
                                case BuiltObjectSubRole.MiningShip:
                                    flag28 = flag10;
                                    break;
                                case BuiltObjectSubRole.SmallSpacePort:
                                case BuiltObjectSubRole.MediumSpacePort:
                                case BuiltObjectSubRole.LargeSpacePort:
                                    flag28 = flag5;
                                    break;
                                case BuiltObjectSubRole.GasMiningStation:
                                case BuiltObjectSubRole.MiningStation:
                                case BuiltObjectSubRole.ResortBase:
                                case BuiltObjectSubRole.GenericBase:
                                case BuiltObjectSubRole.EnergyResearchStation:
                                case BuiltObjectSubRole.WeaponsResearchStation:
                                case BuiltObjectSubRole.HighTechResearchStation:
                                case BuiltObjectSubRole.MonitoringStation:
                                case BuiltObjectSubRole.DefensiveBase:
                                    flag28 = flag6;
                                    break;
                            }
                            bool flag31 = false;
                            if (flag28)
                            {
                                if (builtObject3.Empire == empire)
                                {
                                    flag31 = true;
                                }
                                else if (empire.IsObjectVisibleToThisEmpireImprecise(builtObject3))
                                {
                                    flag31 = true;
                                }
                            }
                            if (builtObject3.ShipGroup != null && builtObject3.ShipGroup.LeadShip == builtObject3)
                            {
                                flag31 = false;
                            }
                            if (!flag31)
                            {
                                continue;
                            }
                            int num82 = (int)((builtObject3.Xpos - (double)int_16) / double_15);
                            int num83 = (int)((builtObject3.Ypos - (double)int_18) / double_15);
                            if (num82 < 0 || num82 > int_13 || num83 < 0 || num83 > int_14)
                            {
                                continue;
                            }
                            if (flag15 || flag16)
                            {
                                bool flag32 = false;
                                flag32 = ((builtObject3.Owner == null) ? flag16 : flag15);
                                if (flag32 && builtObject3.Empire == empire)
                                {
                                    method_254(graphics_0, builtObject3, num82, num83, int_16, int_18, double_15, bool_13: false);
                                }
                            }
                            int num84 = 10;
                            if (double_15 < 400.0)
                            {
                                num84 = (int)((double)num84 * Math.Sqrt(400.0 / double_15));
                            }
                            if (double_15 > 4000.0)
                            {
                                num84 = (int)((double)num84 / (double_15 / 4000.0));
                            }
                            if (builtObject3.Owner == null)
                            {
                                System.Drawing.Color.FromArgb(160, 160, 160);
                            }
                            if (num84 < 6)
                            {
                                num84 = 6;
                            }
                            int num85 = 12;
                            if (builtObject3.Role == BuiltObjectRole.Base)
                            {
                                num85 = 15;
                            }
                            if (num84 > num85)
                            {
                                num84 = num85;
                            }
                            if (builtObject3.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject3.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject3.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                DrawShipSymbol(graphics_0, builtObject3, ResolveShipSymbolColor(builtObject3), num82 - num84 / 2, num83 - num84 / 2, num84, num84, num84, num84, fillInterior: true, main_0.double_0);
                                if (main_0._Game.SelectedObject == builtObject3)
                                {
                                    num84 = (int)((double)num84 * 1.6);
                                    method_210(num82 - num84 / 2, num83 - num84 / 2, num82 + num84 / 2, num83 + num84 / 2, graphics_0);
                                }
                                else if (main_0._Game.SelectedObject is BuiltObjectList)
                                {
                                    BuiltObjectList builtObjectList2 = (BuiltObjectList)main_0._Game.SelectedObject;
                                    if (builtObjectList2.Contains(builtObject3))
                                    {
                                        num84 = (int)((double)num84 * 1.6);
                                        method_210(num82 - num84 / 2, num83 - num84 / 2, num82 + num84 / 2, num83 + num84 / 2, graphics_0);
                                    }
                                }
                            }
                            if ((!flag29 && galaxy_0.FastTestShipInColonizedSystem(builtObject3)) || builtObject3.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject3.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject3.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                continue;
                            }
                            DrawShipSymbol(graphics_0, builtObject3, ResolveShipSymbolColor(builtObject3), num82 - num84 / 2, num83 - num84 / 2, num84, num84, num84, num84, fillInterior: true, main_0.double_0);
                            if (main_0._Game.SelectedObject == builtObject3)
                            {
                                num84 = (int)((double)num84 * 1.6);
                                method_210(num82 - num84 / 2, num83 - num84 / 2, num82 + num84 / 2, num83 + num84 / 2, graphics_0);
                            }
                            else if (main_0._Game.SelectedObject is BuiltObjectList)
                            {
                                BuiltObjectList builtObjectList3 = (BuiltObjectList)main_0._Game.SelectedObject;
                                if (builtObjectList3.Contains(builtObject3))
                                {
                                    num84 = (int)((double)num84 * 1.6);
                                    method_210(num82 - num84 / 2, num83 - num84 / 2, num82 + num84 / 2, num83 + num84 / 2, graphics_0);
                                }
                            }
                        }
                    }
                }
            }
            if (double_15 > num13)
            {
                BuiltObjectList builtObjectList4 = empire.KnownPirateBases;
                if (main_0._Game.GodMode)
                {
                    builtObjectList4 = new BuiltObjectList();
                    for (int num86 = 0; num86 < galaxy_0.PirateEmpires.Count; num86++)
                    {
                        Empire empire4 = galaxy_0.PirateEmpires[num86];
                        if (empire4.PirateEmpireBaseHabitat != null && empire4.PirateEmpireBaseHabitat.BasesAtHabitat != null && empire4.PirateEmpireBaseHabitat.BasesAtHabitat.Count > 0)
                        {
                            builtObjectList4.Add(empire4.PirateEmpireBaseHabitat.BasesAtHabitat[0]);
                        }
                    }
                }
                for (int num87 = 0; num87 < builtObjectList4.Count; num87++)
                {
                    BuiltObject builtObject4 = builtObjectList4[num87];
                    int num88 = (int)((builtObject4.Xpos - (double)int_16) / double_15);
                    int num89 = (int)((builtObject4.Ypos - (double)int_18) / double_15);
                    if (num88 >= 0 && num88 <= int_13 && num89 >= 0 && num89 <= int_14)
                    {
                        int num90 = 27;
                        int num91 = 16;
                        if (double_15 < 600.0)
                        {
                            num90 = 35;
                            num91 = 21;
                        }
                        else if (double_15 > 4000.0)
                        {
                            num90 = 20;
                            num91 = 12;
                        }
                        System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(num88 - num90 / 2, num89 - num91 / 2, num90, num91);
                        if (builtObject4.Empire != null && builtObject4.Empire.SmallFlagPicture != null)
                        {
                            graphics_0.DrawImage(builtObject4.Empire.SmallFlagPicture, rect2);
                        }
                        else
                        {
                            graphics_0.DrawImage(main_0.bitmap_49, rect2);
                        }
                        System.Drawing.Color color4 = System.Drawing.Color.FromArgb(80, 80, 80);
                        using Pen pen8 = new Pen(color4, 1f);
                        graphics_0.DrawRectangle(pen8, rect2);
                    }
                }
                int num92 = 22;
                if (double_15 < 400.0)
                {
                    num92 = (int)((double)num92 * Math.Sqrt(400.0 / double_15));
                }
                if (double_15 > 4000.0)
                {
                    num92 = (int)((double)num92 / (double_15 / 4000.0));
                }
                if (num92 < 12)
                {
                    num92 = 12;
                }
                SolidBrush solidBrush2 = new SolidBrush(color);
                if (empire != null)
                {
                    if (flag2)
                    {
                        method_256(graphics_0, solidBrush2, empire, double_15, num92, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                    }
                    SolidBrush solidBrush3 = new SolidBrush(color2);
                    SolidBrush solidBrush4 = new SolidBrush(color3);
                    for (int num93 = 0; num93 < galaxy_0.Empires.Count; num93++)
                    {
                        Empire empire5 = galaxy_0.Empires[num93];
                        if (empire5 == empire)
                        {
                            continue;
                        }
                        if (empireList.Contains(empire5))
                        {
                            if (flag2 || flag11)
                            {
                                method_256(graphics_0, solidBrush4, empire5, double_15, num92, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                            }
                        }
                        else if (flag2)
                        {
                            method_256(graphics_0, solidBrush3, empire5, double_15, num92, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                        }
                    }
                    solidBrush3?.Dispose();
                    solidBrush4?.Dispose();
                }
                solidBrush2?.Dispose();
            }
            solidBrush?.Dispose();
            pen.Dispose();
        }

        private System.Drawing.Rectangle method_249(RectangleF rectangleF_1)
        {
            int num = (int)(rectangleF_1.X + 0.5f);
            int num2 = (int)(rectangleF_1.Y + 0.5f);
            int num3 = (int)(rectangleF_1.Width + 0.5f);
            int num4 = (int)(rectangleF_1.Height + 0.5f);
            return new System.Drawing.Rectangle(num, num2, num3, num4);
        }

        public EventPing[] ToArrayThreadSafe(List<EventPing> list)
        {
            int num = 0;
            if (list != null)
            {
                num = list.Count;
            }
            EventPing[] array = new EventPing[num];
            try
            {
                for (int i = 0; i < num; i++)
                {
                    array[i] = list[i];
                }
                return array;
            }
            catch
            {
                return array;
            }
        }

        private void method_250(SpriteBatch spriteBatch_2, SpriteBatch spriteBatch_3, int int_11, int int_12, int int_13, int int_14, double double_15, int int_15)
        {
            int_15 = 255;
            bool flag = false;
            if (main_0.gameOptions_0 != null)
            {
                flag = main_0.gameOptions_0.CleanGalaxyView;
            }
            DateTime currentDateTime = galaxy_0.CurrentDateTime;
            long currentStarDate = galaxy_0.CurrentStarDate;
            Empire empire = main_0._Game.PlayerEmpire;
            bool flag2 = false;
            if (main_0.empire_1 != null)
            {
                empire = main_0.empire_1;
                flag2 = true;
            }
            bool flag3 = true;
            bool flag4 = true;
            bool flag5 = true;
            bool flag6 = true;
            bool flag7 = true;
            bool flag8 = true;
            bool flag9 = true;
            bool flag10 = true;
            bool flag11 = true;
            bool flag12 = true;
            bool flag13 = true;
            bool flag14 = true;
            if (main_0.gameOptions_0 != null && !main_0._Game.GodMode && double_15 > 3500.0)
            {
                flag3 = main_0.gameOptions_0.GalaxyViewDisplayFleets;
                flag4 = main_0.gameOptions_0.GalaxyViewDisplayResupplyShips;
                flag5 = main_0.gameOptions_0.GalaxyViewDisplayMilitaryShips;
                flag6 = main_0.gameOptions_0.GalaxyViewDisplaySpacePorts;
                flag7 = main_0.gameOptions_0.GalaxyViewDisplayOtherBases;
                flag8 = main_0.gameOptions_0.GalaxyViewDisplayExplorationShips;
                flag9 = main_0.gameOptions_0.GalaxyViewDisplayColonyShips;
                flag10 = main_0.gameOptions_0.GalaxyViewDisplayConstructionShips;
                flag11 = main_0.gameOptions_0.GalaxyViewDisplayCivilianShips;
                flag12 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets;
                flag13 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips;
                flag14 = main_0.gameOptions_0.GalaxyViewDisplayAlwaysPirates;
            }
            bool flag15 = false;
            bool flag16 = false;
            bool flag17 = false;
            bool flag18 = false;
            bool flag19 = false;
            bool flag20 = false;
            if (main_0.gameOptions_0 != null)
            {
                flag15 = main_0.gameOptions_0.MapOverlayFleetPostures;
                flag16 = main_0.gameOptions_0.MapOverlayTravelVectorsState;
                flag17 = main_0.gameOptions_0.MapOverlayTravelVectorsPrivate;
                flag18 = main_0.gameOptions_0.MapOverlayPotentialColonies;
                flag19 = main_0.gameOptions_0.MapOverlayScenicLocations;
                flag20 = main_0.gameOptions_0.MapOverlayResearchLocations;
            }
            System.Drawing.Color color = System.Drawing.Color.FromArgb(int_15, 112, 112, 112);
            double num = 150000.0;
            float num2 = (float)((num / double_15 + 0.5) * 1.1);
            float num3 = num2 / 2f;
            int num4 = (int)((double)Galaxy.SizeX / double_15);
            double double_16 = (double)int_13 * double_15;
            double double_17 = (double)int_14 * double_15;
            int int_16 = 0;
            int int_17 = 0;
            int int_18 = 0;
            int int_19 = 0;
            method_225(int_11, int_12, double_16, double_17, ref int_16, ref int_17, ref int_18, ref int_19);
            double num5 = (double)num4 / (double)Galaxy.SectorMaxX;
            Vector2 vector = spriteFont_0.MeasureString("H");
            float num6 = (float)(num5 / 2.0 - (double)vector.X / 2.0);
            float num7 = (float)(num5 / 2.0 - (double)vector.Y / 2.0);
            SolidBrush solidBrush = new SolidBrush(main_0.color_7);
            int num8 = galaxy_0.ResolveSector(int_16, int_12).X;
            int num9 = galaxy_0.ResolveSector(int_17, int_12).X;
            int num10 = galaxy_0.ResolveSector(int_11, int_18).Y;
            int num11 = galaxy_0.ResolveSector(int_11, int_19).Y;
            int num12 = galaxy_0.ResolveIndex(int_16, int_12).X;
            int num13 = galaxy_0.ResolveIndex(int_17, int_12).X;
            int num14 = galaxy_0.ResolveIndex(int_11, int_18).Y;
            int num15 = galaxy_0.ResolveIndex(int_11, int_19).Y;
            double num16 = 150.0;
            if (double_15 > num16)
            {
                if (!flag)
                {
                    bool flag21 = false;
                    bool flag22 = false;
                    float num17 = 0f;
                    float num18 = int_14;
                    if (num10 <= 1)
                    {
                        num17 = (float)((double)(num10 * Galaxy.SectorSize - int_18) / double_15);
                    }
                    if (num11 >= Galaxy.SectorMaxX - 1)
                    {
                        num18 = (float)((double)((num11 + 1) * Galaxy.SectorSize - int_18) / double_15);
                        flag22 = true;
                    }
                    float num19 = 0f;
                    float num20 = int_13;
                    if (num8 <= 1)
                    {
                        num19 = (float)((double)(num8 * Galaxy.SectorSize - int_16) / double_15);
                    }
                    if (num9 >= Galaxy.SectorMaxY - 1)
                    {
                        num20 = (float)((double)((num9 + 1) * Galaxy.SectorSize - int_16) / double_15);
                        flag21 = true;
                    }
                    for (int i = num8; i <= num9; i++)
                    {
                        float num21 = (float)((double)(i * Galaxy.SectorSize - int_16) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num21, num17, num21, num18, main_0.color_5, 1);
                        string text = ((char)(i + 65)).ToString();
                        float val = num17 - 1f;
                        val = Math.Max(0f, val);
                        XnaDrawingHelper.DrawString(point: new PointF(num21 + num6, val), spriteBatch: spriteBatch_2, text: text, font: spriteFont_1, color: solidBrush.Color);
                        val = num18 - 14f;
                        val = Math.Min((float)base.ClientRectangle.Height - 11f, val);
                        XnaDrawingHelper.DrawString(point: new PointF(num21 + num6, val), spriteBatch: spriteBatch_2, text: text, font: spriteFont_1, color: solidBrush.Color);
                    }
                    if (flag21)
                    {
                        float num22 = (float)((double)((num9 + 1) * Galaxy.SectorSize - int_16) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num22, num17, num22, num18, main_0.color_5, 1);
                    }
                    for (int j = num10; j <= num11; j++)
                    {
                        float num23 = (float)((double)(j * Galaxy.SectorSize - int_18) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num19, num23, num20, num23, main_0.color_5, 1);
                        string text2 = (j + 1).ToString();
                        float val2 = num19 - 2f;
                        val2 = Math.Max(0f, val2);
                        XnaDrawingHelper.DrawString(point: new PointF(val2, num23 + num7), spriteBatch: spriteBatch_2, text: text2, font: spriteFont_1, color: solidBrush.Color);
                        val2 = num20 - 14f;
                        val2 = Math.Min((float)base.ClientRectangle.Width - 8f, val2);
                        XnaDrawingHelper.DrawString(point: new PointF(val2, num23 + num7), spriteBatch: spriteBatch_2, text: text2, font: spriteFont_1, color: solidBrush.Color);
                    }
                    if (flag22)
                    {
                        float num24 = (float)((double)((num11 + 1) * Galaxy.SectorSize - int_18) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num19, num24, num20, num24, main_0.color_5, 1);
                    }
                }
                new System.Drawing.Rectangle(int_16, int_18, int_17 - int_16, int_19 - int_18);
            }
            if (flag15)
            {
                method_247(spriteBatch_2, empire, double_15, int_13, int_14, int_16, int_18);
            }
            for (int k = 0; k < galaxy_0.Systems.Count; k++)
            {
                SystemInfo systemInfo = galaxy_0.Systems[k];
                if (systemInfo.Sector.X < num8 || systemInfo.Sector.X > num9 || systemInfo.Sector.Y < num10 || systemInfo.Sector.Y > num11)
                {
                    continue;
                }
                float num25 = (float)((systemInfo.SystemStar.Xpos - (double)int_16) / double_15);
                float num26 = (float)((systemInfo.SystemStar.Ypos - (double)int_18) / double_15);
                SystemVisibilityStatus systemVisibilityStatus = empire.CheckSystemVisibilityStatus(k);
                int num27 = 0;
                float val3 = 0f;
                bool flag23 = false;
                bool flag24 = false;
                if (double_15 > num16 && !flag2 && !flag && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode) && systemInfo.DominantEmpire != null)
                {
                    num27 = Math.Min(systemInfo.DominantEmpire.TotalStrategicValue, 1500000);
                    val3 = (float)(Math.Pow(num27, 0.35) * 600.0);
                    float num28 = 0f;
                    float num29 = 0f;
                    HabitatList habitatList = new HabitatList();
                    habitatList.AddRange(systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars);
                    if (habitatList.Count > 0)
                    {
                        for (int l = 0; l < habitatList.Count; l++)
                        {
                            Habitat habitat = habitatList[l];
                            if (habitat != null)
                            {
                                SystemVisibilityStatus systemVisibilityStatus2 = empire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                                if (systemVisibilityStatus2 == SystemVisibilityStatus.Visible || systemVisibilityStatus2 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                {
                                    num28 = (float)((habitat.Xpos - (double)int_16) / double_15);
                                    num29 = (float)((habitat.Ypos - (double)int_18) / double_15);
                                    XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26, num28, num29, systemInfo.DominantEmpire.Empire.MainColor, 1, dashed: true);
                                }
                            }
                        }
                    }
                    if (systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count > 0)
                    {
                        Habitat[] array = ListHelper.ToArrayThreadSafe(systemInfo.DominantEmpire.Empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars);
                        foreach (Habitat habitat2 in array)
                        {
                            if (habitatList.Contains(habitat2) || habitat2 == null)
                            {
                                continue;
                            }
                            SystemInfo systemInfo2 = galaxy_0.Systems[habitat2.SystemIndex];
                            if (systemInfo2.Sector.X < num8 || systemInfo2.Sector.X > num9 || systemInfo2.Sector.Y < num10 || systemInfo2.Sector.Y > num11)
                            {
                                SystemVisibilityStatus systemVisibilityStatus3 = empire.CheckSystemVisibilityStatus(habitat2.SystemIndex);
                                if (systemVisibilityStatus3 == SystemVisibilityStatus.Visible || systemVisibilityStatus3 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                {
                                    num28 = (float)((habitat2.Xpos - (double)int_16) / double_15);
                                    num29 = (float)((habitat2.Ypos - (double)int_18) / double_15);
                                    XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26, num28, num29, systemInfo.DominantEmpire.Empire.MainColor, 1, dashed: true);
                                }
                            }
                        }
                    }
                    EmpireSystemSummaryList otherEmpires = systemInfo.OtherEmpires;
                    if (otherEmpires != null)
                    {
                        for (int n = 0; n < otherEmpires.Count; n++)
                        {
                            EmpireSystemSummary empireSystemSummary = otherEmpires[n];
                            Empire empire2 = empireSystemSummary.Empire;
                            habitatList.Clear();
                            habitatList.AddRange(empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].LinkSystemStars);
                            if (habitatList.Count > 0)
                            {
                                for (int num30 = 0; num30 < habitatList.Count; num30++)
                                {
                                    Habitat habitat3 = habitatList[num30];
                                    if (habitat3 != null)
                                    {
                                        SystemVisibilityStatus systemVisibilityStatus4 = empire.CheckSystemVisibilityStatus(habitat3.SystemIndex);
                                        if (systemVisibilityStatus4 == SystemVisibilityStatus.Visible || systemVisibilityStatus4 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                        {
                                            num28 = (float)((habitat3.Xpos - (double)int_16) / double_15);
                                            num29 = (float)((habitat3.Ypos - (double)int_18) / double_15);
                                            XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26, num28, num29, empire2.MainColor, 1, dashed: true);
                                        }
                                    }
                                }
                            }
                            if (empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count <= 0)
                            {
                                continue;
                            }
                            ListHelper.ToArrayThreadSafe(empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars);
                            for (int num31 = 0; num31 < empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars.Count; num31++)
                            {
                                Habitat habitat4 = empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].ReciprocalLinkSystemStars[num31];
                                if (habitatList.Contains(habitat4) || habitat4 == null)
                                {
                                    continue;
                                }
                                SystemInfo systemInfo3 = galaxy_0.Systems[habitat4.SystemIndex];
                                if (systemInfo3.Sector.X < num8 || systemInfo3.Sector.X > num9 || systemInfo3.Sector.Y < num10 || systemInfo3.Sector.Y > num11)
                                {
                                    SystemVisibilityStatus systemVisibilityStatus5 = empire.CheckSystemVisibilityStatus(habitat4.SystemIndex);
                                    if (systemVisibilityStatus5 == SystemVisibilityStatus.Visible || systemVisibilityStatus5 == SystemVisibilityStatus.Explored || main_0._Game.GodMode)
                                    {
                                        num28 = (float)((habitat4.Xpos - (double)int_16) / double_15);
                                        num29 = (float)((habitat4.Ypos - (double)int_18) / double_15);
                                        XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26, num28, num29, empire2.MainColor, 1, dashed: true);
                                    }
                                }
                            }
                        }
                    }
                }
                val3 = Math.Max(Galaxy.MaxSolarSystemSize, val3);
                if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    val3 = systemInfo.SystemStar.NovaProgression;
                }
                val3 = (int)((double)val3 / double_15);
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    val3 = (int)((double)val3 * 0.7);
                }
                if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    val3 = (int)((double)val3 * 2.0);
                }
                if (!(num25 + val3 >= 0f) || !(num25 - val3 <= (float)int_13) || !(num26 + val3 >= 0f) || !(num26 - val3 <= (float)int_14))
                {
                    continue;
                }
                Pen pen = method_268(empire, flag2, systemInfo, int_15);
                SolidBrush solidBrush2 = method_269(empire, flag2, systemInfo, int_15);
                int val4 = (int)((double)systemInfo.SystemStar.Diameter / (double_15 / 70.0));
                val4 = Math.Min(Math.Max(8, val4), 25);
                if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    val4 = (int)((double)systemInfo.SystemStar.NovaProgression * 2.0 / double_15);
                }
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    val3 = Math.Max(6f, val3);
                }
                else if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    val3 = Math.Max(5f, val3);
                }
                else if (val3 * 2f - 4f <= (float)val4)
                {
                    val3 = val4 / 2 + 3;
                }
                if (double_15 > num16 && !flag && (systemInfo.PlayerPotentialColonies || ((systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored) && systemInfo.DominantEmpire == null && systemInfo.IndependentColonyCount > 0)))
                {
                    float num32 = (float)((double)Galaxy.MaxSolarSystemSize / double_15);
                    num32 *= 0.75f;
                    int num33 = (int)((double)val4 * 0.7);
                    if (num32 * 2f - 4f <= (float)num33)
                    {
                        num32 = num33 / 2 + 3;
                    }
                    RectangleF rectangleF = new RectangleF(num25 - num32, num26 - num32, num32 * 2f, num32 * 2f);
                    System.Drawing.Rectangle area = new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
                    XnaDrawingHelper.DrawCircle(spriteBatch_2, area, color, 1, dashed: true);
                }
                RectangleF rectangleF_ = new RectangleF(num25 - val3, num26 - val3, val3 * 2f, val3 * 2f);
                if (double_15 > num16)
                {
                    if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                    {
                        val3 *= 0.7f;
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num25, num26 - val3, num25, num26 + val3, pen.Color, (int)pen.Width);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num25 - val3, num26, num25 + val3, num26, pen.Color, (int)pen.Width);
                    }
                    else if (!flag && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode) && (systemInfo.DominantEmpire != null || systemInfo.IndependentColonyCount > 0))
                    {
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, method_249(rectangleF_), pen.Color, (int)pen.Width, pen.DashStyle == DashStyle.Dash);
                    }
                }
                if (!flag && empire.PirateEmpireBaseHabitat != null && empire.PirateInfluenceSystemIds.Contains(systemInfo.SystemStar.SystemIndex))
                {
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle((int)(num25 - num3), (int)(num26 - num3), (int)num2, (int)num2);
                    int alpha = 160;
                    int alpha2 = 128;
                    if (double_15 < 500.0)
                    {
                        alpha = Math.Max(0, Math.Min(255, (int)(double_15 / 500.0 * 160.0)));
                        alpha2 = Math.Max(0, Math.Min(255, (int)(double_15 / 500.0 * 128.0)));
                    }
                    System.Drawing.Color tintColor = System.Drawing.Color.FromArgb(alpha, empire.MainColor);
                    if (empire.MainColor == System.Drawing.Color.FromArgb(255, 1, 1, 1))
                    {
                        tintColor = System.Drawing.Color.FromArgb(alpha2, 255, 255, 255);
                    }
                    if (texture2D_24 != null && !texture2D_24.IsDisposed)
                    {
                        XnaDrawingHelper.DrawTexture(spriteBatch_3, texture2D_24, method_249(rectangle), 0f, 1f, tintColor);
                    }
                }
                Texture2D texture2D = null;
                Bitmap bitmap = null;
                bool flag25 = false;
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    texture2D = texture2D_18[0];
                    bitmap = bitmap_1[0];
                }
                else if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    bitmap = main_0.bitmap_206[systemInfo.SystemStar.NovaImageIndexMajor];
                    texture2D = texture2D_20[systemInfo.SystemStar.NovaImageIndexMajor];
                }
                else
                {
                    texture2D = texture2D_18[systemInfo.SystemStar.MapPictureRef];
                    bitmap = bitmap_1[systemInfo.SystemStar.MapPictureRef];
                }
                double num34 = (double)texture2D.Height / (double)texture2D.Width;
                int num35 = val4;
                int num36 = (int)((double)val4 * num34);
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle((int)num25 - num35 / 2, (int)num26 - num36 / 2, num35, num36);
                double num37 = 0.6;
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    num37 = 1.0;
                }
                if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                {
                    num37 = 1.3;
                }
                else if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                {
                    num37 = 1.3;
                }
                int val5 = (int)((double)val4 * num37);
                int val6 = (int)((double)val4 * num37 * num34);
                val5 = Math.Max(8, val5);
                val6 = Math.Max(8, val6);
                System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle((int)num25 - val5 / 2, (int)num26 - val6 / 2, val5, val6);
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    rectangle_ = new System.Drawing.Rectangle((int)num25 - (int)val3, (int)num26 - (int)val3, (int)val3 * 2, (int)val3 * 2);
                }
                else
                {
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova && systemInfo.SystemStar.Type != HabitatType.BlackHole && double_15 < 150.0)
                    {
                        int val7 = (int)((double)systemInfo.SystemStar.Diameter / double_15);
                        int num38 = (int)((systemInfo.SystemStar.Xpos - (double)int_16) / double_15) - 1;
                        int num39 = (int)((systemInfo.SystemStar.Ypos - (double)int_18) / double_15) - 1;
                        val7 = Math.Max(10, val7);
                        rectangle2 = new System.Drawing.Rectangle(num38 - val7 / 2, num39 - val7 / 2, val7 + 2, val7 + 2);
                    }
                    if (systemInfo.SystemStar.Type == HabitatType.SuperNova)
                    {
                        _ = rectangle2.Width;
                    }
                    if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                    {
                        if (double_15 < 5100.0)
                        {
                            val3 = Math.Max(11f, val3);
                            val3 = Math.Min(17f, val3);
                        }
                        rectangle2 = new System.Drawing.Rectangle((int)num25 - (int)val3, (int)num26 - (int)val3, (int)val3 * 2, (int)val3 * 2);
                        rectangle_ = new System.Drawing.Rectangle(rectangle2.Location, rectangle2.Size);
                        rectangleF_ = new System.Drawing.Rectangle(rectangle2.Location, rectangle2.Size);
                    }
                    if (double_15 > method_60(systemInfo.SystemStar.Type) && (double_15 < 5100.0 || systemInfo.SystemStar.Type == HabitatType.BlackHole || systemInfo.SystemStar.Type == HabitatType.SuperNova))
                    {
                        if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                        {
                            XnaDrawingHelper.DrawTexture(spriteBatch_3, texture2D, rectangle2, 0f);
                        }
                        else
                        {
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, rectangle2, 0f);
                        }
                    }
                    if (flag25)
                    {
                        method_22(texture2D);
                    }
                    if (systemInfo.SystemStar.Type != HabitatType.SuperNova && systemInfo.SystemStar.Type != HabitatType.BlackHole)
                    {
                        System.Drawing.Color color_ = method_120(bitmap);
                        double double_18 = 2.0;
                        double double_19 = 1.0;
                        if (double_15 < 5100.0)
                        {
                            double double_20 = 20.0;
                            double num40 = method_61(systemInfo.SystemStar.Type, out double_20);
                            if (double_15 > double_20)
                            {
                                if (double_15 < num40)
                                {
                                    double num41 = num40 - double_20;
                                    double_18 = 1.0 + (double_15 - double_20) / num41 * 1.0;
                                    double_19 = (double_15 - double_20) / num41;
                                }
                                method_234(spriteBatch_2, galaxy_0, currentStarDate, systemInfo.SystemStar, color_, rectangle2, double_18, double_19);
                            }
                        }
                        else
                        {
                            double_18 = 1.3;
                            method_234(spriteBatch_2, galaxy_0, currentStarDate, systemInfo.SystemStar, color_, rectangle2, double_18, double_19);
                        }
                    }
                }
                //BaconMain.DrawWeaponRanges(spriteBatch_2, int_16, int_18, double_15);
                MainView.OnDrawWeaponRangesMods(spriteBatch_2, int_16, int_18, double_15);
                if (double_15 > num16)
                {
                    if (main_0._Game.SelectedObject == systemInfo)
                    {
                        if ((float)rectangle_.Width > rectangleF_.Width)
                        {
                            method_212(spriteBatch_2, rectangle_.X - 3, rectangle_.Y - 3, rectangle_.X + rectangle_.Width + 4, rectangle_.Y + rectangle_.Height + 4);
                        }
                        else
                        {
                            method_212(spriteBatch_2, rectangleF_.X - 3f, rectangleF_.Y - 3f, rectangleF_.X + rectangleF_.Width + 4f, rectangleF_.Y + rectangleF_.Height + 4f);
                        }
                    }
                    bool flag26 = false;
                    SpriteFont spriteFont = spriteFont_1;
                    float num42 = 0f;
                    float num43 = 3f;
                    bool flag27 = false;
                    if (double_15 < 4000.0)
                    {
                        flag27 = true;
                        if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible || main_0._Game.GodMode)
                        {
                            flag26 = true;
                            if (systemInfo.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                            {
                                if (systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire.Capital != null)
                                {
                                    Habitat habitat5 = Galaxy.DetermineHabitatSystemStar(systemInfo.DominantEmpire.Empire.Capital);
                                    if (habitat5 == systemInfo.SystemStar)
                                    {
                                        flag23 = true;
                                    }
                                    else if (systemInfo.DominantEmpire.Empire.CapitalSystemStars.Contains(systemInfo.SystemStar))
                                    {
                                        flag24 = true;
                                    }
                                }
                                spriteFont = spriteFont_2;
                                num42 = 3f;
                            }
                        }
                    }
                    else
                    {
                        num43 = 2f;
                        if ((systemInfo.IndependentColonyCount > 0 || systemInfo.DominantEmpire != null) && (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || main_0._Game.GodMode))
                        {
                            flag26 = true;
                            spriteFont = spriteFont_0;
                        }
                    }
                    if (flag26 && !flag)
                    {
                        if (systemInfo.PlagueId >= 0)
                        {
                            Plague plague = Galaxy.PlaguesStatic[systemInfo.PlagueId];
                            if (plague != null && plague.PictureRef >= 0 && plague.PictureRef < texture2D_26.Length)
                            {
                                Texture2D texture2D2 = ((!flag27) ? texture2D_30[plague.PictureRef] : texture2D_26[plague.PictureRef]);
                                _ = spriteFont.LineSpacing;
                                System.Drawing.Rectangle destination = new System.Drawing.Rectangle((int)(num25 - (val3 + (float)texture2D2.Width / 2f)), (int)(num26 - (val3 + (float)texture2D2.Width / 2f)), texture2D2.Width, texture2D2.Height);
                                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D2, destination, 0f);
                            }
                        }
                        int num44 = 0;
                        float num45 = 0f;
                        float num46 = 0f;
                        Texture2D texture2D3;
                        Texture2D texture2D4;
                        Texture2D texture2D5;
                        if (flag27)
                        {
                            texture2D3 = texture2D_9;
                            texture2D4 = texture2D_10;
                            texture2D5 = texture2D_11;
                        }
                        else
                        {
                            texture2D3 = texture2D_27;
                            texture2D4 = texture2D_28;
                            texture2D5 = texture2D_29;
                        }
                        if (flag23 || flag24)
                        {
                            num45 = texture2D3.Height;
                            num44 = (int)num45;
                        }
                        if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].IsRefuellingPoint)
                        {
                            num46 = texture2D5.Height;
                            num44 += (int)num46;
                        }
                        string name = systemInfo.SystemStar.Name;
                        int num47 = (int)(num25 + 1f + (float)num44 + val3);
                        int num48 = (int)num26 - (spriteFont.LineSpacing + 1);
                        XnaDrawingHelper.DrawStringDropShadow(point: new System.Drawing.Point(num47, num48), spriteBatch: spriteBatch_2, text: name, font: spriteFont, foreColor: solidBrush2.Color);
                        if (flag23)
                        {
                            System.Drawing.Rectangle destination2 = new System.Drawing.Rectangle((int)(num25 + 1f + val3), (int)(num26 - (float)(texture2D3.Height - 1)), texture2D3.Width, texture2D3.Height);
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D3, destination2, 0f);
                        }
                        else if (flag24)
                        {
                            System.Drawing.Rectangle destination3 = new System.Drawing.Rectangle((int)(num25 + 1f + val3), (int)(num26 - (float)(texture2D4.Height - 1)), texture2D4.Width, texture2D4.Height);
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D4, destination3, 0f);
                        }
                        if (empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].IsRefuellingPoint)
                        {
                            System.Drawing.Rectangle destination4 = new System.Drawing.Rectangle((int)(num25 + 1f + val3 + num45), (int)(num26 - (float)(texture2D5.Height - 1)), texture2D5.Width, texture2D5.Height);
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D5, destination4, 0f);
                        }
                        float num49 = spriteFont.MeasureString(name).X;
                        if (systemInfo.HasRuins)
                        {
                            float num50 = num25 + val3 + 1f + (float)num44 + num49;
                            float num51 = num26 - ((float)spriteFont.LineSpacing - 1f) + num42;
                            RectangleF rectangleF_2 = new RectangleF(num50, num51 + 5f, num43, num43);
                            XnaDrawingHelper.FillRectangle(spriteBatch_2, method_249(rectangleF_2), solidBrush2.Color);
                            rectangleF_2 = new RectangleF(num50 + 6f, num51 + 5f, num43, num43);
                            XnaDrawingHelper.FillRectangle(spriteBatch_2, method_249(rectangleF_2), solidBrush2.Color);
                            rectangleF_2 = new RectangleF(num50 + 3f, num51, num43, num43);
                            XnaDrawingHelper.FillRectangle(spriteBatch_2, method_249(rectangleF_2), solidBrush2.Color);
                        }
                    }
                    if (systemInfo_0 != null && systemInfo == systemInfo_0)
                    {
                        if ((float)rectangle2.Width > rectangleF_.Width)
                        {
                            method_209(spriteBatch_2, rectangle_);
                        }
                        else
                        {
                            method_208(spriteBatch_2, rectangleF_);
                        }
                    }
                    else if (systemInfo.SystemStar != null && empire.CheckSystemExplored(systemInfo.SystemStar))
                    {
                        bool flag28 = false;
                        if (flag18)
                        {
                            if (systemInfo.PlayerPotentialColonies)
                            {
                                flag28 = true;
                            }
                            if (systemInfo.IndependentColonyCount > 0)
                            {
                                Empire empire3 = galaxy_0.CheckSystemOwnership(systemInfo.SystemStar);
                                if (empire3 == null || empire3 == empire)
                                {
                                    flag28 = true;
                                }
                            }
                        }
                        if (flag19 && systemInfo.HasScenery)
                        {
                            if (systemInfo.SystemStar != null && systemInfo.SystemStar.ScenicFactor > 0f)
                            {
                                flag28 = true;
                            }
                            if (!flag28 && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                            {
                                for (int num52 = 0; num52 < systemInfo.Habitats.Count; num52++)
                                {
                                    Habitat habitat6 = systemInfo.Habitats[num52];
                                    if (habitat6 != null && habitat6.ScenicFactor > 0f && !galaxy_0.CheckAlreadyHaveMiningStationAtHabitat(habitat6, empire))
                                    {
                                        flag28 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag20 && systemInfo.HasResearchBonus)
                        {
                            if (systemInfo.SystemStar != null && systemInfo.SystemStar.ResearchBonus > 0)
                            {
                                flag28 = true;
                            }
                            if (!flag28 && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                            {
                                for (int num53 = 0; num53 < systemInfo.Habitats.Count; num53++)
                                {
                                    Habitat habitat7 = systemInfo.Habitats[num53];
                                    if (habitat7 != null && habitat7.ResearchBonus > 0 && !empire.CheckResearchStationAtLocation(habitat7))
                                    {
                                        flag28 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag28)
                        {
                            if ((float)rectangle2.Width > rectangleF_.Width)
                            {
                                method_209(spriteBatch_2, rectangle_);
                            }
                            else
                            {
                                method_208(spriteBatch_2, rectangleF_);
                            }
                        }
                    }
                }
                solidBrush2.Dispose();
                pen.Dispose();
            }
            if (main_0.double_0 > 70.0 && main_0.double_0 <= main_0.double_5)
            {
                for (int num54 = 0; num54 < galaxy_0.GalaxyLocations.Count; num54++)
                {
                    GalaxyLocation galaxyLocation = galaxy_0.GalaxyLocations[num54];
                    if (main_0._Game.GodMode || empire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        int num55 = (int)(((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0 - (double)int_16) / double_15);
                        int num56 = (int)(((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0 - (double)int_18) / double_15);
                        if (num55 > -100 && num55 < base.ClientRectangle.Width + 100 && num56 > -100 && num56 < base.ClientRectangle.Height + 100 && galaxyLocation.ShowName && !flag)
                        {
                            SpriteFont spriteFont2 = spriteFont_3;
                            spriteFont2 = ((galaxyLocation.Type == GalaxyLocationType.NebulaCloud) ? ((double_15 > 4000.0) ? spriteFont_1 : ((!(double_15 > 1000.0)) ? spriteFont_2 : spriteFont_3)) : ((double_15 > 4000.0) ? spriteFont_0 : ((!(double_15 > 1000.0)) ? spriteFont_3 : spriteFont_1)));
                            Vector2 vector2 = spriteFont2.MeasureString(galaxyLocation.Name);
                            int num57 = num55 - (int)(vector2.X / 2f);
                            int num58 = num56 - (int)(vector2.Y / 2f);
                            if (galaxyLocation.Type != GalaxyLocationType.NebulaCloud)
                            {
                                num57 -= 5;
                                num58 -= 20;
                            }
                            XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, galaxyLocation.Name, spriteFont2, solidBrush_0.Color, num57, num58);
                        }
                    }
                    if (galaxyLocation.Effect == GalaxyLocationEffectType.LightningDamage)
                    {
                        int num59 = (int)(((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0 - (double)int_16) / double_15);
                        int num60 = (int)(((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0 - (double)int_18) / double_15);
                        if (num59 > -100 && num59 < base.ClientRectangle.Width + 100 && num60 > -100 && num60 < base.ClientRectangle.Height + 100)
                        {
                            method_262(spriteBatch_2, num59, num60, galaxyLocation, double_15);
                        }
                    }
                }
            }
            DateTime now = DateTime.Now;
            EventPing[] array2 = ToArrayThreadSafe(list_29);
            foreach (EventPing eventPing in array2)
            {
                method_232(spriteBatch_2, now, main_0.double_0, eventPing.Point.X, eventPing.Point.Y, main_0.int_13, main_0.int_14, int_16, int_17, int_18, int_19);
            }
            dateTime_3 = now;
            method_229(spriteBatch_2, int_16, int_17, int_18, int_19);
            System.Drawing.Color.FromArgb(int_15, 0, 0, 255);
            System.Drawing.Color.FromArgb(int_15, 96, 96, 255);
            System.Drawing.Color color2 = System.Drawing.Color.FromArgb(int_15, 0, 0, 255);
            System.Drawing.Color.FromArgb(int_15, 96, 96, 255);
            System.Drawing.Color color3 = System.Drawing.Color.FromArgb(int_15, 192, 192, 0);
            System.Drawing.Color.FromArgb(int_15, 192, 192, 96);
            System.Drawing.Color color4 = System.Drawing.Color.FromArgb(int_15, 255, 0, 0);
            System.Drawing.Color.FromArgb(int_15, 255, 96, 96);
            DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
            DiplomaticRelation[] array3 = empire.DiplomaticRelations.ToArray();
            foreach (DiplomaticRelation diplomaticRelation in array3)
            {
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            if (main_0.double_0 <= main_0.double_5 && !flag)
            {
                for (int num63 = num12; num63 <= num13; num63++)
                {
                    for (int num64 = num14; num64 <= num15; num64++)
                    {
                        if (galaxy_0.BuiltObjectIndex.Length <= num63 || galaxy_0.BuiltObjectIndex[num63].Length <= num64)
                        {
                            continue;
                        }
                        BuiltObject[] array4 = ListHelper.ToArrayThreadSafe(galaxy_0.BuiltObjectIndex[num63][num64]);
                        for (int num65 = 0; num65 < array4.Length; num65++)
                        {
                            BuiltObject builtObject = null;
                            if (array4.Length > num65)
                            {
                                builtObject = array4[num65];
                                if (builtObject == null || builtObject.HasBeenDestroyed)
                                {
                                    builtObject = null;
                                }
                            }
                            if (builtObject == null)
                            {
                                continue;
                            }
                            bool flag29 = false;
                            bool flag30 = false;
                            bool flag31 = false;
                            if (builtObject.Empire == empire)
                            {
                                _ = builtObject.Empire.MainColor;
                            }
                            else if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                            {
                                _ = builtObject.Empire.MainColor;
                                flag30 = true;
                                if (builtObject.Empire.MainColor == System.Drawing.Color.FromArgb(1, 1, 1))
                                {
                                    System.Drawing.Color.FromArgb(48, 48, 48);
                                }
                            }
                            else if (empireList.Contains(builtObject.Empire))
                            {
                                _ = builtObject.Empire.MainColor;
                                flag30 = true;
                            }
                            else if (builtObject.Empire != null)
                            {
                                _ = builtObject.Empire.MainColor;
                            }
                            switch (builtObject.SubRole)
                            {
                                case BuiltObjectSubRole.Escort:
                                case BuiltObjectSubRole.Frigate:
                                case BuiltObjectSubRole.Destroyer:
                                case BuiltObjectSubRole.Cruiser:
                                case BuiltObjectSubRole.CapitalShip:
                                case BuiltObjectSubRole.TroopTransport:
                                case BuiltObjectSubRole.Carrier:
                                    flag29 = flag5;
                                    if (flag30 && !flag29 && flag13)
                                    {
                                        flag29 = true;
                                    }
                                    if (flag31 && !flag29 && flag14)
                                    {
                                        flag29 = true;
                                    }
                                    break;
                                case BuiltObjectSubRole.ResupplyShip:
                                    flag29 = flag4;
                                    if (flag30 && !flag29 && flag13)
                                    {
                                        flag29 = true;
                                    }
                                    break;
                                case BuiltObjectSubRole.ExplorationShip:
                                    flag29 = flag8;
                                    break;
                                case BuiltObjectSubRole.ColonyShip:
                                    flag29 = flag9;
                                    break;
                                case BuiltObjectSubRole.ConstructionShip:
                                    flag29 = flag10;
                                    break;
                                case BuiltObjectSubRole.SmallFreighter:
                                case BuiltObjectSubRole.MediumFreighter:
                                case BuiltObjectSubRole.LargeFreighter:
                                case BuiltObjectSubRole.PassengerShip:
                                case BuiltObjectSubRole.GasMiningShip:
                                case BuiltObjectSubRole.MiningShip:
                                    flag29 = flag11;
                                    break;
                                case BuiltObjectSubRole.SmallSpacePort:
                                case BuiltObjectSubRole.MediumSpacePort:
                                case BuiltObjectSubRole.LargeSpacePort:
                                    flag29 = flag6;
                                    break;
                                case BuiltObjectSubRole.GasMiningStation:
                                case BuiltObjectSubRole.MiningStation:
                                case BuiltObjectSubRole.ResortBase:
                                case BuiltObjectSubRole.GenericBase:
                                case BuiltObjectSubRole.EnergyResearchStation:
                                case BuiltObjectSubRole.WeaponsResearchStation:
                                case BuiltObjectSubRole.HighTechResearchStation:
                                case BuiltObjectSubRole.MonitoringStation:
                                case BuiltObjectSubRole.DefensiveBase:
                                    flag29 = flag7;
                                    break;
                            }
                            bool flag32 = false;
                            if (flag29)
                            {
                                if (builtObject.Empire == empire)
                                {
                                    flag32 = true;
                                }
                                else if (empire.IsObjectVisibleToThisEmpireImprecise(builtObject))
                                {
                                    flag32 = true;
                                }
                            }
                            if (builtObject.ShipGroup != null && builtObject.ShipGroup.LeadShip == builtObject)
                            {
                                flag32 = false;
                            }
                            if (!flag32)
                            {
                                continue;
                            }
                            int num66 = (int)((builtObject.Xpos - (double)int_16) / double_15);
                            int num67 = (int)((builtObject.Ypos - (double)int_18) / double_15);
                            if (num66 < 0 || num66 > int_13 || num67 < 0 || num67 > int_14)
                            {
                                continue;
                            }
                            if (flag16 || flag17)
                            {
                                bool flag33 = false;
                                flag33 = ((builtObject.Owner == null) ? flag17 : flag16);
                                if (flag33 && builtObject.ActualEmpire == empire)
                                {
                                    if (SpecialHighlightBuiltObjects.Count > 0 && SpecialHighlightBuiltObjects.Contains(builtObject))
                                    {
                                        method_253(spriteBatch_2, builtObject, num66, num67, int_16, int_18, double_15, bool_13: false, System.Drawing.Color.FromArgb(255, 0, 0), 2);
                                    }
                                    else
                                    {
                                        method_251(spriteBatch_2, builtObject, num66, num67, int_16, int_18, double_15, bool_13: false);
                                    }
                                }
                            }
                            int num68 = 10;
                            if (double_15 < 400.0)
                            {
                                num68 = (int)((double)num68 * Math.Sqrt(400.0 / double_15));
                            }
                            if (double_15 > 4000.0)
                            {
                                num68 = (int)((double)num68 / (double_15 / 4000.0));
                            }
                            if (builtObject.Owner == null)
                            {
                                System.Drawing.Color.FromArgb(160, 160, 160);
                            }
                            if (num68 < 6)
                            {
                                num68 = 6;
                            }
                            int num69 = 12;
                            if (builtObject.Role == BuiltObjectRole.Base)
                            {
                                num69 = 15;
                            }
                            if (num68 > num69)
                            {
                                num68 = num69;
                            }
                            if (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                DrawShipSymbolXna(spriteBatch_2, builtObject, ResolveShipSymbolColor(builtObject), num66 - num68 / 2, num67 - num68 / 2, num68, num68, num68, num68, galaxyLevel: true, currentDateTime);
                                if (main_0._Game.SelectedObject == builtObject)
                                {
                                    num68 = (int)((double)num68 * 1.6);
                                    method_212(spriteBatch_2, num66 - num68 / 2, num67 - num68 / 2, num66 + num68 / 2, num67 + num68 / 2);
                                }
                                else if (main_0._Game.SelectedObject is BuiltObjectList)
                                {
                                    BuiltObjectList builtObjectList = (BuiltObjectList)main_0._Game.SelectedObject;
                                    if (builtObjectList.Contains(builtObject))
                                    {
                                        num68 = (int)((double)num68 * 1.6);
                                        method_212(spriteBatch_2, num66 - num68 / 2, num67 - num68 / 2, num66 + num68 / 2, num67 + num68 / 2);
                                    }
                                }
                            }
                            if ((!flag30 && galaxy_0.FastTestShipInColonizedSystem(builtObject)) || builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                continue;
                            }
                            DrawShipSymbolXna(spriteBatch_2, builtObject, ResolveShipSymbolColor(builtObject), num66 - num68 / 2, num67 - num68 / 2, num68, num68, num68, num68, galaxyLevel: true, currentDateTime);
                            if (main_0._Game.SelectedObject == builtObject)
                            {
                                num68 = (int)((double)num68 * 1.6);
                                method_212(spriteBatch_2, num66 - num68 / 2, num67 - num68 / 2, num66 + num68 / 2, num67 + num68 / 2);
                            }
                            else if (main_0._Game.SelectedObject is BuiltObjectList)
                            {
                                BuiltObjectList builtObjectList2 = (BuiltObjectList)main_0._Game.SelectedObject;
                                if (builtObjectList2.Contains(builtObject))
                                {
                                    num68 = (int)((double)num68 * 1.6);
                                    method_212(spriteBatch_2, num66 - num68 / 2, num67 - num68 / 2, num66 + num68 / 2, num67 + num68 / 2);
                                }
                            }
                        }
                    }
                }
            }
            if (main_0._Game.SelectedObject != null && !flag)
            {
                BuiltObject builtObject2 = null;
                double num70 = 0.0;
                if (main_0._Game.SelectedObject is BuiltObject)
                {
                    builtObject2 = (BuiltObject)main_0._Game.SelectedObject;
                    num70 = builtObject2.CurrentRange();
                }
                else if (main_0._Game.SelectedObject is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)main_0._Game.SelectedObject;
                    builtObject2 = shipGroup.LeadShip;
                    num70 = shipGroup.CurrentRange();
                }
                //BaconMain.DrawGravityWellRange(spriteBatch_2, int_16, int_18, double_15);
                MainView.OnDrawGravityWellRangeMods(spriteBatch_2, int_16, int_18, double_15);
                if (builtObject2 != null && builtObject2.Role != BuiltObjectRole.Base && builtObject2.ActualEmpire == empire)
                {
                    double num71 = builtObject2.Xpos - num70;
                    double num72 = builtObject2.Ypos - num70;
                    float num73 = (float)((num71 - (double)int_16) / double_15);
                    float num74 = (float)((num72 - (double)int_18) / double_15);
                    float num75 = (float)(num70 / double_15 * 2.0);
                    if ((double)num73 < (double)Galaxy.SizeX)
                    {
                        new RectangleF((float)num70, num74, num75, num75);
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, num73, num74, num75, num75, main_0.VuqPpUtdZU, 1, 200, dashed: true);
                    }
                    int int_20 = (int)((builtObject2.Xpos - (double)int_16) / double_15);
                    int int_21 = (int)((builtObject2.Ypos - (double)int_18) / double_15);
                    method_252(spriteBatch_2, builtObject2, int_20, int_21, int_16, int_18, double_15, bool_13: true, System.Drawing.Color.Yellow);
                }
            }
            if (double_15 > num16 && !flag)
            {
                BuiltObject[] array5 = ListHelper.ToArrayThreadSafe(empire.KnownPirateBases);
                if (main_0._Game.GodMode)
                {
                    BuiltObjectList builtObjectList3 = new BuiltObjectList();
                    for (int num76 = 0; num76 < galaxy_0.PirateEmpires.Count; num76++)
                    {
                        Empire empire4 = galaxy_0.PirateEmpires[num76];
                        if (empire4.PirateEmpireBaseHabitat != null && empire4.PirateEmpireBaseHabitat.BasesAtHabitat != null && empire4.PirateEmpireBaseHabitat.BasesAtHabitat.Count > 0)
                        {
                            builtObjectList3.Add(empire4.PirateEmpireBaseHabitat.BasesAtHabitat[0]);
                        }
                    }
                    array5 = ListHelper.ToArrayThreadSafe(builtObjectList3);
                }
                foreach (BuiltObject builtObject3 in array5)
                {
                    if (builtObject3 == null || builtObject3.HasBeenDestroyed)
                    {
                        continue;
                    }
                    int num78 = (int)((builtObject3.Xpos - (double)int_16) / double_15);
                    int num79 = (int)((builtObject3.Ypos - (double)int_18) / double_15);
                    if (num78 < 0 || num78 > int_13 || num79 < 0 || num79 > int_14)
                    {
                        continue;
                    }
                    int num80 = 27;
                    int num81 = 16;
                    if (double_15 < 600.0)
                    {
                        num80 = 35;
                        num81 = 21;
                    }
                    else if (double_15 > 4000.0)
                    {
                        num80 = 20;
                        num81 = 12;
                    }
                    System.Drawing.Rectangle rectangle3 = new System.Drawing.Rectangle(num78 - num80 / 2, num79 - num81 / 2, num80, num81);
                    if (builtObject3.Empire != null && builtObject3.Empire.MediumFlagPicture != null)
                    {
                        if (texture2D_21.Length > builtObject3.Empire.EmpireId)
                        {
                            Texture2D texture2D6 = texture2D_21[builtObject3.Empire.EmpireId];
                            if (texture2D6 == null || texture2D6.IsDisposed)
                            {
                                texture2D6 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, builtObject3.Empire.MediumFlagPicture);
                                texture2D_21[builtObject3.Empire.EmpireId] = texture2D6;
                            }
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D6, rectangle3, 0f);
                        }
                        else
                        {
                            Texture2D texture2D7 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, builtObject3.Empire.MediumFlagPicture);
                            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D7, rectangle3, 0f);
                            method_22(texture2D7);
                        }
                    }
                    else
                    {
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_22, rectangle3, 0f);
                    }
                    System.Drawing.Color color5 = System.Drawing.Color.FromArgb(80, 80, 80);
                    XnaDrawingHelper.DrawRectangle(spriteBatch_2, rectangle3, color5, 1);
                }
                int num82 = 22;
                if (double_15 < 400.0)
                {
                    num82 = (int)((double)num82 * Math.Sqrt(400.0 / double_15));
                }
                if (double_15 > 4000.0)
                {
                    num82 = (int)((double)num82 / (double_15 / 4000.0));
                }
                if (num82 < 12)
                {
                    num82 = 12;
                }
                SolidBrush solidBrush3 = new SolidBrush(color2);
                if (empire != null)
                {
                    if (flag3)
                    {
                        method_258(spriteBatch_2, solidBrush3, empire, double_15, num82, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                    }
                    SolidBrush solidBrush4 = new SolidBrush(color3);
                    SolidBrush solidBrush5 = new SolidBrush(color4);
                    Empire[] array6 = ListHelper.ToArrayThreadSafe(galaxy_0.Empires);
                    foreach (Empire empire5 in array6)
                    {
                        if (empire5 == empire)
                        {
                            continue;
                        }
                        if (empireList.Contains(empire5))
                        {
                            if (flag3 || flag12)
                            {
                                method_258(spriteBatch_2, solidBrush5, empire5, double_15, num82, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                            }
                        }
                        else if (flag3)
                        {
                            method_258(spriteBatch_2, solidBrush4, empire5, double_15, num82, int_13, int_14, int_16, int_17, int_18, int_19, empire);
                        }
                    }
                    solidBrush4?.Dispose();
                    solidBrush5?.Dispose();
                }
                solidBrush3?.Dispose();
            }
            solidBrush?.Dispose();
        }

        private void method_251(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13)
        {
            method_252(spriteBatch_2, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, System.Drawing.Color.FromArgb(170, 170, 170));
        }

        private void method_252(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, System.Drawing.Color color_18)
        {
            method_253(spriteBatch_2, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, color_18, 1);
        }

        private void method_253(SpriteBatch spriteBatch_2, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, System.Drawing.Color color_18, int int_15)
        {
            //BaconMainView.method_253(this, spriteBatch_2, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, color_18, int_15);
            MainView.OnDrawPathLineMods(this, spriteBatch_2, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, color_18, int_15);
        }

        private void method_254(Graphics graphics_0, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13)
        {
            method_255(graphics_0, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, pen_2);
        }

        private void method_255(Graphics graphics_0, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, Pen pen_11)
        {
            if (builtObject_1 != null && !builtObject_1.HasBeenDestroyed && builtObject_1.Role != BuiltObjectRole.Base && builtObject_1.TopSpeed > 0 && builtObject_1.WarpSpeed > 0 && (builtObject_1.CurrentSpeed > (float)builtObject_1.TopSpeed || builtObject_1.HyperjumpPrepare) && builtObject_1.Mission != null && builtObject_1.Mission.Type != 0 && (bool_13 || builtObject_1.ShipGroup == null || builtObject_1.ShipGroup.LeadShip == builtObject_1))
            {
                System.Drawing.Point point = builtObject_1.Mission.ResolveTargetCoordinatesCurrentCommand();
                int x = (int)(((double)point.X - (double)int_13) / double_15);
                int y = (int)(((double)point.Y - (double)int_14) / double_15);
                double num = Math.Abs((double)point.X - builtObject_1.Xpos) + Math.Abs((double)point.Y - builtObject_1.Ypos);
                num *= 1500.0 / double_15;
                if (num > 40000.0)
                {
                    graphics_0.DrawLine(pen_11, int_11, int_12, x, y);
                }
            }
        }

        private void method_256(Graphics graphics_0, SolidBrush solidBrush_21, Empire empire_0, double double_15, int int_11, int int_12, int int_13, double double_16, double double_17, double double_18, double double_19, Empire empire_1)
        {
            using SolidBrush brush_ = new SolidBrush(empire_0.MainColor);
            for (int i = 0; i < empire_0.ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = empire_0.ShipGroups[i];
                if (!empire_1.IsObjectVisibleToThisEmpire(shipGroup.LeadShip))
                {
                    continue;
                }
                int num = (int)((shipGroup.LeadShip.Xpos - double_16) / double_15);
                int num2 = (int)((shipGroup.LeadShip.Ypos - double_18) / double_15);
                if (num < 0 || num > int_12 || num2 < 0 || num2 > int_13)
                {
                    continue;
                }
                if (main_0.gameOptions_0 != null && main_0.gameOptions_0.MapOverlayTravelVectorsState)
                {
                    method_254(graphics_0, shipGroup.LeadShip, num, num2, (int)double_16, (int)double_18, double_15, bool_13: false);
                }
                int num3 = empire_1.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(shipGroup);
                if (num3 >= 0)
                {
                    FleetAttack fleetAttack = empire_1.IncomingEnemyFleetsAndPlanetDestroyers[num3];
                    bool flag = true;
                    if (fleetAttack.Fleet.Mission != null && (fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.Attack || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.WaitAndAttack || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.Bombard || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.WaitAndBombard))
                    {
                        if (BuiltObjectMission.ResolveMissionTargetEmpire(fleetAttack.Fleet.Mission) != empire_1)
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        using Pen pen = new Pen(System.Drawing.Color.FromArgb(128, 255, 0, 0), 2f);
                        float num4 = 7f;
                        float num5 = 12f;
                        if (double_15 > 5000.0)
                        {
                            num4 = 4f;
                            num5 = 7f;
                        }
                        using AdjustableArrowCap customEndCap = new AdjustableArrowCap(num4, num5);
                        pen.EndCap = LineCap.Custom;
                        pen.CustomEndCap = customEndCap;
                        pen.DashStyle = DashStyle.Dash;
                        System.Drawing.Point point = fleetAttack.Fleet.Mission.ResolveTargetCoordinates(fleetAttack.Fleet.Mission);
                        int x = (int)(((double)point.X - double_16) / double_15);
                        int y = (int)(((double)point.Y - double_18) / double_15);
                        graphics_0.DrawLine(pen, num, num2, x, y);
                    }
                }
                method_257(shipGroup, num, num2, int_11, solidBrush_21, graphics_0);
                if (main_0._Game.SelectedObject == shipGroup)
                {
                    method_210(num - int_11 / 2, num2 - int_11 / 2, num + int_11 / 2, num2 + int_11 / 2, graphics_0);
                }
                if (shipGroup_0 != null && shipGroup == shipGroup_0)
                {
                    System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(num - (int_11 / 2 + 4), num2 - (int_11 / 2 + 4), int_11 + 8, int_11 + 8);
                    method_204(rectangle_, graphics_0);
                }
                Font font = font_1;
                Habitat habitat = galaxy_0.FastFindNearestColony((int)shipGroup.LeadShip.Xpos, (int)shipGroup.LeadShip.Ypos, shipGroup.Empire, 0);
                if (habitat != null)
                {
                    double num6 = galaxy_0.CalculateDistance(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, habitat.Xpos, habitat.Ypos);
                    if (num6 > (double)Galaxy.MaxSolarSystemSize * 2.1)
                    {
                        if (double_15 > 4000.0)
                        {
                            font = font_3;
                        }
                        string name = shipGroup.Name;
                        System.Drawing.Point point_ = new System.Drawing.Point(num - int_11 / 2, num2 - (font.Height + int_11 / 2));
                        method_267(graphics_0, name, font, point_, brush_);
                    }
                }
                else
                {
                    if (double_15 > 4000.0)
                    {
                        font = font_3;
                    }
                    string name2 = shipGroup.Name;
                    System.Drawing.Point point_2 = new System.Drawing.Point(num - int_11 / 2, num2 - (font.Height + int_11 / 2));
                    method_267(graphics_0, name2, font, point_2, brush_);
                }
            }
        }

        private void method_257(ShipGroup shipGroup_1, float float_0, float float_1, int int_11, Brush brush_0, Graphics graphics_0)
        {
            float_0 -= (float)int_11 / 2f;
            float_1 -= (float)int_11 / 3f;
            System.Drawing.Color color = System.Drawing.Color.Gray;
            Pen pen = null;
            SolidBrush solidBrush = null;
            if (shipGroup_1.Empire != null)
            {
                color = shipGroup_1.Empire.MainColor;
                System.Drawing.Color color2 = method_226(color, 64);
                pen = new Pen(color2, 1.5f);
                solidBrush = new SolidBrush(color);
            }
            List<PointF> list = new List<PointF>();
            float num = (float)((double)int_11 / 1.154700538379);
            list.Add(new PointF(float_0, float_1));
            list.Add(new PointF(float_0 + (float)int_11, float_1));
            list.Add(new PointF(float_0 + (float)int_11 / 2f, float_1 + num));
            graphics_0.FillPolygon(solidBrush, list.ToArray());
            graphics_0.DrawPolygon(pen, list.ToArray());
            if (main_0.double_0 < 6000.0)
            {
                System.Drawing.Color color3 = method_263(color);
                string s = shipGroup_1.Ships.Count.ToString();
                float num2 = float_0 + ((float)int_11 - graphics_0.MeasureString(s, font_3, 100, StringFormat.GenericTypographic).Width) / 2f;
                float num3 = float_1 + (float)int_11 * 0.1f;
                graphics_0.DrawString(point: new PointF(num2, num3), s: s, font: font_3, brush: new SolidBrush(color3), format: StringFormat.GenericTypographic);
            }
            pen?.Dispose();
            solidBrush?.Dispose();
        }

        private void method_258(SpriteBatch spriteBatch_2, SolidBrush solidBrush_21, Empire empire_0, double double_15, int int_11, int int_12, int int_13, double double_16, double double_17, double double_18, double double_19, Empire empire_1)
        {
            for (int i = 0; i < empire_0.ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = empire_0.ShipGroups[i];
                if (shipGroup.Empire != empire_1 && !empire_1.IsObjectVisibleToThisEmpire(shipGroup.LeadShip, includeLongRangeScanners: true, includeShipsOutsideSystems: false))
                {
                    continue;
                }
                int num = (int)((shipGroup.LeadShip.Xpos - double_16) / double_15);
                int num2 = (int)((shipGroup.LeadShip.Ypos - double_18) / double_15);
                if (num < 0 || num > int_12 || num2 < 0 || num2 > int_13)
                {
                    continue;
                }
                if (main_0.gameOptions_0 != null && main_0.gameOptions_0.MapOverlayTravelVectorsState)
                {
                    bool flag = false;
                    if (main_0._Game.SelectedObject is ShipGroup)
                    {
                        ShipGroup shipGroup2 = (ShipGroup)main_0._Game.SelectedObject;
                        if (shipGroup2 == shipGroup)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        method_252(spriteBatch_2, shipGroup.LeadShip, num, num2, (int)double_16, (int)double_18, double_15, bool_13: false, System.Drawing.Color.Yellow);
                    }
                    else
                    {
                        method_251(spriteBatch_2, shipGroup.LeadShip, num, num2, (int)double_16, (int)double_18, double_15, bool_13: false);
                    }
                }
                int num3 = empire_1.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(shipGroup);
                if (num3 >= 0)
                {
                    FleetAttack fleetAttack = empire_1.IncomingEnemyFleetsAndPlanetDestroyers[num3];
                    bool flag2 = true;
                    if (fleetAttack.Fleet.Mission != null && (fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.Attack || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.WaitAndAttack || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.Bombard || fleetAttack.Fleet.Mission.Type == BuiltObjectMissionType.WaitAndBombard))
                    {
                        if (BuiltObjectMission.ResolveMissionTargetEmpire(fleetAttack.Fleet.Mission) != empire_1)
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        flag2 = false;
                    }
                    if (flag2)
                    {
                        System.Drawing.Point point = fleetAttack.Fleet.Mission.ResolveTargetCoordinates(fleetAttack.Fleet.Mission);
                        int x = (int)(((double)point.X - double_16) / double_15);
                        int y = (int)(((double)point.Y - double_18) / double_15);
                        XnaDrawingHelper.DrawLine(spriteBatch_2, num, num2, x, y, System.Drawing.Color.FromArgb(128, 255, 0, 0), 2, dashed: true);
                    }
                }
                method_259(spriteBatch_2, shipGroup, num, num2, int_11);
                if (main_0._Game.SelectedObject == shipGroup)
                {
                    method_212(spriteBatch_2, num - int_11 / 2, num2 - int_11 / 2, num + int_11 / 2, num2 + int_11 / 2);
                }
                if (shipGroup_0 != null && shipGroup == shipGroup_0)
                {
                    System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(num - (int_11 / 2 + 4), num2 - (int_11 / 2 + 4), int_11 + 8, int_11 + 8);
                    method_205(spriteBatch_2, rectangle_);
                }
                SpriteFont spriteFont = spriteFont_2;
                bool flag3 = true;
                if (shipGroup.LeadShip != null && shipGroup.LeadShip.NearestSystemStar != null)
                {
                    SystemInfo systemInfo = galaxy_0.Systems[shipGroup.LeadShip.NearestSystemStar];
                    if (systemInfo != null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire == shipGroup.Empire)
                    {
                        flag3 = false;
                    }
                }
                if (flag3)
                {
                    if (double_15 > 4000.0)
                    {
                        spriteFont = spriteFont_0;
                    }
                    string name = shipGroup.Name;
                    System.Drawing.Point point2 = new System.Drawing.Point(num - int_11 / 2, num2 - (spriteFont.LineSpacing + int_11 / 2));
                    System.Drawing.Color foreColor = empire_0.MainColor;
                    if (foreColor.R == 1 && foreColor.G == 1 && foreColor.B == 1)
                    {
                        foreColor = System.Drawing.Color.FromArgb(128, 128, 128);
                    }
                    XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, name, spriteFont, foreColor, point2);
                }
            }
        }

        private void method_259(SpriteBatch spriteBatch_2, ShipGroup shipGroup_1, float float_0, float float_1, int int_11)
        {
            System.Drawing.Color color = System.Drawing.Color.Gray;
            method_226(color, 48);
            if (shipGroup_1.Empire != null)
            {
                color = shipGroup_1.Empire.MainColor;
                method_226(color, 48);
                if (color.R == 1 && color.G == 1 && color.B == 1)
                {
                    color = System.Drawing.Color.FromArgb(8, 8, 8);
                    System.Drawing.Color.FromArgb(48, 48, 48);
                }
            }
            float num = (float)texture2D_52.Width / (float)texture2D_52.Height;
            int num2 = (int)((float)int_11 * num);
            int num3 = (int)float_0 - num2 / 2;
            int num4 = (int)float_1 - int_11 / 2;
            XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num3, num4, num2, int_11), spriteBatch: spriteBatch_2, texture: texture2D_52, rotationAngle: 0f, tintColor: color);
            if (main_0.double_0 < 6000.0)
            {
                System.Drawing.Color color2 = method_263(color);
                string text = shipGroup_1.Ships.Count.ToString();
                float num5 = (float)num3 + ((float)num2 - spriteFont_1.MeasureString(text).X) / 2f;
                float num6 = (float)num4 + (float)int_11 * 0.1f;
                XnaDrawingHelper.DrawString(point: new PointF(num5, num6), spriteBatch: spriteBatch_2, text: text, font: spriteFont_1, color: color2);
            }
        }

        private void method_260(SpriteBatch spriteBatch_2, ShipGroup shipGroup_1, float float_0, float float_1, int int_11)
        {
            float_0 -= (float)int_11 / 2f;
            float_1 -= (float)int_11 / 3f;
            System.Drawing.Color color_ = System.Drawing.Color.Gray;
            System.Drawing.Color color = method_226(color_, 64);
            if (shipGroup_1.Empire != null)
            {
                color_ = shipGroup_1.Empire.MainColor;
                color = method_226(color_, 64);
            }
            List<PointF> list = new List<PointF>();
            float num = (float)((double)int_11 / 1.154700538379);
            list.Add(new PointF(float_0, float_1));
            list.Add(new PointF(float_0 + (float)int_11, float_1));
            list.Add(new PointF(float_0 + (float)int_11 / 2f, float_1 + num));
            XnaDrawingHelper.DrawPolygon(spriteBatch_2, list.ToArray(), color, 2);
            if (main_0.double_0 < 6000.0)
            {
                System.Drawing.Color color2 = method_263(color_);
                string text = shipGroup_1.Ships.Count.ToString();
                float num2 = float_0 + ((float)int_11 - spriteFont_0.MeasureString(text).X) / 2f;
                float num3 = float_1 + (float)int_11 * 0.1f;
                XnaDrawingHelper.DrawString(point: new PointF(num2, num3), spriteBatch: spriteBatch_2, text: text, font: spriteFont_0, color: color2);
            }
        }

        private void method_261(Graphics graphics_0, int int_11, int int_12, GalaxyLocation galaxyLocation_0, double double_15)
        {
            if (!(galaxyLocation_0.EffectAmount > 0.0) && Galaxy.Rnd.Next(0, 60) != 1)
            {
                return;
            }
            TimeSpan timeSpan = new TimeSpan(galaxy_0.CurrentDateTime.Ticks);
            if (galaxyLocation_0.EffectAmount <= 0.0)
            {
                galaxyLocation_0.EffectAmount = timeSpan.TotalMilliseconds;
                galaxyLocation_0.EffectRandomSeed = Galaxy.Rnd.Next(0, 50000);
            }
            double num = timeSpan.TotalMilliseconds - galaxyLocation_0.EffectAmount;
            if (num > 500.0)
            {
                galaxyLocation_0.EffectAmount = 0.0;
                return;
            }
            double double_16 = 1.0 - num % 250.0 / 250.0 * 0.95;
            Random random = new Random(galaxyLocation_0.EffectRandomSeed);
            double num2 = (double)galaxyLocation_0.Width / double_15;
            double num3 = (double)galaxyLocation_0.Height / double_15;
            double num4 = num2 / 2.5;
            double num5 = num2 * 0.15;
            double num6 = num3 * 0.15;
            double num7 = num2 * 0.3 * random.NextDouble();
            double num8 = num3 * 0.3 * random.NextDouble();
            using Bitmap bitmap = lightningGenerator_0.GenerateLightning(galaxyLocation_0.EffectRandomSeed, (int)num4);
            int num9 = (int)((double)int_11 - num2 / 2.0 + num5 + num7);
            int num10 = (int)((double)int_12 - num3 / 2.0 + num6 + num8);
            using ImageAttributes imageAttrs = method_236(double_16);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(num9, num10, bitmap.Width, bitmap.Height);
            graphics_0.DrawImage(bitmap, destRect, 0f, 0f, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttrs);
        }

        private void method_262(SpriteBatch spriteBatch_2, int int_11, int int_12, GalaxyLocation galaxyLocation_0, double double_15)
        {
            if (!(galaxyLocation_0.EffectAmount > 0.0) && Galaxy.Rnd.Next(0, 200) != 1)
            {
                return;
            }
            TimeSpan timeSpan = new TimeSpan(galaxy_0.CurrentDateTime.Ticks);
            if (galaxyLocation_0.EffectAmount <= 0.0)
            {
                galaxyLocation_0.EffectAmount = timeSpan.TotalMilliseconds;
                galaxyLocation_0.EffectRandomSeed = Galaxy.Rnd.Next(0, 50000);
            }
            double num = timeSpan.TotalMilliseconds - galaxyLocation_0.EffectAmount;
            if (num > 500.0)
            {
                galaxyLocation_0.EffectAmount = 0.0;
                return;
            }
            double num2 = 1.0 - num % 250.0 / 250.0 * 0.95;
            Random random = new Random(galaxyLocation_0.EffectRandomSeed);
            double num3 = (double)galaxyLocation_0.Width / double_15;
            double num4 = (double)galaxyLocation_0.Height / double_15;
            double num5 = num3 / 2.5;
            double num6 = num3 * 0.15;
            double num7 = num4 * 0.15;
            double num8 = num3 * 0.3 * random.NextDouble();
            double num9 = num4 * 0.3 * random.NextDouble();
            using Bitmap bitmap = lightningGenerator_0.GenerateLightning(galaxyLocation_0.EffectRandomSeed, (int)num5);
            int num10 = (int)((double)int_11 - num3 / 2.0 + num6 + num8);
            int num11 = (int)((double)int_12 - num4 / 2.0 + num7 + num9);
            Texture2D texture2D = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap);
            System.Drawing.Color tintColor = System.Drawing.Color.FromArgb((int)(num2 * 255.0), 255, 255, 255);
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(num10, num11, bitmap.Width, bitmap.Height);
            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, destination, 0f, tintColor);
            method_22(texture2D);
        }

        private System.Drawing.Color method_263(System.Drawing.Color color_18)
        {
            System.Drawing.Color gray = System.Drawing.Color.Gray;
            int num = (color_18.R + color_18.G + color_18.B) / 3;
            if (num > 127)
            {
                return System.Drawing.Color.Black;
            }
            return System.Drawing.Color.White;
        }

        private void method_264(BuiltObjectRole builtObjectRole_0, int int_11, int int_12, int int_13, Brush brush_0, Pen pen_11, Graphics graphics_0)
        {
            int_11 -= int_13 / 2;
            int_12 -= int_13 / 2;
            graphics_0.FillEllipse(brush_0, int_11, int_12, int_13, int_13);
            graphics_0.DrawEllipse(pen_11, int_11, int_12, int_13, int_13);
            float num = int_11;
            float num2 = int_12;
            float num3 = int_13;
            float num4 = num3 / 2f;
            float num5 = num3 * 0.17f;
            float num6 = num3 * 0.83f;
            switch (builtObjectRole_0)
            {
                case BuiltObjectRole.Military:
                    graphics_0.FillRectangle(new SolidBrush(pen_11.Color), num + num4 - 1f, num2 + num4 - 1f, 2f, 2f);
                    break;
                case BuiltObjectRole.Exploration:
                    graphics_0.DrawLine(pen_11, num + num5, num2 + num5, num + num6, num2 + num6);
                    break;
                case BuiltObjectRole.Colony:
                    graphics_0.DrawLine(pen_11, num, num2 + num4, num + num3, num2 + num4);
                    break;
                case BuiltObjectRole.Build:
                    graphics_0.DrawLine(pen_11, num + num5, num2 + num5, num + num6, num2 + num6);
                    graphics_0.DrawLine(pen_11, num + num5, num2 + num6, num + num6, num2 + num5);
                    break;
                case BuiltObjectRole.Freight:
                case BuiltObjectRole.Passenger:
                case BuiltObjectRole.Resource:
                    break;
                case BuiltObjectRole.Base:
                    graphics_0.DrawLine(pen_11, num + num4, num2, num + num4, num2 + num3);
                    graphics_0.DrawLine(pen_11, num, num2 + num4, num + num3, num2 + num4);
                    break;
            }
        }

        private Bitmap method_265(SystemInfo systemInfo_1, int int_11, System.Drawing.Rectangle rectangle_5)
        {
            int num = 0;
            int num2 = 0;
            if (systemInfo_1.DominantEmpire != null)
            {
                num = systemInfo_1.DominantEmpire.Empire.LargeFlagPicture.Width;
                num2 = systemInfo_1.DominantEmpire.Empire.LargeFlagPicture.Height;
            }
            Bitmap bitmap = new Bitmap(num, num, PixelFormat.Format32bppPArgb);
            using (GraphicsPath graphicsPath = new GraphicsPath())
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, num - 1, num - 1);
                graphicsPath.AddEllipse(rect);
                using (new Region(graphicsPath))
                {
                    Bitmap bitmap2 = new Bitmap(num, num, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics = Graphics.FromImage(bitmap2))
                    {
                        SolidBrush brush = new SolidBrush(systemInfo_1.DominantEmpire.Empire.SecondaryColor);
                        System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(0, 0, num, num);
                        graphics.FillRectangle(brush, rect2);
                        graphics.DrawImage(systemInfo_1.DominantEmpire.Empire.LargeFlagPicture, 0, (num - num2) / 2, num, num2);
                        if (int_11 < 255)
                        {
                            bitmap2 = method_16(bitmap2, (float)int_11 / 255f);
                        }
                    }
                    TextureBrush brush2 = new TextureBrush(bitmap2);
                    using Graphics graphics2 = Graphics.FromImage(bitmap);
                    graphics2.InterpolationMode = InterpolationMode.High;
                    graphics2.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics2.FillPath(brush2, graphicsPath);
                }
            }
            return main_0.PrecacheScaledBitmap(bitmap, rectangle_5.Width, rectangle_5.Height);
        }

        private void method_266(Graphics graphics_0, string string_0, Font font_4, System.Drawing.Point point_5)
        {
            method_267(graphics_0, string_0, font_4, point_5, solidBrush_0);
        }

        private void method_267(Graphics graphics_0, string string_0, Font font_4, System.Drawing.Point point_5, Brush brush_0)
        {
            point_5 = new System.Drawing.Point(point_5.X + 1, point_5.Y + 1);
            graphics_0.DrawString(string_0, font_4, solidBrush_2, point_5);
            point_5 = new System.Drawing.Point(point_5.X - 1, point_5.Y - 1);
            graphics_0.DrawString(string_0, font_4, brush_0, point_5);
        }

        private Pen method_268(Empire empire_0, bool bool_13, SystemInfo systemInfo_1, int int_11)
        {
            Pen pen = null;
            SystemVisibilityStatus systemVisibilityStatus = empire_0.CheckSystemVisibilityStatus(systemInfo_1.SystemStar.SystemIndex);
            if (!bool_13 && systemInfo_1.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible || main_0._Game.GodMode))
            {
                System.Drawing.Color color = System.Drawing.Color.FromArgb(int_11, systemInfo_1.DominantEmpire.Empire.MainColor.R, systemInfo_1.DominantEmpire.Empire.MainColor.G, systemInfo_1.DominantEmpire.Empire.MainColor.B);
                pen = new Pen(color, 3f);
                if (systemInfo_1.IsDisputed)
                {
                    pen.DashStyle = DashStyle.Dash;
                }
            }
            else
            {
                switch (systemVisibilityStatus)
                {
                    case SystemVisibilityStatus.Unexplored:
                        pen = new Pen(System.Drawing.Color.FromArgb(int_11, 60, 60, 120), 1.5f);
                        break;
                    case SystemVisibilityStatus.Explored:
                    case SystemVisibilityStatus.Visible:
                        pen = new Pen(System.Drawing.Color.FromArgb(int_11, 112, 112, 112), 1.5f);
                        break;
                }
            }
            return pen;
        }

        private SolidBrush method_269(Empire empire_0, bool bool_13, SystemInfo systemInfo_1, int int_11)
        {
            SolidBrush result = null;
            SystemVisibilityStatus systemVisibilityStatus = empire_0.CheckSystemVisibilityStatus(systemInfo_1.SystemStar.SystemIndex);
            if (!bool_13 && systemInfo_1.DominantEmpire != null && (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible || main_0._Game.GodMode))
            {
                System.Drawing.Color color = System.Drawing.Color.FromArgb(int_11, systemInfo_1.DominantEmpire.Empire.MainColor.R, systemInfo_1.DominantEmpire.Empire.MainColor.G, systemInfo_1.DominantEmpire.Empire.MainColor.B);
                result = new SolidBrush(color);
            }
            else
            {
                switch (systemVisibilityStatus)
                {
                    case SystemVisibilityStatus.Unexplored:
                        result = new SolidBrush(System.Drawing.Color.FromArgb(int_11, 48, 48, 48));
                        break;
                    case SystemVisibilityStatus.Explored:
                        result = new SolidBrush(System.Drawing.Color.FromArgb(int_11, 112, 112, 112));
                        break;
                    case SystemVisibilityStatus.Visible:
                        result = new SolidBrush(System.Drawing.Color.FromArgb(int_11, 112, 112, 112));
                        break;
                }
            }
            return result;
        }

        public static SamplerState[] SwitchSamplerStatesToPointClamp(GraphicsDevice graphics)
        {
            SamplerState[] array = new SamplerState[16];
            for (int i = 0; i < 16; i++)
            {
                array[i] = graphics.SamplerStates[i];
                graphics.SamplerStates[i] = SamplerState.PointClamp;
                graphics.SamplerStates[i] = SamplerState.LinearClamp;
                graphics.SamplerStates[i] = SamplerState.PointClamp;
            }
            return array;
        }

        public static void RevertSamplerStates(GraphicsDevice graphics, SamplerState[] previousSamplerStates)
        {
            for (int i = 0; i < 16; i++)
            {
                if (previousSamplerStates[i] == null)
                {
                    graphics.SamplerStates[i] = SamplerState.PointClamp;
                }
                else
                {
                    graphics.SamplerStates[i] = previousSamplerStates[i];
                }
            }
        }

        private static double OnMinZoomLevelForWeaponsCirclesMods()
        {
            var tmp = MainView.MinZoomLevelForWeaponsCirclesMods;
            double res = 0.9;
            if (tmp != null)
            {
                var args = new ReturnDoubleModsArgs();
                tmp(null, args);
                res = args.Result;
            }
            return res;
        }
        private static double OnBackgroundStarsAtZoomLevelMods()
        {
            var tmp = MainView.BackgroundStarsAtZoomLevelMods;
            double res = 0.9;
            if (tmp != null)
            {
                var args = new ReturnDoubleModsArgs();
                tmp(null, args);
                res = args.Result;
            }
            return res;
        }
        private static void OnDrawWeaponRangesMods(SpriteBatch spriteBatch_2,      int num14,      int num16,      double zoomLevel)
        {
            var tmp = MainView.DrawWeaponRanges;
            if (tmp != null)
            {
                var args = new DrawRangeModsArgs(spriteBatch_2, num14, num16, zoomLevel);
                tmp(null, args);
            }
        }
        private static void OnDrawGravityWellRangeMods(SpriteBatch spriteBatch_2, int num14, int num16, double zoomLevel)
        {
            var tmp = MainView.DrawGravityWellRange;
            if (tmp != null)
            {
                var args = new DrawRangeModsArgs(spriteBatch_2, num14, num16, zoomLevel);
                tmp(null, args);
            }
        }
        private static void OnDrawPathLineMods(MainView mainview,
                                                      SpriteBatch spriteBatch_2,
                                                      BuiltObject builtObject_1,
                                                      int int_11,
                                                      int int_12,
                                                      int int_13,
                                                      int int_14,
                                                      double double_15,
                                                      bool bool_13,
                                                      System.Drawing.Color color_18,
                                                      int int_15)
        {
            var tmp = MainView.DrawPathLineMods;
            if (tmp != null)
            {
                var args = new DrawPathLineModsArgs(mainview, spriteBatch_2, builtObject_1, int_11, int_12, int_13, int_14, double_15, bool_13, color_18, int_15);
                tmp(null, args);
            }
        }
    }
}
