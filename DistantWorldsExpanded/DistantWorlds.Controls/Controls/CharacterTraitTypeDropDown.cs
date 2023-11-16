// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterTraitTypeDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CharacterTraitTypeDropDown : ComboBox
  {
    private List<CharacterTraitType> _TraitTypes;
    private bool _AllowNullValue;

    public CharacterTraitTypeDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData() => this._TraitTypes = (List<CharacterTraitType>) null;

    public void BindData(CharacterRole characterRole, bool allowNullValue)
    {
      this._TraitTypes = Character.DetermineValidTraitsForRole(characterRole, true, true);
      this._AllowNullValue = allowNullValue;
      this.ItemHeight = 22;
      this.Items.Clear();
      if (this._AllowNullValue)
        this.Items.Add((object) "");
      if (this._TraitTypes != null)
      {
        for (int index = 0; index < this._TraitTypes.Count; ++index)
          this.Items.Add((object) this._TraitTypes[index]);
      }
      if (this._AllowNullValue)
        this.SelectedIndex = 0;
      else
        this.SelectedIndex = -1;
    }

    public void SetSelectedTrait(CharacterTraitType traitType)
    {
      if (this._TraitTypes == null)
      {
        if (this._AllowNullValue)
          this.SelectedIndex = 0;
        else
          this.SelectedIndex = -1;
      }
      else if (traitType != CharacterTraitType.Undefined)
      {
        int num = this._TraitTypes.IndexOf(traitType);
        if (this._AllowNullValue)
          ++num;
        this.SelectedIndex = num;
      }
      else if (this._AllowNullValue)
        this.SelectedIndex = 0;
      else
        this.SelectedIndex = -1;
    }

    public CharacterTraitType SelectedTrait
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        int num = 0;
        if (this._AllowNullValue)
        {
          ++num;
          if (selectedIndex == 0)
            return CharacterTraitType.Undefined;
        }
        if (selectedIndex < 0 || this._TraitTypes == null)
          return CharacterTraitType.Undefined;
        int index = selectedIndex - num;
        return index >= 0 && this._TraitTypes.Count > index ? this._TraitTypes[index] : CharacterTraitType.Undefined;
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e) => e.ItemHeight = 22;

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(2f, (float) (e.Bounds.Y + 3));
      Font font = this.Font;
      SolidBrush solidBrush1 = new SolidBrush(this.ForeColor);
      SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(128, (int) byte.MaxValue, 0, 128));
      int num = 0;
      if (this._AllowNullValue)
        ++num;
      if (this._AllowNullValue && e.Index == 0)
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        string s = "(" + TextResolver.GetText("None") + ")";
        e.Graphics.DrawString(s, font, (Brush) solidBrush1, point);
      }
      else if (this._TraitTypes != null && this._TraitTypes.Count > 0 && e.Index >= num)
      {
        CharacterTraitType traitType = this._TraitTypes[e.Index - num];
        if (traitType != CharacterTraitType.Undefined)
        {
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          string s = Galaxy.ResolveDescription(traitType);
          e.Graphics.DrawString(s, font, (Brush) solidBrush1, point);
        }
      }
      e.DrawFocusRectangle();
    }
  }
}
