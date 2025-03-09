// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// DistantWorlds.Main
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Management;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using BaconDistantWorlds;
using ExpansionMod;
using DistantWorlds.Controls;
using DistantWorlds.Types;
//using Ionic.Zlib;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Graphics;
//using SlimDX.DirectSound;
using ExpansionMod.HotKeyMapping;
using ExpansionMod.Objects;
using System.Collections.Concurrent;

namespace DistantWorlds {

  public partial class Main {


        private void mainView_MouseMove(object sender, MouseEventArgs e)
        {
            object obj = null;
            Keyboard keyboard = new Keyboard();
            Point point = PointToClient(MouseHelper.GetCursorPosition());
            if (diplomaticMessageQueue_0.CheckHover(point) || itemListCollectionPanel_0.DetectHoveredElement(point))
            {
                return;
            }
            int int_ = point.X;
            int int_2 = point.Y;
            method_151(ref int_, ref int_2);
            Point point2 = PointToClient(MouseHelper.GetCursorPosition());
            int_17 = point2.X;
            int_18 = point2.Y;
            int_15 = int_;
            int_16 = int_2;
            Cursor cursor = null;
            string text = string.Empty;
            ShipAction shipAction = null;
            string empty = string.Empty;
            if (mouseHoverMode_0 != 0)
            {
                obj = method_143(int_, int_2, bool_28: false);
                switch (mouseHoverMode_0)
                {
                    case MouseHoverMode.SetFleetAttackPoint:
                        {
                            cursor = cursor_12;
                            if (!bool_18 || shipGroup_2 == null)
                            {
                                break;
                            }
                            bool flag2 = false;
                            string arg2 = "(" + TextResolver.GetText("None") + ")";
                            if (obj != null)
                            {
                                if (obj is Habitat)
                                {
                                    Habitat habitat2 = (Habitat)obj;
                                    if (habitat2.Empire != null && habitat2.Empire != _Game.PlayerEmpire)
                                    {
                                        arg2 = habitat2.Name;
                                        flag2 = true;
                                    }
                                }
                                else if (obj is BuiltObject)
                                {
                                    BuiltObject builtObject2 = (BuiltObject)obj;
                                    if (builtObject2.Role == BuiltObjectRole.Base && builtObject2.Empire != null && builtObject2.Empire != _Game.PlayerEmpire)
                                    {
                                        arg2 = builtObject2.Name;
                                        flag2 = true;
                                    }
                                }
                            }
                            text = ((!flag2) ? (string.Format(TextResolver.GetText("Set Fleet Target X"), arg2) + "  (" + TextResolver.GetText("valid target is colony or base of another empire") + ")") : (string.Format(TextResolver.GetText("Set Fleet Target X"), arg2) + "  (" + TextResolver.GetText("click to set") + ")"));
                            break;
                        }
                    case MouseHoverMode.SetFleetHomeBase:
                        {
                            cursor = cursor_13;
                            if (!bool_18 || shipGroup_2 == null)
                            {
                                break;
                            }
                            bool flag3 = false;
                            string arg3 = "(" + TextResolver.GetText("None") + ")";
                            if (obj != null)
                            {
                                BuiltObject shipToRefuel = _Game.PlayerEmpire.BuiltObjects.FindFirstBuiltObject(BuiltObjectRole.Military);
                                if (obj is Habitat)
                                {
                                    Habitat habitat3 = (Habitat)obj;
                                    if (_Game.Galaxy.IsStellarObjectDockable(habitat3, _Game.PlayerEmpire) && _Game.Galaxy.CheckEmpireCanRefuelAtEmpire(shipToRefuel, habitat3.Empire, _Game.PlayerEmpire))
                                    {
                                        arg3 = habitat3.Name;
                                        flag3 = true;
                                    }
                                }
                                else if (obj is BuiltObject)
                                {
                                    BuiltObject builtObject3 = (BuiltObject)obj;
                                    if (builtObject3.Role == BuiltObjectRole.Base && _Game.Galaxy.IsStellarObjectDockable(builtObject3, _Game.PlayerEmpire) && _Game.Galaxy.CheckEmpireCanRefuelAtEmpire(shipToRefuel, builtObject3.Empire, _Game.PlayerEmpire))
                                    {
                                        arg3 = builtObject3.Name;
                                        flag3 = true;
                                    }
                                }
                            }
                            text = ((!flag3) ? (string.Format(TextResolver.GetText("Set Fleet Home Base X"), arg3) + "  (" + TextResolver.GetText("valid home base is friendly refueling point") + ")") : (string.Format(TextResolver.GetText("Set Fleet Home Base X"), arg3) + "  (" + TextResolver.GetText("click to set") + ")"));
                            break;
                        }
                    case MouseHoverMode.SetEventActionTarget:
                        {
                            bool flag = true;
                            cursor = cursor_16;
                            string arg = string.Empty;
                            if (obj != null && obj is BuiltObject)
                            {
                                BuiltObject builtObject = (BuiltObject)obj;
                                arg = builtObject.Name;
                            }
                            else if (obj != null && obj is Habitat)
                            {
                                Habitat habitat = (Habitat)obj;
                                arg = habitat.Name;
                            }
                            else
                            {
                                flag = false;
                                cursor = cursor_17;
                            }
                            text = ((!flag) ? TextResolver.GetText("SetEventActionTargetInvalid Description") : string.Format(TextResolver.GetText("SetEventActionTarget Description"), arg));
                            break;
                        }
                }
            }
            else if (pnlGameEditor.Visible && gameEditorSelector.EditMode != 0)
            {
                switch (gameEditorSelector.EditMode)
                {
                    case EditorMode.System:
                        cursor = cursor_18;
                        empty = TextResolver.GetText("Left-click to place System");
                        break;
                    case EditorMode.GasCloud:
                        cursor = cursor_19;
                        empty = TextResolver.GetText("Left-click to place Gas Cloud");
                        break;
                    case EditorMode.Star:
                        cursor = cursor_20;
                        empty = TextResolver.GetText("Left-click to place Star");
                        break;
                    case EditorMode.Planet:
                        cursor = cursor_21;
                        empty = TextResolver.GetText("Left-click to place Planet");
                        break;
                    case EditorMode.Moon:
                        cursor = cursor_22;
                        empty = TextResolver.GetText("Left-click to place Moon");
                        break;
                    case EditorMode.Asteroid:
                        cursor = cursor_23;
                        empty = TextResolver.GetText("Left-click to place Asteroid");
                        break;
                    case EditorMode.AsteroidField:
                        cursor = cursor_24;
                        empty = TextResolver.GetText("Left-click to place Asteroid Field");
                        break;
                    case EditorMode.BuiltObject:
                        cursor = cursor_25;
                        empty = TextResolver.GetText("Left-click to place Ship or Base");
                        break;
                    case EditorMode.Colony:
                        cursor = cursor_26;
                        empty = TextResolver.GetText("Left-click to place Colony");
                        break;
                    case EditorMode.AlienRace:
                        cursor = cursor_27;
                        empty = TextResolver.GetText("Left-click to place Independent Alien Race");
                        break;
                    case EditorMode.Creature:
                        cursor = cursor_29;
                        empty = TextResolver.GetText("Left-click to place Creature");
                        break;
                    case EditorMode.Pirates:
                        cursor = cursor_30;
                        empty = TextResolver.GetText("Left-click to place Pirate Faction");
                        break;
                    case EditorMode.Ruins:
                    case EditorMode.RuinsSpecialGovernment:
                    case EditorMode.RuinsSuperWeapon:
                    case EditorMode.RuinsSleepingRace:
                    case EditorMode.RuinsRefugees:
                    case EditorMode.RuinsOrigins:
                    case EditorMode.RuinsLostShip:
                    case EditorMode.RuinsLostColony:
                        cursor = cursor_31;
                        empty = TextResolver.GetText("Left-click to place Ruins");
                        break;
                    case EditorMode.DebrisField:
                        cursor = cursor_10;
                        empty = TextResolver.GetText("Left-click to place Debris Field");
                        break;
                    case EditorMode.EmpireExploration:
                        cursor = cursor_32;
                        empty = TextResolver.GetText("Left-click to explore system, right-click to unexplore");
                        break;
                    case EditorMode.ClearItems:
                        cursor = cursor_33;
                        empty = TextResolver.GetText("Left-click to erase item");
                        break;
                    case EditorMode.ClearColony:
                        cursor = cursor_34;
                        empty = TextResolver.GetText("Left-click to erase Colony");
                        break;
                    case EditorMode.ClearAlienRace:
                        cursor = cursor_35;
                        empty = TextResolver.GetText("Left-click to erase Alien Race");
                        break;
                    case EditorMode.ClearRuins:
                        cursor = cursor_36;
                        empty = TextResolver.GetText("Left-click to erase Ruins");
                        break;
                    case EditorMode.ClearAsteroidField:
                        cursor = cursor_37;
                        empty = TextResolver.GetText("Left-click to erase Asteroid Field");
                        break;
                    case EditorMode.Character:
                        cursor = cursor_28;
                        empty = TextResolver.GetText("Left-click to place Character");
                        break;
                }
            }
            else if (bool_18 && !keyboard.CtrlKeyDown)
            {
                obj = method_143(int_, int_2, bool_28: false);
                Empire empire = null;
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                bool flag4 = false;
                bool flag5 = false;
                bool flag6 = false;
                bool flag7 = false;
                bool flag8 = false;
                bool flag9 = false;
                if (builtObject_5 != null)
                {
                    empire = builtObject_5.Empire;
                    num = builtObject_5.FirepowerRaw;
                    num3 = builtObject_5.BombardWeaponPower;
                    num2 = builtObject_5.FighterCapacity;
                    num4 = builtObject_5.TroopCapacityRemaining;
                    num5 = 0;
                    if (builtObject_5.Troops != null)
                    {
                        num5 = builtObject_5.Troops.TotalAttackStrength;
                        num6 = builtObject_5.Troops.Count;
                    }
                    if (builtObject_5.AssaultStrength > 0 && builtObject_5.AssaultRange > 0)
                    {
                        flag9 = true;
                    }
                    if (builtObject_5.Role == BuiltObjectRole.Military)
                    {
                        flag4 = true;
                    }
                    if ((builtObject_5.ExtractionGas > 0 || builtObject_5.ExtractionLuxury > 0 || builtObject_5.ExtractionMine > 0) && builtObject_5.SubRole != BuiltObjectSubRole.ResupplyShip)
                    {
                        flag5 = true;
                    }
                    if (builtObject_5.IsPlanetDestroyer)
                    {
                        flag7 = true;
                    }
                    flag6 = builtObject_5.IsShipYard;
                    if (builtObject_5.SubRole == BuiltObjectSubRole.ResupplyShip)
                    {
                        flag8 = true;
                    }
                }
                else if (shipGroup_2 != null)
                {
                    empire = shipGroup_2.Empire;
                    num = shipGroup_2.TotalFirepower;
                    num2 = shipGroup_2.TotalFighterCount;
                    num3 = shipGroup_2.TotalBombardPower;
                    num4 = 0;
                    num5 = shipGroup_2.TotalTroopAttackStrength;
                    if (num5 > 0)
                    {
                        num6 = 1;
                    }
                    if (shipGroup_2.TotalAvailableBoardingAssaultStrength(_Game.Galaxy.CurrentDateTime) > 0)
                    {
                        flag9 = true;
                    }
                    flag4 = true;
                    flag5 = false;
                    flag6 = false;
                }
                if (obj == null)
                {
                    habitat_1 = null;
                    creature_0 = null;
                    builtObject_6 = null;
                    habitat_2 = null;
                    habitat_2 = null;
                    Habitat habitat4 = null;
                    Habitat habitat5 = _Game.Galaxy.FastFindNearestSystem(int_, int_2);
                    if (habitat5 != null)
                    {
                        SystemInfo systemInfo = _Game.Galaxy.Systems[habitat5.SystemIndex];
                        if (systemInfo != null)
                        {
                            habitat4 = _Game.Galaxy.FindNearestHabitatInSystem(systemInfo, int_, int_2);
                        }
                    }
                    if (habitat4 != null)
                    {
                        double num7 = _Game.Galaxy.CalculateDistance(int_, int_2, habitat4.Xpos, habitat4.Ypos);
                        num7 -= (double)(habitat4.Diameter / 2);
                        if (num7 > 0.0 && num7 < Galaxy.MouseHoverHabitatProximityRange)
                        {
                            habitat_2 = habitat4;
                        }
                    }
                    if (habitat_2 != null)
                    {
                        if (_Game.PlayerEmpire.CheckSystemExplored(habitat_2.SystemIndex))
                        {
                            bool flag10 = false;
                            if (habitat_2.Empire != null && habitat_2.Empire != _Game.Galaxy.IndependentEmpire && habitat_2.Empire != empire && flag4 && empire != null)
                            {
                                DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(habitat_2.Empire);
                                if (diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions || diplomaticRelation.Type == DiplomaticRelationType.War)
                                {
                                    flag10 = true;
                                }
                            }
                            if (flag10)
                            {
                                cursor = cursor_9;
                                text = TextResolver.GetText("Blockade X") + " " + Galaxy.ResolveDescription(habitat_2.Category) + " " + habitat_2.Name;
                                shipAction = new ShipAction(BuiltObjectMissionType.Blockade, habitat_2);
                            }
                            else
                            {
                                cursor = cursor_1;
                                text = string.Format(TextResolver.GetText("Move to X"), Galaxy.ResolveDescription(habitat_2.Category) + " " + habitat_2.Name);
                                int num8 = int_15 - (int)habitat_2.Xpos;
                                int num9 = int_16 - (int)habitat_2.Ypos;
                                shipAction = new ShipAction(offset: new Point(num8, num9), missionType: BuiltObjectMissionType.Move, target: habitat_2, design: null);
                            }
                        }
                        else
                        {
                            cursor = cursor_0;
                            Point offset2 = new Point(int_, int_2);
                            text = TextResolver.GetText("Move here");
                            string text2 = text;
                            text = text2 + " (" + int_.ToString("0,K") + ", " + int_2.ToString("0,K") + ")";
                            shipAction = new ShipAction(BuiltObjectMissionType.Move, null, offset2, null);
                        }
                    }
                    else if (!flag6)
                    {
                        cursor = cursor_0;
                        Point offset3 = new Point(int_, int_2);
                        text = TextResolver.GetText("Move here");
                        string text2 = text;
                        text = text2 + " (" + int_.ToString("0,K") + ", " + int_2.ToString("0,K") + ")";
                        shipAction = new ShipAction(BuiltObjectMissionType.Move, null, offset3, null);
                    }
                }
                else if (obj is Habitat)
                {
                    habitat_1 = (Habitat)obj;
                    builtObject_6 = null;
                    creature_0 = null;
                    habitat_2 = null;
                    if (_Game.PlayerEmpire.CheckSystemExplored(habitat_1.SystemIndex))
                    {
                        if ((habitat_1.Category == HabitatCategoryType.GasCloud || habitat_1.Type == HabitatType.BlackHole) && !flag6 && !flag8)
                        {
                            cursor = cursor_0;
                            Point offset4 = new Point(int_, int_2);
                            text = TextResolver.GetText("Move here");
                            string text2 = text;
                            text = text2 + " (" + int_.ToString("0,K") + ", " + int_2.ToString("0,K") + ")";
                            shipAction = new ShipAction(BuiltObjectMissionType.Move, null, offset4, null);
                        }
                        if (habitat_1.Ruin != null && habitat_1.Ruin.PlayerEmpireEncountered && _Game.Galaxy.CheckRuinsHaveBenefit(habitat_1.Ruin, _Game.PlayerEmpire))
                        {
                            shipAction_0 = null;
                            return;
                        }
                        bool flag11 = false;
                        if ((num > 0 || num5 > 0 || num2 > 0) && habitat_1.Empire != null && habitat_1.Empire != empire && habitat_1.Population != null && habitat_1.Population.TotalAmount > 0L)
                        {
                            flag11 = true;
                            if (empire != null && habitat_1.Empire != null)
                            {
                                DiplomaticRelation diplomaticRelation2 = empire.ObtainDiplomaticRelation(habitat_1.Empire);
                                if (diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation2.Type == DiplomaticRelationType.Protectorate || diplomaticRelation2.Type == DiplomaticRelationType.FreeTradeAgreement)
                                {
                                    flag11 = false;
                                }
                            }
                        }
                        else if (flag7 && _Game.Galaxy.CanDestroyHabitat(builtObject_5, habitat_1))
                        {
                            flag11 = true;
                            if (empire != null && habitat_1.Empire != null)
                            {
                                DiplomaticRelation diplomaticRelation3 = empire.ObtainDiplomaticRelation(habitat_1.Empire);
                                if (diplomaticRelation3.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation3.Type == DiplomaticRelationType.Protectorate || diplomaticRelation3.Type == DiplomaticRelationType.FreeTradeAgreement)
                                {
                                    flag11 = false;
                                }
                            }
                        }
                        else if (num5 > 0 && habitat_1.Empire == _Game.Galaxy.IndependentEmpire && habitat_1.Empire != empire && habitat_1.Population != null && habitat_1.Population.TotalAmount > 0L)
                        {
                            flag11 = true;
                        }
                        if (flag11)
                        {
                            if (keyboard.ShiftKeyDown && num3 > 0 && habitat_1.Empire != null && habitat_1.Empire != _Game.PlayerEmpire)
                            {
                                cursor = cursor_3;
                                string text3 = string.Format(TextResolver.GetText("Bombard X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                                text = text3;
                                shipAction = new ShipAction(BuiltObjectMissionType.Bombard, habitat_1);
                            }
                            else if (habitat_1.Empire != null && habitat_1.Empire != _Game.PlayerEmpire && _Game.PlayerEmpire.PirateEmpireBaseHabitat != null && flag9 && num6 <= 0)
                            {
                                cursor = cursor_15;
                                string text4 = string.Format(TextResolver.GetText("Raid X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                                text = text4;
                                shipAction = new ShipAction(BuiltObjectMissionType.Raid, habitat_1);
                            }
                            else if (keyboard.AltKeyDown && flag9 && habitat_1.Empire != null && habitat_1.Empire != _Game.PlayerEmpire && _Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                            {
                                cursor = cursor_15;
                                string text5 = string.Format(TextResolver.GetText("Raid X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                                text = text5;
                                shipAction = new ShipAction(BuiltObjectMissionType.Raid, habitat_1);
                            }
                            else
                            {
                                cursor = cursor_2;
                                string text6 = string.Format(TextResolver.GetText("Attack X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                                if (flag7)
                                {
                                    text6 = string.Format(TextResolver.GetText("Destroy X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                                }
                                text = text6;
                                shipAction = new ShipAction(BuiltObjectMissionType.Attack, habitat_1);
                            }
                        }
                        if (flag4 && habitat_1.Empire == empire)
                        {
                            cursor = cursor_5;
                            text = string.Format(TextResolver.GetText("Patrol X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.Patrol, habitat_1);
                        }
                        else if (flag4 && (habitat_1.Category == HabitatCategoryType.GasCloud || habitat_1.Category == HabitatCategoryType.Star))
                        {
                            cursor = cursor_5;
                            text = string.Format(TextResolver.GetText("Patrol X"), habitat_1.Name + " " + TextResolver.GetText("system"));
                            shipAction = new ShipAction(BuiltObjectMissionType.Patrol, habitat_1);
                        }
                        if (flag5 && (habitat_1.Empire == null || habitat_1.Empire == _Game.Galaxy.IndependentEmpire))
                        {
                            cursor = cursor_6;
                            text = string.Format(TextResolver.GetText("Mine X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.ExtractResources, habitat_1);
                        }
                        if (num4 >= 100 && habitat_1.Empire == empire && habitat_1.Troops != null && habitat_1.Troops.Count > 0)
                        {
                            cursor = cursor_7;
                            text = string.Format(TextResolver.GetText("Load Troops at X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.LoadTroops, habitat_1);
                        }
                        else if (num4 >= 100 && habitat_1.Empire != empire && habitat_1.InvadingTroops != null && habitat_1.InvadingTroops.Count > 0 && habitat_1.InvadingTroops[0].Empire == empire)
                        {
                            cursor = cursor_7;
                            text = string.Format(TextResolver.GetText("Load Troops at X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.LoadTroops, habitat_1);
                        }
                        if ((num6 > 0 && num4 < 100 && habitat_1.Empire == empire) || (num5 > 0 && habitat_1.Empire == empire && habitat_1.Troops != null && habitat_1.Troops.Count == 0))
                        {
                            cursor = cursor_8;
                            text = string.Format(TextResolver.GetText("Unload Troops at X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.UnloadTroops, habitat_1);
                        }
                        if (builtObject_5 != null && builtObject_5.SubRole == BuiltObjectSubRole.ColonyShip && (habitat_1.Empire == null || habitat_1.Empire == _Game.Galaxy.IndependentEmpire))
                        {
                            int newPopulationAmount = 0;
                            if (builtObject_5.Empire != null && builtObject_5.Empire.CanBuiltObjectColonizeHabitat(builtObject_5, habitat_1, out newPopulationAmount) && builtObject_5.Empire.CanEmpireColonizeHabitatRange(builtObject_5.Empire, habitat_1))
                            {
                                cursor = cursor_4;
                                text = string.Format(TextResolver.GetText("Colonize X"), Galaxy.ResolveDescription(habitat_1.Category) + " " + habitat_1.Name);
                                shipAction = new ShipAction(BuiltObjectMissionType.Colonize, habitat_1);
                            }
                        }
                    }
                    else
                    {
                        cursor = cursor_0;
                        Point offset5 = new Point(int_, int_2);
                        text = TextResolver.GetText("Move here");
                        string text2 = text;
                        text = text2 + " (" + int_.ToString("0,K") + ", " + int_2.ToString("0,K") + ")";
                        shipAction = new ShipAction(BuiltObjectMissionType.Move, null, offset5, null);
                    }
                }
                else if (obj is BuiltObject)
                {
                    habitat_1 = null;
                    builtObject_6 = (BuiltObject)obj;
                    creature_0 = null;
                    habitat_2 = null;
                    if (_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject_6))
                    {
                        if (builtObject_6.Empire == null && builtObject_6.DamagedComponentCount == 0 && builtObject_6.UnbuiltComponentCount == 0)
                        {
                            shipAction_0 = null;
                            return;
                        }
                        if ((num > 0 || num2 > 0 || num5 > 0) && builtObject_6.Empire != empire && builtObject_6.Empire != _Game.Galaxy.IndependentEmpire)
                        {
                            bool flag12 = true;
                            if (empire != null && builtObject_6.Empire != null)
                            {
                                DiplomaticRelation diplomaticRelation4 = empire.ObtainDiplomaticRelation(builtObject_6.Empire);
                                if (diplomaticRelation4.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation4.Type == DiplomaticRelationType.Protectorate || diplomaticRelation4.Type == DiplomaticRelationType.FreeTradeAgreement)
                                {
                                    flag12 = false;
                                }
                            }
                            if (flag12)
                            {
                                if (flag9 && keyboard.ShiftKeyDown)
                                {
                                    cursor = cursor_14;
                                    text = string.Format(TextResolver.GetText("Capture X"), builtObject_6.Name);
                                    shipAction = new ShipAction(BuiltObjectMissionType.Capture, builtObject_6);
                                }
                                else if (flag9 && keyboard.AltKeyDown && _Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                                {
                                    cursor = cursor_15;
                                    text = string.Format(TextResolver.GetText("Raid X"), builtObject_6.Name);
                                    shipAction = new ShipAction(BuiltObjectMissionType.Raid, builtObject_6);
                                }
                                else
                                {
                                    cursor = cursor_2;
                                    text = string.Format(TextResolver.GetText("Attack X"), builtObject_6.Name);
                                    shipAction = new ShipAction(BuiltObjectMissionType.Attack, builtObject_6);
                                }
                            }
                        }
                        if ((num > 0 || num2 > 0 || num5 > 0) && builtObject_6.Empire == empire && builtObject_6.FirepowerRaw == 0 && builtObject_6 != builtObject_5)
                        {
                            cursor = cursor_10;
                            text = string.Format(TextResolver.GetText("Escort X"), builtObject_6.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.Escort, builtObject_6);
                        }
                        if ((num > 0 || num2 > 0) && builtObject_6.Empire == empire && builtObject_6.Role == BuiltObjectRole.Base)
                        {
                            cursor = cursor_5;
                            text = string.Format(TextResolver.GetText("Patrol X"), builtObject_6.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.Patrol, builtObject_6);
                        }
                        if (flag6 && (builtObject_6.DamagedComponentCount > 0 || builtObject_6.UnbuiltComponentCount > 0) && builtObject_6.BuiltAt == null && builtObject_5 != builtObject_6)
                        {
                            cursor = cursor_11;
                            text = string.Format(TextResolver.GetText("Repair X"), builtObject_6.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.Build, null, builtObject_6);
                        }
                    }
                    else
                    {
                        cursor = cursor_0;
                        Point offset6 = new Point(int_, int_2);
                        text = TextResolver.GetText("Move here");
                        string text2 = text;
                        text = text2 + " (" + int_.ToString("0,K") + ", " + int_2.ToString("0,K") + ")";
                        shipAction = new ShipAction(BuiltObjectMissionType.Move, null, offset6, null);
                    }
                }
                else if (obj is Creature)
                {
                    habitat_1 = null;
                    builtObject_6 = null;
                    creature_0 = (Creature)obj;
                    habitat_2 = null;
                    if (_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(creature_0))
                    {
                        if (num > 0 || num2 > 0)
                        {
                            cursor = cursor_2;
                            text = string.Format(TextResolver.GetText("Attack X"), creature_0.Name);
                            shipAction = new ShipAction(BuiltObjectMissionType.Attack, creature_0);
                        }
                    }
                    else
                    {
                        cursor = cursor_0;
                        Point offset7 = new Point(int_, int_2);
                        text = TextResolver.GetText("Move here");
                        string text2 = text;
                        text = text2 + " (" + int_.ToString("0,K") + ", " + int_2.ToString("0,K") + ")";
                        shipAction = new ShipAction(BuiltObjectMissionType.Move, null, offset7, null);
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                int num10 = int_32 - int_;
                int num11 = int_33 - int_2;
                int_13 += num10;
                int_14 += num11;
                if (Math.Abs(num10) >= 2 || Math.Abs(num11) >= 2)
                {
                    bool_16 = true;
                }
            }
            if (!string.IsNullOrEmpty(text) && mouseHoverMode_0 == MouseHoverMode.Undefined)
            {
                text = TextResolver.GetText("Right-click to") + " " + text;
                text = text + "   (" + TextResolver.GetText("Ctrl-Right-click for more missions") + "...)";
            }
            if (cursor == null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Cursor.Current = Cursors.SizeAll;
                }
                else
                {
                    Cursor.Current = cursor_0;
                    Cursor = cursor_0;
                }
            }
            else
            {
                Control childAtPoint = GetChildAtPoint(point, GetChildAtPointSkip.None);
                if (childAtPoint != null && childAtPoint.Name == "mainView")
                {
                    Cursor = cursor;
                }
                else
                {
                    Cursor = cursor_0;
                }
            }
            string_17 = text;
            if (!string.IsNullOrEmpty(empty))
            {
                string_17 = empty;
            }
            shipAction_0 = shipAction;
        }

        private void method_206()
        {
            string text = string.Empty;
            string value = string.Empty;
            string text2 = string.Empty;
            Point pt = PointToClient(MouseHelper.GetCursorPosition());
            bool_10 = false;
            Control childAtPoint = GetChildAtPoint(pt, GetChildAtPointSkip.Invisible);
            if (childAtPoint != null && childAtPoint != mainView)
            {
                bool_10 = true;
                switch (childAtPoint.Name)
                {
                    case "btnMapCivilianFade":
                        value = btnMapCivilianFade.Hint;
                        break;
                    case "btnMapOverlay1":
                        value = btnMapOverlay1.Hint;
                        break;
                    case "btnMapOverlay2":
                        value = btnMapOverlay2.Hint;
                        break;
                    case "btnMapOverlay3":
                        value = btnMapOverlay3.Hint;
                        break;
                    case "btnMapOverlay4":
                        value = btnMapOverlay4.Hint;
                        break;
                    case "btnMapOverlay5":
                        value = btnMapOverlay5.Hint;
                        break;
                    case "btnMapOverlay6":
                        value = btnMapOverlay6.Hint;
                        break;
                    case "btnMapOverlay7":
                        value = btnMapOverlay7.Hint;
                        break;
                    case "btnMapOverlay8":
                        value = btnMapOverlay8.Hint;
                        break;
                    case "btnSelectionAction1":
                        text = btnSelectionAction1.Hint;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = method_590(btnSelectionAction1.Tag);
                        }
                        break;
                    case "btnSelectionAction2":
                        text = btnSelectionAction2.Hint;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = method_590(btnSelectionAction2.Tag);
                        }
                        break;
                    case "btnSelectionAction3":
                        text = btnSelectionAction3.Hint;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = method_590(btnSelectionAction3.Tag);
                        }
                        break;
                    case "btnSelectionAction4":
                        text = btnSelectionAction4.Hint;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = method_590(btnSelectionAction4.Tag);
                        }
                        break;
                    case "btnSelectionAction5":
                        text = btnSelectionAction5.Hint;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = method_590(btnSelectionAction5.Tag);
                        }
                        break;
                    case "btnSelectionAction6":
                        text = btnSelectionAction6.Hint;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = method_590(btnSelectionAction6.Tag);
                        }
                        break;
                    case "btnSelectionAction7":
                        text = btnSelectionAction7.Hint;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = method_590(btnSelectionAction7.Tag);
                        }
                        break;
                    case "btnSelectionAction8":
                        text = btnSelectionAction8.Hint;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = method_590(btnSelectionAction8.Tag);
                        }
                        break;
                    case "btnGameSpeedIncrease":
                        if (btnGameSpeedIncrease.Visible && btnGameSpeedIncrease.Enabled)
                        {
                            text = TextResolver.GetText("Increase game speed") + " (+)";
                        }
                        break;
                    case "btnGameSpeedDecrease":
                        if (btnGameSpeedDecrease.Visible && btnGameSpeedDecrease.Enabled)
                        {
                            text = TextResolver.GetText("Decrease game speed") + " (-)";
                        }
                        break;
                    case "btnGalacticHistory":
                        if (btnGalacticHistory.Visible && btnGalacticHistory.Enabled)
                        {
                            text2 = TextResolver.GetText("Open Galactic History screen");
                        }
                        break;
                    case "btnHistoryMessages":
                        if (btnHistoryMessages.Visible && btnHistoryMessages.Enabled)
                        {
                            text2 = TextResolver.GetText("Open Message History screen") + " (H)";
                        }
                        break;
                    case "btnSelectionForward":
                        if (btnSelectionForward.Visible && btnSelectionForward.Enabled)
                        {
                            text = TextResolver.GetText("Next selected item") + " (N)";
                        }
                        break;
                    case "btnSelectionBack":
                        if (btnSelectionBack.Visible && btnSelectionBack.Enabled)
                        {
                            text = TextResolver.GetText("Previous selected item") + " (B)";
                        }
                        break;
                    case "btnCycleBasesBack":
                        if (btnCycleBasesBack.Visible)
                        {
                            text = TextResolver.GetText("Previous Space Port") + " (Shift-P)";
                        }
                        break;
                    case "btnCycleColoniesBack":
                        if (btnCycleColoniesBack.Visible)
                        {
                            text = TextResolver.GetText("Previous Colony") + " (Shift-C)";
                        }
                        break;
                    case "btnCycleConstructionBack":
                        if (btnCycleConstructionBack.Visible)
                        {
                            text = TextResolver.GetText("Previous Construction ship") + " (Shift-Y)";
                        }
                        break;
                    case "btnCycleIdleShipsBack":
                        if (btnCycleIdleShipsBack.Visible)
                        {
                            text = TextResolver.GetText("Previous Idle ship") + " (Shift-I)";
                        }
                        break;
                    case "btnCycleMilitaryBack":
                        if (btnCycleMilitaryBack.Visible)
                        {
                            text = TextResolver.GetText("Previous Military ship") + " (Shift-M)";
                        }
                        break;
                    case "btnCycleOtherBack":
                        if (btnCycleOtherBack.Visible)
                        {
                            text = TextResolver.GetText("Previous Exploration or Colony ship") + " (Shift-X)";
                        }
                        break;
                    case "btnCycleShipGroupsBack":
                        if (btnCycleShipGroupsBack.Visible)
                        {
                            text = TextResolver.GetText("Previous Fleet") + " (Shift-F)";
                        }
                        break;
                    case "btnCycleShipStance":
                        if (!btnCycleShipStance.Visible)
                        {
                            break;
                        }
                        text = TextResolver.GetText("Change engagement stance") + " (" + TextResolver.GetText("comma") + ")";
                        if (_Game.SelectedObject == null)
                        {
                            break;
                        }
                        if (_Game.SelectedObject is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)_Game.SelectedObject;
                            if (builtObject.Role == BuiltObjectRole.Military && builtObject.Empire == _Game.PlayerEmpire)
                            {
                                string text3 = text;
                                text = text3 + "   (" + TextResolver.GetText("Currently") + ": " + Galaxy.ResolveEngagementStanceDescription(builtObject) + ")";
                            }
                        }
                        else if (_Game.SelectedObject is ShipGroup)
                        {
                            ShipGroup shipGroup = (ShipGroup)_Game.SelectedObject;
                            if (shipGroup.Empire == _Game.PlayerEmpire)
                            {
                                string text4 = text;
                                text = text4 + "   (" + TextResolver.GetText("Currently") + ": " + Galaxy.ResolveEngagementStanceDescription(shipGroup) + ")";
                            }
                        }
                        break;
                    case "btnCycleBases":
                        if (btnCycleBases.Visible)
                        {
                            text = TextResolver.GetText("Next Space Port") + " (P)";
                        }
                        break;
                    case "btnCycleColonies":
                        if (btnCycleColonies.Visible)
                        {
                            text = TextResolver.GetText("Next Colony") + " (C)";
                        }
                        break;
                    case "btnCycleConstruction":
                        if (btnCycleConstruction.Visible)
                        {
                            text = TextResolver.GetText("Next Construction ship") + " (Y)";
                        }
                        break;
                    case "btnLockView":
                        if (btnLockView.Visible)
                        {
                            text = TextResolver.GetText("Lock/unlock view on selected item") + " (L)";
                        }
                        break;
                    case "btnCycleIdleShips":
                        if (btnCycleIdleShips.Visible)
                        {
                            text = TextResolver.GetText("Next Idle ship") + " (I)";
                        }
                        break;
                    case "btnCycleMilitary":
                        if (btnCycleMilitary.Visible)
                        {
                            text = TextResolver.GetText("Next Military ship") + " (M)";
                        }
                        break;
                    case "btnCycleOther":
                        if (btnCycleOther.Visible)
                        {
                            text = TextResolver.GetText("Next Exploration or Colony ship") + " (X)";
                        }
                        break;
                    case "btnCycleShipGroups":
                        if (btnCycleShipGroups.Visible)
                        {
                            text = TextResolver.GetText("Next Fleet") + " (F)";
                        }
                        break;
                    case "btnSelectionPanelSize":
                        if (btnSelectionPanelSize.Visible)
                        {
                            text = ((!pnlDetailInfo.ContentSizeIsLarge) ? TextResolver.GetText("Enlarge Selection Panel") : TextResolver.GetText("Shrink Selection Panel"));
                        }
                        break;
                    case "btnZoomColony":
                        if (btnZoomColony.Visible)
                        {
                            value = TextResolver.GetText("Zoom to 100%") + " (Home)";
                        }
                        break;
                    case "btnZoomIn":
                        if (btnZoomIn.Visible)
                        {
                            value = TextResolver.GetText("Zoom In") + " (Page Down)";
                        }
                        break;
                    case "btnZoomOut":
                        if (btnZoomOut.Visible)
                        {
                            value = TextResolver.GetText("Zoom Out") + " (Page Up)";
                        }
                        break;
                    case "btnZoomRegion":
                        if (btnZoomRegion.Visible)
                        {
                            value = TextResolver.GetText("Zoom to Galaxy") + " (End)";
                        }
                        break;
                    case "btnZoomSector":
                        if (jQaYpdpkDs.Visible)
                        {
                            value = TextResolver.GetText("Zoom to Sector") + " (Delete)";
                        }
                        break;
                    case "btnZoomSystem":
                        if (btnZoomSystem.Visible)
                        {
                            value = TextResolver.GetText("Zoom to System") + " (Insert)";
                        }
                        break;
                    case "btnZoomSelection":
                        if (btnZoomSelection.Visible)
                        {
                            value = TextResolver.GetText("Zoom to selected item") + " (Backspace)";
                        }
                        break;
                    case "btnSelectNearestMilitary":
                        if (btnSelectNearestMilitary.Visible)
                        {
                            text = TextResolver.GetText("Select nearest available military ship") + " (Z)";
                        }
                        break;
                    case "btnMainViewDisplayToggle":
                        if (btnMainViewDisplayToggle.Visible)
                        {
                            value = TextResolver.GetText("Toggle display detail level") + " (D)";
                        }
                        break;
                    case "btnHelp":
                        if (btnHelp.Visible)
                        {
                            text = TextResolver.GetText("Open Galactopedia Help screen") + " (F1)";
                        }
                        break;
                    case "tbtnBuiltObjects":
                        if (tbtnBuiltObjects.Visible)
                        {
                            text2 = TextResolver.GetText("Open Ships and Bases screen") + " (F11)";
                        }
                        break;
                    case "tbtnColonies":
                        if (tbtnColonies.Visible)
                        {
                            text2 = TextResolver.GetText("Open Colonies screen") + " (F2)";
                        }
                        break;
                    case "tbtnConstructionYards":
                        if (tbtnConstructionYards.Visible)
                        {
                            text2 = TextResolver.GetText("Open Construction Yards screen") + " (F10)";
                        }
                        break;
                    case "tbtnDesigns":
                        if (tbtnDesigns.Visible)
                        {
                            text2 = TextResolver.GetText("Open Designs screen") + " (F8)";
                        }
                        break;
                    case "tbtnEmpires":
                        if (tbtnEmpires.Visible)
                        {
                            text2 = TextResolver.GetText("Open Diplomacy screen") + " (F5)";
                        }
                        break;
                    case "tbtnGalaxyMap":
                        if (tbtnGalaxyMap.Visible)
                        {
                            value = TextResolver.GetText("Open Galaxy Map screen") + " (G)";
                        }
                        break;
                    case "tbtnIntelligenceAgents":
                        if (tbtnIntelligenceAgents.Visible)
                        {
                            text2 = TextResolver.GetText("Open Intelligence Agents screen") + " (F4)";
                        }
                        break;
                    case "tbtnResearch":
                        if (tbtnResearch.Visible)
                        {
                            text2 = TextResolver.GetText("Open Research screen") + " (F7)";
                            IndustryType industryType = method_391();
                            if (industryType != 0)
                            {
                                text2 = text2 + "   (" + string.Format(TextResolver.GetText("requires new X project assignment"), Galaxy.ResolveDescription(industryType)) + ")";
                            }
                        }
                        break;
                    case "tbtnShipGroups":
                        if (tbtnShipGroups.Visible)
                        {
                            text2 = TextResolver.GetText("Open Fleets screen") + " (F12)";
                        }
                        break;
                    case "tbtnTroops":
                        if (tbtnTroops.Visible)
                        {
                            text2 = TextResolver.GetText("Open Troops screen");
                        }
                        break;
                    case "btnEmpirePolicy":
                        if (btnEmpirePolicy.Visible)
                        {
                            text2 = TextResolver.GetText("Open Empire Policy screen");
                        }
                        break;
                    case "btnBuildOrder":
                        if (btnBuildOrder.Visible)
                        {
                            text2 = TextResolver.GetText("Open Build Order screen") + " (F9)";
                        }
                        break;
                    case "btnGameEditor":
                        if (btnGameEditor.Visible)
                        {
                            text2 = TextResolver.GetText("Switch to Game Editor");
                        }
                        break;
                    case "btnEmpireGraphs":
                        if (btnEmpireGraphs.Visible)
                        {
                            text2 = TextResolver.GetText("Open Empire Comparison and Victory Conditions") + " (V)";
                        }
                        break;
                    case "btnEmpireSummary":
                        if (btnEmpireSummary.Visible)
                        {
                            text2 = TextResolver.GetText("Open Your Empire Summary screen") + " (F6)";
                        }
                        break;
                    case "btnExpansionPlanner":
                        if (btnExpansionPlanner.Visible)
                        {
                            text2 = TextResolver.GetText("Open Expansion Planner screen") + " (F3)";
                        }
                        break;
                    case "btnGameMenu":
                        if (btnGameMenu.Visible)
                        {
                            text = TextResolver.GetText("Show Game Menu: load & save, options, exit") + " (Esc)";
                        }
                        break;
                    case "btnPlayPause":
                        if (btnPlayPause.Visible)
                        {
                            text = ((_Game.Galaxy.TimeState != GalaxyTimeState.Paused) ? (TextResolver.GetText("Pause the game") + " (" + TextResolver.GetText("Pause") + " " + TextResolver.GetText("or") + " " + TextResolver.GetText("Spacebar") + ")") : TextResolver.GetText("Resume the game"));
                        }
                        break;
                    case "lstMessages":
                        if (lstMessages.Visible)
                        {
                            text2 = TextResolver.GetText("Messages: click a message for more information");
                        }
                        break;
                    case "pnlSystemMap":
                    case "picSystem":
                        if (picSystem.Visible)
                        {
                            value = TextResolver.GetText("Map: click to move view to a new location");
                        }
                        break;
                    case "pnlDetailInfo":
                    case "pnlInfoPanel":
                        {
                            if (pnlDetailInfo.Visible)
                            {
                                text = TextResolver.GetText("Selection Panel: click to center view on selected item");
                            }
                            Point point = pnlInfoPanel.PointToClient(MouseHelper.GetCursorPosition());
                            point.Offset(pnlDetailInfo.Location.X * -1, pnlDetailInfo.Location.Y * -1);
                            Hotspot hotspot = pnlDetailInfo.Hotspots.ResolveHotspotAtPoint(point.X, point.Y);
                            if (hotspot != null)
                            {
                                hotspot.Hovered = true;
                                pnlDetailInfo.Invalidate();
                                if (!string.IsNullOrEmpty(hotspot.HoverMessage))
                                {
                                    text = hotspot.HoverMessage;
                                }
                            }
                            break;
                        }
                }
            }
            int_11++;
            if (int_11 % int_12 == 0)
            {
                Point point2 = PointToClient(MouseHelper.GetCursorPosition());
                int int_ = point2.X;
                int int_2 = point2.Y;
                method_151(ref int_, ref int_2);
                object obj = method_143(int_, int_2, bool_28: false);
                int_11 = 0;
                if (obj != null)
                {
                    if (obj is BuiltObject)
                    {
                        BuiltObject builtObject2 = (BuiltObject)obj;
                        if (_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject2))
                        {
                            hoverPanel_0.SetData(_Game, builtObject2);
                        }
                        else
                        {
                            hoverPanel_0.SetData(_Game, (Creature)null);
                        }
                    }
                    else if (obj is Habitat)
                    {
                        Habitat habitat = (Habitat)obj;
                        if (_Game.PlayerEmpire.CheckSystemExplored(habitat.SystemIndex))
                        {
                            method_149();
                            Bitmap bitmap = mainView.method_57(habitat.HabitatIndex - int_28);
                            if (habitat.Type == HabitatType.SuperNova)
                            {
                                bitmap = bitmap_206[habitat.NovaImageIndexMajor];
                            }
                            else if (habitat.Category == HabitatCategoryType.Star)
                            {
                                bitmap = bitmap_196[habitat.MapPictureRef];
                            }
                            if (bitmap == null)
                            {
                                bitmap = mainView.method_54(habitat, 32);
                            }
                            hoverPanel_0.SetData(_Game, habitat, bitmap);
                        }
                        else
                        {
                            hoverPanel_0.SetData(_Game, (Creature)null);
                        }
                    }
                    else if (obj is Creature)
                    {
                        Creature creature = (Creature)obj;
                        if (_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(creature))
                        {
                            hoverPanel_0.SetData(_Game, creature);
                        }
                        else
                        {
                            hoverPanel_0.SetData(_Game, (Creature)null);
                        }
                    }
                }
                else
                {
                    hoverPanel_0.SetData(_Game, (Creature)null);
                }
            }
            if (!string.IsNullOrEmpty(text))
            {
                string_17 = text;
            }
            if (!string.IsNullOrEmpty(value))
            {
                string_18 = value;
            }
            else
            {
                string_18 = string.Empty;
            }
            if (!string.IsNullOrEmpty(text2))
            {
                string_19 = text2;
            }
            else
            {
                string_19 = string.Empty;
            }
        }

        internal Control method_207(Point point_0)
        {
            foreach (Control control in base.Controls)
            {
                if (control.Visible && control.Enabled && control != mainView && control.Left <= point_0.X && control.Left + control.Width >= point_0.X && control.Top <= point_0.Y && control.Top + control.Height >= point_0.Y)
                {
                    return control;
                }
            }
            return null;
        }

        public void method_208(object object_7)
        {
            method_209(object_7, bool_28: true);
        }

        internal void method_209(object object_7, bool bool_28)
        {
            if (_Game == null)
            {
                return;
            }
            if (!_Game.GodMode)
            {
                if (object_7 is BuiltObject && !_Game.PlayerEmpire.IsObjectVisibleToThisEmpire((BuiltObject)object_7))
                {
                    return;
                }
                if (object_7 is Fighter)
                {
                    if (!_Game.PlayerEmpire.IsObjectVisibleToThisEmpire((Fighter)object_7))
                    {
                        return;
                    }
                }
                else if (object_7 is Habitat)
                {
                    if (_Game.PlayerEmpire.CheckSystemVisibilityStatus(((Habitat)object_7).SystemIndex) == SystemVisibilityStatus.Unexplored)
                    {
                        return;
                    }
                }
                else if (object_7 is Creature)
                {
                    if (!_Game.PlayerEmpire.IsObjectVisibleToThisEmpire((Creature)object_7))
                    {
                        return;
                    }
                }
                else if (object_7 is ShipGroup)
                {
                    if (!_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(((ShipGroup)object_7).LeadShip))
                    {
                        return;
                    }
                }
                else if (object_7 is BuiltObjectList)
                {
                    BuiltObjectList builtObjectList = (BuiltObjectList)object_7;
                    for (int i = 0; i < builtObjectList.Count; i++)
                    {
                        BuiltObject objectToTest = builtObjectList[i];
                        if (!_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(objectToTest))
                        {
                            return;
                        }
                    }
                }
            }
            BuiltObjectList builtObjectList_ = new BuiltObjectList();
            if (object_7 != null)
            {
                _Game.SelectedObject = object_7;
                btnZoomSelection.Enabled = true;
                if (object_7 is StellarObject)
                {
                    builtObjectList_ = _Game.PlayerEmpire.DetermineShipsMovingToDestination((StellarObject)object_7);
                }
                if (object_7 is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)object_7;
                    if (builtObject.Role == BuiltObjectRole.Military && builtObject.Empire == _Game.PlayerEmpire)
                    {
                        btnCycleShipStance.Visible = true;
                    }
                    else
                    {
                        btnCycleShipStance.Visible = false;
                    }
                }
                else if (object_7 is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)object_7;
                    if (shipGroup.Empire == _Game.PlayerEmpire)
                    {
                        btnCycleShipStance.Visible = true;
                    }
                    else
                    {
                        btnCycleShipStance.Visible = false;
                    }
                }
                else
                {
                    btnCycleShipStance.Visible = false;
                }
                if (UhvLmNjli7)
                {
                    method_157(object_7);
                    UhvLmNjli7 = true;
                }
            }
            else
            {
                _Game.SelectedObject = null;
                UhvLmNjli7 = false;
                keys_0 = Keys.None;
                dateTime_0 = DateTime.MinValue;
                btnZoomSelection.Enabled = false;
                btnCycleShipStance.Visible = false;
            }
            method_592();
            bool_18 = false;
            builtObject_5 = null;
            habitat_1 = null;
            creature_0 = null;
            habitat_2 = null;
            builtObject_6 = null;
            string_17 = string.Empty;
            shipAction_0 = null;
            if (_Game.SelectedObject is BuiltObject)
            {
                BuiltObject builtObject2 = (BuiltObject)_Game.SelectedObject;
                BuiltObjectImageData builtObjectImageData = builtObjectImageCache_0.ObtainImageData(builtObject2);
                if (builtObjectImageData != null)
                {
                    Bitmap image = new Bitmap(builtObjectImageData.Image);
                    image = ((builtObject2.Empire == null) ? PrepareBuiltObjectImage(builtObject2, image, Color.Gray, Color.Gray, 1.0, 1) : PrepareBuiltObjectImage(builtObject2, image, builtObject2.Empire.MainColor, builtObject2.Empire.SecondaryColor, 1.0, 1));
                    pnlDetailInfo.SetData(_Game, _Game.Galaxy, image, new Bitmap(builtObjectImageData.MaskImage), builtObject2);
                    if (builtObject2.TopSpeed > 0 && builtObject2.Owner == _Game.PlayerEmpire)
                    {
                        bool_18 = true;
                        builtObject_5 = builtObject2;
                    }
                }
            }
            else if (_Game.SelectedObject is Fighter)
            {
                Fighter fighter = (Fighter)_Game.SelectedObject;
                Bitmap image2 = new Bitmap(bitmap_6[fighter.PictureRef]);
                image2 = ((fighter.Empire == null) ? PrepareFighterImage(fighter, image2, Color.Gray, Color.Gray, 1.0, 1, 1.0) : PrepareFighterImage(fighter, image2, fighter.Empire.MainColor, fighter.Empire.SecondaryColor, 1.0, 1, 1.0));
                pnlDetailInfo.SetData(_Game, _Game.Galaxy, image2, new Bitmap(bitmap_7[fighter.PictureRef]), fighter);
            }
            else if (_Game.SelectedObject is Habitat)
            {
                Habitat habitat = (Habitat)_Game.SelectedObject;
                Bitmap backgroundPicture = mainView.method_54(habitat, pnlDetailInfo.ClientSize.Width);
                pnlDetailInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat);
            }
            else if (_Game.SelectedObject is Creature)
            {
                Creature creature = (Creature)_Game.SelectedObject;
                pnlDetailInfo.SetData(_Game, _Game.Galaxy, bitmap_10[creature.PictureRef][0], new Bitmap(bitmap_11[creature.PictureRef][0]), creature);
            }
            else if (_Game.SelectedObject is SystemInfo)
            {
                SystemInfo systemInfo = (SystemInfo)_Game.SelectedObject;
                Bitmap bitmap = null;
                bitmap = ((systemInfo.SystemStar.Category != HabitatCategoryType.GasCloud) ? bitmap_196[systemInfo.SystemStar.MapPictureRef] : bitmap_4);
                pnlDetailInfo.SetData(_Game, _Game.Galaxy, bitmap, systemInfo);
            }
            else if (_Game.SelectedObject is ShipGroup)
            {
                ShipGroup shipGroup2 = (ShipGroup)_Game.SelectedObject;
                pnlDetailInfo.SetData(_Game, _Game.Galaxy, shipGroup2);
                if (shipGroup2.Empire == _Game.PlayerEmpire)
                {
                    bool_18 = true;
                    shipGroup_2 = shipGroup2;
                }
            }
            else if (_Game.SelectedObject is BuiltObjectList)
            {
                BuiltObjectList builtObjects = (BuiltObjectList)_Game.SelectedObject;
                pnlDetailInfo.SetData(_Game, _Game.Galaxy, builtObjects);
            }
            else if (_Game.SelectedObject == null)
            {
                string_20 = string.Empty;
                pnlDetailInfo.ClearData();
            }
            if (bool_28 && _Game.SelectedObject != null)
            {
                if (list_5.Count > 0 && list_5[int_22] == _Game.SelectedObject)
                {
                    return;
                }
                if (int_22 + 1 < list_5.Count && list_5[int_22 + 1] == _Game.SelectedObject)
                {
                    int_22++;
                    return;
                }
                if (_Game.SelectedObject is BuiltObjectList)
                {
                    BuiltObjectList builtObjectList2 = (BuiltObjectList)_Game.SelectedObject;
                    if (list_5.Count > 0 && list_5[int_22] is BuiltObjectList)
                    {
                        BuiltObjectList builtObjectList3 = (BuiltObjectList)list_5[int_22];
                        if (builtObjectList2.Count == builtObjectList3.Count)
                        {
                            bool flag = true;
                            for (int j = 0; j < builtObjectList2.Count; j++)
                            {
                                if (builtObjectList2[j] != builtObjectList3[j])
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                return;
                            }
                        }
                    }
                }
                if (int_22 < list_5.Count - 1)
                {
                    int_22++;
                    list_5.Insert(int_22, _Game.SelectedObject);
                }
                else if (list_5.Count >= int_23)
                {
                    for (int k = 0; k < list_5.Count - 1; k++)
                    {
                        list_5[k] = list_5[k + 1];
                    }
                    list_5[list_5.Count - 1] = _Game.SelectedObject;
                    int_22 = list_5.Count - 1;
                }
                else
                {
                    list_5.Add(_Game.SelectedObject);
                    int_22 = list_5.Count - 1;
                }
            }
            method_212();
            method_246(builtObjectList_);
            Focus();
        }

        private void btnSelectionForward_Click(object sender, EventArgs e)
        {
            if (_Game.SelectedObject != null || list_5.Count <= 0)
            {
                if (int_22 < list_5.Count - 1)
                {
                    int_22++;
                }
                else if (list_5.Count > 0)
                {
                    int_22 = 0;
                }
            }
            if (int_22 >= list_5.Count)
            {
                int_22 = list_5.Count - 1;
            }
            int_22 = method_211(int_22);
            if (int_22 < 0)
            {
                int_22 = 0;
                method_209(null, bool_28: false);
            }
            else if (int_22 >= 0 && int_22 < list_5.Count)
            {
                method_209(list_5[int_22], bool_28: false);
            }
            method_212();
        }

        private void btnSelectionBack_Click(object sender, EventArgs e)
        {
            if (_Game.SelectedObject != null || list_5.Count <= 0)
            {
                if (int_22 > 0)
                {
                    int_22--;
                }
                else if (list_5.Count > 0)
                {
                    int_22 = list_5.Count - 1;
                }
            }
            int_22 = method_210(int_22);
            if (int_22 < 0)
            {
                int_22 = 0;
                method_209(null, bool_28: false);
            }
            else
            {
                method_209(list_5[int_22], bool_28: false);
            }
            method_212();
        }

        private int method_210(int int_64)
        {
            bool flag = false;
            while (true)
            {
                if (!flag)
                {
                    if (int_64 >= 0)
                    {
                        if (list_5.Count <= 0)
                        {
                            break;
                        }
                        if (list_5[int_64] == null)
                        {
                            list_5.RemoveAt(int_64);
                            int_64--;
                        }
                        else if (list_5[int_64] is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)list_5[int_64];
                            if (!builtObject.HasBeenDestroyed && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject))
                            {
                                flag = true;
                                continue;
                            }
                            list_5.RemoveAt(int_64);
                            int_64--;
                        }
                        else if (list_5[int_64] is Fighter)
                        {
                            Fighter fighter = (Fighter)list_5[int_64];
                            if (!fighter.HasBeenDestroyed && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(fighter))
                            {
                                flag = true;
                                continue;
                            }
                            list_5.RemoveAt(int_64);
                            int_64--;
                        }
                        else if (list_5[int_64] is ShipGroup)
                        {
                            ShipGroup shipGroup = (ShipGroup)list_5[int_64];
                            if (shipGroup.Ships != null && shipGroup.Ships.Count > 0 && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(shipGroup.LeadShip))
                            {
                                flag = true;
                                continue;
                            }
                            list_5.RemoveAt(int_64);
                            int_64--;
                        }
                        else if (list_5[int_64] is Creature)
                        {
                            Creature creature = (Creature)list_5[int_64];
                            if (!creature.HasBeenDestroyed && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(creature) && creature.IsVisible)
                            {
                                flag = true;
                                continue;
                            }
                            list_5.RemoveAt(int_64);
                            int_64--;
                        }
                        else
                        {
                            flag = true;
                        }
                        continue;
                    }
                    if (list_5.Count <= 0)
                    {
                        return -1;
                    }
                    return 0;
                }
                return int_64;
            }
            return -1;
        }

        private int method_211(int int_64)
        {
            bool flag = false;
            while (true)
            {
                if (!flag)
                {
                    if (int_64 < list_5.Count)
                    {
                        if (list_5.Count <= 0)
                        {
                            break;
                        }
                        if (list_5[int_64] == null)
                        {
                            list_5.RemoveAt(int_64);
                        }
                        else if (list_5[int_64] is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)list_5[int_64];
                            if (!builtObject.HasBeenDestroyed && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject))
                            {
                                flag = true;
                            }
                            else
                            {
                                list_5.RemoveAt(int_64);
                            }
                        }
                        else if (list_5[int_64] is Fighter)
                        {
                            Fighter fighter = (Fighter)list_5[int_64];
                            if (!fighter.HasBeenDestroyed && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(fighter))
                            {
                                flag = true;
                            }
                            else
                            {
                                list_5.RemoveAt(int_64);
                            }
                        }
                        else if (list_5[int_64] is ShipGroup)
                        {
                            ShipGroup shipGroup = (ShipGroup)list_5[int_64];
                            if (shipGroup.Ships != null && shipGroup.Ships.Count > 0 && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(shipGroup.LeadShip))
                            {
                                flag = true;
                            }
                            else
                            {
                                list_5.RemoveAt(int_64);
                            }
                        }
                        else if (list_5[int_64] is Creature)
                        {
                            Creature creature = (Creature)list_5[int_64];
                            if (!creature.HasBeenDestroyed && _Game.PlayerEmpire.IsObjectVisibleToThisEmpire(creature) && creature.IsVisible)
                            {
                                flag = true;
                            }
                            else
                            {
                                list_5.RemoveAt(int_64);
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                        continue;
                    }
                    if (list_5.Count <= 0)
                    {
                        return -1;
                    }
                    return 0;
                }
                return int_64;
            }
            return -1;
        }

        private void method_212()
        {
            if (list_5.Count > 0)
            {
                btnSelectionBack.Enabled = true;
                btnSelectionForward.Enabled = true;
            }
            else
            {
                btnSelectionBack.Enabled = false;
                btnSelectionForward.Enabled = false;
            }
        }

        private void method_213(Habitat habitat_9)
        {
            if (habitatList_0.Count > 0 && habitatList_0[int_24] == habitat_9)
            {
                return;
            }
            if (int_24 < habitatList_0.Count - 1)
            {
                int_24++;
                habitatList_0[int_24] = habitat_9;
                if (int_24 < habitatList_0.Count - 1)
                {
                    habitatList_0.RemoveRange(int_24 + 1, habitatList_0.Count - (int_24 + 1));
                }
            }
            else if (habitatList_0.Count >= dremNtuMsv)
            {
                for (int i = 0; i < habitatList_0.Count - 1; i++)
                {
                    habitatList_0[i] = habitatList_0[i + 1];
                }
                habitatList_0[habitatList_0.Count - 1] = habitat_9;
                int_24 = habitatList_0.Count - 1;
            }
            else
            {
                habitatList_0.Add(habitat_9);
                int_24 = habitatList_0.Count - 1;
            }
            method_215();
        }

        private void btnGalaxyMapBack_Click(object sender, EventArgs e)
        {
            if (int_24 > 0)
            {
                int_24--;
                method_214(habitatList_0[int_24]);
            }
            method_215();
        }

        private void btnGalaxyMapForward_Click(object sender, EventArgs e)
        {
            if (int_24 < habitatList_0.Count - 1)
            {
                int_24++;
                method_214(habitatList_0[int_24]);
            }
            method_215();
        }

        private void method_214(Habitat habitat_9)
        {
            habitat_8 = habitat_9;
            Habitat habitat = (habitat_7 = Galaxy.DetermineHabitatSystemStar(habitat_9));
            int solIndexStart = 0;
            int scaleFactor = Galaxy.MaxSolarSystemSize * 2 / picSystemMap.ClientRectangle.Width;
            if (habitat != null)
            {
                solIndexStart = _Game.Galaxy.Habitats.IndexOf(habitat);
            }
            Bitmap backgroundPicture = mainView.method_54(habitat_8, pnlHabitatInfo.ClientSize.Width);
            pnlHabitatInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat_8);
            method_147(habitat_8);
            picSystemMap.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: false, solIndexStart, scaleFactor, drawViewIndicator: false, erasePrevious: false, clearFirst: true, showIndicatorLines: true, habitat.Name);
            picSystemMap.Invalidate();
            gmapMain.SetPosition(habitat_7.Xpos, habitat_7.Ypos);
            gmapMain.Invalidate();
            if (habitat_8 != null)
            {
                if (habitat_8.LandscapePictureRef >= 0)
                {
                    pnlGalaxyMapHabitatPicture.BackgroundImage = bitmap_29[habitat_8.LandscapePictureRef];
                }
                else
                {
                    pnlGalaxyMapHabitatPicture.BackgroundImage = null;
                }
            }
            else
            {
                pnlGalaxyMapHabitatPicture.BackgroundImage = null;
            }
        }

        private void method_215()
        {
            if (int_24 < habitatList_0.Count - 1)
            {
                btnGalaxyMapForward.Enabled = true;
            }
            else
            {
                btnGalaxyMapForward.Enabled = false;
            }
            if (int_24 > 0)
            {
                btnGalaxyMapBack.Enabled = true;
            }
            else
            {
                btnGalaxyMapBack.Enabled = false;
            }
        }

        internal void method_216()
        {
            method_218(3, 7);
        }

        internal void method_217(int int_64)
        {
            method_218(int_64, 7);
        }

        internal void method_218(int int_64, int int_65)
        {
            int_20 = int_64;
            int_19 = int_65;
        }

        private void method_219()
        {
            if (_Game != null && _Game.Galaxy != null && int_19 > 0)
            {
                if (int_21 == int_20)
                {
                    int_21 = int_20 * -1;
                }
                else
                {
                    int_21 = int_20;
                }
                if (vhadzRiecM == int_20)
                {
                    vhadzRiecM = int_20 * -1;
                }
                else
                {
                    vhadzRiecM = int_20;
                }
                int_19--;
            }
            else
            {
                int_21 = 0;
                vhadzRiecM = 0;
            }
        }

        private bool method_220()
        {
            return method_221(1);
        }

        private bool method_221(int int_64)
        {
            int num = int_29 - int_28;
            if (num + int_64 >= 2000)
            {
                string string_ = TextResolver.GetText("You cannot place any more items in this system. Please remove another item first, then retry placing your new item.");
                MessageBoxEx messageBoxEx = method_370(string_, TextResolver.GetText("Too many items in system"));
                messageBoxEx.Show(this);
                Focus();
                return false;
            }
            return true;
        }

        private void method_222(int int_64, int int_65)
        {
            empire_1 = null;
            object lockObject = default(object);
            if (gameEditorSelector.EditMode == EditorMode.System)
            {
                SystemInfo systemInfo = new SystemInfo();
                Habitat habitat = _Game.Galaxy.SelectStar(gameEditorSelector.EditorHabitatType, int_64, int_65);
                HabitatList habitatList = _Game.Galaxy.SetupSolarSystem(_Game.Galaxy.GalaxyShape, habitat);
                HabitatList habitatList2 = new HabitatList();
                if (habitatList.Count > 1)
                {
                    for (int i = 1; i < habitatList.Count; i++)
                    {
                        habitatList2.Add(habitatList[i]);
                    }
                }
                systemInfo.Habitats = habitatList2;
                systemInfo.SystemStar = habitat;
                for (int j = 0; j < _Game.Galaxy.Creatures.Count; j++)
                {
                    if (_Game.Galaxy.Creatures[j].NearestSystemStar == habitat)
                    {
                        systemInfo.Creatures.Add(_Game.Galaxy.Creatures[j]);
                    }
                }
                Sector sector = (systemInfo.Sector = _Game.Galaxy.ResolveSector(int_64, int_65));
                systemInfo = _Game.Galaxy.DetermineSystemInfo(systemInfo, _Game.PlayerEmpire);
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(lockObject = _Game.Galaxy._LockObject, ref lockTaken);
                    _Game.Galaxy.AddSystem(systemInfo);
                    return;
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(lockObject);
                    }
                }
            }
            if (gameEditorSelector.EditMode == EditorMode.Star)
            {
                SystemInfo systemInfo2 = new SystemInfo();
                Habitat habitat2 = _Game.Galaxy.SelectStar(gameEditorSelector.EditorHabitatType, int_64, int_65);
                HabitatList habitatList3 = new HabitatList();
                habitatList3.Add(habitat2);
                systemInfo2.Habitats = new HabitatList();
                systemInfo2.SystemStar = habitat2;
                for (int k = 0; k < _Game.Galaxy.Creatures.Count; k++)
                {
                    if (_Game.Galaxy.Creatures[k].NearestSystemStar == habitat2)
                    {
                        systemInfo2.Creatures.Add(_Game.Galaxy.Creatures[k]);
                    }
                }
                Sector sector2 = (systemInfo2.Sector = _Game.Galaxy.ResolveSector(int_64, int_65));
                systemInfo2 = _Game.Galaxy.DetermineSystemInfo(systemInfo2, _Game.PlayerEmpire);
                bool lockTaken2 = false;
                try
                {
                    Monitor.Enter(lockObject = _Game.Galaxy._LockObject, ref lockTaken2);
                    _Game.Galaxy.AddSystem(systemInfo2);
                    return;
                }
                finally
                {
                    if (lockTaken2)
                    {
                        Monitor.Exit(lockObject);
                    }
                }
            }
            if (gameEditorSelector.EditMode == EditorMode.GasCloud)
            {
                SystemInfo systemInfo3 = new SystemInfo();
                Habitat habitat3 = _Game.Galaxy.GenerateGasCloud(gameEditorSelector.EditorHabitatType, int_64, int_65);
                if (habitat3 == null)
                {
                    return;
                }
                HabitatList habitatList4 = new HabitatList();
                habitatList4.Add(habitat3);
                systemInfo3.Habitats = new HabitatList();
                systemInfo3.SystemStar = habitat3;
                for (int l = 0; l < _Game.Galaxy.Creatures.Count; l++)
                {
                    if (_Game.Galaxy.Creatures[l].NearestSystemStar == habitat3)
                    {
                        systemInfo3.Creatures.Add(_Game.Galaxy.Creatures[l]);
                    }
                }
                Sector sector3 = (systemInfo3.Sector = _Game.Galaxy.ResolveSector(int_64, int_65));
                systemInfo3 = _Game.Galaxy.DetermineSystemInfo(systemInfo3, _Game.PlayerEmpire);
                bool lockTaken3 = false;
                try
                {
                    Monitor.Enter(lockObject = _Game.Galaxy._LockObject, ref lockTaken3);
                    _Game.Galaxy.AddSystem(systemInfo3);
                    return;
                }
                finally
                {
                    if (lockTaken3)
                    {
                        Monitor.Exit(lockObject);
                    }
                }
            }
            if (gameEditorSelector.EditMode == EditorMode.Planet)
            {
                HabitatType type = HabitatType.BarrenRock;
                int pictureRef = 0;
                int diameter = 100;
                int num = 10000;
                int landscapePictureRef = 0;
                int minOrbitDistance = 9000;
                int maxOrbitDistance = 11000;
                switch (gameEditorSelector.EditorHabitatType)
                {
                    case HabitatType.Volcanic:
                        _Game.Galaxy.SelectVolcanicPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                    case HabitatType.Desert:
                        _Game.Galaxy.SelectDesertPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                    case HabitatType.MarshySwamp:
                        _Game.Galaxy.SelectMarshySwampPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                    case HabitatType.Continental:
                        _Game.Galaxy.SelectContinentalPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                    case HabitatType.Ocean:
                        _Game.Galaxy.SelectOceanPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                    case HabitatType.BarrenRock:
                        _Game.Galaxy.SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                    case HabitatType.Ice:
                        _Game.Galaxy.SelectIcePlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                    case HabitatType.GasGiant:
                        _Game.Galaxy.SelectGasGiantPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                    case HabitatType.FrozenGasGiant:
                        _Game.Galaxy.SelectFrozenGasGiantPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        break;
                }
                Habitat habitat4 = _Game.Galaxy.FindNearestSystemGasCloudAsteroid(int_64, int_65);
                if (habitat4 == null || habitat4.Category != 0)
                {
                    return;
                }
                double num2 = _Game.Galaxy.CalculateDistance(habitat4.Xpos, habitat4.Ypos, int_64, int_65);
                num = (int)num2;
                if (num2 > (double)Galaxy.MaxSolarSystemSize || !method_220())
                {
                    return;
                }
                double orbitangle = _Game.Galaxy.CalculateAngleFromCoords(int_64, int_65, habitat4.Xpos, habitat4.Ypos, num);
                bool orbitdirection = true;
                if (Galaxy.Rnd.Next(0, 5) == 1)
                {
                    orbitdirection = false;
                }
                int orbitspeed = Galaxy.Rnd.Next(2, 5);
                Habitat habitat5 = new Habitat(_Game.Galaxy, HabitatCategoryType.Planet, type, _Game.Galaxy.GenerateCodeName(), habitat4, orbitangle, orbitdirection, num, orbitspeed, doInitialMove: false);
                habitat5.Diameter = (short)diameter;
                habitat5.PictureRef = (short)pictureRef;
                habitat5.LandscapePictureRef = (short)landscapePictureRef;
                habitat5.Xpos = int_64;
                habitat5.Ypos = int_65;
                habitat5 = _Game.Galaxy.SelectResources(habitat5);
                _Game.Galaxy.SelectHabitatPictures(habitat5);
                bool lockTaken4 = false;
                try
                {
                    Monitor.Enter(lockObject = _Game.Galaxy._LockObject, ref lockTaken4);
                    if (_Game.Galaxy.AddHabitat(habitat5, habitat4))
                    {
                        _Game.Galaxy.Systems[habitat4.SystemIndex] = _Game.Galaxy.DetermineSystemInfo(_Game.Galaxy.Systems[habitat4.SystemIndex], _Game.PlayerEmpire);
                        method_149();
                        int_29++;
                        bool_19 = true;
                    }
                    return;
                }
                finally
                {
                    if (lockTaken4)
                    {
                        Monitor.Exit(lockObject);
                    }
                }
            }
            if (gameEditorSelector.EditMode == EditorMode.Moon)
            {
                HabitatType type2 = HabitatType.BarrenRock;
                int pictureRef2 = 0;
                int diameter2 = 100;
                int num3 = 500;
                int landscapePictureRef2 = 0;
                int minOrbitDistance2 = 400;
                int maxOrbitDistance2 = 600;
                switch (gameEditorSelector.EditorHabitatType)
                {
                    case HabitatType.Volcanic:
                        _Game.Galaxy.SelectVolcanicPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                        break;
                    case HabitatType.Desert:
                        _Game.Galaxy.SelectDesertPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                        break;
                    case HabitatType.MarshySwamp:
                        _Game.Galaxy.SelectMarshySwampPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                        break;
                    case HabitatType.Continental:
                        _Game.Galaxy.SelectContinentalPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                        break;
                    case HabitatType.Ocean:
                        _Game.Galaxy.SelectOceanPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                        break;
                    case HabitatType.BarrenRock:
                        _Game.Galaxy.SelectBarrenRockPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                        break;
                    case HabitatType.Ice:
                        _Game.Galaxy.SelectIcePlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                        break;
                }
                Habitat habitat6 = _Game.Galaxy.FindNearestHabitat(int_64, int_65, HabitatCategoryType.Planet);
                if (habitat6 == null)
                {
                    return;
                }
                double num4 = _Game.Galaxy.CalculateDistance(habitat6.Xpos, habitat6.Ypos, int_64, int_65);
                num3 = (int)num4;
                if (num4 > (double)Galaxy.MaxMoonOrbitSize)
                {
                    return;
                }
                if ((double)diameter2 > (double)habitat6.Diameter * 0.33)
                {
                    diameter2 = (int)((double)habitat6.Diameter * ((double)Galaxy.Rnd.Next(25, 33) * 0.01));
                }
                if (!method_220())
                {
                    return;
                }
                double orbitangle2 = _Game.Galaxy.CalculateAngleFromCoords(int_64, int_65, habitat6.Xpos, habitat6.Ypos, num3);
                bool orbitdirection2 = true;
                if (Galaxy.Rnd.Next(0, 5) == 1)
                {
                    orbitdirection2 = false;
                }
                int orbitspeed2 = Galaxy.Rnd.Next(4, 9);
                Habitat habitat7 = new Habitat(_Game.Galaxy, HabitatCategoryType.Moon, type2, _Game.Galaxy.GenerateCodeName(), habitat6, orbitangle2, orbitdirection2, num3, orbitspeed2, doInitialMove: false);
                habitat7.Diameter = (short)diameter2;
                habitat7.PictureRef = (short)pictureRef2;
                habitat7.LandscapePictureRef = (short)landscapePictureRef2;
                habitat7.Xpos = int_64;
                habitat7.Ypos = int_65;
                habitat7 = _Game.Galaxy.SelectResources(habitat7);
                _Game.Galaxy.SelectHabitatPictures(habitat7);
                bool lockTaken5 = false;
                try
                {
                    Monitor.Enter(lockObject = _Game.Galaxy._LockObject, ref lockTaken5);
                    if (_Game.Galaxy.AddHabitat(habitat7, habitat6.Parent))
                    {
                        _Game.Galaxy.Systems[habitat6.Parent.SystemIndex] = _Game.Galaxy.DetermineSystemInfo(_Game.Galaxy.Systems[habitat6.Parent.SystemIndex], _Game.PlayerEmpire);
                        method_149();
                        int_29++;
                        bool_19 = true;
                    }
                    return;
                }
                finally
                {
                    if (lockTaken5)
                    {
                        Monitor.Exit(lockObject);
                    }
                }
            }
            if (gameEditorSelector.EditMode == EditorMode.Asteroid && gameEditorSelector.vwAsteroids.SelectedValue == TextResolver.GetText("Asteroid"))
            {
                int num5 = 0;
                int num6 = 100;
                int num7 = 10000;
                Habitat habitat8 = _Game.Galaxy.FindNearestSystemGasCloudAsteroid(int_64, int_65);
                if (habitat8 == null || habitat8.Category != 0)
                {
                    return;
                }
                double num8 = _Game.Galaxy.CalculateDistance(habitat8.Xpos, habitat8.Ypos, int_64, int_65);
                num7 = (int)num8;
                if (num8 > (double)Galaxy.MaxSolarSystemSize)
                {
                    return;
                }
                num6 = Galaxy.Rnd.Next(15, 30);
                num5 = (short)(GalaxyImages.HabitatImageOffsetAsteroidsNormal + Galaxy.Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsNormal));
                if (!method_220())
                {
                    return;
                }
                double orbitangle3 = _Game.Galaxy.CalculateAngleFromCoords(int_64, int_65, habitat8.Xpos, habitat8.Ypos, num7);
                Habitat habitat9 = new Habitat(_Game.Galaxy, HabitatCategoryType.Asteroid, HabitatType.BarrenRock, _Game.Galaxy.GenerateCodeName(), habitat8, orbitangle3, orbitdirection: true, num7, Galaxy.Rnd.Next(2, 8), doInitialMove: false);
                habitat9.Diameter = (short)num6;
                habitat9.PictureRef = (short)num5;
                habitat9.LandscapePictureRef = -1;
                habitat9.Xpos = int_64;
                habitat9.Ypos = int_65;
                if (Galaxy.Rnd.Next(0, 5) == 2)
                {
                    habitat9.OrbitDirection = false;
                }
                habitat9 = _Game.Galaxy.SelectResources(habitat9);
                _Game.Galaxy.SelectHabitatPictures(habitat9);
                bool lockTaken6 = false;
                try
                {
                    Monitor.Enter(lockObject = _Game.Galaxy._LockObject, ref lockTaken6);
                    if (_Game.Galaxy.AddHabitat(habitat9, habitat8))
                    {
                        _Game.Galaxy.Systems[habitat8.SystemIndex] = _Game.Galaxy.DetermineSystemInfo(_Game.Galaxy.Systems[habitat8.SystemIndex], _Game.PlayerEmpire);
                        method_149();
                        int_29++;
                        bool_19 = true;
                    }
                    return;
                }
                finally
                {
                    if (lockTaken6)
                    {
                        Monitor.Exit(lockObject);
                    }
                }
            }
            if (gameEditorSelector.EditMode == EditorMode.AsteroidField)
            {
                int num9 = 10000;
                Habitat habitat10 = _Game.Galaxy.FindNearestSystemGasCloudAsteroid(int_64, int_65);
                if (habitat10 == null || habitat10.Category != 0)
                {
                    return;
                }
                double num10 = _Game.Galaxy.CalculateDistance(habitat10.Xpos, habitat10.Ypos, int_64, int_65);
                num9 = (int)num10;
                if (num10 > (double)Galaxy.MaxSolarSystemSize)
                {
                    return;
                }
                int orbitSpeed = Galaxy.Rnd.Next(1, 4);
                bool orbitDirection = true;
                if (Galaxy.Rnd.Next(0, 5) == 2)
                {
                    orbitDirection = false;
                }
                int num11 = Galaxy.Rnd.Next(80, 350);
                if (!method_221(num11))
                {
                    return;
                }
                HabitatType type3 = HabitatType.BarrenRock;
                if (gameEditorSelector.vwAsteroids.SelectedValue == TextResolver.GetText("Asteroid Field - Metal"))
                {
                    type3 = HabitatType.Metal;
                }
                else if (gameEditorSelector.vwAsteroids.SelectedValue == TextResolver.GetText("Asteroid Field - Ice"))
                {
                    type3 = HabitatType.Ice;
                }
                HabitatList asteroids = _Game.Galaxy.GenerateAsteroidField(num11, int_64, int_65, habitat10, orbitDirection, orbitSpeed, num9, 1.0, 1.0, type3);
                bool lockTaken7 = false;
                try
                {
                    Monitor.Enter(lockObject = _Game.Galaxy._LockObject, ref lockTaken7);
                    if (_Game.Galaxy.AddAsteroidField(asteroids, habitat10))
                    {
                        _Game.Galaxy.Systems[habitat10.SystemIndex] = _Game.Galaxy.DetermineSystemInfo(_Game.Galaxy.Systems[habitat10.SystemIndex], _Game.PlayerEmpire);
                        method_149();
                        int_29 += num11;
                        bool_19 = true;
                    }
                    return;
                }
                finally
                {
                    if (lockTaken7)
                    {
                        Monitor.Exit(lockObject);
                    }
                }
            }
            if (gameEditorSelector.EditMode == EditorMode.BuiltObject)
            {
                Empire selectedEmpire = gameEditorSelector.cmbPlaceShipEmpire.SelectedEmpire;
                Design selectedDesign = gameEditorSelector.cmbPlaceShipDesign.SelectedDesign;
                if (selectedEmpire == null || selectedDesign == null)
                {
                    return;
                }
                Habitat habitat11 = method_201(int_64, int_65);
                int offsetX = 0;
                int offsetY = 0;
                if (selectedDesign.Role != BuiltObjectRole.Base && habitat11 == null)
                {
                    habitat11 = method_199(int_64, int_65);
                }
                if (habitat11 != null)
                {
                    offsetX = (int)((double)int_64 - habitat11.Xpos);
                    offsetY = (int)((double)int_65 - habitat11.Ypos);
                }
                bool isStateOwned = _Game.Galaxy.DetermineBuiltObjectIsState(selectedDesign.SubRole);
                if (selectedDesign.Role == BuiltObjectRole.Base)
                {
                    if ((selectedDesign.SubRole != BuiltObjectSubRole.GenericBase || _Game.Galaxy.PirateEmpires.Contains(selectedDesign.Empire)) && selectedDesign.SubRole != BuiltObjectSubRole.EnergyResearchStation && selectedDesign.SubRole != BuiltObjectSubRole.WeaponsResearchStation && selectedDesign.SubRole != BuiltObjectSubRole.HighTechResearchStation && selectedDesign.SubRole != BuiltObjectSubRole.MonitoringStation && selectedDesign.SubRole != BuiltObjectSubRole.ResortBase)
                    {
                        if (habitat11 != null && habitat11.Category != 0)
                        {
                            selectedDesign.BuildCount++;
                            string text = string.Empty;
                            switch (selectedDesign.SubRole)
                            {
                                case BuiltObjectSubRole.GasMiningStation:
                                    text = " " + TextResolver.GetText("Gas Mining Station");
                                    break;
                                case BuiltObjectSubRole.MiningStation:
                                    text = " " + TextResolver.GetText("Mining Station");
                                    break;
                                case BuiltObjectSubRole.SmallSpacePort:
                                case BuiltObjectSubRole.MediumSpacePort:
                                case BuiltObjectSubRole.LargeSpacePort:
                                    text = " " + TextResolver.GetText("Space Port");
                                    break;
                                case BuiltObjectSubRole.GenericBase:
                                    text = " " + TextResolver.GetText("Station");
                                    break;
                                case BuiltObjectSubRole.EnergyResearchStation:
                                case BuiltObjectSubRole.WeaponsResearchStation:
                                case BuiltObjectSubRole.HighTechResearchStation:
                                    text = " " + TextResolver.GetText("Research Station");
                                    break;
                                case BuiltObjectSubRole.MonitoringStation:
                                    text = " " + TextResolver.GetText("Beacon");
                                    break;
                                case BuiltObjectSubRole.DefensiveBase:
                                    text = " " + TextResolver.GetText("Defense Platform");
                                    break;
                            }
                            string name = habitat11.Name + text;
                            if (_Game.Galaxy.PirateEmpires.Contains(selectedEmpire))
                            {
                                name = _Game.Galaxy.GeneratePirateBaseName(habitat11);
                            }
                            BuiltObject builtObject = new BuiltObject(selectedDesign, name, _Game.Galaxy, fullyBuilt: true);
                            builtObject.Empire = selectedEmpire;
                            builtObject.Heading = _Game.Galaxy.SelectRandomHeading();
                            builtObject.TargetHeading = builtObject.Heading;
                            builtObject.ReDefine();
                            builtObject.CurrentFuel = builtObject.FuelCapacity;
                            builtObject.CurrentShields = builtObject.ShieldsCapacity;
                            selectedEmpire.AddBuiltObjectToGalaxy(builtObject, habitat11, offsetLocationFromParent: false, isStateOwned, offsetX, offsetY, sendMessage: false);
                        }
                    }
                    else
                    {
                        selectedDesign.BuildCount++;
                        string name2 = _Game.Galaxy.GenerateBuiltObjectName(selectedDesign);
                        BuiltObject builtObject2 = new BuiltObject(selectedDesign, name2, _Game.Galaxy, fullyBuilt: true);
                        builtObject2.Empire = selectedEmpire;
                        builtObject2.Heading = _Game.Galaxy.SelectRandomHeading();
                        builtObject2.TargetHeading = builtObject2.Heading;
                        builtObject2.TargetHeading = builtObject2.Heading;
                        builtObject2.ReDefine();
                        builtObject2.CurrentFuel = builtObject2.FuelCapacity;
                        builtObject2.CurrentShields = builtObject2.ShieldsCapacity;
                        if (habitat11 != null && habitat11.Category != 0)
                        {
                            builtObject2.Name = habitat11.Name + " " + TextResolver.GetText("Station");
                            selectedEmpire.AddBuiltObjectToGalaxy(builtObject2, habitat11, offsetLocationFromParent: false, isStateOwned, offsetX, offsetY, sendMessage: false);
                        }
                        else
                        {
                            builtObject2.Xpos = int_64;
                            builtObject2.Ypos = int_65;
                            selectedEmpire.AddBuiltObjectToGalaxy(builtObject2, null, offsetLocationFromParent: false, isStateOwned, sendMessage: false);
                        }
                    }
                }
                else
                {
                    selectedDesign.BuildCount++;
                    string name3 = _Game.Galaxy.GenerateBuiltObjectName(selectedDesign);
                    BuiltObject builtObject3 = new BuiltObject(selectedDesign, name3, _Game.Galaxy, fullyBuilt: true);
                    builtObject3.Empire = selectedEmpire;
                    builtObject3.Heading = _Game.Galaxy.SelectRandomHeading();
                    builtObject3.TargetHeading = builtObject3.Heading;
                    builtObject3.ReDefine();
                    builtObject3.CurrentFuel = builtObject3.FuelCapacity;
                    builtObject3.CurrentShields = builtObject3.ShieldsCapacity;
                    if (builtObject3.SubRole == BuiltObjectSubRole.ColonyShip)
                    {
                        builtObject3.NativeRace = selectedEmpire.DominantRace;
                    }
                    if (habitat11 != null && habitat11.Category != 0)
                    {
                        selectedEmpire.AddBuiltObjectToGalaxy(builtObject3, habitat11, offsetLocationFromParent: false, isStateOwned, offsetX, offsetY);
                        return;
                    }
                    builtObject3.Xpos = int_64;
                    builtObject3.Ypos = int_65;
                    selectedEmpire.AddBuiltObjectToGalaxy(builtObject3, null, offsetLocationFromParent: false, isStateOwned, sendMessage: false);
                }
                return;
            }
            if (gameEditorSelector.EditMode == EditorMode.Colony)
            {
                Empire selectedEmpire2 = gameEditorSelector.cmbPlaceColonyEmpire.SelectedEmpire;
                if (selectedEmpire2 == null)
                {
                    return;
                }
                Habitat habitat12 = method_201(int_64, int_65);
                if (habitat12 == null || (habitat12.Empire != null && habitat12.Empire != _Game.Galaxy.IndependentEmpire) || (habitat12.Category != HabitatCategoryType.Planet && habitat12.Category != HabitatCategoryType.Moon && habitat12.Category != HabitatCategoryType.Asteroid) || habitat12.Type == HabitatType.GasGiant || habitat12.Type == HabitatType.FrozenGasGiant)
                {
                    return;
                }
                selectedEmpire2.TakeOwnershipOfColony(habitat12, selectedEmpire2);
                habitat12.IsRefuellingDepot = true;
                long amount = (long)((double)habitat12.MaximumPopulation * 0.7);
                Population population = new Population(selectedEmpire2.DominantRace, amount);
                habitat12.Population.Add(population);
                for (int m = 0; m < _Game.Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; m++)
                {
                    ResourceDefinition resourceDefinition = _Game.Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[m];
                    if (resourceDefinition != null)
                    {
                        //int num12 = 300;
                        Cargo cargo = new Cargo(amount: (resourceDefinition.RelativeImportance > 0.4f || resourceDefinition.IsFuel) ? 3000 : ((!(resourceDefinition.RelativeImportance > 0.2f)) ? 300 : 800), resource: new Resource(resourceDefinition.ResourceID), empire: selectedEmpire2);
                        habitat12.Cargo.Add(cargo);
                    }
                }
                int developmentLevel = Galaxy.Rnd.Next(0, 51);
                habitat12.SetDevelopmentLevel(developmentLevel);
                habitat12.GrowPopulation(new TimeSpan(0L));
                int val = habitat12.EstimatedDefensiveForceRequired(atWar: false) / 70;
                val = Math.Min(val, 5);
                if (val <= 0)
                {
                    return;
                }
                int num13 = (int)(100.0 * ((double)habitat12.Population.DominantRace.AggressionLevel / 100.0) * ((double)habitat12.Population.DominantRace.IntelligenceLevel / 100.0));
                if (habitat12.Ruin != null)
                {
                    num13 = (int)((double)num13 * (1.0 + habitat12.Ruin.BonusDefensive));
                }
                for (int n = 0; n < val; n++)
                {
                    Troop troop = habitat12.GenerateNewTroop();
                    if (troop != null)
                    {
                        troop.Readiness = 100f;
                        habitat12.Troops.Add(troop);
                        selectedEmpire2.Troops.Add(troop);
                    }
                }
                return;
            }
            if (gameEditorSelector.EditMode == EditorMode.AlienRace)
            {
                Race selectedRace = gameEditorSelector.cmbPlaceAlienRace.SelectedRace;
                if (selectedRace == null)
                {
                    return;
                }
                Habitat habitat13 = method_201(int_64, int_65);
                if (habitat13 != null && (habitat13.Category == HabitatCategoryType.Planet || habitat13.Category == HabitatCategoryType.Moon) && habitat13.Type != HabitatType.GasGiant && habitat13.Type != HabitatType.FrozenGasGiant)
                {
                    habitat13.IsRefuellingDepot = true;
                    long amount3 = (long)((double)habitat13.MaximumPopulation * 0.3);
                    Population population2 = new Population(selectedRace, amount3);
                    habitat13.Population.Add(population2);
                    _Game.Galaxy.IndependentEmpire.TakeOwnershipOfColony(habitat13, _Game.Galaxy.IndependentEmpire, destroyBases: false, destroyTroops: false);
                    if (habitat13.Owner == null)
                    {
                        habitat13.Owner = _Game.Galaxy.IndependentEmpire;
                        habitat13.Empire = _Game.Galaxy.IndependentEmpire;
                    }
                    if (habitat13.DevelopmentLevel <= habitat13.GetDevelopmentLevel())
                    {
                        habitat13.SetDevelopmentLevel(habitat13.GetDevelopmentLevel());
                    }
                    habitat13.GrowPopulation(new TimeSpan(0L));
                }
                return;
            }
            if (gameEditorSelector.EditMode == EditorMode.Character)
            {
                Empire selectedEmpire3 = gameEditorSelector.cmbCharacterEmpire.SelectedEmpire;
                if (selectedEmpire3 == null)
                {
                    return;
                }
                CharacterRole characterRole = CharacterRole.Undefined;
                Character selectedCharacter = gameEditorSelector.cmbCharacters.GetSelectedCharacter(out characterRole);
                Habitat habitat14 = method_201(int_64, int_65);
                BuiltObject builtObject4 = method_202(int_64, int_65);
                StellarObject stellarObject = null;
                if (builtObject4 != null)
                {
                    stellarObject = builtObject4;
                }
                else if (habitat14 != null && habitat14.Population != null && habitat14.Population.TotalAmount > 0L && habitat14.Empire != null)
                {
                    stellarObject = habitat14;
                }
                if (stellarObject != null)
                {
                    if (selectedCharacter != null && selectedEmpire3.DominantRace != null && selectedEmpire3.DominantRace.AvailableCharacters != null && selectedEmpire3.DominantRace.AvailableCharacters.Contains(selectedCharacter))
                    {
                        selectedEmpire3.DominantRace.AvailableCharacters.ActivateAndRemoveCharacter(selectedCharacter, _Game.Galaxy, selectedEmpire3, stellarObject);
                        gameEditorSelector.cmbCharacters.BindData(selectedEmpire3, selectedEmpire3.DominantRace.AvailableCharacters, characterImageCache_0, _Game.Galaxy, allowRoleSelection: true, allowNullSelection: false);
                    }
                    else if (characterRole != 0)
                    {
                        selectedEmpire3.GenerateNewCharacterRandom(characterRole, stellarObject, activate: true);
                    }
                }
                return;
            }
            if (gameEditorSelector.EditMode == EditorMode.Creature)
            {
                CreatureType selectedCreatureType = gameEditorSelector.vwCreature.SelectedCreatureType;
                if (selectedCreatureType == CreatureType.Undefined)
                {
                    return;
                }
                Habitat habitat15 = method_201(int_64, int_65);
                int num14 = 0;
                int num15 = 0;
                bool lockLocation = false;
                if (selectedCreatureType != CreatureType.DesertSpaceSlug && selectedCreatureType != CreatureType.RockSpaceSlug)
                {
                    if (habitat15 == null)
                    {
                        habitat15 = method_200(int_64, int_65, 500.0);
                    }
                }
                else
                {
                    lockLocation = true;
                }
                if (habitat15 == null || habitat15.Category == HabitatCategoryType.Star)
                {
                    return;
                }
                num14 = (int)((double)int_64 - habitat15.Xpos);
                num15 = (int)((double)int_65 - habitat15.Ypos);
                _Game.Galaxy.GenerateCreatureAtHabitat(selectedCreatureType, habitat15, lockLocation, num14, num15);
                for (int num16 = 0; num16 < _Game.Galaxy.Creatures.Count; num16++)
                {
                    if (_Game.Galaxy.Creatures[num16].ParentHabitat != null && !_Game.Galaxy.Systems[_Game.Galaxy.Creatures[num16].ParentHabitat.SystemIndex].Creatures.Contains(_Game.Galaxy.Creatures[num16]))
                    {
                        _Game.Galaxy.Systems[_Game.Galaxy.Creatures[num16].ParentHabitat.SystemIndex].Creatures.Add(_Game.Galaxy.Creatures[num16]);
                    }
                }
                return;
            }
            if (gameEditorSelector.EditMode == EditorMode.Pirates)
            {
                Habitat habitat16 = method_201(int_64, int_65);
                int num17 = 0;
                int num18 = 0;
                if (habitat16 == null)
                {
                    return;
                }
                num17 = (int)((double)int_64 - habitat16.Xpos);
                num18 = (int)((double)int_65 - habitat16.Ypos);
                if (habitat16.Category != 0 && _Game.Galaxy.NextEmpireID < Galaxy.MaximumEmpireCount)
                {
                    _Game.Galaxy.SelectPopularDesignCandidates();
                    string editModeExtra = gameEditorSelector.EditModeExtra;
                    Race race = _Game.Galaxy.Races[editModeExtra];
                    if (race != null)
                    {
                        _Game.Galaxy.GeneratePirateEmpire(habitat16, num17, num18, race, race.DefaultPiratePlaystyle);
                    }
                    else
                    {
                        _Game.Galaxy.GeneratePirateEmpire(habitat16, num17, num18, useRace: true);
                    }
                    gameEditorSelector.PirateFactions = _Game.Galaxy.PirateEmpires;
                }
                return;
            }
            if (gameEditorSelector.EditMode != EditorMode.Ruins && gameEditorSelector.EditMode != EditorMode.RuinsLostColony && gameEditorSelector.EditMode != EditorMode.RuinsLostShip && gameEditorSelector.EditMode != EditorMode.RuinsOrigins && gameEditorSelector.EditMode != EditorMode.RuinsRefugees && gameEditorSelector.EditMode != EditorMode.RuinsSleepingRace && gameEditorSelector.EditMode != EditorMode.RuinsSpecialGovernment && gameEditorSelector.EditMode != EditorMode.RuinsSuperWeapon)
            {
                if (gameEditorSelector.EditMode == EditorMode.DebrisField)
                {
                    GalaxyLocationList galaxyLocationList = _Game.Galaxy.DetermineGalaxyLocationsInRangeAtPoint(int_64, int_65, Galaxy.MaxSolarSystemSize, GalaxyLocationType.DebrisField);
                    if (galaxyLocationList == null || galaxyLocationList.Count == 0)
                    {
                        int shipCount = Galaxy.Rnd.Next(10, 50);
                        _Game.Galaxy.GenerateDebrisField(int_64, int_65, string.Empty, shipCount);
                    }
                }
                else if (gameEditorSelector.EditMode == EditorMode.ClearItems)
                {
                    Habitat habitat17 = method_201(int_64, int_65);
                    if (habitat17 != null)
                    {
                        SystemInfo systemInfo4 = _Game.Galaxy.Systems[habitat17.SystemIndex];
                        if (habitat17.Category != 0 && habitat17.Category != HabitatCategoryType.GasCloud)
                        {
                            HabitatList habitatList5 = new HabitatList();
                            if (habitat17.Category == HabitatCategoryType.Planet)
                            {
                                for (int num19 = 0; num19 < systemInfo4.Habitats.Count; num19++)
                                {
                                    if (systemInfo4.Habitats[num19].Parent == habitat17)
                                    {
                                        habitatList5.Add(systemInfo4.Habitats[num19]);
                                    }
                                }
                            }
                            for (int num20 = 0; num20 < habitatList5.Count; num20++)
                            {
                                Habitat habitat18 = habitatList5[num20];
                                int_29--;
                                _Game.Galaxy.RemoveHabitat(habitat18);
                            }
                            int_29--;
                            _Game.Galaxy.RemoveHabitat(habitat17);
                        }
                        else
                        {
                            _Game.Galaxy.RemoveSystem(systemInfo4);
                            method_149();
                            picSystem.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: true, int_28, int_35, drawViewIndicator: true, erasePrevious: true, clearFirst: false, showIndicatorLines: false, string.Empty);
                        }
                    }
                    else
                    {
                        object obj = method_142(int_64, int_65);
                        if (obj != null)
                        {
                            if (obj is BuiltObject)
                            {
                                BuiltObject builtObject5 = (BuiltObject)obj;
                                if (builtObject5.Role == BuiltObjectRole.Base && _Game.Galaxy.PirateEmpires.Contains(builtObject5.Empire))
                                {
                                    int num21 = 0;
                                    for (int num22 = 0; num22 < builtObject5.Empire.BuiltObjects.Count; num22++)
                                    {
                                        if (builtObject5.Empire.BuiltObjects[num22].Role == BuiltObjectRole.Base)
                                        {
                                            num21++;
                                        }
                                    }
                                    _Game.Galaxy.ClearFromKnownPirateBases(builtObject5);
                                    if (num21 <= 1)
                                    {
                                        _Game.Galaxy.ClearFromKnownPirateBases(builtObject5.Empire);
                                        BuiltObject[] array = builtObject5.Empire.BuiltObjects.ToArray();
                                        BuiltObject[] array2 = array;
                                        foreach (BuiltObject builtObject6 in array2)
                                        {
                                            builtObject6.CompleteTeardown(_Game.Galaxy, removeFromEmpire: true);
                                        }
                                        _Game.Galaxy.PirateEmpires.Remove(builtObject5.Empire);
                                        gameEditorSelector.PirateFactions = _Game.Galaxy.PirateEmpires;
                                        gameEditorSelector.cmbPlaceShipDesign.SelectedIndex = -1;
                                    }
                                }
                                builtObject5.CompleteTeardown(_Game.Galaxy, removeFromEmpire: true);
                            }
                            else if (obj is Creature)
                            {
                                Creature creature = (Creature)obj;
                                creature.CompleteTeardown();
                            }
                            else if (obj is SystemInfo)
                            {
                                SystemInfo system = (SystemInfo)obj;
                                _Game.Galaxy.RemoveSystem(system);
                            }
                        }
                    }
                    picSystem.Invalidate();
                }
                else if (gameEditorSelector.EditMode == EditorMode.ClearColony)
                {
                    Habitat habitat19 = method_201(int_64, int_65);
                    if (habitat19 != null && habitat19.Empire != null && habitat19.Empire != _Game.Galaxy.IndependentEmpire)
                    {
                        habitat19.ClearColony(null);
                        habitat19.ConstructionQueue = null;
                        habitat19.ManufacturingQueue = null;
                        habitat19.SetDevelopmentLevel(0);
                        habitat19.GrowPopulation(new TimeSpan(0L));
                        pnlDetailInfo.Invalidate();
                    }
                }
                else if (gameEditorSelector.EditMode == EditorMode.ClearAlienRace)
                {
                    Habitat habitat20 = method_201(int_64, int_65);
                    if (habitat20 != null && habitat20.Population != null && habitat20.Population.TotalAmount > 0L)
                    {
                        bool flag = false;
                        if (habitat20.Empire == null || habitat20.Empire == _Game.Galaxy.IndependentEmpire)
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            habitat20.Population.Clear();
                            habitat20.SetDevelopmentLevel(0);
                            habitat20.GrowPopulation(new TimeSpan(0L));
                            pnlDetailInfo.Invalidate();
                        }
                    }
                }
                else if (gameEditorSelector.EditMode == EditorMode.ClearRuins)
                {
                    Habitat habitat21 = method_201(int_64, int_65);
                    if (habitat21 != null && habitat21.Ruin != null)
                    {
                        habitat21.Ruin = null;
                        if (_Game.Galaxy.RuinsHabitats.Contains(habitat21))
                        {
                            _Game.Galaxy.RuinsHabitats.Remove(habitat21);
                        }
                        mainView.ClearPrecachedHabitatBitmaps();
                        if (_Game.SelectedObject is Habitat)
                        {
                            Habitat habitat22 = (Habitat)_Game.SelectedObject;
                            Bitmap backgroundPicture = mainView.method_54(habitat22, pnlDetailInfo.ClientSize.Width);
                            pnlDetailInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, habitat22);
                        }
                    }
                }
                else if (gameEditorSelector.EditMode == EditorMode.ClearAsteroidField)
                {
                    HabitatList habitatList6 = new HabitatList();
                    Habitat habitat23 = _Game.Galaxy.FastFindNearestSystem(int_64, int_65);
                    double num24 = _Game.Galaxy.CalculateDistance(int_64, int_65, habitat23.Xpos, habitat23.Ypos);
                    if (!(num24 < (double)(Galaxy.MaxSolarSystemSize + 500)))
                    {
                        return;
                    }
                    for (int num25 = _Game.Galaxy.Systems[habitat23.SystemIndex].Habitats.Count - 1; num25 >= 0; num25--)
                    {
                        Habitat habitat24 = _Game.Galaxy.Systems[habitat23.SystemIndex].Habitats[num25];
                        num24 = _Game.Galaxy.CalculateDistance(habitat24.Xpos, habitat24.Ypos, int_64, int_65);
                        if (num24 < 4000.0 && habitat24.Category == HabitatCategoryType.Asteroid)
                        {
                            habitatList6.Add(habitat24);
                        }
                        else
                        {
                            if (habitatList6.Count > 10)
                            {
                                break;
                            }
                            habitatList6.Clear();
                        }
                    }
                    if (habitatList6.Count <= 0)
                    {
                        return;
                    }
                    habitatList6.Reverse();
                    bool flag2 = true;
                    for (int num26 = 0; num26 < habitatList6.Count; num26++)
                    {
                        if (habitatList6[num26].HabitatIndex != habitatList6[0].HabitatIndex + num26)
                        {
                            flag2 = false;
                            break;
                        }
                    }
                    if (flag2 && _Game.Galaxy.RemoveAsteroidField(habitatList6, habitat23))
                    {
                        _Game.Galaxy.Systems[habitat23.SystemIndex] = _Game.Galaxy.DetermineSystemInfo(_Game.Galaxy.Systems[habitat23.SystemIndex], _Game.PlayerEmpire);
                        method_149();
                        int_29 -= habitatList6.Count;
                        bool_19 = true;
                    }
                }
                else
                {
                    if (gameEditorSelector.EditMode != EditorMode.EmpireExploration || gameEditorSelector.EditorEmpire == null)
                    {
                        return;
                    }
                    empire_1 = gameEditorSelector.EditorEmpire;
                    Habitat habitat25 = null;
                    object obj2 = method_142(int_64, int_65);
                    if (obj2 is SystemInfo)
                    {
                        SystemInfo systemInfo5 = (SystemInfo)obj2;
                        habitat25 = systemInfo5.SystemStar;
                    }
                    else
                    {
                        habitat25 = _Game.Galaxy.FastFindNearestSystem(int_64, int_65);
                        if (habitat25 != null)
                        {
                            double num27 = _Game.Galaxy.CalculateDistance(int_64, int_65, habitat25.Xpos, habitat25.Ypos);
                            if (num27 > (double)(Galaxy.MaxSolarSystemSize + 500))
                            {
                                habitat25 = null;
                            }
                        }
                    }
                    if (habitat25 != null)
                    {
                        SystemVisibilityStatus status = empire_1.SystemVisibility[habitat25.SystemIndex].Status;
                        if (status == SystemVisibilityStatus.Undefined || status == SystemVisibilityStatus.Unexplored)
                        {
                            status = SystemVisibilityStatus.Explored;
                            empire_1.SystemVisibility[habitat25.SystemIndex].Status = SystemVisibilityStatus.Explored;
                            empire_1.ResourceMap.SetSystemResourcesKnown(habitat25, known: true);
                            method_149();
                            bool_19 = true;
                        }
                    }
                }
                return;
            }
            Habitat habitat26 = method_201(int_64, int_65);
            if (habitat26 == null || (habitat26.Category != HabitatCategoryType.Planet && habitat26.Category != HabitatCategoryType.Moon) || habitat26.Diameter < 60 || habitat26.Ruin != null)
            {
                return;
            }
            if (gameEditorSelector.EditMode == EditorMode.Ruins)
            {
                _Game.Galaxy.SelectRuins(habitat26, definitePlacement: true, assignCreatures: false, allowNegativeEffects: true);
            }
            else
            {
                switch (gameEditorSelector.EditMode)
                {
                    case EditorMode.RuinsSpecialGovernment:
                        _Game.Galaxy.SelectSpecialRuins(habitat26, EventMessageType.SpecialGovernmentType, allowCreatures: false);
                        break;
                    case EditorMode.RuinsSuperWeapon:
                        _Game.Galaxy.SelectSpecialRuins(habitat26, EventMessageType.ExoticTechDiscovered, allowCreatures: false);
                        break;
                    case EditorMode.RuinsSleepingRace:
                        _Game.Galaxy.SelectSpecialRuins(habitat26, EventMessageType.SleepersAwake, allowCreatures: false);
                        break;
                    case EditorMode.RuinsRefugees:
                        _Game.Galaxy.SelectSpecialRuins(habitat26, EventMessageType.GalacticRefugees, allowCreatures: false);
                        break;
                    case EditorMode.RuinsOrigins:
                        _Game.Galaxy.SelectSpecialRuins(habitat26, EventMessageType.OriginsDiscovery, allowCreatures: false);
                        break;
                    case EditorMode.RuinsLostShip:
                        _Game.Galaxy.SelectSpecialRuins(habitat26, EventMessageType.LostBuiltObjectCoordinates, allowCreatures: false);
                        break;
                    case EditorMode.RuinsLostColony:
                        _Game.Galaxy.SelectSpecialRuins(habitat26, EventMessageType.LostColonyCoordinates, allowCreatures: false);
                        break;
                }
            }
            mainView.ClearPrecachedHabitatBitmaps();
        }

