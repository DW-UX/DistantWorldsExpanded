// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SystemInfoDistance
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SystemInfoDistance : IComparable<SystemInfoDistance>
  {
    public SystemInfo SystemInfo;
    public double Distance;

    int IComparable<SystemInfoDistance>.CompareTo(SystemInfoDistance other) => this.Distance.CompareTo(other.Distance);
  }
}
