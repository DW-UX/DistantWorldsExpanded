// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SystemInfo
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SystemInfo : IComparable<SystemInfo>
  {
    public EmpireSystemSummary DominantEmpire;
    public EmpireSystemSummaryList OtherEmpires;
    public Sector Sector;
    public Habitat SystemStar;
    public HabitatList Habitats = new HabitatList();
    public CreatureList Creatures = new CreatureList();
    public bool IsDisputed;
    public bool HasRuins;
    public bool HasScenery;
    public bool HasResearchBonus;
    public int PlanetCount;
    public int MoonCount;
    public int BlockadeCount;
    public short PlagueId = -1;
    public int IndependentColonyCount;
    public bool PlayerPotentialColonies;

    public void CopyFromOther(SystemInfo other)
    {
      this.DominantEmpire = other.DominantEmpire;
      this.OtherEmpires = other.OtherEmpires;
      this.Sector = other.Sector;
      this.SystemStar = other.SystemStar;
      this.IsDisputed = other.IsDisputed;
      this.HasRuins = other.HasRuins;
      this.HasScenery = other.HasScenery;
      this.HasResearchBonus = other.HasResearchBonus;
      this.PlanetCount = other.PlanetCount;
      this.MoonCount = other.MoonCount;
      this.BlockadeCount = other.BlockadeCount;
      this.PlagueId = other.PlagueId;
      this.IndependentColonyCount = other.IndependentColonyCount;
      this.PlayerPotentialColonies = other.PlayerPotentialColonies;
    }

    int IComparable<SystemInfo>.CompareTo(SystemInfo other) => this.SystemStar != null && other.SystemStar != null ? this.SystemStar.Name.CompareTo(other.SystemStar.Name) : 0;
  }
}
