// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DiplomacyEmpireSummary
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DiplomacyEmpireSummary : GradientPanel
  {
    private IContainer components;
    private Galaxy _Galaxy;
    private Empire _Empire;
    private Empire _PlayerEmpire;
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

    public DiplomacyEmpireSummary()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold);
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._Empire = (Empire) null;
      this._PlayerEmpire = (Empire) null;
    }

    public void Ignite(Galaxy galaxy, Empire empire, Empire playerEmpire)
    {
      this._Galaxy = galaxy;
      this._Empire = empire;
      this._PlayerEmpire = playerEmpire;
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawEmpireSummary(e.Graphics);
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

    private void DrawEmpireSummary(Graphics graphics)
    {
      int x = 190;
      int topMargin1 = this._TopMargin;
      Point point = new Point(this._LeftMargin, topMargin1);
      if (this._Empire == null || this._Empire.PirateEmpireBaseHabitat != null || this._PlayerEmpire.PirateEmpireBaseHabitat != null)
        return;
      string str1 = this._Empire.ResolveFeelingDescription(this._Empire.EmpireEvaluations[this._PlayerEmpire]);
      point = new Point(this._LeftMargin, topMargin1);
      this.DrawStringWithDropShadow(graphics, string.Format(TextResolver.GetText("FEELING with us"), (object) str1), this.Font, point);
      int y1 = topMargin1 + this._RowHeight;
      DiplomaticRelation diplomaticRelation = this._Empire.DiplomaticRelations[this._PlayerEmpire];
      SolidBrush solidBrush = new SolidBrush(this._Galaxy.SelectDiplomaticRelationColor(diplomaticRelation.Type));
      point = new Point(this._LeftMargin, y1);
      graphics.DrawString(Galaxy.ResolveDescription(diplomaticRelation.Type), this.Font, (Brush) solidBrush, (PointF) point);
      int y2 = y1 + this._RowHeight;
      point = new Point(this._LeftMargin, y2);
      this.DrawStringWithDropShadow(graphics, this._Empire.GovernmentAttributes.Name, this.Font, point);
      int y3 = y2 + this._RowHeight;
      string str2 = string.Empty;
      if (this._Empire.WarWeariness <= 0.0)
        str2 = TextResolver.GetText("No None");
      else if (this._Empire.WarWeariness > 0.0 && this._Empire.WarWeariness <= 6.0)
        str2 = TextResolver.GetText("Mild");
      else if (this._Empire.WarWeariness >= 6.0 && this._Empire.WarWeariness <= 12.0)
        str2 = TextResolver.GetText("Tolerable");
      else if (this._Empire.WarWeariness >= 12.0 && this._Empire.WarWeariness <= 18.0)
        str2 = TextResolver.GetText("Significant");
      else if (this._Empire.WarWeariness >= 18.0 && this._Empire.WarWeariness <= 26.0)
        str2 = TextResolver.GetText("Serious");
      else if (this._Empire.WarWeariness >= 26.0 && this._Empire.WarWeariness <= 34.0)
        str2 = TextResolver.GetText("Critical");
      else if (this._Empire.WarWeariness > 34.0)
        str2 = TextResolver.GetText("Rampant");
      point = new Point(this._LeftMargin, y3);
      this.DrawStringWithDropShadow(graphics, string.Format(TextResolver.GetText("LEVEL war weariness"), (object) str2), this.Font, point);
      int y4 = y3 + this._RowHeight;
      string str3 = this._Empire.CivilityDescription();
      point = new Point(this._LeftMargin, y4);
      this.DrawStringWithDropShadow(graphics, string.Format(TextResolver.GetText("DESCRIBE reputation"), (object) str3), this.Font, point);
      int num = y4 + this._RowHeight;
      int topMargin2 = this._TopMargin;
      HabitatList habitatList = new HabitatList();
      foreach (Habitat colony in (SyncList<Habitat>) this._Empire.Colonies)
      {
        Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(colony);
        if (!habitatList.Contains(habitatSystemStar))
          habitatList.Add(habitatSystemStar);
      }
      string text = string.Format(TextResolver.GetText("#colonies#systems"), (object) this._Empire.Colonies.Count.ToString(), (object) habitatList.Count.ToString());
      point = new Point(x, topMargin2);
      this.DrawStringWithDropShadow(graphics, text, this.Font, point);
      int y5 = topMargin2 + this._RowHeight;
      point = new Point(x, y5);
      this.DrawStringWithDropShadow(graphics, this._Empire.TotalPopulation.ToString("0,,M") + " " + TextResolver.GetText("Population").ToLower(CultureInfo.InvariantCulture), this.Font, point);
      int y6 = y5 + this._RowHeight;
      point = new Point(x, y6);
      this.DrawStringWithDropShadow(graphics, this._Empire.TotalColonyStrategicValue.ToString("0,K") + " " + TextResolver.GetText("Strategic Value").ToLower(CultureInfo.InvariantCulture), this.Font, point);
      int y7 = y6 + this._RowHeight;
      point = new Point(x, y7);
      this.DrawStringWithDropShadow(graphics, this._Empire.MilitaryPotency.ToString("#0") + " " + TextResolver.GetText("Military firepower").ToLower(CultureInfo.InvariantCulture), this.Font, point);
      int y8 = y7 + this._RowHeight;
      point = new Point(x, y8);
      this.DrawStringWithDropShadow(graphics, string.Format(TextResolver.GetText("X credits"), (object) this._Empire.StateMoney.ToString("0,K")), this.Font, point);
      num = y8 + this._RowHeight;
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
