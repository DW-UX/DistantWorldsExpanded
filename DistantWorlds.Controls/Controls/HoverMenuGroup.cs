// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HoverMenuGroup
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class HoverMenuGroup : Panel
  {
    private List<HoverMenuItem> _Items = new List<HoverMenuItem>();
    private HoverMenuItem _HoveredItem;
    protected static IntPtr m_HBitmap;

    public void InitializeMenu()
    {
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.BackColor = Color.Transparent;
      for (int index = 0; index < this._Items.Count; ++index)
      {
        HoverMenuItem hoverMenuItem = this._Items[index];
        if (hoverMenuItem != null)
          hoverMenuItem.Visible = false;
      }
      this.Update();
      Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        Point screen = this.PointToScreen(this.Location);
        graphics.CopyFromScreen(screen, new Point(0, 0), this.Size);
      }
      this.BackgroundImage = (Image) bitmap;
      this.BackColor = Color.White;
      for (int index = 0; index < this._Items.Count; ++index)
      {
        HoverMenuItem hoverMenuItem = this._Items[index];
        if (hoverMenuItem != null)
          hoverMenuItem.Visible = true;
      }
      this.Invalidate();
    }

    public static Bitmap GetDesktopImage()
    {
      IntPtr dc = User32.GetDC(User32.GetDesktopWindow());
      IntPtr compatibleDc = (IntPtr) Gdi32.CreateCompatibleDC((int) dc);
      HoverMenuGroup.SIZE size;
      size.cx = User32.GetSystemMetrics(0);
      size.cy = User32.GetSystemMetrics(1);
      IntPtr compatibleBitmap = (IntPtr) Gdi32.CreateCompatibleBitmap((int) dc, size.cx, size.cy);
      if (!(compatibleBitmap != IntPtr.Zero))
        return (Bitmap) null;
      IntPtr hgdiobj = (IntPtr) Gdi32.SelectObject((int) compatibleDc, (int) compatibleBitmap);
      Gdi32.BitBlt((int) compatibleDc, 0, 0, size.cx, size.cy, (int) dc, 0, 0, 13369376U);
      Gdi32.SelectObject((int) compatibleDc, (int) hgdiobj);
      Gdi32.DeleteDC((int) compatibleDc);
      User32.ReleaseDC(User32.GetDesktopWindow(), dc);
      Bitmap desktopImage = Image.FromHbitmap(compatibleBitmap);
      Gdi32.DeleteObject((int) compatibleBitmap);
      GC.Collect();
      return desktopImage;
    }

    public void AddMenuItem(HoverMenuItem menuItem)
    {
      if (menuItem == null)
        return;
      menuItem.PerformOwnRendering = false;
      this._Items.Add(menuItem);
      if (this.Controls.Contains((Control) menuItem))
        return;
      this.Controls.Add((Control) menuItem);
      menuItem.Parent = (Control) this;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      if (this.BackColor == Color.Transparent)
      {
        base.OnPaintBackground(e);
      }
      else
      {
        if (this.BackgroundImage == null || this.BackgroundImage.PixelFormat == PixelFormat.Undefined)
          return;
        GraphicsHelper.SetGraphicsQualityToLow(e.Graphics);
        e.Graphics.DrawImage(this.BackgroundImage, new Point(0, 0));
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.BackgroundImage != null)
      {
        int pixelFormat = (int) this.BackgroundImage.PixelFormat;
      }
      GraphicsHelper.SetGraphicsQualityToLow(e.Graphics);
      for (int index = 0; index < this._Items.Count; ++index)
      {
        HoverMenuItem hoverMenuItem = this._Items[index];
        if (hoverMenuItem != null && hoverMenuItem.Visible)
        {
          Bitmap bitmap = !hoverMenuItem.IsHovered ? hoverMenuItem.DefaultImage : hoverMenuItem.HoveredImage;
          if (bitmap != null && bitmap.PixelFormat != PixelFormat.Undefined)
          {
            Rectangle destRect = new Rectangle(hoverMenuItem.Left + (hoverMenuItem.Width - bitmap.Width) / 2, hoverMenuItem.Top + (hoverMenuItem.Height - bitmap.Height) / 2, bitmap.Width, bitmap.Height);
            Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            e.Graphics.DrawImage((Image) bitmap, destRect, srcRect, GraphicsUnit.Pixel);
          }
        }
      }
    }

    public void CheckHoverState(
      Point mouseScreenLocation,
      Point parentScreenLocation,
      out bool stateChanged)
    {
      stateChanged = false;
      HoverMenuItem hoverMenuItem1 = (HoverMenuItem) null;
      for (int index = 0; index < this._Items.Count; ++index)
      {
        HoverMenuItem hoverMenuItem2 = this._Items[index];
        if (hoverMenuItem2 != null && hoverMenuItem2.CheckHoverState(mouseScreenLocation, parentScreenLocation))
          hoverMenuItem1 = hoverMenuItem2;
      }
      for (int index = 0; index < this._Items.Count; ++index)
      {
        HoverMenuItem hoverMenuItem3 = this._Items[index];
        if (hoverMenuItem3 != null && hoverMenuItem3 != hoverMenuItem1)
          hoverMenuItem3.IsHovered = false;
      }
      if (this._HoveredItem != hoverMenuItem1)
      {
        stateChanged = true;
        if (stateChanged && this.BackColor == Color.Transparent && this.CanInitialize())
          this.InitializeMenu();
      }
      this._HoveredItem = hoverMenuItem1;
    }

    private bool CanInitialize()
    {
      bool flag = false;
      Form form = this.FindForm();
      if (form != null)
      {
        Control[] controlArray = form.Controls.Find("pnlEncyclopedia", true);
        if (controlArray != null && controlArray.Length == 1)
          flag = !controlArray[0].Visible;
      }
      return flag;
    }

    public struct SIZE
    {
      public int cx;
      public int cy;
    }
  }
}
