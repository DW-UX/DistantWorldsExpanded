// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResourceListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ResourceListView : ListViewBase
  {
    private IContainer components;
    private ResourceList _Resources;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.Font;
      this._Grid.CellContentClick += new DataGridViewCellEventHandler(this._Grid_CellContentClick);
    }

    public ResourceListView()
    {
      this.InitializeComponent();
      DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
      gridViewImageColumn.Description = "Picture";
      gridViewImageColumn.HeaderText = "";
      gridViewImageColumn.Name = "Picture";
      gridViewImageColumn.ReadOnly = true;
      gridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
      gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
      gridViewImageColumn.ValueType = typeof (Image);
      gridViewImageColumn.Width = 30;
      gridViewImageColumn.FillWeight = 30f;
      this._Grid.Columns.Add((DataGridViewColumn) gridViewImageColumn);
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
      gridViewLinkColumn.ValueType = typeof (string);
      gridViewLinkColumn.Width = 160;
      gridViewLinkColumn.FillWeight = 160f;
      this._Grid.Columns.Add((DataGridViewColumn) gridViewLinkColumn);
      DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn1.HeaderText = TextResolver.GetText("Type");
      viewTextBoxColumn1.Name = "Type";
      viewTextBoxColumn1.ReadOnly = true;
      viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn1.ValueType = typeof (string);
      viewTextBoxColumn1.Width = 70;
      viewTextBoxColumn1.FillWeight = 70f;
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
      DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn2.HeaderText = TextResolver.GetText("Price");
      viewTextBoxColumn2.Name = "Price";
      viewTextBoxColumn2.ReadOnly = true;
      viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn2.ValueType = typeof (double);
      viewTextBoxColumn2.Width = 50;
      viewTextBoxColumn2.FillWeight = 50f;
      viewTextBoxColumn2.DefaultCellStyle.Format = "0.0";
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
      DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn3.HeaderText = TextResolver.GetText("Sources");
      viewTextBoxColumn3.Name = "Sources";
      viewTextBoxColumn3.ReadOnly = true;
      viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn3.ValueType = typeof (int);
      viewTextBoxColumn3.Width = 50;
      viewTextBoxColumn3.FillWeight = 50f;
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
      DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn4.HeaderText = TextResolver.GetText("Your Stock");
      viewTextBoxColumn4.Name = "Stock - Your Empire";
      viewTextBoxColumn4.ReadOnly = true;
      viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn4.ValueType = typeof (double);
      viewTextBoxColumn4.Width = 50;
      viewTextBoxColumn4.FillWeight = 50f;
      viewTextBoxColumn4.DefaultCellStyle.Format = "0.0K";
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn4);
      DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn5.HeaderText = TextResolver.GetText("In Transit - Your Empire");
      viewTextBoxColumn5.Name = "In Transit - Your Empire";
      viewTextBoxColumn5.ReadOnly = true;
      viewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn5.ValueType = typeof (double);
      viewTextBoxColumn5.Width = 50;
      viewTextBoxColumn5.FillWeight = 50f;
      viewTextBoxColumn5.DefaultCellStyle.Format = "0.0K";
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn5);
      DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn6.HeaderText = TextResolver.GetText("Unfulfilled Demand - Your Empire");
      viewTextBoxColumn6.Name = "Unfulfilled Demand - Your Empire";
      viewTextBoxColumn6.ReadOnly = true;
      viewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn6.ValueType = typeof (double);
      viewTextBoxColumn6.Width = 50;
      viewTextBoxColumn6.FillWeight = 50f;
      viewTextBoxColumn6.DefaultCellStyle.Format = "0.0K";
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn6);
      DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn7.HeaderText = TextResolver.GetText("Galaxy Stock");
      viewTextBoxColumn7.Name = "Stock - Galaxy";
      viewTextBoxColumn7.ReadOnly = true;
      viewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn7.ValueType = typeof (double);
      viewTextBoxColumn7.Width = 50;
      viewTextBoxColumn7.FillWeight = 50f;
      viewTextBoxColumn7.DefaultCellStyle.Format = "0.0K";
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn7);
      DataGridViewTextBoxColumn viewTextBoxColumn8 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn8.HeaderText = TextResolver.GetText("In Transit - Galaxy");
      viewTextBoxColumn8.Name = "In Transit - Galaxy";
      viewTextBoxColumn8.ReadOnly = true;
      viewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn8.ValueType = typeof (double);
      viewTextBoxColumn8.Width = 50;
      viewTextBoxColumn8.FillWeight = 50f;
      viewTextBoxColumn8.DefaultCellStyle.Format = "0.0K";
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn8);
      DataGridViewTextBoxColumn viewTextBoxColumn9 = new DataGridViewTextBoxColumn();
      viewTextBoxColumn9.HeaderText = TextResolver.GetText("Unfulfilled Demand - Galaxy");
      viewTextBoxColumn9.Name = "Unfulfilled Demand - Galaxy";
      viewTextBoxColumn9.ReadOnly = true;
      viewTextBoxColumn9.SortMode = DataGridViewColumnSortMode.Automatic;
      viewTextBoxColumn9.ValueType = typeof (double);
      viewTextBoxColumn9.Width = 50;
      viewTextBoxColumn9.FillWeight = 50f;
      viewTextBoxColumn9.DefaultCellStyle.Format = "0.0K";
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn9);
    }

    public Resource SelectedResource
    {
      get
      {
        DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
        if (selectedRows.Count != 1)
          return (Resource) null;
        int index = -1;
        object tag = selectedRows[0].Cells[1].Tag;
        if (tag != null && tag is byte num)
          index = (int) num;
        return index >= 0 ? this._Resources[index] : (Resource) null;
      }
    }

    public ResourceList Resources => this._Resources;

    public void ClearData() => this._Resources = (ResourceList) null;

    public void BindData(Galaxy galaxy, ResourceList resources, Bitmap[] resourceImages)
    {
      this._Resources = resources;
      this._Grid.SuspendLayout();
      this._Grid.Rows.Clear();
      if (resources != null)
      {
        for (int index = 0; index < resources.Count; ++index)
        {
          this._Grid.Rows.Add();
          DataGridViewRow row = this._Grid.Rows[index];
          row.Cells[0].Value = (object) resourceImages[resources[index].PictureRef];
          row.Cells[1].Value = (object) resources[index].Name;
          row.Cells[1].Tag = (object) resources[index].ResourceID;
          string text = TextResolver.GetText("Strategic");
          if (resources[index].IsLuxuryResource)
            text = TextResolver.GetText("Luxury");
          row.Cells[2].Value = (object) text;
          row.Cells[3].Value = (object) galaxy.ResourceCurrentPrices[(int) resources[index].ResourceID];
          row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
          row.Cells[4].Value = (object) galaxy.CountResourceSourcesForEmpire(galaxy.PlayerEmpire, resources[index].ResourceID);
          row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
          row.Cells[5].Value = (object) (galaxy.CountResourceSupplyForEmpire(galaxy.PlayerEmpire, resources[index].ResourceID) / 1000.0);
          double inTransitAmount1 = 0.0;
          double resourceDemandForEmpire = galaxy.CalculateResourceDemandForEmpire(galaxy.PlayerEmpire, resources[index].ResourceID, out inTransitAmount1);
          row.Cells[6].Value = (object) (inTransitAmount1 / 1000.0);
          row.Cells[7].Value = (object) (resourceDemandForEmpire / 1000.0);
          row.Cells[8].Value = (object) (galaxy.CountResourceSupplyForGalaxy(resources[index].ResourceID) / 1000.0);
          double inTransitAmount2 = 0.0;
          double resourceDemand = galaxy.CalculateResourceDemand(resources[index].ResourceID, out inTransitAmount2);
          row.Cells[9].Value = (object) (resourceDemand / 1000.0);
          row.Cells[10].Value = (object) (inTransitAmount2 / 1000.0);
        }
      }
      this.RememberSorting();
      this._Grid.ResumeLayout();
    }

    public event ResourceListView.CellContentClickDelegate CellContentClick;

    private void _Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex != 1)
        return;
      int tag = (int) (byte) this._Grid.Rows[e.RowIndex].Cells[1].Tag;
      if (this.CellContentClick == null)
        return;
      this.CellContentClick(sender, tag);
    }

    public delegate void CellContentClickDelegate(object sender, int resourceId);
  }
}
