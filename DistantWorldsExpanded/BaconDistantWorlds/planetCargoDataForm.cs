// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.planetCargoDataForm
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Controls;
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
  public class planetCargoDataForm : Form
  {
    public BuiltObject ship = (BuiltObject) null;
    public StellarObject tradePartner = (StellarObject) null;
    public bool wasPaused = false;
    public bool tradePartnerIsPlanet = false;
    public bool tradePartnerIsPirate = false;
    public Habitat nearestColony;
    public int marketLiquidity = 0;
    private IContainer components = (IContainer) null;
    private DataGridView shipCargoDataGridView;
    private DataGridViewTextBoxColumn cargoNameColumn;
    private DataGridViewTextBoxColumn cargoQuantityColumn;
    private DataGridView tradePartnerCargoDataGridView;
    private DataGridViewTextBoxColumn planetCargoNameColumn;
    private DataGridViewTextBoxColumn planetQuantityColumn;
    private DataGridViewTextBoxColumn planetSellPriceColumn;
    private DataGridViewTextBoxColumn planetPurchasePriceColumn;
    private Button sellCargoButton;
    private TextBox sellQuantityTextBox;
    private Label sellAmountLabel;
    private Label buyAmountLabel;
    private TextBox buyQuantityTextBox;
    private Button buyCargoButton;
    private Label shipNamelabel;
    private Label planetNamelabel;
    private Label remainingCargoSpacelabel;
    private Label totalCashLabel;
    private PictureBox shipPictureBox;
    private PictureBox tradePartnerPictureBox;
    private DataGridView tradeHistoryDataGridView;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn resourceColumn;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private Label marketCashLabel;
    private Label tradePartnerRemainingCargoSpaceLabel;
    private Button giveCargoButton;
    private GlassButton sellAllButton;
    private Button buyFakePapersButton;
    private Button takeCargoButton;

    public planetCargoDataForm(Main main, BuiltObject tradeShip, StellarObject shipOrPlanet)
    {
      try
      {
        this.ship = tradeShip;
        this.tradePartner = shipOrPlanet;
        this.InitializeComponent();
        this.UpdateInventories();
        this.LoadTradeHistory();
        this.DisableButtonsWhenTradePartnerNotPresent();
        this.shipNamelabel.Text = this.ship.Name;
        if (this.tradePartner != null)
        {
          this.planetNamelabel.Text = this.tradePartner.Name;
          if (this.tradePartner is Habitat)
            this.tradePartnerIsPlanet = true;
          else if (this.tradePartner is BuiltObject && (this.tradePartner as BuiltObject).Empire.PirateEmpireBaseHabitat != null)
            this.tradePartnerIsPirate = true;
          if (this.tradePartnerIsPlanet && ((this.tradePartner as Habitat).Population == null || (this.tradePartner as Habitat).Population.Count == 0))
          {
            BaconMain.tradeFormOpen = false;
            return;
          }
          if (this.tradePartner is Habitat && (this.tradePartner as Habitat).Empire.PirateEmpireBaseHabitat != null)
            this.tradePartnerIsPirate = true;
        }
        if (tradeShip.Empire == main._Game.Galaxy.IndependentEmpire || this.tradePartner == null || this.tradePartner.Empire != main._Game.Galaxy.IndependentEmpire)
          this.buyFakePapersButton.Hide();
        this.SizeLabelFont(this.shipNamelabel);
        this.SizeLabelFont(this.planetNamelabel);
        this.shipPictureBox.Image = (Image) new Bitmap(BaconBuiltObjectImageCache.shipPictures[tradeShip.PictureRef]);
        string empty = string.Empty;
        Bitmap bitmap = (Bitmap) null;
        if (this.tradePartner is BuiltObject)
        {
          bitmap = new Bitmap(BaconBuiltObjectImageCache.shipPictures[(this.tradePartner as BuiltObject).PictureRef]);
          if ((this.tradePartner as BuiltObject).ActualEmpire == this.ship.ActualEmpire)
            this.takeCargoButton.Show();
          else
            this.takeCargoButton.Hide();
        }
        else if (this.tradePartner is Habitat)
        {
          bool smallImageSupplied = false;
          bitmap = new Bitmap((Image) BaconBuiltObject.myMain.habitatImageCache_0.FastGetImage((int) (this.tradePartner as Habitat).PictureRef, out smallImageSupplied));
          if ((this.tradePartner as Habitat).Empire == this.ship.ActualEmpire)
            this.takeCargoButton.Show();
          else
            this.takeCargoButton.Hide();
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
        BaconMain.tradeFormOpen = false;
      }
      finally
      {
        BaconMain.tradeFormOpen = false;
      }
    }

    public bool IsTradePartnerAPirate()
    {
      this.tradePartnerIsPirate = this.tradePartner is Habitat && (this.tradePartner as Habitat).Empire.PirateEmpireBaseHabitat != null || this.tradePartner is BuiltObject && (this.tradePartner as BuiltObject).Empire.PirateEmpireBaseHabitat != null;
      return this.tradePartnerIsPirate;
    }

    public void UpdateInventories()
    {
      this.nearestColony = BaconGalaxy.FastFindNearestColonyAnyEmpire(this.ship.Xpos, this.ship.Ypos, true);
      this.remainingCargoSpacelabel.Text = "Remaining Cargo space: " + this.ship.CargoSpace.ToString();
      this.totalCashLabel.Text = "Total cash reserves: " + this.ship.BaconValues["cash"]?.ToString();
      this.marketCashLabel.Text = "Market liquidity: " + this.CalculateMarketLiquidity();
      if (this.tradePartner != null)
      {
        if (this.tradePartner is BuiltObject && (this.tradePartner as BuiltObject).CargoCapacity < 1000000)
          this.tradePartnerRemainingCargoSpaceLabel.Text = this.tradePartner.Name + " remaining cargo space: " + (this.tradePartner as BuiltObject).CargoSpace.ToString();
        else
          this.tradePartnerRemainingCargoSpaceLabel.Text = this.tradePartner.Name + " remaining cargo space: Unlimited";
      }
      int index1 = 0;
      int index2 = 0;
      if (this.shipCargoDataGridView.CurrentCell != null)
        index1 = this.shipCargoDataGridView.CurrentCell.RowIndex;
      if (this.tradePartnerCargoDataGridView.CurrentCell != null)
        index2 = this.tradePartnerCargoDataGridView.CurrentCell.RowIndex;
      this.shipCargoDataGridView.Rows.Clear();
      this.tradePartnerCargoDataGridView.Rows.Clear();
      CargoList cargo1 = this.ship.Cargo;
      CargoList cargoList = (CargoList) null;
      if (this.tradePartner != null)
        cargoList = this.tradePartner.Cargo;
      int index3 = 0;
      foreach (Cargo cargo2 in (SyncList<Cargo>) cargo1)
      {
        if (cargo2.CommodityIsResource)
        {
          DataGridViewRow dataGridViewRow = new DataGridViewRow();
          if (cargo2.EmpireId == this.ship.ActualEmpire.EmpireId)
          {
            object[] objArray = new object[2]
            {
              (object) cargo2.Resource.Name,
              (object) cargo2.Amount
            };
            this.shipCargoDataGridView.Rows.Add(objArray);
            this.shipCargoDataGridView.Rows[index3].SetValues(objArray);
            ++index3;
          }
        }
      }
      if (cargoList != null)
      {
        this.PopulateGridWithActualPrices();
        int num1 = 0;
        int num2 = 0;
        if (this.tradePartner != null && this.tradePartner.Empire != null)
        {
          if (this.tradePartner is Habitat)
          {
            num1 = 2000;
            num2 = (this.tradePartner as Habitat).Empire.EmpireId;
          }
          else
          {
            num1 = 500;
            if (this.tradePartner is BuiltObject)
              num2 = (this.tradePartner as BuiltObject).ActualEmpire.EmpireId;
          }
        }
        foreach (Cargo cargo3 in (SyncList<Cargo>) cargoList)
        {
          if (cargo3.EmpireId == num2 && cargo3.Available > cargo3.Reserved + num1 && cargo3.CommodityIsResource)
          {
            foreach (DataGridViewRow row in (IEnumerable) this.tradePartnerCargoDataGridView.Rows)
            {
              if (row.Cells[0].Value.ToString() == cargo3.Resource.Name)
              {
                row.Cells[1].Value = (object) Math.Max(0, cargo3.Available - (cargo3.Reserved + num1));
                break;
              }
            }
          }
        }
      }
      if (this.shipCargoDataGridView.Rows != null && this.shipCargoDataGridView.Rows.Count > 0 && this.shipCargoDataGridView.Rows.Count <= index1)
        index1 = Math.Max(0, this.shipCargoDataGridView.Rows.Count - 1);
      if (this.shipCargoDataGridView.Rows != null && this.shipCargoDataGridView.Rows.Count > index1)
      {
        this.shipCargoDataGridView.Rows[index1].Selected = true;
        this.shipCargoDataGridView.CurrentCell = this.shipCargoDataGridView.Rows[index1].Cells[0];
      }
      if (this.tradePartnerCargoDataGridView.Rows != null && this.tradePartnerCargoDataGridView.Rows.Count > index2)
      {
        this.tradePartnerCargoDataGridView.Rows[index2].Selected = true;
        this.tradePartnerCargoDataGridView.CurrentCell = this.tradePartnerCargoDataGridView.Rows[index2].Cells[0];
      }
      this.shipCargoDataGridView.Refresh();
      this.tradePartnerCargoDataGridView.Refresh();
    }

    public void PopulateGridWithActualPrices()
    {
      if (this.nearestColony == null)
        return;
      double num1 = (double) this.nearestColony.TaxRate / 2.0;
      if (this.nearestColony.BaconValues == null)
        this.nearestColony.BaconValues = new Dictionary<string, object>();
      if (!this.nearestColony.BaconValues.ContainsKey("resourcePriceList"))
        this.nearestColony.BaconValues["resourcePriceList"] = (object) BaconHabitat.CreatePlanetResourcePriceList(this.nearestColony);
      Dictionary<string, double> baconValue = (Dictionary<string, double>) this.nearestColony.BaconValues["resourcePriceList"];
      if (!BaconHabitat.addSalesTax)
        num1 = 0.0;
      double num2 = 1.0;
      double num3 = 1.0;
      double num4 = 1.0 - Math.Max(0.1, Math.Min(0.9, BaconMain.tradeTax + num1));
      if (this.tradePartner != null && this.tradePartner is BuiltObject && ((this.tradePartner as BuiltObject).Role == BuiltObjectRole.Freight || (this.tradePartner as BuiltObject).Role == BuiltObjectRole.Resource))
        num2 = 1.1;
      if (this.tradePartner != null && this.tradePartner.Empire != null && this.tradePartner.Empire.PirateEmpireBaseHabitat != null)
        num3 = 0.9;
      if (this.tradePartner != null && this.tradePartner is BuiltObject && (this.tradePartner as BuiltObject).SubRole == BuiltObjectSubRole.ConstructionShip && (BaconBuiltObject.myMain == null || (this.tradePartner as BuiltObject).ActualEmpire != BaconBuiltObject.myMain._Game.PlayerEmpire))
        this.buyCargoButton.Enabled = false;
      int num5 = 0;
      foreach (KeyValuePair<string, double> keyValuePair in baconValue)
      {
        string resourceName = keyValuePair.Key;
        double num6 = keyValuePair.Value;
        double resourceCurrentPrice = BaconBuiltObject.myMain._Game.Galaxy.ResourceCurrentPrices[num5];
        double num7 = num6 * resourceCurrentPrice * num2 * num3;
        double num8 = 0.0;
        double num9 = 1.0;
        if (this.tradePartner != null && this.tradePartner is BuiltObject && (this.tradePartner as BuiltObject).SubRole == BuiltObjectSubRole.ConstructionShip)
        {
          ManufacturingQueue manufacturingQueue = (this.tradePartner as BuiltObject).ManufacturingQueue;
          if (manufacturingQueue != null && manufacturingQueue.DeficientResources.Contains((byte) num5))
            num9 = 3.0;
        }
        else if (this.tradePartner != null && this.tradePartner is BuiltObject && ((this.tradePartner as BuiltObject).SubRole == BuiltObjectSubRole.SmallSpacePort || (this.tradePartner as BuiltObject).SubRole == BuiltObjectSubRole.MediumSpacePort || (this.tradePartner as BuiltObject).SubRole == BuiltObjectSubRole.LargeSpacePort))
        {
          ManufacturingQueue manufacturingQueue = (this.tradePartner as BuiltObject).ManufacturingQueue;
          if (manufacturingQueue != null && manufacturingQueue.DeficientResources.Contains((byte) num5))
            num9 = 3.0;
        }
        else if (this.tradePartner != null && this.tradePartner is Habitat && (this.tradePartner as Habitat).Empire != null)
        {
          ManufacturingQueue manufacturingQueue = (this.tradePartner as Habitat).ManufacturingQueue;
          if (manufacturingQueue != null && manufacturingQueue.DeficientResources.Contains((byte) num5))
            num9 = 3.0;
        }
        if (this.tradePartner != null && this.tradePartner is Habitat)
        {
          ResourceDefinition resourceDefinition = BaconBuiltObject.myMain._Game.Galaxy.ResourceSystem.Resources.FirstOrDefault<ResourceDefinition>((Func<ResourceDefinition, bool>) (x => x.Name == resourceName));
          int resourceID = -1;
          if (resourceDefinition != null)
            resourceID = (int) resourceDefinition.ResourceID;
          IEnumerable<EmpireActivity> source = BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire.PirateMissions.Where<EmpireActivity>((Func<EmpireActivity, bool>) (x => x.Type == EmpireActivityType.Smuggle && x.Target != null && x.Target.Name == (this.tradePartner as Habitat).Name && (int) x.ResourceId == resourceID));
          if (source != null && source.Count<EmpireActivity>() > 0)
            num9 *= 1.2999999523162842;
        }
        if (this.ship.Components.Count<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (x => x.Category == ComponentCategoryType.Extractor)) > 0)
          num8 = resourceCurrentPrice * 0.8;
        object[] objArray = new object[4]
        {
          (object) resourceName,
          (object) 0,
          (object) (num7 * num9),
          (object) (num7 * num9 * num4 - num8)
        };
        this.tradePartnerCargoDataGridView.Rows.Add(objArray);
        this.tradePartnerCargoDataGridView.Rows[num5].SetValues(objArray);
        double num10 = num7 * num9 / resourceCurrentPrice;
        if (num10 > 1.6)
          this.tradePartnerCargoDataGridView.Rows[num5].DefaultCellStyle.BackColor = Color.Gold;
        else if (num10 > 1.3)
          this.tradePartnerCargoDataGridView.Rows[num5].DefaultCellStyle.BackColor = Color.Silver;
        else if (num10 < 0.6)
          this.tradePartnerCargoDataGridView.Rows[num5].DefaultCellStyle.BackColor = Color.LightGreen;
        else if (num10 < 0.3)
          this.tradePartnerCargoDataGridView.Rows[num5].DefaultCellStyle.BackColor = Color.DarkRed;
        ++num5;
      }
    }

    public void DisableButtonsWhenTradePartnerNotPresent()
    {
      if (this.tradePartner == null)
      {
        this.sellQuantityTextBox.Enabled = false;
        this.sellCargoButton.Enabled = false;
        this.sellAllButton.Enabled = false;
        this.buyCargoButton.Enabled = false;
        this.buyQuantityTextBox.Enabled = false;
        this.tradePartnerPictureBox.Visible = false;
        this.planetNamelabel.Visible = false;
        this.marketCashLabel.Visible = false;
        this.tradePartnerCargoDataGridView.Visible = false;
        this.tradePartnerRemainingCargoSpaceLabel.Visible = false;
        this.Width = 500;
      }
      if (this.ship.Components.Count<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>) (x => x.Category == ComponentCategoryType.Extractor)) <= 0)
        return;
      this.buyCargoButton.Enabled = false;
      this.buyQuantityTextBox.Enabled = false;
    }

    private void cargoTransferForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (this.ship.BaconValues == null)
        return;
      List<object[]> objArrayList1 = new List<object[]>();
      List<object[]> objArrayList2 = this.ship.BaconValues.ContainsKey("tradeHistory") ? (List<object[]>) this.ship.BaconValues["tradeHistory"] : new List<object[]>();
      for (int index = 0; index < this.tradeHistoryDataGridView.Rows.Count; ++index)
      {
        object[] objArray = new object[5]
        {
          this.tradeHistoryDataGridView.Rows[index].Cells[0].Value,
          this.tradeHistoryDataGridView.Rows[index].Cells[1].Value,
          this.tradeHistoryDataGridView.Rows[index].Cells[2].Value,
          this.tradeHistoryDataGridView.Rows[index].Cells[3].Value,
          this.tradeHistoryDataGridView.Rows[index].Cells[4].Value
        };
        objArrayList1.Add(objArray);
      }
      this.ship.BaconValues["tradeHistory"] = (object) objArrayList1;
      BaconMain.tradeFormOpen = false;
      if (this.wasPaused || BaconBuiltObject.myMain == null)
        return;
      BaconBuiltObject.myMain._Game.Galaxy.Resume();
    }

    private void shipCargoDataGridView_SelectionChanged(object sender, EventArgs e)
    {
      if (this.shipCargoDataGridView.SelectedCells == null || this.shipCargoDataGridView.SelectedCells.Count < 1 || this.shipCargoDataGridView.SelectedRows == null || this.shipCargoDataGridView.SelectedRows.Count < 1)
        return;
      this.sellQuantityTextBox.Text = this.shipCargoDataGridView.SelectedRows[0].Cells[1].Value.ToString();
    }

    private void tradePartnerCargoDataGridView_SelectionChanged(object sender, EventArgs e) => this.buyQuantityTextBox.Text = this.CalculateMaxResourcePurchasable().ToString();

    private void sellCargoButton_Click(object sender, EventArgs e) => this.sellOrGiveCargoButton(sender, e);

    private void giveCargoButton_Click(object sender, EventArgs e) => this.sellOrGiveCargoButton(sender, e, true);

    private void sellOrGiveCargoButton(object sender, EventArgs e, bool giveAwayForFree = false)
    {
      int result = 0;
      if (!int.TryParse(this.sellQuantityTextBox.Text, out result))
        return;
      int resourceSellable = this.CalculateMaxResourceSellable(giveAwayForFree);
      if (result > resourceSellable)
        result = resourceSellable;
      DataGridViewSelectedRowCollection selectedRows = this.shipCargoDataGridView.SelectedRows;
      if (selectedRows == null)
        return;
      if (selectedRows == null)
        return;
      Main main = BaconBuiltObject.myMain;
      if (main == null || selectedRows.Count < 1)
        return;
      string resourceName = (string) selectedRows[0].Cells[0].Value;
      double price = giveAwayForFree ? 0.0 : this.FindResourcePriceFromTradePartner(resourceName);
      if (price < 0.0)
        return;
      int num = (int) Math.Ceiling((double) result * price);
      if (giveAwayForFree)
        num = 0;
      this.ship.BaconValues["cash"] = (object) ((int) this.ship.BaconValues["cash"] + num);
      Empire empire = !(this.tradePartner is BuiltObject) ? this.tradePartner.Empire : (this.tradePartner as BuiltObject).ActualEmpire;
      if (empire != null)
      {
        if (this.tradePartnerIsPirate || empire.GovernmentAttributes != null && empire.GovernmentAttributes.SpecialFunctionCode == 1)
          empire.StateMoney -= (double) num;
        else
          empire.PrivateMoney -= (double) num;
        if (this.nearestColony.BaconValues.ContainsKey("marketcash"))
        {
          int baconValue = (int) this.nearestColony.BaconValues["marketcash"];
          this.nearestColony.BaconValues["marketcash"] = (object) ((int) this.nearestColony.BaconValues["marketcash"] - num);
        }
      }
      DataGridViewRow dataGridViewRow = (DataGridViewRow) null;
      foreach (DataGridViewRow row in (IEnumerable) this.tradePartnerCargoDataGridView.Rows)
      {
        if (row.Cells[0].Value.ToString() == resourceName)
        {
          dataGridViewRow = row;
          break;
        }
      }
      if (result > 0)
      {
        if (dataGridViewRow != null)
        {
          dataGridViewRow.Cells[1].Value = (object) ((int) dataGridViewRow.Cells[1].Value + result);
          this.shipCargoDataGridView.SelectedRows[0].Cells[1].Value = (object) ((int) this.shipCargoDataGridView.SelectedRows[0].Cells[1].Value - result);
        }
        CargoList cargo1 = this.ship.Cargo;
        Cargo cargo2 = cargo1.FirstOrDefault<Cargo>((Func<Cargo, bool>) (x => x.CommodityIsResource && x.Resource.Name == this.shipCargoDataGridView.SelectedRows[0].Cells[0].Value.ToString()));
        if (cargo2 != null)
        {
          cargo2.Amount -= result;
          if (cargo2.Amount <= 0)
            cargo1.Remove(cargo2);
        }
        Cargo cargo3 = this.tradePartner.Cargo.FirstOrDefault<Cargo>((Func<Cargo, bool>) (x => x.CommodityIsResource && x.Resource.Name == this.shipCargoDataGridView.SelectedRows[0].Cells[0].Value.ToString() && x.EmpireId == this.tradePartner.Empire.EmpireId));
        if (cargo3 != null)
        {
          cargo3.Amount += result;
        }
        else
        {
          Resource resource = (Resource) null;
          ResourceDefinitionList resources = main._Game.Galaxy.ResourceSystem.Resources;
          for (int index = 0; index < resources.Count; ++index)
          {
            if (resources[index].Name == this.shipCargoDataGridView.SelectedRows[0].Cells[0].Value.ToString())
              resource = new Resource(resources[index].ResourceID);
          }
          if (resource != null)
            this.tradePartner.Cargo.Add(new Cargo(resource, result, this.tradePartner.Empire));
        }
        this.LogTransaction("Sale", resourceName, result, price);
      }
      this.UpdateInventories();
      this.shipCargoDataGridView.Focus();
    }

    private void buyCargoButton_Click(object sender, EventArgs e) => this.BuyOrTakeCargo(sender, e);

    private void takeCargoButton_Click(object sender, EventArgs e) => this.BuyOrTakeCargo(sender, e, true);

    private void BuyOrTakeCargo(object sender, EventArgs e, bool takeForFree = false)
    {
      int result = 0;
      if (!int.TryParse(this.buyQuantityTextBox.Text, out result))
        return;
      int resourcePurchasable = this.CalculateMaxResourcePurchasable(takeForFree);
      if (result > resourcePurchasable)
        result = resourcePurchasable;
      DataGridViewSelectedRowCollection selectedRows = this.tradePartnerCargoDataGridView.SelectedRows;
      if (selectedRows == null)
        return;
      Main main = BaconBuiltObject.myMain;
      if (main == null)
        return;
      DataGridViewRow dataGridViewRow1 = selectedRows[0];
      string resourceName = (string) dataGridViewRow1.Cells[0].Value;
      double price = (double) dataGridViewRow1.Cells[2].Value;
      int num = (int) Math.Ceiling((double) result * price);
      if (takeForFree)
        num = 0;
      this.ship.BaconValues["cash"] = (object) ((int) this.ship.BaconValues["cash"] - num);
      Empire empire = !(this.tradePartner is BuiltObject) ? this.tradePartner.Empire : (this.tradePartner as BuiltObject).ActualEmpire;
      if (empire != null)
      {
        if (this.tradePartnerIsPirate)
          empire.StateMoney += (double) num;
        else
          empire.PrivateMoney += (double) num;
        if (this.nearestColony.BaconValues.ContainsKey("marketcash"))
        {
          int baconValue = (int) this.nearestColony.BaconValues["marketcash"];
          this.nearestColony.BaconValues["marketcash"] = (object) ((int) this.nearestColony.BaconValues["marketcash"] + num);
        }
      }
      DataGridViewRow dataGridViewRow2 = (DataGridViewRow) null;
      foreach (DataGridViewRow row in (IEnumerable) this.shipCargoDataGridView.Rows)
      {
        if (row.Cells[0].Value.ToString() == resourceName)
        {
          dataGridViewRow2 = row;
          break;
        }
      }
      if (result > 0)
      {
        if (dataGridViewRow2 != null)
        {
          dataGridViewRow2.Cells[1].Value = (object) ((int) dataGridViewRow2.Cells[1].Value + result);
          this.tradePartnerCargoDataGridView.SelectedRows[0].Cells[1].Value = (object) ((int) this.tradePartnerCargoDataGridView.SelectedRows[0].Cells[1].Value - result);
        }
        else
        {
          object[] objArray = new object[2]
          {
            (object) resourceName,
            (object) result
          };
          this.shipCargoDataGridView.Rows.Add(objArray);
          this.shipCargoDataGridView.Rows[this.shipCargoDataGridView.Rows.Count - 1].SetValues(objArray);
          this.tradePartnerCargoDataGridView.SelectedRows[0].Cells[1].Value = (object) ((int) this.tradePartnerCargoDataGridView.SelectedRows[0].Cells[1].Value - result);
        }
        Cargo cargo1 = this.ship.Cargo.FirstOrDefault<Cargo>((Func<Cargo, bool>) (x => x.CommodityIsResource && x.Resource.Name == this.tradePartnerCargoDataGridView.SelectedRows[0].Cells[0].Value.ToString()));
        if (cargo1 != null)
        {
          cargo1.Amount += result;
        }
        else
        {
          Resource resource = (Resource) null;
          ResourceDefinitionList resources = main._Game.Galaxy.ResourceSystem.Resources;
          for (int index = 0; index < resources.Count; ++index)
          {
            if (resources[index].Name == this.tradePartnerCargoDataGridView.SelectedRows[0].Cells[0].Value.ToString())
              resource = new Resource(resources[index].ResourceID);
          }
          if (resource != null)
            this.ship.Cargo.Add(new Cargo(resource, result, this.ship.ActualEmpire));
        }
        Cargo cargo2 = this.tradePartner.Cargo.FirstOrDefault<Cargo>((Func<Cargo, bool>) (x => x.CommodityIsResource && x.Resource.Name == this.tradePartnerCargoDataGridView.SelectedRows[0].Cells[0].Value.ToString()));
        if (cargo2 != null)
          cargo2.Amount -= result;
        this.LogTransaction("Purchase", resourceName, result, price);
      }
      this.UpdateInventories();
      this.tradePartnerCargoDataGridView.Focus();
    }

    public int CalculateMaxResourcePurchasable(bool takeForFree = false)
    {
      if (this.tradePartnerCargoDataGridView.SelectedRows == null || this.tradePartnerCargoDataGridView.SelectedRows.Count < 1)
        return 0;
      DataGridViewRow selectedRow = this.tradePartnerCargoDataGridView.SelectedRows[0];
      int val2 = (int) selectedRow.Cells[1].Value;
      int val1 = Math.Min((int) ((double) (int) this.ship.BaconValues["cash"] / (double) selectedRow.Cells[2].Value), val2);
      if (takeForFree)
        val1 = val2;
      return Math.Min(val1, this.ship.CargoSpace);
    }

    public string CalculateMarketLiquidity()
    {
      this.marketLiquidity = this.tradePartner == null || !this.IsTradePartnerAPirate() || !(this.tradePartner is BuiltObject) || (this.tradePartner as BuiltObject).ActualEmpire != this.ship.ActualEmpire || (this.tradePartner as BuiltObject).Role != BuiltObjectRole.Base ? (!this.nearestColony.BaconValues.ContainsKey("marketcash") ? 0 : (int) this.nearestColony.BaconValues["marketcash"]) : (int) this.tradePartner.Empire.StateMoney;
      return this.marketLiquidity.ToString();
    }

    public int CalculateMaxResourceSellable(bool givingAwayForFree = false)
    {
      if (this.shipCargoDataGridView.SelectedRows == null || this.shipCargoDataGridView.SelectedRows.Count < 1)
        return 0;
      DataGridViewRow selectedRow = this.shipCargoDataGridView.SelectedRows[0];
      string str;
      double fromTradePartner = this.FindResourcePriceFromTradePartner(str = (string) selectedRow.Cells[0].Value);
      int val2 = (int) selectedRow.Cells[1].Value;
      int num = (int) this.nearestColony.BaconValues["marketcash"];
      if (this.tradePartner != null && this.IsTradePartnerAPirate() && this.tradePartner is BuiltObject && (this.tradePartner as BuiltObject).ActualEmpire == this.ship.ActualEmpire && (this.tradePartner as BuiltObject).Role == BuiltObjectRole.Base)
        num = (int) this.tradePartner.Empire.StateMoney;
      int val1 = (int) ((double) num / fromTradePartner);
      if (givingAwayForFree)
        val1 = 999999999;
      return Math.Min(Math.Min(val1, val2), this.tradePartner.CargoSpace);
    }

    public double FindResourcePriceFromTradePartner(string resourceName)
    {
      double fromTradePartner = -1.0;
      DataGridViewRow dataGridViewRow = (DataGridViewRow) null;
      DataGridViewRowCollection rows = this.tradePartnerCargoDataGridView.Rows;
      for (int index = 0; index < rows.Count; ++index)
      {
        if (rows[index].Cells[0].Value.ToString() == resourceName)
        {
          dataGridViewRow = rows[index];
          break;
        }
      }
      if (dataGridViewRow != null)
        fromTradePartner = (double) dataGridViewRow.Cells[3].Value;
      return fromTradePartner;
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

    public void LoadTradeHistory()
    {
      if (this.ship.BaconValues == null || !this.ship.BaconValues.ContainsKey("tradeHistory"))
        return;
      List<object[]> baconValue = (List<object[]>) this.ship.BaconValues["tradeHistory"];
      int index = 0;
      foreach (object[] objArray1 in baconValue)
      {
        object[] objArray2 = new object[5]
        {
          objArray1[0],
          objArray1[1],
          objArray1[2],
          objArray1[3],
          objArray1[4]
        };
        this.tradeHistoryDataGridView.Rows.Add(objArray2);
        this.tradeHistoryDataGridView.Rows[index].SetValues(objArray2);
        ++index;
      }
    }

    public void LogTransaction(
      string transactionType,
      string resourceName,
      int quantity,
      double price)
    {
      Main main = BaconBuiltObject.myMain;
      if (main == null)
        return;
      long currentStarDate = main._Game.Galaxy.CurrentStarDate;
      int num = Galaxy.RealSecondsInGalacticYear * 1000;
      string str = Galaxy.ResolveStarDateDescription(currentStarDate);
      int index = 0;
      if (this.tradeHistoryDataGridView.Rows != null)
        index = this.tradeHistoryDataGridView.Rows.Count;
      for (; index > 99; --index)
      {
        this.tradeHistoryDataGridView.Sort(this.tradeHistoryDataGridView.Columns[4], ListSortDirection.Descending);
        this.tradeHistoryDataGridView.Rows.RemoveAt(this.tradeHistoryDataGridView.RowCount - 1);
      }
      object[] objArray = new object[5]
      {
        (object) transactionType,
        (object) resourceName,
        (object) quantity,
        (object) price,
        (object) str
      };
      this.tradeHistoryDataGridView.Rows.Add(objArray);
      this.tradeHistoryDataGridView.Rows[index].SetValues(objArray);
    }

    private void buyFakePapersButton_Click(object sender, EventArgs e)
    {
      if (this.ship.Empire == BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire)
        return;
      this.ship.BaconValues["cash"] = (object) ((int) this.ship.BaconValues["cash"] - BaconMain.newIDCost);
      this.ship.Empire = BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire;
      this.buyFakePapersButton.Hide();
      this.totalCashLabel.Text = "Total cash reserves: " + this.ship.BaconValues["cash"]?.ToString();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.shipCargoDataGridView = new DataGridView();
      this.cargoNameColumn = new DataGridViewTextBoxColumn();
      this.cargoQuantityColumn = new DataGridViewTextBoxColumn();
      this.tradePartnerCargoDataGridView = new DataGridView();
      this.planetCargoNameColumn = new DataGridViewTextBoxColumn();
      this.planetQuantityColumn = new DataGridViewTextBoxColumn();
      this.planetSellPriceColumn = new DataGridViewTextBoxColumn();
      this.planetPurchasePriceColumn = new DataGridViewTextBoxColumn();
      this.sellCargoButton = new Button();
      this.sellAllButton = new GlassButton();
      this.sellQuantityTextBox = new TextBox();
      this.sellAmountLabel = new Label();
      this.buyAmountLabel = new Label();
      this.buyQuantityTextBox = new TextBox();
      this.buyCargoButton = new Button();
      this.shipNamelabel = new Label();
      this.planetNamelabel = new Label();
      this.remainingCargoSpacelabel = new Label();
      this.totalCashLabel = new Label();
      this.shipPictureBox = new PictureBox();
      this.tradePartnerPictureBox = new PictureBox();
      this.tradeHistoryDataGridView = new DataGridView();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.resourceColumn = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.marketCashLabel = new Label();
      this.tradePartnerRemainingCargoSpaceLabel = new Label();
      this.giveCargoButton = new Button();
      this.buyFakePapersButton = new Button();
      this.takeCargoButton = new Button();
      ((ISupportInitialize) this.shipCargoDataGridView).BeginInit();
      ((ISupportInitialize) this.tradePartnerCargoDataGridView).BeginInit();
      ((ISupportInitialize) this.shipPictureBox).BeginInit();
      ((ISupportInitialize) this.tradePartnerPictureBox).BeginInit();
      ((ISupportInitialize) this.tradeHistoryDataGridView).BeginInit();
      this.SuspendLayout();
      this.shipCargoDataGridView.AllowUserToAddRows = false;
      this.shipCargoDataGridView.AllowUserToDeleteRows = false;
      this.shipCargoDataGridView.AllowUserToResizeRows = false;
      this.shipCargoDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.shipCargoDataGridView.Columns.AddRange((DataGridViewColumn) this.cargoNameColumn, (DataGridViewColumn) this.cargoQuantityColumn);
      this.shipCargoDataGridView.Location = new Point(12, 76);
      this.shipCargoDataGridView.MultiSelect = false;
      this.shipCargoDataGridView.Name = "shipCargoDataGridView";
      this.shipCargoDataGridView.ReadOnly = true;
      this.shipCargoDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.shipCargoDataGridView.Size = new Size(343, 300);
      this.shipCargoDataGridView.StandardTab = true;
      this.shipCargoDataGridView.TabIndex = 1;
      this.shipCargoDataGridView.SelectionChanged += new EventHandler(this.shipCargoDataGridView_SelectionChanged);
      this.cargoNameColumn.HeaderText = "Cargo";
      this.cargoNameColumn.Name = "cargoNameColumn";
      this.cargoNameColumn.ReadOnly = true;
      this.cargoNameColumn.Width = 150;
      this.cargoQuantityColumn.HeaderText = "Quantity";
      this.cargoQuantityColumn.Name = "cargoQuantityColumn";
      this.cargoQuantityColumn.ReadOnly = true;
      this.cargoQuantityColumn.Width = 150;
      this.tradePartnerCargoDataGridView.AllowUserToAddRows = false;
      this.tradePartnerCargoDataGridView.AllowUserToDeleteRows = false;
      this.tradePartnerCargoDataGridView.AllowUserToResizeRows = false;
      this.tradePartnerCargoDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.tradePartnerCargoDataGridView.Columns.AddRange((DataGridViewColumn) this.planetCargoNameColumn, (DataGridViewColumn) this.planetQuantityColumn, (DataGridViewColumn) this.planetSellPriceColumn, (DataGridViewColumn) this.planetPurchasePriceColumn);
      this.tradePartnerCargoDataGridView.Location = new Point(618, 76);
      this.tradePartnerCargoDataGridView.MultiSelect = false;
      this.tradePartnerCargoDataGridView.Name = "tradePartnerCargoDataGridView";
      this.tradePartnerCargoDataGridView.ReadOnly = true;
      this.tradePartnerCargoDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.tradePartnerCargoDataGridView.Size = new Size(419, 300);
      this.tradePartnerCargoDataGridView.StandardTab = true;
      this.tradePartnerCargoDataGridView.TabIndex = 2;
      this.tradePartnerCargoDataGridView.SelectionChanged += new EventHandler(this.tradePartnerCargoDataGridView_SelectionChanged);
      this.planetCargoNameColumn.HeaderText = "Cargo";
      this.planetCargoNameColumn.Name = "planetCargoNameColumn";
      this.planetCargoNameColumn.ReadOnly = true;
      this.planetCargoNameColumn.Width = 150;
      this.planetQuantityColumn.FillWeight = 25f;
      this.planetQuantityColumn.HeaderText = "Quantity";
      this.planetQuantityColumn.Name = "planetQuantityColumn";
      this.planetQuantityColumn.ReadOnly = true;
      this.planetQuantityColumn.Width = 75;
      this.planetSellPriceColumn.FillWeight = 25f;
      this.planetSellPriceColumn.HeaderText = "Sell Price";
      this.planetSellPriceColumn.Name = "planetSellPriceColumn";
      this.planetSellPriceColumn.ReadOnly = true;
      this.planetSellPriceColumn.Width = 75;
      this.planetPurchasePriceColumn.FillWeight = 25f;
      this.planetPurchasePriceColumn.HeaderText = "Purchase Price";
      this.planetPurchasePriceColumn.Name = "planetPurchasePriceColumn";
      this.planetPurchasePriceColumn.ReadOnly = true;
      this.planetPurchasePriceColumn.Width = 75;
      this.sellCargoButton.Location = new Point(361, 244);
      this.sellCargoButton.Name = "sellCargoButton";
      this.sellCargoButton.Size = new Size(75, 23);
      this.sellCargoButton.TabIndex = 3;
      this.sellCargoButton.TabStop = false;
      this.sellCargoButton.Text = "Sell";
      this.sellCargoButton.UseVisualStyleBackColor = true;
      this.sellCargoButton.Click += new EventHandler(this.sellCargoButton_Click);
      this.sellAllButton.BackColor = Color.FromArgb(0, 0, 0);
      this.sellAllButton.ClipBackground = false;
      this.sellAllButton.DelayFrameRefresh = false;
      this.sellAllButton.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.sellAllButton.ForeColor = Color.FromArgb(150, 150, 150);
      this.sellAllButton.GlowColor = Color.FromArgb(48, 48, 128);
      this.sellAllButton.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.sellAllButton.IntensifyColors = false;
      this.sellAllButton.Location = new Point(361, 604);
      this.sellAllButton.Name = "sellAllButton";
      this.sellAllButton.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.sellAllButton.ShineColor = Color.FromArgb(112, 112, 128);
      this.sellAllButton.Size = new Size(75, 23);
      this.sellAllButton.TabIndex = 4;
      this.sellAllButton.TabStop = false;
      this.sellAllButton.Text = "Sell all";
      this.sellAllButton.TextColor = Color.FromArgb(120, 120, 120);
      this.sellAllButton.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.sellAllButton.ToggledOn = false;
      this.sellAllButton.Visible = false;
      this.sellQuantityTextBox.Location = new Point(361, 205);
      this.sellQuantityTextBox.Name = "sellQuantityTextBox";
      this.sellQuantityTextBox.Size = new Size(75, 20);
      this.sellQuantityTextBox.TabIndex = 5;
      this.sellQuantityTextBox.TabStop = false;
      this.sellAmountLabel.AutoSize = true;
      this.sellAmountLabel.Location = new Point(361, 179);
      this.sellAmountLabel.Name = "sellAmountLabel";
      this.sellAmountLabel.Size = new Size(63, 13);
      this.sellAmountLabel.TabIndex = 6;
      this.sellAmountLabel.Text = "Sell Amount";
      this.buyAmountLabel.AutoSize = true;
      this.buyAmountLabel.Location = new Point(534, 179);
      this.buyAmountLabel.Name = "buyAmountLabel";
      this.buyAmountLabel.Size = new Size(64, 13);
      this.buyAmountLabel.TabIndex = 10;
      this.buyAmountLabel.Text = "Buy Amount";
      this.buyQuantityTextBox.Location = new Point(537, 205);
      this.buyQuantityTextBox.Name = "buyQuantityTextBox";
      this.buyQuantityTextBox.Size = new Size(75, 20);
      this.buyQuantityTextBox.TabIndex = 9;
      this.buyQuantityTextBox.TabStop = false;
      this.buyCargoButton.Location = new Point(537, 244);
      this.buyCargoButton.Name = "buyCargoButton";
      this.buyCargoButton.Size = new Size(75, 23);
      this.buyCargoButton.TabIndex = 7;
      this.buyCargoButton.TabStop = false;
      this.buyCargoButton.Text = "Buy";
      this.buyCargoButton.UseVisualStyleBackColor = true;
      this.buyCargoButton.Click += new EventHandler(this.buyCargoButton_Click);
      this.shipNamelabel.Font = new Font("Microsoft Sans Serif", 27.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.shipNamelabel.Location = new Point(9, 24);
      this.shipNamelabel.Name = "shipNamelabel";
      this.shipNamelabel.Size = new Size(346, 42);
      this.shipNamelabel.TabIndex = 11;
      this.shipNamelabel.Text = "Ship name here";
      this.planetNamelabel.Font = new Font("Microsoft Sans Serif", 27.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.planetNamelabel.Location = new Point(611, 24);
      this.planetNamelabel.Name = "planetNamelabel";
      this.planetNamelabel.Size = new Size(426, 42);
      this.planetNamelabel.TabIndex = 12;
      this.planetNamelabel.Text = "Planet name here";
      this.remainingCargoSpacelabel.AutoSize = true;
      this.remainingCargoSpacelabel.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.remainingCargoSpacelabel.Location = new Point(13, 393);
      this.remainingCargoSpacelabel.Name = "remainingCargoSpacelabel";
      this.remainingCargoSpacelabel.Size = new Size(183, 20);
      this.remainingCargoSpacelabel.TabIndex = 13;
      this.remainingCargoSpacelabel.Text = "Remaining Cargo space:";
      this.totalCashLabel.AutoSize = true;
      this.totalCashLabel.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.totalCashLabel.Location = new Point(13, 428);
      this.totalCashLabel.Name = "totalCashLabel";
      this.totalCashLabel.Size = new Size(150, 20);
      this.totalCashLabel.TabIndex = 14;
      this.totalCashLabel.Text = "Total cash reserves:";
      this.shipPictureBox.Location = new Point(362, 12);
      this.shipPictureBox.Name = "shipPictureBox";
      this.shipPictureBox.Size = new Size(100, 100);
      this.shipPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.shipPictureBox.TabIndex = 15;
      this.shipPictureBox.TabStop = false;
      this.tradePartnerPictureBox.Location = new Point(512, 12);
      this.tradePartnerPictureBox.Name = "tradePartnerPictureBox";
      this.tradePartnerPictureBox.Size = new Size(100, 100);
      this.tradePartnerPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.tradePartnerPictureBox.TabIndex = 16;
      this.tradePartnerPictureBox.TabStop = false;
      this.tradeHistoryDataGridView.AllowUserToAddRows = false;
      this.tradeHistoryDataGridView.AllowUserToDeleteRows = false;
      this.tradeHistoryDataGridView.AllowUserToResizeRows = false;
      this.tradeHistoryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.tradeHistoryDataGridView.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.resourceColumn, (DataGridViewColumn) this.dataGridViewTextBoxColumn2, (DataGridViewColumn) this.dataGridViewTextBoxColumn3, (DataGridViewColumn) this.dataGridViewTextBoxColumn4);
      this.tradeHistoryDataGridView.Location = new Point(618, 382);
      this.tradeHistoryDataGridView.MultiSelect = false;
      this.tradeHistoryDataGridView.Name = "tradeHistoryDataGridView";
      this.tradeHistoryDataGridView.ReadOnly = true;
      this.tradeHistoryDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.tradeHistoryDataGridView.Size = new Size(419, 257);
      this.tradeHistoryDataGridView.StandardTab = true;
      this.tradeHistoryDataGridView.TabIndex = 17;
      this.tradeHistoryDataGridView.TabStop = false;
      this.dataGridViewTextBoxColumn1.FillWeight = 50f;
      this.dataGridViewTextBoxColumn1.HeaderText = "Type";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Width = 75;
      this.resourceColumn.HeaderText = "Resource";
      this.resourceColumn.Name = "resourceColumn";
      this.resourceColumn.ReadOnly = true;
      this.resourceColumn.Width = 75;
      this.dataGridViewTextBoxColumn2.FillWeight = 25f;
      this.dataGridViewTextBoxColumn2.HeaderText = "Quantity";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.Width = 75;
      this.dataGridViewTextBoxColumn3.FillWeight = 25f;
      this.dataGridViewTextBoxColumn3.HeaderText = "Price";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.Width = 75;
      this.dataGridViewTextBoxColumn4.FillWeight = 25f;
      this.dataGridViewTextBoxColumn4.HeaderText = "Date";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      this.dataGridViewTextBoxColumn4.Width = 76;
      this.marketCashLabel.AutoSize = true;
      this.marketCashLabel.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.marketCashLabel.Location = new Point(13, 468);
      this.marketCashLabel.Name = "marketCashLabel";
      this.marketCashLabel.Size = new Size(119, 20);
      this.marketCashLabel.TabIndex = 18;
      this.marketCashLabel.Text = "Market Liquidity";
      this.tradePartnerRemainingCargoSpaceLabel.AutoSize = true;
      this.tradePartnerRemainingCargoSpaceLabel.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.tradePartnerRemainingCargoSpaceLabel.Location = new Point(13, 509);
      this.tradePartnerRemainingCargoSpaceLabel.Name = "tradePartnerRemainingCargoSpaceLabel";
      this.tradePartnerRemainingCargoSpaceLabel.Size = new Size(197, 20);
      this.tradePartnerRemainingCargoSpaceLabel.TabIndex = 19;
      this.tradePartnerRemainingCargoSpaceLabel.Text = "Trade Partner cargo space";
      this.giveCargoButton.Location = new Point(361, 285);
      this.giveCargoButton.Name = "giveCargoButton";
      this.giveCargoButton.Size = new Size(75, 23);
      this.giveCargoButton.TabIndex = 20;
      this.giveCargoButton.TabStop = false;
      this.giveCargoButton.Text = "Give";
      this.giveCargoButton.UseVisualStyleBackColor = true;
      this.giveCargoButton.Click += new EventHandler(this.giveCargoButton_Click);
      this.buyFakePapersButton.Location = new Point(361, 327);
      this.buyFakePapersButton.Name = "buyFakePapersButton";
      this.buyFakePapersButton.Size = new Size(75, 23);
      this.buyFakePapersButton.TabIndex = 21;
      this.buyFakePapersButton.TabStop = false;
      this.buyFakePapersButton.Text = "Buy new ID";
      this.buyFakePapersButton.UseVisualStyleBackColor = true;
      this.buyFakePapersButton.Click += new EventHandler(this.buyFakePapersButton_Click);
      this.takeCargoButton.Location = new Point(537, 285);
      this.takeCargoButton.Name = "takeCargoButton";
      this.takeCargoButton.Size = new Size(75, 23);
      this.takeCargoButton.TabIndex = 22;
      this.takeCargoButton.TabStop = false;
      this.takeCargoButton.Text = "Take";
      this.takeCargoButton.UseVisualStyleBackColor = true;
      this.takeCargoButton.Click += new EventHandler(this.takeCargoButton_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1049, 639);
      this.Controls.Add((Control) this.takeCargoButton);
      this.Controls.Add((Control) this.buyFakePapersButton);
      this.Controls.Add((Control) this.giveCargoButton);
      this.Controls.Add((Control) this.tradePartnerRemainingCargoSpaceLabel);
      this.Controls.Add((Control) this.marketCashLabel);
      this.Controls.Add((Control) this.tradeHistoryDataGridView);
      this.Controls.Add((Control) this.tradePartnerPictureBox);
      this.Controls.Add((Control) this.shipPictureBox);
      this.Controls.Add((Control) this.totalCashLabel);
      this.Controls.Add((Control) this.remainingCargoSpacelabel);
      this.Controls.Add((Control) this.planetNamelabel);
      this.Controls.Add((Control) this.shipNamelabel);
      this.Controls.Add((Control) this.buyAmountLabel);
      this.Controls.Add((Control) this.buyQuantityTextBox);
      this.Controls.Add((Control) this.buyCargoButton);
      this.Controls.Add((Control) this.sellAmountLabel);
      this.Controls.Add((Control) this.sellQuantityTextBox);
      this.Controls.Add((Control) this.sellAllButton);
      this.Controls.Add((Control) this.sellCargoButton);
      this.Controls.Add((Control) this.tradePartnerCargoDataGridView);
      this.Controls.Add((Control) this.shipCargoDataGridView);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (planetCargoDataForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Cargo Buy/Sell";
      this.FormClosed += new FormClosedEventHandler(this.cargoTransferForm_FormClosed);
      ((ISupportInitialize) this.shipCargoDataGridView).EndInit();
      ((ISupportInitialize) this.tradePartnerCargoDataGridView).EndInit();
      ((ISupportInitialize) this.shipPictureBox).EndInit();
      ((ISupportInitialize) this.tradePartnerPictureBox).EndInit();
      ((ISupportInitialize) this.tradeHistoryDataGridView).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
