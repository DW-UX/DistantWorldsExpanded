// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GameEventPanel
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
  public class GameEventPanel : UserControl
  {
    private IContainer components;
    private EventTriggerTypeDropDown cmbEventTriggerType;
    private SmoothLabel lblEventTriggerType;
    private PlanetaryFacilityDefinitionDropDown cmbBuildPlanetaryFacilityDefinition;
    private BuiltObjectSubRoleDropDown cmbBuildBuiltObjectSubRole;
    private CheckBox chkTriggeredByRuin;
    private SmoothLabel lblBuiltObjectSubRole;
    private SmoothLabel lblBuildPlanetaryFacilityDefinition;
    private TextBox txtTitle;
    private TextBox txtDescription;
    private SmoothLabel lblTitle;
    private SmoothLabel lblDescription;
    private CheckBox chkCanOnlyBeTriggeredByPlayer;
    private EventActionListView ctlEventActions;
    private SmoothLabel lblTriggerObjectName;
    private MultipleEventActionTypeDropDown cmbMultipleEventActionType;
    private GlassButton btnAddNewActionBlank;
    private GlassButton btnAddNewActionTarget;
    private SmoothLabel lblEventActions;
    private GlassButton btnDelete;
    private GlassButton btnClose;
    private EmpireDropDown cmbEmpire;
    private EmpireDropDown cmbEmpireOther;
    private DiplomaticRelationTypeActualDropDown cmbDiplomaticRelationType;
    private ResearchNodeDefinitionDropDown cmbResearchProject;
    private SmoothLabel lblEmpire;
    private SmoothLabel lblEmpireOther;
    private SmoothLabel lblDiplomaticRelationType;
    private SmoothLabel lblResearchProject;
    private CharacterDropDown cmbCharacter;
    private SmoothLabel lblCharacter;
    private GameEvent _GameEvent;
    private Galaxy _Galaxy;
    private Font _NormalFont;
    private Font _TitleFont;
    public CharacterImageCache _CharacterImageCache;
    private bool _DisableSelectedIndexChanged;
    private bool _Binding;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.chkTriggeredByRuin = new CheckBox();
      this.txtTitle = new TextBox();
      this.txtDescription = new TextBox();
      this.chkCanOnlyBeTriggeredByPlayer = new CheckBox();
      this.btnClose = new GlassButton();
      this.btnDelete = new GlassButton();
      this.lblEventActions = new SmoothLabel();
      this.btnAddNewActionTarget = new GlassButton();
      this.btnAddNewActionBlank = new GlassButton();
      this.cmbMultipleEventActionType = new MultipleEventActionTypeDropDown();
      this.lblTriggerObjectName = new SmoothLabel();
      this.ctlEventActions = new EventActionListView();
      this.lblDescription = new SmoothLabel();
      this.lblTitle = new SmoothLabel();
      this.lblBuildPlanetaryFacilityDefinition = new SmoothLabel();
      this.lblBuiltObjectSubRole = new SmoothLabel();
      this.cmbBuildBuiltObjectSubRole = new BuiltObjectSubRoleDropDown();
      this.cmbBuildPlanetaryFacilityDefinition = new PlanetaryFacilityDefinitionDropDown();
      this.lblEventTriggerType = new SmoothLabel();
      this.cmbEventTriggerType = new EventTriggerTypeDropDown();
      this.cmbEmpire = new EmpireDropDown();
      this.cmbEmpireOther = new EmpireDropDown();
      this.cmbDiplomaticRelationType = new DiplomaticRelationTypeActualDropDown();
      this.cmbResearchProject = new ResearchNodeDefinitionDropDown();
      this.lblEmpire = new SmoothLabel();
      this.lblEmpireOther = new SmoothLabel();
      this.lblDiplomaticRelationType = new SmoothLabel();
      this.lblResearchProject = new SmoothLabel();
      this.cmbCharacter = new CharacterDropDown();
      this.lblCharacter = new SmoothLabel();
      this.SuspendLayout();
      this.chkTriggeredByRuin.AutoSize = true;
      this.chkTriggeredByRuin.ForeColor = Color.FromArgb(170, 170, 170);
      this.chkTriggeredByRuin.Location = new Point(219, 163);
      this.chkTriggeredByRuin.Name = "chkTriggeredByRuin";
      this.chkTriggeredByRuin.Size = new Size(111, 17);
      this.chkTriggeredByRuin.TabIndex = 4;
      this.chkTriggeredByRuin.Text = "Triggered By Ruin";
      this.chkTriggeredByRuin.UseVisualStyleBackColor = true;
      this.chkTriggeredByRuin.CheckedChanged += new EventHandler(this.chkTriggeredByRuin_CheckedChanged);
      this.txtTitle.BackColor = Color.FromArgb(48, 48, 64);
      this.txtTitle.BorderStyle = BorderStyle.FixedSingle;
      this.txtTitle.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtTitle.Location = new Point(80, 64);
      this.txtTitle.Name = "txtTitle";
      this.txtTitle.Size = new Size(250, 20);
      this.txtTitle.TabIndex = 7;
      this.txtDescription.BackColor = Color.FromArgb(48, 48, 64);
      this.txtDescription.BorderStyle = BorderStyle.FixedSingle;
      this.txtDescription.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtDescription.Location = new Point(80, 90);
      this.txtDescription.Multiline = true;
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.Size = new Size(250, 60);
      this.txtDescription.TabIndex = 8;
      this.chkCanOnlyBeTriggeredByPlayer.AutoSize = true;
      this.chkCanOnlyBeTriggeredByPlayer.ForeColor = Color.FromArgb(170, 170, 170);
      this.chkCanOnlyBeTriggeredByPlayer.Location = new Point(80, 41);
      this.chkCanOnlyBeTriggeredByPlayer.Name = "chkCanOnlyBeTriggeredByPlayer";
      this.chkCanOnlyBeTriggeredByPlayer.Size = new Size(180, 17);
      this.chkCanOnlyBeTriggeredByPlayer.TabIndex = 11;
      this.chkCanOnlyBeTriggeredByPlayer.Text = "Can Only Be Triggered By Player";
      this.chkCanOnlyBeTriggeredByPlayer.UseVisualStyleBackColor = true;
      this.btnClose.BackColor = Color.FromArgb(0, 0, 0);
      this.btnClose.ClipBackground = false;
      this.btnClose.DelayFrameRefresh = false;
      this.btnClose.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnClose.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnClose.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnClose.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnClose.IntensifyColors = false;
      this.btnClose.Location = new Point(259, 6);
      this.btnClose.Name = "btnClose";
      this.btnClose.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnClose.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnClose.Size = new Size(75, 25);
      this.btnClose.TabIndex = 19;
      this.btnClose.Text = "Close";
      this.btnClose.TextColor = Color.FromArgb(120, 120, 120);
      this.btnClose.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnClose.ToggledOn = false;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.btnDelete.BackColor = Color.FromArgb(0, 0, 0);
      this.btnDelete.ClipBackground = false;
      this.btnDelete.DelayFrameRefresh = false;
      this.btnDelete.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnDelete.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnDelete.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnDelete.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnDelete.IntensifyColors = false;
      this.btnDelete.Location = new Point(10, 460);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnDelete.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnDelete.Size = new Size(320, 25);
      this.btnDelete.TabIndex = 18;
      this.btnDelete.Text = "Delete Event and Actions";
      this.btnDelete.TextColor = Color.FromArgb(120, 120, 120);
      this.btnDelete.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnDelete.ToggledOn = false;
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this.lblEventActions.AutoSize = true;
      this.lblEventActions.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblEventActions.Location = new Point(10, 332);
      this.lblEventActions.Name = "lblEventActions";
      this.lblEventActions.Size = new Size(48, 13);
      this.lblEventActions.TabIndex = 17;
      this.lblEventActions.Text = "Action(s)";
      this.btnAddNewActionTarget.BackColor = Color.FromArgb(0, 0, 0);
      this.btnAddNewActionTarget.ClipBackground = false;
      this.btnAddNewActionTarget.DelayFrameRefresh = false;
      this.btnAddNewActionTarget.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnAddNewActionTarget.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnAddNewActionTarget.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnAddNewActionTarget.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnAddNewActionTarget.IntensifyColors = false;
      this.btnAddNewActionTarget.Location = new Point(175, 414);
      this.btnAddNewActionTarget.Name = "btnAddNewActionTarget";
      this.btnAddNewActionTarget.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnAddNewActionTarget.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnAddNewActionTarget.Size = new Size(155, 40);
      this.btnAddNewActionTarget.TabIndex = 16;
      this.btnAddNewActionTarget.Text = "Select Target of New Action by clicking in Main view";
      this.btnAddNewActionTarget.TextColor = Color.FromArgb(120, 120, 120);
      this.btnAddNewActionTarget.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnAddNewActionTarget.ToggledOn = false;
      this.btnAddNewActionTarget.Click += new EventHandler(this.btnAddNewActionTarget_Click);
      this.btnAddNewActionBlank.BackColor = Color.FromArgb(0, 0, 0);
      this.btnAddNewActionBlank.ClipBackground = false;
      this.btnAddNewActionBlank.DelayFrameRefresh = false;
      this.btnAddNewActionBlank.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnAddNewActionBlank.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnAddNewActionBlank.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnAddNewActionBlank.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnAddNewActionBlank.IntensifyColors = false;
      this.btnAddNewActionBlank.Location = new Point(10, 414);
      this.btnAddNewActionBlank.Name = "btnAddNewActionBlank";
      this.btnAddNewActionBlank.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnAddNewActionBlank.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnAddNewActionBlank.Size = new Size(155, 40);
      this.btnAddNewActionBlank.TabIndex = 15;
      this.btnAddNewActionBlank.Text = "Add New Blank Action";
      this.btnAddNewActionBlank.TextColor = Color.FromArgb(120, 120, 120);
      this.btnAddNewActionBlank.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnAddNewActionBlank.ToggledOn = false;
      this.btnAddNewActionBlank.Click += new EventHandler(this.btnAddNewActionBlank_Click);
      this.cmbMultipleEventActionType.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbMultipleEventActionType.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbMultipleEventActionType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbMultipleEventActionType.FlatStyle = FlatStyle.Popup;
      this.cmbMultipleEventActionType.Font = new Font("Verdana", 8.25f);
      this.cmbMultipleEventActionType.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbMultipleEventActionType.FormattingEnabled = true;
      this.cmbMultipleEventActionType.Location = new Point(130, 325);
      this.cmbMultipleEventActionType.Name = "cmbMultipleEventActionType";
      this.cmbMultipleEventActionType.Size = new Size(200, 22);
      this.cmbMultipleEventActionType.TabIndex = 14;
      this.lblTriggerObjectName.AutoSize = true;
      this.lblTriggerObjectName.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTriggerObjectName.Location = new Point(10, 10);
      this.lblTriggerObjectName.Name = "lblTriggerObjectName";
      this.lblTriggerObjectName.Size = new Size(105, 13);
      this.lblTriggerObjectName.TabIndex = 13;
      this.lblTriggerObjectName.Text = "Trigger Object Name";
      this.ctlEventActions.Location = new Point(10, 348);
      this.ctlEventActions.Name = "ctlEventActions";
      this.ctlEventActions.RowTemplateHeight = 20;
      this.ctlEventActions.Size = new Size(320, 60);
      this.ctlEventActions.SoundsEnabled = false;
      this.ctlEventActions.TabIndex = 12;
      this.ctlEventActions.RowDoubleClick += new EventHandler(this.ctlEventActions_RowDoubleClick);
      this.lblDescription.AutoSize = true;
      this.lblDescription.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblDescription.Location = new Point(10, 92);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new Size(60, 13);
      this.lblDescription.TabIndex = 10;
      this.lblDescription.Text = "Description";
      this.lblTitle.AutoSize = true;
      this.lblTitle.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTitle.Location = new Point(10, 66);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(27, 13);
      this.lblTitle.TabIndex = 9;
      this.lblTitle.Text = "Title";
      this.lblBuildPlanetaryFacilityDefinition.AutoSize = true;
      this.lblBuildPlanetaryFacilityDefinition.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblBuildPlanetaryFacilityDefinition.Location = new Point(6, 196);
      this.lblBuildPlanetaryFacilityDefinition.Name = "lblBuildPlanetaryFacilityDefinition";
      this.lblBuildPlanetaryFacilityDefinition.Size = new Size(92, 13);
      this.lblBuildPlanetaryFacilityDefinition.TabIndex = 6;
      this.lblBuildPlanetaryFacilityDefinition.Text = "Build Facility Type";
      this.lblBuiltObjectSubRole.AutoSize = true;
      this.lblBuiltObjectSubRole.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblBuiltObjectSubRole.Location = new Point(192, 196);
      this.lblBuiltObjectSubRole.Name = "lblBuiltObjectSubRole";
      this.lblBuiltObjectSubRole.Size = new Size(84, 13);
      this.lblBuiltObjectSubRole.TabIndex = 5;
      this.lblBuiltObjectSubRole.Text = "Build Base Type";
      this.cmbBuildBuiltObjectSubRole.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbBuildBuiltObjectSubRole.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbBuildBuiltObjectSubRole.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbBuildBuiltObjectSubRole.FlatStyle = FlatStyle.Flat;
      this.cmbBuildBuiltObjectSubRole.Font = new Font("Verdana", 8.25f);
      this.cmbBuildBuiltObjectSubRole.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbBuildBuiltObjectSubRole.FormattingEnabled = true;
      this.cmbBuildBuiltObjectSubRole.Location = new Point(282, 193);
      this.cmbBuildBuiltObjectSubRole.Name = "cmbBuildBuiltObjectSubRole";
      this.cmbBuildBuiltObjectSubRole.Size = new Size(121, 22);
      this.cmbBuildBuiltObjectSubRole.TabIndex = 3;
      this.cmbBuildBuiltObjectSubRole.SelectedIndexChanged += new EventHandler(this.cmbBuildBuiltObjectSubRole_SelectedIndexChanged);
      this.cmbBuildPlanetaryFacilityDefinition.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbBuildPlanetaryFacilityDefinition.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbBuildPlanetaryFacilityDefinition.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbBuildPlanetaryFacilityDefinition.FlatStyle = FlatStyle.Flat;
      this.cmbBuildPlanetaryFacilityDefinition.Font = new Font("Verdana", 8.25f);
      this.cmbBuildPlanetaryFacilityDefinition.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbBuildPlanetaryFacilityDefinition.FormattingEnabled = true;
      this.cmbBuildPlanetaryFacilityDefinition.Location = new Point(104, 193);
      this.cmbBuildPlanetaryFacilityDefinition.Name = "cmbBuildPlanetaryFacilityDefinition";
      this.cmbBuildPlanetaryFacilityDefinition.Size = new Size(121, 22);
      this.cmbBuildPlanetaryFacilityDefinition.TabIndex = 2;
      this.cmbBuildPlanetaryFacilityDefinition.SelectedIndexChanged += new EventHandler(this.cmbBuildPlanetaryFacilityDefinition_SelectedIndexChanged);
      this.lblEventTriggerType.AutoSize = true;
      this.lblEventTriggerType.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblEventTriggerType.Location = new Point(10, 163);
      this.lblEventTriggerType.Name = "lblEventTriggerType";
      this.lblEventTriggerType.Size = new Size(67, 13);
      this.lblEventTriggerType.TabIndex = 1;
      this.lblEventTriggerType.Text = "Trigger Type";
      this.cmbEventTriggerType.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbEventTriggerType.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbEventTriggerType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbEventTriggerType.FlatStyle = FlatStyle.Flat;
      this.cmbEventTriggerType.Font = new Font("Verdana", 8.25f);
      this.cmbEventTriggerType.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbEventTriggerType.FormattingEnabled = true;
      this.cmbEventTriggerType.Location = new Point(80, 160);
      this.cmbEventTriggerType.Name = "cmbEventTriggerType";
      this.cmbEventTriggerType.Size = new Size(121, 22);
      this.cmbEventTriggerType.TabIndex = 0;
      this.cmbEventTriggerType.SelectedIndexChanged += new EventHandler(this.cmbEventTriggerType_SelectedIndexChanged);
      this.cmbEmpire.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbEmpire.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbEmpire.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbEmpire.FlatStyle = FlatStyle.Popup;
      this.cmbEmpire.Font = new Font("Verdana", 8.25f);
      this.cmbEmpire.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbEmpire.FormattingEnabled = true;
      this.cmbEmpire.Location = new Point(104, 232);
      this.cmbEmpire.Name = "cmbEmpire";
      this.cmbEmpire.Size = new Size(121, 22);
      this.cmbEmpire.TabIndex = 20;
      this.cmbEmpire.SelectedIndexChanged += new EventHandler(this.cmbEmpire_SelectedIndexChanged);
      this.cmbEmpireOther.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbEmpireOther.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbEmpireOther.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbEmpireOther.FlatStyle = FlatStyle.Popup;
      this.cmbEmpireOther.Font = new Font("Verdana", 8.25f);
      this.cmbEmpireOther.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbEmpireOther.FormattingEnabled = true;
      this.cmbEmpireOther.Location = new Point(104, 260);
      this.cmbEmpireOther.Name = "cmbEmpireOther";
      this.cmbEmpireOther.Size = new Size(121, 22);
      this.cmbEmpireOther.TabIndex = 21;
      this.cmbEmpireOther.SelectedIndexChanged += new EventHandler(this.cmbEmpireOther_SelectedIndexChanged);
      this.cmbDiplomaticRelationType.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbDiplomaticRelationType.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbDiplomaticRelationType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbDiplomaticRelationType.FlatStyle = FlatStyle.Popup;
      this.cmbDiplomaticRelationType.Font = new Font("Verdana", 8.25f);
      this.cmbDiplomaticRelationType.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbDiplomaticRelationType.FormattingEnabled = true;
      this.cmbDiplomaticRelationType.Location = new Point(104, 289);
      this.cmbDiplomaticRelationType.Name = "cmbDiplomaticRelationType";
      this.cmbDiplomaticRelationType.Size = new Size(121, 22);
      this.cmbDiplomaticRelationType.TabIndex = 22;
      this.cmbDiplomaticRelationType.SelectedIndexChanged += new EventHandler(this.cmbDiplomaticRelationType_SelectedIndexChanged);
      this.cmbResearchProject.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbResearchProject.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbResearchProject.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbResearchProject.FlatStyle = FlatStyle.Popup;
      this.cmbResearchProject.Font = new Font("Verdana", 8.25f);
      this.cmbResearchProject.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbResearchProject.FormattingEnabled = true;
      this.cmbResearchProject.Location = new Point(273, 289);
      this.cmbResearchProject.Name = "cmbResearchProject";
      this.cmbResearchProject.Size = new Size(121, 22);
      this.cmbResearchProject.TabIndex = 23;
      this.cmbResearchProject.SelectedIndexChanged += new EventHandler(this.cmbResearchProject_SelectedIndexChanged);
      this.lblEmpire.AutoSize = true;
      this.lblEmpire.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblEmpire.Location = new Point(10, 235);
      this.lblEmpire.Name = "lblEmpire";
      this.lblEmpire.Size = new Size(39, 13);
      this.lblEmpire.TabIndex = 24;
      this.lblEmpire.Text = "Empire";
      this.lblEmpireOther.AutoSize = true;
      this.lblEmpireOther.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblEmpireOther.Location = new Point(10, 260);
      this.lblEmpireOther.Name = "lblEmpireOther";
      this.lblEmpireOther.Size = new Size(68, 13);
      this.lblEmpireOther.TabIndex = 25;
      this.lblEmpireOther.Text = "Other Empire";
      this.lblDiplomaticRelationType.AutoSize = true;
      this.lblDiplomaticRelationType.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblDiplomaticRelationType.Location = new Point(10, 292);
      this.lblDiplomaticRelationType.Name = "lblDiplomaticRelationType";
      this.lblDiplomaticRelationType.Size = new Size(73, 13);
      this.lblDiplomaticRelationType.TabIndex = 26;
      this.lblDiplomaticRelationType.Text = "Relation Type";
      this.lblResearchProject.AutoSize = true;
      this.lblResearchProject.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblResearchProject.Location = new Point(245, 269);
      this.lblResearchProject.Name = "lblResearchProject";
      this.lblResearchProject.Size = new Size(89, 13);
      this.lblResearchProject.TabIndex = 27;
      this.lblResearchProject.Text = "Research Project";
      this.cmbCharacter.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbCharacter.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbCharacter.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCharacter.FlatStyle = FlatStyle.Popup;
      this.cmbCharacter.Font = new Font("Verdana", 8.25f);
      this.cmbCharacter.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbCharacter.FormattingEnabled = true;
      this.cmbCharacter.Location = new Point(273, 232);
      this.cmbCharacter.Name = "cmbCharacter";
      this.cmbCharacter.Size = new Size(121, 22);
      this.cmbCharacter.TabIndex = 28;
      this.cmbCharacter.SelectedIndexChanged += new EventHandler(this.cmbCharacter_SelectedIndexChanged);
      this.lblCharacter.AutoSize = true;
      this.lblCharacter.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblCharacter.Location = new Point(231, 235);
      this.lblCharacter.Name = "lblCharacter";
      this.lblCharacter.Size = new Size(53, 13);
      this.lblCharacter.TabIndex = 29;
      this.lblCharacter.Text = "Character";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(24, 24, 32);
      this.Controls.Add((Control) this.lblCharacter);
      this.Controls.Add((Control) this.cmbCharacter);
      this.Controls.Add((Control) this.lblResearchProject);
      this.Controls.Add((Control) this.lblDiplomaticRelationType);
      this.Controls.Add((Control) this.lblEmpireOther);
      this.Controls.Add((Control) this.lblEmpire);
      this.Controls.Add((Control) this.cmbResearchProject);
      this.Controls.Add((Control) this.cmbDiplomaticRelationType);
      this.Controls.Add((Control) this.cmbEmpireOther);
      this.Controls.Add((Control) this.cmbEmpire);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.btnDelete);
      this.Controls.Add((Control) this.lblEventActions);
      this.Controls.Add((Control) this.btnAddNewActionTarget);
      this.Controls.Add((Control) this.btnAddNewActionBlank);
      this.Controls.Add((Control) this.cmbMultipleEventActionType);
      this.Controls.Add((Control) this.lblTriggerObjectName);
      this.Controls.Add((Control) this.ctlEventActions);
      this.Controls.Add((Control) this.chkCanOnlyBeTriggeredByPlayer);
      this.Controls.Add((Control) this.lblDescription);
      this.Controls.Add((Control) this.lblTitle);
      this.Controls.Add((Control) this.txtDescription);
      this.Controls.Add((Control) this.txtTitle);
      this.Controls.Add((Control) this.lblBuildPlanetaryFacilityDefinition);
      this.Controls.Add((Control) this.lblBuiltObjectSubRole);
      this.Controls.Add((Control) this.chkTriggeredByRuin);
      this.Controls.Add((Control) this.cmbBuildBuiltObjectSubRole);
      this.Controls.Add((Control) this.cmbBuildPlanetaryFacilityDefinition);
      this.Controls.Add((Control) this.lblEventTriggerType);
      this.Controls.Add((Control) this.cmbEventTriggerType);
      this.Name = nameof (GameEventPanel);
      this.Size = new Size(340, 495);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public event EventHandler PanelClosed;

    public event EventHandler SelectActionTarget;

    public event EventHandler AddNewBlankAction;

    public event EventHandler EditAction;

    public GameEventPanel() => this.InitializeComponent();

    public EventAction SelectedEventAction => this.ctlEventActions.SelectedEventAction;

    public GameEvent GameEvent => this._GameEvent;

    public void ClearData()
    {
      this._GameEvent = (GameEvent) null;
      this._Galaxy = (Galaxy) null;
    }

    public void SetFontCache(IFontCache fontCache) => this.ctlEventActions.SetFontCache(fontCache);

    public void BindData(
      Galaxy galaxy,
      GameEvent gameEvent,
      Bitmap[] planetaryFacilityImages,
      Font normalFont,
      Font titleFont,
      CharacterImageCache characterImageCache)
    {
      this._Binding = true;
      this._Galaxy = galaxy;
      this._GameEvent = gameEvent;
      this._NormalFont = normalFont;
      this._TitleFont = titleFont;
      this._CharacterImageCache = characterImageCache;
      if (this._GameEvent != null)
        this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
      else
        this.lblTriggerObjectName.Text = string.Empty;
      this.LayoutPanel();
      this.cmbEventTriggerType.BindData(this._GameEvent.ValidTriggerTypes);
      this.cmbEventTriggerType.SetSelectedEventTriggerType(this._GameEvent.TriggerType);
      this.chkCanOnlyBeTriggeredByPlayer.Checked = this._GameEvent.CanOnlyBeTriggeredByPlayer;
      this.txtTitle.Text = this._GameEvent.Title;
      this.txtDescription.Text = this._GameEvent.Description;
      List<BuiltObjectSubRole> subRoles = new List<BuiltObjectSubRole>();
      subRoles.Add(BuiltObjectSubRole.MiningStation);
      subRoles.Add(BuiltObjectSubRole.GasMiningStation);
      subRoles.Add(BuiltObjectSubRole.SmallSpacePort);
      subRoles.Add(BuiltObjectSubRole.MediumSpacePort);
      subRoles.Add(BuiltObjectSubRole.LargeSpacePort);
      subRoles.Add(BuiltObjectSubRole.DefensiveBase);
      subRoles.Add(BuiltObjectSubRole.WeaponsResearchStation);
      subRoles.Add(BuiltObjectSubRole.EnergyResearchStation);
      subRoles.Add(BuiltObjectSubRole.HighTechResearchStation);
      subRoles.Add(BuiltObjectSubRole.MonitoringStation);
      subRoles.Add(BuiltObjectSubRole.ResortBase);
      subRoles.Add(BuiltObjectSubRole.ColonyShip);
      subRoles.Add(BuiltObjectSubRole.ConstructionShip);
      subRoles.Add(BuiltObjectSubRole.ResupplyShip);
      if (this._GameEvent.TriggerType == EventTriggerType.CharacterAppears || this._GameEvent.TriggerType == EventTriggerType.CharacterKilled)
        this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
      else
        this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
      this.cmbEmpire.SetSelectedEmpire(this._GameEvent.Empire);
      if (this._GameEvent.TriggerType == EventTriggerType.EmpireEliminated)
        this.cmbEmpireOther.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, true);
      else
        this.cmbEmpireOther.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
      this.cmbEmpireOther.SetSelectedEmpire(this._GameEvent.EmpireOther);
      this.cmbDiplomaticRelationType.BindData(new List<DiplomaticRelationType>()
      {
        DiplomaticRelationType.None,
        DiplomaticRelationType.FreeTradeAgreement,
        DiplomaticRelationType.Protectorate,
        DiplomaticRelationType.MutualDefensePact,
        DiplomaticRelationType.SubjugatedDominion,
        DiplomaticRelationType.TradeSanctions,
        DiplomaticRelationType.War
      });
      this.cmbDiplomaticRelationType.SetSelectedDiplomaticRelationType(this._GameEvent.DiplomaticRelationType);
      this.cmbResearchProject.BindData(galaxy.ResearchNodeDefinitions, false);
      this.cmbResearchProject.SetSelectedResearchNode(this._GameEvent.ResearchProjectId);
      this.cmbBuildBuiltObjectSubRole.SelectedIndexChanged -= new EventHandler(this.cmbBuildBuiltObjectSubRole_SelectedIndexChanged);
      this.cmbBuildPlanetaryFacilityDefinition.SelectedIndexChanged -= new EventHandler(this.cmbBuildPlanetaryFacilityDefinition_SelectedIndexChanged);
      this.cmbBuildBuiltObjectSubRole.BindData(subRoles, true);
      this.cmbBuildBuiltObjectSubRole.SetSelectedBuiltObjectSubRole(this._GameEvent.TriggerBuiltObjectSubRole);
      this.cmbBuildPlanetaryFacilityDefinition.BindData((Empire) null, Galaxy.PlanetaryFacilityDefinitionsStatic, planetaryFacilityImages, true);
      this.cmbBuildPlanetaryFacilityDefinition.SetSelectedPlanetaryFacilityDefinition(this._GameEvent.TriggerFacility);
      this.cmbBuildBuiltObjectSubRole.SelectedIndexChanged += new EventHandler(this.cmbBuildBuiltObjectSubRole_SelectedIndexChanged);
      this.cmbBuildPlanetaryFacilityDefinition.SelectedIndexChanged += new EventHandler(this.cmbBuildPlanetaryFacilityDefinition_SelectedIndexChanged);
      this.chkTriggeredByRuin.CheckedChanged -= new EventHandler(this.chkTriggeredByRuin_CheckedChanged);
      this.chkTriggeredByRuin.Text = TextResolver.GetText("Triggered By Ruin");
      this.chkTriggeredByRuin.Enabled = false;
      this.chkTriggeredByRuin.Checked = false;
      if (this._GameEvent.TriggerObject != null && this._GameEvent.TriggerObject is Habitat)
      {
        Habitat triggerObject = (Habitat) this._GameEvent.TriggerObject;
        if (triggerObject.Ruin != null)
        {
          this.chkTriggeredByRuin.Enabled = true;
          this.chkTriggeredByRuin.Text = string.Format(TextResolver.GetText("Triggered By Ruin X"), (object) triggerObject.Ruin.Name);
          this.chkTriggeredByRuin.Checked = this._GameEvent.TriggerRuin != null;
        }
        else
        {
          this.chkTriggeredByRuin.Text = TextResolver.GetText("Triggered By Ruin");
          this.chkTriggeredByRuin.Enabled = false;
          this.chkTriggeredByRuin.Checked = false;
        }
      }
      else
      {
        this.chkTriggeredByRuin.Text = TextResolver.GetText("Triggered By Ruin");
        this.chkTriggeredByRuin.Enabled = false;
        this.chkTriggeredByRuin.Checked = false;
      }
      this.chkTriggeredByRuin.CheckedChanged += new EventHandler(this.chkTriggeredByRuin_CheckedChanged);
      this.cmbMultipleEventActionType.BindData(new List<MultipleEventActionType>()
      {
        MultipleEventActionType.ExecuteAllActions,
        MultipleEventActionType.ExecuteSingleRandomAction
      });
      this.cmbMultipleEventActionType.SetSelectedMultipleEventActionType(this._GameEvent.Actions.ExecutionType);
      this.ctlEventActions.BindData(this._GameEvent.Actions);
      this._Binding = false;
    }

    public void BindActions() => this.ctlEventActions.BindData(this._GameEvent.Actions);

    public GameEvent UnbindData()
    {
      this._GameEvent.TriggerType = this.cmbEventTriggerType.SelectedEventTriggerType;
      this._GameEvent.CanOnlyBeTriggeredByPlayer = this.chkCanOnlyBeTriggeredByPlayer.Checked;
      this._GameEvent.Title = this.txtTitle.Text;
      this._GameEvent.Description = this.txtDescription.Text;
      switch (this._GameEvent.TriggerType)
      {
        case EventTriggerType.DiplomaticRelationChange:
          this._GameEvent.Empire = this.cmbEmpire.SelectedEmpire;
          this._GameEvent.EmpireOther = this.cmbEmpireOther.SelectedEmpire;
          this._GameEvent.DiplomaticRelationType = this.cmbDiplomaticRelationType.SelectedDiplomaticRelationType;
          break;
        case EventTriggerType.EmpireEncounter:
          this._GameEvent.Empire = this.cmbEmpire.SelectedEmpire;
          this._GameEvent.EmpireOther = this.cmbEmpireOther.SelectedEmpire;
          break;
        case EventTriggerType.ResearchBreakthrough:
          this._GameEvent.Empire = this.cmbEmpire.SelectedEmpire;
          ResearchNodeDefinition selectedResearchNode = this.cmbResearchProject.SelectedResearchNode;
          if (selectedResearchNode != null)
          {
            this._GameEvent.ResearchProjectId = selectedResearchNode.ResearchNodeId;
            break;
          }
          break;
        case EventTriggerType.PlanetDestroyerConstructionCompleted:
          this._GameEvent.Empire = this.cmbEmpire.SelectedEmpire;
          break;
        case EventTriggerType.EmpireEliminated:
          this._GameEvent.Empire = this.cmbEmpire.SelectedEmpire;
          this._GameEvent.EmpireOther = this.cmbEmpireOther.SelectedEmpire;
          break;
        case EventTriggerType.CharacterAppears:
          this._GameEvent.Empire = this.cmbEmpire.SelectedEmpire;
          this._GameEvent.Character = this.cmbCharacter.GetSelectedCharacter();
          break;
        case EventTriggerType.CharacterKilled:
          this._GameEvent.Empire = this.cmbEmpire.SelectedEmpire;
          this._GameEvent.Character = this.cmbCharacter.GetSelectedCharacter();
          break;
      }
      this._GameEvent.TriggerBuiltObjectSubRole = !this.cmbBuildBuiltObjectSubRole.Enabled ? BuiltObjectSubRole.Undefined : this.cmbBuildBuiltObjectSubRole.SelectedBuiltObjectSubRole;
      this._GameEvent.TriggerFacility = !this.cmbBuildPlanetaryFacilityDefinition.Enabled ? (PlanetaryFacilityDefinition) null : this.cmbBuildPlanetaryFacilityDefinition.SelectedPlanetaryFacility;
      if (this.chkTriggeredByRuin.Checked)
      {
        if (this._GameEvent.TriggerObject != null && this._GameEvent.TriggerObject is Habitat)
        {
          Habitat triggerObject = (Habitat) this._GameEvent.TriggerObject;
          if (triggerObject.Ruin != null)
          {
            this._GameEvent.TriggerRuin = triggerObject.Ruin;
            this._GameEvent.TriggerRuin.GameEventId = this._GameEvent.GameEventId;
          }
          else
            this._GameEvent.TriggerRuin = (Ruin) null;
        }
        else
          this._GameEvent.TriggerRuin = (Ruin) null;
      }
      else
        this._GameEvent.TriggerRuin = (Ruin) null;
      this._GameEvent.Actions = this.ctlEventActions.GetActions();
      this._GameEvent.Actions.ExecutionType = this.cmbMultipleEventActionType.SelectedMultipleEventActionType;
      return this._GameEvent;
    }

    private void cmbEventTriggerType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this._GameEvent.TriggerType = this.cmbEventTriggerType.SelectedEventTriggerType;
      this.lblEmpire.Visible = false;
      this.lblEmpireOther.Visible = false;
      this.lblDiplomaticRelationType.Visible = false;
      this.lblResearchProject.Visible = false;
      this.lblCharacter.Visible = false;
      this.cmbEmpire.Visible = false;
      this.cmbEmpireOther.Visible = false;
      this.cmbDiplomaticRelationType.Visible = false;
      this.cmbResearchProject.Visible = false;
      this.cmbCharacter.Visible = false;
      this.cmbBuildBuiltObjectSubRole.Enabled = false;
      this.cmbBuildPlanetaryFacilityDefinition.Enabled = false;
      this.lblBuiltObjectSubRole.Visible = false;
      this.lblBuildPlanetaryFacilityDefinition.Visible = false;
      this.cmbBuildBuiltObjectSubRole.Visible = false;
      this.cmbBuildPlanetaryFacilityDefinition.Visible = false;
      Empire selectedEmpire1 = this.cmbEmpire.SelectedEmpire;
      Empire selectedEmpire2 = this.cmbEmpireOther.SelectedEmpire;
      bool flag1 = false;
      bool includeNoEmpire1 = false;
      bool flag2 = false;
      bool includeNoEmpire2 = false;
      switch (this._GameEvent.TriggerType)
      {
        case EventTriggerType.Investigate:
          this.cmbBuildBuiltObjectSubRole.Enabled = false;
          this.cmbBuildPlanetaryFacilityDefinition.Enabled = false;
          this.lblBuiltObjectSubRole.Visible = false;
          this.lblBuildPlanetaryFacilityDefinition.Visible = false;
          this.cmbBuildBuiltObjectSubRole.Visible = false;
          this.cmbBuildPlanetaryFacilityDefinition.Visible = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = true;
          break;
        case EventTriggerType.Destroy:
        case EventTriggerType.Capture:
          this.cmbBuildBuiltObjectSubRole.Enabled = false;
          this.cmbBuildPlanetaryFacilityDefinition.Enabled = false;
          this.lblBuiltObjectSubRole.Visible = false;
          this.lblBuildPlanetaryFacilityDefinition.Visible = false;
          this.cmbBuildBuiltObjectSubRole.Visible = false;
          this.cmbBuildPlanetaryFacilityDefinition.Visible = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = true;
          break;
        case EventTriggerType.Build:
          this.cmbBuildBuiltObjectSubRole.Enabled = true;
          this.cmbBuildPlanetaryFacilityDefinition.Enabled = true;
          this.lblBuiltObjectSubRole.Visible = true;
          this.lblBuildPlanetaryFacilityDefinition.Visible = true;
          this.cmbBuildBuiltObjectSubRole.Visible = true;
          this.cmbBuildPlanetaryFacilityDefinition.Visible = true;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = true;
          break;
        case EventTriggerType.DiplomaticRelationChange:
          this.lblEmpire.Visible = true;
          this.lblEmpireOther.Visible = true;
          this.lblDiplomaticRelationType.Visible = true;
          this.cmbEmpire.Visible = true;
          this.cmbEmpireOther.Visible = true;
          this.cmbDiplomaticRelationType.Visible = true;
          this.chkCanOnlyBeTriggeredByPlayer.Checked = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = false;
          break;
        case EventTriggerType.EmpireEncounter:
          this.lblEmpire.Visible = true;
          this.lblEmpireOther.Visible = true;
          this.cmbEmpire.Visible = true;
          this.cmbEmpireOther.Visible = true;
          this.chkCanOnlyBeTriggeredByPlayer.Checked = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = false;
          break;
        case EventTriggerType.ResearchBreakthrough:
          this.lblEmpire.Visible = true;
          this.lblResearchProject.Visible = true;
          this.cmbEmpire.Visible = true;
          this.cmbResearchProject.Visible = true;
          this.chkCanOnlyBeTriggeredByPlayer.Checked = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = false;
          break;
        case EventTriggerType.PlanetDestroyerConstructionCompleted:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.chkCanOnlyBeTriggeredByPlayer.Checked = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = false;
          break;
        case EventTriggerType.EmpireEliminated:
          this.lblEmpire.Visible = true;
          this.lblEmpireOther.Visible = true;
          this.cmbEmpire.Visible = true;
          this.cmbEmpireOther.Visible = true;
          this.chkCanOnlyBeTriggeredByPlayer.Checked = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = false;
          includeNoEmpire2 = true;
          break;
        case EventTriggerType.CharacterAppears:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblCharacter.Visible = true;
          this.cmbCharacter.Visible = true;
          this.chkCanOnlyBeTriggeredByPlayer.Checked = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = false;
          flag1 = true;
          break;
        case EventTriggerType.CharacterKilled:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblCharacter.Visible = true;
          this.cmbCharacter.Visible = true;
          this.chkCanOnlyBeTriggeredByPlayer.Checked = false;
          this.chkCanOnlyBeTriggeredByPlayer.Enabled = false;
          flag1 = true;
          break;
      }
      if (flag1)
        this.cmbEmpire.BindData(this._Galaxy.PlayerEmpire, this._Galaxy.Empires, this._Galaxy.PirateEmpires, (Empire) null, includeNoEmpire1);
      else
        this.cmbEmpire.BindData(this._Galaxy.PlayerEmpire, this._Galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, includeNoEmpire1);
      if (flag2)
        this.cmbEmpireOther.BindData(this._Galaxy.PlayerEmpire, this._Galaxy.Empires, this._Galaxy.PirateEmpires, (Empire) null, includeNoEmpire2);
      else
        this.cmbEmpireOther.BindData(this._Galaxy.PlayerEmpire, this._Galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, includeNoEmpire2);
      if (!this._Binding)
      {
        this.cmbEmpire.SelectedIndex = 0;
        this.cmbEmpire.SetSelectedEmpire(selectedEmpire1);
        this.cmbEmpireOther.SelectedIndex = 0;
      }
      this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
    }

    private void chkTriggeredByRuin_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkTriggeredByRuin.Checked)
      {
        if (this._GameEvent.TriggerObject != null && this._GameEvent.TriggerObject is Habitat)
        {
          Habitat triggerObject = (Habitat) this._GameEvent.TriggerObject;
          if (triggerObject.Ruin != null)
          {
            this._GameEvent.TriggerRuin = triggerObject.Ruin;
            this._GameEvent.ValidTriggerTypes = this._GameEvent.ResolveValidTriggerTypes(this._GameEvent.TriggerObject, triggerObject.Ruin);
            this._GameEvent.TriggerType = this._GameEvent.ValidTriggerTypes[0];
          }
          else
          {
            this._GameEvent.TriggerRuin = (Ruin) null;
            this._GameEvent.ValidTriggerTypes = this._GameEvent.ResolveValidTriggerTypes(this._GameEvent.TriggerObject, (Ruin) null);
          }
        }
        else
        {
          this._GameEvent.TriggerRuin = (Ruin) null;
          this._GameEvent.ValidTriggerTypes = this._GameEvent.ResolveValidTriggerTypes(this._GameEvent.TriggerObject, (Ruin) null);
        }
      }
      else
      {
        this._GameEvent.TriggerRuin = (Ruin) null;
        this._GameEvent.ValidTriggerTypes = this._GameEvent.ResolveValidTriggerTypes(this._GameEvent.TriggerObject, (Ruin) null);
      }
      this.cmbEventTriggerType.BindData(this._GameEvent.ValidTriggerTypes);
      this.cmbEventTriggerType.SetSelectedEventTriggerType(this._GameEvent.TriggerType);
    }

    private void cmbBuildPlanetaryFacilityDefinition_SelectedIndexChanged(
      object sender,
      EventArgs e)
    {
      if (this._DisableSelectedIndexChanged)
        return;
      this._DisableSelectedIndexChanged = true;
      this.cmbBuildBuiltObjectSubRole.SetSelectedBuiltObjectSubRole(BuiltObjectSubRole.Undefined);
      this._DisableSelectedIndexChanged = false;
      this._GameEvent.TriggerBuiltObjectSubRole = this.cmbBuildBuiltObjectSubRole.SelectedBuiltObjectSubRole;
      this._GameEvent.TriggerFacility = this.cmbBuildPlanetaryFacilityDefinition.SelectedPlanetaryFacility;
      this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
    }

    private void cmbBuildBuiltObjectSubRole_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._DisableSelectedIndexChanged)
        return;
      this._DisableSelectedIndexChanged = true;
      this.cmbBuildPlanetaryFacilityDefinition.SetSelectedPlanetaryFacilityDefinition((PlanetaryFacilityDefinition) null);
      this._GameEvent.TriggerBuiltObjectSubRole = this.cmbBuildBuiltObjectSubRole.SelectedBuiltObjectSubRole;
      this._GameEvent.TriggerFacility = this.cmbBuildPlanetaryFacilityDefinition.SelectedPlanetaryFacility;
      this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
      this._DisableSelectedIndexChanged = false;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      GameEvent byId = this._Galaxy.GameEvents.GetById(this._GameEvent.GameEventId);
      if (byId != null)
      {
        if (byId.TriggerObject != null)
          byId.TriggerObject.GameEventId = (short) -1;
        this._Galaxy.GameEvents.Remove(byId);
      }
      this.ClosePanel();
    }

    public void LayoutPanel()
    {
      this.SuspendLayout();
      this.lblTriggerObjectName.Location = new Point(5, 5);
      this.lblTriggerObjectName.Font = this._TitleFont;
      this.lblTriggerObjectName.MaximumSize = new Size(345, 75);
      this.btnClose.Size = new Size(75, 25);
      this.btnClose.Location = new Point(359, 6);
      this.lblEventTriggerType.Location = new Point(10, 91);
      this.lblEventTriggerType.Font = this._NormalFont;
      this.cmbEventTriggerType.Location = new Point(120, 88);
      this.cmbEventTriggerType.Size = new Size(181, 22);
      this.cmbEventTriggerType.BringToFront();
      this.chkTriggeredByRuin.Location = new Point(309, 91);
      this.chkCanOnlyBeTriggeredByPlayer.Location = new Point(120, 116);
      this.lblTitle.Location = new Point(10, 141);
      this.lblTitle.Font = this._NormalFont;
      this.txtTitle.Location = new Point(120, 139);
      this.txtTitle.Size = new Size(310, 20);
      this.txtTitle.Font = this._NormalFont;
      this.lblDescription.Location = new Point(10, 167);
      this.lblDescription.Font = this._NormalFont;
      this.txtDescription.Location = new Point(120, 165);
      this.txtDescription.Size = new Size(310, 60);
      this.txtDescription.ScrollBars = ScrollBars.Vertical;
      this.txtDescription.Font = this._NormalFont;
      this.lblBuiltObjectSubRole.Location = new Point(10, 230);
      this.lblBuiltObjectSubRole.Font = this._NormalFont;
      this.lblBuiltObjectSubRole.Text = TextResolver.GetText("Build Base/Ship");
      this.cmbBuildBuiltObjectSubRole.Size = new Size(310, 22);
      this.cmbBuildBuiltObjectSubRole.Location = new Point(120, 227);
      this.lblBuildPlanetaryFacilityDefinition.Location = new Point(10, 253);
      this.lblBuildPlanetaryFacilityDefinition.Font = this._NormalFont;
      this.lblBuildPlanetaryFacilityDefinition.Text = TextResolver.GetText("Build Facility Type");
      this.cmbBuildPlanetaryFacilityDefinition.Size = new Size(310, 22);
      this.cmbBuildPlanetaryFacilityDefinition.Location = new Point(120, 250);
      this.lblEmpire.Location = new Point(10, 276);
      this.lblEmpire.Font = this._NormalFont;
      this.lblEmpire.Text = TextResolver.GetText("Empire");
      this.cmbEmpire.Location = new Point(120, 273);
      this.cmbEmpire.Size = new Size(310, 22);
      this.lblEmpireOther.Location = new Point(10, 299);
      this.lblEmpireOther.Font = this._NormalFont;
      this.lblEmpireOther.Text = TextResolver.GetText("Other Empire");
      this.cmbEmpireOther.Location = new Point(120, 296);
      this.cmbEmpireOther.Size = new Size(310, 22);
      this.lblCharacter.Location = new Point(10, 299);
      this.lblCharacter.Font = this._NormalFont;
      this.lblCharacter.Text = TextResolver.GetText("Character");
      this.cmbCharacter.Location = new Point(120, 296);
      this.cmbCharacter.Size = new Size(310, 22);
      this.lblResearchProject.Location = new Point(10, 322);
      this.lblResearchProject.Font = this._NormalFont;
      this.lblResearchProject.Text = TextResolver.GetText("Research Project");
      this.cmbResearchProject.Location = new Point(120, 319);
      this.cmbResearchProject.Size = new Size(310, 22);
      this.lblDiplomaticRelationType.Location = new Point(10, 322);
      this.lblDiplomaticRelationType.Font = this._NormalFont;
      this.lblDiplomaticRelationType.Text = TextResolver.GetText("Relation Type");
      this.cmbDiplomaticRelationType.Location = new Point(120, 319);
      this.cmbDiplomaticRelationType.Size = new Size(310, 22);
      this.lblEventActions.Location = new Point(10, 362);
      this.lblEventActions.Font = this._NormalFont;
      this.cmbMultipleEventActionType.Location = new Point(120, 355);
      this.cmbMultipleEventActionType.Size = new Size(310, 22);
      this.cmbMultipleEventActionType.BringToFront();
      this.ctlEventActions.Location = new Point(10, 378);
      this.ctlEventActions.Size = new Size(420, 160);
      this.ctlEventActions.Grid.Columns["Type"].Width = 150;
      this.ctlEventActions.Grid.Columns["Description"].Width = 270;
      this.ctlEventActions.BringToFront();
      this.btnAddNewActionBlank.Size = new Size(205, 60);
      this.btnAddNewActionBlank.Location = new Point(10, 544);
      this.btnAddNewActionTarget.Size = new Size(205, 60);
      this.btnAddNewActionTarget.Location = new Point(225, 544);
      this.btnDelete.Size = new Size(420, 25);
      this.btnDelete.Location = new Point(10, 610);
      this.ResumeLayout();
    }

    private void btnAddNewActionBlank_Click(object sender, EventArgs e)
    {
      if (this.AddNewBlankAction == null)
        return;
      this.AddNewBlankAction((object) this, new EventArgs());
    }

    private void btnAddNewActionTarget_Click(object sender, EventArgs e)
    {
      if (this.SelectActionTarget == null)
        return;
      this.SelectActionTarget((object) this, new EventArgs());
    }

    private void ctlEventActions_RowDoubleClick(object sender, EventArgs e)
    {
      if (this.EditAction == null)
        return;
      this.EditAction((object) this, new EventArgs());
    }

    private void ClosePanel()
    {
      if (this.PanelClosed == null)
        return;
      this.PanelClosed((object) this, new EventArgs());
    }

    private void btnClose_Click(object sender, EventArgs e) => this.ClosePanel();

    private void cmbEmpire_SelectedIndexChanged(object sender, EventArgs e)
    {
      this._GameEvent.Empire = this.cmbEmpire.SelectedEmpire;
      this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
      this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
      if (this._GameEvent.Empire == null)
        return;
      switch (this.cmbEventTriggerType.SelectedEventTriggerType)
      {
        case EventTriggerType.CharacterAppears:
          if (this._GameEvent.Empire.DominantRace == null || this._GameEvent.Empire.DominantRace.AvailableCharacters == null || this._GameEvent.Empire.DominantRace.AvailableCharacters.Count <= 0)
            break;
          this.cmbCharacter.BindData(this._GameEvent.Empire, this._GameEvent.Empire.DominantRace.AvailableCharacters, this._CharacterImageCache, this._Galaxy, false, false);
          this.cmbCharacter.SetSelectedCharacter(this._GameEvent.Empire.DominantRace.AvailableCharacters[0]);
          break;
        case EventTriggerType.CharacterKilled:
          if (this._GameEvent.Empire.Characters == null || this._GameEvent.Empire.Characters.Count <= 0)
            break;
          this.cmbCharacter.BindData(this._GameEvent.Empire, this._GameEvent.Empire.Characters, this._CharacterImageCache, this._Galaxy, false, false);
          this.cmbCharacter.SetSelectedCharacter(this._GameEvent.Empire.Characters[0]);
          break;
      }
    }

    private void cmbEmpireOther_SelectedIndexChanged(object sender, EventArgs e)
    {
      this._GameEvent.EmpireOther = this.cmbEmpireOther.SelectedEmpire;
      this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
    }

    private void cmbDiplomaticRelationType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this._GameEvent.DiplomaticRelationType = this.cmbDiplomaticRelationType.SelectedDiplomaticRelationType;
      this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
    }

    private void cmbResearchProject_SelectedIndexChanged(object sender, EventArgs e)
    {
      this._GameEvent.ResearchProjectId = this.cmbResearchProject.SelectedResearchNode == null ? -1 : this.cmbResearchProject.SelectedResearchNode.ResearchNodeId;
      this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
    }

    private void cmbCharacter_SelectedIndexChanged(object sender, EventArgs e)
    {
      this._GameEvent.Character = this.cmbCharacter.GetSelectedCharacter();
      this.lblTriggerObjectName.Text = Galaxy.ResolveDescription(this._GameEvent);
    }
  }
}
