// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DesignListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class DesignListView : ListViewBase
    {
        private IContainer components;
        private Galaxy _Galaxy;
        private DesignList _Designs;
        private Bitmap _ObsoleteImage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();

        public DesignListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn1 = (DataGridViewImageColumn)new DesignListView.SortableImageColumn();
            gridViewImageColumn1.Description = "EmpirePicture";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "EmpirePicture";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ValueType = typeof(Image);
            gridViewImageColumn1.Width = 30;
            gridViewImageColumn1.FillWeight = 30f;
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
            viewTextBoxColumn1.Width = 120;
            viewTextBoxColumn1.FillWeight = 120f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Role");
            viewTextBoxColumn2.Name = "Role";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 70;
            viewTextBoxColumn2.FillWeight = 70f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("SubRole");
            viewTextBoxColumn3.Name = "SubRole";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(string);
            viewTextBoxColumn3.Width = 100;
            viewTextBoxColumn3.FillWeight = 100f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("Cost");
            viewTextBoxColumn4.Name = "Cost";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(double);
            viewTextBoxColumn4.Width = 40;
            viewTextBoxColumn4.FillWeight = 40f;
            viewTextBoxColumn4.DefaultCellStyle.Format = "#####0";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn5.HeaderText = TextResolver.GetText("Maintenance Abbreviation");
            viewTextBoxColumn5.Name = "Maintenance";
            viewTextBoxColumn5.ReadOnly = false;
            viewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn5.ValueType = typeof(double);
            viewTextBoxColumn5.Width = 40;
            viewTextBoxColumn5.FillWeight = 40f;
            viewTextBoxColumn5.DefaultCellStyle.Format = "#####0";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
            DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn6.HeaderText = TextResolver.GetText("Date Created");
            viewTextBoxColumn6.Name = "DateCreated";
            viewTextBoxColumn6.ReadOnly = false;
            viewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn6.ValueType = typeof(string);
            viewTextBoxColumn6.Width = 70;
            viewTextBoxColumn6.FillWeight = 70f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
            DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn7.HeaderText = TextResolver.GetText("Size");
            viewTextBoxColumn7.Name = "Size";
            viewTextBoxColumn7.ReadOnly = false;
            viewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn7.ValueType = typeof(int);
            viewTextBoxColumn7.Width = 40;
            viewTextBoxColumn7.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn7);
            DataGridViewTextBoxColumn viewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn8.HeaderText = TextResolver.GetText("Amount");
            viewTextBoxColumn8.Name = "BuildCount";
            viewTextBoxColumn8.ReadOnly = false;
            viewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn8.ValueType = typeof(int);
            viewTextBoxColumn8.Width = 40;
            viewTextBoxColumn8.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn8);
            DataGridViewTextBoxColumn viewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn9.HeaderText = TextResolver.GetText("Upgrade");
            viewTextBoxColumn9.Name = "Upgrade";
            viewTextBoxColumn9.ReadOnly = false;
            viewTextBoxColumn9.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn9.ValueType = typeof(string);
            viewTextBoxColumn9.Width = 40;
            viewTextBoxColumn9.FillWeight = 40f;
            viewTextBoxColumn9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn9);
            DataGridViewTextBoxColumn viewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn10.HeaderText = TextResolver.GetText("Retrofit");
            viewTextBoxColumn10.Name = "AutoRetrofit";
            viewTextBoxColumn10.ReadOnly = false;
            viewTextBoxColumn10.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn10.ValueType = typeof(string);
            viewTextBoxColumn10.Width = 40;
            viewTextBoxColumn10.FillWeight = 40f;
            viewTextBoxColumn10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn10);
            DataGridViewTextBoxColumn viewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn11.HeaderText = TextResolver.GetText("Optimized");
            viewTextBoxColumn11.Name = "Optimized";
            viewTextBoxColumn11.ReadOnly = false;
            viewTextBoxColumn11.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn11.ValueType = typeof(string);
            viewTextBoxColumn11.Width = 40;
            viewTextBoxColumn11.FillWeight = 40f;
            viewTextBoxColumn11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn11);
            DataGridViewImageColumn gridViewImageColumn3 = (DataGridViewImageColumn)new DesignListView.SortableImageColumn();
            gridViewImageColumn3.Description = TextResolver.GetText("Obsolete");
            gridViewImageColumn3.HeaderText = TextResolver.GetText("Obsolete");
            gridViewImageColumn3.Name = "Obsolete";
            gridViewImageColumn3.ReadOnly = true;
            gridViewImageColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn3.ValueType = typeof(string);
            gridViewImageColumn3.Width = 30;
            gridViewImageColumn3.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn3);
        }

        public DesignList SelectedDesigns
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                DesignList selectedDesigns = new DesignList();
                foreach (DataGridViewRow row in (BaseCollection)selectedRows)
                {
                    Design design = this.ResolveDesign(row);
                    if (design != null)
                        selectedDesigns.Add(design);
                }
                return selectedDesigns;
            }
        }

        public void SetObsolete(DataGridViewRow row, bool obsolete)
        {
            string text = TextResolver.GetText("Not obsolete");
            Bitmap bitmap = (Bitmap)null;
            if (obsolete)
            {
                text = TextResolver.GetText("Obsolete");
                bitmap = this._ObsoleteImage;
            }
          ((DesignListView.SortableImageCell)row.Cells["Obsolete"]).ScaledImage = bitmap;
            row.Cells["Obsolete"].Value = (object)text;
            row.Cells["Obsolete"].ToolTipText = text;
        }

        public Design ResolveDesign(DataGridViewRow row)
        {
            //index = -1;
            object tag = row.Cells[2].Tag;
            if (tag is int index)
            { return this._Designs[index]; }
            else { return (Design)null; }
        }

        public Design SelectedDesign
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Design)null;
                //index = -1;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index)
                { return this._Designs[index]; }
                else { return (Design)null; }
            }
        }

        public void SelectDesign(Design designToSelect)
        {
            int num1 = -1;
            if (designToSelect == null)
                return;
            for (int index = 0; index < this._Designs.Count; ++index)
            {
                if (this._Designs[index] == designToSelect)
                {
                    num1 = index;
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

        public void ClearData()
        {
            this._Galaxy = (Galaxy)null;
            this._Designs = (DesignList)null;
        }

        private Bitmap GenerateObsoleteImage()
        {
            Bitmap obsoleteImage = new Bitmap(16, 16, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)obsoleteImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.Red, 2f))
                {
                    pen.EndCap = LineCap.Round;
                    pen.StartCap = LineCap.Round;
                    graphics.DrawLine(pen, 4f, 2f, 11f, 14f);
                    graphics.DrawLine(pen, 11f, 2f, 4f, 14f);
                    graphics.DrawLine(pen, 1f, 8f, 14f, 8f);
                }
            }
            return obsoleteImage;
        }

        public void BindData(
          Galaxy galaxy,
          DesignList designs,
          Bitmap[] builtObjectImages,
          bool allowMultiSelect)
        {
            this._Galaxy = galaxy;
            this._Designs = designs;
            this._Grid.MultiSelect = allowMultiSelect;
            this._ObsoleteImage = this.GenerateObsoleteImage();
            this._Grid.SuspendLayout();
            this._Grid.Rows.Clear();
            if (designs != null && designs.Count > 0)
                this._Grid.Rows.Add(designs.Count);
            if (designs != null)
            {
                for (int index1 = 0; index1 < designs.Count; ++index1)
                {
                    DataGridViewRow row = this._Grid.Rows[index1];
                    Color color = Color.Empty;
                    string str1 = string.Empty;
                    if (designs[index1].IsManuallyCreated)
                    {
                        color = Color.FromArgb((int)byte.MaxValue, 102, 0);
                        str1 = TextResolver.GetText("This design is manually created");
                    }
                  ((DesignListView.SortableImageCell)row.Cells[0]).ScaledImage = designs[index1].Empire.SmallFlagPicture;
                    row.Cells[0].Value = (object)designs[index1].Empire.Name;
                    Bitmap bitmap = this.PrepareBuiltObjectImage(builtObjectImages[designs[index1].PictureRef], designs[index1].Empire.MainColor, designs[index1].Empire.SecondaryColor, 1, 1);
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    row.Cells[1].Value = (object)bitmap;
                    row.Cells[2].Value = (object)designs[index1].Name;
                    row.Cells[2].Tag = (object)index1;
                    row.Cells[3].Value = (object)Galaxy.ResolveDescription(designs[index1].Role);
                    row.Cells[4].Value = (object)Galaxy.ResolveDescription(designs[index1].SubRole);
                    double num1 = 0.0;
                    double num2 = 0.0;
                    if (designs[index1] != null)
                    {
                        num1 = designs[index1].CalculateCurrentPurchasePrice(this._Galaxy);
                        num2 = designs[index1].CalculateMaintenanceCosts(this._Galaxy, designs[index1].Empire);
                    }
                    row.Cells[5].Value = (object)num1;
                    row.Cells[6].Value = (object)num2;
                    long dateCreated = designs[index1].DateCreated;
                    int num3 = (int)(dateCreated / (long)(1000 * Galaxy.RealSecondsInGalacticYear));
                    long num4 = (long)(num3 * (1000 * Galaxy.RealSecondsInGalacticYear));
                    int num5 = (int)((dateCreated - num4) / (long)(100 * Galaxy.RealSecondsInGalacticYear));
                    long num6 = (long)(num5 * (100 * Galaxy.RealSecondsInGalacticYear));
                    int num7 = (int)((dateCreated - (num4 + num6)) / 2000L);
                    int num8 = num5 + 1;
                    int num9 = num7 + 1;
                    string str2 = num3.ToString("0000") + "." + num8.ToString("00") + "." + num9.ToString("00");
                    row.Cells[7].Value = (object)str2;
                    row.Cells[8].Value = (object)designs[index1].Size;
                    int num10 = 0;
                    for (int index2 = 0; index2 < designs[index1].Empire.BuiltObjects.Count; ++index2)
                    {
                        if (designs[index1].Empire.BuiltObjects[index2].Design == designs[index1])
                            ++num10;
                    }
                    for (int index3 = 0; index3 < designs[index1].Empire.PrivateBuiltObjects.Count; ++index3)
                    {
                        if (designs[index1].Empire.PrivateBuiltObjects[index3].Design == designs[index1])
                            ++num10;
                    }
                    row.Cells[9].Value = (object)num10;
                    string text1 = TextResolver.GetText("Automatic");
                    if (!this._Galaxy.PlayerEmpire.CheckDesignSubRoleShouldBeUpgraded(designs[index1].SubRole))
                        text1 = TextResolver.GetText("Manual");
                    row.Cells[10].Value = (object)text1;
                    bool flag = true;
                    switch (designs[index1].SubRole)
                    {
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                        case BuiltObjectSubRole.PassengerShip:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                            flag = false;
                            break;
                    }
                    string text2 = TextResolver.GetText("Automatic");
                    if (!designs[index1].AllowAutoRetrofit)
                        text2 = TextResolver.GetText("Manual");
                    row.Cells[11].Value = (object)text2;
                    if (flag)
                    {
                        row.Cells[11].ToolTipText = string.Empty;
                        row.Cells[11].Style.ForeColor = Color.FromArgb(170, 170, 170);
                    }
                    else
                    {
                        row.Cells[11].ToolTipText = TextResolver.GetText("Private design - cannot manually retrofit");
                        row.Cells[11].Style.ForeColor = Color.FromArgb(96, 96, 96);
                    }
                    row.Cells[12].Value = (object)TextResolver.GetText("No");
                    if (designs[index1].OptimizedDesign > 0)
                    {
                        row.Cells[12].Value = (object)TextResolver.GetText("Yes");
                        row.Cells[12].ToolTipText = TextResolver.GetText("This is an optimized design");
                    }
                    string text3 = TextResolver.GetText("Not obsolete");
                    if (designs[index1].IsObsolete)
                    {
                        ((DesignListView.SortableImageCell)row.Cells[13]).ScaledImage = this._ObsoleteImage;
                        text3 = TextResolver.GetText("Obsolete");
                    }
                    row.Cells[13].Value = (object)text3;
                    row.Cells[13].ToolTipText = text3;
                    if (color != Color.Empty)
                    {
                        foreach (DataGridViewCell cell in (BaseCollection)row.Cells)
                        {
                            if (!color.IsEmpty)
                            {
                                cell.Style.ForeColor = color;
                                cell.Style.SelectionForeColor = color;
                            }
                            if (!string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(cell.ToolTipText))
                                cell.ToolTipText = str1;
                        }
                    }
                }
            }
            this.RememberSorting();
            this._Grid.ResumeLayout();
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new DesignListView.SortableImageCell();
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
    }
}
