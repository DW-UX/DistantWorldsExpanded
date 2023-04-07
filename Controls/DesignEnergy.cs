// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DesignEnergy
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DesignEnergy : GradientPanel
  {
    private Galaxy _Galaxy;
    private Design _Design;
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _TitleFont;
    private IContainer components;

    public DesignEnergy()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(15.33f);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._Design = (Design) null;
    }

    public void Ignite(Galaxy galaxy, Design design)
    {
      this._Galaxy = galaxy;
      this._Design = design;
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawEnergyInfo(e.Graphics);
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

    private void DrawEnergyInfo(Graphics graphics)
    {
      int x1 = 8;
      int width = 142;
      int x2 = 160;
      int y1 = 30;
      Point point = new Point();
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      point = new Point(x1, 10);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Energy"), this._TitleFont, point);
      if (this._Design == null)
        return;
      int alignedOffsetPosition1 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Energy Collection"), this.Font, width);
      point = new Point(x1 + alignedOffsetPosition1, y1);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Energy Collection"), this.Font, point);
      point = new Point(x2, y1);
      this.DrawStringWithDropShadow(graphics, this._Design.EnergyCollection.ToString(), this._BoldFont, point);
      int y2 = y1 + this._RowHeight;
      int alignedOffsetPosition2 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Reactor Power Output"), this.Font, width);
      point = new Point(x1 + alignedOffsetPosition2, y2);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Reactor Power Output"), this.Font, point);
      point = new Point(x2, y2);
      this.DrawStringWithDropShadow(graphics, this._Design.ReactorPowerOutput.ToString(), this._BoldFont, point);
      int y3 = y2 + this._RowHeight;
      int alignedOffsetPosition3 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Static Energy Usage"), this.Font, width);
      point = new Point(x1 + alignedOffsetPosition3, y3);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Static Energy Usage"), this.Font, point);
      point = new Point(x2, y3);
      this.DrawStringWithDropShadow(graphics, this._Design.StaticEnergyConsumption.ToString(), this._BoldFont, point);
      int y4 = y3 + this._RowHeight;
      int alignedOffsetPosition4 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Excess Energy Output"), this.Font, width);
      point = new Point(x1 + alignedOffsetPosition4, y4);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Excess Energy Output"), this.Font, point);
      point = new Point(x2, y4);
      double num1 = (double) (this._Design.ReactorPowerOutput - this._Design.StaticEnergyConsumption);
      SolidBrush solidBrush = this.SelectGoodBadBrush(num1);
      graphics.DrawString(num1.ToString("###0"), this._BoldFont, (Brush) solidBrush, (PointF) point);
      int y5 = y4 + this._RowHeight + this._RowHeight;
      point = new Point(x1, y5);
      string str1 = TextResolver.GetText("Fuel Type") + " = ";
      string str2 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.FuelType != null)
        str2 = this._Design.FuelType.Name;
      string text1 = str1 + str2;
      this.DrawStringWithDropShadow(graphics, text1, this.Font, point);
      int y6 = y5 + this._RowHeight;
      int alignedOffsetPosition5 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Fuel Capacity"), this.Font, width);
      point = new Point(x1 + alignedOffsetPosition5, y6);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Fuel Capacity"), this.Font, point);
      point = new Point(x2, y6);
      this.DrawStringWithDropShadow(graphics, this._Design.FuelCapacity.ToString(), this._BoldFont, point);
      int y7 = y6 + this._RowHeight;
      int alignedOffsetPosition6 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Energy Storage"), this.Font, width);
      point = new Point(x1 + alignedOffsetPosition6, y7);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Energy Storage"), this.Font, point);
      point = new Point(x2, y7);
      this.DrawStringWithDropShadow(graphics, this._Design.ReactorStorageCapacity.ToString(), this._BoldFont, point);
      int y8 = y7 + this._RowHeight;
      double num2 = (double) this._Design.ReactorCycleFuelConsumption / 1000.0 / (double) this._Design.ReactorStorageCapacity * 1000.0;
      string text2 = string.Format(TextResolver.GetText("X fuel units per 1000 energy units"), (object) num2.ToString("0.00"));
      point = new Point(x1, y8);
      this.DrawStringWithDropShadow(graphics, text2, this.Font, point);
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }
  }
}
