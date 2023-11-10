// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.PersistentGradientPanel
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
  public class PersistentGradientPanel : Panel
  {
    private string _Title;
    private string _Text;
    private Image _Picture;
    private int _PictureSize;
    private bool _AlignPictureRight;
    private Image _SmallPicture;
    private Rectangle _SmallPictureLocation;
    private int _OriginalHeight;
    private Color _BackColour1 = SystemColors.Window;
    private Color _BackColour2 = SystemColors.Window;
    private LinearGradientMode _GradientMode = LinearGradientMode.None;
    private BorderStyle _BorderStyle;
    private Color _BorderColour = SystemColors.WindowFrame;
    private int _BorderWidth = 1;
    private int _Curvature;
    private CornerCurveMode _CurveMode = CornerCurveMode.All;

    private void DesignModeInvalidate()
    {
      if (!this.DesignMode)
        return;
      this.Invalidate();
    }

    [Description("The primary background color used to display text and graphics in the control.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Window")]
    public new Color BackColor
    {
      get => this._BackColour1;
      set
      {
        this._BackColour1 = value;
        this.DesignModeInvalidate();
      }
    }

    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Window")]
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

    [DefaultValue(typeof (LinearGradientMode), "None")]
    [Description("The gradient direction used to paint the control.")]
    [Category("Appearance")]
    public LinearGradientMode GradientMode
    {
      get => this._GradientMode;
      set
      {
        this._GradientMode = value;
        this.DesignModeInvalidate();
      }
    }

    [DefaultValue(typeof (BorderStyle), "None")]
    [Description("The border style used to paint the control.")]
    [Category("Appearance")]
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

    [Description("The width of the border used to paint the control.")]
    [DefaultValue(typeof (int), "1")]
    [Category("Appearance")]
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
    [Description("The radius of the curve used to paint the corners of the control.")]
    [Category("Appearance")]
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
    [Description("The style of the curves to be drawn on the control.")]
    [DefaultValue(typeof (CornerCurveMode), "All")]
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
          adjustedCurve = this._Curvature <= this.ClientRectangle.Width / 2 ? this._Curvature : PersistentGradientPanel.DoubleToInt((double) (this.ClientRectangle.Width / 2));
          if (adjustedCurve > this.ClientRectangle.Height / 2)
            adjustedCurve = PersistentGradientPanel.DoubleToInt((double) (this.ClientRectangle.Height / 2));
        }
        return adjustedCurve;
      }
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg == 277)
      {
        this.Invalidate();
      }
      else
      {
        if (m.Msg != 276)
          return;
        this.Invalidate();
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
      GraphicsPath path = this.GetPath();
      Rectangle clientRectangle = this.ClientRectangle;
      if (this.ClientRectangle.Width == 0)
        ++clientRectangle.Width;
      if (this.ClientRectangle.Height == 0)
        ++clientRectangle.Height;
      LinearGradientBrush linearGradientBrush = this._GradientMode != LinearGradientMode.None ? new LinearGradientBrush(clientRectangle, this._BackColour1, this._BackColour2, (System.Drawing.Drawing2D.LinearGradientMode) this._GradientMode) : new LinearGradientBrush(clientRectangle, this._BackColour1, this._BackColour1, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
      graphics.FillPath((Brush) linearGradientBrush, path);
      linearGradientBrush.Dispose();
      switch (this._BorderStyle)
      {
        case BorderStyle.FixedSingle:
          Pen pen = new Pen(this._BorderColour, (float) this._BorderWidth);
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
                num2 = PersistentGradientPanel.DoubleToInt((double) (this.BorderWidth / 2));
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

    public static int DoubleToInt(double value) => Decimal.ToInt32(Decimal.Floor(Decimal.Parse(value.ToString(), (IFormatProvider) NumberFormatInfo.InvariantInfo)));

    public PersistentGradientPanel()
    {
      this.SetDefaultControlStyles();
      this.CustomInitialisation();
      this.AutoScroll = false;
      this.AutoScrollMargin = new Size(0, 0);
      this.Padding = new Padding(0);
      this.Margin = new Padding(0);
      using (Graphics graphics = this.CreateGraphics())
        this.DrawPanelWithBackground(graphics);
    }

    public void Reset()
    {
      using (Graphics graphics = this.CreateGraphics())
      {
        this.ClearPanel(graphics);
        this.DrawPanelWithBackground(graphics);
      }
    }

    private void ClearPanel(Graphics graphics) => graphics.Clear(Color.Transparent);

    public void Clear()
    {
      this._Title = string.Empty;
      this._Picture = (Image) null;
      this._PictureSize = 0;
      this._SmallPicture = (Image) null;
      this._SmallPictureLocation = new Rectangle(0, 0, 0, 0);
      this._AlignPictureRight = false;
      this.Text = string.Empty;
    }

    public void SetData(
      string title,
      string text,
      Image picture,
      int pictureSize,
      Image smallPicture,
      Rectangle smallPictureLocation,
      bool alignPictureRight)
    {
      bool flag = false;
      if (this._Title != title || this._Text != text || this._Picture != picture || this._PictureSize != pictureSize || this._SmallPicture != smallPicture || this._SmallPictureLocation != smallPictureLocation || this._AlignPictureRight != alignPictureRight)
        flag = true;
      this._Title = title;
      this._Text = text;
      this._Picture = picture;
      this._PictureSize = pictureSize;
      this._SmallPicture = smallPicture;
      this._SmallPictureLocation = smallPictureLocation;
      this._AlignPictureRight = alignPictureRight;
      if (!flag)
        return;
      using (Graphics graphics = this.CreateGraphics())
      {
        this.ClearPanel(graphics);
        this.DrawPanelWithBackground(graphics);
      }
    }

    public string Title
    {
      get => this._Title;
      set
      {
        bool flag = false;
        if (this._Title != value)
          flag = true;
        this._Title = value;
        if (!flag)
          return;
        using (Graphics graphics = this.CreateGraphics())
        {
          this.ClearPanel(graphics);
          this.DrawPanelWithBackground(graphics);
        }
      }
    }

    public override string Text
    {
      get => this._Text;
      set
      {
        bool flag = false;
        if (this._Text != value)
          flag = true;
        this._Text = value;
        if (!flag)
          return;
        using (Graphics graphics = this.CreateGraphics())
        {
          this.ClearPanel(graphics);
          this.DrawPanelWithBackground(graphics);
        }
      }
    }

    public Image Picture
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

    public int PictureSize
    {
      get => this._PictureSize;
      set => this._PictureSize = value;
    }

    public bool AlignPictureRight
    {
      get => this._AlignPictureRight;
      set => this._AlignPictureRight = value;
    }

    public Image SmallPicture
    {
      get => this._SmallPicture;
      set => this._SmallPicture = value;
    }

    public Rectangle SmallPictureLocation
    {
      get => this._SmallPictureLocation;
      set => this._SmallPictureLocation = value;
    }

    private void DrawPanel(object sender, PaintEventArgs pe) => this.DrawPanel(pe.Graphics);

    private void DrawPanelWithBackground(Graphics graphics)
    {
      if (this._CurveMode != CornerCurveMode.None)
        base.OnPaintBackground(new PaintEventArgs(graphics, new Rectangle(0, 0, this.Width, this.Height)));
      this.DrawBackground(graphics);
      this.DrawPanel(graphics);
    }

    internal void DrawPanel(Graphics graphics)
    {
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      Font font = new Font(this.Font.FontFamily, this.Font.Size + 1.5f, FontStyle.Bold);
      this.AutoScrollMargin = new Size(0, 0);
      if (this._Picture != null)
      {
        Rectangle rect = new Rectangle();
        int num1 = this.ClientRectangle.Width - 6;
        int num2 = this.ClientRectangle.Height - 6;
        double num3 = (double) this._Picture.Width / (double) this._Picture.Height;
        int num4;
        int num5;
        if (this._Picture.Width > this._Picture.Height)
        {
          num4 = (int) ((double) this._PictureSize * num3);
          num5 = this._PictureSize;
        }
        else
        {
          num4 = this._PictureSize;
          num5 = (int) ((double) this._PictureSize / num3);
        }
        double num6;
        if (num5 > num2)
        {
          num6 = (double) num2 / (double) num5;
          if ((double) num4 * num6 > (double) num1)
            num6 *= (double) num1 / ((double) num4 * num6);
        }
        else if (num4 > num1)
        {
          num6 = (double) num1 / (double) num4;
          if ((double) num5 * num6 > (double) num2)
            num6 *= (double) num2 / ((double) num5 * num6);
        }
        else
          num6 = 1.0;
        int num7;
        int num8;
        int width;
        if (this._AlignPictureRight)
        {
          num7 = num1 - (int) ((double) num4 * num6);
          num8 = (num2 - (int) ((double) num5 * num6)) / 2;
          width = (int) ((double) num4 * num6);
          if (num1 - num7 - width / 2 < num2 / 2)
            num7 = num1 - (num2 / 2 + width / 2);
        }
        else
        {
          num7 = (num1 - (int) ((double) num4 * num6)) / 2;
          num8 = (num2 - (int) ((double) num5 * num6)) / 2;
          width = (int) ((double) num4 * num6);
        }
        rect = new Rectangle(num7 + 3, num8 + 3, width, (int) ((double) num5 * num6));
        graphics.DrawImage(this._Picture, rect);
        if (this._SmallPicture != null)
          graphics.DrawImage(this._SmallPicture, this._SmallPictureLocation);
      }
      int y = 6;
      if (!string.IsNullOrEmpty(this._Title))
      {
        graphics.DrawString(this._Title, font, (Brush) new SolidBrush(Color.Black), new PointF(7f, (float) (y + 1)));
        graphics.DrawString(this._Title, font, (Brush) new SolidBrush(Color.White), new PointF(6f, (float) y));
        SizeF sizeF = graphics.MeasureString(this._Title, font);
        y += (int) sizeF.Height + 6;
      }
      graphics.DrawString(this._Text, this.Font, (Brush) new SolidBrush(Color.Black), new PointF(7f, (float) (y + 1)));
      graphics.DrawString(this._Text, this.Font, (Brush) new SolidBrush(Color.White), new PointF(6f, (float) y));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawPanel(e.Graphics);
    }
  }
}
