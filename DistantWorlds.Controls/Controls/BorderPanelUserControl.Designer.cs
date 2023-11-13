namespace DistantWorlds.Controls
{
    partial class BorderPanelUserControl
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
            BodyPanel = new System.Windows.Forms.Panel();
            SuspendLayout();
            // 
            // BodyPanel
            // 
            BodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            BodyPanel.Location = new System.Drawing.Point(0, 0);
            BodyPanel.Name = "BodyPanel";
            BodyPanel.Size = new System.Drawing.Size(150, 150);
            BodyPanel.TabIndex = 1;
            BodyPanel.Paint += BodyPanel_Paint;
            // 
            // BorderPanelUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(BodyPanel);
            Name = "BorderPanelUserControl";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel BodyPanel;
    }
}
