using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    partial class ThemesScreenPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DistantWorlds.Controls.ThemesScreenPanel));

            this.SuspendLayout();

            this.lblThemeGalaxyMaps = new DistantWorlds.Controls.SmoothLabel();
            this.picThemeImage = new System.Windows.Forms.PictureBox();
            this.lblThemeDescription = new DistantWorlds.Controls.SmoothLabel();
            this.lblThemeTitle = new DistantWorlds.Controls.SmoothLabel();
            this.lblCurrentTheme = new System.Windows.Forms.Label();
            this.pnlThemeDetail = new DistantWorlds.Controls.GradientPanel();
            this.btnThemeSwitch = new DistantWorlds.Controls.GlassButton();
            this.btnThemeCancel = new DistantWorlds.Controls.GlassButton();

            ((System.ComponentModel.ISupportInitialize)this.picThemeImage).BeginInit();
            this.pnlThemeDetail.SuspendLayout();
            this.BackColor = System.Drawing.Color.FromArgb(48, 48, 64);
            this.BackgroundImage = (System.Drawing.Image)resources.GetObject("ThemesScreenPanel.BackgroundImage");
            this.BorderColor1 = System.Drawing.Color.FromArgb(96, 200, 200, 200);
            this.BorderColor2 = System.Drawing.Color.FromArgb(96, 140, 140, 140);
            this.BorderColor3 = System.Drawing.Color.FromArgb(96, 20, 20, 20);
            this.BorderColor4 = System.Drawing.Color.FromArgb(96, 80, 80, 80);
            this.BorderSize = 3;
            this.lblThemeGalaxyMaps.AutoSize = true;
            this.lblThemeGalaxyMaps.Font = UiScale.NewFont("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblThemeGalaxyMaps.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblThemeGalaxyMaps.Location = new System.Drawing.Point(UiScale.S(78), UiScale.S(67));
            this.lblThemeGalaxyMaps.Name = "lblThemeGalaxyMaps";
            this.lblThemeGalaxyMaps.Size = new System.Drawing.Size(UiScale.S(80), UiScale.S(13));
            this.lblThemeGalaxyMaps.TabIndex = 4;
            this.lblThemeGalaxyMaps.Text = "Galaxy Maps";
            this.picThemeImage.Location = new System.Drawing.Point(UiScale.S(118), UiScale.S(12));
            this.picThemeImage.Name = "picThemeImage";
            this.picThemeImage.Size = new System.Drawing.Size(UiScale.S(100), UiScale.S(50));
            this.picThemeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picThemeImage.TabIndex = 3;
            this.picThemeImage.TabStop = false;
            this.lblThemeDescription.AutoSize = true;
            this.lblThemeDescription.Font = UiScale.NewFont("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblThemeDescription.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblThemeDescription.Location = new System.Drawing.Point(UiScale.S(24), UiScale.S(47));
            this.lblThemeDescription.Name = "lblThemeDescription";
            this.lblThemeDescription.Size = new System.Drawing.Size(UiScale.S(40), UiScale.S(13));
            this.lblThemeDescription.TabIndex = 2;
            this.lblThemeDescription.Text = "Detail";
            this.lblThemeTitle.AutoSize = true;
            this.lblThemeTitle.Font = UiScale.NewFont("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblThemeTitle.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblThemeTitle.Location = new System.Drawing.Point(UiScale.S(24), UiScale.S(18));
            this.lblThemeTitle.Name = "lblThemeTitle";
            this.lblThemeTitle.Size = new System.Drawing.Size(UiScale.S(36), UiScale.S(14));
            this.lblThemeTitle.TabIndex = 1;
            this.lblThemeTitle.Text = "Title";
            this.lblCurrentTheme.AutoSize = true;
            this.lblCurrentTheme.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentTheme.Font = UiScale.NewFont("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblCurrentTheme.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblCurrentTheme.Location = new System.Drawing.Point(UiScale.S(17), UiScale.S(17));
            this.lblCurrentTheme.Name = "lblCurrentTheme";
            this.lblCurrentTheme.Size = new System.Drawing.Size(UiScale.S(105), UiScale.S(14));
            this.lblCurrentTheme.TabIndex = 216;
            this.lblCurrentTheme.Text = "Current Theme";
            this.pnlThemeDetail.BackColor = System.Drawing.Color.FromArgb(39, 40, 44);
            this.pnlThemeDetail.BackColor2 = System.Drawing.Color.FromArgb(22, 21, 26);
            this.pnlThemeDetail.BackColor3 = System.Drawing.Color.FromArgb(51, 54, 61);
            this.pnlThemeDetail.BorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            this.pnlThemeDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlThemeDetail.BorderWidth = UiScale.S(2);
            this.pnlThemeDetail.Controls.Add(this.lblThemeGalaxyMaps);
            this.pnlThemeDetail.Controls.Add(this.picThemeImage);
            this.pnlThemeDetail.Controls.Add(this.lblThemeDescription);
            this.pnlThemeDetail.Controls.Add(this.lblThemeTitle);
            this.pnlThemeDetail.Curvature = UiScale.S(20);
            this.pnlThemeDetail.GradientMode = DistantWorlds.Controls.LinearGradientMode.Vertical;
            this.pnlThemeDetail.Location = new System.Drawing.Point(UiScale.S(99), UiScale.S(16));
            this.pnlThemeDetail.Margin = new System.Windows.Forms.Padding(0);
            this.pnlThemeDetail.Name = "pnlThemeDetail";
            this.pnlThemeDetail.Size = new System.Drawing.Size(UiScale.S(235), UiScale.S(87));
            this.pnlThemeDetail.TabIndex = 29; this.btnThemeSwitch.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.btnThemeSwitch.ClipBackground = false;
            this.btnThemeSwitch.DelayFrameRefresh = false;
            this.btnThemeSwitch.Font = UiScale.NewFont("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnThemeSwitch.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.btnThemeSwitch.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            this.btnThemeSwitch.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            this.btnThemeSwitch.IntensifyColors = false;
            this.btnThemeSwitch.Location = new System.Drawing.Point(UiScale.S(196), UiScale.S(126));
            this.btnThemeSwitch.Name = "btnThemeSwitch";
            this.btnThemeSwitch.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            this.btnThemeSwitch.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            this.btnThemeSwitch.Size = new System.Drawing.Size(UiScale.S(138), UiScale.S(30));
            this.btnThemeSwitch.TabIndex = 214;
            this.btnThemeSwitch.TabStop = false;
            this.btnThemeSwitch.Text = "Switch Theme";
            this.btnThemeSwitch.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.btnThemeSwitch.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnThemeSwitch.ToggledOn = false;
            this.btnThemeCancel.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.btnThemeCancel.ClipBackground = false;
            this.btnThemeCancel.DelayFrameRefresh = false;
            this.btnThemeCancel.Font = UiScale.NewFont("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnThemeCancel.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.btnThemeCancel.GlowColor = System.Drawing.Color.FromArgb(48, 48, 128);
            this.btnThemeCancel.InnerBorderColor = System.Drawing.Color.FromArgb(67, 67, 77);
            this.btnThemeCancel.IntensifyColors = false;
            this.btnThemeCancel.Location = new System.Drawing.Point(UiScale.S(99), UiScale.S(125));
            this.btnThemeCancel.Name = "btnThemeCancel";
            this.btnThemeCancel.OuterBorderColor = System.Drawing.Color.FromArgb(0, 0, 16);
            this.btnThemeCancel.ShineColor = System.Drawing.Color.FromArgb(112, 112, 128);
            this.btnThemeCancel.Size = new System.Drawing.Size(UiScale.S(95), UiScale.S(30));
            this.btnThemeCancel.TabIndex = 215;
            this.btnThemeCancel.TabStop = false;
            this.btnThemeCancel.Text = "Cancel";
            this.btnThemeCancel.TextColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.btnThemeCancel.TextColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnThemeCancel.ToggledOn = false;

            this.Controls.Add(this.lblCurrentTheme);
            this.Controls.Add(this.pnlThemeDetail);
            this.Controls.Add(this.btnThemeSwitch);
            this.Controls.Add(this.btnThemeCancel);
            this.HeaderIcon = null;
            this.HeaderTitle = "Change Theme";
            this.Location = new System.Drawing.Point(UiScale.S(198), UiScale.S(19));
            this.Name = "pnlThemes";
            this.Size = new System.Drawing.Size(UiScale.S(344), UiScale.S(172));
            this.TabIndex = 125;
            this.Visible = false;


            this.pnlThemeDetail.ResumeLayout(false);
            this.pnlThemeDetail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.picThemeImage).EndInit();
        }

        #endregion


        internal Label lblCurrentTheme;
        internal GradientPanel pnlThemeDetail;
        internal GlassButton btnThemeSwitch;
        internal GlassButton btnThemeCancel;
        internal SmoothLabel lblThemeGalaxyMaps;

        internal SmoothLabel lblThemeDescription;

        internal SmoothLabel lblThemeTitle;


        internal PictureBox picThemeImage;
    }

}
