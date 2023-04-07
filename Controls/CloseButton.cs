// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CloseButton
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CloseButton : Button
  {
    private Color _HoverForeColor;
    private Color _HoverBackColor;
    private int _Radius;
    private bool _IsHovered;
    private static string _SoundLocation;
    private static SoundPlayer _SoundPlayer;
    private static object _SoundLock = new object();
    public static double Volume = 1.0;

    public int Radius
    {
      get => this._Radius;
      set => this._Radius = value;
    }

    public CloseButton()
    {
      this.ForeColor = Color.FromArgb(67, 67, 77);
      this.BackColor = Color.Black;
      this._HoverForeColor = Color.FromArgb(134, 134, 154);
      this._HoverBackColor = Color.Black;
      this._Radius = 8;
      this.FlatStyle = FlatStyle.Standard;
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
      this.UpdateStyles();
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 32;
        return createParams;
      }
    }

    private static void PlaySound()
    {
      lock (CloseButton._SoundLock)
      {
        if (string.IsNullOrEmpty(CloseButton._SoundLocation) || CloseButton._SoundPlayer == null || CloseButton.Volume <= 0.0)
          return;
        CloseButton._SoundPlayer.Play();
      }
    }

    public static void SetSoundLocation(string soundLocation)
    {
      if (string.IsNullOrEmpty(soundLocation))
        return;
      SoundPlayer soundPlayer1 = CloseButton._SoundPlayer;
      CloseButton._SoundLocation = soundLocation;
      SoundPlayer soundPlayer2 = new SoundPlayer(soundLocation);
      soundPlayer2.Load();
      CloseButton._SoundPlayer = soundPlayer2;
      soundPlayer1?.Dispose();
    }

    protected override void OnClick(EventArgs e)
    {
      CloseButton.PlaySound();
      base.OnClick(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      this._IsHovered = true;
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      this._IsHovered = false;
      base.OnMouseLeave(e);
    }

    protected void InvalidateEx()
    {
      if (this.Parent == null)
        return;
      this.Parent.Invalidate(new Rectangle(this.Location, this.Size), true);
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
      if (this._IsHovered)
        this.DrawShape(pevent.Graphics, this._HoverForeColor, this._HoverBackColor, 2f);
      else
        this.DrawShape(pevent.Graphics, this.ForeColor, this.BackColor, 3f);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent) => this.InvalidateEx();

    private GraphicsPath GenerateRoundedRectangle(Rectangle rectangle, int radius)
    {
      GraphicsPath roundedRectangle = new GraphicsPath();
      int left = rectangle.Left;
      int top = rectangle.Top;
      int width = rectangle.Width;
      int height = rectangle.Height;
      int num = radius << 1;
      roundedRectangle.AddArc(left, top, num, num, 180f, 90f);
      roundedRectangle.AddLine(left + radius, top, left + width - radius, top);
      roundedRectangle.AddArc(left + width - num, top, num, num, 270f, 90f);
      roundedRectangle.AddLine(left + width, top + radius, left + width, top + height - radius);
      roundedRectangle.AddArc(left + width - num, top + height - num, num, num, 0.0f, 90f);
      roundedRectangle.AddLine(left + width - radius, top + height, left + radius, top + height);
      roundedRectangle.AddArc(left, top + height - num, num, num, 90f, 90f);
      roundedRectangle.AddLine(left, top + height - radius, left, top + radius);
      roundedRectangle.CloseFigure();
      return roundedRectangle;
    }

    private void DrawShape(Graphics graphics, Color foreColor, Color backColor, float penWidth)
    {
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      this._Radius = 8;
      int num = 8;
      if ((double) penWidth <= 2.5)
        ++num;
      using (GraphicsPath roundedRectangle = this.GenerateRoundedRectangle(Rectangle.Inflate(this.ClientRectangle, -2, -2), this._Radius))
      {
        using (Pen pen = new Pen(foreColor, penWidth))
        {
          graphics.DrawPath(pen, roundedRectangle);
          graphics.DrawLine(pen, num, num, this.Width - num, this.Height - num);
          graphics.DrawLine(pen, num, this.Height - num, this.Width - num, num);
        }
      }
    }
  }
}
