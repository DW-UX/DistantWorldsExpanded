// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.RoundRectanglePanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class RoundRectanglePanel : Panel
  {
    public int CornerCurveRadius = 5;
    public int BorderWidth = 5;

    protected override void OnPaint(PaintEventArgs e) => this.DrawBorder(e.Graphics, this.ForeColor, this.BorderWidth);

    public void DrawBorder(Graphics graphics, Color color, int width)
    {
      int num = width / 2;
      using (GraphicsPath roundRectangle = RoundRectanglePanel.CreateRoundRectangle(new Rectangle(num, num, this.Width - width, this.Height - width), this.CornerCurveRadius - 1))
      {
        GraphicsHelper.SetGraphicsQualityToHigh(graphics);
        using (Pen pen = new Pen(color))
        {
          pen.Width = (float) width;
          graphics.DrawPath(pen, roundRectangle);
        }
      }
    }

    private static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius) => RoundRectanglePanel.CreateRoundRectangle(new RectangleF((float) rectangle.X, (float) rectangle.Y, (float) rectangle.Width, (float) rectangle.Height), (float) radius);

    private static GraphicsPath CreateRoundRectangle(RectangleF rectangle, float radius)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      double left = (double) rectangle.Left;
      double top = (double) rectangle.Top;
      double width = (double) rectangle.Width;
      double height = (double) rectangle.Height;
      float diameter = radius * 2f;
      return RoundRectanglePanel.DefineOutline(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, diameter, radius, 0.0f);
    }

    private static GraphicsPath DefineOutline(
      float left,
      float top,
      float width,
      float height,
      float diameter,
      float radius,
      float correctionAmount)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddArc(left, top, diameter, diameter, 180f, 90f);
      graphicsPath.AddLine(left + radius, top + correctionAmount, left + width - radius, top + correctionAmount);
      graphicsPath.AddArc((float) ((double) left + (double) width - ((double) diameter + (double) correctionAmount)), top, diameter, diameter, 270f, 90f);
      graphicsPath.AddLine(left + width - correctionAmount, top + radius, left + width - correctionAmount, top + height - radius);
      graphicsPath.AddArc((float) ((double) left + (double) width - ((double) diameter + (double) correctionAmount)), (float) ((double) top + (double) height - ((double) diameter + (double) correctionAmount)), diameter, diameter, 0.0f, 90f);
      graphicsPath.AddLine(left + width - radius, top + height - correctionAmount, left + radius, top + height - correctionAmount);
      graphicsPath.AddArc(left, (float) ((double) top + (double) height - ((double) diameter + (double) correctionAmount)), diameter, diameter, 90f, 90f);
      graphicsPath.AddLine(left + correctionAmount, top + height - radius, left + correctionAmount, top + radius);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }
  }
}
