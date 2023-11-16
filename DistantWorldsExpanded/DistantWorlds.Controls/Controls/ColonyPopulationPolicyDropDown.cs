// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ColonyPopulationPolicyDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ColonyPopulationPolicyDropDown : ComboBox
  {
    private List<ColonyPopulationPolicy> _Policies;

    public ColonyPopulationPolicyDropDown()
    {
      this.GeneratePolicies();
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    private void GeneratePolicies()
    {
      this._Policies = new List<ColonyPopulationPolicy>();
      this._Policies.Add(ColonyPopulationPolicy.Assimilate);
      this._Policies.Add(ColonyPopulationPolicy.DoNotAccept);
      this._Policies.Add(ColonyPopulationPolicy.Resettle);
      this._Policies.Add(ColonyPopulationPolicy.Enslave);
      this._Policies.Add(ColonyPopulationPolicy.Exterminate);
    }

    public void BindData()
    {
      this.Items.Clear();
      for (int index = 0; index < this._Policies.Count; ++index)
        this.Items.Add((object) Galaxy.ResolveDescription(this._Policies[index]));
    }

    public ColonyPopulationPolicy SelectedPolicy
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex < 0 || this._Policies == null || selectedIndex < 0 || selectedIndex >= this._Policies.Count ? ColonyPopulationPolicy.Assimilate : this._Policies[selectedIndex];
      }
    }

    public void SetSelectedPolicy(ColonyPopulationPolicy policy)
    {
      int num = -1;
      if (this._Policies != null)
        num = this._Policies.IndexOf(policy);
      this.SelectedIndex = num;
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 21;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(1f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      string empty = string.Empty;
      if (this._Policies != null && this._Policies.Count > 0 && e.Index >= 0)
      {
        string s = Galaxy.ResolveDescription(this._Policies[e.Index]);
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
