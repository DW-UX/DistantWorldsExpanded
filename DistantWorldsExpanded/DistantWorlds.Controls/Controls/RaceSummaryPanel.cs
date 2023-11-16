// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.RaceSummaryPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class RaceSummaryPanel : Panel
  {
    private RaceSummary _Summary;
    private Font _Font;
    private Font _HeaderFont;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);

    public void BindData(RaceSummary summary, Font font, Font headerFont)
    {
      this.Height = 100;
      this._Summary = summary;
      this._Font = font;
      this._HeaderFont = headerFont;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawSummary(e.Graphics, this._Summary);
    }

    private void DrawSummary(Graphics graphics, RaceSummary summary)
    {
      if (graphics == null || summary == null)
        return;
      GraphicsHelper.SetGraphicsQualityToHigh(graphics);
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      float y = 0.0f;
      int x = 20;
      float num = 10f;
      for (int index1 = 0; index1 < summary.Sections.Count; ++index1)
      {
        RaceSummarySection section = summary.Sections[index1];
        if (!string.IsNullOrEmpty(section.Heading))
        {
          SizeF size = graphics.MeasureString(section.Heading, this._HeaderFont, this.Width);
          this.DrawStringWithDropShadowBounded(graphics, section.Heading, this._HeaderFont, new Point(0, (int) y), size);
          y += size.Height;
        }
        for (int index2 = 0; index2 < section.Items.Count; ++index2)
        {
          string text = section.Items[index2];
          SizeF size = graphics.MeasureString(text, this._Font, this.Width - x);
          this.DrawStringWithDropShadowBounded(graphics, text, this._Font, new Point(x, (int) y), size);
          y += size.Height;
        }
        y += num;
      }
      if (this.Height >= (int) y + 1)
        return;
      this.Height = (int) y + 1;
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      this.DrawStringWithDropShadow(graphics, text, font, location, (Brush) this._WhiteBrush);
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Brush brush)
    {
      location = new Point(location.X + 1, location.Y + 1);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location);
      location = new Point(location.X - 1, location.Y - 1);
      graphics.DrawString(text, font, brush, (PointF) location);
    }

    private void DrawStringWithDropShadowBounded(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SizeF size)
    {
      this.DrawStringWithDropShadowBounded(graphics, text, font, location, size, Color.Empty);
    }

    private void DrawStringWithDropShadowBounded(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SizeF size,
      Color textColor)
    {
      location = new Point(location.X + 1, location.Y + 1);
      PointF pointF = new PointF((float) location.X, (float) location.Y);
      RectangleF layoutRectangle = new RectangleF(pointF.X, pointF.Y, size.Width + 2f, size.Height + 2f);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, layoutRectangle, StringFormat.GenericTypographic);
      location = new Point(location.X - 1, location.Y - 1);
      pointF = new PointF((float) location.X, (float) location.Y);
      layoutRectangle = new RectangleF(pointF.X, pointF.Y, size.Width + 2f, size.Height + 2f);
      if (textColor.IsEmpty)
      {
        graphics.DrawString(text, font, (Brush) this._WhiteBrush, layoutRectangle, StringFormat.GenericTypographic);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(textColor))
          graphics.DrawString(text, font, (Brush) solidBrush, layoutRectangle, StringFormat.GenericTypographic);
      }
    }
  }
}
