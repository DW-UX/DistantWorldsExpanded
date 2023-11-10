// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HabitatDropDown
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
  public class HabitatDropDown : ComboBox
  {
    private HabitatList _Habitats;
    private Bitmap[] _HabitatImages;
    private Galaxy _Galaxy;

    public HabitatDropDown()
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
      this._Habitats = (HabitatList) null;
      this._Galaxy = (Galaxy) null;
    }

    private void ClearImages()
    {
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

    public void InitializeImages(Bitmap[] habitatImages)
    {
      this.ClearImages();
      this._HabitatImages = new Bitmap[habitatImages.Length];
      for (int index = 0; index < habitatImages.Length; ++index)
      {
        int height = 19;
        double num = (double) height / (double) habitatImages[index].Height;
        int width = (int) ((double) habitatImages[index].Width * num);
        this._HabitatImages[index] = this.PrecacheScaledBitmap(habitatImages[index], width, height);
      }
    }

    public void BindData(HabitatList habitats, Galaxy galaxy)
    {
      this._Habitats = habitats;
      this._Galaxy = galaxy;
      this.Items.Clear();
      if (this._Habitats == null)
        return;
      this._Habitats = this._Habitats.OrderByName();
      this.Items.AddRange((object[]) this._Habitats.ToArray());
    }

    public Habitat SelectedHabitat
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex < 0 ? (Habitat) null : this._Habitats[selectedIndex];
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 19;
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

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(56f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      if (this._Habitats != null && this._Habitats.Count > 0 && e.Index >= 0)
      {
        e.Graphics.DrawString(this._Habitats[e.Index].Name, font, (Brush) solidBrush, point);
        Rectangle rect = new Rectangle();
        if (this._Habitats[e.Index].Empire != null && this._Habitats[e.Index].Empire != this._Galaxy.IndependentEmpire)
        {
          Bitmap largeFlagPicture = this._Habitats[e.Index].Empire.LargeFlagPicture;
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          int height = e.Bounds.Height - 2;
          double num = (double) height / (double) largeFlagPicture.Height;
          int width = (int) ((double) largeFlagPicture.Width * num);
          rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
          e.Graphics.DrawImage((Image) largeFlagPicture, rect);
        }
        Bitmap habitatImage = this._HabitatImages[(int) this._Habitats[e.Index].PictureRef];
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        int height1 = e.Bounds.Height - 2;
        double num1 = (double) height1 / (double) habitatImage.Height;
        int width1 = (int) ((double) habitatImage.Width * num1);
        rect = new Rectangle(38, e.Bounds.Y + 1, width1, height1);
        e.Graphics.DrawImage((Image) habitatImage, rect);
      }
      e.DrawFocusRectangle();
    }
  }
}
