// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ColorDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ColorDropDown : ComboBox
  {
    private List<Color> _Colors;
    private bool _AllowWhite;
    private bool _AllowBlack;

    public ColorDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void Ignite() => this.Ignite(false, false, false, Color.Empty);

    public void Ignite(
      bool allowWhite,
      bool allowBlack,
      bool useDarkerPalette,
      Color includeColor)
    {
      this._AllowWhite = allowWhite;
      this._AllowBlack = allowBlack;
      this.Items.Clear();
      this._Colors = this.ResolveColors(useDarkerPalette, includeColor);
      for (int index = 0; index < this._Colors.Count; ++index)
        this.Items.Add((object) this._Colors[index]);
      this.SelectedIndex = 0;
    }

    private List<Color> ResolveColors() => this.ResolveColors(false);

    private List<Color> ResolveColors(bool useDarkerPalette) => this.ResolveColors(useDarkerPalette, Color.Empty);

    private List<Color> ResolveColors(bool useDarkerPalette, Color includeColor)
    {
      List<Color> colors = new List<Color>();
      int num = 20;
      if (this._AllowWhite)
        num = 21;
      for (int key = 0; key < num; ++key)
      {
        Color color = Galaxy.SelectColorFromKey(key);
        if (useDarkerPalette)
          color = Galaxy.SelectColorFromKeyDark(key);
        colors.Add(color);
      }
      if (this._AllowBlack)
        colors.Add(Galaxy.SelectColorFromKey(23));
      if (!includeColor.IsEmpty && !GraphicsHelper.ContainsColor(colors, includeColor))
        colors.Add(includeColor);
      return colors;
    }

    public void SetSelectedColor(int colorIndex)
    {
      int num1 = -1;
      int num2 = 19;
      if (this._AllowWhite)
        num2 = 20;
      if (colorIndex >= 0 && colorIndex <= num2)
        num1 = colorIndex;
      else if (this._AllowBlack)
      {
        if (this._AllowWhite && colorIndex <= 21)
          num1 = colorIndex;
        else if (colorIndex <= 20)
          num1 = colorIndex;
      }
      else
        num1 = -1;
      this.SelectedIndex = num1;
    }

    public void SetSelectedColor(Color color) => this.SelectedIndex = GraphicsHelper.IndexOfColor(this._Colors, color);

    public Color SelectedColor
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex < 0 ? Color.FromArgb(0, 0, 0, 0) : this._Colors[selectedIndex];
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
      Font font = this.Font;
      SolidBrush solidBrush1 = new SolidBrush(this.ForeColor);
      if (this._Colors != null && e.Index >= 0 && e.Index < this._Colors.Count)
      {
        SolidBrush solidBrush2 = new SolidBrush(this._Colors[e.Index]);
        Rectangle rect = new Rectangle(1, e.Bounds.Y + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
        e.Graphics.FillRectangle((Brush) solidBrush2, rect);
      }
      e.DrawFocusRectangle();
    }
  }
}
