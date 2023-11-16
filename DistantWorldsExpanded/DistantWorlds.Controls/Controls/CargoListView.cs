// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CargoListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class CargoListView : ListViewBase
    {
        private IContainer components;
        private CargoList _CargoList;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();

        public CargoListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn1 = new DataGridViewImageColumn();
            gridViewImageColumn1.Description = "Empire";
            gridViewImageColumn1.HeaderText = TextResolver.GetText("Empire");
            gridViewImageColumn1.Name = "Empire";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ValueType = typeof(Image);
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
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Amount Abbreviation");
            viewTextBoxColumn2.Name = "Amount";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(int);
            viewTextBoxColumn2.Width = 50;
            viewTextBoxColumn2.FillWeight = 50f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Reserved Abbreviation");
            viewTextBoxColumn3.Name = "Reserved";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 50;
            viewTextBoxColumn3.FillWeight = 50f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
        }

        public Cargo SelectedCargo
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Cargo)null;
                //index = -1;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index)
                { return this._CargoList[index]; }
                else { return (Cargo)null; }
            }
        }

        public void ClearData() => this._CargoList = (CargoList)null;

        public void BindData(
          CargoList cargoList,
          Bitmap[] componentImages,
          Bitmap[] resourceImages,
          Bitmap[] designImages,
          Galaxy galaxy)
        {
            this._CargoList = cargoList;
            this._Grid.Rows.Clear();
            if (cargoList != null)
            {
                for (int index = 0; index < cargoList.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    if (galaxy != null)
                    {
                        Empire empire = galaxy.GetEmpireById(cargoList[index].EmpireId);
                        if (empire == null && cargoList[index].EmpireId == galaxy.IndependentEmpire.EmpireId)
                            empire = galaxy.IndependentEmpire;
                        if (empire != null)
                        {
                            row.Cells[0].Value = (object)empire.SmallFlagPicture;
                            row.Cells[0].ToolTipText = empire.Name;
                        }
                        else
                        {
                            row.Cells[0].Value = (object)null;
                            row.Cells[0].ToolTipText = "(" + TextResolver.GetText("None") + ")";
                        }
                    }
                    if (cargoList[index].CommodityComponent != null)
                    {
                        DistantWorlds.Types.Component commodityComponent = cargoList[index].CommodityComponent;
                        int pictureRef = commodityComponent.PictureRef;
                        row.Cells[2].Value = (object)commodityComponent.Name;
                        row.Cells[1].Value = (object)componentImages[pictureRef];
                    }
                    else if (cargoList[index].CommodityResource != null)
                    {
                        Resource commodityResource = cargoList[index].CommodityResource;
                        int pictureRef = commodityResource.PictureRef;
                        row.Cells[2].Value = (object)commodityResource.Name;
                        row.Cells[1].Value = (object)resourceImages[pictureRef];
                    }
                    row.Cells[2].Tag = (object)index;
                    row.Cells[3].Value = (object)cargoList[index].Amount;
                    row.Cells[4].Value = (object)cargoList[index].Reserved;
                }
            }
            this.RememberSorting();
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new CargoListView.SortableImageCell();
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
