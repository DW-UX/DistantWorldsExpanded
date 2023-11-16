// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EventActionTypeDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class EventActionTypeDropDown : ComboBox
  {
    private List<EventActionType> _EventActionTypes;
    private bool _AllowNull;

    public EventActionTypeDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData() => this._EventActionTypes = (List<EventActionType>) null;

    public void BindData(List<EventActionType> eventActionTypes) => this.BindData(eventActionTypes, false);

    public void BindData(List<EventActionType> eventActionTypes, bool allowNull)
    {
      this._EventActionTypes = eventActionTypes;
      this._AllowNull = allowNull;
      this.Items.Clear();
      if (this._AllowNull)
        this.Items.Add(new object());
      if (this._EventActionTypes == null)
        return;
      this._EventActionTypes = this.SortItemsByName(this._EventActionTypes);
      for (int index = 0; index < this._EventActionTypes.Count; ++index)
        this.Items.Add((object) this._EventActionTypes[index]);
    }

    public List<EventActionType> SortItemsByName(List<EventActionType> items)
    {
      EventActionType[] array = items.ToArray();
      string[] keys = new string[array.Length];
      for (int index = 0; index < array.Length; ++index)
        keys[index] = Galaxy.ResolveDescription(array[index]);
      Array.Sort<string, EventActionType>(keys, array);
      List<EventActionType> eventActionTypeList = new List<EventActionType>();
      eventActionTypeList.AddRange((IEnumerable<EventActionType>) array);
      return eventActionTypeList;
    }

    public EventActionType SelectedEventActionType
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return EventActionType.Undefined;
        if (this._AllowNull)
        {
          if (selectedIndex == 0)
            return EventActionType.Undefined;
          --selectedIndex;
        }
        return this._EventActionTypes != null && selectedIndex >= 0 && selectedIndex < this._EventActionTypes.Count ? this._EventActionTypes[selectedIndex] : EventActionType.Undefined;
      }
    }

    public void SetSelectedEventActionType(EventActionType eventActionType)
    {
      int num = -1;
      if (this._EventActionTypes != null && eventActionType != EventActionType.Undefined)
        num = this._EventActionTypes.IndexOf(eventActionType);
      if (this._AllowNull)
      {
        ++num;
        if (eventActionType == EventActionType.Undefined)
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
      else if (this._EventActionTypes != null && this._EventActionTypes.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowNull)
          --index;
        string s = Galaxy.ResolveDescription(this._EventActionTypes[index]);
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
