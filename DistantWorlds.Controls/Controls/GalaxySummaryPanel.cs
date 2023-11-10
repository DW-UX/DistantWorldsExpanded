// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GalaxySummaryPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class GalaxySummaryPanel : Panel
  {
    private GalaxySummary _GalaxySummary;
    private Font _LargeFont;
    private Font _NormalFont;
    private Font _BoldFont;
    private int _LineHeight = 12;
    private int _SpaceHeight = 6;
    private int _Margin = 5;
    private SolidBrush _Brush = new SolidBrush(Color.FromArgb(170, 170, 170));

    public void ClearData()
    {
      this._GalaxySummary = (GalaxySummary) null;
      this.Invalidate();
    }

    public void BindData(
      GalaxySummary galaxySummary,
      Font largeFont,
      Font normalFont,
      Font boldFont)
    {
      this._LargeFont = largeFont;
      this._NormalFont = normalFont;
      this._BoldFont = boldFont;
      this._GalaxySummary = galaxySummary;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.Draw(e.Graphics);
    }

    private void Draw(Graphics graphics)
    {
      if (this._GalaxySummary == null)
        return;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(64, 128, 128, 128)))
        graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      int margin = this._Margin;
      SizeF maxSize = new SizeF((float) (this.Width - this._Margin * 2), (float) (this.Height - this._Margin * 2));
      GraphicsHelper.DrawStringWithDropShadow(graphics, this._GalaxySummary.Title, this._LargeFont, new Point(this._Margin, margin), (Brush) this._Brush, maxSize);
      int y1 = margin + this._LargeFont.Height + this._SpaceHeight;
      string text = string.Format(TextResolver.GetText("Physical Galaxy Description"), (object) this._GalaxySummary.StarCount.ToString(), (object) Galaxy.ResolveDescription(this._GalaxySummary.Shape), (object) this._GalaxySummary.SectorWidth.ToString(), (object) this._GalaxySummary.SectorHeight.ToString());
      GraphicsHelper.DrawStringWithDropShadow(graphics, text, this._BoldFont, new Point(this._Margin, y1), (Brush) this._Brush, maxSize);
      int y2 = y1 + this._LineHeight + this._LineHeight + this._LineHeight;
      GraphicsHelper.DrawStringWithDropShadow(graphics, this._GalaxySummary.Description, this._NormalFont, new Point(this._Margin, y2), (Brush) this._Brush, maxSize);
    }
  }
}
