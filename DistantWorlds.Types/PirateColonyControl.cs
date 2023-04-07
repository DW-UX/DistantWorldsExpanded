// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PirateColonyControl
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PirateColonyControl : IComparable<PirateColonyControl>
  {
    public byte EmpireId;
    public float ControlLevel;
    public bool HasFacilityControl;

    public PirateColonyControl(int empireId, float controlLevel)
      : this(empireId, controlLevel, false)
    {
    }

    public PirateColonyControl(int empireId, float controlLevel, bool hasFacilityControl)
    {
      if (empireId > (int) byte.MaxValue)
        return;
      this.EmpireId = (byte) empireId;
      this.ControlLevel = controlLevel;
      this.HasFacilityControl = hasFacilityControl;
    }

    int IComparable<PirateColonyControl>.CompareTo(PirateColonyControl other) => this.ControlLevel.CompareTo(other.ControlLevel);
  }
}
