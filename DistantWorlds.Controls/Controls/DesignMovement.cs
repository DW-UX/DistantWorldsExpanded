// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DesignMovement
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DesignMovement : GradientPanel
  {
    private IContainer components;
    private Galaxy _Galaxy;
    private Design _Design;
    private int _RowHeight = 16;
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

    public DesignMovement()
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
      this.DrawMovementInfo(e.Graphics);
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

    private void DrawMovementInfo(Graphics graphics)
    {
      int width1 = 60;
      int x1 = 10;
      int width2 = 55;
      int num1 = 70;
      int width3 = 55;
      int x2 = 125;
      int width4 = 55;
      int x3 = 180;
      int width5 = 55;
      int x4 = 235;
      int y1 = this._TopMargin + this._RowHeight + 4;
      int y2 = this._TopMargin + this._RowHeight * 2 + 4;
      int y3 = this._TopMargin + this._RowHeight * 3 + 4;
      int num2 = this._TopMargin + this._RowHeight * 4 + 4;
      int num3 = this._TopMargin + this._RowHeight * 4 + 60 + 4;
      int y4 = this._TopMargin + this._RowHeight * 4 + 66 + 4;
      int y5 = this._TopMargin + this._RowHeight * 5 + 72 + 4;
      SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb(80, (int) byte.MaxValue, (int) byte.MaxValue, 80));
      SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(80, (int) byte.MaxValue, 168, 80));
      SolidBrush solidBrush3 = new SolidBrush(Color.FromArgb(80, (int) byte.MaxValue, 80, 80));
      SolidBrush solidBrush4 = new SolidBrush(Color.FromArgb(80, 80, 80, (int) byte.MaxValue));
      SolidBrush solidBrush5 = new SolidBrush(Color.FromArgb(90, 140, 140, 140));
      SolidBrush solidBrush6 = new SolidBrush(Color.FromArgb(90, 80, 80, 80));
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      Point location = new Point();
      location = new Point(8, 8);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Movement"), this._TitleFont, location);
      if (this._Design == null)
        return;
      if (this._Design.TopSpeed <= 0)
      {
        location = new Point(this.CalculateCenterAlignedOffsetPosition(graphics, "(" + TextResolver.GetText("No movement") + ")", this._BoldFont, 300), 75);
        this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("No movement") + ")", this._BoldFont, location);
      }
      else
      {
        graphics.FillRectangle((Brush) solidBrush5, x1, y2, 280, y3 - y2);
        graphics.FillRectangle((Brush) solidBrush6, x1, y3, 280, num2 - y3);
        graphics.FillRectangle((Brush) solidBrush1, num1, y1, width2, num3 - y1);
        graphics.FillRectangle((Brush) solidBrush2, x2, y1, width3, num3 - y1);
        graphics.FillRectangle((Brush) solidBrush3, x3, y1, width4, num3 - y1);
        graphics.FillRectangle((Brush) solidBrush4, x4, y1, width5, num3 - y1);
        int alignedOffsetPosition1 = this.CalculateCenterAlignedOffsetPosition(graphics, TextResolver.GetText("Impulse"), this.Font, width2);
        location = new Point(num1 + alignedOffsetPosition1, y1);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Impulse"), this.Font, location);
        int alignedOffsetPosition2 = this.CalculateCenterAlignedOffsetPosition(graphics, TextResolver.GetText("Cruise"), this.Font, width3);
        location = new Point(x2 + alignedOffsetPosition2, y1);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Cruise"), this.Font, location);
        int alignedOffsetPosition3 = this.CalculateCenterAlignedOffsetPosition(graphics, TextResolver.GetText("Sprint"), this.Font, width4);
        location = new Point(x3 + alignedOffsetPosition3, y1);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Sprint"), this.Font, location);
        int alignedOffsetPosition4 = this.CalculateCenterAlignedOffsetPosition(graphics, TextResolver.GetText("Hyper"), this.Font, width5);
        location = new Point(x4 + alignedOffsetPosition4, y1);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Hyper"), this.Font, location);
        int alignedOffsetPosition5 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Speed Abbreviation"), this.Font, width1);
        location = new Point(x1 + alignedOffsetPosition5 - 10, y2);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Speed Abbreviation"), this.Font, location);
        int alignedOffsetPosition6 = this.CalculateCenterAlignedOffsetPosition(graphics, Galaxy.MovementImpulseSpeed.ToString(), this.Font, width2);
        location = new Point(num1 + alignedOffsetPosition6, y2);
        this.DrawStringWithDropShadow(graphics, Galaxy.MovementImpulseSpeed.ToString(), this.Font, location);
        int alignedOffsetPosition7 = this.CalculateCenterAlignedOffsetPosition(graphics, this._Design.CruiseSpeed.ToString(), this.Font, width3);
        location = new Point(x2 + alignedOffsetPosition7, y2);
        this.DrawStringWithDropShadow(graphics, this._Design.CruiseSpeed.ToString(), this.Font, location);
        int alignedOffsetPosition8 = this.CalculateCenterAlignedOffsetPosition(graphics, this._Design.TopSpeed.ToString(), this.Font, width4);
        location = new Point(x3 + alignedOffsetPosition8, y2);
        this.DrawStringWithDropShadow(graphics, this._Design.TopSpeed.ToString(), this.Font, location);
        int alignedOffsetPosition9 = this.CalculateCenterAlignedOffsetPosition(graphics, this._Design.WarpSpeed.ToString(), this.Font, width5);
        location = new Point(x4 + alignedOffsetPosition9, y2);
        this.DrawStringWithDropShadow(graphics, this._Design.WarpSpeed.ToString(), this.Font, location);
        int alignedOffsetPosition10 = this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Energy"), this.Font, width1);
        location = new Point(x1 + alignedOffsetPosition10 - 10, y3);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Energy"), this.Font, location);
        int alignedOffsetPosition11 = this.CalculateCenterAlignedOffsetPosition(graphics, this._Design.ImpulseSpeedFuelBurn.ToString(), this.Font, width2);
        location = new Point(num1 + alignedOffsetPosition11, y3);
        this.DrawStringWithDropShadow(graphics, this._Design.ImpulseSpeedFuelBurn.ToString(), this.Font, location);
        int alignedOffsetPosition12 = this.CalculateCenterAlignedOffsetPosition(graphics, this._Design.CruiseSpeedFuelBurn.ToString(), this.Font, width3);
        location = new Point(x2 + alignedOffsetPosition12, y3);
        this.DrawStringWithDropShadow(graphics, this._Design.CruiseSpeedFuelBurn.ToString(), this.Font, location);
        int alignedOffsetPosition13 = this.CalculateCenterAlignedOffsetPosition(graphics, this._Design.TopSpeedFuelBurn.ToString(), this.Font, width4);
        location = new Point(x3 + alignedOffsetPosition13, y3);
        this.DrawStringWithDropShadow(graphics, this._Design.TopSpeedFuelBurn.ToString(), this.Font, location);
        int alignedOffsetPosition14 = this.CalculateCenterAlignedOffsetPosition(graphics, this._Design.WarpSpeedFuelBurn.ToString(), this.Font, width5);
        location = new Point(x4 + alignedOffsetPosition14, y3);
        this.DrawStringWithDropShadow(graphics, this._Design.WarpSpeedFuelBurn.ToString(), this.Font, location);
        int alignedOffsetPosition15 = this.CalculateCenterAlignedOffsetPosition(graphics, TextResolver.GetText("Energy"), this.Font, width1);
        location = new Point(x1 + alignedOffsetPosition15, num2 + 15);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Energy"), this.Font, location);
        int alignedOffsetPosition16 = this.CalculateCenterAlignedOffsetPosition(graphics, TextResolver.GetText("Curve"), this.Font, width1);
        location = new Point(x1 + alignedOffsetPosition16, num2 + 30);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Curve"), this.Font, location);
        int num4 = Math.Max(this._Design.TopSpeedFuelBurn, this._Design.WarpSpeedFuelBurn);
        int energyConsumption = this._Design.StaticEnergyConsumption;
        double num5 = 60.0 / (double) (energyConsumption + num4);
        Pen pen1 = new Pen((Brush) this._WhiteBrush, 2f);
        pen1.DashStyle = DashStyle.Dash;
        Pen pen2 = new Pen((Brush) this._WhiteBrush, 3f);
        int num6 = num3 - (int) ((double) energyConsumption * num5);
        graphics.DrawLine(pen1, num1, num6, x4 + width5, num6);
        Font font = new Font(this.Font.FontFamily, this.Font.Size - 2f, GraphicsUnit.Pixel);
        int alignedOffsetPosition17 = this.CalculateCenterAlignedOffsetPosition(graphics, TextResolver.GetText("Static Energy Usage"), font, 220);
        location = new Point(num1 + alignedOffsetPosition17, num6 + 1);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Static Energy Usage"), font, location);
        graphics.DrawLine(pen2, num1, num3 - (int) ((double) energyConsumption * num5), num1 + width2 / 2, num3 - (int) ((double) (energyConsumption + this._Design.ImpulseSpeedFuelBurn) * num5));
        graphics.DrawLine(pen2, num1 + width2 / 2, num3 - (int) ((double) (energyConsumption + this._Design.ImpulseSpeedFuelBurn) * num5), x2 + width3 / 2, num3 - (int) ((double) (energyConsumption + this._Design.CruiseSpeedFuelBurn) * num5));
        graphics.DrawLine(pen2, x2 + width3 / 2, num3 - (int) ((double) (energyConsumption + this._Design.CruiseSpeedFuelBurn) * num5), x3 + width4 / 2, num3 - (int) ((double) (energyConsumption + this._Design.TopSpeedFuelBurn) * num5));
        graphics.DrawLine(pen2, x3 + width4 / 2, num3 - (int) ((double) (energyConsumption + this._Design.TopSpeedFuelBurn) * num5), x4 + width5 / 2, num3 - (int) ((double) (energyConsumption + this._Design.WarpSpeedFuelBurn) * num5));
        graphics.DrawLine(pen2, x4 + width5 / 2, num3 - (int) ((double) (energyConsumption + this._Design.WarpSpeedFuelBurn) * num5), x4 + width5, num3 - (int) ((double) (energyConsumption + this._Design.WarpSpeedFuelBurn) * num5));
        location = new Point(x1, y4);
        string str1 = this._Design.AccelerationRate.ToString("#0.#") + "/" + TextResolver.GetText("seconds abbreviation");
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Acceleration") + ": " + str1, this.Font, location);
        location = new Point(x1 + 150, y4);
        string str2 = (this._Design.TurnRate * (180.0 / Math.PI)).ToString("##0") + "°/" + TextResolver.GetText("seconds abbreviation");
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Turn Rate") + ": " + str2, this.Font, location);
        location = new Point(x1, y5);
        string empty = string.Empty;
        string text;
        if (this._Design.WarpSpeed > 0)
        {
          double num7 = this._Design.MaximumRange() / (double) Galaxy.SectorSize;
          text = string.Format(TextResolver.GetText("Design Fuel Range Sector Description"), (object) num7.ToString("0.00"));
        }
        else
        {
          double num8 = this._Design.MaximumRange() / 48000.0;
          text = string.Format(TextResolver.GetText("Design Fuel Range System Description"), (object) num8.ToString("0%"));
        }
        this.DrawStringWithDropShadow(graphics, text, this.Font, location);
      }
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
