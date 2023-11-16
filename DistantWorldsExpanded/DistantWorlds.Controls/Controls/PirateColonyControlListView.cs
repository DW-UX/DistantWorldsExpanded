// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.PirateColonyControlListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class PirateColonyControlListView : ListViewBase
    {
        private PirateColonyControlList _PirateColonyControl;
        private Habitat _Habitat;
        private IContainer components;

        public PirateColonyControlListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.Description = "Picture";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "Picture";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn.ValueType = typeof(Image);
            gridViewImageColumn.Width = 35;
            gridViewImageColumn.FillWeight = 35f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
            viewTextBoxColumn.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn.Name = "Name";
            viewTextBoxColumn.ReadOnly = true;
            viewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn.ValueType = typeof(string);
            viewTextBoxColumn.Width = 145;
            viewTextBoxColumn.FillWeight = 145f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn);
            DataGridViewNumericUpDownColumn numericUpDownColumn = new DataGridViewNumericUpDownColumn();
            numericUpDownColumn.HeaderText = TextResolver.GetText("Control") + " %";
            numericUpDownColumn.Name = "Control";
            numericUpDownColumn.ReadOnly = false;
            numericUpDownColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            numericUpDownColumn.Minimum = 0M;
            numericUpDownColumn.Maximum = 100M;
            numericUpDownColumn.Increment = 1M;
            numericUpDownColumn.ValueType = typeof(float);
            numericUpDownColumn.Width = 70;
            numericUpDownColumn.FillWeight = 70f;
            this._Grid.Columns.Add((DataGridViewColumn)numericUpDownColumn);
            DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            viewCheckBoxColumn.HeaderText = TextResolver.GetText("Facilities");
            viewCheckBoxColumn.ToolTipText = TextResolver.GetText("Has control of pirate facilities at colony");
            viewCheckBoxColumn.Name = "Facilities";
            viewCheckBoxColumn.ReadOnly = false;
            viewCheckBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            viewCheckBoxColumn.ValueType = typeof(bool);
            viewCheckBoxColumn.Width = 50;
            viewCheckBoxColumn.FillWeight = 50f;
            this._Grid.Columns.Add((DataGridViewColumn)viewCheckBoxColumn);
        }

        public PirateColonyControlList PirateColonyControl
        {
            get
            {
                PlanetaryFacility planetaryFacility = (PlanetaryFacility)null;
                if (this._Habitat != null && this._Habitat.Facilities != null)
                    planetaryFacility = this._Habitat.Facilities.FindBestPirateFacility(true);
                for (int index1 = 0; index1 < this._Grid.Rows.Count; ++index1)
                {
                    //index2 = -1;
                    object tag = this._Grid.Rows[index1].Cells[1].Tag;
                    if (tag is int index2)
                    {
                        if (index2 >= 0 && this._PirateColonyControl.Count > index2)
                        {
                            float num = (float)this._Grid.Rows[index1].Cells["Control"].Value / 100f;
                            this._PirateColonyControl[index2].ControlLevel = num;
                            bool flag = (bool)this._Grid.Rows[index1].Cells["Facilities"].EditedFormattedValue;
                            if (flag)
                            {
                                if (planetaryFacility == null)
                                {
                                    flag = false;
                                }
                                else
                                {
                                    for (int index3 = 0; index3 < this._Grid.Rows.Count; ++index3)
                                    {
                                        if (index1 != index3 && (bool)this._Grid.Rows[index3].Cells["Facilities"].EditedFormattedValue)
                                            flag = false;
                                    }
                                }
                            }
                            this._PirateColonyControl[index2].HasFacilityControl = flag;
                        }
                    }
                }
                if (planetaryFacility != null && this._PirateColonyControl.GetByFacilityControl() == null)
                    this._PirateColonyControl.GetHighestControl().HasFacilityControl = true;
                return this._PirateColonyControl;
            }
        }

        public void SelectPirateColonyControl(DistantWorlds.Types.PirateColonyControl pirateColonyControl)
        {
            int num1 = -1;
            if (pirateColonyControl == null)
                return;
            for (int index = 0; index < this._PirateColonyControl.Count; ++index)
            {
                if (this._PirateColonyControl[index] == pirateColonyControl)
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
                object tag = this._Grid.Rows[index].Cells[1].Tag;
                if (tag != null && tag is int num2 && num2 == num1)
                {
                    this._Grid.Rows[index].Selected = true;
                    this._Grid.FirstDisplayedScrollingRowIndex = index;
                    break;
                }
            }
        }

        public DistantWorlds.Types.PirateColonyControl SelectedPirateColonyControl
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (DistantWorlds.Types.PirateColonyControl)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index)
                { return this._PirateColonyControl[index]; }
                else { return (DistantWorlds.Types.PirateColonyControl)null; }
            }
        }

        public void ClearData()
        {
            this._PirateColonyControl = (PirateColonyControlList)null;
            this._Habitat = (Habitat)null;
        }

        public bool CheckPirateFactionPresent(int empireId)
        {
            for (int index = 0; index < this._PirateColonyControl.Count; ++index)
            {
                if ((int)this._PirateColonyControl[index].EmpireId == empireId)
                    return true;
            }
            return false;
        }

        public void BindData(
          PirateColonyControlList pirateColonyControl,
          DistantWorlds.Types.EmpireList pirateFactions,
          Habitat habitat)
        {
            pirateColonyControl.Sort();
            pirateColonyControl.Reverse();
            this._PirateColonyControl = pirateColonyControl;
            this._Habitat = habitat;
            this._Grid.ReadOnly = false;
            this._Grid.EditMode = DataGridViewEditMode.EditOnEnter;
            this._Grid.SuspendLayout();
            this._Grid.Rows.Clear();
            if (pirateColonyControl != null && pirateColonyControl.Count > 0)
                this._Grid.Rows.Add(pirateColonyControl.Count);
            if (pirateColonyControl != null)
            {
                for (int index = 0; index < pirateColonyControl.Count; ++index)
                {
                    DistantWorlds.Types.PirateColonyControl pirateColonyControl1 = pirateColonyControl[index];
                    if (pirateColonyControl1 != null)
                    {
                        DataGridViewRow row = this._Grid.Rows[index];
                        Empire byEmpireId = pirateFactions.GetByEmpireId((int)pirateColonyControl1.EmpireId);
                        if (byEmpireId != null)
                        {
                            row.Cells[0].Value = (object)byEmpireId.SmallFlagPicture;
                            row.Cells[1].Value = (object)byEmpireId.Name;
                            row.Cells[1].Tag = (object)index;
                            row.Cells[2].Value = (object)(float)((double)pirateColonyControl1.ControlLevel * 100.0);
                            row.Cells[2].Style.Format = "##0";
                            row.Cells[2].ReadOnly = false;
                            row.Cells[3].Value = (object)pirateColonyControl1.HasFacilityControl;
                        }
                    }
                }
            }
            this.RememberSorting();
            this._Grid.ResumeLayout();
        }

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
                this.CellTemplate = (DataGridViewCell)new PirateColonyControlListView.SortableImageCell();
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
                this.CellTemplate = (DataGridViewCell)new PirateColonyControlListView.SortableImageNumberCell();
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
