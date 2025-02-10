// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DirectionCtrl
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
  public class DirectionCtrl : BufferPaintingCtrl
  {
    private bool mouseInside;
    private Color color = Color.DarkGray;
    private Color hoverColor = Color.Orange;
    private DirectionStyle directionStyle = DirectionStyle.Up;
    private Image image;
    private Image imageHover;

    public event DirectionCtrlStyleChangedEvent handlerStyleChange;

    public DirectionCtrl()
    {
      this.MouseEnter += new EventHandler(this.OnMouseEnterEvent);
      this.MouseLeave += new EventHandler(this.OnMouseLeaveEvent);
      this.MouseClick += new MouseEventHandler(this.OnMouseClickEvent);
    }

    [Description("Get/Set where this control points to")]
    [DefaultValue(DirectionStyle.Up)]
    [Category("Apperance")]
    public DirectionStyle DirectionStyle
    {
      get => this.directionStyle;
      set
      {
        if (this.directionStyle != value)
        {
          if (this.image != null)
            this.image.Dispose();
          this.image = (Image) null;
        }
        this.directionStyle = value;
      }
    }

    [Category("Appearance")]
    [Description("Get/Set the color used for the direction control")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color Color
    {
      get => this.color;
      set
      {
        if (!(value != this.color))
          return;
        this.color = value;
        this.InitializeImage();
      }
    }

    [Description("Get/Set the color used for the direction control")]
    [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color HoverColor
    {
      get => this.hoverColor;
      set
      {
        if (!(value != this.hoverColor))
          return;
        this.hoverColor = value;
        this.InitializeImage();
      }
    }

    private void InitializeImage()
    {
      if (this.image != null)
      {
        this.image.Dispose();
        this.image = (Image) null;
      }
      if (this.imageHover != null)
      {
        this.imageHover.Dispose();
        this.imageHover = (Image) null;
      }
      Brush brush1 = (Brush) new SolidBrush(this.color);
      this.image = this.CreateImage(brush1, true);
      brush1.Dispose();
      Brush brush2 = (Brush) new SolidBrush(this.hoverColor);
      this.imageHover = this.CreateImage(brush2, false);
      brush2.Dispose();
    }

    private Image CreateImage(Brush brush, bool flag)
    {
      string str = "»";
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;
      int width = this.Width;
      int height = this.Height;
      if (width == 0)
        width = 1;
      if (height == 0)
        height = 1;
      Image image = (Image) new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      Graphics graphics = Graphics.FromImage(image);
      graphics.Clear(Color.Transparent);
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.MeasureString(str, this.Font);
      Font font = !flag ? new Font("Arial", 15f, FontStyle.Bold) : new Font("Arial", 12f, FontStyle.Bold);
      graphics.DrawString(str, font, brush, new RectangleF(0.0f, 0.0f, (float) this.Width, (float) this.Height), format);
      graphics.Dispose();
      image.RotateFlip((RotateFlipType) this.directionStyle);
      return image;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.image == null)
        this.InitializeImage();
      e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      if (!this.mouseInside)
        e.Graphics.DrawImage(this.image, new Point(0, 0));
      else
        e.Graphics.DrawImage(this.imageHover, new Point(0, 0));
      base.OnPaint(e);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.InitializeImage();
      this.Update();
    }

    private void OnMouseEnterEvent(object sender, EventArgs e)
    {
      this.mouseInside = true;
      this.Refresh();
    }

    private void OnMouseLeaveEvent(object sender, EventArgs e)
    {
      this.mouseInside = false;
      this.Refresh();
    }

    private void OnMouseClickEvent(object sender, MouseEventArgs e) => this.DoMouseClick();

    public void DoMouseClick()
    {
      DirectionStyle directionStyle = this.directionStyle;
      switch (this.directionStyle)
      {
        case DirectionStyle.Right:
          this.directionStyle = DirectionStyle.Left;
          break;
        case DirectionStyle.Down:
          this.directionStyle = DirectionStyle.Up;
          break;
        case DirectionStyle.Left:
          this.directionStyle = DirectionStyle.Right;
          break;
        case DirectionStyle.Up:
          this.directionStyle = DirectionStyle.Down;
          break;
      }
      this.InitializeImage();
      if (this.handlerStyleChange == null)
        return;
      this.handlerStyleChange((object) this, new ChangeStyleEventArgs(directionStyle, this.directionStyle));
    }
  }
}
