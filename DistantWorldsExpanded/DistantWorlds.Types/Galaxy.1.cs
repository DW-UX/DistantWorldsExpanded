using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public partial class Galaxy
    {
        public int CountColoniesByType(HabitatType type)
        {
            int num = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire == null || empire == IndependentEmpire || !empire.Active)
                {
                    continue;
                }
                for (int j = 0; j < empire.Colonies.Count; j++)
                {
                    Habitat habitat = empire.Colonies[j];
                    if (habitat != null && !habitat.HasBeenDestroyed && habitat.Type == type)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int CountPirateControlledColonies()
        {
            int num = 0;
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire == null || empire == IndependentEmpire || !empire.Active)
                {
                    continue;
                }
                for (int j = 0; j < empire.Colonies.Count; j++)
                {
                    Habitat habitat = empire.Colonies[j];
                    if (habitat != null && !habitat.HasBeenDestroyed && habitat.GetPirateControl().Count > 0)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public HabitatList DetermineLargestColoniesByType(HabitatType type)
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire == null || empire == IndependentEmpire || !empire.Active)
                {
                    continue;
                }
                for (int j = 0; j < empire.Colonies.Count; j++)
                {
                    Habitat habitat = empire.Colonies[j];
                    if (habitat != null && !habitat.HasBeenDestroyed && habitat.Type == type)
                    {
                        habitatList.Add(habitat);
                    }
                }
            }
            habitatList.Sort();
            habitatList.Reverse();
            return habitatList;
        }

        private void CheckVictoryConditions(Empire playerEmpire, VictoryConditions globalVictoryConditions, EmpireVictoryConditions playerConditionsToAchieve, EmpireVictoryConditions playerConditionsToPrevent)
        {
            long num = CurrentStarDate;
            if (globalVictoryConditions != null && globalVictoryConditions.StartDate > 0)
            {
                num = globalVictoryConditions.StartDate;
            }
            if (CurrentStarDate < num)
            {
                return;
            }
            string description = string.Empty;
            if (globalVictoryConditions != null)
            {
                if (globalVictoryConditions.TimeLimit && CurrentStarDate >= globalVictoryConditions.TimeLimitDate)
                {
                    Empire empire = null;
                    int num2 = 0;
                    for (int i = 0; i < Empires.Count; i++)
                    {
                        Empire empire2 = Empires[i];
                        if (empire2.TotalColonyStrategicValue > num2 && empire2.DominantRace != null && empire2.DominantRace.Playable)
                        {
                            empire = empire2;
                            num2 = empire2.TotalColonyStrategicValue;
                        }
                    }
                    if (empire != null)
                    {
                        GameEndOutcome gameEndOutcome = GameEndOutcome.Undefined;
                        if (empire == playerEmpire)
                        {
                            gameEndOutcome = GameEndOutcome.Victory;
                            description += string.Format(TextResolver.GetText("Victory Conditions Time Limit Win"), ResolveStarDateDescription(globalVictoryConditions.TimeLimitDate));
                        }
                        else
                        {
                            gameEndOutcome = GameEndOutcome.Defeat;
                            description += string.Format(TextResolver.GetText("Victory Conditions Time Limit Lose"), ResolveStarDateDescription(globalVictoryConditions.TimeLimitDate), empire.Name);
                        }
                        OnGameEnd(new GameEndEventArgs(empire, gameEndOutcome, description, 0));
                    }
                }
                int code = 0;
                Empire empire3 = CheckGlobalVictoryConditions(playerEmpire, globalVictoryConditions, out description, out code);
                if (empire3 != null)
                {
                    if (empire3 == playerEmpire)
                    {
                        OnGameEnd(new GameEndEventArgs(empire3, GameEndOutcome.Victory, description, code));
                    }
                    else
                    {
                        OnGameEnd(new GameEndEventArgs(empire3, GameEndOutcome.Defeat, description, code));
                    }
                }
            }
            if (CheckEmpireVictoryConditionsToAchieve(playerConditionsToAchieve, playerEmpire))
            {
                OnGameEnd(new GameEndEventArgs(playerEmpire, GameEndOutcome.Victory, description, 0));
            }
            if (CheckEmpireVictoryConditionsToPrevent(playerConditionsToPrevent, playerEmpire))
            {
                OnGameEnd(new GameEndEventArgs(null, GameEndOutcome.Defeat, description, 0));
            }
        }

        public Empire CheckVictoryConditionsWinner(bool requireReachVictoryThreshold)
        {
            if (GlobalVictoryConditions != null)
            {
                VictoryConditionProgressList victoryConditionProgressList = GenerateVictoryConditionProgresses(this, GlobalVictoryConditions, filterOutUnmetEmpires: false);
                VictoryConditionProgressList victoryConditionProgressList2 = new VictoryConditionProgressList();
                for (int i = 0; i < victoryConditionProgressList.Count; i++)
                {
                    if (!requireReachVictoryThreshold || victoryConditionProgressList[i].TotalProgress >= GlobalVictoryConditions.VictoryThresholdPercentage)
                    {
                        victoryConditionProgressList2.Add(victoryConditionProgressList[i]);
                    }
                }
                VictoryConditionProgress victoryConditionProgress = null;
                if (victoryConditionProgressList2.Count > 0)
                {
                    for (int j = 0; j < victoryConditionProgressList2.Count; j++)
                    {
                        if (victoryConditionProgress == null || victoryConditionProgressList2[j].TotalProgress > victoryConditionProgress.TotalProgress || (victoryConditionProgressList2[j].TotalProgress == victoryConditionProgress.TotalProgress && victoryConditionProgressList2[j].Empire.TotalColonyStrategicValue > victoryConditionProgress.Empire.TotalColonyStrategicValue))
                        {
                            victoryConditionProgress = victoryConditionProgressList2[j];
                        }
                    }
                }
                if (victoryConditionProgress != null)
                {
                    return victoryConditionProgress.Empire;
                }
            }
            return null;
        }

        public static VictoryConditionProgressList GenerateVictoryConditionProgresses(Galaxy galaxy, VictoryConditions globalVictoryConditions, bool filterOutUnmetEmpires)
        {
            VictoryConditionProgressList victoryConditionProgressList = new VictoryConditionProgressList();
            long currentStarDate = galaxy.CurrentStarDate;
            if (globalVictoryConditions != null)
            {
                Empire empire = galaxy.IdentifyShakturiEmpire();
                Empire empire2 = galaxy.IdentifyMechanoidEmpire();
                EmpireList empireList = galaxy.Empires;
                if (galaxy.PlayerEmpire.PirateEmpireBaseHabitat != null)
                {
                    empireList = galaxy.PirateEmpires;
                }
                long num = 0L;
                double num2 = 0.0;
                double num3 = 0.0;
                for (int i = 0; i < empireList.Count; i++)
                {
                    Empire empire3 = empireList[i];
                    if (empire3 != null && empire3.Active && empire3 != galaxy.IndependentEmpire && empire3 != empire && empire3 != empire2)
                    {
                        if (galaxy.PlayerEmpire.PirateEmpireBaseHabitat != null)
                        {
                            HabitatList ownedColonies = new HabitatList();
                            HabitatList pirateControlledColonies = empire3.Colonies.GetPirateControlledColonies(empire3, out ownedColonies);
                            long num4 = ownedColonies.TotalPopulation();
                            long num5 = pirateControlledColonies.TotalPopulation();
                            num += num4 + num5 / 2;
                            num2 += empire3.CalculateAccurateAnnualIncome();
                            num3 += (double)ownedColonies.Count;
                            num3 += (double)pirateControlledColonies.Count / 2.0;
                        }
                        else
                        {
                            num += empire3.TotalPopulation;
                            num2 += empire3.PrivateAnnualRevenue;
                            num3 += (double)empire3.Colonies.Count;
                        }
                    }
                }
                num = Math.Max(100L, num);
                num2 = Math.Max(1.0, num2);
                num3 = Math.Max(1.0, num3);
                long num6 = (long)((double)num * (globalVictoryConditions.PopulationPercent / 100.0));
                double num7 = num2 * (globalVictoryConditions.EconomyPercent / 100.0);
                int num8 = (int)(0.99 + num3 * (globalVictoryConditions.TerritoryPercent / 100.0));
                victoryConditionProgressList = new VictoryConditionProgressList();
                for (int j = 0; j < empireList.Count; j++)
                {
                    Empire empire4 = empireList[j];
                    if (empire4 == null || !empire4.Active || empire4 == galaxy.IndependentEmpire || empire4 == empire || empire4 == empire2)
                    {
                        continue;
                    }
                    bool flag = true;
                    if (filterOutUnmetEmpires && empire4 != galaxy.PlayerEmpire)
                    {
                        if (empire4.PirateEmpireBaseHabitat == null && galaxy.PlayerEmpire.PirateEmpireBaseHabitat == null)
                        {
                            DiplomaticRelation diplomaticRelation = galaxy.PlayerEmpire.ObtainDiplomaticRelation(empire4);
                            if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            PirateRelation pirateRelation = galaxy.PlayerEmpire.ObtainPirateRelation(empire4);
                            if (pirateRelation.Type == PirateRelationType.NotMet)
                            {
                                flag = false;
                            }
                        }
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    RaceVictoryConditionProgressList conditionProgresses = null;
                    if (globalVictoryConditions.EnableRaceSpecificVictoryConditions)
                    {
                        CalculateRaceVictoryConditionsProgress(galaxy, empire4, empire4.DominantRace, out conditionProgresses);
                    }
                    double territoryPercent = 0.0;
                    double economyPercent = 0.0;
                    double populationPercent = 0.0;
                    double territoryProgress = 0.0;
                    double economyProgress = 0.0;
                    double populationProgress = 0.0;
                    if (globalVictoryConditions.Territory)
                    {
                        if (empire4.PirateEmpireBaseHabitat != null)
                        {
                            int ownedColonyCount = 0;
                            int num9 = empire4.Colonies.CountPirateControlledColonies(empire4, out ownedColonyCount);
                            double num10 = (double)ownedColonyCount + (double)num9 / 2.0;
                            territoryPercent = num10 / num3;
                            territoryProgress = Math.Max(0.0, Math.Min(1.0, num10 / (double)num8));
                        }
                        else
                        {
                            territoryPercent = (double)empire4.Colonies.Count / num3;
                            territoryProgress = Math.Max(0.0, Math.Min(1.0, (double)empire4.Colonies.Count / (double)num8));
                        }
                    }
                    if (globalVictoryConditions.Economy)
                    {
                        if (empire4.PirateEmpireBaseHabitat != null)
                        {
                            double num11 = empire4.CalculateAccurateAnnualIncome();
                            economyPercent = num11 / num2;
                            economyProgress = Math.Max(0.0, Math.Min(1.0, num11 / num7));
                        }
                        else
                        {
                            economyPercent = empire4.PrivateAnnualRevenue / num2;
                            economyProgress = Math.Max(0.0, Math.Min(1.0, empire4.PrivateAnnualRevenue / num7));
                        }
                    }
                    if (globalVictoryConditions.Population)
                    {
                        if (empire4.PirateEmpireBaseHabitat != null)
                        {
                            HabitatList ownedColonies2 = new HabitatList();
                            HabitatList pirateControlledColonies2 = empire4.Colonies.GetPirateControlledColonies(empire4, out ownedColonies2);
                            long num12 = ownedColonies2.TotalPopulation();
                            long num13 = pirateControlledColonies2.TotalPopulation();
                            long num14 = num12 + num13 / 2;
                            populationPercent = (double)num14 / (double)num;
                            populationProgress = Math.Max(0.0, Math.Min(1.0, (double)num14 / (double)num6));
                        }
                        else
                        {
                            populationPercent = (double)empire4.TotalPopulation / (double)num;
                            populationProgress = Math.Max(0.0, Math.Min(1.0, (double)empire4.TotalPopulation / (double)num6));
                        }
                    }
                    VictoryConditionProgress victoryConditionProgress = new VictoryConditionProgress(empire4, globalVictoryConditions.Territory, globalVictoryConditions.Economy, globalVictoryConditions.Population, territoryProgress, economyProgress, populationProgress, conditionProgresses);
                    victoryConditionProgress.TerritoryPercent = territoryPercent;
                    victoryConditionProgress.EconomyPercent = economyPercent;
                    victoryConditionProgress.PopulationPercent = populationPercent;
                    if (empire4.PirateEmpireBaseHabitat != null && empire4.Colonies != null)
                    {
                        long num15 = empire4.Colonies.TotalPopulationOwnedColonies(empire4);
                        if (num15 > 0)
                        {
                            double num16 = (double)num15 / 500000000.0;
                            num16 = (victoryConditionProgress.PirateBonusAmount = num16 / 100.0);
                        }
                    }
                    if (empire4.VictoryBonus != 0f)
                    {
                        victoryConditionProgress.BonusAmount += empire4.VictoryBonus;
                    }
                    double num17 = empire4.CalculateVictoryBonusFromStandingWonders(currentStarDate);
                    if (num17 > 0.0)
                    {
                        victoryConditionProgress.StandingWonderBonusAmount = num17;
                        victoryConditionProgress.BonusAmount += num17;
                    }
                    victoryConditionProgressList.Add(victoryConditionProgress);
                }
            }
            return victoryConditionProgressList;
        }

        public RaceVictoryConditionList ResolvePirateVictoryConditions(PiratePlayStyle playStyle)
        {
            RaceVictoryConditionList raceVictoryConditionList = new RaceVictoryConditionList();
            switch (playStyle)
            {
                case PiratePlayStyle.Balanced:
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateControlColoniesPercentage, 10.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateBuildCriminalNetwork, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateMostProtectionIncome, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.CaptureMostShips, 0.0, 20f));
                    break;
                case PiratePlayStyle.Pirate:
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateEliminateMostPirateFactions, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateControlColoniesPercentage, 10.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateMostSuccessfulRaids, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateBuildMostHiddenBases, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateBuildHiddenFortress, 0.0, 20f));
                    break;
                case PiratePlayStyle.Mercenary:
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateMostSuccessfulRaids, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.CaptureMostShips, 0.0, 20f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateBuildCriminalNetwork, 0.0, 20f));
                    break;
                case PiratePlayStyle.Smuggler:
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateMostSmugglingIncome, 0.0, 40f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateMostProtectionIncome, 0.0, 15f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.MostIntelligenceMissionsSucceed, 0.0, 15f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.ResearchMostAdvanced, 0.0, 15f));
                    raceVictoryConditionList.Add(new RaceVictoryCondition(RaceVictoryConditionType.PirateBuildCriminalNetwork, 0.0, 15f));
                    break;
            }
            return raceVictoryConditionList;
        }

        private Empire CheckGlobalVictoryConditions(Empire playerEmpire, VictoryConditions globalVictoryConditions, out string description, out int code)
        {
            description = string.Empty;
            code = 0;
            if (globalVictoryConditions != null)
            {
                long num = 0L;
                double num2 = 0.0;
                int num3 = 0;
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    num += empire.TotalPopulation;
                    num2 += empire.PrivateAnnualRevenue;
                    num3 += empire.Colonies.Count;
                }
                Empire empire2 = null;
                if (globalVictoryConditions.DefendHabitat != null && globalVictoryConditions.DefendHabitatEmpire != null && (globalVictoryConditions.DefendHabitat.HasBeenDestroyed || globalVictoryConditions.DefendHabitat.Empire != globalVictoryConditions.DefendHabitatEmpire))
                {
                    Empire empire3 = IdentifyShakturiEmpire();
                    if (empire3 != null)
                    {
                        empire2 = empire3;
                    }
                    else if (globalVictoryConditions.DefendHabitat.Empire != null && globalVictoryConditions.DefendHabitat.Empire != IndependentEmpire)
                    {
                        empire2 = globalVictoryConditions.DefendHabitat.Empire;
                        Empire empire4 = IdentifyMechanoidEmpire();
                        if (empire4 != null && globalVictoryConditions.DefendHabitatEmpire == empire4)
                        {
                            empire2.HaveDefeatedAncientGuardians = true;
                        }
                    }
                    code = 1;
                }
                Empire empire5 = null;
                if (globalVictoryConditions.TargetHabitat != null && globalVictoryConditions.TargetHabitatEmpire != null && (globalVictoryConditions.TargetHabitat.HasBeenDestroyed || globalVictoryConditions.TargetHabitat.Empire != globalVictoryConditions.TargetHabitatEmpire))
                {
                    empire5 = playerEmpire;
                    DecimateEmpire(globalVictoryConditions.TargetHabitatEmpire, empire5);
                    GuardiansDepart();
                    ShakturiDefeated = true;
                    empire5.HaveDefeatedShakturi = true;
                    code = 1;
                }
                Empire empire6 = CheckVictoryConditionsWinner(requireReachVictoryThreshold: true);
                Empire empire7 = null;
                if (empire2 != null)
                {
                    empire7 = empire2;
                    string text = empire7.Name;
                    if (empire7 == playerEmpire)
                    {
                        text = TextResolver.GetText("You");
                    }
                    if (globalVictoryConditions.DefendHabitat.HasBeenDestroyed)
                    {
                        description += string.Format(TextResolver.GetText("Victory Conditions Colony Destroy"), text, ResolveDescription(globalVictoryConditions.DefendHabitat.Category).ToLower(CultureInfo.InvariantCulture), globalVictoryConditions.DefendHabitat.Name);
                    }
                    else if (globalVictoryConditions.DefendHabitat.Empire == empire2)
                    {
                        description += string.Format(TextResolver.GetText("Victory Conditions Colony Invade"), text, ResolveDescription(globalVictoryConditions.DefendHabitat.Category).ToLower(CultureInfo.InvariantCulture), globalVictoryConditions.DefendHabitat.Name);
                    }
                    else
                    {
                        description += string.Format(TextResolver.GetText("Victory Conditions Colony Cause Loss"), text, ResolveDescription(globalVictoryConditions.DefendHabitat.Category).ToLower(CultureInfo.InvariantCulture), globalVictoryConditions.DefendHabitat.Name, globalVictoryConditions.DefendHabitatEmpire.Name);
                    }
                }
                if (empire5 != null)
                {
                    empire7 = empire5;
                    string text2 = empire7.Name;
                    if (empire7 == playerEmpire)
                    {
                        text2 = TextResolver.GetText("You");
                    }
                    if (globalVictoryConditions.TargetHabitat.HasBeenDestroyed)
                    {
                        description += string.Format(TextResolver.GetText("Victory Conditions Colony Destroy"), text2, ResolveDescription(globalVictoryConditions.TargetHabitat.Category).ToLower(CultureInfo.InvariantCulture), globalVictoryConditions.TargetHabitat.Name);
                    }
                    else if (globalVictoryConditions.TargetHabitat.Empire == empire2)
                    {
                        description += string.Format(TextResolver.GetText("Victory Conditions Colony Invade"), text2, ResolveDescription(globalVictoryConditions.TargetHabitat.Category).ToLower(CultureInfo.InvariantCulture), globalVictoryConditions.TargetHabitat.Name);
                    }
                    else
                    {
                        description += string.Format(TextResolver.GetText("Victory Conditions Colony Cause Loss"), text2, ResolveDescription(globalVictoryConditions.TargetHabitat.Category).ToLower(CultureInfo.InvariantCulture), globalVictoryConditions.TargetHabitat.Name, globalVictoryConditions.TargetHabitatEmpire.Name);
                    }
                }
                if (empire7 == null && empire6 != null)
                {
                    empire7 = empire6;
                    if (empire7 == playerEmpire)
                    {
                        description += TextResolver.GetText("Victory Conditions Threshold Win");
                    }
                    else
                    {
                        description += string.Format(TextResolver.GetText("Victory Conditions Threshold Lose"), empire7.Name);
                    }
                }
                return empire7;
            }
            return null;
        }

        private bool CheckEmpireVictoryConditionsToPrevent(EmpireVictoryConditions empireVictoryConditions, Empire playerEmpire)
        {
            if (empireVictoryConditions != null)
            {
                bool flag = true;
                if (empireVictoryConditions.CaptureColonies.Count > 0)
                {
                    foreach (Habitat captureColony in empireVictoryConditions.CaptureColonies)
                    {
                        if (captureColony.Owner == playerEmpire)
                        {
                            flag = false;
                        }
                    }
                }
                bool flag2 = true;
                if (empireVictoryConditions.EliminateEmpires.Count > 0)
                {
                    foreach (Empire eliminateEmpire in empireVictoryConditions.EliminateEmpires)
                    {
                        if (eliminateEmpire.Active)
                        {
                            flag2 = false;
                        }
                    }
                }
                bool flag3 = true;
                if (empireVictoryConditions.DestroyBuiltObjects.Count > 0)
                {
                    foreach (BuiltObject destroyBuiltObject in empireVictoryConditions.DestroyBuiltObjects)
                    {
                        if (BuiltObjects.Contains(destroyBuiltObject))
                        {
                            flag3 = false;
                        }
                    }
                }
                if (flag && flag2 && flag3 && (empireVictoryConditions.CaptureColonies.Count > 0 || empireVictoryConditions.EliminateEmpires.Count > 0 || empireVictoryConditions.DestroyBuiltObjects.Count > 0))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckEmpireVictoryConditionsToAchieve(EmpireVictoryConditions empireVictoryConditions, Empire playerEmpire)
        {
            if (empireVictoryConditions != null)
            {
                bool flag = true;
                if (empireVictoryConditions.CaptureColonies.Count > 0)
                {
                    foreach (Habitat captureColony in empireVictoryConditions.CaptureColonies)
                    {
                        if (captureColony.Owner != playerEmpire)
                        {
                            flag = false;
                        }
                    }
                }
                bool flag2 = true;
                if (empireVictoryConditions.EliminateEmpires.Count > 0)
                {
                    foreach (Empire eliminateEmpire in empireVictoryConditions.EliminateEmpires)
                    {
                        if (eliminateEmpire.Active)
                        {
                            flag2 = false;
                        }
                    }
                }
                bool flag3 = true;
                if (empireVictoryConditions.DestroyBuiltObjects.Count > 0)
                {
                    foreach (BuiltObject destroyBuiltObject in empireVictoryConditions.DestroyBuiltObjects)
                    {
                        if (BuiltObjects.Contains(destroyBuiltObject))
                        {
                            flag3 = false;
                        }
                    }
                }
                if (flag && flag2 && flag3 && (empireVictoryConditions.CaptureColonies.Count > 0 || empireVictoryConditions.EliminateEmpires.Count > 0 || empireVictoryConditions.DestroyBuiltObjects.Count > 0))
                {
                    return true;
                }
            }
            return false;
        }

        private Empire CheckVictoryTerritory(VictoryConditions victoryConditions, int totalTerritory)
        {
            if (victoryConditions.Territory)
            {
                EmpireList empireList = new EmpireList();
                int num = (int)(0.99 + (double)totalTerritory * (victoryConditions.TerritoryPercent / 100.0));
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    if (empire.Colonies.Count >= num && empire.DominantRace != null && empire.DominantRace.Playable)
                    {
                        empireList.Add(empire);
                    }
                }
                if (empireList.Count > 0)
                {
                    Empire result = null;
                    int num2 = 0;
                    {
                        foreach (Empire item in empireList)
                        {
                            if (item.Colonies.Count > num2)
                            {
                                result = item;
                                num2 = item.Colonies.Count;
                            }
                        }
                        return result;
                    }
                }
            }
            return null;
        }

        private Empire CheckVictoryEconomy(VictoryConditions victoryConditions, double totalEconomy)
        {
            if (victoryConditions.Economy)
            {
                EmpireList empireList = new EmpireList();
                double num = totalEconomy * (victoryConditions.EconomyPercent / 100.0);
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    if (empire.PrivateAnnualRevenue >= num && empire.DominantRace != null && empire.DominantRace.Playable)
                    {
                        empireList.Add(empire);
                    }
                }
                if (empireList.Count > 0)
                {
                    Empire result = null;
                    double num2 = 0.0;
                    {
                        foreach (Empire item in empireList)
                        {
                            if (item.PrivateAnnualRevenue > num2)
                            {
                                result = item;
                                num2 = item.PrivateAnnualRevenue;
                            }
                        }
                        return result;
                    }
                }
            }
            return null;
        }

        private Empire CheckVictoryPopulation(VictoryConditions victoryConditions, long totalPopulation)
        {
            if (victoryConditions.Population)
            {
                EmpireList empireList = new EmpireList();
                long num = (long)((double)totalPopulation * (victoryConditions.PopulationPercent / 100.0));
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    if (empire.TotalPopulation >= num && empire.DominantRace != null && empire.DominantRace.Playable)
                    {
                        empireList.Add(empire);
                    }
                }
                if (empireList.Count > 0)
                {
                    Empire result = null;
                    long num2 = 0L;
                    {
                        foreach (Empire item in empireList)
                        {
                            if (item.TotalPopulation > num2)
                            {
                                result = item;
                                num2 = item.TotalPopulation;
                            }
                        }
                        return result;
                    }
                }
            }
            return null;
        }

        public void GuardiansDepart()
        {
            Empire empire = null;
            Race race = null;
            for (int i = 0; i < Races.Count; i++)
            {
                if (Races[i].Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
                {
                    race = Races[i];
                    break;
                }
            }
            for (int j = 0; j < Empires.Count; j++)
            {
                if (Empires[j].DominantRace != null && Empires[j].DominantRace == race)
                {
                    empire = Empires[j];
                    break;
                }
            }
            if (empire != null)
            {
                GovernmentAttributes firstByAvailability = Governments.GetFirstByAvailability(2);
                if (firstByAvailability != null && !PlayerEmpire.AllowableGovernmentTypes.Contains(firstByAvailability.GovernmentId))
                {
                    PlayerEmpire.AllowableGovernmentTypes.Add(firstByAvailability.GovernmentId);
                }
                if (empire.Research != null && empire.Research.TechTree != null)
                {
                    for (int k = 0; k < empire.Research.TechTree.Count; k++)
                    {
                        if (empire.Research.TechTree[k].IsResearched)
                        {
                            ResearchNode researchNode = PlayerEmpire.Research.TechTree.FindNodeById(empire.Research.TechTree[k].ResearchNodeId);
                            if (researchNode != null && !researchNode.IsResearched)
                            {
                                PlayerEmpire.DoResearchBreakthrough(researchNode, selfResearched: true, blockMessages: true, suppressUpdate: true);
                            }
                        }
                    }
                    PlayerEmpire.Research.Update(PlayerEmpire.DominantRace);
                    PlayerEmpire.ReviewDesignsBuiltObjectsImprovedComponents();
                    PlayerEmpire.ReviewResearchAbilities();
                }
                MergeGalaxyMap(empire, PlayerEmpire);
                HabitatList habitatList = new HabitatList();
                habitatList.AddRange(empire.Colonies);
                for (int l = 0; l < habitatList.Count; l++)
                {
                    Habitat habitat = habitatList[l];
                    habitat.Population.Add(new Population(PlayerEmpire.DominantRace, 500000000L));
                    PopulationList populationList = new PopulationList();
                    if (habitat.Population != null && habitat.Population.Count > 0)
                    {
                        for (int m = 0; m < habitat.Population.Count; m++)
                        {
                            if (habitat.Population[m].Race == race)
                            {
                                populationList.Add(habitat.Population[m]);
                            }
                        }
                        for (int n = 0; n < populationList.Count; n++)
                        {
                            habitat.Population.Remove(populationList[n]);
                        }
                    }
                    PlayerEmpire.TakeOwnershipOfColony(habitat, PlayerEmpire, destroyAllBuiltObjectsAndTroopsAtColony: false);
                }
                if (empire.Active)
                {
                    empire.CompleteTeardown(PlayerEmpire, removeFromGalaxy: true, sendMessages: false);
                }
            }
            for (int num = 0; num < Empires.Count; num++)
            {
                Empire empire2 = Empires[num];
                if (empire2 == null || !empire2.Active || empire2.DiplomaticRelations == null)
                {
                    continue;
                }
                for (int num2 = 0; num2 < empire2.DiplomaticRelations.Count; num2++)
                {
                    DiplomaticRelation diplomaticRelation = empire2.DiplomaticRelations[num2];
                    if (diplomaticRelation != null)
                    {
                        diplomaticRelation.Locked = false;
                    }
                }
            }
        }

        public void WipeoutEmpireMakeColoniesIndependent(Empire empire)
        {
            if (empire != null)
            {
                HabitatList habitatList = new HabitatList();
                habitatList.AddRange(empire.Colonies);
                for (int i = 0; i < habitatList.Count; i++)
                {
                    Habitat colony = habitatList[i];
                    IndependentEmpire.TakeOwnershipOfColony(colony, IndependentEmpire, destroyAllBuiltObjectsAndTroopsAtColony: true);
                }
                empire.CompleteTeardown(null, removeFromGalaxy: true, sendMessages: false);
            }
        }

        public void DecimateEmpire(Empire empire, Empire decimatingEmpire)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(empire.PrivateBuiltObjects);
            builtObjectList.AddRange(empire.BuiltObjects);
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                int num = Rnd.Next(0, 100);
                if (num > 20)
                {
                    builtObjectList[i].CompleteTeardown(this);
                }
                else if (num > 5)
                {
                    DamageBuiltObjectComponents(builtObjectList[i], Rnd.NextDouble());
                }
            }
            HabitatList habitatList = new HabitatList();
            habitatList.AddRange(empire.Colonies);
            for (int j = 0; j < habitatList.Count; j++)
            {
                int num2 = Rnd.Next(0, 10);
                if (num2 > 3 && empire.Colonies.Count > 2)
                {
                    habitatList[j].ClearColony(decimatingEmpire);
                }
                else if (num2 > 0)
                {
                    empire.TakeOwnershipOfColony(habitatList[j], IndependentEmpire, destroyAllBuiltObjectsAndTroopsAtColony: true);
                }
            }
        }

        public void ReviewIndependentColonies()
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < Habitats.Count; i++)
            {
                if (Habitats[i].Population.Count > 0 && Habitats[i].Empire == IndependentEmpire)
                {
                    habitatList.Add(Habitats[i]);
                }
            }
            IndependentColonies = habitatList;
        }

        public void UpdateSystemInfo(Empire playerEmpire)
        {
            Design latestColonyDesign = null;
            List<HabitatType> colonizableHabitatTypes = new List<HabitatType>();
            if (playerEmpire != null)
            {
                latestColonyDesign = playerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                colonizableHabitatTypes = playerEmpire.ColonizableHabitatTypesForEmpire(playerEmpire);
            }
            for (int i = 0; i < Systems.Count; i++)
            {
                SystemInfo other = DetermineSystemInfo(Systems[i], playerEmpire, colonizableHabitatTypes, latestColonyDesign);
                Systems[i].CopyFromOther(other);
                GalaxyIndex galaxyIndex = ResolveIndex(Systems[i].SystemStar.Xpos, Systems[i].SystemStar.Ypos);
                if (!SystemsIndex[galaxyIndex.X][galaxyIndex.Y].Contains(Systems[i]))
                {
                    SystemsIndex[galaxyIndex.X][galaxyIndex.Y].Add(Systems[i]);
                }
            }
        }

        public SystemInfo DetermineSystemInfo(SystemInfo system, Empire playerEmpire)
        {
            Design latestColonyDesign = null;
            List<HabitatType> colonizableHabitatTypes = new List<HabitatType>();
            if (playerEmpire != null)
            {
                latestColonyDesign = playerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                colonizableHabitatTypes = playerEmpire.ColonizableHabitatTypesForEmpire(playerEmpire);
            }
            return DetermineSystemInfo(system, playerEmpire, colonizableHabitatTypes, latestColonyDesign);
        }

        public SystemInfo DetermineSystemInfo(SystemInfo system, Empire playerEmpire, List<HabitatType> colonizableHabitatTypes, Design latestColonyDesign)
        {
            EmpireList empireList = new EmpireList();
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            List<long> list3 = new List<long>();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            short plagueId = -1;
            bool flag = false;
            bool hasRuins = false;
            bool hasScenery = false;
            bool hasResearchBonus = false;
            if (system.SystemStar.ScenicFactor > 0f)
            {
                hasScenery = true;
            }
            if (system.SystemStar.ResearchBonus > 0)
            {
                hasResearchBonus = true;
            }
            for (int i = 0; i < system.Habitats.Count; i++)
            {
                if (system.Habitats[i].Category == HabitatCategoryType.Asteroid)
                {
                    continue;
                }
                Habitat habitat = system.Habitats[i];
                if (habitat.Ruin != null)
                {
                    hasRuins = true;
                }
                if (habitat.ScenicFactor > 0f)
                {
                    hasScenery = true;
                }
                if (habitat.ResearchBonus > 0)
                {
                    hasResearchBonus = true;
                }
                if (habitat.Category == HabitatCategoryType.Planet)
                {
                    num++;
                }
                else if (habitat.Category == HabitatCategoryType.Moon)
                {
                    num2++;
                }
                if (habitat.IsBlockaded)
                {
                    num3++;
                }
                if (habitat.PlagueId >= 0)
                {
                    plagueId = habitat.PlagueId;
                }
                if (habitat.Empire == IndependentEmpire)
                {
                    num4++;
                }
                else if (playerEmpire != null && !flag && playerEmpire.CanEmpireColonizeHabitat(playerEmpire, habitat, colonizableHabitatTypes, latestColonyDesign) && (habitat.Quality >= 0.5f || (habitat.Resources != null && habitat.Resources.HasSuperLuxuryResources()) || (habitat.Ruin != null && (habitat.Ruin.BonusDefensive > 0.0 || habitat.Ruin.BonusDiplomacy > 0.0 || habitat.Ruin.BonusHappiness > 0.0 || habitat.Ruin.BonusResearchEnergy > 0.0 || habitat.Ruin.BonusResearchHighTech > 0.0 || habitat.Ruin.BonusResearchWeapons > 0.0 || habitat.Ruin.BonusWealth > 0.0))))
                {
                    flag = true;
                }
                if (habitat.Empire != null && habitat.Empire != IndependentEmpire)
                {
                    int num5 = empireList.IndexOf(habitat.Empire);
                    if (num5 < 0)
                    {
                        empireList.Add(habitat.Empire);
                        list.Add(0);
                        list2.Add(0);
                        list3.Add(0L);
                        num5 = empireList.Count - 1;
                    }
                    list[num5] += habitat.StrategicValue;
                    list3[num5] += habitat.Population.TotalAmount;
                    list2[num5]++;
                }
            }
            Empire empire = null;
            int num6 = 0;
            long num7 = 0L;
            int colonyCount = 0;
            for (int j = 0; j < empireList.Count; j++)
            {
                if (list[j] > num6)
                {
                    empire = empireList[j];
                    num6 = list[j];
                    num7 = list3[j];
                    colonyCount = list2[j];
                }
                else if (list[j] == num6 && list3[j] > num7)
                {
                    empire = empireList[j];
                    num6 = list[j];
                    num7 = list3[j];
                    colonyCount = list2[j];
                }
            }
            EmpireSystemSummary empireSystemSummary = null;
            EmpireSystemSummaryList empireSystemSummaryList = null;
            new BuiltObjectList();
            if (empire != null)
            {
                empireSystemSummary = new EmpireSystemSummary();
                empireSystemSummary.Empire = empire;
                empireSystemSummary.ColonyCount = colonyCount;
                empireSystemSummary.TotalStrategicValue = num6;
                for (int k = 0; k < empireList.Count; k++)
                {
                    Empire empire2 = empireList[k];
                    if (empire2 != empire)
                    {
                        if (empireSystemSummaryList == null)
                        {
                            empireSystemSummaryList = new EmpireSystemSummaryList();
                        }
                        EmpireSystemSummary empireSystemSummary2 = new EmpireSystemSummary();
                        empireSystemSummary2.Empire = empire2;
                        empireSystemSummary2.ColonyCount = list2[k];
                        empireSystemSummary2.TotalStrategicValue = list[k];
                        empireSystemSummaryList.Add(empireSystemSummary2);
                    }
                }
            }
            system.PlanetCount = num;
            system.MoonCount = num2;
            system.BlockadeCount = num3;
            system.PlagueId = plagueId;
            system.IndependentColonyCount = num4;
            system.HasRuins = hasRuins;
            system.HasScenery = hasScenery;
            system.HasResearchBonus = hasResearchBonus;
            if (playerEmpire != null)
            {
                system.PlayerPotentialColonies = flag;
            }
            system.DominantEmpire = empireSystemSummary;
            system.OtherEmpires = empireSystemSummaryList;
            if (empireList.Count > 1)
            {
                system.IsDisputed = true;
            }
            else
            {
                system.IsDisputed = false;
            }
            return system;
        }

        public void ReviewComponentPrices()
        {
            ComponentDefinition[] componentDefinitionsStatic = ComponentDefinitionsStatic;
            foreach (ComponentDefinition componentDefinition in componentDefinitionsStatic)
            {
                double num = 0.0;
                for (int j = 0; j < componentDefinition.RequiredResources.Count; j++)
                {
                    ComponentResource componentResource = componentDefinition.RequiredResources[j];
                    double num2 = ResourceCurrentPrices[componentResource.ResourceID];
                    num += num2 * (double)componentResource.Quantity;
                }
                if (componentDefinition.ComponentID >= ComponentCurrentPrices.Count)
                {
                    ComponentCurrentPrices.Add(0.0);
                }
                ComponentCurrentPrices[componentDefinition.ComponentID] = num * 2.0;
            }
        }

        private void RemoveCompletedOrders()
        {
            OrderList orderList = new OrderList();
            for (int i = 0; i < Orders.Count; i++)
            {
                Order order = Orders[i];
                int num = order.AmountRequested - order.AmountDelivered;
                if (num <= 0)
                {
                    orderList.Add(order);
                }
            }
            lock (Orders._LockObject)
            {
                foreach (Order item in orderList)
                {
                    Orders.Remove(item);
                }
            }
        }

        public long CalculateOrderPlacementDate(Order order)
        {
            long timeSinceOrderPlacement = 0L;
            return CalculateOrderPlacementDate(order, out timeSinceOrderPlacement);
        }

        public long CalculateOrderPlacementDate(Order order, out long timeSinceOrderPlacement)
        {
            long num = long.MaxValue;
            int num2 = (int)(OrderExpiryYearsLuxury * (double)RealSecondsInGalacticYear * 1000.0);
            int num3 = (int)(1000.0 * (double)RealSecondsInGalacticYear * 1000.0);
            long currentStarDate = CurrentStarDate;
            long num4 = order.ExpiryDate - currentStarDate;
            num = ((num4 <= num2) ? (order.ExpiryDate - num2) : (order.ExpiryDate - num3));
            timeSinceOrderPlacement = currentStarDate - num;
            return num;
        }

        public bool CancelContract(Contract contract)
        {
            if (contract != null)
            {
                if (contract.Supplier != null)
                {
                    int num = contract.AmountToFulfill - contract.AmountPickedUp;
                    if (num > 0)
                    {
                        CargoList cargo = contract.Supplier.Cargo;
                        Empire empireById = GetEmpireById(contract.BuyerEmpireId);
                        if (empireById != null && cargo != null)
                        {
                            Cargo cargo2 = null;
                            if (contract.ResourceId >= 0)
                            {
                                cargo2 = cargo.GetCargo(new Resource((byte)contract.ResourceId), empireById);
                            }
                            else if (contract.ComponentId >= 0)
                            {
                                cargo2 = cargo.GetCargo(new Component(contract.ComponentId), empireById);
                            }
                            if (cargo2 != null)
                            {
                                cargo2.Reserved -= num;
                                cargo2.Reserved = Math.Max(0, cargo2.Reserved);
                                if (cargo2.Amount <= 0 && cargo2.Reserved <= 0)
                                {
                                    cargo.Remove(cargo2);
                                }
                            }
                        }
                    }
                }
                contract.AmountToFulfill = contract.AmountDelivered;
                return true;
            }
            return false;
        }

        private void CancelExpiredOrders()
        {
            long currentStarDate = CurrentStarDate;
            OrderList orderList = new OrderList();
            for (int i = 0; i < Orders.Count; i++)
            {
                Order order = Orders[i];
                if (currentStarDate <= order.ExpiryDate || order.AmountStillToArrive > 0)
                {
                    continue;
                }
                orderList.Add(order);
                if (order.Contracts == null || order.Contracts.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < order.Contracts.Count; j++)
                {
                    Contract contract = order.Contracts[j];
                    if (contract != null)
                    {
                        CancelContract(contract);
                    }
                }
            }
            lock (Orders._LockObject)
            {
                foreach (Order item in orderList)
                {
                    Orders.Remove(item);
                }
            }
        }

        public double CalculateResourceDemand(byte resourceId, out double inTransitAmount)
        {
            double num = 0.0;
            inTransitAmount = 0.0;
            for (int i = 0; i < Orders.Count; i++)
            {
                Order order = Orders[i];
                if (order.CommodityResource != null && order.CommodityResource.ResourceID == resourceId)
                {
                    if (order.AmountOutstandingToContract > 0)
                    {
                        num += (double)order.AmountOutstandingToContract;
                    }
                    if (order.AmountStillToArrive > 0)
                    {
                        inTransitAmount += order.AmountStillToArrive;
                    }
                }
            }
            return num;
        }

        public double CalculateResourceDemandForEmpire(Empire empire, byte resourceId, out double inTransitAmount)
        {
            double num = 0.0;
            inTransitAmount = 0.0;
            for (int i = 0; i < Orders.Count; i++)
            {
                Order order = Orders[i];
                if (order.CommodityResource != null && order.CommodityResource.ResourceID == resourceId && ((order.RequestingBuiltObject != null && order.RequestingBuiltObject.ActualEmpire == empire) || (order.RequestingColony != null && order.RequestingColony.Empire == empire)))
                {
                    if (order.AmountOutstandingToContract > 0)
                    {
                        num += (double)order.AmountOutstandingToContract;
                    }
                    if (order.AmountStillToArrive > 0)
                    {
                        inTransitAmount += order.AmountStillToArrive;
                    }
                }
            }
            return num;
        }

        public void ReviewResourcePrices()
        {
            int[] array = new int[ResourceSystem.Resources.Count];
            for (int i = 0; i < Orders.Count; i++)
            {
                Order order = Orders[i];
                if (order.CommodityResource != null && order.AmountOutstandingToContract > 0)
                {
                    Resource commodityResource = order.CommodityResource;
                    array[commodityResource.ResourceID] += order.AmountOutstandingToContract;
                }
            }
            int[] array2 = new int[ResourceSystem.Resources.Count];
            EmpireList empireList = new EmpireList();
            empireList.AddRange(Empires);
            empireList.AddRange(PirateEmpires);
            for (int j = 0; j < empireList.Count; j++)
            {
                Empire empire = empireList[j];
                if (empire == null || !empire.Active)
                {
                    continue;
                }
                for (int k = 0; k < empire.Colonies.Count; k++)
                {
                    Habitat habitat = empire.Colonies[k];
                    if (habitat == null || habitat.Empire != empire || habitat.Cargo == null)
                    {
                        continue;
                    }
                    for (int l = 0; l < habitat.Cargo.Count; l++)
                    {
                        Cargo cargo = habitat.Cargo[l];
                        if (cargo != null && cargo.CommodityResource != null && cargo.Available > 0)
                        {
                            Resource commodityResource2 = cargo.CommodityResource;
                            array2[commodityResource2.ResourceID] += cargo.Available;
                        }
                    }
                }
                for (int m = 0; m < empire.SpacePorts.Count; m++)
                {
                    BuiltObject builtObject = empire.SpacePorts[m];
                    if (builtObject == null || (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Empire == empire) || builtObject.Cargo == null)
                    {
                        continue;
                    }
                    for (int n = 0; n < builtObject.Cargo.Count; n++)
                    {
                        Cargo cargo2 = builtObject.Cargo[n];
                        if (cargo2 != null && cargo2.CommodityResource != null && cargo2.Available > 0)
                        {
                            Resource commodityResource3 = cargo2.CommodityResource;
                            array2[commodityResource3.ResourceID] += cargo2.Available;
                        }
                    }
                }
                for (int num = 0; num < empire.MiningStations.Count; num++)
                {
                    BuiltObject builtObject2 = empire.MiningStations[num];
                    if (builtObject2 == null || builtObject2.Cargo == null)
                    {
                        continue;
                    }
                    for (int num2 = 0; num2 < builtObject2.Cargo.Count; num2++)
                    {
                        Cargo cargo3 = builtObject2.Cargo[num2];
                        if (cargo3 != null && cargo3.CommodityResource != null && cargo3.Available > 0)
                        {
                            Resource commodityResource4 = cargo3.CommodityResource;
                            array2[commodityResource4.ResourceID] += cargo3.Available;
                        }
                    }
                }
            }
            for (int num3 = 0; num3 < ResourceSystem.Resources.Count; num3++)
            {
                ResourceDefinition resourceDefinition = ResourceSystem.Resources[num3];
                double num4 = (double)array[num3] / Math.Max(1.0, array2[num3]);
                double num5 = (double)resourceDefinition.BasePrice * num4;
                double num6 = num5 - ResourceCurrentPrices[num3];
                num6 = ((!(num6 > 0.0)) ? (num6 / 2.0) : (num6 / 4.0));
                if (num6 > ResourceCurrentPrices[num3] / 2.0)
                {
                    num6 = ResourceCurrentPrices[num3] / 2.0;
                }
                double num7 = ResourceCurrentPrices[num3];
                num7 += num6;
                double val = (double)resourceDefinition.BasePrice * 0.1667;
                double val2 = (double)resourceDefinition.BasePrice * 0.35;
                if (resourceDefinition.SuperLuxuryBonusAmount > 0)
                {
                    val = (double)resourceDefinition.BasePrice / 2.0;
                    val2 = (double)resourceDefinition.BasePrice * 3.0;
                }
                num7 = Math.Max(val, num7);
                num7 = Math.Min(val2, num7);
                if (double.IsNaN(num7))
                {
                    num7 = resourceDefinition.BasePrice;
                }
                ResourceCurrentPrices[num3] = num7;
            }
        }

        private int DeviationFromNormal(int value)
        {
            return Math.Abs(value - 100);
        }

        public string GenerateIndependentColonyReport(Empire potentialColonizer, Habitat colony, Race race)
        {
            Habitat habitat = DetermineHabitatSystemStar(colony);
            string text = string.Format(TextResolver.GetText("Independent Colony Report Intro"), race.Name, ResolveDescription(colony.Type).ToLower(CultureInfo.InvariantCulture), ResolveDescription(colony.Category).ToLower(CultureInfo.InvariantCulture), colony.Name, habitat.Name);
            text += ".\n\n";
            if (potentialColonizer.DominantRace != race)
            {
                text += GenerateRaceReport(race);
                text += "\n\n";
            }
            if (potentialColonizer == PlayerEmpire)
            {
                string text2 = GenerateIndependentColonyStoryClue(colony);
                if (!string.IsNullOrEmpty(text2))
                {
                    text += "*** ";
                    text += string.Format(TextResolver.GetText("Independent Colony Legend Intro"), race.Name);
                    text += text2;
                    text += " ***\n\n";
                }
            }
            _ = string.Empty;
            int num = CheckColonizationLikeliness(colony, potentialColonizer.DominantRace);
            if (num <= -20)
            {
                return text + string.Format(TextResolver.GetText("Independent Colony Colonization Hostile"), ResolveDescription(colony.Category));
            }
            if (num <= 0)
            {
                return text + string.Format(TextResolver.GetText("Independent Colony Colonization Unlikely"), ResolveDescription(colony.Category));
            }
            return text + string.Format(TextResolver.GetText("Independent Colony Colonization Good"), ResolveDescription(colony.Category));
        }

        public string ResolveColonizationLikelinessDescription(Habitat potentialColony, Empire colonizingEmpire)
        {
            string empty = string.Empty;
            int num = CheckColonizationLikeliness(potentialColony, colonizingEmpire.DominantRace);
            if (num <= -20)
            {
                return TextResolver.GetText("Most unlikely");
            }
            if (num <= -5)
            {
                return TextResolver.GetText("Unlikely");
            }
            if (num <= 5)
            {
                return TextResolver.GetText("Possible");
            }
            return TextResolver.GetText("Likely");
        }

        public int CheckColonizationLikeliness(Habitat potentialColony, Race colonizingRace)
        {
            int num = colonizingRace.FriendlinessLevel - colonizingRace.AggressionLevel;
            int num2 = 100;
            if (potentialColony.Empire == IndependentEmpire && potentialColony.Population != null && potentialColony.Population.DominantRace != null)
            {
                Race dominantRace = potentialColony.Population.DominantRace;
                num2 = dominantRace.FriendlinessLevel - dominantRace.AggressionLevel;
                if (dominantRace == colonizingRace)
                {
                    num2 += 35;
                    num2 = Math.Max(5, num2);
                    num += 25;
                    num = Math.Max(5, num);
                }
                else if (dominantRace.FamilyId == colonizingRace.FamilyId)
                {
                    num2 += 20;
                    num += 15;
                }
            }
            return num + num2;
        }

        public string GenerateRaceReport(Race race)
        {
            string empty = string.Empty;
            int num = 0;
            string text = string.Empty;
            if (DeviationFromNormal(race.AggressionLevel) > num)
            {
                num = DeviationFromNormal(race.AggressionLevel);
                text = ((race.AggressionLevel <= 100) ? TextResolver.GetText("Passive") : TextResolver.GetText("Aggressive"));
            }
            if (DeviationFromNormal(race.CautionLevel) > num)
            {
                num = DeviationFromNormal(race.CautionLevel);
                text = ((race.CautionLevel < 100) ? TextResolver.GetText("Reckless") : TextResolver.GetText("Cautious"));
            }
            if (DeviationFromNormal(race.FriendlinessLevel) > num)
            {
                num = DeviationFromNormal(race.FriendlinessLevel);
                text = ((race.FriendlinessLevel < 100) ? TextResolver.GetText("Unfriendly") : TextResolver.GetText("Friendly"));
            }
            if (DeviationFromNormal(race.IntelligenceLevel) > num)
            {
                num = DeviationFromNormal(race.IntelligenceLevel);
                text = ((race.IntelligenceLevel < 100) ? TextResolver.GetText("Stupid") : TextResolver.GetText("Intelligent"));
            }
            if (DeviationFromNormal(race.LoyaltyLevel) > num)
            {
                num = DeviationFromNormal(race.LoyaltyLevel);
                text = ((race.LoyaltyLevel < 100) ? TextResolver.GetText("Unreliable") : TextResolver.GetText("Dependable"));
            }
            string empty2 = string.Empty;
            empty2 = ((num > 30) ? TextResolver.GetText("Extremely") : ((num > 20) ? TextResolver.GetText("Very") : ((num <= 10) ? TextResolver.GetText("Slightly") : TextResolver.GetText("Quite"))));
            string empty3 = string.Empty;
            empty3 = ((race.IntelligenceLevel > 120) ? TextResolver.GetText("a highly advanced") : ((race.IntelligenceLevel > 109) ? TextResolver.GetText("an advanced") : ((race.IntelligenceLevel > 100) ? TextResolver.GetText("an average") : ((race.IntelligenceLevel <= 85) ? TextResolver.GetText("a primitive") : TextResolver.GetText("a slightly backward")))));
            empty = (string.IsNullOrEmpty(text) ? (empty + string.Format(TextResolver.GetText("Race Report RACE ADVANCEMENT RACEFAMILY"), race.Name, empty3, ResolveRaceFamilyDescription(race.FamilyId))) : (empty + string.Format(TextResolver.GetText("Race Report RACE ADVANCEMENT RACEFAMILY INTENSITY QUALITY"), race.Name, empty3, ResolveRaceFamilyDescription(race.FamilyId), empty2.ToLower(CultureInfo.InvariantCulture), text.ToLower(CultureInfo.InvariantCulture))));
            empty += ". ";
            string text2 = string.Empty;
            if (race.EspionageBonus > 0)
            {
                text2 = (string.IsNullOrEmpty(text2) ? (text2 + string.Format(TextResolver.GetText("Race Bonus ABILITY"), TextResolver.GetText("cunning spies"))) : (text2 + string.Format(TextResolver.GetText("Race Bonus Extra ABILITY"), TextResolver.GetText("cunning spies"))));
                text2 += ". ";
            }
            if (race.ResearchBonus > 0)
            {
                text2 = (string.IsNullOrEmpty(text2) ? (text2 + string.Format(TextResolver.GetText("Race Bonus ABILITY"), TextResolver.GetText("gifted scientists"))) : (text2 + string.Format(TextResolver.GetText("Race Bonus Extra ABILITY"), TextResolver.GetText("gifted scientists"))));
                text2 += ". ";
            }
            if (race.ResourceExtractionBonus > 0)
            {
                text2 = (string.IsNullOrEmpty(text2) ? (text2 + string.Format(TextResolver.GetText("Race Bonus ABILITY"), TextResolver.GetText("industrious miners"))) : (text2 + string.Format(TextResolver.GetText("Race Bonus Extra ABILITY"), TextResolver.GetText("industrious miners"))));
                text2 += ". ";
            }
            if (race.SatisfactionModifier > 0)
            {
                text2 = (string.IsNullOrEmpty(text2) ? (text2 + string.Format(TextResolver.GetText("Race Bonus ABILITY"), TextResolver.GetText("natural optimists"))) : (text2 + string.Format(TextResolver.GetText("Race Bonus Extra ABILITY"), TextResolver.GetText("natural optimists"))));
                text2 += ". ";
            }
            if (race.ShipMaintenanceSavings > 0)
            {
                text2 = (string.IsNullOrEmpty(text2) ? (text2 + string.Format(TextResolver.GetText("Race Bonus ABILITY"), TextResolver.GetText("master starship engineers"))) : (text2 + string.Format(TextResolver.GetText("Race Bonus Extra ABILITY"), TextResolver.GetText("master starship engineers"))));
                text2 += ". ";
            }
            if (race.TroopMaintenanceSavings > 0)
            {
                text2 = (string.IsNullOrEmpty(text2) ? (text2 + string.Format(TextResolver.GetText("Race Bonus ABILITY"), TextResolver.GetText("superb ground troops"))) : (text2 + string.Format(TextResolver.GetText("Race Bonus Extra ABILITY"), TextResolver.GetText("superb ground troops"))));
                text2 += ". ";
            }
            if (race.WarWearinessAttenuation > 0)
            {
                text2 = (string.IsNullOrEmpty(text2) ? (text2 + string.Format(TextResolver.GetText("Race Bonus ABILITY"), TextResolver.GetText("tenacious fighters"))) : (text2 + string.Format(TextResolver.GetText("Race Bonus Extra ABILITY"), TextResolver.GetText("tenacious fighters"))));
                text2 += ". ";
            }
            return empty + text2;
        }

        public static string SplitString(string input)
        {
            string[] array = Regex.Split(input, "([A-Z])");
            string text = string.Empty;
            int num;
            for (num = 1; num < array.Length; num++)
            {
                text += array[num];
                num++;
                text += array[num];
                text += " ";
            }
            return text.Trim();
        }

        public static bool CheckCharacterTraitAppliesOnlyToExistingSkills(CharacterTraitType trait)
        {
            switch (trait)
            {
                case CharacterTraitType.Lazy:
                case CharacterTraitType.Energetic:
                case CharacterTraitType.GoodTactician:
                case CharacterTraitType.PoorTactician:
                case CharacterTraitType.Drunk:
                case CharacterTraitType.ToughDiscipline:
                case CharacterTraitType.LaxDiscipline:
                case CharacterTraitType.IntelligenceAddict:
                case CharacterTraitType.IntelligenceSober:
                    return true;
                default:
                    return false;
            }
        }

        public static CharacterSkillList DetermineEffectsOfCharacterTrait(CharacterTraitType trait)
        {
            return DetermineEffectsOfCharacterTrait(trait, CharacterRole.Undefined);
        }

        public static CharacterSkillList DetermineEffectsOfCharacterTrait(CharacterTraitType trait, CharacterRole role)
        {
            CharacterSkillList characterSkillList = new CharacterSkillList();
            int num = 10;
            int num2 = 5;
            int num3 = 20;
            switch (trait)
            {
                case CharacterTraitType.Smuggler:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingIncome, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingEvasion, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, num));
                    break;
                case CharacterTraitType.BountyHunter:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.BoardingAssault, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, num));
                    break;
                case CharacterTraitType.IntelligenceAddict:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num));
                    break;
                case CharacterTraitType.IntelligenceCorrupt:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, -num));
                    break;
                case CharacterTraitType.IntelligenceCourageous:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, num));
                    break;
                case CharacterTraitType.IntelligenceEloquentSpeaker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, num));
                    break;
                case CharacterTraitType.IntelligenceLawful:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, -num));
                    break;
                case CharacterTraitType.IntelligenceMeasured:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, num));
                    break;
                case CharacterTraitType.IntelligencePoorSpeaker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num));
                    break;
                case CharacterTraitType.IntelligenceSober:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, num));
                    break;
                case CharacterTraitType.IntelligenceTolerant:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, num));
                    break;
                case CharacterTraitType.IntelligenceUninhibited:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num));
                    break;
                case CharacterTraitType.IntelligenceWeak:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, -num));
                    break;
                case CharacterTraitType.IntelligenceXenophobic:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num));
                    break;
                case CharacterTraitType.Paranoid:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num));
                    break;
                case CharacterTraitType.Trusting:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num));
                    break;
                case CharacterTraitType.PeaceThroughStrength:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num));
                    break;
                case CharacterTraitType.Pacifist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num));
                    break;
                case CharacterTraitType.Expansionist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, num));
                    break;
                case CharacterTraitType.Isolationist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchWeapons, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, -num));
                    break;
                case CharacterTraitType.Diplomat:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num));
                    break;
                case CharacterTraitType.Obnoxious:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num));
                    break;
                case CharacterTraitType.Famous:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, num));
                    break;
                case CharacterTraitType.Disliked:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, -num));
                    break;
                case CharacterTraitType.GoodAdministrator:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, num));
                    break;
                case CharacterTraitType.PoorAdministrator:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, -num));
                    break;
                case CharacterTraitType.BeanCounter:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num));
                    break;
                case CharacterTraitType.Generous:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num));
                    break;
                case CharacterTraitType.Engineer:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, num));
                    break;
                case CharacterTraitType.Luddite:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, -num));
                    break;
                case CharacterTraitType.FreeTrader:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, num));
                    break;
                case CharacterTraitType.Protectionist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, -num));
                    break;
                case CharacterTraitType.Environmentalist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, -num));
                    break;
                case CharacterTraitType.Industrialist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, num));
                    break;
                case CharacterTraitType.Organized:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, num));
                    break;
                case CharacterTraitType.Disorganized:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, -num));
                    break;
                case CharacterTraitType.HealthOriented:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, -num));
                    break;
                case CharacterTraitType.LaborOriented:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, num));
                    break;
                case CharacterTraitType.Spiritual:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num));
                    break;
                case CharacterTraitType.Logical:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num));
                    break;
                case CharacterTraitType.GoodStrategist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, num));
                    break;
                case CharacterTraitType.PoorStrategist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, -num));
                    break;
                case CharacterTraitType.Uninhibited:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num));
                    break;
                case CharacterTraitType.Measured:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num));
                    break;
                case CharacterTraitType.Addict:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num));
                    break;
                case CharacterTraitType.Sober:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num));
                    break;
                case CharacterTraitType.Courageous:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, num));
                    break;
                case CharacterTraitType.Weak:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, -num));
                    break;
                case CharacterTraitType.Tolerant:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num));
                    break;
                case CharacterTraitType.Xenophobic:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num));
                    break;
                case CharacterTraitType.EloquentSpeaker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num));
                    break;
                case CharacterTraitType.PoorSpeaker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num));
                    break;
                case CharacterTraitType.Corrupt:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, -num));
                    break;
                case CharacterTraitType.Lawful:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, num));
                    break;
                case CharacterTraitType.Lazy:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchWeapons, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchEnergy, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryBaseMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianBaseMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Fighters, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsRange, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.RepairBonus, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopExperienceGain, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthArmor, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthInfantry, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthSpecialForces, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthPlanetaryDefense, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingEvasion, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.BoardingAssault, -num2));
                    break;
                case CharacterTraitType.Energetic:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchWeapons, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchEnergy, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryBaseMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianBaseMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Fighters, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsRange, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.RepairBonus, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopExperienceGain, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthArmor, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthInfantry, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthSpecialForces, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthPlanetaryDefense, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingEvasion, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.BoardingAssault, num2));
                    break;
                case CharacterTraitType.Linguist:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, num));
                    break;
                case CharacterTraitType.TongueTied:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, -num));
                    break;
                case CharacterTraitType.Technical:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, num));
                    break;
                case CharacterTraitType.NonTechnical:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, -num));
                    break;
                case CharacterTraitType.PoorTactician:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchWeapons, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchEnergy, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryBaseMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianBaseMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Fighters, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsRange, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.RepairBonus, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopExperienceGain, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthArmor, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthInfantry, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthSpecialForces, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthPlanetaryDefense, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingEvasion, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.BoardingAssault, -num2));
                    break;
                case CharacterTraitType.GoodTactician:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchWeapons, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchEnergy, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryBaseMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianBaseMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Fighters, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsRange, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.RepairBonus, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopExperienceGain, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthArmor, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthInfantry, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthSpecialForces, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthPlanetaryDefense, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingEvasion, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.BoardingAssault, num2));
                    break;
                case CharacterTraitType.StrongSpaceAttacker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, num));
                    break;
                case CharacterTraitType.PoorSpaceAttacker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, -num));
                    break;
                case CharacterTraitType.StrongSpaceDefender:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, num));
                    break;
                case CharacterTraitType.PoorSpaceDefender:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, -num));
                    break;
                case CharacterTraitType.Drunk:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchWeapons, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchEnergy, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryBaseMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianBaseMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Fighters, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsRange, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.RepairBonus, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopExperienceGain, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthArmor, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthInfantry, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthSpecialForces, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthPlanetaryDefense, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingEvasion, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.BoardingAssault, -num2));
                    break;
                case CharacterTraitType.ToughDiscipline:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchWeapons, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchEnergy, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryBaseMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianBaseMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Fighters, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsRange, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.RepairBonus, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopExperienceGain, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthArmor, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthInfantry, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthSpecialForces, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthPlanetaryDefense, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingIncome, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingEvasion, num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.BoardingAssault, num2));
                    break;
                case CharacterTraitType.LaxDiscipline:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Diplomacy, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TradeIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TourismIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyCorruption, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyHappiness, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PopulationGrowth, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MiningRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ColonyShipConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.FacilityConstructionSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchWeapons, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchEnergy, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ResearchHighTech, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryShipMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianShipMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.MilitaryBaseMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CivilianBaseMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WarWeariness, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipManeuvering, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Fighters, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsRange, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShieldRechargeRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.RepairBonus, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopExperienceGain, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthArmor, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthInfantry, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthSpecialForces, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopStrengthPlanetaryDefense, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingIncome, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.SmugglingEvasion, -num2));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.BoardingAssault, -num2));
                    break;
                case CharacterTraitType.GoodSpaceLogistician:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, num));
                    break;
                case CharacterTraitType.PoorSpaceLogistician:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.ShipEnergyUsage, -num));
                    break;
                case CharacterTraitType.NaturalSpaceLeader:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.WeaponsDamage, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.DamageControl, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Targeting, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Countermeasures, num));
                    break;
                case CharacterTraitType.SkilledNavigator:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, num));
                    break;
                case CharacterTraitType.PoorNavigator:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.HyperjumpSpeed, -num));
                    break;
                case CharacterTraitType.StrongGroundAttacker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, num));
                    break;
                case CharacterTraitType.PoorGroundAttacker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, -num));
                    break;
                case CharacterTraitType.StrongGroundDefender:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, num));
                    break;
                case CharacterTraitType.PoorGroundDefender:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, -num));
                    break;
                case CharacterTraitType.GoodGroundLogistician:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, num));
                    break;
                case CharacterTraitType.PoorGroundLogistician:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopMaintenance, -num));
                    break;
                case CharacterTraitType.NaturalGroundLeader:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopExperienceGain, num));
                    break;
                case CharacterTraitType.GoodRecruiter:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, num));
                    break;
                case CharacterTraitType.PoorRecruiter:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecruitment, -num));
                    break;
                case CharacterTraitType.CarefulAttacker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, num));
                    break;
                case CharacterTraitType.RecklessAttacker:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundAttack, num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopGroundDefense, -num));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.TroopRecoveryRate, -num));
                    break;
                case CharacterTraitType.DoubleAgent:
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Espionage, -num3));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.CounterEspionage, -num3));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Sabotage, -num3));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Concealment, -num3));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.PsyOps, -num3));
                    characterSkillList.Add(new CharacterSkill(CharacterSkillType.Assassination, -num3));
                    break;
            }
            if (role != 0)
            {
                CharacterSkillList characterSkillList2 = new CharacterSkillList();
                for (int i = 0; i < characterSkillList.Count; i++)
                {
                    if (!Character.CheckSkillValid(characterSkillList[i].Type, role))
                    {
                        characterSkillList2.Add(characterSkillList[i]);
                    }
                }
                for (int j = 0; j < characterSkillList2.Count; j++)
                {
                    characterSkillList.Remove(characterSkillList2[j]);
                }
            }
            return characterSkillList;
        }

        public static List<CharacterSkillType> DetermineCharacterSkillsAffectedByEvent(CharacterEventType eventType, out List<float> relativeImportances)
        {
            List<CharacterSkillType> list = new List<CharacterSkillType>();
            relativeImportances = new List<float>();
            switch (eventType)
            {
                case CharacterEventType.CriticalResearchSuccess:
                case CharacterEventType.CriticalResearchFailure:
                    list.Add(CharacterSkillType.ResearchWeapons);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ResearchEnergy);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ResearchHighTech);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.Subjugated:
                    list.Add(CharacterSkillType.Diplomacy);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ColonyHappiness);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.MilitaryShipConstructionSpeed);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.TreatyBroken:
                    list.Add(CharacterSkillType.Diplomacy);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.TourismIncome);
                    relativeImportances.Add(0.5f);
                    list.Add(CharacterSkillType.TradeIncome);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.BuildSpaceport:
                    list.Add(CharacterSkillType.MilitaryBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.MilitaryShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.BuildOtherBase:
                    list.Add(CharacterSkillType.CivilianBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.BuildCivilianShip:
                    list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianShipMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ColonyIncome);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.BuildColonyShip:
                    list.Add(CharacterSkillType.ColonyShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.PopulationGrowth);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.BuildFacility:
                    list.Add(CharacterSkillType.FacilityConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ColonyIncome);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.BuildWonder:
                    list.Add(CharacterSkillType.FacilityConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ColonyIncome);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.BuildMilitaryBase:
                    list.Add(CharacterSkillType.MilitaryBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.MilitaryShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.BuildMilitaryShip:
                    list.Add(CharacterSkillType.MilitaryShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.MilitaryShipMaintenance);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.BuildMiningStation:
                    list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.MiningRate);
                    relativeImportances.Add(2f);
                    break;
                case CharacterEventType.BuildResearchStationEnergy:
                    list.Add(CharacterSkillType.CivilianBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ResearchEnergy);
                    relativeImportances.Add(2f);
                    break;
                case CharacterEventType.BuildResearchStationHighTech:
                    list.Add(CharacterSkillType.CivilianBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ResearchHighTech);
                    relativeImportances.Add(2f);
                    break;
                case CharacterEventType.BuildResearchStationWeapons:
                    list.Add(CharacterSkillType.CivilianBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ResearchWeapons);
                    relativeImportances.Add(2f);
                    break;
                case CharacterEventType.BuildResortBase:
                    list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CivilianBaseMaintenance);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.TourismIncome);
                    relativeImportances.Add(2f);
                    break;
                case CharacterEventType.CashNegative:
                    list.Add(CharacterSkillType.ColonyIncome);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.CashPositive:
                    list.Add(CharacterSkillType.ColonyIncome);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.ColonyDevelopmentIncrease:
                    list.Add(CharacterSkillType.ColonyHappiness);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.PopulationGrowth);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.ColonyDevelopmentDecrease:
                    list.Add(CharacterSkillType.ColonyHappiness);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.PopulationGrowth);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.GroundInvasion:
                    list.Add(CharacterSkillType.TroopExperienceGain);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.TroopGroundAttack);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.TroopGroundDefense);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.TroopRecoveryRate);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.TroopStrengthArmor);
                    relativeImportances.Add(0.5f);
                    list.Add(CharacterSkillType.TroopStrengthInfantry);
                    relativeImportances.Add(0.5f);
                    list.Add(CharacterSkillType.TroopStrengthSpecialForces);
                    relativeImportances.Add(0.5f);
                    list.Add(CharacterSkillType.TroopStrengthPlanetaryDefense);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.HyperjumpExit:
                    list.Add(CharacterSkillType.HyperjumpSpeed);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.IntelligenceAgentOursCaptured:
                    list.Add(CharacterSkillType.Espionage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Sabotage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Concealment);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.IntelligenceAgentRecruited:
                    list.Add(CharacterSkillType.Espionage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Sabotage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.CounterEspionage);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.IntelligenceMissionFailEspionage:
                    list.Add(CharacterSkillType.Espionage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Concealment);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.IntelligenceMissionFailSabotage:
                    list.Add(CharacterSkillType.Sabotage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.PsyOps);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.IntelligenceMissionInterceptEnemy:
                    list.Add(CharacterSkillType.CounterEspionage);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.IntelligenceMissionSucceedEspionage:
                    list.Add(CharacterSkillType.Espionage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Assassination);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.PsyOps);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Concealment);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.IntelligenceMissionSucceedSabotage:
                    list.Add(CharacterSkillType.Sabotage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Assassination);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.PsyOps);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Concealment);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.ResearchAdvanceEnergy:
                    list.Add(CharacterSkillType.ResearchEnergy);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.ResearchAdvanceHighTech:
                    list.Add(CharacterSkillType.ResearchHighTech);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.ResearchAdvanceWeapons:
                    list.Add(CharacterSkillType.ResearchWeapons);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.SpaceBattle:
                    list.Add(CharacterSkillType.Countermeasures);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.DamageControl);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Fighters);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.RepairBonus);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ShieldRechargeRate);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ShipEnergyUsage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ShipManeuvering);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Targeting);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.WeaponsDamage);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.WeaponsRange);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.TourismIncome:
                    list.Add(CharacterSkillType.TourismIncome);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Diplomacy);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.TradeIncome:
                    list.Add(CharacterSkillType.TradeIncome);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Diplomacy);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.TreatySigned:
                    list.Add(CharacterSkillType.Diplomacy);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.TradeIncome);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.TroopComplete:
                    list.Add(CharacterSkillType.TroopRecruitment);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.TroopGroundDefense);
                    relativeImportances.Add(0.5f);
                    list.Add(CharacterSkillType.TroopMaintenance);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.WarEnded:
                    list.Add(CharacterSkillType.WarWeariness);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Diplomacy);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.WarStarted:
                    list.Add(CharacterSkillType.WarWeariness);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Diplomacy);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.Boarding:
                    list.Add(CharacterSkillType.BoardingAssault);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.ShipManeuvering);
                    relativeImportances.Add(0.5f);
                    list.Add(CharacterSkillType.Targeting);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.Raid:
                    list.Add(CharacterSkillType.BoardingAssault);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.Targeting);
                    relativeImportances.Add(0.5f);
                    list.Add(CharacterSkillType.SmugglingIncome);
                    relativeImportances.Add(0.5f);
                    break;
                case CharacterEventType.SmugglingSuccess:
                    list.Add(CharacterSkillType.SmugglingIncome);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.SmugglingEvasion);
                    relativeImportances.Add(1f);
                    break;
                case CharacterEventType.SmugglingDetection:
                    list.Add(CharacterSkillType.SmugglingIncome);
                    relativeImportances.Add(1f);
                    list.Add(CharacterSkillType.SmugglingEvasion);
                    relativeImportances.Add(1f);
                    break;
            }
            return list;
        }

        public static bool DetermineCharacterEventRelevantToRole(CharacterEventType eventType, CharacterRole role)
        {
            switch (role)
            {
                case CharacterRole.Ambassador:
                    switch (eventType)
                    {
                        case CharacterEventType.TreatySigned:
                        case CharacterEventType.WarStarted:
                        case CharacterEventType.WarEnded:
                        case CharacterEventType.TradeIncome:
                        case CharacterEventType.TourismIncome:
                        case CharacterEventType.IntelligenceMissionSucceedEspionage:
                        case CharacterEventType.IntelligenceMissionSucceedSabotage:
                        case CharacterEventType.IntelligenceMissionFailEspionage:
                        case CharacterEventType.IntelligenceMissionFailSabotage:
                        case CharacterEventType.IntelligenceMissionInterceptEnemy:
                        case CharacterEventType.IntelligenceAgentOursCaptured:
                        case CharacterEventType.IntelligenceAgentRecruited:
                        case CharacterEventType.TargetOfFailedAssassination:
                        case CharacterEventType.Subjugated:
                        case CharacterEventType.TreatyBroken:
                        case CharacterEventType.AmbassadorAssignedToEmpire:
                        case CharacterEventType.CharacterStart:
                        case CharacterEventType.CharacterTraitGain:
                        case CharacterEventType.CharacterSkillGain:
                        case CharacterEventType.CharacterSkillProgress:
                        case CharacterEventType.CharacterTransferLocation:
                            return true;
                    }
                    break;
                case CharacterRole.ColonyGovernor:
                    switch (eventType)
                    {
                        case CharacterEventType.TradeIncome:
                        case CharacterEventType.TourismIncome:
                        case CharacterEventType.ColonyDevelopmentIncrease:
                        case CharacterEventType.ColonyDevelopmentDecrease:
                        case CharacterEventType.CashNegative:
                        case CharacterEventType.CashPositive:
                        case CharacterEventType.TroopComplete:
                        case CharacterEventType.BuildMilitaryShip:
                        case CharacterEventType.BuildCivilianShip:
                        case CharacterEventType.BuildColonyShip:
                        case CharacterEventType.BuildMilitaryBase:
                        case CharacterEventType.BuildSpaceport:
                        case CharacterEventType.BuildResearchStationWeapons:
                        case CharacterEventType.BuildResearchStationEnergy:
                        case CharacterEventType.BuildResearchStationHighTech:
                        case CharacterEventType.BuildMiningStation:
                        case CharacterEventType.BuildResortBase:
                        case CharacterEventType.BuildOtherBase:
                        case CharacterEventType.BuildFacility:
                        case CharacterEventType.BuildWonder:
                        case CharacterEventType.TargetOfFailedAssassination:
                        case CharacterEventType.CharacterStart:
                        case CharacterEventType.CharacterTraitGain:
                        case CharacterEventType.CharacterSkillGain:
                        case CharacterEventType.CharacterSkillProgress:
                        case CharacterEventType.CharacterTransferLocation:
                            return true;
                    }
                    break;
                case CharacterRole.FleetAdmiral:
                    switch (eventType)
                    {
                        case CharacterEventType.WarStarted:
                        case CharacterEventType.WarEnded:
                        case CharacterEventType.HyperjumpExit:
                        case CharacterEventType.SpaceBattle:
                        case CharacterEventType.TargetOfFailedAssassination:
                        case CharacterEventType.CharacterStart:
                        case CharacterEventType.CharacterTraitGain:
                        case CharacterEventType.CharacterSkillGain:
                        case CharacterEventType.CharacterSkillProgress:
                        case CharacterEventType.CharacterTransferLocation:
                        case CharacterEventType.Raid:
                            return true;
                    }
                    break;
                case CharacterRole.ShipCaptain:
                    switch (eventType)
                    {
                        case CharacterEventType.WarStarted:
                        case CharacterEventType.WarEnded:
                        case CharacterEventType.HyperjumpExit:
                        case CharacterEventType.SpaceBattle:
                        case CharacterEventType.TargetOfFailedAssassination:
                        case CharacterEventType.CharacterStart:
                        case CharacterEventType.CharacterTraitGain:
                        case CharacterEventType.CharacterSkillGain:
                        case CharacterEventType.CharacterSkillProgress:
                        case CharacterEventType.CharacterTransferLocation:
                        case CharacterEventType.Boarding:
                        case CharacterEventType.Raid:
                        case CharacterEventType.SmugglingSuccess:
                        case CharacterEventType.SmugglingDetection:
                            return true;
                    }
                    break;
                case CharacterRole.IntelligenceAgent:
                    switch (eventType)
                    {
                        case CharacterEventType.IntelligenceMissionSucceedEspionage:
                        case CharacterEventType.IntelligenceMissionSucceedSabotage:
                        case CharacterEventType.IntelligenceMissionFailEspionage:
                        case CharacterEventType.IntelligenceMissionFailSabotage:
                        case CharacterEventType.IntelligenceMissionInterceptEnemy:
                        case CharacterEventType.IntelligenceAgentOursCaptured:
                        case CharacterEventType.IntelligenceAgentRecruited:
                        case CharacterEventType.TargetOfFailedAssassination:
                        case CharacterEventType.CharacterStart:
                        case CharacterEventType.CharacterTraitGain:
                        case CharacterEventType.CharacterSkillGain:
                        case CharacterEventType.CharacterSkillProgress:
                        case CharacterEventType.CharacterTransferLocation:
                            return true;
                    }
                    break;
                case CharacterRole.Leader:
                case CharacterRole.PirateLeader:
                    return true;
                case CharacterRole.Scientist:
                    switch (eventType)
                    {
                        case CharacterEventType.ResearchAdvanceWeapons:
                        case CharacterEventType.ResearchAdvanceEnergy:
                        case CharacterEventType.ResearchAdvanceHighTech:
                        case CharacterEventType.BuildResearchStationWeapons:
                        case CharacterEventType.BuildResearchStationEnergy:
                        case CharacterEventType.BuildResearchStationHighTech:
                        case CharacterEventType.TargetOfFailedAssassination:
                        case CharacterEventType.CriticalResearchSuccess:
                        case CharacterEventType.CriticalResearchFailure:
                        case CharacterEventType.CharacterStart:
                        case CharacterEventType.CharacterTraitGain:
                        case CharacterEventType.CharacterSkillGain:
                        case CharacterEventType.CharacterSkillProgress:
                        case CharacterEventType.CharacterTransferLocation:
                            return true;
                    }
                    break;
                case CharacterRole.TroopGeneral:
                    switch (eventType)
                    {
                        case CharacterEventType.WarStarted:
                        case CharacterEventType.WarEnded:
                        case CharacterEventType.TroopComplete:
                        case CharacterEventType.BuildMilitaryBase:
                        case CharacterEventType.BuildFacility:
                        case CharacterEventType.BuildWonder:
                        case CharacterEventType.GroundInvasion:
                        case CharacterEventType.TargetOfFailedAssassination:
                        case CharacterEventType.CharacterStart:
                        case CharacterEventType.CharacterTraitGain:
                        case CharacterEventType.CharacterSkillGain:
                        case CharacterEventType.CharacterSkillProgress:
                        case CharacterEventType.CharacterTransferLocation:
                            return true;
                    }
                    break;
            }
            return false;
        }

        public static bool DetermineCharacterEventIsPublic(CharacterEventType eventType)
        {
            switch (eventType)
            {
                case CharacterEventType.TreatySigned:
                case CharacterEventType.WarStarted:
                case CharacterEventType.WarEnded:
                case CharacterEventType.CashNegative:
                case CharacterEventType.TroopComplete:
                case CharacterEventType.IntelligenceMissionSucceedEspionage:
                case CharacterEventType.IntelligenceMissionSucceedSabotage:
                case CharacterEventType.IntelligenceMissionFailEspionage:
                case CharacterEventType.IntelligenceMissionFailSabotage:
                case CharacterEventType.IntelligenceMissionInterceptEnemy:
                case CharacterEventType.IntelligenceAgentOursCaptured:
                case CharacterEventType.IntelligenceAgentRecruited:
                case CharacterEventType.ResearchAdvanceWeapons:
                case CharacterEventType.ResearchAdvanceEnergy:
                case CharacterEventType.ResearchAdvanceHighTech:
                case CharacterEventType.BuildColonyShip:
                case CharacterEventType.BuildMilitaryBase:
                case CharacterEventType.BuildSpaceport:
                case CharacterEventType.BuildResearchStationWeapons:
                case CharacterEventType.BuildResearchStationEnergy:
                case CharacterEventType.BuildResearchStationHighTech:
                case CharacterEventType.BuildMiningStation:
                case CharacterEventType.BuildResortBase:
                case CharacterEventType.BuildOtherBase:
                case CharacterEventType.BuildFacility:
                case CharacterEventType.BuildWonder:
                case CharacterEventType.SpaceBattle:
                case CharacterEventType.GroundInvasion:
                case CharacterEventType.TargetOfFailedAssassination:
                case CharacterEventType.AmbassadorAssignedToEmpire:
                case CharacterEventType.CriticalResearchSuccess:
                case CharacterEventType.CriticalResearchFailure:
                case CharacterEventType.CharacterStart:
                case CharacterEventType.CharacterTraitGain:
                case CharacterEventType.CharacterSkillGain:
                case CharacterEventType.CharacterSkillProgress:
                case CharacterEventType.CharacterTransferLocation:
                case CharacterEventType.Boarding:
                case CharacterEventType.Raid:
                case CharacterEventType.SmugglingSuccess:
                case CharacterEventType.SmugglingDetection:
                    return true;
                default:
                    return false;
            }
        }

        public List<CharacterTraitType> IntersectTraitLists(List<CharacterTraitType> traits1, List<CharacterTraitType> traits2)
        {
            List<CharacterTraitType> list = new List<CharacterTraitType>();
            if (traits1 != null && traits2 != null)
            {
                for (int i = 0; i < traits1.Count; i++)
                {
                    if (traits2.Contains(traits1[i]))
                    {
                        list.Add(traits1[i]);
                    }
                }
            }
            return list;
        }

        public Habitat ResolveNearestLocation(StellarObject attackedTarget, BuiltObject ship, out bool nearby)
        {
            nearby = false;
            Habitat habitat = null;
            if (attackedTarget == null)
            {
                if (ship != null)
                {
                    habitat = FindNearestHabitat(ship.Xpos, ship.Ypos);
                    if (habitat != null)
                    {
                        double num = CalculateDistanceSquared(ship.Xpos, ship.Ypos, habitat.Xpos, habitat.Ypos);
                        if (num < 9000000.0)
                        {
                            nearby = true;
                        }
                    }
                }
            }
            else if (attackedTarget is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)attackedTarget;
                if (builtObject.ParentHabitat != null)
                {
                    habitat = builtObject.ParentHabitat;
                    nearby = true;
                }
                else
                {
                    habitat = FindNearestHabitat(builtObject.Xpos, builtObject.Ypos);
                    if (habitat != null)
                    {
                        double num2 = CalculateDistanceSquared(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos);
                        if (num2 < 9000000.0)
                        {
                            nearby = true;
                        }
                    }
                }
            }
            else if (attackedTarget is Habitat)
            {
                habitat = (Habitat)attackedTarget;
                nearby = true;
            }
            return habitat;
        }

        public int CalculateEmpireScore(Empire empire)
        {
            int population;
            int economy;
            int colonies;
            int military;
            int research;
            int wonders;
            return CalculateEmpireScore(empire, out population, out economy, out colonies, out military, out research, out wonders);
        }

        public int CalculateEmpireScore(Empire empire, out int population, out int economy, out int colonies, out int military, out int research, out int wonders)
        {
            int result = 0;
            population = 0;
            economy = 0;
            colonies = 0;
            military = 0;
            research = 0;
            wonders = 0;
            if (empire != null)
            {
                if (empire.PirateEmpireBaseHabitat == null)
                {
                    population = (int)(empire.TotalPopulation / 1000000);
                    economy = (int)(empire.PrivateAnnualRevenue / 10.0);
                    colonies = empire.Colonies.Count * 1000;
                    military = empire.MilitaryPotency * 5;
                    research = (int)(empire.Research.TechTree.CalculateTotalCostResearchedProjects() / (float)BaseTechCost * 20f);
                    research = Math.Max(0, Math.Min(1000000, research));
                    wonders = empire.CumulateFacilityValue1(PlanetaryFacilityType.Wonder, mustBeCompleted: true) * 100;
                }
                else
                {
                    population = (int)(empire.CalculatePirateControlPopulationValue() / 1000000);
                    double num = 0.0;
                    if (empire.PirateEconomy != null)
                    {
                        if (empire.PirateEconomy.LastYear != null)
                        {
                            num = empire.PirateEconomy.LastYear.TotalIncome;
                        }
                        else if (empire.PirateEconomy.ThisYear != null)
                        {
                            num = empire.PirateEconomy.ThisYear.TotalIncome;
                        }
                    }
                    economy = (int)(num / 10.0);
                    colonies = empire.Colonies.Count * 1000;
                    military = empire.MilitaryPotency * 5;
                    research = (int)(empire.Research.TechTree.CalculateTotalCostResearchedProjects() / (float)BaseTechCost * 20f);
                    research = Math.Max(0, Math.Min(1000000, research));
                    wonders += empire.CountFacilities(PlanetaryFacilityType.PirateBase, mustBeCompleted: true) * 1000;
                    wonders += empire.CountFacilities(PlanetaryFacilityType.PirateFortress, mustBeCompleted: true) * 2000;
                    wonders += empire.CountFacilities(PlanetaryFacilityType.PirateCriminalNetwork, mustBeCompleted: true) * 5000;
                }
                result = population + economy + colonies + military + research + wonders;
            }
            return result;
        }

        public GameSummary DetermineGameSummary()
        {
            GameSummary gameSummary = new GameSummary();
            gameSummary.DifficultyLevel = DifficultyLevel;
            gameSummary.GalaxyStarCount = StarCount;
            if (PlayerEmpire != null)
            {
                gameSummary.PlayerAchievements = PlayerEmpire.Achievements;
                gameSummary.PlayerEmpireName = PlayerEmpire.Name;
                GovernmentAttributes governmentAttributes = null;
                if (PlayerEmpire.GovernmentId >= 0 && PlayerEmpire.GovernmentId < Governments.Count)
                {
                    governmentAttributes = Governments[PlayerEmpire.GovernmentId];
                    gameSummary.PlayerGovernmentName = governmentAttributes.Name;
                }
                gameSummary.PlayerMainColor = PlayerEmpire.MainColor;
                gameSummary.PlayerRace = PlayerEmpire.DominantRace;
                gameSummary.PlayerScore = CalculateEmpireScore(PlayerEmpire);
            }
            return gameSummary;
        }

        public void ReviewAchievements()
        {
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Counters != null)
                {
                    empire.Achievements = ReviewAchievementsForEmpire(empire);
                    empire.Score = CalculateEmpireScore(empire);
                }
            }
            for (int j = 0; j < PirateEmpires.Count; j++)
            {
                Empire empire2 = PirateEmpires[j];
                if (empire2 != null && empire2.Counters != null)
                {
                    empire2.Achievements = ReviewAchievementsForEmpire(empire2);
                    empire2.Score = CalculateEmpireScore(empire2);
                }
            }
        }

        public AchievementList ReviewAchievementsForEmpire(Empire empire)
        {
            AchievementList achievementList = new AchievementList();
            if (empire != null && empire.Counters != null)
            {
                bool flag = false;
                long num = CurrentStarDate - ActualStartDate;
                if (num > RealSecondsInGalacticYear * 1000 * 10)
                {
                    flag = true;
                }
                if (GameRaceSpecificVictoryConditionsEnabled)
                {
                    RaceVictoryConditionProgressList conditionProgresses = new RaceVictoryConditionProgressList();
                    double num2 = CalculateRaceVictoryConditionsProgress(this, empire, empire.DominantRace, out conditionProgresses);
                    if (num2 >= 1.0)
                    {
                        achievementList.Add(new Achievement(AchievementType.AchieveAllRaceVictoryConditions, 0, empire.DominantRace));
                    }
                }
                if (empire.Counters.DestroyedEnemyMilitaryShipCount >= 50)
                {
                    achievementList.Add(new Achievement(AchievementType.DestroyEnemyMilitaryShipsAndBases, empire.Counters.DestroyedEnemyMilitaryShipCount, null));
                }
                if (empire.Counters.DestroyedEnemyCivilianShipCount >= 50)
                {
                    achievementList.Add(new Achievement(AchievementType.DestroyEnemyCivilianShipsAndBases, empire.Counters.DestroyedEnemyCivilianShipCount, null));
                }
                if (empire.Counters.DestroyedEnemyTroopCount >= 50)
                {
                    achievementList.Add(new Achievement(AchievementType.DestroyEnemyTroops, empire.Counters.DestroyedEnemyTroopCount, null));
                }
                int num3 = empire.Counters.DestroyedCreatureCountArdilus + empire.Counters.DestroyedCreatureCountKaltor + empire.Counters.DestroyedCreatureCountSandSlug + empire.Counters.DestroyedCreatureCountSpaceSlug;
                if (num3 >= 20)
                {
                    achievementList.Add(new Achievement(AchievementType.DestroySpaceMonsters, num3, null));
                }
                if (empire.Counters.DestroyedCreatureCountSilverMist >= 10)
                {
                    achievementList.Add(new Achievement(AchievementType.DestroySilverMists, empire.Counters.DestroyedCreatureCountSilverMist, null));
                }
                if (empire.Counters.ColoniesConqueredCount >= 10)
                {
                    achievementList.Add(new Achievement(AchievementType.ConquerEnemyColonies, empire.Counters.ColoniesConqueredCount, null));
                }
                int num4 = empire.Counters.IntelligenceMissionSuccessEspionageCount + empire.Counters.IntelligenceMissionSuccessSabotageCount;
                if (num4 >= 25)
                {
                    achievementList.Add(new Achievement(AchievementType.SuccessfulIntelligenceMissions, num4, null));
                }
                if (empire.Counters.WarsWeStartedCount >= 20)
                {
                    achievementList.Add(new Achievement(AchievementType.StartWars, empire.Counters.WarsWeStartedCount, null));
                }
                if (empire.Counters.BrokenTreatyCount >= 20)
                {
                    achievementList.Add(new Achievement(AchievementType.BreakTreaties, empire.Counters.BrokenTreatyCount, null));
                }
                if (empire.Counters.KillEnemyCharactersCount >= 20)
                {
                    achievementList.Add(new Achievement(AchievementType.EliminateEnemyCharacters, empire.Counters.KillEnemyCharactersCount, null));
                }
                if (empire.Counters.EliminateEmpireCount >= 1)
                {
                    achievementList.Add(new Achievement(AchievementType.EliminateEnemyEmpires, empire.Counters.EliminateEmpireCount, null));
                }
                if (empire.Counters.EliminatePirateEmpireCount >= 5)
                {
                    achievementList.Add(new Achievement(AchievementType.EliminatePirateFactions, empire.Counters.EliminatePirateEmpireCount, null));
                }
                double num5 = CalculateHighestTradeVolume();
                if (empire.Counters.TradeIncomeTotalVolume >= num5 && empire.Counters.TradeIncomeTotalVolume > 0.0 && flag)
                {
                    int value = Math.Min(int.MaxValue, (int)empire.Counters.TradeIncomeTotalVolume);
                    achievementList.Add(new Achievement(AchievementType.HighestTradeIncome, value, null));
                }
                double num6 = CalculateHighestMiningVolume();
                int num7 = empire.Counters.MiningExtractionGas + empire.Counters.MiningExtractionLuxury + empire.Counters.MiningExtractionStrategic;
                if ((double)num7 >= num6 && num7 > 0 && flag)
                {
                    achievementList.Add(new Achievement(AchievementType.HighestMiningVolume, num7, null));
                }
                if (empire.Counters.CaptureShipCount >= 10)
                {
                    achievementList.Add(new Achievement(AchievementType.CaptureEnemyShips, empire.Counters.CaptureShipCount, null));
                }
                long currentStarDate = CurrentStarDate;
                long num8 = empire.Counters.TimeSpentAtWar(currentStarDate);
                long num9 = currentStarDate - _StartStarDate;
                double num10 = (double)num8 / (double)num9;
                if (num10 >= 0.9 && flag)
                {
                    achievementList.Add(new Achievement(AchievementType.SpendAllTimeAtWar, 0, null));
                }
                else if (num10 <= 0.0 && flag)
                {
                    achievementList.Add(new Achievement(AchievementType.SpendNoTimeAtWar, 0, null));
                }
                if (empire.Counters.RaidSuccessCount >= 10)
                {
                    achievementList.Add(new Achievement(AchievementType.SuccessfulRaids, empire.Counters.RaidSuccessCount, null));
                }
                int num11 = empire.CountFacilities(PlanetaryFacilityType.Wonder, mustBeCompleted: true);
                if (num11 >= 1)
                {
                    achievementList.Add(new Achievement(AchievementType.BuildWonders, num11, null));
                }
                if (empire.Achievements.ContainsType(AchievementType.OwnOperationalPlanetDestroyer))
                {
                    achievementList.Add(new Achievement(AchievementType.OwnOperationalPlanetDestroyer, 0, null));
                }
                else
                {
                    int num12 = 0;
                    for (int i = 0; i < empire.BuiltObjects.Count; i++)
                    {
                        BuiltObject builtObject = empire.BuiltObjects[i];
                        if (builtObject != null && builtObject.UnbuiltComponentCount <= 0 && builtObject.SubRole == BuiltObjectSubRole.CapitalShip && builtObject.Components.ContainsComponentId(25))
                        {
                            num12++;
                        }
                    }
                    if (num12 > 0)
                    {
                        achievementList.Add(new Achievement(AchievementType.OwnOperationalPlanetDestroyer, num12, null));
                    }
                }
                if (empire.Achievements.ContainsType(AchievementType.JoinTheFreedomAlliance))
                {
                    achievementList.Add(new Achievement(AchievementType.JoinTheFreedomAlliance, 0, null));
                }
                else
                {
                    Empire empire2 = IdentifyMechanoidEmpire();
                    if (empire2 != null && empire.PirateEmpireBaseHabitat == null)
                    {
                        DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(empire2);
                        if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact && diplomaticRelation.Locked)
                        {
                            achievementList.Add(new Achievement(AchievementType.JoinTheFreedomAlliance, 0, null));
                        }
                    }
                }
                if (empire.Achievements.ContainsType(AchievementType.JoinTheShakturi))
                {
                    achievementList.Add(new Achievement(AchievementType.JoinTheShakturi, 0, null));
                }
                else
                {
                    Empire empire3 = IdentifyShakturiEmpire();
                    if (empire3 != null && empire.PirateEmpireBaseHabitat == null)
                    {
                        DiplomaticRelation diplomaticRelation2 = empire.ObtainDiplomaticRelation(empire3);
                        if (diplomaticRelation2 != null && diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact)
                        {
                            achievementList.Add(new Achievement(AchievementType.JoinTheShakturi, 0, null));
                        }
                    }
                }
                if (empire.EmpireSplitCount > 0)
                {
                    achievementList.Add(new Achievement(AchievementType.EmpireSplits, empire.EmpireSplitCount, null));
                }
                if (empire.HaveDefeatedAncientGuardians)
                {
                    achievementList.Add(new Achievement(AchievementType.DefeatAncients, 0, null));
                }
                if (empire.HaveDefeatedShakturi)
                {
                    achievementList.Add(new Achievement(AchievementType.DefeatShakturi, 0, null));
                }
                if (empire.DefeatedLegendaryPiratesCount > 0)
                {
                    achievementList.Add(new Achievement(AchievementType.DefeatLegendaryPirates, empire.DefeatedLegendaryPiratesCount, null));
                }
                if (empire.GovernmentAttributes != null && empire.GovernmentAttributes.Availability == 3)
                {
                    achievementList.Add(new Achievement(AchievementType.ChangeGovernmentToWayOfDarkness, 0, null));
                }
                if (empire.GovernmentAttributes != null && empire.GovernmentAttributes.Availability == 2)
                {
                    achievementList.Add(new Achievement(AchievementType.ChangeGovernmentToWayOfTheAncients, 0, null));
                }
            }
            return achievementList;
        }

        public static int ResolveAchievementMedalImageIndex(AchievementType achievementType, int level)
        {
            int num = 0;
            if (level > 0)
            {
                level--;
            }
            switch (achievementType)
            {
                case AchievementType.AchieveAllRaceVictoryConditions:
                    num = 0;
                    break;
                case AchievementType.DestroyEnemyMilitaryShipsAndBases:
                    num = 1;
                    break;
                case AchievementType.DestroyEnemyCivilianShipsAndBases:
                    num = 4;
                    break;
                case AchievementType.DestroyEnemyTroops:
                    num = 7;
                    break;
                case AchievementType.DestroySpaceMonsters:
                    num = 10;
                    break;
                case AchievementType.DestroySilverMists:
                    num = 13;
                    break;
                case AchievementType.ConquerEnemyColonies:
                    num = 16;
                    break;
                case AchievementType.StartWars:
                    num = 19;
                    break;
                case AchievementType.BreakTreaties:
                    num = 20;
                    break;
                case AchievementType.EliminateEnemyCharacters:
                    num = 21;
                    break;
                case AchievementType.EliminateEnemyEmpires:
                    num = 24;
                    break;
                case AchievementType.HighestTradeIncome:
                    num = 27;
                    break;
                case AchievementType.HighestMiningVolume:
                    num = 28;
                    break;
                case AchievementType.CaptureEnemyShips:
                    num = 29;
                    break;
                case AchievementType.EliminatePirateFactions:
                    num = 32;
                    break;
                case AchievementType.SpendAllTimeAtWar:
                    num = 35;
                    break;
                case AchievementType.SpendNoTimeAtWar:
                    num = 36;
                    break;
                case AchievementType.SuccessfulRaids:
                    num = 37;
                    break;
                case AchievementType.ChangeGovernmentToWayOfDarkness:
                    num = 40;
                    break;
                case AchievementType.ChangeGovernmentToWayOfTheAncients:
                    num = 41;
                    break;
                case AchievementType.EmpireSplits:
                    num = 42;
                    break;
                case AchievementType.BuildWonders:
                    num = 43;
                    break;
                case AchievementType.OwnOperationalPlanetDestroyer:
                    num = 46;
                    break;
                case AchievementType.JoinTheFreedomAlliance:
                    num = 47;
                    break;
                case AchievementType.JoinTheShakturi:
                    num = 48;
                    break;
                case AchievementType.DefeatAncients:
                    num = 49;
                    break;
                case AchievementType.DefeatShakturi:
                    num = 50;
                    break;
                case AchievementType.DefeatLegendaryPirates:
                    num = 51;
                    break;
                case AchievementType.SuccessfulIntelligenceMissions:
                    num = 52;
                    break;
            }
            return num + level;
        }

        public static int DetermineAchievementValueForLevel(AchievementType achievementType, int level)
        {
            int result = 0;
            switch (achievementType)
            {
                case AchievementType.DestroyEnemyMilitaryShipsAndBases:
                case AchievementType.DestroyEnemyCivilianShipsAndBases:
                case AchievementType.DestroyEnemyTroops:
                    switch (level)
                    {
                        case 1:
                            result = 50;
                            break;
                        case 2:
                            result = 100;
                            break;
                        case 3:
                            result = 1000;
                            break;
                    }
                    break;
                case AchievementType.DestroySpaceMonsters:
                case AchievementType.EliminateEnemyCharacters:
                    switch (level)
                    {
                        case 1:
                            result = 20;
                            break;
                        case 2:
                            result = 50;
                            break;
                        case 3:
                            result = 100;
                            break;
                    }
                    break;
                case AchievementType.DestroySilverMists:
                case AchievementType.ConquerEnemyColonies:
                case AchievementType.CaptureEnemyShips:
                case AchievementType.SuccessfulRaids:
                    switch (level)
                    {
                        case 1:
                            result = 10;
                            break;
                        case 2:
                            result = 50;
                            break;
                        case 3:
                            result = 100;
                            break;
                    }
                    break;
                case AchievementType.SuccessfulIntelligenceMissions:
                    switch (level)
                    {
                        case 1:
                            result = 25;
                            break;
                        case 2:
                            result = 100;
                            break;
                        case 3:
                            result = 1000;
                            break;
                    }
                    break;
                case AchievementType.EliminateEnemyEmpires:
                case AchievementType.BuildWonders:
                    switch (level)
                    {
                        case 1:
                            result = 1;
                            break;
                        case 2:
                            result = 5;
                            break;
                        case 3:
                            result = 10;
                            break;
                    }
                    break;
                case AchievementType.EliminatePirateFactions:
                    switch (level)
                    {
                        case 1:
                            result = 5;
                            break;
                        case 2:
                            result = 10;
                            break;
                        case 3:
                            result = 20;
                            break;
                    }
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
        }

        public static int DetermineAchievementLevel(AchievementType achievementType, int value)
        {
            int result = 0;
            switch (achievementType)
            {
                case AchievementType.AchieveAllRaceVictoryConditions:
                case AchievementType.StartWars:
                case AchievementType.BreakTreaties:
                case AchievementType.HighestTradeIncome:
                case AchievementType.HighestMiningVolume:
                case AchievementType.SpendAllTimeAtWar:
                case AchievementType.SpendNoTimeAtWar:
                case AchievementType.ChangeGovernmentToWayOfDarkness:
                case AchievementType.ChangeGovernmentToWayOfTheAncients:
                case AchievementType.EmpireSplits:
                case AchievementType.OwnOperationalPlanetDestroyer:
                case AchievementType.JoinTheFreedomAlliance:
                case AchievementType.JoinTheShakturi:
                case AchievementType.DefeatAncients:
                case AchievementType.DefeatShakturi:
                case AchievementType.DefeatLegendaryPirates:
                    result = 1;
                    break;
                case AchievementType.DestroyEnemyMilitaryShipsAndBases:
                case AchievementType.DestroyEnemyCivilianShipsAndBases:
                case AchievementType.DestroyEnemyTroops:
                    if (value >= 1000)
                    {
                        result = 3;
                    }
                    else if (value >= 100)
                    {
                        result = 2;
                    }
                    else if (value >= 50)
                    {
                        result = 1;
                    }
                    break;
                case AchievementType.DestroySpaceMonsters:
                case AchievementType.EliminateEnemyCharacters:
                    if (value >= 100)
                    {
                        result = 3;
                    }
                    else if (value >= 50)
                    {
                        result = 2;
                    }
                    else if (value >= 20)
                    {
                        result = 1;
                    }
                    break;
                case AchievementType.DestroySilverMists:
                case AchievementType.ConquerEnemyColonies:
                case AchievementType.CaptureEnemyShips:
                case AchievementType.SuccessfulRaids:
                    if (value >= 100)
                    {
                        result = 3;
                    }
                    else if (value >= 50)
                    {
                        result = 2;
                    }
                    else if (value >= 10)
                    {
                        result = 1;
                    }
                    break;
                case AchievementType.SuccessfulIntelligenceMissions:
                    if (value >= 1000)
                    {
                        result = 3;
                    }
                    else if (value >= 100)
                    {
                        result = 2;
                    }
                    else if (value >= 25)
                    {
                        result = 1;
                    }
                    break;
                case AchievementType.EliminateEnemyEmpires:
                case AchievementType.BuildWonders:
                    if (value >= 10)
                    {
                        result = 3;
                    }
                    else if (value >= 5)
                    {
                        result = 2;
                    }
                    else if (value >= 1)
                    {
                        result = 1;
                    }
                    break;
                case AchievementType.EliminatePirateFactions:
                    if (value >= 20)
                    {
                        result = 3;
                    }
                    else if (value >= 10)
                    {
                        result = 2;
                    }
                    else if (value >= 5)
                    {
                        result = 1;
                    }
                    break;
            }
            return result;
        }

        public static string ResolveAchievementTitleComplete(Achievement achievement)
        {
            string text = ResolveTitle(achievement.Type, achievement.AdditionalData);
            int level = DetermineAchievementLevel(achievement.Type, achievement.Value);
            string text2 = ResolveAchievementLevelDescription(achievement.Type, level);
            string text3 = text;
            if (!string.IsNullOrEmpty(text2))
            {
                text3 = text3 + " " + text2;
            }
            return text3;
        }

        public static string ResolveAchievementLevelDescription(AchievementType achievementType, int level)
        {
            string result = string.Empty;
            switch (achievementType)
            {
                case AchievementType.DestroyEnemyMilitaryShipsAndBases:
                    switch (level)
                    {
                        case 1:
                            result = TextResolver.GetText("Achievement Level L");
                            break;
                        case 2:
                            result = TextResolver.GetText("Achievement Level C");
                            break;
                        case 3:
                            result = TextResolver.GetText("Achievement Level M");
                            break;
                    }
                    break;
                case AchievementType.DestroyEnemyCivilianShipsAndBases:
                case AchievementType.DestroyEnemyTroops:
                case AchievementType.DestroySpaceMonsters:
                case AchievementType.DestroySilverMists:
                case AchievementType.ConquerEnemyColonies:
                case AchievementType.SuccessfulIntelligenceMissions:
                case AchievementType.EliminateEnemyCharacters:
                case AchievementType.EliminateEnemyEmpires:
                case AchievementType.CaptureEnemyShips:
                case AchievementType.EliminatePirateFactions:
                case AchievementType.SuccessfulRaids:
                case AchievementType.BuildWonders:
                    switch (level)
                    {
                        case 1:
                            result = TextResolver.GetText("Achievement Level I");
                            break;
                        case 2:
                            result = TextResolver.GetText("Achievement Level II");
                            break;
                        case 3:
                            result = TextResolver.GetText("Achievement Level III");
                            break;
                    }
                    break;
            }
            return result;
        }

        public static string ResolveDescription(Achievement achievement)
        {
            string result = string.Empty;
            int level = DetermineAchievementLevel(achievement.Type, achievement.Value);
            int num = DetermineAchievementValueForLevel(achievement.Type, level);
            switch (achievement.Type)
            {
                case AchievementType.AchieveAllRaceVictoryConditions:
                    result = TextResolver.GetText("AchievementType AchieveAllRaceVictoryConditions");
                    break;
                case AchievementType.BreakTreaties:
                    result = TextResolver.GetText("AchievementType BreakTreaties");
                    break;
                case AchievementType.BuildWonders:
                    result = string.Format(TextResolver.GetText("AchievementType BuildWonders"), num.ToString());
                    break;
                case AchievementType.CaptureEnemyShips:
                    result = string.Format(TextResolver.GetText("AchievementType CaptureEnemyShips"), num.ToString());
                    break;
                case AchievementType.ChangeGovernmentToWayOfDarkness:
                    result = TextResolver.GetText("AchievementType ChangeGovernmentToWayOfDarkness");
                    break;
                case AchievementType.ChangeGovernmentToWayOfTheAncients:
                    result = TextResolver.GetText("AchievementType ChangeGovernmentToWayOfTheAncients");
                    break;
                case AchievementType.ConquerEnemyColonies:
                    result = string.Format(TextResolver.GetText("AchievementType ConquerEnemyColonies"), num.ToString());
                    break;
                case AchievementType.DefeatAncients:
                    result = TextResolver.GetText("AchievementType DefeatAncients");
                    break;
                case AchievementType.DefeatLegendaryPirates:
                    result = TextResolver.GetText("AchievementType DefeatLegendaryPirates");
                    break;
                case AchievementType.DefeatShakturi:
                    result = TextResolver.GetText("AchievementType DefeatShakturi");
                    break;
                case AchievementType.DestroyEnemyCivilianShipsAndBases:
                    result = string.Format(TextResolver.GetText("AchievementType DestroyEnemyCivilianShipsAndBases"), num.ToString());
                    break;
                case AchievementType.DestroyEnemyMilitaryShipsAndBases:
                    result = string.Format(TextResolver.GetText("AchievementType DestroyEnemyMilitaryShipsAndBases"), num.ToString());
                    break;
                case AchievementType.DestroyEnemyTroops:
                    result = string.Format(TextResolver.GetText("AchievementType DestroyEnemyTroops"), num.ToString());
                    break;
                case AchievementType.DestroySilverMists:
                    result = string.Format(TextResolver.GetText("AchievementType DestroySilverMists"), num.ToString());
                    break;
                case AchievementType.DestroySpaceMonsters:
                    result = string.Format(TextResolver.GetText("AchievementType DestroySpaceMonsters"), num.ToString());
                    break;
                case AchievementType.EliminateEnemyCharacters:
                    result = string.Format(TextResolver.GetText("AchievementType EliminateEnemyCharacters"), num.ToString());
                    break;
                case AchievementType.EliminateEnemyEmpires:
                    result = string.Format(TextResolver.GetText("AchievementType EliminateEnemyEmpires"), num.ToString());
                    break;
                case AchievementType.EliminatePirateFactions:
                    result = string.Format(TextResolver.GetText("AchievementType EliminatePirateFactions"), num.ToString());
                    break;
                case AchievementType.EmpireSplits:
                    result = TextResolver.GetText("AchievementType EmpireSplits");
                    break;
                case AchievementType.HighestMiningVolume:
                    result = TextResolver.GetText("AchievementType HighestMiningVolume");
                    break;
                case AchievementType.HighestTradeIncome:
                    result = TextResolver.GetText("AchievementType HighestTradeIncome");
                    break;
                case AchievementType.JoinTheFreedomAlliance:
                    result = TextResolver.GetText("AchievementType JoinTheFreedomAlliance");
                    break;
                case AchievementType.JoinTheShakturi:
                    result = TextResolver.GetText("AchievementType JoinTheShakturi");
                    break;
                case AchievementType.OwnOperationalPlanetDestroyer:
                    result = TextResolver.GetText("AchievementType OwnOperationalPlanetDestroyer");
                    break;
                case AchievementType.SpendAllTimeAtWar:
                    result = TextResolver.GetText("AchievementType SpendAllTimeAtWar");
                    break;
                case AchievementType.SpendNoTimeAtWar:
                    result = TextResolver.GetText("AchievementType SpendNoTimeAtWar");
                    break;
                case AchievementType.StartWars:
                    result = TextResolver.GetText("AchievementType StartWars");
                    break;
                case AchievementType.SuccessfulIntelligenceMissions:
                    result = string.Format(TextResolver.GetText("AchievementType SuccessfulIntelligenceMissions"), num.ToString());
                    break;
                case AchievementType.SuccessfulRaids:
                    result = string.Format(TextResolver.GetText("AchievementType SuccessfulRaids"), num.ToString());
                    break;
            }
            return result;
        }

        public static string ResolveTitle(AchievementType achievementType, object additionalData)
        {
            string result = string.Empty;
            switch (achievementType)
            {
                case AchievementType.AchieveAllRaceVictoryConditions:
                    if (additionalData != null && additionalData is Race)
                    {
                        Race race = (Race)additionalData;
                        result = string.Format(TextResolver.GetText("AchievementTitle AchieveAllRaceVictoryConditions"), race.Name);
                    }
                    break;
                case AchievementType.BreakTreaties:
                    result = TextResolver.GetText("AchievementTitle BreakTreaties");
                    break;
                case AchievementType.BuildWonders:
                    result = TextResolver.GetText("AchievementTitle BuildWonders");
                    break;
                case AchievementType.CaptureEnemyShips:
                    result = TextResolver.GetText("AchievementTitle CaptureEnemyShips");
                    break;
                case AchievementType.ChangeGovernmentToWayOfDarkness:
                    result = TextResolver.GetText("AchievementTitle ChangeGovernmentToWayOfDarkness");
                    break;
                case AchievementType.ChangeGovernmentToWayOfTheAncients:
                    result = TextResolver.GetText("AchievementTitle ChangeGovernmentToWayOfTheAncients");
                    break;
                case AchievementType.ConquerEnemyColonies:
                    result = TextResolver.GetText("AchievementTitle ConquerEnemyColonies");
                    break;
                case AchievementType.DefeatAncients:
                    result = TextResolver.GetText("AchievementTitle DefeatAncients");
                    break;
                case AchievementType.DefeatLegendaryPirates:
                    result = TextResolver.GetText("AchievementTitle DefeatLegendaryPirates");
                    break;
                case AchievementType.DefeatShakturi:
                    result = TextResolver.GetText("AchievementTitle DefeatShakturi");
                    break;
                case AchievementType.DestroyEnemyCivilianShipsAndBases:
                    result = TextResolver.GetText("AchievementTitle DestroyEnemyCivilianShipsAndBases");
                    break;
                case AchievementType.DestroyEnemyMilitaryShipsAndBases:
                    result = TextResolver.GetText("AchievementTitle DestroyEnemyMilitaryShipsAndBases");
                    break;
                case AchievementType.DestroyEnemyTroops:
                    result = TextResolver.GetText("AchievementTitle DestroyEnemyTroops");
                    break;
                case AchievementType.DestroySilverMists:
                    result = TextResolver.GetText("AchievementTitle DestroySilverMists");
                    break;
                case AchievementType.DestroySpaceMonsters:
                    result = TextResolver.GetText("AchievementTitle DestroySpaceMonsters");
                    break;
                case AchievementType.EliminateEnemyCharacters:
                    result = TextResolver.GetText("AchievementTitle EliminateEnemyCharacters");
                    break;
                case AchievementType.EliminateEnemyEmpires:
                    result = TextResolver.GetText("AchievementTitle EliminateEnemyEmpires");
                    break;
                case AchievementType.EliminatePirateFactions:
                    result = TextResolver.GetText("AchievementTitle EliminatePirateFactions");
                    break;
                case AchievementType.EmpireSplits:
                    result = TextResolver.GetText("AchievementTitle EmpireSplits");
                    break;
                case AchievementType.HighestMiningVolume:
                    result = TextResolver.GetText("AchievementTitle HighestMiningVolume");
                    break;
                case AchievementType.HighestTradeIncome:
                    result = TextResolver.GetText("AchievementTitle HighestTradeIncome");
                    break;
                case AchievementType.JoinTheFreedomAlliance:
                    result = TextResolver.GetText("AchievementTitle JoinTheFreedomAlliance");
                    break;
                case AchievementType.JoinTheShakturi:
                    result = TextResolver.GetText("AchievementTitle JoinTheShakturi");
                    break;
                case AchievementType.OwnOperationalPlanetDestroyer:
                    result = TextResolver.GetText("AchievementTitle OwnOperationalPlanetDestroyer");
                    break;
                case AchievementType.SpendAllTimeAtWar:
                    result = TextResolver.GetText("AchievementTitle SpendAllTimeAtWar");
                    break;
                case AchievementType.SpendNoTimeAtWar:
                    result = TextResolver.GetText("AchievementTitle SpendNoTimeAtWar");
                    break;
                case AchievementType.StartWars:
                    result = TextResolver.GetText("AchievementTitle StartWars");
                    break;
                case AchievementType.SuccessfulIntelligenceMissions:
                    result = TextResolver.GetText("AchievementTitle SuccessfulIntelligenceMissions");
                    break;
                case AchievementType.SuccessfulRaids:
                    result = TextResolver.GetText("AchievementTitle SuccessfulRaids");
                    break;
            }
            return result;
        }

        public int CalculateHighestMiningVolume()
        {
            int num = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Counters != null)
                {
                    int num2 = empire.Counters.MiningExtractionGas + empire.Counters.MiningExtractionLuxury + empire.Counters.MiningExtractionStrategic;
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }
            }
            return num;
        }

        public double CalculateHighestTradeVolume()
        {
            double num = 0.0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Counters != null && empire.Counters.TradeIncomeTotalVolume > num)
                {
                    num = empire.Counters.TradeIncomeTotalVolume;
                }
            }
            return num;
        }

        public void DoCharacterEventLeader(CharacterEventType eventType, object eventData, Empire leaderEmpire)
        {
            CharacterList sourceCharacters = new CharacterList();
            DoCharacterEvent(eventType, eventData, sourceCharacters, includeLeader: true, leaderEmpire);
        }

        public void DoCharacterEvent(CharacterEventType eventType, object eventData, Character character)
        {
            CharacterList characterList = new CharacterList();
            characterList.Add(character);
            DoCharacterEvent(eventType, eventData, characterList);
        }

        public void DoCharacterEvent(CharacterEventType eventType, object eventData, CharacterList characters)
        {
            DoCharacterEvent(eventType, eventData, characters, includeLeader: false, null);
        }

        public void DoCharacterEvent(CharacterEventType eventType, object eventData, Character character, bool includeLeader, Empire leaderEmpire)
        {
            CharacterList characterList = new CharacterList();
            characterList.Add(character);
            DoCharacterEvent(eventType, eventData, characterList, includeLeader, leaderEmpire);
        }

        public void DoCharacterEvent(CharacterEventType eventType, object eventData, CharacterList sourceCharacters, bool includeLeader, Empire leaderEmpire)
        {
            if (sourceCharacters == null || sourceCharacters.Count <= 0)
            {
                return;
            }
            CharacterList characterList = new CharacterList();
            characterList.AddRange(sourceCharacters);
            if (includeLeader && leaderEmpire != null)
            {
                if (leaderEmpire.PirateEmpireBaseHabitat == null)
                {
                    CharacterList charactersByRole = leaderEmpire.Characters.GetCharactersByRole(CharacterRole.Leader);
                    for (int i = 0; i < charactersByRole.Count; i++)
                    {
                        Character character = charactersByRole[i];
                        if (character != null && !characterList.Contains(character))
                        {
                            characterList.Add(character);
                        }
                    }
                }
                else
                {
                    CharacterList charactersByRole2 = leaderEmpire.Characters.GetCharactersByRole(CharacterRole.PirateLeader);
                    for (int j = 0; j < charactersByRole2.Count; j++)
                    {
                        Character character2 = charactersByRole2[j];
                        if (character2 != null && !characterList.Contains(character2))
                        {
                            characterList.Add(character2);
                        }
                    }
                }
            }
            List<float> relativeImportances = new List<float>();
            List<CharacterSkillType> list = DetermineCharacterSkillsAffectedByEvent(eventType, out relativeImportances);
            if (eventType == CharacterEventType.GroundInvasion && eventData != null && eventData is InvasionStats)
            {
                InvasionStats invasionStats = (InvasionStats)eventData;
                if (invasionStats.Colony != null && invasionStats.Colony.Troops != null)
                {
                    if (list.Contains(CharacterSkillType.TroopStrengthArmor) && invasionStats.Colony.Troops.CountByType(TroopType.Armored) <= 0)
                    {
                        list.Remove(CharacterSkillType.TroopStrengthArmor);
                    }
                    if (list.Contains(CharacterSkillType.TroopStrengthInfantry) && invasionStats.Colony.Troops.CountByType(TroopType.Infantry) <= 0)
                    {
                        list.Remove(CharacterSkillType.TroopStrengthInfantry);
                    }
                    if (list.Contains(CharacterSkillType.TroopStrengthSpecialForces) && invasionStats.Colony.Troops.CountByType(TroopType.SpecialForces) <= 0)
                    {
                        list.Remove(CharacterSkillType.TroopStrengthSpecialForces);
                    }
                    if (list.Contains(CharacterSkillType.TroopStrengthPlanetaryDefense) && invasionStats.Colony.Troops.CountByType(TroopType.Artillery) <= 0)
                    {
                        list.Remove(CharacterSkillType.TroopStrengthPlanetaryDefense);
                    }
                }
            }
            for (int k = 0; k < characterList.Count; k++)
            {
                Character character3 = characterList[k];
                if (character3 == null)
                {
                    continue;
                }
                if (DetermineCharacterEventRelevantToRole(eventType, character3.Role))
                {
                    long currentStarDate = CurrentStarDate;
                    CharacterEvent characterEvent = new CharacterEvent(eventType, eventData, currentStarDate);
                    character3.EventHistory.Add(characterEvent);
                }
                List<CharacterTraitType> traits = Character.DetermineValidTraitsForRole(character3.Role);
                List<CharacterTraitType> list2 = new List<CharacterTraitType>();
                List<CharacterTraitType> list3 = new List<CharacterTraitType>();
                List<CharacterSkillType> validSkillsForRole = Character.DetermineValidSkillsForRole(character3.Role);
                bool flag = Rnd.Next(0, 5) == 1;
                bool flag2 = Rnd.Next(0, 20) == 1;
                bool flag3 = Rnd.Next(0, 80) == 1;
                if (character3.BonusesKnown)
                {
                    switch (eventType)
                    {
                        case CharacterEventType.Boarding:
                            {
                                if (!flag || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                if (Rnd.Next(0, 2) == 1)
                                {
                                    list2.Add(CharacterTraitType.BountyHunter);
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType2 = list3[Rnd.Next(0, list3.Count)];
                                if (character3.AddTrait(characterTraitType2, starting: false, this))
                                {
                                    string empty = string.Empty;
                                    if (eventData != null && eventData is BuiltObject)
                                    {
                                        BuiltObject builtObject = (BuiltObject)eventData;
                                        empty = string.Format(TextResolver.GetText("Character New Trait Boarding Location"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType2), builtObject.Name);
                                    }
                                    else
                                    {
                                        empty = string.Format(TextResolver.GetText("Character New Trait Boarding"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType2));
                                    }
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, empty);
                                }
                                break;
                            }
                        case CharacterEventType.Raid:
                            {
                                if (!flag || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                if (Rnd.Next(0, 2) == 1)
                                {
                                    list2.Add(CharacterTraitType.BountyHunter);
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType25 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType25, starting: false, this))
                                {
                                    break;
                                }
                                string empty4 = string.Empty;
                                if (eventData != null)
                                {
                                    string text19 = string.Empty;
                                    if (eventData is Habitat)
                                    {
                                        text19 = ((Habitat)eventData).Name;
                                    }
                                    else if (eventData is BuiltObject)
                                    {
                                        text19 = ((BuiltObject)eventData).Name;
                                    }
                                    empty4 = string.Format(TextResolver.GetText("Character New Trait Raid Location"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType25), text19);
                                }
                                else
                                {
                                    empty4 = string.Format(TextResolver.GetText("Character New Trait Raid"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType25));
                                }
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, empty4);
                                break;
                            }
                        case CharacterEventType.SmugglingSuccess:
                            {
                                if (!flag || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Smuggler);
                                if (Rnd.Next(0, 3) == 1)
                                {
                                    list2.Add(CharacterTraitType.Addict);
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType9 = list3[Rnd.Next(0, list3.Count)];
                                if (character3.AddTrait(characterTraitType9, starting: false, this))
                                {
                                    string empty2 = string.Empty;
                                    if (eventData != null && eventData is StellarObject)
                                    {
                                        StellarObject stellarObject = (StellarObject)eventData;
                                        empty2 = string.Format(TextResolver.GetText("Character New Trait Smuggling Success Location"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType9), stellarObject.Name);
                                    }
                                    else
                                    {
                                        empty2 = string.Format(TextResolver.GetText("Character New Trait Smuggling Success"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType9));
                                    }
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, empty2);
                                }
                                break;
                            }
                        case CharacterEventType.CriticalResearchFailure:
                            if (flag && !DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) && character3.AddTrait(CharacterTraitType.Methodical, starting: false, this) && eventData != null && eventData is ResearchNode)
                            {
                                ResearchNode researchNode4 = (ResearchNode)eventData;
                                string text16 = string.Empty;
                                if (researchNode4 != null)
                                {
                                    text16 = researchNode4.Name;
                                }
                                string description20 = string.Format(TextResolver.GetText("Character New Trait Critical Research Failure"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(CharacterTraitType.Methodical), text16);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description20);
                            }
                            break;
                        case CharacterEventType.CriticalResearchSuccess:
                            if (flag && !DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) && character3.AddTrait(CharacterTraitType.Creative, starting: false, this) && eventData != null && eventData is ResearchNode)
                            {
                                ResearchNode researchNode = (ResearchNode)eventData;
                                string text9 = string.Empty;
                                if (researchNode != null)
                                {
                                    text9 = researchNode.Name;
                                }
                                string description10 = string.Format(TextResolver.GetText("Character New Trait Critical Research Success"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(CharacterTraitType.Creative), text9);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description10);
                            }
                            break;
                        case CharacterEventType.TargetOfFailedAssassination:
                            if (flag && !DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) && character3.AddTrait(CharacterTraitType.Paranoid, starting: false, this))
                            {
                                string description2 = string.Format(TextResolver.GetText("Character New Trait Failed Assassination"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(CharacterTraitType.Paranoid));
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description2);
                            }
                            break;
                        case CharacterEventType.WarStarted:
                            if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                            {
                                break;
                            }
                            list2.Add(CharacterTraitType.PeaceThroughStrength);
                            if (eventData != null && eventData is Empire)
                            {
                                Empire empire2 = (Empire)eventData;
                                if (empire2.DominantRace != null && character3.Empire.DominantRace != null && empire2.DominantRace.FamilyId != character3.Empire.DominantRace.FamilyId)
                                {
                                    list2.Add(CharacterTraitType.Xenophobic);
                                    list2.Add(CharacterTraitType.IntelligenceXenophobic);
                                }
                            }
                            list3 = IntersectTraitLists(traits, list2);
                            if (list3.Count > 0)
                            {
                                CharacterTraitType characterTraitType12 = list3[Rnd.Next(0, list3.Count)];
                                if (character3.AddTrait(characterTraitType12, starting: false, this))
                                {
                                    string description11 = string.Format(TextResolver.GetText("Character New Trait War Started"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType12));
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description11);
                                }
                            }
                            break;
                        case CharacterEventType.WarEnded:
                            if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                            {
                                break;
                            }
                            list2.Add(CharacterTraitType.Drunk);
                            list3 = IntersectTraitLists(traits, list2);
                            if (list3.Count > 0)
                            {
                                CharacterTraitType characterTraitType21 = list3[Rnd.Next(0, list3.Count)];
                                if (character3.AddTrait(characterTraitType21, starting: false, this))
                                {
                                    string description21 = string.Format(TextResolver.GetText("Character New Trait War Ended"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType21));
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description21);
                                }
                            }
                            break;
                        case CharacterEventType.GroundInvasion:
                            {
                                if (!flag || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) || eventData == null || !(eventData is InvasionStats))
                                {
                                    break;
                                }
                                InvasionStats invasionStats2 = (InvasionStats)eventData;
                                if (invasionStats2 == null || invasionStats2.Colony == null)
                                {
                                    break;
                                }
                                if (invasionStats2.Colony.Empire != null)
                                {
                                    if (invasionStats2.Colony.Empire == character3.Empire)
                                    {
                                        list2.AddRange(new CharacterTraitType[2]
                                        {
                                CharacterTraitType.GoodStrategist,
                                CharacterTraitType.GoodTactician
                                        });
                                        if (invasionStats2.DefendingEmpire == character3.Empire)
                                        {
                                            list2.Add(CharacterTraitType.StrongGroundDefender);
                                            if (invasionStats2.TroopsDamageToDefenders < invasionStats2.TroopsDamageToInvaders)
                                            {
                                                list2.Add(CharacterTraitType.CarefulAttacker);
                                            }
                                            else
                                            {
                                                list2.Add(CharacterTraitType.RecklessAttacker);
                                            }
                                        }
                                        else
                                        {
                                            list2.Add(CharacterTraitType.StrongGroundAttacker);
                                            if (invasionStats2.TroopsDamageToDefenders < invasionStats2.TroopsDamageToInvaders)
                                            {
                                                list2.Add(CharacterTraitType.RecklessAttacker);
                                            }
                                            else
                                            {
                                                list2.Add(CharacterTraitType.CarefulAttacker);
                                            }
                                        }
                                        if (character3.Location != null)
                                        {
                                            double num = CalculateDistance(character3.Location.Xpos, character3.Location.Ypos, invasionStats2.Colony.Xpos, invasionStats2.Colony.Ypos);
                                            if (num < 3000.0)
                                            {
                                                list2.Add(CharacterTraitType.LocalDefenseTactics);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        list2.AddRange(new CharacterTraitType[2]
                                        {
                                CharacterTraitType.PoorStrategist,
                                CharacterTraitType.PoorTactician
                                        });
                                        if (invasionStats2.DefendingEmpire == character3.Empire)
                                        {
                                            list2.Add(CharacterTraitType.PoorGroundDefender);
                                            if (invasionStats2.DestroyedDefendingTroops > 4)
                                            {
                                                list2.Add(CharacterTraitType.Drunk);
                                            }
                                            if (invasionStats2.Colony.Population != null && invasionStats2.Colony.Population.TotalAmount > 100000000)
                                            {
                                                list2.Add(CharacterTraitType.PeaceThroughStrength);
                                            }
                                        }
                                        else
                                        {
                                            list2.Add(CharacterTraitType.PoorGroundAttacker);
                                            if (invasionStats2.DestroyedInvadingTroops > 4)
                                            {
                                                list2.Add(CharacterTraitType.Drunk);
                                            }
                                        }
                                    }
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count > 0)
                                {
                                    CharacterTraitType characterTraitType26 = list3[Rnd.Next(0, list3.Count)];
                                    if (character3.AddTrait(characterTraitType26, starting: false, this))
                                    {
                                        string description25 = string.Format(TextResolver.GetText("Character New Trait Ground Invasion"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType26), invasionStats2.Colony.Name);
                                        character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description25);
                                    }
                                }
                                break;
                            }
                        case CharacterEventType.SpaceBattle:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) || eventData == null || !(eventData is SpaceBattleStats))
                                {
                                    break;
                                }
                                SpaceBattleStats spaceBattleStats = (SpaceBattleStats)eventData;
                                if (spaceBattleStats == null)
                                {
                                    break;
                                }
                                if (spaceBattleStats.DestroyedEnemyShipBaseSize + spaceBattleStats.DestroyedEnemyShipBaseSizeByFighters > spaceBattleStats.DestroyedFriendlyShipBaseSize + spaceBattleStats.DestroyedFriendlyShipBaseSizeByFighters)
                                {
                                    list2.AddRange(new CharacterTraitType[4]
                                    {
                            CharacterTraitType.GoodStrategist,
                            CharacterTraitType.GoodTactician,
                            CharacterTraitType.StrongSpaceAttacker,
                            CharacterTraitType.StrongSpaceDefender
                                    });
                                    if (spaceBattleStats.DestroyedFriendlyShipsCapitalShip > 0 || spaceBattleStats.DestroyedFriendlyShipsCruiser > 0 || spaceBattleStats.DestroyedFriendlyShipsCarrier > 0 || spaceBattleStats.DestroyedFriendlyShipsDestroyer > 0 || spaceBattleStats.DestroyedEnemyShipsTroopTransport > 0 || spaceBattleStats.DestroyedEnemyShipsResupplyShip > 0)
                                    {
                                        list2.Add(CharacterTraitType.Drunk);
                                    }
                                    if (spaceBattleStats.Location != null && spaceBattleStats.NearLocation)
                                    {
                                        if (spaceBattleStats.Location.Empire != null && spaceBattleStats.Location.Empire == character3.Empire)
                                        {
                                            list2.Add(CharacterTraitType.LocalDefenseTactics);
                                        }
                                        else if (spaceBattleStats.Location.BasesAtHabitat != null && spaceBattleStats.Location.BasesAtHabitat.Count > 0)
                                        {
                                            for (int l = 0; l < spaceBattleStats.Location.BasesAtHabitat.Count; l++)
                                            {
                                                BuiltObject builtObject5 = spaceBattleStats.Location.BasesAtHabitat[l];
                                                if (builtObject5 != null && !builtObject5.HasBeenDestroyed && builtObject5.Empire != null && builtObject5.Empire == character3.Empire)
                                                {
                                                    list2.Add(CharacterTraitType.LocalDefenseTactics);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    list2.AddRange(new CharacterTraitType[4]
                                    {
                            CharacterTraitType.PoorStrategist,
                            CharacterTraitType.PoorTactician,
                            CharacterTraitType.PoorSpaceAttacker,
                            CharacterTraitType.PoorSpaceDefender
                                    });
                                    if (spaceBattleStats.DestroyedFriendlyShipsCapitalShip > 0 || spaceBattleStats.DestroyedFriendlyShipsCruiser > 0 || spaceBattleStats.DestroyedFriendlyShipsCarrier > 0 || spaceBattleStats.DestroyedFriendlyShipsDestroyer > 0 || spaceBattleStats.DestroyedFriendlyShipsFrigate > 0 || spaceBattleStats.DestroyedFriendlyShipsEscort > 0 || spaceBattleStats.DestroyedEnemyShipsTroopTransport > 0 || spaceBattleStats.DestroyedEnemyShipsResupplyShip > 0)
                                    {
                                        list2.Add(CharacterTraitType.Drunk);
                                    }
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count > 0)
                                {
                                    CharacterTraitType characterTraitType11 = list3[Rnd.Next(0, list3.Count)];
                                    if (character3.AddTrait(characterTraitType11, starting: false, this))
                                    {
                                        string empty3 = string.Empty;
                                        empty3 = ((spaceBattleStats.Location == null) ? string.Format(TextResolver.GetText("Character New Trait Space Battle"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType11)) : string.Format(TextResolver.GetText("Character New Trait Space Battle With Location"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType11), spaceBattleStats.Location.Name));
                                        character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, empty3);
                                    }
                                }
                                break;
                            }
                        case CharacterEventType.ResearchAdvanceEnergy:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) || eventData == null || !(eventData is ResearchNode))
                                {
                                    break;
                                }
                                ResearchNode researchNode5 = (ResearchNode)eventData;
                                if (researchNode5 == null || researchNode5.Industry != IndustryType.Energy)
                                {
                                    break;
                                }
                                if (researchNode5.Category == ComponentCategoryType.Extractor)
                                {
                                    list2.Add(CharacterTraitType.Expansionist);
                                    list2.Add(CharacterTraitType.LaborOriented);
                                }
                                else if (researchNode5.Category == ComponentCategoryType.Construction)
                                {
                                    list2.Add(CharacterTraitType.LaborOriented);
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count > 0)
                                {
                                    CharacterTraitType characterTraitType31 = list3[Rnd.Next(0, list3.Count)];
                                    if (character3.AddTrait(characterTraitType31, starting: false, this))
                                    {
                                        string description30 = string.Format(TextResolver.GetText("Character New Trait Research Advance"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType31), researchNode5.Name);
                                        character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description30);
                                    }
                                }
                                break;
                            }
                        case CharacterEventType.ResearchAdvanceHighTech:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) || eventData == null || !(eventData is ResearchNode))
                                {
                                    break;
                                }
                                ResearchNode researchNode2 = (ResearchNode)eventData;
                                if (researchNode2 == null || researchNode2.Industry != IndustryType.HighTech)
                                {
                                    break;
                                }
                                if (researchNode2.Abilities != null && researchNode2.Abilities.Count > 0 && (researchNode2.Abilities[0].Type == ResearchAbilityType.ColonizeHabitatType || researchNode2.Abilities[0].Type == ResearchAbilityType.PopulationGrowthRate))
                                {
                                    list2.Add(CharacterTraitType.Expansionist);
                                }
                                else if (researchNode2.Category == ComponentCategoryType.Storage)
                                {
                                    list2.Add(CharacterTraitType.Expansionist);
                                }
                                else if (researchNode2.ResolveComponentType() == ComponentType.HabitationMedicalCenter)
                                {
                                    list2.Add(CharacterTraitType.HealthOriented);
                                }
                                else if (researchNode2.ResolveComponentType() == ComponentType.ComputerCommandCenter)
                                {
                                    list2.Add(CharacterTraitType.GoodStrategist);
                                }
                                else if (researchNode2.ResolveComponentType() == ComponentType.HabitationRecreationCenter)
                                {
                                    list2.Add(CharacterTraitType.Uninhibited);
                                    list2.Add(CharacterTraitType.Addict);
                                    list2.Add(CharacterTraitType.IntelligenceUninhibited);
                                    list2.Add(CharacterTraitType.IntelligenceAddict);
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count > 0)
                                {
                                    CharacterTraitType characterTraitType13 = list3[Rnd.Next(0, list3.Count)];
                                    if (character3.AddTrait(characterTraitType13, starting: false, this))
                                    {
                                        string description12 = string.Format(TextResolver.GetText("Character New Trait Research Advance"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType13), researchNode2.Name);
                                        character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description12);
                                    }
                                }
                                break;
                            }
                        case CharacterEventType.ResearchAdvanceWeapons:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) || eventData == null || !(eventData is ResearchNode))
                                {
                                    break;
                                }
                                ResearchNode researchNode3 = (ResearchNode)eventData;
                                if (researchNode3 == null || researchNode3.Industry != IndustryType.Weapon)
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.PeaceThroughStrength);
                                list2.Add(CharacterTraitType.Isolationist);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count > 0)
                                {
                                    CharacterTraitType characterTraitType16 = list3[Rnd.Next(0, list3.Count)];
                                    if (character3.AddTrait(characterTraitType16, starting: false, this))
                                    {
                                        string description15 = string.Format(TextResolver.GetText("Character New Trait Research Advance"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType16), researchNode3.Name);
                                        character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description15);
                                    }
                                }
                                break;
                            }
                        case CharacterEventType.Subjugated:
                            {
                                if (!flag || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Pacifist);
                                list2.Add(CharacterTraitType.Weak);
                                list2.Add(CharacterTraitType.IntelligenceWeak);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType29 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType29, starting: false, this))
                                {
                                    break;
                                }
                                string text22 = string.Empty;
                                if (eventData != null && eventData is Empire)
                                {
                                    Empire empire3 = (Empire)eventData;
                                    if (empire3 != null)
                                    {
                                        text22 = empire3.Name;
                                    }
                                }
                                string description28 = string.Format(TextResolver.GetText("Character New Trait Subjugated"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType29), text22);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description28);
                                break;
                            }
                        case CharacterEventType.BuildMilitaryShip:
                            {
                                if (!flag3 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Isolationist);
                                list2.Add(CharacterTraitType.Engineer);
                                list2.Add(CharacterTraitType.Organized);
                                list2.Add(CharacterTraitType.Technical);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType3 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType3, starting: false, this))
                                {
                                    break;
                                }
                                string text2 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject2 = (BuiltObject)eventData;
                                    if (builtObject2 != null)
                                    {
                                        text2 = builtObject2.Name;
                                    }
                                }
                                string description3 = string.Format(TextResolver.GetText("Character New Trait Build Military Ship"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType3), text2);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description3);
                                break;
                            }
                        case CharacterEventType.BuildCivilianShip:
                            {
                                if (!flag3 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.LaborOriented);
                                list2.Add(CharacterTraitType.Technical);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType20 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType20, starting: false, this))
                                {
                                    break;
                                }
                                string text15 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject10 = (BuiltObject)eventData;
                                    if (builtObject10 != null)
                                    {
                                        text15 = builtObject10.Name;
                                    }
                                }
                                string description19 = string.Format(TextResolver.GetText("Character New Trait Build Civilian Ship"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType20), text15);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description19);
                                break;
                            }
                        case CharacterEventType.BuildColonyShip:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Expansionist);
                                list2.Add(CharacterTraitType.Organized);
                                list2.Add(CharacterTraitType.Technical);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType5 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType5, starting: false, this))
                                {
                                    break;
                                }
                                string text4 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject3 = (BuiltObject)eventData;
                                    if (builtObject3 != null)
                                    {
                                        text4 = builtObject3.Name;
                                    }
                                }
                                string description5 = string.Format(TextResolver.GetText("Character New Trait Build Colony Ship"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType5), text4);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description5);
                                break;
                            }
                        case CharacterEventType.BuildSpaceport:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject7 = (BuiltObject)eventData;
                                    if (builtObject7 != null && builtObject7.MedicalCapacity > 0)
                                    {
                                        list2.Add(CharacterTraitType.HealthOriented);
                                    }
                                }
                                list2.Add(CharacterTraitType.Engineer);
                                list2.Add(CharacterTraitType.Organized);
                                list2.Add(CharacterTraitType.Technical);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType15 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType15, starting: false, this))
                                {
                                    break;
                                }
                                string text11 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject8 = (BuiltObject)eventData;
                                    if (builtObject8 != null)
                                    {
                                        text11 = builtObject8.Name;
                                    }
                                }
                                string description14 = string.Format(TextResolver.GetText("Character New Trait Build Spaceport"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType15), text11);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description14);
                                break;
                            }
                        case CharacterEventType.BuildMilitaryBase:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Organized);
                                list2.Add(CharacterTraitType.Technical);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType22 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType22, starting: false, this))
                                {
                                    break;
                                }
                                string text17 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject11 = (BuiltObject)eventData;
                                    if (builtObject11 != null)
                                    {
                                        text17 = builtObject11.Name;
                                    }
                                }
                                string description22 = string.Format(TextResolver.GetText("Character New Trait Build Military Base"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType22), text17);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description22);
                                break;
                            }
                        case CharacterEventType.BuildResearchStationEnergy:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Engineer);
                                list2.Add(CharacterTraitType.Organized);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType18 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType18, starting: false, this))
                                {
                                    break;
                                }
                                string text14 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject9 = (BuiltObject)eventData;
                                    if (builtObject9 != null)
                                    {
                                        text14 = builtObject9.Name;
                                    }
                                }
                                string description17 = string.Format(TextResolver.GetText("Character New Trait Build Research Station"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType18), text14);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description17);
                                break;
                            }
                        case CharacterEventType.BuildResearchStationHighTech:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Engineer);
                                list2.Add(CharacterTraitType.Organized);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType27 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType27, starting: false, this))
                                {
                                    break;
                                }
                                string text20 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject12 = (BuiltObject)eventData;
                                    if (builtObject12 != null)
                                    {
                                        text20 = builtObject12.Name;
                                    }
                                }
                                string description26 = string.Format(TextResolver.GetText("Character New Trait Build Research Station"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType27), text20);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description26);
                                break;
                            }
                        case CharacterEventType.BuildResearchStationWeapons:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Engineer);
                                list2.Add(CharacterTraitType.Organized);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType14 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType14, starting: false, this))
                                {
                                    break;
                                }
                                string text10 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject6 = (BuiltObject)eventData;
                                    if (builtObject6 != null)
                                    {
                                        text10 = builtObject6.Name;
                                    }
                                }
                                string description13 = string.Format(TextResolver.GetText("Character New Trait Build Research Station"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType14), text10);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description13);
                                break;
                            }
                        case CharacterEventType.BuildResortBase:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Tolerant);
                                list2.Add(CharacterTraitType.IntelligenceTolerant);
                                list2.Add(CharacterTraitType.Organized);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType6 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType6, starting: false, this))
                                {
                                    break;
                                }
                                string text5 = string.Empty;
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject4 = (BuiltObject)eventData;
                                    if (builtObject4 != null)
                                    {
                                        text5 = builtObject4.Name;
                                    }
                                }
                                string description6 = string.Format(TextResolver.GetText("Character New Trait Build Resort Base"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType6), text5);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description6);
                                break;
                            }
                        case CharacterEventType.BuildFacility:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Organized);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType24 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType24, starting: false, this))
                                {
                                    break;
                                }
                                string text18 = string.Empty;
                                if (eventData != null && eventData is PlanetaryFacility)
                                {
                                    PlanetaryFacility planetaryFacility3 = (PlanetaryFacility)eventData;
                                    if (planetaryFacility3 != null)
                                    {
                                        text18 = planetaryFacility3.Name;
                                    }
                                }
                                string description24 = string.Format(TextResolver.GetText("Character New Trait Build Facility"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType24), text18);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description24);
                                break;
                            }
                        case CharacterEventType.BuildWonder:
                            {
                                if (!flag || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                if (eventData != null && eventData is PlanetaryFacility)
                                {
                                    PlanetaryFacility planetaryFacility = (PlanetaryFacility)eventData;
                                    if (planetaryFacility != null && planetaryFacility.Type == PlanetaryFacilityType.Wonder && (planetaryFacility.WonderType == WonderType.ColonyPopulationGrowth || planetaryFacility.WonderType == WonderType.EmpirePopulationGrowth))
                                    {
                                        list2.Add(CharacterTraitType.HealthOriented);
                                    }
                                }
                                list2.Add(CharacterTraitType.Organized);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType4 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType4, starting: false, this))
                                {
                                    break;
                                }
                                string text3 = string.Empty;
                                if (eventData != null && eventData is PlanetaryFacility)
                                {
                                    PlanetaryFacility planetaryFacility2 = (PlanetaryFacility)eventData;
                                    if (planetaryFacility2 != null)
                                    {
                                        text3 = planetaryFacility2.Name;
                                    }
                                }
                                string description4 = string.Format(TextResolver.GetText("Character New Trait Build Wonder"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType4), text3);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description4);
                                break;
                            }
                        case CharacterEventType.TreatySigned:
                            {
                                if (!flag || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData) || eventData == null || !(eventData is DiplomaticRelation))
                                {
                                    break;
                                }
                                DiplomaticRelation diplomaticRelation = (DiplomaticRelation)eventData;
                                if (diplomaticRelation == null || diplomaticRelation.OtherEmpire == null)
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Diplomat);
                                list2.Add(CharacterTraitType.FreeTrader);
                                list2.Add(CharacterTraitType.Measured);
                                list2.Add(CharacterTraitType.IntelligenceMeasured);
                                if (diplomaticRelation.OtherEmpire.DominantRace != null && character3.Empire.DominantRace != null && diplomaticRelation.OtherEmpire.DominantRace.FamilyId != character3.Empire.DominantRace.FamilyId)
                                {
                                    list2.Add(CharacterTraitType.Tolerant);
                                    list2.Add(CharacterTraitType.IntelligenceTolerant);
                                }
                                if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact)
                                {
                                    list2.Add(CharacterTraitType.EloquentSpeaker);
                                    list2.Add(CharacterTraitType.IntelligenceEloquentSpeaker);
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count > 0)
                                {
                                    CharacterTraitType characterTraitType19 = list3[Rnd.Next(0, list3.Count)];
                                    if (character3.AddTrait(characterTraitType19, starting: false, this))
                                    {
                                        string description18 = string.Format(TextResolver.GetText("Character New Trait Treaty Signed"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType19), diplomaticRelation.OtherEmpire.Name);
                                        character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description18);
                                    }
                                }
                                break;
                            }
                        case CharacterEventType.TreatyBroken:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Protectionist);
                                list2.Add(CharacterTraitType.PoorSpeaker);
                                list2.Add(CharacterTraitType.IntelligencePoorSpeaker);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType8 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType8, starting: false, this))
                                {
                                    break;
                                }
                                string text7 = string.Empty;
                                if (eventData != null && eventData is Empire)
                                {
                                    Empire empire = (Empire)eventData;
                                    if (empire != null)
                                    {
                                        text7 = empire.Name;
                                    }
                                }
                                string description8 = string.Format(TextResolver.GetText("Character New Trait Treaty Broken"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType8), text7);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description8);
                                break;
                            }
                        case CharacterEventType.AmbassadorAssignedToEmpire:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.Linguist);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType30 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType30, starting: false, this))
                                {
                                    break;
                                }
                                string text23 = string.Empty;
                                if (eventData != null && eventData is Empire)
                                {
                                    Empire empire4 = (Empire)eventData;
                                    if (empire4 != null)
                                    {
                                        text23 = empire4.Name;
                                    }
                                }
                                string description29 = string.Format(TextResolver.GetText("Character New Trait Ambassador Assigned To Empire"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType30), text23);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description29);
                                break;
                            }
                        case CharacterEventType.TroopComplete:
                            {
                                if (!flag3 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.GoodRecruiter);
                                list2.Add(CharacterTraitType.GoodGroundLogistician);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType17 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType17, starting: false, this))
                                {
                                    break;
                                }
                                string text12 = string.Empty;
                                string text13 = string.Empty;
                                if (eventData != null && eventData is Troop)
                                {
                                    Troop troop = (Troop)eventData;
                                    if (troop != null)
                                    {
                                        text12 = troop.Name;
                                        if (troop.Colony != null)
                                        {
                                            text13 = troop.Colony.Name;
                                        }
                                        else if (troop.BuiltObject != null)
                                        {
                                            text13 = troop.BuiltObject.Name;
                                        }
                                    }
                                }
                                string description16 = string.Format(TextResolver.GetText("Character New Trait Troop Complete"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType17), text13, text12);
                                character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description16);
                                break;
                            }
                        case CharacterEventType.IntelligenceMissionFailEspionage:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.DoubleAgent);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType7 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType7, starting: false, this) || eventData == null || !(eventData is IntelligenceMission))
                                {
                                    break;
                                }
                                IntelligenceMission intelligenceMission2 = (IntelligenceMission)eventData;
                                if (intelligenceMission2 != null)
                                {
                                    string text6 = string.Empty;
                                    if (intelligenceMission2.Agent != null)
                                    {
                                        text6 = intelligenceMission2.Agent.Name;
                                    }
                                    string description7 = string.Format(TextResolver.GetText("Character New Trait Intelligence Mission Failure"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType7), ResolveDescription(intelligenceMission2.Type), text6);
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description7);
                                }
                                break;
                            }
                        case CharacterEventType.IntelligenceMissionFailSabotage:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.DoubleAgent);
                                list2.Add(CharacterTraitType.IntelligenceAddict);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType28 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType28, starting: false, this) || eventData == null || !(eventData is IntelligenceMission))
                                {
                                    break;
                                }
                                IntelligenceMission intelligenceMission5 = (IntelligenceMission)eventData;
                                if (intelligenceMission5 != null)
                                {
                                    string text21 = string.Empty;
                                    if (intelligenceMission5.Agent != null)
                                    {
                                        text21 = intelligenceMission5.Agent.Name;
                                    }
                                    string description27 = string.Format(TextResolver.GetText("Character New Trait Intelligence Mission Failure"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType28), ResolveDescription(intelligenceMission5.Type), text21);
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description27);
                                }
                                break;
                            }
                        case CharacterEventType.IntelligenceMissionInterceptEnemy:
                            if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                            {
                                break;
                            }
                            list2.Add(CharacterTraitType.IntelligenceXenophobic);
                            list3 = IntersectTraitLists(traits, list2);
                            if (list3.Count > 0)
                            {
                                CharacterTraitType characterTraitType23 = list3[Rnd.Next(0, list3.Count)];
                                if (character3.AddTrait(characterTraitType23, starting: false, this))
                                {
                                    string description23 = string.Format(TextResolver.GetText("Character New Trait Intercept Foreign Agent"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType23));
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description23);
                                }
                            }
                            break;
                        case CharacterEventType.IntelligenceMissionSucceedEspionage:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.IntelligenceMeasured);
                                if (eventData != null && eventData is IntelligenceMission)
                                {
                                    IntelligenceMission intelligenceMission3 = (IntelligenceMission)eventData;
                                    if (intelligenceMission3 != null && intelligenceMission3.Type == IntelligenceMissionType.StealTechData)
                                    {
                                        list2.Add(CharacterTraitType.ForeignSpy);
                                    }
                                }
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType10 = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType10, starting: false, this) || eventData == null || !(eventData is IntelligenceMission))
                                {
                                    break;
                                }
                                IntelligenceMission intelligenceMission4 = (IntelligenceMission)eventData;
                                if (intelligenceMission4 != null)
                                {
                                    string text8 = string.Empty;
                                    if (intelligenceMission4.Agent != null)
                                    {
                                        text8 = intelligenceMission4.Agent.Name;
                                    }
                                    string description9 = string.Format(TextResolver.GetText("Character New Trait Intelligence Mission Success"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType10), ResolveDescription(intelligenceMission4.Type), text8);
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description9);
                                }
                                break;
                            }
                        case CharacterEventType.IntelligenceMissionSucceedSabotage:
                            {
                                if (!flag2 || DoCharacterEventChanceNewSkill(character3, list, validSkillsForRole, eventType, eventData))
                                {
                                    break;
                                }
                                list2.Add(CharacterTraitType.IntelligenceSober);
                                list2.Add(CharacterTraitType.IntelligenceCourageous);
                                list3 = IntersectTraitLists(traits, list2);
                                if (list3.Count <= 0)
                                {
                                    break;
                                }
                                CharacterTraitType characterTraitType = list3[Rnd.Next(0, list3.Count)];
                                if (!character3.AddTrait(characterTraitType, starting: true, this) || eventData == null || !(eventData is IntelligenceMission))
                                {
                                    break;
                                }
                                IntelligenceMission intelligenceMission = (IntelligenceMission)eventData;
                                if (intelligenceMission != null)
                                {
                                    string text = string.Empty;
                                    if (intelligenceMission.Agent != null)
                                    {
                                        text = intelligenceMission.Agent.Name;
                                    }
                                    string description = string.Format(TextResolver.GetText("Character New Trait Intelligence Mission Success"), ResolveDescription(character3.Role), character3.Name, ResolveDescription(characterTraitType), ResolveDescription(intelligenceMission.Type), text);
                                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description);
                                }
                                break;
                            }
                    }
                }
                if (!character3.BonusesKnown)
                {
                    switch (character3.Role)
                    {
                        case CharacterRole.ShipCaptain:
                            switch (eventType)
                            {
                                case CharacterEventType.SpaceBattle:
                                case CharacterEventType.Boarding:
                                case CharacterEventType.Raid:
                                    character3.BonusesKnown = true;
                                    break;
                            }
                            break;
                        case CharacterRole.FleetAdmiral:
                            switch (eventType)
                            {
                                case CharacterEventType.SpaceBattle:
                                case CharacterEventType.Boarding:
                                case CharacterEventType.Raid:
                                    character3.BonusesKnown = true;
                                    break;
                            }
                            break;
                        case CharacterRole.IntelligenceAgent:
                            switch (eventType)
                            {
                                case CharacterEventType.IntelligenceMissionSucceedEspionage:
                                case CharacterEventType.IntelligenceMissionSucceedSabotage:
                                case CharacterEventType.IntelligenceMissionFailEspionage:
                                case CharacterEventType.IntelligenceMissionFailSabotage:
                                case CharacterEventType.IntelligenceMissionInterceptEnemy:
                                    character3.BonusesKnown = true;
                                    break;
                            }
                            break;
                        case CharacterRole.TroopGeneral:
                            switch (eventType)
                            {
                                case CharacterEventType.SpaceBattle:
                                case CharacterEventType.GroundInvasion:
                                case CharacterEventType.Raid:
                                    character3.BonusesKnown = true;
                                    break;
                            }
                            break;
                    }
                }
                if (!character3.BonusesKnown || character3.Skills == null || character3.Skills.Count <= 0)
                {
                    continue;
                }
                List<CharacterSkillType> list4 = new List<CharacterSkillType>();
                List<float> list5 = new List<float>();
                for (int m = 0; m < character3.Skills.Count; m++)
                {
                    CharacterSkill characterSkill = character3.Skills[m];
                    for (int n = 0; n < list.Count; n++)
                    {
                        CharacterSkillType characterSkillType = list[n];
                        if (characterSkillType == characterSkill.Type)
                        {
                            list4.Add(characterSkillType);
                            list5.Add(relativeImportances[n]);
                        }
                    }
                }
                int num2 = -1;
                if (list4.Count > 0)
                {
                    num2 = Rnd.Next(0, list4.Count);
                }
                if (num2 < 0)
                {
                    continue;
                }
                CharacterSkillType characterSkillType2 = list4[num2];
                float num3 = list5[num2];
                if (characterSkillType2 == CharacterSkillType.Undefined)
                {
                    continue;
                }
                float num4 = 0f;
                switch (eventType)
                {
                    case CharacterEventType.Boarding:
                        num4 = 15f;
                        break;
                    case CharacterEventType.Raid:
                        num4 = 15f;
                        break;
                    case CharacterEventType.SmugglingSuccess:
                        num4 = 20f;
                        break;
                    case CharacterEventType.SmugglingDetection:
                        num4 = -40f;
                        break;
                    case CharacterEventType.CriticalResearchFailure:
                        num4 = -35f;
                        break;
                    case CharacterEventType.CriticalResearchSuccess:
                        num4 = 50f;
                        break;
                    case CharacterEventType.Subjugated:
                        num4 = -30f;
                        break;
                    case CharacterEventType.TreatyBroken:
                        num4 = -15f;
                        break;
                    case CharacterEventType.BuildCivilianShip:
                        num4 = 5f;
                        break;
                    case CharacterEventType.BuildColonyShip:
                        num4 = 40f;
                        break;
                    case CharacterEventType.BuildFacility:
                        num4 = 40f;
                        break;
                    case CharacterEventType.BuildWonder:
                        num4 = 100f;
                        break;
                    case CharacterEventType.BuildMilitaryBase:
                        num4 = 40f;
                        break;
                    case CharacterEventType.BuildMilitaryShip:
                        num4 = 9f;
                        break;
                    case CharacterEventType.BuildMiningStation:
                        num4 = 20f;
                        break;
                    case CharacterEventType.BuildOtherBase:
                        num4 = 20f;
                        break;
                    case CharacterEventType.BuildResearchStationEnergy:
                        num4 = 40f;
                        break;
                    case CharacterEventType.BuildResearchStationHighTech:
                        num4 = 40f;
                        break;
                    case CharacterEventType.BuildResearchStationWeapons:
                        num4 = 40f;
                        break;
                    case CharacterEventType.BuildResortBase:
                        num4 = 40f;
                        break;
                    case CharacterEventType.BuildSpaceport:
                        num4 = 40f;
                        break;
                    case CharacterEventType.CashNegative:
                        num4 = -8f;
                        break;
                    case CharacterEventType.CashPositive:
                        num4 = 1f;
                        break;
                    case CharacterEventType.ColonyDevelopmentIncrease:
                        num4 = 8f;
                        break;
                    case CharacterEventType.ColonyDevelopmentDecrease:
                        num4 = -8f;
                        break;
                    case CharacterEventType.GroundInvasion:
                        if (eventData != null && eventData is InvasionStats)
                        {
                            InvasionStats invasionStats3 = (InvasionStats)eventData;
                            if (invasionStats3 != null && invasionStats3.Colony != null)
                            {
                                num4 = ((invasionStats3.Colony.Empire != character3.Empire) ? (-50f) : 50f);
                            }
                        }
                        break;
                    case CharacterEventType.HyperjumpExit:
                        if (eventData != null && eventData is BuiltObject)
                        {
                            BuiltObject builtObject13 = (BuiltObject)eventData;
                            if (builtObject13 != null && builtObject13.ShipGroup != null)
                            {
                                num4 = builtObject13.LastHyperjumpDistance / 5000000f;
                            }
                        }
                        break;
                    case CharacterEventType.IntelligenceAgentOursCaptured:
                        num4 = -15f;
                        break;
                    case CharacterEventType.IntelligenceAgentRecruited:
                        num4 = 20f;
                        break;
                    case CharacterEventType.IntelligenceMissionFailEspionage:
                        num4 = -15f;
                        break;
                    case CharacterEventType.IntelligenceMissionFailSabotage:
                        num4 = -15f;
                        break;
                    case CharacterEventType.IntelligenceMissionInterceptEnemy:
                        num4 = 20f;
                        break;
                    case CharacterEventType.IntelligenceMissionSucceedEspionage:
                        num4 = 40f;
                        break;
                    case CharacterEventType.IntelligenceMissionSucceedSabotage:
                        num4 = 40f;
                        break;
                    case CharacterEventType.ResearchAdvanceEnergy:
                        num4 = 25f;
                        break;
                    case CharacterEventType.ResearchAdvanceHighTech:
                        num4 = 25f;
                        break;
                    case CharacterEventType.ResearchAdvanceWeapons:
                        num4 = 25f;
                        break;
                    case CharacterEventType.SpaceBattle:
                        {
                            if (eventData == null || !(eventData is SpaceBattleStats))
                            {
                                break;
                            }
                            SpaceBattleStats spaceBattleStats2 = (SpaceBattleStats)eventData;
                            if (spaceBattleStats2 != null)
                            {
                                double num5 = 1.0 + (double)(spaceBattleStats2.DestroyedEnemyShipBaseSize + spaceBattleStats2.DestroyedEnemyShipBaseSizeByFighters);
                                double num6 = 1.0 + (double)(spaceBattleStats2.DestroyedFriendlyShipBaseSize + spaceBattleStats2.DestroyedFriendlyShipBaseSizeByFighters);
                                double num7 = num5 / num6;
                                switch (characterSkillType2)
                                {
                                    case CharacterSkillType.Countermeasures:
                                        num4 = Math.Min(50f, 20f * (spaceBattleStats2.ShieldsDamageAbsorbed / (float)num5));
                                        break;
                                    case CharacterSkillType.DamageControl:
                                        num4 = (float)spaceBattleStats2.DamageToUs / 100f;
                                        break;
                                    case CharacterSkillType.Fighters:
                                        num4 = (float)spaceBattleStats2.DestroyedEnemyFighters / 5f * ((1f + (float)spaceBattleStats2.DestroyedEnemyFighters) / (1f + (float)spaceBattleStats2.DestroyedFriendlyFighters));
                                        break;
                                    case CharacterSkillType.RepairBonus:
                                        num4 = (float)spaceBattleStats2.DamageRepaired / 5f;
                                        break;
                                    case CharacterSkillType.ShieldRechargeRate:
                                        num4 = spaceBattleStats2.ShieldsDamageAbsorbed / 200f;
                                        break;
                                    case CharacterSkillType.ShipManeuvering:
                                        {
                                            float num8 = 1 + spaceBattleStats2.DestroyedEnemyShipsFrigate + spaceBattleStats2.DestroyedEnemyShipsEscort;
                                            float num9 = 1 + spaceBattleStats2.DestroyedFriendlyShipsFrigate + spaceBattleStats2.DestroyedFriendlyShipsEscort;
                                            float num10 = Math.Max(0.2f, Math.Min(5f, num8 / num9));
                                            num4 = Math.Min(50f, 20f * num10);
                                            break;
                                        }
                                    case CharacterSkillType.Targeting:
                                        {
                                            float val2 = (float)spaceBattleStats2.WeaponsHits / (float)spaceBattleStats2.WeaponsMisses;
                                            val2 = Math.Max(0.1f, Math.Min(1f, val2));
                                            float val3 = (1f + spaceBattleStats2.WeaponsDamageToEnemy) / (1f + spaceBattleStats2.ShieldsDamageAbsorbed);
                                            val3 = Math.Max(0.2f, Math.Min(5f, val3));
                                            num4 = Math.Min(50f, 20f * val2 * val3);
                                            break;
                                        }
                                    case CharacterSkillType.WeaponsDamage:
                                        num4 = Math.Min(50f, spaceBattleStats2.WeaponsDamageToEnemy / 100f);
                                        break;
                                    case CharacterSkillType.WeaponsRange:
                                        {
                                            float val = (float)spaceBattleStats2.WeaponsHitsLongRange / (float)spaceBattleStats2.WeaponsHits;
                                            val = Math.Max(0.1f, Math.Min(1f, val));
                                            num4 = Math.Min(100f, spaceBattleStats2.WeaponsDamageToEnemy / 20f) * val;
                                            num4 = Math.Min(50f, num4);
                                            break;
                                        }
                                    default:
                                        num4 = ((!(num7 < 1.0)) ? ((float)(num5 / 100.0)) : (-1f * (float)(num6 / 100.0)));
                                        break;
                                }
                                num4 = Math.Max(-50f, Math.Min(50f, num4));
                            }
                            break;
                        }
                    case CharacterEventType.TourismIncome:
                        num4 = 3f;
                        break;
                    case CharacterEventType.TradeIncome:
                        num4 = 2f;
                        break;
                    case CharacterEventType.TreatySigned:
                        if (eventData != null && eventData is DiplomaticRelationType)
                        {
                            switch ((DiplomaticRelationType)eventData)
                            {
                                case DiplomaticRelationType.FreeTradeAgreement:
                                    num4 = 30f;
                                    break;
                                case DiplomaticRelationType.MutualDefensePact:
                                case DiplomaticRelationType.Protectorate:
                                    num4 = 60f;
                                    break;
                            }
                        }
                        break;
                    case CharacterEventType.TroopComplete:
                        num4 = 7f;
                        break;
                    case CharacterEventType.WarEnded:
                        num4 = ((characterSkillType2 != CharacterSkillType.WarWeariness) ? 10f : 40f);
                        break;
                    case CharacterEventType.WarStarted:
                        num4 = ((characterSkillType2 != CharacterSkillType.WarWeariness) ? 10f : (-15f));
                        break;
                }
                num4 *= (float)(0.5 + Rnd.NextDouble());
                num4 *= num3;
                if (float.IsNaN(num4))
                {
                    num4 = 0f;
                }
                num4 /= 100f;
                CharacterSkill skill = character3.GetSkill(characterSkillType2);
                if (skill != null && character3.IncrementSkillProgress(characterSkillType2, num4, this) && character3.Empire != null)
                {
                    string text24 = string.Empty;
                    if (character3.Location != null)
                    {
                        text24 = character3.Location.Name;
                    }
                    int skillLevel = character3.GetSkillLevel(skill.Type);
                    string description31 = string.Format(TextResolver.GetText("Character Skill Increase Description"), ResolveDescription(character3.Role), character3.Name, text24, ResolveDescription(characterSkillType2), skillLevel.ToString("+0;-0"));
                    character3.Empire.SendMessageToEmpire(character3.Empire, EmpireMessageType.CharacterSkillTraitChange, character3, description31);
                }
            }
        }

        public bool DoCharacterEventChanceNewSkill(Character character, List<CharacterSkillType> skills, List<CharacterSkillType> validSkillsForRole, CharacterEventType eventType, object eventData)
        {
            if (Rnd.Next(0, 5) == 1)
            {
                for (int i = 0; i < skills.Count; i++)
                {
                    CharacterSkillType characterSkillType = skills[i];
                    if (characterSkillType == CharacterSkillType.Undefined || !validSkillsForRole.Contains(characterSkillType) || character.Skills.GetSkillByType(characterSkillType) != null || !character.AddSkill(characterSkillType, Rnd.Next(3, 7), this))
                    {
                        continue;
                    }
                    CharacterSkill skill = character.GetSkill(characterSkillType);
                    if (skill != null)
                    {
                        string description = string.Empty;
                        switch (eventType)
                        {
                            case CharacterEventType.Boarding:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject4 = (BuiltObject)eventData;
                                    string text6 = string.Empty;
                                    if (builtObject4 != null)
                                    {
                                        text6 = builtObject4.Name;
                                    }
                                    description = string.Format(TextResolver.GetText("Character New Skill Boarding"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), text6);
                                }
                                break;
                            case CharacterEventType.Raid:
                                if (eventData != null)
                                {
                                    string text9 = string.Empty;
                                    if (eventData is BuiltObject)
                                    {
                                        BuiltObject builtObject6 = (BuiltObject)eventData;
                                        text9 = builtObject6.Name;
                                    }
                                    else if (eventData is Habitat)
                                    {
                                        Habitat habitat = (Habitat)eventData;
                                        text9 = habitat.Name;
                                    }
                                    description = string.Format(TextResolver.GetText("Character New Skill Raid"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), text9);
                                }
                                break;
                            case CharacterEventType.SmugglingSuccess:
                                if (eventData != null && eventData is StellarObject)
                                {
                                    StellarObject stellarObject2 = (StellarObject)eventData;
                                    string text7 = string.Empty;
                                    if (stellarObject2 != null)
                                    {
                                        text7 = stellarObject2.Name;
                                    }
                                    description = string.Format(TextResolver.GetText("Character New Skill Smuggling Success"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), text7);
                                }
                                break;
                            case CharacterEventType.SmugglingDetection:
                                if (eventData != null && eventData is StellarObject)
                                {
                                    StellarObject stellarObject = (StellarObject)eventData;
                                    string text4 = string.Empty;
                                    if (stellarObject != null)
                                    {
                                        text4 = stellarObject.Name;
                                    }
                                    description = string.Format(TextResolver.GetText("Character New Skill Smuggling Detection"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), text4);
                                }
                                break;
                            case CharacterEventType.CriticalResearchFailure:
                                if (eventData != null && eventData is ResearchNode)
                                {
                                    ResearchNode researchNode4 = (ResearchNode)eventData;
                                    string text10 = string.Empty;
                                    if (researchNode4 != null)
                                    {
                                        text10 = researchNode4.Name;
                                    }
                                    description = string.Format(TextResolver.GetText("Character New Skill Critical Research Failure"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), text10);
                                }
                                break;
                            case CharacterEventType.CriticalResearchSuccess:
                                if (eventData != null && eventData is ResearchNode)
                                {
                                    ResearchNode researchNode = (ResearchNode)eventData;
                                    string text2 = string.Empty;
                                    if (researchNode != null)
                                    {
                                        text2 = researchNode.Name;
                                    }
                                    description = string.Format(TextResolver.GetText("Character New Skill Critical Research Success"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), text2);
                                }
                                break;
                            case CharacterEventType.TargetOfFailedAssassination:
                                description = string.Format(TextResolver.GetText("Character New Skill Failed Assassination"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type));
                                break;
                            case CharacterEventType.WarStarted:
                                description = string.Format(TextResolver.GetText("Character New Skill War Started"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type));
                                break;
                            case CharacterEventType.WarEnded:
                                description = string.Format(TextResolver.GetText("Character New Skill War Ended"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type));
                                break;
                            case CharacterEventType.GroundInvasion:
                                if (eventData != null && eventData is InvasionStats)
                                {
                                    InvasionStats invasionStats = (InvasionStats)eventData;
                                    if (invasionStats != null && invasionStats.Colony != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Ground Invasion"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), invasionStats.Colony.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.SpaceBattle:
                                if (eventData != null && eventData is SpaceBattleStats)
                                {
                                    SpaceBattleStats spaceBattleStats = (SpaceBattleStats)eventData;
                                    if (spaceBattleStats != null)
                                    {
                                        description = ((spaceBattleStats.Location == null) ? string.Format(TextResolver.GetText("Character New Skill Space Battle"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type)) : string.Format(TextResolver.GetText("Character New Skill Space Battle With Location"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), spaceBattleStats.Location.Name));
                                    }
                                }
                                break;
                            case CharacterEventType.ResearchAdvanceEnergy:
                                if (eventData != null && eventData is ResearchNode)
                                {
                                    ResearchNode researchNode2 = (ResearchNode)eventData;
                                    if (researchNode2 != null && researchNode2.Industry == IndustryType.Energy)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Research Advance"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), researchNode2.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.ResearchAdvanceHighTech:
                                if (eventData != null && eventData is ResearchNode)
                                {
                                    ResearchNode researchNode5 = (ResearchNode)eventData;
                                    if (researchNode5 != null && researchNode5.Industry == IndustryType.HighTech)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Research Advance"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), researchNode5.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.ResearchAdvanceWeapons:
                                if (eventData != null && eventData is ResearchNode)
                                {
                                    ResearchNode researchNode3 = (ResearchNode)eventData;
                                    if (researchNode3 != null && researchNode3.Industry == IndustryType.Weapon)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Research Advance"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), researchNode3.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.Subjugated:
                                if (eventData != null && eventData is Empire)
                                {
                                    Empire empire2 = (Empire)eventData;
                                    if (empire2 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Subjugated"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), empire2.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildMilitaryShip:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject2 = (BuiltObject)eventData;
                                    if (builtObject2 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Military Ship"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject2.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildCivilianShip:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject9 = (BuiltObject)eventData;
                                    if (builtObject9 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Civilian Ship"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject9.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildColonyShip:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject8 = (BuiltObject)eventData;
                                    if (builtObject8 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Colony Ship"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject8.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildSpaceport:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject5 = (BuiltObject)eventData;
                                    if (builtObject5 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Spaceport"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject5.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildMilitaryBase:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject3 = (BuiltObject)eventData;
                                    if (builtObject3 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Military Base"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject3.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildResearchStationEnergy:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)eventData;
                                    if (builtObject != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Research Station"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildResearchStationHighTech:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject11 = (BuiltObject)eventData;
                                    if (builtObject11 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Research Station"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject11.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildResearchStationWeapons:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject10 = (BuiltObject)eventData;
                                    if (builtObject10 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Research Station"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject10.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildResortBase:
                                if (eventData != null && eventData is BuiltObject)
                                {
                                    BuiltObject builtObject7 = (BuiltObject)eventData;
                                    if (builtObject7 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Resort Base"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), builtObject7.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildFacility:
                                if (eventData != null && eventData is PlanetaryFacility)
                                {
                                    PlanetaryFacility planetaryFacility2 = (PlanetaryFacility)eventData;
                                    if (planetaryFacility2 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Facility"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), planetaryFacility2.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.BuildWonder:
                                if (eventData != null && eventData is PlanetaryFacility)
                                {
                                    PlanetaryFacility planetaryFacility = (PlanetaryFacility)eventData;
                                    if (planetaryFacility != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Build Wonder"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), planetaryFacility.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.TreatySigned:
                                if (eventData != null && eventData is DiplomaticRelation)
                                {
                                    DiplomaticRelation diplomaticRelation = (DiplomaticRelation)eventData;
                                    if (diplomaticRelation != null && diplomaticRelation.OtherEmpire != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Treaty Signed"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), diplomaticRelation.OtherEmpire.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.TreatyBroken:
                                if (eventData != null && eventData is Empire)
                                {
                                    Empire empire = (Empire)eventData;
                                    if (empire != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Treaty Broken"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), empire.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.AmbassadorAssignedToEmpire:
                                if (eventData != null && eventData is Empire)
                                {
                                    Empire empire3 = (Empire)eventData;
                                    if (empire3 != null)
                                    {
                                        description = string.Format(TextResolver.GetText("Character New Skill Ambassador Assigned To Empire"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), empire3.Name);
                                    }
                                }
                                break;
                            case CharacterEventType.TroopComplete:
                                {
                                    if (eventData == null || !(eventData is Troop))
                                    {
                                        break;
                                    }
                                    Troop troop = (Troop)eventData;
                                    if (troop != null)
                                    {
                                        string text11 = string.Empty;
                                        if (troop.Colony != null)
                                        {
                                            text11 = troop.Colony.Name;
                                        }
                                        else if (troop.BuiltObject != null)
                                        {
                                            text11 = troop.BuiltObject.Name;
                                        }
                                        description = string.Format(TextResolver.GetText("Character New Skill Troop Complete"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), text11, troop.Name);
                                    }
                                    break;
                                }
                            case CharacterEventType.IntelligenceMissionFailEspionage:
                                {
                                    if (eventData == null || !(eventData is IntelligenceMission))
                                    {
                                        break;
                                    }
                                    IntelligenceMission intelligenceMission4 = (IntelligenceMission)eventData;
                                    if (intelligenceMission4 != null)
                                    {
                                        string text8 = string.Empty;
                                        if (intelligenceMission4.Agent != null)
                                        {
                                            text8 = intelligenceMission4.Agent.Name;
                                        }
                                        description = string.Format(TextResolver.GetText("Character New Skill Intelligence Mission Failure"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), ResolveDescription(intelligenceMission4.Type), text8);
                                    }
                                    break;
                                }
                            case CharacterEventType.IntelligenceMissionFailSabotage:
                                {
                                    if (eventData == null || !(eventData is IntelligenceMission))
                                    {
                                        break;
                                    }
                                    IntelligenceMission intelligenceMission3 = (IntelligenceMission)eventData;
                                    if (intelligenceMission3 != null)
                                    {
                                        string text5 = string.Empty;
                                        if (intelligenceMission3.Agent != null)
                                        {
                                            text5 = intelligenceMission3.Agent.Name;
                                        }
                                        description = string.Format(TextResolver.GetText("Character New Skill Intelligence Mission Failure"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), ResolveDescription(intelligenceMission3.Type), text5);
                                    }
                                    break;
                                }
                            case CharacterEventType.IntelligenceMissionInterceptEnemy:
                                description = string.Format(TextResolver.GetText("Character New Skill Intercept Foreign Agent"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type));
                                break;
                            case CharacterEventType.IntelligenceMissionSucceedEspionage:
                                {
                                    if (eventData == null || !(eventData is IntelligenceMission))
                                    {
                                        break;
                                    }
                                    IntelligenceMission intelligenceMission2 = (IntelligenceMission)eventData;
                                    if (intelligenceMission2 != null)
                                    {
                                        string text3 = string.Empty;
                                        if (intelligenceMission2.Agent != null)
                                        {
                                            text3 = intelligenceMission2.Agent.Name;
                                        }
                                        description = string.Format(TextResolver.GetText("Character New Skill Intelligence Mission Success"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), ResolveDescription(intelligenceMission2.Type), text3);
                                    }
                                    break;
                                }
                            case CharacterEventType.IntelligenceMissionSucceedSabotage:
                                {
                                    if (eventData == null || !(eventData is IntelligenceMission))
                                    {
                                        break;
                                    }
                                    IntelligenceMission intelligenceMission = (IntelligenceMission)eventData;
                                    if (intelligenceMission != null)
                                    {
                                        string text = string.Empty;
                                        if (intelligenceMission.Agent != null)
                                        {
                                            text = intelligenceMission.Agent.Name;
                                        }
                                        description = string.Format(TextResolver.GetText("Character New Skill Intelligence Mission Success"), ResolveDescription(character.Role), character.Name, ResolveDescription(skill.Type), ResolveDescription(intelligenceMission.Type), text);
                                    }
                                    break;
                                }
                        }
                        character.Empire.SendMessageToEmpire(character.Empire, EmpireMessageType.CharacterSkillTraitChange, character, description);
                    }
                    return true;
                }
            }
            return false;
        }

        public static string ResolveDescription(CharacterEventType eventType)
        {
            string result = string.Empty;
            switch (eventType)
            {
                case CharacterEventType.BuildMilitaryShip:
                case CharacterEventType.BuildCivilianShip:
                case CharacterEventType.BuildColonyShip:
                    result = TextResolver.GetText("Character Event Ship Built");
                    break;
                case CharacterEventType.BuildFacility:
                    result = TextResolver.GetText("Character Event Facility Built");
                    break;
                case CharacterEventType.BuildMilitaryBase:
                case CharacterEventType.BuildResearchStationWeapons:
                case CharacterEventType.BuildResearchStationEnergy:
                case CharacterEventType.BuildResearchStationHighTech:
                case CharacterEventType.BuildMiningStation:
                case CharacterEventType.BuildResortBase:
                case CharacterEventType.BuildOtherBase:
                    result = TextResolver.GetText("Character Event Base Built");
                    break;
                case CharacterEventType.BuildSpaceport:
                    result = TextResolver.GetText("Character Event Spaceport Built");
                    break;
                case CharacterEventType.BuildWonder:
                    result = TextResolver.GetText("Character Event Wonder Built");
                    break;
                case CharacterEventType.CashNegative:
                    result = TextResolver.GetText("Character Event Cash Negative");
                    break;
                case CharacterEventType.CashPositive:
                    result = TextResolver.GetText("Character Event Cash Positive");
                    break;
                case CharacterEventType.GroundInvasion:
                    result = TextResolver.GetText("Character Event Ground Invasion");
                    break;
                case CharacterEventType.IntelligenceAgentOursCaptured:
                    result = TextResolver.GetText("Character Event Intelligence Agent Captured");
                    break;
                case CharacterEventType.IntelligenceAgentRecruited:
                    result = TextResolver.GetText("Character Event Intelligence Agent Recruited");
                    break;
                case CharacterEventType.IntelligenceMissionFailEspionage:
                case CharacterEventType.IntelligenceMissionFailSabotage:
                    result = TextResolver.GetText("Character Event Intelligence Mission Failed");
                    break;
                case CharacterEventType.IntelligenceMissionInterceptEnemy:
                    result = TextResolver.GetText("Character Event Intelligence Agent Intercepted");
                    break;
                case CharacterEventType.IntelligenceMissionSucceedEspionage:
                case CharacterEventType.IntelligenceMissionSucceedSabotage:
                    result = TextResolver.GetText("Character Event Intelligence Mission Succeeded");
                    break;
                case CharacterEventType.CriticalResearchFailure:
                    result = TextResolver.GetText("Character Event Research Critical Failure");
                    break;
                case CharacterEventType.CriticalResearchSuccess:
                    result = TextResolver.GetText("Character Event Research Critical Success");
                    break;
                case CharacterEventType.CharacterStart:
                    result = TextResolver.GetText("Character Event Start");
                    break;
                case CharacterEventType.CharacterTransferLocation:
                    result = TextResolver.GetText("Character Event Transfer Location");
                    break;
                case CharacterEventType.CharacterTraitGain:
                    result = TextResolver.GetText("Character Event Trait Gain");
                    break;
                case CharacterEventType.CharacterSkillGain:
                    result = TextResolver.GetText("Character Event Skill Gain");
                    break;
                case CharacterEventType.CharacterSkillProgress:
                    result = TextResolver.GetText("Character Event Skill Progress");
                    break;
                case CharacterEventType.ResearchAdvanceWeapons:
                case CharacterEventType.ResearchAdvanceEnergy:
                case CharacterEventType.ResearchAdvanceHighTech:
                    result = TextResolver.GetText("Character Event Research Breakthrough");
                    break;
                case CharacterEventType.SpaceBattle:
                    result = TextResolver.GetText("Character Event Space Battle");
                    break;
                case CharacterEventType.TourismIncome:
                    result = TextResolver.GetText("Character Event Tourism Income");
                    break;
                case CharacterEventType.TradeIncome:
                    result = TextResolver.GetText("Character Event Trade Income");
                    break;
                case CharacterEventType.TreatySigned:
                    result = TextResolver.GetText("Character Event Treaty Signed");
                    break;
                case CharacterEventType.TroopComplete:
                    result = TextResolver.GetText("Character Event Troop Complete");
                    break;
                case CharacterEventType.WarEnded:
                    result = TextResolver.GetText("Character Event War Ended");
                    break;
                case CharacterEventType.WarStarted:
                    result = TextResolver.GetText("Character Event War Started");
                    break;
                case CharacterEventType.TargetOfFailedAssassination:
                    result = TextResolver.GetText("Character Event Target Of Failed Assassination");
                    break;
                case CharacterEventType.Subjugated:
                    result = TextResolver.GetText("Character Event Subjugated");
                    break;
                case CharacterEventType.TreatyBroken:
                    result = TextResolver.GetText("Character Event Treaty Broken");
                    break;
                case CharacterEventType.AmbassadorAssignedToEmpire:
                    result = TextResolver.GetText("Character Event Ambassador Assigned To Empire");
                    break;
                case CharacterEventType.Boarding:
                    result = TextResolver.GetText("Character Event Boarding");
                    break;
                case CharacterEventType.Raid:
                    result = TextResolver.GetText("Character Event Raid");
                    break;
                case CharacterEventType.SmugglingSuccess:
                    result = TextResolver.GetText("Character Event Smuggling Success");
                    break;
                case CharacterEventType.SmugglingDetection:
                    result = TextResolver.GetText("Character Event Smuggling Detection");
                    break;
            }
            return result;
        }

    }
}
