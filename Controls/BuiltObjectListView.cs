// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.BuiltObjectListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

//using BaconDistantWorlds;
using DistantWorlds.Types;
using DistantWorlds.Controls.Mods;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class BuiltObjectListView : ListViewBase
    {
        private static Bitmap[] _BuiltObjectImages;
        private static Bitmap _AutomationImage;
        public StellarObjectList _StellarObjects;
        private static Bitmap[] _HabitatImages;
        public bool _ShowBuiltObjectDetail;
        private bool _AllowMultiselect;
        public bool _ShowDetails;
        private IContainer components;

        public static event EventHandler<BindDataGenericModsArgs> BindDataGenericMods;

        public BuiltObjectListView()
        {
            this.InitializeComponent();
            this._Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this._Grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this._Grid.EnableHeadersVisualStyles = false;
            this._Grid.RowTemplate.Height = 25;
            DataGridViewImageColumn gridViewImageColumn1 = (DataGridViewImageColumn)new BuiltObjectListView.SortableImageColumn();
            gridViewImageColumn1.Description = "Empire";
            gridViewImageColumn1.HeaderText = TextResolver.GetText("Empire");
            gridViewImageColumn1.Name = "Empire";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn1.ValueType = typeof(string);
            gridViewImageColumn1.Width = 25;
            gridViewImageColumn1.FillWeight = 25f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
            DataGridViewImageColumn gridViewImageColumn2 = new DataGridViewImageColumn();
            gridViewImageColumn2.Description = "Picture";
            gridViewImageColumn2.HeaderText = "";
            gridViewImageColumn2.Name = "Picture";
            gridViewImageColumn2.ReadOnly = true;
            gridViewImageColumn2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn2.ValueType = typeof(Image);
            gridViewImageColumn2.Width = 30;
            gridViewImageColumn2.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn1.Name = "Name";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 105;
            viewTextBoxColumn1.FillWeight = 105f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Role");
            viewTextBoxColumn2.Name = "Role";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 105;
            viewTextBoxColumn2.FillWeight = 105f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Mission");
            viewTextBoxColumn3.Name = "Mission";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(string);
            viewTextBoxColumn3.Width = 60;
            viewTextBoxColumn3.FillWeight = 60f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("System");
            viewTextBoxColumn4.Name = "System";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(string);
            viewTextBoxColumn4.Width = 60;
            viewTextBoxColumn4.FillWeight = 60f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn5.HeaderText = TextResolver.GetText("Firepower");
            viewTextBoxColumn5.Name = "Firepower";
            viewTextBoxColumn5.ReadOnly = false;
            viewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn5.ValueType = typeof(int);
            viewTextBoxColumn5.Width = 50;
            viewTextBoxColumn5.FillWeight = 50f;
            viewTextBoxColumn5.DefaultCellStyle.Format = "#0";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
            DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn6.HeaderText = TextResolver.GetText("Speed");
            viewTextBoxColumn6.Name = "Speed";
            viewTextBoxColumn6.ReadOnly = false;
            viewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn6.ValueType = typeof(int);
            viewTextBoxColumn6.Width = 50;
            viewTextBoxColumn6.FillWeight = 50f;
            viewTextBoxColumn6.DefaultCellStyle.Format = "#0";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
            DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn7.HeaderText = TextResolver.GetText("Maintenance Cost");
            viewTextBoxColumn7.Name = "Maintenance";
            viewTextBoxColumn7.ReadOnly = false;
            viewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn7.ValueType = typeof(double);
            viewTextBoxColumn7.Width = 50;
            viewTextBoxColumn7.FillWeight = 50f;
            viewTextBoxColumn7.DefaultCellStyle.Format = "#0";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn7);
            DataGridViewTextBoxColumn viewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn8.HeaderText = TextResolver.GetText("Fleet");
            viewTextBoxColumn8.Name = "Fleet";
            viewTextBoxColumn8.ReadOnly = false;
            viewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn8.ValueType = typeof(string);
            viewTextBoxColumn8.Width = 60;
            viewTextBoxColumn8.FillWeight = 60f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn8);
            DataGridViewImageColumn gridViewImageColumn3 = (DataGridViewImageColumn)new BuiltObjectListView.SortableImageColumn();
            gridViewImageColumn3.Description = "Automated";
            gridViewImageColumn3.HeaderText = TextResolver.GetText("Automated");
            gridViewImageColumn3.Name = "Automated";
            gridViewImageColumn3.ReadOnly = true;
            gridViewImageColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn3.ValueType = typeof(string);
            gridViewImageColumn3.Width = 25;
            gridViewImageColumn3.FillWeight = 25f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn3);
        }

        public void SetAutomation(DataGridViewRow row, bool automated)
        {
            string text = TextResolver.GetText("Not automated");
            Bitmap bitmap = (Bitmap)null;
            if (automated)
            {
                text = TextResolver.GetText("Automated");
                bitmap = BuiltObjectListView._AutomationImage;
            }
          ((BuiltObjectListView.SortableImageCell)row.Cells["Automated"]).ScaledImage = bitmap;
            row.Cells["Automated"].Value = (object)text;
            row.Cells["Automated"].ToolTipText = text;
        }

        public bool ShowBuiltObjectDetail
        {
            get => this._ShowBuiltObjectDetail;
            set => this._ShowBuiltObjectDetail = value;
        }

        public BuiltObjectList SelectedBuiltObjects
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                BuiltObjectList selectedBuiltObjects = new BuiltObjectList();
                foreach (DataGridViewRow row in (BaseCollection)selectedRows)
                {
                    BuiltObject builtObject = this.ResolveBuiltObject(row);
                    if (builtObject != null)
                        selectedBuiltObjects.Add(builtObject);
                }
                return selectedBuiltObjects;
            }
        }

        public BuiltObject ResolveBuiltObject(DataGridViewRow row)
        {
            object tag = row.Cells[2].Tag;
            if (tag is int stellarObjectId)
            {
                StellarObject stellarObjectById = this._StellarObjects.FindStellarObjectById(stellarObjectId);
                return stellarObjectById is BuiltObject ? (BuiltObject)stellarObjectById : (BuiltObject)null;
            }
            else
            {
                return (BuiltObject)null;
            }
        }

        public StellarObject ResolveStellarObject(DataGridViewRow row)
        {
            object tag = row.Cells[2].Tag;
            if (tag is int stellarObjectId)
            {
                return this._StellarObjects != null ? this._StellarObjects.FindStellarObjectById(stellarObjectId) : (StellarObject)null;
            }
            else
            {
                return (StellarObject)null;
            }
        }

        public StellarObject SelectedStellarObject
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                return selectedRows.Count == 1 ? this.ResolveStellarObject(selectedRows[0]) : (StellarObject)null;
            }
        }

        public BuiltObject SelectedBuiltObject
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                return selectedRows.Count == 1 ? this.ResolveBuiltObject(selectedRows[0]) : (BuiltObject)null;
            }
        }

        public void SelectBuiltObject(BuiltObject builtObjectToSelect)
        {
            int num1 = -1;
            if (builtObjectToSelect == null)
                return;
            for (int index = 0; index < this._StellarObjects.Count; ++index)
            {
                if (this._StellarObjects[index] is BuiltObject && this._StellarObjects[index] == builtObjectToSelect)
                {
                    num1 = ((BuiltObject)this._StellarObjects[index]).BuiltObjectID;
                    break;
                }
            }
            if (num1 < 0)
                return;
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
                this._Grid.Rows[index].Selected = false;
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                object tag = this._Grid.Rows[index].Cells[2].Tag;
                if (tag != null && tag is int num2 && num2 == num1)
                {
                    this._Grid.Rows[index].Selected = true;
                    this._Grid.FirstDisplayedScrollingRowIndex = index;
                    break;
                }
            }
        }

        private Bitmap PrepareBuiltObjectImage(
          Bitmap image,
          Color mainColor,
          Color secondaryColor,
          int size,
          int targetSize)
        {
            double num = Math.Sqrt((double)targetSize / (double)size);
            int width = (int)((double)image.Width * num);
            int height = (int)((double)image.Height * num);
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage((Image)image, new Rectangle(0, 0, width, height));
            graphics.Dispose();
            return bitmap;
        }

        private static void ClearImages()
        {
            BuiltObjectListView.ClearImageArray(BuiltObjectListView._HabitatImages);
            BuiltObjectListView.ClearImageArray(BuiltObjectListView._BuiltObjectImages);
        }

        private static void ClearImageArray(Bitmap[] imageArray)
        {
            if (imageArray == null)
                return;
            for (int index = 0; index < imageArray.Length; ++index)
            {
                if (imageArray[index] != null)
                {
                    imageArray[index].Dispose();
                    imageArray[index] = (Bitmap)null;
                }
            }
        }

        public void Kickstart(bool allowMultiselect)
        {
            this._AllowMultiselect = allowMultiselect;
            this._Grid.MultiSelect = this._AllowMultiselect;
        }

        public static void InitializeImages(
          Bitmap[] builtObjectImages,
          Bitmap[] habitatImages,
          Bitmap automationImage)
        {
            BuiltObjectListView.ClearImages();
            BuiltObjectListView._BuiltObjectImages = new Bitmap[builtObjectImages.Length];
            for (int index = 0; index < builtObjectImages.Length; ++index)
            {
                BuiltObjectListView._BuiltObjectImages[index] = ListViewBase.PrescaleImageStatic(builtObjectImages[index], 30, 30);
                BuiltObjectListView._BuiltObjectImages[index].RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            BuiltObjectListView._HabitatImages = new Bitmap[habitatImages.Length];
            for (int index = 0; index < habitatImages.Length; ++index)
            {
                int width1 = habitatImages[index].Width;
                int height1 = habitatImages[index].Height;
                double num = (double)height1 / 30.0;
                int width2 = (int)((double)width1 / num);
                int height2 = (int)((double)height1 / num);
                Bitmap bitmap = new Bitmap(width2, height2, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage((Image)bitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.DrawImage((Image)habitatImages[index], new Rectangle(0, 0, width2, height2));
                }
                BuiltObjectListView._HabitatImages[index] = bitmap;
            }
            BuiltObjectListView._AutomationImage = automationImage;
        }

        public void ClearData() => this._StellarObjects = (StellarObjectList)null;

        public void BindDataGeneric(StellarObjectList stellarObjects, Galaxy galaxy) => this.BindDataGeneric(stellarObjects, galaxy, false);

        public void BindDataGeneric(StellarObjectList stellarObjects, Galaxy galaxy, bool showDetails)
        {
            //BaconBuiltObjectListView.BindDataGeneric(this, stellarObjects, galaxy, showDetails);
            BuiltObjectListView.OnBindDataGenericMods(this, stellarObjects, galaxy, showDetails);
        }

        public void BindSingleHabitat(Habitat habitat, DataGridViewRow row, Empire defaultEmpire)
        {
            row.Height = 30;
            if (habitat == null)
                return;
            if (habitat.Empire != null)
            {
                ((BuiltObjectListView.SortableImageCell)row.Cells[0]).ScaledImage = habitat.Empire.SmallFlagPicture;
                row.Cells[0].Value = (object)habitat.Empire.Name;
                row.Cells[0].ToolTipText = habitat.Empire.Name;
            }
            Bitmap habitatImage = BuiltObjectListView._HabitatImages[(int)habitat.PictureRef];
            row.Cells[1].Value = (object)habitatImage;
            row.Cells[1].ToolTipText = "";
            row.Cells[2].Value = (object)habitat.Name;
            row.Cells[2].Tag = (object)(habitat.HabitatIndex + StellarObjectList.HabitatIndexRangeIncrease);
            string str1 = Galaxy.ResolveDescription(habitat.Type) + " " + Galaxy.ResolveDescription(habitat.Category);
            row.Cells[3].Value = (object)str1;
            string str2 = "(" + TextResolver.GetText("None") + ")";
            row.Cells[4].Value = (object)str2;
            string str3 = string.Empty;
            Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat);
            if (habitatSystemStar != null)
                str3 = habitatSystemStar.Name;
            row.Cells[5].Value = (object)str3;
            row.Cells[6].Value = (object)0;
            row.Cells[7].Value = (object)0;
            row.Cells[8].Value = (object)0.0;
            row.Cells[9].Value = (object)("(" + TextResolver.GetText("None") + ")");
            ((BuiltObjectListView.SortableImageCell)row.Cells[10]).ScaledImage = (Bitmap)null;
            row.Cells[10].Value = (object)"";
            row.Cells[10].ToolTipText = "";
        }

        public void BindSingleBuiltObject(
          BuiltObject builtObject,
          DataGridViewRow row,
          Empire defaultEmpire,
          Galaxy galaxy)
        {
            row.Height = 30;
            Empire empire = builtObject.Empire;
            if (builtObject.PirateEmpireId > (byte)0)
                empire = galaxy.GetEmpireById((int)builtObject.PirateEmpireId);
            if (empire != null)
            {
                ((BuiltObjectListView.SortableImageCell)row.Cells[0]).ScaledImage = empire.SmallFlagPicture;
                row.Cells[0].Value = (object)empire.Name;
                row.Cells[0].ToolTipText = empire.Name;
            }
            else
                row.Cells[0].ToolTipText = "(" + TextResolver.GetText("No Empire") + ")";
            Bitmap builtObjectImage = BuiltObjectListView._BuiltObjectImages[builtObject.PictureRef];
            row.Cells[1].Value = (object)builtObjectImage;
            row.Cells[1].ToolTipText = builtObject.Design.Name + " (" + builtObject.SubRole.ToString() + ")";
            string str1 = builtObject.Name;
            if (builtObject.Empire == galaxy.IndependentEmpire && empire != builtObject.Empire && builtObject.PirateEmpireId > (byte)0)
                str1 = str1 + " (" + TextResolver.GetText("Smuggler") + ")";
            row.Cells[2].Value = (object)str1;
            row.Cells[2].Tag = (object)builtObject.BuiltObjectID;
            if (this._ShowDetails)
            {
                if (builtObject.DamagedComponentCount > 0)
                {
                    row.Cells[2].Style.ForeColor = Color.Red;
                    row.Cells[2].Style.SelectionForeColor = Color.Red;
                    string str2 = string.Format(TextResolver.GetText("X components damaged"), (object)builtObject.DamagedComponentCount.ToString());
                    if (builtObject.Role == BuiltObjectRole.Base)
                        str2 = builtObject.ParentHabitat == null || builtObject.ParentHabitat.Population == null || builtObject.ParentHabitat.Population.Count <= 0 || builtObject.ParentHabitat.Empire != defaultEmpire ? str2 + " (" + TextResolver.GetText("send a construction ship to repair") + ")" : str2 + " (" + TextResolver.GetText("repairing at colony") + ")";
                    else if (builtObject.WarpSpeed <= 0)
                        str2 = str2 + " (" + TextResolver.GetText("no hyperdrive, cannot travel for repairs") + ")";
                    row.Cells[2].ToolTipText = str2;
                }
                else if (builtObject.UnbuiltComponentCount > 0)
                {
                    row.Cells[2].Style.ForeColor = Color.Orange;
                    row.Cells[2].Style.SelectionForeColor = Color.Orange;
                    string str3 = string.Format(TextResolver.GetText("Under construction (X components unbuilt)"), (object)builtObject.UnbuiltComponentCount.ToString());
                    row.Cells[2].ToolTipText = str3;
                }
            }
            string str4 = Galaxy.ResolveDescription(builtObject.Role) + ", " + Galaxy.ResolveDescription(builtObject.SubRole);
            row.Cells[3].Value = (object)str4;
            string str5 = "(" + TextResolver.GetText("None") + ")";
            if (builtObject.Mission != null && builtObject.Mission.Type != BuiltObjectMissionType.Undefined)
                str5 = Galaxy.ResolveDescription(builtObject.Mission.Type);
            row.Cells[4].Value = (object)str5;
            string str6 = string.Empty;
            if (builtObject.NearestSystemStar != null)
                str6 = builtObject.NearestSystemStar.Name;
            if (string.IsNullOrEmpty(str6))
                str6 = "(" + TextResolver.GetText("Deep Space") + ")";
            row.Cells[5].Value = (object)str6;
            row.Cells[6].Value = (object)builtObject.FirepowerRaw;
            row.Cells[7].Value = (object)(int)builtObject.TopSpeed;
            double annualSupportCost = (double)builtObject.AnnualSupportCost;
            double num1 = 0.0;
            if (builtObject.Empire != null)
                num1 = annualSupportCost * builtObject.Empire.ShipMaintenanceSavings;
            double num2 = annualSupportCost - num1;
            row.Cells[8].Value = (object)num2;
            row.Cells[9].Value = builtObject.ShipGroup == null ? (object)("(" + TextResolver.GetText("None") + ")") : (object)builtObject.ShipGroup.Name;
            string text = TextResolver.GetText("Not automated");
            if (builtObject.IsAutoControlled)
            {
                ((BuiltObjectListView.SortableImageCell)row.Cells[10]).ScaledImage = BuiltObjectListView._AutomationImage;
                text = TextResolver.GetText("Automated");
            }
            row.Cells[10].Value = (object)text;
            row.Cells[10].ToolTipText = text;
        }

        public void BindData(BuiltObjectList builtObjects, Galaxy galaxy) => this.BindData(builtObjects, galaxy, false);

        public void BindData(BuiltObjectList builtObjects, Galaxy galaxy, bool showDetails)
        {
            StellarObjectList stellarObjects = new StellarObjectList();
            stellarObjects.AddRange(builtObjects);
            this.BindDataGeneric(stellarObjects, galaxy, showDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = nameof(BuiltObjectListView);
            this.Size = new Size(247, 157);
            this.ResumeLayout(false);
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new BuiltObjectListView.SortableImageCell();
                this.ValueType = typeof(string);
            }
        }

        private class SortableImageCell : DataGridViewImageCell
        {
            private Bitmap _Bitmap;

            public SortableImageCell() => this.ValueType = typeof(string);

            public Bitmap ScaledImage
            {
                get => this._Bitmap;
                set => this._Bitmap = value;
            }

            protected override object GetFormattedValue(
              object value,
              int rowIndex,
              ref DataGridViewCellStyle cellStyle,
              TypeConverter valueTypeConverter,
              TypeConverter formattedValueTypeConverter,
              DataGridViewDataErrorContexts context)
            {
                return (object)this.ScaledImage;
            }

            public override object DefaultNewRowValue => (object)string.Empty;
        }

        private static void OnBindDataGenericMods(BuiltObjectListView boListView, StellarObjectList stellarObjects, Galaxy galaxy, bool showDetails)
        {
            var tmp = BuiltObjectListView.BindDataGenericMods;
            if (tmp != null)
            {
                var args = new BindDataGenericModsArgs(boListView, stellarObjects, galaxy, showDetails);
                tmp(null, args);
            }
        }
    }
}
