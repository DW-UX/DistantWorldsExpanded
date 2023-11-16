// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpirePriority
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpirePriority : IComparable<EmpirePriority>
  {
    private Empire _Empire;
    private double _Priority;

    public EmpirePriority(Empire empire, double priority)
    {
      this._Empire = empire;
      this._Priority = priority;
    }

    public Empire Empire
    {
      get => this._Empire;
      set => this._Empire = value;
    }

    public double Priority
    {
      get => this._Priority;
      set => this._Priority = value;
    }

    int IComparable<EmpirePriority>.CompareTo(EmpirePriority other) => this.Priority.CompareTo(other.Priority);
  }
}
