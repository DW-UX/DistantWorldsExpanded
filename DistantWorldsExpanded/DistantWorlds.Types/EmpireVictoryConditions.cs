// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireVictoryConditions
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireVictoryConditions
  {
    private EmpireList _EliminateEmpires = new EmpireList();
    private HabitatList _CaptureColonies = new HabitatList();
    private BuiltObjectList _DestroyBuiltObjects = new BuiltObjectList();

    public EmpireList EliminateEmpires
    {
      get => this._EliminateEmpires;
      set => this._EliminateEmpires = value;
    }

    public HabitatList CaptureColonies
    {
      get => this._CaptureColonies;
      set => this._CaptureColonies = value;
    }

    public BuiltObjectList DestroyBuiltObjects
    {
      get => this._DestroyBuiltObjects;
      set => this._DestroyBuiltObjects = value;
    }
  }
}
