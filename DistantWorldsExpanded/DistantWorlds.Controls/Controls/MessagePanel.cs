// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MessagePanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DistantWorlds.Controls
{
  public class MessagePanel : Panel
  {
    private string _Message = string.Empty;
    private int _Padding = 12;
    protected IFontCache _FontCache;
    private float _FontSize = 15.33f;
    private bool _FontIsBold;

    public MessagePanel()
    {
      this.SetFont(18.67f);
      this.BorderStyle = BorderStyle.None;
      this.BackColor = Color.FromArgb(64, 0, 0, 0);
      this.Padding = new Padding(12);
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Message
    {
      get => this._Message;
      set
      {
        this._Message = value;
        this.Invalidate();
      }
    }

    public virtual void SetFontCache(IFontCache fontCache)
    {
      this._FontCache = fontCache;
      if ((double) this._FontSize <= 0.0)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
    }

    public void SetFont(float pixelSize) => this.SetFont(pixelSize, false);

    public void SetFont(float pixelSize, bool isBold)
    {
      this._FontSize = pixelSize;
      this._FontIsBold = isBold;
      if (this._FontCache == null)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this._Padding = 6;
      RectangleF layoutRectangle = new RectangleF((float) this._Padding, (float) this._Padding, (float) (this.ClientRectangle.Width - this._Padding * 2), (float) (this.ClientRectangle.Height - this._Padding * 2));
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      e.Graphics.DrawString(this._Message, this.Font, (Brush) new SolidBrush(Color.FromArgb(0, 0, 0)), layoutRectangle, StringFormat.GenericDefault);
      layoutRectangle.Offset(-1f, -1f);
      e.Graphics.DrawString(this._Message, this.Font, (Brush) new SolidBrush(Color.FromArgb(224, 224, 224)), layoutRectangle, StringFormat.GenericDefault);
    }
  }
}
