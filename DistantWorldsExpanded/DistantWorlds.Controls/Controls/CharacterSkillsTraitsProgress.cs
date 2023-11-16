// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterSkillsTraitsProgress
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CharacterSkillsTraitsProgress : Panel
  {
    private Character _Character;
    private Font _LargeFont;
    private Font _LargeBoldFont;
    private Font _HugeFont;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private volatile bool _Initializing;

    public CharacterSkillsTraitsProgress()
    {
      this.Font = new Font("Verdana", 8f);
      this.InitializeFonts();
    }

    private void InitializeFonts()
    {
      this._LargeFont = new Font(this.Font.FontFamily, 16.67f, FontStyle.Regular, GraphicsUnit.Pixel);
      this._LargeBoldFont = new Font(this.Font.FontFamily, 16.67f, FontStyle.Bold, GraphicsUnit.Pixel);
      this._HugeFont = new Font(this.Font.FontFamily, 22.67f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void BindData(Character character)
    {
      this.Height = 0;
      this._Initializing = true;
      this._Character = character;
      this.InitializeFonts();
      this._Initializing = false;
      this.Invalidate();
    }

    public void ClearData() => this._Character = (Character) null;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawCharacter(e.Graphics, this._Character);
    }

    private void DrawCharacter(Graphics graphics, Character character)
    {
      if (graphics == null || character == null || this._Initializing)
        return;
      GraphicsHelper.SetGraphicsQualityToHigh(graphics);
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      int y = 2;
      int num1 = 5;
      int width = 205;
      int x1 = 200;
      int x2 = 260;
      int num2 = 100;
      int height = 18;
      int num3 = 3;
      string empty = string.Empty;
      if (character.Traits.Count > 0)
      {
        string text = empty + TextResolver.GetText("Traits").ToUpper(CultureInfo.InvariantCulture) + ": ";
        if (character.BonusesKnown)
        {
          for (int index = 0; index < character.Traits.Count; ++index)
            text = text + Galaxy.ResolveDescription(character.Traits[index]) + ", ";
          if (!string.IsNullOrEmpty(text) && text.Length >= 2)
            text = text.Substring(0, text.Length - 2);
        }
        else
          text += TextResolver.GetText("Character untested - skill levels and traits unknown");
        SizeF size = graphics.MeasureString(text, this._LargeFont, this.Width);
        this.DrawStringWithDropShadowBounded(graphics, text, this._LargeFont, new Point(0, y), size);
        y = y + (int) size.Height + num1;
      }
      if (character.Skills != null && character.Skills.Count > 0)
      {
        for (int index = 0; index < character.Skills.Count; ++index)
        {
          CharacterSkill skill = character.Skills[index];
          if (skill != null)
          {
            string text1 = Galaxy.ResolveDescription(skill.Type);
            SizeF size = graphics.MeasureString(text1, this._LargeFont, width);
            int x3 = width - (int) size.Width;
            this.DrawStringWithDropShadowBounded(graphics, text1, this._LargeFont, new Point(x3, y), size);
            if (character.BonusesKnown)
            {
              int skillLevel = character.GetSkillLevel(skill.Type);
              string text2 = ((double) skillLevel / 100.0).ToString("+0%;-0%");
              if (skillLevel >= 0)
                this.DrawStringWithDropShadow(graphics, text2, this._HugeFont, new Point(x1, y - 5), (Brush) this._GreenBrush);
              else
                this.DrawStringWithDropShadow(graphics, text2, this._HugeFont, new Point(x1, y - 5), (Brush) this._RedBrush);
            }
            else
              this.DrawStringWithDropShadow(graphics, "?%", this._HugeFont, new Point(x1, y - 5), (Brush) this._WhiteBrush);
            float num4 = skill.Progress / skill.NextProgressThreshold;
            this.DrawBarGraph(skill.NextProgressThreshold, skill.Progress, height, num2, Color.FromArgb(40, 40, 40), Color.FromArgb(170, 170, 170), Color.FromArgb(96, 0, 0, 0), graphics, new Point(x2, y - 2), num4.ToString("0%"));
            y = y + (int) size.Height + num3;
          }
        }
      }
      if (character.BonusesKnown && character.TraitSkills != null && character.TraitSkills.Count > 0)
      {
        for (int index = 0; index < character.TraitSkills.Count; ++index)
        {
          CharacterSkill traitSkill = character.TraitSkills[index];
          if (traitSkill != null && character.Skills.GetSkillByType(traitSkill.Type) == null)
          {
            int skillLevel = character.GetSkillLevel(traitSkill.Type);
            if (skillLevel != 0)
            {
              string text3 = Galaxy.ResolveDescription(traitSkill.Type);
              SizeF size = graphics.MeasureString(text3, this._LargeFont, width);
              int x4 = width - (int) size.Width;
              this.DrawStringWithDropShadowBounded(graphics, text3, this._LargeFont, new Point(x4, y), size);
              if (character.BonusesKnown)
              {
                string text4 = ((double) skillLevel / 100.0).ToString("+0%;-0%");
                if (skillLevel >= 0)
                  this.DrawStringWithDropShadow(graphics, text4, this._HugeFont, new Point(x1, y - 5), (Brush) this._GreenBrush);
                else
                  this.DrawStringWithDropShadow(graphics, text4, this._HugeFont, new Point(x1, y - 5), (Brush) this._RedBrush);
              }
              else
                this.DrawStringWithDropShadow(graphics, "?%", this._HugeFont, new Point(x1, y - 5), (Brush) this._WhiteBrush);
              string text5 = "(" + TextResolver.GetText("from Trait") + ")";
              SizeF sizeF = graphics.MeasureString(text5, this.Font, num2);
              this.DrawStringWithDropShadow(graphics, text5, this.Font, new Point(x2 + (num2 - (int) sizeF.Width) / 2, y + 1));
              y = y + (int) size.Height + num3;
            }
          }
        }
      }
      if (this.Height >= y)
        return;
      this.Height = y;
    }

    private void DrawBarGraph(
      float maximumValue,
      float currentValue,
      int height,
      int overallWidth,
      Color fillColorStart,
      Color fillColorEnd,
      Color backgroundColor,
      Graphics graphics,
      Point location,
      string currentValueText)
    {
      Point point1 = new Point(location.X, location.Y);
      int width1 = overallWidth;
      int width2 = (int) ((double) currentValue / (double) maximumValue * (double) width1);
      if (width2 > width1)
        width2 = width1;
      Rectangle rect1 = new Rectangle(point1.X, point1.Y, width1, height);
      Rectangle rect2 = new Rectangle(point1.X, point1.Y, width2, height);
      Rectangle rect3 = new Rectangle(point1.X - 1, point1.Y, width2 + 2, height);
      using (SolidBrush solidBrush = new SolidBrush(backgroundColor))
      {
        graphics.FillRectangle((Brush) solidBrush, rect1);
        if (rect2.Width > 0)
        {
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect3, fillColorStart, fillColorEnd, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
            graphics.FillRectangle((Brush) linearGradientBrush, rect2);
        }
      }
      SizeF sizeF = graphics.MeasureString(currentValueText, this.Font);
      int width3 = (int) sizeF.Width;
      int height1 = (int) sizeF.Height;
      Point point2 = new Point(point1.X + (width1 - width3) / 2, point1.Y + (height - height1) / 2 + 2);
      graphics.DrawString(currentValueText, this.Font, (Brush) this._BlackBrush, new PointF((float) point2.X + 1f, (float) point2.Y + 1f));
      graphics.DrawString(currentValueText, this.Font, (Brush) this._WhiteBrush, new PointF((float) point2.X, (float) point2.Y));
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
