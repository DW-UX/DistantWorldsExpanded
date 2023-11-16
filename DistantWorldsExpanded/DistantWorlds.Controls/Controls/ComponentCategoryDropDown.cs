// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ComponentCategoryDropDown
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
  public class ComponentCategoryDropDown : ComboBox
  {
    private List<ComponentCategoryType> _Categories;
    private Bitmap[] _ComponentImages;

    public ComponentCategoryDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    private List<ComponentCategoryType> ReorderSpecialCategories(
      List<ComponentCategoryType> categories)
    {
      int index1 = categories.IndexOf(ComponentCategoryType.WeaponSuperBeam);
      if (index1 >= 0)
      {
        categories.RemoveAt(index1);
        categories.Add(ComponentCategoryType.WeaponSuperBeam);
      }
      int index2 = categories.IndexOf(ComponentCategoryType.WeaponSuperArea);
      if (index2 >= 0)
      {
        categories.RemoveAt(index2);
        categories.Add(ComponentCategoryType.WeaponSuperArea);
      }
      return categories;
    }

    public void ClearData() => this._Categories = (List<ComponentCategoryType>) null;

    private List<ComponentCategoryType> ResolveComponentCategories() => new List<ComponentCategoryType>()
    {
      ComponentCategoryType.Armor,
      ComponentCategoryType.Computer,
      ComponentCategoryType.Construction,
      ComponentCategoryType.EnergyCollector,
      ComponentCategoryType.Engine,
      ComponentCategoryType.Extractor,
      ComponentCategoryType.Fighter,
      ComponentCategoryType.Habitation,
      ComponentCategoryType.HyperDrive,
      ComponentCategoryType.Labs,
      ComponentCategoryType.Manufacturer,
      ComponentCategoryType.Reactor,
      ComponentCategoryType.Sensor,
      ComponentCategoryType.Shields,
      ComponentCategoryType.Storage,
      ComponentCategoryType.WeaponArea,
      ComponentCategoryType.WeaponBeam,
      ComponentCategoryType.WeaponIon,
      ComponentCategoryType.WeaponPointDefense,
      ComponentCategoryType.WeaponTorpedo,
      ComponentCategoryType.WeaponSuperBeam,
      ComponentCategoryType.WeaponSuperArea
    };

    public void BindData(Bitmap[] componentImages)
    {
      this._Categories = this.ResolveComponentCategories();
      this._ComponentImages = componentImages;
      this._Categories = this.ReorderSpecialCategories(this._Categories);
      this.ItemHeight = 22;
      this.Items.Clear();
      if (this._Categories == null)
        return;
      for (int index = 0; index < this._Categories.Count; ++index)
        this.Items.Add((object) this._Categories[index]);
    }

    public void SetSelectedCategory(ComponentCategoryType componentCategory)
    {
      if (this._Categories == null)
        this.SelectedIndex = -1;
      else if (componentCategory != ComponentCategoryType.Undefined)
        this.SelectedIndex = this._Categories.IndexOf(componentCategory);
      else
        this.SelectedIndex = -1;
    }

    public ComponentCategoryType SelectedCategory
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex < 0 || this._Categories == null || this._Categories.Count <= selectedIndex ? ComponentCategoryType.Undefined : this._Categories[selectedIndex];
      }
    }

    private Bitmap SelectComponentImage(ComponentCategoryType componentCategory)
    {
      Bitmap bitmap = (Bitmap) null;
      switch (componentCategory)
      {
        case ComponentCategoryType.WeaponBeam:
          bitmap = this._ComponentImages[0];
          break;
        case ComponentCategoryType.WeaponTorpedo:
          bitmap = this._ComponentImages[8];
          break;
        case ComponentCategoryType.WeaponArea:
          bitmap = this._ComponentImages[16];
          break;
        case ComponentCategoryType.Armor:
          bitmap = this._ComponentImages[24];
          break;
        case ComponentCategoryType.Shields:
          bitmap = this._ComponentImages[28];
          break;
        case ComponentCategoryType.Engine:
          bitmap = this._ComponentImages[40];
          break;
        case ComponentCategoryType.HyperDrive:
          bitmap = this._ComponentImages[52];
          break;
        case ComponentCategoryType.Reactor:
          bitmap = this._ComponentImages[60];
          break;
        case ComponentCategoryType.EnergyCollector:
          bitmap = this._ComponentImages[68];
          break;
        case ComponentCategoryType.Extractor:
          bitmap = this._ComponentImages[73];
          break;
        case ComponentCategoryType.Manufacturer:
          bitmap = this._ComponentImages[80];
          break;
        case ComponentCategoryType.Storage:
          bitmap = this._ComponentImages[95];
          break;
        case ComponentCategoryType.Sensor:
          bitmap = this._ComponentImages[107];
          break;
        case ComponentCategoryType.Computer:
          bitmap = this._ComponentImages[(int) sbyte.MaxValue];
          break;
        case ComponentCategoryType.Labs:
          bitmap = this._ComponentImages[136];
          break;
        case ComponentCategoryType.Construction:
          bitmap = this._ComponentImages[148];
          break;
        case ComponentCategoryType.Habitation:
          bitmap = this._ComponentImages[163];
          break;
        case ComponentCategoryType.WeaponSuperBeam:
          bitmap = this._ComponentImages[169];
          break;
        case ComponentCategoryType.WeaponSuperArea:
          bitmap = this._ComponentImages[170];
          break;
      }
      return bitmap;
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e) => e.ItemHeight = 22;

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      int width = 20;
      PointF point = new PointF(28f, (float) (e.Bounds.Y + 3));
      Font font = this.Font;
      SolidBrush solidBrush1 = new SolidBrush(this.ForeColor);
      SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(128, (int) byte.MaxValue, 0, 128));
      if (this._Categories != null && this._Categories.Count > 0 && e.Index >= 0)
      {
        ComponentCategoryType category = this._Categories[e.Index];
        if (category != ComponentCategoryType.Undefined)
        {
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          Bitmap bitmap = this.SelectComponentImage(category);
          int height = (int) ((double) bitmap.Height / ((double) bitmap.Width / (double) width));
          if (height > 20)
          {
            width = (int) ((double) width * (20.0 / (double) height));
            height = 20;
          }
          Rectangle rect = new Rectangle(e.Bounds.X + (20 - width) / 2, e.Bounds.Y + (20 - height) / 2, width, height);
          e.Graphics.DrawImage((Image) bitmap, rect);
          string s = Galaxy.ResolveDescription(category);
          e.Graphics.DrawString(s, font, (Brush) solidBrush1, point);
        }
      }
      e.DrawFocusRectangle();
    }
  }
}
