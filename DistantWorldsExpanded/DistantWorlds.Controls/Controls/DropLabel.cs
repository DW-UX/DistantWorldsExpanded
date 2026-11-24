// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DropLabel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DropLabel : Label
  {
    private string _Text;
    private Color _DropshadowColor = Color.Black;
    private int _DropshadowOffset = UiScale.S(1);
    private IContainer components;

    public DropLabel() => this.ForeColor = Color.White;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text
    {
      get => this._Text;
      set
      {
        base.Text = value;
        this._Text = value;
        this.Size = new Size(this.Size.Width + this._DropshadowOffset + UiScale.S(1), this.Size.Height + this._DropshadowOffset + UiScale.S(1));
        this.Invalidate();
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DropshadowColor
    {
      get => this._DropshadowColor;
      set => this._DropshadowColor = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int DropshadowOffset
    {
      get => this._DropshadowOffset;
      set => this._DropshadowOffset = value;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
      e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
      e.Graphics.SmoothingMode = SmoothingMode.None;
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      using StringFormat format = StringFormat.GenericTypographic;
      SizeF sizeF = e.Graphics.MeasureString(this._Text, this.Font, this.Width, format);
      float x = Math.Max(0.0f, (float) (((double) this.Width - (double) sizeF.Width) / 2.0));
      
                using Brush brush1 = new SolidBrush(this.DropshadowColor);
                using Brush brush2 = new SolidBrush(this.ForeColor);
      RectangleF layoutRectangle = new RectangleF(x + (float) this._DropshadowOffset, (float) this._DropshadowOffset, sizeF.Width + UiScale.S(2f), (float) this.Height + UiScale.S(2f));
      e.Graphics.DrawString(this._Text, this.Font,  brush1, layoutRectangle, format);
      layoutRectangle = new RectangleF(x, 0.0f, sizeF.Width + UiScale.S(2f), (float) this.Height + UiScale.S(2f));
      e.Graphics.DrawString(this._Text, this.Font,  brush2, layoutRectangle, format);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
  }
}
