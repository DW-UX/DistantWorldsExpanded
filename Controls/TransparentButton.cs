// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.TransparentButton
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class TransparentButton : Button
  {
    public Bitmap ButtonImages;
    protected int imageWidth;
    protected int imageHeight;
    protected int xOffset;
    protected int yOffset;
    private System.ComponentModel.Container components;
    private TransparentButton.ControlState enmState;
    private bool bCanClick;
    private Point locPoint;

    public void SetBitmap(Bitmap bm)
    {
      this.ButtonImages = bm;
      this.imageWidth = bm.Width / 4;
      this.imageHeight = bm.Height;
    }

    public void SetRegion(GraphicsPath gp) => this.Region = new Region(gp);

    public TransparentButton() => this.InitializeComponent();

    public Point AdjustImageLocation
    {
      get => this.locPoint;
      set
      {
        this.locPoint = value;
        this.Invalidate();
      }
    }

    protected override void OnClick(EventArgs ea)
    {
      this.Capture = false;
      this.bCanClick = false;
      this.enmState = !this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)) ? TransparentButton.ControlState.Normal : TransparentButton.ControlState.Hover;
      this.Invalidate();
      base.OnClick(ea);
    }

    protected override void OnMouseEnter(EventArgs ea)
    {
      base.OnMouseEnter(ea);
      this.enmState = TransparentButton.ControlState.Hover;
      this.Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs mea)
    {
      base.OnMouseDown(mea);
      if (mea.Button != MouseButtons.Left)
        return;
      this.bCanClick = true;
      this.enmState = TransparentButton.ControlState.Pressed;
      this.Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs mea)
    {
      base.OnMouseMove(mea);
      if (this.ClientRectangle.Contains(mea.X, mea.Y))
      {
        if (this.enmState != TransparentButton.ControlState.Hover || !this.Capture || this.bCanClick)
          return;
        this.bCanClick = true;
        this.enmState = TransparentButton.ControlState.Pressed;
        this.Invalidate();
      }
      else
      {
        if (this.enmState != TransparentButton.ControlState.Pressed)
          return;
        this.bCanClick = false;
        this.enmState = TransparentButton.ControlState.Hover;
        this.Invalidate();
      }
    }

    protected override void OnMouseLeave(EventArgs ea)
    {
      base.OnMouseLeave(ea);
      this.enmState = TransparentButton.ControlState.Normal;
      this.Invalidate();
    }

    [DllImport("gdi32.dll")]
    private static extern int GetPixel(IntPtr hdc, int nX, int nY);

    protected override void OnPaint(PaintEventArgs pea)
    {
      base.OnPaint(pea);
      this.OnPaintBackground(pea);
      switch (this.enmState)
      {
        case TransparentButton.ControlState.Normal:
          if (this.Enabled)
          {
            if (this.Focused || this.IsDefault)
            {
              this.OnDrawDefault(pea.Graphics);
              break;
            }
            this.OnDrawNormal(pea.Graphics);
            break;
          }
          this.OnDrawDisabled(pea.Graphics);
          break;
        case TransparentButton.ControlState.Hover:
          this.OnDrawHover(pea.Graphics);
          break;
        case TransparentButton.ControlState.Pressed:
          this.OnDrawPressed(pea.Graphics);
          break;
      }
      if (this.ButtonImages == null)
        return;
      Rectangle srcRect = new Rectangle(this.xOffset + pea.ClipRectangle.X, this.yOffset + pea.ClipRectangle.Y, pea.ClipRectangle.Width, pea.ClipRectangle.Height);
      GraphicsUnit srcUnit = GraphicsUnit.Pixel;
      pea.Graphics.DrawImage((Image) this.ButtonImages, pea.ClipRectangle, srcRect, srcUnit);
    }

    protected override void OnEnabledChanged(EventArgs ea)
    {
      base.OnEnabledChanged(ea);
      this.enmState = TransparentButton.ControlState.Normal;
      this.Invalidate();
    }

    private void DrawNormalButton(Graphics g)
    {
      this.xOffset = 0;
      this.yOffset = 0;
    }

    private void OnDrawDefault(Graphics g) => this.DrawNormalButton(g);

    private void OnDrawNormal(Graphics g) => this.DrawNormalButton(g);

    private void OnDrawHover(Graphics g)
    {
      this.xOffset = this.imageWidth * 2;
      this.yOffset = 0;
    }

    private void OnDrawPressed(Graphics g)
    {
      this.xOffset = this.imageWidth;
      this.yOffset = 0;
    }

    private void OnDrawDisabled(Graphics g)
    {
      this.xOffset = this.imageWidth * 3;
      this.yOffset = 0;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = new System.ComponentModel.Container();

    public enum ControlState
    {
      Normal,
      Hover,
      Pressed,
      Default,
      Disabled,
    }
  }
}
