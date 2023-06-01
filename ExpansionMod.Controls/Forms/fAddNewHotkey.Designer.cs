namespace ExpansionMod.Controls
{
    partial class fAddNewHotkey
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
            this.cmbHotKeyNames = new System.Windows.Forms.ComboBox();
            this.bindingSourceHotKeyList = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new DistantWorlds.Controls.GlassButton();
            this.btnAddHotkey = new DistantWorlds.Controls.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lstbHotkeys = new System.Windows.Forms.ListBox();
            this.bindingSourceSelectedHotKeys = new System.Windows.Forms.BindingSource(this.components);
            this.btnRemoveHotKey = new DistantWorlds.Controls.GlassButton();
            this.btnMoveUp = new DistantWorlds.Controls.GlassButton();
            this.btnMoveDown = new DistantWorlds.Controls.GlassButton();
            this.btnSave = new DistantWorlds.Controls.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceHotKeyList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSelectedHotKeys)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbHotKeyNames
            // 
            this.cmbHotKeyNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.cmbHotKeyNames.DataSource = this.bindingSourceHotKeyList;
            this.cmbHotKeyNames.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.cmbHotKeyNames.FormattingEnabled = true;
            this.cmbHotKeyNames.Location = new System.Drawing.Point(263, 27);
            this.cmbHotKeyNames.Name = "cmbHotKeyNames";
            this.cmbHotKeyNames.Size = new System.Drawing.Size(121, 21);
            this.cmbHotKeyNames.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label2.Location = new System.Drawing.Point(302, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target";
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
            this.btnCancel.Location = new System.Drawing.Point(263, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnCancel.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnCancel.Size = new System.Drawing.Size(121, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnCancel.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCancel.ToggledOn = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddHotkey
            // 
            this.btnAddHotkey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddHotkey.ClipBackground = false;
            this.btnAddHotkey.DelayFrameRefresh = false;
            this.btnAddHotkey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnAddHotkey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnAddHotkey.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnAddHotkey.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnAddHotkey.IntensifyColors = false;
            this.btnAddHotkey.Location = new System.Drawing.Point(263, 54);
            this.btnAddHotkey.Name = "btnAddHotkey";
            this.btnAddHotkey.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnAddHotkey.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnAddHotkey.Size = new System.Drawing.Size(121, 23);
            this.btnAddHotkey.TabIndex = 6;
            this.btnAddHotkey.Text = "Add";
            this.btnAddHotkey.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnAddHotkey.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAddHotkey.ToggledOn = false;
            this.btnAddHotkey.Click += new System.EventHandler(this.btnAddHotkey_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label1.Location = new System.Drawing.Point(110, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Hotkeys";
            // 
            // lstbHotkeys
            // 
            this.lstbHotkeys.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.lstbHotkeys.DataSource = this.bindingSourceSelectedHotKeys;
            this.lstbHotkeys.FormattingEnabled = true;
            this.lstbHotkeys.Location = new System.Drawing.Point(12, 27);
            this.lstbHotkeys.Name = "lstbHotkeys";
            this.lstbHotkeys.Size = new System.Drawing.Size(245, 199);
            this.lstbHotkeys.TabIndex = 8;
            // 
            // btnRemoveHotKey
            // 
            this.btnRemoveHotKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveHotKey.ClipBackground = false;
            this.btnRemoveHotKey.DelayFrameRefresh = false;
            this.btnRemoveHotKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnRemoveHotKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRemoveHotKey.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRemoveHotKey.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRemoveHotKey.IntensifyColors = false;
            this.btnRemoveHotKey.Location = new System.Drawing.Point(263, 83);
            this.btnRemoveHotKey.Name = "btnRemoveHotKey";
            this.btnRemoveHotKey.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRemoveHotKey.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRemoveHotKey.Size = new System.Drawing.Size(121, 23);
            this.btnRemoveHotKey.TabIndex = 10;
            this.btnRemoveHotKey.Text = "Remove";
            this.btnRemoveHotKey.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRemoveHotKey.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRemoveHotKey.ToggledOn = false;
            this.btnRemoveHotKey.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMoveUp.ClipBackground = false;
            this.btnMoveUp.DelayFrameRefresh = false;
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnMoveUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMoveUp.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnMoveUp.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnMoveUp.IntensifyColors = false;
            this.btnMoveUp.Location = new System.Drawing.Point(263, 170);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveUp.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveUp.Size = new System.Drawing.Size(121, 23);
            this.btnMoveUp.TabIndex = 11;
            this.btnMoveUp.Text = "Move up";
            this.btnMoveUp.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnMoveUp.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMoveUp.ToggledOn = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMoveDown.ClipBackground = false;
            this.btnMoveDown.DelayFrameRefresh = false;
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnMoveDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMoveDown.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnMoveDown.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnMoveDown.IntensifyColors = false;
            this.btnMoveDown.Location = new System.Drawing.Point(264, 199);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveDown.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveDown.Size = new System.Drawing.Size(121, 23);
            this.btnMoveDown.TabIndex = 12;
            this.btnMoveDown.Text = "Move down";
            this.btnMoveDown.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnMoveDown.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMoveDown.ToggledOn = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSave.ClipBackground = false;
            this.btnSave.DelayFrameRefresh = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnSave.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnSave.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnSave.IntensifyColors = false;
            this.btnSave.Location = new System.Drawing.Point(263, 112);
            this.btnSave.Name = "btnSave";
            this.btnSave.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnSave.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnSave.Size = new System.Drawing.Size(121, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnSave.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSave.ToggledOn = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fAddNewHotkey
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(394, 233);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnRemoveHotKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstbHotkeys);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddHotkey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbHotKeyNames);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fAddNewHotkey";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "fAddNewHotkey";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceHotKeyList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSelectedHotKeys)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbHotKeyNames;
        private System.Windows.Forms.Label label2;
        private DistantWorlds.Controls.GlassButton btnCancel;
        private DistantWorlds.Controls.GlassButton btnAddHotkey;
        private System.Windows.Forms.BindingSource bindingSourceHotKeyList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstbHotkeys;
        private DistantWorlds.Controls.GlassButton btnRemoveHotKey;
        private DistantWorlds.Controls.GlassButton btnMoveUp;
        private DistantWorlds.Controls.GlassButton btnMoveDown;
        private System.Windows.Forms.BindingSource bindingSourceSelectedHotKeys;
        private DistantWorlds.Controls.GlassButton btnSave;
    }
}