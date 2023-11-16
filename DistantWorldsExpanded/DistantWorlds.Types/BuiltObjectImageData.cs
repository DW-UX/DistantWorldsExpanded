// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectImageData
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System.Collections.Generic;
using System.Drawing;

namespace DistantWorlds.Types
{
  public class BuiltObjectImageData
  {
    public Bitmap Image;
    public Bitmap MaskImage;
    public int Size;
    public List<Rectangle> ThrusterLocations = new List<Rectangle>();
    public List<Point> LightPoints = new List<Point>();
    public BuiltObjectImageSize ImageSize;

    public BuiltObjectImageData(
      Bitmap image,
      Bitmap maskImage,
      int size,
      List<Rectangle> thrusterLocations,
      List<Point> lightPoints,
      BuiltObjectImageSize imageSize)
    {
      this.Image = image;
      this.MaskImage = maskImage;
      this.Size = size;
      this.ThrusterLocations = thrusterLocations;
      this.LightPoints = lightPoints;
      this.ImageSize = imageSize;
    }
  }
}
