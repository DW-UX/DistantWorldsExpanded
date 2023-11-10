// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GalaxySummaryListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class GalaxySummaryListView : ListViewBase
    {
        private IContainer components;
        private GalaxySummaryList _GalaxySummaries;

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

        public GalaxySummaryListView()
        {
            this.InitializeComponent();
            DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
            viewTextBoxColumn.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn.Name = "Name";
            viewTextBoxColumn.ReadOnly = false;
            viewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn.ValueType = typeof(string);
            viewTextBoxColumn.Width = 170;
            viewTextBoxColumn.FillWeight = 170f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn);
        }

        public GalaxySummary SelectedGalaxySummary
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (GalaxySummary)null;
                //index = -1;
                object tag = selectedRows[0].Cells[0].Tag;
                if (tag is int index)
                { return this._GalaxySummaries[index]; }
                else { return (GalaxySummary)null; }
            }
        }

        public void ClearData()
        {
            this._GalaxySummaries = (GalaxySummaryList)null;
            this.BindData((GalaxySummaryList)null);
        }

        public void BindData(GalaxySummaryList galaxySummaries)
        {
            this._GalaxySummaries = galaxySummaries;
            this._Grid.Rows.Clear();
            if (galaxySummaries != null)
            {
                for (int index = 0; index < galaxySummaries.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.Cells[0].Value = string.IsNullOrEmpty(galaxySummaries[index].Title) ? (object)galaxySummaries[index].Filename : (object)galaxySummaries[index].Title;
                    row.Cells[0].Tag = (object)index;
                }
            }
            this.RememberSorting();
        }
    }
}
