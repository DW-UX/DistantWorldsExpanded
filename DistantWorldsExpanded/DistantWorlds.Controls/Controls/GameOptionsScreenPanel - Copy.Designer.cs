using System.ComponentModel;
using System.Windows.Forms;

namespace DistantWorlds.Controls; 

partial class GameOptionsScreenPanelTemp {

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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(GameOptionsScreenPanelTemp));
        btnGameOptionsShowMessages = new GlassButton();
        chkOptionsLoadedGamesPaused = new CheckBox();
        grpOptionsAutoSave = new GroupBox();
        numOptionsAutoSaveMinutes = new NumericUpDown();
        chkOptionsAutoSave = new CheckBox();
        lblOptionsMouseScrollMode = new Label();
        cmbOptionsMouseScrollWheelBehaviour = new ComboBox();
        grpOptionsDisplaySettings = new GroupBox();
        tableLayoutPanel3 = new TableLayoutPanel();
        sldOptionsMainViewGuiScale = new ColorSlider();
        sldOptionsMainViewScrollSpeed = new ColorSlider();
        lblOptionsMainViewZoomSpeed = new Label();
        lblOptionsMainViewScrollSpeed = new Label();
        sldOptionsMainViewZoomSpeed = new ColorSlider();
        sldOptionsMainViewStarFieldSize = new ColorSlider();
        lblOptionsMainViewStarFieldSize = new Label();
        btnGameOptionsAdvancedDisplaySettings = new GlassButton();
        lblOptionsMainViewGuiScale = new Label();
        chkOptionsAutoPauseInPopup = new CheckBox();
        grpOptionsVolume = new GroupBox();
        tableLayoutPanel2 = new TableLayoutPanel();
        lblOptionsMusicVolume = new Label();
        sldOptionsSoundEffectsVolume = new ColorSlider();
        lblOptionsSoundEffectsVolume = new Label();
        sldOptionsMusicVolume = new ColorSlider();
        grpOptionsControl = new GroupBox();
        tableLayoutPanel1 = new TableLayoutPanel();
        btnGameOptionsResetAutomationMessages = new GlassButton();
        cmbOptionsControlOfferPirateMissions = new ComboBox();
        lblOptionsControlOfferPirateMissions = new Label();
        pnlOptionsAutomationMode = new System.Windows.Forms.Panel();
        lblOptionsAutomationMode = new Label();
        cmbOptionsAutomationMode = new ComboBox();
        lblOptionsControlColonization = new Label();
        btnGameOptionsEmpireSettings = new GlassButton();
        cmbOptionsControlColonization = new ComboBox();
        chkOptionsControlPopulationPolicy = new CheckBox();
        chkOptionsControlCharacterLocations = new CheckBox();
        chkOptionsControlResearch = new CheckBox();
        chkOptionsControlColonyTaxRates = new CheckBox();
        cmbOptionsControlConstruction = new ComboBox();
        lblOptionsControlConstruction = new Label();
        chkOptionsControlDesigns = new CheckBox();
        cmbOptionsControlAgentMissions = new ComboBox();
        lblOptionsControlAgentMissions = new Label();
        cmbOptionsControlAttacks = new ComboBox();
        lblOptionsControlAttacks = new Label();
        chkOptionsControlTroops = new CheckBox();
        chkOptionsControlFleets = new CheckBox();
        lblOptionsControlDiplomacyGifts = new Label();
        cmbOptionsControlDiplomacyGifts = new ComboBox();
        cmbOptionsControlDiplomacyTreaties = new ComboBox();
        lblOptionsControlDiplomacyTreaties = new Label();
        cmbOptionsControlDiplomacyOffense = new ComboBox();
        lblOptionsControlDiplomacyOffense = new Label();
        cmbOptionsControlColonyFacilities = new ComboBox();
        lblOptionsControlColonyFacilities = new Label();
        grpOptionsPopupMessages = new GroupBox();
        chkOptionsPopupMessageConstructionResourceShortage = new CheckBox();
        chkOptionsPopupMessageUnderAttackCivilianBases = new CheckBox();
        chkOptionsPopupMessageUnderAttackMilitaryShips = new CheckBox();
        chkOptionsPopupMessageUnderAttackExplorationShips = new CheckBox();
        chkOptionsPopupMessageUnderAttackOtherStateBases = new CheckBox();
        chkOptionsPopupMessageUnderAttackColonyConstructionShips = new CheckBox();
        chkOptionsPopupMessageUnderAttackColoniesSpaceports = new CheckBox();
        chkOptionsPopupMessageUnderAttackCivilianShips = new CheckBox();
        chkOptionsPopupMessageShipNeedsRefuelling = new CheckBox();
        chkOptionsPopupMessageShipMissionComplete = new CheckBox();
        chkOptionsPopupMessageExploration = new CheckBox();
        chkOptionsPopupMessageIntelligenceMissions = new CheckBox();
        chkOptionsPopupMessageResearchBreakthrough = new CheckBox();
        chkOptionsPopupMessageEmpireMetDestroyed = new CheckBox();
        chkOptionsPopupMessageColonyGainLoss = new CheckBox();
        chkOptionsPopupMessageDiplomacyWarTradeSanctions = new CheckBox();
        chkOptionsPopupMessageDiplomacyTreaties = new CheckBox();
        chkOptionsPopupMessageRequestWarning = new CheckBox();
        chkOptionsPopupMessageShipBuilt = new CheckBox();
        tableLayoutPanel4 = new TableLayoutPanel();
        tableLayoutPanel5 = new TableLayoutPanel();
        grpOptionsAutoSave.SuspendLayout();
        ((ISupportInitialize)numOptionsAutoSaveMinutes).BeginInit();
        grpOptionsDisplaySettings.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        grpOptionsVolume.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        grpOptionsControl.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        pnlOptionsAutomationMode.SuspendLayout();
        grpOptionsPopupMessages.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel5.SuspendLayout();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.TitleText = "Options";
        // 
        // btnGameOptionsShowMessages
        // 
        btnGameOptionsShowMessages.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        btnGameOptionsShowMessages.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        btnGameOptionsShowMessages.ClipBackground = false;
        btnGameOptionsShowMessages.DelayFrameRefresh = false;
        btnGameOptionsShowMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        btnGameOptionsShowMessages.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        btnGameOptionsShowMessages.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        btnGameOptionsShowMessages.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        btnGameOptionsShowMessages.IntensifyColors = false;
        btnGameOptionsShowMessages.Location = new System.Drawing.Point(9, 608);
        btnGameOptionsShowMessages.Name = "btnGameOptionsShowMessages";
        btnGameOptionsShowMessages.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        btnGameOptionsShowMessages.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        btnGameOptionsShowMessages.Size = new System.Drawing.Size(515, 35);
        btnGameOptionsShowMessages.TabIndex = 76;
        btnGameOptionsShowMessages.Text = "Show Message Options";
        btnGameOptionsShowMessages.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        btnGameOptionsShowMessages.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        btnGameOptionsShowMessages.ToggledOn = false;
        // 
        // chkOptionsLoadedGamesPaused
        // 
        chkOptionsLoadedGamesPaused.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        chkOptionsLoadedGamesPaused.BackColor = System.Drawing.Color.Transparent;
        chkOptionsLoadedGamesPaused.Font = new System.Drawing.Font("Verdana", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsLoadedGamesPaused.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsLoadedGamesPaused.Location = new System.Drawing.Point(242, 3);
        chkOptionsLoadedGamesPaused.Name = "chkOptionsLoadedGamesPaused";
        chkOptionsLoadedGamesPaused.Size = new System.Drawing.Size(98, 17);
        chkOptionsLoadedGamesPaused.TabIndex = 75;
        chkOptionsLoadedGamesPaused.Text = "Loaded games are paused";
        chkOptionsLoadedGamesPaused.UseVisualStyleBackColor = false;
        // 
        // grpOptionsAutoSave
        // 
        grpOptionsAutoSave.BackColor = System.Drawing.Color.Transparent;
        grpOptionsAutoSave.Controls.Add(numOptionsAutoSaveMinutes);
        grpOptionsAutoSave.Controls.Add(chkOptionsAutoSave);
        grpOptionsAutoSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsAutoSave.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsAutoSave.Location = new System.Drawing.Point(3, 3);
        grpOptionsAutoSave.Name = "grpOptionsAutoSave";
        grpOptionsAutoSave.Size = new System.Drawing.Size(160, 44);
        grpOptionsAutoSave.TabIndex = 74;
        grpOptionsAutoSave.TabStop = false;
        grpOptionsAutoSave.Text = "Auto Save";
        // 
        // numOptionsAutoSaveMinutes
        // 
        numOptionsAutoSaveMinutes.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        numOptionsAutoSaveMinutes.BorderStyle = BorderStyle.FixedSingle;
        numOptionsAutoSaveMinutes.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        numOptionsAutoSaveMinutes.Location = new System.Drawing.Point(60, 16);
        numOptionsAutoSaveMinutes.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
        numOptionsAutoSaveMinutes.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        numOptionsAutoSaveMinutes.Name = "numOptionsAutoSaveMinutes";
        numOptionsAutoSaveMinutes.Size = new System.Drawing.Size(35, 21);
        numOptionsAutoSaveMinutes.TabIndex = 31;
        numOptionsAutoSaveMinutes.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // chkOptionsAutoSave
        // 
        chkOptionsAutoSave.BackColor = System.Drawing.Color.Transparent;
        chkOptionsAutoSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsAutoSave.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsAutoSave.Location = new System.Drawing.Point(7, 17);
        chkOptionsAutoSave.Name = "chkOptionsAutoSave";
        chkOptionsAutoSave.Size = new System.Drawing.Size(144, 17);
        chkOptionsAutoSave.TabIndex = 13;
        chkOptionsAutoSave.Text = "Every          minutes";
        chkOptionsAutoSave.UseVisualStyleBackColor = false;
        // 
        // lblOptionsMouseScrollMode
        // 
        lblOptionsMouseScrollMode.AutoSize = true;
        lblOptionsMouseScrollMode.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMouseScrollMode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMouseScrollMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMouseScrollMode.Location = new System.Drawing.Point(3, 23);
        lblOptionsMouseScrollMode.Name = "lblOptionsMouseScrollMode";
        lblOptionsMouseScrollMode.Size = new System.Drawing.Size(169, 13);
        lblOptionsMouseScrollMode.TabIndex = 73;
        lblOptionsMouseScrollMode.Text = "Mouse scroll-wheel behavior";
        // 
        // cmbOptionsMouseScrollWheelBehaviour
        // 
        cmbOptionsMouseScrollWheelBehaviour.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsMouseScrollWheelBehaviour.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsMouseScrollWheelBehaviour.FlatStyle = FlatStyle.Popup;
        cmbOptionsMouseScrollWheelBehaviour.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsMouseScrollWheelBehaviour.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsMouseScrollWheelBehaviour.FormattingEnabled = true;
        cmbOptionsMouseScrollWheelBehaviour.Items.AddRange(new object[] { "No movement", "Move to selected item", "Move to mouse cursor location" });
        cmbOptionsMouseScrollWheelBehaviour.Location = new System.Drawing.Point(178, 26);
        cmbOptionsMouseScrollWheelBehaviour.Name = "cmbOptionsMouseScrollWheelBehaviour";
        cmbOptionsMouseScrollWheelBehaviour.Size = new System.Drawing.Size(160, 21);
        cmbOptionsMouseScrollWheelBehaviour.TabIndex = 72;
        // 
        // grpOptionsDisplaySettings
        // 
        grpOptionsDisplaySettings.BackColor = System.Drawing.Color.Transparent;
        grpOptionsDisplaySettings.Controls.Add(tableLayoutPanel3);
        grpOptionsDisplaySettings.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsDisplaySettings.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsDisplaySettings.Location = new System.Drawing.Point(12, 7);
        grpOptionsDisplaySettings.Name = "grpOptionsDisplaySettings";
        grpOptionsDisplaySettings.Size = new System.Drawing.Size(515, 136);
        grpOptionsDisplaySettings.TabIndex = 59;
        grpOptionsDisplaySettings.TabStop = false;
        grpOptionsDisplaySettings.Text = "Display Settings";
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.ColumnCount = 2;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel3.Controls.Add(btnGameOptionsAdvancedDisplaySettings, 0, 4);
        tableLayoutPanel3.Controls.Add(sldOptionsMainViewGuiScale, 1, 3);
        tableLayoutPanel3.Controls.Add(sldOptionsMainViewScrollSpeed, 1, 0);
        tableLayoutPanel3.Controls.Add(lblOptionsMainViewGuiScale, 0, 3);
        tableLayoutPanel3.Controls.Add(lblOptionsMainViewZoomSpeed, 0, 1);
        tableLayoutPanel3.Controls.Add(lblOptionsMainViewScrollSpeed, 0, 0);
        tableLayoutPanel3.Controls.Add(sldOptionsMainViewZoomSpeed, 1, 1);
        tableLayoutPanel3.Controls.Add(sldOptionsMainViewStarFieldSize, 1, 2);
        tableLayoutPanel3.Controls.Add(lblOptionsMainViewStarFieldSize, 0, 2);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 5;
        tableLayoutPanel3.RowStyles.Add(new RowStyle());
        tableLayoutPanel3.RowStyles.Add(new RowStyle());
        tableLayoutPanel3.RowStyles.Add(new RowStyle());
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel3.Size = new System.Drawing.Size(509, 116);
        tableLayoutPanel3.TabIndex = 1000;
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
        sldOptionsMainViewGuiScale.Location = new System.Drawing.Point(89, 69);
        sldOptionsMainViewGuiScale.Maximum = 4000;
        sldOptionsMainViewGuiScale.Minimum = 500;
        sldOptionsMainViewGuiScale.Name = "sldOptionsMainViewGuiScale";
        sldOptionsMainViewGuiScale.Size = new System.Drawing.Size(417, 14);
        sldOptionsMainViewGuiScale.SmallChange = 1U;
        sldOptionsMainViewGuiScale.TabIndex = 999;
        sldOptionsMainViewGuiScale.Text = "colorSlider1";
        sldOptionsMainViewGuiScale.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewGuiScale.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewGuiScale.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewGuiScale.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMainViewGuiScale.ThumbSize = 20;
        sldOptionsMainViewGuiScale.Value = 1000;
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
        sldOptionsMainViewScrollSpeed.Location = new System.Drawing.Point(89, 3);
        sldOptionsMainViewScrollSpeed.Minimum = 1;
        sldOptionsMainViewScrollSpeed.Name = "sldOptionsMainViewScrollSpeed";
        sldOptionsMainViewScrollSpeed.Size = new System.Drawing.Size(417, 16);
        sldOptionsMainViewScrollSpeed.SmallChange = 1U;
        sldOptionsMainViewScrollSpeed.TabIndex = 58;
        sldOptionsMainViewScrollSpeed.Text = "colorSlider1";
        sldOptionsMainViewScrollSpeed.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewScrollSpeed.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewScrollSpeed.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewScrollSpeed.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMainViewScrollSpeed.ThumbSize = 20;
        // 
        // lblOptionsMainViewZoomSpeed
        // 
        lblOptionsMainViewZoomSpeed.AutoSize = true;
        lblOptionsMainViewZoomSpeed.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMainViewZoomSpeed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMainViewZoomSpeed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMainViewZoomSpeed.Location = new System.Drawing.Point(3, 22);
        lblOptionsMainViewZoomSpeed.Name = "lblOptionsMainViewZoomSpeed";
        lblOptionsMainViewZoomSpeed.Size = new System.Drawing.Size(80, 13);
        lblOptionsMainViewZoomSpeed.TabIndex = 63;
        lblOptionsMainViewZoomSpeed.Text = "Zoom Speed";
        // 
        // lblOptionsMainViewScrollSpeed
        // 
        lblOptionsMainViewScrollSpeed.AutoSize = true;
        lblOptionsMainViewScrollSpeed.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMainViewScrollSpeed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMainViewScrollSpeed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMainViewScrollSpeed.Location = new System.Drawing.Point(3, 0);
        lblOptionsMainViewScrollSpeed.Name = "lblOptionsMainViewScrollSpeed";
        lblOptionsMainViewScrollSpeed.Size = new System.Drawing.Size(79, 13);
        lblOptionsMainViewScrollSpeed.TabIndex = 59;
        lblOptionsMainViewScrollSpeed.Text = "Scroll Speed";
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
        sldOptionsMainViewZoomSpeed.Location = new System.Drawing.Point(89, 25);
        sldOptionsMainViewZoomSpeed.Minimum = 10;
        sldOptionsMainViewZoomSpeed.Name = "sldOptionsMainViewZoomSpeed";
        sldOptionsMainViewZoomSpeed.Size = new System.Drawing.Size(417, 16);
        sldOptionsMainViewZoomSpeed.SmallChange = 1U;
        sldOptionsMainViewZoomSpeed.TabIndex = 62;
        sldOptionsMainViewZoomSpeed.Text = "colorSlider1";
        sldOptionsMainViewZoomSpeed.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewZoomSpeed.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewZoomSpeed.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewZoomSpeed.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMainViewZoomSpeed.ThumbSize = 20;
        sldOptionsMainViewZoomSpeed.Value = 30;
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
        sldOptionsMainViewStarFieldSize.Location = new System.Drawing.Point(89, 47);
        sldOptionsMainViewStarFieldSize.Maximum = 2000;
        sldOptionsMainViewStarFieldSize.Minimum = 50;
        sldOptionsMainViewStarFieldSize.Name = "sldOptionsMainViewStarFieldSize";
        sldOptionsMainViewStarFieldSize.Size = new System.Drawing.Size(417, 16);
        sldOptionsMainViewStarFieldSize.SmallChange = 1U;
        sldOptionsMainViewStarFieldSize.TabIndex = 60;
        sldOptionsMainViewStarFieldSize.Text = "colorSlider1";
        sldOptionsMainViewStarFieldSize.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMainViewStarFieldSize.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMainViewStarFieldSize.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMainViewStarFieldSize.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMainViewStarFieldSize.ThumbSize = 20;
        sldOptionsMainViewStarFieldSize.Value = 1000;
        // 
        // lblOptionsMainViewStarFieldSize
        // 
        lblOptionsMainViewStarFieldSize.AutoSize = true;
        lblOptionsMainViewStarFieldSize.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMainViewStarFieldSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMainViewStarFieldSize.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMainViewStarFieldSize.Location = new System.Drawing.Point(3, 44);
        lblOptionsMainViewStarFieldSize.Name = "lblOptionsMainViewStarFieldSize";
        lblOptionsMainViewStarFieldSize.Size = new System.Drawing.Size(78, 13);
        lblOptionsMainViewStarFieldSize.TabIndex = 61;
        lblOptionsMainViewStarFieldSize.Text = "Star Density";
        // 
        // btnGameOptionsAdvancedDisplaySettings
        // 
        btnGameOptionsAdvancedDisplaySettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnGameOptionsAdvancedDisplaySettings.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        btnGameOptionsAdvancedDisplaySettings.ClipBackground = false;
        tableLayoutPanel3.SetColumnSpan(btnGameOptionsAdvancedDisplaySettings, 2);
        btnGameOptionsAdvancedDisplaySettings.DelayFrameRefresh = false;
        btnGameOptionsAdvancedDisplaySettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        btnGameOptionsAdvancedDisplaySettings.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        btnGameOptionsAdvancedDisplaySettings.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        btnGameOptionsAdvancedDisplaySettings.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        btnGameOptionsAdvancedDisplaySettings.IntensifyColors = false;
        btnGameOptionsAdvancedDisplaySettings.Location = new System.Drawing.Point(3, 91);
        btnGameOptionsAdvancedDisplaySettings.Name = "btnGameOptionsAdvancedDisplaySettings";
        btnGameOptionsAdvancedDisplaySettings.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        btnGameOptionsAdvancedDisplaySettings.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        btnGameOptionsAdvancedDisplaySettings.Size = new System.Drawing.Size(150, 22);
        btnGameOptionsAdvancedDisplaySettings.TabIndex = 66;
        btnGameOptionsAdvancedDisplaySettings.Text = "Advanced Settings...";
        btnGameOptionsAdvancedDisplaySettings.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        btnGameOptionsAdvancedDisplaySettings.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        btnGameOptionsAdvancedDisplaySettings.ToggledOn = false;
        // 
        // lblOptionsMainViewGuiScale
        // 
        lblOptionsMainViewGuiScale.AutoSize = true;
        lblOptionsMainViewGuiScale.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMainViewGuiScale.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMainViewGuiScale.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMainViewGuiScale.Location = new System.Drawing.Point(3, 66);
        lblOptionsMainViewGuiScale.Name = "lblOptionsMainViewGuiScale";
        lblOptionsMainViewGuiScale.Size = new System.Drawing.Size(64, 13);
        lblOptionsMainViewGuiScale.TabIndex = 999;
        lblOptionsMainViewGuiScale.Text = "GUI Scale";
        // 
        // chkOptionsAutoPauseInPopup
        // 
        chkOptionsAutoPauseInPopup.BackColor = System.Drawing.Color.Transparent;
        chkOptionsAutoPauseInPopup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsAutoPauseInPopup.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsAutoPauseInPopup.Location = new System.Drawing.Point(12, 10);
        chkOptionsAutoPauseInPopup.Name = "chkOptionsAutoPauseInPopup";
        chkOptionsAutoPauseInPopup.Size = new System.Drawing.Size(192, 17);
        chkOptionsAutoPauseInPopup.TabIndex = 15;
        chkOptionsAutoPauseInPopup.Text = "Auto Pause in Game Screens";
        chkOptionsAutoPauseInPopup.UseVisualStyleBackColor = false;
        chkOptionsAutoPauseInPopup.Visible = false;
        // 
        // grpOptionsVolume
        // 
        grpOptionsVolume.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpOptionsVolume.BackColor = System.Drawing.Color.Transparent;
        grpOptionsVolume.Controls.Add(tableLayoutPanel2);
        grpOptionsVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsVolume.Location = new System.Drawing.Point(12, 147);
        grpOptionsVolume.Name = "grpOptionsVolume";
        grpOptionsVolume.Size = new System.Drawing.Size(515, 64);
        grpOptionsVolume.TabIndex = 10;
        grpOptionsVolume.TabStop = false;
        grpOptionsVolume.Text = "Sound Volume";
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel2.Controls.Add(lblOptionsMusicVolume, 0, 0);
        tableLayoutPanel2.Controls.Add(sldOptionsSoundEffectsVolume, 1, 1);
        tableLayoutPanel2.Controls.Add(lblOptionsSoundEffectsVolume, 0, 1);
        tableLayoutPanel2.Controls.Add(sldOptionsMusicVolume, 1, 0);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 2;
        tableLayoutPanel2.RowStyles.Add(new RowStyle());
        tableLayoutPanel2.RowStyles.Add(new RowStyle());
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel2.Size = new System.Drawing.Size(509, 44);
        tableLayoutPanel2.TabIndex = 14;
        // 
        // lblOptionsMusicVolume
        // 
        lblOptionsMusicVolume.AutoSize = true;
        lblOptionsMusicVolume.BackColor = System.Drawing.Color.Transparent;
        lblOptionsMusicVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsMusicVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsMusicVolume.Location = new System.Drawing.Point(3, 0);
        lblOptionsMusicVolume.Name = "lblOptionsMusicVolume";
        lblOptionsMusicVolume.Size = new System.Drawing.Size(38, 13);
        lblOptionsMusicVolume.TabIndex = 12;
        lblOptionsMusicVolume.Text = "Music";
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
        sldOptionsSoundEffectsVolume.Location = new System.Drawing.Point(54, 25);
        sldOptionsSoundEffectsVolume.Name = "sldOptionsSoundEffectsVolume";
        sldOptionsSoundEffectsVolume.Size = new System.Drawing.Size(452, 16);
        sldOptionsSoundEffectsVolume.SmallChange = 1U;
        sldOptionsSoundEffectsVolume.TabIndex = 11;
        sldOptionsSoundEffectsVolume.Text = "colorSlider1";
        sldOptionsSoundEffectsVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsSoundEffectsVolume.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsSoundEffectsVolume.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsSoundEffectsVolume.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsSoundEffectsVolume.ThumbSize = 20;
        // 
        // lblOptionsSoundEffectsVolume
        // 
        lblOptionsSoundEffectsVolume.AutoSize = true;
        lblOptionsSoundEffectsVolume.BackColor = System.Drawing.Color.Transparent;
        lblOptionsSoundEffectsVolume.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsSoundEffectsVolume.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsSoundEffectsVolume.Location = new System.Drawing.Point(3, 22);
        lblOptionsSoundEffectsVolume.Name = "lblOptionsSoundEffectsVolume";
        lblOptionsSoundEffectsVolume.Size = new System.Drawing.Size(45, 13);
        lblOptionsSoundEffectsVolume.TabIndex = 13;
        lblOptionsSoundEffectsVolume.Text = "Effects";
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
        sldOptionsMusicVolume.Location = new System.Drawing.Point(54, 3);
        sldOptionsMusicVolume.Name = "sldOptionsMusicVolume";
        sldOptionsMusicVolume.Size = new System.Drawing.Size(452, 16);
        sldOptionsMusicVolume.SmallChange = 1U;
        sldOptionsMusicVolume.TabIndex = 10;
        sldOptionsMusicVolume.Text = "colorSlider1";
        sldOptionsMusicVolume.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 96);
        sldOptionsMusicVolume.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 48, 64);
        sldOptionsMusicVolume.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 40);
        sldOptionsMusicVolume.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
        sldOptionsMusicVolume.ThumbSize = 20;
        // 
        // grpOptionsControl
        // 
        grpOptionsControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpOptionsControl.BackColor = System.Drawing.Color.Transparent;
        grpOptionsControl.Controls.Add(tableLayoutPanel1);
        grpOptionsControl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        grpOptionsControl.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        grpOptionsControl.Location = new System.Drawing.Point(9, 283);
        grpOptionsControl.Name = "grpOptionsControl";
        grpOptionsControl.Size = new System.Drawing.Size(518, 319);
        grpOptionsControl.TabIndex = 4;
        grpOptionsControl.TabStop = false;
        grpOptionsControl.Text = "Automation";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.Controls.Add(btnGameOptionsResetAutomationMessages, 0, 9);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlOfferPirateMissions, 2, 8);
        tableLayoutPanel1.Controls.Add(lblOptionsControlOfferPirateMissions, 1, 8);
        tableLayoutPanel1.Controls.Add(pnlOptionsAutomationMode, 0, 0);
        tableLayoutPanel1.Controls.Add(lblOptionsControlColonization, 1, 0);
        tableLayoutPanel1.Controls.Add(btnGameOptionsEmpireSettings, 0, 8);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlColonization, 2, 0);
        tableLayoutPanel1.Controls.Add(chkOptionsControlPopulationPolicy, 0, 6);
        tableLayoutPanel1.Controls.Add(chkOptionsControlCharacterLocations, 0, 3);
        tableLayoutPanel1.Controls.Add(chkOptionsControlResearch, 0, 7);
        tableLayoutPanel1.Controls.Add(chkOptionsControlColonyTaxRates, 0, 1);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlConstruction, 2, 1);
        tableLayoutPanel1.Controls.Add(lblOptionsControlConstruction, 1, 1);
        tableLayoutPanel1.Controls.Add(chkOptionsControlDesigns, 0, 2);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlAgentMissions, 2, 2);
        tableLayoutPanel1.Controls.Add(lblOptionsControlAgentMissions, 1, 2);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlAttacks, 2, 3);
        tableLayoutPanel1.Controls.Add(lblOptionsControlAttacks, 1, 3);
        tableLayoutPanel1.Controls.Add(chkOptionsControlTroops, 0, 4);
        tableLayoutPanel1.Controls.Add(chkOptionsControlFleets, 0, 5);
        tableLayoutPanel1.Controls.Add(lblOptionsControlDiplomacyGifts, 1, 4);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlDiplomacyGifts, 2, 4);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlDiplomacyTreaties, 2, 5);
        tableLayoutPanel1.Controls.Add(lblOptionsControlDiplomacyTreaties, 1, 5);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlDiplomacyOffense, 2, 6);
        tableLayoutPanel1.Controls.Add(lblOptionsControlDiplomacyOffense, 1, 6);
        tableLayoutPanel1.Controls.Add(cmbOptionsControlColonyFacilities, 2, 7);
        tableLayoutPanel1.Controls.Add(lblOptionsControlColonyFacilities, 1, 7);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Padding = new Padding(3);
        tableLayoutPanel1.RowCount = 10;
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel1.Size = new System.Drawing.Size(512, 299);
        tableLayoutPanel1.TabIndex = 80;
        // 
        // btnGameOptionsResetAutomationMessages
        // 
        btnGameOptionsResetAutomationMessages.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        btnGameOptionsResetAutomationMessages.ClipBackground = false;
        btnGameOptionsResetAutomationMessages.DelayFrameRefresh = false;
        btnGameOptionsResetAutomationMessages.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        btnGameOptionsResetAutomationMessages.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        btnGameOptionsResetAutomationMessages.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        btnGameOptionsResetAutomationMessages.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        btnGameOptionsResetAutomationMessages.IntensifyColors = false;
        btnGameOptionsResetAutomationMessages.Location = new System.Drawing.Point(6, 266);
        btnGameOptionsResetAutomationMessages.Name = "btnGameOptionsResetAutomationMessages";
        btnGameOptionsResetAutomationMessages.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        btnGameOptionsResetAutomationMessages.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        btnGameOptionsResetAutomationMessages.Size = new System.Drawing.Size(144, 30);
        btnGameOptionsResetAutomationMessages.TabIndex = 16;
        btnGameOptionsResetAutomationMessages.Text = "Reset Warnings";
        btnGameOptionsResetAutomationMessages.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        btnGameOptionsResetAutomationMessages.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        btnGameOptionsResetAutomationMessages.ToggledOn = false;
        // 
        // cmbOptionsControlOfferPirateMissions
        // 
        cmbOptionsControlOfferPirateMissions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlOfferPirateMissions.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlOfferPirateMissions.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlOfferPirateMissions.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlOfferPirateMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlOfferPirateMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlOfferPirateMissions.FormattingEnabled = true;
        cmbOptionsControlOfferPirateMissions.Items.AddRange(new object[] { "Control manually", "Suggest new colony facilities", "Fully automate" });
        cmbOptionsControlOfferPirateMissions.Location = new System.Drawing.Point(340, 229);
        cmbOptionsControlOfferPirateMissions.Name = "cmbOptionsControlOfferPirateMissions";
        cmbOptionsControlOfferPirateMissions.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlOfferPirateMissions.TabIndex = 78;
        // 
        // lblOptionsControlOfferPirateMissions
        // 
        lblOptionsControlOfferPirateMissions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlOfferPirateMissions.AutoSize = true;
        lblOptionsControlOfferPirateMissions.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlOfferPirateMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlOfferPirateMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlOfferPirateMissions.Location = new System.Drawing.Point(210, 226);
        lblOptionsControlOfferPirateMissions.Name = "lblOptionsControlOfferPirateMissions";
        lblOptionsControlOfferPirateMissions.Padding = new Padding(0, 7, 0, 0);
        lblOptionsControlOfferPirateMissions.Size = new System.Drawing.Size(124, 20);
        lblOptionsControlOfferPirateMissions.TabIndex = 79;
        lblOptionsControlOfferPirateMissions.Text = "Offer Pirate Missions";
        // 
        // pnlOptionsAutomationMode
        // 
        pnlOptionsAutomationMode.AutoSize = true;
        pnlOptionsAutomationMode.BackColor = System.Drawing.Color.FromArgb(128, 128, 0, 64);
        pnlOptionsAutomationMode.Controls.Add(lblOptionsAutomationMode);
        pnlOptionsAutomationMode.Controls.Add(cmbOptionsAutomationMode);
        pnlOptionsAutomationMode.Dock = DockStyle.Fill;
        pnlOptionsAutomationMode.Location = new System.Drawing.Point(6, 6);
        pnlOptionsAutomationMode.Name = "pnlOptionsAutomationMode";
        pnlOptionsAutomationMode.Size = new System.Drawing.Size(175, 28);
        pnlOptionsAutomationMode.TabIndex = 68;
        // 
        // lblOptionsAutomationMode
        // 
        lblOptionsAutomationMode.AutoSize = true;
        lblOptionsAutomationMode.BackColor = System.Drawing.Color.Transparent;
        lblOptionsAutomationMode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsAutomationMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsAutomationMode.Location = new System.Drawing.Point(4, 7);
        lblOptionsAutomationMode.Name = "lblOptionsAutomationMode";
        lblOptionsAutomationMode.Size = new System.Drawing.Size(37, 13);
        lblOptionsAutomationMode.TabIndex = 67;
        lblOptionsAutomationMode.Text = "Mode";
        // 
        // cmbOptionsAutomationMode
        // 
        cmbOptionsAutomationMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsAutomationMode.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsAutomationMode.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsAutomationMode.FlatStyle = FlatStyle.Popup;
        cmbOptionsAutomationMode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsAutomationMode.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsAutomationMode.FormattingEnabled = true;
        cmbOptionsAutomationMode.Items.AddRange(new object[] { "(Custom)", "Default", "Expert (None)", "Rule in Absence (Full)", "Expansion", "War and Combat", "Diplomacy", "Spy Master" });
        cmbOptionsAutomationMode.Location = new System.Drawing.Point(45, 4);
        cmbOptionsAutomationMode.Name = "cmbOptionsAutomationMode";
        cmbOptionsAutomationMode.Size = new System.Drawing.Size(127, 21);
        cmbOptionsAutomationMode.TabIndex = 66;
        // 
        // lblOptionsControlColonization
        // 
        lblOptionsControlColonization.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlColonization.AutoSize = true;
        lblOptionsControlColonization.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlColonization.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlColonization.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlColonization.Location = new System.Drawing.Point(257, 3);
        lblOptionsControlColonization.Name = "lblOptionsControlColonization";
        lblOptionsControlColonization.Padding = new Padding(0, 10, 0, 0);
        lblOptionsControlColonization.Size = new System.Drawing.Size(77, 23);
        lblOptionsControlColonization.TabIndex = 65;
        lblOptionsControlColonization.Text = "Colonization";
        // 
        // btnGameOptionsEmpireSettings
        // 
        btnGameOptionsEmpireSettings.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
        btnGameOptionsEmpireSettings.ClipBackground = false;
        btnGameOptionsEmpireSettings.DelayFrameRefresh = false;
        btnGameOptionsEmpireSettings.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        btnGameOptionsEmpireSettings.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
        btnGameOptionsEmpireSettings.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
        btnGameOptionsEmpireSettings.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
        btnGameOptionsEmpireSettings.IntensifyColors = false;
        btnGameOptionsEmpireSettings.Location = new System.Drawing.Point(6, 229);
        btnGameOptionsEmpireSettings.Name = "btnGameOptionsEmpireSettings";
        btnGameOptionsEmpireSettings.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
        btnGameOptionsEmpireSettings.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
        btnGameOptionsEmpireSettings.Size = new System.Drawing.Size(144, 31);
        btnGameOptionsEmpireSettings.TabIndex = 71;
        btnGameOptionsEmpireSettings.Text = "Other Empire Settings";
        btnGameOptionsEmpireSettings.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
        btnGameOptionsEmpireSettings.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
        btnGameOptionsEmpireSettings.ToggledOn = false;
        // 
        // cmbOptionsControlColonization
        // 
        cmbOptionsControlColonization.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlColonization.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlColonization.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlColonization.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlColonization.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlColonization.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlColonization.FormattingEnabled = true;
        cmbOptionsControlColonization.Items.AddRange(new object[] { "Control manually", "Suggest new colonies", "Fully automate" });
        cmbOptionsControlColonization.Location = new System.Drawing.Point(340, 10);
        cmbOptionsControlColonization.Margin = new Padding(3, 7, 3, 3);
        cmbOptionsControlColonization.Name = "cmbOptionsControlColonization";
        cmbOptionsControlColonization.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlColonization.TabIndex = 64;
        // 
        // chkOptionsControlPopulationPolicy
        // 
        chkOptionsControlPopulationPolicy.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlPopulationPolicy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlPopulationPolicy.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlPopulationPolicy.Location = new System.Drawing.Point(6, 175);
        chkOptionsControlPopulationPolicy.Name = "chkOptionsControlPopulationPolicy";
        chkOptionsControlPopulationPolicy.Size = new System.Drawing.Size(175, 17);
        chkOptionsControlPopulationPolicy.TabIndex = 76;
        chkOptionsControlPopulationPolicy.Text = "Colony Population Policies";
        chkOptionsControlPopulationPolicy.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlCharacterLocations
        // 
        chkOptionsControlCharacterLocations.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlCharacterLocations.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlCharacterLocations.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlCharacterLocations.Location = new System.Drawing.Point(6, 94);
        chkOptionsControlCharacterLocations.Name = "chkOptionsControlCharacterLocations";
        chkOptionsControlCharacterLocations.Size = new System.Drawing.Size(140, 17);
        chkOptionsControlCharacterLocations.TabIndex = 77;
        chkOptionsControlCharacterLocations.Text = "Character Locations";
        chkOptionsControlCharacterLocations.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlResearch
        // 
        chkOptionsControlResearch.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlResearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlResearch.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlResearch.Location = new System.Drawing.Point(6, 202);
        chkOptionsControlResearch.Name = "chkOptionsControlResearch";
        chkOptionsControlResearch.Size = new System.Drawing.Size(79, 17);
        chkOptionsControlResearch.TabIndex = 72;
        chkOptionsControlResearch.Text = "Research";
        chkOptionsControlResearch.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlColonyTaxRates
        // 
        chkOptionsControlColonyTaxRates.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlColonyTaxRates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlColonyTaxRates.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlColonyTaxRates.Location = new System.Drawing.Point(6, 40);
        chkOptionsControlColonyTaxRates.Name = "chkOptionsControlColonyTaxRates";
        chkOptionsControlColonyTaxRates.Size = new System.Drawing.Size(127, 17);
        chkOptionsControlColonyTaxRates.TabIndex = 10;
        chkOptionsControlColonyTaxRates.Text = "Colony Tax Rates";
        chkOptionsControlColonyTaxRates.UseVisualStyleBackColor = false;
        // 
        // cmbOptionsControlConstruction
        // 
        cmbOptionsControlConstruction.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlConstruction.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlConstruction.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlConstruction.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlConstruction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlConstruction.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlConstruction.FormattingEnabled = true;
        cmbOptionsControlConstruction.Items.AddRange(new object[] { "Control manually", "Suggest new ships and bases", "Fully automate" });
        cmbOptionsControlConstruction.Location = new System.Drawing.Point(340, 40);
        cmbOptionsControlConstruction.Name = "cmbOptionsControlConstruction";
        cmbOptionsControlConstruction.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlConstruction.TabIndex = 62;
        // 
        // lblOptionsControlConstruction
        // 
        lblOptionsControlConstruction.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlConstruction.AutoSize = true;
        lblOptionsControlConstruction.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlConstruction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlConstruction.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlConstruction.Location = new System.Drawing.Point(253, 37);
        lblOptionsControlConstruction.Name = "lblOptionsControlConstruction";
        lblOptionsControlConstruction.Padding = new Padding(0, 7, 0, 0);
        lblOptionsControlConstruction.Size = new System.Drawing.Size(81, 20);
        lblOptionsControlConstruction.TabIndex = 63;
        lblOptionsControlConstruction.Text = "Ship Building";
        // 
        // chkOptionsControlDesigns
        // 
        chkOptionsControlDesigns.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlDesigns.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlDesigns.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlDesigns.Location = new System.Drawing.Point(6, 67);
        chkOptionsControlDesigns.Name = "chkOptionsControlDesigns";
        chkOptionsControlDesigns.Size = new System.Drawing.Size(94, 17);
        chkOptionsControlDesigns.TabIndex = 5;
        chkOptionsControlDesigns.Text = "Ship Design";
        chkOptionsControlDesigns.UseVisualStyleBackColor = false;
        // 
        // cmbOptionsControlAgentMissions
        // 
        cmbOptionsControlAgentMissions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlAgentMissions.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlAgentMissions.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlAgentMissions.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlAgentMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlAgentMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlAgentMissions.FormattingEnabled = true;
        cmbOptionsControlAgentMissions.Items.AddRange(new object[] { "Control manually", "Suggest offensive missions", "Fully automate" });
        cmbOptionsControlAgentMissions.Location = new System.Drawing.Point(340, 67);
        cmbOptionsControlAgentMissions.Name = "cmbOptionsControlAgentMissions";
        cmbOptionsControlAgentMissions.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlAgentMissions.TabIndex = 60;
        // 
        // lblOptionsControlAgentMissions
        // 
        lblOptionsControlAgentMissions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlAgentMissions.AutoSize = true;
        lblOptionsControlAgentMissions.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlAgentMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlAgentMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlAgentMissions.Location = new System.Drawing.Point(243, 64);
        lblOptionsControlAgentMissions.Name = "lblOptionsControlAgentMissions";
        lblOptionsControlAgentMissions.Padding = new Padding(0, 7, 0, 0);
        lblOptionsControlAgentMissions.Size = new System.Drawing.Size(91, 20);
        lblOptionsControlAgentMissions.TabIndex = 61;
        lblOptionsControlAgentMissions.Text = "Agent Missions";
        // 
        // cmbOptionsControlAttacks
        // 
        cmbOptionsControlAttacks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlAttacks.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlAttacks.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlAttacks.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlAttacks.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlAttacks.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlAttacks.FormattingEnabled = true;
        cmbOptionsControlAttacks.Items.AddRange(new object[] { "Control manually", "Suggest attack targets", "Fully automate" });
        cmbOptionsControlAttacks.Location = new System.Drawing.Point(340, 94);
        cmbOptionsControlAttacks.Name = "cmbOptionsControlAttacks";
        cmbOptionsControlAttacks.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlAttacks.TabIndex = 53;
        // 
        // lblOptionsControlAttacks
        // 
        lblOptionsControlAttacks.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlAttacks.AutoSize = true;
        lblOptionsControlAttacks.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlAttacks.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlAttacks.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlAttacks.Location = new System.Drawing.Point(187, 91);
        lblOptionsControlAttacks.Name = "lblOptionsControlAttacks";
        lblOptionsControlAttacks.Padding = new Padding(0, 7, 0, 0);
        lblOptionsControlAttacks.Size = new System.Drawing.Size(147, 20);
        lblOptionsControlAttacks.TabIndex = 54;
        lblOptionsControlAttacks.Text = "Attacks Against Enemies";
        // 
        // chkOptionsControlTroops
        // 
        chkOptionsControlTroops.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlTroops.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlTroops.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlTroops.Location = new System.Drawing.Point(6, 121);
        chkOptionsControlTroops.Name = "chkOptionsControlTroops";
        chkOptionsControlTroops.Size = new System.Drawing.Size(132, 17);
        chkOptionsControlTroops.TabIndex = 7;
        chkOptionsControlTroops.Text = "Troop Recruitment";
        chkOptionsControlTroops.UseVisualStyleBackColor = false;
        // 
        // chkOptionsControlFleets
        // 
        chkOptionsControlFleets.BackColor = System.Drawing.Color.Transparent;
        chkOptionsControlFleets.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsControlFleets.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsControlFleets.Location = new System.Drawing.Point(6, 148);
        chkOptionsControlFleets.Name = "chkOptionsControlFleets";
        chkOptionsControlFleets.Size = new System.Drawing.Size(114, 17);
        chkOptionsControlFleets.TabIndex = 8;
        chkOptionsControlFleets.Text = "Fleet Formation";
        chkOptionsControlFleets.UseVisualStyleBackColor = false;
        // 
        // lblOptionsControlDiplomacyGifts
        // 
        lblOptionsControlDiplomacyGifts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlDiplomacyGifts.AutoSize = true;
        lblOptionsControlDiplomacyGifts.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlDiplomacyGifts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlDiplomacyGifts.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlDiplomacyGifts.Location = new System.Drawing.Point(187, 118);
        lblOptionsControlDiplomacyGifts.Name = "lblOptionsControlDiplomacyGifts";
        lblOptionsControlDiplomacyGifts.Padding = new Padding(0, 7, 0, 0);
        lblOptionsControlDiplomacyGifts.Size = new System.Drawing.Size(147, 20);
        lblOptionsControlDiplomacyGifts.TabIndex = 56;
        lblOptionsControlDiplomacyGifts.Text = "Sending Diplomatic Gifts";
        // 
        // cmbOptionsControlDiplomacyGifts
        // 
        cmbOptionsControlDiplomacyGifts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlDiplomacyGifts.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlDiplomacyGifts.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlDiplomacyGifts.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlDiplomacyGifts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlDiplomacyGifts.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlDiplomacyGifts.FormattingEnabled = true;
        cmbOptionsControlDiplomacyGifts.Items.AddRange(new object[] { "Control manually", "Suggest gifts to empires", "Fully automate" });
        cmbOptionsControlDiplomacyGifts.Location = new System.Drawing.Point(340, 121);
        cmbOptionsControlDiplomacyGifts.Name = "cmbOptionsControlDiplomacyGifts";
        cmbOptionsControlDiplomacyGifts.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlDiplomacyGifts.TabIndex = 55;
        // 
        // cmbOptionsControlDiplomacyTreaties
        // 
        cmbOptionsControlDiplomacyTreaties.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlDiplomacyTreaties.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlDiplomacyTreaties.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlDiplomacyTreaties.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlDiplomacyTreaties.FormattingEnabled = true;
        cmbOptionsControlDiplomacyTreaties.Items.AddRange(new object[] { "Control manually", "Suggest new treaties", "Fully automate" });
        cmbOptionsControlDiplomacyTreaties.Location = new System.Drawing.Point(340, 148);
        cmbOptionsControlDiplomacyTreaties.Name = "cmbOptionsControlDiplomacyTreaties";
        cmbOptionsControlDiplomacyTreaties.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlDiplomacyTreaties.TabIndex = 58;
        // 
        // lblOptionsControlDiplomacyTreaties
        // 
        lblOptionsControlDiplomacyTreaties.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlDiplomacyTreaties.AutoSize = true;
        lblOptionsControlDiplomacyTreaties.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlDiplomacyTreaties.Location = new System.Drawing.Point(282, 145);
        lblOptionsControlDiplomacyTreaties.Name = "lblOptionsControlDiplomacyTreaties";
        lblOptionsControlDiplomacyTreaties.Padding = new Padding(0, 7, 0, 0);
        lblOptionsControlDiplomacyTreaties.Size = new System.Drawing.Size(52, 20);
        lblOptionsControlDiplomacyTreaties.TabIndex = 59;
        lblOptionsControlDiplomacyTreaties.Text = "Treaties";
        // 
        // cmbOptionsControlDiplomacyOffense
        // 
        cmbOptionsControlDiplomacyOffense.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlDiplomacyOffense.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlDiplomacyOffense.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlDiplomacyOffense.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlDiplomacyOffense.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlDiplomacyOffense.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlDiplomacyOffense.FormattingEnabled = true;
        cmbOptionsControlDiplomacyOffense.Items.AddRange(new object[] { "Control manually", "Suggest war and trade sanctions", "Fully automate" });
        cmbOptionsControlDiplomacyOffense.Location = new System.Drawing.Point(340, 175);
        cmbOptionsControlDiplomacyOffense.Name = "cmbOptionsControlDiplomacyOffense";
        cmbOptionsControlDiplomacyOffense.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlDiplomacyOffense.TabIndex = 58;
        // 
        // lblOptionsControlDiplomacyOffense
        // 
        lblOptionsControlDiplomacyOffense.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlDiplomacyOffense.AutoSize = true;
        lblOptionsControlDiplomacyOffense.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlDiplomacyOffense.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlDiplomacyOffense.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlDiplomacyOffense.Location = new System.Drawing.Point(198, 172);
        lblOptionsControlDiplomacyOffense.Name = "lblOptionsControlDiplomacyOffense";
        lblOptionsControlDiplomacyOffense.Padding = new Padding(0, 7, 0, 0);
        lblOptionsControlDiplomacyOffense.Size = new System.Drawing.Size(136, 20);
        lblOptionsControlDiplomacyOffense.TabIndex = 59;
        lblOptionsControlDiplomacyOffense.Text = "War && Trade Sanctions";
        // 
        // cmbOptionsControlColonyFacilities
        // 
        cmbOptionsControlColonyFacilities.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        cmbOptionsControlColonyFacilities.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        cmbOptionsControlColonyFacilities.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOptionsControlColonyFacilities.FlatStyle = FlatStyle.Popup;
        cmbOptionsControlColonyFacilities.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        cmbOptionsControlColonyFacilities.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        cmbOptionsControlColonyFacilities.FormattingEnabled = true;
        cmbOptionsControlColonyFacilities.Items.AddRange(new object[] { "Control manually", "Suggest new colony facilities", "Fully automate" });
        cmbOptionsControlColonyFacilities.Location = new System.Drawing.Point(340, 202);
        cmbOptionsControlColonyFacilities.Name = "cmbOptionsControlColonyFacilities";
        cmbOptionsControlColonyFacilities.Size = new System.Drawing.Size(166, 21);
        cmbOptionsControlColonyFacilities.TabIndex = 74;
        // 
        // lblOptionsControlColonyFacilities
        // 
        lblOptionsControlColonyFacilities.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblOptionsControlColonyFacilities.AutoSize = true;
        lblOptionsControlColonyFacilities.BackColor = System.Drawing.Color.Transparent;
        lblOptionsControlColonyFacilities.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblOptionsControlColonyFacilities.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        lblOptionsControlColonyFacilities.Location = new System.Drawing.Point(196, 199);
        lblOptionsControlColonyFacilities.Name = "lblOptionsControlColonyFacilities";
        lblOptionsControlColonyFacilities.Padding = new Padding(0, 7, 0, 0);
        lblOptionsControlColonyFacilities.Size = new System.Drawing.Size(138, 20);
        lblOptionsControlColonyFacilities.TabIndex = 75;
        lblOptionsControlColonyFacilities.Text = "Colony Facility Building";
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
        grpOptionsPopupMessages.Name = "grpOptionsPopupMessages";
        grpOptionsPopupMessages.Size = new System.Drawing.Size(295, 349);
        grpOptionsPopupMessages.TabIndex = 18;
        grpOptionsPopupMessages.TabStop = false;
        grpOptionsPopupMessages.Text = "Popup Messages";
        // 
        // chkOptionsPopupMessageConstructionResourceShortage
        // 
        chkOptionsPopupMessageConstructionResourceShortage.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageConstructionResourceShortage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageConstructionResourceShortage.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageConstructionResourceShortage.Location = new System.Drawing.Point(7, 325);
        chkOptionsPopupMessageConstructionResourceShortage.Name = "chkOptionsPopupMessageConstructionResourceShortage";
        chkOptionsPopupMessageConstructionResourceShortage.Size = new System.Drawing.Size(211, 17);
        chkOptionsPopupMessageConstructionResourceShortage.TabIndex = 32;
        chkOptionsPopupMessageConstructionResourceShortage.Text = "Construction Resource Shortage";
        chkOptionsPopupMessageConstructionResourceShortage.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackCivilianBases
        // 
        chkOptionsPopupMessageUnderAttackCivilianBases.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackCivilianBases.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackCivilianBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackCivilianBases.Location = new System.Drawing.Point(7, 223);
        chkOptionsPopupMessageUnderAttackCivilianBases.Name = "chkOptionsPopupMessageUnderAttackCivilianBases";
        chkOptionsPopupMessageUnderAttackCivilianBases.Size = new System.Drawing.Size(193, 17);
        chkOptionsPopupMessageUnderAttackCivilianBases.TabIndex = 31;
        chkOptionsPopupMessageUnderAttackCivilianBases.Text = "Under Attack - Civilian Bases";
        chkOptionsPopupMessageUnderAttackCivilianBases.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackMilitaryShips
        // 
        chkOptionsPopupMessageUnderAttackMilitaryShips.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackMilitaryShips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackMilitaryShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackMilitaryShips.Location = new System.Drawing.Point(7, 274);
        chkOptionsPopupMessageUnderAttackMilitaryShips.Name = "chkOptionsPopupMessageUnderAttackMilitaryShips";
        chkOptionsPopupMessageUnderAttackMilitaryShips.Size = new System.Drawing.Size(189, 17);
        chkOptionsPopupMessageUnderAttackMilitaryShips.TabIndex = 30;
        chkOptionsPopupMessageUnderAttackMilitaryShips.Text = "Under Attack - Military Ships";
        chkOptionsPopupMessageUnderAttackMilitaryShips.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackExplorationShips
        // 
        chkOptionsPopupMessageUnderAttackExplorationShips.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackExplorationShips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackExplorationShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackExplorationShips.Location = new System.Drawing.Point(7, 240);
        chkOptionsPopupMessageUnderAttackExplorationShips.Name = "chkOptionsPopupMessageUnderAttackExplorationShips";
        chkOptionsPopupMessageUnderAttackExplorationShips.Size = new System.Drawing.Size(212, 17);
        chkOptionsPopupMessageUnderAttackExplorationShips.TabIndex = 29;
        chkOptionsPopupMessageUnderAttackExplorationShips.Text = "Under Attack - Exploration Ships";
        chkOptionsPopupMessageUnderAttackExplorationShips.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackOtherStateBases
        // 
        chkOptionsPopupMessageUnderAttackOtherStateBases.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackOtherStateBases.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackOtherStateBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackOtherStateBases.Location = new System.Drawing.Point(7, 291);
        chkOptionsPopupMessageUnderAttackOtherStateBases.Name = "chkOptionsPopupMessageUnderAttackOtherStateBases";
        chkOptionsPopupMessageUnderAttackOtherStateBases.Size = new System.Drawing.Size(284, 17);
        chkOptionsPopupMessageUnderAttackOtherStateBases.TabIndex = 28;
        chkOptionsPopupMessageUnderAttackOtherStateBases.Text = "Under Attack - Research, Monitoring, Resorts";
        chkOptionsPopupMessageUnderAttackOtherStateBases.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackColonyConstructionShips
        // 
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Location = new System.Drawing.Point(7, 257);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Name = "chkOptionsPopupMessageUnderAttackColonyConstructionShips";
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Size = new System.Drawing.Size(276, 17);
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.TabIndex = 27;
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.Text = "Under Attack - Colony && Construction Ships";
        chkOptionsPopupMessageUnderAttackColonyConstructionShips.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackColoniesSpaceports
        // 
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Location = new System.Drawing.Point(7, 308);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Name = "chkOptionsPopupMessageUnderAttackColoniesSpaceports";
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Size = new System.Drawing.Size(242, 17);
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.TabIndex = 26;
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.Text = "Under Attack - Colonies && Spaceports";
        chkOptionsPopupMessageUnderAttackColoniesSpaceports.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageUnderAttackCivilianShips
        // 
        chkOptionsPopupMessageUnderAttackCivilianShips.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageUnderAttackCivilianShips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageUnderAttackCivilianShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageUnderAttackCivilianShips.Location = new System.Drawing.Point(7, 206);
        chkOptionsPopupMessageUnderAttackCivilianShips.Name = "chkOptionsPopupMessageUnderAttackCivilianShips";
        chkOptionsPopupMessageUnderAttackCivilianShips.Size = new System.Drawing.Size(190, 17);
        chkOptionsPopupMessageUnderAttackCivilianShips.TabIndex = 25;
        chkOptionsPopupMessageUnderAttackCivilianShips.Text = "Under Attack - Civilian Ships";
        chkOptionsPopupMessageUnderAttackCivilianShips.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageShipNeedsRefuelling
        // 
        chkOptionsPopupMessageShipNeedsRefuelling.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageShipNeedsRefuelling.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageShipNeedsRefuelling.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageShipNeedsRefuelling.Location = new System.Drawing.Point(7, 189);
        chkOptionsPopupMessageShipNeedsRefuelling.Name = "chkOptionsPopupMessageShipNeedsRefuelling";
        chkOptionsPopupMessageShipNeedsRefuelling.Size = new System.Drawing.Size(207, 17);
        chkOptionsPopupMessageShipNeedsRefuelling.TabIndex = 20;
        chkOptionsPopupMessageShipNeedsRefuelling.Text = "Ship Needs Refuelling or Repair";
        chkOptionsPopupMessageShipNeedsRefuelling.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageShipMissionComplete
        // 
        chkOptionsPopupMessageShipMissionComplete.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageShipMissionComplete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageShipMissionComplete.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageShipMissionComplete.Location = new System.Drawing.Point(7, 172);
        chkOptionsPopupMessageShipMissionComplete.Name = "chkOptionsPopupMessageShipMissionComplete";
        chkOptionsPopupMessageShipMissionComplete.Size = new System.Drawing.Size(155, 17);
        chkOptionsPopupMessageShipMissionComplete.TabIndex = 19;
        chkOptionsPopupMessageShipMissionComplete.Text = "Ship Mission Complete";
        chkOptionsPopupMessageShipMissionComplete.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageExploration
        // 
        chkOptionsPopupMessageExploration.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageExploration.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageExploration.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageExploration.Location = new System.Drawing.Point(7, 155);
        chkOptionsPopupMessageExploration.Name = "chkOptionsPopupMessageExploration";
        chkOptionsPopupMessageExploration.Size = new System.Drawing.Size(158, 17);
        chkOptionsPopupMessageExploration.TabIndex = 16;
        chkOptionsPopupMessageExploration.Text = "Exploration discoveries";
        chkOptionsPopupMessageExploration.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageIntelligenceMissions
        // 
        chkOptionsPopupMessageIntelligenceMissions.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageIntelligenceMissions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageIntelligenceMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageIntelligenceMissions.Location = new System.Drawing.Point(7, 138);
        chkOptionsPopupMessageIntelligenceMissions.Name = "chkOptionsPopupMessageIntelligenceMissions";
        chkOptionsPopupMessageIntelligenceMissions.Size = new System.Drawing.Size(143, 17);
        chkOptionsPopupMessageIntelligenceMissions.TabIndex = 15;
        chkOptionsPopupMessageIntelligenceMissions.Text = "Intelligence Missions";
        chkOptionsPopupMessageIntelligenceMissions.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageResearchBreakthrough
        // 
        chkOptionsPopupMessageResearchBreakthrough.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageResearchBreakthrough.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageResearchBreakthrough.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageResearchBreakthrough.Location = new System.Drawing.Point(7, 121);
        chkOptionsPopupMessageResearchBreakthrough.Name = "chkOptionsPopupMessageResearchBreakthrough";
        chkOptionsPopupMessageResearchBreakthrough.Size = new System.Drawing.Size(160, 17);
        chkOptionsPopupMessageResearchBreakthrough.TabIndex = 7;
        chkOptionsPopupMessageResearchBreakthrough.Text = "Research breakthrough";
        chkOptionsPopupMessageResearchBreakthrough.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageEmpireMetDestroyed
        // 
        chkOptionsPopupMessageEmpireMetDestroyed.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageEmpireMetDestroyed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageEmpireMetDestroyed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageEmpireMetDestroyed.Location = new System.Drawing.Point(7, 104);
        chkOptionsPopupMessageEmpireMetDestroyed.Name = "chkOptionsPopupMessageEmpireMetDestroyed";
        chkOptionsPopupMessageEmpireMetDestroyed.Size = new System.Drawing.Size(127, 17);
        chkOptionsPopupMessageEmpireMetDestroyed.TabIndex = 8;
        chkOptionsPopupMessageEmpireMetDestroyed.Text = "Empire Discovery";
        chkOptionsPopupMessageEmpireMetDestroyed.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageColonyGainLoss
        // 
        chkOptionsPopupMessageColonyGainLoss.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageColonyGainLoss.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageColonyGainLoss.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageColonyGainLoss.Location = new System.Drawing.Point(7, 87);
        chkOptionsPopupMessageColonyGainLoss.Name = "chkOptionsPopupMessageColonyGainLoss";
        chkOptionsPopupMessageColonyGainLoss.Size = new System.Drawing.Size(141, 17);
        chkOptionsPopupMessageColonyGainLoss.TabIndex = 13;
        chkOptionsPopupMessageColonyGainLoss.Text = "Colony Gain or Loss";
        chkOptionsPopupMessageColonyGainLoss.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageDiplomacyWarTradeSanctions
        // 
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Location = new System.Drawing.Point(7, 70);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Name = "chkOptionsPopupMessageDiplomacyWarTradeSanctions";
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Size = new System.Drawing.Size(157, 17);
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.TabIndex = 10;
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.Text = "War && Trade Sanctions";
        chkOptionsPopupMessageDiplomacyWarTradeSanctions.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageDiplomacyTreaties
        // 
        chkOptionsPopupMessageDiplomacyTreaties.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageDiplomacyTreaties.Location = new System.Drawing.Point(7, 53);
        chkOptionsPopupMessageDiplomacyTreaties.Name = "chkOptionsPopupMessageDiplomacyTreaties";
        chkOptionsPopupMessageDiplomacyTreaties.Size = new System.Drawing.Size(100, 17);
        chkOptionsPopupMessageDiplomacyTreaties.TabIndex = 11;
        chkOptionsPopupMessageDiplomacyTreaties.Text = "Treaty offers";
        chkOptionsPopupMessageDiplomacyTreaties.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageRequestWarning
        // 
        chkOptionsPopupMessageRequestWarning.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageRequestWarning.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageRequestWarning.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageRequestWarning.Location = new System.Drawing.Point(7, 36);
        chkOptionsPopupMessageRequestWarning.Name = "chkOptionsPopupMessageRequestWarning";
        chkOptionsPopupMessageRequestWarning.Size = new System.Drawing.Size(181, 17);
        chkOptionsPopupMessageRequestWarning.TabIndex = 14;
        chkOptionsPopupMessageRequestWarning.Text = "Requests, Warnings && Gifts";
        chkOptionsPopupMessageRequestWarning.UseVisualStyleBackColor = false;
        // 
        // chkOptionsPopupMessageShipBuilt
        // 
        chkOptionsPopupMessageShipBuilt.BackColor = System.Drawing.Color.Transparent;
        chkOptionsPopupMessageShipBuilt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        chkOptionsPopupMessageShipBuilt.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
        chkOptionsPopupMessageShipBuilt.Location = new System.Drawing.Point(7, 19);
        chkOptionsPopupMessageShipBuilt.Name = "chkOptionsPopupMessageShipBuilt";
        chkOptionsPopupMessageShipBuilt.Size = new System.Drawing.Size(108, 17);
        chkOptionsPopupMessageShipBuilt.TabIndex = 5;
        chkOptionsPopupMessageShipBuilt.Text = "New Ship Built";
        chkOptionsPopupMessageShipBuilt.UseVisualStyleBackColor = false;
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.ColumnCount = 2;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel4.Controls.Add(grpOptionsAutoSave, 0, 0);
        tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 1, 0);
        tableLayoutPanel4.Location = new System.Drawing.Point(12, 218);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 1;
        tableLayoutPanel4.RowStyles.Add(new RowStyle());
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel4.Size = new System.Drawing.Size(515, 59);
        tableLayoutPanel4.TabIndex = 77;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.ColumnCount = 2;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel5.Controls.Add(lblOptionsMouseScrollMode, 0, 1);
        tableLayoutPanel5.Controls.Add(cmbOptionsMouseScrollWheelBehaviour, 1, 1);
        tableLayoutPanel5.Controls.Add(chkOptionsLoadedGamesPaused, 1, 0);
        tableLayoutPanel5.Dock = DockStyle.Fill;
        tableLayoutPanel5.Location = new System.Drawing.Point(169, 3);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.RowCount = 2;
        tableLayoutPanel5.RowStyles.Add(new RowStyle());
        tableLayoutPanel5.RowStyles.Add(new RowStyle());
        tableLayoutPanel5.Size = new System.Drawing.Size(343, 100);
        tableLayoutPanel5.TabIndex = 75;
        // 
        // GameOptionsScreenPanelTemp
        // 
        BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
        BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
        Controls.Add(tableLayoutPanel4);
        Controls.Add(btnGameOptionsShowMessages);
        Controls.Add(grpOptionsDisplaySettings);
        Controls.Add(chkOptionsAutoPauseInPopup);
        Controls.Add(grpOptionsVolume);
        Controls.Add(grpOptionsControl);
        HeaderTitle = "Options";
        Location = new System.Drawing.Point(227, 32);
        Name = "GameOptionsScreenPanelTemp";
        Size = new System.Drawing.Size(541, 653);
        grpOptionsAutoSave.ResumeLayout(false);
        ((ISupportInitialize)numOptionsAutoSaveMinutes).EndInit();
        grpOptionsDisplaySettings.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        grpOptionsVolume.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        grpOptionsControl.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        pnlOptionsAutomationMode.ResumeLayout(false);
        pnlOptionsAutomationMode.PerformLayout();
        grpOptionsPopupMessages.ResumeLayout(false);
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel5.PerformLayout();
        ResumeLayout(false);
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


        internal System.Windows.Forms.Panel pnlOptionsAutomationMode;


        internal CheckBox chkOptionsControlPopulationPolicy;

        internal CheckBox chkOptionsControlCharacterLocations;

        internal Label lblOptionsControlColonyFacilities;

        internal ComboBox cmbOptionsControlColonyFacilities;

        internal CheckBox chkOptionsControlResearch;

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
    private TableLayoutPanel tableLayoutPanel1;
    internal GlassButton btnGameOptionsResetAutomationMessages;
    internal ComboBox cmbOptionsControlOfferPirateMissions;
    internal Label lblOptionsControlOfferPirateMissions;
    internal GlassButton btnGameOptionsEmpireSettings;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
    private TableLayoutPanel tableLayoutPanel4;
    private TableLayoutPanel tableLayoutPanel5;
}