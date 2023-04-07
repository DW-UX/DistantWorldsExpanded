// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.InfoPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

//using BaconDistantWorlds;
using DistantWorlds.Types;
using DistantWorlds.Controls.Mods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class InfoPanel : Panel
    {
        private bool _ContentSizeIsLarge;
        private Font _NormalFontNormalSize;
        private Font _NormalBoldFontNormalSize;
        private Font _TitleFontNormalSize;
        private Font _TinyFontNormalSize;
        private Font _NormalFontLargeSize;
        private Font _NormalBoldFontLargeSize;
        private Font _TitleFontLargeSize;
        private Font _TinyFontLargeSize;
        private Font _TinyFont;
        public Font _NormalFont;
        public Font _NormalFontBold;
        public Font _TitleFont;
        private int _ImageSize = 14;
        private int _ImageScaleSize = 24;
        private int _HabitatImageSize = 26;
        public int _RowHeight = 15;
        public Size _FlagSizeSmall = new Size(30, 18);
        private Size _FlagSizeSystem = new Size(20, 12);
        private int _MinPictureSize = 60;
        private int _MaxPictureSize = 200;
        private int _RuinImageHeight = 18;
        public int _MaxGraphTextWidth = 200;
        private int _PopulationTextWidth = 75;
        private int _PopulationAmountWidth = 48;
        private int _LabelWidth = 65;
        private int _LabelWidthHabitat = 70;
        public int _Height2 = 2;
        public int _Height3 = 3;
        public int _Height4 = 4;
        public int _Height5 = 5;
        private int _Height6 = 6;
        private int _Height8 = 8;
        private int _Height10 = 10;
        private int _ColonySummaryDetailWidth = 20;
        private Bitmap _Picture;
        private Bitmap _MaskImage;
        private int _PictureSize;
        private double _PictureAngle;
        public Bitmap _EmpirePicture;
        private Color _BackColour1 = Color.FromArgb(39, 40, 44);
        private Color _BackColour2 = Color.FromArgb(22, 21, 26);
        private Color _BackColour3 = Color.FromArgb(51, 54, 61);
        private LinearGradientMode _GradientMode = LinearGradientMode.Vertical;
        private BorderStyle _BorderStyle;
        private Color _BorderColour = Color.FromArgb(67, 67, 77);
        private int _BorderWidth = 1;
        private int _Curvature;
        private CornerCurveMode _CurveMode = CornerCurveMode.All;
        public Game _Game;
        public Galaxy _Galaxy;
        public BuiltObject _BuiltObject;
        private Fighter _Fighter;
        private Habitat _Habitat;
        private Creature _Creature;
        private BuiltObjectList _BuiltObjects;
        private ShipGroup _ShipGroup;
        private SystemInfo _SystemInfo;
        public EmpireActivity _SmugglingMission;
        public static CharacterImageCache _CharacterImageCache;
        public bool _BuiltObjectIsScanned;
        public DateTime _LastBuiltObjectScanTime = DateTime.MinValue;
        public Empire _ActualEmpire;
        private HotspotList _Hotspots = new HotspotList();
        private bool _AddHotspots;
        private Pen _HotspotPen = new Pen(Color.FromArgb(170, 170, 170), 1f);
        public bool _ShowExtendedInfo;
        public Color _EmpireColor = Color.Empty;
        private Color _WhiteColor = Color.FromArgb(200, 200, 200);
        public SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(200, 200, 200));
        public SolidBrush _BlackBrush = new SolidBrush(Color.Black);
        public SolidBrush _RedBrush = new SolidBrush(Color.Red);
        public SolidBrush _LabelAreaBrush = new SolidBrush(Color.FromArgb(63, (int)sbyte.MaxValue, 32, 32));
        private Pen _BlackPen = new Pen(Color.Black);
        private SolidBrush _SemiSubtleBrush = new SolidBrush(Color.FromArgb(48, 48, 96));
        private SolidBrush _BrightBrush = new SolidBrush(Color.FromArgb(96, 96, (int)byte.MaxValue));
        public Color _UnknownColor = Color.FromArgb(96, 96, 96);
        private Color _PirateColor = Color.FromArgb(80, 80, 80);
        private Color _IndependentColor = Color.FromArgb(160, 160, 160);
        private Bitmap[] _TroopImagesInfantry;
        private Bitmap[] _TroopImagesFadedInfantry;
        private Bitmap[] _TroopImagesArmored;
        private Bitmap[] _TroopImagesFadedArmored;
        private Bitmap[] _TroopImagesArtillery;
        private Bitmap[] _TroopImagesFadedArtillery;
        private Bitmap[] _TroopImagesSpecialForces;
        private Bitmap[] _TroopImagesFadedSpecialForces;
        private Bitmap[] _TroopImagesPirateRaider;
        private Bitmap[] _TroopImagesFadedPirateRaider;
        private Bitmap[] _ResourceImages;
        private Bitmap[] _RaceImages;
        public Bitmap[] _BuiltObjectImages;
        private Bitmap[] _FighterImages;
        private Bitmap[] _FighterImagesFaded;
        private Bitmap[] _RuinImages;
        private Bitmap[] _FacilityImages;
        private Bitmap[] _FacilityImagesFaded;
        private Bitmap[] _HabitatImages;
        private Bitmap[] _PlagueImages;
        public static Bitmap[] _MessageImages;
        private static Bitmap[] TroopImagesInfantry;
        private static Bitmap[] TroopImagesFadedInfantry;
        private static Bitmap[] TroopImagesArmored;
        private static Bitmap[] TroopImagesFadedArmored;
        private static Bitmap[] TroopImagesArtillery;
        private static Bitmap[] TroopImagesFadedArtillery;
        private static Bitmap[] TroopImagesSpecialForces;
        private static Bitmap[] TroopImagesFadedSpecialForces;
        private static Bitmap[] TroopImagesPirateRaider;
        private static Bitmap[] TroopImagesFadedPirateRaider;
        private static Bitmap[] ResourceImages;
        private static Bitmap[] RaceImages;
        private static Bitmap[] BuiltObjectImages;
        private static Bitmap[] FighterImages;
        private static Bitmap[] FighterImagesFaded;
        private static Bitmap[] RuinImages;
        private static Bitmap[] FacilityImages;
        private static Bitmap[] FacilityImagesFaded;
        private static Bitmap[] HabitatImages;
        private static Bitmap[] PlagueImages;
        private static Bitmap[] TroopImagesInfantryLarge;
        private static Bitmap[] TroopImagesFadedInfantryLarge;
        private static Bitmap[] TroopImagesArmoredLarge;
        private static Bitmap[] TroopImagesFadedArmoredLarge;
        private static Bitmap[] TroopImagesArtilleryLarge;
        private static Bitmap[] TroopImagesFadedArtilleryLarge;
        private static Bitmap[] TroopImagesSpecialForcesLarge;
        private static Bitmap[] TroopImagesFadedSpecialForcesLarge;
        private static Bitmap[] TroopImagesPirateRaiderLarge;
        private static Bitmap[] TroopImagesFadedPirateRaiderLarge;
        private static Bitmap[] ResourceImagesLarge;
        private static Bitmap[] RaceImagesLarge;
        private static Bitmap[] BuiltObjectImagesLarge;
        private static Bitmap[] FighterImagesLarge;
        private static Bitmap[] FighterImagesFadedLarge;
        private static Bitmap[] RuinImagesLarge;
        private static Bitmap[] FacilityImagesLarge;
        private static Bitmap[] FacilityImagesFadedLarge;
        private static Bitmap[] HabitatImagesLarge;
        private static Bitmap[] PlagueImagesLarge;
        private static Bitmap _ApprovalSmileImage;
        private static Bitmap _ApprovalNeutralImage;
        private static Bitmap _ApprovalSadImage;
        private static Bitmap _ApprovalAngryImage;
        private static Bitmap _DevelopmentImage;
        private static Bitmap _ColonyImage;
        private static Bitmap _FirepowerImage;
        public static Bitmap _ShipGroupLeadShipImage;
        private static Bitmap _CapitalColonyImage;
        private static Bitmap _RegionalCapitalColonyImage;
        public static Bitmap _AutomateImage;
        public static Bitmap _BlockadeImage;
        private bool _EmpireCanColonize;
        private string _ColonizeExplanation = string.Empty;
        public string _CharacterBonuses = string.Empty;
        protected IFontCache _FontCache;
        private float _FontSize = 15.33f;
        private bool _FontIsBold;

        public static event EventHandler<FormatForLargeNumbersModsArgs> FormatForLargeNumbersMods;
        public static event EventHandler<DrawBuiltObjectModsArgs> DrawBuiltObjectMods;
        public static event EventHandler<DrawStringRedWithDropShadowtModsArgs> DrawStringRedWithDropShadowtMods;
        public static event EventHandler<DrawStringWithDropShadowModsArgs> DrawStringWithDropShadowMods;
        public static event EventHandler<DrawStringColorWithDropShadowModsArgs> DrawStringColorWithDropShadowMods;
        public static event EventHandler<DrawStringWithDropShadowBoundedModsArgs> DrawStringWithDropShadowBoundedMods;
        public static event EventHandler<DrawShipGroupModsArgs> DrawShipGroupMods;
        public static event EventHandler<DrawStringWithDropShadowModsArgs2> DrawStringWithDropShadowMods2;


        public bool ContentSizeIsLarge => this._ContentSizeIsLarge;

        public bool AddHotspots
        {
            get => this._AddHotspots;
            set => this._AddHotspots = value;
        }

        public virtual void SetFontCache(IFontCache fontCache)
        {
            this._FontCache = fontCache;
            if ((double)this._FontSize <= 0.0)
                return;
            this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
        }

        public void SetFont(float pixelSize) => this.SetFont(pixelSize, false);

        public void SetFont(float pixelSize, bool isBold)
        {
            this._FontSize = pixelSize;
            this._FontIsBold = isBold;
            if (this._FontCache == null)
                return;
            Font font = this.Font;
            this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
        }

        public HotspotList Hotspots => this._Hotspots;

        public void AddHotspot(Rectangle region, object relatedObject) => this.AddHotspot(region, relatedObject, string.Empty);

        public void AddHotspot(Rectangle region, object relatedObject, string message)
        {
            if (!this._AddHotspots)
                return;
            this._Hotspots.Add(new Hotspot(region, relatedObject, message));
        }

        private void DesignModeInvalidate()
        {
            if (!this.DesignMode)
                return;
            this.Invalidate();
        }

        [Description("The primary background color used to display text and graphics in the control.")]
        [DefaultValue(typeof(Color), "Window")]
        [Category("Appearance")]
        public new Color BackColor
        {
            get => this._BackColour1;
            set
            {
                this._BackColour1 = value;
                this.DesignModeInvalidate();
            }
        }

        [DefaultValue(typeof(Color), "Window")]
        [Category("Appearance")]
        [Description("The secondary background color used to paint the control.")]
        public Color BackColor2
        {
            get => this._BackColour2;
            set
            {
                this._BackColour2 = value;
                this.DesignModeInvalidate();
            }
        }

        [DefaultValue(typeof(Color), "Window")]
        [Category("Appearance")]
        [Description("The third background color used to paint the control.")]
        public Color BackColor3
        {
            get => this._BackColour3;
            set
            {
                this._BackColour3 = value;
                this.DesignModeInvalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(LinearGradientMode), "None")]
        [Description("The gradient direction used to paint the control.")]
        public LinearGradientMode GradientMode
        {
            get => this._GradientMode;
            set
            {
                this._GradientMode = value;
                this.DesignModeInvalidate();
            }
        }

        [Category("Appearance")]
        [Description("The border style used to paint the control.")]
        [DefaultValue(typeof(BorderStyle), "None")]
        public new BorderStyle BorderStyle
        {
            get => this._BorderStyle;
            set
            {
                this._BorderStyle = value;
                this.DesignModeInvalidate();
            }
        }

        [Description("The border color used to paint the control.")]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "WindowFrame")]
        public Color BorderColor
        {
            get => this._BorderColour;
            set
            {
                this._BorderColour = value;
                this.DesignModeInvalidate();
            }
        }

        [Description("The width of the border used to paint the control.")]
        [Category("Appearance")]
        [DefaultValue(typeof(int), "1")]
        public int BorderWidth
        {
            get => this._BorderWidth;
            set
            {
                this._BorderWidth = value;
                this.DesignModeInvalidate();
            }
        }

        [Category("Appearance")]
        [Description("The radius of the curve used to paint the corners of the control.")]
        [DefaultValue(typeof(int), "0")]
        public int Curvature
        {
            get => this._Curvature;
            set
            {
                this._Curvature = value;
                this.DesignModeInvalidate();
            }
        }

        [Description("The style of the curves to be drawn on the control.")]
        [Category("Appearance")]
        [DefaultValue(typeof(CornerCurveMode), "All")]
        public CornerCurveMode CurveMode
        {
            get => this._CurveMode;
            set
            {
                this._CurveMode = value;
                this.DesignModeInvalidate();
            }
        }

        private int AdjustedCurve
        {
            get
            {
                int adjustedCurve = 0;
                if (this._CurveMode != CornerCurveMode.None)
                {
                    adjustedCurve = this._Curvature <= this.ClientRectangle.Width / 2 ? this._Curvature : InfoPanel.DoubleToInt((double)(this.ClientRectangle.Width / 2));
                    if (adjustedCurve > this.ClientRectangle.Height / 2)
                        adjustedCurve = InfoPanel.DoubleToInt((double)(this.ClientRectangle.Height / 2));
                }
                return adjustedCurve;
            }
        }

        public bool ShowExtendedInfo
        {
            get => this._ShowExtendedInfo;
            set => this._ShowExtendedInfo = value;
        }

        private void SetDefaultControlStyles()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserMouse, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ContainerControl, false);
            this.UpdateStyles();
        }

        private void CustomInitialisation()
        {
            this.SuspendLayout();
            base.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
            this.ResumeLayout(false);
        }

        public void DrawBackground(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = this.GetPath();
            Rectangle clientRectangle = this.ClientRectangle;
            if (this.ClientRectangle.Width == 0)
                ++clientRectangle.Width;
            if (this.ClientRectangle.Height == 0)
                ++clientRectangle.Height;
            LinearGradientBrush linearGradientBrush;
            if (this._GradientMode == LinearGradientMode.None)
            {
                linearGradientBrush = new LinearGradientBrush(clientRectangle, this._BackColour1, this._BackColour1, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            }
            else
            {
                ColorBlend colorBlend = new ColorBlend(3);
                Color[] colorArray = new Color[3]
                {
          this._BackColour1,
          this._BackColour2,
          this._BackColour3
                };
                float[] numArray = new float[3] { 0.0f, 0.5f, 1f };
                colorBlend.Colors = colorArray;
                colorBlend.Positions = numArray;
                linearGradientBrush = new LinearGradientBrush(clientRectangle, this._BackColour1, this._BackColour2, (System.Drawing.Drawing2D.LinearGradientMode)this._GradientMode);
                linearGradientBrush.InterpolationColors = colorBlend;
            }
            graphics.FillPath((Brush)linearGradientBrush, path);
            linearGradientBrush.Dispose();
            switch (this._BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    Pen pen = new Pen(this._BorderColour, (float)this._BorderWidth);
                    graphics.DrawPath(pen, path);
                    pen.Dispose();
                    break;
                case BorderStyle.Fixed3D:
                    PersistentGradientPanel.DrawBorder3D(graphics, this.ClientRectangle);
                    break;
            }
            linearGradientBrush.Dispose();
            path.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            this.DrawBackground(e.Graphics);
        }

        protected GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            if (this._BorderStyle == BorderStyle.Fixed3D)
            {
                path.AddRectangle(this.ClientRectangle);
            }
            else
            {
                try
                {
                    int num1 = 0;
                    Rectangle clientRectangle = this.ClientRectangle;
                    int num2 = 0;
                    switch (this._BorderStyle)
                    {
                        case BorderStyle.None:
                            num1 = this.AdjustedCurve;
                            break;
                        case BorderStyle.FixedSingle:
                            if (this._BorderWidth > 1)
                                num2 = InfoPanel.DoubleToInt((double)(this.BorderWidth / 2));
                            num1 = this.AdjustedCurve;
                            break;
                    }
                    if (num1 == 0)
                    {
                        path.AddRectangle(clientRectangle);
                    }
                    else
                    {
                        int num3 = clientRectangle.Width - num2;
                        int num4 = clientRectangle.Height - num2;
                        int num5 = (this._CurveMode & CornerCurveMode.TopRight) == CornerCurveMode.None ? 1 : num1 * 2;
                        path.AddArc(num3 - num5, num2, num5, num5, 270f, 90f);
                        int num6 = (this._CurveMode & CornerCurveMode.BottomRight) == CornerCurveMode.None ? 1 : num1 * 2;
                        path.AddArc(num3 - num6, num4 - num6, num6, num6, 0.0f, 90f);
                        int num7 = (this._CurveMode & CornerCurveMode.BottomLeft) == CornerCurveMode.None ? 1 : num1 * 2;
                        path.AddArc(num2, num4 - num7, num7, num7, 90f, 90f);
                        int num8 = (this._CurveMode & CornerCurveMode.TopLeft) == CornerCurveMode.None ? 1 : num1 * 2;
                        path.AddArc(num2, num2, num8, num8, 180f, 90f);
                        path.CloseFigure();
                    }
                }
                catch (Exception ex)
                {
                    path.AddRectangle(this.ClientRectangle);
                }
            }
            return path;
        }

        public static void DrawBorder3D(Graphics graphics, Rectangle rectangle)
        {
            graphics.SmoothingMode = SmoothingMode.Default;
            graphics.DrawLine(SystemPens.ControlDark, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Y);
            graphics.DrawLine(SystemPens.ControlDark, rectangle.X, rectangle.Y, rectangle.X, rectangle.Height - 1);
            graphics.DrawLine(SystemPens.ControlDarkDark, rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 1, rectangle.Y + 1);
            graphics.DrawLine(SystemPens.ControlDarkDark, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Height - 1);
            graphics.DrawLine(SystemPens.ControlLight, rectangle.X + 1, rectangle.Height - 2, rectangle.Width - 2, rectangle.Height - 2);
            graphics.DrawLine(SystemPens.ControlLight, rectangle.Width - 2, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
            graphics.DrawLine(SystemPens.ControlLightLight, rectangle.X, rectangle.Height - 1, rectangle.Width - 1, rectangle.Height - 1);
            graphics.DrawLine(SystemPens.ControlLightLight, rectangle.Width - 1, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
        }

        public static int DoubleToInt(double value) => Decimal.ToInt32(Decimal.Floor(Decimal.Parse(value.ToString(), (IFormatProvider)NumberFormatInfo.InvariantInfo)));

        public InfoPanel()
        {
            this.SetDefaultControlStyles();
            this.CustomInitialisation();
            this.AutoScroll = false;
            this.AutoScrollMargin = new Size(0, 0);
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            this.SetFont(15.33f);
            this._NormalFont = this.Font;
            this._NormalFontBold = new Font(this.Font, FontStyle.Bold);
            this._HotspotPen = new Pen(Color.FromArgb(170, 170, 170), 1f);
            this._HotspotPen.DashStyle = DashStyle.Dot;
            using (Graphics graphics = this.CreateGraphics())
                this.DrawPanelWithBackground(graphics);
        }

        public void Reset()
        {
            this.RepointImageInstances();
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
        }

        private void ClearPanel(Graphics graphics) => graphics.Clear(Color.Transparent);

        public void Clear()
        {
            this._Picture = (Bitmap)null;
            this._EmpireColor = this._WhiteColor;
            this.Text = string.Empty;
        }

        public void ReDraw()
        {
            if (this._BuiltObject != null)
            {
                this._PictureAngle = (double)this._BuiltObject.Heading * -1.0;
                if (this._BuiltObject.ActualEmpire != null)
                    this._EmpirePicture = this.PrescaleImage(this._BuiltObject.ActualEmpire.LargeFlagPicture, this._FlagSizeSmall.Width, this._FlagSizeSmall.Height);
            }
            else if (this._Fighter != null)
            {
                this._PictureAngle = (double)this._Fighter.Heading * -1.0;
                if (this._Fighter.Empire != null)
                    this._EmpirePicture = this.PrescaleImage(this._Fighter.Empire.LargeFlagPicture, this._FlagSizeSmall.Width, this._FlagSizeSmall.Height);
            }
            else if (this._Habitat != null)
            {
                this._PictureAngle = 0.0;
                if (this._Habitat.Empire != null && this._Habitat.Empire != this._Galaxy.IndependentEmpire)
                    this._EmpirePicture = this.PrescaleImage(this._Habitat.Empire.LargeFlagPicture, this._FlagSizeSmall.Width, this._FlagSizeSmall.Height);
            }
            this.Invalidate();
        }

        public void ClearData()
        {
            this._Hotspots.Clear();
            this._Game = (Game)null;
            this._Galaxy = (Galaxy)null;
            this._BuiltObject = (BuiltObject)null;
            this._Fighter = (Fighter)null;
            this._Habitat = (Habitat)null;
            this._Creature = (Creature)null;
            this._BuiltObjects = (BuiltObjectList)null;
            this._ShipGroup = (ShipGroup)null;
            this._SystemInfo = (SystemInfo)null;
            this._Picture = (Bitmap)null;
            this._EmpirePicture = (Bitmap)null;
            this._EmpireColor = this._WhiteColor;
            this._ActualEmpire = (Empire)null;
            this._SmugglingMission = (EmpireActivity)null;
            this._PictureAngle = 0.0;
            this._PictureSize = 0;
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
            this.Invalidate();
        }

        private void SetEmpirePictureAndColor(Empire empire)
        {
            if (empire != null)
            {
                this._EmpirePicture = this.PrescaleImage(empire.LargeFlagPicture, this._FlagSizeSmall.Width, this._FlagSizeSmall.Height);
                if (empire == this._Galaxy.IndependentEmpire)
                    this._EmpireColor = this._IndependentColor;
                else if (empire.PirateEmpireBaseHabitat != null)
                {
                    if (empire.MainColor == Color.FromArgb(1, 1, 1))
                        this._EmpireColor = this._PirateColor;
                    else
                        this._EmpireColor = empire.MainColor;
                }
                else
                    this._EmpireColor = empire.MainColor;
            }
            else
            {
                this._EmpireColor = this._WhiteColor;
                this._EmpirePicture = new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
            }
        }

        private void SetFonts()
        {
            this.SetFont(15.33f);
            this._NormalFontNormalSize = this.CreateDisposeFont(this._NormalFontNormalSize, 15.33f, false);
            this._NormalBoldFontNormalSize = this.CreateDisposeFont(this._NormalBoldFontNormalSize, 15.33f, true);
            this._TitleFontNormalSize = this.CreateDisposeFont(this._TitleFontNormalSize, 17.33f, true);
            this._TinyFontNormalSize = this.CreateDisposeFont(this._TinyFontNormalSize, 10.67f, false);
            this._NormalFontLargeSize = this.CreateDisposeFont(this._NormalFontLargeSize, 21.4f, false);
            this._NormalBoldFontLargeSize = this.CreateDisposeFont(this._NormalBoldFontLargeSize, 21.4f, true);
            this._TitleFontLargeSize = this.CreateDisposeFont(this._TitleFontLargeSize, 23.4f, true);
            this._TinyFontLargeSize = this.CreateDisposeFont(this._TinyFontLargeSize, 15.33f, false);
            if (this._ContentSizeIsLarge)
            {
                this._NormalFont = this._NormalFontLargeSize;
                this._NormalFontBold = this._NormalBoldFontLargeSize;
                this._TitleFont = this._TitleFontLargeSize;
                this._TinyFont = this._TinyFontLargeSize;
            }
            else
            {
                this._NormalFont = this._NormalFontNormalSize;
                this._NormalFontBold = this._NormalBoldFontNormalSize;
                this._TitleFont = this._TitleFontNormalSize;
                this._TinyFont = this._TinyFontNormalSize;
            }
        }

        private Font CreateDisposeFont(Font font, float size, bool isBold)
        {
            Font font1 = font;
            if ((double)size > 0.0)
                font = this._FontCache.GenerateFont(size, isBold);
            font1?.Dispose();
            return font;
        }

        public void SetData(
          Game game,
          Galaxy galaxy,
          Bitmap backgroundPicture,
          Bitmap maskImage,
          BuiltObject builtObject)
        {
            this.SetFonts();
            this.RepointImageInstances();
            this._Hotspots.Clear();
            this._AddHotspots = true;
            this._Game = game;
            this._Galaxy = galaxy;
            this._BuiltObjects = (BuiltObjectList)null;
            this._BuiltObject = builtObject;
            this._Fighter = (Fighter)null;
            this._Habitat = (Habitat)null;
            this._Creature = (Creature)null;
            this._ShipGroup = (ShipGroup)null;
            this._SystemInfo = (SystemInfo)null;
            this._EmpireColor = this._WhiteColor;
            this._SmugglingMission = (EmpireActivity)null;
            if (this._Picture != null)
                this._Picture.Dispose();
            this._Picture = this.FadeImage(backgroundPicture, 0.33f);
            if (this._MaskImage != null)
                this._MaskImage.Dispose();
            if (maskImage != null && maskImage.PixelFormat != PixelFormat.Undefined)
                this._MaskImage = new Bitmap((Image)maskImage);
            Empire empire = builtObject.Empire;
            if (builtObject.ActualEmpire == game.PlayerEmpire || game.GodMode)
                empire = builtObject.ActualEmpire;
            this.SetEmpirePictureAndColor(empire);
            this._ActualEmpire = empire;
            if (builtObject.Role == BuiltObjectRole.Freight && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Transport && builtObject.Mission.SecondaryTargetHabitat != null)
            {
                EmpireActivity firstByTargetAndType = this._ActualEmpire.PirateMissions.GetFirstByTargetAndType((StellarObject)builtObject.Mission.SecondaryTargetHabitat, EmpireActivityType.Smuggle);
                if (firstByTargetAndType != null && firstByTargetAndType.RequestingEmpire != this._ActualEmpire)
                    this._SmugglingMission = firstByTargetAndType;
            }
            this._PictureAngle = (double)builtObject.Heading * -1.0;
            this._PictureSize = (int)((double)builtObject.Size / 0.6);
            if (this._PictureSize < this._MinPictureSize)
                this._PictureSize = this._MinPictureSize;
            if (this._PictureSize > this._MaxPictureSize)
                this._PictureSize = this._MaxPictureSize;
            this._CharacterBonuses = Galaxy.GenerateCharacterBonusDescription(builtObject);
            this._BuiltObjectIsScanned = this._Galaxy.CheckBuiltObjectScanned(this._BuiltObject);
            this._LastBuiltObjectScanTime = DateTime.Now;
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
        }

        public void SetData(
          Game game,
          Galaxy galaxy,
          Bitmap backgroundPicture,
          Bitmap maskImage,
          Fighter fighter)
        {
            this.SetFonts();
            this.RepointImageInstances();
            this._Hotspots.Clear();
            this._AddHotspots = true;
            this._Game = game;
            this._Galaxy = galaxy;
            this._BuiltObjects = (BuiltObjectList)null;
            this._BuiltObject = (BuiltObject)null;
            this._Fighter = fighter;
            this._Habitat = (Habitat)null;
            this._Creature = (Creature)null;
            this._ShipGroup = (ShipGroup)null;
            this._SystemInfo = (SystemInfo)null;
            this._EmpireColor = this._WhiteColor;
            this._SmugglingMission = (EmpireActivity)null;
            if (this._Picture != null)
                this._Picture.Dispose();
            this._Picture = this.FadeImage(backgroundPicture, 0.33f);
            this._CharacterBonuses = Galaxy.GenerateCharacterBonusDescription(fighter);
            if (this._MaskImage != null)
                this._MaskImage.Dispose();
            if (maskImage != null && maskImage.PixelFormat != PixelFormat.Undefined)
                this._MaskImage = new Bitmap((Image)maskImage);
            this.SetEmpirePictureAndColor(fighter.Empire);
            this._PictureAngle = (double)fighter.Heading * -1.0;
            this._PictureSize = (int)((double)fighter.Size * 15.0);
            if (this._PictureSize < this._MinPictureSize)
                this._PictureSize = this._MinPictureSize;
            if (this._PictureSize > this._MaxPictureSize)
                this._PictureSize = this._MaxPictureSize;
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
        }

        private Bitmap PreProcessImage(Bitmap image, int pictureSize)
        {
            image = this.FadeImage(image, 0.33f);
            Rectangle rectangle1 = new Rectangle();
            Rectangle rectangle2 = new Rectangle();
            int num1 = this.ClientRectangle.Width - 6;
            int num2 = this.ClientRectangle.Height - 6;
            double num3 = (double)image.Width / (double)image.Height;
            int num4;
            int num5;
            if (image.Width > image.Height)
            {
                num4 = (int)((double)pictureSize * num3);
                num5 = this._PictureSize;
            }
            else
            {
                num4 = pictureSize;
                num5 = (int)((double)pictureSize / num3);
            }
            double num6;
            if (num5 > num2)
            {
                num6 = (double)num2 / (double)num5;
                if ((double)num4 * num6 > (double)num1)
                    num6 *= (double)num1 / ((double)num4 * num6);
            }
            else if (num4 > num1)
            {
                num6 = (double)num1 / (double)num4;
                if ((double)num5 * num6 > (double)num2)
                    num6 *= (double)num2 / ((double)num5 * num6);
            }
            else
                num6 = 1.0;
            rectangle1 = new Rectangle((num1 - (int)((double)num4 * num6)) / 2 + 3, (num2 - (int)((double)num5 * num6)) / 2 + 3, (int)((double)num4 * num6), (int)((double)num5 * num6));
            Bitmap bitmap = new Bitmap(rectangle1.Width, rectangle1.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                Rectangle rect = new Rectangle(0, 0, rectangle1.Width, rectangle1.Height);
                graphics.DrawImage((Image)image, rect);
            }
            return bitmap;
        }

        private Bitmap OverlayRuins(Bitmap image, Habitat habitat)
        {
            Bitmap bitmap1 = new Bitmap((Image)image);
            if (habitat.Ruin != null)
            {
                using (Graphics graphics = Graphics.FromImage((Image)bitmap1))
                {
                    Bitmap bitmap2 = this.FadeImage(new Bitmap((Image)this._RuinImages[habitat.Ruin.PictureRef]), 0.7f);
                    double num = (double)image.Width / (double)habitat.Diameter;
                    int x = (int)(((double)((int)habitat.Diameter / 2) + habitat.Ruin.ParentX) * num - (double)(bitmap2.Width / 2));
                    int y = (int)(((double)((int)habitat.Diameter / 2) + habitat.Ruin.ParentY) * num - (double)(bitmap2.Height / 2));
                    graphics.DrawImage((Image)bitmap2, new Point(x, y));
                }
            }
            return bitmap1;
        }

        public void SetData(Game game, Galaxy galaxy, Bitmap backgroundPicture, Habitat habitat)
        {
            this.SetFonts();
            this.RepointImageInstances();
            this._Hotspots.Clear();
            this._AddHotspots = true;
            this._Game = game;
            this._Galaxy = galaxy;
            this._BuiltObjects = (BuiltObjectList)null;
            this._BuiltObject = (BuiltObject)null;
            this._Fighter = (Fighter)null;
            this._Habitat = habitat;
            this._Creature = (Creature)null;
            this._ShipGroup = (ShipGroup)null;
            this._SystemInfo = (SystemInfo)null;
            this._SmugglingMission = (EmpireActivity)null;
            this.SetEmpirePictureAndColor(habitat.Empire);
            this._EmpireCanColonize = this._Galaxy.PlayerEmpire.CanEmpireColonizeHabitat(habitat, out this._ColonizeExplanation);
            this._CharacterBonuses = Galaxy.GenerateCharacterBonusDescription(habitat);
            if (this._Picture != null)
                this._Picture.Dispose();
            this._Picture = backgroundPicture;
            this._PictureAngle = 0.0;
            this._PictureSize = (int)habitat.Diameter;
            if (this._PictureSize < this._MinPictureSize)
                this._PictureSize = this._MinPictureSize;
            if (this._PictureSize > this._MaxPictureSize)
                this._PictureSize = this._MaxPictureSize;
            this._Picture = this.PreProcessImage(this._Picture, this._PictureSize);
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
        }

        public Bitmap Picture
        {
            get => this._Picture;
            set
            {
                this._Picture = value;
                if (this._Picture != null)
                    this._PictureSize = this._Picture.Width;
                else
                    this._PictureSize = 0;
            }
        }

        private void DrawPanel(object sender, PaintEventArgs pe) => this.DrawPanel(pe.Graphics);

        private void DrawPanelWithBackground(Graphics graphics)
        {
            try
            {
                if (this._CurveMode != CornerCurveMode.None)
                    base.OnPaintBackground(new PaintEventArgs(graphics, new Rectangle(0, 0, this.Width, this.Height)));
                this.DrawBackground(graphics);
                this.DrawPanel(graphics);
            }
            catch (Exception ex)
            {
                this.ClearData();
            }
        }

        private Color ResolveLightColor(Color color, int increaseAmount) => Color.FromArgb(Math.Max(0, Math.Min((int)byte.MaxValue, (int)color.R + increaseAmount)), Math.Max(0, Math.Min((int)byte.MaxValue, (int)color.G + increaseAmount)), Math.Max(0, Math.Min((int)byte.MaxValue, (int)color.B + increaseAmount)));

        internal void DrawPanel(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            if (this._BuiltObject != null)
            {
                if (this._Game.GodMode || this._Game.PlayerEmpire.IsObjectVisibleToThisEmpire((StellarObject)this._BuiltObject))
                    this.DrawBuiltObject(this._BuiltObject, graphics);
                else
                    this._Game.SelectedObject = (object)null;
            }
            else if (this._Fighter != null)
            {
                if (this._Game.GodMode || this._Game.PlayerEmpire.IsObjectVisibleToThisEmpire((StellarObject)this._Fighter))
                    this.DrawFighter(this._Fighter, graphics);
                else
                    this._Game.SelectedObject = (object)null;
            }
            else if (this._Habitat != null)
                this.DrawHabitat(this._Habitat, graphics);
            else if (this._Creature != null)
            {
                if (this._Game.GodMode || this._Game.PlayerEmpire.IsObjectVisibleToThisEmpire(this._Creature))
                    this.DrawCreature(this._Creature, graphics);
                else
                    this._Game.SelectedObject = (object)null;
            }
            else if (this._BuiltObjects != null)
                this.DrawBuiltObjectSelection(this._BuiltObjects, graphics);
            else if (this._ShipGroup != null)
            {
                if (this._Game.GodMode || this._Game.PlayerEmpire.IsObjectVisibleToThisEmpire((StellarObject)this._ShipGroup.LeadShip))
                    this.DrawShipGroup(this._ShipGroup, graphics);
                else
                    this._Game.SelectedObject = (object)null;
            }
            else if (this._SystemInfo != null)
                this.DrawSystemInfo(this._SystemInfo, graphics);
            this.DrawHotspotHoverRegions(graphics);
            this._AddHotspots = false;
        }

        private void DrawHotspotHoverRegions(Graphics graphics)
        {
            foreach (Hotspot hotspot in (List<Hotspot>)this._Hotspots)
            {
                if (hotspot.Hovered)
                {
                    graphics.DrawRectangle(this._HotspotPen, hotspot.Region);
                    hotspot.Hovered = false;
                }
            }
        }

        private Bitmap OverlayDamage(BuiltObject builtObject, Bitmap image, Bitmap maskingImage)
        {
            if (builtObject.DamagedComponentCount <= 0)
                return image;
            double num = (double)builtObject.DamagedComponentCount / (double)builtObject.Components.Count;
            int damagedPixelCount = (int)((double)(image.Width * image.Height) * 0.7 * num);
            Random rnd = new Random(builtObject.BuiltObjectID);
            using (HatchBrush damageBrush = new HatchBrush(HatchStyle.Cross, Color.FromArgb(160, 160, 160), Color.FromArgb(1, 1, 1)))
                return this.OverlayDamage(image, maskingImage, (Brush)damageBrush, damagedPixelCount, rnd);
        }

        private Bitmap OverlayDamage(Fighter fighter, Bitmap image, Bitmap maskingImage)
        {
            if ((double)fighter.Health >= 1.0 || fighter.UnderConstruction)
                return image;
            double num = 1.0 - (double)fighter.Health;
            int damagedPixelCount = (int)((double)(image.Width * image.Height) * 0.7 * num);
            Random rnd = new Random(fighter.FighterID);
            using (HatchBrush damageBrush = new HatchBrush(HatchStyle.Cross, Color.FromArgb(160, 160, 160), Color.FromArgb(1, 1, 1)))
                return this.OverlayDamage(image, maskingImage, (Brush)damageBrush, damagedPixelCount, rnd);
        }

        private Bitmap OverlayDamage(Creature creature, Bitmap image, Bitmap maskingImage)
        {
            if (creature.Damage <= 0.0)
                return image;
            double num = creature.Damage / (double)creature.DamageKillThreshhold;
            int damagedPixelCount = (int)((double)(image.Width * image.Height) * 0.3 * num);
            Random rnd = new Random(creature.CreatureID);
            Color color = Color.FromArgb(1, 1, 1);
            switch (creature.Type)
            {
                case CreatureType.Kaltor:
                    color = Color.FromArgb(96, 110, 32, 80);
                    break;
                case CreatureType.RockSpaceSlug:
                    color = Color.FromArgb(96, 64, 32, 36);
                    break;
                case CreatureType.DesertSpaceSlug:
                    color = Color.FromArgb(96, 160, 56, 0);
                    break;
                case CreatureType.Ardilus:
                    color = Color.FromArgb(96, 48, 8, 20);
                    break;
                case CreatureType.SilverMist:
                    float transparencyLevel = Math.Min(1f, Math.Max(0.0f, (float)(1.0 - 0.89999997615814209 * (creature.Damage / (double)creature.DamageKillThreshhold))));
                    return this.FadeImage(image, transparencyLevel);
            }
            using (SolidBrush damageBrush = new SolidBrush(color))
                return this.OverlayDamage(image, maskingImage, (Brush)damageBrush, damagedPixelCount, rnd);
        }

        private Bitmap OverlayDamage(
          Bitmap image,
          Bitmap maskingImage,
          Brush damageBrush,
          int damagedPixelCount,
          Random rnd)
        {
            int width1 = Math.Max(2, image.Width);
            int height1 = Math.Max(2, image.Height);
            int width2 = Math.Max(2, maskingImage.Width);
            int height2 = Math.Max(2, maskingImage.Height);
            Bitmap bitmap = new Bitmap(width1, height1, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.DrawImageUnscaled((Image)image, 0, 0);
                List<Rectangle> rectangleList = new List<Rectangle>();
                List<Color> colorList = new List<Color>();
                int num1;
                for (; damagedPixelCount > 0; damagedPixelCount -= num1)
                {
                    num1 = 0;
                    int num2 = rnd.Next(0, 6);
                    int x = rnd.Next(0, bitmap.Width - 1);
                    int y = rnd.Next(0, bitmap.Height - 1);
                    switch (num2)
                    {
                        case 0:
                            rectangleList.Add(new Rectangle(x, y, 3, 3));
                            rectangleList.Add(new Rectangle(x + 3, y, 1, 2));
                            num1 = 11;
                            break;
                        case 1:
                            rectangleList.Add(new Rectangle(x, y, 2, 2));
                            rectangleList.Add(new Rectangle(x + 1, y + 2, 2, 2));
                            num1 = 8;
                            break;
                        case 2:
                            rectangleList.Add(new Rectangle(x, y, 3, 5));
                            rectangleList.Add(new Rectangle(x - 1, y + 2, 2, 2));
                            num1 = 19;
                            break;
                        case 3:
                            rectangleList.Add(new Rectangle(x, y, 2, 2));
                            rectangleList.Add(new Rectangle(x - 1, y - 1, 2, 2));
                            num1 = 7;
                            break;
                        case 4:
                            rectangleList.Add(new Rectangle(x, y, 2, 2));
                            rectangleList.Add(new Rectangle(x - 1, y - 1, 1, 1));
                            rectangleList.Add(new Rectangle(x + 1, y + 2, 1, 1));
                            num1 = 6;
                            break;
                        case 5:
                            rectangleList.Add(new Rectangle(x, y, 3, 3));
                            rectangleList.Add(new Rectangle(x - 2, y, 2, 2));
                            num1 = 13;
                            break;
                    }
                }
                if (rectangleList.Count > 0)
                    graphics.FillRectangles(damageBrush, rectangleList.ToArray());
                this.SetGraphicsQualityToHigh(graphics);
                Rectangle srcRect = new Rectangle(0, 0, width2, height2);
                Rectangle destRect = new Rectangle(0, 0, width1, height1);
                using (SolidBrush solidBrush = new SolidBrush(Color.Black))
                {
                    int num3 = 1;
                    graphics.FillRectangle((Brush)solidBrush, new Rectangle(0, 0, width1, num3));
                    graphics.FillRectangle((Brush)solidBrush, new Rectangle(0, 0, num3, height1));
                    graphics.FillRectangle((Brush)solidBrush, new Rectangle(width1 - num3, 0, num3, height1));
                    graphics.FillRectangle((Brush)solidBrush, new Rectangle(0, height1 - num3, width1, num3));
                    graphics.DrawImage((Image)maskingImage, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        private Bitmap OverlayConstructionProgress(BuiltObject builtObject, Bitmap image)
        {
            if (builtObject.UnbuiltComponentCount <= 0)
                return image;
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.DrawImageUnscaled((Image)image, 0, 0);
                double num1 = 1.0 - (double)builtObject.UnbuiltComponentCount / (double)builtObject.Components.Count;
                int Seed = (int)((double)bitmap.Width * num1);
                Random random = new Random(Seed);
                int y = 0;
                int maxValue1 = Math.Max(3, builtObject.Size / 200);
                int num2 = bitmap.Width - Seed;
                int height;
                for (; y < bitmap.Height - 1; y += height)
                {
                    int num3 = random.Next(-maxValue1, maxValue1);
                    int width = Math.Max(0, Math.Min(bitmap.Width - 1, num2 + num3));
                    int num4 = random.Next(3, 6);
                    height = Math.Min(y + num4, bitmap.Height - 1) - y;
                    Rectangle rect = new Rectangle(0, y, width, height);
                    graphics.FillRectangle((Brush)this._BlackBrush, rect);
                }
                int num5 = 0;
                int maxValue2 = Math.Max(8, builtObject.Size / 80);
                List<Rectangle> rectangleList = new List<Rectangle>();
                for (; num5 < bitmap.Height; num5 += random.Next(1, 3))
                {
                    int num6 = random.Next(2, maxValue2);
                    graphics.DrawLine(this._BlackPen, 0, num5, bitmap.Width - Seed + num6, num5);
                    Rectangle rectangle = new Rectangle(bitmap.Width - Seed, num5, bitmap.Width - Seed + num6 - (bitmap.Width - Seed), 1);
                    rectangleList.Add(rectangle);
                }
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        private void SetGraphicsQualityToLow(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics.PixelOffsetMode = PixelOffsetMode.None;
        }

        public void SetGraphicsQualityToHigh(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        public void DrawBackgroundPicture(Graphics graphics)
        {
            if (this._Picture == null || this._Picture.PixelFormat == PixelFormat.Undefined)
                return;
            Rectangle rect = new Rectangle();
            int num1 = this.ClientRectangle.Width - 6;
            int num2 = this.ClientRectangle.Height - 6;
            Bitmap bitmap1;
            if (this._SystemInfo != null)
            {
                bitmap1 = this._Picture;
                int x = (num1 - bitmap1.Width) / 2;
                int y = (num2 - bitmap1.Height) / 2;
                if (this._SystemInfo.SystemStar.Category == HabitatCategoryType.Star)
                    x = (int)((double)bitmap1.Width * 0.55) * -1;
                rect = new Rectangle(x, y, bitmap1.Width, bitmap1.Height);
            }
            else if (this._Habitat != null)
            {
                bitmap1 = this._Picture;
                int x = (num1 - bitmap1.Width) / 2;
                int y = (num2 - bitmap1.Height) / 2;
                if (this._Habitat.Category == HabitatCategoryType.Star)
                    x = (int)((double)bitmap1.Width * 0.55) * -1;
                rect = new Rectangle(x, y, bitmap1.Width, bitmap1.Height);
            }
            else
            {
                int num3 = 0;
                int num4 = 0;
                if (this._BuiltObject != null)
                {
                    Bitmap bitmap2 = this.OverlayConstructionProgress(this._BuiltObject, this.OverlayDamage(this._BuiltObject, this._Picture, this._MaskImage));
                    double num5 = (double)this._PictureSize / (double)this._BuiltObjectImages[this._BuiltObject.PictureRef].Width;
                    int width = (int)((double)this._BuiltObjectImages[this._BuiltObject.PictureRef].Width * num5);
                    int height = (int)((double)this._BuiltObjectImages[this._BuiltObject.PictureRef].Height * num5);
                    Bitmap bitmap3 = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics1 = Graphics.FromImage((Image)bitmap3))
                    {
                        graphics1.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics1.InterpolationMode = InterpolationMode.Bilinear;
                        graphics1.SmoothingMode = SmoothingMode.None;
                        graphics1.DrawImage((Image)bitmap2, new Rectangle(0, 0, width, height));
                    }
                    bitmap1 = this.RotateImage((Image)bitmap3, (float)this._PictureAngle);
                    num3 = bitmap1.Width;
                    num4 = bitmap1.Height;
                }
                else if (this._Fighter != null)
                {
                    Bitmap bitmap4 = this.OverlayDamage(this._Fighter, this._Picture, this._MaskImage);
                    double num6 = (double)this._PictureSize / (double)this._FighterImages[(int)this._Fighter.PictureRef].Width;
                    int width = (int)((double)this._FighterImages[(int)this._Fighter.PictureRef].Width * num6);
                    int height = (int)((double)this._FighterImages[(int)this._Fighter.PictureRef].Height * num6);
                    Bitmap bitmap5 = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics2 = Graphics.FromImage((Image)bitmap5))
                    {
                        graphics2.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics2.InterpolationMode = InterpolationMode.Bilinear;
                        graphics2.SmoothingMode = SmoothingMode.None;
                        graphics2.DrawImage((Image)bitmap4, new Rectangle(0, 0, width, height));
                    }
                    bitmap1 = this.RotateImage((Image)bitmap5, (float)this._PictureAngle);
                    num3 = bitmap1.Width;
                    num4 = bitmap1.Height;
                }
                else if (this._Creature != null)
                {
                    double num7 = (double)this._PictureSize / (double)this._Picture.Width;
                    int width = (int)((double)this._Picture.Width * num7);
                    int height = (int)((double)this._Picture.Height * num7);
                    Bitmap image = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics3 = Graphics.FromImage((Image)image))
                    {
                        graphics3.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics3.InterpolationMode = InterpolationMode.Bilinear;
                        graphics3.SmoothingMode = SmoothingMode.None;
                        graphics3.DrawImage((Image)this._Picture, new Rectangle(0, 0, width, height));
                    }
                    bitmap1 = this.RotateImage((Image)this.OverlayDamage(this._Creature, image, this._MaskImage), this._Creature.CurrentHeading * -1f);
                    num3 = bitmap1.Width;
                    num4 = bitmap1.Height;
                }
                else
                    bitmap1 = this.FadeImage(this._Picture, 0.6f);
                double num8 = 1.0;
                if (num3 <= 0)
                {
                    double num9 = (double)bitmap1.Width / (double)bitmap1.Height;
                    if (bitmap1.Width > bitmap1.Height)
                    {
                        num3 = (int)((double)this._PictureSize * num9);
                        num4 = this._PictureSize;
                    }
                    else
                    {
                        num3 = this._PictureSize;
                        num4 = (int)((double)this._PictureSize / num9);
                    }
                    if (num4 > num2)
                    {
                        num8 = (double)num2 / (double)num4;
                        if ((double)num3 * num8 > (double)num1)
                            num8 *= (double)num1 / ((double)num3 * num8);
                    }
                    else if (num3 > num1)
                    {
                        num8 = (double)num1 / (double)num3;
                        if ((double)num4 * num8 > (double)num2)
                            num8 *= (double)num2 / ((double)num4 * num8);
                    }
                    else
                        num8 = 1.0;
                }
                rect = new Rectangle((num1 - (int)((double)num3 * num8)) / 2 + 3, (num2 - (int)((double)num4 * num8)) / 2 + 3, (int)((double)num3 * num8), (int)((double)num4 * num8));
            }
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.DrawImage((Image)bitmap1, rect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                this.DrawPanel(e.Graphics);
            }
            catch (Exception ex)
            {
                this.ClearData();
            }
        }

        private static Bitmap CopyBitmap(Bitmap image)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImageUnscaled((Image)image, 0, 0);
            }
            return bitmap;
        }

        private Bitmap PrescaleImage(Bitmap originalBitmap, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width + 1, height + 1, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImage((Image)originalBitmap, new Rectangle(0, 0, width, height));
            }
            return bitmap;
        }

        private Bitmap FadeImage(Bitmap image, float transparencyLevel)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                Rectangle destRect = new Rectangle(0, 0, image.Width, image.Height);
                ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
                {
          new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 0.0f, transparencyLevel, 0.0f },
          new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
                });
                ImageAttributes imageAttrs = new ImageAttributes();
                imageAttrs.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                graphics.DrawImage((Image)image, destRect, 0.0f, 0.0f, (float)image.Width, (float)image.Height, GraphicsUnit.Pixel, imageAttrs);
            }
            return bitmap;
        }

        private static Bitmap PrescaleImageStatic(Bitmap originalBitmap, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width + 1, height + 1, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImage((Image)originalBitmap, new Rectangle(0, 0, width, height));
            }
            return bitmap;
        }

        private static Bitmap FadeImageStatic(Bitmap image, float transparencyLevel)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                Rectangle destRect = new Rectangle(0, 0, image.Width, image.Height);
                ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
                {
          new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
          new float[5]{ 0.0f, 0.0f, 0.0f, transparencyLevel, 0.0f },
          new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
                });
                ImageAttributes imageAttrs = new ImageAttributes();
                imageAttrs.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                graphics.DrawImage((Image)image, destRect, 0.0f, 0.0f, (float)image.Width, (float)image.Height, GraphicsUnit.Pixel, imageAttrs);
            }
            return bitmap;
        }

        private static Bitmap MakeImageSquare(Bitmap image, int size)
        {
            Bitmap bitmap = new Bitmap(size, size, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
                Rectangle destRect = new Rectangle((size - image.Width) / 2, (size - image.Height) / 2, image.Width, image.Height);
                graphics.DrawImage((Image)image, destRect, srcRect, GraphicsUnit.Pixel);
            }
            return bitmap;
        }

        private static Size ResolveImageSize(Bitmap image, int maximumSize)
        {
            int width;
            int height;
            if (image.Width > image.Height)
            {
                double num = (double)image.Height / (double)image.Width;
                width = maximumSize;
                height = (int)((double)maximumSize * num);
            }
            else
            {
                double num = (double)image.Width / (double)image.Height;
                height = maximumSize;
                width = (int)((double)maximumSize * num);
            }
            return new Size(width, height);
        }

        private static void ClearImageArray(Bitmap[] imageArray)
        {
            if (imageArray == null)
                return;
            for (int index = 0; index < imageArray.Length; ++index)
            {
                if (imageArray[index] != null)
                {
                    imageArray[index].Dispose();
                    imageArray[index] = (Bitmap)null;
                }
            }
        }

        private static void ClearImages()
        {
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesInfantry);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedInfantry);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesArmored);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedArmored);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesArtillery);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedArtillery);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesSpecialForces);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedSpecialForces);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesPirateRaider);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedPirateRaider);
            InfoPanel.ClearImageArray(InfoPanel.ResourceImages);
            InfoPanel.ClearImageArray(InfoPanel.RaceImages);
            InfoPanel.ClearImageArray(InfoPanel.BuiltObjectImages);
            InfoPanel.ClearImageArray(InfoPanel.FighterImages);
            InfoPanel.ClearImageArray(InfoPanel.FighterImagesFaded);
            InfoPanel.ClearImageArray(InfoPanel.RuinImages);
            InfoPanel.ClearImageArray(InfoPanel.HabitatImages);
            InfoPanel.ClearImageArray(InfoPanel.FacilityImages);
            InfoPanel.ClearImageArray(InfoPanel.FacilityImagesFaded);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesInfantryLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedInfantryLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesArmoredLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedArmoredLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesArtilleryLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedArtilleryLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesSpecialForcesLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedSpecialForcesLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesPirateRaiderLarge);
            InfoPanel.ClearImageArray(InfoPanel.TroopImagesFadedPirateRaiderLarge);
            InfoPanel.ClearImageArray(InfoPanel.ResourceImagesLarge);
            InfoPanel.ClearImageArray(InfoPanel.RaceImagesLarge);
            InfoPanel.ClearImageArray(InfoPanel.BuiltObjectImagesLarge);
            InfoPanel.ClearImageArray(InfoPanel.FighterImagesLarge);
            InfoPanel.ClearImageArray(InfoPanel.FighterImagesFadedLarge);
            InfoPanel.ClearImageArray(InfoPanel.RuinImagesLarge);
            InfoPanel.ClearImageArray(InfoPanel.HabitatImagesLarge);
            InfoPanel.ClearImageArray(InfoPanel.FacilityImagesLarge);
            InfoPanel.ClearImageArray(InfoPanel.FacilityImagesFadedLarge);
        }

        public void Kickstart(bool isLargeSize)
        {
            this._ContentSizeIsLarge = isLargeSize;
            if (isLargeSize)
                this.SetContentSizeLarge();
            else
                this.SetContentSizeNormal();
        }

        public static void InitializeImages(
          CharacterImageCache characterImageCache,
          Bitmap[] troopImagesInfantry,
          Bitmap[] troopImagesArmored,
          Bitmap[] troopImagesArtillery,
          Bitmap[] troopImagesSpecialForces,
          Bitmap[] troopImagesPirateRaider,
          Bitmap[] resourceImages,
          RaceImageCache raceImageCache,
          Bitmap[] builtObjectImages,
          Bitmap[] fighterImages,
          Bitmap[] ruinImages,
          Bitmap[] habitatImages,
          Bitmap[] facilityImages,
          Bitmap approvalSmileImage,
          Bitmap approvalNeutralImage,
          Bitmap approvalSadImage,
          Bitmap approvalAngryImage,
          Bitmap developmentImage,
          Bitmap colonyImage,
          Bitmap firepowerImage,
          Bitmap shipGroupLeadShipImage,
          Bitmap capitalColonyImage,
          Bitmap regionalCapitalColonyImage,
          Bitmap automateImage,
          Bitmap blockadeImage,
          Bitmap[] messageImages,
          Bitmap[] plagueImages)
        {
            int num1 = 14;
            int num2 = 24;
            int num3 = 18;
            int num4 = 26;
            int num5 = 20;
            int num6 = 34;
            int num7 = 25;
            int num8 = 36;
            InfoPanel.ClearImages();
            InfoPanel._CharacterImageCache = characterImageCache;
            InfoPanel._ApprovalSmileImage = approvalSmileImage;
            InfoPanel._ApprovalNeutralImage = approvalNeutralImage;
            InfoPanel._ApprovalSadImage = approvalSadImage;
            InfoPanel._ApprovalAngryImage = approvalAngryImage;
            InfoPanel._DevelopmentImage = developmentImage;
            InfoPanel._ColonyImage = colonyImage;
            InfoPanel._FirepowerImage = firepowerImage;
            InfoPanel._ShipGroupLeadShipImage = shipGroupLeadShipImage;
            InfoPanel._CapitalColonyImage = capitalColonyImage;
            InfoPanel._RegionalCapitalColonyImage = regionalCapitalColonyImage;
            InfoPanel._AutomateImage = automateImage;
            InfoPanel._BlockadeImage = blockadeImage;
            InfoPanel._MessageImages = messageImages;
            if (ruinImages != null)
            {
                if (InfoPanel.RuinImages != null)
                {
                    for (int index = 0; index < InfoPanel.RuinImages.Length; ++index)
                    {
                        if (InfoPanel.RuinImages[index] != null)
                        {
                            InfoPanel.RuinImages[index].Dispose();
                            InfoPanel.RuinImages[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.RuinImages = new Bitmap[ruinImages.Length];
                int height1 = num3;
                for (int index = 0; index < ruinImages.Length; ++index)
                {
                    double num9 = (double)ruinImages[index].Width / (double)ruinImages[index].Height;
                    int width = (int)((double)height1 * num9);
                    InfoPanel.RuinImages[index] = InfoPanel.PrescaleImageStatic(ruinImages[index], width, height1);
                }
                if (InfoPanel.RuinImagesLarge != null)
                {
                    for (int index = 0; index < InfoPanel.RuinImagesLarge.Length; ++index)
                    {
                        if (InfoPanel.RuinImagesLarge[index] != null)
                        {
                            InfoPanel.RuinImagesLarge[index].Dispose();
                            InfoPanel.RuinImagesLarge[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.RuinImagesLarge = new Bitmap[ruinImages.Length];
                int height2 = num7;
                for (int index = 0; index < ruinImages.Length; ++index)
                {
                    double num10 = (double)ruinImages[index].Width / (double)ruinImages[index].Height;
                    int width = (int)((double)height2 * num10);
                    InfoPanel.RuinImagesLarge[index] = InfoPanel.PrescaleImageStatic(ruinImages[index], width, height2);
                }
            }
            if (facilityImages != null)
            {
                if (InfoPanel.FacilityImages != null)
                {
                    for (int index = 0; index < InfoPanel.FacilityImages.Length; ++index)
                    {
                        if (InfoPanel.FacilityImages[index] != null)
                        {
                            InfoPanel.FacilityImages[index].Dispose();
                            InfoPanel.FacilityImages[index] = (Bitmap)null;
                        }
                    }
                }
                if (InfoPanel.FacilityImagesFaded != null)
                {
                    for (int index = 0; index < InfoPanel.FacilityImagesFaded.Length; ++index)
                    {
                        if (InfoPanel.FacilityImagesFaded[index] != null)
                        {
                            InfoPanel.FacilityImagesFaded[index].Dispose();
                            InfoPanel.FacilityImagesFaded[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.FacilityImages = new Bitmap[facilityImages.Length];
                InfoPanel.FacilityImagesFaded = new Bitmap[facilityImages.Length];
                int height3 = num1;
                for (int index = 0; index < facilityImages.Length; ++index)
                {
                    double num11 = (double)facilityImages[index].Width / (double)facilityImages[index].Height;
                    int width = (int)((double)height3 * num11);
                    InfoPanel.FacilityImages[index] = InfoPanel.PrescaleImageStatic(facilityImages[index], width, height3);
                }
                for (int index = 0; index < facilityImages.Length; ++index)
                {
                    double num12 = (double)facilityImages[index].Width / (double)facilityImages[index].Height;
                    int width = (int)((double)height3 * num12);
                    InfoPanel.FacilityImagesFaded[index] = InfoPanel.FadeImageStatic(InfoPanel.PrescaleImageStatic(facilityImages[index], width, height3), 0.4f);
                }
                if (InfoPanel.FacilityImagesLarge != null)
                {
                    for (int index = 0; index < InfoPanel.FacilityImagesLarge.Length; ++index)
                    {
                        if (InfoPanel.FacilityImagesLarge[index] != null)
                        {
                            InfoPanel.FacilityImagesLarge[index].Dispose();
                            InfoPanel.FacilityImagesLarge[index] = (Bitmap)null;
                        }
                    }
                }
                if (InfoPanel.FacilityImagesFadedLarge != null)
                {
                    for (int index = 0; index < InfoPanel.FacilityImagesFadedLarge.Length; ++index)
                    {
                        if (InfoPanel.FacilityImagesFadedLarge[index] != null)
                        {
                            InfoPanel.FacilityImagesFadedLarge[index].Dispose();
                            InfoPanel.FacilityImagesFadedLarge[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.FacilityImagesLarge = new Bitmap[facilityImages.Length];
                InfoPanel.FacilityImagesFadedLarge = new Bitmap[facilityImages.Length];
                int height4 = num5;
                for (int index = 0; index < facilityImages.Length; ++index)
                {
                    double num13 = (double)facilityImages[index].Width / (double)facilityImages[index].Height;
                    int width = (int)((double)height4 * num13);
                    InfoPanel.FacilityImagesLarge[index] = InfoPanel.PrescaleImageStatic(facilityImages[index], width, height4);
                }
                for (int index = 0; index < facilityImages.Length; ++index)
                {
                    double num14 = (double)facilityImages[index].Width / (double)facilityImages[index].Height;
                    int width = (int)((double)height4 * num14);
                    InfoPanel.FacilityImagesFadedLarge[index] = InfoPanel.FadeImageStatic(InfoPanel.PrescaleImageStatic(facilityImages[index], width, height4), 0.4f);
                }
            }
            if (troopImagesInfantry != null)
            {
                InfoPanel.InitializeTroopImageArray(troopImagesInfantry, ref InfoPanel.TroopImagesInfantry, ref InfoPanel.TroopImagesFadedInfantry, num1);
                InfoPanel.InitializeTroopImageArray(troopImagesInfantry, ref InfoPanel.TroopImagesInfantryLarge, ref InfoPanel.TroopImagesFadedInfantryLarge, num5);
            }
            if (troopImagesArmored != null)
            {
                InfoPanel.InitializeTroopImageArray(troopImagesArmored, ref InfoPanel.TroopImagesArmored, ref InfoPanel.TroopImagesFadedArmored, num1);
                InfoPanel.InitializeTroopImageArray(troopImagesArmored, ref InfoPanel.TroopImagesArmoredLarge, ref InfoPanel.TroopImagesFadedArmoredLarge, num5);
            }
            if (troopImagesArtillery != null)
            {
                InfoPanel.InitializeTroopImageArray(troopImagesArtillery, ref InfoPanel.TroopImagesArtillery, ref InfoPanel.TroopImagesFadedArtillery, num1);
                InfoPanel.InitializeTroopImageArray(troopImagesArtillery, ref InfoPanel.TroopImagesArtilleryLarge, ref InfoPanel.TroopImagesFadedArtilleryLarge, num5);
            }
            if (troopImagesSpecialForces != null)
            {
                InfoPanel.InitializeTroopImageArray(troopImagesSpecialForces, ref InfoPanel.TroopImagesSpecialForces, ref InfoPanel.TroopImagesFadedSpecialForces, num1);
                InfoPanel.InitializeTroopImageArray(troopImagesSpecialForces, ref InfoPanel.TroopImagesSpecialForcesLarge, ref InfoPanel.TroopImagesFadedSpecialForcesLarge, num5);
            }
            if (troopImagesPirateRaider != null)
            {
                InfoPanel.InitializeTroopImageArray(troopImagesPirateRaider, ref InfoPanel.TroopImagesPirateRaider, ref InfoPanel.TroopImagesFadedPirateRaider, num1);
                InfoPanel.InitializeTroopImageArray(troopImagesPirateRaider, ref InfoPanel.TroopImagesPirateRaiderLarge, ref InfoPanel.TroopImagesFadedPirateRaiderLarge, num5);
            }
            if (resourceImages != null)
            {
                if (InfoPanel.ResourceImages != null)
                {
                    for (int index = 0; index < InfoPanel.ResourceImages.Length; ++index)
                    {
                        if (InfoPanel.ResourceImages[index] != null)
                        {
                            InfoPanel.ResourceImages[index].Dispose();
                            InfoPanel.ResourceImages[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.ResourceImages = new Bitmap[resourceImages.Length];
                int num15 = (int)((double)num1 * 1.0);
                for (int index = 0; index < resourceImages.Length; ++index)
                {
                    int height = num1;
                    int width = (int)((double)num1 / (double)resourceImages[index].Height * (double)resourceImages[index].Width);
                    if (width > num15)
                    {
                        double num16 = (double)num15 / (double)width;
                        width = (int)((double)width * num16);
                        height = (int)((double)height * num16);
                    }
                    InfoPanel.ResourceImages[index] = InfoPanel.PrescaleImageStatic(resourceImages[index], width, height);
                }
                if (InfoPanel.ResourceImagesLarge != null)
                {
                    for (int index = 0; index < InfoPanel.ResourceImagesLarge.Length; ++index)
                    {
                        if (InfoPanel.ResourceImagesLarge[index] != null)
                        {
                            InfoPanel.ResourceImagesLarge[index].Dispose();
                            InfoPanel.ResourceImagesLarge[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.ResourceImagesLarge = new Bitmap[resourceImages.Length];
                int num17 = (int)((double)num5 * 1.0);
                for (int index = 0; index < resourceImages.Length; ++index)
                {
                    int height = num5;
                    int width = (int)((double)num5 / (double)resourceImages[index].Height * (double)resourceImages[index].Width);
                    if (width > num17)
                    {
                        double num18 = (double)num17 / (double)width;
                        width = (int)((double)width * num18);
                        height = (int)((double)height * num18);
                    }
                    InfoPanel.ResourceImagesLarge[index] = InfoPanel.PrescaleImageStatic(resourceImages[index], width, height);
                }
            }
            if (plagueImages != null)
            {
                if (InfoPanel.PlagueImages != null)
                {
                    for (int index = 0; index < InfoPanel.PlagueImages.Length; ++index)
                    {
                        if (InfoPanel.PlagueImages[index] != null)
                        {
                            InfoPanel.PlagueImages[index].Dispose();
                            InfoPanel.PlagueImages[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.PlagueImages = new Bitmap[plagueImages.Length];
                int num19 = (int)((double)num1 * 1.0);
                for (int index = 0; index < plagueImages.Length; ++index)
                {
                    int height = num1;
                    int width = (int)((double)num1 / (double)plagueImages[index].Height * (double)plagueImages[index].Width);
                    if (width > num19)
                    {
                        double num20 = (double)num19 / (double)width;
                        width = (int)((double)width * num20);
                        height = (int)((double)height * num20);
                    }
                    InfoPanel.PlagueImages[index] = InfoPanel.PrescaleImageStatic(plagueImages[index], width, height);
                }
                if (InfoPanel.PlagueImagesLarge != null)
                {
                    for (int index = 0; index < InfoPanel.PlagueImagesLarge.Length; ++index)
                    {
                        if (InfoPanel.PlagueImagesLarge[index] != null)
                        {
                            InfoPanel.PlagueImagesLarge[index].Dispose();
                            InfoPanel.PlagueImagesLarge[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.PlagueImagesLarge = new Bitmap[plagueImages.Length];
                int num21 = (int)((double)num5 * 1.0);
                for (int index = 0; index < plagueImages.Length; ++index)
                {
                    int height = num5;
                    int width = (int)((double)num5 / (double)plagueImages[index].Height * (double)plagueImages[index].Width);
                    if (width > num21)
                    {
                        double num22 = (double)num21 / (double)width;
                        width = (int)((double)width * num22);
                        height = (int)((double)height * num22);
                    }
                    InfoPanel.PlagueImagesLarge[index] = InfoPanel.PrescaleImageStatic(plagueImages[index], width, height);
                }
            }
            if (raceImageCache != null)
            {
                if (InfoPanel.RaceImages != null)
                {
                    for (int index = 0; index < InfoPanel.RaceImages.Length; ++index)
                    {
                        if (InfoPanel.RaceImages[index] != null)
                        {
                            InfoPanel.RaceImages[index].Dispose();
                            InfoPanel.RaceImages[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.RaceImages = new Bitmap[raceImageCache.RaceImagesLength];
                for (int racePictureRef = 0; racePictureRef < InfoPanel.RaceImages.Length; ++racePictureRef)
                    InfoPanel.RaceImages[racePictureRef] = InfoPanel.PrescaleImageStatic(raceImageCache.GetRaceImage(racePictureRef), num1, num1);
                if (InfoPanel.RaceImagesLarge != null)
                {
                    for (int index = 0; index < InfoPanel.RaceImagesLarge.Length; ++index)
                    {
                        if (InfoPanel.RaceImagesLarge[index] != null)
                        {
                            InfoPanel.RaceImagesLarge[index].Dispose();
                            InfoPanel.RaceImagesLarge[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.RaceImagesLarge = new Bitmap[raceImageCache.RaceImagesLength];
                for (int racePictureRef = 0; racePictureRef < InfoPanel.RaceImagesLarge.Length; ++racePictureRef)
                    InfoPanel.RaceImagesLarge[racePictureRef] = InfoPanel.PrescaleImageStatic(raceImageCache.GetRaceImage(racePictureRef), num5, num5);
            }
            if (builtObjectImages != null)
            {
                if (InfoPanel.BuiltObjectImages != null)
                {
                    for (int index = 0; index < InfoPanel.BuiltObjectImages.Length; ++index)
                    {
                        if (InfoPanel.BuiltObjectImages[index] != null)
                        {
                            InfoPanel.BuiltObjectImages[index].Dispose();
                            InfoPanel.BuiltObjectImages[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.BuiltObjectImages = new Bitmap[builtObjectImages.Length];
                for (int index = 0; index < builtObjectImages.Length; ++index)
                {
                    Bitmap originalBitmap = InfoPanel.CopyBitmap(builtObjectImages[index]);
                    originalBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    originalBitmap.MakeTransparent(Color.Black);
                    InfoPanel.BuiltObjectImages[index] = InfoPanel.PrescaleImageStatic(originalBitmap, num2, num2);
                    originalBitmap.Dispose();
                }
                if (InfoPanel.BuiltObjectImagesLarge != null)
                {
                    for (int index = 0; index < InfoPanel.BuiltObjectImagesLarge.Length; ++index)
                    {
                        if (InfoPanel.BuiltObjectImagesLarge[index] != null)
                        {
                            InfoPanel.BuiltObjectImagesLarge[index].Dispose();
                            InfoPanel.BuiltObjectImagesLarge[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.BuiltObjectImagesLarge = new Bitmap[builtObjectImages.Length];
                for (int index = 0; index < builtObjectImages.Length; ++index)
                {
                    Bitmap originalBitmap = InfoPanel.CopyBitmap(builtObjectImages[index]);
                    originalBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    originalBitmap.MakeTransparent(Color.Black);
                    InfoPanel.BuiltObjectImagesLarge[index] = InfoPanel.PrescaleImageStatic(originalBitmap, num6, num6);
                    originalBitmap.Dispose();
                }
            }
            if (fighterImages != null)
            {
                if (InfoPanel.FighterImages != null)
                {
                    for (int index = 0; index < InfoPanel.FighterImages.Length; ++index)
                    {
                        if (InfoPanel.FighterImages[index] != null)
                        {
                            InfoPanel.FighterImages[index].Dispose();
                            InfoPanel.FighterImages[index] = (Bitmap)null;
                        }
                    }
                }
                if (InfoPanel.FighterImagesFaded != null)
                {
                    for (int index = 0; index < InfoPanel.FighterImagesFaded.Length; ++index)
                    {
                        if (InfoPanel.FighterImagesFaded[index] != null)
                        {
                            InfoPanel.FighterImagesFaded[index].Dispose();
                            InfoPanel.FighterImagesFaded[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.FighterImages = new Bitmap[fighterImages.Length];
                InfoPanel.FighterImagesFaded = new Bitmap[fighterImages.Length];
                for (int index = 0; index < fighterImages.Length; ++index)
                {
                    Bitmap originalBitmap = InfoPanel.CopyBitmap(fighterImages[index]);
                    originalBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    originalBitmap.MakeTransparent(Color.Black);
                    InfoPanel.FighterImages[index] = InfoPanel.PrescaleImageStatic(originalBitmap, num1, num1);
                    InfoPanel.FighterImagesFaded[index] = InfoPanel.FadeImageStatic(InfoPanel.FighterImages[index], 0.4f);
                    originalBitmap.Dispose();
                }
                if (InfoPanel.FighterImagesLarge != null)
                {
                    for (int index = 0; index < InfoPanel.FighterImagesLarge.Length; ++index)
                    {
                        if (InfoPanel.FighterImagesLarge[index] != null)
                        {
                            InfoPanel.FighterImagesLarge[index].Dispose();
                            InfoPanel.FighterImagesLarge[index] = (Bitmap)null;
                        }
                    }
                }
                if (InfoPanel.FighterImagesFadedLarge != null)
                {
                    for (int index = 0; index < InfoPanel.FighterImagesFadedLarge.Length; ++index)
                    {
                        if (InfoPanel.FighterImagesFadedLarge[index] != null)
                        {
                            InfoPanel.FighterImagesFadedLarge[index].Dispose();
                            InfoPanel.FighterImagesFadedLarge[index] = (Bitmap)null;
                        }
                    }
                }
                InfoPanel.FighterImagesLarge = new Bitmap[fighterImages.Length];
                InfoPanel.FighterImagesFadedLarge = new Bitmap[fighterImages.Length];
                for (int index = 0; index < fighterImages.Length; ++index)
                {
                    Bitmap originalBitmap = InfoPanel.CopyBitmap(fighterImages[index]);
                    originalBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    originalBitmap.MakeTransparent(Color.Black);
                    InfoPanel.FighterImagesLarge[index] = InfoPanel.PrescaleImageStatic(originalBitmap, num5, num5);
                    InfoPanel.FighterImagesFadedLarge[index] = InfoPanel.FadeImageStatic(InfoPanel.FighterImagesLarge[index], 0.4f);
                    originalBitmap.Dispose();
                }
            }
            if (habitatImages == null)
                return;
            if (InfoPanel.HabitatImages != null)
            {
                for (int index = 0; index < InfoPanel.HabitatImages.Length; ++index)
                {
                    if (InfoPanel.HabitatImages[index] != null)
                    {
                        InfoPanel.HabitatImages[index].Dispose();
                        InfoPanel.HabitatImages[index] = (Bitmap)null;
                    }
                }
            }
            InfoPanel.HabitatImages = new Bitmap[habitatImages.Length];
            for (int index = 0; index < habitatImages.Length; ++index)
                InfoPanel.HabitatImages[index] = InfoPanel.PrescaleImageStatic(habitatImages[index], num4, num4);
            if (InfoPanel.HabitatImagesLarge != null)
            {
                for (int index = 0; index < InfoPanel.HabitatImagesLarge.Length; ++index)
                {
                    if (InfoPanel.HabitatImagesLarge[index] != null)
                    {
                        InfoPanel.HabitatImagesLarge[index].Dispose();
                        InfoPanel.HabitatImagesLarge[index] = (Bitmap)null;
                    }
                }
            }
            InfoPanel.HabitatImagesLarge = new Bitmap[habitatImages.Length];
            for (int index = 0; index < habitatImages.Length; ++index)
                InfoPanel.HabitatImagesLarge[index] = InfoPanel.PrescaleImageStatic(habitatImages[index], num8, num8);
        }

        private static void InitializeTroopImageArray(
          Bitmap[] sourceImages,
          ref Bitmap[] troopImages,
          ref Bitmap[] troopImagesFaded,
          int troopMaximumSize)
        {
            if (troopImages != null)
            {
                for (int index = 0; index < troopImages.Length; ++index)
                {
                    if (troopImages[index] != null)
                    {
                        troopImages[index].Dispose();
                        troopImages[index] = (Bitmap)null;
                    }
                }
            }
            if (troopImagesFaded != null)
            {
                for (int index = 0; index < troopImagesFaded.Length; ++index)
                {
                    if (troopImagesFaded[index] != null)
                    {
                        troopImagesFaded[index].Dispose();
                        troopImagesFaded[index] = (Bitmap)null;
                    }
                }
            }
            troopImages = new Bitmap[sourceImages.Length];
            troopImagesFaded = new Bitmap[troopImages.Length];
            for (int index = 0; index < troopImages.Length; ++index)
            {
                Size size = InfoPanel.ResolveImageSize(sourceImages[index], troopMaximumSize);
                Bitmap image = InfoPanel.PrescaleImageStatic(sourceImages[index], size.Width, size.Height);
                Bitmap bitmap1 = image;
                Bitmap bitmap2 = InfoPanel.MakeImageSquare(image, troopMaximumSize);
                bitmap1.Dispose();
                troopImages[index] = bitmap2;
            }
            for (int index = 0; index < troopImagesFaded.Length; ++index)
            {
                Size size = InfoPanel.ResolveImageSize(sourceImages[index], troopMaximumSize);
                Bitmap image1 = InfoPanel.PrescaleImageStatic(sourceImages[index], size.Width, size.Height);
                Bitmap bitmap3 = image1;
                Bitmap image2 = InfoPanel.MakeImageSquare(image1, troopMaximumSize);
                bitmap3.Dispose();
                Bitmap bitmap4 = image2;
                Bitmap bitmap5 = InfoPanel.FadeImageStatic(image2, 0.4f);
                bitmap4.Dispose();
                troopImagesFaded[index] = bitmap5;
            }
        }

        public void RepointImageInstances()
        {
            if (this._ContentSizeIsLarge)
            {
                this._TroopImagesInfantry = InfoPanel.TroopImagesInfantryLarge;
                this._TroopImagesFadedInfantry = InfoPanel.TroopImagesFadedInfantryLarge;
                this._TroopImagesArmored = InfoPanel.TroopImagesArmoredLarge;
                this._TroopImagesFadedArmored = InfoPanel.TroopImagesFadedArmoredLarge;
                this._TroopImagesArtillery = InfoPanel.TroopImagesArtilleryLarge;
                this._TroopImagesFadedArtillery = InfoPanel.TroopImagesFadedArtilleryLarge;
                this._TroopImagesSpecialForces = InfoPanel.TroopImagesSpecialForcesLarge;
                this._TroopImagesFadedSpecialForces = InfoPanel.TroopImagesFadedSpecialForcesLarge;
                this._TroopImagesPirateRaider = InfoPanel.TroopImagesPirateRaiderLarge;
                this._TroopImagesFadedPirateRaider = InfoPanel.TroopImagesFadedPirateRaiderLarge;
                this._ResourceImages = InfoPanel.ResourceImagesLarge;
                this._RaceImages = InfoPanel.RaceImagesLarge;
                this._BuiltObjectImages = InfoPanel.BuiltObjectImagesLarge;
                this._FighterImages = InfoPanel.FighterImagesLarge;
                this._FighterImagesFaded = InfoPanel.FighterImagesFadedLarge;
                this._RuinImages = InfoPanel.RuinImagesLarge;
                this._FacilityImages = InfoPanel.FacilityImagesLarge;
                this._FacilityImagesFaded = InfoPanel.FacilityImagesFadedLarge;
                this._HabitatImages = InfoPanel.HabitatImagesLarge;
                this._PlagueImages = InfoPanel.PlagueImagesLarge;
            }
            else
            {
                this._TroopImagesInfantry = InfoPanel.TroopImagesInfantry;
                this._TroopImagesFadedInfantry = InfoPanel.TroopImagesFadedInfantry;
                this._TroopImagesArmored = InfoPanel.TroopImagesArmored;
                this._TroopImagesFadedArmored = InfoPanel.TroopImagesFadedArmored;
                this._TroopImagesArtillery = InfoPanel.TroopImagesArtillery;
                this._TroopImagesFadedArtillery = InfoPanel.TroopImagesFadedArtillery;
                this._TroopImagesSpecialForces = InfoPanel.TroopImagesSpecialForces;
                this._TroopImagesFadedSpecialForces = InfoPanel.TroopImagesFadedSpecialForces;
                this._TroopImagesPirateRaider = InfoPanel.TroopImagesPirateRaider;
                this._TroopImagesFadedPirateRaider = InfoPanel.TroopImagesFadedPirateRaider;
                this._ResourceImages = InfoPanel.ResourceImages;
                this._RaceImages = InfoPanel.RaceImages;
                this._BuiltObjectImages = InfoPanel.BuiltObjectImages;
                this._FighterImages = InfoPanel.FighterImages;
                this._FighterImagesFaded = InfoPanel.FighterImagesFaded;
                this._RuinImages = InfoPanel.RuinImages;
                this._FacilityImages = InfoPanel.FacilityImages;
                this._FacilityImagesFaded = InfoPanel.FacilityImagesFaded;
                this._HabitatImages = InfoPanel.HabitatImages;
                this._PlagueImages = InfoPanel.PlagueImages;
            }
        }

        public void SetContentSizeNormal()
        {
            this._ImageSize = 14;
            this._ImageScaleSize = 24;
            this._HabitatImageSize = 26;
            this._TinyFont = this._TinyFontNormalSize;
            this._NormalFont = this._NormalFontNormalSize;
            this._NormalFontBold = this._NormalBoldFontNormalSize;
            this._TitleFont = this._TinyFontNormalSize;
            this._RowHeight = 15;
            this._FlagSizeSmall = new Size(30, 18);
            this._FlagSizeSystem = new Size(20, 12);
            this._MinPictureSize = 60;
            this._MaxPictureSize = 200;
            this._RuinImageHeight = 18;
            this._MaxGraphTextWidth = 200;
            this._PopulationTextWidth = 75;
            this._PopulationAmountWidth = 48;
            this._LabelWidth = 65;
            this._LabelWidthHabitat = 70;
            this._Height2 = 2;
            this._Height3 = 3;
            this._Height4 = 4;
            this._Height5 = 5;
            this._Height6 = 6;
            this._Height8 = 8;
            this._Height10 = 10;
            this._ColonySummaryDetailWidth = 20;
            this.RepointImageInstances();
        }

        public void SetContentSizeLarge()
        {
            this._ImageSize = 20;
            this._ImageScaleSize = 34;
            this._HabitatImageSize = 36;
            this._TinyFont = this._TinyFontLargeSize;
            this._NormalFont = this._NormalFontLargeSize;
            this._NormalFontBold = this._NormalBoldFontLargeSize;
            this._TitleFont = this._TinyFontLargeSize;
            this._RowHeight = 21;
            this._FlagSizeSmall = new Size(42, 25);
            this._FlagSizeSystem = new Size(28, 17);
            this._MinPictureSize = 84;
            this._MaxPictureSize = 280;
            this._RuinImageHeight = 25;
            this._MaxGraphTextWidth = 280;
            this._PopulationTextWidth = 105;
            this._PopulationAmountWidth = 67;
            this._LabelWidth = 91;
            this._LabelWidthHabitat = 98;
            this._Height2 = 3;
            this._Height3 = 4;
            this._Height4 = 6;
            this._Height5 = 7;
            this._Height6 = 8;
            this._Height8 = 11;
            this._Height10 = 14;
            this._ColonySummaryDetailWidth = 28;
            this.RepointImageInstances();
        }

        public void DrawBarGraph(
          string description,
          int descriptionWidth,
          int maximumValue,
          int currentValue,
          int height,
          int overallWidth,
          Color fillColorStart,
          Color fillColorEnd,
          Color backgroundColor,
          Graphics graphics,
          Point location)
        {
            this.DrawBarGraph(description, descriptionWidth, maximumValue, currentValue, height, overallWidth, fillColorStart, fillColorEnd, Color.Empty, Color.Empty, backgroundColor, graphics, location, string.Empty);
        }

        public void DrawBarGraph(
          string description,
          int descriptionWidth,
          int maximumValue,
          int currentValue,
          int height,
          int overallWidth,
          Color fillColorStart,
          Color fillColorEnd,
          Color backgroundColor,
          Graphics graphics,
          Point location,
          string suffixData)
        {
            this.DrawBarGraph(description, descriptionWidth, maximumValue, currentValue, height, overallWidth, fillColorStart, fillColorEnd, Color.Empty, Color.Empty, backgroundColor, graphics, location, suffixData);
        }

        public void DrawBarGraph(
          string description,
          int descriptionWidth,
          int maximumValue,
          int currentValue,
          int height,
          int overallWidth,
          Color fillColorStart,
          Color fillColorEnd,
          Color alternateFillColorStart,
          Color alternateFillColorEnd,
          Color backgroundColor,
          Graphics graphics,
          Point location)
        {
            this.DrawBarGraph(description, descriptionWidth, maximumValue, currentValue, height, overallWidth, fillColorStart, fillColorEnd, backgroundColor, graphics, location, string.Empty);
        }

        public void DrawBarGraph(
          string description,
          int descriptionWidth,
          int maximumValue,
          int currentValue,
          int height,
          int overallWidth,
          Color fillColorStart,
          Color fillColorEnd,
          Color alternateFillColorStart,
          Color alternateFillColorEnd,
          Color backgroundColor,
          Graphics graphics,
          Point location,
          string suffixData)
        {
            Color color1 = fillColorStart;
            Color color2 = fillColorEnd;
            if (alternateFillColorStart != Color.Empty)
                color1 = this.UpdateColor(fillColorStart, alternateFillColorStart);
            if (alternateFillColorEnd != Color.Empty)
                color2 = this.UpdateColor(fillColorEnd, alternateFillColorEnd);
            int num = (int)graphics.MeasureString("99999", this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width + 5;
            int width1 = (int)graphics.MeasureString(description, this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (descriptionWidth - width1), location.Y - 2);
            descriptionWidth += 10;
            Point point1 = new Point(location.X + descriptionWidth, location.Y);
            int width2 = overallWidth - (descriptionWidth + num);
            int width3 = (int)((double)currentValue / (double)maximumValue * (double)width2);
            if (width3 > width2)
                width3 = width2;
            string str = currentValue.ToString() + suffixData;
            string text = maximumValue.ToString();
            if (description == TextResolver.GetText("Super Laser") || description == TextResolver.GetText("Status"))
            {
                text = text + " " + TextResolver.GetText("seconds abbreviation");
                str = suffixData;
            }
            int width4 = (int)graphics.MeasureString(str, this._NormalFont).Width;
            Point point2 = new Point(point1.X + (width2 - width4) / 2, point1.Y);
            Point location2 = new Point(point1.X + width2 + 5, point1.Y);
            Rectangle rect1 = new Rectangle(point1.X, point1.Y, width2, height);
            Rectangle rect2 = new Rectangle(point1.X, point1.Y, width3, height);
            Rectangle rect3 = new Rectangle(point1.X - 1, point1.Y, width3 + 2, height);
            LinearGradientBrush linearGradientBrush = (LinearGradientBrush)null;
            if (rect2.Width > 0)
                linearGradientBrush = new LinearGradientBrush(rect3, color1, color2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            SolidBrush solidBrush = new SolidBrush(backgroundColor);
            this.DrawStringWithDropShadow(graphics, description, this._NormalFontBold, location1);
            graphics.FillRectangle((Brush)solidBrush, rect1);
            if (rect2.Width > 0)
                graphics.FillRectangle((Brush)linearGradientBrush, rect2);
            graphics.DrawString(str, this._NormalFont, (Brush)this._WhiteBrush, new PointF((float)point2.X, (float)point2.Y));
            this.DrawStringWithDropShadow(graphics, text, this._NormalFont, location2);
            linearGradientBrush?.Dispose();
            solidBrush.Dispose();
        }

        private void DrawFacilities(
          int labelWidth,
          Habitat habitat,
          PlanetaryFacilityList facilities,
          Graphics graphics,
          Point location,
          int overallWidth)
        {
            int width = (int)graphics.MeasureString(TextResolver.GetText("Facilities"), this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (labelWidth - width), location.Y - 2);
            labelWidth += 10;
            this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Facilities"), this._NormalFontBold, location1);
            int num = labelWidth;
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(190, 24, 24, 32));
            Rectangle rect = new Rectangle(location.X + labelWidth, location.Y, overallWidth - labelWidth, this._ImageSize);
            graphics.FillRectangle((Brush)solidBrush, rect);
            if (facilities == null || facilities.Count == 0)
            {
                this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", this._NormalFont, new Point(location.X + labelWidth, location.Y));
            }
            else
            {
                for (int index = 0; index < facilities.Count; ++index)
                {
                    PlanetaryFacility facility = facilities[index];
                    if (facility != null)
                    {
                        Point point = new Point(location.X + num, location.Y);
                        Bitmap facilityImage = this._FacilityImages[(int)facility.PictureRef];
                        string str = facility.Name;
                        if ((double)facility.ConstructionProgress < 1.0)
                        {
                            facilityImage = this._FacilityImagesFaded[(int)facility.PictureRef];
                            str = str + " (" + facility.ConstructionProgress.ToString("0%") + " " + TextResolver.GetText("Complete").ToLower(CultureInfo.InvariantCulture) + ")";
                        }
                        switch (facility.Type)
                        {
                            case PlanetaryFacilityType.PirateBase:
                            case PlanetaryFacilityType.PirateFortress:
                            case PlanetaryFacilityType.PirateCriminalNetwork:
                                PirateColonyControl byFacilityControl = habitat.GetPirateControl().GetByFacilityControl();
                                if (byFacilityControl != null && byFacilityControl.HasFacilityControl)
                                {
                                    Empire empireById = this._Galaxy.GetEmpireById((int)byFacilityControl.EmpireId);
                                    if (empireById != null)
                                    {
                                        str = str + " (" + empireById.Name + ")";
                                        break;
                                    }
                                    break;
                                }
                                break;
                        }
                        string message = str + " (" + TextResolver.GetText("click for details") + ")";
                        graphics.DrawImageUnscaled((Image)facilityImage, point);
                        this.AddHotspot(new Rectangle(point.X, point.Y, facilityImage.Width, facilityImage.Height), (object)facility, message);
                        num += facilityImage.Width + 2;
                    }
                }
            }
        }

        private void DrawTroopsAgents(
          int labelWidth,
          TroopList troops,
          TroopList troopsRecruiting,
          TroopList troopsInvading,
          CharacterList characters,
          CharacterList invadingCharacters,
          Graphics graphics,
          Point location,
          int overallWidth,
          int offsetX)
        {
            this.DrawTroopsAgents(labelWidth, troops, troopsRecruiting, troopsInvading, characters, invadingCharacters, graphics, location, overallWidth, offsetX, string.Empty);
        }

        public void DrawTroopsAgents(
          int labelWidth,
          TroopList troops,
          TroopList troopsRecruiting,
          TroopList troopsInvading,
          CharacterList characters,
          CharacterList invadingCharacters,
          Graphics graphics,
          Point location,
          int overallWidth,
          int offsetX,
          string prefix)
        {
            int width1 = (int)graphics.MeasureString(TextResolver.GetText("Troops"), this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (labelWidth - width1), location.Y - 2);
            labelWidth += 10;
            int num1 = 0;
            if (!string.IsNullOrEmpty(prefix))
            {
                num1 = 2 + (int)graphics.MeasureString(prefix, this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                Point location2 = new Point(location.X + labelWidth, location.Y);
                this.DrawStringWithDropShadow(graphics, prefix, this._NormalFont, location2);
            }
            labelWidth += num1;
            int num2 = overallWidth - labelWidth;
            if (troops == null)
                troops = new TroopList();
            if (troopsRecruiting == null)
                troopsRecruiting = new TroopList();
            if (troopsInvading == null)
                troopsInvading = new TroopList();
            if (characters == null)
                characters = new CharacterList();
            if (invadingCharacters == null)
                invadingCharacters = new CharacterList();
            int num3 = troops.Count * this._ImageSize;
            int num4 = troopsRecruiting.Count * this._ImageSize;
            int num5 = troopsInvading.Count * this._ImageSize;
            int num6 = characters.Count * this._ImageSize;
            int num7 = invadingCharacters.Count * this._ImageSize;
            int num8 = this._ImageSize / 2;
            int num9 = num6 + num8 + num4 + num8 + num3 + num8 + num5 + num8 + num7;
            int width2 = this._ImageSize;
            if (num9 > num2)
            {
                int num10 = num9 - num8 * 3;
                width2 = (int)(((double)num2 - (double)num8 * 3.0) / (double)num10 * (double)this._ImageSize);
                if (width2 > this._ImageSize)
                    width2 = this._ImageSize;
                if (width2 <= 0)
                    width2 = 1;
            }
            this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Troops"), this._NormalFontBold, location1);
            int num11 = labelWidth + offsetX;
            SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb(190, 24, 24, 32));
            Rectangle rect1 = new Rectangle(location.X + labelWidth + offsetX, location.Y, overallWidth - labelWidth, this._ImageSize);
            graphics.FillRectangle((Brush)solidBrush1, rect1);
            if (troopsInvading.Count == 0 && troops.Count == 0 && troopsRecruiting.Count == 0 && characters.Count == 0 && invadingCharacters.Count == 0)
                this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", this._NormalFont, new Point(location.X + labelWidth, location.Y));
            if (characters.Count > 0)
            {
                for (int index = 0; index < characters.Count; ++index)
                {
                    Character character = characters[index];
                    if (character != null)
                    {
                        Point point = new Point(location.X + num11, location.Y);
                        Bitmap characterImageVerySmall = InfoPanel._CharacterImageCache.ObtainCharacterImageVerySmall(character);
                        if (characterImageVerySmall != null && characterImageVerySmall.PixelFormat != PixelFormat.Undefined)
                        {
                            graphics.DrawImageUnscaled((Image)characterImageVerySmall, point);
                            if (character.Empire == this._Galaxy.PlayerEmpire)
                            {
                                string message = characters[index].Name + " (" + Galaxy.ResolveDescription(character.Role) + ", " + character.Empire.Name + ")" + "   (" + TextResolver.GetText("click for details") + ")";
                                this.AddHotspot(new Rectangle(point.X, point.Y, width2, characterImageVerySmall.Height), (object)character, message);
                            }
                            else if (character.Empire != null)
                                this.AddHotspot(new Rectangle(point.X, point.Y, width2, characterImageVerySmall.Height), (object)character, character.Name + " (" + Galaxy.ResolveDescription(character.Role) + ", " + character.Empire.Name + ")");
                            else
                                this.AddHotspot(new Rectangle(point.X, point.Y, width2, characterImageVerySmall.Height), (object)null, character.Name + " (" + Galaxy.ResolveDescription(character.Role) + ", " + TextResolver.GetText("No Empire") + ")");
                            num11 += width2;
                        }
                    }
                }
                num11 += num8;
            }
            if (troopsRecruiting.Count > 0)
            {
                int num12 = num11;
                for (int index = 0; index < troopsRecruiting.Count; ++index)
                {
                    Troop troop = troopsRecruiting[index];
                    if (troop != null && troop.Garrisoned)
                    {
                        Point point = new Point(location.X + num11, location.Y);
                        Bitmap imagesSpecialForce = this._TroopImagesInfantry[troop.PictureRef];
                        switch (troop.Type)
                        {
                            case TroopType.Infantry:
                                imagesSpecialForce = this._TroopImagesInfantry[troop.PictureRef];
                                break;
                            case TroopType.Armored:
                                imagesSpecialForce = this._TroopImagesArmored[troop.PictureRef];
                                break;
                            case TroopType.Artillery:
                                imagesSpecialForce = this._TroopImagesArtillery[troop.PictureRef];
                                break;
                            case TroopType.SpecialForces:
                                imagesSpecialForce = this._TroopImagesSpecialForces[troop.PictureRef];
                                break;
                            case TroopType.PirateRaider:
                                imagesSpecialForce = this._TroopImagesPirateRaider[troop.PictureRef];
                                break;
                        }
                        using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(0, 128, 0)))
                            graphics.FillRectangle((Brush)solidBrush2, point.X, point.Y, imagesSpecialForce.Width, imagesSpecialForce.Height);
                    }
                    num11 += width2;
                }
                num11 = num12;
            }
            if (troopsRecruiting.Count > 0)
            {
                for (int index = 0; index < troopsRecruiting.Count; ++index)
                {
                    Troop troop = troopsRecruiting[index];
                    if (troop != null)
                    {
                        Point point = new Point(location.X + num11, location.Y);
                        Bitmap fadedSpecialForce = this._TroopImagesFadedInfantry[troop.PictureRef];
                        switch (troop.Type)
                        {
                            case TroopType.Infantry:
                                fadedSpecialForce = this._TroopImagesFadedInfantry[troop.PictureRef];
                                break;
                            case TroopType.Armored:
                                fadedSpecialForce = this._TroopImagesFadedArmored[troop.PictureRef];
                                break;
                            case TroopType.Artillery:
                                fadedSpecialForce = this._TroopImagesFadedArtillery[troop.PictureRef];
                                break;
                            case TroopType.SpecialForces:
                                fadedSpecialForce = this._TroopImagesFadedSpecialForces[troop.PictureRef];
                                break;
                            case TroopType.PirateRaider:
                                fadedSpecialForce = this._TroopImagesFadedPirateRaider[troop.PictureRef];
                                break;
                        }
                        graphics.DrawImageUnscaled((Image)fadedSpecialForce, point);
                        if (troop.Empire == this._Galaxy.PlayerEmpire)
                        {
                            string str = TextResolver.GetText("Recruiting") + " " + troop.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop) + ", " + troop.Empire.Name;
                            if (troop.Garrisoned)
                                str = str + ", " + TextResolver.GetText("Garrisoned");
                            string message = str + ")";
                            this.AddHotspot(new Rectangle(point.X, point.Y, width2, fadedSpecialForce.Height), (object)troop, message);
                        }
                        else
                            this.AddHotspot(new Rectangle(point.X, point.Y, width2, fadedSpecialForce.Height), (object)null, TextResolver.GetText("Recruiting") + " " + troop.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop) + ", " + troop.Empire.Name + ")");
                        num11 += width2;
                    }
                }
            }
            if (troops.Count > 0)
            {
                int num13 = num11;
                for (int index = 0; index < troops.Count; ++index)
                {
                    Troop troop = troops[index];
                    if (troop != null && troop.Garrisoned)
                    {
                        Point point = new Point(location.X + num11, location.Y);
                        Bitmap imagesSpecialForce = this._TroopImagesInfantry[troop.PictureRef];
                        switch (troop.Type)
                        {
                            case TroopType.Infantry:
                                imagesSpecialForce = this._TroopImagesInfantry[troop.PictureRef];
                                break;
                            case TroopType.Armored:
                                imagesSpecialForce = this._TroopImagesArmored[troop.PictureRef];
                                break;
                            case TroopType.Artillery:
                                imagesSpecialForce = this._TroopImagesArtillery[troop.PictureRef];
                                break;
                            case TroopType.SpecialForces:
                                imagesSpecialForce = this._TroopImagesSpecialForces[troop.PictureRef];
                                break;
                            case TroopType.PirateRaider:
                                imagesSpecialForce = this._TroopImagesPirateRaider[troop.PictureRef];
                                break;
                        }
                        using (SolidBrush solidBrush3 = new SolidBrush(Color.FromArgb(0, 128, 0)))
                            graphics.FillRectangle((Brush)solidBrush3, point.X, point.Y, imagesSpecialForce.Width, imagesSpecialForce.Height);
                    }
                    num11 += width2;
                }
                num11 = num13;
            }
            if (troops.Count > 0)
            {
                for (int index = 0; index < troops.Count; ++index)
                {
                    Troop troop = troops[index];
                    if (troop != null)
                    {
                        Point point = new Point(location.X + num11, location.Y);
                        Bitmap imagesSpecialForce = this._TroopImagesInfantry[troop.PictureRef];
                        switch (troop.Type)
                        {
                            case TroopType.Infantry:
                                imagesSpecialForce = this._TroopImagesInfantry[troop.PictureRef];
                                break;
                            case TroopType.Armored:
                                imagesSpecialForce = this._TroopImagesArmored[troop.PictureRef];
                                break;
                            case TroopType.Artillery:
                                imagesSpecialForce = this._TroopImagesArtillery[troop.PictureRef];
                                break;
                            case TroopType.SpecialForces:
                                imagesSpecialForce = this._TroopImagesSpecialForces[troop.PictureRef];
                                break;
                            case TroopType.PirateRaider:
                                imagesSpecialForce = this._TroopImagesPirateRaider[troop.PictureRef];
                                break;
                        }
                        graphics.DrawImageUnscaled((Image)imagesSpecialForce, point);
                        if (troop.Empire == this._Galaxy.PlayerEmpire)
                        {
                            string str = troop.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop) + ", " + troop.Empire.Name;
                            if (troop.Garrisoned)
                                str = str + ", " + TextResolver.GetText("Garrisoned");
                            string message = str + ")";
                            this.AddHotspot(new Rectangle(point.X, point.Y, width2, imagesSpecialForce.Height), (object)troop, message);
                        }
                        else if (troop.Empire != null)
                            this.AddHotspot(new Rectangle(point.X, point.Y, width2, imagesSpecialForce.Height), (object)null, troop.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop) + ", " + troop.Empire.Name + ")");
                        else
                            this.AddHotspot(new Rectangle(point.X, point.Y, width2, imagesSpecialForce.Height), (object)null, troop.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop) + ", " + TextResolver.GetText("No Empire") + ")");
                        num11 += width2;
                    }
                }
                num11 += num8;
            }
            if (troopsInvading.Count > 0 || invadingCharacters.Count > 0)
            {
                using (SolidBrush solidBrush4 = new SolidBrush(Color.Red))
                {
                    int width3 = invadingCharacters.Count * width2 + troopsInvading.Count * width2 + 6;
                    if (troopsInvading.Count > 0 && invadingCharacters.Count > 0)
                        width3 += num8;
                    Rectangle rect2 = new Rectangle(location.X + num11, location.Y, width3, this._ImageSize);
                    graphics.FillRectangle((Brush)solidBrush4, rect2);
                }
            }
            if (troopsInvading.Count > 0)
            {
                for (int index = 0; index < troopsInvading.Count; ++index)
                {
                    Troop troop = troopsInvading[index];
                    if (troop != null)
                    {
                        Point point = new Point(location.X + num11, location.Y);
                        Bitmap imagesSpecialForce = this._TroopImagesInfantry[troop.PictureRef];
                        switch (troop.Type)
                        {
                            case TroopType.Infantry:
                                imagesSpecialForce = this._TroopImagesInfantry[troop.PictureRef];
                                break;
                            case TroopType.Armored:
                                imagesSpecialForce = this._TroopImagesArmored[troop.PictureRef];
                                break;
                            case TroopType.Artillery:
                                imagesSpecialForce = this._TroopImagesArtillery[troop.PictureRef];
                                break;
                            case TroopType.SpecialForces:
                                imagesSpecialForce = this._TroopImagesSpecialForces[troop.PictureRef];
                                break;
                            case TroopType.PirateRaider:
                                imagesSpecialForce = this._TroopImagesPirateRaider[troop.PictureRef];
                                break;
                        }
                        graphics.DrawImageUnscaled((Image)imagesSpecialForce, point);
                        if (troop.Empire == this._Galaxy.PlayerEmpire)
                        {
                            string message = TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " " + troop.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop) + ", " + troop.Empire.Name + ")";
                            this.AddHotspot(new Rectangle(point.X, point.Y, width2, imagesSpecialForce.Height), (object)troop, message);
                        }
                        else
                        {
                            string str = TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " " + troop.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop);
                            if (troop.Empire != null)
                                str = str + ", " + troop.Empire.Name;
                            string message = str + ")";
                            this.AddHotspot(new Rectangle(point.X, point.Y, width2, imagesSpecialForce.Height), (object)null, message);
                        }
                        num11 += width2;
                    }
                }
                num11 += num8;
            }
            if (invadingCharacters.Count > 0)
            {
                for (int index = 0; index < invadingCharacters.Count; ++index)
                {
                    Character invadingCharacter = invadingCharacters[index];
                    if (invadingCharacter != null)
                    {
                        Point point = new Point(location.X + num11, location.Y);
                        Bitmap characterImageVerySmall = InfoPanel._CharacterImageCache.ObtainCharacterImageVerySmall(invadingCharacter);
                        if (characterImageVerySmall != null && characterImageVerySmall.PixelFormat != PixelFormat.Undefined)
                        {
                            graphics.DrawImageUnscaled((Image)characterImageVerySmall, point);
                            if (invadingCharacter.Empire == this._Galaxy.PlayerEmpire)
                            {
                                this.AddHotspot(new Rectangle(point.X, point.Y, width2, characterImageVerySmall.Height), (object)invadingCharacter, TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " " + invadingCharacter.Name + " (" + invadingCharacter.Empire.Name + ")");
                            }
                            else
                            {
                                string str = string.Empty;
                                if (invadingCharacter.Empire != null)
                                    str = invadingCharacter.Empire.Name;
                                this.AddHotspot(new Rectangle(point.X, point.Y, width2, characterImageVerySmall.Height), (object)null, TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " " + invadingCharacter.Name + " (" + str + ")");
                            }
                            num11 += width2;
                        }
                    }
                }
                int num14 = num11 + num8;
            }
            solidBrush1.Dispose();
        }

        public void DrawFighters(
          int labelWidth,
          FighterList fighters,
          Graphics graphics,
          Point location,
          int overallWidth)
        {
            int width1 = (int)graphics.MeasureString(TextResolver.GetText("Fighters"), this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (labelWidth - width1), location.Y - 2);
            labelWidth += 10;
            int num1 = overallWidth - labelWidth;
            if (fighters == null)
                fighters = new FighterList();
            int num2 = fighters.Count * this._ImageSize;
            int num3 = this._ImageSize / 2;
            int num4 = num2;
            int width2 = this._ImageSize;
            if (num4 > num1)
            {
                int num5 = num4 - num3 * 3;
                int val2 = (int)(((double)num1 - (double)num3 * 3.0) / (double)num5 * (double)this._ImageSize);
                if (val2 > this._ImageSize)
                    val2 = this._ImageSize;
                width2 = Math.Max(1, val2);
            }
            this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Fighters"), this._NormalFontBold, location1);
            int num6 = labelWidth;
            SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb(190, 24, 24, 32));
            Rectangle rect1 = new Rectangle(location.X + labelWidth, location.Y, overallWidth - labelWidth, this._ImageSize);
            graphics.FillRectangle((Brush)solidBrush1, rect1);
            if (fighters.Count == 0)
            {
                this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", this._NormalFont, new Point(location.X + labelWidth, location.Y));
            }
            else
            {
                FighterList fighterList1 = new FighterList();
                FighterList fighterList2 = new FighterList();
                FighterList fighterList3 = new FighterList();
                for (int index = 0; index < fighters.Count; ++index)
                {
                    if (fighters[index].OnboardCarrier)
                    {
                        if (fighters[index].UnderConstruction)
                            fighterList1.Add(fighters[index]);
                        else
                            fighterList2.Add(fighters[index]);
                    }
                    else
                        fighterList3.Add(fighters[index]);
                }
                for (int index = 0; index < fighterList3.Count; ++index)
                {
                    if ((double)fighterList3[index].Health < 1.0)
                    {
                        using (SolidBrush solidBrush2 = new SolidBrush(Color.Red))
                        {
                            Rectangle rect2 = new Rectangle(location.X + num6, location.Y, this._ImageSize, this._ImageSize);
                            graphics.FillRectangle((Brush)solidBrush2, rect2);
                        }
                    }
                    Point point = new Point(location.X + num6, location.Y);
                    graphics.DrawImageUnscaled((Image)this._FighterImages[(int)fighterList3[index].PictureRef], point);
                    this.AddHotspot(new Rectangle(point.X, point.Y, width2, this._FighterImages[(int)fighterList3[index].PictureRef].Height), (object)fighterList3[index], fighterList3[index].Name + " (" + Galaxy.ResolveMissionDescription(fighterList3[index]) + ")");
                    num6 += width2;
                }
                int num7 = num6 + num3;
                for (int index = 0; index < fighterList2.Count; ++index)
                {
                    if ((double)fighterList2[index].Health < 1.0 && !fighterList2[index].UnderConstruction)
                    {
                        using (SolidBrush solidBrush3 = new SolidBrush(Color.Red))
                        {
                            Rectangle rect3 = new Rectangle(location.X + num7, location.Y, this._ImageSize, this._ImageSize);
                            graphics.FillRectangle((Brush)solidBrush3, rect3);
                        }
                    }
                    Point point = new Point(location.X + num7, location.Y);
                    graphics.DrawImageUnscaled((Image)this._FighterImages[(int)fighterList2[index].PictureRef], point);
                    string str = string.Empty;
                    if (fighterList2[index].ParentBuiltObject != null)
                        str = " (" + TextResolver.GetText("Onboard") + " " + fighterList2[index].ParentBuiltObject.Name + ")";
                    this.AddHotspot(new Rectangle(point.X, point.Y, width2, this._FighterImages[(int)fighterList2[index].PictureRef].Height), (object)fighterList2[index], fighterList2[index].Name + str);
                    num7 += width2;
                }
                int num8 = num7 + num3;
                for (int index = 0; index < fighterList1.Count; ++index)
                {
                    Point point = new Point(location.X + num8, location.Y);
                    graphics.DrawImageUnscaled((Image)this._FighterImagesFaded[(int)fighterList1[index].PictureRef], point);
                    this.AddHotspot(new Rectangle(point.X, point.Y, width2, this._FighterImagesFaded[(int)fighterList1[index].PictureRef].Height), (object)fighterList1[index], fighterList1[index].Name + " (" + TextResolver.GetText("Building") + ")");
                    num8 += width2;
                }
            }
            solidBrush1.Dispose();
        }

        private int DrawPopulation(
          int labelWidth,
          Habitat habitat,
          PopulationList populations,
          Graphics graphics,
          Point location,
          int overallWidth,
          int rowHeight)
        {
            using (SolidBrush textBrush = new SolidBrush(this._WhiteColor))
                return this.DrawPopulation(labelWidth, habitat, populations, graphics, location, overallWidth, rowHeight, textBrush);
        }

        private int DrawPopulation(
          int labelWidth,
          Habitat habitat,
          PopulationList populations,
          Graphics graphics,
          Point location,
          int overallWidth,
          int rowHeight,
          SolidBrush textBrush)
        {
            if (populations != null)
            {
                populations.Sort();
                populations.Reverse();
            }
            int width = (int)graphics.MeasureString(TextResolver.GetText("Populace"), this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (labelWidth - width), location.Y - 2);
            this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Populace"), this._NormalFontBold, location1);
            labelWidth += 10;
            int num1 = 0;
            for (int index1 = 0; index1 < populations.Count; ++index1)
            {
                Population population = populations[index1];
                int num2 = labelWidth;
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(190, 24, 24, 32));
                Rectangle rect = new Rectangle(location.X + num2, location.Y + num1, overallWidth - num2 + 4, this._ImageSize);
                graphics.FillRectangle((Brush)solidBrush, rect);
                Bitmap bitmap = this._RaceImages[population.Race.PictureRef];
                string s = population.Race.Name;
                //string text1 = BaconInfoPanel.FormatForLargeNumbers(population.Amount);
                string text1 = InfoPanel.OnFormatForLargeNumbersMods(population.Amount);
                string text2 = ((double)population.GrowthRate - 1.0).ToString("+#0%;-#0%;0%");
                bool flag = false;
                if (populations.TotalAmount >= habitat.MaximumPopulation)
                {
                    flag = true;
                    text2 = TextResolver.GetText("Maximum Abbreviation");
                }
                if (index1 == 1 && populations.Count > 2)
                {
                    bitmap = (Bitmap)null;
                    s = TextResolver.GetText("Others");
                    long num3 = 0;
                    for (int index2 = 1; index2 < populations.Count; ++index2)
                        num3 += Math.Max(0L, populations[index2].Amount);
                    //text1 = BaconInfoPanel.FormatForLargeNumbers(num3);
                    text1 = InfoPanel.OnFormatForLargeNumbersMods(num3);
                    text2 = string.Empty;
                }
                else if (index1 > 1)
                    break;
                Point point = new Point(location.X + num2, location.Y + num1);
                if (bitmap != null)
                {
                    graphics.DrawImageUnscaled((Image)bitmap, point);
                    if (s != TextResolver.GetText("Others"))
                        this.AddHotspot(new Rectangle(point, bitmap.Size), (object)population.Race, s + " (" + TextResolver.GetText("click for details") + ")");
                }
                int num4 = num2 + this._ImageSize;
                point = new Point(location.X + num4, location.Y + num1);
                Rectangle rectangle = new Rectangle(point.X, point.Y + 1, this._PopulationTextWidth, this._ImageSize);
                StringFormat format = new StringFormat();
                format.Trimming = StringTrimming.EllipsisCharacter;
                graphics.DrawString(s, this._NormalFont, (Brush)this._BlackBrush, (RectangleF)rectangle, format);
                rectangle = new Rectangle(point.X - 1, point.Y, this._PopulationTextWidth, this._ImageSize);
                graphics.DrawString(s, this._NormalFont, (Brush)textBrush, (RectangleF)rectangle, format);
                if (s != TextResolver.GetText("Others"))
                    this.AddHotspot(rectangle, (object)population.Race, s + " (" + TextResolver.GetText("click for details") + ")");
                int num5 = num4 + this._PopulationTextWidth;
                point = new Point(location.X + num5, location.Y + num1);
                this.DrawStringWithDropShadow(graphics, text1, this._NormalFont, point, textBrush);
                int num6 = num5 + this._PopulationAmountWidth;
                point = new Point(location.X + num6, location.Y + num1);
                if (flag)
                    this.DrawStringRedWithDropShadow(graphics, text2, this._NormalFont, point);
                else
                    this.DrawStringWithDropShadow(graphics, text2, this._NormalFont, point, textBrush);
                solidBrush.Dispose();
                num1 += rowHeight;
            }
            if (populations.Count > 1)
            {
                int num7 = labelWidth + this._ImageSize;
                Point location2 = new Point(location.X + num7, location.Y + num1);
                this.DrawStringWithDropShadow(graphics, TextResolver.GetText("TOTAL") + ":", this._NormalFont, location2, textBrush);
                int num8 = num7 + this._PopulationTextWidth;
                location2 = new Point(location.X + num8, location.Y + num1);
                this.DrawStringWithDropShadow(graphics, populations.TotalAmount.ToString("0,,") + "M", this._NormalFont, location2, textBrush);
                int num9 = num8 + this._PopulationAmountWidth;
                location2 = new Point(location.X + num9, location.Y + num1);
                if (populations.TotalAmount >= habitat.MaximumPopulation)
                {
                    this.DrawStringRedWithDropShadow(graphics, TextResolver.GetText("Maximum Abbreviation"), this._NormalFont, location2);
                }
                else
                {
                    double num10 = populations.OverallGrowthRate - 1.0;
                    this.DrawStringWithDropShadow(graphics, num10.ToString("+#0%;-#0%;0%"), this._NormalFont, location2, textBrush);
                }
                num1 += rowHeight;
            }
            if (num1 == 0)
                num1 = rowHeight;
            return num1;
        }

        private void DrawResources(
          int labelWidth,
          HabitatResourceList resources,
          Graphics graphics,
          Point location,
          int overallWidth)
        {
            using (SolidBrush textBrush = new SolidBrush(this._WhiteColor))
                this.DrawResources(labelWidth, resources, graphics, location, overallWidth, textBrush);
        }

        private void DrawResources(
          int labelWidth,
          HabitatResourceList resources,
          Graphics graphics,
          Point location,
          int overallWidth,
          SolidBrush textBrush)
        {
            int width1 = (int)graphics.MeasureString(TextResolver.GetText("Resource"), this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (labelWidth - width1), location.Y - 2);
            this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Resource"), this._NormalFontBold, location1);
            labelWidth += 10;
            overallWidth += 5;
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(190, 24, 24, 32));
            Rectangle rect = new Rectangle(location.X + labelWidth, location.Y, overallWidth - labelWidth, this._ImageSize);
            graphics.FillRectangle((Brush)solidBrush, rect);
            int num1 = labelWidth;
            Point location2 = new Point(location.X + num1, location.Y);
            if (resources == null || resources.Count == 0)
            {
                this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", this._NormalFont, location2, textBrush);
            }
            else
            {
                int num2 = 3;
                HabitatResourceList habitatResourceList = resources.Clone();
                for (int index = 0; index < habitatResourceList.Count; ++index)
                {
                    int width2 = this._ResourceImages[habitatResourceList[index].PictureRef].Width;
                    string text = ((double)habitatResourceList[index].Abundance / 1000.0).ToString("0%");
                    int width3 = (int)graphics.MeasureString(text, this._TinyFont).Width;
                    location2 = new Point(location.X + num1, location.Y);
                    int num3 = Math.Max(0, (this._ImageSize - this._ResourceImages[habitatResourceList[index].PictureRef].Height) / 2);
                    Point point = new Point(location2.X, location2.Y + num3);
                    graphics.DrawImageUnscaled((Image)this._ResourceImages[habitatResourceList[index].PictureRef], point);
                    this.AddHotspot(new Rectangle(point, this._ResourceImages[habitatResourceList[index].PictureRef].Size), (object)resources[index], habitatResourceList[index].Name + " (" + TextResolver.GetText("click for details") + ")");
                    int num4 = num1 + (width2 - 2);
                    location2 = new Point(location.X + num4, location.Y + 1);
                    this.DrawStringWithDropShadow(graphics, text, this._TinyFont, location2, textBrush);
                    num1 = num4 + width3 + num2;
                }
                solidBrush.Dispose();
            }
        }

        public void DrawBuiltObjectList(
          int labelWidth,
          string label,
          BuiltObjectList builtObjects,
          int waitingCount,
          Graphics graphics,
          Point location,
          int overallWidth)
        {
            this.DrawBuiltObjectList(labelWidth, label, builtObjects, waitingCount, graphics, location, overallWidth, string.Empty);
        }

        public void DrawBuiltObjectList(
          int labelWidth,
          string label,
          BuiltObjectList builtObjects,
          int waitingCount,
          Graphics graphics,
          Point location,
          int overallWidth,
          string suffix)
        {
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            int width1 = (int)graphics.MeasureString(label, this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (labelWidth - width1), location.Y - 2);
            labelWidth += 10;
            int num1 = overallWidth - labelWidth;
            if (builtObjects == null)
                builtObjects = new BuiltObjectList();
            int num2 = builtObjects.Count * this._ImageSize;
            int num3 = this._ImageSize / 2;
            string text = waitingCount.ToString() + " " + TextResolver.GetText("waiting");
            int width2 = (int)graphics.MeasureString(text, this._NormalFont).Width;
            int num4 = num2 + num3 + width2;
            int num5 = this._ImageSize;
            if (num4 > num1)
            {
                int num6 = num4 - (num3 + width2);
                num5 = (int)(((double)num1 - ((double)num3 + (double)width2)) / (double)num6 * (double)this._ImageSize);
                if (num5 > this._ImageSize)
                    num5 = this._ImageSize;
            }
            this.DrawStringWithDropShadow(graphics, label, this._NormalFontBold, location1);
            int num7 = labelWidth;
            Point location2 = new Point(location.X + num7, location.Y);
            if (builtObjects.Count == 0)
            {
                int width3 = (int)graphics.MeasureString("(" + TextResolver.GetText("None") + ")", this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", this._NormalFont, location2);
                num7 += width3 + num3;
            }
            else
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                for (int index = 0; index < builtObjects.Count; ++index)
                {
                    location2 = new Point(location.X + num7, location.Y);
                    BuiltObject builtObject = builtObjects[index];
                    if (builtObject != null)
                    {
                        Bitmap bitmap = (Bitmap)null;
                        if (this._BuiltObjectImages.Length > builtObject.PictureRef)
                            bitmap = this._BuiltObjectImages[builtObject.PictureRef];
                        string str = string.Empty;
                        Rectangle rectangle = new Rectangle(location.X + num7, location.Y, this._ImageSize, this._ImageSize);
                        if (bitmap != null && bitmap.PixelFormat != PixelFormat.Undefined)
                        {
                            Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                            if (builtObject.DamagedComponentCount > 0)
                            {
                                if (builtObject.ActualEmpire == this._Game.PlayerEmpire || this._Game.GodMode)
                                    graphics.FillRectangle((Brush)new SolidBrush(Color.FromArgb(64, (int)byte.MaxValue, 0, 64)), rectangle);
                            }
                            else if (builtObject.UnbuiltComponentCount > 0 && (builtObject.ActualEmpire == this._Game.PlayerEmpire || this._Game.GodMode))
                            {
                                graphics.FillRectangle((Brush)new SolidBrush(Color.FromArgb(64, (int)byte.MaxValue, 128, 0)), rectangle);
                                str = TextResolver.GetText("Under construction");
                            }
                            graphics.DrawImage((Image)bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
                        }
                        if (!string.IsNullOrEmpty(str))
                            this.AddHotspot(rectangle, (object)builtObject, builtObject.Name + " (" + str + " - " + TextResolver.GetText("click to select") + ")");
                        else
                            this.AddHotspot(rectangle, (object)builtObject, builtObject.Name + " (" + TextResolver.GetText("click to select") + ")");
                        num7 += num5;
                    }
                }
            }
            if (builtObjects.Count > 0)
                num7 += num3;
            location2 = new Point(location.X + num7, location.Y);
            if (waitingCount > 0)
                this.DrawStringWithDropShadow(graphics, text, this._NormalFont, location2);
            if (string.IsNullOrEmpty(suffix))
                return;
            int num8 = 0;
            if (waitingCount > 0)
                num8 = (int)graphics.MeasureString(text, this._NormalFont, this.Width, StringFormat.GenericDefault).Width + 10;
            location2 = new Point(location.X + num7 + num8, location.Y);
            this.DrawStringWithDropShadow(graphics, suffix, this._NormalFont, location2);
        }

        private Bitmap RotateImage(Image image, float angle)
        {
            float num = image != null ? (float)image.Width : throw new ArgumentNullException(nameof(image));
            float height1 = (float)image.Height;
            angle *= -1f;
            angle *= 57.29578f;
            angle %= 360f;
            if ((double)angle < 0.0)
                angle += 360f;
            PointF[] pts = new PointF[4];
            pts[1].X = num;
            pts[2].X = num;
            pts[2].Y = height1;
            pts[3].Y = height1;
            Matrix matrix = new Matrix();
            matrix.Rotate(angle);
            matrix.TransformPoints(pts);
            double val1_1 = double.MinValue;
            double val1_2 = double.MinValue;
            double val1_3 = double.MaxValue;
            double val1_4 = double.MaxValue;
            foreach (PointF pointF in pts)
            {
                val1_1 = Math.Max(val1_1, (double)pointF.X);
                val1_3 = Math.Min(val1_3, (double)pointF.X);
                val1_2 = Math.Max(val1_2, (double)pointF.Y);
                val1_4 = Math.Min(val1_4, (double)pointF.Y);
            }
            double width = Math.Ceiling(val1_1 - val1_3);
            double height2 = Math.Ceiling(val1_2 - val1_4);
            Bitmap bitmap = new Bitmap((int)width, (int)height2);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                graphics.SmoothingMode = SmoothingMode.None;
                PointF point1 = new PointF((float)(width / 2.0), (float)(height2 / 2.0));
                PointF point2 = new PointF(point1.X - num / 2f, point1.Y - num / 2f);
                matrix.Reset();
                matrix.RotateAt(angle, point1);
                graphics.Transform = matrix;
                graphics.DrawImage(image, point2);
            }
            return bitmap;
        }

        private Bitmap RotateBitmap(Bitmap InputImage, double angle)
        {
            double width = (double)InputImage.Width;
            double height = (double)InputImage.Height;
            Point[] pointArray = new Point[4]
            {
        new Point(0, 0),
        new Point((int) width, 0),
        new Point(0, (int) height),
        new Point((int) width, (int) height)
            };
            double num1 = width / 2.0;
            double num2 = height / 2.0;
            for (int index = 0; index <= 3; ++index)
            {
                pointArray[index].X -= (int)num1;
                pointArray[index].Y -= (int)num2;
            }
            double num3 = angle;
            double num4 = Math.Sin(num3);
            double num5 = Math.Cos(num3);
            for (int index = 0; index <= 3; ++index)
            {
                double x = (double)pointArray[index].X;
                double y = (double)pointArray[index].Y;
                pointArray[index].X = (int)(x * num5 + y * num4);
                pointArray[index].Y = (int)(-x * num4 + y * num5);
            }
            double x1 = (double)pointArray[0].X;
            double y1 = (double)pointArray[0].Y;
            for (int index = 1; index <= 3; ++index)
            {
                if (x1 > (double)pointArray[index].X)
                    x1 = (double)pointArray[index].X;
                if (y1 > (double)pointArray[index].Y)
                    y1 = (double)pointArray[index].Y;
            }
            for (int index = 0; index <= 3; ++index)
            {
                pointArray[index].X -= (int)x1;
                pointArray[index].Y -= (int)y1;
            }
            Bitmap bitmap = new Bitmap((int)(-2.0 * x1), (int)(-2.0 * y1), PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                Point[] destPoints = new Point[3]
                {
          pointArray[0],
          pointArray[1],
          pointArray[2]
                };
                graphics.DrawImage((Image)InputImage, destPoints);
            }
            return bitmap;
        }

        private void DrawCreature(Creature creature, Graphics graphics)
        {
            if (creature.HasBeenDestroyed)
            {
                this._Game.SelectedObject = (object)null;
                this.ClearData();
            }
            else
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                int y1 = 6;
                int rowHeight = this._RowHeight;
                int x = 5;
                int overallWidth = this.ClientRectangle.Width - 10;
                int labelWidth = this._LabelWidth;
                Color backgroundColor = Color.FromArgb((int)sbyte.MaxValue, 8, 8, 48);
                Font titleFont = this._TitleFont;
                this.DrawBackgroundPicture(graphics);
                StringFormat genericTypographic = StringFormat.GenericTypographic;
                SizeF sizeF = graphics.MeasureString(creature.Name, titleFont, this.Width - 20, genericTypographic);
                RectangleF layoutRectangle = new RectangleF((float)x, (float)y1, sizeF.Width, sizeF.Height + 2f);
                graphics.DrawString(creature.Name, titleFont, (Brush)this._WhiteBrush, layoutRectangle, genericTypographic);
                Point location = new Point();
                int y2 = y1 + (int)sizeF.Height + this._Height8;
                location = new Point(x, y2);
                string text = TextResolver.GetText("Size") + ": " + creature.Size.ToString() + ", " + TextResolver.GetText("Attack Strength") + ": " + creature.AttackStrength.ToString();
                this.DrawStringWithDropShadow(graphics, text, this._NormalFont, location);
                int y3 = y2 + rowHeight + this._Height10;
                int y4 = rowHeight + (int)sizeF.Height + 5 + rowHeight;
                Rectangle rect = new Rectangle(x - 2, y4, x + labelWidth - 1, this.ClientRectangle.Height - (y4 + 2));
                graphics.FillRectangle((Brush)this._LabelAreaBrush, rect);
                int descriptionWidth = labelWidth - 5;
                location = new Point(x, y3);
                this.DrawBarGraph(TextResolver.GetText("Health"), descriptionWidth, creature.DamageKillThreshhold, (int)((double)creature.DamageKillThreshhold - creature.Damage), rowHeight - 2, overallWidth, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int)byte.MaxValue), backgroundColor, graphics, location);
                int y5 = y3 + rowHeight + this._Height5;
                location = new Point(x, y5);
                this.DrawBarGraph(TextResolver.GetText("Speed Abbreviation"), descriptionWidth, creature.MovementSpeed, (int)creature.CurrentSpeed, rowHeight - 2, overallWidth, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int)byte.MaxValue), backgroundColor, graphics, location);
                int num = y5 + rowHeight;
            }
        }

        private void DrawFighter(Fighter fighter, Graphics graphics)
        {
            if (fighter.HasBeenDestroyed)
            {
                this._Game.SelectedObject = (object)null;
                this.ClearData();
            }
            else
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                SolidBrush solidBrush = new SolidBrush(this._UnknownColor);
                bool flag = false;
                if (fighter.Empire != this._Game.PlayerEmpire && this._Game.PlayerEmpire.EmpiresViewable.Contains(fighter.Empire))
                    flag = true;
                int y1 = 6;
                int rowHeight = this._RowHeight;
                int x = 5;
                int overallWidth = this.ClientRectangle.Width - 10;
                int width1 = (int)graphics.MeasureString(TextResolver.GetText("Weapons"), this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                Color backgroundColor = Color.FromArgb((int)sbyte.MaxValue, 8, 8, 48);
                Font titleFont = this._TitleFont;
                this.DrawBackgroundPicture(graphics);
                Point point1 = new Point(overallWidth - (this._FlagSizeSmall.Width - 2), 6);
                if (this._Fighter.Empire == this._Galaxy.IndependentEmpire)
                {
                    int width2 = (int)graphics.MeasureString(fighter.Empire.Name, this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                    PointF point2 = new PointF((float)overallWidth - (float)width2, 6f);
                    graphics.DrawString(fighter.Empire.Name, this._NormalFont, (Brush)this._WhiteBrush, point2);
                }
                else
                {
                    graphics.DrawImageUnscaled((Image)this._EmpirePicture, point1);
                    if (this._Fighter.Empire != null)
                        this.AddHotspot(new Rectangle(point1, this._EmpirePicture.Size), (object)this._Fighter.Empire, this._Fighter.Empire.Name + " (" + TextResolver.GetText("click for details") + ")");
                }
                Point location = new Point(x, y1);
                this.DrawStringWithDropShadow(graphics, fighter.Name, titleFont, location, new SolidBrush(this._EmpireColor));
                int y2 = y1 + titleFont.Height + 8;
                location = new Point(x, y2);
                string text1 = "(" + TextResolver.GetText("Unknown mission") + ")";
                int num1;
                if (fighter.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag)
                {
                    if (fighter.Empire == null)
                    {
                        Empire independentEmpire = this._Galaxy.IndependentEmpire;
                    }
                    string text2 = Galaxy.ResolveMissionDescription(fighter);
                    this.DrawStringWithDropShadow(graphics, text2, this._NormalFont, location);
                    num1 = y2 + rowHeight;
                }
                else
                {
                    this.DrawStringWithDropShadow(graphics, text1, this._NormalFont, location, solidBrush);
                    num1 = y2 + rowHeight;
                }
                int y3 = num1 + this._Height5;
                int num2 = width1 + 5;
                int y4 = y3;
                Rectangle rect = new Rectangle(x - 2, y4, x + num2 - 1, this.ClientRectangle.Height - (y4 + 2));
                graphics.FillRectangle((Brush)this._LabelAreaBrush, rect);
                int num3 = num2 - 5;
                location = new Point(x, y3);
                string description1 = "(" + TextResolver.GetText("Abandoned") + ")";
                if (fighter.Empire != null)
                    description1 = fighter.Empire.Name;
                if (fighter.Empire == this._Galaxy.IndependentEmpire)
                    description1 = "(" + TextResolver.GetText("Independent") + ")";
                this.DrawLabelledDescription(graphics, TextResolver.GetText("Empire"), num3, description1, location);
                int y5 = y3 + rowHeight + this._Height5;
                location = new Point(x, y5);
                if (fighter.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag)
                {
                    string suffixData = string.Empty;
                    if (fighter.UnderConstruction)
                        suffixData = " (" + TextResolver.GetText("Under construction").ToLower(CultureInfo.InvariantCulture) + ")";
                    Math.Max(0, (int)fighter.CurrentEnergy);
                    this.DrawBarGraph(TextResolver.GetText("Health"), num3, 100, (int)((double)fighter.Health * 100.0), rowHeight - 2, overallWidth, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int)byte.MaxValue), backgroundColor, graphics, location, suffixData);
                }
                else
                    this.DrawLabelledDescription(graphics, TextResolver.GetText("Health"), num3, "(" + TextResolver.GetText("Unknown") + ")", location, solidBrush);
                int y6 = y5 + rowHeight;
                location = new Point(x, y6);
                if (fighter.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag)
                {
                    int currentValue = Math.Max(0, (int)fighter.CurrentEnergy);
                    this.DrawBarGraph(TextResolver.GetText("Energy"), num3, (int)fighter.Specification.EnergyCapacity, currentValue, rowHeight - 2, overallWidth, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int)byte.MaxValue), backgroundColor, graphics, location);
                }
                else
                    this.DrawLabelledDescription(graphics, TextResolver.GetText("Energy"), num3, "(" + TextResolver.GetText("Unknown") + ")", location, solidBrush);
                int y7 = y6 + rowHeight;
                location = new Point(x, y7);
                string suffixData1 = string.Empty;
                if (fighter.ShieldsReducedLocation)
                    suffixData1 = " (" + TextResolver.GetText("reducing") + ")";
                this.DrawBarGraph(TextResolver.GetText("Shields"), num3, (int)fighter.Specification.ShieldsCapacity, (int)fighter.CurrentShields, rowHeight - 2, overallWidth, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int)byte.MaxValue), backgroundColor, graphics, location, suffixData1);
                int y8 = y7 + rowHeight;
                location = new Point(x, y8);
                string suffixData2 = string.Empty;
                if (fighter.MovementSlowedLocation)
                    suffixData2 = suffixData2 + " (" + TextResolver.GetText("slowed") + ")";
                this.DrawBarGraph(TextResolver.GetText("Speed"), num3, (int)fighter.TopSpeed, (int)fighter.CurrentSpeed, rowHeight - 2, overallWidth, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int)byte.MaxValue), backgroundColor, graphics, location, suffixData2);
                int y9 = y8 + rowHeight + this._Height5;
                location = new Point(x, y9);
                string description2 = TextResolver.GetText("Firepower") + ": " + fighter.FirepowerRaw.ToString() + ", " + TextResolver.GetText("Range") + ": " + fighter.Specification.WeaponRange.ToString();
                if (fighter.FirepowerRaw == 0)
                    description2 = "(" + TextResolver.GetText("None") + ")";
                this.DrawLabelledDescription(graphics, TextResolver.GetText("Weapons"), num3, description2, location);
                int y10 = y9 + rowHeight;
                if (!string.IsNullOrEmpty(this._CharacterBonuses))
                {
                    location = new Point(x, y10);
                    this.DrawLabel(graphics, TextResolver.GetText("Bonuses"), num3, location);
                    int width3 = overallWidth - num3;
                    SizeF size = graphics.MeasureString(this._CharacterBonuses, this._NormalFont, width3, StringFormat.GenericDefault);
                    location = new Point(x + num3 + 10, y10 + 1);
                    this.DrawStringWithDropShadowBounded(graphics, this._CharacterBonuses, this._NormalFont, location, size);
                    int num4 = y10 + (int)size.Height;
                }
                solidBrush.Dispose();
            }
        }

        private void DrawBuiltObject(BuiltObject builtObject, Graphics graphics)
        {
            //BaconInfoPanel.DrawBuiltObject(this, builtObject, graphics);
            InfoPanel.OnDrawBuiltObjectMods(this, builtObject, graphics);
        }

        private void DrawSystemColoniesSummary(
          Habitat systemStar,
          Graphics graphics,
          int startY,
          int labelWidth)
        {
            int habitatImageSize = this._HabitatImageSize;
            int rowPadding = 3;
            HabitatList habitatList = new HabitatList();
            for (int index = 0; index < this._Galaxy.Systems[systemStar.SystemIndex].Habitats.Count; ++index)
            {
                Habitat habitat = this._Galaxy.Systems[systemStar.SystemIndex].Habitats[index];
                if (habitat.Empire != null && habitat.Population != null && habitat.Population.DominantRace != null && !habitatList.Contains(habitat))
                    habitatList.Add(habitat);
            }
            for (int index = 0; index < this._Galaxy.Systems[systemStar.SystemIndex].Habitats.Count; ++index)
            {
                Habitat habitat = this._Galaxy.Systems[systemStar.SystemIndex].Habitats[index];
                if (habitat.BasesAtHabitat.Count > 0 && (habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.MiningStation || habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.GasMiningStation || habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.ResortBase || habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.EnergyResearchStation || habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.WeaponsResearchStation || habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.HighTechResearchStation || habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.DefensiveBase || habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.MonitoringStation || habitat.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.GenericBase) && !habitatList.Contains(habitat))
                    habitatList.Add(habitat);
            }
            int num1 = this.Width - labelWidth - 10;
            int val2 = num1 / habitatImageSize;
            int num2 = Math.Max(1, (num1 - habitatImageSize * habitatList.Count) / (habitatList.Count + 1));
            int x = labelWidth + num2;
            if (habitatList.Count > 0)
            {
                using (Pen pen = new Pen(Color.FromArgb(170, 170, 170), 1f))
                {
                    pen.DashPattern = new float[2] { 1f, 2f };
                    int num3 = startY + this._HabitatImageSize + rowPadding + 13;
                    int x1 = x - 5;
                    int x2 = x1 + 10 + (Math.Min(habitatList.Count, val2) * (habitatImageSize + num2) - num2);
                    graphics.DrawLine(pen, x1, num3, x2, num3);
                }
            }
            for (int index = 0; index < habitatList.Count; ++index)
            {
                if (index >= val2)
                {
                    this.DrawStringWithDropShadow(graphics, "...", this._NormalFont, new Point(x, startY + 5));
                    break;
                }
                this.DrawSingleColonySummary(habitatList[index], graphics, x, startY, rowPadding);
                x += habitatImageSize + num2;
            }
        }

        private void DrawSingleColonySummary(
          Habitat colony,
          Graphics graphics,
          int x,
          int y,
          int rowPadding)
        {
            Rectangle srcRect = Rectangle.Empty;
            Rectangle rectangle = Rectangle.Empty;
            bool flag1 = false;
            bool flag2 = false;
            if (colony.BasesAtHabitat != null && colony.BasesAtHabitat.Count > 0 && colony.BasesAtHabitat[0].Empire != null && colony.BasesAtHabitat[0].Empire != this._Galaxy.IndependentEmpire && (colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.MiningStation || colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.GasMiningStation || colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.ResortBase))
            {
                flag1 = true;
                if (colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.MiningStation || colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.GasMiningStation)
                    flag2 = true;
            }
            this.AddHotspot(new Rectangle(x, y, this._HabitatImages[(int)colony.PictureRef].Width, this._HabitatImages[(int)colony.PictureRef].Height), (object)colony, colony.Name + " (" + TextResolver.GetText("click to select") + ")");
            graphics.DrawImage((Image)this._HabitatImages[(int)colony.PictureRef], x, y);
            y += this._HabitatImageSize + rowPadding / 2;
            int summaryDetailWidth = this._ColonySummaryDetailWidth;
            int num1 = (this._HabitatImageSize - summaryDetailWidth) / 2;
            x += num1;
            this.SetGraphicsQualityToHigh(graphics);
            string text = string.Empty;
            switch (colony.Type)
            {
                case HabitatType.Volcanic:
                    text = TextResolver.GetText("PlanetType Abbreviation Volcanic");
                    break;
                case HabitatType.Desert:
                    text = TextResolver.GetText("PlanetType Abbreviation Desert");
                    break;
                case HabitatType.MarshySwamp:
                    text = TextResolver.GetText("PlanetType Abbreviation Marshy Swamp");
                    break;
                case HabitatType.Continental:
                    text = TextResolver.GetText("PlanetType Abbreviation Continental");
                    break;
                case HabitatType.Ocean:
                    text = TextResolver.GetText("PlanetType Abbreviation Ocean");
                    break;
                case HabitatType.BarrenRock:
                    text = TextResolver.GetText("PlanetType Abbreviation Rock");
                    break;
                case HabitatType.Ice:
                    text = TextResolver.GetText("PlanetType Abbreviation Ice");
                    break;
                case HabitatType.GasGiant:
                    text = TextResolver.GetText("PlanetType Abbreviation Gas Giant");
                    break;
                case HabitatType.FrozenGasGiant:
                    text = TextResolver.GetText("PlanetType Abbreviation Frozen Gas Giant");
                    break;
            }
            if (colony.Category == HabitatCategoryType.Asteroid)
                text = TextResolver.GetText("PlanetType Abbreviation Asteroid");
            else if (colony.Category == HabitatCategoryType.GasCloud)
                text = TextResolver.GetText("PlanetType Abbreviation Gas Cloud");
            int width = (int)graphics.MeasureString(text, this._TinyFont, this._MaxGraphTextWidth, StringFormat.GenericDefault).Width;
            int x1 = x + (summaryDetailWidth - width) / 2;
            this.DrawStringWithDropShadow(graphics, text, this._TinyFont, new Point(x1, y));
            y += 15 + rowPadding + rowPadding;
            int height = (int)((double)summaryDetailWidth * 0.6);
            if (!flag1 && colony.Empire != null && colony.Empire != this._Galaxy.IndependentEmpire)
            {
                srcRect = new Rectangle(0, 0, colony.Empire.LargeFlagPicture.Width, colony.Empire.LargeFlagPicture.Height);
                rectangle = new Rectangle(x, y, summaryDetailWidth, height);
                graphics.DrawImage((Image)colony.Empire.LargeFlagPicture, rectangle, srcRect, GraphicsUnit.Pixel);
                this.AddHotspot(rectangle, (object)colony.Empire, colony.Empire.Name + " (" + TextResolver.GetText("click for details") + ")");
            }
            else if (colony.BasesAtHabitat.Count > 0)
            {
                Empire empire = colony.BasesAtHabitat[0].Empire;
                if (empire != null)
                {
                    srcRect = new Rectangle(0, 0, empire.LargeFlagPicture.Width, empire.LargeFlagPicture.Height);
                    rectangle = new Rectangle(x, y, summaryDetailWidth, height);
                    graphics.DrawImage((Image)empire.LargeFlagPicture, rectangle, srcRect, GraphicsUnit.Pixel);
                    this.AddHotspot(rectangle, (object)empire, empire.Name + " (" + TextResolver.GetText("click for details") + ")");
                }
            }
            y += height + rowPadding;
            this.SetGraphicsQualityToLow(graphics);
            if (!flag1 && colony.Population != null && colony.Population.DominantRace != null)
            {
                int num2 = (summaryDetailWidth - this._RaceImages[colony.Population.DominantRace.PictureRef].Width) / 2;
                Bitmap raceImage = this._RaceImages[colony.Population.DominantRace.PictureRef];
                graphics.DrawImage((Image)raceImage, x + num2, y);
                this.AddHotspot(new Rectangle(x + num2, y, raceImage.Width, raceImage.Height), (object)colony.Population.DominantRace, colony.Population.DominantRace.Name + " (" + TextResolver.GetText("click for details") + ")");
                y += this._RaceImages[0].Height + rowPadding;
                this.DrawPopulationIndicator(colony, graphics, x, y);
            }
            else
            {
                if (colony.BasesAtHabitat.Count <= 0)
                    return;
                this.SetGraphicsQualityToHigh(graphics);
                srcRect = new Rectangle(0, 0, this._BuiltObjectImages[colony.BasesAtHabitat[0].PictureRef].Width, this._BuiltObjectImages[colony.BasesAtHabitat[0].PictureRef].Height);
                rectangle = new Rectangle(x + 1, y, summaryDetailWidth - 2, summaryDetailWidth - 2);
                if (this._Game.PlayerEmpire.IsObjectVisibleToThisEmpire((StellarObject)colony.BasesAtHabitat[0]))
                    this.AddHotspot(rectangle, (object)colony.BasesAtHabitat[0], colony.BasesAtHabitat[0].Name + " (" + TextResolver.GetText("click to select") + ")");
                graphics.DrawImage((Image)this._BuiltObjectImages[colony.BasesAtHabitat[0].PictureRef], rectangle, srcRect, GraphicsUnit.Pixel);
                y += rectangle.Height + rowPadding;
                if (!flag2)
                    return;
                if (this._Galaxy.PlayerEmpire.ResourceMap != null && this._Galaxy.PlayerEmpire.ResourceMap.CheckResourcesKnown(colony))
                {
                    this.SetGraphicsQualityToLow(graphics);
                    HabitatResourceList habitatResourceList = colony.Resources.Clone();
                    if (habitatResourceList.Count <= 0)
                        return;
                    int num3 = (summaryDetailWidth - this._ResourceImages[habitatResourceList[0].PictureRef].Width) / 2;
                    this.AddHotspot(new Rectangle(x + num3, y, this._ResourceImages[habitatResourceList[0].PictureRef].Width, this._ResourceImages[habitatResourceList[0].PictureRef].Height), (object)habitatResourceList[0], habitatResourceList[0].Name + " (" + TextResolver.GetText("click for details") + ")");
                    graphics.DrawImage((Image)this._ResourceImages[habitatResourceList[0].PictureRef], x + num3, y);
                }
                else
                {
                    int num4 = 6;
                    this.DrawStringWithDropShadow(graphics, "?", this._TinyFont, new Point(x + num4, y));
                }
            }
        }

        private void DrawPopulationIndicator(Habitat habitat, Graphics graphics, int x, int y)
        {
            int num1 = 20;
            int num2 = 15;
            int num3 = num1 / 5;
            int num4 = num2 / 5;
            for (int index1 = 0; index1 < 5; ++index1)
            {
                for (int index2 = 0; index2 < 5; ++index2)
                    graphics.FillRectangle((Brush)this._SemiSubtleBrush, x + index1 * num3, y + (4 - index2) * num4, num3 - 1, num4 - 1);
            }
            int num5 = 0;
            int num6 = 0;
            if (habitat.Population.TotalAmount > 2500000000L)
                num5 = 5;
            else if (habitat.Population.TotalAmount > 500000000L)
                num5 = 4;
            else if (habitat.Population.TotalAmount > 100000000L)
                num5 = 3;
            else if (habitat.Population.TotalAmount > 20000000L)
                num5 = 2;
            else if (habitat.Population.TotalAmount > 0L)
                num5 = 1;
            if (habitat.DevelopmentLevel > 80)
                num6 = 5;
            else if (habitat.DevelopmentLevel > 60)
                num6 = 4;
            else if (habitat.DevelopmentLevel > 40)
                num6 = 3;
            else if (habitat.DevelopmentLevel > 20)
                num6 = 2;
            else if (habitat.DevelopmentLevel > 0)
                num6 = 1;
            for (int index3 = 0; index3 < num5; ++index3)
            {
                for (int index4 = 0; index4 < num6; ++index4)
                    graphics.FillRectangle((Brush)this._BrightBrush, x + index3 * num3, y + (4 - index4) * num4, num3 - 1, num4 - 1);
            }
        }

        private void DrawHabitat(Habitat habitat, Graphics graphics)
        {
            if (habitat.HasBeenDestroyed)
            {
                this._Game.SelectedObject = (object)null;
                this.ClearData();
            }
            else
            {
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                bool flag1 = false;
                if (habitat.Empire != this._Game.PlayerEmpire)
                {
                    if (this._Game.PlayerEmpire.EmpiresViewable.Contains(habitat.Empire))
                        flag1 = true;
                    else if (habitat.GetPirateControl().GetByFaction(this._Game.PlayerEmpire) != null)
                        flag1 = true;
                }
                SystemVisibilityStatus visibilityStatus = SystemVisibilityStatus.Unexplored;
                if (this._Game.PlayerEmpire.CheckSystemExplored(habitat.SystemIndex))
                    visibilityStatus = SystemVisibilityStatus.Explored;
                if (this._Game.PlayerEmpire.CheckSystemVisible(habitat.SystemIndex))
                    visibilityStatus = SystemVisibilityStatus.Visible;
                if (this._Game.GodMode)
                    visibilityStatus = SystemVisibilityStatus.Visible;
                Color color1 = this._WhiteColor;
                Color color2 = this._WhiteColor;
                if (visibilityStatus == SystemVisibilityStatus.Unexplored)
                    color1 = this._UnknownColor;
                if (this._Game.PlayerEmpire.ResourceMap == null || !this._Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat))
                    color2 = this._UnknownColor;
                SolidBrush solidBrush1 = new SolidBrush(color1);
                SolidBrush textBrush1 = new SolidBrush(color2);
                int y1 = 3;
                int rowHeight = this._RowHeight;
                int x1 = 5;
                int overallWidth1 = this.ClientRectangle.Width - 10;
                Color.FromArgb((int)sbyte.MaxValue, 8, 8, 48);
                Font titleFont = this._TitleFont;
                this.DrawBackgroundPicture(graphics);
                string description1 = string.Empty;
                Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                if (habitatSystemStar != null)
                    description1 = habitatSystemStar.Name;
                int labelWidthHabitat = this._LabelWidthHabitat;
                int y2 = y1 + titleFont.Height + 3 + (rowHeight + 3);
                if (habitatSystemStar == habitat && habitat.Category != HabitatCategoryType.GasCloud)
                    labelWidthHabitat = this._LabelWidthHabitat;
                if (habitat.IsBlockaded)
                    y2 += rowHeight;
                if (habitat.PlagueId >= (short)0)
                    y2 += rowHeight;
                Rectangle rect1 = new Rectangle(x1 - 2, y2, x1 + labelWidthHabitat - 1, this.ClientRectangle.Height - (y2 + 2));
                if (habitat.Category != HabitatCategoryType.Star)
                    graphics.FillRectangle((Brush)this._LabelAreaBrush, rect1);
                int labelWidth = labelWidthHabitat - 5;
                Point point1 = new Point(overallWidth1 - (this._FlagSizeSmall.Width - 5), 3);
                if (habitat.Empire != null && habitat.Empire != this._Galaxy.IndependentEmpire)
                {
                    if (this._EmpirePicture != null)
                    {
                        graphics.DrawImageUnscaled((Image)this._EmpirePicture, point1);
                        if (habitat.Empire != null)
                            this.AddHotspot(new Rectangle(point1, this._EmpirePicture.Size), (object)habitat.Empire, habitat.Empire.Name + " (" + TextResolver.GetText("click for details") + ")");
                    }
                }
                else if (habitat.Population.TotalAmount > 0L)
                {
                    int width = (int)graphics.MeasureString(TextResolver.GetText("Independent"), this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                    PointF point2 = new PointF((float)overallWidth1 - (float)width, 6f);
                    graphics.DrawString(TextResolver.GetText("Independent"), this._NormalFont, (Brush)solidBrush1, point2);
                }
                Point point3 = new Point(x1, y1 + 2);
                if (habitat.Empire != null && habitat.Empire != this._Galaxy.IndependentEmpire)
                {
                    if (habitat.Empire.Capital == habitat)
                    {
                        graphics.DrawImageUnscaled((Image)InfoPanel._CapitalColonyImage, point3);
                        point3 = new Point(x1 + (InfoPanel._CapitalColonyImage.Width + 2), y1);
                    }
                    else if (habitat.Empire.Capitals.Contains(habitat))
                    {
                        graphics.DrawImageUnscaled((Image)InfoPanel._RegionalCapitalColonyImage, point3);
                        point3 = new Point(x1 + (InfoPanel._RegionalCapitalColonyImage.Width + 2), y1);
                    }
                }
                if (visibilityStatus == SystemVisibilityStatus.Explored || visibilityStatus == SystemVisibilityStatus.Visible)
                {
                    if (habitat.Empire != null && habitat.Empire.MainColor.ToArgb() != this._EmpireColor.ToArgb())
                        this._EmpireColor = habitat.Empire.MainColor;
                    using (SolidBrush brush = new SolidBrush(this._EmpireColor))
                    {
                        this.DrawStringWithDropShadow(graphics, habitat.Name, titleFont, point3, brush);
                        if (habitat.Empire != null)
                        {
                            if (habitat.Empire != this._Galaxy.IndependentEmpire)
                            {
                                int width = (int)graphics.MeasureString(habitat.Name, titleFont, 300, StringFormat.GenericTypographic).Width;
                                Point location = new Point(point3.X + width + 7, point3.Y + 3);
                                this.DrawStringWithDropShadow(graphics, "(" + habitat.Empire.Name + ")", this._NormalFont, location, brush);
                            }
                        }
                    }
                }
                else
                {
                    string str = Galaxy.ResolveDescription(habitat.Category);
                    this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Unknown") + " " + str + ")", titleFont, point3, new SolidBrush(this._UnknownColor));
                }
                int y3 = y1 + (titleFont.Height + 3);
                point3 = new Point(x1, y3);
                double num1 = (double)habitat.Diameter / 10.0;
                string str1 = Galaxy.ResolveDescription(habitat.Type);
                if (habitat.Type != HabitatType.BlackHole)
                    str1 = str1 + " " + Galaxy.ResolveDescription(habitat.Category);
                string text1 = str1 + ", " + num1.ToString("##0.0K");
                if (habitat.Category == HabitatCategoryType.Planet || habitat.Category == HabitatCategoryType.Moon)
                {
                    double num2 = (double)habitat.Quality * 100.0;
                    text1 = text1 + ",   " + TextResolver.GetText("Quality") + ": " + num2.ToString("0") + "%";
                    if ((double)habitat.BaseQuality != (double)habitat.Quality)
                    {
                        double num3 = (double)habitat.BaseQuality * 100.0;
                        text1 = text1 + " (" + TextResolver.GetText("Maximum Abbreviation") + " " + num3.ToString("0") + "%)";
                    }
                }
                this.DrawStringWithDropShadow(graphics, text1, this._NormalFont, point3, solidBrush1);
                int num4;
                if ((this._Galaxy.PlayerEmpire.PirateEmpireBaseHabitat != null || this._Game.GodMode) && habitat.GetPirateControl().Count > 0)
                {
                    y3 += rowHeight;
                    point3 = new Point(x1, y3);
                    Empire empire = (Empire)null;
                    PirateColonyControl pirateColonyControl1 = (PirateColonyControl)null;
                    string message = string.Empty;
                    for (int index = 0; index < habitat.GetPirateControl().Count; ++index)
                    {
                        PirateColonyControl pirateColonyControl2 = habitat.GetPirateControl()[index];
                        if (pirateColonyControl2 != null)
                        {
                            Empire byEmpireId = this._Galaxy.PirateEmpires.GetByEmpireId((int)pirateColonyControl2.EmpireId);
                            if (byEmpireId != null)
                            {
                                if (empire == null)
                                {
                                    empire = byEmpireId;
                                    pirateColonyControl1 = pirateColonyControl2;
                                }
                                if (message.Length > 0)
                                    message += ", ";
                                message = message + byEmpireId.Name + " (" + pirateColonyControl2.ControlLevel.ToString("0%") + ")";
                            }
                        }
                    }
                    if (empire != null && empire.Active && pirateColonyControl1 != null)
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        int width = (int)((double)this._FlagSizeSmall.Width * 0.734);
                        int height = (int)((double)width * 0.6);
                        Rectangle rect2 = new Rectangle(x1 + 2, y3, width, height);
                        graphics.DrawImage((Image)empire.LargeFlagPicture, rect2);
                        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                        point3 = new Point(x1 + width + 5, y3);
                        string empty = string.Empty;
                        string text2;
                        if (habitat.GetPirateControl().Count > 1)
                        {
                            string text3 = TextResolver.GetText("Pirate Control Description Multiple");
                            string name = empire.Name;
                            string str2 = pirateColonyControl1.ControlLevel.ToString("0%");
                            num4 = habitat.GetPirateControl().Count - 1;
                            string str3 = num4.ToString("0");
                            text2 = string.Format(text3, (object)name, (object)str2, (object)str3);
                        }
                        else
                            text2 = string.Format(TextResolver.GetText("Pirate Control Description Sole"), (object)empire.Name, (object)pirateColonyControl1.ControlLevel.ToString("0%"));
                        this.DrawStringWithDropShadow(graphics, text2, this._NormalFont, point3);
                        this.AddHotspot(new Rectangle(5, point3.Y, this.Width - 10, rowHeight), (object)null, message);
                    }
                }
                if (visibilityStatus == SystemVisibilityStatus.Explored || visibilityStatus == SystemVisibilityStatus.Visible)
                {
                    if (habitat.Ruin != null)
                    {
                        point3 = new Point(this.ClientRectangle.Width - (this._RuinImages[habitat.Ruin.PictureRef].Width + 8), y3);
                        graphics.DrawImageUnscaled((Image)this._RuinImages[habitat.Ruin.PictureRef], point3);
                        this.AddHotspot(new Rectangle(point3, this._RuinImages[habitat.Ruin.PictureRef].Size), (object)habitat.Ruin, habitat.Ruin.Name + " (" + TextResolver.GetText("click for details") + ")");
                    }
                    int y4 = y3 + rowHeight;
                    if (habitat.PlagueId >= (short)0)
                    {
                        Plague plague = this._Galaxy.Plagues[(int)habitat.PlagueId];
                        if (plague != null)
                        {
                            int x2 = x1;
                            Bitmap bitmap = (Bitmap)null;
                            if (plague.PictureRef >= 0 && plague.PictureRef < this._PlagueImages.Length)
                                bitmap = this._PlagueImages[plague.PictureRef];
                            if (bitmap != null)
                            {
                                graphics.DrawImage((Image)bitmap, new Point(x1, y4));
                                x2 += bitmap.Width + 3;
                            }
                            string text4 = string.Format(TextResolver.GetText("Plague Colony Infection"), (object)plague.Name).ToUpper(CultureInfo.InvariantCulture) + "!";
                            point3 = new Point(x2, y4);
                            this.DrawStringWithDropShadow(graphics, text4, this._NormalFont, point3, this._RedBrush);
                            y4 += rowHeight;
                        }
                    }
                    if (habitat.IsBlockaded)
                    {
                        Blockade blockade = this._Galaxy.Blockades[habitat];
                        if (blockade != null)
                        {
                            point3 = new Point(x1, y4);
                            double num5 = (double)rowHeight / (double)InfoPanel._BlockadeImage.Height;
                            int width = (int)((double)InfoPanel._BlockadeImage.Width * num5);
                            Rectangle rect3 = new Rectangle(x1, y4, width, rowHeight);
                            graphics.DrawImage((Image)InfoPanel._BlockadeImage, rect3);
                            point3 = new Point(x1 + width + 2, y4 + 3);
                            graphics.DrawImage((Image)blockade.Initiator.SmallFlagPicture, point3);
                            string text5 = string.Format(TextResolver.GetText("Blockaded by EMPIRE"), (object)blockade.Initiator.Name);
                            point3 = new Point(x1 + width + blockade.Initiator.SmallFlagPicture.Width + 4, y4);
                            this.DrawStringWithDropShadow(graphics, text5, this._NormalFont, point3, solidBrush1);
                            y4 += rowHeight;
                        }
                    }
                    if (habitat.RaidCountdown > (byte)0)
                    {
                        point3 = new Point(x1, y4);
                        using (SolidBrush brush = new SolidBrush(Color.Yellow))
                            this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("This colony was recently Raided") + ")", this._NormalFont, point3, brush);
                        y4 += rowHeight;
                    }
                    if ((flag1 || habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode) && habitat.ManufacturingQueue != null && habitat.ManufacturingQueue.DeficientResources != null && habitat.ManufacturingQueue.DeficientResources.Count > 0)
                    {
                        ResourceDatePair[] array = habitat.ManufacturingQueue.DeficientResources.ToArray();
                        string empty = string.Empty;
                        for (int index = 0; index < array.Length; ++index)
                        {
                            if (index > 0)
                                empty += ", ";
                            empty += new Resource(array[index].ResourceId).Name;
                        }
                        Bitmap messageImage = InfoPanel._MessageImages[30];
                        Rectangle srcRect = new Rectangle(0, 0, messageImage.Width, messageImage.Height);
                        Rectangle destRect = new Rectangle(x1 + 3, y4, rowHeight, rowHeight);
                        this.SetGraphicsQualityToHigh(graphics);
                        graphics.DrawImage((Image)messageImage, destRect, srcRect, GraphicsUnit.Pixel);
                        point3 = new Point(destRect.Right + 2, y4);
                        string text6 = string.Format(TextResolver.GetText("Construction Resource Shortage Message Short"), (object)empty);
                        SizeF size = graphics.MeasureString(text6, this._NormalFont, this.ClientSize.Width - (point3.X + 5));
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)byte.MaxValue, 128, 0)))
                        {
                            size = new SizeF(size.Width, Math.Min((float)((double)rowHeight * 2.0 + 1.0), size.Height));
                            this.DrawStringWithDropShadowBounded(graphics, text6, this._NormalFont, point3, size, brush);
                        }
                        y4 += (int)size.Height;
                    }
                    int y5 = y4 + this._Height5;
                    if (habitat.Empire != null && habitat.Empire != this._Galaxy.IndependentEmpire && (habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag1))
                    {
                        CharacterList characterList = new CharacterList();
                        if (habitat.Characters != null)
                            characterList = habitat.Characters.GetCharactersByRole(CharacterRole.ColonyGovernor);
                        if (characterList.Count > 0)
                        {
                            int x3 = overallWidth1 - 35;
                            for (int index = 0; index < characterList.Count; ++index)
                            {
                                point3 = new Point(x3, y5 - 20);
                                Bitmap characterImageSmall = InfoPanel._CharacterImageCache.ObtainCharacterImageSmall(characterList[index]);
                                if (characterImageSmall != null && characterImageSmall.PixelFormat != PixelFormat.Undefined)
                                {
                                    graphics.DrawImageUnscaled((Image)characterImageSmall, point3);
                                    string message = characterList[index].Name + " (" + Galaxy.ResolveDescription(characterList[index].Role) + ")" + "   (" + TextResolver.GetText("click for details") + ")";
                                    this.AddHotspot(new Rectangle(point3.X, point3.Y, characterImageSmall.Width, characterImageSmall.Height), (object)characterList[index], message);
                                    x3 -= 38;
                                }
                            }
                        }
                    }
                    point3 = new Point(x1, y5);
                    int num6;
                    if (habitatSystemStar == habitat && habitat.Category != HabitatCategoryType.GasCloud)
                    {
                        string text7 = TextResolver.GetText("Solar");
                        byte num7 = habitat.SolarRadiation;
                        string str4 = num7.ToString();
                        string[] strArray1 = new string[5]
                        {
              text7 + " " + str4,
              ", ",
              TextResolver.GetText("Microwave"),
              " ",
              null
                        };
                        string[] strArray2 = strArray1;
                        num7 = habitat.MicrowaveRadiation;
                        string str5 = num7.ToString();
                        strArray2[4] = str5;
                        string[] strArray3 = new string[5]
                        {
              string.Concat(strArray1),
              ", ",
              TextResolver.GetText("X-ray"),
              " ",
              null
                        };
                        string[] strArray4 = strArray3;
                        num7 = habitat.XrayRadiation;
                        string str6 = num7.ToString();
                        strArray4[4] = str6;
                        string description2 = string.Concat(strArray3);
                        this.DrawLabelledDescription(graphics, TextResolver.GetText("Energy").ToUpper(CultureInfo.InvariantCulture), labelWidth, description2, point3, solidBrush1);
                        int y6 = y5 + rowHeight;
                        if (habitat.ResearchBonus > (byte)0)
                        {
                            point3 = new Point(x1, y6);
                            float num8 = (float)habitat.ResearchBonus / 100f;
                            string description3 = string.Format(TextResolver.GetText("X research bonus to AREA"), (object)num8.ToString("+0%"), (object)Galaxy.ResolveDescription(habitat.ResearchBonusIndustry));
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("Research"), labelWidth, description3, point3, solidBrush1);
                            y6 += rowHeight;
                        }
                        if ((double)habitat.ScenicFactor > 0.0)
                        {
                            point3 = new Point(x1, y6);
                            string description4 = habitat.ScenicFactor.ToString("+0%");
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("Scenery"), labelWidth, description4, point3, solidBrush1);
                            y6 += rowHeight;
                        }
                        int num9 = overallWidth1 - (x1 + labelWidth + 10);
                        DesignList beBuiltAtHabitat = this._Galaxy.PlayerEmpire.CheckBasesToBeBuiltAtHabitat(habitat);
                        if (beBuiltAtHabitat.Count > 0)
                        {
                            int y7 = y6 + 4;
                            point3 = new Point(x1 + labelWidth + 10, y7);
                            string text8 = string.Format(TextResolver.GetText("Construction ship queued to build X here"), (object)Galaxy.ResolveDescription(beBuiltAtHabitat[0].SubRole));
                            SizeF sizeF = graphics.MeasureString(text8, this._NormalFont, num9, StringFormat.GenericTypographic);
                            this.DrawStringWithDropShadow(graphics, text8, this._NormalFont, point3, this._WhiteBrush, num9);
                            y6 = y7 + (int)sizeF.Height;
                        }
                        num6 = y6 + rowHeight;
                        if (visibilityStatus == SystemVisibilityStatus.Visible || visibilityStatus == SystemVisibilityStatus.Explored)
                            this.DrawSystemColoniesSummary(habitat, graphics, num6, labelWidth);
                    }
                    else
                    {
                        this.DrawLabelledDescription(graphics, TextResolver.GetText("System"), labelWidth, description1, point3, solidBrush1);
                        int y8 = y5 + rowHeight;
                        if (habitat.ResearchBonus > (byte)0)
                        {
                            point3 = new Point(x1, y8);
                            float num10 = (float)habitat.ResearchBonus / 100f;
                            string description5 = string.Format(TextResolver.GetText("X research bonus to AREA"), (object)num10.ToString("+0%"), (object)Galaxy.ResolveDescription(habitat.ResearchBonusIndustry));
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("Research"), labelWidth, description5, point3, solidBrush1);
                            y8 += rowHeight;
                        }
                        if ((double)habitat.ScenicFactor > 0.0)
                        {
                            point3 = new Point(x1, y8);
                            string description6 = habitat.ScenicFactor.ToString("+0%");
                            if (!string.IsNullOrEmpty(habitat.ScenicFeature))
                                description6 = string.Format(TextResolver.GetText("BONUSAMOUNT from FEATURE"), (object)habitat.ScenicFactor.ToString("+0%"), (object)habitat.ScenicFeature);
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("Scenery"), labelWidth, description6, point3, solidBrush1);
                            y8 += rowHeight;
                        }
                        if (habitat.Ruin != null)
                        {
                            point3 = new Point(x1, y8);
                            SizeF sizeF = graphics.MeasureString(habitat.Ruin.Name, this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic);
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("Ruins"), labelWidth, habitat.Ruin.Name, point3, solidBrush1);
                            this.AddHotspot(new Rectangle(x1 + labelWidth + 10, y8, (int)sizeF.Width + 1, rowHeight), (object)habitat.Ruin, habitat.Ruin.Name + " (" + TextResolver.GetText("click for details") + ")");
                            y8 += rowHeight;
                        }
                        int y9 = y8 + this._Height4;
                        if (habitat.Population.TotalAmount > 0L)
                        {
                            point3 = new Point(x1, y9);
                            y9 += this.DrawPopulation(labelWidth, habitat, habitat.Population, graphics, point3, overallWidth1, rowHeight, solidBrush1);
                        }
                        if (habitat.Empire == this._Galaxy.IndependentEmpire || habitat.Empire == null)
                        {
                            point3 = new Point(x1, y9);
                            int num11 = this._Galaxy.CheckColonizationLikeliness(habitat, this._Game.PlayerEmpire.DominantRace);
                            SolidBrush textBrush2 = !this._EmpireCanColonize || num11 <= -5 || (double)habitat.Quality < 0.5 ? new SolidBrush(Color.Red) : new SolidBrush(solidBrush1.Color);
                            if (this._EmpireCanColonize && (double)habitat.Quality < 0.5)
                                this.DrawLabelledDescription(graphics, TextResolver.GetText("Colonize"), labelWidth, TextResolver.GetText("Yes, but low quality is undesirable"), point3, textBrush2);
                            else
                                this.DrawLabelledDescription(graphics, TextResolver.GetText("Colonize"), labelWidth, this._ColonizeExplanation, point3, textBrush2);
                            textBrush2.Dispose();
                            y9 += rowHeight;
                        }
                        point3 = new Point(x1, y9);
                        bool flag2 = false;
                        if (this._Game.PlayerEmpire.ResourceMap != null)
                            flag2 = this._Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat);
                        if (habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag2)
                            this.DrawResources(labelWidth, habitat.Resources, graphics, point3, overallWidth1, textBrush1);
                        else
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("Resource"), labelWidth, "(" + TextResolver.GetText("Unknown") + ")", point3, textBrush1);
                        int y10 = y9 + rowHeight + this._Height4;
                        if (habitat.Population.TotalAmount > 0L && habitat.Empire != null && habitat.Empire != this._Galaxy.IndependentEmpire)
                        {
                            int x4 = x1;
                            point3 = new Point(x4, y10);
                            num4 = habitat.StrategicValue;
                            string str7 = num4.ToString("0,K");
                            int width1 = (int)graphics.MeasureString(str7, this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                            num4 = habitat.DevelopmentLevel;
                            string text9 = num4.ToString("##0") + "%";
                            if (habitat.Ruin != null || habitat.WonderForDevelopment != null)
                            {
                                int val1 = 0;
                                if (habitat.WonderForDevelopment != null)
                                    val1 = habitat.WonderForDevelopment.Value1;
                                else if (habitat.Ruin != null)
                                    val1 = Math.Max(val1, (int)(habitat.Ruin.DevelopmentBonus * 100.0));
                                text9 = text9 + "(" + val1.ToString("+##0;-##0;0") + "%)";
                            }
                            SizeF sizeF = graphics.MeasureString(text9, this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic);
                            int width2 = (int)sizeF.Width;
                            string empty1 = string.Empty;
                            if (habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag1 || visibilityStatus == SystemVisibilityStatus.Visible)
                                empty1 = habitat.EmpireApprovalRating.ToString("+###;-###;0");
                            sizeF = graphics.MeasureString(empty1, this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic);
                            double width3 = (double)sizeF.Width;
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("Value"), labelWidth, str7, point3, solidBrush1);
                            int x5 = x4 + (labelWidth + 10 + width1 + (int)((double)this._ImageSize * 0.75));
                            point3 = new Point(x5, y10);
                            graphics.DrawImageUnscaled((Image)InfoPanel._DevelopmentImage, point3);
                            int x6 = x5 + this._ImageSize;
                            point3 = new Point(x6, y10);
                            this.DrawStringWithDropShadow(graphics, text9, this._NormalFont, point3, solidBrush1);
                            int x7 = x6 + width2 + (int)((double)this._ImageSize * 0.75);
                            point3 = new Point(x7, y10);
                            if (!string.IsNullOrEmpty(empty1))
                            {
                                Bitmap bitmap = habitat.EmpireApprovalRating <= 15.0 ? (habitat.EmpireApprovalRating <= 0.0 ? (habitat.EmpireApprovalRating <= -15.0 ? InfoPanel._ApprovalAngryImage : InfoPanel._ApprovalSadImage) : InfoPanel._ApprovalNeutralImage) : InfoPanel._ApprovalSmileImage;
                                if (habitat.Rebelling)
                                    bitmap = InfoPanel._ApprovalAngryImage;
                                graphics.DrawImageUnscaled((Image)bitmap, point3);
                                point3 = new Point(x7 + this._ImageSize, y10);
                                this.DrawStringWithDropShadow(graphics, empty1, this._NormalFont, point3, solidBrush1);
                            }
                            int y11 = y10 + rowHeight;
                            point3 = new Point(x1, y11);
                            double num12 = 0.0;
                            if (habitat.Empire != null)
                                num12 = Math.Max(0.0, habitat.Empire.PirateEmpireBaseHabitat == null ? habitat.AnnualRevenue / habitat.Empire.PrivateAnnualRevenue : habitat.AnnualRevenue / habitat.Empire.CalculateAccurateAnnualIncome());
                            string empty2 = string.Empty;
                            double annualRevenue = habitat.AnnualRevenue;
                            double num13;
                            string description7;
                            if (habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag1)
                            {
                                string str8;
                                if (annualRevenue < 0.0)
                                {
                                    string text10 = TextResolver.GetText("Colony Revenue and GDP portion");
                                    num13 = annualRevenue / 1000.0;
                                    string str9 = num13.ToString("0K");
                                    string str10 = num12.ToString("##0%");
                                    str8 = string.Format(text10, (object)str9, (object)str10);
                                }
                                else if (annualRevenue < 1000.0)
                                {
                                    string text11 = TextResolver.GetText("Colony Revenue and GDP portion");
                                    num13 = annualRevenue / 1000.0;
                                    string str11 = num13.ToString("0.00K");
                                    string str12 = num12.ToString("##0.00%");
                                    str8 = string.Format(text11, (object)str11, (object)str12);
                                }
                                else
                                    str8 = string.Format(TextResolver.GetText("Colony Revenue and GDP portion"), (object)annualRevenue.ToString("0,K"), (object)num12.ToString("##0%"));
                                double corruption = habitat.Corruption;
                                description7 = str8 + " (" + corruption.ToString("##0%") + " " + TextResolver.GetText("Corruption").ToLower(CultureInfo.InvariantCulture) + ")";
                            }
                            else
                                description7 = annualRevenue >= 1000.0 ? annualRevenue.ToString("0,K") : (annualRevenue / 1000.0).ToString("0.00K");
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("GDP"), labelWidth, description7, point3, solidBrush1);
                            int y12 = y11 + rowHeight;
                            point3 = new Point(x1, y12);
                            string empty3 = string.Empty;
                            double annualTaxRevenue = habitat.AnnualTaxRevenue;
                            double taxComplianceRate = habitat.TaxComplianceRate;
                            string description8;
                            if (habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag1)
                            {
                                string str13 = habitat.TaxRate.ToString("#0%;-#0%;0%");
                                if (habitat.Rebelling)
                                {
                                    description8 = str13 + " " + TextResolver.GetText("NO TAX PAID (Rebelling)");
                                }
                                else
                                {
                                    if (annualTaxRevenue < 1000.0)
                                    {
                                        string str14 = str13;
                                        num13 = annualTaxRevenue / 1000.0;
                                        string str15 = num13.ToString("0.00K");
                                        description8 = str14 + " (" + str15 + ")";
                                    }
                                    else
                                        description8 = str13 + " (" + annualTaxRevenue.ToString("0,K") + ")";
                                    if ((double)habitat.TaxRate > 0.0)
                                        description8 = description8 + " = " + taxComplianceRate.ToString("0%") + " " + TextResolver.GetText("compliance");
                                }
                            }
                            else
                            {
                                string str16 = habitat.TaxRate.ToString("#0%;-#0%;0%");
                                num13 = habitat.AnnualTaxRevenue;
                                string str17 = num13.ToString("0,K");
                                description8 = str16 + " (" + str17 + ")";
                            }
                            this.DrawLabelledDescription(graphics, TextResolver.GetText("Tax"), labelWidth, description8, point3, solidBrush1);
                            y10 = y12 + rowHeight + this._Height4;
                        }
                        int offsetX = 0;
                        int overallWidth2 = overallWidth1;
                        point3 = new Point(x1, y10);
                        this.DrawFacilities(labelWidth, habitat, habitat.Facilities, graphics, point3, overallWidth1);
                        num6 = y10 + rowHeight;
                        if (habitat.Population.TotalAmount > 0L && habitat.Empire != null || habitat.Troops != null && habitat.Troops.Count > 0 || habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0 || habitat.TroopsToRecruit != null && habitat.TroopsToRecruit.Count > 0 || habitat.Characters != null && habitat.Characters.Count > 0 || habitat.InvadingCharacters != null && habitat.InvadingCharacters.Count > 0)
                        {
                            point3 = new Point(x1, num6);
                            bool flag3 = false;
                            if (habitat.Characters != null && habitat.Characters.CheckCharactersOfEmpirePresent(this._Game.PlayerEmpire) || habitat.InvadingCharacters != null && habitat.InvadingCharacters.CheckCharactersOfEmpirePresent(this._Game.PlayerEmpire) || habitat.Troops != null && habitat.Troops.CheckTroopsOfEmpirePresent(this._Game.PlayerEmpire) || habitat.InvadingTroops != null && habitat.InvadingTroops.CheckTroopsOfEmpirePresent(this._Game.PlayerEmpire))
                                flag3 = true;
                            if (habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag1 || flag3 || visibilityStatus == SystemVisibilityStatus.Visible)
                            {
                                this.DrawTroopsAgents(labelWidth, habitat.Troops, habitat.TroopsToRecruit, habitat.InvadingTroops, habitat.Characters, habitat.InvadingCharacters, graphics, point3, overallWidth2, offsetX);
                                string str18 = string.Format(TextResolver.GetText("Show Colony Ground Report"), (object)habitat.Name);
                                if (habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0)
                                {
                                    str18 = string.Format(TextResolver.GetText("Show Colony Battle Report"), (object)habitat.Name);
                                    using (SolidBrush solidBrush2 = new SolidBrush(GraphicsHelper.OscillateColor(Color.FromArgb(0, (int)byte.MaxValue, 0, 0), Color.FromArgb(160, (int)byte.MaxValue, 0, 0), DateTime.Now)))
                                        graphics.FillRectangle((Brush)solidBrush2, new Rectangle(5, point3.Y, this.Width - 10, rowHeight));
                                }
                                Empire invader = (Empire)null;
                                Empire defender = (Empire)null;
                                habitat.ResolveInvasionEmpires(out defender, out invader);
                                if (defender == null)
                                    defender = habitat.Empire;
                                int defendingStrength = 0;
                                int attackingStrength = 0;
                                double totalDefendModifier = 0.0;
                                double totalAttackModifier = 0.0;
                                habitat.CalculateForceStrengths(defender, invader, habitat.Troops, habitat.Characters, habitat.InvadingTroops, habitat.InvadingCharacters, out defendingStrength, out attackingStrength, out totalDefendModifier, out totalAttackModifier, out List<double> _, out List<string> _, out List<double> _, out List<string> _);
                                int infantryCount;
                                int artilleryCount;
                                int armorCount;
                                int specialForcesCount;
                                habitat.Troops.GetTroopCountsByType(out infantryCount, out artilleryCount, out armorCount, out specialForcesCount);
                                string str19 = " (" + Galaxy.ResolveTroopCompositionDescription(infantryCount, artilleryCount, armorCount, specialForcesCount) + ")";
                                bool isDefending = false;
                                int populationStrength = habitat.CalculatePopulationStrength(out isDefending, invader, defender);
                                if (isDefending)
                                    defendingStrength += populationStrength;
                                else
                                    attackingStrength += populationStrength;
                                string message;
                                if (invader != null)
                                {
                                    string str20 = string.Format(TextResolver.GetText("Battle Strength Description"), (object)(defendingStrength.ToString("0,K") + str19), (object)attackingStrength.ToString("0,K"));
                                    message = str18 + "  (" + TextResolver.GetText("Strength") + ": " + str20 + ")";
                                }
                                else
                                    message = str18 + "  (" + TextResolver.GetText("Strength") + ": " + defendingStrength.ToString("0,K") + str19 + ")";
                                this.AddHotspot(new Rectangle(5, point3.Y, this.Width - 10, rowHeight), (object)new object[1]
                                {
                  (object) habitat
                                }, message);
                            }
                            else
                                this.DrawLabelledDescription(graphics, TextResolver.GetText("Troops"), labelWidth, "(" + TextResolver.GetText("Unknown") + ")", point3, solidBrush1);
                            num6 += rowHeight;
                            if (habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0 && (habitat.Empire == this._Galaxy.PlayerEmpire || habitat.InvadingTroops[0].Empire == this._Galaxy.PlayerEmpire))
                            {
                                Empire defender;
                                Empire invader;
                                habitat.ResolveInvasionEmpires(out defender, out invader);
                                int attackingStrength = 0;
                                int defendingStrength = 0;
                                habitat.CalculateForceStrengths(defender, invader, habitat.Troops, habitat.Characters, habitat.InvadingTroops, habitat.InvadingCharacters, out defendingStrength, out attackingStrength);
                                bool isDefending = true;
                                int populationStrength = habitat.CalculatePopulationStrength(out isDefending, invader, defender);
                                string str21 = attackingStrength.ToString("0,K");
                                string str22 = defendingStrength.ToString("0,K");
                                if (isDefending)
                                {
                                    num4 = defendingStrength + populationStrength;
                                    str22 = num4.ToString("0,K") + " (" + string.Format(TextResolver.GetText("X from population"), (object)populationStrength.ToString("0,K")) + ")";
                                }
                                else
                                {
                                    num4 = attackingStrength + populationStrength;
                                    str21 = num4.ToString("0,K") + " (" + string.Format(TextResolver.GetText("X from population"), (object)populationStrength.ToString("0,K")) + ")";
                                }
                                string description9 = "  " + str22 + "   vs   " + str21;
                                point3 = new Point(x1, num6);
                                using (SolidBrush solidBrush3 = new SolidBrush(GraphicsHelper.OscillateColor(Color.FromArgb(0, (int)byte.MaxValue, 0, 0), Color.FromArgb(160, (int)byte.MaxValue, 0, 0), DateTime.Now)))
                                    graphics.FillRectangle((Brush)solidBrush3, new Rectangle(5, point3.Y, this.Width - 10, rowHeight));
                                this.DrawLabelledDescription(graphics, "", labelWidth, description9, point3);
                                string message = string.Format(TextResolver.GetText("Show Colony Battle Report"), (object)habitat.Name);
                                this.AddHotspot(new Rectangle(5, point3.Y, this.Width - 10, rowHeight), (object)new object[1]
                                {
                  (object) habitat
                                }, message);
                                num6 += rowHeight;
                            }
                        }
                        if (habitat.Population.TotalAmount > 0L && habitat.Empire != null && habitat.Empire != this._Galaxy.IndependentEmpire && habitat.ConstructionQueue != null && habitat.ConstructionQueue.ConstructionYards.Count > 0)
                        {
                            point3 = new Point(x1, num6);
                            if (habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag1 || visibilityStatus == SystemVisibilityStatus.Visible)
                            {
                                BuiltObjectList builtObjects = new BuiltObjectList();
                                for (int index = 0; index < habitat.ConstructionQueue.ConstructionYards.Count; ++index)
                                {
                                    ConstructionYard constructionYard = habitat.ConstructionQueue.ConstructionYards[index];
                                    if (constructionYard.ShipUnderConstruction != null)
                                        builtObjects.Add(constructionYard.ShipUnderConstruction);
                                }
                                this.DrawBuiltObjectList(labelWidth, TextResolver.GetText("Building"), builtObjects, habitat.ConstructionQueue.ConstructionWaitQueue.Count, graphics, point3, overallWidth1);
                            }
                            else
                                this.DrawLabelledDescription(graphics, TextResolver.GetText("Building"), labelWidth, "(" + TextResolver.GetText("Unknown") + ")", point3, solidBrush1);
                            num6 += rowHeight;
                        }
                        if (habitat.Population.TotalAmount > 0L && habitat.Empire != null && habitat.DockingBays != null && habitat.DockingBays.Count > 0)
                        {
                            point3 = new Point(x1, num6);
                            if (habitat.Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag1 || visibilityStatus == SystemVisibilityStatus.Visible)
                            {
                                BuiltObjectList builtObjects = new BuiltObjectList();
                                for (int index = 0; index < habitat.DockingBays.Count; ++index)
                                {
                                    DockingBay dockingBay = habitat.DockingBays[index];
                                    if (dockingBay.DockedShip != null)
                                        builtObjects.Add(dockingBay.DockedShip);
                                }
                                if (habitat.DockingBayWaitQueue != null)
                                    this.DrawBuiltObjectList(labelWidth, TextResolver.GetText("Docked"), builtObjects, habitat.DockingBayWaitQueue.Count, graphics, point3, overallWidth1);
                                else
                                    this.DrawBuiltObjectList(labelWidth, TextResolver.GetText("Docked"), builtObjects, 0, graphics, point3, overallWidth1);
                            }
                            else
                                this.DrawLabelledDescription(graphics, TextResolver.GetText("Docked"), labelWidth, "(" + TextResolver.GetText("Unknown") + ")", point3, solidBrush1);
                            num6 += rowHeight;
                        }
                        if (habitat.Empire == this._Galaxy.IndependentEmpire || habitat.Empire == null)
                        {
                            int num14 = overallWidth1 - (x1 + labelWidth + 10);
                            BuiltObject builtObject = this._Galaxy.PlayerEmpire.CheckColonizingHabitat(habitat);
                            if (builtObject != null)
                            {
                                int y13 = num6 + this._Height4;
                                point3 = new Point(x1 + labelWidth + 10, y13);
                                string text12 = string.Format(TextResolver.GetText("COLONYSHIP colonizing here"), (object)builtObject.Name);
                                SizeF sizeF = graphics.MeasureString(text12, this._NormalFont, num14, StringFormat.GenericTypographic);
                                this.DrawStringWithDropShadow(graphics, text12, this._NormalFont, point3, this._WhiteBrush, num14);
                                num6 = y13 + (int)sizeF.Height;
                            }
                            DesignList beBuiltAtHabitat = this._Galaxy.PlayerEmpire.CheckBasesToBeBuiltAtHabitat(habitat);
                            if (beBuiltAtHabitat.Count > 0)
                            {
                                int y14 = num6 + 4;
                                point3 = new Point(x1 + labelWidth + 10, y14);
                                string text13 = string.Format(TextResolver.GetText("Construction ship queued to build X here"), (object)Galaxy.ResolveDescription(beBuiltAtHabitat[0].SubRole));
                                SizeF sizeF = graphics.MeasureString(text13, this._NormalFont, num14, StringFormat.GenericTypographic);
                                this.DrawStringWithDropShadow(graphics, text13, this._NormalFont, point3, this._WhiteBrush, num14);
                                num6 = y14 + (int)sizeF.Height;
                            }
                        }
                    }
                    if (this._ShowExtendedInfo && !string.IsNullOrEmpty(this._CharacterBonuses))
                    {
                        point3 = new Point(x1, num6);
                        this.DrawLabel(graphics, TextResolver.GetText("Bonuses"), labelWidth, point3);
                        int width = overallWidth1 - labelWidth;
                        SizeF size = graphics.MeasureString(this._CharacterBonuses, this._NormalFont, width, StringFormat.GenericDefault);
                        point3 = new Point(x1 + labelWidth + 10, num6 + 1);
                        this.DrawStringWithDropShadowBounded(graphics, this._CharacterBonuses, this._NormalFont, point3, size);
                        int num15 = num6 + (int)size.Height;
                    }
                }
                solidBrush1.Dispose();
                textBrush1.Dispose();
            }
        }

        public void DrawLabel(Graphics graphics, string label, int labelWidth, Point location)
        {
            int width = (int)graphics.MeasureString(label, this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (labelWidth - width), location.Y);
            this.DrawStringWithDropShadow(graphics, label, this._NormalFontBold, location1);
        }

        public void DrawLabelledDescription(
          Graphics graphics,
          string label,
          int labelWidth,
          string description,
          Point location)
        {
            using (SolidBrush textBrush = new SolidBrush(this._WhiteColor))
                this.DrawLabelledDescription(graphics, label, labelWidth, description, location, textBrush);
        }

        public void DrawLabelledDescription(
          Graphics graphics,
          string label,
          int labelWidth,
          string description,
          Point location,
          SolidBrush textBrush)
        {
            int width = (int)graphics.MeasureString(label, this._NormalFontBold, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location1 = new Point(location.X + (labelWidth - width), location.Y - 2);
            labelWidth += 10;
            Point location2 = new Point(location.X + labelWidth, location.Y);
            this.DrawStringWithDropShadow(graphics, label, this._NormalFontBold, location1);
            this.DrawStringWithDropShadow(graphics, description, this._NormalFont, location2, textBrush);
        }

        private void DrawStringRedWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location)
        {
            //BaconInfoPanel.DrawStringRedWithDropShadow(this, graphics, text, font, location);
            InfoPanel.OnDrawStringRedWithDropShadowMods(this, graphics, text, font, location);
        }

        private void DrawStringColorWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          Color color)
        {
            //BaconInfoPanel.DrawStringColorWithDropShadow(this, graphics, text, font, location, color);
            InfoPanel.OnDrawStringColorWithDropShadowMods(this, graphics, text, font, location, color);
        }

        private void DrawStringWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location)
        {
            this.DrawStringWithDropShadow(graphics, text, font, location, this._WhiteBrush);
        }

        private void DrawStringWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          SolidBrush brush)
        {
            //BaconInfoPanel.DrawStringWithDropShadow(this, graphics, text, font, location, brush);
            InfoPanel.OnDrawStringWithDropShadowMods(this, graphics, text, font, location, brush);
        }

        private void DrawStringWithDropShadow(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          SolidBrush brush,
          int maxWidth)
        {
            //BaconInfoPanel.DrawStringWithDropShadow(this, graphics, text, font, location, brush, maxWidth);
            InfoPanel.OnDrawStringWithDropShadowMods(this, graphics, text, font, location, brush, maxWidth);
        }

        public Color CheckDropshadowColor(Color mainColor)
        {
            Color black = Color.Black;
            if (mainColor.ToArgb() == this._PirateColor.ToArgb() || mainColor.ToArgb() == this._UnknownColor.ToArgb())
                return black;
            if ((int)mainColor.R + (int)mainColor.G + (int)mainColor.B <= 176)
            {
                Color color = this._WhiteBrush.Color;
            }
            return Galaxy.DetermineContrastDropShadowColor(mainColor);
        }

        public void DrawStringWithDropShadowBounded(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          SizeF size)
        {
            this.DrawStringWithDropShadowBounded(graphics, text, font, location, size, this._WhiteBrush);
        }

        public void DrawStringWithDropShadowBounded(
          Graphics graphics,
          string text,
          Font font,
          Point location,
          SizeF size,
          SolidBrush brush)
        {
            //BaconInfoPanel.DrawStringWithDropShadowBounded(this, graphics, text, font, location, size, brush);
            InfoPanel.OnDrawStringWithDropShadowBoundedMods(this, graphics, text, font, location, size, brush);
        }

        public void SetData(Game game, Galaxy galaxy, BuiltObjectList builtObjects)
        {
            this.SetFonts();
            this.RepointImageInstances();
            this._Hotspots.Clear();
            this._AddHotspots = true;
            this._Game = game;
            this._Galaxy = galaxy;
            this._BuiltObjects = builtObjects;
            this._BuiltObject = (BuiltObject)null;
            this._Fighter = (Fighter)null;
            this._Habitat = (Habitat)null;
            this._Creature = (Creature)null;
            this._ShipGroup = (ShipGroup)null;
            this._SystemInfo = (SystemInfo)null;
            this._Picture = (Bitmap)null;
            this._SmugglingMission = (EmpireActivity)null;
            if (builtObjects != null && builtObjects.Count > 0)
                this.SetEmpirePictureAndColor(builtObjects[0].Empire);
            else
                this.SetEmpirePictureAndColor((Empire)null);
            this._CharacterBonuses = string.Empty;
            this._PictureAngle = 0.0;
            this._PictureSize = 0;
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
        }

        public void SetData(Game game, Galaxy galaxy, Bitmap backgroundPicture, SystemInfo systemInfo)
        {
            this.SetFonts();
            this.RepointImageInstances();
            this._Hotspots.Clear();
            this._AddHotspots = true;
            this._Game = game;
            this._Galaxy = galaxy;
            this._BuiltObjects = (BuiltObjectList)null;
            this._BuiltObject = (BuiltObject)null;
            this._Fighter = (Fighter)null;
            this._Habitat = (Habitat)null;
            this._Creature = (Creature)null;
            this._ShipGroup = (ShipGroup)null;
            this._SystemInfo = systemInfo;
            this._Picture = (Bitmap)null;
            this._SmugglingMission = (EmpireActivity)null;
            this.SetEmpirePictureAndColor((Empire)null);
            this._EmpireColor = this._UnknownColor;
            this._CharacterBonuses = string.Empty;
            this._Picture = backgroundPicture;
            this._PictureAngle = 0.0;
            this._PictureSize = (int)systemInfo.SystemStar.Diameter;
            if (this._PictureSize < this._MinPictureSize)
                this._PictureSize = this._MinPictureSize;
            if (this._PictureSize > this._MaxPictureSize)
                this._PictureSize = this._MaxPictureSize;
            this._Picture = this.PreProcessImage(this._Picture, this._PictureSize);
            Empire empire = this._Galaxy.CheckSystemOwnership(systemInfo.SystemStar);
            this._EmpireColor = this._Game.PlayerEmpire.CheckSystemExplored(systemInfo.SystemStar.SystemIndex) ? (empire == null ? this._WhiteColor : empire.MainColor) : this._UnknownColor;
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
        }

        public void SetData(Game game, Galaxy galaxy, ShipGroup shipGroup)
        {
            this.SetFonts();
            this.RepointImageInstances();
            this._Hotspots.Clear();
            this._AddHotspots = true;
            this._Game = game;
            this._Galaxy = galaxy;
            this._BuiltObjects = (BuiltObjectList)null;
            this._BuiltObject = (BuiltObject)null;
            this._Fighter = (Fighter)null;
            this._Habitat = (Habitat)null;
            this._Creature = (Creature)null;
            this._ShipGroup = shipGroup;
            this._SystemInfo = (SystemInfo)null;
            this._Picture = (Bitmap)null;
            this._SmugglingMission = (EmpireActivity)null;
            this.SetEmpirePictureAndColor(shipGroup.Empire);
            this._PictureAngle = 0.0;
            this._PictureSize = 0;
            this._CharacterBonuses = Galaxy.GenerateCharacterBonusDescription(shipGroup);
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
        }

        public void SetData(
          Game game,
          Galaxy galaxy,
          Bitmap backgroundPicture,
          Bitmap maskImage,
          Creature creature)
        {
            this.SetFonts();
            this.RepointImageInstances();
            this._Hotspots.Clear();
            this._AddHotspots = true;
            this._Game = game;
            this._Galaxy = galaxy;
            this._BuiltObjects = (BuiltObjectList)null;
            this._BuiltObject = (BuiltObject)null;
            this._Fighter = (Fighter)null;
            this._Habitat = (Habitat)null;
            this._Creature = creature;
            this._ShipGroup = (ShipGroup)null;
            this._SystemInfo = (SystemInfo)null;
            this._SmugglingMission = (EmpireActivity)null;
            if (this._Picture != null)
                this._Picture.Dispose();
            this._Picture = this.FadeImage(backgroundPicture, 0.33f);
            if (this._MaskImage != null)
                this._MaskImage.Dispose();
            if (maskImage != null && maskImage.PixelFormat != PixelFormat.Undefined)
                this._MaskImage = new Bitmap((Image)maskImage);
            this.SetEmpirePictureAndColor((Empire)null);
            this._CharacterBonuses = string.Empty;
            this._EmpirePicture = (Bitmap)null;
            this._PictureAngle = (double)creature.CurrentHeading * -1.0;
            this._PictureSize = (int)((double)creature.Size / 0.6);
            if (this._PictureSize < this._MinPictureSize)
                this._PictureSize = this._MinPictureSize;
            if (this._PictureSize > this._MaxPictureSize)
                this._PictureSize = this._MaxPictureSize;
            using (Graphics graphics = this.CreateGraphics())
            {
                this.ClearPanel(graphics);
                this.DrawPanelWithBackground(graphics);
            }
        }

        private void DrawSystemInfo(SystemInfo systemInfo, Graphics graphics)
        {
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            SystemVisibilityStatus visibilityStatus = this._Game.PlayerEmpire.CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
            if (this._Game.GodMode)
                visibilityStatus = SystemVisibilityStatus.Visible;
            Color color = this._WhiteColor;
            if (visibilityStatus == SystemVisibilityStatus.Unexplored)
                color = this._UnknownColor;
            SolidBrush solidBrush = new SolidBrush(color);
            int y1 = 6;
            int rowHeight = this._RowHeight;
            int x1 = 5;
            int num1 = this.ClientRectangle.Width - 10;
            Font titleFont = this._TitleFont;
            Empire relatedObject = this._Galaxy.CheckSystemOwnership(systemInfo.SystemStar);
            string text1 = systemInfo.SystemStar.Name;
            string str1 = string.Empty;
            if (systemInfo.SystemStar.Category == HabitatCategoryType.Star && systemInfo.SystemStar.Type != HabitatType.BlackHole)
                str1 = TextResolver.GetText("System");
            else if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                str1 = TextResolver.GetText("HabitatCategoryType GasCloud");
            else if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
                str1 = TextResolver.GetText("HabitatType BlackHole");
            if (systemInfo.SystemStar.Type != HabitatType.BlackHole)
                text1 = text1 + " " + str1;
            this.DrawBackgroundPicture(graphics);
            int labelWidth1 = this._LabelWidth;
            int y2 = y1 + titleFont.Height + 5 + (rowHeight + 3);
            Rectangle rectangle1 = new Rectangle(x1 - 2, y2, x1 + labelWidth1 - 1, this.ClientRectangle.Height - (y2 + 2));
            int labelWidth2 = labelWidth1 - 5;
            Point point = new Point(x1, y1);
            if (visibilityStatus == SystemVisibilityStatus.Explored || visibilityStatus == SystemVisibilityStatus.Visible)
                this.DrawStringWithDropShadow(graphics, text1, titleFont, point, new SolidBrush(this._EmpireColor));
            else
                this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Unknown") + " " + str1 + ")", titleFont, point, new SolidBrush(this._UnknownColor));
            int y3 = y1 + (titleFont.Height + 5);
            point = new Point(x1, y3);
            string str2 = string.Empty;
            if (systemInfo.SystemStar != null)
                str2 = systemInfo.SystemStar.Type != HabitatType.BlackHole ? Galaxy.ResolveDescription(systemInfo.SystemStar.Type) + " " + Galaxy.ResolveDescription(systemInfo.SystemStar.Category) : TextResolver.GetText("HabitatType BlackHole");
            string empty1 = string.Empty;
            bool flag = false;
            if (systemInfo.DominantEmpire != null && this._Game.PlayerEmpire.EmpiresViewable.Contains(systemInfo.DominantEmpire.Empire))
                flag = true;
            string text2 = visibilityStatus == SystemVisibilityStatus.Visible || visibilityStatus == SystemVisibilityStatus.Explored || this._Game.GodMode || flag ? (systemInfo.SystemStar.Category != HabitatCategoryType.GasCloud ? str2 + ", " + string.Format(TextResolver.GetText("X planets, Y moons"), (object)systemInfo.PlanetCount.ToString(), (object)systemInfo.MoonCount.ToString()) : str2) : str2;
            this.DrawStringWithDropShadow(graphics, text2, this._NormalFont, point, solidBrush);
            int num2 = y3 + rowHeight;
            if (visibilityStatus == SystemVisibilityStatus.Explored || visibilityStatus == SystemVisibilityStatus.Visible)
            {
                if (systemInfo.SystemStar.ResearchBonus > (byte)0)
                {
                    int y4 = num2 + this._Height5;
                    point = new Point(x1, y4);
                    float num3 = (float)systemInfo.SystemStar.ResearchBonus / 100f;
                    string description = string.Format(TextResolver.GetText("X research bonus to AREA"), (object)num3.ToString("+0%"), (object)Galaxy.ResolveDescription(systemInfo.SystemStar.ResearchBonusIndustry));
                    this.DrawLabelledDescription(graphics, TextResolver.GetText("Research"), labelWidth2, description, point, solidBrush);
                    num2 = y4 + rowHeight;
                }
                if ((double)systemInfo.SystemStar.ScenicFactor > 0.0)
                {
                    int y5 = num2 + this._Height5;
                    point = new Point(x1, y5);
                    string description = systemInfo.SystemStar.ScenicFactor.ToString("+0%");
                    this.DrawLabelledDescription(graphics, TextResolver.GetText("Scenery"), labelWidth2, description, point, solidBrush);
                    num2 = y5 + rowHeight;
                }
            }
            int y6 = num2 + this._Height5;
            Point location = new Point(x1, y6 - 2);
            this.DrawLabel(graphics, TextResolver.GetText("Owner"), labelWidth2, location);
            Size flagSizeSystem = this._FlagSizeSystem;
            Rectangle srcRect = Rectangle.Empty;
            Rectangle rectangle2 = Rectangle.Empty;
            this.SetGraphicsQualityToHigh(graphics);
            if (relatedObject != null)
            {
                int x2 = x1 + labelWidth2 + 10;
                string empty2 = string.Empty;
                if (visibilityStatus == SystemVisibilityStatus.Visible || visibilityStatus == SystemVisibilityStatus.Explored || this._Game.GodMode || flag)
                {
                    point = new Point(x2, y6);
                    srcRect = new Rectangle(0, 0, relatedObject.LargeFlagPicture.Width, relatedObject.LargeFlagPicture.Height);
                    rectangle2 = new Rectangle(point, flagSizeSystem);
                    graphics.DrawImage((Image)relatedObject.LargeFlagPicture, rectangle2, srcRect, GraphicsUnit.Pixel);
                    if (relatedObject != null)
                        this.AddHotspot(rectangle2, (object)relatedObject, relatedObject.Name + " (" + TextResolver.GetText("click for details") + ")");
                    int x3 = x2 + (flagSizeSystem.Width + 2);
                    point = new Point(x3, y6);
                    string name = relatedObject.Name;
                    this.DrawStringWithDropShadow(graphics, name, this._NormalFont, point, solidBrush);
                    int x4 = x3 + (int)graphics.MeasureString(name, this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width + 10;
                    point = new Point(x4, y6);
                    if (systemInfo.DominantEmpire != null)
                    {
                        graphics.DrawImageUnscaled((Image)InfoPanel._ColonyImage, point);
                        point = new Point(x4 + InfoPanel._ColonyImage.Width, y6);
                        string text3 = systemInfo.DominantEmpire.ColonyCount.ToString();
                        double width = (double)graphics.MeasureString(text3, this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                        this.DrawStringWithDropShadow(graphics, text3, this._NormalFont, point, solidBrush);
                    }
                }
                else
                {
                    point = new Point(x2, y6);
                    string text4 = "(" + TextResolver.GetText("Unknown") + ")";
                    this.DrawStringWithDropShadow(graphics, text4, this._NormalFont, point, solidBrush);
                    int num4 = x2 + (int)graphics.MeasureString(text4, this._NormalFont, this._MaxGraphTextWidth, StringFormat.GenericTypographic).Width + 10;
                }
            }
            else
            {
                point = new Point(x1 + 10 + labelWidth2, y6);
                if (visibilityStatus == SystemVisibilityStatus.Visible || visibilityStatus == SystemVisibilityStatus.Explored || this._Game.GodMode)
                    this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", this._NormalFont, point, solidBrush);
                else
                    this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Unknown") + ")", this._NormalFont, point, solidBrush);
            }
            int y7 = y6 + (rowHeight + this._Height6);
            location = new Point(x1, y7 - 2);
            this.DrawLabel(graphics, TextResolver.GetText("Resource"), labelWidth2, location);
            point = new Point(x1 + 10 + labelWidth2, y7);
            if (visibilityStatus == SystemVisibilityStatus.Explored || visibilityStatus == SystemVisibilityStatus.Visible)
            {
                HabitatResourceList habitatResourceList1 = new HabitatResourceList();
                if (systemInfo.SystemStar.Resources != null && systemInfo.SystemStar.Resources.Count > 0)
                {
                    HabitatResourceList habitatResourceList2 = systemInfo.SystemStar.Resources.Clone();
                    for (int index = 0; index < habitatResourceList2.Count; ++index)
                    {
                        if (!habitatResourceList1.Contains(habitatResourceList2[index]))
                            habitatResourceList1.Add(habitatResourceList2[index]);
                    }
                }
                if (systemInfo.Habitats != null)
                {
                    for (int index1 = 0; index1 < systemInfo.Habitats.Count; ++index1)
                    {
                        if (this._Game.PlayerEmpire.ResourceMap != null && this._Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(systemInfo.Habitats[index1]))
                        {
                            HabitatResourceList habitatResourceList3 = systemInfo.Habitats[index1].Resources.Clone();
                            for (int index2 = 0; index2 < habitatResourceList3.Count; ++index2)
                            {
                                if (!habitatResourceList1.Contains(habitatResourceList3[index2]))
                                    habitatResourceList1.Add(habitatResourceList3[index2]);
                            }
                        }
                    }
                }
                int num5 = 2;
                int x5 = labelWidth2 + 10 + x1;
                if (habitatResourceList1.Count > 0)
                {
                    for (int index = 0; index < habitatResourceList1.Count; ++index)
                    {
                        int width = this._ResourceImages[habitatResourceList1[index].PictureRef].Width;
                        if (x5 + width + num5 > num1)
                        {
                            y7 += rowHeight + this._Height2;
                            x5 = labelWidth2 + 10 + x1 - num5;
                        }
                        point = new Point(x5, y7);
                        graphics.DrawImageUnscaled((Image)this._ResourceImages[habitatResourceList1[index].PictureRef], point);
                        this.AddHotspot(new Rectangle(point, this._ResourceImages[habitatResourceList1[index].PictureRef].Size), (object)habitatResourceList1[index], habitatResourceList1[index].Name + " (" + TextResolver.GetText("click for details") + ")");
                        x5 = x5 + width + num5;
                    }
                }
                else
                    this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", this._NormalFont, point, solidBrush);
            }
            else
                this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Unknown") + ")", this._NormalFont, point, solidBrush);
            int startY = y7 + (rowHeight + rowHeight);
            if (visibilityStatus == SystemVisibilityStatus.Visible || visibilityStatus == SystemVisibilityStatus.Explored)
                this.DrawSystemColoniesSummary(systemInfo.SystemStar, graphics, startY, labelWidth2);
            solidBrush.Dispose();
        }

        private void DrawShipGroup(ShipGroup shipGroup, Graphics graphics)
        {
            //BaconInfoPanel.DrawShipGroup(this, shipGroup, graphics);
            InfoPanel.OnDrawShipGroupMods(this, shipGroup, graphics);
        }

        private Color UpdateColor(Color normalColor, Color alternateColor)
        {
            int second = DateTime.Now.ToUniversalTime().Second;
            int millisecond = DateTime.Now.ToUniversalTime().Millisecond;
            if (second % 2 == 1)
                millisecond += 1000;
            double num = millisecond <= 1000 ? (double)Math.Abs(1000 - millisecond) / 1000.0 : (double)(millisecond - 1000) / 1000.0;
            return Color.FromArgb((int)(byte)((uint)normalColor.A - (uint)(byte)((double)((int)normalColor.A - (int)alternateColor.A) * num)), (int)(byte)((uint)normalColor.R - (uint)(byte)((double)((int)normalColor.R - (int)alternateColor.R) * num)), (int)(byte)((uint)normalColor.G - (uint)(byte)((double)((int)normalColor.G - (int)alternateColor.G) * num)), (int)(byte)((uint)normalColor.B - (uint)(byte)((double)((int)normalColor.B - (int)alternateColor.B) * num)));
        }

        private void DrawBuiltObjectSelection(BuiltObjectList builtObjects, Graphics graphics)
        {
            if (builtObjects.Count <= 0)
                return;
            SolidBrush solidBrush = new SolidBrush(this._UnknownColor);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            bool flag = false;
            if (builtObjects[0].Empire != this._Game.PlayerEmpire && this._Game.PlayerEmpire.EmpiresViewable.Contains(builtObjects[0].Empire))
                flag = true;
            int y1 = 6;
            int rowHeight = this._RowHeight;
            int x = 5;
            int num1 = this.ClientRectangle.Width - 10;
            Point point = new Point(num1 - (this._FlagSizeSmall.Width - 2), 6);
            if (this._EmpirePicture != null)
                graphics.DrawImageUnscaled((Image)this._EmpirePicture, point);
            Font titleFont = this._TitleFont;
            Point location = new Point(x, y1);
            this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Multiple Ships") + ")", titleFont, location, new SolidBrush(this._EmpireColor));
            int y2 = y1 + (titleFont.Height + this._Height5);
            string empty = string.Empty;
            string text;
            if (builtObjects[0].Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag)
            {
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                for (int index = 0; index < builtObjects.Count; ++index)
                {
                    BuiltObject builtObject = builtObjects[index];
                    num2 += builtObject.FirepowerRaw;
                    num3 += builtObject.CalculateAvailableAssaultPodAttackStrength(this._Galaxy.CurrentDateTime);
                    if (builtObject.Troops != null)
                    {
                        num4 += builtObject.Troops.Count;
                        num5 += builtObject.Troops.TotalAttackStrength;
                    }
                }
                text = builtObjects.Count.ToString() + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture) + ", " + num2.ToString() + " " + TextResolver.GetText("Firepower").ToLower(CultureInfo.InvariantCulture) + ", " + num3.ToString("0") + " " + TextResolver.GetText("Boarding Strength").ToLower(CultureInfo.InvariantCulture) + ", " + string.Format(TextResolver.GetText("X troops (Y strength)"), (object)num4.ToString(), (object)num5.ToString());
            }
            else
                text = builtObjects.Count.ToString() + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture);
            SizeF size = graphics.MeasureString(text, this._NormalFont, this.ClientSize.Width - (x + 10));
            size = new SizeF(size.Width + 2f, size.Height + 2f);
            location = new Point(x, y2);
            this.DrawStringWithDropShadowBounded(graphics, text, this._NormalFont, location, size);
            int num6 = y2 + (int)size.Height + rowHeight / 4;
            int index1 = 0;
            int num7 = -2;
            int num8 = 0;
            int num9 = 27;
            int num10 = num9;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            while (index1 < builtObjects.Count)
            {
                Image builtObjectImage = (Image)this._BuiltObjectImages[builtObjects[index1].PictureRef];
                location = new Point(x + num7, num6 + num8);
                int num11 = Math.Min((int)(Math.Sqrt((double)builtObjects[index1].Size) * 1.25), num9);
                int num12 = (num9 - num11) / 2;
                Rectangle srcRect = new Rectangle(0, 0, builtObjectImage.Width, builtObjectImage.Height);
                Rectangle destRect = new Rectangle(x + num7 + num12, num6 + num8 + num12, num11, num11);
                Rectangle rectangle = new Rectangle(x + num7, num6 + num8, num9, num9);
                if (builtObjects[index1].DamagedComponentCount > 0 && (builtObjects[0].Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag))
                    graphics.FillRectangle((Brush)new SolidBrush(Color.FromArgb(64, (int)byte.MaxValue, 0, 64)), rectangle);
                graphics.DrawImage(builtObjectImage, destRect, srcRect, GraphicsUnit.Pixel);
                this.AddHotspot(rectangle, (object)builtObjects[index1], builtObjects[index1].Name + " (" + TextResolver.GetText("click to select") + ")");
                if (builtObjects[0].Empire == this._Game.PlayerEmpire || this._Game.GodMode || flag)
                {
                    int height = (int)(builtObjects[index1].CurrentFuel / (double)builtObjects[index1].FuelCapacity * (double)(num9 - 4));
                    Rectangle rect = new Rectangle(x + num7 + num9 - 4, num6 + num8 + (num9 - 2 - height), 2, height);
                    graphics.FillRectangle((Brush)new SolidBrush(Color.Green), rect);
                }
                ++index1;
                if (num9 > num10)
                    num10 = num9;
                num7 += num9;
                if (num7 > num1 - num9)
                {
                    num8 += num10;
                    num7 = -2;
                }
            }
            solidBrush.Dispose();
        }

        private static string OnFormatForLargeNumbersMods(long value)
        {
            var tmp = InfoPanel.FormatForLargeNumbersMods;
            string res = value.ToString();
            if (tmp != null)
            {
                var args = new FormatForLargeNumbersModsArgs(value);
                tmp(null, args);
                res = args.Result;
            }
            return res;
        }
        private static void OnDrawBuiltObjectMods(InfoPanel infoPanel, BuiltObject builtObject, Graphics graphics)
        {
            var tmp = InfoPanel.DrawBuiltObjectMods;
            if (tmp != null)
            {
                var args = new DrawBuiltObjectModsArgs(infoPanel, builtObject, graphics);
                tmp(null, args);
            }
        }
        private static void OnDrawStringRedWithDropShadowMods(InfoPanel panel, Graphics graphics, string text, Font font, Point location)
        {
            var tmp = InfoPanel.DrawStringRedWithDropShadowtMods;
            if (tmp != null)
            {
                var args = new DrawStringRedWithDropShadowtModsArgs(panel, graphics, text, font, location);
                tmp(null, args);
            }
        }
        private static void OnDrawStringWithDropShadowMods(InfoPanel panel, Graphics graphics, string text, Font font, Point location, SolidBrush brush)
        {
            var tmp = InfoPanel.DrawStringWithDropShadowMods;
            if (tmp != null)
            {
                var args = new DrawStringWithDropShadowModsArgs(panel, graphics, text, font, location, brush);
                tmp(null, args);
            }
        }
        private static void OnDrawStringColorWithDropShadowMods(InfoPanel panel, Graphics graphics, string text, Font font, Point location, Color color)
        {
            var tmp = InfoPanel.DrawStringColorWithDropShadowMods;
            if (tmp != null)
            {
                var args = new DrawStringColorWithDropShadowModsArgs(panel, graphics, text, font, location, color);
                tmp(null, args);
            }
        }
        private static void OnDrawStringWithDropShadowBoundedMods(InfoPanel panel, Graphics graphics, string text, Font font, Point location, SizeF size, SolidBrush brush)
        {
            var tmp = InfoPanel.DrawStringWithDropShadowBoundedMods;
            if (tmp != null)
            {
                var args = new DrawStringWithDropShadowBoundedModsArgs(panel, graphics, text, font, location, size, brush);
                tmp(null, args);
            }
        }
        private static void OnDrawShipGroupMods(InfoPanel panel, ShipGroup shipGroup, Graphics graphics)
        {
            var tmp = InfoPanel.DrawShipGroupMods;
            if (tmp != null)
            {
                var args = new DrawShipGroupModsArgs(panel, shipGroup, graphics);
                tmp(null, args);
            }
        }
        private static void OnDrawStringWithDropShadowMods(InfoPanel panel, Graphics graphics, string text, Font font, Point location, SolidBrush brush, int maxWidth)
        {
            var tmp = InfoPanel.DrawStringWithDropShadowMods2;
            if (tmp != null)
            {
                var args = new DrawStringWithDropShadowModsArgs2(panel, graphics, text, font, location, brush, maxWidth);
                tmp(null, args);
            }
        }
    }
}
