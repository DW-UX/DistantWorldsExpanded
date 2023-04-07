// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.LightningPathNode
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class LightningPathNode
  {
    public double X;
    public double Y;
    public double Angle;
    public double Width;
    public LightningPathNodeList Children = new LightningPathNodeList();

    public LightningPathNode(double x, double y, double angle, double width)
    {
      this.X = x;
      this.Y = y;
      this.Angle = angle;
      this.Width = width;
    }
  }
}
