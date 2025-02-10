// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HeaderPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class HeaderPanel : Panel
  {
    private IContainer components;
    public CloseButton btnClose;
    private Color _TextColor;
    private Color _TextColor2;
    private Color _OuterBorderColor;
    private Color _InnerBorderColor;
    private Color _ShineColor;
    private Color _GlowColor;
    private string _TitleText;
    private Font _TitleFont;
    private Image _Icon;
    private Rectangle _IconLocation;
    private Point _TitleLocation;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnClose = new CloseButton();
      this.SuspendLayout();
      this.btnClose.BackColor = Color.FromArgb(22, 21, 26);
      this.btnClose.ForeColor = Color.FromArgb(67, 67, 77);
      this.btnClose.Location = new Point(0, 0);
      this.btnClose.Name = "btnClose";
      this.btnClose.Radius = 5;
      this.btnClose.Size = new Size(30, 30);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.ResumeLayout(false);
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TitleText
    {
      get => this._TitleText;
      set => this._TitleText = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image Icon
    {
      get => this._Icon;
      set => this._Icon = value;
    }

    public event EventHandler CloseButtonClicked;

    public HeaderPanel()
    {
      this.InitializeComponent();
      this.DoubleBuffered = true;
      this.BackColor = Color.FromArgb(0, 0, 20);
      this.BackColor = Color.FromArgb(20, 20, 40);
      this.ForeColor = Color.White;
      this._TextColor = this.ForeColor;
      this._TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this._OuterBorderColor = Color.FromArgb(0, 0, 20);
      this._InnerBorderColor = Color.FromArgb(48, 48, 68);
      this._ShineColor = Color.FromArgb(128, 128, 148);
      this._GlowColor = Color.FromArgb(80, 80, 160);
      this.SetFont();
    }

    public void SetFont()
    {
      this.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, GraphicsUnit.Pixel);
      this._TitleFont = new Font(this.Font.FontFamily, 22.67f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.BackColor = Color.Transparent;
      this.BackColor = Color.FromArgb(0, 0, 0);
      this.ForeColor = Color.FromArgb(150, 150, 150);
      this._OuterBorderColor = Color.FromArgb(0, 0, 16);
      this._InnerBorderColor = Color.FromArgb(67, 67, 77);
      this._ShineColor = Color.FromArgb(96, 96, 104);
      this._GlowColor = Color.FromArgb(48, 48, 128);
    }

    protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

    private void ReparentControl(Control parent, Control child)
    {
      if (child.Parent == parent)
        return;
      if (!parent.Controls.Contains(child))
        parent.Controls.Add(child);
      child.Parent = parent;
    }

    public void DoLayout()
    {
      this.SuspendLayout();
      this.ReparentControl((Control) this, (Control) this.btnClose);
      this._IconLocation = new Rectangle(10, 10, 30, 30);
      this._TitleLocation = new Point(45, 11);
      if (this._Icon == null)
        this._TitleLocation = new Point(10, 11);
      this.btnClose.Size = new Size(30, 30);
      this.btnClose.Location = new Point(this.ClientRectangle.Width - 41, 9);
      this.ResumeLayout();
      this.btnClose.Invalidate();
      this.Invalidate();
    }

    protected override void OnPaintBackground(PaintEventArgs e) => HeaderPanel.DrawBackground(e.Graphics, this.ClientRectangle, false, false, false, true, this._OuterBorderColor, this.BackColor, this._GlowColor, this._ShineColor, this._InnerBorderColor, 0.0f, 0);

    private void DrawIcon(Graphics graphics)
    {
      if (this._Icon == null)
        return;
      double num1 = (double) this._Icon.Width / (double) this._Icon.Height;
      int num2 = 10;
      int num3 = 10;
      int width1 = this._Icon.Width;
      int height1 = this._Icon.Height;
      double num4 = height1 <= 30 || height1 <= width1 ? (width1 <= 30 || width1 <= height1 ? 30.0 / (double) width1 : 30.0 / (double) width1) : 30.0 / (double) height1;
      int width2 = (int) ((double) width1 * num4);
      int height2 = (int) ((double) height1 * num4);
      Rectangle rect = new Rectangle(num2 + (30 - width2) / 2, num3 + (30 - height2) / 2, width2, height2);
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.DrawImage(this._Icon, rect);
    }

    private void DrawTitle(Graphics graphics) => graphics.DrawString(this._TitleText, this._TitleFont, (Brush) new SolidBrush(this._TextColor), (PointF) this._TitleLocation);

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      HeaderPanel.DrawBackground(e.Graphics, this.ClientRectangle, false, false, false, true, this._OuterBorderColor, this.BackColor, this._GlowColor, this._ShineColor, this._InnerBorderColor, 0.0f, 0);
      this.DrawIcon(e.Graphics);
      this.DrawTitle(e.Graphics);
    }

    private static void DrawBackground(
      Graphics g,
      Rectangle rectangle,
      bool pressed,
      bool hovered,
      bool animating,
      bool enabled,
      Color outerBorderColor,
      Color backColor,
      Color glowColor,
      Color shineColor,
      Color innerBorderColor,
      float glowOpacity,
      int radius)
    {
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rectangle1 = rectangle;
      --rectangle1.Width;
      --rectangle1.Height;
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddRectangle(rectangle1);
        using (Pen pen = new Pen(outerBorderColor))
          g.DrawPath(pen, path);
      }
      ++rectangle1.X;
      ++rectangle1.Y;
      rectangle1.Width -= 2;
      rectangle1.Height -= 2;
      Rectangle rectangle2 = rectangle1;
      rectangle2.Height >>= 1;
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddRectangle(rectangle1);
        using (Brush brush = (Brush) new SolidBrush(backColor))
          g.FillPath(brush, path);
      }
      if ((hovered || animating) && !pressed)
      {
        using (GraphicsPath path = new GraphicsPath())
        {
          path.AddRectangle(rectangle1);
          g.SetClip(path, CombineMode.Intersect);
          using (GraphicsPath bottomRadialPath = HeaderPanel.CreateBottomRadialPath(rectangle1))
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
        using (GraphicsPath topRoundRectangle = HeaderPanel.CreateTopRoundRectangle(rectangle2, radius - 2))
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
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddRectangle(rectangle1);
        using (Pen pen = new Pen(innerBorderColor))
          g.DrawPath(pen, path);
      }
      g.SmoothingMode = smoothingMode;
    }

    private static GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius)
    {
      GraphicsPath topRoundRectangle = new GraphicsPath();
      topRoundRectangle.AddRectangle(rectangle);
      topRoundRectangle.CloseFigure();
      return topRoundRectangle;
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

    private void btnClose_Click(object sender, EventArgs e)
    {
      if (this.CloseButtonClicked == null)
        return;
      this.CloseButtonClicked(sender, e);
    }
  }
}
