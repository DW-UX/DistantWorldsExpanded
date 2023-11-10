// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ComponentListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class ComponentListView : ListViewBase
    {
        private ComponentList _Components;
        private bool _SummarizedMode;
        private IContainer components;

        public bool SummarizedMode
        {
            get => this._SummarizedMode;
            set
            {
                this._SummarizedMode = value;
                this._Grid.Columns["Amount"].Visible = this._SummarizedMode;
            }
        }

        public ComponentList Components => this._Components;

        public ComponentListView()
        {
            this.InitializeComponent();
            this._Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this._Grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this._Grid.EnableHeadersVisualStyles = false;
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
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Amount");
            viewTextBoxColumn1.Name = "Amount";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(int);
            viewTextBoxColumn1.Width = 30;
            viewTextBoxColumn1.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewLinkColumn gridViewLinkColumn = new DataGridViewLinkColumn();
            gridViewLinkColumn.HeaderText = TextResolver.GetText("Name");
            gridViewLinkColumn.Name = "Name";
            gridViewLinkColumn.ReadOnly = true;
            gridViewLinkColumn.LinkBehavior = LinkBehavior.AlwaysUnderline;
            gridViewLinkColumn.ActiveLinkColor = this.Grid.DefaultCellStyle.ForeColor;
            gridViewLinkColumn.LinkColor = this.Grid.DefaultCellStyle.ForeColor;
            gridViewLinkColumn.TrackVisitedState = false;
            gridViewLinkColumn.VisitedLinkColor = this.Grid.DefaultCellStyle.ForeColor;
            gridViewLinkColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewLinkColumn.ValueType = typeof(string);
            gridViewLinkColumn.Width = 130;
            gridViewLinkColumn.FillWeight = 130f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewLinkColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Category");
            viewTextBoxColumn2.Name = "Category";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 90;
            viewTextBoxColumn2.FillWeight = 90f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Size");
            viewTextBoxColumn3.Name = "Size";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 40;
            viewTextBoxColumn3.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("Tech");
            viewTextBoxColumn4.Name = "TechPoints";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(int);
            viewTextBoxColumn4.Width = 40;
            viewTextBoxColumn4.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
        }

        public int SelectedAmount
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                return selectedRows.Count == 1 ? (int)selectedRows[0].Cells[1].Value : 0;
            }
        }

        public DistantWorlds.Types.Component SelectedComponent
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows == null || selectedRows.Count != 1)
                    return (DistantWorlds.Types.Component)null;
                //index = -1;
                if (selectedRows[0].Cells == null)
                    return (DistantWorlds.Types.Component)null;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index && index >= 0 && this._Components != null && this._Components.Count > index)
                { return this._Components[index]; }
                else { return (DistantWorlds.Types.Component)null; }
            }
        }

        public void SelectRow(int rowIndex) => this.SelectRow(rowIndex, true);

        public void SelectRow(int rowIndex, bool scrollToRow)
        {
            if (rowIndex >= this._Grid.Rows.Count)
                return;
            this._Grid.Rows[rowIndex].Selected = true;
            if (!scrollToRow)
                return;
            this._Grid.FirstDisplayedScrollingRowIndex = rowIndex;
        }

        public void SelectComponent(DistantWorlds.Types.Component componentToSelect)
        {
            if (this._SummarizedMode)
            {
                int num1 = -1;
                if (componentToSelect != null)
                    num1 = componentToSelect.ComponentID;
                if (num1 < 0)
                    return;
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
            else
            {
                int num3 = -1;
                if (componentToSelect == null)
                    return;
                for (int index = 0; index < this._Components.Count; ++index)
                {
                    if (this._Components[index] == componentToSelect)
                    {
                        num3 = index;
                        break;
                    }
                }
                if (num3 < 0)
                    return;
                for (int index = 0; index < this._Grid.Rows.Count; ++index)
                {
                    object tag = this._Grid.Rows[index].Cells[2].Tag;
                    if (tag != null && tag is int num4 && num4 == num3)
                    {
                        this._Grid.Rows[index].Selected = true;
                        this._Grid.FirstDisplayedScrollingRowIndex = index;
                        break;
                    }
                }
            }
        }

        public void ClearData() => this._Components = (ComponentList)null;

        public void BindData(ComponentList components, Bitmap[] componentImages) => this.BindData(components, componentImages, (Galaxy)null);

        public void BindData(ComponentList components, Bitmap[] componentImages, Galaxy galaxy)
        {
            this.SuspendLayout();
            this._Grid.SuspendLayout();
            this._Components = components;
            ResourceList resourceList = new ResourceList();
            if (galaxy != null && galaxy.PlayerEmpire != null)
                resourceList = galaxy.PlayerEmpire.DetermineResourcesEmpireSupplies();
            this._Grid.Columns["Amount"].Visible = this._SummarizedMode;
            this._Grid.Rows.Clear();
            if (components != null)
            {
                if (this._SummarizedMode)
                {
                    ComponentList componentList = new ComponentList();
                    List<int> intList1 = new List<int>();
                    List<int> intList2 = new List<int>();
                    for (int index1 = 0; index1 < components.Count; ++index1)
                    {
                        int index2 = componentList.IndexById(components[index1]);
                        if (index2 < 0)
                        {
                            componentList.Add(components[index1]);
                            intList1.Add(1);
                            intList2.Add(index1);
                        }
                        else
                        {
                            List<int> intList3;
                            int index3;
                            (intList3 = intList1)[index3 = index2] = intList3[index3] + 1;
                            intList2[index2] = index1;
                        }
                    }
                    for (int index = 0; index < componentList.Count; ++index)
                    {
                        this._Grid.Rows.Add();
                        DataGridViewRow row = this._Grid.Rows[index];
                        row.Cells[0].Value = (object)componentImages[componentList[index].PictureRef];
                        row.Cells[1].Value = (object)intList1[index];
                        row.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        row.Cells[1].Tag = (object)componentList[index].ComponentID;
                        row.Cells[2].Value = (object)componentList[index].Name;
                        row.Cells[2].Tag = (object)intList2[index];
                        row.Cells[3].Value = (object)Galaxy.ResolveComponentCategoryAbbreviation(componentList[index].Category);
                        row.Cells[3].ToolTipText = Galaxy.ResolveDescription(componentList[index].Category);
                        row.Cells[4].Value = (object)(componentList[index].Size * intList1[index]);
                        int num = 1;
                        if (num < 1)
                            num = 1;
                        row.Cells[5].Value = (object)num;
                        row.Cells[5].Style.Format = "####K";
                    }
                }
                else
                {
                    for (int index4 = 0; index4 < components.Count; ++index4)
                    {
                        DistantWorlds.Types.Component component = components[index4];
                        this._Grid.Rows.Add();
                        DataGridViewRow row = this._Grid.Rows[index4];
                        row.Cells[0].Value = (object)componentImages[component.PictureRef];
                        row.Cells[1].Value = (object)0;
                        row.Cells[2].Value = (object)components[index4].Name;
                        row.Cells[2].Tag = (object)index4;
                        bool flag = true;
                        string str = string.Empty;
                        if (resourceList != null)
                        {
                            for (int index5 = 0; index5 < component.RequiredResources.Count; ++index5)
                            {
                                ComponentResource requiredResource = component.RequiredResources[index5];
                                if (requiredResource != null && !resourceList.Contains((Resource)requiredResource))
                                {
                                    flag = false;
                                    str = (str.Length <= 0 ? TextResolver.GetText("Missing Component Resource Supply") + " " : str + ", ") + requiredResource.Name;
                                }
                            }
                        }
                        if (!flag)
                        {
                            row.Cells[2].Style.ForeColor = Color.FromArgb((int)byte.MaxValue, 128, 0);
                            ((DataGridViewLinkCell)row.Cells[2]).ActiveLinkColor = Color.FromArgb((int)byte.MaxValue, 128, 0);
                            ((DataGridViewLinkCell)row.Cells[2]).LinkColor = Color.FromArgb((int)byte.MaxValue, 128, 0);
                            ((DataGridViewLinkCell)row.Cells[2]).VisitedLinkColor = Color.FromArgb((int)byte.MaxValue, 128, 0);
                            row.Cells[2].ToolTipText = str;
                        }
                        row.Cells[3].Value = (object)Galaxy.ResolveComponentCategoryAbbreviation(component.Category);
                        row.Cells[3].ToolTipText = Galaxy.ResolveDescription(component.Category);
                        row.Cells[4].Value = (object)component.Size;
                        int num = 1;
                        if (num < 1)
                            num = 1;
                        row.Cells[5].Value = (object)num;
                        row.Cells[5].Style.Format = "####K";
                    }
                }
            }
            this.RememberSorting();
            this._Grid.ResumeLayout();
            this.ResumeLayout();
        }

        public event ComponentListView.CellContentClickDelegate CellContentClick;

        private void _Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 1)
                return;
            DistantWorlds.Types.Component component = this._Components[(int)this._Grid.Rows[e.RowIndex].Cells[2].Tag];
            int resourceId = -1;
            if (component != null)
                resourceId = component.ComponentID;
            if (this.CellContentClick == null)
                return;
            this.CellContentClick(sender, resourceId);
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
            this.Name = nameof(ComponentListView);
            this._Grid.CellContentClick += new DataGridViewCellEventHandler(this._Grid_CellContentClick);
            this.ResumeLayout(false);
        }

        public delegate void CellContentClickDelegate(object sender, int resourceId);
    }
}
