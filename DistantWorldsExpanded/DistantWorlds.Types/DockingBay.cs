// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DockingBay
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DockingBay
  {
    private BuiltObject _DockedShip;
    public int _Capacity;
    private short _ComponentId;
    private short _BuiltObjectComponentId;

    public DockingBay(int componentId, short builtObjectComponentId, int capacity)
    {
      this._Capacity = capacity;
      this._ComponentId = (short) componentId;
      this._BuiltObjectComponentId = builtObjectComponentId;
    }

    public short ParentBuiltObjectComponentId => this._BuiltObjectComponentId;

    public short ParentComponentId => this._ComponentId;

    public BuiltObject DockedShip
    {
      get => this._DockedShip;
      set => this._DockedShip = value;
    }

    public int Capacity => this._Capacity;
  }
}
