// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.TradeRestrictedResourcesPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class TradeRestrictedResourcesPanel : UserControl
  {
    private bool _ShouldBeVisible;
    private bool _ShowingLabel;
    private bool _ShowingCheckbox;
    private Galaxy _Galaxy;
    private Empire _Empire;
    private bool _EnableCheckboxChanges;
    private IContainer components;
    private CheckBox chkTradeResources;
    private Label lblTradeResources;

    public bool ShouldBeVisible => this._ShouldBeVisible;

    public TradeRestrictedResourcesPanel() => this.InitializeComponent();

    public void ClearSettings()
    {
      this._Galaxy = (Galaxy) null;
      this._Empire = (Empire) null;
    }

    public void BindSettings(Galaxy galaxy, Empire empire)
    {
      this._EnableCheckboxChanges = false;
      this._Galaxy = galaxy;
      this._Empire = empire;
      this._ShouldBeVisible = false;
      this.Visible = true;
      ResourceList resourcesEmpireSupplies1 = empire.DetermineResourcesEmpireSupplies();
      string str1 = string.Empty;
      bool flag1 = false;
      for (int index = 0; index < galaxy.ResourceSystem.SuperLuxuryResources.Count; ++index)
      {
        ResourceDefinition superLuxuryResource = galaxy.ResourceSystem.SuperLuxuryResources[index];
        if (superLuxuryResource != null && resourcesEmpireSupplies1.Contains(new Resource(superLuxuryResource.ResourceID)))
        {
          str1 = str1 + superLuxuryResource.Name + ", ";
          flag1 = true;
        }
      }
      if (str1.Length >= 2)
        str1 = str1.Substring(0, str1.Length - 2);
      if (flag1)
      {
        this._ShouldBeVisible = true;
        this._ShowingLabel = true;
        DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(galaxy.PlayerEmpire);
        string str2 = string.Format(TextResolver.GetText("Restricted Resource Trade Description"), (object) str1);
        if (!diplomaticRelation.SupplyRestrictedResources)
          str2 = string.Format(TextResolver.GetText("Restricted Resource Trade Refuse Description"), (object) str1);
        this.lblTradeResources.Text = str2;
        this.lblTradeResources.Visible = true;
      }
      else
      {
        this._ShowingLabel = false;
        this.lblTradeResources.Text = string.Empty;
        this.lblTradeResources.Visible = false;
      }
      ResourceList resourcesEmpireSupplies2 = galaxy.PlayerEmpire.DetermineResourcesEmpireSupplies();
      string str3 = string.Empty;
      bool flag2 = false;
      for (int index = 0; index < galaxy.ResourceSystem.SuperLuxuryResources.Count; ++index)
      {
        ResourceDefinition superLuxuryResource = galaxy.ResourceSystem.SuperLuxuryResources[index];
        if (superLuxuryResource != null && resourcesEmpireSupplies2.Contains(new Resource(superLuxuryResource.ResourceID)))
        {
          str3 = str3 + superLuxuryResource.Name + ", ";
          flag2 = true;
        }
      }
      if (str3.Length >= 2)
        str3 = str3.Substring(0, str3.Length - 2);
      if (flag2)
      {
        this._ShouldBeVisible = true;
        this._ShowingCheckbox = true;
        this.chkTradeResources.Visible = true;
        this.chkTradeResources.Text = string.Format(TextResolver.GetText("Restricted Resource Trade Us Description"), (object) str3);
        this.chkTradeResources.Checked = galaxy.PlayerEmpire.ObtainDiplomaticRelation(empire).SupplyRestrictedResources;
      }
      else
      {
        this._ShowingCheckbox = false;
        this.chkTradeResources.Visible = false;
        this.chkTradeResources.Checked = false;
      }
      this.DoLayout();
      this._EnableCheckboxChanges = true;
    }

    public void DoLayout()
    {
      int num1 = 18;
      int num2 = 1;
      int x = 1;
      bool flag = false;
      if (this._ShouldBeVisible)
      {
        if (this._ShowingLabel)
        {
          this.lblTradeResources.Location = new Point(x, num2);
          num2 += num1;
          flag = true;
          this.lblTradeResources.BringToFront();
        }
        if (this._ShowingCheckbox)
        {
          this.chkTradeResources.Location = new Point(x, num2);
          num2 += num1;
          flag = true;
          this.chkTradeResources.BringToFront();
        }
      }
      if (!flag)
        return;
      this.Size = new Size(this.Width, num2);
      this._ShouldBeVisible = true;
    }

    private void chkTradeResources_CheckedChanged(object sender, EventArgs e)
    {
      if (!this._EnableCheckboxChanges)
        return;
      this._Galaxy.PlayerEmpire.ObtainDiplomaticRelation(this._Empire).SupplyRestrictedResources = this.chkTradeResources.Checked;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.chkTradeResources = new CheckBox();
      this.lblTradeResources = new Label();
      this.SuspendLayout();
      this.chkTradeResources.AutoSize = true;
      this.chkTradeResources.Location = new Point(3, 22);
      this.chkTradeResources.Name = "chkTradeResources";
      this.chkTradeResources.Size = new Size(197, 17);
      this.chkTradeResources.TabIndex = 0;
      this.chkTradeResources.Text = "Trade restricted resources with them";
      this.chkTradeResources.UseVisualStyleBackColor = true;
      this.chkTradeResources.CheckedChanged += new EventHandler(this.chkTradeResources_CheckedChanged);
      this.lblTradeResources.AutoSize = true;
      this.lblTradeResources.Location = new Point(3, 3);
      this.lblTradeResources.Name = "lblTradeResources";
      this.lblTradeResources.Size = new Size(67, 13);
      this.lblTradeResources.TabIndex = 1;
      this.lblTradeResources.Text = "They have...";
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.lblTradeResources);
      this.Controls.Add((Control) this.chkTradeResources);
      this.Name = nameof (TradeRestrictedResourcesPanel);
      this.Size = new Size(301, 41);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
