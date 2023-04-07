// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxyNebulaeGenerator
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DistantWorlds.Types
{
  public class GalaxyNebulaeGenerator
  {
    private Random _Rnd;
    private Bitmap _GalaxyBackground;
    private Bitmap[] _CloudImages;
    private List<string> _SystemNames;
    private List<Color> _CloudColors;
    private int _ColorChannelBoost = 15;
    private bool _GenerateImage = true;
    private FbmNoise _Fbm;
    private byte[][] _Noise;

    public GalaxyNebulaeGenerator(
      Bitmap[] cloudImages,
      List<string> systemNames,
      List<Color> cloudColors)
    {
      this._CloudImages = cloudImages;
      this._SystemNames = systemNames;
      this._CloudColors = cloudColors;
    }

    private ImageAttributes CalculateImageAttributes(Color tintColor)
    {
      float num1 = (float) tintColor.R / (float) byte.MaxValue;
      float num2 = (float) tintColor.G / (float) byte.MaxValue;
      float num3 = (float) tintColor.B / (float) byte.MaxValue;
      ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
      {
        new float[5]{ num1, num2, num3, 0.0f, 0.0f },
        new float[5]{ num1, num2, num3, 0.0f, 0.0f },
        new float[5]{ num1, num2, num3, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 1f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
      });
      ImageAttributes imageAttributes = new ImageAttributes();
      imageAttributes.SetColorMatrix(newColorMatrix);
      return imageAttributes;
    }

    private ImageAttributes CalculateImageAttributesFullTint(Color tintColor)
    {
      ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
      {
        new float[5],
        new float[5],
        new float[5],
        new float[5]{ 0.0f, 0.0f, 0.0f, 1f, 0.0f },
        new float[5]
        {
          (float) tintColor.R / (float) byte.MaxValue,
          (float) tintColor.G / (float) byte.MaxValue,
          (float) tintColor.B / (float) byte.MaxValue,
          0.0f,
          1f
        }
      });
      ImageAttributes attributesFullTint = new ImageAttributes();
      attributesFullTint.SetColorMatrix(newColorMatrix);
      return attributesFullTint;
    }

    private Bitmap TintBitmap(Bitmap inputImage, Color tintColor) => this.TintBitmap(inputImage, tintColor, false);

    private Bitmap TintBitmap(Bitmap inputImage, Color tintColor, bool fullTint)
    {
      ImageAttributes imageAttr = !fullTint ? this.CalculateImageAttributes(tintColor) : this.CalculateImageAttributesFullTint(tintColor);
      Bitmap bitmap = new Bitmap((Image) inputImage);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      Rectangle destRect = new Rectangle(0, 0, inputImage.Width, inputImage.Height);
      graphics.DrawImage((Image) inputImage, destRect, 0, 0, inputImage.Width, inputImage.Height, GraphicsUnit.Pixel, imageAttr);
      return bitmap;
    }

    private int SelectRandomCloudIndex() => this._Rnd.Next(0, this._CloudImages.Length);

    private Bitmap SelectCloudImage(int cloudImageIndex) => this._CloudImages[cloudImageIndex];

    private Bitmap SelectRandomCloudImage() => this._CloudImages[this.SelectRandomCloudIndex()];

    private Point ResolveGalaxyCoordsToImagePoint(double x, double y)
    {
      double num = (double) Galaxy.SizeX / (double) this._GalaxyBackground.Width;
      int val2_1 = (int) (x / num);
      int val2_2 = (int) (y / num);
      return new Point(Math.Min(this._GalaxyBackground.Width - 1, Math.Max(0, val2_1)), Math.Min(this._GalaxyBackground.Height - 1, Math.Max(0, val2_2)));
    }

    private Rectangle ConvertLocationToRectangle(GalaxyLocation location, double galaxyScaleFactor)
    {
      double num1 = 1.4;
      double num2 = galaxyScaleFactor / num1;
      int num3 = (int) ((double) location.Width / galaxyScaleFactor);
      int num4 = (int) ((double) location.Height / galaxyScaleFactor);
      int width = (int) ((double) location.Width / num2);
      int height = (int) ((double) location.Height / num2);
      int num5 = (width - num3) / 2;
      int num6 = (height - num4) / 2;
      return new Rectangle((int) ((double) location.Xpos / galaxyScaleFactor) - num5, (int) ((double) location.Ypos / galaxyScaleFactor) - num6, width, height);
    }

    private GalaxyLocation GenerateNebulaCloud(int x, int y, int minSize, int maxSize) => this.GenerateNebulaCloud(x, y, minSize, maxSize, string.Empty, false);

    private GalaxyLocationEffectType SelectEffect()
    {
      GalaxyLocationEffectType locationEffectType = GalaxyLocationEffectType.None;
      switch (this._Rnd.Next(0, 2))
      {
        case 0:
          locationEffectType = GalaxyLocationEffectType.LightningDamage;
          break;
        case 1:
          locationEffectType = GalaxyLocationEffectType.ShieldReduction;
          break;
      }
      return locationEffectType;
    }

    private GalaxyLocation GenerateNebulaCloud(
      int x,
      int y,
      int minSize,
      int maxSize,
      string name,
      bool showName)
    {
      int num = this._Rnd.Next(minSize, maxSize);
      double x1 = (double) (x - num / 2);
      double y1 = (double) (y - num / 2);
      int pictureRef = this.SelectRandomCloudIndex();
      return new GalaxyLocation(name, GalaxyLocationType.NebulaCloud, x1, y1, (double) num, (double) num, pictureRef)
      {
        ShowName = showName,
        Effect = GalaxyLocationEffectType.MovementSlowed
      };
    }

    private string GenerateCodeName() => ((char) this._Rnd.Next(65, 91)).ToString() + ((char) this._Rnd.Next(65, 91)).ToString() + this._Rnd.Next(1, 1000).ToString();

    private string GenerateArmName() => this._SystemNames[this._Rnd.Next(0, this._SystemNames.Count)] + " Arm";

    private string GenerateCloudName(GalaxyLocationEffectType effectType) => this.GenerateCloudName(effectType, string.Empty);

    private string GenerateCloudName(GalaxyLocationEffectType effectType, string lastName)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string[] strArray = new string[11]
      {
        "Cluster",
        "Cloud",
        "Expanse",
        "Nebula",
        "Murk",
        "Gloom",
        "Fog",
        "Zone",
        "Region",
        "Drift",
        "Corridor"
      };
      switch (effectType)
      {
        case GalaxyLocationEffectType.HyperjumpDisabled:
          strArray = new string[7]
          {
            "Mire",
            "Halt",
            "Labyrinth",
            "Morass",
            "Maze",
            "Impasse",
            "Deep"
          };
          break;
        case GalaxyLocationEffectType.LightningDamage:
          strArray = new string[6]
          {
            "Storm",
            "Tempest",
            "Typhoon",
            "Squall",
            "Turmoil",
            "Maelstrom"
          };
          break;
        case GalaxyLocationEffectType.ShieldReduction:
          strArray = new string[3]
          {
            "Desolation",
            "Void",
            "Waste"
          };
          break;
      }
      string systemName = this._SystemNames[this._Rnd.Next(0, this._SystemNames.Count)];
      int index = this._Rnd.Next(0, strArray.Length);
      if (string.IsNullOrEmpty(lastName))
        lastName = strArray[index];
      return systemName + " " + lastName;
    }

    private bool CheckOverlapExistingLocation(
      GalaxyLocation newLocation,
      GalaxyLocationList existingLocations)
    {
      Rectangle rect = new Rectangle((int) newLocation.Xpos, (int) newLocation.Ypos, (int) newLocation.Width, (int) newLocation.Height);
      foreach (GalaxyLocation existingLocation in (SyncList<GalaxyLocation>) existingLocations)
      {
        if (new Rectangle((int) existingLocation.Xpos, (int) existingLocation.Ypos, (int) existingLocation.Width, (int) existingLocation.Height).IntersectsWith(rect))
          return true;
      }
      return false;
    }

    private bool CheckWithinGalaxyBounds(GalaxyLocation newLocation) => (double) newLocation.Xpos >= 0.0 && (double) newLocation.Ypos >= 0.0 && (double) newLocation.Xpos + (double) newLocation.Width <= (double) Galaxy.SizeX && (double) newLocation.Ypos + (double) newLocation.Height <= (double) Galaxy.SizeY;

    private Color DetermineLocationColor(GalaxyLocation location)
    {
      Color color1 = this.SelectCloudColor();
      Color color2 = this.SampleColorAtPoint((double) location.Xpos + (double) location.Width / 2.0, (double) location.Ypos + (double) location.Height / 2.0);
      int num1 = ((int) color2.R + (int) color1.R) / 2;
      int num2 = ((int) color2.G + (int) color1.G) / 2;
      int num3 = ((int) color2.B + (int) color1.B) / 2;
      double num4 = 1.0;
      return Color.FromArgb(Math.Min((int) byte.MaxValue, (int) ((double) num1 * num4)), Math.Min((int) byte.MaxValue, (int) ((double) num2 * num4)), Math.Min((int) byte.MaxValue, (int) ((double) num3 * num4)));
    }

    private Color SampleColorAtPoint(double x, double y)
    {
      Point imagePoint = this.ResolveGalaxyCoordsToImagePoint(x, y);
      return this._GalaxyBackground.GetPixel(imagePoint.X, imagePoint.Y);
    }

    public Bitmap GenerateGalaxyNebulae(
      bool generateImage,
      int randomSeed,
      int starCount,
      GalaxyShape galaxyShape,
      int width,
      int height,
      Bitmap galaxyBackground,
      out GalaxyLocationList locations)
    {
      this._Rnd = new Random(randomSeed);
      this._GenerateImage = generateImage;
      this._GalaxyBackground = galaxyBackground;
      if (this._GenerateImage)
      {
        this._Fbm = new FbmNoise(randomSeed);
        this._Noise = this._Fbm.MakeTurbulence(512, 0.4, 2.5);
        this._Noise = this._Fbm.TileNoise(this._Noise, 48);
      }
      int sizeX = Galaxy.SizeX;
      int sizeY = Galaxy.SizeY;
      double galaxyScaleFactor = (double) sizeX / (double) width;
      int num1 = sizeX / 100;
      int num2 = sizeX / 60;
      int minSize1 = sizeX / 35;
      int maxSize1 = sizeX / 15;
      int minSize2 = sizeX / 30;
      int maxSize2 = sizeX / 15;
      int maxValue = sizeX / 30;
      int maximumCloudSize = sizeX / 10;
      width = Math.Max(1, width);
      height = Math.Max(1, height);
      Bitmap image = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      Graphics graphics = Graphics.FromImage((Image) image);
      GraphicsHelper.SetGraphicsQualityToHigh(graphics);
      locations = galaxyShape == GalaxyShape.Elliptical || galaxyShape == GalaxyShape.Spiral ? this.DrawSpiralArms(graphics, sizeX, sizeY, maximumCloudSize, galaxyScaleFactor) : new GalaxyLocationList();
      int num3 = this._Rnd.Next(20, 40);
      for (int index1 = 0; index1 < num3; ++index1)
      {
        GalaxyLocationList items = new GalaxyLocationList();
        bool flag = true;
        int num4 = this._Rnd.Next(3, 10);
        int num5 = (num4 - 1) / 2;
        int num6 = this._Rnd.Next(0, sizeX);
        int num7 = this._Rnd.Next(0, sizeY);
        string cloudName = this.GenerateCloudName(GalaxyLocationEffectType.None);
        for (int index2 = 0; index2 < num4; ++index2)
        {
          int num8 = this._Rnd.Next(-maxValue, maxValue);
          int num9 = this._Rnd.Next(-maxValue, maxValue);
          int x = num6 + num8;
          int y = num7 + num9;
          string name = cloudName;
          GalaxyLocationEffectType effectType = GalaxyLocationEffectType.None;
          bool showName = false;
          if (this._Rnd.Next(0, 15) == 1)
          {
            effectType = this.SelectEffect();
            name = this.GenerateCloudName(effectType);
            showName = true;
            if (index2 == num5)
              ++num5;
          }
          if (index2 == num5)
            showName = true;
          GalaxyLocation nebulaCloud = this.GenerateNebulaCloud(x, y, minSize2, maxSize2, name, showName);
          if (effectType != GalaxyLocationEffectType.None)
            nebulaCloud.Effect = effectType;
          if (!this.CheckWithinGalaxyBounds(nebulaCloud))
          {
            ++num5;
          }
          else
          {
            items.Add(nebulaCloud);
            if (this.CheckOverlapExistingLocation(nebulaCloud, locations))
            {
              flag = false;
              break;
            }
          }
        }
        if (flag)
        {
          locations.AddRange((IEnumerable<GalaxyLocation>) items);
          if (this._GenerateImage)
          {
            foreach (GalaxyLocation location in (SyncList<GalaxyLocation>) items)
            {
              Bitmap bitmap = this.TintBitmap(this.OrientNebulaCloudFromGalaxyCenter(this.SelectCloudImage((int) location.PictureRef), location), this.DetermineLocationColor(location), true);
              Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
              Rectangle rectangle = this.ConvertLocationToRectangle(location, galaxyScaleFactor);
              graphics.DrawImage((Image) bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
            }
          }
        }
      }
      int num10 = this._Rnd.Next(40, 60);
      for (int index = 0; index < num10; ++index)
      {
        int x = this._Rnd.Next(0, sizeX);
        int y = this._Rnd.Next(0, sizeY);
        string cloudName = this.GenerateCloudName(GalaxyLocationEffectType.None);
        GalaxyLocationEffectType effectType = GalaxyLocationEffectType.None;
        if (this._Rnd.Next(0, 10) == 1)
        {
          effectType = this.SelectEffect();
          cloudName = this.GenerateCloudName(effectType);
        }
        GalaxyLocation nebulaCloud = this.GenerateNebulaCloud(x, y, minSize1, maxSize1, cloudName, true);
        if (effectType != GalaxyLocationEffectType.None)
          nebulaCloud.Effect = effectType;
        if (!this.CheckOverlapExistingLocation(nebulaCloud, locations) && this.CheckWithinGalaxyBounds(nebulaCloud))
        {
          locations.Add(nebulaCloud);
          if (this._GenerateImage)
          {
            Bitmap bitmap = this.TintBitmap(this.OrientNebulaCloudFromGalaxyCenter(this.SelectCloudImage((int) nebulaCloud.PictureRef), nebulaCloud), this.DetermineLocationColor(nebulaCloud), true);
            Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Rectangle rectangle = this.ConvertLocationToRectangle(nebulaCloud, galaxyScaleFactor);
            graphics.DrawImage((Image) bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
          }
        }
      }
      if (this._GenerateImage)
        this._Fbm.ApplyNoiseToImageTransparent(image, new Rectangle(0, 0, image.Width, image.Height), this._Noise, (double) byte.MaxValue);
      return image;
    }

    private Bitmap RotateBitmap(Bitmap InputImage, double angle)
    {
      double width = (double) InputImage.Width;
      double height = (double) InputImage.Height;
      Point[] pointArray = new Point[4]
      {
        new Point(0, 0),
        new Point((int) width, 0),
        new Point(0, (int) height),
        new Point((int) width, (int) height)
      };
      double num1 = width / 2.0;
      double num2 = height / 2.0;
      for (int index = 0; index <= 3; ++index)
      {
        pointArray[index].X -= (int) num1;
        pointArray[index].Y -= (int) num2;
      }
      double num3 = angle;
      double num4 = Math.Sin(num3);
      double num5 = Math.Cos(num3);
      for (int index = 0; index <= 3; ++index)
      {
        double x = (double) pointArray[index].X;
        double y = (double) pointArray[index].Y;
        pointArray[index].X = (int) (x * num5 + y * num4);
        pointArray[index].Y = (int) (-x * num4 + y * num5);
      }
      double x1 = (double) pointArray[0].X;
      double y1 = (double) pointArray[0].Y;
      for (int index = 1; index <= 3; ++index)
      {
        if (x1 > (double) pointArray[index].X)
          x1 = (double) pointArray[index].X;
        if (y1 > (double) pointArray[index].Y)
          y1 = (double) pointArray[index].Y;
      }
      for (int index = 0; index <= 3; ++index)
      {
        pointArray[index].X -= (int) x1;
        pointArray[index].Y -= (int) y1;
      }
      Bitmap bitmap = new Bitmap(Math.Max(1, (int) (-2.0 * x1)), Math.Max(1, (int) (-2.0 * y1)), PixelFormat.Format32bppPArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      Point[] destPoints = new Point[3]
      {
        pointArray[0],
        pointArray[1],
        pointArray[2]
      };
      graphics.DrawImage((Image) InputImage, destPoints);
      return bitmap;
    }

    internal Bitmap RotateImage(Image image, float angle, bool highQuality)
    {
      float num = image != null ? (float) image.Width : throw new ArgumentNullException(nameof (image));
      float height1 = (float) image.Height;
      angle *= -1f;
      angle *= 57.29578f;
      angle %= 360f;
      while ((double) angle < 0.0)
        angle += 360f;
      PointF[] pts = new PointF[4];
      pts[1].X = num;
      pts[2].X = num;
      pts[2].Y = height1;
      pts[3].Y = height1;
      Matrix matrix = new Matrix();
      PointF point1 = new PointF(num / 2f, height1 / 2f);
      matrix.RotateAt(angle, point1, MatrixOrder.Append);
      matrix.TransformPoints(pts);
      double val1_1 = double.MinValue;
      double val1_2 = double.MinValue;
      double val1_3 = double.MaxValue;
      double val1_4 = double.MaxValue;
      foreach (PointF pointF in pts)
      {
        val1_1 = Math.Max(val1_1, (double) pointF.X);
        val1_3 = Math.Min(val1_3, (double) pointF.X);
        val1_2 = Math.Max(val1_2, (double) pointF.Y);
        val1_4 = Math.Min(val1_4, (double) pointF.Y);
      }
      double val2_1 = Math.Ceiling(val1_1 - val1_3);
      double val2_2 = Math.Ceiling(val1_2 - val1_4);
      double width = Math.Max(1.0, val2_1);
      double height2 = Math.Max(1.0, val2_2);
      Bitmap bitmap = new Bitmap((int) width, (int) height2, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        if (highQuality)
        {
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
        else
        {
          graphics.CompositingQuality = CompositingQuality.HighSpeed;
          graphics.InterpolationMode = InterpolationMode.Bilinear;
          graphics.SmoothingMode = SmoothingMode.HighSpeed;
        }
        PointF point2 = new PointF((float) (width / 2.0), (float) (height2 / 2.0));
        PointF point3 = new PointF(point2.X - num / 2f, point2.Y - height1 / 2f);
        matrix.Reset();
        matrix.RotateAt(angle, point2, MatrixOrder.Append);
        graphics.Transform = matrix;
        graphics.DrawImage(image, point3);
      }
      return bitmap;
    }

    private Bitmap OrientNebulaCloudFromGalaxyCenter(Bitmap image, GalaxyLocation location)
    {
      float angle = (float) Galaxy.DetermineAngle((double) location.Xpos + (double) location.Width / 2.0, (double) location.Ypos + (double) location.Height / 2.0, (double) (Galaxy.SizeX / 2), (double) (Galaxy.SizeY / 2)) - 1.57079637f;
      return this.RotateBitmap(image, (double) angle);
    }

    public GalaxyLocationList DrawSpiralArms(
      Graphics graphics,
      int width,
      int height,
      int maximumCloudSize,
      double galaxyScaleFactor)
    {
      GalaxyLocationList galaxyLocationList = new GalaxyLocationList();
      Size size = new Size(width, height);
      double x = (double) (size.Width / 2);
      double y = (double) (size.Height / 2);
      double startRadius = (double) (size.Width / 27);
      galaxyLocationList.AddRange((IEnumerable<GalaxyLocation>) this.DrawSpiralArm(graphics, 0.0, startRadius, x, y, 0, 1, maximumCloudSize, galaxyScaleFactor));
      galaxyLocationList.AddRange((IEnumerable<GalaxyLocation>) this.DrawSpiralArm(graphics, Math.PI, startRadius, x, y, 0, -1, maximumCloudSize, galaxyScaleFactor));
      return galaxyLocationList;
    }

    public GalaxyLocationList DrawSpiralArm(
      Graphics graphics,
      double startAngle,
      double startRadius,
      double x,
      double y,
      int offsetChangeX,
      int offsetChangeY,
      int maximumCloudSize,
      double galaxyScaleFactor)
    {
      GalaxyLocationList galaxyLocationList = new GalaxyLocationList();
      string armName = this.GenerateArmName();
      int num1 = 3;
      int num2 = num1;
      int num3 = 0;
      double num4 = 1.6;
      double num5 = startRadius;
      double num6 = Math.PI / 2.0;
      double num7 = startAngle;
      double num8 = startAngle;
      double num9 = startAngle - 33.0 * Math.PI / 10.0;
      while (num8 > num9)
      {
        double num10 = num5 / startRadius;
        double num11 = 3.0 * Math.PI / 10.0 / num10;
        double num12 = 9.0 * Math.PI / 20.0 / num10;
        if (this._Rnd.Next(0, 5) == 1)
          num12 = 3.0 * Math.PI / 4.0 / num10;
        double num13 = num12 - num11;
        double num14 = num11 + this._Rnd.NextDouble() * num13;
        num8 -= num14;
        if (num8 <= num7 - num6)
        {
          double num15 = num5 * num4;
          double num16 = num15 - num5;
          num5 = num15;
          num7 -= num6;
          x += (double) offsetChangeX * num16;
          y += (double) offsetChangeY * num16;
          switch (offsetChangeX)
          {
            case -1:
              offsetChangeX = 0;
              offsetChangeY = 1;
              break;
            case 1:
              offsetChangeX = 0;
              offsetChangeY = -1;
              break;
            default:
              switch (offsetChangeY)
              {
                case -1:
                  offsetChangeX = -1;
                  offsetChangeY = 0;
                  break;
                case 1:
                  offsetChangeX = 1;
                  offsetChangeY = 0;
                  break;
              }
              break;
          }
        }
        int x1 = (int) (x + Math.Cos(num8) * num5);
        int y1 = (int) (y + Math.Sin(num8) * num5);
        int num17 = (int) ((double) maximumCloudSize * (0.6 + this._Rnd.NextDouble() * 0.4));
        string name = armName;
        GalaxyLocationEffectType effectType = GalaxyLocationEffectType.None;
        bool showName = false;
        if (this._Rnd.Next(0, 15) == 1)
        {
          effectType = this.SelectEffect();
          name = this.GenerateCloudName(effectType);
          showName = true;
          if (num3 == num2)
            ++num2;
        }
        if (num3 == num2)
        {
          showName = true;
          num2 += num1;
        }
        GalaxyLocation nebulaCloud = this.GenerateNebulaCloud(x1, y1, num17, num17, name, showName);
        if (effectType != GalaxyLocationEffectType.None)
          nebulaCloud.Effect = effectType;
        if (this.CheckWithinGalaxyBounds(nebulaCloud))
        {
          galaxyLocationList.Add(nebulaCloud);
          if (this._GenerateImage)
          {
            Bitmap bitmap = this.TintBitmap(this.OrientNebulaCloudFromGalaxyCenter(this.SelectCloudImage((int) nebulaCloud.PictureRef), nebulaCloud), this.DetermineLocationColor(nebulaCloud), true);
            Rectangle rectangle = this.ConvertLocationToRectangle(nebulaCloud, galaxyScaleFactor);
            Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            graphics.DrawImage((Image) bitmap, rectangle, srcRect, GraphicsUnit.Pixel);
          }
          ++num3;
        }
      }
      return galaxyLocationList;
    }

    private Color SelectCloudColor() => this._CloudColors[this._Rnd.Next(0, this._CloudColors.Count)];
  }
}
