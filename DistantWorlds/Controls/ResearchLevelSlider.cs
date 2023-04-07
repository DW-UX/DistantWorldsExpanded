// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResearchLevelSlider
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class ResearchLevelSlider : UserControl
    {
        private int int_0;

        private int int_1;

        private Main main_0;

        private ResearchArea researchArea_0;

        private IContainer icontainer_0;

        private ColorSlider sldResearchLevel;

        private Label lblResearchArea;

        private Label lblLatestComponent;

        private PictureBox picResearchArea;

        private PictureBox picLatestComponent;

        public ResearchArea ResearchArea => researchArea_0;

        public ResearchLevelSlider() : base()
        {
            Class7.VEFSJNszvZKMZ();
            InitializeComponent();
            LayoutControls();
        }

        public void ClearData()
        {
            researchArea_0 = null;
        }

        public void BindData(Main parentForm, ResearchArea researchArea)
        {
            main_0 = parentForm;
            researchArea_0 = researchArea;
            picResearchArea.SizeMode = PictureBoxSizeMode.Zoom;
            picLatestComponent.SizeMode = PictureBoxSizeMode.Zoom;
            LayoutControls();
            Bitmap bitmap = method_4(researchArea_0.Category);
            Size size = method_0(bitmap, 20);
            picResearchArea.Image = method_2(bitmap, size.Width, size.Height);
            lblResearchArea.Text = Galaxy.ResolveDescription(researchArea_0.Category);
            sldResearchLevel.Minimum = 0;
            sldResearchLevel.Maximum = Math.Max((int)(researchArea_0.TechPoints + 1f), method_3(researchArea_0.Category));
            sldResearchLevel.Value = (int)researchArea_0.TechPoints;
            method_1(researchArea_0.TechPoints);
        }

        private Size method_0(Image image_0, int int_2)
        {
            int num = image_0.Width;
            int num2 = image_0.Height;
            double num3 = 1.0;
            num3 = ((num <= num2) ? ((double)num2 / (double)int_2) : ((double)num / (double)int_2));
            num = (int)((double)num * num3);
            num2 = (int)((double)num2 * num3);
            return new Size(num, num2);
        }

        private void method_1(double double_0)
        {
            DistantWorlds.Types.Component component = DistantWorlds.Types.Component.EvaluateLatest(researchArea_0.Category, double_0);
            if (component != null)
            {
                Size size = method_0(main_0.bitmap_21[component.ComponentID], 20);
                picLatestComponent.Image = method_2(main_0.bitmap_21[component.ComponentID], size.Width, size.Height);
                lblLatestComponent.Text = component.Name;
            }
            else
            {
                picLatestComponent.Image = null;
                lblLatestComponent.Text = string.Empty;
            }
        }

        public void LayoutControls()
        {
            int num = base.ClientRectangle.Width - (388 + int_1);
            SuspendLayout();
            picResearchArea.Location = new Point(0, 0);
            lblResearchArea.Location = new Point(24, 2);
            lblResearchArea.ForeColor = Color.FromArgb(170, 170, 170);
            sldResearchLevel.Location = new Point(134, 0);
            sldResearchLevel.Size = new Size(num, 16);
            picLatestComponent.Location = new Point(138 + num + 4, 0);
            lblLatestComponent.Location = new Point(138 + num + 4 + 20 + 4, 2);
            lblLatestComponent.ForeColor = Color.FromArgb(170, 170, 170);
            ResumeLayout();
        }

        private Bitmap method_2(Bitmap bitmap_0, int int_2, int int_3)
        {
            Bitmap bitmap = new Bitmap(int_2, int_3, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(bitmap_0, new Rectangle(0, 0, int_2, int_3));
            graphics.Dispose();
            return bitmap;
        }

        private int method_3(ComponentCategoryType componentCategoryType_0)
        {
            int result = 0;
            switch (componentCategoryType_0)
            {
                case ComponentCategoryType.WeaponBeam:
                    result = 1520000;
                    break;
                case ComponentCategoryType.WeaponTorpedo:
                    result = 1420000;
                    break;
                case ComponentCategoryType.WeaponArea:
                    result = 1220000;
                    break;
                case ComponentCategoryType.Armor:
                    result = 710000;
                    break;
                case ComponentCategoryType.Shields:
                    result = 1900000;
                    break;
                case ComponentCategoryType.Engine:
                    result = 1220000;
                    break;
                case ComponentCategoryType.HyperDrive:
                    result = 1270000;
                    break;
                case ComponentCategoryType.Reactor:
                    result = 1390000;
                    break;
                case ComponentCategoryType.EnergyCollector:
                    result = 1600000;
                    break;
                case ComponentCategoryType.Extractor:
                    result = 365000;
                    break;
                case ComponentCategoryType.Manufacturer:
                    result = 320000;
                    break;
                case ComponentCategoryType.Storage:
                    result = 650000;
                    break;
                case ComponentCategoryType.Sensor:
                    result = 1090000;
                    break;
                case ComponentCategoryType.Computer:
                    result = 1600000;
                    break;
                case ComponentCategoryType.Labs:
                    result = 390000;
                    break;
                case ComponentCategoryType.Construction:
                    result = 2120000;
                    break;
                case ComponentCategoryType.Habitation:
                    result = 930000;
                    break;
            }
            return result;
        }

        private Bitmap method_4(ComponentCategoryType componentCategoryType_0)
        {
            Bitmap result = null;
            switch (componentCategoryType_0)
            {
                case ComponentCategoryType.WeaponBeam:
                    result = main_0.bitmap_21[0];
                    break;
                case ComponentCategoryType.WeaponTorpedo:
                    result = main_0.bitmap_21[8];
                    break;
                case ComponentCategoryType.WeaponArea:
                    result = main_0.bitmap_21[16];
                    break;
                case ComponentCategoryType.Armor:
                    result = main_0.bitmap_21[24];
                    break;
                case ComponentCategoryType.Shields:
                    result = main_0.bitmap_21[28];
                    break;
                case ComponentCategoryType.Engine:
                    result = main_0.bitmap_21[40];
                    break;
                case ComponentCategoryType.HyperDrive:
                    result = main_0.bitmap_21[52];
                    break;
                case ComponentCategoryType.Reactor:
                    result = main_0.bitmap_21[60];
                    break;
                case ComponentCategoryType.EnergyCollector:
                    result = main_0.bitmap_21[68];
                    break;
                case ComponentCategoryType.Extractor:
                    result = main_0.bitmap_21[73];
                    break;
                case ComponentCategoryType.Manufacturer:
                    result = main_0.bitmap_21[80];
                    break;
                case ComponentCategoryType.Storage:
                    result = main_0.bitmap_21[95];
                    break;
                case ComponentCategoryType.Sensor:
                    result = main_0.bitmap_21[107];
                    break;
                case ComponentCategoryType.Computer:
                    result = main_0.bitmap_21[127];
                    break;
                case ComponentCategoryType.Labs:
                    result = main_0.bitmap_21[136];
                    break;
                case ComponentCategoryType.Construction:
                    result = main_0.bitmap_21[148];
                    break;
                case ComponentCategoryType.Habitation:
                    result = main_0.bitmap_21[163];
                    break;
            }
            return result;
        }

        private void sldResearchLevel_Scroll(object sender, ScrollEventArgs e)
        {
            researchArea_0.TechPoints = sldResearchLevel.Value;
            method_1(researchArea_0.TechPoints);
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
            this.sldResearchLevel = new DistantWorlds.Controls.ColorSlider();
            this.lblResearchArea = new System.Windows.Forms.Label();
            this.lblLatestComponent = new System.Windows.Forms.Label();
            this.picResearchArea = new System.Windows.Forms.PictureBox();
            this.picLatestComponent = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.picResearchArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.picLatestComponent).BeginInit();
            base.SuspendLayout();
            this.sldResearchLevel.BackColor = System.Drawing.Color.Transparent;
            this.sldResearchLevel.BarInnerColor = System.Drawing.Color.FromArgb(64, 0, 192);
            this.sldResearchLevel.BarOuterColor = System.Drawing.Color.FromArgb(40, 0, 120);
            this.sldResearchLevel.BarPenColor = System.Drawing.Color.FromArgb(24, 0, 96);
            this.sldResearchLevel.BorderRoundRectSize = new System.Drawing.Size(2, 2);
            this.sldResearchLevel.ElapsedInnerColor = System.Drawing.Color.FromArgb(96, 96, 255);
            this.sldResearchLevel.ElapsedOuterColor = System.Drawing.Color.FromArgb(48, 48, 180);
            this.sldResearchLevel.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.sldResearchLevel.ForeColor = System.Drawing.Color.White;
            this.sldResearchLevel.LargeChange = 5u;
            this.sldResearchLevel.Location = new System.Drawing.Point(74, 7);
            this.sldResearchLevel.Minimum = 1;
            this.sldResearchLevel.Name = "sldResearchLevel";
            this.sldResearchLevel.Size = new System.Drawing.Size(120, 16);
            this.sldResearchLevel.SmallChange = 1u;
            this.sldResearchLevel.TabIndex = 76;
            this.sldResearchLevel.Text = "colorSlider1";
            this.sldResearchLevel.ThumbInnerColor = System.Drawing.Color.FromArgb(80, 80, 255);
            this.sldResearchLevel.ThumbOuterColor = System.Drawing.Color.FromArgb(48, 32, 160);
            this.sldResearchLevel.ThumbPenColor = System.Drawing.Color.FromArgb(32, 32, 64);
            this.sldResearchLevel.ThumbRoundRectSize = new System.Drawing.Size(3, 3);
            this.sldResearchLevel.ThumbSize = 20;
            this.sldResearchLevel.Scroll += new System.Windows.Forms.ScrollEventHandler(sldResearchLevel_Scroll);
            this.lblResearchArea.AutoSize = true;
            this.lblResearchArea.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblResearchArea.ForeColor = System.Drawing.Color.White;
            this.lblResearchArea.Location = new System.Drawing.Point(33, 10);
            this.lblResearchArea.Name = "lblResearchArea";
            this.lblResearchArea.Size = new System.Drawing.Size(34, 13);
            this.lblResearchArea.TabIndex = 77;
            this.lblResearchArea.Text = "Area";
            this.lblLatestComponent.AutoSize = true;
            this.lblLatestComponent.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lblLatestComponent.ForeColor = System.Drawing.Color.White;
            this.lblLatestComponent.Location = new System.Drawing.Point(240, 10);
            this.lblLatestComponent.Name = "lblLatestComponent";
            this.lblLatestComponent.Size = new System.Drawing.Size(111, 13);
            this.lblLatestComponent.TabIndex = 78;
            this.lblLatestComponent.Text = "Latest Component";
            this.picResearchArea.Location = new System.Drawing.Point(7, 6);
            this.picResearchArea.Name = "picResearchArea";
            this.picResearchArea.Size = new System.Drawing.Size(20, 20);
            this.picResearchArea.TabIndex = 79;
            this.picResearchArea.TabStop = false;
            this.picLatestComponent.Location = new System.Drawing.Point(214, 6);
            this.picLatestComponent.Name = "picLatestComponent";
            this.picLatestComponent.Size = new System.Drawing.Size(20, 20);
            this.picLatestComponent.TabIndex = 80;
            this.picLatestComponent.TabStop = false;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            base.Controls.Add(this.picLatestComponent);
            base.Controls.Add(this.picResearchArea);
            base.Controls.Add(this.lblLatestComponent);
            base.Controls.Add(this.lblResearchArea);
            base.Controls.Add(this.sldResearchLevel);
            base.Name = "ResearchLevelSlider";
            base.Size = new System.Drawing.Size(355, 33);
            ((System.ComponentModel.ISupportInitialize)this.picResearchArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.picLatestComponent).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}
