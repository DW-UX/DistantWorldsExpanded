// Decompiled with JetBrains decompiler
// Type: DistantWorlds.AnimationSystem
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Controls;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DistantWorlds
{
  public class AnimationSystem
  {
    private MainView mainView_0;
    private AnimationList animationList_0;

    public AnimationSystem(MainView parent):base()
    {
      
      // ISSUE: explicit constructor call
      this.mainView_0 = parent;
      this.animationList_0 = new AnimationList();
    }

    public void ClearAnimations() => this.animationList_0.Clear();

    public void AddAnimation(Animation animation) => this.animationList_0.Add(animation);

    public void DoAnimations(System.Drawing.Graphics graphics, DateTime time)
    {
      AnimationList animationList = new AnimationList();
      foreach (Animation animation_0 in (List<Animation>) this.animationList_0)
      {
        Rectangle rectangle_0 = this.mainView_0.method_34((int) animation_0.Xpos, (int) animation_0.Ypos, animation_0.Width, animation_0.Height, this.mainView_0.main_0.double_0);
        if (this.method_0(graphics, animation_0, rectangle_0, time))
          animationList.Add(animation_0);
      }
      foreach (Animation animation in (List<Animation>) animationList)
        this.animationList_0.Remove(animation);
    }

    private bool method_0(
      System.Drawing.Graphics graphics_0,
      Animation animation_0,
      Rectangle rectangle_0,
      DateTime dateTime_0)
    {
      int num1 = (int) ((double) animation_0.Images.Length / (double) animation_0.FramesPerSecond * 1000.0) / Math.Max(1, animation_0.Images.Length - 1);
      int index = Math.Max(0, (int) dateTime_0.Subtract(animation_0.StartTime).TotalMilliseconds / num1);
      if (index >= animation_0.Images.Length)
        return true;
      Bitmap bitmap_7 = animation_0.Images[index];
      ImageAttributes imageAttr = (ImageAttributes) null;
      if (animation_0.TintColor != Color.Empty)
        imageAttr = this.mainView_0.method_219(animation_0.TintColor);
      int width = bitmap_7.Width;
      int height = bitmap_7.Height;
      if (animation_0.RotationAngle != 0.0)
      {
        float float_0 = (float) (animation_0.RotationAngle * -1.0);
        bitmap_7 = this.mainView_0.method_217(bitmap_7, float_0);
      }
      int num2 = (bitmap_7.Width - width) / 2;
      int num3 = (bitmap_7.Height - height) / 2;
      int num4 = (int) ((double) num2 / this.mainView_0.main_0.double_0);
      int num5 = (int) ((double) num3 / this.mainView_0.main_0.double_0);
      rectangle_0 = new Rectangle(rectangle_0.X - num4, rectangle_0.Y - num5, rectangle_0.Width + num4 * 2, rectangle_0.Height + num5 * 2);
      Rectangle srcRect = new Rectangle(0, 0, bitmap_7.Width, bitmap_7.Height);
      graphics_0.InterpolationMode = InterpolationMode.Low;
      graphics_0.SmoothingMode = SmoothingMode.None;
      graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
      if (imageAttr == null)
        graphics_0.DrawImage((Image) bitmap_7, rectangle_0, srcRect, GraphicsUnit.Pixel);
      else
        graphics_0.DrawImage((Image) bitmap_7, rectangle_0, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, imageAttr);
      graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
      graphics_0.CompositingQuality = CompositingQuality.HighQuality;
      return false;
    }

    public void DoAnimationsXna(SpriteBatch spriteBatch, DateTime time)
    {
      AnimationList animationList = new AnimationList();
      foreach (Animation animation_0 in (List<Animation>) this.animationList_0)
      {
        Rectangle rectangle_0 = this.mainView_0.method_34((int) animation_0.Xpos, (int) animation_0.Ypos, animation_0.Width, animation_0.Height, this.mainView_0.main_0.double_0);
        if (this.method_1(spriteBatch, animation_0, rectangle_0, time))
          animationList.Add(animation_0);
      }
      foreach (Animation animation in (List<Animation>) animationList)
      {
        animation.Teardown();
        this.animationList_0.Remove(animation);
      }
    }

    private bool method_1(
      SpriteBatch spriteBatch_0,
      Animation animation_0,
      Rectangle rectangle_0,
      DateTime dateTime_0)
    {
      int num = (int) ((double) animation_0.Textures.Length / (double) animation_0.FramesPerSecond * 1000.0) / Math.Max(1, animation_0.Textures.Length - 1);
      int index = Math.Max(0, (int) dateTime_0.Subtract(animation_0.StartTime).TotalMilliseconds / num);
      if (index >= animation_0.Textures.Length)
        return true;
      Texture2D texture = animation_0.Textures[index];
      float rotationAngle = (float) (animation_0.RotationAngle * -1.0);
      if (animation_0.TintColor == Color.Empty)
        XnaDrawingHelper.DrawTexture(spriteBatch_0, texture, rectangle_0, rotationAngle);
      else
        XnaDrawingHelper.DrawTexture(spriteBatch_0, texture, rectangle_0, rotationAngle, animation_0.TintColor);
      return false;
    }
  }
}
