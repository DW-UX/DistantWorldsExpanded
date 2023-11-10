// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.FleetDropDown
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
  public class FleetDropDown : ComboBox
  {
    private ShipGroupList _Fleets;
    private Galaxy _Galaxy;
    private bool _NullFleetSelection;
    private Bitmap[] _BuiltObjectImages;

    public FleetDropDown()
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
        double num = (double) height / (double) builtObjectImages[index].Width;
        int width = (int) ((double) builtObjectImages[index].Height * num);
        this._BuiltObjectImages[index] = GraphicsHelper.ScaleImage(builtObjectImages[index], width, height, 1f);
        this._BuiltObjectImages[index].RotateFlip(RotateFlipType.Rotate270FlipNone);
      }
    }

    public void BindData(ShipGroupList fleets, bool provideNullSelection, Galaxy galaxy)
    {
      this._Fleets = fleets;
      this._Galaxy = galaxy;
      this._NullFleetSelection = provideNullSelection;
      this.Items.Clear();
      if (this._NullFleetSelection)
        this.Items.Add((object) "");
      if (this._Fleets == null)
        return;
      this._Fleets.Sort();
      this.Items.AddRange((object[]) this._Fleets.ToArray());
    }

    public void SetSelectedFleet(ShipGroup fleet)
    {
      if (fleet != null)
      {
        int num = this._Fleets.IndexOf(fleet);
        if (this._NullFleetSelection)
        {
          if (num >= 0)
            ++num;
          else
            num = 0;
        }
        this.SelectedIndex = num;
      }
      else if (this._NullFleetSelection)
        this.SelectedIndex = 0;
      else
        this.SelectedIndex = -1;
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
        return selectedIndex < 0 ? (ShipGroup) null : this._Fleets[selectedIndex - num];
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
      PointF point = new PointF(60f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      if (this._Fleets != null && e.Index >= 0)
      {
        int num1 = 0;
        if (this._NullFleetSelection)
          ++num1;
        if (this._NullFleetSelection && e.Index == 0)
        {
          e.Graphics.DrawString("(" + TextResolver.GetText("No Fleet") + ")", font, (Brush) solidBrush, point);
        }
        else
        {
          int index = e.Index - num1;
          e.Graphics.DrawString(this._Fleets[index].Name, font, (Brush) solidBrush, point);
          Rectangle rect = new Rectangle();
          if (this._Fleets[index].Empire != null && this._Fleets[index].Empire != this._Galaxy.IndependentEmpire)
          {
            Bitmap largeFlagPicture = this._Fleets[index].Empire.LargeFlagPicture;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int height = e.Bounds.Height - 2;
            double num2 = (double) height / (double) largeFlagPicture.Height;
            int width = (int) ((double) largeFlagPicture.Width * num2);
            rect = new Rectangle(3, e.Bounds.Y + 1, width, height);
            e.Graphics.DrawImage((Image) largeFlagPicture, rect);
          }
          BuiltObject leadShip = this._Fleets[index].LeadShip;
          if (leadShip != null && this._BuiltObjectImages != null && this._BuiltObjectImages.Length > leadShip.PictureRef)
          {
            Bitmap builtObjectImage = this._BuiltObjectImages[leadShip.PictureRef];
            if (builtObjectImage != null)
              e.Graphics.DrawImage((Image) builtObjectImage, new Point(38, e.Bounds.Y + 1));
          }
        }
      }
      e.DrawFocusRectangle();
    }
  }
}
