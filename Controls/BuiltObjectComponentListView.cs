// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.BuiltObjectComponentListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class BuiltObjectComponentListView : ListViewBase
    {
        private IContainer components;
        private BuiltObjectComponentList _BuiltObjectComponents;
        private Bitmap[] _Images;
        private Bitmap[] _StatusImages;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();

        public BuiltObjectComponentListView()
        {
            this.InitializeComponent();
            this._Grid.RowTemplate.Height = 34;
            DataGridViewImageColumn gridViewImageColumn = (DataGridViewImageColumn)new BuiltObjectComponentListView.SortableImageColumn();
            gridViewImageColumn.Description = "Picture";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "Picture";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Normal;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn.ValueType = typeof(string);
            gridViewImageColumn.Width = 35;
            gridViewImageColumn.FillWeight = 35f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn1.Name = "Name";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 125;
            viewTextBoxColumn1.FillWeight = 125f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Category");
            viewTextBoxColumn2.Name = "Category";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 90;
            viewTextBoxColumn2.FillWeight = 90f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Size");
            viewTextBoxColumn3.Name = "Size";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 40;
            viewTextBoxColumn3.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
        }

        public BuiltObjectComponent SelectedComponent
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows == null || selectedRows.Count != 1)
                    return (BuiltObjectComponent)null;
                //index = -1;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index && this._BuiltObjectComponents != null && this._BuiltObjectComponents.Count > index && index >= 0)
                {
                    return this._BuiltObjectComponents[index];
                }
                else { return (BuiltObjectComponent)null; }
            }
        }

        public void ClearImages() => GraphicsHelper.DisposeImageArray(this._Images);

        public void InitializeImages(Bitmap[] componentImages, Bitmap[] statusImages)
        {
            this.ClearImages();
            this._Images = new Bitmap[componentImages.Length];
            this._StatusImages = statusImages;
            for (int index = 0; index < componentImages.Length; ++index)
            {
                int width = componentImages[index].Width;
                int height = componentImages[index].Height;
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                Graphics graphics = Graphics.FromImage((Image)bitmap);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImage((Image)componentImages[index], new Rectangle(0, 0, width, height));
                graphics.Dispose();
                this._Images[index] = bitmap;
            }
        }

        public void ClearData() => this._BuiltObjectComponents = (BuiltObjectComponentList)null;

        public void BindData(BuiltObject builtObject)
        {
            this._Grid.Rows.Clear();
            if (builtObject != null)
            {
                this._BuiltObjectComponents = builtObject.Components;
                if (this._BuiltObjectComponents != null && this._BuiltObjectComponents.Count > 0)
                    this._Grid.Rows.Add(this._BuiltObjectComponents.Count);
                if (this._BuiltObjectComponents != null)
                {
                    for (int index = 0; index < this._BuiltObjectComponents.Count; ++index)
                    {
                        BuiltObjectComponent builtObjectComponent = this._BuiltObjectComponents[index];
                        if (builtObjectComponent != null && this._Grid.Rows.Count > index)
                        {
                            DataGridViewRow row = this._Grid.Rows[index];
                            row.Height = 34;
                            bool flag = true;
                            int num = -1;
                            if (builtObject.DisabledComponentIndexes != null)
                                num = builtObject.DisabledComponentIndexes.IndexOf((short)index);
                            if (num >= 0)
                                flag = false;
                            string empty = string.Empty;
                            switch (builtObjectComponent.Status)
                            {
                                case ComponentStatus.Unbuilt:
                                    ((BuiltObjectComponentListView.SortableImageCell)row.Cells[0]).ScaledImage = this._StatusImages[0];
                                    string str1 = "(" + TextResolver.GetText("Under construction") + ") " + builtObjectComponent.Name;
                                    row.Cells[0].Value = (object)str1;
                                    row.Cells[0].ToolTipText = str1;
                                    break;
                                case ComponentStatus.Normal:
                                    if (flag)
                                    {
                                        if (builtObjectComponent.PictureRef < this._Images.Length)
                                            ((BuiltObjectComponentListView.SortableImageCell)row.Cells[0]).ScaledImage = this._Images[builtObjectComponent.PictureRef];
                                        string name = builtObjectComponent.Name;
                                        row.Cells[0].Value = (object)name;
                                        row.Cells[0].ToolTipText = name;
                                        break;
                                    }
                                    if (builtObjectComponent.PictureRef < this._Images.Length)
                                        ((BuiltObjectComponentListView.SortableImageCell)row.Cells[0]).ScaledImage = GraphicsHelper.TransparentImage(this._Images[builtObjectComponent.PictureRef], 0.15f);
                                    string str2 = "(" + TextResolver.GetText("Disabled") + ") " + builtObjectComponent.Name;
                                    row.Cells[0].Value = (object)str2;
                                    row.Cells[0].ToolTipText = str2;
                                    break;
                                case ComponentStatus.Damaged:
                                    ((BuiltObjectComponentListView.SortableImageCell)row.Cells[0]).ScaledImage = this._StatusImages[1];
                                    string str3 = "(" + TextResolver.GetText("Damaged") + ") " + builtObjectComponent.Name;
                                    row.Cells[0].Value = (object)str3;
                                    row.Cells[0].ToolTipText = str3;
                                    break;
                            }
                            row.Cells[1].Value = !flag ? (object)(builtObjectComponent.Name + "  (" + TextResolver.GetText("Disabled") + ")") : (object)builtObjectComponent.Name;
                            row.Cells[2].Value = (object)Galaxy.ResolveDescription(builtObjectComponent.Category);
                            row.Cells[2].Tag = (object)index;
                            row.Cells[3].Value = (object)builtObjectComponent.Size;
                        }
                    }
                }
            }
            this.RememberSorting();
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new BuiltObjectComponentListView.SortableImageCell();
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
