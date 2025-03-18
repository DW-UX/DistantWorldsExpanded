// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.FleetHabitatDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class FleetHabitatDropDown : ComboBox
  {
    private ShipGroupList _Fleets;
    private HabitatList _Habitats;
    private Galaxy _Galaxy;
    private bool _NullFleetSelection;
    private Bitmap[] _BuiltObjectImages;
    private Bitmap[] _HabitatImages;

    public FleetHabitatDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData()
    {
      this._Fleets = (ShipGroupList) null;
      this._Habitats = (HabitatList) null;
      this._Galaxy = (Galaxy) null;
    }

    private void ClearImages()
    {
      if (this._BuiltObjectImages != null && this._BuiltObjectImages.Length > 0)
      {
        for (int index = 0; index < this._BuiltObjectImages.Length; ++index)
        {
          if (this._BuiltObjectImages[index] != null && this._BuiltObjectImages[index].PixelFormat != PixelFormat.Undefined)
          {
            this._BuiltObjectImages[index].Dispose();
            this._BuiltObjectImages[index] = (Bitmap) null;
          }
        }
      }
      if (this._HabitatImages == null || this._HabitatImages.Length <= 0)
        return;
      for (int index = 0; index < this._HabitatImages.Length; ++index)
      {
        if (this._HabitatImages[index] != null && this._HabitatImages[index].PixelFormat != PixelFormat.Undefined)
        {
          this._HabitatImages[index].Dispose();
          this._HabitatImages[index] = (Bitmap) null;
        }
      }
    }

    public void InitializeImages(Bitmap[] builtObjectImages, Bitmap[] habitatImages)
    {
      this.ClearImages();
      this._BuiltObjectImages = new Bitmap[builtObjectImages.Length];
      for (int index = 0; index < builtObjectImages.Length; ++index)
      {
        int height = 19;
        double num = (double) height / (double) builtObjectImages[index].Width;
        int width = (int) ((double) builtObjectImages[index].Height * num);
        this._BuiltObjectImages[index] = GraphicsHelper.ScaleImage(builtObjectImages[index], width, height, 1f);
        this._BuiltObjectImages[index].RotateFlip(RotateFlipType.Rotate270FlipNone);
      }
      this._HabitatImages = new Bitmap[habitatImages.Length];
      for (int index = 0; index < habitatImages.Length; ++index)
      {
        int height = 19;
        double num = (double) height / (double) habitatImages[index].Height;
        int width = (int) ((double) habitatImages[index].Width * num);
        this._HabitatImages[index] = GraphicsHelper.ScaleImage(habitatImages[index], width, height, 1f);
      }
    }

    public void BindData(
      ShipGroupList fleets,
      HabitatList habitats,
      bool provideNullSelection,
      Galaxy galaxy)
    {
      this._Fleets = fleets;
      this._Habitats = habitats;
      this._Galaxy = galaxy;
      this._NullFleetSelection = provideNullSelection;
      this.Items.Clear();
      if (this._NullFleetSelection)
        this.Items.Add((object) "");
      if (this._Fleets != null)
      {
        this._Fleets.Sort((x,y) => x.Name.CompareTo(y.Name));
        this.Items.AddRange((object[]) this._Fleets.ToArray());
      }
      if (this._Habitats == null)
        return;
      this._Habitats = this._Habitats.OrderByName();
      this.Items.AddRange((object[]) this._Habitats.ToArray());
    }

    public ShipGroup SelectedFleet
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        int num = 0;
        if (this._NullFleetSelection)
        {
          ++num;
          if (selectedIndex == 0)
            return (ShipGroup) null;
        }
        if (selectedIndex < 0)
          return (ShipGroup) null;
        if (this._Fleets == null)
          return (ShipGroup) null;
        int index = selectedIndex - num;
        return index >= 0 && index < this._Fleets.Count ? this._Fleets[index] : (ShipGroup) null;
      }
    }

    public Habitat SelectedHabitat
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (Habitat) null;
        int num = 0;
        if (this._NullFleetSelection)
        {
          ++num;
          if (selectedIndex == 0)
            return (Habitat) null;
        }
        if (this._Fleets != null)
          num += this._Fleets.Count;
        if (this._Habitats == null)
          return (Habitat) null;
        int index = selectedIndex - num;
        return index >= 0 && index < this._Habitats.Count ? this._Habitats[index] : (Habitat) null;
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 19;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      try
      {
        base.OnDrawItem(e);
        e.DrawBackground();
        PointF point = new PointF(60f, (float) (e.Bounds.Y + 1));
        Font font = this.Font;
        SolidBrush solidBrush = new SolidBrush(this.ForeColor);
        if (e.Index >= 0 && (this._Fleets != null || this._Habitats != null))
        {
          int num1 = 0;
          if (this._NullFleetSelection)
            ++num1;
          if (this._NullFleetSelection && e.Index == 0)
          {
            e.Graphics.DrawString("(" + TextResolver.GetText("None") + ")", font, (Brush) solidBrush, point);
          }
          else
          {
            int index = e.Index - num1;
            if (this._Fleets != null && index < this._Fleets.Count)
            {
              if (index >= 0 && index < this._Fleets.Count)
              {
                ShipGroup fleet = this._Fleets[index];
                if (fleet != null)
                {
                  e.Graphics.DrawString(fleet.Name, font, (Brush) solidBrush, point);
                  Rectangle rect = new Rectangle();
                  if (fleet.Empire != null && fleet.Empire != this._Galaxy.IndependentEmpire)
                  {
                    Bitmap largeFlagPicture = fleet.Empire.LargeFlagPicture;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    int height = e.Bounds.Height - 2;
                    double num2 = (double) height / (double) largeFlagPicture.Height;
                    int width = (int) ((double) largeFlagPicture.Width * num2);
                    rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
                    e.Graphics.DrawImage((Image) largeFlagPicture, rect);
                  }
                  BuiltObject leadShip = fleet.LeadShip;
                  if (leadShip != null && this._BuiltObjectImages != null && this._BuiltObjectImages.Length > leadShip.PictureRef)
                  {
                    Bitmap builtObjectImage = this._BuiltObjectImages[leadShip.PictureRef];
                    if (builtObjectImage != null)
                      e.Graphics.DrawImage((Image) builtObjectImage, new Point(38, e.Bounds.Y + 1));
                  }
                }
              }
            }
            else if (this._Habitats != null)
            {
              if (this._Fleets != null)
                index -= this._Fleets.Count;
              if (index >= 0 && index < this._Habitats.Count)
              {
                Habitat habitat = this._Habitats[index];
                if (habitat != null)
                {
                  e.Graphics.DrawString(habitat.Name, font, (Brush) solidBrush, point);
                  Rectangle rect = new Rectangle();
                  if (habitat.Empire != null && habitat.Empire != this._Galaxy.IndependentEmpire)
                  {
                    Bitmap largeFlagPicture = habitat.Empire.LargeFlagPicture;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    int height = e.Bounds.Height - 2;
                    double num3 = (double) height / (double) largeFlagPicture.Height;
                    int width = (int) ((double) largeFlagPicture.Width * num3);
                    rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
                    e.Graphics.DrawImage((Image) largeFlagPicture, rect);
                  }
                  Bitmap habitatImage = this._HabitatImages[(int) habitat.PictureRef];
                  e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                  e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                  int height1 = e.Bounds.Height - 2;
                  double num4 = (double) height1 / (double) habitatImage.Height;
                  int width1 = (int) ((double) habitatImage.Width * num4);
                  rect = new Rectangle(38, e.Bounds.Y + 1, width1, height1);
                  e.Graphics.DrawImage((Image) habitatImage, rect);
                }
              }
            }
          }
        }
        e.DrawFocusRectangle();
      }
      catch (Exception ex)
      {
      }
    }
  }
}
