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
    internal void InitializeComponent()
    {
        TableLayoutPanel tblDisplaySettings;
        FlowLayoutPanel flwAdvancedSettings;
        TableLayoutPanel tlbSoundVolume;
        TableLayoutPanel tlbBody;
        TableLayoutPanel tlbMiscMiddle;
        FlowLayoutPanel flwMidLeft;
        FlowLayoutPanel flwMidRightTopDown;
        TableLayoutPanel tlbMouseScrollWheelBehavior;
        FlowLayoutPanel flwAutomationTopLeft;
        btnGameOptionsAdvancedDisplaySettings = new GlassButton();
        lblOptionsMainViewScrollSpeed = new Label();
        sldOptionsMainViewStarFieldSize = new ColorSlider();
        sldOptionsMainViewZoomSpeed = new ColorSlider();
        sldOptionsMainViewGuiScale = new ColorSlider();
        lblOptionsMainViewZoomSpeed = new Label();
        lblOptionsMainViewStarFieldSize = new Label();
        lblOptionsMainViewGuiScale = new Label();
        sldOptionsMainViewScrollSpeed = new ColorSlider();
        lblOptionsSoundEffectsVolume = new Label();
        sldOptionsMusicVolume = new ColorSlider();
        lblOptionsMusicVolume = new Label();
        sldOptionsSoundEffectsVolume = new ColorSlider();
        tableLayoutPanel1 = new TableLayoutPanel();
        btnGameOptionsShowMessages = new GlassButton();
        chkOptionsAutoPauseInPopup = new System.Windows.Forms.CheckBox();
        grpOptionsAutoSave = new GroupBox();
        flwAutoSave = new FlowLayoutPanel();
        tlbAutoSave = new TableLayoutPanel();
        lblAutoSaveMinutes = new Label();
        numOptionsAutoSaveMinutes = new NumericUpDown();
        lblAutoSaveEvery = new Label();
        chkOptionsAutoSave = new System.Windows.Forms.CheckBox();
        flwMidRightToRight = new FlowLayoutPanel();
        chkOptionsLoadedGamesPaused = new System.Windows.Forms.CheckBox();
        lblOptionsMouseScrollMode = new Label();
        cmbOptionsMouseScrollWheelBehaviour = new ComboBox();
        grpOptionsDisplaySettings = new GroupBox();
        grpOptionsControl = new GroupBox();
        tlbAutomation = new TableLayoutPanel();
        tlbAutomationRight = new TableLayoutPanel();
        cmbOptionsControlOfferPirateMissions = new ComboBox();
        lblOptionsControlOfferPirateMissions = new Label();
        cmbOptionsControlColonization = new ComboBox();
        lblOptionsControlColonization = new Label();
        cmbOptionsControlColonyFacilities = new ComboBox();
        lblOptionsControlColonyFacilities = new Label();
        lblOptionsControlConstruction = new Label();
        lblOptionsControlAgentMissions = new Label();
        cmbOptionsControlDiplomacyOffense = new ComboBox();
        lblOptionsControlDiplomacyOffense = new Label();
        lblOptionsControlDiplomacyTreaties = new Label();
        lblOptionsControlAttacks = new Label();
        cmbOptionsControlDiplomacyTreaties = new ComboBox();
        lblOptionsControlDiplomacyGifts = new Label();
        cmbOptionsControlAttacks = new ComboBox();
        cmbOptionsControlConstruction = new ComboBox();
        cmbOptionsControlDiplomacyGifts = new ComboBox();
        cmbOptionsControlAgentMissions = new ComboBox();
        flwAutomationLeft = new FlowLayoutPanel();
        pnlOptionsAutomationMode = new TableLayoutPanel();
        lblOptionsAutomationMode = new Label();
        cmbOptionsAutomationMode = new ComboBox();
        btnGameOptionsResetAutomationMessages = new GlassButton();
        chkOptionsControlColonyTaxRates = new System.Windows.Forms.CheckBox();
        chkOptionsControlDesigns = new System.Windows.Forms.CheckBox();
        chkOptionsControlFleets = new System.Windows.Forms.CheckBox();
        chkOptionsControlTroops = new System.Windows.Forms.CheckBox();
        chkOptionsControlResearch = new System.Windows.Forms.CheckBox();
        chkOptionsControlPopulationPolicy = new System.Windows.Forms.CheckBox();
        chkOptionsControlCharacterLocations = new System.Windows.Forms.CheckBox();
        btnGameOptionsEmpireSettings = new GlassButton();
        grpOptionsVolume = new GroupBox();
        grpOptionsPopupMessages = new GroupBox();
        chkOptionsPopupMessageConstructionResourceShortage = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageUnderAttackCivilianBases = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageUnderAttackMilitaryShips = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageUnderAttackExplorationShips = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageUnderAttackOtherStateBases = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageUnderAttackColonyConstructionShips = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageUnderAttackColoniesSpaceports = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageUnderAttackCivilianShips = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageShipNeedsRefuelling = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageShipMissionComplete = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageExploration = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageIntelligenceMissions = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageResearchBreakthrough = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageEmpireMetDestroyed = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageColonyGainLoss = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageDiplomacyWarTradeSanctions = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageDiplomacyTreaties = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageRequestWarning = new System.Windows.Forms.CheckBox();
        chkOptionsPopupMessageShipBuilt = new System.Windows.Forms.CheckBox();
        tblDisplaySettings = new TableLayoutPanel();
        flwAdvancedSettings = new FlowLayoutPanel();
        tlbSoundVolume = new TableLayoutPanel();
        tlbBody = new TableLayoutPanel();
        tlbMiscMiddle = new TableLayoutPanel();
        flwMidLeft = new FlowLayoutPanel();
        flwMidRightTopDown = new FlowLayoutPanel();
        tlbMouseScrollWheelBehavior = new TableLayoutPanel();
        flwAutomationTopLeft = new FlowLayoutPanel();
        tblDisplaySettings.SuspendLayout();
        flwAdvancedSettings.SuspendLayout();
        tlbSoundVolume.SuspendLayout();
        tlbBody.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        tlbMiscMiddle.SuspendLayout();
        flwMidLeft.SuspendLayout();
        grpOptionsAutoSave.SuspendLayout();
        flwAutoSave.SuspendLayout();
        tlbAutoSave.SuspendLayout();
        ((ISupportInitialize)numOptionsAutoSaveMinutes).BeginInit();
        flwMidRightToRight.SuspendLayout();
        flwMidRightTopDown.SuspendLayout();
        tlbMouseScrollWheelBehavior.SuspendLayout();
        grpOptionsDisplaySettings.SuspendLayout();
        grpOptionsControl.SuspendLayout();
        tlbAutomation.SuspendLayout();
        tlbAutomationRight.SuspendLayout();
        flwAutomationLeft.SuspendLayout();
        flwAutomationTopLeft.SuspendLayout();
        pnlOptionsAutomationMode.SuspendLayout();
        grpOptionsVolume.SuspendLayout();
        grpOptionsPopupMessages.SuspendLayout();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.Margin = new Padding(0);
        pnlHeader.TitleText = "Options";
        // 
        // tblDisplaySettings
        // 
        tblDisplaySettings.AutoSize = true;
        tblDisplaySettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tblDisplaySettings.ColumnCount = 2;
        tblDisplaySettings.ColumnStyles.Add(new ColumnStyle());
        tblDisplaySettings.ColumnStyles.Add(new ColumnStyle());
        tblDisplaySettings.Controls.Add(flwAdvancedSettings, 0, 4);
        tblDisplaySettings.Controls.Add(lblOptionsMainViewScrollSpeed, 0, 0);
        tblDisplaySettings.Controls.Add(sldOptionsMainViewStarFieldSize, 1, 2);
        tblDisplaySettings.Controls.Add(sldOptionsMainViewZoomSpeed, 1, 1);
        tblDisplaySettings.Controls.Add(sldOptionsMainViewGuiScale, 1, 3);
        tblDisplaySettings.Controls.Add(lblOptionsMainViewZoomSpeed, 0, 1);
        tblDisplaySettings.Controls.Add(lblOptionsMainViewStarFieldSize, 0, 2);
        tblDisplaySettings.Controls.Add(lblOptionsMainViewGuiScale, 0, 3);
        tblDisplaySettings.Controls.Add(sldOptionsMainViewScrollSpeed, 1, 0);
        tblDisplaySettings.Dock = DockStyle.Fill;
        tblDisplaySettings.Location = new System.Drawing.Point(0, 14);
        tblDisplaySettings.Margin = new Padding(0);
        tblDisplaySettings.Name = "tblDisplaySettings";
        tblDisplaySettings.RowCount = 5;
        tblDisplaySettings.RowStyles.Add(new RowStyle());
        tblDisplaySettings.RowStyles.Add(new RowStyle());
        tblDisplaySettings.RowStyles.Add(new RowStyle());
        tblDisplaySettings.RowStyles.Add(new RowStyle());
        tblDisplaySettings.RowStyles.Add(new RowStyle());
        tblDisplaySettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tblDisplaySettings.Size = new System.Drawing.Size(588, 108);
        tblDisplaySettings.TabIndex = 0;
        // 
        // flwAdvancedSettings
        // 
        flwAdvancedSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        flwAdvancedSettings.AutoSize = true;
        flwAdvancedSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tblDisplaySettings.SetColumnSpan(flwAdvancedSettings, 2);
        flwAdvancedSettings.Controls.Add(btnGameOptionsAdvancedDisplaySettings);
        flwAdvancedSettings.FlowDirection = FlowDirection.RightToLeft;
        flwAdvancedSettings.Location = new System.Drawing.Point(0, 80);
        flwAdvancedSettings.Margin = new Padding(0);
        flwAdvancedSettings.Name = "flwAdvancedSettings";
        flwAdvancedSettings.Size = new System.Drawing.Size(588, 28);
        flwAdvancedSettings.TabIndex = 0;
        // 
        // btnGameOptionsAdvancedDisplaySettings
        // 
        btnGameOptionsAdvancedDisplaySettings.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        btnGameOptionsAdvancedDisplaySettings.ClipBackground = false;
        btnGameOptionsAdvancedDisplaySettings.DelayFrameRefresh = false;
        btnGameOptionsAdvancedDisplaySettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        btnGameOptionsAdvancedDisplaySettings.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        btnGameOptionsAdvancedDisplaySettings.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        btnGameOptionsAdvancedDisplaySettings.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        btnGameOptionsAdvancedDisplaySettings.IntensifyColors = false;
        btnGameOptionsAdvancedDisplaySettings.Location = new System.Drawing.Point(435, 3);
        btnGameOptionsAdvancedDisplaySettings.Name = "btnGameOptionsAdvancedDisplaySettings";
        btnGameOptionsAdvancedDisplaySettings.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        btnGameOptionsAdvancedDisplaySettings.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        btnGameOptionsAdvancedDisplaySettings.Size = new System.Drawing.Size(150, 22);
        btnGameOptionsAdvancedDisplaySettings.TabIndex = 4;
        btnGameOptionsAdvancedDisplaySettings.Text = "Advanced Settings...";
        btnGameOptionsAdvancedDisplaySettings.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        btnGameOptionsAdvancedDisplaySettings.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        btnGameOptionsAdvancedDisplaySettings.ToggledOn = false;
        // 
        // lblOptionsMainViewScrollSpeed
        // 
        lblOptionsMainViewScrollSpeed.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblOptionsMainViewScrollSpeed.AutoSize = true;
        lblOptionsMainViewScrollSpeed.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMainViewScrollSpeed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMainViewScrollSpeed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMainViewScrollSpeed.Location = new System.Drawing.Point(0, 0);
        lblOptionsMainViewScrollSpeed.Margin = new Padding(0);
        lblOptionsMainViewScrollSpeed.Name = "lblOptionsMainViewScrollSpeed";
        lblOptionsMainViewScrollSpeed.Size = new System.Drawing.Size(80, 20);
        lblOptionsMainViewScrollSpeed.TabIndex = 1;
        lblOptionsMainViewScrollSpeed.Text = "Scroll Speed";
        lblOptionsMainViewScrollSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // sldOptionsMainViewStarFieldSize
        // 
        sldOptionsMainViewStarFieldSize.BackColor = System.Drawing.Color.Transparent;
        sldOptionsMainViewStarFieldSize.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        sldOptionsMainViewStarFieldSize.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewStarFieldSize.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        sldOptionsMainViewStarFieldSize.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        sldOptionsMainViewStarFieldSize.Dock = DockStyle.Fill;
        sldOptionsMainViewStarFieldSize.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewStarFieldSize.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewStarFieldSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        sldOptionsMainViewStarFieldSize.ForeColor = System.Drawing.Color.White;
        sldOptionsMainViewStarFieldSize.LargeChange = 5U;
        sldOptionsMainViewStarFieldSize.Location = new System.Drawing.Point(80, 40);
        sldOptionsMainViewStarFieldSize.Margin = new Padding(0);
        sldOptionsMainViewStarFieldSize.Maximum = 2000;
        sldOptionsMainViewStarFieldSize.Minimum = 50;
        sldOptionsMainViewStarFieldSize.Name = "sldOptionsMainViewStarFieldSize";
        sldOptionsMainViewStarFieldSize.Size = new System.Drawing.Size(508, 20);
        sldOptionsMainViewStarFieldSize.SmallChange = 1U;
        sldOptionsMainViewStarFieldSize.TabIndex = 2;
        sldOptionsMainViewStarFieldSize.Text = "colorSlider1";
        sldOptionsMainViewStarFieldSize.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewStarFieldSize.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewStarFieldSize.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewStarFieldSize.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMainViewStarFieldSize.ThumbSize = 20;
        sldOptionsMainViewStarFieldSize.Value = 1000;
        // 
        // sldOptionsMainViewZoomSpeed
        // 
        sldOptionsMainViewZoomSpeed.BackColor = System.Drawing.Color.Transparent;
        sldOptionsMainViewZoomSpeed.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        sldOptionsMainViewZoomSpeed.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewZoomSpeed.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        sldOptionsMainViewZoomSpeed.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        sldOptionsMainViewZoomSpeed.Dock = DockStyle.Fill;
        sldOptionsMainViewZoomSpeed.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewZoomSpeed.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewZoomSpeed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        sldOptionsMainViewZoomSpeed.ForeColor = System.Drawing.Color.White;
        sldOptionsMainViewZoomSpeed.LargeChange = 5U;
        sldOptionsMainViewZoomSpeed.Location = new System.Drawing.Point(80, 20);
        sldOptionsMainViewZoomSpeed.Margin = new Padding(0);
        sldOptionsMainViewZoomSpeed.Minimum = 10;
        sldOptionsMainViewZoomSpeed.Name = "sldOptionsMainViewZoomSpeed";
        sldOptionsMainViewZoomSpeed.Size = new System.Drawing.Size(508, 20);
        sldOptionsMainViewZoomSpeed.SmallChange = 1U;
        sldOptionsMainViewZoomSpeed.TabIndex = 1;
        sldOptionsMainViewZoomSpeed.Text = "colorSlider1";
        sldOptionsMainViewZoomSpeed.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewZoomSpeed.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewZoomSpeed.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewZoomSpeed.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMainViewZoomSpeed.ThumbSize = 20;
        sldOptionsMainViewZoomSpeed.Value = 30;
        // 
        // sldOptionsMainViewGuiScale
        // 
        sldOptionsMainViewGuiScale.BackColor = System.Drawing.Color.Transparent;
        sldOptionsMainViewGuiScale.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        sldOptionsMainViewGuiScale.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewGuiScale.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        sldOptionsMainViewGuiScale.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        sldOptionsMainViewGuiScale.Dock = DockStyle.Fill;
        sldOptionsMainViewGuiScale.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewGuiScale.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewGuiScale.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        sldOptionsMainViewGuiScale.ForeColor = System.Drawing.Color.White;
        sldOptionsMainViewGuiScale.LargeChange = 5U;
        sldOptionsMainViewGuiScale.Location = new System.Drawing.Point(80, 60);
        sldOptionsMainViewGuiScale.Margin = new Padding(0);
        sldOptionsMainViewGuiScale.Maximum = 4000;
        sldOptionsMainViewGuiScale.Minimum = 500;
        sldOptionsMainViewGuiScale.Name = "sldOptionsMainViewGuiScale";
        sldOptionsMainViewGuiScale.Size = new System.Drawing.Size(508, 20);
        sldOptionsMainViewGuiScale.SmallChange = 1U;
        sldOptionsMainViewGuiScale.TabIndex = 3;
        sldOptionsMainViewGuiScale.Text = "colorSlider1";
        sldOptionsMainViewGuiScale.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewGuiScale.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewGuiScale.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewGuiScale.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMainViewGuiScale.ThumbSize = 20;
        sldOptionsMainViewGuiScale.Value = 1000;
        // 
        // lblOptionsMainViewZoomSpeed
        // 
        lblOptionsMainViewZoomSpeed.AutoSize = true;
        lblOptionsMainViewZoomSpeed.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMainViewZoomSpeed.Dock = DockStyle.Fill;
        lblOptionsMainViewZoomSpeed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMainViewZoomSpeed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMainViewZoomSpeed.Location = new System.Drawing.Point(0, 20);
        lblOptionsMainViewZoomSpeed.Margin = new Padding(0);
        lblOptionsMainViewZoomSpeed.Name = "lblOptionsMainViewZoomSpeed";
        lblOptionsMainViewZoomSpeed.Size = new System.Drawing.Size(80, 20);
        lblOptionsMainViewZoomSpeed.TabIndex = 5;
        lblOptionsMainViewZoomSpeed.Text = "Zoom Speed";
        lblOptionsMainViewZoomSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblOptionsMainViewStarFieldSize
        // 
        lblOptionsMainViewStarFieldSize.AutoSize = true;
        lblOptionsMainViewStarFieldSize.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMainViewStarFieldSize.Dock = DockStyle.Fill;
        lblOptionsMainViewStarFieldSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMainViewStarFieldSize.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMainViewStarFieldSize.Location = new System.Drawing.Point(0, 40);
        lblOptionsMainViewStarFieldSize.Margin = new Padding(0);
        lblOptionsMainViewStarFieldSize.Name = "lblOptionsMainViewStarFieldSize";
        lblOptionsMainViewStarFieldSize.Size = new System.Drawing.Size(80, 20);
        lblOptionsMainViewStarFieldSize.TabIndex = 6;
        lblOptionsMainViewStarFieldSize.Text = "Star Density";
        lblOptionsMainViewStarFieldSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // lblOptionsMainViewGuiScale
        // 
        lblOptionsMainViewGuiScale.AutoSize = true;
        lblOptionsMainViewGuiScale.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMainViewGuiScale.Dock = DockStyle.Fill;
        lblOptionsMainViewGuiScale.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMainViewGuiScale.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMainViewGuiScale.Location = new System.Drawing.Point(0, 60);
        lblOptionsMainViewGuiScale.Margin = new Padding(0);
        lblOptionsMainViewGuiScale.Name = "lblOptionsMainViewGuiScale";
        lblOptionsMainViewGuiScale.Size = new System.Drawing.Size(80, 20);
        lblOptionsMainViewGuiScale.TabIndex = 7;
        lblOptionsMainViewGuiScale.Text = "GUI Scale";
        lblOptionsMainViewGuiScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // sldOptionsMainViewScrollSpeed
        // 
        sldOptionsMainViewScrollSpeed.BackColor = System.Drawing.Color.Transparent;
        sldOptionsMainViewScrollSpeed.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        sldOptionsMainViewScrollSpeed.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewScrollSpeed.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        sldOptionsMainViewScrollSpeed.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        sldOptionsMainViewScrollSpeed.Dock = DockStyle.Fill;
        sldOptionsMainViewScrollSpeed.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewScrollSpeed.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewScrollSpeed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        sldOptionsMainViewScrollSpeed.ForeColor = System.Drawing.Color.White;
        sldOptionsMainViewScrollSpeed.LargeChange = 5U;
        sldOptionsMainViewScrollSpeed.Location = new System.Drawing.Point(80, 0);
        sldOptionsMainViewScrollSpeed.Margin = new Padding(0);
        sldOptionsMainViewScrollSpeed.Minimum = 1;
        sldOptionsMainViewScrollSpeed.Name = "sldOptionsMainViewScrollSpeed";
        sldOptionsMainViewScrollSpeed.Size = new System.Drawing.Size(508, 20);
        sldOptionsMainViewScrollSpeed.SmallChange = 1U;
        sldOptionsMainViewScrollSpeed.TabIndex = 0;
        sldOptionsMainViewScrollSpeed.Text = "colorSlider1";
        sldOptionsMainViewScrollSpeed.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewScrollSpeed.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewScrollSpeed.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewScrollSpeed.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMainViewScrollSpeed.ThumbSize = 20;
        // 
        // tlbSoundVolume
        // 
        tlbSoundVolume.AutoSize = true;
        tlbSoundVolume.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tlbSoundVolume.ColumnCount = 2;
        tlbSoundVolume.ColumnStyles.Add(new ColumnStyle());
        tlbSoundVolume.ColumnStyles.Add(new ColumnStyle());
        tlbSoundVolume.Controls.Add(lblOptionsSoundEffectsVolume, 0, 1);
        tlbSoundVolume.Controls.Add(sldOptionsMusicVolume, 1, 0);
        tlbSoundVolume.Controls.Add(lblOptionsMusicVolume, 0, 0);
        tlbSoundVolume.Controls.Add(sldOptionsSoundEffectsVolume, 1, 1);
        tlbSoundVolume.Dock = DockStyle.Fill;
        tlbSoundVolume.Location = new System.Drawing.Point(0, 14);
        tlbSoundVolume.Margin = new Padding(0);
        tlbSoundVolume.Name = "tlbSoundVolume";
        tlbSoundVolume.RowCount = 3;
        tlbSoundVolume.RowStyles.Add(new RowStyle());
        tlbSoundVolume.RowStyles.Add(new RowStyle());
        tlbSoundVolume.RowStyles.Add(new RowStyle());
        tlbSoundVolume.Size = new System.Drawing.Size(588, 40);
        tlbSoundVolume.TabIndex = 0;
        // 
        // lblOptionsSoundEffectsVolume
        // 
        lblOptionsSoundEffectsVolume.AutoSize = true;
        lblOptionsSoundEffectsVolume.BackColor = System.Drawing.Color.Transparent;
        lblOptionsSoundEffectsVolume.Dock = DockStyle.Fill;
        lblOptionsSoundEffectsVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsSoundEffectsVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsSoundEffectsVolume.Location = new System.Drawing.Point(0, 20);
        lblOptionsSoundEffectsVolume.Margin = new Padding(0);
        lblOptionsSoundEffectsVolume.Name = "lblOptionsSoundEffectsVolume";
        lblOptionsSoundEffectsVolume.Size = new System.Drawing.Size(45, 20);
        lblOptionsSoundEffectsVolume.TabIndex = 0;
        lblOptionsSoundEffectsVolume.Text = "Effects";
        lblOptionsSoundEffectsVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // sldOptionsMusicVolume
        // 
        sldOptionsMusicVolume.BackColor = System.Drawing.Color.Transparent;
        sldOptionsMusicVolume.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        sldOptionsMusicVolume.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMusicVolume.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        sldOptionsMusicVolume.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        sldOptionsMusicVolume.Dock = DockStyle.Fill;
        sldOptionsMusicVolume.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMusicVolume.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMusicVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        sldOptionsMusicVolume.ForeColor = System.Drawing.Color.White;
        sldOptionsMusicVolume.LargeChange = 5U;
        sldOptionsMusicVolume.Location = new System.Drawing.Point(45, 0);
        sldOptionsMusicVolume.Margin = new Padding(0);
        sldOptionsMusicVolume.Name = "sldOptionsMusicVolume";
        sldOptionsMusicVolume.Size = new System.Drawing.Size(543, 20);
        sldOptionsMusicVolume.SmallChange = 1U;
        sldOptionsMusicVolume.TabIndex = 5;
        sldOptionsMusicVolume.Text = "colorSlider1";
        sldOptionsMusicVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMusicVolume.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMusicVolume.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMusicVolume.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMusicVolume.ThumbSize = 20;
        // 
        // lblOptionsMusicVolume
        // 
        lblOptionsMusicVolume.AutoSize = true;
        lblOptionsMusicVolume.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMusicVolume.Dock = DockStyle.Fill;
        lblOptionsMusicVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMusicVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMusicVolume.Location = new System.Drawing.Point(0, 0);
        lblOptionsMusicVolume.Margin = new Padding(0);
        lblOptionsMusicVolume.Name = "lblOptionsMusicVolume";
        lblOptionsMusicVolume.Size = new System.Drawing.Size(45, 20);
        lblOptionsMusicVolume.TabIndex = 2;
        lblOptionsMusicVolume.Text = "Music";
        lblOptionsMusicVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // sldOptionsSoundEffectsVolume
        // 
        sldOptionsSoundEffectsVolume.BackColor = System.Drawing.Color.Transparent;
        sldOptionsSoundEffectsVolume.BarInnerColor = System.Drawing.Color.FromArgb(64, 64, 72);
        sldOptionsSoundEffectsVolume.BarOuterColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsSoundEffectsVolume.BarPenColor = System.Drawing.Color.FromArgb(16, 16, 24);
        sldOptionsSoundEffectsVolume.BorderRoundRectSize = new System.Drawing.Size(2, 2);
        sldOptionsSoundEffectsVolume.Dock = DockStyle.Fill;
        sldOptionsSoundEffectsVolume.ElapsedInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsSoundEffectsVolume.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsSoundEffectsVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        sldOptionsSoundEffectsVolume.ForeColor = System.Drawing.Color.White;
        sldOptionsSoundEffectsVolume.LargeChange = 5U;
        sldOptionsSoundEffectsVolume.Location = new System.Drawing.Point(45, 20);
        sldOptionsSoundEffectsVolume.Margin = new Padding(0);
        sldOptionsSoundEffectsVolume.Name = "sldOptionsSoundEffectsVolume";
        sldOptionsSoundEffectsVolume.Size = new System.Drawing.Size(543, 20);
        sldOptionsSoundEffectsVolume.SmallChange = 1U;
        sldOptionsSoundEffectsVolume.TabIndex = 6;
        sldOptionsSoundEffectsVolume.Text = "colorSlider1";
        sldOptionsSoundEffectsVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsSoundEffectsVolume.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsSoundEffectsVolume.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsSoundEffectsVolume.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsSoundEffectsVolume.ThumbSize = 20;
        // 
        // tlbBody
        // 
        tlbBody.AutoSize = true;
        tlbBody.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tlbBody.ColumnCount = 1;
        tlbBody.ColumnStyles.Add(new ColumnStyle());
        tlbBody.Controls.Add(tableLayoutPanel1, 0, 5);
        tlbBody.Controls.Add(tlbMiscMiddle, 0, 3);
        tlbBody.Controls.Add(grpOptionsDisplaySettings, 0, 1);
        tlbBody.Controls.Add(grpOptionsControl, 0, 4);
        tlbBody.Controls.Add(grpOptionsVolume, 0, 2);
        tlbBody.Dock = DockStyle.Fill;
        tlbBody.Location = new System.Drawing.Point(0, 0);
        tlbBody.Margin = new Padding(0);
        tlbBody.Name = "tlbBody";
        tlbBody.RowCount = 6;
        tlbBody.RowStyles.Add(new RowStyle());
        tlbBody.RowStyles.Add(new RowStyle());
        tlbBody.RowStyles.Add(new RowStyle());
        tlbBody.RowStyles.Add(new RowStyle());
        tlbBody.RowStyles.Add(new RowStyle());
        tlbBody.RowStyles.Add(new RowStyle());
        tlbBody.Size = new System.Drawing.Size(588, 480);
        tlbBody.TabIndex = 0;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.AutoSize = true;
        tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(btnGameOptionsShowMessages, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new System.Drawing.Point(0, 452);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.Size = new System.Drawing.Size(588, 28);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // btnGameOptionsShowMessages
        // 
        btnGameOptionsShowMessages.AutoSize = true;
        btnGameOptionsShowMessages.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnGameOptionsShowMessages.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        btnGameOptionsShowMessages.ClipBackground = false;
        btnGameOptionsShowMessages.DelayFrameRefresh = false;
        btnGameOptionsShowMessages.Dock = DockStyle.Fill;
        btnGameOptionsShowMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        btnGameOptionsShowMessages.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        btnGameOptionsShowMessages.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        btnGameOptionsShowMessages.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        btnGameOptionsShowMessages.IntensifyColors = false;
        btnGameOptionsShowMessages.Location = new System.Drawing.Point(227, 3);
        btnGameOptionsShowMessages.Margin = new Padding(0, 3, 0, 3);
        btnGameOptionsShowMessages.Name = "btnGameOptionsShowMessages";
        btnGameOptionsShowMessages.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        btnGameOptionsShowMessages.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        btnGameOptionsShowMessages.Size = new System.Drawing.Size(134, 22);
        btnGameOptionsShowMessages.TabIndex = 31;
        btnGameOptionsShowMessages.Text = "Show Message Options";
        btnGameOptionsShowMessages.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        btnGameOptionsShowMessages.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        btnGameOptionsShowMessages.ToggledOn = false;
        // 
        // tlbMiscMiddle
        // 
        tlbMiscMiddle.AutoSize = true;
        tlbMiscMiddle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tlbMiscMiddle.ColumnCount = 2;
        tlbMiscMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlbMiscMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlbMiscMiddle.Controls.Add(flwMidLeft, 0, 0);
        tlbMiscMiddle.Controls.Add(flwMidRightToRight, 1, 0);
        tlbMiscMiddle.Dock = DockStyle.Fill;
        tlbMiscMiddle.Location = new System.Drawing.Point(0, 180);
        tlbMiscMiddle.Margin = new Padding(0);
        tlbMiscMiddle.Name = "tlbMiscMiddle";
        tlbMiscMiddle.RowCount = 2;
        tlbMiscMiddle.RowStyles.Add(new RowStyle());
        tlbMiscMiddle.RowStyles.Add(new RowStyle());
        tlbMiscMiddle.Size = new System.Drawing.Size(588, 58);
        tlbMiscMiddle.TabIndex = 1;
        // 
        // flwMidLeft
        // 
        flwMidLeft.AutoSize = true;
        flwMidLeft.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flwMidLeft.Controls.Add(chkOptionsAutoPauseInPopup);
        flwMidLeft.Controls.Add(grpOptionsAutoSave);
        flwMidLeft.FlowDirection = FlowDirection.TopDown;
        flwMidLeft.Location = new System.Drawing.Point(0, 0);
        flwMidLeft.Margin = new Padding(0);
        flwMidLeft.Name = "flwMidLeft";
        flwMidLeft.Size = new System.Drawing.Size(192, 58);
        flwMidLeft.TabIndex = 0;
        flwMidLeft.WrapContents = false;
        // 
        // chkOptionsAutoPauseInPopup
        // 
        chkOptionsAutoPauseInPopup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        chkOptionsAutoPauseInPopup.AutoSize = true;
        chkOptionsAutoPauseInPopup.BackColor = System.Drawing.Color.Transparent;
        chkOptionsAutoPauseInPopup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsAutoPauseInPopup.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsAutoPauseInPopup.Location = new System.Drawing.Point(0, 1);
        chkOptionsAutoPauseInPopup.Margin = new Padding(0, 1, 0, 1);
        chkOptionsAutoPauseInPopup.Name = "chkOptionsAutoPauseInPopup";
        chkOptionsAutoPauseInPopup.Size = new System.Drawing.Size(192, 17);
        chkOptionsAutoPauseInPopup.TabIndex = 7;
        chkOptionsAutoPauseInPopup.Text = "Auto Pause in Game Screens";
        chkOptionsAutoPauseInPopup.UseVisualStyleBackColor = false;
        chkOptionsAutoPauseInPopup.Visible = false;
        // 
        // grpOptionsAutoSave
        // 
        grpOptionsAutoSave.AutoSize = true;
        grpOptionsAutoSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        grpOptionsAutoSave.BackColor = System.Drawing.Color.Transparent;
        grpOptionsAutoSave.Controls.Add(flwAutoSave);
        grpOptionsAutoSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsAutoSave.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsAutoSave.Location = new System.Drawing.Point(0, 19);
        grpOptionsAutoSave.Margin = new Padding(0);
        grpOptionsAutoSave.Name = "grpOptionsAutoSave";
        grpOptionsAutoSave.Padding = new Padding(0, 0, 0, 2);
        grpOptionsAutoSave.Size = new System.Drawing.Size(160, 39);
        grpOptionsAutoSave.TabIndex = 1;
        grpOptionsAutoSave.TabStop = false;
        grpOptionsAutoSave.Text = "Auto Save";
        // 
        // flwAutoSave
        // 
        flwAutoSave.AutoSize = true;
        flwAutoSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flwAutoSave.Controls.Add(tlbAutoSave);
        flwAutoSave.Dock = DockStyle.Fill;
        flwAutoSave.Location = new System.Drawing.Point(0, 14);
        flwAutoSave.Margin = new Padding(0);
        flwAutoSave.Name = "flwAutoSave";
        flwAutoSave.Size = new System.Drawing.Size(160, 23);
        flwAutoSave.TabIndex = 0;
        // 
        // tlbAutoSave
        // 
        tlbAutoSave.AutoSize = true;
        tlbAutoSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tlbAutoSave.ColumnCount = 5;
        tlbAutoSave.ColumnStyles.Add(new ColumnStyle());
        tlbAutoSave.ColumnStyles.Add(new ColumnStyle());
        tlbAutoSave.ColumnStyles.Add(new ColumnStyle());
        tlbAutoSave.ColumnStyles.Add(new ColumnStyle());
        tlbAutoSave.ColumnStyles.Add(new ColumnStyle());
        tlbAutoSave.Controls.Add(lblAutoSaveMinutes, 3, 1);
        tlbAutoSave.Controls.Add(numOptionsAutoSaveMinutes, 2, 1);
        tlbAutoSave.Controls.Add(lblAutoSaveEvery, 1, 1);
        tlbAutoSave.Controls.Add(chkOptionsAutoSave, 0, 1);
        tlbAutoSave.Location = new System.Drawing.Point(0, 0);
        tlbAutoSave.Margin = new Padding(0);
        tlbAutoSave.Name = "tlbAutoSave";
        tlbAutoSave.RowCount = 2;
        tlbAutoSave.RowStyles.Add(new RowStyle());
        tlbAutoSave.RowStyles.Add(new RowStyle());
        tlbAutoSave.Size = new System.Drawing.Size(160, 23);
        tlbAutoSave.TabIndex = 0;
        // 
        // lblAutoSaveMinutes
        // 
        lblAutoSaveMinutes.AutoSize = true;
        lblAutoSaveMinutes.Dock = DockStyle.Fill;
        lblAutoSaveMinutes.Location = new System.Drawing.Point(101, 1);
        lblAutoSaveMinutes.Margin = new Padding(0, 1, 0, 1);
        lblAutoSaveMinutes.Name = "lblAutoSaveMinutes";
        lblAutoSaveMinutes.Size = new System.Drawing.Size(59, 21);
        lblAutoSaveMinutes.TabIndex = 0;
        lblAutoSaveMinutes.Text = "minutes";
        lblAutoSaveMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // numOptionsAutoSaveMinutes
        // 
        numOptionsAutoSaveMinutes.AutoSize = true;
        numOptionsAutoSaveMinutes.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        numOptionsAutoSaveMinutes.BorderStyle = BorderStyle.FixedSingle;
        numOptionsAutoSaveMinutes.Dock = DockStyle.Fill;
        numOptionsAutoSaveMinutes.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        numOptionsAutoSaveMinutes.Location = new System.Drawing.Point(62, 1);
        numOptionsAutoSaveMinutes.Margin = new Padding(0, 1, 0, 1);
        numOptionsAutoSaveMinutes.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
        numOptionsAutoSaveMinutes.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        numOptionsAutoSaveMinutes.Name = "numOptionsAutoSaveMinutes";
        numOptionsAutoSaveMinutes.Size = new System.Drawing.Size(39, 21);
        numOptionsAutoSaveMinutes.TabIndex = 9;
        numOptionsAutoSaveMinutes.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // lblAutoSaveEvery
        // 
        lblAutoSaveEvery.AutoSize = true;
        lblAutoSaveEvery.Dock = DockStyle.Fill;
        lblAutoSaveEvery.Location = new System.Drawing.Point(17, 1);
        lblAutoSaveEvery.Margin = new Padding(0, 1, 0, 1);
        lblAutoSaveEvery.Name = "lblAutoSaveEvery";
        lblAutoSaveEvery.Size = new System.Drawing.Size(45, 21);
        lblAutoSaveEvery.TabIndex = 2;
        lblAutoSaveEvery.Text = "Every";
        lblAutoSaveEvery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // chkOptionsAutoSave
        // 
        chkOptionsAutoSave.BackColor = System.Drawing.Color.Transparent;
        chkOptionsAutoSave.Dock = DockStyle.Fill;
        chkOptionsAutoSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsAutoSave.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsAutoSave.Location = new System.Drawing.Point(0, 1);
        chkOptionsAutoSave.Margin = new Padding(0, 1, 0, 1);
        chkOptionsAutoSave.Name = "chkOptionsAutoSave";
        chkOptionsAutoSave.Size = new System.Drawing.Size(17, 21);
        chkOptionsAutoSave.TabIndex = 8;
        chkOptionsAutoSave.UseVisualStyleBackColor = false;
        // 
        // flwMidRightToRight
        // 
        flwMidRightToRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        flwMidRightToRight.AutoSize = true;
        flwMidRightToRight.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flwMidRightToRight.Controls.Add(flwMidRightTopDown);
        flwMidRightToRight.FlowDirection = FlowDirection.RightToLeft;
        flwMidRightToRight.Location = new System.Drawing.Point(294, 0);
        flwMidRightToRight.Margin = new Padding(0);
        flwMidRightToRight.Name = "flwMidRightToRight";
        flwMidRightToRight.Size = new System.Drawing.Size(294, 42);
        flwMidRightToRight.TabIndex = 1;
        // 
        // flwMidRightTopDown
        // 
        flwMidRightTopDown.AutoSize = true;
        flwMidRightTopDown.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flwMidRightTopDown.Controls.Add(chkOptionsLoadedGamesPaused);
        flwMidRightTopDown.Controls.Add(tlbMouseScrollWheelBehavior);
        flwMidRightTopDown.FlowDirection = FlowDirection.TopDown;
        flwMidRightTopDown.Location = new System.Drawing.Point(0, 0);
        flwMidRightTopDown.Margin = new Padding(0);
        flwMidRightTopDown.Name = "flwMidRightTopDown";
        flwMidRightTopDown.Size = new System.Drawing.Size(294, 42);
        flwMidRightTopDown.TabIndex = 0;
        flwMidRightTopDown.WrapContents = false;
        // 
        // chkOptionsLoadedGamesPaused
        // 
        chkOptionsLoadedGamesPaused.AutoSize = true;
        chkOptionsLoadedGamesPaused.BackColor = System.Drawing.Color.Transparent;
        chkOptionsLoadedGamesPaused.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        chkOptionsLoadedGamesPaused.Dock = DockStyle.Fill;
        chkOptionsLoadedGamesPaused.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsLoadedGamesPaused.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsLoadedGamesPaused.Location = new System.Drawing.Point(0, 1);
        chkOptionsLoadedGamesPaused.Margin = new Padding(0, 1, 0, 1);
        chkOptionsLoadedGamesPaused.Name = "chkOptionsLoadedGamesPaused";
        chkOptionsLoadedGamesPaused.Size = new System.Drawing.Size(294, 17);
        chkOptionsLoadedGamesPaused.TabIndex = 10;
        chkOptionsLoadedGamesPaused.Text = "Loaded games are paused";
        chkOptionsLoadedGamesPaused.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        chkOptionsLoadedGamesPaused.TextImageRelation = TextImageRelation.TextBeforeImage;
        chkOptionsLoadedGamesPaused.UseVisualStyleBackColor = false;
        // 
        // tlbMouseScrollWheelBehavior
        // 
        tlbMouseScrollWheelBehavior.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tlbMouseScrollWheelBehavior.AutoSize = true;
        tlbMouseScrollWheelBehavior.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tlbMouseScrollWheelBehavior.ColumnCount = 3;
        tlbMouseScrollWheelBehavior.ColumnStyles.Add(new ColumnStyle());
        tlbMouseScrollWheelBehavior.ColumnStyles.Add(new ColumnStyle());
        tlbMouseScrollWheelBehavior.ColumnStyles.Add(new ColumnStyle());
        tlbMouseScrollWheelBehavior.Controls.Add(lblOptionsMouseScrollMode, 1, 1);
        tlbMouseScrollWheelBehavior.Controls.Add(cmbOptionsMouseScrollWheelBehaviour, 2, 1);
        tlbMouseScrollWheelBehavior.Location = new System.Drawing.Point(0, 19);
        tlbMouseScrollWheelBehavior.Margin = new Padding(0);
        tlbMouseScrollWheelBehavior.Name = "tlbMouseScrollWheelBehavior";
        tlbMouseScrollWheelBehavior.RowCount = 3;
        tlbMouseScrollWheelBehavior.RowStyles.Add(new RowStyle());
        tlbMouseScrollWheelBehavior.RowStyles.Add(new RowStyle());
        tlbMouseScrollWheelBehavior.RowStyles.Add(new RowStyle());
        tlbMouseScrollWheelBehavior.Size = new System.Drawing.Size(294, 23);
        tlbMouseScrollWheelBehavior.TabIndex = 1;
        // 
        // lblOptionsMouseScrollMode
        // 
        lblOptionsMouseScrollMode.AutoSize = true;
        lblOptionsMouseScrollMode.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMouseScrollMode.Dock = DockStyle.Fill;
        lblOptionsMouseScrollMode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMouseScrollMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMouseScrollMode.Location = new System.Drawing.Point(0, 1);
        lblOptionsMouseScrollMode.Margin = new Padding(0, 1, 0, 1);
        lblOptionsMouseScrollMode.Name = "lblOptionsMouseScrollMode";
        lblOptionsMouseScrollMode.Size = new System.Drawing.Size(169, 21);
        lblOptionsMouseScrollMode.TabIndex = 0;
        lblOptionsMouseScrollMode.Text = "Mouse scroll-wheel behavior";
        lblOptionsMouseScrollMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // cmbOptionsMouseScrollWheelBehaviour
        // 
        cmbOptionsMouseScrollWheelBehaviour.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsMouseScrollWheelBehaviour.Dock = DockStyle.Fill;
        cmbOptionsMouseScrollWheelBehaviour.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsMouseScrollWheelBehaviour.FlatStyle = FlatStyle.Popup;
        cmbOptionsMouseScrollWheelBehaviour.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsMouseScrollWheelBehaviour.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsMouseScrollWheelBehaviour.FormattingEnabled = true;
        cmbOptionsMouseScrollWheelBehaviour.Items.AddRange(new object[] { "No movement", "Move to selected item", "Move to mouse cursor location" });
        cmbOptionsMouseScrollWheelBehaviour.Location = new System.Drawing.Point(169, 1);
        cmbOptionsMouseScrollWheelBehaviour.Margin = new Padding(0, 1, 0, 1);
        cmbOptionsMouseScrollWheelBehaviour.Name = "cmbOptionsMouseScrollWheelBehaviour";
        cmbOptionsMouseScrollWheelBehaviour.Size = new System.Drawing.Size(125, 21);
        cmbOptionsMouseScrollWheelBehaviour.TabIndex = 11;
        // 
        // grpOptionsDisplaySettings
        // 
        grpOptionsDisplaySettings.AutoSize = true;
        grpOptionsDisplaySettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        grpOptionsDisplaySettings.BackColor = System.Drawing.Color.Transparent;
        grpOptionsDisplaySettings.Controls.Add(tblDisplaySettings);
        grpOptionsDisplaySettings.Dock = DockStyle.Fill;
        grpOptionsDisplaySettings.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsDisplaySettings.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsDisplaySettings.Location = new System.Drawing.Point(0, 0);
        grpOptionsDisplaySettings.Margin = new Padding(0);
        grpOptionsDisplaySettings.Name = "grpOptionsDisplaySettings";
        grpOptionsDisplaySettings.Padding = new Padding(0, 0, 0, 2);
        grpOptionsDisplaySettings.Size = new System.Drawing.Size(588, 124);
        grpOptionsDisplaySettings.TabIndex = 2;
        grpOptionsDisplaySettings.TabStop = false;
        grpOptionsDisplaySettings.Text = "Display Settings";
        // 
        // grpOptionsControl
        // 
        grpOptionsControl.AutoSize = true;
        grpOptionsControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        grpOptionsControl.BackColor = System.Drawing.Color.Transparent;
        grpOptionsControl.Controls.Add(tlbAutomation);
        grpOptionsControl.Dock = DockStyle.Fill;
        grpOptionsControl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsControl.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsControl.Location = new System.Drawing.Point(0, 238);
        grpOptionsControl.Margin = new Padding(0);
        grpOptionsControl.Name = "grpOptionsControl";
        grpOptionsControl.Padding = new Padding(0, 0, 0, 2);
        grpOptionsControl.Size = new System.Drawing.Size(588, 214);
        grpOptionsControl.TabIndex = 3;
        grpOptionsControl.TabStop = false;
        grpOptionsControl.Text = "Automation";
        // 
        // tlbAutomation
        // 
        tlbAutomation.AutoSize = true;
        tlbAutomation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tlbAutomation.ColumnCount = 2;
        tlbAutomation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        tlbAutomation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        tlbAutomation.Controls.Add(tlbAutomationRight, 1, 0);
        tlbAutomation.Controls.Add(flwAutomationLeft, 0, 0);
        tlbAutomation.Dock = DockStyle.Fill;
        tlbAutomation.Location = new System.Drawing.Point(0, 14);
        tlbAutomation.Margin = new Padding(0);
        tlbAutomation.Name = "tlbAutomation";
        tlbAutomation.RowCount = 1;
        tlbAutomation.RowStyles.Add(new RowStyle());
        tlbAutomation.Size = new System.Drawing.Size(588, 198);
        tlbAutomation.TabIndex = 0;
        // 
        // tlbAutomationRight
        // 
        tlbAutomationRight.AutoSize = true;
        tlbAutomationRight.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tlbAutomationRight.ColumnCount = 3;
        tlbAutomationRight.ColumnStyles.Add(new ColumnStyle());
        tlbAutomationRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tlbAutomationRight.ColumnStyles.Add(new ColumnStyle());
        tlbAutomationRight.Controls.Add(cmbOptionsControlOfferPirateMissions, 2, 8);
        tlbAutomationRight.Controls.Add(lblOptionsControlOfferPirateMissions, 1, 8);
        tlbAutomationRight.Controls.Add(cmbOptionsControlColonization, 2, 0);
        tlbAutomationRight.Controls.Add(lblOptionsControlColonization, 1, 0);
        tlbAutomationRight.Controls.Add(cmbOptionsControlColonyFacilities, 2, 7);
        tlbAutomationRight.Controls.Add(lblOptionsControlColonyFacilities, 1, 7);
        tlbAutomationRight.Controls.Add(lblOptionsControlConstruction, 1, 1);
        tlbAutomationRight.Controls.Add(lblOptionsControlAgentMissions, 1, 2);
        tlbAutomationRight.Controls.Add(cmbOptionsControlDiplomacyOffense, 2, 6);
        tlbAutomationRight.Controls.Add(lblOptionsControlDiplomacyOffense, 1, 6);
        tlbAutomationRight.Controls.Add(lblOptionsControlDiplomacyTreaties, 1, 5);
        tlbAutomationRight.Controls.Add(lblOptionsControlAttacks, 1, 3);
        tlbAutomationRight.Controls.Add(cmbOptionsControlDiplomacyTreaties, 2, 5);
        tlbAutomationRight.Controls.Add(lblOptionsControlDiplomacyGifts, 1, 4);
        tlbAutomationRight.Controls.Add(cmbOptionsControlAttacks, 2, 3);
        tlbAutomationRight.Controls.Add(cmbOptionsControlConstruction, 2, 1);
        tlbAutomationRight.Controls.Add(cmbOptionsControlDiplomacyGifts, 2, 4);
        tlbAutomationRight.Controls.Add(cmbOptionsControlAgentMissions, 2, 2);
        tlbAutomationRight.Dock = DockStyle.Fill;
        tlbAutomationRight.Location = new System.Drawing.Point(235, 0);
        tlbAutomationRight.Margin = new Padding(0);
        tlbAutomationRight.Name = "tlbAutomationRight";
        tlbAutomationRight.RowCount = 10;
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.RowStyles.Add(new RowStyle());
        tlbAutomationRight.Size = new System.Drawing.Size(353, 198);
        tlbAutomationRight.TabIndex = 0;
        // 
        // cmbOptionsControlOfferPirateMissions
        // 
        cmbOptionsControlOfferPirateMissions.Anchor = AnchorStyles.Right;
        cmbOptionsControlOfferPirateMissions.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlOfferPirateMissions.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlOfferPirateMissions.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlOfferPirateMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlOfferPirateMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlOfferPirateMissions.FormattingEnabled = true;
        cmbOptionsControlOfferPirateMissions.Items.AddRange(new object[] { "Control manually", "Suggest new colony facilities", "Fully automate" });
        cmbOptionsControlOfferPirateMissions.Location = new System.Drawing.Point(193, 176);
        cmbOptionsControlOfferPirateMissions.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlOfferPirateMissions.Name = "cmbOptionsControlOfferPirateMissions";
        cmbOptionsControlOfferPirateMissions.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlOfferPirateMissions.TabIndex = 30;
        // 
        // lblOptionsControlOfferPirateMissions
        // 
        lblOptionsControlOfferPirateMissions.AutoSize = true;
        lblOptionsControlOfferPirateMissions.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlOfferPirateMissions.Dock = DockStyle.Fill;
        lblOptionsControlOfferPirateMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlOfferPirateMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlOfferPirateMissions.Location = new System.Drawing.Point(0, 177);
        lblOptionsControlOfferPirateMissions.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlOfferPirateMissions.Name = "lblOptionsControlOfferPirateMissions";
        lblOptionsControlOfferPirateMissions.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlOfferPirateMissions.TabIndex = 1;
        lblOptionsControlOfferPirateMissions.Text = "Offer Pirate Missions";
        lblOptionsControlOfferPirateMissions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // cmbOptionsControlColonization
        // 
        cmbOptionsControlColonization.Anchor = AnchorStyles.Right;
        cmbOptionsControlColonization.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlColonization.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlColonization.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlColonization.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlColonization.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlColonization.FormattingEnabled = true;
        cmbOptionsControlColonization.Items.AddRange(new object[] { "Control manually", "Suggest new colonies", "Fully automate" });
        cmbOptionsControlColonization.Location = new System.Drawing.Point(193, 0);
        cmbOptionsControlColonization.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlColonization.Name = "cmbOptionsControlColonization";
        cmbOptionsControlColonization.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlColonization.TabIndex = 22;
        // 
        // lblOptionsControlColonization
        // 
        lblOptionsControlColonization.AutoSize = true;
        lblOptionsControlColonization.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlColonization.Dock = DockStyle.Fill;
        lblOptionsControlColonization.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlColonization.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlColonization.Location = new System.Drawing.Point(0, 1);
        lblOptionsControlColonization.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlColonization.Name = "lblOptionsControlColonization";
        lblOptionsControlColonization.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlColonization.TabIndex = 3;
        lblOptionsControlColonization.Text = "Colonization";
        lblOptionsControlColonization.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // cmbOptionsControlColonyFacilities
        // 
        cmbOptionsControlColonyFacilities.Anchor = AnchorStyles.Right;
        cmbOptionsControlColonyFacilities.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlColonyFacilities.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlColonyFacilities.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlColonyFacilities.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlColonyFacilities.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlColonyFacilities.FormattingEnabled = true;
        cmbOptionsControlColonyFacilities.Items.AddRange(new object[] { "Control manually", "Suggest new colony facilities", "Fully automate" });
        cmbOptionsControlColonyFacilities.Location = new System.Drawing.Point(193, 154);
        cmbOptionsControlColonyFacilities.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlColonyFacilities.Name = "cmbOptionsControlColonyFacilities";
        cmbOptionsControlColonyFacilities.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlColonyFacilities.TabIndex = 29;
        // 
        // lblOptionsControlColonyFacilities
        // 
        lblOptionsControlColonyFacilities.AutoSize = true;
        lblOptionsControlColonyFacilities.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlColonyFacilities.Dock = DockStyle.Fill;
        lblOptionsControlColonyFacilities.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlColonyFacilities.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlColonyFacilities.Location = new System.Drawing.Point(0, 155);
        lblOptionsControlColonyFacilities.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlColonyFacilities.Name = "lblOptionsControlColonyFacilities";
        lblOptionsControlColonyFacilities.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlColonyFacilities.TabIndex = 5;
        lblOptionsControlColonyFacilities.Text = "Colony Facility Building";
        lblOptionsControlColonyFacilities.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // lblOptionsControlConstruction
        // 
        lblOptionsControlConstruction.AutoSize = true;
        lblOptionsControlConstruction.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlConstruction.Dock = DockStyle.Fill;
        lblOptionsControlConstruction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlConstruction.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlConstruction.Location = new System.Drawing.Point(0, 23);
        lblOptionsControlConstruction.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlConstruction.Name = "lblOptionsControlConstruction";
        lblOptionsControlConstruction.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlConstruction.TabIndex = 6;
        lblOptionsControlConstruction.Text = "Ship Building";
        lblOptionsControlConstruction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // lblOptionsControlAgentMissions
        // 
        lblOptionsControlAgentMissions.AutoSize = true;
        lblOptionsControlAgentMissions.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlAgentMissions.Dock = DockStyle.Fill;
        lblOptionsControlAgentMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlAgentMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlAgentMissions.Location = new System.Drawing.Point(0, 45);
        lblOptionsControlAgentMissions.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlAgentMissions.Name = "lblOptionsControlAgentMissions";
        lblOptionsControlAgentMissions.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlAgentMissions.TabIndex = 7;
        lblOptionsControlAgentMissions.Text = "Agent Missions";
        lblOptionsControlAgentMissions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // cmbOptionsControlDiplomacyOffense
        // 
        cmbOptionsControlDiplomacyOffense.Anchor = AnchorStyles.Right;
        cmbOptionsControlDiplomacyOffense.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlDiplomacyOffense.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlDiplomacyOffense.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlDiplomacyOffense.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlDiplomacyOffense.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlDiplomacyOffense.FormattingEnabled = true;
        cmbOptionsControlDiplomacyOffense.Items.AddRange(new object[] { "Control manually", "Suggest war and trade sanctions", "Fully automate" });
        cmbOptionsControlDiplomacyOffense.Location = new System.Drawing.Point(193, 132);
        cmbOptionsControlDiplomacyOffense.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlDiplomacyOffense.Name = "cmbOptionsControlDiplomacyOffense";
        cmbOptionsControlDiplomacyOffense.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlDiplomacyOffense.TabIndex = 28;
        // 
        // lblOptionsControlDiplomacyOffense
        // 
        lblOptionsControlDiplomacyOffense.AutoSize = true;
        lblOptionsControlDiplomacyOffense.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlDiplomacyOffense.Dock = DockStyle.Fill;
        lblOptionsControlDiplomacyOffense.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlDiplomacyOffense.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlDiplomacyOffense.Location = new System.Drawing.Point(0, 133);
        lblOptionsControlDiplomacyOffense.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlDiplomacyOffense.Name = "lblOptionsControlDiplomacyOffense";
        lblOptionsControlDiplomacyOffense.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlDiplomacyOffense.TabIndex = 9;
        lblOptionsControlDiplomacyOffense.Text = "War && Trade Sanctions";
        lblOptionsControlDiplomacyOffense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // lblOptionsControlDiplomacyTreaties
        // 
        lblOptionsControlDiplomacyTreaties.AutoSize = true;
        lblOptionsControlDiplomacyTreaties.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlDiplomacyTreaties.Dock = DockStyle.Fill;
        lblOptionsControlDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlDiplomacyTreaties.Location = new System.Drawing.Point(0, 111);
        lblOptionsControlDiplomacyTreaties.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlDiplomacyTreaties.Name = "lblOptionsControlDiplomacyTreaties";
        lblOptionsControlDiplomacyTreaties.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlDiplomacyTreaties.TabIndex = 10;
        lblOptionsControlDiplomacyTreaties.Text = "Treaties";
        lblOptionsControlDiplomacyTreaties.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // lblOptionsControlAttacks
        // 
        lblOptionsControlAttacks.AutoSize = true;
        lblOptionsControlAttacks.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlAttacks.Dock = DockStyle.Fill;
        lblOptionsControlAttacks.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlAttacks.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlAttacks.Location = new System.Drawing.Point(0, 67);
        lblOptionsControlAttacks.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlAttacks.Name = "lblOptionsControlAttacks";
        lblOptionsControlAttacks.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlAttacks.TabIndex = 11;
        lblOptionsControlAttacks.Text = "Attacks Against Enemies";
        lblOptionsControlAttacks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // cmbOptionsControlDiplomacyTreaties
        // 
        cmbOptionsControlDiplomacyTreaties.Anchor = AnchorStyles.Right;
        cmbOptionsControlDiplomacyTreaties.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlDiplomacyTreaties.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlDiplomacyTreaties.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlDiplomacyTreaties.FormattingEnabled = true;
        cmbOptionsControlDiplomacyTreaties.Items.AddRange(new object[] { "Control manually", "Suggest new treaties", "Fully automate" });
        cmbOptionsControlDiplomacyTreaties.Location = new System.Drawing.Point(193, 110);
        cmbOptionsControlDiplomacyTreaties.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlDiplomacyTreaties.Name = "cmbOptionsControlDiplomacyTreaties";
        cmbOptionsControlDiplomacyTreaties.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlDiplomacyTreaties.TabIndex = 27;
        // 
        // lblOptionsControlDiplomacyGifts
        // 
        lblOptionsControlDiplomacyGifts.AutoSize = true;
        lblOptionsControlDiplomacyGifts.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlDiplomacyGifts.Dock = DockStyle.Fill;
        lblOptionsControlDiplomacyGifts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlDiplomacyGifts.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlDiplomacyGifts.Location = new System.Drawing.Point(0, 89);
        lblOptionsControlDiplomacyGifts.Margin = new Padding(0, 1, 0, 1);
        lblOptionsControlDiplomacyGifts.Name = "lblOptionsControlDiplomacyGifts";
        lblOptionsControlDiplomacyGifts.Size = new System.Drawing.Size(193, 20);
        lblOptionsControlDiplomacyGifts.TabIndex = 13;
        lblOptionsControlDiplomacyGifts.Text = "Sending Diplomatic Gifts";
        lblOptionsControlDiplomacyGifts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // cmbOptionsControlAttacks
        // 
        cmbOptionsControlAttacks.Anchor = AnchorStyles.Right;
        cmbOptionsControlAttacks.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlAttacks.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlAttacks.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlAttacks.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlAttacks.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlAttacks.FormattingEnabled = true;
        cmbOptionsControlAttacks.Items.AddRange(new object[] { "Control manually", "Suggest attack targets", "Fully automate" });
        cmbOptionsControlAttacks.Location = new System.Drawing.Point(193, 66);
        cmbOptionsControlAttacks.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlAttacks.Name = "cmbOptionsControlAttacks";
        cmbOptionsControlAttacks.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlAttacks.TabIndex = 25;
        // 
        // cmbOptionsControlConstruction
        // 
        cmbOptionsControlConstruction.Anchor = AnchorStyles.Right;
        cmbOptionsControlConstruction.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlConstruction.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlConstruction.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlConstruction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlConstruction.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlConstruction.FormattingEnabled = true;
        cmbOptionsControlConstruction.Items.AddRange(new object[] { "Control manually", "Suggest new ships and bases", "Fully automate" });
        cmbOptionsControlConstruction.Location = new System.Drawing.Point(193, 22);
        cmbOptionsControlConstruction.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlConstruction.Name = "cmbOptionsControlConstruction";
        cmbOptionsControlConstruction.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlConstruction.TabIndex = 23;
        // 
        // cmbOptionsControlDiplomacyGifts
        // 
        cmbOptionsControlDiplomacyGifts.Anchor = AnchorStyles.Right;
        cmbOptionsControlDiplomacyGifts.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlDiplomacyGifts.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlDiplomacyGifts.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlDiplomacyGifts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlDiplomacyGifts.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlDiplomacyGifts.FormattingEnabled = true;
        cmbOptionsControlDiplomacyGifts.Items.AddRange(new object[] { "Control manually", "Suggest gifts to empires", "Fully automate" });
        cmbOptionsControlDiplomacyGifts.Location = new System.Drawing.Point(193, 88);
        cmbOptionsControlDiplomacyGifts.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlDiplomacyGifts.Name = "cmbOptionsControlDiplomacyGifts";
        cmbOptionsControlDiplomacyGifts.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlDiplomacyGifts.TabIndex = 26;
        // 
        // cmbOptionsControlAgentMissions
        // 
        cmbOptionsControlAgentMissions.Anchor = AnchorStyles.Right;
        cmbOptionsControlAgentMissions.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlAgentMissions.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlAgentMissions.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlAgentMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlAgentMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlAgentMissions.FormattingEnabled = true;
        cmbOptionsControlAgentMissions.Items.AddRange(new object[] { "Control manually", "Suggest offensive missions", "Fully automate" });
        cmbOptionsControlAgentMissions.Location = new System.Drawing.Point(193, 44);
        cmbOptionsControlAgentMissions.Margin = new Padding(0, 0, 0, 1);
        cmbOptionsControlAgentMissions.Name = "cmbOptionsControlAgentMissions";
        cmbOptionsControlAgentMissions.Size = new System.Drawing.Size(160, 21);
        cmbOptionsControlAgentMissions.TabIndex = 24;
        // 
        // flwAutomationLeft
        // 
        flwAutomationLeft.AutoSize = true;
        flwAutomationLeft.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flwAutomationLeft.Controls.Add(flwAutomationTopLeft);
        flwAutomationLeft.Controls.Add(chkOptionsControlColonyTaxRates);
        flwAutomationLeft.Controls.Add(chkOptionsControlDesigns);
        flwAutomationLeft.Controls.Add(chkOptionsControlFleets);
        flwAutomationLeft.Controls.Add(chkOptionsControlTroops);
        flwAutomationLeft.Controls.Add(chkOptionsControlResearch);
        flwAutomationLeft.Controls.Add(chkOptionsControlPopulationPolicy);
        flwAutomationLeft.Controls.Add(chkOptionsControlCharacterLocations);
        flwAutomationLeft.Controls.Add(btnGameOptionsEmpireSettings);
        flwAutomationLeft.Dock = DockStyle.Fill;
        flwAutomationLeft.FlowDirection = FlowDirection.TopDown;
        flwAutomationLeft.Location = new System.Drawing.Point(0, 0);
        flwAutomationLeft.Margin = new Padding(0);
        flwAutomationLeft.Name = "flwAutomationLeft";
        flwAutomationLeft.Size = new System.Drawing.Size(235, 198);
        flwAutomationLeft.TabIndex = 1;
        flwAutomationLeft.WrapContents = false;
        // 
        // flwAutomationTopLeft
        // 
        flwAutomationTopLeft.AutoSize = true;
        flwAutomationTopLeft.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flwAutomationTopLeft.Controls.Add(pnlOptionsAutomationMode);
        flwAutomationTopLeft.Controls.Add(btnGameOptionsResetAutomationMessages);
        flwAutomationTopLeft.Location = new System.Drawing.Point(0, 0);
        flwAutomationTopLeft.Margin = new Padding(0);
        flwAutomationTopLeft.Name = "flwAutomationTopLeft";
        flwAutomationTopLeft.Size = new System.Drawing.Size(182, 36);
        flwAutomationTopLeft.TabIndex = 0;
        flwAutomationTopLeft.WrapContents = false;
        // 
        // pnlOptionsAutomationMode
        // 
        pnlOptionsAutomationMode.AutoSize = true;
        pnlOptionsAutomationMode.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        pnlOptionsAutomationMode.BackColor = System.Drawing.Color.FromArgb(128, 128, 0, 64);
        pnlOptionsAutomationMode.ColumnCount = 2;
        pnlOptionsAutomationMode.ColumnStyles.Add(new ColumnStyle());
        pnlOptionsAutomationMode.ColumnStyles.Add(new ColumnStyle());
        pnlOptionsAutomationMode.Controls.Add(lblOptionsAutomationMode, 0, 1);
        pnlOptionsAutomationMode.Controls.Add(cmbOptionsAutomationMode, 1, 1);
        pnlOptionsAutomationMode.Dock = DockStyle.Left;
        pnlOptionsAutomationMode.Location = new System.Drawing.Point(0, 0);
        pnlOptionsAutomationMode.Margin = new Padding(0);
        pnlOptionsAutomationMode.Name = "pnlOptionsAutomationMode";
        pnlOptionsAutomationMode.Padding = new Padding(0, 3, 0, 3);
        pnlOptionsAutomationMode.RowCount = 3;
        pnlOptionsAutomationMode.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        pnlOptionsAutomationMode.RowStyles.Add(new RowStyle());
        pnlOptionsAutomationMode.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        pnlOptionsAutomationMode.Size = new System.Drawing.Size(117, 36);
        pnlOptionsAutomationMode.TabIndex = 0;
        // 
        // lblOptionsAutomationMode
        // 
        lblOptionsAutomationMode.AutoSize = true;
        lblOptionsAutomationMode.BackColor = System.Drawing.Color.Transparent;
        lblOptionsAutomationMode.Dock = DockStyle.Fill;
        lblOptionsAutomationMode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsAutomationMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsAutomationMode.Location = new System.Drawing.Point(0, 7);
        lblOptionsAutomationMode.Margin = new Padding(0);
        lblOptionsAutomationMode.Name = "lblOptionsAutomationMode";
        lblOptionsAutomationMode.Size = new System.Drawing.Size(37, 21);
        lblOptionsAutomationMode.TabIndex = 0;
        lblOptionsAutomationMode.Text = "Mode";
        lblOptionsAutomationMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // cmbOptionsAutomationMode
        // 
        cmbOptionsAutomationMode.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsAutomationMode.Dock = DockStyle.Fill;
        cmbOptionsAutomationMode.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsAutomationMode.FlatStyle = FlatStyle.Popup;
        cmbOptionsAutomationMode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsAutomationMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsAutomationMode.FormattingEnabled = true;
        cmbOptionsAutomationMode.Items.AddRange(new object[] { "(Custom)", "Default", "Expert (None)", "Rule in Absence (Full)", "Expansion", "War and Combat", "Diplomacy", "Spy Master" });
        cmbOptionsAutomationMode.Location = new System.Drawing.Point(37, 7);
        cmbOptionsAutomationMode.Margin = new Padding(0);
        cmbOptionsAutomationMode.Name = "cmbOptionsAutomationMode";
        cmbOptionsAutomationMode.Size = new System.Drawing.Size(80, 21);
        cmbOptionsAutomationMode.TabIndex = 12;
        // 
        // btnGameOptionsResetAutomationMessages
        // 
        btnGameOptionsResetAutomationMessages.AutoSize = true;
        btnGameOptionsResetAutomationMessages.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnGameOptionsResetAutomationMessages.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        btnGameOptionsResetAutomationMessages.ClipBackground = false;
        btnGameOptionsResetAutomationMessages.DelayFrameRefresh = false;
        btnGameOptionsResetAutomationMessages.Dock = DockStyle.Fill;
        btnGameOptionsResetAutomationMessages.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        btnGameOptionsResetAutomationMessages.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        btnGameOptionsResetAutomationMessages.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        btnGameOptionsResetAutomationMessages.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        btnGameOptionsResetAutomationMessages.IntensifyColors = false;
        btnGameOptionsResetAutomationMessages.Location = new System.Drawing.Point(120, 3);
        btnGameOptionsResetAutomationMessages.Name = "btnGameOptionsResetAutomationMessages";
        btnGameOptionsResetAutomationMessages.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        btnGameOptionsResetAutomationMessages.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        btnGameOptionsResetAutomationMessages.Size = new System.Drawing.Size(59, 30);
        btnGameOptionsResetAutomationMessages.TabIndex = 13;
        btnGameOptionsResetAutomationMessages.Text = "Reset\r\nWarnings";
        btnGameOptionsResetAutomationMessages.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        btnGameOptionsResetAutomationMessages.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        btnGameOptionsResetAutomationMessages.ToggledOn = false;
        // 
        // chkOptionsControlColonyTaxRates
        // 
        chkOptionsControlColonyTaxRates.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlColonyTaxRates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlColonyTaxRates.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlColonyTaxRates.Location = new System.Drawing.Point(0, 37);
        chkOptionsControlColonyTaxRates.Margin = new Padding(0, 1, 0, 1);
        chkOptionsControlColonyTaxRates.Name = "chkOptionsControlColonyTaxRates";
        chkOptionsControlColonyTaxRates.Size = new System.Drawing.Size(127, 17);
        chkOptionsControlColonyTaxRates.TabIndex = 14;
        chkOptionsControlColonyTaxRates.Text = "Colony Tax Rates";
        chkOptionsControlColonyTaxRates.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlDesigns
        // 
        chkOptionsControlDesigns.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlDesigns.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlDesigns.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlDesigns.Location = new System.Drawing.Point(0, 56);
        chkOptionsControlDesigns.Margin = new Padding(0, 1, 0, 1);
        chkOptionsControlDesigns.Name = "chkOptionsControlDesigns";
        chkOptionsControlDesigns.Size = new System.Drawing.Size(94, 17);
        chkOptionsControlDesigns.TabIndex = 15;
        chkOptionsControlDesigns.Text = "Ship Design";
        chkOptionsControlDesigns.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlFleets
        // 
        chkOptionsControlFleets.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlFleets.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlFleets.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlFleets.Location = new System.Drawing.Point(0, 75);
        chkOptionsControlFleets.Margin = new Padding(0, 1, 0, 1);
        chkOptionsControlFleets.Name = "chkOptionsControlFleets";
        chkOptionsControlFleets.Size = new System.Drawing.Size(114, 17);
        chkOptionsControlFleets.TabIndex = 16;
        chkOptionsControlFleets.Text = "Fleet Formation";
        chkOptionsControlFleets.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlTroops
        // 
        chkOptionsControlTroops.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlTroops.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlTroops.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlTroops.Location = new System.Drawing.Point(0, 94);
        chkOptionsControlTroops.Margin = new Padding(0, 1, 0, 1);
        chkOptionsControlTroops.Name = "chkOptionsControlTroops";
        chkOptionsControlTroops.Size = new System.Drawing.Size(132, 17);
        chkOptionsControlTroops.TabIndex = 17;
        chkOptionsControlTroops.Text = "Troop Recruitment";
        chkOptionsControlTroops.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlResearch
        // 
        chkOptionsControlResearch.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlResearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlResearch.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlResearch.Location = new System.Drawing.Point(0, 113);
        chkOptionsControlResearch.Margin = new Padding(0, 1, 0, 1);
        chkOptionsControlResearch.Name = "chkOptionsControlResearch";
        chkOptionsControlResearch.Size = new System.Drawing.Size(79, 17);
        chkOptionsControlResearch.TabIndex = 18;
        chkOptionsControlResearch.Text = "Research";
        chkOptionsControlResearch.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlPopulationPolicy
        // 
        chkOptionsControlPopulationPolicy.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlPopulationPolicy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlPopulationPolicy.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlPopulationPolicy.Location = new System.Drawing.Point(0, 132);
        chkOptionsControlPopulationPolicy.Margin = new Padding(0, 1, 0, 1);
        chkOptionsControlPopulationPolicy.Name = "chkOptionsControlPopulationPolicy";
        chkOptionsControlPopulationPolicy.Size = new System.Drawing.Size(175, 17);
        chkOptionsControlPopulationPolicy.TabIndex = 19;
        chkOptionsControlPopulationPolicy.Text = "Colony Population Policies";
        chkOptionsControlPopulationPolicy.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlCharacterLocations
        // 
        chkOptionsControlCharacterLocations.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlCharacterLocations.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlCharacterLocations.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlCharacterLocations.Location = new System.Drawing.Point(0, 151);
        chkOptionsControlCharacterLocations.Margin = new Padding(0, 1, 0, 1);
        chkOptionsControlCharacterLocations.Name = "chkOptionsControlCharacterLocations";
        chkOptionsControlCharacterLocations.Size = new System.Drawing.Size(140, 17);
        chkOptionsControlCharacterLocations.TabIndex = 20;
        chkOptionsControlCharacterLocations.Text = "Character Locations";
        chkOptionsControlCharacterLocations.UseVisualStyleBackColor = false;
        // 
        // btnGameOptionsEmpireSettings
        // 
        btnGameOptionsEmpireSettings.AutoSize = true;
        btnGameOptionsEmpireSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        btnGameOptionsEmpireSettings.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        btnGameOptionsEmpireSettings.ClipBackground = false;
        btnGameOptionsEmpireSettings.DelayFrameRefresh = false;
        btnGameOptionsEmpireSettings.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        btnGameOptionsEmpireSettings.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        btnGameOptionsEmpireSettings.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        btnGameOptionsEmpireSettings.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        btnGameOptionsEmpireSettings.IntensifyColors = false;
        btnGameOptionsEmpireSettings.Location = new System.Drawing.Point(3, 172);
        btnGameOptionsEmpireSettings.Name = "btnGameOptionsEmpireSettings";
        btnGameOptionsEmpireSettings.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        btnGameOptionsEmpireSettings.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        btnGameOptionsEmpireSettings.Size = new System.Drawing.Size(117, 20);
        btnGameOptionsEmpireSettings.TabIndex = 21;
        btnGameOptionsEmpireSettings.Text = "Other Empire Settings";
        btnGameOptionsEmpireSettings.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        btnGameOptionsEmpireSettings.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        btnGameOptionsEmpireSettings.ToggledOn = false;
        // 
        // grpOptionsVolume
        // 
        grpOptionsVolume.AutoSize = true;
        grpOptionsVolume.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        grpOptionsVolume.BackColor = System.Drawing.Color.Transparent;
        grpOptionsVolume.Controls.Add(tlbSoundVolume);
        grpOptionsVolume.Dock = DockStyle.Fill;
        grpOptionsVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsVolume.Location = new System.Drawing.Point(0, 124);
        grpOptionsVolume.Margin = new Padding(0);
        grpOptionsVolume.Name = "grpOptionsVolume";
        grpOptionsVolume.Padding = new Padding(0, 0, 0, 2);
        grpOptionsVolume.Size = new System.Drawing.Size(588, 56);
        grpOptionsVolume.TabIndex = 4;
        grpOptionsVolume.TabStop = false;
        grpOptionsVolume.Text = "Sound Volume";
        // 
        // grpOptionsPopupMessages
        // 
        grpOptionsPopupMessages.BackColor = System.Drawing.Color.Transparent;
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageConstructionResourceShortage);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageUnderAttackCivilianBases);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageUnderAttackMilitaryShips);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageUnderAttackExplorationShips);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageUnderAttackOtherStateBases);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageUnderAttackColonyConstructionShips);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageUnderAttackColoniesSpaceports);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageUnderAttackCivilianShips);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageShipNeedsRefuelling);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageShipMissionComplete);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageExploration);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageIntelligenceMissions);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageResearchBreakthrough);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageEmpireMetDestroyed);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageColonyGainLoss);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageDiplomacyWarTradeSanctions);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageDiplomacyTreaties);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageRequestWarning);
        grpOptionsPopupMessages.Controls.Add(chkOptionsPopupMessageShipBuilt);
        grpOptionsPopupMessages.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsPopupMessages.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsPopupMessages.Location = new System.Drawing.Point(317, 14);
        grpOptionsPopupMessages.Margin = new Padding(0);
        grpOptionsPopupMessages.Name = "grpOptionsPopupMessages";
        grpOptionsPopupMessages.Size = new System.Drawing.Size(295, 349);
        grpOptionsPopupMessages.TabIndex = 0;
        grpOptionsPopupMessages.TabStop = false;
        grpOptionsPopupMessages.Text = "Popup Messages";
        // 
        // chkOptionsPopupMessageConstructionResourceShortage
        // 
        chkOptionsPopupMessageConstructionResourceShortage.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageConstructionResourceShortage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageConstructionResourceShortage.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageConstructionResourceShortage.Location = new System.Drawing.Point(7, 325);
        chkOptionsPopupMessageConstructionResourceShortage.Margin = new Padding(0);
        chkOptionsPopupMessageConstructionResourceShortage.Name = "chkOptionsPopupMessageConstructionResourceShortage";
        chkOptionsPopupMessageConstructionResourceShortage.Size = new System.Drawing.Size(211, 17);
        chkOptionsPopupMessageConstructionResourceShortage.TabIndex = 0;
        chkOptionsPopupMessageConstructionResourceShortage.TabStop = false;
        chkOptionsPopupMessageConstructionResourceShortage.Text = "Construction Resource Shortage";
        chkOptionsPopupMessageConstructionResourceShortage.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackCivilianBases
        // 
        chkOptionsPopupMessageUnderAttackCivilianBases.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackCivilianBases.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackCivilianBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackCivilianBases.Location = new System.Drawing.Point(7, 223);
        chkOptionsPopupMessageUnderAttackCivilianBases.Margin = new Padding(0);
        chkOptionsPopupMessageUnderAttackCivilianBases.Name = "chkOptionsPopupMessageUnderAttackCivilianBases";
        chkOptionsPopupMessageUnderAttackCivilianBases.Size = new System.Drawing.Size(193, 17);
        chkOptionsPopupMessageUnderAttackCivilianBases.TabIndex = 1;
        chkOptionsPopupMessageUnderAttackCivilianBases.TabStop = false;
        chkOptionsPopupMessageUnderAttackCivilianBases.Text = "Under Attack - Civilian Bases";
        chkOptionsPopupMessageUnderAttackCivilianBases.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackMilitaryShips
        // 
        chkOptionsPopupMessageUnderAttackMilitaryShips.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackMilitaryShips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackMilitaryShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackMilitaryShips.Location = new System.Drawing.Point(7, 274);
        chkOptionsPopupMessageUnderAttackMilitaryShips.Margin = new Padding(0);
        chkOptionsPopupMessageUnderAttackMilitaryShips.Name = "chkOptionsPopupMessageUnderAttackMilitaryShips";
        chkOptionsPopupMessageUnderAttackMilitaryShips.Size = new System.Drawing.Size(189, 17);
        chkOptionsPopupMessageUnderAttackMilitaryShips.TabIndex = 2;
        chkOptionsPopupMessageUnderAttackMilitaryShips.TabStop = false;
        chkOptionsPopupMessageUnderAttackMilitaryShips.Text = "Under Attack - Military Ships";
        chkOptionsPopupMessageUnderAttackMilitaryShips.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackExplorationShips
        // 
        chkOptionsPopupMessageUnderAttackExplorationShips.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackExplorationShips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackExplorationShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackExplorationShips.Location = new System.Drawing.Point(7, 240);
        chkOptionsPopupMessageUnderAttackExplorationShips.Margin = new Padding(0);
        chkOptionsPopupMessageUnderAttackExplorationShips.Name = "chkOptionsPopupMessageUnderAttackExplorationShips";
        chkOptionsPopupMessageUnderAttackExplorationShips.Size = new System.Drawing.Size(212, 17);
        chkOptionsPopupMessageUnderAttackExplorationShips.TabIndex = 3;
        chkOptionsPopupMessageUnderAttackExplorationShips.TabStop = false;
        chkOptionsPopupMessageUnderAttackExplorationShips.Text = "Under Attack - Exploration Ships";
        chkOptionsPopupMessageUnderAttackExplorationShips.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackOtherStateBases
        // 
        chkOptionsPopupMessageUnderAttackOtherStateBases.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackOtherStateBases.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackOtherStateBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackOtherStateBases.Location = new System.Drawing.Point(7, 291);
        chkOptionsPopupMessageUnderAttackOtherStateBases.Margin = new Padding(0);
        chkOptionsPopupMessageUnderAttackOtherStateBases.Name = "chkOptionsPopupMessageUnderAttackOtherStateBases";
        chkOptionsPopupMessageUnderAttackOtherStateBases.Size = new System.Drawing.Size(284, 17);
        chkOptionsPopupMessageUnderAttackOtherStateBases.TabIndex = 4;
        chkOptionsPopupMessageUnderAttackOtherStateBases.TabStop = false;
        chkOptionsPopupMessageUnderAttackOtherStateBases.Text = "Under Attack - Research, Monitoring, Resorts";
        chkOptionsPopupMessageUnderAttackOtherStateBases.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackColonyConstructionShips
        // 
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Location = new System.Drawing.Point(7, 257);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Margin = new Padding(0);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Name = "chkOptionsPopupMessageUnderAttackColonyConstructionShips";
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Size = new System.Drawing.Size(276, 17);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.TabIndex = 5;
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.TabStop = false;
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Text = "Under Attack - Colony && Construction Ships";
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackColoniesSpaceports
        // 
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Location = new System.Drawing.Point(7, 308);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Margin = new Padding(0);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Name = "chkOptionsPopupMessageUnderAttackColoniesSpaceports";
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Size = new System.Drawing.Size(242, 17);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.TabIndex = 6;
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.TabStop = false;
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Text = "Under Attack - Colonies && Spaceports";
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackCivilianShips
        // 
        chkOptionsPopupMessageUnderAttackCivilianShips.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackCivilianShips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackCivilianShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackCivilianShips.Location = new System.Drawing.Point(7, 206);
        chkOptionsPopupMessageUnderAttackCivilianShips.Margin = new Padding(0);
        chkOptionsPopupMessageUnderAttackCivilianShips.Name = "chkOptionsPopupMessageUnderAttackCivilianShips";
        chkOptionsPopupMessageUnderAttackCivilianShips.Size = new System.Drawing.Size(190, 17);
        chkOptionsPopupMessageUnderAttackCivilianShips.TabIndex = 7;
        chkOptionsPopupMessageUnderAttackCivilianShips.TabStop = false;
        chkOptionsPopupMessageUnderAttackCivilianShips.Text = "Under Attack - Civilian Ships";
        chkOptionsPopupMessageUnderAttackCivilianShips.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageShipNeedsRefuelling
        // 
        chkOptionsPopupMessageShipNeedsRefuelling.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageShipNeedsRefuelling.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageShipNeedsRefuelling.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageShipNeedsRefuelling.Location = new System.Drawing.Point(7, 189);
        chkOptionsPopupMessageShipNeedsRefuelling.Margin = new Padding(0);
        chkOptionsPopupMessageShipNeedsRefuelling.Name = "chkOptionsPopupMessageShipNeedsRefuelling";
        chkOptionsPopupMessageShipNeedsRefuelling.Size = new System.Drawing.Size(207, 17);
        chkOptionsPopupMessageShipNeedsRefuelling.TabIndex = 8;
        chkOptionsPopupMessageShipNeedsRefuelling.TabStop = false;
        chkOptionsPopupMessageShipNeedsRefuelling.Text = "Ship Needs Refuelling or Repair";
        chkOptionsPopupMessageShipNeedsRefuelling.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageShipMissionComplete
        // 
        chkOptionsPopupMessageShipMissionComplete.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageShipMissionComplete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageShipMissionComplete.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageShipMissionComplete.Location = new System.Drawing.Point(7, 172);
        chkOptionsPopupMessageShipMissionComplete.Margin = new Padding(0);
        chkOptionsPopupMessageShipMissionComplete.Name = "chkOptionsPopupMessageShipMissionComplete";
        chkOptionsPopupMessageShipMissionComplete.Size = new System.Drawing.Size(155, 17);
        chkOptionsPopupMessageShipMissionComplete.TabIndex = 9;
        chkOptionsPopupMessageShipMissionComplete.TabStop = false;
        chkOptionsPopupMessageShipMissionComplete.Text = "Ship Mission Complete";
        chkOptionsPopupMessageShipMissionComplete.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageExploration
        // 
        chkOptionsPopupMessageExploration.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageExploration.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageExploration.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageExploration.Location = new System.Drawing.Point(7, 155);
        chkOptionsPopupMessageExploration.Margin = new Padding(0);
        chkOptionsPopupMessageExploration.Name = "chkOptionsPopupMessageExploration";
        chkOptionsPopupMessageExploration.Size = new System.Drawing.Size(158, 17);
        chkOptionsPopupMessageExploration.TabIndex = 10;
        chkOptionsPopupMessageExploration.TabStop = false;
        chkOptionsPopupMessageExploration.Text = "Exploration discoveries";
        chkOptionsPopupMessageExploration.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageIntelligenceMissions
        // 
        chkOptionsPopupMessageIntelligenceMissions.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageIntelligenceMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageIntelligenceMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageIntelligenceMissions.Location = new System.Drawing.Point(7, 138);
        chkOptionsPopupMessageIntelligenceMissions.Margin = new Padding(0);
        chkOptionsPopupMessageIntelligenceMissions.Name = "chkOptionsPopupMessageIntelligenceMissions";
        chkOptionsPopupMessageIntelligenceMissions.Size = new System.Drawing.Size(143, 17);
        chkOptionsPopupMessageIntelligenceMissions.TabIndex = 11;
        chkOptionsPopupMessageIntelligenceMissions.TabStop = false;
        chkOptionsPopupMessageIntelligenceMissions.Text = "Intelligence Missions";
        chkOptionsPopupMessageIntelligenceMissions.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageResearchBreakthrough
        // 
        chkOptionsPopupMessageResearchBreakthrough.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageResearchBreakthrough.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageResearchBreakthrough.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageResearchBreakthrough.Location = new System.Drawing.Point(7, 121);
        chkOptionsPopupMessageResearchBreakthrough.Margin = new Padding(0);
        chkOptionsPopupMessageResearchBreakthrough.Name = "chkOptionsPopupMessageResearchBreakthrough";
        chkOptionsPopupMessageResearchBreakthrough.Size = new System.Drawing.Size(160, 17);
        chkOptionsPopupMessageResearchBreakthrough.TabIndex = 12;
        chkOptionsPopupMessageResearchBreakthrough.TabStop = false;
        chkOptionsPopupMessageResearchBreakthrough.Text = "Research breakthrough";
        chkOptionsPopupMessageResearchBreakthrough.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageEmpireMetDestroyed
        // 
        chkOptionsPopupMessageEmpireMetDestroyed.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageEmpireMetDestroyed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageEmpireMetDestroyed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageEmpireMetDestroyed.Location = new System.Drawing.Point(7, 104);
        chkOptionsPopupMessageEmpireMetDestroyed.Margin = new Padding(0);
        chkOptionsPopupMessageEmpireMetDestroyed.Name = "chkOptionsPopupMessageEmpireMetDestroyed";
        chkOptionsPopupMessageEmpireMetDestroyed.Size = new System.Drawing.Size(127, 17);
        chkOptionsPopupMessageEmpireMetDestroyed.TabIndex = 13;
        chkOptionsPopupMessageEmpireMetDestroyed.TabStop = false;
        chkOptionsPopupMessageEmpireMetDestroyed.Text = "Empire Discovery";
        chkOptionsPopupMessageEmpireMetDestroyed.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageColonyGainLoss
        // 
        chkOptionsPopupMessageColonyGainLoss.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageColonyGainLoss.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageColonyGainLoss.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageColonyGainLoss.Location = new System.Drawing.Point(7, 87);
        chkOptionsPopupMessageColonyGainLoss.Margin = new Padding(0);
        chkOptionsPopupMessageColonyGainLoss.Name = "chkOptionsPopupMessageColonyGainLoss";
        chkOptionsPopupMessageColonyGainLoss.Size = new System.Drawing.Size(141, 17);
        chkOptionsPopupMessageColonyGainLoss.TabIndex = 14;
        chkOptionsPopupMessageColonyGainLoss.TabStop = false;
        chkOptionsPopupMessageColonyGainLoss.Text = "Colony Gain or Loss";
        chkOptionsPopupMessageColonyGainLoss.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageDiplomacyWarTradeSanctions
        // 
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Location = new System.Drawing.Point(7, 70);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Margin = new Padding(0);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Name = "chkOptionsPopupMessageDiplomacyWarTradeSanctions";
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Size = new System.Drawing.Size(157, 17);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.TabIndex = 15;
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.TabStop = false;
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Text = "War && Trade Sanctions";
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageDiplomacyTreaties
        // 
        chkOptionsPopupMessageDiplomacyTreaties.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageDiplomacyTreaties.Location = new System.Drawing.Point(7, 53);
        chkOptionsPopupMessageDiplomacyTreaties.Margin = new Padding(0);
        chkOptionsPopupMessageDiplomacyTreaties.Name = "chkOptionsPopupMessageDiplomacyTreaties";
        chkOptionsPopupMessageDiplomacyTreaties.Size = new System.Drawing.Size(100, 17);
        chkOptionsPopupMessageDiplomacyTreaties.TabIndex = 16;
        chkOptionsPopupMessageDiplomacyTreaties.TabStop = false;
        chkOptionsPopupMessageDiplomacyTreaties.Text = "Treaty offers";
        chkOptionsPopupMessageDiplomacyTreaties.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageRequestWarning
        // 
        chkOptionsPopupMessageRequestWarning.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageRequestWarning.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageRequestWarning.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageRequestWarning.Location = new System.Drawing.Point(7, 36);
        chkOptionsPopupMessageRequestWarning.Margin = new Padding(0);
        chkOptionsPopupMessageRequestWarning.Name = "chkOptionsPopupMessageRequestWarning";
        chkOptionsPopupMessageRequestWarning.Size = new System.Drawing.Size(181, 17);
        chkOptionsPopupMessageRequestWarning.TabIndex = 17;
        chkOptionsPopupMessageRequestWarning.TabStop = false;
        chkOptionsPopupMessageRequestWarning.Text = "Requests, Warnings && Gifts";
        chkOptionsPopupMessageRequestWarning.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageShipBuilt
        // 
        chkOptionsPopupMessageShipBuilt.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageShipBuilt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageShipBuilt.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageShipBuilt.Location = new System.Drawing.Point(7, 19);
        chkOptionsPopupMessageShipBuilt.Margin = new Padding(0);
        chkOptionsPopupMessageShipBuilt.Name = "chkOptionsPopupMessageShipBuilt";
        chkOptionsPopupMessageShipBuilt.Size = new System.Drawing.Size(108, 17);
        chkOptionsPopupMessageShipBuilt.TabIndex = 18;
        chkOptionsPopupMessageShipBuilt.TabStop = false;
        chkOptionsPopupMessageShipBuilt.Text = "New Ship Built";
        chkOptionsPopupMessageShipBuilt.UseVisualStyleBackColor = false;
        // 
        // GameOptionsScreenPanel
        // 
        AutoSize = false;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        Controls.Add(tlbBody);
        HeaderTitle = "Options";
        Location = new System.Drawing.Point(227, 32);
        Name = "GameOptionsScreenPanel";
        Size = new System.Drawing.Size(588, 480);
        tblDisplaySettings.ResumeLayout(false);
        tblDisplaySettings.PerformLayout();
        flwAdvancedSettings.ResumeLayout(false);
        tlbSoundVolume.ResumeLayout(false);
        tlbSoundVolume.PerformLayout();
        tlbBody.ResumeLayout(false);
        tlbBody.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        tlbMiscMiddle.ResumeLayout(false);
        tlbMiscMiddle.PerformLayout();
        flwMidLeft.ResumeLayout(false);
        flwMidLeft.PerformLayout();
        grpOptionsAutoSave.ResumeLayout(false);
        grpOptionsAutoSave.PerformLayout();
        flwAutoSave.ResumeLayout(false);
        flwAutoSave.PerformLayout();
        tlbAutoSave.ResumeLayout(false);
        tlbAutoSave.PerformLayout();
        ((ISupportInitialize)numOptionsAutoSaveMinutes).EndInit();
        flwMidRightToRight.ResumeLayout(false);
        flwMidRightToRight.PerformLayout();
        flwMidRightTopDown.ResumeLayout(false);
        flwMidRightTopDown.PerformLayout();
        tlbMouseScrollWheelBehavior.ResumeLayout(false);
        tlbMouseScrollWheelBehavior.PerformLayout();
        grpOptionsDisplaySettings.ResumeLayout(false);
        grpOptionsDisplaySettings.PerformLayout();
        grpOptionsControl.ResumeLayout(false);
        grpOptionsControl.PerformLayout();
        tlbAutomation.ResumeLayout(false);
        tlbAutomation.PerformLayout();
        tlbAutomationRight.ResumeLayout(false);
        tlbAutomationRight.PerformLayout();
        flwAutomationLeft.ResumeLayout(false);
        flwAutomationLeft.PerformLayout();
        flwAutomationTopLeft.ResumeLayout(false);
        flwAutomationTopLeft.PerformLayout();
        pnlOptionsAutomationMode.ResumeLayout(false);
        pnlOptionsAutomationMode.PerformLayout();
        grpOptionsVolume.ResumeLayout(false);
        grpOptionsVolume.PerformLayout();
        grpOptionsPopupMessages.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
  }

  #endregion

  internal GlassButton btnGameOptionsShowMessages;
  internal GroupBox grpOptionsAutoSave;
  internal GroupBox grpOptionsDisplaySettings;
  internal System.Windows.Forms.CheckBox chkOptionsAutoPauseInPopup;
  internal GroupBox grpOptionsVolume;
  internal GroupBox grpOptionsControl;
  internal NumericUpDown numOptionsAutoSaveMinutes;
  internal System.Windows.Forms.CheckBox chkOptionsAutoSave;

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

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageExploration;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageIntelligenceMissions;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageRequestWarning;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageColonyGainLoss;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageDiplomacyTreaties;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageDiplomacyWarTradeSanctions;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageEmpireMetDestroyed;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageResearchBreakthrough;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageShipBuilt;

        internal Label lblOptionsSoundEffectsVolume;

        internal Label lblOptionsMusicVolume;

        internal ColorSlider sldOptionsSoundEffectsVolume;

        internal ColorSlider sldOptionsMusicVolume;

        internal ComboBox cmbOptionsAutomationMode;

        internal Label lblOptionsAutomationMode;

        internal Label lblOptionsControlOfferPirateMissions;

        internal ComboBox cmbOptionsControlOfferPirateMissions;


        internal System.Windows.Forms.CheckBox chkOptionsControlPopulationPolicy;

        internal System.Windows.Forms.CheckBox chkOptionsControlCharacterLocations;

        internal Label lblOptionsControlColonyFacilities;

        internal ComboBox cmbOptionsControlColonyFacilities;

        internal System.Windows.Forms.CheckBox chkOptionsControlResearch;

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

        internal System.Windows.Forms.CheckBox chkOptionsControlTroops;

        internal System.Windows.Forms.CheckBox chkOptionsControlColonyTaxRates;

        internal System.Windows.Forms.CheckBox chkOptionsControlFleets;

        internal System.Windows.Forms.CheckBox chkOptionsControlDesigns;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageConstructionResourceShortage;


        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageUnderAttackCivilianBases;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageUnderAttackMilitaryShips;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageUnderAttackExplorationShips;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageUnderAttackOtherStateBases;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageUnderAttackColonyConstructionShips;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageUnderAttackColoniesSpaceports;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageUnderAttackCivilianShips;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageShipNeedsRefuelling;

        internal System.Windows.Forms.CheckBox chkOptionsPopupMessageShipMissionComplete;
    private TableLayoutPanel tlbBody;
    private FlowLayoutPanel flwAutoSave;
    private TableLayoutPanel tlbAutoSave;
    internal Label lblAutoSaveMinutes;
    internal Label lblAutoSaveEvery;
    private TableLayoutPanel tlbAutomationRight;
    internal TableLayoutPanel pnlOptionsAutomationMode;
    private FlowLayoutPanel flwAutomationLeft;
    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tlbAutomation;
    private FlowLayoutPanel flwMidRightToRight;
    internal System.Windows.Forms.CheckBox chkOptionsLoadedGamesPaused;
    internal Label lblOptionsMouseScrollMode;
    internal ComboBox cmbOptionsMouseScrollWheelBehaviour;
}