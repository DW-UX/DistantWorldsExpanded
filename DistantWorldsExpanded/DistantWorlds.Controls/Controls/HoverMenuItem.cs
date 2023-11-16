// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HoverMenuItem
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Media;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class HoverMenuItem : Panel
  {
    private static string _SoundLocation;
    private static SoundPlayer _SoundPlayer;
    private static object _SoundLock = new object();
    public static double Volume = 1.0;
    public Bitmap DefaultImage;
    public Bitmap HoveredImage;
    public bool IsHovered;
    public bool PerformOwnRendering = true;

    public HoverMenuItem()
    {
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.Size = new Size(100, 30);
    }

    private static void PlaySound()
    {
      lock (HoverMenuItem._SoundLock)
      {
        if (string.IsNullOrEmpty(HoverMenuItem._SoundLocation) || HoverMenuItem._SoundPlayer == null || HoverMenuItem.Volume <= 0.0)
          return;
        HoverMenuItem._SoundPlayer.Play();
      }
    }

    public static void SetSoundLocation(string soundLocation)
    {
      if (string.IsNullOrEmpty(soundLocation))
        return;
      HoverMenuItem._SoundLocation = soundLocation;
      HoverMenuItem._SoundPlayer = new SoundPlayer(soundLocation);
      HoverMenuItem._SoundPlayer.Load();
    }

    public void InitializeMenuItem(Bitmap defaultImage, Bitmap hoveredImage)
    {
      this.BackColor = Color.Transparent;
      this.BorderStyle = BorderStyle.None;
      this.DefaultImage = defaultImage;
      this.HoveredImage = hoveredImage;
    }

    public bool CheckHoverState(Point mouseScreenLocation, Point parentScreenLocation)
    {
      bool flag = new Rectangle(new Point(parentScreenLocation.X + this.Location.X, parentScreenLocation.Y + this.Location.Y), this.Size).Contains(mouseScreenLocation);
      if (this.IsHovered != flag)
        this.Invalidate();
      this.IsHovered = flag;
      return this.IsHovered;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.PerformOwnRendering || !this.Visible)
        return;
      Bitmap bitmap = !this.IsHovered ? this.DefaultImage : this.HoveredImage;
      if (bitmap == null || bitmap.PixelFormat == PixelFormat.Undefined)
        return;
      Rectangle destRect = new Rectangle((this.Width - bitmap.Width) / 2, (this.Height - bitmap.Height) / 2, bitmap.Width, bitmap.Height);
      Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
      e.Graphics.DrawImage((Image) bitmap, destRect, srcRect, GraphicsUnit.Pixel);
    }

    protected override void OnClick(EventArgs e)
    {
      HoverMenuItem.PlaySound();
      base.OnClick(e);
    }
  }
}
