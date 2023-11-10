// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GovernmentStyleDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class GovernmentStyleDropDown : ComboBox
  {
    private GovernmentAttributesList _GovernmentStyles;
    public bool AllowNullItem;
    public string NullItemText = "(Random)";

    public GovernmentStyleDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void Ignite()
    {
      this._GovernmentStyles = this.ResolveDefaultGovernmentStyles();
      this.Ignite(this._GovernmentStyles);
    }

    public void IgniteAll()
    {
      List<int> allowableGovernmentTypes = new List<int>();
      for (int index = 0; index < Galaxy.GovernmentsStatic.Count; ++index)
        allowableGovernmentTypes.Add(Galaxy.GovernmentsStatic[index].GovernmentId);
      this.Ignite(allowableGovernmentTypes);
    }

    public void Ignite(List<int> allowableGovernmentTypes)
    {
      this._GovernmentStyles = Galaxy.GovernmentsStatic.GetByIds(allowableGovernmentTypes);
      this.Ignite(this._GovernmentStyles);
    }

    public void Ignite(GovernmentAttributesList allowableGovernmentTypes)
    {
      this._GovernmentStyles = allowableGovernmentTypes;
      this.Items.Clear();
      if (this.AllowNullItem)
        this.Items.Add((object) this.NullItemText);
      for (int index = 0; index < this._GovernmentStyles.Count; ++index)
        this.Items.Add((object) this._GovernmentStyles[index]);
    }

    private GovernmentAttributesList ResolveDefaultGovernmentStyles() => Galaxy.GovernmentsStatic.GetByAvailability(1);

    public void SetSelectedGovernmentStyle(int governmentId)
    {
      int num = -1;
      if (this._GovernmentStyles != null)
      {
        for (int index = 0; index < this._GovernmentStyles.Count; ++index)
        {
          if (this._GovernmentStyles[index].GovernmentId == governmentId)
          {
            num = index;
            break;
          }
        }
        if (this.AllowNullItem)
        {
          if (num >= 0)
            ++num;
          else if (governmentId == -1)
            num = 0;
        }
      }
      this.SelectedIndex = num;
    }

    public int SelectedGovernmentId
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return this.AllowNullItem ? (selectedIndex >= 0 && selectedIndex != 0 ? this._GovernmentStyles[selectedIndex - 1].GovernmentId : -1) : (selectedIndex >= 0 ? this._GovernmentStyles[selectedIndex].GovernmentId : -1);
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 19;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(0.0f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      int num = 0;
      if (this._GovernmentStyles != null)
        num = this._GovernmentStyles.Count;
      if (this.AllowNullItem)
        ++num;
      if (this._GovernmentStyles != null && e.Index >= 0 && e.Index < num)
      {
        if (this.AllowNullItem)
        {
          if (e.Index == 0)
            e.Graphics.DrawString(this.NullItemText, font, (Brush) solidBrush, point);
          else
            e.Graphics.DrawString(this._GovernmentStyles[e.Index - 1].Name, font, (Brush) solidBrush, point);
        }
        else
          e.Graphics.DrawString(this._GovernmentStyles[e.Index].Name, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
