// Decompiled with JetBrains decompiler
// Type: DistantWorlds.XnaDrawingHelper
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace DistantWorlds
{
  public static class XnaDrawingHelper
  {
    private static Texture2D texture2D_0;
    private static uint[] uint_0;
    private static uint[] uint_1;
    private static uint[] uint_2;

    public static void Initialize(GraphicsDevice graphics)
    {
      XnaDrawingHelper.texture2D_0 = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
      Microsoft.Xna.Framework.Color[] data = new Microsoft.Xna.Framework.Color[1]
      {
        Microsoft.Xna.Framework.Color.White
      };
      XnaDrawingHelper.texture2D_0.SetData<Microsoft.Xna.Framework.Color>(data);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      int x,
      int y,
      float rotationAngle,
      float scaleFactor)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, (float) x, (float) y, rotationAngle, scaleFactor, System.Drawing.Color.White);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      int x,
      int y,
      float rotationAngle,
      float scaleFactor,
      System.Drawing.Color color)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, (float) x, (float) y, rotationAngle, scaleFactor, color);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      float x,
      float y,
      float rotationAngle,
      float scaleFactor)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, x, y, rotationAngle, new SizeF(scaleFactor, scaleFactor), System.Drawing.Color.White);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      float x,
      float y,
      float rotationAngle,
      float scaleFactor,
      System.Drawing.Color color)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, x, y, rotationAngle, new SizeF(scaleFactor, scaleFactor), color);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      float x,
      float y,
      float rotationAngle,
      float scaleFactor,
      System.Drawing.Color color,
      bool offsetPositionByCenter)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, x, y, rotationAngle, new SizeF(scaleFactor, scaleFactor), color, offsetPositionByCenter);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      float x,
      float y,
      float rotationAngle,
      SizeF scaleFactor,
      System.Drawing.Color color)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, x, y, rotationAngle, scaleFactor, color, true);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      float x,
      float y,
      float rotationAngle,
      SizeF scaleFactor,
      System.Drawing.Color color,
      bool offsetPositionByCenter)
    {
      if (XnaDrawingHelper.smethod_0((GraphicsResource) texture))
        return;
      Vector2 position = new Vector2(x, y);
      Vector2 origin = new Vector2((float) (texture.Width / 2), (float) (texture.Height / 2));
      if (offsetPositionByCenter)
        position += origin;
      Vector2 scale = new Vector2(scaleFactor.Width, scaleFactor.Height);
      Microsoft.Xna.Framework.Color color1 = XnaDrawingHelper.ResolveXnaColor(color);
      spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?(), color1, rotationAngle, origin, scale, SpriteEffects.None, 0.0f);
    }

    public static void DrawTextureStretchedBeam(
      SpriteBatch spriteBatch,
      Texture2D texture,
      float firingX,
      float firingY,
      float firingAngle,
      SizeF scaleFactor,
      System.Drawing.Color color)
    {
      if (XnaDrawingHelper.smethod_0((GraphicsResource) texture))
        return;
      Vector2 position = new Vector2(firingX, firingY);
      Vector2 origin = new Vector2(0.0f, (float) (texture.Height / 2));
      Vector2 scale = new Vector2(scaleFactor.Width, scaleFactor.Height);
      Microsoft.Xna.Framework.Color color1 = XnaDrawingHelper.ResolveXnaColor(color);
      spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?(), color1, firingAngle, origin, scale, SpriteEffects.None, 0.0f);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      int x,
      int y,
      int width,
      int height,
      float rotationAngle)
    {
      System.Drawing.Rectangle destination = new System.Drawing.Rectangle(x, y, width, height);
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, destination, rotationAngle, System.Drawing.Color.White);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      System.Drawing.Rectangle destination,
      float rotationAngle)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, destination, rotationAngle, System.Drawing.Color.White);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      System.Drawing.Rectangle destination,
      float rotationAngle,
      float layerDepth)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, destination, rotationAngle, layerDepth, System.Drawing.Color.White);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      System.Drawing.Rectangle destination,
      float rotationAngle,
      System.Drawing.Color tintColor)
    {
      XnaDrawingHelper.DrawTexture(spriteBatch, texture, destination, rotationAngle, 0.0f, tintColor);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      System.Drawing.Rectangle destination,
      float rotationAngle,
      float layerDepth,
      System.Drawing.Color tintColor)
    {
      if (XnaDrawingHelper.smethod_0((GraphicsResource) texture))
        return;
      Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(destination.X, destination.Y, destination.Width, destination.Height);
      Vector2 scale = new Vector2((float) rectangle.Width / (float) texture.Width, (float) rectangle.Height / (float) texture.Height);
      Vector2 position = new Vector2((float) (rectangle.X + rectangle.Width / 2), (float) (rectangle.Y + rectangle.Height / 2));
      Vector2 origin = new Vector2((float) (texture.Width / 2), (float) (texture.Height / 2));
      Microsoft.Xna.Framework.Color color = XnaDrawingHelper.ResolveXnaColor(tintColor);
      spriteBatch.Draw(texture, position, new Microsoft.Xna.Framework.Rectangle?(), color, rotationAngle, origin, scale, SpriteEffects.None, layerDepth);
    }

    public static void DrawTexture(
      SpriteBatch spriteBatch,
      Texture2D texture,
      System.Drawing.Rectangle source,
      System.Drawing.Rectangle destination)
    {
      if (XnaDrawingHelper.smethod_0((GraphicsResource) texture))
        return;
      Microsoft.Xna.Framework.Rectangle destinationRectangle = new Microsoft.Xna.Framework.Rectangle(destination.X, destination.Y, destination.Width, destination.Height);
      Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(source.X, source.Y, source.Width, source.Height);
      Vector2 vector2 = new Vector2((float) (texture.Width / 2), (float) (texture.Height / 2));
      spriteBatch.Draw(texture, destinationRectangle, new Microsoft.Xna.Framework.Rectangle?(rectangle), Microsoft.Xna.Framework.Color.White);
    }

    private static bool smethod_0(GraphicsResource graphicsResource_0) => graphicsResource_0 == null || graphicsResource_0.IsDisposed;

    public static Bitmap FastTextureToBitmap(Texture2D texture)
    {
      if (XnaDrawingHelper.smethod_0((GraphicsResource) texture))
        return (Bitmap) null;
      byte[] numArray = new byte[4 * texture.Width * texture.Height];
      texture.GetData<byte>(numArray);
      Bitmap bitmap = new Bitmap(texture.Width, texture.Height, PixelFormat.Format32bppArgb);
      BitmapData bitmapdata = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, texture.Width, texture.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
      IntPtr scan0 = bitmapdata.Scan0;
      Marshal.Copy(numArray, 0, scan0, numArray.Length);
      bitmap.UnlockBits(bitmapdata);
      return bitmap;
    }

    public static unsafe Texture2D FastBitmapToTextureWithMipMaps(
      GraphicsDevice graphics,
      Bitmap bitmap)
    {
      uint[] data = new uint[bitmap.Width * bitmap.Height];
      BitmapData bitmapdata = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
      uint* scan0 = (uint*) (void*) bitmapdata.Scan0;
      for (int index = 0; index < data.Length; ++index)
        data[index] = (uint) (((int) scan0[index] & (int) byte.MaxValue) << 16 | (int) scan0[index] & 65280 | (int) ((scan0[index] & 16711680U) >> 16) | (int) scan0[index] & -16777216);
      bitmap.UnlockBits(bitmapdata);
      Texture2D textureWithMipMaps = new Texture2D(graphics, bitmap.Width, bitmap.Height, true, SurfaceFormat.Color);
      textureWithMipMaps.SetData<uint>(data);
      return textureWithMipMaps;
    }

    public static unsafe Texture2D FastBitmapToTextureHighQuality(
      GraphicsDevice graphics,
      Bitmap bitmap)
    {
      int length = bitmap.Width * bitmap.Height;
      uint[] data = new uint[length];
      BitmapData bitmapdata = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
      uint* scan0 = (uint*) (void*) bitmapdata.Scan0;
      for (int index = 0; index < length; ++index)
        data[index] = (uint) (((int) scan0[index] & (int) byte.MaxValue) << 16 | (int) scan0[index] & 65280 | (int) ((scan0[index] & 16711680U) >> 16) | (int) scan0[index] & -16777216);
      bitmap.UnlockBits(bitmapdata);
      Texture2D textureHighQuality = new Texture2D(graphics, bitmap.Width, bitmap.Height, false, SurfaceFormat.Color);
      textureHighQuality.SetData<uint>(data);
      return textureHighQuality;
    }

    public static unsafe void FastBitmapToTexture(
      GraphicsDevice graphics,
      Bitmap bitmap,
      ref Texture2D texture,
      uint[] pixelBuffer)
    {
      if (bitmap == null || texture == null || pixelBuffer == null)
        throw new ApplicationException("Missing Bitmap, Texture or Pixelbuffer");
      int num1 = bitmap.Width * bitmap.Height;
      int num2 = texture.Width * texture.Height;
      if (pixelBuffer.Length != num1 || pixelBuffer.Length != num2)
        throw new ApplicationException("Supplied Bitmap, Texture and Pixelbuffer sizes do not match");
      BitmapData bitmapdata = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
      uint* scan0 = (uint*) (void*) bitmapdata.Scan0;
      for (int index = 0; index < num1; ++index)
        pixelBuffer[index] = (uint) (((int) scan0[index] & (int) byte.MaxValue) << 16 | (int) scan0[index] & 65280 | (int) ((scan0[index] & 16711680U) >> 16) | (int) scan0[index] & -16777216);
      bitmap.UnlockBits(bitmapdata);
      texture = new Texture2D(graphics, bitmap.Width, bitmap.Height, false, SurfaceFormat.Color);
      texture.SetData<uint>(pixelBuffer);
    }

    public static Texture2D FastBitmapToTexture(GraphicsDevice graphics, Bitmap bitmap) => XnaDrawingHelper.FastBitmapToTexture(graphics, bitmap, false);

    public static unsafe Texture2D FastBitmapToTexture(
      GraphicsDevice graphics,
      Bitmap bitmap,
      bool useAlternateBuffer)
    {
      int elementCount = bitmap.Width * bitmap.Height;
      Texture2D texture;
      if (useAlternateBuffer)
      {
        BitmapData bitmapdata = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
        uint* scan0 = (uint*) (void*) bitmapdata.Scan0;
        for (int index = 0; index < elementCount; ++index)
          XnaDrawingHelper.uint_1[index] = (uint) (((int) scan0[index] & (int) byte.MaxValue) << 16 | (int) scan0[index] & 65280 | (int) ((scan0[index] & 16711680U) >> 16) | (int) scan0[index] & -16777216);
        bitmap.UnlockBits(bitmapdata);
        texture = new Texture2D(graphics, bitmap.Width, bitmap.Height, false, SurfaceFormat.Color);
        texture.SetData<uint>(XnaDrawingHelper.uint_1, 0, elementCount);
      }
      else
      {
        BitmapData bitmapdata = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
        uint* scan0 = (uint*) (void*) bitmapdata.Scan0;
        for (int index = 0; index < elementCount; ++index)
          XnaDrawingHelper.uint_0[index] = (uint) (((int) scan0[index] & (int) byte.MaxValue) << 16 | (int) scan0[index] & 65280 | (int) ((scan0[index] & 16711680U) >> 16) | (int) scan0[index] & -16777216);
        bitmap.UnlockBits(bitmapdata);
        texture = new Texture2D(graphics, bitmap.Width, bitmap.Height, false, SurfaceFormat.Color);
        texture.SetData<uint>(XnaDrawingHelper.uint_0, 0, elementCount);
      }
      return texture;
    }

    public static Texture2D FastBitmapToTextureSmall(GraphicsDevice graphics, Bitmap bitmap) => XnaDrawingHelper.FastBitmapToTextureSmall(graphics, bitmap, true);

    public static unsafe Texture2D FastBitmapToTextureSmall(
      GraphicsDevice graphics,
      Bitmap bitmap,
      bool clearBuffer)
    {
      int num = bitmap.Width * bitmap.Height;
      BitmapData bitmapdata = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
      uint* scan0 = (uint*) (void*) bitmapdata.Scan0;
      for (int index = 0; index < num; ++index)
        XnaDrawingHelper.uint_2[index] = (uint) (((int) scan0[index] & (int) byte.MaxValue) << 16 | (int) scan0[index] & 65280 | (int) ((scan0[index] & 16711680U) >> 16) | (int) scan0[index] & -16777216);
      bitmap.UnlockBits(bitmapdata);
      if (clearBuffer)
      {
        int length = XnaDrawingHelper.uint_2.Length;
        Array.Clear((Array) XnaDrawingHelper.uint_2, num, length - num);
      }
      Texture2D textureSmall = new Texture2D(graphics, bitmap.Width, bitmap.Height, false, SurfaceFormat.Color);
      textureSmall.SetData<uint>(XnaDrawingHelper.uint_2, 0, num);
      return textureSmall;
    }

    public static Texture2D BitmapToTexture(GraphicsDevice graphics, Bitmap bitmap)
    {
      MemoryStream memoryStream = new MemoryStream();
      bitmap.Save((Stream) memoryStream, ImageFormat.Png);
      Texture2D texture = Texture2D.FromStream(graphics, (Stream) memoryStream);
      memoryStream.Close();
      memoryStream.Dispose();
      return texture;
    }

    public static void ConvertBitmapsToTexturesIfExist(
      GraphicsDevice graphics,
      Bitmap[] images,
      ref Texture2D[] textures)
    {
      if (images == null || images.Length <= 0 || images[0] == null || images[0].PixelFormat == PixelFormat.Undefined)
        return;
      XnaDrawingHelper.DisposeTextureArray(textures);
      List<Texture2D> texture2DList = new List<Texture2D>();
      for (int index = 0; index < images.Length; ++index)
      {
        if (images[index] != null && images[index].PixelFormat != PixelFormat.Undefined)
          texture2DList.Add(XnaDrawingHelper.FastBitmapToTexture(graphics, images[index]));
      }
      textures = texture2DList.ToArray();
    }

    public static Texture2D[] ConvertBitmapsToTextures(GraphicsDevice graphics, Bitmap[] images)
    {
      Texture2D[] textures = new Texture2D[images.Length];
      for (int index = 0; index < images.Length; ++index)
        textures[index] = XnaDrawingHelper.FastBitmapToTexture(graphics, images[index]);
      return textures;
    }

    public static List<Texture2D> ConvertBitmapsToTextures(
      GraphicsDevice graphics,
      List<Bitmap> images)
    {
      return XnaDrawingHelper.ConvertBitmapsToTextures(graphics, images, false);
    }

    public static List<Texture2D> ConvertBitmapsToTextures(
      GraphicsDevice graphics,
      List<Bitmap> images,
      bool useAlternateBuffer)
    {
      List<Texture2D> textures = new List<Texture2D>();
      for (int index = 0; index < images.Count; ++index)
        textures.Add(XnaDrawingHelper.FastBitmapToTexture(graphics, images[index], useAlternateBuffer));
      return textures;
    }

    public static void DisposeTextureArray(Texture2D[] textures)
    {
      if (textures == null || textures.Length <= 0)
        return;
      for (int index = 0; index < textures.Length; ++index)
      {
        Texture2D texture = textures[index];
        if (texture != null && !texture.IsDisposed)
          texture.Dispose();
        textures[index] = (Texture2D) null;
      }
    }

    public static void DrawRectangle(
      SpriteBatch spriteBatch,
      System.Drawing.Rectangle rectangle,
      System.Drawing.Color color,
      int lineThickness)
    {
      XnaDrawingHelper.DrawRectangle(spriteBatch, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, lineThickness);
    }

    public static void DrawRectangle(
      SpriteBatch spriteBatch,
      int x,
      int y,
      int width,
      int height,
      System.Drawing.Color color,
      int lineThickness)
    {
      Vector2 vector2_1 = new Vector2((float) x, (float) y);
      Vector2 vector2_2 = new Vector2((float) (x + width), (float) y);
      Vector2 vector2_3 = new Vector2((float) (x + width), (float) (y + height));
      Vector2 vector2_4 = new Vector2((float) x, (float) (y + height));
      XnaDrawingHelper.DrawLine(spriteBatch, vector2_1, vector2_2, color, lineThickness);
      XnaDrawingHelper.DrawLine(spriteBatch, vector2_2, vector2_3, color, lineThickness);
      XnaDrawingHelper.DrawLine(spriteBatch, vector2_3, vector2_4, color, lineThickness);
      XnaDrawingHelper.DrawLine(spriteBatch, vector2_4, vector2_1, color, lineThickness);
    }

    public static void FillRectangle(SpriteBatch spriteBatch, System.Drawing.Rectangle rectangle, System.Drawing.Color color)
    {
      Microsoft.Xna.Framework.Color color1 = XnaDrawingHelper.ResolveXnaColor(color);
      Vector2 position = new Vector2((float) rectangle.X, (float) rectangle.Y);
      Vector2 scale = new Vector2((float) rectangle.Width, (float) rectangle.Height);
      spriteBatch.Draw(XnaDrawingHelper.texture2D_0, position, new Microsoft.Xna.Framework.Rectangle?(), color1, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      System.Drawing.Point point1,
      System.Drawing.Point point2,
      System.Drawing.Color color,
      int lineThickness)
    {
      XnaDrawingHelper.DrawLine(spriteBatch, point1, point2, color, lineThickness, false);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      System.Drawing.Point point1,
      System.Drawing.Point point2,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed)
    {
      XnaDrawingHelper.DrawLine(spriteBatch, (float) point1.X, (float) point1.Y, (float) point2.X, (float) point2.Y, color, lineThickness, dashed);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      int x1,
      int y1,
      int x2,
      int y2,
      System.Drawing.Color color,
      int lineThickness)
    {
      XnaDrawingHelper.DrawLine(spriteBatch, x1, y1, x2, y2, color, lineThickness, false);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      int x1,
      int y1,
      int x2,
      int y2,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed)
    {
      XnaDrawingHelper.DrawLine(spriteBatch, (float) x1, (float) y1, (float) x2, (float) y2, color, lineThickness, dashed, (Texture2D) null);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      int x1,
      int y1,
      int x2,
      int y2,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed,
      Texture2D arrowHeadTexture)
    {
      XnaDrawingHelper.DrawLine(spriteBatch, (float) x1, (float) y1, (float) x2, (float) y2, color, lineThickness, dashed, arrowHeadTexture);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      float x1,
      float y1,
      float x2,
      float y2,
      System.Drawing.Color color,
      int lineThickness)
    {
      XnaDrawingHelper.DrawLine(spriteBatch, x1, y1, x2, y2, color, lineThickness, false);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      float x1,
      float y1,
      float x2,
      float y2,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed)
    {
      Vector2 vector1 = new Vector2(x1, y1);
      Vector2 vector2 = new Vector2(x2, y2);
      XnaDrawingHelper.DrawLine(spriteBatch, vector1, vector2, color, lineThickness, dashed, (Texture2D) null);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      float x1,
      float y1,
      float x2,
      float y2,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed,
      Texture2D arrowHeadTexture)
    {
      Vector2 vector1 = new Vector2(x1, y1);
      Vector2 vector2 = new Vector2(x2, y2);
      XnaDrawingHelper.DrawLine(spriteBatch, vector1, vector2, color, lineThickness, dashed, arrowHeadTexture);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      Vector2 vector1,
      Vector2 vector2,
      System.Drawing.Color color,
      int lineThickness)
    {
      XnaDrawingHelper.DrawLine(spriteBatch, vector1, vector2, color, lineThickness, false);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      Vector2 vector1,
      Vector2 vector2,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed)
    {
      XnaDrawingHelper.DrawLine(spriteBatch, vector1, vector2, color, lineThickness, dashed, (Texture2D) null);
    }

    public static void DrawLine(
      SpriteBatch spriteBatch,
      Vector2 vector1,
      Vector2 vector2,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed,
      Texture2D arrowHeadTexture)
    {
      float x1 = Vector2.Distance(vector1, vector2);
      float num1 = (float) Math.Atan2((double) vector2.Y - (double) vector1.Y, (double) vector2.X - (double) vector1.X);
      double num2 = Math.Cos((double) num1);
      double num3 = Math.Sin((double) num1);
      Microsoft.Xna.Framework.Color color1 = XnaDrawingHelper.ResolveXnaColor(color);
      if (dashed)
      {
        float x2 = 6f;
        int num4 = (int) ((double) x1 / (double) x2);
        Vector2 vector2_1 = new Vector2(vector1.X, vector1.Y);
        for (int index = 0; index < num4; ++index)
        {
          if (index == num4 - 1)
          {
            XnaDrawingHelper.DrawLine(spriteBatch, vector2_1, vector2, color, lineThickness);
            if (arrowHeadTexture != null)
            {
              float rotationAngle = num1 + 1.57079637f;
              float scaleFactor = (float) (9.0 / ((double) arrowHeadTexture.Width / (double) Math.Min(2f, (float) lineThickness)));
              float num5 = (float) arrowHeadTexture.Height * scaleFactor;
              float num6 = (float) Math.Atan2((double) vector1.Y - (double) vector2.Y, (double) vector1.X - (double) vector2.X);
              float x3 = vector2.X + (float) (Math.Cos((double) num6) * ((double) num5 / 2.0));
              float y = vector2.Y + (float) (Math.Sin((double) num6) * ((double) num5 / 2.0));
              XnaDrawingHelper.DrawTexture(spriteBatch, arrowHeadTexture, x3, y, rotationAngle, scaleFactor, color, false);
            }
          }
          else
          {
            Vector2 vector2_2 = new Vector2(vector2_1.X + (float) num2 * x2, vector2_1.Y + (float) num3 * x2);
            if (index % 2 == 0)
              spriteBatch.Draw(XnaDrawingHelper.texture2D_0, vector2_1, new Microsoft.Xna.Framework.Rectangle?(), color1, num1, Vector2.Zero, new Vector2(x2, (float) lineThickness), SpriteEffects.None, 0.0f);
            vector2_1 = vector2_2;
          }
        }
      }
      else
      {
        Vector2 origin = new Vector2(0.0f, 0.5f);
        spriteBatch.Draw(XnaDrawingHelper.texture2D_0, vector1, new Microsoft.Xna.Framework.Rectangle?(), color1, num1, origin, new Vector2(x1, (float) lineThickness), SpriteEffects.None, 0.0f);
      }
    }

    public static void DrawLineCustomTexture(
      SpriteBatch spriteBatch,
      System.Drawing.Point start,
      System.Drawing.Point end,
      Texture2D customTexture,
      System.Drawing.Color color,
      int lineThickness)
    {
      Vector2 position = new Vector2((float) start.X, (float) start.Y);
      Vector2 vector2 = new Vector2((float) end.X, (float) end.Y);
      float x = Vector2.Distance(position, vector2);
      float num = (float) Math.Atan2((double) vector2.Y - (double) position.Y, (double) vector2.X - (double) position.X);
      Math.Cos((double) num);
      Math.Sin((double) num);
      Microsoft.Xna.Framework.Color color1 = XnaDrawingHelper.ResolveXnaColor(color);
      spriteBatch.Draw(customTexture, position, new Microsoft.Xna.Framework.Rectangle?(), color1, num, Vector2.Zero, new Vector2(x, (float) lineThickness), SpriteEffects.None, 0.0f);
    }

    public static Microsoft.Xna.Framework.Color ResolveXnaColor(System.Drawing.Color color) => Microsoft.Xna.Framework.Color.FromNonPremultiplied((int) color.R, (int) color.G, (int) color.B, (int) color.A);

    public static void DrawString(
      SpriteBatch spriteBatch,
      string text,
      SpriteFont font,
      System.Drawing.Color color,
      int x,
      int y)
    {
      XnaDrawingHelper.DrawString(spriteBatch, text, font, color, new PointF((float) x, (float) y));
    }

    public static void DrawString(
      SpriteBatch spriteBatch,
      string text,
      SpriteFont font,
      System.Drawing.Color color,
      PointF point)
    {
      Microsoft.Xna.Framework.Color color1 = XnaDrawingHelper.ResolveXnaColor(color);
      Vector2 position = new Vector2(point.X, point.Y);
      float scale = 1f;
      spriteBatch.DrawString(font, text, position, color1, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
    }

    public static void DrawStringDropShadow(
      SpriteBatch spriteBatch,
      string text,
      SpriteFont font,
      System.Drawing.Color foreColor,
      int x,
      int y)
    {
      XnaDrawingHelper.DrawStringDropShadow(spriteBatch, text, font, foreColor, new PointF((float) x, (float) y));
    }

    public static void DrawStringDropShadow(
      SpriteBatch spriteBatch,
      string text,
      SpriteFont font,
      System.Drawing.Color foreColor,
      PointF point)
    {
      XnaDrawingHelper.DrawStringDropShadow(spriteBatch, text, font, foreColor, System.Drawing.Color.Black, point);
    }

    public static void DrawStringDropShadow(
      SpriteBatch spriteBatch,
      string text,
      SpriteFont font,
      System.Drawing.Color foreColor,
      System.Drawing.Color shadowColor,
      int x,
      int y)
    {
      XnaDrawingHelper.DrawStringDropShadow(spriteBatch, text, font, foreColor, new PointF((float) x, (float) y));
    }

    public static void DrawStringDropShadow(
      SpriteBatch spriteBatch,
      string text,
      SpriteFont font,
      System.Drawing.Color foreColor,
      System.Drawing.Color shadowColor,
      PointF point)
    {
      PointF point1 = new PointF(point.X + 1f, point.Y + 1f);
      XnaDrawingHelper.DrawString(spriteBatch, text, font, shadowColor, point1);
      XnaDrawingHelper.DrawString(spriteBatch, text, font, foreColor, point);
    }

    public static void DrawPolygon(
      SpriteBatch spriteBatch,
      System.Drawing.Point[] points,
      System.Drawing.Color color,
      int lineThickness)
    {
      for (int index = 0; index < points.Length; ++index)
      {
        Vector2 vector1 = new Vector2((float) points[index].X, (float) points[index].Y);
        Vector2 vector2 = index != points.Length - 1 ? new Vector2((float) points[index + 1].X, (float) points[index + 1].Y) : new Vector2((float) points[0].X, (float) points[0].Y);
        XnaDrawingHelper.DrawLine(spriteBatch, vector1, vector2, color, lineThickness);
      }
    }

    public static void DrawPolygon(
      SpriteBatch spriteBatch,
      PointF[] points,
      System.Drawing.Color color,
      int lineThickness)
    {
      for (int index = 0; index < points.Length; ++index)
      {
        Vector2 vector1 = new Vector2(points[index].X, points[index].Y);
        Vector2 vector2 = index != points.Length - 1 ? new Vector2(points[index + 1].X, points[index + 1].Y) : new Vector2(points[0].X, points[0].Y);
        XnaDrawingHelper.DrawLine(spriteBatch, vector1, vector2, color, lineThickness);
      }
    }

    public static void DrawCircle(
      SpriteBatch spriteBatch,
      int x,
      int y,
      int width,
      int height,
      System.Drawing.Color color,
      int lineThickness,
      int segmentCount)
    {
      XnaDrawingHelper.DrawCircle(spriteBatch, (float) x, (float) y, (float) width, (float) height, color, lineThickness, segmentCount, false);
    }

    public static void DrawCircle(
      SpriteBatch spriteBatch,
      int x,
      int y,
      int width,
      int height,
      System.Drawing.Color color,
      int lineThickness,
      int segmentCount,
      bool dashed)
    {
      XnaDrawingHelper.DrawCircle(spriteBatch, (float) x, (float) y, (float) width, (float) height, color, lineThickness, segmentCount, dashed);
    }

    public static void DrawCircle(
      SpriteBatch spriteBatch,
      float x,
      float y,
      float width,
      float height,
      System.Drawing.Color color,
      int lineThickness,
      int segmentCount)
    {
      XnaDrawingHelper.DrawCircle(spriteBatch, x, y, width, height, color, lineThickness, segmentCount, false);
    }

    public static void DrawCircle(
      SpriteBatch spriteBatch,
      float x,
      float y,
      float width,
      float height,
      System.Drawing.Color color,
      int lineThickness,
      int segmentCount,
      bool dashed)
    {
      float num1 = width / 2f;
      float num2 = x + width / 2f;
      float num3 = y + height / 2f;
      float num4 = 6.28318548f;
      float num5 = num4 / (float) segmentCount;
      List<Vector2> vector2List = new List<Vector2>();
      for (float num6 = 0.0f; (double) num6 < (double) num4; num6 += num5)
        vector2List.Add(new Vector2(num2 + num1 * (float) Math.Cos((double) num6), num3 + num1 * (float) Math.Sin((double) num6)));
      Microsoft.Xna.Framework.Color xnaColor = XnaDrawingHelper.ResolveXnaColor(color);
      float num7 = 6.28318548f / (float) segmentCount;
      if (dashed)
        num7 = 6.28318548f / (float) (segmentCount / 2);
      float angle = (float) (Math.PI / 2.0 + 2.0 * Math.PI / (double) (segmentCount * 2));
      for (int index = 0; index < vector2List.Count; ++index)
      {
        if (!dashed || index % 2 != 1)
        {
          Vector2 vector1 = vector2List[index];
          Vector2 vector2 = index != vector2List.Count - 1 ? vector2List[index + 1] : vector2List[0];
          XnaDrawingHelper.DrawLineForCircle(spriteBatch, vector1, vector2, angle, color, xnaColor, lineThickness, false);
          angle += num7;
        }
      }
    }

    public static void DrawCircle(
      SpriteBatch spriteBatch,
      System.Drawing.Rectangle area,
      System.Drawing.Color color,
      int lineThickness)
    {
      XnaDrawingHelper.DrawCircle(spriteBatch, area, 30, color, lineThickness, false);
    }

    public static void DrawCircle(
      SpriteBatch spriteBatch,
      System.Drawing.Rectangle area,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed)
    {
      XnaDrawingHelper.DrawCircle(spriteBatch, area, 30, color, lineThickness, dashed);
    }

    public static void DrawCircle(
      SpriteBatch spriteBatch,
      System.Drawing.Rectangle area,
      int sideCount,
      System.Drawing.Color color,
      int lineThickness)
    {
      XnaDrawingHelper.DrawCircle(spriteBatch, area, sideCount, color, lineThickness, false);
    }

    public static void DrawCircle(
      SpriteBatch spriteBatch,
      System.Drawing.Rectangle area,
      int sideCount,
      System.Drawing.Color color,
      int lineThickness,
      bool dashed)
    {
      float num1 = (float) (area.Width / 2);
      float x = (float) area.X;
      float y = (float) area.Y;
      float num2 = x + num1;
      float num3 = y + num1;
      float num4 = 6.28318548f;
      float num5 = num4 / (float) sideCount;
      List<Vector2> vector2List = new List<Vector2>();
      for (float num6 = 0.0f; (double) num6 < (double) num4; num6 += num5)
        vector2List.Add(new Vector2(num2 + num1 * (float) Math.Cos((double) num6), num3 + num1 * (float) Math.Sin((double) num6)));
      Microsoft.Xna.Framework.Color xnaColor = XnaDrawingHelper.ResolveXnaColor(color);
      float num7 = 6.28318548f / (float) sideCount;
      if (dashed)
        num7 = 6.28318548f / (float) (sideCount / 2);
      float angle = (float) (Math.PI / 2.0 + 2.0 * Math.PI / (double) (sideCount * 2));
      for (int index = 0; index < vector2List.Count; ++index)
      {
        if (!dashed || index % 2 != 1)
        {
          Vector2 vector1 = vector2List[index];
          Vector2 vector2 = index != vector2List.Count - 1 ? vector2List[index + 1] : vector2List[0];
          XnaDrawingHelper.DrawLineForCircle(spriteBatch, vector1, vector2, angle, color, xnaColor, lineThickness, false);
          angle += num7;
        }
      }
    }

    public static void DrawLineForCircle(
      SpriteBatch spriteBatch,
      Vector2 vector1,
      Vector2 vector2,
      float angle,
      System.Drawing.Color color,
      Microsoft.Xna.Framework.Color xnaColor,
      int lineThickness,
      bool dashed)
    {
      float x1 = Vector2.Distance(vector1, vector2);
      double num1 = Math.Cos((double) angle);
      double num2 = Math.Sin((double) angle);
      if (dashed)
      {
        float x2 = 6f;
        int num3 = (int) ((double) x1 / (double) x2);
        Vector2 vector2_1 = new Vector2(vector1.X, vector1.Y);
        for (int index = 0; index < num3; ++index)
        {
          if (index == num3 - 1)
          {
            XnaDrawingHelper.DrawLine(spriteBatch, vector2_1, vector2, color, lineThickness);
          }
          else
          {
            Vector2 vector2_2 = new Vector2(vector2_1.X + (float) num1 * x2, vector2_1.Y + (float) num2 * x2);
            if (index % 2 == 0)
              spriteBatch.Draw(XnaDrawingHelper.texture2D_0, vector2_1, new Microsoft.Xna.Framework.Rectangle?(), xnaColor, angle, Vector2.Zero, new Vector2(x2, (float) lineThickness), SpriteEffects.None, 0.0f);
            vector2_1 = vector2_2;
          }
        }
      }
      else
      {
        Vector2 origin = new Vector2(0.0f, 0.5f);
        spriteBatch.Draw(XnaDrawingHelper.texture2D_0, vector1, new Microsoft.Xna.Framework.Rectangle?(), xnaColor, angle, origin, new Vector2(x1, (float) lineThickness), SpriteEffects.None, 0.0f);
      }
    }

    static XnaDrawingHelper()
    {
      Class7.VEFSJNszvZKMZ();
      XnaDrawingHelper.uint_0 = new uint[4000000];
      XnaDrawingHelper.uint_1 = new uint[4000000];
      XnaDrawingHelper.uint_2 = new uint[40000];
    }
  }
}
