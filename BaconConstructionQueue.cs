// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconConstructionQueue
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;
using System;

namespace BaconDistantWorlds
{
  public static class BaconConstructionQueue
  {
    public static double myConstructionSpeedMultiplier = 5.0;

    public static double ConstructionSpeedMultiplier(ConstructionQueue queue)
    {
      double num = 1.0;
      if (queue != null && queue.Empire != null && queue.Empire.Name.Contains("Romulan"))
        num = BaconConstructionQueue.myConstructionSpeedMultiplier;
      return num;
    }

    public static void MoveCargoFromBuilderToBuiltBase(
      BuiltObject constructionShip,
      BuiltObject builtObject)
    {
      Empire actualEmpire = builtObject.ActualEmpire;
      int num = -1;
      if (actualEmpire != null)
        num = actualEmpire.EmpireId;
      CargoList cargoList = new CargoList();
      for (int index = 0; index < constructionShip.Cargo.Count; ++index)
      {
        Cargo cargo = constructionShip.Cargo[index];
        if (cargo != null && cargo.EmpireId == num && cargo.CommodityResource != null && cargo.Amount > 0)
        {
          int amount = Math.Min(cargo.Amount, 100);
          builtObject.Cargo.Add(new Cargo(cargo.CommodityResource, amount, cargo.EmpireId));
          cargo.Amount -= amount;
        }
      }
    }

