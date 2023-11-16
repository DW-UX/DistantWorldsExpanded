// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.TroopList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class TroopList : SyncList<Troop>
  {
    public Troop GetFirstByTypeAndRace(TroopType type, Race race)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Troop firstByTypeAndRace = this[index];
        if (firstByTypeAndRace != null && firstByTypeAndRace.Type == type && firstByTypeAndRace.Race == race)
          return firstByTypeAndRace;
      }
      return (Troop) null;
    }

    public bool CheckTroopsOfEmpirePresent(Empire empire)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] != null && this[index].Empire == empire)
          return true;
      }
      return false;
    }

    public void SplitTroopsByType(
      bool includePirateRaidersWithInfantry,
      out TroopList infantryTroops,
      out TroopList armoredTroops,
      out TroopList artilleryTroops,
      out TroopList specialForcesTroops)
    {
      infantryTroops = new TroopList();
      armoredTroops = new TroopList();
      artilleryTroops = new TroopList();
      specialForcesTroops = new TroopList();
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null)
        {
          switch (troop.Type)
          {
            case TroopType.Infantry:
              infantryTroops.Add(troop);
              continue;
            case TroopType.Armored:
              armoredTroops.Add(troop);
              continue;
            case TroopType.Artillery:
              artilleryTroops.Add(troop);
              continue;
            case TroopType.SpecialForces:
              specialForcesTroops.Add(troop);
              continue;
            case TroopType.PirateRaider:
              if (includePirateRaidersWithInfantry)
              {
                infantryTroops.Add(troop);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }

    public void GetTroopCountsByType(
      out int infantryCount,
      out int artilleryCount,
      out int armorCount,
      out int specialForcesCount)
    {
      infantryCount = 0;
      artilleryCount = 0;
      armorCount = 0;
      specialForcesCount = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null)
        {
          switch (troop.Type)
          {
            case TroopType.Infantry:
              ++infantryCount;
              continue;
            case TroopType.Armored:
              ++armorCount;
              continue;
            case TroopType.Artillery:
              ++artilleryCount;
              continue;
            case TroopType.SpecialForces:
              ++specialForcesCount;
              continue;
            default:
              continue;
          }
        }
      }
    }

    public double GetArtilleryTroopDefendStrength()
    {
      double troopDefendStrength = 0.0;
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && troop.Type == TroopType.Artillery)
          troopDefendStrength += troop.OverallDefendStrength;
      }
      return troopDefendStrength;
    }

    public void GetTroopStrengthsByType(
      bool defending,
      out double infantryStrength,
      out double artilleryStrength,
      out double armorStrength,
      out double specialForcesStrength)
    {
      infantryStrength = 0.0;
      armorStrength = 0.0;
      artilleryStrength = 0.0;
      specialForcesStrength = 0.0;
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null)
        {
          switch (troop.Type)
          {
            case TroopType.Infantry:
            case TroopType.PirateRaider:
              if (defending)
              {
                infantryStrength += troop.OverallDefendStrength;
                continue;
              }
              infantryStrength += troop.OverallAttackStrength;
              continue;
            case TroopType.Armored:
              if (defending)
              {
                armorStrength += troop.OverallDefendStrength;
                continue;
              }
              armorStrength += troop.OverallAttackStrength;
              continue;
            case TroopType.Artillery:
              if (defending)
              {
                artilleryStrength += troop.OverallDefendStrength;
                continue;
              }
              artilleryStrength += troop.OverallAttackStrength;
              continue;
            case TroopType.SpecialForces:
              if (defending)
              {
                specialForcesStrength += troop.OverallDefendStrength;
                continue;
              }
              specialForcesStrength += troop.OverallAttackStrength;
              continue;
            default:
              continue;
          }
        }
      }
    }

    public int CountByType(TroopType troopType)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && troop.Type == troopType)
          ++num;
      }
      return num;
    }

    public int CountByType(TroopType troopType, out int defendingStrength)
    {
      int num = 0;
      defendingStrength = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && troop.Type == troopType)
        {
          ++num;
          defendingStrength += (int) troop.OverallDefendStrength;
        }
      }
      return num;
    }

    public TroopList GetByType(TroopType troopType)
    {
      TroopList byType = new TroopList();
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && troop.Type == troopType)
          byType.Add(troop);
      }
      return byType;
    }

    public TroopList GetByType(List<TroopType> troopTypes)
    {
      TroopList byType = new TroopList();
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && troopTypes.Contains(troop.Type))
          byType.Add(troop);
      }
      return byType;
    }

    public Troop GetFirstByName(string troopName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Troop firstByName = this[index];
        if (firstByName != null && firstByName.Name == troopName)
          return firstByName;
      }
      return (Troop) null;
    }

    public Troop GetFirstNonGarrisoned(TroopType troopType)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Troop firstNonGarrisoned = this[index];
        if (firstNonGarrisoned != null && (troopType == TroopType.Undefined || firstNonGarrisoned.Type == troopType) && !firstNonGarrisoned.Garrisoned)
          return firstNonGarrisoned;
      }
      return (Troop) null;
    }

    public Troop GetFirstNonGarrisonedWithinSize(TroopType troopType, int maximumSize)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Troop garrisonedWithinSize = this[index];
        if (garrisonedWithinSize != null && (troopType == TroopType.Undefined || garrisonedWithinSize.Type == troopType) && !garrisonedWithinSize.Garrisoned && garrisonedWithinSize.Size <= maximumSize)
          return garrisonedWithinSize;
      }
      return (Troop) null;
    }

    public int CountTroopsNotRecruiting()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        bool flag = true;
        if ((double) this[index].Readiness < 100.0 && this[index].AtColony && this[index].Colony != null && this[index].Colony.TroopsToRecruit.Contains(this[index]))
          flag = false;
        if (flag)
          ++num;
      }
      return num;
    }

    public int TotalAttackStrength
    {
      get
      {
        double totalAttackStrength = 0.0;
        foreach (Troop troop in (SyncList<Troop>) this)
          totalAttackStrength += (double) troop.AttackStrength * (double) troop.Readiness;
        return (int) totalAttackStrength;
      }
    }

    public int TotalDefendStrength
    {
      get
      {
        double totalDefendStrength = 0.0;
        foreach (Troop troop in (SyncList<Troop>) this)
          totalDefendStrength += (double) troop.DefendStrength * (double) troop.Readiness;
        return (int) totalDefendStrength;
      }
    }

    public int TotalDefendStrengthExcludeReadiness
    {
      get
      {
        double excludeReadiness = 0.0;
        foreach (Troop troop in (SyncList<Troop>) this)
          excludeReadiness += (double) troop.DefendStrength * 100.0;
        return (int) excludeReadiness;
      }
    }

    public int TotalAttackStrengthWithoutPickup
    {
      get
      {
        double strengthWithoutPickup = 0.0;
        foreach (Troop troop in (SyncList<Troop>) this)
        {
          if (!troop.AwaitingPickup)
            strengthWithoutPickup += (double) troop.AttackStrength * (double) troop.Readiness;
        }
        return (int) strengthWithoutPickup;
      }
    }

    public int TotalDefendStrengthWithoutPickup
    {
      get
      {
        double strengthWithoutPickup = 0.0;
        foreach (Troop troop in (SyncList<Troop>) this)
        {
          if (!troop.AwaitingPickup)
            strengthWithoutPickup += (double) troop.DefendStrength * (double) troop.Readiness;
        }
        return (int) strengthWithoutPickup;
      }
    }

    public int TotalDefendStrengthGarrisoned
    {
      get
      {
        double strengthGarrisoned = 0.0;
        foreach (Troop troop in (SyncList<Troop>) this)
        {
          if (troop.Garrisoned)
            strengthGarrisoned += (double) troop.DefendStrength * (double) troop.Readiness;
        }
        return (int) strengthGarrisoned;
      }
    }

    public int TotalDefendStrengthGarrisonedExcludeReadiness
    {
      get
      {
        int excludeReadiness = 0;
        foreach (Troop troop in (SyncList<Troop>) this)
        {
          if (troop.Garrisoned)
            excludeReadiness += troop.DefendStrength;
        }
        return excludeReadiness;
      }
    }

    public int TotalDefendStrengthNotGarrisoned
    {
      get
      {
        double strengthNotGarrisoned = 0.0;
        foreach (Troop troop in (SyncList<Troop>) this)
        {
          if (!troop.Garrisoned)
            strengthNotGarrisoned += (double) troop.DefendStrength * (double) troop.Readiness;
        }
        return (int) strengthNotGarrisoned;
      }
    }

    public int TotalDefendStrengthNotGarrisonedNotAwaitingPickup
    {
      get
      {
        double notAwaitingPickup = 0.0;
        foreach (Troop troop in (SyncList<Troop>) this)
        {
          if (!troop.Garrisoned && !troop.AwaitingPickup)
            notAwaitingPickup += (double) troop.DefendStrength * (double) troop.Readiness;
        }
        return (int) notAwaitingPickup;
      }
    }

    public TroopList GetTroopsNotGarrisonedNotAwaitingPickup()
    {
      TroopList notAwaitingPickup = new TroopList();
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && !troop.Garrisoned && !troop.AwaitingPickup)
          notAwaitingPickup.Add(troop);
      }
      return notAwaitingPickup;
    }

    public TroopList GetTroopsNotGarrisonedAtColony()
    {
      TroopList garrisonedAtColony = new TroopList();
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && !troop.Garrisoned && troop.AtColony && troop.Colony != null && troop.Colony.Troops != null && troop.Colony.Troops.Contains(troop))
          garrisonedAtColony.Add(troop);
      }
      return garrisonedAtColony;
    }

    public void RemoveTroopsByType(TroopType troopTypeToRemove, bool alsoRemoveFromEmpire)
    {
      TroopList troopList = new TroopList();
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && troop.Type == troopTypeToRemove)
          troopList.Add(troop);
      }
      for (int index = 0; index < troopList.Count; ++index)
      {
        Troop troop = troopList[index];
        if (troop != null)
        {
          this.Remove(troop);
          if (alsoRemoveFromEmpire && troop.Empire != null && troop.Empire.Troops != null && troop.Empire.Troops.Contains(troop))
            troop.Empire.Troops.Remove(troop);
        }
      }
    }

    public double AnnualTroopMaintenance(Empire empire)
    {
      double num1 = 0.0;
      double num2 = 1.0;
      if (empire != null && empire.Leader != null)
        num2 *= 1.0 + (double) empire.Leader.TroopMaintenance / 100.0;
      for (int index = 0; index < this.Count; ++index)
      {
        Troop troop = this[index];
        if (troop != null && !troop.BeingRecruited)
        {
          double num3 = Galaxy.TroopAnnualMaintenance * (double) troop.MaintenanceMultiplier;
          if (empire != null)
          {
            num3 *= (double) empire.TroopMaintenanceFactor;
            if (empire.GovernmentAttributes != null)
              num3 *= empire.GovernmentAttributes.MaintenanceCosts;
          }
          double num4 = num3 / num2;
          if (troop.Colony != null)
          {
            if (troop.Colony.Characters != null && troop.Colony.Characters.Count > 0)
            {
              double num5 = 1.0 + (double) troop.Colony.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopMaintenance) / 100.0;
              num4 /= num5;
            }
          }
          else if (troop.BuiltObject != null && troop.BuiltObject.Characters != null && troop.BuiltObject.Characters.Count > 0)
          {
            double num6 = 1.0 + (double) troop.BuiltObject.Characters.GetHighestSkillLevel(CharacterSkillType.TroopMaintenance) / 100.0;
            num4 /= num6;
          }
          num1 += num4;
        }
      }
      return num1;
    }

    public int TotalSize
    {
      get
      {
        int totalSize = 0;
        foreach (Troop troop in (SyncList<Troop>) this)
          totalSize += troop.Size;
        return totalSize;
      }
    }
  }
}
