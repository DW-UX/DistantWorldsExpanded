// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.SpecialRuin
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
  public class SpecialRuin : UserControl
  {
    private Ruin _Ruin;
    private Bitmap[] _RuinImages;
    private RaceList _Races;
    private Bitmap[] _RaceImages;
    protected IFontCache _FontCache;
    private float _FontSize = 15.33f;
    private bool _FontIsBold;
    private IContainer components;
    private PictureBox picImage;
    private TextBox txtName;
    private Label lblName;
    private Label lblTitle;
    private GroupBox grpBonuses;
    private NumericUpDown numApprovalBonus;
    private Label lblOriginsBonus;
    private Label lblNewPopulationBonus;
    private Label lblComponentBonus;
    private Label lblGovernmentBonus;
    private RaceDropDown cmbSleepingRace;
    private GovernmentStyleDropDown cmbGovernmentStyle;
    private ComboBox cmbComponent;
    private HScrollBar scrImage;

    public virtual void SetFontCache(IFontCache fontCache)
    {
      this._FontCache = fontCache;
      if ((double) this._FontSize <= 0.0)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
    }

    public void SetFont(float pointSize) => this.SetFont(pointSize, false);

    public void SetFont(float pointSize, bool isBold)
    {
      this._FontSize = pointSize;
      this._FontIsBold = isBold;
      if (this._FontCache == null)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
    }

    public SpecialRuin() => this.InitializeComponent();

    public void ClearData()
    {
      this._Ruin = (Ruin) null;
      this._Races = (RaceList) null;
    }

    public void BindData(Ruin ruin, Bitmap[] ruinImages, RaceList races, Bitmap[] raceImages)
    {
      this._Ruin = ruin;
      this._RuinImages = ruinImages;
      this._Races = races;
      this._RaceImages = raceImages;
      this.picImage.SizeMode = PictureBoxSizeMode.Zoom;
      GovernmentAttributesList byAvailability = Galaxy.GovernmentsStatic.GetByAvailability(2);
      byAvailability.AddRange((IEnumerable<GovernmentAttributes>) Galaxy.GovernmentsStatic.GetByAvailability(3));
      this.cmbGovernmentStyle.Ignite(byAvailability);
      this.cmbSleepingRace.BindData(this.Font, races, raceImages, false);
      ResearchNodeDefinitionList nodeDefinitionList = this.ResolveValidExoticTechs();
      this.cmbComponent.Items.Clear();
      for (int index = 0; index < nodeDefinitionList.Count; ++index)
        this.cmbComponent.Items.Add((object) nodeDefinitionList[index].Name);
      this.BindValues();
      this.LocalizeControlText();
      this.SetControlState();
    }

    private ResearchNodeDefinitionList ResolveValidExoticTechs()
    {
      ResearchNodeDefinitionList nodeDefinitionList = new ResearchNodeDefinitionList();
      nodeDefinitionList.AddRange((IEnumerable<ResearchNodeDefinition>) Galaxy.ResearchNodeDefinitionsStatic.FindAllNodesBySpecialFunctionCode(3));
      nodeDefinitionList.AddRange((IEnumerable<ResearchNodeDefinition>) Galaxy.ResearchNodeDefinitionsStatic.FindAllNodesBySpecialFunctionCode(2));
      nodeDefinitionList.AddRange((IEnumerable<ResearchNodeDefinition>) Galaxy.ResearchNodeDefinitionsStatic.FindAllNodesBySpecialFunctionCode(5));
      return nodeDefinitionList;
    }

    private ResearchNode ResolveResearchProjectForComponent(DistantWorlds.Types.Component component)
    {
      ResearchNode researchNode = (ResearchNode) null;
      if (component != null)
      {
        ResearchNodeDefinition researchNodeDefinition = Galaxy.ResearchNodeDefinitionsStatic.ResolveResearchNodeForComponent(component);
        if (researchNodeDefinition != null)
          researchNode = new ResearchNode(researchNodeDefinition.ResearchNodeId);
      }
      return researchNode;
    }

    private ResearchNode ResolveResearchProject()
    {
      ResearchNodeDefinitionList nodeDefinitionList = this.ResolveValidExoticTechs();
      int selectedIndex = this.cmbComponent.SelectedIndex;
      return selectedIndex >= 0 && selectedIndex < nodeDefinitionList.Count ? new ResearchNode(nodeDefinitionList[selectedIndex].ResearchNodeId) : (ResearchNode) null;
    }

    private void BindSpecialComponent(ResearchNode researchProject)
    {
      ResearchNodeDefinitionList nodeDefinitionList = this.ResolveValidExoticTechs();
      if (researchProject == null)
        return;
      for (int index = 0; index < nodeDefinitionList.Count; ++index)
      {
        if (nodeDefinitionList[index].Name == researchProject.Name)
          this.cmbComponent.SelectedIndex = index;
      }
    }

    private void BindValues()
    {
      if (this._Ruin != null)
      {
        string text = TextResolver.GetText("Ruins");
        switch (this._Ruin.Type)
        {
          case RuinType.Government:
            text = TextResolver.GetText("Ruins: Government");
            break;
          case RuinType.Component:
            text = TextResolver.GetText("Ruins: Super Weapon");
            break;
          case RuinType.NewPopulation:
            text = TextResolver.GetText("Ruins: Sleepers");
            break;
          case RuinType.Refugees:
            text = TextResolver.GetText("Ruins: Refugees");
            break;
          case RuinType.Origins:
            text = TextResolver.GetText("Ruins: Origins");
            break;
          case RuinType.LostBuiltObject:
            text = TextResolver.GetText("Ruins: Lost Ship");
            break;
          case RuinType.LostColony:
            text = TextResolver.GetText("Ruins: Lost Colony");
            break;
          case RuinType.UnlockResearchProject:
            text = TextResolver.GetText("Ruins: Unlock Research Project");
            break;
        }
        this.lblTitle.Text = text;
        this.picImage.Image = (Image) this._RuinImages[this._Ruin.PictureRef];
        this.txtName.Text = this._Ruin.Name;
        this.numApprovalBonus.Value = (Decimal) this._Ruin.OriginsApprovalRatingBonus;
        this.cmbSleepingRace.SetSelectedRace(this._Ruin.HabitatNewRace);
        this.cmbGovernmentStyle.SetSelectedGovernmentStyle(this._Ruin.SpecialGovernmentId);
        if (this._Ruin.ResearchProjectId <= 0)
          return;
        ResearchNode researchProject = new ResearchNode(this._Ruin.ResearchProjectId);
        if (researchProject == null)
          return;
        this.BindSpecialComponent(researchProject);
      }
      else
      {
        this.picImage.Image = (Image) null;
        this.lblTitle.Text = TextResolver.GetText("Ruins");
        this.txtName.Text = string.Empty;
        this.numApprovalBonus.Value = 0M;
        this.cmbComponent.SelectedIndex = 0;
        this.cmbGovernmentStyle.SelectedIndex = 0;
        this.cmbSleepingRace.SelectedIndex = 0;
      }
    }

    private void LocalizeControlText()
    {
      this.lblComponentBonus.Text = TextResolver.GetText("Component");
      this.lblGovernmentBonus.Text = TextResolver.GetText("Government");
      this.lblName.Text = TextResolver.GetText("Name");
      this.lblNewPopulationBonus.Text = TextResolver.GetText("Sleeping Race");
      this.lblOriginsBonus.Text = TextResolver.GetText("Approval");
      this.lblTitle.Text = TextResolver.GetText("Ruins");
      this.grpBonuses.Text = TextResolver.GetText("Bonuses");
    }

    public Ruin GetRuin()
    {
      if (this._Ruin != null)
      {
        this._Ruin.Name = this.txtName.Text;
        switch (this._Ruin.Type)
        {
          case RuinType.Government:
            this._Ruin.SpecialGovernmentId = this.cmbGovernmentStyle.SelectedGovernmentId;
            break;
          case RuinType.Component:
          case RuinType.UnlockResearchProject:
            ResearchNode researchNode = this.ResolveResearchProject();
            if (researchNode != null)
            {
              this._Ruin.ResearchProjectId = researchNode.ResearchNodeId;
              break;
            }
            break;
          case RuinType.NewPopulation:
            this._Ruin.HabitatNewRace = this.cmbSleepingRace.SelectedRace;
            break;
          case RuinType.Origins:
            this._Ruin.OriginsApprovalRatingBonus = (int) this.numApprovalBonus.Value;
            break;
        }
      }
      return this._Ruin;
    }

    private void HideAllBonusControls()
    {
      this.lblComponentBonus.Visible = false;
      this.cmbComponent.Visible = false;
      this.lblGovernmentBonus.Visible = false;
      this.cmbGovernmentStyle.Visible = false;
      this.lblNewPopulationBonus.Visible = false;
      this.cmbSleepingRace.Visible = false;
      this.lblOriginsBonus.Visible = false;
      this.numApprovalBonus.Visible = false;
    }

    private void SetControlState()
    {
      this.HideAllBonusControls();
      switch (this._Ruin.Type)
      {
        case RuinType.Government:
          this.lblGovernmentBonus.Location = new Point(5, 18);
          this.cmbGovernmentStyle.Location = new Point(96, 14);
          this.lblGovernmentBonus.Visible = true;
          this.cmbGovernmentStyle.Visible = true;
          break;
        case RuinType.Component:
        case RuinType.UnlockResearchProject:
          this.lblComponentBonus.Location = new Point(5, 18);
          this.cmbComponent.Location = new Point(96, 14);
          this.lblComponentBonus.Visible = true;
          this.cmbComponent.Visible = true;
          break;
        case RuinType.NewPopulation:
          this.lblNewPopulationBonus.Location = new Point(5, 18);
          this.cmbSleepingRace.Location = new Point(96, 14);
          this.lblNewPopulationBonus.Visible = true;
          this.cmbSleepingRace.Visible = true;
          break;
        case RuinType.Origins:
          this.lblOriginsBonus.Location = new Point(5, 18);
          this.numApprovalBonus.Location = new Point(96, 14);
          this.lblOriginsBonus.Visible = true;
          this.numApprovalBonus.Visible = true;
          break;
      }
    }

    public void ImageScroll(int newValue)
    {
      if (this._Ruin == null)
        return;
      int num = newValue;
      if (num >= this._RuinImages.Length)
        num = this._RuinImages.Length - 1;
      else if (num < 0)
        num = 0;
      this._Ruin.PictureRef = num;
      this.picImage.Image = (Image) this._RuinImages[this._Ruin.PictureRef];
      this.picImage.Refresh();
    }

    private void scrImage_Scroll(object sender, ScrollEventArgs e) => this.ImageScroll(e.NewValue);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.picImage = new PictureBox();
      this.txtName = new TextBox();
      this.lblName = new Label();
      this.lblTitle = new Label();
      this.grpBonuses = new GroupBox();
      this.cmbComponent = new ComboBox();
      this.cmbGovernmentStyle = new GovernmentStyleDropDown();
      this.cmbSleepingRace = new RaceDropDown();
      this.numApprovalBonus = new NumericUpDown();
      this.lblOriginsBonus = new Label();
      this.lblNewPopulationBonus = new Label();
      this.lblComponentBonus = new Label();
      this.lblGovernmentBonus = new Label();
      this.scrImage = new HScrollBar();
      ((ISupportInitialize) this.picImage).BeginInit();
      this.grpBonuses.SuspendLayout();
      this.numApprovalBonus.BeginInit();
      this.SuspendLayout();
      this.picImage.BackColor = Color.Transparent;
      this.picImage.Location = new Point(205, 0);
      this.picImage.Name = "picImage";
      this.picImage.Size = new Size(50, 50);
      this.picImage.TabIndex = 8;
      this.picImage.TabStop = false;
      this.txtName.BackColor = Color.FromArgb(48, 48, 64);
      this.txtName.BorderStyle = BorderStyle.FixedSingle;
      this.txtName.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtName.Location = new Point(47, 23);
      this.txtName.Name = "txtName";
      this.txtName.Size = new Size(150, 20);
      this.txtName.TabIndex = 7;
      this.lblName.AutoSize = true;
      this.lblName.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblName.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblName.Location = new Point(5, 25);
      this.lblName.Name = "lblName";
      this.lblName.Size = new Size(40, 13);
      this.lblName.TabIndex = 6;
      this.lblName.Text = "Name";
      this.lblTitle.AutoSize = true;
      this.lblTitle.Font = new Font("Verdana", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTitle.ForeColor = Color.White;
      this.lblTitle.Location = new Point(0, 0);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(53, 18);
      this.lblTitle.TabIndex = 5;
      this.lblTitle.Text = "Ruins";
      this.grpBonuses.BackColor = Color.Transparent;
      this.grpBonuses.Controls.Add((Control) this.cmbComponent);
      this.grpBonuses.Controls.Add((Control) this.cmbGovernmentStyle);
      this.grpBonuses.Controls.Add((Control) this.cmbSleepingRace);
      this.grpBonuses.Controls.Add((Control) this.numApprovalBonus);
      this.grpBonuses.Controls.Add((Control) this.lblOriginsBonus);
      this.grpBonuses.Controls.Add((Control) this.lblNewPopulationBonus);
      this.grpBonuses.Controls.Add((Control) this.lblComponentBonus);
      this.grpBonuses.Controls.Add((Control) this.lblGovernmentBonus);
      this.grpBonuses.Font = new Font("Verdana", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.grpBonuses.ForeColor = Color.FromArgb(170, 170, 170);
      this.grpBonuses.Location = new Point(3, 50);
      this.grpBonuses.Name = "grpBonuses";
      this.grpBonuses.Size = new Size(254, 116);
      this.grpBonuses.TabIndex = 9;
      this.grpBonuses.TabStop = false;
      this.grpBonuses.Text = "Bonuses";
      this.cmbComponent.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbComponent.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbComponent.FlatStyle = FlatStyle.Popup;
      this.cmbComponent.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbComponent.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbComponent.FormattingEnabled = true;
      this.cmbComponent.Items.AddRange(new object[3]
      {
        (object) "Death Ray",
        (object) "Devastator Pulse",
        (object) "Warp Bubble Generator"
      });
      this.cmbComponent.Location = new Point(96, 38);
      this.cmbComponent.Name = "cmbComponent";
      this.cmbComponent.Size = new Size(150, 21);
      this.cmbComponent.TabIndex = 12;
      this.cmbGovernmentStyle.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbGovernmentStyle.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbGovernmentStyle.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbGovernmentStyle.FlatStyle = FlatStyle.Popup;
      this.cmbGovernmentStyle.Font = new Font("Verdana", 8.25f);
      this.cmbGovernmentStyle.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbGovernmentStyle.FormattingEnabled = true;
      this.cmbGovernmentStyle.Location = new Point(96, 14);
      this.cmbGovernmentStyle.Name = "cmbGovernmentStyle";
      this.cmbGovernmentStyle.Size = new Size(150, 22);
      this.cmbGovernmentStyle.TabIndex = 11;
      this.cmbSleepingRace.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbSleepingRace.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbSleepingRace.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSleepingRace.FlatStyle = FlatStyle.Popup;
      this.cmbSleepingRace.Font = new Font("Verdana", 9f);
      this.cmbSleepingRace.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbSleepingRace.FormattingEnabled = true;
      this.cmbSleepingRace.Location = new Point(96, 63);
      this.cmbSleepingRace.Name = "cmbSleepingRace";
      this.cmbSleepingRace.Size = new Size(150, 23);
      this.cmbSleepingRace.TabIndex = 10;
      this.numApprovalBonus.BackColor = Color.FromArgb(48, 48, 64);
      this.numApprovalBonus.BorderStyle = BorderStyle.FixedSingle;
      this.numApprovalBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.numApprovalBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.numApprovalBonus.Location = new Point(96, 90);
      this.numApprovalBonus.Maximum = new Decimal(new int[4]
      {
        20,
        0,
        0,
        0
      });
      this.numApprovalBonus.Name = "numApprovalBonus";
      this.numApprovalBonus.Size = new Size(60, 21);
      this.numApprovalBonus.TabIndex = 9;
      this.lblOriginsBonus.AutoSize = true;
      this.lblOriginsBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblOriginsBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblOriginsBonus.Location = new Point(5, 92);
      this.lblOriginsBonus.Name = "lblOriginsBonus";
      this.lblOriginsBonus.Size = new Size(58, 13);
      this.lblOriginsBonus.TabIndex = 8;
      this.lblOriginsBonus.Text = "Approval";
      this.lblNewPopulationBonus.AutoSize = true;
      this.lblNewPopulationBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblNewPopulationBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblNewPopulationBonus.Location = new Point(5, 67);
      this.lblNewPopulationBonus.Name = "lblNewPopulationBonus";
      this.lblNewPopulationBonus.Size = new Size(88, 13);
      this.lblNewPopulationBonus.TabIndex = 4;
      this.lblNewPopulationBonus.Text = "Sleeping Race";
      this.lblComponentBonus.AutoSize = true;
      this.lblComponentBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblComponentBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblComponentBonus.Location = new Point(5, 42);
      this.lblComponentBonus.Name = "lblComponentBonus";
      this.lblComponentBonus.Size = new Size(60, 13);
      this.lblComponentBonus.TabIndex = 3;
      this.lblComponentBonus.Text = "Research";
      this.lblGovernmentBonus.AutoSize = true;
      this.lblGovernmentBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblGovernmentBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblGovernmentBonus.Location = new Point(5, 17);
      this.lblGovernmentBonus.Name = "lblGovernmentBonus";
      this.lblGovernmentBonus.Size = new Size(78, 13);
      this.lblGovernmentBonus.TabIndex = 2;
      this.lblGovernmentBonus.Text = "Government";
      this.scrImage.Location = new Point(205, 38);
      this.scrImage.Name = "scrImage";
      this.scrImage.Size = new Size(50, 12);
      this.scrImage.TabIndex = 10;
      this.scrImage.Scroll += new ScrollEventHandler(this.scrImage_Scroll);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.scrImage);
      this.Controls.Add((Control) this.grpBonuses);
      this.Controls.Add((Control) this.picImage);
      this.Controls.Add((Control) this.txtName);
      this.Controls.Add((Control) this.lblName);
      this.Controls.Add((Control) this.lblTitle);
      this.Name = nameof (SpecialRuin);
      this.Size = new Size(260, 170);
      ((ISupportInitialize) this.picImage).EndInit();
      this.grpBonuses.ResumeLayout(false);
      this.grpBonuses.PerformLayout();
      this.numApprovalBonus.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
