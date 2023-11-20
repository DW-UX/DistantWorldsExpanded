using DistantWorlds.Types;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DistantWorlds.Controls
{
    partial class StartNewGameSelectGameTypeScreenPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartNewGameSelectGameTypeScreenPanel));
            pnlStartNewGameIntroductoryBorder = new RoundRectanglePanel();
            btnStartNewGameIntroductory = new GlassButton();
            lblStartNewGameActiveTheme = new SmoothLabel();
            picStartNewGameYourEmpireTypeTimeline = new PictureBox();
            btnStartNewGameYourEmpireTypeLegends = new GlassButton();
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi = new GlassButton();
            btnStartNewGameYourEmpireTypeClassicEra = new GlassButton();
            btnStartNewGameYourEmpireTypeTheAncientGalaxy = new GlassButton();
            btnStartNewGameYourEmpireTypeNormalClassic = new GlassButton();
            btnStartNewGameYourEmpireTypePirateClassic = new GlassButton();
            btnStartNewGameYourEmpireTypePirateShadows = new GlassButton();
            btnStartNewGameYourEmpireTypeNormalShadows = new GlassButton();
            lblHelpTitle = new SmoothLabel();
            lblHelpDescription = new SmoothLabel();
            toolTip = new System.Windows.Forms.ToolTip(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            pnlStartNewGameIntroductoryBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStartNewGameYourEmpireTypeTimeline).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // pnlStartNewGameIntroductoryBorder
            // 
            tableLayoutPanel1.SetColumnSpan(pnlStartNewGameIntroductoryBorder, 2);
            pnlStartNewGameIntroductoryBorder.Controls.Add(btnStartNewGameIntroductory);
            pnlStartNewGameIntroductoryBorder.Dock = DockStyle.Fill;
            pnlStartNewGameIntroductoryBorder.ForeColor = System.Drawing.Color.Green;
            pnlStartNewGameIntroductoryBorder.Location = new System.Drawing.Point(3, 17);
            pnlStartNewGameIntroductoryBorder.Name = "pnlStartNewGameIntroductoryBorder";
            pnlStartNewGameIntroductoryBorder.Padding = new Padding(5);
            pnlStartNewGameIntroductoryBorder.Size = new System.Drawing.Size(888, 84);
            pnlStartNewGameIntroductoryBorder.TabIndex = 279;
            // 
            // btnStartNewGameIntroductory
            // 
            btnStartNewGameIntroductory.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameIntroductory.ClipBackground = false;
            btnStartNewGameIntroductory.DelayFrameRefresh = false;
            btnStartNewGameIntroductory.Dock = DockStyle.Fill;
            btnStartNewGameIntroductory.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameIntroductory.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameIntroductory.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameIntroductory.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameIntroductory.IntensifyColors = false;
            btnStartNewGameIntroductory.Location = new System.Drawing.Point(5, 5);
            btnStartNewGameIntroductory.Name = "btnStartNewGameIntroductory";
            btnStartNewGameIntroductory.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameIntroductory.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameIntroductory.Size = new System.Drawing.Size(878, 74);
            btnStartNewGameIntroductory.TabIndex = 278;
            btnStartNewGameIntroductory.TabStop = false;
            btnStartNewGameIntroductory.Text = "Introductory Game >>";
            btnStartNewGameIntroductory.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameIntroductory.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameIntroductory.ToggledOn = false;
            // 
            // lblStartNewGameActiveTheme
            // 
            lblStartNewGameActiveTheme.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblStartNewGameActiveTheme.AutoSize = true;
            lblStartNewGameActiveTheme.BackColor = System.Drawing.Color.Transparent;
            lblStartNewGameActiveTheme.ForeColor = System.Drawing.Color.FromArgb(255, 192, 0);
            lblStartNewGameActiveTheme.Location = new System.Drawing.Point(3, 620);
            lblStartNewGameActiveTheme.Name = "lblStartNewGameActiveTheme";
            lblStartNewGameActiveTheme.Size = new System.Drawing.Size(894, 15);
            lblStartNewGameActiveTheme.TabIndex = 277;
            lblStartNewGameActiveTheme.Text = "Active Theme";
            lblStartNewGameActiveTheme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picStartNewGameYourEmpireTypeTimeline
            // 
            picStartNewGameYourEmpireTypeTimeline.Dock = DockStyle.Fill;
            picStartNewGameYourEmpireTypeTimeline.Image = (System.Drawing.Image)resources.GetObject("picStartNewGameYourEmpireTypeTimeline.Image");
            picStartNewGameYourEmpireTypeTimeline.Location = new System.Drawing.Point(3, 395);
            picStartNewGameYourEmpireTypeTimeline.Name = "picStartNewGameYourEmpireTypeTimeline";
            picStartNewGameYourEmpireTypeTimeline.Size = new System.Drawing.Size(894, 136);
            picStartNewGameYourEmpireTypeTimeline.SizeMode = PictureBoxSizeMode.StretchImage;
            picStartNewGameYourEmpireTypeTimeline.TabIndex = 276;
            picStartNewGameYourEmpireTypeTimeline.TabStop = false;
            // 
            // btnStartNewGameYourEmpireTypeLegends
            // 
            btnStartNewGameYourEmpireTypeLegends.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourEmpireTypeLegends.ClipBackground = false;
            btnStartNewGameYourEmpireTypeLegends.DelayFrameRefresh = false;
            btnStartNewGameYourEmpireTypeLegends.Dock = DockStyle.Fill;
            btnStartNewGameYourEmpireTypeLegends.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourEmpireTypeLegends.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourEmpireTypeLegends.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourEmpireTypeLegends.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourEmpireTypeLegends.IntensifyColors = false;
            btnStartNewGameYourEmpireTypeLegends.Location = new System.Drawing.Point(733, 3);
            btnStartNewGameYourEmpireTypeLegends.Name = "btnStartNewGameYourEmpireTypeLegends";
            btnStartNewGameYourEmpireTypeLegends.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourEmpireTypeLegends.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourEmpireTypeLegends.Size = new System.Drawing.Size(158, 270);
            btnStartNewGameYourEmpireTypeLegends.TabIndex = 275;
            btnStartNewGameYourEmpireTypeLegends.TabStop = false;
            btnStartNewGameYourEmpireTypeLegends.Text = "Legends >>";
            btnStartNewGameYourEmpireTypeLegends.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourEmpireTypeLegends.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourEmpireTypeLegends.ToggledOn = false;
            // 
            // btnStartNewGameYourEmpireTypeReturnOfTheShakturi
            // 
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.ClipBackground = false;
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.DelayFrameRefresh = false;
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Dock = DockStyle.Fill;
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.IntensifyColors = false;
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Location = new System.Drawing.Point(587, 3);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Name = "btnStartNewGameYourEmpireTypeReturnOfTheShakturi";
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Size = new System.Drawing.Size(140, 270);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.TabIndex = 274;
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.TabStop = false;
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Text = "Return of the Shakturi >>";
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.ToggledOn = false;
            // 
            // btnStartNewGameYourEmpireTypeClassicEra
            // 
            btnStartNewGameYourEmpireTypeClassicEra.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourEmpireTypeClassicEra.ClipBackground = false;
            btnStartNewGameYourEmpireTypeClassicEra.DelayFrameRefresh = false;
            btnStartNewGameYourEmpireTypeClassicEra.Dock = DockStyle.Fill;
            btnStartNewGameYourEmpireTypeClassicEra.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourEmpireTypeClassicEra.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourEmpireTypeClassicEra.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourEmpireTypeClassicEra.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourEmpireTypeClassicEra.IntensifyColors = false;
            btnStartNewGameYourEmpireTypeClassicEra.Location = new System.Drawing.Point(441, 3);
            btnStartNewGameYourEmpireTypeClassicEra.Name = "btnStartNewGameYourEmpireTypeClassicEra";
            btnStartNewGameYourEmpireTypeClassicEra.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourEmpireTypeClassicEra.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourEmpireTypeClassicEra.Size = new System.Drawing.Size(140, 270);
            btnStartNewGameYourEmpireTypeClassicEra.TabIndex = 272;
            btnStartNewGameYourEmpireTypeClassicEra.TabStop = false;
            btnStartNewGameYourEmpireTypeClassicEra.Text = "Classic Era >>";
            btnStartNewGameYourEmpireTypeClassicEra.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourEmpireTypeClassicEra.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourEmpireTypeClassicEra.ToggledOn = false;
            // 
            // btnStartNewGameYourEmpireTypeTheAncientGalaxy
            // 
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.ClipBackground = false;
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.DelayFrameRefresh = false;
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Dock = DockStyle.Fill;
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.IntensifyColors = false;
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Location = new System.Drawing.Point(3, 3);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Name = "btnStartNewGameYourEmpireTypeTheAncientGalaxy";
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Size = new System.Drawing.Size(140, 270);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.TabIndex = 271;
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.TabStop = false;
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Text = "The Ancient Galaxy >>";
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.ToggledOn = false;
            // 
            // btnStartNewGameYourEmpireTypeNormalClassic
            // 
            btnStartNewGameYourEmpireTypeNormalClassic.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourEmpireTypeNormalClassic.ClipBackground = false;
            btnStartNewGameYourEmpireTypeNormalClassic.DelayFrameRefresh = false;
            btnStartNewGameYourEmpireTypeNormalClassic.Dock = DockStyle.Fill;
            btnStartNewGameYourEmpireTypeNormalClassic.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourEmpireTypeNormalClassic.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourEmpireTypeNormalClassic.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourEmpireTypeNormalClassic.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourEmpireTypeNormalClassic.IntensifyColors = false;
            btnStartNewGameYourEmpireTypeNormalClassic.Location = new System.Drawing.Point(3, 3);
            btnStartNewGameYourEmpireTypeNormalClassic.Name = "btnStartNewGameYourEmpireTypeNormalClassic";
            btnStartNewGameYourEmpireTypeNormalClassic.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourEmpireTypeNormalClassic.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourEmpireTypeNormalClassic.Size = new System.Drawing.Size(441, 73);
            btnStartNewGameYourEmpireTypeNormalClassic.TabIndex = 264;
            btnStartNewGameYourEmpireTypeNormalClassic.TabStop = false;
            btnStartNewGameYourEmpireTypeNormalClassic.Text = "Play a Custom Game as a Standard Empire >>";
            btnStartNewGameYourEmpireTypeNormalClassic.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourEmpireTypeNormalClassic.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourEmpireTypeNormalClassic.ToggledOn = false;
            // 
            // btnStartNewGameYourEmpireTypePirateClassic
            // 
            btnStartNewGameYourEmpireTypePirateClassic.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourEmpireTypePirateClassic.ClipBackground = false;
            btnStartNewGameYourEmpireTypePirateClassic.DelayFrameRefresh = false;
            btnStartNewGameYourEmpireTypePirateClassic.Dock = DockStyle.Fill;
            btnStartNewGameYourEmpireTypePirateClassic.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourEmpireTypePirateClassic.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourEmpireTypePirateClassic.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourEmpireTypePirateClassic.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourEmpireTypePirateClassic.IntensifyColors = false;
            btnStartNewGameYourEmpireTypePirateClassic.Location = new System.Drawing.Point(450, 3);
            btnStartNewGameYourEmpireTypePirateClassic.Name = "btnStartNewGameYourEmpireTypePirateClassic";
            btnStartNewGameYourEmpireTypePirateClassic.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourEmpireTypePirateClassic.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourEmpireTypePirateClassic.Size = new System.Drawing.Size(441, 73);
            btnStartNewGameYourEmpireTypePirateClassic.TabIndex = 263;
            btnStartNewGameYourEmpireTypePirateClassic.TabStop = false;
            btnStartNewGameYourEmpireTypePirateClassic.Text = "Play a Custom Game as a Pirate Faction >>";
            btnStartNewGameYourEmpireTypePirateClassic.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourEmpireTypePirateClassic.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourEmpireTypePirateClassic.ToggledOn = false;
            // 
            // btnStartNewGameYourEmpireTypePirateShadows
            // 
            btnStartNewGameYourEmpireTypePirateShadows.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourEmpireTypePirateShadows.ClipBackground = false;
            btnStartNewGameYourEmpireTypePirateShadows.DelayFrameRefresh = false;
            btnStartNewGameYourEmpireTypePirateShadows.Dock = DockStyle.Fill;
            btnStartNewGameYourEmpireTypePirateShadows.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourEmpireTypePirateShadows.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourEmpireTypePirateShadows.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourEmpireTypePirateShadows.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourEmpireTypePirateShadows.IntensifyColors = false;
            btnStartNewGameYourEmpireTypePirateShadows.Location = new System.Drawing.Point(149, 3);
            btnStartNewGameYourEmpireTypePirateShadows.Name = "btnStartNewGameYourEmpireTypePirateShadows";
            btnStartNewGameYourEmpireTypePirateShadows.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourEmpireTypePirateShadows.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourEmpireTypePirateShadows.Size = new System.Drawing.Size(140, 270);
            btnStartNewGameYourEmpireTypePirateShadows.TabIndex = 262;
            btnStartNewGameYourEmpireTypePirateShadows.TabStop = false;
            btnStartNewGameYourEmpireTypePirateShadows.Text = "Pirate Faction in the Age of Shadows >>";
            btnStartNewGameYourEmpireTypePirateShadows.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourEmpireTypePirateShadows.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourEmpireTypePirateShadows.ToggledOn = false;
            // 
            // btnStartNewGameYourEmpireTypeNormalShadows
            // 
            btnStartNewGameYourEmpireTypeNormalShadows.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourEmpireTypeNormalShadows.ClipBackground = false;
            btnStartNewGameYourEmpireTypeNormalShadows.DelayFrameRefresh = false;
            btnStartNewGameYourEmpireTypeNormalShadows.Dock = DockStyle.Fill;
            btnStartNewGameYourEmpireTypeNormalShadows.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourEmpireTypeNormalShadows.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourEmpireTypeNormalShadows.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourEmpireTypeNormalShadows.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourEmpireTypeNormalShadows.IntensifyColors = false;
            btnStartNewGameYourEmpireTypeNormalShadows.Location = new System.Drawing.Point(295, 3);
            btnStartNewGameYourEmpireTypeNormalShadows.Name = "btnStartNewGameYourEmpireTypeNormalShadows";
            btnStartNewGameYourEmpireTypeNormalShadows.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourEmpireTypeNormalShadows.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourEmpireTypeNormalShadows.Size = new System.Drawing.Size(140, 270);
            btnStartNewGameYourEmpireTypeNormalShadows.TabIndex = 240;
            btnStartNewGameYourEmpireTypeNormalShadows.TabStop = false;
            btnStartNewGameYourEmpireTypeNormalShadows.Text = "Standard Empire in the Age of Shadows >>";
            btnStartNewGameYourEmpireTypeNormalShadows.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourEmpireTypeNormalShadows.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourEmpireTypeNormalShadows.ToggledOn = false;
            // 
            // lblHelpTitle
            // 
            lblHelpTitle.AutoSize = true;
            lblHelpTitle.BackColor = System.Drawing.Color.Transparent;
            lblHelpTitle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblHelpTitle.ForeColor = System.Drawing.Color.Yellow;
            lblHelpTitle.Location = new System.Drawing.Point(3, 0);
            lblHelpTitle.Name = "lblHelpTitle";
            lblHelpTitle.Size = new System.Drawing.Size(0, 14);
            lblHelpTitle.TabIndex = 11;
            // 
            // lblHelpDescription
            // 
            lblHelpDescription.AutoSize = true;
            lblHelpDescription.BackColor = System.Drawing.Color.Transparent;
            lblHelpDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblHelpDescription.ForeColor = System.Drawing.Color.Yellow;
            lblHelpDescription.Location = new System.Drawing.Point(9, 0);
            lblHelpDescription.MaximumSize = new System.Drawing.Size(720, 32);
            lblHelpDescription.Name = "lblHelpDescription";
            lblHelpDescription.Size = new System.Drawing.Size(0, 13);
            lblHelpDescription.TabIndex = 9;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 32000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(pnlStartNewGameIntroductoryBorder, 0, 1);
            tableLayoutPanel1.Controls.Add(lblHelpTitle, 0, 0);
            tableLayoutPanel1.Controls.Add(lblHelpDescription, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(894, 104);
            tableLayoutPanel1.TabIndex = 280;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(lblStartNewGameActiveTheme, 0, 4);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel2.Controls.Add(picStartNewGameYourEmpireTypeTimeline, 0, 2);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 55.4169464F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 27.8491116F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.7339439F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new System.Drawing.Size(900, 635);
            tableLayoutPanel2.TabIndex = 281;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 6;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(btnStartNewGameYourEmpireTypeTheAncientGalaxy, 0, 0);
            tableLayoutPanel3.Controls.Add(btnStartNewGameYourEmpireTypePirateShadows, 1, 0);
            tableLayoutPanel3.Controls.Add(btnStartNewGameYourEmpireTypeLegends, 5, 0);
            tableLayoutPanel3.Controls.Add(btnStartNewGameYourEmpireTypeNormalShadows, 2, 0);
            tableLayoutPanel3.Controls.Add(btnStartNewGameYourEmpireTypeReturnOfTheShakturi, 4, 0);
            tableLayoutPanel3.Controls.Add(btnStartNewGameYourEmpireTypeClassicEra, 3, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(3, 113);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new System.Drawing.Size(894, 276);
            tableLayoutPanel3.TabIndex = 281;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(btnStartNewGameYourEmpireTypeNormalClassic, 0, 0);
            tableLayoutPanel4.Controls.Add(btnStartNewGameYourEmpireTypePirateClassic, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new System.Drawing.Point(3, 537);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new System.Drawing.Size(894, 79);
            tableLayoutPanel4.TabIndex = 282;
            // 
            // StartNewGameSelectGameTypeScreenPanel
            // 
            BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            Controls.Add(tableLayoutPanel2);
            Location = new System.Drawing.Point(20, 100);
            Name = "StartNewGameSelectGameTypeScreenPanel";
            Size = new System.Drawing.Size(900, 635);
            pnlStartNewGameIntroductoryBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picStartNewGameYourEmpireTypeTimeline).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion


        public GlassButton btnStartNewGameYourEmpireTypeLegends;

        public GlassButton btnStartNewGameYourEmpireTypeReturnOfTheShakturi;

        public GlassButton btnStartNewGameYourEmpireTypeClassicEra;

        public GlassButton btnStartNewGameYourEmpireTypeTheAncientGalaxy;
        public SmoothLabel lblStartNewGameActiveTheme;
        public GlassButton btnStartNewGameIntroductory;
        public RoundRectanglePanel pnlStartNewGameIntroductoryBorder;
        public GlassButton btnStartNewGameYourEmpireTypeNormalClassic;

        public GlassButton btnStartNewGameYourEmpireTypePirateClassic;
        public GlassButton btnStartNewGameYourEmpireTypeNormalShadows;

        public GlassButton btnStartNewGameYourEmpireTypePirateShadows;

        private System.Windows.Forms.ToolTip toolTip;
        private PictureBox picStartNewGameYourEmpireTypeTimeline;
        public SmoothLabel lblHelpDescription;
        public SmoothLabel lblHelpTitle;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
    }
}
