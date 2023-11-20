// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MapKey
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class MapKey : GradientPanel
  {
    private IContainer components;
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
    private SolidBrush _SunPen = new SolidBrush(Color.Yellow);
    private SolidBrush _BarrenRockPlanetPen = new SolidBrush(Color.FromArgb(64, 64, 64));
    private SolidBrush _VolcanicPlanetPen = new SolidBrush(Color.OrangeRed);
    private SolidBrush _DesertPlanetPen = new SolidBrush(Color.SandyBrown);
    private SolidBrush _MarshySwampPlanetPen = new SolidBrush(Color.Yellow);
    private SolidBrush _ContinentalPlanetPen = new SolidBrush(Color.Green);
    private SolidBrush _OceanPlanetPen = new SolidBrush(Color.Blue);
    private SolidBrush _IcePlanetPen = new SolidBrush(Color.Aqua);
    private SolidBrush _GasGiantPlanetPen = new SolidBrush(Color.Red);
    private SolidBrush _FrozenGasGiantPlanetPen = new SolidBrush(Color.DeepPink);
    private SolidBrush _MainSequencePen = new SolidBrush(Color.Yellow);
    private SolidBrush _RedGiantPen = new SolidBrush(Color.Red);
    private SolidBrush _SuperGiantPen = new SolidBrush(Color.Red);
    private SolidBrush _WhiteDwarfPen = new SolidBrush(Color.White);
    private SolidBrush _NeutronPen = new SolidBrush(Color.Aqua);
    private SolidBrush _SuperNovaPen = new SolidBrush(Color.Purple);
    private SolidBrush _BlackHolePen = new SolidBrush(Color.FromArgb(0, 0, 176));
    private SolidBrush _GasCloudPen = new SolidBrush(Color.Violet);
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _TitleFont;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();

    public override void SetFontCache(IFontCache fontCache)
    {
      base.SetFontCache(fontCache);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public MapKey()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawColorKey(e.Graphics);
    }

    private int CalculateCenterAlignedOffsetPosition(
      Graphics graphics,
      string text,
      Font font,
      int width)
    {
      int width1 = (int) graphics.MeasureString(text, font, width, StringFormat.GenericTypographic).Width;
      return (width - width1) / 2;
    }

    private int CalculateRightAlignedOffsetPosition(
      Graphics graphics,
      string text,
      Font font,
      int width)
    {
      int width1 = (int) graphics.MeasureString(text, font, width, StringFormat.GenericTypographic).Width;
      return width - width1;
    }

    private void DrawKeyItem(Graphics graphics, Color color, string description, Point location)
    {
      Point location1 = new Point(location.X + 10, location.Y + 2);
      graphics.FillRectangle((Brush) new SolidBrush(color), new Rectangle(location1, new Size(10, 10)));
      Point point = new Point(location.X + 26, location.Y);
      graphics.DrawString(description, this.Font, (Brush) this._WhiteBrush, (PointF) point);
    }

    private void DrawColorKey(Graphics graphics)
    {
      int x = 10;
      int y1 = 30;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      Point location = new Point(x, 10);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Trade Description Galaxy Map"), this._TitleFont, location);
      location = new Point(x, y1);
      this.DrawKeyItem(graphics, this._MainSequencePen.Color, TextResolver.GetText("Main Sequence star system"), location);
      int y2 = y1 + this._RowHeight;
      location = new Point(x, y2);
      this.DrawKeyItem(graphics, this._RedGiantPen.Color, TextResolver.GetText("Red Giant or Super Giant star system"), location);
      int y3 = y2 + this._RowHeight;
      location = new Point(x, y3);
      this.DrawKeyItem(graphics, this._WhiteDwarfPen.Color, TextResolver.GetText("White Dwarf star system"), location);
      int y4 = y3 + this._RowHeight;
      location = new Point(x, y4);
      this.DrawKeyItem(graphics, this._NeutronPen.Color, TextResolver.GetText("Neutron star system"), location);
      int y5 = y4 + this._RowHeight;
      location = new Point(x, y5);
      this.DrawKeyItem(graphics, this._SuperNovaPen.Color, TextResolver.GetText("Supernova star"), location);
      int y6 = y5 + this._RowHeight;
      location = new Point(x, y6);
      this.DrawKeyItem(graphics, this._BlackHolePen.Color, TextResolver.GetText("HabitatType BlackHole"), location);
      int y7 = y6 + this._RowHeight;
      location = new Point(x, y7);
      this.DrawKeyItem(graphics, this._GasCloudPen.Color, TextResolver.GetText("HabitatCategoryType GasCloud"), location);
      int y8 = y7 + this._RowHeight + this._RowHeight;
      location = new Point(x, y8);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("System Map"), this._TitleFont, location);
      int y9 = y8 + (int) ((double) this._RowHeight * 1.5);
      location = new Point(x, y9);
      this.DrawKeyItem(graphics, this._ContinentalPlanetPen.Color, TextResolver.GetText("Continental planet or moon"), location);
      int y10 = y9 + this._RowHeight;
      location = new Point(x, y10);
      this.DrawKeyItem(graphics, this._MarshySwampPlanetPen.Color, TextResolver.GetText("Marshy Swamp planet or moon"), location);
      int y11 = y10 + this._RowHeight;
      location = new Point(x, y11);
      this.DrawKeyItem(graphics, this._DesertPlanetPen.Color, TextResolver.GetText("Desert planet or moon"), location);
      int y12 = y11 + this._RowHeight;
      location = new Point(x, y12);
      this.DrawKeyItem(graphics, this._OceanPlanetPen.Color, TextResolver.GetText("Ocean planet or moon"), location);
      int y13 = y12 + this._RowHeight;
      location = new Point(x, y13);
      this.DrawKeyItem(graphics, this._IcePlanetPen.Color, TextResolver.GetText("Ice planet or moon"), location);
      int y14 = y13 + this._RowHeight;
      location = new Point(x, y14);
      this.DrawKeyItem(graphics, this._VolcanicPlanetPen.Color, TextResolver.GetText("Volcanic planet or moon"), location);
      int y15 = y14 + this._RowHeight;
      location = new Point(x, y15);
      this.DrawKeyItem(graphics, this._BarrenRockPlanetPen.Color, TextResolver.GetText("Barren Rock planet, moon or Asteroid"), location);
      int y16 = y15 + this._RowHeight;
      location = new Point(x, y16);
      this.DrawKeyItem(graphics, this._GasGiantPlanetPen.Color, TextResolver.GetText("Gas Giant planet"), location);
      int y17 = y16 + this._RowHeight;
      location = new Point(x, y17);
      this.DrawKeyItem(graphics, this._FrozenGasGiantPlanetPen.Color, TextResolver.GetText("Frozen Gas Giant planet"), location);
      int y18 = y17 + this._RowHeight + this._RowHeight;
      location = new Point(x, y18);
      this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Filtered View Items"), this._TitleFont, location);
      int y19 = y18 + (int) ((double) this._RowHeight * 1.5);
      location = new Point(x, y19);
      this.DrawKeyItem(graphics, Color.Yellow, TextResolver.GetText("Item matching view filter"), location);
      int y20 = y19 + this._RowHeight;
      location = new Point(x, y20);
      this.DrawKeyItem(graphics, Color.FromArgb(80, 80, 80), TextResolver.GetText("Item not matching view filter"), location);
      int num = y20 + this._RowHeight;
    }

    private SolidBrush SelectGoodBadBrush(double value) => this.SelectGoodBadBrush(value, true);

    private SolidBrush SelectGoodBadBrush(double value, bool negativeIsBad)
    {
      SolidBrush solidBrush = this._WhiteBrush;
      if (value < 0.0)
        solidBrush = !negativeIsBad ? this._GreenBrush : this._RedBrush;
      else if (value > 0.0)
        solidBrush = !negativeIsBad ? this._RedBrush : this._GreenBrush;
      return solidBrush;
    }

    private SolidBrush SelectBrush(double value) => this.SelectBrush(value, true);

    private SolidBrush SelectBrush(double value, bool negativeIsRed)
    {
      SolidBrush solidBrush = this._WhiteBrush;
      if (value < 0.0)
        solidBrush = this._RedBrush;
      return solidBrush;
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      location = new Point(location.X + 1, location.Y + 1);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location);
      location = new Point(location.X - 1, location.Y - 1);
      graphics.DrawString(text, font, (Brush) this._WhiteBrush, (PointF) location);
    }
  }
}
