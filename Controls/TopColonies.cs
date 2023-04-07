// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.TopColonies
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class TopColonies : GradientPanel
  {
    private IContainer components;
    private Game _Game;
    private Bitmap[] _HabitatImages;
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

    public TopColonies()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(15.33f);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void ClearData() => this._Game = (Game) null;

    public void Ignite(Game game, Bitmap[] habitatImages)
    {
      this._Game = game;
      this._HabitatImages = habitatImages;
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawColonies(e.Graphics);
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

    private void DrawColonies(Graphics graphics)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int num1 = this._TopMargin + 25;
      int width1 = 840;
      int num2 = (this.ClientRectangle.Height - (this._TopMargin + num1)) / 60;
      int topMargin = this._TopMargin;
      string text1 = TextResolver.GetText("Top Colonies");
      Point location1 = new Point(this._LeftMargin, topMargin);
      this.DrawStringWithDropShadow(graphics, text1, this._TitleFont, location1);
      int y = num1 + 7;
      if (this._Game == null)
        return;
      DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
      HabitatList habitatList = new HabitatList();
      empireList.Add(this._Game.PlayerEmpire);
      habitatList.AddRange((IEnumerable<Habitat>) this._Game.PlayerEmpire.Colonies);
      if (this._Game.PlayerEmpire.DiplomaticRelations != null)
      {
        foreach (DiplomaticRelation diplomaticRelation in (SyncList<DiplomaticRelation>) this._Game.PlayerEmpire.DiplomaticRelations)
        {
          if (diplomaticRelation.Type != DiplomaticRelationType.NotMet)
          {
            empireList.Add(diplomaticRelation.OtherEmpire);
            foreach (Habitat colony in (SyncList<Habitat>) diplomaticRelation.OtherEmpire.Colonies)
            {
              if (colony.Owner == diplomaticRelation.OtherEmpire && this._Game.PlayerEmpire.CheckSystemExplored(colony.SystemIndex) && !habitatList.Contains(colony))
                habitatList.Add(colony);
            }
          }
        }
      }
      if (this._Game.PlayerEmpire.PirateRelations != null)
      {
        foreach (PirateRelation pirateRelation in (List<PirateRelation>) this._Game.PlayerEmpire.PirateRelations)
        {
          if (pirateRelation.Type != PirateRelationType.NotMet)
          {
            empireList.Add(pirateRelation.OtherEmpire);
            foreach (Habitat colony in (SyncList<Habitat>) pirateRelation.OtherEmpire.Colonies)
            {
              if (colony.Owner == pirateRelation.OtherEmpire && this._Game.PlayerEmpire.CheckSystemExplored(colony.SystemIndex) && !habitatList.Contains(colony))
                habitatList.Add(colony);
            }
          }
        }
      }
      habitatList.Sort();
      habitatList.Reverse();
      int num3 = 1;
      foreach (Habitat habitat in (SyncList<Habitat>) habitatList)
      {
        SolidBrush solidBrush = num3 % 2 != 0 ? new SolidBrush(Color.FromArgb(0, 0, 80)) : new SolidBrush(Color.FromArgb(0, 0, 48));
        Rectangle rect1 = new Rectangle(this._LeftMargin, y, width1, 60);
        graphics.FillRectangle((Brush) solidBrush, rect1);
        Point location2 = new Point(this._LeftMargin + 20, y + 22);
        this.DrawStringWithDropShadow(graphics, num3.ToString(), this._TitleFont, location2);
        int width2 = 55;
        double num4 = (double) this._HabitatImages[(int) habitat.PictureRef].Height / (double) this._HabitatImages[(int) habitat.PictureRef].Width;
        int height;
        if (num4 > 1.0)
        {
          height = 55;
          width2 = (int) ((double) width2 / num4);
        }
        else
          height = (int) ((double) width2 * num4);
        Rectangle rect2 = new Rectangle(this._LeftMargin + 62, y + 2, width2, height);
        graphics.DrawImage((Image) this._HabitatImages[(int) habitat.PictureRef], rect2);
        location2 = new Point(this._LeftMargin + 136, y + 4);
        string text2 = habitat.Name + ", " + Galaxy.DetermineHabitatSystemStar(habitat).Name + " " + TextResolver.GetText("System").ToLower(CultureInfo.InvariantCulture);
        this.DrawStringWithDropShadow(graphics, text2, this._BoldFont, location2);
        Rectangle rect3 = new Rectangle(this._LeftMargin + 136, y + 20, 30, 18);
        graphics.DrawImage((Image) habitat.Empire.LargeFlagPicture, rect3);
        location2 = new Point(this._LeftMargin + 172, y + 22);
        this.DrawStringWithDropShadow(graphics, habitat.Empire.Name, this.Font, location2);
        string text3 = TextResolver.GetText("Population") + ": " + habitat.Population.TotalAmount.ToString("0,,M") + ",   " + TextResolver.GetText("Development Level") + ": " + habitat.DevelopmentLevel.ToString("#0") + "%" + ",   " + TextResolver.GetText("GDP") + ": " + habitat.AnnualRevenue.ToString("0,K") + " " + TextResolver.GetText("credits");
        location2 = new Point(this._LeftMargin + 136, y + 40);
        this.DrawStringWithDropShadow(graphics, text3, this.Font, location2);
        y += 60;
        ++num3;
        if (num3 > num2)
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
