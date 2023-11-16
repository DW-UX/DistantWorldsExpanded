namespace ExpansionMod.Controls
{
    partial class HotKeyModEditorControl
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
            this.components = new System.ComponentModel.Container();
            this.gradientPanel1 = new DistantWorlds.Controls.GradientPanel();
            this.btnRemoveHotkey = new DistantWorlds.Controls.GlassButton();
            this.btnAddHotkey = new DistantWorlds.Controls.GlassButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lstbHotkeys = new System.Windows.Forms.ListBox();
            this.mappedHotKeysBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.targetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lstbTarget = new System.Windows.Forms.ListBox();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mappedHotKeysBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.AutoSize = true;
            this.gradientPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.gradientPanel1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(21)))), ((int)(((byte)(26)))));
            this.gradientPanel1.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(61)))));
            this.gradientPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.gradientPanel1.Controls.Add(this.btnRemoveHotkey);
            this.gradientPanel1.Controls.Add(this.btnAddHotkey);
            this.gradientPanel1.Controls.Add(this.label2);
            this.gradientPanel1.Controls.Add(this.lstbHotkeys);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.Controls.Add(this.lstbTarget);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel1.GradientMode = DistantWorlds.Controls.LinearGradientMode.Vertical;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(629, 219);
            this.gradientPanel1.TabIndex = 0;
            // 
            // btnRemoveHotkey
            // 
            this.btnRemoveHotkey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveHotkey.ClipBackground = false;
            this.btnRemoveHotkey.DelayFrameRefresh = false;
            this.btnRemoveHotkey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRemoveHotkey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRemoveHotkey.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRemoveHotkey.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRemoveHotkey.IntensifyColors = false;
            this.btnRemoveHotkey.Location = new System.Drawing.Point(546, 61);
            this.btnRemoveHotkey.Name = "btnRemoveHotkey";
            this.btnRemoveHotkey.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRemoveHotkey.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRemoveHotkey.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveHotkey.TabIndex = 5;
            this.btnRemoveHotkey.Text = "Remove";
            this.btnRemoveHotkey.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRemoveHotkey.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRemoveHotkey.ToggledOn = false;
            this.btnRemoveHotkey.Click += new System.EventHandler(this.btnRemoveHotkey_Click);
            // 
            // btnAddHotkey
            // 
            this.btnAddHotkey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddHotkey.ClipBackground = false;
            this.btnAddHotkey.DelayFrameRefresh = false;
            this.btnAddHotkey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddHotkey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnAddHotkey.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnAddHotkey.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnAddHotkey.IntensifyColors = false;
            this.btnAddHotkey.Location = new System.Drawing.Point(546, 32);
            this.btnAddHotkey.Name = "btnAddHotkey";
            this.btnAddHotkey.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnAddHotkey.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnAddHotkey.Size = new System.Drawing.Size(75, 23);
            this.btnAddHotkey.TabIndex = 4;
            this.btnAddHotkey.Text = "Add";
            this.btnAddHotkey.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnAddHotkey.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAddHotkey.ToggledOn = false;
            this.btnAddHotkey.Click += new System.EventHandler(this.btnAddHotkey_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label2.Location = new System.Drawing.Point(393, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hotkeys";
            // 
            // lstbHotkeys
            // 
            this.lstbHotkeys.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.lstbHotkeys.DataSource = this.mappedHotKeysBindingSource;
            this.lstbHotkeys.DisplayMember = "ViewName";
            this.lstbHotkeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstbHotkeys.FormattingEnabled = true;
            this.lstbHotkeys.ItemHeight = 16;
            this.lstbHotkeys.Location = new System.Drawing.Point(295, 33);
            this.lstbHotkeys.Name = "lstbHotkeys";
            this.lstbHotkeys.Size = new System.Drawing.Size(245, 164);
            this.lstbHotkeys.TabIndex = 2;
            // 
            // mappedHotKeysBindingSource
            // 
            this.mappedHotKeysBindingSource.DataMember = "MappedHotKeys";
            this.mappedHotKeysBindingSource.DataSource = this.targetBindingSource;
            // 
            // targetBindingSource
            // 
            this.targetBindingSource.AllowNew = false;
            this.targetBindingSource.DataSource = typeof(ExpansionMod.Controls.ViewKeyMappingTarget);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label1.Location = new System.Drawing.Point(133, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Target";
            // 
            // lstbTarget
            // 
            this.lstbTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.lstbTarget.DataSource = this.targetBindingSource;
            this.lstbTarget.DisplayMember = "ViewName";
            this.lstbTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstbTarget.FormattingEnabled = true;
            this.lstbTarget.HorizontalScrollbar = true;
            this.lstbTarget.ItemHeight = 16;
            this.lstbTarget.Location = new System.Drawing.Point(15, 33);
            this.lstbTarget.Name = "lstbTarget";
            this.lstbTarget.Size = new System.Drawing.Size(260, 164);
            this.lstbTarget.TabIndex = 0;
            // 
            // HotKeyModEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gradientPanel1);
            this.Name = "HotKeyModEditorControl";
            this.Size = new System.Drawing.Size(629, 219);
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mappedHotKeysBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DistantWorlds.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.ListBox lstbTarget;
        private System.Windows.Forms.Label label1;
        private DistantWorlds.Controls.GlassButton btnRemoveHotkey;
        private DistantWorlds.Controls.GlassButton btnAddHotkey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstbHotkeys;
        private System.Windows.Forms.BindingSource targetBindingSource;
        private System.Windows.Forms.BindingSource mappedHotKeysBindingSource;
    }
}
