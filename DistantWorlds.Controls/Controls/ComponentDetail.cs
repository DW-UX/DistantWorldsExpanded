// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ComponentDetail
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ComponentDetail : GradientPanel
  {
    private Galaxy _Galaxy;
    private DistantWorlds.Types.Component _Component;
    private Bitmap _Image;
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
    private int _DescriptionX = 10;
    private int _DescriptionWidth = 115;
    private int _ValueX = 135;
    private int _ValueWidth = 40;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _TitleFont;
    private Font _SmallFont;
    private Font _SmallBoldFont;
    private IContainer components;

    public ComponentDetail()
    {
      this.Font = new Font("Verdana", 8f);
      this.SetFont(15.33f);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
      this._SmallFont = new Font(this.Font.FontFamily, 14.67f, FontStyle.Regular, GraphicsUnit.Pixel);
      this._SmallBoldFont = new Font(this.Font.FontFamily, 14.67f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._Component = (DistantWorlds.Types.Component) null;
    }

    public void Ignite(Galaxy galaxy, DistantWorlds.Types.Component component, Bitmap componentImage) => this.Ignite(galaxy, component, componentImage, false);

    public void Ignite(Galaxy galaxy, DistantWorlds.Types.Component component, Bitmap componentImage, bool largeFont)
    {
      this._Galaxy = galaxy;
      this._Component = component;
      this._Image = componentImage;
      if (largeFont)
      {
        this._RowHeight = 18;
        this._BoldFont = new Font(this.Font.FontFamily, this.Font.Size + 4f, FontStyle.Bold, GraphicsUnit.Pixel);
        this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 6f, FontStyle.Bold, GraphicsUnit.Pixel);
        this._SmallFont = new Font(this.Font.FontFamily, 17f, FontStyle.Regular, GraphicsUnit.Pixel);
        this._SmallBoldFont = new Font(this.Font.FontFamily, 17f, FontStyle.Bold, GraphicsUnit.Pixel);
      }
      else
      {
        this._RowHeight = 14;
        this._BoldFont = new Font(this.Font, FontStyle.Bold);
        this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
        this._SmallFont = new Font(this.Font.FontFamily, 13f, FontStyle.Regular, GraphicsUnit.Pixel);
        this._SmallBoldFont = new Font(this.Font.FontFamily, 13f, FontStyle.Bold, GraphicsUnit.Pixel);
      }
      this.Invalidate();
    }

    public DistantWorlds.Types.Component Component => this._Component;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawComponentDetailInfo(e.Graphics);
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

    public void ResetTextPositions()
    {
      this._DescriptionX = 10;
      this._DescriptionWidth = 115;
      this._ValueX = 135;
      this._ValueWidth = 40;
    }

    public void SetTextPositions(
      int descriptionX,
      int descriptionWidth,
      int valueX,
      int valueWidth)
    {
      this._DescriptionX = descriptionX;
      this._DescriptionWidth = descriptionWidth;
      this._ValueX = valueX;
      this._ValueWidth = valueWidth;
    }

    private void DrawComponentDetailInfo(Graphics graphics)
    {
      string[] descriptions = new string[8];
      string[] values = new string[8];
      int num = 30;
      Point location = new Point();
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      location = new Point(this._DescriptionX - 2, 12);
      if (this._Component == null)
        return;
      string title;
      string type;
      string sizeCost;
      string staticEnergy;
      this._Galaxy.ResolveComponentDescriptionComplete(this._Galaxy.PlayerEmpire, this._Component, (ComponentImprovement) null, out title, out type, out sizeCost, out staticEnergy, out descriptions, out values);
      int width = (int) ((double) this._Image.Width * (20.0 / (double) this._Image.Height));
      int x = this.Width - (10 + width);
      Rectangle rect = new Rectangle(x, 10, width, 20);
      StringFormat format1 = new StringFormat(StringFormatFlags.FitBlackBox);
      format1.Trimming = StringTrimming.EllipsisCharacter;
      SizeF sizeF1 = graphics.MeasureString(this._Component.Name, this._TitleFont, x - location.X, format1);
      if ((double) sizeF1.Height > 22.0)
        location.Y -= 7;
      RectangleF layoutRectangle1 = new RectangleF((float) location.X, (float) location.Y, sizeF1.Width + 3f, sizeF1.Height + 3f);
      graphics.DrawString(title, this._TitleFont, (Brush) this._BlackBrush, layoutRectangle1, format1);
      layoutRectangle1 = new RectangleF(layoutRectangle1.X - 1f, layoutRectangle1.Y - 1f, layoutRectangle1.Width, layoutRectangle1.Height);
      graphics.DrawString(title, this._TitleFont, (Brush) this._WhiteBrush, layoutRectangle1, format1);
      graphics.DrawImage((Image) this._Image, rect);
      int y1 = num + this._RowHeight;
      location = new Point(this._DescriptionX, y1);
      this.DrawStringWithDropShadow(graphics, type, this._SmallBoldFont, location);
      int y2 = y1 + this._RowHeight;
      location = new Point(this._DescriptionX, y2);
      this.DrawStringWithDropShadow(graphics, sizeCost, this._SmallFont, location);
      int y3 = y2 + this._RowHeight;
      if (this._Component.EnergyUsed > 0)
      {
        location = new Point(this._DescriptionX + this.CalculateRightAlignedOffsetPosition(graphics, TextResolver.GetText("Static Energy Used"), this._SmallFont, this._DescriptionWidth), y3);
        this.DrawStringWithDropShadow(graphics, TextResolver.GetText("Static Energy Used"), this._SmallFont, location);
        location = new Point(this._ValueX, y3 - 2);
        this.DrawStringWithDropShadow(graphics, staticEnergy, this._SmallBoldFont, location);
        y3 += this._RowHeight;
      }
      for (int index = 0; index < 8; ++index)
      {
        if (!string.IsNullOrEmpty(descriptions[index]))
        {
          if (string.IsNullOrEmpty(values[index]))
          {
            StringFormat format2 = new StringFormat(StringFormatFlags.FitBlackBox);
            format2.Trimming = StringTrimming.EllipsisCharacter;
            SizeF sizeF2 = graphics.MeasureString(descriptions[index], this._SmallFont, this.Width - 6, format2);
            RectangleF layoutRectangle2 = new RectangleF(4f, (float) y3, sizeF2.Width + 3f, sizeF2.Height + 3f);
            graphics.DrawString(descriptions[index], this._SmallFont, (Brush) this._BlackBrush, layoutRectangle2, format2);
            layoutRectangle2 = new RectangleF(layoutRectangle2.X - 1f, layoutRectangle2.Y - 1f, layoutRectangle2.Width, layoutRectangle2.Height);
            graphics.DrawString(descriptions[index], this._SmallFont, (Brush) this._WhiteBrush, layoutRectangle2, format2);
            y3 += (int) sizeF2.Height;
          }
          else
          {
            location = new Point(this._DescriptionX + this.CalculateRightAlignedOffsetPosition(graphics, descriptions[index], this._SmallFont, this._DescriptionWidth), y3);
            this.DrawStringWithDropShadow(graphics, descriptions[index], this._SmallFont, location);
            location = new Point(this._ValueX, y3 - 1);
            this.DrawStringWithDropShadow(graphics, values[index], this._SmallBoldFont, location);
            y3 += this._RowHeight - 1;
          }
        }
      }
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }
  }
}
