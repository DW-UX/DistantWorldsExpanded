// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireListView : ListViewBase
    {
        private DistantWorlds.Types.EmpireList _Empires;
        private RaceImageCache _RaceImageCache;
        private IContainer components;

        public EmpireListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn1 = new DataGridViewImageColumn();
            gridViewImageColumn1.Description = "Empire";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "Empire";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ValueType = typeof(Image);
            gridViewImageColumn1.Width = 45;
            gridViewImageColumn1.FillWeight = 45f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
            DataGridViewImageColumn gridViewImageColumn2 = (DataGridViewImageColumn)new EmpireListView.SortableImageColumn();
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
            DataGridViewTextBoxDropShadowColumn dropShadowColumn = new DataGridViewTextBoxDropShadowColumn();
            dropShadowColumn.UseDropShadow = true;
            dropShadowColumn.HeaderText = TextResolver.GetText("Name");
            dropShadowColumn.Name = "Name";
            dropShadowColumn.ReadOnly = false;
            dropShadowColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            dropShadowColumn.ValueType = typeof(string);
            dropShadowColumn.Width = 170;
            dropShadowColumn.FillWeight = 170f;
            this._Grid.Columns.Add((DataGridViewColumn)dropShadowColumn);
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
                else
                { return (Empire)null; }               
            }
        }

        public void SelectEmpire(Empire empire)
        {
            int num1 = this._Empires.IndexOf(empire);
            if (num1 < 0)
                return;
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                DataGridViewRow row = this._Grid.Rows[index];
                object tag = row.Cells[2].Tag;
                if (tag is int num2)
                {
                    if (num2 == num1)
                    {
                        row.Selected = true;
                        this._Grid.FirstDisplayedScrollingRowIndex = index;
                    }
                }
            }
        }

        public DistantWorlds.Types.EmpireList Empires => this._Empires;

        public void ClearData() => this._Empires = (DistantWorlds.Types.EmpireList)null;

        public void BindData(DistantWorlds.Types.EmpireList empires, RaceImageCache raceImageCache)
        {
            this._Empires = empires;
            this._RaceImageCache = raceImageCache;
            this._Grid.Rows.Clear();
            this._Grid.Columns[0].Width = 46;
            this._Grid.Columns[1].Width = 30;
            if (empires != null)
            {
                int num1 = 0;
                for (int index = 0; index < empires.Count; ++index)
                {
                    Empire empire = empires[index];
                    if (empire != null && empire.TotalColonyStrategicValue > num1)
                        num1 = empire.TotalColonyStrategicValue;
                }
                Font font = new Font(this._Grid.Font.FontFamily, 17f, FontStyle.Bold, GraphicsUnit.Pixel);
                for (int index = 0; index < empires.Count; ++index)
                {
                    Empire empire = empires[index];
                    if (empire != null)
                    {
                        this._Grid.Rows.Add();
                        DataGridViewRow row = this._Grid.Rows[index];
                        int height = 24;
                        double num2 = (double)empire.LargeFlagPicture.Height / (double)height;
                        int width = (int)((double)empire.LargeFlagPicture.Width / num2);
                        Bitmap bitmap = this.PrescaleImage(empire.LargeFlagPicture, width, height);
                        row.Cells[0].Value = (object)bitmap;
                        if (empire.DominantRace != null)
                        {
                            ((EmpireListView.SortableImageCell)row.Cells[1]).ScaledImage = this._RaceImageCache.GetEmpireDominantRaceImage(empire, true, false, false);
                            row.Cells[1].Value = (object)empire.DominantRace.Name;
                            ((DataGridViewImageCell)row.Cells[1]).ImageLayout = DataGridViewImageCellLayout.Normal;
                            row.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            row.Cells[1].ToolTipText = empire.DominantRace.Name;
                        }
                        row.Cells[2].Value = (object)empire.Name;
                        row.Cells[2].Tag = (object)index;
                        row.Cells[2].Style.ForeColor = empire.MainColor;
                        row.Cells[2].Style.Font = font;
                        ((DataGridViewTextBoxDropShadowCell)row.Cells[2]).MaximumAmount = num1;
                        ((DataGridViewTextBoxDropShadowCell)row.Cells[2]).Amount = empire.TotalColonyStrategicValue;
                    }
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
                this.CellTemplate = (DataGridViewCell)new EmpireListView.SortableImageCell();
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
