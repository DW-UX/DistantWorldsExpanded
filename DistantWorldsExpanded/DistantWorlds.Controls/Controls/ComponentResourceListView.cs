// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ComponentResourceListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class ComponentResourceListView : ListViewBase
    {
        private ComponentResourceList _ComponentResources;
        private IContainer components;

        public ComponentResourceListView()
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
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Type");
            viewTextBoxColumn1.Name = "Type";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 170;
            viewTextBoxColumn1.FillWeight = 170f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Quantity Abbreviation");
            viewTextBoxColumn2.Name = "Quantity";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(int);
            viewTextBoxColumn2.Width = 50;
            viewTextBoxColumn2.FillWeight = 50f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
        }

        public ComponentResource SelectedResource
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (ComponentResource)null;
                //index = -1;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index)
                { return this._ComponentResources[index]; }
                else { return (ComponentResource)null; }
            }
        }

        public void ClearData() => this._ComponentResources = (ComponentResourceList)null;

        public void BindData(ComponentResourceList componentResources, Bitmap[] resourceImages)
        {
            this._ComponentResources = componentResources;
            this._Grid.Rows.Clear();
            if (componentResources != null)
            {
                for (int index = 0; index < componentResources.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.Cells[0].Value = (object)resourceImages[componentResources[index].PictureRef];
                    row.Cells[1].Value = (object)componentResources[index].Name;
                    row.Cells[2].Tag = (object)index;
                    row.Cells[2].Value = (object)(int)componentResources[index].Quantity;
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
    }
}
