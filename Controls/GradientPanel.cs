// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GradientPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class GradientPanel : Panel
  {
    private Color _BackColour1 = Color.FromArgb(39, 40, 44);
    private Color _BackColour2 = Color.FromArgb(22, 21, 26);
    private Color _BackColour3 = Color.FromArgb(51, 54, 61);
    private LinearGradientMode _GradientMode = LinearGradientMode.Vertical;
    private BorderStyle _BorderStyle = BorderStyle.FixedSingle;
    private Color _BorderColour = Color.FromArgb(67, 67, 77);
    private int _BorderWidth = 1;
    private int _Curvature;
    private CornerCurveMode _CurveMode = CornerCurveMode.All;
    protected IFontCache _FontCache;
    private float _FontSize = 15.33f;
    private bool _FontIsBold;

    public virtual void SetFontCache(IFontCache fontCache)
    {
      this._FontCache = fontCache;
      if ((double) this._FontSize <= 0.0)
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
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
    }

    private void DesignModeInvalidate()
    {
      if (!this.DesignMode)
        return;
      this.Invalidate();
    }

    [DefaultValue(typeof (Color), "Window")]
    [Category("Appearance")]
    [Description("The primary background color used to display text and graphics in the control.")]
    public new Color BackColor
    {
      get => this._BackColour1;
      set
      {
        this._BackColour1 = value;
        this.DesignModeInvalidate();
      }
    }

    [Description("The secondary background color used to paint the control.")]
    [DefaultValue(typeof (Color), "Window")]
    [Category("Appearance")]
    public Color BackColor2
    {
      get => this._BackColour2;
      set
      {
        this._BackColour2 = value;
        this.DesignModeInvalidate();
      }
    }

    [Description("The third background color used to paint the control.")]
    [DefaultValue(typeof (Color), "Window")]
    [Category("Appearance")]
    public Color BackColor3
    {
      get => this._BackColour3;
      set
      {
        this._BackColour3 = value;
        this.DesignModeInvalidate();
      }
    }

    [DefaultValue(typeof (LinearGradientMode), "None")]
    [Category("Appearance")]
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

    [Description("The border style used to paint the control.")]
    [Category("Appearance")]
    [DefaultValue(typeof (BorderStyle), "None")]
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
    [DefaultValue(typeof (Color), "WindowFrame")]
    [Category("Appearance")]
    public Color BorderColor
    {
      get => this._BorderColour;
      set
      {
        this._BorderColour = value;
        this.DesignModeInvalidate();
      }
    }

    [Category("Appearance")]
    [DefaultValue(typeof (int), "1")]
    [Description("The width of the border used to paint the control.")]
    public int BorderWidth
    {
      get => this._BorderWidth;
      set
      {
        this._BorderWidth = value;
        this.DesignModeInvalidate();
      }
    }

    [DefaultValue(typeof (int), "0")]
    [Category("Appearance")]
    [Description("The radius of the curve used to paint the corners of the control.")]
    public int Curvature
    {
      get => this._Curvature;
      set
      {
        this._Curvature = value;
        this.DesignModeInvalidate();
      }
    }

    [Category("Appearance")]
    [DefaultValue(typeof (CornerCurveMode), "All")]
    [Description("The style of the curves to be drawn on the control.")]
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
          adjustedCurve = this._Curvature <= this.ClientRectangle.Width / 2 ? this._Curvature : GradientPanel.DoubleToInt((double) (this.ClientRectangle.Width / 2));
          if (adjustedCurve > this.ClientRectangle.Height / 2)
            adjustedCurve = GradientPanel.DoubleToInt((double) (this.ClientRectangle.Height / 2));
        }
        return adjustedCurve;
      }
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

    private void DrawBackground(Graphics graphics)
    {
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      GraphicsPath path = this.GetPath(this.ClientRectangle);
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
        float[] numArray = new float[3]{ 0.0f, 0.5f, 1f };
        colorBlend.Colors = colorArray;
        colorBlend.Positions = numArray;
        linearGradientBrush = new LinearGradientBrush(clientRectangle, this._BackColour1, this._BackColour2, (System.Drawing.Drawing2D.LinearGradientMode) this._GradientMode);
        linearGradientBrush.InterpolationColors = colorBlend;
      }
      graphics.FillPath((Brush) linearGradientBrush, path);
      linearGradientBrush.Dispose();
      switch (this._BorderStyle)
      {
        case BorderStyle.FixedSingle:
          Color topColor = ControlPaint.Light(this._BorderColour);
          Color borderColour = this._BorderColour;
          Color rightColor = ControlPaint.Dark(this._BorderColour);
          Color bottomColor = ControlPaint.DarkDark(this._BorderColour);
          this.DrawBorderShaded(graphics, topColor, borderColour, rightColor, bottomColor);
          break;
        case BorderStyle.Fixed3D:
          GradientPanel.DrawBorder3D(graphics, this.ClientRectangle);
          break;
      }
      path.Dispose();
    }

    private void DrawBorderShaded(
      Graphics graphics,
      Color topColor,
      Color leftColor,
      Color rightColor,
      Color bottomColor)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
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
            num2 = GradientPanel.DoubleToInt((double) (this.BorderWidth / 2));
          else if (this._BorderWidth == 1)
            num2 = 1;
          num1 = this.AdjustedCurve;
          break;
      }
      Pen pen1 = new Pen(this._BorderColour, (float) this._BorderWidth);
      if (num1 == 0)
      {
        graphics.DrawRectangle(pen1, clientRectangle);
      }
      else
      {
        int num3 = clientRectangle.Width - num2;
        int num4 = clientRectangle.Height - num2;
        int num5 = 0;
        int y1 = 0;
        int num6 = 0;
        int num7 = 0;
        if ((this._CurveMode & CornerCurveMode.TopRight) != CornerCurveMode.None)
          y1 = num1;
        if ((this._CurveMode & CornerCurveMode.BottomRight) != CornerCurveMode.None)
          num6 = num1;
        if ((this._CurveMode & CornerCurveMode.BottomLeft) != CornerCurveMode.None)
          num7 = num1;
        if ((this._CurveMode & CornerCurveMode.TopLeft) != CornerCurveMode.None)
          num5 = num1;
        Pen pen2 = new Pen(Color.Black, 0.5f);
        Pen pen3 = new Pen(topColor, (float) this._BorderWidth);
        Pen pen4 = new Pen(leftColor, (float) this._BorderWidth);
        Pen pen5 = new Pen(rightColor, (float) this._BorderWidth);
        Pen pen6 = new Pen(bottomColor, (float) this._BorderWidth);
        LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(new Rectangle(0, 0, num1, num1), topColor, leftColor, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
        Pen pen7 = new Pen((Brush) linearGradientBrush1, (float) this._BorderWidth);
        LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Rectangle(clientRectangle.Width - num1, 0, num1, num1), topColor, rightColor, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
        Pen pen8 = new Pen((Brush) linearGradientBrush2, (float) this._BorderWidth);
        LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(new Rectangle(0, clientRectangle.Height - num1, num1, num1), leftColor, bottomColor, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
        Pen pen9 = new Pen((Brush) linearGradientBrush3, (float) this._BorderWidth);
        LinearGradientBrush linearGradientBrush4 = new LinearGradientBrush(new Rectangle(clientRectangle.Width - num1, clientRectangle.Height - num1, num1, num1), rightColor, bottomColor, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
        Pen pen10 = new Pen((Brush) linearGradientBrush4, (float) this._BorderWidth);
        int num8 = 0;
        int num9 = (this._CurveMode & CornerCurveMode.TopRight) == CornerCurveMode.None ? 1 : num1 * 2;
        graphics.DrawArc(pen8, num3 - num9, num8, num9, num9, 270, 90);
        graphics.DrawLine(pen5, num3, y1, num3, num4 - num6);
        int num10 = (this._CurveMode & CornerCurveMode.BottomRight) == CornerCurveMode.None ? 1 : num1 * 2;
        graphics.DrawArc(pen10, num3 - num10, num4 - num10, num10, num10, 0, 90);
        graphics.DrawLine(pen6, num7 - 1, num4, num3 - num6 + 1, num4);
        int num11 = (this._CurveMode & CornerCurveMode.BottomLeft) == CornerCurveMode.None ? 1 : num1 * 2;
        graphics.DrawArc(pen9, num8, num4 - num11, num11, num11, 90, 90);
        graphics.DrawLine(pen4, 0.0f, (float) ((double) clientRectangle.Height - (double) num7 + 1.0), 0.0f, (float) num5);
        int num12 = (this._CurveMode & CornerCurveMode.TopLeft) == CornerCurveMode.None ? 1 : num1 * 2;
        graphics.DrawArc(pen7, num8, num8, num12, num12, 180, 90);
        graphics.DrawLine(pen3, (float) num5, 0.0f, (float) ((double) clientRectangle.Width - (double) y1 + 1.0), 0.0f);
        pen2.Dispose();
        pen3.Dispose();
        pen4.Dispose();
        pen5.Dispose();
        pen6.Dispose();
        linearGradientBrush1.Dispose();
        pen7.Dispose();
        linearGradientBrush2.Dispose();
        pen8.Dispose();
        linearGradientBrush3.Dispose();
        pen9.Dispose();
        linearGradientBrush4.Dispose();
        pen10.Dispose();
      }
    }

    private static GraphicsPath CreateRoundRectangleCorrected(RectangleF rectangle, float radius)
    {
      GraphicsPath rectangleCorrected = new GraphicsPath();
      float left = rectangle.Left;
      float top = rectangle.Top;
      float width = rectangle.Width;
      float height = rectangle.Height;
      float num1 = radius * 2f;
      float num2 = num1 + 1f;
      float num3 = radius;
      rectangleCorrected.AddArc(left, top, num1, num1, 180f, 90f);
      rectangleCorrected.AddLine(left + num3, top + 1f, left + width - num3, top + 1f);
      rectangleCorrected.AddArc(left + width - num2, top, num1, num1, 270f, 90f);
      rectangleCorrected.AddLine((float) ((double) left + (double) width - 1.0), top + num3, (float) ((double) left + (double) width - 1.0), top + height - num3);
      rectangleCorrected.AddArc(left + width - num2, top + height - num2, num1, num1, 0.0f, 90f);
      rectangleCorrected.AddLine(left + width - num3, (float) ((double) top + (double) height - 1.0), left + num3, (float) ((double) top + (double) height - 1.0));
      rectangleCorrected.AddArc(left, top + height - num2, num1, num1, 90f, 90f);
      rectangleCorrected.AddLine(left + 1f, top + height - num3, left + 1f, top + num3);
      rectangleCorrected.CloseFigure();
      return rectangleCorrected;
    }

    private GraphicsPath DefineOutline(Rectangle rect)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      int num1 = 0;
      float num2 = ((float) this.Curvature + 1f) * 2f;
      switch (this._BorderStyle)
      {
        case BorderStyle.None:
          num1 = this.AdjustedCurve;
          break;
        case BorderStyle.FixedSingle:
          if (this._BorderWidth > 1)
          {
            GradientPanel.DoubleToInt((double) (this.BorderWidth / 2));
          }
          else
          {
            int borderWidth = this._BorderWidth;
          }
          num1 = this.AdjustedCurve;
          break;
      }
      if (num1 == 0)
      {
        graphicsPath.AddRectangle(rect);
      }
      else
      {
        int width = rect.Width;
        int height = rect.Height;
        int num3 = (this._CurveMode & CornerCurveMode.TopRight) == CornerCurveMode.None ? 1 : num1 * 2;
        graphicsPath.AddArc(width - num3, 0, num3, num3, 270f, 90f);
        int num4 = (this._CurveMode & CornerCurveMode.BottomRight) == CornerCurveMode.None ? 1 : num1 * 2;
        graphicsPath.AddArc(width - num4, height - num4, num4, num4, 0.0f, 90f);
        int num5 = (this._CurveMode & CornerCurveMode.BottomLeft) == CornerCurveMode.None ? 1 : num1 * 2;
        graphicsPath.AddArc(0.0f, (float) height - ((float) num5 + 1f), (float) num5, (float) num5, 90f, 90f);
        int num6 = (this._CurveMode & CornerCurveMode.TopLeft) == CornerCurveMode.None ? 1 : num1 * 2;
        graphicsPath.AddArc(0, 0, num6, num6, 180f, 90f);
        graphicsPath.CloseFigure();
      }
      return graphicsPath;
    }

    public void DoRegionClip()
    {
      RectangleF rectangleF = new RectangleF((float) this.ClientRectangle.X, (float) this.ClientRectangle.Y, (float) this.ClientRectangle.Width, (float) this.ClientRectangle.Height);
      int num = Math.Max(0, this.BorderWidth - 1);
      rectangleF.Inflate((float) num, (float) num);
      using (GraphicsPath path = this.DefineOutline(new Rectangle((int) rectangleF.X, (int) rectangleF.Y, (int) rectangleF.Width, (int) rectangleF.Height)))
        this.Region = new Region(path);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      if (this.BackgroundImage != null)
      {
        this.DrawBackground(e.Graphics);
        base.OnPaintBackground(e);
      }
      else
      {
        base.OnPaintBackground(e);
        this.DrawBackground(e.Graphics);
      }
    }

    protected GraphicsPath GetPath(Rectangle rectangle)
    {
      GraphicsPath path = new GraphicsPath();
      if (this._BorderStyle == BorderStyle.Fixed3D)
      {
        path.AddRectangle(rectangle);
      }
      else
      {
        try
        {
          int num1 = 0;
          Rectangle rect = rectangle;
          int num2 = 0;
          switch (this._BorderStyle)
          {
            case BorderStyle.None:
              num1 = this.AdjustedCurve;
              break;
            case BorderStyle.FixedSingle:
              if (this._BorderWidth > 1)
                num2 = GradientPanel.DoubleToInt((double) (this.BorderWidth / 2));
              else if (this._BorderWidth == 1)
                num2 = 1;
              num1 = this.AdjustedCurve;
              break;
          }
          if (num1 == 0)
          {
            path.AddRectangle(rect);
          }
          else
          {
            int num3 = rect.Width - num2;
            int num4 = rect.Height - num2;
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
          path.AddRectangle(rectangle);
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

    public static int DoubleToInt(double value) => Decimal.ToInt32(Decimal.Floor(Decimal.Parse(value.ToString(), (IFormatProvider) NumberFormatInfo.InvariantInfo)));

    public GradientPanel()
    {
      this.SetDefaultControlStyles();
      this.CustomInitialisation();
      this.DoubleBuffered = true;
      this.AutoScroll = false;
      this.AutoScrollMargin = new Size(0, 0);
      this.Padding = new Padding(0);
      this.Margin = new Padding(0);
      using (Graphics graphics = this.CreateGraphics())
        this.DrawPanelBackground(graphics);
    }

    public void Clear(Graphics graphics)
    {
      this.ClearPanel(graphics);
      this.DrawPanelBackground(graphics);
    }

    private void ClearPanel(Graphics graphics) => graphics.Clear(Color.Transparent);

    public void DrawPanelBackground(Graphics graphics)
    {
      if (this._CurveMode != CornerCurveMode.None)
        base.OnPaintBackground(new PaintEventArgs(graphics, new Rectangle(0, 0, this.Width, this.Height)));
      this.DrawBackground(graphics);
    }
  }
}