        private void method_223(int int_64, int int_65)
        {
            method_201(int_64, int_65);
            method_199(int_64, int_65);
            if (gameEditorSelector.EditMode != EditorMode.EmpireExploration || gameEditorSelector.EditorEmpire == null)
            {
                return;
            }
            empire_1 = gameEditorSelector.EditorEmpire;
            Habitat habitat = null;
            object obj = method_142(int_64, int_65);
            if (obj is SystemInfo)
            {
                SystemInfo systemInfo = (SystemInfo)obj;
                habitat = systemInfo.SystemStar;
            }
            else
            {
                habitat = _Game.Galaxy.FastFindNearestSystem(int_64, int_65);
                if (habitat != null)
                {
                    double num = _Game.Galaxy.CalculateDistance(int_64, int_65, habitat.Xpos, habitat.Ypos);
                    if (num > (double)(Galaxy.MaxSolarSystemSize + 500))
                    {
                        habitat = null;
                    }
                }
            }
            if (habitat == null)
            {
                return;
            }
            SystemVisibilityStatus status = empire_1.SystemVisibility[habitat.SystemIndex].Status;
            if (status == SystemVisibilityStatus.Explored || status == SystemVisibilityStatus.Visible)
            {
                SystemVisibilityStatus systemVisibilityStatus = empire_1.CheckSystemVisible(habitat, null, null, null);
                if (systemVisibilityStatus != SystemVisibilityStatus.Visible)
                {
                    empire_1.SystemVisibility[habitat.SystemIndex].Status = SystemVisibilityStatus.Unexplored;
                    empire_1.ResourceMap.SetSystemResourcesKnown(habitat, known: false);
                }
                method_149();
                bool_19 = true;
            }
        }

