using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls;

public partial class CheckBox : System.Windows.Forms.CheckBox {

  private SolidBrush _foreBrush;

  private Pen _backPen;

  public CheckBox()
    : base() {
    base.AutoSize = false;
  }

  public override bool AutoSize {
    set => base.AutoSize = false; // don't support
    get => base.AutoSize;
  }

  protected override void OnPaint(PaintEventArgs e) {
    base.OnPaintBackground(e);
    var g = e.Graphics;
    g.CompositingQuality = CompositingQuality.HighQuality;
    g.SmoothingMode = SmoothingMode.HighQuality;
    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    var side = Math.Min(ClientSize.Width, ClientSize.Height);
    var state = Checked ? ButtonState.Checked : ButtonState.Normal;

    if (_foreBrush?.Color != ForeColor)
      _foreBrush = new(ForeColor);
    if (_backPen?.Color != BackColor)
      _backPen = new(BackColor);

    switch (CheckAlign) {
      case ContentAlignment.TopLeft:
      case ContentAlignment.MiddleLeft: {
        ControlPaint.DrawCheckBox(g, Padding.Left, Padding.Top, side, side, state);
        var stringRect = new RectangleF(
          Padding.Left + side,
          Padding.Top,
          ClientSize.Width - side - Padding.Left,
          side);
        FitTextAndFontToRect(g, stringRect, DeviceDpi);
        g.DrawString(Text, Font, _foreBrush, stringRect);
        break;
      }
      case ContentAlignment.TopRight:
      case ContentAlignment.MiddleRight: {
        var x = ClientSize.Width - side - Padding.Right;
        ControlPaint.DrawCheckBox(g, x, Padding.Top, side, side, state);
        var stringRect = new RectangleF(
          Padding.Left,
          Padding.Top,
          x - Padding.Left,
          side
        );
        FitTextAndFontToRect(g, stringRect, DeviceDpi);
        g.DrawString(Text, Font, _foreBrush, stringRect);
        break;
      }
      default:
        throw new NotImplementedException();
    }
  }

  private void FitTextAndFontToRect(Graphics g, in RectangleF stringRect, int deviceDpi) {
    // measure string and update font to fit the box
    var step = Font.Unit switch {
      GraphicsUnit.Pixel => 1f,
      GraphicsUnit.Inch => 1f / deviceDpi,
      GraphicsUnit.Display => 1f / (deviceDpi / 75f),
      GraphicsUnit.Document => 1f / (deviceDpi / 300f),
      GraphicsUnit.Millimeter => 1f / (deviceDpi / 25.4f),
      GraphicsUnit.Point => 1f / (deviceDpi / 72f),
      _ => throw new NotImplementedException()
    };
    
    var width = stringRect.Width + 1;
    while (g.MeasureString(Text, Font).Width > width) {
      Font = new(Font.FontFamily, Font.Size - step, Font.Style, Font.Unit);
    }
  }

}