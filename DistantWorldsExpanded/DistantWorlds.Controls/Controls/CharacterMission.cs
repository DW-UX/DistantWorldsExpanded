// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterMission
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CharacterMission : GradientPanel
  {
    private IContainer components;
    private EmpireDropDown cmbTargetEmpire;
    private ComboBox cmbMissionType;
    private ComboBox cmbTargetObject;
    private ComboBox cmbTimeToComplete;
    private GlassButton btnCancelMission;
    private GlassButton btnAssignMission;
    private Label lblTargetEmpire;
    private Label lblMissionType;
    private Label lblTargetObject;
    private Label lblTimeToComplete;
    private Label lblMissionDifficulty;
    private Label lblMissionDifficultyLabel;
    private Label lblMissionDifficultyWarning;
    private LinkLabel lnkMissionTypes;
    private Empire _PlayerEmpire;
    private Character _Character;
    private Galaxy _Galaxy;
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
    private double _MissionDifficultyCautionFactor = 1.5;
    private double _MissionDifficultyWarningFactor = 1.0;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _LargeFont;
    private Font _TitleFont;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    public event EventHandler MissionAssigned;

    public event EventHandler MissionCancelled;

    public event EventHandler MissionTypeHelp;

    public CharacterMission()
    {
      this.InitializeComponent();
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this._LargeFont = new Font(this.Font.FontFamily, FontSize.Large, FontStyle.Regular, GraphicsUnit.Pixel);
      this._BoldFont = new Font(this.Font.FontFamily, FontSize.Large, FontStyle.Bold, GraphicsUnit.Pixel);
      this._TitleFont = new Font(this.Font.FontFamily, FontSize.Title, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void ClearData()
    {
      this._PlayerEmpire = (Empire) null;
      this._Character = (Character) null;
      this._Galaxy = (Galaxy) null;
    }

    public void BindData(Empire playerEmpire, Galaxy galaxy, Character character)
    {
      this._PlayerEmpire = playerEmpire;
      this._Galaxy = galaxy;
      this._Character = character;
      this._LargeFont = new Font(this.Font.FontFamily, FontSize.Large, FontStyle.Regular, GraphicsUnit.Pixel);
      this._BoldFont = new Font(this.Font.FontFamily, FontSize.Large, FontStyle.Bold, GraphicsUnit.Pixel);
      this._TitleFont = new Font(this.Font.FontFamily, FontSize.Title, FontStyle.Bold, GraphicsUnit.Pixel);
      DistantWorlds.Types.EmpireList empires = new DistantWorlds.Types.EmpireList();
      for (int index = 0; index < this._Galaxy.Empires.Count; ++index)
      {
        Empire empire = this._Galaxy.Empires[index];
        if (this._PlayerEmpire != empire && empire != null)
        {
          if (playerEmpire.PirateEmpireBaseHabitat == null)
          {
            DiplomaticRelation diplomaticRelation = this._PlayerEmpire.DiplomaticRelations[empire];
            if (diplomaticRelation != null && diplomaticRelation.Type != DiplomaticRelationType.NotMet)
              empires.Add(empire);
          }
          else
          {
            PirateRelation pirateRelation = this._PlayerEmpire.ObtainPirateRelation(empire);
            if (pirateRelation != null && pirateRelation.Type != PirateRelationType.NotMet)
              empires.Add(empire);
          }
        }
      }
      for (int index = 0; index < this._Galaxy.PirateEmpires.Count; ++index)
      {
        Empire pirateEmpire = this._Galaxy.PirateEmpires[index];
        if (this._PlayerEmpire != pirateEmpire && pirateEmpire != null)
        {
          PirateRelation pirateRelation = this._PlayerEmpire.ObtainPirateRelation(pirateEmpire);
          if (pirateRelation != null && pirateRelation.Type != PirateRelationType.NotMet)
            empires.Add(pirateEmpire);
        }
      }
      empires.Sort();
      this.cmbTargetEmpire.BindData(playerEmpire, empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
      this.cmbMissionType.Items.Clear();
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.CounterIntelligence));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.SabotageConstruction));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.DestroyBase));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.StealTerritoryMap));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.StealOperationsMap));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.StealGalaxyMap));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.StealTechData));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.SabotageColony));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.InciteRevolution));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.AssassinateCharacter));
      this.cmbMissionType.Items.Add((object) Galaxy.ResolveDescription(IntelligenceMissionType.DeepCover));
      this.cmbTimeToComplete.Items.Clear();
      this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("One month"));
      this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("Three months"));
      this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("One year"));
      this.lblTargetEmpire.Location = new Point(10, 9);
      this.cmbTargetEmpire.Size = new Size(180, 21);
      this.cmbTargetEmpire.Location = new Point(10, 23);
      this.lblMissionType.Location = new Point(10, 52);
      this.cmbMissionType.Size = new Size(180, 21);
      this.cmbMissionType.Location = new Point(10, 66);
      this.lblTargetObject.Location = new Point(10, 95);
      this.cmbTargetObject.Size = new Size(180, 21);
      this.cmbTargetObject.Location = new Point(10, 109);
      this.lblTimeToComplete.Location = new Point(10, 138);
      this.cmbTimeToComplete.Size = new Size(180, 21);
      this.cmbTimeToComplete.Location = new Point(10, 152);
      this.lblMissionDifficulty.Location = new Point(275, 28);
      this.lblMissionDifficultyLabel.BringToFront();
      this.lblMissionDifficultyLabel.Location = new Point(200, 56);
      this.lblMissionDifficultyLabel.MaximumSize = new Size(190, 20);
      this.lblMissionDifficultyLabel.Size = new Size(190, 20);
      this.lblMissionDifficultyLabel.AutoSize = false;
      this.lblMissionDifficultyLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.lblMissionDifficultyWarning.Visible = true;
      this.lblMissionDifficultyWarning.Location = new Point(200, 70);
      this.lblMissionDifficultyWarning.MaximumSize = new Size(190, 82);
      this.lblMissionDifficultyWarning.Size = new Size(190, 82);
      this.lblMissionDifficultyWarning.AutoSize = false;
      this.lblMissionDifficultyWarning.TextAlign = ContentAlignment.MiddleCenter;
      this.btnAssignMission.Size = new Size(190, 25);
      this.btnAssignMission.Location = new Point(200, 152);
      this.btnCancelMission.Size = new Size(190, 25);
      this.btnCancelMission.Location = new Point(200, 152);
      this.lnkMissionTypes.Location = new Point(200, 5);
      this.lnkMissionTypes.Size = new Size(190, 20);
      this.lnkMissionTypes.MaximumSize = new Size(190, 20);
      this.lnkMissionTypes.AutoSize = false;
      this.lnkMissionTypes.TextAlign = ContentAlignment.MiddleRight;
      this.lnkMissionTypes.Text = TextResolver.GetText("Learn about Intelligence Missions...");
      this.cmbTargetObject.Visible = false;
      this.lblTargetObject.Visible = false;
      this.lblMissionDifficulty.Text = string.Empty;
      this.lblMissionDifficultyWarning.Text = string.Empty;
      this.SetControlFonts();
      this.SetControlsAndMission();
    }

    private void SetControlFonts()
    {
      this.lblMissionDifficulty.Font = this._TitleFont;
      this.lblMissionDifficultyLabel.Font = this.Font;
      this.lblMissionDifficultyWarning.Font = this._LargeFont;
      this.lblMissionDifficulty.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMissionDifficultyLabel.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMissionDifficultyWarning.ForeColor = Color.FromArgb(170, 170, 170);
      this.lnkMissionTypes.Font = this.Font;
    }

    private void SetControlsAndMission()
    {
      if (this._Character == null)
      {
        this.cmbTargetEmpire.Enabled = false;
        this.cmbMissionType.Enabled = false;
        this.cmbTargetObject.Enabled = false;
        this.cmbTimeToComplete.Enabled = false;
        this.lblMissionDifficulty.Text = string.Empty;
        this.lblMissionDifficultyLabel.Text = string.Empty;
        this.lblMissionDifficultyWarning.Text = string.Empty;
        this.btnAssignMission.Enabled = false;
        this.btnAssignMission.Visible = true;
        this.btnCancelMission.Enabled = false;
        this.btnCancelMission.Visible = false;
        this.SetState((IntelligenceMission) null);
      }
      else if (this._Character.Mission != null && this._Character.Mission.Type != IntelligenceMissionType.Undefined)
      {
        this.cmbTargetEmpire.Enabled = false;
        this.cmbMissionType.Enabled = false;
        this.cmbTargetObject.Enabled = false;
        this.cmbTimeToComplete.Enabled = false;
        this.btnAssignMission.Enabled = false;
        this.btnAssignMission.Visible = false;
        this.btnCancelMission.Enabled = true;
        this.btnCancelMission.Visible = true;
        this.lblTimeToComplete.Visible = true;
        this.cmbTimeToComplete.Visible = true;
        this.SetState(this._Character.Mission);
        this.cmbTargetObject.Enabled = false;
        string difficultyDescription = this.GetMissionDifficultyDescription(this._Character.Mission, this._Character);
        this.lblMissionDifficulty.Text = difficultyDescription;
        this.lblMissionDifficultyWarning.Text = this.GetMissionDifficultyWarning(this._Character.Mission, this._Character);
        if (string.IsNullOrEmpty(difficultyDescription))
        {
          this.lblMissionDifficultyLabel.Text = string.Empty;
          this.lblMissionDifficultyLabel.Location = new Point(200, 41);
          this.lblMissionDifficultyLabel.MaximumSize = new Size(190, 50);
          this.lblMissionDifficultyLabel.Size = new Size(190, 50);
          string str = Galaxy.ResolveDescriptionCharacterTask(this._Character, this._Galaxy);
          if (string.IsNullOrEmpty(str))
            str = Galaxy.ResolveCharacterLocationDescription(this._Character);
          this.lblMissionDifficultyLabel.Text = str;
        }
        else
        {
          this.lblMissionDifficultyLabel.Location = new Point(200, 56);
          this.lblMissionDifficultyLabel.MaximumSize = new Size(190, 20);
          this.lblMissionDifficultyLabel.Size = new Size(190, 20);
          this.lblMissionDifficultyLabel.Text = TextResolver.GetText("Success Probability");
        }
      }
      else
      {
        IntelligenceMission state = this.GetState();
        if (state != null)
        {
          string difficultyDescription = this.GetMissionDifficultyDescription(state, this._Character);
          this.lblMissionDifficulty.Text = difficultyDescription;
          this.lblMissionDifficultyWarning.Text = this.GetMissionDifficultyWarning(state, this._Character);
          if (string.IsNullOrEmpty(difficultyDescription))
            this.lblMissionDifficultyLabel.Text = string.Empty;
          else
            this.lblMissionDifficultyLabel.Text = TextResolver.GetText("Success Probability");
          this.btnAssignMission.Enabled = true;
        }
        else
        {
          this.lblMissionDifficultyLabel.Text = string.Empty;
          this.btnAssignMission.Enabled = false;
        }
        this.lblTimeToComplete.Visible = true;
        this.cmbTimeToComplete.Visible = true;
        this.cmbTargetEmpire.Enabled = true;
        this.cmbMissionType.Enabled = true;
        this.cmbTargetObject.Enabled = true;
        this.cmbTimeToComplete.Enabled = true;
        this.btnAssignMission.Visible = true;
        this.btnCancelMission.Enabled = false;
        this.btnCancelMission.Visible = false;
      }
    }

    private IntelligenceMissionType ResolveMissionType()
    {
      if (this.cmbMissionType.SelectedItem != null)
      {
        string str = this.cmbMissionType.SelectedItem.ToString();
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.CounterIntelligence))
          return IntelligenceMissionType.CounterIntelligence;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.DeepCover))
          return IntelligenceMissionType.DeepCover;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.InciteRevolution))
          return IntelligenceMissionType.InciteRevolution;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.SabotageColony))
          return IntelligenceMissionType.SabotageColony;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.SabotageConstruction))
          return IntelligenceMissionType.SabotageConstruction;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.StealGalaxyMap))
          return IntelligenceMissionType.StealGalaxyMap;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.StealOperationsMap))
          return IntelligenceMissionType.StealOperationsMap;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.StealTechData))
          return IntelligenceMissionType.StealTechData;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.StealTerritoryMap))
          return IntelligenceMissionType.StealTerritoryMap;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.AssassinateCharacter))
          return IntelligenceMissionType.AssassinateCharacter;
        if (str == Galaxy.ResolveDescription(IntelligenceMissionType.DestroyBase))
          return IntelligenceMissionType.DestroyBase;
      }
      return IntelligenceMissionType.Undefined;
    }

    private ResearchNode ResolveTargetObjectResearch()
    {
      if (this.cmbTargetObject.SelectedItem != null)
      {
        string str = this.cmbTargetObject.SelectedItem.ToString();
        foreach (ResearchNode researchNode in (SyncList<ResearchNode>) this._PlayerEmpire.Research.TechTree)
        {
          if (researchNode.Name == str)
            return researchNode;
        }
      }
      return (ResearchNode) null;
    }

    private StellarObject ResolveTargetObjectColony(Empire targetEmpire)
    {
      if (this.cmbTargetObject.SelectedItem != null)
      {
        string lower = this.cmbTargetObject.SelectedItem.ToString().ToLower(CultureInfo.InvariantCulture);
        foreach (StellarObject resolveKnownColony in this._PlayerEmpire.ResolveKnownColonies(targetEmpire))
        {
          if (resolveKnownColony.Name.ToLower(CultureInfo.InvariantCulture) == lower)
            return resolveKnownColony;
        }
      }
      return (StellarObject) null;
    }

    private StellarObject ResolveTargetObjectConstructionYard(Empire targetEmpire)
    {
      if (this.cmbTargetObject.SelectedItem != null)
      {
        string str = this.cmbTargetObject.SelectedItem.ToString();
        foreach (StellarObject constructionYard in this._PlayerEmpire.ResolveKnownConstructionYards(targetEmpire))
        {
          if (constructionYard.Name == str)
            return constructionYard;
        }
      }
      return (StellarObject) null;
    }

    private StellarObject ResolveTargetObjectBase(Empire targetEmpire)
    {
      if (this.cmbTargetObject.SelectedItem != null)
      {
        string str = this.cmbTargetObject.SelectedItem.ToString();
        foreach (StellarObject resolveKnownBase in this._PlayerEmpire.ResolveKnownBases(targetEmpire))
        {
          if (resolveKnownBase.Name == str)
            return resolveKnownBase;
        }
      }
      return (StellarObject) null;
    }

    private Character ResolveTargetObjectCharacter(Empire targetEmpire)
    {
      if (this.cmbTargetObject.SelectedItem != null)
      {
        string str = this.cmbTargetObject.SelectedItem.ToString();
        foreach (Character resolveKnownCharacter in (SyncList<Character>) this._PlayerEmpire.ResolveKnownCharacters(targetEmpire))
        {
          if (resolveKnownCharacter.Name + " (" + Galaxy.ResolveDescription(resolveKnownCharacter.Role) + ")" == str)
            return resolveKnownCharacter;
        }
      }
      return (Character) null;
    }

    private long ResolveTimeToComplete()
    {
      long complete = 0;
      if (this.cmbTimeToComplete.SelectedItem != null)
      {
        long num1 = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 12);
        long num2 = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 4);
        long num3 = (long) (Galaxy.RealSecondsInGalacticYear * 1000);
        string str = this.cmbTimeToComplete.SelectedItem.ToString();
        if (str == TextResolver.GetText("One month"))
          complete = num1;
        else if (str == TextResolver.GetText("Three months"))
          complete = num2;
        else if (str == TextResolver.GetText("One year"))
          complete = num3;
        else if (str == TextResolver.GetText("Until cancelled"))
          complete = 4611686018427387903L;
      }
      return complete;
    }

    private void SetTargetObjectKnownCharacters(Empire targetEmpire)
    {
      CharacterList characterList = this._PlayerEmpire.ResolveKnownCharacters(targetEmpire);
      List<string> stringList = new List<string>();
      foreach (Character character in (SyncList<Character>) characterList)
      {
        string str = character.Name + " (" + Galaxy.ResolveDescription(character.Role) + ")";
        stringList.Add(str);
      }
      stringList.Sort();
      this.cmbTargetObject.Items.Clear();
      this.cmbTargetObject.Items.AddRange((object[]) stringList.ToArray());
    }

    private void SetTargetObjectKnownBases(Empire targetEmpire)
    {
      List<StellarObject> stellarObjectList = this._PlayerEmpire.ResolveKnownBases(targetEmpire);
      List<string> stringList = new List<string>();
      foreach (StellarObject stellarObject in stellarObjectList)
        stringList.Add(stellarObject.Name);
      stringList.Sort();
      this.cmbTargetObject.Items.Clear();
      this.cmbTargetObject.Items.AddRange((object[]) stringList.ToArray());
    }

    private void SetTargetObjectKnownConstructionYards(Empire targetEmpire)
    {
      List<StellarObject> stellarObjectList = this._PlayerEmpire.ResolveKnownConstructionYards(targetEmpire);
      List<string> stringList = new List<string>();
      foreach (StellarObject stellarObject in stellarObjectList)
      {
        if (stellarObject.Empire == targetEmpire)
          stringList.Add(stellarObject.Name);
      }
      stringList.Sort();
      this.cmbTargetObject.Items.Clear();
      this.cmbTargetObject.Items.AddRange((object[]) stringList.ToArray());
    }

    private void SetTargetObjectKnownColonies(Empire targetEmpire)
    {
      List<StellarObject> stellarObjectList = this._PlayerEmpire.ResolveKnownColonies(targetEmpire);
      List<string> stringList = new List<string>();
      foreach (StellarObject stellarObject in stellarObjectList)
      {
        if (stellarObject.Empire == targetEmpire)
          stringList.Add(stellarObject.Name);
      }
      stringList.Sort();
      this.cmbTargetObject.Items.Clear();
      this.cmbTargetObject.Items.AddRange((object[]) stringList.ToArray());
    }

    private void SetTargetObjectAdvancedResearch(Empire targetEmpire)
    {
      ResearchNodeList researchNodeList = this.ResolveAdvancedResearchProjects(targetEmpire);
      List<string> stringList = new List<string>();
      foreach (ResearchNode researchNode in (SyncList<ResearchNode>) researchNodeList)
        stringList.Add(researchNode.Name);
      stringList.Sort();
      this.cmbTargetObject.Items.Clear();
      this.cmbTargetObject.Items.AddRange((object[]) stringList.ToArray());
    }

    private ResearchNodeList ResolveAdvancedResearchProjects(Empire targetEmpire) => this._PlayerEmpire.Research.ResolveMoreAdvancedProjects(targetEmpire);

    private void SetTargetObject(ResearchNode researchProject)
    {
      int num = -1;
      for (int index = 0; index < this.cmbTargetObject.Items.Count; ++index)
      {
        if (researchProject != null && this.cmbTargetObject.Items[index].ToString() == researchProject.Name)
          num = index;
      }
      this.cmbTargetObject.SelectedIndex = num;
    }

    private void SetTargetObject(Character character)
    {
      string str = character.Name + " (" + Galaxy.ResolveDescription(character.Role) + ")";
      int num = -1;
      for (int index = 0; index < this.cmbTargetObject.Items.Count; ++index)
      {
        if (this.cmbTargetObject.Items[index].ToString() == str)
          num = index;
      }
      this.cmbTargetObject.SelectedIndex = num;
    }

    private void SetTargetObject(StellarObject stellarObject)
    {
      string name = stellarObject.Name;
      int num = -1;
      for (int index = 0; index < this.cmbTargetObject.Items.Count; ++index)
      {
        if (this.cmbTargetObject.Items[index].ToString() == name)
          num = index;
      }
      this.cmbTargetObject.SelectedIndex = num;
    }

    private void SetState(IntelligenceMission mission)
    {
      this.cmbTargetEmpire.SelectedIndexChanged -= new EventHandler(this.cmbTargetEmpire_SelectedIndexChanged);
      this.cmbMissionType.SelectedIndexChanged -= new EventHandler(this.cmbMissionType_SelectedIndexChanged);
      this.cmbTargetObject.SelectedIndexChanged -= new EventHandler(this.cmbTargetObject_SelectedIndexChanged);
      this.cmbTimeToComplete.SelectedIndexChanged -= new EventHandler(this.cmbTimeToComplete_SelectedIndexChanged);
      long num1 = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 12);
      long num2 = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 4);
      int secondsInGalacticYear = Galaxy.RealSecondsInGalacticYear;
      if (mission != null && mission.Type != IntelligenceMissionType.Undefined)
      {
        this.cmbTargetEmpire.SetSelectedEmpire(mission.TargetEmpire);
        this.cmbTargetObject.Enabled = false;
        this.cmbTargetObject.Visible = false;
        this.lblTargetObject.Visible = false;
        bool flag = true;
        switch (mission.Type)
        {
          case IntelligenceMissionType.SabotageConstruction:
            this.cmbMissionType.SelectedIndex = 1;
            this.cmbTargetObject.Enabled = true;
            this.cmbTargetObject.Visible = true;
            this.lblTargetObject.Visible = true;
            this.SetTargetObjectKnownConstructionYards(mission.TargetEmpire);
            this.SetTargetObject((StellarObject) mission.Target);
            break;
          case IntelligenceMissionType.StealGalaxyMap:
            this.cmbMissionType.SelectedIndex = 5;
            break;
          case IntelligenceMissionType.StealOperationsMap:
            this.cmbMissionType.SelectedIndex = 4;
            break;
          case IntelligenceMissionType.StealTechData:
            this.cmbMissionType.SelectedIndex = 6;
            this.cmbTargetObject.Enabled = true;
            this.cmbTargetObject.Visible = true;
            this.lblTargetObject.Visible = true;
            this.SetTargetObjectAdvancedResearch(mission.TargetEmpire);
            this.SetTargetObject((ResearchNode) mission.Target);
            break;
          case IntelligenceMissionType.SabotageColony:
            this.cmbMissionType.SelectedIndex = 7;
            this.cmbTargetObject.Enabled = true;
            this.cmbTargetObject.Visible = true;
            this.lblTargetObject.Visible = true;
            this.SetTargetObjectKnownColonies(mission.TargetEmpire);
            this.SetTargetObject((StellarObject) mission.Target);
            break;
          case IntelligenceMissionType.DeepCover:
            this.cmbMissionType.SelectedIndex = 10;
            if (mission.Outcome == IntelligenceMissionOutcome.SucceedNotDetect)
            {
              flag = false;
              break;
            }
            break;
          case IntelligenceMissionType.InciteRevolution:
            this.cmbMissionType.SelectedIndex = 8;
            break;
          case IntelligenceMissionType.CounterIntelligence:
            this.cmbMissionType.SelectedIndex = 0;
            flag = false;
            break;
          case IntelligenceMissionType.StealTerritoryMap:
            this.cmbMissionType.SelectedIndex = 3;
            break;
          case IntelligenceMissionType.AssassinateCharacter:
            this.cmbMissionType.SelectedIndex = 9;
            this.cmbTargetObject.Enabled = true;
            this.cmbTargetObject.Visible = true;
            this.lblTargetObject.Visible = true;
            this.SetTargetObjectKnownCharacters(mission.TargetEmpire);
            this.SetTargetObject((Character) mission.Target);
            break;
          case IntelligenceMissionType.DestroyBase:
            this.cmbMissionType.SelectedIndex = 2;
            this.cmbTargetObject.Enabled = true;
            this.cmbTargetObject.Visible = true;
            this.lblTargetObject.Visible = true;
            this.SetTargetObjectKnownBases(mission.TargetEmpire);
            this.SetTargetObject((StellarObject) mission.Target);
            break;
        }
        if (mission.TimeLength <= num1)
          this.cmbTimeToComplete.SelectedIndex = 0;
        else if (mission.TimeLength <= num2)
          this.cmbTimeToComplete.SelectedIndex = 1;
        else
          this.cmbTimeToComplete.SelectedIndex = 2;
        if (flag)
        {
          this.lblTimeToComplete.Visible = true;
          this.cmbTimeToComplete.Visible = true;
        }
        else
        {
          this.lblTimeToComplete.Visible = false;
          this.cmbTimeToComplete.Visible = false;
        }
      }
      else
      {
        if (this.cmbTargetEmpire.Items.Count > 0)
          this.cmbTargetEmpire.SelectedIndex = 0;
        this.cmbMissionType.SelectedIndex = 0;
        if (this.cmbTargetObject.Items.Count > 0)
          this.cmbTargetObject.SelectedIndex = 0;
        this.cmbTimeToComplete.SelectedIndex = 0;
        this.cmbTargetObject.Enabled = false;
        this.cmbTargetObject.Visible = false;
      }
      this.cmbTargetEmpire.SelectedIndexChanged += new EventHandler(this.cmbTargetEmpire_SelectedIndexChanged);
      this.cmbMissionType.SelectedIndexChanged += new EventHandler(this.cmbMissionType_SelectedIndexChanged);
      this.cmbTargetObject.SelectedIndexChanged += new EventHandler(this.cmbTargetObject_SelectedIndexChanged);
      this.cmbTimeToComplete.SelectedIndexChanged += new EventHandler(this.cmbTimeToComplete_SelectedIndexChanged);
    }

    private IntelligenceMission GetState()
    {
      IntelligenceMission state = (IntelligenceMission) null;
      object obj = (object) null;
      Empire selectedEmpire = this.cmbTargetEmpire.SelectedEmpire;
      IntelligenceMissionType type = this.ResolveMissionType();
      if (selectedEmpire != null)
      {
        switch (type)
        {
          case IntelligenceMissionType.SabotageConstruction:
            obj = (object) this.ResolveTargetObjectConstructionYard(selectedEmpire);
            break;
          case IntelligenceMissionType.StealTechData:
            obj = (object) this.ResolveTargetObjectResearch();
            break;
          case IntelligenceMissionType.SabotageColony:
            obj = (object) this.ResolveTargetObjectColony(selectedEmpire);
            break;
          case IntelligenceMissionType.AssassinateCharacter:
            obj = (object) this.ResolveTargetObjectCharacter(selectedEmpire);
            break;
          case IntelligenceMissionType.DestroyBase:
            obj = (object) this.ResolveTargetObjectBase(selectedEmpire);
            break;
        }
      }
      long complete = this.ResolveTimeToComplete();
      long currentStarDate = this._Galaxy.CurrentStarDate;
      if (type == IntelligenceMissionType.CounterIntelligence)
        state = new IntelligenceMission(this._PlayerEmpire, this._Character, currentStarDate);
      else if (selectedEmpire != null && type != IntelligenceMissionType.Undefined && complete > 0L)
      {
        switch (type)
        {
          case IntelligenceMissionType.SabotageConstruction:
            if (obj != null)
            {
              if (obj is Habitat)
              {
                state = new IntelligenceMission(this._PlayerEmpire, this._Character, type, currentStarDate, (Habitat) obj);
                break;
              }
              if (obj is BuiltObject)
              {
                state = new IntelligenceMission(this._PlayerEmpire, this._Character, IntelligenceMissionType.SabotageConstruction, currentStarDate, (BuiltObject) obj);
                break;
              }
              break;
            }
            break;
          case IntelligenceMissionType.StealGalaxyMap:
          case IntelligenceMissionType.StealOperationsMap:
          case IntelligenceMissionType.DeepCover:
          case IntelligenceMissionType.InciteRevolution:
          case IntelligenceMissionType.StealTerritoryMap:
            state = new IntelligenceMission(this._PlayerEmpire, this._Character, type, currentStarDate, selectedEmpire);
            break;
          case IntelligenceMissionType.StealTechData:
            if (obj != null)
            {
              state = new IntelligenceMission(this._PlayerEmpire, this._Character, currentStarDate, selectedEmpire, (ResearchNode) obj);
              break;
            }
            break;
          case IntelligenceMissionType.SabotageColony:
            if (obj != null)
            {
              state = new IntelligenceMission(this._PlayerEmpire, this._Character, type, currentStarDate, (Habitat) obj);
              break;
            }
            break;
          case IntelligenceMissionType.CounterIntelligence:
            state = new IntelligenceMission(this._PlayerEmpire, this._Character, currentStarDate);
            break;
          case IntelligenceMissionType.AssassinateCharacter:
            if (obj != null && obj is Character)
            {
              state = new IntelligenceMission(this._PlayerEmpire, this._Character, IntelligenceMissionType.AssassinateCharacter, currentStarDate, (Character) obj);
              break;
            }
            break;
          case IntelligenceMissionType.DestroyBase:
            if (obj != null && obj is BuiltObject)
            {
              state = new IntelligenceMission(this._PlayerEmpire, this._Character, IntelligenceMissionType.DestroyBase, currentStarDate, (BuiltObject) obj);
              break;
            }
            break;
        }
      }
      if (state != null)
        state.TimeLength = complete;
      return state;
    }

    protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);

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

    private void DrawCharacterMissionInfo(Graphics graphics)
    {
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

    private void InitializeComponent()
    {
      this.cmbTargetEmpire = new EmpireDropDown();
      this.cmbMissionType = new ComboBox();
      this.cmbTargetObject = new ComboBox();
      this.cmbTimeToComplete = new ComboBox();
      this.btnCancelMission = new GlassButton();
      this.btnAssignMission = new GlassButton();
      this.lblTargetEmpire = new Label();
      this.lblMissionType = new Label();
      this.lblTargetObject = new Label();
      this.lblTimeToComplete = new Label();
      this.lblMissionDifficulty = new Label();
      this.lblMissionDifficultyLabel = new Label();
      this.lblMissionDifficultyWarning = new Label();
      this.lnkMissionTypes = new LinkLabel();
      this.SuspendLayout();
      this.cmbTargetEmpire.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbTargetEmpire.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbTargetEmpire.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbTargetEmpire.FlatStyle = FlatStyle.Popup;
      this.cmbTargetEmpire.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbTargetEmpire.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbTargetEmpire.FormattingEnabled = true;
      this.cmbTargetEmpire.Location = new Point(17, 92);
      this.cmbTargetEmpire.Name = "cmbTargetEmpire";
      this.cmbTargetEmpire.Size = new Size(121, 22);
      this.cmbTargetEmpire.TabIndex = 51;
      this.cmbTargetEmpire.SelectedIndexChanged += new EventHandler(this.cmbTargetEmpire_SelectedIndexChanged);
      this.cmbMissionType.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbMissionType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbMissionType.FlatStyle = FlatStyle.Popup;
      this.cmbMissionType.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbMissionType.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbMissionType.FormattingEnabled = true;
      this.cmbMissionType.Location = new Point(17, 92);
      this.cmbMissionType.Name = "cmbMissionType";
      this.cmbMissionType.Size = new Size(121, 21);
      this.cmbMissionType.TabIndex = 51;
      this.cmbMissionType.SelectedIndexChanged += new EventHandler(this.cmbMissionType_SelectedIndexChanged);
      this.cmbTargetObject.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbTargetObject.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbTargetObject.FlatStyle = FlatStyle.Popup;
      this.cmbTargetObject.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbTargetObject.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbTargetObject.FormattingEnabled = true;
      this.cmbTargetObject.Location = new Point(17, 92);
      this.cmbTargetObject.Name = "cmbTargetObject";
      this.cmbTargetObject.Size = new Size(121, 21);
      this.cmbTargetObject.TabIndex = 51;
      this.cmbTargetObject.SelectedIndexChanged += new EventHandler(this.cmbTargetObject_SelectedIndexChanged);
      this.cmbTimeToComplete.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbTimeToComplete.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbTimeToComplete.FlatStyle = FlatStyle.Popup;
      this.cmbTimeToComplete.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbTimeToComplete.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbTimeToComplete.FormattingEnabled = true;
      this.cmbTimeToComplete.Location = new Point(17, 92);
      this.cmbTimeToComplete.Name = "cmbTimeToComplete";
      this.cmbTimeToComplete.Size = new Size(121, 21);
      this.cmbTimeToComplete.TabIndex = 51;
      this.cmbTimeToComplete.SelectedIndexChanged += new EventHandler(this.cmbTimeToComplete_SelectedIndexChanged);
      this.btnCancelMission.BackColor = Color.FromArgb(0, 0, 0);
      this.btnCancelMission.ClipBackground = false;
      this.btnCancelMission.Font = new Font("Verdana", 8f, FontStyle.Bold);
      this.btnCancelMission.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnCancelMission.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnCancelMission.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnCancelMission.IntensifyColors = false;
      this.btnCancelMission.Location = new Point(0, 0);
      this.btnCancelMission.Name = "btnCancelMission";
      this.btnCancelMission.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnCancelMission.ShineColor = Color.FromArgb(96, 96, 104);
      this.btnCancelMission.Size = new Size(75, 23);
      this.btnCancelMission.TabIndex = 0;
      this.btnCancelMission.Text = TextResolver.GetText("Cancel Mission");
      this.btnCancelMission.TextColor = Color.FromArgb(120, 120, 120);
      this.btnCancelMission.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnCancelMission.Click += new EventHandler(this.btnCancelMission_Click);
      this.btnAssignMission.BackColor = Color.FromArgb(0, 0, 0);
      this.btnAssignMission.ClipBackground = false;
      this.btnAssignMission.Font = new Font("Verdana", 8f, FontStyle.Bold);
      this.btnAssignMission.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnAssignMission.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnAssignMission.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnAssignMission.IntensifyColors = false;
      this.btnAssignMission.Location = new Point(0, 0);
      this.btnAssignMission.Name = "btnAssignMission";
      this.btnAssignMission.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnAssignMission.ShineColor = Color.FromArgb(96, 96, 104);
      this.btnAssignMission.Size = new Size(75, 23);
      this.btnAssignMission.TabIndex = 0;
      this.btnAssignMission.Text = TextResolver.GetText("Assign Mission");
      this.btnAssignMission.TextColor = Color.FromArgb(120, 120, 120);
      this.btnAssignMission.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnAssignMission.Click += new EventHandler(this.btnAssignMission_Click);
      this.lblTargetEmpire.AutoSize = true;
      this.lblTargetEmpire.BackColor = Color.Transparent;
      this.lblTargetEmpire.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTargetEmpire.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTargetEmpire.Location = new Point(0, 0);
      this.lblTargetEmpire.Name = "lblTargetEmpire";
      this.lblTargetEmpire.Size = new Size(100, 13);
      this.lblTargetEmpire.TabIndex = 0;
      this.lblTargetEmpire.Text = TextResolver.GetText("Target Empire");
      this.lblMissionType.AutoSize = true;
      this.lblMissionType.BackColor = Color.Transparent;
      this.lblMissionType.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblMissionType.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMissionType.Location = new Point(0, 0);
      this.lblMissionType.Name = "lblMissionType";
      this.lblMissionType.Size = new Size(91, 13);
      this.lblMissionType.TabIndex = 0;
      this.lblMissionType.Text = TextResolver.GetText("Mission Type");
      this.lblTargetObject.AutoSize = true;
      this.lblTargetObject.BackColor = Color.Transparent;
      this.lblTargetObject.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTargetObject.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTargetObject.Location = new Point(0, 0);
      this.lblTargetObject.Name = "lblTargetObject";
      this.lblTargetObject.Size = new Size(50, 13);
      this.lblTargetObject.TabIndex = 0;
      this.lblTargetObject.Text = TextResolver.GetText("Target");
      this.lblTimeToComplete.AutoSize = true;
      this.lblTimeToComplete.BackColor = Color.Transparent;
      this.lblTimeToComplete.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTimeToComplete.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTimeToComplete.Location = new Point(0, 0);
      this.lblTimeToComplete.Name = "lblTimeToComplete";
      this.lblTimeToComplete.Size = new Size(121, 13);
      this.lblTimeToComplete.TabIndex = 0;
      this.lblTimeToComplete.Text = TextResolver.GetText("Time to Complete");
      this.lblMissionDifficulty.AutoSize = true;
      this.lblMissionDifficulty.BackColor = Color.Transparent;
      this.lblMissionDifficulty.Font = new Font("Verdana", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblMissionDifficulty.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMissionDifficulty.Location = new Point(0, 0);
      this.lblMissionDifficulty.Name = "lblMissionDifficulty";
      this.lblMissionDifficulty.Size = new Size(0, 23);
      this.lblMissionDifficulty.TabIndex = 0;
      this.lblMissionDifficultyLabel.AutoSize = true;
      this.lblMissionDifficultyLabel.BackColor = Color.Transparent;
      this.lblMissionDifficultyLabel.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblMissionDifficultyLabel.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMissionDifficultyLabel.Location = new Point(0, 0);
      this.lblMissionDifficultyLabel.Name = "lblMissionDifficultyLabel";
      this.lblMissionDifficultyLabel.Size = new Size(78, 26);
      this.lblMissionDifficultyLabel.TabIndex = 0;
      this.lblMissionDifficultyLabel.Text = TextResolver.GetText("Success Probability");
      this.lblMissionDifficultyLabel.TextAlign = ContentAlignment.MiddleRight;
      this.lblMissionDifficultyWarning.AutoSize = true;
      this.lblMissionDifficultyWarning.BackColor = Color.Transparent;
      this.lblMissionDifficultyWarning.Font = new Font("Verdana", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblMissionDifficultyWarning.ForeColor = Color.FromArgb(160, 160, (int) byte.MaxValue);
      this.lblMissionDifficultyWarning.Location = new Point(0, 0);
      this.lblMissionDifficultyWarning.Name = "lblMissionDifficultyWarning";
      this.lblMissionDifficultyWarning.Size = new Size(0, 16);
      this.lblMissionDifficultyWarning.TabIndex = 0;
      this.lnkMissionTypes.ActiveLinkColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
      this.lnkMissionTypes.AutoSize = true;
      this.lnkMissionTypes.BackColor = Color.Transparent;
      this.lnkMissionTypes.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lnkMissionTypes.LinkBehavior = LinkBehavior.HoverUnderline;
      this.lnkMissionTypes.LinkColor = Color.FromArgb((int) byte.MaxValue, 192, 0);
      this.lnkMissionTypes.Location = new Point(-16, 53);
      this.lnkMissionTypes.Name = "lnkMissionTypes";
      this.lnkMissionTypes.Size = new Size(151, 13);
      this.lnkMissionTypes.TabIndex = 79;
      this.lnkMissionTypes.TabStop = true;
      this.lnkMissionTypes.Text = TextResolver.GetText("About this mission type...");
      this.lnkMissionTypes.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkMissionTypes_LinkClicked);
      this.Controls.Add((Control) this.cmbTargetEmpire);
      this.Controls.Add((Control) this.cmbMissionType);
      this.Controls.Add((Control) this.cmbTargetObject);
      this.Controls.Add((Control) this.cmbTimeToComplete);
      this.Controls.Add((Control) this.lblTargetEmpire);
      this.Controls.Add((Control) this.lblMissionType);
      this.Controls.Add((Control) this.lblTargetObject);
      this.Controls.Add((Control) this.lblTimeToComplete);
      this.Controls.Add((Control) this.btnAssignMission);
      this.Controls.Add((Control) this.btnCancelMission);
      this.Controls.Add((Control) this.lblMissionDifficulty);
      this.Controls.Add((Control) this.lblMissionDifficultyLabel);
      this.Controls.Add((Control) this.lblMissionDifficultyWarning);
      this.Controls.Add((Control) this.lnkMissionTypes);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void btnCancelMission_Click(object sender, EventArgs e)
    {
      if (this._PlayerEmpire.ControlAgentAssignment == AutomationLevel.FullyAutomated)
      {
        MessageBoxEx messageBox = MessageBoxExManager.GetMessageBox(TextResolver.GetText("Agent Assignment"));
        if (messageBox != null && messageBox.Show().ToLower(CultureInfo.InvariantCulture) == "off")
          this._PlayerEmpire.ControlAgentAssignment = AutomationLevel.Manual;
      }
      this._PlayerEmpire.CancelIntelligenceMission(this._Character.Mission);
      this._Character.Mission = (IntelligenceMission) null;
      this.SetControlsAndMission();
      if (this.MissionCancelled == null)
        return;
      this.MissionCancelled((object) this, new EventArgs());
    }

    private void btnAssignMission_Click(object sender, EventArgs e)
    {
      if (this._PlayerEmpire.ControlAgentAssignment == AutomationLevel.FullyAutomated)
      {
        MessageBoxEx messageBox = MessageBoxExManager.GetMessageBox(TextResolver.GetText("Agent Assignment"));
        if (messageBox != null && messageBox.Show().ToLower(CultureInfo.InvariantCulture) == "off")
          this._PlayerEmpire.ControlAgentAssignment = AutomationLevel.Manual;
      }
      IntelligenceMission state = this.GetState();
      if (state == null)
        return;
      this._Character.Mission = state;
      this.SetControlsAndMission();
      if (this.MissionAssigned == null)
        return;
      this.MissionAssigned((object) this, new EventArgs());
    }

    private string GetMissionDifficultyDescription(IntelligenceMission mission, Character agent)
    {
      string difficultyDescription = "(" + TextResolver.GetText("Unknown") + ")";
      if (mission != null)
        difficultyDescription = mission.Type != IntelligenceMissionType.CounterIntelligence ? (mission.Type != IntelligenceMissionType.DeepCover || mission.Outcome != IntelligenceMissionOutcome.SucceedNotDetect ? agent.Empire.CalculateIntelligenceMissionSuccessChance(mission, agent).ToString("#0%") : string.Empty) : string.Empty;
      return difficultyDescription;
    }

    private string GetMissionDifficultyWarning(IntelligenceMission mission, Character agent)
    {
      string difficultyWarning = string.Empty;
      if (mission != null)
      {
        if (mission.Type == IntelligenceMissionType.CounterIntelligence)
          difficultyWarning = string.Empty;
        else if (mission.Type == IntelligenceMissionType.DeepCover && mission.Outcome == IntelligenceMissionOutcome.SucceedNotDetect)
        {
          difficultyWarning = string.Empty;
        }
        else
        {
          double missionSuccessChance = agent.Empire.CalculateIntelligenceMissionSuccessChance(mission, agent);
          if (missionSuccessChance < 0.7)
          {
            difficultyWarning = TextResolver.GetText("WARNING: This mission is highly risky - your agent may be captured");
            this.lblMissionDifficultyWarning.ForeColor = Color.Red;
          }
          else if (missionSuccessChance < 0.85)
          {
            difficultyWarning = TextResolver.GetText("This mission is somewhat risky, maybe you should reconsider");
            this.lblMissionDifficultyWarning.ForeColor = Color.Yellow;
          }
          else
          {
            difficultyWarning = string.Empty;
            this.lblMissionDifficultyWarning.ForeColor = Color.FromArgb(170, 170, 170);
          }
        }
      }
      return difficultyWarning;
    }

    private void cmbTargetObject_SelectedIndexChanged(object sender, EventArgs e) => this.SetControlsAndMission();

    private void cmbTimeToComplete_SelectedIndexChanged(object sender, EventArgs e) => this.SetControlsAndMission();

    private void cmbMissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
      Empire selectedEmpire = this.cmbTargetEmpire.SelectedEmpire;
      this.SetControlsAndMission();
      if (this._Character != null && this._Character.Mission != null)
      {
        int type = (int) this._Character.Mission.Type;
      }
      this.cmbTargetObject.Enabled = false;
      this.cmbTargetObject.Visible = false;
      this.lblTargetObject.Visible = false;
      if (this.ResolveMissionType() == IntelligenceMissionType.CounterIntelligence)
      {
        this.cmbTimeToComplete.SelectedIndexChanged -= new EventHandler(this.cmbTimeToComplete_SelectedIndexChanged);
        this.cmbTimeToComplete.Items.Clear();
        this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("Until cancelled"));
        this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("One month"));
        this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("Three months"));
        this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("One year"));
        this.cmbTimeToComplete.SelectedIndex = 0;
        this.cmbTimeToComplete.SelectedIndexChanged += new EventHandler(this.cmbTimeToComplete_SelectedIndexChanged);
      }
      else
      {
        this.cmbTimeToComplete.SelectedIndexChanged -= new EventHandler(this.cmbTimeToComplete_SelectedIndexChanged);
        this.cmbTimeToComplete.Items.Clear();
        this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("One month"));
        this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("Three months"));
        this.cmbTimeToComplete.Items.Add((object) TextResolver.GetText("One year"));
        this.cmbTimeToComplete.SelectedIndexChanged += new EventHandler(this.cmbTimeToComplete_SelectedIndexChanged);
      }
      if (selectedEmpire == null)
        return;
      switch (this.ResolveMissionType())
      {
        case IntelligenceMissionType.SabotageConstruction:
          this.SetTargetObjectKnownConstructionYards(selectedEmpire);
          this.cmbTargetObject.Enabled = true;
          this.cmbTargetObject.Visible = true;
          this.lblTargetObject.Visible = true;
          break;
        case IntelligenceMissionType.StealTechData:
          this.SetTargetObjectAdvancedResearch(selectedEmpire);
          this.cmbTargetObject.Enabled = true;
          this.cmbTargetObject.Visible = true;
          this.lblTargetObject.Visible = true;
          break;
        case IntelligenceMissionType.SabotageColony:
          this.SetTargetObjectKnownColonies(selectedEmpire);
          this.cmbTargetObject.Enabled = true;
          this.cmbTargetObject.Visible = true;
          this.lblTargetObject.Visible = true;
          break;
        case IntelligenceMissionType.AssassinateCharacter:
          this.SetTargetObjectKnownCharacters(selectedEmpire);
          this.cmbTargetObject.Enabled = true;
          this.cmbTargetObject.Visible = true;
          this.lblTargetObject.Visible = true;
          break;
        case IntelligenceMissionType.DestroyBase:
          this.SetTargetObjectKnownBases(selectedEmpire);
          this.cmbTargetObject.Enabled = true;
          this.cmbTargetObject.Visible = true;
          this.lblTargetObject.Visible = true;
          break;
      }
    }

    private void cmbTargetEmpire_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.cmbMissionType.SelectedIndex = 0;
      this.cmbMissionType.SelectedIndex = 1;
    }

    private void lnkMissionTypes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (this.MissionTypeHelp == null)
        return;
      this.MissionTypeHelp((object) this, new EventArgs());
    }
  }
}
