// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireComparison
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class EmpireComparison : GradientPanel
  {
    private IContainer components;
    private Game _Game;
    private EmpireComparisonType _ComparisonType;
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
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

    public EmpireComparison()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void ClearData() => this._Game = (Game) null;

    public void Ignite(Game game, EmpireComparisonType comparisonType)
    {
      this._Game = game;
      this._ComparisonType = comparisonType;
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawEmpireComparison(e.Graphics);
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

    private void DrawEmpireComparison(Graphics graphics)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int num1 = this._TopMargin + 30;
      int num2 = this._LeftMargin + 40;
      int overallWidth = 800;
      int num3 = this.ClientRectangle.Height - (this._TopMargin + num1);
      int num4 = num3 / 25;
      int topMargin = this._TopMargin;
      string text1 = string.Empty;
      switch (this._ComparisonType)
      {
        case EmpireComparisonType.Undefined:
          text1 = "";
          break;
        case EmpireComparisonType.Population:
          text1 = TextResolver.GetText("Population");
          break;
        case EmpireComparisonType.Territory:
          text1 = TextResolver.GetText("Territory - Colonies");
          break;
        case EmpireComparisonType.Economy:
          text1 = TextResolver.GetText("Economy - Annual GDP");
          break;
        case EmpireComparisonType.StrategicValue:
          text1 = TextResolver.GetText("Strategic Value");
          break;
        case EmpireComparisonType.MilitaryStrength:
          text1 = TextResolver.GetText("Military Strength");
          break;
      }
      Point location1 = new Point(this._LeftMargin, topMargin);
      this.DrawStringWithDropShadow(graphics, text1, this._TitleFont, location1);
      int y = num1 + 7;
      if (this._Game == null)
        return;
      EmpirePriorityList orderedKnownEmpires = this._Game.Galaxy.DetermineOrderedKnownEmpires(this._Game.PlayerEmpire, this._ComparisonType);
      Pen pen = new Pen(Color.White);
      graphics.DrawLine(pen, num2, num1, num2 + overallWidth, num1);
      graphics.DrawLine(pen, num2, num1, num2, num1 + num3);
      string format = string.Empty;
      switch (this._ComparisonType)
      {
        case EmpireComparisonType.Population:
          format = "0,,M";
          break;
        case EmpireComparisonType.Territory:
          format = TextResolver.GetText("colonies format");
          break;
        case EmpireComparisonType.Economy:
          format = TextResolver.GetText("credits format");
          break;
        case EmpireComparisonType.StrategicValue:
          format = "0,K";
          break;
        case EmpireComparisonType.MilitaryStrength:
          format = TextResolver.GetText("firepower format");
          break;
      }
      string text2 = orderedKnownEmpires[0].Priority.ToString(format);
      int width = (int) graphics.MeasureString(text2, this._BoldFont, 200, StringFormat.GenericTypographic).Width;
      Point location2 = new Point(num2 + overallWidth - width, num1 - 14);
      this.DrawStringWithDropShadow(graphics, text2, this._BoldFont, location2);
      double num5 = (double) overallWidth / orderedKnownEmpires[0].Priority;
      int num6 = 0;
      foreach (EmpirePriority empirePriority in (SyncList<EmpirePriority>) orderedKnownEmpires)
      {
        Rectangle rect = new Rectangle(this._LeftMargin, y, 33, 20);
        graphics.DrawImage((Image) empirePriority.Empire.LargeFlagPicture, rect);
        Point location3 = new Point(num2 + 1, y);
        if (empirePriority.Empire == this._Game.PlayerEmpire)
          this.DrawBarGraph((int) (orderedKnownEmpires[0].Priority * num5), (int) (empirePriority.Priority * num5), 20, overallWidth, Color.FromArgb(150, 96, 0, 48), Color.FromArgb(150, (int) byte.MaxValue, 0, 128), Color.FromArgb((int) sbyte.MaxValue, 32, 32, 128), graphics, location3, (num6 + 1).ToString() + ". " + empirePriority.Empire.Name);
        else
          this.DrawBarGraph((int) (orderedKnownEmpires[0].Priority * num5), (int) (empirePriority.Priority * num5), 20, overallWidth, Color.FromArgb(150, 48, 0, 96), Color.FromArgb(150, 128, 0, (int) byte.MaxValue), Color.FromArgb((int) sbyte.MaxValue, 32, 32, 128), graphics, location3, (num6 + 1).ToString() + ". " + empirePriority.Empire.Name);
        y += 25;
        ++num6;
        if (num6 > num4)
          break;
      }
    }

    private void DrawBarGraph(
      int maximumValue,
      int currentValue,
      int height,
      int overallWidth,
      Color fillColorStart,
      Color fillColorEnd,
      Color backgroundColor,
      Graphics graphics,
      Point location,
      string description)
    {
      int num = (int) graphics.MeasureString("9999", this.Font, 200, StringFormat.GenericTypographic).Width + 5;
      Point point1 = new Point(location.X, location.Y);
      int width1 = overallWidth;
      int width2 = (int) ((double) currentValue / (double) maximumValue * (double) width1);
      if (width2 > width1)
        width2 = width1;
      int width3 = (int) graphics.MeasureString(description, this.Font).Width;
      Point point2 = new Point(point1.X + (width1 - width3) / 2, point1.Y);
      Rectangle rect1 = new Rectangle(point1.X, point1.Y, width1, height);
      Rectangle rect2 = new Rectangle(point1.X, point1.Y, width2, height);
      Rectangle rect3 = new Rectangle(point1.X - 1, point1.Y, width2 + 2, height);
      LinearGradientBrush linearGradientBrush = (LinearGradientBrush) null;
      if (rect2.Width > 0)
        linearGradientBrush = new LinearGradientBrush(rect3, fillColorStart, fillColorEnd, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
      SolidBrush solidBrush = new SolidBrush(backgroundColor);
      graphics.FillRectangle((Brush) solidBrush, rect1);
      if (rect2.Width > 0)
        graphics.FillRectangle((Brush) linearGradientBrush, rect2);
      graphics.DrawString(description, this.Font, (Brush) this._WhiteBrush, new PointF((float) point2.X, (float) point2.Y + 3f));
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
      graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location, StringFormat.GenericTypographic);
      location = new Point(location.X - 1, location.Y - 1);
      graphics.DrawString(text, font, (Brush) this._WhiteBrush, (PointF) location, StringFormat.GenericTypographic);
    }
  }
}
