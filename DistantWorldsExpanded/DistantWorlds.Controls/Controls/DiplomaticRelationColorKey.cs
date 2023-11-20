// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DiplomaticRelationColorKey
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DiplomaticRelationColorKey : GradientPanel
  {
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
    private SolidBrush _MutualDefenseBrush = new SolidBrush(Color.FromArgb(64, 64, 232));
    private SolidBrush _ProtectorateBrush = new SolidBrush(Color.FromArgb(112, 112, (int) byte.MaxValue));
    private SolidBrush _FreeTradeBrush = new SolidBrush(Color.FromArgb(0, (int) byte.MaxValue, 0));
    private SolidBrush _NoneBrush = new SolidBrush(Color.FromArgb(128, 128, 128));
    private SolidBrush _TruceBrush = new SolidBrush(Color.Yellow);
    private SolidBrush _SubjugatedBrush = new SolidBrush(Color.Yellow);
    private SolidBrush _TradeSanctionsBrush = new SolidBrush(Color.Orange);
    private SolidBrush _WarBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, 0, 0));
    private SolidBrush _NotMetBrush = new SolidBrush(Color.Tan);
    private SolidBrush _PirateProtectionBrush = new SolidBrush(Color.FromArgb(160, 160, (int) byte.MaxValue));
    private SolidBrush _PirateAttackBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, 160, 160));
    private SolidBrush _PirateAllianceBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
    private SolidBrush _WhiteBrush = new SolidBrush(Color.White);
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _TitleFont;
    private IContainer components;

    public override void SetFontCache(IFontCache fontCache)
    {
      base.SetFontCache(fontCache);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public DiplomaticRelationColorKey()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawColorKey(e.Graphics);
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

    private void DrawColorKey(Graphics graphics)
    {
      int x1 = 10;
      int width1 = 125;
      int y1 = 30;
      Point point = new Point();
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      point = new Point(x1, 10);
      this.DrawStringWithDropShadow(graphics, "Diplomatic Relations", this._TitleFont, point);
      int x2 = 135;
      int height = 3;
      int width2 = 30;
      int num1 = 0;
      int alignedOffsetPosition1 = this.CalculateRightAlignedOffsetPosition(graphics, "Mutual Defense Pact", this.Font, width1);
      point = new Point(num1 + alignedOffsetPosition1, y1);
      graphics.DrawString("Mutual Defense Pact", this.Font, (Brush) this._MutualDefenseBrush, (PointF) point, StringFormat.GenericTypographic);
      Rectangle rect = new Rectangle(x2, y1 + 6, width2, height);
      graphics.FillRectangle((Brush) this._MutualDefenseBrush, rect);
      int y2 = y1 + this._RowHeight;
      int alignedOffsetPosition2 = this.CalculateRightAlignedOffsetPosition(graphics, "Protectorate", this.Font, width1);
      point = new Point(num1 + alignedOffsetPosition2, y2);
      graphics.DrawString("Protectorate", this.Font, (Brush) this._ProtectorateBrush, (PointF) point, StringFormat.GenericTypographic);
      rect = new Rectangle(x2, y2 + 6, width2, height);
      graphics.FillRectangle((Brush) this._ProtectorateBrush, rect);
      int y3 = y2 + this._RowHeight;
      int alignedOffsetPosition3 = this.CalculateRightAlignedOffsetPosition(graphics, "Free Trade Agreement", this.Font, width1);
      point = new Point(num1 + alignedOffsetPosition3, y3);
      graphics.DrawString("Free Trade Agreement", this.Font, (Brush) this._FreeTradeBrush, (PointF) point, StringFormat.GenericTypographic);
      rect = new Rectangle(x2, y3 + 6, width2, height);
      graphics.FillRectangle((Brush) this._FreeTradeBrush, rect);
      int y4 = y3 + this._RowHeight;
      int alignedOffsetPosition4 = this.CalculateRightAlignedOffsetPosition(graphics, "No Relationship", this.Font, width1);
      point = new Point(num1 + alignedOffsetPosition4, y4);
      graphics.DrawString("No Relationship", this.Font, (Brush) this._NoneBrush, (PointF) point, StringFormat.GenericTypographic);
      rect = new Rectangle(x2, y4 + 6, width2, height);
      graphics.FillRectangle((Brush) this._NoneBrush, rect);
      int y5 = y4 + this._RowHeight;
      int alignedOffsetPosition5 = this.CalculateRightAlignedOffsetPosition(graphics, "Subjugated Dominion", this.Font, width1);
      point = new Point(num1 + alignedOffsetPosition5, y5);
      graphics.DrawString("Subjugated Dominion", this.Font, (Brush) this._SubjugatedBrush, (PointF) point, StringFormat.GenericTypographic);
      rect = new Rectangle(x2, y5 + 6, width2, height);
      graphics.FillRectangle((Brush) this._SubjugatedBrush, rect);
      int y6 = y5 + this._RowHeight;
      int alignedOffsetPosition6 = this.CalculateRightAlignedOffsetPosition(graphics, "Trade Sanctions", this.Font, width1);
      point = new Point(num1 + alignedOffsetPosition6, y6);
      graphics.DrawString("Trade Sanctions", this.Font, (Brush) this._TradeSanctionsBrush, (PointF) point, StringFormat.GenericTypographic);
      rect = new Rectangle(x2, y6 + 6, width2, height);
      graphics.FillRectangle((Brush) this._TradeSanctionsBrush, rect);
      int y7 = y6 + this._RowHeight;
      int alignedOffsetPosition7 = this.CalculateRightAlignedOffsetPosition(graphics, "War", this.Font, width1);
      point = new Point(num1 + alignedOffsetPosition7, y7);
      graphics.DrawString("War", this.Font, (Brush) this._WarBrush, (PointF) point, StringFormat.GenericTypographic);
      rect = new Rectangle(x2, y7 + 6, width2, height);
      graphics.FillRectangle((Brush) this._WarBrush, rect);
      int y8 = y7 + this._RowHeight + this._RowHeight;
      int alignedOffsetPosition8 = this.CalculateRightAlignedOffsetPosition(graphics, "Pirate Protection", this.Font, width1);
      point = new Point(num1 + alignedOffsetPosition8, y8);
      graphics.DrawString("Pirate Protection", this.Font, (Brush) this._PirateProtectionBrush, (PointF) point, StringFormat.GenericTypographic);
      rect = new Rectangle(x2, y8 + 6, width2, height);
      graphics.FillRectangle((Brush) this._PirateProtectionBrush, rect);
      int num2 = y8 + this._RowHeight;
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
