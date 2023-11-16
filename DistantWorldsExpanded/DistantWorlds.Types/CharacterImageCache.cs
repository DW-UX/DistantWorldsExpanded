// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CharacterImageCache
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DistantWorlds.Types
{
  public class CharacterImageCache
  {
    private object _LockObject = new object();
    private Hashtable _Images = new Hashtable();
    private Hashtable _ImagesSmall = new Hashtable();
    private Hashtable _ImagesVerySmall = new Hashtable();
    private Hashtable _LastUsage = new Hashtable();
    private string _ApplicationStartupPath;
    private string _CustomizationSetName;
    private RaceList _Races;
    private Bitmap[] _RaceImages;
    private Bitmap[] _CharacterRoleImages;

    public void Initialize(
      string applicationStartupPath,
      string customizationSetName,
      RaceList races,
      Bitmap[] raceImages,
      Bitmap[] characterRoleImages)
    {
      this.ClearAll();
      this._ApplicationStartupPath = applicationStartupPath;
      this._CustomizationSetName = customizationSetName;
      this._Races = races;
      this._RaceImages = raceImages;
      this._CharacterRoleImages = characterRoleImages;
    }

    public void ClearCharacterImages(Character character)
    {
      if (character == null)
        return;
      string characterImageKey = this.ObtainCharacterImageKey(character);
      if (string.IsNullOrEmpty(characterImageKey))
        return;
      object image = this._Images[(object) characterImageKey];
      if (image != null)
      {
        if (image is Bitmap)
          ((Image) image).Dispose();
        this._Images[(object) characterImageKey] = (object) null;
      }
      object obj = this._ImagesSmall[(object) characterImageKey];
      if (obj == null)
        return;
      if (obj is Bitmap)
        ((Image) obj).Dispose();
      this._ImagesSmall[(object) characterImageKey] = (object) null;
    }

    public Bitmap ObtainCharacterImage(Character character)
    {
      Bitmap image1 = (Bitmap) null;
      if (character != null)
      {
        string characterImageKey = this.ObtainCharacterImageKey(character);
        if (!string.IsNullOrEmpty(characterImageKey))
        {
          object image2 = this._Images[(object) characterImageKey];
          if (image2 != null)
          {
            if (image2 is Bitmap)
            {
              image1 = (Bitmap) image2;
              this._LastUsage[(object) characterImageKey] = (object) DateTime.Now;
            }
            else
            {
              Bitmap imageSmall = (Bitmap) null;
              Bitmap imageVerySmall = (Bitmap) null;
              this.CacheImage(character, out image1, out imageSmall, out imageVerySmall);
            }
          }
          else
          {
            Bitmap imageSmall = (Bitmap) null;
            Bitmap imageVerySmall = (Bitmap) null;
            this.CacheImage(character, out image1, out imageSmall, out imageVerySmall);
          }
        }
      }
      return image1;
    }

    public Bitmap ObtainCharacterImageSmall(Character character)
    {
      Bitmap imageSmall = (Bitmap) null;
      if (character != null)
      {
        string characterImageKey = this.ObtainCharacterImageKey(character);
        if (!string.IsNullOrEmpty(characterImageKey))
        {
          object obj = this._ImagesSmall[(object) characterImageKey];
          if (obj != null)
          {
            if (obj is Bitmap)
            {
              imageSmall = (Bitmap) obj;
              this._LastUsage[(object) characterImageKey] = (object) DateTime.Now;
            }
            else
            {
              Bitmap image = (Bitmap) null;
              Bitmap imageVerySmall = (Bitmap) null;
              this.CacheImage(character, out image, out imageSmall, out imageVerySmall);
            }
          }
          else
          {
            Bitmap image = (Bitmap) null;
            Bitmap imageVerySmall = (Bitmap) null;
            this.CacheImage(character, out image, out imageSmall, out imageVerySmall);
          }
        }
      }
      return imageSmall;
    }

    public Bitmap ObtainCharacterImageVerySmall(Character character)
    {
      Bitmap imageVerySmall = (Bitmap) null;
      if (character != null)
      {
        string characterImageKey = this.ObtainCharacterImageKey(character);
        if (!string.IsNullOrEmpty(characterImageKey))
        {
          object obj = this._ImagesVerySmall[(object) characterImageKey];
          if (obj != null)
          {
            if (obj is Bitmap)
            {
              imageVerySmall = (Bitmap) obj;
              this._LastUsage[(object) characterImageKey] = (object) DateTime.Now;
            }
            else
            {
              Bitmap image = (Bitmap) null;
              Bitmap imageSmall = (Bitmap) null;
              this.CacheImage(character, out image, out imageSmall, out imageVerySmall);
            }
          }
          else
          {
            Bitmap image = (Bitmap) null;
            Bitmap imageSmall = (Bitmap) null;
            this.CacheImage(character, out image, out imageSmall, out imageVerySmall);
          }
        }
      }
      return imageVerySmall;
    }

    public string ObtainCharacterImageKey(Character character)
    {
      if (character == null)
        return string.Empty;
      if (!string.IsNullOrEmpty(character.PictureFilename))
        return character.PictureFilename;
      return character.Race != null ? character.Race.Name + Galaxy.ResolveDescription(character.Role) : Galaxy.ResolveDescription(character.Role);
    }

    private void CacheImage(
      Character character,
      out Bitmap image,
      out Bitmap imageSmall,
      out Bitmap imageVerySmall)
    {
      imageSmall = (Bitmap) null;
      imageVerySmall = (Bitmap) null;
      int num1 = 38;
      int num2 = 13;
      if (!string.IsNullOrEmpty(character.PictureFilename))
      {
        image = this.LoadImage(character);
        if (image == null)
          return;
        imageSmall = GraphicsHelper.ScaleLimitImage(image, num1, num1, 1f);
        imageSmall = this.OverlayRoleIcon(character, imageSmall, 0.35, 1);
        imageVerySmall = GraphicsHelper.ScaleLimitImage(image, num2, num2, 1f);
        imageVerySmall = this.OverlayRoleIcon(character, imageVerySmall, 0.48, 0);
        image = this.OverlayRoleIcon(character, image, 0.2, 20);
        this._Images[(object) character.PictureFilename] = (object) image;
        this._ImagesSmall[(object) character.PictureFilename] = (object) imageSmall;
        this._ImagesVerySmall[(object) character.PictureFilename] = (object) imageVerySmall;
        this._LastUsage[(object) character.PictureFilename] = (object) DateTime.Now;
      }
      else
      {
        string characterImageKey = this.ObtainCharacterImageKey(character);
        image = character.Race == null ? new Bitmap(200, 200, PixelFormat.Format32bppPArgb) : new Bitmap((Image) this._RaceImages[character.Race.PictureRef]);
        if (image == null)
          return;
        imageSmall = GraphicsHelper.ScaleLimitImage(image, num1, num1, 1f);
        imageSmall = this.OverlayRoleIcon(character, imageSmall, 0.35, 1);
        imageVerySmall = GraphicsHelper.ScaleLimitImage(image, num2, num2, 1f);
        imageVerySmall = this.OverlayRoleIcon(character, imageVerySmall, 0.48, 0);
        image = this.OverlayRoleIcon(character, image, 0.2, 20);
        this._Images[(object) characterImageKey] = (object) image;
        this._ImagesSmall[(object) characterImageKey] = (object) imageSmall;
        this._ImagesVerySmall[(object) characterImageKey] = (object) imageVerySmall;
        this._LastUsage[(object) characterImageKey] = (object) DateTime.Now;
      }
    }

    private Bitmap LoadImage(Character character)
    {
      Bitmap bitmap = (Bitmap) null;
      if (character != null && !string.IsNullOrEmpty(character.PictureFilename))
      {
        string str = this._ApplicationStartupPath + "\\images\\units\\characters\\" + character.PictureFilename;
        string empty = string.Empty;
        if (!string.IsNullOrEmpty(this._CustomizationSetName))
        {
          string path = this._ApplicationStartupPath + "\\customization\\" + this._CustomizationSetName + "\\images\\units\\characters\\" + character.PictureFilename;
          if (File.Exists(path))
            str = path;
        }
        if (File.Exists(str))
          bitmap = this.SafeLoadImage(str);
      }
      return bitmap;
    }

    private Bitmap SafeLoadImage(string imagePath)
    {
      Bitmap bitmap = (Bitmap) null;
      if (File.Exists(imagePath))
      {
        try
        {
          bitmap = GraphicsHelper.LoadImageFromFilePath(imagePath);
        }
        catch (Exception ex)
        {
          bitmap = (Bitmap) null;
        }
      }
      return bitmap;
    }

    public Bitmap GetRoleIcon(CharacterRole role)
    {
      Bitmap roleIcon = (Bitmap) null;
      switch (role)
      {
        case CharacterRole.Leader:
          roleIcon = this._CharacterRoleImages[0];
          break;
        case CharacterRole.Ambassador:
          roleIcon = this._CharacterRoleImages[1];
          break;
        case CharacterRole.ColonyGovernor:
          roleIcon = this._CharacterRoleImages[2];
          break;
        case CharacterRole.FleetAdmiral:
          roleIcon = this._CharacterRoleImages[3];
          break;
        case CharacterRole.TroopGeneral:
          roleIcon = this._CharacterRoleImages[4];
          break;
        case CharacterRole.IntelligenceAgent:
          roleIcon = this._CharacterRoleImages[5];
          break;
        case CharacterRole.Scientist:
          roleIcon = this._CharacterRoleImages[6];
          break;
        case CharacterRole.PirateLeader:
          roleIcon = this._CharacterRoleImages[7];
          break;
        case CharacterRole.ShipCaptain:
          roleIcon = this._CharacterRoleImages[8];
          break;
      }
      return roleIcon;
    }

    private Bitmap OverlayRoleIcon(
      Character character,
      Bitmap image,
      double iconSizeRatio,
      int minimumEdgeOffset)
    {
      Bitmap bitmap1 = new Bitmap((Image) image);
      if (character != null)
      {
        using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
        {
          GraphicsHelper.SetGraphicsQualityToHigh(graphics);
          Bitmap bitmap2 = (Bitmap) null;
          switch (character.Role)
          {
            case CharacterRole.Leader:
              bitmap2 = this._CharacterRoleImages[0];
              break;
            case CharacterRole.Ambassador:
              bitmap2 = this._CharacterRoleImages[1];
              break;
            case CharacterRole.ColonyGovernor:
              bitmap2 = this._CharacterRoleImages[2];
              break;
            case CharacterRole.FleetAdmiral:
              bitmap2 = this._CharacterRoleImages[3];
              break;
            case CharacterRole.TroopGeneral:
              bitmap2 = this._CharacterRoleImages[4];
              break;
            case CharacterRole.IntelligenceAgent:
              bitmap2 = this._CharacterRoleImages[5];
              break;
            case CharacterRole.Scientist:
              bitmap2 = this._CharacterRoleImages[6];
              break;
            case CharacterRole.PirateLeader:
              bitmap2 = this._CharacterRoleImages[7];
              break;
            case CharacterRole.ShipCaptain:
              bitmap2 = this._CharacterRoleImages[8];
              break;
          }
          if (bitmap2 != null)
          {
            Rectangle srcRect = new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
            int width = Math.Max(1, (int) ((double) image.Width * iconSizeRatio));
            int num1 = minimumEdgeOffset;
            double num2 = (double) width / (double) bitmap2.Width;
            int height = Math.Max(1, (int) ((double) bitmap2.Height * num2));
            Rectangle destRect = new Rectangle(image.Width - (width + num1), image.Height - (height + num1), width, height);
            graphics.DrawImage((Image) bitmap2, destRect, srcRect, GraphicsUnit.Pixel);
          }
        }
      }
      return bitmap1;
    }

    private void ClearImageArray(Bitmap[] images)
    {
      if (images != null)
      {
        for (int index = 0; index < images.Length; ++index)
        {
          if (images[index] != null)
          {
            if (images[index].PixelFormat != PixelFormat.Undefined)
              images[index].Dispose();
            images[index] = (Bitmap) null;
          }
        }
      }
      images = (Bitmap[]) null;
    }

    public void ClearCharacterImageCache()
    {
      foreach (string key in (IEnumerable) this._Images.Keys)
      {
        object image = this._Images[(object) key];
        if (image != null && image is Bitmap)
          ((Image) image).Dispose();
      }
      this._Images.Clear();
      foreach (string key in (IEnumerable) this._ImagesSmall.Keys)
      {
        object obj = this._ImagesSmall[(object) key];
        if (obj != null && obj is Bitmap)
          ((Image) obj).Dispose();
      }
      this._ImagesSmall.Clear();
    }

    public void ClearAll()
    {
      this.ClearCharacterImageCache();
      this._Races = (RaceList) null;
      this._ApplicationStartupPath = string.Empty;
      this._CustomizationSetName = string.Empty;
    }

    public void ClearOldImages() => this.ClearOldImages(1200);

    public void ClearOldImages(int maximumAgeInSeconds)
    {
      DateTime dateTime1 = DateTime.Now.Subtract(new TimeSpan(0, 0, maximumAgeInSeconds));
      List<string> stringList = new List<string>();
      lock (this._LockObject)
      {
        foreach (string key in (IEnumerable) this._Images.Keys)
        {
          if (this._LastUsage[(object) key] is DateTime dateTime2 && dateTime2 < dateTime1)
          {
            object image = this._Images[(object) key];
            if (image != null && image is Bitmap)
              ((Image) image).Dispose();
            stringList.Add(key);
          }
        }
        for (int index = 0; index < stringList.Count; ++index)
        {
          this._Images.Remove((object) stringList[index]);
          this._LastUsage.Remove((object) stringList[index]);
        }
      }
    }
  }
}
