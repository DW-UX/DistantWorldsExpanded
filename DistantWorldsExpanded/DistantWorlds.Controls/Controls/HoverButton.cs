// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HoverButton
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.ComponentModel;

namespace DistantWorlds.Controls
{
  public class HoverButton : Button
  {
    private static string _SoundLocation;
    private static SoundPlayer _SoundPlayer;
    private static object _SoundLock = new object();
    public static double Volume = 1.0;

    private static void PlaySound()
    {
      lock (HoverButton._SoundLock)
      {
        if (string.IsNullOrEmpty(HoverButton._SoundLocation) || HoverButton._SoundPlayer == null || HoverButton.Volume <= 0.0)
          return;
        HoverButton._SoundPlayer.Play();
      }
    }

    public static void SetSoundLocation(string soundLocation)
    {
      if (string.IsNullOrEmpty(soundLocation))
        return;
      HoverButton._SoundLocation = soundLocation;
      HoverButton._SoundPlayer = new SoundPlayer(soundLocation);
      HoverButton._SoundPlayer.Load();
    }

    protected override void OnClick(EventArgs e)
    {
      HoverButton.PlaySound();
      base.OnClick(e);
    }

    public HoverButton()
    {
      base.BackColor = Color.FromArgb(24, 24, 32);
      base.ForeColor = Color.FromArgb(170, 170, 170);
      base.FlatStyle = FlatStyle.Flat;
      base.FlatAppearance.BorderColor = Color.Silver;
      base.FlatAppearance.BorderSize = 0;
      base.FlatAppearance.MouseOverBackColor = Color.FromArgb(48, 48, 64);
      base.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 80);
      base.Font = new Font("Verdana", 8f, FontStyle.Regular);
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Color BackColor
    {
      get => base.BackColor;
      set
      {
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Color ForeColor
    {
      get => base.ForeColor;
      set
      {
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new FlatStyle FlatStyle
    {
      get => base.FlatStyle;
      set
      {
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Font Font
    {
      get => base.Font;
      set => base.Font = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new FlatButtonAppearance FlatAppearance
    {
      get => base.FlatAppearance;
      set
      {
      }
    }
  }
}
