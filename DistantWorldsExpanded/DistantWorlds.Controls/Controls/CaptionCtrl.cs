// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CaptionCtrl
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  internal class CaptionCtrl : CornerCtrl
  {
    private bool mouseDown;
    private int mouseX;
    private int mouseY;
    private string text = "Caption";
    private BrushType brushType = BrushType.Gradient;
    private Color colorOne = Color.White;
    private Color colorTwo = Color.FromArgb(155, Color.Orange);
    private Color textColor = Color.Black;
    private Image captionIcon;
    private DirectionCtrl directionCtrl;
    private IContainer components;
    private Brush brush;

    public event CaptionDraggingEvent Dragging;

    public CaptionCtrl()
    {
      this.InitializeComponent();
      this.MouseMove += new MouseEventHandler(this.OnMouseMoveEvent);
      this.MouseDown += new MouseEventHandler(this.OnMouseDownEvent);
      this.MouseUp += new MouseEventHandler(this.OnMouseUpEvent);
      this.InitializeBrush();
    }

    [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DirectionCtrlColor
    {
      get => this.directionCtrl.Color;
      set => this.directionCtrl.Color = value;
    }

    [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DirectionCtrlHoverColor
    {
      get => this.directionCtrl.HoverColor;
      set => this.directionCtrl.HoverColor = value;
    }

    private Image PrescaleImage(
      Image originalBitmap,
      int imageWidth,
      int imageHeight,
      int width,
      int height)
    {
      Bitmap bitmap = new Bitmap(imageWidth, imageHeight, PixelFormat.Format32bppPArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int x = (imageWidth - width) / 2;
      int y = (imageHeight - height) / 2;
      graphics.DrawImage(originalBitmap, new Rectangle(x, y, width, height));
      graphics.Dispose();
      return (Image) bitmap;
    }

    [Description("Get or set the image to be displayed in the caption")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image CaptionIcon
    {
      get => this.captionIcon;
      set
      {
        if (value != null)
        {
          int width = 16;
          double num = (double) width / (double) value.Width;
          int height = (int) ((double) value.Height * num);
          this.captionIcon = this.PrescaleImage(value, 16, 16, width, height);
        }
        else
          this.captionIcon = (Image) null;
        this.Update();
      }
    }

    [Description("Get/Set the text to be displayed")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CaptionText
    {
      get => this.text;
      set
      {
        this.text = value;
        this.Update();
      }
    }

    [Description("The font for this control also used to draw the text in the caption")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font CaptionFont
    {
      get => this.Font;
      set
      {
        this.Font = value;
        this.Refresh();
      }
    }

    [Description("The starting color for the gradient brush")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorOne
    {
      get => this.colorOne;
      set
      {
        this.colorOne = value;
        this.InitializeBrush();
        this.Refresh();
      }
    }

    [Description("The end color for the gradient brush ")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColorTwo
    {
      get => this.colorTwo;
      set
      {
        this.colorTwo = value;
        this.InitializeBrush();
        this.Refresh();
      }
    }

    [Description("The color used for drawing caption text ")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
    {
      get => this.textColor;
      set
      {
        this.textColor = value;
        this.Refresh();
      }
    }

    [Category("Appearance")]
    [Description("The brush used in painting the caption")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BrushType BrushType
    {
      get => this.brushType;
      set
      {
        if (value == this.brushType)
          return;
        this.brushType = value;
        this.InitializeBrush();
        this.Refresh();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    protected override void InitializeGraphicPath()
    {
      this.cornerSquare = this.Height > this.Width ? (int) ((double) this.Height * 0.05000000074505806) : (int) ((double) this.Width * 0.05000000074505806);
      base.InitializeGraphicPath();
    }

    private void InitializeComponent()
    {
      this.directionCtrl = new DirectionCtrl();
      this.SuspendLayout();
      this.Name = nameof (CaptionCtrl);
      this.ResumeLayout(false);
    }

    private void InitializeBrush()
    {
      if (this.brush != null)
        this.brush.Dispose();
      if (this.brushType == BrushType.Solid)
      {
        this.brush = (Brush) new SolidBrush(this.colorOne);
      }
      else
      {
        int width = this.Width;
        int height = this.Height;
        if (width == 0)
          width = 1;
        if (height == 0)
          height = 1;
        if (this.directionCtrl.DirectionStyle == DirectionStyle.Up || this.directionCtrl.DirectionStyle == DirectionStyle.Down)
          this.brush = (Brush) new LinearGradientBrush(new Rectangle(0, 0, width, height), this.colorOne, this.colorTwo, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
        else
          this.brush = (Brush) new LinearGradientBrush(new Rectangle(0, 0, width, height), this.colorOne, this.colorTwo, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
      }
    }

    public void SetStyleChangedHandler(DirectionCtrlStyleChangedEvent handler) => this.directionCtrl.handlerStyleChange += handler;

    public bool IsDraggingEnabled() => this.Dragging != null;

    public void SetDirectionStyle(DirectionStyle style) => this.directionCtrl.DirectionStyle = style;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
      e.Graphics.InterpolationMode = InterpolationMode.High;
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      if (this.graphicPath == null)
        this.InitializeGraphicPath();
      e.Graphics.FillPath(this.brush, this.graphicPath);
      float num1 = 0.0f;
      float num2 = 0.0f;
      StringFormat stringFormat;
      if (this.Width >= this.Height)
      {
        stringFormat = new StringFormat();
        num2 = (float) (((double) this.Height - (double) e.Graphics.MeasureString(this.text, this.Font, new PointF(0.0f, 0.0f), stringFormat).Height) * 0.5);
      }
      else
      {
        stringFormat = new StringFormat(StringFormatFlags.DirectionVertical);
        num1 = (float) (((double) this.Width - (double) e.Graphics.MeasureString(this.text, this.Font, new PointF(0.0f, 0.0f), stringFormat).Width) * 0.5);
      }
      float num3 = num2 - 2f;
      float num4 = num1 + 3f;
      if (this.captionIcon != null)
      {
        if (this.Width >= this.Height)
        {
          e.Graphics.DrawImage(this.captionIcon, this.cornerSquare / 6, this.cornerSquare / 6);
          e.Graphics.DrawString(this.text, this.Font, (Brush) new SolidBrush(this.textColor), new PointF(num4 + (float) (this.cornerSquare / 6) + (float) this.captionIcon.Width, num3 + (float) (this.cornerSquare / 6)), stringFormat);
        }
        else
        {
          e.Graphics.DrawImage(this.captionIcon, this.cornerSquare / 6, this.cornerSquare / 6);
          e.Graphics.DrawString(this.text, this.Font, (Brush) new SolidBrush(this.textColor), new PointF(num4 + (float) (this.cornerSquare / 6), num3 + (float) (this.cornerSquare / 6) + (float) this.captionIcon.Height), stringFormat);
        }
      }
      else
        e.Graphics.DrawString(this.text, this.Font, (Brush) new SolidBrush(this.textColor), new PointF(num4 + (float) (this.cornerSquare / 6), num3 + (float) (this.cornerSquare / 6)), stringFormat);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      base.OnMouseClick(e);
      this.directionCtrl.DoMouseClick();
    }

    private void OnMouseMoveEvent(object sender, MouseEventArgs e)
    {
      if (!this.mouseDown || this.Dragging == null || this.mouseX == e.X && this.mouseY == e.Y)
        return;
      this.Dragging((object) this, new CaptionDraggingEventArgs(this.mouseX - e.X, this.mouseY - e.Y));
    }

    private void OnMouseDownEvent(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.mouseDown = true;
      this.mouseX = e.X;
      this.mouseY = e.Y;
    }

    private void OnMouseUpEvent(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.mouseDown = false;
      this.mouseX = 0;
      this.mouseY = 0;
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.InitializeGraphicPath();
      if (this.Width >= this.Height)
      {
        this.directionCtrl.Location = new Point(this.Width - this.Height, 0);
        this.directionCtrl.Width = this.Height;
        this.directionCtrl.Height = this.Height;
      }
      else
      {
        this.directionCtrl.Location = new Point(0, this.Height - this.Width);
        this.directionCtrl.Width = this.Width;
        this.directionCtrl.Height = this.Width;
      }
      this.InitializeBrush();
      this.Region = new Region(this.regionPath);
    }
  }
}
