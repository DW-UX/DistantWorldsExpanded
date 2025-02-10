// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ColorSlider
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  [DefaultEvent("Scroll")]
  [ToolboxBitmap(typeof (TrackBar))]
  [DefaultProperty("BarInnerColor")]
  public class ColorSlider : Control
  {
    private Rectangle thumbRect;
    private Rectangle barRect;
    private Rectangle barHalfRect;
    private Rectangle thumbHalfRect;
    private Rectangle elapsedRect;
    private int thumbSize = 15;
    private GraphicsPath thumbCustomShape;
    private Size thumbRoundRectSize = new Size(8, 8);
    private Size borderRoundRectSize = new Size(8, 8);
    private Orientation barOrientation;
    private int trackerValue = 50;
    private int barMinimum;
    private int barMaximum = 100;
    private uint smallChange = 1;
    private uint largeChange = 5;
    private bool drawFocusRectangle = true;
    private bool drawSemitransparentThumb = true;
    private bool mouseEffects = true;
    private int mouseWheelBarPartitions = 10;
    private Color thumbOuterColor;
    private Color thumbInnerColor;
    private Color thumbPenColor;
    private Color barOuterColor;
    private Color barInnerColor;
    private Color barPenColor;
    private Color elapsedOuterColor;
    private Color elapsedInnerColor;
    private Color[,] aColorSchema = new Color[4, 8]
    {
      {
        Color.White,
        Color.Gainsboro,
        Color.Silver,
        Color.SkyBlue,
        Color.DarkSlateBlue,
        Color.Gainsboro,
        Color.DarkGreen,
        Color.Chartreuse
      },
      {
        Color.White,
        Color.Gainsboro,
        Color.Silver,
        Color.Red,
        Color.DarkRed,
        Color.Gainsboro,
        Color.Coral,
        Color.LightCoral
      },
      {
        Color.White,
        Color.Gainsboro,
        Color.Silver,
        Color.GreenYellow,
        Color.Yellow,
        Color.Gold,
        Color.Orange,
        Color.OrangeRed
      },
      {
        Color.White,
        Color.Gainsboro,
        Color.Silver,
        Color.Red,
        Color.Crimson,
        Color.Gainsboro,
        Color.DarkViolet,
        Color.Violet
      }
    };
    private ColorSlider.ColorSchemas colorSchema;
    private bool mouseInRegion;
    private bool mouseInThumbRegion;
    private IContainer components;

    [Category("Action")]
    [Description("Event fires when the Value property changes")]
    public event EventHandler ValueChanged;

    [Description("Event fires when the Slider position is changed")]
    [Category("Behavior")]
    public event ScrollEventHandler Scroll;

    [Browsable(false)]
    public Rectangle ThumbRect => this.thumbRect;

    [DefaultValue(15)]
    [Category("ColorSlider")]
    [Description("Set Slider thumb size")]
    public int ThumbSize
    {
      get => this.thumbSize;
      set
      {
        if (!(value > 0 & value < (this.barOrientation == Orientation.Horizontal ? this.ClientRectangle.Width : this.ClientRectangle.Height)))
          throw new ArgumentOutOfRangeException("TrackSize has to be greather than zero and lower than half of Slider width");
        this.thumbSize = value;
        this.Invalidate();
      }
    }

    [Description("Set Slider's thumb's custom shape")]
    [Category("ColorSlider")]
    [Browsable(false)]
    [DefaultValue(typeof (GraphicsPath), "null")]
    public GraphicsPath ThumbCustomShape
    {
      get => this.thumbCustomShape;
      set
      {
        this.thumbCustomShape = value;
        this.thumbSize = (this.barOrientation == Orientation.Horizontal ? (int) value.GetBounds().Width : (int) value.GetBounds().Height) + 1;
        this.Invalidate();
      }
    }

    [DefaultValue(typeof (Size), "8; 8")]
    [Category("ColorSlider")]
    [Description("Set Slider's thumb round rect size")]
    public Size ThumbRoundRectSize
    {
      get => this.thumbRoundRectSize;
      set
      {
        int height = value.Height;
        int width = value.Width;
        if (height <= 0)
          height = 1;
        if (width <= 0)
          width = 1;
        this.thumbRoundRectSize = new Size(width, height);
        this.Invalidate();
      }
    }

    [DefaultValue(typeof (Size), "8; 8")]
    [Description("Set Slider's border round rect size")]
    [Category("ColorSlider")]
    public Size BorderRoundRectSize
    {
      get => this.borderRoundRectSize;
      set
      {
        int height = value.Height;
        int width = value.Width;
        if (height <= 0)
          height = 1;
        if (width <= 0)
          width = 1;
        this.borderRoundRectSize = new Size(width, height);
        this.Invalidate();
      }
    }

    [Description("Set Slider orientation")]
    [DefaultValue(Orientation.Horizontal)]
    [Category("ColorSlider")]
    public Orientation Orientation
    {
      get => this.barOrientation;
      set
      {
        if (this.barOrientation == value)
          return;
        this.barOrientation = value;
        int width = this.Width;
        this.Width = this.Height;
        this.Height = width;
        if (this.thumbCustomShape != null)
          this.thumbSize = (this.barOrientation == Orientation.Horizontal ? (int) this.thumbCustomShape.GetBounds().Width : (int) this.thumbCustomShape.GetBounds().Height) + 1;
        this.Invalidate();
      }
    }

    [DefaultValue(50)]
    [Description("Set Slider value")]
    [Category("ColorSlider")]
    public int Value
    {
      get => this.trackerValue;
      set
      {
        if (!(value >= this.barMinimum & value <= this.barMaximum))
          throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
        this.trackerValue = value;
        if (this.ValueChanged != null)
          this.ValueChanged((object) this, new EventArgs());
        this.Invalidate();
      }
    }

    [Category("ColorSlider")]
    [DefaultValue(0)]
    [Description("Set Slider minimal point")]
    public int Minimum
    {
      get => this.barMinimum;
      set
      {
        this.barMinimum = value < this.barMaximum ? value : throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
        if (this.trackerValue < this.barMinimum)
        {
          this.trackerValue = this.barMinimum;
          if (this.ValueChanged != null)
            this.ValueChanged((object) this, new EventArgs());
        }
        this.Invalidate();
      }
    }

    [Category("ColorSlider")]
    [Description("Set Slider maximal point")]
    [DefaultValue(100)]
    public int Maximum
    {
      get => this.barMaximum;
      set
      {
        this.barMaximum = value > this.barMinimum ? value : throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
        if (this.trackerValue > this.barMaximum)
        {
          this.trackerValue = this.barMaximum;
          if (this.ValueChanged != null)
            this.ValueChanged((object) this, new EventArgs());
        }
        this.Invalidate();
      }
    }

    [Description("Set trackbar's small change")]
    [Category("ColorSlider")]
    [DefaultValue(1)]
    public uint SmallChange
    {
      get => this.smallChange;
      set => this.smallChange = value;
    }

    [Category("ColorSlider")]
    [Description("Set trackbar's large change")]
    [DefaultValue(5)]
    public uint LargeChange
    {
      get => this.largeChange;
      set => this.largeChange = value;
    }

    [Category("ColorSlider")]
    [Description("Set whether to draw focus rectangle")]
    [DefaultValue(true)]
    public bool DrawFocusRectangle
    {
      get => this.drawFocusRectangle;
      set
      {
        this.drawFocusRectangle = value;
        this.Invalidate();
      }
    }

    [DefaultValue(true)]
    [Category("ColorSlider")]
    [Description("Set whether to draw semitransparent thumb")]
    public bool DrawSemitransparentThumb
    {
      get => this.drawSemitransparentThumb;
      set
      {
        this.drawSemitransparentThumb = value;
        this.Invalidate();
      }
    }

    [DefaultValue(true)]
    [Category("ColorSlider")]
    [Description("Set whether mouse entry and exit actions have impact on how control look")]
    public bool MouseEffects
    {
      get => this.mouseEffects;
      set
      {
        this.mouseEffects = value;
        this.Invalidate();
      }
    }

    [DefaultValue(10)]
    [Description("Set to how many parts is bar divided when using mouse wheel")]
    [Category("ColorSlider")]
    public int MouseWheelBarPartitions
    {
      get => this.mouseWheelBarPartitions;
      set => this.mouseWheelBarPartitions = value > 0 ? value : throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
    }

    [Description("Set Slider thumb outer color")]
    [Category("ColorSlider")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ThumbOuterColor
    {
      get => this.thumbOuterColor;
      set => this.Invalidate();
    }

    [Description("Set Slider thumb inner color")]
    [Category("ColorSlider")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ThumbInnerColor
    {
      get => this.thumbInnerColor;
      set => this.Invalidate();
    }

    [Category("ColorSlider")]
    [Description("Set Slider thumb pen color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ThumbPenColor
    {
      get => this.thumbPenColor;
      set => this.Invalidate();
    }

    [Category("ColorSlider")]
    [Description("Set Slider bar outer color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BarOuterColor
    {
      get => this.barOuterColor;
      set => this.Invalidate();
    }

    [Category("ColorSlider")]
    [Description("Set Slider bar inner color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BarInnerColor
    {
      get => this.barInnerColor;
      set => this.Invalidate();
    }

    [Category("ColorSlider")]
    [Description("Set Slider bar pen color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BarPenColor
    {
      get => this.barPenColor;
      set => this.Invalidate();
    }

    [Description("Set Slider's elapsed part outer color")]
    [Category("ColorSlider")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ElapsedOuterColor
    {
      get => this.elapsedOuterColor;
      set => this.Invalidate();
    }

    [Category("ColorSlider")]
    [Description("Set Slider's elapsed part inner color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ElapsedInnerColor
    {
      get => this.elapsedInnerColor;
      set => this.Invalidate();
    }

    [DefaultValue(typeof (ColorSlider.ColorSchemas), "PerlBlueGreen")]
    [Category("ColorSlider")]
    [Description("Set Slider color schema. Has no effect when slider colors are changed manually after schema was applied.")]
    public ColorSlider.ColorSchemas ColorSchema
    {
      get => this.colorSchema;
      set
      {
        this.colorSchema = value;
        byte index = (byte) value;
        this.thumbOuterColor = this.aColorSchema[(int) index, 0];
        this.thumbInnerColor = this.aColorSchema[(int) index, 1];
        this.thumbPenColor = this.aColorSchema[(int) index, 2];
        this.barOuterColor = this.aColorSchema[(int) index, 3];
        this.barInnerColor = this.aColorSchema[(int) index, 4];
        this.barPenColor = this.aColorSchema[(int) index, 5];
        this.elapsedOuterColor = this.aColorSchema[(int) index, 6];
        this.elapsedInnerColor = this.aColorSchema[(int) index, 7];
        this.Invalidate();
      }
    }

    public ColorSlider(int min, int max, int value)
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.BackColor = Color.Transparent;
      this.barInnerColor = Color.FromArgb(64, 64, 72);
      this.barOuterColor = Color.FromArgb(32, 32, 40);
      this.barPenColor = Color.FromArgb(16, 16, 24);
      this.elapsedInnerColor = Color.FromArgb(80, 80, 96);
      this.elapsedOuterColor = Color.FromArgb(48, 48, 64);
      this.thumbInnerColor = Color.FromArgb(80, 80, 96);
      this.thumbOuterColor = Color.FromArgb(48, 48, 64);
      this.thumbPenColor = Color.FromArgb(32, 32, 40);
      this.Minimum = min;
      this.Maximum = max;
      this.Value = value;
    }

    public ColorSlider()
      : this(0, 100, 50)
    {
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.Enabled)
      {
        Color[] colorArray = ColorSlider.DesaturateColors(this.thumbOuterColor, this.thumbInnerColor, this.thumbPenColor, this.barOuterColor, this.barInnerColor, this.barPenColor, this.elapsedOuterColor, this.elapsedInnerColor);
        this.DrawColorSlider(e, colorArray[0], colorArray[1], colorArray[2], colorArray[3], colorArray[4], colorArray[5], colorArray[6], colorArray[7]);
      }
      else if (this.mouseEffects && this.mouseInRegion)
      {
        Color[] colorArray = ColorSlider.LightenColors(this.thumbOuterColor, this.thumbInnerColor, this.thumbPenColor, this.barOuterColor, this.barInnerColor, this.barPenColor, this.elapsedOuterColor, this.elapsedInnerColor);
        this.DrawColorSlider(e, colorArray[0], colorArray[1], colorArray[2], colorArray[3], colorArray[4], colorArray[5], colorArray[6], colorArray[7]);
      }
      else
        this.DrawColorSlider(e, this.thumbOuterColor, this.thumbInnerColor, this.thumbPenColor, this.barOuterColor, this.barInnerColor, this.barPenColor, this.elapsedOuterColor, this.elapsedInnerColor);
    }

    private void DrawColorSlider(
      PaintEventArgs e,
      Color thumbOuterColorPaint,
      Color thumbInnerColorPaint,
      Color thumbPenColorPaint,
      Color barOuterColorPaint,
      Color barInnerColorPaint,
      Color barPenColorPaint,
      Color elapsedOuterColorPaint,
      Color elapsedInnerColorPaint)
    {
      try
      {
        this.thumbRect = this.barOrientation != Orientation.Horizontal ? new Rectangle(1, (this.trackerValue - this.barMinimum) * (this.ClientRectangle.Height - this.thumbSize) / (this.barMaximum - this.barMinimum), this.ClientRectangle.Width - 3, this.thumbSize - 1) : new Rectangle((this.trackerValue - this.barMinimum) * (this.ClientRectangle.Width - this.thumbSize) / (this.barMaximum - this.barMinimum), 1, this.thumbSize - 1, this.ClientRectangle.Height - 3);
        this.barRect = this.ClientRectangle;
        this.thumbHalfRect = this.thumbRect;
        System.Drawing.Drawing2D.LinearGradientMode linearGradientMode;
        if (this.barOrientation == Orientation.Horizontal)
        {
          this.barRect.Inflate(-1, -this.barRect.Height / 3);
          this.barHalfRect = this.barRect;
          this.barHalfRect.Height /= 2;
          linearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
          this.thumbHalfRect.Height /= 2;
          this.elapsedRect = this.barRect;
          this.elapsedRect.Width = this.thumbRect.Left + this.thumbSize / 2;
        }
        else
        {
          this.barRect.Inflate(-this.barRect.Width / 3, -1);
          this.barHalfRect = this.barRect;
          this.barHalfRect.Width /= 2;
          linearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
          this.thumbHalfRect.Width /= 2;
          this.elapsedRect = this.barRect;
          this.elapsedRect.Height = this.thumbRect.Top + this.thumbSize / 2;
        }
        GraphicsPath path;
        if (this.thumbCustomShape == null)
        {
          path = ColorSlider.CreateRoundRectPath(this.thumbRect, this.thumbRoundRectSize);
        }
        else
        {
          path = this.thumbCustomShape;
          Matrix matrix = new Matrix();
          matrix.Translate((float) this.thumbRect.Left - path.GetBounds().Left, (float) this.thumbRect.Top - path.GetBounds().Top);
          path.Transform(matrix);
        }
        using (LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(this.barHalfRect, barOuterColorPaint, barInnerColorPaint, linearGradientMode))
        {
          linearGradientBrush1.WrapMode = WrapMode.TileFlipXY;
          e.Graphics.FillRectangle((Brush) linearGradientBrush1, this.barRect);
          using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(this.barHalfRect, elapsedOuterColorPaint, elapsedInnerColorPaint, linearGradientMode))
          {
            linearGradientBrush2.WrapMode = WrapMode.TileFlipXY;
            if (this.Capture && this.drawSemitransparentThumb)
            {
              Region region = new Region(this.elapsedRect);
              region.Exclude(path);
              e.Graphics.FillRegion((Brush) linearGradientBrush2, region);
            }
            else
              e.Graphics.FillRectangle((Brush) linearGradientBrush2, this.elapsedRect);
          }
          using (Pen pen = new Pen(barPenColorPaint, 0.5f))
            e.Graphics.DrawRectangle(pen, this.barRect);
        }
        Color color1 = thumbOuterColorPaint;
        Color color2 = thumbInnerColorPaint;
        if (this.Capture && this.drawSemitransparentThumb)
        {
          color1 = Color.FromArgb(175, thumbOuterColorPaint);
          color2 = Color.FromArgb(175, thumbInnerColorPaint);
        }
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.thumbHalfRect, color1, color2, linearGradientMode))
        {
          linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          e.Graphics.FillPath((Brush) linearGradientBrush, path);
          Color color = thumbPenColorPaint;
          if (this.mouseEffects && (this.Capture || this.mouseInThumbRegion))
            color = ControlPaint.Dark(color);
          using (Pen pen = new Pen(color))
            e.Graphics.DrawPath(pen, path);
        }
        if (!(this.Focused & this.drawFocusRectangle))
          return;
        using (Pen pen = new Pen(Color.FromArgb(200, barPenColorPaint)))
        {
          pen.DashStyle = DashStyle.Dot;
          Rectangle clientRectangle = this.ClientRectangle;
          clientRectangle.Width -= 2;
          --clientRectangle.Height;
          ++clientRectangle.X;
          using (GraphicsPath roundRectPath = ColorSlider.CreateRoundRectPath(clientRectangle, this.borderRoundRectSize))
          {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawPath(pen, roundRectPath);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("DrawBackGround Error in " + this.Name + ":" + ex.ToString());
      }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      this.Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.mouseInRegion = true;
      this.Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.mouseInRegion = false;
      this.mouseInThumbRegion = false;
      this.Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left)
        return;
      this.Capture = true;
      if (this.Scroll != null)
        this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.ThumbTrack, this.trackerValue));
      if (this.ValueChanged != null)
        this.ValueChanged((object) this, new EventArgs());
      this.OnMouseMove(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.mouseInThumbRegion = ColorSlider.IsPointInRect(e.Location, this.thumbRect);
      if (this.Capture & e.Button == MouseButtons.Left)
      {
        ScrollEventType type = ScrollEventType.ThumbPosition;
        Point location = e.Location;
        int num1 = this.barOrientation == Orientation.Horizontal ? location.X : location.Y;
        int num2 = this.thumbSize >> 1;
        this.trackerValue = (int) ((double) (num1 - num2) * (double) ((float) (this.barMaximum - this.barMinimum) / (float) ((this.barOrientation == Orientation.Horizontal ? this.ClientSize.Width : this.ClientSize.Height) - 2 * num2)) + (double) this.barMinimum + 0.5);
        if (this.trackerValue <= this.barMinimum)
        {
          this.trackerValue = this.barMinimum;
          type = ScrollEventType.First;
        }
        else if (this.trackerValue >= this.barMaximum)
        {
          this.trackerValue = this.barMaximum;
          type = ScrollEventType.Last;
        }
        if (this.Scroll != null)
          this.Scroll((object) this, new ScrollEventArgs(type, this.trackerValue));
        if (this.ValueChanged != null)
          this.ValueChanged((object) this, new EventArgs());
      }
      this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.Capture = false;
      this.mouseInThumbRegion = ColorSlider.IsPointInRect(e.Location, this.thumbRect);
      if (this.Scroll != null)
        this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.EndScroll, this.trackerValue));
      if (this.ValueChanged != null)
        this.ValueChanged((object) this, new EventArgs());
      this.Invalidate();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.SetProperValue(this.Value + e.Delta / 120 * (this.barMaximum - this.barMinimum) / this.mouseWheelBarPartitions);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      switch (e.KeyCode)
      {
        case Keys.Prior:
          this.SetProperValue(this.Value + (int) this.largeChange);
          if (this.Scroll != null)
          {
            this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.LargeIncrement, this.Value));
            break;
          }
          break;
        case Keys.Next:
          this.SetProperValue(this.Value - (int) this.largeChange);
          if (this.Scroll != null)
          {
            this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.LargeDecrement, this.Value));
            break;
          }
          break;
        case Keys.End:
          this.Value = this.barMaximum;
          break;
        case Keys.Home:
          this.Value = this.barMinimum;
          break;
        case Keys.Left:
        case Keys.Down:
          this.SetProperValue(this.Value - (int) this.smallChange);
          if (this.Scroll != null)
          {
            this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.SmallDecrement, this.Value));
            break;
          }
          break;
        case Keys.Up:
        case Keys.Right:
          this.SetProperValue(this.Value + (int) this.smallChange);
          if (this.Scroll != null)
          {
            this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.SmallIncrement, this.Value));
            break;
          }
          break;
      }
      if (this.Scroll != null && this.Value == this.barMinimum)
        this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.First, this.Value));
      if (this.Scroll != null && this.Value == this.barMaximum)
        this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.Last, this.Value));
      Point client = this.PointToClient(MouseHelper.GetCursorPosition());
      this.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, client.X, client.Y, 0));
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Tab | Control.ModifierKeys == Keys.Shift)
        return base.ProcessDialogKey(keyData);
      this.OnKeyDown(new KeyEventArgs(keyData));
      return true;
    }

    public static GraphicsPath CreateRoundRectPath(Rectangle rect, Size size)
    {
      GraphicsPath roundRectPath = new GraphicsPath();
      roundRectPath.AddLine(rect.Left + size.Width / 2, rect.Top, rect.Right - size.Width / 2, rect.Top);
      roundRectPath.AddArc(rect.Right - size.Width, rect.Top, size.Width, size.Height, 270f, 90f);
      roundRectPath.AddLine(rect.Right, rect.Top + size.Height / 2, rect.Right, rect.Bottom - size.Width / 2);
      roundRectPath.AddArc(rect.Right - size.Width, rect.Bottom - size.Height, size.Width, size.Height, 0.0f, 90f);
      roundRectPath.AddLine(rect.Right - size.Width / 2, rect.Bottom, rect.Left + size.Width / 2, rect.Bottom);
      roundRectPath.AddArc(rect.Left, rect.Bottom - size.Height, size.Width, size.Height, 90f, 90f);
      roundRectPath.AddLine(rect.Left, rect.Bottom - size.Height / 2, rect.Left, rect.Top + size.Height / 2);
      roundRectPath.AddArc(rect.Left, rect.Top, size.Width, size.Height, 180f, 90f);
      return roundRectPath;
    }

    public static Color[] DesaturateColors(params Color[] colorsToDesaturate)
    {
      Color[] colorArray = new Color[colorsToDesaturate.Length];
      for (int index = 0; index < colorsToDesaturate.Length; ++index)
      {
        int num = (int) ((double) colorsToDesaturate[index].R * 0.3 + (double) colorsToDesaturate[index].G * 0.6 + (double) colorsToDesaturate[index].B * 0.1);
        colorArray[index] = Color.FromArgb(-65793 * ((int) byte.MaxValue - num) - 1);
      }
      return colorArray;
    }

    public static Color[] LightenColors(params Color[] colorsToLighten)
    {
      Color[] colorArray = new Color[colorsToLighten.Length];
      for (int index = 0; index < colorsToLighten.Length; ++index)
        colorArray[index] = ControlPaint.Light(colorsToLighten[index]);
      return colorArray;
    }

    private void SetProperValue(int val)
    {
      if (val < this.barMinimum)
        this.Value = this.barMinimum;
      else if (val > this.barMaximum)
        this.Value = this.barMaximum;
      else
        this.Value = val;
    }

    private static bool IsPointInRect(Point pt, Rectangle rect) => pt.X > rect.Left & pt.X < rect.Right & pt.Y > rect.Top & pt.Y < rect.Bottom;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.Size = new Size(200, 30);
      this.ResumeLayout(false);
    }

    public enum ColorSchemas
    {
      PerlBlueGreen,
      PerlRedCoral,
      PerlGold,
      PerlRoyalColors,
    }
  }
}
