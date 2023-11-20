// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResearchSummary
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ResearchSummary : GradientPanel
  {
    private Galaxy _Galaxy;
    private Empire _Empire;
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

    public ResearchSummary()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._Empire = (Empire) null;
    }

    public void Ignite(Galaxy galaxy, Empire empire)
    {
      this._Galaxy = galaxy;
      this._Empire = empire;
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawResearchSummary(e.Graphics);
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

    private void DrawResearchSummary(Graphics graphics)
    {
      int leftMargin = this._LeftMargin;
      int x1 = this._LeftMargin + 325;
      int x2 = this._LeftMargin + 410;
      int x3 = this._LeftMargin + 495;
      int topMargin = this._TopMargin;
      Point location = new Point(this._LeftMargin, topMargin);
      if (this._Empire == null)
        return;
      location = new Point(leftMargin, topMargin);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Total Research Capacity"), this._TitleFont, location);
      location = new Point(x1, topMargin);
      this.DrawStringWithDropShadow(graphics, this._Empire.ResearchWeaponsPotential.ToString("#0,K"), this._TitleFont, location);
      location = new Point(x2, topMargin);
      this.DrawStringWithDropShadow(graphics, this._Empire.ResearchEnergyPotential.ToString("#0,K"), this._TitleFont, location);
      location = new Point(x3, topMargin);
      this.DrawStringWithDropShadow(graphics, this._Empire.ResearchHighTechPotential.ToString("#0,K"), this._TitleFont, location);
      int y = topMargin + (int) ((double) this._RowHeight * 1.7);
      Rectangle rect = new Rectangle(this._LeftMargin - 2, y - 1, this.ClientRectangle.Width - this._LeftMargin * 2, this._RowHeight + 8);
      SolidBrush solidBrush = new SolidBrush(Color.FromArgb(96, (int) byte.MaxValue, 64, 128));
      graphics.FillRectangle((Brush) solidBrush, rect);
      location = new Point(leftMargin, y);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Actual Output (including bonuses)"), this._TitleFont, location);
      location = new Point(x1, y);
      this.DrawStringWithDropShadow(graphics, this._Empire.ResearchWeaponsOutput.ToString("#0,K"), this._TitleFont, location);
      location = new Point(x2, y);
      this.DrawStringWithDropShadow(graphics, this._Empire.ResearchEnergyOutput.ToString("#0,K"), this._TitleFont, location);
      location = new Point(x3, y);
      this.DrawStringWithDropShadow(graphics, this._Empire.ResearchHighTechOutput.ToString("#0,K"), this._TitleFont, location);
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
