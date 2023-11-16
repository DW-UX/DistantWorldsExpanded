// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.TroopListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class TroopListView : ListViewBase
    {
        private TroopList _Troops;
        private bool _Editable;
        private DataGridViewCellStyle _GarrisonStyle = new DataGridViewCellStyle();
        private IContainer components;

        public TroopListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.Description = "Empire";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "Empire";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn.ValueType = typeof(Image);
            gridViewImageColumn.Width = 25;
            gridViewImageColumn.FillWeight = 25f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn1.Name = "Name";
            viewTextBoxColumn1.MaxInputLength = 100;
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 120;
            viewTextBoxColumn1.FillWeight = 120f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Experience");
            viewTextBoxColumn2.Name = "Experience";
            viewTextBoxColumn2.MaxInputLength = 100;
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 60;
            viewTextBoxColumn2.FillWeight = 60f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Type");
            viewTextBoxColumn3.Name = "Type";
            viewTextBoxColumn3.MaxInputLength = 100;
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(string);
            viewTextBoxColumn3.Width = 120;
            viewTextBoxColumn3.FillWeight = 120f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewNumericUpDownColumn numericUpDownColumn = new DataGridViewNumericUpDownColumn();
            numericUpDownColumn.HeaderText = TextResolver.GetText("Readiness");
            numericUpDownColumn.Name = "Size";
            numericUpDownColumn.ReadOnly = true;
            numericUpDownColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            numericUpDownColumn.Minimum = 1M;
            numericUpDownColumn.Maximum = 100M;
            numericUpDownColumn.ValueType = typeof(double);
            numericUpDownColumn.Width = 35;
            numericUpDownColumn.FillWeight = 35f;
            numericUpDownColumn.DefaultCellStyle.Format = "##0";
            this._Grid.Columns.Add((DataGridViewColumn)numericUpDownColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("Attack Strength");
            viewTextBoxColumn4.Name = "AttackStrength";
            viewTextBoxColumn4.ReadOnly = true;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(double);
            viewTextBoxColumn4.Width = 60;
            viewTextBoxColumn4.FillWeight = 60f;
            viewTextBoxColumn4.DefaultCellStyle.Format = "####0";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn5.HeaderText = TextResolver.GetText("Defend Strength");
            viewTextBoxColumn5.Name = "DefendStrength";
            viewTextBoxColumn5.ReadOnly = true;
            viewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn5.ValueType = typeof(double);
            viewTextBoxColumn5.Width = 60;
            viewTextBoxColumn5.FillWeight = 60f;
            viewTextBoxColumn5.DefaultCellStyle.Format = "####0";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
            DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn6.HeaderText = TextResolver.GetText("Maintenance");
            viewTextBoxColumn6.Name = "Maintenance";
            viewTextBoxColumn6.ReadOnly = true;
            viewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn6.ValueType = typeof(double);
            viewTextBoxColumn6.Width = 80;
            viewTextBoxColumn6.FillWeight = 80f;
            viewTextBoxColumn6.DefaultCellStyle.Format = "#####0";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
            DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn7.HeaderText = TextResolver.GetText("Location");
            viewTextBoxColumn7.Name = "Location";
            viewTextBoxColumn7.ReadOnly = true;
            viewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn7.ValueType = typeof(string);
            viewTextBoxColumn7.Width = 160;
            viewTextBoxColumn7.FillWeight = 160f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn7);
        }

        public TroopList Troops
        {
            get
            {
                if (!this._Editable)
                    return this._Troops;
                for (int index1 = 0; index1 < this._Grid.Rows.Count; ++index1)
                {
                    //index2 = -1;
                    object tag = this._Grid.Rows[index1].Cells[1].Tag;
                    if (tag is int index2)
                    {
                        if (index2 >= 0)
                        {
                            string str = (string)this._Grid.Rows[index1].Cells["Name"].Value;
                            double num = Math.Max(0.0, Math.Min(100.0, (double)this._Grid.Rows[index1].Cells["Size"].Value));
                            this._Troops[index2].Name = str;
                            this._Troops[index2].Readiness = (float)num;
                        }
                    }

                }
                return this._Troops;
            }
        }

        public TroopList SelectedTroops
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                TroopList selectedTroops = new TroopList();
                foreach (DataGridViewRow row in (BaseCollection)selectedRows)
                {
                    Troop troop = this.ResolveTroop(row);
                    if (troop != null)
                        selectedTroops.Add(troop);
                }
                return selectedTroops;
            }
        }

        public DataGridViewRow ResolveRow(Troop troop)
        {
            int num1 = -1;
            if (troop != null)
            {
                for (int index = 0; index < this._Troops.Count; ++index)
                {
                    if (this._Troops[index] == troop)
                    {
                        num1 = index;
                        break;
                    }
                }
                if (num1 >= 0)
                {
                    for (int index = 0; index < this._Grid.Rows.Count; ++index)
                    {
                        object tag = this._Grid.Rows[index].Cells[1].Tag;
                        if (tag != null && tag is int num2 && num2 == num1)
                            return this._Grid.Rows[index];
                    }
                }
            }
            return (DataGridViewRow)null;
        }

        public Troop ResolveTroop(DataGridViewRow row)
        {
            //index = -1;
            object tag = row.Cells[1].Tag;
            if (tag is int index && index >= 0 && index < this._Troops.Count)
            { return this._Troops[index]; }
            else
            { return (Troop)null; }
        }

        public Troop SelectedTroop
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Troop)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index)
                { return this._Troops[index]; }
                else
                { return (Troop)null; }
            }
        }

        public void SelectTroop(Troop troopToSelect)
        {
            int num1 = -1;
            if (troopToSelect == null)
                return;
            for (int index = 0; index < this._Troops.Count; ++index)
            {
                if (this._Troops[index] == troopToSelect)
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

        public DataGridViewCellStyle GarrisonStyle => this._GarrisonStyle;

        public void ClearData() => this._Troops = (TroopList)null;

        public void BindData(TroopList troops) => this.BindData(troops, false);

        public void BindData(TroopList troops, bool editable)
        {
            this._Troops = troops;
            this._Editable = editable;
            this._Grid.ReadOnly = !editable;
            if (editable)
                this._Grid.EditMode = DataGridViewEditMode.EditOnEnter;
            this._Grid.Columns["Name"].ReadOnly = !editable;
            this._Grid.Columns["Size"].ReadOnly = !editable;
            this._Grid.Rows.Clear();
            if (troops != null && troops.Count > 0)
                this._Grid.Rows.Add(troops.Count);
            this._GarrisonStyle.ForeColor = Color.FromArgb(0, (int)byte.MaxValue, 0);
            if (troops != null)
            {
                for (int index = 0; index < troops.Count; ++index)
                {
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.ReadOnly = !editable;
                    if (troops[index].Empire != null)
                    {
                        row.Cells[0].Value = (object)troops[index].Empire.SmallFlagPicture;
                        row.Cells[0].ToolTipText = troops[index].Empire.Name;
                    }
                    else
                    {
                        row.Cells[0].Value = (object)null;
                        row.Cells[0].ToolTipText = "(" + TextResolver.GetText("None") + ")";
                    }
                    row.Cells[1].Value = !editable ? (object)troops[index].Name : (object)troops[index].Name;
                    if (troops[index].Garrisoned)
                    {
                        row.Cells[1].Style = this._GarrisonStyle;
                        row.Cells[1].ToolTipText = TextResolver.GetText("This troop is garrisoned at this location");
                    }
                    row.Cells[1].ReadOnly = !editable;
                    row.Cells[1].Tag = (object)index;
                    row.Cells[2].Value = (object)Galaxy.ResolveTroopStrengthDescription(troops[index]);
                    row.Cells[3].Value = (object)Galaxy.ResolveDescription(troops[index].Type);
                    row.Cells[4].Value = (object)(double)troops[index].Readiness;
                    row.Cells[4].ReadOnly = !editable;
                    row.Cells[5].Value = (object)troops[index].OverallAttackStrength;
                    row.Cells[6].Value = (object)troops[index].OverallDefendStrength;
                    double num1 = 1.0;
                    double num2 = 1.0;
                    if (troops[index].Empire != null)
                    {
                        num2 = (double)troops[index].Empire.TroopMaintenanceFactor;
                        if (troops[index].Empire.GovernmentAttributes != null)
                            num1 = troops[index].Empire.GovernmentAttributes.MaintenanceCosts;
                    }
                    double num3 = Galaxy.TroopAnnualMaintenance * num1 * (double)troops[index].MaintenanceMultiplier * num2;
                    if (troops[index].BeingRecruited)
                        num3 = 0.0;
                    row.Cells[7].Value = (object)num3;
                    if (troops[index].Colony != null)
                        row.Cells[8].Value = (object)troops[index].Colony.Name;
                    else if (troops[index].BuiltObject != null)
                        row.Cells[8].Value = (object)("(" + TextResolver.GetText("Onboard") + " " + troops[index].BuiltObject.Name + ")");
                    else
                        row.Cells[8].Value = (object)string.Empty;
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = nameof(TroopListView);
            this.Size = new Size(295, 157);
            this.ResumeLayout(false);
        }
    }
}
