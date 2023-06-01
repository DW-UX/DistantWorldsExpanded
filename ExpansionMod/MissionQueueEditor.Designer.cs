namespace ExpansionMod
{
    partial class MissionQueueEditor
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
            this.currentMissionText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.missionList = new System.Windows.Forms.ListBox();
            this.bindingSourceQueuedMissions = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnSwap = new DistantWorlds.Controls.GlassButton();
            this.btnMoveUp = new DistantWorlds.Controls.GlassButton();
            this.btnMoveDown = new DistantWorlds.Controls.GlassButton();
            this.btnSave = new DistantWorlds.Controls.GlassButton();
            this.btnCancel = new DistantWorlds.Controls.GlassButton();
            this.btnRemove = new DistantWorlds.Controls.GlassButton();
            this.btnRestoreMissions = new DistantWorlds.Controls.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceQueuedMissions)).BeginInit();
            this.SuspendLayout();
            // 
            // currentMissionText
            // 
            this.currentMissionText.AutoSize = true;
            this.currentMissionText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.currentMissionText.Location = new System.Drawing.Point(257, 29);
            this.currentMissionText.Name = "currentMissionText";
            this.currentMissionText.Size = new System.Drawing.Size(78, 13);
            this.currentMissionText.TabIndex = 0;
            this.currentMissionText.Text = "Current mission";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label2.Location = new System.Drawing.Point(257, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // missionList
            // 
            this.missionList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.missionList.DataSource = this.bindingSourceQueuedMissions;
            this.missionList.DisplayMember = "Name";
            this.missionList.FormattingEnabled = true;
            this.missionList.Location = new System.Drawing.Point(12, 29);
            this.missionList.Name = "missionList";
            this.missionList.Size = new System.Drawing.Size(239, 225);
            this.missionList.TabIndex = 3;
            // 
            // bindingSourceQueuedMissions
            // 
            this.bindingSourceQueuedMissions.DataSource = typeof(ExpansionMod.ListMissionObject);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label1.Location = new System.Drawing.Point(76, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Quequed missions";
            // 
            // btnSwap
            // 
            this.btnSwap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSwap.ClipBackground = false;
            this.btnSwap.DelayFrameRefresh = false;
            this.btnSwap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnSwap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnSwap.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnSwap.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnSwap.IntensifyColors = false;
            this.btnSwap.Location = new System.Drawing.Point(257, 69);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnSwap.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnSwap.Size = new System.Drawing.Size(176, 23);
            this.btnSwap.TabIndex = 9;
            this.btnSwap.Text = "Swap selected";
            this.btnSwap.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnSwap.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSwap.ToggledOn = false;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
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
            this.btnMoveUp.Location = new System.Drawing.Point(258, 131);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveUp.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUp.TabIndex = 10;
            this.btnMoveUp.Text = "Up";
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
            this.btnMoveDown.Location = new System.Drawing.Point(258, 160);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveDown.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDown.TabIndex = 11;
            this.btnMoveDown.Text = "Down";
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
            this.btnSave.Location = new System.Drawing.Point(258, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnSave.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnSave.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSave.ToggledOn = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.ClipBackground = false;
            this.btnCancel.DelayFrameRefresh = false;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnCancel.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnCancel.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnCancel.IntensifyColors = false;
            this.btnCancel.Location = new System.Drawing.Point(359, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnCancel.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnCancel.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCancel.ToggledOn = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemove.ClipBackground = false;
            this.btnRemove.DelayFrameRefresh = false;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRemove.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRemove.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRemove.IntensifyColors = false;
            this.btnRemove.Location = new System.Drawing.Point(359, 131);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRemove.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 14;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRemove.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRemove.ToggledOn = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnRestoreMissions
            // 
            this.btnRestoreMissions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRestoreMissions.ClipBackground = false;
            this.btnRestoreMissions.DelayFrameRefresh = false;
            this.btnRestoreMissions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnRestoreMissions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRestoreMissions.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRestoreMissions.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRestoreMissions.IntensifyColors = false;
            this.btnRestoreMissions.Location = new System.Drawing.Point(359, 160);
            this.btnRestoreMissions.Name = "btnRestoreMissions";
            this.btnRestoreMissions.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRestoreMissions.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRestoreMissions.Size = new System.Drawing.Size(75, 23);
            this.btnRestoreMissions.TabIndex = 15;
            this.btnRestoreMissions.Text = "Restore";
            this.btnRestoreMissions.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRestoreMissions.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRestoreMissions.ToggledOn = false;
            this.btnRestoreMissions.Click += new System.EventHandler(this.btnRestoreMissions_Click);
            // 
            // MissionQueueEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(465, 263);
            this.Controls.Add(this.btnRestoreMissions);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.missionList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.currentMissionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MissionQueueEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MissionQueueEditor";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceQueuedMissions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label currentMissionText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox missionList;
        private System.Windows.Forms.BindingSource bindingSourceQueuedMissions;
        private System.Windows.Forms.Label label1;
        private DistantWorlds.Controls.GlassButton btnSwap;
        private DistantWorlds.Controls.GlassButton btnMoveUp;
        private DistantWorlds.Controls.GlassButton btnMoveDown;
        private DistantWorlds.Controls.GlassButton btnSave;
        private DistantWorlds.Controls.GlassButton btnCancel;
        private DistantWorlds.Controls.GlassButton btnRemove;
        private DistantWorlds.Controls.GlassButton btnRestoreMissions;
    }
}