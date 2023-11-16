// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourceDatePair
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourceDatePair
  {
    public byte ResourceId;
    public long StarDate;

    public ResourceDatePair(byte resourceId, long starDate)
    {
      this.ResourceId = resourceId;
      this.StarDate = starDate;
    }
  }
}
