namespace BaconDistantWorlds.Forms
{
    partial class SelectMiningDeliveryTarget
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gradientPanel1 = new DistantWorlds.Controls.GradientPanel();
            this.cmbTarget = new System.Windows.Forms.ComboBox();
            this.btnCancel = new DistantWorlds.Controls.GlassButton();
            this.btnSelectTargets = new DistantWorlds.Controls.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.bindingTarget = new System.Windows.Forms.BindingSource(this.components);
            this.btnSetAutoTarget = new DistantWorlds.Controls.GlassButton();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.AutoSize = true;
            this.gradientPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.gradientPanel1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.gradientPanel1.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(61)))));
            this.gradientPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.gradientPanel1.Controls.Add(this.btnSetAutoTarget);
            this.gradientPanel1.Controls.Add(this.cmbTarget);
            this.gradientPanel1.Controls.Add(this.btnCancel);
            this.gradientPanel1.Controls.Add(this.btnSelectTargets);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel1.GradientMode = DistantWorlds.Controls.LinearGradientMode.Vertical;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(199, 128);
            this.gradientPanel1.TabIndex = 2;
            // 
            // cmbTarget
            // 
            this.cmbTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarget.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbTarget.FormattingEnabled = true;
            this.cmbTarget.Location = new System.Drawing.Point(10, 34);
            this.cmbTarget.Name = "cmbTarget";
            this.cmbTarget.Size = new System.Drawing.Size(179, 21);
            this.cmbTarget.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.ClipBackground = false;
            this.btnCancel.DelayFrameRefresh = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnCancel.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnCancel.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnCancel.IntensifyColors = false;
            this.btnCancel.Location = new System.Drawing.Point(114, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnCancel.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnCancel.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCancel.ToggledOn = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelectTargets
            // 
            this.btnSelectTargets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSelectTargets.ClipBackground = false;
            this.btnSelectTargets.DelayFrameRefresh = false;
            this.btnSelectTargets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnSelectTargets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnSelectTargets.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnSelectTargets.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnSelectTargets.IntensifyColors = false;
            this.btnSelectTargets.Location = new System.Drawing.Point(10, 66);
            this.btnSelectTargets.Name = "btnSelectTargets";
            this.btnSelectTargets.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnSelectTargets.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnSelectTargets.Size = new System.Drawing.Size(75, 23);
            this.btnSelectTargets.TabIndex = 4;
            this.btnSelectTargets.Text = "Add";
            this.btnSelectTargets.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnSelectTargets.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSelectTargets.ToggledOn = false;
            this.btnSelectTargets.Click += new System.EventHandler(this.btnSelectTargets_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label1.Location = new System.Drawing.Point(77, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Target";
            // 
            // btnSetAutoTarget
            // 
            this.btnSetAutoTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSetAutoTarget.ClipBackground = false;
            this.btnSetAutoTarget.DelayFrameRefresh = false;
            this.btnSetAutoTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnSetAutoTarget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnSetAutoTarget.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnSetAutoTarget.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnSetAutoTarget.IntensifyColors = false;
            this.btnSetAutoTarget.Location = new System.Drawing.Point(12, 93);
            this.btnSetAutoTarget.Name = "btnSetAutoTarget";
            this.btnSetAutoTarget.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnSetAutoTarget.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnSetAutoTarget.Size = new System.Drawing.Size(175, 23);
            this.btnSetAutoTarget.TabIndex = 8;
            this.btnSetAutoTarget.Text = "Set auto target";
            this.btnSetAutoTarget.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnSetAutoTarget.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSetAutoTarget.ToggledOn = false;
            this.btnSetAutoTarget.Click += new System.EventHandler(this.btnSetAutoTarget_Click);
            // 
            // SelectMiningDeliveryTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(199, 128);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectMiningDeliveryTarget";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SelectMiningDeliveryTarget";
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingTarget)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DistantWorlds.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.ComboBox cmbTarget;
        private DistantWorlds.Controls.GlassButton btnCancel;
        private DistantWorlds.Controls.GlassButton btnSelectTargets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource bindingTarget;
        private DistantWorlds.Controls.GlassButton btnSetAutoTarget;
    }
}