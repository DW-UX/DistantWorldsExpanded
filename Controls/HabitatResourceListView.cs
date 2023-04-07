// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HabitatResourceListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class HabitatResourceListView : ListViewBase
    {
        private IContainer components;
        private HabitatResourceList _HabitatResources;
        private bool _Editable;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();

        public HabitatResourceListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.Description = "Picture";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "Picture";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn.ValueType = typeof(Image);
            gridViewImageColumn.Width = 30;
            gridViewImageColumn.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
            viewTextBoxColumn.HeaderText = TextResolver.GetText("Type");
            viewTextBoxColumn.Name = "Type";
            viewTextBoxColumn.ReadOnly = false;
            viewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn.ValueType = typeof(string);
            viewTextBoxColumn.Width = 160;
            viewTextBoxColumn.FillWeight = 160f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn);
            DataGridViewNumericUpDownColumn numericUpDownColumn = new DataGridViewNumericUpDownColumn();
            numericUpDownColumn.HeaderText = "%";
            numericUpDownColumn.Name = "Abundance";
            numericUpDownColumn.ReadOnly = false;
            numericUpDownColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            numericUpDownColumn.Minimum = 1M;
            numericUpDownColumn.Maximum = 100M;
            numericUpDownColumn.Increment = 1M;
            numericUpDownColumn.ValueType = typeof(double);
            numericUpDownColumn.Width = 45;
            numericUpDownColumn.FillWeight = 45f;
            this._Grid.Columns.Add((DataGridViewColumn)numericUpDownColumn);
        }

        public HabitatResource SelectedResource
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (HabitatResource)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index && index >= 0 && this._HabitatResources.Count > index)
                { return this._HabitatResources[index]; }
                else
                { return (HabitatResource)null; }
            }
        }

        public HabitatResourceList Resources
        {
            get
            {
                if (!this._Editable)
                    return this._HabitatResources;
                for (int index1 = 0; index1 < this._Grid.Rows.Count; ++index1)
                {
                    //index2 = -1;
                    object tag = this._Grid.Rows[index1].Cells[1].Tag;
                    if (tag is int index2)
                    {
                        if (index2 >= 0 && this._HabitatResources.Count > index2)
                        {
                            double num = (double)this._Grid.Rows[index1].Cells["Abundance"].Value * 10.0;
                            this._HabitatResources[index2].Abundance = (short)num;
                        }
                    }
                }
                return this._HabitatResources;
            }
        }

        public void ClearData() => this._HabitatResources = (HabitatResourceList)null;

        public void BindData(HabitatResourceList habitatResources, Bitmap[] resourceImages) => this.BindData(habitatResources, resourceImages, false);

        public void BindData(
          HabitatResourceList habitatResources,
          Bitmap[] resourceImages,
          bool editable)
        {
            this._HabitatResources = habitatResources;
            this._Editable = editable;
            this._Grid.ReadOnly = !editable;
            if (editable)
                this._Grid.EditMode = DataGridViewEditMode.EditOnEnter;
            this._Grid.Columns["Type"].ReadOnly = true;
            this._Grid.Columns["Abundance"].ReadOnly = !editable;
            this._Grid.Rows.Clear();
            if (habitatResources != null)
            {
                for (int index = 0; index < habitatResources.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.Cells[0].Value = (object)resourceImages[habitatResources[index].PictureRef];
                    row.Cells[1].Value = (object)habitatResources[index].Name;
                    row.Cells[1].Tag = (object)index;
                    row.Cells[2].Value = (object)((double)habitatResources[index].Abundance / 10.0);
                    row.Cells[2].Style.Format = "##0";
                    row.Cells[2].ReadOnly = !editable;
                }
            }
            this.RememberSorting();
        }
    }
}
