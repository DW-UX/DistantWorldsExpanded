// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HabitatAttitudeSummary
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class HabitatAttitudeSummary : GradientPanel
  {
    private IContainer components;
    private Habitat _Habitat;
    private Galaxy _Galaxy;
    private Font _NormalFont = new Font("Verdana", 8f, FontStyle.Regular);
    private Font _NormalFontBold = new Font("Verdana", 8f, FontStyle.Bold);
    private Color _NormalFontColor = Color.FromArgb(170, 170, 170);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    public override void SetFontCache(IFontCache fontCache)
    {
      this._FontCache = fontCache;
      this._NormalFont = this._FontCache.GenerateFont(16.67f, false);
      this._NormalFontBold = this._FontCache.GenerateFont(16.67f, true);
    }

    public void ClearData()
    {
      this._Habitat = (Habitat) null;
      this._Galaxy = (Galaxy) null;
    }

    public void BindData(Galaxy galaxy, Habitat habitat)
    {
      this._Galaxy = galaxy;
      this._Habitat = habitat;
      this.BackColor = Color.Transparent;
      this.BackColor2 = Color.Transparent;
      this.BackColor3 = Color.Transparent;
      this.BorderStyle = BorderStyle.None;
      this.BorderWidth = 0;
      this.Curvature = 0;
      this.CurveMode = CornerCurveMode.None;
      this.Padding = new Padding(0);
      this.Height = 0;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawSummary(e.Graphics);
    }

    private void DrawSummary(Graphics graphics)
    {
      if (this._Habitat == null || this._Galaxy == null)
        return;
      float x = 0.0f;
      double y1 = 0.0;
      using (SolidBrush solidBrush1 = new SolidBrush(this._NormalFontColor))
      {
        StringFormat format = new StringFormat((StringFormatFlags) 0);
        string str1 = this.ResolveFeelingDescription(this._Habitat.EmpireApprovalRating);
        string str2 = string.Format(TextResolver.GetText("The inhabitants of COLONY are FEELING with you"), (object) this._Habitat.Name, (object) str1) + " (" + this._Habitat.EmpireApprovalRating.ToString("+0;-0;0") + ")";
        SizeF sizeF = graphics.MeasureString(str2, this._NormalFontBold, this.Width, format);
        RectangleF layoutRectangle = new RectangleF(x, (float) y1, sizeF.Width + 2f, sizeF.Height);
        graphics.DrawString(str2, this._NormalFontBold, (Brush) solidBrush1, layoutRectangle, format);
        double y2 = y1 + (double) ((int) sizeF.Height + 8);
        if (this._Habitat.RaidCountdown > (byte) 0)
        {
          string str3 = TextResolver.GetText("Raid Economy Damage Description") + " (-" + this._Habitat.RaidEconomyDamageFactor.ToString("0%") + ")";
          sizeF = graphics.MeasureString(str3, this._NormalFont, this.Width, format);
          layoutRectangle = new RectangleF(x, (float) y2, sizeF.Width + 2f, sizeF.Height);
          graphics.DrawString(str3, this._NormalFont, (Brush) solidBrush1, layoutRectangle, format);
          y2 += (double) ((int) sizeF.Height + 6);
        }
        if (this._Habitat.Empire != null)
        {
          if (this._Habitat.Empire.EconomyEfficiency > 1.0)
          {
            string str4 = TextResolver.GetText("Economy Efficiency Bonus Description") + " (+" + (this._Habitat.Empire.EconomyEfficiency - 1.0).ToString("0%") + ")";
            sizeF = graphics.MeasureString(str4, this._NormalFont, this.Width, format);
            layoutRectangle = new RectangleF(x, (float) y2, sizeF.Width + 2f, sizeF.Height);
            graphics.DrawString(str4, this._NormalFont, (Brush) solidBrush1, layoutRectangle, format);
            y2 += (double) ((int) sizeF.Height + 6);
          }
          else if (this._Habitat.Empire.EconomyEfficiency < 1.0)
          {
            string str5 = TextResolver.GetText("Economy Efficiency Penalty Description") + " (-" + (1.0 - this._Habitat.Empire.EconomyEfficiency).ToString("0%") + ")";
            sizeF = graphics.MeasureString(str5, this._NormalFont, this.Width, format);
            layoutRectangle = new RectangleF(x, (float) y2, sizeF.Width + 2f, sizeF.Height);
            graphics.DrawString(str5, this._NormalFont, (Brush) solidBrush1, layoutRectangle, format);
            y2 += (double) ((int) sizeF.Height + 6);
          }
        }
        PirateColonyControlList pirateControl = this._Habitat.GetPirateControl();
        if (pirateControl != null && pirateControl.Count > 0)
        {
          double num = 0.0;
          PirateColonyControl highestControl = pirateControl.GetHighestControl();
          if (highestControl != null)
            num = (double) (highestControl.ControlLevel / 10f);
          if (this._Habitat.Facilities != null)
          {
            PlanetaryFacility completedPirateFacility = this._Habitat.Facilities.FindBestCompletedPirateFacility(true);
            if (completedPirateFacility != null)
              num += (double) completedPirateFacility.Value3 / 100.0;
          }
          if (num > 0.0)
          {
            string str6 = TextResolver.GetText("Pirate Corruption Description") + " (+" + num.ToString("0%") + ")";
            sizeF = graphics.MeasureString(str6, this._NormalFont, this.Width, format);
            layoutRectangle = new RectangleF(x, (float) y2, sizeF.Width + 2f, sizeF.Height);
            graphics.DrawString(str6, this._NormalFont, (Brush) solidBrush1, layoutRectangle, format);
            y2 += (double) ((int) sizeF.Height + 6);
          }
        }
        List<string> habitatWonderBonuses = this.DetermineHabitatWonderBonuses();
        for (int index = 0; index < habitatWonderBonuses.Count; ++index)
        {
          sizeF = graphics.MeasureString(habitatWonderBonuses[index], this._NormalFont, this.Width, format);
          layoutRectangle = new RectangleF(x, (float) y2, sizeF.Width + 2f, sizeF.Height);
          graphics.DrawString(habitatWonderBonuses[index], this._NormalFont, (Brush) solidBrush1, layoutRectangle, format);
          y2 += (double) ((int) sizeF.Height + 6);
        }
        List<string> habitatRacialBonuses = this.DetermineHabitatRacialBonuses();
        for (int index = 0; index < habitatRacialBonuses.Count; ++index)
        {
          sizeF = graphics.MeasureString(habitatRacialBonuses[index], this._NormalFont, this.Width, format);
          layoutRectangle = new RectangleF(x, (float) y2, sizeF.Width + 2f, sizeF.Height);
          graphics.DrawString(habitatRacialBonuses[index], this._NormalFont, (Brush) solidBrush1, layoutRectangle, format);
          y2 += (double) ((int) sizeF.Height + 6);
        }
        List<string> habitatResourceBonuses = this.DetermineHabitatResourceBonuses();
        for (int index = 0; index < habitatResourceBonuses.Count; ++index)
        {
          sizeF = graphics.MeasureString(habitatResourceBonuses[index], this._NormalFont, this.Width, format);
          layoutRectangle = new RectangleF(x, (float) y2, sizeF.Width + 2f, sizeF.Height);
          graphics.DrawString(habitatResourceBonuses[index], this._NormalFont, (Brush) solidBrush1, layoutRectangle, format);
          y2 += (double) ((int) sizeF.Height + 6);
        }
        HabitatAttitudeFactorList habitatAttitudeFactors = this.DetermineHabitatAttitudeFactors();
        for (int index = 0; index < habitatAttitudeFactors.Count; ++index)
        {
          double num = habitatAttitudeFactors[index].Value;
          string str7 = habitatAttitudeFactors[index].Description + " (" + num.ToString("+0;-0;0") + ")";
          sizeF = graphics.MeasureString(str7, this._NormalFont, this.Width, format);
          layoutRectangle = new RectangleF(x, (float) y2, sizeF.Width + 2f, sizeF.Height);
          if (num < 0.0)
          {
            using (SolidBrush solidBrush2 = new SolidBrush(Color.Red))
              graphics.DrawString(str7, this._NormalFont, (Brush) solidBrush2, layoutRectangle, format);
          }
          else
          {
            using (SolidBrush solidBrush3 = new SolidBrush(Color.LightGreen))
              graphics.DrawString(str7, this._NormalFont, (Brush) solidBrush3, layoutRectangle, format);
          }
          y2 += (double) ((int) sizeF.Height + 2);
        }
        if (this.Height >= (int) y2 + 1)
          return;
        this.Height = (int) y2 + 1;
      }
    }

    private List<string> DetermineHabitatWonderBonuses()
    {
      List<string> habitatWonderBonuses = new List<string>();
      if (this._Habitat.Facilities != null && this._Habitat.Facilities.Count > 0)
      {
        for (int index = 0; index < this._Habitat.Facilities.Count; ++index)
        {
          PlanetaryFacility facility = this._Habitat.Facilities[index];
          if (facility != null && (double) facility.ConstructionProgress >= 1.0 && facility.Type == PlanetaryFacilityType.Wonder)
          {
            string str = this._Galaxy.ResolveWonderDescriptionShort(Galaxy.PlanetaryFacilityDefinitionsStatic[facility.PlanetaryFacilityDefinitionId]);
            habitatWonderBonuses.Add(str);
          }
        }
      }
      return habitatWonderBonuses;
    }

    private List<string> DetermineHabitatResourceBonuses()
    {
      List<string> habitatResourceBonuses = new List<string>();
      if (this._Habitat.ResourceBonuses != null)
      {
        for (int index = 0; index < this._Habitat.ResourceBonuses.Count; ++index)
        {
          ResourceBonus resourceBonuse = this._Habitat.ResourceBonuses[index];
          if (resourceBonuse != null && resourceBonuse.Effect != ColonyResourceEffect.Happiness)
            habitatResourceBonuses.Add(Galaxy.ResolveDescription(resourceBonuse));
        }
      }
      return habitatResourceBonuses;
    }

    private List<string> DetermineHabitatRacialBonuses()
    {
      List<string> habitatRacialBonuses = new List<string>();
      if (this._Habitat.Population != null && this._Habitat.Population.DominantRace != null)
      {
        Race dominantRace = this._Habitat.Population.DominantRace;
        if (this._Habitat.Characters != null)
        {
          CharacterList transferringCharacters = this._Habitat.Characters.GetNonTransferringCharacters(CharacterRole.ColonyGovernor);
          if (transferringCharacters.Count > 0 && transferringCharacters[0] != null)
            habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Colony governor provides bonuses to this colony"), (object) transferringCharacters[0].Name));
        }
        if (dominantRace.WarWearinessAttenuation > 0)
        {
          double num = (double) dominantRace.WarWearinessAttenuation / 100.0;
          habitatRacialBonuses.Add(string.Format(TextResolver.GetText("The RACE give this colony lower war weariness"), (object) dominantRace.Name) + " (-" + num.ToString("0%") + ")");
        }
        RaceEventType raceEventType = this._Habitat.RaceEventType;
        if ((uint) raceEventType <= 6U)
        {
          switch (raceEventType - (byte) 1)
          {
            case RaceEventType.Undefined:
              habitatRacialBonuses.Add(TextResolver.GetText("Race Event Colony Description NepthysWineVintage"));
              break;
            case RaceEventType.NepthysWineVintage:
              break;
            case RaceEventType.UnderwaterLeviathan:
              habitatRacialBonuses.Add(TextResolver.GetText("Race Event Colony Description GreatHuntStrongTroops"));
              break;
            default:
              if (raceEventType == RaceEventType.WarriorWaveTroopRecruitment)
              {
                habitatRacialBonuses.Add(TextResolver.GetText("Race Event Colony Description WarriorWave"));
                break;
              }
              break;
          }
        }
        else
        {
          switch (raceEventType - (byte) 11)
          {
            case RaceEventType.Undefined:
              habitatRacialBonuses.Add(TextResolver.GetText("Race Event Colony Description AntiXenoRiotsExterminate"));
              break;
            case RaceEventType.NepthysWineVintage:
              habitatRacialBonuses.Add(TextResolver.GetText("Race Event Colony Description XenophobiaNoAssimilate"));
              break;
            default:
              if (raceEventType != RaceEventType.TodashGalacticChampionships)
              {
                switch (raceEventType - (byte) 26)
                {
                  case RaceEventType.Undefined:
                    string str = string.Empty;
                    if (this._Habitat.Empire != null && this._Habitat.Empire.DominantRace != null)
                      str = this._Habitat.Empire.DominantRace.Name;
                    habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Event Colony Description DeathCultExterminate"), (object) str));
                    break;
                  case RaceEventType.UnderwaterLeviathan:
                    habitatRacialBonuses.Add(TextResolver.GetText("Race Event Colony Description PredictiveHistory"));
                    break;
                }
              }
              else
              {
                habitatRacialBonuses.Add(TextResolver.GetText("Race Event Colony Description TodashGalacticChampionships"));
                break;
              }
              break;
          }
        }
        if (dominantRace.SatisfactionModifier > 0)
        {
          double num = (double) dominantRace.SatisfactionModifier / 100.0;
          habitatRacialBonuses.Add(string.Format(TextResolver.GetText("The RACE give this colony higher happiness"), (object) dominantRace.Name) + " (+" + num.ToString("0%") + ")");
        }
        if ((double) this._Habitat.SlaveryBonusFactor > 1.0)
          habitatRacialBonuses.Add(TextResolver.GetText("Slavery gives this colony higher income") + " (+" + (this._Habitat.SlaveryBonusFactor - 1f).ToString("0%") + ")");
        switch (this._Habitat.Type)
        {
          case HabitatType.Volcanic:
            if (dominantRace.ColonyConstructionSpeedFactorVolcanic > 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Increase"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorVolcanic - 1.0).ToString("+0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            if (dominantRace.ColonyConstructionSpeedFactorVolcanic < 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Decrease"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorVolcanic - 1.0).ToString("-0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            break;
          case HabitatType.Desert:
            if (dominantRace.ColonyConstructionSpeedFactorDesert > 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Increase"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorDesert - 1.0).ToString("+0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            if (dominantRace.ColonyConstructionSpeedFactorDesert < 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Decrease"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorDesert - 1.0).ToString("-0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            break;
          case HabitatType.MarshySwamp:
            if (dominantRace.ColonyConstructionSpeedFactorMarshySwamp > 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Increase"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorMarshySwamp - 1.0).ToString("+0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            if (dominantRace.ColonyConstructionSpeedFactorMarshySwamp < 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Decrease"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorMarshySwamp - 1.0).ToString("-0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            break;
          case HabitatType.Continental:
            if (dominantRace.ColonyConstructionSpeedFactorContinental > 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Increase"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorContinental - 1.0).ToString("+0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            if (dominantRace.ColonyConstructionSpeedFactorContinental < 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Decrease"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorContinental - 1.0).ToString("-0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            break;
          case HabitatType.Ocean:
            if (dominantRace.ColonyConstructionSpeedFactorOcean > 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Increase"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorOcean - 1.0).ToString("+0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            if (dominantRace.ColonyConstructionSpeedFactorOcean < 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Decrease"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorOcean - 1.0).ToString("-0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            break;
          case HabitatType.Ice:
            if (dominantRace.ColonyConstructionSpeedFactorIce > 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Increase"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorIce - 1.0).ToString("+0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            if (dominantRace.ColonyConstructionSpeedFactorIce < 1.0)
            {
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus ConstructionSpeed Decrease"), (object) dominantRace.Name, (object) (dominantRace.ColonyConstructionSpeedFactorIce - 1.0).ToString("-0%"), (object) Galaxy.ResolveDescription(this._Habitat.Type)));
              break;
            }
            break;
        }
        if ((this._Habitat.ColonyPopulationPolicy == ColonyPopulationPolicy.Exterminate || this._Habitat.ColonyPopulationPolicyRaceFamily == ColonyPopulationPolicy.Exterminate) && dominantRace.ColonyPopulationPolicyGrowthFactorExterminate != 1.0)
        {
          bool flag = false;
          for (int index = 0; index < this._Habitat.Population.Count; ++index)
          {
            Population population = this._Habitat.Population[index];
            if (population != null && population.Race != null)
            {
              if (this._Habitat.ColonyPopulationPolicyRaceFamily == ColonyPopulationPolicy.Exterminate && population.Race != dominantRace && (int) population.Race.FamilyId == (int) dominantRace.FamilyId)
              {
                flag = true;
                break;
              }
              if (this._Habitat.ColonyPopulationPolicy == ColonyPopulationPolicy.Exterminate && population.Race != dominantRace && (int) population.Race.FamilyId != (int) dominantRace.FamilyId)
              {
                flag = true;
                break;
              }
            }
          }
          if (flag)
          {
            if (dominantRace.ColonyPopulationPolicyGrowthFactorExterminate > 1.0)
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus Extermination Growth Increase"), (object) dominantRace.Name, (object) (dominantRace.ColonyPopulationPolicyGrowthFactorExterminate - 1.0).ToString("+0%")));
            else if (dominantRace.ColonyPopulationPolicyGrowthFactorExterminate < 1.0)
              habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus Extermination Growth Decrease"), (object) dominantRace.Name, (object) (dominantRace.ColonyPopulationPolicyGrowthFactorExterminate - 1.0).ToString("-0%")));
          }
        }
        if (dominantRace.SpaceportArmorStrengthFactor > 1.0)
          habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus Spaceport Armor Strength Increase"), (object) dominantRace.Name, (object) (dominantRace.SpaceportArmorStrengthFactor - 1.0).ToString("+0%")));
        if (dominantRace.MigrationFactor > 1.0)
          habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus Migration Increase"), (object) dominantRace.Name, (object) (dominantRace.MigrationFactor - 1.0).ToString("+0%")));
        if (dominantRace.TroopRegenerationFactor > 1.0)
          habitatRacialBonuses.Add(string.Format(TextResolver.GetText("Race Colony Bonus Troop Regeneration Increase"), (object) dominantRace.Name, (object) (dominantRace.TroopRegenerationFactor - 1.0).ToString("+0%")));
      }
      return habitatRacialBonuses;
    }

    private HabitatAttitudeFactorList DetermineHabitatAttitudeFactors()
    {
      HabitatAttitudeFactorList habitatAttitudeFactors = new HabitatAttitudeFactorList();
      double empireApprovalRating = this._Habitat.EmpireApprovalRating;
      double num1 = this._Habitat.ModifyApprovalValueByEmpireAttributes((double) this._Habitat.DevelopmentLevel / 5.0);
      string empty = string.Empty;
      string description1 = num1 <= 16.0 ? (num1 <= 12.0 ? (num1 <= 8.0 ? (num1 <= 4.0 ? TextResolver.GetText("Our colony has begun to develop") : TextResolver.GetText("Our colony has some development")) : TextResolver.GetText("Our colony has a reasonable level of development")) : TextResolver.GetText("Our colony has a high level of development")) : TextResolver.GetText("Our colony has a very high level of development");
      habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num1, description1));
      double num2 = this._Habitat.ModifyApprovalValueByEmpireAttributes(this._Habitat.TaxApproval);
      if (num2 > 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num2, TextResolver.GetText("We approve of the current tax rate")));
      else if (num2 < 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num2, TextResolver.GetText("The current tax rate is too high!")));
      if (this._Habitat.Empire.SpecialBonusHappiness > 0.0 && this._Habitat.Empire.SpecialBonusHappinessRuin != null)
      {
        double num3 = 0.0;
        if (this._Habitat.EmpireApprovalRating > 0.0)
          num3 = this._Habitat.EmpireApprovalRating / (1.0 + this._Habitat.Empire.SpecialBonusHappiness);
        else if (this._Habitat.EmpireApprovalRating < 0.0)
          num3 = this._Habitat.EmpireApprovalRating * (1.0 + this._Habitat.Empire.SpecialBonusHappiness);
        double num4 = Math.Abs(num3 * this._Habitat.Empire.SpecialBonusHappiness);
        string description2 = string.Empty;
        Habitat habitat = this._Galaxy.IdentifyRuinHabitat(this._Habitat.Empire.SpecialBonusHappinessRuin);
        if (habitat != null)
        {
          Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat);
          description2 = string.Format(TextResolver.GetText("Our empire happiness is enhanced by the RUIN at COLONY in the SYSTEM"), (object) this._Habitat.Empire.SpecialBonusHappinessRuin.Name, (object) habitat.Name, (object) habitatSystemStar.Name);
        }
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num4, description2));
      }
      if (this._Habitat.Empire.LeaderChangeInfluence != 0.0)
      {
        double num5 = this._Habitat.ModifyApprovalValueByEmpireAttributes(this._Habitat.Empire.LeaderChangeInfluence * 20.0);
        string str = string.Empty;
        if (this._Habitat.Empire.Leader != null)
          str = this._Habitat.Empire.Leader.Name;
        if (num5 > 0.0)
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num5, string.Format(TextResolver.GetText("Leader Change Colony Boost"), (object) str)));
        else
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num5, string.Format(TextResolver.GetText("Leader Change Colony Disrupt"), (object) str)));
      }
      int num6 = 0;
      bool flag = false;
      if (this._Habitat.Characters != null && this._Habitat.Characters.Count > 0)
      {
        num6 += this._Habitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyHappiness);
        if (num6 != 0)
          flag = true;
      }
      if (this._Habitat.Empire != null && this._Habitat.Empire.Leader != null)
        num6 += this._Habitat.Empire.Leader.ColonyHappiness;
      double num7 = (double) num6 / 100.0;
      if (num7 != 0.0)
      {
        double num8 = empireApprovalRating <= 0.0 ? empireApprovalRating - empireApprovalRating * (1.0 + num7) : empireApprovalRating - empireApprovalRating / (1.0 + num7);
        if (flag)
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num8, string.Format(TextResolver.GetText("Character bonus at colony Leader and Governor"), (object) num7.ToString("+0%;-0%"))));
        else
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num8, string.Format(TextResolver.GetText("Character bonus at colony Leader only"), (object) num7.ToString("+0%;-0%"))));
      }
      double num9 = 0.0;
      if (this._Habitat.Facilities != null && this._Habitat.Facilities.Count > 0)
      {
        for (int index = 0; index < this._Habitat.Facilities.Count; ++index)
        {
          PlanetaryFacility facility = this._Habitat.Facilities[index];
          if (facility != null && (double) facility.ConstructionProgress >= 1.0 && facility.Type == PlanetaryFacilityType.Wonder && facility.WonderType == WonderType.ColonyHappiness)
            num9 = (double) facility.Value2 / 100.0;
        }
      }
      if (num9 > 0.0)
      {
        double num10 = empireApprovalRating <= 0.0 ? empireApprovalRating - empireApprovalRating * (1.0 + num9) : empireApprovalRating - empireApprovalRating / (1.0 + num9);
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num10, string.Format(TextResolver.GetText("Wonders provide a X happiness bonus"), (object) num9.ToString("+0%;-0%"))));
      }
      if (this._Habitat.ResourceBonuses != null)
      {
        for (int index = 0; index < this._Habitat.ResourceBonuses.Count; ++index)
        {
          ResourceBonus resourceBonuse = this._Habitat.ResourceBonuses[index];
          if (resourceBonuse != null && resourceBonuse.Effect == ColonyResourceEffect.Happiness)
            habitatAttitudeFactors.Add(new HabitatAttitudeFactor(resourceBonuse.Value, Galaxy.ResolveDescription(resourceBonuse)));
        }
      }
      if (this._Habitat.Empire != null && this._Habitat.Empire != this._Galaxy.IndependentEmpire)
      {
        double num11 = this._Habitat.ModifyApprovalValueByEmpireAttributes(this._Habitat.Empire.WarWeariness * -0.3);
        if (this._Habitat.Empire.GovernmentAttributes != null && this._Habitat.Empire.GovernmentAttributes.WarWeariness != 0.0)
          num11 *= this._Habitat.Empire.GovernmentAttributes.WarWeariness;
        if (this._Habitat.Population != null && this._Habitat.Population.DominantRace != null && this._Habitat.Population.DominantRace.WarWearinessAttenuation > 0)
        {
          double num12 = (double) this._Habitat.Population.DominantRace.WarWearinessAttenuation / 100.0;
          num11 *= 1.0 - num12;
        }
        if (num11 > 0.0)
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num11, TextResolver.GetText("We are happy that our empire is at peace")));
        else if (num11 < 0.0)
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num11, TextResolver.GetText("We tire of our empire's wars")));
      }
      double num13 = this._Habitat.ModifyApprovalValueByEmpireAttributes((double) this._Habitat.CulturalDistressFactor * -1.0);
      if (num13 > 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num13, TextResolver.GetText("We have no cultural distress")));
      else if (num13 < 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num13, TextResolver.GetText("We are in awe of nearby colonies of other empires")));
      double num14 = this._Habitat.ModifyApprovalValueByEmpireAttributes((double) this._Habitat.ConqueredFactor);
      if (num14 < 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num14, TextResolver.GetText("We are angry at the recent conquest of our colony")));
      double num15 = this._Habitat.ModifyApprovalValueByEmpireAttributes(-15.0 * (1.0 - this._Habitat.CalculateStrategicResourceSupplyGrowthFactor()));
      if (num15 < 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num15, TextResolver.GetText("Resource shortages are hampering the growth of our colony")));
      double num16 = this._Habitat.ModifyApprovalValueByEmpireAttributes((double) this._Habitat.HappinessModifier);
      if (num16 > 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num16, TextResolver.GetText("Recreational and medical facilities benefit us")));
      else if (num16 < 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num16, TextResolver.GetText("We have been incited to rebellion")));
      double num17 = 0.0;
      if (this._Habitat.Owner != null && this._Habitat.Owner != this._Galaxy.IndependentEmpire)
        num17 = this._Habitat.Owner.CivilityRatingApprovalRaw;
      double num18 = 1.0;
      if (this._Habitat.Population != null && this._Habitat.Population.DominantRace != null)
        num18 = this._Habitat.Empire.CalculateRacialReputationConcern(this._Habitat.Population.DominantRace);
      double num19 = this._Habitat.ModifyApprovalValueByEmpireAttributes(num17 / num18);
      if (num19 > 0.5)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num19, TextResolver.GetText("We are proud of our empire's good reputation")));
      else if (num19 < -0.5)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num19, TextResolver.GetText("We are concerned about our empire's poor reputation")));
      double num20 = this._Habitat.ModifyApprovalValueByEmpireAttributes(this._Habitat.RacialHappiness);
      if (num20 > 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num20, TextResolver.GetText("We have no racial unhappiness")));
      else if (num20 < 0.0 && this._Habitat.Population != null && this._Habitat.Population.DominantRace != null && this._Habitat.Empire != null && this._Habitat.Empire.DominantRace != null)
      {
        Race dominantRace1 = this._Habitat.Population.DominantRace;
        Race dominantRace2 = this._Habitat.Empire.DominantRace;
        string description3 = string.Format(TextResolver.GetText("RACE are unhappy being part of our RACE empire"), (object) dominantRace1.Name, (object) dominantRace2.Name);
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num20, description3));
      }
      double exterminationConcern = 0.0;
      double slaveryConcern = 0.0;
      double populationPolicyConcern = this._Habitat.CalculatePopulationPolicyConcern(out exterminationConcern, out slaveryConcern);
      if (populationPolicyConcern < 0.0)
      {
        if (exterminationConcern < 0.0 && slaveryConcern < 0.0)
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(populationPolicyConcern, TextResolver.GetText("The inhabitants are upset at your harsh policy of enslavement and extermination")));
        else if (exterminationConcern < 0.0)
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(populationPolicyConcern, TextResolver.GetText("The inhabitants are upset at your harsh policy of extermination")));
        else
          habitatAttitudeFactors.Add(new HabitatAttitudeFactor(populationPolicyConcern, TextResolver.GetText("The inhabitants are upset at your harsh policy of enslavement")));
      }
      double num21 = this._Habitat.ModifyApprovalValueByEmpireAttributes(this._Habitat.WarWithOurRace);
      if (num21 > 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num21, TextResolver.GetText("You are not at war with empires of our race")));
      else if (num21 < 0.0 && this._Habitat.Population != null && this._Habitat.Population.DominantRace != null)
      {
        Race dominantRace = this._Habitat.Population.DominantRace;
        string description4 = string.Format(TextResolver.GetText("RACE are upset that we are at war with their species"), (object) dominantRace.Name);
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num21, description4));
      }
      double num22 = this._Habitat.ModifyApprovalValueByEmpireAttributes((double) this._Habitat.Damage * -20.0);
      if (num22 < 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num22, TextResolver.GetText("Our colony's environment has been ravaged by destruction")));
      double num23 = this._Habitat.ModifyApprovalValueByEmpireAttributes(this._Habitat.RaidEconomyDamageFactor * -20.0);
      if (num23 < 0.0)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num23, TextResolver.GetText("Raid Unhappiness Description")));
      Plague plague = (Plague) null;
      double num24 = this._Habitat.ModifyApprovalValueByEmpireAttributes(this._Habitat.GetPlagueUnhappinessFactor(out plague));
      if (num24 < 0.0 && plague != null)
        habitatAttitudeFactors.Add(new HabitatAttitudeFactor(num24, string.Format(TextResolver.GetText("Plague Unhappiness Description"), (object) plague.Name)));
      habitatAttitudeFactors.Sort();
      habitatAttitudeFactors.Reverse();
      return habitatAttitudeFactors;
    }

    private string ResolveFeelingDescription(double approvalRating)
    {
      string empty = string.Empty;
      return approvalRating <= 15.0 ? (approvalRating <= 0.0 ? (approvalRating <= -15.0 ? TextResolver.GetText("angry") : TextResolver.GetText("unhappy")) : TextResolver.GetText("satisfied")) : TextResolver.GetText("happy");
    }
  }
}
