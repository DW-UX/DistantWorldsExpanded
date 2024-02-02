// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconBuiltObject
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Controls;
using DistantWorlds.Types;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ExpansionMod.Objects;
using BaconDistantWorlds.HotKeys;
using BaconDistantWorlds.Forms;

namespace BaconDistantWorlds
{
    public static class BaconBuiltObject
    {
        static class class69
        {
            public static CallSite<Action<CallSite, Type, object, string, object[]>> callSite69;
        }
        static class class76
        {
            public static CallSite<Func<CallSite, object, MethodInfo>> p2;
            public static CallSite<Func<CallSite, object, string, BindingFlags, object>> p1;
            public static CallSite<Func<CallSite, object, object>> p0;
            public static CallSite<Func<CallSite, MethodInfo, object, object[], object>> p3;
        }
        public static Main myMain;
        public static double myDamageMultiplier = 3.0;
        private static int myArmorReactivityMultiplier = 2;
        private static int myCargoBayCapacityMultiplier = 5;
        private static double myAssaultPodStrengthMultiplier = 6.0;
        public static bool useStarGravityWells = true;
        public static bool smallShipsJumpSooner = false;
        public static float sublightFuelBurnDivisor = 1f;
        public static float noFuelCruiseSpeedMultiplier = 0.33f;
        public static float noFuelTopSpeedMultiplier = 0.33f;
        public static float noFuelHyperSpeedMultiplier = 0.33f;
        public static float weaponRangeMultiplierForBases = 1f;
        public static Empire targetEmpireForSpy = (Empire)null;
        public static Habitat temporaryHabitat = (Habitat)null;
        public static int scientificDataForResourceSurvey = 3;
        public static int scientificDataForRuins = 90;
        public static BuiltObjectMission immMission = (BuiltObjectMission)null;
        public static int freighterRepeatMissionCount = 9;
        public static int debugCounter = 0;
        public static int debugValueOne = 0;
        public static double starGravityWellRangeSquared = (double)Galaxy.MaxSolarSystemSize * (double)Galaxy.MaxSolarSystemSize;
        public static List<BuiltObjectMissionType> debugListOfMissions = new List<BuiltObjectMissionType>();
        public static int shipFreeRepairTimeFromCrewSkillAverage = 160;
        public static int shipFreeRepairTimeFromCrewSkillExperienced = 120;
        public static int shipFreeRepairTimeFromCrewSkillVeteran = 80;
        public static int shipFreeRepairTimeFromCrewSkillElite = 40;
        public static int shipFreeRepairTimeFromCrewSkillLegendary = 20;
        public static string fighterBayLabel = "fighter";
        public static string bomberBayLabel = "bomber";
        public static string mixedBayLabel = "assault";
        public static bool pointDefenseAffectsMissiles = true;
        public static bool limitNewFighterBuildToColonies = false;
        public static int orbitalAsteroidCost = 10000;
        public static double privateBuildCostToStateMoney = 1.0;
        public static Dictionary<string, CargoList> shipsToBeDestroyed = new Dictionary<string, CargoList>();
        public static string tailGunnerResearch = "Point Defense Weapons";
        public static BuiltObject theShip;
        //public static object globalCargoMissionSource = (object)null;
        //public static object globalCargoMissionDestination = (object)null;
        public static StellarObject fighterTarget = (StellarObject)null;
        public static bool AllowPrivateShipAssigment = false;

        public static bool IsMyShip(BuiltObject ship) => ship != null && ship.Empire != null && ship.Empire.Name.Contains("Romulan") || ship != null && ship.ActualEmpire != null && ship.ActualEmpire.Name.Contains("Romulan") || ship != null && ship.Owner != null && ship.Owner.Name.Contains("Romulan");

        public static double InflictDamage(BuiltObject firingShip)
        {
            double num = 1.0;
            if (BaconBuiltObject.IsMyShip(firingShip))
                num = BaconBuiltObject.myDamageMultiplier;
            return num;
        }

        public static int ArmorReactivityMultiplier(BuiltObject target)
        {
            int num = 1;
            if (BaconBuiltObject.IsMyShip(target))
                num = BaconBuiltObject.myArmorReactivityMultiplier;
            return num;
        }

        public static int CargoBayCapacityMultiplier(BuiltObject target)
        {
            int num = 1;
            if (target != null && target.Empire != null && target.Empire.Name.Contains("Romulan"))
                num = BaconBuiltObject.myCargoBayCapacityMultiplier;
            return num;
        }

        public static double AssaultPodStrengthMultiplier(BuiltObject firingShip)
        {
            double num = 1.0;
            if (BaconBuiltObject.IsMyShip(firingShip))
                num = BaconBuiltObject.myAssaultPodStrengthMultiplier;
            return num;
        }

