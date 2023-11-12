using System;
using System.Drawing;
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
    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
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
        FitTextAndFontToRect(g, stringRect);
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
        FitTextAndFontToRect(g, stringRect);
        g.DrawString(Text, Font, _foreBrush, stringRect);
        break;
      }
      default:
        throw new NotImplementedException();
    }
  }

  private void FitTextAndFontToRect(Graphics g, in RectangleF stringRect) {
    // measure string and update font to fit the box
    while (g.MeasureString(Text, Font).Width
           > stringRect.Width) {
      var step = 0.1f;
      var newFont = Font;
      while (MathF.Abs(newFont.Size - Font.Size) < 0.001f)
        newFont = new(Font.FontFamily, Font.Size - step, Font.Style);
      Font = newFont;
    }
  }

}