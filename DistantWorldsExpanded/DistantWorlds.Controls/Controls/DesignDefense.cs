// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DesignDefense
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
  public class DesignDefense : GradientPanel
  {
    private IContainer components;
    private Galaxy _Galaxy;
    private Design _Design;
    private int _RowHeight = 14;
    private int _TopMargin = 8;
    private int _LeftMargin = 8;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _TitleFont;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    public DesignDefense()
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
      this.DrawDefenseInfo(e.Graphics);
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

    private void DrawDefenseInfo(Graphics graphics)
    {
      int x1 = 10;
      int width = 140;
      int x2 = 160;
      int y1 = 25;
      Point location = new Point();
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      location = new Point(x1, 10);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Defense"), this._TitleFont, location);
      if (this._Design == null)
        return;
      int alignedOffsetPosition1 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Shields"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition1, y1);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Shields"), this.Font, location);
      location = new Point(x2, y1);
      this.DrawStringWithDropShadow(graphics, this._Design.ShieldsCapacity.ToString(), this._BoldFont, location);
      int y2 = y1 + this._RowHeight;
      int alignedOffsetPosition2 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Shield Recharge Rate"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition2, y2);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Shield Recharge Rate"), this.Font, location);
      location = new Point(x2, y2);
      this.DrawStringWithDropShadow(graphics, this._Design.ShieldRechargeRate.ToString(), this._BoldFont, location);
      int y3 = y2 + this._RowHeight;
      int alignedOffsetPosition3 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Shield Area Recharge Range"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition3, y3);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Shield Area Recharge Range"), this.Font, location);
      location = new Point(x2, y3);
      string text1 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.ShieldAreaRechargeRange > (short) 0)
        text1 = this._Design.ShieldAreaRechargeRange.ToString();
      this.DrawStringWithDropShadow(graphics, text1, this._BoldFont, location);
      int y4 = y3 + this._RowHeight;
      int alignedOffsetPosition4 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Armor"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition4, y4);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Armor"), this.Font, location);
      location = new Point(x2, y4);
      this.DrawStringWithDropShadow(graphics, this._Design.Armor.ToString(), this._BoldFont, location);
      int y5 = y4 + this._RowHeight;
      int alignedOffsetPosition5 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Reactive Armor Strength"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition5, y5);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Reactive Armor Strength"), this.Font, location);
      location = new Point(x2, y5);
      this.DrawStringWithDropShadow(graphics, this._Design.ArmorReactive.ToString(), this._BoldFont, location);
      int y6 = y5 + this._RowHeight;
      int alignedOffsetPosition6 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Countermeasures"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition6, y6);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Countermeasures"), this.Font, location);
      location = new Point(x2, y6);
      string text2 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.CountermeasureModifier > 0)
        text2 = "+" + this._Design.CountermeasureModifier.ToString() + "%";
      this.DrawStringWithDropShadow(graphics, text2, this._BoldFont, location);
      int y7 = y6 + this._RowHeight;
      int alignedOffsetPosition7 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Component Type Countermeasures Fleet"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition7, y7);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Component Type Countermeasures Fleet"), this.Font, location);
      location = new Point(x2, y7);
      string text3 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.FleetCountermeasureModifier > (short) 0)
        text3 = "+" + this._Design.FleetCountermeasureModifier.ToString() + "%";
      this.DrawStringWithDropShadow(graphics, text3, this._BoldFont, location);
      int y8 = y7 + this._RowHeight;
      int alignedOffsetPosition8 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Stealth: Visibility Range"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition8, y8);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Stealth: Visibility Range"), this.Font, location);
      location = new Point(x2, y8);
      string text4 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.Stealth < 1.0)
        text4 = "-" + ((1.0 - this._Design.Stealth) * 100.0).ToString("0.0") + "%";
      this.DrawStringWithDropShadow(graphics, text4, this._BoldFont, location);
      int y9 = y8 + this._RowHeight;
      int alignedOffsetPosition9 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Boarding Defense Strength"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition9, y9);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Boarding Defense Strength"), this.Font, location);
      location = new Point(x2, y9);
      string text5 = "(" + TextResolver.GetText("None") + ")";
      int boardingDefenseValue = this._Design.CalculateBoardingDefenseValue(this._Galaxy.PlayerEmpire.DominantRace);
      if (boardingDefenseValue > 0)
        text5 = boardingDefenseValue.ToString("0");
      this.DrawStringWithDropShadow(graphics, text5, this._BoldFont, location);
      int y10 = y9 + this._RowHeight;
      int alignedOffsetPosition10 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Damage Reduction"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition10, y10);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Damage Reduction"), this.Font, location);
      location = new Point(x2, y10);
      string text6 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.DamageReduction > 0.0)
        text6 = (this._Design.DamageReduction * 100.0).ToString("#0") + "%";
      this.DrawStringWithDropShadow(graphics, text6, this._BoldFont, location);
      int y11 = y10 + this._RowHeight;
      int alignedOffsetPosition11 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Repair Component"), this.Font, width);
      location = new Point(x1 + alignedOffsetPosition11, y11);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Repair Component"), this.Font, location);
      location = new Point(x2, y11);
      string text7 = "(" + TextResolver.GetText("None") + ")";
      if (this._Design.DamageRepair > 0)
        text7 = ((double) this._Design.DamageRepair).ToString("#0") + " " + TextResolver.GetText("seconds abbreviation");
      this.DrawStringWithDropShadow(graphics, text7, this._BoldFont, location);
      int num = y11 + this._RowHeight;
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
  }
}
