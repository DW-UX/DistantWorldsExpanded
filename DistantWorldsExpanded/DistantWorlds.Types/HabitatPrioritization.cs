// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.HabitatPrioritization
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class HabitatPrioritization : IComparable<HabitatPrioritization>
  {
    public Habitat Habitat;
    public int Priority;
    public BuiltObject AssignedShip;

    public void Clear()
    {
      this.Habitat = (Habitat) null;
      this.AssignedShip = (BuiltObject) null;
    }

    public int CompareTo(HabitatPrioritization habitatPrioritization) => this.Priority.CompareTo(habitatPrioritization.Priority);

    public HabitatPrioritization(Habitat habitat, int priority)
    {
      this.Habitat = habitat;
      this.Priority = priority;
    }
  }
}
