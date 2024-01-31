using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    partial class StartNewGameYourRacePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartNewGameYourRacePanel));
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlStartNewGameYourEmpireRace = new GradientPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            cmbStartNewGameYourEmpireRace = new RaceDropDown();
            lblStartNewGameYourEmpireRaceName = new SmoothLabel();
            lnkStartNewGameYourEmpireRace = new LinkLabel();
            pnlStartNewGameYourEmpireRaceAttributesContainer = new System.Windows.Forms.Panel();
            pnlStartNewGameYourEmpireRaceAttributes = new RaceSummaryPanel();
            picStartNewGameYourEmpireRace = new PictureBox();
            picStartNewGameYourRaceImage = new PictureBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            btnStartNewGameYourRacePrevious = new GlassButton();
            btnStartNewGameYourRaceNext = new GlassButton();
            tableLayoutPanel1.SuspendLayout();
            pnlStartNewGameYourEmpireRace.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            pnlStartNewGameYourEmpireRaceAttributesContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStartNewGameYourEmpireRace).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picStartNewGameYourRaceImage).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(pnlStartNewGameYourEmpireRace, 0, 1);
            tableLayoutPanel1.Controls.Add(picStartNewGameYourRaceImage, 1, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            tableLayoutPanel1.TabIndex = 262;
            // 
            // pnlStartNewGameYourEmpireRace
            // 
            pnlStartNewGameYourEmpireRace.AutoSize = true;
            pnlStartNewGameYourEmpireRace.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlStartNewGameYourEmpireRace.BackColor = System.Drawing.Color.FromArgb(39, 40, 44);
            pnlStartNewGameYourEmpireRace.BackColor2 = System.Drawing.Color.FromArgb(22, 21, 26);
            pnlStartNewGameYourEmpireRace.BackColor3 = System.Drawing.Color.FromArgb(51, 54, 61);
            pnlStartNewGameYourEmpireRace.BorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            pnlStartNewGameYourEmpireRace.BorderStyle = BorderStyle.FixedSingle;
            pnlStartNewGameYourEmpireRace.BorderWidth = 2;
            pnlStartNewGameYourEmpireRace.Controls.Add(tableLayoutPanel2);
            pnlStartNewGameYourEmpireRace.Curvature = 20;
            pnlStartNewGameYourEmpireRace.Dock = DockStyle.Fill;
            pnlStartNewGameYourEmpireRace.GradientMode = LinearGradientMode.Vertical;
            pnlStartNewGameYourEmpireRace.Location = new System.Drawing.Point(0, 20);
            pnlStartNewGameYourEmpireRace.Margin = new Padding(0);
            pnlStartNewGameYourEmpireRace.Name = "pnlStartNewGameYourEmpireRace";
            pnlStartNewGameYourEmpireRace.Size = new System.Drawing.Size(1, 1);
            pnlStartNewGameYourEmpireRace.TabIndex = 238;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(cmbStartNewGameYourEmpireRace, 0, 0);
            tableLayoutPanel2.Controls.Add(lblStartNewGameYourEmpireRaceName, 1, 0);
            tableLayoutPanel2.Controls.Add(lnkStartNewGameYourEmpireRace, 0, 2);
            tableLayoutPanel2.Controls.Add(pnlStartNewGameYourEmpireRaceAttributesContainer, 1, 1);
            tableLayoutPanel2.Controls.Add(picStartNewGameYourEmpireRace, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(7);
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new System.Drawing.Size(1, 1);
            tableLayoutPanel2.TabIndex = 239;
            // 
            // cmbStartNewGameYourEmpireRace
            // 
            cmbStartNewGameYourEmpireRace.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            cmbStartNewGameYourEmpireRace.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStartNewGameYourEmpireRace.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStartNewGameYourEmpireRace.FlatStyle = FlatStyle.Popup;
            cmbStartNewGameYourEmpireRace.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbStartNewGameYourEmpireRace.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            cmbStartNewGameYourEmpireRace.FormattingEnabled = true;
            cmbStartNewGameYourEmpireRace.Location = new System.Drawing.Point(10, 10);
            cmbStartNewGameYourEmpireRace.Name = "cmbStartNewGameYourEmpireRace";
            cmbStartNewGameYourEmpireRace.Size = new System.Drawing.Size(200, 23);
            cmbStartNewGameYourEmpireRace.TabIndex = 237;
            // 
            // lblStartNewGameYourEmpireRaceName
            // 
            lblStartNewGameYourEmpireRaceName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblStartNewGameYourEmpireRaceName.Location = new System.Drawing.Point(216, 12);
            lblStartNewGameYourEmpireRaceName.Name = "lblStartNewGameYourEmpireRaceName";
            lblStartNewGameYourEmpireRaceName.Size = new System.Drawing.Size(487, 19);
            lblStartNewGameYourEmpireRaceName.TabIndex = 261;
            // 
            // lnkStartNewGameYourEmpireRace
            // 
            lnkStartNewGameYourEmpireRace.ActiveLinkColor = System.Drawing.Color.FromArgb(255, 128, 0);
            lnkStartNewGameYourEmpireRace.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lnkStartNewGameYourEmpireRace.AutoSize = true;
            lnkStartNewGameYourEmpireRace.BackColor = System.Drawing.Color.Transparent;
            lnkStartNewGameYourEmpireRace.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lnkStartNewGameYourEmpireRace.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkStartNewGameYourEmpireRace.LinkColor = System.Drawing.Color.FromArgb(255, 192, 0);
            lnkStartNewGameYourEmpireRace.Location = new System.Drawing.Point(10, 245);
            lnkStartNewGameYourEmpireRace.Name = "lnkStartNewGameYourEmpireRace";
            lnkStartNewGameYourEmpireRace.Size = new System.Drawing.Size(200, 13);
            lnkStartNewGameYourEmpireRace.TabIndex = 212;
            lnkStartNewGameYourEmpireRace.TabStop = true;
            lnkStartNewGameYourEmpireRace.Text = "Read more about this race...";
            // 
            // pnlStartNewGameYourEmpireRaceAttributesContainer
            // 
            pnlStartNewGameYourEmpireRaceAttributesContainer.AutoScroll = true;
            pnlStartNewGameYourEmpireRaceAttributesContainer.AutoSize = true;
            pnlStartNewGameYourEmpireRaceAttributesContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlStartNewGameYourEmpireRaceAttributesContainer.Controls.Add(pnlStartNewGameYourEmpireRaceAttributes);
            pnlStartNewGameYourEmpireRaceAttributesContainer.Dock = DockStyle.Fill;
            pnlStartNewGameYourEmpireRaceAttributesContainer.Location = new System.Drawing.Point(216, 39);
            pnlStartNewGameYourEmpireRaceAttributesContainer.Name = "pnlStartNewGameYourEmpireRaceAttributesContainer";
            tableLayoutPanel2.SetRowSpan(pnlStartNewGameYourEmpireRaceAttributesContainer, 2);
            pnlStartNewGameYourEmpireRaceAttributesContainer.Size = new System.Drawing.Size(487, 220);
            pnlStartNewGameYourEmpireRaceAttributesContainer.TabIndex = 238;
            // 
            // pnlStartNewGameYourEmpireRaceAttributes
            // 
            pnlStartNewGameYourEmpireRaceAttributes.AutoScroll = true;
            pnlStartNewGameYourEmpireRaceAttributes.AutoSize = true;
            pnlStartNewGameYourEmpireRaceAttributes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlStartNewGameYourEmpireRaceAttributes.Dock = DockStyle.Fill;
            pnlStartNewGameYourEmpireRaceAttributes.Location = new System.Drawing.Point(0, 0);
            pnlStartNewGameYourEmpireRaceAttributes.Name = "pnlStartNewGameYourEmpireRaceAttributes";
            pnlStartNewGameYourEmpireRaceAttributes.Size = new System.Drawing.Size(487, 220);
            pnlStartNewGameYourEmpireRaceAttributes.TabIndex = 211;
            // 
            // picStartNewGameYourEmpireRace
            // 
            picStartNewGameYourEmpireRace.Location = new System.Drawing.Point(10, 39);
            picStartNewGameYourEmpireRace.Name = "picStartNewGameYourEmpireRace";
            picStartNewGameYourEmpireRace.Size = new System.Drawing.Size(200, 200);
            picStartNewGameYourEmpireRace.SizeMode = PictureBoxSizeMode.Zoom;
            picStartNewGameYourEmpireRace.TabIndex = 209;
            picStartNewGameYourEmpireRace.TabStop = false;
            // 
            // picStartNewGameYourRaceImage
            // 
            picStartNewGameYourRaceImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picStartNewGameYourRaceImage.Image = (System.Drawing.Image)resources.GetObject("picStartNewGameYourRaceImage.Image");
            picStartNewGameYourRaceImage.Location = new System.Drawing.Point(-72, 23);
            picStartNewGameYourRaceImage.Name = "picStartNewGameYourRaceImage";
            picStartNewGameYourRaceImage.Size = new System.Drawing.Size(70, 1);
            picStartNewGameYourRaceImage.SizeMode = PictureBoxSizeMode.Zoom;
            picStartNewGameYourRaceImage.TabIndex = 259;
            picStartNewGameYourRaceImage.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel3, 2);
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(btnStartNewGameYourRacePrevious, 0, 0);
            tableLayoutPanel3.Controls.Add(btnStartNewGameYourRaceNext, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(3, 7);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new System.Drawing.Size(1, 1);
            tableLayoutPanel3.TabIndex = 260;
            // 
            // btnStartNewGameYourRacePrevious
            // 
            btnStartNewGameYourRacePrevious.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnStartNewGameYourRacePrevious.AutoSize = true;
            btnStartNewGameYourRacePrevious.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnStartNewGameYourRacePrevious.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourRacePrevious.ClipBackground = false;
            btnStartNewGameYourRacePrevious.DelayFrameRefresh = false;
            btnStartNewGameYourRacePrevious.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourRacePrevious.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourRacePrevious.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourRacePrevious.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourRacePrevious.IntensifyColors = false;
            btnStartNewGameYourRacePrevious.Location = new System.Drawing.Point(3, 3);
            btnStartNewGameYourRacePrevious.Name = "btnStartNewGameYourRacePrevious";
            btnStartNewGameYourRacePrevious.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourRacePrevious.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourRacePrevious.Size = new System.Drawing.Size(1, 20);
            btnStartNewGameYourRacePrevious.TabIndex = 258;
            btnStartNewGameYourRacePrevious.TabStop = false;
            btnStartNewGameYourRacePrevious.Text = "<< Previous: Colonization && Territory";
            btnStartNewGameYourRacePrevious.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourRacePrevious.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourRacePrevious.ToggledOn = false;
            // 
            // btnStartNewGameYourRaceNext
            // 
            btnStartNewGameYourRaceNext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnStartNewGameYourRaceNext.AutoSize = true;
            btnStartNewGameYourRaceNext.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnStartNewGameYourRaceNext.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            btnStartNewGameYourRaceNext.ClipBackground = false;
            btnStartNewGameYourRaceNext.DelayFrameRefresh = false;
            btnStartNewGameYourRaceNext.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            btnStartNewGameYourRaceNext.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            btnStartNewGameYourRaceNext.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            btnStartNewGameYourRaceNext.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            btnStartNewGameYourRaceNext.IntensifyColors = false;
            btnStartNewGameYourRaceNext.Location = new System.Drawing.Point(3, 3);
            btnStartNewGameYourRaceNext.Name = "btnStartNewGameYourRaceNext";
            btnStartNewGameYourRaceNext.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            btnStartNewGameYourRaceNext.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            btnStartNewGameYourRaceNext.Size = new System.Drawing.Size(1, 20);
            btnStartNewGameYourRaceNext.TabIndex = 240;
            btnStartNewGameYourRaceNext.TabStop = false;
            btnStartNewGameYourRaceNext.Text = "Next: Your Empire >>";
            btnStartNewGameYourRaceNext.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnStartNewGameYourRaceNext.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            btnStartNewGameYourRaceNext.ToggledOn = false;
            // 
            // StartNewGameYourRacePanel
            // 
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(tableLayoutPanel1);
            Location = new System.Drawing.Point(10, 200);
            Name = "StartNewGameYourRacePanel";
            Size = new System.Drawing.Size(0, 0);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            pnlStartNewGameYourEmpireRace.ResumeLayout(false);
            pnlStartNewGameYourEmpireRace.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            pnlStartNewGameYourEmpireRaceAttributesContainer.ResumeLayout(false);
            pnlStartNewGameYourEmpireRaceAttributesContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picStartNewGameYourEmpireRace).EndInit();
            ((System.ComponentModel.ISupportInitialize)picStartNewGameYourRaceImage).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        public GradientPanel pnlStartNewGameYourEmpireRace;
        private TableLayoutPanel tableLayoutPanel2;
        public RaceDropDown cmbStartNewGameYourEmpireRace;
        public SmoothLabel lblStartNewGameYourEmpireRaceName;
        public LinkLabel lnkStartNewGameYourEmpireRace;
        public System.Windows.Forms.Panel pnlStartNewGameYourEmpireRaceAttributesContainer;
        public RaceSummaryPanel pnlStartNewGameYourEmpireRaceAttributes;
        public PictureBox picStartNewGameYourEmpireRace;
        public PictureBox picStartNewGameYourRaceImage;
        private TableLayoutPanel tableLayoutPanel3;
        public GlassButton btnStartNewGameYourRacePrevious;
        public GlassButton btnStartNewGameYourRaceNext;
    }
}
