// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResearchFacilities
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
    public class ResearchFacilities : ListViewBase
    {
        private BuiltObjectList _BuiltObjects;
        private IContainer components;

        public ResearchFacilities()
        {
            this.InitializeComponent();
            this._Grid.RowTemplate.Height = 25;
            DataGridViewImageColumn gridViewImageColumn1 = (DataGridViewImageColumn)new ResearchFacilities.SortableImageColumn();
            gridViewImageColumn1.Description = "Empire";
            gridViewImageColumn1.HeaderText = "";
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
            viewTextBoxColumn1.Width = 125;
            viewTextBoxColumn1.FillWeight = 125f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Weapons");
            viewTextBoxColumn2.Name = "Weapons";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(int);
            viewTextBoxColumn2.Width = 100;
            viewTextBoxColumn2.FillWeight = 100f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Energy");
            viewTextBoxColumn3.Name = "Energy";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 100;
            viewTextBoxColumn3.FillWeight = 100f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("HighTech");
            viewTextBoxColumn4.Name = "HighTech";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(int);
            viewTextBoxColumn4.Width = 100;
            viewTextBoxColumn4.FillWeight = 100f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
        }

        public BuiltObject SelectedBuiltObject
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (BuiltObject)null;
                //builtObjectId = -1;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int builtObjectId)
                { return this._BuiltObjects.FindBuiltObjectById(builtObjectId); }
                else { return (BuiltObject)null; }
            }
        }

        public void SelectBuiltObject(BuiltObject builtObjectToSelect)
        {
            int num1 = -1;
            if (builtObjectToSelect == null)
                return;
            for (int index = 0; index < this._BuiltObjects.Count; ++index)
            {
                if (this._BuiltObjects[index] == builtObjectToSelect)
                {
                    num1 = this._BuiltObjects[index].BuiltObjectID;
                    break;
                }
            }
            if (num1 < 0)
                return;
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

        public void ClearData() => this._BuiltObjects = (BuiltObjectList)null;

        public void BindData(BuiltObjectList builtObjects, Bitmap[] images)
        {
            this._BuiltObjects = builtObjects;
            this._Grid.SuspendLayout();
            this._Grid.Rows.Clear();
            if (builtObjects != null)
            {
                for (int index = 0; index < builtObjects.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.Height = 30;
                    BuiltObject builtObject = builtObjects[index];
                    if (builtObject != null)
                    {
                        ((ResearchFacilities.SortableImageCell)row.Cells[0]).ScaledImage = builtObject.Empire.SmallFlagPicture;
                        row.Cells[0].Value = (object)builtObject.Empire.Name;
                        row.Cells[0].ToolTipText = builtObject.Empire.Name;
                        Bitmap image = (Bitmap)null;
                        if (images.Length > builtObject.PictureRef)
                            image = images[builtObject.PictureRef];
                        Bitmap bitmap = (Bitmap)null;
                        if (image != null && image.PixelFormat != PixelFormat.Undefined)
                        {
                            bitmap = this.PrepareBuiltObjectImage(image, builtObject.Empire.MainColor, builtObject.Empire.SecondaryColor, 1, 1);
                            bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        }
                        row.Cells[1].Value = (object)bitmap;
                        row.Cells[1].ToolTipText = builtObject.Design.Name + " (" + builtObject.SubRole.ToString() + ")";
                        row.Cells[2].Value = (object)builtObject.Name;
                        row.Cells[2].Tag = (object)builtObject.BuiltObjectID;
                        row.Cells[3].Value = (object)this.CalculateCurrentResearch(builtObject, (double)builtObject.ResearchWeapons);
                        row.Cells[3].Style.Format = "#0,K";
                        row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        row.Cells[4].Value = (object)this.CalculateCurrentResearch(builtObject, (double)builtObject.ResearchEnergy);
                        row.Cells[4].Style.Format = "#0,K";
                        row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        row.Cells[5].Value = (object)this.CalculateCurrentResearch(builtObject, (double)builtObject.ResearchHighTech);
                        row.Cells[5].Style.Format = "#0,K";
                        row.Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            this.RememberSorting();
            this._Grid.ResumeLayout();
        }

        private int CalculateCurrentResearch(BuiltObject builtObject, double researchOutput) => builtObject.IsFunctional ? (int)researchOutput : 0;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Font;
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new ResearchFacilities.SortableImageCell();
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
