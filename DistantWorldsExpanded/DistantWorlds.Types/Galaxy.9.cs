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
        private void GenerateNewPirateEmpires()
        {
            int num = (int)(2.0 * PiratePrevalence * (double)MaximumEmpireAmount);
            if (DestroyedPiratesDoNotRespawn)
            {
                long num2 = CurrentStarDate - _StartStarDate;
                if (num2 > 300000)
                {
                    num = PirateEmpires.Count;
                }
            }
            if (PirateEmpires.Count >= num)
            {
                return;
            }
            int num3 = num - PirateEmpires.Count;
            double num4 = (double)MaxSolarSystemSize * 2.1;
            double num5 = (double)SizeX / 50.0;
            switch (PirateProximity)
            {
                case 0:
                    num4 *= 1.0;
                    num5 *= 1.0;
                    break;
                case 1:
                    num4 *= 4.0;
                    num5 *= 4.0;
                    break;
                case 2:
                    num4 *= 8.0;
                    num5 *= 8.0;
                    break;
            }
            int num6 = (int)((double)StarCount * 0.7);
            double val = ((double)num6 - (double)ColonyCount) / (double)num6;
            val = Math.Max(val, 0.1);
            num3 = (int)((double)num3 * val);
            for (int i = 0; i < num3; i++)
            {
                double x = 0.0;
                double y = 0.0;
                bool flag = false;
                Habitat habitat = null;
                int num7 = 0;
                while (!flag && num7 < 100)
                {
                    ObtainRandomGalaxyCoordinates(out x, out y);
                    ResourceDefinition resourceDefinition = ResourceSystem.FuelResources[0];
                    habitat = FindNearestHabitatWithResource(x, y, resourceDefinition.ResourceID);
                    if (habitat != null)
                    {
                        for (int j = 0; j < IndependentColonies.Count; j++)
                        {
                            Habitat habitat2 = IndependentColonies[j];
                            if (habitat2 != null && habitat2.Empire == IndependentEmpire && habitat2.SystemIndex == habitat.SystemIndex)
                            {
                                habitat = null;
                                break;
                            }
                        }
                    }
                    if (habitat != null)
                    {
                        BuiltObject builtObject = FindNearestBuiltObject((int)habitat.Xpos, (int)habitat.Ypos, BuiltObjectRole.Undefined, includeIndependentBuiltObjects: false);
                        double num8 = double.MaxValue;
                        if (builtObject != null)
                        {
                            num8 = CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                        }
                        if (num8 > num4)
                        {
                            Habitat habitat3 = FindNearestColony(habitat.Xpos, habitat.Ypos, null, 0, includeIndependentColonies: false);
                            double num9 = double.MaxValue;
                            if (habitat3 != null)
                            {
                                num9 = CalculateDistance(habitat.Xpos, habitat.Ypos, habitat3.Xpos, habitat3.Ypos);
                            }
                            if (num9 > num5)
                            {
                                bool flag2 = false;
                                Empire empire = FindNearestPirateFaction(habitat.Xpos, habitat.Ypos, null, includeSuperPirates: true);
                                if (empire != null && empire.PirateEmpireBaseHabitat != null)
                                {
                                    double num10 = CalculateDistance(habitat.Xpos, habitat.Ypos, empire.PirateEmpireBaseHabitat.Xpos, empire.PirateEmpireBaseHabitat.Ypos);
                                    if (num10 < 1000000.0)
                                    {
                                        flag2 = true;
                                    }
                                }
                                if (!flag2)
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                    num7++;
                }
                if (flag && NextEmpireID < MaximumEmpireCount)
                {
                    SelectRelativeHabitatSurfacePoint(habitat, out var x2, out var y2);
                    GeneratePirateEmpire(habitat, (int)x2, (int)y2, useRace: true);
                }
            }
        }

        public Habitat IdentifyPirateNewHomeLocation(Empire pirateFaction)
        {
            Habitat habitat = null;
            if (pirateFaction != null)
            {
                double num = SizeX / 2;
                double num2 = SizeY / 2;
                Habitat systemToExclude = null;
                if (pirateFaction.PirateEmpireBaseHabitat != null)
                {
                    num = pirateFaction.PirateEmpireBaseHabitat.Xpos;
                    num2 = pirateFaction.PirateEmpireBaseHabitat.Ypos;
                    systemToExclude = DetermineHabitatSystemStar(pirateFaction.PirateEmpireBaseHabitat);
                }
                List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                list.Add(BuiltObjectSubRole.SmallSpacePort);
                list.Add(BuiltObjectSubRole.MediumSpacePort);
                list.Add(BuiltObjectSubRole.LargeSpacePort);
                list.Add(BuiltObjectSubRole.MiningStation);
                list.Add(BuiltObjectSubRole.GasMiningStation);
                list.Add(BuiltObjectSubRole.EnergyResearchStation);
                list.Add(BuiltObjectSubRole.HighTechResearchStation);
                list.Add(BuiltObjectSubRole.WeaponsResearchStation);
                list.Add(BuiltObjectSubRole.ResortBase);
                list.Add(BuiltObjectSubRole.MonitoringStation);
                list.Add(BuiltObjectSubRole.DefensiveBase);
                List<BuiltObjectSubRole> subRoles = list;
                HabitatList habitatList = pirateFaction.DetermineHabitatsWithBasesIncludingBuilding(subRoles);
                byte resourceID = ResourceSystem.FuelResources[0].ResourceID;
                Design design = pirateFaction.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.Frigate, pirateFaction);
                if (design != null && design.FuelType != null)
                {
                    resourceID = design.FuelType.ResourceID;
                }
                int num3 = 0;
                bool flag = false;
                double num4 = num;
                double num5 = num2;
                while (!flag && num3 < 50)
                {
                    double num6 = 400000.0 + Rnd.NextDouble() * 400000.0;
                    double num7 = Rnd.NextDouble() * Math.PI * 2.0;
                    num4 += Math.Sin(num7) * num6;
                    num5 += Math.Cos(num7) * num6;
                    habitat = FastFindNearestFuelHabitatAlternate(num4, num5, resourceID, pirateFaction.PirateEmpireBaseHabitat, pirateFaction, systemToExclude, allowBases: false);
                    if (habitat != null)
                    {
                        Habitat habitat2 = FindNearestColony(habitat.Xpos, habitat.Ypos, null, 0, includeIndependentColonies: false);
                        double num8 = double.MaxValue;
                        if (habitat2 != null)
                        {
                            num8 = CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos);
                        }
                        if (num8 > 500000.0)
                        {
                            bool flag2 = false;
                            Empire empire = FindNearestPirateFaction(habitat.Xpos, habitat.Ypos, pirateFaction, includeSuperPirates: true);
                            if (empire != null && empire.PirateEmpireBaseHabitat != null)
                            {
                                double num9 = CalculateDistance(habitat.Xpos, habitat.Ypos, empire.PirateEmpireBaseHabitat.Xpos, empire.PirateEmpireBaseHabitat.Ypos);
                                if (num9 < 1000000.0)
                                {
                                    flag2 = true;
                                }
                            }
                            if (!flag2 && (habitat.BasesAtHabitat == null || habitat.BasesAtHabitat.Count <= 0) && !habitatList.Contains(habitat))
                            {
                                flag = true;
                            }
                        }
                    }
                    num3++;
                }
            }
            return habitat;
        }

        public static Component GetLatestEmpireComponent(ComponentType componentType, ComponentList latestEmpireComponents)
        {
            return latestEmpireComponents[(int)componentType];
        }

        private void DoSuperPirateTasks()
        {
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire.PirateEmpireSuperPirates)
                {
                    DoSuperPirateTasks(empire);
                }
            }
        }

        public BuiltObject IdentifyPirateBase(Empire pirateFaction)
        {
            if (pirateFaction != null && pirateFaction.BuiltObjects != null && pirateFaction.PirateEmpireBaseHabitat != null)
            {
                BuiltObject builtObject = null;
                if (pirateFaction.PirateEmpireBaseHabitat.BasesAtHabitat != null)
                {
                    for (int i = 0; i < pirateFaction.PirateEmpireBaseHabitat.BasesAtHabitat.Count; i++)
                    {
                        BuiltObject builtObject2 = pirateFaction.PirateEmpireBaseHabitat.BasesAtHabitat[i];
                        if (builtObject2.Role == BuiltObjectRole.Base && builtObject2.ActualEmpire == pirateFaction && builtObject2.ParentHabitat != null && builtObject2.ParentHabitat == pirateFaction.PirateEmpireBaseHabitat && builtObject2.ExtractionGas > 0 && (builtObject == null || builtObject.Size < builtObject2.Size))
                        {
                            builtObject = builtObject2;
                        }
                    }
                }
                if (builtObject == null)
                {
                    for (int j = 0; j < pirateFaction.BuiltObjects.Count; j++)
                    {
                        BuiltObject builtObject3 = pirateFaction.BuiltObjects[j];
                        if (builtObject3.Role == BuiltObjectRole.Base && builtObject3.ExtractionGas > 0 && (builtObject == null || builtObject.Size < builtObject3.Size))
                        {
                            builtObject = builtObject3;
                        }
                    }
                }
                return builtObject;
            }
            return null;
        }

        public BuiltObject IdentifyPirateSpaceport(Empire pirateFaction)
        {
            if (pirateFaction != null && pirateFaction.BuiltObjects != null && pirateFaction.PirateEmpireBaseHabitat != null)
            {
                BuiltObject builtObject = null;
                if (pirateFaction.PirateEmpireBaseHabitat.BasesAtHabitat != null)
                {
                    for (int i = 0; i < pirateFaction.PirateEmpireBaseHabitat.BasesAtHabitat.Count; i++)
                    {
                        BuiltObject builtObject2 = pirateFaction.PirateEmpireBaseHabitat.BasesAtHabitat[i];
                        if (builtObject2.Role == BuiltObjectRole.Base && builtObject2.ActualEmpire == pirateFaction && (builtObject2.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject2.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject2.SubRole == BuiltObjectSubRole.LargeSpacePort) && builtObject2.ParentHabitat != null && builtObject2.ParentHabitat == pirateFaction.PirateEmpireBaseHabitat && builtObject2.ExtractionGas > 0 && (builtObject == null || builtObject.Size < builtObject2.Size))
                        {
                            builtObject = builtObject2;
                        }
                    }
                }
                if (builtObject == null)
                {
                    for (int j = 0; j < pirateFaction.BuiltObjects.Count; j++)
                    {
                        BuiltObject builtObject3 = pirateFaction.BuiltObjects[j];
                        if (builtObject3.Role == BuiltObjectRole.Base && (builtObject3.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject3.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject3.SubRole == BuiltObjectSubRole.LargeSpacePort) && builtObject3.ExtractionGas > 0 && (builtObject == null || builtObject.Size < builtObject3.Size))
                        {
                            builtObject = builtObject3;
                        }
                    }
                }
                return builtObject;
            }
            return null;
        }

        private void DoSuperPirateTasks(Empire superPirateFaction)
        {
            if (superPirateFaction.ShipGroups == null)
            {
                superPirateFaction.ShipGroups = new ShipGroupList();
            }
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < superPirateFaction.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = superPirateFaction.BuiltObjects[i];
                if (builtObject.Role == BuiltObjectRole.Military && builtObject.BuiltAt == null && builtObject.ShipGroup == null && builtObject.TopSpeed > 0 && builtObject.DamagedComponentCount == 0 && builtObject.SubRole != BuiltObjectSubRole.Escort)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            BuiltObject builtObject2 = IdentifyPirateBase(superPirateFaction);
            if (superPirateFaction.ShipGroups.Count == 0 && builtObjectList.Count > 0)
            {
                ShipGroup shipGroup = new ShipGroup(this);
                shipGroup.Empire = superPirateFaction;
                shipGroup.ShipTargetAmount = int.MaxValue;
                shipGroup.TroopTargetStrength = 0;
                shipGroup.GatherPoint = builtObject2;
                superPirateFaction.AddShipsToShipGroup(shipGroup, builtObjectList, int.MaxValue, isNew: true, builtObject2);
                if (shipGroup.Ships.Count > 0)
                {
                    shipGroup.Name = TextResolver.GetText("Phantom Fleet");
                    superPirateFaction.ShipGroups.Add(shipGroup);
                    superPirateFaction.ShipGroups.Sort((x, y) => x.Name.CompareTo(y.Name));
                }
            }
            if (superPirateFaction.ShipGroups.Count <= 0)
            {
                return;
            }
            ShipGroup shipGroup2 = superPirateFaction.ShipGroups[0];
            if (shipGroup2 == null)
            {
                return;
            }
            shipGroup2.Posture = FleetPosture.Attack;
            shipGroup2.PostureRangeSquared = double.MaxValue;
            if (builtObjectList.Count > 0)
            {
                superPirateFaction.AddShipsToShipGroup(shipGroup2, builtObjectList, int.MaxValue, isNew: true, builtObject2);
            }
            if ((shipGroup2.Mission == null || shipGroup2.Mission.Type == BuiltObjectMissionType.Undefined) && builtObject2 != null)
            {
                BuiltObject builtObject3 = FindNearestBaseForPirateAttack(builtObject2.Xpos, builtObject2.Ypos, superPirateFaction);
                if (builtObject3 != null)
                {
                    shipGroup2.AssignMission(BuiltObjectMissionType.Attack, builtObject3, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                }
            }
        }

        public void ChanceAttackedPirateFactionJoinsPhantomPirates(Empire attackingPhantomPirateFaction, BuiltObject attackedPirateBase)
        {
            if (attackingPhantomPirateFaction == null || !attackingPhantomPirateFaction.PirateEmpireSuperPirates || attackingPhantomPirateFaction.PirateEmpireBaseHabitat == null || attackedPirateBase == null || attackedPirateBase.Role != BuiltObjectRole.Base || attackedPirateBase.Empire == null || attackedPirateBase.Empire == PlayerEmpire || attackedPirateBase.Empire.PirateEmpireBaseHabitat == null || attackedPirateBase.Empire.PirateEmpireSuperPirates || !((double)attackedPirateBase.CurrentShields < (double)attackedPirateBase.ShieldsCapacity * 0.5) || Rnd.Next(0, 50) != 1)
            {
                return;
            }
            int num = 1;
            if (attackedPirateBase.Empire.BuiltObjects != null)
            {
                num = attackedPirateBase.Empire.BuiltObjects.TotalMobileMilitaryFirepower();
            }
            if (num >= 600)
            {
                return;
            }
            int num2 = 1;
            if (attackingPhantomPirateFaction.BuiltObjects != null)
            {
                num2 = attackingPhantomPirateFaction.BuiltObjects.TotalMobileMilitaryFirepower();
            }
            double num3 = (double)num2 / (double)num;
            if (!(num3 > 2.0))
            {
                return;
            }
            Empire empire = attackedPirateBase.Empire;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(empire.BuiltObjects);
            foreach (BuiltObject item in builtObjectList)
            {
                attackingPhantomPirateFaction.TakeOwnershipOfBuiltObject(item, attackingPhantomPirateFaction, setDesignAsObsolete: true);
                item.Stance = BuiltObjectStance.AttackEnemies;
                item.FleeWhen = BuiltObjectFleeWhen.Shields20;
                item.Design.Stance = BuiltObjectStance.AttackEnemies;
                item.Design.FleeWhen = BuiltObjectFleeWhen.Shields20;
            }
            bool flag = false;
            switch (attackedPirateBase.SubRole)
            {
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                    flag = true;
                    break;
            }
            if (flag && Rnd.Next(0, 4) == 1)
            {
                FearfulPirateFactionJoinsPlayer(attackingPhantomPirateFaction, empire);
            }
        }

        public static double CalculateBuiltObjectLootingValue(BuiltObject builtObject)
        {
            return BaconGalaxy.CalculateBuiltObjectLootingValue(builtObject);
        }

        public void PirateFactionJoinsEmpire(Empire empire, Empire pirateFaction)
        {
            if (empire != null && pirateFaction != null)
            {
                EliminatePirateFaction(pirateFaction, empire);
            }
        }

        public void FearfulPirateFactionJoinsPlayer(Empire phantomPirateFaction, Empire pirateFactionToExclude)
        {
            if (PlayerEmpire == null)
            {
                return;
            }
            Empire empire = null;
            if (PlayerEmpire.PirateEmpireBaseHabitat != null)
            {
                empire = FindNearestPirateFaction(PlayerEmpire.PirateEmpireBaseHabitat.Xpos, PlayerEmpire.PirateEmpireBaseHabitat.Ypos, pirateFactionToExclude, includeSuperPirates: false);
            }
            else if (PlayerEmpire.Capital != null)
            {
                empire = FindNearestPirateFaction(PlayerEmpire.Capital.Xpos, PlayerEmpire.Capital.Ypos, pirateFactionToExclude, includeSuperPirates: false);
            }
            if (empire == null || empire.PirateEmpireSuperPirates || empire == PlayerEmpire)
            {
                return;
            }
            int num = 1;
            if (empire.BuiltObjects != null)
            {
                num = empire.BuiltObjects.TotalMobileMilitaryFirepower();
            }
            if (num >= 600)
            {
                return;
            }
            int num2 = 1;
            if (phantomPirateFaction.BuiltObjects != null)
            {
                num2 = phantomPirateFaction.BuiltObjects.TotalMobileMilitaryFirepower();
            }
            double num3 = (double)num2 / (double)num;
            if (num3 > 2.0)
            {
                BuiltObject builtObject = IdentifyPirateSpaceport(empire);
                string message = string.Format(TextResolver.GetText("Pirate Faction Joins Your Empire Fearful Phantom Pirates"), empire.Name, phantomPirateFaction.Name);
                PirateFactionJoinsEmpire(PlayerEmpire, empire);
                string text = TextResolver.GetText("Pirate Faction Joins Your Empire");
                if (builtObject != null)
                {
                    PlayerEmpire.SendEventMessageToEmpire(EventMessageType.PirateFactionJoinsYou, text, message, builtObject, builtObject.ParentHabitat);
                }
                else
                {
                    PlayerEmpire.SendEventMessageToEmpire(EventMessageType.PirateFactionJoinsYou, text, message, null, null);
                }
            }
        }

        public int CountPirateFactionsAcceptedSmugglingMission(StellarObject target)
        {
            int num = 0;
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire != null && empire.Active && empire.PirateMissions != null && empire.PirateMissions.GetFirstByTargetAndType(target, EmpireActivityType.Smuggle) != null)
                {
                    num++;
                }
            }
            return num;
        }

        public void RemovePirateSmugglingMissionFromAllEmpires(EmpireActivity smugglingMission)
        {
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire != null && empire.Active && empire.PirateMissions != null && empire.PirateMissions.ContainsEquivalent(smugglingMission.Target, smugglingMission.Type))
                {
                    string empty = string.Empty;
                    empire.SendMessageToEmpire(description: (smugglingMission.RequestingEmpire == IndependentEmpire) ? ((smugglingMission.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Pirate Independent"), smugglingMission.Target.Name, new Resource(smugglingMission.ResourceId).Name) : string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Pirate Independent All Resources"), smugglingMission.Target.Name)) : ((smugglingMission.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Pirate"), smugglingMission.Target.Name, new Resource(smugglingMission.ResourceId).Name, smugglingMission.RequestingEmpire.Name) : string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Pirate All Resources"), smugglingMission.Target.Name, smugglingMission.RequestingEmpire.Name)), recipientEmpire: empire, messageType: EmpireMessageType.PirateSmugglingMissionCompleted, subject: smugglingMission.Target);
                    empire.PirateMissions.RemoveEquivalent(smugglingMission);
                }
            }
        }

        public int CountPirateEmpiresConsideringMission(EmpireActivity mission, Empire pirateEmpireToExclude)
        {
            int num = 0;
            if (mission != null)
            {
                for (int i = 0; i < PirateEmpires.Count; i++)
                {
                    Empire empire = PirateEmpires[i];
                    if (empire == null || !empire.Active || empire == pirateEmpireToExclude)
                    {
                        continue;
                    }
                    switch (mission.Type)
                    {
                        case EmpireActivityType.Attack:
                            {
                                int pirateEmpireStrength2 = empire.BuiltObjects.TotalMobileMilitaryFirepower();
                                if (empire.PirateCheckAcceptAttackMission(mission, pirateEmpireStrength2))
                                {
                                    num++;
                                }
                                break;
                            }
                        case EmpireActivityType.Defend:
                            {
                                int pirateEmpireStrength = empire.BuiltObjects.TotalMobileMilitaryFirepower();
                                if (empire.PirateCheckAcceptDefendMission(mission, pirateEmpireStrength))
                                {
                                    num++;
                                }
                                break;
                            }
                        case EmpireActivityType.Smuggle:
                            if (empire.Policy.AcceptPirateSmugglingMissions && empire.IsObjectAreaKnownToThisEmpire(mission.Target) && empire.Freighters != null && empire.Freighters.Count > 0)
                            {
                                num++;
                            }
                            break;
                    }
                }
            }
            return num;
        }

        private void ReviewPirateMissionsAndAssign(long starDate, double timePassed)
        {
            EmpireActivityList empireActivityList = new EmpireActivityList();
            EmpireActivityList empireActivityList2 = new EmpireActivityList();
            for (int i = 0; i < PirateMissions.Count; i++)
            {
                EmpireActivity empireActivity = PirateMissions[i];
                if (empireActivity == null || (empireActivity.Type != EmpireActivityType.Attack && empireActivity.Type != EmpireActivityType.Defend))
                {
                    continue;
                }
                if (empireActivity.AssignedEmpire != null)
                {
                    long num = (long)(timePassed * 1000.0);
                    empireActivity.BidTimeRemaining -= num;
                    if (empireActivity.BidTimeRemaining <= 0)
                    {
                        empireActivity.BidTimeRemaining = 0L;
                        empireActivity.ExpiryDate = starDate + (long)(2.0 * (double)RealSecondsInGalacticYear * 1000.0);
                        empireActivity.AssignedEmpire.PirateMissions.Add(empireActivity);
                        empireActivityList.Add(empireActivity);
                    }
                }
                else if (empireActivity.ExpiryDate < starDate)
                {
                    empireActivityList2.Add(empireActivity);
                }
            }
            for (int j = 0; j < empireActivityList.Count; j++)
            {
                PirateMissions.Remove(empireActivityList[j]);
            }
            for (int k = 0; k < empireActivityList2.Count; k++)
            {
                EmpireActivity empireActivity2 = empireActivityList2[k];
                if (empireActivity2 != null)
                {
                    if (empireActivity2.AssignedEmpire != null)
                    {
                        empireActivity2.AssignedEmpire.CompletePirateMission(empireActivity2);
                    }
                    if (empireActivity2.RequestingEmpire != null && empireActivity2.RequestingEmpire.PirateMissions != null)
                    {
                        empireActivity2.RequestingEmpire.PirateMissions.Remove(empireActivity2);
                    }
                    PirateMissions.Remove(empireActivity2);
                }
            }
        }

        public bool CheckCancelAttackMissionsForBuiltObject(BuiltObject builtObject, Empire empireToExclude)
        {
            bool result = false;
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire == null || !empire.Active || empire.PirateMissions == null || (empireToExclude != null && empire == empireToExclude))
                {
                    continue;
                }
                EmpireActivity byAttackTarget = empire.PirateMissions.GetByAttackTarget(builtObject, empire);
                if (byAttackTarget != null)
                {
                    empire.PirateMissions.Remove(byAttackTarget);
                    if (byAttackTarget.RequestingEmpire != null && byAttackTarget.RequestingEmpire.PirateMissions != null)
                    {
                        byAttackTarget.RequestingEmpire.PirateMissions.Remove(byAttackTarget);
                    }
                    if (byAttackTarget.AssignedEmpire != null && byAttackTarget.AssignedEmpire.PirateMissions != null)
                    {
                        byAttackTarget.AssignedEmpire.PirateMissions.Remove(byAttackTarget);
                    }
                    if (byAttackTarget.RequestingEmpire != null)
                    {
                        string description = string.Format(TextResolver.GetText("Pirate Attack Mission Cancelled Pirate"), byAttackTarget.Target.Name, byAttackTarget.RequestingEmpire.Name);
                        empire.SendMessageToEmpire(empire, EmpireMessageType.PirateAttackMissionCompleted, byAttackTarget.Target, description);
                        description = string.Format(TextResolver.GetText("Pirate Attack Mission Cancelled Other"), byAttackTarget.Target.Name, empire.Name);
                        byAttackTarget.RequestingEmpire.SendMessageToEmpire(byAttackTarget.RequestingEmpire, EmpireMessageType.PirateAttackMissionCompleted, byAttackTarget.Target, description);
                    }
                    result = true;
                }
            }
            return result;
        }

        private void AssignPirateShipMissions()
        {
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire pirateEmpire = PirateEmpires[i];
                AssignPirateShipMissions(pirateEmpire);
            }
        }

        private void AssignPirateShipMissions(Empire pirateEmpire)
        {
            if (DeferEventsForGameStart || pirateEmpire == null || pirateEmpire.BuiltObjects == null || pirateEmpire.PirateMissions == null)
            {
                return;
            }
            BuiltObject builtObject = null;
            for (int i = 0; i < pirateEmpire.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject2 = pirateEmpire.BuiltObjects[i];
                if (builtObject2 != null && builtObject2.Role == BuiltObjectRole.Base)
                {
                    builtObject = builtObject2;
                }
            }
            if (builtObject == null)
            {
                return;
            }
            double num = (double)SectorSize * Math.Max(1.0, _LifePrevalenceMultiplier);
            for (int j = 0; j < pirateEmpire.BuiltObjects.Count; j++)
            {
                BuiltObject builtObject3 = pirateEmpire.BuiltObjects[j];
                if (builtObject3 == null || builtObject3.Role != BuiltObjectRole.Military || builtObject3.ShipGroup != null || (builtObject3.Mission != null && builtObject3.Mission.Type != 0))
                {
                    continue;
                }
                if (builtObject3.DamagedComponentCount > 0 || builtObject3.RepairForNextMission)
                {
                    if (builtObject != null)
                    {
                        builtObject3.AssignMission(BuiltObjectMissionType.Repair, builtObject, null, BuiltObjectMissionPriority.Normal);
                        builtObject3.RepairForNextMission = false;
                    }
                    continue;
                }
                if (builtObject3.RefuelForNextMission)
                {
                    if (builtObject != null)
                    {
                        builtObject3.AssignMission(BuiltObjectMissionType.Refuel, builtObject, null, BuiltObjectMissionPriority.Normal);
                        builtObject3.RefuelForNextMission = false;
                    }
                    continue;
                }
                if (pirateEmpire.PirateMissions.ResolveActivitiesByType(EmpireActivityType.Attack).Count > 0)
                {
                    EmpireActivityList empireActivityList = pirateEmpire.PirateMissions.ResolveActivitiesByType(EmpireActivityType.Attack);
                    int index = Rnd.Next(0, empireActivityList.Count);
                    Empire targetEmpire = empireActivityList[index].TargetEmpire;
                    BuiltObject builtObject4 = FastFindNearestMiningStation((int)builtObject.Xpos, (int)builtObject.Ypos, targetEmpire);
                    if (builtObject4 == null)
                    {
                        builtObject4 = FastFindNearestResearchFacility((int)builtObject.Xpos, (int)builtObject.Ypos, targetEmpire);
                    }
                    if (builtObject4 == null)
                    {
                        builtObject4 = FastFindNearestLongRangeScanner((int)builtObject.Xpos, (int)builtObject.Ypos, targetEmpire);
                    }
                    if (builtObject4 != null && builtObject3.WithinFuelRangeAndRefuel(builtObject4.Xpos, builtObject4.Ypos, 0.0))
                    {
                        builtObject3.AssignMission(BuiltObjectMissionType.Attack, builtObject4, null, BuiltObjectMissionPriority.Normal);
                    }
                    continue;
                }
                if (Rnd.Next(0, 3) == 1)
                {
                    BuiltObject builtObject5 = null;
                    builtObject5 = ((Rnd.Next(0, 2) != 1) ? FindNearestBuiltObject((int)builtObject.Xpos, (int)builtObject.Ypos, BuiltObjectSubRole.GasMiningStation, includeSecondaryEmpires: false) : FindNearestBuiltObject((int)builtObject.Xpos, (int)builtObject.Ypos, BuiltObjectSubRole.MiningStation, includeSecondaryEmpires: false));
                    if (builtObject5 != null && builtObject5.Empire != pirateEmpire && builtObject5.Empire != null && pirateEmpire.ObtainPirateRelation(builtObject5.Empire).Type != PirateRelationType.Protection)
                    {
                        double num2 = CalculateDistance(builtObject5.Xpos, builtObject5.Ypos, builtObject3.Xpos, builtObject3.Ypos);
                        if (num2 < num && builtObject3.WithinFuelRangeAndRefuel(builtObject5.Xpos, builtObject5.Ypos, 0.0))
                        {
                            builtObject3.AssignMission(BuiltObjectMissionType.Attack, builtObject5, null, BuiltObjectMissionPriority.Normal);
                        }
                    }
                    continue;
                }
                Habitat habitat = FastFindNearestIndependentHabitat(builtObject3.Xpos, builtObject3.Ypos);
                if (habitat != null)
                {
                    double num3 = CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject3.Xpos, builtObject3.Ypos);
                    if (num3 < num && builtObject3.WithinFuelRangeAndRefuel(habitat.Xpos, habitat.Ypos, 0.0))
                    {
                        long starDate = CurrentStarDate + (long)(0.5 * (double)(RealSecondsInGalacticYear * 1000));
                        builtObject3.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat, null, 0.0, 0.0, starDate, BuiltObjectMissionPriority.Normal, allowReprocessing: true);
                    }
                }
            }
        }

        public string GeneratePirateBaseName(Habitat habitat)
        {
            string[] array = new string[13]
            {
            "Secret", "Eagles", "Villainous", "Brigands", "Outlaws", "Fugitives", "Desperado", "Secluded", "Bounty Hunters", "Lonely",
            "Gamblers", "Bandits", "Smugglers"
            };
            string[] array2 = new string[20]
            {
            "Lair", "Base", "Hideout", "Retreat", "Fortress", "Cave", "Cove", "Outpost", "Den", "Haunt",
            "Hideaway", "Nest", "Sanctuary", "Refuge", "Shelter", "Haven", "End", "Rest", "Station", "Stronghold"
            };
            int num = Rnd.Next(0, array.Length);
            string text = array[num];
            num = Rnd.Next(0, array2.Length);
            string text2 = array2[num];
            string result = text + " " + text2;
            if (habitat != null && habitat.Category != HabitatCategoryType.GasCloud && Rnd.Next(0, 4) == 1)
            {
                Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                result = habitat2.Name + " " + array2[Rnd.Next(0, array2.Length)];
            }
            return result;
        }

        private string GeneratePirateEmpireName(Habitat habitat, PiratePlayStyle playStyle)
        {
            string[] array = new string[21]
            {
            "Bloody", "Dread", "Black", "Dirty", "Evil", "Iron", "Red", "Fierce", "Cruel", "Sinister",
            "Vicious", "Lone", "Savage", "Fearsome", "Deadly", "Venomous", "Murderous", "Dark", "Grim", "Haunted",
            "Menacing"
            };
            string[] array2 = new string[8] { "Sun", "Star", "Rock", "Moon", "Storm", "Fang", "Claw", "Dagger" };
            string[] array3 = new string[16]
            {
            "Pirates", "Marauders", "Bandits", "Raiders", "Buccaneers", "Outlaws", "Corsairs", "Pillagers", "Gangsters", "Ravagers",
            "Prowlers", "Intruders", "Invaders", "Skyjackers", "Gang", "Mercenaries"
            };
            switch (playStyle)
            {
                case PiratePlayStyle.Balanced:
                    array = new string[13]
                    {
                "Black", "Iron", "Red", "Fierce", "Sinister", "Lone", "Savage", "Fearsome", "Venomous", "Dark",
                "Grim", "Menacing", "Dread"
                    };
                    array3 = new string[9] { "Council", "Network", "League", "Force", "Clan", "Authority", "Confederacy", "Confederation", "Security" };
                    break;
                case PiratePlayStyle.Mercenary:
                    array = new string[24]
                    {
                "Bloody", "Black", "Dirty", "Evil", "Iron", "Red", "Fierce", "Cruel", "Sinister", "Vicious",
                "Lone", "Savage", "Fearsome", "Deadly", "Venomous", "Murderous", "Dark", "Grim", "Haunted", "Menacing",
                "Dread", "Blood", "Burning", "Hidden"
                    };
                    array3 = new string[18]
                    {
                "Pirates", "Marauders", "Bandits", "Raiders", "Buccaneers", "Outlaws", "Corsairs", "Pillagers", "Gangsters", "Ravagers",
                "Prowlers", "Intruders", "Invaders", "Skyjackers", "Gang", "Mercenaries", "Warriors", "Army"
                    };
                    break;
                case PiratePlayStyle.Pirate:
                    array = new string[24]
                    {
                "Bloody", "Black", "Dirty", "Evil", "Iron", "Red", "Fierce", "Cruel", "Sinister", "Vicious",
                "Lone", "Savage", "Fearsome", "Deadly", "Venomous", "Murderous", "Dark", "Grim", "Haunted", "Menacing",
                "Dread", "Burning", "Fire", "Lost"
                    };
                    array3 = new string[16]
                    {
                "Pirates", "Marauders", "Bandits", "Raiders", "Buccaneers", "Outlaws", "Corsairs", "Pillagers", "Gangsters", "Ravagers",
                "Prowlers", "Intruders", "Invaders", "Skyjackers", "Gang", "Horde"
                    };
                    break;
                case PiratePlayStyle.Smuggler:
                    array = new string[13]
                    {
                "Black", "Iron", "Red", "Fierce", "Sinister", "Lone", "Savage", "Fearsome", "Hidden", "Dark",
                "Grim", "Menacing", "Dread"
                    };
                    array2 = new string[10] { "Sun", "Star", "Rock", "Moon", "Storm", "Fang", "Claw", "Market", "Trade", "Merchant" };
                    array3 = new string[19]
                    {
                "Corporation", "Consortium", "Syndicate", "Cartel", "Guild", "Gang", "Company", "Interstellar", "Shipping", "Spaceways",
                "Freightways", "Industries", "Mining", "Transport", "Exports", "Ventures", "Starfreight", "Minerals", "Salvage"
                    };
                    break;
            }
            int num = Rnd.Next(0, array.Length);
            string text = array[num];
            num = Rnd.Next(0, array2.Length);
            string text2 = array2[num];
            num = Rnd.Next(0, array3.Length);
            string text3 = array3[num];
            string result = string.Empty;
            switch (Rnd.Next(0, 3))
            {
                case 0:
                    result = text + " " + text3;
                    break;
                case 1:
                    result = text + " " + text2 + " " + text3;
                    break;
                case 2:
                    {
                        Habitat habitat2 = FindNearestColony(habitat.Xpos, habitat.Ypos, null, 0, includeIndependentColonies: true);
                        Habitat habitat3 = DetermineHabitatSystemStar(habitat2);
                        result = habitat3.Name + " " + text3;
                        break;
                    }
            }
            return result;
        }

        public static string RemoveSpecialCharacters(string text)
        {
            return Regex.Replace(text, "[^a-zA-Z0-9-]+", "", RegexOptions.Compiled);
        }

        public static EncyclopediaItemList AddRaceTopics(EncyclopediaItemList encyclopediaItems, RaceList races, string applicationStartupPath, string customizationSetName)
        {
            string text = applicationStartupPath + "\\help\\";
            string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\help\\";
            for (int i = 0; i < races.Count; i++)
            {
                Race race = races[i];
                if (race == null)
                {
                    continue;
                }
                string text3 = "Race_" + RemoveSpecialCharacters(race.Name) + ".mht";
                if (File.Exists(text2 + text3) || File.Exists(text + text3))
                {
                    encyclopediaItems.Add(new EncyclopediaItem(race.Name, text3, EncyclopediaCategory.Races, isCategoryRoot: false));
                    switch (race.NativeHabitatType)
                    {
                        case HabitatType.Continental:
                            encyclopediaItems[race.Name].RelatedItems.Add(encyclopediaItems[TextResolver.GetText("Continental Planets")]);
                            encyclopediaItems[TextResolver.GetText("Continental Planets")].RelatedItems.Add(encyclopediaItems[race.Name]);
                            break;
                        case HabitatType.MarshySwamp:
                            encyclopediaItems[race.Name].RelatedItems.Add(encyclopediaItems[TextResolver.GetText("Marshy Swamp Planets")]);
                            encyclopediaItems[TextResolver.GetText("Marshy Swamp Planets")].RelatedItems.Add(encyclopediaItems[race.Name]);
                            break;
                        case HabitatType.Ocean:
                            encyclopediaItems[race.Name].RelatedItems.Add(encyclopediaItems[TextResolver.GetText("Ocean Planets")]);
                            encyclopediaItems[TextResolver.GetText("Ocean Planets")].RelatedItems.Add(encyclopediaItems[race.Name]);
                            break;
                        case HabitatType.Desert:
                            encyclopediaItems[race.Name].RelatedItems.Add(encyclopediaItems[TextResolver.GetText("Desert Planets")]);
                            encyclopediaItems[TextResolver.GetText("Desert Planets")].RelatedItems.Add(encyclopediaItems[race.Name]);
                            break;
                        case HabitatType.Ice:
                            encyclopediaItems[race.Name].RelatedItems.Add(encyclopediaItems[TextResolver.GetText("Ice Planets")]);
                            encyclopediaItems[TextResolver.GetText("Ice Planets")].RelatedItems.Add(encyclopediaItems[race.Name]);
                            break;
                        case HabitatType.Volcanic:
                            encyclopediaItems[race.Name].RelatedItems.Add(encyclopediaItems[TextResolver.GetText("Volcanic Planets")]);
                            encyclopediaItems[TextResolver.GetText("Volcanic Planets")].RelatedItems.Add(encyclopediaItems[race.Name]);
                            break;
                    }
                }
            }
            return encyclopediaItems;
        }

        public static EncyclopediaItemList AddGameInfoTopics(EncyclopediaItemList encyclopediaItems, string applicationStartupPath, string customizationSetName)
        {
            string text = applicationStartupPath + "\\help\\";
            string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\help\\";
            string text3 = "GameInfo_";
            List<FileInfo> list = new List<FileInfo>();
            if (!string.IsNullOrEmpty(customizationSetName) && Directory.Exists(text2))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(text2);
                list.AddRange(directoryInfo.GetFiles(text3 + "*.mht"));
            }
            if (Directory.Exists(text))
            {
                DirectoryInfo directoryInfo2 = new DirectoryInfo(text);
                list.AddRange(directoryInfo2.GetFiles(text3 + "*.mht"));
            }
            string text4 = text3 + "Default.mht";
            if (!File.Exists(text2 + text4) && !File.Exists(text + text4))
            {
                text4 = string.Empty;
            }
            if (list.Count > 0)
            {
                EncyclopediaItem encyclopediaItem = null;
                EncyclopediaItem encyclopediaItem2 = null;
                if (!string.IsNullOrEmpty(text4))
                {
                    encyclopediaItem = new EncyclopediaItem(TextResolver.GetText("Game Info"), text4, EncyclopediaCategory.GameInfo, isCategoryRoot: true);
                    encyclopediaItems.Add(encyclopediaItem);
                    encyclopediaItem2 = new EncyclopediaItem(TextResolver.GetText("Game Info"), text4, EncyclopediaCategory.GameInfo);
                    encyclopediaItems.Add(encyclopediaItem2);
                }
                else
                {
                    string name = list[0].Name;
                    int length = text3.Length;
                    string text5 = name.Substring(length, name.Length - length);
                    text5 = text5.Substring(0, text5.Length - 4);
                    text5 = SplitString(text5);
                    encyclopediaItem = new EncyclopediaItem(TextResolver.GetText("Game Info"), name, EncyclopediaCategory.GameInfo, isCategoryRoot: true);
                    encyclopediaItems.Add(encyclopediaItem);
                    encyclopediaItem2 = new EncyclopediaItem(text5, name, EncyclopediaCategory.GameInfo);
                    encyclopediaItems.Add(encyclopediaItem2);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    string name2 = list[i].Name;
                    if (name2 != encyclopediaItem.Filename)
                    {
                        int length2 = text3.Length;
                        string text6 = name2.Substring(length2, name2.Length - length2);
                        text6 = text6.Substring(0, text6.Length - 4);
                        text6 = SplitString(text6);
                        EncyclopediaItem encyclopediaItem3 = new EncyclopediaItem(text6, name2, EncyclopediaCategory.GameInfo, isCategoryRoot: false);
                        encyclopediaItems.Add(encyclopediaItem3);
                        encyclopediaItem3.RelatedItems.Add(encyclopediaItem2);
                        encyclopediaItem.RelatedItems.Add(encyclopediaItem3);
                        encyclopediaItem2.RelatedItems.Add(encyclopediaItem3);
                    }
                }
            }
            return encyclopediaItems;
        }

        public static EncyclopediaItemList AddThemeTopics(EncyclopediaItemList encyclopediaItems, string applicationStartupPath, string customizationSetName)
        {
            _ = applicationStartupPath + "\\help\\";
            string text = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\help\\";
            if (!string.IsNullOrEmpty(customizationSetName) && Directory.Exists(text))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(text);
                FileInfo[] files = directoryInfo.GetFiles(customizationSetName + "_*.mht");
                string text2 = customizationSetName + ".mht";
                if (!File.Exists(text + text2))
                {
                    text2 = string.Empty;
                    if (files.Length > 0)
                    {
                        text2 = files[0].Name;
                    }
                }
                if (!string.IsNullOrEmpty(text2))
                {
                    EncyclopediaItem encyclopediaItem = new EncyclopediaItem(string.Format(TextResolver.GetText("THEMENAME Theme"), customizationSetName), text2, EncyclopediaCategory.Theme, isCategoryRoot: true);
                    encyclopediaItems.Add(encyclopediaItem);
                    EncyclopediaItem encyclopediaItem2 = new EncyclopediaItem(string.Format(TextResolver.GetText("THEMENAME Theme"), customizationSetName), text2, EncyclopediaCategory.Theme);
                    encyclopediaItems.Add(encyclopediaItem2);
                    for (int i = 0; i < files.Length; i++)
                    {
                        string name = files[i].Name;
                        int num = customizationSetName.Length + 1;
                        string text3 = name.Substring(num, name.Length - num);
                        text3 = text3.Substring(0, text3.Length - 4);
                        text3 = SplitString(text3);
                        EncyclopediaItem encyclopediaItem3 = new EncyclopediaItem(text3, name, EncyclopediaCategory.Theme, isCategoryRoot: false);
                        encyclopediaItems.Add(encyclopediaItem3);
                        encyclopediaItem3.RelatedItems.Add(encyclopediaItem2);
                        encyclopediaItem.RelatedItems.Add(encyclopediaItem3);
                        encyclopediaItem2.RelatedItems.Add(encyclopediaItem3);
                    }
                }
            }
            return encyclopediaItems;
        }

        public static EncyclopediaItemList AddGovernmentTopics(EncyclopediaItemList encyclopediaItems, GovernmentAttributesList governments, string applicationStartupPath, string customizationSetName)
        {
            string text = applicationStartupPath + "\\help\\";
            string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\help\\";
            for (int i = 0; i < governments.Count; i++)
            {
                GovernmentAttributes governmentAttributes = governments[i];
                if (governmentAttributes != null)
                {
                    string text3 = "GameConcepts_GovernmentTypes_" + RemoveSpecialCharacters(governmentAttributes.Name) + ".mht";
                    if (File.Exists(text2 + text3) || File.Exists(text + text3))
                    {
                        encyclopediaItems.Add(new EncyclopediaItem(governmentAttributes.Name, text3, EncyclopediaCategory.GovernmentTypes, isCategoryRoot: false));
                        encyclopediaItems[TextResolver.GetText("Government Types")].RelatedItems.Add(encyclopediaItems[governmentAttributes.Name]);
                    }
                }
            }
            return encyclopediaItems;
        }

        public static EncyclopediaItemList AddResourceTopics(EncyclopediaItemList encyclopediaItems, ResourceSystem resourceSystem, string applicationStartupPath, string customizationSetName)
        {
            string text = applicationStartupPath + "\\help\\";
            string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\help\\";
            for (int i = 0; i < resourceSystem.Resources.Count; i++)
            {
                ResourceDefinition resourceDefinition = resourceSystem.Resources[i];
                if (resourceDefinition == null)
                {
                    continue;
                }
                string text3 = "Resource_" + RemoveSpecialCharacters(resourceDefinition.Name) + ".mht";
                if (File.Exists(text2 + text3) || File.Exists(text + text3))
                {
                    encyclopediaItems.Add(new EncyclopediaItem(resourceDefinition.Name, text3, EncyclopediaCategory.Resources, isCategoryRoot: false));
                    encyclopediaItems[resourceDefinition.Name].RelatedItems.Add(encyclopediaItems[TextResolver.GetText("Resources")]);
                    if (resourceDefinition.IsFuel)
                    {
                        encyclopediaItems[resourceDefinition.Name].RelatedItems.Add(encyclopediaItems[TextResolver.GetText("Fuel")]);
                        encyclopediaItems[TextResolver.GetText("Fuel")].RelatedItems.Add(encyclopediaItems[resourceDefinition.Name]);
                    }
                }
            }
            return encyclopediaItems;
        }

        public GalaxySummary GenerateSummary()
        {
            GalaxySummary galaxySummary = new GalaxySummary();
            galaxySummary.StarCount = StarCount;
            galaxySummary.SectorWidth = SectorWidth;
            galaxySummary.SectorHeight = SectorHeight;
            galaxySummary.Shape = GalaxyShape;
            galaxySummary.Title = Title;
            galaxySummary.Description = Description;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active && empire.PlayableInScenario)
                {
                    galaxySummary.EmpireSummaries.Add(EmpireSummary.GenerateSummaryFromEmpire(empire));
                }
            }
            for (int j = 0; j < PirateEmpires.Count; j++)
            {
                Empire empire2 = PirateEmpires[j];
                if (empire2 != null && empire2.Active && empire2.PlayableInScenario)
                {
                    galaxySummary.EmpireSummaries.Add(EmpireSummary.GenerateSummaryFromEmpire(empire2));
                }
            }
            return galaxySummary;
        }

        public ComponentCategoryType SelectRandomComponentCategory(IndustryType industry)
        {
            switch (industry)
            {
                case IndustryType.Energy:
                    switch (Rnd.Next(0, 6))
                    {
                        case 0:
                            return ComponentCategoryType.Shields;
                        case 1:
                            return ComponentCategoryType.Engine;
                        case 2:
                            return ComponentCategoryType.HyperDrive;
                        case 3:
                            return ComponentCategoryType.Reactor;
                        case 4:
                            return ComponentCategoryType.EnergyCollector;
                        case 5:
                            return ComponentCategoryType.Extractor;
                    }
                    break;
                case IndustryType.HighTech:
                    switch (Rnd.Next(0, 7))
                    {
                        case 0:
                            return ComponentCategoryType.Sensor;
                        case 1:
                            return ComponentCategoryType.Computer;
                        case 2:
                            return ComponentCategoryType.Labs;
                        case 3:
                            return ComponentCategoryType.Habitation;
                        case 4:
                            return ComponentCategoryType.Manufacturer;
                        case 5:
                            return ComponentCategoryType.Storage;
                        case 6:
                            return ComponentCategoryType.Construction;
                    }
                    break;
                case IndustryType.Weapon:
                    switch (Rnd.Next(0, 5))
                    {
                        case 0:
                            return ComponentCategoryType.Armor;
                        case 1:
                            return ComponentCategoryType.WeaponArea;
                        case 2:
                            return ComponentCategoryType.WeaponBeam;
                        case 3:
                            return ComponentCategoryType.WeaponTorpedo;
                        case 4:
                            return ComponentCategoryType.Fighter;
                    }
                    break;
            }
            return ComponentCategoryType.WeaponBeam;
        }

        public ComponentCategoryType SelectRandomComponentCategory()
        {
            ComponentCategoryType result = ComponentCategoryType.Undefined;
            switch (Rnd.Next(0, 17))
            {
                case 0:
                    result = ComponentCategoryType.WeaponBeam;
                    break;
                case 1:
                    result = ComponentCategoryType.WeaponArea;
                    break;
                case 2:
                    result = ComponentCategoryType.WeaponTorpedo;
                    break;
                case 3:
                    result = ComponentCategoryType.Armor;
                    break;
                case 4:
                    result = ComponentCategoryType.Shields;
                    break;
                case 5:
                    result = ComponentCategoryType.Engine;
                    break;
                case 6:
                    result = ComponentCategoryType.HyperDrive;
                    break;
                case 7:
                    result = ComponentCategoryType.Reactor;
                    break;
                case 8:
                    result = ComponentCategoryType.EnergyCollector;
                    break;
                case 9:
                    result = ComponentCategoryType.Extractor;
                    break;
                case 10:
                    result = ComponentCategoryType.Manufacturer;
                    break;
                case 11:
                    result = ComponentCategoryType.Storage;
                    break;
                case 12:
                    result = ComponentCategoryType.Sensor;
                    break;
                case 13:
                    result = ComponentCategoryType.Computer;
                    break;
                case 14:
                    result = ComponentCategoryType.Labs;
                    break;
                case 15:
                    result = ComponentCategoryType.Construction;
                    break;
                case 16:
                    result = ComponentCategoryType.Habitation;
                    break;
            }
            return result;
        }

        public short GetMatchingGameEventIdDiplomaticRelationChange(Empire empire1, Empire empire2, DiplomaticRelationType relationType)
        {
            for (int i = 0; i < GameEvents.Count; i++)
            {
                GameEvent gameEvent = GameEvents[i];
                if (gameEvent != null && !gameEvent.HasBeenTriggered && gameEvent.TriggerType == EventTriggerType.DiplomaticRelationChange && gameEvent.Empire == empire1 && gameEvent.EmpireOther == empire2 && gameEvent.DiplomaticRelationType == relationType)
                {
                    return gameEvent.GameEventId;
                }
            }
            return -1;
        }

        public short GetMatchingGameEventIdEmpireEncounter(Empire empire1, Empire empire2)
        {
            for (int i = 0; i < GameEvents.Count; i++)
            {
                GameEvent gameEvent = GameEvents[i];
                if (gameEvent != null && !gameEvent.HasBeenTriggered && gameEvent.TriggerType == EventTriggerType.EmpireEncounter && gameEvent.Empire == empire1 && gameEvent.EmpireOther == empire2)
                {
                    return gameEvent.GameEventId;
                }
            }
            return -1;
        }

        public short GetMatchingGameEventIdResearchBreakthrough(Empire empire1, int researchProjectId)
        {
            for (int i = 0; i < GameEvents.Count; i++)
            {
                GameEvent gameEvent = GameEvents[i];
                if (gameEvent != null && !gameEvent.HasBeenTriggered && gameEvent.TriggerType == EventTriggerType.ResearchBreakthrough && gameEvent.Empire == empire1 && gameEvent.ResearchProjectId == researchProjectId)
                {
                    return gameEvent.GameEventId;
                }
            }
            return -1;
        }

        public short GetMatchingGameEventIdPlanetDestroyerConstructionCompleted(Empire empire1)
        {
            for (int i = 0; i < GameEvents.Count; i++)
            {
                GameEvent gameEvent = GameEvents[i];
                if (gameEvent != null && !gameEvent.HasBeenTriggered && gameEvent.TriggerType == EventTriggerType.PlanetDestroyerConstructionCompleted && gameEvent.Empire == empire1)
                {
                    return gameEvent.GameEventId;
                }
            }
            return -1;
        }

        public short GetMatchingGameEventIdEmpireEliminated(Empire empire, Empire eliminatingEmpire)
        {
            for (int i = 0; i < GameEvents.Count; i++)
            {
                GameEvent gameEvent = GameEvents[i];
                if (gameEvent != null && !gameEvent.HasBeenTriggered && gameEvent.TriggerType == EventTriggerType.EmpireEliminated && gameEvent.Empire == empire && (gameEvent.EmpireOther == null || gameEvent.EmpireOther == eliminatingEmpire))
                {
                    return gameEvent.GameEventId;
                }
            }
            return -1;
        }

        public short GetMatchingGameEventIdCharacterAppears(Character character)
        {
            for (int i = 0; i < GameEvents.Count; i++)
            {
                GameEvent gameEvent = GameEvents[i];
                if (gameEvent != null && !gameEvent.HasBeenTriggered && gameEvent.TriggerType == EventTriggerType.CharacterAppears && gameEvent.Character == character)
                {
                    return gameEvent.GameEventId;
                }
            }
            return -1;
        }

        public short GetMatchingGameEventIdCharacterKilled(Character character)
        {
            for (int i = 0; i < GameEvents.Count; i++)
            {
                GameEvent gameEvent = GameEvents[i];
                if (gameEvent != null && !gameEvent.HasBeenTriggered && gameEvent.TriggerType == EventTriggerType.CharacterKilled && gameEvent.Character == character)
                {
                    return gameEvent.GameEventId;
                }
            }
            return -1;
        }

        public bool CheckTriggerEvent(short gameEventId, Empire triggerEmpire, EventTriggerType triggerType, object additionalData)
        {
            if (gameEventId >= 0)
            {
                GameEvent byId = GameEvents.GetById(gameEventId);
                if (byId != null && !byId.HasBeenTriggered && byId.TriggerType == triggerType)
                {
                    switch (triggerType)
                    {
                        case EventTriggerType.Investigate:
                            {
                                if (byId.TriggerRuin != null)
                                {
                                    byId.TriggerRuin.GameEventId *= -1;
                                    DoGameEvent(byId, triggerEmpire);
                                    if (byId.HasBeenTriggered)
                                    {
                                        return true;
                                    }
                                    byId.TriggerRuin.GameEventId = Math.Abs(byId.TriggerRuin.GameEventId);
                                    return false;
                                }
                                if (byId.TriggerObject == null || !(byId.TriggerObject is BuiltObject))
                                {
                                    break;
                                }
                                BuiltObject builtObject2 = (BuiltObject)byId.TriggerObject;
                                if (builtObject2.Owner == null)
                                {
                                    byId.TriggerObject.GameEventId *= -1;
                                    DoGameEvent(byId, triggerEmpire);
                                    if (byId.HasBeenTriggered)
                                    {
                                        return true;
                                    }
                                    byId.TriggerObject.GameEventId = Math.Abs(byId.TriggerObject.GameEventId);
                                    return false;
                                }
                                break;
                            }
                        case EventTriggerType.DiplomaticRelationChange:
                        case EventTriggerType.EmpireEncounter:
                        case EventTriggerType.ResearchBreakthrough:
                        case EventTriggerType.PlanetDestroyerConstructionCompleted:
                        case EventTriggerType.EmpireEliminated:
                        case EventTriggerType.CharacterAppears:
                        case EventTriggerType.CharacterKilled:
                            DoGameEvent(byId, triggerEmpire);
                            if (byId.HasBeenTriggered)
                            {
                                return true;
                            }
                            break;
                        case EventTriggerType.Destroy:
                            if (byId.TriggerObject != null)
                            {
                                byId.TriggerObject.GameEventId *= -1;
                                DoGameEvent(byId, triggerEmpire);
                                if (byId.HasBeenTriggered)
                                {
                                    return true;
                                }
                                byId.TriggerObject.GameEventId = Math.Abs(byId.TriggerObject.GameEventId);
                                return false;
                            }
                            break;
                        case EventTriggerType.Capture:
                            if (byId.TriggerObject != null)
                            {
                                byId.TriggerObject.GameEventId *= -1;
                                DoGameEvent(byId, triggerEmpire);
                                if (byId.HasBeenTriggered)
                                {
                                    return true;
                                }
                                byId.TriggerObject.GameEventId = Math.Abs(byId.TriggerObject.GameEventId);
                                return false;
                            }
                            break;
                        case EventTriggerType.Build:
                            if (byId.TriggerObject == null || !(byId.TriggerObject is Habitat))
                            {
                                break;
                            }
                            if (byId.TriggerFacility != null)
                            {
                                if (additionalData == null || !(additionalData is PlanetaryFacility))
                                {
                                    break;
                                }
                                PlanetaryFacility planetaryFacility = (PlanetaryFacility)additionalData;
                                if (planetaryFacility.PlanetaryFacilityDefinitionId == byId.TriggerFacility.PlanetaryFacilityDefinitionId)
                                {
                                    byId.TriggerObject.GameEventId *= -1;
                                    DoGameEvent(byId, triggerEmpire);
                                    if (byId.HasBeenTriggered)
                                    {
                                        return true;
                                    }
                                    byId.TriggerObject.GameEventId = Math.Abs(byId.TriggerObject.GameEventId);
                                    return false;
                                }
                            }
                            else
                            {
                                if (byId.TriggerBuiltObjectSubRole == BuiltObjectSubRole.Undefined || additionalData == null || !(additionalData is BuiltObject))
                                {
                                    break;
                                }
                                BuiltObject builtObject = (BuiltObject)additionalData;
                                if (builtObject.SubRole == byId.TriggerBuiltObjectSubRole)
                                {
                                    byId.TriggerObject.GameEventId *= -1;
                                    DoGameEvent(byId, triggerEmpire);
                                    if (byId.HasBeenTriggered)
                                    {
                                        return true;
                                    }
                                    byId.TriggerObject.GameEventId = Math.Abs(byId.TriggerObject.GameEventId);
                                    return false;
                                }
                            }
                            break;
                    }
                }
            }
            return false;
        }

        private void DoGameEvent(short gameEventId, Empire triggerEmpire)
        {
            if (gameEventId >= 0 && gameEventId < GameEvents.Count)
            {
                GameEvent byId = GameEvents.GetById(gameEventId);
                DoGameEvent(byId, triggerEmpire);
            }
        }

        private void DoGameEvent(GameEvent gameEvent, Empire triggerEmpire)
        {
            if (gameEvent == null || gameEvent.HasBeenTriggered || (gameEvent.CanOnlyBeTriggeredByPlayer && triggerEmpire != PlayerEmpire))
            {
                return;
            }
            gameEvent.HasBeenTriggered = true;
            if (gameEvent.Actions == null || gameEvent.Actions.Count <= 0)
            {
                return;
            }
            long currentStarDate = CurrentStarDate;
            if (gameEvent.Actions.Count > 1)
            {
                if (gameEvent.Actions.ExecutionType == MultipleEventActionType.ExecuteSingleRandomAction)
                {
                    int index = Rnd.Next(0, gameEvent.Actions.Count);
                    ExecuteOrDelayEventAction(gameEvent.Actions[index], triggerEmpire, gameEvent, currentStarDate);
                    return;
                }
                for (int i = 0; i < gameEvent.Actions.Count; i++)
                {
                    ExecuteOrDelayEventAction(gameEvent.Actions[i], triggerEmpire, gameEvent, currentStarDate);
                }
            }
            else
            {
                ExecuteOrDelayEventAction(gameEvent.Actions[0], triggerEmpire, gameEvent, currentStarDate);
            }
        }

        public void ExecuteOrDelayEventAction(EventAction eventAction, Empire triggerEmpire, GameEvent gameEvent, long starDate)
        {
            if (eventAction != null)
            {
                long num = RealSecondsInGalacticYear * 1000 / 360;
                switch (eventAction.ExecutionType)
                {
                    case EventActionExecutionType.Immediately:
                        ExecuteEventAction(eventAction, triggerEmpire, gameEvent);
                        break;
                    case EventActionExecutionType.Delay:
                        {
                            long num4 = (eventAction.ExecutionDate = starDate + eventAction.DelayDaysMinimum * num);
                            AddDelayedEventAction(eventAction, triggerEmpire, gameEvent);
                            break;
                        }
                    case EventActionExecutionType.RandomDelay:
                        {
                            long num2 = Rnd.Next(0, Math.Max(0, eventAction.DelayDaysMaximum - eventAction.DelayDaysMinimum));
                            long num3 = (eventAction.ExecutionDate = starDate + (eventAction.DelayDaysMinimum + num2) * num);
                            AddDelayedEventAction(eventAction, triggerEmpire, gameEvent);
                            break;
                        }
                }
            }
        }

        private void AddDelayedEventAction(EventAction eventAction, Empire triggerEmpire, GameEvent gameEvent)
        {
            lock (_DelayedActionLockObject)
            {
                DelayedActions.Add(new EventActionExecutionPackage(eventAction, gameEvent, triggerEmpire));
            }
        }

        private void ProcessDelayedEventActions(long starDate)
        {
            if (DelayedActions.Count <= 0)
            {
                return;
            }
            EventActionExecutionPackageList eventActionExecutionPackageList = new EventActionExecutionPackageList();
            EventActionExecutionPackage[] array = new EventActionExecutionPackage[0];
            lock (_DelayedActionLockObject)
            {
                array = ListHelper.ToArrayThreadSafe(DelayedActions);
            }
            foreach (EventActionExecutionPackage eventActionExecutionPackage in array)
            {
                if (eventActionExecutionPackage != null && eventActionExecutionPackage.Action != null && eventActionExecutionPackage.Action.ExecutionDate <= starDate)
                {
                    ExecuteEventAction(eventActionExecutionPackage.Action, eventActionExecutionPackage.TriggerEmpire, eventActionExecutionPackage.GameEvent);
                    eventActionExecutionPackageList.Add(eventActionExecutionPackage);
                }
            }
            lock (_DelayedActionLockObject)
            {
                for (int j = 0; j < eventActionExecutionPackageList.Count; j++)
                {
                    DelayedActions.Remove(eventActionExecutionPackageList[j]);
                }
            }
        }

        private void ExecuteEventAction(EventAction eventAction, Empire triggerEmpire, GameEvent gameEvent)
        {
            Empire empire = null;
            bool flag = false;
            string title = string.Empty;
            string text = string.Empty;
            object additionalData = null;
            object location = null;
            BuiltObject builtObject = null;
            Habitat habitat = null;
            Creature creature = null;
            switch (eventAction.Type)
            {
                case EventActionType.AcquireBuiltObject:
                    if (eventAction.Target == null || !(eventAction.Target is BuiltObject))
                    {
                        break;
                    }
                    builtObject = (BuiltObject)eventAction.Target;
                    if (!builtObject.HasBeenDestroyed && builtObject.ActualEmpire != triggerEmpire && triggerEmpire != null)
                    {
                        triggerEmpire.TakeOwnershipOfBuiltObject(builtObject, triggerEmpire, setDesignAsObsolete: true, removeFromFleet: true);
                        string text3 = string.Empty;
                        if (builtObject.NearestSystemStar != null)
                        {
                            text3 = builtObject.NearestSystemStar.Name;
                        }
                        if (builtObject.Role == BuiltObjectRole.Base)
                        {
                            text = string.Format(TextResolver.GetText("GameEventAction Description AcquireBuiltObject Base"), builtObject.Name, text3);
                            title = TextResolver.GetText("GameEventAction Title AcquireBuiltObject Base");
                        }
                        else
                        {
                            text = string.Format(TextResolver.GetText("GameEventAction Description AcquireBuiltObject Ship"), ResolveDescription(builtObject.SubRole).ToLower(CultureInfo.InvariantCulture), builtObject.Name, text3);
                            title = TextResolver.GetText("GameEventAction Title AcquireBuiltObject Ship");
                        }
                        additionalData = builtObject;
                        location = builtObject;
                        flag = true;
                    }
                    break;
                case EventActionType.AcquireHabitat:
                    if (eventAction.Target != null && eventAction.Target is Habitat)
                    {
                        habitat = (Habitat)eventAction.Target;
                        if (!habitat.HasBeenDestroyed && habitat.Empire != triggerEmpire && habitat.Population != null && habitat.Population.TotalAmount > 0 && triggerEmpire != null)
                        {
                            triggerEmpire.TakeOwnershipOfColony(habitat, triggerEmpire, destroyBases: false, destroyTroops: false);
                            Habitat habitat23 = DetermineHabitatSystemStar(habitat);
                            text = string.Format(TextResolver.GetText("GameEventAction Description AcquireHabitat"), ResolveDescription(habitat.Type).ToLower(CultureInfo.InvariantCulture), habitat.Name, habitat23.Name);
                            title = TextResolver.GetText("GameEventAction Title AcquireHabitat");
                            additionalData = habitat;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.BuildPlanetaryFacility:
                    if (eventAction.Target != null && eventAction.Target is Habitat && ((Habitat)eventAction.Target).Population != null && ((Habitat)eventAction.Target).Population.TotalAmount > 0)
                    {
                        habitat = (Habitat)eventAction.Target;
                        if (eventAction.Value >= 0 && eventAction.Value < PlanetaryFacilityDefinitions.Count)
                        {
                            Habitat habitat13 = DetermineHabitatSystemStar(habitat);
                            PlanetaryFacilityDefinition planetaryFacilityDefinition = PlanetaryFacilityDefinitions[eventAction.Value];
                            if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.Wonder)
                            {
                                habitat.QueueWonderConstruction(planetaryFacilityDefinition, fullyConstructed: true);
                            }
                            else
                            {
                                habitat.QueueFacilityConstruction(planetaryFacilityDefinition.Type, fullyConstructed: true);
                            }
                            text = string.Format(TextResolver.GetText("GameEventAction Description BuildPlanetaryFacility"), planetaryFacilityDefinition.Name, habitat.Name, habitat13.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title BuildPlanetaryFacility"), planetaryFacilityDefinition.Name);
                            additionalData = planetaryFacilityDefinition;
                            location = habitat;
                            flag = true;
                        }
                    }
                    else
                    {
                        if (triggerEmpire == null)
                        {
                            break;
                        }
                        if (triggerEmpire.Capital != null)
                        {
                            habitat = triggerEmpire.Capital;
                        }
                        else
                        {
                            HabitatList ownedColonies = triggerEmpire.Colonies.GetOwnedColonies(triggerEmpire);
                            if (ownedColonies.Count > 0)
                            {
                                ownedColonies.Sort();
                                habitat = ownedColonies[0];
                            }
                        }
                        if (habitat != null && eventAction.Value >= 0 && eventAction.Value < PlanetaryFacilityDefinitions.Count)
                        {
                            Habitat habitat14 = DetermineHabitatSystemStar(habitat);
                            PlanetaryFacilityDefinition planetaryFacilityDefinition2 = PlanetaryFacilityDefinitions[eventAction.Value];
                            habitat.QueueFacilityConstruction(planetaryFacilityDefinition2.Type, fullyConstructed: true);
                            text = string.Format(TextResolver.GetText("GameEventAction Description BuildPlanetaryFacility"), planetaryFacilityDefinition2.Name, habitat.Name, habitat14.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title BuildPlanetaryFacility"), planetaryFacilityDefinition2.Name);
                            additionalData = planetaryFacilityDefinition2;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.DestroyPlanetaryFacility:
                    if (eventAction.Target != null && eventAction.Target is Habitat && ((Habitat)eventAction.Target).Population != null && ((Habitat)eventAction.Target).Population.TotalAmount > 0)
                    {
                        habitat = (Habitat)eventAction.Target;
                        if (eventAction.Value >= 0 && eventAction.Value < PlanetaryFacilityDefinitions.Count)
                        {
                            PlanetaryFacility byId = habitat.Facilities.GetById(PlanetaryFacilityDefinitions[eventAction.Value].PlanetaryFacilityDefinitionId);
                            if (byId != null)
                            {
                                Habitat habitat24 = DetermineHabitatSystemStar(habitat);
                                habitat.Facilities.Remove(byId);
                                habitat.CheckRemoveFacilityTracking(byId);
                                habitat.ReviewPlanetaryFacilities(habitat.Empire);
                                text = string.Format(TextResolver.GetText("GameEventAction Description DestroyPlanetaryFacility"), byId.Name, habitat.Name, habitat24.Name);
                                title = string.Format(TextResolver.GetText("GameEventAction Title DestroyPlanetaryFacility"), byId.Name);
                                additionalData = byId;
                                location = habitat;
                                flag = true;
                            }
                        }
                    }
                    else
                    {
                        if (triggerEmpire == null || eventAction.Value < 0 || eventAction.Value >= PlanetaryFacilityDefinitions.Count)
                        {
                            break;
                        }
                        HabitatList ownedColonies2 = triggerEmpire.Colonies.GetOwnedColonies(triggerEmpire);
                        if (ownedColonies2.Count > 0)
                        {
                            habitat = ownedColonies2.FindColonyWithFacilityId(PlanetaryFacilityDefinitions[eventAction.Value].PlanetaryFacilityDefinitionId);
                        }
                        if (habitat != null)
                        {
                            PlanetaryFacility byId2 = habitat.Facilities.GetById(PlanetaryFacilityDefinitions[eventAction.Value].PlanetaryFacilityDefinitionId);
                            if (byId2 != null)
                            {
                                Habitat habitat25 = DetermineHabitatSystemStar(habitat);
                                habitat.Facilities.Remove(byId2);
                                habitat.CheckRemoveFacilityTracking(byId2);
                                habitat.ReviewPlanetaryFacilities(habitat.Empire);
                                text = string.Format(TextResolver.GetText("GameEventAction Description DestroyPlanetaryFacility"), byId2.Name, habitat.Name, habitat25.Name);
                                title = string.Format(TextResolver.GetText("GameEventAction Title DestroyPlanetaryFacility"), byId2.Name);
                                additionalData = byId2;
                                location = habitat;
                                flag = true;
                            }
                        }
                    }
                    break;
                case EventActionType.ChangeEmpireGovernment:
                    if (eventAction.Empire != null && eventAction.Value >= 0 && eventAction.Value < Governments.Count)
                    {
                        GovernmentAttributes governmentAttributes2 = Governments[eventAction.Value];
                        eventAction.Empire.ChangeGovernment(governmentAttributes2.GovernmentId);
                        text = string.Format(TextResolver.GetText("GameEventAction Description ChangeEmpireGovernment"), eventAction.Empire.Name, governmentAttributes2.Name);
                        title = TextResolver.GetText("GameEventAction Title ChangeEmpireGovernment");
                        additionalData = eventAction.Empire;
                        location = eventAction.Empire.Capital;
                        flag = true;
                    }
                    break;
                case EventActionType.ChangeRaceBias:
                    {
                        if (eventAction.Race == null || eventAction.RaceOther == null || eventAction.Race == eventAction.RaceOther)
                        {
                            break;
                        }
                        int bias = eventAction.Race.Biases.GetBias(eventAction.RaceOther);
                        bias += eventAction.Value;
                        eventAction.Race.Biases.SetBias(eventAction.RaceOther.Name, bias);
                        for (int n = 0; n < Empires.Count; n++)
                        {
                            Empire empire7 = Empires[n];
                            if (empire7 == null || !empire7.Active || empire7.EmpireEvaluations == null || empire7.DominantRace == null || empire7.DominantRace != eventAction.Race)
                            {
                                continue;
                            }
                            for (int num8 = 0; num8 < empire7.EmpireEvaluations.Count; num8++)
                            {
                                EmpireEvaluation empireEvaluation2 = empire7.EmpireEvaluations[num8];
                                if (empireEvaluation2 != null && empireEvaluation2.Empire != null && empireEvaluation2.Empire.DominantRace != null && empireEvaluation2.Empire.DominantRace == eventAction.RaceOther)
                                {
                                    empireEvaluation2.Bias = empireEvaluation2.BiasRaw + (double)eventAction.Value;
                                }
                            }
                        }
                        text = string.Format(TextResolver.GetText("GameEventAction Description ChangeRaceBias"), eventAction.Race.Name, eventAction.RaceOther.Name, eventAction.Value.ToString("0"));
                        title = TextResolver.GetText("GameEventAction Title ChangeRaceBias");
                        additionalData = eventAction.Race;
                        location = null;
                        flag = true;
                        break;
                    }
                case EventActionType.RevealObject:
                    if (eventAction.Target == null || triggerEmpire == null)
                    {
                        break;
                    }
                    if (eventAction.Target is BuiltObject)
                    {
                        builtObject = (BuiltObject)eventAction.Target;
                        if (triggerEmpire == PlayerEmpire && this.LocationPinged != null)
                        {
                            this.LocationPinged(builtObject, new EventArgs());
                        }
                        string text2 = string.Empty;
                        if (builtObject.NearestSystemStar != null)
                        {
                            text2 = builtObject.NearestSystemStar.Name;
                        }
                        if (builtObject.Role == BuiltObjectRole.Base)
                        {
                            text = string.Format(TextResolver.GetText("GameEventAction Description RevealObject Base"), builtObject.Name, text2);
                            title = TextResolver.GetText("GameEventAction Title RevealObject Base");
                        }
                        else
                        {
                            text = string.Format(TextResolver.GetText("GameEventAction Description RevealObject Ship"), ResolveDescription(builtObject.SubRole), builtObject.Name, text2);
                            title = TextResolver.GetText("GameEventAction Title RevealObject Ship");
                        }
                        additionalData = builtObject;
                        location = builtObject;
                        flag = true;
                    }
                    else if (eventAction.Target is Habitat)
                    {
                        habitat = (Habitat)eventAction.Target;
                        Habitat habitat6 = DetermineHabitatSystemStar(habitat);
                        SystemVisibilityStatus systemVisibilityStatus = triggerEmpire.CheckSystemVisibilityStatus(habitat6);
                        if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored || systemVisibilityStatus == SystemVisibilityStatus.Undefined)
                        {
                            triggerEmpire.SetSystemVisibility(habitat6, SystemVisibilityStatus.Explored);
                        }
                        if (triggerEmpire == PlayerEmpire && this.LocationPinged != null)
                        {
                            this.LocationPinged(habitat, new EventArgs());
                        }
                        text = string.Format(TextResolver.GetText("GameEventAction Description RevealObject Planet"), ResolveDescription(habitat.Category), habitat.Name, habitat6.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title RevealObject Planet"), ResolveDescription(habitat.Category));
                        additionalData = habitat;
                        location = habitat;
                        flag = true;
                    }
                    break;
                case EventActionType.DestroyBuiltObject:
                    if (eventAction.Target == null || !(eventAction.Target is BuiltObject))
                    {
                        break;
                    }
                    builtObject = (BuiltObject)eventAction.Target;
                    if (!builtObject.HasBeenDestroyed)
                    {
                        builtObject.InflictDamage(builtObject, null, 1000000.0, CurrentDateTime, this, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                        string text4 = string.Empty;
                        if (builtObject.NearestSystemStar != null)
                        {
                            text4 = builtObject.NearestSystemStar.Name;
                        }
                        if (builtObject.Role == BuiltObjectRole.Base)
                        {
                            text = string.Format(TextResolver.GetText("GameEventAction Description DestroyBuiltObject Base"), builtObject.Name, text4);
                            title = TextResolver.GetText("GameEventAction Title DestroyBuiltObject Base");
                        }
                        else
                        {
                            text = string.Format(TextResolver.GetText("GameEventAction Description DestroyBuiltObject Ship"), ResolveDescription(builtObject.SubRole).ToLower(CultureInfo.InvariantCulture), builtObject.Name, text4);
                            title = TextResolver.GetText("GameEventAction Title DestroyBuiltObject Ship");
                        }
                        additionalData = builtObject;
                        location = builtObject;
                        flag = true;
                    }
                    break;
                case EventActionType.DisasterAtColony:
                    if (eventAction.Target != null && eventAction.Target is Habitat)
                    {
                        habitat = (Habitat)eventAction.Target;
                        if (!habitat.HasBeenDestroyed && habitat.Population != null && habitat.Population.TotalAmount > 0 && habitat.Empire != null)
                        {
                            Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                            habitat.Empire.EmpireEventColonyNaturalDisaster(habitat);
                            text = string.Format(TextResolver.GetText("GameEventAction Description DisasterAtColony"), habitat.Name, habitat2.Name);
                            title = TextResolver.GetText("GameEventAction Title DisasterAtColony");
                            additionalData = habitat;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.EmpireDeclaresWarOnTriggerEmpire:
                    if (eventAction.Empire != null && eventAction.Empire.PirateEmpireBaseHabitat == null && triggerEmpire != null && triggerEmpire.PirateEmpireBaseHabitat == null && eventAction.Empire != triggerEmpire)
                    {
                        eventAction.Empire.DeclareWar(triggerEmpire);
                        text = string.Format(TextResolver.GetText("GameEventAction Description EmpireDeclaresWarOnTriggerEmpire"), eventAction.Empire.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title EmpireDeclaresWarOnTriggerEmpire"), eventAction.Empire.Name);
                        additionalData = eventAction.Empire;
                        location = null;
                        flag = true;
                    }
                    break;
                case EventActionType.PirateFactionJoinsTriggerEmpire:
                    if (eventAction.Empire != null && triggerEmpire != null && eventAction.Empire != triggerEmpire && eventAction.Empire.PirateEmpireBaseHabitat != null)
                    {
                        PirateFactionJoinsEmpire(triggerEmpire, eventAction.Empire);
                        text = string.Format(TextResolver.GetText("GameEventAction Description PirateFactionJoinsTriggerEmpire"), eventAction.Empire.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title PirateFactionJoinsTriggerEmpire"), eventAction.Empire.Name);
                        additionalData = eventAction.Empire;
                        location = null;
                        flag = true;
                    }
                    break;
                case EventActionType.StartPlague:
                    if (eventAction.Target != null && eventAction.Target is Habitat)
                    {
                        habitat = (Habitat)eventAction.Target;
                        if (habitat.Population != null && habitat.Population.TotalAmount > 0 && habitat.Owner != null)
                        {
                            Habitat habitat10 = DetermineHabitatSystemStar(habitat);
                            habitat.Owner.EmpireEventPlague(habitat);
                            text = string.Format(TextResolver.GetText("GameEventAction Description StartPlague"), habitat.Name, habitat10.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title StartPlague"), habitat.Name);
                            additionalData = habitat;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.EndPlague:
                    if (eventAction.Target != null && eventAction.Target is Habitat)
                    {
                        habitat = (Habitat)eventAction.Target;
                        if (habitat.Population != null && habitat.Population.TotalAmount > 0 && habitat.Owner != null && habitat.PlagueTimeRemaining > 0f && habitat.PlagueId >= 0)
                        {
                            Plague plague = Plagues[habitat.PlagueId];
                            if (plague != null)
                            {
                                Habitat habitat15 = DetermineHabitatSystemStar(habitat);
                                text = string.Format(TextResolver.GetText("GameEventAction Description EndPlague"), plague.Name, habitat.Name, habitat15.Name);
                                habitat.PlagueId = -1;
                                habitat.PlagueTimeRemaining = 0f;
                                title = string.Format(TextResolver.GetText("GameEventAction Title EndPlague"), habitat.Name);
                                additionalData = habitat;
                                location = habitat;
                                flag = true;
                            }
                        }
                    }
                    else
                    {
                        if (triggerEmpire == null || triggerEmpire.Colonies == null)
                        {
                            break;
                        }
                        for (int j = 0; j < triggerEmpire.Colonies.Count; j++)
                        {
                            Habitat habitat16 = triggerEmpire.Colonies[j];
                            if (habitat16 != null && !habitat16.HasBeenDestroyed && habitat16.PlagueTimeRemaining > 0f && habitat16.PlagueId >= 0)
                            {
                                Plague plague2 = Plagues[habitat.PlagueId];
                                if (plague2 != null)
                                {
                                    Habitat habitat17 = DetermineHabitatSystemStar(habitat);
                                    text = string.Format(TextResolver.GetText("GameEventAction Description EndPlague"), plague2.Name, habitat.Name, habitat17.Name);
                                    habitat16.PlagueId = -1;
                                    habitat16.PlagueTimeRemaining = 0f;
                                    title = string.Format(TextResolver.GetText("GameEventAction Title EndPlague"), habitat.Name);
                                    additionalData = habitat;
                                    location = habitat;
                                    flag = true;
                                }
                            }
                        }
                    }
                    break;
                case EventActionType.EnemyFleetDefectsToTriggerEmpire:
                    {
                        if (triggerEmpire == null)
                        {
                            break;
                        }
                        if (triggerEmpire.PirateEmpireBaseHabitat == null)
                        {
                            EmpireEvaluation lowestEvaluationKnownEmpire = triggerEmpire.EmpireEvaluations.GetLowestEvaluationKnownEmpire(triggerEmpire, new EmpireList());
                            if (lowestEvaluationKnownEmpire != null && lowestEvaluationKnownEmpire.Empire != triggerEmpire)
                            {
                                ShipGroup shipGroup2 = lowestEvaluationKnownEmpire.Empire.EmpireEventRogueFleetDefects(triggerEmpire);
                                if (shipGroup2 != null)
                                {
                                    text = string.Format(TextResolver.GetText("GameEventAction Description EnemyFleetDefectsToTriggerEmpire"), shipGroup2.Name, lowestEvaluationKnownEmpire.Empire.Name);
                                    title = TextResolver.GetText("GameEventAction Title EnemyFleetDefectsToTriggerEmpire");
                                    additionalData = shipGroup2;
                                    location = null;
                                    flag = true;
                                }
                            }
                            break;
                        }
                        PirateRelation relationWithLowestEvaluation = triggerEmpire.PirateRelations.GetRelationWithLowestEvaluation();
                        if (relationWithLowestEvaluation != null && relationWithLowestEvaluation.OtherEmpire != triggerEmpire)
                        {
                            ShipGroup shipGroup3 = relationWithLowestEvaluation.OtherEmpire.EmpireEventRogueFleetDefects(triggerEmpire);
                            if (shipGroup3 != null)
                            {
                                text = string.Format(TextResolver.GetText("GameEventAction Description EnemyFleetDefectsToTriggerEmpire"), shipGroup3.Name, relationWithLowestEvaluation.OtherEmpire.Name);
                                title = TextResolver.GetText("GameEventAction Title EnemyFleetDefectsToTriggerEmpire");
                                additionalData = shipGroup3;
                                location = null;
                                flag = true;
                            }
                        }
                        break;
                    }
                case EventActionType.FindMoneyTreasure:
                    if (eventAction.MoneyAmount > 0.0 && triggerEmpire != null)
                    {
                        triggerEmpire.StateMoney += eventAction.MoneyAmount;
                        triggerEmpire.PirateEconomy.PerformIncome(eventAction.MoneyAmount, PirateIncomeType.Undefined, CurrentStarDate);
                        text = string.Format(TextResolver.GetText("GameEventAction Description FindMoneyTreasure"), eventAction.MoneyAmount.ToString("###,###,##0"));
                        title = TextResolver.GetText("GameEventAction Title FindMoneyTreasure");
                        additionalData = null;
                        location = null;
                        flag = true;
                    }
                    break;
                case EventActionType.GenerateBuiltObject:
                    {
                        if (eventAction.Target == null || !(eventAction.Target is Habitat))
                        {
                            break;
                        }
                        habitat = (Habitat)eventAction.Target;
                        if (eventAction.BuiltObjectSubRole == BuiltObjectSubRole.Undefined)
                        {
                            break;
                        }
                        Empire empire3 = triggerEmpire;
                        if (empire3 == null && Empires.Count > 0)
                        {
                            empire3 = Empires[0];
                        }
                        if (empire3 != null)
                        {
                            Design design = empire3.GenerateDesignFromSpec(empire3.DesignSpecifications.GetBySubRole(eventAction.BuiltObjectSubRole), eventAction.TechLevel);
                            if (design != null)
                            {
                                design.PictureRef = ShipImageHelper.ResolveMinorShipImageIndex(design.SubRole, largeShips: true);
                                Habitat habitat7 = DetermineHabitatSystemStar(habitat);
                                builtObject = GenerateAbandonedBuiltObject(habitat, design, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Notify);
                                text = string.Format(TextResolver.GetText("GameEventAction Description GenerateBuiltObject"), ResolveDescription(builtObject.SubRole).ToLower(CultureInfo.InvariantCulture), builtObject.Name, habitat.Name, habitat7.Name);
                                title = string.Format(TextResolver.GetText("GameEventAction Title GenerateBuiltObject"), ResolveDescription(builtObject.SubRole));
                                additionalData = builtObject;
                                location = habitat;
                                flag = true;
                            }
                        }
                        break;
                    }
                case EventActionType.GenerateCreatureSwarm:
                    if (eventAction.Target != null && eventAction.Target is Habitat && eventAction.CreatureType != 0 && eventAction.Value > 0)
                    {
                        habitat = (Habitat)eventAction.Target;
                        int num7 = Math.Min(50, eventAction.Value);
                        for (int m = 0; m < num7; m++)
                        {
                            Habitat habitat21 = DetermineHabitatSystemStar(habitat);
                            creature = GenerateCreatureAtHabitat(eventAction.CreatureType, habitat, lockLocation: false);
                            text = string.Format(TextResolver.GetText("GameEventAction Description GenerateCreatureSwarm"), ResolveDescription(eventAction.CreatureType), habitat.Name, habitat21.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title GenerateCreatureSwarm"), ResolveDescription(eventAction.CreatureType));
                            additionalData = creature;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.GenerateNewEmpire:
                    {
                        if (eventAction.Race == null || eventAction.Target == null || !(eventAction.Target is Habitat))
                        {
                            break;
                        }
                        habitat = (Habitat)eventAction.Target;
                        if (habitat == null || habitat.HasBeenDestroyed || (habitat.Empire != null && habitat.Empire != IndependentEmpire))
                        {
                            break;
                        }
                        GovernmentAttributes governmentAttributes3 = null;
                        List<int> list = Empire.ResolveDefaultAllowableGovernmentTypes(eventAction.Race);
                        GovernmentAttributesList governmentAttributesList2 = Empire.DetermineMostSuitableGovermentTypes(eventAction.Race, list);
                        if (governmentAttributesList2 != null && governmentAttributesList2.Count > 0)
                        {
                            int index = Rnd.Next(0, governmentAttributesList2.Count);
                            governmentAttributes3 = governmentAttributesList2[index];
                            if (eventAction.Race.PreferredStartingGovernmentId >= 0 && list.Contains(eventAction.Race.PreferredStartingGovernmentId))
                            {
                                governmentAttributes3 = Governments[eventAction.Race.PreferredStartingGovernmentId];
                            }
                        }
                        double expansion = 0.0;
                        Empire empire6 = GenerateEmpire(this, isPlayerEmpire: false, string.Empty, habitat, eventAction.Race, eventAction.Race.DesignPictureFamilyIndex, governmentAttributes3.GovernmentId, 1.0, TextResolver.GetText("Normal"), 1, 0.5, 1.0, out expansion, null, null);
                        if (empire6 != null)
                        {
                            Habitat habitat26 = DetermineHabitatSystemStar(habitat);
                            text = string.Format(TextResolver.GetText("GameEventAction Description GenerateNewEmpire"), eventAction.Race.Name, empire6.Name, habitat.Name, habitat26.Name);
                            title = TextResolver.GetText("GameEventAction Title GenerateNewEmpire");
                            additionalData = empire6;
                            location = habitat;
                            flag = true;
                        }
                        break;
                    }
                case EventActionType.GenerateNewPirateFaction:
                    if (eventAction.Race == null || eventAction.Target == null || !(eventAction.Target is Habitat))
                    {
                        break;
                    }
                    habitat = (Habitat)eventAction.Target;
                    if (habitat != null && !habitat.HasBeenDestroyed && (habitat.Empire == null || habitat.Empire == IndependentEmpire) && NextEmpireID < MaximumEmpireCount)
                    {
                        SelectRelativeHabitatSurfacePoint(habitat, out var x, out var y);
                        PiratePlayStyle piratePlaystyle = SelectRandomPiratePlaystyle();
                        Empire empire4 = GeneratePirateEmpire(habitat, (int)x, (int)y, eventAction.Race, eventAction.Race.DesignPictureFamilyIndexPirates, 0.5, piratePlaystyle, isPlayerEmpire: false, isSuperPirates: false);
                        if (empire4 != null)
                        {
                            Habitat habitat12 = DetermineHabitatSystemStar(habitat);
                            text = string.Format(TextResolver.GetText("GameEventAction Description GenerateNewPirateFaction"), eventAction.Race.Name, empire4.Name, habitat.Name, habitat12.Name);
                            title = TextResolver.GetText("GameEventAction Title GenerateNewPirateFaction");
                            additionalData = empire4;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.GeneratePirateAmbush:
                    {
                        if (eventAction.Target == null || !(eventAction.Target is Habitat))
                        {
                            break;
                        }
                        habitat = (Habitat)eventAction.Target;
                        Empire empire2 = FindNearestPirateFaction(habitat.Xpos, habitat.Ypos, triggerEmpire, includeSuperPirates: false);
                        if (empire2 != null)
                        {
                            int num = Math.Min(50, eventAction.Value);
                            Habitat habitat3 = habitat;
                            double num2 = habitat.Xpos;
                            double num3 = habitat.Ypos;
                            int num4 = 0;
                            Habitat habitat4 = DetermineHabitatSystemStar(habitat);
                            while (habitat3 == habitat4 && num4 < 20)
                            {
                                num2 += Rnd.NextDouble() * 200000.0 - 100000.0;
                                num3 += Rnd.NextDouble() * 200000.0 - 100000.0;
                                habitat3 = FindNearestSystemGasCloudAsteroid(num2, num3);
                                num4++;
                            }
                            for (int i = 0; i < num; i++)
                            {
                                BuiltObject builtObject2 = GeneratePirateShip(empire2, BuiltObjectSubRole.Frigate, habitat3);
                                builtObject2.AssignMission(BuiltObjectMissionType.Move, habitat, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                text = string.Format(TextResolver.GetText("GameEventAction Description GeneratePirateAmbush"), empire2.Name, habitat3.Name);
                                title = TextResolver.GetText("GameEventAction Title GeneratePirateAmbush");
                                additionalData = empire2;
                                location = habitat3;
                                flag = true;
                            }
                        }
                        break;
                    }
                case EventActionType.GenerateRefugeeFleet:
                    if (eventAction.Target != null && eventAction.Target is Habitat)
                    {
                        habitat = (Habitat)eventAction.Target;
                        Race race = null;
                        race = ((eventAction.Race == null) ? SelectRandomRace(75) : eventAction.Race);
                        Empire empire5 = triggerEmpire;
                        if (empire5 == null && Empires.Count > 0)
                        {
                            empire5 = Empires[0];
                        }
                        if (empire5 != null)
                        {
                            Design design2 = empire5.GenerateDesignFromSpec(empire5.ObtainDesignSpec(BuiltObjectSubRole.ColonyShip), 3.0);
                            Design design3 = empire5.GenerateDesignFromSpec(empire5.ObtainDesignSpec(BuiltObjectSubRole.Frigate), 3.0);
                            Design design4 = empire5.GenerateDesignFromSpec(empire5.ObtainDesignSpec(BuiltObjectSubRole.Cruiser), 3.0);
                            design2.PictureRef = ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.ColonyShip, race, isPirates: false);
                            design3.PictureRef = ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.Frigate, race, isPirates: false);
                            design4.PictureRef = ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.Cruiser, race, isPirates: false);
                            BuiltObject builtObject5 = GenerateAbandonedBuiltObject(habitat, design2, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Notify);
                            builtObject5.Name = string.Format(TextResolver.GetText("Refugee SHIPTYPE"), ResolveDescription(BuiltObjectSubRole.ColonyShip));
                            builtObject5.NativeRace = race;
                            BuiltObject builtObject6 = GenerateAbandonedBuiltObject(habitat, design3, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Notify);
                            builtObject6.Name = string.Format(TextResolver.GetText("Refugee SHIPTYPE"), ResolveDescription(BuiltObjectSubRole.Frigate));
                            BuiltObject builtObject7 = GenerateAbandonedBuiltObject(habitat, design4, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Notify);
                            builtObject7.Name = string.Format(TextResolver.GetText("Refugee SHIPTYPE"), ResolveDescription(BuiltObjectSubRole.Cruiser));
                            Habitat habitat22 = DetermineHabitatSystemStar(habitat);
                            text = string.Format(TextResolver.GetText("GameEventAction Description GenerateRefugeeFleet"), race.Name, habitat22.Name);
                            title = TextResolver.GetText("GameEventAction Title GenerateRefugeeFleet");
                            additionalData = race;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.GenerateResourceAtHabitat:
                    if (eventAction.Target != null && eventAction.Target is Habitat && eventAction.Value >= 0 && eventAction.Value < ResourceSystem.Resources.Count)
                    {
                        habitat = (Habitat)eventAction.Target;
                        if (!habitat.HasBeenDestroyed)
                        {
                            Resource resource2 = new Resource(ResourceSystem.Resources[eventAction.Value].ResourceID);
                            Empire.EmpireEventColonyResourceAppearance(habitat, resource2, triggerEmpire);
                            Habitat habitat11 = DetermineHabitatSystemStar(habitat);
                            text = string.Format(TextResolver.GetText("GameEventAction Description GenerateResourceAtHabitat"), resource2.Name, habitat.Name, habitat11.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title GenerateResourceAtHabitat"), resource2.Name);
                            additionalData = resource2;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.RemoveResourceAtHabitat:
                    if (eventAction.Target != null && eventAction.Target is Habitat && eventAction.Value >= 0 && eventAction.Value < ResourceSystem.Resources.Count)
                    {
                        habitat = (Habitat)eventAction.Target;
                        if (!habitat.HasBeenDestroyed)
                        {
                            Resource resource = new Resource(ResourceSystem.Resources[eventAction.Value].ResourceID);
                            Empire.EmpireEventColonyResourceDepletion(habitat, resource, triggerEmpire, this);
                            Habitat habitat5 = DetermineHabitatSystemStar(habitat);
                            text = string.Format(TextResolver.GetText("GameEventAction Description RemoveResourceAtHabitat"), resource.Name, habitat.Name, habitat5.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title RemoveResourceAtHabitat"), resource.Name);
                            additionalData = resource;
                            location = habitat;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.InterceptResource:
                    if (triggerEmpire != null && eventAction.Value >= 0 && eventAction.Value < ResourceSystem.Resources.Count)
                    {
                        Resource resource3 = new Resource(ResourceSystem.Resources[eventAction.Value].ResourceID);
                        double x2 = 0.0;
                        double y2 = 0.0;
                        if (triggerEmpire.Capital != null)
                        {
                            x2 = triggerEmpire.Capital.Xpos;
                            y2 = triggerEmpire.Capital.Ypos;
                        }
                        else if (triggerEmpire.PirateEmpireBaseHabitat != null)
                        {
                            x2 = triggerEmpire.PirateEmpireBaseHabitat.Xpos;
                            y2 = triggerEmpire.PirateEmpireBaseHabitat.Ypos;
                        }
                        if (gameEvent != null && gameEvent.TriggerObject != null)
                        {
                            x2 = gameEvent.TriggerObject.Xpos;
                            y2 = gameEvent.TriggerObject.Ypos;
                        }
                        BuiltObject builtObject4 = FastFindNearestSpacePort(x2, y2, triggerEmpire);
                        triggerEmpire.RandomEventRareResourceIntercepted(resource3, builtObject4, null);
                        string arg2 = string.Empty;
                        if (builtObject4.NearestSystemStar != null)
                        {
                            arg2 = builtObject4.NearestSystemStar.Name;
                        }
                        text = string.Format(TextResolver.GetText("GameEventAction Description InterceptResource"), resource3.Name, arg2, builtObject4.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title InterceptResource"), resource3.Name);
                        additionalData = resource3;
                        location = builtObject4;
                        flag = true;
                    }
                    break;
                case EventActionType.LearnAboutLostColony:
                    if (eventAction.Target == null || !(eventAction.Target is Habitat) || eventAction.Race == null || eventAction.Value <= 0 || triggerEmpire == null)
                    {
                        break;
                    }
                    habitat = (Habitat)eventAction.Target;
                    if (habitat.Owner == null)
                    {
                        switch (habitat.Type)
                        {
                            case HabitatType.Volcanic:
                            case HabitatType.Desert:
                            case HabitatType.MarshySwamp:
                            case HabitatType.Continental:
                            case HabitatType.Ocean:
                            case HabitatType.Ice:
                                {
                                    Habitat habitat9 = DetermineHabitatSystemStar(habitat);
                                    long newPopulationAmount = Math.Min(20000000000L, eventAction.Value * 1000000);
                                    triggerEmpire.MakeHabitatIntoColony(habitat, null, eventAction.Race, newPopulationAmount);
                                    text = string.Format(TextResolver.GetText("GameEventAction Description LearnAboutLostColony"), eventAction.Race.Name, habitat.Name, habitat9.Name);
                                    title = TextResolver.GetText("GameEventAction Title LearnAboutLostColony");
                                    additionalData = eventAction.Race;
                                    location = habitat;
                                    flag = true;
                                    break;
                                }
                        }
                    }
                    break;
                case EventActionType.LearnAboutSpecialLocation:
                    if (eventAction.Location != null && triggerEmpire != null && !triggerEmpire.KnownGalaxyLocations.Contains(eventAction.Location))
                    {
                        string arg = ResolveSectorDescriptionStatic(eventAction.Location.Xpos, eventAction.Location.Ypos);
                        triggerEmpire.KnownGalaxyLocations.Add(eventAction.Location);
                        if (triggerEmpire == PlayerEmpire)
                        {
                            triggerEmpire.AddLocationHint(new Point((int)eventAction.Location.Xpos + (int)eventAction.Location.Width / 2, (int)eventAction.Location.Ypos + (int)eventAction.Location.Height / 2));
                        }
                        text = string.Format(TextResolver.GetText("GameEventAction Description LearnAboutSpecialLocation"), eventAction.Location.Name, arg);
                        title = TextResolver.GetText("GameEventAction Title LearnAboutSpecialLocation");
                        additionalData = eventAction.Location;
                        location = eventAction.Location;
                        flag = true;
                    }
                    break;
                case EventActionType.LearnExplorationInfo:
                    {
                        if (eventAction.Value <= 0 || triggerEmpire == null || gameEvent == null || (gameEvent.TriggerObject == null && gameEvent.TriggerRuin == null))
                        {
                            break;
                        }
                        double x3 = 0.0;
                        double y3 = 0.0;
                        if (gameEvent.TriggerObject != null)
                        {
                            x3 = gameEvent.TriggerObject.Xpos;
                            y3 = gameEvent.TriggerObject.Ypos;
                        }
                        else if (gameEvent.TriggerRuin != null)
                        {
                            habitat = RuinsHabitats.FindHabitatWithRuin(gameEvent.TriggerRuin);
                            if (habitat != null)
                            {
                                x3 = habitat.Xpos;
                                y3 = habitat.Xpos;
                            }
                        }
                        Habitat habitat18 = FindNearestSystemGasCloudAsteroid(x3, y3);
                        int num6 = Math.Min(50, eventAction.Value);
                        for (int k = 0; k < num6; k++)
                        {
                            Habitat habitat19 = FastFindNearestUnexploredHabitat(x3, y3, triggerEmpire);
                            if (habitat19 == null)
                            {
                                break;
                            }
                            triggerEmpire.SystemVisibility[habitat19.SystemIndex].TotallyExplored = true;
                            if (triggerEmpire.ResourceMap != null)
                            {
                                for (int l = 0; l < Systems[habitat19.SystemIndex].Habitats.Count; l++)
                                {
                                    Habitat habitat20 = Systems[habitat19.SystemIndex].Habitats[l];
                                    triggerEmpire.ResourceMap.SetResourcesKnown(habitat20, known: true);
                                }
                                if (Systems[habitat19.SystemIndex].SystemStar != null)
                                {
                                    triggerEmpire.ResourceMap.SetResourcesKnown(Systems[habitat19.SystemIndex].SystemStar, known: true);
                                }
                            }
                            SystemVisibilityStatus status = triggerEmpire.SystemVisibility[habitat19.SystemIndex].Status;
                            if (status == SystemVisibilityStatus.Unexplored || status == SystemVisibilityStatus.Undefined)
                            {
                                triggerEmpire.SystemVisibility[habitat19.SystemIndex].Status = SystemVisibilityStatus.Explored;
                            }
                        }
                        text = string.Format(TextResolver.GetText("GameEventAction Description LearnExplorationInfo"), num6.ToString("0"), habitat18.Name);
                        title = TextResolver.GetText("GameEventAction Title LearnExplorationInfo");
                        additionalData = null;
                        location = habitat18;
                        flag = true;
                        break;
                    }
                case EventActionType.LearnGovernmentType:
                    {
                        if (triggerEmpire == null || triggerEmpire.PirateEmpireBaseHabitat != null || eventAction.Value < 0 || eventAction.Value >= Governments.Count)
                        {
                            break;
                        }
                        GovernmentAttributes governmentAttributes = Governments[eventAction.Value];
                        if (governmentAttributes == null)
                        {
                            break;
                        }
                        if (!triggerEmpire.AllowableGovernmentTypes.Contains(governmentAttributes.GovernmentId))
                        {
                            triggerEmpire.AllowableGovernmentTypes.Add(governmentAttributes.GovernmentId);
                        }
                        if (triggerEmpire != PlayerEmpire)
                        {
                            GovernmentAttributesList governmentAttributesList = Empire.DetermineMostSuitableGovermentTypes(triggerEmpire.DominantRace, triggerEmpire.AllowableGovernmentTypes);
                            int governmentId = governmentAttributesList[0].GovernmentId;
                            if (governmentId == governmentAttributes.GovernmentId)
                            {
                                triggerEmpire.HaveRevolution(triggerEmpire.DominantRace, governmentId);
                            }
                        }
                        text = string.Format(TextResolver.GetText("GameEventAction Description LearnGovernmentType"), governmentAttributes.Name);
                        title = TextResolver.GetText("GameEventAction Title LearnGovernmentType");
                        additionalData = governmentAttributes;
                        location = null;
                        flag = true;
                        break;
                    }
                case EventActionType.LearnTech:
                    if (triggerEmpire != null && eventAction.Value >= 0 && eventAction.Value < ResearchNodeDefinitions.Count)
                    {
                        ResearchNode researchNode = triggerEmpire.Research.TechTree.FindNodeById(eventAction.Value);
                        if (researchNode != null && !researchNode.IsResearched)
                        {
                            triggerEmpire.DoResearchBreakthrough(researchNode, selfResearched: true, blockMessages: true, suppressUpdate: true);
                            triggerEmpire.Research.Update(triggerEmpire.DominantRace);
                            triggerEmpire.ReviewDesignsBuiltObjectsImprovedComponents();
                            triggerEmpire.ReviewResearchAbilities();
                            text = string.Format(TextResolver.GetText("GameEventAction Description LearnTech"), researchNode.Name);
                            title = TextResolver.GetText("GameEventAction Title LearnTech");
                            additionalData = researchNode;
                            location = null;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.UnlockTech:
                    if (triggerEmpire != null && eventAction.Value >= 0 && eventAction.Value < ResearchNodeDefinitions.Count)
                    {
                        ResearchNode researchNode4 = triggerEmpire.Research.TechTree.FindNodeById(eventAction.Value);
                        if (researchNode4 != null && !researchNode4.IsEnabled)
                        {
                            researchNode4.IsEnabled = true;
                            text = string.Format(TextResolver.GetText("GameEventAction Description UnlockTech"), researchNode4.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title UnlockTech"), researchNode4.Name);
                            additionalData = researchNode4;
                            location = null;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.MakeEmpireContact:
                    if (eventAction.Empire == null || triggerEmpire == null || eventAction.Empire == triggerEmpire)
                    {
                        break;
                    }
                    if (eventAction.Empire.PirateEmpireBaseHabitat == null && triggerEmpire.PirateEmpireBaseHabitat == null)
                    {
                        DiplomaticRelation diplomaticRelation7 = triggerEmpire.ObtainDiplomaticRelation(eventAction.Empire);
                        if (diplomaticRelation7.Type == DiplomaticRelationType.NotMet)
                        {
                            diplomaticRelation7.Type = DiplomaticRelationType.None;
                            diplomaticRelation7 = eventAction.Empire.ObtainDiplomaticRelation(triggerEmpire);
                            diplomaticRelation7.Type = DiplomaticRelationType.None;
                            text = string.Format(TextResolver.GetText("GameEventAction Description MakeEmpireContact"), eventAction.Empire);
                            title = string.Format(TextResolver.GetText("GameEventAction Title MakeEmpireContact"), eventAction.Empire);
                            additionalData = eventAction.Empire;
                            location = null;
                            flag = true;
                        }
                    }
                    else
                    {
                        PirateRelation pirateRelation = triggerEmpire.ObtainPirateRelation(eventAction.Empire);
                        if (pirateRelation.Type == PirateRelationType.NotMet)
                        {
                            triggerEmpire.ChangePirateRelation(eventAction.Empire, PirateRelationType.None, CurrentStarDate);
                            text = string.Format(TextResolver.GetText("GameEventAction Description MakeEmpireContact"), eventAction.Empire);
                            title = string.Format(TextResolver.GetText("GameEventAction Title MakeEmpireContact"), eventAction.Empire);
                            additionalData = eventAction.Empire;
                            location = null;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.SleepingRaceAwokenAtHabitat:
                    if (eventAction.Target == null || !(eventAction.Target is Habitat) || eventAction.Race == null || eventAction.Value <= 0)
                    {
                        break;
                    }
                    habitat = (Habitat)eventAction.Target;
                    if (habitat.Owner == null)
                    {
                        long amount = Math.Min(20000000000L, eventAction.Value * 1000000);
                        Population population = new Population(eventAction.Race, amount);
                        if (habitat.Population == null)
                        {
                            habitat.Population = new PopulationList();
                        }
                        habitat.Population.Add(population);
                        IndependentEmpire.TakeOwnershipOfColony(habitat, IndependentEmpire);
                        Habitat habitat8 = DetermineHabitatSystemStar(habitat);
                        text = string.Format(TextResolver.GetText("GameEventAction Description SleepingRaceAwokenAtHabitat"), eventAction.Race.Name, habitat.Name, habitat8.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title SleepingRaceAwokenAtHabitat"), eventAction.Race.Name);
                        additionalData = eventAction.Race;
                        location = habitat;
                        flag = true;
                    }
                    break;
                case EventActionType.SplitEmpireCivilWar:
                    if (eventAction.Empire != null)
                    {
                        double splinterPortion2 = 0.3 + Rnd.NextDouble() * 0.25;
                        eventAction.Empire.InitiateEmpireSplit(splinterPortion2, declareWar: true);
                        text = string.Format(TextResolver.GetText("GameEventAction Description SplitEmpireCivilWar"), eventAction.Empire.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title SplitEmpireCivilWar"), eventAction.Empire.Name);
                        additionalData = eventAction.Empire;
                        location = null;
                        flag = true;
                    }
                    break;
                case EventActionType.SplitEmpirePeacefully:
                    if (eventAction.Empire != null)
                    {
                        double splinterPortion = 0.3 + Rnd.NextDouble() * 0.25;
                        eventAction.Empire.InitiateEmpireSplit(splinterPortion, declareWar: false);
                        text = string.Format(TextResolver.GetText("GameEventAction Description SplitEmpirePeacefully"), eventAction.Empire.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title SplitEmpirePeacefully"), eventAction.Empire.Name);
                        additionalData = eventAction.Empire;
                        location = null;
                        flag = true;
                    }
                    break;
                case EventActionType.UnlockTechForEmpire:
                    if (eventAction.Empire != null && eventAction.Value >= 0 && eventAction.Value < ResearchNodeDefinitions.Count)
                    {
                        ResearchNode researchNode2 = eventAction.Empire.Research.TechTree.FindNodeById(eventAction.Value);
                        if (researchNode2 != null && !researchNode2.IsEnabled)
                        {
                            researchNode2.IsEnabled = true;
                            text = string.Format(TextResolver.GetText("GameEventAction Description UnlockTechForEmpire"), researchNode2.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title UnlockTechForEmpire"), researchNode2.Name);
                            additionalData = researchNode2;
                            location = null;
                            flag = true;
                            empire = eventAction.Empire;
                        }
                    }
                    break;
                case EventActionType.ChangeEmpireReputation:
                    if (eventAction.Empire != null && eventAction.Value != 0)
                    {
                        eventAction.Empire.CivilityRating += eventAction.Value;
                        if (eventAction.Value >= 0)
                        {
                            text = TextResolver.GetText("GameEventAction Description ChangeEmpireReputation Better");
                            title = TextResolver.GetText("GameEventAction Title ChangeEmpireReputation Better");
                        }
                        else
                        {
                            text = TextResolver.GetText("GameEventAction Description ChangeEmpireReputation Worse");
                            title = TextResolver.GetText("GameEventAction Title ChangeEmpireReputation Worse");
                        }
                        additionalData = null;
                        location = null;
                        flag = true;
                        empire = eventAction.Empire;
                    }
                    break;
                case EventActionType.ChangeEmpireEvaluation:
                    if (eventAction.Empire != null && eventAction.EmpireOther != null && eventAction.Empire != eventAction.EmpireOther && eventAction.Value != 0)
                    {
                        EmpireEvaluation empireEvaluation = eventAction.Empire.ObtainEmpireEvaluation(eventAction.EmpireOther);
                        if (empireEvaluation != null)
                        {
                            empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw + (double)eventAction.Value;
                            text = ((eventAction.Value < 0) ? string.Format(TextResolver.GetText("GameEventAction Description ChangeEmpireEvaluation Worse"), eventAction.Empire.Name) : string.Format(TextResolver.GetText("GameEventAction Description ChangeEmpireEvaluation Better"), eventAction.Empire.Name));
                            title = string.Format(TextResolver.GetText("GameEventAction Title ChangeEmpireEvaluation"), eventAction.Empire.Name);
                            additionalData = null;
                            location = null;
                            flag = true;
                            empire = eventAction.EmpireOther;
                        }
                    }
                    break;
                case EventActionType.InitiateTreaty:
                    {
                        if (eventAction.Empire == null || eventAction.EmpireOther == null || eventAction.Empire == eventAction.EmpireOther || (eventAction.DiplomaticRelationType != DiplomaticRelationType.FreeTradeAgreement && eventAction.DiplomaticRelationType != DiplomaticRelationType.Protectorate && eventAction.DiplomaticRelationType != DiplomaticRelationType.MutualDefensePact))
                        {
                            break;
                        }
                        DiplomaticRelation diplomaticRelation2 = eventAction.Empire.ObtainDiplomaticRelation(eventAction.EmpireOther);
                        if (diplomaticRelation2 == null)
                        {
                            break;
                        }
                        if (diplomaticRelation2.Type != eventAction.DiplomaticRelationType)
                        {
                            eventAction.Empire.ChangeDiplomaticRelation(diplomaticRelation2, eventAction.DiplomaticRelationType, blockFlowonEffects: false, eventAction.LockedAlliance, eventAction.AllianceName);
                            text = string.Format(TextResolver.GetText("GameEventAction Description InitiateTreaty"), ResolveDescription(eventAction.DiplomaticRelationType), eventAction.Empire.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title InitiateTreaty"), ResolveDescription(eventAction.DiplomaticRelationType));
                            additionalData = null;
                            location = null;
                            flag = true;
                            empire = eventAction.EmpireOther;
                            break;
                        }
                        if (eventAction.LockedAlliance && !diplomaticRelation2.Locked)
                        {
                            diplomaticRelation2.Locked = true;
                            DiplomaticRelation diplomaticRelation3 = eventAction.EmpireOther.ObtainDiplomaticRelation(eventAction.Empire);
                            if (diplomaticRelation3 != null)
                            {
                                diplomaticRelation3.Locked = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(eventAction.AllianceName) && diplomaticRelation2.AllianceName != eventAction.AllianceName)
                        {
                            diplomaticRelation2.AllianceName = eventAction.AllianceName;
                            DiplomaticRelation diplomaticRelation4 = eventAction.EmpireOther.ObtainDiplomaticRelation(eventAction.Empire);
                            if (diplomaticRelation4 != null)
                            {
                                diplomaticRelation4.AllianceName = eventAction.AllianceName;
                            }
                        }
                        break;
                    }
                case EventActionType.BreakTreaty:
                    if (eventAction.Empire != null && eventAction.EmpireOther != null && eventAction.Empire != eventAction.EmpireOther)
                    {
                        DiplomaticRelation diplomaticRelation = eventAction.Empire.ObtainDiplomaticRelation(eventAction.EmpireOther);
                        if (diplomaticRelation != null && (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || diplomaticRelation.Type == DiplomaticRelationType.Protectorate || diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact))
                        {
                            DiplomaticRelationType type = diplomaticRelation.Type;
                            eventAction.Empire.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                            text = string.Format(TextResolver.GetText("GameEventAction Description BreakTreaty"), ResolveDescription(type), eventAction.Empire.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title BreakTreaty"), ResolveDescription(type));
                            additionalData = null;
                            location = null;
                            flag = true;
                            empire = eventAction.EmpireOther;
                        }
                    }
                    break;
                case EventActionType.StartTradingSuperLuxuryResources:
                    if (eventAction.Empire != null && eventAction.EmpireOther != null && eventAction.Empire != eventAction.EmpireOther)
                    {
                        DiplomaticRelation diplomaticRelation9 = eventAction.Empire.ObtainDiplomaticRelation(eventAction.EmpireOther);
                        if (diplomaticRelation9 != null && !diplomaticRelation9.SupplyRestrictedResources)
                        {
                            diplomaticRelation9.SupplyRestrictedResources = true;
                            text = string.Format(TextResolver.GetText("GameEventAction Description StartTradingSuperLuxuryResources"), eventAction.Empire.Name);
                            title = TextResolver.GetText("GameEventAction Title StartTradingSuperLuxuryResources");
                            additionalData = null;
                            location = null;
                            flag = true;
                            empire = eventAction.EmpireOther;
                        }
                    }
                    break;
                case EventActionType.StopTradingSuperLuxuryResources:
                    if (eventAction.Empire != null && eventAction.EmpireOther != null && eventAction.Empire != eventAction.EmpireOther)
                    {
                        DiplomaticRelation diplomaticRelation8 = eventAction.Empire.ObtainDiplomaticRelation(eventAction.EmpireOther);
                        if (diplomaticRelation8 != null && diplomaticRelation8.SupplyRestrictedResources)
                        {
                            diplomaticRelation8.SupplyRestrictedResources = false;
                            text = string.Format(TextResolver.GetText("GameEventAction Description StopTradingSuperLuxuryResources"), eventAction.Empire.Name);
                            title = TextResolver.GetText("GameEventAction Title StopTradingSuperLuxuryResources");
                            additionalData = null;
                            location = null;
                            flag = true;
                            empire = eventAction.EmpireOther;
                        }
                    }
                    break;
                case EventActionType.GeneralMessageToEmpire:
                    if (eventAction.Empire != null && !string.IsNullOrWhiteSpace(eventAction.MessageText))
                    {
                        text = eventAction.MessageText;
                        title = TextResolver.GetText("GameEventAction Title GeneralMessageToEmpire");
                        additionalData = eventAction;
                        location = null;
                        flag = true;
                        empire = eventAction.Empire;
                    }
                    break;
                case EventActionType.EmpireMessageToEmpire:
                    if (eventAction.Empire != null && eventAction.EmpireOther != null && !string.IsNullOrWhiteSpace(eventAction.MessageText))
                    {
                        text = eventAction.MessageText;
                        title = string.Format(TextResolver.GetText("GameEventAction Title EmpireMessageToEmpire"), eventAction.Empire.Name);
                        additionalData = eventAction;
                        location = null;
                        flag = true;
                        empire = eventAction.EmpireOther;
                    }
                    break;
                case EventActionType.ResearchBonusInProject:
                    if (eventAction.Value >= 0 && eventAction.Value < ResearchNodeDefinitions.Count)
                    {
                        ResearchNode researchNode3 = triggerEmpire.Research.TechTree.FindNodeById(eventAction.Value);
                        if (researchNode3 != null && !researchNode3.IsResearched)
                        {
                            float cost = researchNode3.Cost;
                            float num5 = (float)(Rnd.NextDouble() * (double)cost);
                            researchNode3.Progress += num5;
                            text = string.Format(TextResolver.GetText("GameEventAction Description ResearchBonusInProject"), researchNode3.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title ResearchBonusInProject"), researchNode3.Name);
                            additionalData = null;
                            location = null;
                            flag = true;
                        }
                    }
                    break;
                case EventActionType.VictoryConditionBonus:
                    if (eventAction.Empire != null)
                    {
                        eventAction.Empire.VictoryBonus += (float)eventAction.Value / 100f;
                        text = string.Format(TextResolver.GetText("GameEventAction Description VictoryConditionBonus"), eventAction.Value.ToString("+0;-0") + "%");
                        title = TextResolver.GetText("GameEventAction Title VictoryConditionBonus");
                        additionalData = eventAction.Empire;
                        location = null;
                        flag = true;
                        empire = eventAction.Empire;
                    }
                    break;
                case EventActionType.EmpireDeclaresWarOnOtherEmpire:
                    if (eventAction.Empire == null || eventAction.EmpireOther == null || eventAction.Empire == eventAction.EmpireOther)
                    {
                        break;
                    }
                    if (eventAction.DiplomaticRelationType != DiplomaticRelationType.War)
                    {
                        eventAction.Empire.DeclareWar(eventAction.EmpireOther, null, eventAction.LockedAlliance, blockFlowonEffects: false);
                        text = string.Format(TextResolver.GetText("GameEventAction Description EmpireDeclaresWarOnOtherEmpire"), eventAction.Empire.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title EmpireDeclaresWarOnOtherEmpire"), eventAction.Empire.Name, eventAction.EmpireOther.Name);
                        additionalData = null;
                        location = null;
                        flag = true;
                        empire = eventAction.EmpireOther;
                    }
                    else
                    {
                        if (!eventAction.LockedAlliance)
                        {
                            break;
                        }
                        DiplomaticRelation diplomaticRelation5 = eventAction.Empire.ObtainDiplomaticRelation(eventAction.EmpireOther);
                        if (diplomaticRelation5 != null && !diplomaticRelation5.Locked)
                        {
                            diplomaticRelation5.Locked = true;
                            DiplomaticRelation diplomaticRelation6 = eventAction.EmpireOther.ObtainDiplomaticRelation(eventAction.Empire);
                            if (diplomaticRelation6 != null)
                            {
                                diplomaticRelation6.Locked = true;
                            }
                        }
                    }
                    break;
                case EventActionType.SendFleetAttack:
                    if (eventAction.Target != null && eventAction.Empire != null && eventAction.EmpireOther != null && eventAction.Empire != eventAction.EmpireOther && eventAction.Target.Empire == eventAction.EmpireOther)
                    {
                        ShipGroup shipGroup = eventAction.Empire.IdentifyNearestAvailableFleet(eventAction.Target.Xpos, eventAction.Target.Ypos, mustBeAutomated: true, mustBeWithinFuelRange: true, 0.0, 0.0, defendFleetsMustBeWithinPostureRange: false, forceFleetUse: true, 10);
                        if (shipGroup == null)
                        {
                            shipGroup = eventAction.Empire.IdentifyNearestAvailableFleet(eventAction.Target.Xpos, eventAction.Target.Ypos, mustBeAutomated: true, mustBeWithinFuelRange: true, 0.0, 0.0, defendFleetsMustBeWithinPostureRange: false, forceFleetUse: true, 0);
                        }
                        if (shipGroup != null)
                        {
                            shipGroup.AssignMission(BuiltObjectMissionType.Attack, eventAction.Target, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            text = string.Format(TextResolver.GetText("GameEventAction Description SendFleetAttack"), eventAction.Target.Name, eventAction.Empire.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title SendFleetAttack"), eventAction.Target.Name);
                            additionalData = eventAction.Target;
                            location = eventAction.Target;
                            flag = true;
                            empire = eventAction.Empire;
                        }
                    }
                    break;
                case EventActionType.SendPlanetDestroyerAttack:
                    if (eventAction.Target != null && eventAction.Target is Habitat && eventAction.Empire != null && eventAction.EmpireOther != null && eventAction.Empire != eventAction.EmpireOther && eventAction.Target.Empire == eventAction.EmpireOther && eventAction.Empire.PlanetDestroyers != null && eventAction.Empire.PlanetDestroyers.Count > 0)
                    {
                        BuiltObject builtObject8 = eventAction.Empire.PlanetDestroyers.GetFirstAvailableWithinRange(BuiltObjectRole.Military, eventAction.Target.Xpos, eventAction.Target.Ypos, 0.0, includeLowAndNormalPriorityMissions: true);
                        if (builtObject8 == null)
                        {
                            builtObject8 = eventAction.Empire.PlanetDestroyers.GetNearestBuiltObjectCompleteUndamaged(eventAction.Target.Xpos, eventAction.Target.Ypos, BuiltObjectRole.Military, null);
                        }
                        if (builtObject8 != null && builtObject8.IsPlanetDestroyer)
                        {
                            builtObject8.AssignMission(BuiltObjectMissionType.Attack, eventAction.Target, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            text = string.Format(TextResolver.GetText("GameEventAction Description SendPlanetDestroyerAttack"), eventAction.Target.Name, eventAction.Empire.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title SendPlanetDestroyerAttack"), eventAction.Target.Name);
                            additionalData = eventAction.Target;
                            location = eventAction.Target;
                            flag = true;
                            empire = eventAction.Empire;
                        }
                    }
                    break;
                case EventActionType.IntergalacticConvoyMilitary:
                    if (eventAction.Empire != null && eventAction.Value > 0 && eventAction.Empire.Active)
                    {
                        GenerateMilitaryConvoy(eventAction.Empire, eventAction.Value, 1f);
                        text = string.Empty;
                        flag = true;
                    }
                    break;
                case EventActionType.IntergalacticConvoyCivilian:
                    if (eventAction.Empire != null && eventAction.Value > 0 && eventAction.Empire.Active)
                    {
                        GenerateCivilianConvoy(eventAction.Empire, eventAction.Value, 1f, string.Empty);
                        text = string.Empty;
                        flag = true;
                    }
                    break;
                case EventActionType.CharacterGenerate:
                    if (eventAction.Empire == null || eventAction.Target == null || (eventAction.Target.Empire != eventAction.Empire && eventAction.Target.Empire != IndependentEmpire))
                    {
                        break;
                    }
                    if (eventAction.Character != null && !eventAction.Character.Active)
                    {
                        if (eventAction.Empire.GenerateNewCharacterFromCustom(eventAction.Character, eventAction.Target))
                        {
                            text = string.Format(TextResolver.GetText("GameEventAction Description CharacterGenerate"), ResolveDescription(eventAction.Character.Role), eventAction.Character.Name, eventAction.Target.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title CharacterGenerate"), ResolveDescription(eventAction.Character.Role));
                            additionalData = eventAction.Character;
                            location = eventAction.Target;
                            flag = true;
                            empire = eventAction.Empire;
                        }
                    }
                    else if (eventAction.CharacterRole != 0)
                    {
                        Character character = eventAction.Empire.GenerateNewCharacterRandom(eventAction.CharacterRole, eventAction.Target, activate: true);
                        if (character != null)
                        {
                            text = string.Format(TextResolver.GetText("GameEventAction Description CharacterGenerate"), ResolveDescription(character.Role), character.Name, eventAction.Target.Name);
                            title = string.Format(TextResolver.GetText("GameEventAction Title CharacterGenerate"), ResolveDescription(character.Role));
                            additionalData = character;
                            location = eventAction.Target;
                            flag = true;
                            empire = eventAction.Empire;
                        }
                    }
                    break;
                case EventActionType.CharacterKill:
                    if (eventAction.Empire != null && eventAction.Character != null && eventAction.Character.Active)
                    {
                        text = string.Format(TextResolver.GetText("GameEventAction Description CharacterKill"), ResolveDescription(eventAction.Character.Role), eventAction.Character.Name);
                        title = string.Format(TextResolver.GetText("GameEventAction Title CharacterKill"), ResolveDescription(eventAction.Character.Role));
                        additionalData = eventAction.Character;
                        location = eventAction.Character.Location;
                        flag = true;
                        empire = eventAction.Empire;
                        eventAction.Character.Kill(this);
                    }
                    break;
                case EventActionType.CharacterChangeEmpire:
                    if (eventAction.Empire == null || eventAction.Character == null || !eventAction.Character.Active || eventAction.EmpireOther == null || eventAction.Empire == eventAction.EmpireOther)
                    {
                        break;
                    }
                    eventAction.Character.CompleteEmpireChange(eventAction.EmpireOther);
                    if (eventAction.EmpireOther.PirateEmpireBaseHabitat != null)
                    {
                        BuiltObject builtObject3 = IdentifyPirateSpaceport(eventAction.EmpireOther);
                        if (builtObject3 != null)
                        {
                            eventAction.Character.CompleteLocationTransfer(builtObject3, this);
                        }
                        else
                        {
                            builtObject3 = IdentifyPirateBase(eventAction.EmpireOther);
                            if (builtObject3 != null)
                            {
                                eventAction.Character.CompleteLocationTransfer(builtObject3, this);
                            }
                        }
                    }
                    else if (eventAction.EmpireOther.Capital != null)
                    {
                        eventAction.Character.CompleteLocationTransfer(eventAction.EmpireOther.Capital, this);
                    }
                    text = string.Format(TextResolver.GetText("GameEventAction Description CharacterChangeEmpire"), ResolveDescription(eventAction.Character.Role), eventAction.Character.Name, eventAction.Empire.Name, eventAction.EmpireOther.Name);
                    title = string.Format(TextResolver.GetText("GameEventAction Title CharacterChangeEmpire"), ResolveDescription(eventAction.Character.Role));
                    additionalData = eventAction.Character;
                    location = eventAction.Character.Location;
                    flag = true;
                    empire = eventAction.Empire;
                    break;
                case EventActionType.CharacterChangeRole:
                    if (eventAction.Empire != null && eventAction.Character != null && eventAction.Character.Active && eventAction.CharacterRole != 0)
                    {
                        text = string.Format(TextResolver.GetText("GameEventAction Description CharacterChangeRole"), ResolveDescription(eventAction.Character.Role), eventAction.Character.Name, ResolveDescription(eventAction.CharacterRole));
                        title = string.Format(TextResolver.GetText("GameEventAction Title CharacterChangeRole"), ResolveDescription(eventAction.Character.Role), ResolveDescription(eventAction.CharacterRole));
                        additionalData = eventAction.Character;
                        location = eventAction.Character.Location;
                        flag = true;
                        empire = eventAction.Empire;
                        eventAction.Character.RemoveAllSkillsAndTraits();
                        eventAction.Character.Role = eventAction.CharacterRole;
                        eventAction.Character.Empire.ApplyRandomCharacterSkillsTraits(eventAction.Character, boostSkillLevels: false);
                        OnCharacterImageChanged(new CharacterImageChangedEventArgs(eventAction.Character));
                    }
                    break;
                case EventActionType.CharacterChangeImage:
                    if (eventAction.Empire != null && eventAction.Character != null && eventAction.Character.Active && !string.IsNullOrEmpty(eventAction.ImageFilename))
                    {
                        text = string.Empty;
                        additionalData = eventAction.Character;
                        location = eventAction.Character.Location;
                        flag = true;
                        empire = eventAction.Empire;
                        eventAction.Character.PictureFilename = eventAction.ImageFilename;
                        OnCharacterImageChanged(new CharacterImageChangedEventArgs(eventAction.Character));
                    }
                    break;
            }
            if (gameEvent != null)
            {
                if (!string.IsNullOrWhiteSpace(gameEvent.Title))
                {
                    title = gameEvent.Title;
                }
                if (!string.IsNullOrWhiteSpace(gameEvent.Description))
                {
                    text += "\n\n";
                    text += gameEvent.Description;
                }
            }
            if (!BaconGalaxy.ExecuteEventAction(this, eventAction, triggerEmpire, gameEvent, flag))
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(eventAction.MessageTitle))
            {
                title = eventAction.MessageTitle;
            }
            if (!string.IsNullOrWhiteSpace(eventAction.MessageText))
            {
                text = eventAction.MessageText;
            }
            if (!string.IsNullOrEmpty(text))
            {
                if (empire != null)
                {
                    empire.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, title, text, additionalData, location);
                }
                else
                {
                    triggerEmpire?.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, title, text, additionalData, location);
                }
            }
        }

        private void RemoveNullBuiltObjects()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                if (BuiltObjects[i] == null)
                {
                    list.Add(i);
                }
            }
            if (list.Count > 0)
            {
                for (int num = list.Count - 1; num >= 0; num--)
                {
                    BuiltObjects.RemoveAt(list[num]);
                }
            }
        }

        private void FixResourceMapsByteSplicing(int startIndex, int endIndex, HabitatList newHabitats)
        {
            _Reindexing = true;
            if (newHabitats.Count % 8 != 0)
            {
                throw new ApplicationException("Cannot insert new habitats into resource maps - habitat count is not a multiple of 8");
            }
            List<byte[]> list = new List<byte[]>();
            List<byte[]> list2 = new List<byte[]>();
            List<bool[]> list3 = new List<bool[]>();
            int num = startIndex - newHabitats.Count;
            int num2 = num / 8;
            int afterByteIndex = num2 + 1;
            byte[] before;
            byte[] after;
            bool[] middle;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.ResourceMap != null)
                {
                    RetrieveResourceMapBlocks(empire.ResourceMap, num2, afterByteIndex, out before, out after, out middle);
                    list.Add(before);
                    list2.Add(after);
                    list3.Add(middle);
                }
            }
            if (IndependentEmpire != null && IndependentEmpire.ResourceMap != null)
            {
                RetrieveResourceMapBlocks(IndependentEmpire.ResourceMap, num2, afterByteIndex, out before, out after, out middle);
                list.Add(before);
                list2.Add(after);
                list3.Add(middle);
            }
            for (int j = 0; j < PirateEmpires.Count; j++)
            {
                Empire empire2 = PirateEmpires[j];
                if (empire2 != null && empire2.ResourceMap != null)
                {
                    RetrieveResourceMapBlocks(empire2.ResourceMap, num2, afterByteIndex, out before, out after, out middle);
                    list.Add(before);
                    list2.Add(after);
                    list3.Add(middle);
                }
            }
            ReindexHabitats(startIndex, endIndex, newHabitats.Count, newHabitats);
            int num3 = 0;
            for (int k = 0; k < Empires.Count; k++)
            {
                Empire empire3 = Empires[k];
                if (empire3 != null && empire3.ResourceMap != null)
                {
                    empire3.ResourceMap = RecreateResourceMapFromBlocks(num2, afterByteIndex, list[num3], list2[num3], list3[num3], newHabitats);
                    num3++;
                }
            }
            if (IndependentEmpire != null && IndependentEmpire.ResourceMap != null)
            {
                IndependentEmpire.ResourceMap = RecreateResourceMapFromBlocks(num2, afterByteIndex, list[num3], list2[num3], list3[num3], newHabitats);
                num3++;
            }
            for (int l = 0; l < PirateEmpires.Count; l++)
            {
                Empire empire4 = PirateEmpires[l];
                if (empire4 != null && empire4.ResourceMap != null)
                {
                    empire4.ResourceMap = RecreateResourceMapFromBlocks(num2, afterByteIndex, list[num3], list2[num3], list3[num3], newHabitats);
                    num3++;
                }
            }
            _Reindexing = false;
        }

        private GalaxyResourceMap RecreateResourceMapFromBlocks(int beforeByteIndex, int afterByteIndex, byte[] before, byte[] after, bool[] middle, HabitatList newHabitats)
        {
            GalaxyResourceMap galaxyResourceMap = new GalaxyResourceMap();
            galaxyResourceMap.InitializeFlags(Habitats.Count, this);
            if (newHabitats != null)
            {
                Array.Copy(before, 0, galaxyResourceMap._ResourcesKnown, 0, before.Length);
                int destinationIndex = afterByteIndex + newHabitats.Count / 8;
                Array.Copy(after, 0, galaxyResourceMap._ResourcesKnown, destinationIndex, after.Length);
                int habitatIndex = newHabitats[0].HabitatIndex;
                int num = beforeByteIndex * 8;
                for (int i = 0; i < middle.Length; i++)
                {
                    int num2 = num + i;
                    if (num2 == habitatIndex)
                    {
                        num += newHabitats.Count;
                        num2 = num + i;
                    }
                    galaxyResourceMap.SetResourcesKnownRaw(num2, middle[i]);
                }
            }
            return galaxyResourceMap;
        }

        private void RetrieveResourceMapBlocks(GalaxyResourceMap map, int beforeByteIndex, int afterByteIndex, out byte[] before, out byte[] after, out bool[] middle)
        {
            before = new byte[beforeByteIndex];
            Array.Copy(map._ResourcesKnown, 0, before, 0, beforeByteIndex);
            int num = map._ResourcesKnown.Length - afterByteIndex;
            after = new byte[num];
            Array.Copy(map._ResourcesKnown, afterByteIndex, after, 0, num);
            middle = new bool[8];
            int num2 = beforeByteIndex * 8;
            for (int i = 0; i < 8; i++)
            {
                middle[i] = map.CheckResourcesKnownRaw(num2 + i);
            }
        }

        private void FixResourceMaps(int startIndex, int endIndex, int movement, HabitatList newHabitats)
        {
            _Reindexing = true;
            List<bool[]> list = new List<bool[]>();
            List<bool[]> list2 = new List<bool[]>();
            List<bool[]> list3 = new List<bool[]>();
            for (int i = 0; i < Empires.Count; i++)
            {
                list.Add(new bool[Habitats.Count]);
                Empire empire = Empires[i];
                if (empire.ResourceMap == null)
                {
                    continue;
                }
                if (newHabitats != null)
                {
                    for (int j = 0; j < Habitats.Count; j++)
                    {
                        if (newHabitats.Contains(Habitats[j]))
                        {
                            list[i][j] = false;
                        }
                        else
                        {
                            list[i][j] = empire.ResourceMap.CheckResourcesKnownRaw(Habitats[j]);
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < Habitats.Count; k++)
                    {
                        list[i][k] = empire.ResourceMap.CheckResourcesKnownRaw(Habitats[k]);
                    }
                }
            }
            list3.Add(new bool[Habitats.Count]);
            if (IndependentEmpire.ResourceMap != null)
            {
                if (newHabitats != null)
                {
                    for (int l = 0; l < Habitats.Count; l++)
                    {
                        if (newHabitats.Contains(Habitats[l]))
                        {
                            list3[0][l] = false;
                        }
                        else
                        {
                            list3[0][l] = IndependentEmpire.ResourceMap.CheckResourcesKnownRaw(Habitats[l]);
                        }
                    }
                }
                else
                {
                    for (int m = 0; m < Habitats.Count; m++)
                    {
                        list3[0][m] = IndependentEmpire.ResourceMap.CheckResourcesKnownRaw(Habitats[m]);
                    }
                }
            }
            for (int n = 0; n < PirateEmpires.Count; n++)
            {
                list2.Add(new bool[Habitats.Count]);
                Empire empire2 = PirateEmpires[n];
                if (empire2.ResourceMap == null)
                {
                    continue;
                }
                if (newHabitats != null)
                {
                    for (int num = 0; num < Habitats.Count; num++)
                    {
                        if (newHabitats.Contains(Habitats[num]))
                        {
                            list2[n][num] = false;
                        }
                        else
                        {
                            list2[n][num] = empire2.ResourceMap.CheckResourcesKnownRaw(Habitats[num]);
                        }
                    }
                }
                else
                {
                    for (int num2 = 0; num2 < Habitats.Count; num2++)
                    {
                        list2[n][num2] = empire2.ResourceMap.CheckResourcesKnownRaw(Habitats[num2]);
                    }
                }
            }
            ReindexHabitats(startIndex, endIndex, movement, newHabitats);
            for (int num3 = 0; num3 < Empires.Count; num3++)
            {
                Empire empire3 = Empires[num3];
                GalaxyResourceMap galaxyResourceMap = new GalaxyResourceMap();
                galaxyResourceMap.InitializeFlags(Habitats.Count, this);
                for (int num4 = 0; num4 < Habitats.Count; num4++)
                {
                    galaxyResourceMap.SetResourcesKnownRaw(Habitats[num4], list[num3][num4]);
                }
                empire3.ResourceMap = galaxyResourceMap;
            }
            for (int num5 = 0; num5 < PirateEmpires.Count; num5++)
            {
                Empire empire4 = PirateEmpires[num5];
                if (empire4.ResourceMap != null)
                {
                    GalaxyResourceMap galaxyResourceMap2 = new GalaxyResourceMap();
                    galaxyResourceMap2.InitializeFlags(Habitats.Count, this);
                    for (int num6 = 0; num6 < Habitats.Count; num6++)
                    {
                        galaxyResourceMap2.SetResourcesKnownRaw(Habitats[num6], list2[num5][num6]);
                    }
                    empire4.ResourceMap = galaxyResourceMap2;
                }
            }
            if (IndependentEmpire.ResourceMap != null)
            {
                GalaxyResourceMap galaxyResourceMap3 = new GalaxyResourceMap();
                galaxyResourceMap3.InitializeFlags(Habitats.Count, this);
                for (int num7 = 0; num7 < Habitats.Count; num7++)
                {
                    galaxyResourceMap3.SetResourcesKnownRaw(Habitats[num7], list3[0][num7]);
                }
                IndependentEmpire.ResourceMap = galaxyResourceMap3;
            }
            _Reindexing = false;
        }

        public bool RemoveHabitat(Habitat habitat)
        {
            int num = -1;
            int num2 = -1;
            if (habitat.Category == HabitatCategoryType.Star || habitat.Category == HabitatCategoryType.GasCloud)
            {
                return false;
            }
            if (habitat.Category == HabitatCategoryType.Planet)
            {
                for (int i = 0; i < Habitats.Count; i++)
                {
                    if (Habitats[i].Category == HabitatCategoryType.Moon && Habitats[i].Parent == habitat)
                    {
                        _ = Habitats[i].HasBeenDestroyed;
                    }
                }
            }
            RemoveSingleHabitat(habitat);
            num = habitat.HabitatIndex;
            num2 = Habitats.Count - 1;
            int movement = -1;
            FixResourceMaps(num, num2, movement, null);
            RemoveNullBuiltObjects();
            return true;
        }

        public void RemoveSystem(SystemInfo system)
        {
            Habitat systemStar = system.SystemStar;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            foreach (BuiltObject builtObject in BuiltObjects)
            {
                if (builtObject != null && builtObject.NearestSystemStar == system.SystemStar)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            foreach (BuiltObject item in builtObjectList)
            {
                item.ClearPreviousMissionRequirements();
                item.CompleteTeardown(this, removeFromEmpire: true);
            }
            Creature[] array = system.Creatures.ToArray();
            Creature[] array2 = array;
            foreach (Creature creature in array2)
            {
                creature.CompleteTeardown();
            }
            system.Creatures.Clear();
            int systemIndex = systemStar.SystemIndex;
            int habitatIndex = system.SystemStar.HabitatIndex;
            int movement = -1 * (system.Habitats.Count + 1);
            Habitat[] array3 = system.Habitats.ToArray();
            Habitat[] array4 = array3;
            foreach (Habitat habitat in array4)
            {
                if (habitat.Category != 0 && habitat.Category != HabitatCategoryType.GasCloud)
                {
                    RemoveSingleHabitat(habitat);
                }
            }
            RemoveSingleHabitat(system.SystemStar);
            if (systemIndex >= 0)
            {
                Systems.RemoveAt(systemIndex);
            }
            int endIndex = Habitats.Count - 1;
            FixResourceMaps(habitatIndex, endIndex, movement, null);
            system.Habitats.Clear();
            system.DominantEmpire = null;
            system.OtherEmpires = null;
            system.Sector = null;
            system.SystemStar = null;
            CompactSystemIndexes(systemIndex, systemIndex);
            RemoveNullBuiltObjects();
        }

        private void RemoveSingleHabitat(Habitat habitat)
        {
            habitat.CompleteTeardown();
        }

        public bool RemoveAsteroidField(HabitatList asteroids, Habitat nearestSystemStar)
        {
            if (nearestSystemStar != null && asteroids != null && asteroids.Count > 0)
            {
                _ = nearestSystemStar.SystemIndex;
                int habitatIndex = asteroids[0].HabitatIndex;
                int movement = -1 * asteroids.Count;
                foreach (Habitat asteroid in asteroids)
                {
                    RemoveSingleHabitat(asteroid);
                }
                int endIndex = Habitats.Count - 1;
                FixResourceMaps(habitatIndex, endIndex, movement, null);
                return true;
            }
            return false;
        }

        public bool AddHabitat(Habitat habitat, Habitat nearestSystemStar)
        {
            if (nearestSystemStar != null)
            {
                int num = -1;
                num = (habitat.HabitatIndex = ((Systems[nearestSystemStar.SystemIndex].Habitats.Count <= 0) ? (Systems[nearestSystemStar.SystemIndex].SystemStar.HabitatIndex + 1) : (Systems[nearestSystemStar.SystemIndex].Habitats[Systems[nearestSystemStar.SystemIndex].Habitats.Count - 1].HabitatIndex + 1)));
                habitat.SystemIndex = nearestSystemStar.SystemIndex;
                Habitats.Insert(num, habitat);
                Systems[nearestSystemStar].Habitats.Add(habitat);
                GalaxyIndex galaxyIndex = ResolveIndex(nearestSystemStar.Xpos, nearestSystemStar.Ypos);
                HabitatIndex[galaxyIndex.X][galaxyIndex.Y].Add(habitat);
                int startIndex = habitat.HabitatIndex + 1;
                int endIndex = Habitats.Count - 1;
                int movement = 1;
                HabitatList habitatList = new HabitatList();
                habitatList.Add(habitat);
                FixResourceMaps(startIndex, endIndex, movement, habitatList);
                SetSystemHabitatExploration(habitat, nearestSystemStar);
                return true;
            }
            return false;
        }

        public bool AddSystem(SystemInfo system)
        {
            if (system.SystemStar == null)
            {
                return false;
            }
            Habitat habitat = FindNearestSystemGasCloudAsteroid(system.SystemStar.Xpos, system.SystemStar.Ypos);
            if (habitat != null)
            {
                double num = CalculateDistance(system.SystemStar.Xpos, system.SystemStar.Ypos, habitat.Xpos, habitat.Ypos);
                if (num < (double)(MaxSolarSystemSize * 4))
                {
                    return false;
                }
            }
            int count = Systems.Count;
            int num2 = 0;
            if (Habitats.Count > 0)
            {
                num2 = Habitats[Habitats.Count - 1].HabitatIndex + 1;
            }
            GalaxyIndex galaxyIndex = ResolveIndex(system.SystemStar.Xpos, system.SystemStar.Ypos);
            system.SystemStar.SystemIndex = count;
            system.SystemStar.HabitatIndex = num2;
            for (int i = 0; i < system.Habitats.Count; i++)
            {
                system.Habitats[i].SystemIndex = count;
                system.Habitats[i].HabitatIndex = num2 + i + 1;
            }
            Habitats.Add(system.SystemStar);
            HabitatIndex[galaxyIndex.X][galaxyIndex.Y].Add(system.SystemStar);
            if (system.Habitats != null && system.Habitats.Count > 0)
            {
                Habitats.AddRange(system.Habitats);
                HabitatIndex[galaxyIndex.X][galaxyIndex.Y].AddRange(system.Habitats);
            }
            system = DetermineSystemInfo(system, null, null, null);
            int endIndex = num2;
            int movement = 0;
            HabitatList habitatList = new HabitatList();
            habitatList.Add(system.SystemStar);
            if (system.Habitats != null && system.Habitats.Count > 0)
            {
                habitatList.AddRange(system.Habitats);
            }
            FixResourceMaps(num2, endIndex, movement, habitatList);
            Systems.Add(system);
            GalaxyIndex galaxyIndex2 = ResolveIndex(system.SystemStar.Xpos, system.SystemStar.Ypos);
            SystemsIndex[galaxyIndex2.X][galaxyIndex2.Y].Add(system);
            AddSystemToEmpires(system);
            return true;
        }

        private void AddSystemToEmpires(SystemInfo system)
        {
            foreach (Empire empire in Empires)
            {
                SystemVisibility systemVisibility = new SystemVisibility();
                systemVisibility.Status = SystemVisibilityStatus.Unexplored;
                systemVisibility.SystemStar = system.SystemStar;
                empire.SystemVisibility.Add(systemVisibility);
            }
            foreach (Empire pirateEmpire in PirateEmpires)
            {
                SystemVisibility systemVisibility2 = new SystemVisibility();
                systemVisibility2.Status = SystemVisibilityStatus.Unexplored;
                systemVisibility2.SystemStar = system.SystemStar;
                pirateEmpire.SystemVisibility.Add(systemVisibility2);
            }
            if (IndependentEmpire != null && IndependentEmpire.SystemVisibility != null)
            {
                SystemVisibility systemVisibility3 = new SystemVisibility();
                systemVisibility3.Status = SystemVisibilityStatus.Unexplored;
                systemVisibility3.SystemStar = system.SystemStar;
                IndependentEmpire.SystemVisibility.Add(systemVisibility3);
            }
        }

        private void SetSystemHabitatExploration(Habitat systemHabitat, Habitat systemStar)
        {
            HabitatList habitatList = new HabitatList();
            habitatList.Add(systemHabitat);
            SetSystemHabitatsExploration(habitatList, systemStar);
        }

        private void SetSystemHabitatsExploration(HabitatList systemHabitats, Habitat systemStar)
        {
            foreach (Empire empire in Empires)
            {
                if (empire.ResourceMap == null)
                {
                    continue;
                }
                SystemVisibilityStatus status = empire.SystemVisibility[systemStar.SystemIndex].Status;
                if (status != SystemVisibilityStatus.Explored && status != SystemVisibilityStatus.Visible)
                {
                    continue;
                }
                foreach (Habitat systemHabitat in systemHabitats)
                {
                    empire.ResourceMap.SetResourcesKnown(systemHabitat, known: true);
                }
            }
            foreach (Empire pirateEmpire in PirateEmpires)
            {
                if (pirateEmpire.SystemVisibility == null || pirateEmpire.ResourceMap == null)
                {
                    continue;
                }
                SystemVisibilityStatus status2 = pirateEmpire.SystemVisibility[systemStar.SystemIndex].Status;
                if (status2 != SystemVisibilityStatus.Explored && status2 != SystemVisibilityStatus.Visible)
                {
                    continue;
                }
                foreach (Habitat systemHabitat2 in systemHabitats)
                {
                    pirateEmpire.ResourceMap.SetResourcesKnown(systemHabitat2, known: true);
                }
            }
            if (IndependentEmpire.SystemVisibility == null || IndependentEmpire.ResourceMap == null)
            {
                return;
            }
            SystemVisibilityStatus status3 = IndependentEmpire.SystemVisibility[systemStar.SystemIndex].Status;
            if (status3 != SystemVisibilityStatus.Explored && status3 != SystemVisibilityStatus.Visible)
            {
                return;
            }
            foreach (Habitat systemHabitat3 in systemHabitats)
            {
                IndependentEmpire.ResourceMap.SetResourcesKnown(systemHabitat3, known: true);
            }
        }

        public static int ResolveHyperDriveIndex(Component component)
        {
            return component.SpecialImageIndex;
        }

        private string GenerateGoldAsteroidName(Habitat sun)
        {
            string[] array = new string[7] { "Concealed", "Lost", "Golden", "Precious", "Glittering", "Hidden", "Miner's" };
            string[] array2 = new string[9] { "Hoard", "Rock", "Treasure", "Nugget", "Fortune", "Folly", "Legend", "Star", "Prize" };
            string empty = string.Empty;
            if (sun != null && sun.Type != HabitatType.SuperNova && Rnd.Next(0, 3) == 1)
            {
                if (Rnd.Next(0, 2) == 1)
                {
                    return sun.Name + " " + array2[Rnd.Next(0, array2.Length)];
                }
                return array2[Rnd.Next(0, array2.Length)] + " of " + sun.Name;
            }
            return array[Rnd.Next(0, array.Length)] + " " + array2[Rnd.Next(0, array2.Length)];
        }

        private string GenerateCrystalAsteroidName(Habitat sun)
        {
            string[] array = new string[8] { "Concealed", "Lost", "Shining", "Precious", "Glittering", "Hidden", "Miner's", "Crystal" };
            string[] array2 = new string[10] { "Hoard", "Rock", "Treasure", "Jewel", "Fortune", "Folly", "Legend", "Star", "Prize", "Gem" };
            string empty = string.Empty;
            if (sun != null && sun.Type != HabitatType.SuperNova && Rnd.Next(0, 3) == 1)
            {
                if (Rnd.Next(0, 2) == 1)
                {
                    return sun.Name + " " + array2[Rnd.Next(0, array2.Length)];
                }
                return array2[Rnd.Next(0, array2.Length)] + " of " + sun.Name;
            }
            return array[Rnd.Next(0, array.Length)] + " " + array2[Rnd.Next(0, array2.Length)];
        }

        public Habitat GenerateTreasureAsteroid(Habitat sun, double orbitAngle, int orbitDistance, bool orbitDirection, int orbitSpeed, bool doInitialMove)
        {
            Habitat habitat = null;
            ResourceDefinition byName = ResourceSystem.Resources.GetByName("Gold");
            ResourceDefinition byName2 = ResourceSystem.Resources.GetByName("Dilithium Crystal");
            if (byName != null || byName2 != null)
            {
                habitat = new Habitat(this, HabitatCategoryType.Asteroid, HabitatType.Metal, TextResolver.GetText("Asteroid"), sun, orbitAngle, orbitDirection, orbitDistance, orbitSpeed, doInitialMove);
                habitat.Diameter = (short)Rnd.Next(35, 50);
                habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.LandscapeImageCountBarrenRock));
                if (habitat.Resources == null)
                {
                    habitat.Resources = new HabitatResourceList();
                }
                short abundance = (short)Rnd.Next(800, 1000);
                if (byName2 == null)
                {
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetAsteroidsGold + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsGold));
                    habitat.Resources.Add(new HabitatResource(byName.ResourceID, abundance));
                    habitat.Name = GenerateGoldAsteroidName(sun);
                }
                else if (byName == null)
                {
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetAsteroidsCrystal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsCrystal));
                    habitat.Resources.Add(new HabitatResource(byName2.ResourceID, abundance));
                    habitat.Name = GenerateCrystalAsteroidName(sun);
                }
                else if (Rnd.Next(0, 2) == 1)
                {
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetAsteroidsGold + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsGold));
                    habitat.Resources.Add(new HabitatResource(byName.ResourceID, abundance));
                    habitat.Name = GenerateGoldAsteroidName(sun);
                }
                else
                {
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetAsteroidsCrystal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsCrystal));
                    habitat.Resources.Add(new HabitatResource(byName2.ResourceID, abundance));
                    habitat.Name = GenerateCrystalAsteroidName(sun);
                }
                habitat.ScenicFactor = (float)(0.3 + Rnd.NextDouble() * 0.3);
            }
            return habitat;
        }

        public HabitatList GenerateAsteroidField(int asteroidCount, double x, double y, bool orbitDirection, int orbitSpeed, double distanceSpreadFactor, double arcSpreadFactor, ResourceDefinitionList randomOrderedResources)
        {
            Habitat habitat = FindNearestSystemGasCloudAsteroid(x, y);
            int orbitDistance = (int)CalculateDistance(habitat.Xpos, habitat.Ypos, x, y);
            return GenerateAsteroidField(asteroidCount, x, y, habitat, orbitDirection, orbitSpeed, orbitDistance, distanceSpreadFactor, arcSpreadFactor, HabitatType.BarrenRock, randomOrderedResources);
        }

        public HabitatList GenerateAsteroidField(int asteroidCount, double x, double y, bool orbitDirection, int orbitSpeed, double distanceSpreadFactor, double arcSpreadFactor)
        {
            Habitat habitat = FindNearestSystemGasCloudAsteroid(x, y);
            int orbitDistance = (int)CalculateDistance(habitat.Xpos, habitat.Ypos, x, y);
            return GenerateAsteroidField(asteroidCount, x, y, habitat, orbitDirection, orbitSpeed, orbitDistance, distanceSpreadFactor, arcSpreadFactor, HabitatType.BarrenRock);
        }

        public HabitatList GenerateAsteroidField(int asteroidCount, double x, double y, Habitat nearestSystemStar, bool orbitDirection, int orbitSpeed, int orbitDistance, double distanceSpreadFactor, double arcSpreadFactor, HabitatType type)
        {
            return GenerateAsteroidField(asteroidCount, x, y, nearestSystemStar, orbitDirection, orbitSpeed, orbitDistance, distanceSpreadFactor, arcSpreadFactor, type, null);
        }

        public HabitatList GenerateAsteroidField(int asteroidCount, double x, double y, Habitat nearestSystemStar, bool orbitDirection, int orbitSpeed, int orbitDistance, double distanceSpreadFactor, double arcSpreadFactor, HabitatType type, ResourceDefinitionList randomOrderedResources)
        {
            HabitatList habitatList = new HabitatList();
            double num = CalculateAngleFromCoords(x, y, nearestSystemStar.Xpos, nearestSystemStar.Ypos, orbitDistance);
            double num2 = arcSpreadFactor * arcSpreadFactor;
            double val = (double)(MaxSolarSystemSize - orbitDistance) / (double)(MaxSolarSystemSize / 3);
            val = Math.Min(3.0, Math.Max(0.3, val));
            num2 *= val;
            double num3 = Math.Max(0.06, 0.13 * ((double)asteroidCount / 350.0));
            double num4 = Math.Max(250.0, 500.0 * ((double)asteroidCount / 350.0) * distanceSpreadFactor);
            double num5 = 0.4;
            double num6 = num5 * -1.0;
            double num7 = (double)orbitDistance + num6 * num4 * distanceSpreadFactor;
            double num8 = (double)orbitDistance + num5 * num4 * distanceSpreadFactor;
            double num9 = num + num6 * num3 * num2;
            double num10 = num + num5 * num3 * num2;
            if (num9 > num10)
            {
                double num11 = num10;
                num10 = num9;
                num9 = num11;
            }
            for (int i = 0; i < asteroidCount; i++)
            {
                int num12 = Rnd.Next(10, 25);
                if (Rnd.Next(0, 30) == 5)
                {
                    num12 = Rnd.Next(26, 45);
                }
                int num13 = GalaxyImages.HabitatImageOffsetAsteroidsNormal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsNormal);
                int num14 = orbitDistance + (int)((Rnd.NextDouble() - 0.5) * Rnd.NextDouble() * 2.0 * num4 * distanceSpreadFactor);
                double num15 = num + (Rnd.NextDouble() - 0.5) * Rnd.NextDouble() * 2.0 * num3 * num2;
                if ((double)num14 > num7 && (double)num14 < num8 && num15 > num9 && num15 < num10)
                {
                    double num16 = Rnd.NextDouble() * 0.8 - 0.4;
                    double num17 = Rnd.NextDouble() * 0.8 - 0.4;
                    num14 = orbitDistance + (int)(num16 * num4 * distanceSpreadFactor);
                    num15 = num + num17 * num3 * num2;
                }
                string name = GenerateCodeName() + ", " + TextResolver.GetText("Asteroid Field");
                Habitat habitat = new Habitat(this, HabitatCategoryType.Asteroid, HabitatType.BarrenRock, name, nearestSystemStar, num15, orbitDirection, num14, orbitSpeed, doInitialMove: false);
                habitat.Diameter = (short)num12;
                habitat.PictureRef = (short)num13;
                habitat.LandscapePictureRef = -1;
                int minimumResourceCount = 0;
                if (type == HabitatType.Metal && Rnd.Next(0, 3) > 0)
                {
                    minimumResourceCount = 1;
                }
                habitat = SelectResources(habitat, minimumResourceCount, null, 0, randomOrderedResources);
                habitat.Type = type;
                SelectHabitatPictures(habitat);
                if (Rnd.Next(0, 1300) == 1)
                {
                    habitat = GenerateTreasureAsteroid(nearestSystemStar, num15, num14, orbitDirection, orbitSpeed, doInitialMove: false);
                }
                habitatList.Add(habitat);
            }
            return habitatList;
        }

        public bool AddAsteroidField(HabitatList asteroids, Habitat nearestSystemStar)
        {
            if (nearestSystemStar != null && asteroids != null && asteroids.Count > 0)
            {
                int num = Habitats.Count;
                int num2 = Habitats.IndexOf(nearestSystemStar);
                for (int i = num2 + 1; i < Habitats.Count; i++)
                {
                    if (Habitats[i].Parent == null)
                    {
                        num = i;
                        break;
                    }
                }
                GalaxyIndex galaxyIndex = ResolveIndex(nearestSystemStar.Xpos, nearestSystemStar.Ypos);
                for (int j = 0; j < asteroids.Count; j++)
                {
                    asteroids[j].HabitatIndex = num + j;
                    asteroids[j].SystemIndex = nearestSystemStar.SystemIndex;
                }
                Habitats.InsertRange(num, asteroids);
                Systems[nearestSystemStar].Habitats.AddRange(asteroids);
                HabitatIndex[galaxyIndex.X][galaxyIndex.Y].AddRange(asteroids);
                int startIndex = num + asteroids.Count;
                int endIndex = Habitats.Count - 1;
                int count = asteroids.Count;
                if (asteroids.Count % 8 == 0)
                {
                    FixResourceMapsByteSplicing(startIndex, endIndex, asteroids);
                }
                else
                {
                    FixResourceMaps(startIndex, endIndex, count, asteroids);
                }
                SetSystemHabitatsExploration(asteroids, nearestSystemStar);
                return true;
            }
            return false;
        }

        private void CompactSystemIndexes(int startIndex, int endIndex)
        {
            int num = endIndex - startIndex + 1;
            for (int i = 0; i < Habitats.Count; i++)
            {
                if (Habitats[i].SystemIndex >= startIndex)
                {
                    Habitats[i].SystemIndex -= num;
                }
            }
        }

        private void ReindexHabitats(int startIndex, int endIndex, int movement, HabitatList newHabitats)
        {
            if (newHabitats == null || newHabitats.Count == 0)
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (i < Habitats.Count)
                    {
                        Habitats[i].HabitatIndex += movement;
                    }
                }
                return;
            }
            for (int j = startIndex; j <= endIndex; j++)
            {
                if (j < Habitats.Count && !newHabitats.Contains(Habitats[j]))
                {
                    Habitats[j].HabitatIndex += movement;
                }
            }
        }

        public static void TEST_SteamAchievements(string applicationStartupPath)
        {
            SteamAPI.Initialize(applicationStartupPath);
            SteamAPI.SetAchievementIfNecessary(new Achievement(AchievementType.OwnOperationalPlanetDestroyer, 0, null));
            SteamAPI.Shutdown();
        }
    }
}
