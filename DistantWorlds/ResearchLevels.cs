// Decompiled with JetBrains decompiler
// Type: DistantWorlds.ResearchLevels
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Controls;
using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DistantWorlds
{
    public class ResearchLevels : UserControl
    {
        private Main main_0;

        private ResearchAreaList researchAreaList_0;

        private IContainer icontainer_0;

        private ResearchLevelSlider resBeamWeapon;

        private ResearchLevelSlider kabbfiXxi;

        private ResearchLevelSlider resAreaWeapon;

        private ResearchLevelSlider resArmor;

        private ResearchLevelSlider resShields;

        private ResearchLevelSlider resEngine;

        private ResearchLevelSlider resHyperDrive;

        private ResearchLevelSlider resReactor;

        private ResearchLevelSlider resEnergyCollector;

        private ResearchLevelSlider resExtractor;

        private ResearchLevelSlider resManufacturer;

        private ResearchLevelSlider resStorage;

        private ResearchLevelSlider resSensor;

        private ResearchLevelSlider resComputer;

        private ResearchLevelSlider resLabs;

        private ResearchLevelSlider resConstruction;

        private ResearchLevelSlider resHabitation;

        private Label lblProgressTitle;

        private Label lblLatestComponentTitle;

        private Label lblAreaTitle;

        public ResearchAreaList ResearchAreas => researchAreaList_0;

        public ResearchLevels():base()
        {
            Class7.VEFSJNszvZKMZ();
            InitializeComponent();
        }

        private void method_0()
        {
            foreach (Control control in base.Controls)
            {
                control.ForeColor = Color.FromArgb(170, 170, 170);
            }
        }

        public void LayoutControls()
        {
            int num = base.Width - 20;
            SuspendLayout();
            method_0();
            lblAreaTitle.Font = new Font(lblAreaTitle.Font, FontStyle.Bold);
            lblProgressTitle.Font = new Font(lblProgressTitle.Font, FontStyle.Bold);
            lblLatestComponentTitle.Font = new Font(lblLatestComponentTitle.Font, FontStyle.Bold);
            lblAreaTitle.Location = new Point(10, 10);
            lblProgressTitle.Location = new Point(150, 10);
            lblLatestComponentTitle.Location = new Point(400, 10);
            resBeamWeapon.Size = new Size(num, 24);
            kabbfiXxi.Size = new Size(num, 24);
            resAreaWeapon.Size = new Size(num, 24);
            resArmor.Size = new Size(num, 24);
            resShields.Size = new Size(num, 24);
            resEngine.Size = new Size(num, 24);
            resHyperDrive.Size = new Size(num, 24);
            resReactor.Size = new Size(num, 24);
            resEnergyCollector.Size = new Size(num, 24);
            resExtractor.Size = new Size(num, 24);
            resManufacturer.Size = new Size(num, 24);
            resStorage.Size = new Size(num, 24);
            resSensor.Size = new Size(num, 24);
            resComputer.Size = new Size(num, 24);
            resLabs.Size = new Size(num, 24);
            resConstruction.Size = new Size(num, 24);
            resHabitation.Size = new Size(num, 24);
            resBeamWeapon.Location = new Point(10, 36);
            kabbfiXxi.Location = new Point(10, 63);
            resAreaWeapon.Location = new Point(10, 90);
            resArmor.Location = new Point(10, 117);
            resShields.Location = new Point(10, 144);
            resEngine.Location = new Point(10, 171);
            resHyperDrive.Location = new Point(10, 198);
            resReactor.Location = new Point(10, 225);
            resEnergyCollector.Location = new Point(10, 252);
            resExtractor.Location = new Point(10, 279);
            resManufacturer.Location = new Point(10, 306);
            resStorage.Location = new Point(10, 333);
            resSensor.Location = new Point(10, 360);
            resComputer.Location = new Point(10, 387);
            resLabs.Location = new Point(10, 414);
            resConstruction.Location = new Point(10, 441);
            resHabitation.Location = new Point(10, 468);
            ResumeLayout();
        }

        public void ClearData()
        {
            researchAreaList_0 = null;
            resAreaWeapon.ClearData();
            resArmor.ClearData();
            resBeamWeapon.ClearData();
            resComputer.ClearData();
            resConstruction.ClearData();
            resEnergyCollector.ClearData();
            resEngine.ClearData();
            resExtractor.ClearData();
            resHabitation.ClearData();
            resHyperDrive.ClearData();
            resLabs.ClearData();
            resManufacturer.ClearData();
            resReactor.ClearData();
            resSensor.ClearData();
            resShields.ClearData();
            resStorage.ClearData();
            kabbfiXxi.ClearData();
        }

        public void BindData(Main parentForm, ResearchAreaList researchAreas)
        {
            main_0 = parentForm;
            researchAreaList_0 = researchAreas;
            LayoutControls();
            SuspendLayout();
            resBeamWeapon.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.WeaponBeam));
            kabbfiXxi.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.WeaponTorpedo));
            resAreaWeapon.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.WeaponArea));
            resArmor.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Armor));
            resShields.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Shields));
            resEngine.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Engine));
            resHyperDrive.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.HyperDrive));
            resReactor.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Reactor));
            resEnergyCollector.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.EnergyCollector));
            resExtractor.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Extractor));
            resManufacturer.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Manufacturer));
            resStorage.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Storage));
            resSensor.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Sensor));
            resComputer.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Computer));
            resLabs.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Labs));
            resConstruction.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Construction));
            resHabitation.BindData(parentForm, researchAreaList_0.GetByCategory(ComponentCategoryType.Habitation));
            ResumeLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && icontainer_0 != null)
            {
                icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.resBeamWeapon = new DistantWorlds.Controls.ResearchLevelSlider();
            this.kabbfiXxi = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resAreaWeapon = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resArmor = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resShields = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resEngine = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resHyperDrive = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resReactor = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resEnergyCollector = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resExtractor = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resManufacturer = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resStorage = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resSensor = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resComputer = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resLabs = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resConstruction = new DistantWorlds.Controls.ResearchLevelSlider();
            this.resHabitation = new DistantWorlds.Controls.ResearchLevelSlider();
            this.lblProgressTitle = new System.Windows.Forms.Label();
            this.lblLatestComponentTitle = new System.Windows.Forms.Label();
            this.lblAreaTitle = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this.resBeamWeapon.BackColor = System.Drawing.Color.Transparent;
            this.resBeamWeapon.Location = new System.Drawing.Point(16, 42);
            this.resBeamWeapon.Name = "resBeamWeapon";
            this.resBeamWeapon.Size = new System.Drawing.Size(483, 20);
            this.resBeamWeapon.TabIndex = 0;
            this.kabbfiXxi.BackColor = System.Drawing.Color.Transparent;
            this.kabbfiXxi.Location = new System.Drawing.Point(16, 68);
            this.kabbfiXxi.Name = "resTorpedoWeapon";
            this.kabbfiXxi.Size = new System.Drawing.Size(483, 20);
            this.kabbfiXxi.TabIndex = 1;
            this.resAreaWeapon.BackColor = System.Drawing.Color.Transparent;
            this.resAreaWeapon.Location = new System.Drawing.Point(16, 94);
            this.resAreaWeapon.Name = "resAreaWeapon";
            this.resAreaWeapon.Size = new System.Drawing.Size(563, 20);
            this.resAreaWeapon.TabIndex = 2;
            this.resArmor.BackColor = System.Drawing.Color.Transparent;
            this.resArmor.Location = new System.Drawing.Point(16, 120);
            this.resArmor.Name = "resArmor";
            this.resArmor.Size = new System.Drawing.Size(563, 20);
            this.resArmor.TabIndex = 3;
            this.resShields.BackColor = System.Drawing.Color.Transparent;
            this.resShields.Location = new System.Drawing.Point(16, 146);
            this.resShields.Name = "resShields";
            this.resShields.Size = new System.Drawing.Size(563, 20);
            this.resShields.TabIndex = 4;
            this.resEngine.BackColor = System.Drawing.Color.Transparent;
            this.resEngine.Location = new System.Drawing.Point(16, 172);
            this.resEngine.Name = "resEngine";
            this.resEngine.Size = new System.Drawing.Size(563, 20);
            this.resEngine.TabIndex = 5;
            this.resHyperDrive.BackColor = System.Drawing.Color.Transparent;
            this.resHyperDrive.Location = new System.Drawing.Point(16, 198);
            this.resHyperDrive.Name = "resHyperDrive";
            this.resHyperDrive.Size = new System.Drawing.Size(563, 20);
            this.resHyperDrive.TabIndex = 6;
            this.resReactor.BackColor = System.Drawing.Color.Transparent;
            this.resReactor.Location = new System.Drawing.Point(16, 224);
            this.resReactor.Name = "resReactor";
            this.resReactor.Size = new System.Drawing.Size(563, 20);
            this.resReactor.TabIndex = 7;
            this.resEnergyCollector.BackColor = System.Drawing.Color.Transparent;
            this.resEnergyCollector.Location = new System.Drawing.Point(16, 250);
            this.resEnergyCollector.Name = "resEnergyCollector";
            this.resEnergyCollector.Size = new System.Drawing.Size(563, 20);
            this.resEnergyCollector.TabIndex = 8;
            this.resExtractor.BackColor = System.Drawing.Color.Transparent;
            this.resExtractor.Location = new System.Drawing.Point(16, 276);
            this.resExtractor.Name = "resExtractor";
            this.resExtractor.Size = new System.Drawing.Size(563, 20);
            this.resExtractor.TabIndex = 9;
            this.resManufacturer.BackColor = System.Drawing.Color.Transparent;
            this.resManufacturer.Location = new System.Drawing.Point(16, 302);
            this.resManufacturer.Name = "resManufacturer";
            this.resManufacturer.Size = new System.Drawing.Size(563, 20);
            this.resManufacturer.TabIndex = 10;
            this.resStorage.BackColor = System.Drawing.Color.Transparent;
            this.resStorage.Location = new System.Drawing.Point(16, 328);
            this.resStorage.Name = "resStorage";
            this.resStorage.Size = new System.Drawing.Size(563, 20);
            this.resStorage.TabIndex = 11;
            this.resSensor.BackColor = System.Drawing.Color.Transparent;
            this.resSensor.Location = new System.Drawing.Point(16, 354);
            this.resSensor.Name = "resSensor";
            this.resSensor.Size = new System.Drawing.Size(563, 20);
            this.resSensor.TabIndex = 12;
            this.resComputer.BackColor = System.Drawing.Color.Transparent;
            this.resComputer.Location = new System.Drawing.Point(16, 380);
            this.resComputer.Name = "resComputer";
            this.resComputer.Size = new System.Drawing.Size(563, 20);
            this.resComputer.TabIndex = 13;
            this.resLabs.BackColor = System.Drawing.Color.Transparent;
            this.resLabs.Location = new System.Drawing.Point(16, 406);
            this.resLabs.Name = "resLabs";
            this.resLabs.Size = new System.Drawing.Size(563, 20);
            this.resLabs.TabIndex = 14;
            this.resConstruction.BackColor = System.Drawing.Color.Transparent;
            this.resConstruction.Location = new System.Drawing.Point(16, 432);
            this.resConstruction.Name = "resConstruction";
            this.resConstruction.Size = new System.Drawing.Size(563, 20);
            this.resConstruction.TabIndex = 15;
            this.resHabitation.BackColor = System.Drawing.Color.Transparent;
            this.resHabitation.Location = new System.Drawing.Point(15, 458);
            this.resHabitation.Name = "resHabitation";
            this.resHabitation.Size = new System.Drawing.Size(563, 20);
            this.resHabitation.TabIndex = 16;
            this.lblProgressTitle.AutoSize = true;
            this.lblProgressTitle.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblProgressTitle.Location = new System.Drawing.Point(187, 13);
            this.lblProgressTitle.Name = "lblProgressTitle";
            this.lblProgressTitle.Size = new System.Drawing.Size(72, 16);
            this.lblProgressTitle.TabIndex = 17;
            this.lblProgressTitle.Text = "Progress";
            this.lblLatestComponentTitle.AutoSize = true;
            this.lblLatestComponentTitle.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblLatestComponentTitle.Location = new System.Drawing.Point(462, 13);
            this.lblLatestComponentTitle.Name = "lblLatestComponentTitle";
            this.lblLatestComponentTitle.Size = new System.Drawing.Size(142, 16);
            this.lblLatestComponentTitle.TabIndex = 18;
            this.lblLatestComponentTitle.Text = "Latest Component";
            this.lblAreaTitle.AutoSize = true;
            this.lblAreaTitle.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblAreaTitle.Location = new System.Drawing.Point(32, 13);
            this.lblAreaTitle.Name = "lblAreaTitle";
            this.lblAreaTitle.Size = new System.Drawing.Size(42, 16);
            this.lblAreaTitle.TabIndex = 19;
            this.lblAreaTitle.Text = "Area";
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            base.Controls.Add(this.lblAreaTitle);
            base.Controls.Add(this.lblLatestComponentTitle);
            base.Controls.Add(this.lblProgressTitle);
            base.Controls.Add(this.resHabitation);
            base.Controls.Add(this.resConstruction);
            base.Controls.Add(this.resLabs);
            base.Controls.Add(this.resComputer);
            base.Controls.Add(this.resSensor);
            base.Controls.Add(this.resStorage);
            base.Controls.Add(this.resManufacturer);
            base.Controls.Add(this.resExtractor);
            base.Controls.Add(this.resEnergyCollector);
            base.Controls.Add(this.resReactor);
            base.Controls.Add(this.resHyperDrive);
            base.Controls.Add(this.resEngine);
            base.Controls.Add(this.resShields);
            base.Controls.Add(this.resArmor);
            base.Controls.Add(this.resAreaWeapon);
            base.Controls.Add(this.kabbfiXxi);
            base.Controls.Add(this.resBeamWeapon);
            this.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.ForeColor = System.Drawing.Color.White;
            base.Name = "ResearchLevels";
            base.Size = new System.Drawing.Size(665, 491);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}
