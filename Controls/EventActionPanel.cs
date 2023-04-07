// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EventActionPanel
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
  public class EventActionPanel : UserControl
  {
    private IContainer components;
    private EventActionTypeDropDown cmbEventActionType;
    private ResourceDropDown cmbResource;
    private SmoothLabel lblEventActionType;
    private SmoothLabel lblTriggerObjectName;
    private SmoothLabel lblResource;
    private BuiltObjectSubRoleDropDown cmbBuiltObjectSubRole;
    private SmoothLabel lblBuiltObjectSubRole;
    private NumericUpDown numValue;
    private SmoothLabel lblValue;
    private GalaxyLocationDropDown cmbGalaxyLocation;
    private SmoothLabel lblGalaxyLocation;
    private RaceDropDown cmbRace;
    private SmoothLabel lblRace;
    private EmpireDropDown cmbEmpire;
    private SmoothLabel lblEmpire;
    private CreatureTypeDropDown cmbCreatureType;
    private SmoothLabel lblCreatureType;
    private ResearchNodeDefinitionDropDown cmbResearchProject;
    private SmoothLabel lblResearchProject;
    private GovernmentStyleDropDown cmbGovernmentStyle;
    private SmoothLabel lblGovernmentType;
    private PlanetaryFacilityDefinitionDropDown cmbPlanetaryFacilityDefinition;
    private SmoothLabel lblPlanetaryFacility;
    private SmoothLabel lblActionTarget;
    private SmoothLabel lblDescription;
    private GlassButton btnClose;
    private GlassButton btnDelete;
    private GlassButton btnGoto;
    private RaceDropDown cmbRaceOther;
    private SmoothLabel lblRaceOther;
    private EventActionExecutionTypeDropDown cmbEventActionExecutionType;
    private SmoothLabel lblEventActionExecutionType;
    private NumericUpDown numDelayStart;
    private NumericUpDown numDelayEnd;
    private SmoothLabel lblDelayStart;
    private SmoothLabel lblDelayEnd;
    private EmpireDropDown cmbEmpire2;
    private DiplomaticRelationTypeActualDropDown cmbDiplomaticRelationType;
    private TextBox txtFilename;
    private TextBox txtMessageText;
    private SmoothLabel lblDiplomaticRelationType;
    private SmoothLabel lblEmpire2;
    private SmoothLabel lblFilename;
    private SmoothLabel lblMessageText;
    private CheckBox chkLockedTreaty;
    private TextBox txtMessageTitle;
    private SmoothLabel lblMessageTitle;
    private SmoothLabel lblMessageExplanation;
    private SmoothLabel lblAllianceName;
    private TextBox txtAllianceName;
    private CharacterDropDown cmbCharacter;
    private SmoothLabel lblCharacter;
    private SmoothLabel lblCharacter2;
    private CharacterDropDown cmbCharacter2;
    private EventAction _EventAction;
    private GameEvent _GameEvent;
    private Galaxy _Galaxy;
    private Font _NormalFont;
    private Font _BoldFont;
    private Font _TitleFont;
    private CharacterImageCache _CharacterImageCache;
    private bool _Binding;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.numValue = new NumericUpDown();
      this.numDelayStart = new NumericUpDown();
      this.numDelayEnd = new NumericUpDown();
      this.txtFilename = new TextBox();
      this.txtMessageText = new TextBox();
      this.chkLockedTreaty = new CheckBox();
      this.lblMessageText = new SmoothLabel();
      this.lblFilename = new SmoothLabel();
      this.lblEmpire2 = new SmoothLabel();
      this.lblDiplomaticRelationType = new SmoothLabel();
      this.cmbDiplomaticRelationType = new DiplomaticRelationTypeActualDropDown();
      this.cmbEmpire2 = new EmpireDropDown();
      this.lblDelayEnd = new SmoothLabel();
      this.lblDelayStart = new SmoothLabel();
      this.lblEventActionExecutionType = new SmoothLabel();
      this.cmbEventActionExecutionType = new EventActionExecutionTypeDropDown();
      this.lblRaceOther = new SmoothLabel();
      this.cmbRaceOther = new RaceDropDown();
      this.btnGoto = new GlassButton();
      this.btnDelete = new GlassButton();
      this.btnClose = new GlassButton();
      this.lblDescription = new SmoothLabel();
      this.lblActionTarget = new SmoothLabel();
      this.lblPlanetaryFacility = new SmoothLabel();
      this.cmbPlanetaryFacilityDefinition = new PlanetaryFacilityDefinitionDropDown();
      this.lblGovernmentType = new SmoothLabel();
      this.cmbGovernmentStyle = new GovernmentStyleDropDown();
      this.lblResearchProject = new SmoothLabel();
      this.cmbResearchProject = new ResearchNodeDefinitionDropDown();
      this.lblCreatureType = new SmoothLabel();
      this.cmbCreatureType = new CreatureTypeDropDown();
      this.lblEmpire = new SmoothLabel();
      this.cmbEmpire = new EmpireDropDown();
      this.lblRace = new SmoothLabel();
      this.cmbRace = new RaceDropDown();
      this.lblGalaxyLocation = new SmoothLabel();
      this.cmbGalaxyLocation = new GalaxyLocationDropDown();
      this.lblValue = new SmoothLabel();
      this.lblBuiltObjectSubRole = new SmoothLabel();
      this.cmbBuiltObjectSubRole = new BuiltObjectSubRoleDropDown();
      this.lblResource = new SmoothLabel();
      this.lblTriggerObjectName = new SmoothLabel();
      this.lblEventActionType = new SmoothLabel();
      this.cmbResource = new ResourceDropDown();
      this.cmbEventActionType = new EventActionTypeDropDown();
      this.txtMessageTitle = new TextBox();
      this.lblMessageTitle = new SmoothLabel();
      this.lblMessageExplanation = new SmoothLabel();
      this.lblAllianceName = new SmoothLabel();
      this.txtAllianceName = new TextBox();
      this.cmbCharacter = new CharacterDropDown();
      this.lblCharacter = new SmoothLabel();
      this.lblCharacter2 = new SmoothLabel();
      this.cmbCharacter2 = new CharacterDropDown();
      this.numValue.BeginInit();
      this.numDelayStart.BeginInit();
      this.numDelayEnd.BeginInit();
      this.SuspendLayout();
      this.numValue.BackColor = Color.FromArgb(48, 48, 64);
      this.numValue.BorderStyle = BorderStyle.FixedSingle;
      this.numValue.ForeColor = Color.FromArgb(170, 170, 170);
      this.numValue.Location = new Point(78, 265);
      this.numValue.Name = "numValue";
      this.numValue.Size = new Size(120, 20);
      this.numValue.TabIndex = 7;
      this.numValue.ValueChanged += new EventHandler(this.numValue_ValueChanged);
      this.numDelayStart.BackColor = Color.FromArgb(48, 48, 64);
      this.numDelayStart.BorderStyle = BorderStyle.FixedSingle;
      this.numDelayStart.ForeColor = Color.FromArgb(170, 170, 170);
      this.numDelayStart.Location = new Point(294, 47);
      this.numDelayStart.Name = "numDelayStart";
      this.numDelayStart.Size = new Size(45, 20);
      this.numDelayStart.TabIndex = 32;
      this.numDelayStart.ValueChanged += new EventHandler(this.numDelayStart_ValueChanged);
      this.numDelayEnd.BackColor = Color.FromArgb(48, 48, 64);
      this.numDelayEnd.BorderStyle = BorderStyle.FixedSingle;
      this.numDelayEnd.ForeColor = Color.FromArgb(170, 170, 170);
      this.numDelayEnd.Location = new Point(398, 49);
      this.numDelayEnd.Name = "numDelayEnd";
      this.numDelayEnd.Size = new Size(50, 20);
      this.numDelayEnd.TabIndex = 33;
      this.numDelayEnd.ValueChanged += new EventHandler(this.numDelayEnd_ValueChanged);
      this.txtFilename.BackColor = Color.FromArgb(48, 48, 64);
      this.txtFilename.BorderStyle = BorderStyle.FixedSingle;
      this.txtFilename.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtFilename.Location = new Point(304, 124);
      this.txtFilename.Name = "txtFilename";
      this.txtFilename.Size = new Size(100, 20);
      this.txtFilename.TabIndex = 38;
      this.txtMessageText.BackColor = Color.FromArgb(48, 48, 64);
      this.txtMessageText.BorderStyle = BorderStyle.FixedSingle;
      this.txtMessageText.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtMessageText.Location = new Point(304, 174);
      this.txtMessageText.Multiline = true;
      this.txtMessageText.Name = "txtMessageText";
      this.txtMessageText.Size = new Size(100, 40);
      this.txtMessageText.TabIndex = 39;
      this.chkLockedTreaty.AutoSize = true;
      this.chkLockedTreaty.ForeColor = Color.FromArgb(170, 170, 170);
      this.chkLockedTreaty.Location = new Point(430, 267);
      this.chkLockedTreaty.Name = "chkLockedTreaty";
      this.chkLockedTreaty.Size = new Size(62, 17);
      this.chkLockedTreaty.TabIndex = 44;
      this.chkLockedTreaty.Text = "Locked";
      this.chkLockedTreaty.UseVisualStyleBackColor = true;
      this.lblMessageText.AutoSize = true;
      this.lblMessageText.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMessageText.Location = new Point(260, 176);
      this.lblMessageText.Name = "lblMessageText";
      this.lblMessageText.Size = new Size(74, 13);
      this.lblMessageText.TabIndex = 43;
      this.lblMessageText.Text = "Message Text";
      this.lblFilename.AutoSize = true;
      this.lblFilename.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblFilename.Location = new Point(249, 126);
      this.lblFilename.Name = "lblFilename";
      this.lblFilename.Size = new Size(97, 13);
      this.lblFilename.TabIndex = 42;
      this.lblFilename.Text = "Background Image";
      this.lblEmpire2.AutoSize = true;
      this.lblEmpire2.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblEmpire2.Location = new Point(253, 402);
      this.lblEmpire2.Name = "lblEmpire2";
      this.lblEmpire2.Size = new Size(68, 13);
      this.lblEmpire2.TabIndex = 41;
      this.lblEmpire2.Text = "Other Empire";
      this.lblDiplomaticRelationType.AutoSize = true;
      this.lblDiplomaticRelationType.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblDiplomaticRelationType.Location = new Point(251, 266);
      this.lblDiplomaticRelationType.Name = "lblDiplomaticRelationType";
      this.lblDiplomaticRelationType.Size = new Size(37, 13);
      this.lblDiplomaticRelationType.TabIndex = 40;
      this.lblDiplomaticRelationType.Text = "Treaty";
      this.cmbDiplomaticRelationType.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbDiplomaticRelationType.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbDiplomaticRelationType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbDiplomaticRelationType.FlatStyle = FlatStyle.Popup;
      this.cmbDiplomaticRelationType.Font = new Font("Verdana", 8.25f);
      this.cmbDiplomaticRelationType.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbDiplomaticRelationType.FormattingEnabled = true;
      this.cmbDiplomaticRelationType.Location = new Point(294, 263);
      this.cmbDiplomaticRelationType.Name = "cmbDiplomaticRelationType";
      this.cmbDiplomaticRelationType.Size = new Size(121, 22);
      this.cmbDiplomaticRelationType.TabIndex = 37;
      this.cmbDiplomaticRelationType.SelectedIndexChanged += new EventHandler(this.cmbDiplomaticRelationType_SelectedIndexChanged);
      this.cmbEmpire2.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbEmpire2.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbEmpire2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbEmpire2.FlatStyle = FlatStyle.Popup;
      this.cmbEmpire2.Font = new Font("Verdana", 8.25f);
      this.cmbEmpire2.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbEmpire2.FormattingEnabled = true;
      this.cmbEmpire2.Location = new Point(327, 399);
      this.cmbEmpire2.Name = "cmbEmpire2";
      this.cmbEmpire2.Size = new Size(121, 22);
      this.cmbEmpire2.TabIndex = 36;
      this.cmbEmpire2.SelectedIndexChanged += new EventHandler(this.cmbEmpire2_SelectedIndexChanged);
      this.lblDelayEnd.AutoSize = true;
      this.lblDelayEnd.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblDelayEnd.Location = new Point(376, 51);
      this.lblDelayEnd.Name = "lblDelayEnd";
      this.lblDelayEnd.Size = new Size(16, 13);
      this.lblDelayEnd.TabIndex = 35;
      this.lblDelayEnd.Text = "to";
      this.lblDelayStart.AutoSize = true;
      this.lblDelayStart.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblDelayStart.Location = new Point(216, 51);
      this.lblDelayStart.Name = "lblDelayStart";
      this.lblDelayStart.Size = new Size(72, 13);
      this.lblDelayStart.TabIndex = 34;
      this.lblDelayStart.Text = "Delay in Days";
      this.lblEventActionExecutionType.AutoSize = true;
      this.lblEventActionExecutionType.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblEventActionExecutionType.Location = new Point(3, 50);
      this.lblEventActionExecutionType.Name = "lblEventActionExecutionType";
      this.lblEventActionExecutionType.Size = new Size(78, 13);
      this.lblEventActionExecutionType.TabIndex = 31;
      this.lblEventActionExecutionType.Text = "When Execute";
      this.cmbEventActionExecutionType.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbEventActionExecutionType.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbEventActionExecutionType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbEventActionExecutionType.FlatStyle = FlatStyle.Popup;
      this.cmbEventActionExecutionType.Font = new Font("Verdana", 8.25f);
      this.cmbEventActionExecutionType.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbEventActionExecutionType.FormattingEnabled = true;
      this.cmbEventActionExecutionType.Location = new Point(77, 47);
      this.cmbEventActionExecutionType.Name = "cmbEventActionExecutionType";
      this.cmbEventActionExecutionType.Size = new Size(121, 22);
      this.cmbEventActionExecutionType.TabIndex = 30;
      this.cmbEventActionExecutionType.SelectedIndexChanged += new EventHandler(this.cmbEventActionExecutionType_SelectedIndexChanged);
      this.lblRaceOther.AutoSize = true;
      this.lblRaceOther.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblRaceOther.Location = new Point(203, 358);
      this.lblRaceOther.Name = "lblRaceOther";
      this.lblRaceOther.Size = new Size(33, 13);
      this.lblRaceOther.TabIndex = 29;
      this.lblRaceOther.Text = "Race";
      this.cmbRaceOther.BackColor = Color.FromArgb(22, 21, 26);
      this.cmbRaceOther.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbRaceOther.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbRaceOther.FlatStyle = FlatStyle.Popup;
      this.cmbRaceOther.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbRaceOther.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbRaceOther.FormattingEnabled = true;
      this.cmbRaceOther.Location = new Point(242, 350);
      this.cmbRaceOther.Name = "cmbRaceOther";
      this.cmbRaceOther.Size = new Size(121, 21);
      this.cmbRaceOther.TabIndex = 28;
      this.cmbRaceOther.SelectedIndexChanged += new EventHandler(this.cmbRaceOther_SelectedIndexChanged);
      this.btnGoto.BackColor = Color.FromArgb(0, 0, 0);
      this.btnGoto.ClipBackground = false;
      this.btnGoto.DelayFrameRefresh = false;
      this.btnGoto.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnGoto.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnGoto.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnGoto.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnGoto.IntensifyColors = false;
      this.btnGoto.Location = new Point(177, 614);
      this.btnGoto.Name = "btnGoto";
      this.btnGoto.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnGoto.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnGoto.Size = new Size(75, 25);
      this.btnGoto.TabIndex = 27;
      this.btnGoto.Text = "Go to Target";
      this.btnGoto.TextColor = Color.FromArgb(120, 120, 120);
      this.btnGoto.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnGoto.ToggledOn = false;
      this.btnGoto.Click += new EventHandler(this.btnGoto_Click);
      this.btnDelete.BackColor = Color.FromArgb(0, 0, 0);
      this.btnDelete.ClipBackground = false;
      this.btnDelete.DelayFrameRefresh = false;
      this.btnDelete.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnDelete.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnDelete.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnDelete.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnDelete.IntensifyColors = false;
      this.btnDelete.Location = new Point(86, 614);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnDelete.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnDelete.Size = new Size(75, 25);
      this.btnDelete.TabIndex = 26;
      this.btnDelete.Text = "Delete";
      this.btnDelete.TextColor = Color.FromArgb(120, 120, 120);
      this.btnDelete.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnDelete.ToggledOn = false;
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this.btnClose.BackColor = Color.FromArgb(0, 0, 0);
      this.btnClose.ClipBackground = false;
      this.btnClose.DelayFrameRefresh = false;
      this.btnClose.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnClose.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnClose.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnClose.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnClose.IntensifyColors = false;
      this.btnClose.Location = new Point(285, 10);
      this.btnClose.Name = "btnClose";
      this.btnClose.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnClose.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnClose.Size = new Size(75, 25);
      this.btnClose.TabIndex = 25;
      this.btnClose.Text = "Close";
      this.btnClose.TextColor = Color.FromArgb(120, 120, 120);
      this.btnClose.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnClose.ToggledOn = false;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.lblDescription.AutoSize = true;
      this.lblDescription.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblDescription.Location = new Point(75, 150);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new Size(60, 13);
      this.lblDescription.TabIndex = 24;
      this.lblDescription.Text = "Description";
      this.lblActionTarget.AutoSize = true;
      this.lblActionTarget.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblActionTarget.Location = new Point(75, 124);
      this.lblActionTarget.Name = "lblActionTarget";
      this.lblActionTarget.Size = new Size(71, 13);
      this.lblActionTarget.TabIndex = 23;
      this.lblActionTarget.Text = "Action Target";
      this.lblPlanetaryFacility.AutoSize = true;
      this.lblPlanetaryFacility.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblPlanetaryFacility.Location = new Point(16, 577);
      this.lblPlanetaryFacility.Name = "lblPlanetaryFacility";
      this.lblPlanetaryFacility.Size = new Size(86, 13);
      this.lblPlanetaryFacility.TabIndex = 22;
      this.lblPlanetaryFacility.Text = "Planetary Facility";
      this.cmbPlanetaryFacilityDefinition.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbPlanetaryFacilityDefinition.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPlanetaryFacilityDefinition.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbPlanetaryFacilityDefinition.FlatStyle = FlatStyle.Popup;
      this.cmbPlanetaryFacilityDefinition.Font = new Font("Verdana", 8.25f);
      this.cmbPlanetaryFacilityDefinition.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbPlanetaryFacilityDefinition.FormattingEnabled = true;
      this.cmbPlanetaryFacilityDefinition.Location = new Point(78, 574);
      this.cmbPlanetaryFacilityDefinition.Name = "cmbPlanetaryFacilityDefinition";
      this.cmbPlanetaryFacilityDefinition.Size = new Size(121, 22);
      this.cmbPlanetaryFacilityDefinition.TabIndex = 21;
      this.cmbPlanetaryFacilityDefinition.SelectedIndexChanged += new EventHandler(this.cmbPlanetaryFacilityDefinition_SelectedIndexChanged);
      this.lblGovernmentType.AutoSize = true;
      this.lblGovernmentType.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblGovernmentType.Location = new Point(16, 531);
      this.lblGovernmentType.Name = "lblGovernmentType";
      this.lblGovernmentType.Size = new Size(92, 13);
      this.lblGovernmentType.TabIndex = 20;
      this.lblGovernmentType.Text = "Government Type";
      this.cmbGovernmentStyle.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbGovernmentStyle.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbGovernmentStyle.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbGovernmentStyle.FlatStyle = FlatStyle.Popup;
      this.cmbGovernmentStyle.Font = new Font("Verdana", 8.25f);
      this.cmbGovernmentStyle.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbGovernmentStyle.FormattingEnabled = true;
      this.cmbGovernmentStyle.Location = new Point(77, 528);
      this.cmbGovernmentStyle.Name = "cmbGovernmentStyle";
      this.cmbGovernmentStyle.Size = new Size(121, 22);
      this.cmbGovernmentStyle.TabIndex = 19;
      this.cmbGovernmentStyle.SelectedIndexChanged += new EventHandler(this.cmbGovernmentStyle_SelectedIndexChanged);
      this.lblResearchProject.AutoSize = true;
      this.lblResearchProject.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblResearchProject.Location = new Point(2, 491);
      this.lblResearchProject.Name = "lblResearchProject";
      this.lblResearchProject.Size = new Size(89, 13);
      this.lblResearchProject.TabIndex = 18;
      this.lblResearchProject.Text = "Research Project";
      this.cmbResearchProject.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbResearchProject.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbResearchProject.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbResearchProject.FlatStyle = FlatStyle.Popup;
      this.cmbResearchProject.Font = new Font("Verdana", 8.25f);
      this.cmbResearchProject.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbResearchProject.FormattingEnabled = true;
      this.cmbResearchProject.Location = new Point(97, 487);
      this.cmbResearchProject.Name = "cmbResearchProject";
      this.cmbResearchProject.Size = new Size(121, 22);
      this.cmbResearchProject.TabIndex = 17;
      this.cmbResearchProject.SelectedIndexChanged += new EventHandler(this.cmbResearchProject_SelectedIndexChanged);
      this.lblCreatureType.AutoSize = true;
      this.lblCreatureType.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblCreatureType.Location = new Point(16, 445);
      this.lblCreatureType.Name = "lblCreatureType";
      this.lblCreatureType.Size = new Size(74, 13);
      this.lblCreatureType.TabIndex = 16;
      this.lblCreatureType.Text = "Creature Type";
      this.cmbCreatureType.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbCreatureType.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbCreatureType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCreatureType.FlatStyle = FlatStyle.Popup;
      this.cmbCreatureType.Font = new Font("Verdana", 8.25f);
      this.cmbCreatureType.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbCreatureType.FormattingEnabled = true;
      this.cmbCreatureType.Location = new Point(78, 442);
      this.cmbCreatureType.Name = "cmbCreatureType";
      this.cmbCreatureType.Size = new Size(121, 22);
      this.cmbCreatureType.TabIndex = 15;
      this.cmbCreatureType.SelectedIndexChanged += new EventHandler(this.cmbCreatureType_SelectedIndexChanged);
      this.lblEmpire.AutoSize = true;
      this.lblEmpire.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblEmpire.Location = new Point(12, 402);
      this.lblEmpire.Name = "lblEmpire";
      this.lblEmpire.Size = new Size(39, 13);
      this.lblEmpire.TabIndex = 14;
      this.lblEmpire.Text = "Empire";
      this.cmbEmpire.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbEmpire.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbEmpire.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbEmpire.FlatStyle = FlatStyle.Popup;
      this.cmbEmpire.Font = new Font("Verdana", 8.25f);
      this.cmbEmpire.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbEmpire.FormattingEnabled = true;
      this.cmbEmpire.Location = new Point(77, 399);
      this.cmbEmpire.Name = "cmbEmpire";
      this.cmbEmpire.Size = new Size(121, 22);
      this.cmbEmpire.TabIndex = 13;
      this.cmbEmpire.SelectedIndexChanged += new EventHandler(this.cmbEmpire_SelectedIndexChanged);
      this.lblRace.AutoSize = true;
      this.lblRace.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblRace.Location = new Point(12, 353);
      this.lblRace.Name = "lblRace";
      this.lblRace.Size = new Size(33, 13);
      this.lblRace.TabIndex = 12;
      this.lblRace.Text = "Race";
      this.cmbRace.BackColor = Color.FromArgb(22, 21, 26);
      this.cmbRace.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbRace.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbRace.FlatStyle = FlatStyle.Popup;
      this.cmbRace.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbRace.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbRace.FormattingEnabled = true;
      this.cmbRace.Location = new Point(77, 350);
      this.cmbRace.Name = "cmbRace";
      this.cmbRace.Size = new Size(121, 21);
      this.cmbRace.TabIndex = 11;
      this.cmbRace.SelectedIndexChanged += new EventHandler(this.cmbRace_SelectedIndexChanged);
      this.lblGalaxyLocation.AutoSize = true;
      this.lblGalaxyLocation.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblGalaxyLocation.Location = new Point(12, 315);
      this.lblGalaxyLocation.Name = "lblGalaxyLocation";
      this.lblGalaxyLocation.Size = new Size(86, 13);
      this.lblGalaxyLocation.TabIndex = 10;
      this.lblGalaxyLocation.Text = "Special Location";
      this.cmbGalaxyLocation.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbGalaxyLocation.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbGalaxyLocation.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbGalaxyLocation.FlatStyle = FlatStyle.Popup;
      this.cmbGalaxyLocation.Font = new Font("Verdana", 8.25f);
      this.cmbGalaxyLocation.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbGalaxyLocation.FormattingEnabled = true;
      this.cmbGalaxyLocation.Location = new Point(77, 310);
      this.cmbGalaxyLocation.Name = "cmbGalaxyLocation";
      this.cmbGalaxyLocation.Size = new Size(121, 22);
      this.cmbGalaxyLocation.TabIndex = 9;
      this.cmbGalaxyLocation.SelectedIndexChanged += new EventHandler(this.cmbGalaxyLocation_SelectedIndexChanged);
      this.lblValue.AutoSize = true;
      this.lblValue.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblValue.Location = new Point(12, 267);
      this.lblValue.Name = "lblValue";
      this.lblValue.Size = new Size(34, 13);
      this.lblValue.TabIndex = 8;
      this.lblValue.Text = "Value";
      this.lblBuiltObjectSubRole.AutoSize = true;
      this.lblBuiltObjectSubRole.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblBuiltObjectSubRole.Location = new Point(7, 229);
      this.lblBuiltObjectSubRole.Name = "lblBuiltObjectSubRole";
      this.lblBuiltObjectSubRole.Size = new Size(48, 13);
      this.lblBuiltObjectSubRole.TabIndex = 6;
      this.lblBuiltObjectSubRole.Text = "SubRole";
      this.cmbBuiltObjectSubRole.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbBuiltObjectSubRole.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbBuiltObjectSubRole.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbBuiltObjectSubRole.FlatStyle = FlatStyle.Popup;
      this.cmbBuiltObjectSubRole.Font = new Font("Verdana", 8.25f);
      this.cmbBuiltObjectSubRole.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbBuiltObjectSubRole.FormattingEnabled = true;
      this.cmbBuiltObjectSubRole.Location = new Point(77, 226);
      this.cmbBuiltObjectSubRole.Name = "cmbBuiltObjectSubRole";
      this.cmbBuiltObjectSubRole.Size = new Size(121, 22);
      this.cmbBuiltObjectSubRole.TabIndex = 5;
      this.cmbBuiltObjectSubRole.SelectedIndexChanged += new EventHandler(this.cmbBuiltObjectSubRole_SelectedIndexChanged);
      this.lblResource.AutoSize = true;
      this.lblResource.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblResource.Location = new Point(7, 187);
      this.lblResource.Name = "lblResource";
      this.lblResource.Size = new Size(53, 13);
      this.lblResource.TabIndex = 4;
      this.lblResource.Text = "Resource";
      this.lblTriggerObjectName.AutoSize = true;
      this.lblTriggerObjectName.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTriggerObjectName.Location = new Point(15, 13);
      this.lblTriggerObjectName.Name = "lblTriggerObjectName";
      this.lblTriggerObjectName.Size = new Size(105, 13);
      this.lblTriggerObjectName.TabIndex = 3;
      this.lblTriggerObjectName.Text = "Trigger Object Name";
      this.lblEventActionType.AutoSize = true;
      this.lblEventActionType.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblEventActionType.Location = new Point(7, 92);
      this.lblEventActionType.Name = "lblEventActionType";
      this.lblEventActionType.Size = new Size(64, 13);
      this.lblEventActionType.TabIndex = 2;
      this.lblEventActionType.Text = "Action Type";
      this.cmbResource.AllowNullResourceText = "(KEY NOT FOUND: 'All Resources')";
      this.cmbResource.BackColor = Color.FromArgb(22, 21, 26);
      this.cmbResource.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbResource.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbResource.FlatStyle = FlatStyle.Popup;
      this.cmbResource.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbResource.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbResource.FormattingEnabled = true;
      this.cmbResource.Location = new Point(77, 184);
      this.cmbResource.Name = "cmbResource";
      this.cmbResource.Size = new Size(121, 21);
      this.cmbResource.TabIndex = 1;
      this.cmbResource.SelectedIndexChanged += new EventHandler(this.cmbResource_SelectedIndexChanged);
      this.cmbEventActionType.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbEventActionType.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbEventActionType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbEventActionType.FlatStyle = FlatStyle.Popup;
      this.cmbEventActionType.Font = new Font("Verdana", 8.25f);
      this.cmbEventActionType.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbEventActionType.FormattingEnabled = true;
      this.cmbEventActionType.Location = new Point(77, 89);
      this.cmbEventActionType.Name = "cmbEventActionType";
      this.cmbEventActionType.Size = new Size(121, 22);
      this.cmbEventActionType.TabIndex = 0;
      this.cmbEventActionType.SelectedIndexChanged += new EventHandler(this.cmbEventActionType_SelectedIndexChanged);
      this.txtMessageTitle.BackColor = Color.FromArgb(48, 48, 64);
      this.txtMessageTitle.BorderStyle = BorderStyle.FixedSingle;
      this.txtMessageTitle.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtMessageTitle.Location = new Point(304, 148);
      this.txtMessageTitle.Name = "txtMessageTitle";
      this.txtMessageTitle.Size = new Size(100, 20);
      this.txtMessageTitle.TabIndex = 45;
      this.lblMessageTitle.AutoSize = true;
      this.lblMessageTitle.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMessageTitle.Location = new Point(249, 150);
      this.lblMessageTitle.Name = "lblMessageTitle";
      this.lblMessageTitle.Size = new Size(73, 13);
      this.lblMessageTitle.TabIndex = 46;
      this.lblMessageTitle.Text = "Message Title";
      this.lblMessageExplanation.AutoSize = true;
      this.lblMessageExplanation.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMessageExplanation.Location = new Point(282, 98);
      this.lblMessageExplanation.Name = "lblMessageExplanation";
      this.lblMessageExplanation.Size = new Size(108, 13);
      this.lblMessageExplanation.TabIndex = 47;
      this.lblMessageExplanation.Text = "Message Explanation";
      this.lblAllianceName.AutoSize = true;
      this.lblAllianceName.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblAllianceName.Location = new Point(249, 222);
      this.lblAllianceName.Name = "lblAllianceName";
      this.lblAllianceName.Size = new Size(75, 13);
      this.lblAllianceName.TabIndex = 49;
      this.lblAllianceName.Text = "Alliance Name";
      this.txtAllianceName.BackColor = Color.FromArgb(48, 48, 64);
      this.txtAllianceName.BorderStyle = BorderStyle.FixedSingle;
      this.txtAllianceName.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtAllianceName.Location = new Point(304, 220);
      this.txtAllianceName.Name = "txtAllianceName";
      this.txtAllianceName.Size = new Size(100, 20);
      this.txtAllianceName.TabIndex = 48;
      this.cmbCharacter.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbCharacter.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbCharacter.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCharacter.FlatStyle = FlatStyle.Popup;
      this.cmbCharacter.Font = new Font("Verdana", 8.25f);
      this.cmbCharacter.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbCharacter.FormattingEnabled = true;
      this.cmbCharacter.Location = new Point(379, 487);
      this.cmbCharacter.Name = "cmbCharacter";
      this.cmbCharacter.Size = new Size(121, 22);
      this.cmbCharacter.TabIndex = 50;
      this.cmbCharacter.SelectedIndexChanged += new EventHandler(this.cmbCharacter_SelectedIndexChanged);
      this.lblCharacter.AutoSize = true;
      this.lblCharacter.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblCharacter.Location = new Point(301, 490);
      this.lblCharacter.Name = "lblCharacter";
      this.lblCharacter.Size = new Size(53, 13);
      this.lblCharacter.TabIndex = 51;
      this.lblCharacter.Text = "Character";
      this.lblCharacter2.AutoSize = true;
      this.lblCharacter2.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblCharacter2.Location = new Point(301, 525);
      this.lblCharacter2.Name = "lblCharacter2";
      this.lblCharacter2.Size = new Size(78, 13);
      this.lblCharacter2.TabIndex = 53;
      this.lblCharacter2.Text = "Character Role";
      this.cmbCharacter2.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbCharacter2.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbCharacter2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCharacter2.FlatStyle = FlatStyle.Popup;
      this.cmbCharacter2.Font = new Font("Verdana", 8.25f);
      this.cmbCharacter2.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbCharacter2.FormattingEnabled = true;
      this.cmbCharacter2.Location = new Point(379, 522);
      this.cmbCharacter2.Name = "cmbCharacter2";
      this.cmbCharacter2.Size = new Size(121, 22);
      this.cmbCharacter2.TabIndex = 52;
      this.cmbCharacter2.SelectedIndexChanged += new EventHandler(this.cmbCharacter2_SelectedIndexChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(24, 24, 32);
      this.Controls.Add((Control) this.lblCharacter2);
      this.Controls.Add((Control) this.cmbCharacter2);
      this.Controls.Add((Control) this.lblCharacter);
      this.Controls.Add((Control) this.cmbCharacter);
      this.Controls.Add((Control) this.lblAllianceName);
      this.Controls.Add((Control) this.txtAllianceName);
      this.Controls.Add((Control) this.lblMessageExplanation);
      this.Controls.Add((Control) this.lblMessageTitle);
      this.Controls.Add((Control) this.txtMessageTitle);
      this.Controls.Add((Control) this.chkLockedTreaty);
      this.Controls.Add((Control) this.lblMessageText);
      this.Controls.Add((Control) this.lblFilename);
      this.Controls.Add((Control) this.lblEmpire2);
      this.Controls.Add((Control) this.lblDiplomaticRelationType);
      this.Controls.Add((Control) this.txtMessageText);
      this.Controls.Add((Control) this.txtFilename);
      this.Controls.Add((Control) this.cmbDiplomaticRelationType);
      this.Controls.Add((Control) this.cmbEmpire2);
      this.Controls.Add((Control) this.lblDelayEnd);
      this.Controls.Add((Control) this.lblDelayStart);
      this.Controls.Add((Control) this.numDelayEnd);
      this.Controls.Add((Control) this.numDelayStart);
      this.Controls.Add((Control) this.lblEventActionExecutionType);
      this.Controls.Add((Control) this.cmbEventActionExecutionType);
      this.Controls.Add((Control) this.lblRaceOther);
      this.Controls.Add((Control) this.cmbRaceOther);
      this.Controls.Add((Control) this.btnGoto);
      this.Controls.Add((Control) this.btnDelete);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.lblDescription);
      this.Controls.Add((Control) this.lblActionTarget);
      this.Controls.Add((Control) this.lblPlanetaryFacility);
      this.Controls.Add((Control) this.cmbPlanetaryFacilityDefinition);
      this.Controls.Add((Control) this.lblGovernmentType);
      this.Controls.Add((Control) this.cmbGovernmentStyle);
      this.Controls.Add((Control) this.lblResearchProject);
      this.Controls.Add((Control) this.cmbResearchProject);
      this.Controls.Add((Control) this.lblCreatureType);
      this.Controls.Add((Control) this.cmbCreatureType);
      this.Controls.Add((Control) this.lblEmpire);
      this.Controls.Add((Control) this.cmbEmpire);
      this.Controls.Add((Control) this.lblRace);
      this.Controls.Add((Control) this.cmbRace);
      this.Controls.Add((Control) this.lblGalaxyLocation);
      this.Controls.Add((Control) this.cmbGalaxyLocation);
      this.Controls.Add((Control) this.lblValue);
      this.Controls.Add((Control) this.numValue);
      this.Controls.Add((Control) this.lblBuiltObjectSubRole);
      this.Controls.Add((Control) this.cmbBuiltObjectSubRole);
      this.Controls.Add((Control) this.lblResource);
      this.Controls.Add((Control) this.lblTriggerObjectName);
      this.Controls.Add((Control) this.lblEventActionType);
      this.Controls.Add((Control) this.cmbResource);
      this.Controls.Add((Control) this.cmbEventActionType);
      this.Name = nameof (EventActionPanel);
      this.Size = new Size(513, 675);
      this.numValue.EndInit();
      this.numDelayStart.EndInit();
      this.numDelayEnd.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public event EventHandler PanelClosed;

    public event EventHandler ActionDeleted;

    public event EventHandler GotoTarget;

    public EventActionPanel() => this.InitializeComponent();

    public void ClearData()
    {
      this._EventAction = (EventAction) null;
      this._GameEvent = (GameEvent) null;
      this._Galaxy = (Galaxy) null;
    }

    private void BindSourceValues(
      Galaxy galaxy,
      Bitmap[] planetaryFacilityImages,
      Bitmap[] raceImages,
      Bitmap[] resourceImages,
      CharacterImageCache characterImageCache)
    {
      this.cmbEventActionExecutionType.BindData(new List<EventActionExecutionType>()
      {
        EventActionExecutionType.Immediately,
        EventActionExecutionType.Delay,
        EventActionExecutionType.RandomDelay
      });
      this.cmbBuiltObjectSubRole.BindData(new List<BuiltObjectSubRole>()
      {
        BuiltObjectSubRole.CapitalShip,
        BuiltObjectSubRole.Carrier,
        BuiltObjectSubRole.ColonyShip,
        BuiltObjectSubRole.ConstructionShip,
        BuiltObjectSubRole.Cruiser,
        BuiltObjectSubRole.DefensiveBase,
        BuiltObjectSubRole.Destroyer,
        BuiltObjectSubRole.EnergyResearchStation,
        BuiltObjectSubRole.Escort,
        BuiltObjectSubRole.ExplorationShip,
        BuiltObjectSubRole.Frigate,
        BuiltObjectSubRole.GasMiningShip,
        BuiltObjectSubRole.GasMiningStation,
        BuiltObjectSubRole.HighTechResearchStation,
        BuiltObjectSubRole.LargeFreighter,
        BuiltObjectSubRole.LargeSpacePort,
        BuiltObjectSubRole.MediumFreighter,
        BuiltObjectSubRole.MediumSpacePort,
        BuiltObjectSubRole.MiningShip,
        BuiltObjectSubRole.MiningStation,
        BuiltObjectSubRole.MonitoringStation,
        BuiltObjectSubRole.PassengerShip,
        BuiltObjectSubRole.ResortBase,
        BuiltObjectSubRole.ResupplyShip,
        BuiltObjectSubRole.SmallFreighter,
        BuiltObjectSubRole.SmallSpacePort,
        BuiltObjectSubRole.TroopTransport,
        BuiltObjectSubRole.WeaponsResearchStation
      }, false);
      this.cmbCreatureType.BindData(new List<CreatureType>()
      {
        CreatureType.Ardilus,
        CreatureType.Kaltor,
        CreatureType.DesertSpaceSlug,
        CreatureType.RockSpaceSlug,
        CreatureType.SilverMist
      }, false);
      this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
      this.cmbGalaxyLocation.BindData(galaxy.GalaxyLocations.ResolveSpecialLocations(), false);
      this.cmbGovernmentStyle.IgniteAll();
      this.cmbPlanetaryFacilityDefinition.BindData((Empire) null, galaxy.PlanetaryFacilityDefinitions, planetaryFacilityImages, false);
      this.cmbRace.BindData(this._NormalFont, galaxy.Races, raceImages, false);
      this.cmbRaceOther.BindData(this._NormalFont, galaxy.Races, raceImages, false);
      this.cmbResearchProject.BindData(galaxy.ResearchNodeDefinitions, false);
      this.cmbResource.BindData(this._NormalFont, galaxy.ResourceSystem.Resources, resourceImages, false, false);
      this.numValue.Minimum = 1M;
      this.numValue.Maximum = 1000000M;
      this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
      this.cmbDiplomaticRelationType.BindData(new List<DiplomaticRelationType>()
      {
        DiplomaticRelationType.FreeTradeAgreement,
        DiplomaticRelationType.Protectorate,
        DiplomaticRelationType.MutualDefensePact
      });
    }

    private void BindEventActionControls(Galaxy galaxy)
    {
      this._Binding = true;
      this.txtMessageTitle.Text = this._EventAction.MessageTitle;
      this.txtMessageText.Text = this._EventAction.MessageText;
      this.txtFilename.Text = this._EventAction.ImageFilename;
      this.cmbEventActionExecutionType.SetSelectedEventActionExecutionType(this._EventAction.ExecutionType);
      if (this._EventAction.DelayDaysMinimum >= (short) 0)
        this.numDelayStart.Value = (Decimal) this._EventAction.DelayDaysMinimum;
      if (this._EventAction.DelayDaysMaximum >= (short) 0)
        this.numDelayEnd.Value = (Decimal) this._EventAction.DelayDaysMaximum;
      switch (this._EventAction.Type)
      {
        case EventActionType.FindMoneyTreasure:
          if (this._EventAction.MoneyAmount <= 0.0)
            this._EventAction.MoneyAmount = 1000.0;
          this.numValue.Value = (Decimal) (int) this._EventAction.MoneyAmount;
          break;
        case EventActionType.LearnExplorationInfo:
          if (this._EventAction.Value <= 0)
            this._EventAction.Value = 8;
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.LearnTech:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.ResearchNodeDefinitionsStatic.Count)
          {
            this.cmbResearchProject.SetSelectedResearchNode(Galaxy.ResearchNodeDefinitionsStatic[this._EventAction.Value]);
            break;
          }
          break;
        case EventActionType.UnlockTech:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.ResearchNodeDefinitionsStatic.Count)
          {
            this.cmbResearchProject.SetSelectedResearchNode(Galaxy.ResearchNodeDefinitionsStatic[this._EventAction.Value]);
            break;
          }
          break;
        case EventActionType.LearnGovernmentType:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.GovernmentsStatic.Count)
          {
            this.cmbGovernmentStyle.SetSelectedGovernmentStyle(this._EventAction.Value);
            break;
          }
          break;
        case EventActionType.LearnAboutSpecialLocation:
          if (this._EventAction.Location == null && galaxy.GalaxyLocations.ResolveSpecialLocations().Count > 0)
            this._EventAction.Location = galaxy.GalaxyLocations.ResolveSpecialLocations()[0];
          this.cmbGalaxyLocation.SetSelectedLocation(this._EventAction.Location);
          break;
        case EventActionType.LearnAboutLostColony:
          if (this._EventAction.Race == null && galaxy.Races.Count > 0)
            this._EventAction.Race = galaxy.Races[0];
          this.cmbRace.SetSelectedRace(this._EventAction.Race);
          if (this._EventAction.Value <= 0)
            this._EventAction.Value = 2000;
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.SleepingRaceAwokenAtHabitat:
          if (this._EventAction.Race == null && galaxy.Races.Count > 0)
            this._EventAction.Race = galaxy.Races[0];
          this.cmbRace.SetSelectedRace(this._EventAction.Race);
          if (this._EventAction.Value <= 0)
            this._EventAction.Value = 1000;
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.SplitEmpirePeacefully:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 1)
            this._EventAction.Empire = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          break;
        case EventActionType.SplitEmpireCivilWar:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 1)
            this._EventAction.Empire = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          break;
        case EventActionType.PirateFactionJoinsTriggerEmpire:
          if (this._EventAction.Empire == null && galaxy.PirateEmpires.Count > 1)
            this._EventAction.Empire = galaxy.PirateEmpires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, (DistantWorlds.Types.EmpireList) null, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          break;
        case EventActionType.EmpireDeclaresWarOnTriggerEmpire:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 1)
            this._EventAction.Empire = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          break;
        case EventActionType.ChangeEmpireGovernment:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          this.cmbGovernmentStyle.SetSelectedGovernmentStyle(this._EventAction.Value);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          break;
        case EventActionType.GenerateBuiltObject:
          if (this._EventAction.BuiltObjectSubRole == BuiltObjectSubRole.Undefined)
            this._EventAction.BuiltObjectSubRole = BuiltObjectSubRole.Frigate;
          this.cmbBuiltObjectSubRole.SetSelectedBuiltObjectSubRole(this._EventAction.BuiltObjectSubRole);
          if (this._EventAction.TechLevel <= 0)
            this._EventAction.TechLevel = 3;
          this.numValue.Value = (Decimal) this._EventAction.TechLevel;
          break;
        case EventActionType.GenerateCreatureSwarm:
          if (this._EventAction.CreatureType == CreatureType.Undefined)
            this._EventAction.CreatureType = CreatureType.Kaltor;
          this.cmbCreatureType.SetSelectedCreatureType(this._EventAction.CreatureType);
          if (this._EventAction.Value <= 0)
            this._EventAction.Value = 3;
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.GeneratePirateAmbush:
          if (this._EventAction.Value <= 0)
            this._EventAction.Value = 3;
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.GenerateRefugeeFleet:
          if (this._EventAction.Race == null && galaxy.Races.Count > 0)
            this._EventAction.Race = galaxy.Races[0];
          this.cmbRace.SetSelectedRace(this._EventAction.Race);
          break;
        case EventActionType.GenerateNewEmpire:
          if (this._EventAction.Race == null && galaxy.Races.Count > 0)
            this._EventAction.Race = galaxy.Races[0];
          this.cmbRace.SetSelectedRace(this._EventAction.Race);
          break;
        case EventActionType.GenerateNewPirateFaction:
          if (this._EventAction.Race == null && galaxy.Races.Count > 0)
            this._EventAction.Race = galaxy.Races[0];
          this.cmbRace.SetSelectedRace(this._EventAction.Race);
          break;
        case EventActionType.MakeEmpireContact:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 1)
            this._EventAction.Empire = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          break;
        case EventActionType.InterceptResource:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.ResourceSystemStatic.Resources.Count)
          {
            this.cmbResource.SetSelectedResource(new Resource((byte) this._EventAction.Value));
            break;
          }
          break;
        case EventActionType.GenerateResourceAtHabitat:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.ResourceSystemStatic.Resources.Count)
          {
            this.cmbResource.SetSelectedResource(new Resource((byte) this._EventAction.Value));
            break;
          }
          break;
        case EventActionType.RemoveResourceAtHabitat:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.ResourceSystemStatic.Resources.Count)
          {
            this.cmbResource.SetSelectedResource(new Resource((byte) this._EventAction.Value));
            break;
          }
          break;
        case EventActionType.BuildPlanetaryFacility:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.PlanetaryFacilityDefinitionsStatic.Count)
          {
            this.cmbPlanetaryFacilityDefinition.SetSelectedPlanetaryFacilityDefinition(Galaxy.PlanetaryFacilityDefinitionsStatic[this._EventAction.Value]);
            break;
          }
          break;
        case EventActionType.DestroyPlanetaryFacility:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.PlanetaryFacilityDefinitionsStatic.Count)
          {
            this.cmbPlanetaryFacilityDefinition.SetSelectedPlanetaryFacilityDefinition(Galaxy.PlanetaryFacilityDefinitionsStatic[this._EventAction.Value]);
            break;
          }
          break;
        case EventActionType.ChangeRaceBias:
          if (this._EventAction.Race == null && galaxy.Races.Count > 0)
            this._EventAction.Race = galaxy.Races[0];
          if (this._EventAction.RaceOther == null && galaxy.Races.Count > 1)
            this._EventAction.RaceOther = galaxy.Races[1];
          this.cmbRace.SetSelectedRace(this._EventAction.Race);
          this.cmbRaceOther.SetSelectedRace(this._EventAction.RaceOther);
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.ChangeEmpireReputation:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          if (this._EventAction.Value < -100 || this._EventAction.Value > 100)
            this._EventAction.Value = 5;
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.ChangeEmpireEvaluation:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          if (this._EventAction.Value < -100 || this._EventAction.Value > 100)
            this._EventAction.Value = 5;
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.InitiateTreaty:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          if (!this.cmbDiplomaticRelationType.GetDiplomaticRelationTypes().Contains(this._EventAction.DiplomaticRelationType) && this.cmbDiplomaticRelationType.GetDiplomaticRelationTypes().Count > 0)
            this._EventAction.DiplomaticRelationType = this.cmbDiplomaticRelationType.GetDiplomaticRelationTypes()[0];
          this.cmbDiplomaticRelationType.SetSelectedDiplomaticRelationType(this._EventAction.DiplomaticRelationType);
          this.txtAllianceName.Text = this._EventAction.AllianceName;
          this.chkLockedTreaty.Checked = this._EventAction.LockedAlliance;
          break;
        case EventActionType.BreakTreaty:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          break;
        case EventActionType.StartTradingSuperLuxuryResources:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          break;
        case EventActionType.StopTradingSuperLuxuryResources:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          break;
        case EventActionType.GeneralMessageToEmpire:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          break;
        case EventActionType.EmpireMessageToEmpire:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          break;
        case EventActionType.ResearchBonusInProject:
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.ResearchNodeDefinitionsStatic.Count)
          {
            this.cmbResearchProject.SetSelectedResearchNode(Galaxy.ResearchNodeDefinitionsStatic[this._EventAction.Value]);
            break;
          }
          break;
        case EventActionType.UnlockTechForEmpire:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this._EventAction.Value = Math.Max(0, this._EventAction.Value);
          if (this._EventAction.Value >= 0 && this._EventAction.Value < Galaxy.ResearchNodeDefinitionsStatic.Count)
          {
            this.cmbResearchProject.SetSelectedResearchNode(Galaxy.ResearchNodeDefinitionsStatic[this._EventAction.Value]);
            break;
          }
          break;
        case EventActionType.EmpireDeclaresWarOnOtherEmpire:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          this.chkLockedTreaty.Checked = this._EventAction.LockedAlliance;
          break;
        case EventActionType.VictoryConditionBonus:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          if (this._EventAction.Value < -100 || this._EventAction.Value > 100)
            this._EventAction.Value = 5;
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.SendFleetAttack:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          break;
        case EventActionType.SendPlanetDestroyerAttack:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          break;
        case EventActionType.IntergalacticConvoyMilitary:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.Value <= 0)
            this._EventAction.Value = 10;
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.IntergalacticConvoyCivilian:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.Value <= 0)
            this._EventAction.Value = 10;
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.numValue.Value = (Decimal) this._EventAction.Value;
          break;
        case EventActionType.CharacterGenerate:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.Character == null && this._EventAction.CharacterRole == CharacterRole.Undefined)
            this._EventAction.CharacterRole = CharacterRole.Leader;
          if (this._EventAction.Character != null && this._EventAction.Character.Empire != this._EventAction.Empire)
          {
            this._EventAction.Character = (Character) null;
            if (this._EventAction.CharacterRole == CharacterRole.Undefined)
              this._EventAction.CharacterRole = CharacterRole.Leader;
          }
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          if (this._EventAction.Empire != null)
          {
            if (this._EventAction.Empire.DominantRace != null && this._EventAction.Empire.DominantRace.AvailableCharacters != null && this._EventAction.Empire.DominantRace.AvailableCharacters.Count > 0)
            {
              this.cmbCharacter.BindData(this._EventAction.Empire, this._EventAction.Empire.DominantRace.AvailableCharacters, this._CharacterImageCache, this._Galaxy, true, false);
              this.cmbCharacter.SetSelectedCharacter(this._EventAction.Empire.DominantRace.AvailableCharacters[0]);
            }
            else
              this.cmbCharacter.BindData(this._EventAction.Empire, (CharacterList) null, this._CharacterImageCache, this._Galaxy, true, false);
            if (this._EventAction.Character != null)
            {
              this.cmbCharacter.SetSelectedCharacter(this._EventAction.Character);
              break;
            }
            if (this._EventAction.CharacterRole != CharacterRole.Undefined)
            {
              this.cmbCharacter.SetSelectedCharacter(this._EventAction.CharacterRole);
              break;
            }
            break;
          }
          break;
        case EventActionType.CharacterKill:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.Character != null && this._EventAction.Character.Empire != this._EventAction.Empire)
            this._EventAction.Character = (Character) null;
          if (this._EventAction.Empire != null && this._EventAction.Character == null && this._EventAction.Empire.Characters != null && this._EventAction.Empire.Characters.Count > 0)
            this._EventAction.Character = this._EventAction.Empire.Characters[0];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          if (this._EventAction.Empire != null)
          {
            this.cmbCharacter.BindData(this._EventAction.Empire, this._EventAction.Empire.Characters, this._CharacterImageCache, this._Galaxy, false, false);
            this.cmbCharacter.SetSelectedCharacter(this._EventAction.Character);
            break;
          }
          break;
        case EventActionType.CharacterChangeEmpire:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.Character != null && this._EventAction.Character.Empire != this._EventAction.Empire)
            this._EventAction.Character = (Character) null;
          if (this._EventAction.Empire != null && this._EventAction.Character == null && this._EventAction.Empire.Characters != null && this._EventAction.Empire.Characters.Count > 0)
            this._EventAction.Character = this._EventAction.Empire.Characters[0];
          if (this._EventAction.EmpireOther == null && galaxy.Empires.Count > 1)
            this._EventAction.EmpireOther = galaxy.Empires[1];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbEmpire2.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire2.SetSelectedEmpire(this._EventAction.EmpireOther);
          this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          if (this._EventAction.Empire != null)
          {
            this.cmbCharacter.BindData(this._EventAction.Empire, this._EventAction.Empire.Characters, this._CharacterImageCache, this._Galaxy, false, false);
            this.cmbCharacter.SetSelectedCharacter(this._EventAction.Character);
            break;
          }
          break;
        case EventActionType.CharacterChangeRole:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.Character != null && this._EventAction.Character.Empire != this._EventAction.Empire)
            this._EventAction.Character = (Character) null;
          if (this._EventAction.Empire != null && this._EventAction.Character == null && this._EventAction.Empire.Characters != null && this._EventAction.Empire.Characters.Count > 0)
            this._EventAction.Character = this._EventAction.Empire.Characters[0];
          if (this._EventAction.CharacterRole == CharacterRole.Undefined)
            this._EventAction.CharacterRole = CharacterRole.Leader;
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          this.cmbCharacter2.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          if (this._EventAction.Empire != null)
          {
            this.cmbCharacter.BindData(this._EventAction.Empire, this._EventAction.Empire.Characters, this._CharacterImageCache, this._Galaxy, false, false);
            this.cmbCharacter.SetSelectedCharacter(this._EventAction.Character);
            this.cmbCharacter2.BindData(this._EventAction.Empire, (CharacterList) null, this._CharacterImageCache, this._Galaxy, true, false);
            this.cmbCharacter2.SetSelectedCharacter(this._EventAction.CharacterRole);
            break;
          }
          break;
        case EventActionType.CharacterChangeImage:
          if (this._EventAction.Empire == null && galaxy.Empires.Count > 0)
            this._EventAction.Empire = galaxy.Empires[0];
          if (this._EventAction.Character != null && this._EventAction.Character.Empire != this._EventAction.Empire)
            this._EventAction.Character = (Character) null;
          if (this._EventAction.Empire != null && this._EventAction.Character == null && this._EventAction.Empire.Characters != null && this._EventAction.Empire.Characters.Count > 0)
            this._EventAction.Character = this._EventAction.Empire.Characters[0];
          this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
          this.cmbEmpire.SetSelectedEmpire(this._EventAction.Empire);
          this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          if (this._EventAction.Empire != null)
          {
            this.cmbCharacter.BindData(this._EventAction.Empire, this._EventAction.Empire.Characters, this._CharacterImageCache, this._Galaxy, false, false);
            this.cmbCharacter.SetSelectedCharacter(this._EventAction.Character);
            this.txtFilename.Text = this._EventAction.ImageFilename;
            break;
          }
          break;
      }
      this._Binding = false;
    }

    public void BindData(
      EventAction eventAction,
      GameEvent gameEvent,
      Font normalFont,
      Font boldFont,
      Font titleFont,
      Galaxy galaxy,
      Bitmap[] planetaryFacilityImages,
      Bitmap[] raceImages,
      Bitmap[] resourceImages,
      CharacterImageCache characterImageCache)
    {
      this._Binding = true;
      this._EventAction = eventAction;
      this._GameEvent = gameEvent;
      this._Galaxy = galaxy;
      this._CharacterImageCache = characterImageCache;
      this._NormalFont = normalFont;
      this._BoldFont = boldFont;
      this._TitleFont = titleFont;
      this.BindSourceValues(galaxy, planetaryFacilityImages, raceImages, resourceImages, characterImageCache);
      this.LayoutControls();
      if (this._EventAction != null)
      {
        this._EventAction.ValidActionTypes = this._EventAction.ResolveValidActionTypes(this._EventAction.Target);
        this.cmbEventActionType.BindData(this._EventAction.ValidActionTypes, false);
      }
      this.cmbEventActionType.SetSelectedEventActionType(this._EventAction.Type);
      this.BindEventActionControls(this._Galaxy);
      this._Binding = true;
      this.SetTargetDescriptionFromEventAction(this._EventAction);
      if (this._GameEvent != null)
        this.lblTriggerObjectName.Text = string.Format(TextResolver.GetText("Action for X"), (object) Galaxy.ResolveDescription(this._GameEvent));
      else
        this.lblTriggerObjectName.Text = string.Empty;
      this._Binding = false;
    }

    public EventAction UnbindData()
    {
      this._EventAction.ExecutionType = this.cmbEventActionExecutionType.SelectedEventActionExecutionType;
      switch (this._EventAction.ExecutionType)
      {
        case EventActionExecutionType.Immediately:
          this._EventAction.DelayDaysMinimum = (short) -1;
          this._EventAction.DelayDaysMaximum = (short) -1;
          break;
        case EventActionExecutionType.Delay:
          this._EventAction.DelayDaysMinimum = (short) this.numDelayStart.Value;
          this._EventAction.DelayDaysMaximum = (short) -1;
          break;
        case EventActionExecutionType.RandomDelay:
          this._EventAction.DelayDaysMinimum = (short) this.numDelayStart.Value;
          this._EventAction.DelayDaysMaximum = (short) this.numDelayEnd.Value;
          break;
      }
      switch (this._EventAction.Type)
      {
        case EventActionType.FindMoneyTreasure:
          this._EventAction.MoneyAmount = (double) this.numValue.Value;
          break;
        case EventActionType.LearnExplorationInfo:
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.LearnTech:
          if (this.cmbResearchProject.SelectedResearchNode != null)
          {
            this._EventAction.Value = this.cmbResearchProject.SelectedResearchNode.ResearchNodeId;
            break;
          }
          break;
        case EventActionType.UnlockTech:
          if (this.cmbResearchProject.SelectedResearchNode != null)
          {
            this._EventAction.Value = this.cmbResearchProject.SelectedResearchNode.ResearchNodeId;
            break;
          }
          break;
        case EventActionType.LearnGovernmentType:
          this._EventAction.Value = this.cmbGovernmentStyle.SelectedGovernmentId;
          break;
        case EventActionType.LearnAboutSpecialLocation:
          this._EventAction.Location = this.cmbGalaxyLocation.SelectedLocation;
          break;
        case EventActionType.LearnAboutLostColony:
          this._EventAction.Race = this.cmbRace.SelectedRace;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.SleepingRaceAwokenAtHabitat:
          this._EventAction.Race = this.cmbRace.SelectedRace;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.SplitEmpirePeacefully:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          break;
        case EventActionType.SplitEmpireCivilWar:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          break;
        case EventActionType.PirateFactionJoinsTriggerEmpire:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          break;
        case EventActionType.EmpireDeclaresWarOnTriggerEmpire:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          break;
        case EventActionType.ChangeEmpireGovernment:
          this._EventAction.Value = this.cmbGovernmentStyle.SelectedGovernmentId;
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          break;
        case EventActionType.GenerateBuiltObject:
          this._EventAction.BuiltObjectSubRole = this.cmbBuiltObjectSubRole.SelectedBuiltObjectSubRole;
          this._EventAction.TechLevel = (int) this.numValue.Value;
          break;
        case EventActionType.GenerateCreatureSwarm:
          this._EventAction.CreatureType = this.cmbCreatureType.SelectedCreatureType;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.GeneratePirateAmbush:
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.GenerateRefugeeFleet:
          this._EventAction.Race = this.cmbRace.SelectedRace;
          break;
        case EventActionType.GenerateNewEmpire:
          this._EventAction.Race = this.cmbRace.SelectedRace;
          break;
        case EventActionType.GenerateNewPirateFaction:
          this._EventAction.Race = this.cmbRace.SelectedRace;
          break;
        case EventActionType.MakeEmpireContact:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          break;
        case EventActionType.InterceptResource:
          if (this.cmbResource.SelectedResource != null)
          {
            this._EventAction.Value = (int) this.cmbResource.SelectedResource.ResourceID;
            break;
          }
          break;
        case EventActionType.GenerateResourceAtHabitat:
          if (this.cmbResource.SelectedResource != null)
          {
            this._EventAction.Value = (int) this.cmbResource.SelectedResource.ResourceID;
            break;
          }
          break;
        case EventActionType.RemoveResourceAtHabitat:
          if (this.cmbResource.SelectedResource != null)
          {
            this._EventAction.Value = (int) this.cmbResource.SelectedResource.ResourceID;
            break;
          }
          break;
        case EventActionType.BuildPlanetaryFacility:
          if (this.cmbPlanetaryFacilityDefinition.SelectedPlanetaryFacility != null)
          {
            this._EventAction.Value = this.cmbPlanetaryFacilityDefinition.SelectedPlanetaryFacility.PlanetaryFacilityDefinitionId;
            break;
          }
          break;
        case EventActionType.DestroyPlanetaryFacility:
          if (this.cmbPlanetaryFacilityDefinition.SelectedPlanetaryFacility != null)
          {
            this._EventAction.Value = this.cmbPlanetaryFacilityDefinition.SelectedPlanetaryFacility.PlanetaryFacilityDefinitionId;
            break;
          }
          break;
        case EventActionType.ChangeRaceBias:
          this._EventAction.Race = this.cmbRace.SelectedRace;
          this._EventAction.RaceOther = this.cmbRaceOther.SelectedRace;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.ChangeEmpireReputation:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.ChangeEmpireEvaluation:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.InitiateTreaty:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          this._EventAction.DiplomaticRelationType = this.cmbDiplomaticRelationType.SelectedDiplomaticRelationType;
          this._EventAction.LockedAlliance = this.chkLockedTreaty.Checked;
          this._EventAction.AllianceName = this.txtAllianceName.Text;
          break;
        case EventActionType.BreakTreaty:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          break;
        case EventActionType.StartTradingSuperLuxuryResources:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          break;
        case EventActionType.StopTradingSuperLuxuryResources:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          break;
        case EventActionType.GeneralMessageToEmpire:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.ImageFilename = this.txtFilename.Text;
          break;
        case EventActionType.EmpireMessageToEmpire:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          this._EventAction.ImageFilename = this.txtFilename.Text;
          break;
        case EventActionType.ResearchBonusInProject:
          if (this.cmbResearchProject.SelectedResearchNode != null)
          {
            this._EventAction.Value = this.cmbResearchProject.SelectedResearchNode.ResearchNodeId;
            break;
          }
          break;
        case EventActionType.UnlockTechForEmpire:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          if (this.cmbResearchProject.SelectedResearchNode != null)
          {
            this._EventAction.Value = this.cmbResearchProject.SelectedResearchNode.ResearchNodeId;
            break;
          }
          break;
        case EventActionType.EmpireDeclaresWarOnOtherEmpire:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          this._EventAction.LockedAlliance = this.chkLockedTreaty.Checked;
          break;
        case EventActionType.VictoryConditionBonus:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.SendFleetAttack:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          break;
        case EventActionType.SendPlanetDestroyerAttack:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          break;
        case EventActionType.IntergalacticConvoyMilitary:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.IntergalacticConvoyCivilian:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.Value = (int) this.numValue.Value;
          break;
        case EventActionType.CharacterGenerate:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.Character = (Character) null;
          this._EventAction.CharacterRole = CharacterRole.Undefined;
          CharacterRole characterRole = CharacterRole.Undefined;
          Character selectedCharacter = this.cmbCharacter.GetSelectedCharacter(out characterRole);
          if (selectedCharacter != null)
          {
            this._EventAction.Character = selectedCharacter;
            break;
          }
          if (characterRole != CharacterRole.Undefined)
          {
            this._EventAction.CharacterRole = characterRole;
            break;
          }
          break;
        case EventActionType.CharacterKill:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.Character = this.cmbCharacter.GetSelectedCharacter();
          break;
        case EventActionType.CharacterChangeEmpire:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.EmpireOther = this.cmbEmpire2.SelectedEmpire;
          this._EventAction.Character = this.cmbCharacter.GetSelectedCharacter();
          break;
        case EventActionType.CharacterChangeRole:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.Character = this.cmbCharacter.GetSelectedCharacter();
          this._EventAction.CharacterRole = this.cmbCharacter2.GetSelectedCharacterRole();
          break;
        case EventActionType.CharacterChangeImage:
          this._EventAction.Empire = this.cmbEmpire.SelectedEmpire;
          this._EventAction.Character = this.cmbCharacter.GetSelectedCharacter();
          this._EventAction.ImageFilename = this.txtFilename.Text;
          break;
      }
      this._EventAction.MessageTitle = this.txtMessageTitle.Text;
      this._EventAction.MessageText = this.txtMessageText.Text;
      return this._EventAction;
    }

    private void LayoutControls()
    {
      this.SuspendLayout();
      this.lblTriggerObjectName.Location = new Point(10, 10);
      this.lblTriggerObjectName.Font = this._TitleFont;
      this.lblTriggerObjectName.MaximumSize = new Size(350, 50);
      this.btnClose.Size = new Size(75, 25);
      this.btnClose.Location = new Point(365, 10);
      this.lblDescription.Location = new Point(10, 68);
      this.lblDescription.Font = this._BoldFont;
      this.lblDescription.MaximumSize = new Size(430, 40);
      this.lblDescription.ForeColor = Color.Yellow;
      this.lblEventActionType.Location = new Point(10, 113);
      this.lblEventActionType.Font = this._NormalFont;
      this.cmbEventActionType.Location = new Point(125, 110);
      this.cmbEventActionType.Size = new Size(315, 22);
      this.cmbEventActionType.BringToFront();
      this.lblActionTarget.Location = new Point(10, 105);
      this.lblActionTarget.Font = this._NormalFont;
      this.lblActionTarget.Visible = false;
      this.lblEventActionExecutionType.Location = new Point(10, 143);
      this.lblEventActionExecutionType.Font = this._NormalFont;
      this.cmbEventActionExecutionType.Location = new Point(125, 140);
      this.cmbEventActionExecutionType.Size = new Size(100, 22);
      this.cmbEventActionExecutionType.BringToFront();
      this.lblDelayStart.Location = new Point(235, 143);
      this.lblDelayStart.Font = this._NormalFont;
      this.numDelayStart.Size = new Size(45, 20);
      this.numDelayStart.Location = new Point(320, 140);
      this.numDelayStart.BringToFront();
      this.lblDelayEnd.Location = new Point(370, 143);
      this.lblDelayEnd.Font = this._NormalFont;
      this.numDelayEnd.Size = new Size(45, 20);
      this.numDelayEnd.Location = new Point(395, 140);
      this.numDelayEnd.BringToFront();
      this.btnDelete.Size = new Size(210, 40);
      this.btnDelete.Location = new Point(10, 577);
      this.btnDelete.Text = TextResolver.GetText("Delete Action");
      this.btnGoto.Size = new Size(210, 40);
      this.btnGoto.Location = new Point(230, 577);
      this.btnGoto.Text = TextResolver.GetText("Go to Target");
      this.lblBuiltObjectSubRole.Font = this._NormalFont;
      this.lblCreatureType.Font = this._NormalFont;
      this.lblEmpire.Font = this._NormalFont;
      this.lblGalaxyLocation.Font = this._NormalFont;
      this.lblGovernmentType.Font = this._NormalFont;
      this.lblPlanetaryFacility.Font = this._NormalFont;
      this.lblRace.Font = this._NormalFont;
      this.lblRaceOther.Font = this._NormalFont;
      this.lblResearchProject.Font = this._NormalFont;
      this.lblResource.Font = this._NormalFont;
      this.lblValue.Font = this._NormalFont;
      this.lblDiplomaticRelationType.Font = this._NormalFont;
      this.lblEmpire2.Font = this._NormalFont;
      this.lblMessageExplanation.Font = this._NormalFont;
      this.lblMessageTitle.Font = this._NormalFont;
      this.lblMessageText.Font = this._NormalFont;
      this.lblFilename.Font = this._NormalFont;
      this.lblAllianceName.Font = this._NormalFont;
      this.lblCharacter.Font = this._NormalFont;
      this.lblCharacter2.Font = this._NormalFont;
      Point point1 = new Point(10, 185);
      Point point2 = new Point(125, 182);
      Point point3 = new Point(10, 215);
      Point point4 = new Point(125, 212);
      Point point5 = new Point(10, 245);
      Point point6 = new Point(125, 242);
      Point point7 = new Point(335, 242);
      this.lblBuiltObjectSubRole.Location = point1;
      this.lblCreatureType.Location = point1;
      this.lblEmpire.Location = point1;
      this.lblGalaxyLocation.Location = point1;
      this.lblGovernmentType.Location = point5;
      this.lblPlanetaryFacility.Location = point3;
      this.lblRace.Location = point1;
      this.lblRaceOther.Location = point3;
      this.lblResearchProject.Location = point5;
      this.lblResource.Location = point3;
      this.lblValue.Location = point5;
      this.lblDiplomaticRelationType.Location = point5;
      this.lblEmpire2.Location = point3;
      this.lblCharacter.Location = point3;
      this.lblCharacter2.Location = point5;
      this.cmbBuiltObjectSubRole.Location = point2;
      this.cmbCreatureType.Location = point2;
      this.cmbEmpire.Location = point2;
      this.cmbGalaxyLocation.Location = point2;
      this.cmbGovernmentStyle.Location = point6;
      this.cmbPlanetaryFacilityDefinition.Location = point4;
      this.cmbRace.Location = point2;
      this.cmbRaceOther.Location = point4;
      this.cmbResearchProject.Location = point6;
      this.cmbResource.Location = point4;
      this.numValue.Location = point6;
      this.cmbEmpire2.Location = point4;
      this.cmbDiplomaticRelationType.Location = point6;
      this.cmbCharacter.Location = point4;
      this.cmbCharacter2.Location = point6;
      this.lblAllianceName.Location = new Point(335, 185);
      this.txtAllianceName.Size = new Size(105, 20);
      this.txtAllianceName.Location = new Point(335, 200);
      this.txtAllianceName.BringToFront();
      this.lblMessageExplanation.Location = new Point(125, 280);
      this.lblMessageExplanation.MaximumSize = new Size(315, 60);
      this.lblMessageExplanation.Text = TextResolver.GetText("Event Action Message Explanation");
      this.lblMessageTitle.Location = new Point(10, 340);
      this.txtMessageTitle.Location = new Point(125, 337);
      this.txtMessageTitle.Size = new Size(315, 20);
      this.lblMessageText.Location = new Point(10, 365);
      this.lblFilename.Location = new Point(10, 550);
      this.txtMessageText.Location = new Point(125, 362);
      this.txtFilename.Location = new Point(125, 547);
      this.txtMessageText.Size = new Size(315, 180);
      this.txtFilename.Size = new Size(315, 20);
      this.txtMessageText.ScrollBars = ScrollBars.Vertical;
      this.chkLockedTreaty.Location = point7;
      Size size = new Size(200, 22);
      this.cmbBuiltObjectSubRole.Size = size;
      this.cmbCreatureType.Size = size;
      this.cmbEmpire.Size = size;
      this.cmbGalaxyLocation.Size = size;
      this.cmbGovernmentStyle.Size = size;
      this.cmbPlanetaryFacilityDefinition.Size = size;
      this.cmbRace.Size = size;
      this.cmbRaceOther.Size = size;
      this.cmbResearchProject.Size = size;
      this.cmbResource.Size = size;
      this.cmbDiplomaticRelationType.Size = size;
      this.cmbEmpire2.Size = size;
      this.cmbCharacter.Size = size;
      this.cmbCharacter2.Size = size;
      this.cmbBuiltObjectSubRole.BringToFront();
      this.cmbCreatureType.BringToFront();
      this.cmbEmpire.BringToFront();
      this.cmbGalaxyLocation.BringToFront();
      this.cmbGovernmentStyle.BringToFront();
      this.cmbPlanetaryFacilityDefinition.BringToFront();
      this.cmbRace.BringToFront();
      this.cmbResearchProject.BringToFront();
      this.cmbResource.BringToFront();
      this.numValue.BringToFront();
      this.cmbDiplomaticRelationType.BringToFront();
      this.cmbEmpire2.BringToFront();
      this.cmbCharacter.BringToFront();
      this.cmbCharacter2.BringToFront();
      this.btnClose.BringToFront();
      this.ResumeLayout();
    }

    private void SetControlsForActionType(EventActionType eventActionType)
    {
      this.lblBuiltObjectSubRole.Visible = false;
      this.lblCreatureType.Visible = false;
      this.lblEmpire.Visible = false;
      this.lblGalaxyLocation.Visible = false;
      this.lblGovernmentType.Visible = false;
      this.lblPlanetaryFacility.Visible = false;
      this.lblRace.Visible = false;
      this.lblRaceOther.Visible = false;
      this.lblResearchProject.Visible = false;
      this.lblResource.Visible = false;
      this.lblValue.Visible = false;
      this.lblDelayStart.Visible = false;
      this.lblDelayEnd.Visible = false;
      this.lblDiplomaticRelationType.Visible = false;
      this.lblEmpire2.Visible = false;
      this.lblAllianceName.Visible = false;
      this.lblCharacter.Visible = false;
      this.lblCharacter2.Visible = false;
      this.lblMessageExplanation.Visible = true;
      this.lblMessageTitle.Visible = true;
      this.lblMessageText.Visible = true;
      this.lblFilename.Visible = false;
      this.lblBuiltObjectSubRole.Text = TextResolver.GetText("Ship/Base Type");
      this.lblCreatureType.Text = TextResolver.GetText("Creature Type");
      this.lblEmpire.Text = TextResolver.GetText("Empire");
      this.lblGalaxyLocation.Text = TextResolver.GetText("Special Location");
      this.lblGovernmentType.Text = TextResolver.GetText("Government");
      this.lblPlanetaryFacility.Text = TextResolver.GetText("Planetary Facility");
      this.lblRace.Text = TextResolver.GetText("Race");
      this.lblRaceOther.Text = TextResolver.GetText("Other Race");
      this.lblResearchProject.Text = TextResolver.GetText("Research Project");
      this.lblResource.Text = TextResolver.GetText("Resource");
      this.lblValue.Text = TextResolver.GetText("Value");
      this.lblCharacter.Text = TextResolver.GetText("Character");
      this.lblCharacter2.Text = TextResolver.GetText("Character Role");
      this.lblDiplomaticRelationType.Text = TextResolver.GetText("Treaty");
      this.lblEmpire2.Text = TextResolver.GetText("Other Empire");
      this.lblMessageText.Text = TextResolver.GetText("Message Text");
      this.lblMessageTitle.Text = TextResolver.GetText("Message Title");
      this.lblFilename.Text = TextResolver.GetText("Background Image");
      this.cmbBuiltObjectSubRole.Visible = false;
      this.cmbCreatureType.Visible = false;
      this.cmbEmpire.Visible = false;
      this.cmbGalaxyLocation.Visible = false;
      this.cmbGovernmentStyle.Visible = false;
      this.cmbPlanetaryFacilityDefinition.Visible = false;
      this.cmbRace.Visible = false;
      this.cmbRaceOther.Visible = false;
      this.cmbResearchProject.Visible = false;
      this.cmbResource.Visible = false;
      this.numValue.Visible = false;
      this.chkLockedTreaty.Visible = false;
      this.cmbCharacter.Visible = false;
      this.cmbCharacter2.Visible = false;
      this.numDelayStart.Visible = false;
      this.numDelayEnd.Visible = false;
      this.cmbDiplomaticRelationType.Visible = false;
      this.cmbEmpire2.Visible = false;
      this.txtMessageText.Visible = true;
      this.txtFilename.Visible = false;
      this.txtAllianceName.Visible = false;
      Point point1 = new Point(10, 215);
      Point point2 = new Point(125, 212);
      Point point3 = new Point(10, 245);
      Point point4 = new Point(125, 242);
      this.lblEmpire2.Location = point1;
      this.cmbEmpire2.Location = point2;
      switch (eventActionType)
      {
        case EventActionType.FindMoneyTreasure:
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 1000000M;
          this.lblValue.Text = TextResolver.GetText("Money");
          break;
        case EventActionType.LearnExplorationInfo:
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 50M;
          this.lblValue.Text = TextResolver.GetText("# Systems");
          break;
        case EventActionType.LearnTech:
          this.lblResearchProject.Visible = true;
          this.cmbResearchProject.Visible = true;
          break;
        case EventActionType.UnlockTech:
          this.lblResearchProject.Visible = true;
          this.cmbResearchProject.Visible = true;
          break;
        case EventActionType.LearnGovernmentType:
          this.lblGovernmentType.Visible = true;
          this.cmbGovernmentStyle.Visible = true;
          break;
        case EventActionType.LearnAboutSpecialLocation:
          this.lblGalaxyLocation.Visible = true;
          this.cmbGalaxyLocation.Visible = true;
          break;
        case EventActionType.LearnAboutLostColony:
          this.lblRace.Visible = true;
          this.cmbRace.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 20000M;
          this.lblValue.Text = TextResolver.GetText("Pop in millions");
          break;
        case EventActionType.SleepingRaceAwokenAtHabitat:
          this.lblRace.Visible = true;
          this.cmbRace.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 20000M;
          this.lblValue.Text = TextResolver.GetText("Pop in millions");
          break;
        case EventActionType.SplitEmpirePeacefully:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          break;
        case EventActionType.SplitEmpireCivilWar:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          break;
        case EventActionType.PirateFactionJoinsTriggerEmpire:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          break;
        case EventActionType.EmpireDeclaresWarOnTriggerEmpire:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          break;
        case EventActionType.ChangeEmpireGovernment:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblGovernmentType.Visible = true;
          this.cmbGovernmentStyle.Visible = true;
          break;
        case EventActionType.GenerateBuiltObject:
          this.lblBuiltObjectSubRole.Visible = true;
          this.cmbBuiltObjectSubRole.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 7M;
          this.lblValue.Text = TextResolver.GetText("Tech Level");
          break;
        case EventActionType.GenerateCreatureSwarm:
          this.lblCreatureType.Visible = true;
          this.cmbCreatureType.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 50M;
          this.lblValue.Text = TextResolver.GetText("# Creatures");
          break;
        case EventActionType.GeneratePirateAmbush:
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 50M;
          this.lblValue.Text = TextResolver.GetText("# Ships");
          break;
        case EventActionType.GenerateRefugeeFleet:
          this.lblRace.Visible = true;
          this.cmbRace.Visible = true;
          break;
        case EventActionType.GenerateNewEmpire:
          this.lblRace.Visible = true;
          this.cmbRace.Visible = true;
          break;
        case EventActionType.GenerateNewPirateFaction:
          this.lblRace.Visible = true;
          this.cmbRace.Visible = true;
          break;
        case EventActionType.MakeEmpireContact:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          break;
        case EventActionType.InterceptResource:
          this.lblResource.Visible = true;
          this.cmbResource.Visible = true;
          break;
        case EventActionType.GenerateResourceAtHabitat:
          this.lblResource.Visible = true;
          this.cmbResource.Visible = true;
          break;
        case EventActionType.RemoveResourceAtHabitat:
          this.lblResource.Visible = true;
          this.cmbResource.Visible = true;
          break;
        case EventActionType.BuildPlanetaryFacility:
          this.lblPlanetaryFacility.Visible = true;
          this.cmbPlanetaryFacilityDefinition.Visible = true;
          break;
        case EventActionType.DestroyPlanetaryFacility:
          this.lblPlanetaryFacility.Visible = true;
          this.cmbPlanetaryFacilityDefinition.Visible = true;
          break;
        case EventActionType.ChangeRaceBias:
          this.lblRace.Visible = true;
          this.cmbRace.Visible = true;
          this.lblRaceOther.Visible = true;
          this.cmbRaceOther.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.lblValue.Text = TextResolver.GetText("Bias Change");
          this.numValue.Minimum = -50M;
          this.numValue.Maximum = 50M;
          break;
        case EventActionType.ChangeEmpireReputation:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = -100M;
          this.numValue.Maximum = 100M;
          this.lblValue.Text = TextResolver.GetText("Reputation Change");
          break;
        case EventActionType.ChangeEmpireEvaluation:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = -100M;
          this.numValue.Maximum = 100M;
          this.lblValue.Text = TextResolver.GetText("Evaluation Change");
          break;
        case EventActionType.InitiateTreaty:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          this.lblDiplomaticRelationType.Visible = true;
          this.cmbDiplomaticRelationType.Visible = true;
          this.chkLockedTreaty.Visible = true;
          this.lblAllianceName.Visible = true;
          this.txtAllianceName.Visible = true;
          break;
        case EventActionType.BreakTreaty:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          break;
        case EventActionType.StartTradingSuperLuxuryResources:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          break;
        case EventActionType.StopTradingSuperLuxuryResources:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          break;
        case EventActionType.GeneralMessageToEmpire:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblFilename.Visible = true;
          this.txtFilename.Visible = true;
          this.lblMessageExplanation.Visible = false;
          break;
        case EventActionType.EmpireMessageToEmpire:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          this.lblFilename.Visible = true;
          this.txtFilename.Visible = true;
          this.lblEmpire.Text = TextResolver.GetText("Sender");
          this.lblEmpire2.Text = TextResolver.GetText("Recipient");
          this.lblMessageExplanation.Visible = false;
          break;
        case EventActionType.ResearchBonusInProject:
          this.lblResearchProject.Visible = true;
          this.cmbResearchProject.Visible = true;
          break;
        case EventActionType.UnlockTechForEmpire:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblResearchProject.Visible = true;
          this.cmbResearchProject.Visible = true;
          break;
        case EventActionType.EmpireDeclaresWarOnOtherEmpire:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          this.chkLockedTreaty.Visible = true;
          break;
        case EventActionType.VictoryConditionBonus:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = -100M;
          this.numValue.Maximum = 100M;
          break;
        case EventActionType.SendFleetAttack:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          break;
        case EventActionType.SendPlanetDestroyerAttack:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          break;
        case EventActionType.IntergalacticConvoyMilitary:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 100M;
          break;
        case EventActionType.IntergalacticConvoyCivilian:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblValue.Visible = true;
          this.numValue.Visible = true;
          this.numValue.Minimum = 1M;
          this.numValue.Maximum = 100M;
          break;
        case EventActionType.CharacterGenerate:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblCharacter.Visible = true;
          this.cmbCharacter.Visible = true;
          break;
        case EventActionType.CharacterKill:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblCharacter.Visible = true;
          this.cmbCharacter.Visible = true;
          break;
        case EventActionType.CharacterChangeEmpire:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblCharacter.Visible = true;
          this.cmbCharacter.Visible = true;
          this.lblEmpire2.Visible = true;
          this.cmbEmpire2.Visible = true;
          this.lblEmpire2.Location = point3;
          this.cmbEmpire2.Location = point4;
          break;
        case EventActionType.CharacterChangeRole:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblCharacter.Visible = true;
          this.cmbCharacter.Visible = true;
          this.lblCharacter2.Visible = true;
          this.cmbCharacter2.Visible = true;
          break;
        case EventActionType.CharacterChangeImage:
          this.lblEmpire.Visible = true;
          this.cmbEmpire.Visible = true;
          this.lblCharacter.Visible = true;
          this.cmbCharacter.Visible = true;
          this.lblFilename.Visible = true;
          this.txtFilename.Visible = true;
          this.lblFilename.Text = TextResolver.GetText("Character Image");
          break;
      }
    }

    private void SetTargetDescriptionFromEventAction(EventAction eventAction)
    {
      if (eventAction.Target != null)
        this.lblActionTarget.Text = eventAction.Target.Name;
      else
        this.lblActionTarget.Text = string.Empty;
      this.lblDescription.Text = Galaxy.ResolveDescription(eventAction);
    }

    private void cmbEventActionType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetControlsForActionType(this.cmbEventActionType.SelectedEventActionType);
      this._EventAction.Type = this.cmbEventActionType.SelectedEventActionType;
      if (this._Binding)
        return;
      this.BindEventActionControls(this._Galaxy);
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void ClosePanel()
    {
      if (this.PanelClosed == null)
        return;
      this.PanelClosed((object) this, new EventArgs());
    }

    private void btnClose_Click(object sender, EventArgs e) => this.ClosePanel();

    private void btnDelete_Click(object sender, EventArgs e)
    {
      GameEvent byId = this._Galaxy.GameEvents.GetById(this._GameEvent.GameEventId);
      if (byId != null && byId.Actions != null)
        byId.Actions.Remove(this._EventAction);
      this.ClosePanel();
    }

    private void btnGoto_Click(object sender, EventArgs e)
    {
      if (this._EventAction.Target == null || this.GotoTarget == null)
        return;
      this.GotoTarget((object) this._EventAction.Target, new EventArgs());
    }

    private void cmbBuiltObjectSubRole_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbResource_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbGalaxyLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void numValue_ValueChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbRace_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbEmpire_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!this._Binding)
      {
        this.UnbindData();
        this.SetTargetDescriptionFromEventAction(this._EventAction);
      }
      Empire selectedEmpire = this.cmbEmpire.SelectedEmpire;
      switch (this._EventAction.Type)
      {
        case EventActionType.CharacterGenerate:
          this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          if (selectedEmpire == null)
            break;
          if (selectedEmpire.DominantRace != null && selectedEmpire.DominantRace.AvailableCharacters != null && selectedEmpire.DominantRace.AvailableCharacters.Count > 0)
          {
            this.cmbCharacter.BindData(selectedEmpire, selectedEmpire.DominantRace.AvailableCharacters, this._CharacterImageCache, this._Galaxy, true, false);
            this.cmbCharacter.SetSelectedCharacter(selectedEmpire.DominantRace.AvailableCharacters[0]);
            break;
          }
          this.cmbCharacter.BindData(selectedEmpire, (CharacterList) null, this._CharacterImageCache, this._Galaxy, true, false);
          this.cmbCharacter.SetSelectedCharacter(CharacterRole.Leader);
          break;
        case EventActionType.CharacterKill:
        case EventActionType.CharacterChangeEmpire:
        case EventActionType.CharacterChangeImage:
          this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          if (selectedEmpire == null)
            break;
          this.cmbCharacter.BindData(selectedEmpire, selectedEmpire.Characters, this._CharacterImageCache, this._Galaxy, false, false);
          if (selectedEmpire.Characters.Count <= 0)
            break;
          this.cmbCharacter.SetSelectedCharacter(selectedEmpire.Characters[0]);
          break;
        case EventActionType.CharacterChangeRole:
          this.cmbCharacter.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          if (selectedEmpire == null)
            break;
          Character character = (Character) null;
          this.cmbCharacter.BindData(selectedEmpire, selectedEmpire.Characters, this._CharacterImageCache, this._Galaxy, false, false);
          if (selectedEmpire.Characters.Count > 0)
          {
            character = selectedEmpire.Characters[0];
            this.cmbCharacter.SetSelectedCharacter(character);
          }
          this.cmbCharacter2.BindData((Empire) null, (CharacterList) null, (CharacterImageCache) null, this._Galaxy, false, false);
          this.cmbCharacter2.BindData(selectedEmpire, (CharacterList) null, this._CharacterImageCache, this._Galaxy, true, false);
          CharacterRole characterRole = CharacterRole.IntelligenceAgent;
          if (character != null && character.Role == CharacterRole.IntelligenceAgent)
            characterRole = CharacterRole.Scientist;
          this.cmbCharacter2.SetSelectedCharacter(characterRole);
          break;
      }
    }

    private void cmbCreatureType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbResearchProject_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbGovernmentStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbPlanetaryFacilityDefinition_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbRaceOther_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbEventActionExecutionType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!this._Binding)
      {
        this.UnbindData();
        this.SetTargetDescriptionFromEventAction(this._EventAction);
      }
      this.numDelayStart.Minimum = 0M;
      this.numDelayStart.Maximum = 32000M;
      this.numDelayEnd.Minimum = 0M;
      this.numDelayEnd.Maximum = 32000M;
      switch (this.cmbEventActionExecutionType.SelectedEventActionExecutionType)
      {
        case EventActionExecutionType.Immediately:
          this.lblDelayStart.Visible = false;
          this.numDelayStart.Visible = false;
          this.lblDelayEnd.Visible = false;
          this.numDelayEnd.Visible = false;
          break;
        case EventActionExecutionType.Delay:
          this.lblDelayStart.Visible = true;
          this.numDelayStart.Visible = true;
          this.lblDelayEnd.Visible = false;
          this.numDelayEnd.Visible = false;
          break;
        case EventActionExecutionType.RandomDelay:
          this.lblDelayStart.Visible = true;
          this.numDelayStart.Visible = true;
          this.lblDelayEnd.Visible = true;
          this.numDelayEnd.Visible = true;
          break;
      }
    }

    private void numDelayStart_ValueChanged(object sender, EventArgs e)
    {
      if (this.numDelayEnd.Value <= this.numDelayStart.Value)
        this.numDelayEnd.Value = Math.Min(32000M, ++this.numDelayStart.Value);
      this.numDelayEnd.Minimum = this.numDelayStart.Value;
      this.numDelayEnd.Maximum = Math.Max(this.numDelayEnd.Maximum, this.numDelayEnd.Minimum);
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void numDelayEnd_ValueChanged(object sender, EventArgs e)
    {
      if (this.numDelayStart.Value >= this.numDelayEnd.Value)
        this.numDelayStart.Value = Math.Max(0M, --this.numDelayEnd.Value);
      this.numDelayStart.Maximum = this.numDelayEnd.Value;
      this.numDelayStart.Minimum = Math.Min(this.numDelayStart.Minimum, this.numDelayStart.Maximum);
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbEmpire2_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbDiplomaticRelationType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbCharacter_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }

    private void cmbCharacter2_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this._Binding)
        return;
      this.UnbindData();
      this.SetTargetDescriptionFromEventAction(this._EventAction);
    }
  }
}
