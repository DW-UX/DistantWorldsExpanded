// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.SmoothLabel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class SmoothLabel : Label
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      base.OnPaint(e);
    }
  }
}
