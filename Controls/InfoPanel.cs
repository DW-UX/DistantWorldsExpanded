// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.InfoPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

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
using BaconDistantWorlds;

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

        private DistantWorlds.Controls.LinearGradientMode _GradientMode = DistantWorlds.Controls.LinearGradientMode.Vertical;

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

        public SolidBrush _LabelAreaBrush = new SolidBrush(Color.FromArgb(63, 127, 32, 32));

        private Pen _BlackPen = new Pen(Color.Black);

        private SolidBrush _SemiSubtleBrush = new SolidBrush(Color.FromArgb(48, 48, 96));

        private SolidBrush _BrightBrush = new SolidBrush(Color.FromArgb(96, 96, 255));

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

        public bool ContentSizeIsLarge => _ContentSizeIsLarge;

        public bool AddHotspots
        {
            get
            {
                return _AddHotspots;
            }
            set
            {
                _AddHotspots = value;
            }
        }

        public HotspotList Hotspots => _Hotspots;

        [Description("The primary background color used to display text and graphics in the control.")]
        [DefaultValue(typeof(Color), "Window")]
        [Category("Appearance")]
        public new Color BackColor
        {
            get
            {
                return _BackColour1;
            }
            set
            {
                _BackColour1 = value;
                DesignModeInvalidate();
            }
        }

        [DefaultValue(typeof(Color), "Window")]
        [Category("Appearance")]
        [Description("The secondary background color used to paint the control.")]
        public Color BackColor2
        {
            get
            {
                return _BackColour2;
            }
            set
            {
                _BackColour2 = value;
                DesignModeInvalidate();
            }
        }

        [DefaultValue(typeof(Color), "Window")]
        [Category("Appearance")]
        [Description("The third background color used to paint the control.")]
        public Color BackColor3
        {
            get
            {
                return _BackColour3;
            }
            set
            {
                _BackColour3 = value;
                DesignModeInvalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(DistantWorlds.Controls.LinearGradientMode), "None")]
        [Description("The gradient direction used to paint the control.")]
        public DistantWorlds.Controls.LinearGradientMode GradientMode
        {
            get
            {
                return _GradientMode;
            }
            set
            {
                _GradientMode = value;
                DesignModeInvalidate();
            }
        }

        [Category("Appearance")]
        [Description("The border style used to paint the control.")]
        [DefaultValue(typeof(BorderStyle), "None")]
        public new BorderStyle BorderStyle
        {
            get
            {
                return _BorderStyle;
            }
            set
            {
                _BorderStyle = value;
                DesignModeInvalidate();
            }
        }

        [Description("The border color used to paint the control.")]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "WindowFrame")]
        public Color BorderColor
        {
            get
            {
                return _BorderColour;
            }
            set
            {
                _BorderColour = value;
                DesignModeInvalidate();
            }
        }

        [Description("The width of the border used to paint the control.")]
        [Category("Appearance")]
        [DefaultValue(typeof(int), "1")]
        public int BorderWidth
        {
            get
            {
                return _BorderWidth;
            }
            set
            {
                _BorderWidth = value;
                DesignModeInvalidate();
            }
        }

        [Category("Appearance")]
        [Description("The radius of the curve used to paint the corners of the control.")]
        [DefaultValue(typeof(int), "0")]
        public int Curvature
        {
            get
            {
                return _Curvature;
            }
            set
            {
                _Curvature = value;
                DesignModeInvalidate();
            }
        }

        [Description("The style of the curves to be drawn on the control.")]
        [Category("Appearance")]
        [DefaultValue(typeof(CornerCurveMode), "All")]
        public CornerCurveMode CurveMode
        {
            get
            {
                return _CurveMode;
            }
            set
            {
                _CurveMode = value;
                DesignModeInvalidate();
            }
        }

        private int AdjustedCurve
        {
            get
            {
                int num = 0;
                if (_CurveMode != 0)
                {
                    num = ((_Curvature <= base.ClientRectangle.Width / 2) ? _Curvature : DoubleToInt(base.ClientRectangle.Width / 2));
                    if (num > base.ClientRectangle.Height / 2)
                    {
                        num = DoubleToInt(base.ClientRectangle.Height / 2);
                    }
                }
                return num;
            }
        }

        public bool ShowExtendedInfo
        {
            get
            {
                return _ShowExtendedInfo;
            }
            set
            {
                _ShowExtendedInfo = value;
            }
        }

        public Bitmap Picture
        {
            get
            {
                return _Picture;
            }
            set
            {
                _Picture = value;
                if (_Picture != null)
                {
                    _PictureSize = _Picture.Width;
                }
                else
                {
                    _PictureSize = 0;
                }
            }
        }

        public virtual void SetFontCache(IFontCache fontCache)
        {
            _FontCache = fontCache;
            if (_FontSize > 0f)
            {
                Font = _FontCache.GenerateFont(_FontSize, _FontIsBold);
            }
        }

        public void SetFont(float pixelSize)
        {
            SetFont(pixelSize, isBold: false);
        }

        public void SetFont(float pixelSize, bool isBold)
        {
            _FontSize = pixelSize;
            _FontIsBold = isBold;
            if (_FontCache != null)
            {
                Font font = Font;
                Font = _FontCache.GenerateFont(_FontSize, _FontIsBold);
            }
        }

        public void AddHotspot(Rectangle region, object relatedObject)
        {
            AddHotspot(region, relatedObject, string.Empty);
        }

        public void AddHotspot(Rectangle region, object relatedObject, string message)
        {
            if (_AddHotspots)
            {
                _Hotspots.Add(new Hotspot(region, relatedObject, message));
            }
        }

        private void DesignModeInvalidate()
        {
            if (base.DesignMode)
            {
                Invalidate();
            }
        }

        private void SetDefaultControlStyles()
        {
            SetStyle(ControlStyles.ResizeRedraw, value: true);
            SetStyle(ControlStyles.UserMouse, value: true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
            SetStyle(ControlStyles.UserPaint, value: true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
            SetStyle(ControlStyles.ContainerControl, value: false);
            UpdateStyles();
        }

        private void CustomInitialisation()
        {
            SuspendLayout();
            base.BackColor = Color.Transparent;
            BorderStyle = BorderStyle.None;
            ResumeLayout(performLayout: false);
        }

        public void DrawBackground(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = GetPath();
            Rectangle clientRectangle = base.ClientRectangle;
            if (base.ClientRectangle.Width == 0)
            {
                clientRectangle.Width++;
            }
            if (base.ClientRectangle.Height == 0)
            {
                clientRectangle.Height++;
            }
            LinearGradientBrush linearGradientBrush;
            if (_GradientMode == DistantWorlds.Controls.LinearGradientMode.None)
            {
                linearGradientBrush = new LinearGradientBrush(clientRectangle, _BackColour1, _BackColour1, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            }
            else
            {
                ColorBlend colorBlend = new ColorBlend(3);
                Color[] colors = new Color[3] { _BackColour1, _BackColour2, _BackColour3 };
                float[] positions = new float[3] { 0f, 0.5f, 1f };
                colorBlend.Colors = colors;
                colorBlend.Positions = positions;
                linearGradientBrush = new LinearGradientBrush(clientRectangle, _BackColour1, _BackColour2, (System.Drawing.Drawing2D.LinearGradientMode)_GradientMode);
                linearGradientBrush.InterpolationColors = colorBlend;
            }
            graphics.FillPath(linearGradientBrush, path);
            linearGradientBrush.Dispose();
            switch (_BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    {
                        Pen pen = new Pen(_BorderColour, _BorderWidth);
                        graphics.DrawPath(pen, path);
                        pen.Dispose();
                        break;
                    }
                case BorderStyle.Fixed3D:
                    PersistentGradientPanel.DrawBorder3D(graphics, base.ClientRectangle);
                    break;
            }
            linearGradientBrush.Dispose();
            path.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawBackground(e.Graphics);
        }

        protected GraphicsPath GetPath()
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            if (_BorderStyle == BorderStyle.Fixed3D)
            {
                graphicsPath.AddRectangle(base.ClientRectangle);
                return graphicsPath;
            }
            try
            {
                int num = 0;
                Rectangle clientRectangle = base.ClientRectangle;
                int num2 = 0;
                switch (_BorderStyle)
                {
                    case BorderStyle.FixedSingle:
                        if (_BorderWidth > 1)
                        {
                            num2 = DoubleToInt(BorderWidth / 2);
                        }
                        num = AdjustedCurve;
                        break;
                    case BorderStyle.None:
                        num = AdjustedCurve;
                        break;
                }
                if (num == 0)
                {
                    graphicsPath.AddRectangle(clientRectangle);
                    return graphicsPath;
                }
                int num3 = clientRectangle.Width - num2;
                int num4 = clientRectangle.Height - num2;
                int num5 = 1;
                num5 = (((_CurveMode & CornerCurveMode.TopRight) == 0) ? 1 : (num * 2));
                graphicsPath.AddArc(num3 - num5, num2, num5, num5, 270f, 90f);
                num5 = (((_CurveMode & CornerCurveMode.BottomRight) == 0) ? 1 : (num * 2));
                graphicsPath.AddArc(num3 - num5, num4 - num5, num5, num5, 0f, 90f);
                num5 = (((_CurveMode & CornerCurveMode.BottomLeft) == 0) ? 1 : (num * 2));
                graphicsPath.AddArc(num2, num4 - num5, num5, num5, 90f, 90f);
                num5 = (((_CurveMode & CornerCurveMode.TopLeft) == 0) ? 1 : (num * 2));
                graphicsPath.AddArc(num2, num2, num5, num5, 180f, 90f);
                graphicsPath.CloseFigure();
                return graphicsPath;
            }
            catch (Exception)
            {
                graphicsPath.AddRectangle(base.ClientRectangle);
                return graphicsPath;
            }
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

        public static int DoubleToInt(double value)
        {
            return decimal.ToInt32(decimal.Floor(decimal.Parse(value.ToString(), NumberFormatInfo.InvariantInfo)));
        }

        public InfoPanel()
        {
            SetDefaultControlStyles();
            CustomInitialisation();
            AutoScroll = false;
            base.AutoScrollMargin = new Size(0, 0);
            base.Padding = new Padding(0);
            base.Margin = new Padding(0);
            SetFont(15.33f);
            _NormalFont = Font;
            _NormalFontBold = new Font(Font, FontStyle.Bold);
            _HotspotPen = new Pen(Color.FromArgb(170, 170, 170), 1f);
            _HotspotPen.DashStyle = DashStyle.Dot;
            using Graphics graphics = CreateGraphics();
            DrawPanelWithBackground(graphics);
        }

        public void Reset()
        {
            RepointImageInstances();
            using Graphics graphics = CreateGraphics();
            ClearPanel(graphics);
            DrawPanelWithBackground(graphics);
        }

        private void ClearPanel(Graphics graphics)
        {
            graphics.Clear(Color.Transparent);
        }

        public void Clear()
        {
            _Picture = null;
            _EmpireColor = _WhiteColor;
            Text = string.Empty;
        }

        public void ReDraw()
        {
            if (_BuiltObject != null)
            {
                _PictureAngle = _BuiltObject.Heading * -1f;
                if (_BuiltObject.ActualEmpire != null)
                {
                    _EmpirePicture = PrescaleImage(_BuiltObject.ActualEmpire.LargeFlagPicture, _FlagSizeSmall.Width, _FlagSizeSmall.Height);
                }
            }
            else if (_Fighter != null)
            {
                _PictureAngle = _Fighter.Heading * -1f;
                if (_Fighter.Empire != null)
                {
                    _EmpirePicture = PrescaleImage(_Fighter.Empire.LargeFlagPicture, _FlagSizeSmall.Width, _FlagSizeSmall.Height);
                }
            }
            else if (_Habitat != null)
            {
                _PictureAngle = 0.0;
                if (_Habitat.Empire != null && _Habitat.Empire != _Galaxy.IndependentEmpire)
                {
                    _EmpirePicture = PrescaleImage(_Habitat.Empire.LargeFlagPicture, _FlagSizeSmall.Width, _FlagSizeSmall.Height);
                }
            }
            Invalidate();
        }

        public void ClearData()
        {
            _Hotspots.Clear();
            _Game = null;
            _Galaxy = null;
            _BuiltObject = null;
            _Fighter = null;
            _Habitat = null;
            _Creature = null;
            _BuiltObjects = null;
            _ShipGroup = null;
            _SystemInfo = null;
            _Picture = null;
            _EmpirePicture = null;
            _EmpireColor = _WhiteColor;
            _ActualEmpire = null;
            _SmugglingMission = null;
            _PictureAngle = 0.0;
            _PictureSize = 0;
            using (Graphics graphics = CreateGraphics())
            {
                ClearPanel(graphics);
                DrawPanelWithBackground(graphics);
            }
            Invalidate();
        }

        private void SetEmpirePictureAndColor(Empire empire)
        {
            if (empire != null)
            {
                _EmpirePicture = PrescaleImage(empire.LargeFlagPicture, _FlagSizeSmall.Width, _FlagSizeSmall.Height);
                if (empire == _Galaxy.IndependentEmpire)
                {
                    _EmpireColor = _IndependentColor;
                }
                else if (empire.PirateEmpireBaseHabitat != null)
                {
                    if (empire.MainColor == Color.FromArgb(1, 1, 1))
                    {
                        _EmpireColor = _PirateColor;
                    }
                    else
                    {
                        _EmpireColor = empire.MainColor;
                    }
                }
                else
                {
                    _EmpireColor = empire.MainColor;
                }
            }
            else
            {
                _EmpireColor = _WhiteColor;
                _EmpirePicture = new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
            }
        }

        private void SetFonts()
        {
            SetFont(15.33f);
            _NormalFontNormalSize = CreateDisposeFont(_NormalFontNormalSize, 15.33f, isBold: false);
            _NormalBoldFontNormalSize = CreateDisposeFont(_NormalBoldFontNormalSize, 15.33f, isBold: true);
            _TitleFontNormalSize = CreateDisposeFont(_TitleFontNormalSize, 17.33f, isBold: true);
            _TinyFontNormalSize = CreateDisposeFont(_TinyFontNormalSize, 10.67f, isBold: false);
            _NormalFontLargeSize = CreateDisposeFont(_NormalFontLargeSize, 21.4f, isBold: false);
            _NormalBoldFontLargeSize = CreateDisposeFont(_NormalBoldFontLargeSize, 21.4f, isBold: true);
            _TitleFontLargeSize = CreateDisposeFont(_TitleFontLargeSize, 23.4f, isBold: true);
            _TinyFontLargeSize = CreateDisposeFont(_TinyFontLargeSize, 15.33f, isBold: false);
            if (_ContentSizeIsLarge)
            {
                _NormalFont = _NormalFontLargeSize;
                _NormalFontBold = _NormalBoldFontLargeSize;
                _TitleFont = _TitleFontLargeSize;
                _TinyFont = _TinyFontLargeSize;
            }
            else
            {
                _NormalFont = _NormalFontNormalSize;
                _NormalFontBold = _NormalBoldFontNormalSize;
                _TitleFont = _TitleFontNormalSize;
                _TinyFont = _TinyFontNormalSize;
            }
        }

        private Font CreateDisposeFont(Font font, float size, bool isBold)
        {
            Font font2 = font;
            if (size > 0f)
            {
                font = _FontCache.GenerateFont(size, isBold);
            }
            font2?.Dispose();
            return font;
        }

        public void SetData(Game game, Galaxy galaxy, Bitmap backgroundPicture, Bitmap maskImage, BuiltObject builtObject)
        {
            SetFonts();
            RepointImageInstances();
            _Hotspots.Clear();
            _AddHotspots = true;
            _Game = game;
            _Galaxy = galaxy;
            _BuiltObjects = null;
            _BuiltObject = builtObject;
            _Fighter = null;
            _Habitat = null;
            _Creature = null;
            _ShipGroup = null;
            _SystemInfo = null;
            _EmpireColor = _WhiteColor;
            _SmugglingMission = null;
            if (_Picture != null)
            {
                _Picture.Dispose();
            }
            _Picture = FadeImage(backgroundPicture, 0.33f);
            if (_MaskImage != null)
            {
                _MaskImage.Dispose();
            }
            if (maskImage != null && maskImage.PixelFormat != 0)
            {
                _MaskImage = new Bitmap(maskImage);
            }
            Empire empire = builtObject.Empire;
            if (builtObject.ActualEmpire == game.PlayerEmpire || game.GodMode)
            {
                empire = builtObject.ActualEmpire;
            }
            SetEmpirePictureAndColor(empire);
            _ActualEmpire = empire;
            if (builtObject.Role == BuiltObjectRole.Freight && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Transport && builtObject.Mission.SecondaryTargetHabitat != null)
            {
                EmpireActivity firstByTargetAndType = _ActualEmpire.PirateMissions.GetFirstByTargetAndType(builtObject.Mission.SecondaryTargetHabitat, EmpireActivityType.Smuggle);
                if (firstByTargetAndType != null && firstByTargetAndType.RequestingEmpire != _ActualEmpire)
                {
                    _SmugglingMission = firstByTargetAndType;
                }
            }
            _PictureAngle = builtObject.Heading * -1f;
            _PictureSize = (int)((double)builtObject.Size / 0.6);
            if (_PictureSize < _MinPictureSize)
            {
                _PictureSize = _MinPictureSize;
            }
            if (_PictureSize > _MaxPictureSize)
            {
                _PictureSize = _MaxPictureSize;
            }
            _CharacterBonuses = Galaxy.GenerateCharacterBonusDescription(builtObject);
            _BuiltObjectIsScanned = _Galaxy.CheckBuiltObjectScanned(_BuiltObject);
            _LastBuiltObjectScanTime = DateTime.Now;
            using Graphics graphics = CreateGraphics();
            ClearPanel(graphics);
            DrawPanelWithBackground(graphics);
        }

        public void SetData(Game game, Galaxy galaxy, Bitmap backgroundPicture, Bitmap maskImage, Fighter fighter)
        {
            SetFonts();
            RepointImageInstances();
            _Hotspots.Clear();
            _AddHotspots = true;
            _Game = game;
            _Galaxy = galaxy;
            _BuiltObjects = null;
            _BuiltObject = null;
            _Fighter = fighter;
            _Habitat = null;
            _Creature = null;
            _ShipGroup = null;
            _SystemInfo = null;
            _EmpireColor = _WhiteColor;
            _SmugglingMission = null;
            if (_Picture != null)
            {
                _Picture.Dispose();
            }
            _Picture = FadeImage(backgroundPicture, 0.33f);
            _CharacterBonuses = Galaxy.GenerateCharacterBonusDescription(fighter);
            if (_MaskImage != null)
            {
                _MaskImage.Dispose();
            }
            if (maskImage != null && maskImage.PixelFormat != 0)
            {
                _MaskImage = new Bitmap(maskImage);
            }
            SetEmpirePictureAndColor(fighter.Empire);
            _PictureAngle = fighter.Heading * -1f;
            _PictureSize = (int)((double)fighter.Size * 15.0);
            if (_PictureSize < _MinPictureSize)
            {
                _PictureSize = _MinPictureSize;
            }
            if (_PictureSize > _MaxPictureSize)
            {
                _PictureSize = _MaxPictureSize;
            }
            using Graphics graphics = CreateGraphics();
            ClearPanel(graphics);
            DrawPanelWithBackground(graphics);
        }

        private Bitmap PreProcessImage(Bitmap image, int pictureSize)
        {
            image = FadeImage(image, 0.33f);
            Rectangle rectangle = default(Rectangle);
            Rectangle rectangle2 = default(Rectangle);
            int num = base.ClientRectangle.Width - 6;
            int num2 = base.ClientRectangle.Height - 6;
            double num3 = (double)image.Width / (double)image.Height;
            int num4;
            int num5;
            if (image.Width > image.Height)
            {
                num4 = (int)((double)pictureSize * num3);
                num5 = _PictureSize;
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
                if ((double)num4 * num6 > (double)num)
                {
                    num6 *= (double)num / ((double)num4 * num6);
                }
            }
            else if (num4 > num)
            {
                num6 = (double)num / (double)num4;
                if ((double)num5 * num6 > (double)num2)
                {
                    num6 *= (double)num2 / ((double)num5 * num6);
                }
            }
            else
            {
                num6 = 1.0;
            }
            int num7 = (num - (int)((double)num4 * num6)) / 2;
            int num8 = (num2 - (int)((double)num5 * num6)) / 2;
            int num9 = (int)((double)num4 * num6);
            rectangle = new Rectangle(num7 + 3, num8 + 3, num9, (int)((double)num5 * num6));
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            rectangle2 = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
            graphics.DrawImage(image, rectangle2);
            return bitmap;
        }

        private Bitmap OverlayRuins(Bitmap image, Habitat habitat)
        {
            Bitmap bitmap = new Bitmap(image);
            if (habitat.Ruin != null)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    Bitmap image2 = new Bitmap(_RuinImages[habitat.Ruin.PictureRef]);
                    image2 = FadeImage(image2, 0.7f);
                    double num = (double)image.Width / (double)habitat.Diameter;
                    int num2 = (int)(((double)(habitat.Diameter / 2) + habitat.Ruin.ParentX) * num - (double)(image2.Width / 2));
                    int num3 = (int)(((double)(habitat.Diameter / 2) + habitat.Ruin.ParentY) * num - (double)(image2.Height / 2));
                    graphics.DrawImage(image2, new Point(num2, num3));
                    return bitmap;
                }
            }
            return bitmap;
        }

        public void SetData(Game game, Galaxy galaxy, Bitmap backgroundPicture, Habitat habitat)
        {
            SetFonts();
            RepointImageInstances();
            _Hotspots.Clear();
            _AddHotspots = true;
            _Game = game;
            _Galaxy = galaxy;
            _BuiltObjects = null;
            _BuiltObject = null;
            _Fighter = null;
            _Habitat = habitat;
            _Creature = null;
            _ShipGroup = null;
            _SystemInfo = null;
            _SmugglingMission = null;
            SetEmpirePictureAndColor(habitat.Empire);
            _EmpireCanColonize = _Galaxy.PlayerEmpire.CanEmpireColonizeHabitat(habitat, out _ColonizeExplanation);
            _CharacterBonuses = Galaxy.GenerateCharacterBonusDescription(habitat);
            if (_Picture != null)
            {
                _Picture.Dispose();
            }
            _Picture = backgroundPicture;
            _PictureAngle = 0.0;
            _PictureSize = habitat.Diameter;
            if (_PictureSize < _MinPictureSize)
            {
                _PictureSize = _MinPictureSize;
            }
            if (_PictureSize > _MaxPictureSize)
            {
                _PictureSize = _MaxPictureSize;
            }
            _Picture = PreProcessImage(_Picture, _PictureSize);
            using Graphics graphics = CreateGraphics();
            ClearPanel(graphics);
            DrawPanelWithBackground(graphics);
        }

        private void DrawPanel(object sender, PaintEventArgs pe)
        {
            DrawPanel(pe.Graphics);
        }

        private void DrawPanelWithBackground(Graphics graphics)
        {
            try
            {
                if (_CurveMode != 0)
                {
                    PaintEventArgs e = new PaintEventArgs(graphics, new Rectangle(0, 0, base.Width, base.Height));
                    base.OnPaintBackground(e);
                }
                DrawBackground(graphics);
                DrawPanel(graphics);
            }
            catch (Exception)
            {
                ClearData();
            }
        }

        private Color ResolveLightColor(Color color, int increaseAmount)
        {
            int red = Math.Max(0, Math.Min(255, color.R + increaseAmount));
            int green = Math.Max(0, Math.Min(255, color.G + increaseAmount));
            int blue = Math.Max(0, Math.Min(255, color.B + increaseAmount));
            return Color.FromArgb(red, green, blue);
        }

        internal void DrawPanel(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            if (_BuiltObject != null)
            {
                if (_Game.GodMode || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(_BuiltObject))
                {
                    DrawBuiltObject(_BuiltObject, graphics);
                }
                else
                {
                    _Game.SelectedObject = null;
                }
            }
            else if (_Fighter != null)
            {
                if (_Game.GodMode || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(_Fighter))
                {
                    DrawFighter(_Fighter, graphics);
                }
                else
                {
                    _Game.SelectedObject = null;
                }
            }
            else if (_Habitat != null)
            {
                DrawHabitat(_Habitat, graphics);
            }
            else if (_Creature != null)
            {
                if (_Game.GodMode || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(_Creature))
                {
                    DrawCreature(_Creature, graphics);
                }
                else
                {
                    _Game.SelectedObject = null;
                }
            }
            else if (_BuiltObjects != null)
            {
                DrawBuiltObjectSelection(_BuiltObjects, graphics);
            }
            else if (_ShipGroup != null)
            {
                if (_Game.GodMode || _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(_ShipGroup.LeadShip))
                {
                    DrawShipGroup(_ShipGroup, graphics);
                }
                else
                {
                    _Game.SelectedObject = null;
                }
            }
            else if (_SystemInfo != null)
            {
                DrawSystemInfo(_SystemInfo, graphics);
            }
            DrawHotspotHoverRegions(graphics);
            _AddHotspots = false;
        }

        private void DrawHotspotHoverRegions(Graphics graphics)
        {
            foreach (Hotspot hotspot in _Hotspots)
            {
                if (hotspot.Hovered)
                {
                    graphics.DrawRectangle(_HotspotPen, hotspot.Region);
                    hotspot.Hovered = false;
                }
            }
        }

        private Bitmap OverlayDamage(BuiltObject builtObject, Bitmap image, Bitmap maskingImage)
        {
            if (builtObject.DamagedComponentCount > 0)
            {
                double num = (double)builtObject.DamagedComponentCount / (double)builtObject.Components.Count;
                int num2 = image.Width * image.Height;
                int damagedPixelCount = (int)((double)num2 * 0.7 * num);
                Random rnd = new Random(builtObject.BuiltObjectID);
                using HatchBrush damageBrush = new HatchBrush(HatchStyle.Cross, Color.FromArgb(160, 160, 160), Color.FromArgb(1, 1, 1));
                return OverlayDamage(image, maskingImage, damageBrush, damagedPixelCount, rnd);
            }
            return image;
        }

        private Bitmap OverlayDamage(Fighter fighter, Bitmap image, Bitmap maskingImage)
        {
            if (fighter.Health < 1f && !fighter.UnderConstruction)
            {
                double num = 1f - fighter.Health;
                int num2 = image.Width * image.Height;
                int damagedPixelCount = (int)((double)num2 * 0.7 * num);
                Random rnd = new Random(fighter.FighterID);
                using HatchBrush damageBrush = new HatchBrush(HatchStyle.Cross, Color.FromArgb(160, 160, 160), Color.FromArgb(1, 1, 1));
                return OverlayDamage(image, maskingImage, damageBrush, damagedPixelCount, rnd);
            }
            return image;
        }

        private Bitmap OverlayDamage(Creature creature, Bitmap image, Bitmap maskingImage)
        {
            if (creature.Damage > 0.0)
            {
                double num = creature.Damage / (double)creature.DamageKillThreshhold;
                int num2 = image.Width * image.Height;
                int damagedPixelCount = (int)((double)num2 * 0.3 * num);
                Random rnd = new Random(creature.CreatureID);
                Color color = Color.FromArgb(1, 1, 1);
                switch (creature.Type)
                {
                    case CreatureType.SilverMist:
                        {
                            float val = 1f - 0.9f * ((float)creature.Damage / (float)creature.DamageKillThreshhold);
                            val = Math.Min(1f, Math.Max(0f, val));
                            return FadeImage(image, val);
                        }
                    case CreatureType.Ardilus:
                        color = Color.FromArgb(96, 48, 8, 20);
                        break;
                    case CreatureType.DesertSpaceSlug:
                        color = Color.FromArgb(96, 160, 56, 0);
                        break;
                    case CreatureType.Kaltor:
                        color = Color.FromArgb(96, 110, 32, 80);
                        break;
                    case CreatureType.RockSpaceSlug:
                        color = Color.FromArgb(96, 64, 32, 36);
                        break;
                }
                using SolidBrush damageBrush = new SolidBrush(color);
                return OverlayDamage(image, maskingImage, damageBrush, damagedPixelCount, rnd);
            }
            return image;
        }

        private Bitmap OverlayDamage(Bitmap image, Bitmap maskingImage, Brush damageBrush, int damagedPixelCount, Random rnd)
        {
            int num = Math.Max(2, image.Width);
            int num2 = Math.Max(2, image.Height);
            int num3 = Math.Max(2, maskingImage.Width);
            int num4 = Math.Max(2, maskingImage.Height);
            Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.DrawImageUnscaled(image, 0, 0);
                List<Rectangle> list = new List<Rectangle>();
                new List<Color>();
                while (damagedPixelCount > 0)
                {
                    int num5 = 0;
                    int num6 = rnd.Next(0, 6);
                    int num7 = rnd.Next(0, bitmap.Width - 1);
                    int num8 = rnd.Next(0, bitmap.Height - 1);
                    switch (num6)
                    {
                        case 0:
                            list.Add(new Rectangle(num7, num8, 3, 3));
                            list.Add(new Rectangle(num7 + 3, num8, 1, 2));
                            num5 = 11;
                            break;
                        case 1:
                            list.Add(new Rectangle(num7, num8, 2, 2));
                            list.Add(new Rectangle(num7 + 1, num8 + 2, 2, 2));
                            num5 = 8;
                            break;
                        case 2:
                            list.Add(new Rectangle(num7, num8, 3, 5));
                            list.Add(new Rectangle(num7 - 1, num8 + 2, 2, 2));
                            num5 = 19;
                            break;
                        case 3:
                            list.Add(new Rectangle(num7, num8, 2, 2));
                            list.Add(new Rectangle(num7 - 1, num8 - 1, 2, 2));
                            num5 = 7;
                            break;
                        case 4:
                            list.Add(new Rectangle(num7, num8, 2, 2));
                            list.Add(new Rectangle(num7 - 1, num8 - 1, 1, 1));
                            list.Add(new Rectangle(num7 + 1, num8 + 2, 1, 1));
                            num5 = 6;
                            break;
                        case 5:
                            list.Add(new Rectangle(num7, num8, 3, 3));
                            list.Add(new Rectangle(num7 - 2, num8, 2, 2));
                            num5 = 13;
                            break;
                    }
                    damagedPixelCount -= num5;
                }
                if (list.Count > 0)
                {
                    graphics.FillRectangles(damageBrush, list.ToArray());
                }
                SetGraphicsQualityToHigh(graphics);
                Rectangle srcRect = new Rectangle(0, 0, num3, num4);
                Rectangle destRect = new Rectangle(0, 0, num, num2);
                using SolidBrush brush = new SolidBrush(Color.Black);
                int num9 = 1;
                graphics.FillRectangle(brush, new Rectangle(0, 0, num, num9));
                graphics.FillRectangle(brush, new Rectangle(0, 0, num9, num2));
                graphics.FillRectangle(brush, new Rectangle(num - num9, 0, num9, num2));
                graphics.FillRectangle(brush, new Rectangle(0, num2 - num9, num, num9));
                graphics.DrawImage(maskingImage, destRect, srcRect, GraphicsUnit.Pixel);
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        private Bitmap OverlayConstructionProgress(BuiltObject builtObject, Bitmap image)
        {
            if (builtObject.UnbuiltComponentCount <= 0)
            {
                return image;
            }
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImageUnscaled(image, 0, 0);
                double num = 1.0 - (double)builtObject.UnbuiltComponentCount / (double)builtObject.Components.Count;
                int num2 = (int)((double)bitmap.Width * num);
                Random random = new Random(num2);
                int i = 0;
                int num3 = Math.Max(3, builtObject.Size / 200);
                int num4 = bitmap.Width - num2;
                int num7;
                for (; i < bitmap.Height - 1; i += num7)
                {
                    int num5 = random.Next(-num3, num3);
                    int num6 = Math.Max(0, Math.Min(bitmap.Width - 1, num4 + num5));
                    num7 = random.Next(3, 6);
                    int val = i + num7;
                    val = Math.Min(val, bitmap.Height - 1);
                    num7 = val - i;
                    graphics.FillRectangle(rect: new Rectangle(0, i, num6, num7), brush: _BlackBrush);
                }
                int j = 0;
                int maxValue = Math.Max(8, builtObject.Size / 80);
                List<Rectangle> list = new List<Rectangle>();
                for (; j < bitmap.Height; j += random.Next(1, 3))
                {
                    int num8 = random.Next(2, maxValue);
                    graphics.DrawLine(_BlackPen, 0, j, bitmap.Width - num2 + num8, j);
                    Rectangle item = new Rectangle(bitmap.Width - num2, j, bitmap.Width - num2 + num8 - (bitmap.Width - num2), 1);
                    list.Add(item);
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
            if (_Picture == null || _Picture.PixelFormat == PixelFormat.Undefined)
            {
                return;
            }
            Bitmap bitmap = null;
            Rectangle rectangle = default(Rectangle);
            int num = base.ClientRectangle.Width - 6;
            int num2 = base.ClientRectangle.Height - 6;
            if (_SystemInfo != null)
            {
                bitmap = _Picture;
                int num3 = (num - bitmap.Width) / 2;
                int num4 = (num2 - bitmap.Height) / 2;
                if (_SystemInfo.SystemStar.Category == HabitatCategoryType.Star)
                {
                    num3 = (int)((double)bitmap.Width * 0.55) * -1;
                }
                rectangle = new Rectangle(num3, num4, bitmap.Width, bitmap.Height);
            }
            else if (_Habitat != null)
            {
                bitmap = _Picture;
                int num5 = (num - bitmap.Width) / 2;
                int num6 = (num2 - bitmap.Height) / 2;
                if (_Habitat.Category == HabitatCategoryType.Star)
                {
                    num5 = (int)((double)bitmap.Width * 0.55) * -1;
                }
                rectangle = new Rectangle(num5, num6, bitmap.Width, bitmap.Height);
            }
            else
            {
                int num7 = 0;
                int num8 = 0;
                if (_BuiltObject != null)
                {
                    bitmap = OverlayDamage(_BuiltObject, _Picture, _MaskImage);
                    bitmap = OverlayConstructionProgress(_BuiltObject, bitmap);
                    double num9 = (double)_PictureSize / (double)_BuiltObjectImages[_BuiltObject.PictureRef].Width;
                    num7 = (int)((double)_BuiltObjectImages[_BuiltObject.PictureRef].Width * num9);
                    num8 = (int)((double)_BuiltObjectImages[_BuiltObject.PictureRef].Height * num9);
                    Bitmap image = new Bitmap(num7, num8, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics2 = Graphics.FromImage(image))
                    {
                        graphics2.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics2.InterpolationMode = InterpolationMode.Bilinear;
                        graphics2.SmoothingMode = SmoothingMode.None;
                        graphics2.DrawImage(bitmap, new Rectangle(0, 0, num7, num8));
                    }
                    bitmap = RotateImage(image, (float)_PictureAngle);
                    num7 = bitmap.Width;
                    num8 = bitmap.Height;
                }
                else if (_Fighter != null)
                {
                    bitmap = OverlayDamage(_Fighter, _Picture, _MaskImage);
                    double num10 = (double)_PictureSize / (double)_FighterImages[_Fighter.PictureRef].Width;
                    num7 = (int)((double)_FighterImages[_Fighter.PictureRef].Width * num10);
                    num8 = (int)((double)_FighterImages[_Fighter.PictureRef].Height * num10);
                    Bitmap image2 = new Bitmap(num7, num8, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics3 = Graphics.FromImage(image2))
                    {
                        graphics3.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics3.InterpolationMode = InterpolationMode.Bilinear;
                        graphics3.SmoothingMode = SmoothingMode.None;
                        graphics3.DrawImage(bitmap, new Rectangle(0, 0, num7, num8));
                    }
                    bitmap = RotateImage(image2, (float)_PictureAngle);
                    num7 = bitmap.Width;
                    num8 = bitmap.Height;
                }
                else if (_Creature != null)
                {
                    double num11 = (double)_PictureSize / (double)_Picture.Width;
                    num7 = (int)((double)_Picture.Width * num11);
                    num8 = (int)((double)_Picture.Height * num11);
                    Bitmap image3 = new Bitmap(num7, num8, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics4 = Graphics.FromImage(image3))
                    {
                        graphics4.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics4.InterpolationMode = InterpolationMode.Bilinear;
                        graphics4.SmoothingMode = SmoothingMode.None;
                        graphics4.DrawImage(_Picture, new Rectangle(0, 0, num7, num8));
                    }
                    bitmap = OverlayDamage(_Creature, image3, _MaskImage);
                    bitmap = RotateImage(bitmap, _Creature.CurrentHeading * -1f);
                    num7 = bitmap.Width;
                    num8 = bitmap.Height;
                }
                else
                {
                    bitmap = FadeImage(_Picture, 0.6f);
                }
                double num12 = 1.0;
                if (num7 <= 0)
                {
                    double num13 = (double)bitmap.Width / (double)bitmap.Height;
                    if (bitmap.Width > bitmap.Height)
                    {
                        num7 = (int)((double)_PictureSize * num13);
                        num8 = _PictureSize;
                    }
                    else
                    {
                        num7 = _PictureSize;
                        num8 = (int)((double)_PictureSize / num13);
                    }
                    if (num8 > num2)
                    {
                        num12 = (double)num2 / (double)num8;
                        if ((double)num7 * num12 > (double)num)
                        {
                            num12 *= (double)num / ((double)num7 * num12);
                        }
                    }
                    else if (num7 > num)
                    {
                        num12 = (double)num / (double)num7;
                        if ((double)num8 * num12 > (double)num2)
                        {
                            num12 *= (double)num2 / ((double)num8 * num12);
                        }
                    }
                    else
                    {
                        num12 = 1.0;
                    }
                }
                int num14 = (num - (int)((double)num7 * num12)) / 2;
                int num15 = (num2 - (int)((double)num8 * num12)) / 2;
                int num16 = (int)((double)num7 * num12);
                rectangle = new Rectangle(num14 + 3, num15 + 3, num16, (int)((double)num8 * num12));
            }
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.DrawImage(bitmap, rectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                DrawPanel(e.Graphics);
            }
            catch (Exception)
            {
                ClearData();
            }
        }

        private static Bitmap CopyBitmap(Bitmap image)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImageUnscaled(image, 0, 0);
            return bitmap;
        }

        private Bitmap PrescaleImage(Bitmap originalBitmap, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width + 1, height + 1, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(originalBitmap, new Rectangle(0, 0, width, height));
            return bitmap;
        }

        private Bitmap FadeImage(Bitmap image, float transparencyLevel)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle destRect = new Rectangle(0, 0, image.Width, image.Height);
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5] { 0f, 0f, 0f, transparencyLevel, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(image, destRect, 0f, 0f, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            return bitmap;
        }

        private static Bitmap PrescaleImageStatic(Bitmap originalBitmap, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width + 1, height + 1, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(originalBitmap, new Rectangle(0, 0, width, height));
            return bitmap;
        }

        private static Bitmap FadeImageStatic(Bitmap image, float transparencyLevel)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle destRect = new Rectangle(0, 0, image.Width, image.Height);
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5] { 0f, 0f, 0f, transparencyLevel, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(image, destRect, 0f, 0f, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            return bitmap;
        }

        private static Bitmap MakeImageSquare(Bitmap image, int size)
        {
            Bitmap bitmap = new Bitmap(size, size, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
            int num = (size - image.Width) / 2;
            int num2 = (size - image.Height) / 2;
            Rectangle destRect = new Rectangle(num, num2, image.Width, image.Height);
            graphics.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
            return bitmap;
        }

        private static Size ResolveImageSize(Bitmap image, int maximumSize)
        {
            int num = 0;
            int num2 = 0;
            if (image.Width > image.Height)
            {
                double num3 = (double)image.Height / (double)image.Width;
                num = maximumSize;
                num2 = (int)((double)maximumSize * num3);
            }
            else
            {
                double num4 = (double)image.Width / (double)image.Height;
                num2 = maximumSize;
                num = (int)((double)maximumSize * num4);
            }
            return new Size(num, num2);
        }

        private static void ClearImageArray(Bitmap[] imageArray)
        {
            if (imageArray == null)
            {
                return;
            }
            for (int i = 0; i < imageArray.Length; i++)
            {
                if (imageArray[i] != null)
                {
                    imageArray[i].Dispose();
                    imageArray[i] = null;
                }
            }
        }

        private static void ClearImages()
        {
            ClearImageArray(TroopImagesInfantry);
            ClearImageArray(TroopImagesFadedInfantry);
            ClearImageArray(TroopImagesArmored);
            ClearImageArray(TroopImagesFadedArmored);
            ClearImageArray(TroopImagesArtillery);
            ClearImageArray(TroopImagesFadedArtillery);
            ClearImageArray(TroopImagesSpecialForces);
            ClearImageArray(TroopImagesFadedSpecialForces);
            ClearImageArray(TroopImagesPirateRaider);
            ClearImageArray(TroopImagesFadedPirateRaider);
            ClearImageArray(ResourceImages);
            ClearImageArray(RaceImages);
            ClearImageArray(BuiltObjectImages);
            ClearImageArray(FighterImages);
            ClearImageArray(FighterImagesFaded);
            ClearImageArray(RuinImages);
            ClearImageArray(HabitatImages);
            ClearImageArray(FacilityImages);
            ClearImageArray(FacilityImagesFaded);
            ClearImageArray(TroopImagesInfantryLarge);
            ClearImageArray(TroopImagesFadedInfantryLarge);
            ClearImageArray(TroopImagesArmoredLarge);
            ClearImageArray(TroopImagesFadedArmoredLarge);
            ClearImageArray(TroopImagesArtilleryLarge);
            ClearImageArray(TroopImagesFadedArtilleryLarge);
            ClearImageArray(TroopImagesSpecialForcesLarge);
            ClearImageArray(TroopImagesFadedSpecialForcesLarge);
            ClearImageArray(TroopImagesPirateRaiderLarge);
            ClearImageArray(TroopImagesFadedPirateRaiderLarge);
            ClearImageArray(ResourceImagesLarge);
            ClearImageArray(RaceImagesLarge);
            ClearImageArray(BuiltObjectImagesLarge);
            ClearImageArray(FighterImagesLarge);
            ClearImageArray(FighterImagesFadedLarge);
            ClearImageArray(RuinImagesLarge);
            ClearImageArray(HabitatImagesLarge);
            ClearImageArray(FacilityImagesLarge);
            ClearImageArray(FacilityImagesFadedLarge);
        }

        public void Kickstart(bool isLargeSize)
        {
            _ContentSizeIsLarge = isLargeSize;
            if (isLargeSize)
            {
                SetContentSizeLarge();
            }
            else
            {
                SetContentSizeNormal();
            }
        }

        public static void InitializeImages(CharacterImageCache characterImageCache, Bitmap[] troopImagesInfantry, Bitmap[] troopImagesArmored, Bitmap[] troopImagesArtillery, Bitmap[] troopImagesSpecialForces, Bitmap[] troopImagesPirateRaider, Bitmap[] resourceImages, RaceImageCache raceImageCache, Bitmap[] builtObjectImages, Bitmap[] fighterImages, Bitmap[] ruinImages, Bitmap[] habitatImages, Bitmap[] facilityImages, Bitmap approvalSmileImage, Bitmap approvalNeutralImage, Bitmap approvalSadImage, Bitmap approvalAngryImage, Bitmap developmentImage, Bitmap colonyImage, Bitmap firepowerImage, Bitmap shipGroupLeadShipImage, Bitmap capitalColonyImage, Bitmap regionalCapitalColonyImage, Bitmap automateImage, Bitmap blockadeImage, Bitmap[] messageImages, Bitmap[] plagueImages)
        {
            int num = 14;
            int num2 = 24;
            int num3 = 18;
            int num4 = 26;
            int num5 = 20;
            int num6 = 34;
            int num7 = 25;
            int num8 = 36;
            ClearImages();
            _CharacterImageCache = characterImageCache;
            _ApprovalSmileImage = approvalSmileImage;
            _ApprovalNeutralImage = approvalNeutralImage;
            _ApprovalSadImage = approvalSadImage;
            _ApprovalAngryImage = approvalAngryImage;
            _DevelopmentImage = developmentImage;
            _ColonyImage = colonyImage;
            _FirepowerImage = firepowerImage;
            _ShipGroupLeadShipImage = shipGroupLeadShipImage;
            _CapitalColonyImage = capitalColonyImage;
            _RegionalCapitalColonyImage = regionalCapitalColonyImage;
            _AutomateImage = automateImage;
            _BlockadeImage = blockadeImage;
            _MessageImages = messageImages;
            if (ruinImages != null)
            {
                if (RuinImages != null)
                {
                    for (int i = 0; i < RuinImages.Length; i++)
                    {
                        if (RuinImages[i] != null)
                        {
                            RuinImages[i].Dispose();
                            RuinImages[i] = null;
                        }
                    }
                }
                RuinImages = new Bitmap[ruinImages.Length];
                int num9 = num3;
                for (int j = 0; j < ruinImages.Length; j++)
                {
                    double num10 = (double)ruinImages[j].Width / (double)ruinImages[j].Height;
                    int num11 = (int)((double)num9 * num10);
                    RuinImages[j] = PrescaleImageStatic(ruinImages[j], num11, num9);
                }
                if (RuinImagesLarge != null)
                {
                    for (int k = 0; k < RuinImagesLarge.Length; k++)
                    {
                        if (RuinImagesLarge[k] != null)
                        {
                            RuinImagesLarge[k].Dispose();
                            RuinImagesLarge[k] = null;
                        }
                    }
                }
                RuinImagesLarge = new Bitmap[ruinImages.Length];
                num9 = num7;
                for (int l = 0; l < ruinImages.Length; l++)
                {
                    double num12 = (double)ruinImages[l].Width / (double)ruinImages[l].Height;
                    int num13 = (int)((double)num9 * num12);
                    RuinImagesLarge[l] = PrescaleImageStatic(ruinImages[l], num13, num9);
                }
            }
            if (facilityImages != null)
            {
                if (FacilityImages != null)
                {
                    for (int m = 0; m < FacilityImages.Length; m++)
                    {
                        if (FacilityImages[m] != null)
                        {
                            FacilityImages[m].Dispose();
                            FacilityImages[m] = null;
                        }
                    }
                }
                if (FacilityImagesFaded != null)
                {
                    for (int n = 0; n < FacilityImagesFaded.Length; n++)
                    {
                        if (FacilityImagesFaded[n] != null)
                        {
                            FacilityImagesFaded[n].Dispose();
                            FacilityImagesFaded[n] = null;
                        }
                    }
                }
                FacilityImages = new Bitmap[facilityImages.Length];
                FacilityImagesFaded = new Bitmap[facilityImages.Length];
                int num14 = num;
                for (int num15 = 0; num15 < facilityImages.Length; num15++)
                {
                    double num16 = (double)facilityImages[num15].Width / (double)facilityImages[num15].Height;
                    int num17 = (int)((double)num14 * num16);
                    FacilityImages[num15] = PrescaleImageStatic(facilityImages[num15], num17, num14);
                }
                for (int num18 = 0; num18 < facilityImages.Length; num18++)
                {
                    double num19 = (double)facilityImages[num18].Width / (double)facilityImages[num18].Height;
                    int num20 = (int)((double)num14 * num19);
                    FacilityImagesFaded[num18] = FadeImageStatic(PrescaleImageStatic(facilityImages[num18], num20, num14), 0.4f);
                }
                if (FacilityImagesLarge != null)
                {
                    for (int num21 = 0; num21 < FacilityImagesLarge.Length; num21++)
                    {
                        if (FacilityImagesLarge[num21] != null)
                        {
                            FacilityImagesLarge[num21].Dispose();
                            FacilityImagesLarge[num21] = null;
                        }
                    }
                }
                if (FacilityImagesFadedLarge != null)
                {
                    for (int num22 = 0; num22 < FacilityImagesFadedLarge.Length; num22++)
                    {
                        if (FacilityImagesFadedLarge[num22] != null)
                        {
                            FacilityImagesFadedLarge[num22].Dispose();
                            FacilityImagesFadedLarge[num22] = null;
                        }
                    }
                }
                FacilityImagesLarge = new Bitmap[facilityImages.Length];
                FacilityImagesFadedLarge = new Bitmap[facilityImages.Length];
                num14 = num5;
                for (int num23 = 0; num23 < facilityImages.Length; num23++)
                {
                    double num24 = (double)facilityImages[num23].Width / (double)facilityImages[num23].Height;
                    int num25 = (int)((double)num14 * num24);
                    FacilityImagesLarge[num23] = PrescaleImageStatic(facilityImages[num23], num25, num14);
                }
                for (int num26 = 0; num26 < facilityImages.Length; num26++)
                {
                    double num27 = (double)facilityImages[num26].Width / (double)facilityImages[num26].Height;
                    int num28 = (int)((double)num14 * num27);
                    FacilityImagesFadedLarge[num26] = FadeImageStatic(PrescaleImageStatic(facilityImages[num26], num28, num14), 0.4f);
                }
            }
            if (troopImagesInfantry != null)
            {
                InitializeTroopImageArray(troopImagesInfantry, ref TroopImagesInfantry, ref TroopImagesFadedInfantry, num);
                InitializeTroopImageArray(troopImagesInfantry, ref TroopImagesInfantryLarge, ref TroopImagesFadedInfantryLarge, num5);
            }
            if (troopImagesArmored != null)
            {
                InitializeTroopImageArray(troopImagesArmored, ref TroopImagesArmored, ref TroopImagesFadedArmored, num);
                InitializeTroopImageArray(troopImagesArmored, ref TroopImagesArmoredLarge, ref TroopImagesFadedArmoredLarge, num5);
            }
            if (troopImagesArtillery != null)
            {
                InitializeTroopImageArray(troopImagesArtillery, ref TroopImagesArtillery, ref TroopImagesFadedArtillery, num);
                InitializeTroopImageArray(troopImagesArtillery, ref TroopImagesArtilleryLarge, ref TroopImagesFadedArtilleryLarge, num5);
            }
            if (troopImagesSpecialForces != null)
            {
                InitializeTroopImageArray(troopImagesSpecialForces, ref TroopImagesSpecialForces, ref TroopImagesFadedSpecialForces, num);
                InitializeTroopImageArray(troopImagesSpecialForces, ref TroopImagesSpecialForcesLarge, ref TroopImagesFadedSpecialForcesLarge, num5);
            }
            if (troopImagesPirateRaider != null)
            {
                InitializeTroopImageArray(troopImagesPirateRaider, ref TroopImagesPirateRaider, ref TroopImagesFadedPirateRaider, num);
                InitializeTroopImageArray(troopImagesPirateRaider, ref TroopImagesPirateRaiderLarge, ref TroopImagesFadedPirateRaiderLarge, num5);
            }
            if (resourceImages != null)
            {
                if (ResourceImages != null)
                {
                    for (int num29 = 0; num29 < ResourceImages.Length; num29++)
                    {
                        if (ResourceImages[num29] != null)
                        {
                            ResourceImages[num29].Dispose();
                            ResourceImages[num29] = null;
                        }
                    }
                }
                ResourceImages = new Bitmap[resourceImages.Length];
                int num30 = (int)((double)num * 1.0);
                for (int num31 = 0; num31 < resourceImages.Length; num31++)
                {
                    int num32 = num;
                    int num33 = (int)((double)num / (double)resourceImages[num31].Height * (double)resourceImages[num31].Width);
                    if (num33 > num30)
                    {
                        double num34 = (double)num30 / (double)num33;
                        num33 = (int)((double)num33 * num34);
                        num32 = (int)((double)num32 * num34);
                    }
                    ResourceImages[num31] = PrescaleImageStatic(resourceImages[num31], num33, num32);
                }
                if (ResourceImagesLarge != null)
                {
                    for (int num35 = 0; num35 < ResourceImagesLarge.Length; num35++)
                    {
                        if (ResourceImagesLarge[num35] != null)
                        {
                            ResourceImagesLarge[num35].Dispose();
                            ResourceImagesLarge[num35] = null;
                        }
                    }
                }
                ResourceImagesLarge = new Bitmap[resourceImages.Length];
                num30 = (int)((double)num5 * 1.0);
                for (int num36 = 0; num36 < resourceImages.Length; num36++)
                {
                    int num37 = num5;
                    int num38 = (int)((double)num5 / (double)resourceImages[num36].Height * (double)resourceImages[num36].Width);
                    if (num38 > num30)
                    {
                        double num39 = (double)num30 / (double)num38;
                        num38 = (int)((double)num38 * num39);
                        num37 = (int)((double)num37 * num39);
                    }
                    ResourceImagesLarge[num36] = PrescaleImageStatic(resourceImages[num36], num38, num37);
                }
            }
            if (plagueImages != null)
            {
                if (PlagueImages != null)
                {
                    for (int num40 = 0; num40 < PlagueImages.Length; num40++)
                    {
                        if (PlagueImages[num40] != null)
                        {
                            PlagueImages[num40].Dispose();
                            PlagueImages[num40] = null;
                        }
                    }
                }
                PlagueImages = new Bitmap[plagueImages.Length];
                int num41 = (int)((double)num * 1.0);
                for (int num42 = 0; num42 < plagueImages.Length; num42++)
                {
                    int num43 = num;
                    int num44 = (int)((double)num / (double)plagueImages[num42].Height * (double)plagueImages[num42].Width);
                    if (num44 > num41)
                    {
                        double num45 = (double)num41 / (double)num44;
                        num44 = (int)((double)num44 * num45);
                        num43 = (int)((double)num43 * num45);
                    }
                    PlagueImages[num42] = PrescaleImageStatic(plagueImages[num42], num44, num43);
                }
                if (PlagueImagesLarge != null)
                {
                    for (int num46 = 0; num46 < PlagueImagesLarge.Length; num46++)
                    {
                        if (PlagueImagesLarge[num46] != null)
                        {
                            PlagueImagesLarge[num46].Dispose();
                            PlagueImagesLarge[num46] = null;
                        }
                    }
                }
                PlagueImagesLarge = new Bitmap[plagueImages.Length];
                num41 = (int)((double)num5 * 1.0);
                for (int num47 = 0; num47 < plagueImages.Length; num47++)
                {
                    int num48 = num5;
                    int num49 = (int)((double)num5 / (double)plagueImages[num47].Height * (double)plagueImages[num47].Width);
                    if (num49 > num41)
                    {
                        double num50 = (double)num41 / (double)num49;
                        num49 = (int)((double)num49 * num50);
                        num48 = (int)((double)num48 * num50);
                    }
                    PlagueImagesLarge[num47] = PrescaleImageStatic(plagueImages[num47], num49, num48);
                }
            }
            if (raceImageCache != null)
            {
                if (RaceImages != null)
                {
                    for (int num51 = 0; num51 < RaceImages.Length; num51++)
                    {
                        if (RaceImages[num51] != null)
                        {
                            RaceImages[num51].Dispose();
                            RaceImages[num51] = null;
                        }
                    }
                }
                RaceImages = new Bitmap[raceImageCache.RaceImagesLength];
                for (int num52 = 0; num52 < RaceImages.Length; num52++)
                {
                    RaceImages[num52] = PrescaleImageStatic(raceImageCache.GetRaceImage(num52), num, num);
                }
                if (RaceImagesLarge != null)
                {
                    for (int num53 = 0; num53 < RaceImagesLarge.Length; num53++)
                    {
                        if (RaceImagesLarge[num53] != null)
                        {
                            RaceImagesLarge[num53].Dispose();
                            RaceImagesLarge[num53] = null;
                        }
                    }
                }
                RaceImagesLarge = new Bitmap[raceImageCache.RaceImagesLength];
                for (int num54 = 0; num54 < RaceImagesLarge.Length; num54++)
                {
                    RaceImagesLarge[num54] = PrescaleImageStatic(raceImageCache.GetRaceImage(num54), num5, num5);
                }
            }
            if (builtObjectImages != null)
            {
                if (BuiltObjectImages != null)
                {
                    for (int num55 = 0; num55 < BuiltObjectImages.Length; num55++)
                    {
                        if (BuiltObjectImages[num55] != null)
                        {
                            BuiltObjectImages[num55].Dispose();
                            BuiltObjectImages[num55] = null;
                        }
                    }
                }
                BuiltObjectImages = new Bitmap[builtObjectImages.Length];
                for (int num56 = 0; num56 < builtObjectImages.Length; num56++)
                {
                    Bitmap bitmap = CopyBitmap(builtObjectImages[num56]);
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    bitmap.MakeTransparent(Color.Black);
                    BuiltObjectImages[num56] = PrescaleImageStatic(bitmap, num2, num2);
                    bitmap.Dispose();
                }
                if (BuiltObjectImagesLarge != null)
                {
                    for (int num57 = 0; num57 < BuiltObjectImagesLarge.Length; num57++)
                    {
                        if (BuiltObjectImagesLarge[num57] != null)
                        {
                            BuiltObjectImagesLarge[num57].Dispose();
                            BuiltObjectImagesLarge[num57] = null;
                        }
                    }
                }
                BuiltObjectImagesLarge = new Bitmap[builtObjectImages.Length];
                for (int num58 = 0; num58 < builtObjectImages.Length; num58++)
                {
                    Bitmap bitmap2 = CopyBitmap(builtObjectImages[num58]);
                    bitmap2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    bitmap2.MakeTransparent(Color.Black);
                    BuiltObjectImagesLarge[num58] = PrescaleImageStatic(bitmap2, num6, num6);
                    bitmap2.Dispose();
                }
            }
            if (fighterImages != null)
            {
                if (FighterImages != null)
                {
                    for (int num59 = 0; num59 < FighterImages.Length; num59++)
                    {
                        if (FighterImages[num59] != null)
                        {
                            FighterImages[num59].Dispose();
                            FighterImages[num59] = null;
                        }
                    }
                }
                if (FighterImagesFaded != null)
                {
                    for (int num60 = 0; num60 < FighterImagesFaded.Length; num60++)
                    {
                        if (FighterImagesFaded[num60] != null)
                        {
                            FighterImagesFaded[num60].Dispose();
                            FighterImagesFaded[num60] = null;
                        }
                    }
                }
                FighterImages = new Bitmap[fighterImages.Length];
                FighterImagesFaded = new Bitmap[fighterImages.Length];
                for (int num61 = 0; num61 < fighterImages.Length; num61++)
                {
                    Bitmap bitmap3 = CopyBitmap(fighterImages[num61]);
                    bitmap3.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    bitmap3.MakeTransparent(Color.Black);
                    FighterImages[num61] = PrescaleImageStatic(bitmap3, num, num);
                    FighterImagesFaded[num61] = FadeImageStatic(FighterImages[num61], 0.4f);
                    bitmap3.Dispose();
                }
                if (FighterImagesLarge != null)
                {
                    for (int num62 = 0; num62 < FighterImagesLarge.Length; num62++)
                    {
                        if (FighterImagesLarge[num62] != null)
                        {
                            FighterImagesLarge[num62].Dispose();
                            FighterImagesLarge[num62] = null;
                        }
                    }
                }
                if (FighterImagesFadedLarge != null)
                {
                    for (int num63 = 0; num63 < FighterImagesFadedLarge.Length; num63++)
                    {
                        if (FighterImagesFadedLarge[num63] != null)
                        {
                            FighterImagesFadedLarge[num63].Dispose();
                            FighterImagesFadedLarge[num63] = null;
                        }
                    }
                }
                FighterImagesLarge = new Bitmap[fighterImages.Length];
                FighterImagesFadedLarge = new Bitmap[fighterImages.Length];
                for (int num64 = 0; num64 < fighterImages.Length; num64++)
                {
                    Bitmap bitmap4 = CopyBitmap(fighterImages[num64]);
                    bitmap4.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    bitmap4.MakeTransparent(Color.Black);
                    FighterImagesLarge[num64] = PrescaleImageStatic(bitmap4, num5, num5);
                    FighterImagesFadedLarge[num64] = FadeImageStatic(FighterImagesLarge[num64], 0.4f);
                    bitmap4.Dispose();
                }
            }
            if (habitatImages == null)
            {
                return;
            }
            if (HabitatImages != null)
            {
                for (int num65 = 0; num65 < HabitatImages.Length; num65++)
                {
                    if (HabitatImages[num65] != null)
                    {
                        HabitatImages[num65].Dispose();
                        HabitatImages[num65] = null;
                    }
                }
            }
            HabitatImages = new Bitmap[habitatImages.Length];
            for (int num66 = 0; num66 < habitatImages.Length; num66++)
            {
                HabitatImages[num66] = PrescaleImageStatic(habitatImages[num66], num4, num4);
            }
            if (HabitatImagesLarge != null)
            {
                for (int num67 = 0; num67 < HabitatImagesLarge.Length; num67++)
                {
                    if (HabitatImagesLarge[num67] != null)
                    {
                        HabitatImagesLarge[num67].Dispose();
                        HabitatImagesLarge[num67] = null;
                    }
                }
            }
            HabitatImagesLarge = new Bitmap[habitatImages.Length];
            for (int num68 = 0; num68 < habitatImages.Length; num68++)
            {
                HabitatImagesLarge[num68] = PrescaleImageStatic(habitatImages[num68], num8, num8);
            }
        }

        private static void InitializeTroopImageArray(Bitmap[] sourceImages, ref Bitmap[] troopImages, ref Bitmap[] troopImagesFaded, int troopMaximumSize)
        {
            if (troopImages != null)
            {
                for (int i = 0; i < troopImages.Length; i++)
                {
                    if (troopImages[i] != null)
                    {
                        troopImages[i].Dispose();
                        troopImages[i] = null;
                    }
                }
            }
            if (troopImagesFaded != null)
            {
                for (int j = 0; j < troopImagesFaded.Length; j++)
                {
                    if (troopImagesFaded[j] != null)
                    {
                        troopImagesFaded[j].Dispose();
                        troopImagesFaded[j] = null;
                    }
                }
            }
            troopImages = new Bitmap[sourceImages.Length];
            troopImagesFaded = new Bitmap[troopImages.Length];
            for (int k = 0; k < troopImages.Length; k++)
            {
                Size size = ResolveImageSize(sourceImages[k], troopMaximumSize);
                Bitmap bitmap = PrescaleImageStatic(sourceImages[k], size.Width, size.Height);
                Bitmap bitmap2 = bitmap;
                bitmap = MakeImageSquare(bitmap, troopMaximumSize);
                bitmap2.Dispose();
                troopImages[k] = bitmap;
            }
            for (int l = 0; l < troopImagesFaded.Length; l++)
            {
                Size size2 = ResolveImageSize(sourceImages[l], troopMaximumSize);
                Bitmap bitmap3 = PrescaleImageStatic(sourceImages[l], size2.Width, size2.Height);
                Bitmap bitmap4 = bitmap3;
                bitmap3 = MakeImageSquare(bitmap3, troopMaximumSize);
                bitmap4.Dispose();
                bitmap4 = bitmap3;
                bitmap3 = FadeImageStatic(bitmap3, 0.4f);
                bitmap4.Dispose();
                troopImagesFaded[l] = bitmap3;
            }
        }

        public void RepointImageInstances()
        {
            if (_ContentSizeIsLarge)
            {
                _TroopImagesInfantry = TroopImagesInfantryLarge;
                _TroopImagesFadedInfantry = TroopImagesFadedInfantryLarge;
                _TroopImagesArmored = TroopImagesArmoredLarge;
                _TroopImagesFadedArmored = TroopImagesFadedArmoredLarge;
                _TroopImagesArtillery = TroopImagesArtilleryLarge;
                _TroopImagesFadedArtillery = TroopImagesFadedArtilleryLarge;
                _TroopImagesSpecialForces = TroopImagesSpecialForcesLarge;
                _TroopImagesFadedSpecialForces = TroopImagesFadedSpecialForcesLarge;
                _TroopImagesPirateRaider = TroopImagesPirateRaiderLarge;
                _TroopImagesFadedPirateRaider = TroopImagesFadedPirateRaiderLarge;
                _ResourceImages = ResourceImagesLarge;
                _RaceImages = RaceImagesLarge;
                _BuiltObjectImages = BuiltObjectImagesLarge;
                _FighterImages = FighterImagesLarge;
                _FighterImagesFaded = FighterImagesFadedLarge;
                _RuinImages = RuinImagesLarge;
                _FacilityImages = FacilityImagesLarge;
                _FacilityImagesFaded = FacilityImagesFadedLarge;
                _HabitatImages = HabitatImagesLarge;
                _PlagueImages = PlagueImagesLarge;
            }
            else
            {
                _TroopImagesInfantry = TroopImagesInfantry;
                _TroopImagesFadedInfantry = TroopImagesFadedInfantry;
                _TroopImagesArmored = TroopImagesArmored;
                _TroopImagesFadedArmored = TroopImagesFadedArmored;
                _TroopImagesArtillery = TroopImagesArtillery;
                _TroopImagesFadedArtillery = TroopImagesFadedArtillery;
                _TroopImagesSpecialForces = TroopImagesSpecialForces;
                _TroopImagesFadedSpecialForces = TroopImagesFadedSpecialForces;
                _TroopImagesPirateRaider = TroopImagesPirateRaider;
                _TroopImagesFadedPirateRaider = TroopImagesFadedPirateRaider;
                _ResourceImages = ResourceImages;
                _RaceImages = RaceImages;
                _BuiltObjectImages = BuiltObjectImages;
                _FighterImages = FighterImages;
                _FighterImagesFaded = FighterImagesFaded;
                _RuinImages = RuinImages;
                _FacilityImages = FacilityImages;
                _FacilityImagesFaded = FacilityImagesFaded;
                _HabitatImages = HabitatImages;
                _PlagueImages = PlagueImages;
            }
        }

        public void SetContentSizeNormal()
        {
            _ImageSize = 14;
            _ImageScaleSize = 24;
            _HabitatImageSize = 26;
            _TinyFont = _TinyFontNormalSize;
            _NormalFont = _NormalFontNormalSize;
            _NormalFontBold = _NormalBoldFontNormalSize;
            _TitleFont = _TinyFontNormalSize;
            _RowHeight = 15;
            _FlagSizeSmall = new Size(30, 18);
            _FlagSizeSystem = new Size(20, 12);
            _MinPictureSize = 60;
            _MaxPictureSize = 200;
            _RuinImageHeight = 18;
            _MaxGraphTextWidth = 200;
            _PopulationTextWidth = 75;
            _PopulationAmountWidth = 48;
            _LabelWidth = 65;
            _LabelWidthHabitat = 70;
            _Height2 = 2;
            _Height3 = 3;
            _Height4 = 4;
            _Height5 = 5;
            _Height6 = 6;
            _Height8 = 8;
            _Height10 = 10;
            _ColonySummaryDetailWidth = 20;
            RepointImageInstances();
        }

        public void SetContentSizeLarge()
        {
            _ImageSize = 20;
            _ImageScaleSize = 34;
            _HabitatImageSize = 36;
            _TinyFont = _TinyFontLargeSize;
            _NormalFont = _NormalFontLargeSize;
            _NormalFontBold = _NormalBoldFontLargeSize;
            _TitleFont = _TinyFontLargeSize;
            _RowHeight = 21;
            _FlagSizeSmall = new Size(42, 25);
            _FlagSizeSystem = new Size(28, 17);
            _MinPictureSize = 84;
            _MaxPictureSize = 280;
            _RuinImageHeight = 25;
            _MaxGraphTextWidth = 280;
            _PopulationTextWidth = 105;
            _PopulationAmountWidth = 67;
            _LabelWidth = 91;
            _LabelWidthHabitat = 98;
            _Height2 = 3;
            _Height3 = 4;
            _Height4 = 6;
            _Height5 = 7;
            _Height6 = 8;
            _Height8 = 11;
            _Height10 = 14;
            _ColonySummaryDetailWidth = 28;
            RepointImageInstances();
        }

        public void DrawBarGraph(string description, int descriptionWidth, int maximumValue, int currentValue, int height, int overallWidth, Color fillColorStart, Color fillColorEnd, Color backgroundColor, Graphics graphics, Point location)
        {
            DrawBarGraph(description, descriptionWidth, maximumValue, currentValue, height, overallWidth, fillColorStart, fillColorEnd, Color.Empty, Color.Empty, backgroundColor, graphics, location, string.Empty);
        }

        public void DrawBarGraph(string description, int descriptionWidth, int maximumValue, int currentValue, int height, int overallWidth, Color fillColorStart, Color fillColorEnd, Color backgroundColor, Graphics graphics, Point location, string suffixData)
        {
            DrawBarGraph(description, descriptionWidth, maximumValue, currentValue, height, overallWidth, fillColorStart, fillColorEnd, Color.Empty, Color.Empty, backgroundColor, graphics, location, suffixData);
        }

        public void DrawBarGraph(string description, int descriptionWidth, int maximumValue, int currentValue, int height, int overallWidth, Color fillColorStart, Color fillColorEnd, Color alternateFillColorStart, Color alternateFillColorEnd, Color backgroundColor, Graphics graphics, Point location)
        {
            DrawBarGraph(description, descriptionWidth, maximumValue, currentValue, height, overallWidth, fillColorStart, fillColorEnd, backgroundColor, graphics, location, string.Empty);
        }

        public void DrawBarGraph(string description, int descriptionWidth, int maximumValue, int currentValue, int height, int overallWidth, Color fillColorStart, Color fillColorEnd, Color alternateFillColorStart, Color alternateFillColorEnd, Color backgroundColor, Graphics graphics, Point location, string suffixData)
        {
            Color color = fillColorStart;
            Color color2 = fillColorEnd;
            if (alternateFillColorStart != Color.Empty)
            {
                color = UpdateColor(fillColorStart, alternateFillColorStart);
            }
            if (alternateFillColorEnd != Color.Empty)
            {
                color2 = UpdateColor(fillColorEnd, alternateFillColorEnd);
            }
            int num = (int)graphics.MeasureString("99999", _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            num += 5;
            int num2 = (int)graphics.MeasureString(description, _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location2 = new Point(location.X + (descriptionWidth - num2), location.Y - 2);
            descriptionWidth += 10;
            Point point = new Point(location.X + descriptionWidth, location.Y);
            int num3 = overallWidth - (descriptionWidth + num);
            int num4 = (int)((double)currentValue / (double)maximumValue * (double)num3);
            if (num4 > num3)
            {
                num4 = num3;
            }
            string s = currentValue + suffixData;
            string text = maximumValue.ToString();
            if (description == TextResolver.GetText("Super Laser") || description == TextResolver.GetText("Status"))
            {
                text = text + " " + TextResolver.GetText("seconds abbreviation");
                s = suffixData;
            }
            int num5 = (int)graphics.MeasureString(s, _NormalFont).Width;
            Point point2 = new Point(point.X + (num3 - num5) / 2, point.Y);
            Point location3 = new Point(point.X + num3 + 5, point.Y);
            Rectangle rect = new Rectangle(point.X, point.Y, num3, height);
            Rectangle rect2 = new Rectangle(point.X, point.Y, num4, height);
            Rectangle rect3 = new Rectangle(point.X - 1, point.Y, num4 + 2, height);
            LinearGradientBrush linearGradientBrush = null;
            if (rect2.Width > 0)
            {
                linearGradientBrush = new LinearGradientBrush(rect3, color, color2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            }
            SolidBrush solidBrush = new SolidBrush(backgroundColor);
            DrawStringWithDropShadow(graphics, description, _NormalFontBold, location2);
            graphics.FillRectangle(solidBrush, rect);
            if (rect2.Width > 0)
            {
                graphics.FillRectangle(linearGradientBrush, rect2);
            }
            graphics.DrawString(s, _NormalFont, _WhiteBrush, new PointF(point2.X, point2.Y));
            DrawStringWithDropShadow(graphics, text, _NormalFont, location3);
            linearGradientBrush?.Dispose();
            solidBrush.Dispose();
        }

        private void DrawFacilities(int labelWidth, Habitat habitat, PlanetaryFacilityList facilities, Graphics graphics, Point location, int overallWidth)
        {
            int num = (int)graphics.MeasureString(TextResolver.GetText("Facilities"), _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location2 = new Point(location.X + (labelWidth - num), location.Y - 2);
            labelWidth += 10;
            DrawStringWithDropShadow(graphics, TextResolver.GetText("Facilities"), _NormalFontBold, location2);
            int num2 = labelWidth;
            Color color = Color.FromArgb(190, 24, 24, 32);
            SolidBrush brush = new SolidBrush(color);
            Rectangle rect = new Rectangle(location.X + labelWidth, location.Y, overallWidth - labelWidth, _ImageSize);
            graphics.FillRectangle(brush, rect);
            if (facilities == null || facilities.Count == 0)
            {
                DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", _NormalFont, new Point(location.X + labelWidth, location.Y));
                return;
            }
            for (int i = 0; i < facilities.Count; i++)
            {
                PlanetaryFacility planetaryFacility = facilities[i];
                if (planetaryFacility == null)
                {
                    continue;
                }
                Point point = new Point(location.X + num2, location.Y);
                Bitmap bitmap = _FacilityImages[planetaryFacility.PictureRef];
                string text = planetaryFacility.Name;
                if (planetaryFacility.ConstructionProgress < 1f)
                {
                    bitmap = _FacilityImagesFaded[planetaryFacility.PictureRef];
                    string text2 = text;
                    text = text2 + " (" + planetaryFacility.ConstructionProgress.ToString("0%") + " " + TextResolver.GetText("Complete").ToLower(CultureInfo.InvariantCulture) + ")";
                }
                switch (planetaryFacility.Type)
                {
                    case PlanetaryFacilityType.PirateBase:
                    case PlanetaryFacilityType.PirateFortress:
                    case PlanetaryFacilityType.PirateCriminalNetwork:
                        {
                            PirateColonyControl byFacilityControl = habitat.GetPirateControl().GetByFacilityControl();
                            if (byFacilityControl != null && byFacilityControl.HasFacilityControl)
                            {
                                Empire empireById = _Galaxy.GetEmpireById(byFacilityControl.EmpireId);
                                if (empireById != null)
                                {
                                    text = text + " (" + empireById.Name + ")";
                                }
                            }
                            break;
                        }
                }
                text = text + " (" + TextResolver.GetText("click for details") + ")";
                graphics.DrawImageUnscaled(bitmap, point);
                AddHotspot(new Rectangle(point.X, point.Y, bitmap.Width, bitmap.Height), planetaryFacility, text);
                num2 += bitmap.Width + 2;
            }
        }

        private void DrawTroopsAgents(int labelWidth, TroopList troops, TroopList troopsRecruiting, TroopList troopsInvading, CharacterList characters, CharacterList invadingCharacters, Graphics graphics, Point location, int overallWidth, int offsetX)
        {
            DrawTroopsAgents(labelWidth, troops, troopsRecruiting, troopsInvading, characters, invadingCharacters, graphics, location, overallWidth, offsetX, string.Empty);
        }

        public void DrawTroopsAgents(int labelWidth, TroopList troops, TroopList troopsRecruiting, TroopList troopsInvading, CharacterList characters, CharacterList invadingCharacters, Graphics graphics, Point location, int overallWidth, int offsetX, string prefix)
        {
            int num = (int)graphics.MeasureString(TextResolver.GetText("Troops"), _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location2 = new Point(location.X + (labelWidth - num), location.Y - 2);
            labelWidth += 10;
            int num2 = 0;
            if (!string.IsNullOrEmpty(prefix))
            {
                num2 = 2 + (int)graphics.MeasureString(prefix, _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                DrawStringWithDropShadow(location: new Point(location.X + labelWidth, location.Y), graphics: graphics, text: prefix, font: _NormalFont);
            }
            labelWidth += num2;
            int num3 = overallWidth - labelWidth;
            if (troops == null)
            {
                troops = new TroopList();
            }
            if (troopsRecruiting == null)
            {
                troopsRecruiting = new TroopList();
            }
            if (troopsInvading == null)
            {
                troopsInvading = new TroopList();
            }
            if (characters == null)
            {
                characters = new CharacterList();
            }
            if (invadingCharacters == null)
            {
                invadingCharacters = new CharacterList();
            }
            int num4 = troops.Count * _ImageSize;
            int num5 = troopsRecruiting.Count * _ImageSize;
            int num6 = troopsInvading.Count * _ImageSize;
            int num7 = characters.Count * _ImageSize;
            int num8 = invadingCharacters.Count * _ImageSize;
            int num9 = _ImageSize / 2;
            int num10 = num7 + num9 + num5 + num9 + num4 + num9 + num6 + num9 + num8;
            int num11 = _ImageSize;
            if (num10 > num3)
            {
                int num12 = num10 - num9 * 3;
                num11 = (int)(((double)num3 - (double)num9 * 3.0) / (double)num12 * (double)_ImageSize);
                if (num11 > _ImageSize)
                {
                    num11 = _ImageSize;
                }
                if (num11 <= 0)
                {
                    num11 = 1;
                }
            }
            DrawStringWithDropShadow(graphics, TextResolver.GetText("Troops"), _NormalFontBold, location2);
            int num13 = labelWidth + offsetX;
            Color color = Color.FromArgb(190, 24, 24, 32);
            SolidBrush solidBrush = new SolidBrush(color);
            Rectangle rect = new Rectangle(location.X + labelWidth + offsetX, location.Y, overallWidth - labelWidth, _ImageSize);
            graphics.FillRectangle(solidBrush, rect);
            if (troopsInvading.Count == 0 && troops.Count == 0 && troopsRecruiting.Count == 0 && characters.Count == 0 && invadingCharacters.Count == 0)
            {
                DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", _NormalFont, new Point(location.X + labelWidth, location.Y));
            }
            if (characters.Count > 0)
            {
                for (int i = 0; i < characters.Count; i++)
                {
                    Character character = characters[i];
                    if (character == null)
                    {
                        continue;
                    }
                    Point point = new Point(location.X + num13, location.Y);
                    Bitmap bitmap = _CharacterImageCache.ObtainCharacterImageVerySmall(character);
                    if (bitmap != null && bitmap.PixelFormat != 0)
                    {
                        graphics.DrawImageUnscaled(bitmap, point);
                        if (character.Empire == _Galaxy.PlayerEmpire)
                        {
                            string text = characters[i].Name + " (" + Galaxy.ResolveDescription(character.Role) + ", " + character.Empire.Name + ")";
                            text = text + "   (" + TextResolver.GetText("click for details") + ")";
                            AddHotspot(new Rectangle(point.X, point.Y, num11, bitmap.Height), character, text);
                        }
                        else if (character.Empire != null)
                        {
                            AddHotspot(new Rectangle(point.X, point.Y, num11, bitmap.Height), character, character.Name + " (" + Galaxy.ResolveDescription(character.Role) + ", " + character.Empire.Name + ")");
                        }
                        else
                        {
                            AddHotspot(new Rectangle(point.X, point.Y, num11, bitmap.Height), null, character.Name + " (" + Galaxy.ResolveDescription(character.Role) + ", " + TextResolver.GetText("No Empire") + ")");
                        }
                        num13 += num11;
                    }
                }
                num13 += num9;
            }
            if (troopsRecruiting.Count > 0)
            {
                int num14 = num13;
                for (int j = 0; j < troopsRecruiting.Count; j++)
                {
                    Troop troop = troopsRecruiting[j];
                    if (troop != null && troop.Garrisoned)
                    {
                        Point point2 = new Point(location.X + num13, location.Y);
                        Bitmap bitmap2 = _TroopImagesInfantry[troop.PictureRef];
                        switch (troop.Type)
                        {
                            case TroopType.Infantry:
                                bitmap2 = _TroopImagesInfantry[troop.PictureRef];
                                break;
                            case TroopType.Armored:
                                bitmap2 = _TroopImagesArmored[troop.PictureRef];
                                break;
                            case TroopType.Artillery:
                                bitmap2 = _TroopImagesArtillery[troop.PictureRef];
                                break;
                            case TroopType.SpecialForces:
                                bitmap2 = _TroopImagesSpecialForces[troop.PictureRef];
                                break;
                            case TroopType.PirateRaider:
                                bitmap2 = _TroopImagesPirateRaider[troop.PictureRef];
                                break;
                        }
                        using SolidBrush brush = new SolidBrush(Color.FromArgb(0, 128, 0));
                        graphics.FillRectangle(brush, point2.X, point2.Y, bitmap2.Width, bitmap2.Height);
                    }
                    num13 += num11;
                }
                num13 = num14;
            }
            if (troopsRecruiting.Count > 0)
            {
                for (int k = 0; k < troopsRecruiting.Count; k++)
                {
                    Troop troop2 = troopsRecruiting[k];
                    if (troop2 == null)
                    {
                        continue;
                    }
                    Point point3 = new Point(location.X + num13, location.Y);
                    Bitmap bitmap3 = _TroopImagesFadedInfantry[troop2.PictureRef];
                    switch (troop2.Type)
                    {
                        case TroopType.Infantry:
                            bitmap3 = _TroopImagesFadedInfantry[troop2.PictureRef];
                            break;
                        case TroopType.Armored:
                            bitmap3 = _TroopImagesFadedArmored[troop2.PictureRef];
                            break;
                        case TroopType.Artillery:
                            bitmap3 = _TroopImagesFadedArtillery[troop2.PictureRef];
                            break;
                        case TroopType.SpecialForces:
                            bitmap3 = _TroopImagesFadedSpecialForces[troop2.PictureRef];
                            break;
                        case TroopType.PirateRaider:
                            bitmap3 = _TroopImagesFadedPirateRaider[troop2.PictureRef];
                            break;
                    }
                    graphics.DrawImageUnscaled(bitmap3, point3);
                    if (troop2.Empire == _Galaxy.PlayerEmpire)
                    {
                        string text2 = TextResolver.GetText("Recruiting") + " " + troop2.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop2);
                        text2 = text2 + ", " + troop2.Empire.Name;
                        if (troop2.Garrisoned)
                        {
                            text2 = text2 + ", " + TextResolver.GetText("Garrisoned");
                        }
                        text2 += ")";
                        AddHotspot(new Rectangle(point3.X, point3.Y, num11, bitmap3.Height), troop2, text2);
                    }
                    else
                    {
                        AddHotspot(new Rectangle(point3.X, point3.Y, num11, bitmap3.Height), null, TextResolver.GetText("Recruiting") + " " + troop2.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop2) + ", " + troop2.Empire.Name + ")");
                    }
                    num13 += num11;
                }
            }
            if (troops.Count > 0)
            {
                int num15 = num13;
                for (int l = 0; l < troops.Count; l++)
                {
                    Troop troop3 = troops[l];
                    if (troop3 != null && troop3.Garrisoned)
                    {
                        Point point4 = new Point(location.X + num13, location.Y);
                        Bitmap bitmap4 = _TroopImagesInfantry[troop3.PictureRef];
                        switch (troop3.Type)
                        {
                            case TroopType.Infantry:
                                bitmap4 = _TroopImagesInfantry[troop3.PictureRef];
                                break;
                            case TroopType.Armored:
                                bitmap4 = _TroopImagesArmored[troop3.PictureRef];
                                break;
                            case TroopType.Artillery:
                                bitmap4 = _TroopImagesArtillery[troop3.PictureRef];
                                break;
                            case TroopType.SpecialForces:
                                bitmap4 = _TroopImagesSpecialForces[troop3.PictureRef];
                                break;
                            case TroopType.PirateRaider:
                                bitmap4 = _TroopImagesPirateRaider[troop3.PictureRef];
                                break;
                        }
                        using SolidBrush brush2 = new SolidBrush(Color.FromArgb(0, 128, 0));
                        graphics.FillRectangle(brush2, point4.X, point4.Y, bitmap4.Width, bitmap4.Height);
                    }
                    num13 += num11;
                }
                num13 = num15;
            }
            if (troops.Count > 0)
            {
                for (int m = 0; m < troops.Count; m++)
                {
                    Troop troop4 = troops[m];
                    if (troop4 == null)
                    {
                        continue;
                    }
                    Point point5 = new Point(location.X + num13, location.Y);
                    Bitmap bitmap5 = _TroopImagesInfantry[troop4.PictureRef];
                    switch (troop4.Type)
                    {
                        case TroopType.Infantry:
                            bitmap5 = _TroopImagesInfantry[troop4.PictureRef];
                            break;
                        case TroopType.Armored:
                            bitmap5 = _TroopImagesArmored[troop4.PictureRef];
                            break;
                        case TroopType.Artillery:
                            bitmap5 = _TroopImagesArtillery[troop4.PictureRef];
                            break;
                        case TroopType.SpecialForces:
                            bitmap5 = _TroopImagesSpecialForces[troop4.PictureRef];
                            break;
                        case TroopType.PirateRaider:
                            bitmap5 = _TroopImagesPirateRaider[troop4.PictureRef];
                            break;
                    }
                    graphics.DrawImageUnscaled(bitmap5, point5);
                    if (troop4.Empire == _Galaxy.PlayerEmpire)
                    {
                        string text3 = troop4.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop4);
                        text3 = text3 + ", " + troop4.Empire.Name;
                        if (troop4.Garrisoned)
                        {
                            text3 = text3 + ", " + TextResolver.GetText("Garrisoned");
                        }
                        text3 += ")";
                        AddHotspot(new Rectangle(point5.X, point5.Y, num11, bitmap5.Height), troop4, text3);
                    }
                    else if (troop4.Empire != null)
                    {
                        AddHotspot(new Rectangle(point5.X, point5.Y, num11, bitmap5.Height), null, troop4.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop4) + ", " + troop4.Empire.Name + ")");
                    }
                    else
                    {
                        AddHotspot(new Rectangle(point5.X, point5.Y, num11, bitmap5.Height), null, troop4.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop4) + ", " + TextResolver.GetText("No Empire") + ")");
                    }
                    num13 += num11;
                }
                num13 += num9;
            }
            if (troopsInvading.Count > 0 || invadingCharacters.Count > 0)
            {
                using SolidBrush brush3 = new SolidBrush(Color.Red);
                int num16 = invadingCharacters.Count * num11 + troopsInvading.Count * num11 + 6;
                if (troopsInvading.Count > 0 && invadingCharacters.Count > 0)
                {
                    num16 += num9;
                }
                Rectangle rect2 = new Rectangle(location.X + num13, location.Y, num16, _ImageSize);
                graphics.FillRectangle(brush3, rect2);
            }
            if (troopsInvading.Count > 0)
            {
                for (int n = 0; n < troopsInvading.Count; n++)
                {
                    Troop troop5 = troopsInvading[n];
                    if (troop5 == null)
                    {
                        continue;
                    }
                    Point point6 = new Point(location.X + num13, location.Y);
                    Bitmap bitmap6 = _TroopImagesInfantry[troop5.PictureRef];
                    switch (troop5.Type)
                    {
                        case TroopType.Infantry:
                            bitmap6 = _TroopImagesInfantry[troop5.PictureRef];
                            break;
                        case TroopType.Armored:
                            bitmap6 = _TroopImagesArmored[troop5.PictureRef];
                            break;
                        case TroopType.Artillery:
                            bitmap6 = _TroopImagesArtillery[troop5.PictureRef];
                            break;
                        case TroopType.SpecialForces:
                            bitmap6 = _TroopImagesSpecialForces[troop5.PictureRef];
                            break;
                        case TroopType.PirateRaider:
                            bitmap6 = _TroopImagesPirateRaider[troop5.PictureRef];
                            break;
                    }
                    graphics.DrawImageUnscaled(bitmap6, point6);
                    if (troop5.Empire == _Galaxy.PlayerEmpire)
                    {
                        string text4 = TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " " + troop5.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop5);
                        text4 = text4 + ", " + troop5.Empire.Name + ")";
                        AddHotspot(new Rectangle(point6.X, point6.Y, num11, bitmap6.Height), troop5, text4);
                    }
                    else
                    {
                        string text5 = TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " " + troop5.Name + " (" + Galaxy.ResolveTroopStrengthDescription(troop5);
                        if (troop5.Empire != null)
                        {
                            text5 = text5 + ", " + troop5.Empire.Name;
                        }
                        text5 += ")";
                        AddHotspot(new Rectangle(point6.X, point6.Y, num11, bitmap6.Height), null, text5);
                    }
                    num13 += num11;
                }
                num13 += num9;
            }
            if (invadingCharacters.Count > 0)
            {
                for (int num17 = 0; num17 < invadingCharacters.Count; num17++)
                {
                    Character character2 = invadingCharacters[num17];
                    if (character2 == null)
                    {
                        continue;
                    }
                    Point point7 = new Point(location.X + num13, location.Y);
                    Bitmap bitmap7 = _CharacterImageCache.ObtainCharacterImageVerySmall(character2);
                    if (bitmap7 == null || bitmap7.PixelFormat == PixelFormat.Undefined)
                    {
                        continue;
                    }
                    graphics.DrawImageUnscaled(bitmap7, point7);
                    if (character2.Empire == _Galaxy.PlayerEmpire)
                    {
                        AddHotspot(new Rectangle(point7.X, point7.Y, num11, bitmap7.Height), character2, TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " " + character2.Name + " (" + character2.Empire.Name + ")");
                    }
                    else
                    {
                        string text6 = string.Empty;
                        if (character2.Empire != null)
                        {
                            text6 = character2.Empire.Name;
                        }
                        AddHotspot(new Rectangle(point7.X, point7.Y, num11, bitmap7.Height), null, TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " " + character2.Name + " (" + text6 + ")");
                    }
                    num13 += num11;
                }
                num13 += num9;
            }
            solidBrush.Dispose();
        }

        public void DrawFighters(int labelWidth, FighterList fighters, Graphics graphics, Point location, int overallWidth)
        {
            int num = (int)graphics.MeasureString(TextResolver.GetText("Fighters"), _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location2 = new Point(location.X + (labelWidth - num), location.Y - 2);
            labelWidth += 10;
            int num2 = overallWidth - labelWidth;
            if (fighters == null)
            {
                fighters = new FighterList();
            }
            int num3 = fighters.Count * _ImageSize;
            int num4 = _ImageSize / 2;
            int num5 = num3;
            int num6 = _ImageSize;
            if (num5 > num2)
            {
                int num7 = num5 - num4 * 3;
                num6 = (int)(((double)num2 - (double)num4 * 3.0) / (double)num7 * (double)_ImageSize);
                if (num6 > _ImageSize)
                {
                    num6 = _ImageSize;
                }
                num6 = Math.Max(1, num6);
            }
            DrawStringWithDropShadow(graphics, TextResolver.GetText("Fighters"), _NormalFontBold, location2);
            int num8 = labelWidth;
            Color color = Color.FromArgb(190, 24, 24, 32);
            SolidBrush solidBrush = new SolidBrush(color);
            Rectangle rect = new Rectangle(location.X + labelWidth, location.Y, overallWidth - labelWidth, _ImageSize);
            graphics.FillRectangle(solidBrush, rect);
            if (fighters.Count == 0)
            {
                DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", _NormalFont, new Point(location.X + labelWidth, location.Y));
            }
            else
            {
                FighterList fighterList = new FighterList();
                FighterList fighterList2 = new FighterList();
                FighterList fighterList3 = new FighterList();
                for (int i = 0; i < fighters.Count; i++)
                {
                    if (fighters[i].OnboardCarrier)
                    {
                        if (fighters[i].UnderConstruction)
                        {
                            fighterList.Add(fighters[i]);
                        }
                        else
                        {
                            fighterList2.Add(fighters[i]);
                        }
                    }
                    else
                    {
                        fighterList3.Add(fighters[i]);
                    }
                }
                for (int j = 0; j < fighterList3.Count; j++)
                {
                    if (fighterList3[j].Health < 1f)
                    {
                        using SolidBrush brush = new SolidBrush(Color.Red);
                        Rectangle rect2 = new Rectangle(location.X + num8, location.Y, _ImageSize, _ImageSize);
                        graphics.FillRectangle(brush, rect2);
                    }
                    Point point = new Point(location.X + num8, location.Y);
                    graphics.DrawImageUnscaled(_FighterImages[fighterList3[j].PictureRef], point);
                    AddHotspot(new Rectangle(point.X, point.Y, num6, _FighterImages[fighterList3[j].PictureRef].Height), fighterList3[j], fighterList3[j].Name + " (" + Galaxy.ResolveMissionDescription(fighterList3[j]) + ")");
                    num8 += num6;
                }
                num8 += num4;
                for (int k = 0; k < fighterList2.Count; k++)
                {
                    if (fighterList2[k].Health < 1f && !fighterList2[k].UnderConstruction)
                    {
                        using SolidBrush brush2 = new SolidBrush(Color.Red);
                        Rectangle rect3 = new Rectangle(location.X + num8, location.Y, _ImageSize, _ImageSize);
                        graphics.FillRectangle(brush2, rect3);
                    }
                    Point point2 = new Point(location.X + num8, location.Y);
                    graphics.DrawImageUnscaled(_FighterImages[fighterList2[k].PictureRef], point2);
                    string text = string.Empty;
                    if (fighterList2[k].ParentBuiltObject != null)
                    {
                        text = " (" + TextResolver.GetText("Onboard") + " " + fighterList2[k].ParentBuiltObject.Name + ")";
                    }
                    AddHotspot(new Rectangle(point2.X, point2.Y, num6, _FighterImages[fighterList2[k].PictureRef].Height), fighterList2[k], fighterList2[k].Name + text);
                    num8 += num6;
                }
                num8 += num4;
                for (int l = 0; l < fighterList.Count; l++)
                {
                    Point point3 = new Point(location.X + num8, location.Y);
                    graphics.DrawImageUnscaled(_FighterImagesFaded[fighterList[l].PictureRef], point3);
                    AddHotspot(new Rectangle(point3.X, point3.Y, num6, _FighterImagesFaded[fighterList[l].PictureRef].Height), fighterList[l], fighterList[l].Name + " (" + TextResolver.GetText("Building") + ")");
                    num8 += num6;
                }
            }
            solidBrush.Dispose();
        }

        private int DrawPopulation(int labelWidth, Habitat habitat, PopulationList populations, Graphics graphics, Point location, int overallWidth, int rowHeight)
        {
            using SolidBrush textBrush = new SolidBrush(_WhiteColor);
            return DrawPopulation(labelWidth, habitat, populations, graphics, location, overallWidth, rowHeight, textBrush);
        }

        private int DrawPopulation(int labelWidth, Habitat habitat, PopulationList populations, Graphics graphics, Point location, int overallWidth, int rowHeight, SolidBrush textBrush)
        {
            if (populations != null)
            {
                populations.Sort();
                populations.Reverse();
            }
            int num = (int)graphics.MeasureString(TextResolver.GetText("Populace"), _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            DrawStringWithDropShadow(location: new Point(location.X + (labelWidth - num), location.Y - 2), graphics: graphics, text: TextResolver.GetText("Populace"), font: _NormalFontBold);
            labelWidth += 10;
            int num2 = 0;
            for (int i = 0; i < populations.Count; i++)
            {
                Population population = populations[i];
                int num3 = labelWidth;
                Color color = Color.FromArgb(190, 24, 24, 32);
                SolidBrush solidBrush = new SolidBrush(color);
                Rectangle rect = new Rectangle(location.X + num3, location.Y + num2, overallWidth - num3 + 4, _ImageSize);
                graphics.FillRectangle(solidBrush, rect);
                Bitmap bitmap = _RaceImages[population.Race.PictureRef];
                string name = population.Race.Name;
                string text = BaconInfoPanel.FormatForLargeNumbers(population.Amount);
                string text2 = ((double)(population.GrowthRate - 1f)).ToString("+#0%;-#0%;0%");
                bool flag = false;
                if (populations.TotalAmount >= habitat.MaximumPopulation)
                {
                    flag = true;
                    text2 = TextResolver.GetText("Maximum Abbreviation");
                }
                if (i == 1 && populations.Count > 2)
                {
                    bitmap = null;
                    name = TextResolver.GetText("Others");
                    long num4 = 0L;
                    for (int j = 1; j < populations.Count; j++)
                    {
                        num4 += Math.Max(0L, populations[j].Amount);
                    }
                    text = BaconInfoPanel.FormatForLargeNumbers(num4);
                    text2 = string.Empty;
                }
                else if (i > 1)
                {
                    break;
                }
                Point point = new Point(location.X + num3, location.Y + num2);
                if (bitmap != null)
                {
                    graphics.DrawImageUnscaled(bitmap, point);
                    if (name != TextResolver.GetText("Others"))
                    {
                        AddHotspot(new Rectangle(point, bitmap.Size), population.Race, name + " (" + TextResolver.GetText("click for details") + ")");
                    }
                }
                num3 += _ImageSize;
                point = new Point(location.X + num3, location.Y + num2);
                Rectangle rectangle = new Rectangle(point.X, point.Y + 1, _PopulationTextWidth, _ImageSize);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                graphics.DrawString(name, _NormalFont, _BlackBrush, rectangle, stringFormat);
                rectangle = new Rectangle(point.X - 1, point.Y, _PopulationTextWidth, _ImageSize);
                graphics.DrawString(name, _NormalFont, textBrush, rectangle, stringFormat);
                if (name != TextResolver.GetText("Others"))
                {
                    AddHotspot(rectangle, population.Race, name + " (" + TextResolver.GetText("click for details") + ")");
                }
                num3 += _PopulationTextWidth;
                DrawStringWithDropShadow(location: new Point(location.X + num3, location.Y + num2), graphics: graphics, text: text, font: _NormalFont, brush: textBrush);
                num3 += _PopulationAmountWidth;
                point = new Point(location.X + num3, location.Y + num2);
                if (flag)
                {
                    DrawStringRedWithDropShadow(graphics, text2, _NormalFont, point);
                }
                else
                {
                    DrawStringWithDropShadow(graphics, text2, _NormalFont, point, textBrush);
                }
                solidBrush.Dispose();
                num2 += rowHeight;
            }
            if (populations.Count > 1)
            {
                int num5 = labelWidth + _ImageSize;
                DrawStringWithDropShadow(location: new Point(location.X + num5, location.Y + num2), graphics: graphics, text: TextResolver.GetText("TOTAL") + ":", font: _NormalFont, brush: textBrush);
                num5 += _PopulationTextWidth;
                DrawStringWithDropShadow(location: new Point(location.X + num5, location.Y + num2), graphics: graphics, text: populations.TotalAmount.ToString("0,,") + "M", font: _NormalFont, brush: textBrush);
                num5 += _PopulationAmountWidth;
                Point location6 = new Point(location.X + num5, location.Y + num2);
                if (populations.TotalAmount >= habitat.MaximumPopulation)
                {
                    DrawStringRedWithDropShadow(graphics, TextResolver.GetText("Maximum Abbreviation"), _NormalFont, location6);
                }
                else
                {
                    DrawStringWithDropShadow(graphics, (populations.OverallGrowthRate - 1.0).ToString("+#0%;-#0%;0%"), _NormalFont, location6, textBrush);
                }
                num2 += rowHeight;
            }
            if (num2 == 0)
            {
                num2 = rowHeight;
            }
            return num2;
        }

        private void DrawResources(int labelWidth, HabitatResourceList resources, Graphics graphics, Point location, int overallWidth)
        {
            using SolidBrush textBrush = new SolidBrush(_WhiteColor);
            DrawResources(labelWidth, resources, graphics, location, overallWidth, textBrush);
        }

        private void DrawResources(int labelWidth, HabitatResourceList resources, Graphics graphics, Point location, int overallWidth, SolidBrush textBrush)
        {
            int num = (int)graphics.MeasureString(TextResolver.GetText("Resource"), _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            DrawStringWithDropShadow(location: new Point(location.X + (labelWidth - num), location.Y - 2), graphics: graphics, text: TextResolver.GetText("Resource"), font: _NormalFontBold);
            labelWidth += 10;
            overallWidth += 5;
            Color color = Color.FromArgb(190, 24, 24, 32);
            SolidBrush solidBrush = new SolidBrush(color);
            Rectangle rect = new Rectangle(location.X + labelWidth, location.Y, overallWidth - labelWidth, _ImageSize);
            graphics.FillRectangle(solidBrush, rect);
            int num2 = labelWidth;
            Point location3 = new Point(location.X + num2, location.Y);
            if (resources == null || resources.Count == 0)
            {
                DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", _NormalFont, location3, textBrush);
                return;
            }
            int num3 = 3;
            HabitatResourceList habitatResourceList = resources.Clone();
            for (int i = 0; i < habitatResourceList.Count; i++)
            {
                int num4 = _ResourceImages[habitatResourceList[i].PictureRef].Width;
                string text = ((double)habitatResourceList[i].Abundance / 1000.0).ToString("0%");
                int num5 = (int)graphics.MeasureString(text, _TinyFont).Width;
                location3 = new Point(location.X + num2, location.Y);
                int num6 = Math.Max(0, (_ImageSize - _ResourceImages[habitatResourceList[i].PictureRef].Height) / 2);
                Point point = new Point(location3.X, location3.Y + num6);
                graphics.DrawImageUnscaled(_ResourceImages[habitatResourceList[i].PictureRef], point);
                AddHotspot(new Rectangle(point, _ResourceImages[habitatResourceList[i].PictureRef].Size), resources[i], habitatResourceList[i].Name + " (" + TextResolver.GetText("click for details") + ")");
                num2 += num4 - 2;
                DrawStringWithDropShadow(location: new Point(location.X + num2, location.Y + 1), graphics: graphics, text: text, font: _TinyFont, brush: textBrush);
                num2 += num5;
                num2 += num3;
            }
            solidBrush.Dispose();
        }

        public void DrawBuiltObjectList(int labelWidth, string label, BuiltObjectList builtObjects, int waitingCount, Graphics graphics, Point location, int overallWidth)
        {
            DrawBuiltObjectList(labelWidth, label, builtObjects, waitingCount, graphics, location, overallWidth, string.Empty);
        }

        public void DrawBuiltObjectList(int labelWidth, string label, BuiltObjectList builtObjects, int waitingCount, Graphics graphics, Point location, int overallWidth, string suffix)
        {
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            int num = (int)graphics.MeasureString(label, _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location2 = new Point(location.X + (labelWidth - num), location.Y - 2);
            labelWidth += 10;
            int num2 = overallWidth - labelWidth;
            if (builtObjects == null)
            {
                builtObjects = new BuiltObjectList();
            }
            int num3 = builtObjects.Count * _ImageSize;
            int num4 = _ImageSize / 2;
            string text = waitingCount + " " + TextResolver.GetText("waiting");
            int num5 = (int)graphics.MeasureString(text, _NormalFont).Width;
            int num6 = num3 + num4 + num5;
            int num7 = _ImageSize;
            if (num6 > num2)
            {
                int num8 = num6 - (num4 + num5);
                num7 = (int)(((double)num2 - ((double)num4 + (double)num5)) / (double)num8 * (double)_ImageSize);
                if (num7 > _ImageSize)
                {
                    num7 = _ImageSize;
                }
            }
            DrawStringWithDropShadow(graphics, label, _NormalFontBold, location2);
            int num9 = labelWidth;
            Point location3 = new Point(location.X + num9, location.Y);
            if (builtObjects.Count == 0)
            {
                int num10 = (int)graphics.MeasureString("(" + TextResolver.GetText("None") + ")", _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", _NormalFont, location3);
                num9 += num10 + num4;
            }
            else
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                for (int i = 0; i < builtObjects.Count; i++)
                {
                    location3 = new Point(location.X + num9, location.Y);
                    BuiltObject builtObject = builtObjects[i];
                    if (builtObject == null)
                    {
                        continue;
                    }
                    Bitmap bitmap = null;
                    if (_BuiltObjectImages.Length > builtObject.PictureRef)
                    {
                        bitmap = _BuiltObjectImages[builtObject.PictureRef];
                    }
                    string empty = string.Empty;
                    Rectangle rectangle = new Rectangle(location.X + num9, location.Y, _ImageSize, _ImageSize);
                    if (bitmap != null && bitmap.PixelFormat != 0)
                    {
                        Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                        if (builtObject.DamagedComponentCount > 0)
                        {
                            if (builtObject.ActualEmpire == _Game.PlayerEmpire || _Game.GodMode)
                            {
                                graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, 255, 0, 64)), rectangle);
                            }
                        }
                        else if (builtObject.UnbuiltComponentCount > 0 && (builtObject.ActualEmpire == _Game.PlayerEmpire || _Game.GodMode))
                        {
                            graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, 255, 128, 0)), rectangle);
                            empty = TextResolver.GetText("Under construction");
                        }
                        graphics.DrawImage(bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
                    }
                    if (!string.IsNullOrEmpty(empty))
                    {
                        AddHotspot(rectangle, builtObject, builtObject.Name + " (" + empty + " - " + TextResolver.GetText("click to select") + ")");
                    }
                    else
                    {
                        AddHotspot(rectangle, builtObject, builtObject.Name + " (" + TextResolver.GetText("click to select") + ")");
                    }
                    num9 += num7;
                }
            }
            if (builtObjects.Count > 0)
            {
                num9 += num4;
            }
            location3 = new Point(location.X + num9, location.Y);
            if (waitingCount > 0)
            {
                DrawStringWithDropShadow(graphics, text, _NormalFont, location3);
            }
            if (!string.IsNullOrEmpty(suffix))
            {
                int num11 = 0;
                if (waitingCount > 0)
                {
                    num11 = (int)graphics.MeasureString(text, _NormalFont, base.Width, StringFormat.GenericDefault).Width;
                    num11 += 10;
                }
                DrawStringWithDropShadow(location: new Point(location.X + num9 + num11, location.Y), graphics: graphics, text: suffix, font: _NormalFont);
            }
        }

        private Bitmap RotateImage(Image image, float angle)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            float num = image.Width;
            float num2 = image.Height;
            angle *= -1f;
            angle *= 57.29578f;
            angle %= 360f;
            if ((double)angle < 0.0)
            {
                angle += 360f;
            }
            PointF[] array = new PointF[4];
            array[1].X = num;
            array[2].X = num;
            array[2].Y = num2;
            array[3].Y = num2;
            Matrix matrix = new Matrix();
            matrix.Rotate(angle);
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
            Bitmap bitmap = new Bitmap((int)num7, (int)num8);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.SmoothingMode = SmoothingMode.None;
            PointF point = new PointF((float)(num7 / 2.0), (float)(num8 / 2.0));
            PointF point2 = new PointF(point.X - num / 2f, point.Y - num / 2f);
            matrix.Reset();
            matrix.RotateAt(angle, point);
            graphics.Transform = matrix;
            graphics.DrawImage(image, point2);
            return bitmap;
        }

        private Bitmap RotateBitmap(Bitmap InputImage, double angle)
        {
            double num = InputImage.Width;
            double num2 = InputImage.Height;
            Point[] array = new Point[4]
            {
            new Point(0, 0),
            new Point((int)num, 0),
            new Point(0, (int)num2),
            new Point((int)num, (int)num2)
            };
            double num3 = num / 2.0;
            double num4 = num2 / 2.0;
            for (int i = 0; i <= 3; i++)
            {
                array[i].X -= (int)num3;
                array[i].Y -= (int)num4;
            }
            double num5 = Math.Sin(angle);
            double num6 = Math.Cos(angle);
            for (int j = 0; j <= 3; j++)
            {
                double num7 = array[j].X;
                double num8 = array[j].Y;
                array[j].X = (int)(num7 * num6 + num8 * num5);
                array[j].Y = (int)((0.0 - num7) * num5 + num8 * num6);
            }
            double num9 = array[0].X;
            double num10 = array[0].Y;
            for (int k = 1; k <= 3; k++)
            {
                if (num9 > (double)array[k].X)
                {
                    num9 = array[k].X;
                }
                if (num10 > (double)array[k].Y)
                {
                    num10 = array[k].Y;
                }
            }
            for (int l = 0; l <= 3; l++)
            {
                array[l].X -= (int)num9;
                array[l].Y -= (int)num10;
            }
            Bitmap bitmap = new Bitmap((int)(-2.0 * num9), (int)(-2.0 * num10), PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            Point[] destPoints = new Point[3]
            {
            array[0],
            array[1],
            array[2]
            };
            graphics.DrawImage(InputImage, destPoints);
            return bitmap;
        }

        private void DrawCreature(Creature creature, Graphics graphics)
        {
            if (creature.HasBeenDestroyed)
            {
                _Game.SelectedObject = null;
                ClearData();
                return;
            }
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            int num = 6;
            int rowHeight = _RowHeight;
            int num2 = 5;
            int overallWidth = base.ClientRectangle.Width - 10;
            int labelWidth = _LabelWidth;
            Color backgroundColor = Color.FromArgb(127, 8, 8, 48);
            Font titleFont = _TitleFont;
            DrawBackgroundPicture(graphics);
            StringFormat genericTypographic = StringFormat.GenericTypographic;
            SizeF sizeF = graphics.MeasureString(creature.Name, titleFont, base.Width - 20, genericTypographic);
            graphics.DrawString(layoutRectangle: new RectangleF(num2, num, sizeF.Width, sizeF.Height + 2f), s: creature.Name, font: titleFont, brush: _WhiteBrush, format: genericTypographic);
            Point point = default(Point);
            num += (int)sizeF.Height;
            num += _Height8;
            point = new Point(num2, num);
            string text = TextResolver.GetText("Size") + ": " + creature.Size + ", " + TextResolver.GetText("Attack Strength") + ": " + creature.AttackStrength;
            DrawStringWithDropShadow(graphics, text, _NormalFont, point);
            num += rowHeight;
            num += _Height10;
            int num3 = rowHeight + (int)sizeF.Height + 5;
            num3 += rowHeight;
            Rectangle rect = new Rectangle(num2 - 2, num3, num2 + labelWidth - 1, base.ClientRectangle.Height - (num3 + 2));
            graphics.FillRectangle(_LabelAreaBrush, rect);
            labelWidth -= 5;
            DrawBarGraph(location: new Point(num2, num), description: TextResolver.GetText("Health"), descriptionWidth: labelWidth, maximumValue: creature.DamageKillThreshhold, currentValue: (int)((double)creature.DamageKillThreshhold - creature.Damage), height: rowHeight - 2, overallWidth: overallWidth, fillColorStart: Color.FromArgb(150, 48, 0, 96), fillColorEnd: Color.FromArgb(150, 128, 0, 255), backgroundColor: backgroundColor, graphics: graphics);
            num += rowHeight;
            num += _Height5;
            DrawBarGraph(location: new Point(num2, num), description: TextResolver.GetText("Speed Abbreviation"), descriptionWidth: labelWidth, maximumValue: creature.MovementSpeed, currentValue: (int)creature.CurrentSpeed, height: rowHeight - 2, overallWidth: overallWidth, fillColorStart: Color.FromArgb(150, 48, 0, 96), fillColorEnd: Color.FromArgb(150, 128, 0, 255), backgroundColor: backgroundColor, graphics: graphics);
            num += rowHeight;
        }

        private void DrawFighter(Fighter fighter, Graphics graphics)
        {
            if (fighter.HasBeenDestroyed)
            {
                _Game.SelectedObject = null;
                ClearData();
                return;
            }
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            SolidBrush solidBrush = new SolidBrush(_UnknownColor);
            bool flag = false;
            if (fighter.Empire != _Game.PlayerEmpire && _Game.PlayerEmpire.EmpiresViewable.Contains(fighter.Empire))
            {
                flag = true;
            }
            int num = 6;
            int rowHeight = _RowHeight;
            int num2 = 5;
            int num3 = base.ClientRectangle.Width - 10;
            int num4 = (int)graphics.MeasureString(TextResolver.GetText("Weapons"), _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Color backgroundColor = Color.FromArgb(127, 8, 8, 48);
            Font titleFont = _TitleFont;
            DrawBackgroundPicture(graphics);
            Point point = new Point(num3 - (_FlagSizeSmall.Width - 2), 6);
            if (_Fighter.Empire == _Galaxy.IndependentEmpire)
            {
                int num5 = (int)graphics.MeasureString(fighter.Empire.Name, _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                graphics.DrawString(point: new PointF((float)num3 - (float)num5, 6f), s: fighter.Empire.Name, font: _NormalFont, brush: _WhiteBrush);
            }
            else
            {
                graphics.DrawImageUnscaled(_EmpirePicture, point);
                if (_Fighter.Empire != null)
                {
                    AddHotspot(new Rectangle(point, _EmpirePicture.Size), _Fighter.Empire, _Fighter.Empire.Name + " (" + TextResolver.GetText("click for details") + ")");
                }
            }
            DrawStringWithDropShadow(location: new Point(num2, num), graphics: graphics, text: fighter.Name, font: titleFont, brush: new SolidBrush(_EmpireColor));
            num += titleFont.Height;
            num += 8;
            Point location2 = new Point(num2, num);
            string text = "(" + TextResolver.GetText("Unknown mission") + ")";
            if (fighter.Empire == _Game.PlayerEmpire || _Game.GodMode || flag)
            {
                Empire empire = fighter.Empire;
                if (empire == null)
                {
                    empire = _Galaxy.IndependentEmpire;
                }
                text = Galaxy.ResolveMissionDescription(fighter);
                DrawStringWithDropShadow(graphics, text, _NormalFont, location2);
                num += rowHeight;
            }
            else
            {
                DrawStringWithDropShadow(graphics, text, _NormalFont, location2, solidBrush);
                num += rowHeight;
            }
            num += _Height5;
            num4 += 5;
            int num6 = num;
            Rectangle rect = new Rectangle(num2 - 2, num6, num2 + num4 - 1, base.ClientRectangle.Height - (num6 + 2));
            graphics.FillRectangle(_LabelAreaBrush, rect);
            num4 -= 5;
            location2 = new Point(num2, num);
            string description = "(" + TextResolver.GetText("Abandoned") + ")";
            if (fighter.Empire != null)
            {
                description = fighter.Empire.Name;
            }
            if (fighter.Empire == _Galaxy.IndependentEmpire)
            {
                description = "(" + TextResolver.GetText("Independent") + ")";
            }
            DrawLabelledDescription(graphics, TextResolver.GetText("Empire"), num4, description, location2);
            num += rowHeight;
            num += _Height5;
            location2 = new Point(num2, num);
            if (fighter.Empire == _Game.PlayerEmpire || _Game.GodMode || flag)
            {
                string suffixData = string.Empty;
                if (fighter.UnderConstruction)
                {
                    suffixData = " (" + TextResolver.GetText("Under construction").ToLower(CultureInfo.InvariantCulture) + ")";
                }
                Math.Max(0, (int)fighter.CurrentEnergy);
                DrawBarGraph(TextResolver.GetText("Health"), num4, 100, (int)(fighter.Health * 100f), rowHeight - 2, num3, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, 255), backgroundColor, graphics, location2, suffixData);
            }
            else
            {
                DrawLabelledDescription(graphics, TextResolver.GetText("Health"), num4, "(" + TextResolver.GetText("Unknown") + ")", location2, solidBrush);
            }
            num += rowHeight;
            location2 = new Point(num2, num);
            if (fighter.Empire == _Game.PlayerEmpire || _Game.GodMode || flag)
            {
                int currentValue = Math.Max(0, (int)fighter.CurrentEnergy);
                DrawBarGraph(TextResolver.GetText("Energy"), num4, fighter.Specification.EnergyCapacity, currentValue, rowHeight - 2, num3, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, 255), backgroundColor, graphics, location2);
            }
            else
            {
                DrawLabelledDescription(graphics, TextResolver.GetText("Energy"), num4, "(" + TextResolver.GetText("Unknown") + ")", location2, solidBrush);
            }
            num += rowHeight;
            location2 = new Point(num2, num);
            string suffixData2 = string.Empty;
            if (fighter.ShieldsReducedLocation)
            {
                suffixData2 = " (" + TextResolver.GetText("reducing") + ")";
            }
            DrawBarGraph(TextResolver.GetText("Shields"), num4, fighter.Specification.ShieldsCapacity, (int)fighter.CurrentShields, rowHeight - 2, num3, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, 255), backgroundColor, graphics, location2, suffixData2);
            num += rowHeight;
            location2 = new Point(num2, num);
            string text2 = string.Empty;
            if (fighter.MovementSlowedLocation)
            {
                text2 = text2 + " (" + TextResolver.GetText("slowed") + ")";
            }
            DrawBarGraph(TextResolver.GetText("Speed"), num4, fighter.TopSpeed, (int)fighter.CurrentSpeed, rowHeight - 2, num3, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, 255), backgroundColor, graphics, location2, text2);
            num += rowHeight;
            num += _Height5;
            location2 = new Point(num2, num);
            string description2 = TextResolver.GetText("Firepower") + ": " + fighter.FirepowerRaw + ", " + TextResolver.GetText("Range") + ": " + fighter.Specification.WeaponRange;
            if (fighter.FirepowerRaw == 0)
            {
                description2 = "(" + TextResolver.GetText("None") + ")";
            }
            DrawLabelledDescription(graphics, TextResolver.GetText("Weapons"), num4, description2, location2);
            num += rowHeight;
            if (!string.IsNullOrEmpty(_CharacterBonuses))
            {
                DrawLabel(location: new Point(num2, num), graphics: graphics, label: TextResolver.GetText("Bonuses"), labelWidth: num4);
                int num7 = num3 - num4;
                SizeF size = graphics.MeasureString(_CharacterBonuses, _NormalFont, num7, StringFormat.GenericDefault);
                DrawStringWithDropShadowBounded(location: new Point(num2 + num4 + 10, num + 1), graphics: graphics, text: _CharacterBonuses, font: _NormalFont, size: size);
                num += (int)size.Height;
            }
            solidBrush.Dispose();
        }

        private void DrawBuiltObject(BuiltObject builtObject, Graphics graphics)
        {
            BaconInfoPanel.DrawBuiltObject(this, builtObject, graphics);
        }

        private void DrawSystemColoniesSummary(Habitat systemStar, Graphics graphics, int startY, int labelWidth)
        {
            int habitatImageSize = _HabitatImageSize;
            int num = 6;
            int num2 = 3;
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < _Galaxy.Systems[systemStar.SystemIndex].Habitats.Count; i++)
            {
                Habitat habitat = _Galaxy.Systems[systemStar.SystemIndex].Habitats[i];
                if (habitat.Empire != null && habitat.Population != null && habitat.Population.DominantRace != null && !habitatList.Contains(habitat))
                {
                    habitatList.Add(habitat);
                }
            }
            for (int j = 0; j < _Galaxy.Systems[systemStar.SystemIndex].Habitats.Count; j++)
            {
                Habitat habitat2 = _Galaxy.Systems[systemStar.SystemIndex].Habitats[j];
                if (habitat2.BasesAtHabitat.Count > 0 && (habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.MiningStation || habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.GasMiningStation || habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.ResortBase || habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.EnergyResearchStation || habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.WeaponsResearchStation || habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.HighTechResearchStation || habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.DefensiveBase || habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.MonitoringStation || habitat2.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.GenericBase) && !habitatList.Contains(habitat2))
                {
                    habitatList.Add(habitat2);
                }
            }
            int num3 = base.Width - labelWidth - 10;
            int num4 = num3 / habitatImageSize;
            num = (num3 - habitatImageSize * habitatList.Count) / (habitatList.Count + 1);
            num = Math.Max(1, num);
            int num5 = labelWidth + num;
            if (habitatList.Count > 0)
            {
                using Pen pen = new Pen(Color.FromArgb(170, 170, 170), 1f);
                pen.DashPattern = new float[2] { 1f, 2f };
                int num6 = startY + _HabitatImageSize + num2 + 13;
                int num7 = num5 - 5;
                int x = num7 + 10 + (Math.Min(habitatList.Count, num4) * (habitatImageSize + num) - num);
                graphics.DrawLine(pen, num7, num6, x, num6);
            }
            for (int k = 0; k < habitatList.Count; k++)
            {
                if (k >= num4)
                {
                    DrawStringWithDropShadow(graphics, "...", _NormalFont, new Point(num5, startY + 5));
                    break;
                }
                DrawSingleColonySummary(habitatList[k], graphics, num5, startY, num2);
                num5 += habitatImageSize + num;
            }
        }

        private void DrawSingleColonySummary(Habitat colony, Graphics graphics, int x, int y, int rowPadding)
        {
            Rectangle empty = Rectangle.Empty;
            Rectangle empty2 = Rectangle.Empty;
            bool flag = false;
            bool flag2 = false;
            if (colony.BasesAtHabitat != null && colony.BasesAtHabitat.Count > 0 && colony.BasesAtHabitat[0].Empire != null && colony.BasesAtHabitat[0].Empire != _Galaxy.IndependentEmpire && (colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.MiningStation || colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.GasMiningStation || colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.ResortBase))
            {
                flag = true;
                if (colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.MiningStation || colony.BasesAtHabitat[0].SubRole == BuiltObjectSubRole.GasMiningStation)
                {
                    flag2 = true;
                }
            }
            AddHotspot(new Rectangle(x, y, _HabitatImages[colony.PictureRef].Width, _HabitatImages[colony.PictureRef].Height), colony, colony.Name + " (" + TextResolver.GetText("click to select") + ")");
            graphics.DrawImage(_HabitatImages[colony.PictureRef], x, y);
            y += _HabitatImageSize + rowPadding / 2;
            int colonySummaryDetailWidth = _ColonySummaryDetailWidth;
            int num = (_HabitatImageSize - colonySummaryDetailWidth) / 2;
            x += num;
            SetGraphicsQualityToHigh(graphics);
            string empty3 = string.Empty;
            switch (colony.Type)
            {
                case HabitatType.BarrenRock:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Rock");
                    break;
                case HabitatType.Continental:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Continental");
                    break;
                case HabitatType.Desert:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Desert");
                    break;
                case HabitatType.Ice:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Ice");
                    break;
                case HabitatType.MarshySwamp:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Marshy Swamp");
                    break;
                case HabitatType.Ocean:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Ocean");
                    break;
                case HabitatType.Volcanic:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Volcanic");
                    break;
                case HabitatType.FrozenGasGiant:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Frozen Gas Giant");
                    break;
                case HabitatType.GasGiant:
                    empty3 = TextResolver.GetText("PlanetType Abbreviation Gas Giant");
                    break;
            }
            if (colony.Category == HabitatCategoryType.Asteroid)
            {
                empty3 = TextResolver.GetText("PlanetType Abbreviation Asteroid");
            }
            else if (colony.Category == HabitatCategoryType.GasCloud)
            {
                empty3 = TextResolver.GetText("PlanetType Abbreviation Gas Cloud");
            }
            int num2 = (int)graphics.MeasureString(empty3, _TinyFont, _MaxGraphTextWidth, StringFormat.GenericDefault).Width;
            int num3 = x + (colonySummaryDetailWidth - num2) / 2;
            DrawStringWithDropShadow(graphics, empty3, _TinyFont, new Point(num3, y));
            y += 15 + rowPadding + rowPadding;
            int num4 = (int)((double)colonySummaryDetailWidth * 0.6);
            if (!flag && colony.Empire != null && colony.Empire != _Galaxy.IndependentEmpire)
            {
                empty = new Rectangle(0, 0, colony.Empire.LargeFlagPicture.Width, colony.Empire.LargeFlagPicture.Height);
                empty2 = new Rectangle(x, y, colonySummaryDetailWidth, num4);
                graphics.DrawImage(colony.Empire.LargeFlagPicture, empty2, empty, GraphicsUnit.Pixel);
                AddHotspot(empty2, colony.Empire, colony.Empire.Name + " (" + TextResolver.GetText("click for details") + ")");
            }
            else if (colony.BasesAtHabitat.Count > 0)
            {
                Empire empire = colony.BasesAtHabitat[0].Empire;
                if (empire != null)
                {
                    empty = new Rectangle(0, 0, empire.LargeFlagPicture.Width, empire.LargeFlagPicture.Height);
                    empty2 = new Rectangle(x, y, colonySummaryDetailWidth, num4);
                    graphics.DrawImage(empire.LargeFlagPicture, empty2, empty, GraphicsUnit.Pixel);
                    AddHotspot(empty2, empire, empire.Name + " (" + TextResolver.GetText("click for details") + ")");
                }
            }
            y += num4 + rowPadding;
            SetGraphicsQualityToLow(graphics);
            int num5 = 0;
            if (!flag && colony.Population != null && colony.Population.DominantRace != null)
            {
                num5 = (colonySummaryDetailWidth - _RaceImages[colony.Population.DominantRace.PictureRef].Width) / 2;
                Bitmap bitmap = _RaceImages[colony.Population.DominantRace.PictureRef];
                graphics.DrawImage(bitmap, x + num5, y);
                AddHotspot(new Rectangle(x + num5, y, bitmap.Width, bitmap.Height), colony.Population.DominantRace, colony.Population.DominantRace.Name + " (" + TextResolver.GetText("click for details") + ")");
                y += _RaceImages[0].Height + rowPadding;
                DrawPopulationIndicator(colony, graphics, x, y);
            }
            else
            {
                if (colony.BasesAtHabitat.Count <= 0)
                {
                    return;
                }
                SetGraphicsQualityToHigh(graphics);
                empty = new Rectangle(0, 0, _BuiltObjectImages[colony.BasesAtHabitat[0].PictureRef].Width, _BuiltObjectImages[colony.BasesAtHabitat[0].PictureRef].Height);
                empty2 = new Rectangle(x + 1, y, colonySummaryDetailWidth - 2, colonySummaryDetailWidth - 2);
                if (_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(colony.BasesAtHabitat[0]))
                {
                    AddHotspot(empty2, colony.BasesAtHabitat[0], colony.BasesAtHabitat[0].Name + " (" + TextResolver.GetText("click to select") + ")");
                }
                graphics.DrawImage(_BuiltObjectImages[colony.BasesAtHabitat[0].PictureRef], empty2, empty, GraphicsUnit.Pixel);
                y += empty2.Height + rowPadding;
                if (!flag2)
                {
                    return;
                }
                if (_Galaxy.PlayerEmpire.ResourceMap != null && _Galaxy.PlayerEmpire.ResourceMap.CheckResourcesKnown(colony))
                {
                    SetGraphicsQualityToLow(graphics);
                    HabitatResourceList habitatResourceList = colony.Resources.Clone();
                    if (habitatResourceList.Count > 0)
                    {
                        num5 = (colonySummaryDetailWidth - _ResourceImages[habitatResourceList[0].PictureRef].Width) / 2;
                        AddHotspot(new Rectangle(x + num5, y, _ResourceImages[habitatResourceList[0].PictureRef].Width, _ResourceImages[habitatResourceList[0].PictureRef].Height), habitatResourceList[0], habitatResourceList[0].Name + " (" + TextResolver.GetText("click for details") + ")");
                        graphics.DrawImage(_ResourceImages[habitatResourceList[0].PictureRef], x + num5, y);
                    }
                }
                else
                {
                    num5 = 6;
                    DrawStringWithDropShadow(graphics, "?", _TinyFont, new Point(x + num5, y));
                }
            }
        }

        private void DrawPopulationIndicator(Habitat habitat, Graphics graphics, int x, int y)
        {
            int num = 20;
            int num2 = 15;
            int num3 = num / 5;
            int num4 = num2 / 5;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    graphics.FillRectangle(_SemiSubtleBrush, x + i * num3, y + (4 - j) * num4, num3 - 1, num4 - 1);
                }
            }
            int num5 = 0;
            int num6 = 0;
            if (habitat.Population.TotalAmount > 2500000000u)
            {
                num5 = 5;
            }
            else if (habitat.Population.TotalAmount > 500000000)
            {
                num5 = 4;
            }
            else if (habitat.Population.TotalAmount > 100000000)
            {
                num5 = 3;
            }
            else if (habitat.Population.TotalAmount > 20000000)
            {
                num5 = 2;
            }
            else if (habitat.Population.TotalAmount > 0)
            {
                num5 = 1;
            }
            if (habitat.DevelopmentLevel > 80)
            {
                num6 = 5;
            }
            else if (habitat.DevelopmentLevel > 60)
            {
                num6 = 4;
            }
            else if (habitat.DevelopmentLevel > 40)
            {
                num6 = 3;
            }
            else if (habitat.DevelopmentLevel > 20)
            {
                num6 = 2;
            }
            else if (habitat.DevelopmentLevel > 0)
            {
                num6 = 1;
            }
            for (int k = 0; k < num5; k++)
            {
                for (int l = 0; l < num6; l++)
                {
                    graphics.FillRectangle(_BrightBrush, x + k * num3, y + (4 - l) * num4, num3 - 1, num4 - 1);
                }
            }
        }

        private void DrawHabitat(Habitat habitat, Graphics graphics)
        {
            if (habitat.HasBeenDestroyed)
            {
                _Game.SelectedObject = null;
                ClearData();
                return;
            }
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            bool flag = false;
            if (habitat.Empire != _Game.PlayerEmpire)
            {
                if (_Game.PlayerEmpire.EmpiresViewable.Contains(habitat.Empire))
                {
                    flag = true;
                }
                else
                {
                    PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(_Game.PlayerEmpire);
                    if (byFaction != null)
                    {
                        flag = true;
                    }
                }
            }
            SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Unexplored;
            if (_Game.PlayerEmpire.CheckSystemExplored(habitat.SystemIndex))
            {
                systemVisibilityStatus = SystemVisibilityStatus.Explored;
            }
            if (_Game.PlayerEmpire.CheckSystemVisible(habitat.SystemIndex))
            {
                systemVisibilityStatus = SystemVisibilityStatus.Visible;
            }
            if (_Game.GodMode)
            {
                systemVisibilityStatus = SystemVisibilityStatus.Visible;
            }
            Color color = _WhiteColor;
            Color color2 = _WhiteColor;
            if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
            {
                color = _UnknownColor;
            }
            if (_Game.PlayerEmpire.ResourceMap == null || !_Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat))
            {
                color2 = _UnknownColor;
            }
            SolidBrush solidBrush = new SolidBrush(color);
            SolidBrush solidBrush2 = new SolidBrush(color2);
            int num = 3;
            int rowHeight = _RowHeight;
            int num2 = 5;
            int num3 = base.ClientRectangle.Width - 10;
            Color.FromArgb(127, 8, 8, 48);
            Font titleFont = _TitleFont;
            DrawBackgroundPicture(graphics);
            string description = string.Empty;
            Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
            if (habitat2 != null)
            {
                description = habitat2.Name;
            }
            int labelWidthHabitat = _LabelWidthHabitat;
            int num4 = num + titleFont.Height + 3;
            num4 += rowHeight + 3;
            if (habitat2 == habitat && habitat.Category != HabitatCategoryType.GasCloud)
            {
                labelWidthHabitat = _LabelWidthHabitat;
            }
            if (habitat.IsBlockaded)
            {
                num4 += rowHeight;
            }
            if (habitat.PlagueId >= 0)
            {
                num4 += rowHeight;
            }
            Rectangle rect = new Rectangle(num2 - 2, num4, num2 + labelWidthHabitat - 1, base.ClientRectangle.Height - (num4 + 2));
            if (habitat.Category != 0)
            {
                graphics.FillRectangle(_LabelAreaBrush, rect);
            }
            labelWidthHabitat -= 5;
            Point point = new Point(num3 - (_FlagSizeSmall.Width - 5), 3);
            if (habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire)
            {
                if (_EmpirePicture != null)
                {
                    graphics.DrawImageUnscaled(_EmpirePicture, point);
                    if (habitat.Empire != null)
                    {
                        AddHotspot(new Rectangle(point, _EmpirePicture.Size), habitat.Empire, habitat.Empire.Name + " (" + TextResolver.GetText("click for details") + ")");
                    }
                }
            }
            else if (habitat.Population.TotalAmount > 0)
            {
                int num5 = (int)graphics.MeasureString(TextResolver.GetText("Independent"), _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                graphics.DrawString(point: new PointF((float)num3 - (float)num5, 6f), s: TextResolver.GetText("Independent"), font: _NormalFont, brush: solidBrush);
            }
            Point point3 = new Point(num2, num + 2);
            if (habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire)
            {
                if (habitat.Empire.Capital == habitat)
                {
                    graphics.DrawImageUnscaled(_CapitalColonyImage, point3);
                    point3 = new Point(num2 + (_CapitalColonyImage.Width + 2), num);
                }
                else if (habitat.Empire.Capitals.Contains(habitat))
                {
                    graphics.DrawImageUnscaled(_RegionalCapitalColonyImage, point3);
                    point3 = new Point(num2 + (_RegionalCapitalColonyImage.Width + 2), num);
                }
            }
            if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                if (habitat.Empire != null && habitat.Empire.MainColor.ToArgb() != _EmpireColor.ToArgb())
                {
                    _EmpireColor = habitat.Empire.MainColor;
                }
                using SolidBrush brush = new SolidBrush(_EmpireColor);
                DrawStringWithDropShadow(graphics, habitat.Name, titleFont, point3, brush);
                if (habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire)
                {
                    int num6 = (int)graphics.MeasureString(habitat.Name, titleFont, 300, StringFormat.GenericTypographic).Width;
                    DrawStringWithDropShadow(location: new Point(point3.X + num6 + 7, point3.Y + 3), graphics: graphics, text: "(" + habitat.Empire.Name + ")", font: _NormalFont, brush: brush);
                }
            }
            else
            {
                string text = Galaxy.ResolveDescription(habitat.Category);
                DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Unknown") + " " + text + ")", titleFont, point3, new SolidBrush(_UnknownColor));
            }
            num += titleFont.Height + 3;
            point3 = new Point(num2, num);
            double num7 = (double)habitat.Diameter / 10.0;
            string text2 = Galaxy.ResolveDescription(habitat.Type);
            if (habitat.Type != HabitatType.BlackHole)
            {
                text2 = text2 + " " + Galaxy.ResolveDescription(habitat.Category);
            }
            text2 = text2 + ", " + num7.ToString("##0.0K");
            if (habitat.Category == HabitatCategoryType.Planet || habitat.Category == HabitatCategoryType.Moon)
            {
                double num8 = (double)habitat.Quality * 100.0;
                string text3 = text2;
                text2 = text3 + ",   " + TextResolver.GetText("Quality") + ": " + num8.ToString("0") + "%";
                if (habitat.BaseQuality != habitat.Quality)
                {
                    double num9 = (double)habitat.BaseQuality * 100.0;
                    text3 = text2;
                    text2 = text3 + " (" + TextResolver.GetText("Maximum Abbreviation") + " " + num9.ToString("0") + "%)";
                }
            }
            DrawStringWithDropShadow(graphics, text2, _NormalFont, point3, solidBrush);
            if ((_Galaxy.PlayerEmpire.PirateEmpireBaseHabitat != null || _Game.GodMode) && habitat.GetPirateControl().Count > 0)
            {
                num += rowHeight;
                point3 = new Point(num2, num);
                Empire empire = null;
                PirateColonyControl pirateColonyControl = null;
                string text4 = string.Empty;
                for (int i = 0; i < habitat.GetPirateControl().Count; i++)
                {
                    PirateColonyControl pirateColonyControl2 = habitat.GetPirateControl()[i];
                    if (pirateColonyControl2 == null)
                    {
                        continue;
                    }
                    Empire byEmpireId = _Galaxy.PirateEmpires.GetByEmpireId(pirateColonyControl2.EmpireId);
                    if (byEmpireId != null)
                    {
                        if (empire == null)
                        {
                            empire = byEmpireId;
                            pirateColonyControl = pirateColonyControl2;
                        }
                        if (text4.Length > 0)
                        {
                            text4 += ", ";
                        }
                        string text3 = text4;
                        text4 = text3 + byEmpireId.Name + " (" + pirateColonyControl2.ControlLevel.ToString("0%") + ")";
                    }
                }
                if (empire != null && empire.Active && pirateColonyControl != null)
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    int num10 = (int)((double)_FlagSizeSmall.Width * 0.734);
                    int num11 = (int)((double)num10 * 0.6);
                    graphics.DrawImage(rect: new Rectangle(num2 + 2, num, num10, num11), image: empire.LargeFlagPicture);
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    point3 = new Point(num2 + num10 + 5, num);
                    string empty = string.Empty;
                    empty = ((habitat.GetPirateControl().Count <= 1) ? string.Format(TextResolver.GetText("Pirate Control Description Sole"), empire.Name, pirateColonyControl.ControlLevel.ToString("0%")) : string.Format(TextResolver.GetText("Pirate Control Description Multiple"), empire.Name, pirateColonyControl.ControlLevel.ToString("0%"), (habitat.GetPirateControl().Count - 1).ToString("0")));
                    DrawStringWithDropShadow(graphics, empty, _NormalFont, point3);
                    AddHotspot(new Rectangle(5, point3.Y, base.Width - 10, rowHeight), null, text4);
                }
            }
            if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                if (habitat.Ruin != null)
                {
                    point3 = new Point(base.ClientRectangle.Width - (_RuinImages[habitat.Ruin.PictureRef].Width + 8), num);
                    graphics.DrawImageUnscaled(_RuinImages[habitat.Ruin.PictureRef], point3);
                    AddHotspot(new Rectangle(point3, _RuinImages[habitat.Ruin.PictureRef].Size), habitat.Ruin, habitat.Ruin.Name + " (" + TextResolver.GetText("click for details") + ")");
                }
                num += rowHeight;
                if (habitat.PlagueId >= 0)
                {
                    Plague plague = _Galaxy.Plagues[habitat.PlagueId];
                    if (plague != null)
                    {
                        int num12 = num2;
                        Bitmap bitmap = null;
                        if (plague.PictureRef >= 0 && plague.PictureRef < _PlagueImages.Length)
                        {
                            bitmap = _PlagueImages[plague.PictureRef];
                        }
                        if (bitmap != null)
                        {
                            graphics.DrawImage(bitmap, new Point(num2, num));
                            num12 += bitmap.Width + 3;
                        }
                        string text5 = string.Format(TextResolver.GetText("Plague Colony Infection"), plague.Name).ToUpper(CultureInfo.InvariantCulture) + "!";
                        DrawStringWithDropShadow(location: new Point(num12, num), graphics: graphics, text: text5, font: _NormalFont, brush: _RedBrush);
                        num += rowHeight;
                    }
                }
                if (habitat.IsBlockaded)
                {
                    Blockade blockade = _Galaxy.Blockades[habitat];
                    if (blockade != null)
                    {
                        point3 = new Point(num2, num);
                        double num13 = (double)rowHeight / (double)_BlockadeImage.Height;
                        int num14 = (int)((double)_BlockadeImage.Width * num13);
                        graphics.DrawImage(rect: new Rectangle(num2, num, num14, rowHeight), image: _BlockadeImage);
                        graphics.DrawImage(point: new Point(num2 + num14 + 2, num + 3), image: blockade.Initiator.SmallFlagPicture);
                        string text6 = string.Format(TextResolver.GetText("Blockaded by EMPIRE"), blockade.Initiator.Name);
                        DrawStringWithDropShadow(location: new Point(num2 + num14 + blockade.Initiator.SmallFlagPicture.Width + 4, num), graphics: graphics, text: text6, font: _NormalFont, brush: solidBrush);
                        num += rowHeight;
                    }
                }
                if (habitat.RaidCountdown > 0)
                {
                    point3 = new Point(num2, num);
                    using (SolidBrush brush2 = new SolidBrush(Color.Yellow))
                    {
                        DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("This colony was recently Raided") + ")", _NormalFont, point3, brush2);
                    }
                    num += rowHeight;
                }
                if ((flag || habitat.Empire == _Game.PlayerEmpire || _Game.GodMode) && habitat.ManufacturingQueue != null && habitat.ManufacturingQueue.DeficientResources != null && habitat.ManufacturingQueue.DeficientResources.Count > 0)
                {
                    ResourceDatePair[] array = habitat.ManufacturingQueue.DeficientResources.ToArray();
                    string text7 = string.Empty;
                    for (int j = 0; j < array.Length; j++)
                    {
                        if (j > 0)
                        {
                            text7 += ", ";
                        }
                        text7 += new Resource(array[j].ResourceId).Name;
                    }
                    Bitmap bitmap2 = _MessageImages[30];
                    Rectangle srcRect = new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
                    Rectangle destRect = new Rectangle(num2 + 3, num, rowHeight, rowHeight);
                    SetGraphicsQualityToHigh(graphics);
                    graphics.DrawImage(bitmap2, destRect, srcRect, GraphicsUnit.Pixel);
                    point3 = new Point(destRect.Right + 2, num);
                    string text8 = string.Format(TextResolver.GetText("Construction Resource Shortage Message Short"), text7);
                    SizeF size = graphics.MeasureString(text8, _NormalFont, base.ClientSize.Width - (point3.X + 5));
                    using (SolidBrush brush3 = new SolidBrush(Color.FromArgb(255, 128, 0)))
                    {
                        size = new SizeF(size.Width, Math.Min((float)rowHeight * 2f + 1f, size.Height));
                        DrawStringWithDropShadowBounded(graphics, text8, _NormalFont, point3, size, brush3);
                    }
                    num += (int)size.Height;
                }
                num += _Height5;
                if (habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire && (habitat.Empire == _Game.PlayerEmpire || _Game.GodMode || flag))
                {
                    CharacterList characterList = new CharacterList();
                    if (habitat.Characters != null)
                    {
                        characterList = habitat.Characters.GetCharactersByRole(CharacterRole.ColonyGovernor);
                    }
                    if (characterList.Count > 0)
                    {
                        int num15 = num3 - 35;
                        for (int k = 0; k < characterList.Count; k++)
                        {
                            point3 = new Point(num15, num - 20);
                            Bitmap bitmap3 = _CharacterImageCache.ObtainCharacterImageSmall(characterList[k]);
                            if (bitmap3 != null && bitmap3.PixelFormat != 0)
                            {
                                graphics.DrawImageUnscaled(bitmap3, point3);
                                string text9 = characterList[k].Name + " (" + Galaxy.ResolveDescription(characterList[k].Role) + ")";
                                text9 = text9 + "   (" + TextResolver.GetText("click for details") + ")";
                                AddHotspot(new Rectangle(point3.X, point3.Y, bitmap3.Width, bitmap3.Height), characterList[k], text9);
                                num15 -= 38;
                            }
                        }
                    }
                }
                point3 = new Point(num2, num);
                if (habitat2 == habitat && habitat.Category != HabitatCategoryType.GasCloud)
                {
                    string text10 = TextResolver.GetText("Solar") + " " + habitat.SolarRadiation;
                    string text3 = text10;
                    text10 = text3 + ", " + TextResolver.GetText("Microwave") + " " + habitat.MicrowaveRadiation;
                    text3 = text10;
                    text10 = text3 + ", " + TextResolver.GetText("X-ray") + " " + habitat.XrayRadiation;
                    DrawLabelledDescription(graphics, TextResolver.GetText("Energy").ToUpper(CultureInfo.InvariantCulture), labelWidthHabitat, text10, point3, solidBrush);
                    num += rowHeight;
                    if (habitat.ResearchBonus > 0)
                    {
                        point3 = new Point(num2, num);
                        string description2 = string.Format(arg0: ((float)(int)habitat.ResearchBonus / 100f).ToString("+0%"), format: TextResolver.GetText("X research bonus to AREA"), arg1: Galaxy.ResolveDescription(habitat.ResearchBonusIndustry));
                        DrawLabelledDescription(graphics, TextResolver.GetText("Research"), labelWidthHabitat, description2, point3, solidBrush);
                        num += rowHeight;
                    }
                    if (habitat.ScenicFactor > 0f)
                    {
                        point3 = new Point(num2, num);
                        string description3 = habitat.ScenicFactor.ToString("+0%");
                        DrawLabelledDescription(graphics, TextResolver.GetText("Scenery"), labelWidthHabitat, description3, point3, solidBrush);
                        num += rowHeight;
                    }
                    int maxWidth = num3 - (num2 + labelWidthHabitat + 10);
                    DesignList designList = _Galaxy.PlayerEmpire.CheckBasesToBeBuiltAtHabitat(habitat);
                    if (designList.Count > 0)
                    {
                        num += 4;
                        point3 = new Point(num2 + labelWidthHabitat + 10, num);
                        string text11 = string.Format(TextResolver.GetText("Construction ship queued to build X here"), Galaxy.ResolveDescription(designList[0].SubRole));
                        SizeF sizeF = graphics.MeasureString(text11, _NormalFont, maxWidth, StringFormat.GenericTypographic);
                        DrawStringWithDropShadow(graphics, text11, _NormalFont, point3, _WhiteBrush, maxWidth);
                        num += (int)sizeF.Height;
                    }
                    num += rowHeight;
                    if (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored)
                    {
                        DrawSystemColoniesSummary(habitat, graphics, num, labelWidthHabitat);
                    }
                }
                else
                {
                    DrawLabelledDescription(graphics, TextResolver.GetText("System"), labelWidthHabitat, description, point3, solidBrush);
                    num += rowHeight;
                    if (habitat.ResearchBonus > 0)
                    {
                        point3 = new Point(num2, num);
                        string description4 = string.Format(arg0: ((float)(int)habitat.ResearchBonus / 100f).ToString("+0%"), format: TextResolver.GetText("X research bonus to AREA"), arg1: Galaxy.ResolveDescription(habitat.ResearchBonusIndustry));
                        DrawLabelledDescription(graphics, TextResolver.GetText("Research"), labelWidthHabitat, description4, point3, solidBrush);
                        num += rowHeight;
                    }
                    if (habitat.ScenicFactor > 0f)
                    {
                        point3 = new Point(num2, num);
                        string description5 = habitat.ScenicFactor.ToString("+0%");
                        if (!string.IsNullOrEmpty(habitat.ScenicFeature))
                        {
                            description5 = string.Format(TextResolver.GetText("BONUSAMOUNT from FEATURE"), habitat.ScenicFactor.ToString("+0%"), habitat.ScenicFeature);
                        }
                        DrawLabelledDescription(graphics, TextResolver.GetText("Scenery"), labelWidthHabitat, description5, point3, solidBrush);
                        num += rowHeight;
                    }
                    if (habitat.Ruin != null)
                    {
                        point3 = new Point(num2, num);
                        SizeF sizeF2 = graphics.MeasureString(habitat.Ruin.Name, _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic);
                        DrawLabelledDescription(graphics, TextResolver.GetText("Ruins"), labelWidthHabitat, habitat.Ruin.Name, point3, solidBrush);
                        Rectangle region = new Rectangle(num2 + labelWidthHabitat + 10, num, (int)sizeF2.Width + 1, rowHeight);
                        AddHotspot(region, habitat.Ruin, habitat.Ruin.Name + " (" + TextResolver.GetText("click for details") + ")");
                        num += rowHeight;
                    }
                    num += _Height4;
                    if (habitat.Population.TotalAmount > 0)
                    {
                        num += DrawPopulation(location: new Point(num2, num), labelWidth: labelWidthHabitat, habitat: habitat, populations: habitat.Population, graphics: graphics, overallWidth: num3, rowHeight: rowHeight, textBrush: solidBrush);
                    }
                    if (habitat.Empire == _Galaxy.IndependentEmpire || habitat.Empire == null)
                    {
                        point3 = new Point(num2, num);
                        int num16 = _Galaxy.CheckColonizationLikeliness(habitat, _Game.PlayerEmpire.DominantRace);
                        SolidBrush solidBrush3 = null;
                        solidBrush3 = ((_EmpireCanColonize && num16 > -5 && !(habitat.Quality < 0.5f)) ? new SolidBrush(solidBrush.Color) : new SolidBrush(Color.Red));
                        if (_EmpireCanColonize && habitat.Quality < 0.5f)
                        {
                            DrawLabelledDescription(graphics, TextResolver.GetText("Colonize"), labelWidthHabitat, TextResolver.GetText("Yes, but low quality is undesirable"), point3, solidBrush3);
                        }
                        else
                        {
                            DrawLabelledDescription(graphics, TextResolver.GetText("Colonize"), labelWidthHabitat, _ColonizeExplanation, point3, solidBrush3);
                        }
                        solidBrush3.Dispose();
                        num += rowHeight;
                    }
                    point3 = new Point(num2, num);
                    bool flag2 = false;
                    if (_Game.PlayerEmpire.ResourceMap != null)
                    {
                        flag2 = _Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat);
                    }
                    if (habitat.Empire == _Game.PlayerEmpire || _Game.GodMode || flag2)
                    {
                        DrawResources(labelWidthHabitat, habitat.Resources, graphics, point3, num3, solidBrush2);
                    }
                    else
                    {
                        DrawLabelledDescription(graphics, TextResolver.GetText("Resource"), labelWidthHabitat, "(" + TextResolver.GetText("Unknown") + ")", point3, solidBrush2);
                    }
                    num += rowHeight;
                    num += _Height4;
                    if (habitat.Population.TotalAmount > 0 && habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire)
                    {
                        int num17 = num2;
                        point3 = new Point(num17, num);
                        string description6 = habitat.StrategicValue.ToString("0,K");
                        int num18 = (int)graphics.MeasureString(description6, _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                        string text12 = habitat.DevelopmentLevel.ToString("##0") + "%";
                        if (habitat.Ruin != null || habitat.WonderForDevelopment != null)
                        {
                            int val = 0;
                            if (habitat.WonderForDevelopment != null)
                            {
                                val = habitat.WonderForDevelopment.Value1;
                            }
                            else if (habitat.Ruin != null)
                            {
                                val = Math.Max(val, (int)(habitat.Ruin.DevelopmentBonus * 100.0));
                            }
                            text12 += "(";
                            text12 = text12 + val.ToString("+##0;-##0;0") + "%)";
                        }
                        int num19 = (int)graphics.MeasureString(text12, _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                        string value = string.Empty;
                        if (habitat.Empire == _Game.PlayerEmpire || _Game.GodMode || flag || systemVisibilityStatus == SystemVisibilityStatus.Visible)
                        {
                            value = habitat.EmpireApprovalRating.ToString("+###;-###;0");
                        }
                        _ = graphics.MeasureString(value, _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                        DrawLabelledDescription(graphics, TextResolver.GetText("Value"), labelWidthHabitat, description6, point3, solidBrush);
                        num17 += labelWidthHabitat + 10 + num18 + (int)((double)_ImageSize * 0.75);
                        graphics.DrawImageUnscaled(point: new Point(num17, num), image: _DevelopmentImage);
                        num17 += _ImageSize;
                        DrawStringWithDropShadow(location: new Point(num17, num), graphics: graphics, text: text12, font: _NormalFont, brush: solidBrush);
                        num17 += num19;
                        num17 += (int)((double)_ImageSize * 0.75);
                        point3 = new Point(num17, num);
                        Bitmap bitmap4 = null;
                        if (!string.IsNullOrEmpty(value))
                        {
                            bitmap4 = ((habitat.EmpireApprovalRating > 15.0) ? _ApprovalSmileImage : ((habitat.EmpireApprovalRating > 0.0) ? _ApprovalNeutralImage : ((!(habitat.EmpireApprovalRating > -15.0)) ? _ApprovalAngryImage : _ApprovalSadImage)));
                            if (habitat.Rebelling)
                            {
                                bitmap4 = _ApprovalAngryImage;
                            }
                            graphics.DrawImageUnscaled(bitmap4, point3);
                            num17 += _ImageSize;
                            DrawStringWithDropShadow(location: new Point(num17, num), graphics: graphics, text: value, font: _NormalFont, brush: solidBrush);
                        }
                        num += rowHeight;
                        point3 = new Point(num2, num);
                        double num20 = 0.0;
                        if (habitat.Empire != null)
                        {
                            num20 = ((habitat.Empire.PirateEmpireBaseHabitat == null) ? (habitat.AnnualRevenue / habitat.Empire.PrivateAnnualRevenue) : (habitat.AnnualRevenue / habitat.Empire.CalculateAccurateAnnualIncome()));
                            num20 = Math.Max(0.0, num20);
                        }
                        string empty2 = string.Empty;
                        double annualRevenue = habitat.AnnualRevenue;
                        if (habitat.Empire != _Game.PlayerEmpire && !_Game.GodMode && !flag)
                        {
                            empty2 = ((!(annualRevenue < 1000.0)) ? annualRevenue.ToString("0,K") : (annualRevenue / 1000.0).ToString("0.00K"));
                        }
                        else
                        {
                            empty2 = ((annualRevenue < 0.0) ? string.Format(TextResolver.GetText("Colony Revenue and GDP portion"), (annualRevenue / 1000.0).ToString("0K"), num20.ToString("##0%")) : ((!(annualRevenue < 1000.0)) ? string.Format(TextResolver.GetText("Colony Revenue and GDP portion"), annualRevenue.ToString("0,K"), num20.ToString("##0%")) : string.Format(TextResolver.GetText("Colony Revenue and GDP portion"), (annualRevenue / 1000.0).ToString("0.00K"), num20.ToString("##0.00%"))));
                            double corruption = habitat.Corruption;
                            string text3 = empty2;
                            empty2 = text3 + " (" + corruption.ToString("##0%") + " " + TextResolver.GetText("Corruption").ToLower(CultureInfo.InvariantCulture) + ")";
                        }
                        DrawLabelledDescription(graphics, TextResolver.GetText("GDP"), labelWidthHabitat, empty2, point3, solidBrush);
                        num += rowHeight;
                        point3 = new Point(num2, num);
                        string empty3 = string.Empty;
                        double annualTaxRevenue = habitat.AnnualTaxRevenue;
                        double taxComplianceRate = habitat.TaxComplianceRate;
                        if (habitat.Empire == _Game.PlayerEmpire || _Game.GodMode || flag)
                        {
                            empty3 = habitat.TaxRate.ToString("#0%;-#0%;0%");
                            if (habitat.Rebelling)
                            {
                                empty3 = empty3 + " " + TextResolver.GetText("NO TAX PAID (Rebelling)");
                            }
                            else
                            {
                                empty3 = ((!(annualTaxRevenue < 1000.0)) ? (empty3 + " (" + annualTaxRevenue.ToString("0,K") + ")") : (empty3 + " (" + (annualTaxRevenue / 1000.0).ToString("0.00K") + ")"));
                                if (habitat.TaxRate > 0f)
                                {
                                    string text3 = empty3;
                                    empty3 = text3 + " = " + taxComplianceRate.ToString("0%") + " " + TextResolver.GetText("compliance");
                                }
                            }
                        }
                        else
                        {
                            empty3 = habitat.TaxRate.ToString("#0%;-#0%;0%") + " (" + habitat.AnnualTaxRevenue.ToString("0,K") + ")";
                        }
                        DrawLabelledDescription(graphics, TextResolver.GetText("Tax"), labelWidthHabitat, empty3, point3, solidBrush);
                        num += rowHeight;
                        num += _Height4;
                    }
                    int offsetX = 0;
                    int overallWidth = num3;
                    DrawFacilities(location: new Point(num2, num), labelWidth: labelWidthHabitat, habitat: habitat, facilities: habitat.Facilities, graphics: graphics, overallWidth: num3);
                    num += rowHeight;
                    if ((habitat.Population.TotalAmount > 0 && habitat.Empire != null) || (habitat.Troops != null && habitat.Troops.Count > 0) || (habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0) || (habitat.TroopsToRecruit != null && habitat.TroopsToRecruit.Count > 0) || (habitat.Characters != null && habitat.Characters.Count > 0) || (habitat.InvadingCharacters != null && habitat.InvadingCharacters.Count > 0))
                    {
                        point3 = new Point(num2, num);
                        bool flag3 = false;
                        if ((habitat.Characters != null && habitat.Characters.CheckCharactersOfEmpirePresent(_Game.PlayerEmpire)) || (habitat.InvadingCharacters != null && habitat.InvadingCharacters.CheckCharactersOfEmpirePresent(_Game.PlayerEmpire)) || (habitat.Troops != null && habitat.Troops.CheckTroopsOfEmpirePresent(_Game.PlayerEmpire)) || (habitat.InvadingTroops != null && habitat.InvadingTroops.CheckTroopsOfEmpirePresent(_Game.PlayerEmpire)))
                        {
                            flag3 = true;
                        }
                        if (habitat.Empire == _Game.PlayerEmpire || _Game.GodMode || flag || flag3 || systemVisibilityStatus == SystemVisibilityStatus.Visible)
                        {
                            DrawTroopsAgents(labelWidthHabitat, habitat.Troops, habitat.TroopsToRecruit, habitat.InvadingTroops, habitat.Characters, habitat.InvadingCharacters, graphics, point3, overallWidth, offsetX);
                            string text13 = string.Format(TextResolver.GetText("Show Colony Ground Report"), habitat.Name);
                            if (habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0)
                            {
                                text13 = string.Format(TextResolver.GetText("Show Colony Battle Report"), habitat.Name);
                                Color color3 = GraphicsHelper.OscillateColor(Color.FromArgb(0, 255, 0, 0), Color.FromArgb(160, 255, 0, 0), DateTime.Now);
                                using SolidBrush brush4 = new SolidBrush(color3);
                                graphics.FillRectangle(brush4, new Rectangle(5, point3.Y, base.Width - 10, rowHeight));
                            }
                            Empire invader = null;
                            Empire defender = null;
                            habitat.ResolveInvasionEmpires(out defender, out invader);
                            if (defender == null)
                            {
                                defender = habitat.Empire;
                            }
                            int defendingStrength = 0;
                            int attackingStrength = 0;
                            double totalDefendModifier = 0.0;
                            double totalAttackModifier = 0.0;
                            habitat.CalculateForceStrengths(defender, invader, habitat.Troops, habitat.Characters, habitat.InvadingTroops, habitat.InvadingCharacters, out defendingStrength, out attackingStrength, out totalDefendModifier, out totalAttackModifier, out var _, out var _, out var _, out var _);
                            habitat.Troops.GetTroopCountsByType(out var infantryCount, out var artilleryCount, out var armorCount, out var specialForcesCount);
                            string text14 = " (" + Galaxy.ResolveTroopCompositionDescription(infantryCount, artilleryCount, armorCount, specialForcesCount) + ")";
                            bool isDefending = false;
                            int num21 = habitat.CalculatePopulationStrength(out isDefending, invader, defender);
                            if (isDefending)
                            {
                                defendingStrength += num21;
                            }
                            else
                            {
                                attackingStrength += num21;
                            }
                            if (invader != null)
                            {
                                string text15 = string.Format(TextResolver.GetText("Battle Strength Description"), defendingStrength.ToString("0,K") + text14, attackingStrength.ToString("0,K"));
                                string text3 = text13;
                                text13 = text3 + "  (" + TextResolver.GetText("Strength") + ": " + text15 + ")";
                            }
                            else
                            {
                                string text3 = text13;
                                text13 = text3 + "  (" + TextResolver.GetText("Strength") + ": " + defendingStrength.ToString("0,K") + text14 + ")";
                            }
                            AddHotspot(new Rectangle(5, point3.Y, base.Width - 10, rowHeight), new object[1] { habitat }, text13);
                        }
                        else
                        {
                            DrawLabelledDescription(graphics, TextResolver.GetText("Troops"), labelWidthHabitat, "(" + TextResolver.GetText("Unknown") + ")", point3, solidBrush);
                        }
                        num += rowHeight;
                        if (habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0 && (habitat.Empire == _Galaxy.PlayerEmpire || habitat.InvadingTroops[0].Empire == _Galaxy.PlayerEmpire))
                        {
                            habitat.ResolveInvasionEmpires(out var defender2, out var invader2);
                            int attackingStrength2 = 0;
                            int defendingStrength2 = 0;
                            habitat.CalculateForceStrengths(defender2, invader2, habitat.Troops, habitat.Characters, habitat.InvadingTroops, habitat.InvadingCharacters, out defendingStrength2, out attackingStrength2);
                            bool isDefending2 = true;
                            int num22 = habitat.CalculatePopulationStrength(out isDefending2, invader2, defender2);
                            string text16 = attackingStrength2.ToString("0,K");
                            string text17 = defendingStrength2.ToString("0,K");
                            if (isDefending2)
                            {
                                text17 = (defendingStrength2 + num22).ToString("0,K");
                                text17 = text17 + " (" + string.Format(TextResolver.GetText("X from population"), num22.ToString("0,K")) + ")";
                            }
                            else
                            {
                                text16 = (attackingStrength2 + num22).ToString("0,K");
                                text16 = text16 + " (" + string.Format(TextResolver.GetText("X from population"), num22.ToString("0,K")) + ")";
                            }
                            string description7 = "  " + text17 + "   vs   " + text16;
                            point3 = new Point(num2, num);
                            Color color4 = GraphicsHelper.OscillateColor(Color.FromArgb(0, 255, 0, 0), Color.FromArgb(160, 255, 0, 0), DateTime.Now);
                            using (SolidBrush brush5 = new SolidBrush(color4))
                            {
                                graphics.FillRectangle(brush5, new Rectangle(5, point3.Y, base.Width - 10, rowHeight));
                            }
                            DrawLabelledDescription(graphics, "", labelWidthHabitat, description7, point3);
                            string message = string.Format(TextResolver.GetText("Show Colony Battle Report"), habitat.Name);
                            AddHotspot(new Rectangle(5, point3.Y, base.Width - 10, rowHeight), new object[1] { habitat }, message);
                            num += rowHeight;
                        }
                    }
                    if (habitat.Population.TotalAmount > 0 && habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire && habitat.ConstructionQueue != null && habitat.ConstructionQueue.ConstructionYards.Count > 0)
                    {
                        point3 = new Point(num2, num);
                        if (habitat.Empire == _Game.PlayerEmpire || _Game.GodMode || flag || systemVisibilityStatus == SystemVisibilityStatus.Visible)
                        {
                            BuiltObjectList builtObjectList = new BuiltObjectList();
                            for (int l = 0; l < habitat.ConstructionQueue.ConstructionYards.Count; l++)
                            {
                                ConstructionYard constructionYard = habitat.ConstructionQueue.ConstructionYards[l];
                                if (constructionYard.ShipUnderConstruction != null)
                                {
                                    builtObjectList.Add(constructionYard.ShipUnderConstruction);
                                }
                            }
                            DrawBuiltObjectList(labelWidthHabitat, TextResolver.GetText("Building"), builtObjectList, habitat.ConstructionQueue.ConstructionWaitQueue.Count, graphics, point3, num3);
                        }
                        else
                        {
                            DrawLabelledDescription(graphics, TextResolver.GetText("Building"), labelWidthHabitat, "(" + TextResolver.GetText("Unknown") + ")", point3, solidBrush);
                        }
                        num += rowHeight;
                    }
                    if (habitat.Population.TotalAmount > 0 && habitat.Empire != null && habitat.DockingBays != null && habitat.DockingBays.Count > 0)
                    {
                        point3 = new Point(num2, num);
                        if (habitat.Empire == _Game.PlayerEmpire || _Game.GodMode || flag || systemVisibilityStatus == SystemVisibilityStatus.Visible)
                        {
                            BuiltObjectList builtObjectList2 = new BuiltObjectList();
                            for (int m = 0; m < habitat.DockingBays.Count; m++)
                            {
                                DockingBay dockingBay = habitat.DockingBays[m];
                                if (dockingBay.DockedShip != null)
                                {
                                    builtObjectList2.Add(dockingBay.DockedShip);
                                }
                            }
                            if (habitat.DockingBayWaitQueue != null)
                            {
                                DrawBuiltObjectList(labelWidthHabitat, TextResolver.GetText("Docked"), builtObjectList2, habitat.DockingBayWaitQueue.Count, graphics, point3, num3);
                            }
                            else
                            {
                                DrawBuiltObjectList(labelWidthHabitat, TextResolver.GetText("Docked"), builtObjectList2, 0, graphics, point3, num3);
                            }
                        }
                        else
                        {
                            DrawLabelledDescription(graphics, TextResolver.GetText("Docked"), labelWidthHabitat, "(" + TextResolver.GetText("Unknown") + ")", point3, solidBrush);
                        }
                        num += rowHeight;
                    }
                    if (habitat.Empire == _Galaxy.IndependentEmpire || habitat.Empire == null)
                    {
                        int maxWidth2 = num3 - (num2 + labelWidthHabitat + 10);
                        BuiltObject builtObject = _Galaxy.PlayerEmpire.CheckColonizingHabitat(habitat);
                        if (builtObject != null)
                        {
                            num += _Height4;
                            point3 = new Point(num2 + labelWidthHabitat + 10, num);
                            string text18 = string.Format(TextResolver.GetText("COLONYSHIP colonizing here"), builtObject.Name);
                            SizeF sizeF3 = graphics.MeasureString(text18, _NormalFont, maxWidth2, StringFormat.GenericTypographic);
                            DrawStringWithDropShadow(graphics, text18, _NormalFont, point3, _WhiteBrush, maxWidth2);
                            num += (int)sizeF3.Height;
                        }
                        DesignList designList2 = _Galaxy.PlayerEmpire.CheckBasesToBeBuiltAtHabitat(habitat);
                        if (designList2.Count > 0)
                        {
                            num += 4;
                            point3 = new Point(num2 + labelWidthHabitat + 10, num);
                            string text19 = string.Format(TextResolver.GetText("Construction ship queued to build X here"), Galaxy.ResolveDescription(designList2[0].SubRole));
                            SizeF sizeF4 = graphics.MeasureString(text19, _NormalFont, maxWidth2, StringFormat.GenericTypographic);
                            DrawStringWithDropShadow(graphics, text19, _NormalFont, point3, _WhiteBrush, maxWidth2);
                            num += (int)sizeF4.Height;
                        }
                    }
                }
                if (_ShowExtendedInfo && !string.IsNullOrEmpty(_CharacterBonuses))
                {
                    DrawLabel(location: new Point(num2, num), graphics: graphics, label: TextResolver.GetText("Bonuses"), labelWidth: labelWidthHabitat);
                    int num23 = num3 - labelWidthHabitat;
                    SizeF size2 = graphics.MeasureString(_CharacterBonuses, _NormalFont, num23, StringFormat.GenericDefault);
                    DrawStringWithDropShadowBounded(location: new Point(num2 + labelWidthHabitat + 10, num + 1), graphics: graphics, text: _CharacterBonuses, font: _NormalFont, size: size2);
                    num += (int)size2.Height;
                }
            }
            solidBrush.Dispose();
            solidBrush2.Dispose();
        }

        public void DrawLabel(Graphics graphics, string label, int labelWidth, Point location)
        {
            int num = (int)graphics.MeasureString(label, _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            DrawStringWithDropShadow(location: new Point(location.X + (labelWidth - num), location.Y), graphics: graphics, text: label, font: _NormalFontBold);
        }

        public void DrawLabelledDescription(Graphics graphics, string label, int labelWidth, string description, Point location)
        {
            using SolidBrush textBrush = new SolidBrush(_WhiteColor);
            DrawLabelledDescription(graphics, label, labelWidth, description, location, textBrush);
        }

        public void DrawLabelledDescription(Graphics graphics, string label, int labelWidth, string description, Point location, SolidBrush textBrush)
        {
            int num = (int)graphics.MeasureString(label, _NormalFontBold, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
            Point location2 = new Point(location.X + (labelWidth - num), location.Y - 2);
            labelWidth += 10;
            Point location3 = new Point(location.X + labelWidth, location.Y);
            DrawStringWithDropShadow(graphics, label, _NormalFontBold, location2);
            DrawStringWithDropShadow(graphics, description, _NormalFont, location3, textBrush);
        }

        private void DrawStringRedWithDropShadow(Graphics graphics, string text, Font font, Point location)
        {
            BaconInfoPanel.DrawStringRedWithDropShadow(this, graphics, text, font, location);
        }

        private void DrawStringColorWithDropShadow(Graphics graphics, string text, Font font, Point location, Color color)
        {
            BaconInfoPanel.DrawStringColorWithDropShadow(this, graphics, text, font, location, color);
        }

        private void DrawStringWithDropShadow(Graphics graphics, string text, Font font, Point location)
        {
            DrawStringWithDropShadow(graphics, text, font, location, _WhiteBrush);
        }

        private void DrawStringWithDropShadow(Graphics graphics, string text, Font font, Point location, SolidBrush brush)
        {
            BaconInfoPanel.DrawStringWithDropShadow(this, graphics, text, font, location, brush);
        }

        private void DrawStringWithDropShadow(Graphics graphics, string text, Font font, Point location, SolidBrush brush, int maxWidth)
        {
            BaconInfoPanel.DrawStringWithDropShadow(this, graphics, text, font, location, brush, maxWidth);
        }

        public Color CheckDropshadowColor(Color mainColor)
        {
            Color black = Color.Black;
            if (mainColor.ToArgb() == _PirateColor.ToArgb() || mainColor.ToArgb() == _UnknownColor.ToArgb())
            {
                return black;
            }
            int num = mainColor.R + mainColor.G + mainColor.B;
            if (num <= 176)
            {
                black = _WhiteBrush.Color;
            }
            return Galaxy.DetermineContrastDropShadowColor(mainColor);
        }

        public void DrawStringWithDropShadowBounded(Graphics graphics, string text, Font font, Point location, SizeF size)
        {
            DrawStringWithDropShadowBounded(graphics, text, font, location, size, _WhiteBrush);
        }

        public void DrawStringWithDropShadowBounded(Graphics graphics, string text, Font font, Point location, SizeF size, SolidBrush brush)
        {
            BaconInfoPanel.DrawStringWithDropShadowBounded(this, graphics, text, font, location, size, brush);
        }

        public void SetData(Game game, Galaxy galaxy, BuiltObjectList builtObjects)
        {
            SetFonts();
            RepointImageInstances();
            _Hotspots.Clear();
            _AddHotspots = true;
            _Game = game;
            _Galaxy = galaxy;
            _BuiltObjects = builtObjects;
            _BuiltObject = null;
            _Fighter = null;
            _Habitat = null;
            _Creature = null;
            _ShipGroup = null;
            _SystemInfo = null;
            _Picture = null;
            _SmugglingMission = null;
            if (builtObjects != null && builtObjects.Count > 0)
            {
                SetEmpirePictureAndColor(builtObjects[0].Empire);
            }
            else
            {
                SetEmpirePictureAndColor(null);
            }
            _CharacterBonuses = string.Empty;
            _PictureAngle = 0.0;
            _PictureSize = 0;
            using Graphics graphics = CreateGraphics();
            ClearPanel(graphics);
            DrawPanelWithBackground(graphics);
        }

        public void SetData(Game game, Galaxy galaxy, Bitmap backgroundPicture, SystemInfo systemInfo)
        {
            SetFonts();
            RepointImageInstances();
            _Hotspots.Clear();
            _AddHotspots = true;
            _Game = game;
            _Galaxy = galaxy;
            _BuiltObjects = null;
            _BuiltObject = null;
            _Fighter = null;
            _Habitat = null;
            _Creature = null;
            _ShipGroup = null;
            _SystemInfo = systemInfo;
            _Picture = null;
            _SmugglingMission = null;
            SetEmpirePictureAndColor(null);
            _EmpireColor = _UnknownColor;
            _CharacterBonuses = string.Empty;
            _Picture = backgroundPicture;
            _PictureAngle = 0.0;
            _PictureSize = systemInfo.SystemStar.Diameter;
            if (_PictureSize < _MinPictureSize)
            {
                _PictureSize = _MinPictureSize;
            }
            if (_PictureSize > _MaxPictureSize)
            {
                _PictureSize = _MaxPictureSize;
            }
            _Picture = PreProcessImage(_Picture, _PictureSize);
            Empire empire = _Galaxy.CheckSystemOwnership(systemInfo.SystemStar);
            if (!_Game.PlayerEmpire.CheckSystemExplored(systemInfo.SystemStar.SystemIndex))
            {
                _EmpireColor = _UnknownColor;
            }
            else if (empire != null)
            {
                _EmpireColor = empire.MainColor;
            }
            else
            {
                _EmpireColor = _WhiteColor;
            }
            using Graphics graphics = CreateGraphics();
            ClearPanel(graphics);
            DrawPanelWithBackground(graphics);
        }

        public void SetData(Game game, Galaxy galaxy, ShipGroup shipGroup)
        {
            SetFonts();
            RepointImageInstances();
            _Hotspots.Clear();
            _AddHotspots = true;
            _Game = game;
            _Galaxy = galaxy;
            _BuiltObjects = null;
            _BuiltObject = null;
            _Fighter = null;
            _Habitat = null;
            _Creature = null;
            _ShipGroup = shipGroup;
            _SystemInfo = null;
            _Picture = null;
            _SmugglingMission = null;
            SetEmpirePictureAndColor(shipGroup.Empire);
            _PictureAngle = 0.0;
            _PictureSize = 0;
            _CharacterBonuses = Galaxy.GenerateCharacterBonusDescription(shipGroup);
            using Graphics graphics = CreateGraphics();
            ClearPanel(graphics);
            DrawPanelWithBackground(graphics);
        }

        public void SetData(Game game, Galaxy galaxy, Bitmap backgroundPicture, Bitmap maskImage, Creature creature)
        {
            SetFonts();
            RepointImageInstances();
            _Hotspots.Clear();
            _AddHotspots = true;
            _Game = game;
            _Galaxy = galaxy;
            _BuiltObjects = null;
            _BuiltObject = null;
            _Fighter = null;
            _Habitat = null;
            _Creature = creature;
            _ShipGroup = null;
            _SystemInfo = null;
            _SmugglingMission = null;
            if (_Picture != null)
            {
                _Picture.Dispose();
            }
            _Picture = FadeImage(backgroundPicture, 0.33f);
            if (_MaskImage != null)
            {
                _MaskImage.Dispose();
            }
            if (maskImage != null && maskImage.PixelFormat != 0)
            {
                _MaskImage = new Bitmap(maskImage);
            }
            SetEmpirePictureAndColor(null);
            _CharacterBonuses = string.Empty;
            _EmpirePicture = null;
            _PictureAngle = creature.CurrentHeading * -1f;
            _PictureSize = (int)((double)creature.Size / 0.6);
            if (_PictureSize < _MinPictureSize)
            {
                _PictureSize = _MinPictureSize;
            }
            if (_PictureSize > _MaxPictureSize)
            {
                _PictureSize = _MaxPictureSize;
            }
            using Graphics graphics = CreateGraphics();
            ClearPanel(graphics);
            DrawPanelWithBackground(graphics);
        }

        private void DrawSystemInfo(SystemInfo systemInfo, Graphics graphics)
        {
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            SystemVisibilityStatus systemVisibilityStatus = _Game.PlayerEmpire.CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
            if (_Game.GodMode)
            {
                systemVisibilityStatus = SystemVisibilityStatus.Visible;
            }
            Color color = _WhiteColor;
            if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
            {
                color = _UnknownColor;
            }
            SolidBrush solidBrush = new SolidBrush(color);
            int num = 6;
            int rowHeight = _RowHeight;
            int num2 = 5;
            int num3 = base.ClientRectangle.Width - 10;
            Font titleFont = _TitleFont;
            Empire empire = _Galaxy.CheckSystemOwnership(systemInfo.SystemStar);
            string text = systemInfo.SystemStar.Name;
            string empty = string.Empty;
            if (systemInfo.SystemStar.Category == HabitatCategoryType.Star && systemInfo.SystemStar.Type != HabitatType.BlackHole)
            {
                empty = TextResolver.GetText("System");
            }
            else if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
            {
                empty = TextResolver.GetText("HabitatCategoryType GasCloud");
            }
            else if (systemInfo.SystemStar.Type == HabitatType.BlackHole)
            {
                empty = TextResolver.GetText("HabitatType BlackHole");
            }
            if (systemInfo.SystemStar.Type != HabitatType.BlackHole)
            {
                text = text + " " + empty;
            }
            DrawBackgroundPicture(graphics);
            int labelWidth = _LabelWidth;
            int num4 = num + titleFont.Height + 5;
            num4 += rowHeight + 3;
            new Rectangle(num2 - 2, num4, num2 + labelWidth - 1, base.ClientRectangle.Height - (num4 + 2));
            labelWidth -= 5;
            Point location = new Point(num2, num);
            if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                DrawStringWithDropShadow(graphics, text, titleFont, location, new SolidBrush(_EmpireColor));
            }
            else
            {
                DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Unknown") + " " + empty + ")", titleFont, location, new SolidBrush(_UnknownColor));
            }
            num += titleFont.Height + 5;
            location = new Point(num2, num);
            string text2 = string.Empty;
            if (systemInfo.SystemStar != null)
            {
                text2 = ((systemInfo.SystemStar.Type != HabitatType.BlackHole) ? (Galaxy.ResolveDescription(systemInfo.SystemStar.Type) + " " + Galaxy.ResolveDescription(systemInfo.SystemStar.Category)) : TextResolver.GetText("HabitatType BlackHole"));
            }
            string empty2 = string.Empty;
            bool flag = false;
            if (systemInfo.DominantEmpire != null && _Game.PlayerEmpire.EmpiresViewable.Contains(systemInfo.DominantEmpire.Empire))
            {
                flag = true;
            }
            empty2 = ((systemVisibilityStatus != SystemVisibilityStatus.Visible && systemVisibilityStatus != SystemVisibilityStatus.Explored && !_Game.GodMode && !flag) ? text2 : ((systemInfo.SystemStar.Category != HabitatCategoryType.GasCloud) ? (text2 + ", " + string.Format(TextResolver.GetText("X planets, Y moons"), systemInfo.PlanetCount.ToString(), systemInfo.MoonCount.ToString())) : text2));
            DrawStringWithDropShadow(graphics, empty2, _NormalFont, location, solidBrush);
            num += rowHeight;
            if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                if (systemInfo.SystemStar.ResearchBonus > 0)
                {
                    num += _Height5;
                    location = new Point(num2, num);
                    string description = string.Format(arg0: ((float)(int)systemInfo.SystemStar.ResearchBonus / 100f).ToString("+0%"), format: TextResolver.GetText("X research bonus to AREA"), arg1: Galaxy.ResolveDescription(systemInfo.SystemStar.ResearchBonusIndustry));
                    DrawLabelledDescription(graphics, TextResolver.GetText("Research"), labelWidth, description, location, solidBrush);
                    num += rowHeight;
                }
                if (systemInfo.SystemStar.ScenicFactor > 0f)
                {
                    num += _Height5;
                    location = new Point(num2, num);
                    string description2 = systemInfo.SystemStar.ScenicFactor.ToString("+0%");
                    DrawLabelledDescription(graphics, TextResolver.GetText("Scenery"), labelWidth, description2, location, solidBrush);
                    num += rowHeight;
                }
            }
            num += _Height5;
            DrawLabel(location: new Point(num2, num - 2), graphics: graphics, label: TextResolver.GetText("Owner"), labelWidth: labelWidth);
            Size flagSizeSystem = _FlagSizeSystem;
            Rectangle empty3 = Rectangle.Empty;
            Rectangle empty4 = Rectangle.Empty;
            SetGraphicsQualityToHigh(graphics);
            if (empire != null)
            {
                int num5 = num2 + labelWidth + 10;
                string empty5 = string.Empty;
                if (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || _Game.GodMode || flag)
                {
                    location = new Point(num5, num);
                    empty3 = new Rectangle(0, 0, empire.LargeFlagPicture.Width, empire.LargeFlagPicture.Height);
                    empty4 = new Rectangle(location, flagSizeSystem);
                    graphics.DrawImage(empire.LargeFlagPicture, empty4, empty3, GraphicsUnit.Pixel);
                    if (empire != null)
                    {
                        AddHotspot(empty4, empire, empire.Name + " (" + TextResolver.GetText("click for details") + ")");
                    }
                    num5 += flagSizeSystem.Width + 2;
                    location = new Point(num5, num);
                    empty5 = empire.Name;
                    DrawStringWithDropShadow(graphics, empty5, _NormalFont, location, solidBrush);
                    num5 += (int)graphics.MeasureString(empty5, _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                    num5 += 10;
                    location = new Point(num5, num);
                    if (systemInfo.DominantEmpire != null)
                    {
                        graphics.DrawImageUnscaled(_ColonyImage, location);
                        num5 += _ColonyImage.Width;
                        location = new Point(num5, num);
                        string text3 = systemInfo.DominantEmpire.ColonyCount.ToString();
                        _ = graphics.MeasureString(text3, _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                        DrawStringWithDropShadow(graphics, text3, _NormalFont, location, solidBrush);
                    }
                }
                else
                {
                    location = new Point(num5, num);
                    empty5 = "(" + TextResolver.GetText("Unknown") + ")";
                    DrawStringWithDropShadow(graphics, empty5, _NormalFont, location, solidBrush);
                    num5 += (int)graphics.MeasureString(empty5, _NormalFont, _MaxGraphTextWidth, StringFormat.GenericTypographic).Width;
                    num5 += 10;
                }
            }
            else
            {
                location = new Point(num2 + 10 + labelWidth, num);
                if (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored || _Game.GodMode)
                {
                    DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", _NormalFont, location, solidBrush);
                }
                else
                {
                    DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Unknown") + ")", _NormalFont, location, solidBrush);
                }
            }
            num += rowHeight + _Height6;
            DrawLabel(location: new Point(num2, num - 2), graphics: graphics, label: TextResolver.GetText("Resource"), labelWidth: labelWidth);
            location = new Point(num2 + 10 + labelWidth, num);
            if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                HabitatResourceList habitatResourceList = new HabitatResourceList();
                if (systemInfo.SystemStar.Resources != null && systemInfo.SystemStar.Resources.Count > 0)
                {
                    HabitatResourceList habitatResourceList2 = systemInfo.SystemStar.Resources.Clone();
                    for (int i = 0; i < habitatResourceList2.Count; i++)
                    {
                        if (!habitatResourceList.Contains(habitatResourceList2[i]))
                        {
                            habitatResourceList.Add(habitatResourceList2[i]);
                        }
                    }
                }
                if (systemInfo.Habitats != null)
                {
                    for (int j = 0; j < systemInfo.Habitats.Count; j++)
                    {
                        if (_Game.PlayerEmpire.ResourceMap == null || !_Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(systemInfo.Habitats[j]))
                        {
                            continue;
                        }
                        HabitatResourceList habitatResourceList3 = systemInfo.Habitats[j].Resources.Clone();
                        for (int k = 0; k < habitatResourceList3.Count; k++)
                        {
                            if (!habitatResourceList.Contains(habitatResourceList3[k]))
                            {
                                habitatResourceList.Add(habitatResourceList3[k]);
                            }
                        }
                    }
                }
                int num6 = 2;
                int num7 = labelWidth + 10 + num2;
                if (habitatResourceList.Count > 0)
                {
                    for (int l = 0; l < habitatResourceList.Count; l++)
                    {
                        int num8 = _ResourceImages[habitatResourceList[l].PictureRef].Width;
                        if (num7 + num8 + num6 > num3)
                        {
                            num += rowHeight + _Height2;
                            num7 = labelWidth + 10 + num2 - num6;
                        }
                        location = new Point(num7, num);
                        graphics.DrawImageUnscaled(_ResourceImages[habitatResourceList[l].PictureRef], location);
                        AddHotspot(new Rectangle(location, _ResourceImages[habitatResourceList[l].PictureRef].Size), habitatResourceList[l], habitatResourceList[l].Name + " (" + TextResolver.GetText("click for details") + ")");
                        num7 += num8;
                        num7 += num6;
                    }
                }
                else
                {
                    DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("None") + ")", _NormalFont, location, solidBrush);
                }
            }
            else
            {
                DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("Unknown") + ")", _NormalFont, location, solidBrush);
            }
            num += rowHeight + rowHeight;
            if (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored)
            {
                DrawSystemColoniesSummary(systemInfo.SystemStar, graphics, num, labelWidth);
            }
            solidBrush.Dispose();
        }

        private void DrawShipGroup(ShipGroup shipGroup, Graphics graphics)
        {
            BaconInfoPanel.DrawShipGroup(this, shipGroup, graphics);
        }

        private Color UpdateColor(Color normalColor, Color alternateColor)
        {
            double num = 0.0;
            int second = DateTime.Now.ToUniversalTime().Second;
            int num2 = DateTime.Now.ToUniversalTime().Millisecond;
            if (second % 2 == 1)
            {
                num2 += 1000;
            }
            num = ((num2 <= 1000) ? ((double)Math.Abs(1000 - num2) / 1000.0) : ((double)(num2 - 1000) / 1000.0));
            byte alpha = (byte)(normalColor.A - (byte)((double)(normalColor.A - alternateColor.A) * num));
            byte red = (byte)(normalColor.R - (byte)((double)(normalColor.R - alternateColor.R) * num));
            byte green = (byte)(normalColor.G - (byte)((double)(normalColor.G - alternateColor.G) * num));
            byte blue = (byte)(normalColor.B - (byte)((double)(normalColor.B - alternateColor.B) * num));
            return Color.FromArgb(alpha, red, green, blue);
        }

        private void DrawBuiltObjectSelection(BuiltObjectList builtObjects, Graphics graphics)
        {
            if (builtObjects.Count <= 0)
            {
                return;
            }
            SolidBrush solidBrush = new SolidBrush(_UnknownColor);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            bool flag = false;
            if (builtObjects[0].Empire != _Game.PlayerEmpire && _Game.PlayerEmpire.EmpiresViewable.Contains(builtObjects[0].Empire))
            {
                flag = true;
            }
            int num = 6;
            int rowHeight = _RowHeight;
            int num2 = 5;
            int num3 = base.ClientRectangle.Width - 10;
            Point point = new Point(num3 - (_FlagSizeSmall.Width - 2), 6);
            if (_EmpirePicture != null)
            {
                graphics.DrawImageUnscaled(_EmpirePicture, point);
            }
            Font titleFont = _TitleFont;
            DrawStringWithDropShadow(location: new Point(num2, num), graphics: graphics, text: "(" + TextResolver.GetText("Multiple Ships") + ")", font: titleFont, brush: new SolidBrush(_EmpireColor));
            num += titleFont.Height + _Height5;
            string empty = string.Empty;
            if (builtObjects[0].Empire == _Game.PlayerEmpire || _Game.GodMode || flag)
            {
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                int num7 = 0;
                for (int i = 0; i < builtObjects.Count; i++)
                {
                    BuiltObject builtObject = builtObjects[i];
                    num4 += builtObject.FirepowerRaw;
                    num5 += builtObject.CalculateAvailableAssaultPodAttackStrength(_Galaxy.CurrentDateTime);
                    if (builtObject.Troops != null)
                    {
                        num6 += builtObject.Troops.Count;
                        num7 += builtObject.Troops.TotalAttackStrength;
                    }
                }
                empty = builtObjects.Count + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture) + ", " + num4 + " " + TextResolver.GetText("Firepower").ToLower(CultureInfo.InvariantCulture);
                string text = empty;
                empty = text + ", " + num5.ToString("0") + " " + TextResolver.GetText("Boarding Strength").ToLower(CultureInfo.InvariantCulture);
                empty = empty + ", " + string.Format(TextResolver.GetText("X troops (Y strength)"), num6.ToString(), num7.ToString());
            }
            else
            {
                empty = builtObjects.Count + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture);
            }
            SizeF sizeF = graphics.MeasureString(empty, _NormalFont, base.ClientSize.Width - (num2 + 10));
            sizeF = new SizeF(sizeF.Width + 2f, sizeF.Height + 2f);
            DrawStringWithDropShadowBounded(location: new Point(num2, num), graphics: graphics, text: empty, font: _NormalFont, size: sizeF);
            num += (int)sizeF.Height;
            num += rowHeight / 4;
            int num8 = 0;
            int num9 = -2;
            int num10 = 0;
            int num11 = 27;
            int num12 = num11;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            while (num8 < builtObjects.Count)
            {
                Image image = _BuiltObjectImages[builtObjects[num8].PictureRef];
                Point point2 = new Point(num2 + num9, num + num10);
                int val = (int)(Math.Sqrt(builtObjects[num8].Size) * 1.25);
                val = Math.Min(val, num11);
                int num13 = (num11 - val) / 2;
                Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
                Rectangle destRect = new Rectangle(num2 + num9 + num13, num + num10 + num13, val, val);
                Rectangle rectangle = new Rectangle(num2 + num9, num + num10, num11, num11);
                if (builtObjects[num8].DamagedComponentCount > 0 && (builtObjects[0].Empire == _Game.PlayerEmpire || _Game.GodMode || flag))
                {
                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, 255, 0, 64)), rectangle);
                }
                graphics.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
                AddHotspot(rectangle, builtObjects[num8], builtObjects[num8].Name + " (" + TextResolver.GetText("click to select") + ")");
                if (builtObjects[0].Empire == _Game.PlayerEmpire || _Game.GodMode || flag)
                {
                    double num14 = builtObjects[num8].CurrentFuel / (double)builtObjects[num8].FuelCapacity;
                    int num15 = (int)(num14 * (double)(num11 - 4));
                    graphics.FillRectangle(rect: new Rectangle(num2 + num9 + num11 - 4, num + num10 + (num11 - 2 - num15), 2, num15), brush: new SolidBrush(Color.Green));
                }
                num8++;
                if (num11 > num12)
                {
                    num12 = num11;
                }
                num9 += num11;
                if (num9 > num3 - num11)
                {
                    num10 += num12;
                    num9 = -2;
                }
            }
            solidBrush.Dispose();
        }
    }
}
