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
            this.lblCurrentMission = new System.Windows.Forms.Label();
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
            this.cmbOtherConstructShips = new System.Windows.Forms.ComboBox();
            this.bindingSourceAllConstrShips = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.btnAssignOtherShip = new DistantWorlds.Controls.GlassButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRetrofit = new DistantWorlds.Controls.GlassButton();
            this.btnMoveToBottom = new DistantWorlds.Controls.GlassButton();
            this.btnMoveToTop = new DistantWorlds.Controls.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceQueuedMissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAllConstrShips)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCurrentMission
            // 
            this.lblCurrentMission.AutoSize = true;
            this.lblCurrentMission.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentMission.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblCurrentMission.Location = new System.Drawing.Point(7, 23);
            this.lblCurrentMission.Name = "lblCurrentMission";
            this.lblCurrentMission.Size = new System.Drawing.Size(210, 16);
            this.lblCurrentMission.TabIndex = 0;
            this.lblCurrentMission.Text = "Current mission name placeholder";
            // 
            // missionList
            // 
            this.missionList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.missionList.DataSource = this.bindingSourceQueuedMissions;
            this.missionList.DisplayMember = "Name";
            this.missionList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.missionList.FormattingEnabled = true;
            this.missionList.ItemHeight = 16;
            this.missionList.Location = new System.Drawing.Point(11, 30);
            this.missionList.Name = "missionList";
            this.missionList.Size = new System.Drawing.Size(332, 372);
            this.missionList.TabIndex = 3;
            // 
            // bindingSourceQueuedMissions
            // 
            this.bindingSourceQueuedMissions.AllowNew = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label1.Location = new System.Drawing.Point(76, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Quequed missions";
            // 
            // btnSwap
            // 
            this.btnSwap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSwap.ClipBackground = false;
            this.btnSwap.DelayFrameRefresh = false;
            this.btnSwap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSwap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnSwap.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnSwap.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnSwap.IntensifyColors = false;
            this.btnSwap.Location = new System.Drawing.Point(10, 49);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnSwap.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnSwap.Size = new System.Drawing.Size(212, 23);
            this.btnSwap.TabIndex = 9;
            this.btnSwap.Text = "Swap selected with current";
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
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoveUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMoveUp.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnMoveUp.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnMoveUp.IntensifyColors = false;
            this.btnMoveUp.Location = new System.Drawing.Point(10, 19);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveUp.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveUp.Size = new System.Drawing.Size(142, 23);
            this.btnMoveUp.TabIndex = 10;
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
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoveDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMoveDown.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnMoveDown.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnMoveDown.IntensifyColors = false;
            this.btnMoveDown.Location = new System.Drawing.Point(179, 19);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveDown.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveDown.Size = new System.Drawing.Size(142, 23);
            this.btnMoveDown.TabIndex = 11;
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
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnSave.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnSave.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnSave.IntensifyColors = false;
            this.btnSave.Location = new System.Drawing.Point(10, 167);
            this.btnSave.Name = "btnSave";
            this.btnSave.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnSave.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnSave.Size = new System.Drawing.Size(142, 23);
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
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnCancel.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnCancel.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnCancel.IntensifyColors = false;
            this.btnCancel.Location = new System.Drawing.Point(179, 167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnCancel.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnCancel.Size = new System.Drawing.Size(142, 23);
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
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRemove.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRemove.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRemove.IntensifyColors = false;
            this.btnRemove.Location = new System.Drawing.Point(10, 88);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRemove.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRemove.Size = new System.Drawing.Size(142, 23);
            this.btnRemove.TabIndex = 14;
            this.btnRemove.Text = "Remove current";
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
            this.btnRestoreMissions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRestoreMissions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRestoreMissions.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRestoreMissions.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRestoreMissions.IntensifyColors = false;
            this.btnRestoreMissions.Location = new System.Drawing.Point(179, 88);
            this.btnRestoreMissions.Name = "btnRestoreMissions";
            this.btnRestoreMissions.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRestoreMissions.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRestoreMissions.Size = new System.Drawing.Size(142, 23);
            this.btnRestoreMissions.TabIndex = 15;
            this.btnRestoreMissions.Text = "Restore";
            this.btnRestoreMissions.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRestoreMissions.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRestoreMissions.ToggledOn = false;
            this.btnRestoreMissions.Click += new System.EventHandler(this.btnRestoreMissions_Click);
            // 
            // cmbOtherConstructShips
            // 
            this.cmbOtherConstructShips.DataSource = this.bindingSourceAllConstrShips;
            this.cmbOtherConstructShips.DisplayMember = "Name";
            this.cmbOtherConstructShips.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOtherConstructShips.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbOtherConstructShips.FormattingEnabled = true;
            this.cmbOtherConstructShips.Location = new System.Drawing.Point(10, 40);
            this.cmbOtherConstructShips.Name = "cmbOtherConstructShips";
            this.cmbOtherConstructShips.Size = new System.Drawing.Size(311, 21);
            this.cmbOtherConstructShips.TabIndex = 16;
            // 
            // bindingSourceAllConstrShips
            // 
            this.bindingSourceAllConstrShips.AllowNew = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.label3.Location = new System.Drawing.Point(94, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Available ships";
            // 
            // btnAssignOtherShip
            // 
            this.btnAssignOtherShip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAssignOtherShip.ClipBackground = false;
            this.btnAssignOtherShip.DelayFrameRefresh = false;
            this.btnAssignOtherShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAssignOtherShip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnAssignOtherShip.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnAssignOtherShip.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnAssignOtherShip.IntensifyColors = false;
            this.btnAssignOtherShip.Location = new System.Drawing.Point(9, 67);
            this.btnAssignOtherShip.Name = "btnAssignOtherShip";
            this.btnAssignOtherShip.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnAssignOtherShip.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnAssignOtherShip.Size = new System.Drawing.Size(312, 23);
            this.btnAssignOtherShip.TabIndex = 18;
            this.btnAssignOtherShip.Text = "Move selected mission to other ship queue";
            this.btnAssignOtherShip.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnAssignOtherShip.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAssignOtherShip.ToggledOn = false;
            this.btnAssignOtherShip.Click += new System.EventHandler(this.btnAssignOtherShip_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btnSwap);
            this.groupBox1.Controls.Add(this.lblCurrentMission);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.groupBox1.Location = new System.Drawing.Point(350, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 91);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current mission";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbOtherConstructShips);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnAssignOtherShip);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.groupBox2.Location = new System.Drawing.Point(350, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 96);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Assign to other ship";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRetrofit);
            this.groupBox3.Controls.Add(this.btnMoveToBottom);
            this.groupBox3.Controls.Add(this.btnMoveToTop);
            this.groupBox3.Controls.Add(this.btnMoveUp);
            this.groupBox3.Controls.Add(this.btnMoveDown);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnRestoreMissions);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnRemove);
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.groupBox3.Location = new System.Drawing.Point(350, 201);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 201);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mission";
            // 
            // btnRetrofit
            // 
            this.btnRetrofit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRetrofit.ClipBackground = false;
            this.btnRetrofit.DelayFrameRefresh = false;
            this.btnRetrofit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRetrofit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnRetrofit.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnRetrofit.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnRetrofit.IntensifyColors = false;
            this.btnRetrofit.Location = new System.Drawing.Point(179, 117);
            this.btnRetrofit.Name = "btnRetrofit";
            this.btnRetrofit.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnRetrofit.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnRetrofit.Size = new System.Drawing.Size(142, 23);
            this.btnRetrofit.TabIndex = 18;
            this.btnRetrofit.Text = "Retrofit";
            this.btnRetrofit.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnRetrofit.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRetrofit.ToggledOn = false;
            this.btnRetrofit.Click += new System.EventHandler(this.btnRetrofit_Click);
            // 
            // btnMoveToBottom
            // 
            this.btnMoveToBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMoveToBottom.ClipBackground = false;
            this.btnMoveToBottom.DelayFrameRefresh = false;
            this.btnMoveToBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoveToBottom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMoveToBottom.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnMoveToBottom.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnMoveToBottom.IntensifyColors = false;
            this.btnMoveToBottom.Location = new System.Drawing.Point(179, 48);
            this.btnMoveToBottom.Name = "btnMoveToBottom";
            this.btnMoveToBottom.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveToBottom.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveToBottom.Size = new System.Drawing.Size(142, 23);
            this.btnMoveToBottom.TabIndex = 17;
            this.btnMoveToBottom.Text = "Move to bottom";
            this.btnMoveToBottom.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnMoveToBottom.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMoveToBottom.ToggledOn = false;
            this.btnMoveToBottom.Click += new System.EventHandler(this.btnMoveToBottom_Click);
            // 
            // btnMoveToTop
            // 
            this.btnMoveToTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMoveToTop.ClipBackground = false;
            this.btnMoveToTop.DelayFrameRefresh = false;
            this.btnMoveToTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoveToTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnMoveToTop.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(128)))));
            this.btnMoveToTop.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(77)))));
            this.btnMoveToTop.IntensifyColors = false;
            this.btnMoveToTop.Location = new System.Drawing.Point(10, 48);
            this.btnMoveToTop.Name = "btnMoveToTop";
            this.btnMoveToTop.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16)))));
            this.btnMoveToTop.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.btnMoveToTop.Size = new System.Drawing.Size(142, 23);
            this.btnMoveToTop.TabIndex = 16;
            this.btnMoveToTop.Text = "Move to top";
            this.btnMoveToTop.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnMoveToTop.TextColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMoveToTop.ToggledOn = false;
            this.btnMoveToTop.Click += new System.EventHandler(this.btnMoveToTop_Click);
            // 
            // MissionQueueEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(694, 413);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.missionList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MissionQueueEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MissionQueueEditor";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceQueuedMissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAllConstrShips)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentMission;
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
        private System.Windows.Forms.ComboBox cmbOtherConstructShips;
        private System.Windows.Forms.Label label3;
        private DistantWorlds.Controls.GlassButton btnAssignOtherShip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.BindingSource bindingSourceAllConstrShips;
        private DistantWorlds.Controls.GlassButton btnMoveToBottom;
        private DistantWorlds.Controls.GlassButton btnMoveToTop;
        private DistantWorlds.Controls.GlassButton btnRetrofit;
    }
}