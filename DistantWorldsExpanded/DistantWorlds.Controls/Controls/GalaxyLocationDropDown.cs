// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GalaxyLocationDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class GalaxyLocationDropDown : ComboBox
  {
    private GalaxyLocationList _Locations;
    private bool _AllowNullLocation;

    public GalaxyLocationDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData() => this._Locations = (GalaxyLocationList) null;

    public void BindData(GalaxyLocationList locations) => this.BindData(locations, false);

    public void BindData(GalaxyLocationList locations, bool allowNullLocation)
    {
      this._Locations = locations;
      this._AllowNullLocation = allowNullLocation;
      this.Items.Clear();
      if (this._AllowNullLocation)
        this.Items.Add(new object());
      if (this._Locations == null)
        return;
      this._Locations.Sort();
      this.Items.AddRange((object[]) this._Locations.ToArray());
    }

    public GalaxyLocation SelectedLocation
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (GalaxyLocation) null;
        if (this._AllowNullLocation)
        {
          if (selectedIndex == 0)
            return (GalaxyLocation) null;
          --selectedIndex;
        }
        return this._Locations != null && selectedIndex >= 0 && selectedIndex < this._Locations.Count ? this._Locations[selectedIndex] : (GalaxyLocation) null;
      }
    }

    public void SetSelectedLocation(GalaxyLocation location)
    {
      int num = -1;
      if (this._Locations != null && location != null)
        num = this._Locations.IndexOf(location);
      if (this._AllowNullLocation)
      {
        ++num;
        if (location == null)
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
      if (this._AllowNullLocation && e.Index == 0)
        e.Graphics.DrawString("(" + TextResolver.GetText("None") + ")", font, (Brush) solidBrush, point);
      else if (this._Locations != null && this._Locations.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowNullLocation)
          --index;
        string name = this._Locations[index].Name;
        e.Graphics.DrawString(name, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
