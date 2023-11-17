using System.ComponentModel;
using System.Windows.Forms;

namespace DistantWorlds.Controls; 

partial class GameOptionsMessagesScreenPanel {

  /// <summary> 
  /// Required designer variable.
  /// </summary>
  private IContainer components = null;

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
  private void InitializeComponent() {
    components = new System.ComponentModel.Container();
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DistantWorlds.Controls.GameOptionsMessagesScreenPanel));

    this.grpOptionsScrollingMessages = new System.Windows.Forms.GroupBox();
    this.chkOptionsScrollingMessageExploration = new CheckBox();
    this.chkOptionsScrollingMessageIntelligenceMissions = new CheckBox();
    this.chkOptionsScrollingMessageResearchBreakthrough = new CheckBox();
    this.chkOptionsScrollingMessageEmpireMetDestroyed = new CheckBox();
    this.chkOptionsScrollingMessageColonyGainLoss = new CheckBox();
    this.chkOptionsScrollingMessageUnderAttackCivilianShips = new CheckBox();
    this.chkOptionsScrollingMessageWarTradeSanctions = new CheckBox();
    this.chkOptionsScrollingMessageDiplomacyTreaties = new CheckBox();
    this.chkOptionsScrollingMessageRequestWarning = new CheckBox();
    this.chkOptionsScrollingMessageNewShipBuilt = new CheckBox();
    this.chkOptionsScrollingMessageConstructionResourceShortage = new CheckBox();
    this.chkOptionsScrollingMessageUnderAttackCivilianBases = new CheckBox();
    this.chkOptionsScrollingMessageUnderAttackMilitaryShips = new CheckBox();
    this.chkOptionsScrollingMessageUnderAttackExplorationShips = new CheckBox();
    this.chkOptionsScrollingMessageUnderAttackOtherStateBases = new CheckBox();
    this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips = new CheckBox();
    this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports = new CheckBox();
    this.chkOptionsScrollingMessageShipNeedsRefuelling = new CheckBox();
    this.chkOptionsScrollingMessageShipMissionComplete = new CheckBox();

    this.SuspendLayout();
    this.grpOptionsScrollingMessages.SuspendLayout();

    this.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
    this.BackgroundImage = (System.Drawing.Image)resources.GetObject("GameOptionsMessages.BackgroundImage");
    this.BorderColor1 = System.Drawing.Color.FromArgb(96, 200, 200, 200);
    this.BorderColor2 = System.Drawing.Color.FromArgb(96, 140, 140, 140);
    this.BorderColor3 = System.Drawing.Color.FromArgb(96, 20, 20, 20);
    this.BorderColor4 = System.Drawing.Color.FromArgb(96, 80, 80, 80);
    this.BorderSize = 3;
    this.Controls.Add(this.grpOptionsPopupMessages);
    this.Controls.Add(this.grpOptionsScrollingMessages);
    this.HeaderIcon = null;
    this.HeaderTitle = "Message Settings";
    this.Location = new System.Drawing.Point(100, 100);
    this.Name = "this";
    this.Size = new System.Drawing.Size(625, 400);
    this.TabIndex = 131;
    this.Visible = false;

                this.grpOptionsScrollingMessages.BackColor = System.Drawing.Color.Transparent;
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageConstructionResourceShortage);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageUnderAttackCivilianBases);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageUnderAttackMilitaryShips);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageUnderAttackExplorationShips);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageUnderAttackOtherStateBases);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageShipNeedsRefuelling);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageShipMissionComplete);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageExploration);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageIntelligenceMissions);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageResearchBreakthrough);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageEmpireMetDestroyed);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageColonyGainLoss);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageUnderAttackCivilianShips);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageWarTradeSanctions);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageDiplomacyTreaties);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageRequestWarning);
            this.grpOptionsScrollingMessages.Controls.Add(this.chkOptionsScrollingMessageNewShipBuilt);
            this.grpOptionsScrollingMessages.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.grpOptionsScrollingMessages.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.grpOptionsScrollingMessages.Location = new System.Drawing.Point(12, 14);
            this.grpOptionsScrollingMessages.Name = "grpOptionsScrollingMessages";
            this.grpOptionsScrollingMessages.Size = new System.Drawing.Size(295, 349);
            this.grpOptionsScrollingMessages.TabIndex = 19;
            this.grpOptionsScrollingMessages.TabStop = false;
            this.grpOptionsScrollingMessages.Text = "Scrolling Messages";

            this.chkOptionsScrollingMessageConstructionResourceShortage.AutoSize = true;
            this.chkOptionsScrollingMessageConstructionResourceShortage.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageConstructionResourceShortage.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageConstructionResourceShortage.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageConstructionResourceShortage.Location = new System.Drawing.Point(7, 325);
            this.chkOptionsScrollingMessageConstructionResourceShortage.Name = "chkOptionsScrollingMessageConstructionResourceShortage";
            this.chkOptionsScrollingMessageConstructionResourceShortage.Size = new System.Drawing.Size(211, 17);
            this.chkOptionsScrollingMessageConstructionResourceShortage.TabIndex = 33;
            this.chkOptionsScrollingMessageConstructionResourceShortage.Text = "Construction Resource Shortage";
            this.chkOptionsScrollingMessageConstructionResourceShortage.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.AutoSize = true;
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.Location = new System.Drawing.Point(7, 223);
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.Name = "chkOptionsScrollingMessageUnderAttackCivilianBases";
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.Size = new System.Drawing.Size(193, 17);
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.TabIndex = 24;
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.Text = "Under Attack - Civilian Bases";
            this.chkOptionsScrollingMessageUnderAttackCivilianBases.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.AutoSize = true;
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.Location = new System.Drawing.Point(7, 274);
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.Name = "chkOptionsScrollingMessageUnderAttackMilitaryShips";
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.Size = new System.Drawing.Size(189, 17);
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.TabIndex = 23;
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.Text = "Under Attack - Military Ships";
            this.chkOptionsScrollingMessageUnderAttackMilitaryShips.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.AutoSize = true;
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.Location = new System.Drawing.Point(7, 240);
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.Name = "chkOptionsScrollingMessageUnderAttackExplorationShips";
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.Size = new System.Drawing.Size(212, 17);
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.TabIndex = 22;
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.Text = "Under Attack - Exploration Ships";
            this.chkOptionsScrollingMessageUnderAttackExplorationShips.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.AutoSize = true;
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.Location = new System.Drawing.Point(7, 291);
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.Name = "chkOptionsScrollingMessageUnderAttackOtherStateBases";
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.Size = new System.Drawing.Size(284, 17);
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.TabIndex = 21;
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.Text = "Under Attack - Research, Monitoring, Resorts";
            this.chkOptionsScrollingMessageUnderAttackOtherStateBases.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.AutoSize = true;
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Location = new System.Drawing.Point(7, 257);
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Name = "chkOptionsScrollingMessageUnderAttackColonyConstructionShips";
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Size = new System.Drawing.Size(276, 17);
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.TabIndex = 20;
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Text = "Under Attack - Colony && Construction Ships";
            this.chkOptionsScrollingMessageUnderAttackColonyConstructionShips.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.AutoSize = true;
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Location = new System.Drawing.Point(7, 308);
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Name = "chkOptionsScrollingMessageUnderAttackColoniesSpaceports";
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Size = new System.Drawing.Size(242, 17);
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.TabIndex = 19;
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Text = "Under Attack - Colonies && Spaceports";
            this.chkOptionsScrollingMessageUnderAttackColoniesSpaceports.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageShipNeedsRefuelling.AutoSize = true;
            this.chkOptionsScrollingMessageShipNeedsRefuelling.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageShipNeedsRefuelling.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageShipNeedsRefuelling.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageShipNeedsRefuelling.Location = new System.Drawing.Point(7, 189);
            this.chkOptionsScrollingMessageShipNeedsRefuelling.Name = "chkOptionsScrollingMessageShipNeedsRefuelling";
            this.chkOptionsScrollingMessageShipNeedsRefuelling.Size = new System.Drawing.Size(207, 17);
            this.chkOptionsScrollingMessageShipNeedsRefuelling.TabIndex = 18;
            this.chkOptionsScrollingMessageShipNeedsRefuelling.Text = "Ship Needs Refuelling or Repair";
            this.chkOptionsScrollingMessageShipNeedsRefuelling.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageShipMissionComplete.AutoSize = true;
            this.chkOptionsScrollingMessageShipMissionComplete.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageShipMissionComplete.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageShipMissionComplete.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageShipMissionComplete.Location = new System.Drawing.Point(7, 172);
            this.chkOptionsScrollingMessageShipMissionComplete.Name = "chkOptionsScrollingMessageShipMissionComplete";
            this.chkOptionsScrollingMessageShipMissionComplete.Size = new System.Drawing.Size(155, 17);
            this.chkOptionsScrollingMessageShipMissionComplete.TabIndex = 17;
            this.chkOptionsScrollingMessageShipMissionComplete.Text = "Ship Mission Complete";
            this.chkOptionsScrollingMessageShipMissionComplete.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageExploration.AutoSize = true;
            this.chkOptionsScrollingMessageExploration.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageExploration.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageExploration.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageExploration.Location = new System.Drawing.Point(7, 155);
            this.chkOptionsScrollingMessageExploration.Name = "chkOptionsScrollingMessageExploration";
            this.chkOptionsScrollingMessageExploration.Size = new System.Drawing.Size(158, 17);
            this.chkOptionsScrollingMessageExploration.TabIndex = 16;
            this.chkOptionsScrollingMessageExploration.Text = "Exploration discoveries";
            this.chkOptionsScrollingMessageExploration.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageIntelligenceMissions.AutoSize = true;
            this.chkOptionsScrollingMessageIntelligenceMissions.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageIntelligenceMissions.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageIntelligenceMissions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageIntelligenceMissions.Location = new System.Drawing.Point(7, 138);
            this.chkOptionsScrollingMessageIntelligenceMissions.Name = "chkOptionsScrollingMessageIntelligenceMissions";
            this.chkOptionsScrollingMessageIntelligenceMissions.Size = new System.Drawing.Size(143, 17);
            this.chkOptionsScrollingMessageIntelligenceMissions.TabIndex = 15;
            this.chkOptionsScrollingMessageIntelligenceMissions.Text = "Intelligence Missions";
            this.chkOptionsScrollingMessageIntelligenceMissions.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageResearchBreakthrough.AutoSize = true;
            this.chkOptionsScrollingMessageResearchBreakthrough.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageResearchBreakthrough.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageResearchBreakthrough.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageResearchBreakthrough.Location = new System.Drawing.Point(7, 121);
            this.chkOptionsScrollingMessageResearchBreakthrough.Name = "chkOptionsScrollingMessageResearchBreakthrough";
            this.chkOptionsScrollingMessageResearchBreakthrough.Size = new System.Drawing.Size(160, 17);
            this.chkOptionsScrollingMessageResearchBreakthrough.TabIndex = 7;
            this.chkOptionsScrollingMessageResearchBreakthrough.Text = "Research breakthrough";
            this.chkOptionsScrollingMessageResearchBreakthrough.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageEmpireMetDestroyed.AutoSize = true;
            this.chkOptionsScrollingMessageEmpireMetDestroyed.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageEmpireMetDestroyed.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageEmpireMetDestroyed.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageEmpireMetDestroyed.Location = new System.Drawing.Point(7, 104);
            this.chkOptionsScrollingMessageEmpireMetDestroyed.Name = "chkOptionsScrollingMessageEmpireMetDestroyed";
            this.chkOptionsScrollingMessageEmpireMetDestroyed.Size = new System.Drawing.Size(127, 17);
            this.chkOptionsScrollingMessageEmpireMetDestroyed.TabIndex = 8;
            this.chkOptionsScrollingMessageEmpireMetDestroyed.Text = "Empire Discovery";
            this.chkOptionsScrollingMessageEmpireMetDestroyed.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageColonyGainLoss.AutoSize = true;
            this.chkOptionsScrollingMessageColonyGainLoss.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageColonyGainLoss.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageColonyGainLoss.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageColonyGainLoss.Location = new System.Drawing.Point(7, 87);
            this.chkOptionsScrollingMessageColonyGainLoss.Name = "chkOptionsScrollingMessageColonyGainLoss";
            this.chkOptionsScrollingMessageColonyGainLoss.Size = new System.Drawing.Size(141, 17);
            this.chkOptionsScrollingMessageColonyGainLoss.TabIndex = 13;
            this.chkOptionsScrollingMessageColonyGainLoss.Text = "Colony Gain or Loss";
            this.chkOptionsScrollingMessageColonyGainLoss.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.AutoSize = true;
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.Location = new System.Drawing.Point(7, 206);
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.Name = "chkOptionsScrollingMessageUnderAttackCivilianShips";
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.Size = new System.Drawing.Size(190, 17);
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.TabIndex = 9;
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.Text = "Under Attack - Civilian Ships";
            this.chkOptionsScrollingMessageUnderAttackCivilianShips.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageWarTradeSanctions.AutoSize = true;
            this.chkOptionsScrollingMessageWarTradeSanctions.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageWarTradeSanctions.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageWarTradeSanctions.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageWarTradeSanctions.Location = new System.Drawing.Point(7, 70);
            this.chkOptionsScrollingMessageWarTradeSanctions.Name = "chkOptionsScrollingMessageWarTradeSanctions";
            this.chkOptionsScrollingMessageWarTradeSanctions.Size = new System.Drawing.Size(157, 17);
            this.chkOptionsScrollingMessageWarTradeSanctions.TabIndex = 10;
            this.chkOptionsScrollingMessageWarTradeSanctions.Text = "War && Trade Sanctions";
            this.chkOptionsScrollingMessageWarTradeSanctions.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageDiplomacyTreaties.AutoSize = true;
            this.chkOptionsScrollingMessageDiplomacyTreaties.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageDiplomacyTreaties.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageDiplomacyTreaties.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageDiplomacyTreaties.Location = new System.Drawing.Point(7, 53);
            this.chkOptionsScrollingMessageDiplomacyTreaties.Name = "chkOptionsScrollingMessageDiplomacyTreaties";
            this.chkOptionsScrollingMessageDiplomacyTreaties.Size = new System.Drawing.Size(100, 17);
            this.chkOptionsScrollingMessageDiplomacyTreaties.TabIndex = 11;
            this.chkOptionsScrollingMessageDiplomacyTreaties.Text = "Treaty offers";
            this.chkOptionsScrollingMessageDiplomacyTreaties.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageRequestWarning.AutoSize = true;
            this.chkOptionsScrollingMessageRequestWarning.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageRequestWarning.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageRequestWarning.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageRequestWarning.Location = new System.Drawing.Point(7, 36);
            this.chkOptionsScrollingMessageRequestWarning.Name = "chkOptionsScrollingMessageRequestWarning";
            this.chkOptionsScrollingMessageRequestWarning.Size = new System.Drawing.Size(181, 17);
            this.chkOptionsScrollingMessageRequestWarning.TabIndex = 14;
            this.chkOptionsScrollingMessageRequestWarning.Text = "Requests, Warnings && Gifts";
            this.chkOptionsScrollingMessageRequestWarning.UseVisualStyleBackColor = false;
            this.chkOptionsScrollingMessageNewShipBuilt.AutoSize = true;
            this.chkOptionsScrollingMessageNewShipBuilt.BackColor = System.Drawing.Color.Transparent;
            this.chkOptionsScrollingMessageNewShipBuilt.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.chkOptionsScrollingMessageNewShipBuilt.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.chkOptionsScrollingMessageNewShipBuilt.Location = new System.Drawing.Point(7, 19);
            this.chkOptionsScrollingMessageNewShipBuilt.Name = "chkOptionsScrollingMessageNewShipBuilt";
            this.chkOptionsScrollingMessageNewShipBuilt.Size = new System.Drawing.Size(108, 17);
            this.chkOptionsScrollingMessageNewShipBuilt.TabIndex = 5;
            this.chkOptionsScrollingMessageNewShipBuilt.Text = "New Ship Built";
            this.chkOptionsScrollingMessageNewShipBuilt.UseVisualStyleBackColor = false;
            
            // resume layout
            
            this.grpOptionsScrollingMessages.ResumeLayout(false);
            this.grpOptionsScrollingMessages.PerformLayout();
  }

  #endregion

  internal GroupBox grpOptionsPopupMessages;
  internal GroupBox grpOptionsScrollingMessages;

  internal CheckBox chkOptionsScrollingMessageConstructionResourceShortage;

  internal CheckBox chkOptionsScrollingMessageUnderAttackCivilianBases;

  internal CheckBox chkOptionsScrollingMessageUnderAttackMilitaryShips;

  internal CheckBox chkOptionsScrollingMessageUnderAttackExplorationShips;

  internal CheckBox chkOptionsScrollingMessageUnderAttackOtherStateBases;

  internal CheckBox chkOptionsScrollingMessageUnderAttackColonyConstructionShips;

  internal CheckBox chkOptionsScrollingMessageUnderAttackColoniesSpaceports;

  internal CheckBox chkOptionsScrollingMessageShipNeedsRefuelling;

  internal CheckBox chkOptionsScrollingMessageShipMissionComplete;

  internal CheckBox chkOptionsScrollingMessageExploration;

  internal CheckBox chkOptionsScrollingMessageIntelligenceMissions;

  internal CheckBox chkOptionsScrollingMessageRequestWarning;

  internal CheckBox chkOptionsScrollingMessageColonyGainLoss;

  internal CheckBox chkOptionsScrollingMessageDiplomacyTreaties;

  internal CheckBox chkOptionsScrollingMessageWarTradeSanctions;

  internal CheckBox chkOptionsScrollingMessageUnderAttackCivilianShips;

  internal CheckBox chkOptionsScrollingMessageEmpireMetDestroyed;

  internal CheckBox chkOptionsScrollingMessageResearchBreakthrough;

  internal CheckBox chkOptionsScrollingMessageNewShipBuilt;


}