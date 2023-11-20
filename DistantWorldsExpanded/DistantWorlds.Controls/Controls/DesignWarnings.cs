// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DesignWarnings
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DesignWarnings : GradientPanel
  {
    private Galaxy _Galaxy;
    private Design _Design;
    private List<string> _MustDo;
    private List<string> _ShouldDo;
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private SolidBrush _YellowBrush = new SolidBrush(Color.Yellow);
    private Font _BoldFont;
    private Font _TitleFont;
    private IContainer components;

    public DesignWarnings()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.BackColor = Color.Transparent;
      this.BackColor2 = Color.Transparent;
      this.BackColor3 = Color.Transparent;
      this.GradientMode = LinearGradientMode.None;
      this.CurveMode = CornerCurveMode.None;
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._Design = (Design) null;
    }

    public void Ignite(Galaxy galaxy, Design design, List<string> mustDo, List<string> shouldDo)
    {
      this._Galaxy = galaxy;
      this._Design = design;
      this._MustDo = mustDo;
      this._ShouldDo = shouldDo;
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.Height = 0;
      this.BackColor = Color.Transparent;
      this.BackColor2 = Color.Transparent;
      this.BackColor3 = Color.Transparent;
      this.GradientMode = LinearGradientMode.None;
      this.CurveMode = CornerCurveMode.None;
      this.BorderStyle = BorderStyle.None;
      this.BorderWidth = 0;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawWarningInfo(e.Graphics);
    }

    private int CalculateCenterAlignedOffsetPosition(
      Graphics graphics,
      string text,
      Font font,
      int width)
    {
      int width1 = (int) graphics.MeasureString(text, font, width, StringFormat.GenericTypographic).Width;
      return (width - width1) / 2;
    }

    private int CalculateRightAlignedOffsetPosition(
      Graphics graphics,
      string text,
      Font font,
      int width)
    {
      int width1 = (int) graphics.MeasureString(text, font, width, StringFormat.GenericTypographic).Width;
      return width - width1;
    }

    public void DrawWarningInfo(Graphics graphics)
    {
      int x = 10;
      int y = 30;
      Point location1 = new Point();
      location1 = new Point(x, 10);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Warnings"), this._TitleFont, location1);
      if (this._Design == null)
        return;
      SolidBrush redBrush = this._RedBrush;
      foreach (string text in this._MustDo)
      {
        Point location2 = new Point(x, y);
        SizeF maxSize = graphics.MeasureString(text, this._BoldFont, this.Width);
        GraphicsHelper.DrawStringWithDropShadow(graphics, text, this._BoldFont, location2, (Brush) redBrush, maxSize);
        y += (int) maxSize.Height - 4;
      }
      SolidBrush yellowBrush = this._YellowBrush;
      foreach (string text in this._ShouldDo)
      {
        Point location3 = new Point(x, y);
        SizeF maxSize = graphics.MeasureString(text, this._BoldFont, this.Width);
        GraphicsHelper.DrawStringWithDropShadow(graphics, text, this._BoldFont, location3, (Brush) yellowBrush, maxSize);
        y += (int) maxSize.Height - 4;
      }
      if (this.Height >= y + 1)
        return;
      this.Height = y + 1;
    }

    private SolidBrush SelectGoodBadBrush(double value) => this.SelectGoodBadBrush(value, true);

    private SolidBrush SelectGoodBadBrush(double value, bool negativeIsBad)
    {
      SolidBrush solidBrush = this._WhiteBrush;
      if (value < 0.0)
        solidBrush = !negativeIsBad ? this._GreenBrush : this._RedBrush;
      else if (value > 0.0)
        solidBrush = !negativeIsBad ? this._RedBrush : this._GreenBrush;
      return solidBrush;
    }

    private SolidBrush SelectBrush(double value) => this.SelectBrush(value, true);

    private SolidBrush SelectBrush(double value, bool negativeIsRed)
    {
      SolidBrush solidBrush = this._WhiteBrush;
      if (value < 0.0)
        solidBrush = this._RedBrush;
      return solidBrush;
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      location = new Point(location.X + 1, location.Y + 1);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location);
      location = new Point(location.X - 1, location.Y - 1);
      graphics.DrawString(text, font, (Brush) this._WhiteBrush, (PointF) location);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.ResumeLayout(false);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }
  }
}
