// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.StartingEmpiresListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class StartingEmpiresListView : ListViewBase
  {
    private IContainer components;
    private RaceList _Races;
    private Bitmap _RemoveImage;
    private bool _Initialised;

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
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Bitmap RemoveImage
    {
      get => this._RemoveImage;
      set => this._RemoveImage = value;
    }

    public void _Grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      if (!this._Initialised || e.RowIndex < 0)
        return;
      this._Grid.Rows[e.RowIndex].Cells["Name"].Value = (object) string.Empty;
      this._Grid.Rows[e.RowIndex].Cells["Race"].Value = (object) ((string[]) ((DataGridViewComboBoxColumn) this._Grid.Columns["Race"]).DataSource)[0];
      this._Grid.Rows[e.RowIndex].Cells["Government"].Value = (object) ("(" + TextResolver.GetText("Random") + ")");
      this._Grid.Rows[e.RowIndex].Cells["Size"].Value = (object) ("(" + TextResolver.GetText("Random") + ")");
      this._Grid.Rows[e.RowIndex].Cells["TechLevel"].Value = (object) ("(" + TextResolver.GetText("Random") + ")");
      this._Grid.Rows[e.RowIndex].Cells["HomeSystem"].Value = (object) TextResolver.GetText("Normal");
      this._Grid.Rows[e.RowIndex].Cells["Proximity"].Value = (object) ("(" + TextResolver.GetText("Random") + ")");
      this._Grid.Rows[e.RowIndex].Cells["Remove"].Value = (object) this._RemoveImage;
    }

    public void _Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex != 7)
        return;
      this._Grid.Rows.RemoveAt(e.RowIndex);
    }

    public StartingEmpiresListView()
    {
      this.InitializeComponent();
      this._Initialised = false;
      this._Grid.RowTemplate.Height = 25;
      this._Grid.ReadOnly = false;
      this._Grid.EditMode = DataGridViewEditMode.EditOnEnter;
      this._Grid.RowsAdded += new DataGridViewRowsAddedEventHandler(this._Grid_RowsAdded);
      this._Grid.CellContentClick += new DataGridViewCellEventHandler(this._Grid_CellContentClick);
      this._Grid.CellValueChanged += new DataGridViewCellEventHandler(this._Grid_CellValueChanged);
      DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
      viewTextBoxColumn.HeaderText = TextResolver.GetText("Name");
      viewTextBoxColumn.Name = "Name";
      viewTextBoxColumn.ReadOnly = false;
      viewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
      viewTextBoxColumn.ValueType = typeof (string);
      viewTextBoxColumn.Width = 180;
      this._Grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
      DataGridViewComboBoxColumn viewComboBoxColumn1 = new DataGridViewComboBoxColumn();
      List<string> stringList = new List<string>();
      stringList.Add("(" + TextResolver.GetText("Random") + ")");
      this._Races = Galaxy.LoadRaces(Application.StartupPath, string.Empty);
      this._Races = this._Races.ResolvePlayableRaces();
      foreach (Race race in (SyncList<Race>) this._Races)
        stringList.Add(race.Name);
      stringList.Sort();
      viewComboBoxColumn1.DataSource = (object) stringList.ToArray();
      viewComboBoxColumn1.FlatStyle = FlatStyle.Popup;
      viewComboBoxColumn1.HeaderText = TextResolver.GetText("Race");
      viewComboBoxColumn1.Name = "Race";
      viewComboBoxColumn1.Width = 120;
      viewComboBoxColumn1.ValueType = typeof (string);
      this._Grid.Columns.Add((DataGridViewColumn) viewComboBoxColumn1);
      DataGridViewComboBoxColumn viewComboBoxColumn2 = new DataGridViewComboBoxColumn();
      viewComboBoxColumn2.DataSource = (object) new List<string>()
      {
        "(" + TextResolver.GetText("Random") + ")",
        TextResolver.GetText("Despotism"),
        TextResolver.GetText("Military Dictatorship"),
        TextResolver.GetText("Feudalism"),
        TextResolver.GetText("Monarchy"),
        TextResolver.GetText("Republic"),
        TextResolver.GetText("Democracy")
      };
      viewComboBoxColumn2.FlatStyle = FlatStyle.Popup;
      viewComboBoxColumn2.HeaderText = TextResolver.GetText("Government");
      viewComboBoxColumn2.Name = "Government";
      viewComboBoxColumn2.Width = 140;
      viewComboBoxColumn2.ValueType = typeof (string);
      this._Grid.Columns.Add((DataGridViewColumn) viewComboBoxColumn2);
      DataGridViewComboBoxColumn viewComboBoxColumn3 = new DataGridViewComboBoxColumn();
      viewComboBoxColumn3.Items.Add((object) ("(" + TextResolver.GetText("Random") + ")"));
      viewComboBoxColumn3.Items.Add((object) TextResolver.GetText("PreWarp"));
      viewComboBoxColumn3.Items.Add((object) TextResolver.GetText("Starting"));
      viewComboBoxColumn3.Items.Add((object) TextResolver.GetText("Young"));
      viewComboBoxColumn3.Items.Add((object) TextResolver.GetText("Expanding"));
      viewComboBoxColumn3.Items.Add((object) TextResolver.GetText("Mature"));
      viewComboBoxColumn3.Items.Add((object) TextResolver.GetText("Old"));
      viewComboBoxColumn3.FlatStyle = FlatStyle.Popup;
      viewComboBoxColumn3.HeaderText = TextResolver.GetText("Size");
      viewComboBoxColumn3.Name = "Size";
      viewComboBoxColumn3.Width = 80;
      viewComboBoxColumn3.ValueType = typeof (string);
      this._Grid.Columns.Add((DataGridViewColumn) viewComboBoxColumn3);
      DataGridViewComboBoxColumn viewComboBoxColumn4 = new DataGridViewComboBoxColumn();
      viewComboBoxColumn4.Items.Add((object) ("(" + TextResolver.GetText("Random") + ")"));
      viewComboBoxColumn4.Items.Add((object) TextResolver.GetText("PreWarp"));
      viewComboBoxColumn4.Items.Add((object) TextResolver.GetText("Starting"));
      viewComboBoxColumn4.Items.Add((object) string.Format(TextResolver.GetText("Level X"), (object) "1"));
      viewComboBoxColumn4.Items.Add((object) string.Format(TextResolver.GetText("Level X"), (object) "2"));
      viewComboBoxColumn4.Items.Add((object) string.Format(TextResolver.GetText("Level X"), (object) "3"));
      viewComboBoxColumn4.Items.Add((object) string.Format(TextResolver.GetText("Level X"), (object) "4"));
      viewComboBoxColumn4.Items.Add((object) string.Format(TextResolver.GetText("Level X"), (object) "5"));
      viewComboBoxColumn4.Items.Add((object) string.Format(TextResolver.GetText("Level X"), (object) "6"));
      viewComboBoxColumn4.Items.Add((object) string.Format(TextResolver.GetText("Level X"), (object) "7"));
      viewComboBoxColumn4.FlatStyle = FlatStyle.Popup;
      viewComboBoxColumn4.HeaderText = TextResolver.GetText("Tech Level");
      viewComboBoxColumn4.Name = "TechLevel";
      viewComboBoxColumn4.Width = 80;
      viewComboBoxColumn4.ValueType = typeof (string);
      this._Grid.Columns.Add((DataGridViewColumn) viewComboBoxColumn4);
      DataGridViewComboBoxColumn viewComboBoxColumn5 = new DataGridViewComboBoxColumn();
      viewComboBoxColumn5.Items.Add((object) TextResolver.GetText("Harsh"));
      viewComboBoxColumn5.Items.Add((object) TextResolver.GetText("Trying"));
      viewComboBoxColumn5.Items.Add((object) TextResolver.GetText("Normal"));
      viewComboBoxColumn5.Items.Add((object) TextResolver.GetText("Agreeable"));
      viewComboBoxColumn5.Items.Add((object) TextResolver.GetText("Excellent"));
      viewComboBoxColumn5.FlatStyle = FlatStyle.Popup;
      viewComboBoxColumn5.HeaderText = TextResolver.GetText("Home System");
      viewComboBoxColumn5.Name = "HomeSystem";
      viewComboBoxColumn5.Width = 100;
      viewComboBoxColumn5.ValueType = typeof (string);
      this._Grid.Columns.Add((DataGridViewColumn) viewComboBoxColumn5);
      DataGridViewComboBoxColumn viewComboBoxColumn6 = new DataGridViewComboBoxColumn();
      viewComboBoxColumn6.Items.Add((object) ("(" + TextResolver.GetText("Random") + ")"));
      viewComboBoxColumn6.Items.Add((object) TextResolver.GetText("Same System"));
      viewComboBoxColumn6.Items.Add((object) TextResolver.GetText("Nearby"));
      viewComboBoxColumn6.Items.Add((object) TextResolver.GetText("Average"));
      viewComboBoxColumn6.Items.Add((object) TextResolver.GetText("Distant"));
      for (int x = 0; x < 10; ++x)
      {
        for (int y = 0; y < 10; ++y)
        {
          string str = Galaxy.ResolveSectorDescription(new Sector(x, y));
          viewComboBoxColumn6.Items.Add((object) (TextResolver.GetText("Sector") + " " + str));
        }
      }
      viewComboBoxColumn6.FlatStyle = FlatStyle.Popup;
      viewComboBoxColumn6.HeaderText = TextResolver.GetText("Proximity to You");
      viewComboBoxColumn6.Name = "Proximity";
      viewComboBoxColumn6.Width = 110;
      viewComboBoxColumn6.ValueType = typeof (string);
      this._Grid.Columns.Add((DataGridViewColumn) viewComboBoxColumn6);
      DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
      gridViewImageColumn.HeaderText = "";
      gridViewImageColumn.Name = "Remove";
      gridViewImageColumn.ReadOnly = true;
      gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
      gridViewImageColumn.ValueType = typeof (Image);
      gridViewImageColumn.Width = 30;
      this._Grid.Columns.Add((DataGridViewColumn) gridViewImageColumn);
      this._Initialised = true;
    }

    private void _Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex != 1)
        return;
      string str = (string) this._Grid.Rows[e.RowIndex].Cells[2].Value;
      Race selectedRace = this.DetermineSelectedRace(e.RowIndex);
      List<int> intList = Empire.ResolveDefaultAllowableGovernmentTypes(selectedRace, true);
      List<string> stringList = new List<string>();
      stringList.Add("(" + TextResolver.GetText("Random") + ")");
      int num = -1;
      for (int index = 0; index < intList.Count; ++index)
      {
        if (intList[index] >= 0 && intList[index] < Galaxy.GovernmentsStatic.Count)
        {
          GovernmentAttributes governmentAttributes = Galaxy.GovernmentsStatic[intList[index]];
          if (governmentAttributes != null)
          {
            stringList.Add(governmentAttributes.Name);
            if (selectedRace != null && selectedRace.PreferredStartingGovernmentId != -1 && intList[index] == selectedRace.PreferredStartingGovernmentId)
              num = index;
          }
        }
      }
      if (!stringList.Contains(str))
        this._Grid.Rows[e.RowIndex].Cells[2].Value = (object) stringList[0];
      if (selectedRace != null && selectedRace.PreferredStartingGovernmentId != -1 && num >= 0)
      {
        int index = num + 1;
        if (stringList.Count > index)
          this._Grid.Rows[e.RowIndex].Cells[2].Value = (object) stringList[index];
      }
      ((DataGridViewComboBoxCell) this._Grid.Rows[e.RowIndex].Cells[2]).DataSource = (object) stringList.ToArray();
    }

    public DataGridViewRow GetRowByEmpireName(string empireName)
    {
      DataGridViewRow rowByEmpireName = (DataGridViewRow) null;
      for (int index = 0; index < this._Grid.Rows.Count; ++index)
      {
        if (this._Grid.Rows[index].Cells["Name"].Value == (object) empireName)
          return this._Grid.Rows[index];
      }
      return rowByEmpireName;
    }

    private Race DetermineSelectedRace(int rowIndex)
    {
      if (this._Grid.Rows.Count > rowIndex && this._Races != null)
      {
        string str = (string) this._Grid.Rows[rowIndex].Cells["Race"].Value;
        if (str == "(" + TextResolver.GetText("Random") + ")")
          return (Race) null;
        for (int index = 0; index < this._Races.Count; ++index)
        {
          if (this._Races[index].Name.ToLower(CultureInfo.InvariantCulture) == str.ToLower(CultureInfo.InvariantCulture))
            return this._Races[index];
        }
      }
      return (Race) null;
    }

    public void SetProximityValues(int sectorWidth, int sectorHeight)
    {
      DataGridViewColumn column = this._Grid.Columns["Proximity"];
      if (column == null || !(column is DataGridViewComboBoxColumn))
        return;
      DataGridViewComboBoxColumn viewComboBoxColumn = (DataGridViewComboBoxColumn) column;
      viewComboBoxColumn.Items.Clear();
      viewComboBoxColumn.Items.Add((object) ("(" + TextResolver.GetText("Random") + ")"));
      viewComboBoxColumn.Items.Add((object) TextResolver.GetText("Same System"));
      viewComboBoxColumn.Items.Add((object) TextResolver.GetText("Nearby"));
      viewComboBoxColumn.Items.Add((object) TextResolver.GetText("Average"));
      viewComboBoxColumn.Items.Add((object) TextResolver.GetText("Distant"));
      for (int x = 0; x < sectorWidth; ++x)
      {
        for (int y = 0; y < sectorHeight; ++y)
        {
          string str = Galaxy.ResolveSectorDescription(new Sector(x, y));
          viewComboBoxColumn.Items.Add((object) (TextResolver.GetText("Sector") + " " + str));
        }
      }
    }

    public void SetRaces(string customizationSetName)
    {
      List<string> stringList = new List<string>();
      stringList.Add("(" + TextResolver.GetText("Random") + ")");
      RaceList raceList = Galaxy.LoadRaces(Application.StartupPath, customizationSetName);
      foreach (Race race in (SyncList<Race>) raceList)
        stringList.Add(race.Name);
      stringList.Sort();
      ((DataGridViewComboBoxColumn) this._Grid.Columns["Race"]).DataSource = (object) stringList.ToArray();
      this._Races = raceList;
    }

    private class SortableImageColumn : DataGridViewImageColumn
    {
      public SortableImageColumn()
      {
        this.CellTemplate = (DataGridViewCell) new StartingEmpiresListView.SortableImageCell();
        this.ValueType = typeof (string);
      }
    }

    private class SortableImageCell : DataGridViewImageCell
    {
      private Bitmap _Bitmap;

      public SortableImageCell() => this.ValueType = typeof (string);

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
        return (object) this.ScaledImage;
      }

      public override object DefaultNewRowValue => (object) string.Empty;
    }
  }
}
