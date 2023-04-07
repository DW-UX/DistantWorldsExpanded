// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.RaceVictoryConditionsPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class RaceVictoryConditionsPanel : GradientPanel
  {
    private Font _BoldFont;
    private Font _TitleFont;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private Color _EconomyColor = Color.FromArgb(0, 0, 80);
    private Color _PopulationColor = Color.FromArgb(0, 80, 0);
    private Color _TerritoryColor = Color.FromArgb(80, 80, 0);
    private Color _RaceColor = Color.FromArgb(80, 0, 0);
    private Color _EconomyColor2 = Color.FromArgb(0, 0, (int) byte.MaxValue);
    private Color _PopulationColor2 = Color.FromArgb(0, (int) byte.MaxValue, 0);
    private Color _TerritoryColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
    private Color _RaceColor2 = Color.FromArgb((int) byte.MaxValue, 0, 0);
    private Color _BonusColor = Color.FromArgb(80, 40, 0);
    private Color _BonusColor2 = Color.FromArgb((int) byte.MaxValue, 128, 0);
    private Game _Game;
    private Galaxy _Galaxy;
    private VictoryConditionProgressList _ConditionProgresses;
    private VictoryConditions _GlobalVictoryConditions;
    private double _RowHeight = 22.0;
    private double _RowSpacing = 6.0;
    private RaceImageCache _RaceImageCache;
    protected VictoryConditionProgress _HoveredCondition;
    private int _StartY;

    public RaceVictoryConditionsPanel()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(15.33f);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void InitializeImages(RaceImageCache raceImageCache) => this._RaceImageCache = raceImageCache;

    public void BindData(Game game, Galaxy galaxy, VictoryConditions globalVictoryConditions)
    {
      this.SetFont(15.33f);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
      this._Game = game;
      this._Galaxy = galaxy;
      this._GlobalVictoryConditions = globalVictoryConditions;
      this._ConditionProgresses = Galaxy.GenerateVictoryConditionProgresses(this._Galaxy, this._GlobalVictoryConditions, true);
    }

    public void ClearData()
    {
      this._Game = (Game) null;
      this._Galaxy = (Galaxy) null;
      this._GlobalVictoryConditions = (VictoryConditions) null;
      this._ConditionProgresses = (VictoryConditionProgressList) null;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      try
      {
        this._StartY = this.DrawVictoryConditions(e.Graphics);
        this.DrawConditions(e.Graphics, this._ConditionProgresses, this._GlobalVictoryConditions, this._StartY);
        if (this._HoveredCondition == null)
          return;
        this.ShowVictoryConditionDetail(e.Graphics, this._HoveredCondition, this._ConditionProgresses, this._StartY);
      }
      catch (Exception ex)
      {
      }
    }

    private int DrawVictoryConditions(Graphics graphics)
    {
      int num1 = 14;
      int num2 = 10;
      int x = 10;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int num3 = 40;
      int y1 = num2;
      Point location = new Point(x, y1);
      bool flag = false;
      if (this._Game != null)
      {
        if (this._Game.IsFinished)
        {
          location = new Point(x, y1);
          this.DrawStringWithDropShadow(graphics, TextResolver.GetText("GAME OVER"), this._BoldFont, location, Color.Yellow);
          if (this._Game.Victor != null)
          {
            y1 += num1;
            location = new Point(x + num3, y1);
            this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Winner") + ": " + this._Game.Victor.Name, this._BoldFont, location, Color.Yellow);
          }
          y1 = y1 + num1 + num1;
        }
        if (this._Game.GlobalVictoryConditions != null)
        {
          if (this._Game.GlobalVictoryConditions.Economy || this._Game.GlobalVictoryConditions.Population || this._Game.GlobalVictoryConditions.Territory || this._Game.GlobalVictoryConditions.TimeLimit || this._Game.GlobalVictoryConditions.TargetHabitat != null || this._Game.GlobalVictoryConditions.DefendHabitat != null || this._Game.GlobalVictoryConditions.EnableRaceSpecificVictoryConditions)
            flag = true;
          if (this._Game.GlobalVictoryConditions.TimeLimit)
          {
            string text = string.Format(TextResolver.GetText("To win - Time Limit"), (object) Galaxy.ResolveStarDateDescription(this._Game.GlobalVictoryConditions.TimeLimitDate));
            location = new Point(x, y1);
            this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
            int y2 = y1 + num1;
            location = new Point(x + num3, y2);
            this.DrawStringWithDropShadow(graphics, "(" + TextResolver.GetText("To win - Time Limit explanation") + ")", this.Font, location);
            y1 = y2 + num1 + num1;
          }
          if (this._Game.GlobalVictoryConditions.StartDate > 0L)
          {
            string text = string.Format(TextResolver.GetText("To win - Start Date"), (object) Galaxy.ResolveStarDateDescription(this._Game.GlobalVictoryConditions.StartDate));
            location = new Point(x, y1);
            this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
            y1 = y1 + num1 + num1;
          }
          if (this._Game.GlobalVictoryConditions.DefendHabitat != null && this._Game.GlobalVictoryConditions.DefendHabitatEmpire != null)
          {
            string text = string.Format(TextResolver.GetText("Victory Conditions Defend Colony"), (object) Galaxy.ResolveDescription(this._Game.GlobalVictoryConditions.DefendHabitat.Category).ToLower(CultureInfo.InvariantCulture), (object) this._Game.GlobalVictoryConditions.DefendHabitat.Name, (object) this._Game.GlobalVictoryConditions.DefendHabitatEmpire.Name);
            location = new Point(x, y1);
            this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
            y1 = y1 + num1 + num1;
          }
          if (this._Game.GlobalVictoryConditions.TargetHabitat != null && this._Game.GlobalVictoryConditions.TargetHabitatEmpire != null)
          {
            string text = string.Format(TextResolver.GetText("Victory Conditions Conquer Colony"), (object) Galaxy.ResolveDescription(this._Game.GlobalVictoryConditions.TargetHabitat.Category).ToLower(CultureInfo.InvariantCulture), (object) this._Game.GlobalVictoryConditions.TargetHabitat.Name, (object) this._Game.GlobalVictoryConditions.TargetHabitatEmpire.Name);
            location = new Point(x, y1);
            this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
            y1 = y1 + num1 + num1;
          }
        }
        if (!flag && this._Game.PlayerVictoryConditionsToAchieve == null && this._Game.PlayerVictoryConditionsToPrevent == null)
        {
          location = new Point(x, y1);
          string text = TextResolver.GetText("SANDBOX MODE");
          this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
          y1 = y1 + num1 + num1;
        }
      }
      return y1;
    }

    private void DrawEmpireConditionsDetail(
      Graphics graphics,
      Rectangle rectangle,
      VictoryConditionProgress conditionProgress)
    {
      if (conditionProgress == null)
        return;
      GraphicsHelper.SetGraphicsQualityToHigh(graphics);
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      if (conditionProgress.BonusAmount > 0.0 || conditionProgress.PirateBonusAmount > 0.0)
        rectangle.Height += 60;
      double width1 = (double) rectangle.Width;
      int num1 = 5;
      float num2 = (float) (num1 * 2);
      int num3 = 5;
      int num4 = 7;
      double num5 = width1;
      int portionCount = conditionProgress.GetPortionCount();
      if (portionCount > 0)
        num5 = width1 / (double) (portionCount + 1);
      if (portionCount == 1 && conditionProgress.RaceVictoryConditionsProgress != null && conditionProgress.RaceVictoryConditionsProgress.Count > 0)
        num5 = width1;
      double width2 = num5 - (double) num2;
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(250, 0, 0, 0)))
        graphics.FillRectangle((Brush) solidBrush, rectangle);
      int x1 = rectangle.X + num1;
      int y1 = rectangle.Y + num1;
      int num6 = 30;
      int width3 = (int) ((double) num6 / 0.6);
      if (conditionProgress.Empire != null)
      {
        Bitmap largeFlagPicture = conditionProgress.Empire.LargeFlagPicture;
        if (largeFlagPicture != null)
        {
          Rectangle srcRect = new Rectangle(0, 0, largeFlagPicture.Width, largeFlagPicture.Height);
          Rectangle destRect = new Rectangle(x1, y1, width3, num6);
          graphics.DrawImage((Image) largeFlagPicture, destRect, srcRect, GraphicsUnit.Pixel);
          x1 += width3 + num1;
        }
        Bitmap bitmap = (Bitmap) null;
        if (conditionProgress.Empire.DominantRace != null)
          bitmap = this._RaceImageCache.GetEmpireDominantRaceImageSize30(conditionProgress.Empire);
        if (bitmap != null)
        {
          Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
          Rectangle destRect = new Rectangle(x1, y1, num6, num6);
          graphics.DrawImage((Image) bitmap, destRect, srcRect, GraphicsUnit.Pixel);
          x1 += num6 + num1;
        }
        string text1 = conditionProgress.Empire.Name + "  (" + conditionProgress.TotalProgress.ToString("0%") + ")";
        this.DrawStringWithDropShadow(graphics, text1, this._TitleFont, new Point(x1, y1 + 6));
        string text2 = TextResolver.GetText("Victory Threshold") + ": " + this._GlobalVictoryConditions.VictoryThresholdPercentage.ToString("0%");
        SizeF sizeF = graphics.MeasureString(text2, this._TitleFont);
        this.DrawStringWithDropShadow(graphics, text2, this._TitleFont, new Point(rectangle.Right - (int) sizeF.Width, y1 + 6));
      }
      using (Pen pen = new Pen(Color.FromArgb(170, 170, 170), 1f))
      {
        pen.DashStyle = DashStyle.Dot;
        graphics.DrawLine(pen, new Point(rectangle.Left, y1 + num6 + num1), new Point(rectangle.Right, y1 + num6 + num1));
      }
      if (portionCount > 1)
      {
        using (Pen pen = new Pen(Color.FromArgb(170, 170, 170), 1f))
        {
          pen.DashStyle = DashStyle.Dot;
          for (int index = 1; index < portionCount; ++index)
          {
            int x2 = (int) ((double) index * num5);
            graphics.DrawLine(pen, new Point(x2, rectangle.Top + num1 + num6 + num1), new Point(x2, rectangle.Bottom));
          }
        }
      }
      double num7 = 1.0 / (double) portionCount;
      double territoryProgress = 0.0;
      double economyProgress = 0.0;
      double populationProgress = 0.0;
      double raceProgress = 0.0;
      conditionProgress.GetProgressAll(out territoryProgress, out economyProgress, out populationProgress, out raceProgress);
      SizeF size1 = new SizeF((float) width2, (float) rectangle.Height);
      int num8 = 0;
      if (conditionProgress.EconomyEnabled)
      {
        int x3 = (int) ((double) num8 * num5) + num1;
        int y2 = rectangle.Top + num1 + num6 + num1 + num1;
        string text3 = TextResolver.GetText("Economy") + "  " + (economyProgress * 100.0).ToString("0") + "/" + num7.ToString("0%");
        SizeF size2 = graphics.MeasureString(text3, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text3, this._BoldFont, new Point(x3, y2), size2, this._EconomyColor2);
        int y3 = y2 + (int) size2.Height + num4;
        string text4 = TextResolver.GetText("Target") + ":";
        size2 = graphics.MeasureString(text4, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text4, this._BoldFont, new Point(x3, y3), size2);
        int y4 = y3 + (int) size2.Height;
        string text5 = string.Format(TextResolver.GetText("Victory Conditions Economy"), (object) this._GlobalVictoryConditions.EconomyPercent.ToString("#0"));
        size2 = graphics.MeasureString(text5, this.Font, (int) width2 - 20, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text5, this.Font, new Point(x3 + 20, y4), size2);
        int y5 = y4 + (int) size2.Height + num4;
        string str1 = conditionProgress.Empire.Name;
        if (conditionProgress.Empire == this._Galaxy.PlayerEmpire)
          str1 = TextResolver.GetText("Your Empire");
        string text6 = str1 + ":";
        size2 = graphics.MeasureString(text6, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text6, this._BoldFont, new Point(x3, y5), size2);
        int y6 = y5 + (int) size2.Height;
        string str2 = string.Format(TextResolver.GetText("Victory Conditions Economy"), (object) (conditionProgress.EconomyPercent * 100.0).ToString("#0"));
        double num9 = conditionProgress.Empire.PrivateAnnualRevenue;
        if (conditionProgress.Empire != null && conditionProgress.Empire.PirateEmpireBaseHabitat != null)
          num9 = conditionProgress.Empire.CalculateAccurateAnnualIncome();
        string text7 = str2 + "  (" + string.Format(TextResolver.GetText("Trade Description Money"), (object) num9.ToString("###,###,###,###,##0")) + ")";
        size2 = graphics.MeasureString(text7, this.Font, (int) width2 - 20, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text7, this.Font, new Point(x3 + 20, y6), size2);
        y1 = y6 + (int) size2.Height;
        ++num8;
      }
      if (conditionProgress.PopulationEnabled)
      {
        int x4 = (int) ((double) num8 * num5) + num1;
        int y7 = rectangle.Top + num1 + num6 + num1 + num1;
        string text8 = TextResolver.GetText("Population") + "  " + (populationProgress * 100.0).ToString("0") + "/" + num7.ToString("0%");
        SizeF size3 = graphics.MeasureString(text8, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text8, this._BoldFont, new Point(x4, y7), size3, this._PopulationColor2);
        int y8 = y7 + (int) size3.Height + num4;
        string text9 = TextResolver.GetText("Target") + ":";
        size3 = graphics.MeasureString(text9, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text9, this._BoldFont, new Point(x4, y8), size3);
        int y9 = y8 + (int) size3.Height;
        string text10 = string.Format(TextResolver.GetText("Victory Conditions Population"), (object) this._GlobalVictoryConditions.PopulationPercent.ToString("#0"));
        size3 = graphics.MeasureString(text10, this.Font, (int) width2 - 20, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text10, this.Font, new Point(x4 + 20, y9), size3);
        int y10 = y9 + (int) size3.Height + num4;
        string str3 = conditionProgress.Empire.Name;
        if (conditionProgress.Empire == this._Galaxy.PlayerEmpire)
          str3 = TextResolver.GetText("Your Empire");
        string text11 = str3 + ":";
        size3 = graphics.MeasureString(text11, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text11, this._BoldFont, new Point(x4, y10), size3);
        int y11 = y10 + (int) size3.Height;
        string str4 = string.Format(TextResolver.GetText("Victory Conditions Population"), (object) (conditionProgress.PopulationPercent * 100.0).ToString("#0"));
        long num10 = conditionProgress.Empire.TotalPopulation;
        if (conditionProgress.Empire != null && conditionProgress.Empire.PirateEmpireBaseHabitat != null)
        {
          HabitatList ownedColonies = new HabitatList();
          HabitatList controlledColonies = conditionProgress.Empire.Colonies.GetPirateControlledColonies(conditionProgress.Empire, out ownedColonies);
          num10 = ownedColonies.TotalPopulation() + controlledColonies.TotalPopulation() / 2L;
        }
        string text12 = str4 + "  (" + TextResolver.GetText("Population") + ": " + num10.ToString("0,,M") + ")";
        size3 = graphics.MeasureString(text12, this.Font, (int) width2 - 20, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text12, this.Font, new Point(x4 + 20, y11), size3);
        y1 = y11 + (int) size3.Height;
        ++num8;
      }
      if (conditionProgress.TerritoryEnabled)
      {
        int x5 = (int) ((double) num8 * num5) + num1;
        int y12 = rectangle.Top + num1 + num6 + num1 + num1;
        string text13 = TextResolver.GetText("Territory") + "  " + (territoryProgress * 100.0).ToString("0") + "/" + num7.ToString("0%");
        SizeF size4 = graphics.MeasureString(text13, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text13, this._BoldFont, new Point(x5, y12), size4, this._TerritoryColor2);
        int y13 = y12 + (int) size4.Height + num4;
        string text14 = TextResolver.GetText("Target") + ":";
        size4 = graphics.MeasureString(text14, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text14, this._BoldFont, new Point(x5, y13), size4);
        int y14 = y13 + (int) size4.Height;
        string text15 = string.Format(TextResolver.GetText("Victory Conditions Territory"), (object) this._GlobalVictoryConditions.TerritoryPercent.ToString("#0"));
        size4 = graphics.MeasureString(text15, this.Font, (int) width2 - 20, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text15, this.Font, new Point(x5 + 20, y14), size4);
        int y15 = y14 + (int) size4.Height + num4;
        string str5 = conditionProgress.Empire.Name;
        if (conditionProgress.Empire == this._Galaxy.PlayerEmpire)
          str5 = TextResolver.GetText("Your Empire");
        string text16 = str5 + ":";
        size4 = graphics.MeasureString(text16, this._BoldFont, (int) width2, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text16, this._BoldFont, new Point(x5, y15), size4);
        int y16 = y15 + (int) size4.Height;
        string str6 = string.Format(TextResolver.GetText("Victory Conditions Territory"), (object) (conditionProgress.TerritoryPercent * 100.0).ToString("#0"));
        double count = (double) conditionProgress.Empire.Colonies.Count;
        string text17;
        if (conditionProgress.Empire != null && conditionProgress.Empire.PirateEmpireBaseHabitat != null)
        {
          int ownedColonyCount = 0;
          int num11 = conditionProgress.Empire.Colonies.CountPirateControlledColonies(conditionProgress.Empire, out ownedColonyCount);
          double num12 = (double) ownedColonyCount + (double) num11 / 2.0;
          string str7 = string.Format(TextResolver.GetText("Pirate Territory Colonies Controlled vs Owned"), (object) num11.ToString("#0"), (object) ownedColonyCount.ToString("#0"));
          text17 = str6 + "  (" + num12.ToString("#0.0") + " " + TextResolver.GetText("colonies") + ":  " + str7 + ")";
        }
        else
          text17 = str6 + "  (" + count.ToString("#0") + " " + TextResolver.GetText("colonies") + ")";
        size4 = graphics.MeasureString(text17, this.Font, (int) width2 - 20, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text17, this.Font, new Point(x5 + 20, y16), size4);
        y1 = y16 + (int) size4.Height;
        ++num8;
      }
      if (conditionProgress.RaceVictoryConditionsProgress != null && conditionProgress.RaceVictoryConditionsProgress.Count > 0)
      {
        int x6 = (int) ((double) num8 * num5) + num1;
        int y17 = rectangle.Top + num1 + num6 + num1 + num1;
        double num13 = num5 * 2.0;
        if (portionCount == 1)
          num13 = width1;
        double val1 = num13 - (double) num2;
        int num14 = 0;
        int num15 = 30;
        double width4 = Math.Min(val1, 840.0);
        int width5 = (int) (width4 - 60.0);
        int num16 = num15 + width5;
        string empty1 = string.Empty;
        string text18;
        if (conditionProgress.Empire.PirateEmpireBaseHabitat == null)
          text18 = conditionProgress.Empire.DominantRace.Name + " " + TextResolver.GetText("Race Victory Conditions") + "  " + (raceProgress * 100.0).ToString("0") + "/" + num7.ToString("0%");
        else
          text18 = Galaxy.ResolveDescription(conditionProgress.Empire.PiratePlayStyle) + " " + TextResolver.GetText("Victory Conditions") + "  " + (raceProgress * 100.0).ToString("0") + "/" + num7.ToString("0%");
        SizeF size5 = graphics.MeasureString(text18, this._BoldFont, (int) width4, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text18, this._BoldFont, new Point(x6, y17), size5, this._RaceColor2);
        int y18 = y17 + (int) size5.Height + num4;
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Portion"), this._BoldFont, new Point(x6 + num14, y18));
        SizeF sizeF = graphics.MeasureString(TextResolver.GetText("Progress"), this._BoldFont);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Progress"), this._BoldFont, new Point(rectangle.Right - (int) sizeF.Width, y18));
        y1 = y18 + (int) size5.Height + num3;
        for (int index = 0; index < conditionProgress.RaceVictoryConditionsProgress.Count; ++index)
        {
          RaceVictoryConditionProgress conditionProgress1 = conditionProgress.RaceVictoryConditionsProgress[index];
          if (conditionProgress1 != null)
          {
            string text19 = conditionProgress1.Condition.Proportion.ToString("0") + "%";
            this.DrawStringWithDropShadowBounded(graphics, text19, this.Font, new Point(x6 + num14, y1), size1);
            string text20 = Galaxy.ResolveDescription(conditionProgress1.Condition, conditionProgress.Empire);
            SizeF size6 = graphics.MeasureString(text20, this.Font, width5, StringFormat.GenericTypographic);
            size1 = graphics.MeasureString(text20, this.Font, width5);
            this.DrawStringWithDropShadowBounded(graphics, text20, this.Font, new Point(x6 + num15, y1), size6);
            string text21 = conditionProgress1.ThisProgress.ToString("0%");
            this.DrawStringWithDropShadow(graphics, text21, this.Font, new Point(x6 + num16, y1));
            int y19 = y1 + (int) size6.Height;
            if (conditionProgress1.BestEmpire != null && conditionProgress1.BestEmpire != conditionProgress.Empire || !string.IsNullOrEmpty(conditionProgress1.Detail))
            {
              DiplomaticRelation diplomaticRelation = this._Galaxy.PlayerEmpire.ObtainDiplomaticRelation(conditionProgress1.BestEmpire);
              string empty2 = string.Empty;
              if (diplomaticRelation.Type != DiplomaticRelationType.NotMet && conditionProgress1.BestEmpire != null && conditionProgress1.BestEmpire != conditionProgress.Empire)
              {
                empty2 += conditionProgress1.BestEmpire.Name;
                if (!string.IsNullOrEmpty(conditionProgress1.Detail))
                  empty2 += ": ";
              }
              string text22 = empty2 + conditionProgress1.Detail;
              SizeF size7 = graphics.MeasureString(text22, this.Font, width5, StringFormat.GenericTypographic);
              this.DrawStringWithDropShadowBounded(graphics, text22, this.Font, new Point(x6 + num15 + 20, y19), size7);
              y19 += (int) size7.Height;
            }
            y1 = y19 + num3;
          }
        }
      }
      if (conditionProgress.PirateBonusAmount > 0.0)
      {
        if (conditionProgress.Empire == null || conditionProgress.Empire.PirateEmpireBaseHabitat == null || conditionProgress.Empire.Colonies == null)
          return;
        int num17 = rectangle.Top + num1 + num6 + num1 + num1;
        int x7 = rectangle.Left + num1;
        int y20 = y1 + 10;
        if (y20 < num17)
          y20 = num17;
        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(250, 0, 0, 0)))
          graphics.FillRectangle((Brush) solidBrush, new Rectangle(rectangle.X, y20, rectangle.Width, rectangle.Bottom - y20));
        if (conditionProgress.BonusAmount > 0.0)
        {
          double num18 = conditionProgress.PirateBonusAmount + conditionProgress.BonusAmount;
          size1 = new SizeF((float) (rectangle.Width - num1 * 2), (float) rectangle.Height);
          long num19 = conditionProgress.Empire.Colonies.TotalPopulationOwnedColonies(conditionProgress.Empire);
          string empty3 = string.Empty;
          string text23 = conditionProgress.StandingWonderBonusAmount <= 0.0 ? string.Format(TextResolver.GetText("Pirate Owned Colony Population Victory Condition Bonus Plus Others Description"), (object) num18.ToString("+0.0%")) : string.Format(TextResolver.GetText("Pirate Owned Colony Population Victory Condition Bonus Plus Others Standing Wonders Description"), (object) num18.ToString("+0.0%"));
          SizeF sizeF = graphics.MeasureString(text23, this._BoldFont, rectangle.Width, StringFormat.GenericTypographic);
          this.DrawStringWithDropShadowBounded(graphics, text23, this._BoldFont, new Point(x7, y20), size1);
          int y21 = y20 + (int) sizeF.Height;
          int x8 = rectangle.Left + num1 + 50;
          string empty4 = string.Empty;
          string text24 = conditionProgress.StandingWonderBonusAmount <= 0.0 ? string.Format(TextResolver.GetText("Pirate Owned Colony Population Victory Condition Bonus Plus Others Detail"), (object) num19.ToString("0,,M"), (object) conditionProgress.Empire.Name) : string.Format(TextResolver.GetText("Pirate Owned Colony Population Victory Condition Bonus Plus Others Standing Wonders Detail"), (object) num19.ToString("0,,M"), (object) conditionProgress.Empire.Name);
          this.DrawStringWithDropShadowBounded(graphics, text24, this.Font, new Point(x8, y21), size1);
        }
        else
        {
          double pirateBonusAmount = conditionProgress.PirateBonusAmount;
          size1 = new SizeF((float) (rectangle.Width - num1 * 2), (float) rectangle.Height);
          long num20 = conditionProgress.Empire.Colonies.TotalPopulationOwnedColonies(conditionProgress.Empire);
          string text25 = string.Format(TextResolver.GetText("Pirate Owned Colony Population Victory Condition Bonus Description"), (object) pirateBonusAmount.ToString("+0.0%"));
          SizeF sizeF = graphics.MeasureString(text25, this._BoldFont, rectangle.Width, StringFormat.GenericTypographic);
          this.DrawStringWithDropShadowBounded(graphics, text25, this._BoldFont, new Point(x7, y20), size1);
          int y22 = y20 + (int) sizeF.Height;
          int x9 = rectangle.Left + num1 + 50;
          string text26 = string.Format(TextResolver.GetText("Pirate Owned Colony Population Victory Condition Bonus Detail"), (object) num20.ToString("0,,M"), (object) conditionProgress.Empire.Name);
          this.DrawStringWithDropShadowBounded(graphics, text26, this.Font, new Point(x9, y22), size1);
        }
      }
      else
      {
        if (conditionProgress.BonusAmount <= 0.0 || conditionProgress.Empire == null)
          return;
        int num21 = rectangle.Top + num1 + num6 + num1 + num1;
        int x10 = rectangle.Left + num1;
        int y23 = y1 + 10;
        if (y23 < num21)
          y23 = num21;
        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(250, 0, 0, 0)))
          graphics.FillRectangle((Brush) solidBrush, new Rectangle(rectangle.X, y23, rectangle.Width, rectangle.Bottom - y23));
        size1 = new SizeF((float) (rectangle.Width - num1 * 2), (float) rectangle.Height);
        string empty5 = string.Empty;
        string text27 = conditionProgress.StandingWonderBonusAmount <= 0.0 ? string.Format(TextResolver.GetText("Victory Condition Bonus Description"), (object) conditionProgress.BonusAmount.ToString("+0.0%;-0.0%")) : string.Format(TextResolver.GetText("Victory Condition Bonus Description Standing Wonders"), (object) conditionProgress.BonusAmount.ToString("+0.0%;-0.0%"));
        SizeF sizeF = graphics.MeasureString(text27, this._BoldFont, rectangle.Width, StringFormat.GenericTypographic);
        this.DrawStringWithDropShadowBounded(graphics, text27, this._BoldFont, new Point(x10, y23), size1);
        int y24 = y23 + (int) sizeF.Height;
        int x11 = rectangle.Left + num1 + 50;
        string empty6 = string.Empty;
        string text28 = conditionProgress.StandingWonderBonusAmount <= 0.0 ? string.Format(TextResolver.GetText("Victory Condition Bonus Detail"), (object) conditionProgress.BonusAmount.ToString("+0.0%;-0.0%")) : string.Format(TextResolver.GetText("Victory Condition Bonus Detail Standing Wonders"), (object) conditionProgress.BonusAmount.ToString("+0.0%;-0.0%"));
        this.DrawStringWithDropShadowBounded(graphics, text28, this.Font, new Point(x11, y24), size1);
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this._HoveredCondition = this.GetHoveredCondition(this._ConditionProgresses, this._StartY);
      this.Invalidate();
    }

    public void ShowVictoryConditionDetail(
      Graphics graphics,
      VictoryConditionProgress condition,
      VictoryConditionProgressList conditionProgresses,
      int startY)
    {
      int num1 = conditionProgresses.IndexOf(condition);
      if (num1 < 0)
        return;
      int num2 = 5 + num1 * (int) (this._RowHeight + this._RowSpacing);
      int height = 300;
      int x = 0;
      Rectangle rectangle = Rectangle.Empty;
      rectangle = num2 + (int) this._RowHeight + height + startY <= this.Height ? new Rectangle(x, num2 + (int) this._RowHeight + startY, this.Width - x, height) : new Rectangle(x, num2 - height + startY, this.Width - x, height);
      this.DrawEmpireConditionsDetail(graphics, rectangle, condition);
    }

    public VictoryConditionProgress GetHoveredCondition(
      VictoryConditionProgressList conditionProgresses,
      int startY)
    {
      Point client = this.PointToClient(MouseHelper.GetCursorPosition());
      int x = client.X;
      int num1 = client.Y - 5 - startY;
      int index = -1;
      double num2 = this._RowHeight / (this._RowHeight + this._RowSpacing);
      double num3 = (double) num1 / (this._RowHeight + this._RowSpacing);
      if (num3 - (double) (int) num3 < num2)
        index = num1 / (int) (this._RowHeight + this._RowSpacing);
      return conditionProgresses != null && index >= 0 && index < conditionProgresses.Count ? conditionProgresses[index] : (VictoryConditionProgress) null;
    }

    private void DrawConditions(
      Graphics graphics,
      VictoryConditionProgressList conditionProgresses,
      VictoryConditions globalVictoryConditions,
      int startY)
    {
      GraphicsHelper.SetGraphicsQualityToHigh(graphics);
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      if (conditionProgresses == null || globalVictoryConditions == null)
        return;
      int width1 = (int) (this._RowHeight / 0.6);
      int num1 = width1 + 3 + (int) this._RowHeight + 3;
      double width2 = (double) this.Width - (double) num1;
      double rowHeight = this._RowHeight;
      double rowSpacing = this._RowSpacing;
      double num2 = 5.0 + (double) startY;
      int num3 = num1 + (int) (width2 * globalVictoryConditions.VictoryThresholdPercentage);
      conditionProgresses.Sort();
      conditionProgresses.Reverse();
      for (int index = 0; index < conditionProgresses.Count; ++index)
      {
        VictoryConditionProgress conditionProgress = conditionProgresses[index];
        double x1 = 0.0;
        double width3 = 0.0;
        double width4 = 0.0;
        double width5 = 0.0;
        double width6 = 0.0;
        double width7 = 0.0;
        if (conditionProgress != null)
        {
          double territoryProgress = 0.0;
          double economyProgress = 0.0;
          double populationProgress = 0.0;
          double raceProgress = 0.0;
          conditionProgress.GetProgressAll(out territoryProgress, out economyProgress, out populationProgress, out raceProgress);
          if (globalVictoryConditions.Economy && economyProgress > 0.0)
            width3 = economyProgress * width2;
          if (globalVictoryConditions.Population && populationProgress > 0.0)
            width4 = populationProgress * width2;
          if (globalVictoryConditions.Territory && territoryProgress > 0.0)
            width5 = territoryProgress * width2;
          if (globalVictoryConditions.EnableRaceSpecificVictoryConditions && raceProgress > 0.0)
            width6 = raceProgress * width2;
          if (conditionProgress.BonusAmount > 0.0 || conditionProgress.PirateBonusAmount > 0.0)
            width7 = (conditionProgress.BonusAmount + conditionProgress.PirateBonusAmount) * width2;
          if (conditionProgress.Empire != null)
          {
            Bitmap largeFlagPicture = conditionProgress.Empire.LargeFlagPicture;
            if (largeFlagPicture != null)
            {
              Rectangle srcRect = new Rectangle(0, 0, largeFlagPicture.Width, largeFlagPicture.Height);
              Rectangle destRect = new Rectangle((int) x1, (int) num2, width1, (int) this._RowHeight);
              graphics.DrawImage((Image) largeFlagPicture, destRect, srcRect, GraphicsUnit.Pixel);
            }
            Bitmap bitmap = (Bitmap) null;
            if (conditionProgress.Empire.DominantRace != null)
              bitmap = this._RaceImageCache.GetEmpireDominantRaceImageSize30(conditionProgress.Empire);
            if (bitmap != null)
            {
              Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
              Rectangle destRect = new Rectangle((int) x1 + width1 + 3, (int) num2, (int) this._RowHeight, (int) this._RowHeight);
              graphics.DrawImage((Image) bitmap, destRect, srcRect, GraphicsUnit.Pixel);
            }
          }
          double x2 = x1 + ((double) (width1 + 3) + this._RowHeight + 3.0);
          Rectangle rect1 = new Rectangle((int) x2, (int) num2, (int) width2, (int) rowHeight);
          using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(64, 0, 0, 0)))
            graphics.FillRectangle((Brush) solidBrush, rect1);
          using (Pen pen = new Pen(Color.Red, 1f))
            graphics.DrawLine(pen, num3, (int) num2, num3, (int) num2 + (int) this._RowHeight);
          if (width3 >= 1.0)
          {
            rect1 = new Rectangle((int) x2, (int) num2, (int) width3, (int) rowHeight);
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, this._EconomyColor, this._EconomyColor2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
              graphics.FillRectangle((Brush) linearGradientBrush, rect1);
          }
          double x3 = x2 + width3;
          if (width4 >= 1.0)
          {
            rect1 = new Rectangle((int) x3, (int) num2, (int) width4, (int) rowHeight);
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, this._PopulationColor, this._PopulationColor2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
              graphics.FillRectangle((Brush) linearGradientBrush, rect1);
          }
          double x4 = x3 + width4;
          if (width5 >= 1.0)
          {
            rect1 = new Rectangle((int) x4, (int) num2, (int) width5, (int) rowHeight);
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, this._TerritoryColor, this._TerritoryColor2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
              graphics.FillRectangle((Brush) linearGradientBrush, rect1);
          }
          double x5 = x4 + width5;
          if (width6 >= 1.0)
          {
            rect1 = new Rectangle((int) x5, (int) num2, (int) width6, (int) rowHeight);
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, this._RaceColor, this._RaceColor2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
              graphics.FillRectangle((Brush) linearGradientBrush, rect1);
          }
          double x6 = x5 + width6;
          if (width7 >= 1.0)
          {
            rect1 = new Rectangle((int) x6, (int) num2, (int) width7, (int) rowHeight);
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, this._BonusColor, this._BonusColor2, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
              graphics.FillRectangle((Brush) linearGradientBrush, rect1);
          }
          double num4 = x6 + width7;
          string text = (index + 1).ToString() + ". " + conditionProgress.Empire.Name;
          SizeF sizeF = graphics.MeasureString(text, this._BoldFont, (int) width2);
          int x7 = (int) ((width2 - (double) sizeF.Width) / 2.0);
          int y = (int) (num2 + (rowHeight - (double) sizeF.Height) / 2.0);
          this.DrawStringWithDropShadow(graphics, text, this._BoldFont, new Point(x7, y));
          if (conditionProgress.Empire == this._Galaxy.PlayerEmpire)
          {
            using (Pen pen = new Pen(Color.Yellow, 2f))
            {
              Rectangle rect2 = new Rectangle(0, (int) num2 - 2, this.Width, (int) this._RowHeight + 4);
              graphics.DrawRectangle(pen, rect2);
            }
          }
          num2 += rowHeight + rowSpacing;
        }
      }
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      this.DrawStringWithDropShadow(graphics, text, font, location, (Brush) this._WhiteBrush);
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

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Brush brush)
    {
      location = new Point(location.X + 1, location.Y + 1);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location);
      location = new Point(location.X - 1, location.Y - 1);
      graphics.DrawString(text, font, brush, (PointF) location);
    }

    private void DrawStringWithDropShadowBounded(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SizeF size)
    {
      this.DrawStringWithDropShadowBounded(graphics, text, font, location, size, Color.Empty);
    }

    private void DrawStringWithDropShadowBounded(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SizeF size,
      Color textColor)
    {
      location = new Point(location.X + 1, location.Y + 1);
      PointF pointF = new PointF((float) location.X, (float) location.Y);
      RectangleF layoutRectangle = new RectangleF(pointF.X, pointF.Y, size.Width + 2f, size.Height + 2f);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, layoutRectangle, StringFormat.GenericTypographic);
      location = new Point(location.X - 1, location.Y - 1);
      pointF = new PointF((float) location.X, (float) location.Y);
      layoutRectangle = new RectangleF(pointF.X, pointF.Y, size.Width + 2f, size.Height + 2f);
      if (textColor.IsEmpty)
      {
        graphics.DrawString(text, font, (Brush) this._WhiteBrush, layoutRectangle, StringFormat.GenericTypographic);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(textColor))
          graphics.DrawString(text, font, (Brush) solidBrush, layoutRectangle, StringFormat.GenericTypographic);
      }
    }
  }
}