        private void method_224(int int_64, int int_65)
        {
            if (_Game.SelectedObject != null)
            {
                if (_Game.SelectedObject is Creature)
                {
                    method_479();
                }
                if (_Game.SelectedObject is BuiltObject)
                {
                    PilQidafZH();
                }
                if (_Game.SelectedObject is Habitat)
                {
                    Habitat habitat_ = (Habitat)_Game.SelectedObject;
                    method_491(habitat_);
                }
            }
        }

        private void method_225()
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            soundEffectRequest.Balance = 0.0;
            soundEffectRequest.Filename = "grid.wav";
            soundEffectRequest.Volume = effectsPlayer_0.Volume;
            method_0(soundEffectRequest);
        }

        private void method_226()
        {
            try
            {
                if (soundPlayer_0 == null)
                {
                    soundPlayer_0 = new SoundPlayer();
                }
                soundPlayer_0.SoundLocation = string_28;
                soundPlayer_0.Load();
                if (_Game.SoundEffectsVolume > 0.0)
                {
                    soundPlayer_0.Play();
                }
            }
            catch (Exception)
            {
            }
        }

        private void mainView_MouseDown(object sender, MouseEventArgs e)
        {
            _pressedKeys.Clear();
            Point point = PointToClient(MouseHelper.GetCursorPosition());
            int int_ = point.X;
            int int_2 = point.Y;
            method_151(ref int_, ref int_2);
            itemListCollectionPanel_0.MouseDown(e.Location);
            bool_16 = false;
            int_32 = int_;
            int_33 = int_2;
        }

