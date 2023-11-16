// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireSummaryListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireSummaryListView : ListViewBase
    {
        private IContainer components;
        private EmpireSummaryList _EmpireSummaries;
        private RaceImageCache _RaceImageCache;

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

        public EmpireSummaryListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.Description = "Race";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "Race";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn.ValueType = typeof(Image);
            gridViewImageColumn.Width = 30;
            gridViewImageColumn.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
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

        public EmpireSummary SelectedEmpireSummary
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (EmpireSummary)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index)
                { return this._EmpireSummaries[index]; }
                else { return (EmpireSummary)null; }
            }
        }

        public void ClearData()
        {
            this._EmpireSummaries = (EmpireSummaryList)null;
            this._RaceImageCache = (RaceImageCache)null;
            this.BindData((EmpireSummaryList)null, (RaceList)null, (RaceImageCache)null);
        }

        public void BindData(
          EmpireSummaryList empireSummaries,
          RaceList races,
          RaceImageCache raceImageCache)
        {
            this._RaceImageCache = raceImageCache;
            this._EmpireSummaries = empireSummaries;
            this._Grid.Rows.Clear();
            if (empireSummaries != null)
            {
                for (int index = 0; index < empireSummaries.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    Race race = races[empireSummaries[index].RaceName];
                    if (race != null)
                        row.Cells[0].Value = (object)raceImageCache.GetRaceImage(race.PictureRef);
                    row.Cells[0].ToolTipText = empireSummaries[index].RaceName;
                    row.Cells[1].Value = (object)empireSummaries[index].Name;
                    row.Cells[1].Style.ForeColor = empireSummaries[index].MainColor;
                    row.Cells[1].Tag = (object)index;
                }
            }
            this.RememberSorting();
        }
    }
}
