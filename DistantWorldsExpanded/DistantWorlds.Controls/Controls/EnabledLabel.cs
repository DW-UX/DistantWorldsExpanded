// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EnabledLabel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class EnabledLabel : Panel
  {
    private IContainer components;
    private string _Text;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();

    public EnabledLabel() => this.InitializeComponent();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text
    {
      get => this._Text;
      set => this._Text = value;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      RectangleF layoutRectangle = new RectangleF(0.0f, 0.0f, (float) this.Width, (float) this.Height);
      e.Graphics.DrawString(this._Text, this.Font, (Brush) new SolidBrush(Color.White), layoutRectangle, StringFormat.GenericTypographic);
    }
  }
}