        public static MessageBoxEx method_372(string string_30, string string_31)
        {
            MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string)null, new Font("Verdana", 9f, FontStyle.Regular));
            string_30 = string_30.Replace("\n", Environment.NewLine);
            messageBox.Text = string_30;
            messageBox.Caption = string_31;
            messageBox.AddButton(MessageBoxExButtons.Yes);
            messageBox.AddButton(MessageBoxExButtons.No);
            messageBox.Icon = MessageBoxExIcon.Question;
            messageBox.Show();
            return messageBox;
        }

        public static void BaconKeyboardInput(KeyEventArgs e, List<Keys> keys)
        {
            if (BaconMain.settingsInitialized)
            {
                var hotKeyManager = BaconMain.EntryPointClass.GetHotKeyManager() as HotKeyManager;
                //hotKeyManager.GetMappedTargetByKeyCode(e.KeyCode, out KeyMappingTarget target);

                MappedHotKey target;
                if (hotKeyManager.GetMappedTarget(keys, out target))
                {
                    //switch (keyCode)
                    //{
                    //case Keys.Return:
                    int targetId;
                    if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ShowDetailedInfo, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {

                        //if (e.Control)
                        {
                            BaconBuiltObject.ShowDetailedInformation(BaconBuiltObject.myMain);
                            e.Handled = true;
                        }
                    }
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ShowMissionCommand, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.ShowMissionCommands(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                    }
                    //case Keys.D3:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.AssignCargoMission, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.AssignCargoMission(BaconBuiltObject.myMain, null, false);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.D4:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.SetFighterTarget, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.fighterTarget = BaconBuiltObject.myMain._Game.SelectedObject as StellarObject;
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.D5:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.OrderBombersToAttackAll, out targetId) &&
                    targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.OrderBombersToAttack(BaconBuiltObject.myMain, BaconBuiltObject.myMain._Game.SelectedObject, true);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.D6:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.OrderBombersToAttack, out targetId) &&
                targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.OrderBombersToAttack(BaconBuiltObject.myMain, BaconBuiltObject.myMain._Game.SelectedObject, false);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.D7:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.OrderBombersToAttack, out targetId) &&
            targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.TransferFighter(BaconBuiltObject.myMain, BaconBuiltObject.myMain._Game.SelectedObject);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.A:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ToggleAutomateCarrierOps, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.ToggleAutomateCarrierOps(BaconBuiltObject.myMain, BaconBuiltObject.myMain._Game.SelectedObject);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ToggleShipAutoBaconImpl, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        if (BaconBuiltObject.myMain._Game.SelectedObject is BuiltObject && (BaconBuiltObject.myMain._Game.SelectedObject as BuiltObject).Empire == BaconBuiltObject.myMain._Game.PlayerEmpire && (BaconBuiltObject.myMain._Game.SelectedObject as BuiltObject).IsAutoControlled)
                        {
                            (BaconBuiltObject.myMain._Game.SelectedObject as BuiltObject).IsAutoControlled = false;
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.B:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ShowCustomBomberForm, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            if (!BaconMain.customBomberFormOpen)
                            {
                                BaconMain.customBomberFormOpen = true;
                                BaconEmpire.ShowCustomBomberForm(BaconBuiltObject.myMain);
                            }
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.C:
                    //            else if (hotKeyManager.ResolveTargetFriendlyName(, out targetId) &&
                    //targetId == target.Parent.TargetMethodId)
                    //            {
                    //                if (e.Control)
                    //                {
                    //                    e.Handled = true;
                    //                    //break;
                    //                }
                    //                //break;
                    //            }
                    //case Keys.D:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.CalculateDistance, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            BaconBuiltObject.CalculateDistance(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.E:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ShipFinder, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            BaconBuiltObject.ShipFinder(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.F:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.FixExplorerCurrentSystem, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            if (BaconBuiltObject.myMain._Game.SelectedObject is BuiltObject selectedObject)
                            {
                                BaconBuiltObject.IsOutsideStarGravityWell(selectedObject);
                                if (selectedObject.NearestSystemStar != null)
                                {
                                    BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, selectedObject.Name + " is in system " + selectedObject.NearestSystemStar.Name);
                                    selectedObject.ActualEmpire.ResolveSystemVisibility(selectedObject, false);
                                }
                                else
                                    BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, selectedObject.Name + " is not in a system.");
                            }
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.M:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.AssignMiningShipToTarget, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.AssignMiningShipToTarget(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.P:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ShowPrisonForm, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            if (!BaconMain.prisonFormOpen)
                            {
                                BaconMain.prisonFormOpen = true;
                                BaconEmpire.ShowPrisonForm(BaconBuiltObject.myMain);
                            }
                            else if (BaconMain.prisonForm != null)
                                BaconMain.prisonForm.BringToFront();
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.Q:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.IncreaseDockingBayCapacity, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            BaconBuiltObject.IncreaseDockingBayCapacity(BaconBuiltObject.myMain);
                            //break;
                        }
                        //break;
                    }
                    //case Keys.R:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ShowDamagedComponents, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            BaconBuiltObject.ShowDamagedComponents(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                    }
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.RevealIfPirate, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.RevealIfPirate(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                    }
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.RushStateShips, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Shift)
                        {
                            BaconBuiltObject.RushStateShips(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.S:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ShowStats, out targetId) &&
                    targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            BaconBuiltObject.ShowStats(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.T:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.AddShipToTradeList, out targetId) &&
                targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            BaconBuiltObject.AddShipToTradeList(BaconBuiltObject.myMain);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.U:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.ForceUnloadAtDestination, out targetId) &&
           targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.ForceUnloadAtDestination(BaconBuiltObject.myMain, BaconBuiltObject.myMain._Game.SelectedObject as BuiltObject);
                            e.Handled = true;
                            //break;
                        }
                        //break;
                    }
                    //case Keys.W:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.AssignPassengershipMission, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Control)
                        {
                            BaconBuiltObject.AssignPassengershipMission(BaconBuiltObject.myMain, (object)null, false);
                            //break;
                        }
                        //break;
                    }
                    //case Keys.OemSemicolon:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.CycleSelectedByRoleBackward, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            if (BaconBuiltObject.myMain._Game.SelectedObject is BuiltObject && (BaconBuiltObject.myMain._Game.SelectedObject as BuiltObject).ActualEmpire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                                BaconBuiltObject.CycleShipSelected(BaconBuiltObject.myMain, "backward");
                            else if (BaconBuiltObject.myMain._Game.SelectedObject is Fighter && (BaconBuiltObject.myMain._Game.SelectedObject as Fighter).ParentBuiltObject.ActualEmpire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                                BaconBuiltObject.CycleFighterSelected(BaconBuiltObject.myMain, "backward");
                            else if (BaconBuiltObject.myMain._Game.SelectedObject is ShipGroup && (BaconBuiltObject.myMain._Game.SelectedObject as ShipGroup).Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                                BaconBuiltObject.CycleFleetSelected(BaconBuiltObject.myMain, "backward");
                            //break;
                        }
                        //break;
                    }
                    //case Keys.OemQuestion:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.GetParentCarrier, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            BaconBuiltObject.GetParentCarrier(BaconBuiltObject.myMain);
                            //break;
                        }
                        //break;
                    }
                    //case Keys.OemQuotes:
                    else if (hotKeyManager.ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames.CycleSelectedByRoleForward, out targetId) &&
        targetId == target.Parent.TargetMethodId)
                    {
                        //if (e.Alt)
                        {
                            if (BaconBuiltObject.myMain._Game.SelectedObject is BuiltObject && (BaconBuiltObject.myMain._Game.SelectedObject as BuiltObject).ActualEmpire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                                BaconBuiltObject.CycleShipSelected(BaconBuiltObject.myMain, "forward");
                            else if (BaconBuiltObject.myMain._Game.SelectedObject is Fighter && (BaconBuiltObject.myMain._Game.SelectedObject as Fighter).ParentBuiltObject.ActualEmpire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                                BaconBuiltObject.CycleFighterSelected(BaconBuiltObject.myMain, "forward");
                            else if (BaconBuiltObject.myMain._Game.SelectedObject is ShipGroup && (BaconBuiltObject.myMain._Game.SelectedObject as ShipGroup).Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                                BaconBuiltObject.CycleFleetSelected(BaconBuiltObject.myMain, "forward");
                            //break;
                        }
                    }
                }
                //break;
                //}
            }
        }

        public static void IncreaseDockingBayCapacity(Main main)
        {
            if (!(main._Game.SelectedObject is BuiltObject))
                return;
            BuiltObject selectedObject = (BuiltObject)main._Game.SelectedObject;
            if (selectedObject.Empire != null && BaconBuiltObject.IsMyShip(selectedObject) && selectedObject.Name == "sdkfhsdkfhsdfjkh")
            {
                DockingBayList dockingBays = selectedObject.DockingBays;
                if (dockingBays != null)
                {
                    foreach (DockingBay dockingBay in (SyncList<DockingBay>)dockingBays)
                    {
                        if (dockingBay.Capacity < 2000)
                            dockingBay._Capacity *= 100;
                    }
                }
            }
        }

        public static void RushStateShips(Main main)
        {
            if (main._Game.SelectedObject is BuiltObject selectedObject
                && selectedObject.ConstructionQueue != null
                && selectedObject.ConstructionQueue.ConstructionWaitQueue != null)
            {
                List<BuiltObject> builtObjectList = new List<BuiltObject>();
                for (int index = 1; index < selectedObject.ConstructionQueue.ConstructionWaitQueue.Count; ++index)
                {
                    if (selectedObject.ConstructionQueue.ConstructionWaitQueue[index] != null && selectedObject.ActualEmpire.BuiltObjects.Contains(selectedObject.ConstructionQueue.ConstructionWaitQueue[index]))
                        builtObjectList.Add(selectedObject.ConstructionQueue.ConstructionWaitQueue[index]);
                }
                for (int index = 0; index < builtObjectList.Count; ++index)
                    selectedObject.ConstructionQueue.ConstructionWaitQueue.Remove(builtObjectList[index]);
                builtObjectList.Reverse();
                for (int index = 0; index < builtObjectList.Count; ++index)
                    selectedObject.ConstructionQueue.ConstructionWaitQueue.Insert(0, builtObjectList[index]);
            }
        }

        public static void AddShipToTradeList(Main main)
        {
            if (!(main._Game.SelectedObject is BuiltObject))
                return;
            BuiltObject selectedObject = (BuiltObject)main._Game.SelectedObject;
            if (selectedObject.ActualEmpire == main._Game.PlayerEmpire && !BaconGalaxy.manualTradeItems.Contains(selectedObject))
                BaconGalaxy.manualTradeItems.Add(selectedObject);
        }

        public static void ShipSpeedToggle(Main main, BuiltObject ship)
        {
            if (ship == null || ship.Mission == null || ship.ActualEmpire != main._Game.PlayerEmpire && !main._Game.PlayerEmpire.Name.Contains("Romulan"))
                return;
            Command command1 = ship.Mission.FastPeekCurrentCommand();
            Command command2 = command1.Clone();
            if (command1.Action == CommandAction.MoveTo)
            {
                command2.Action = CommandAction.SprintTo;
                ship.PreferredSpeed = (float)ship.TopSpeed;
                ship.TargetSpeed = (int)ship.TopSpeed;
            }
            else
            {
                if (command1.Action != CommandAction.SprintTo)
                    return;
                command2.Action = CommandAction.MoveTo;
                ship.PreferredSpeed = (float)ship.CruiseSpeed;
                ship.TargetSpeed = (int)ship.CruiseSpeed;
            }
            ship.Mission.CompleteCommand();
            ship.Mission.InsertCommandAtTop(command2);
        }

        public static void SpreadPop(Main main)
        {
            if (!(main._Game.SelectedObject is Habitat selectedObject))
                return;
            Empire empire = selectedObject.Empire;
            if (empire.DominantRace == null)
                return;
            Race targetRace = empire.DominantRace;
            long num1 = 0;
            foreach (Habitat colony in (SyncList<Habitat>)empire.Colonies)
            {
                Population population = colony.Population.FirstOrDefault<Population>((Func<Population, bool>)(x => x.Race == targetRace));
                if (population != null)
                {
                    num1 += population.Amount;
                    colony.Population.Remove(population);
                }
            }
            while (num1 > 1000L)
            {
                long num2 = num1 / (long)empire.Colonies.Count<Habitat>();
                foreach (Habitat colony in (SyncList<Habitat>)empire.Colonies)
                {
                    Population population1 = colony.Population.FirstOrDefault<Population>((Func<Population, bool>)(x => x.Race == targetRace));
                    long num3 = 0;
                    if (population1 != null)
                        num3 = population1.Amount;
                    long maximumPopulation = colony.MaximumPopulation;
                    long num4 = 0;
                    foreach (Population population2 in (SyncList<Population>)colony.Population)
                        num4 += population2.Amount;
                    long amount = Math.Max(Math.Min(maximumPopulation - num4, num2 + num3), 0L) - num3;
                    if (amount > 0L)
                    {
                        if (population1 != null)
                        {
                            population1.Amount += amount;
                        }
                        else
                        {
                            Population population3 = new Population(targetRace, amount);
                            colony.Population.Add(population3);
                        }
                        num1 -= amount;
                    }
                }
            }
        }

        public static void AddTroopsToColony(Main main, int troopsToAdd)
        {
            for (int index = 0; index < troopsToAdd; ++index)
                main.btnColonyTroopsRecruit_Click((object)null, (EventArgs)null);
        }

        public static void AssignSpiesToMission(Main main, int months)
        {
            int num1;
            switch (months)
            {
                case 3:
                    num1 = 4;
                    break;
                case 12:
                    num1 = 1;
                    break;
                default:
                    num1 = 12;
                    break;
            }
            Empire playerEmpire = main._Game.PlayerEmpire;
            if (BaconBuiltObject.targetEmpireForSpy == null || BaconBuiltObject.targetEmpireForSpy == playerEmpire)
                return;
            List<Character> list = playerEmpire.Characters.Where<Character>((Func<Character, bool>)(x => x.Role == CharacterRole.IntelligenceAgent)).ToList<Character>();
            List<Character> characterList = new List<Character>();
            foreach (Character character in list)
            {
                if (character.Mission != null && character.Mission.Type != IntelligenceMissionType.CounterIntelligence)
                    characterList.Add(character);
            }
            foreach (Character character in characterList)
                list.Remove(character);
            Character character1 = list.OrderByDescending<Character, int>((Func<Character, int>)(x => x.GetSkillLevel(CharacterSkillType.Espionage))).ToList<Character>().FirstOrDefault<Character>();
            if (character1 == null)
                return;
            ResearchNodeList researchNodeList = playerEmpire.Research.ResolveMoreAdvancedProjects(BaconBuiltObject.targetEmpireForSpy, false);
            if (researchNodeList.Count == 0)
                return;
            int index = Galaxy.Rnd.Next(0, researchNodeList.Count);
            IntelligenceMission intelligenceMission = new IntelligenceMission(playerEmpire, (Character)null, main._Game.Galaxy.CurrentStarDate, BaconBuiltObject.targetEmpireForSpy, (ResearchNode)null);
            intelligenceMission.ResetResearchProject(researchNodeList[index]);
            int num2 = playerEmpire == null ? 0 : (playerEmpire.Name.Contains("Romulan") ? 1 : 0);
            intelligenceMission.TimeLength = num2 == 0 ? (long)(Galaxy.RealSecondsInGalacticYear * 1000 / num1) : (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 24);
            intelligenceMission.Agent = character1;
            character1.Mission = intelligenceMission;
        }

        public static void ClearOrdersForShip(Main main)
        {
            if (!(main._Game.SelectedObject is BuiltObject selectedObject) || selectedObject.Mission == null)
                return;
            selectedObject.Mission.Clear();
            if (selectedObject.BaconValues != null && selectedObject.BaconValues.ContainsKey("RepeatingMission"))
            {
                selectedObject.BaconValues.Remove("RepeatingMission");
                if (selectedObject.BaconValues.Count == 0)
                    selectedObject.BaconValues = (Dictionary<string, object>)null;
            }
        }

        public static void ShowDamagedComponents(Main main)
        {
            if (main._Game.SelectedObject == null || !(main._Game.SelectedObject is BuiltObject))
                return;
            BuiltObject selectedObject = main._Game.SelectedObject as BuiltObject;
            IEnumerable<IGrouping<string, short>> groupings = selectedObject.Components.Where<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>)(x => x.Status == ComponentStatus.Damaged)).ToList<BuiltObjectComponent>().GroupBy<BuiltObjectComponent, string, short>((Func<BuiltObjectComponent, string>)(x => x.Name), (Func<BuiltObjectComponent, short>)(x => x.BuiltObjectComponentId));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IGrouping<string, short> source in groupings)
            {
                if (source.Key.Length < 19)
                    stringBuilder.Append(source.Key).Append("\t").Append("\t").Append(source.Count<short>()).Append(Environment.NewLine);
                else
                    stringBuilder.Append(source.Key).Append("\t").Append(source.Count<short>()).Append(Environment.NewLine);
            }
            string str1 = stringBuilder.ToString();
            string str2 = selectedObject.Name + " Damaged Components";
            MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string)null, new Font("Verdana", 9f, FontStyle.Regular));
            messageBox.Text = str1;
            messageBox.Caption = str2;
            messageBox.AddButton(MessageBoxExButtons.Ok);
            messageBox.Icon = MessageBoxExIcon.None;
            bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
            if (!flag)
                main._Game.Galaxy.Pause();
            messageBox.Show();
            if (!flag)
                main._Game.Galaxy.Resume();
        }

        public static void CalculateDistance(Main main)
        {
            if (!(main._Game.SelectedObject is StellarObject selectedObject) || BaconBuiltObject.fighterTarget == null)
                return;
            double distanceStatic = Galaxy.CalculateDistanceStatic(selectedObject.Xpos, selectedObject.Ypos, BaconBuiltObject.fighterTarget.Xpos, BaconBuiltObject.fighterTarget.Ypos);
            if (main._Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                main._Game.PlayerEmpire.SendMessageToEmpire(main._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, "Distance from " + BaconBuiltObject.fighterTarget.Name + " to " + (main._Game.SelectedObject as StellarObject).Name + " is " + ((int)distanceStatic).ToString(), Point.Empty, "distance Measurement");
            }
            else
            {
                string str1 = "Distance from " + BaconBuiltObject.fighterTarget.Name + " to " + (main._Game.SelectedObject as StellarObject).Name + " is " + ((int)distanceStatic).ToString();
                string str2 = "Distance";
                MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string)null, new Font("Verdana", 9f, FontStyle.Regular));
                messageBox.Text = str1;
                messageBox.Caption = str2;
                messageBox.AddButton(MessageBoxExButtons.Ok);
                messageBox.Icon = MessageBoxExIcon.None;
                bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                if (!flag)
                    main._Game.Galaxy.Pause();
                messageBox.Show();
                if (!flag)
                    main._Game.Galaxy.Resume();
            }
        }

        public static void ToggleAutomateCarrierOps(Main main, object selectedObject)
        {
            BuiltObject builtObject = (BuiltObject)null;
            Fighter fighter = (Fighter)null;
            switch (selectedObject)
            {
                case BuiltObject _:
                    builtObject = selectedObject as BuiltObject;
                    break;
                case Fighter _:
                    fighter = selectedObject as Fighter;
                    break;
                default:
                    return;
            }
            if (builtObject != null)
            {
                if (builtObject.Fighters == null || builtObject.Empire != main._Game.PlayerEmpire)
                    return;
                if (builtObject.Name.StartsWith("*"))
                    builtObject.Name = builtObject.Name.Replace("*", "");
                else
                    builtObject.Name = "*" + builtObject.Name;
            }
            else
            {
                if (fighter == null)
                    return;
                if (fighter.Name.StartsWith("!"))
                    fighter.Name = fighter.Name.Replace("!", "");
                else
                    fighter.Name = "!" + fighter.Name;
            }
        }

        public static void GetParentCarrier(Main main)
        {
            if (!(main._Game.SelectedObject is Fighter) || (main._Game.SelectedObject as Fighter).ParentBuiltObject.Empire != main._Game.PlayerEmpire)
                return;
            BuiltObject parentBuiltObject = (main._Game.SelectedObject as Fighter).ParentBuiltObject;
            if (parentBuiltObject != null)
            {
                main._Game.SelectedObject = (object)parentBuiltObject;
                if (main._Game.SelectedObject != null)
                    main.method_208(main._Game.SelectedObject);
            }
        }

        public static void ResetAssaultPods(BuiltObject ship)
        {
            IEnumerable<Weapon> source1 = ship.Weapons.Where<Weapon>((Func<Weapon, bool>)(x => x.Component.Category == ComponentCategoryType.AssaultPod));
            if (!(source1 is IList<Weapon> weaponList))
                weaponList = (IList<Weapon>)source1.ToList<Weapon>();
            IList<Weapon> source2 = weaponList;
            if (!source2.Any<Weapon>())
                return;
            foreach (Weapon weapon in (IEnumerable<Weapon>)source2)
            {
                if ((ship.LastTouch.Ticks - weapon.LastFired.Ticks) / 10000000L > 120L)
                    weapon.Reset();
            }
        }

        public static void OrderBombersToAttack(Main main, object shipOrFighter, bool allBombers)
        {
            int num;
            if (BaconBuiltObject.fighterTarget != null)
            {
                switch (shipOrFighter)
                {
                    case null:
                        break;
                    case BuiltObject _:
                    case Fighter _:
                    case BuiltObjectList _:
                        num = 0;
                        goto label_5;
                    default:
                        num = !(shipOrFighter is ShipGroup) ? 1 : 0;
                        goto label_5;
                }
            }
            num = 1;
        label_5:
            if (num != 0 || shipOrFighter is ShipGroup && (shipOrFighter as ShipGroup).Ships[0].Empire != main._Game.PlayerEmpire || shipOrFighter is BuiltObjectList && (shipOrFighter as BuiltObjectList)[0].Empire != main._Game.PlayerEmpire || shipOrFighter is StellarObject && (shipOrFighter as StellarObject).Empire != main._Game.PlayerEmpire)
                return;
            switch (shipOrFighter)
            {
                case BuiltObject _:
                    BuiltObject builtObject = shipOrFighter as BuiltObject;
                    if (builtObject.Fighters == null)
                        break;
                    FighterList source1 = new FighterList();
                    source1.AddRange(builtObject.Fighters.Where<Fighter>((Func<Fighter, bool>)(x => x.OnboardCarrier)));
                    if (allBombers)
                        source1 = builtObject.Fighters;
                    builtObject.LaunchAvailableBombers();
                    if (source1 != null && source1.Any<Fighter>())
                    {
                        foreach (Fighter fighter in (SyncList<Fighter>)source1)
                        {
                            if (fighter.MissionType != FighterMissionType.ReturnToCarrier)
                            {
                                fighter.AssignAttackTarget(BaconBuiltObject.fighterTarget);
                                if (!fighter.Name.StartsWith("!"))
                                    fighter.Name = "!" + fighter.Name;
                            }
                        }
                    }
                    break;
                case Fighter _:
                    if ((shipOrFighter as Fighter).MissionType == FighterMissionType.ReturnToCarrier)
                        break;
                    if ((shipOrFighter as Fighter).OnboardCarrier)
                        (shipOrFighter as Fighter).ParentBuiltObject.LaunchFighter(shipOrFighter as Fighter);
                    (shipOrFighter as Fighter).AssignAttackTarget(BaconBuiltObject.fighterTarget);
                    if (!(shipOrFighter as Fighter).Name.StartsWith("!"))
                        (shipOrFighter as Fighter).Name = "!" + (shipOrFighter as Fighter).Name;
                    break;
                case BuiltObjectList _:
                    using (IEnumerator<BuiltObject> enumerator = ((IEnumerable<BuiltObject>)shipOrFighter).GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            BuiltObject current = enumerator.Current;
                            if (current.Fighters != null)
                            {
                                FighterList source2 = new FighterList();
                                source2.AddRange(current.Fighters.Where<Fighter>((Func<Fighter, bool>)(x => x.OnboardCarrier)));
                                if (allBombers)
                                    source2 = current.Fighters;
                                current.LaunchAvailableBombers();
                                if (source2 != null && source2.Any<Fighter>())
                                {
                                    foreach (Fighter fighter in (SyncList<Fighter>)source2)
                                    {
                                        if (fighter.MissionType != FighterMissionType.ReturnToCarrier)
                                        {
                                            fighter.AssignAttackTarget(BaconBuiltObject.fighterTarget);
                                            if (!fighter.Name.StartsWith("!"))
                                                fighter.Name = "!" + fighter.Name;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                case ShipGroup _:
                    foreach (BuiltObject ship in (SyncList<BuiltObject>)((ShipGroup)shipOrFighter).Ships)
                    {
                        if (ship.Fighters != null)
                        {
                            FighterList source3 = new FighterList();
                            source3.AddRange(ship.Fighters.Where<Fighter>((Func<Fighter, bool>)(x => x.OnboardCarrier)));
                            if (allBombers)
                                source3 = ship.Fighters;
                            ship.LaunchAvailableBombers();
                            if (source3 != null && source3.Any<Fighter>())
                            {
                                foreach (Fighter fighter in (SyncList<Fighter>)source3)
                                {
                                    if (fighter.MissionType != FighterMissionType.ReturnToCarrier)
                                    {
                                        fighter.AssignAttackTarget(BaconBuiltObject.fighterTarget);
                                        if (!fighter.Name.StartsWith("!"))
                                            fighter.Name = "!" + fighter.Name;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public static void TransferFighter(Main main, object fighterOrShip)
        {
            if (!(fighterOrShip is Fighter) && !(fighterOrShip is BuiltObject) || BaconBuiltObject.fighterTarget == null || !(BaconBuiltObject.fighterTarget is BuiltObject) || (BaconBuiltObject.fighterTarget as BuiltObject).ActualEmpire != main._Game.PlayerEmpire)
                return;
            BuiltObject fighterTarget = BaconBuiltObject.fighterTarget as BuiltObject;
            if (fighterTarget.Fighters == null || fighterTarget.FighterRepairRate <= 0)
                return;
            List<Fighter> source = new List<Fighter>();
            if (fighterOrShip is Fighter && (fighterOrShip as Fighter).Empire == main._Game.PlayerEmpire)
                source.Add(fighterOrShip as Fighter);
            else if (fighterOrShip is BuiltObject && (fighterOrShip as BuiltObject).ActualEmpire == main._Game.PlayerEmpire)
            {
                foreach (Fighter fighter in (SyncList<Fighter>)(fighterOrShip as BuiltObject).Fighters)
                {
                    if (!fighter.UnderConstruction)
                        source.Add(fighter);
                }
            }
            int fighterSizeOnCarrier1 = BaconBuiltObject.GetTotalFighterSizeOnCarrier(fighterTarget, FighterType.Interceptor, true);
            int num1 = BaconBuiltObject.GetFighterCapacity(fighterTarget, FighterType.Interceptor) * 2 - fighterSizeOnCarrier1;
            if (source.Any<Fighter>())
            {
                foreach (Fighter fighter in source)
                {
                    if (num1 >= fighter.Size && !fighter.OnboardCarrier && fighter.Specification != null && fighter.Specification.Type == FighterType.Interceptor && Galaxy.CalculateDistanceSquaredStatic(fighter.Xpos, fighter.Ypos, fighterTarget.Xpos, fighterTarget.Ypos) <= BaconFighter.CalculateMaximumTargetRange(fighter) * 2.0)
                    {
                        fighter.ParentBuiltObject.Fighters.Remove(fighter);
                        fighter.ParentBuiltObject = fighterTarget;
                        fighterTarget.Fighters.Add(fighter);
                        fighter.ReturnToCarrier();
                        num1 -= fighter.Size;
                    }
                }
            }
            int fighterSizeOnCarrier2 = BaconBuiltObject.GetTotalFighterSizeOnCarrier(fighterTarget, FighterType.Bomber, true);
            int num2 = BaconBuiltObject.GetFighterCapacity(fighterTarget, FighterType.Bomber) * 2 - fighterSizeOnCarrier2;
            if (!source.Any<Fighter>())
                return;
            foreach (Fighter fighter in source)
            {
                if (num2 >= fighter.Size && !fighter.OnboardCarrier && fighter.Specification != null && fighter.Specification.Type == FighterType.Bomber && Galaxy.CalculateDistanceSquaredStatic(fighter.Xpos, fighter.Ypos, fighterTarget.Xpos, fighterTarget.Ypos) <= BaconFighter.CalculateMaximumTargetRange(fighter) * 2.0)
                {
                    fighter.ParentBuiltObject.Fighters.Remove(fighter);
                    fighter.ParentBuiltObject = fighterTarget;
                    fighterTarget.Fighters.Add(fighter);
                    fighter.ReturnToCarrier();
                    num2 -= fighter.Size;
                }
            }
        }

        public static void ShowDetailedInformation(Main main)
        {
            if (main._Game.SelectedObject is BuiltObject)
            {
                BuiltObject selectedObject = main._Game.SelectedObject as BuiltObject;
                string text = string.Empty;
                string caption = "Ship information";
                if (selectedObject.SubRole == BuiltObjectSubRole.ExplorationShip)
                    text = BaconBuiltObject.ShowWhatWeAreResearching(main, selectedObject);
                else if (selectedObject.CargoCapacity > 0)
                    text = BaconBuiltObject.ShowCargo(main, selectedObject);
                if (selectedObject.BaconValues != null && selectedObject.BaconValues.ContainsKey("notarget"))
                {
                    text = text + Environment.NewLine + "Target restrictions" + Environment.NewLine;
                    foreach (string str in (List<string>)selectedObject.BaconValues["notarget"])
                        text = text + str + Environment.NewLine;
                }
                if (selectedObject.BaconValues != null && selectedObject.BaconValues.ContainsKey("cargos"))
                {
                    text = text + Environment.NewLine + "Manually assigned cargos" + Environment.NewLine;
                    foreach (string str in (List<string>)selectedObject.BaconValues["cargos"])
                        text = text + str + Environment.NewLine;
                }
                if (selectedObject.BaconValues != null && selectedObject.BaconValues.ContainsKey("customBomberName"))
                {
                    string baconValue = (string)selectedObject.BaconValues["customBomberName"];
                    text = text + "Custom bomber name: " + baconValue;
                }
                if (!(text != string.Empty))
                    return;
                BaconBuiltObject.ShowMessageBox(main, text, caption);
            }
            else
            {
                if (!(main._Game.SelectedObject is Habitat))
                    return;
                BaconHabitat.ShowDetailedInformation(main, main._Game.SelectedObject as Habitat);
            }
        }

        public static void ShowMissionCommands(Main main)
        {
            BuiltObject selectedObject = main._Game.SelectedObject as BuiltObject;
            StringBuilder stringBuilder = new StringBuilder();
            string empty = string.Empty;
            string caption = "Mission information";
            if (selectedObject == null || selectedObject.Mission == null || selectedObject.Mission.Type == BuiltObjectMissionType.Undefined || !main._Game.PlayerEmpire.Name.Contains("Romulan") && selectedObject.ActualEmpire != main._Game.Galaxy.PlayerEmpire)
                return;
            Command[] commandArray = selectedObject.Mission.ShowAllCommands();
            if (commandArray.Length == 0)
                return;
            for (int index = 0; index < commandArray.Length; ++index)
            {
                stringBuilder.Append((index + 1).ToString() + " " + commandArray[index].Action.ToString() + " ");
                if (commandArray[index].TargetCreature != null)
                    stringBuilder.Append(commandArray[index].TargetCreature.Name);
                else if (commandArray[index].TargetHabitat != null)
                    stringBuilder.Append(commandArray[index].TargetHabitat.Name);
                else if (commandArray[index].TargetBuiltObject != null)
                    stringBuilder.Append(commandArray[index].TargetBuiltObject.Name);
                else if (commandArray[index].TargetShipGroup != null)
                    stringBuilder.Append(commandArray[index].TargetShipGroup.Name);
                else if ((double)commandArray[index].TargetRelativeXpos == 0.0 && (double)commandArray[index].TargetRelativeYpos == 0.0)
                    stringBuilder.Append(commandArray[index].Xpos.ToString() + "," + commandArray[index].Ypos.ToString());
                stringBuilder.Append(Environment.NewLine);
            }
            string text = stringBuilder.ToString();
            if (!(text != string.Empty))
                return;
            BaconBuiltObject.ShowMessageBox(main, text, caption);
        }

        public static string ShowWhatWeAreResearching(Main main, BuiltObject ship)
        {
            if (ship.BaconValues == null)
                return string.Empty;
            if (!ship.BaconValues.ContainsKey("lab0") && !ship.BaconValues.ContainsKey("lab1") && !ship.BaconValues.ContainsKey("lab2"))
                return "This is not a science ship Add some labs!";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(ship.Name + " is researching..." + Environment.NewLine);
            if (ship.BaconValues.ContainsKey("lab0"))
                stringBuilder.Append(ship.BaconValues["lab0"]?.ToString() + Environment.NewLine);
            if (ship.BaconValues.ContainsKey("lab1"))
                stringBuilder.Append(ship.BaconValues["lab1"]?.ToString() + Environment.NewLine);
            if (ship.BaconValues.ContainsKey("lab2"))
                stringBuilder.Append(ship.BaconValues["lab2"]?.ToString() + Environment.NewLine);
            if (ship.BaconValues.ContainsKey("scientificData"))
            {
                stringBuilder.Append("Scientific data: ");
                stringBuilder.Append(ship.BaconValues["scientificData"]?.ToString() + Environment.NewLine);
            }
            return stringBuilder.ToString();
        }

        public static string ShowCargo(Main main, BuiltObject ship)
        {
            if (!(main._Game.SelectedObject is BuiltObject))
                return string.Empty;
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            object target = ship?.Mission?.Target;
            object secondaryTarget = ship?.Mission?.SecondaryTarget;
            if (ship.Mission != null && ship.Mission.Cargo != null)
            {
                foreach (Cargo cargo in (SyncList<Cargo>)ship.Mission.Cargo)
                    str3 = str3 + cargo.Amount.ToString() + " " + cargo.CommodityResource.Name + Environment.NewLine;
            }
            if (ship.Cargo != null)
            {
                foreach (Cargo cargo in (SyncList<Cargo>)ship.Cargo)
                {
                    if (cargo.CommodityIsResource)
                        str4 = str4 + cargo.Amount.ToString() + " " + cargo.CommodityResource.Name + Environment.NewLine;
                }
            }
            if (target != null && secondaryTarget != null)
            {
                switch (target)
                {
                    case BuiltObject _:
                        str1 = (target as StellarObject).Name;
                        break;
                    case Habitat _:
                        str1 = (target as StellarObject).Name;
                        break;
                    case Creature _:
                        str1 = (target as StellarObject).Name;
                        break;
                }
                switch (secondaryTarget)
                {
                    case BuiltObject _:
                        str2 = (secondaryTarget as StellarObject).Name;
                        break;
                    case Habitat _:
                        str2 = (secondaryTarget as StellarObject).Name;
                        break;
                    case Creature _:
                        str2 = (secondaryTarget as StellarObject).Name;
                        break;
                }
            }
            str5 = "Mission Information";
            string str6;
            if (ship.Mission != null && ship.Mission.Type == BuiltObjectMissionType.Transport)
                str6 = "Transport" + (ship.BaconValues == null || !ship.BaconValues.ContainsKey("RepeatingMission") ? "" : " (repeating)") + Environment.NewLine + "From: " + str1 + Environment.NewLine + "To: " + str2 + Environment.NewLine + Environment.NewLine + "Mission Cargo: " + Environment.NewLine + str3 + Environment.NewLine + Environment.NewLine + "Currently carrying: " + Environment.NewLine + str4;
            else if (ship.Mission != null && ship.Mission.Type == BuiltObjectMissionType.ExtractResources)
            {
                str6 = "Mine " + (ship.BaconValues == null || !ship.BaconValues.ContainsKey("RepeatingMission") ? "" : " (repeating)") + str1 + Environment.NewLine + "Deliver to " + str2 + Environment.NewLine + "Total mined: " + Environment.NewLine + str4;
            }
            else
            {
                str5 = "Cargo";
                str6 = str4;
            }
            return str6;
        }

        public static void ShowMessageBox(Main main, string text, string caption)
        {
            MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string)null, new Font("Verdana", 9f, FontStyle.Regular));
            messageBox.Text = text;
            messageBox.Caption = caption;
            messageBox.AddButton(MessageBoxExButtons.Ok);
            messageBox.Icon = MessageBoxExIcon.None;
            bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
            if (!flag)
                main._Game.Galaxy.Pause();
            messageBox.Show();
            if (flag)
                return;
            main._Game.Galaxy.Resume();
        }

        public static void AssignPassengershipMission(Main main, object ship, bool repeat)
        {
            if (main == null)
                return;

            BuiltObject builtObject;
            if (ship == null && main._Game.SelectedObject is BuiltObject)
            {
                builtObject = (BuiltObject)main._Game.SelectedObject;
            }
            else
            {
                if (!(ship is BuiltObject))
                    return;
                builtObject = (BuiltObject)ship;
            }
            if (builtObject.Role == BuiltObjectRole.Passenger &&
                (main._Game.PlayerEmpire.BuiltObjects.Contains(builtObject)
                || (BaconBuiltObject.AllowPrivateShipAssigment && main._Game.PlayerEmpire.PrivateBuiltObjects.Contains(builtObject)))
                && builtObject.Empire != null)
            {
                //string source = "";
                //string destination = "";
                Habitat sourceHab = null;
                Habitat targetHab = null;
                Race dominantRace = null;
                if (repeat && builtObject.BaconValues != null && builtObject.BaconValues.ContainsKey("RepeatingMission"))
                {
                    BuiltObjectMission baconValue = (BuiltObjectMission)builtObject.BaconValues["RepeatingMission"];
                    sourceHab = baconValue.TargetHabitat;
                    targetHab = baconValue.SecondaryTargetHabitat;
                    dominantRace = sourceHab.Population.DominantRace;
                }
                else
                {
                    //List<string> source1 = BaconBuiltObject.ReadPassengerShipSettings();
                    //if (source1.Count != 2)
                    //    return;
                    //source = source1.First<string>();
                    //destination = source1.Last<string>();
                    //target = empire.Colonies.First<Habitat>((Func<Habitat, bool>)(x => x.Name == source));
                    //target2 = empire.Colonies.First<Habitat>((Func<Habitat, bool>)(x => x.Name == destination));
                    bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                    if (!flag)
                    { main._Game.Galaxy.Pause(); }
                    using SelectPassengerMissionTargets selectForm = new SelectPassengerMissionTargets(main);
                    if (selectForm.ShowDialog(main) == DialogResult.OK)
                    {
                        sourceHab = selectForm.SelectedSource;
                        targetHab = selectForm.SelectedTarget;
                        dominantRace = sourceHab.Population.DominantRace;
                        repeat = selectForm.Repeatable;
                    }
                    if (!flag)
                    { main._Game.Galaxy.Resume(); }
                    if (selectForm.DialogResult != DialogResult.OK)
                    { return; }
                }
                if (sourceHab == null || targetHab == null || dominantRace == null)
                    return;
                long amount = Math.Min((long)builtObject.PopulationCapacity, sourceHab.Population[dominantRace].Amount / 50L);
                builtObject.AssignMission(
                    BuiltObjectMissionType.Transport,
                    (object)sourceHab,
                    (object)targetHab,
                    new PopulationList() { new Population(dominantRace, amount) },
                    BuiltObjectMissionPriority.Normal);

                if (!repeat)
                    return;
                if (builtObject.BaconValues == null)
                    builtObject.BaconValues = new Dictionary<string, object>();
                if (!builtObject.BaconValues.ContainsKey("RepeatingMission"))
                {
                    BuiltObjectMission builtObjectMission = builtObject.Mission.Clone();
                    builtObject.BaconValues.Add("RepeatingMission", (object)builtObjectMission);
                }
            }
            else
            {
                MessageBoxEx messageBoxEx = MessageBoxExManager.CreateMessageBox(null, BaconBuiltObject.myMain.font_3);
                messageBoxEx.Text = "Wrong target";
                messageBoxEx.Caption = "Warning";
                messageBoxEx.AddButton(MessageBoxExButtons.Ok);
                messageBoxEx.Icon = MessageBoxExIcon.Warning;
                messageBoxEx.Show(BaconBuiltObject.myMain);
            }
        }

        public static void ShipFinder(Main main)
        {
            try
            {
                string input = Interaction.InputBox("Enter the name of the object you wish to find or special command.", "Bacon Input", XPos: 0, YPos: 0);
                if (input.StartsWith("#"))
                    BaconBuiltObject.ManipulateFleet(main, input);
                else if (input.StartsWith("!"))
                {
                    BaconBuiltObject.GenericCommands(main, input);
                }
                else
                {
                    main._Game.SelectedObject = (object)main._Game.PlayerEmpire.BuiltObjects.FirstOrDefault<BuiltObject>((Func<BuiltObject, bool>)(x => x.Name.StartsWith(input, StringComparison.InvariantCultureIgnoreCase)));
                    if (main._Game.SelectedObject == null)
                        main._Game.SelectedObject = (object)main._Game.PlayerEmpire.PrivateBuiltObjects.FirstOrDefault<BuiltObject>((Func<BuiltObject, bool>)(x => x.Name.StartsWith(input, StringComparison.InvariantCultureIgnoreCase)));
                    if (main._Game.SelectedObject == null)
                        main._Game.SelectedObject = (object)main._Game.Galaxy.Habitats.FirstOrDefault<Habitat>((Func<Habitat, bool>)(x => x.Name.StartsWith(input, StringComparison.InvariantCultureIgnoreCase)));
                    if (main._Game.SelectedObject == null)
                    {
                        foreach (BuiltObject builtObject in (SyncList<BuiltObject>)main._Game.Galaxy.BuiltObjects)
                        {
                            if (builtObject != null && builtObject.Name != null)
                            {
                                if (builtObject.Name.StartsWith(input, StringComparison.InvariantCultureIgnoreCase))
                                { main._Game.SelectedObject = (object)builtObject; }
                                if (String.Equals(builtObject.Name, input, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    main._Game.SelectedObject = (object)builtObject;
                                    break;
                                }
                            }
                        }
                    }
                    if (main._Game.SelectedObject != null)
                        main.method_208(main._Game.SelectedObject);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void GenericCommands(Main main, string input)
        {
            if (input.StartsWith("!nukem", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.DestroyAllShips(main);
            else if (input.StartsWith("!bringonthebugs", StringComparison.InvariantCultureIgnoreCase))
                BaconGalaxy.SetShakturiBeacon(main);
            else if (input.StartsWith("!gogobugs", StringComparison.InvariantCultureIgnoreCase))
                BaconGalaxy.SpawnShakturi(main);
            else if (input.StartsWith("!placebugs", StringComparison.InvariantCultureIgnoreCase))
                BaconGalaxy.SpawnShakturiHere(main);
            else if (input.StartsWith("!clear", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.ClearOrdersForShip(main);
            else if (input.StartsWith("!spy", StringComparison.InvariantCultureIgnoreCase))
            {
                int result = 1;
                bool flag = false;
                string[] strArray = input.Split(' ');
                if (strArray.Length > 1)
                    flag = int.TryParse(strArray[1], out result);
                else
                    BaconBuiltObject.AssignSpiesToMission(main, 1);
                if (!flag)
                    return;
                BaconBuiltObject.AssignSpiesToMission(main, result);
            }
            else if (input.StartsWith("!troops", StringComparison.InvariantCultureIgnoreCase))
            {
                int result = 1;
                bool flag = false;
                string[] strArray = input.Split(' ');
                if (strArray.Length > 1)
                    flag = int.TryParse(strArray[1], out result);
                if (!flag)
                    return;
                BaconBuiltObject.AddTroopsToColony(main, result);
            }
            else if (input.StartsWith("!note", StringComparison.InvariantCultureIgnoreCase))
            {
                string message = input.Substring(5);
                BaconBuiltObject.AddReminderNote(main._Game.SelectedObject, message);
            }
            else if (input.StartsWith("!braveships", StringComparison.InvariantCultureIgnoreCase))
                BaconEmpire.SetRetreatWhenAttacked();
            else if (input.StartsWith("!cheat2112", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.ApplyStartingCheatSettings(main);
            else if (input.StartsWith("!cheatgoldmine", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.GiveAllResourcesToPlanet(main, 2700);
            else if (input.StartsWith("!loadshiptemplates", StringComparison.InvariantCultureIgnoreCase))
            {
                bool flag = false;
                bool result = true;
                string[] strArray = input.Split(' ');
                if (strArray.Length > 1)
                    flag = bool.TryParse(strArray[1], out result);
                if (flag)
                    BaconMain.RegenerateShipDesigns(main, result);
                else
                    BaconMain.RegenerateShipDesigns(main);
            }
            else if (input.StartsWith("!rangecircles", StringComparison.InvariantCultureIgnoreCase))
            {
                if (BaconMain.drawWeaponRangeCircles)
                {
                    BaconMain.drawWeaponRangeCircles = false;
                    BaconMain.minZoomLevelForWeaponsCircles = 5.0;
                }
                else
                {
                    BaconMain.drawWeaponRangeCircles = true;
                    BaconMain.minZoomLevelForWeaponsCircles = 0.9;
                }
            }
            else if (input.StartsWith("!freetrader", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.MakeShipFreeTrader(main, input);
            else if (input.StartsWith("!loan", StringComparison.InvariantCultureIgnoreCase))
                BaconEmpire.Loan(main, input);
            else if (input.StartsWith("!stockpilebooty", StringComparison.InvariantCultureIgnoreCase))
                BaconEmpire.SetResourcesForAllCapitals(main, input, true);
            else if (input.StartsWith("!stockpile", StringComparison.InvariantCultureIgnoreCase))
                BaconEmpire.SetResourcesForAllCapitals(main, input);
            else if (input.StartsWith("!infrastructure", StringComparison.InvariantCultureIgnoreCase))
                BaconHabitat.InvestInInfastructure(main, input);
            else if (input.StartsWith("!filterresource", StringComparison.InvariantCultureIgnoreCase) || string.Equals(input, "!fr", StringComparison.InvariantCultureIgnoreCase))
                BaconMain.SetResourceFilter(main, input);
            else if (input.StartsWith("!godmode", StringComparison.InvariantCultureIgnoreCase))
                main._Game.GodMode = !main._Game.GodMode;
            else if (input.StartsWith("!golegit", StringComparison.InvariantCultureIgnoreCase))
                BaconEmpire.ChangePirateEmpireToRegularEmpire(main, main._Game.PlayerEmpire);
            else if (input.StartsWith("!notarget", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.SetTargetRestriction(main, input);
            else if (input.StartsWith("!yourock", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.AddAsteroidInOrbit(main, input);
            else if (input.StartsWith("!overtime", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.AddYearsToVictoryStartDate(main, input);
            else if (input.StartsWith("!piraterespawn", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.ToddlePirateRespawnSetting(main);
            else if (input.StartsWith("!respawnpiratesnow", StringComparison.InvariantCultureIgnoreCase))
            {
                object galaxy = (object)main._Game.Galaxy;
                string str = "GenerateNewPirateEmpires";
                object[] objArray = new object[0];
                // ISSUE: reference to a compiler-generated field
                if (BaconBuiltObject.class69.callSite69 == null)
                {
                    // ISSUE: reference to a compiler-generated field
                    BaconBuiltObject.class69.callSite69 = CallSite<Action<CallSite, Type, object, string, object[]>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "InvokePrivateMethod", (IEnumerable<Type>)null, typeof(BaconBuiltObject), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[4]
                    {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                    }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                BaconBuiltObject.class69.callSite69.Target((CallSite)BaconBuiltObject.class69.callSite69, typeof(BaconBuiltObject), galaxy, str, objArray);
            }
            else if (input.StartsWith("!splitoff", StringComparison.InvariantCultureIgnoreCase))
            {
                bool flag = false;
                bool result = true;
                string[] strArray = input.Split(' ');
                if (strArray.Length > 1)
                    flag = bool.TryParse(strArray[1], out result);
                if (!flag && strArray.Length != 1)
                    return;
                BaconHabitat.LeaveEmpire(main, result);
            }
            else if (input.StartsWith("!cargos", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.ParseAndStoreCustomCargoRequest(main, input);
            else if (input.StartsWith("!price", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.DisplayCommodityPrices(main, input);
            else if (input.StartsWith("!science", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.ConvertAllExplorersToScience(main, input);
            else if (input.StartsWith("!growclones", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.CreateTroopsOnShip(main, input);
            else if (input.StartsWith("!bomber", StringComparison.InvariantCultureIgnoreCase))
                BaconBuiltObject.AssignCustomBomberNameForCarrier(main, input);
            else if (input.StartsWith("!piratebase", StringComparison.InvariantCultureIgnoreCase))
            {
                BaconHabitat.AddFundsToPirateBase(main, input);
            }
            else if (input.StartsWith("!setdifficulty", StringComparison.InvariantCultureIgnoreCase))
            {
                BaconBuiltObject.SetCustomDifficulty(main);
            }
            else if (input.StartsWith("!ResetAllRepairTemplates", StringComparison.InvariantCultureIgnoreCase))
            {
                Main._ExpModMain.FixAllDesignRepairTemplates(main._Game, true);
            }
            else if (input.StartsWith("!ResetAiRepairTemplates", StringComparison.InvariantCultureIgnoreCase))
            {
                Main._ExpModMain.FixAIDesignRepairTemplates(main._Game, true);
            }
            else if (input.StartsWith("!ResetPlayerRepairTemplates", StringComparison.InvariantCultureIgnoreCase))
            {
                Main._ExpModMain.FixPlayerDesignRepairTemplates(main._Game, true);
            }
        }

        public static void SetCustomDifficulty(Main main)
        {
            main._Game.PlayerEmpire.ColonyCorruptionFactor = Galaxy.ColonyCorruptionFactorDefault * BaconMain.customDifficultyColonyCorruptionFactor;
            main._Game.PlayerEmpire.WarWearinessFactor = Galaxy.WarWearinessFactorDefault * BaconMain.customDifficultyWarWearinessFactor;
            main._Game.PlayerEmpire.ResearchRate = Galaxy.ResearchRateDefault / BaconMain.customDifficultyResearchRate;
            main._Game.PlayerEmpire.PopulationGrowthRate = Galaxy.PopulationGrowthRateDefault / BaconMain.customDifficultyPopulationGrowthRate;
            main._Game.PlayerEmpire.MiningRate = Galaxy.MiningRateDefault / BaconMain.customDifficultyMiningRate;
            main._Game.PlayerEmpire.TargettingFactor = Galaxy.TargettingFactorDefault / Math.Sqrt(BaconMain.customDifficultyTargettingFactor);
            main._Game.PlayerEmpire.CountermeasuresFactor = Galaxy.CountermeasuresFactorDefault / Math.Sqrt(BaconMain.customDifficultyCountermeasuresFactor);
            main._Game.PlayerEmpire.ColonyShipBuildSpeedRate = Galaxy.ColonyShipBuildSpeedRateDefault / BaconMain.customDifficultyColonyShipBuildSpeedRate;
            main._Game.PlayerEmpire.ColonyIncomeFactor = Galaxy.ColonyIncomeFactorDefault / BaconMain.customDifficultyColonyIncomeFactor;
        }

        public static void AssignCustomBomberNameForCarrier(Main main, string input)
        {
            string str = "";
            if (main._Game.SelectedObject == null)
                return;
            string[] strArray = input.Split('=');
            if (strArray.Length > 1)
                str = strArray[1];
            List<BuiltObject> source = new List<BuiltObject>();
            if (main._Game.SelectedObject is BuiltObject)
            {
                if (main._Game.SelectedObject is BuiltObject selectedObject1 && selectedObject1.Design != null && selectedObject1.Design.FighterCapacity > 0)
                    source.Add(selectedObject1);
            }
            else if (main._Game.SelectedObject is ShipGroup && main._Game.SelectedObject is ShipGroup selectedObject2)
            {
                foreach (BuiltObject ship in (SyncList<BuiltObject>)selectedObject2.Ships)
                {
                    if (ship.Design != null && ship.Design.FighterCapacity > 0)
                        source.Add(ship);
                }
            }
            if (!source.Any<BuiltObject>())
                return;
            foreach (BuiltObject builtObject in source)
            {
                if (builtObject.ActualEmpire == main._Game.PlayerEmpire)
                {
                    if (builtObject.BaconValues == null)
                        builtObject.BaconValues = new Dictionary<string, object>();
                    if (str == "")
                    {
                        if (builtObject.BaconValues.ContainsKey("customBomberName"))
                            builtObject.BaconValues.Remove("customBomberName");
                    }
                    else if (builtObject.BaconValues.ContainsKey("customBomberName"))
                        builtObject.BaconValues["customBomberName"] = (object)str;
                    else
                        builtObject.BaconValues.Add("customBomberName", (object)str);
                    List<Fighter> fighterList = new List<Fighter>();
                    if (builtObject.Fighters != null)
                    {
                        foreach (Fighter fighter in (SyncList<Fighter>)builtObject.Fighters)
                        {
                            if (fighter.UnderConstruction)
                                fighterList.Add(fighter);
                        }
                    }
                    foreach (Fighter fighter in fighterList)
                        fighter.CompleteTeardown(main._Game.Galaxy);
                }
            }
        }

        public static void CreateTroopsOnShip(Main main, string input)
        {
            List<BuiltObject> source = new List<BuiltObject>();
            if (main._Game.SelectedObject is BuiltObject)
            {
                if (main._Game.SelectedObject is BuiltObject selectedObject1 && selectedObject1.TroopCapacityRemaining >= 100)
                    source.Add(selectedObject1);
            }
            else if (main._Game.SelectedObject is ShipGroup && main._Game.SelectedObject is ShipGroup selectedObject2)
            {
                foreach (BuiltObject ship in (SyncList<BuiltObject>)selectedObject2.Ships)
                {
                    if (ship.TroopCapacityRemaining >= 100)
                        source.Add(ship);
                }
            }
            if (!source.Any<BuiltObject>())
                return;
            foreach (BuiltObject builtObject in source)
            {
                for (; builtObject.TroopCapacityRemaining >= 100 && builtObject.ActualEmpire.StateMoney > 1000.0; builtObject.ActualEmpire.StateMoney -= 1000.0)
                {
                    Troop troop = source[0].ActualEmpire.IdentifyStrongestRaceAttackTroop();
                    Troop newTroop = Galaxy.GenerateNewTroop(TextResolver.GetText("Clone Trooper Battalion"), TroopType.Infantry, troop.AttackStrength, builtObject.ActualEmpire, builtObject.ActualEmpire.DominantRace, false);
                    newTroop.Readiness = 0.01f;
                    newTroop.BuiltObject = builtObject;
                    builtObject.ActualEmpire.Troops.Add(newTroop);
                    builtObject.Troops.Add(newTroop);
                }
            }
        }

        public static void ConvertAllExplorersToScience(Main main, string input)
        {
            string[] strArray = input.Split(' ');
            bool flag1 = false;
            if (strArray.Length > 1 && strArray[1] == "all")
                flag1 = true;
            List<Design> designList = new List<Design>();
            List<BuiltObject> list1;
            if (flag1)
            {
                list1 = main._Game.Galaxy.BuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.SubRole == BuiltObjectSubRole.ExplorationShip)).ToList<BuiltObject>();
                foreach (Empire empire in (SyncList<Empire>)main._Game.Galaxy.Empires)
                {
                    List<Design> list2 = empire.Designs.Where<Design>((Func<Design, bool>)(x => x.SubRole == BuiltObjectSubRole.ExplorationShip)).ToList<Design>();
                    if (list2 != null && list2.Count > 0)
                    {
                        foreach (Design design in list2)
                            designList.Add(design);
                    }
                }
            }
            else
            {
                list1 = main._Game.Galaxy.PlayerEmpire.BuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.SubRole == BuiltObjectSubRole.ExplorationShip)).ToList<BuiltObject>();
                designList = main._Game.Galaxy.PlayerEmpire.Designs.Where<Design>((Func<Design, bool>)(x => x.SubRole == BuiltObjectSubRole.ExplorationShip)).ToList<Design>();
            }
            foreach (BuiltObject builtObject in list1)
            {
                bool flag2 = false;
                if (!builtObject.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsWeaponsLab)))
                {
                    ComponentDefinition componentDefinition = ((IEnumerable<ComponentDefinition>)BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>)(x => x.Type == ComponentType.LabsWeaponsLab));
                    if (componentDefinition != null)
                        builtObject.Components.Add(new BuiltObjectComponent(componentDefinition.ComponentID, ComponentStatus.Normal));
                    flag2 = true;
                }
                if (!builtObject.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsHighTechLab)))
                {
                    ComponentDefinition componentDefinition = ((IEnumerable<ComponentDefinition>)BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>)(x => x.Type == ComponentType.LabsHighTechLab));
                    if (componentDefinition != null)
                        builtObject.Components.Add(new BuiltObjectComponent(componentDefinition.ComponentID, ComponentStatus.Normal));
                    flag2 = true;
                }
                if (!builtObject.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsEnergyLab)))
                {
                    ComponentDefinition componentDefinition = ((IEnumerable<ComponentDefinition>)BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>)(x => x.Type == ComponentType.LabsEnergyLab));
                    if (componentDefinition != null)
                        builtObject.Components.Add(new BuiltObjectComponent(componentDefinition.ComponentID, ComponentStatus.Normal));
                    flag2 = true;
                }
                if (flag2)
                    builtObject.ReDefine();
            }
            foreach (Design design in designList)
            {
                if (!design.Components.Exists((Predicate<Component>)(x => x.Type == ComponentType.LabsWeaponsLab)))
                {
                    ComponentDefinition componentDefinition = ((IEnumerable<ComponentDefinition>)BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>)(x => x.Type == ComponentType.LabsWeaponsLab));
                    if (componentDefinition != null)
                        design.Components.Add((Component)new BuiltObjectComponent(componentDefinition.ComponentID, ComponentStatus.Normal));
                }
                if (!design.Components.Exists((Predicate<Component>)(x => x.Type == ComponentType.LabsHighTechLab)))
                {
                    ComponentDefinition componentDefinition = ((IEnumerable<ComponentDefinition>)BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>)(x => x.Type == ComponentType.LabsHighTechLab));
                    if (componentDefinition != null)
                        design.Components.Add((Component)new BuiltObjectComponent(componentDefinition.ComponentID, ComponentStatus.Normal));
                }
                if (!design.Components.Exists((Predicate<Component>)(x => x.Type == ComponentType.LabsEnergyLab)))
                {
                    ComponentDefinition componentDefinition = ((IEnumerable<ComponentDefinition>)BaconBuiltObject.myMain._Game.Galaxy.ComponentDefinitions).FirstOrDefault<ComponentDefinition>((Func<ComponentDefinition, bool>)(x => x.Type == ComponentType.LabsEnergyLab));
                    if (componentDefinition != null)
                        design.Components.Add((Component)new BuiltObjectComponent(componentDefinition.ComponentID, ComponentStatus.Normal));
                }
            }
        }

        public static void DisplayCommodityPrices(Main main, string input)
        {
            if (input.Length < 8)
            {
                BaconBuiltObject.ShowMessageBox(main, "Usage: !price commodityName", "Invalid format");
            }
            else
            {
                string commodityToFind = input.Substring(7).ToLower();
                ResourceDefinitionList resources = main._Game.Galaxy.ResourceSystem.Resources;
                ResourceDefinition commodity = resources.FirstOrDefault<ResourceDefinition>((Func<ResourceDefinition, bool>)(x => x.Name.ToLower().StartsWith(commodityToFind)));
                if (commodity == null)
                {
                    BaconBuiltObject.ShowMessageBox(main, "Nothing fould for " + commodityToFind + ". Did you spell it right?", "Prices");
                }
                else
                {
                    int index = resources.IndexOf(commodity);
                    StellarObject locationToSearch;
                    if (main._Game.SelectedObject != null && main._Game.SelectedObject is StellarObject)
                        locationToSearch = main._Game.SelectedObject as StellarObject;
                    else if (main._Game.PlayerEmpire.Capital != null)
                        locationToSearch = (StellarObject)main._Game.PlayerEmpire.Capital;
                    else if (main._Game.PlayerEmpire.BuiltObjects != null && main._Game.PlayerEmpire.BuiltObjects.Count > 0)
                    {
                        locationToSearch = (StellarObject)main._Game.PlayerEmpire.BuiltObjects[0];
                    }
                    else
                    {
                        BaconBuiltObject.ShowMessageBox(main, "Select an object to center search on.", "Prices");
                        return;
                    }
                    List<Habitat> habitatsFound = new List<Habitat>();
                    foreach (SystemInfo system in (SyncList<SystemInfo>)main._Game.Galaxy.Systems)
                    {
                        foreach (Habitat habitat in (SyncList<Habitat>)system.Habitats)
                        {
                            if (habitat.BaconValues != null && habitat.BaconValues.ContainsKey("resourcePriceList"))
                                habitatsFound.Add(habitat);
                        }
                    }
                    habitatsFound = habitatsFound.OrderBy<Habitat, double>((Func<Habitat, double>)(x => Galaxy.CalculateDistanceSquaredStatic(x.Xpos, x.Ypos, locationToSearch.Xpos, locationToSearch.Ypos))).ToList<Habitat>();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Location\t\t\tprice\tquantity" + Environment.NewLine);
                    for (int i = 0; i < Math.Min(10, habitatsFound.Count); i++)
                    {
                        string str = string.Format("{0:0.0000}", (object)(((Dictionary<string, double>)habitatsFound[i].BaconValues["resourcePriceList"])[commodity.Name] * BaconBuiltObject.myMain._Game.Galaxy.ResourceCurrentPrices[index]));
                        stringBuilder.Append(habitatsFound[i].Name + ": ");
                        if (habitatsFound[i].Name.Length < 8)
                            stringBuilder.Append("\t\t\t");
                        else if (habitatsFound[i].Name.Length < 16)
                            stringBuilder.Append("\t\t");
                        else
                            stringBuilder.Append("\t");
                        stringBuilder.Append(string.Format("{0:0.0000}", (object)str));
                        int num = 0;
                        if (habitatsFound[i].Cargo.FirstOrDefault<Cargo>((Func<Cargo, bool>)(x => x.CommodityIsResource && x.Resource.Name == commodity.Name && x.EmpireId == habitatsFound[i].Empire.EmpireId)) != null)
                            num = Math.Max(0, habitatsFound[i].Cargo.FirstOrDefault<Cargo>((Func<Cargo, bool>)(x => x.CommodityIsResource && x.Resource.Name == commodity.Name && x.EmpireId == habitatsFound[i].Empire.EmpireId)).Amount);
                        stringBuilder.Append("\t" + num.ToString() + Environment.NewLine);
                    }
                    stringBuilder.Append(Environment.NewLine + Environment.NewLine + "Note: prices do not reflect local taxes and are subject to change.");
                    BaconBuiltObject.ShowMessageBox(main, stringBuilder.ToString(), "Prices near " + locationToSearch.Name + " for " + commodity.Name + ".");
                }
            }
        }

        public static void ParseAndStoreCustomCargoRequest(Main main, string input)
        {
            if (main._Game.SelectedObject == null || !(main._Game.SelectedObject is BuiltObject))
                return;
            BuiltObject selectedObject = main._Game.SelectedObject as BuiltObject;
            if (selectedObject.ActualEmpire != main._Game.PlayerEmpire)
                return;
            if (input == "!cargos")
            {
                if (selectedObject.BaconValues == null || !selectedObject.BaconValues.ContainsKey("cargos"))
                    return;
                selectedObject.BaconValues.Remove("cargos");
                if (selectedObject.BaconValues.Count == 0)
                    selectedObject.BaconValues = (Dictionary<string, object>)null;
            }
            else
            {
                if (selectedObject.BaconValues == null)
                    selectedObject.BaconValues = new Dictionary<string, object>();
                string[] strArray = input.Split(',');
                if (strArray.Length < 2)
                    return;
                List<string> stringList = new List<string>();
                for (int index = 1; index < strArray.Length; ++index)
                {
                    string str = strArray[index].ToLower().Trim();
                    stringList.Add(str);
                }
                if (selectedObject.BaconValues.ContainsKey("cargos"))
                    selectedObject.BaconValues["cargos"] = (object)stringList;
                else
                    selectedObject.BaconValues.Add("cargos", (object)stringList);
            }
        }

        public static void InvokePrivateMethod(
          object classInstanceToInvoke,
          string nameOfPrivateMethod,
          object[] paramList)
        {
            // ISSUE: reference to a compiler-generated field
            if (BaconBuiltObject.class76.p2 == null)
            {
                // ISSUE: reference to a compiler-generated field
                BaconBuiltObject.class76.p2 = CallSite<Func<CallSite, object, MethodInfo>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(MethodInfo), typeof(BaconBuiltObject)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, MethodInfo> target1 = BaconBuiltObject.class76.p2.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, MethodInfo>> p2 = BaconBuiltObject.class76.p2;
            // ISSUE: reference to a compiler-generated field
            if (BaconBuiltObject.class76.p1 == null)
            {
                // ISSUE: reference to a compiler-generated field
                BaconBuiltObject.class76.p1 = CallSite<Func<CallSite, object, string, BindingFlags, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetMethod", (IEnumerable<Type>)null, typeof(BaconBuiltObject), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[3]
                {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
                }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string, BindingFlags, object> target2 = BaconBuiltObject.class76.p1.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string, BindingFlags, object>> p1 = BaconBuiltObject.class76.p1;
            // ISSUE: reference to a compiler-generated field
            if (BaconBuiltObject.class76.p0 == null)
            {
                // ISSUE: reference to a compiler-generated field
                BaconBuiltObject.class76.p0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetType", (IEnumerable<Type>)null, typeof(BaconBuiltObject), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[1]
                {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj1 = BaconBuiltObject.class76.p0.Target((CallSite)BaconBuiltObject.class76.p0, classInstanceToInvoke);
            string str = nameOfPrivateMethod;
            object obj2 = target2((CallSite)p1, obj1, str, BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo methodInfo = target1((CallSite)p2, obj2);
            // ISSUE: reference to a compiler-generated field
            if (BaconBuiltObject.class76.p3 == null)
            {
                // ISSUE: reference to a compiler-generated field
                BaconBuiltObject.class76.p3 = CallSite<Func<CallSite, MethodInfo, object, object[], object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Invoke", (IEnumerable<Type>)null, typeof(BaconBuiltObject), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[3]
                {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj3 = BaconBuiltObject.class76.p3.Target((CallSite)BaconBuiltObject.class76.p3, methodInfo, classInstanceToInvoke, paramList);
        }

        public static void ToddlePirateRespawnSetting(Main main)
        {
            if (main._Game.Galaxy.DestroyedPiratesDoNotRespawn)
            {
                main._Game.Galaxy.DestroyedPiratesDoNotRespawn = false;
                main._Game.PlayerEmpire.SendMessageToEmpire(main._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, "Pirates will respawn.");
            }
            else
            {
                main._Game.Galaxy.DestroyedPiratesDoNotRespawn = true;
                main._Game.PlayerEmpire.SendMessageToEmpire(main._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, "Pirates will not respawn.");
            }
        }

        public static void AddYearsToVictoryStartDate(Main main, string input)
        {
            bool flag = false;
            int result = 1;
            string[] strArray = input.Split(' ');
            if (strArray.Length > 1)
                flag = int.TryParse(strArray[1], out result);
            if (!flag)
                result = 1;
            main._Game.Galaxy.GlobalVictoryConditions.StartDate += (long)(Galaxy.RealSecondsInGalacticYear * 1000 * result);
        }

        public static void AddAsteroidInOrbit(Main main, string input)
        {
            Habitat selectedObject = main._Game.SelectedObject as Habitat;
            if (!(main._Game.SelectedObject is Habitat) || selectedObject.Owner != main._Game.PlayerEmpire || !main._Game.PlayerEmpire.Colonies.Contains(selectedObject))
                BaconBuiltObject.ShowMessageBox(main, "You can only add an orbital asteroid habitat to a colony you own.", "Not your colony");
            if (main._Game.PlayerEmpire.StateMoney < (double)BaconBuiltObject.orbitalAsteroidCost)
                BaconBuiltObject.ShowMessageBox(main, "Insufficient funds. Deploying an orbital asteroid costs " + BaconBuiltObject.orbitalAsteroidCost.ToString() + ".", "Not enough money");
            BaconGalaxy.GeneratePlainAsteroid(main._Game.Galaxy, selectedObject);
            main._Game.PlayerEmpire.StateMoney -= (double)BaconBuiltObject.orbitalAsteroidCost;
        }

        public static void MakeShipFreeTrader(Main main, string input)
        {
            if (!(main._Game.SelectedObject is BuiltObject selectedObject) || selectedObject.ActualEmpire != main._Game.PlayerEmpire || selectedObject.CargoCapacity <= 0)
                return;
            bool flag = false;
            int result = 10000;
            string[] strArray = input.Split(' ');
            if (strArray.Length > 1)
                flag = int.TryParse(strArray[1], out result);
            if (!flag)
                result = 10000;
            if (result == 0)
            {
                BaconBuiltObject.RemoveShipAsFreeTrader(main, selectedObject);
            }
            else
            {
                if (result < 0)
                {
                    if (!BaconBuiltObject.IsFreeTrader(main, main._Game.SelectedObject))
                        return;
                    int baconValue = (int)selectedObject.BaconValues["cash"];
                    if (Math.Abs(result) > baconValue)
                        result = -1 * baconValue;
                }
                if (main._Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                    main._Game.PlayerEmpire.StateMoney -= (double)result;
                else
                    main._Game.PlayerEmpire.PrivateMoney -= (double)result;
                if (BaconBuiltObject.IsFreeTrader(main, main._Game.SelectedObject))
                {
                    int baconValue = (int)selectedObject.BaconValues["cash"];
                    selectedObject.BaconValues["cash"] = (object)(baconValue + result);
                }
                else
                {
                    if (selectedObject.BaconValues == null)
                        selectedObject.BaconValues = new Dictionary<string, object>();
                    selectedObject.BaconValues.Add("cash", (object)result);
                    BaconBuiltObject.GiveShipUniqueDesign(main, selectedObject);
                }
            }
        }

        public static void GiveShipUniqueDesign(Main main, BuiltObject ship)
        {
            Design design1 = ship.Design.Clone();
            design1.TacticsStrongerShips = BattleTactics.AllWeapons;
            design1.TacticsWeakerShips = BattleTactics.PointBlank;
            ship.TacticsStrongerShips = BattleTactics.AllWeapons;
            ship.TacticsWeakerShips = BattleTactics.PointBlank;
            design1.FleeWhen = BuiltObjectFleeWhen.Never;
            ship.FleeWhen = BuiltObjectFleeWhen.Never;
            int num = ship.ActualEmpire.Designs.Count<Design>((Func<Design, bool>)(x => x.Name.Contains(ship.Design.Name)));
            Design design2 = design1;
            design2.Name = design2.Name + "_ft" + num.ToString();
            design1.IsObsolete = true;
            ship.Design = design1;
            ship.ActualEmpire.Designs.Add(design1);
        }

        public static void RemoveShipAsFreeTrader(Main main, BuiltObject ship)
        {
            if (ship.BaconValues == null || !ship.BaconValues.ContainsKey("cash"))
                return;
            int baconValue = (int)ship.BaconValues["cash"];
            if (main._Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                main._Game.PlayerEmpire.StateMoney += (double)baconValue;
            else
                main._Game.PlayerEmpire.PrivateMoney += (double)baconValue;
            ship.BaconValues.Remove("cash");
            string[] strArray = Regex.Split(ship.Design.Name, "_ft");
            string baseDesignName = "";
            if (strArray.Length != 0)
                baseDesignName = strArray[0];
            if (baseDesignName != "")
            {
                Design design = ship.ActualEmpire.Designs.FirstOrDefault<Design>((Func<Design, bool>)(x => x.Name == baseDesignName));
                if (design != null)
                {
                    ship.Design = design;
                    ship.TacticsStrongerShips = ship.Design.TacticsStrongerShips;
                    ship.TacticsWeakerShips = ship.Design.TacticsWeakerShips;
                    ship.FleeWhen = ship.Design.FleeWhen;
                    ship.ReDefine();
                }
            }
        }

        public static bool IsFreeTrader(Main main, object shipObject)
        {
            if (!(shipObject is BuiltObject))
                return false;
            BuiltObject builtObject = (BuiltObject)shipObject;
            if (builtObject.BaconValues == null)
                return false;
            if (builtObject.ActualEmpire == main._Game.PlayerEmpire)
                return builtObject.BaconValues.ContainsKey("cash");
            if (builtObject.BaconValues.Count == 0)
                builtObject.BaconValues = (Dictionary<string, object>)null;
            return false;
        }

        public static void ManipulateFleet(Main main, string input)
        {
            if (input.StartsWith("#combine"))
            {
                string commandParameters = input.Substring(9);
                ShipGroup shipGroup1;
                ShipGroup shipGroup2;
                if (!commandParameters.Contains("+") && main._Game.SelectedObject is ShipGroup && ((ShipGroup)main._Game.SelectedObject).Empire == main._Game.PlayerEmpire)
                {
                    shipGroup1 = (ShipGroup)main._Game.SelectedObject;
                    shipGroup2 = main._Game.PlayerEmpire.ShipGroups.FirstOrDefault<ShipGroup>((Func<ShipGroup, bool>)(x => x.Name == commandParameters));
                }
                else
                {
                    string[] parts = commandParameters.Split('+');
                    if (parts.Length != 2)
                        return;
                    shipGroup1 = main._Game.PlayerEmpire.ShipGroups.FirstOrDefault<ShipGroup>((Func<ShipGroup, bool>)(x => x.Name == parts[0]));
                    shipGroup2 = main._Game.PlayerEmpire.ShipGroups.FirstOrDefault<ShipGroup>((Func<ShipGroup, bool>)(x => x.Name == parts[1]));
                }
                if (shipGroup2 == null || shipGroup1 == null || shipGroup2 == shipGroup1)
                    return;
                foreach (BuiltObject ship in (SyncList<BuiltObject>)shipGroup2.Ships)
                {
                    shipGroup1.Ships.Add(ship);
                    ship.ShipGroup = shipGroup1;
                }
            label_22:
                int num1;
                if (shipGroup2 == null)
                {
                    num1 = 0;
                }
                else
                {
                    int? count = shipGroup2.Ships?.Count;
                    int num2 = 0;
                    num1 = count.GetValueOrDefault() > num2 & count.HasValue ? 1 : 0;
                }
                if (num1 != 0)
                {
                    using (IEnumerator<BuiltObject> enumerator = shipGroup2.Ships.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            BuiltObject current = enumerator.Current;
                            shipGroup2.Ships.Remove(current);
                        }
                        goto label_22;
                    }
                }
                else
                    main._Game.PlayerEmpire.ShipGroups.Remove(shipGroup2);
            }
            else if (input.StartsWith("#disband"))
            {
                ShipGroup shipGroup;
                if (main._Game.SelectedObject is ShipGroup && ((ShipGroup)main._Game.SelectedObject).Empire == main._Game.PlayerEmpire)
                {
                    shipGroup = (ShipGroup)main._Game.SelectedObject;
                }
                else
                {
                    string commandParameters = input.Substring(9);
                    shipGroup = main._Game.PlayerEmpire.ShipGroups.FirstOrDefault<ShipGroup>((Func<ShipGroup, bool>)(x => x.Name == commandParameters));
                }
                if (shipGroup == null)
                    return;
                shipGroup.Ships.ForEach((Action<BuiltObject>)(x => x.IsAutoControlled = true));
            label_41:
                int num3;
                if (shipGroup == null)
                {
                    num3 = 0;
                }
                else
                {
                    int? count = shipGroup.Ships?.Count;
                    int num4 = 0;
                    num3 = count.GetValueOrDefault() > num4 & count.HasValue ? 1 : 0;
                }
                if (num3 == 0)
                    return;
                using (IEnumerator<BuiltObject> enumerator = shipGroup.Ships.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                        enumerator.Current.LeaveShipGroup();
                    goto label_41;
                }
            }
            else if (input.StartsWith("#join"))
            {
                string commandParameters = input.Substring(6);
                ShipGroup shipGroup3 = main._Game.PlayerEmpire.ShipGroups.FirstOrDefault<ShipGroup>((Func<ShipGroup, bool>)(x => x.Name == commandParameters));
                if (shipGroup3 == null)
                    return;
                if (main._Game.SelectedObject is BuiltObjectList)
                {
                    foreach (BuiltObject builtObject in (SyncList<BuiltObject>)(main._Game.SelectedObject as BuiltObjectList))
                    {
                        if (!shipGroup3.Ships.Contains(builtObject))
                        {
                            if (builtObject.ShipGroup != null)
                            {
                                ShipGroup shipGroup4 = builtObject.ShipGroup;
                                if (builtObject.ShipGroup.Ships.Contains(builtObject))
                                {
                                    builtObject.ShipGroup.Ships.Remove(builtObject);
                                    if (shipGroup4.Ships != null && shipGroup4.Ships.Count == 0 && builtObject.ActualEmpire.ShipGroups.Contains(shipGroup4))
                                        builtObject.ActualEmpire.ShipGroups.Remove(shipGroup4);
                                }
                            }
                            shipGroup3.Ships.Add(builtObject);
                            builtObject.ShipGroup = shipGroup3;
                        }
                    }
                }
                else
                {
                    if (!(main._Game.SelectedObject is BuiltObject) || shipGroup3.Ships.Contains(main._Game.SelectedObject as BuiltObject))
                        return;
                    shipGroup3.Ships.Add(main._Game.SelectedObject as BuiltObject);
                    (main._Game.SelectedObject as BuiltObject).ShipGroup = shipGroup3;
                }
            }
            else
            {
                if (!input.StartsWith("#add") || !(main._Game.SelectedObject is ShipGroup) || ((ShipGroup)main._Game.SelectedObject).Empire != main._Game.PlayerEmpire)
                    return;
                ShipGroup selectedObject = (ShipGroup)main._Game.SelectedObject;
                string[] strArray = input.Substring(5).Split('=');
                int result = 1;
                string lower;
                if (strArray.Length == 1)
                {
                    lower = strArray[0].ToLower();
                }
                else
                {
                    if (strArray.Length != 2)
                        return;
                    lower = strArray[0];
                    if (!int.TryParse(strArray[1], out result))
                        return;
                }
                List<BuiltObject> nearestShipsByDesign = BaconBuiltObject.FindNearestShipsByDesign(main, lower, result, selectedObject.LeadShip.Xpos, selectedObject.LeadShip.Ypos, selectedObject);
                if (nearestShipsByDesign == null || !nearestShipsByDesign.Any<BuiltObject>())
                    return;
                foreach (BuiltObject ship in nearestShipsByDesign)
                {
                    selectedObject.AddShipToFleet(ship);
                    ship.ShipGroup = selectedObject;
                }
            }
        }

        public static List<BuiltObject> FindNearestShipsByDesign(
          Main main,
          string designToFind,
          int amountToFind,
          double xPos,
          double yPos,
          ShipGroup fleet = null)
        {
            List<BuiltObject> source1 = new List<BuiltObject>();
            List<Design> all = main._Game.PlayerEmpire.Designs.FindAll((Predicate<Design>)(x => x.Name.ToLower().StartsWith(designToFind.ToLower())));
            if (all.Count < 1)
                return source1;
            foreach (Design design1 in all.OrderByDescending<Design, long>((Func<Design, long>)(x => x.DateCreated)).ToList<Design>())
            {
                Design design = design1;
                List<BuiltObject> list = main._Game.PlayerEmpire.BuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.Design == design)).ToList<BuiltObject>();
                source1.AddRange((IEnumerable<BuiltObject>)list);
            }
            List<BuiltObject> list1 = source1.OrderBy<BuiltObject, double>((Func<BuiltObject, double>)(x => Galaxy.CalculateDistanceSquaredStatic(x.Xpos, x.Ypos, xPos, yPos))).ToList<BuiltObject>();
            if (fleet != null)
                list1 = list1.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.ShipGroup == null)).ToList<BuiltObject>();
            foreach (BuiltObject builtObject in list1.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.CurrentFuel / (double)x.Design.FuelCapacity < 0.33)).ToList<BuiltObject>())
            {
                if (list1.Contains(builtObject))
                    list1.Remove(builtObject);
            }
            foreach (BuiltObject builtObject in list1.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.DamagedComponentCount > 5)).ToList<BuiltObject>())
            {
                if (list1.Contains(builtObject))
                    list1.Remove(builtObject);
            }
            int count = Math.Min(amountToFind, list1.Count);
            IEnumerable<BuiltObject> source2 = list1.Take<BuiltObject>(count);
            return source2 != null ? source2.ToList<BuiltObject>() : (List<BuiltObject>)null;
        }

        public static void ShowStats(Main main)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!(main._Game.SelectedObject is BuiltObject) || main._Game.SelectedObject is BuiltObject && (main._Game.SelectedObject as BuiltObject).Role != BuiltObjectRole.Military)
                return;
            BuiltObject selectedObject = main._Game.SelectedObject as BuiltObject;
            stringBuilder.Append("Combat stats for ").Append(selectedObject.Name).Append(Environment.NewLine);
            stringBuilder.Append("Crew level: ").Append(BaconBuiltObject.CalculateCrewLevel(main, selectedObject)).Append(Environment.NewLine);
            if (selectedObject.BattleStats != null)
            {
                BaconSpaceBattleStats.AddLatestCombatStats(selectedObject, selectedObject.BattleStats);
                selectedObject.BattleStats = new SpaceBattleStats();
            }
            selectedObject.ReDefine();
            SpaceBattleStats careerBattleStats = selectedObject.CareerBattleStats;
            if (careerBattleStats == null)
                return;
            stringBuilder.Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsCapitalShip > 0)
                stringBuilder.Append("Capital ship kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsCapitalShip).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsCarrier > 0)
                stringBuilder.Append("Crrier kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsCarrier).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsCruiser > 0)
                stringBuilder.Append("Cruiser kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsCruiser).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsDestroyer > 0)
                stringBuilder.Append("Destroyer kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsDestroyer).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsFrigate > 0)
                stringBuilder.Append("Frigate kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsFrigate).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsEscort > 0)
                stringBuilder.Append("Escort kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsEscort).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyFighters > 0)
                stringBuilder.Append("Fighter kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyFighters).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsTroopTransport > 0)
                stringBuilder.Append("Troopship kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsTroopTransport).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsResupplyShip > 0)
                stringBuilder.Append("Resupply ship kills: ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsResupplyShip).Append(Environment.NewLine);
            if (careerBattleStats.DestroyedEnemyShipsOtherShips > 0)
                stringBuilder.Append("Ship kills (other): ").Append("\t").Append("\t").Append(careerBattleStats.DestroyedEnemyShipsOtherShips).Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            if ((double)careerBattleStats.WeaponsDamageToEnemy > 0.0)
                stringBuilder.Append("Damage Inflicted: ").Append("\t").Append("\t").Append((int)careerBattleStats.WeaponsDamageToEnemy).Append(Environment.NewLine);
            if (careerBattleStats.DamageToUs > 0)
                stringBuilder.Append("Damage Taken: ").Append("\t").Append("\t").Append(careerBattleStats.DamageToUs).Append(Environment.NewLine);
            bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
            if (!flag)
                main._Game.Galaxy.Pause();
            MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string)null, new Font("Verdana", 9f, FontStyle.Regular));
            messageBox.Text = stringBuilder.ToString();
            messageBox.Caption = "Battle Stats";
            messageBox.AddButton(MessageBoxExButtons.Ok);
            messageBox.Icon = MessageBoxExIcon.None;
            messageBox.Show();
            if (flag)
                return;
            main._Game.Galaxy.Resume();
        }

        public static string CalculateCrewLevel(Main main, BuiltObject ship)
        {
            if (ship.Role != BuiltObjectRole.Military)
                return "";
            int num = 0;
            if (ship.CareerBattleStats == null)
                ship.CareerBattleStats = new SpaceBattleStats();
            float weaponsDamageToEnemy = ship.CareerBattleStats.WeaponsDamageToEnemy;
            int damageToUs = ship.CareerBattleStats.DamageToUs;
            if (ship.Role == BuiltObjectRole.Military)
                num = (int)Math.Max(0.0f, weaponsDamageToEnemy - (float)(damageToUs * 3));
            return num >= 100 ? (num >= 3999 ? (num >= 8999 ? (num >= 15999 ? (num >= 24999 ? "legendary" : "elite") : "veteran") : "experienced") : "average") : "green";
        }

        //public static void AssignGlobalCargoMissionSource(Main main)
        //{
        //    if (main._Game.SelectedObject is Habitat && (main._Game.SelectedObject as Habitat).Owner == main._Game.PlayerEmpire)
        //        BaconBuiltObject.globalCargoMissionSource = main._Game.SelectedObject;
        //    else if (main._Game.SelectedObject is BuiltObject && (main._Game.SelectedObject as BuiltObject).ActualEmpire == main._Game.PlayerEmpire)
        //    {
        //        BaconBuiltObject.globalCargoMissionSource = main._Game.SelectedObject;
        //    }
        //    else
        //    {
        //        string caption = "Source selection error.";
        //        BaconBuiltObject.ShowMessageBox(main, "You must select bases or planets which your empire owns.", caption);
        //    }
        //}

        //public static void AssignGlobalCargoMissionDestination(Main main)
        //{
        //    if (main._Game.SelectedObject is Habitat && (main._Game.SelectedObject as Habitat).Owner == main._Game.PlayerEmpire)
        //        BaconBuiltObject.globalCargoMissionDestination = main._Game.SelectedObject;
        //    else if (main._Game.SelectedObject is BuiltObject && (main._Game.SelectedObject as BuiltObject).ActualEmpire == main._Game.PlayerEmpire)
        //    {
        //        BaconBuiltObject.globalCargoMissionDestination = main._Game.SelectedObject;
        //    }
        //    else
        //    {
        //        string caption = "Destination selection error.";
        //        BaconBuiltObject.ShowMessageBox(main, "You must select bases or planets which your empire owns.", caption);
        //    }
        //}

        public static void AssignCargoMission(Main main, BuiltObject ship, bool repeat)
        {
            BuiltObject freighter = (BuiltObject)null;
            object sourceObj = (object)null;
            object targetObj = (object)null;
            bool flag1 = false;
            if (main == null)
            { return; }

            if (ship == null && main._Game.SelectedObject is BuiltObject)
            {
                freighter = (BuiltObject)main._Game.SelectedObject;
                if (freighter != null && freighter.Role == BuiltObjectRole.Freight
                       && (main._Game.PlayerEmpire.BuiltObjects.Contains(main._Game.SelectedObject)
                       || (BaconBuiltObject.AllowPrivateShipAssigment && main._Game.PlayerEmpire.PrivateBuiltObjects.Contains(main._Game.SelectedObject))))
                {
                    //sourceHab = BaconBuiltObject.globalCargoMissionSource;
                    //targetHab = BaconBuiltObject.globalCargoMissionDestination;

                    bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                    if (!flag)
                    { main._Game.Galaxy.Pause(); }
                    using SelectCargoMissionTargets selectForm = new SelectCargoMissionTargets(main);
                    if (selectForm.ShowDialog(main) == DialogResult.OK)
                    {
                        sourceObj = selectForm.SelectedSource;
                        targetObj = selectForm.SelectedTarget;
                        repeat = selectForm.Repeatable;
                    }
                    if (!flag)
                    { main._Game.Galaxy.Resume(); }
                    if (selectForm.DialogResult != DialogResult.OK)
                    { return; }

                }
                else
                {
                    MessageBoxEx messageBoxEx = MessageBoxExManager.CreateMessageBox(null, BaconBuiltObject.myMain.font_3);
                    messageBoxEx.Text = "Wrong target";
                    messageBoxEx.Caption = "Warning";
                    messageBoxEx.AddButton(MessageBoxExButtons.Ok);
                    messageBoxEx.Icon = MessageBoxExIcon.Warning;
                    messageBoxEx.Show(BaconBuiltObject.myMain);
                }
            }
            else
            {
                if (!(ship is BuiltObject))
                    return;
                freighter = (BuiltObject)ship;
                if (repeat && freighter.BaconValues != null && freighter.BaconValues.ContainsKey("RepeatingMission"))
                {
                    BuiltObjectMission baconValue = (BuiltObjectMission)freighter.BaconValues["RepeatingMission"];
                    sourceObj = baconValue.Target;
                    targetObj = baconValue.SecondaryTarget;
                    flag1 = true;
                }
            }
            if (!flag1 && ((main._Game.SelectedObject as BuiltObject).CargoCapacity <= 0 || (main._Game.SelectedObject as BuiltObject).Empire != main._Game.PlayerEmpire && (main._Game.SelectedObject as BuiltObject).ActualEmpire != main._Game.PlayerEmpire || targetObj == null || sourceObj == null))
                return;
            CargoList cargo1 = new CargoList();
            IEnumerable<Cargo> source = (IEnumerable<Cargo>)new List<Cargo>();
            int cargoQuantityDivisor = 2;
            bool flag2 = false;
            if (targetObj is BuiltObject && ((BuiltObject)targetObj).SubRole == BuiltObjectSubRole.ConstructionShip)
            {
                cargoQuantityDivisor = 1000;
                flag2 = true;
            }
            for (; !source.Any<Cargo>() && cargoQuantityDivisor < 51 | flag2 && cargoQuantityDivisor < 1001; ++cargoQuantityDivisor)
            {
                if (sourceObj is BuiltObject && (sourceObj as BuiltObject).Cargo != null)
                {
                    source = (sourceObj as BuiltObject).Cargo.Where<Cargo>((Func<Cargo, bool>)(x => x.Amount >= freighter.CargoCapacity / cargoQuantityDivisor && x.EmpireId == main._Game.PlayerEmpire.EmpireId));
                }
                else
                {
                    if (!(sourceObj is Habitat) || (sourceObj as Habitat).Cargo == null)
                        return;
                    source = (sourceObj as Habitat).Cargo.Where<Cargo>((Func<Cargo, bool>)(x => x.Amount >= freighter.CargoCapacity / cargoQuantityDivisor && x.EmpireId == main._Game.PlayerEmpire.EmpireId));
                }
            }
            if (flag2)
            {
                List<Cargo> list = source.ToList<Cargo>();
                if (list != null)
                {
                    list.RemoveAll((Predicate<Cargo>)(x => x.CommodityIsResource && x.Resource.IsLuxuryResource));
                    list.RemoveAll((Predicate<Cargo>)(x => x.CommodityIsComponent));
                }
                source = (IEnumerable<Cargo>)list;
            }
            if (freighter.BaconValues != null && freighter.BaconValues.ContainsKey("cargos"))
                source = (IEnumerable<Cargo>)BaconBuiltObject.ReplaceMissionCargoWithCustomCargo(main, freighter);
            int num1 = source.Count<Cargo>();
            if (num1 < 1)
                return;
            foreach (Cargo cargo2 in source)
            {
                Cargo cargo = cargo2;
                int amount = freighter.CargoCapacity / num1;
                if (flag2)
                {
                    int num2 = 0;
                    Cargo cargo3 = (targetObj as BuiltObject).Cargo.FirstOrDefault<Cargo>((Func<Cargo, bool>)(x => x.CommodityIsResource && (int)x.CommodityResource.ResourceID == (int)cargo.CommodityResource.ResourceID));
                    if (cargo3 != null)
                        num2 = cargo3.Amount;
                    amount -= num2;
                }
                if (amount > 0)
                    cargo1.Add(new Cargo(cargo.Resource, amount, freighter.ActualEmpire));
            }
            freighter.AssignMission(BuiltObjectMissionType.Transport, sourceObj, targetObj, cargo1, BuiltObjectMissionPriority.High);
            if (flag2)
                BaconBuiltObject.RemoveAllCommandsOfTypeFromQueue(freighter, CommandAction.Refuel);
            if (!repeat)
                return;
            if (freighter.BaconValues == null)
                freighter.BaconValues = new Dictionary<string, object>();
            if (!freighter.BaconValues.ContainsKey("RepeatingMission"))
            {
                BuiltObjectMission builtObjectMission = freighter.Mission.Clone();
                freighter.BaconValues.Add("RepeatingMission", (object)builtObjectMission);
            }

        }

        public static List<Cargo> ReplaceMissionCargoWithCustomCargo(Main main, BuiltObject freighter)
        {
            List<Cargo> cargoList = new List<Cargo>();
            try
            {
                if (freighter.BaconValues != null && freighter.BaconValues.ContainsKey("cargos"))
                {
                    List<string> baconValue = (List<string>)freighter.BaconValues["cargos"];
                    if (baconValue != null && baconValue.Count > 0)
                    {
                        foreach (string str in baconValue)
                        {
                            string cargoString = str;
                            ResourceDefinition resourceDefinition = main._Game.Galaxy.ResourceSystem.Resources.FirstOrDefault<ResourceDefinition>((Func<ResourceDefinition, bool>)(x => x.Name.ToLower().StartsWith(cargoString)));
                            if (resourceDefinition != null)
                            {
                                ResourceDefinition resource = main._Game.Galaxy.ResourceSystem.Resources[(int)resourceDefinition.ResourceID];
                                if (resource != null)
                                {
                                    Cargo cargo = new Cargo(new Resource(resource.ResourceID), 1, freighter.Empire);
                                    cargoList.Add(cargo);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return cargoList;
        }

        public static void RemoveAllCommandsOfTypeFromQueue(
          BuiltObject ship,
          CommandAction actionToRemove)
        {
            if (ship.Mission == null || !ship.Mission.CheckCommandsForAction(CommandAction.Refuel))
                return;
            Command[] commandArray = ship.Mission.ShowAllCommands();
            CommandQueue commands = new CommandQueue();
            foreach (Command command in commandArray)
            {
                if (command.Action != actionToRemove)
                    commands.Enqueue(command);
            }
            ship.Mission.ReplaceCommandStack(commands);
        }

        public static void CycleShipSelected(Main main, string direction)
        {
            BuiltObject ship = main._Game.SelectedObject as BuiltObject;
            BuiltObjectSubRole subRole = ship.SubRole;
            Empire empire = ship.Empire;
            if (empire == null || empire.Name == "Independent")
                empire = ship.ActualEmpire;
            List<BuiltObject> list = empire.BuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.SubRole == subRole)).OrderBy<BuiltObject, int>((Func<BuiltObject, int>)(x => x.BuiltObjectID)).ToList<BuiltObject>();
            list.AddRange((IEnumerable<BuiltObject>)empire.PrivateBuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.SubRole == subRole)).OrderBy<BuiltObject, int>((Func<BuiltObject, int>)(x => x.BuiltObjectID)).ToList<BuiltObject>());
            BuiltObject builtObject;
            if (direction == "forward")
            {
                int index = list.FindIndex((Predicate<BuiltObject>)(x => x.BuiltObjectID == ship.BuiltObjectID));
                builtObject = list[index + 1 < list.Count ? index + 1 : 0];
            }
            else
            {
                int index = list.FindIndex((Predicate<BuiltObject>)(x => x.BuiltObjectID == ship.BuiltObjectID));
                builtObject = list[index - 1 > -1 ? index - 1 : list.Count - 1];
            }
            main._Game.SelectedObject = (object)builtObject;
            if (main._Game.SelectedObject == null)
                return;
            main.method_208(main._Game.SelectedObject);
        }

        public static void CycleFleetSelected(Main main, string direction)
        {
            ShipGroup fleet = main._Game.SelectedObject as ShipGroup;
            List<ShipGroup> list = fleet.Empire.ShipGroups.ToList<ShipGroup>();
            ShipGroup shipGroup;
            if (direction == "forward")
            {
                int index = list.FindIndex((Predicate<ShipGroup>)(x => x.Name == fleet.Name));
                shipGroup = list[index + 1 < list.Count ? index + 1 : 0];
            }
            else
            {
                int index = list.FindIndex((Predicate<ShipGroup>)(x => x.Name == fleet.Name));
                shipGroup = list[index - 1 > -1 ? index - 1 : list.Count - 1];
            }
            main._Game.SelectedObject = (object)shipGroup;
            if (main._Game.SelectedObject == null)
                return;
            main.method_208(main._Game.SelectedObject);
        }

        public static void CycleFighterSelected(Main main, string direction)
        {
            BuiltObject parentBuiltObject = (main._Game.SelectedObject as Fighter).ParentBuiltObject;
            Fighter selectedFighter = main._Game.SelectedObject as Fighter;
            List<Fighter> fighters = (List<Fighter>)parentBuiltObject.Fighters;
            Fighter fighter;
            if (direction == "forward")
            {
                int index = fighters.FindIndex((Predicate<Fighter>)(x => x.FighterID == selectedFighter.FighterID));
                fighter = fighters[index + 1 < fighters.Count ? index + 1 : 0];
            }
            else
            {
                int index = fighters.FindIndex((Predicate<Fighter>)(x => x.FighterID == selectedFighter.FighterID));
                fighter = fighters[index - 1 > -1 ? index - 1 : fighters.Count - 1];
            }
            main._Game.SelectedObject = (object)fighter;
            if (main._Game.SelectedObject == null)
                return;
            main.method_208(main._Game.SelectedObject);
        }

        public static void SetShipNearestStar(BuiltObject ship)
        {
            Habitat habitat = ship.NearestSystemStar;
            if (habitat != null)
                return;
            if (BaconBuiltObject.myMain != null)
            {
                Habitat nearestSystem = BaconBuiltObject.myMain._Game.Galaxy.FastFindNearestSystem(ship.Xpos, ship.Ypos);
                if (nearestSystem != null && Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, nearestSystem.Xpos, nearestSystem.Ypos) < (double)(Galaxy.MaxSolarSystemSize * Galaxy.MaxSolarSystemSize + 1000000))
                    habitat = nearestSystem;
            }
            if (habitat != null)
            {
                ship.NearestSystemStar = habitat;
                if (ship.ActualEmpire != null)
                    ship.ActualEmpire.ResolveSystemVisibility(ship, false);
            }
        }

        public static void AssignNearestSystemStarIfNull(BuiltObject ship)
        {
            Habitat habitat = ship.NearestSystemStar;
            if (habitat != null)
                return;
            if (BaconBuiltObject.myMain != null)
            {
                Habitat nearestSystem = BaconBuiltObject.myMain._Game.Galaxy.FastFindNearestSystem(ship.Xpos, ship.Ypos);
                if (nearestSystem != null && Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, nearestSystem.Xpos, nearestSystem.Ypos) < (double)(Galaxy.MaxSolarSystemSize * Galaxy.MaxSolarSystemSize + 1000000))
                    habitat = nearestSystem;
            }
            if (habitat != null)
            {
                ship.NearestSystemStar = habitat;
                if (ship.ActualEmpire != null)
                    ship.ActualEmpire.ResolveSystemVisibility(ship, false);
            }
        }

        public static bool IsOutsideStarGravityWell(BuiltObject ship)
        {
            Habitat habitat = ship.NearestSystemStar;
            if (habitat == null)
            {
                if (BaconBuiltObject.myMain != null)
                {
                    Habitat nearestSystem = BaconBuiltObject.myMain._Game.Galaxy.FastFindNearestSystem(ship.Xpos, ship.Ypos);
                    if (nearestSystem != null && Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, nearestSystem.Xpos, nearestSystem.Ypos) < (double)(Galaxy.MaxSolarSystemSize * Galaxy.MaxSolarSystemSize + 1000000))
                        habitat = nearestSystem;
                }
                if (habitat == null)
                    return true;
                ship.NearestSystemStar = habitat;
                if (ship.ActualEmpire == null)
                    ;
            }
            if (!BaconBuiltObject.useStarGravityWells)
                return true;
            double reductionForSmallShip = BaconBuiltObject.GetGravityWellReductionForSmallShip(ship);
            double mitigationForHyperDrive = BaconBuiltObject.GetGravityWellMitigationForHyperDrive(ship);
            int num = (int)habitat.SolarRadiation + (int)habitat.MicrowaveRadiation + (int)habitat.XrayRadiation;
            return habitat.Category == HabitatCategoryType.GasCloud || Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, habitat.Xpos, habitat.Ypos) > BaconBuiltObject.starGravityWellRangeSquared * ((double)num / 100.0) * ((double)num / 100.0) * reductionForSmallShip * reductionForSmallShip * mitigationForHyperDrive * mitigationForHyperDrive;
        }

        public static double GetGravityWellMitigationForHyperDrive(BuiltObject ship)
        {
            double mitigationForHyperDrive = 1.0;
            List<BuiltObjectComponent> list = ship.Components.Where<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>)(x => x.Category == ComponentCategoryType.HyperDrive)).ToList<BuiltObjectComponent>();
            if (list != null && list.Count<BuiltObjectComponent>() > 0)
            {
                foreach (BuiltObjectComponent builtObjectComponent in list)
                {
                    int num1 = builtObjectComponent.Value4;
                    int num2 = builtObjectComponent.Value5;
                    if (num1 > 0 && num2 > 0)
                    {
                        double num3 = Convert.ToDouble(num1) / Convert.ToDouble(num2);
                        if (num3 < mitigationForHyperDrive)
                            mitigationForHyperDrive = num3;
                    }
                }
            }
            return mitigationForHyperDrive;
        }

        public static bool CheckHyperjumpPending(BuiltObject ship)
        {
            Habitat nearestSystemStar = ship.NearestSystemStar;
            if (nearestSystemStar != null && BaconBuiltObject.myMain != null && ship.ActualEmpire != null && (ship.ActualEmpire.SystemVisibility[nearestSystemStar.SystemIndex].Status == SystemVisibilityStatus.Unexplored || ship.ActualEmpire.SystemVisibility[nearestSystemStar.SystemIndex].Status == SystemVisibilityStatus.Undefined))
            {
                ship.ActualEmpire.SystemVisibility[nearestSystemStar.SystemIndex].Status = SystemVisibilityStatus.Visible;
                ship.ActualEmpire.ResolveSystemVisibility(ship, false);
            }
            if (!BaconBuiltObject.IsOutsideStarGravityWell(ship) || ship.Mission == null)
                return false;
            if (ship.Mission.Type == BuiltObjectMissionType.Escape)
                return true;
            if (ship.Mission.Type == BuiltObjectMissionType.Refuel || ship.Mission.Type == BuiltObjectMissionType.Repair || ship.Mission.Type == BuiltObjectMissionType.Retrofit)
            {
                Point point = ship.Mission.ResolveTargetCoordinates(ship.Mission);
                if (Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, (double)point.X, (double)point.Y) > (double)(Galaxy.HyperJumpThreshhold * Galaxy.HyperJumpThreshhold) && ship.WarpSpeed > 0)
                    return true;
            }
            Command command = ship.Mission.FastPeekCurrentCommand();
            return command != null && command.Action == CommandAction.HyperTo;
        }

        public static bool CheckFightersOnboardAndRetrieve(BuiltObject ship)
        {
            bool flag = BaconBuiltObject.IsOutsideStarGravityWell(ship);
            if (!flag || ship.Fighters == null || ship.Fighters.Count <= 0)
                return flag;
            for (int index = 0; index < ship.Fighters.Count; ++index)
            {
                Fighter fighter = ship.Fighters[index];
                if (!fighter.OnboardCarrier && !fighter.HasBeenDestroyed)
                {
                    flag = false;
                    fighter.ReturnToCarrier();
                }
            }
            return flag;
        }

        public static void CheckNearTarget(BuiltObject ship)
        {
            if (ship.Name.StartsWith("???"))
                BaconBuiltObject.myMain._Game.Galaxy.Pause();
            if (ship == null || ship.Mission == null || ship.Mission.FastPeekCurrentCommand() == null || ship.Mission.FastPeekCurrentCommand().Action != CommandAction.HyperTo)
                return;
            if (ship.Mission.Type == BuiltObjectMissionType.Escape)
                return;
            try
            {
                double x2 = 0.0;
                double y2 = 0.0;
                object target = ship?.Mission?.Target;
                object secondaryTarget = ship?.Mission?.SecondaryTarget;
                StellarObject stellarObject = ((StellarObject)ship.Mission.FastPeekCurrentCommand().TargetHabitat ?? (StellarObject)ship.Mission.FastPeekCurrentCommand().TargetBuiltObject) ?? (StellarObject)ship.Mission.FastPeekCurrentCommand().TargetCreature;
                if (stellarObject == null && target != null)
                    stellarObject = !(target is ShipGroup) ? (StellarObject)target : (StellarObject)((ShipGroup)target).Ships[0];
                if (target != null && stellarObject != null)
                {
                    string name = stellarObject.Name;
                    if (target is BuiltObject && name == ship.Mission.TargetBuiltObject.Name)
                    {
                        x2 = (target as StellarObject).Xpos;
                        y2 = (target as StellarObject).Ypos;
                    }
                    else if (target is Habitat && name == ship.Mission.TargetHabitat.Name)
                    {
                        x2 = (target as StellarObject).Xpos;
                        y2 = (target as StellarObject).Ypos;
                    }
                    else if (target is Creature && name == ship.Mission.TargetCreature.Name)
                    {
                        x2 = (target as StellarObject).Xpos;
                        y2 = (target as StellarObject).Ypos;
                    }
                    else if (secondaryTarget is BuiltObject && name == ship.Mission.SecondaryTargetBuiltObject.Name)
                    {
                        x2 = (secondaryTarget as StellarObject).Xpos;
                        y2 = (secondaryTarget as StellarObject).Ypos;
                    }
                    else if (secondaryTarget is Habitat && name == ship.Mission.SecondaryTargetHabitat.Name)
                    {
                        x2 = (secondaryTarget as StellarObject).Xpos;
                        y2 = (secondaryTarget as StellarObject).Ypos;
                    }
                    else if (secondaryTarget is Creature && name == ship.Mission.SecondaryTargetCreature.Name)
                    {
                        x2 = (secondaryTarget as StellarObject).Xpos;
                        y2 = (secondaryTarget as StellarObject).Ypos;
                    }
                }
                if (Math.Abs(x2) < 0.01 && Math.Abs(y2) < 0.01)
                {
                    if (stellarObject != null)
                    {
                        x2 = stellarObject.Xpos;
                        y2 = stellarObject.Ypos;
                    }
                    else
                    {
                        x2 = (double)ship.Mission.X;
                        y2 = (double)ship.Mission.Y;
                    }
                }
                if ((double)Math.Abs(ship.Mission.FastPeekCurrentCommand().Xpos - (float)x2) > 10.0 || (double)Math.Abs(ship.Mission.FastPeekCurrentCommand().Ypos - (float)y2) > 10.0)
                {
                    if (!ship.Mission.ManuallyAssigned && stellarObject is BuiltObject && (double)(stellarObject as BuiltObject).CurrentSpeed > (double)(stellarObject as BuiltObject).TopSpeed && (stellarObject as BuiltObject).ActualEmpire != ship.ActualEmpire)
                    {
                        ship.Mission.Clear();
                    }
                    else
                    {
                        ship.Mission.FastPeekCurrentCommand().Xpos = (float)x2;
                        ship.Mission.FastPeekCurrentCommand().Ypos = (float)y2;
                    }
                }
                if (ship.Mission != null && Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, x2, y2) <= (double)(Galaxy.HyperJumpThreshhold * Galaxy.HyperJumpThreshhold))
                {
                    ship.Mission.CompleteCommand();
                    if (ship.Mission != null && ship.Mission.Target != null && ship.Mission.FastPeekCurrentCommand().Action == CommandAction.MoveTo && ship.Mission.ShowNextCommand().Action == CommandAction.ConditionalHyperTo)
                        ship.Mission.CompleteCommand();
                    if ((double)ship.CurrentSpeed > (double)ship.TopSpeed)
                        ship.CurrentSpeed = (float)ship.CruiseSpeed;
                }
                if (ship.NearestSystemStar == null || ship.ActualEmpire == null || (double)ship.CurrentSpeed > (double)ship.TopSpeed)
                    return;
                ship.ActualEmpire.ResolveSystemVisibility(ship, false);
            }
            catch (Exception ex)
            {
            }
        }

        public static void ForceUnloadAtDestination(Main main, BuiltObject ship)
        {
            if (ship == null || ship == null)
                return;
            BuiltObjectMission mission = ship.Mission;
            FieldInfo field1 = typeof(BuiltObjectMission).GetField("_MissionCargo", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field1 != (FieldInfo)null)
                field1.SetValue((object)mission, (object)ship.Cargo);
            CommandQueue commandQueue = new CommandQueue();
            Command command1 = new Command(CommandAction.ConditionalHyperTo, mission.TargetBuiltObject);
            Command command2 = new Command(CommandAction.SetParent, mission.TargetBuiltObject);
            Command command3 = new Command(CommandAction.MoveTo, mission.TargetBuiltObject);
            Command command4 = new Command(CommandAction.Dock, mission.TargetBuiltObject);
            Command command5 = new Command(CommandAction.Undock, mission.TargetBuiltObject);
            Command command6 = new Command(CommandAction.Attack, mission.TargetBuiltObject);
            Command command7 = new Command(CommandAction.ExtractResources, mission.TargetBuiltObject);
            Command command8 = new Command(CommandAction.ImpulseTo, mission.TargetBuiltObject);
            Command command9 = new Command(CommandAction.Blockade, mission.TargetBuiltObject);
            Command command10 = new Command(CommandAction.Escort, mission.TargetBuiltObject);
            Command command11 = new Command(CommandAction.SprintTo, mission.TargetBuiltObject);
            Command command12 = new Command(CommandAction.SelectTargetToAttack, mission.TargetBuiltObject);
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(command4.Clone());
            if (mission.Cargo != null && mission.Cargo.Count > 0)
                commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Cargo.Clone()));
            commandQueue.Enqueue(command5.Clone());
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(mission.GenerateParkCommand());
            FieldInfo field2 = typeof(BuiltObjectMission).GetField("_Commands", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field2 != (FieldInfo)null)
                field2.SetValue((object)mission, (object)commandQueue);
            ship.Mission = mission;
        }

        public static void CreateCharacter(Main main, BuiltObject ship, CharacterRole role)
        {
            Empire empire = ship.Empire;
            bool isRandomCharacter = true;
            Character newCharacter = empire.GenerateNewCharacter(role, (StellarObject)null, false, out isRandomCharacter);
            newCharacter.Activate(main._Game.Galaxy, empire, (StellarObject)empire.Capital);
            string description = string.Format(TextResolver.GetText("New Character Appeared Message ROLE NAME LOCATION"), (object)Galaxy.ResolveDescription(newCharacter.Role), (object)newCharacter.Name, (object)empire.Capital.Name);
            empire.SendMessageToEmpire(empire, EmpireMessageType.CharacterAppearance, (object)newCharacter, description);
        }

        public static void FreighterToConstructionShip(Main main, BuiltObject ship)
        {
            ship.Owner = main._Game.PlayerEmpire;
            ship.Empire = main._Game.PlayerEmpire;
            ship.SubRole = BuiltObjectSubRole.ConstructionShip;
            ship.Role = BuiltObjectRole.Build;
        }

        public static void GiveAllResourcesToPlanet(Main main, int abundance = 900)
        {
            if (!(main._Game.SelectedObject is Habitat))
                return;
            Habitat selectedObject = main._Game.SelectedObject as Habitat;
            HabitatResourceList habitatResourceList = new HabitatResourceList();
            for (byte resourceId = 0; resourceId < (byte)19; ++resourceId)
                habitatResourceList.Add(new HabitatResource(resourceId, abundance));
            selectedObject.Resources = habitatResourceList;
        }

        public static List<string> ReadPassengerShipSettings()
        {
            List<string> stringList = new List<string>();
            try
            {
                using (StreamReader streamReader = new StreamReader("Passengers.txt"))
                {
                    string str1;
                    while ((str1 = streamReader.ReadLine()) != null)
                    {
                        if (!str1.StartsWith("//") && !string.IsNullOrEmpty(str1))
                        {
                            if (str1.StartsWith("source"))
                            {
                                string[] strArray = str1.Split('=');
                                string str2 = strArray[strArray.Length - 1];
                                stringList.Add(str2);
                            }
                            else if (str1.StartsWith("destination"))
                            {
                                string[] strArray = str1.Split('=');
                                string str3 = strArray[strArray.Length - 1];
                                stringList.Add(str3);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
            }
            catch (Exception ex)
            {
                string stackTrace = ex.StackTrace;
            }
            return stringList;
        }

        public static short SpeedMultiplier(short calculatedSpeed, BuiltObject upgradingShip) => calculatedSpeed;

        public static void ConvertShipRole(BuiltObject ship)
        {
            if (!ship.Name.Contains("AUX"))
                return;
            if (ship.Name.Contains("Gas"))
            {
                ship.SubRole = BuiltObjectSubRole.GasMiningShip;
                ship.Role = BuiltObjectRole.Resource;
                ship.Stance = BuiltObjectStance.DoNotAttack;
                ship.AttackRangeSquared = 0.0f;
            }
            else if (ship.Name.Contains("Miner"))
            {
                ship.SubRole = BuiltObjectSubRole.MiningShip;
                ship.Role = BuiltObjectRole.Resource;
                ship.Stance = BuiltObjectStance.DoNotAttack;
                ship.AttackRangeSquared = 0.0f;
            }
            else if (ship.Name.Contains("Passenger"))
            {
                ship.SubRole = BuiltObjectSubRole.PassengerShip;
                ship.Role = BuiltObjectRole.Passenger;
                ship.Stance = BuiltObjectStance.DoNotAttack;
                ship.AttackRangeSquared = 0.0f;
            }
            else if (ship.Name.Contains("Constructor"))
            {
                ship.SubRole = BuiltObjectSubRole.ConstructionShip;
                ship.Role = BuiltObjectRole.Build;
                ship.Stance = BuiltObjectStance.DoNotAttack;
            }
            ship.Name = ship.Name.Replace("AUX", "State");
        }

        public static void ModMyShip(BuiltObject ship)
        {
            if ((double)Math.Abs(BaconBuiltObject.sublightFuelBurnDivisor - 1f) > 0.01 && (double)Math.Abs(BaconBuiltObject.sublightFuelBurnDivisor) > 0.01)
            {
                float val1_1 = (float)ship.CruiseSpeedFuelBurn / BaconBuiltObject.sublightFuelBurnDivisor;
                float val1_2 = (float)ship.TopSpeedFuelBurn / BaconBuiltObject.sublightFuelBurnDivisor;
                ship.CruiseSpeedFuelBurn = Math.Max((short)val1_1, (short)1);
                ship.TopSpeedFuelBurn = Math.Max((short)val1_2, (short)1);
            }
            if (ship.ActualEmpire.PirateEmpireBaseHabitat != null && ship.Role == BuiltObjectRole.Freight && ship.Empire == ship.ActualEmpire && BaconBuiltObject.myMain != null && BaconBuiltObject.myMain._Game != null && BaconBuiltObject.myMain._Game.Galaxy != null && BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire != null)
                ship.Empire = BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire;
            if (ship.Role == BuiltObjectRole.Base)
                BaconBuiltObject.ModWeaponRangeForBases(ship);
            if (BaconBuiltObject.IsMyShip(ship))
            {
                try
                {
                    Empire empire = ship.Empire;
                    int num1 = Math.Min(10, Math.Max(0, empire.CountResourceSupplyLocations((byte)3, true)));
                    int num2 = Math.Min(10, Math.Max(0, empire.CountResourceSupplyLocations((byte)8, true)));
                    int num3 = Math.Min(10, Math.Max(0, empire.CountResourceSupplyLocations((byte)18, true)));
                    int num4 = Math.Min(10, Math.Max(0, empire.CountResourceSupplyLocations((byte)4, true)));
                    int num5 = Math.Min(10, Math.Max(0, empire.CountResourceSupplyLocations((byte)6, true)));
                    int num6 = Math.Min(10, Math.Max(0, empire.CountResourceSupplyLocations((byte)0, true)));
                    int num7 = Math.Min(10, Math.Max(0, empire.CountResourceSupplyLocations((byte)1, true)));
                    int num8 = 0;
                    if (ship.FuelType != null && ship.FuelType.Name == "Hydrogen")
                        num8 = num2;
                    else if (ship.FuelType != null && ship.FuelType.Name == "Caslon")
                        num8 = num3;
                    ship.CruiseSpeed += (short)(num8 * (int)ship.CruiseSpeed / 10);
                    ship.TopSpeed += (short)(int)(short)(num8 * (int)ship.TopSpeed / 10);
                    ship.WarpSpeed += num1 * ship.WarpSpeed / 10;
                    ship.CruiseSpeedFuelBurn /= (short)2;
                    ship.TopSpeedFuelBurn /= (short)2;
                    ship.WarpSpeedFuelBurn /= 2;
                    ship.TurnRate *= 2f;
                    if (ship.Role == BuiltObjectRole.Military && ship.AssaultStrength > (short)0)
                    {
                        ship.AssaultStrength *= (short)4;
                        ship.AssaultShieldPenetration *= (short)4;
                    }
                    FieldInfo field = typeof(BuiltObject).GetField("_CargoCapacity", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (field != (FieldInfo)null)
                        field.SetValue((object)ship, (object)(ship.CargoCapacity * 5));
                    ship.ConstructionQueue?.ConstructionYards?.ForEach((Action<ConstructionYard>)(x => x.ConstructionSpeed *= 10));
                    foreach (Weapon weapon in (SyncList<Weapon>)ship.Weapons)
                    {
                        int range = weapon.Range;
                        int speed = weapon.Speed;
                        if (weapon.Component != null)
                        {
                            ComponentImprovement componentImprovement = new ComponentImprovement((Component)weapon.Component);
                            switch (weapon.Component.Category)
                            {
                                case ComponentCategoryType.WeaponBeam:
                                case ComponentCategoryType.WeaponSuperBeam:
                                    int num9 = weapon._ImprovedComponent.Value2;
                                    int num10 = num9 + num4 * num9 / 10;
                                    componentImprovement.Value2 = num10;
                                    int num11 = weapon._ImprovedComponent.Value4;
                                    int num12 = num11 + num4 * num11 / 10;
                                    componentImprovement.Value4 = num12;
                                    break;
                                case ComponentCategoryType.WeaponTorpedo:
                                    int num13 = range + num5 * range / 10;
                                    componentImprovement.Value2 = num13;
                                    int num14 = weapon._ImprovedComponent.Value4;
                                    int num15 = num14 + num5 * num14 / 10;
                                    if (num15 < (int)ship.TopSpeed)
                                        num15 = (int)ship.TopSpeed;
                                    componentImprovement.Value4 = num15;
                                    break;
                                case ComponentCategoryType.WeaponArea:
                                case ComponentCategoryType.WeaponSuperArea:
                                    int num16 = range + num7 * range / 10;
                                    componentImprovement.Value2 = num16;
                                    break;
                                case ComponentCategoryType.WeaponIon:
                                case ComponentCategoryType.WeaponGravity:
                                    int num17 = range + num6 * range / 10;
                                    componentImprovement.Value2 = num17;
                                    break;
                            }
                            weapon._ImprovedComponent = componentImprovement;
                        }
                    }
                    if (ship.Weapons != null && ship.Weapons.Count > 0)
                        ship.MaximumWeaponsRange = ship.Weapons.OrderByDescending<Weapon, int>((Func<Weapon, int>)(x => x.Range)).ToList<Weapon>()[0].Range;
                }
                catch (Exception ex)
                {
                }
            }
            if (BaconBuiltObject.IsFreeTrader(BaconBuiltObject.myMain, (object)ship) && !ship.Design.Name.Contains("_ft"))
                BaconBuiltObject.GiveShipUniqueDesign(BaconBuiltObject.myMain, ship);
            BaconBuiltObject.ApplyCrewExperience(ship);
        }

        public static void ModWeaponRangeForBases(BuiltObject ship)
        {
            if (ship.Weapons == null || ship.Weapons.Count == 0)
                return;
            foreach (Weapon weapon in (SyncList<Weapon>)ship.Weapons)
            {
                int range = weapon.Range;
                int speed = weapon.Speed;
                if (weapon.Component != null)
                    weapon._ImprovedComponent = new ComponentImprovement((Component)weapon.Component)
                    {
                        Value2 = Convert.ToInt32((float)range * BaconBuiltObject.weaponRangeMultiplierForBases),
                        Value4 = weapon._ImprovedComponent.Value4 * 2
                    };
            }
            if (ship.Weapons == null || ship.Weapons.Count <= 0)
                return;
            ship.MaximumWeaponsRange = ship.Weapons.OrderByDescending<Weapon, int>((Func<Weapon, int>)(x => x.Range)).ToList<Weapon>()[0].Range;
        }

        public static void ApplyCrewExperience(BuiltObject ship)
        {
            if (ship.Role != BuiltObjectRole.Military)
                return;
            string crewLevel = BaconBuiltObject.CalculateCrewLevel((Main)null, ship);
            int num1 = 0;
            switch (crewLevel)
            {
                case "average":
                    num1 = 1;
                    break;
                case "experienced":
                    num1 = 2;
                    break;
                case "veteran":
                    num1 = 3;
                    break;
                case "elite":
                    num1 = 4;
                    break;
                case "legendary":
                    num1 = 5;
                    break;
            }
            Random random = new Random((int)ship.DateBuilt % 1000 + ship.BuiltObjectID);
            for (; num1 > 0; --num1)
            {
                switch (random.Next(5))
                {
                    case 0:
                        ship.CruiseSpeed += (short)((int)ship.CruiseSpeed / 10);
                        ship.TopSpeed += (short)(int)(short)((int)ship.TopSpeed / 10);
                        break;
                    case 1:
                        using (IEnumerator<Weapon> enumerator = ship.Weapons.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Weapon current = enumerator.Current;
                                if (current.Component != null)
                                {
                                    ComponentImprovement componentImprovement = new ComponentImprovement((Component)current.Component);
                                    switch (current.Component.Category)
                                    {
                                        case ComponentCategoryType.WeaponBeam:
                                        case ComponentCategoryType.WeaponTorpedo:
                                        case ComponentCategoryType.WeaponArea:
                                        case ComponentCategoryType.WeaponIon:
                                        case ComponentCategoryType.WeaponGravity:
                                        case ComponentCategoryType.WeaponSuperBeam:
                                            int num2 = current._ImprovedComponent.Value2;
                                            int num3 = num2 + num2 / 10;
                                            componentImprovement.Value2 = num3;
                                            int num4 = current._ImprovedComponent.Value4;
                                            int num5 = num4 + num4 / 10;
                                            componentImprovement.Value4 = num5;
                                            break;
                                    }
                                    current._ImprovedComponent = componentImprovement;
                                }
                            }
                            break;
                        }
                    case 2:
                        ship.ShieldsCapacity += ship.ShieldsCapacity / 10;
                        ship.ShieldRechargeRate += ship.ShieldRechargeRate / 10f;
                        break;
                    case 3:
                        ship.TargettingModifier += (short)Math.Max(10, (int)ship.TargettingModifier / 4);
                        ship.CountermeasureModifier += (short)Math.Max(10, (int)ship.CountermeasureModifier / 4);
                        ship.StaticEnergyConsumption -= (int)(short)(ship.StaticEnergyConsumption / 5);
                        ship.FuelCapacity += ship.FuelCapacity / 10;
                        break;
                    case 4:
                        bool flag = true;
                        using (IEnumerator<Weapon> enumerator = ship.Weapons.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Weapon current = enumerator.Current;
                                if (current.Component != null)
                                {
                                    ComponentImprovement componentImprovement = new ComponentImprovement((Component)current.Component);
                                    componentImprovement.Value1 = current._ImprovedComponent.Value1;
                                    componentImprovement.Value2 = current._ImprovedComponent.Value2;
                                    componentImprovement.Value3 = current._ImprovedComponent.Value3;
                                    componentImprovement.Value4 = current._ImprovedComponent.Value4;
                                    componentImprovement.Value5 = current._ImprovedComponent.Value5;
                                    componentImprovement.Value6 = current._ImprovedComponent.Value6;
                                    componentImprovement.Value7 = current._ImprovedComponent.Value7;
                                    switch (current.Component.Category)
                                    {
                                        case ComponentCategoryType.WeaponBeam:
                                        case ComponentCategoryType.WeaponTorpedo:
                                        case ComponentCategoryType.WeaponArea:
                                        case ComponentCategoryType.WeaponIon:
                                        case ComponentCategoryType.WeaponGravity:
                                        case ComponentCategoryType.WeaponSuperBeam:
                                            int num6 = current._ImprovedComponent.Value1;
                                            if (num6 > 9)
                                                num6 += num6 / 10;
                                            else if (flag)
                                            {
                                                ++num6;
                                                flag = !flag;
                                            }
                                            else
                                                flag = !flag;
                                            componentImprovement.Value1 = num6;
                                            break;
                                    }
                                    current._ImprovedComponent = componentImprovement;
                                }
                            }
                            break;
                        }
                }
            }
        }

        public static float WeaponRangeIncrementForDamageLoss(BuiltObject firingShip)
        {
            float num = 100f;
            if (BaconBuiltObject.IsMyShip(firingShip))
                num = 200f;
            return num;
        }

        public static float WeaponDamageDropoff(BuiltObject firingShip, Weapon weapon, float rawDamage)
        {
            if (weapon.Component.Type == ComponentType.WeaponMissile || weapon.Component.Type == ComponentType.WeaponBombard || weapon.Component.Type == ComponentType.WeaponSuperMissile || weapon.Component.Type == ComponentType.WeaponPointDefense)
                return rawDamage;
            float num = weapon.DistanceTravelled / (float)weapon.Range * rawDamage;
            if (firingShip.Empire.Name.Contains("Romulan"))
                num *= 0.5f;
            return Math.Min(Math.Max(0.0f, rawDamage - num), rawDamage);
        }

        public static Weapon CreateNewWeapon(object shipOrWeaponList, int i)
        {
            BuiltObject builtObject = (BuiltObject)null;
            Weapon weapon = (Weapon)null;
            switch (shipOrWeaponList)
            {
                case BuiltObject _:
                    builtObject = shipOrWeaponList as BuiltObject;
                    if (builtObject.Name.Contains("Royal"))
                        builtObject.CurrentEnergy = 300.0;
                    weapon = builtObject.Weapons[i];
                    break;
                case WeaponList _:
                    weapon = (shipOrWeaponList as WeaponList)[i];
                    break;
            }
            ComponentImprovement componentImprovement = new ComponentImprovement((Component)weapon.Component);
            if (builtObject != null && builtObject.Empire.Name.Contains("Romulan"))
            {
                componentImprovement.Value2 *= 3;
                componentImprovement.Value6 /= 3;
            }
            return new Weapon(componentImprovement)
            {
                LastFired = weapon.LastFired,
                X = weapon.X,
                Y = weapon.Y
            };
        }

        public static FighterSpecification BuildBetterFighters(
          BuiltObject carrier,
          FighterSpecification fighterDesign)
        {
            if (!carrier.Empire.Name.Contains("Romulan"))
                return fighterDesign;
            FighterSpecification fighterSpecification = BaconFighter.CloneFighterSpecification(fighterDesign);
            fighterSpecification.TopSpeed *= (short)2;
            fighterSpecification.TurnRate *= 2f;
            fighterSpecification.AccelerationRate *= 2f;
            fighterSpecification.WeaponSpeed *= (short)2;
            fighterSpecification.WeaponRange *= (short)2;
            return fighterSpecification;
        }

        public static void Temp(FighterSpecification fs, float costMult, float ammoMult) => new List<Tuple<FighterSpecification, float, float, short>>()
    {
      Tuple.Create<FighterSpecification, float, float, short>(fs, 1f, 1f, (short) 1)
    };

        public static int GetTotalFighterSizeOnCarrier(
          BuiltObject carrier,
          FighterType fighterType,
          bool includeUnderConstruction)
        {
            int fighterSizeOnCarrier = 0;
            for (int index = 0; index < carrier.Fighters.Count; ++index)
            {
                if (carrier.Fighters[index].Specification.Type == fighterType && (includeUnderConstruction || !carrier.Fighters[index].UnderConstruction))
                    fighterSizeOnCarrier += (int)carrier.Fighters[index].Specification.Size;
            }
            return fighterSizeOnCarrier;
        }

        public static int GetFighterCapacity(BuiltObject carrier, FighterType fighterType)
        {
            int fighterCapacity = 0;
            for (int index = 0; index < carrier.Components.Count; ++index)
            {
                Empire actualEmpire = carrier.ActualEmpire;
                ComponentImprovement componentImprovement = actualEmpire == null || actualEmpire.Research == null ? new ComponentImprovement((Component)carrier.Components[index]) : actualEmpire.Research.ResolveImprovedComponentValues((Component)carrier.Components[index]);
                if (componentImprovement.ImprovedComponent.Category == ComponentCategoryType.Fighter)
                {
                    if (componentImprovement.ImprovedComponent.Name.ToLower().Contains(BaconBuiltObject.fighterBayLabel) && fighterType == FighterType.Interceptor)
                        fighterCapacity += componentImprovement.Value1;
                    else if (componentImprovement.ImprovedComponent.Name.ToLower().Contains(BaconBuiltObject.bomberBayLabel) && fighterType == FighterType.Bomber)
                        fighterCapacity += componentImprovement.Value1;
                    else if (componentImprovement.ImprovedComponent.Name.ToLower().Contains(BaconBuiltObject.mixedBayLabel))
                        fighterCapacity += componentImprovement.Value1 / 2;
                }
            }
            return fighterCapacity;
        }

        public static void BuildNewFighters(BuiltObject carrier)
        {
            if (carrier.FighterCapacity <= 0)
                return;
            FighterSpecification fighterSpecification1 = carrier.Empire.Research.IdentifyLatestFighterSpecification();
            FighterSpecification bomberDesign = carrier.Empire.Research.IdentifyLatestBomberSpecification();
            FighterSpecification fighterSpecification2 = BaconBuiltObject.CheckForCustomGunship(carrier, bomberDesign);
            if (fighterSpecification1 == null && fighterSpecification2 == null)
            {
                if (carrier.Empire.PirateEmpireBaseHabitat == null)
                    return;
                fighterSpecification1 = Galaxy.FighterSpecificationsStatic[0];
                fighterSpecification2 = Galaxy.FighterSpecificationsStatic[5];
            }
            int num1 = 10;
            int num2 = 10;
            if (fighterSpecification1 != null)
                num1 = (int)fighterSpecification1.Size;
            if (fighterSpecification2 != null)
                num2 = (int)fighterSpecification2.Size;
            if (carrier.Fighters == null)
                carrier.Fighters = new FighterList();
            int num3 = 0;
            int num4 = 0;
            for (int index = 0; index < carrier.Fighters.Count; ++index)
            {
                if (carrier.Fighters[index].Specification.Type == FighterType.Bomber)
                    num4 += (int)carrier.Fighters[index].Specification.Size;
                else
                    num3 += (int)carrier.Fighters[index].Specification.Size;
            }
            int num5 = 0;
            int num6 = 0;
            for (int index = 0; index < carrier.Components.Count; ++index)
            {
                Empire actualEmpire = carrier.ActualEmpire;
                ComponentImprovement componentImprovement = actualEmpire == null || actualEmpire.Research == null ? new ComponentImprovement((Component)carrier.Components[index]) : actualEmpire.Research.ResolveImprovedComponentValues((Component)carrier.Components[index]);
                if (componentImprovement.ImprovedComponent.Category == ComponentCategoryType.Fighter)
                {
                    if (componentImprovement.ImprovedComponent.Name.ToLower().Contains(BaconBuiltObject.fighterBayLabel))
                        num5 += componentImprovement.Value1;
                    else if (componentImprovement.ImprovedComponent.Name.ToLower().Contains(BaconBuiltObject.bomberBayLabel))
                        num6 += componentImprovement.Value1;
                    else if (componentImprovement.ImprovedComponent.Name.ToLower().Contains(BaconBuiltObject.mixedBayLabel))
                    {
                        num5 += componentImprovement.Value1 / 2;
                        num6 += componentImprovement.Value1 / 2;
                    }
                }
            }
            if (BaconBuiltObject.myMain != null && BaconBuiltObject.myMain._Game != null && carrier.Empire != BaconBuiltObject.myMain._Game.PlayerEmpire)
            {
                num5 /= 2;
                num6 = num5;
            }
            int num7 = (num5 - num3) / num1;
            int num8 = (num6 - num4) / num2;
            if (fighterSpecification1 != null)
            {
                for (int index = 0; index < num7; ++index)
                    BaconBuiltObject.BuildFighter(carrier, fighterSpecification1);
            }
            if (fighterSpecification2 != null)
            {
                for (int index = 0; index < num8; ++index)
                    BaconBuiltObject.BuildFighter(carrier, fighterSpecification2);
            }
        }

        public static void BuildFighter(BuiltObject carrier, FighterSpecification fighterSpecification)
        {
            if (fighterSpecification == null)
                return;
            fighterSpecification = BaconBuiltObject.BuildBetterFighters(carrier, fighterSpecification);
            if (carrier.FighterCapacity >= (int)fighterSpecification.Size)
            {
                if (carrier.Fighters == null)
                    carrier.Fighters = new FighterList();
                if (carrier.FighterCapacity - carrier.Fighters.TotalSize >= (int)fighterSpecification.Size)
                {
                    Fighter fighter = new Fighter(carrier._Galaxy, fighterSpecification, carrier);
                    if (carrier.ActualEmpire.Research.LatestProjects.FirstOrDefault<ResearchNode>((Func<ResearchNode, bool>)(x => x.Name == BaconBuiltObject.tailGunnerResearch && x.IsResearched)) != null)
                    {
                        for (int index = 0; index < fighter.Size / 10; ++index)
                            BaconFighter.AddWeaponToFighter(fighter, "defensiveGun");
                    }
                }
            }
        }

        public static void ManufactureRepairFighters(BuiltObject carrier, double timePassed)
        {
            if (carrier.Fighters == null || carrier.FighterCapacity <= 0 || carrier.FighterRepairRate <= 0)
                return;
            float num1 = (float)(timePassed * (double)carrier.FighterRepairRate * 0.01);
            if (BaconBuiltObject.IsMyShip(carrier))
                num1 *= 2f;
            if (carrier.Role == BuiltObjectRole.Base && BaconBuiltObject.limitNewFighterBuildToColonies)
                num1 *= 2f;
            bool flag = true;
            if (BaconBuiltObject.limitNewFighterBuildToColonies && BaconBuiltObject.myMain != null && carrier.Role != BuiltObjectRole.Base && carrier.ActualEmpire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                flag = BaconBuiltObject.IsShipInSystemWithFriendlyColony(carrier);
            for (int index = 0; index < carrier.Fighters.Count; ++index)
            {
                Fighter fighter = carrier.Fighters[index];
                if (fighter.OnboardCarrier)
                {
                    if (fighter.UnderConstruction)
                    {
                        if (flag)
                        {
                            if ((double)fighter.Health < 1.0)
                            {
                                float num2 = num1 / BaconFighter.fighterBuildSpeedDivisor * 10f / (float)Math.Max(1, fighter.Size);
                                float num3;
                                if (1.0 - (double)fighter.Health <= (double)num2)
                                {
                                    num3 = num2 - (1f - fighter.Health);
                                    fighter.Health = 1f;
                                    fighter.UnderConstruction = false;
                                    BaconFighter.PayWhenFighterIsBuilt(fighter);
                                }
                                else
                                {
                                    fighter.Health += num2;
                                    num3 = 0.0f;
                                }
                                num1 = (float)((double)num3 * (double)fighter.Size / 10.0) * BaconFighter.fighterBuildSpeedDivisor;
                            }
                            else
                                fighter.UnderConstruction = false;
                        }
                    }
                    else if ((double)fighter.Health < 1.0)
                    {
                        if (1.0 - (double)fighter.Health <= (double)num1)
                        {
                            num1 -= 1f - fighter.Health;
                            fighter.Health = 1f;
                        }
                        else
                        {
                            fighter.Health += num1;
                            num1 = 0.0f;
                        }
                    }
                }
                if ((double)num1 <= 0.0)
                    break;
            }
        }

        public static bool IsShipInSystemWithFriendlyColony(BuiltObject ship)
        {
            if (ship.NearestSystemStar == null || ship.ActualEmpire.Colonies == null)
                return false;
            Habitat nearestSystemStar = ship.NearestSystemStar;
            foreach (Habitat colony in (SyncList<Habitat>)ship.ActualEmpire.Colonies)
            {
                Habitat habitat = colony;
                while (habitat.Parent != null)
                    habitat = habitat.Parent;
                if (habitat != null && nearestSystemStar.Name == habitat.Name)
                    return true;
            }
            return false;
        }

        public static FighterSpecification CheckForCustomGunship(
          BuiltObject carrier,
          FighterSpecification bomberDesign)
        {
            if (carrier.BaconValues == null || !carrier.BaconValues.ContainsKey("customBomberName"))
                return bomberDesign;
            string customBomberName = (string)carrier.BaconValues["customBomberName"];
            List<Tuple<FighterSpecification, float, float, short>> tupleList = new List<Tuple<FighterSpecification, float, float, short>>();
            if (carrier.ActualEmpire == null)
                return bomberDesign;
            Tuple<FighterSpecification, float, float, short> tuple = BaconBuiltObject.GetCustomFighterDesigns(carrier.ActualEmpire).FirstOrDefault<Tuple<FighterSpecification, float, float, short>>((Func<Tuple<FighterSpecification, float, float, short>, bool>)(x => x.Item1.Name == customBomberName));
            if (tuple == null)
                return bomberDesign;
            FighterSpecification fighterSpecification = tuple.Item1;
            fighterSpecification.SortTag = (float)tuple.Item4;
            return fighterSpecification;
        }

        public static List<Tuple<FighterSpecification, float, float, short>> GetCustomFighterDesigns(
          Empire empire)
        {
            List<Tuple<FighterSpecification, float, float, short>> customFighterDesigns = new List<Tuple<FighterSpecification, float, float, short>>();
            if (empire.PirateEmpireBaseHabitat == null)
            {
                if (empire.Capital != null && empire.Capital.BaconValues != null && empire.Capital.BaconValues.ContainsKey("customBomberDesigns"))
                    customFighterDesigns = (List<Tuple<FighterSpecification, float, float, short>>)empire.Capital.BaconValues["customBomberDesigns"];
            }
            else if (empire.BuiltObjects != null && empire.BuiltObjects.Count > 0 && empire.BuiltObjects[0].BaconValues != null && empire.BuiltObjects[0].BaconValues.ContainsKey("customBomberDesigns"))
                customFighterDesigns = (List<Tuple<FighterSpecification, float, float, short>>)empire.BuiltObjects[0].BaconValues["customBomberDesigns"];
            return customFighterDesigns;
        }

        public static bool AddCustomFighterDesign(
          Tuple<FighterSpecification, float, float, short> newDesign,
          Empire empire)
        {
            if (empire.PirateEmpireBaseHabitat == null)
            {
                if (empire.Capital != null)
                {
                    if (empire.Capital.BaconValues == null)
                        empire.Capital.BaconValues = new Dictionary<string, object>();
                    if (empire.Capital.BaconValues.ContainsKey("customBomberDesigns"))
                    {
                        List<Tuple<FighterSpecification, float, float, short>> baconValue = (List<Tuple<FighterSpecification, float, float, short>>)empire.Capital.BaconValues["customBomberDesigns"];
                        if (!baconValue.Contains(newDesign))
                        {
                            baconValue.Add(newDesign);
                            empire.Capital.BaconValues["customBomberDesigns"] = (object)baconValue;
                            return true;
                        }
                    }
                    else
                    {
                        empire.Capital.BaconValues.Add("customBomberDesigns", (object)new List<Tuple<FighterSpecification, float, float, short>>()
            {
              newDesign
            });
                        return true;
                    }
                }
            }
            else if (empire.BuiltObjects != null && empire.BuiltObjects.Count > 0)
            {
                if (empire.BuiltObjects[0].BaconValues == null)
                    empire.BuiltObjects[0].BaconValues = new Dictionary<string, object>();
                if (empire.BuiltObjects[0].BaconValues.ContainsKey("customBomberDesigns"))
                {
                    List<Tuple<FighterSpecification, float, float, short>> baconValue = (List<Tuple<FighterSpecification, float, float, short>>)empire.BuiltObjects[0].BaconValues["customBomberDesigns"];
                    if (!baconValue.Contains(newDesign))
                    {
                        baconValue.Add(newDesign);
                        empire.BuiltObjects[0].BaconValues["customBomberDesigns"] = (object)baconValue;
                        return true;
                    }
                }
                else
                {
                    empire.BuiltObjects[0].BaconValues.Add("customBomberDesigns", (object)new List<Tuple<FighterSpecification, float, float, short>>()
          {
            newDesign
          });
                    return true;
                }
            }
            return false;
        }

        public static void DeserializeExtraFields(BuiltObject ship, SerializationInfo info)
        {
            try
            {
                ship.CareerBattleStats = (SpaceBattleStats)info.GetValue("Cbs", typeof(SpaceBattleStats));
                ship.BaconValues = (Dictionary<string, object>)info.GetValue("baconShipSettings", typeof(Dictionary<string, object>));
            }
            catch (Exception ex)
            {
                if (!(ex.GetType() == typeof(SerializationException)))
                    ;
            }
        }

        public static void SerializeExtraFields(BuiltObject ship, SerializationInfo info)
        {
            try
            {
                if (ship.BattleStats != null)
                {
                    BaconSpaceBattleStats.AddLatestCombatStats(ship, ship.BattleStats);
                    ship.BattleStats = new SpaceBattleStats();
                }
                info.AddValue("Cbs", (object)ship.CareerBattleStats);
                info.AddValue("baconShipSettings", (object)ship.BaconValues);
            }
            catch (Exception ex)
            {
            }
        }

        public static void DestroyAllShips(Main main, Func<BuiltObject, bool> deleteCondition = null)
        {
            if (deleteCondition == null)
                deleteCondition = (Func<BuiltObject, bool>)(BuiltObject => BuiltObject.Owner == null && BuiltObject.ActualEmpire == null);
            BuiltObjectList builtObjects = main._Game.Galaxy.BuiltObjects;
            BuiltObjectList builtObjectList1 = new BuiltObjectList();
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            try
            {
                foreach (BuiltObject builtObject in (SyncList<BuiltObject>)builtObjects)
                {
                    if (builtObject != null)
                    {
                        if (!deleteCondition(builtObject))
                        {
                            if (!builtObjectList2.Contains(builtObject))
                                builtObjectList2.Add(builtObject);
                        }
                        else if (builtObject.Characters.Any<Character>())
                        {
                            if (!builtObjectList2.Contains(builtObject))
                                builtObjectList2.Add(builtObject);
                        }
                        else
                        {
                            builtObject.Characters = new CharacterList();
                            builtObject.CompleteTeardown(main._Game.Galaxy);
                        }
                    }
                }
                main._Game.Galaxy.BuiltObjects = builtObjectList2;
            }
            catch (Exception ex)
            {
            }
        }

        public static void DestroyAllShips(Main main, bool keepBases)
        {
            BuiltObjectList builtObjects = main._Game.Galaxy.BuiltObjects;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            try
            {
                foreach (BuiltObject builtObject in (SyncList<BuiltObject>)builtObjects)
                {
                    if (builtObject != null)
                    {
                        if (builtObject.Characters.Any<Character>() && !builtObjectList.Contains(builtObject))
                            builtObjectList.Add(builtObject);
                        if (keepBases && builtObject.Role == BuiltObjectRole.Base && builtObject.ParentHabitat?.Name == builtObject.Empire?.Capital?.Name)
                        {
                            if (!builtObjectList.Contains(builtObject))
                                builtObjectList.Add(builtObject);
                        }
                        else if (keepBases && builtObject.Role == BuiltObjectRole.Base && builtObject.ParentHabitat?.Name == builtObject.Empire?.PirateEmpireBaseHabitat?.Name)
                        {
                            if (!builtObjectList.Contains(builtObject))
                                builtObjectList.Add(builtObject);
                        }
                        else
                        {
                            builtObject.Characters = new CharacterList();
                            builtObject.CompleteTeardown(main._Game.Galaxy);
                        }
                    }
                }
                main._Game.Galaxy.BuiltObjects = builtObjectList;
            }
            catch (Exception ex)
            {
            }
        }

        public static int CalculateOverallStrengthFactorWithoutShields(BuiltObject ship) => ship.Role != BuiltObjectRole.Military ? 0 : ship.CalculateFirepowerFactor() + ship.CalculateFighterFactor();

        public static void ApplyStartingCheatSettings(Main main)
        {
            Empire playerEmpire = main._Game.PlayerEmpire;
            playerEmpire.ShipMaintenancePrivateFactor = 0.05;
            playerEmpire.ShipMaintenanceStateFactor = 0.05;
            playerEmpire.TargettingFactor = Math.Max(1.0, playerEmpire.TargettingFactor);
            playerEmpire.CountermeasuresFactor = Math.Max(1.0, playerEmpire.CountermeasuresFactor);
            playerEmpire.TroopMaintenanceFactor = 0.05f;
            if (playerEmpire.PirateEmpireBaseHabitat == null)
                return;
            playerEmpire.RaidBonusFactor *= 10.0;
            playerEmpire.SmugglingIncomeFactor *= 10.0;
            playerEmpire.LootingFactor *= 10.0;
        }

        public static bool AssignMissionCheckPreconditions(BuiltObject ship)
        {
            bool flag = true;
            if (ship.BaconValues != null && ship.BaconValues.ContainsKey("sleep"))
                flag = false;
            return flag;
        }

        public static void AddScientificData(BuiltObject ship, Habitat planet, string eventType)
        {
            if (ship.SubRole != BuiltObjectSubRole.ExplorationShip || eventType == "scanArea" && planet.Resources == null || planet.Resources != null && planet.Resources.Count == 0 || !ship.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsWeaponsLab)) && !ship.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsEnergyLab)) && !ship.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsHighTechLab)))
                return;
            if (ship.BaconValues == null)
                ship.BaconValues = new Dictionary<string, object>();
            if (!ship.BaconValues.ContainsKey("scientificData"))
                ship.BaconValues.Add("scientificData", (object)0);
            int baconValue = (int)ship.BaconValues["scientificData"];
            int num;
            switch (eventType)
            {
                case "scanArea":
                    ship.BaconValues["scientificData"] = (object)(planet.Resources.Count * BaconBuiltObject.scientificDataForResourceSurvey + baconValue);
                    return;
                case "scientificData":
                    if (planet.Ruin != null)
                    {
                        num = !planet.Ruin.PlayerEmpireEncountered ? 1 : 0;
                        break;
                    }
                    goto default;
                default:
                    num = 0;
                    break;
            }
            if (num == 0)
                return;
            ship.BaconValues["scientificData"] = (object)(BaconBuiltObject.scientificDataForRuins + baconValue);
        }

        public static void CommandActionExtractResources(BuiltObject ship)
        {
            if (!ship.FirstExecutionOfCommand)
                return;
            BaconHabitat.CheckIfShouldBuildAsteroidColony(ship.Mission);
            bool flag = false;
            if (ship.BaconValues != null)
            {
                if (ship.BaconValues.ContainsKey("cash") && !ship.IsAutoControlled)
                    flag = true;
                if (ship.BaconValues.ContainsKey("ShipNote") && ((string)ship.BaconValues["ShipNote"]).StartsWith("nodump"))
                    flag = true;
            }
            if (flag)
            {
                Command command = ship.Mission.FastPeekCurrentCommand().Clone();
                CommandQueue commands = new CommandQueue();
                commands.Enqueue(command);
                ship.Mission.ReplaceCommandStack(commands);
            }
        }

        public static void RecruitShipOfficer(Main main, BuiltObject ship, Habitat location)
        {
            main._Game.Galaxy.PlayerEmpire.GenerateNewCharacter(CharacterRole.ShipCaptain, (StellarObject)ship);
            int num = (int)ship.BaconValues["cash"] - BaconMain.baseShipOfficerCost;
            ship.BaconValues["cash"] = (object)num;
        }

        public static void AssignMiningShipToTarget(Main main)
        {
            Habitat planet = main._Game.SelectedObject as Habitat;
            if (planet == null)
            { return; }
            else
            {
                BuiltObjectSubRole shipTypeToSelect = BuiltObjectSubRole.MiningShip;
                if (planet.Type == HabitatType.GasGiant || planet.Type == HabitatType.FrozenGasGiant)
                    shipTypeToSelect = BuiltObjectSubRole.GasMiningShip;
                List<BuiltObject> source = new List<BuiltObject>();
                List<BuiltObject> list1 = new List<BuiltObject>();
                if (BaconBuiltObject.AllowPrivateShipAssigment)
                {
                    list1 = main._Game.Galaxy.PlayerEmpire.PrivateBuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x =>
                {
                    if (x.SubRole != shipTypeToSelect)
                        return false;
                    return x.Mission == null || x.Mission.Type == BuiltObjectMissionType.Undefined;
                })).ToList<BuiltObject>();
                }
                List<BuiltObject> list2 = main._Game.Galaxy.PlayerEmpire.BuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x =>
                {
                    if (x.SubRole != shipTypeToSelect)
                        return false;
                    return x.Mission == null || x.Mission.Type == BuiltObjectMissionType.Undefined;
                })).ToList<BuiltObject>();
                if (list1.Any<BuiltObject>())
                    source.AddRange((IEnumerable<BuiltObject>)list1);
                if (list2.Any<BuiltObject>())
                    source.AddRange((IEnumerable<BuiltObject>)list2);
                if (source.Count == 0)
                {
                    MessageBoxEx messageBoxEx = MessageBoxExManager.CreateMessageBox(null, BaconBuiltObject.myMain.font_3);
                    messageBoxEx.Text = "No idle ships found";
                    messageBoxEx.Caption = "Warning";
                    messageBoxEx.AddButton(MessageBoxExButtons.Ok);
                    messageBoxEx.Icon = MessageBoxExIcon.Warning;
                    messageBoxEx.Show(BaconBuiltObject.myMain);
                    return;
                }
                BuiltObject builtObject1 = source.OrderBy<BuiltObject, double>((Func<BuiltObject, double>)(x => Galaxy.CalculateDistanceSquaredStatic(x.Xpos, x.Ypos, planet.Xpos, planet.Ypos))).ToList<BuiltObject>().FirstOrDefault<BuiltObject>();
                if (builtObject1 == null)
                    return;
                BuiltObject builtObject2 = (BuiltObject)null;
                bool deliverToSpecificDestination = false;
                bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                if (!flag)
                { main._Game.Galaxy.Pause(); }

                using SelectMiningDeliveryTarget selectForm = new SelectMiningDeliveryTarget(main);
                if (selectForm.ShowDialog(main) == DialogResult.OK)
                {
                    deliverToSpecificDestination = selectForm.DeliverToSpecificDestination;
                }
                if (!flag)
                { main._Game.Galaxy.Resume(); }
                if (selectForm.DialogResult != DialogResult.OK)
                { return; }

                if (deliverToSpecificDestination)
                {
                    builtObject2 = selectForm.SelectedTarget;
                }
                else
                {
                    List<BuiltObject> list3 = main._Game.Galaxy.PlayerEmpire.BuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x =>
                    {
                        if (x.Role != BuiltObjectRole.Base)
                            return false;
                        return x.SubRole == BuiltObjectSubRole.LargeSpacePort || x.SubRole == BuiltObjectSubRole.MediumSpacePort || x.SubRole == BuiltObjectSubRole.SmallSpacePort || x.SubRole == BuiltObjectSubRole.GenericBase;
                    })).ToList<BuiltObject>();
                    if (list3.Any<BuiltObject>())
                    {
                        List<BuiltObject> list4 = list3.OrderBy<BuiltObject, double>((Func<BuiltObject, double>)(x => Galaxy.CalculateDistanceSquaredStatic(x.Xpos, x.Ypos, planet.Xpos, planet.Ypos))).ToList<BuiltObject>();
                        if (list4.Any<BuiltObject>())
                            builtObject2 = list4.FirstOrDefault<BuiltObject>();
                    }
                }
                if (builtObject2 != null)
                {
                    builtObject1.AssignMission(BuiltObjectMissionType.ExtractResources, (object)planet, (object)builtObject2, BuiltObjectMissionPriority.Normal);
                    CommandQueue commands = new CommandQueue();
                    commands.Enqueue(new Command(CommandAction.ClearParent));
                    commands.Enqueue(new Command(CommandAction.ConditionalHyperTo, builtObject1.Mission.TargetHabitat));
                    commands.Enqueue(new Command(CommandAction.SetParent, builtObject1.Mission.TargetHabitat));
                    commands.Enqueue(new Command(CommandAction.MoveTo, builtObject1.Mission.TargetHabitat));
                    commands.Enqueue(new Command(CommandAction.ImpulseTo, builtObject1.Mission.TargetHabitat));
                    commands.Enqueue(new Command(CommandAction.ExtractResources, builtObject1.Mission.TargetHabitat));
                    commands.Enqueue(new Command(CommandAction.ClearParent));
                    commands.Enqueue(new Command(CommandAction.ConditionalHyperTo, builtObject2));
                    commands.Enqueue(new Command(CommandAction.SetParent, builtObject2));
                    commands.Enqueue(new Command(CommandAction.MoveTo, builtObject2));
                    commands.Enqueue(new Command(CommandAction.Dock, builtObject2));
                    commands.Enqueue(new Command(CommandAction.Unload));
                    commands.Enqueue(new Command(CommandAction.Refuel));
                    commands.Enqueue(new Command(CommandAction.Undock, builtObject2));
                    commands.Enqueue(new Command(CommandAction.SetParent, builtObject2));
                    commands.Enqueue(builtObject1.Mission.GenerateParkCommand());
                    builtObject1.Mission.ReplaceCommandStack(commands);
                }
                main._Game.SelectedObject = (object)builtObject1;
                if (main._Game.SelectedObject == null)
                    return;
                main.method_208(main._Game.SelectedObject);
            }
        }

        public static double GetGravityWellReductionForSmallShip(BuiltObject ship)
        {
            if (!BaconBuiltObject.smallShipsJumpSooner)
                return 1.0;
            double? nullable1 = new double?(1.0);
            bool flag = ship.ShipGroup != null;
            Empire empire = ship.Empire == null || !(ship.Empire.Name != "Independent") ? ship.ActualEmpire : ship.Empire;
            int size = ship.Size;
            double? nullable2;
            double? nullable3;
            if (ship.Role == BuiltObjectRole.Military || ship.Role == BuiltObjectRole.Exploration)
            {
                nullable2 = empire?.DominantRace?.MilitaryShipSizeFactor;
                double num = BaconRace.MilitaryShipSizeMultiplier(empire);
                nullable3 = nullable2.HasValue ? new double?(nullable2.GetValueOrDefault() * num) : new double?();
            }
            else
            {
                nullable2 = empire?.DominantRace?.CivilianShipSizeFactor;
                double num = BaconRace.CivilianShipSizeMultiplier(empire);
                nullable3 = nullable2.HasValue ? new double?(nullable2.GetValueOrDefault() * num) : new double?();
            }
            nullable2 = nullable3 ?? new double?(1.0);
            double num1 = nullable2.Value;
            int? nullable4 = empire?.MaximumConstructionSize();
            double? nullable5;
            if (!nullable4.HasValue)
            {
                nullable2 = new double?();
                nullable5 = nullable2;
            }
            else
                nullable5 = new double?((double)nullable4.GetValueOrDefault());
            double? nullable6 = nullable5;
            double num2 = nullable6.HasValue ? nullable6.Value : 230.0;
            if (flag)
                size = ship.ShipGroup.Ships.OrderBy<BuiltObject, int>((Func<BuiltObject, int>)(x => x.Size)).Last<BuiltObject>().Size;
            return (double)size / (num2 * num1);
        }

        public static bool ShouldSendShipTowardEdgeOfGravityWell(BuiltObject ship) => ship != null && ship != null && !BaconBuiltObject.IsOutsideStarGravityWell(ship) && ship.Mission != null && BaconBuiltObject.FindRangeSquaredToTarget(ship) >= BaconBuiltObject.starGravityWellRangeSquared * 1.3999999761581421;

        public static bool SendShipTowardsEdgeOfGravityWell(BuiltObject ship)
        {
            bool flag = false;
            try
            {
                BaconBuiltObject.CheckIfShouldUseStargateAndReassign(ship);
                if (BaconBuiltObject.ShouldSendShipTowardEdgeOfGravityWell(ship))
                {
                    Habitat nearestSystemStar = ship.NearestSystemStar;
                    double reductionForSmallShip = BaconBuiltObject.GetGravityWellReductionForSmallShip(ship);
                    int num1 = (int)nearestSystemStar.SolarRadiation + (int)nearestSystemStar.MicrowaveRadiation + (int)nearestSystemStar.XrayRadiation;
                    double mitigationForHyperDrive = BaconBuiltObject.GetGravityWellMitigationForHyperDrive(ship);
                    double num2 = Math.Sqrt(BaconBuiltObject.starGravityWellRangeSquared * ((double)num1 / 100.0) * ((double)num1 / 100.0) * reductionForSmallShip * reductionForSmallShip * mitigationForHyperDrive * mitigationForHyperDrive);
                    double num3 = Math.Max(num2 * 1.03, num2 + 1000.0);
                    double angle = Galaxy.DetermineAngle(ship.Xpos, ship.Ypos, nearestSystemStar.Xpos, nearestSystemStar.Ypos);
                    double x = nearestSystemStar.Xpos + num3 * Math.Cos(angle + Math.PI);
                    double y = nearestSystemStar.Ypos + num3 * Math.Sin(angle + Math.PI);
                    BuiltObjectMission mission = ship.Mission;
                    if (ship.Mission != null)
                        ship.Mission.InsertCommandAtTop(new Command(CommandAction.MoveTo, x, y));
                    flag = true;
                }
            }
            catch (Exception ex)
            {
            }
            return flag;
        }

        public static double FindRangeSquaredToTarget(BuiltObject ship)
        {
            double x2 = 0.0;
            double y2 = 0.0;
            if (ship.Mission != null && ship.Mission.FastPeekCurrentCommand() != null)
            {
                if ((double)ship.Mission.X > -1900010000.0 && (double)ship.Mission.Y > -1900010000.0 && ship.Mission.Type == BuiltObjectMissionType.Move && ship.Mission.Target == null)
                {
                    x2 = (double)ship.Mission.X;
                    y2 = (double)ship.Mission.Y;
                }
                else if ((double)ship.Mission.FastPeekCurrentCommand().Xpos > -1900010000.0 && (double)ship.Mission.FastPeekCurrentCommand().Ypos > -1900010000.0)
                {
                    x2 = (double)ship.Mission.FastPeekCurrentCommand().Xpos;
                    y2 = (double)ship.Mission.FastPeekCurrentCommand().Ypos;
                }
                else if (ship.Mission.TargetBuiltObject != null)
                {
                    x2 = ship.Mission.TargetBuiltObject.Xpos;
                    y2 = ship.Mission.TargetBuiltObject.Ypos;
                }
                else if (ship.Mission.TargetHabitat != null)
                {
                    x2 = ship.Mission.TargetHabitat.Xpos;
                    y2 = ship.Mission.TargetHabitat.Ypos;
                }
                else if (ship.Mission.TargetSector != null)
                {
                    x2 = (double)ship.Mission.TargetSector.X;
                    y2 = (double)ship.Mission.TargetSector.Y;
                }
                else if (ship.Mission.Target != null)
                {
                    x2 = (double)ship.Mission.X;
                    y2 = (double)ship.Mission.Y;
                }
                else if (ship.Mission.TargetCreature != null)
                {
                    x2 = ship.Mission.TargetCreature.Xpos;
                    y2 = ship.Mission.TargetCreature.Ypos;
                }
                else if (ship.Mission.TargetSector != null)
                {
                    x2 = (double)ship.Mission.TargetSector.X;
                    y2 = (double)ship.Mission.TargetSector.Y;
                }
                else if (ship.Mission.SecondaryTargetBuiltObject != null)
                {
                    x2 = ship.Mission.SecondaryTargetBuiltObject.Xpos;
                    y2 = ship.Mission.SecondaryTargetBuiltObject.Ypos;
                }
                else if (ship.Mission.SecondaryTargetHabitat != null)
                {
                    x2 = ship.Mission.SecondaryTargetHabitat.Xpos;
                    y2 = ship.Mission.SecondaryTargetHabitat.Ypos;
                }
                else if (ship.Mission.SecondaryTarget != null)
                {
                    if (ship.Mission.SecondaryTarget is BuiltObject)
                    {
                        x2 = (ship.Mission.SecondaryTarget as BuiltObject).Xpos;
                        y2 = (ship.Mission.SecondaryTarget as BuiltObject).Ypos;
                    }
                    else if (ship.Mission.SecondaryTarget is Habitat)
                    {
                        x2 = (ship.Mission.SecondaryTarget as Habitat).Xpos;
                        y2 = (ship.Mission.SecondaryTarget as Habitat).Ypos;
                    }
                    else if (ship.Mission.SecondaryTarget is Creature)
                    {
                        x2 = (ship.Mission.SecondaryTarget as Creature).Xpos;
                        y2 = (ship.Mission.SecondaryTarget as Creature).Ypos;
                    }
                }
                else if (ship.Mission.SecondaryTargetCreature != null)
                {
                    x2 = ship.Mission.SecondaryTargetCreature.Xpos;
                    y2 = ship.Mission.SecondaryTargetCreature.Ypos;
                }
                else
                    ++BaconBuiltObject.debugCounter;
            }
            return Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, x2, y2);
        }

        public static int FindResalePriceOfShip(BuiltObject ship, Empire buyer)
        {
            double resalePriceOfShip = -1.0;
            if (BaconBuiltObject.myMain == null || ship.Components.Any<BuiltObjectComponent>((Func<BuiltObjectComponent, bool>)(x => x.Status == ComponentStatus.Unbuilt)))
                return (int)resalePriceOfShip;
            double num1 = ship.Design.CalculateCurrentPurchasePrice(BaconBuiltObject.myMain._Game.Galaxy) * (1.0 + ship.ActualEmpire.TradeBonus - buyer.TradeBonus);
            if (!buyer.CanBuildDesignTech(ship.Design))
                num1 *= 2.0;
            switch (BaconBuiltObject.CalculateCrewLevel((Main)null, ship))
            {
                case "green":
                    num1 *= 0.9;
                    break;
                case "experienced":
                    num1 *= 1.1;
                    break;
                case "veteran":
                    num1 *= 1.2;
                    break;
                case "elite":
                    num1 *= 1.3;
                    break;
                case "legendary":
                    num1 *= 1.4;
                    break;
            }
            if (ship.Role == BuiltObjectRole.Military && buyer.CheckAtWar())
                num1 *= 1.2;
            if (ship.Cargo != null)
            {
                double num2 = Math.Max(0.02, (double)(1L - ship.ActualEmpire.TotalPopulation / 1000000000L));
                num1 += BaconBuiltObject.CalculateValueOfCargoForEmpire(ship.Cargo, ship.ActualEmpire) * num2;
            }
            double num3 = ship.CurrentFuel / (double)Math.Max(1, ship.FuelCapacity);
            if (num3 < 0.25)
                num1 *= 0.5;
            else if (num3 > 0.9)
                num1 *= 1.1;
            double num4 = 0.0;
            foreach (EmpireRelationshipFactor relationshipFactor in (SyncList<EmpireRelationshipFactor>)ship.Empire.DetermineEmpireRelationshipFactors(buyer))
                num4 += relationshipFactor.Value;
            return (int)(num1 * Math.Max(0.0, 1.0 + num4 / 100.0));
        }

        public static double CalculateValueOfCargoForEmpire(CargoList cargo, Empire owningEmpire)
        {
            if (BaconBuiltObject.myMain == null)
                return 0.0;
            double ofCargoForEmpire = 0.0;
            foreach (Cargo cargo1 in (SyncList<Cargo>)cargo)
            {
                if (cargo1.EmpireId == owningEmpire.EmpireId)
                {
                    double num = BaconBuiltObject.myMain._Game.Galaxy.ResourceCurrentPrices[(int)cargo1.Resource.ResourceID] * (double)(cargo1.Amount - cargo1.Reserved);
                    ofCargoForEmpire += num;
                }
            }
            return ofCargoForEmpire;
        }

        public static string PauseAndShowYesNoMessageBox(Main main, string message, string caption)
        {
            bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
            if (!flag)
                main._Game.Galaxy.Pause();
            MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string)null, new Font("Verdana", 9f, FontStyle.Regular));
            messageBox.Text = message;
            messageBox.Caption = caption;
            messageBox.AddButton(MessageBoxExButtons.Yes);
            messageBox.AddButton(MessageBoxExButtons.No);
            messageBox.Icon = MessageBoxExIcon.None;
            string str = messageBox.Show();
            if (!flag)
                main._Game.Galaxy.Resume();
            return str;
        }

        public static string PauseAndShowMessageBox(Main main, string message, string caption)
        {
            bool flag = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
            if (!flag)
                main._Game.Galaxy.Pause();
            MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string)null, new Font("Verdana", 9f, FontStyle.Regular));
            messageBox.Text = message;
            messageBox.Caption = caption;
            messageBox.Icon = MessageBoxExIcon.None;
            string str = messageBox.Show();
            if (!flag)
                main._Game.Galaxy.Resume();
            return str;
        }

        public static void AddReminderNote(object shipObject, string message)
        {
            if (!(shipObject is BuiltObject) || (shipObject as BuiltObject).ActualEmpire != BaconBuiltObject.myMain._Game.PlayerEmpire)
                return;
            BuiltObject builtObject = (BuiltObject)shipObject;
            if (builtObject.Mission == null || builtObject.Mission.FastPeekCurrentCommand() == null)
                return;
            if (builtObject.BaconValues == null)
                builtObject.BaconValues = new Dictionary<string, object>();
            if (builtObject.BaconValues.ContainsKey("ShipNote"))
            {
                if (string.IsNullOrEmpty(message))
                {
                    builtObject.BaconValues.Remove("ShipNote");
                    if (builtObject.BaconValues.Count == 0)
                        builtObject.BaconValues = (Dictionary<string, object>)null;
                }
                else
                    builtObject.BaconValues["ShipNote"] = (object)message.Trim();
            }
            else
                builtObject.BaconValues.Add("ShipNote", (object)message.Trim());
        }

        public static bool CheckAndAssignRepeatingMission(BuiltObject ship)
        {
            if (ship.BaconValues == null)
                return false;
            bool flag = false;
            if (ship.BaconValues.ContainsKey("ShipNote"))
            {
                string message = (string)ship.BaconValues["ShipNote"] + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Do you wish to select this ship?";
                if (BaconBuiltObject.PauseAndShowYesNoMessageBox(BaconBuiltObject.myMain, message, ship.Name + " reminder note") == "Yes")
                {
                    BaconBuiltObject.myMain._Game.SelectedObject = (object)ship;
                    if (BaconBuiltObject.myMain._Game.SelectedObject != null)
                        BaconBuiltObject.myMain.method_208(BaconBuiltObject.myMain._Game.SelectedObject);
                }
                ship.BaconValues.Remove("ShipNote");
            }
            if (ship.BaconValues.ContainsKey("RepeatingMission"))
            {
                BuiltObjectMission baconValue = (BuiltObjectMission)ship.BaconValues["RepeatingMission"];
                CargoList cargo = baconValue.Cargo;
                if (baconValue.Population != null)
                    BaconBuiltObject.AssignPassengershipMission(BaconBuiltObject.myMain, (object)ship, true);
                else if (cargo != null)
                    BaconBuiltObject.AssignCargoMission(BaconBuiltObject.myMain, ship, true);
                flag = true;
            }
            if (ship.BaconValues.Count == 0)
                ship.BaconValues = (Dictionary<string, object>)null;
            return flag;
        }

        public static bool AssignQueuedMission(BuiltObject ship, bool allowReprocessing)
        {
            if (ship._SubsequentMissions == null || ship._SubsequentMissions.Count <= 0)
                return BaconBuiltObject.CheckAndAssignRepeatingMission(ship);
            BuiltObjectMission subsequentMission = ship._SubsequentMissions[0];
            if (subsequentMission.Type == BuiltObjectMissionType.Blockade)
            {
                if (subsequentMission.TargetBuiltObject != null)
                {
                    if (!subsequentMission.ManuallyAssigned ? ship.WithinFuelRangeAndRefuel(subsequentMission.TargetBuiltObject.Xpos, subsequentMission.TargetBuiltObject.Ypos, 0.0) : ship.WithinFuelRange(subsequentMission.TargetBuiltObject.Xpos, subsequentMission.TargetBuiltObject.Ypos, 0.0))
                    {
                        ship.Empire.ImplementBlockade(subsequentMission.TargetBuiltObject, false, false);
                        ship.ClearPreviousMissionRequirements();
                        ship.AssignMission(BuiltObjectMissionType.Blockade, (object)subsequentMission.TargetBuiltObject, (object)null, BuiltObjectMissionPriority.Normal);
                    }
                }
                else if (subsequentMission.TargetHabitat != null)
                {
                    if (!subsequentMission.ManuallyAssigned ? ship.WithinFuelRangeAndRefuel(subsequentMission.TargetHabitat.Xpos, subsequentMission.TargetHabitat.Ypos, 0.0) : ship.WithinFuelRange(subsequentMission.TargetHabitat.Xpos, subsequentMission.TargetHabitat.Ypos, 0.0))
                    {
                        ship.Empire.ImplementBlockade(subsequentMission.TargetHabitat, false, false);
                        ship.ClearPreviousMissionRequirements();
                        ship.AssignMission(BuiltObjectMissionType.Blockade, (object)subsequentMission.TargetHabitat, (object)null, BuiltObjectMissionPriority.Normal);
                    }
                }
            }
            else if (subsequentMission.Type == BuiltObjectMissionType.Refuel)
            {
                ship.ClearPreviousMissionRequirements();
                ship.AssignMission(subsequentMission.Type, subsequentMission.Target, subsequentMission.SecondaryTarget, subsequentMission.Cargo, subsequentMission.Troops, subsequentMission.Population, subsequentMission.Design, (double)subsequentMission.X, (double)subsequentMission.Y, subsequentMission.StarDate, subsequentMission.Priority, allowReprocessing);
            }
            else if (subsequentMission.Type == BuiltObjectMissionType.Retrofit)
            {
                if (ship.Empire != null)
                {
                    ship.ClearPreviousMissionRequirements();
                    ship.Empire.AssignRetrofitMission(ship);
                }
            }
            else
            {
                Point point = subsequentMission.ResolveTargetCoordinates(subsequentMission);
                if (!subsequentMission.ManuallyAssigned ? ship.WithinFuelRangeAndRefuel((double)point.X, (double)point.Y, 0.0) : ship.WithinFuelRange((double)point.X, (double)point.Y, 0.0))
                {
                    ship.ClearPreviousMissionRequirements();
                    ship.AssignMission(subsequentMission.Type, subsequentMission.Target, subsequentMission.SecondaryTarget, subsequentMission.Cargo, subsequentMission.Troops, subsequentMission.Population, subsequentMission.Design, (double)subsequentMission.X, (double)subsequentMission.Y, subsequentMission.StarDate, subsequentMission.Priority, allowReprocessing);
                }
            }
            if (ship._SubsequentMissions.Count > 0)
                ship._SubsequentMissions.RemoveAt(0);
            return true;
        }

        public static List<BuiltObject> FindWhoIsTargetingMe(BuiltObject objectiveShip)
        {
            List<BuiltObject> source = new List<BuiltObject>();
            Empire actualEmpire = objectiveShip.ActualEmpire;
            List<BuiltObject> builtObjects = (List<BuiltObject>)actualEmpire.BuiltObjects;
            List<BuiltObject> privateBuiltObjects = (List<BuiltObject>)actualEmpire.PrivateBuiltObjects;
            foreach (BuiltObject builtObject in builtObjects)
            {
                if (builtObject.Mission != null && builtObject.Mission.TargetBuiltObject != null && builtObject.Mission.TargetBuiltObject == objectiveShip)
                    source.Add(builtObject);
                else if (builtObject.Mission != null && builtObject.Mission.SecondaryTargetBuiltObject != null && builtObject.Mission.SecondaryTargetBuiltObject == objectiveShip)
                    source.Add(builtObject);
            }
            foreach (BuiltObject builtObject in privateBuiltObjects)
            {
                if (builtObject.Mission != null && builtObject.Mission.SecondaryTargetBuiltObject != null && builtObject.Mission.SecondaryTargetBuiltObject == objectiveShip)
                    source.Add(builtObject);
                else if (builtObject.Mission != null && builtObject.Mission.SecondaryTargetBuiltObject != null && builtObject.Mission.SecondaryTargetBuiltObject == objectiveShip)
                    source.Add(builtObject);
            }
            return source.OrderBy<BuiltObject, double>((Func<BuiltObject, double>)(x => Galaxy.CalculateDistanceSquaredStatic(objectiveShip.Xpos, objectiveShip.Ypos, x.Ypos, x.Ypos))).ToList<BuiltObject>();
        }

        public static void HugeProcessingSpanActions(BuiltObject ship)
        {
            if (BaconBuiltObject.myMain == null || BaconBuiltObject.myMain._Game == null)
                return;
            if (ship.NearestSystemStar == null)
                BaconBuiltObject.AssignNearestSystemStarIfNull(ship);
            if (new Random().NextDouble() > 0.25)
                return;
            if (ship.ActualEmpire != null && ship.ActualEmpire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                BaconBuiltObject.HandlePlayerPrisoners(ship);
            else
                BaconBuiltObject.HandleAIPrisoners(ship);
        }

        public static List<Character> GetSpiesInPrison(BuiltObject ship)
        {
            if (ship.BaconValues == null)
                return (List<Character>)null;
            Dictionary<string, object> baconValues = ship.BaconValues;
            return !baconValues.ContainsKey("capturedSpies") ? (List<Character>)null : (List<Character>)baconValues["capturedSpies"];
        }

        public static void HandlePlayerPrisoners(BuiltObject ship)
        {
            try
            {
                if (ship.BaconValues == null)
                    return;
                List<Character> spiesInPrison = BaconBuiltObject.GetSpiesInPrison(ship);
                if (spiesInPrison == null || spiesInPrison.Count == 0)
                    return;
                foreach (Character character1 in spiesInPrison)
                {
                    if (character1.Empire != BaconBuiltObject.myMain._Game.PlayerEmpire)
                    {
                        if (BaconBuiltObject.SpyEscaped(ship, character1))
                        {
                            BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, character1.Name + " has escaped from " + ship.Name, Point.Empty, "prisonbreak");
                            CharacterEvent character2 = BaconCharacter.AddEventToCharacter("Escaped", character1.Name + " escaped from " + ship.Name, character1);
                            character1.EventHistory.Add(character2);
                        }
                        else if (BaconBuiltObject.SpyDefected(ship, character1))
                        {
                            BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, character1.Name + " has agreed to join our empire.", Point.Empty, "defect");
                            CharacterEvent character3 = BaconCharacter.AddEventToCharacter("Defected", character1.Name + " defected to " + ship.Name, character1);
                            character1.EventHistory.Add(character3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void HandleAIPrisoners(BuiltObject ship)
        {
            try
            {
                if (ship.ActualEmpire == BaconBuiltObject.myMain._Game.PlayerEmpire || ship.BaconValues == null)
                    return;
                List<Character> spiesInPrison = BaconBuiltObject.GetSpiesInPrison(ship);
                if (spiesInPrison == null || spiesInPrison.Count == 0)
                    return;
                BuiltObject builtObject = BaconBuiltObject.myMain._Game.PlayerEmpire.BuiltObjects[0];
                if (builtObject.BaconValues == null)
                    builtObject.BaconValues = new Dictionary<string, object>();
                List<Character> characterList1 = new List<Character>();
                if (builtObject.BaconValues.ContainsKey("capturedSpies"))
                    characterList1 = (List<Character>)builtObject.BaconValues["capturedSpies"];
                List<Character> characterList2 = new List<Character>();
                foreach (Character character1 in spiesInPrison)
                {
                    if (BaconBuiltObject.SpyEscaped(ship, character1))
                    {
                        characterList2.Add(character1);
                        BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, character1.Name + " has escaped from " + ship.Name, Point.Empty, "prisonbreak");
                        CharacterEvent character2 = BaconCharacter.AddEventToCharacter("Escaped", character1.Name + " escaped from " + ship.Name, character1);
                        character1.EventHistory.Add(character2);
                    }
                    else if (BaconBuiltObject.SpyDefected(ship, character1))
                    {
                        characterList2.Add(character1);
                        BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, character1.Name + " has joined " + ship.Empire.Name, Point.Empty, "defect");
                        CharacterEvent character3 = BaconCharacter.AddEventToCharacter("Escaped", character1.Name + " defected to " + ship.Name, character1);
                        character1.EventHistory.Add(character3);
                    }
                    else if (BaconBuiltObject.ShouldRansomSpy(ship, character1))
                    {
                        characterList1.Add(character1);
                        BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, ship.Empire.Name + " is willing to ransom our agent " + character1.Name);
                        characterList2.Add(character1);
                    }
                }
                foreach (Character character in characterList2)
                    spiesInPrison.Remove(character);
                builtObject.BaconValues["capturedSpies"] = (object)characterList1;
            }
            catch (Exception ex)
            {
            }
        }

        public static bool ShouldRansomSpy(BuiltObject ship, Character spy)
        {
            bool flag = false;
            if (ship.Empire.ObtainDiplomaticRelation(BaconBuiltObject.myMain._Game.PlayerEmpire).Type != DiplomaticRelationType.War)
                flag = true;
            return flag;
        }

        public static bool SpyEscaped(BuiltObject ship, Character spy)
        {
            try
            {
                if (BaconBuiltObject.myMain == null)
                    return false;
                double baseEscapeChance = BaconCharacter.spyBaseEscapeChance;
                if (new Random().NextDouble() > baseEscapeChance)
                    return false;
                if (spy.Empire.Characters == null)
                    spy.Empire.Characters = new CharacterList();
                spy.Empire.Characters.Add(spy);
                if (spy.Empire.PirateEmpireBaseHabitat == null)
                {
                    if (spy.Empire.Capital.Characters == null)
                        spy.Empire.Capital.Characters = new CharacterList();
                    spy.Empire.Capital.Characters.Add(spy);
                }
                else
                {
                    if (spy.Empire.BuiltObjects[0].Characters == null)
                        spy.Empire.BuiltObjects[0].Characters = new CharacterList();
                    spy.Empire.BuiltObjects[0].Characters.Add(spy);
                    spy.Location = (StellarObject)spy.Empire.BuiltObjects[0];
                }
                IntelligenceMission intelligenceMission = new IntelligenceMission(spy.Empire, spy, BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate)
                {
                    TimeLength = (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 4)
                };
                spy.Mission = intelligenceMission;
                ((List<Character>)ship.Empire.BuiltObjects[0].BaconValues["capturedSpies"]).Remove(spy);
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        public static bool SpyDefected(BuiltObject ship, Character spy)
        {
            try
            {
                if (BaconBuiltObject.myMain == null)
                    return false;
                double baseDefectChance = BaconCharacter.spyBaseDefectChance;
                if (new Random().NextDouble() > baseDefectChance)
                    return false;
                if (ship.Empire.Characters == null)
                    ship.Empire.Characters = new CharacterList();
                ship.Empire.Characters.Add(spy);
                if (ship.Empire.BuiltObjects[0].Characters == null)
                    ship.Empire.BuiltObjects[0].Characters = new CharacterList();
                ship.Empire.BuiltObjects[0].Characters.Add(spy);
                spy.Location = (StellarObject)ship.Empire.BuiltObjects[0];
                spy.Empire = ship.ActualEmpire;
                IntelligenceMission intelligenceMission = new IntelligenceMission(spy.Empire, spy, BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate)
                {
                    TimeLength = (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 4)
                };
                spy.Mission = intelligenceMission;
                ((List<Character>)ship.Empire.BuiltObjects[0].BaconValues["capturedSpies"]).Remove(spy);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Image SetOpacity(Image image, float opacity)
        {
            ColorMatrix newColorMatrix = new ColorMatrix();
            newColorMatrix.Matrix33 = opacity;
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
            }
            return (Image)bitmap;
        }

        public static void ClearCargo(BuiltObject ship)
        {
            if (!ship.IsAutoControlled)
                return;
            ship.Cargo.Clear();
        }

        public static void TeleportToSystem(BuiltObject ship, Habitat star)
        {
            if (ship.NearestSystemStar == null)
                return;
            if (star == null)
                return;
            try
            {
                Habitat nearestSystemStar = ship.NearestSystemStar;
                SystemVisibilityStatus visibilityStatus = ship.Empire.CheckSystemVisible(nearestSystemStar, ship.ActualEmpire, (BuiltObject)null, (Habitat)null);
                ship.ActualEmpire.SystemVisibility[nearestSystemStar.SystemIndex].Status = visibilityStatus;
                if (ship.Mission != null && (ship.Mission.FastPeekCurrentCommand().Action == CommandAction.HyperTo || ship.Mission.FastPeekCurrentCommand().Action == CommandAction.ConditionalHyperTo))
                {
                    ship.Mission.CompleteCommand();
                    Command command = ship.Mission.FastPeekCurrentCommand();
                    if (command != null && command.Action == CommandAction.SetParent)
                        ship.Mission.InsertCommandAtTop(ship.Mission.FastPeekCurrentCommand());
                }
            }
            catch (Exception ex)
            {
            }
            if (BaconBuiltObject.myMain != null)
                BaconBuiltObject.DrawStargateAnimation(BaconBuiltObject.myMain, ship, 3, 15);
            GalaxyIndex galaxyIndex1 = ship._Galaxy.ResolveIndex(ship.Xpos, ship.Ypos);
            double num1 = ship.NearestSystemStar.Xpos - ship.Xpos;
            double num2 = ship.NearestSystemStar.Ypos - ship.Ypos;
            ship.NearestSystemStar = star;
            ship.Xpos = star.Xpos - num1;
            ship.Ypos = star.Ypos - num2;
            GalaxyIndex galaxyIndex2 = ship._Galaxy.ResolveIndex(ship.Xpos, ship.Ypos);
            if (galaxyIndex1 != galaxyIndex2)
            {
                ship._Galaxy.BuiltObjectIndex[galaxyIndex1.X][galaxyIndex1.Y].Remove(ship);
                ship._Galaxy.BuiltObjectIndex[galaxyIndex2.X][galaxyIndex2.Y].Add(ship);
            }
            if (ship.ActualEmpire != null)
                ship.ActualEmpire.ResolveSystemVisibility(ship, false);
            ship._LastPositionX = ship.Xpos;
            ship._LastPositionY = ship.Ypos;
            ship.NearestSystemStar = (Habitat)null;
            BaconBuiltObject.AssignNearestSystemStarIfNull(ship);
        }

        public static void UpdatePosition(BuiltObject ship)
        {
            Habitat nearestSystem = ship._Galaxy.FastFindNearestSystem(ship.Xpos, ship.Ypos);
            if (nearestSystem != null && ship._Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, nearestSystem.Xpos, nearestSystem.Ypos) < ((double)Galaxy.MaxSolarSystemSize + 500.0) * ((double)Galaxy.MaxSolarSystemSize + 500.0))
                ship.NearestSystemStar = nearestSystem;
            GalaxyIndex galaxyIndex = ship._Galaxy.ResolveIndex(ship.Xpos, ship.Ypos);
            ship.UpdateIndexesForMovement(galaxyIndex.X, galaxyIndex.Y, ship._Galaxy, false);
        }

        public static void CheckIfShouldUseStargateAndReassign(BuiltObject ship)
        {
            if (ship.Mission == null || !BaconMain.useStargates || !ship.ActualEmpire.Name.StartsWith("Romulan"))
                return;
            if (BaconMain.isDebugging && ship.Name.StartsWith(BaconMain.debugLabel))
                BaconBuiltObject.myMain._Game.Galaxy.Pause();
            switch (ship.Mission.Type)
            {
                case BuiltObjectMissionType.Build:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Transport:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_TransportMission(ship);
                    break;
                case BuiltObjectMissionType.Blockade:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Attack:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Retrofit:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Colonize:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.MoveAndWait:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Refuel:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.ExtractResources:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.LoadTroops:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.UnloadTroops:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Repair:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Move:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Bombard:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
                case BuiltObjectMissionType.Capture:
                    BaconBuiltObject.CheckIfShouldUseStargateAndReassign_MoveMission(ship);
                    break;
            }
        }

        public static void CheckIfShouldUseStargateAndReassign_TransportMission(BuiltObject ship)
        {
            if (ship.Mission == null || ship.Mission.FastPeekCurrentCommand().Action != CommandAction.ConditionalHyperTo)
                return;
            ship.NearestSystemStar = (Habitat)null;
            BaconBuiltObject.SetShipNearestStar(ship);
            Func<Habitat, bool> ofStargateAllowed = BaconBuiltObject.FindTypeOfStargateAllowed(ship);
            Habitat nearestColonyAnyEmpire = BaconGalaxy.FastFindNearestColonyAnyEmpire(ship.Xpos, ship.Ypos, false, ofStargateAllowed);
            List<double> missionTargetXy = BaconBuiltObject.FindMissionTargetXY(ship);
            Habitat star = BaconGalaxy.FastFindNearestColonyAnyEmpire(missionTargetXy[0], missionTargetXy[1], false, ofStargateAllowed);
            if (nearestColonyAnyEmpire.SystemIndex == star.SystemIndex && ship.NearestSystemStar != null && ship.NearestSystemStar.SystemIndex == nearestColonyAnyEmpire.SystemIndex || ship.Mission.FastPeekCurrentCommand().TargetHabitat != null && ship.Mission.FastPeekCurrentCommand().TargetHabitat == nearestColonyAnyEmpire || Math.Sqrt(Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, missionTargetXy[0], missionTargetXy[1])) < Math.Sqrt(Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, nearestColonyAnyEmpire.Xpos, nearestColonyAnyEmpire.Ypos)) + Math.Sqrt(Galaxy.CalculateDistanceSquaredStatic(star.Xpos, star.Ypos, missionTargetXy[0], missionTargetXy[1])))
                return;
            if (ship.NearestSystemStar != null && ship.NearestSystemStar.SystemIndex == nearestColonyAnyEmpire.SystemIndex)
            {
                while (star.Parent != null)
                    star = star.Parent;
                BaconBuiltObject.TeleportToSystem(ship, star);
                Command command = ship.Mission.FastPeekCurrentCommand();
                float xpos = command.Xpos;
                float ypos = command.Ypos;
                if (ship.Mission.ShowNextCommand().TargetHabitat != null)
                    ship.Mission.TargetHabitat = ship.Mission.ShowNextCommand().TargetHabitat;
                else if (ship.Mission.ShowNextCommand().TargetBuiltObject != null)
                    ship.Mission.TargetBuiltObject = ship.Mission.ShowNextCommand().TargetBuiltObject;
                else if (ship.Mission.ShowNextCommand().TargetCreature != null)
                    ship.Mission.TargetCreature = ship.Mission.ShowNextCommand().TargetCreature;
                else if (ship.Mission.ShowNextCommand().TargetShipGroup != null)
                    ship.Mission.TargetShipGroup = ship.Mission.ShowNextCommand().TargetShipGroup;
                else
                    ship.Mission.TargetHabitat = (Habitat)null;
            }
            else
            {
                ship.Mission.FastPeekCurrentCommand().TargetHabitat = (Habitat)null;
                ship.Mission.InsertCommandAtTop(new Command(CommandAction.MoveTo, nearestColonyAnyEmpire));
                ship.Mission.TargetHabitat = nearestColonyAnyEmpire;
            }
        }

        public static void CheckIfShouldUseStargateAndReassign_MoveMission(BuiltObject ship)
        {
            if (ship.Mission == null || ship.Mission.FastPeekCurrentCommand().Action != CommandAction.ConditionalHyperTo)
                return;
            ship.NearestSystemStar = (Habitat)null;
            BaconBuiltObject.SetShipNearestStar(ship);
            Func<Habitat, bool> ofStargateAllowed = BaconBuiltObject.FindTypeOfStargateAllowed(ship);
            Habitat nearestColonyAnyEmpire = BaconGalaxy.FastFindNearestColonyAnyEmpire(ship.Xpos, ship.Ypos, false, ofStargateAllowed);
            List<double> missionTargetXy = BaconBuiltObject.FindMissionTargetXY(ship);
            Habitat star = BaconGalaxy.FastFindNearestColonyAnyEmpire(missionTargetXy[0], missionTargetXy[1], false, ofStargateAllowed);
            if (nearestColonyAnyEmpire.SystemIndex == star.SystemIndex && ship.NearestSystemStar != null && ship.NearestSystemStar.SystemIndex == nearestColonyAnyEmpire.SystemIndex || ship.Mission.FastPeekCurrentCommand().TargetHabitat != null && ship.Mission.FastPeekCurrentCommand().TargetHabitat == nearestColonyAnyEmpire || Math.Sqrt(Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, missionTargetXy[0], missionTargetXy[1])) < Math.Sqrt(Galaxy.CalculateDistanceSquaredStatic(ship.Xpos, ship.Ypos, nearestColonyAnyEmpire.Xpos, nearestColonyAnyEmpire.Ypos)) + Math.Sqrt(Galaxy.CalculateDistanceSquaredStatic(star.Xpos, star.Ypos, missionTargetXy[0], missionTargetXy[1])))
                return;
            if (ship.NearestSystemStar != null && ship.NearestSystemStar.SystemIndex == nearestColonyAnyEmpire.SystemIndex)
            {
                while (star.Parent != null)
                    star = star.Parent;
                BaconBuiltObject.TeleportToSystem(ship, star);
                Command command = ship.Mission.FastPeekCurrentCommand();
                float xpos = command.Xpos;
                float ypos = command.Ypos;
                if (ship.Mission.ShowNextCommand()?.TargetHabitat != null)
                    ship.Mission.TargetHabitat = ship.Mission.ShowNextCommand().TargetHabitat;
                else if (ship.Mission.ShowNextCommand()?.TargetBuiltObject != null)
                    ship.Mission.TargetBuiltObject = ship.Mission.ShowNextCommand().TargetBuiltObject;
                else if (ship.Mission.ShowNextCommand()?.TargetCreature != null)
                    ship.Mission.TargetCreature = ship.Mission.ShowNextCommand().TargetCreature;
                else if (ship.Mission.ShowNextCommand()?.TargetShipGroup != null)
                {
                    ship.Mission.TargetShipGroup = ship.Mission.ShowNextCommand().TargetShipGroup;
                }
                else
                {
                    ship.Mission.TargetHabitat = (Habitat)null;
                    FieldInfo field1 = typeof(BuiltObjectMission).GetField("_MissionXCoord", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (field1 != (FieldInfo)null)
                        field1.SetValue((object)ship.Mission, (object)xpos);
                    FieldInfo field2 = typeof(BuiltObjectMission).GetField("_MissionYCoord", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (field1 != (FieldInfo)null)
                        field2.SetValue((object)ship.Mission, (object)ypos);
                }
            }
            else
            {
                ship.Mission.FastPeekCurrentCommand().TargetHabitat = (Habitat)null;
                ship.Mission.InsertCommandAtTop(new Command(CommandAction.MoveTo, nearestColonyAnyEmpire));
                ship.Mission.TargetHabitat = nearestColonyAnyEmpire;
            }
        }

        public static Func<Habitat, bool> FindTypeOfStargateAllowed(BuiltObject ship) => (Func<Habitat, bool>)(Habitat => Habitat.Empire == ship.Empire);

        public static int FindNumberOfTheActionTypeInStack(BuiltObject ship, CommandAction act)
        {
            int actionTypeInStack = 0;
            if (ship.Mission == null)
                return actionTypeInStack;
            foreach (Command showAllCommand in ship.Mission.ShowAllCommands())
            {
                if (showAllCommand.Action == act)
                    ++actionTypeInStack;
            }
            return actionTypeInStack;
        }

        public static List<double> FindMissionTargetXY(BuiltObject ship)
        {
            double num1 = 0.0;
            double num2 = 0.0;
            List<double> missionTargetXy = new List<double>()
      {
        0.0,
        0.0
      };
            if (ship.Mission != null)
            {
                int actionTypeInStack = BaconBuiltObject.FindNumberOfTheActionTypeInStack(ship, CommandAction.Undock);
                Command command = ship.Mission.FastPeekCurrentCommand();
                if (actionTypeInStack >= 2 && (double)ship.Mission.X > -1900010000.0 && (double)ship.Mission.Y > -1900010000.0)
                {
                    num1 = (double)ship.Mission.X;
                    num2 = (double)ship.Mission.Y;
                }
                else if ((double)ship.Mission.FastPeekCurrentCommand().Xpos > -1900010000.0 && (double)ship.Mission.FastPeekCurrentCommand().Ypos > -1900010000.0)
                {
                    num1 = (double)ship.Mission.FastPeekCurrentCommand().Xpos;
                    num2 = (double)ship.Mission.FastPeekCurrentCommand().Ypos;
                }
                else if (command.TargetBuiltObject != null)
                {
                    num1 = command.TargetBuiltObject.Xpos;
                    num2 = command.TargetBuiltObject.Ypos;
                }
                else if (command.TargetHabitat != null)
                {
                    num1 = command.TargetHabitat.Xpos;
                    num2 = command.TargetHabitat.Ypos;
                }
                else if (command.TargetCreature != null)
                {
                    num1 = command.TargetCreature.Xpos;
                    num2 = command.TargetCreature.Ypos;
                }
                else if (actionTypeInStack >= 2 && ship.Mission.TargetBuiltObject != null)
                {
                    num1 = ship.Mission.TargetBuiltObject.Xpos;
                    num2 = ship.Mission.TargetBuiltObject.Ypos;
                }
                else if (actionTypeInStack >= 2 && ship.Mission.TargetHabitat != null)
                {
                    num1 = ship.Mission.TargetHabitat.Xpos;
                    num2 = ship.Mission.TargetHabitat.Ypos;
                }
                else if (actionTypeInStack >= 2 && ship.Mission.TargetSector != null)
                {
                    num1 = (double)ship.Mission.TargetSector.X;
                    num2 = (double)ship.Mission.TargetSector.Y;
                }
                else if (actionTypeInStack >= 2 && ship.Mission.Target != null)
                {
                    num1 = (double)ship.Mission.X;
                    num2 = (double)ship.Mission.Y;
                }
                else if (actionTypeInStack >= 2 && ship.Mission.TargetCreature != null)
                {
                    num1 = ship.Mission.TargetCreature.Xpos;
                    num2 = ship.Mission.TargetCreature.Ypos;
                }
                else if (actionTypeInStack <= 1 && ship.Mission.SecondaryTargetBuiltObject != null)
                {
                    num1 = ship.Mission.SecondaryTargetBuiltObject.Xpos;
                    num2 = ship.Mission.SecondaryTargetBuiltObject.Ypos;
                }
                else if (actionTypeInStack <= 1 && ship.Mission.SecondaryTargetHabitat != null)
                {
                    num1 = ship.Mission.SecondaryTargetHabitat.Xpos;
                    num2 = ship.Mission.SecondaryTargetHabitat.Ypos;
                }
                else if (actionTypeInStack <= 1 && ship.Mission.SecondaryTargetCreature != null)
                {
                    num1 = ship.Mission.SecondaryTargetCreature.Xpos;
                    num2 = ship.Mission.SecondaryTargetCreature.Ypos;
                }
                else if (ship.Mission.TargetBuiltObject != null)
                {
                    num1 = ship.Mission.TargetBuiltObject.Xpos;
                    num2 = ship.Mission.TargetBuiltObject.Ypos;
                }
                else if (ship.Mission.TargetHabitat != null)
                {
                    num1 = ship.Mission.TargetHabitat.Xpos;
                    num2 = ship.Mission.TargetHabitat.Ypos;
                }
                else if (ship.Mission.TargetSector != null)
                {
                    num1 = (double)ship.Mission.TargetSector.X;
                    num2 = (double)ship.Mission.TargetSector.Y;
                }
                else if (ship.Mission.Target != null)
                {
                    num1 = (double)ship.Mission.X;
                    num2 = (double)ship.Mission.Y;
                }
                else if (ship.Mission.TargetCreature != null)
                {
                    num1 = ship.Mission.TargetCreature.Xpos;
                    num2 = ship.Mission.TargetCreature.Ypos;
                }
                missionTargetXy[0] = num1;
                missionTargetXy[1] = num2;
            }
            return missionTargetXy;
        }

        public static void DrawStargateAnimation(
          Main main,
          BuiltObject ship,
          int sizeMultiplier = 1,
          int framesPersecond = 30)
        {
            int num = (int)Math.Sqrt((double)(ship.Size * 30));
            DistantWorlds.Animation animation = new DistantWorlds.Animation(main.mainView.list_26[0], main._Game.Galaxy.CurrentDateTime, framesPersecond, ship.Xpos, ship.Ypos, num * sizeMultiplier, num * sizeMultiplier, (double)ship.TargetHeading * -1.0, Color.Empty)
            {
                DisposeTexturesWhenComplete = false
            };
            main.mainView.animationSystem_0.AddAnimation(animation);
        }

        public static int CheckForNegativeRefueling(BuiltObject ship, int refuelAmount)
        {
            if (refuelAmount < 0)
                refuelAmount = 0;
            return refuelAmount;
        }

        public static void ExitHyperjump(BuiltObject ship)
        {
            if (ship.WarpSpeed <= 0 || (double)ship.CurrentSpeed <= (double)(ship.WarpSpeed / 3))
                return;
            ship._HyperjumpJustExited = true;
            ship.HyperExitStartAnimation = true;
            ship._HyperjumpPrepare = false;
            ship.HyperEnterStartAnimation = false;
            double xpos = ship.Xpos;
            double ypos = ship.Ypos;
            ship.CheckForHyperExitGravityWells(ref xpos, ref ypos);
            ship.Xpos = xpos;
            ship.Ypos = ypos;
            if (ship.ShipGroup == null)
                ship._Galaxy.DoCharacterEvent(CharacterEventType.HyperjumpExit, (object)ship, ship.Characters);
            else
                ship._Galaxy.DoCharacterEvent(CharacterEventType.HyperjumpExit, (object)ship, ship.ShipGroup.ObtainCharacters());
            Habitat nearestSystem = ship._Galaxy.FastFindNearestSystem(ship.Xpos, ship.Ypos);
            if (nearestSystem != null && ship._Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, nearestSystem.Xpos, nearestSystem.Ypos) < ((double)Galaxy.MaxSolarSystemSize + 1000.0) * ((double)Galaxy.MaxSolarSystemSize + 1000.0))
                ship.NearestSystemStar = nearestSystem;
            if (ship.ActualEmpire != null)
                ship.ActualEmpire.ResolveSystemVisibility(ship, false);
        }

        public static void CheckFuelHandicap(BuiltObject ship)
        {
            if (ship.CurrentFuel <= 0.0 && ship.CurrentEnergy <= 0.0)
            {
                if (ship._FuelHandicapped)
                    return;
                ship.TopSpeed = (short)((double)ship.TopSpeed * (double)BaconBuiltObject.noFuelTopSpeedMultiplier);
                ship.CruiseSpeed = (short)((double)ship.CruiseSpeed * (double)BaconBuiltObject.noFuelCruiseSpeedMultiplier);
                ship.WarpSpeed = (int)((double)ship.WarpSpeed * (double)BaconBuiltObject.noFuelHyperSpeedMultiplier);
                if (ship.NearestSystemStar == null && ship.WarpSpeedWithBonuses > 0)
                {
                    if ((double)ship.CurrentSpeed > (double)ship.WarpSpeedWithBonuses)
                        ship.CurrentSpeed = (float)ship.WarpSpeedWithBonuses;
                    if (ship.TargetSpeed > ship.WarpSpeedWithBonuses)
                        ship.TargetSpeed = ship.WarpSpeedWithBonuses;
                }
                else
                {
                    if ((double)ship.CurrentSpeed > (double)ship.TopSpeed)
                        ship.CurrentSpeed = (float)ship.TopSpeed;
                    if (ship.TargetSpeed > (int)ship.TopSpeed)
                        ship.TargetSpeed = (int)ship.TopSpeed;
                }
                ship._FuelHandicapped = true;
            }
            else
            {
                if (!ship._FuelHandicapped)
                    return;
                ship.ReDefine();
                ship._FuelHandicapped = false;
            }
        }

        public static int GetCargoIndex(BuiltObject dockingShip)
        {
            int num = -1;
            return dockingShip == null || dockingShip.DockedAt == null || dockingShip.DockedAt.Cargo == null || dockingShip.DockedAt.Cargo.Count < 1 ? num : (!(dockingShip.DockedAt is BuiltObject) ? dockingShip.DockedAt.Cargo.IndexOf(dockingShip.FuelType, dockingShip.DockedAt.Empire) : dockingShip.DockedAt.Cargo.IndexOf(dockingShip.FuelType, (dockingShip.DockedAt as BuiltObject).ActualEmpire));
        }

        public static void AddShipsOfTypeToEachEmpire(
          BuiltObjectSubRole subrole,
          int amount,
          string pirateOrEmpire = "pirate")
        {
            switch (pirateOrEmpire)
            {
                case "pirate":
                    using (IEnumerator<Empire> enumerator = BaconBuiltObject.myMain._Game.Galaxy.PirateEmpires.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Empire current = enumerator.Current;
                            BuiltObject parent = (BuiltObject)null;
                            if (current.BuiltObjects.Count > 0)
                                parent = current.BuiltObjects[0];
                            if (parent != null)
                            {
                                Design newestCanBuild = current.LatestDesigns.FindNewestCanBuild(subrole, current);
                                if (newestCanBuild != null)
                                {
                                    for (int index = 0; index < amount; ++index)
                                    {
                                        string name = BaconBuiltObject.myMain._Game.Galaxy.SelectRandomUniqueStandardShipName((Habitat)null);
                                        newestCanBuild.BuildCount = newestCanBuild.BuildCount++;
                                        BuiltObject builtObject1 = new BuiltObject(newestCanBuild, name, BaconBuiltObject.myMain._Game.Galaxy, true);
                                        builtObject1.Empire = current;
                                        builtObject1.Heading = 0.0f;
                                        builtObject1.TargetHeading = 0.0f;
                                        BuiltObject builtObject2 = builtObject1;
                                        builtObject2.ReDefine();
                                        builtObject2.CurrentFuel = (double)builtObject2.FuelCapacity;
                                        builtObject2.CurrentShields = (float)builtObject2.ShieldsCapacity;
                                        current.AddBuiltObjectToGalaxy(builtObject2, (object)parent, false, true, 0, 0, false);
                                    }
                                }
                            }
                        }
                        break;
                    }
                case "empire":
                    foreach (Empire empire in (SyncList<Empire>)BaconBuiltObject.myMain._Game.Galaxy.Empires)
                    {
                        Habitat capital = empire.Capital;
                        if (capital != null)
                        {
                            Design newestCanBuild = empire.LatestDesigns.FindNewestCanBuild(subrole, empire);
                            BuiltObject builtObject3 = (BuiltObject)null;
                            if (newestCanBuild != null)
                            {
                                for (int index = 0; index < amount; ++index)
                                {
                                    string name = BaconBuiltObject.myMain._Game.Galaxy.SelectRandomUniqueStandardShipName((Habitat)null);
                                    newestCanBuild.BuildCount = newestCanBuild.BuildCount++;
                                    BuiltObject builtObject4 = new BuiltObject(newestCanBuild, name, BaconBuiltObject.myMain._Game.Galaxy, true);
                                    builtObject4.Empire = empire;
                                    builtObject4.Heading = BaconBuiltObject.myMain._Game.Galaxy.SelectRandomHeading();
                                    builtObject4.TargetHeading = builtObject3.Heading;
                                    builtObject4.SupportCostFactor = 0.0f;
                                    builtObject3 = builtObject4;
                                    builtObject3.ReDefine();
                                    builtObject3.CurrentFuel = (double)builtObject3.FuelCapacity;
                                    builtObject3.CurrentShields = (float)builtObject3.ShieldsCapacity;
                                    empire.AddBuiltObjectToGalaxy(builtObject3, (object)capital, false, true, 0, 0, false);
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public static void DoRepairs(BuiltObject ship, double timePassed)
        {
            if (BaconBuiltObject.myMain == null)
                return;
            int num1 = ship.DamageRepair;
            int num2 = 0;
            switch (BaconBuiltObject.CalculateCrewLevel(BaconBuiltObject.myMain, ship))
            {
                case "average":
                    num2 = BaconBuiltObject.shipFreeRepairTimeFromCrewSkillAverage;
                    break;
                case "experienced":
                    num2 = BaconBuiltObject.shipFreeRepairTimeFromCrewSkillExperienced;
                    break;
                case "veteran":
                    num2 = BaconBuiltObject.shipFreeRepairTimeFromCrewSkillVeteran;
                    break;
                case "elite":
                    num2 = BaconBuiltObject.shipFreeRepairTimeFromCrewSkillElite;
                    break;
                case "legendary":
                    num2 = BaconBuiltObject.shipFreeRepairTimeFromCrewSkillLegendary;
                    break;
            }
            if (num2 != 0 && (num2 < num1 || num1 == 0))
                num1 = num2;
            if (ship.Empire != null && num1 > 0 && ship.DamagedComponentCount > 0)
            {
                double num3 = (double)num1;
                if (ship.ShipGroup != null)
                    num3 /= ship.ShipGroup.RepairBonus;
                double num4 = num3 / ship.CaptainRepairBonus;
                int num5 = (int)(timePassed / num4);
                if (num5 == 0)
                {
                    double num6 = timePassed / num4;
                    if (new Random().NextDouble() < num6)
                        num5 = 1;
                }
                int componentToRepairCount = num5;
                if (componentToRepairCount > 0)
                {
                    List<ComponentCategoryType> repairTemplate;
                    if (ship.Design.RepaitPriorityTemplateName != null &&
                       (repairTemplate = Main._ExpModMain.GetRepairPriorityList(ship.Design.RepaitPriorityTemplateName)) != null)
                    {
                        foreach (var item in ship.Components.GroupBy(x => x.Category).OrderBy(x => repairTemplate.IndexOf(x.Key)))
                        {
                            foreach (var component in item)
                            {

                                if (component.Status == ComponentStatus.Damaged)
                                {
                                    component.Status = ComponentStatus.Normal;
                                    --componentToRepairCount;
                                }
                                if (componentToRepairCount == 0)
                                {
                                    break;
                                }
                            }
                            if (componentToRepairCount == 0)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        int num8 = Galaxy.Rnd.Next(0, ship.Components.Count<BuiltObjectComponent>());
                        for (int index = num8; index < ship.Components.Count<BuiltObjectComponent>() && componentToRepairCount > 0; ++index)
                        {
                            if (ship.Components[index].Status == ComponentStatus.Damaged)
                            {
                                ship.Components[index].Status = ComponentStatus.Normal;
                                --componentToRepairCount;
                            }
                        }
                        for (int index = 0; index < num8 && componentToRepairCount > 0; ++index)
                        {
                            if (ship.Components[index].Status == ComponentStatus.Damaged)
                            {
                                ship.Components[index].Status = ComponentStatus.Normal;
                                --componentToRepairCount;
                            }
                        }
                    }
                    ship.ReDefine();
                    int repairAmount = Math.Max(0, num5 - componentToRepairCount);
                    if (ship.BattleStats != null)
                        ship.BattleStats.DamageRepairedUs(repairAmount);
                    if (ship.ShipGroup != null && ship.ShipGroup.BattleStats != null)
                        ship.ShipGroup.BattleStats.DamageRepairedUs(repairAmount);
                }
            }
            if (ship.DamagedComponentCount != 0 || ship.Mission == null || ship.Mission.Type != BuiltObjectMissionType.Repair)
                return;
            ship.Mission.Clear();
        }

        public static StellarObject[] IdentifySystemThreatsToUs(
          BuiltObject ship,
          Habitat systemStar,
          out int[] threatLevels,
          out int totalThreatLevel)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            double num1 = (double)Galaxy.ThreatRange;
            if ((double)ship.SensorProximityArrayRange > num1)
                num1 = (double)ship.SensorProximityArrayRange;
            double num2 = (double)Galaxy.ThreatRange / 2.0;
            double num3 = num1 * num1;
            for (int index = 0; index < ship.Empire.SystemVisibility[systemStar.SystemIndex].Threats.Count<BuiltObject>(); ++index)
            {
                BuiltObject threat = ship.Empire.SystemVisibility[systemStar.SystemIndex].Threats[index];
                double distanceSquared = ship._Galaxy.CalculateDistanceSquared(threat.Xpos, threat.Ypos, ship.Xpos, ship.Ypos);
                if (distanceSquared <= num3)
                {
                    double num4 = Math.Sqrt(distanceSquared);
                    if (num4 < 1500.0 || stellarObjectList.Count<StellarObject>() < 50)
                    {
                        double num5 = Math.Max(1.0, num2 - num4);
                        double num6 = num5 * num5 / 1000000.0;
                        if (threat.PirateEmpireId > (byte)0 && threat.Empire == ship._Galaxy.IndependentEmpire && (int)threat.PirateEmpireId != (int)ship.PirateEmpireId && ship.SensorTraceScannerPower > (short)0 && num4 < (double)ship.SensorTraceScannerRange && (int)ship.SensorTraceScannerPower >= (int)threat.SensorTraceScannerJamming && threat.Characters != null)
                        {
                            double num7 = 0.2;
                            double d = 0.01 * (double)threat.Characters.GetHighestSkillLevel(CharacterSkillType.SmugglingEvasion);
                            double num8 = Math.Max(0.01, num7 - num7 * Math.Sqrt(d));
                            if (Galaxy.Rnd.NextDouble() < num8)
                            {
                                Empire byEmpireId = ship._Galaxy.PirateEmpires.GetByEmpireId((int)threat.PirateEmpireId);
                                if (byEmpireId != null && ship.Empire != null && ship.Empire.ObtainPirateRelation(byEmpireId).Type != PirateRelationType.Protection)
                                {
                                    threat.Empire = byEmpireId;
                                    string description1 = string.Format(TextResolver.GetText("Pirate Smuggler Detected Ours Ship"), (object)threat.Name, (object)ship.Empire.Name, (object)systemStar.Name);
                                    byEmpireId.SendMessageToEmpire(byEmpireId, EmpireMessageType.PirateSmugglerDetected, (object)threat, description1);
                                    string description2 = string.Format(TextResolver.GetText("Pirate Smuggler Detected Other Ship"), (object)threat.Name, (object)byEmpireId.Name, (object)systemStar.Name);
                                    ship.Empire.SendMessageToEmpire(ship.Empire, EmpireMessageType.PirateSmugglerDetected, (object)threat, description2);
                                    ship._Galaxy.DoCharacterEvent(CharacterEventType.SmugglingDetection, (object)ship, threat.Characters);
                                    threat.ClearPreviousMissionRequirements();
                                    threat.AssignMission(BuiltObjectMissionType.Escape, (object)ship, (object)null, BuiltObjectMissionPriority.High);
                                }
                            }
                        }
                        stellarObjectList.Add((StellarObject)threat);
                        double d1 = num6 * (double)ship.Empire.SystemVisibility[systemStar.SystemIndex].ThreatLevels[index];
                        if (double.IsNaN(d1))
                            d1 = 1.0;
                        if (ship.SubRole == BuiltObjectSubRole.DefensiveBase && BaconBuiltObject.myMain != null && threat.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire && threat.Troops != null && threat.Troops.Count > 0)
                            d1 *= 100.0;
                        threat.SortTag = d1;
                    }
                }
            }
            for (int index = 0; index < ship._Galaxy.Systems[systemStar.SystemIndex].Creatures.Count<Creature>(); ++index)
            {
                Creature creature = ship._Galaxy.Systems[systemStar.SystemIndex].Creatures[index];
                if (creature.IsVisible && creature.AttackStrength > 0)
                {
                    double distanceSquared = ship._Galaxy.CalculateDistanceSquared(creature.Xpos, creature.Ypos, ship.Xpos, ship.Ypos);
                    if (distanceSquared <= num3)
                    {
                        double num9 = Math.Sqrt(distanceSquared);
                        double num10 = Math.Max(1.0, num2 - num9);
                        double num11 = num10 * num10 / 1000000.0;
                        stellarObjectList.Add((StellarObject)creature);
                        double d = Math.Max(1.0, num11 * 4000.0);
                        if (double.IsNaN(d))
                            d = 1.0;
                        creature.SortTag = d;
                    }
                }
            }
            StellarObjectList source = BaconBuiltObject.FilterInvalidTargets(ship, stellarObjectList);
            StellarObject.SortStellarObject sortStellarObject = new StellarObject.SortStellarObject();
            source.Sort((IComparer<StellarObject>)sortStellarObject);
            source.Reverse();
            totalThreatLevel = 0;
            for (int index = 0; index < source.Count<StellarObject>(); ++index)
            {
                double d = source[index].SortTag / 1000.0;
                if (d > 21474836.0)
                    d = 21474836.0;
                else if (d < 1.0)
                    d = 1.0;
                if (double.IsNaN(d))
                    d = 1.0;
                totalThreatLevel += (int)d;
            }
            int count = Math.Min(10, source.Count<StellarObject>());
            StellarObject[] array = new StellarObject[count];
            source.CopyTo(0, array, 0, count);
            threatLevels = new int[count];
            for (int index = 0; index < count; ++index)
                threatLevels[index] = (int)Math.Max((double)int.MinValue, Math.Min((double)int.MaxValue, source[index].SortTag));
            return array;
        }

        public static StellarObjectList FilterInvalidTargets(
          BuiltObject ship,
          StellarObjectList threatList)
        {
            StellarObjectList stellarObjectList1 = new StellarObjectList();
            StellarObjectList stellarObjectList2 = new StellarObjectList();
            List<string> stringList = new List<string>();
            if (ship.BaconValues == null || !ship.BaconValues.ContainsKey("notarget"))
                return threatList;
            List<string> baconValue = (List<string>)ship.BaconValues["notarget"];
            for (int index = 0; index < threatList.Count; ++index)
            {
                if (threatList[index] is BuiltObject)
                {
                    BuiltObject threat = threatList[index] as BuiltObject;
                    if (threat.Role == BuiltObjectRole.Military && baconValue.Contains("military") || threat.Role == BuiltObjectRole.Base && baconValue.Contains("bases") || threat.CalculateFirepowerFactor() > ship.CalculateFirepowerFactor() && baconValue.Contains("stronger") || (int)threat.TopSpeed > (int)ship.TopSpeed && baconValue.Contains("faster"))
                        stellarObjectList2.Add((StellarObject)threat);
                }
            }
            for (int index = 0; index < threatList.Count; ++index)
            {
                if (!stellarObjectList2.Contains(threatList[index]))
                    stellarObjectList1.Add(threatList[index]);
            }
            return stellarObjectList1;
        }

        public static void SetTargetRestriction(Main main, string input)
        {
            if (main._Game.SelectedObject is BuiltObject)
            {
                BaconBuiltObject.SetTargetRestrictionShip(main, input, main._Game.SelectedObject as BuiltObject);
            }
            else
            {
                if (!(main._Game.SelectedObject is ShipGroup))
                    return;
                foreach (BuiltObject ship in (SyncList<BuiltObject>)(main._Game.SelectedObject as ShipGroup).Ships)
                    BaconBuiltObject.SetTargetRestrictionShip(main, input, ship);
            }
        }

        public static void SetTargetRestrictionShip(Main main, string input, BuiltObject ship)
        {
            if (ship.ActualEmpire != main._Game.PlayerEmpire && !main._Game.PlayerEmpire.Name.Contains("Romulan"))
                return;
            if (ship.BaconValues == null)
                ship.BaconValues = new Dictionary<string, object>();
            List<string> stringList = new List<string>();
            if (ship.BaconValues.ContainsKey("notarget"))
                stringList = (List<string>)ship.BaconValues["notarget"];
            string[] strArray = input.Split(' ');
            if (strArray.Length == 1)
            {
                stringList.Clear();
                ship.BaconValues.Remove("notarget");
            }
            else
            {
                for (int index = 1; index < strArray.Length; ++index)
                {
                    if (!stringList.Contains(strArray[index]))
                        stringList.Add(strArray[index]);
                }
                ship.BaconValues["notarget"] = (object)stringList;
            }
            if (ship.BaconValues.Count != 0)
                return;
            ship.BaconValues = (Dictionary<string, object>)null;
        }

        public static void InterceptMissiles(BuiltObject ship, DateTime time, bool inView)
        {
            if (!BaconBuiltObject.pointDefenseAffectsMissiles)
                return;
            try
            {
                if (ship._AssaultPodFiringCounter >= (short)32766)
                    ship._AssaultPodFiringCounter = (short)0;
                ++ship._AssaultPodFiringCounter;
                if (inView && (int)ship._AssaultPodFiringCounter % 5 != 0)
                    return;
                for (int index1 = 0; index1 < ship.Attackers.Count; ++index1)
                {
                    StellarObject attacker = ship.Attackers[index1];
                    if (attacker != null && attacker is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)attacker;
                        if (builtObject.Weapons != null)
                        {
                            for (int index2 = 0; index2 < builtObject.Weapons.Count; ++index2)
                            {
                                Weapon weapon1 = builtObject.Weapons[index2];
                                if (weapon1 != null && weapon1.Component != null && weapon1.Component.Type == ComponentType.WeaponMissile && (double)weapon1.DistanceTravelled >= 100.0 && weapon1.Target != null && weapon1.Target == ship && ship.Weapons != null)
                                {
                                    for (int index3 = 0; index3 < ship.Weapons.Count; ++index3)
                                    {
                                        Weapon weapon2 = ship.Weapons[index3];
                                        if (weapon2.Component.Type != ComponentType.AssaultPod && weapon2 != null && weapon2.Component != null && (weapon2.Component.Type == ComponentType.WeaponPointDefense || weapon2.Component.Type == ComponentType.WeaponBeam || weapon2.Component.Type == ComponentType.WeaponPhaser || weapon2.Component.Type == ComponentType.WeaponRailGun) && (double)weapon1.DistanceFromTarget <= (double)weapon2.Range && weapon2.IsAvailable(ship, time))
                                        {
                                            bool flag = false;
                                            if (weapon2.Component.Type == ComponentType.WeaponPointDefense)
                                                flag = true;
                                            double hitRangeChance = 0.5;
                                            bool willHit = true;
                                            weapon2.Fire(ship._Galaxy, (StellarObject)ship, weapon1, (double)weapon1.DistanceFromTarget, time, willHit, hitRangeChance);
                                            if (weapon1 != null && !flag && BaconBuiltObject.myMain != null)
                                            {
                                                DistantWorlds.Animation animation = new DistantWorlds.Animation(BaconBuiltObject.myMain.mainView.list_26[0], BaconBuiltObject.myMain._Game.Galaxy.CurrentDateTime, 60, weapon1.X, weapon1.Y, 24, 24, -1.0, Color.Empty)
                                                {
                                                    DisposeTexturesWhenComplete = false
                                                };
                                                BaconBuiltObject.myMain.mainView.animationSystem_0.AddAnimation(animation);
                                            }
                                            weapon1.Reset();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void GiveAllStrategicResourcesCargoToBase(BuiltObject ship, int amount)
        {
            foreach (ResourceDefinition resourceDefinition in BaconBuiltObject.myMain._Game.Galaxy.ResourceSystem.StrategicResources.ToList<ResourceDefinition>())
                ship.Cargo.Add(new Cargo(new Resource(resourceDefinition.ResourceID), amount, ship.Empire));
        }

        public static bool CheckAndCreateBaconValuesKey(BuiltObject ship, string keyToFind)
        {
            if (ship.BaconValues == null)
                ship.BaconValues = new Dictionary<string, object>();
            if (ship.BaconValues.ContainsKey(keyToFind))
                return true;
            ship.BaconValues["scientificData"] = (object)0;
            return false;
        }

        public static double PrivateSectorBuildOrRefitInvestInInfrastructure(
          BuiltObject ship,
          double cost)
        {
            double num1 = (1.0 - BaconBuiltObject.privateBuildCostToStateMoney) * cost;
            double num2 = BaconBuiltObject.privateBuildCostToStateMoney * cost;
            try
            {
                if (BaconMain.isDebugging && ship.Name == BaconMain.debugLabel)
                    BaconBuiltObject.myMain._Game.Galaxy.Pause();
                StellarObjectList coloniesToExclude = new StellarObjectList();
                foreach (BuiltObject spacePort in (SyncList<BuiltObject>)ship.ActualEmpire.SpacePorts)
                {
                    if (spacePort.Name.StartsWith("--"))
                        coloniesToExclude.Add((StellarObject)spacePort);
                }
                Habitat habitat = ship.ActualEmpire.SelectRandomSpacePortColony(coloniesToExclude);
                if (habitat != null)
                {
                    if (habitat.BaconValues == null)
                        habitat.BaconValues = new Dictionary<string, object>();
                    if (habitat.BaconValues.ContainsKey("infrastructure"))
                    {
                        int baconValue = (int)habitat.BaconValues["infrastructure"];
                        habitat.BaconValues["infrastructure"] = (object)(baconValue + (int)num1);
                    }
                    else
                        habitat.BaconValues.Add("infrastructure", (object)(int)num1);
                }
            }
            catch (Exception ex)
            {
                if (BaconMain.isDebugging)
                    BaconBuiltObject.myMain._Game.Galaxy.Pause();
            }
            return num2;
        }

        public static void SaveShipInfoBeforeDestruction(BuiltObject shipToBeDestroyed)
        {
            if (BaconBuiltObject.shipsToBeDestroyed.ContainsKey(shipToBeDestroyed.Name) || shipToBeDestroyed.Cargo == null)
                return;
            CargoList cargoList = new CargoList();
            foreach (Cargo cargo1 in (SyncList<Cargo>)shipToBeDestroyed.Cargo)
            {
                if (BaconBuiltObject.myMain != null && cargo1.CommodityIsResource)
                {
                    Cargo cargo2 = new Cargo(cargo1.Resource, cargo1.Amount, BaconBuiltObject.myMain._Game.PlayerEmpire);
                    cargoList.Add(cargo2);
                }
            }
            BaconBuiltObject.shipsToBeDestroyed.Add(shipToBeDestroyed.Name, cargoList);
        }

        public static void CollectScrapFromDestroyedBuiltObjects(
          BuiltObject attacker,
          BuiltObject target)
        {
            if (attacker != null && attacker.CargoSpace > 0 && attacker.BaconValues != null && attacker.BaconValues.ContainsKey("cash"))
            {
                WeaponList weapons = attacker.Weapons;
                int num1 = 0;
                float num2;
                if (weapons == null || weapons.Count == 0)
                {
                    num2 = 1f;
                    num1 = 1;
                }
                else
                {
                    int num3 = 0;
                    int num4 = 0;
                    foreach (Weapon weapon in (SyncList<Weapon>)weapons)
                    {
                        if (weapon.Component.Type == ComponentType.AssaultPod)
                            ++num4;
                        else
                            num3 += weapon.RawDamage;
                    }
                    int val2 = weapons.Count - num4;
                    num2 = (float)(num3 / Math.Max(1, val2));
                }
                float num5 = 10f;
                float num6 = num2 / (num2 + num5);
                BuiltObjectComponentList components = target.Components;
                ComponentList componentList = new ComponentList();
                Random random = new Random();
                foreach (Component component1 in (SyncList<BuiltObjectComponent>)components)
                {
                    float num7;
                    switch (component1.Type)
                    {
                        case ComponentType.WeaponBeam:
                        case ComponentType.WeaponTorpedo:
                        case ComponentType.WeaponMissile:
                        case ComponentType.WeaponPointDefense:
                        case ComponentType.WeaponIonCannon:
                        case ComponentType.WeaponIonPulse:
                        case ComponentType.WeaponIonDefense:
                        case ComponentType.WeaponTractorBeam:
                        case ComponentType.WeaponGravityBeam:
                        case ComponentType.WeaponAreaGravity:
                        case ComponentType.WeaponAreaDestruction:
                        case ComponentType.WeaponPhaser:
                        case ComponentType.WeaponRailGun:
                            num7 = 0.15f;
                            break;
                        case ComponentType.AssaultPod:
                        case ComponentType.HyperDeny:
                        case ComponentType.HyperStop:
                        case ComponentType.Armor:
                        case ComponentType.Shields:
                        case ComponentType.ShieldRecharge:
                        case ComponentType.HyperDrive:
                        case ComponentType.StorageFuel:
                        case ComponentType.StorageCargo:
                        case ComponentType.SensorResourceProfileSensor:
                        case ComponentType.SensorLongRange:
                        case ComponentType.SensorScannerJammer:
                        case ComponentType.SensorStealth:
                        case ComponentType.ComputerTargetting:
                        case ComponentType.ComputerCountermeasures:
                        case ComponentType.DamageControl:
                            num7 = 0.18f;
                            break;
                        case ComponentType.EngineMainThrust:
                        case ComponentType.EngineVectoring:
                        case ComponentType.Reactor:
                        case ComponentType.EnergyCollector:
                        case ComponentType.EnergyToFuel:
                            num7 = 0.2f;
                            break;
                        default:
                            num7 = 0.1f;
                            break;
                    }
                    double num8 = random.NextDouble();
                    double num9 = random.NextDouble();
                    if ((double)num7 > 0.0099999997764825821 && num8 < (double)num7 && num9 > (double)num6)
                    {
                        Component component2 = new Component(component1.ComponentID);
                        componentList.Add(component2);
                    }
                }
                if (componentList.Count > 0)
                {
                    if (attacker.BaconValues == null)
                        attacker.BaconValues = new Dictionary<string, object>();
                    if (!attacker.BaconValues.ContainsKey("scrapComponents"))
                        attacker.BaconValues.Add("scrapComponents", (object)new List<Component>());
                    List<Component> baconValue = (List<Component>)attacker.BaconValues["scrapComponents"];
                    foreach (Component component in (SyncList<Component>)componentList)
                        baconValue.Add(component);
                    attacker.BaconValues["scrapComponents"] = (object)baconValue;
                }
                CargoList cargo1 = target.Cargo;
                if (BaconBuiltObject.shipsToBeDestroyed.ContainsKey(target.Name))
                    cargo1 = BaconBuiltObject.shipsToBeDestroyed[target.Name];
                CargoList cargoList = new CargoList();
                if (cargo1 != null && cargo1.Count > 0)
                {
                    foreach (Cargo cargo2 in (SyncList<Cargo>)cargo1)
                    {
                        if (cargo2.CommodityIsResource)
                        {
                            int amount = 0;
                            for (int index = 0; index < cargo2.Amount; ++index)
                            {
                                if (random.NextDouble() > (double)num6)
                                    ++amount;
                            }
                            if (amount > 0)
                                cargoList.Add(new Cargo(new Resource(cargo2.Resource.ResourceID), amount, attacker.ActualEmpire.EmpireId));
                        }
                    }
                }
                if (cargoList.Count > 0)
                {
                    foreach (Cargo cargo3 in (SyncList<Cargo>)cargoList)
                    {
                        Cargo cargo = cargo3;
                        Cargo cargo4 = attacker.Cargo.FirstOrDefault<Cargo>((Func<Cargo, bool>)(x => (int)x.Resource.ResourceID == (int)cargo.Resource.ResourceID));
                        if (cargo4 != null)
                            cargo4.Amount += cargo.Amount;
                        else
                            attacker.Cargo.Add(cargo);
                    }
                }
            }
            if (!BaconBuiltObject.shipsToBeDestroyed.ContainsKey(target.Name))
                return;
            BaconBuiltObject.shipsToBeDestroyed.Remove(target.Name);
        }

        public static void RevealIfPirate(Main main)
        {
            if (!(main._Game.SelectedObject is BuiltObject selectedObject) || selectedObject.ActualEmpire != main._Game.PlayerEmpire && main._Game.PlayerEmpire.Name.Contains("Romulan") || selectedObject.Empire == selectedObject.ActualEmpire)
                return;
            selectedObject.Empire = selectedObject.ActualEmpire;
            main._Game.PlayerEmpire.SendMessageToEmpire(main._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, selectedObject.Name + " has revealed itself as a pirate of " + selectedObject.ActualEmpire.Name);
        }

        public static void CheckFightersNeedUpgrading(BuiltObject carrier)
        {
            if (carrier != null && carrier.BaconValues != null && carrier.BaconValues.ContainsKey("customBomberName") || carrier.Fighters == null || carrier.Fighters.Count <= 0 || carrier.InBattle || carrier.Empire == null || carrier.Empire.Policy == null || carrier.Empire.Research == null || !carrier.Empire.Policy.ResearchDesignAutoUpgradeFighters)
                return;
            FighterSpecification fighterSpecification1 = carrier.Empire.Research.IdentifyLatestFighterSpecification();
            FighterSpecification fighterSpecification2 = carrier.Empire.Research.IdentifyLatestBomberSpecification();
            for (int index = 0; index < carrier.Fighters.Count; ++index)
            {
                if (carrier.Fighters[index].Specification != null && carrier.Fighters[index].Specification.Type == FighterType.Bomber)
                {
                    if (fighterSpecification2 != null && carrier.Fighters[index].Specification.FighterSpecificationId != fighterSpecification2.FighterSpecificationId)
                    {
                        if (!carrier.Fighters[index].OnboardCarrier && carrier.Fighters[index].MissionType != FighterMissionType.ReturnToCarrier)
                            carrier.Fighters[index].ReturnToCarrier();
                        else if (carrier.Fighters[index].OnboardCarrier)
                            carrier.Fighters[index].CompleteTeardown(carrier._Galaxy);
                    }
                }
                else if (carrier.Fighters[index].Specification != null && carrier.Fighters[index].Specification.Type == FighterType.Interceptor && fighterSpecification1 != null && carrier.Fighters[index].Specification.FighterSpecificationId != fighterSpecification1.FighterSpecificationId)
                {
                    if (!carrier.Fighters[index].OnboardCarrier && carrier.Fighters[index].MissionType != FighterMissionType.ReturnToCarrier)
                        carrier.Fighters[index].ReturnToCarrier();
                    else if (carrier.Fighters[index].OnboardCarrier)
                        carrier.Fighters[index].CompleteTeardown(carrier._Galaxy);
                }
            }
        }
    }
}
