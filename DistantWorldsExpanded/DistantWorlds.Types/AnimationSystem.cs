// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.AnimationSystem
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
  public class AnimationSystem
  {
    private AnimationList _Animations;

    public AnimationSystem() => this._Animations = new AnimationList();

    public void ClearAnimations() => this._Animations.Clear();

    public void AddAnimation(Animation animation) => this._Animations.Add(animation);

    public Animation[] GetAnimations() => this._Animations.ToArray();

    public void DoAnimations(Graphics graphics, DateTime time)
    {
      AnimationList animationList = new AnimationList();
      foreach (Animation animation in (List<Animation>) this._Animations)
      {
        Rectangle drawRectangle = new Rectangle((int) animation.Xpos, (int) animation.Ypos, animation.Width, animation.Height);
        if (this.DrawAnimatedImages(graphics, animation, drawRectangle, time))
          animationList.Add(animation);
      }
      foreach (Animation animation in (List<Animation>) animationList)
        this._Animations.Remove(animation);
    }

    private bool DrawAnimatedImages(
      Graphics graphics,
      Animation animation,
      Rectangle drawRectangle,
      DateTime time)
    {
      int num1 = (int) ((double) animation.Images.Length / (double) animation.FramesPerSecond * 1000.0) / Math.Max(1, animation.Images.Length - 1);
      int index = Math.Max(0, (int) time.Subtract(animation.StartTime).TotalMilliseconds / num1);
      if (index >= animation.Images.Length)
        return true;
      Bitmap image = animation.Images[index];
      ImageAttributes imageAttr = (ImageAttributes) null;
      if (animation.TintColor != Color.Empty)
        imageAttr = this.CalculateImageAttributesOldStyle(animation.TintColor);
      int width = image.Width;
      int height = image.Height;
      if (animation.RotationAngle != 0.0)
      {
        float angle = (float) (animation.RotationAngle * -1.0);
        image = this.RotateImage(image, angle);
      }
      int num2 = (image.Width - width) / 2;
      int num3 = (image.Height - height) / 2;
      drawRectangle = new Rectangle(drawRectangle.X - num2, drawRectangle.Y - num3, drawRectangle.Width + num2 * 2, drawRectangle.Height + num3 * 2);
      Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
      graphics.InterpolationMode = InterpolationMode.Low;
      graphics.SmoothingMode = SmoothingMode.None;
      graphics.CompositingQuality = CompositingQuality.HighSpeed;
      if (imageAttr == null)
        graphics.DrawImage((Image) image, drawRectangle, srcRect, GraphicsUnit.Pixel);
      else
        graphics.DrawImage((Image) image, drawRectangle, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, imageAttr);
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      return false;
    }

    private ImageAttributes CalculateImageAttributesOldStyle(Color tintColor)
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
      ImageAttributes attributesOldStyle = new ImageAttributes();
      attributesOldStyle.SetColorMatrix(newColorMatrix);
      return attributesOldStyle;
    }

    private Bitmap RotateImage(Bitmap image, float angle) => this.RotateImage(image, angle, GraphicsQuality.Medium);

    private Bitmap RotateImage(Bitmap image, float angle, GraphicsQuality quality)
    {
      float f = image != null ? (float) image.Width : throw new ArgumentNullException(nameof (image));
      float height1 = (float) image.Height;
      angle *= -1f;
      angle *= 57.29578f;
      angle %= 360f;
      if ((double) angle < 0.0)
        angle += 360f;
      PointF[] pts = new PointF[4];
      pts[1].X = f;
      pts[2].X = f;
      pts[2].Y = height1;
      pts[3].Y = height1;
      Bitmap bitmap = (Bitmap) null;
      using (Matrix matrix = new Matrix())
      {
        matrix.Rotate(angle);
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
        double num1 = Math.Ceiling(val1_1 - val1_3);
        double num2 = Math.Ceiling(val1_2 - val1_4);
        if (double.IsNaN(num1))
          num1 = !float.IsNaN(f) ? Math.Max(1.0, (double) image.Width) : 50.0;
        if (double.IsNaN(num2))
          num2 = !float.IsNaN(height1) ? Math.Max(1.0, (double) image.Height) : 50.0;
        double width = Math.Max(1.0, num1);
        double height2 = Math.Max(1.0, num2);
        bitmap = new Bitmap((int) width, (int) height2, PixelFormat.Format32bppPArgb);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          this.SetGraphicsQuality(graphics, quality);
          PointF point1 = new PointF((float) (width / 2.0), (float) (height2 / 2.0));
          PointF point2 = new PointF(point1.X - f / 2f, point1.Y - height1 / 2f);
          matrix.Reset();
          matrix.RotateAt(angle, point1);
          graphics.Transform = matrix;
          graphics.DrawImage((Image) image, point2);
        }
      }
      return bitmap;
    }

    private void SetGraphicsQuality(Graphics graphics, GraphicsQuality quality)
    {
      switch (quality)
      {
        case GraphicsQuality.Undefined:
        case GraphicsQuality.Medium:
          graphics.CompositingQuality = CompositingQuality.HighSpeed;
          graphics.InterpolationMode = InterpolationMode.Bilinear;
          graphics.SmoothingMode = SmoothingMode.HighSpeed;
          break;
        case GraphicsQuality.Low:
          graphics.CompositingQuality = CompositingQuality.HighSpeed;
          graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
          graphics.SmoothingMode = SmoothingMode.None;
          break;
        case GraphicsQuality.High:
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
          break;
      }
    }
  }
}
