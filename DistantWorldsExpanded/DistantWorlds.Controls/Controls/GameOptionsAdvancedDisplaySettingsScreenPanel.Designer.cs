using System.ComponentModel;
using System.Windows.Forms;

namespace DistantWorlds.Controls; 

partial class GameOptionsAdvancedDisplaySettingsScreenPanel {

  /// <summary> 
  /// Required designer variable.
  /// </summary>
  internal IContainer components = null;

  /// <summary> 
  /// Clean up any resources being used.
  /// </summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing) {
    if (disposing && (components != null)) {
      components.Dispose();
    }

    base.Dispose(disposing);
  }

  #region Component Designer generated code

  /// <summary> 
  /// Required method for Designer support - do not modify 
  /// the contents of this method with the code editor.
  /// </summary>
  internal void InitializeComponent() {
    components = new System.ComponentModel.Container();
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DistantWorlds.Controls.GameOptionsAdvancedDisplaySettingsScreenPanel));
    
    // child control constructors
            
    this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons = new System.Windows.Forms.GroupBox();
    this.chkGameOptionsGalaxyDisplayAlwaysPirates = new();
    this.chkGameOptionsGalaxyDisplayColonyShips = new();
    this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips = new();
    this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets = new();
    this.chkGameOptionsGalaxyDisplayConstructionShips = new();
    this.chkGameOptionsGalaxyDisplaySpacePorts = new();
    this.chkGameOptionsGalaxyDisplayExplorationShips = new();
    this.chkGameOptionsGalaxyDisplayResupplyShips = new();
    this.chkGameOptionsGalaxyDisplayOtherBases = new();
    this.chkGameOptionsGalaxyDisplayCivilianShips = new();
    this.chkGameOptionsGalaxyDisplayFleets = new();
    this.chkGameOptionsGalaxyDisplayMilitaryShips = new();
    this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail = new DistantWorlds.Controls.LabelledTrackBar();
    this.chkOptionsShowSystemNebulae = new();
    this.chkGameOptionsGalaxyDisplayCleanGalaxyView = new();
    this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate = new System.Windows.Forms.GroupBox();
    this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate = new System.Windows.Forms.NumericUpDown();
    this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited = new();
    this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS = new System.Windows.Forms.Label();


    // suspend layout
    this.SuspendLayout();
    this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.SuspendLayout();
    this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.SuspendLayout();
    ((System.ComponentModel.ISupportInitialize)this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate).BeginInit();
    
    // setup main control
    this.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
    this.BackgroundImage = (System.Drawing.Image)resources.GetObject("AdvancedDisplaySettings.BackgroundImage");
    this.BorderColor1 = System.Drawing.Color.FromArgb(96, 200, 200, 200);
    this.BorderColor2 = System.Drawing.Color.FromArgb(96, 140, 140, 140);
    this.BorderColor3 = System.Drawing.Color.FromArgb(96, 20, 20, 20);
    this.BorderColor4 = System.Drawing.Color.FromArgb(96, 80, 80, 80);
    this.BorderSize = 3;
    this.Controls.Add(this.chkGameOptionsGalaxyDisplayCleanGalaxyView);
    this.Controls.Add(this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons);
    this.Controls.Add(this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail);
    this.Controls.Add(this.chkOptionsShowSystemNebulae);
    this.Controls.Add(this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate);
    this.HeaderIcon = null;
    this.HeaderTitle = "Advanced Display Settings";
    this.Location = new System.Drawing.Point(377, 348);
    this.Name = "pnlGameOptionsAdvancedDisplaySettings";
    this.Size = new System.Drawing.Size(300, 410);
    this.TabIndex = 124;
    this.Visible = false;
    
    // child control setup
                this.chkGameOptionsGalaxyDisplayCleanGalaxyView.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.Location = new System.Drawing.Point(12, 359);
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.Name = "chkGameOptionsGalaxyDisplayCleanGalaxyView";
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.Size = new System.Drawing.Size(133, 17);
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.TabIndex = 75;
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.Text = "Clean Galaxy view";
            this.chkGameOptionsGalaxyDisplayCleanGalaxyView.UseVisualStyleBackColor = false;
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.BackColor = System.Drawing.Color.Transparent;
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayAlwaysPirates);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayColonyShips);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayConstructionShips);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplaySpacePorts);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayExplorationShips);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayResupplyShips);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayOtherBases);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayCivilianShips);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayFleets);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Controls.Add(this.chkGameOptionsGalaxyDisplayMilitaryShips);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Location = new System.Drawing.Point(12, 159);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Name = "grpGameOptionsAdvancedDisplaySettingsGalaxyIcons";
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Size = new System.Drawing.Size(274, 191);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.TabIndex = 70;
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.TabStop = false;
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Text = "Galaxy View - Ship Display";
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.Location = new System.Drawing.Point(10, 167);
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.Name = "chkGameOptionsGalaxyDisplayAlwaysPirates";
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.Size = new System.Drawing.Size(142, 17);
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.TabIndex = 80;
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.Text = "Always show Pirates";
            this.chkGameOptionsGalaxyDisplayAlwaysPirates.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayColonyShips.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayColonyShips.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayColonyShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayColonyShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayColonyShips.Location = new System.Drawing.Point(136, 37);
            this.chkGameOptionsGalaxyDisplayColonyShips.Name = "chkGameOptionsGalaxyDisplayColonyShips";
            this.chkGameOptionsGalaxyDisplayColonyShips.Size = new System.Drawing.Size(99, 17);
            this.chkGameOptionsGalaxyDisplayColonyShips.TabIndex = 79;
            this.chkGameOptionsGalaxyDisplayColonyShips.Text = "Colony ships";
            this.chkGameOptionsGalaxyDisplayColonyShips.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Location = new System.Drawing.Point(10, 148);
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Name = "chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips";
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Size = new System.Drawing.Size(220, 17);
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.TabIndex = 78;
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Text = "Always show enemy Military ships";
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Location = new System.Drawing.Point(10, 129);
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Name = "chkGameOptionsGalaxyDisplayAlwaysEnemyFleets";
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Size = new System.Drawing.Size(179, 17);
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.TabIndex = 77;
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Text = "Always show enemy Fleets";
            this.chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayConstructionShips.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayConstructionShips.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayConstructionShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayConstructionShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayConstructionShips.Location = new System.Drawing.Point(136, 56);
            this.chkGameOptionsGalaxyDisplayConstructionShips.Name = "chkGameOptionsGalaxyDisplayConstructionShips";
            this.chkGameOptionsGalaxyDisplayConstructionShips.Size = new System.Drawing.Size(131, 17);
            this.chkGameOptionsGalaxyDisplayConstructionShips.TabIndex = 76;
            this.chkGameOptionsGalaxyDisplayConstructionShips.Text = "Construction ships";
            this.chkGameOptionsGalaxyDisplayConstructionShips.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplaySpacePorts.AutoSize = true;
            this.chkGameOptionsGalaxyDisplaySpacePorts.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplaySpacePorts.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplaySpacePorts.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplaySpacePorts.Location = new System.Drawing.Point(10, 75);
            this.chkGameOptionsGalaxyDisplaySpacePorts.Name = "chkGameOptionsGalaxyDisplaySpacePorts";
            this.chkGameOptionsGalaxyDisplaySpacePorts.Size = new System.Drawing.Size(94, 17);
            this.chkGameOptionsGalaxyDisplaySpacePorts.TabIndex = 75;
            this.chkGameOptionsGalaxyDisplaySpacePorts.Text = "Space ports";
            this.chkGameOptionsGalaxyDisplaySpacePorts.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayExplorationShips.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayExplorationShips.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayExplorationShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayExplorationShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayExplorationShips.Location = new System.Drawing.Point(136, 18);
            this.chkGameOptionsGalaxyDisplayExplorationShips.Name = "chkGameOptionsGalaxyDisplayExplorationShips";
            this.chkGameOptionsGalaxyDisplayExplorationShips.Size = new System.Drawing.Size(123, 17);
            this.chkGameOptionsGalaxyDisplayExplorationShips.TabIndex = 74;
            this.chkGameOptionsGalaxyDisplayExplorationShips.Text = "Exploration ships";
            this.chkGameOptionsGalaxyDisplayExplorationShips.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayResupplyShips.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayResupplyShips.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayResupplyShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayResupplyShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayResupplyShips.Location = new System.Drawing.Point(10, 37);
            this.chkGameOptionsGalaxyDisplayResupplyShips.Name = "chkGameOptionsGalaxyDisplayResupplyShips";
            this.chkGameOptionsGalaxyDisplayResupplyShips.Size = new System.Drawing.Size(111, 17);
            this.chkGameOptionsGalaxyDisplayResupplyShips.TabIndex = 73;
            this.chkGameOptionsGalaxyDisplayResupplyShips.Text = "Resupply ships";
            this.chkGameOptionsGalaxyDisplayResupplyShips.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayOtherBases.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayOtherBases.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayOtherBases.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayOtherBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayOtherBases.Location = new System.Drawing.Point(10, 94);
            this.chkGameOptionsGalaxyDisplayOtherBases.Name = "chkGameOptionsGalaxyDisplayOtherBases";
            this.chkGameOptionsGalaxyDisplayOtherBases.Size = new System.Drawing.Size(95, 17);
            this.chkGameOptionsGalaxyDisplayOtherBases.TabIndex = 72;
            this.chkGameOptionsGalaxyDisplayOtherBases.Text = "Other bases";
            this.chkGameOptionsGalaxyDisplayOtherBases.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayCivilianShips.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayCivilianShips.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayCivilianShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayCivilianShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayCivilianShips.Location = new System.Drawing.Point(136, 75);
            this.chkGameOptionsGalaxyDisplayCivilianShips.Name = "chkGameOptionsGalaxyDisplayCivilianShips";
            this.chkGameOptionsGalaxyDisplayCivilianShips.Size = new System.Drawing.Size(101, 17);
            this.chkGameOptionsGalaxyDisplayCivilianShips.TabIndex = 71;
            this.chkGameOptionsGalaxyDisplayCivilianShips.Text = "Civilian ships";
            this.chkGameOptionsGalaxyDisplayCivilianShips.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayFleets.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayFleets.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayFleets.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayFleets.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayFleets.Location = new System.Drawing.Point(10, 18);
            this.chkGameOptionsGalaxyDisplayFleets.Name = "chkGameOptionsGalaxyDisplayFleets";
            this.chkGameOptionsGalaxyDisplayFleets.Size = new System.Drawing.Size(59, 17);
            this.chkGameOptionsGalaxyDisplayFleets.TabIndex = 70;
            this.chkGameOptionsGalaxyDisplayFleets.Text = "Fleets";
            this.chkGameOptionsGalaxyDisplayFleets.UseVisualStyleBackColor = false;
            this.chkGameOptionsGalaxyDisplayMilitaryShips.AutoSize = true;
            this.chkGameOptionsGalaxyDisplayMilitaryShips.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsGalaxyDisplayMilitaryShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsGalaxyDisplayMilitaryShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsGalaxyDisplayMilitaryShips.Location = new System.Drawing.Point(10, 56);
            this.chkGameOptionsGalaxyDisplayMilitaryShips.Name = "chkGameOptionsGalaxyDisplayMilitaryShips";
            this.chkGameOptionsGalaxyDisplayMilitaryShips.Size = new System.Drawing.Size(100, 17);
            this.chkGameOptionsGalaxyDisplayMilitaryShips.TabIndex = 69;
            this.chkGameOptionsGalaxyDisplayMilitaryShips.Text = "Military ships";
            this.chkGameOptionsGalaxyDisplayMilitaryShips.UseVisualStyleBackColor = false;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.BackColor = System.Drawing.Color.FromArgb(39, 40, 44);
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.BackColor2 = System.Drawing.Color.FromArgb(36, 35, 40);
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.BackColor3 = System.Drawing.Color.FromArgb(51, 54, 61);
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.BorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Curvature = 10;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.GradientMode = DistantWorlds.Controls.LinearGradientMode.Vertical;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.LabelText = "System Nebulae Detail";
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.LabelWidth = 100;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.LinkText = null;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.LinkWidth = 0;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Location = new System.Drawing.Point(13, 109);
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Margin = new System.Windows.Forms.Padding(0);
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Name = "tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail";
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Size = new System.Drawing.Size(270, 42);
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.SliderOffset = 25;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.TabIndex = 68;
            this.tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Value = 0;
            this.chkOptionsShowSystemNebulae.AutoSize = true;
            this.chkOptionsShowSystemNebulae.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsShowSystemNebulae.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsShowSystemNebulae.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsShowSystemNebulae.Location = new System.Drawing.Point(12, 82);
            this.chkOptionsShowSystemNebulae.Name = "chkOptionsShowSystemNebulae";
            this.chkOptionsShowSystemNebulae.Size = new System.Drawing.Size(222, 17);
            this.chkOptionsShowSystemNebulae.TabIndex = 64;
            this.chkOptionsShowSystemNebulae.Text = "Display nebulae clouds in systems";
            this.chkOptionsShowSystemNebulae.UseVisualStyleBackColor = false;
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.BackColor = System.Drawing.Color.Transparent;
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Controls.Add(this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate);
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Controls.Add(this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited);
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Controls.Add(this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS);
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Location = new System.Drawing.Point(12, 10);
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Name = "grpGameOptionsAdvancedDisplaySettingsMaximumFramerate";
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Size = new System.Drawing.Size(274, 50);
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.TabIndex = 67;
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.TabStop = false;
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Text = "Maximum Framerate";

            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Location = new System.Drawing.Point(107, 18);
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Name = "numGameOptionsAdvancedDisplaySettingsMaximumFramerate";
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Size = new System.Drawing.Size(45, 21);
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.TabIndex = 67;
            this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Value = new decimal(new int[4] { 50, 0, 0, 0 });
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.AutoSize = true;
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.BackColor = System.Drawing.Color.Transparent;
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Location = new System.Drawing.Point(8, 20);
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Name = "chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited";
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Size = new System.Drawing.Size(79, 17);
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.TabIndex = 69;
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Text = "Unlimited";
            this.chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.UseVisualStyleBackColor = false;
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.AutoSize = true;
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.BackColor = System.Drawing.Color.Transparent;
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Location = new System.Drawing.Point(157, 21);
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Name = "lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS";
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Size = new System.Drawing.Size(24, 13);
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.TabIndex = 68;
            this.lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Text = "fps";
            
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.ResumeLayout(false);
            this.grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.PerformLayout();
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.ResumeLayout(false);
            this.grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.numGameOptionsAdvancedDisplaySettingsMaximumFramerate).EndInit();
  }

  #endregion

  

  internal CheckBox chkOptionsShowSystemNebulae;
  internal GroupBox grpGameOptionsAdvancedDisplaySettingsGalaxyIcons;
  internal CheckBox chkGameOptionsGalaxyDisplayAlwaysPirates;
  internal CheckBox chkGameOptionsGalaxyDisplayColonyShips;
  internal CheckBox chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips;
  internal CheckBox chkGameOptionsGalaxyDisplayAlwaysEnemyFleets;
  internal CheckBox chkGameOptionsGalaxyDisplayConstructionShips;
  internal CheckBox chkGameOptionsGalaxyDisplaySpacePorts;
  internal CheckBox chkGameOptionsGalaxyDisplayExplorationShips;
  internal CheckBox chkGameOptionsGalaxyDisplayResupplyShips;
  internal CheckBox chkGameOptionsGalaxyDisplayOtherBases;
  internal CheckBox chkGameOptionsGalaxyDisplayCivilianShips;
  internal CheckBox chkGameOptionsGalaxyDisplayFleets;
  internal CheckBox chkGameOptionsGalaxyDisplayMilitaryShips;
  internal LabelledTrackBar tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail;
  internal CheckBox chkGameOptionsGalaxyDisplayCleanGalaxyView;
  internal GroupBox grpGameOptionsAdvancedDisplaySettingsMaximumFramerate;
  internal NumericUpDown numGameOptionsAdvancedDisplaySettingsMaximumFramerate;
  internal CheckBox chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited;
  internal Label lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS;




}