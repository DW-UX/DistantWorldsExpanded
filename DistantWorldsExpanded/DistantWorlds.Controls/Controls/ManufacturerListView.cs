// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ManufacturerListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class ManufacturerListView : ListViewBase
    {
        private IContainer components;
        private ManufacturerList _Manufacturers;

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

        public ManufacturerListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn1 = new DataGridViewImageColumn();
            gridViewImageColumn1.Description = "ParentComponentPicture";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "ParentComponentPicture";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ValueType = typeof(Image);
            gridViewImageColumn1.Width = 30;
            gridViewImageColumn1.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
            DataGridViewImageColumn gridViewImageColumn2 = new DataGridViewImageColumn();
            gridViewImageColumn2.Description = "Component";
            gridViewImageColumn2.HeaderText = "";
            gridViewImageColumn2.Name = "Component";
            gridViewImageColumn2.ReadOnly = true;
            gridViewImageColumn2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn2.ValueType = typeof(Image);
            gridViewImageColumn2.Width = 30;
            gridViewImageColumn2.FillWeight = 30f;
            gridViewImageColumn2.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = "Component";
            viewTextBoxColumn1.Name = "ComponentName";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 150;
            viewTextBoxColumn1.FillWeight = 150f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = "Progress";
            viewTextBoxColumn2.Name = "Progress";
            viewTextBoxColumn2.ReadOnly = true;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(double);
            viewTextBoxColumn2.Width = 40;
            viewTextBoxColumn2.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = "Speed";
            viewTextBoxColumn3.Name = "Speed";
            viewTextBoxColumn3.ReadOnly = true;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 40;
            viewTextBoxColumn3.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
        }

        public Manufacturer SelectedManufacturer
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Manufacturer)null;
                //index = -1;
                object tag = selectedRows[0].Cells[0].Tag;
                if (tag is int index)
                { return this._Manufacturers[index]; }
                else { return (Manufacturer)null; }
            }
        }

        public void ClearData() => this._Manufacturers = (ManufacturerList)null;

        public void BindData(
          ManufacturerList manufacturers,
          Bitmap[] componentImages,
          Bitmap[] builtObjectImages)
        {
            this._Manufacturers = manufacturers;
            this._Grid.Rows.Clear();
            if (manufacturers != null)
            {
                for (int index = 0; index < manufacturers.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.Cells[0].Value = (object)componentImages[manufacturers[index].ParentBuiltObjectComponent.PictureRef];
                    row.Cells[0].ToolTipText = manufacturers[index].ParentBuiltObjectComponent.Name + "(" + manufacturers[index].ParentBuiltObjectComponent.Industry.ToString() + ")";
                    row.Cells[0].Tag = (object)index;
                    if (manufacturers[index].Component != null)
                    {
                        row.Cells[1].Value = (object)componentImages[manufacturers[index].Component.PictureRef];
                        row.Cells[2].Value = (object)manufacturers[index].Component.Name;
                        double num = (double)manufacturers[index].Progress / (double)manufacturers[index].Component.Size;
                        row.Cells[3].Value = (object)num;
                        row.Cells[3].Style.Format = "##0%";
                    }
                    else
                    {
                        row.Cells[1].Value = (object)null;
                        row.Cells[2].Value = (object)string.Empty;
                        row.Cells[3].Value = (object)0;
                    }
                    row.Cells[4].Value = (object)manufacturers[index].ManufacturingSpeed;
                }
            }
            this.RememberSorting();
        }
    }
}
