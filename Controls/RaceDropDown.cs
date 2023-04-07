// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.RaceDropDown
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
  public class RaceDropDown : ComboBox
  {
    private RaceList _Races;
    private Bitmap[] _RaceImages;
    private bool _AllowRandomRace;

    public RaceDropDown() => this.SetDefaults(this.Font);

    private void SetDefaults(Font font)
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(22, 21, 26);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = font;
    }

    public void ClearData() => this._Races = (RaceList) null;

    public void BindData(Font font, RaceList races, Bitmap[] raceImages) => this.BindData(font, races, raceImages, false);

    public void BindData(Font font, RaceList races, Bitmap[] raceImages, bool allowRandomRace)
    {
      this.SetDefaults(font);
      this._Races = races;
      this._AllowRandomRace = allowRandomRace;
      this.Items.Clear();
      if (this._AllowRandomRace)
        this.Items.Add(new object());
      if (this._Races != null)
      {
        this._Races.Sort();
        this.Items.AddRange((object[]) this._Races.ToArray());
      }
      this._RaceImages = new Bitmap[0];
      if (raceImages == null || raceImages.Length <= 0)
        return;
      this._RaceImages = new Bitmap[raceImages.Length];
      for (int index = 0; index < raceImages.Length; ++index)
      {
        int height = this.ClientRectangle.Height - 2;
        double num = (double) height / (double) raceImages[index].Height;
        int width = (int) ((double) raceImages[index].Width * num);
        this._RaceImages[index] = this.PrecacheScaledBitmap(raceImages[index], width, height);
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

    public void SetSelectedRace(Race race)
    {
      int num = -1;
      if (this._Races != null)
        num = this._Races.IndexOf(race);
      if (this._AllowRandomRace && race == null)
        num = 0;
      if (this._AllowRandomRace)
        ++num;
      this.SelectedIndex = num;
    }

    public Race SelectedRace
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (Race) null;
        if (this._AllowRandomRace && selectedIndex == 0)
          return (Race) null;
        if (this._AllowRandomRace)
          --selectedIndex;
        return this._Races[selectedIndex];
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = this.ClientRectangle.Height - 2;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      float num1 = (float) ((double) (this.ClientRectangle.Height - this.Font.Height) / 2.0 - 2.0);
      PointF point = new PointF((float) this.ClientRectangle.Height, (float) e.Bounds.Y + num1);
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      if (this._AllowRandomRace && e.Index == 0)
        e.Graphics.DrawString("(" + TextResolver.GetText("Random") + ")", font, (Brush) solidBrush, point);
      else if (this._Races != null && this._Races.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowRandomRace)
          --index;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.DrawString(this._Races[index].Name, font, (Brush) solidBrush, point);
        Rectangle rect = new Rectangle();
        Bitmap raceImage = this._RaceImages[this._Races[index].PictureRef];
        int height = e.Bounds.Height - 2;
        double num2 = (double) height / (double) raceImage.Height;
        int width = (int) ((double) raceImage.Width * num2);
        rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
        e.Graphics.DrawImage((Image) raceImage, rect);
      }
      e.DrawFocusRectangle();
    }
  }
}
