// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireStartList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Globalization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireStartList : SyncList<EmpireStart>
  {
    public void Update(RaceList races) => this.Update(races, string.Empty);

    public void Update(RaceList races, string raceNameToExcludeWhenSelectingRandomRaces)
    {
      Random rnd = new Random((int) DateTime.Now.Ticks);
      foreach (EmpireStart empireStart in (SyncList<EmpireStart>) this)
      {
        string raceName = empireStart.Race;
        if (empireStart.ResolvedRace != null)
          raceName = empireStart.ResolvedRace.Name;
        Race race = this.ResolveRace(races, raceName, rnd, raceNameToExcludeWhenSelectingRandomRaces);
        int empireExpansion = (int) this.DetermineEmpireExpansion(empireStart.Age, rnd);
        empireStart.ResolvedRace = race;
        empireStart.ProjectedColonyAmount = empireExpansion;
      }
      this.Sort();
      this.Reverse();
    }

    public int IndexOf(Race race)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].ResolvedRace == race)
            return index;
        }
        return -1;
      }
    }

    public int TotalColoniesForRace(Race race)
    {
      int num = 0;
      foreach (EmpireStart empireStart in (SyncList<EmpireStart>) this)
      {
        if (empireStart.ResolvedRace == race)
          num += empireStart.ProjectedColonyAmount;
      }
      return num;
    }

    public int TotalColonyAmount
    {
      get
      {
        int totalColonyAmount = 0;
        foreach (EmpireStart empireStart in (SyncList<EmpireStart>) this)
          totalColonyAmount += empireStart.ProjectedColonyAmount;
        return totalColonyAmount;
      }
    }

    private Race ResolveRace(
      RaceList races,
      string raceName,
      Random rnd,
      string raceNameToExcludeWhenSelectingRandomRaces)
    {
      if (raceName.ToLower(CultureInfo.InvariantCulture) == "(" + TextResolver.GetText("random") + ")")
        return this.SelectRandomUnusedRace(races, rnd, raceNameToExcludeWhenSelectingRandomRaces);
      foreach (Race race in (SyncList<Race>) races)
      {
        if (race.Name == raceName)
          return race;
      }
      return (Race) null;
    }

    public Race SelectRandomUnusedRace(
      RaceList races,
      Random rnd,
      string raceNameToExcludeWhenSelectingRandomRaces)
    {
      RaceList raceList1 = races.ResolvePlayableRaces();
      RaceList raceList2 = new RaceList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResolvedRace != null && !raceList2.Contains(this[index].ResolvedRace))
          raceList2.Add(this[index].ResolvedRace);
      }
      RaceList raceList3 = new RaceList();
      for (int index = 0; index < raceList1.Count; ++index)
      {
        if (!raceList2.Contains(raceList1[index]) && (string.IsNullOrEmpty(raceNameToExcludeWhenSelectingRandomRaces) || raceList1[index].Name != raceNameToExcludeWhenSelectingRandomRaces))
          raceList3.Add(raceList1[index]);
      }
      return raceList3.Count > 0 ? raceList3[rnd.Next(0, raceList3.Count)] : raceList1[rnd.Next(0, raceList1.Count)];
    }

    public Race SelectRandomRacePreferHospitableHabitats(
      RaceList races,
      int intelligenceThreshhold,
      Random rnd)
    {
      RaceList raceList = new RaceList();
      foreach (Race race in (SyncList<Race>) races)
      {
        if (race.IntelligenceLevel >= intelligenceThreshhold)
        {
          int num = 1;
          switch (race.NativeHabitatType)
          {
            case HabitatType.Volcanic:
              num = 1;
              break;
            case HabitatType.Desert:
              num = 3;
              break;
            case HabitatType.MarshySwamp:
              num = 4;
              break;
            case HabitatType.Continental:
              num = 5;
              break;
            case HabitatType.Ocean:
              num = 2;
              break;
            case HabitatType.BarrenRock:
              num = 0;
              break;
            case HabitatType.Ice:
              num = 1;
              break;
          }
          for (int index = 0; index < num; ++index)
            raceList.Add(race);
        }
      }
      Race race1 = (Race) null;
      if (raceList.Count > 0)
        race1 = raceList[rnd.Next(0, raceList.Count)];
      return race1;
    }

    private double DetermineEmpireExpansion(int age, Random rnd)
    {
      double empireExpansion = 1.0;
      double num1 = Galaxy.EmpireAgeExpansionRateMaximum - Galaxy.EmpireAgeExpansionRateMinimum;
      for (int index = 0; index < age; ++index)
      {
        double num2 = Galaxy.EmpireAgeExpansionRateMinimum + rnd.NextDouble() * num1;
        empireExpansion *= num2;
      }
      return empireExpansion;
    }
  }
}
