using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DistantWorlds.Controls;

public partial class CheckBox : System.Windows.Forms.CheckBox
{

    private static readonly StringFormat _textFormat = new(StringFormat.GenericDefault)
    {
        FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip,
        LineAlignment = StringAlignment.Center,
        // Captions are written for standard check boxes, where "&&" stands for a plain ampersand.
        HotkeyPrefix = HotkeyPrefix.Hide,
    };

    private SolidBrush _foreBrush;

    private Pen _backPen;

    private bool _fitToText;

    public CheckBox()
      : base()
    {
        base.AutoSize = false;
    }

    // The base class cannot auto-size an owner-drawn check box, so asking for it
    // makes this control grow to fit its own text instead.
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool AutoSize
    {
        set
        {
            base.AutoSize = false;
            _fitToText = value;
            FitToText();
        }
        get => base.AutoSize;
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        FitToText();
    }

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        FitToText();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        FitToText();
    }

    private void FitToText()
    {
        if (!_fitToText || string.IsNullOrEmpty(Text))
        {
            return;
        }
        SizeF text;
        using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
        {
            text = g.MeasureString(Text, Font, int.MaxValue, _textFormat);
        }
        // Only the width follows the text: the height also sets the size of the box, and rows are spaced for it.
        int width = Padding.Horizontal + Height + (int)Math.Ceiling(text.Width);
        if (width > Width)
        {
            Width = width;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaintBackground(e);
        var g = e.Graphics;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        int lineHeight = (int)Math.Ceiling(Font.GetHeight(g));
        // A check box taller than a line and a half is meant to hold wrapped text beside a normal-sized box.
        bool multiline = ClientSize.Height > lineHeight * 3 / 2 + Padding.Vertical;
        var side = multiline ? Math.Min(ClientSize.Width, lineHeight + 2) : Math.Min(ClientSize.Width, ClientSize.Height);
        int boxY = multiline ? Padding.Top + (ClientSize.Height - Padding.Vertical - side) / 2 : Padding.Top;
        int textHeight = multiline ? ClientSize.Height - Padding.Vertical : side;
        var state = Checked ? ButtonState.Checked : ButtonState.Normal;

        if (_foreBrush?.Color != ForeColor)
            _foreBrush = new(ForeColor);
        if (_backPen?.Color != BackColor)
            _backPen = new(BackColor);

        switch (CheckAlign)
        {
            case ContentAlignment.TopLeft:
            case ContentAlignment.MiddleLeft:
                {
                    ControlPaint.DrawCheckBox(g, Padding.Left, boxY, side, side, state);
                    var stringRect = new RectangleF(
                      Padding.Left + side,
                      Padding.Top,
                      ClientSize.Width - side - Padding.Left,
                      textHeight);
                    DrawText(g, stringRect, multiline);
                    break;
                }
            case ContentAlignment.TopRight:
            case ContentAlignment.MiddleRight:
                {
                    var x = ClientSize.Width - side - Padding.Right;
                    ControlPaint.DrawCheckBox(g, x, boxY, side, side, state);
                    var stringRect = new RectangleF(
                      Padding.Left,
                      Padding.Top,
                      x - Padding.Left,
                      textHeight
                    );
                    DrawText(g, stringRect, multiline);
                    break;
                }
            default:
                throw new NotImplementedException();
        }
    }

    private static readonly StringFormat _wrappedFormat = new(StringFormat.GenericDefault)
    {
        LineAlignment = StringAlignment.Center,
        HotkeyPrefix = HotkeyPrefix.Hide,
    };

    private void DrawText(Graphics g, RectangleF stringRect, bool multiline)
    {
        if (multiline)
        {
            using Font wrapped = FitFont(g, font => g.MeasureString(Text, font, (int)stringRect.Width, _wrappedFormat).Height <= stringRect.Height + 1);
            g.DrawString(Text, wrapped, _foreBrush, stringRect, _wrappedFormat);
            return;
        }
        if (_fitToText)
        {
            g.DrawString(Text, Font, _foreBrush, stringRect, _textFormat);
            return;
        }
        float width = stringRect.Width + 1;
        using Font fitted = FitFont(g, font => g.MeasureString(Text, font).Width <= width);
        g.DrawString(Text, fitted, _foreBrush, stringRect);
    }

    // A fixed-size check box keeps its bounds, so shrink the font until the text fits them.
    private Font FitFont(Graphics g, Func<Font, bool> fits)
    {
        int deviceDpi = DeviceDpi;
        var step = Font.Unit switch
        {
            GraphicsUnit.Pixel => 1f,
            GraphicsUnit.Inch => 1f / deviceDpi,
            GraphicsUnit.Display => 1f / (deviceDpi / 75f),
            GraphicsUnit.Document => 1f / (deviceDpi / 300f),
            GraphicsUnit.Millimeter => 1f / (deviceDpi / 25.4f),
            GraphicsUnit.Point => 1f / (deviceDpi / 72f),
            _ => throw new NotImplementedException()
        };

        var font = new Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);
        while (font.Size - step > 0f && !fits(font))
        {
            var smaller = new Font(font.FontFamily, font.Size - step, font.Style, font.Unit);
            font.Dispose();
            font = smaller;
        }
        return font;
    }

}
