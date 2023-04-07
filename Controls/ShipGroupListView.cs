// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ShipGroupListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class ShipGroupListView : ListViewBase
    {
        private IContainer components;
        private ShipGroupList _ShipGroups;

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

        public ShipGroupListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn = (DataGridViewImageColumn)new ShipGroupListView.SortableImageColumn();
            gridViewImageColumn.Description = "Admirals & Generals";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "Admirals & Generals";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn.ValueType = typeof(string);
            gridViewImageColumn.Width = 30;
            gridViewImageColumn.FillWeight = 30f;
            gridViewImageColumn.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
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
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Ships");
            viewTextBoxColumn2.Name = "Ships";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(int);
            viewTextBoxColumn2.Width = 40;
            viewTextBoxColumn2.FillWeight = 40f;
            viewTextBoxColumn2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Power");
            viewTextBoxColumn3.Name = "Firepower";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 40;
            viewTextBoxColumn3.FillWeight = 40f;
            viewTextBoxColumn3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("Troops");
            viewTextBoxColumn4.Name = "Troops";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(int);
            viewTextBoxColumn4.Width = 40;
            viewTextBoxColumn4.FillWeight = 40f;
            viewTextBoxColumn4.DefaultCellStyle.Format = "0,K";
            viewTextBoxColumn4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn5.HeaderText = TextResolver.GetText("Home colony");
            viewTextBoxColumn5.Name = "Home colony";
            viewTextBoxColumn5.ReadOnly = false;
            viewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn5.ValueType = typeof(string);
            viewTextBoxColumn5.Width = 90;
            viewTextBoxColumn5.FillWeight = 90f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
            DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn6.HeaderText = TextResolver.GetText("Mission");
            viewTextBoxColumn6.Name = "Mission";
            viewTextBoxColumn6.ReadOnly = false;
            viewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn6.ValueType = typeof(string);
            viewTextBoxColumn6.Width = 180;
            viewTextBoxColumn6.FillWeight = 180f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
            DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn7.HeaderText = TextResolver.GetText("Current system");
            viewTextBoxColumn7.Name = "Current system";
            viewTextBoxColumn7.ReadOnly = false;
            viewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn7.ValueType = typeof(string);
            viewTextBoxColumn7.Width = 90;
            viewTextBoxColumn7.FillWeight = 90f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn7);
        }

        public ShipGroup SelectedShipGroup
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (ShipGroup)null;
                //index = -1;
                object tag = selectedRows[0].Cells[3].Tag;
                if (tag is int index)
                { return this._ShipGroups[index]; }
                else { return (ShipGroup)null; }
            }
        }

        public void SelectShipGroup(ShipGroup shipGroupToSelect)
        {
            int num1 = -1;
            if (shipGroupToSelect == null)
                return;
            for (int index = 0; index < this._ShipGroups.Count; ++index)
            {
                if (this._ShipGroups[index] == shipGroupToSelect)
                {
                    num1 = index;
                    break;
                }
            }
            if (num1 < 0)
                return;
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                object tag = this._Grid.Rows[index].Cells[3].Tag;
                if (tag != null && tag is int num2 && num2 == num1)
                {
                    this._Grid.Rows[index].Selected = true;
                    this._Grid.FirstDisplayedScrollingRowIndex = index;
                    break;
                }
            }
        }

        public void ClearData() => this._ShipGroups = (ShipGroupList)null;

        public void BindData(ShipGroupList shipGroups, CharacterImageCache characterImageCache)
        {
            this._ShipGroups = shipGroups;
            this._Grid.SuspendLayout();
            this._Grid.Rows.Clear();
            if (shipGroups != null)
            {
                for (int index1 = 0; index1 < shipGroups.Count; ++index1)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index1];
                    if (shipGroups[index1].Empire != null && shipGroups[index1].Empire.Characters != null)
                    {
                        CharacterList admiralsAndGenerals = shipGroups[index1].Empire.Characters.GetFleetAdmiralsAndGenerals(shipGroups[index1]);
                        if (admiralsAndGenerals != null && admiralsAndGenerals.Count > 0)
                        {
                            ((ShipGroupListView.SortableImageCell)row.Cells[0]).ScaledImage = characterImageCache.ObtainCharacterImageVerySmall(admiralsAndGenerals[0]);
                            string str = string.Empty;
                            for (int index2 = 0; index2 < admiralsAndGenerals.Count; ++index2)
                                str = str + admiralsAndGenerals[index2].Name + ", ";
                            if (!string.IsNullOrEmpty(str) && str.Length >= 2)
                                str = str.Substring(0, str.Length - 2);
                            row.Cells[0].ToolTipText = str;
                        }
                    }
                    row.Cells[1].Value = (object)shipGroups[index1].Name;
                    row.Cells[2].Value = (object)shipGroups[index1].Ships.Count;
                    row.Cells[3].Value = (object)shipGroups[index1].TotalFirepower;
                    row.Cells[3].Tag = (object)index1;
                    row.Cells[4].Value = (object)shipGroups[index1].TotalTroopAttackStrength;
                    row.Cells[5].Value = shipGroups[index1].GatherPoint == null ? (object)("(" + TextResolver.GetText("None") + ")") : (object)shipGroups[index1].GatherPoint.Name;
                    row.Cells[6].Value = (object)Galaxy.ResolveDescription(shipGroups[index1].Empire, shipGroups[index1].Mission);
                    row.Cells[7].Value = shipGroups[index1].LeadShip.NearestSystemStar == null ? (object)("(" + TextResolver.GetText("Deep Space") + ")") : (object)shipGroups[index1].LeadShip.NearestSystemStar.Name;
                }
            }
            this.RememberSorting();
            this._Grid.ResumeLayout();
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new ShipGroupListView.SortableImageCell();
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

        private class SortableImageNumberColumn : DataGridViewImageColumn
        {
            public SortableImageNumberColumn()
            {
                this.CellTemplate = (DataGridViewCell)new ShipGroupListView.SortableImageNumberCell();
                this.ValueType = typeof(int);
            }
        }

        private class SortableImageNumberCell : DataGridViewImageCell
        {
            private Bitmap _Bitmap;

            public SortableImageNumberCell() => this.ValueType = typeof(int);

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

            public override object DefaultNewRowValue => (object)0;
        }
    }
}
