// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.TroopDropDown
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
  public class TroopDropDown : ComboBox
  {
    private TroopList _Troops;
    private Bitmap[] _TroopImagesInfantry;
    private Bitmap[] _TroopImagesArmored;
    private Bitmap[] _TroopImagesArtillery;
    private Bitmap[] _TroopImagesSpecialForces;
    private Bitmap[] _TroopImagesPirateRaider;

    public TroopDropDown() => this.SetDefaults(this.Font);

    private void SetDefaults(Font font)
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(22, 21, 26);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = font;
    }

    public void ClearData() => this._Troops = (TroopList) null;

    public void BindData(
      Font font,
      TroopList troops,
      Bitmap[] troopImagesInfantry,
      Bitmap[] troopImagesArmored,
      Bitmap[] troopImagesArtillery,
      Bitmap[] troopImagesSpecialForces,
      Bitmap[] troopImagesPirateRaider)
    {
      this.SetDefaults(font);
      this._Troops = troops;
      this.Items.Clear();
      if (this._Troops != null)
        this.Items.AddRange((object[]) this._Troops.ToArray());
      GraphicsHelper.DisposeImageArray(this._TroopImagesInfantry);
      GraphicsHelper.DisposeImageArray(this._TroopImagesArmored);
      GraphicsHelper.DisposeImageArray(this._TroopImagesArtillery);
      GraphicsHelper.DisposeImageArray(this._TroopImagesSpecialForces);
      GraphicsHelper.DisposeImageArray(this._TroopImagesPirateRaider);
      this._TroopImagesInfantry = new Bitmap[0];
      this._TroopImagesArmored = new Bitmap[0];
      this._TroopImagesArtillery = new Bitmap[0];
      this._TroopImagesSpecialForces = new Bitmap[0];
      this._TroopImagesPirateRaider = new Bitmap[0];
      this.SetupImageArray(troopImagesInfantry, ref this._TroopImagesInfantry);
      this.SetupImageArray(troopImagesArmored, ref this._TroopImagesArmored);
      this.SetupImageArray(troopImagesArtillery, ref this._TroopImagesArtillery);
      this.SetupImageArray(troopImagesSpecialForces, ref this._TroopImagesSpecialForces);
      this.SetupImageArray(troopImagesPirateRaider, ref this._TroopImagesPirateRaider);
    }

    private void SetupImageArray(Bitmap[] supplied, ref Bitmap[] cached)
    {
      if (supplied == null || supplied.Length <= 0)
        return;
      cached = new Bitmap[supplied.Length];
      for (int index = 0; index < supplied.Length; ++index)
      {
        int height = 19;
        double num = (double) height / (double) supplied[index].Height;
        int width = (int) ((double) supplied[index].Width * num);
        cached[index] = this.PrecacheScaledBitmap(supplied[index], width, height);
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
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.DrawImage((Image) unscaledBitmap, new Rectangle(0, 0, width, height));
      graphics.Dispose();
      return bitmap;
    }

    public void SetSelectedTroop(Troop troop)
    {
      int num = -1;
      if (this._Troops != null && troop != null)
      {
        for (int index = 0; index < this._Troops.Count; ++index)
        {
          Troop troop1 = this._Troops[index];
          if (troop1 != null && troop1.Race == troop.Race && troop1.Type == troop.Type)
          {
            num = index;
            break;
          }
        }
      }
      this.SelectedIndex = num;
    }

    public Troop SelectedTroop
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (Troop) null;
        return this._Troops != null && selectedIndex >= 0 && selectedIndex < this._Troops.Count ? this._Troops[selectedIndex] : (Troop) null;
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
      PointF point = new PointF(30f, (float) (e.Bounds.Y + 2));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      if (this._Troops != null && this._Troops.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        string empty = string.Empty;
        string s;
        if (this._Troops[index].Race != null)
          s = this._Troops[index].Name + "  (" + this._Troops[index].Race.Name + " " + Galaxy.ResolveDescription(this._Troops[index].Type) + ")";
        else
          s = this._Troops[index].Name + "  (" + Galaxy.ResolveDescription(this._Troops[index].Type) + ")";
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
        Rectangle rect = new Rectangle();
        Bitmap bitmap = (Bitmap) null;
        switch (this._Troops[index].Type)
        {
          case TroopType.Infantry:
            bitmap = this._TroopImagesInfantry[this._Troops[index].PictureRef];
            break;
          case TroopType.Armored:
            bitmap = this._TroopImagesArmored[this._Troops[index].PictureRef];
            break;
          case TroopType.Artillery:
            bitmap = this._TroopImagesArtillery[this._Troops[index].PictureRef];
            break;
          case TroopType.SpecialForces:
            bitmap = this._TroopImagesSpecialForces[this._Troops[index].PictureRef];
            break;
          case TroopType.PirateRaider:
            bitmap = this._TroopImagesPirateRaider[this._Troops[index].PictureRef];
            break;
        }
        if (bitmap != null)
        {
          int height = e.Bounds.Height - 2;
          double num = (double) height / (double) bitmap.Height;
          int width = (int) ((double) bitmap.Width * num);
          rect = new Rectangle((30 - bitmap.Width) / 2, e.Bounds.Y + 1, width, height);
          e.Graphics.DrawImage((Image) bitmap, rect);
        }
      }
      e.DrawFocusRectangle();
    }
  }
}
