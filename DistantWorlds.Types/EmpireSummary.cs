// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireSummary
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireSummary
  {
    public int EmpireId;
    public string Name;
    public int GovernmentId;
    public string RaceName;
    public bool IsPirateFaction;
    public PiratePlayStyle PiratePlayStyle;
    public int ColonyCount;
    public int SystemCount;
    public long PopulationAmount;
    public int SpaceportCount;
    public double Money;
    public double Cashflow;
    public Color MainColor;
    public Color SecondaryColor;
    public int FlagIndex;
    public string Description = string.Empty;

    public static EmpireSummary GenerateSummaryFromEmpire(Empire empire)
    {
      EmpireSummary summaryFromEmpire = new EmpireSummary();
      if (empire != null)
      {
        summaryFromEmpire.EmpireId = empire.EmpireId;
        summaryFromEmpire.Name = empire.Name;
        summaryFromEmpire.GovernmentId = empire.GovernmentId;
        if (empire.DominantRace != null)
          summaryFromEmpire.RaceName = empire.DominantRace.Name;
        summaryFromEmpire.IsPirateFaction = empire.PirateEmpireBaseHabitat != null;
        summaryFromEmpire.PiratePlayStyle = empire.PiratePlayStyle;
        if (empire.Colonies != null)
        {
          summaryFromEmpire.ColonyCount = empire.Colonies.Count;
          HabitatList habitatList = new HabitatList();
          foreach (Habitat colony in (SyncList<Habitat>) empire.Colonies)
          {
            Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(colony);
            if (!habitatList.Contains(habitatSystemStar))
              habitatList.Add(habitatSystemStar);
          }
          summaryFromEmpire.SystemCount = habitatList.Count;
        }
        summaryFromEmpire.PopulationAmount = empire.TotalPopulation;
        if (empire.SpacePorts != null)
          summaryFromEmpire.SpaceportCount = empire.SpacePorts.Count;
        summaryFromEmpire.Money = empire.StateMoney;
        summaryFromEmpire.Cashflow = empire.CalculateAccurateAnnualCashflow();
        summaryFromEmpire.MainColor = empire.MainColor;
        summaryFromEmpire.SecondaryColor = empire.SecondaryColor;
        summaryFromEmpire.FlagIndex = empire.FlagShape;
        summaryFromEmpire.Description = empire.Description;
      }
      return summaryFromEmpire;
    }
  }
}
