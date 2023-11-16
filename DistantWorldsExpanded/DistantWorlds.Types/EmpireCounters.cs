// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireCounters
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireCounters
  {
    private object _LockObject = new object();
    private Empire _Empire;
    public int DestroyedEnemyMilitaryShipCount;
    public int DestroyedEnemyMilitaryShipSize;
    public int DestroyedEnemyMilitaryShipFirepower;
    public int DestroyedEnemyMilitaryShipCountEscort;
    public int DestroyedEnemyMilitaryShipCountFrigate;
    public int DestroyedEnemyMilitaryShipCountDestroyer;
    public int DestroyedEnemyMilitaryShipCountCruiser;
    public int DestroyedEnemyMilitaryShipCountCapitalShip;
    public int DestroyedEnemyMilitaryShipCountTroopTransport;
    public int DestroyedEnemyMilitaryShipCountCarrier;
    public int DestroyedEnemyMilitaryShipCountResupplyShip;
    public int DestroyedEnemyCivilianShipCount;
    public int DestroyedEnemyCivilianShipSize;
    public int DestroyedEnemyCivilianShipCountResearchStation;
    public int DestroyedEnemyCivilianShipCountMiningStation;
    public int DestroyedEnemyCivilianShipCountSpaceport;
    public int DestroyedEnemyCivilianShipCountDefensiveBase;
    public int DestroyedEnemyCivilianShipCountFreighter;
    public int DestroyedEnemyCivilianShipCountPassengerShip;
    public int DestroyedEnemyCivilianShipCountMiningShip;
    public int DestroyedEnemyCivilianShipCountOtherBases;
    public int DestroyedEnemyTroopCount;
    public int DestroyedCreatureCountKaltor;
    public int DestroyedCreatureCountSpaceSlug;
    public int DestroyedCreatureCountSandSlug;
    public int DestroyedCreatureCountArdilus;
    public int DestroyedCreatureCountSilverMist;
    public int ColoniesConqueredCount;
    public int IntelligenceMissionSuccessEspionageCount;
    public int IntelligenceMissionSuccessSabotageCount;
    public int IntelligenceMissionFailureEspionageCount;
    public int IntelligenceMissionFailureSabotageCount;
    public int IntelligenceMissionSuccessCounterIntelligenceCount;
    public int IntelligenceMissionAgentCapturedCount;
    public int LossesMilitaryShipCount;
    public int LossesMilitaryShipSize;
    public int LossesMilitaryShipFirepower;
    public int LossesCivilianShipCount;
    public int LossesCivilianShipSize;
    public int LossesSpaceportCount;
    public int LossesOtherBasesCount;
    public int LossesTroopCount;
    public int LossesColoniesTotalCount;
    public long LossesColoniesPopulationAmount;
    public int LossesColoniesContinentalCount;
    public int LossesColoniesMarshySwampCount;
    public int LossesColoniesOceanCount;
    public int LossesColoniesDesertCount;
    public int LossesColoniesIceCount;
    public int LossesColoniesVolcanicCount;
    public long ExterminatedPopulationAmount;
    public double TradeIncomeStateBonus;
    public double TradeIncomeTotalVolume;
    public double TourismIncome;
    public double ColonyPrivateRevenueTotal;
    public int WarsWeStartedCount;
    public int WarsDeclaredOnUsCount;
    public int SubjugationsMade;
    public int BrokenTreatyCount;
    public int KillEnemyCharactersCount;
    public int LossesCharactersKilledCount;
    public int EliminateEmpireCount;
    public int EliminateEmpireStrategicValue;
    public int MiningExtractionGas;
    public int MiningExtractionLuxury;
    public int MiningExtractionStrategic;
    [OptionalField]
    public int MiningExtractionColonyManufactured;
    public int BuildMilitaryShipCount;
    public int BuildCivilianShipCount;
    public int BuildBaseCount;
    public int CaptureShipCount;
    public int EliminatePirateEmpireCount;
    public int CompletedPirateMissionAttackCount;
    public int CompletedPirateMissionDefendCount;
    public double PirateSmugglingIncome;
    public double PirateProtectionIncome;
    [OptionalField]
    public int RaidSuccessCount;
    private long _TimeSpentAtWarExcludingCurrent;
    private long _AtWarStartDate = long.MaxValue;

    public Empire Empire => this._Empire;

    public EmpireCounters(Empire empire) => this._Empire = empire;

    public long TimeSpentAtWar(long starDate)
    {
      long num = 0;
      if (this._AtWarStartDate != long.MaxValue)
        num = starDate - this._AtWarStartDate;
      return this._TimeSpentAtWarExcludingCurrent + num;
    }

    public long AtWarStartDate => this._AtWarStartDate;

    public void FixupAtWarCounter(long starDate)
    {
      this._TimeSpentAtWarExcludingCurrent += starDate - this._AtWarStartDate;
      this._AtWarStartDate = long.MaxValue;
    }

    public void ProcessEmpireElimination(Empire empire, Galaxy galaxy, Empire thisEmpire)
    {
      if (empire == null)
        return;
      Empire empire1 = galaxy.IdentifyMechanoidEmpire();
      Empire empire2 = galaxy.IdentifyShakturiEmpire();
      if (empire1 != null && empire == empire1 && thisEmpire != null)
        thisEmpire.HaveDefeatedAncientGuardians = true;
      if (empire2 != null && empire == empire2 && thisEmpire != null)
        thisEmpire.HaveDefeatedShakturi = true;
      if (empire.PirateEmpireBaseHabitat == null)
      {
        ++this.EliminateEmpireCount;
        this.EliminateEmpireStrategicValue += empire.TotalColonyStrategicValue;
      }
      else
        ++this.EliminatePirateEmpireCount;
    }

    public void ProcessCharacterDeath(Character character)
    {
      if (character == null || character.Empire == this._Empire)
        return;
      ++this.KillEnemyCharactersCount;
      if (character.Empire == null)
        return;
      ++character.Empire.Counters.LossesCharactersKilledCount;
    }

    public void ProcessRelationChange(
      DiplomaticRelation relation,
      Empire initiator,
      DiplomaticRelationType newRelationType,
      long starDate)
    {
      this.ProcessRelationChange(relation, initiator, newRelationType, starDate, new DiplomaticRelationType?());
    }

    public void ProcessRelationChange(
      DiplomaticRelation relation,
      Empire initiator,
      DiplomaticRelationType newRelationType,
      long starDate,
      DiplomaticRelationType? previousRelationType)
    {
      if (!previousRelationType.HasValue)
        previousRelationType = new DiplomaticRelationType?(relation.Type);
      if (relation == null || initiator == null)
        return;
      switch (newRelationType)
      {
        case DiplomaticRelationType.None:
        case DiplomaticRelationType.SubjugatedDominion:
        case DiplomaticRelationType.TradeSanctions:
        case DiplomaticRelationType.War:
          ref DiplomaticRelationType? local = ref previousRelationType;
          DiplomaticRelationType valueOrDefault = local.GetValueOrDefault();
          if (local.HasValue)
          {
            switch (valueOrDefault)
            {
              case DiplomaticRelationType.FreeTradeAgreement:
              case DiplomaticRelationType.MutualDefensePact:
              case DiplomaticRelationType.Protectorate:
                if (initiator == this._Empire)
                {
                  ++this.BrokenTreatyCount;
                  break;
                }
                break;
            }
          }
          else
            break;
          break;
      }
      if (newRelationType == DiplomaticRelationType.SubjugatedDominion && initiator == this._Empire)
        ++this.SubjugationsMade;
      if (newRelationType == DiplomaticRelationType.War)
      {
        if (initiator == this._Empire)
          ++this.WarsWeStartedCount;
        else
          ++this.WarsDeclaredOnUsCount;
      }
      if (newRelationType == DiplomaticRelationType.War)
      {
        if (this._AtWarStartDate != long.MaxValue)
          return;
        this._AtWarStartDate = starDate;
      }
      else
      {
        DiplomaticRelationType? nullable = previousRelationType;
        if ((nullable.GetValueOrDefault() != DiplomaticRelationType.War ? 0 : (nullable.HasValue ? 1 : 0)) == 0 || relation.ThisEmpire.CheckAtWar(relation.OtherEmpire) || this._AtWarStartDate == long.MaxValue)
          return;
        this._TimeSpentAtWarExcludingCurrent += starDate - this._AtWarStartDate;
        this._AtWarStartDate = long.MaxValue;
      }
    }

    public void ProcessColonyRevenue(double amount) => this.ColonyPrivateRevenueTotal += amount;

    public void ProcessTourismIncome(double amount) => this.TourismIncome += amount;

    public void ProcessTradeBonus(DiplomaticRelation relation, double amount)
    {
      this.TradeIncomeTotalVolume += amount;
      if (relation == null)
        return;
      this.TradeIncomeStateBonus += relation.TradeBonus * amount;
    }

    public void ProcessExterminatedPopulation(long amount) => this.ExterminatedPopulationAmount += amount;

    public void ProcessIntelligenceMissionOutcome(
      IntelligenceMission mission,
      IntelligenceMissionOutcome outcome)
    {
      if (mission == null || outcome == IntelligenceMissionOutcome.Undefined)
        return;
      bool flag1 = false;
      bool flag2 = false;
      switch (outcome)
      {
        case IntelligenceMissionOutcome.SucceedNotDetect:
        case IntelligenceMissionOutcome.SucceedDetect:
          flag1 = true;
          break;
        case IntelligenceMissionOutcome.Capture:
          flag2 = true;
          break;
      }
      if (flag2)
        ++this.IntelligenceMissionAgentCapturedCount;
      switch (mission.Type)
      {
        case IntelligenceMissionType.SabotageConstruction:
        case IntelligenceMissionType.SabotageColony:
        case IntelligenceMissionType.InciteRevolution:
        case IntelligenceMissionType.AssassinateCharacter:
        case IntelligenceMissionType.DestroyBase:
          if (flag1)
          {
            ++this.IntelligenceMissionSuccessSabotageCount;
            break;
          }
          ++this.IntelligenceMissionFailureSabotageCount;
          break;
        case IntelligenceMissionType.StealGalaxyMap:
        case IntelligenceMissionType.StealOperationsMap:
        case IntelligenceMissionType.StealTechData:
        case IntelligenceMissionType.DeepCover:
        case IntelligenceMissionType.StealTerritoryMap:
          if (flag1)
          {
            ++this.IntelligenceMissionSuccessEspionageCount;
            break;
          }
          ++this.IntelligenceMissionFailureEspionageCount;
          break;
        case IntelligenceMissionType.CounterIntelligence:
          if (!flag1)
            break;
          ++this.IntelligenceMissionSuccessCounterIntelligenceCount;
          break;
      }
    }

    public void ProcessColonyConquest(Habitat colony, Empire previousOwner)
    {
      if (colony == null)
        return;
      ++this.ColoniesConqueredCount;
      if (previousOwner == null)
        return;
      ++previousOwner.Counters.LossesColoniesTotalCount;
      if (colony.Population != null)
        previousOwner.Counters.LossesColoniesPopulationAmount += colony.Population.TotalAmount;
      switch (colony.Type)
      {
        case HabitatType.Volcanic:
          ++previousOwner.Counters.LossesColoniesVolcanicCount;
          break;
        case HabitatType.Desert:
          ++previousOwner.Counters.LossesColoniesDesertCount;
          break;
        case HabitatType.MarshySwamp:
          ++previousOwner.Counters.LossesColoniesMarshySwampCount;
          break;
        case HabitatType.Continental:
          ++previousOwner.Counters.LossesColoniesContinentalCount;
          break;
        case HabitatType.Ocean:
          ++previousOwner.Counters.LossesColoniesOceanCount;
          break;
        case HabitatType.Ice:
          ++previousOwner.Counters.LossesColoniesIceCount;
          break;
      }
    }

    public void ProcessBuiltObjectConstruction(BuiltObject builtObject)
    {
      if (builtObject == null)
        return;
      if (builtObject.Role == BuiltObjectRole.Base)
        ++this.BuildBaseCount;
      else if (builtObject.Role == BuiltObjectRole.Military)
        ++this.BuildMilitaryShipCount;
      else
        ++this.BuildCivilianShipCount;
    }

    public void ProcessBuiltObjectDestruction(BuiltObject builtObject)
    {
      if (builtObject == null)
        return;
      if (builtObject.Characters != null)
      {
        for (int index = 0; index < builtObject.Characters.Count; ++index)
          this.ProcessCharacterDeath(builtObject.Characters[index]);
      }
      if (builtObject.Troops != null)
      {
        for (int index = 0; index < builtObject.Troops.Count; ++index)
          this.ProcessTroopDestruction(builtObject.Troops[index]);
      }
      if (builtObject.Empire != null)
      {
        if (builtObject.Owner == null)
        {
          ++this.LossesCivilianShipCount;
          if (builtObject.Design != null)
            this.LossesCivilianShipSize += builtObject.Design.Size;
          if (builtObject.Role == BuiltObjectRole.Base && builtObject.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject.SubRole != BuiltObjectSubRole.LargeSpacePort)
            ++this.LossesOtherBasesCount;
        }
        else
        {
          switch (builtObject.SubRole)
          {
            case BuiltObjectSubRole.Escort:
            case BuiltObjectSubRole.Frigate:
            case BuiltObjectSubRole.Destroyer:
            case BuiltObjectSubRole.Cruiser:
            case BuiltObjectSubRole.CapitalShip:
            case BuiltObjectSubRole.TroopTransport:
            case BuiltObjectSubRole.Carrier:
            case BuiltObjectSubRole.ResupplyShip:
              ++builtObject.Empire.Counters.LossesMilitaryShipCount;
              if (builtObject.Design != null)
              {
                builtObject.Empire.Counters.LossesMilitaryShipFirepower += builtObject.Design.FirepowerRaw;
                builtObject.Empire.Counters.LossesMilitaryShipSize += builtObject.Design.Size;
                break;
              }
              break;
            case BuiltObjectSubRole.SmallSpacePort:
            case BuiltObjectSubRole.MediumSpacePort:
            case BuiltObjectSubRole.LargeSpacePort:
              ++builtObject.Empire.Counters.LossesSpaceportCount;
              break;
            default:
              if (builtObject.Role == BuiltObjectRole.Base)
              {
                ++this.LossesOtherBasesCount;
                break;
              }
              break;
          }
        }
      }
      if (builtObject.Owner == null)
      {
        ++this.DestroyedEnemyCivilianShipCount;
        if (builtObject.Design != null)
          this.DestroyedEnemyCivilianShipSize += builtObject.Design.Size;
        switch (builtObject.SubRole)
        {
          case BuiltObjectSubRole.SmallFreighter:
          case BuiltObjectSubRole.MediumFreighter:
          case BuiltObjectSubRole.LargeFreighter:
            ++this.DestroyedEnemyCivilianShipCountFreighter;
            break;
          case BuiltObjectSubRole.PassengerShip:
            ++this.DestroyedEnemyCivilianShipCountPassengerShip;
            break;
          case BuiltObjectSubRole.GasMiningShip:
          case BuiltObjectSubRole.MiningShip:
            ++this.DestroyedEnemyCivilianShipCountMiningShip;
            break;
          case BuiltObjectSubRole.GasMiningStation:
          case BuiltObjectSubRole.MiningStation:
            ++this.DestroyedEnemyCivilianShipCountMiningStation;
            break;
          default:
            if (builtObject.Role != BuiltObjectRole.Base)
              break;
            ++this.DestroyedEnemyCivilianShipCountOtherBases;
            break;
        }
      }
      else
      {
        if (builtObject.Role == BuiltObjectRole.Military)
        {
          ++this.DestroyedEnemyMilitaryShipCount;
          if (builtObject.Design != null)
          {
            this.DestroyedEnemyMilitaryShipFirepower += builtObject.Design.FirepowerRaw;
            this.DestroyedEnemyMilitaryShipSize += builtObject.Design.Size;
          }
        }
        switch (builtObject.SubRole)
        {
          case BuiltObjectSubRole.Escort:
            ++this.DestroyedEnemyMilitaryShipCountEscort;
            break;
          case BuiltObjectSubRole.Frigate:
            ++this.DestroyedEnemyMilitaryShipCountFrigate;
            break;
          case BuiltObjectSubRole.Destroyer:
            ++this.DestroyedEnemyMilitaryShipCountDestroyer;
            break;
          case BuiltObjectSubRole.Cruiser:
            ++this.DestroyedEnemyMilitaryShipCountCruiser;
            break;
          case BuiltObjectSubRole.CapitalShip:
            ++this.DestroyedEnemyMilitaryShipCountCapitalShip;
            break;
          case BuiltObjectSubRole.TroopTransport:
            ++this.DestroyedEnemyMilitaryShipCountTroopTransport;
            break;
          case BuiltObjectSubRole.Carrier:
            ++this.DestroyedEnemyMilitaryShipCountCarrier;
            break;
          case BuiltObjectSubRole.ResupplyShip:
            ++this.DestroyedEnemyMilitaryShipCountResupplyShip;
            break;
          case BuiltObjectSubRole.SmallSpacePort:
          case BuiltObjectSubRole.MediumSpacePort:
          case BuiltObjectSubRole.LargeSpacePort:
            ++this.DestroyedEnemyCivilianShipCountSpaceport;
            break;
          case BuiltObjectSubRole.EnergyResearchStation:
          case BuiltObjectSubRole.WeaponsResearchStation:
          case BuiltObjectSubRole.HighTechResearchStation:
            ++this.DestroyedEnemyCivilianShipCountResearchStation;
            break;
          case BuiltObjectSubRole.DefensiveBase:
            ++this.DestroyedEnemyCivilianShipCountDefensiveBase;
            break;
          default:
            if (builtObject.Role != BuiltObjectRole.Base)
              break;
            ++this.DestroyedEnemyCivilianShipCountOtherBases;
            break;
        }
      }
    }

    public void ProcessCreatureDeath(Creature creature)
    {
      if (creature == null)
        return;
      switch (creature.Type)
      {
        case CreatureType.Kaltor:
          ++this.DestroyedCreatureCountKaltor;
          break;
        case CreatureType.RockSpaceSlug:
          ++this.DestroyedCreatureCountSpaceSlug;
          break;
        case CreatureType.DesertSpaceSlug:
          ++this.DestroyedCreatureCountSandSlug;
          break;
        case CreatureType.Ardilus:
          ++this.DestroyedCreatureCountArdilus;
          break;
        case CreatureType.SilverMist:
          ++this.DestroyedCreatureCountSilverMist;
          break;
      }
    }

    public void ProcessTroopDestruction(Troop troop)
    {
      if (troop == null)
        return;
      ++this.DestroyedEnemyTroopCount;
      if (troop.Empire == null)
        return;
      ++troop.Empire.Counters.LossesTroopCount;
    }
  }
}
