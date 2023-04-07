// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MultipleEventActionTypeDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class MultipleEventActionTypeDropDown : ComboBox
  {
    private List<MultipleEventActionType> _MultipleEventActionTypes;

    public MultipleEventActionTypeDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData() => this._MultipleEventActionTypes = (List<MultipleEventActionType>) null;

    public void BindData(
      List<MultipleEventActionType> multipleEventActionTypes)
    {
      this._MultipleEventActionTypes = multipleEventActionTypes;
      this.Items.Clear();
      if (this._MultipleEventActionTypes == null)
        return;
      for (int index = 0; index < this._MultipleEventActionTypes.Count; ++index)
        this.Items.Add((object) this._MultipleEventActionTypes[index]);
    }

    public MultipleEventActionType SelectedMultipleEventActionType
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex < 0 || this._MultipleEventActionTypes == null || selectedIndex < 0 || selectedIndex >= this._MultipleEventActionTypes.Count ? MultipleEventActionType.ExecuteAllActions : this._MultipleEventActionTypes[selectedIndex];
      }
    }

    public void SetSelectedMultipleEventActionType(MultipleEventActionType multipleEventActionType)
    {
      int num = -1;
      if (this._MultipleEventActionTypes != null)
        num = this._MultipleEventActionTypes.IndexOf(multipleEventActionType);
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
      if (this._MultipleEventActionTypes != null && this._MultipleEventActionTypes.Count > 0 && e.Index >= 0)
      {
        string s = Galaxy.ResolveDescription(this._MultipleEventActionTypes[e.Index]);
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
