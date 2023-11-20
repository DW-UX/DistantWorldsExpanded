// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GameVictoryConditions
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class GameVictoryConditions : GradientPanel
  {
    private Game _Game;
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _TitleFont;

    public GameVictoryConditions()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void ClearData() => this._Game = (Game) null;

    public void Ignite(Game game)
    {
      this._Game = game;
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawVictoryConditions(e.Graphics);
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

    private void DrawVictoryConditions(Graphics graphics)
    {
      this._LeftMargin = 10;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int num1 = 40;
      int num2 = 80;
      int num3 = 280;
      int topMargin = this._TopMargin;
      Point location = new Point(this._LeftMargin, topMargin);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Victory Conditions"), this._TitleFont, location);
      this._LeftMargin += 20;
      int y1 = topMargin + this._RowHeight + this._RowHeight + this._RowHeight;
      location = new Point(this._LeftMargin, y1);
      bool flag = false;
      if (this._Game == null)
        return;
      if (this._Game.IsFinished)
      {
        location = new Point(this._LeftMargin, y1);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("GAME OVER"), this._BoldFont, location, Color.Yellow);
        if (this._Game.Victor != null)
        {
          y1 += this._RowHeight;
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Winner") + ": " + this._Game.Victor.Name, this._BoldFont, location, Color.Yellow);
        }
        y1 = y1 + this._RowHeight + this._RowHeight;
      }
      if (this._Game.GlobalVictoryConditions != null)
      {
        if (this._Game.GlobalVictoryConditions.Economy || this._Game.GlobalVictoryConditions.Population || this._Game.GlobalVictoryConditions.Territory || this._Game.GlobalVictoryConditions.TimeLimit || this._Game.GlobalVictoryConditions.TargetHabitat != null || this._Game.GlobalVictoryConditions.DefendHabitat != null)
        {
          flag = true;
          location = new Point(this._LeftMargin, y1);
          this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Global Conditions"), this._BoldFont, location);
          y1 = y1 + this._RowHeight + this._RowHeight;
        }
        if (this._Game.GlobalVictoryConditions.TimeLimit)
        {
          string text = string.Format(TextResolver.GetText("To win - Time Limit"), (object) Galaxy.ResolveStarDateDescription(this._Game.GlobalVictoryConditions.TimeLimitDate));
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
          int y2 = y1 + this._RowHeight;
          location = new Point(this._LeftMargin + num2, y2);
          this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("To win - Time Limit explanation") + ")", this.Font, location);
          y1 = y2 + this._RowHeight + this._RowHeight;
        }
        if (this._Game.GlobalVictoryConditions.StartDate > 0L)
        {
          string text = string.Format(TextResolver.GetText("To win - Start Date"), (object) Galaxy.ResolveStarDateDescription(this._Game.GlobalVictoryConditions.StartDate));
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
          y1 = y1 + this._RowHeight + this._RowHeight;
        }
        if (this._Game.GlobalVictoryConditions.Economy)
        {
          string text = string.Format(TextResolver.GetText("Victory Conditions Economy"), (object) this._Game.GlobalVictoryConditions.EconomyPercent.ToString("#0"));
          Empire otherEmpire = (Empire) null;
          double num4 = 0.0;
          double num5 = 0.0;
          foreach (Empire empire in (SyncList<Empire>) this._Game.Galaxy.Empires)
          {
            num5 += empire.PrivateAnnualRevenue;
            if (empire.DominantRace != null && empire.DominantRace.Playable && empire.PrivateAnnualRevenue > num4)
            {
              otherEmpire = empire;
              num4 = empire.PrivateAnnualRevenue;
            }
          }
          string str = "(" + TextResolver.GetText("None") + ")";
          if (otherEmpire != null)
          {
            double num6 = num4 / num5;
            str = otherEmpire.Name + " (" + num6.ToString("0%") + ")";
            if (otherEmpire != this._Game.PlayerEmpire)
            {
              DiplomaticRelation diplomaticRelation = this._Game.PlayerEmpire.DiplomaticRelations[otherEmpire];
              if (diplomaticRelation == null || diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                str = "(" + TextResolver.GetText("Unknown empire") + ")";
            }
          }
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, text, this.Font, location);
          int y3 = y1 + this._RowHeight;
          location = new Point(this._LeftMargin + num2, y3);
          this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Closest Empire") + ": " + str, this._BoldFont, location);
          y1 = y3 + this._RowHeight + this._RowHeight;
        }
        if (this._Game.GlobalVictoryConditions.Population)
        {
          string text = string.Format(TextResolver.GetText("Victory Conditions Population"), (object) this._Game.GlobalVictoryConditions.PopulationPercent.ToString("#0"));
          Empire otherEmpire = (Empire) null;
          long num7 = 0;
          long num8 = 0;
          foreach (Empire empire in (SyncList<Empire>) this._Game.Galaxy.Empires)
          {
            num8 += empire.TotalPopulation;
            if (empire.DominantRace != null && empire.DominantRace.Playable && empire.TotalPopulation > num7)
            {
              otherEmpire = empire;
              num7 = empire.TotalPopulation;
            }
          }
          string str = "(" + TextResolver.GetText("None") + ")";
          if (otherEmpire != null)
          {
            double num9 = (double) num7 / (double) num8;
            str = otherEmpire.Name + " (" + num9.ToString("0%") + ")";
            if (otherEmpire != this._Game.PlayerEmpire)
            {
              DiplomaticRelation diplomaticRelation = this._Game.PlayerEmpire.DiplomaticRelations[otherEmpire];
              if (diplomaticRelation == null || diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                str = "(" + TextResolver.GetText("Unknown empire") + ")";
            }
          }
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, text, this.Font, location);
          int y4 = y1 + this._RowHeight;
          location = new Point(this._LeftMargin + num2, y4);
          this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Closest Empire") + ": " + str, this._BoldFont, location);
          y1 = y4 + this._RowHeight + this._RowHeight;
        }
        if (this._Game.GlobalVictoryConditions.Territory)
        {
          string text = string.Format(TextResolver.GetText("Victory Conditions Territory"), (object) this._Game.GlobalVictoryConditions.TerritoryPercent.ToString("#0"));
          Empire otherEmpire = (Empire) null;
          int num10 = 0;
          int num11 = 0;
          foreach (Empire empire in (SyncList<Empire>) this._Game.Galaxy.Empires)
          {
            num11 += empire.Colonies.Count;
            if (empire.DominantRace != null && empire.DominantRace.Playable && empire.Colonies.Count > num10)
            {
              otherEmpire = empire;
              num10 = empire.Colonies.Count;
            }
          }
          string str = "(" + TextResolver.GetText("None") + ")";
          if (otherEmpire != null)
          {
            double num12 = (double) num10 / (double) num11;
            str = otherEmpire.Name + " (" + num12.ToString("0%") + ")";
            if (otherEmpire != this._Game.PlayerEmpire)
            {
              DiplomaticRelation diplomaticRelation = this._Game.PlayerEmpire.DiplomaticRelations[otherEmpire];
              if (diplomaticRelation == null || diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                str = "(" + TextResolver.GetText("Unknown empire") + ")";
            }
          }
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, text, this.Font, location);
          int y5 = y1 + this._RowHeight;
          location = new Point(this._LeftMargin + num2, y5);
          this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Closest Empire") + ": " + str, this._BoldFont, location);
          y1 = y5 + this._RowHeight + this._RowHeight;
        }
        y1 = y1 + this._RowHeight + this._RowHeight;
        if (this._Game.GlobalVictoryConditions.DefendHabitat != null && this._Game.GlobalVictoryConditions.DefendHabitatEmpire != null)
        {
          string text = string.Format(TextResolver.GetText("Victory Conditions Defend Colony"), (object) Galaxy.ResolveDescription(this._Game.GlobalVictoryConditions.DefendHabitat.Category).ToLower(CultureInfo.InvariantCulture), (object) this._Game.GlobalVictoryConditions.DefendHabitat.Name, (object) this._Game.GlobalVictoryConditions.DefendHabitatEmpire.Name);
          location = new Point(this._LeftMargin, y1);
          this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
          y1 = y1 + this._RowHeight + this._RowHeight;
        }
        if (this._Game.GlobalVictoryConditions.TargetHabitat != null && this._Game.GlobalVictoryConditions.TargetHabitatEmpire != null)
        {
          string text = string.Format(TextResolver.GetText("Victory Conditions Conquer Colony"), (object) Galaxy.ResolveDescription(this._Game.GlobalVictoryConditions.TargetHabitat.Category).ToLower(CultureInfo.InvariantCulture), (object) this._Game.GlobalVictoryConditions.TargetHabitat.Name, (object) this._Game.GlobalVictoryConditions.TargetHabitatEmpire.Name);
          location = new Point(this._LeftMargin, y1);
          this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
          y1 = y1 + this._RowHeight + this._RowHeight;
        }
      }
      if (this._Game.PlayerVictoryConditionsToAchieve != null)
      {
        location = new Point(this._LeftMargin, y1);
        this.DrawStringWithDropShadow(graphics, "Your Conditions to Achieve", this._BoldFont, location);
        y1 = y1 + this._RowHeight + this._RowHeight;
        if (this._Game.PlayerVictoryConditionsToAchieve.CaptureColonies != null && this._Game.PlayerVictoryConditionsToAchieve.CaptureColonies.Count > 0)
        {
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, "Capture the following colonies: ", this.Font, location);
          foreach (Habitat captureColony in (SyncList<Habitat>) this._Game.PlayerVictoryConditionsToAchieve.CaptureColonies)
          {
            location = new Point(this._LeftMargin + num3, y1);
            string text = captureColony.Name;
            if (captureColony.Empire.Capital == captureColony)
              text = captureColony.Empire.Name + " capital";
            this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
            y1 += this._RowHeight;
          }
          y1 += this._RowHeight;
        }
        if (this._Game.PlayerVictoryConditionsToAchieve.DestroyBuiltObjects != null && this._Game.PlayerVictoryConditionsToAchieve.DestroyBuiltObjects.Count > 0)
        {
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, "Destroy the following ships or bases: ", this.Font, location);
          foreach (BuiltObject destroyBuiltObject in (SyncList<BuiltObject>) this._Game.PlayerVictoryConditionsToAchieve.DestroyBuiltObjects)
          {
            location = new Point(this._LeftMargin + num3, y1);
            this.DrawStringWithDropShadow(graphics, destroyBuiltObject.Name, this._BoldFont, location);
            y1 += this._RowHeight;
          }
          y1 += this._RowHeight;
        }
        if (this._Game.PlayerVictoryConditionsToAchieve.EliminateEmpires != null && this._Game.PlayerVictoryConditionsToAchieve.EliminateEmpires.Count > 0)
        {
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, "Eliminate the following empires: ", this.Font, location);
          foreach (Empire eliminateEmpire in (SyncList<Empire>) this._Game.PlayerVictoryConditionsToAchieve.EliminateEmpires)
          {
            location = new Point(this._LeftMargin + num3, y1);
            this.DrawStringWithDropShadow(graphics, eliminateEmpire.Name, this._BoldFont, location);
            y1 += this._RowHeight;
          }
          y1 += this._RowHeight;
        }
      }
      if (this._Game.PlayerVictoryConditionsToPrevent != null)
      {
        location = new Point(this._LeftMargin, y1);
        this.DrawStringWithDropShadow(graphics, "Your Conditions to Prevent", this._BoldFont, location);
        y1 = y1 + this._RowHeight + this._RowHeight;
        if (this._Game.PlayerVictoryConditionsToPrevent.CaptureColonies != null && this._Game.PlayerVictoryConditionsToPrevent.CaptureColonies.Count > 0)
        {
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, "Loss of the following colonies: ", this.Font, location);
          foreach (Habitat captureColony in (SyncList<Habitat>) this._Game.PlayerVictoryConditionsToPrevent.CaptureColonies)
          {
            location = new Point(this._LeftMargin + num3, y1);
            this.DrawStringWithDropShadow(graphics, captureColony.Name, this._BoldFont, location);
            y1 += this._RowHeight;
          }
          y1 += this._RowHeight;
        }
        if (this._Game.PlayerVictoryConditionsToPrevent.DestroyBuiltObjects != null && this._Game.PlayerVictoryConditionsToPrevent.DestroyBuiltObjects.Count > 0)
        {
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, "Destruction of the following ships or bases: ", this.Font, location);
          foreach (BuiltObject destroyBuiltObject in (SyncList<BuiltObject>) this._Game.PlayerVictoryConditionsToPrevent.DestroyBuiltObjects)
          {
            location = new Point(this._LeftMargin + num3, y1);
            this.DrawStringWithDropShadow(graphics, destroyBuiltObject.Name, this._BoldFont, location);
            y1 += this._RowHeight;
          }
          y1 += this._RowHeight;
        }
        if (this._Game.PlayerVictoryConditionsToPrevent.EliminateEmpires != null && this._Game.PlayerVictoryConditionsToPrevent.EliminateEmpires.Count > 0)
        {
          location = new Point(this._LeftMargin + num1, y1);
          this.DrawStringWithDropShadow(graphics, "Elimination of the following empires: ", this.Font, location);
          foreach (Empire eliminateEmpire in (SyncList<Empire>) this._Game.PlayerVictoryConditionsToPrevent.EliminateEmpires)
          {
            location = new Point(this._LeftMargin + num3, y1);
            this.DrawStringWithDropShadow(graphics, eliminateEmpire.Name, this._BoldFont, location);
            y1 += this._RowHeight;
          }
          y1 += this._RowHeight;
        }
      }
      if (this._Game.Galaxy.GameRaceSpecificVictoryConditionsEnabled)
      {
        location = new Point(this._LeftMargin, y1);
        string text = TextResolver.GetText("Race-specific Victory Conditions are Active");
        this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
        y1 = y1 + this._RowHeight + this._RowHeight;
        if (this._Game.Galaxy.PlayerEmpire != null && this._Game.Galaxy.PlayerEmpire.DominantRace != null)
        {
          Race dominantRace = this._Game.Galaxy.PlayerEmpire.DominantRace;
        }
      }
      if (flag || this._Game.PlayerVictoryConditionsToAchieve != null || this._Game.PlayerVictoryConditionsToPrevent != null)
        return;
      location = new Point(this._LeftMargin, y1);
      string text1 = TextResolver.GetText("SANDBOX MODE");
      this.DrawStringWithDropShadow(graphics, text1, this._BoldFont, location);
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
      this.DrawStringWithDropShadow(graphics, text, font, location, this._WhiteBrush.Color);
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Color foreColor)
    {
      location = new Point(location.X + 1, location.Y + 1);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location, StringFormat.GenericTypographic);
      location = new Point(location.X - 1, location.Y - 1);
      using (SolidBrush solidBrush = new SolidBrush(foreColor))
        graphics.DrawString(text, font, (Brush) solidBrush, (PointF) location, StringFormat.GenericTypographic);
    }
  }
}
