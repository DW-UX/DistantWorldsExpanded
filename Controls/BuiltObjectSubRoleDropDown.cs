// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.BuiltObjectSubRoleDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class BuiltObjectSubRoleDropDown : ComboBox
  {
    private List<BuiltObjectSubRole> _SubRoles;
    private bool _AllowNullRole;

    public BuiltObjectSubRoleDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData() => this._SubRoles = (List<BuiltObjectSubRole>) null;

    public void BindData(List<BuiltObjectSubRole> subRoles) => this.BindData(subRoles, false);

    public void BindData(List<BuiltObjectSubRole> subRoles, bool allowNullRole)
    {
      this._SubRoles = subRoles;
      this._AllowNullRole = allowNullRole;
      this.Items.Clear();
      if (this._AllowNullRole)
        this.Items.Add(new object());
      if (this._SubRoles == null)
        return;
      this._SubRoles.Sort();
      for (int index = 0; index < this._SubRoles.Count; ++index)
        this.Items.Add((object) this._SubRoles[index]);
    }

    public BuiltObjectSubRole SelectedBuiltObjectSubRole
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return BuiltObjectSubRole.Undefined;
        if (this._AllowNullRole)
        {
          if (selectedIndex == 0)
            return BuiltObjectSubRole.Undefined;
          --selectedIndex;
        }
        return this._SubRoles != null && selectedIndex >= 0 && selectedIndex < this._SubRoles.Count ? this._SubRoles[selectedIndex] : BuiltObjectSubRole.Undefined;
      }
    }

    public void SetSelectedBuiltObjectSubRole(BuiltObjectSubRole subRole)
    {
      int num = -1;
      if (this._SubRoles != null && subRole != BuiltObjectSubRole.Undefined)
        num = this._SubRoles.IndexOf(subRole);
      if (this._AllowNullRole)
      {
        ++num;
        if (subRole == BuiltObjectSubRole.Undefined)
          num = 0;
      }
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
      PointF point = new PointF(0.0f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      string empty = string.Empty;
      if (this._AllowNullRole && e.Index == 0)
        e.Graphics.DrawString("(" + TextResolver.GetText("None") + ")", font, (Brush) solidBrush, point);
      else if (this._SubRoles != null && this._SubRoles.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowNullRole)
          --index;
        string s = Galaxy.ResolveDescription(this._SubRoles[index]);
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
