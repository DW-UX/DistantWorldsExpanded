// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.BorderPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    [Serializable]
    public class BorderPanel : Panel
  {
    private int _BorderSize = 3;
    private Color _BorderColor1 = Color.FromArgb(96, 200, 200, 200);
    private Color _BorderColor2 = Color.FromArgb(96, 140, 140, 140);
    private Color _BorderColor3 = Color.FromArgb(96, 20, 20, 20);
    private Color _BorderColor4 = Color.FromArgb(96, 80, 80, 80);
    private List<Control> _HighlightControls = new List<Control>();
    private Color _HighlightColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
    private Color _HighlightColor1 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
    private Color _HighlightColor2 = Color.FromArgb(128, (int) byte.MaxValue, 48, 96);
    protected IFontCache _FontCache;
    private float _FontSize = FontSize.Normal;
    private bool _FontIsBold;

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

    public BorderPanel()
    {
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.UpdateStyles();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<Control> HighlightControls
    {
      get => this._HighlightControls;
      set
      {
        this._HighlightControls = value;
        this.Invalidate();
      }
    }

    public int BorderSize
    {
      get => this._BorderSize;
      set => this._BorderSize = value;
    }

    public Color BorderColor1
    {
      get => this._BorderColor1;
      set
      {
      }
    }

    public Color BorderColor2
    {
      get => this._BorderColor2;
      set
      {
      }
    }

    public Color BorderColor3
    {
      get => this._BorderColor3;
      set
      {
      }
    }

    public Color BorderColor4
    {
      get => this._BorderColor4;
      set
      {
      }
    }

    private void UpdateHighlightColor()
    {
      int second = DateTime.Now.ToUniversalTime().Second;
      int millisecond = DateTime.Now.ToUniversalTime().Millisecond;
      if (second % 2 == 1)
        millisecond += 1000;
      double num = millisecond <= 1000 ? (double) Math.Abs(1000 - millisecond) / 1000.0 : (double) (millisecond - 1000) / 1000.0;
      this._HighlightColor = Color.FromArgb((int) (byte) ((uint) this._HighlightColor1.A - (uint) (byte) ((double) ((int) this._HighlightColor1.A - (int) this._HighlightColor2.A) * num)), (int) (byte) ((uint) this._HighlightColor1.R - (uint) (byte) ((double) ((int) this._HighlightColor1.R - (int) this._HighlightColor2.R) * num)), (int) (byte) ((uint) this._HighlightColor1.G - (uint) (byte) ((double) ((int) this._HighlightColor1.G - (int) this._HighlightColor2.G) * num)), (int) (byte) ((uint) this._HighlightColor1.B - (uint) (byte) ((double) ((int) this._HighlightColor1.B - (int) this._HighlightColor2.B) * num)));
    }

    protected override void OnVisibleChanged(EventArgs e) {
      base.OnVisibleChanged(e);
      ClearHighlightControls();
    }

    protected void ClearHighlightControls() {
      this._HighlightControls = (List<Control>)null;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this._HighlightControls != null && this._HighlightControls.Count > 0)
      {
        this.UpdateHighlightColor();
        using (Pen pen = new Pen(this._HighlightColor, 4f))
        {
          foreach (Control highlightControl in this._HighlightControls)
          {
            if (highlightControl.Parent == this || this is ScreenPanel && ((ScreenPanel) this).pnlBody == highlightControl.Parent)
            {
              Rectangle rect = new Rectangle(highlightControl.Location.X - 3, highlightControl.Location.Y - 3, highlightControl.Size.Width + 6, highlightControl.Size.Height + 6);
              if (this is ScreenPanel && ((ScreenPanel) this).pnlBody == highlightControl.Parent)
              {
                using (Graphics graphics = ((ScreenPanel) this).pnlBody.CreateGraphics())
                  graphics.DrawRectangle(pen, rect);
              }
              else
                e.Graphics.DrawRectangle(pen, rect);
            }
          }
        }
      }
      else
      {
        this.BorderStyle = BorderStyle.None;
        this.BackColor = Color.FromArgb(48, 48, 64);
        base.OnPaint(e);
      }
      int num1 = 0;
      int num2 = this.ClientRectangle.Width - 1;
      int num3 = 0;
      int num4 = this.ClientRectangle.Height - 1;
      Pen pen1 = new Pen(this._BorderColor1, 1f);
      Pen pen2 = new Pen(this._BorderColor2, 1f);
      Pen pen3 = new Pen(this._BorderColor3, 1f);
      Pen pen4 = new Pen(this._BorderColor4, 1f);
      for (int index = 0; index < this._BorderSize; ++index)
      {
        e.Graphics.DrawLine(pen1, num1 + index, num3 + index, num2 - index, num3 + index);
        e.Graphics.DrawLine(pen2, num1 + index, num3 + index, num1 + index, num4 - index);
        e.Graphics.DrawLine(pen3, num1 + index, num4 - index, num2 - index, num4 - index);
        e.Graphics.DrawLine(pen4, num2 - index, num3 + index, num2 - index, num4 - index);
      }
      pen1.Dispose();
      pen2.Dispose();
      pen3.Dispose();
      pen4.Dispose();
    }
  }
}