    public static void ReviewConstructionSpeed(ConstructionQueue conQueue)
    {
      if (conQueue.ParentHabitat == null)
      {
        conQueue._ConstructionSpeed = 1;
        ResearchSystem researchSystem = (ResearchSystem) null;
        if (conQueue.ParentBuiltObject != null && conQueue.ParentBuiltObject.Empire != null)
          researchSystem = conQueue.ParentBuiltObject.Empire.Research;
        double val1 = 1.0;
        double num1 = 1.0;
        double num2 = 1.0;
        if (conQueue.ParentBuiltObject != null && conQueue.ParentBuiltObject.ParentHabitat != null)
        {
          if (conQueue.ParentBuiltObject.ParentHabitat.Facilities != null)
          {
            for (int index = 0; index < conQueue.ParentBuiltObject.ParentHabitat.Facilities.Count; ++index)
            {
              PlanetaryFacility facility = conQueue.ParentBuiltObject.ParentHabitat.Facilities[index];
              if (facility != null && facility.Type == PlanetaryFacilityType.Wonder && facility.WonderType == WonderType.ColonyConstructionSpeed && (double) facility.ConstructionProgress >= 1.0)
                val1 = Math.Max(val1, 1.0 + (double) facility.Value2 / 100.0);
            }
          }
          if (conQueue.ParentBuiltObject.ParentHabitat.ResourceBonuses != null)
          {
            double totalByEffectType = conQueue.ParentBuiltObject.ParentHabitat.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.ConstructionSpeed);
            if (totalByEffectType > 0.0)
              num1 = 1.0 + Math.Max(0.0, Math.Min(100.0, totalByEffectType)) / 100.0;
          }
          if (conQueue.ParentBuiltObject.ParentHabitat != null && conQueue.ParentBuiltObject.ParentHabitat.Population != null && conQueue.ParentBuiltObject.ParentHabitat.Population.DominantRace != null)
          {
            Race dominantRace = conQueue.ParentBuiltObject.ParentHabitat.Population.DominantRace;
            num2 *= dominantRace.ConstructionSpeedModifier * BaconConstructionQueue.ConstructionSpeedMultiplier(conQueue);
            switch (conQueue.ParentBuiltObject.ParentHabitat.Type)
            {
              case HabitatType.Volcanic:
                if (dominantRace.ColonyConstructionSpeedFactorVolcanic != 1.0)
                {
                  num2 *= dominantRace.ColonyConstructionSpeedFactorVolcanic;
                  break;
                }
                break;
              case HabitatType.Desert:
                if (dominantRace.ColonyConstructionSpeedFactorDesert != 1.0)
                {
                  num2 *= dominantRace.ColonyConstructionSpeedFactorDesert;
                  break;
                }
                break;
              case HabitatType.MarshySwamp:
                if (dominantRace.ColonyConstructionSpeedFactorMarshySwamp != 1.0)
                {
                  num2 *= dominantRace.ColonyConstructionSpeedFactorMarshySwamp;
                  break;
                }
                break;
              case HabitatType.Continental:
                if (dominantRace.ColonyConstructionSpeedFactorContinental != 1.0)
                {
                  num2 *= dominantRace.ColonyConstructionSpeedFactorContinental;
                  break;
                }
                break;
              case HabitatType.Ocean:
                if (dominantRace.ColonyConstructionSpeedFactorOcean != 1.0)
                {
                  num2 *= dominantRace.ColonyConstructionSpeedFactorOcean;
                  break;
                }
                break;
              case HabitatType.Ice:
                if (dominantRace.ColonyConstructionSpeedFactorIce != 1.0)
                {
                  num2 *= dominantRace.ColonyConstructionSpeedFactorIce;
                  break;
                }
                break;
            }
          }
        }
        if (conQueue.ConstructionYards == null)
          return;
        foreach (ConstructionYard constructionYard in (SyncList<ConstructionYard>) conQueue.ConstructionYards)
        {
          if (researchSystem != null)
          {
            ComponentImprovement componentImprovement = researchSystem.ResolveImprovedComponentValues(new Component((int) constructionYard.ComponentId));
            constructionYard.ConstructionSpeed = (int) ((double) componentImprovement.Value1 * val1 * num1 * num2);
            if (constructionYard.ConstructionSpeed > conQueue.ConstructionSpeed)
              conQueue._ConstructionSpeed = constructionYard.ConstructionSpeed;
          }
          else if (constructionYard.ConstructionSpeed > conQueue.ConstructionSpeed)
            conQueue._ConstructionSpeed = constructionYard.ConstructionSpeed;
        }
      }
      else
      {
        double num3 = 10000000.0;
        if (conQueue.ParentHabitat.Population != null && conQueue.ParentHabitat.Population.Count > 0)
          num3 = (double) conQueue.ParentHabitat.Population.TotalAmount;
        int num4 = (int) (600.0 * Math.Min(1.0, Math.Sqrt(num3 / Galaxy.ColonyBuildSpeedIdealPopulation)));
        if (conQueue.ParentHabitat.Facilities != null)
        {
          double num5 = 0.0;
          for (int index = 0; index < conQueue.ParentHabitat.Facilities.Count; ++index)
          {
            PlanetaryFacility facility = conQueue.ParentHabitat.Facilities[index];
            if (facility != null && facility.Type == PlanetaryFacilityType.Wonder && facility.WonderType == WonderType.ColonyConstructionSpeed && (double) facility.ConstructionProgress >= 1.0)
              num5 = (double) facility.Value2 / 100.0;
          }
          if (num5 > 0.0)
            num4 = (int) ((double) num4 * (1.0 + num5));
        }
        if (conQueue.ParentHabitat.ResourceBonuses != null)
        {
          double totalByEffectType = conQueue.ParentHabitat.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.ConstructionSpeed);
          if (totalByEffectType > 0.0)
          {
            double num6 = 1.0 + Math.Max(0.0, Math.Min(100.0, totalByEffectType)) / 100.0;
            num4 = (int) ((double) num4 * num6);
          }
        }
        if (conQueue.ParentHabitat.Population != null && conQueue.ParentHabitat.Population.DominantRace != null)
        {
          Race dominantRace = conQueue.ParentHabitat.Population.DominantRace;
          num4 = (int) ((double) num4 * dominantRace.ConstructionSpeedModifier * BaconConstructionQueue.ConstructionSpeedMultiplier(conQueue));
          switch (conQueue.ParentHabitat.Type)
          {
            case HabitatType.Volcanic:
              if (dominantRace.ColonyConstructionSpeedFactorVolcanic != 1.0)
              {
                num4 = (int) ((double) num4 * dominantRace.ColonyConstructionSpeedFactorVolcanic);
                break;
              }
              break;
            case HabitatType.Desert:
              if (dominantRace.ColonyConstructionSpeedFactorDesert != 1.0)
              {
                num4 = (int) ((double) num4 * dominantRace.ColonyConstructionSpeedFactorDesert);
                break;
              }
              break;
            case HabitatType.MarshySwamp:
              if (dominantRace.ColonyConstructionSpeedFactorMarshySwamp != 1.0)
              {
                num4 = (int) ((double) num4 * dominantRace.ColonyConstructionSpeedFactorMarshySwamp);
                break;
              }
              break;
            case HabitatType.Continental:
              if (dominantRace.ColonyConstructionSpeedFactorContinental != 1.0)
              {
                num4 = (int) ((double) num4 * dominantRace.ColonyConstructionSpeedFactorContinental);
                break;
              }
              break;
            case HabitatType.Ocean:
              if (dominantRace.ColonyConstructionSpeedFactorOcean != 1.0)
              {
                num4 = (int) ((double) num4 * dominantRace.ColonyConstructionSpeedFactorOcean);
                break;
              }
              break;
            case HabitatType.Ice:
              if (dominantRace.ColonyConstructionSpeedFactorIce != 1.0)
              {
                num4 = (int) ((double) num4 * dominantRace.ColonyConstructionSpeedFactorIce);
                break;
              }
              break;
          }
        }
        int val1_1 = 0;
        int val1_2 = 0;
        int val1_3 = 0;
        if (conQueue.ParentHabitat != null)
        {
          if (conQueue.ParentHabitat.Characters != null && conQueue.ParentHabitat.Characters.Count > 0)
          {
            val1_1 = Math.Max(val1_1, conQueue.ParentHabitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.MilitaryShipConstructionSpeed));
            val1_2 = Math.Max(val1_2, conQueue.ParentHabitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.CivilianShipConstructionSpeed));
            val1_3 = Math.Max(val1_3, conQueue.ParentHabitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyShipConstructionSpeed));
          }
        }
        else if (conQueue.ParentBuiltObject != null)
        {
          if (conQueue.ParentBuiltObject.Characters != null && conQueue.ParentBuiltObject.Characters.Count > 0)
          {
            val1_1 = Math.Max(val1_1, conQueue.ParentBuiltObject.Characters.GetHighestSkillLevel(CharacterSkillType.MilitaryShipConstructionSpeed));
            val1_2 = Math.Max(val1_2, conQueue.ParentBuiltObject.Characters.GetHighestSkillLevel(CharacterSkillType.CivilianShipConstructionSpeed));
            val1_3 = Math.Max(val1_3, conQueue.ParentBuiltObject.Characters.GetHighestSkillLevel(CharacterSkillType.ColonyShipConstructionSpeed));
          }
          if (conQueue.ParentBuiltObject.ParentHabitat != null && conQueue.ParentBuiltObject.ParentHabitat.Characters != null && conQueue.ParentBuiltObject.ParentHabitat.Characters.Count > 0)
          {
            val1_1 = Math.Max(val1_1, conQueue.ParentBuiltObject.ParentHabitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.MilitaryShipConstructionSpeed));
            val1_2 = Math.Max(val1_2, conQueue.ParentBuiltObject.ParentHabitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.CivilianShipConstructionSpeed));
            val1_3 = Math.Max(val1_3, conQueue.ParentBuiltObject.ParentHabitat.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyShipConstructionSpeed));
          }
        }
        Empire empire = conQueue.Empire;
        if (empire != null && empire.Leader != null)
        {
          val1_1 += empire.Leader.MilitaryShipConstructionSpeed;
          val1_2 += empire.Leader.CivilianShipConstructionSpeed;
          val1_3 += empire.Leader.CivilianShipConstructionSpeed;
        }
        conQueue.MilitaryConstructionSpeedModifier = 1.0 + (double) val1_1 / 100.0;
        conQueue.CivilianConstructionSpeedModifier = 1.0 + (double) val1_2 / 100.0;
        conQueue.ColonyConstructionSpeedModifier = 1.0 + (double) val1_3 / 100.0;
        conQueue._ConstructionSpeed = num4;
        foreach (ConstructionYard constructionYard in (SyncList<ConstructionYard>) conQueue.ConstructionYards)
          constructionYard.ConstructionSpeed = num4;
      }
    }
  }
}
