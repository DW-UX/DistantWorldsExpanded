// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.BuiltObjectDropDown
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
  public class BuiltObjectDropDown : ComboBox
  {
    private BuiltObjectList _BuiltObjects;
    private Bitmap[] _BuiltObjectImages;
    private Galaxy _Galaxy;
    public Color HighlightFleetLeadShipsColor = Color.Empty;

    public BuiltObjectDropDown()
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
      this._BuiltObjects = (BuiltObjectList) null;
      this._Galaxy = (Galaxy) null;
    }

    private void ClearImages()
    {
      if (this._BuiltObjectImages == null || this._BuiltObjectImages.Length <= 0)
        return;
      for (int index = 0; index < this._BuiltObjectImages.Length; ++index)
      {
        if (this._BuiltObjectImages[index] != null && this._BuiltObjectImages[index].PixelFormat != PixelFormat.Undefined)
        {
          this._BuiltObjectImages[index].Dispose();
          this._BuiltObjectImages[index] = (Bitmap) null;
        }
      }
    }

    public void InitializeImages(Bitmap[] builtObjectImages)
    {
      this.ClearImages();
      this._BuiltObjectImages = new Bitmap[builtObjectImages.Length];
      for (int index = 0; index < builtObjectImages.Length; ++index)
      {
        int height = 19;
        double num = (double) height / (double) builtObjectImages[index].Height;
        int width = (int) ((double) builtObjectImages[index].Width * num);
        this._BuiltObjectImages[index] = GraphicsHelper.ScaleImage(builtObjectImages[index], width, height, 1f);
      }
    }

    public void BindData(BuiltObjectList builtObjects, Galaxy galaxy, bool normalHeight) => this.BindData(builtObjects, galaxy, normalHeight, Color.Empty);

    public void BindData(
      BuiltObjectList builtObjects,
      Galaxy galaxy,
      bool normalHeight,
      Color highlightFleetLeadShipsColor)
    {
      this._BuiltObjects = builtObjects;
      this._Galaxy = galaxy;
      this.HighlightFleetLeadShipsColor = highlightFleetLeadShipsColor;
      if (normalHeight)
        this.ItemHeight = 19;
      else
        this.ItemHeight = 28;
      this.Items.Clear();
      if (this._BuiltObjects == null)
        return;
      this.Items.AddRange((object[]) this._BuiltObjects.ToArray());
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

    public BuiltObject SelectedBuiltObject
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex < 0 || this._BuiltObjects == null || this._BuiltObjects.Count == 0 ? (BuiltObject) null : this._BuiltObjects[selectedIndex];
      }
    }

    public void SetSelectedBuiltObject(BuiltObject builtObject)
    {
      int num = -1;
      if (this._BuiltObjects != null)
        num = this._BuiltObjects.IndexOf(builtObject);
      this.SelectedIndex = num;
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = this.ItemHeight;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      if (this._BuiltObjects != null && this._BuiltObjects.Count > 0 && e.Index >= 0 && e.Index < this._BuiltObjects.Count)
      {
        int x1 = 0;
        Rectangle rect = new Rectangle();
        if (!this.HighlightFleetLeadShipsColor.IsEmpty && this._BuiltObjects[e.Index].ShipGroup != null && this._BuiltObjects[e.Index].ShipGroup.LeadShip == this._BuiltObjects[e.Index])
        {
          using (SolidBrush solidBrush = new SolidBrush(this.HighlightFleetLeadShipsColor))
            e.Graphics.FillRectangle((Brush) solidBrush, e.Bounds);
        }
        if (this._BuiltObjects[e.Index].Empire != null && this._BuiltObjects[e.Index].Empire != this._Galaxy.IndependentEmpire)
        {
          Bitmap largeFlagPicture = this._BuiltObjects[e.Index].Empire.LargeFlagPicture;
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          int height = e.Bounds.Height - 4;
          double num = (double) height / (double) largeFlagPicture.Height;
          int width = (int) ((double) largeFlagPicture.Width * num);
          int x2 = 4;
          int y = e.Bounds.Y + 2;
          rect = new Rectangle(x2, y, width, height);
          e.Graphics.DrawImage((Image) largeFlagPicture, rect);
          x1 = x2 + width + 4;
        }
        Bitmap builtObjectImage = this._BuiltObjectImages[this._BuiltObjects[e.Index].PictureRef];
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        int height1 = e.Bounds.Height - 2;
        double num1 = (double) height1 / (double) builtObjectImage.Height;
        int width1 = (int) ((double) builtObjectImage.Width * num1);
        int y1 = e.Bounds.Y + 2;
        rect = new Rectangle(x1, y1, width1, height1);
        e.Graphics.DrawImage((Image) builtObjectImage, rect);
        PointF point = new PointF((float) (x1 + width1 + 4), (float) (e.Bounds.Y + Math.Max(1, (this.ItemHeight - 20) / 2)));
        Font font = this.Font;
        SolidBrush solidBrush1 = new SolidBrush(this.ForeColor);
        e.Graphics.DrawString(this._BuiltObjects[e.Index].Name, font, (Brush) solidBrush1, point);
      }
      e.DrawFocusRectangle();
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.ItemHeight = 36;
      this.ResumeLayout(false);
    }
  }
}
