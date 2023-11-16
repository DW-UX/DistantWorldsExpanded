// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.FlagShapeDropDown
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
  public class FlagShapeDropDown : ComboBox
  {
    private Color _PrimaryColor = Color.FromArgb(48, 48, 48);
    private Color _SecondaryColor = Color.FromArgb(208, 208, 208);
    private List<Bitmap> _FlagShapes = new List<Bitmap>();

    public FlagShapeDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void Ignite()
    {
      this.Items.Clear();
      if (this._FlagShapes == null)
        return;
      for (int index = 0; index < this._FlagShapes.Count; ++index)
        this.Items.Add((object) index);
    }

    public void BindData(Color primaryColor, Color secondaryColor, List<Bitmap> flagShapes)
    {
      this._FlagShapes = flagShapes;
      this._PrimaryColor = primaryColor;
      this._SecondaryColor = secondaryColor;
      this.Ignite();
    }

    public void SetSelectedFlagShape(int flagShapeIndex)
    {
      if (flagShapeIndex >= 0 && flagShapeIndex <= this._FlagShapes.Count)
        this.SelectedIndex = flagShapeIndex;
      else
        this.SelectedIndex = -1;
    }

    public int SelectedFlagShapeIndex => this.SelectedIndex;

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = this.Size.Height - 2;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      Font font = this.Font;
      SolidBrush solidBrush = new SolidBrush(this.ForeColor);
      if (e.Index >= 0)
      {
        Bitmap smallFlagPicture = (Bitmap) null;
        Bitmap largeFlagPicture = (Bitmap) null;
        Galaxy.GenerateEmpireFlag(this._PrimaryColor, this._SecondaryColor, e.Index, this._FlagShapes, ref smallFlagPicture, ref largeFlagPicture);
        e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        int height = e.Bounds.Height - 2;
        int width = (int) ((double) height * 1.666666);
        Rectangle rect = new Rectangle((e.Bounds.Width - width) / 2, e.Bounds.Y + (e.Bounds.Height - height) / 2, width, height);
        e.Graphics.DrawImage((Image) largeFlagPicture, rect);
        largeFlagPicture.Dispose();
        smallFlagPicture.Dispose();
      }
      e.DrawFocusRectangle();
    }
  }
}
