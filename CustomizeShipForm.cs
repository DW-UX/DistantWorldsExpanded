// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.CustomizeShipForm
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BaconDistantWorlds
{
  public class CustomizeShipForm : Form
  {
    public BuiltObject ship = (BuiltObject) null;
    public StellarObject planetDoingTheMod = (StellarObject) null;
    public bool wasPaused = false;
    public bool planetDoingTheModIsPlanet = false;
    public bool planetDoingTheModIsPirate = false;
    private IContainer components = (IContainer) null;
    private PictureBox tradePartnerPictureBox;
    private PictureBox shipPictureBox;
    private Label planetNamelabel;
    private Label shipNamelabel;
    private DataGridView equipedComponentsDataGridView;
    private Button unequipButton;
    private Button equipButton;
    private Button dumpButton;
    private DataGridViewTextBoxColumn componentID;
    private DataGridViewTextBoxColumn planetQuantityColumn;
    private DataGridViewTextBoxColumn planetCargoNameColumn;
    private DataGridView unequipedComponentsDataGridView;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private Label totalCashLabel;

    public CustomizeShipForm(Main main, BuiltObject shipToCustomize, StellarObject shipOrPlanet)
    {
      try
      {
        this.ship = shipToCustomize;
        this.planetDoingTheMod = shipOrPlanet;
        this.InitializeComponent();
        this.UpdateInventories();
        this.DisableButtonsWhenTradePartnerNotPresent();
        this.shipNamelabel.Text = this.ship.Name;
        if (this.planetDoingTheMod != null)
        {
          this.planetNamelabel.Text = this.planetDoingTheMod.Name;
          if (this.planetDoingTheMod is Habitat)
            this.planetDoingTheModIsPlanet = true;
          else if (this.planetDoingTheMod is BuiltObject && (this.planetDoingTheMod as BuiltObject).Empire.PirateEmpireBaseHabitat != null)
            this.planetDoingTheModIsPirate = true;
          if (this.planetDoingTheModIsPlanet && !main._Game.Galaxy.IndependentColonies.Contains(this.planetDoingTheMod as Habitat) && !BaconHabitat.IsAsteroidColony(this.planetDoingTheMod as Habitat))
          {
            BaconMain.customizeShipFormOpen = false;
            return;
          }
          if (this.planetDoingTheMod is Habitat && (this.planetDoingTheMod as Habitat).Empire.PirateEmpireBaseHabitat != null)
            this.planetDoingTheModIsPirate = true;
        }
        this.SizeLabelFont(this.shipNamelabel);
        this.SizeLabelFont(this.planetNamelabel);
        this.shipPictureBox.Image = (Image) new Bitmap(BaconBuiltObjectImageCache.shipPictures[shipToCustomize.PictureRef]);
        string empty = string.Empty;
        Bitmap bitmap = (Bitmap) null;
        if (shipOrPlanet is BuiltObject)
          bitmap = new Bitmap(BaconBuiltObjectImageCache.shipPictures[(shipOrPlanet as BuiltObject).PictureRef]);
        else if (shipOrPlanet is Habitat)
        {
          bool smallImageSupplied = false;
          bitmap = new Bitmap((Image) BaconBuiltObject.myMain.habitatImageCache_0.FastGetImage((int) (shipOrPlanet as Habitat).PictureRef, out smallImageSupplied));
        }
        if (bitmap != null)
          this.tradePartnerPictureBox.Image = (Image) bitmap;
        this.wasPaused = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
        if (!this.wasPaused)
          main._Game.Galaxy.Pause();
        this.Show();
      }
      catch (Exception ex)
      {
        BaconMain.customizeShipFormOpen = false;
      }
      finally
      {
        BaconMain.customizeShipFormOpen = false;
      }
    }

    public void UpdateInventories()
    {
      this.totalCashLabel.Text = "Total cash reserves: " + this.ship.BaconValues["cash"]?.ToString();
      int index1 = 0;
      int index2 = 0;
      if (this.unequipedComponentsDataGridView.CurrentCell != null)
        index1 = this.unequipedComponentsDataGridView.CurrentCell.RowIndex;
      if (this.equipedComponentsDataGridView.CurrentCell != null)
        index2 = this.equipedComponentsDataGridView.CurrentCell.RowIndex;
      this.unequipedComponentsDataGridView.Rows.Clear();
      this.equipedComponentsDataGridView.Rows.Clear();
      BuiltObjectComponentList components1 = this.ship.Components;
      ComponentList components2 = this.ship.Design.Components;
      BuiltObjectComponentList objectComponentList = new BuiltObjectComponentList();
      Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
      foreach (BuiltObjectComponent builtObjectComponent in (SyncList<BuiltObjectComponent>) components1)
      {
        int compID = builtObjectComponent.ComponentID;
        int num1 = components2.Count<DistantWorlds.Types.Component>((Func<DistantWorlds.Types.Component, bool>) (x => x.ComponentID == compID));
        int num2 = components1.Count<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (x => x.ComponentID == compID)) - num1;
        if (num2 > 0 && !dictionary1.ContainsKey(compID))
          dictionary1.Add(compID, num2);
      }
      Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
      if (this.ship.BaconValues != null && this.ship.BaconValues.ContainsKey("scrapComponents"))
      {
        List<DistantWorlds.Types.Component> baconValue = (List<DistantWorlds.Types.Component>) this.ship.BaconValues["scrapComponents"];
        foreach (DistantWorlds.Types.Component component in baconValue)
        {
          DistantWorlds.Types.Component comp = component;
          if (!dictionary2.ContainsKey(comp.ComponentID))
          {
            int num = baconValue.Count<DistantWorlds.Types.Component>((Func<DistantWorlds.Types.Component, bool>) (x => x.ComponentID == comp.ComponentID));
            dictionary2.Add(comp.ComponentID, num);
          }
        }
      }
      int index3 = 0;
      foreach (KeyValuePair<int, int> keyValuePair in dictionary1)
      {
        KeyValuePair<int, int> componentIDAndQuantity = keyValuePair;
        DataGridViewRow dataGridViewRow = new DataGridViewRow();
        string name = ((IEnumerable<ComponentDefinition>) BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>) (x => x.ComponentID == componentIDAndQuantity.Key)).Name;
        object[] objArray = new object[3]
        {
          (object) componentIDAndQuantity.Key,
          (object) componentIDAndQuantity.Value,
          (object) name
        };
        this.equipedComponentsDataGridView.Rows.Add(objArray);
        this.equipedComponentsDataGridView.Rows[index3].SetValues(objArray);
        ++index3;
      }
      int index4 = 0;
      foreach (KeyValuePair<int, int> keyValuePair in dictionary2)
      {
        KeyValuePair<int, int> componentIDAndQuantity = keyValuePair;
        DataGridViewRow dataGridViewRow = new DataGridViewRow();
        string name = ((IEnumerable<ComponentDefinition>) BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>) (x => x.ComponentID == componentIDAndQuantity.Key)).Name;
        object[] objArray = new object[3]
        {
          (object) componentIDAndQuantity.Key,
          (object) componentIDAndQuantity.Value,
          (object) name
        };
        this.unequipedComponentsDataGridView.Rows.Add(objArray);
        this.unequipedComponentsDataGridView.Rows[index4].SetValues(objArray);
        ++index4;
      }
      if (this.unequipedComponentsDataGridView.Rows != null && this.unequipedComponentsDataGridView.Rows.Count > 0 && this.unequipedComponentsDataGridView.Rows.Count <= index1)
        index1 = Math.Max(0, this.unequipedComponentsDataGridView.Rows.Count - 1);
      if (this.unequipedComponentsDataGridView.Rows != null && this.unequipedComponentsDataGridView.Rows.Count > index1)
      {
        this.unequipedComponentsDataGridView.Rows[index1].Selected = true;
        this.unequipedComponentsDataGridView.CurrentCell = this.unequipedComponentsDataGridView.Rows[index1].Cells[2];
      }
      if (this.equipedComponentsDataGridView.Rows != null && this.equipedComponentsDataGridView.Rows.Count > index2)
      {
        this.equipedComponentsDataGridView.Rows[index2].Selected = true;
        this.equipedComponentsDataGridView.CurrentCell = this.equipedComponentsDataGridView.Rows[index2].Cells[2];
      }
      this.unequipedComponentsDataGridView.Refresh();
      this.equipedComponentsDataGridView.Refresh();
    }

    public void DisableButtonsWhenTradePartnerNotPresent()
    {
      if (this.planetDoingTheMod == null)
      {
        this.equipButton.Enabled = false;
        this.unequipButton.Enabled = false;
        this.tradePartnerPictureBox.Enabled = false;
        this.planetNamelabel.Enabled = false;
      }
      if ((int) this.ship.BaconValues["cash"] < BaconMain.componentEquipCost || this.ship.PirateEmpireId == (byte) 0)
        this.equipButton.Enabled = false;
      else
        this.equipButton.Enabled = true;
    }

    private void SizeLabelFont(Label lbl)
    {
      string text = lbl.Text;
      if (text.Length <= 0)
        return;
      int emSize1 = 100;
      int num1 = lbl.DisplayRectangle.Width - 3;
      int num2 = lbl.DisplayRectangle.Height - 3;
      using (Graphics graphics = lbl.CreateGraphics())
      {
        for (int emSize2 = 1; emSize2 <= 100; ++emSize2)
        {
          using (Font font = new Font(lbl.Font.FontFamily, (float) emSize2))
          {
            SizeF sizeF = graphics.MeasureString(text, font);
            if ((double) sizeF.Width > (double) num1 || (double) sizeF.Height > (double) num2)
            {
              emSize1 = emSize2 - 1;
              break;
            }
          }
        }
      }
      lbl.Font = new Font(lbl.Font.FontFamily, (float) emSize1);
    }

    private void unequipButton_Click(object sender, EventArgs e)
    {
      if (this.equipedComponentsDataGridView.SelectedRows == null || this.equipedComponentsDataGridView.SelectedRows.Count <= 0)
        return;
      this.MoveComponent(this.equipedComponentsDataGridView, this.unequipedComponentsDataGridView);
    }

    private void equipButton_Click(object sender, EventArgs e)
    {
      if (this.unequipedComponentsDataGridView.SelectedRows == null || this.unequipedComponentsDataGridView.SelectedRows.Count <= 0)
        return;
      this.MoveComponent(this.unequipedComponentsDataGridView, this.equipedComponentsDataGridView);
      int num = (int) this.ship.BaconValues["cash"] - BaconMain.componentEquipCost;
      this.ship.BaconValues["cash"] = (object) num;
      this.totalCashLabel.Text = "Total cash reserves: " + num.ToString();
      if (num < BaconMain.componentEquipCost)
        this.equipButton.Enabled = false;
    }

    private void dumpButton_Click(object sender, EventArgs e)
    {
      if (this.unequipedComponentsDataGridView.SelectedRows == null || this.unequipedComponentsDataGridView.SelectedRows.Count <= 0)
        return;
      this.MoveComponent(this.unequipedComponentsDataGridView, (DataGridView) null);
    }

    private void MoveComponent(DataGridView from, DataGridView to)
    {
      DataGridViewRow selectedRow = from.SelectedRows[0];
      int index = selectedRow.Index;
      int num = (int) selectedRow.Cells[1].Value;
      if (to != null)
        CustomizeShipForm.AddComponentToDataGridView(to, (int) selectedRow.Cells[0].Value);
      if (num > 1)
      {
        selectedRow.Cells[1].Value = (object) (num - 1);
      }
      else
      {
        from.Rows.Remove(from.SelectedRows[0]);
        if (index > 0)
          from.Rows[index - 1].Selected = true;
        else if (from.Rows.Count > 0)
          from.Rows[0].Selected = true;
      }
    }

    public static void AddComponentToDataGridView(DataGridView to, int componentID)
    {
      DataGridViewRow dataGridViewRow = (DataGridViewRow) null;
      foreach (DataGridViewRow row in (IEnumerable) to.Rows)
      {
        if ((int) row.Cells[0].Value == componentID)
        {
          dataGridViewRow = row;
          break;
        }
      }
      if (dataGridViewRow != null)
      {
        int num = (int) dataGridViewRow.Cells[1].Value;
        dataGridViewRow.Cells[1].Value = (object) (num + 1);
      }
      else
      {
        string name = ((IEnumerable<ComponentDefinition>) BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>) (x => x.ComponentID == componentID)).Name;
        object[] objArray = new object[3]
        {
          (object) componentID,
          (object) 1,
          (object) name
        };
        to.Rows.Add(objArray);
        to.Rows[to.Rows.Count - 1].SetValues(objArray);
      }
    }

    private void CustomizeShipForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.StoreUneqipedComponents();
      this.RecalculateShipWithNewlyEquipedComponents();
      BaconMain.tradeFormOpen = false;
      if (this.wasPaused || BaconBuiltObject.myMain == null)
        return;
      BaconBuiltObject.myMain._Game.Galaxy.Resume();
    }

    public void StoreUneqipedComponents()
    {
      List<DistantWorlds.Types.Component> componentList = new List<DistantWorlds.Types.Component>();
      DataGridViewRowCollection rows = this.unequipedComponentsDataGridView.Rows;
      for (int index1 = 0; index1 < rows.Count; ++index1)
      {
        int num = (int) rows[index1].Cells[1].Value;
        int componentID = (int) rows[index1].Cells[0].Value;
        for (int index2 = 0; index2 < num; ++index2)
          componentList.Add((DistantWorlds.Types.Component) new BuiltObjectComponent(componentID, ComponentStatus.Normal));
      }
      this.ship.BaconValues["scrapComponents"] = (object) componentList;
    }

    public void RecalculateShipWithNewlyEquipedComponents()
    {
      this.ship.Components = new BuiltObjectComponentList();
      foreach (DistantWorlds.Types.Component component in (SyncList<DistantWorlds.Types.Component>) this.ship.Design.Components)
        this.ship.Components.Add(new BuiltObjectComponent(component.ComponentID, ComponentStatus.Normal));
      DataGridViewRowCollection rows = this.equipedComponentsDataGridView.Rows;
      for (int index1 = 0; index1 < rows.Count; ++index1)
      {
        int num = (int) rows[index1].Cells[1].Value;
        int componentID = (int) rows[index1].Cells[0].Value;
        for (int index2 = 0; index2 < num; ++index2)
          this.ship.Components.Add(new BuiltObjectComponent(componentID, ComponentStatus.Normal));
      }
      this.ship.ReDefine();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tradePartnerPictureBox = new PictureBox();
      this.shipPictureBox = new PictureBox();
      this.planetNamelabel = new Label();
      this.shipNamelabel = new Label();
      this.equipedComponentsDataGridView = new DataGridView();
      this.unequipButton = new Button();
      this.equipButton = new Button();
      this.dumpButton = new Button();
      this.componentID = new DataGridViewTextBoxColumn();
      this.planetQuantityColumn = new DataGridViewTextBoxColumn();
      this.planetCargoNameColumn = new DataGridViewTextBoxColumn();
      this.unequipedComponentsDataGridView = new DataGridView();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.totalCashLabel = new Label();
      ((ISupportInitialize) this.tradePartnerPictureBox).BeginInit();
      ((ISupportInitialize) this.shipPictureBox).BeginInit();
      ((ISupportInitialize) this.equipedComponentsDataGridView).BeginInit();
      ((ISupportInitialize) this.unequipedComponentsDataGridView).BeginInit();
      this.SuspendLayout();
      this.tradePartnerPictureBox.Location = new Point(513, 13);
      this.tradePartnerPictureBox.Name = "tradePartnerPictureBox";
      this.tradePartnerPictureBox.Size = new Size(100, 100);
      this.tradePartnerPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.tradePartnerPictureBox.TabIndex = 20;
      this.tradePartnerPictureBox.TabStop = false;
      this.shipPictureBox.Location = new Point(363, 13);
      this.shipPictureBox.Name = "shipPictureBox";
      this.shipPictureBox.Size = new Size(100, 100);
      this.shipPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.shipPictureBox.TabIndex = 19;
      this.shipPictureBox.TabStop = false;
      this.planetNamelabel.Font = new Font("Microsoft Sans Serif", 27.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.planetNamelabel.Location = new Point(612, 25);
      this.planetNamelabel.Name = "planetNamelabel";
      this.planetNamelabel.Size = new Size(426, 42);
      this.planetNamelabel.TabIndex = 18;
      this.planetNamelabel.Text = "Planet name here";
      this.shipNamelabel.Font = new Font("Microsoft Sans Serif", 27.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.shipNamelabel.Location = new Point(10, 25);
      this.shipNamelabel.Name = "shipNamelabel";
      this.shipNamelabel.Size = new Size(346, 42);
      this.shipNamelabel.TabIndex = 17;
      this.shipNamelabel.Text = "Ship name here";
      this.equipedComponentsDataGridView.AllowUserToAddRows = false;
      this.equipedComponentsDataGridView.AllowUserToDeleteRows = false;
      this.equipedComponentsDataGridView.AllowUserToResizeRows = false;
      this.equipedComponentsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.equipedComponentsDataGridView.Columns.AddRange((DataGridViewColumn) this.componentID, (DataGridViewColumn) this.planetQuantityColumn, (DataGridViewColumn) this.planetCargoNameColumn);
      this.equipedComponentsDataGridView.Location = new Point(618, 70);
      this.equipedComponentsDataGridView.MultiSelect = false;
      this.equipedComponentsDataGridView.Name = "equipedComponentsDataGridView";
      this.equipedComponentsDataGridView.ReadOnly = true;
      this.equipedComponentsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.equipedComponentsDataGridView.Size = new Size(419, 278);
      this.equipedComponentsDataGridView.StandardTab = true;
      this.equipedComponentsDataGridView.TabIndex = 21;
      this.unequipButton.Location = new Point(537, 199);
      this.unequipButton.Name = "unequipButton";
      this.unequipButton.Size = new Size(75, 23);
      this.unequipButton.TabIndex = 23;
      this.unequipButton.TabStop = false;
      this.unequipButton.Text = "Unequip";
      this.unequipButton.UseVisualStyleBackColor = true;
      this.unequipButton.Click += new EventHandler(this.unequipButton_Click);
      this.equipButton.Location = new Point(537, 405);
      this.equipButton.Name = "equipButton";
      this.equipButton.Size = new Size(75, 23);
      this.equipButton.TabIndex = 24;
      this.equipButton.TabStop = false;
      this.equipButton.Text = "Equip";
      this.equipButton.UseVisualStyleBackColor = true;
      this.equipButton.Click += new EventHandler(this.equipButton_Click);
      this.dumpButton.Location = new Point(537, 449);
      this.dumpButton.Name = "dumpButton";
      this.dumpButton.Size = new Size(75, 23);
      this.dumpButton.TabIndex = 25;
      this.dumpButton.TabStop = false;
      this.dumpButton.Text = "Dump";
      this.dumpButton.UseVisualStyleBackColor = true;
      this.dumpButton.Click += new EventHandler(this.dumpButton_Click);
      this.componentID.HeaderText = "componentId";
      this.componentID.Name = "componentID";
      this.componentID.ReadOnly = true;
      this.componentID.Visible = false;
      this.componentID.Width = 5;
      this.planetQuantityColumn.FillWeight = 25f;
      this.planetQuantityColumn.HeaderText = "Quantity";
      this.planetQuantityColumn.Name = "planetQuantityColumn";
      this.planetQuantityColumn.ReadOnly = true;
      this.planetQuantityColumn.Width = 50;
      this.planetCargoNameColumn.HeaderText = "Component Name";
      this.planetCargoNameColumn.Name = "planetCargoNameColumn";
      this.planetCargoNameColumn.ReadOnly = true;
      this.planetCargoNameColumn.Width = 330;
      this.unequipedComponentsDataGridView.AllowUserToAddRows = false;
      this.unequipedComponentsDataGridView.AllowUserToDeleteRows = false;
      this.unequipedComponentsDataGridView.AllowUserToResizeRows = false;
      this.unequipedComponentsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.unequipedComponentsDataGridView.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.dataGridViewTextBoxColumn2, (DataGridViewColumn) this.dataGridViewTextBoxColumn3);
      this.unequipedComponentsDataGridView.Location = new Point(619, 354);
      this.unequipedComponentsDataGridView.MultiSelect = false;
      this.unequipedComponentsDataGridView.Name = "unequipedComponentsDataGridView";
      this.unequipedComponentsDataGridView.ReadOnly = true;
      this.unequipedComponentsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.unequipedComponentsDataGridView.Size = new Size(419, 278);
      this.unequipedComponentsDataGridView.StandardTab = true;
      this.unequipedComponentsDataGridView.TabIndex = 26;
      this.dataGridViewTextBoxColumn1.HeaderText = "componentId";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Visible = false;
      this.dataGridViewTextBoxColumn1.Width = 5;
      this.dataGridViewTextBoxColumn2.FillWeight = 25f;
      this.dataGridViewTextBoxColumn2.HeaderText = "Quantity";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.Width = 50;
      this.dataGridViewTextBoxColumn3.HeaderText = "Component Name";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.Width = 330;
      this.totalCashLabel.AutoSize = true;
      this.totalCashLabel.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.totalCashLabel.Location = new Point(13, 93);
      this.totalCashLabel.Name = "totalCashLabel";
      this.totalCashLabel.Size = new Size(150, 20);
      this.totalCashLabel.TabIndex = 27;
      this.totalCashLabel.Text = "Total cash reserves:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1049, 639);
      this.Controls.Add((Control) this.totalCashLabel);
      this.Controls.Add((Control) this.unequipedComponentsDataGridView);
      this.Controls.Add((Control) this.dumpButton);
      this.Controls.Add((Control) this.equipButton);
      this.Controls.Add((Control) this.unequipButton);
      this.Controls.Add((Control) this.equipedComponentsDataGridView);
      this.Controls.Add((Control) this.tradePartnerPictureBox);
      this.Controls.Add((Control) this.shipPictureBox);
      this.Controls.Add((Control) this.planetNamelabel);
      this.Controls.Add((Control) this.shipNamelabel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (CustomizeShipForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Customize Ship";
      this.FormClosed += new FormClosedEventHandler(this.CustomizeShipForm_FormClosed);
      ((ISupportInitialize) this.tradePartnerPictureBox).EndInit();
      ((ISupportInitialize) this.shipPictureBox).EndInit();
      ((ISupportInitialize) this.equipedComponentsDataGridView).EndInit();
      ((ISupportInitialize) this.unequipedComponentsDataGridView).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
