// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.BuiltObjectListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using BaconDistantWorlds;
using DistantWorlds.Types;
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
        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                CellTemplate = new SortableImageCell();
                base.ValueType = typeof(string);
            }
        }

        private class SortableImageCell : DataGridViewImageCell
        {
            private Bitmap _Bitmap;

            public Bitmap ScaledImage
            {
                get
                {
                    return _Bitmap;
                }
                set
                {
                    _Bitmap = value;
                }
            }

            public override object DefaultNewRowValue => string.Empty;

            public SortableImageCell()
            {
                ValueType = typeof(string);
            }

            protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
            {
                return ScaledImage;
            }
        }

        private static Bitmap[] _BuiltObjectImages;

        private static Bitmap _AutomationImage;

        public StellarObjectList _StellarObjects;

        private static Bitmap[] _HabitatImages;

        public bool _ShowBuiltObjectDetail;

        private bool _AllowMultiselect;

        public bool _ShowDetails;

        private IContainer components;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowBuiltObjectDetail
        {
            get
            {
                return _ShowBuiltObjectDetail;
            }
            set
            {
                _ShowBuiltObjectDetail = value;
            }
        }

        public BuiltObjectList SelectedBuiltObjects
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = _Grid.SelectedRows;
                BuiltObjectList builtObjectList = new BuiltObjectList();
                foreach (DataGridViewRow item in selectedRows)
                {
                    BuiltObject builtObject = ResolveBuiltObject(item);
                    if (builtObject != null)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
                return builtObjectList;
            }
        }

        public StellarObject SelectedStellarObject
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = _Grid.SelectedRows;
                if (selectedRows.Count == 1)
                {
                    return ResolveStellarObject(selectedRows[0]);
                }
                return null;
            }
        }

        public BuiltObject SelectedBuiltObject
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = _Grid.SelectedRows;
                if (selectedRows.Count == 1)
                {
                    return ResolveBuiltObject(selectedRows[0]);
                }
                return null;
            }
        }

        public BuiltObjectListView()
        {
            InitializeComponent();
            _Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            _Grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            _Grid.EnableHeadersVisualStyles = false;
            _Grid.RowTemplate.Height = 25;
            DataGridViewImageColumn dataGridViewImageColumn = null;
            DataGridViewTextBoxColumn dataGridViewTextBoxColumn = null;
            dataGridViewImageColumn = new SortableImageColumn
            {
                Description = "Empire",
                HeaderText = TextResolver.GetText("Empire"),
                Name = "Empire",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(string),
                Width = 25,
                FillWeight = 25f
            };
            _Grid.Columns.Add(dataGridViewImageColumn);
            dataGridViewImageColumn = new DataGridViewImageColumn
            {
                Description = "Picture",
                HeaderText = "",
                Name = "Picture",
                ReadOnly = true,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ValueType = typeof(Image),
                Width = 30,
                FillWeight = 30f
            };
            _Grid.Columns.Add(dataGridViewImageColumn);
            dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = TextResolver.GetText("Name"),
                Name = "Name",
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(string),
                Width = 105,
                FillWeight = 105f
            };
            _Grid.Columns.Add(dataGridViewTextBoxColumn);
            dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = TextResolver.GetText("Role"),
                Name = "Role",
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(string),
                Width = 105,
                FillWeight = 105f
            };
            _Grid.Columns.Add(dataGridViewTextBoxColumn);
            dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = TextResolver.GetText("Mission"),
                Name = "Mission",
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(string),
                Width = 60,
                FillWeight = 60f
            };
            _Grid.Columns.Add(dataGridViewTextBoxColumn);
            dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = TextResolver.GetText("System"),
                Name = "System",
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(string),
                Width = 60,
                FillWeight = 60f
            };
            _Grid.Columns.Add(dataGridViewTextBoxColumn);
            dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = TextResolver.GetText("Firepower"),
                Name = "Firepower",
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(int),
                Width = 50,
                FillWeight = 50f,
                DefaultCellStyle =
            {
                Format = "#0"
            }
            };
            _Grid.Columns.Add(dataGridViewTextBoxColumn);
            dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = TextResolver.GetText("Speed"),
                Name = "Speed",
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(int),
                Width = 50,
                FillWeight = 50f,
                DefaultCellStyle =
            {
                Format = "#0"
            }
            };
            _Grid.Columns.Add(dataGridViewTextBoxColumn);
            dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = TextResolver.GetText("Maintenance Cost"),
                Name = "Maintenance",
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(double),
                Width = 50,
                FillWeight = 50f,
                DefaultCellStyle =
            {
                Format = "#0"
            }
            };
            _Grid.Columns.Add(dataGridViewTextBoxColumn);
            dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = TextResolver.GetText("Fleet"),
                Name = "Fleet",
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(string),
                Width = 60,
                FillWeight = 60f
            };
            _Grid.Columns.Add(dataGridViewTextBoxColumn);
            dataGridViewImageColumn = new SortableImageColumn
            {
                Description = "Automated",
                HeaderText = TextResolver.GetText("Automated"),
                Name = "Automated",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(string),
                Width = 25,
                FillWeight = 25f
            };
            _Grid.Columns.Add(dataGridViewImageColumn);
        }

        public void SetAutomation(DataGridViewRow row, bool automated)
        {
            string text = TextResolver.GetText("Not automated");
            Bitmap scaledImage = null;
            if (automated)
            {
                text = TextResolver.GetText("Automated");
                scaledImage = _AutomationImage;
            }
            ((SortableImageCell)row.Cells["Automated"]).ScaledImage = scaledImage;
            ((SortableImageCell)row.Cells["Automated"]).Value = text;
            row.Cells["Automated"].ToolTipText = text;
        }

        public BuiltObject ResolveBuiltObject(DataGridViewRow row)
        {
            int num = -1;
            object tag = row.Cells[2].Tag;
            if (tag != null && tag is int)
            {
                num = (int)tag;
            }
            if (num >= 0)
            {
                StellarObject stellarObject = _StellarObjects.FindStellarObjectById(num);
                if (stellarObject is BuiltObject)
                {
                    return (BuiltObject)stellarObject;
                }
                return null;
            }
            return null;
        }

        public StellarObject ResolveStellarObject(DataGridViewRow row)
        {
            int num = -1;
            object tag = row.Cells[2].Tag;
            if (tag != null && tag is int)
            {
                num = (int)tag;
            }
            if (num >= 0)
            {
                if (_StellarObjects != null)
                {
                    return _StellarObjects.FindStellarObjectById(num);
                }
                return null;
            }
            return null;
        }

        public void SelectBuiltObject(BuiltObject builtObjectToSelect)
        {
            int num = -1;
            if (builtObjectToSelect == null)
            {
                return;
            }
            for (int i = 0; i < _StellarObjects.Count; i++)
            {
                if (_StellarObjects[i] is BuiltObject && _StellarObjects[i] == builtObjectToSelect)
                {
                    num = ((BuiltObject)_StellarObjects[i]).BuiltObjectID;
                    break;
                }
            }
            if (num < 0)
            {
                return;
            }
            for (int j = 0; j < _Grid.Rows.Count; j++)
            {
                _Grid.Rows[j].Selected = false;
            }
            for (int k = 0; k < _Grid.Rows.Count; k++)
            {
                object tag = _Grid.Rows[k].Cells[2].Tag;
                if (tag != null && tag is int num2 && num2 == num)
                {
                    _Grid.Rows[k].Selected = true;
                    _Grid.FirstDisplayedScrollingRowIndex = k;
                    break;
                }
            }
        }

        private Bitmap PrepareBuiltObjectImage(Bitmap image, Color mainColor, Color secondaryColor, int size, int targetSize)
        {
            double d = (double)targetSize / (double)size;
            d = Math.Sqrt(d);
            int num = (int)((double)image.Width * d);
            int num2 = (int)((double)image.Height * d);
            Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2));
            graphics.Dispose();
            return bitmap;
        }

        private static void ClearImages()
        {
            ClearImageArray(_HabitatImages);
            ClearImageArray(_BuiltObjectImages);
        }

        private static void ClearImageArray(Bitmap[] imageArray)
        {
            if (imageArray == null)
            {
                return;
            }
            for (int i = 0; i < imageArray.Length; i++)
            {
                if (imageArray[i] != null)
                {
                    imageArray[i].Dispose();
                    imageArray[i] = null;
                }
            }
        }

        public void Kickstart(bool allowMultiselect)
        {
            _AllowMultiselect = allowMultiselect;
            _Grid.MultiSelect = _AllowMultiselect;
        }

        public static void InitializeImages(Bitmap[] builtObjectImages, Bitmap[] habitatImages, Bitmap automationImage)
        {
            ClearImages();
            _BuiltObjectImages = new Bitmap[builtObjectImages.Length];
            for (int i = 0; i < builtObjectImages.Length; i++)
            {
                _BuiltObjectImages[i] = ListViewBase.PrescaleImageStatic(builtObjectImages[i], 30, 30);
                _BuiltObjectImages[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            _HabitatImages = new Bitmap[habitatImages.Length];
            for (int j = 0; j < habitatImages.Length; j++)
            {
                int num = habitatImages[j].Width;
                int num2 = habitatImages[j].Height;
                double num3 = (double)num2 / 30.0;
                num = (int)((double)num / num3);
                num2 = (int)((double)num2 / num3);
                Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.DrawImage(habitatImages[j], new Rectangle(0, 0, num, num2));
                }
                _HabitatImages[j] = bitmap;
            }
            _AutomationImage = automationImage;
        }

        public void ClearData()
        {
            _StellarObjects = null;
        }

        public void BindDataGeneric(StellarObjectList stellarObjects, Galaxy galaxy)
        {
            BindDataGeneric(stellarObjects, galaxy, showDetails: false);
        }

        public void BindDataGeneric(StellarObjectList stellarObjects, Galaxy galaxy, bool showDetails)
        {
            BaconBuiltObjectListView.BindDataGeneric(this, stellarObjects, galaxy, showDetails);
        }

        public void BindSingleHabitat(Habitat habitat, DataGridViewRow row, Empire defaultEmpire)
        {
            row.Height = 30;
            if (habitat != null)
            {
                if (habitat.Empire != null)
                {
                    ((SortableImageCell)row.Cells[0]).ScaledImage = habitat.Empire.SmallFlagPicture;
                    ((SortableImageCell)row.Cells[0]).Value = habitat.Empire.Name;
                    row.Cells[0].ToolTipText = habitat.Empire.Name;
                }
                Bitmap value = _HabitatImages[habitat.PictureRef];
                row.Cells[1].Value = value;
                row.Cells[1].ToolTipText = "";
                row.Cells[2].Value = habitat.Name;
                row.Cells[2].Tag = habitat.HabitatIndex + StellarObjectList.HabitatIndexRangeIncrease;
                string value2 = Galaxy.ResolveDescription(habitat.Type) + " " + Galaxy.ResolveDescription(habitat.Category);
                row.Cells[3].Value = value2;
                string value3 = "(" + TextResolver.GetText("None") + ")";
                row.Cells[4].Value = value3;
                string value4 = string.Empty;
                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                if (habitat2 != null)
                {
                    value4 = habitat2.Name;
                }
                row.Cells[5].Value = value4;
                row.Cells[6].Value = 0;
                row.Cells[7].Value = 0;
                row.Cells[8].Value = 0.0;
                row.Cells[9].Value = "(" + TextResolver.GetText("None") + ")";
                ((SortableImageCell)row.Cells[10]).ScaledImage = null;
                ((SortableImageCell)row.Cells[10]).Value = "";
                row.Cells[10].ToolTipText = "";
            }
        }

        public void BindSingleBuiltObject(BuiltObject builtObject, DataGridViewRow row, Empire defaultEmpire, Galaxy galaxy)
        {
            row.Height = 30;
            Empire empire = builtObject.Empire;
            if (builtObject.PirateEmpireId > 0)
            {
                empire = galaxy.GetEmpireById(builtObject.PirateEmpireId);
            }
            if (empire != null)
            {
                ((SortableImageCell)row.Cells[0]).ScaledImage = empire.SmallFlagPicture;
                ((SortableImageCell)row.Cells[0]).Value = empire.Name;
                row.Cells[0].ToolTipText = empire.Name;
            }
            else
            {
                row.Cells[0].ToolTipText = "(" + TextResolver.GetText("No Empire") + ")";
            }
            Bitmap value = _BuiltObjectImages[builtObject.PictureRef];
            row.Cells[1].Value = value;
            row.Cells[1].ToolTipText = builtObject.Design.Name + " (" + builtObject.SubRole.ToString() + ")";
            string text = builtObject.Name;
            if (builtObject.Empire == galaxy.IndependentEmpire && empire != builtObject.Empire && builtObject.PirateEmpireId > 0)
            {
                text = text + " (" + TextResolver.GetText("Smuggler") + ")";
            }
            row.Cells[2].Value = text;
            row.Cells[2].Tag = builtObject.BuiltObjectID;
            if (_ShowDetails)
            {
                if (builtObject.DamagedComponentCount > 0)
                {
                    row.Cells[2].Style.ForeColor = Color.Red;
                    row.Cells[2].Style.SelectionForeColor = Color.Red;
                    string text2 = string.Format(TextResolver.GetText("X components damaged"), builtObject.DamagedComponentCount.ToString());
                    if (builtObject.Role == BuiltObjectRole.Base)
                    {
                        text2 = ((builtObject.ParentHabitat == null || builtObject.ParentHabitat.Population == null || builtObject.ParentHabitat.Population.Count <= 0 || builtObject.ParentHabitat.Empire != defaultEmpire) ? (text2 + " (" + TextResolver.GetText("send a construction ship to repair") + ")") : (text2 + " (" + TextResolver.GetText("repairing at colony") + ")"));
                    }
                    else if (builtObject.WarpSpeed <= 0)
                    {
                        text2 = text2 + " (" + TextResolver.GetText("no hyperdrive, cannot travel for repairs") + ")";
                    }
                    row.Cells[2].ToolTipText = text2;
                }
                else if (builtObject.UnbuiltComponentCount > 0)
                {
                    row.Cells[2].Style.ForeColor = Color.Orange;
                    row.Cells[2].Style.SelectionForeColor = Color.Orange;
                    string toolTipText = string.Format(TextResolver.GetText("Under construction (X components unbuilt)"), builtObject.UnbuiltComponentCount.ToString());
                    row.Cells[2].ToolTipText = toolTipText;
                }
            }
            string value2 = Galaxy.ResolveDescription(builtObject.Role) + ", " + Galaxy.ResolveDescription(builtObject.SubRole);
            row.Cells[3].Value = value2;
            string value3 = "(" + TextResolver.GetText("None") + ")";
            if (builtObject.Mission != null && builtObject.Mission.Type != 0)
            {
                value3 = Galaxy.ResolveDescription(builtObject.Mission.Type);
            }
            row.Cells[4].Value = value3;
            string value4 = string.Empty;
            if (builtObject.NearestSystemStar != null)
            {
                value4 = builtObject.NearestSystemStar.Name;
            }
            if (string.IsNullOrEmpty(value4))
            {
                value4 = "(" + TextResolver.GetText("Deep Space") + ")";
            }
            row.Cells[5].Value = value4;
            row.Cells[6].Value = builtObject.FirepowerRaw;
            row.Cells[7].Value = (int)builtObject.TopSpeed;
            double num = builtObject.AnnualSupportCost;
            double num2 = 0.0;
            if (builtObject.Empire != null)
            {
                num2 = num * builtObject.Empire.ShipMaintenanceSavings;
            }
            num -= num2;
            row.Cells[8].Value = num;
            if (builtObject.ShipGroup != null)
            {
                row.Cells[9].Value = builtObject.ShipGroup.Name;
            }
            else
            {
                row.Cells[9].Value = "(" + TextResolver.GetText("None") + ")";
            }
            string text3 = TextResolver.GetText("Not automated");
            if (builtObject.IsAutoControlled)
            {
                ((SortableImageCell)row.Cells[10]).ScaledImage = _AutomationImage;
                text3 = TextResolver.GetText("Automated");
            }
            ((SortableImageCell)row.Cells[10]).Value = text3;
            row.Cells[10].ToolTipText = text3;
        }

        public void BindData(BuiltObjectList builtObjects, Galaxy galaxy)
        {
            BindData(builtObjects, galaxy, showDetails: false);
        }

        public void BindData(BuiltObjectList builtObjects, Galaxy galaxy, bool showDetails)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            stellarObjectList.AddRange(builtObjects);
            BindDataGeneric(stellarObjectList, galaxy, showDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Name = "BuiltObjectListView";
            base.Size = new System.Drawing.Size(247, 157);
            base.ResumeLayout(false);
        }
    }
}
