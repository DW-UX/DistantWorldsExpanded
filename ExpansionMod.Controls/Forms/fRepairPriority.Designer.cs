namespace ExpansionMod.Controls.Forms
{
    partial class fRepairPriority
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
            this.btnSaveSelect = new DistantWorlds.Controls.GlassButton();
            this.btnMoveDown = new DistantWorlds.Controls.GlassButton();
            this.bindingSourceRepairPriorityTemplates = new System.Windows.Forms.BindingSource(this.components);
            this.btnMoveUp = new DistantWorlds.Controls.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lstbRepairPriority = new System.Windows.Forms.ListBox();
            this.bindingSourceRepairPriority = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new DistantWorlds.Controls.GlassButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTempalteList = new System.Windows.Forms.ComboBox();
            this.btnRemoveTemplate = new DistantWorlds.Controls.GlassButton();
            this.btnAddNewTemplate = new DistantWorlds.Controls.GlassButton();
            this.btnRename = new DistantWorlds.Controls.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRepairPriorityTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRepairPriority)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveSelect
            // 
            this.btnSaveSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSaveSelect.ClipBackground = false;
            this.btnSaveSelect.DelayFrameRefresh = false;
            this.btnSaveSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnSaveSelect.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnSaveSelect.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnSaveSelect.IntensifyColors = false;
            this.btnSaveSelect.Location = new System.Drawing.Point(262, 116);
            this.btnSaveSelect.Name = "btnSaveSelect";
            this.btnSaveSelect.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnSaveSelect.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnSaveSelect.Size = new System.Drawing.Size(131, 23);
            this.btnSaveSelect.TabIndex = 19;
            this.btnSaveSelect.Text = "Save and select";
            this.btnSaveSelect.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnSaveSelect.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSaveSelect.ToggledOn = false;
            this.btnSaveSelect.Click += new System.EventHandler(this.btnSaveSelect_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMoveDown.ClipBackground = false;
            this.btnMoveDown.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingSourceRepairPriorityTemplates, "UserGenerated", true));
            this.btnMoveDown.DelayFrameRefresh = false;
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoveDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMoveDown.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnMoveDown.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnMoveDown.IntensifyColors = false;
            this.btnMoveDown.Location = new System.Drawing.Point(263, 203);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveDown.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveDown.Size = new System.Drawing.Size(130, 23);
            this.btnMoveDown.TabIndex = 18;
            this.btnMoveDown.Text = "Move down";
            this.btnMoveDown.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnMoveDown.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMoveDown.ToggledOn = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // bindingSourceRepairPriorityTemplates
            // 
            this.bindingSourceRepairPriorityTemplates.DataSource = typeof(ExpansionMod.Objects.RepairPriority);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMoveUp.ClipBackground = false;
            this.btnMoveUp.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingSourceRepairPriorityTemplates, "UserGenerated", true));
            this.btnMoveUp.DelayFrameRefresh = false;
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoveUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMoveUp.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnMoveUp.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnMoveUp.IntensifyColors = false;
            this.btnMoveUp.Location = new System.Drawing.Point(262, 174);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveUp.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveUp.Size = new System.Drawing.Size(130, 23);
            this.btnMoveUp.TabIndex = 17;
            this.btnMoveUp.Text = "Move up";
            this.btnMoveUp.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnMoveUp.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMoveUp.ToggledOn = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label1.Location = new System.Drawing.Point(110, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Hotkeys";
            // 
            // lstbRepairPriority
            // 
            this.lstbRepairPriority.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.lstbRepairPriority.DataSource = this.bindingSourceRepairPriority;
            this.lstbRepairPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstbRepairPriority.FormattingEnabled = true;
            this.lstbRepairPriority.ItemHeight = 16;
            this.lstbRepairPriority.Location = new System.Drawing.Point(12, 28);
            this.lstbRepairPriority.Name = "lstbRepairPriority";
            this.lstbRepairPriority.Size = new System.Drawing.Size(245, 228);
            this.lstbRepairPriority.TabIndex = 15;
            // 
            // bindingSourceRepairPriority
            // 
            this.bindingSourceRepairPriority.DataMember = "Priority";
            this.bindingSourceRepairPriority.DataSource = this.bindingSourceRepairPriorityTemplates;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.ClipBackground = false;
            this.btnCancel.DelayFrameRefresh = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnCancel.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnCancel.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnCancel.IntensifyColors = false;
            this.btnCancel.Location = new System.Drawing.Point(262, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnCancel.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnCancel.Size = new System.Drawing.Size(130, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnCancel.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCancel.ToggledOn = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label2.Location = new System.Drawing.Point(267, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Current template";
            // 
            // cmbTempalteList
            // 
            this.cmbTempalteList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.cmbTempalteList.DataSource = this.bindingSourceRepairPriorityTemplates;
            this.cmbTempalteList.DisplayMember = "TemplateName";
            this.cmbTempalteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTempalteList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbTempalteList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.cmbTempalteList.FormattingEnabled = true;
            this.cmbTempalteList.Location = new System.Drawing.Point(263, 28);
            this.cmbTempalteList.Name = "cmbTempalteList";
            this.cmbTempalteList.Size = new System.Drawing.Size(129, 24);
            this.cmbTempalteList.TabIndex = 20;
            this.cmbTempalteList.Validated += new System.EventHandler(this.cmbTempalteList_Validated);
            // 
            // btnRemoveTemplate
            // 
            this.btnRemoveTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveTemplate.ClipBackground = false;
            this.btnRemoveTemplate.DelayFrameRefresh = false;
            this.btnRemoveTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRemoveTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRemoveTemplate.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRemoveTemplate.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRemoveTemplate.IntensifyColors = false;
            this.btnRemoveTemplate.Location = new System.Drawing.Point(263, 87);
            this.btnRemoveTemplate.Name = "btnRemoveTemplate";
            this.btnRemoveTemplate.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRemoveTemplate.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRemoveTemplate.Size = new System.Drawing.Size(130, 23);
            this.btnRemoveTemplate.TabIndex = 23;
            this.btnRemoveTemplate.Text = "Remove cur.";
            this.btnRemoveTemplate.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRemoveTemplate.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRemoveTemplate.ToggledOn = false;
            this.btnRemoveTemplate.Click += new System.EventHandler(this.btnRemoveTemplate_Click);
            // 
            // btnAddNewTemplate
            // 
            this.btnAddNewTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddNewTemplate.ClipBackground = false;
            this.btnAddNewTemplate.DelayFrameRefresh = false;
            this.btnAddNewTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddNewTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnAddNewTemplate.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnAddNewTemplate.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnAddNewTemplate.IntensifyColors = false;
            this.btnAddNewTemplate.Location = new System.Drawing.Point(263, 58);
            this.btnAddNewTemplate.Name = "btnAddNewTemplate";
            this.btnAddNewTemplate.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnAddNewTemplate.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnAddNewTemplate.Size = new System.Drawing.Size(130, 23);
            this.btnAddNewTemplate.TabIndex = 22;
            this.btnAddNewTemplate.Text = "Add new";
            this.btnAddNewTemplate.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnAddNewTemplate.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAddNewTemplate.ToggledOn = false;
            this.btnAddNewTemplate.Click += new System.EventHandler(this.btnAddNewTemplate_Click);
            // 
            // btnRename
            // 
            this.btnRename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRename.ClipBackground = false;
            this.btnRename.DelayFrameRefresh = false;
            this.btnRename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRename.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRename.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRename.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRename.IntensifyColors = false;
            this.btnRename.Location = new System.Drawing.Point(263, 232);
            this.btnRename.Name = "btnRename";
            this.btnRename.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRename.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRename.Size = new System.Drawing.Size(130, 23);
            this.btnRename.TabIndex = 24;
            this.btnRename.Text = "Rename";
            this.btnRename.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRename.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRename.ToggledOn = false;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // fRepairPriority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(399, 268);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnRemoveTemplate);
            this.Controls.Add(this.btnAddNewTemplate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTempalteList);
            this.Controls.Add(this.btnSaveSelect);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstbRepairPriority);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fRepairPriority";
            this.ShowInTaskbar = false;
            this.Text = "fRepairPriority";
            this.Load += new System.EventHandler(this.fRepairPriority_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRepairPriorityTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRepairPriority)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DistantWorlds.Controls.GlassButton btnSaveSelect;
        private DistantWorlds.Controls.GlassButton btnMoveDown;
        private DistantWorlds.Controls.GlassButton btnMoveUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstbRepairPriority;
        private DistantWorlds.Controls.GlassButton btnCancel;
        private System.Windows.Forms.BindingSource bindingSourceRepairPriority;
        private System.Windows.Forms.BindingSource bindingSourceRepairPriorityTemplates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTempalteList;
        private DistantWorlds.Controls.GlassButton btnRemoveTemplate;
        private DistantWorlds.Controls.GlassButton btnAddNewTemplate;
        private DistantWorlds.Controls.GlassButton btnRename;
    }
}