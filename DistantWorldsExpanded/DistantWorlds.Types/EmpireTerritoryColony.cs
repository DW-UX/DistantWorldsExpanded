// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireTerritoryColony
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

namespace DistantWorlds.Types
{
  public class EmpireTerritoryColony
  {
    public Habitat Colony;
    public HabitatList OverlappingColonies = new HabitatList();

    public EmpireTerritoryColony(Habitat colony)
    {
      bool empireHasWarptech = true;
      if (colony.Empire != null)
        empireHasWarptech = colony.Empire.CheckEmpireHasHyperDriveTech(colony.Empire);
      colony.RecalculateColonyInfluenceRadius(empireHasWarptech);
      this.Colony = colony;
    }
  }
}
