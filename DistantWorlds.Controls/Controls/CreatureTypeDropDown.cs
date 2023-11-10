// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CreatureTypeDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CreatureTypeDropDown : ComboBox
  {
    private List<CreatureType> _CreatureTypes;
    private bool _AllowNull;

    public CreatureTypeDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData() => this._CreatureTypes = (List<CreatureType>) null;

    public void BindData(List<CreatureType> creatureTypes) => this.BindData(creatureTypes, false);

    public void BindData(List<CreatureType> creatureTypes, bool allowNull)
    {
      this._CreatureTypes = creatureTypes;
      this._AllowNull = allowNull;
      this.Items.Clear();
      if (this._AllowNull)
        this.Items.Add(new object());
      if (this._CreatureTypes == null)
        return;
      this._CreatureTypes.Sort();
      for (int index = 0; index < this._CreatureTypes.Count; ++index)
        this.Items.Add((object) this._CreatureTypes[index]);
    }

    public CreatureType SelectedCreatureType
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return CreatureType.Undefined;
        if (this._AllowNull)
        {
          if (selectedIndex == 0)
            return CreatureType.Undefined;
          --selectedIndex;
        }
        return this._CreatureTypes != null && selectedIndex >= 0 && selectedIndex < this._CreatureTypes.Count ? this._CreatureTypes[selectedIndex] : CreatureType.Undefined;
      }
    }

    public void SetSelectedCreatureType(CreatureType creatureType)
    {
      int num = -1;
      if (this._CreatureTypes != null && creatureType != CreatureType.Undefined)
        num = this._CreatureTypes.IndexOf(creatureType);
      if (this._AllowNull)
      {
        ++num;
        if (creatureType == CreatureType.Undefined)
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
      if (this._AllowNull && e.Index == 0)
        e.Graphics.DrawString("(" + TextResolver.GetText("None") + ")", font, (Brush) solidBrush, point);
      else if (this._CreatureTypes != null && this._CreatureTypes.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowNull)
          --index;
        string s = Galaxy.ResolveDescription(this._CreatureTypes[index]);
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
