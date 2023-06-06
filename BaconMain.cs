// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconMain
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Controls;
using DistantWorlds.Types;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BaconDistantWorlds
{
    public static class BaconMain
    {
        public static bool settingsInitialized = false;

        public static bool saveStats = true;

        public static short statSaveIntervalInGameDays = 90;

        public static double backgroundStarsAtZoomLevel = 300.0;

        public static string empireSaveStatsName;

        public static string pirateSaveStatsName;

        public static bool devMode = false;

        public static double lastTimeBuiltObjectPanelShowedNewShips = 0.0;

        public static bool drawWeaponsRange = true;

        public static bool drawWeaponRangeCircles = true;

        public static double minZoomLevelForWeaponsCircles = 0.9;

        public static prisonForm prisonForm;

        public static bool prisonFormOpen = false;

        public static bool shipViewerOpen = false;

        public static bool codeFormOpen = false;

        public static planetCargoDataForm tradeForm;

        public static CustomizeShipForm customizeShipForm;

        public static InvasionCommandForm invasionCommandForm;

        public static CustomBomberForm CustomBomberForm;

        public static bool tradeFormOpen = false;

        public static bool customizeShipFormOpen = false;

        public static bool invasionCommandFormOpen = false;

        public static bool customBomberFormOpen = false;

        public static double tradeTax = 0.1;

        public static int newIDCost = 10000;

        public static int baseShipOfficerCost = 5000;

        public static int componentEquipCost = 1000;

        public static bool useStargates = false;

        public static bool isDebugging = false;

        public static string debugLabel = "This is a long label";

        public static Func<Habitat, bool> debugFunc = null;

        public static int maximumResourceLevelToStockAtBaseNotAtColony = 50;

        public static int resourceFilter = -1;

        public static string expansionPlannerSelection = "colonies";

        public static float invasionStrategyResult = 1.5f;

        public static float invasionStrategyRemainingGuesses = 0.1f;

        public static bool useInvasionModifierReputation = true;

        public static int quartersOfCashAvailable = 4;

        public static double customDifficultyColonyCorruptionFactor = 1.0;

        public static double customDifficultyWarWearinessFactor = 1.0;

        public static double customDifficultyResearchRate = 1.0;

        public static double customDifficultyPopulationGrowthRate = 1.0;

        public static double customDifficultyMiningRate = 1.0;

        public static double customDifficultyTargettingFactor = 1.0;

        public static double customDifficultyCountermeasuresFactor = 1.0;

        public static double customDifficultyColonyShipBuildSpeedRate = 1.0;

        public static double customDifficultyColonyIncomeFactor = 1.0;

        public static List<string> baconValuesToCopyOnChangingCapital = new List<string> { "scientificData", "capturedSpies", "customBomberDesigns" };

        private static string outputLog = Environment.CurrentDirectory + "\\output_log.txt";

        private static ReaderWriterLock locker;

        public static string[] cmbBuiltObjectFilterStrings = new string[26]
        {
        "(" + TextResolver.GetText("Show all ships and bases") + ")",
        TextResolver.GetText("Selected Item"),
        TextResolver.GetText("Colony Ships"),
        TextResolver.GetText("Construction Yards"),
        TextResolver.GetText("Defensive Bases"),
        TextResolver.GetText("Exploration Ships"),
        TextResolver.GetText("Freighters"),
        TextResolver.GetText("Military Ships"),
        TextResolver.GetText("Mining Ships"),
        TextResolver.GetText("Mining Stations"),
        TextResolver.GetText("Monitoring Stations"),
        TextResolver.GetText("Passenger Ships"),
        TextResolver.GetText("Other Bases"),
        TextResolver.GetText("Research Stations"),
        TextResolver.GetText("Resort Bases"),
        TextResolver.GetText("Resupply Ships"),
        TextResolver.GetText("Space Ports"),
        TextResolver.GetText("Troop Carriers"),
        "Damaged Ships",
        "Low Fuel Ships",
        "Ships Under Attack",
        "Newest Ships",
        "State Versions",
        "Upgradable",
        "Free Traders",
        "Repeating Mission"
        };

        public static BaconEntryPoint EntryPointClass = null;

        public static void HandleToolstripClick(ShipAction action)
        {
            if (BaconBuiltObject.myMain == null)
            {
                return;
            }
            ShipActionType actionType = action.ActionType;
            if (actionType == ShipActionType.GiveBuiltObject)
            {
                if (!(action.Target is BuiltObject))
                {
                    return;
                }
                BuiltObject builtObject = action.Target as BuiltObject;
                Empire empire = builtObject.Empire;
                if (!empire.Name.Contains("Romulan") && !empire.Name.Contains("Mining Company"))
                {
                    return;
                }
                bool flag = empire.PirateEmpireBaseHabitat != null;
                if (empire == null)
                {
                    return;
                }
                int num = BaconBuiltObject.FindResalePriceOfShip(builtObject, action.Target2 as Empire);
                if (flag)
                {
                    empire.StateMoney += (double)num * Math.Max(1.0, empire.SmugglingIncomeFactor);
                }
                else
                {
                    empire.StateMoney += num;
                }
                if (!(action.Target2 is Empire))
                {
                    return;
                }
                Empire empire2 = action.Target2 as Empire;
                empire2.StateMoney -= num;
                if (!empire2.PreWarpProgressEventOccurredFirstHyperjump)
                {
                    return;
                }
                BuiltObjectComponent builtObjectComponent = builtObject.Components.FirstOrDefault((BuiltObjectComponent x) => x.Category == ComponentCategoryType.HyperDrive);
                if (builtObjectComponent == null)
                {
                    int componentID = -1;
                    ComponentList componentsHyperdriveOrderedByPower = Galaxy.ComponentsHyperdriveOrderedByPower;
                    if (componentsHyperdriveOrderedByPower != null && componentsHyperdriveOrderedByPower.Count >= 1)
                    {
                        componentID = componentsHyperdriveOrderedByPower[componentsHyperdriveOrderedByPower.Count - 1].ComponentID;
                    }
                    builtObject.Components.Add(new BuiltObjectComponent(componentID, ComponentStatus.Normal));
                }
                return;
            }
            if (action.Hint == "popup")
            {
                string extraData = action.ExtraData;
                MessageBoxEx messageBoxEx = MessageBoxExManager.CreateMessageBox(null, new Font("Verdana", 9f, FontStyle.Regular));
                messageBoxEx.Text = extraData;
                messageBoxEx.Caption = "Message";
                messageBoxEx.AddButton(MessageBoxExButtons.Ok);
                messageBoxEx.Icon = MessageBoxExIcon.None;
                if (BaconBuiltObject.myMain != null)
                {
                    bool flag2 = BaconBuiltObject.myMain._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                    if (!flag2)
                    {
                        BaconBuiltObject.myMain._Game.Galaxy.Pause();
                    }
                    string text = messageBoxEx.Show();
                    if (!flag2)
                    {
                        BaconBuiltObject.myMain._Game.Galaxy.Resume();
                    }
                }
                return;
            }
            if (action.Hint == "sleep")
            {
                if (!(action.Target is BuiltObject))
                {
                    return;
                }
                BuiltObject builtObject2 = (BuiltObject)action.Target;
                if (builtObject2.Empire != BaconBuiltObject.myMain._Game.PlayerEmpire && builtObject2.ActualEmpire != BaconBuiltObject.myMain._Game.PlayerEmpire && !BaconBuiltObject.myMain._Game.PlayerEmpire.Name.Contains("Romulan"))
                {
                    return;
                }
                if (builtObject2.BaconValues != null && builtObject2.BaconValues.ContainsKey("sleep"))
                {
                    builtObject2.BaconValues.Remove("sleep");
                    builtObject2.Owner = builtObject2.ActualEmpire;
                    if (builtObject2.BaconValues.Count == 0)
                    {
                        builtObject2.BaconValues = null;
                    }
                    return;
                }
                if (builtObject2.BaconValues == null)
                {
                    builtObject2.BaconValues = new Dictionary<string, object>();
                }
                builtObject2.BaconValues.Add("sleep", null);
                if (builtObject2.Mission != null)
                {
                    builtObject2.Mission.Clear();
                }
                builtObject2.Owner = null;
                return;
            }
            if (action.Hint == "asteroidColony")
            {
                BuiltObject ship = action.Target as BuiltObject;
                Habitat asteroid = action.Target2 as Habitat;
                BaconHabitat.DeployAsteroidColony(ship, asteroid);
                return;
            }
            if (action.Hint == "research")
            {
                try
                {
                    BuiltObject builtObject3 = action.Target as BuiltObject;
                    ResearchNode researchNode = null;
                    if (action.Target2 is ResearchNode)
                    {
                        researchNode = (ResearchNode)action.Target2;
                    }
                    if (researchNode != null)
                    {
                        if (builtObject3.BaconValues == null)
                        {
                            builtObject3.BaconValues = new Dictionary<string, object>();
                        }
                        switch (researchNode.Industry)
                        {
                            case IndustryType.Undefined:
                                break;
                            case IndustryType.Weapon:
                                if (builtObject3.BaconValues.ContainsKey("lab0"))
                                {
                                    builtObject3.BaconValues["lab0"] = researchNode;
                                }
                                else
                                {
                                    builtObject3.BaconValues.Add("lab0", researchNode);
                                }
                                break;
                            case IndustryType.Energy:
                                if (builtObject3.BaconValues.ContainsKey("lab1"))
                                {
                                    builtObject3.BaconValues["lab1"] = researchNode;
                                }
                                else
                                {
                                    builtObject3.BaconValues.Add("lab1", researchNode);
                                }
                                break;
                            case IndustryType.HighTech:
                                if (builtObject3.BaconValues.ContainsKey("lab2"))
                                {
                                    builtObject3.BaconValues["lab2"] = researchNode;
                                }
                                else
                                {
                                    builtObject3.BaconValues.Add("lab2", researchNode);
                                }
                                break;
                        }
                    }
                    return;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (action.Hint == "constructShip")
            {
                Habitat planet = action.Target as Habitat;
                Design design = action.Target2 as Design;
                BaconHabitat.BuildShipForPirate(planet, BaconBuiltObject.myMain._Game.PlayerEmpire, design);
            }
            else if (action.Hint == "missionExploreRuins")
            {
                BuiltObject builtObject4 = action.Target as BuiltObject;
                Habitat planet2 = action.Target2 as Habitat;
                Character character = builtObject4.Characters.FirstOrDefault((Character x) => x.Role == CharacterRole.ShipCaptain);
                if (character != null)
                {
                    BaconHabitat.BeginScientificMissionExploreRuins(builtObject4, character, planet2);
                }
            }
            else if (action.Hint == "missionProspectForResources")
            {
                BuiltObject builtObject5 = action.Target as BuiltObject;
                Habitat planet3 = action.Target2 as Habitat;
                Character explorer = builtObject5.Characters.FirstOrDefault((Character x) => x.Role == CharacterRole.ShipCaptain);
                BaconHabitat.BeginScientificMissionProspectForResources(builtObject5, explorer, planet3);
            }
            else if (action.Hint == "trade")
            {
                BuiltObject tradeShip = action.Target as BuiltObject;
                StellarObject shipOrPlanet = action.Target2 as StellarObject;
                if (!tradeFormOpen)
                {
                    tradeFormOpen = true;
                    tradeForm = new planetCargoDataForm(BaconBuiltObject.myMain, tradeShip, shipOrPlanet);
                }
            }
            else if (action.Hint == "recruitShipOfficer")
            {
                BuiltObject ship2 = action.Target as BuiltObject;
                Habitat location = action.Target2 as Habitat;
                BaconBuiltObject.RecruitShipOfficer(BaconBuiltObject.myMain, ship2, location);
            }
            else if (action.Hint == "customizeShip")
            {
                BuiltObject shipToCustomize = action.Target as BuiltObject;
                Habitat shipOrPlanet2 = action.Target2 as Habitat;
                if (!customizeShipFormOpen)
                {
                    customizeShipFormOpen = true;
                    customizeShipForm = new CustomizeShipForm(BaconBuiltObject.myMain, shipToCustomize, shipOrPlanet2);
                }
            }
            else if (action.Hint == "invasionCommand")
            {
                Habitat invasionPlanet = action.Target2 as Habitat;
                if (!invasionCommandFormOpen)
                {
                    invasionCommandFormOpen = true;
                    invasionCommandForm = new InvasionCommandForm(BaconBuiltObject.myMain, invasionPlanet);
                    invasionCommandForm.ShowDialog();
                }
            }
        }

        public static void AddMoreBuildableTypes(List<BuiltObjectSubRole> subRoles)
        {
            subRoles.Add(BuiltObjectSubRole.ConstructionShip);
        }

        public static void DrawGravityWellRange(SpriteBatch spriteBatch_2, int num14, int num16, double double_15)
        {
            if (BaconBuiltObject.myMain == null)
            {
                return;
            }
            Main myMain = BaconBuiltObject.myMain;
            if (!BaconBuiltObject.useStarGravityWells || myMain._Game.SelectedObject == null)
            {
                return;
            }
            BuiltObject builtObject = null;
            double num17 = 0.0;
            if (myMain._Game.SelectedObject is BuiltObject)
            {
                builtObject = (BuiltObject)myMain._Game.SelectedObject;
            }
            else if (myMain._Game.SelectedObject is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)myMain._Game.SelectedObject;
                builtObject = shipGroup.LeadShip;
            }
            if (builtObject != null && builtObject.Role != BuiltObjectRole.Base && (builtObject.ActualEmpire == myMain._Game.PlayerEmpire || BaconBuiltObject.myMain._Game.PlayerEmpire.Name.Contains("Romulan")) && builtObject.NearestSystemStar != null)
            {
                num17 = CalculateGravityWellSize(myMain, builtObject);
                if (!(num17 <= 0.0))
                {
                    double num18 = builtObject.NearestSystemStar.Xpos - num17;
                    double num19 = builtObject.NearestSystemStar.Ypos - num17;
                    float num20 = (float)((num18 - (double)num14) / double_15);
                    float num21 = (float)((num19 - (double)num16) / double_15);
                    float num22 = (float)(num17 / double_15 * 2.0);
                    new RectangleF(num20, num21, num22, num22);
                    XnaDrawingHelper.DrawCircle(spriteBatch_2, num20, num21, num22, num22, Color.Blue, 1, 200, true);
                }
            }
        }

        public static void DrawWeaponRanges(SpriteBatch spriteBatch_2, int num14, int num16, double zoomLevel)
        {
            if (!drawWeaponRangeCircles || BaconBuiltObject.myMain == null)
            {
                return;
            }
            Main myMain = BaconBuiltObject.myMain;
            if (myMain._Game.SelectedObject == null || !(myMain._Game.SelectedObject is BuiltObject))
            {
                return;
            }
            BuiltObject builtObject = (BuiltObject)myMain._Game.SelectedObject;
            Color color = Color.Beige;
            if (builtObject.Weapons.Count > 0)
            {
                List<Weapon> list = (from x in builtObject.Weapons
                                     group x by x.Component.Type into g
                                     select g.First()).ToList();
                foreach (Weapon item in list)
                {
                    if (!item.Component.Name.Contains("Assault Pod"))
                    {
                        if (item.Component.Type == ComponentType.WeaponBeam)
                        {
                            color = Color.Blue;
                        }
                        else if (item.Component.Type == ComponentType.WeaponMissile)
                        {
                            color = Color.Yellow;
                        }
                        else if (item.Component.Type == ComponentType.WeaponTorpedo)
                        {
                            color = Color.Red;
                        }
                        else if (item.Component.Type == ComponentType.WeaponIonPulse)
                        {
                            color = Color.Green;
                        }
                        else if (item.Component.Type == ComponentType.WeaponIonCannon)
                        {
                            color = Color.Ivory;
                        }
                        else if (item.Component.Type == ComponentType.WeaponGravityBeam)
                        {
                            color = Color.Orange;
                        }
                        else if (item.Component.Type == ComponentType.WeaponPhaser)
                        {
                            color = Color.Aqua;
                        }
                        else if (item.Component.Type == ComponentType.WeaponRailGun)
                        {
                            color = Color.Aquamarine;
                        }
                        int range = item.Range;
                        double num17 = builtObject.Xpos - (double)range;
                        double num18 = builtObject.Ypos - (double)range;
                        float num19 = (float)((num17 - (double)num14) / zoomLevel);
                        float num20 = (float)((num18 - (double)num16) / zoomLevel);
                        float num21 = (float)((double)range / zoomLevel * 2.0);
                        XnaDrawingHelper.DrawCircle(spriteBatch_2, num19, num20, num21, num21, color, 1, 200, false);
                    }
                }
            }
            if (builtObject.Fighters == null || builtObject.Fighters.Count <= 0 || !(zoomLevel > 7.76))
            {
                return;
            }
            List<Fighter> list2 = (from x in builtObject.Fighters
                                   group x by x.Specification into g
                                   select g.First()).ToList();
            foreach (Fighter item2 in list2)
            {
                if (item2.Specification.Type == FighterType.Interceptor)
                {
                    color = Color.Silver;
                }
                else if (item2.Specification.Type == FighterType.Bomber)
                {
                    color = Color.Gold;
                }
                double num22 = Math.Sqrt(Math.Round(BaconFighter.CalculateMaximumTargetRange(item2), 2));
                double num23 = builtObject.Xpos - num22;
                double num24 = builtObject.Ypos - num22;
                float num25 = (float)((num23 - (double)num14) / zoomLevel);
                float num26 = (float)((num24 - (double)num16) / zoomLevel);
                float num27 = (float)(num22 / zoomLevel * 2.0);
                XnaDrawingHelper.DrawCircle(spriteBatch_2, num25, num26, num27, num27, color, 1, 200, false);
            }
        }

        public static double CalculateGravityWellSize(Main mian, BuiltObject ship)
        {
            double gravityWellReductionForSmallShip = BaconBuiltObject.GetGravityWellReductionForSmallShip(ship);
            Habitat nearestSystemStar = ship.NearestSystemStar;
            if (nearestSystemStar == null)
            {
                return 0.0;
            }
            double gravityWellMitigationForHyperDrive = BaconBuiltObject.GetGravityWellMitigationForHyperDrive(ship);
            int num = nearestSystemStar.SolarRadiation + nearestSystemStar.MicrowaveRadiation + nearestSystemStar.XrayRadiation;
            return Math.Sqrt(BaconBuiltObject.starGravityWellRangeSquared) * ((double)num / 100.0) * gravityWellReductionForSmallShip * gravityWellMitigationForHyperDrive;
        }

        public static Game OverrideGalaxySetup(Start start, Game game)
        {
            return game;
        }

        public static void BaconInitialize(Main main)
        {
            if (settingsInitialized)
            {
                return;
            }
            BaconBuiltObject.myMain = main;
            Galaxy.MinimumHabitatPopulationAmount = 100L;
            empireSaveStatsName = "SaveStatsEmpire" + main._Game.Galaxy.RandomSeed + ".xml";
            pirateSaveStatsName = "SaveStatsPirates" + main._Game.Galaxy.RandomSeed + ".xml";
            if (!File.Exists(empireSaveStatsName))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlNode documentElement = xmlDocument.DocumentElement;
                    XmlElement newChild = xmlDocument.CreateElement("GameStats");
                    if (documentElement != null)
                    {
                        documentElement.AppendChild(newChild);
                    }
                    else
                    {
                        xmlDocument.AppendChild(newChild);
                    }
                    xmlDocument.Save(empireSaveStatsName);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (!File.Exists(pirateSaveStatsName))
            {
                try
                {
                    XmlDocument xmlDocument2 = new XmlDocument();
                    XmlNode documentElement2 = xmlDocument2.DocumentElement;
                    XmlElement newChild2 = xmlDocument2.CreateElement("GameStats");
                    if (documentElement2 != null)
                    {
                        documentElement2.AppendChild(newChild2);
                    }
                    else
                    {
                        xmlDocument2.AppendChild(newChild2);
                    }
                    xmlDocument2.Save(pirateSaveStatsName);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            Dictionary<string, string> dictionary = ReadBaconSettings();
            if (dictionary.TryGetValue("HyperJumpThreshhold", out var value) && int.TryParse(value, out var result))
            {
                Galaxy.HyperJumpThreshhold = result;
            }
            if (dictionary.TryGetValue("BaseHyperJumpAccuracy", out value) && int.TryParse(value, out result))
            {
                Galaxy.BaseHyperJumpAccuracy = result;
            }
            if (dictionary.TryGetValue("TroopGarrisonMinimumPerColony", out value) && int.TryParse(value, out result))
            {
                main._Game.Galaxy.IndependentEmpire.Policy.TroopGarrisonMinimumPerColony = result;
            }
            if (dictionary.TryGetValue("useStarGravityWells", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result2 = false;
                if (bool.TryParse(value, out result2))
                {
                    BaconBuiltObject.useStarGravityWells = result2;
                }
            }
            if (dictionary.TryGetValue("smallShipsJumpSooner", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result3 = false;
                if (bool.TryParse(value, out result3))
                {
                    BaconBuiltObject.smallShipsJumpSooner = result3;
                }
            }
            if (dictionary.TryGetValue("priceReductionFactor", out value) && int.TryParse(value, out result))
            {
                BaconGalaxy.priceReductionFactor = result;
            }
            if (dictionary.TryGetValue("sublightFuelBurnDivisor", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result4))
            {
                BaconBuiltObject.sublightFuelBurnDivisor = result4;
            }
            if (dictionary.TryGetValue("fighterRangeMultiple", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result5))
            {
                BaconFighter.fighterRangeMultiple = result5;
            }
            if (dictionary.TryGetValue("ammoExhaustChanceMissile", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconFighter.ammoExhaustChanceMissile = result4;
            }
            if (dictionary.TryGetValue("ammoExhaustChanceTorpedo", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconFighter.ammoExhaustChanceTorpedo = result4;
            }
            if (dictionary.TryGetValue("fighterBuildCost", out value) && int.TryParse(value, out result))
            {
                BaconFighter.fighterBuildCost = result;
            }
            if (dictionary.TryGetValue("fighterBuildSpeedDivisor", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconFighter.fighterBuildSpeedDivisor = result4;
            }
            if (dictionary.TryGetValue("Shadow", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result6 = false;
                if (bool.TryParse(value, out result6))
                {
                    BaconInfoPanel.shadow = result6;
                }
            }
            if (dictionary.TryGetValue("saveStats", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result7 = false;
                if (bool.TryParse(value, out result7))
                {
                    saveStats = result7;
                }
            }
            if (dictionary.TryGetValue("saveInterval", out value) && short.TryParse(value, out var result8))
            {
                if (result8 < 10)
                {
                    result8 = 10;
                }
                statSaveIntervalInGameDays = result8;
            }
            if (saveStats)
            {
                EventActionExecutionPackageList delayedActions = main._Game.Galaxy.DelayedActions;
                EventActionExecutionPackage eventActionExecutionPackage = delayedActions.FirstOrDefault((EventActionExecutionPackage x) => x.Action.MessageTitle.Contains("SaveStats"));
                if (eventActionExecutionPackage == null)
                {
                    EventAction eventAction = new EventAction(null, EventActionType.StartPlague);
                    eventAction.MessageTitle = "SaveStats";
                    eventAction.ExecutionDate = main._Game.Galaxy.CurrentStarDate + Galaxy.RealSecondsInGalacticYear * 1000 / 360;
                    GameEvent gameEvent = new GameEvent(main._Game.Galaxy, 0, null);
                    eventActionExecutionPackage = new EventActionExecutionPackage(eventAction, gameEvent, main._Game.PlayerEmpire);
                    main._Game.Galaxy.DelayedActions.Add(eventActionExecutionPackage);
                }
            }
            if (dictionary.TryGetValue("researchPerLab", out value) && int.TryParse(value, out result))
            {
                BaconEmpire.researchPerLab = result;
            }
            if (BaconEmpire.researchPerLab > 0f)
            {
                EventActionExecutionPackageList delayedActions2 = main._Game.Galaxy.DelayedActions;
                EventActionExecutionPackage eventActionExecutionPackage2 = delayedActions2.FirstOrDefault((EventActionExecutionPackage x) => x.Action.MessageTitle.Contains("ProcessEmpireScienceShips"));
                if (eventActionExecutionPackage2 == null)
                {
                    EventAction eventAction2 = new EventAction(null, EventActionType.StartPlague);
                    eventAction2.MessageTitle = "ProcessEmpireScienceShips";
                    eventAction2.ExecutionDate = main._Game.Galaxy.CurrentStarDate + Galaxy.RealSecondsInGalacticYear * 1000 / 360 * Galaxy.Rnd.Next(26, 35);
                    GameEvent gameEvent2 = new GameEvent(main._Game.Galaxy, 0, null);
                    eventActionExecutionPackage2 = new EventActionExecutionPackage(eventAction2, gameEvent2, main._Game.PlayerEmpire);
                    main._Game.Galaxy.DelayedActions.Add(eventActionExecutionPackage2);
                }
            }
            if (dictionary.TryGetValue("fighterOnBomberViolence", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconFighter.fighterOnBomberDamageMultiplier = result4;
            }
            if (dictionary.TryGetValue("backgroundStarsAtZoomLevel", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                backgroundStarsAtZoomLevel = result5;
            }
            if (dictionary.TryGetValue("lowStarCount", out value) && int.TryParse(value, out result))
            {
                result = (BaconStart.lowStarCount = Math.Max(Math.Min(100, result), 10));
            }
            if (dictionary.TryGetValue("alwaysShowAsteroidColonies", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result9 = false;
                if (bool.TryParse(value, out result9))
                {
                    BaconHabitat.alwaysShowAsteroidColonies = result9;
                }
            }
            if (dictionary.TryGetValue("allowAsteroidColonies", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result10 = false;
                if (bool.TryParse(value, out result10))
                {
                    BaconHabitat.allowAsteroidColonies = result10;
                }
            }
            if (dictionary.TryGetValue("asteroidColonyCost", out value) && int.TryParse(value, out result))
            {
                BaconHabitat.asteroidColonyCost = result;
            }
            if (dictionary.TryGetValue("scientificDataForResourceSurvey", out value) && int.TryParse(value, out result))
            {
                BaconBuiltObject.scientificDataForResourceSurvey = result;
            }
            if (dictionary.TryGetValue("scientificDataForRuins", out value) && int.TryParse(value, out result))
            {
                BaconBuiltObject.scientificDataForRuins = result;
            }
            if (dictionary.TryGetValue("tradeEverything", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result11 = false;
                if (bool.TryParse(value, out result11))
                {
                    BaconGalaxy.tradeEverything = result11;
                }
            }
            if (dictionary.TryGetValue("shipMaintenanceCostPerSizeUnit", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                Galaxy.ShipMaintenanceCostPerSizeUnit = result4;
            }
            if (dictionary.TryGetValue("shipMarkupFactor", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                Galaxy.ShipMarkupFactor = result4;
            }
            if (dictionary.TryGetValue("shipMarkupFactorPirates", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                Galaxy.ShipMarkupFactorPirates = result4;
            }
            if (dictionary.TryGetValue("warWearinessMax", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                Galaxy.WarWearinessMaximum = result4;
            }
            if (dictionary.TryGetValue("asteroidColonyPrevalenceDivisor", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconHabitat.asteroidColonyPrevalenceDivisor = result4;
            }
            if (dictionary.TryGetValue("lowIndependentLifeValue", out value) && int.TryParse(value, out result))
            {
                BaconStart.lowIndependentLifeValue = result;
            }
            if (dictionary.TryGetValue("warWearinessReduction", out value) && int.TryParse(value, out result))
            {
                BaconEmpire.warWearinessReduction = result;
            }
            if (dictionary.TryGetValue("spyCaptureChance", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconCharacter.spyCaptureChance = result4;
            }
            if (dictionary.TryGetValue("capturedSpyEscapeChance", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconCharacter.spyBaseEscapeChance = result4;
            }
            if (dictionary.TryGetValue("capturedSpyDefectChance", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconCharacter.spyBaseDefectChance = result4;
            }
            if (dictionary.TryGetValue("spyBaseValue", out value) && int.TryParse(value, out result))
            {
                BaconCharacter.spyBaseValue = result;
            }
            if (dictionary.TryGetValue("tradeTax", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                tradeTax = result5;
            }
            if (dictionary.TryGetValue("SubjugationTributePercentage", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                BaconEmpire.SubjugationTributePercentage = result5;
                Galaxy.SubjugationTributePercentage = result5;
            }
            if (dictionary.TryGetValue("weaponRangeMultiplierForBases", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                BaconBuiltObject.weaponRangeMultiplierForBases = result4;
            }
            if (dictionary.TryGetValue("allowInfrastructureImprovements", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result12 = false;
                if (bool.TryParse(value, out result12))
                {
                    BaconHabitat.allowInfrastructureImprovements = result12;
                }
            }
            if (dictionary.TryGetValue("infrastructureSpendingPerDevelopmentLevel", out value) && int.TryParse(value, out result))
            {
                if (result < 10000)
                {
                    result = 10000;
                }
                BaconHabitat.infrastructureSpendingPerDevelopmentLevel = result;
            }
            if (dictionary.TryGetValue("maxInfrastructureInvestmentAllowed", out value) && int.TryParse(value, out result))
            {
                if (result < 10000)
                {
                    result = 10000;
                }
                BaconHabitat.maxInfrastructureInvestmentAllowed = result;
            }
            if (dictionary.TryGetValue("infrastuctureDurability", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                result4 = (BaconHabitat.infrasetuctureDurability = Math.Min(Math.Max(result4, 0.1f), 1f));
            }
            if (dictionary.TryGetValue("marketPriceUpdateChance", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                BaconHabitat.marketPriceUpdateChance = result5;
            }
            if (dictionary.TryGetValue("maximumResourceLevelToStockAtBaseNotAtColony", out value) && int.TryParse(value, out result))
            {
                maximumResourceLevelToStockAtBaseNotAtColony = result;
            }
            if (dictionary.TryGetValue("noFuelCruiseSpeedMultiplier", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.CurrentUICulture, out result4))
            {
                result4 = (BaconBuiltObject.noFuelCruiseSpeedMultiplier = Math.Min(Math.Max(result4, 0.1f), 1f));
            }
            if (dictionary.TryGetValue("noFuelTopSpeedMultiplier", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.CurrentUICulture, out result4))
            {
                result4 = (BaconBuiltObject.noFuelTopSpeedMultiplier = Math.Min(Math.Max(result4, 0.1f), 1f));
                if (BaconBuiltObject.noFuelTopSpeedMultiplier < BaconBuiltObject.noFuelCruiseSpeedMultiplier)
                {
                    BaconBuiltObject.noFuelTopSpeedMultiplier = BaconBuiltObject.noFuelCruiseSpeedMultiplier;
                }
            }
            if (dictionary.TryGetValue("noFuelHyperSpeedMultiplier", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.CurrentUICulture, out result4))
            {
                result4 = (BaconBuiltObject.noFuelHyperSpeedMultiplier = Math.Min(Math.Max(result4, 0.1f), 1f));
            }
            if (dictionary.TryGetValue("pirateControlLevelToBuildShipsAtIndependentPlanets", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                result4 = (BaconHabitat.pirateControlLevelToBuildShipsAtIndependentPlanets = Math.Min(Math.Max(result4, 0.1f), 1f));
            }
            if (dictionary.TryGetValue("shipFreeRepairTimeFromCrewSkillAverage", out value) && int.TryParse(value, out result))
            {
                BaconBuiltObject.shipFreeRepairTimeFromCrewSkillAverage = result;
            }
            if (dictionary.TryGetValue("shipFreeRepairTimeFromCrewSkillExperienced", out value) && int.TryParse(value, out result))
            {
                BaconBuiltObject.shipFreeRepairTimeFromCrewSkillExperienced = result;
            }
            if (dictionary.TryGetValue("shipFreeRepairTimeFromCrewSkillVeteran", out value) && int.TryParse(value, out result))
            {
                BaconBuiltObject.shipFreeRepairTimeFromCrewSkillVeteran = result;
            }
            if (dictionary.TryGetValue("shipFreeRepairTimeFromCrewSkillElite", out value) && int.TryParse(value, out result))
            {
                BaconBuiltObject.shipFreeRepairTimeFromCrewSkillElite = result;
            }
            if (dictionary.TryGetValue("shipFreeRepairTimeFromCrewSkillLegendary", out value) && int.TryParse(value, out result))
            {
                BaconBuiltObject.shipFreeRepairTimeFromCrewSkillLegendary = result;
            }
            if (dictionary.TryGetValue("fighterBayLabel", out value))
            {
                BaconBuiltObject.fighterBayLabel = value.ToLower();
            }
            if (dictionary.TryGetValue("bomberBayLabel", out value))
            {
                BaconBuiltObject.bomberBayLabel = value.ToLower();
            }
            if (dictionary.TryGetValue("mixedBayLabel", out value))
            {
                BaconBuiltObject.mixedBayLabel = value.ToLower();
            }
            if (dictionary.TryGetValue("pointDefenseAffectsMissiles", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result13 = false;
                if (bool.TryParse(value, out result13))
                {
                    BaconBuiltObject.pointDefenseAffectsMissiles = result13;
                }
            }
            if (dictionary.TryGetValue("orbitalAsteroidCost", out value) && int.TryParse(value, out result))
            {
                BaconBuiltObject.orbitalAsteroidCost = result;
            }
            if (dictionary.TryGetValue("privateBuildCostToStateMoney", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (BaconBuiltObject.privateBuildCostToStateMoney = Math.Max(0.0, Math.Min(1.0, result5)));
            }
            if (dictionary.TryGetValue("addSalesTax", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result14 = false;
                if (bool.TryParse(value, out result14))
                {
                    BaconHabitat.addSalesTax = result14;
                }
            }
            if (dictionary.TryGetValue("newIDCost", out value) && int.TryParse(value, out result))
            {
                newIDCost = result;
            }
            if (dictionary.TryGetValue("baseShipOfficerCost", out value) && int.TryParse(value, out result))
            {
                baseShipOfficerCost = result;
            }
            if (dictionary.TryGetValue("componentEquipCost", out value) && int.TryParse(value, out result))
            {
                componentEquipCost = result;
            }
            if (dictionary.TryGetValue("invasionStrategyResult", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                invasionStrategyResult = result4;
            }
            if (dictionary.TryGetValue("invasionStrategyRemainingGuesses", out value) && float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result4))
            {
                invasionStrategyRemainingGuesses = Math.Max(0f, result4);
            }
            if (dictionary.TryGetValue("useInvasionModifierReputation", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result15 = false;
                if (bool.TryParse(value, out result15))
                {
                    useInvasionModifierReputation = result15;
                }
            }
            if (dictionary.TryGetValue("quartersOfCashAvailable", out value) && int.TryParse(value, out result))
            {
                quartersOfCashAvailable = result;
            }
            if (dictionary.TryGetValue("limitNewFighterBuildToColonies", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result16 = false;
                if (bool.TryParse(value, out result16))
                {
                    BaconBuiltObject.limitNewFighterBuildToColonies = result16;
                }
            }
            if (dictionary.TryGetValue("tailGunnerReasearch", out value))
            {
                BaconBuiltObject.tailGunnerResearch = value;
            }
            if (dictionary.TryGetValue("useStargates", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result17 = false;
                if (bool.TryParse(value, out result17))
                {
                    useStargates = result17;
                }
            }
            if (dictionary.TryGetValue("pirateBaseTroops", out value) && int.TryParse(value, out result))
            {
                BaconHabitat.pirateBaseTroops = result;
            }
            if (dictionary.TryGetValue("pirateFortressTroops", out value) && int.TryParse(value, out result))
            {
                BaconHabitat.pirateFortressTroops = result;
            }
            if (dictionary.TryGetValue("pirateCriminalNetworkTroops", out value) && int.TryParse(value, out result))
            {
                BaconHabitat.pirateCriminalNetworkTroops = result;
            }
            if (dictionary.TryGetValue("showRangeCircles", out value) && (value.Trim() == "true" || value == "false"))
            {
                bool result18 = false;
                if (bool.TryParse(value, out result18))
                {
                    if (result18)
                    {
                        drawWeaponRangeCircles = true;
                        minZoomLevelForWeaponsCircles = 0.9;
                    }
                    else
                    {
                        drawWeaponRangeCircles = false;
                        minZoomLevelForWeaponsCircles = 5.0;
                    }
                }
            }
            if (dictionary.TryGetValue("pirateMaxPopulationInfluence", out value) && long.TryParse(value, out var result19))
            {
                BaconHabitat.pirateMaxPopulationInfluence = result19;
            }
            if (dictionary.TryGetValue("customDifficultyColonyCorruptionFactor", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyColonyCorruptionFactor = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            if (dictionary.TryGetValue("customDifficultyWarWearinessFactor", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyWarWearinessFactor = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            if (dictionary.TryGetValue("customDifficultyResearchRate", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyResearchRate = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            if (dictionary.TryGetValue("customDifficultyPopulationGrowthRate", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyPopulationGrowthRate = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            if (dictionary.TryGetValue("customDifficultyMiningRate", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyMiningRate = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            if (dictionary.TryGetValue("customDifficultyTargettingFactor", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyTargettingFactor = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            if (dictionary.TryGetValue("customDifficultyCountermeasuresFactor", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyCountermeasuresFactor = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            if (dictionary.TryGetValue("customDifficultyColonyShipBuildSpeedRate", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyColonyShipBuildSpeedRate = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            if (dictionary.TryGetValue("customDifficultyColonyIncomeFactor", out value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result5))
            {
                result5 = (customDifficultyColonyIncomeFactor = Math.Max(0.01, Math.Min(10.0, result5)));
            }
            main.cmbBuiltObjectFilter.Items.Clear();
            ComboBox.ObjectCollection items = main.cmbBuiltObjectFilter.Items;
            object[] items2 = cmbBuiltObjectFilterStrings;
            items.AddRange(items2);
            lastTimeBuiltObjectPanelShowedNewShips = main._Game.Galaxy.CurrentStarDate;
            AddOtherDelayedEvents(main);
            ModAllShips(main);
            BaconDesign.RedefineAllBases(main);
            settingsInitialized = true;
        }

        public static void AddOtherDelayedEvents(Main main)
        {
            EventActionExecutionPackageList delayedActions = main._Game.Galaxy.DelayedActions;
            EventActionExecutionPackage eventActionExecutionPackage = delayedActions.FirstOrDefault((EventActionExecutionPackage x) => x.Action.MessageTitle.Contains("ClearShipsAboutToBeDestroyed"));
            if (eventActionExecutionPackage == null)
            {
                EventAction eventAction = new EventAction(null, EventActionType.StartPlague);
                eventAction.MessageTitle = "ClearShipsAboutToBeDestroyed";
                eventAction.ExecutionDate = main._Game.Galaxy.CurrentStarDate + Galaxy.RealSecondsInGalacticYear * 1000 / 360 * Galaxy.Rnd.Next(10, 12);
                GameEvent gameEvent = new GameEvent(main._Game.Galaxy, 0, null);
                eventActionExecutionPackage = new EventActionExecutionPackage(eventAction, gameEvent, main._Game.PlayerEmpire);
                main._Game.Galaxy.DelayedActions.Add(eventActionExecutionPackage);
            }
        }

        public static void ModAllShips(Main main)
        {
            foreach (BuiltObject builtObject in main._Game.Galaxy.BuiltObjects)
            {
                if (builtObject != null && builtObject.Role == BuiltObjectRole.Military)
                {
                    BaconBuiltObject.ApplyCrewExperience(builtObject);
                }
            }
        }

        public static Dictionary<string, string> ReadBaconSettings()
        {
            List<string> list = new List<string>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                using StreamReader streamReader = new StreamReader("BaconSettings.txt", Encoding.Default);
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    if (!text.StartsWith("//") && !string.IsNullOrEmpty(text))
                    {
                        string[] array = text.Split('=');
                        string key = array[0];
                        string value = array[array.Length - 1];
                        dictionary.Add(key, value);
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex2)
            {
                string stackTrace = ex2.StackTrace;
            }
            return dictionary;
        }

        public static Color SetColorForDiplomacyBackground(Empire empire)
        {
            BaconBuiltObject.targetEmpireForSpy = empire;
            Color mainColor = empire.MainColor;
            int red = (int)mainColor.R / 2;
            int green = (int)mainColor.G / 2;
            int blue = (int)mainColor.B / 2;
            Color result = Color.FromArgb(red, green, blue);
            if (BaconInfoPanel.shadow)
            {
                return mainColor;
            }
            return result;
        }

        public static void ProcessGameStats(Main main)
        {
            List<string> list = new List<string>();
            list.Add("State Money");
            list.Add("Private Money");
            list.Add("Income");
            list.Add("Military Strength");
            list.Add("Tech Researched");
            list.Add("Population");
            list.Add("Economy");
            list.Add("Happiness");
            list.Add("Resources Mined");
            list.Add("Ships Destroyed");
            list.Add("Ships Lost");
            List<string> list2 = new List<string>();
            list2.Add("State Money");
            list2.Add("Income");
            list2.Add("Military Strength");
            list2.Add("Tech Researched");
            List<Empire> empireList = main._Game.Galaxy.Empires.ToList();
            ProcessGameStats(main, empireList, list, empireSaveStatsName);
            List<Empire> empireList2 = main._Game.Galaxy.PirateEmpires.ToList();
            ProcessGameStats(main, empireList2, list2, pirateSaveStatsName);
        }

        public static void ProcessGameStats(Main main, List<Empire> empireList, List<string> statsToTrack, string saveFilename)
        {
            long num = main._Game.Galaxy.CurrentStarDate - main._Game.Galaxy.ActualStartDate;
            int num2 = Galaxy.RealSecondsInGalacticYear * 1000;
            string innerText = $"{(double)num / (double)num2:0.00}";
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(saveFilename);
                XmlNode documentElement = xmlDocument.DocumentElement;
                XmlNode xmlNode = null;
                foreach (string item in statsToTrack)
                {
                    xmlNode = null;
                    foreach (XmlNode childNode in xmlDocument.ChildNodes[0].ChildNodes)
                    {
                        if (childNode.Attributes.Count > 0 && childNode.Attributes[0].InnerText == item)
                        {
                            xmlNode = childNode;
                            break;
                        }
                    }
                    if (xmlNode == null)
                    {
                        XmlElement xmlElement = xmlDocument.CreateElement("GameStat");
                        xmlElement.SetAttribute("name", item);
                        xmlNode = xmlElement;
                    }
                    XmlNode xmlNode3 = xmlNode;
                    XmlNode xmlNode4 = null;
                    foreach (Empire empire in empireList)
                    {
                        xmlNode4 = null;
                        foreach (XmlNode item2 in xmlNode3)
                        {
                            if (item2.Attributes != null && item2.Attributes[0].InnerText == empire.Name)
                            {
                                xmlNode4 = item2;
                                break;
                            }
                        }
                        if (xmlNode4 == null)
                        {
                            XmlElement xmlElement2 = xmlDocument.CreateElement("SpaceEmpire");
                            xmlElement2.SetAttribute("name", empire.Name);
                            xmlNode4 = xmlElement2;
                        }
                        XmlNode xmlNode6 = xmlNode4;
                        XmlElement xmlElement3 = xmlDocument.CreateElement("TimeAndValue");
                        XmlElement xmlElement4 = xmlDocument.CreateElement("TimeAndValue");
                        XmlElement xmlElement5 = xmlDocument.CreateElement("Time");
                        xmlElement5.InnerText = innerText;
                        XmlElement xmlElement6 = xmlDocument.CreateElement("Value");
                        xmlElement6.InnerText = FindValueForThisNode(item, empire);
                        xmlElement4.AppendChild(xmlElement5);
                        xmlElement4.AppendChild(xmlElement6);
                        xmlNode6.AppendChild(xmlElement4);
                        xmlNode.AppendChild(xmlNode6);
                        xmlDocument.DocumentElement.AppendChild(xmlNode);
                    }
                }
                xmlDocument.Save(saveFilename);
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex2)
            {
                string stackTrace = ex2.StackTrace;
            }
        }

        public static string FindValueForThisNode(string stat, Empire empire)
        {
            string result = "";
            switch (stat)
            {
                case "State Money":
                    result = Math.Round(empire.StateMoney).ToString(CultureInfo.InvariantCulture);
                    break;
                case "Private Money":
                    result = Math.Round(empire.PrivateMoney).ToString(CultureInfo.InvariantCulture);
                    break;
                case "Income":
                    result = empire.CalculateAccurateAnnualIncome().ToString(CultureInfo.InvariantCulture);
                    break;
                case "Military Strength":
                    result = empire.MilitaryPotency.ToString(CultureInfo.InvariantCulture);
                    break;
                case "Tech Researched":
                    result = empire.Research.TechTree.CalculateTotalCostResearchedProjects().ToString(CultureInfo.InvariantCulture);
                    break;
                case "Population":
                    result = empire.TotalPopulation.ToString(CultureInfo.InvariantCulture);
                    break;
                case "Economy":
                    result = empire.PrivateAnnualRevenue.ToString(CultureInfo.InvariantCulture);
                    break;
                case "Happiness":
                    result = empire.AverageHappiness().ToString(CultureInfo.InvariantCulture);
                    break;
                case "Resources Mined":
                    result = (empire.Counters.MiningExtractionGas + empire.Counters.MiningExtractionLuxury + empire.Counters.MiningExtractionStrategic).ToString(CultureInfo.InvariantCulture);
                    break;
                case "Ships Destroyed":
                    result = (empire.Counters.DestroyedEnemyCivilianShipCount + empire.Counters.DestroyedEnemyCivilianShipSize).ToString(CultureInfo.InvariantCulture);
                    break;
                case "Ships Lost":
                    result = (empire.Counters.LossesMilitaryShipCount + empire.Counters.LossesCivilianShipCount).ToString(CultureInfo.InvariantCulture);
                    break;
            }
            return result;
        }

        public static void CatchMethod_78(MainView mv)
        {
        }

        public static void GenerateToolStripItems(Main main)
        {
            //IL_005e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0068: Expected O, but got Unknown
            Point point = ((Control)(object)main).PointToClient(MouseHelper.GetCursorPosition());
            object obj = main.method_143(main.int_15, main.int_16, true);
            Keyboard keyboard = new Keyboard();
            if (!keyboard.AltKeyDown)
            {
                return;
            }
            object obj2 = null;
            main.actionMenu.Items.Clear();
            ShipAction shipAction = null;
            main.actionMenu.Renderer = (ToolStripRenderer)new CustomToolStripRenderer(main.font_3);
            if (main._Game.SelectedObject == null)
            {
                return;
            }
            obj2 = ((main._Game.SelectedObject is BuiltObject) ? ((BuiltObject)main._Game.SelectedObject) : ((!(main._Game.SelectedObject is Habitat)) ? main._Game.SelectedObject : ((Habitat)main._Game.SelectedObject)));
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Hold position toggle");
            shipAction = main.method_315(BuiltObjectMissionType.Undefined, obj2);
            shipAction.Hint = "sleep";
            shipAction.ExtraData = "This toggles sleep for a ship";
            toolStripMenuItem.Tag = shipAction;
            main.actionMenu.Items.Add(toolStripMenuItem);
            if (CanDeployAsteroidColony(main, obj2, obj))
            {
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Deploy Asteroid Colony");
                ShipAction shipAction2 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                shipAction2.Hint = "asteroidColony";
                shipAction2.ExtraData = "This creates a small colony on an asteroid";
                shipAction2.Target = obj2;
                shipAction2.Target2 = obj;
                toolStripMenuItem2.Tag = shipAction2;
                main.actionMenu.Items.Add(toolStripMenuItem2);
            }
            if (BaconBuiltObject.IsFreeTrader(main, obj2))
            {
                if (obj is StellarObject && Galaxy.CalculateDistanceSquaredStatic((obj2 as StellarObject).Xpos, (obj2 as StellarObject).Ypos, (obj as StellarObject).Xpos, (obj as StellarObject).Ypos) > 90000.0)
                {
                    obj = null;
                }
                ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Trade");
                ShipAction shipAction3 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                shipAction3.Hint = "trade";
                shipAction3.ExtraData = "This is how a free trader buys and sells goods.";
                shipAction3.Target = obj2;
                shipAction3.Target2 = obj;
                toolStripMenuItem3.Tag = shipAction3;
                main.actionMenu.Items.Add(toolStripMenuItem3);
            }
            if (BaconBuiltObject.IsFreeTrader(main, obj2))
            {
                if (obj is StellarObject && Galaxy.CalculateDistanceSquaredStatic((obj2 as StellarObject).Xpos, (obj2 as StellarObject).Ypos, (obj as StellarObject).Xpos, (obj as StellarObject).Ypos) > 90000.0)
                {
                    obj = null;
                }
                if (obj is Habitat && (BaconHabitat.IsAsteroidColony(obj as Habitat) || BaconHabitat.IsIndependentColony(obj as Habitat)))
                {
                    ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem("Customize Ship");
                    ShipAction shipAction4 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                    shipAction4.Hint = "customizeShip";
                    shipAction4.ExtraData = "Equip or remove scavaged components.";
                    shipAction4.Target = obj2;
                    shipAction4.Target2 = obj;
                    toolStripMenuItem4.Tag = shipAction4;
                    main.actionMenu.Items.Add(toolStripMenuItem4);
                }
            }
            if (obj is Habitat && (obj as Habitat).InvadingTroops != null)
            {
                TroopList invadingTroops = (obj as Habitat).InvadingTroops;
                if (invadingTroops != null && invadingTroops.Count > 0 && ((obj as Habitat).InvadingTroops[0]?.Empire == main._Game.PlayerEmpire || (obj as Habitat).Empire == main._Game.PlayerEmpire))
                {
                    Habitat habitat = obj as Habitat;
                    ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem("Evaluate Tactics");
                    ShipAction shipAction5 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                    shipAction5.Hint = "invasionCommand";
                    shipAction5.ExtraData = "Try to influence battle by analyzing enemy tactics.";
                    shipAction5.Target = obj2;
                    shipAction5.Target2 = obj;
                    toolStripMenuItem5.Tag = shipAction5;
                    main.actionMenu.Items.Add(toolStripMenuItem5);
                }
            }
            if (IsScienceShip(main, obj2))
            {
                BuiltObject builtObject = (BuiltObject)obj2;
                ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem("Research");
                if (builtObject.BaconValues.ContainsKey("lab0"))
                {
                    ResearchNodeList allPotentialResearchNodesForIndustryType = BaconEmpire.GetAllPotentialResearchNodesForIndustryType(builtObject.Empire, builtObject.Empire.Research.TechTree, IndustryType.Weapon);
                    if (allPotentialResearchNodesForIndustryType != null && allPotentialResearchNodesForIndustryType.Count > 0)
                    {
                        ToolStripMenuItem toolStripMenuItem7 = new ToolStripMenuItem("Weapons Lab");
                        foreach (ResearchNode item in allPotentialResearchNodesForIndustryType)
                        {
                            ToolStripMenuItem toolStripMenuItem8 = new ToolStripMenuItem(item.Name);
                            ShipAction shipAction6 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                            shipAction6.Hint = "research";
                            shipAction6.ExtraData = item.Name;
                            shipAction6.Target2 = item;
                            toolStripMenuItem8.Tag = shipAction6;
                            toolStripMenuItem7.DropDownItems.Add(toolStripMenuItem8);
                            toolStripMenuItem8.DropDownItemClicked += main.actionMenu_ItemClicked;
                        }
                        toolStripMenuItem7.DropDownItemClicked += main.actionMenu_ItemClicked;
                        toolStripMenuItem6.DropDownItems.Add(toolStripMenuItem7);
                    }
                }
                if (builtObject.BaconValues.ContainsKey("lab1"))
                {
                    ResearchNodeList allPotentialResearchNodesForIndustryType2 = BaconEmpire.GetAllPotentialResearchNodesForIndustryType(builtObject.Empire, builtObject.Empire.Research.TechTree, IndustryType.Energy);
                    if (allPotentialResearchNodesForIndustryType2 != null && allPotentialResearchNodesForIndustryType2.Count > 0)
                    {
                        ToolStripMenuItem toolStripMenuItem9 = new ToolStripMenuItem("Energy Lab");
                        foreach (ResearchNode item2 in allPotentialResearchNodesForIndustryType2)
                        {
                            ToolStripMenuItem toolStripMenuItem10 = new ToolStripMenuItem(item2.Name);
                            ShipAction shipAction7 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                            shipAction7.Hint = "research";
                            shipAction7.ExtraData = item2.Name;
                            shipAction7.Target2 = item2;
                            toolStripMenuItem10.Tag = shipAction7;
                            toolStripMenuItem9.DropDownItems.Add(toolStripMenuItem10);
                            toolStripMenuItem10.DropDownItemClicked += main.actionMenu_ItemClicked;
                        }
                        toolStripMenuItem9.DropDownItemClicked += main.actionMenu_ItemClicked;
                        toolStripMenuItem6.DropDownItems.Add(toolStripMenuItem9);
                    }
                }
                if (builtObject.BaconValues.ContainsKey("lab2"))
                {
                    ResearchNodeList allPotentialResearchNodesForIndustryType3 = BaconEmpire.GetAllPotentialResearchNodesForIndustryType(builtObject.Empire, builtObject.Empire.Research.TechTree, IndustryType.HighTech);
                    if (allPotentialResearchNodesForIndustryType3 != null && allPotentialResearchNodesForIndustryType3.Count > 0)
                    {
                        ToolStripMenuItem toolStripMenuItem11 = new ToolStripMenuItem("High Tech Lab");
                        foreach (ResearchNode item3 in allPotentialResearchNodesForIndustryType3)
                        {
                            ToolStripMenuItem toolStripMenuItem12 = new ToolStripMenuItem(item3.Name);
                            ShipAction shipAction8 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                            shipAction8.Hint = "research";
                            shipAction8.ExtraData = item3.Name;
                            shipAction8.Target2 = item3;
                            toolStripMenuItem12.Tag = shipAction8;
                            toolStripMenuItem11.DropDownItems.Add(toolStripMenuItem12);
                            toolStripMenuItem12.DropDownItemClicked += main.actionMenu_ItemClicked;
                        }
                        toolStripMenuItem11.DropDownItemClicked += main.actionMenu_ItemClicked;
                        toolStripMenuItem6.DropDownItems.Add(toolStripMenuItem11);
                    }
                }
                main.actionMenu.Items.Add(toolStripMenuItem6);
            }
            if (CanOrderPirateShipsBuiltAtColony(main, obj2))
            {
                ToolStripMenuItem toolStripMenuItem13 = new ToolStripMenuItem("Construct Ships");
                List<Design> list = main._Game.PlayerEmpire.Designs.Where((Design x) => x.SubRole == BuiltObjectSubRole.ConstructionShip && !x.IsObsolete).ToList();
                if (list != null && list.Count > 0)
                {
                    ToolStripMenuItem toolStripMenuItem14 = new ToolStripMenuItem("Construction Ships");
                    foreach (Design item4 in list)
                    {
                        ToolStripMenuItem toolStripMenuItem15 = new ToolStripMenuItem(item4.Name);
                        ShipAction shipAction9 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                        shipAction9.Hint = "constructShip";
                        shipAction9.ExtraData = "Construct " + item4.Name;
                        shipAction9.Target2 = item4;
                        toolStripMenuItem15.Tag = shipAction9;
                        toolStripMenuItem14.DropDownItems.Add(toolStripMenuItem15);
                        toolStripMenuItem15.DropDownItemClicked += main.actionMenu_ItemClicked;
                    }
                    toolStripMenuItem14.DropDownItemClicked += main.actionMenu_ItemClicked;
                    toolStripMenuItem13.DropDownItems.Add(toolStripMenuItem14);
                }
                list = main._Game.PlayerEmpire.Designs.Where((Design x) => (x.SubRole == BuiltObjectSubRole.LargeFreighter || x.SubRole == BuiltObjectSubRole.MediumFreighter || x.SubRole == BuiltObjectSubRole.SmallFreighter) && !x.IsObsolete).ToList();
                if (list != null && list.Count > 0)
                {
                    ToolStripMenuItem toolStripMenuItem16 = new ToolStripMenuItem("Freighters");
                    foreach (Design item5 in list)
                    {
                        ToolStripMenuItem toolStripMenuItem17 = new ToolStripMenuItem(item5.Name);
                        ShipAction shipAction10 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                        shipAction10.Hint = "constructShip";
                        shipAction10.ExtraData = "Construct " + item5.Name;
                        shipAction10.Target2 = item5;
                        toolStripMenuItem17.Tag = shipAction10;
                        toolStripMenuItem16.DropDownItems.Add(toolStripMenuItem17);
                        toolStripMenuItem17.DropDownItemClicked += main.actionMenu_ItemClicked;
                    }
                    toolStripMenuItem16.DropDownItemClicked += main.actionMenu_ItemClicked;
                    toolStripMenuItem13.DropDownItems.Add(toolStripMenuItem16);
                }
                main.actionMenu.Items.Add(toolStripMenuItem13);
            }
            ToolStripMenuItem toolStripMenuItem18 = new ToolStripMenuItem("Missions");
            if (CanDoMissionScientificData(main, obj2, obj))
            {
                ToolStripMenuItem toolStripMenuItem19 = new ToolStripMenuItem("Science Mission - explore ruins");
                ShipAction shipAction11 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                shipAction11.Hint = "missionExploreRuins";
                shipAction11.ExtraData = "Explore ruins for scientific data.";
                shipAction11.Target = obj2;
                shipAction11.Target2 = obj;
                toolStripMenuItem19.Tag = shipAction11;
                toolStripMenuItem19.DropDownItemClicked += main.actionMenu_ItemClicked;
                toolStripMenuItem18.DropDownItemClicked += main.actionMenu_ItemClicked;
                toolStripMenuItem18.DropDownItems.Add(toolStripMenuItem19);
                main.actionMenu.Items.Add(toolStripMenuItem18);
            }
            if (CanRecruitShipOfficer(main, obj2, obj))
            {
                ToolStripMenuItem toolStripMenuItem20 = new ToolStripMenuItem("Recruit ship officer");
                ShipAction shipAction12 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                shipAction12.Hint = "recruitShipOfficer";
                shipAction12.ExtraData = "This recruits a ship officer.";
                shipAction12.Target = obj2;
                shipAction12.Target2 = obj;
                toolStripMenuItem20.Tag = shipAction12;
                main.actionMenu.Items.Add(toolStripMenuItem20);
            }
            if (CanDoMissionProspectForResources(main, obj2, obj))
            {
                ToolStripMenuItem toolStripMenuItem21 = new ToolStripMenuItem("Science Mission - prospect for resources");
                ShipAction shipAction13 = main.method_315(BuiltObjectMissionType.Undefined, obj2);
                shipAction13.Hint = "missionProspectForResources";
                shipAction13.ExtraData = "Prospect for resources.";
                shipAction13.Target = obj2;
                shipAction13.Target2 = obj;
                toolStripMenuItem21.Tag = shipAction13;
                toolStripMenuItem21.DropDownItemClicked += main.actionMenu_ItemClicked;
                toolStripMenuItem18.DropDownItemClicked += main.actionMenu_ItemClicked;
                toolStripMenuItem18.DropDownItems.Add(toolStripMenuItem21);
                main.actionMenu.Items.Add(toolStripMenuItem18);
            }
            main.actionMenu.Show();
        }

        public static bool CanOrderPirateShipsBuiltAtColony(Main main, object locationObject)
        {
            bool result = false;
            if (locationObject is Habitat && (locationObject as Habitat).Empire == main._Game.Galaxy.IndependentEmpire)
            {
                result = true;
            }
            return result;
        }

        public static bool CanDoMissionProspectForResources(Main main, object shipObject, object locationObject)
        {
            if (shipObject is BuiltObject && ((BuiltObject)shipObject).ActualEmpire == main._Game.PlayerEmpire)
            {
                BuiltObject builtObject = (BuiltObject)shipObject;
                if (!IsScienceShip(main, builtObject))
                {
                    return false;
                }
                if (builtObject.Characters != null && builtObject.Characters.FirstOrDefault((Character x) => x.Role == CharacterRole.ShipCaptain) != null && locationObject is Habitat)
                {
                    Habitat habitat = (Habitat)locationObject;
                    if (habitat.Type != HabitatType.GasGiant && habitat.Type != HabitatType.FrozenGasGiant && habitat.BaconValues == null && Galaxy.CalculateDistanceStatic(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos) < 300.0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CanDoMissionScientificData(Main main, object shipObject, object locationObject)
        {
            if (shipObject is BuiltObject && ((BuiltObject)shipObject).ActualEmpire == main._Game.PlayerEmpire)
            {
                BuiltObject builtObject = (BuiltObject)shipObject;
                if (!IsScienceShip(main, builtObject))
                {
                    return false;
                }
                if (builtObject.Characters != null && builtObject.Characters.FirstOrDefault((Character x) => x.Role == CharacterRole.ShipCaptain) != null && locationObject is Habitat)
                {
                    Habitat habitat = (Habitat)locationObject;
                    if (habitat.Ruin != null)
                    {
                        if (habitat.BaconValues == null)
                        {
                            if (Galaxy.CalculateDistanceStatic(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos) < 300.0)
                            {
                                return true;
                            }
                        }
                        else if (((Habitat)locationObject).BaconValues.ContainsKey("missionExploreRuins"))
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        public static bool CanRecruitShipOfficer(Main main, object shipObject, object locationObject)
        {
            bool result = false;
            if (shipObject is BuiltObject && locationObject is Habitat)
            {
                Empire playerEmpire = main._Game.PlayerEmpire;
                BuiltObject builtObject = (BuiltObject)shipObject;
                Habitat habitat = (Habitat)locationObject;
                if (BaconBuiltObject.IsFreeTrader(main, shipObject) && (main._Game.Galaxy.IndependentColonies.Contains(habitat) || BaconHabitat.IsAsteroidColony(habitat)))
                {
                    int num = (int)builtObject.BaconValues["cash"];
                    int num2 = baseShipOfficerCost;
                    if (num >= num2 && Galaxy.CalculateDistanceStatic(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos) < 300.0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public static bool CanDeployAsteroidColony(Main main, object shipObject, object locationObject)
        {
            bool result = false;
            if (shipObject is BuiltObject && locationObject is Habitat)
            {
                Empire playerEmpire = main._Game.PlayerEmpire;
                BuiltObject builtObject = (BuiltObject)shipObject;
                Habitat habitat = (Habitat)locationObject;
                if (!BaconHabitat.allowAsteroidColonies && !playerEmpire.Name.Contains("Romulan"))
                {
                    return false;
                }
                if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip && builtObject.Empire == playerEmpire && !playerEmpire.Colonies.Contains(habitat) && (habitat.Population == null || habitat.Population.Count == 0) && habitat.Category == HabitatCategoryType.Asteroid && playerEmpire.StateMoney >= (double)BaconHabitat.asteroidColonyCost && Galaxy.CalculateDistanceStatic(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos) < 300.0)
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsScienceShip(Main main, object shipObject)
        {
            if (!(shipObject is BuiltObject))
            {
                return false;
            }
            BuiltObject builtObject = (BuiltObject)shipObject;
            if (builtObject.BaconValues == null || builtObject.SubRole != BuiltObjectSubRole.ExplorationShip)
            {
                return false;
            }
            bool flag = false;
            if (builtObject.Components.Exists((BuiltObjectComponent x) => x.Type == ComponentType.LabsWeaponsLab && x.Status == ComponentStatus.Normal))
            {
                flag = true;
            }
            else if (builtObject.BaconValues.ContainsKey("lab0"))
            {
                builtObject.BaconValues.Remove("lab0");
            }
            if (builtObject.Components.Exists((BuiltObjectComponent x) => x.Type == ComponentType.LabsEnergyLab && x.Status == ComponentStatus.Normal))
            {
                flag = true;
            }
            else if (builtObject.BaconValues.ContainsKey("lab1"))
            {
                builtObject.BaconValues.Remove("lab1");
            }
            if (builtObject.Components.Exists((BuiltObjectComponent x) => x.Type == ComponentType.LabsHighTechLab && x.Status == ComponentStatus.Normal))
            {
                flag = true;
            }
            else if (builtObject.BaconValues.ContainsKey("lab2"))
            {
                builtObject.BaconValues.Remove("lab2");
            }
            if (!flag && builtObject.BaconValues.ContainsKey("scientificData"))
            {
                builtObject.BaconValues.Remove("scientificData");
            }
            if (builtObject.BaconValues.Count == 0)
            {
                builtObject.BaconValues = null;
            }
            if (flag)
            {
                return true;
            }
            return false;
        }

        public static void TogglePause()
        {
            if (BaconBuiltObject.myMain != null)
            {
                if (BaconBuiltObject.myMain._Game.Galaxy.TimeState != GalaxyTimeState.Paused || 1 == 0)
                {
                    BaconBuiltObject.myMain._Game.Galaxy.Pause();
                }
                else
                {
                    BaconBuiltObject.myMain._Game.Galaxy.Resume();
                }
            }
        }

        public static void BaconPause()
        {
            if (BaconBuiltObject.myMain != null)
            {
                BaconBuiltObject.myMain._Game.Galaxy.Pause();
            }
        }

        public static void LogToFile(string message)
        {
            if (IsFileReady(outputLog))
            {
                try
                {
                    locker = new ReaderWriterLock();
                    locker.AcquireWriterLock(int.MaxValue);
                    File.AppendAllText(outputLog, message + Environment.NewLine);
                }
                finally
                {
                    locker.ReleaseWriterLock();
                }
            }
        }

        public static bool IsFileReady(string sFilename)
        {
            try
            {
                using FileStream fileStream = File.Open(sFilename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (fileStream != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static StellarObjectList method_423(Main main, BuiltObject builtObject_8)
        {
            main.cmbBuiltObjectFilter.MaxDropDownItems = cmbBuiltObjectFilterStrings.Length;
            StellarObjectList stellarObjectList = new StellarObjectList();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            Empire playerEmpire = main._Game.PlayerEmpire;
            string text = string.Empty;
            if (main.cmbBuiltObjectFilter.SelectedIndex >= 0)
            {
                text = main.cmbBuiltObjectFilter.Items[main.cmbBuiltObjectFilter.SelectedIndex].ToString();
            }
            main.pnlBuiltObjectInfo.HeaderTitle = TextResolver.GetText("Ships and Bases");
            if (text == TextResolver.GetText("Colony Ships"))
            {
                list.Add(BuiltObjectSubRole.ColonyShip);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
            }
            else if (text == TextResolver.GetText("Construction Yards"))
            {
                main.pnlBuiltObjectInfo.HeaderTitle = TextResolver.GetText("Construction Yards");
                for (int i = 0; i < playerEmpire.BuiltObjects.Count(); i++)
                {
                    BuiltObject builtObject = playerEmpire.BuiltObjects[i];
                    if (builtObject.IsShipYard && builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionYards.Count > 0)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
                for (int j = 0; j < playerEmpire.PrivateBuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = playerEmpire.PrivateBuiltObjects[j];
                    if (builtObject2.IsShipYard && builtObject2.ConstructionQueue != null && builtObject2.ConstructionQueue.ConstructionYards.Count > 0)
                    {
                        builtObjectList.Add(builtObject2);
                    }
                }
                stellarObjectList.AddRange(builtObjectList);
                builtObjectList.Clear();
                for (int k = 0; k < playerEmpire.Colonies.Count; k++)
                {
                    Habitat item = playerEmpire.Colonies[k];
                    stellarObjectList.Add(item);
                }
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_ConstructionYards");
            }
            else if (text == TextResolver.GetText("Exploration Ships"))
            {
                list.Add(BuiltObjectSubRole.ExplorationShip);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
            }
            else if (text == TextResolver.GetText("Freighters"))
            {
                list.Add(BuiltObjectSubRole.SmallFreighter);
                list.Add(BuiltObjectSubRole.MediumFreighter);
                list.Add(BuiltObjectSubRole.LargeFreighter);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                builtObjectList.AddRange(playerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list));
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Cargo");
            }
            else if (text == TextResolver.GetText("Military Ships"))
            {
                list.Add(BuiltObjectSubRole.Escort);
                list.Add(BuiltObjectSubRole.Frigate);
                list.Add(BuiltObjectSubRole.Destroyer);
                list.Add(BuiltObjectSubRole.Cruiser);
                list.Add(BuiltObjectSubRole.CapitalShip);
                list.Add(BuiltObjectSubRole.TroopTransport);
                list.Add(BuiltObjectSubRole.Carrier);
                list.Add(BuiltObjectSubRole.ResupplyShip);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Weapons");
            }
            else if (text == TextResolver.GetText("Mining Ships"))
            {
                list.Add(BuiltObjectSubRole.MiningShip);
                list.Add(BuiltObjectSubRole.GasMiningShip);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                builtObjectList.AddRange(playerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list));
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Cargo");
            }
            else if (text == TextResolver.GetText("Mining Stations"))
            {
                list.Add(BuiltObjectSubRole.MiningStation);
                list.Add(BuiltObjectSubRole.GasMiningStation);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                builtObjectList.AddRange(playerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list));
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Cargo");
            }
            else if (text == TextResolver.GetText("Other Bases"))
            {
                list.Add(BuiltObjectSubRole.GenericBase);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                builtObjectList.AddRange(playerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list));
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Components");
            }
            else if (text == TextResolver.GetText("Research Stations"))
            {
                list.Add(BuiltObjectSubRole.EnergyResearchStation);
                list.Add(BuiltObjectSubRole.WeaponsResearchStation);
                list.Add(BuiltObjectSubRole.HighTechResearchStation);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                builtObjectList.AddRange(playerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list));
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Components");
            }
            else if (text == TextResolver.GetText("Defensive Bases"))
            {
                list.Add(BuiltObjectSubRole.DefensiveBase);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                builtObjectList.AddRange(playerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list));
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Components");
            }
            else if (text == TextResolver.GetText("Monitoring Stations"))
            {
                list.Add(BuiltObjectSubRole.MonitoringStation);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                builtObjectList.AddRange(playerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list));
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Components");
            }
            else if (text == TextResolver.GetText("Passenger Ships"))
            {
                list.Add(BuiltObjectSubRole.PassengerShip);
                builtObjectList = playerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list);
                builtObjectList.AddRange(playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list));
            }
            else if (text == TextResolver.GetText("Resort Bases"))
            {
                list.Add(BuiltObjectSubRole.ResortBase);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
            }
            else if (text == TextResolver.GetText("Resupply Ships"))
            {
                list.Add(BuiltObjectSubRole.ResupplyShip);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
            }
            else if (text == TextResolver.GetText("Space Ports"))
            {
                list.Add(BuiltObjectSubRole.SmallSpacePort);
                list.Add(BuiltObjectSubRole.MediumSpacePort);
                list.Add(BuiltObjectSubRole.LargeSpacePort);
                builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
            }
            else if (text == TextResolver.GetText("Troop Carriers"))
            {
                for (int l = 0; l < playerEmpire.BuiltObjects.Count; l++)
                {
                    BuiltObject builtObject3 = playerEmpire.BuiltObjects[l];
                    if (builtObject3.TroopCapacity > 0)
                    {
                        builtObjectList.Add(builtObject3);
                    }
                }
                for (int m = 0; m < playerEmpire.PrivateBuiltObjects.Count; m++)
                {
                    BuiltObject builtObject4 = playerEmpire.PrivateBuiltObjects[m];
                    if (builtObject4.TroopCapacity > 0)
                    {
                        builtObjectList.Add(builtObject4);
                    }
                }
                main.tabBuiltObjectData.SelectTab("tabBuiltObject_Troops");
            }
            else if (text == TextResolver.GetText("Selected Item"))
            {
                if (builtObject_8 != null && builtObject_8.ActualEmpire == playerEmpire)
                {
                    builtObjectList.Add(builtObject_8);
                }
            }
            else
            {
                switch (text)
                {
                    case "Damaged Ships":
                        {
                            List<BuiltObject> list7 = playerEmpire.BuiltObjects.FindAll((BuiltObject x) => x.Role != BuiltObjectRole.Base && x.Components.Count((BuiltObjectComponent y) => y.Status == ComponentStatus.Damaged) > 0);
                            if (list7.Count <= 0)
                            {
                                break;
                            }
                            list7 = list7.OrderBy((BuiltObject x) => x.SubRole).ThenBy((BuiltObject y) => y.DateBuilt).ToList();
                            foreach (BuiltObject item2 in list7)
                            {
                                stellarObjectList.Add(item2);
                            }
                            break;
                        }
                    case "Low Fuel Ships":
                        {
                            List<BuiltObject> list5 = playerEmpire.BuiltObjects.FindAll((BuiltObject x) => x.BuiltAt == null && x.Role != BuiltObjectRole.Base && x.CurrentFuel <= 0.1);
                            if (list5.Count <= 0)
                            {
                                break;
                            }
                            list5 = list5.OrderBy((BuiltObject x) => x.SubRole).ThenBy((BuiltObject y) => y.DateBuilt).ToList();
                            foreach (BuiltObject item3 in list5)
                            {
                                stellarObjectList.Add(item3);
                            }
                            break;
                        }
                    case "Ships Under Attack":
                        {
                            List<BuiltObject> list8 = new BuiltObjectList();
                            List<BuiltObject> list9 = playerEmpire.BuiltObjects.FindAll((BuiltObject x) => x.BuiltAt == null && x.Attackers != null && x.Attackers.Count > 0);
                            List<BuiltObject> list10 = playerEmpire.PrivateBuiltObjects.FindAll((BuiltObject x) => x.BuiltAt == null && x.Attackers != null && x.Attackers.Count > 0);
                            if (list9.Any())
                            {
                                list8.AddRange(list9);
                            }
                            if (list10.Any())
                            {
                                list8.AddRange(list10);
                            }
                            if (list8.Count <= 0)
                            {
                                break;
                            }
                            list8 = list8.OrderBy((BuiltObject x) => x.SubRole).ThenBy((BuiltObject y) => y.DateBuilt).ToList();
                            foreach (BuiltObject item4 in list8)
                            {
                                stellarObjectList.Add(item4);
                            }
                            break;
                        }
                    case "Newest Ships":
                        {
                            List<BuiltObject> list6 = playerEmpire.BuiltObjects.FindAll((BuiltObject x) => x.Role != BuiltObjectRole.Base && (double)x.DateBuilt > lastTimeBuiltObjectPanelShowedNewShips && x.Components.Count((BuiltObjectComponent y) => y.Status == ComponentStatus.Unbuilt) == 0);
                            if (list6.Count <= 0)
                            {
                                break;
                            }
                            list6 = list6.OrderBy((BuiltObject x) => x.SubRole).ThenBy((BuiltObject y) => y.DateBuilt).ToList();
                            foreach (BuiltObject item5 in list6)
                            {
                                stellarObjectList.Add(item5);
                            }
                            lastTimeBuiltObjectPanelShowedNewShips = main._Game.Galaxy.CurrentStarDate;
                            break;
                        }
                    case "State Versions":
                        list.Add(BuiltObjectSubRole.GasMiningShip);
                        list.Add(BuiltObjectSubRole.MiningShip);
                        list.Add(BuiltObjectSubRole.SmallFreighter);
                        list.Add(BuiltObjectSubRole.MediumFreighter);
                        list.Add(BuiltObjectSubRole.LargeFreighter);
                        list.Add(BuiltObjectSubRole.PassengerShip);
                        builtObjectList = playerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list);
                        builtObjectList.RemoveAll((BuiltObject x) => x.BaconValues != null && x.BaconValues.ContainsKey("cash"));
                        break;
                    case "Upgradable":
                        {
                            List<BuiltObject> list3 = playerEmpire.BuiltObjects.FindAll((BuiltObject x) => x.BuiltAt == null && x.Role != BuiltObjectRole.Base && x.Design.IsObsolete);
                            if (list3.Count <= 0)
                            {
                                break;
                            }
                            list3 = list3.OrderBy((BuiltObject x) => x.SubRole).ThenBy((BuiltObject y) => y.DateBuilt).ToList();
                            foreach (BuiltObject item6 in list3)
                            {
                                stellarObjectList.Add(item6);
                            }
                            break;
                        }
                    case "Free Traders":
                        {
                            List<BuiltObject> list4 = playerEmpire.BuiltObjects.FindAll((BuiltObject x) => x.BuiltAt == null && x.BaconValues != null && x.BaconValues.ContainsKey("cash"));
                            if (list4.Count <= 0)
                            {
                                break;
                            }
                            list4 = list4.OrderBy((BuiltObject x) => x.SubRole).ThenBy((BuiltObject y) => y.DateBuilt).ToList();
                            foreach (BuiltObject item7 in list4)
                            {
                                stellarObjectList.Add(item7);
                            }
                            break;
                        }
                    case "Repeating Mission":
                        {
                            List<BuiltObject> list2 = playerEmpire.BuiltObjects.FindAll((BuiltObject x) => x.BuiltAt == null && x.Role != BuiltObjectRole.Base && x.BaconValues != null && x.BaconValues.ContainsKey("RepeatingMission"));
                            if (list2.Count <= 0)
                            {
                                break;
                            }
                            list2 = list2.OrderBy((BuiltObject x) => x.SubRole).ThenBy((BuiltObject y) => y.DateBuilt).ToList();
                            foreach (BuiltObject item8 in list2)
                            {
                                stellarObjectList.Add(item8);
                            }
                            break;
                        }
                    default:
                        builtObjectList.AddRange(playerEmpire.BuiltObjects);
                        builtObjectList.AddRange(playerEmpire.PrivateBuiltObjects);
                        break;
                }
            }
            if (builtObjectList != null && builtObjectList.Count > 0)
            {
                stellarObjectList.AddRange(builtObjectList);
            }
            return OrderByDistance(main, stellarObjectList);
        }

        public static StellarObjectList OrderByDistance(Main main, StellarObjectList shiplist)
        {
            List<StellarObject> list = new List<StellarObject>();
            List<StellarObject> source = shiplist.ToList();
            StellarObjectList stellarObjectList = new StellarObjectList();
            if (main._Game.SelectedObject != null)
            {
                StellarObject selected = main._Game.SelectedObject as StellarObject;
                if (selected != null)
                {
                    list = source.OrderBy((StellarObject x) => Galaxy.CalculateDistanceSquaredStatic(selected.Xpos, selected.Ypos, x.Xpos, x.Ypos)).ToList();
                }
            }
            if (list.Any())
            {
                stellarObjectList.AddRange(list);
                return stellarObjectList;
            }
            return shiplist;
        }

        public static BuiltObjectList OrderByDistanceBuiltObjects(Main main, BuiltObjectList shiplist)
        {
            List<BuiltObject> list = new List<BuiltObject>();
            List<BuiltObject> source = shiplist.ToList();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            if (main._Game.SelectedObject != null)
            {
                StellarObject selected = main._Game.SelectedObject as StellarObject;
                if (selected != null)
                {
                    list = source.OrderBy((BuiltObject x) => Galaxy.CalculateDistanceSquaredStatic(selected.Xpos, selected.Ypos, x.Xpos, x.Ypos)).ToList();
                }
            }
            if (list.Any())
            {
                builtObjectList.AddRange(list);
                return builtObjectList;
            }
            return shiplist;
        }

        public static HabitatList OrderByDistanceHabitats(Main main, HabitatList habitatList)
        {
            List<Habitat> list = new List<Habitat>();
            List<Habitat> source = habitatList.ToList();
            HabitatList habitatList2 = new HabitatList();
            if (main._Game.SelectedObject != null)
            {
                StellarObject selected = main._Game.SelectedObject as StellarObject;
                if (selected != null)
                {
                    list = source.OrderBy((Habitat x) => Galaxy.CalculateDistanceSquaredStatic(selected.Xpos, selected.Ypos, x.Xpos, x.Ypos)).ToList();
                }
            }
            if (list.Any())
            {
                habitatList2.AddRange(list);
                return habitatList2;
            }
            return habitatList;
        }

        public static void RegenerateShipDesigns(Main main, bool includePlayerEmpire = false)
        {
            List<Empire> list = main._Game.Galaxy.Empires.ToList();
            List<Empire> list2 = main._Game.Galaxy.PirateEmpires.ToList();
            if (!includePlayerEmpire)
            {
                if (list.Contains(main._Game.PlayerEmpire))
                {
                    list.Remove(main._Game.PlayerEmpire);
                }
                else if (list2.Contains(main._Game.PlayerEmpire))
                {
                    list2.Remove(main._Game.PlayerEmpire);
                }
            }
            foreach (Empire item in list)
            {
                bool flag = false;
                if (item.DominantRace.Name == "Shakturi" || item.DominantRace.Name == "Erutkah")
                {
                    flag = true;
                }
                item.GenerateDesignSpecifications(main._Game.Galaxy, item.DominantRace, isPirate: false, flag ? "Shakturi" : item.DominantRace.Name);
            }
            foreach (Empire item2 in list2)
            {
                item2.GenerateDesignSpecifications(main._Game.Galaxy, item2.DominantRace, isPirate: true, item2.DominantRace.Name);
            }
            BaconEmpire.CreateNewDesignsForSelectedShipTypes(main, main._Game.PlayerEmpire);
        }

        public static void OnChangeCapital(Main main)
        {
            Habitat selectedHabitat = main.UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat == null)
            {
                return;
            }
            Habitat capital = main._Game.PlayerEmpire.Capital;
            Habitat habitat = selectedHabitat;
            if (capital.BaconValues == null)
            {
                return;
            }
            if (habitat.BaconValues == null)
            {
                habitat.BaconValues = new Dictionary<string, object>();
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>(capital.BaconValues);
            foreach (KeyValuePair<string, object> item in dictionary)
            {
                if (baconValuesToCopyOnChangingCapital.Contains(item.Key) && !habitat.BaconValues.ContainsKey(item.Key))
                {
                    habitat.BaconValues.Add(item.Key, item.Value);
                    capital.BaconValues.Remove(item.Key);
                }
            }
        }

        public static void LoadUiMessages(Main main, string string_30, string string_31)
        {
            try
            {
                string text = string_30 + "ui\\messages\\";
                string text2 = string_31 + "ui\\messages\\";
                Parallel.Invoke(
                    () => main.bitmap_28[0] = main.method_10(text2, text, "underAttack.png", true),
                    () => main.bitmap_28[1] = main.method_10(text2, text, "colonygain.png", true),
                    () => main.bitmap_28[2] = main.method_10(text2, text, "colonyloss.png", true),
                    () => main.bitmap_28[3] = main.method_10(text2, text, "declarewar.png", true),
                    () => main.bitmap_28[4] = main.method_10(text2, text, "endwar.png", true),
                    () => main.bitmap_28[5] = main.method_10(text2, text, "freetradeagreement.png", true),
                    () => main.bitmap_28[6] = main.method_10(text2, text, "mutualdefensepact.png", true),
                    () => main.bitmap_28[7] = main.method_10(text2, text, "protectorate.png", true),
                    () => main.bitmap_28[8] = main.method_10(text2, text, "researchbreakthrough.png", true),
                    () => main.bitmap_28[9] = main.method_10(text2, text, "resumetrade.png", true),
                    () => main.bitmap_28[10] = main.method_10(text2, text, "subjugateddominion.png", true),
                    () => main.bitmap_28[11] = main.method_10(text2, text, "tradesanctions.png", true),
                    () => main.bitmap_28[12] = main.method_10(text2, text, "canceltreaty.png", true),
                    () => main.bitmap_28[13] = main.method_10(text2, text, "treatyrefused.png", true),
                    () => main.bitmap_28[14] = main.method_10(text2, text, "money.png", true),
                    () => main.bitmap_28[15] = main.method_10(text2, text, "warning.png", true),
                    () => main.bitmap_28[16] = main.method_10(text2, text, "construction.png", true),
                    () => main.bitmap_28[17] = main.method_10(text2, text, "request.png", true),
                    () => main.bitmap_28[18] = main.method_10(text2, text, "agentsuccess.png", true),
                    () => main.bitmap_28[19] = main.method_10(text2, text, "agentfailure.png", true),
                    () => main.bitmap_28[20] = main.method_10(text2, text, "blockade.png", true),
                    () => main.bitmap_28[21] = main.method_10(text2, text, "blockadecancelled.png", true),
                    () => main.bitmap_28[22] = main.method_10(text2, text, "restrictedArea.png", true),
                    () => main.bitmap_28[23] = main.method_10(text2, text, "agentalert.png", true),
                    () => main.bitmap_28[24] = main.method_10(text2, text, "explorationDiscovery.png", true),
                    () => main.bitmap_28[25] = main.method_10(text2, text, "galacticHistory.png", true),
                    () => main.bitmap_28[26] = main.method_10(text2, text, "information.png", true),
                    () => main.bitmap_28[27] = main.method_10(text2, text, "pirateMessage.png", true),
                    () => main.bitmap_28[28] = main.method_10(text2, text, "planetdestroy.png", true),
                    () => main.bitmap_28[29] = main.method_10(text2, text, "galacticnewsnet.png", true),
                    () => main.bitmap_28[30] = main.method_10(text2, text, "construction_stalled.png", true),
                    () => main.bitmap_28[31] = main.method_10(text2, text, "ransom.png", false),
                    () => main.bitmap_28[32] = main.method_10(text2, text, "distance.png", false),
                    () => main.bitmap_28[33] = main.method_10(text2, text, "scienceResearchComplete.png", false),
                    () => main.bitmap_28[34] = main.method_10(text2, text, "prisonbreak.png", false),
                    () => main.bitmap_28[35] = main.method_10(text2, text, "loanPayment.png", false),
                    () => main.bitmap_28[36] = main.method_10(text2, text, "exploreRuins.png", false),
                    () => main.bitmap_28[37] = main.method_10(text2, text, "fighterRepaired.png", false),
                    () => main.bitmap_28[38] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[39] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[40] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[41] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[42] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[43] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[44] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[45] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[46] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[47] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[48] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[49] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[50] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[51] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[52] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[53] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[54] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[55] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[56] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[57] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[58] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[59] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[60] = main.method_10(text2, text, "construction_stalled.png", false),
                    () => main.bitmap_28[61] = main.method_10(text2, text, "construction_stalled.png", false)
                );
                List<Task> taskList = new List<Task>();
                for (int i = 0; i < 62; i++)
                {
                    if (main.bitmap_28[i] == null)
                    {
                        int localI = i;
                        taskList.Add(Task.Run(() => main.bitmap_28[localI] = main.method_10(text2, text, "construction_stalled.png", false)));
                    }
                }
                Task.WaitAll(taskList.ToArray());
            }
            catch (Exception ex)
            {
                BaconBuiltObject.ShowMessageBox(main, ex.Message, "Exception");
            }
        }

        public static void PopulateListsOnLefthandSide(Main main, ItemListPanel itemListPanel_0)
        {
            main.list_6.Clear();
            if (itemListPanel_0 == null)
            {
                return;
            }
            itemListPanel_0.ColonizationFocus = false;
            itemListPanel_0.MiningFocus = false;
            string titleText = itemListPanel_0.TitleText;
            if (titleText == TextResolver.GetText("Colonies"))
            {
                main.list_6.AddRange(ListHelper.ToArrayThreadSafe(main._Game.PlayerEmpire.Colonies));
            }
            else if (titleText == TextResolver.GetText("Characters"))
            {
                main.list_6.AddRange(ListHelper.ToArrayThreadSafe(main._Game.PlayerEmpire.Characters));
            }
            else if (titleText == TextResolver.GetText("Space Ports") + " / " + TextResolver.GetText("Construction Yards"))
            {
                main.list_6.AddRange(ListHelper.ToArrayThreadSafe(main._Game.PlayerEmpire.SpacePorts));
            }
            else if (titleText == TextResolver.GetText("Mining Stations"))
            {
                BuiltObjectList builtObjects = null;
                if (main._Game.PlayerEmpire.PrivateBuiltObjects != null)
                {
                    builtObjects = main._Game.PlayerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(new List<BuiltObjectSubRole>
                {
                    BuiltObjectSubRole.GasMiningStation,
                    BuiltObjectSubRole.MiningStation
                });
                }
                builtObjects = BaconHabitat.FilterParentHabitatListByResource(main, builtObjects, resourceFilter);
                main.list_6.AddRange(ListHelper.ToArrayThreadSafe(OrderByDistanceBuiltObjects(main, builtObjects)));
            }
            else if (titleText == TextResolver.GetText("Construction Ships"))
            {
                main.list_6.AddRange(ListHelper.ToArrayThreadSafe(OrderByDistanceBuiltObjects(main, main._Game.PlayerEmpire.ConstructionShips)));
            }
            else if (titleText == TextResolver.GetText("Exploration Ships"))
            {
                BuiltObjectList builtObjectsBySubRole = main._Game.PlayerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(new List<BuiltObjectSubRole> { BuiltObjectSubRole.ExplorationShip });
                main.list_6.AddRange(ListHelper.ToArrayThreadSafe(OrderByDistanceBuiltObjects(main, builtObjectsBySubRole)));
            }
            else if (titleText == TextResolver.GetText("Enemy Targets"))
            {
                main.list_6.AddRange(main.method_205());
            }
            else if (titleText == TextResolver.GetText("Fleets"))
            {
                main.list_6.AddRange(ListHelper.ToArrayThreadSafe(main._Game.PlayerEmpire.ShipGroups));
            }
            else if (titleText == TextResolver.GetText("Military Ships"))
            {
                BuiltObjectList builtObjectList = new BuiltObjectList();
                for (int i = 0; i < main._Game.PlayerEmpire.BuiltObjects.Count(); i++)
                {
                    if (main._Game.PlayerEmpire.BuiltObjects[i].Role == BuiltObjectRole.Military && (itemListPanel_0.GetToggleButtonState(0) == 1 || main._Game.PlayerEmpire.BuiltObjects[i].ShipGroup == null))
                    {
                        builtObjectList.Add(main._Game.PlayerEmpire.BuiltObjects[i]);
                    }
                }
                main.list_6.AddRange(OrderByDistanceBuiltObjects(main, builtObjectList).ToArray());
            }
            else if (titleText != TextResolver.GetText("Pirate Missions"))
            {
                if (titleText == TextResolver.GetText("Potential Colonies"))
                {
                    itemListPanel_0.ColonizationFocus = true;
                    int toggleButtonState = itemListPanel_0.GetToggleButtonState(0);
                    HabitatPrioritizationList habitatPrioritizationList = main._Game.PlayerEmpire.IdentifyColonizationTargets(main._Game.Galaxy, filterOutDangerousTargets: false, 0, 100, toggleButtonState == 1, includeDistantTargets: true);
                    main.list_6.AddRange(habitatPrioritizationList.ResolveHabitats().ToArray());
                }
                else if (titleText == TextResolver.GetText("Potential Mining Locations"))
                {
                    itemListPanel_0.MiningFocus = true;
                    int toggleButtonState2 = itemListPanel_0.GetToggleButtonState(0);
                    HabitatPrioritizationList habitatPrioritizationList2 = main._Game.PlayerEmpire.IdentifyResourceCentres(main._Game.Galaxy, filterOutAssignedHabitats: false, filterOutDangerousTargets: false, toggleButtonState2 == 1);
                    HabitatList habitats = habitatPrioritizationList2.ResolveHabitats();
                    habitats = BaconHabitat.FilterHabitatListByResource(main, habitats, resourceFilter);
                    main.list_6.AddRange(OrderByDistanceHabitats(main, habitats).ToArray());
                }
                else if (titleText == TextResolver.GetText("Potential Research Locations"))
                {
                    HabitatList habitatList = main._Game.PlayerEmpire.DetermineResearchStationLocation(allowOccupiedSystems: true, mustHaveBuildableResearchStationDesign: false, assignToResearchHabitats: false);
                    main.list_6.AddRange(ListHelper.ToArrayThreadSafe(OrderByDistanceHabitats(main, habitatList)));
                }
                else if (titleText == TextResolver.GetText("Potential Resort Locations"))
                {
                    PrioritizedTargetList prioritizedTargetList = main._Game.PlayerEmpire.DetermineResortBaseBuildLocations();
                    main.list_6.AddRange(OrderByDistanceHabitats(main, prioritizedTargetList.ResolveHabitats()).ToArray());
                }
                else if (titleText == TextResolver.GetText("Special Locations"))
                {
                    main.list_6.AddRange(main._Game.PlayerEmpire.KnownGalaxyLocations.FindLocations(new List<GalaxyLocationType>
                {
                    GalaxyLocationType.DebrisField,
                    GalaxyLocationType.PlanetDestroyer,
                    GalaxyLocationType.RestrictedArea
                }).ToArray());
                }
            }
            else if (main._Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
            {
                int toggleButtonState3 = itemListPanel_0.GetToggleButtonState(0);
                int toggleButtonState4 = itemListPanel_0.GetToggleButtonState(1);
                int num = toggleButtonState3;
                int num2 = num;
                if ((uint)num2 <= 1u)
                {
                    if (toggleButtonState4 == 0)
                    {
                        main.list_6.AddRange(main._Game.PlayerEmpire.PirateMissions.ResolveWhereRequestingEmpireNot(main._Game.PlayerEmpire));
                    }
                    else
                    {
                        EmpireActivityList empireActivityList = main._Game.PlayerEmpire.PirateMissions.ResolveWhereRequestingEmpireNot(main._Game.PlayerEmpire);
                        switch (toggleButtonState4)
                        {
                            case 1:
                                main.list_6.AddRange(empireActivityList.ResolveActivitiesByType(EmpireActivityType.Smuggle));
                                break;
                            case 2:
                                main.list_6.AddRange(empireActivityList.ResolveActivitiesByType(EmpireActivityType.Attack));
                                break;
                            case 3:
                                main.list_6.AddRange(empireActivityList.ResolveActivitiesByType(EmpireActivityType.Defend));
                                break;
                        }
                    }
                }
                if (toggleButtonState3 == 0 || toggleButtonState3 == 2)
                {
                    int num3 = toggleButtonState4;
                    int num4 = num3;
                    if (num4 == 0 || num4 == 2)
                    {
                        EmpireActivityList empireActivityList2 = main._Game.Galaxy.PirateMissions.ResolveByKnownAttackTargetsNotRequestedBy(main._Game.PlayerEmpire);
                        empireActivityList2.StripMissionsWithTargetEmpire(main._Game.PlayerEmpire);
                        main.list_6.AddRange(empireActivityList2);
                    }
                    if (toggleButtonState4 == 0 || toggleButtonState4 == 3)
                    {
                        EmpireActivityList collection = main._Game.Galaxy.PirateMissions.ResolveByAllowedDefendTargetsNotRequestedBy(main._Game.PlayerEmpire);
                        main.list_6.AddRange(collection);
                    }
                    if (toggleButtonState4 == 0 || toggleButtonState4 == 1)
                    {
                        EmpireActivityList empireActivityList3 = main._Game.Galaxy.PirateMissions.ResolveByTypeKnownTarget(EmpireActivityType.Smuggle, main._Game.PlayerEmpire);
                        if (empireActivityList3 != null && empireActivityList3.Count() > 0)
                        {
                            for (int j = 0; j < empireActivityList3.Count(); j++)
                            {
                                if (!main._Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivityList3[j]))
                                {
                                    main.list_6.Add(empireActivityList3[j]);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                int toggleButtonState5 = itemListPanel_0.GetToggleButtonState(0);
                int toggleButtonState6 = itemListPanel_0.GetToggleButtonState(1);
                EmpireActivityList empireActivityList4 = new EmpireActivityList();
                switch (toggleButtonState5)
                {
                    case 0:
                        empireActivityList4 = main._Game.PlayerEmpire.PirateMissions;
                        break;
                    case 1:
                        empireActivityList4 = main._Game.PlayerEmpire.PirateMissions.ResolveAssigned();
                        break;
                    case 2:
                        empireActivityList4 = main._Game.PlayerEmpire.PirateMissions.ResolveUnassigned();
                        break;
                }
                switch (toggleButtonState6)
                {
                    case 0:
                        main.list_6.AddRange(empireActivityList4);
                        break;
                    case 1:
                        main.list_6.AddRange(empireActivityList4.ResolveActivitiesByType(EmpireActivityType.Smuggle));
                        break;
                    case 2:
                        main.list_6.AddRange(empireActivityList4.ResolveActivitiesByType(EmpireActivityType.Attack));
                        break;
                    case 3:
                        main.list_6.AddRange(empireActivityList4.ResolveActivitiesByType(EmpireActivityType.Defend));
                        break;
                }
            }
            itemListPanel_0.BindData(main.list_6.ToArray());
        }

        public static void SetResourceFilter(Main main, string input)
        {
            byte result = 0;
            bool flag = false;
            string[] array = input.Split(' ');
            if (array.Length > 1)
            {
                flag = byte.TryParse(array[1], out result);
            }
            if (flag)
            {
                resourceFilter = result;
            }
            else
            {
                resourceFilter = -1;
            }
        }

        public static void btnDesignsUpgrade_Click(Main main, object sender, EventArgs e)
        {
            if (main._Game.PlayerEmpire.ControlDesigns && main.GenerateAutomationMessageBox(TextResolver.GetText("Ship Design")).Show((IWin32Window)main).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                main._Game.PlayerEmpire.ControlDesigns = false;
            }
            DesignList selectedDesigns = main.ctlDesignsList.SelectedDesigns;
            if (selectedDesigns == null || selectedDesigns.Count() <= 0)
            {
                return;
            }
            Design design = null;
            for (int i = 0; i < selectedDesigns.Count(); i++)
            {
                Design design2 = selectedDesigns[i];
                design = design2.Clone();
                for (int j = 0; j < design.Components.Count(); j++)
                {
                    Component component = design.Components[j];
                    Component component2 = null;
                    switch (component.Category)
                    {
                        case ComponentCategoryType.WeaponBeam:
                        case ComponentCategoryType.Shields:
                        case ComponentCategoryType.HyperDrive:
                        case ComponentCategoryType.Reactor:
                        case ComponentCategoryType.WeaponSuperBeam:
                        case ComponentCategoryType.WeaponSuperArea:
                            {
                                ComponentType type = component.Type;
                                ComponentType componentType = type;
                                component2 = ((componentType != ComponentType.WeaponGravityBeam && componentType - 60 > ComponentType.WeaponBeam && componentType - 65 > ComponentType.WeaponBeam) ? main._Game.PlayerEmpire.Research.EvaluateDesiredComponent(component.Category, ShipDesignFocus.Balanced, preferLatest: true) : main._Game.PlayerEmpire.Research.EvaluateDesiredComponent(component.Type, ShipDesignFocus.Balanced, preferLatest: true));
                                break;
                            }
                        case ComponentCategoryType.WeaponTorpedo:
                        case ComponentCategoryType.WeaponSuperTorpedo:
                            component2 = component.Type switch
                            {
                                ComponentType.WeaponBombard => main._Game.PlayerEmpire.ResolveLatestBombardWeapon(),
                                ComponentType.WeaponMissile => main._Game.PlayerEmpire.ResolveLatestMissileWeapon(),
                                ComponentType.WeaponSuperTorpedo => main._Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponSuperTorpedo),
                                ComponentType.WeaponSuperMissile => main._Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponSuperMissile),
                                _ => main._Game.PlayerEmpire.ResolveLatestStandardTorpedoWeapon(),
                            };
                            break;
                        case ComponentCategoryType.Engine:
                            if (component.Type == ComponentType.EngineMainThrust)
                            {
                                component2 = main._Game.PlayerEmpire.Research.EvaluateDesiredComponent(ComponentType.EngineMainThrust, ShipDesignFocus.Balanced, preferLatest: true);
                            }
                            else if (component.Type == ComponentType.EngineVectoring)
                            {
                                component2 = main._Game.PlayerEmpire.Research.EvaluateDesiredComponent(ComponentType.EngineVectoring, ShipDesignFocus.Balanced, preferLatest: true);
                            }
                            break;
                        default:
                            component2 = main._Game.PlayerEmpire.Research.EvaluateDesiredComponent(component.Type, ShipDesignFocus.Balanced, preferLatest: true);
                            break;
                    }
                    if (component2 != null && component2.ComponentID != component.ComponentID)
                    {
                        design.Components[j] = component2;
                    }
                }
                Component latestComponent = main._Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.HabitationHabModule);
                Component latestComponent2 = main._Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.HabitationLifeSupport);
                ComponentImprovement componentImprovement = new ComponentImprovement(latestComponent);
                ComponentImprovement componentImprovement2 = new ComponentImprovement(latestComponent2);
                if (main._Game.PlayerEmpire != null && main._Game.PlayerEmpire.Research != null)
                {
                    if (latestComponent != null)
                    {
                        componentImprovement = main._Game.PlayerEmpire.Research.ResolveImprovedComponentValues(latestComponent);
                    }
                    if (latestComponent2 != null)
                    {
                        componentImprovement2 = main._Game.PlayerEmpire.Research.ResolveImprovedComponentValues(latestComponent2);
                    }
                }
                int num = main._Game.Galaxy.DetermineHabModulesRequired(componentImprovement, design);
                int num2 = main._Game.Galaxy.DetermineLifeSupportRequired(componentImprovement2, design);
                int num3 = 0;
                int num4 = 0;
                foreach (Component component3 in design.Components)
                {
                    if (component3.Type == ComponentType.HabitationHabModule)
                    {
                        num3++;
                    }
                    else if (component3.Type == ComponentType.HabitationLifeSupport)
                    {
                        num4++;
                    }
                }
                int num5 = num - num3;
                int num6 = num2 - num4;
                for (int k = 0; k < num5; k++)
                {
                    design.Components.Add(componentImprovement.ImprovedComponent);
                }
                for (int l = 0; l < num6; l++)
                {
                    design.Components.Add(componentImprovement2.ImprovedComponent);
                }
                if (design.IsEquivalent(design2))
                {
                    continue;
                }
                string text = design2.Name;
                if (text.Contains(" "))
                {
                    bool flag = false;
                    int num7 = text.LastIndexOf(" ");
                    string text2 = string.Empty;
                    string text3 = string.Empty;
                    if (num7 >= 0)
                    {
                        text3 = text.Substring(0, num7);
                        text2 = text.Substring(num7, text.Length - num7).Trim();
                    }
                    if (text2.Contains("Mk") && text2.Length > 2)
                    {
                        string s = text2.Substring(2, text2.Length - 2).Trim();
                        int result = 0;
                        int.TryParse(s, out result);
                        if (result > 0)
                        {
                            result++;
                            int num8 = result;
                            string text4 = "Mk" + num8;
                            text = text3 + " " + text4;
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        text += " Mk2";
                    }
                }
                else
                {
                    text += " Mk2";
                }
                if (design2 == main._Game.PlayerEmpire.WeaponsResearchStation || design2 == main._Game.PlayerEmpire.HighTechResearchStation || design2 == main._Game.PlayerEmpire.EnergyResearchStation || design2 == main._Game.PlayerEmpire.MonitoringStationCurrentDesign || design2 == main._Game.PlayerEmpire.DefenseBaseDesign)
                {
                    text = design2.Name;
                }
                design.Name = text;
                design.DateCreated = main._Game.Galaxy.CurrentStarDate;
                design.Empire = main._Game.PlayerEmpire;
                design.BuildCount = 0;
                design.IsObsolete = false;
                design.IsManuallyCreated = true;
                design.ReDefine();
                design2.IsObsolete = true;
                main._Game.PlayerEmpire.Designs.Add(design);
            }
            Design design3 = null;
            if (selectedDesigns.Count() == 1)
            {
                design3 = design;
            }
            main.method_307(design3);
        }

        public static int GetNumberOfShipsOfSubRole(Main main, BuiltObjectSubRole subrole)
        {
            int num = 0;
            BuiltObjectList builtObjectsBySubRole = main._Game.PlayerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(subrole);
            if (builtObjectsBySubRole != null)
            {
                num += builtObjectsBySubRole.Count();
            }
            BuiltObjectList builtObjectsBySubRole2 = main._Game.PlayerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(subrole);
            if (builtObjectsBySubRole != null)
            {
                num += builtObjectsBySubRole2.Count();
            }
            return num;
        }

        public static void method_532_SetExpansionPlanner(string expansionPlannerSetting)
        {
            expansionPlannerSelection = expansionPlannerSetting;
        }

        public static string method_160_SetExpansionPlannerCombobox(Main main)
        {
            return expansionPlannerSelection;
        }

        private static void SetAllEmpiresToTechLevel(Main main, int techLevel)
        {
            foreach (Empire empire in main._Game.Galaxy.Empires)
            {
                if (empire != main._Game.PlayerEmpire && empire != null && empire.Research != null && empire.Research.TechTree != null)
                {
                    empire.Research.ResearchQueueEnergy.Clear();
                    empire.Research.ResearchQueueHighTech.Clear();
                    empire.Research.ResearchQueueWeapons.Clear();
                    for (int i = 0; i < empire.Research.TechTree.Count; i++)
                    {
                        empire.Research.TechTree[i].IsRushing = false;
                    }
                    empire.Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.SetTechTreeLevel(main._Game.Galaxy, empire.Research.TechTree, empire.DominantRace, techLevel, isPirate: false);
                    empire.Research.Update(empire.DominantRace);
                    empire.ReviewResearchAbilities();
                    empire.ReviewDesignsBuiltObjectsImprovedComponents();
                }
            }
        }
    }
}
