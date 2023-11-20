// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HeaderPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls {

  public class HeaderPanel : Panel {

    private Color _OuterBorderColor;

    private Color _InnerBorderColor;

    private Color _ShineColor;

    private Color _GlowColor;

    private IContainer components;

    public CloseButton btnClose;

    internal PictureBox pbIcon;

    internal Label lblCaption;

    public bool Pressed;

    public bool Hovered;

    public bool Animating;

    private string _titleText;

    private Image _icon;

    protected override void Dispose(bool disposing) {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() {
      SuspendLayout();
      DoubleBuffered = true;
      btnClose = new CloseButton();
      pbIcon = new PictureBox();
      lblCaption = new Label();

      Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold, GraphicsUnit.Pixel);
      BackColor = Color.FromArgb(0, 0, 0);
      ForeColor = Color.FromArgb(150, 150, 150);

      pbIcon.Location = new Point(0, 0);
      pbIcon.Name = "pbIcon";
      pbIcon.Size = new Size(30, 30);
      pbIcon.MinimumSize = new Size(30, 30);
      pbIcon.AutoSize = false;
      pbIcon.TabIndex = 0;
      pbIcon.TabStop = false;
      pbIcon.Dock = DockStyle.Left;
      pbIcon.Margin = new Padding(3);
      pbIcon.Padding = new Padding(6);
      pbIcon.SizeMode = PictureBoxSizeMode.StretchImage;

      btnClose.BackColor = Color.FromArgb(22, 21, 26);
      btnClose.ForeColor = Color.FromArgb(67, 67, 77);
      btnClose.Location = new Point(0, 0);
      btnClose.Name = "btnClose";
      btnClose.Radius = 5;
      btnClose.Size = new Size(30, 30);
      btnClose.MinimumSize = new Size(30, 30);
      btnClose.AutoSize = false;
      btnClose.TabIndex = 0;
      btnClose.Text = "Close";
      btnClose.UseVisualStyleBackColor = true;
      btnClose.Dock = DockStyle.Right;
      btnClose.Click += new EventHandler(btnClose_Click);
      btnClose.Margin = new Padding(3);

      lblCaption.BackColor = Color.Transparent;
      lblCaption.Location = new Point(0, 0);
      lblCaption.Name = "lblCaption";
      lblCaption.Size = new Size(120, 30);
      lblCaption.MinimumSize = new Size(120, 30);
      lblCaption.AutoSize = true;
      lblCaption.TabIndex = 0;
      lblCaption.TextAlign = ContentAlignment.MiddleLeft;
      lblCaption.Dock = DockStyle.Fill;
      lblCaption.Margin = new Padding(0);
      lblCaption.Padding = new Padding(33,3,33,3);
      
      Controls.Add(pbIcon);
      Controls.Add(lblCaption);
      Controls.Add(btnClose);
      ResumeLayout(false);
    }

    public string TitleText {
      get => _titleText;
      set {
        _titleText = value;
        lblCaption.Text = value;
      }
    }

    public Image Icon {
      get => _icon;
      set {
        _icon = value;
        pbIcon.Image = value;
      }
    }

    public event EventHandler CloseButtonClicked;

    public HeaderPanel() {
      InitializeComponent();

      // fix up caption color
      lblCaption.ForeColor = base.ForeColor;

      // wtf ffs
      /*_TextColor = ForeColor;
      _TitleFont = new Font(Font.FontFamily, FontSize.Title, FontStyle.Bold, GraphicsUnit.Pixel);*/
      _OuterBorderColor = Color.FromArgb(0, 0, 16);
      _InnerBorderColor = Color.FromArgb(67, 67, 77);
      _ShineColor = Color.FromArgb(96, 96, 104);
      _GlowColor = Color.FromArgb(48, 48, 128);

      lblCaption.Text = _titleText;
      lblCaption.Invalidate();
      pbIcon.Image = _icon;
      pbIcon.Invalidate();
    }

    public override Color ForeColor {
      get => lblCaption?.ForeColor ?? base.ForeColor;
      set {
        base.ForeColor = value;
        if (lblCaption is not null)
          lblCaption.ForeColor = value;
      }
    }

    protected override void OnMouseEnter(EventArgs e) {
      Hovered = true;
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e) {
      Hovered = false;
      base.OnMouseLeave(e);
    }

    public void SetFont() {
    }

    protected override void OnPaintBackground(PaintEventArgs e) {
      DrawBackground(e.Graphics, ClientRectangle,
        Pressed, Hovered, Animating, true,
        _OuterBorderColor, BackColor, _GlowColor, _ShineColor, _InnerBorderColor,
        0.0f, 0);
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
      int radius) {
      var smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      var rectangle1 = rectangle;
      --rectangle1.Width;
      --rectangle1.Height;
      using (var path = new GraphicsPath()) {
        path.AddRectangle(rectangle1);
        using (var pen = new Pen(outerBorderColor))
          g.DrawPath(pen, path);
      }

      ++rectangle1.X;
      ++rectangle1.Y;
      rectangle1.Width -= 2;
      rectangle1.Height -= 2;
      var rectangle2 = rectangle1;
      rectangle2.Height >>= 1;
      using (var path = new GraphicsPath()) {
        path.AddRectangle(rectangle1);
        using (Brush brush = new SolidBrush(backColor))
          g.FillPath(brush, path);
      }

      if ((hovered || animating) && !pressed) {
        using var path = new GraphicsPath();
        path.AddRectangle(rectangle1);
        g.SetClip(path, CombineMode.Intersect);
        using (var bottomRadialPath = CreateBottomRadialPath(rectangle1)) {
          using var pathGradientBrush = new PathGradientBrush(bottomRadialPath);
          var alpha = (int)(178.0 * glowOpacity + 0.5);
          var bounds = bottomRadialPath.GetBounds();
          pathGradientBrush.CenterPoint = new PointF((float)((bounds.Left + (double)bounds.Right) / 2.0), (float)((bounds.Top + (double)bounds.Bottom) / 2.0));
          pathGradientBrush.CenterColor = Color.FromArgb(alpha, glowColor);
          pathGradientBrush.SurroundColors = new Color[1] {
            Color.FromArgb(0, glowColor)
          };
          g.FillPath(pathGradientBrush, bottomRadialPath);
        }

        g.ResetClip();
      }

      if (rectangle2 is { Width: > 0, Height: > 0 }) {
        ++rectangle2.Height;
        using (var topRoundRectangle = CreateTopRoundRectangle(rectangle2, radius - 2)) {
          ++rectangle2.Height;
          var alpha = 153;
          if (pressed | !enabled)
            alpha = (int)(0.40000000596046448 * alpha + 0.5);
          using (var linearGradientBrush = new LinearGradientBrush(rectangle2, Color.FromArgb(alpha, shineColor), Color.FromArgb(alpha / 3, shineColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            g.FillPath(linearGradientBrush, topRoundRectangle);
        }

        rectangle2.Height -= 2;
      }

      using (var path = new GraphicsPath()) {
        path.AddRectangle(rectangle1);
        using (var pen = new Pen(innerBorderColor))
          g.DrawPath(pen, path);
      }

      g.SmoothingMode = smoothingMode;
    }

    private static GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius) {
      var topRoundRectangle = new GraphicsPath();
      topRoundRectangle.AddRectangle(rectangle);
      topRoundRectangle.CloseFigure();
      return topRoundRectangle;
    }

    private static GraphicsPath CreateBottomRadialPath(Rectangle rectangle) {
      var bottomRadialPath = new GraphicsPath();
      RectangleF rect = rectangle;
      rect.X -= rect.Width * 0.35f;
      rect.Y -= rect.Height * 0.15f;
      rect.Width *= 1.7f;
      rect.Height *= 2.3f;
      bottomRadialPath.AddEllipse(rect);
      bottomRadialPath.CloseFigure();
      return bottomRadialPath;
    }

    private void btnClose_Click(object sender, EventArgs e)
      => CloseButtonClicked?.Invoke(sender, e);

  }

}