// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ComponentResource
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ComponentResource : Resource
  {
    protected short _Quantity;

    public ComponentResource(byte resourceId, short quantity)
      : base(resourceId)
    {
      this._Quantity = quantity;
    }

    public short Quantity
    {
      get => this._Quantity;
      set => this._Quantity = value;
    }
  }
}