        private void mainView_MouseUp(object sender, MouseEventArgs e)
        {
            _pressedKeys.Clear();
            Point pt = PointToClient(MouseHelper.GetCursorPosition());
            int int_ = pt.X;
            int int_2 = pt.Y;
            method_151(ref int_, ref int_2);
            if (method_470() || itemListCollectionPanel_0.AreaMaximum.Contains(pt) || (int_ == int_32 && int_2 == int_33))
            {
                return;
            }
            BuiltObjectList builtObjectList = method_141(int_32, int_33, int_, int_2);
            if (builtObjectList == null)
            {
                return;
            }
            if (builtObjectList.Count > 1)
            {
                BuiltObjectList builtObjectList2 = new BuiltObjectList();
                for (int i = 0; i < builtObjectList.Count; i++)
                {
                    BuiltObject builtObject = builtObjectList[i];
                    if (method_140(builtObject))
                    {
                        builtObjectList2.Add(builtObject);
                    }
                }
                if (builtObjectList2.Count > 1)
                {
                    method_208(builtObjectList2);
                }
                else if (builtObjectList2.Count == 1)
                {
                    method_208(builtObjectList2[0]);
                }
                else
                {
                    method_208(null);
                }
                return;
            }
            if (builtObjectList.Count == 1)
            {
                method_208(builtObjectList[0]);
                return;
            }
            object obj = method_142(int_32, int_33);
            if (obj == null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    method_208(null);
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                method_208(obj);
            }
        }

