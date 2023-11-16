// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DesignDropDown
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
  public class DesignDropDown : ComboBox
  {
    private DesignList _Designs;
    private Bitmap[] _BuiltObjectImages;
    private Empire _IndependentEmpire;
    private bool _AllowNullDesign;

    public DesignDropDown()
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
      this._Designs = (DesignList) null;
      this._IndependentEmpire = (Empire) null;
    }

    public void BindData(DesignList designs, Bitmap[] builtObjectImages, Empire independentEmpire) => this.BindData(designs, builtObjectImages, independentEmpire, false);

    public void BindData(
      DesignList designs,
      Bitmap[] builtObjectImages,
      Empire independentEmpire,
      bool allowNullDesign)
    {
      this._Designs = designs;
      this._IndependentEmpire = independentEmpire;
      this._AllowNullDesign = allowNullDesign;
      this.Items.Clear();
      if (this._AllowNullDesign)
        this.Items.Add(new object());
      if (this._Designs != null)
      {
        this._Designs.Sort();
        this.Items.AddRange((object[]) this._Designs.ToArray());
      }
      if (builtObjectImages == null)
        return;
      this._BuiltObjectImages = new Bitmap[builtObjectImages.Length];
      for (int index = 0; index < builtObjectImages.Length; ++index)
      {
        Bitmap builtObjectImage = builtObjectImages[index];
        if (builtObjectImage != null && builtObjectImage.PixelFormat != PixelFormat.Undefined)
        {
          int height = 19;
          double num = (double) height / (double) builtObjectImage.Height;
          int width = (int) ((double) builtObjectImage.Width * num);
          this._BuiltObjectImages[index] = this.PrecacheScaledBitmap(builtObjectImage, width, height);
          this._BuiltObjectImages[index].MakeTransparent(Color.Black);
          this._BuiltObjectImages[index].RotateFlip(RotateFlipType.Rotate270FlipNone);
        }
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

    public Design SelectedDesign
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (selectedIndex < 0)
          return (Design) null;
        if (this._AllowNullDesign)
        {
          if (selectedIndex == 0)
            return (Design) null;
          --selectedIndex;
        }
        return this._Designs != null && selectedIndex >= 0 && selectedIndex < this._Designs.Count ? this._Designs[selectedIndex] : (Design) null;
      }
    }

    public void SetSelectedDesign(Design design)
    {
      int num = -1;
      if (this._Designs != null && design != null)
        num = this._Designs.IndexOf(design);
      if (this._AllowNullDesign)
      {
        ++num;
        if (design == null)
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
      PointF point = new PointF(49f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      string empty = string.Empty;
      if (this._AllowNullDesign && e.Index == 0)
        e.Graphics.DrawString("(" + TextResolver.GetText("None") + ")", font, (Brush) solidBrush, point);
      else if (this._Designs != null && this._Designs.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        if (this._AllowNullDesign)
          --index;
        string s = Galaxy.ResolveDescription(this._Designs[index].SubRole) + " (" + this._Designs[index].Name + ")";
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
        Rectangle rect = new Rectangle();
        if (this._Designs[index].Empire != null && this._Designs[index].Empire != this._IndependentEmpire)
        {
          Bitmap largeFlagPicture = this._Designs[index].Empire.LargeFlagPicture;
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          int height = e.Bounds.Height - 2;
          double num = (double) height / (double) largeFlagPicture.Height;
          int width = (int) ((double) largeFlagPicture.Width * num);
          rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
          e.Graphics.DrawImage((Image) largeFlagPicture, rect);
        }
        Bitmap builtObjectImage = this._BuiltObjectImages[this._Designs[index].PictureRef];
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        int height1 = e.Bounds.Height - 2;
        double num1 = (double) height1 / (double) builtObjectImage.Height;
        int width1 = (int) ((double) builtObjectImage.Width * num1);
        rect = new Rectangle(30, e.Bounds.Y + 1, width1, height1);
        e.Graphics.DrawImage((Image) builtObjectImage, rect);
      }
      e.DrawFocusRectangle();
    }
  }
}
