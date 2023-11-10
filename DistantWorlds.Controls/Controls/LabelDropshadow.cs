// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.LabelDropshadow
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class LabelDropshadow : Panel
  {
    private string _Text;
    private Color _DropshadowColor = Color.Black;
    private int _DropshadowOffset = 1;

    public LabelDropshadow() => this.ForeColor = Color.White;

    public new string Text
    {
      get => this._Text;
      set
      {
        this._Text = value;
        this.Invalidate();
      }
    }

    public Color DropshadowColor
    {
      get => this._DropshadowColor;
      set => this._DropshadowColor = value;
    }

    public int DropshadowOffset
    {
      get => this._DropshadowOffset;
      set => this._DropshadowOffset = value;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      SizeF sizeF = e.Graphics.MeasureString(this._Text, this.Font, this.Width, StringFormat.GenericTypographic);
      float x = Math.Max(0.0f, (float) (((double) this.Width - (double) sizeF.Width) / 2.0));
      RectangleF layoutRectangle = new RectangleF(x + (float) this._DropshadowOffset, (float) this._DropshadowOffset, sizeF.Width + 2f, (float) this.Height + 2f);
      e.Graphics.DrawString(this._Text, this.Font, (Brush) new SolidBrush(this.DropshadowColor), layoutRectangle, StringFormat.GenericTypographic);
      layoutRectangle = new RectangleF(x, 0.0f, sizeF.Width + 2f, (float) this.Height + 2f);
      e.Graphics.DrawString(this._Text, this.Font, (Brush) new SolidBrush(this.ForeColor), layoutRectangle, StringFormat.GenericTypographic);
    }
  }
}
