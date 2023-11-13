using System.ComponentModel;
using System.Windows.Forms;

namespace DistantWorlds.Controls; 

partial class GameOptionsScreenPanel {

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
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DistantWorlds.Controls.GameOptionsScreenPanel));
    
    // child control constructors
    this.btnGameOptionsShowMessages = new DistantWorlds.Controls.GlassButton();
    this.chkOptionsLoadedGamesPaused = new DistantWorlds.Controls.CheckBox();
    this.grpOptionsAutoSave = new System.Windows.Forms.GroupBox();

    this.lblOptionsMouseScrollMode = new System.Windows.Forms.Label();
    this.cmbOptionsMouseScrollWheelBehaviour = new System.Windows.Forms.ComboBox();
    this.grpOptionsDisplaySettings = new System.Windows.Forms.GroupBox();

    this.numOptionsAutoSaveMinutes = new System.Windows.Forms.NumericUpDown();
    this.chkOptionsAutoSave = new DistantWorlds.Controls.CheckBox();

    this.chkOptionsAutoPauseInPopup = new DistantWorlds.Controls.CheckBox();
    this.grpOptionsVolume = new System.Windows.Forms.GroupBox();

    this.grpOptionsControl = new System.Windows.Forms.GroupBox();

    this.btnGameOptionsAdvancedDisplaySettings = new DistantWorlds.Controls.GlassButton();
    this.lblOptionsMainViewZoomSpeed = new System.Windows.Forms.Label();
    this.sldOptionsMainViewZoomSpeed = new DistantWorlds.Controls.ColorSlider();
    this.lblOptionsMainViewStarFieldSize = new System.Windows.Forms.Label();
    this.sldOptionsMainViewStarFieldSize = new DistantWorlds.Controls.ColorSlider();
    this.lblOptionsMainViewGuiScale = new System.Windows.Forms.Label();
    this.sldOptionsMainViewGuiScale = new DistantWorlds.Controls.ColorSlider();
    this.lblOptionsMainViewScrollSpeed = new System.Windows.Forms.Label();
    this.sldOptionsMainViewScrollSpeed = new DistantWorlds.Controls.ColorSlider();
    this.lblOptionsSoundEffectsVolume = new System.Windows.Forms.Label();
    this.lblOptionsMusicVolume = new System.Windows.Forms.Label();
    this.sldOptionsSoundEffectsVolume = new DistantWorlds.Controls.ColorSlider();
    this.sldOptionsMusicVolume = new DistantWorlds.Controls.ColorSlider();
    this.cmbOptionsAutomationMode = new System.Windows.Forms.ComboBox();

    this.grpOptionsPopupMessages = new System.Windows.Forms.GroupBox();
    
    this.chkOptionsPopupMessageExploration =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageIntelligenceMissions =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageResearchBreakthrough =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageEmpireMetDestroyed =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageColonyGainLoss =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageDiplomacyWarTradeSanctions =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageDiplomacyTreaties =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageRequestWarning =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageShipBuilt =new  DistantWorlds.Controls.CheckBox();

    this.pnlOptionsAutomationMode = new System.Windows.Forms.Panel();
    this.lblOptionsAutomationMode = new System.Windows.Forms.Label();

    this.lblOptionsControlOfferPirateMissions = new System.Windows.Forms.Label();
    this.cmbOptionsControlOfferPirateMissions = new System.Windows.Forms.ComboBox();
    this.chkOptionsControlCharacterLocations =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsControlPopulationPolicy =new  DistantWorlds.Controls.CheckBox();
    this.lblOptionsControlColonyFacilities = new System.Windows.Forms.Label();
    this.cmbOptionsControlColonyFacilities = new System.Windows.Forms.ComboBox();
    this.chkOptionsControlResearch =new  DistantWorlds.Controls.CheckBox();
    this.btnGameOptionsEmpireSettings = new DistantWorlds.Controls.GlassButton();
    this.lblOptionsControlColonization = new System.Windows.Forms.Label();
    this.lblOptionsControlDiplomacyTreaties = new System.Windows.Forms.Label();
    this.cmbOptionsControlColonization = new System.Windows.Forms.ComboBox();
    this.lblOptionsControlAttacks = new System.Windows.Forms.Label();
    this.lblOptionsControlConstruction = new System.Windows.Forms.Label();
    this.cmbOptionsControlDiplomacyTreaties = new System.Windows.Forms.ComboBox();
    this.cmbOptionsControlConstruction = new System.Windows.Forms.ComboBox();
    this.cmbOptionsControlAttacks = new System.Windows.Forms.ComboBox();
    this.lblOptionsControlAgentMissions = new System.Windows.Forms.Label();
    this.lblOptionsControlDiplomacyGifts = new System.Windows.Forms.Label();
    this.btnGameOptionsResetAutomationMessages = new DistantWorlds.Controls.GlassButton();
    this.lblOptionsControlDiplomacyOffense = new System.Windows.Forms.Label();
    this.cmbOptionsControlAgentMissions = new System.Windows.Forms.ComboBox();
    this.cmbOptionsControlDiplomacyOffense = new System.Windows.Forms.ComboBox();
    this.cmbOptionsControlDiplomacyGifts = new System.Windows.Forms.ComboBox();
    this.chkOptionsControlTroops =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsControlColonyTaxRates =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsControlFleets =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsControlDesigns =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageConstructionResourceShortage =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageUnderAttackCivilianBases =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageUnderAttackMilitaryShips =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageUnderAttackExplorationShips =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageUnderAttackOtherStateBases =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageUnderAttackColonyConstructionShips =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageUnderAttackColoniesSpaceports =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageUnderAttackCivilianShips =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageShipNeedsRefuelling =new  DistantWorlds.Controls.CheckBox();
    this.chkOptionsPopupMessageShipMissionComplete =new  DistantWorlds.Controls.CheckBox();

    // suspend layouts
    this.grpOptionsAutoSave.SuspendLayout();
    ((System.ComponentModel.ISupportInitialize)this.numOptionsAutoSaveMinutes).BeginInit();
    this.grpOptionsDisplaySettings.SuspendLayout();
    this.grpOptionsVolume.SuspendLayout();
    this.grpOptionsControl.SuspendLayout();
    this.pnlOptionsAutomationMode.SuspendLayout();
    this.grpOptionsPopupMessages.SuspendLayout();


    // main control setup
    this.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
    this.BackgroundImage = (System.Drawing.Image)resources.GetObject("GameOptionsScreenPanel.BackgroundImage");
    this.BorderColor1 = System.Drawing.Color.FromArgb(96, 200, 200, 200);
    this.BorderColor2 = System.Drawing.Color.FromArgb(96, 140, 140, 140);
    this.BorderColor3 = System.Drawing.Color.FromArgb(96, 20, 20, 20);
    this.BorderColor4 = System.Drawing.Color.FromArgb(96, 80, 80, 80);
    this.BorderSize = 3;
    this.Controls.Add(this.btnGameOptionsShowMessages);
    this.Controls.Add(this.chkOptionsLoadedGamesPaused);
    this.Controls.Add(this.grpOptionsAutoSave);
    this.Controls.Add(this.lblOptionsMouseScrollMode);
    this.Controls.Add(this.cmbOptionsMouseScrollWheelBehaviour);
    this.Controls.Add(this.grpOptionsDisplaySettings);
    this.Controls.Add(this.chkOptionsAutoPauseInPopup);
    this.Controls.Add(this.grpOptionsVolume);
    this.Controls.Add(this.grpOptionsControl);
    this.HeaderIcon = (System.Drawing.Image)resources.GetObject("pnlGameOptions.HeaderIcon");
    this.HeaderTitle = "Options";
    this.Location = new System.Drawing.Point(227, 32);
    this.Name = "this";
    this.Size = new System.Drawing.Size(525, 538);
    this.TabIndex = 94;
    this.Visible = false;
    
    // child control setup

    this.btnGameOptionsShowMessages.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        this.btnGameOptionsShowMessages.ClipBackground = false;
        this.btnGameOptionsShowMessages.DelayFrameRefresh = false;
        this.btnGameOptionsShowMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        this.btnGameOptionsShowMessages.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        this.btnGameOptionsShowMessages.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        this.btnGameOptionsShowMessages.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        this.btnGameOptionsShowMessages.IntensifyColors = false;
        this.btnGameOptionsShowMessages.Location = new System.Drawing.Point(12, 493);
        this.btnGameOptionsShowMessages.Name = "btnGameOptionsShowMessages";
        this.btnGameOptionsShowMessages.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        this.btnGameOptionsShowMessages.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        this.btnGameOptionsShowMessages.Size = new System.Drawing.Size(499, 35);
        this.btnGameOptionsShowMessages.TabIndex = 76;
        this.btnGameOptionsShowMessages.Text = "Show Message Options";
        this.btnGameOptionsShowMessages.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.btnGameOptionsShowMessages.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        this.btnGameOptionsShowMessages.ToggledOn = false;
        this.chkOptionsLoadedGamesPaused.AutoSize = true;
        this.chkOptionsLoadedGamesPaused.BackColor = System.Drawing.Color.Transparent;
        this.chkOptionsLoadedGamesPaused.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.chkOptionsLoadedGamesPaused.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.chkOptionsLoadedGamesPaused.Location = new System.Drawing.Point(181, 191);
        this.chkOptionsLoadedGamesPaused.Name = "chkOptionsLoadedGamesPaused";
        this.chkOptionsLoadedGamesPaused.Size = new System.Drawing.Size(177, 17);
        this.chkOptionsLoadedGamesPaused.TabIndex = 75;
        this.chkOptionsLoadedGamesPaused.Text = "Loaded games are paused";
        this.chkOptionsLoadedGamesPaused.UseVisualStyleBackColor = false;
        this.grpOptionsAutoSave.BackColor = System.Drawing.Color.Transparent;
        this.grpOptionsAutoSave.Controls.Add(this.numOptionsAutoSaveMinutes);
        this.grpOptionsAutoSave.Controls.Add(this.chkOptionsAutoSave);
        this.grpOptionsAutoSave.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        this.grpOptionsAutoSave.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.grpOptionsAutoSave.Location = new System.Drawing.Point(12, 192);
        this.grpOptionsAutoSave.Name = "grpOptionsAutoSave";
        this.grpOptionsAutoSave.Size = new System.Drawing.Size(160, 44);
        this.grpOptionsAutoSave.TabIndex = 74;
        this.grpOptionsAutoSave.TabStop = false;
        this.grpOptionsAutoSave.Text = "Auto Save";
        this.numOptionsAutoSaveMinutes.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.numOptionsAutoSaveMinutes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.numOptionsAutoSaveMinutes.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.numOptionsAutoSaveMinutes.Location = new System.Drawing.Point(60, 16);
        this.numOptionsAutoSaveMinutes.Maximum = new decimal(new int[4] { 60, 0, 0, 0 });
        this.numOptionsAutoSaveMinutes.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
        this.numOptionsAutoSaveMinutes.Name = "numOptionsAutoSaveMinutes";
        this.numOptionsAutoSaveMinutes.Size = new System.Drawing.Size(35, 21);
        this.numOptionsAutoSaveMinutes.TabIndex = 31;
        this.numOptionsAutoSaveMinutes.Value = new decimal(new int[4] { 10, 0, 0, 0 });
        this.chkOptionsAutoSave.AutoSize = true;
        this.chkOptionsAutoSave.BackColor = System.Drawing.Color.Transparent;
        this.chkOptionsAutoSave.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.chkOptionsAutoSave.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.chkOptionsAutoSave.Location = new System.Drawing.Point(7, 17);
        this.chkOptionsAutoSave.Name = "chkOptionsAutoSave";
        this.chkOptionsAutoSave.Size = new System.Drawing.Size(144, 17);
        this.chkOptionsAutoSave.TabIndex = 13;
        this.chkOptionsAutoSave.Text = "Every          minutes";
        this.chkOptionsAutoSave.UseVisualStyleBackColor = false;
        this.lblOptionsMouseScrollMode.AutoSize = true;
        this.lblOptionsMouseScrollMode.BackColor = System.Drawing.Color.Transparent;
        this.lblOptionsMouseScrollMode.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.lblOptionsMouseScrollMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.lblOptionsMouseScrollMode.Location = new System.Drawing.Point(177, 215);
        this.lblOptionsMouseScrollMode.Name = "lblOptionsMouseScrollMode";
        this.lblOptionsMouseScrollMode.Size = new System.Drawing.Size(169, 13);
        this.lblOptionsMouseScrollMode.TabIndex = 73;
        this.lblOptionsMouseScrollMode.Text = "Mouse scroll-wheel behavior";
        this.cmbOptionsMouseScrollWheelBehaviour.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.cmbOptionsMouseScrollWheelBehaviour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbOptionsMouseScrollWheelBehaviour.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
        this.cmbOptionsMouseScrollWheelBehaviour.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.cmbOptionsMouseScrollWheelBehaviour.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.cmbOptionsMouseScrollWheelBehaviour.FormattingEnabled = true;
        this.cmbOptionsMouseScrollWheelBehaviour.Items.AddRange(new object[3] { "No movement", "Move to selected item", "Move to mouse cursor location" });
        this.cmbOptionsMouseScrollWheelBehaviour.Location = new System.Drawing.Point(351, 211);
        this.cmbOptionsMouseScrollWheelBehaviour.Name = "cmbOptionsMouseScrollWheelBehaviour";
        this.cmbOptionsMouseScrollWheelBehaviour.Size = new System.Drawing.Size(160, 21);
        this.cmbOptionsMouseScrollWheelBehaviour.TabIndex = 72;
        this.grpOptionsDisplaySettings.BackColor = System.Drawing.Color.Transparent;
        this.grpOptionsDisplaySettings.Controls.Add(this.btnGameOptionsAdvancedDisplaySettings);
        this.grpOptionsDisplaySettings.Controls.Add(this.lblOptionsMainViewZoomSpeed);
        this.grpOptionsDisplaySettings.Controls.Add(this.sldOptionsMainViewZoomSpeed);
        this.grpOptionsDisplaySettings.Controls.Add(this.lblOptionsMainViewStarFieldSize);
        this.grpOptionsDisplaySettings.Controls.Add(this.sldOptionsMainViewStarFieldSize);
        this.grpOptionsDisplaySettings.Controls.Add(this.lblOptionsMainViewGuiScale);
        this.grpOptionsDisplaySettings.Controls.Add(this.sldOptionsMainViewGuiScale);
        this.grpOptionsDisplaySettings.Controls.Add(this.lblOptionsMainViewScrollSpeed);
        this.grpOptionsDisplaySettings.Controls.Add(this.sldOptionsMainViewScrollSpeed);
        this.grpOptionsDisplaySettings.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        this.grpOptionsDisplaySettings.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.grpOptionsDisplaySettings.Location = new System.Drawing.Point(12, 7);
        this.grpOptionsDisplaySettings.Name = "grpOptionsDisplaySettings";
        this.grpOptionsDisplaySettings.Size = new System.Drawing.Size(499, 108);
        this.grpOptionsDisplaySettings.TabIndex = 59;
        this.grpOptionsDisplaySettings.TabStop = false;
        this.grpOptionsDisplaySettings.Text = "Display Settings";
        this.btnGameOptionsAdvancedDisplaySettings.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        this.btnGameOptionsAdvancedDisplaySettings.ClipBackground = false;
        this.btnGameOptionsAdvancedDisplaySettings.DelayFrameRefresh = false;
        this.btnGameOptionsAdvancedDisplaySettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        this.btnGameOptionsAdvancedDisplaySettings.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        this.btnGameOptionsAdvancedDisplaySettings.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        this.btnGameOptionsAdvancedDisplaySettings.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        this.btnGameOptionsAdvancedDisplaySettings.IntensifyColors = false;
        this.btnGameOptionsAdvancedDisplaySettings.Location = new System.Drawing.Point(335, 80);
        this.btnGameOptionsAdvancedDisplaySettings.Name = "btnGameOptionsAdvancedDisplaySettings";
        this.btnGameOptionsAdvancedDisplaySettings.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        this.btnGameOptionsAdvancedDisplaySettings.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        this.btnGameOptionsAdvancedDisplaySettings.Size = new System.Drawing.Size(150, 22);
        this.btnGameOptionsAdvancedDisplaySettings.TabIndex = 66;
        this.btnGameOptionsAdvancedDisplaySettings.Text = "Advanced Settings...";
        this.btnGameOptionsAdvancedDisplaySettings.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.btnGameOptionsAdvancedDisplaySettings.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        this.btnGameOptionsAdvancedDisplaySettings.ToggledOn = false;
        this.lblOptionsMainViewZoomSpeed.AutoSize = true;
        this.lblOptionsMainViewZoomSpeed.BackColor = System.Drawing.Color.Transparent;
        this.lblOptionsMainViewZoomSpeed.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.lblOptionsMainViewZoomSpeed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.lblOptionsMainViewZoomSpeed.Location = new System.Drawing.Point(10, 43);
        this.lblOptionsMainViewZoomSpeed.Name = "lblOptionsMainViewZoomSpeed";
        this.lblOptionsMainViewZoomSpeed.Size = new System.Drawing.Size(80, 13);
        this.lblOptionsMainViewZoomSpeed.TabIndex = 63;
        this.lblOptionsMainViewZoomSpeed.Text = "Zoom Speed";
        this.sldOptionsMainViewZoomSpeed.BackColor = System.Drawing.Color.Transparent;
        this.sldOptionsMainViewZoomSpeed.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        this.sldOptionsMainViewZoomSpeed.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMainViewZoomSpeed.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        this.sldOptionsMainViewZoomSpeed.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        this.sldOptionsMainViewZoomSpeed.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMainViewZoomSpeed.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMainViewZoomSpeed.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.sldOptionsMainViewZoomSpeed.ForeColor = System.Drawing.Color.White;
        this.sldOptionsMainViewZoomSpeed.LargeChange = 5u;
        this.sldOptionsMainViewZoomSpeed.Location = new System.Drawing.Point(95, 41);
        this.sldOptionsMainViewZoomSpeed.Minimum = 10;
        this.sldOptionsMainViewZoomSpeed.Name = "sldOptionsMainViewZoomSpeed";
        this.sldOptionsMainViewZoomSpeed.Size = new System.Drawing.Size(390, 16);
        this.sldOptionsMainViewZoomSpeed.SmallChange = 1u;
        this.sldOptionsMainViewZoomSpeed.TabIndex = 62;
        this.sldOptionsMainViewZoomSpeed.Text = "colorSlider1";
        this.sldOptionsMainViewZoomSpeed.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMainViewZoomSpeed.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMainViewZoomSpeed.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMainViewZoomSpeed.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        this.sldOptionsMainViewZoomSpeed.ThumbSize = 20;
        this.sldOptionsMainViewZoomSpeed.Value = 30;
        this.lblOptionsMainViewStarFieldSize.AutoSize = true;
        this.lblOptionsMainViewStarFieldSize.BackColor = System.Drawing.Color.Transparent;
        this.lblOptionsMainViewStarFieldSize.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.lblOptionsMainViewStarFieldSize.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.lblOptionsMainViewStarFieldSize.Location = new System.Drawing.Point(10, 63);
        this.lblOptionsMainViewStarFieldSize.Name = "lblOptionsMainViewStarFieldSize";
        this.lblOptionsMainViewStarFieldSize.Size = new System.Drawing.Size(78, 13);
        this.lblOptionsMainViewStarFieldSize.TabIndex = 61;
        this.lblOptionsMainViewStarFieldSize.Text = "Star Density";
        this.sldOptionsMainViewStarFieldSize.BackColor = System.Drawing.Color.Transparent;
        this.sldOptionsMainViewStarFieldSize.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        this.sldOptionsMainViewStarFieldSize.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMainViewStarFieldSize.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        this.sldOptionsMainViewStarFieldSize.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        this.sldOptionsMainViewStarFieldSize.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMainViewStarFieldSize.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMainViewStarFieldSize.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.sldOptionsMainViewStarFieldSize.ForeColor = System.Drawing.Color.White;
        this.sldOptionsMainViewStarFieldSize.LargeChange = 5u;
        this.sldOptionsMainViewStarFieldSize.Location = new System.Drawing.Point(95, 62);
        this.sldOptionsMainViewStarFieldSize.Maximum = 2000;
        this.sldOptionsMainViewStarFieldSize.Minimum = 50;
        this.sldOptionsMainViewStarFieldSize.Name = "sldOptionsMainViewStarFieldSize";
        this.sldOptionsMainViewStarFieldSize.Size = new System.Drawing.Size(390, 16);
        this.sldOptionsMainViewStarFieldSize.SmallChange = 1u;
        this.sldOptionsMainViewStarFieldSize.TabIndex = 60;
        this.sldOptionsMainViewStarFieldSize.Text = "colorSlider1";
        this.sldOptionsMainViewStarFieldSize.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMainViewStarFieldSize.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMainViewStarFieldSize.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMainViewStarFieldSize.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        this.sldOptionsMainViewStarFieldSize.ThumbSize = 20;
        this.sldOptionsMainViewStarFieldSize.Value = 1000;
        this.lblOptionsMainViewGuiScale.AutoSize = true;
        this.lblOptionsMainViewGuiScale.BackColor = System.Drawing.Color.Transparent;
        this.lblOptionsMainViewGuiScale.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.lblOptionsMainViewGuiScale.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.lblOptionsMainViewGuiScale.Location = new System.Drawing.Point(95, 75);
        this.lblOptionsMainViewGuiScale.Name = "lblOptionsMainViewGuiScale";
        this.lblOptionsMainViewGuiScale.Size = new System.Drawing.Size(78, 13);
        this.lblOptionsMainViewGuiScale.TabIndex = 999;
        this.lblOptionsMainViewGuiScale.Text = "GUI Scale";
        this.sldOptionsMainViewGuiScale.BackColor = System.Drawing.Color.Transparent;
        this.sldOptionsMainViewGuiScale.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        this.sldOptionsMainViewGuiScale.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMainViewGuiScale.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        this.sldOptionsMainViewGuiScale.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        this.sldOptionsMainViewGuiScale.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMainViewGuiScale.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMainViewGuiScale.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.sldOptionsMainViewGuiScale.ForeColor = System.Drawing.Color.White;
        this.sldOptionsMainViewGuiScale.LargeChange = 5u;
        this.sldOptionsMainViewGuiScale.Location = new System.Drawing.Point(95, 62);
        this.sldOptionsMainViewGuiScale.Maximum = 4000;
        this.sldOptionsMainViewGuiScale.Minimum = 500;
        this.sldOptionsMainViewGuiScale.Name = "sldOptionsMainViewGuiScale";
        this.sldOptionsMainViewGuiScale.Size = new System.Drawing.Size(390, 16);
        this.sldOptionsMainViewGuiScale.SmallChange = 1u;
        this.sldOptionsMainViewGuiScale.TabIndex = 999;
        this.sldOptionsMainViewGuiScale.Text = "colorSlider1";
        this.sldOptionsMainViewGuiScale.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMainViewGuiScale.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMainViewGuiScale.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMainViewGuiScale.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        this.sldOptionsMainViewGuiScale.ThumbSize = 20;
        this.sldOptionsMainViewGuiScale.Value = 1000;
        this.lblOptionsMainViewScrollSpeed.AutoSize = true;
        this.lblOptionsMainViewScrollSpeed.BackColor = System.Drawing.Color.Transparent;
        this.lblOptionsMainViewScrollSpeed.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.lblOptionsMainViewScrollSpeed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.lblOptionsMainViewScrollSpeed.Location = new System.Drawing.Point(10, 21);
        this.lblOptionsMainViewScrollSpeed.Name = "lblOptionsMainViewScrollSpeed";
        this.lblOptionsMainViewScrollSpeed.Size = new System.Drawing.Size(79, 13);
        this.lblOptionsMainViewScrollSpeed.TabIndex = 59;
        this.lblOptionsMainViewScrollSpeed.Text = "Scroll Speed";
        this.sldOptionsMainViewScrollSpeed.BackColor = System.Drawing.Color.Transparent;
        this.sldOptionsMainViewScrollSpeed.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        this.sldOptionsMainViewScrollSpeed.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMainViewScrollSpeed.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        this.sldOptionsMainViewScrollSpeed.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        this.sldOptionsMainViewScrollSpeed.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMainViewScrollSpeed.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMainViewScrollSpeed.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.sldOptionsMainViewScrollSpeed.ForeColor = System.Drawing.Color.White;
        this.sldOptionsMainViewScrollSpeed.LargeChange = 5u;
        this.sldOptionsMainViewScrollSpeed.Location = new System.Drawing.Point(95, 20);
        this.sldOptionsMainViewScrollSpeed.Minimum = 1;
        this.sldOptionsMainViewScrollSpeed.Name = "sldOptionsMainViewScrollSpeed";
        this.sldOptionsMainViewScrollSpeed.Size = new System.Drawing.Size(390, 16);
        this.sldOptionsMainViewScrollSpeed.SmallChange = 1u;
        this.sldOptionsMainViewScrollSpeed.TabIndex = 58;
        this.sldOptionsMainViewScrollSpeed.Text = "colorSlider1";
        this.sldOptionsMainViewScrollSpeed.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMainViewScrollSpeed.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMainViewScrollSpeed.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMainViewScrollSpeed.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        this.sldOptionsMainViewScrollSpeed.ThumbSize = 20;
        this.chkOptionsAutoPauseInPopup.AutoSize = true;
        this.chkOptionsAutoPauseInPopup.BackColor = System.Drawing.Color.Transparent;
        this.chkOptionsAutoPauseInPopup.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.chkOptionsAutoPauseInPopup.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.chkOptionsAutoPauseInPopup.Location = new System.Drawing.Point(12, 10);
        this.chkOptionsAutoPauseInPopup.Name = "chkOptionsAutoPauseInPopup";
        this.chkOptionsAutoPauseInPopup.Size = new System.Drawing.Size(192, 17);
        this.chkOptionsAutoPauseInPopup.TabIndex = 15;
        this.chkOptionsAutoPauseInPopup.Text = "Auto Pause in Game Screens";
        this.chkOptionsAutoPauseInPopup.UseVisualStyleBackColor = false;
        this.chkOptionsAutoPauseInPopup.Visible = false;
        this.grpOptionsVolume.BackColor = System.Drawing.Color.Transparent;
        this.grpOptionsVolume.Controls.Add(this.lblOptionsSoundEffectsVolume);
        this.grpOptionsVolume.Controls.Add(this.lblOptionsMusicVolume);
        this.grpOptionsVolume.Controls.Add(this.sldOptionsSoundEffectsVolume);
        this.grpOptionsVolume.Controls.Add(this.sldOptionsMusicVolume);
        this.grpOptionsVolume.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        this.grpOptionsVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.grpOptionsVolume.Location = new System.Drawing.Point(12, 121);
        this.grpOptionsVolume.Name = "grpOptionsVolume";
        this.grpOptionsVolume.Size = new System.Drawing.Size(499, 64);
        this.grpOptionsVolume.TabIndex = 10;
        this.grpOptionsVolume.TabStop = false;
        this.grpOptionsVolume.Text = "Sound Volume";
        this.lblOptionsSoundEffectsVolume.AutoSize = true;
        this.lblOptionsSoundEffectsVolume.BackColor = System.Drawing.Color.Transparent;
        this.lblOptionsSoundEffectsVolume.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.lblOptionsSoundEffectsVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.lblOptionsSoundEffectsVolume.Location = new System.Drawing.Point(17, 40);
        this.lblOptionsSoundEffectsVolume.Name = "lblOptionsSoundEffectsVolume";
        this.lblOptionsSoundEffectsVolume.Size = new System.Drawing.Size(45, 13);
        this.lblOptionsSoundEffectsVolume.TabIndex = 13;
        this.lblOptionsSoundEffectsVolume.Text = "Effects";
        this.lblOptionsMusicVolume.AutoSize = true;
        this.lblOptionsMusicVolume.BackColor = System.Drawing.Color.Transparent;
        this.lblOptionsMusicVolume.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.lblOptionsMusicVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.lblOptionsMusicVolume.Location = new System.Drawing.Point(17, 19);
        this.lblOptionsMusicVolume.Name = "lblOptionsMusicVolume";
        this.lblOptionsMusicVolume.Size = new System.Drawing.Size(38, 13);
        this.lblOptionsMusicVolume.TabIndex = 12;
        this.lblOptionsMusicVolume.Text = "Music";
        this.sldOptionsSoundEffectsVolume.BackColor = System.Drawing.Color.Transparent;
        this.sldOptionsSoundEffectsVolume.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        this.sldOptionsSoundEffectsVolume.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsSoundEffectsVolume.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        this.sldOptionsSoundEffectsVolume.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        this.sldOptionsSoundEffectsVolume.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsSoundEffectsVolume.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsSoundEffectsVolume.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.sldOptionsSoundEffectsVolume.ForeColor = System.Drawing.Color.White;
        this.sldOptionsSoundEffectsVolume.LargeChange = 5u;
        this.sldOptionsSoundEffectsVolume.Location = new System.Drawing.Point(81, 39);
        this.sldOptionsSoundEffectsVolume.Name = "sldOptionsSoundEffectsVolume";
        this.sldOptionsSoundEffectsVolume.Size = new System.Drawing.Size(408, 16);
        this.sldOptionsSoundEffectsVolume.SmallChange = 1u;
        this.sldOptionsSoundEffectsVolume.TabIndex = 11;
        this.sldOptionsSoundEffectsVolume.Text = "colorSlider1";
        this.sldOptionsSoundEffectsVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsSoundEffectsVolume.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsSoundEffectsVolume.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsSoundEffectsVolume.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        this.sldOptionsSoundEffectsVolume.ThumbSize = 20;
        this.sldOptionsMusicVolume.BackColor = System.Drawing.Color.Transparent;
        this.sldOptionsMusicVolume.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        this.sldOptionsMusicVolume.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMusicVolume.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        this.sldOptionsMusicVolume.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        this.sldOptionsMusicVolume.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMusicVolume.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMusicVolume.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.sldOptionsMusicVolume.ForeColor = System.Drawing.Color.White;
        this.sldOptionsMusicVolume.LargeChange = 5u;
        this.sldOptionsMusicVolume.Location = new System.Drawing.Point(81, 18);
        this.sldOptionsMusicVolume.Name = "sldOptionsMusicVolume";
        this.sldOptionsMusicVolume.Size = new System.Drawing.Size(408, 16);
        this.sldOptionsMusicVolume.SmallChange = 1u;
        this.sldOptionsMusicVolume.TabIndex = 10;
        this.sldOptionsMusicVolume.Text = "colorSlider1";
        this.sldOptionsMusicVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        this.sldOptionsMusicVolume.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.sldOptionsMusicVolume.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        this.sldOptionsMusicVolume.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        this.sldOptionsMusicVolume.ThumbSize = 20;
        this.grpOptionsControl.BackColor = System.Drawing.Color.Transparent;
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlOfferPirateMissions);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlOfferPirateMissions);
        this.grpOptionsControl.Controls.Add(this.chkOptionsControlCharacterLocations);
        this.grpOptionsControl.Controls.Add(this.chkOptionsControlPopulationPolicy);
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlColonyFacilities);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlColonyFacilities);
        this.grpOptionsControl.Controls.Add(this.chkOptionsControlResearch);
        this.grpOptionsControl.Controls.Add(this.btnGameOptionsEmpireSettings);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsAutomationMode);
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlColonization);
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlDiplomacyTreaties);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlColonization);
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlAttacks);
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlConstruction);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlDiplomacyTreaties);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlConstruction);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlAttacks);
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlAgentMissions);
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlDiplomacyGifts);
        this.grpOptionsControl.Controls.Add(this.btnGameOptionsResetAutomationMessages);
        this.grpOptionsControl.Controls.Add(this.lblOptionsControlDiplomacyOffense);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlAgentMissions);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlDiplomacyOffense);
        this.grpOptionsControl.Controls.Add(this.cmbOptionsControlDiplomacyGifts);
        this.grpOptionsControl.Controls.Add(this.chkOptionsControlTroops);
        this.grpOptionsControl.Controls.Add(this.chkOptionsControlColonyTaxRates);
        this.grpOptionsControl.Controls.Add(this.chkOptionsControlFleets);
        this.grpOptionsControl.Controls.Add(this.chkOptionsControlDesigns);
        this.grpOptionsControl.Controls.Add(this.pnlOptionsAutomationMode);
        this.grpOptionsControl.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        this.grpOptionsControl.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.grpOptionsControl.Location = new System.Drawing.Point(12, 242);
        this.grpOptionsControl.Name = "grpOptionsControl";
        this.grpOptionsControl.Size = new System.Drawing.Size(499, 241);
        this.grpOptionsControl.TabIndex = 4;
        this.grpOptionsControl.TabStop = false;
        this.grpOptionsControl.Text = "Automation";
        this.cmbOptionsAutomationMode.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        this.cmbOptionsAutomationMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbOptionsAutomationMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
        this.cmbOptionsAutomationMode.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        this.cmbOptionsAutomationMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        this.cmbOptionsAutomationMode.FormattingEnabled = true;
        this.cmbOptionsAutomationMode.Items.AddRange(new object[8] { "(Custom)", "Default", "Expert (None)", "Rule in Absence (Full)", "Expansion", "War and Combat", "Diplomacy", "Spy Master" });
        this.cmbOptionsAutomationMode.Location = new System.Drawing.Point(53, 19);
        this.cmbOptionsAutomationMode.Name = "cmbOptionsAutomationMode";
        this.cmbOptionsAutomationMode.Size = new System.Drawing.Size(116, 21);
        this.cmbOptionsAutomationMode.TabIndex = 66;
        
            this.lblOptionsControlOfferPirateMissions.AutoSize = true;
            this.lblOptionsControlOfferPirateMissions.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlOfferPirateMissions.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlOfferPirateMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlOfferPirateMissions.Location = new System.Drawing.Point(188, 214);
            this.lblOptionsControlOfferPirateMissions.Name = "lblOptionsControlOfferPirateMissions";
            this.lblOptionsControlOfferPirateMissions.Size = new System.Drawing.Size(124, 13);
            this.lblOptionsControlOfferPirateMissions.TabIndex = 79;
            this.lblOptionsControlOfferPirateMissions.Text = "Offer Pirate Missions";
            this.cmbOptionsControlOfferPirateMissions.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlOfferPirateMissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlOfferPirateMissions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlOfferPirateMissions.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlOfferPirateMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlOfferPirateMissions.FormattingEnabled = true;
            this.cmbOptionsControlOfferPirateMissions.Items.AddRange(new object[3] { "Control manually", "Suggest new colony facilities", "Fully automate" });
            this.cmbOptionsControlOfferPirateMissions.Location = new System.Drawing.Point(329, 211);
            this.cmbOptionsControlOfferPirateMissions.Name = "cmbOptionsControlOfferPirateMissions";
            this.cmbOptionsControlOfferPirateMissions.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlOfferPirateMissions.TabIndex = 78;
            
            this.chkOptionsControlCharacterLocations.AutoSize = true;
            this.chkOptionsControlCharacterLocations.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsControlCharacterLocations.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsControlCharacterLocations.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsControlCharacterLocations.Location = new System.Drawing.Point(17, 94);
            this.chkOptionsControlCharacterLocations.Name = "chkOptionsControlCharacterLocations";
            this.chkOptionsControlCharacterLocations.Size = new System.Drawing.Size(140, 17);
            this.chkOptionsControlCharacterLocations.TabIndex = 77;
            this.chkOptionsControlCharacterLocations.Text = "Character Locations";
            this.chkOptionsControlCharacterLocations.UseVisualStyleBackColor = false;
            this.chkOptionsControlPopulationPolicy.AutoSize = true;
            this.chkOptionsControlPopulationPolicy.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsControlPopulationPolicy.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsControlPopulationPolicy.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsControlPopulationPolicy.Location = new System.Drawing.Point(18, 150);
            this.chkOptionsControlPopulationPolicy.Name = "chkOptionsControlPopulationPolicy";
            this.chkOptionsControlPopulationPolicy.Size = new System.Drawing.Size(175, 17);
            this.chkOptionsControlPopulationPolicy.TabIndex = 76;
            this.chkOptionsControlPopulationPolicy.Text = "Colony Population Policies";
            this.chkOptionsControlPopulationPolicy.UseVisualStyleBackColor = false;
            this.lblOptionsControlColonyFacilities.AutoSize = true;
            this.lblOptionsControlColonyFacilities.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlColonyFacilities.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlColonyFacilities.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlColonyFacilities.Location = new System.Drawing.Point(188, 190);
            this.lblOptionsControlColonyFacilities.Name = "lblOptionsControlColonyFacilities";
            this.lblOptionsControlColonyFacilities.Size = new System.Drawing.Size(139, 13);
            this.lblOptionsControlColonyFacilities.TabIndex = 75;
            this.lblOptionsControlColonyFacilities.Text = "Colony Facility Building";
            this.cmbOptionsControlColonyFacilities.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlColonyFacilities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlColonyFacilities.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlColonyFacilities.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlColonyFacilities.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlColonyFacilities.FormattingEnabled = true;
            this.cmbOptionsControlColonyFacilities.Items.AddRange(new object[3] { "Control manually", "Suggest new colony facilities", "Fully automate" });
            this.cmbOptionsControlColonyFacilities.Location = new System.Drawing.Point(329, 187);
            this.cmbOptionsControlColonyFacilities.Name = "cmbOptionsControlColonyFacilities";
            this.cmbOptionsControlColonyFacilities.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlColonyFacilities.TabIndex = 74;
