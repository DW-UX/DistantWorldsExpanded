// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GlassButton
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Media;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DistantWorlds.Controls
{
  [ToolboxItem(true)]
  [ToolboxItemFilter("System.Windows.Forms")]
  [Description("Raises an event when the user clicks it.")]
  [ToolboxBitmap(typeof (GlassButton))]
  public class GlassButton : System.Windows.Forms.Button
  {
    private const int FRAME_DISABLED = 0;
    private const int FRAME_PRESSED = 1;
    private const int FRAME_NORMAL = 2;
    private const int FRAME_ANIMATED = 3;
    private const int animationLength = 200;
    private const int framesCount = 10;
    private Color _TextColor;
    private Color _TextColor2;
    private Color _ClipColor = Color.FromArgb(48, 48, 48);
    private bool _ClipBackground;
    private bool _IntensifyColors;
    private bool _ToggledOn;
    private static string _SoundLocation;
    private static SoundPlayer _SoundPlayer;
    private static object _SoundLock = new object();
    public static double Volume = 1.0;
    public string MinorText;
    public Font MinorFont;
    private bool _DelayFrameRefresh;
    private Color _BackColor;
    private Color _InnerBorderColor;
    private Color _OuterBorderColor;
    private Color _ShineColor;
    private Color _GlowColor;
    private bool fadeOnFocus;
    private bool isHovered;
    private bool isFocused;
    private bool isFocusedByKey;
    private bool isKeyDown;
    private bool isMouseDown;
    public bool EmphasizeDisabled;
    public string Hint = string.Empty;
    private System.Windows.Forms.Button imageButton;
    public bool CornerCurvedTopLeft = true;
    public bool CornerCurvedTopRight = true;
    public bool CornerCurvedBottomRight = true;
    public bool CornerCurvedBottomLeft = true;
    private List<Image> frames;
    private int currentFrame;
    private int direction;
    private IContainer components;
    private System.Timers.Timer timer;

    public bool ToggledOn
    {
      get => this._ToggledOn;
      set
      {
        this._ToggledOn = value;
        if (this._ToggledOn)
        {
          this.isHovered = true;
          this.FadeIn();
          this.Invalidate();
        }
        else
        {
          this.isHovered = false;
          this.FadeOut();
          this.Invalidate();
        }
      }
    }

    private static void PlaySound()
    {
      lock (GlassButton._SoundLock)
      {
        if (string.IsNullOrEmpty(GlassButton._SoundLocation) || GlassButton._SoundPlayer == null || GlassButton.Volume <= 0.0)
          return;
        GlassButton._SoundPlayer.Play();
      }
    }

    public void SetRegion(GraphicsPath gp)
    {
      Region region = this.Region;
      this.Region = new Region(gp);
      region?.Dispose();
    }

    public static void SetSoundLocation(string soundLocation)
    {
      if (string.IsNullOrEmpty(soundLocation))
        return;
      SoundPlayer soundPlayer1 = GlassButton._SoundPlayer;
      GlassButton._SoundLocation = soundLocation;
      SoundPlayer soundPlayer2 = new SoundPlayer(soundLocation);
      soundPlayer2.Load();
      GlassButton._SoundPlayer = soundPlayer2;
      soundPlayer1?.Dispose();
    }

    protected override void OnClick(EventArgs e)
    {
      this.isKeyDown = this.isMouseDown = false;
      GlassButton.PlaySound();
      base.OnClick(e);
    }

    public GlassButton()
    {
      this.InitializeComponent();
      this.timer.Interval = 20.0;
      base.BackColor = Color.FromArgb(0, 0, 20);
      this.BackColor = Color.FromArgb(20, 20, 40);
      this.ForeColor = Color.FromArgb(120, 120, 120);
      this._TextColor = this.ForeColor;
      this._TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.OuterBorderColor = Color.FromArgb(0, 0, 20);
      this.InnerBorderColor = Color.FromArgb(48, 48, 68);
      this.ShineColor = Color.FromArgb(128, 128, 148);
      this.GlowColor = Color.FromArgb(80, 80, 160);
      this.SetFont();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Opaque, false);
    }

    private void SetFont() => this.SetFont(true);

    private void SetFont(bool resetRegion)
    {
      this.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, GraphicsUnit.Pixel);
      this.MinorFont = new Font(this.Font.FontFamily, Math.Max(3f, this.Font.Size - 2f), FontStyle.Regular, GraphicsUnit.Pixel);
      if (!resetRegion)
        return;
      this.ResetDefaultColors(resetRegion);
    }

    public void ResetDefaultColors(bool resetRegion) => this.SetColors(resetRegion, Color.FromArgb(0, 0, 0), Color.FromArgb(150, 150, 150), Color.FromArgb(0, 0, 16), Color.FromArgb(67, 67, 77), Color.FromArgb(112, 112, 128), Color.FromArgb(48, 48, 128), Color.FromArgb(128, 128, (int) byte.MaxValue));

    public void SetColors(
      bool resetRegion,
      Color backColor,
      Color foreColor,
      Color outerBorderColor,
      Color innerBorderColor,
      Color shineColor,
      Color glowColor,
      Color intenseGlowColor)
    {
      this.ForeColor = foreColor;
      if (!resetRegion && this.frames != null && this._BackColor.Equals((object) backColor) && this._OuterBorderColor.Equals((object) outerBorderColor) && this._InnerBorderColor.Equals((object) innerBorderColor) && this._ShineColor.Equals((object) shineColor) && (this._IntensifyColors || this._GlowColor.Equals((object) glowColor)) && (!this._IntensifyColors || this._GlowColor.Equals((object) intenseGlowColor)))
        return;
      this.DelayFrameRefresh = true;
      base.BackColor = Color.Transparent;
      this.BackColor = backColor;
      this.OuterBorderColor = outerBorderColor;
      this.InnerBorderColor = innerBorderColor;
      this.ShineColor = shineColor;
      this.SetGlowColor(glowColor, resetRegion);
      if (this._IntensifyColors)
        this.SetGlowColor(intenseGlowColor, resetRegion);
      this.DelayFrameRefresh = false;
    }

    public bool IntensifyColors
    {
      get => this._IntensifyColors;
      set
      {
        if (this._IntensifyColors == value)
          return;
        this._IntensifyColors = value;
        this.GlowColor = !this._IntensifyColors ? Color.FromArgb(48, 48, 128) : Color.FromArgb(128, 128, (int) byte.MaxValue);
        this.CreateFrames();
      }
    }

    public bool ClipBackground
    {
      get => this._ClipBackground;
      set
      {
        if (this._ClipBackground == value)
          return;
        this._ClipBackground = value;
        this.CreateFrames();
      }
    }

    public void ClearText() => base.Text = string.Empty;

    public new string Text
    {
      get => base.Text;
      set => base.Text = value;
    }

    public void SetText(string text)
    {
      base.Text = text;
      this.SetFont(false);
    }

    public new Point Location
    {
      get => base.Location;
      set
      {
        base.Location = value;
        this.SetFont();
      }
    }

    public new Size Size
    {
      get => base.Size;
      set
      {
        Size size = base.Size;
        base.Size = value;
        if (!size.Equals((object) value))
          return;
        this.ResetDefaultColors(true);
      }
    }

    public bool DelayFrameRefresh
    {
      get => this._DelayFrameRefresh;
      set
      {
        if (!value)
        {
          this.CreateFrames();
          if (this.IsHandleCreated)
            this.Invalidate();
        }
        this._DelayFrameRefresh = value;
      }
    }

    public void SetBackColor(Color color, bool resetRegion)
    {
      if (this._BackColor.Equals((object) color))
        return;
      this._BackColor = color;
      this.UseVisualStyleBackColor = false;
      if (!this._DelayFrameRefresh)
        this.CreateFrames(false, resetRegion);
      this.OnBackColorChanged(EventArgs.Empty);
    }

    public new virtual Color BackColor
    {
      get => this._BackColor;
      set
      {
        if (this._BackColor.Equals((object) value))
          return;
        this._BackColor = value;
        this.UseVisualStyleBackColor = false;
        if (!this._DelayFrameRefresh)
          this.CreateFrames();
        this.OnBackColorChanged(EventArgs.Empty);
      }
    }

    public new virtual Color ForeColor
    {
      get => base.ForeColor;
      set => base.ForeColor = value;
    }

    public virtual Color TextColor
    {
      get => this._TextColor;
      set => this._TextColor = value;
    }

    public virtual Color TextColor2
    {
      get => this._TextColor2;
      set => this._TextColor2 = value;
    }

    public override Image BackgroundImage
    {
      get => base.BackgroundImage;
      set => base.BackgroundImage = value;
    }

    public override ImageLayout BackgroundImageLayout
    {
      get => base.BackgroundImageLayout;
      set => base.BackgroundImageLayout = value;
    }

    [Category("Appearance")]
    [Description("The inner border color of the control.")]
    public virtual Color InnerBorderColor
    {
      get => this._InnerBorderColor;
      set
      {
        if (this._InnerBorderColor.Equals((object) value))
          return;
        this._InnerBorderColor = value;
        if (!this._DelayFrameRefresh)
        {
          this.CreateFrames();
          if (this.IsHandleCreated)
            this.Invalidate();
        }
        this.OnInnerBorderColorChanged(EventArgs.Empty);
      }
    }

    public void SetOuterBorderColor(Color color, bool resetRegion)
    {
      if (this._OuterBorderColor.Equals((object) color))
        return;
      this._OuterBorderColor = color;
      if (!this._DelayFrameRefresh)
      {
        this.CreateFrames(false, resetRegion);
        if (this.IsHandleCreated)
          this.Invalidate();
      }
      this.OnOuterBorderColorChanged(EventArgs.Empty);
    }

    [Category("Appearance")]
    [Description("The outer border color of the control.")]
    public virtual Color OuterBorderColor
    {
      get => this._OuterBorderColor;
      set
      {
        if (this._OuterBorderColor.Equals((object) value))
          return;
        this._OuterBorderColor = value;
        if (!this._DelayFrameRefresh)
        {
          this.CreateFrames();
          if (this.IsHandleCreated)
            this.Invalidate();
        }
        this.OnOuterBorderColorChanged(EventArgs.Empty);
      }
    }

    public void SetShineColor(Color color, bool resetRegion)
    {
      if (this._ShineColor.Equals((object) color))
        return;
      this._ShineColor = color;
      if (!this._DelayFrameRefresh)
      {
        this.CreateFrames(false, resetRegion);
        if (this.IsHandleCreated)
          this.Invalidate();
      }
      this.OnShineColorChanged(EventArgs.Empty);
    }

    [Description("The shine color of the control.")]
    [Category("Appearance")]
    public virtual Color ShineColor
    {
      get => this._ShineColor;
      set
      {
        if (this._ShineColor.Equals((object) value))
          return;
        this._ShineColor = value;
        if (!this._DelayFrameRefresh)
        {
          this.CreateFrames();
          if (this.IsHandleCreated)
            this.Invalidate();
        }
        this.OnShineColorChanged(EventArgs.Empty);
      }
    }

    private void SetGlowColor(Color color, bool resetRegion)
    {
      if (this._GlowColor.Equals((object) color))
        return;
      this._GlowColor = color;
      if (!this._DelayFrameRefresh)
      {
        this.CreateFrames(false, resetRegion);
        if (this.IsHandleCreated)
          this.Invalidate();
      }
      this.OnGlowColorChanged(EventArgs.Empty);
    }

    [Category("Appearance")]
    [Description("The glow color of the control.")]
    public virtual Color GlowColor
    {
      get => this._GlowColor;
      set
      {
        if (this._GlowColor.Equals((object) value))
          return;
        this._GlowColor = value;
        if (!this._DelayFrameRefresh)
        {
          this.CreateFrames();
          if (this.IsHandleCreated)
            this.Invalidate();
        }
        this.OnGlowColorChanged(EventArgs.Empty);
      }
    }

    [Description("Indicates whether the button should fade in and fade out when it is getting and loosing the focus.")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public virtual bool FadeOnFocus
    {
      get => this.fadeOnFocus;
      set
      {
        if (this.fadeOnFocus == value)
          return;
        this.fadeOnFocus = value;
      }
    }

    private bool isPressed
    {
      get
      {
        if (this.isKeyDown)
          return true;
        return this.isMouseDown && this.isHovered;
      }
    }

    [Browsable(false)]
    public PushButtonState State
    {
      get
      {
        if (!this.Enabled)
          return PushButtonState.Disabled;
        if (this.isPressed)
          return PushButtonState.Pressed;
        if (this.isHovered)
          return PushButtonState.Hot;
        return this.isFocused || this.IsDefault ? PushButtonState.Default : PushButtonState.Normal;
      }
    }

    [Description("Event raised when the value of the InnerBorderColor property is changed.")]
    [Category("Property Changed")]
    public event EventHandler InnerBorderColorChanged;

    protected virtual void OnInnerBorderColorChanged(EventArgs e)
    {
      if (this.InnerBorderColorChanged == null)
        return;
      this.InnerBorderColorChanged((object) this, e);
    }

    [Category("Property Changed")]
    [Description("Event raised when the value of the OuterBorderColor property is changed.")]
    public event EventHandler OuterBorderColorChanged;

    protected virtual void OnOuterBorderColorChanged(EventArgs e)
    {
      if (this.OuterBorderColorChanged == null)
        return;
      this.OuterBorderColorChanged((object) this, e);
    }

    [Category("Property Changed")]
    [Description("Event raised when the value of the ShineColor property is changed.")]
    public event EventHandler ShineColorChanged;

    protected virtual void OnShineColorChanged(EventArgs e)
    {
      if (this.ShineColorChanged == null)
        return;
      this.ShineColorChanged((object) this, e);
    }

    [Description("Event raised when the value of the GlowColor property is changed.")]
    [Category("Property Changed")]
    public event EventHandler GlowColorChanged;

    protected virtual void OnGlowColorChanged(EventArgs e)
    {
      if (this.GlowColorChanged == null)
        return;
      this.GlowColorChanged((object) this, e);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      this.CreateFrames();
      base.OnSizeChanged(e);
    }

    protected override void OnEnter(EventArgs e)
    {
      this.isFocused = this.isFocusedByKey = true;
      base.OnEnter(e);
      if (!this.fadeOnFocus)
        return;
      this.FadeIn();
    }

    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      this.isFocused = this.isFocusedByKey = this.isKeyDown = this.isMouseDown = false;
      this.Invalidate();
      if (!this.fadeOnFocus)
        return;
      this.FadeOut();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (!this.isMouseDown && e.Button == MouseButtons.Left)
      {
        this.isMouseDown = true;
        this.isFocusedByKey = false;
        this.Invalidate();
      }
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.isMouseDown)
      {
        this.isMouseDown = false;
        this.Invalidate();
      }
      base.OnMouseUp(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (e.Button == MouseButtons.None)
        return;
      if (!this.ClientRectangle.Contains(e.X, e.Y))
      {
        if (!this.isHovered || this._ToggledOn)
          return;
        this.isHovered = false;
        this.Invalidate();
      }
      else
      {
        if (this.isHovered || this._ToggledOn)
          return;
        this.isHovered = true;
        this.Invalidate();
      }
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (!this._ToggledOn)
      {
        this.isHovered = true;
        this.FadeIn();
        this.Invalidate();
      }
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (!this._ToggledOn)
      {
        this.isHovered = false;
        this.FadeOut();
        this.Invalidate();
      }
      base.OnMouseLeave(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      this.DrawButtonBackgroundFromBuffer(e.Graphics);
      this.DrawForegroundFromButton(e);
      this.DrawButtonForeground(e.Graphics);
      if (this.Paint == null)
        return;
      this.Paint((object) this, e);
    }

    public new event PaintEventHandler Paint;

    private void DrawButtonBackgroundFromBuffer(Graphics graphics)
    {
      int index;
      if (!this.Enabled)
        index = 0;
      else if (this.isPressed)
        index = 1;
      else if (!this.isAnimating && this.currentFrame == 0)
      {
        index = 2;
      }
      else
      {
        if (!this.HasAnimationFrames)
          this.CreateFrames(true);
        index = 3 + this.currentFrame;
      }
      if (this.frames == null || this.frames.Count == 0)
        this.CreateFrames();
      if (this.frames.Count <= index)
        return;
      graphics.DrawImage(this.frames[index], Point.Empty);
    }

    public Image CreateBackgroundFrame(
      bool pressed,
      bool hovered,
      bool animating,
      bool enabled,
      float glowOpacity)
    {
      Rectangle clientRectangle = this.ClientRectangle;
      if (clientRectangle.Width <= 0)
        clientRectangle.Width = 1;
      if (clientRectangle.Height <= 0)
        clientRectangle.Height = 1;
      Image image = (Image) new Bitmap(clientRectangle.Width, clientRectangle.Height);
      using (Graphics g = Graphics.FromImage(image))
      {
        if (this._ClipBackground)
          g.Clear(Color.Black);
        else
          g.Clear(Color.Transparent);
        int radius = 7;
        if (this.Height < 30)
          radius = 6;
        GlassButton.DrawButtonBackground(g, clientRectangle, pressed, hovered, animating, enabled, this.EmphasizeDisabled, this._OuterBorderColor, this._BackColor, this._GlowColor, this._ShineColor, this._InnerBorderColor, glowOpacity, radius, this._ClipBackground, this._ClipColor, this);
      }
      return image;
    }

    private static void DrawButtonBackground(
      Graphics g,
      Rectangle rectangle,
      bool pressed,
      bool hovered,
      bool animating,
      bool enabled,
      bool emphasizeDisabled,
      Color outerBorderColor,
      Color backColor,
      Color glowColor,
      Color shineColor,
      Color innerBorderColor,
      float glowOpacity,
      int radius,
      bool clipBackground,
      Color clipColor,
      GlassButton instance)
    {
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rectangle1 = rectangle;
      --rectangle1.Width;
      --rectangle1.Height;
      using (GraphicsPath roundRectangle = GlassButton.CreateRoundRectangle(rectangle1, radius, instance))
      {
        if (clipBackground)
        {
          using (Pen pen = new Pen(Color.Black))
            g.DrawPath(pen, roundRectangle);
        }
        else
        {
          using (Pen pen = new Pen(outerBorderColor))
            g.DrawPath(pen, roundRectangle);
        }
      }
      ++rectangle1.X;
      ++rectangle1.Y;
      rectangle1.Width -= 2;
      rectangle1.Height -= 2;
      Rectangle rectangle2 = rectangle1;
      rectangle2.Height >>= 1;
      using (GraphicsPath roundRectangle = GlassButton.CreateRoundRectangle(rectangle1, radius - 2, instance))
      {
        using (Brush brush = (Brush) new SolidBrush(outerBorderColor))
          g.FillPath(brush, roundRectangle);
        if (!enabled)
        {
          if (emphasizeDisabled)
          {
            using (HatchBrush hatchBrush = new HatchBrush(HatchStyle.Percent50, Color.FromArgb(1, shineColor)))
              g.FillPath((Brush) hatchBrush, roundRectangle);
          }
        }
      }
      if ((hovered || animating) && !pressed)
      {
        using (GraphicsPath roundRectangle = GlassButton.CreateRoundRectangle(rectangle1, radius - 2, instance))
        {
          g.SetClip(roundRectangle, CombineMode.Intersect);
          using (GraphicsPath bottomRadialPath = GlassButton.CreateBottomRadialPath(rectangle1))
          {
            using (PathGradientBrush pathGradientBrush = new PathGradientBrush(bottomRadialPath))
            {
              int alpha = (int) (178.0 * (double) glowOpacity + 0.5);
              RectangleF bounds = bottomRadialPath.GetBounds();
              pathGradientBrush.CenterPoint = new PointF((float) (((double) bounds.Left + (double) bounds.Right) / 2.0), (float) (((double) bounds.Top + (double) bounds.Bottom) / 2.0));
              pathGradientBrush.CenterColor = Color.FromArgb(alpha, glowColor);
              pathGradientBrush.SurroundColors = new Color[1]
              {
                Color.FromArgb(0, glowColor)
              };
              g.FillPath((Brush) pathGradientBrush, bottomRadialPath);
            }
          }
          g.ResetClip();
        }
      }
      if (rectangle2.Width > 0 && rectangle2.Height > 0)
      {
        ++rectangle2.Height;
        using (GraphicsPath topRoundRectangle = GlassButton.CreateTopRoundRectangle(rectangle2, radius - 2, instance))
        {
          ++rectangle2.Height;
          int alpha = 153;
          if (pressed | !enabled)
            alpha = (int) (0.40000000596046448 * (double) alpha + 0.5);
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle2, Color.FromArgb(alpha, shineColor), Color.FromArgb(alpha / 3, shineColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            g.FillPath((Brush) linearGradientBrush, topRoundRectangle);
        }
        rectangle2.Height -= 2;
      }
      using (GraphicsPath roundRectangle = GlassButton.CreateRoundRectangle(rectangle1, radius - 1, instance))
      {
        using (Pen pen = new Pen(innerBorderColor))
          g.DrawPath(pen, roundRectangle);
      }
      g.SmoothingMode = smoothingMode;
    }

    private void DrawButtonForeground(Graphics g)
    {
      if (!this.Focused || !this.ShowFocusCues)
        return;
      Rectangle clientRectangle = this.ClientRectangle;
      clientRectangle.Inflate(-4, -4);
      ControlPaint.DrawFocusRectangle(g, clientRectangle);
    }

    private void DrawForegroundFromButton(PaintEventArgs pevent)
    {
      if (this.imageButton == null)
      {
        this.imageButton = new System.Windows.Forms.Button();
        this.imageButton.Parent = (Control) new GlassButton.TransparentControl();
        this.imageButton.SuspendLayout();
        this.imageButton.BackColor = Color.Transparent;
        this.imageButton.FlatAppearance.BorderSize = 0;
        this.imageButton.FlatStyle = FlatStyle.Flat;
      }
      else
        this.imageButton.SuspendLayout();
      this.imageButton.AutoEllipsis = this.AutoEllipsis;
      if (this.Enabled || this.EmphasizeDisabled)
      {
        double num = !this.isPressed ? (this.isAnimating || this.currentFrame != 0 ? (double) this.currentFrame / 9.0 : 0.0) : 1.0;
        int val2_1 = (int) this._TextColor.R + (int) ((double) ((int) this._TextColor2.R - (int) this._TextColor.R) * num);
        int val2_2 = (int) this._TextColor.G + (int) ((double) ((int) this._TextColor2.G - (int) this._TextColor.G) * num);
        int val2_3 = (int) this._TextColor.B + (int) ((double) ((int) this._TextColor2.B - (int) this._TextColor.B) * num);
        this.imageButton.ForeColor = Color.FromArgb(Math.Max(0, Math.Min((int) byte.MaxValue, val2_1)), Math.Max(0, Math.Min((int) byte.MaxValue, val2_2)), Math.Max(0, Math.Min((int) byte.MaxValue, val2_3)));
      }
      else
        this.imageButton.ForeColor = Color.FromArgb(3 * (int) this.ForeColor.R + (int) this._BackColor.R >> 2, 3 * (int) this.ForeColor.G + (int) this._BackColor.G >> 2, 3 * (int) this.ForeColor.B + (int) this._BackColor.B >> 2);
      this.imageButton.Font = this.Font;
      this.imageButton.RightToLeft = this.RightToLeft;
      if (this.Image != null)
      {
        this.imageButton.Image = this.Image;
        if (!this.Enabled)
        {
          Size size = this.Image.Size;
          float[][] newColorMatrix1 = new float[5][]
          {
            new float[5]{ 0.2125f, 0.2125f, 0.2125f, 0.0f, 0.0f },
            new float[5]{ 0.2577f, 0.2577f, 0.2577f, 0.0f, 0.0f },
            new float[5]{ 0.0361f, 0.0361f, 0.0361f, 0.0f, 0.0f },
            null,
            null
          };
          float[] numArray = new float[5]
          {
            0.0f,
            0.0f,
            0.0f,
            1f,
            0.0f
          };
          newColorMatrix1[3] = numArray;
          newColorMatrix1[4] = new float[5]
          {
            0.38f,
            0.38f,
            0.38f,
            0.0f,
            1f
          };
          ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix1);
          ImageAttributes imageAttr = new ImageAttributes();
          imageAttr.ClearColorKey();
          imageAttr.SetColorMatrix(newColorMatrix2);
          this.imageButton.Image = (Image) new Bitmap(this.Image.Width, this.Image.Height);
          using (Graphics graphics = Graphics.FromImage(this.imageButton.Image))
            graphics.DrawImage(this.Image, new Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, imageAttr);
        }
      }
      this.imageButton.ImageAlign = this.ImageAlign;
      this.imageButton.ImageIndex = this.ImageIndex;
      // this.imageButton.ImageKey = this.ImageKey;
      // this.imageButton.ImageList = this.ImageList;
      this.imageButton.Padding = this.Padding;
      this.imageButton.Size = this.Size;
      if (!string.IsNullOrEmpty(this.MinorText))
      {
        this.imageButton.Text = string.Empty;
        this.DrawText(pevent.Graphics);
      }
      else
        this.imageButton.Text = this.Text;
      this.imageButton.TextAlign = this.TextAlign;
      this.imageButton.TextImageRelation = this.TextImageRelation;
      this.imageButton.UseCompatibleTextRendering = this.UseCompatibleTextRendering;
      this.imageButton.UseMnemonic = this.UseMnemonic;
      this.imageButton.ResumeLayout();
      this.InvokePaint((Control) this.imageButton, pevent);
      if (!this.EmphasizeDisabled)
        return;
      this.DrawText(pevent.Graphics);
    }

    private void DrawText(Graphics graphics)
    {
      if (string.IsNullOrEmpty(this.Text))
        return;
      StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox);
      format.Trimming = StringTrimming.EllipsisCharacter;
      SizeF sizeF1 = graphics.MeasureString(this.Text, this.Font, this.Width, format);
      SizeF sizeF2 = SizeF.Empty;
      if (!string.IsNullOrEmpty(this.MinorText))
        sizeF2 = graphics.MeasureString(this.MinorText, this.MinorFont, this.Width, format);
      float height = sizeF1.Height;
      if (!string.IsNullOrEmpty(this.MinorText))
        height += 1f + sizeF2.Height;
      if (this.imageButton == null)
        return;
      using (SolidBrush solidBrush1 = new SolidBrush(this.imageButton.ForeColor))
      {
        using (SolidBrush solidBrush2 = new SolidBrush(Color.Black))
        {
          float x1 = (float) (((double) this.Width - (double) sizeF1.Width) / 2.0);
          float y1 = (float) (((double) this.Height - (double) height) / 2.0);
          if (this.EmphasizeDisabled)
          {
            RectangleF layoutRectangle = new RectangleF(x1 + 1f, y1 + 1f, sizeF1.Width + 3f, sizeF1.Height + 3f);
            graphics.DrawString(this.Text, this.Font, (Brush) solidBrush2, layoutRectangle, format);
          }
          RectangleF layoutRectangle1 = new RectangleF(x1, y1, sizeF1.Width + 3f, sizeF1.Height + 3f);
          graphics.DrawString(this.Text, this.Font, (Brush) solidBrush1, layoutRectangle1, format);
          if (string.IsNullOrEmpty(this.MinorText))
            return;
          float x2 = (float) (((double) this.Width - (double) sizeF2.Width) / 2.0);
          float y2 = (float) (((double) this.Height - (double) height) / 2.0 + (double) sizeF1.Height + 1.0);
          if (this.EmphasizeDisabled)
          {
            RectangleF layoutRectangle2 = new RectangleF(x2 + 1f, y2 + 1f, sizeF2.Width + 3f, sizeF2.Height + 3f);
            graphics.DrawString(this.MinorText, this.MinorFont, (Brush) solidBrush2, layoutRectangle2, format);
          }
          RectangleF layoutRectangle3 = new RectangleF(x2, y2, sizeF2.Width + 3f, sizeF2.Height + 3f);
          graphics.DrawString(this.MinorText, this.MinorFont, (Brush) solidBrush1, layoutRectangle3, format);
        }
      }
    }

    private static GraphicsPath CreateRoundRectangle(
      Rectangle rectangle,
      int radius,
      GlassButton instance)
    {
      return GlassButton.CreateRoundRectangle(new RectangleF((float) rectangle.X, (float) rectangle.Y, (float) rectangle.Width, (float) rectangle.Height), (float) radius, instance);
    }

    private static GraphicsPath CreateRoundRectangle(
      RectangleF rectangle,
      float radius,
      GlassButton instance)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      double left = (double) rectangle.Left;
      double top = (double) rectangle.Top;
      double width = (double) rectangle.Width;
      double height = (double) rectangle.Height;
      float diameter = radius * 2f;
      return GlassButton.DefineOutline(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, diameter, radius, 0.0f, instance);
    }

    private static GraphicsPath DefineOutline(
      float left,
      float top,
      float width,
      float height,
      float diameter,
      float radius,
      float correctionAmount,
      GlassButton instance)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      if (instance.CornerCurvedTopLeft)
      {
        graphicsPath.AddArc(left, top, diameter, diameter, 180f, 90f);
        if (instance.CornerCurvedTopRight)
          graphicsPath.AddLine(left + radius, top + correctionAmount, left + width - radius, top + correctionAmount);
        else
          graphicsPath.AddLine(left + (radius + correctionAmount), top + correctionAmount, left + width, top + correctionAmount);
      }
      else if (instance.CornerCurvedTopRight)
        graphicsPath.AddLine(left, top + correctionAmount, (float) ((double) left + (double) width - ((double) radius + (double) correctionAmount)), top + correctionAmount);
      else
        graphicsPath.AddLine(left, top + correctionAmount, left + width, top + correctionAmount);
      if (instance.CornerCurvedTopRight)
      {
        graphicsPath.AddArc((float) ((double) left + (double) width - ((double) diameter + (double) correctionAmount)), top, diameter, diameter, 270f, 90f);
        if (instance.CornerCurvedBottomRight)
          graphicsPath.AddLine(left + width - correctionAmount, top + radius, left + width - correctionAmount, top + height - radius);
        else
          graphicsPath.AddLine(left + width - correctionAmount, top + (radius + correctionAmount), left + width - correctionAmount, top + height);
      }
      else if (instance.CornerCurvedBottomRight)
        graphicsPath.AddLine(left + width - correctionAmount, top, left + width - correctionAmount, (float) ((double) top + (double) height - ((double) radius + (double) correctionAmount)));
      else
        graphicsPath.AddLine(left + width - correctionAmount, top, left + width - correctionAmount, top + height);
      if (instance.CornerCurvedBottomRight)
      {
        graphicsPath.AddArc((float) ((double) left + (double) width - ((double) diameter + (double) correctionAmount)), (float) ((double) top + (double) height - ((double) diameter + (double) correctionAmount)), diameter, diameter, 0.0f, 90f);
        if (instance.CornerCurvedBottomLeft)
          graphicsPath.AddLine(left + width - radius, top + height - correctionAmount, left + radius, top + height - correctionAmount);
        else
          graphicsPath.AddLine(left, top + height - correctionAmount, left + (radius - correctionAmount), top + height - correctionAmount);
      }
      else if (instance.CornerCurvedBottomLeft)
        graphicsPath.AddLine((float) ((double) left + (double) width - ((double) radius + (double) correctionAmount)), top + height - correctionAmount, left, top + height - correctionAmount);
      else
        graphicsPath.AddLine(left + width, top + height - correctionAmount, left, top + height - correctionAmount);
      if (instance.CornerCurvedBottomLeft)
      {
        graphicsPath.AddArc(left, (float) ((double) top + (double) height - ((double) diameter + (double) correctionAmount)), diameter, diameter, 90f, 90f);
        if (instance.CornerCurvedTopLeft)
          graphicsPath.AddLine(left + correctionAmount, top + height - radius, left + correctionAmount, top + radius);
        else
          graphicsPath.AddLine(left + correctionAmount, top, left + correctionAmount, (float) ((double) top + (double) height - ((double) radius + (double) correctionAmount)));
      }
      else if (instance.CornerCurvedTopLeft)
        graphicsPath.AddLine(left + correctionAmount, top + (radius + correctionAmount), left + correctionAmount, top + height);
      else
        graphicsPath.AddLine(left + correctionAmount, top + height, left + correctionAmount, top);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }

    public void SetCornerCurves(bool topLeft, bool topRight, bool bottomRight, bool bottomLeft)
    {
      this.CornerCurvedBottomLeft = bottomLeft;
      this.CornerCurvedBottomRight = bottomRight;
      this.CornerCurvedTopLeft = topLeft;
      this.CornerCurvedTopRight = topRight;
    }

    private static GraphicsPath CreateRoundRectangleCorrected(
      RectangleF rectangle,
      float radius,
      GlassButton instance)
    {
      return GlassButton.DefineOutline(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, radius * 2f, radius, 1f, instance);
    }

    private static GraphicsPath CreateTopRoundRectangle(
      Rectangle rectangle,
      int radius,
      GlassButton instance)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      int left = rectangle.Left;
      int top = rectangle.Top;
      int width = rectangle.Width;
      int height = rectangle.Height;
      int diameter = radius << 1;
      return GlassButton.DefineOutline((float) rectangle.Left, (float) rectangle.Top, (float) rectangle.Width, (float) rectangle.Height, (float) diameter, (float) radius, 0.0f, instance);
    }

    private static GraphicsPath CreateBottomRadialPath(Rectangle rectangle)
    {
      GraphicsPath bottomRadialPath = new GraphicsPath();
      RectangleF rect = (RectangleF) rectangle;
      rect.X -= rect.Width * 0.35f;
      rect.Y -= rect.Height * 0.15f;
      rect.Width *= 1.7f;
      rect.Height *= 2.3f;
      bottomRadialPath.AddEllipse(rect);
      bottomRadialPath.CloseFigure();
      return bottomRadialPath;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new FlatButtonAppearance FlatAppearance => base.FlatAppearance;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new FlatStyle FlatStyle
    {
      get => base.FlatStyle;
      set => base.FlatStyle = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new bool UseVisualStyleBackColor
    {
      get => base.UseVisualStyleBackColor;
      set => base.UseVisualStyleBackColor = value;
    }

    private bool HasAnimationFrames => this.frames != null && this.frames.Count > 3;

    private void CreateFrames() => this.CreateFrames(false);

    private void CreateFrames(bool withAnimationFrames) => this.CreateFrames(withAnimationFrames, true);

    private void CreateFrames(bool withAnimationFrames, bool resetRegion)
    {
      this.DestroyFrames();
      if (!this.IsHandleCreated)
        return;
      if (this.frames == null)
        this.frames = new List<Image>();
      if (resetRegion && this._ClipBackground)
      {
        float num = 7f;
        if (this.Height < 30)
          num = 6f;
        using (GraphicsPath rectangleCorrected = GlassButton.CreateRoundRectangleCorrected(new RectangleF((float) this.ClientRectangle.X, (float) this.ClientRectangle.Y, (float) this.ClientRectangle.Width, (float) this.ClientRectangle.Height), num + 1f, this))
          this.SetRegion(rectangleCorrected);
      }
      this.frames.Add(this.CreateBackgroundFrame(false, false, false, false, 0.0f));
      this.frames.Add(this.CreateBackgroundFrame(true, true, false, true, 0.0f));
      this.frames.Add(this.CreateBackgroundFrame(false, false, false, true, 0.0f));
      if (!withAnimationFrames)
        return;
      for (int index = 0; index < 10; ++index)
        this.frames.Add(this.CreateBackgroundFrame(false, true, true, true, (float) index / 9f));
    }

    private void DestroyFrames()
    {
      if (this.frames == null)
        return;
      while (this.frames.Count > 0)
      {
        this.frames[this.frames.Count - 1].Dispose();
        this.frames.RemoveAt(this.frames.Count - 1);
      }
    }

    private bool isAnimating => this.direction != 0;

    private void FadeIn()
    {
      this.direction = 1;
      this.timer.Enabled = true;
    }

    private void FadeOut()
    {
      this.direction = -1;
      this.timer.Enabled = true;
    }

    private void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (!this.timer.Enabled)
        return;
      this.Invalidate();
      this.currentFrame += this.direction;
      if (this.currentFrame == -1)
      {
        this.currentFrame = 0;
        this.timer.Enabled = false;
        this.direction = 0;
      }
      else
      {
        if (this.currentFrame != 10)
          return;
        this.currentFrame = 9;
        this.timer.Enabled = false;
        this.direction = 0;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.imageButton != null)
        {
          this.imageButton.Parent.Dispose();
          this.imageButton.Parent = (Control) null;
          this.imageButton.Dispose();
          this.imageButton = (System.Windows.Forms.Button) null;
        }
        this.DestroyFrames();
        if (this.components != null)
          this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.timer = new System.Timers.Timer();
      this.SuspendLayout();
      this.timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
      this.ResumeLayout(false);
    }

    private class TransparentControl : Control
    {
      protected override void OnPaintBackground(PaintEventArgs pevent)
      {
      }

      protected override void OnPaint(PaintEventArgs e)
      {
      }
    }
  }
}