        private void mainView_MouseClick(object sender, MouseEventArgs e)
        {
            _pressedKeys.Clear();
            Point point = PointToClient(MouseHelper.GetCursorPosition());
            int int_ = point.X;
            int int_2 = point.Y;
            method_151(ref int_, ref int_2);
            if (bool_16 || (!method_470() && (diplomaticMessageQueue_0.CheckClick(e.Location) || itemListCollectionPanel_0.CheckClick(e.Location, e.Button, isDoubleClick: false))))
            {
                return;
            }
            if (mouseHoverMode_0 != 0)
            {
                object obj = method_143(int_, int_2, bool_28: false);
                switch (mouseHoverMode_0)
                {
                    case MouseHoverMode.SetFleetAttackPoint:
                        if (shipGroup_2 == null)
                        {
                            break;
                        }
                        if (obj != null)
                        {
                            if (obj is Habitat)
                            {
                                Habitat habitat3 = (Habitat)obj;
                                if (habitat3.Empire != null && habitat3.Empire != _Game.PlayerEmpire)
                                {
                                    shipGroup_2.AttackPoint = habitat3;
                                }
                            }
                            else if (obj is BuiltObject)
                            {
                                BuiltObject builtObject3 = (BuiltObject)obj;
                                if (builtObject3.Role == BuiltObjectRole.Base && builtObject3.Empire != null && builtObject3.Empire != _Game.PlayerEmpire)
                                {
                                    shipGroup_2.AttackPoint = builtObject3;
                                }
                            }
                        }
                        else
                        {
                            shipGroup_2.AttackPoint = null;
                        }
                        pnlDetailInfo.Invalidate();
                        break;
                    case MouseHoverMode.SetFleetHomeBase:
                        if (shipGroup_2 == null)
                        {
                            break;
                        }
                        if (obj != null)
                        {
                            BuiltObject shipToRefuel = _Game.PlayerEmpire.BuiltObjects.FindFirstBuiltObject(BuiltObjectRole.Military);
                            if (obj is Habitat)
                            {
                                Habitat habitat2 = (Habitat)obj;
                                if (_Game.Galaxy.IsStellarObjectDockable(habitat2, _Game.PlayerEmpire) && _Game.Galaxy.CheckEmpireCanRefuelAtEmpire(shipToRefuel, habitat2.Empire, _Game.PlayerEmpire))
                                {
                                    shipGroup_2.GatherPoint = habitat2;
                                }
                            }
                            else if (obj is BuiltObject)
                            {
                                BuiltObject builtObject2 = (BuiltObject)obj;
                                if (builtObject2.Role == BuiltObjectRole.Base && _Game.Galaxy.IsStellarObjectDockable(builtObject2, _Game.PlayerEmpire) && _Game.Galaxy.CheckEmpireCanRefuelAtEmpire(shipToRefuel, builtObject2.Empire, _Game.PlayerEmpire))
                                {
                                    shipGroup_2.GatherPoint = builtObject2;
                                }
                            }
                        }
                        else
                        {
                            shipGroup_2.GatherPoint = null;
                        }
                        pnlDetailInfo.Invalidate();
                        break;
                    case MouseHoverMode.SetEventActionTarget:
                        {
                            StellarObject stellarObject = null;
                            EventActionType eventActionType = EventActionType.Undefined;
                            if (obj != null && obj is BuiltObject)
                            {
                                BuiltObject builtObject = (BuiltObject)obj;
                                stellarObject = builtObject;
                                eventActionType = EventActionType.DestroyBuiltObject;
                            }
                            else if (obj != null && obj is Habitat)
                            {
                                Habitat habitat = (Habitat)obj;
                                stellarObject = habitat;
                                eventActionType = EventActionType.DisasterAtColony;
                            }
                            if (stellarObject != null && eventActionType != 0)
                            {
                                EventAction eventAction = new EventAction(stellarObject, eventActionType);
                                if (ctlGameEvent.GameEvent != null)
                                {
                                    ctlGameEvent.GameEvent.Actions.Add(eventAction);
                                    method_675(ctlGameEvent.GameEvent, eventAction);
                                }
                            }
                            mouseHoverMode_0 = MouseHoverMode.Undefined;
                            break;
                        }
                }
                mouseHoverMode_0 = MouseHoverMode.Undefined;
            }
            else
            {
                Keyboard keyboard = new Keyboard();
                if (keyboard.ShiftKeyDown && e.Button == MouseButtons.Left)
                {
                    BuiltObjectList builtObjectList_ = null;
                    object obj2 = method_145(int_, int_2, bool_28: false, out builtObjectList_);
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    if (builtObjectList_ != null && builtObjectList_.Count > 1)
                    {
                        for (int i = 0; i < builtObjectList_.Count; i++)
                        {
                            BuiltObject builtObject4 = builtObjectList_[i];
                            if (method_140(builtObject4))
                            {
                                builtObjectList.Add(builtObject4);
                            }
                        }
                    }
                    else if (obj2 != null && obj2 is BuiltObject && method_140((BuiltObject)obj2))
                    {
                        builtObjectList.Add((BuiltObject)obj2);
                    }
                    if (builtObjectList.Count <= 0)
                    {
                        return;
                    }
                    if (_Game.SelectedObject != null && _Game.SelectedObject is BuiltObjectList)
                    {
                        BuiltObjectList builtObjectList2 = (BuiltObjectList)_Game.SelectedObject;
                        for (int j = 0; j < builtObjectList.Count; j++)
                        {
                            BuiltObject item = builtObjectList[j];
                            if (builtObjectList2.Contains(item))
                            {
                                builtObjectList2.Remove(item);
                            }
                            else
                            {
                                builtObjectList2.Add(item);
                            }
                        }
                        if (builtObjectList2.Count > 0)
                        {
                            method_209(builtObjectList2, bool_28: false);
                        }
                        else
                        {
                            method_208(null);
                        }
                    }
                    else if (_Game.SelectedObject != null && _Game.SelectedObject is BuiltObject)
                    {
                        BuiltObjectList builtObjectList3 = new BuiltObjectList();
                        BuiltObject builtObject5 = (BuiltObject)_Game.SelectedObject;
                        if (method_140(builtObject5))
                        {
                            for (int k = 0; k < builtObjectList.Count; k++)
                            {
                                BuiltObject builtObject6 = builtObjectList[k];
                                if (builtObject5 == builtObject6)
                                {
                                    builtObject5 = null;
                                }
                                else
                                {
                                    builtObjectList3.Add(builtObject6);
                                }
                            }
                            if (builtObject5 != null)
                            {
                                builtObjectList3.Insert(0, builtObject5);
                            }
                            if (builtObjectList3.Count > 0)
                            {
                                method_209(builtObjectList3, bool_28: true);
                            }
                            else
                            {
                                method_208(null);
                            }
                        }
                        else
                        {
                            builtObjectList3.AddRange(builtObjectList);
                            method_209(builtObjectList3, bool_28: true);
                        }
                    }
                    else
                    {
                        method_208(builtObjectList);
                    }
                    return;
                }
                if (keyboard.CtrlKeyDown && e.Button == MouseButtons.Left)
                {
                    method_4(1.0);
                    int_13 = int_;
                    int_14 = int_2;
                    bool_20 = true;
                    mainView.ClearPrecachedHabitatBitmaps();
                    mainView.ClearPreprocessedBuiltObjectImages();
                    mainView.ClearPreprocessedFighterImages();
                    mainView.ClearPreprocessedCreatureImages();
                    mainView.list_34 = null;
                    if (method_149() != habitat_6)
                    {
                        bool_19 = true;
                    }
                    return;
                }
                empire_1 = null;
                if (pnlGameEditor.Visible && gameEditorSelector.EditMode != 0)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        method_222(int_, int_2);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        method_223(int_, int_2);
                    }
                }
                if (e.Button == MouseButtons.Left)
                {
                    BuiltObjectList builtObjectList_2 = null;
                    object obj3 = method_145(int_, int_2, bool_28: false, out builtObjectList_2);
                    if (builtObjectList_2 != null)
                    {
                        Habitat habitat4 = method_146(int_, int_2);
                        if (builtObjectList_2.Count > 1 || (builtObjectList_2.Count > 0 && habitat4 != null && (habitat4.Empire != null || habitat4.Category == HabitatCategoryType.Asteroid || habitat4.Diameter < 150)))
                        {
                            for (int l = 0; l < builtObjectList_2.Count; l++)
                            {
                                BuiltObject objectToTest = builtObjectList_2[l];
                                if (_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(objectToTest))
                                {
                                    builtObjectList_0 = builtObjectList_2;
                                    habitat_3 = habitat4;
                                    method_355();
                                    Point point2 = PointToClient(MouseHelper.GetCursorPosition());
                                    selectionMenu.Show(point2.X, point2.Y);
                                    return;
                                }
                            }
                        }
                    }
                    if (obj3 != null)
                    {
                        method_225();
                    }
                    method_208(obj3);
                }
                else if (e.Button == MouseButtons.Right && !keyboard.CtrlKeyDown)
                {
                    if (builtObject_5 != null && shipAction_0 != null)
                    {
                        if (builtObject_5.BuiltAt != null)
                        {
                            return;
                        }
                        if (shipAction_0.MissionType == BuiltObjectMissionType.Blockade && shipAction_0.Target != null)
                        {
                            if (shipAction_0.Target is Habitat)
                            {
                                Habitat colony = (Habitat)shipAction_0.Target;
                                Blockade blockade = _Game.Galaxy.Blockades[colony];
                                if (blockade != null)
                                {
                                    if (blockade.Initiator != builtObject_5.Empire)
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    builtObject_5.Empire.ImplementBlockade(colony, sendFleet: false, performAuthorizationCheck: false);
                                }
                            }
                            else if (shipAction_0.Target is BuiltObject)
                            {
                                BuiltObject builtObject7 = (BuiltObject)shipAction_0.Target;
                                Blockade blockade2 = _Game.Galaxy.Blockades[builtObject7];
                                if (blockade2 != null)
                                {
                                    if (blockade2.Initiator != builtObject_5.Empire)
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    builtObject_5.Empire.ImplementBlockade(builtObject7, sendFleet: false, performAuthorizationCheck: false);
                                }
                            }
                        }
                        if (shipAction_0.MissionType == BuiltObjectMissionType.LoadTroops && shipAction_0.Target != null && shipAction_0.Target is Habitat)
                        {
                            builtObject_5.ClearPreviousMissionRequirements(manuallyAssigned: true);
                            builtObject_5.Empire.AssignLoadTroopsMission(builtObject_5, (Habitat)shipAction_0.Target, queueMission: false, enforceMinimumTroopLimits: true, manuallyAssigned: true);
                            builtObject_5.IsAutoControlled = false;
                            return;
                        }
                        if (shipAction_0.MissionType == BuiltObjectMissionType.UnloadTroops && shipAction_0.Target != null && shipAction_0.Target is Habitat && builtObject_5.Troops != null)
                        {
                            builtObject_5.ClearPreviousMissionRequirements(manuallyAssigned: true);
                            builtObject_5.AssignMission(BuiltObjectMissionType.UnloadTroops, (Habitat)shipAction_0.Target, null, builtObject_5.Troops, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            builtObject_5.IsAutoControlled = false;
                            return;
                        }
                        builtObject_5.ClearPreviousMissionRequirements(manuallyAssigned: true);
                        if (shipAction_0.Target2 != null && shipAction_0.Target != builtObject_5)
                        {
                            if (shipAction_0.Target2 is BuiltObject)
                            {
                                BuiltObject builtObject8 = (BuiltObject)shipAction_0.Target2;
                                builtObject_5.AssignMission(shipAction_0.MissionType, shipAction_0.Target, builtObject8, builtObject8.Xpos, builtObject8.Ypos, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                                builtObject_5.IsAutoControlled = false;
                            }
                            return;
                        }
                        if (shipAction_0.Design != null)
                        {
                            if (shipAction_0.Position.X == 0 && shipAction_0.Position.Y == 0)
                            {
                                builtObject_5.AssignMission(shipAction_0.MissionType, shipAction_0.Target, null, shipAction_0.Design, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                            else
                            {
                                builtObject_5.AssignMission(shipAction_0.MissionType, shipAction_0.Target, null, shipAction_0.Design, shipAction_0.Position.X, shipAction_0.Position.Y, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                        }
                        else
                        {
                            if (shipAction_0.Position.X == 0 && shipAction_0.Position.Y == 0)
                            {
                                builtObject_5.AssignMission(shipAction_0.MissionType, shipAction_0.Target, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                            else
                            {
                                builtObject_5.AssignMission(shipAction_0.MissionType, shipAction_0.Target, null, shipAction_0.Position.X, shipAction_0.Position.Y, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                            if (shipAction_0.MissionType == BuiltObjectMissionType.Attack || shipAction_0.MissionType == BuiltObjectMissionType.Bombard)
                            {
                                method_0(effectsPlayer_0.ResolveAttackClick());
                            }
                        }
                        builtObject_5.IsAutoControlled = false;
                    }
                    else if (shipGroup_2 != null && shipAction_0 != null)
                    {
                        if (shipAction_0.MissionType == BuiltObjectMissionType.Blockade && shipAction_0.Target != null)
                        {
                            if (shipAction_0.Target is Habitat)
                            {
                                Habitat colony2 = (Habitat)shipAction_0.Target;
                                Blockade blockade3 = _Game.Galaxy.Blockades[colony2];
                                if (blockade3 != null)
                                {
                                    if (blockade3.Initiator != shipGroup_2.Empire)
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    method_348(shipGroup_2, bool_28: false);
                                    shipGroup_2.Empire.ImplementBlockade(colony2, sendFleet: false, performAuthorizationCheck: false);
                                }
                            }
                            else if (shipAction_0.Target is BuiltObject)
                            {
                                BuiltObject builtObject9 = (BuiltObject)shipAction_0.Target;
                                Blockade blockade4 = _Game.Galaxy.Blockades[builtObject9];
                                if (blockade4 != null)
                                {
                                    if (blockade4.Initiator != shipGroup_2.Empire)
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    method_348(shipGroup_2, bool_28: false);
                                    shipGroup_2.Empire.ImplementBlockade(builtObject9, sendFleet: false, performAuthorizationCheck: false);
                                }
                            }
                        }
                        method_348(shipGroup_2, bool_28: false);
                        if (shipAction_0.MissionType == BuiltObjectMissionType.LoadTroops)
                        {
                            if (shipAction_0.Target != null)
                            {
                                if (shipAction_0.Target is Habitat)
                                {
                                    Habitat colony3 = (Habitat)shipAction_0.Target;
                                    shipGroup_2.Empire.AssignFleetLoadTroops(shipGroup_2, colony3, manuallyAssigned: true);
                                }
                            }
                            else
                            {
                                shipGroup_2.Empire.AssignFleetLoadTroops(shipGroup_2, manuallyAssigned: true);
                            }
                            return;
                        }
                        if (shipAction_0.MissionType == BuiltObjectMissionType.UnloadTroops)
                        {
                            if (shipAction_0.Target != null && shipAction_0.Target is Habitat)
                            {
                                Habitat colony4 = (Habitat)shipAction_0.Target;
                                shipGroup_2.Empire.AssignFleetUnloadTroops(shipGroup_2, colony4, manuallyAssigned: true);
                            }
                            return;
                        }
                        if (shipAction_0.Design != null)
                        {
                            if (shipAction_0.Position.X == 0 && shipAction_0.Position.Y == 0)
                            {
                                shipGroup_2.AssignMission(shipAction_0.MissionType, shipAction_0.Target, null, shipAction_0.Design, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                            else
                            {
                                shipGroup_2.AssignMission(shipAction_0.MissionType, shipAction_0.Target, null, null, shipAction_0.Design, shipAction_0.Position.X, shipAction_0.Position.Y, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                        }
                        else
                        {
                            BuiltObjectMissionPriority priority = BuiltObjectMissionPriority.Normal;
                            if (shipAction_0.MissionType == BuiltObjectMissionType.Attack || shipAction_0.MissionType == BuiltObjectMissionType.Bombard || shipAction_0.MissionType == BuiltObjectMissionType.WaitAndAttack || shipAction_0.MissionType == BuiltObjectMissionType.WaitAndBombard)
                            {
                                priority = BuiltObjectMissionPriority.High;
                            }
                            if (shipAction_0.Position.X == 0 && shipAction_0.Position.Y == 0)
                            {
                                BuiltObjectMissionType missionType = shipAction_0.MissionType;
                                if (keyboard.AltKeyDown)
                                {
                                    switch (missionType)
                                    {
                                        case BuiltObjectMissionType.Attack:
                                            missionType = BuiltObjectMissionType.WaitAndAttack;
                                            break;
                                        case BuiltObjectMissionType.Bombard:
                                            missionType = BuiltObjectMissionType.WaitAndBombard;
                                            break;
                                    }
                                }
                                if (missionType != BuiltObjectMissionType.WaitAndAttack && missionType != BuiltObjectMissionType.WaitAndBombard)
                                {
                                    if (shipAction_0.MissionType == BuiltObjectMissionType.Patrol && shipAction_0.Target is Habitat && (((Habitat)shipAction_0.Target).Category == HabitatCategoryType.GasCloud || ((Habitat)shipAction_0.Target).Category == HabitatCategoryType.Star))
                                    {
                                        SystemInfo system = _Game.Galaxy.Systems[(Habitat)shipAction_0.Target];
                                        if (shipGroup_2.Empire.AssignFleetSystemPatrol(shipGroup_2, system))
                                        {
                                            return;
                                        }
                                    }
                                    else if (shipAction_0.MissionType == BuiltObjectMissionType.Patrol && shipAction_0.Target is SystemInfo)
                                    {
                                        SystemInfo system2 = (SystemInfo)shipAction_0.Target;
                                        if (shipGroup_2.Empire.AssignFleetSystemPatrol(shipGroup_2, system2))
                                        {
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (shipGroup_2.Empire.CheckAssignFleetWaitAndAttackMission(shipGroup_2, ref missionType, shipAction_0.Target, priority))
                                    {
                                        return;
                                    }
                                    shipAction_0.SetMissionType(missionType);
                                }
                                shipGroup_2.AssignMission(missionType, shipAction_0.Target, null, priority, manuallyAssigned: true);
                            }
                            else
                            {
                                shipGroup_2.AssignMission(shipAction_0.MissionType, shipAction_0.Target, null, shipAction_0.Position.X, shipAction_0.Position.Y, priority, manuallyAssigned: true);
                            }
                            if (shipAction_0.MissionType == BuiltObjectMissionType.Attack || shipAction_0.MissionType == BuiltObjectMissionType.Bombard || shipAction_0.MissionType == BuiltObjectMissionType.WaitAndAttack || shipAction_0.MissionType == BuiltObjectMissionType.WaitAndBombard)
                            {
                                method_0(effectsPlayer_0.ResolveAttackClick());
                            }
                        }
                    }
                    else if (_Game.SelectedObject == null)
                    {
                        BuiltObjectList builtObjectList4 = new BuiltObjectList();
                        for (int m = 0; m < _Game.PlayerEmpire.BuiltObjects.Count; m++)
                        {
                            BuiltObject builtObject10 = _Game.PlayerEmpire.BuiltObjects[m];
                            if ((builtObject10.Mission == null || builtObject10.Mission.Type == BuiltObjectMissionType.Undefined) && builtObject10.Role != BuiltObjectRole.Base && builtObject10.BuiltAt == null && builtObject10.ShipGroup == null)
                            {
                                builtObjectList4.Add(builtObject10);
                            }
                        }
                        if (builtObjectList4.Count > 0)
                        {
                            method_355();
                            method_354(builtObjectList4, null);
                            selectionMenu.Show(MouseHelper.GetCursorPosition().X, MouseHelper.GetCursorPosition().Y);
                        }
                    }
                    else
                    {
                        int_13 = int_;
                        int_14 = int_2;
                        method_149();
                    }
                }
            }
            mouseHoverMode_0 = MouseHoverMode.Undefined;
        }

        private void method_227(object sender, EventArgs e)
        {
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            method_175(selectedBuiltObject);
        }

        private void method_228(object sender, EventArgs e)
        {
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            method_176(selectedBuiltObject);
        }

        private void method_229()
        {
            ctlDiplomacyTradeThem.Reset();
            ctlDiplomacyTradeUs.Reset();
        }

        private void method_230(ConversationOption conversationOption_0)
        {
            string text = string.Empty;
            Empire initiator = conversationOption_0.Initiator;
            Empire empire = ctlDiplomacyTradeThem.Empire;
            if (initiator == empire)
            {
                empire = _Game.PlayerEmpire;
            }
            DialogPartType type = conversationOption_0.Type;
            object relatedInfo = conversationOption_0.RelatedInfo;
            double cost = conversationOption_0.Cost;
            Race dominantRace = ctlDiplomacyTradeThem.Empire.DominantRace;
            TradeableItemList tradeableItemList = null;
            TradeableItemList tradeableItemList2 = null;
            if (relatedInfo is object[])
            {
                object[] array = (object[])relatedInfo;
                tradeableItemList = (TradeableItemList)array[0];
                tradeableItemList2 = (TradeableItemList)array[1];
            }
            try
            {
                Habitat habitat = null;
                GalaxyLocation galaxyLocation = null;
                string name = TextResolver.GetText("unknown");
                Empire empire2 = null;
                string empty = string.Empty;
                string empty2 = string.Empty;
                TradeableItem tradeableItem = null;
                switch (type)
                {
                    case DialogPartType.CANCELTREATY:
                        {
                            DiplomaticRelationType type2 = (DiplomaticRelationType)relatedInfo;
                            text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), Galaxy.ResolveDescription(type2));
                            break;
                        }
                    case DialogPartType.MUTUALDEFENSE_REQUESTHELP:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.MUTUALDEFENSE_HONORREQUESTHELP:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.INFO_OFFER_UNMETEMPIRE:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.INFO_OFFER_INDEPENDENTCOLONY:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.INFO_OFFER_SYSTEMMAPS:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.INFO_OFFER_RUINS:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.INFO_OFFER_RESTRICTEDAREA:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.INFO_OFFER_DEBRISFIELD:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.INFO_OFFER_PLANETDESTROYER:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.INFO_UNMETEMPIRE:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), ((Empire)relatedInfo).Name);
                        break;
                    case DialogPartType.INFO_EXPLORATION:
                        habitat = (Habitat)relatedInfo;
                        empty2 = _Game.Galaxy.ResolveSectorDescription(habitat.Xpos, habitat.Ypos);
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), habitat.Name, empty2);
                        break;
                    case DialogPartType.INFO_INDEPENDENTCOLONY:
                        habitat = (Habitat)relatedInfo;
                        empty = _Game.Galaxy.GenerateLocationDescription(habitat);
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empty);
                        break;
                    case DialogPartType.INFO_RUINS:
                        {
                            habitat = (Habitat)relatedInfo;
                            string name2 = TextResolver.GetText("Ancient Ruins");
                            if (habitat.Ruin != null)
                            {
                                name2 = habitat.Ruin.Name;
                            }
                            empty = _Game.Galaxy.GenerateLocationDescription(habitat);
                            text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), name2, empty);
                            break;
                        }
                    case DialogPartType.INFO_DEBRISFIELD:
                        galaxyLocation = (GalaxyLocation)relatedInfo;
                        habitat = _Game.Galaxy.FastFindNearestSystem(galaxyLocation.Xpos, galaxyLocation.Ypos);
                        if (habitat != null)
                        {
                            name = habitat.Name;
                        }
                        empty2 = _Game.Galaxy.ResolveSectorDescription(galaxyLocation.Xpos, galaxyLocation.Ypos);
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), name, empty2);
                        break;
                    case DialogPartType.INFO_PLANETDESTROYER:
                        galaxyLocation = (GalaxyLocation)relatedInfo;
                        habitat = _Game.Galaxy.FastFindNearestSystem(galaxyLocation.Xpos, galaxyLocation.Ypos);
                        if (habitat != null)
                        {
                            name = habitat.Name;
                        }
                        empty2 = _Game.Galaxy.ResolveSectorDescription(galaxyLocation.Xpos, galaxyLocation.Ypos);
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), name, empty2);
                        break;
                    case DialogPartType.INFO_RESTRICTEDAREA:
                        galaxyLocation = (GalaxyLocation)relatedInfo;
                        habitat = _Game.Galaxy.FastFindNearestSystem(galaxyLocation.Xpos, galaxyLocation.Ypos);
                        if (habitat != null)
                        {
                            name = habitat.Name;
                        }
                        empty2 = _Game.Galaxy.ResolveSectorDescription(galaxyLocation.Xpos, galaxyLocation.Ypos);
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), name, empty2);
                        break;
                    case DialogPartType.PIRATE_ATTACKOFFER_SINGLEEMPIRE:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name, cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.PIRATE_ATTACKCOMMENCE:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.PIRATE_ALLIANCEPROPOSE:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.GREETING_INTRODUCTION:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name, empire2.DominantRace.Name);
                        break;
                    case DialogPartType.OFFER_DEAL_RESPONSE:
                        text = dialogSet_0.ResolveDialog(DialogPartType.OFFER_DEAL_RESPONSE, dominantRace);
                        break;
                    case DialogPartType.OFFER_DEAL_TERRITORYMAP:
                        tradeableItem = (TradeableItem)relatedInfo;
                        if (tradeableItem.Type == TradeableItemType.TerritoryMap)
                        {
                            text = dialogSet_0.ResolveDialog(DialogPartType.OFFER_DEAL_TERRITORYMAP, dominantRace);
                        }
                        break;
                    case DialogPartType.OFFER_DEAL_GALAXYMAP:
                        tradeableItem = (TradeableItem)relatedInfo;
                        if (tradeableItem.Type == TradeableItemType.GalaxyMap)
                        {
                            text = dialogSet_0.ResolveDialog(DialogPartType.OFFER_DEAL_GALAXYMAP, dominantRace);
                        }
                        break;
                    case DialogPartType.OFFER_DEAL_COMPONENT:
                        tradeableItem = (TradeableItem)relatedInfo;
                        if (tradeableItem.Type == TradeableItemType.ResearchProject)
                        {
                            ResearchNode researchNode = (ResearchNode)tradeableItem.Item;
                            text = string.Format(dialogSet_0.ResolveDialog(DialogPartType.OFFER_DEAL_COMPONENT, dominantRace), researchNode.Name, tradeableItem.Value.ToString("###,###,###,##0"));
                        }
                        break;
                    case DialogPartType.DEAL_OFFER:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), tradeableItemList.ToString(), tradeableItemList2.ToString());
                        break;
                    case DialogPartType.DEAL_DEMAND:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), tradeableItemList.ToString(), tradeableItemList2.ToString());
                        break;
                    case DialogPartType.DEAL_THREAT:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), tradeableItemList.ToString(), tradeableItemList2.ToString());
                        break;
                    case DialogPartType.GIFT_GIVE:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), cost.ToString("###,###,###,##0"));
                        break;
                    case DialogPartType.TRADESANCTIONS_IMPOSE:
                        text = ((!(relatedInfo is string) || !((string)relatedInfo).ToLower(CultureInfo.InvariantCulture).Contains("persuaded")) ? dialogSet_0.ResolveDialog(type, dominantRace) : TextResolver.GetText("Another empire has persuaded us to impose trade sanctions on you"));
                        break;
                    case DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER_ACCEPT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER_REJECT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT_ACCEPT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT_REJECT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.WAR_DECLARE:
                        text = ((!(relatedInfo is string) || !((string)relatedInfo).ToLower(CultureInfo.InvariantCulture).Contains("persuaded")) ? dialogSet_0.ResolveDialog(type, dominantRace) : TextResolver.GetText("Another empire has persuaded us to declare war on you"));
                        break;
                    case DialogPartType.WAR_DECLARE_REQUESTJOINT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.WAR_DECLARE_REQUESTJOINT_ACCEPT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.WAR_DECLARE_REQUESTJOINT_REJECT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.WAR_END_REQUESTOTHER:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.WAR_END_REQUESTOTHER_ACCEPT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.WAR_END_REQUESTOTHER_REJECT:
                        empire2 = (Empire)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), empire2.Name);
                        break;
                    case DialogPartType.PIRATE_PROTECTIONPROPOSE_OFFER_ACCEPT:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), conversationOption_0.Cost.ToString("0"));
                        break;
                    case DialogPartType.PIRATE_PROTECTIONPROPOSE_OFFER_REJECT:
                        text = dialogSet_0.ResolveDialog(type, dominantRace);
                        break;
                    case DialogPartType.PIRATE_EXTORTPROTECTION:
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), conversationOption_0.Cost.ToString("###,##0"));
                        break;
                    case DialogPartType.WARNING_REMOVEFORCESSYSTEM:
                        habitat = (Habitat)relatedInfo;
                        text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), habitat.Name);
                        break;
                    case DialogPartType.WARNING_REMOVEFORCESSYSTEM_RESPONSE_COMPLY:
                    case DialogPartType.WARNING_REMOVEFORCESSYSTEM_RESPONSE_REFUSE:
                    case DialogPartType.WARNING_REMOVEFORCESSYSTEM_RESPONSE_NOFORCESPRESENT:
                        text = dialogSet_0.ResolveDialog(type, dominantRace);
                        if (conversationOption_0.RelatedInfo != null && conversationOption_0.RelatedInfo is Habitat)
                        {
                            Habitat habitat2 = (Habitat)conversationOption_0.RelatedInfo;
                            text = string.Format(text, habitat2.Name);
                        }
                        break;
                    case DialogPartType.WARNING_GENERAL:
                        text = conversationOption_0.Text;
                        break;
                    case DialogPartType.HISTORY_LOCATIONHINT:
                        {
                            string arg = _Game.Galaxy.CheckForStoryLocationHint();
                            text = string.Format(dialogSet_0.ResolveDialog(type, dominantRace), arg);
                            break;
                        }
                    default:
                        text = dialogSet_0.ResolveDialog(type, dominantRace);
                        break;
                    case DialogPartType.HISTORY_OFFER_LOCATIONHINT:
                    case DialogPartType.HISTORY_OFFER_STORYCLUE:
                    case DialogPartType.HISTORY_OFFER_STORYMESSAGE:
                        text = dialogSet_0.ResolveDialog(type, dominantRace);
                        break;
                    case DialogPartType.OFFER_DEAL:
                        break;
                }
            }
            catch
            {
                text = "[" + TextResolver.GetText("Could not parse message") + ": " + type.ToString() + "]";
            }
            pnlDiplomaticConversationResponse.Message = text;
        }

        private int method_231(Empire empire_5, Empire empire_6)
        {
            if (empire_5 != null && empire_6 != null)
            {
                EmpireEvaluation empireEvaluation = empire_5.ObtainEmpireEvaluation(empire_6);
                if (empireEvaluation != null)
                {
                    if (empireEvaluation.OverallAttitude < -10)
                    {
                        return -1;
                    }
                    if (empireEvaluation.OverallAttitude > 10)
                    {
                        return 1;
                    }
                    return 0;
                }
            }
            return 0;
        }

        private DiplomaticRelationType method_232(Empire empire_5, Empire empire_6)
        {
            DiplomaticRelationType diplomaticRelationType = DiplomaticRelationType.None;
            if (empire_6.PirateEmpireBaseHabitat == null)
            {
                DiplomaticRelation diplomaticRelation = empire_5.ObtainDiplomaticRelation(empire_6);
                diplomaticRelationType = empire_5.DetermineDesiredDiplomaticRelationTypical(diplomaticRelation.Strategy, diplomaticRelation.Type);
                if (diplomaticRelationType == DiplomaticRelationType.MutualDefensePact)
                {
                    double num = (double)empire_6.MilitaryPotency / (double)empire_5.MilitaryPotency;
                    if (num > 5.0)
                    {
                        diplomaticRelationType = DiplomaticRelationType.Protectorate;
                    }
                }
            }
            return diplomaticRelationType;
        }

        private Empire method_233(Empire empire_5, Empire empire_6, out Empire empire_7)
        {
            empire_7 = empire_5;
            if (empire_6.MilitaryPotency > empire_5.MilitaryPotency)
            {
                empire_7 = empire_5;
            }
            else
            {
                empire_7 = empire_6;
            }
            return empire_5;
        }

        private ConversationOption method_234(ConversationOption conversationOption_0, DialogPartType dialogPartType_0, Empire empire_5)
        {
            conversationOption_0.Type = dialogPartType_0;
            conversationOption_0.Initiator = empire_5;
            return conversationOption_0;
        }

        private void method_235(Empire empire_5, Empire empire_6)
        {
            DiplomaticRelation diplomaticRelation = empire_5.ProposedDiplomaticRelations[empire_6];
            if (diplomaticRelation != null)
            {
                empire_5.ProposedDiplomaticRelations.Remove(diplomaticRelation);
            }
        }

        private DialogPartType method_236(Empire empire_5, Empire empire_6)
        {
            DialogPartType result = DialogPartType.GREETING_NEUTRAL;
            switch (method_231(empire_5, empire_6))
            {
                case -1:
                    result = DialogPartType.GREETING_ANGRY;
                    break;
                case 0:
                    result = DialogPartType.GREETING_NEUTRAL;
                    break;
                case 1:
                    result = DialogPartType.GREETING_FRIENDLY;
                    break;
            }
            return result;
        }

        private ConversationOption method_237(ConversationOption conversationOption_0)
        {
            Empire initiator = conversationOption_0.Initiator;
            Empire empire = ctlDiplomacyTradeThem.Empire;
            if (initiator == empire)
            {
                empire = _Game.PlayerEmpire;
            }
            _ = empire.PirateEmpireBaseHabitat;
            int num = method_231(empire, initiator);
            long num2 = long.MaxValue;
            int num3 = -1;
            EmpireEvaluation empireEvaluation = null;
            DiplomaticRelation diplomaticRelation = null;
            DiplomaticRelation diplomaticRelation2 = null;
            Empire empire2 = null;
            Habitat habitat = null;
            GalaxyLocation galaxyLocation = null;
            TradeableItemList tradeableItemList = null;
            TradeableItemList tradeableItemList2 = null;
            TradeableItemList tradeableItemList3 = new TradeableItemList();
            TradeableItemList tradeableItemList4 = new TradeableItemList();
            TradeableItemList tradeableItemList5 = new TradeableItemList();
            TradeableItemList tradeableItemList6 = new TradeableItemList();
            if (conversationOption_0.RelatedInfo != null)
            {
                if (conversationOption_0.RelatedInfo is Empire)
                {
                    empire2 = (Empire)conversationOption_0.RelatedInfo;
                }
                else if (conversationOption_0.RelatedInfo is Habitat)
                {
                    habitat = (Habitat)conversationOption_0.RelatedInfo;
                }
                else if (conversationOption_0.RelatedInfo is GalaxyLocation)
                {
                    galaxyLocation = (GalaxyLocation)conversationOption_0.RelatedInfo;
                }
                else if (conversationOption_0.RelatedInfo is object[])
                {
                    object[] array = (object[])conversationOption_0.RelatedInfo;
                    tradeableItemList = (TradeableItemList)array[0];
                    tradeableItemList2 = (TradeableItemList)array[1];
                    if (array.Length > 2)
                    {
                        tradeableItemList3 = (TradeableItemList)array[2];
                        tradeableItemList4 = (TradeableItemList)array[3];
                        tradeableItemList5 = (TradeableItemList)array[4];
                        tradeableItemList6 = (TradeableItemList)array[5];
                    }
                }
            }
            TradeOfferResponse tradeOfferResponse;
            Empire selectedEmpire;
            switch (conversationOption_0.Type)
            {
                case DialogPartType.Exit:
                    method_294();
                    break;
                case DialogPartType.INFO_UNMETEMPIRE:
                    if (initiator.StateMoney >= conversationOption_0.Cost)
                    {
                        DiplomaticRelation diplomaticRelation4 = initiator.ObtainDiplomaticRelation(empire2);
                        diplomaticRelation4.Type = DiplomaticRelationType.None;
                        diplomaticRelation4 = empire2.ObtainDiplomaticRelation(initiator);
                        diplomaticRelation4.Type = DiplomaticRelationType.None;
                        initiator.StateMoney -= conversationOption_0.Cost;
                        empire.StateMoney += conversationOption_0.Cost;
                        empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                        method_195(ctlEmpireDiplomaticRelationList.SelectedEmpire);
                        pnlDiplomacyTalk.BringToFront();
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                    }
                    break;
                case DialogPartType.INFO_EXPLORATION:
                    if (initiator.StateMoney >= conversationOption_0.Cost)
                    {
                        initiator.SystemVisibility[habitat.SystemIndex].Status = SystemVisibilityStatus.Explored;
                        initiator.StateMoney -= conversationOption_0.Cost;
                        empire.StateMoney += conversationOption_0.Cost;
                        empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                    }
                    break;
                case DialogPartType.INFO_INDEPENDENTCOLONY:
                    if (initiator.StateMoney >= conversationOption_0.Cost)
                    {
                        initiator.SystemVisibility[habitat.SystemIndex].Status = SystemVisibilityStatus.Explored;
                        initiator.StateMoney -= conversationOption_0.Cost;
                        empire.StateMoney += conversationOption_0.Cost;
                        empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                    }
                    break;
                case DialogPartType.INFO_RUINS:
                    if (initiator.StateMoney >= conversationOption_0.Cost)
                    {
                        initiator.SystemVisibility[habitat.SystemIndex].Status = SystemVisibilityStatus.Explored;
                        initiator.StateMoney -= conversationOption_0.Cost;
                        empire.StateMoney += conversationOption_0.Cost;
                        empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                        Point location2 = new Point((int)habitat.Xpos, (int)habitat.Ypos);
                        initiator.AddLocationHint(location2);
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                    }
                    break;
                case DialogPartType.INFO_DEBRISFIELD:
                    if (initiator.StateMoney >= conversationOption_0.Cost)
                    {
                        if (!initiator.KnownGalaxyLocations.Contains(galaxyLocation))
                        {
                            initiator.KnownGalaxyLocations.Add(galaxyLocation);
                        }
                        initiator.StateMoney -= conversationOption_0.Cost;
                        empire.StateMoney += conversationOption_0.Cost;
                        empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                        Point location4 = new Point((int)((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0), (int)((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0));
                        initiator.AddLocationHint(location4);
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                    }
                    break;
                case DialogPartType.INFO_PLANETDESTROYER:
                    if (initiator.StateMoney >= conversationOption_0.Cost)
                    {
                        if (!initiator.KnownGalaxyLocations.Contains(galaxyLocation))
                        {
                            initiator.KnownGalaxyLocations.Add(galaxyLocation);
                        }
                        initiator.StateMoney -= conversationOption_0.Cost;
                        empire.StateMoney += conversationOption_0.Cost;
                        empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                        Point location = new Point((int)((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0), (int)((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0));
                        initiator.AddLocationHint(location);
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                    }
                    break;
                case DialogPartType.INFO_RESTRICTEDAREA:
                    if (initiator.StateMoney >= conversationOption_0.Cost)
                    {
                        if (!initiator.KnownGalaxyLocations.Contains(galaxyLocation))
                        {
                            initiator.KnownGalaxyLocations.Add(galaxyLocation);
                        }
                        initiator.StateMoney -= conversationOption_0.Cost;
                        empire.StateMoney += conversationOption_0.Cost;
                        empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                        Point location3 = new Point((int)((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0), (int)((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0));
                        initiator.AddLocationHint(location3);
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                    }
                    break;
                case DialogPartType.PIRATE_ATTACKCOMMENCE:
                    if (initiator.StateMoney >= conversationOption_0.Cost)
                    {
                        num2 = _Game.Galaxy.CurrentStarDate + Galaxy.PirateEmpireAttackExpiryDateLength;
                        empire.PirateMissions.Add(new EmpireActivity(empire2, initiator, num2, EmpireActivityType.Attack));
                        initiator.StateMoney -= conversationOption_0.Cost;
                        empire.StateMoney += conversationOption_0.Cost;
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                    }
                    break;
                case DialogPartType.PIRATE_ALLIANCEACCEPT:
                    _Game.Galaxy.SetupPirateAlliance(initiator, empire, conversationOption_0.Cost, _Game.Galaxy.CurrentStarDate);
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.PIRATE_ALLIANCEACCEPTRESPONSE, empire);
                    break;
                case DialogPartType.PIRATE_ALLIANCEREJECT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.PIRATE_ALLIANCEREJECTRESPONSE, empire);
                    break;
                case DialogPartType.OFFER_FREETRADE:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    if (empire.Reclusive)
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.FREETRADE_REJECT, empire);
                        break;
                    }
                    switch (method_232(empire, initiator))
                    {
                        default:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.FREETRADE_REJECT, empire);
                            break;
                        case DiplomaticRelationType.FreeTradeAgreement:
                        case DiplomaticRelationType.MutualDefensePact:
                        case DiplomaticRelationType.Protectorate:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.FREETRADE_ACCEPT, empire);
                            initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire), DiplomaticRelationType.FreeTradeAgreement);
                            method_235(initiator, empire);
                            method_235(empire, initiator);
                            diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                            break;
                    }
                    break;
                case DialogPartType.OFFER_PROTECTORATE:
                    {
                        if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                        {
                            _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                        }
                        if (empire.Reclusive)
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.PROTECTORATE_REJECT, empire);
                            break;
                        }
                        DiplomaticRelationType diplomaticRelationType = method_232(empire, initiator);
                        if (diplomaticRelationType == DiplomaticRelationType.Protectorate)
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.PROTECTORATE_ACCEPT, empire);
                            initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire), DiplomaticRelationType.Protectorate);
                            method_235(initiator, empire);
                            method_235(empire, initiator);
                            diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.PROTECTORATE_REJECT, empire);
                        }
                        break;
                    }
                case DialogPartType.OFFER_MUTUALDEFENSE:
                    {
                        if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                        {
                            _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                        }
                        if (empire.Reclusive)
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.MUTUALDEFENSE_REJECT, empire);
                            break;
                        }
                        DiplomaticRelationType diplomaticRelationType = method_232(empire, initiator);
                        if (diplomaticRelationType == DiplomaticRelationType.MutualDefensePact)
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.MUTUALDEFENSE_ACCEPT, empire);
                            initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire), DiplomaticRelationType.MutualDefensePact);
                            method_235(initiator, empire);
                            method_235(empire, initiator);
                            diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.MUTUALDEFENSE_REJECT, empire);
                        }
                        break;
                    }
                case DialogPartType.OFFER_DEAL:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.OFFER_DEAL_RESPONSE, empire);
                    break;
                case DialogPartType.OFFER_DEAL_TERRITORYMAP:
                    {
                        if (!(conversationOption_0.RelatedInfo is TradeableItem))
                        {
                            break;
                        }
                        TradeableItem tradeableItem2 = (TradeableItem)conversationOption_0.RelatedInfo;
                        if (empire.DetermineAcceptTerritoryMapTrade(tradeableItem2.Value, _Game.PlayerEmpire))
                        {
                            HabitatList habitatList = initiator.DetermineEmpireSystems(initiator);
                            HabitatList habitatList2 = empire.DetermineEmpireSystems(empire);
                            for (int i = 0; i < habitatList.Count; i++)
                            {
                                Habitat systemStar = habitatList[i];
                                if (!empire.CheckSystemExplored(systemStar))
                                {
                                    empire.SetSystemVisibility(systemStar, SystemVisibilityStatus.Explored);
                                }
                            }
                            for (int j = 0; j < habitatList2.Count; j++)
                            {
                                Habitat systemStar2 = habitatList2[j];
                                if (!initiator.CheckSystemExplored(systemStar2))
                                {
                                    initiator.SetSystemVisibility(systemStar2, SystemVisibilityStatus.Explored);
                                }
                            }
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_ACCEPT, empire);
                            conversationOption_0.ReroutedType = DialogPartType.OFFER_DEAL_TERRITORYMAP;
                            _Galaxy_RefreshView(this, new RefreshViewEventArgs(0.0, 0.0, null, onlyGalaxyBackdrops: true));
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_REJECT, empire);
                            conversationOption_0.ReroutedType = DialogPartType.OFFER_DEAL_TERRITORYMAP;
                        }
                        break;
                    }
                case DialogPartType.OFFER_DEAL_GALAXYMAP:
                    if (conversationOption_0.RelatedInfo is TradeableItem)
                    {
                        TradeableItem tradeableItem = (TradeableItem)conversationOption_0.RelatedInfo;
                        if (empire.DetermineAcceptGalaxyMapTrade(tradeableItem.Value, _Game.PlayerEmpire))
                        {
                            _Game.Galaxy.MergeGalaxyMap(initiator, empire);
                            _Game.Galaxy.MergeGalaxyMap(empire, initiator);
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_ACCEPT, empire);
                            conversationOption_0.ReroutedType = DialogPartType.OFFER_DEAL_GALAXYMAP;
                            _Galaxy_RefreshView(this, new RefreshViewEventArgs(0.0, 0.0, null, onlyGalaxyBackdrops: true));
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_REJECT, empire);
                            conversationOption_0.ReroutedType = DialogPartType.OFFER_DEAL_GALAXYMAP;
                        }
                    }
                    break;
                case DialogPartType.OFFER_DEAL_COMPONENT:
                    {
                        if (!(conversationOption_0.RelatedInfo is TradeableItem))
                        {
                            break;
                        }
                        TradeableItem tradeableItem3 = (TradeableItem)conversationOption_0.RelatedInfo;
                        bool flag = false;
                        if ((double)tradeableItem3.Value <= empire.StateMoney)
                        {
                            double num5 = empire.StateMoney * (0.25 + Galaxy.Rnd.NextDouble() * 0.25);
                            if (empire.StateMoney >= num5 && tradeableItem3.Item is ResearchNode)
                            {
                                ResearchNode researchNode = (ResearchNode)tradeableItem3.Item;
                                ResearchNode equivalent = empire.Research.TechTree.GetEquivalent(researchNode);
                                if (equivalent != null && !equivalent.IsResearched)
                                {
                                    empire.DoResearchBreakthrough(equivalent, selfResearched: false, blockMessages: true, suppressUpdate: false);
                                    empire.StateMoney -= tradeableItem3.Value;
                                    initiator.StateMoney += tradeableItem3.Value;
                                    initiator.PirateEconomy.PerformIncome(tradeableItem3.Value, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                                    flag = true;
                                }
                            }
                        }
                        if (flag)
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_ACCEPT, empire);
                            conversationOption_0.ReroutedType = DialogPartType.OFFER_DEAL_COMPONENT;
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_REJECT, empire);
                            conversationOption_0.ReroutedType = DialogPartType.OFFER_DEAL_COMPONENT;
                        }
                        break;
                    }
                case DialogPartType.DEAL_BEGIN:
                    if (initiator == _Game.PlayerEmpire)
                    {
                        method_302(empire, tradeableItemList2, tradeableItemList, tradeableItemList4, tradeableItemList3, tradeableItemList6, tradeableItemList5);
                    }
                    else
                    {
                        method_302(empire, tradeableItemList, tradeableItemList2, tradeableItemList3, tradeableItemList4, tradeableItemList5, tradeableItemList6);
                    }
                    break;
                case DialogPartType.DEAL_OFFER:
                    tradeableItemList = ctlDiplomacyTradeUs.SelectedTradeItems;
                    tradeableItemList2 = ctlDiplomacyTradeThem.SelectedTradeItems;
                    tradeOfferResponse = empire.EvaluateTradeOffer(initiator, tradeableItemList, tradeableItemList2, disallowCriticalItems: true);
                    if (tradeableItemList.CheckIdentical(tradeableItemList_1) && tradeableItemList2.CheckIdentical(tradeableItemList_0))
                    {
                        tradeOfferResponse = TradeOfferResponse.Accept;
                        //TradeOfferResponse tradeOfferResponse2 = TradeOfferResponse.Accept;
                    }
                    else
                    {
                        switch (tradeOfferResponse)
                        {
                            case TradeOfferResponse.RefuseUnfair:
                            case TradeOfferResponse.Refuse:
                                if (empire != _Game.PlayerEmpire)
                                {
                                    foreach (TradeableItem item in tradeableItemList)
                                    {
                                        if (item.Type == TradeableItemType.ThreatenWar)
                                        {
                                            if (initiator.PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                                            {
                                                initiator.DeclareWar(empire);
                                                diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                                            }
                                        }
                                        else if (item.Type == TradeableItemType.ThreatenTradeSanctions && initiator.PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                                        {
                                            DiplomaticRelation currentDiplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                                            initiator.ChangeDiplomaticRelation(currentDiplomaticRelation, DiplomaticRelationType.TradeSanctions);
                                            initiator.SendMessageToEmpire(empire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.TradeSanctions, TextResolver.GetText("We terminate all trade with you effective immediately!"));
                                            diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                                        }
                                    }
                                }
                                goto IL_10b3;
                            case TradeOfferResponse.Accept:
                            case TradeOfferResponse.AcceptUnfair:
                                break;
                            default:
                                goto IL_10b3;
                        }
                    }
                    foreach (TradeableItem item2 in tradeableItemList)
                    {
                        _Game.Galaxy.GiveTradeableItem(initiator, empire, item2, tradeableItemList2);
                    }
                    foreach (TradeableItem item3 in tradeableItemList2)
                    {
                        _Game.Galaxy.GiveTradeableItem(empire, initiator, item3, tradeableItemList);
                    }
                    goto IL_10b3;
                case DialogPartType.DEAL_ACCEPT:
                case DialogPartType.DEAL_ACCEPTCOMPLAIN:
                    if (conversationOption_0.RelatedInfo is object[] && tradeableItemList2 != null && tradeableItemList2.Count > 0)
                    {
                        foreach (TradeableItem item4 in tradeableItemList)
                        {
                            _Game.Galaxy.GiveTradeableItem(empire, initiator, item4, tradeableItemList2);
                        }
                        foreach (TradeableItem item5 in tradeableItemList2)
                        {
                            _Game.Galaxy.GiveTradeableItem(initiator, empire, item5, tradeableItemList);
                        }
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_ACCEPT_RESPONSE, empire);
                    }
                    else
                    {
                        if (!(conversationOption_0.RelatedInfo is TradeableItem))
                        {
                            break;
                        }
                        TradeableItem tradeableItem4 = (TradeableItem)conversationOption_0.RelatedInfo;
                        if (tradeableItem4.Type == TradeableItemType.TerritoryMap)
                        {
                            HabitatList habitatList3 = empire.DetermineEmpireSystems(empire);
                            HabitatList habitatList4 = initiator.DetermineEmpireSystems(initiator);
                            for (int k = 0; k < habitatList3.Count; k++)
                            {
                                Habitat systemStar3 = habitatList3[k];
                                if (!initiator.CheckSystemExplored(systemStar3))
                                {
                                    initiator.SetSystemVisibility(systemStar3, SystemVisibilityStatus.Explored);
                                }
                            }
                            for (int l = 0; l < habitatList4.Count; l++)
                            {
                                Habitat systemStar4 = habitatList4[l];
                                if (!empire.CheckSystemExplored(systemStar4))
                                {
                                    empire.SetSystemVisibility(systemStar4, SystemVisibilityStatus.Explored);
                                }
                            }
                            conversationOption_0 = method_234(conversationOption_0, method_236(empire, initiator), empire);
                        }
                        else if (tradeableItem4.Type == TradeableItemType.GalaxyMap)
                        {
                            _Game.Galaxy.MergeGalaxyMap(initiator, empire);
                            _Game.Galaxy.MergeGalaxyMap(empire, initiator);
                            conversationOption_0 = method_234(conversationOption_0, method_236(empire, initiator), empire);
                        }
                        else
                        {
                            if (tradeableItem4.Type != TradeableItemType.ResearchProject)
                            {
                                break;
                            }
                            if ((double)tradeableItem4.Value <= initiator.StateMoney)
                            {
                                if (tradeableItem4.Item is ResearchNode)
                                {
                                    ResearchNode researchNode2 = (ResearchNode)tradeableItem4.Item;
                                    ResearchNode equivalent2 = initiator.Research.TechTree.GetEquivalent(researchNode2);
                                    if (equivalent2 != null && !equivalent2.IsResearched)
                                    {
                                        initiator.DoResearchBreakthrough(equivalent2, selfResearched: false, blockMessages: true, suppressUpdate: false);
                                        initiator.StateMoney -= tradeableItem4.Value;
                                        empire.StateMoney += tradeableItem4.Value;
                                        empire.PirateEconomy.PerformIncome(tradeableItem4.Value, PirateIncomeType.SellInfo, _Game.Galaxy.CurrentStarDate);
                                    }
                                }
                                conversationOption_0 = method_234(conversationOption_0, method_236(empire, initiator), empire);
                            }
                            else
                            {
                                conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                            }
                        }
                    }
                    break;
                case DialogPartType.DEAL_IMPROVE:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_BEGIN, empire);
                    tradeableItemList_0 = tradeableItemList;
                    tradeableItemList_2 = tradeableItemList3;
                    tradeableItemList_4 = tradeableItemList5;
                    tradeableItemList_1 = tradeableItemList2;
                    tradeableItemList_3 = tradeableItemList4;
                    tradeableItemList_5 = tradeableItemList6;
                    method_302(empire, tradeableItemList, tradeableItemList2, tradeableItemList3, tradeableItemList4, tradeableItemList5, tradeableItemList6);
                    break;
                case DialogPartType.DEAL_REJECT:
                case DialogPartType.DEAL_REJECTCOMPLAIN:
                    if (conversationOption_0.RelatedInfo is object[] && tradeableItemList2 != null && tradeableItemList2.Count > 0 && empire != _Game.PlayerEmpire)
                    {
                        foreach (TradeableItem item6 in tradeableItemList)
                        {
                            if (item6.Type == TradeableItemType.ThreatenWar)
                            {
                                if (initiator.PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                                {
                                    initiator.DeclareWar(empire);
                                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                                }
                            }
                            else if (item6.Type == TradeableItemType.ThreatenTradeSanctions && initiator.PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                            {
                                diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                                initiator.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.TradeSanctions);
                                initiator.SendMessageToEmpire(empire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.TradeSanctions, TextResolver.GetText("We terminate all trade with you effective immediately!"));
                                diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                            }
                        }
                    }
                    conversationOption_0 = ((conversationOption_0.Type != DialogPartType.DEAL_REJECTCOMPLAIN) ? method_234(conversationOption_0, DialogPartType.DEAL_REJECT_RESPONSE, empire) : method_234(conversationOption_0, DialogPartType.DEAL_REJECTDEMAND_RESPONSE, empire));
                    break;
                case DialogPartType.MUTUALDEFENSE_ACCEPT:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_ACCEPTRESPONSE, empire);
                    empire.ChangeDiplomaticRelation(empire.ObtainDiplomaticRelation(initiator), DiplomaticRelationType.MutualDefensePact);
                    method_235(empire, initiator);
                    method_235(initiator, empire);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    break;
                case DialogPartType.MUTUALDEFENSE_REJECT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_REJECTRESPONSE, empire);
                    break;
                case DialogPartType.MUTUALDEFENSE_HONORREQUESTHELP:
                    {
                        empireEvaluation = empire.ObtainEmpireEvaluation(initiator);
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw + 30.0;
                        initiator.CivilityRating += 8.0;
                        Empire empire5 = empire_2;
                        if (conversationOption_0.RelatedInfo != null && conversationOption_0.RelatedInfo is Empire)
                        {
                            empire5 = (Empire)conversationOption_0.RelatedInfo;
                        }
                        initiator.DeclareWar(empire5, null, lockedWar: false, blockFlowonEffects: true);
                        diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire5);
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.MUTUALDEFENSE_HONORREQUESTHELP_RESPONSE, empire);
                        break;
                    }
                case DialogPartType.MUTUALDEFENSE_DECLINEREQUESTHELP:
                    empireEvaluation = empire.ObtainEmpireEvaluation(initiator);
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 30.0;
                    initiator.CivilityRating -= 6.0;
                    empire.ChangeDiplomaticRelation(empire.ObtainDiplomaticRelation(initiator), DiplomaticRelationType.None, blockFlowonEffects: true);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.MUTUALDEFENSE_DECLINEREQUESTHELP_RESPONSE, empire);
                    break;
                case DialogPartType.PROTECTORATE_ACCEPT:
                    {
                        if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                        {
                            _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                        }
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_ACCEPTRESPONSE, empire);
                        Empire empire_;
                        Empire empire3 = method_233(initiator, empire, out empire_);
                        empire3.ChangeDiplomaticRelation(empire3.ObtainDiplomaticRelation(empire_), DiplomaticRelationType.Protectorate);
                        method_235(empire, initiator);
                        method_235(initiator, empire);
                        diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                        break;
                    }
                case DialogPartType.PROTECTORATE_REJECT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_REJECTRESPONSE, empire);
                    break;
                case DialogPartType.FREETRADE_ACCEPT:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_ACCEPTRESPONSE, empire);
                    empire.ChangeDiplomaticRelation(empire.ObtainDiplomaticRelation(initiator), DiplomaticRelationType.FreeTradeAgreement);
                    method_235(empire, initiator);
                    method_235(initiator, empire);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    break;
                case DialogPartType.FREETRADE_REJECT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_REJECTRESPONSE, empire);
                    break;
                case DialogPartType.CANCELTREATY:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    switch (num)
                    {
                        case -1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.CANCELTREATY_RESPONSE_ANGRY, empire);
                            break;
                        case 0:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.CANCELTREATY_RESPONSE_NEUTRAL, empire);
                            break;
                        case 1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.CANCELTREATY_RESPONSE_FRIENDLY, empire);
                            break;
                    }
                    initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire), DiplomaticRelationType.None);
                    method_235(initiator, empire);
                    method_235(empire, initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    break;
                case DialogPartType.TRADESANCTIONS_IMPOSE:
                    switch (num)
                    {
                        case -1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.TRADESANCTIONS_IMPOSE_RESPONSE_ANGRY, empire);
                            break;
                        case 0:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.TRADESANCTIONS_IMPOSE_RESPONSE_NEUTRAL, empire);
                            break;
                        case 1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.TRADESANCTIONS_IMPOSE_RESPONSE_SURPRISED, empire);
                            break;
                    }
                    initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire), DiplomaticRelationType.TradeSanctions);
                    method_235(initiator, empire);
                    method_235(empire, initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    break;
                case DialogPartType.TRADESANCTIONS_LIFT:
                    switch (num)
                    {
                        case -1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.TRADESANCTIONS_LIFT_RESPONSE, empire);
                            break;
                        case 0:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.TRADESANCTIONS_LIFT_RESPONSE, empire);
                            break;
                        case 1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.TRADESANCTIONS_LIFT_RESPONSE, empire);
                            break;
                    }
                    initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire), DiplomaticRelationType.None);
                    method_235(initiator, empire);
                    method_235(empire, initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    initiator.CancelBlockades(empire);
                    empire.CancelBlockades(initiator);
                    break;
                case DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER_ACCEPT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_ACCEPTRESPONSE, empire);
                    initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire2), DiplomaticRelationType.None);
                    method_235(initiator, empire2);
                    method_235(empire2, initiator);
                    break;
                case DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER_REJECT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_REJECTRESPONSE, empire);
                    break;
                case DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT_ACCEPT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_ACCEPTRESPONSE, empire);
                    initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire2), DiplomaticRelationType.TradeSanctions);
                    empireEvaluation = empire.ObtainEmpireEvaluation(initiator);
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw + 5.0;
                    method_235(initiator, empire2);
                    method_235(empire2, initiator);
                    break;
                case DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT_REJECT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_REJECTRESPONSE, empire);
                    break;
                case DialogPartType.WAR_DECLARE:
                    switch (num)
                    {
                        case -1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WAR_DECLARE_RESPONSE_EAGER, empire);
                            break;
                        case 0:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WAR_DECLARE_RESPONSE_NEUTRAL, empire);
                            break;
                        case 1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WAR_DECLARE_RESPONSE_SURPRISED, empire);
                            break;
                    }
                    initiator.DeclareWar(empire);
                    method_235(initiator, empire);
                    method_235(empire, initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    break;
                case DialogPartType.WAR_DECLARE_REQUESTJOINT_ACCEPT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_ACCEPTRESPONSE, empire);
                    initiator.DeclareWar(empire2);
                    empireEvaluation = empire.ObtainEmpireEvaluation(initiator);
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw + 10.0;
                    method_235(initiator, empire2);
                    method_235(empire2, initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire2);
                    break;
                case DialogPartType.WAR_DECLARE_REQUESTJOINT_REJECT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_REJECTRESPONSE, empire);
                    break;
                case DialogPartType.WAR_END:
                    {
                        WarEndReason endReason2 = WarEndReason.Undefined;
                        if (empire.ConsiderEndWar(initiator, out endReason2))
                        {
                            double winningRatio2 = 0.0;
                            int loserRawDamageBuiltObject2 = 0;
                            int loserRawDamageColony2 = 0;
                            int winnerRawDamageBuiltObject2 = 0;
                            int winnerRawDamageColony2 = 0;
                            Empire loser2 = null;
                            DiplomaticRelation diplomaticRelation3 = empire.ObtainDiplomaticRelation(initiator);
                            Empire empire6 = empire.DetermineVictorInWar(diplomaticRelation3, out winningRatio2, out loser2, out loserRawDamageBuiltObject2, out loserRawDamageColony2, out winnerRawDamageBuiltObject2, out winnerRawDamageColony2);
                            if (empire6 == empire && empire.DetermineWhetherWantToOfferSubjugation(empire) && empire.DetermineSubjugationOfLoserInWar(empire6, loser2, winningRatio2, empire6.MilitaryPotency, loser2.MilitaryPotency))
                            {
                                conversationOption_0 = method_234(conversationOption_0, DialogPartType.WAR_END_SUBJUGATIONDEMAND, empire);
                                break;
                            }
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WAR_END_ACCEPT, empire);
                            diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                            initiator.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation);
                            diplomaticRelation.Type = DiplomaticRelationType.None;
                            diplomaticRelation.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                            diplomaticRelation2 = empire.ObtainDiplomaticRelation(initiator);
                            diplomaticRelation2.Type = DiplomaticRelationType.None;
                            diplomaticRelation2.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                            initiator.ProcessEndOfWarWithEmpire(empire);
                            empire.ProcessEndOfWarWithEmpire(initiator);
                            diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WAR_END_REJECT, empire);
                        }
                        break;
                    }
                case DialogPartType.WAR_END_SUBJUGATIONDEMAND:
                    diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        double winningRatio;
                        Empire loser;
                        int loserRawDamageBuiltObject;
                        int loserRawDamageColony;
                        int winnerRawDamageBuiltObject;
                        int winnerRawDamageColony;
                        Empire empire4 = initiator.DetermineVictorInWar(diplomaticRelation, out winningRatio, out loser, out loserRawDamageBuiltObject, out loserRawDamageColony, out winnerRawDamageBuiltObject, out winnerRawDamageColony);
                        if (empire4 == initiator)
                        {
                            if (initiator.DetermineSubjugationOfLoserInWar(empire4, loser, winningRatio, empire4.MilitaryPotency, loser.MilitaryPotency))
                            {
                                conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATIONDEMAND_ACCEPT, empire);
                                initiator.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation);
                                diplomaticRelation.Type = DiplomaticRelationType.SubjugatedDominion;
                                diplomaticRelation.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                                diplomaticRelation.Initiator = initiator;
                                diplomaticRelation2 = empire.ObtainDiplomaticRelation(initiator);
                                diplomaticRelation2.Type = DiplomaticRelationType.SubjugatedDominion;
                                diplomaticRelation2.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                                diplomaticRelation2.Initiator = initiator;
                                initiator.ProcessEndOfWarWithEmpire(empire);
                                empire.ProcessEndOfWarWithEmpire(initiator);
                                diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                                num2 = long.MaxValue;
                                num3 = initiator.EmpiresViewable.IndexOf(empire);
                                if (num3 >= 0)
                                {
                                    initiator.EmpiresViewableExpiry[num3] = num2;
                                    break;
                                }
                                initiator.EmpiresViewable.Add(empire);
                                initiator.EmpiresViewableExpiry.Add(num2);
                            }
                            else
                            {
                                conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATIONDEMAND_REJECT, empire);
                            }
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATIONDEMAND_REJECT, empire);
                        }
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATIONDEMAND_REJECT, empire);
                    }
                    break;
                case DialogPartType.WAR_END_SUBJUGATIONOFFER:
                    {
                        DiplomaticRelationType diplomaticRelationType = method_232(initiator, empire);
                        if (diplomaticRelationType == DiplomaticRelationType.War)
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATIONOFFER_REJECT, empire);
                            break;
                        }
                        WarEndReason endReason = WarEndReason.Undefined;
                        if (empire.ConsiderEndWar(initiator, out endReason, otherEmpireOfferingToBeSubjugated: true))
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATIONOFFER_ACCEPT, empire);
                            diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                            initiator.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation, empire);
                            diplomaticRelation.Type = DiplomaticRelationType.SubjugatedDominion;
                            diplomaticRelation.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                            diplomaticRelation.Initiator = empire;
                            diplomaticRelation2 = empire.ObtainDiplomaticRelation(initiator);
                            diplomaticRelation2.Initiator = empire;
                            diplomaticRelation2.Type = DiplomaticRelationType.SubjugatedDominion;
                            diplomaticRelation2.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                            initiator.ProcessEndOfWarWithEmpire(empire);
                            empire.ProcessEndOfWarWithEmpire(initiator);
                            diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATIONOFFER_REJECT, empire);
                        }
                        break;
                    }
                case DialogPartType.WAR_END_ACCEPT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.WAR_END_ACCEPT_RESPONSE, empire);
                    diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                    initiator.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation);
                    diplomaticRelation.Type = DiplomaticRelationType.None;
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                    diplomaticRelation2 = empire.ObtainDiplomaticRelation(initiator);
                    diplomaticRelation2.Type = DiplomaticRelationType.None;
                    diplomaticRelation2.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                    initiator.ProcessEndOfWarWithEmpire(empire);
                    empire.ProcessEndOfWarWithEmpire(initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    break;
                case DialogPartType.WAR_END_REJECT:
                    method_294();
                    break;
                case DialogPartType.WAR_END_REQUESTOTHER_ACCEPT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_ACCEPTRESPONSE, empire);
                    diplomaticRelation = initiator.ObtainDiplomaticRelation(empire2);
                    initiator.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation);
                    diplomaticRelation.Type = DiplomaticRelationType.None;
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                    diplomaticRelation2 = empire2.ObtainDiplomaticRelation(initiator);
                    diplomaticRelation2.Type = DiplomaticRelationType.None;
                    diplomaticRelation2.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                    initiator.ProcessEndOfWarWithEmpire(empire2);
                    empire2.ProcessEndOfWarWithEmpire(initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire2);
                    break;
                case DialogPartType.SUBJUGATIONDEMAND_ACCEPT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATIONDEMAND_ACCEPT_RESPONSE, empire);
                    diplomaticRelation = empire.ObtainDiplomaticRelation(initiator);
                    empire.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation, empire);
                    diplomaticRelation.Type = DiplomaticRelationType.SubjugatedDominion;
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                    diplomaticRelation.Initiator = empire;
                    diplomaticRelation2 = initiator.ObtainDiplomaticRelation(empire);
                    diplomaticRelation2.Type = DiplomaticRelationType.SubjugatedDominion;
                    diplomaticRelation2.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                    diplomaticRelation2.Initiator = empire;
                    initiator.ProcessEndOfWarWithEmpire(empire);
                    empire.ProcessEndOfWarWithEmpire(initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    num2 = long.MaxValue;
                    num3 = empire.EmpiresViewable.IndexOf(initiator);
                    if (num3 >= 0)
                    {
                        empire.EmpiresViewableExpiry[num3] = num2;
                        break;
                    }
                    empire.EmpiresViewable.Add(initiator);
                    empire.EmpiresViewableExpiry.Add(num2);
                    break;
                case DialogPartType.SUBJUGATIONDEMAND_REJECT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_REJECTRESPONSE, empire);
                    break;
                case DialogPartType.SUBJUGATIONOFFER_ACCEPT:
                    diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                    initiator.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation, initiator);
                    diplomaticRelation.Type = DiplomaticRelationType.SubjugatedDominion;
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                    diplomaticRelation.Initiator = initiator;
                    diplomaticRelation2 = empire.ObtainDiplomaticRelation(initiator);
                    diplomaticRelation2.Type = DiplomaticRelationType.SubjugatedDominion;
                    diplomaticRelation2.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                    diplomaticRelation2.Initiator = initiator;
                    initiator.ProcessEndOfWarWithEmpire(empire);
                    empire.ProcessEndOfWarWithEmpire(initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    num2 = long.MaxValue;
                    num3 = initiator.EmpiresViewable.IndexOf(empire);
                    if (num3 >= 0)
                    {
                        initiator.EmpiresViewableExpiry[num3] = num2;
                        break;
                    }
                    initiator.EmpiresViewable.Add(empire);
                    initiator.EmpiresViewableExpiry.Add(num2);
                    break;
                case DialogPartType.SUBJUGATION_REQUESTRELEASE:
                    if (method_232(empire, initiator) == DiplomaticRelationType.None && empire.DominantRace.AggressionLevel < 130 + Galaxy.Rnd.Next(0, 80))
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATION_RELEASE, empire);
                        diplomaticRelation = empire.ObtainDiplomaticRelation(initiator);
                        diplomaticRelation.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                        empire.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                        method_235(empire, initiator);
                        method_235(initiator, empire);
                        diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATION_REFUSERELEASE, empire);
                    }
                    break;
                case DialogPartType.SUBJUGATION_RELEASE:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.SUBJUGATION_RELEASE_RESPONSE, empire);
                    initiator.ChangeDiplomaticRelation(initiator.ObtainDiplomaticRelation(empire), DiplomaticRelationType.None);
                    method_235(initiator, empire);
                    method_235(empire, initiator);
                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
                    break;
                case DialogPartType.SUBJUGATION_REFUSERELEASE:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.TREATY_REJECTRESPONSE, empire);
                    break;
                case DialogPartType.GIFT_GIVE:
                    {
                        double num7 = empire.ValueMoneyGiftFromEmpire(initiator, conversationOption_0.Cost);
                        EmpireEvaluation empireEvaluation2 = empire.ObtainEmpireEvaluation(initiator);
                        empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw + num7;
                        initiator.CivilityRating += num7 * 0.1;
                        initiator.StateMoney -= conversationOption_0.Cost;
                        initiator.PirateEconomy.PerformExpense(conversationOption_0.Cost, PirateExpenseType.Undefined, _Game.Galaxy.CurrentStarDate);
                        empire.StateMoney += conversationOption_0.Cost;
                        empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.Undefined, _Game.Galaxy.CurrentStarDate);
                        diplomaticRelation2 = initiator.ObtainDiplomaticRelation(empire);
                        diplomaticRelation2.LastGiftDate = _Game.Galaxy.CurrentStarDate;
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.GIFT_THANKS, empire);
                        break;
                    }
                case DialogPartType.GIFT_THANKS:
                    conversationOption_0 = method_234(conversationOption_0, method_236(empire, initiator), empire);
                    break;
                case DialogPartType.WARNING_INTELLIGENCEMISSIONS:
                    switch (num)
                    {
                        case -1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_INTELLIGENCEMISSIONS_RESPONSE_ANGRY, empire);
                            break;
                        case 0:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_INTELLIGENCEMISSIONS_RESPONSE_NEUTRAL, empire);
                            break;
                        case 1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_INTELLIGENCEMISSIONS_RESPONSE_FRIENDLY, empire);
                            break;
                    }
                    break;
                case DialogPartType.WARNING_ATTACKS:
                    switch (num)
                    {
                        case -1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_ATTACKS_RESPONSE_ANGRY, empire);
                            break;
                        case 0:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_ATTACKS_RESPONSE_NEUTRAL, empire);
                            break;
                        case 1:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_ATTACKS_RESPONSE_FRIENDLY, empire);
                            break;
                    }
                    break;
                case DialogPartType.WARNING_REMOVEFORCESSYSTEM:
                    {
                        //int num6 = 0;
                        habitat = _Game.PlayerEmpire.DetermineTopThreatenedSystem(empire);
                        if (habitat != null)
                        {
                            switch (empire.RemoveMilitaryForcesFromSystem(habitat, initiator))
                            {
                                case 1:
                                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_REMOVEFORCESSYSTEM_RESPONSE_COMPLY, empire);
                                    conversationOption_0.RelatedInfo = habitat;
                                    break;
                                case 0:
                                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_REMOVEFORCESSYSTEM_RESPONSE_NOFORCESPRESENT, empire);
                                    break;
                                case -1:
                                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_REMOVEFORCESSYSTEM_RESPONSE_REFUSE, empire);
                                    conversationOption_0.RelatedInfo = habitat;
                                    break;
                            }
                        }
                        else
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.WARNING_REMOVEFORCESSYSTEM_RESPONSE_NOFORCESPRESENT, empire);
                        }
                        break;
                    }
                case DialogPartType.GOTO_TARGET:
                    if (habitat != null)
                    {
                        method_157(habitat);
                        method_4(50.0);
                        method_294();
                    }
                    break;
                case DialogPartType.HISTORY_OFFER_LOCATIONHINT_ACCEPT:
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.HISTORY_LOCATIONHINT, empire);
                    break;
                case DialogPartType.HISTORY_OFFER_LOCATIONHINT_REJECT:
                    method_294();
                    break;
                case DialogPartType.HISTORY_OFFER_STORYCLUE_ACCEPT:
                    {
                        string text6 = _Game.Galaxy.GenerateBuiltObjectStoryClue(null);
                        string text7 = string.Format(TextResolver.GetText("Reveal Historical Secret"), empire.DominantRace.Name);
                        string text8 = string.Format(TextResolver.GetText("Reveal Historical Secret Details"), empire.DominantRace.Name);
                        text8 += "\n\n";
                        text8 += text6;
                        method_571(text7, text8);
                        EmpireMessage empireMessage2 = new EmpireMessage(_Game.PlayerEmpire, EmpireMessageType.GalacticHistory, null);
                        string text10 = (empireMessage2.Description = text8.Replace("\n", " "));
                        empireMessage2.Title = text7;
                        empireMessage2.SupressPopup = true;
                        _Game.PlayerEmpire.SendMessageToEmpire(empireMessage2, _Game.PlayerEmpire);
                        method_294();
                        break;
                    }
                case DialogPartType.HISTORY_OFFER_STORYCLUE_REJECT:
                    method_294();
                    break;
                case DialogPartType.HISTORY_OFFER_STORYMESSAGE_ACCEPT:
                    {
                        string text = _Game.Galaxy.GenerateMajorStoryItem(_Game.Galaxy.StoryReturnOfTheShakturiEventLevel);
                        string text2 = string.Format(TextResolver.GetText("RACE Share Important Warning"), empire.DominantRace.Name);
                        switch (_Game.Galaxy.StoryReturnOfTheShakturiEventLevel)
                        {
                            case 2:
                                text2 = TextResolver.GetText("Ancient Guardians Reveal All");
                                break;
                            case 3:
                                text2 = TextResolver.GetText("The Shakturi have Returned for Revenge!");
                                break;
                            case 4:
                                text2 = TextResolver.GetText("The Galaxy's Last Hope");
                                break;
                        }
                        string text3 = string.Empty;
                        if (empire.DominantRace.Name.ToLower(CultureInfo.InvariantCulture) != "mechanoid" && empire.DominantRace != _Game.Galaxy.ShakturiActualRace)
                        {
                            text3 = ((_Game.Galaxy.StoryReturnOfTheShakturiEventLevel >= 2) ? (text3 + string.Format(TextResolver.GetText("The RACE tell us an important message"), empire.DominantRace.Name)) : (text3 + string.Format(TextResolver.GetText("The RACE tell us an important message Threat"), empire.DominantRace.Name)));
                            text3 += "\n\n";
                        }
                        text3 += text;
                        method_572(text2, text3, _Game.Galaxy.StoryReturnOfTheShakturiEventLevel);
                        if (_Game.Galaxy.StoryReturnOfTheShakturiEventLevel == 0 || _Game.Galaxy.StoryReturnOfTheShakturiEventLevel == 1 || _Game.Galaxy.StoryReturnOfTheShakturiEventLevel == 3 || _Game.Galaxy.StoryReturnOfTheShakturiEventLevel == 4)
                        {
                            _Game.Galaxy.StoryReturnOfTheShakturiEventLevel++;
                        }
                        EmpireMessage empireMessage = new EmpireMessage(_Game.PlayerEmpire, EmpireMessageType.GalacticHistory, null);
                        string text5 = (empireMessage.Description = text3.Replace("\n", " "));
                        empireMessage.Title = text2;
                        empireMessage.SupressPopup = true;
                        _Game.PlayerEmpire.SendMessageToEmpire(empireMessage, _Game.PlayerEmpire);
                        method_294();
                        break;
                    }
                case DialogPartType.HISTORY_OFFER_STORYMESSAGE_REJECT:
                    method_294();
                    break;
                case DialogPartType.MININGRIGHTS_OFFER:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                    diplomaticRelation.MiningRightsToOther = true;
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.GREETING_FRIENDLY, empire);
                    break;
                case DialogPartType.MININGRIGHTS_CANCEL:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                    diplomaticRelation.MiningRightsToOther = false;
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.GREETING_ANGRY, empire);
                    break;
                case DialogPartType.MILITARYREFUELING_OFFER:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                    diplomaticRelation.MilitaryRefuelingToOther = true;
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.GREETING_FRIENDLY, empire);
                    break;
                case DialogPartType.MILITARYREFUELING_CANCEL:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    diplomaticRelation = initiator.ObtainDiplomaticRelation(empire);
                    diplomaticRelation.MilitaryRefuelingToOther = false;
                    conversationOption_0 = method_234(conversationOption_0, DialogPartType.GREETING_ANGRY, empire);
                    break;
                case DialogPartType.PIRATE_PROTECTIONPROPOSE_OFFER:
                    if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                    }
                    if (empire.DetermineDesirePirateProtection(initiator))
                    {
                        empire.AcceptPirateProtection(initiator, conversationOption_0.Cost);
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.PIRATE_PROTECTIONPROPOSE_OFFER_ACCEPT, empire);
                    }
                    else
                    {
                        conversationOption_0 = method_234(conversationOption_0, DialogPartType.PIRATE_PROTECTIONPROPOSE_OFFER_REJECT, empire);
                    }
                    break;
                case DialogPartType.CANCELPIRATEPROTECTION:
                case DialogPartType.CANCELPIRATEPROTECTIONPIRATE:
                    {
                        if (_Game.PlayerEmpire.ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                        {
                            _Game.PlayerEmpire.ControlDiplomacyTreaties = AutomationLevel.Manual;
                        }
                        switch (num)
                        {
                            case -1:
                                conversationOption_0 = method_234(conversationOption_0, DialogPartType.CANCELTREATY_RESPONSE_ANGRY, empire);
                                break;
                            case 0:
                                conversationOption_0 = method_234(conversationOption_0, DialogPartType.CANCELTREATY_RESPONSE_NEUTRAL, empire);
                                break;
                            case 1:
                                conversationOption_0 = method_234(conversationOption_0, DialogPartType.CANCELTREATY_RESPONSE_FRIENDLY, empire);
                                break;
                        }
                        initiator.ChangePirateRelation(empire, PirateRelationType.None, _Game.Galaxy.CurrentStarDate);
                        PirateRelation pirateRelation = null;
                        if (initiator != null)
                        {
                            pirateRelation = empire.ObtainPirateRelation(initiator);
                            float evaluationChangeAmount = pirateRelation.CalculateOffenseOverCancellingProtection(_Game.Galaxy.CurrentStarDate);
                            empire.ChangePirateEvaluation(initiator, evaluationChangeAmount, PirateRelationEvaluationType.ProtectionCancelled);
                        }
                        break;
                    }
                case DialogPartType.PIRATE_PROTECTIONACCEPTRESPONSE:
                case DialogPartType.PIRATE_TRUCEACCEPTRESPONSE:
                    {
                        if (initiator != null && empire.ObtainPirateRelation(initiator).Type == PirateRelationType.Protection)
                        {
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.PIRATE_PROTECTIONALREADYPAID, empire);
                        }
                        else
                        {
                            if (initiator == null)
                            {
                                break;
                            }
                            if (conversationOption_0.Cost != 0.0 && initiator.StateMoney < conversationOption_0.Cost)
                            {
                                conversationOption_0 = method_234(conversationOption_0, DialogPartType.INFO_NOFUNDS, empire);
                                break;
                            }
                            empire.ChangePirateRelation(initiator, PirateRelationType.Protection, _Game.Galaxy.CurrentStarDate, conversationOption_0.Cost);
                            int num4 = empire.PirateMissions.IndexOfTarget(initiator, EmpireActivityType.Attack);
                            if (num4 >= 0)
                            {
                                empire.PirateMissions.RemoveAt(num4);
                            }
                            if (conversationOption_0.Cost > 0.0)
                            {
                                initiator.StateMoney -= conversationOption_0.Cost;
                                empire.StateMoney += conversationOption_0.Cost;
                                empire.PirateEconomy.PerformIncome(conversationOption_0.Cost, PirateIncomeType.ProtectionAgreement, _Game.Galaxy.CurrentStarDate);
                                empire.Counters.PirateProtectionIncome += conversationOption_0.Cost;
                            }
                            empire.CancelAttacksAgainstEmpire(initiator);
                            initiator.CancelAttacksAgainstEmpire(empire);
                        }
                        break;
                    }
                IL_10b3:
                    switch (tradeOfferResponse)
                    {
                        case TradeOfferResponse.Accept:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_ACCEPT, empire);
                            method_296(empire, conversationOption_0);
                            break;
                        case TradeOfferResponse.AcceptUnfair:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_ACCEPTCOMPLAIN, empire);
                            method_296(empire, conversationOption_0);
                            break;
                        case TradeOfferResponse.PromptForImprovement:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_IMPROVE, empire);
                            break;
                        case TradeOfferResponse.Refuse:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_REJECT, empire);
                            break;
                        case TradeOfferResponse.RefuseUnfair:
                            conversationOption_0 = method_234(conversationOption_0, DialogPartType.DEAL_REJECTCOMPLAIN, empire);
                            break;
                    }
                    initiator.ReviewDesignsBuiltObjectsImprovedComponents();
                    initiator.ReviewResearchAbilities();
                    empire.ReviewDesignsBuiltObjectsImprovedComponents();
                    empire.ReviewResearchAbilities();
                    selectedEmpire = ctlEmpireDiplomaticRelationList.SelectedEmpire;
                    ctlEmpireDiplomaticRelationList.BindData(_Game, _Game.PlayerEmpire, font_5, raceImageCache_0, bitmap_48, bitmap_51, bitmap_55, bitmap_81);
                    ctlEmpireDiplomaticRelationList.SelectEmpire(selectedEmpire);
                    break;
            }
            return conversationOption_0;
        }


  }

}