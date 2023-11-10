// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Animation
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;

namespace DistantWorlds.Types
{
  public class Animation
  {
    private Bitmap[] _Images;
    private double _Xpos;
    private double _Ypos;
    private DateTime _StartTime;
    private double _RotationAngle;
    private int _Width;
    private int _Height;
    private Color _TintColor;
    private int _FramesPerSecond;
    private int _CurrentFrame;
    public object ExtraData;
    public bool DisposeTexturesWhenComplete = true;

    public Animation(
      Bitmap[] images,
      DateTime startTime,
      int framesPerSecond,
      double x,
      double y,
      int width,
      int height)
      : this(images, startTime, framesPerSecond, x, y, width, height, 0.0, Color.Empty)
    {
    }

    public Animation(
      Bitmap[] images,
      DateTime startTime,
      int framesPerSecond,
      double x,
      double y,
      int width,
      int height,
      double rotationAngle,
      Color tintColor)
    {
      this._Images = images;
      this._StartTime = startTime;
      this._FramesPerSecond = framesPerSecond;
      this._Xpos = x;
      this._Ypos = y;
      this._Width = width;
      this._Height = height;
      this._RotationAngle = rotationAngle;
      this._TintColor = tintColor;
    }

    public void Teardown()
    {
    }

    public Bitmap[] Images
    {
      get => this._Images;
      set => this._Images = value;
    }

    public double Xpos
    {
      get => this._Xpos;
      set => this._Xpos = value;
    }

    public double Ypos
    {
      get => this._Ypos;
      set => this._Ypos = value;
    }

    public DateTime StartTime
    {
      get => this._StartTime;
      set => this._StartTime = value;
    }

    public double RotationAngle
    {
      get => this._RotationAngle;
      set => this._RotationAngle = value;
    }

    public int Width
    {
      get => this._Width;
      set => this._Width = value;
    }

    public int Height
    {
      get => this._Height;
      set => this._Height = value;
    }

    public Color TintColor
    {
      get => this._TintColor;
      set => this._TintColor = value;
    }

    public int FramesPerSecond
    {
      get => this._FramesPerSecond;
      set => this._FramesPerSecond = value;
    }

    public int CurrentFrame
    {
      get => this._CurrentFrame;
      set => this._CurrentFrame = value;
    }
  }
}