this.chkOptionsControlResearch.AutoSize = true;
            this.chkOptionsControlResearch.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsControlResearch.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsControlResearch.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsControlResearch.Location = new System.Drawing.Point(182, 189);
            this.chkOptionsControlResearch.Name = "chkOptionsControlResearch";
            this.chkOptionsControlResearch.Size = new System.Drawing.Size(79, 17);
            this.chkOptionsControlResearch.TabIndex = 72;
            this.chkOptionsControlResearch.Text = "Research";
            this.chkOptionsControlResearch.UseVisualStyleBackColor = false;
            this.btnGameOptionsEmpireSettings.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.btnGameOptionsEmpireSettings.ClipBackground = false;
            this.btnGameOptionsEmpireSettings.DelayFrameRefresh = false;
            this.btnGameOptionsEmpireSettings.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnGameOptionsEmpireSettings.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.btnGameOptionsEmpireSettings.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            this.btnGameOptionsEmpireSettings.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            this.btnGameOptionsEmpireSettings.IntensifyColors = false;
            this.btnGameOptionsEmpireSettings.Location = new System.Drawing.Point(30, 166);
            this.btnGameOptionsEmpireSettings.Name = "btnGameOptionsEmpireSettings";
            this.btnGameOptionsEmpireSettings.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            this.btnGameOptionsEmpireSettings.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            this.btnGameOptionsEmpireSettings.Size = new System.Drawing.Size(144, 31);
            this.btnGameOptionsEmpireSettings.TabIndex = 71;
            this.btnGameOptionsEmpireSettings.Text = "Other Empire Settings";
            this.btnGameOptionsEmpireSettings.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.btnGameOptionsEmpireSettings.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGameOptionsEmpireSettings.ToggledOn = false;
            this.lblOptionsControlColonization.AutoSize = true;
            this.lblOptionsControlColonization.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlColonization.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlColonization.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlColonization.Location = new System.Drawing.Point(249, 22);
            this.lblOptionsControlColonization.Name = "lblOptionsControlColonization";
            this.lblOptionsControlColonization.Size = new System.Drawing.Size(77, 13);
            this.lblOptionsControlColonization.TabIndex = 65;
            this.lblOptionsControlColonization.Text = "Colonization";
            this.lblOptionsControlDiplomacyTreaties.AutoSize = true;
            this.lblOptionsControlDiplomacyTreaties.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlDiplomacyTreaties.Location = new System.Drawing.Point(273, 143);
            this.lblOptionsControlDiplomacyTreaties.Name = "lblOptionsControlDiplomacyTreaties";
            this.lblOptionsControlDiplomacyTreaties.Size = new System.Drawing.Size(53, 13);
            this.lblOptionsControlDiplomacyTreaties.TabIndex = 59;
            this.lblOptionsControlDiplomacyTreaties.Text = "Treaties";
            this.cmbOptionsControlColonization.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlColonization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlColonization.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlColonization.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlColonization.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlColonization.FormattingEnabled = true;
            this.cmbOptionsControlColonization.Items.AddRange(new object[3] { "Control manually", "Suggest new colonies", "Fully automate" });
            this.cmbOptionsControlColonization.Location = new System.Drawing.Point(329, 19);
            this.cmbOptionsControlColonization.Name = "cmbOptionsControlColonization";
            this.cmbOptionsControlColonization.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlColonization.TabIndex = 64;
            this.lblOptionsControlAttacks.AutoSize = true;
            this.lblOptionsControlAttacks.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlAttacks.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlAttacks.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlAttacks.Location = new System.Drawing.Point(179, 95);
            this.lblOptionsControlAttacks.Name = "lblOptionsControlAttacks";
            this.lblOptionsControlAttacks.Size = new System.Drawing.Size(147, 13);
            this.lblOptionsControlAttacks.TabIndex = 54;
            this.lblOptionsControlAttacks.Text = "Attacks Against Enemies";
            this.lblOptionsControlConstruction.AutoSize = true;
            this.lblOptionsControlConstruction.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlConstruction.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlConstruction.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlConstruction.Location = new System.Drawing.Point(246, 46);
            this.lblOptionsControlConstruction.Name = "lblOptionsControlConstruction";
            this.lblOptionsControlConstruction.Size = new System.Drawing.Size(81, 13);
            this.lblOptionsControlConstruction.TabIndex = 63;
            this.lblOptionsControlConstruction.Text = "Ship Building";
            this.cmbOptionsControlDiplomacyTreaties.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlDiplomacyTreaties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlDiplomacyTreaties.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlDiplomacyTreaties.FormattingEnabled = true;
            this.cmbOptionsControlDiplomacyTreaties.Items.AddRange(new object[3] { "Control manually", "Suggest new treaties", "Fully automate" });
            this.cmbOptionsControlDiplomacyTreaties.Location = new System.Drawing.Point(329, 139);
            this.cmbOptionsControlDiplomacyTreaties.Name = "cmbOptionsControlDiplomacyTreaties";
            this.cmbOptionsControlDiplomacyTreaties.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlDiplomacyTreaties.TabIndex = 58;
            this.cmbOptionsControlConstruction.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlConstruction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlConstruction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlConstruction.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlConstruction.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlConstruction.FormattingEnabled = true;
            this.cmbOptionsControlConstruction.Items.AddRange(new object[3] { "Control manually", "Suggest new ships and bases", "Fully automate" });
            this.cmbOptionsControlConstruction.Location = new System.Drawing.Point(329, 43);
            this.cmbOptionsControlConstruction.Name = "cmbOptionsControlConstruction";
            this.cmbOptionsControlConstruction.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlConstruction.TabIndex = 62;
            this.cmbOptionsControlAttacks.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlAttacks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlAttacks.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlAttacks.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlAttacks.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlAttacks.FormattingEnabled = true;
            this.cmbOptionsControlAttacks.Items.AddRange(new object[3] { "Control manually", "Suggest attack targets", "Fully automate" });
            this.cmbOptionsControlAttacks.Location = new System.Drawing.Point(329, 91);
            this.cmbOptionsControlAttacks.Name = "cmbOptionsControlAttacks";
            this.cmbOptionsControlAttacks.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlAttacks.TabIndex = 53;
            this.lblOptionsControlAgentMissions.AutoSize = true;
            this.lblOptionsControlAgentMissions.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlAgentMissions.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlAgentMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlAgentMissions.Location = new System.Drawing.Point(236, 70);
            this.lblOptionsControlAgentMissions.Name = "lblOptionsControlAgentMissions";
            this.lblOptionsControlAgentMissions.Size = new System.Drawing.Size(91, 13);
            this.lblOptionsControlAgentMissions.TabIndex = 61;
            this.lblOptionsControlAgentMissions.Text = "Agent Missions";
            this.lblOptionsControlDiplomacyGifts.AutoSize = true;
            this.lblOptionsControlDiplomacyGifts.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlDiplomacyGifts.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlDiplomacyGifts.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlDiplomacyGifts.Location = new System.Drawing.Point(179, 118);
            this.lblOptionsControlDiplomacyGifts.Name = "lblOptionsControlDiplomacyGifts";
            this.lblOptionsControlDiplomacyGifts.Size = new System.Drawing.Size(147, 13);
            this.lblOptionsControlDiplomacyGifts.TabIndex = 56;
            this.lblOptionsControlDiplomacyGifts.Text = "Sending Diplomatic Gifts";
            this.btnGameOptionsResetAutomationMessages.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.btnGameOptionsResetAutomationMessages.ClipBackground = false;
            this.btnGameOptionsResetAutomationMessages.DelayFrameRefresh = false;
            this.btnGameOptionsResetAutomationMessages.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnGameOptionsResetAutomationMessages.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.btnGameOptionsResetAutomationMessages.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            this.btnGameOptionsResetAutomationMessages.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            this.btnGameOptionsResetAutomationMessages.IntensifyColors = false;
            this.btnGameOptionsResetAutomationMessages.Location = new System.Drawing.Point(146, 46);
            this.btnGameOptionsResetAutomationMessages.Name = "btnGameOptionsResetAutomationMessages";
            this.btnGameOptionsResetAutomationMessages.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            this.btnGameOptionsResetAutomationMessages.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            this.btnGameOptionsResetAutomationMessages.Size = new System.Drawing.Size(144, 31);
            this.btnGameOptionsResetAutomationMessages.TabIndex = 16;
            this.btnGameOptionsResetAutomationMessages.Text = "Reset Warnings";
            this.btnGameOptionsResetAutomationMessages.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.btnGameOptionsResetAutomationMessages.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGameOptionsResetAutomationMessages.ToggledOn = false;
            this.lblOptionsControlDiplomacyOffense.AutoSize = true;
            this.lblOptionsControlDiplomacyOffense.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsControlDiplomacyOffense.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsControlDiplomacyOffense.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsControlDiplomacyOffense.Location = new System.Drawing.Point(188, 166);
            this.lblOptionsControlDiplomacyOffense.Name = "lblOptionsControlDiplomacyOffense";
            this.lblOptionsControlDiplomacyOffense.Size = new System.Drawing.Size(138, 13);
            this.lblOptionsControlDiplomacyOffense.TabIndex = 59;
            this.lblOptionsControlDiplomacyOffense.Text = "War && Trade Sanctions";
            this.cmbOptionsControlAgentMissions.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlAgentMissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlAgentMissions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlAgentMissions.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlAgentMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlAgentMissions.FormattingEnabled = true;
            this.cmbOptionsControlAgentMissions.Items.AddRange(new object[3] { "Control manually", "Suggest offensive missions", "Fully automate" });
            this.cmbOptionsControlAgentMissions.Location = new System.Drawing.Point(329, 67);
            this.cmbOptionsControlAgentMissions.Name = "cmbOptionsControlAgentMissions";
            this.cmbOptionsControlAgentMissions.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlAgentMissions.TabIndex = 60;
            this.cmbOptionsControlDiplomacyOffense.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlDiplomacyOffense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlDiplomacyOffense.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlDiplomacyOffense.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlDiplomacyOffense.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlDiplomacyOffense.FormattingEnabled = true;
            this.cmbOptionsControlDiplomacyOffense.Items.AddRange(new object[3] { "Control manually", "Suggest war and trade sanctions", "Fully automate" });
            this.cmbOptionsControlDiplomacyOffense.Location = new System.Drawing.Point(329, 163);
            this.cmbOptionsControlDiplomacyOffense.Name = "cmbOptionsControlDiplomacyOffense";
            this.cmbOptionsControlDiplomacyOffense.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlDiplomacyOffense.TabIndex = 58;
            this.cmbOptionsControlDiplomacyGifts.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.cmbOptionsControlDiplomacyGifts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsControlDiplomacyGifts.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOptionsControlDiplomacyGifts.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.cmbOptionsControlDiplomacyGifts.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.cmbOptionsControlDiplomacyGifts.FormattingEnabled = true;
            this.cmbOptionsControlDiplomacyGifts.Items.AddRange(new object[3] { "Control manually", "Suggest gifts to empires", "Fully automate" });
            this.cmbOptionsControlDiplomacyGifts.Location = new System.Drawing.Point(329, 115);
            this.cmbOptionsControlDiplomacyGifts.Name = "cmbOptionsControlDiplomacyGifts";
            this.cmbOptionsControlDiplomacyGifts.Size = new System.Drawing.Size(160, 21);
            this.cmbOptionsControlDiplomacyGifts.TabIndex = 55;
            this.chkOptionsControlTroops.AutoSize = true;
            this.chkOptionsControlTroops.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsControlTroops.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsControlTroops.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsControlTroops.Location = new System.Drawing.Point(20, 114);
            this.chkOptionsControlTroops.Name = "chkOptionsControlTroops";
            this.chkOptionsControlTroops.Size = new System.Drawing.Size(132, 17);
            this.chkOptionsControlTroops.TabIndex = 7;
            this.chkOptionsControlTroops.Text = "Troop Recruitment";
            this.chkOptionsControlTroops.UseVisualStyleBackColor = false;
            this.chkOptionsControlColonyTaxRates.AutoSize = true;
            this.chkOptionsControlColonyTaxRates.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsControlColonyTaxRates.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsControlColonyTaxRates.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsControlColonyTaxRates.Location = new System.Drawing.Point(25, 57);
            this.chkOptionsControlColonyTaxRates.Name = "chkOptionsControlColonyTaxRates";
            this.chkOptionsControlColonyTaxRates.Size = new System.Drawing.Size(127, 17);
            this.chkOptionsControlColonyTaxRates.TabIndex = 10;
            this.chkOptionsControlColonyTaxRates.Text = "Colony Tax Rates";
            this.chkOptionsControlColonyTaxRates.UseVisualStyleBackColor = false;
            this.chkOptionsControlFleets.AutoSize = true;
            this.chkOptionsControlFleets.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsControlFleets.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsControlFleets.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsControlFleets.Location = new System.Drawing.Point(38, 133);
            this.chkOptionsControlFleets.Name = "chkOptionsControlFleets";
            this.chkOptionsControlFleets.Size = new System.Drawing.Size(114, 17);
            this.chkOptionsControlFleets.TabIndex = 8;
            this.chkOptionsControlFleets.Text = "Fleet Formation";
            this.chkOptionsControlFleets.UseVisualStyleBackColor = false;
            this.chkOptionsControlDesigns.AutoSize = true;
            this.chkOptionsControlDesigns.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsControlDesigns.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsControlDesigns.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsControlDesigns.Location = new System.Drawing.Point(58, 76);
            this.chkOptionsControlDesigns.Name = "chkOptionsControlDesigns";
            this.chkOptionsControlDesigns.Size = new System.Drawing.Size(94, 17);
            this.chkOptionsControlDesigns.TabIndex = 5;
            this.chkOptionsControlDesigns.Text = "Ship Design";
            this.chkOptionsControlDesigns.UseVisualStyleBackColor = false;
            this.pnlOptionsAutomationMode.BackColor = System.Drawing.Color.FromArgb(128, 128, 0, 64);
            this.pnlOptionsAutomationMode.Controls.Add(this.lblOptionsAutomationMode);
            this.pnlOptionsAutomationMode.Location = new System.Drawing.Point(10, 15);
            this.pnlOptionsAutomationMode.Name = "pnlOptionsAutomationMode";
            this.pnlOptionsAutomationMode.Size = new System.Drawing.Size(150, 30);
            this.pnlOptionsAutomationMode.TabIndex = 68;
            this.lblOptionsAutomationMode.AutoSize = true;
            this.lblOptionsAutomationMode.BackColor = System.Drawing.Color.Transparent;
            this.lblOptionsAutomationMode.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblOptionsAutomationMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblOptionsAutomationMode.Location = new System.Drawing.Point(4, 7);
            this.lblOptionsAutomationMode.Name = "lblOptionsAutomationMode";
            this.lblOptionsAutomationMode.Size = new System.Drawing.Size(37, 13);
            this.lblOptionsAutomationMode.TabIndex = 67;
            this.lblOptionsAutomationMode.Text = "Mode";
            this.grpOptionsPopupMessages.BackColor = System.Drawing.Color.Transparent;
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageConstructionResourceShortage);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageUnderAttackCivilianBases);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageUnderAttackMilitaryShips);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageUnderAttackExplorationShips);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageUnderAttackOtherStateBases);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageUnderAttackColonyConstructionShips);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageUnderAttackColoniesSpaceports);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageUnderAttackCivilianShips);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageShipNeedsRefuelling);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageShipMissionComplete);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageExploration);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageIntelligenceMissions);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageResearchBreakthrough);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageEmpireMetDestroyed);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageColonyGainLoss);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageDiplomacyWarTradeSanctions);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageDiplomacyTreaties);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageRequestWarning);
            this.grpOptionsPopupMessages.Controls.Add(this.chkOptionsPopupMessageShipBuilt);
            this.grpOptionsPopupMessages.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.grpOptionsPopupMessages.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.grpOptionsPopupMessages.Location = new System.Drawing.Point(317, 14);
            this.grpOptionsPopupMessages.Name = "grpOptionsPopupMessages";
            this.grpOptionsPopupMessages.Size = new System.Drawing.Size(295, 349);
            this.grpOptionsPopupMessages.TabIndex = 18;
            this.grpOptionsPopupMessages.TabStop = false;
            this.grpOptionsPopupMessages.Text = "Popup Messages";
            
            
            this.chkOptionsPopupMessageConstructionResourceShortage.AutoSize = true;
            this.chkOptionsPopupMessageConstructionResourceShortage.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageConstructionResourceShortage.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageConstructionResourceShortage.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageConstructionResourceShortage.Location = new System.Drawing.Point(7, 325);
            this.chkOptionsPopupMessageConstructionResourceShortage.Name = "chkOptionsPopupMessageConstructionResourceShortage";
            this.chkOptionsPopupMessageConstructionResourceShortage.Size = new System.Drawing.Size(211, 17);
            this.chkOptionsPopupMessageConstructionResourceShortage.TabIndex = 32;
            this.chkOptionsPopupMessageConstructionResourceShortage.Text = "Construction Resource Shortage";
            this.chkOptionsPopupMessageConstructionResourceShortage.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageUnderAttackCivilianBases.AutoSize = true;
            this.chkOptionsPopupMessageUnderAttackCivilianBases.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageUnderAttackCivilianBases.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageUnderAttackCivilianBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageUnderAttackCivilianBases.Location = new System.Drawing.Point(7, 223);
            this.chkOptionsPopupMessageUnderAttackCivilianBases.Name = "chkOptionsPopupMessageUnderAttackCivilianBases";
            this.chkOptionsPopupMessageUnderAttackCivilianBases.Size = new System.Drawing.Size(193, 17);
            this.chkOptionsPopupMessageUnderAttackCivilianBases.TabIndex = 31;
            this.chkOptionsPopupMessageUnderAttackCivilianBases.Text = "Under Attack - Civilian Bases";
            this.chkOptionsPopupMessageUnderAttackCivilianBases.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.AutoSize = true;
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.Location = new System.Drawing.Point(7, 274);
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.Name = "chkOptionsPopupMessageUnderAttackMilitaryShips";
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.Size = new System.Drawing.Size(189, 17);
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.TabIndex = 30;
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.Text = "Under Attack - Military Ships";
            this.chkOptionsPopupMessageUnderAttackMilitaryShips.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageUnderAttackExplorationShips.AutoSize = true;
            this.chkOptionsPopupMessageUnderAttackExplorationShips.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageUnderAttackExplorationShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageUnderAttackExplorationShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageUnderAttackExplorationShips.Location = new System.Drawing.Point(7, 240);
            this.chkOptionsPopupMessageUnderAttackExplorationShips.Name = "chkOptionsPopupMessageUnderAttackExplorationShips";
            this.chkOptionsPopupMessageUnderAttackExplorationShips.Size = new System.Drawing.Size(212, 17);
            this.chkOptionsPopupMessageUnderAttackExplorationShips.TabIndex = 29;
            this.chkOptionsPopupMessageUnderAttackExplorationShips.Text = "Under Attack - Exploration Ships";
            this.chkOptionsPopupMessageUnderAttackExplorationShips.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.AutoSize = true;
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.Location = new System.Drawing.Point(7, 291);
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.Name = "chkOptionsPopupMessageUnderAttackOtherStateBases";
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.Size = new System.Drawing.Size(284, 17);
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.TabIndex = 28;
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.Text = "Under Attack - Research, Monitoring, Resorts";
            this.chkOptionsPopupMessageUnderAttackOtherStateBases.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.AutoSize = true;
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.Location = new System.Drawing.Point(7, 257);
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.Name = "chkOptionsPopupMessageUnderAttackColonyConstructionShips";
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.Size = new System.Drawing.Size(276, 17);
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.TabIndex = 27;
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.Text = "Under Attack - Colony && Construction Ships";
            this.chkOptionsPopupMessageUnderAttackColonyConstructionShips.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.AutoSize = true;
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.Location = new System.Drawing.Point(7, 308);
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.Name = "chkOptionsPopupMessageUnderAttackColoniesSpaceports";
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.Size = new System.Drawing.Size(242, 17);
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.TabIndex = 26;
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.Text = "Under Attack - Colonies && Spaceports";
            this.chkOptionsPopupMessageUnderAttackColoniesSpaceports.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageUnderAttackCivilianShips.AutoSize = true;
            this.chkOptionsPopupMessageUnderAttackCivilianShips.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageUnderAttackCivilianShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageUnderAttackCivilianShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageUnderAttackCivilianShips.Location = new System.Drawing.Point(7, 206);
            this.chkOptionsPopupMessageUnderAttackCivilianShips.Name = "chkOptionsPopupMessageUnderAttackCivilianShips";
            this.chkOptionsPopupMessageUnderAttackCivilianShips.Size = new System.Drawing.Size(190, 17);
            this.chkOptionsPopupMessageUnderAttackCivilianShips.TabIndex = 25;
            this.chkOptionsPopupMessageUnderAttackCivilianShips.Text = "Under Attack - Civilian Ships";
            this.chkOptionsPopupMessageUnderAttackCivilianShips.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageShipNeedsRefuelling.AutoSize = true;
            this.chkOptionsPopupMessageShipNeedsRefuelling.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageShipNeedsRefuelling.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageShipNeedsRefuelling.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageShipNeedsRefuelling.Location = new System.Drawing.Point(7, 189);
            this.chkOptionsPopupMessageShipNeedsRefuelling.Name = "chkOptionsPopupMessageShipNeedsRefuelling";
            this.chkOptionsPopupMessageShipNeedsRefuelling.Size = new System.Drawing.Size(207, 17);
            this.chkOptionsPopupMessageShipNeedsRefuelling.TabIndex = 20;
            this.chkOptionsPopupMessageShipNeedsRefuelling.Text = "Ship Needs Refuelling or Repair";
            this.chkOptionsPopupMessageShipNeedsRefuelling.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageShipMissionComplete.AutoSize = true;
            this.chkOptionsPopupMessageShipMissionComplete.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageShipMissionComplete.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageShipMissionComplete.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageShipMissionComplete.Location = new System.Drawing.Point(7, 172);
            this.chkOptionsPopupMessageShipMissionComplete.Name = "chkOptionsPopupMessageShipMissionComplete";
            this.chkOptionsPopupMessageShipMissionComplete.Size = new System.Drawing.Size(155, 17);
            this.chkOptionsPopupMessageShipMissionComplete.TabIndex = 19;
            this.chkOptionsPopupMessageShipMissionComplete.Text = "Ship Mission Complete";
            this.chkOptionsPopupMessageShipMissionComplete.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageExploration.AutoSize = true;
            this.chkOptionsPopupMessageExploration.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageExploration.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageExploration.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageExploration.Location = new System.Drawing.Point(7, 155);
            this.chkOptionsPopupMessageExploration.Name = "chkOptionsPopupMessageExploration";
            this.chkOptionsPopupMessageExploration.Size = new System.Drawing.Size(158, 17);
            this.chkOptionsPopupMessageExploration.TabIndex = 16;
            this.chkOptionsPopupMessageExploration.Text = "Exploration discoveries";
            this.chkOptionsPopupMessageExploration.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageIntelligenceMissions.AutoSize = true;
            this.chkOptionsPopupMessageIntelligenceMissions.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageIntelligenceMissions.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageIntelligenceMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageIntelligenceMissions.Location = new System.Drawing.Point(7, 138);
            this.chkOptionsPopupMessageIntelligenceMissions.Name = "chkOptionsPopupMessageIntelligenceMissions";
            this.chkOptionsPopupMessageIntelligenceMissions.Size = new System.Drawing.Size(143, 17);
            this.chkOptionsPopupMessageIntelligenceMissions.TabIndex = 15;
            this.chkOptionsPopupMessageIntelligenceMissions.Text = "Intelligence Missions";
            this.chkOptionsPopupMessageIntelligenceMissions.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageResearchBreakthrough.AutoSize = true;
            this.chkOptionsPopupMessageResearchBreakthrough.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageResearchBreakthrough.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageResearchBreakthrough.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageResearchBreakthrough.Location = new System.Drawing.Point(7, 121);
            this.chkOptionsPopupMessageResearchBreakthrough.Name = "chkOptionsPopupMessageResearchBreakthrough";
            this.chkOptionsPopupMessageResearchBreakthrough.Size = new System.Drawing.Size(160, 17);
            this.chkOptionsPopupMessageResearchBreakthrough.TabIndex = 7;
            this.chkOptionsPopupMessageResearchBreakthrough.Text = "Research breakthrough";
            this.chkOptionsPopupMessageResearchBreakthrough.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageEmpireMetDestroyed.AutoSize = true;
            this.chkOptionsPopupMessageEmpireMetDestroyed.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageEmpireMetDestroyed.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageEmpireMetDestroyed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageEmpireMetDestroyed.Location = new System.Drawing.Point(7, 104);
            this.chkOptionsPopupMessageEmpireMetDestroyed.Name = "chkOptionsPopupMessageEmpireMetDestroyed";
            this.chkOptionsPopupMessageEmpireMetDestroyed.Size = new System.Drawing.Size(127, 17);
            this.chkOptionsPopupMessageEmpireMetDestroyed.TabIndex = 8;
            this.chkOptionsPopupMessageEmpireMetDestroyed.Text = "Empire Discovery";
            this.chkOptionsPopupMessageEmpireMetDestroyed.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageColonyGainLoss.AutoSize = true;
            this.chkOptionsPopupMessageColonyGainLoss.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageColonyGainLoss.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageColonyGainLoss.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageColonyGainLoss.Location = new System.Drawing.Point(7, 87);
            this.chkOptionsPopupMessageColonyGainLoss.Name = "chkOptionsPopupMessageColonyGainLoss";
            this.chkOptionsPopupMessageColonyGainLoss.Size = new System.Drawing.Size(141, 17);
            this.chkOptionsPopupMessageColonyGainLoss.TabIndex = 13;
            this.chkOptionsPopupMessageColonyGainLoss.Text = "Colony Gain or Loss";
            this.chkOptionsPopupMessageColonyGainLoss.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.AutoSize = true;
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.Location = new System.Drawing.Point(7, 70);
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.Name = "chkOptionsPopupMessageDiplomacyWarTradeSanctions";
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.Size = new System.Drawing.Size(157, 17);
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.TabIndex = 10;
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.Text = "War && Trade Sanctions";
            this.chkOptionsPopupMessageDiplomacyWarTradeSanctions.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageDiplomacyTreaties.AutoSize = true;
            this.chkOptionsPopupMessageDiplomacyTreaties.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageDiplomacyTreaties.Location = new System.Drawing.Point(7, 53);
            this.chkOptionsPopupMessageDiplomacyTreaties.Name = "chkOptionsPopupMessageDiplomacyTreaties";
            this.chkOptionsPopupMessageDiplomacyTreaties.Size = new System.Drawing.Size(100, 17);
            this.chkOptionsPopupMessageDiplomacyTreaties.TabIndex = 11;
            this.chkOptionsPopupMessageDiplomacyTreaties.Text = "Treaty offers";
            this.chkOptionsPopupMessageDiplomacyTreaties.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageRequestWarning.AutoSize = true;
            this.chkOptionsPopupMessageRequestWarning.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageRequestWarning.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageRequestWarning.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageRequestWarning.Location = new System.Drawing.Point(7, 36);
            this.chkOptionsPopupMessageRequestWarning.Name = "chkOptionsPopupMessageRequestWarning";
            this.chkOptionsPopupMessageRequestWarning.Size = new System.Drawing.Size(181, 17);
            this.chkOptionsPopupMessageRequestWarning.TabIndex = 14;
            this.chkOptionsPopupMessageRequestWarning.Text = "Requests, Warnings && Gifts";
            this.chkOptionsPopupMessageRequestWarning.UseVisualStyleBackColor = false;
            this.chkOptionsPopupMessageShipBuilt.AutoSize = true;
            this.chkOptionsPopupMessageShipBuilt.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsPopupMessageShipBuilt.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsPopupMessageShipBuilt.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsPopupMessageShipBuilt.Location = new System.Drawing.Point(7, 19);
            this.chkOptionsPopupMessageShipBuilt.Name = "chkOptionsPopupMessageShipBuilt";
            this.chkOptionsPopupMessageShipBuilt.Size = new System.Drawing.Size(108, 17);
            this.chkOptionsPopupMessageShipBuilt.TabIndex = 5;
            this.chkOptionsPopupMessageShipBuilt.Text = "New Ship Built";
            this.chkOptionsPopupMessageShipBuilt.UseVisualStyleBackColor = false;
            
            // resume layout
            
            this.grpOptionsAutoSave.ResumeLayout(false);
            this.grpOptionsAutoSave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.numOptionsAutoSaveMinutes).EndInit();
            this.grpOptionsDisplaySettings.ResumeLayout(false);
            this.grpOptionsDisplaySettings.PerformLayout();
            this.grpOptionsVolume.ResumeLayout(false);
            this.grpOptionsVolume.PerformLayout();
            this.grpOptionsControl.ResumeLayout(false);
            this.grpOptionsControl.PerformLayout();
            this.pnlOptionsAutomationMode.ResumeLayout(false);
            this.pnlOptionsAutomationMode.PerformLayout();
            this.grpOptionsPopupMessages.ResumeLayout(false);
            this.grpOptionsPopupMessages.PerformLayout();
            
  }

  #endregion

  internal GlassButton btnGameOptionsShowMessages;
  internal CheckBox chkOptionsLoadedGamesPaused;
  internal GroupBox grpOptionsAutoSave;
  internal Label lblOptionsMouseScrollMode;
  internal ComboBox cmbOptionsMouseScrollWheelBehaviour;
  internal GroupBox grpOptionsDisplaySettings;
  internal CheckBox chkOptionsAutoPauseInPopup;
  internal GroupBox grpOptionsVolume;
  internal GroupBox grpOptionsControl;
  internal NumericUpDown numOptionsAutoSaveMinutes;
  internal CheckBox chkOptionsAutoSave;

  internal GlassButton btnGameOptionsAdvancedDisplaySettings;

        internal Label lblOptionsMainViewZoomSpeed;

        internal ColorSlider sldOptionsMainViewZoomSpeed;

        internal Label lblOptionsMainViewStarFieldSize;

        internal ColorSlider sldOptionsMainViewStarFieldSize;

        internal Label lblOptionsMainViewGuiScale;

        internal ColorSlider sldOptionsMainViewGuiScale;

        internal Label lblOptionsMainViewScrollSpeed;

        internal ColorSlider sldOptionsMainViewScrollSpeed;

        internal GroupBox grpOptionsPopupMessages;

        internal CheckBox chkOptionsPopupMessageExploration;

        internal CheckBox chkOptionsPopupMessageIntelligenceMissions;

        internal CheckBox chkOptionsPopupMessageRequestWarning;

        internal CheckBox chkOptionsPopupMessageColonyGainLoss;

        internal CheckBox chkOptionsPopupMessageDiplomacyTreaties;

        internal CheckBox chkOptionsPopupMessageDiplomacyWarTradeSanctions;

        internal CheckBox chkOptionsPopupMessageEmpireMetDestroyed;

        internal CheckBox chkOptionsPopupMessageResearchBreakthrough;

        internal CheckBox chkOptionsPopupMessageShipBuilt;

        internal Label lblOptionsSoundEffectsVolume;

        internal Label lblOptionsMusicVolume;

        internal ColorSlider sldOptionsSoundEffectsVolume;

        internal ColorSlider sldOptionsMusicVolume;

        internal ComboBox cmbOptionsAutomationMode;

        internal Label lblOptionsAutomationMode;


        internal Panel pnlOptionsAutomationMode;
        internal Label lblOptionsControlOfferPirateMissions;

        internal ComboBox cmbOptionsControlOfferPirateMissions;


        internal CheckBox chkOptionsControlPopulationPolicy;

        internal CheckBox chkOptionsControlCharacterLocations;

        internal Label lblOptionsControlColonyFacilities;

        internal ComboBox cmbOptionsControlColonyFacilities;

        internal CheckBox chkOptionsControlResearch;

        internal GlassButton btnGameOptionsEmpireSettings;

        internal Label lblOptionsControlColonization;

        internal Label lblOptionsControlDiplomacyTreaties;

        internal ComboBox cmbOptionsControlColonization;

        internal Label lblOptionsControlAttacks;

        internal Label lblOptionsControlConstruction;

        internal ComboBox cmbOptionsControlDiplomacyTreaties;

        internal ComboBox cmbOptionsControlConstruction;

        internal ComboBox cmbOptionsControlAttacks;

        internal Label lblOptionsControlAgentMissions;

        internal Label lblOptionsControlDiplomacyGifts;

        internal GlassButton btnGameOptionsResetAutomationMessages;

        internal Label lblOptionsControlDiplomacyOffense;

        internal ComboBox cmbOptionsControlAgentMissions;

        internal ComboBox cmbOptionsControlDiplomacyOffense;

        internal ComboBox cmbOptionsControlDiplomacyGifts;

        internal CheckBox chkOptionsControlTroops;

        internal CheckBox chkOptionsControlColonyTaxRates;

        internal CheckBox chkOptionsControlFleets;

        internal CheckBox chkOptionsControlDesigns;

        internal CheckBox chkOptionsPopupMessageConstructionResourceShortage;


        internal CheckBox chkOptionsPopupMessageUnderAttackCivilianBases;

        internal CheckBox chkOptionsPopupMessageUnderAttackMilitaryShips;

        internal CheckBox chkOptionsPopupMessageUnderAttackExplorationShips;

        internal CheckBox chkOptionsPopupMessageUnderAttackOtherStateBases;

        internal CheckBox chkOptionsPopupMessageUnderAttackColonyConstructionShips;

        internal CheckBox chkOptionsPopupMessageUnderAttackColoniesSpaceports;

        internal CheckBox chkOptionsPopupMessageUnderAttackCivilianShips;

        internal CheckBox chkOptionsPopupMessageShipNeedsRefuelling;

        internal CheckBox chkOptionsPopupMessageShipMissionComplete;

}