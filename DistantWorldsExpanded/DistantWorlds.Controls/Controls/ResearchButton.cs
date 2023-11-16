// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResearchButton
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ResearchButton : GlassButton
  {
    private Empire _Empire;
    private int _ImageSize = 14;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private Bitmap[] _ComponentImages;
    private Bitmap[] _HabitatTypeImages;
    private Bitmap[] _FighterImages;
    private Bitmap[] _FacilityImages;
    private Bitmap[] _TroopImagesInfantry;
    private Bitmap[] _TroopImagesArmored;
    private Bitmap[] _TroopImagesArtillery;
    private Bitmap[] _TroopImagesSpecialForces;
    private Bitmap _UpArrowImage;
    private Bitmap _ResearchImage;
    private Font _Font;
    private Font _BoldFont;

    public void InitializeImages(
      Bitmap[] componentImages,
      Bitmap[] fighterImages,
      Bitmap[] habitatTypeImages,
      Bitmap[] facilityImages,
      Bitmap upArrowImage,
      Bitmap researchImage,
      Bitmap[] troopImagesInfantry,
      Bitmap[] troopImagesArmored,
      Bitmap[] troopImagesArtillery,
      Bitmap[] troopImagesSpecialForces)
    {
      this.ClearImages();
      this.SetFonts();
      this._ComponentImages = this.ScaleLimitImages(componentImages, this._ImageSize, this._ImageSize);
      this._HabitatTypeImages = this.ScaleLimitImages(habitatTypeImages, this._ImageSize, this._ImageSize);
      this._FacilityImages = this.ScaleLimitImages(facilityImages, this._ImageSize, this._ImageSize);
      this._UpArrowImage = this.ScaleLimitImage(upArrowImage, this._ImageSize, this._ImageSize);
      this._ResearchImage = this.ScaleLimitImage(researchImage, 56, 56);
      this._TroopImagesInfantry = this.ScaleLimitImages(troopImagesInfantry, this._ImageSize, this._ImageSize);
      this._TroopImagesArmored = this.ScaleLimitImages(troopImagesArmored, this._ImageSize, this._ImageSize);
      this._TroopImagesArtillery = this.ScaleLimitImages(troopImagesArtillery, this._ImageSize, this._ImageSize);
      this._TroopImagesSpecialForces = this.ScaleLimitImages(troopImagesSpecialForces, this._ImageSize, this._ImageSize);
      this._FighterImages = new Bitmap[fighterImages.Length];
      for (int index = 0; index < fighterImages.Length; ++index)
      {
        Bitmap bitmap = this.ScaleLimitImage(fighterImages[index], this._ImageSize, this._ImageSize);
        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
        this._FighterImages[index] = bitmap;
      }
    }

    private void ClearImages()
    {
      this.ClearImageArray(this._ComponentImages);
      this.ClearImageArray(this._HabitatTypeImages);
      this.ClearImageArray(this._FacilityImages);
      this.ClearImageArray(this._FighterImages);
      this.ClearImageArray(this._TroopImagesInfantry);
      this.ClearImageArray(this._TroopImagesArmored);
      this.ClearImageArray(this._TroopImagesArtillery);
      this.ClearImageArray(this._TroopImagesSpecialForces);
      if (this._UpArrowImage != null)
      {
        this._UpArrowImage.Dispose();
        this._UpArrowImage = (Bitmap) null;
      }
      if (this._ResearchImage == null)
        return;
      this._ResearchImage.Dispose();
      this._ResearchImage = (Bitmap) null;
    }

    private void ClearImageArray(Bitmap[] imageArray)
    {
      if (imageArray == null)
        return;
      for (int index = 0; index < imageArray.Length; ++index)
      {
        if (imageArray[index] != null)
        {
          imageArray[index].Dispose();
          imageArray[index] = (Bitmap) null;
        }
      }
    }

    public void BindData(Empire empire)
    {
      this._Empire = empire;
      this.Image = (Image) null;
      this.SetFonts();
    }

    private void SetFonts()
    {
      if (this._Font != null)
        this._Font.Dispose();
      if (this._BoldFont != null)
        this._BoldFont.Dispose();
      this._Font = new Font(this.Font.FontFamily, 14f, FontStyle.Regular, GraphicsUnit.Pixel);
      this._BoldFont = new Font(this.Font.FontFamily, 14f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void Reset() => this._Empire = (Empire) null;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawResearchInfo(e.Graphics);
    }

    private void DrawResearchInfo(Graphics graphics)
    {
      if (!this.Enabled)
      {
        Bitmap bitmap = this.TransparentImage(this._ResearchImage, 0.33f);
        graphics.DrawImage((Image) bitmap, new Point(7, 9));
      }
      else
        graphics.DrawImage((Image) this._ResearchImage, new Point(7, 9));
      if (this._Empire == null || !this.Enabled)
        return;
      this.DrawIndustryRow(graphics, IndustryType.Weapon, new Point(3, 5));
      this.DrawIndustryRow(graphics, IndustryType.Energy, new Point(3, 23));
      this.DrawIndustryRow(graphics, IndustryType.HighTech, new Point(3, 41));
    }

    private void DrawIndustryRow(Graphics graphics, IndustryType industry, Point location)
    {
      Color color = Color.White;
      string empty = string.Empty;
      ResearchNode researchNode = (ResearchNode) null;
      switch (industry)
      {
        case IndustryType.Weapon:
          color = Color.FromArgb(192, (int) byte.MaxValue, 32, 64);
          if (this._Empire.Research.ResearchQueueWeapons != null && this._Empire.Research.ResearchQueueWeapons.Count > 0)
          {
            researchNode = this._Empire.Research.ResearchQueueWeapons[0];
            break;
          }
          break;
        case IndustryType.Energy:
          color = Color.FromArgb((int) byte.MaxValue, 96, 64, (int) byte.MaxValue);
          if (this._Empire.Research.ResearchQueueEnergy != null && this._Empire.Research.ResearchQueueEnergy.Count > 0)
          {
            researchNode = this._Empire.Research.ResearchQueueEnergy[0];
            break;
          }
          break;
        case IndustryType.HighTech:
          color = Color.FromArgb(192, 32, (int) byte.MaxValue, 64);
          if (this._Empire.Research.ResearchQueueHighTech != null && this._Empire.Research.ResearchQueueHighTech.Count > 0)
          {
            researchNode = this._Empire.Research.ResearchQueueHighTech[0];
            break;
          }
          break;
      }
      using (SolidBrush solidBrush = new SolidBrush(color))
      {
        Point point = new Point(location.X, location.Y);
        int num = 37;
        using (new SolidBrush(Color.FromArgb(64, color)))
        {
          Rectangle rectangle = new Rectangle(location.X + num - 2, location.Y, 36, 18);
        }
        if (researchNode != null)
        {
          Bitmap[] nodeImages = this.GenerateNodeImages(researchNode, 0.7f);
          if (nodeImages != null && nodeImages.Length > 0)
          {
            point = new Point(location.X + num, location.Y + (15 - nodeImages[0].Height) / 2);
            graphics.DrawImage((Image) nodeImages[0], point);
          }
          point = new Point(location.X + num + this._ImageSize - 1, location.Y);
          string text = (researchNode.Progress / researchNode.Cost).ToString("0%");
          this.DrawStringWithDropShadow(graphics, text, this._Font, point, (Brush) solidBrush);
        }
        else
        {
          point = new Point(location.X + num, location.Y);
          this.DrawStringWithDropShadow(graphics, "  -----", this._Font, point, (Brush) solidBrush);
        }
      }
    }

    private Bitmap[] GenerateNodeImages(ResearchNode researchNode) => this.GenerateNodeImages(researchNode, 1f);

    private Bitmap[] GenerateNodeImages(ResearchNode researchNode, float alpha)
    {
      List<Bitmap> bitmapList = new List<Bitmap>();
      if (researchNode.Components != null && researchNode.Components.Count > 0)
      {
        for (int index = 0; index < researchNode.Components.Count; ++index)
        {
          Bitmap bitmap = new Bitmap(this._ImageSize, this._ImageSize, PixelFormat.Format32bppPArgb);
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
          {
            Bitmap componentImage = this._ComponentImages[researchNode.Components[index].PictureRef];
            Point point = new Point((this._ImageSize - componentImage.Width) / 2, (this._ImageSize - componentImage.Height) / 2);
            graphics.DrawImage((Image) componentImage, point);
          }
          bitmapList.Add(bitmap);
        }
      }
      if (researchNode.ComponentImprovements != null && researchNode.ComponentImprovements.Count > 0)
      {
        for (int index = 0; index < researchNode.ComponentImprovements.Count; ++index)
          bitmapList.Add(this.GenerateImprovedComponentImage(this._ComponentImages[researchNode.ComponentImprovements[index].ImprovedComponent.PictureRef]));
      }
      if (researchNode.Abilities != null && researchNode.Abilities.Count > 0)
      {
        for (int index = 0; index < researchNode.Abilities.Count; ++index)
        {
          switch (researchNode.Abilities[index].Type)
          {
            case ResearchAbilityType.ConstructionSize:
              bitmapList.Add(this._ComponentImages[94]);
              break;
            case ResearchAbilityType.PopulationGrowthRate:
              bitmapList.Add(this.GenerateImprovedComponentImage(this._HabitatTypeImages[researchNode.Abilities[index].Value - 1]));
              break;
            case ResearchAbilityType.ColonizeHabitatType:
              bitmapList.Add(new Bitmap((Image) this._HabitatTypeImages[researchNode.Abilities[index].Value - 1]));
              break;
            case ResearchAbilityType.Troop:
              Bitmap componentImage1 = this._TroopImagesInfantry[0];
              if (researchNode.Abilities[index].RelatedObject != null && researchNode.Abilities[index].RelatedObject is TroopType && this._Empire != null && this._Empire.DominantRace != null)
              {
                switch ((TroopType) researchNode.Abilities[index].RelatedObject)
                {
                  case TroopType.Infantry:
                    componentImage1 = this._TroopImagesInfantry[this._Empire.DominantRace.PictureRef];
                    break;
                  case TroopType.Armored:
                    componentImage1 = this._TroopImagesArmored[this._Empire.DominantRace.PictureRef];
                    break;
                  case TroopType.Artillery:
                    componentImage1 = this._TroopImagesArtillery[this._Empire.DominantRace.PictureRef];
                    break;
                  case TroopType.SpecialForces:
                    componentImage1 = this._TroopImagesSpecialForces[this._Empire.DominantRace.PictureRef];
                    break;
                }
              }
              if (researchNode.Abilities[index].Value != 0)
                componentImage1 = this.GenerateImprovedComponentImage(componentImage1);
              bitmapList.Add(componentImage1);
              break;
            case ResearchAbilityType.Boarding:
              Bitmap bitmap = new Bitmap(this._ImageSize, this._ImageSize, PixelFormat.Format32bppPArgb);
              using (Graphics graphics = Graphics.FromImage((Image) bitmap))
              {
                Bitmap componentImage2 = this._ComponentImages[115];
                Point point = new Point((this._ImageSize - componentImage2.Width) / 2, (this._ImageSize - componentImage2.Height) / 2);
                graphics.DrawImage((Image) componentImage2, point);
              }
              bitmapList.Add(bitmap);
              break;
          }
        }
      }
      if (researchNode.Fighters != null && researchNode.Fighters.Count > 0)
      {
        for (int index1 = 0; index1 < researchNode.Fighters.Count; ++index1)
        {
          int index2 = researchNode.Fighters[index1].Type != FighterType.Bomber ? ShipImageHelper.ResolveNewFighterImageIndex(this._Empire.DominantRace, this._Empire.PirateEmpireBaseHabitat != null) : ShipImageHelper.ResolveNewBomberImageIndex(this._Empire.DominantRace, this._Empire.PirateEmpireBaseHabitat != null);
          bitmapList.Add(new Bitmap((Image) this._FighterImages[index2]));
        }
      }
      if (researchNode.PlanetaryFacility != null)
        bitmapList.Add(this._FacilityImages[(int) researchNode.PlanetaryFacility.PictureRef]);
      if ((double) alpha < 1.0)
      {
        for (int index = 0; index < bitmapList.Count; ++index)
        {
          if (bitmapList[index] != null)
            bitmapList[index] = this.TransparentImage(bitmapList[index], alpha);
        }
      }
      return bitmapList.ToArray();
    }

    private Bitmap GenerateImprovedComponentImage(Bitmap componentImage)
    {
      Bitmap improvedComponentImage = new Bitmap(this._ImageSize, this._ImageSize, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) improvedComponentImage))
      {
        int x = (this._ImageSize - componentImage.Width) / 2;
        int y = (this._ImageSize - componentImage.Height) / 2;
        graphics.DrawImage((Image) componentImage, new Point(x, y));
        Point point = new Point(0, Math.Max(0, (componentImage.Height - this._UpArrowImage.Height) / 2));
        graphics.DrawImage((Image) this._UpArrowImage, point);
      }
      return improvedComponentImage;
    }

    private Bitmap[] ScaleLimitImages(Bitmap[] images, int maxWidth, int maxHeight)
    {
      Bitmap[] bitmapArray = new Bitmap[images.Length];
      for (int index = 0; index < images.Length; ++index)
        bitmapArray[index] = this.ScaleLimitImage(images[index], maxWidth, maxHeight);
      return bitmapArray;
    }

    private Bitmap ScaleLimitImage(Bitmap image, int maxWidth, int maxHeight)
    {
      double num = Math.Min((double) maxWidth / (double) image.Width, (double) maxHeight / (double) image.Height);
      Bitmap bitmap;
      if (num < 1.0)
      {
        bitmap = new Bitmap((int) ((double) image.Width * num), (int) ((double) image.Height * num), PixelFormat.Format32bppPArgb);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
          Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
          graphics.DrawImage((Image) image, destRect, srcRect, GraphicsUnit.Pixel);
        }
      }
      else
        bitmap = new Bitmap((Image) image);
      return bitmap;
    }

    private Bitmap TransparentImage(Bitmap image, float alphaLevel)
    {
      Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        ImageAttributes attributesTransparency = this.CalculateImageAttributesTransparency(alphaLevel);
        graphics.DrawImage((Image) image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributesTransparency);
      }
      return bitmap;
    }

    private ImageAttributes CalculateImageAttributesTransparency(float alphaLevel)
    {
      ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
      {
        new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, alphaLevel, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
      });
      ImageAttributes attributesTransparency = new ImageAttributes();
      attributesTransparency.SetColorMatrix(newColorMatrix);
      return attributesTransparency;
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
      this.DrawStringWithDropShadow(graphics, text, font, location, brush, SizeF.Empty);
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      Brush brush,
      SizeF maxSize)
    {
      if (maxSize != SizeF.Empty)
      {
        location = new Point(location.X + 1, location.Y + 1);
        RectangleF layoutRectangle = new RectangleF((float) location.X, (float) location.Y, maxSize.Width, maxSize.Height);
        graphics.DrawString(text, font, (Brush) this._BlackBrush, layoutRectangle, StringFormat.GenericTypographic);
        location = new Point(location.X - 1, location.Y - 1);
        layoutRectangle = new RectangleF((float) location.X, (float) location.Y, maxSize.Width, maxSize.Height);
        graphics.DrawString(text, font, brush, layoutRectangle, StringFormat.GenericTypographic);
      }
      else
      {
        location = new Point(location.X + 1, location.Y + 1);
        graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location);
        location = new Point(location.X - 1, location.Y - 1);
        graphics.DrawString(text, font, brush, (PointF) location);
      }
    }
  }
}
