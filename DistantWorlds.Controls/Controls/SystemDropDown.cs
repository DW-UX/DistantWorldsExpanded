// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.SystemDropDown
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
  public class SystemDropDown : ComboBox
  {
    private SystemInfoList _Systems;
    private Bitmap[] _HabitatImages;
    private Galaxy _Galaxy;

    public SystemDropDown()
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
      this._Systems = (SystemInfoList) null;
      this._Galaxy = (Galaxy) null;
    }

    public void BindData(SystemInfoList systems, Bitmap[] habitatImages, Galaxy galaxy)
    {
      this._Systems = systems;
      this._Galaxy = galaxy;
      this.Items.Clear();
      if (this._Systems != null)
      {
        this._Systems.Sort();
        this.Items.AddRange((object[]) this._Systems.ToArray());
      }
      this._HabitatImages = new Bitmap[habitatImages.Length];
      for (int index = 0; index < habitatImages.Length; ++index)
      {
        int height = 19;
        double num = (double) height / (double) habitatImages[index].Height;
        int width = (int) ((double) habitatImages[index].Width * num);
        this._HabitatImages[index] = this.PrecacheScaledBitmap(habitatImages[index], width, height);
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

    public SystemInfo SelectedSystem
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex < 0 ? (SystemInfo) null : this._Systems[selectedIndex];
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
      PointF point = new PointF(40f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      if (this._Systems[e.Index].SystemStar != null)
        e.Graphics.DrawString(this._Systems[e.Index].SystemStar.Name, font, (Brush) solidBrush, point);
      else
        e.Graphics.DrawString("(" + TextResolver.GetText("Unknown") + ")", font, (Brush) solidBrush, point);
      Rectangle rect = new Rectangle();
      if (this._Systems[e.Index].DominantEmpire != null && this._Systems[e.Index].DominantEmpire.Empire != null && this._Systems[e.Index].DominantEmpire.Empire != this._Galaxy.IndependentEmpire)
      {
        Bitmap largeFlagPicture = this._Systems[e.Index].DominantEmpire.Empire.LargeFlagPicture;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        int height = e.Bounds.Height - 2;
        double num = (double) height / (double) largeFlagPicture.Height;
        int width = (int) ((double) largeFlagPicture.Width * num);
        rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
        e.Graphics.DrawImage((Image) largeFlagPicture, rect);
      }
      if (this._Systems[e.Index].SystemStar != null)
      {
        Bitmap habitatImage = this._HabitatImages[(int) this._Systems[e.Index].SystemStar.PictureRef];
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        int height = e.Bounds.Height - 2;
        double num = (double) height / (double) habitatImage.Height;
        int width = (int) ((double) habitatImage.Width * num);
        rect = new Rectangle(22, e.Bounds.Y + 1, width, height);
        e.Graphics.DrawImage((Image) habitatImage, rect);
      }
      e.DrawFocusRectangle();
    }
  }
}
