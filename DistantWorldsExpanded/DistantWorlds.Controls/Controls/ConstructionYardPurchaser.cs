// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ConstructionYardPurchaser
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ConstructionYardPurchaser : GradientPanel
  {
    private Font _BoldFont;
    private Font _TitleFont;
    private Galaxy _Galaxy;
    private ConstructionQueue _ConstructionQueue;
    private bool _StateConstructionOnly = true;
    private Habitat _Colony;
    private Empire _Empire;
    private DesignList _Designs;
    private IContainer components;
    private ComboBox cmbDesigns;
    private GlassButton btnPurchase;
    private Label lblStateMoney;
    private Label lblStateMoneyLabel;

    public event EventHandler PurchaseMade;

    public ConstructionYardPurchaser()
    {
      this.InitializeComponent();
      this.Font = new Font("Verdana", 8f);
      this.SetFont(15.33f);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 3f, FontStyle.Bold);
      this.BackColor = Color.FromArgb(39, 40, 44);
      this.BackColor2 = Color.FromArgb(22, 21, 26);
      this.BackColor3 = Color.FromArgb(51, 54, 61);
      this.BorderColor = Color.FromArgb(67, 67, 77);
      this.BorderWidth = 2;
      this.BorderStyle = BorderStyle.FixedSingle;
      this.Curvature = 20;
      this.CurveMode = CornerCurveMode.All;
      this.GradientMode = LinearGradientMode.Vertical;
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._ConstructionQueue = (ConstructionQueue) null;
      this._Designs = (DesignList) null;
    }

    public void BindData(
      Empire empire,
      ConstructionQueue constructionQueue,
      Habitat colony,
      Galaxy galaxy,
      bool allowPrivateConstruction)
    {
      this._Galaxy = galaxy;
      this._ConstructionQueue = constructionQueue;
      this._Colony = colony;
      this._Empire = empire;
      this._StateConstructionOnly = !allowPrivateConstruction;
      this._Designs = new DesignList();
      this.DoLayout();
      this.PopulateDesigns();
      if (this._ConstructionQueue == null || this._Empire == null)
        return;
      this.lblStateMoney.Text = string.Format(TextResolver.GetText("X credits"), (object) this._Empire.StateMoney.ToString("#########0"));
    }

    private void LocalizeControlText() => this.btnPurchase.Text = TextResolver.GetText("Purchase");

    private void DoLayout()
    {
      this.SuspendLayout();
      this.LocalizeControlText();
      this.lblStateMoneyLabel.Text = TextResolver.GetText("Available Funds");
      this.lblStateMoneyLabel.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblStateMoneyLabel.Location = new Point(10, 8);
      this.lblStateMoney.Location = new Point(105, 8);
      this.lblStateMoney.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbDesigns.BringToFront();
      this.cmbDesigns.Visible = true;
      this.cmbDesigns.Size = new Size(this.Width - 20, 21);
      this.cmbDesigns.Location = new Point(10, 27);
      this.btnPurchase.Size = new Size(this.Width - 20, 25);
      this.btnPurchase.Location = new Point(10, 56);
      this.ResumeLayout();
    }

    private void PopulateDesigns()
    {
      if (this._ConstructionQueue != null && this._Empire != null && (this._ConstructionQueue.ParentBuiltObject != null && this._ConstructionQueue.ParentBuiltObject.IsShipYard || this._ConstructionQueue.ParentHabitat != null))
      {
        this._Designs = this._Empire.Designs.GetCurrentDesigns();
        DesignList designList = new DesignList();
        foreach (Design design in (SyncList<Design>) this._Designs)
        {
          if (!this._Empire.CanBuildBuiltObject(new BuiltObject(design, "", this._Galaxy), this._Colony))
            designList.Add(design);
          else if (!this._Empire.CheckDesignComponentsResearched(design))
            designList.Add(design);
          else if (design.IsPlanetDestroyer)
          {
            designList.Add(design);
          }
          else
          {
            if (this._StateConstructionOnly)
            {
              switch (design.SubRole)
              {
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                case BuiltObjectSubRole.GasMiningStation:
                case BuiltObjectSubRole.MiningStation:
                  designList.Add(design);
                  continue;
              }
            }
            if (this._ConstructionQueue.ParentHabitat != null && this._Galaxy.DetermineSpacePortAtColonyIncludingUnderConstruction(this._ConstructionQueue.ParentHabitat) != null)
            {
              switch (design.SubRole)
              {
                case BuiltObjectSubRole.Outpost:
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                  designList.Add(design);
                  continue;
              }
            }
            if (this._ConstructionQueue.ParentBuiltObject != null && design.Role == BuiltObjectRole.Base)
              designList.Add(design);
          }
        }
        foreach (Design design in (SyncList<Design>) designList)
          this._Designs.Remove(design);
        List<string> stringList = new List<string>();
        foreach (Design design in (SyncList<Design>) this._Designs)
        {
          string str1 = Galaxy.ResolveDescription(design.SubRole) + ": " + design.Name + " (";
          double currentPurchasePrice = design.CalculateCurrentPurchasePrice(this._Galaxy);
          string str2 = str1 + string.Format(TextResolver.GetText("X credits"), (object) currentPurchasePrice.ToString("######0")) + ")";
          stringList.Add(str2);
        }
        this.cmbDesigns.Items.Clear();
        this.cmbDesigns.MaxDropDownItems = 15;
        this.cmbDesigns.Items.AddRange((object[]) stringList.ToArray());
        if (this.cmbDesigns.Items.Count <= 0)
          return;
        this.cmbDesigns.SelectedIndex = 0;
      }
      else
        this.cmbDesigns.Items.Clear();
    }

    private void FlashAvailableFunds() => this.lblStateMoney.BackColor = Color.Red;

    private void ClearAvailableFunds() => this.lblStateMoney.BackColor = Color.Transparent;

    private void btnPurchase_Click(object sender, EventArgs e)
    {
      Design design = (Design) null;
      int selectedIndex = this.cmbDesigns.SelectedIndex;
      if (selectedIndex >= 0)
        design = this._Designs[selectedIndex];
      if (design == null || this._Empire == null)
        return;
      if (design.SubRole == BuiltObjectSubRole.ColonyShip)
      {
        if (this._Empire.ControlColonization == AutomationLevel.FullyAutomated)
        {
          MessageBoxEx messageBox = MessageBoxExManager.GetMessageBox(TextResolver.GetText("Colonization"));
          if (messageBox != null && messageBox.Show().ToLower(CultureInfo.InvariantCulture) == "off")
            this._Empire.ControlColonization = AutomationLevel.Manual;
        }
      }
      else if (this._Empire.ControlStateConstruction == AutomationLevel.FullyAutomated)
      {
        MessageBoxEx messageBox = MessageBoxExManager.GetMessageBox(TextResolver.GetText("Ship Building"));
        if (messageBox != null && messageBox.Show().ToLower(CultureInfo.InvariantCulture) == "off")
          this._Empire.ControlStateConstruction = AutomationLevel.Manual;
      }
      if (design.CalculateCurrentPurchasePrice(this._Galaxy) <= this._Empire.StateMoney)
      {
        bool isAutoControlled = this._Empire.NewBuiltObjectShouldBeAutomated(design.SubRole);
        bool flag = false;
        bool builtObjectIsState = this._Galaxy.DetermineBuiltObjectIsState(design.SubRole);
        if (this._ConstructionQueue.ParentBuiltObject != null)
        {
          if (this._Empire.PurchaseNewBuiltObject(design, this._ConstructionQueue.ParentBuiltObject, builtObjectIsState, isAutoControlled) != null)
            flag = true;
        }
        else if (this._ConstructionQueue.ParentHabitat != null && this._Empire.PurchaseNewBuiltObject(design, this._ConstructionQueue.ParentHabitat, builtObjectIsState, isAutoControlled) != null)
          flag = true;
        if (flag && this.PurchaseMade != null)
          this.PurchaseMade((object) this, new EventArgs());
        this.ClearAvailableFunds();
        this.PopulateDesigns();
        if (this.cmbDesigns.Items.Count > selectedIndex)
          this.cmbDesigns.SelectedIndex = selectedIndex;
        else
          this.cmbDesigns.SelectedIndex = this.cmbDesigns.Items.Count - 1;
        this.lblStateMoney.Text = string.Format(TextResolver.GetText("X credits"), (object) this._Empire.StateMoney.ToString("#########0"));
      }
      else
        this.FlashAvailableFunds();
    }

    private void cmbDesigns_SelectedIndexChanged(object sender, EventArgs e) => this.ClearAvailableFunds();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.cmbDesigns = new ComboBox();
      this.btnPurchase = new GlassButton();
      this.lblStateMoney = new Label();
      this.lblStateMoneyLabel = new Label();
      this.SuspendLayout();
      this.cmbDesigns.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbDesigns.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbDesigns.FlatStyle = FlatStyle.Popup;
      this.cmbDesigns.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cmbDesigns.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbDesigns.FormattingEnabled = true;
      this.cmbDesigns.Location = new Point(23, 36);
      this.cmbDesigns.Name = "cmbDesigns";
      this.cmbDesigns.Size = new Size(150, 21);
      this.cmbDesigns.TabIndex = 52;
      this.cmbDesigns.Visible = false;
      this.cmbDesigns.SelectedIndexChanged += new EventHandler(this.cmbDesigns_SelectedIndexChanged);
      this.btnPurchase.BackColor = Color.FromArgb(0, 0, 72);
      this.btnPurchase.FlatAppearance.BorderColor = Color.Silver;
      this.btnPurchase.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 0, (int) byte.MaxValue);
      this.btnPurchase.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 224);
      this.btnPurchase.FlatStyle = FlatStyle.Flat;
      this.btnPurchase.Font = new Font("Verdana", 8f);
      this.btnPurchase.ForeColor = Color.FromArgb(170, 170, 170);
      this.btnPurchase.Location = new Point(23, 63);
      this.btnPurchase.Name = "btnPurchase";
      this.btnPurchase.Size = new Size(150, 25);
      this.btnPurchase.TabIndex = 53;
      this.btnPurchase.Text = "Purchase";
      this.btnPurchase.UseVisualStyleBackColor = true;
      this.btnPurchase.Click += new EventHandler(this.btnPurchase_Click);
      this.lblStateMoney.AutoSize = true;
      this.lblStateMoney.BackColor = Color.Transparent;
      this.lblStateMoney.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblStateMoney.ForeColor = Color.White;
      this.lblStateMoney.Location = new Point(24, 16);
      this.lblStateMoney.Name = "lblStateMoney";
      this.lblStateMoney.Size = new Size(49, 13);
      this.lblStateMoney.TabIndex = 54;
      this.lblStateMoney.Text = "Money";
      this.lblStateMoneyLabel.AutoSize = true;
      this.lblStateMoneyLabel.BackColor = Color.Transparent;
      this.lblStateMoneyLabel.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblStateMoneyLabel.ForeColor = Color.White;
      this.lblStateMoneyLabel.Location = new Point(24, 16);
      this.lblStateMoneyLabel.Name = "lblStateMoneyLabel";
      this.lblStateMoneyLabel.Size = new Size(96, 13);
      this.lblStateMoneyLabel.TabIndex = 54;
      this.lblStateMoneyLabel.Text = "Available Funds";
      this.Controls.Add((Control) this.lblStateMoneyLabel);
      this.Controls.Add((Control) this.lblStateMoney);
      this.Controls.Add((Control) this.btnPurchase);
      this.Controls.Add((Control) this.cmbDesigns);
      this.Size = new Size(278, 178);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
