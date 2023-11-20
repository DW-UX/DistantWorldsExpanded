// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DesignIndustry
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DesignIndustry : GradientPanel
  {
    private Galaxy _Galaxy;
    private Design _Design;
    private int _RowHeight = 13;
    private int _TopMargin = 7;
    private int _LeftMargin = 8;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _TitleFont;
    private IContainer components;

    public DesignIndustry()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
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
      this.DrawIndustryInfo(e.Graphics);
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

    private void DrawIndustryInfo(Graphics graphics)
    {
      int num1 = 10;
      int width = 90;
      int x = 110;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      int y1 = 28;
      Point location = new Point();
      int num2 = 2;
      location = new Point(8, 8);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Industry"), this._TitleFont, location);
      if (this._Design == null)
        return;
      int alignedOffsetPosition1 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Cargo Capacity"), this.Font, width);
      location = new Point(num1 + alignedOffsetPosition1, y1);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Cargo Capacity"), this.Font, location);
      location = new Point(x, y1 - num2);
      string text1 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.CargoCapacity > 0)
        text1 = this._Design.CargoCapacity.ToString();
      this.DrawStringWithDropShadow(graphics, text1, this._BoldFont, location);
      int y2 = y1 + this._RowHeight;
      int alignedOffsetPosition2 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Medical"), this.Font, width);
      location = new Point(num1 + alignedOffsetPosition2, y2);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Medical"), this.Font, location);
      location = new Point(x, y2 - num2);
      string text2 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.MedicalCapacity > 0)
        text2 = this._Design.MedicalCapacity.ToString();
      this.DrawStringWithDropShadow(graphics, text2, this._BoldFont, location);
      int num3 = this.Width / 2 + 10;
      location = new Point(num1 + num3, y2);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Recreation"), this.Font, location);
      SizeF sizeF = graphics.MeasureString(TextResolver.GetText("Recreation"), this.Font);
      location = new Point(num3 + (int) sizeF.Width + 10, y2 - num2);
      string text3 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.RecreationCapacity > 0)
        text3 = this._Design.RecreationCapacity.ToString();
      this.DrawStringWithDropShadow(graphics, text3, this._BoldFont, location);
      int y3 = y2 + this._RowHeight;
      int alignedOffsetPosition3 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Research"), this.Font, width);
      location = new Point(num1 + alignedOffsetPosition3, y3);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Research"), this.Font, location);
      location = new Point(x, y3 - num2);
      string str1 = string.Empty;
      if (this._Design.ResearchWeapons > 0)
        str1 = str1 + "W:" + this._Design.ResearchWeapons.ToString("0,K") + ", ";
      if (this._Design.ResearchEnergy > 0)
        str1 = str1 + "E:" + this._Design.ResearchEnergy.ToString("0,K") + ", ";
      if (this._Design.ResearchHighTech > 0)
        str1 = str1 + "H:" + this._Design.ResearchHighTech.ToString("0,K") + ", ";
      string text4 = !string.IsNullOrEmpty(str1) ? str1.Substring(0, str1.Length - 2) : "(" + TextResolver.GetText("None") + ")";
      location = new Point(x, y3 - num2);
      this.DrawStringWithDropShadow(graphics, text4, this._BoldFont, location);
      int y4 = y3 + this._RowHeight + this._RowHeight / 2;
      int alignedOffsetPosition4 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Mining"), this.Font, width);
      location = new Point(num1 + alignedOffsetPosition4, y4);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Mining"), this.Font, location);
      location = new Point(x, y4 - num2);
      string str2 = string.Empty;
      if (this._Design.ExtractionMine > 0)
        str2 = str2 + this._Design.ExtractionMine.ToString() + " " + TextResolver.GetText("Normal").ToLower(CultureInfo.InvariantCulture) + ", ";
      if (this._Design.ExtractionGas > 0)
        str2 = str2 + this._Design.ExtractionGas.ToString() + " " + TextResolver.GetText("Gas").ToLower(CultureInfo.InvariantCulture) + ", ";
      if (this._Design.ExtractionLuxury > 0)
        str2 = str2 + this._Design.ExtractionLuxury.ToString() + " " + TextResolver.GetText("Luxury").ToLower(CultureInfo.InvariantCulture) + ", ";
      string text5 = !string.IsNullOrEmpty(str2) ? str2.Substring(0, str2.Length - 2) : "(" + TextResolver.GetText("None") + ")";
      this.DrawStringWithDropShadow(graphics, text5, this._BoldFont, location);
      int y5 = y4 + this._RowHeight;
      int alignedOffsetPosition5 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Manufacturing"), this.Font, width);
      location = new Point(num1 + alignedOffsetPosition5, y5);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Manufacturing"), this.Font, location);
      location = new Point(x, y5 - num2);
      string str3 = string.Empty;
      if (this._Design.ManufactureWeapons > 0)
        str3 = str3 + "W:" + this._Design.ManufactureWeapons.ToString("0,K") + ", ";
      if (this._Design.ManufactureEnergy > 0)
        str3 = str3 + "E:" + this._Design.ManufactureEnergy.ToString("0,K") + ", ";
      if (this._Design.ManufactureHighTech > 0)
        str3 = str3 + "H:" + this._Design.ManufactureHighTech.ToString("0,K") + ", ";
      string text6 = !string.IsNullOrEmpty(str3) ? str3.Substring(0, str3.Length - 2) : "(" + TextResolver.GetText("None") + ")";
      this.DrawStringWithDropShadow(graphics, text6, this._BoldFont, location);
      int y6 = y5 + this._RowHeight;
      int alignedOffsetPosition6 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Construction"), this.Font, width);
      location = new Point(num1 + alignedOffsetPosition6, y6);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Construction"), this.Font, location);
      location = new Point(x, y6 - num2);
      string text7 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.ConstructionYardCount > 0)
        text7 = this._Design.ConstructionYardCount.ToString() + " " + TextResolver.GetText("Yards").ToLower(CultureInfo.InvariantCulture);
      this.DrawStringWithDropShadow(graphics, text7, this._BoldFont, location);
      int y7 = y6 + this._RowHeight;
      int alignedOffsetPosition7 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Docking Bays"), this.Font, width);
      location = new Point(num1 + alignedOffsetPosition7, y7);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Docking Bays"), this.Font, location);
      location = new Point(x, y7 - num2);
      string text8 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.DockingBayCount > 0)
        text8 = this._Design.DockingBayCount.ToString() + " " + TextResolver.GetText("Bays").ToLower(CultureInfo.InvariantCulture);
      this.DrawStringWithDropShadow(graphics, text8, this._BoldFont, location);
      int num4 = y7 + this._RowHeight;
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
