// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.PlanetaryFacilityDefinitionDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class PlanetaryFacilityDefinitionDropDown : ComboBox
  {
    private PlanetaryFacilityDefinitionList _Facilities;
    private Bitmap[] _FacilityImages;
    private Empire _Empire;
    private bool _AllowNull;

    public PlanetaryFacilityDefinitionDropDown()
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
      this._Empire = (Empire) null;
      this._Facilities = (PlanetaryFacilityDefinitionList) null;
      GraphicsHelper.DisposeImageArray(this._FacilityImages);
    }

    public void BindData(
      Empire empire,
      PlanetaryFacilityDefinitionList facilities,
      Bitmap[] facilityImages)
    {
      this.BindData(empire, facilities, facilityImages, false);
    }

    public void BindData(
      Empire empire,
      PlanetaryFacilityDefinitionList facilities,
      Bitmap[] facilityImages,
      bool allowNull)
    {
      this.ClearData();
      this._Empire = empire;
      this._Facilities = facilities;
      this._AllowNull = allowNull;
      this.Items.Clear();
      if (this._AllowNull)
        this.Items.Add(new object());
      if (this._Facilities != null)
        this.Items.AddRange((object[]) this._Facilities.ToArray());
      this._FacilityImages = new Bitmap[facilityImages.Length];
      for (int index = 0; index < facilityImages.Length; ++index)
      {
        int height = 19;
        double num = (double) height / (double) facilityImages[index].Height;
        int width = (int) ((double) facilityImages[index].Width * num);
        this._FacilityImages[index] = this.PrecacheScaledBitmap(facilityImages[index], width, height);
        this._FacilityImages[index].MakeTransparent(Color.Black);
      }
    }

    private Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, int width, int height)
    {
      if (width < 1)
        width = 1;
      if (height < 1)
        height = 1;
      Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.DrawImage((Image) unscaledBitmap, new Rectangle(0, 0, width, height));
      graphics.Dispose();
      return bitmap;
    }

    public PlanetaryFacilityDefinition SelectedPlanetaryFacility
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (PlanetaryFacilityDefinition) null;
        if (this._AllowNull)
        {
          if (selectedIndex == 0)
            return (PlanetaryFacilityDefinition) null;
          --selectedIndex;
        }
        return this._Facilities != null && selectedIndex >= 0 && selectedIndex < this._Facilities.Count ? this._Facilities[selectedIndex] : (PlanetaryFacilityDefinition) null;
      }
    }

    public void SetSelectedPlanetaryFacilityDefinition(PlanetaryFacilityDefinition facility)
    {
      int num = -1;
      if (this._Facilities != null && facility != null)
        num = this._Facilities.IndexById(facility.PlanetaryFacilityDefinitionId);
      if (this._AllowNull)
      {
        ++num;
        if (facility == null)
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
      PointF point = new PointF(23f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      string empty = string.Empty;
      if (this._AllowNull && e.Index == 0)
        e.Graphics.DrawString("(" + TextResolver.GetText("None") + ")", font, (Brush) solidBrush, point);
      else if (this._Facilities != null && this._Facilities.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowNull)
          --index;
        double planetaryFacilityCost = Galaxy.CalculatePlanetaryFacilityCost(this._Facilities[index], this._Empire);
        string s = this._Facilities[index].Name + " (" + planetaryFacilityCost.ToString("#,###,##0") + " " + TextResolver.GetText("credits") + ")";
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
        Rectangle rect = new Rectangle();
        Bitmap facilityImage = this._FacilityImages[(int) this._Facilities[index].PictureRef];
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        int height = e.Bounds.Height - 2;
        double num = (double) height / (double) facilityImage.Height;
        int width = (int) ((double) facilityImage.Width * num);
        rect = new Rectangle(0, e.Bounds.Y + 1, width, height);
        e.Graphics.DrawImage((Image) facilityImage, rect);
      }
      e.DrawFocusRectangle();
    }
  }
}
