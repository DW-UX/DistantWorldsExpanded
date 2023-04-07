// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireListViewBasic
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireListViewBasic : ListViewBase
    {
        private DistantWorlds.Types.EmpireList _Empires;
        private IContainer components;

        public EmpireListViewBasic()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn1 = new DataGridViewImageColumn();
            gridViewImageColumn1.Description = "Empire";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "Empire";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ValueType = typeof(Image);
            gridViewImageColumn1.Width = 25;
            gridViewImageColumn1.FillWeight = 25f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
            DataGridViewImageColumn gridViewImageColumn2 = (DataGridViewImageColumn)new EmpireListViewBasic.SortableImageColumn();
            gridViewImageColumn2.Description = "Race";
            gridViewImageColumn2.HeaderText = "";
            gridViewImageColumn2.Name = "Race";
            gridViewImageColumn2.ReadOnly = true;
            gridViewImageColumn2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn2.ValueType = typeof(string);
            gridViewImageColumn2.Width = 30;
            gridViewImageColumn2.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
            viewTextBoxColumn.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn.Name = "Name";
            viewTextBoxColumn.ReadOnly = false;
            viewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn.ValueType = typeof(string);
            viewTextBoxColumn.Width = 135;
            viewTextBoxColumn.FillWeight = 135f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn);
        }

        public Empire SelectedEmpire
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Empire)null;
                //index = -1;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index)
                { return this._Empires[index]; }
                else { return (Empire)null; }
            }
        }

        public void ClearData() => this._Empires = (DistantWorlds.Types.EmpireList)null;

        public void BindData(DistantWorlds.Types.EmpireList empires, Bitmap[] raceImages)
        {
            this._Empires = empires;
            this._Grid.Rows.Clear();
            if (empires != null)
            {
                for (int index = 0; index < empires.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.Cells[0].Value = (object)empires[index].SmallFlagPicture;
                    ((EmpireListViewBasic.SortableImageCell)row.Cells[1]).ScaledImage = raceImages[empires[index].DominantRace.PictureRef];
                    row.Cells[1].Value = (object)empires[index].DominantRace.Name;
                    row.Cells[1].ToolTipText = empires[index].DominantRace.Name;
                    row.Cells[2].Value = (object)empires[index].Name;
                    row.Cells[2].Tag = (object)index;
                }
            }
            this.RememberSorting();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new EmpireListViewBasic.SortableImageCell();
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
