// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireSummaryPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class EmpireSummaryPanel : Panel
  {
    private EmpireSummary _EmpireSummary;
    private RaceList _Races;
    private RaceImageCache _RaceImageCache;
    private Font _LargeFont;
    private Font _NormalFont;
    private Font _BoldFont;
    private int _Margin = 5;
    private int _LineHeight = 12;
    private int _SpaceHeight = 6;
    private SolidBrush _Brush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private Bitmap _Image;

    public void ClearData()
    {
      this._EmpireSummary = (EmpireSummary) null;
      this._Races = (RaceList) null;
      this._RaceImageCache = (RaceImageCache) null;
      if (this._Image != null)
      {
        this._Image.Dispose();
        this._Image = (Bitmap) null;
      }
      this.Invalidate();
    }

    public void BindData(
      EmpireSummary empireSummary,
      RaceList races,
      RaceImageCache raceImageCache,
      Font largeFont,
      Font normalFont,
      Font boldFont)
    {
      this._LargeFont = largeFont;
      this._NormalFont = normalFont;
      this._BoldFont = boldFont;
      this._Races = races;
      this._RaceImageCache = raceImageCache;
      if (this._Image != null)
      {
        this._Image.Dispose();
        this._Image = (Bitmap) null;
      }
      this._EmpireSummary = empireSummary;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.Draw(e.Graphics);
    }

    private Bitmap GenerateCompositeEmpireImage(EmpireSummary empireSummary, int height)
    {
      Bitmap compositeEmpireImage = (Bitmap) null;
      if (empireSummary != null)
      {
        Race race = this._Races[empireSummary.RaceName];
        List<Bitmap> flagShapes = new List<Bitmap>();
        if (empireSummary.IsPirateFaction)
        {
          if (empireSummary.FlagIndex >= 0 && empireSummary.FlagIndex < Galaxy.FlagShapesPirates.Count)
            flagShapes = Galaxy.FlagShapesPirates;
        }
        else if (empireSummary.FlagIndex >= 0 && empireSummary.FlagIndex < Galaxy.FlagShapes.Count)
          flagShapes = Galaxy.FlagShapes;
        Bitmap smallFlagPicture = (Bitmap) null;
        Bitmap largeFlagPicture = (Bitmap) null;
        Galaxy.GenerateEmpireFlag(empireSummary.MainColor, empireSummary.SecondaryColor, empireSummary.FlagIndex, flagShapes, ref smallFlagPicture, ref largeFlagPicture);
        int width1 = height;
        int height1 = (int) ((double) height * 0.6);
        int width2 = height;
        int height2 = height;
        int num = height / 3;
        compositeEmpireImage = new Bitmap(width1 + width2 - num, height, PixelFormat.Format32bppPArgb);
        using (Graphics graphics = Graphics.FromImage((Image) compositeEmpireImage))
        {
          GraphicsHelper.SetGraphicsQualityToHigh(graphics);
          Rectangle srcRect = new Rectangle(0, 0, largeFlagPicture.Width, largeFlagPicture.Height);
          Rectangle destRect = new Rectangle(0, (height - height1) / 2, width1, height1);
          graphics.DrawImage((Image) largeFlagPicture, destRect, srcRect, GraphicsUnit.Pixel);
          if (race != null)
          {
            Bitmap raceImage = this._RaceImageCache.GetRaceImage(race.PictureRef);
            if (raceImage != null)
            {
              if (raceImage.PixelFormat != PixelFormat.Undefined)
              {
                srcRect = new Rectangle(0, 0, raceImage.Width, raceImage.Height);
                destRect = new Rectangle(width1 - num, (height - height2) / 2, width2, height2);
                graphics.DrawImage((Image) raceImage, destRect, srcRect, GraphicsUnit.Pixel);
              }
            }
          }
        }
      }
      return compositeEmpireImage;
    }

    private void Draw(Graphics graphics)
    {
      if (this._EmpireSummary == null)
        return;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(64, this._EmpireSummary.MainColor)))
        graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      int margin = this._Margin;
      SizeF maxSize = new SizeF((float) (this.Width - this._Margin * 2), (float) (this.Height - this._Margin * 2));
      if (this._Image == null)
        this._Image = this.GenerateCompositeEmpireImage(this._EmpireSummary, 120);
      if (this._Image != null)
      {
        int x = this.Width - (this._Margin + this._Image.Width);
        graphics.DrawImage((Image) this._Image, new Point(x, margin));
        int num = margin + this._Image.Height + this._SpaceHeight;
      }
      int y1 = this._LineHeight + this._SpaceHeight;
      using (SolidBrush solidBrush = new SolidBrush(this._EmpireSummary.MainColor))
        GraphicsHelper.DrawStringWithDropShadow(graphics, this._EmpireSummary.Name, this._LargeFont, new Point(this._Margin, y1), (Brush) solidBrush, maxSize);
      int y2 = y1 + this._LargeFont.Height;
      int y3;
      if (this._EmpireSummary.IsPirateFaction)
      {
        string str = Galaxy.ResolveDescription(this._EmpireSummary.PiratePlayStyle);
        string text = string.Format(TextResolver.GetText("Empire Summary Race Pirate Description"), (object) this._EmpireSummary.RaceName, (object) str);
        GraphicsHelper.DrawStringWithDropShadow(graphics, text, this._BoldFont, new Point(this._Margin, y2), (Brush) this._Brush, maxSize);
        y3 = y2 + this._LineHeight + this._LineHeight;
      }
      else
      {
        GovernmentAttributes governmentAttributes = (GovernmentAttributes) null;
        if (Galaxy.GovernmentsStatic.Count > this._EmpireSummary.GovernmentId && this._EmpireSummary.GovernmentId >= 0)
          governmentAttributes = Galaxy.GovernmentsStatic[this._EmpireSummary.GovernmentId];
        string text = string.Format(TextResolver.GetText("Empire Summary Race Government Description"), (object) this._EmpireSummary.RaceName, (object) governmentAttributes.Name);
        GraphicsHelper.DrawStringWithDropShadow(graphics, text, this._BoldFont, new Point(this._Margin, y2), (Brush) this._Brush, maxSize);
        y3 = y2 + this._LineHeight + this._LineHeight;
      }
      string text1 = string.Format(TextResolver.GetText("Empire Summary Colonies Description"), (object) this._EmpireSummary.ColonyCount.ToString(), (object) this._EmpireSummary.SystemCount.ToString(), (object) this._EmpireSummary.SpaceportCount.ToString());
      GraphicsHelper.DrawStringWithDropShadow(graphics, text1, this._NormalFont, new Point(this._Margin, y3), (Brush) this._Brush, maxSize);
      int y4 = y3 + this._LineHeight + this._SpaceHeight;
      string text2 = string.Format(TextResolver.GetText("Empire Summary Economy Description"), (object) this._EmpireSummary.Money.ToString("###,###,##0"), (object) this._EmpireSummary.Cashflow.ToString("###,###,##0"));
      GraphicsHelper.DrawStringWithDropShadow(graphics, text2, this._NormalFont, new Point(this._Margin, y4), (Brush) this._Brush, maxSize);
      int y5 = y4 + this._LineHeight + this._SpaceHeight + this._LineHeight + this._LineHeight;
      GraphicsHelper.DrawStringWithDropShadow(graphics, this._EmpireSummary.Description, this._NormalFont, new Point(this._Margin, y5), (Brush) this._Brush, maxSize);
    }
  }
}
