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
using Ionic.Zlib;

namespace DistantWorlds
{

    public partial class Main
    {

        private void method_347(ShipAction shipAction_1, bool bool_28)
        {
            BaconMain.HandleToolstripClick(shipAction_1);
            switch (shipAction_1.ActionType)
            {
                case ShipActionType.ColonyBuildWonder:
                    method_593(shipAction_1);
                    return;
                case ShipActionType.BuildOptionsPrivate:
                    method_593(shipAction_1);
                    return;
                case ShipActionType.BuildOptions:
                    method_593(shipAction_1);
                    return;
                case ShipActionType.ReturnToTop:
                    method_592();
                    return;
                case ShipActionType.ColonyBuildOptions:
                    method_593(shipAction_1);
                    return;
                case ShipActionType.FighterOptions:
                    method_593(shipAction_1);
                    return;
            }
            if (_Game.SelectedObject == null)
            {
                return;
            }
            if (_Game.SelectedObject is Fighter)
            {
                Fighter fighter = (Fighter)_Game.SelectedObject;
                if (fighter.Empire == _Game.PlayerEmpire)
                {
                    switch (shipAction_1.ActionType)
                    {
                        case ShipActionType.FighterLaunchFighters:
                        case ShipActionType.FighterLaunchBombers:
                            if (fighter.OnboardCarrier && fighter.ParentBuiltObject != null && !fighter.ParentBuiltObject.HasBeenDestroyed)
                            {
                                fighter.ParentBuiltObject.LaunchFighter(fighter);
                            }
                            break;
                        case ShipActionType.FighterRetrieveFighters:
                        case ShipActionType.FighterRetrieveBombers:
                            if (!fighter.OnboardCarrier && fighter.ParentBuiltObject != null && !fighter.ParentBuiltObject.HasBeenDestroyed)
                            {
                                fighter.ReturnToCarrier();
                            }
                            break;
                    }
                    BuiltObjectMissionType missionType = shipAction_1.MissionType;
                    if (missionType == BuiltObjectMissionType.Retire)
                    {
                        _Game.Galaxy.CheckTriggerEvent(fighter.GameEventId, _Game.PlayerEmpire, EventTriggerType.Destroy, null);
                        fighter.CompleteTeardown(_Game.Galaxy);
                        method_208(null);
                    }
                }
            }
            else if (_Game.SelectedObject is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)_Game.SelectedObject;
                if (builtObject.Role == BuiltObjectRole.Base)
                {
                    switch (shipAction_1.ActionType)
                    {
                        case ShipActionType.ChangePirateHomeBase:
                            {
                                if (!(shipAction_1.Target is Habitat))
                                {
                                    break;
                                }
                                Habitat habitat = (Habitat)shipAction_1.Target;
                                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && habitat != null)
                                {
                                    BuiltObject builtObject3 = _Game.Galaxy.IdentifyPirateBase(_Game.PlayerEmpire);
                                    if (builtObject3 != null && builtObject3.SupportCostFactor == 0f)
                                    {
                                        builtObject3.SupportCostFactor = 1f;
                                    }
                                    _Game.PlayerEmpire.PirateEmpireBaseHabitat = habitat;
                                    builtObject.SupportCostFactor = 0f;
                                }
                                break;
                            }
                        case ShipActionType.TransferCharacter:
                            if (shipAction_1.Target2 != null && shipAction_1.Target2 is Character)
                            {
                                Character character = (Character)shipAction_1.Target2;
                                CharacterList characterList = _Game.Galaxy.ResolveCharactersValidForLocation(builtObject, _Game.PlayerEmpire);
                                if (characterList.Contains(character))
                                {
                                    character.TransferToNewLocation(builtObject, _Game.Galaxy);
                                }
                            }
                            break;
                        case ShipActionType.GeneratePirateMissionAttack:
                            {
                                double attackPrice2 = _Game.PlayerEmpire.CalculatePirateAttackPrice(builtObject);
                                long expiryDate2 = _Game.Galaxy.CurrentStarDate + (long)(1.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                                EmpireActivity empireActivity2 = new EmpireActivity(builtObject.Empire, builtObject, attackPrice2, _Game.PlayerEmpire, expiryDate2, EmpireActivityType.Attack);
                                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity2))
                                {
                                    _Game.PlayerEmpire.PirateMissions.Add(empireActivity2);
                                    if (!_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity2))
                                    {
                                        _Game.Galaxy.PirateMissions.Add(empireActivity2);
                                    }
                                }
                                else if (_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity2))
                                {
                                    _Game.PlayerEmpire.PirateMissions.RemoveEquivalent(empireActivity2);
                                }
                                break;
                            }
                        case ShipActionType.GeneratePirateMissionDefend:
                            {
                                double attackPrice = _Game.PlayerEmpire.CalculatePirateDefendPrice(builtObject);
                                long expiryDate = _Game.Galaxy.CurrentStarDate + (long)(1.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                                EmpireActivity empireActivity = new EmpireActivity(builtObject.Empire, builtObject, attackPrice, _Game.PlayerEmpire, expiryDate, EmpireActivityType.Defend);
                                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity))
                                {
                                    _Game.PlayerEmpire.PirateMissions.Add(empireActivity);
                                    if (!_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity))
                                    {
                                        _Game.Galaxy.PirateMissions.Add(empireActivity);
                                    }
                                }
                                else if (_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity))
                                {
                                    _Game.PlayerEmpire.PirateMissions.RemoveEquivalent(empireActivity);
                                }
                                break;
                            }
                        case ShipActionType.GiveBuiltObject:
                            {
                                if (!(shipAction_1.Target is BuiltObject))
                                {
                                    break;
                                }
                                BuiltObject builtObject2 = (BuiltObject)shipAction_1.Target;
                                if (!(shipAction_1.Target2 is Empire))
                                {
                                    break;
                                }
                                Empire empire = (Empire)shipAction_1.Target2;
                                if (builtObject2.Empire != empire)
                                {
                                    empire.TakeOwnershipOfBuiltObject(builtObject2, empire, setDesignAsObsolete: true, removeFromFleet: true);
                                    if (_Game.SelectedObject == builtObject2)
                                    {
                                        method_208(null);
                                    }
                                    return;
                                }
                                break;
                            }
                        case ShipActionType.FighterBuildFighter:
                            builtObject.BuildNewFighter();
                            method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                            return;
                        case ShipActionType.FighterBuildBomber:
                            builtObject.BuildNewBomber();
                            method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                            return;
                        case ShipActionType.FighterLaunchFighters:
                            builtObject.LaunchAvailableFighters();
                            method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                            return;
                        case ShipActionType.FighterLaunchBombers:
                            builtObject.LaunchAvailableBombers();
                            method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                            return;
                        case ShipActionType.FighterRetrieveFighters:
                            builtObject.ReturnFighters();
                            method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                            return;
                        case ShipActionType.FighterRetrieveBombers:
                            builtObject.ReturnBombers();
                            method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                            return;
                        case ShipActionType.AssignAttack:
                            if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                            {
                                ShipGroup shipGroup = _Game.PlayerEmpire.IdentifyNearestAvailableFleet(builtObject.Xpos, builtObject.Ypos, mustBeAutomated: false, mustBeWithinFuelRange: true, 0.0);
                                if (shipGroup == null)
                                {
                                    shipGroup = _Game.PlayerEmpire.IdentifyNearestAvailableFleet(builtObject.Xpos, builtObject.Ypos, mustBeAutomated: false, mustBeWithinFuelRange: false, 0.0);
                                }
                                shipGroup?.AssignMission(BuiltObjectMissionType.Attack, builtObject, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                            }
                            break;
                        case ShipActionType.FighterUpgradeAll:
                            {
                                if (builtObject.Fighters == null || builtObject.Fighters.Count <= 0)
                                {
                                    return;
                                }
                                FighterSpecification fighterSpecification = null;
                                FighterSpecification fighterSpecification2 = null;
                                if (builtObject.Empire != null && builtObject.Empire.Research != null)
                                {
                                    fighterSpecification = builtObject.Empire.Research.IdentifyLatestFighterSpecification();
                                    fighterSpecification2 = builtObject.Empire.Research.IdentifyLatestBomberSpecification();
                                }
                                Fighter[] array = ListHelper.ToArrayThreadSafe(builtObject.Fighters);
                                foreach (Fighter fighter2 in array)
                                {
                                    if (fighter2.Specification == null)
                                    {
                                        continue;
                                    }
                                    if (fighterSpecification != null && fighter2.Specification.Type == FighterType.Interceptor)
                                    {
                                        if (fighter2.Specification != fighterSpecification && fighter2.OnboardCarrier)
                                        {
                                            fighter2.CompleteTeardown(_Game.Galaxy);
                                        }
                                    }
                                    else if (fighterSpecification2 != null && fighter2.Specification.Type == FighterType.Bomber && fighter2.Specification != fighterSpecification2 && fighter2.OnboardCarrier)
                                    {
                                        fighter2.CompleteTeardown(_Game.Galaxy);
                                    }
                                }
                                return;
                            }
                    }
                    switch (shipAction_1.MissionType)
                    {
                        case BuiltObjectMissionType.Retire:
                            if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.ConstructionQueue != null)
                            {
                                foreach (ConstructionYard constructionYard in builtObject.ParentHabitat.ConstructionQueue.ConstructionYards)
                                {
                                    if (constructionYard.ShipUnderConstruction == builtObject)
                                    {
                                        constructionYard.ShipUnderConstruction = null;
                                    }
                                }
                                if (builtObject.ParentHabitat.ConstructionQueue.ConstructionWaitQueue.Contains(builtObject))
                                {
                                    builtObject.ParentHabitat.ConstructionQueue.ConstructionWaitQueue.Remove(builtObject);
                                }
                            }
                            if (builtObject.ConstructionQueue != null)
                            {
                                foreach (ConstructionYard constructionYard2 in builtObject.ConstructionQueue.ConstructionYards)
                                {
                                    if (constructionYard2.ShipUnderConstruction != null)
                                    {
                                        constructionYard2.ShipUnderConstruction.SendCharactersHome();
                                        _Game.Galaxy.CheckTriggerEvent(constructionYard2.ShipUnderConstruction.GameEventId, _Game.PlayerEmpire, EventTriggerType.Destroy, null);
                                        constructionYard2.ShipUnderConstruction.CompleteTeardown(_Game.Galaxy, removeFromEmpire: true);
                                        constructionYard2.ShipUnderConstruction = null;
                                    }
                                }
                                BuiltObjectList builtObjectList = new BuiltObjectList();
                                builtObjectList.AddRange(builtObject.ConstructionQueue.ConstructionWaitQueue);
                                foreach (BuiltObject item in builtObjectList)
                                {
                                    item.SendCharactersHome();
                                    _Game.Galaxy.CheckTriggerEvent(item.GameEventId, _Game.PlayerEmpire, EventTriggerType.Destroy, null);
                                    item.CompleteTeardown(_Game.Galaxy, removeFromEmpire: true);
                                }
                                builtObject.ConstructionQueue.ConstructionWaitQueue.Clear();
                            }
                            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                            {
                                double num2 = Galaxy.CalculateBuiltObjectLootingValue(builtObject);
                                num2 *= _Game.PlayerEmpire.ColonyIncomeFactor;
                                num2 *= _Game.PlayerEmpire.LootingFactor;
                                num2 = _Game.PlayerEmpire.ApplyCorruptionToIncome(num2);
                                _Game.PlayerEmpire.StateMoney += num2;
                                _Game.PlayerEmpire.PirateEconomy.PerformIncome(num2, PirateIncomeType.Looting, _Game.Galaxy.CurrentStarDate);
                            }
                            builtObject.SendCharactersHome();
                            _Game.Galaxy.CheckTriggerEvent(builtObject.GameEventId, _Game.PlayerEmpire, EventTriggerType.Destroy, null);
                            builtObject.CompleteTeardown(_Game.Galaxy);
                            break;
                        case BuiltObjectMissionType.Retrofit:
                            {
                                if (shipAction_1.Design == null || builtObject.BuiltAt != null)
                                {
                                    break;
                                }
                                double num = 0.0;
                                ComponentList componentList = builtObject.Components.ResolveComponentList().Diff(shipAction_1.Design.Components);
                                foreach (DistantWorlds.Types.Component item2 in componentList)
                                {
                                    num += _Game.Galaxy.ComponentCurrentPrices[item2.ComponentID];
                                }
                                num *= 5.0;
                                bool flag2 = true;
                                if (builtObject.Owner == null)
                                {
                                    if (num > builtObject.Empire.GetPrivateFunds())
                                    {
                                        flag2 = false;
                                    }
                                }
                                else if (num > builtObject.Owner.StateMoney)
                                {
                                    flag2 = false;
                                }
                                if (flag2 && shipAction_1.Target is BuiltObject)
                                {
                                    builtObject.Empire.AssignRetrofitMission(builtObject, shipAction_1.Design, (BuiltObject)shipAction_1.Target, forceUseOfYard: true);
                                }
                                break;
                            }
                        case BuiltObjectMissionType.Build:
                            if (shipAction_1.Design != null)
                            {
                                if (shipAction_1.Design.SubRole == BuiltObjectSubRole.ColonyShip)
                                {
                                    if (_Game.PlayerEmpire.ControlColonization == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Colonization")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                    {
                                        _Game.PlayerEmpire.ControlColonization = AutomationLevel.Manual;
                                    }
                                }
                                else if (_Game.PlayerEmpire.ControlStateConstruction == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Ship Building")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                {
                                    _Game.PlayerEmpire.ControlStateConstruction = AutomationLevel.Manual;
                                }
                                bool flag = true;
                                if (shipAction_1.Design.SubRole == BuiltObjectSubRole.GasMiningStation || shipAction_1.Design.SubRole == BuiltObjectSubRole.MiningStation || shipAction_1.Design.SubRole == BuiltObjectSubRole.SmallFreighter || shipAction_1.Design.SubRole == BuiltObjectSubRole.MediumFreighter || shipAction_1.Design.SubRole == BuiltObjectSubRole.LargeFreighter || shipAction_1.Design.SubRole == BuiltObjectSubRole.PassengerShip || shipAction_1.Design.SubRole == BuiltObjectSubRole.MiningShip || shipAction_1.Design.SubRole == BuiltObjectSubRole.GasMiningShip)
                                {
                                    flag = false;
                                }
                                bool isAutoControlled = builtObject.Empire.NewBuiltObjectShouldBeAutomated(shipAction_1.Design.SubRole);
                                if (!flag)
                                {
                                    isAutoControlled = true;
                                }
                                if (builtObject.Empire.PurchaseNewBuiltObject(shipAction_1.Design, builtObject, flag, isAutoControlled) == null)
                                {
                                }
                            }
                            else if (builtObject.DamagedComponentCount > 0 && builtObject.Empire == _Game.PlayerEmpire)
                            {
                                _Game.Galaxy.FastFindBestConstructionShip(builtObject.Xpos, builtObject.Ypos, builtObject.Empire)?.AssignMission(BuiltObjectMissionType.BuildRepair, null, builtObject, BuiltObjectMissionPriority.Normal);
                            }
                            break;
                    }
                }
                else
                {
                    if (shipAction_1.ActionType != 0)
                    {
                        switch (shipAction_1.ActionType)
                        {
                            case ShipActionType.GiveBuiltObject:
                                {
                                    if (!(shipAction_1.Target is BuiltObject))
                                    {
                                        break;
                                    }
                                    BuiltObject builtObject5 = (BuiltObject)shipAction_1.Target;
                                    if (!(shipAction_1.Target2 is Empire))
                                    {
                                        break;
                                    }
                                    Empire empire2 = (Empire)shipAction_1.Target2;
                                    if (builtObject5.Empire != empire2)
                                    {
                                        empire2.TakeOwnershipOfBuiltObject(builtObject5, empire2, setDesignAsObsolete: true, removeFromFleet: true);
                                        if (_Game.SelectedObject == builtObject5)
                                        {
                                            method_208(null);
                                        }
                                        return;
                                    }
                                    break;
                                }
                            case ShipActionType.AutomateShip:
                                builtObject.IsAutoControlled = true;
                                if (builtObject.Empire != null)
                                {
                                    if (builtObject.Empire.PirateEmpireBaseHabitat == null)
                                    {
                                        builtObject.Empire.AssignMissionToBuiltObject(builtObject, atWar: false, null);
                                    }
                                    else
                                    {
                                        builtObject.Empire.PirateAssignShipMission(builtObject, _Game.Galaxy.CurrentStarDate);
                                    }
                                }
                                return;
                            case ShipActionType.JoinShipGroup:
                                if (shipAction_1.Target is ShipGroup)
                                {
                                    if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                    {
                                        _Game.PlayerEmpire.ControlMilitaryFleets = false;
                                    }
                                    ShipGroup shipGroup2 = (ShipGroup)shipAction_1.Target;
                                    shipGroup2.AddShipToFleet(builtObject);
                                }
                                else if (shipAction_1.Target == null)
                                {
                                    if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                    {
                                        _Game.PlayerEmpire.ControlMilitaryFleets = false;
                                    }
                                    ShipGroup shipGroup3 = new ShipGroup(_Game.Galaxy);
                                    shipGroup3.Empire = builtObject.Empire;
                                    shipGroup3.GatherPoint = null;
                                    string nextFleetNumberDescription = builtObject.Empire.GetNextFleetNumberDescription();
                                    shipGroup3.Name = string.Format(TextResolver.GetText("Nth Fleet"), nextFleetNumberDescription);
                                    builtObject.Empire.ShipGroups.Add(shipGroup3);
                                    shipGroup3.AddShipToFleet(builtObject);
                                    shipGroup3.Update();
                                    builtObject.Empire.ShipGroups.Sort();
                                    method_208(shipGroup3);
                                }
                                return;
                            case ShipActionType.LeaveShipGroup:
                                if (builtObject.ShipGroup != null)
                                {
                                    if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                    {
                                        _Game.PlayerEmpire.ControlMilitaryFleets = false;
                                    }
                                    builtObject.LeaveShipGroup();
                                }
                                return;
                            case ShipActionType.SetAsLeadShipInGroup:
                                if (builtObject.ShipGroup != null)
                                {
                                    if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                    {
                                        _Game.PlayerEmpire.ControlMilitaryFleets = false;
                                    }
                                    builtObject.ShipGroup.LeadShip = builtObject;
                                }
                                return;
                            case ShipActionType.AssignShipGroupHomeColony:
                                if (shipAction_1.Target is Habitat)
                                {
                                    if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                    {
                                        _Game.PlayerEmpire.ControlMilitaryFleets = false;
                                    }
                                    Habitat habitat3 = (Habitat)shipAction_1.Target;
                                    if (habitat3.Empire == builtObject.Empire && builtObject.ShipGroup != null)
                                    {
                                        builtObject.ShipGroup.GatherPoint = habitat3;
                                    }
                                }
                                return;
                            case ShipActionType.ClearQueuedMissions:
                                builtObject.SubsequentMissions.Clear();
                                return;
                            case ShipActionType.InvestigateRuins:
                                if (shipAction_1.Target is Habitat)
                                {
                                    Habitat habitat2 = (Habitat)shipAction_1.Target;
                                    if (_Game.Galaxy.CheckRuinsHaveBenefit(habitat2.Ruin, _Game.PlayerEmpire))
                                    {
                                        ArhCaEfBkk();
                                        _Game.Galaxy.InvestigateRuins(_Game.PlayerEmpire, habitat2);
                                    }
                                }
                                return;
                            case ShipActionType.InvestigateBuiltObject:
                                if (shipAction_1.Target is BuiltObject)
                                {
                                    BuiltObject builtObject4 = (BuiltObject)shipAction_1.Target;
                                    if (builtObject4.Empire == null)
                                    {
                                        musicPlayer_0.FadePause();
                                        string filePath = Application.StartupPath + "\\sounds\\effects\\discovery.mp3";
                                        musicPlayer_1.SetVolume(SoundVolume.Maximum);
                                        //musicPlayer_1.PlayMusicFileMethodDelegate(filePath);
                                        Invoke(musicPlayer_1.PlayMusicFileMethodDelegate, filePath);
                                        builtObject4.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.Prompt;
                                        _Game.Galaxy.InvestigateAbandonedBuiltObject(_Game.PlayerEmpire, builtObject4);
                                    }
                                }
                                return;
                            case ShipActionType.FighterBuildFighter:
                                builtObject.BuildNewFighter();
                                method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                                return;
                            case ShipActionType.FighterBuildBomber:
                                builtObject.BuildNewBomber();
                                method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                                return;
                            case ShipActionType.FighterLaunchFighters:
                                builtObject.LaunchAvailableFighters();
                                method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                                return;
                            case ShipActionType.FighterLaunchBombers:
                                builtObject.LaunchAvailableBombers();
                                method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                                return;
                            case ShipActionType.FighterRetrieveFighters:
                                builtObject.ReturnFighters();
                                method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                                return;
                            case ShipActionType.FighterRetrieveBombers:
                                builtObject.ReturnBombers();
                                method_593(new ShipAction(ShipActionType.FighterOptions, builtObject));
                                return;
                            case ShipActionType.UnautomateShip:
                                builtObject.IsAutoControlled = false;
                                return;
                            case ShipActionType.FighterUpgradeAll:
                                {
                                    if (builtObject.Fighters == null || builtObject.Fighters.Count <= 0)
                                    {
                                        return;
                                    }
                                    FighterSpecification fighterSpecification3 = null;
                                    FighterSpecification fighterSpecification4 = null;
                                    if (builtObject.Empire != null && builtObject.Empire.Research != null)
                                    {
                                        fighterSpecification3 = builtObject.Empire.Research.IdentifyLatestFighterSpecification();
                                        fighterSpecification4 = builtObject.Empire.Research.IdentifyLatestBomberSpecification();
                                    }
                                    Fighter[] array2 = ListHelper.ToArrayThreadSafe(builtObject.Fighters);
                                    foreach (Fighter fighter3 in array2)
                                    {
                                        if (fighter3.Specification == null)
                                        {
                                            continue;
                                        }
                                        if (fighterSpecification3 != null && fighter3.Specification.Type == FighterType.Interceptor)
                                        {
                                            if (fighter3.Specification != fighterSpecification3 && fighter3.OnboardCarrier)
                                            {
                                                fighter3.CompleteTeardown(_Game.Galaxy);
                                            }
                                        }
                                        else if (fighterSpecification4 != null && fighter3.Specification.Type == FighterType.Bomber && fighter3.Specification != fighterSpecification4 && fighter3.OnboardCarrier)
                                        {
                                            fighter3.CompleteTeardown(_Game.Galaxy);
                                        }
                                    }
                                    return;
                                }
                            case ShipActionType.TransferCharacter:
                                if (shipAction_1.Target2 != null && shipAction_1.Target2 is Character)
                                {
                                    Character character2 = (Character)shipAction_1.Target2;
                                    CharacterList characterList2 = _Game.Galaxy.ResolveCharactersValidForLocation(builtObject, _Game.PlayerEmpire);
                                    if (characterList2.Contains(character2))
                                    {
                                        character2.TransferToNewLocation(builtObject, _Game.Galaxy);
                                    }
                                }
                                break;
                        }
                    }
                    if (builtObject.BuiltAt != null)
                    {
                        return;
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Build || shipAction_1.MissionType == BuiltObjectMissionType.BuildRepair)
                    {
                        if (shipAction_1.Design != null && shipAction_1.Target != null && shipAction_1.Target is BuiltObject)
                        {
                            BuiltObject builtObject6 = (BuiltObject)shipAction_1.Target;
                            if (shipAction_1.IsSubsequentAction)
                            {
                                builtObject.QueueMission(BuiltObjectMissionType.Build, builtObject6, builtObject6, BuiltObjectMissionPriority.Normal);
                                return;
                            }
                            builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                            builtObject.AssignMission(BuiltObjectMissionType.Build, builtObject6, builtObject6, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            return;
                        }
                        if (shipAction_1.Design == null && shipAction_1.Target != null && shipAction_1.Target is BuiltObject)
                        {
                            BuiltObject builtObject7 = (BuiltObject)shipAction_1.Target;
                            if (builtObject7 == builtObject && builtObject7.Role != BuiltObjectRole.Base && (builtObject7.TopSpeed <= 0 || builtObject7.WarpSpeed <= 0))
                            {
                                BuiltObject builtObject8 = _Game.Galaxy.FastFindBestConstructionShip(builtObject7.Xpos, builtObject7.Ypos, builtObject7.Empire);
                                if (builtObject8 != null)
                                {
                                    if (builtObject8.Mission != null && builtObject8.Mission.Type != 0)
                                    {
                                        builtObject8.QueueMission(BuiltObjectMissionType.BuildRepair, null, builtObject7, builtObject7.Xpos, builtObject7.Ypos, BuiltObjectMissionPriority.Normal);
                                        return;
                                    }
                                    builtObject8.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                    builtObject8.AssignMission(BuiltObjectMissionType.BuildRepair, null, builtObject7, builtObject7.Xpos, builtObject7.Ypos, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                                    return;
                                }
                            }
                            if (builtObject7.Role == BuiltObjectRole.Base)
                            {
                                builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                builtObject.AssignMission(BuiltObjectMissionType.BuildRepair, null, builtObject7, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                            else if (shipAction_1.IsSubsequentAction)
                            {
                                builtObject.QueueMission(BuiltObjectMissionType.BuildRepair, null, builtObject7, builtObject7.Xpos, builtObject7.Ypos, BuiltObjectMissionPriority.Normal);
                            }
                            else
                            {
                                builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                builtObject.AssignMission(BuiltObjectMissionType.BuildRepair, null, builtObject7, builtObject7.Xpos, builtObject7.Ypos, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                            return;
                        }
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Patrol && shipAction_1.Target is Habitat && (((Habitat)shipAction_1.Target).Category == HabitatCategoryType.GasCloud || ((Habitat)shipAction_1.Target).Category == HabitatCategoryType.Star))
                    {
                        SystemInfo system = _Game.Galaxy.Systems[(Habitat)shipAction_1.Target];
                        builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                        builtObject.Empire.AssignShipSystemPatrol(builtObject, system, manuallyAssigned: true);
                        builtObject.IsAutoControlled = false;
                        return;
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Patrol && shipAction_1.Target is SystemInfo)
                    {
                        SystemInfo system2 = (SystemInfo)shipAction_1.Target;
                        builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                        builtObject.Empire.AssignShipSystemPatrol(builtObject, system2, manuallyAssigned: true);
                        builtObject.IsAutoControlled = false;
                        return;
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Retrofit)
                    {
                        if (shipAction_1.Design != null && shipAction_1.Target != null && shipAction_1.Target is BuiltObject)
                        {
                            builtObject.Empire.AssignRetrofitMission(builtObject, shipAction_1.Design, (BuiltObject)shipAction_1.Target, forceUseOfYard: true);
                        }
                        else if (shipAction_1.Design != null && shipAction_1.Target != null && shipAction_1.Target is Habitat)
                        {
                            builtObject.Empire.AssignRetrofitMission(builtObject, shipAction_1.Design, (Habitat)shipAction_1.Target, forceUseOfYard: true);
                        }
                        return;
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Retire && shipAction_1.Target != null && shipAction_1.Target is BuiltObject)
                    {
                        BuiltObject builtObject9 = (BuiltObject)shipAction_1.Target;
                        if (builtObject9 == builtObject && builtObject.BuiltAt == null)
                        {
                            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                            {
                                double num3 = Galaxy.CalculateBuiltObjectLootingValue(builtObject);
                                num3 *= _Game.PlayerEmpire.ColonyIncomeFactor;
                                num3 *= _Game.PlayerEmpire.LootingFactor;
                                num3 = _Game.PlayerEmpire.ApplyCorruptionToIncome(num3);
                                _Game.PlayerEmpire.StateMoney += num3;
                                _Game.PlayerEmpire.PirateEconomy.PerformIncome(num3, PirateIncomeType.Looting, _Game.Galaxy.CurrentStarDate);
                            }
                            builtObject.SendCharactersHome();
                            builtObject.CompleteTeardown(_Game.Galaxy);
                            return;
                        }
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.LoadTroops)
                    {
                        if (shipAction_1.Target != null && shipAction_1.Target is Habitat)
                        {
                            if (shipAction_1.IsSubsequentAction)
                            {
                                builtObject.Empire.AssignLoadTroopsMission(builtObject, (Habitat)shipAction_1.Target, queueMission: true, enforceMinimumTroopLimits: false, manuallyAssigned: true);
                                builtObject.IsAutoControlled = false;
                            }
                            else
                            {
                                builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                builtObject.Empire.AssignLoadTroopsMission(builtObject, (Habitat)shipAction_1.Target, queueMission: false, enforceMinimumTroopLimits: false, manuallyAssigned: true);
                                builtObject.IsAutoControlled = false;
                            }
                        }
                        else
                        {
                            builtObject.Empire.AssignLoadTroopsMission(builtObject, null, queueMission: false, enforceMinimumTroopLimits: false, manuallyAssigned: true);
                            builtObject.IsAutoControlled = false;
                        }
                        return;
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Escape)
                    {
                        if (builtObject.Attackers.Count > 0)
                        {
                            shipAction_1.Target = builtObject.Attackers[0];
                            builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                        }
                        else
                        {
                            int[] threatLevel;
                            StellarObject[] array3 = _Game.Galaxy.EvaluateThreats(builtObject, out threatLevel);
                            if (array3 == null || array3.Length <= 0)
                            {
                                return;
                            }
                            shipAction_1.Target = array3[0];
                            builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                        }
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Hold)
                    {
                        builtObject.ExitHyperjump();
                    }
                    if (!shipAction_1.IsSubsequentAction)
                    {
                        builtObject.ClearPreviousMissionRequirements(manuallyAssigned: true);
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.UnloadTroops)
                    {
                        TroopList troopList = new TroopList();
                        if (builtObject.Troops != null && builtObject.Troops.Count > 0)
                        {
                            troopList.AddRange(builtObject.Troops);
                        }
                        if (shipAction_1.Position.X == 0 && shipAction_1.Position.Y == 0)
                        {
                            if (shipAction_1.IsSubsequentAction)
                            {
                                builtObject.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, troopList, BuiltObjectMissionPriority.Normal);
                            }
                            else
                            {
                                builtObject.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, troopList, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                        }
                        else if (shipAction_1.IsSubsequentAction)
                        {
                            builtObject.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, null, troopList, null, null, shipAction_1.Position.X, shipAction_1.Position.Y, -1L, BuiltObjectMissionPriority.Normal);
                        }
                        else
                        {
                            builtObject.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, null, troopList, null, null, shipAction_1.Position.X, shipAction_1.Position.Y, -1L, BuiltObjectMissionPriority.Normal, allowReprocessing: true, manuallyAssigned: true);
                        }
                        return;
                    }
                    if (shipAction_1.Design != null)
                    {
                        if (shipAction_1.Position.X == 0 && shipAction_1.Position.Y == 0)
                        {
                            if (shipAction_1.IsSubsequentAction)
                            {
                                builtObject.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Design, BuiltObjectMissionPriority.Normal);
                            }
                            else
                            {
                                builtObject.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Design, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                        }
                        else if (shipAction_1.IsSubsequentAction)
                        {
                            builtObject.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Design, shipAction_1.Position.X, shipAction_1.Position.Y, BuiltObjectMissionPriority.Normal);
                        }
                        else
                        {
                            builtObject.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Design, shipAction_1.Position.X, shipAction_1.Position.Y, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                        }
                    }
                    else
                    {
                        if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip && _Game.PlayerEmpire.ControlColonization == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Colonization")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                        {
                            _Game.PlayerEmpire.ControlColonization = AutomationLevel.Manual;
                        }
                        if (shipAction_1.Position.X == 0 && shipAction_1.Position.Y == 0)
                        {
                            if (shipAction_1.IsSubsequentAction)
                            {
                                builtObject.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, BuiltObjectMissionPriority.Normal);
                            }
                            else
                            {
                                builtObject.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                            }
                        }
                        else if (shipAction_1.IsSubsequentAction)
                        {
                            builtObject.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Position.X, shipAction_1.Position.Y, BuiltObjectMissionPriority.Normal);
                        }
                        else
                        {
                            builtObject.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Position.X, shipAction_1.Position.Y, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                        }
                    }
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Hold)
                    {
                        if (shipAction_1.Target == null && shipAction_1.Target2 == null)
                        {
                            builtObject.PreferredSpeed = 0f;
                            builtObject.TargetSpeed = 0;
                        }
                        else if (shipAction_1.Target == builtObject && shipAction_1.Target2 == null)
                        {
                            builtObject.PreferredSpeed = 0f;
                            builtObject.TargetSpeed = 0;
                        }
                    }
                    builtObject.IsAutoControlled = false;
                }
            }
            else if (_Game.SelectedObject is Habitat)
            {
                Habitat habitat4 = (Habitat)_Game.SelectedObject;
                if (shipAction_1.ActionType != 0)
                {
                    switch (shipAction_1.ActionType)
                    {
                        case ShipActionType.ColonyTaxUp1:
                        case ShipActionType.ColonyTaxUp5:
                        case ShipActionType.ColonyTaxDown1:
                        case ShipActionType.ColonyTaxDown5:
                            if (_Game.PlayerEmpire.ControlColonyTaxRates && GenerateAutomationMessageBox(TextResolver.GetText("Colony Tax Rates")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                            {
                                _Game.PlayerEmpire.ControlColonyTaxRates = false;
                            }
                            if (habitat4.Empire != null && habitat4.Empire != _Game.Galaxy.IndependentEmpire && habitat4.Empire == _Game.PlayerEmpire)
                            {
                                double num6 = 0.0;
                                switch (shipAction_1.ActionType)
                                {
                                    case ShipActionType.ColonyTaxUp1:
                                        num6 = 0.01;
                                        break;
                                    case ShipActionType.ColonyTaxUp5:
                                        num6 = 0.05;
                                        break;
                                    case ShipActionType.ColonyTaxDown1:
                                        num6 = -0.01;
                                        break;
                                    case ShipActionType.ColonyTaxDown5:
                                        num6 = -0.05;
                                        break;
                                }
                                double num7 = habitat4.TaxRate;
                                num7 += num6;
                                num7 = Math.Max(0.0, Math.Min(num7, Empire._MaxTaxRate));
                                habitat4.TaxRate = (float)num7;
                                pnlDetailInfo.Invalidate();
                            }
                            return;
                        case ShipActionType.BuildColonize:
                            {
                                List<HabitatType> colonizableHabitatTypes = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpire(_Game.PlayerEmpire);
                                Design latestColonyShip = _Game.PlayerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                                BuiltObject builtObject10 = null;
                                foreach (BuiltObject builtObject13 in _Game.PlayerEmpire.BuiltObjects)
                                {
                                    if (builtObject13.SubRole == BuiltObjectSubRole.ColonyShip && builtObject13.Mission != null && builtObject13.Mission.Type == BuiltObjectMissionType.Colonize && builtObject13.Mission.TargetHabitat == habitat4)
                                    {
                                        builtObject10 = builtObject13;
                                        break;
                                    }
                                }
                                if (builtObject10 == null && _Game.PlayerEmpire.CanEmpireColonizeHabitat(_Game.PlayerEmpire, habitat4, colonizableHabitatTypes, latestColonyShip))
                                {
                                    method_539(habitat4);
                                    return;
                                }
                                break;
                            }
                        case ShipActionType.RecruitTroops:
                            {
                                if (_Game.PlayerEmpire.ControlTroopGeneration && GenerateAutomationMessageBox(TextResolver.GetText("Troop Recruitment")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                {
                                    _Game.PlayerEmpire.ControlTroopGeneration = false;
                                }
                                if (habitat4.Empire == null || habitat4.Empire == _Game.Galaxy.IndependentEmpire || habitat4.TroopsToRecruit == null)
                                {
                                    return;
                                }
                                Race dominantRace = habitat4.Population.DominantRace;
                                int num11 = dominantRace.TroopStrength;
                                if (habitat4.Ruin != null)
                                {
                                    num11 = (int)((double)num11 * (1.0 + habitat4.Ruin.BonusDefensive));
                                }
                                double bonusTotalByEffectType = habitat4.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.RecruitedTroopStrength);
                                num11 += (int)bonusTotalByEffectType;
                                Troop troop = null;
                                if (shipAction_1.Target2 != null && shipAction_1.Target2 is Troop)
                                {
                                    troop = (Troop)shipAction_1.Target2;
                                }
                                Troop troop2 = null;
                                if (troop != null)
                                {
                                    if (shipAction_1.ExtraData == "clone")
                                    {
                                        troop2 = Galaxy.GenerateNewTroop(habitat4.Empire.GenerateTroopDescription(troop.Name), troop.Type, troop.AttackStrength, habitat4.Empire, troop.Race, applyBonusFactors: false);
                                        troop2.SetDefendStrength(troop.DefendStrength);
                                    }
                                    else if (shipAction_1.ExtraData == "robotic")
                                    {
                                        troop2 = Galaxy.GenerateNewTroop(habitat4.Empire.GenerateTroopDescription(troop.Name), troop.Type, troop.AttackStrength, habitat4.Empire, troop.Race, applyBonusFactors: false);
                                        troop2.SetDefendStrength(troop.DefendStrength);
                                        troop2.MaintenanceMultiplier = 0.25f;
                                    }
                                    else if (shipAction_1.ExtraData == "elite")
                                    {
                                        troop2 = Galaxy.GenerateNewTroop(habitat4.Empire.GenerateTroopDescription(troop.Name), troop.Type, troop.AttackStrength, habitat4.Empire, troop.Race, applyBonusFactors: false);
                                        troop2.SetDefendStrength(troop.DefendStrength);
                                    }
                                    else
                                    {
                                        troop2 = Galaxy.GenerateNewTroop(habitat4.Empire.GenerateTroopDescription(troop.Name), troop.Type, num11, habitat4.Empire, dominantRace);
                                    }
                                }
                                else
                                {
                                    troop2 = Galaxy.GenerateNewTroop(habitat4.Empire.GenerateTroopDescription(dominantRace.TroopName), TroopType.Infantry, num11, habitat4.Empire, dominantRace);
                                }
                                troop2.Readiness = 0f;
                                troop2.Colony = habitat4;
                                habitat4.TroopsToRecruit.Add(troop2);
                                if (habitat4.Empire != null && habitat4.Empire.Troops != null)
                                {
                                    habitat4.Empire.Troops.Add(troop2);
                                }
                                return;
                            }
                        case ShipActionType.GeneratePirateMissionDefend:
                            {
                                double attackPrice3 = _Game.PlayerEmpire.CalculatePirateDefendPrice(habitat4);
                                long expiryDate3 = _Game.Galaxy.CurrentStarDate + (long)(1.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                                EmpireActivity empireActivity3 = new EmpireActivity(habitat4.Empire, habitat4, attackPrice3, _Game.PlayerEmpire, expiryDate3, EmpireActivityType.Defend);
                                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity3))
                                {
                                    _Game.PlayerEmpire.PirateMissions.Add(empireActivity3);
                                    if (!_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity3))
                                    {
                                        _Game.Galaxy.PirateMissions.Add(empireActivity3);
                                    }
                                }
                                else if (_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity3))
                                {
                                    _Game.PlayerEmpire.PirateMissions.RemoveEquivalent(empireActivity3);
                                }
                                break;
                            }
                        case ShipActionType.GeneratePirateMissionSmuggling:
                            if (shipAction_1.Target2 is byte)
                            {
                                byte b = (byte)shipAction_1.Target2;
                                double attackPrice4 = 1.0;
                                if (b != byte.MaxValue)
                                {
                                    attackPrice4 = _Game.PlayerEmpire.CalculatePirateSmugglePricePerUnit(habitat4, b);
                                }
                                long expiryDate4 = _Game.Galaxy.CurrentStarDate + (long)(3.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                                EmpireActivity empireActivity4 = new EmpireActivity(habitat4.Empire, habitat4, attackPrice4, _Game.PlayerEmpire, expiryDate4, EmpireActivityType.Smuggle);
                                empireActivity4.ResourceId = b;
                                if (b != byte.MaxValue)
                                {
                                    Order order = (empireActivity4.RelatedOrder = _Game.PlayerEmpire.CreateOrder(habitat4, new Resource(b), 10000, isState: true, OrderType.Standard, expiryDate4));
                                }
                                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity4))
                                {
                                    _Game.PlayerEmpire.PirateMissions.Add(empireActivity4);
                                    if (!_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity4))
                                    {
                                        _Game.Galaxy.PirateMissions.Add(empireActivity4);
                                    }
                                    break;
                                }
                                _Game.Galaxy.RemovePirateSmugglingMissionFromAllEmpires(empireActivity4);
                                _Game.PlayerEmpire.PirateMissions.RemoveEquivalent(empireActivity4);
                                EmpireActivity firstByTargetAndType = _Game.Galaxy.PirateMissions.GetFirstByTargetAndType(habitat4, EmpireActivityType.Smuggle);
                                if (firstByTargetAndType != null)
                                {
                                    _Game.Galaxy.PirateMissions.Remove(firstByTargetAndType);
                                    if (firstByTargetAndType.RelatedOrder != null)
                                    {
                                        firstByTargetAndType.RelatedOrder.ExpiryDate = _Game.Galaxy.CurrentStarDate;
                                    }
                                }
                            }
                            else if (shipAction_1.Target2 == null)
                            {
                                method_345();
                            }
                            break;
                        case ShipActionType.DeployVirus:
                            {
                                if (!(shipAction_1.Target is Habitat))
                                {
                                    break;
                                }
                                Habitat habitat5 = (Habitat)shipAction_1.Target;
                                if (habitat5.Population == null || habitat5.Population.TotalAmount <= 0L || !(shipAction_1.Target2 is Plague))
                                {
                                    break;
                                }
                                Plague plague = (Plague)shipAction_1.Target2;
                                habitat5.InfectWithPlague(plague, null);
                                if (plague.SpecialFunctionCode == 1)
                                {
                                    int num8 = Galaxy.Rnd.Next(15, 20);
                                    for (int k = 0; k < num8; k++)
                                    {
                                        _Game.Galaxy.SelectRelativeHabitatSurfacePoint(habitat5, out var num9, out var num10);
                                        _Game.Galaxy.GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat5, lockLocation: false, (int)num9, (int)num10);
                                    }
                                    _Game.PlayerEmpire.LastXaraktorVirusDeploy = _Game.Galaxy.CurrentDateTime;
                                }
                                break;
                            }
                        case ShipActionType.TransferCharacter:
                            if (shipAction_1.Target2 != null && shipAction_1.Target2 is Character)
                            {
                                Character character3 = (Character)shipAction_1.Target2;
                                CharacterList characterList3 = _Game.Galaxy.ResolveCharactersValidForLocation(habitat4, _Game.PlayerEmpire);
                                if (characterList3.Contains(character3))
                                {
                                    character3.TransferToNewLocation(habitat4, _Game.Galaxy);
                                }
                            }
                            break;
                        case ShipActionType.BuildPlanetaryFacility:
                            {
                                if (!(shipAction_1.Target is PlanetaryFacilityDefinition))
                                {
                                    return;
                                }
                                PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)shipAction_1.Target;
                                if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.Wonder)
                                {
                                    PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = habitat4.ResolveBuildableWonders();
                                    if (planetaryFacilityDefinitionList.GetById(planetaryFacilityDefinition.PlanetaryFacilityDefinitionId) != null && _Game.PlayerEmpire.StateMoney >= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, _Game.PlayerEmpire) && habitat4.QueueWonderConstruction(planetaryFacilityDefinition))
                                    {
                                        _Game.PlayerEmpire.StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, _Game.PlayerEmpire);
                                        _Game.PlayerEmpire.PirateEconomy.PerformExpense(Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, _Game.PlayerEmpire), PirateExpenseType.FacilityConstruction, _Game.Galaxy.CurrentStarDate);
                                    }
                                    return;
                                }
                                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = habitat4.ResolveBuildableFacilities();
                                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && habitat4.Empire != _Game.PlayerEmpire)
                                {
                                    planetaryFacilityDefinitionList2 = habitat4.ResolveBuildableFacilitiesPirates(_Game.PlayerEmpire);
                                }
                                double num4 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, _Game.PlayerEmpire);
                                if (planetaryFacilityDefinitionList2.FindFacilityByType(planetaryFacilityDefinition.Type) == null || !(_Game.PlayerEmpire.StateMoney >= num4))
                                {
                                    return;
                                }
                                bool flag3 = true;
                                PirateColonyControl pirateColonyControl = null;
                                if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.PirateBase || planetaryFacilityDefinition.Type == PlanetaryFacilityType.PirateFortress || planetaryFacilityDefinition.Type == PlanetaryFacilityType.PirateCriminalNetwork)
                                {
                                    flag3 = false;
                                    pirateColonyControl = habitat4.GetPirateControl().GetByFacilityControl();
                                    if (pirateColonyControl != null && pirateColonyControl.EmpireId == _Game.PlayerEmpire.EmpireId)
                                    {
                                        flag3 = true;
                                        if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.PirateCriminalNetwork)
                                        {
                                            flag3 = false;
                                            PlanetaryFacility planetaryFacility = habitat4.Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
                                            if (planetaryFacility != null && planetaryFacility.Type == PlanetaryFacilityType.PirateFortress && pirateColonyControl.HasFacilityControl && _Game.PlayerEmpire.CountPirateCriminalNetworks() <= 0)
                                            {
                                                flag3 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        pirateColonyControl = habitat4.GetPirateControl().GetByFaction(_Game.PlayerEmpire);
                                        float num5 = 0.5f;
                                        bool flag4 = true;
                                        if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.PirateFortress)
                                        {
                                            num5 = 1f;
                                        }
                                        if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.PirateCriminalNetwork)
                                        {
                                            flag4 = false;
                                            num5 = 1f;
                                            PlanetaryFacility planetaryFacility2 = habitat4.Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
                                            if (planetaryFacility2 != null && planetaryFacility2.Type == PlanetaryFacilityType.PirateFortress && pirateColonyControl.HasFacilityControl && _Game.PlayerEmpire.CountPirateCriminalNetworks() <= 0)
                                            {
                                                flag4 = true;
                                            }
                                        }
                                        if (pirateColonyControl != null && pirateColonyControl.ControlLevel >= num5 && flag4)
                                        {
                                            flag3 = true;
                                        }
                                    }
                                }
                                if (flag3 && habitat4.QueueFacilityConstruction(planetaryFacilityDefinition.Type))
                                {
                                    if (pirateColonyControl != null)
                                    {
                                        pirateColonyControl.HasFacilityControl = true;
                                    }
                                    _Game.PlayerEmpire.StateMoney -= num4;
                                    _Game.PlayerEmpire.PirateEconomy.PerformExpense(num4, PirateExpenseType.FacilityConstruction, _Game.Galaxy.CurrentStarDate);
                                }
                                return;
                            }
                    }
                }
                BuiltObjectMissionType missionType = shipAction_1.MissionType;
                if (missionType == BuiltObjectMissionType.Build && shipAction_1.Design != null)
                {
                    if (shipAction_1.Design.SubRole == BuiltObjectSubRole.ColonyShip)
                    {
                        if (_Game.PlayerEmpire.ControlColonization == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Colonization")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                        {
                            _Game.PlayerEmpire.ControlColonization = AutomationLevel.Manual;
                        }
                    }
                    else if (_Game.PlayerEmpire.ControlStateConstruction == AutomationLevel.FullyAutomated && GenerateAutomationMessageBox(TextResolver.GetText("Ship Building")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                    {
                        _Game.PlayerEmpire.ControlStateConstruction = AutomationLevel.Manual;
                    }
                    bool flag5 = true;
                    if (shipAction_1.Design.SubRole == BuiltObjectSubRole.GasMiningStation || shipAction_1.Design.SubRole == BuiltObjectSubRole.MiningStation)
                    {
                        flag5 = false;
                    }
                    int int_ = 0;
                    int int_2 = 0;
                    if (bool_28)
                    {
                        int_ = actionMenu.Left;
                        int_2 = actionMenu.Top;
                        method_151(ref int_, ref int_2);
                    }
                    else if (!shipAction_1.Position.IsEmpty)
                    {
                        int_ = shipAction_1.Position.X;
                        int_2 = shipAction_1.Position.Y;
                    }
                    if (habitat4.Empire != null && habitat4.Empire == _Game.PlayerEmpire)
                    {
                        bool isAutoControlled2 = habitat4.Empire.NewBuiltObjectShouldBeAutomated(shipAction_1.Design.SubRole);
                        if (!flag5)
                        {
                            isAutoControlled2 = true;
                        }
                        if (habitat4.Empire.PurchaseNewBuiltObject(shipAction_1.Design, habitat4, int_, int_2, flag5, isAutoControlled2) == null)
                        {
                        }
                    }
                    else if (shipAction_1.Design.SubRole != BuiltObjectSubRole.MiningStation && shipAction_1.Design.SubRole != BuiltObjectSubRole.GasMiningStation)
                    {
                        if (shipAction_1.Design.Role == BuiltObjectRole.Base)
                        {
                            double num12 = 0.0;
                            double num13 = 0.0;
                            if (habitat4.Category == HabitatCategoryType.Star)
                            {
                                if (habitat4.Type == HabitatType.BlackHole)
                                {
                                    _Game.Galaxy.SelectRelativeParkingPoint((double)habitat4.Diameter * 0.7, out num12, out num13);
                                }
                                else
                                {
                                    _Game.Galaxy.SelectRelativeParkingPoint(2000.0 + Galaxy.Rnd.NextDouble() * 3000.0, out num12, out num13);
                                }
                            }
                            else
                            {
                                _Game.Galaxy.SelectRelativeHabitatSurfacePoint(habitat4, out num12, out num13);
                            }
                            BuiltObject builtObject11 = _Game.Galaxy.FastFindBestConstructionShip(habitat4.Xpos, habitat4.Ypos, _Game.PlayerEmpire);
                            if (builtObject11 != null)
                            {
                                if (builtObject11.Mission != null && builtObject11.Mission.Type != 0)
                                {
                                    builtObject11.QueueMission(BuiltObjectMissionType.Build, habitat4, null, shipAction_1.Design, num12, num13, BuiltObjectMissionPriority.Normal);
                                }
                                else
                                {
                                    builtObject11.AssignMission(BuiltObjectMissionType.Build, habitat4, null, shipAction_1.Design, num12, num13, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                                }
                            }
                        }
                    }
                    else
                    {
                        method_540(null, habitat4);
                    }
                }
            }
            else if (_Game.SelectedObject is ShipGroup)
            {
                ShipGroup shipGroup4 = (ShipGroup)_Game.SelectedObject;
                if (shipAction_1.ActionType != 0)
                {
                    switch (shipAction_1.ActionType)
                    {
                        case ShipActionType.AssignShipGroupHomeColony:
                            if (shipAction_1.Target is Habitat)
                            {
                                Habitat habitat6 = (Habitat)shipAction_1.Target;
                                if (habitat6.Empire == shipGroup4.Empire)
                                {
                                    shipGroup4.GatherPoint = habitat6;
                                }
                            }
                            return;
                        case ShipActionType.ClearQueuedMissions:
                            shipGroup4.SubsequentMissions.Clear();
                            {
                                foreach (BuiltObject ship in shipGroup4.Ships)
                                {
                                    ship.SubsequentMissions.Clear();
                                }
                                return;
                            }
                        case ShipActionType.AutomateShip:
                            method_348(shipGroup4, bool_28: true);
                            return;
                        case ShipActionType.DisbandShipGroup:
                            {
                                if (shipAction_1.Target == null || !(shipAction_1.Target is ShipGroup))
                                {
                                    break;
                                }
                                ShipGroup shipGroup5 = (ShipGroup)shipAction_1.Target;
                                if (shipGroup5.Empire != null)
                                {
                                    if (_Game.SelectedObject == shipGroup5)
                                    {
                                        method_208(null);
                                    }
                                    shipGroup5.Empire.DisbandShipGroup(shipGroup5);
                                }
                                break;
                            }
                        case ShipActionType.UnautomateShip:
                            method_348(shipGroup4, bool_28: false);
                            return;
                        case ShipActionType.SetFleetPosture:
                            if (shipGroup4.Posture == FleetPosture.Attack)
                            {
                                shipGroup4.Posture = FleetPosture.Defend;
                            }
                            else if (shipGroup4.Posture == FleetPosture.Defend)
                            {
                                shipGroup4.Posture = FleetPosture.Attack;
                            }
                            return;
                        case ShipActionType.SetFleetRange:
                            if (shipGroup4.PostureRangeSquared <= 2250000.0)
                            {
                                shipGroup4.PostureRangeSquared = 2304000000.0;
                            }
                            else if (shipGroup4.PostureRangeSquared <= 2304000000.0)
                            {
                                shipGroup4.PostureRangeSquared = 250000000000.0;
                            }
                            else if (shipGroup4.PostureRangeSquared <= 250000000000.0)
                            {
                                shipGroup4.PostureRangeSquared = 1000000000000.0;
                            }
                            else if (shipGroup4.PostureRangeSquared <= 1000000000000.0)
                            {
                                shipGroup4.PostureRangeSquared = 3.4028234663852886E+38;
                            }
                            else
                            {
                                shipGroup4.PostureRangeSquared = 2250000.0;
                            }
                            return;
                        case ShipActionType.SetFleetAttackPoint:
                            mouseHoverMode_0 = MouseHoverMode.SetFleetAttackPoint;
                            return;
                        case ShipActionType.SetFleetHomeBase:
                            mouseHoverMode_0 = MouseHoverMode.SetFleetHomeBase;
                            return;
                        case ShipActionType.TransferCharacter:
                            if (shipAction_1.Target2 != null && shipAction_1.Target2 is Character)
                            {
                                Character character4 = (Character)shipAction_1.Target2;
                                if (shipAction_1.Target != null && shipAction_1.Target is BuiltObject)
                                {
                                    BuiltObject destination = (BuiltObject)shipAction_1.Target;
                                    character4.TransferToNewLocation(destination, _Game.Galaxy);
                                }
                            }
                            return;
                    }
                }
                if (shipAction_1.MissionType == BuiltObjectMissionType.Escape)
                {
                    if (shipGroup4.LeadShip.Attackers.Count > 0)
                    {
                        shipAction_1.Target = shipGroup4.LeadShip.Attackers[0];
                    }
                    else
                    {
                        int[] threatLevel2;
                        StellarObject[] array4 = _Game.Galaxy.EvaluateThreats(shipGroup4.LeadShip, out threatLevel2);
                        if (array4 == null || array4.Length <= 0)
                        {
                            return;
                        }
                        shipAction_1.Target = array4[0];
                    }
                }
                if (shipAction_1.MissionType == BuiltObjectMissionType.Blockade && shipAction_1.Target != null)
                {
                    if (shipAction_1.Target is Habitat)
                    {
                        Habitat colony = (Habitat)shipAction_1.Target;
                        Blockade blockade = _Game.Galaxy.Blockades[colony];
                        if (blockade != null)
                        {
                            if (blockade.Initiator != shipGroup4.Empire)
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (shipAction_1.IsSubsequentAction)
                            {
                                shipGroup4.QueueMission(BuiltObjectMissionType.Blockade, (Habitat)shipAction_1.Target, null, BuiltObjectMissionPriority.High);
                            }
                            else
                            {
                                shipGroup4.Empire.ImplementBlockade(colony, sendFleet: false, performAuthorizationCheck: false);
                            }
                            method_348(shipGroup4, bool_28: false);
                        }
                    }
                    else if (shipAction_1.Target is BuiltObject)
                    {
                        BuiltObject builtObject12 = (BuiltObject)shipAction_1.Target;
                        Blockade blockade2 = _Game.Galaxy.Blockades[builtObject12];
                        if (blockade2 != null)
                        {
                            if (blockade2.Initiator != shipGroup4.Empire)
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (shipAction_1.IsSubsequentAction)
                            {
                                shipGroup4.QueueMission(BuiltObjectMissionType.Blockade, (BuiltObject)shipAction_1.Target, null, BuiltObjectMissionPriority.High);
                            }
                            else
                            {
                                shipGroup4.Empire.ImplementBlockade(builtObject12, sendFleet: false, performAuthorizationCheck: false);
                            }
                            method_348(shipGroup4, bool_28: false);
                        }
                    }
                }
                method_348(shipGroup4, bool_28: false);
                BuiltObjectMissionType missionType2 = shipAction_1.MissionType;
                if (missionType2 == BuiltObjectMissionType.WaitAndAttack || missionType2 == BuiltObjectMissionType.WaitAndBombard)
                {
                    if (shipGroup4.Empire.CheckAssignFleetWaitAndAttackMission(shipGroup4, ref missionType2, shipAction_1.Target, BuiltObjectMissionPriority.High))
                    {
                        return;
                    }
                    shipAction_1.SetMissionType(missionType2);
                }
                if (shipAction_1.MissionType == BuiltObjectMissionType.LoadTroops)
                {
                    if (shipAction_1.Target != null && shipAction_1.Target is Habitat)
                    {
                        shipGroup4.Empire.AssignFleetLoadTroops(shipGroup4, (Habitat)shipAction_1.Target, manuallyAssigned: true);
                    }
                    else
                    {
                        shipGroup4.Empire.AssignFleetLoadTroops(shipGroup4, manuallyAssigned: true);
                    }
                    return;
                }
                if (shipAction_1.MissionType == BuiltObjectMissionType.UnloadTroops)
                {
                    if (shipAction_1.Target != null && shipAction_1.Target is Habitat)
                    {
                        shipGroup4.Empire.AssignFleetUnloadTroops(shipGroup4, (Habitat)shipAction_1.Target, manuallyAssigned: true);
                    }
                    return;
                }
                if (shipAction_1.MissionType == BuiltObjectMissionType.Patrol && shipAction_1.Target is Habitat && (((Habitat)shipAction_1.Target).Category == HabitatCategoryType.GasCloud || ((Habitat)shipAction_1.Target).Category == HabitatCategoryType.Star))
                {
                    SystemInfo system3 = _Game.Galaxy.Systems[(Habitat)shipAction_1.Target];
                    shipGroup4.Empire.AssignFleetSystemPatrol(shipGroup4, system3);
                    return;
                }
                if (shipAction_1.MissionType == BuiltObjectMissionType.Patrol && shipAction_1.Target is SystemInfo)
                {
                    SystemInfo system4 = (SystemInfo)shipAction_1.Target;
                    shipGroup4.Empire.AssignFleetSystemPatrol(shipGroup4, system4);
                    return;
                }
                if (shipAction_1.MissionType == BuiltObjectMissionType.Retrofit)
                {
                    StellarObject shipYard = null;
                    if (shipAction_1.Target != null && shipAction_1.Target is BuiltObject)
                    {
                        shipYard = (BuiltObject)shipAction_1.Target;
                    }
                    if (shipGroup4.Empire.AssignFleetRetrofit(shipGroup4, shipYard, isAutoRetrofit: false))
                    {
                        return;
                    }
                }
                if (shipAction_1.MissionType == BuiltObjectMissionType.Repair)
                {
                    if (shipAction_1.IsSubsequentAction)
                    {
                        shipGroup4.QueueMission(BuiltObjectMissionType.Repair, shipAction_1.Target, null, BuiltObjectMissionPriority.High);
                    }
                    else if (shipAction_1.Target != null && shipAction_1.Target is BuiltObject)
                    {
                        shipGroup4.Empire.AssignFleetRepair(shipGroup4, (BuiltObject)shipAction_1.Target);
                    }
                    else
                    {
                        shipGroup4.Empire.AssignFleetRepair(shipGroup4);
                    }
                }
                else if (shipAction_1.MissionType == BuiltObjectMissionType.Refuel)
                {
                    if (shipAction_1.IsSubsequentAction)
                    {
                        shipGroup4.QueueMission(BuiltObjectMissionType.Refuel, shipAction_1.Target, null, BuiltObjectMissionPriority.High);
                    }
                    else
                    {
                        shipGroup4.AssignMission(BuiltObjectMissionType.Refuel, shipAction_1.Target, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                    }
                }
                else if (shipAction_1.MissionType == BuiltObjectMissionType.Hold)
                {
                    shipGroup4.ForceCompleteMission();
                    for (int l = 0; l < shipGroup4.Ships.Count; l++)
                    {
                        shipGroup4.Ships[l].TargetSpeed = 0;
                        shipGroup4.Ships[l].PreferredSpeed = 0f;
                        shipGroup4.Ships[l].IsAutoControlled = false;
                    }
                }
                else if (shipAction_1.Design != null)
                {
                    if (shipAction_1.Position.X == 0 && shipAction_1.Position.Y == 0)
                    {
                        if (shipAction_1.IsSubsequentAction)
                        {
                            shipGroup4.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Design, BuiltObjectMissionPriority.Normal);
                        }
                        else
                        {
                            shipGroup4.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Design, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                        }
                    }
                    else if (shipAction_1.IsSubsequentAction)
                    {
                        shipGroup4.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, null, shipAction_1.Design, shipAction_1.Position.X, shipAction_1.Position.Y, BuiltObjectMissionPriority.Normal);
                    }
                    else
                    {
                        shipGroup4.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, null, shipAction_1.Design, shipAction_1.Position.X, shipAction_1.Position.Y, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                    }
                }
                else
                {
                    BuiltObjectMissionPriority priority = BuiltObjectMissionPriority.Normal;
                    if (shipAction_1.MissionType == BuiltObjectMissionType.Attack || shipAction_1.MissionType == BuiltObjectMissionType.WaitAndAttack || shipAction_1.MissionType == BuiltObjectMissionType.Bombard || shipAction_1.MissionType == BuiltObjectMissionType.WaitAndBombard || shipAction_1.MissionType == BuiltObjectMissionType.Refuel)
                    {
                        priority = BuiltObjectMissionPriority.High;
                    }
                    if (shipAction_1.Position.X == 0 && shipAction_1.Position.Y == 0)
                    {
                        if (shipAction_1.IsSubsequentAction)
                        {
                            shipGroup4.QueueMission(missionType2, shipAction_1.Target, null, priority);
                        }
                        else
                        {
                            shipGroup4.AssignMission(missionType2, shipAction_1.Target, null, priority, manuallyAssigned: true);
                        }
                    }
                    else if (shipAction_1.IsSubsequentAction)
                    {
                        shipGroup4.QueueMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Position.X, shipAction_1.Position.Y, priority);
                    }
                    else
                    {
                        shipGroup4.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Position.X, shipAction_1.Position.Y, priority, manuallyAssigned: true);
                    }
                }
            }
            if (!(_Game.SelectedObject is BuiltObjectList))
            {
                return;
            }
            BuiltObjectList builtObjectList2 = (BuiltObjectList)_Game.SelectedObject;
            if (shipAction_1.ActionType != 0)
            {
                switch (shipAction_1.ActionType)
                {
                    case ShipActionType.UnautomateShip:
                        {
                            foreach (BuiltObject item3 in builtObjectList2)
                            {
                                item3.IsAutoControlled = false;
                            }
                            return;
                        }
                    case ShipActionType.CreateNewFleet:
                        {
                            if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                            {
                                _Game.PlayerEmpire.ControlMilitaryFleets = false;
                            }
                            bool flag6 = false;
                            for (int m = 0; m < builtObjectList2.Count; m++)
                            {
                                if (builtObjectList2[m].Role == BuiltObjectRole.Military)
                                {
                                    flag6 = true;
                                    break;
                                }
                            }
                            if (!flag6)
                            {
                                return;
                            }
                            ShipGroup shipGroup6 = new ShipGroup(_Game.Galaxy);
                            shipGroup6.Empire = _Game.PlayerEmpire;
                            shipGroup6.GatherPoint = _Game.PlayerEmpire.SelectFleetBase(shipGroup6);
                            for (int n = 0; n < builtObjectList2.Count; n++)
                            {
                                if (builtObjectList2[n].Role == BuiltObjectRole.Military)
                                {
                                    shipGroup6.AddShipToFleet(builtObjectList2[n]);
                                }
                            }
                            if (shipGroup6.Ships.Count > 0)
                            {
                                string nextFleetNumberDescription2 = _Game.PlayerEmpire.GetNextFleetNumberDescription();
                                shipGroup6.Name = string.Format(TextResolver.GetText("Nth Fleet"), nextFleetNumberDescription2);
                                _Game.PlayerEmpire.ShipGroups.Add(shipGroup6);
                                _Game.PlayerEmpire.ShipGroups.Sort();
                            }
                            shipGroup6.Update();
                            method_208(shipGroup6);
                            return;
                        }
                    case ShipActionType.AutomateShip:
                        {
                            foreach (BuiltObject item4 in builtObjectList2)
                            {
                                item4.IsAutoControlled = true;
                                if (item4.Empire != null)
                                {
                                    if (item4.Empire.PirateEmpireBaseHabitat == null)
                                    {
                                        item4.Empire.AssignMissionToBuiltObject(item4, atWar: false, null);
                                    }
                                    else
                                    {
                                        item4.Empire.PirateAssignShipMission(item4, _Game.Galaxy.CurrentStarDate);
                                    }
                                }
                            }
                            return;
                        }
                    case ShipActionType.JoinShipGroup:
                        {
                            if (shipAction_1.Target is ShipGroup)
                            {
                                if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                                {
                                    _Game.PlayerEmpire.ControlMilitaryFleets = false;
                                }
                                ShipGroup shipGroup7 = (ShipGroup)shipAction_1.Target;
                                foreach (BuiltObject item5 in builtObjectList2)
                                {
                                    if (item5.Role == BuiltObjectRole.Military)
                                    {
                                        shipGroup7.AddShipToFleet(item5);
                                    }
                                }
                                shipGroup7.Update();
                                return;
                            }
                            if (shipAction_1.Target != null)
                            {
                                return;
                            }
                            if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                            {
                                _Game.PlayerEmpire.ControlMilitaryFleets = false;
                            }
                            ShipGroup shipGroup8 = new ShipGroup(_Game.Galaxy);
                            shipGroup8.Empire = builtObjectList2[0].Empire;
                            foreach (BuiltObject item6 in builtObjectList2)
                            {
                                if (item6.Role == BuiltObjectRole.Military)
                                {
                                    shipGroup8.AddShipToFleet(item6);
                                }
                            }
                            if (shipGroup8.Ships.Count > 0)
                            {
                                shipGroup8.GatherPoint = builtObjectList2[0].Empire.SelectFleetBase(shipGroup8);
                                string nextFleetNumberDescription3 = builtObjectList2[0].Empire.GetNextFleetNumberDescription();
                                shipGroup8.Name = string.Format(TextResolver.GetText("Nth Fleet"), nextFleetNumberDescription3);
                                builtObjectList2[0].Empire.ShipGroups.Add(shipGroup8);
                                shipGroup8.Update();
                                builtObjectList2[0].Empire.ShipGroups.Sort();
                            }
                            method_208(shipGroup8);
                            return;
                        }
                    case ShipActionType.LeaveShipGroup:
                        if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                        {
                            _Game.PlayerEmpire.ControlMilitaryFleets = false;
                        }
                        {
                            foreach (BuiltObject item7 in builtObjectList2)
                            {
                                if (item7.ShipGroup != null)
                                {
                                    item7.LeaveShipGroup();
                                }
                            }
                            return;
                        }
                }
            }
            if (shipAction_1.MissionType == BuiltObjectMissionType.Patrol && shipAction_1.Target is Habitat && (((Habitat)shipAction_1.Target).Category == HabitatCategoryType.GasCloud || ((Habitat)shipAction_1.Target).Category == HabitatCategoryType.Star))
            {
                SystemInfo systemInfo = _Game.Galaxy.Systems[(Habitat)shipAction_1.Target];
                {
                    foreach (BuiltObject item8 in builtObjectList2)
                    {
                        item8.ClearPreviousMissionRequirements(manuallyAssigned: true);
                        item8.AssignMission(shipAction_1.MissionType, systemInfo.SystemStar, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                        item8.IsAutoControlled = false;
                    }
                    return;
                }
            }
            if (shipAction_1.MissionType == BuiltObjectMissionType.Patrol && shipAction_1.Target is SystemInfo)
            {
                SystemInfo systemInfo2 = (SystemInfo)shipAction_1.Target;
                {
                    foreach (BuiltObject item9 in builtObjectList2)
                    {
                        item9.ClearPreviousMissionRequirements(manuallyAssigned: true);
                        item9.AssignMission(shipAction_1.MissionType, systemInfo2.SystemStar, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                        item9.IsAutoControlled = false;
                    }
                    return;
                }
            }
            if (shipAction_1.MissionType == BuiltObjectMissionType.Retire && shipAction_1.Target == null)
            {
                foreach (BuiltObject item10 in builtObjectList2)
                {
                    if (item10.BuiltAt == null)
                    {
                        item10.SendCharactersHome();
                        item10.CompleteTeardown(_Game.Galaxy);
                    }
                }
                list_5.Remove(_Game.SelectedObject);
                if (int_22 >= list_5.Count)
                {
                    int_22 = list_5.Count - 1;
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
                return;
            }
            if (shipAction_1.MissionType == BuiltObjectMissionType.Escape)
            {
                foreach (BuiltObject item11 in builtObjectList2)
                {
                    if (item11.Attackers.Count > 0)
                    {
                        shipAction_1.Target = item11.Attackers[0];
                        item11.ClearPreviousMissionRequirements(manuallyAssigned: true);
                    }
                    else
                    {
                        int[] threatLevel3;
                        StellarObject[] array5 = _Game.Galaxy.EvaluateThreats(item11, out threatLevel3);
                        if (array5 != null && array5.Length > 0)
                        {
                            shipAction_1.Target = array5[0];
                            item11.ClearPreviousMissionRequirements(manuallyAssigned: true);
                        }
                    }
                }
                return;
            }
            if (shipAction_1.MissionType == BuiltObjectMissionType.Attack && shipAction_1.Target != null)
            {
                foreach (BuiltObject item12 in builtObjectList2)
                {
                    if (item12.FirepowerRaw > 0 || item12.FighterCapacity > 0 || (item12.Troops != null && item12.Troops.TotalAttackStrength > 0))
                    {
                        item12.AssignMission(BuiltObjectMissionType.Attack, shipAction_1.Target, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                    }
                }
                return;
            }
            if (shipAction_1.MissionType == BuiltObjectMissionType.Refuel)
            {
                foreach (BuiltObject item13 in builtObjectList2)
                {
                    if (item13 != null)
                    {
                        if (shipAction_1.Target != null && shipAction_1.Target is StellarObject)
                        {
                            StellarObject stellarObject = (StellarObject)shipAction_1.Target;
                            if (stellarObject != null)
                            {
                                item13.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                item13.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.Unavailable);
                            }
                        }
                        else
                        {
                            StellarObject stellarObject2 = _Game.PlayerEmpire.FindNearestRefuellingPoint(item13.Xpos, item13.Ypos, item13.FuelType, 1);
                            if (stellarObject2 != null)
                            {
                                item13.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                item13.AssignMission(BuiltObjectMissionType.Refuel, stellarObject2, null, BuiltObjectMissionPriority.Unavailable);
                            }
                        }
                    }
                }
                return;
            }
            if (shipAction_1.MissionType == BuiltObjectMissionType.Repair)
            {
                foreach (BuiltObject item14 in builtObjectList2)
                {
                    if (item14 != null)
                    {
                        if (shipAction_1.Target != null && shipAction_1.Target is StellarObject)
                        {
                            StellarObject stellarObject3 = (StellarObject)shipAction_1.Target;
                            if (stellarObject3 != null && stellarObject3.ConstructionQueue != null)
                            {
                                item14.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                if (item14.DamagedComponentCount > 0)
                                {
                                    item14.AssignMission(BuiltObjectMissionType.Repair, stellarObject3, null, BuiltObjectMissionPriority.Unavailable);
                                }
                                else
                                {
                                    item14.AssignMission(BuiltObjectMissionType.Refuel, stellarObject3, null, BuiltObjectMissionPriority.Unavailable);
                                }
                            }
                        }
                        else
                        {
                            StellarObject stellarObject4 = _Game.PlayerEmpire.FindNearestShipYardBase(item14);
                            if (stellarObject4 != null)
                            {
                                item14.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                if (item14.DamagedComponentCount > 0)
                                {
                                    item14.AssignMission(BuiltObjectMissionType.Repair, stellarObject4, null, BuiltObjectMissionPriority.Unavailable);
                                }
                                else
                                {
                                    item14.AssignMission(BuiltObjectMissionType.Refuel, stellarObject4, null, BuiltObjectMissionPriority.Unavailable);
                                }
                            }
                        }
                    }
                }
                return;
            }
            foreach (BuiltObject item15 in builtObjectList2)
            {
                item15.ClearPreviousMissionRequirements(manuallyAssigned: true);
                if (shipAction_1.Position.X == 0 && shipAction_1.Position.Y == 0)
                {
                    item15.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                }
                else
                {
                    item15.AssignMission(shipAction_1.MissionType, shipAction_1.Target, null, shipAction_1.Position.X, shipAction_1.Position.Y, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                }
                item15.IsAutoControlled = false;
            }
        }

        public void actionMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null && e.ClickedItem.Tag is ShipAction)
            {
                ShipAction shipAction_ = (ShipAction)e.ClickedItem.Tag;
                method_347(shipAction_, bool_28: true);
            }
        }

        private void method_348(ShipGroup shipGroup_3, bool bool_28)
        {
            foreach (BuiltObject ship in shipGroup_3.Ships)
            {
                ship.IsAutoControlled = bool_28;
            }
        }

        private void btnCycleIdleShips_Click(object sender, EventArgs e)
        {
            BuiltObject builtObject = null;
            ShipGroup shipGroup = null;
            int num = 0;
            int num2 = 0;
            if (builtObject_4 != null)
            {
                num = _Game.PlayerEmpire.BuiltObjects.IndexOf(builtObject_4);
                builtObject = method_350(num, 1);
                if (builtObject == null)
                {
                    shipGroup = method_349(-1, 1);
                    if (shipGroup == null)
                    {
                        builtObject = method_350(-1, 1);
                    }
                }
            }
            else if (shipGroup_1 != null)
            {
                num2 = _Game.PlayerEmpire.ShipGroups.IndexOf(shipGroup_1);
                shipGroup = method_349(num2, 1);
                if (shipGroup == null)
                {
                    builtObject = method_350(-1, 1);
                    if (builtObject == null)
                    {
                        shipGroup = method_349(-1, 1);
                    }
                }
            }
            else
            {
                builtObject = method_350(-1, 1);
                if (builtObject == null)
                {
                    shipGroup = method_349(-1, 1);
                }
            }
            if (builtObject != null)
            {
                builtObject_4 = builtObject;
                shipGroup_1 = null;
            }
            else if (shipGroup != null)
            {
                builtObject_4 = null;
                shipGroup_1 = shipGroup;
            }
            else
            {
                builtObject_4 = null;
                shipGroup_1 = null;
            }
            if (shipGroup_1 != null)
            {
                method_208(shipGroup_1);
                if (UhvLmNjli7)
                {
                    method_157(shipGroup_1);
                    UhvLmNjli7 = true;
                }
            }
            else if (builtObject_4 != null)
            {
                method_208(builtObject_4);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_4);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private ShipGroup method_349(int int_64, int int_65)
        {
            if (_Game.PlayerEmpire.ShipGroups != null && _Game.PlayerEmpire.ShipGroups.Count > 0)
            {
                int num = 0;
                ShipGroup shipGroup = null;
                while (true)
                {
                    int_64 += int_65;
                    num++;
                    switch (int_65)
                    {
                        case 1:
                            if (int_64 >= _Game.PlayerEmpire.ShipGroups.Count)
                            {
                                return null;
                            }
                            goto default;
                        default:
                            {
                                ShipGroup shipGroup2 = _Game.PlayerEmpire.ShipGroups[int_64];
                                if (shipGroup2.Mission == null || shipGroup2.Mission.Type == BuiltObjectMissionType.Undefined)
                                {
                                    shipGroup = shipGroup2;
                                }
                                if (shipGroup != null || num >= _Game.PlayerEmpire.ShipGroups.Count)
                                {
                                    return shipGroup;
                                }
                                break;
                            }
                        case -1:
                            if (int_64 < 0)
                            {
                                return null;
                            }
                            goto default;
                    }
                }
            }
            return null;
        }

        private BuiltObject method_350(int int_64, int int_65)
        {
            if (_Game.PlayerEmpire.BuiltObjects != null && _Game.PlayerEmpire.BuiltObjects.Count > 0)
            {
                int num = 0;
                BuiltObject builtObject = null;
                while (true)
                {
                    int_64 += int_65;
                    num++;
                    switch (int_65)
                    {
                        case 1:
                            if (int_64 >= _Game.PlayerEmpire.BuiltObjects.Count)
                            {
                                return null;
                            }
                            goto default;
                        default:
                            {
                                BuiltObject builtObject2 = _Game.PlayerEmpire.BuiltObjects[int_64];
                                if (builtObject2.ShipGroup == null && (builtObject2.Mission == null || builtObject2.Mission.Type == BuiltObjectMissionType.Undefined) && builtObject2.Role != BuiltObjectRole.Base && builtObject2.BuiltAt == null && !builtObject2.IsAutoControlled)
                                {
                                    builtObject = builtObject2;
                                }
                                if (builtObject != null || num >= _Game.PlayerEmpire.BuiltObjects.Count)
                                {
                                    return builtObject;
                                }
                                break;
                            }
                        case -1:
                            if (int_64 < 0)
                            {
                                return null;
                            }
                            goto default;
                    }
                }
            }
            return null;
        }

        private void btnGameMenu_Click(object sender, EventArgs e)
        {
            if (pnlGameMenu.Visible)
            {
                method_357();
            }
            else
            {
                method_356();
            }
        }

        private void btnEmpireGraphs_Click(object sender, EventArgs e)
        {
            if (vHfFsoqMev.Visible)
            {
                method_401();
            }
            else
            {
                method_400();
            }
        }

        private void method_351(object sender, EventArgs e)
        {
        }

        private void RrhupiLdOr(object object_7)
        {
            if (object_7 == null)
            {
                return;
            }
            if (object_7 is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)object_7;
                if (builtObject.Empire == _Game.PlayerEmpire && builtObject.Role == BuiltObjectRole.Military)
                {
                    if (builtObject.AttackRangeSquared == 0f)
                    {
                        builtObject.AttackRangeSquared = 4000000f;
                    }
                    else if (builtObject.AttackRangeSquared == 4000000f)
                    {
                        builtObject.AttackRangeSquared = 2.304E+09f;
                    }
                    else if (builtObject.AttackRangeSquared == 2.304E+09f)
                    {
                        builtObject.AttackRangeSquared = 0f;
                    }
                    else
                    {
                        builtObject.AttackRangeSquared = 2.304E+09f;
                    }
                    pnlDetailInfo.Invalidate();
                }
            }
            else
            {
                if (!(object_7 is ShipGroup))
                {
                    return;
                }
                ShipGroup shipGroup = (ShipGroup)object_7;
                if (shipGroup.Empire == _Game.PlayerEmpire)
                {
                    if (shipGroup.AttackRangeSquared == 0f)
                    {
                        shipGroup.AttackRangeSquared = 4000000f;
                    }
                    else if (shipGroup.AttackRangeSquared == 4000000f)
                    {
                        shipGroup.AttackRangeSquared = 2.304E+09f;
                    }
                    else if (shipGroup.AttackRangeSquared == 2.304E+09f)
                    {
                        shipGroup.AttackRangeSquared = 0f;
                    }
                    else
                    {
                        shipGroup.AttackRangeSquared = 2.304E+09f;
                    }
                    for (int i = 0; i < shipGroup.Ships.Count; i++)
                    {
                        shipGroup.Ships[i].AttackRangeSquared = shipGroup.AttackRangeSquared;
                    }
                    pnlDetailInfo.Invalidate();
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern short GetKeyState(Keys keys_1);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys keys_1);

        private void method_352(bool bool_28)
        {
            if (!base.ContainsFocus)
            {
                return;
            }
            short num = 0;
            if (bool_28)
            {
                var targets = Main._ExpModMain.GetHotKeyManager().GetAllTargets();

                var up = targets.FirstOrDefault(x => x.KeyTarget == KeyMappingFriendlyNames.MoveViewUp);
                if (up != null)
                {
                    num = GetAsyncKeyState(up.MappedHotKeys[0].KeyCode[0]);
                    if (num < 0)
                    {
                        ebnBxUfJs7[0] = true;
                    }
                }
                var down = targets.FirstOrDefault(x => x.KeyTarget == KeyMappingFriendlyNames.MoveViewDown);
                if (down != null)
                {
                    num = GetAsyncKeyState(down.MappedHotKeys[0].KeyCode[0]);
                    if (num < 0)
                    {
                        ebnBxUfJs7[1] = true;
                    }
                }
                var left = targets.FirstOrDefault(x => x.KeyTarget == KeyMappingFriendlyNames.MoveViewLeft);
                if (left != null)
                {
                    num = GetAsyncKeyState(left.MappedHotKeys[0].KeyCode[0]);
                    if (num < 0)
                    {
                        ebnBxUfJs7[2] = true;
                    }
                }
                var right = targets.FirstOrDefault(x => x.KeyTarget == KeyMappingFriendlyNames.MoveViewRight);
                if (right != null)
                {
                    num = GetAsyncKeyState(right.MappedHotKeys[0].KeyCode[0]);
                    if (num < 0)
                    {
                        ebnBxUfJs7[3] = true;
                    }
                }
            }
            num = GetAsyncKeyState(Keys.Prior);
            if (num < 0)
            {
                ebnBxUfJs7[4] = true;
            }
            num = GetAsyncKeyState(Keys.Next);
            if (num < 0)
            {
                ebnBxUfJs7[5] = true;
            }
            num = GetAsyncKeyState(Keys.Snapshot);
            if (num < 0)
            {
                ebnBxUfJs7[6] = true;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            if (msg.Msg == WM_KEYDOWN && (Keys)msg.WParam.ToInt32() == Keys.Enter)
            {
                _pressedKeys.Add(Keys.Enter);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (_pressedKeys.Count == 0)
            { _pressedKeys.Add(e.KeyCode); }
            else if (_pressedKeys[_pressedKeys.Count - 1] != e.KeyCode)
            { _pressedKeys.Add(e.KeyCode); }
        }
        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (!_ExpModMain.Inited)
            { _ExpModMain.ModInitialize(this); }
            BaconMain.BaconInitialize(this);
            //e = BaconBuiltObject.BaconKeyboardInput(e, _pressedKeys);
            //if (!e.Handled)
            //{ 
            if (_pressedKeys.Count == 0)
            {
                e.Handled = true;
                return;
            }

            _ExpModMain.ModKeyboardInput(e, _pressedKeys);
            //}
            if (_Game == null || e.Handled)
            {
                _pressedKeys.Clear();
                return;
            }
            //if (e.Alt && e.KeyCode == Keys.F4)
            if (_pressedKeys.Count == 2 && _pressedKeys[0] == Keys.Menu && _pressedKeys[1] == Keys.F4)
            {
                _pressedKeys.Clear();
                e.Handled = true;
                return;
            }
            Keys keys = keys_0;
            TimeSpan timeSpan = DateTime.Now.Subtract(dateTime_0);
            //bool flag = false;
            //if (timeSpan.TotalSeconds < 3.0)
            //{
            //    flag = true;
            //}
            if (_pressedKeys.Contains(Keys.ControlKey))
            {
                keys_0 = Keys.None;
                dateTime_0 = DateTime.MinValue;
            }
            else
            {
                keys_0 = e.KeyCode;
                dateTime_0 = DateTime.Now;
            }
            if ((pnlBuiltObjectInfo.Visible || pnlColonyInfo.Visible || pnlDesignDetail.Visible || pnlDesigns.Visible || pnlDiplomacyTalk.Visible || pnlEmpireInfo.Visible || pnlEmpireSummary.Visible || CaLkaMyrMQ.Visible || pnlGameMenu.Visible || kYdDyYeMls.Visible || pnlResearch.Visible || pnlShipGroupInfo.Visible || pnlEncyclopedia.Visible || pnlTroopInfo.Visible || pnlGameEditor.Visible || pnlGameEditorPassword.Visible || pnlGameEditorEnterPassword.Visible || pnlEventMessage.Visible || pnlEmpirePolicy.Visible || pnlBuildOrder.Visible || pnlAdvisorSuggestion.Visible || pnlCharacterEventHistory.Visible) && e.KeyCode != Keys.F1 && e.KeyCode != Keys.F2 && e.KeyCode != Keys.F3 && e.KeyCode != Keys.F4 && e.KeyCode != Keys.F5 && e.KeyCode != Keys.F6 && e.KeyCode != Keys.F7 && e.KeyCode != Keys.F8 && e.KeyCode != Keys.F9 && e.KeyCode != Keys.F10 && e.KeyCode != Keys.F11 && e.KeyCode != Keys.F12 && e.KeyCode != Keys.Escape && e.KeyCode != Keys.NumPad0)
            {
                e.Handled = false;
                return;
            }
            if ((vHfFsoqMev.Visible || pnlGameOptions.Visible || pnlMessageHistory.Visible || pnlExpansionPlanner.Visible) && e.KeyCode != Keys.F1 && e.KeyCode != Keys.F2 && e.KeyCode != Keys.F3 && e.KeyCode != Keys.F4 && e.KeyCode != Keys.F5 && e.KeyCode != Keys.F6 && e.KeyCode != Keys.F7 && e.KeyCode != Keys.F8 && e.KeyCode != Keys.F9 && e.KeyCode != Keys.F10 && e.KeyCode != Keys.F11 && e.KeyCode != Keys.F12 && e.KeyCode != Keys.V && e.KeyCode != Keys.O && e.KeyCode != Keys.H && e.KeyCode != Keys.Escape && e.KeyCode != Keys.OemQuestion && e.KeyCode != Keys.NumPad0)
            {
                e.Handled = false;
                return;
            }
            MappedHotKey targetKeyData;
            if (_ExpModMain.GetMappedTarget(_pressedKeys, out targetKeyData))
            {
                int id;
                if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.ZoomToSelected, out id) &&
                    id == targetKeyData.Parent.TargetMethodId)
                {

                    //case Keys.Back:
                    //case KeyMappingFriendlyNames.ZoomToSelected:
                    btnZoomSelection_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.Escape, out id) &&
                   id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Escape:
                    //case KeyMappingFriendlyNames.Escape:
                    if (pnlGameMenu.Visible)
                    {
                        method_357();
                    }
                    else
                    {
                        method_356();
                    }
                    //break;
                }
                //case Keys.Pause:
                //case Keys.Space:
                //case KeyMappingFriendlyNames.ToglePause:
                if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.ToglePause, out id) &&
                id == targetKeyData.Parent.TargetMethodId)
                {
                    if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                    {
                        method_154();
                    }
                    else
                    {
                        method_155();
                    }
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.ZoomToGalaxyLevel, out id) &&
                id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.End:
                    //case KeyMappingFriendlyNames.ZoomToGalaxyLevel:
                    method_4(double_5);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.ZoomInMaximumLevel, out id) &&
        id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Home:
                    //case KeyMappingFriendlyNames.ZoomInMaximumLevel:
                    method_4(1.0);
                    //break;
                }
                //case Keys.Left:
                //case Keys.Up:
                //case Keys.Right:
                //case Keys.Down:
                //    return;
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.ZoomToSystemLevel, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Insert:
                    //case KeyMappingFriendlyNames.ZoomToSystemLevel:
                    method_4(50.0);
                    /*break*/
                    ;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.ZoomToSectorLevel, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Delete:
                    //case KeyMappingFriendlyNames.ZoomToSectorLevel:
                    method_4(3000.0);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup0, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D0:
                    //case KeyMappingFriendlyNames.ControlGroup0:
                    method_208(_Game.PlayerHotkey0);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup1, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D1:
                    //case KeyMappingFriendlyNames.ControlGroup1:
                    method_208(_Game.PlayerHotkey1);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup2, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D2:
                    //case KeyMappingFriendlyNames.ControlGroup2:
                    method_208(_Game.PlayerHotkey2);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup3, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D3:
                    //case KeyMappingFriendlyNames.ControlGroup3:
                    method_208(_Game.PlayerHotkey3);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup4, out id) &&
       id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D4:
                    //case KeyMappingFriendlyNames.ControlGroup4:
                    method_208(_Game.PlayerHotkey4);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup5, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D5:
                    //case KeyMappingFriendlyNames.ControlGroup5:
                    method_208(_Game.PlayerHotkey5);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup6, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D6:
                    //case KeyMappingFriendlyNames.ControlGroup6:
                    method_208(_Game.PlayerHotkey6);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup7, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D7:
                    //case KeyMappingFriendlyNames.ControlGroup7:
                    method_208(_Game.PlayerHotkey7);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup8, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D8:
                    //case KeyMappingFriendlyNames.ControlGroup8:
                    method_208(_Game.PlayerHotkey8);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup9, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D9:
                    //case KeyMappingFriendlyNames.ControlGroup9:
                    method_208(_Game.PlayerHotkey9);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup0WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D0:
                    //case KeyMappingFriendlyNames.ControlGroup0:
                    method_208(_Game.PlayerHotkey0);
                    method_157(_Game.PlayerHotkey0);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup1WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D1:
                    //case KeyMappingFriendlyNames.ControlGroup1:
                    method_208(_Game.PlayerHotkey1);
                    method_157(_Game.PlayerHotkey1);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup2WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D2:
                    //case KeyMappingFriendlyNames.ControlGroup2:
                    method_208(_Game.PlayerHotkey2);
                    method_157(_Game.PlayerHotkey2);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup3WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D3:
                    //case KeyMappingFriendlyNames.ControlGroup3:
                    method_208(_Game.PlayerHotkey3);
                    method_157(_Game.PlayerHotkey3);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup4WithFocus, out id) &&
       id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D4:
                    //case KeyMappingFriendlyNames.ControlGroup4:
                    method_208(_Game.PlayerHotkey4);
                    method_157(_Game.PlayerHotkey4);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup5WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D5:
                    //case KeyMappingFriendlyNames.ControlGroup5:
                    method_208(_Game.PlayerHotkey5);
                    method_157(_Game.PlayerHotkey5);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup6WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D6:
                    //case KeyMappingFriendlyNames.ControlGroup6:
                    method_208(_Game.PlayerHotkey6);
                    method_157(_Game.PlayerHotkey6);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup7WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D7:
                    //case KeyMappingFriendlyNames.ControlGroup7:
                    method_208(_Game.PlayerHotkey7);
                    method_157(_Game.PlayerHotkey7);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup8WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D8:
                    //case KeyMappingFriendlyNames.ControlGroup8:
                    method_208(_Game.PlayerHotkey8);
                    method_157(_Game.PlayerHotkey8);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SelectControlGroup9WithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D9:
                    //case KeyMappingFriendlyNames.ControlGroup9:
                    method_208(_Game.PlayerHotkey9);
                    method_157(_Game.PlayerHotkey9);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup0, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D0:
                    //case KeyMappingFriendlyNames.ControlGroup0:

                    method_225();
                    _Game.PlayerHotkey0 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup1, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D1:
                    //case KeyMappingFriendlyNames.ControlGroup1:

                    method_225();
                    _Game.PlayerHotkey1 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup2, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D2:
                    //case KeyMappingFriendlyNames.ControlGroup2:

                    method_225();
                    _Game.PlayerHotkey2 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup3, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D3:
                    //case KeyMappingFriendlyNames.ControlGroup3:

                    method_225();
                    _Game.PlayerHotkey3 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup4, out id) &&
       id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D4:
                    //case KeyMappingFriendlyNames.ControlGroup4:

                    method_225();
                    _Game.PlayerHotkey4 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup5, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D5:
                    //case KeyMappingFriendlyNames.ControlGroup5:

                    method_225();
                    _Game.PlayerHotkey5 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup6, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D6:
                    //case KeyMappingFriendlyNames.ControlGroup6:

                    method_225();
                    _Game.PlayerHotkey6 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup7, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D7:
                    //case KeyMappingFriendlyNames.ControlGroup7:

                    method_225();
                    _Game.PlayerHotkey7 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup8, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D8:
                    //case KeyMappingFriendlyNames.ControlGroup8:

                    method_225();
                    _Game.PlayerHotkey8 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.SetControlGroup9, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D9:
                    //case KeyMappingFriendlyNames.ControlGroup9:

                    method_225();
                    _Game.PlayerHotkey9 = _Game.SelectedObject;

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.EnableAuto, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.A:
                    //case KeyMappingFriendlyNames.EnableAuto:
                    if (_Game.SelectedObject is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)_Game.SelectedObject;
                        if (builtObject.Owner != null && builtObject.Owner == _Game.PlayerEmpire)
                        {
                            builtObject.IsAutoControlled = true;
                            if (builtObject.Owner.PirateEmpireBaseHabitat == null)
                            {
                                builtObject.Owner.AssignMissionToBuiltObject(builtObject, atWar: false, null);
                            }
                            else
                            {
                                builtObject.Owner.PirateAssignShipMission(builtObject, _Game.Galaxy.CurrentStarDate);
                            }
                        }
                    }
                    else if (_Game.SelectedObject is ShipGroup)
                    {
                        ShipGroup shipGroup = (ShipGroup)_Game.SelectedObject;
                        if (shipGroup.Empire == _Game.PlayerEmpire)
                        {
                            method_348(shipGroup, bool_28: true);
                        }
                    }
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleSelectionBackward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.B:
                    //case KeyMappingFriendlyNames.CycleSelectionBackward:
                    btnSelectionBack_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleColonySelectionBackward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.C:
                    //case KeyMappingFriendlyNames.CycleColonySelectionBackward:
                    btnCycleColoniesBack_Click(this, new EventArgs());
                    //Disabled in bacon
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(habitat_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleColonySelectionForward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.C:
                    //case KeyMappingFriendlyNames.CycleColonySelectionBackward:
                    btnCycleColonies_Click(this, new EventArgs());

                    //Disabled in bacon
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(habitat_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleColonySelectionBackwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.C:
                    //case KeyMappingFriendlyNames.CycleColonySelectionBackward:
                    btnCycleColoniesBack_Click(this, new EventArgs());

                    method_157(habitat_0);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleColonySelectionForwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.C:
                    //case KeyMappingFriendlyNames.CycleColonySelectionBackward:
                    btnCycleColonies_Click(this, new EventArgs());


                    method_157(habitat_0);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleMainDisplayTypes, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.D:
                    //case KeyMappingFriendlyNames.CycleMainDisplayTypes:
                    btnMainViewDisplayToggle_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.ShipEscapeCommand, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.E:
                    //case KeyMappingFriendlyNames.ShipEscapeCommand:
                    {
                        if (_Game.SelectedObject is BuiltObject)
                        {
                            //break;
                            BuiltObject builtObject4 = (BuiltObject)_Game.SelectedObject;
                            if (builtObject4.Owner != null && builtObject4.Owner == _Game.PlayerEmpire)
                            {

                                StellarObject stellarObject3 = null;
                                if (builtObject4.Attackers.Count > 0)
                                {
                                    stellarObject3 = builtObject4.Attackers[0];
                                    builtObject4.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                }
                                else
                                {
                                    int[] threatLevel;
                                    StellarObject[] array = _Game.Galaxy.EvaluateThreats(builtObject4, out threatLevel);
                                    if (array == null || array.Length <= 0)
                                    {
                                        _pressedKeys.Clear();
                                        return;
                                    }
                                    stellarObject3 = array[0];
                                    builtObject4.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                }
                                builtObject4.ClearPreviousMissionRequirements(manuallyAssigned: true);
                                builtObject4.AssignMission(BuiltObjectMissionType.Escape, stellarObject3, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                                builtObject4.IsAutoControlled = false;
                                //break;
                            }
                        }
                    }
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleFleetForward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F:
                    //case KeyMappingFriendlyNames.CycleFleet:
                    btnCycleShipGroups_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(shipGroup_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleFleetBackward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F:
                    //case KeyMappingFriendlyNames.CycleFleet:
                    btnCycleShipGroupsBack_Click(this, new EventArgs());
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(shipGroup_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleFleetForwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F:
                    //case KeyMappingFriendlyNames.CycleFleet:
                    btnCycleShipGroups_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    method_157(shipGroup_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleFleetBackwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F:
                    //case KeyMappingFriendlyNames.CycleFleet:
                    btnCycleShipGroupsBack_Click(this, new EventArgs());
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    method_157(shipGroup_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenGalaxyMap, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.G:
                    //case KeyMappingFriendlyNames.OpenGalaxyMap:
                    tbtnGalaxyMap_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenMessageHistory, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.H:
                    //case KeyMappingFriendlyNames.OpenMessageHistory:
                    btnHistoryMessages_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleIdleShipForward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.I:
                    //case KeyMappingFriendlyNames.CycleIdleShip:
                    btnCycleIdleShips_Click(this, new EventArgs());

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleIdleShipBackward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.I:
                    //case KeyMappingFriendlyNames.CycleIdleShip:
                    btnCycleIdleShipsBack_Click(this, new EventArgs());

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleIdleShipForwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.I:
                    //case KeyMappingFriendlyNames.CycleIdleShip:
                    btnCycleIdleShips_Click(this, new EventArgs());

                    method_157(builtObject_4);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleIdleShipBackwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.I:
                    //case KeyMappingFriendlyNames.CycleIdleShip:

                    btnCycleIdleShipsBack_Click(this, new EventArgs());

                    method_157(builtObject_4);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.ToggleViewLock, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.L:
                    //case KeyMappingFriendlyNames.ToggleViewLock:
                    btnLockView_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleMilitaryShipForward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.M:
                    //case KeyMappingFriendlyNames.CycleMilitaryShip:
                    btnCycleMilitary_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(builtObject_1);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleMilitaryShipBackward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.M:
                    //case KeyMappingFriendlyNames.CycleMilitaryShip:
                    btnCycleMilitaryBack_Click(this, new EventArgs());
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(builtObject_1);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleMilitaryShipForwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.M:
                    //case KeyMappingFriendlyNames.CycleMilitaryShip:
                    btnCycleMilitary_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    method_157(builtObject_1);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleMilitaryShipBackwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.M:
                    //case KeyMappingFriendlyNames.CycleMilitaryShip:
                    btnCycleMilitaryBack_Click(this, new EventArgs());
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    method_157(builtObject_1);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleSelectionForward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.N:
                    //case KeyMappingFriendlyNames.CycleSelectionForward:
                    btnSelectionForward_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenOptions, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.O:
                    //case KeyMappingFriendlyNames.OpenOptions:
                    if (pnlGameOptions.Visible)
                    {
                        method_413();
                    }
                    else
                    {
                        method_402();
                    }
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleBasesForward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.P:
                    //case KeyMappingFriendlyNames.CycleBases:
                    btnCycleBases_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(builtObject_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleBasesBackward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.P:
                    //case KeyMappingFriendlyNames.CycleBases:
                    btnCycleBasesBack_Click(this, new EventArgs());
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(builtObject_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleBasesForwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.P:
                    //case KeyMappingFriendlyNames.CycleBases:
                    btnCycleBases_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    method_157(builtObject_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleBasesBackwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.P:
                    //case KeyMappingFriendlyNames.CycleBases:
                    btnCycleBasesBack_Click(this, new EventArgs());
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    method_157(builtObject_0);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.RefuelShip, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.R:
                    //case KeyMappingFriendlyNames.RefuelShip:
                    if (_Game.SelectedObject is BuiltObject)
                    {
                        BuiltObject builtObject2 = (BuiltObject)_Game.SelectedObject;
                        if (builtObject2.Owner != null && builtObject2.Owner == _Game.PlayerEmpire)
                        {
                            builtObject2.ClearPreviousMissionRequirements(manuallyAssigned: true);
                            StellarObject stellarObject = null;
                            ResourceList fuelTypes = builtObject2.DetermineFuelRequired();
                            stellarObject = ((builtObject2.Role != BuiltObjectRole.Military) ? _Game.Galaxy.FastFindNearestRefuellingPoint(builtObject2.Xpos, builtObject2.Ypos, fuelTypes, builtObject2.ActualEmpire, builtObject2) : _Game.Galaxy.FastFindNearestRefuellingPoint(builtObject2.Xpos, builtObject2.Ypos, fuelTypes, builtObject2.ActualEmpire, builtObject2, includeResupplyShips: true, null));
                            if (stellarObject != null)
                            {
                                if (stellarObject is BuiltObject)
                                {
                                    builtObject2.AssignMission(BuiltObjectMissionType.Refuel, (BuiltObject)stellarObject, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                                }
                                else if (stellarObject is Habitat)
                                {
                                    builtObject2.AssignMission(BuiltObjectMissionType.Refuel, (Habitat)stellarObject, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                                }
                                builtObject2.IsAutoControlled = false;
                            }
                        }
                    }
                    else
                    {
                        if (_Game.SelectedObject is ShipGroup)
                        {

                            ShipGroup shipGroup2 = (ShipGroup)_Game.SelectedObject;
                            if (shipGroup2.Empire != null && shipGroup2.Empire == _Game.PlayerEmpire && shipGroup2.LeadShip != null)
                            {
                                ResourceList resourceList = shipGroup2.CalculateRequiredFuel();
                                StellarObject stellarObject2 = _Game.PlayerEmpire.DecideBestFleetRefuelPoint(shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos, shipGroup2.Empire, resourceList, null);
                                if (stellarObject2 == null)
                                {
                                    stellarObject2 = _Game.Galaxy.FastFindNearestRefuellingPoint(shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos, resourceList, shipGroup2.Empire, shipGroup2.LeadShip, includeResupplyShips: true, null, shipGroup2.Ships.Count);
                                }
                                if (stellarObject2 != null)
                                {
                                    if (stellarObject2 is BuiltObject)
                                    {
                                        shipGroup2.AssignMission(BuiltObjectMissionType.Refuel, (BuiltObject)stellarObject2, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                                    }
                                    else if (stellarObject2 is Habitat)
                                    {
                                        shipGroup2.AssignMission(BuiltObjectMissionType.Refuel, (Habitat)stellarObject2, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                                    }
                                    method_348(shipGroup2, bool_28: false);
                                }
                            }
                        }
                    }
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.StopShip, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.S:
                    //case KeyMappingFriendlyNames.StopShip:
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    btnGameMenuSave_Click(this, new EventArgs());
                    //    return;
                    //}
                    if (_Game.SelectedObject is BuiltObject)
                    {
                        BuiltObject builtObject3 = (BuiltObject)_Game.SelectedObject;
                        if (builtObject3.BuiltAt == null && builtObject3.Owner != null && builtObject3.Owner == _Game.PlayerEmpire)
                        {
                            builtObject3.ExitHyperjump();
                            builtObject3.ClearPreviousMissionRequirements(manuallyAssigned: true);
                            builtObject3.TargetSpeed = 0;
                            builtObject3.PreferredSpeed = 0f;
                            builtObject3.IsAutoControlled = false;
                        }
                    }
                    else
                    {
                        if (_Game.SelectedObject is ShipGroup)
                        {
                            ShipGroup shipGroup3 = (ShipGroup)_Game.SelectedObject;
                            foreach (BuiltObject ship in shipGroup3.Ships)
                            {
                                ship.ClearPreviousMissionRequirements();
                                ship.TargetSpeed = 0;
                                ship.PreferredSpeed = 0f;
                            }
                            shipGroup3.ForceCompleteMission();
                            method_348(shipGroup3, bool_28: false);
                        }
                    }
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CyclePanelVisibility, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.T:
                    //case KeyMappingFriendlyNames.CyclePanelVisibility:
                    if (pnlInfoPanel.Visible)
                    {
                        method_472(bool_28: true);
                        int num = pnlInfoPanel.Height + btnSelectionBack.Height + btnSelectionAction1.Height + 4;
                        rectangle_0.Y += num;
                        mainView.HoverMessageLocation = new Point(mainView.HoverMessageLocation.X, mainView.HoverMessageLocation.Y + num);
                    }
                    else if (pnlSystemMap.Visible)
                    {
                        method_472(bool_28: false);
                    }
                    else
                    {
                        method_473();
                        int num2 = pnlInfoPanel.Height + btnSelectionBack.Height + btnSelectionAction1.Height + 4;
                        rectangle_0.Y -= num2;
                        mainView.HoverMessageLocation = new Point(mainView.HoverMessageLocation.X, mainView.HoverMessageLocation.Y - num2);
                    }
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenEmpireComparison, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.V:
                    //case KeyMappingFriendlyNames.OpenEmpireComparison:
                    btnEmpireGraphs_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleColonyOrExplorerForward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.X:
                    //case KeyMappingFriendlyNames.CycleColonyOrExplorer:
                    btnCycleOther_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(builtObject_3);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleColonyOrExplorerBackward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.X:
                    //case KeyMappingFriendlyNames.CycleColonyOrExplorer:
                    btnCycleOtherBack_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(builtObject_3);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleColonyOrExplorerForwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.X:
                    //case KeyMappingFriendlyNames.CycleColonyOrExplorer:
                    btnCycleOther_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    method_157(builtObject_3);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleColonyOrExplorerBackwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.X:
                    //case KeyMappingFriendlyNames.CycleColonyOrExplorer:
                    btnCycleOtherBack_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    method_157(builtObject_3);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleConstractionShipForward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Y:
                    //case KeyMappingFriendlyNames.CycleConstractionShip:
                    btnCycleConstruction_Click(this, new EventArgs());

                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(builtObject_2);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleConstractionShipBackward, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Y:
                    //case KeyMappingFriendlyNames.CycleConstractionShip:
                    btnCycleConstructionBack_Click(this, new EventArgs());
                    //if (_pressedKeys.Contains(Keys.ControlKey))
                    //{
                    //    method_157(builtObject_2);
                    //}
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleConstractionShipForwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Y:
                    //case KeyMappingFriendlyNames.CycleConstractionShip:
                    btnCycleConstruction_Click(this, new EventArgs());

                    method_157(builtObject_2);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleConstractionShipBackwardWithFocus, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Y:
                    //case KeyMappingFriendlyNames.CycleConstractionShip:
                    btnCycleConstructionBack_Click(this, new EventArgs());

                    method_157(builtObject_2);

                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.FindNearestMilitaryShip, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Z:
                    //case KeyMappingFriendlyNames.FindNearestMilitaryShip:
                    kouhXgMiyR(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenHelp, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F1:
                    //case KeyMappingFriendlyNames.OpenHelp:
                    btnHelp_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenColoniesScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F2:
                    //case KeyMappingFriendlyNames.OpenColoniesScreen:
                    tbtnColonies_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenExpansionPlannerScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F3:
                    //case KeyMappingFriendlyNames.OpenExpansionPlannerScreen:
                    btnExpansionPlanner_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenIntelligenceAgentsScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F4:
                    //case KeyMappingFriendlyNames.OpenIntelligenceAgentsScreen:
                    tbtnIntelligenceAgents_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenDiplomacyScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F5:
                    //case KeyMappingFriendlyNames.OpenDiplomacyScreen:
                    tbtnEmpires_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenEmpireSummaryScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F6:
                    //case KeyMappingFriendlyNames.OpenEmpireSummaryScreen:
                    btnEmpireSummary_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenResearchScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F7:
                    //case KeyMappingFriendlyNames.OpenResearchScreen:
                    tbtnResearch_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenDesignsScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F8:
                    //case KeyMappingFriendlyNames.OpenDesignsScreen:
                    tbtnDesigns_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenBuildOrderScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F9:
                    //case KeyMappingFriendlyNames.OpenBuildOrderScreen:
                    btnBuildOrder_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenConstructionYardsScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F10:
                    //case KeyMappingFriendlyNames.OpenConstructionYardsScreen:
                    tbtnConstructionYards_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenShipAndBasesScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F11:
                    //case KeyMappingFriendlyNames.OpenShipAndBasesScreen:
                    tbtnBuiltObjects_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenFleetScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.F12:
                    //case KeyMappingFriendlyNames.OpenFleetScreen:
                    tbtnShipGroups_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenGroundInvasionStatusScreen, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.OemOpenBrackets:
                    //case KeyMappingFriendlyNames.OpenGroundInvasionStatusScreen:
                    {
                        if (pnlColonyInvasionContainer.Visible)
                        {
                            method_165();
                        }
                        else
                        {
                            Habitat habitat = null;
                            if (_Game.SelectedObject == null || !(_Game.SelectedObject is Habitat))
                            {
                                habitat = ((_Game.PlayerEmpire.Capital == null) ? null : _Game.PlayerEmpire.Capital);
                            }
                            else
                            {
                                habitat = (Habitat)_Game.SelectedObject;
                                if (habitat.Population == null || habitat.Population.Count <= 0)
                                {
                                    habitat = ((_Game.PlayerEmpire.Capital == null) ? null : _Game.PlayerEmpire.Capital);
                                }
                            }
                            if (habitat != null)
                            {
                                method_164(habitat);
                            }
                        }
                        //break;
                    }
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.IncreaseGameSpeed, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Add:
                    //case Keys.Oemplus:
                    //case KeyMappingFriendlyNames.IncreaseGameSpeed:
                    btnGameSpeedIncrease_Click(this, new EventArgs());
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.CycleShipEngagmentRange, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Oemcomma:
                    //case KeyMappingFriendlyNames.CycleShipEngagmentRange:
                    RrhupiLdOr(_Game.SelectedObject);
                    //break;
                }
                else if (_ExpModMain.ResolveTargetFriendlyName(KeyMappingFriendlyNames.DecreaseGameSpeed, out id) &&
           id == targetKeyData.Parent.TargetMethodId)
                {
                    //case Keys.Subtract:
                    //case Keys.OemMinus:
                    //case KeyMappingFriendlyNames.DecreaseGameSpeed:
                    btnGameSpeedDecrease_Click(this, new EventArgs());
                    //break;
                }
            }
            else
            {
                e.Handled = false;
                _pressedKeys.Clear();
                return;
            }
            Focus();
            _pressedKeys.Clear();
        }

        private void method_353(int upsCounter)
        {
            mainView.bool_8 = true;
            if (mainView.bool_11)
            {
                mainView.DrawMainViewXna(upsCounter);
                Thread.Sleep(1);
            }
            else
            {
                mainView.Refresh();
            }
            Bitmap desktopImage = Start.GetDesktopImage();
            mainView.bool_8 = false;
            EncoderParameter encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            ImageCodecInfo imageCodecInfo = null;
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].MimeType == "image/jpeg")
                {
                    imageCodecInfo = imageEncoders[i];
                    break;
                }
            }
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = encoderParameter;
            string text = GetGameFilesFolderCreateIfNeeded() + "screenshots\\";
            string text2 = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
            string text3 = ".jpg";
            if (imageCodecInfo == null)
            {
                text3 = ".png";
            }
            string text4 = text + text2 + text3;
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            if (!File.Exists(text4))
            {
                if (imageCodecInfo != null)
                {
                    desktopImage.Save(text4, imageCodecInfo, encoderParameters);
                }
                else
                {
                    desktopImage.Save(text4, ImageFormat.Png);
                }
                string text5 = TextResolver.GetText("The screenshot has been saved at the following location") + ": ";
                text5 += Environment.NewLine;
                text5 += Environment.NewLine;
                text5 += text4;
                MessageBoxEx messageBoxEx = method_371(text5, TextResolver.GetText("Screenshot Saved"), MessageBoxExIcon.Information);
                messageBoxEx.Show(this);
            }
            desktopImage?.Dispose();
        }

        public void SetHighlight()
        {
        }

        private void mainView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _pressedKeys.Clear();
            Point point = PointToClient(MouseHelper.GetCursorPosition());
            int int_ = point.X;
            int int_2 = point.Y;
            method_151(ref int_, ref int_2);
            if (itemListCollectionPanel_0.CheckClick(e.Location, e.Button, isDoubleClick: true))
            {
                return;
            }
            if (pnlGameEditor.Visible)
            {
                method_224(int_, int_2);
            }
            else
            {
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                object obj = method_143(int_, int_2, bool_28: false);
                if (obj == null)
                {
                    return;
                }
                if (obj is BuiltObject)
                {
                    if (!_Game.PlayerEmpire.IsObjectVisibleToThisEmpire((BuiltObject)obj))
                    {
                        return;
                    }
                }
                else if (obj is Habitat)
                {
                    if (_Game.PlayerEmpire.CheckSystemVisibilityStatus(((Habitat)obj).SystemIndex) == SystemVisibilityStatus.Unexplored)
                    {
                        return;
                    }
                }
                else if (obj is ShipGroup && !_Game.PlayerEmpire.IsObjectVisibleToThisEmpire(((ShipGroup)obj).LeadShip))
                {
                    return;
                }
                if (obj is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)obj;
                    if (builtObject.Empire == _Game.PlayerEmpire && builtObject.ShipGroup != null)
                    {
                        method_208(builtObject.ShipGroup);
                        bool_18 = true;
                        builtObject_5 = null;
                        shipGroup_2 = builtObject.ShipGroup;
                    }
                }
                else if (obj is SystemInfo)
                {
                    SystemInfo systemInfo = (SystemInfo)obj;
                    int_13 = (int)systemInfo.SystemStar.Xpos;
                    int_14 = (int)systemInfo.SystemStar.Ypos;
                    method_4(30.0);
                    bool_19 = true;
                }
                else if (obj is Habitat)
                {
                    Habitat habitat = (Habitat)obj;
                    if (double_0 <= 1.0 && habitat.Category == HabitatCategoryType.Star)
                    {
                        int_13 = (int)habitat.Xpos;
                        int_14 = (int)habitat.Ypos;
                        method_4(30.0);
                    }
                    else
                    {
                        int_13 = (int)habitat.Xpos;
                        int_14 = (int)habitat.Ypos;
                        method_4(1.0);
                    }
                    bool_19 = true;
                }
            }
        }

        private void method_354(BuiltObjectList builtObjectList_1, Habitat habitat_9)
        {
            if (builtObjectList_1 == null)
            {
                return;
            }
            ToolStripMenuItem toolStripMenuItem = null;
            if (habitat_9 != null)
            {
                Bitmap image = new Bitmap(32, 15, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    Rectangle rect = new Rectangle(18, 0, 14, 14);
                    Bitmap bitmap = habitatImageCache_0.ObtainImageSmall(habitat_9);
                    if (habitat_9.Empire != null)
                    {
                        graphics.DrawImageUnscaled(habitat_9.Empire.SmallFlagPicture, 0, 3);
                    }
                    if (bitmap != null && bitmap.PixelFormat != 0)
                    {
                        bitmap = PrecacheScaledBitmap(bitmap, 14, 14);
                        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        graphics.DrawImage(bitmap, rect);
                    }
                }
                toolStripMenuItem = new ToolStripMenuItem(habitat_9.Name, image);
                toolStripMenuItem.Tag = habitat_9;
                toolStripMenuItem.Font = font_3;
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = true;
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ImageScalingSize = new Size(32, 15);
                selectionMenu.Items.Add(toolStripMenuItem);
            }
            foreach (BuiltObject item in builtObjectList_1)
            {
                if (item.ShipGroup != null && item.ShipGroup.LeadShip == item && double_0 > 100.0)
                {
                    Bitmap image2 = new Bitmap(32, 15, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics2 = Graphics.FromImage(image2))
                    {
                        graphics2.DrawImageUnscaled(item.Empire.SmallFlagPicture, 0, 3);
                    }
                    toolStripMenuItem = new ToolStripMenuItem(item.ShipGroup.Name, image2);
                    toolStripMenuItem.Tag = item.ShipGroup;
                    toolStripMenuItem.Font = font_3;
                }
                else
                {
                    Bitmap image3 = new Bitmap(32, 15, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics3 = Graphics.FromImage(image3))
                    {
                        Rectangle rect2 = new Rectangle(18, 0, 14, 14);
                        Bitmap bitmap2 = null;
                        if (item.Empire != null)
                        {
                            graphics3.DrawImageUnscaled(item.Empire.SmallFlagPicture, 0, 3);
                            bitmap2 = PrepareBuiltObjectImage(item, builtObjectImageCache_0.ObtainImageSmall(item), item.Empire.MainColor, item.Empire.SecondaryColor, item.Size, item.Size);
                        }
                        else
                        {
                            bitmap2 = PrepareBuiltObjectImage(item, builtObjectImageCache_0.ObtainImageSmall(item), Color.Gray, Color.Gray, item.Size, item.Size);
                        }
                        if (bitmap2 != null && bitmap2.PixelFormat != 0)
                        {
                            bitmap2 = PrecacheScaledBitmap(bitmap2, 14, 14);
                            bitmap2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            graphics3.DrawImage(bitmap2, rect2);
                        }
                    }
                    toolStripMenuItem = new ToolStripMenuItem(item.Name + " (" + Galaxy.ResolveDescription(item.SubRole) + ")", image3);
                    toolStripMenuItem.Tag = item;
                    toolStripMenuItem.Font = font_3;
                }
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = true;
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ImageScalingSize = new Size(32, 15);
                selectionMenu.Items.Add(toolStripMenuItem);
            }
        }

        private void method_355()
        {
            _ = actionMenu.SourceControl;
            _ = actionMenu.OwnerItem;
            selectionMenu.Font = font_3;
            selectionMenu.Items.Clear();
            selectionMenu.Renderer = new CustomToolStripRenderer(font_3);
            selectionMenu.ShowImageMargin = true;
            selectionMenu.Margin = new Padding(1);
            selectionMenu.ImageScalingSize = new Size(32, 15);
        }

        private void VxFuqrlDb8(int int_64, int int_65)
        {
            method_355();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList = builtObjectList_0;
            method_354(builtObjectList, habitat_3);
            builtObjectList_0 = null;
            habitat_3 = null;
        }

        private void selectionMenu_Opening(object sender, CancelEventArgs e)
        {
            if (!bool_9 && !bool_10 && !bool_16)
            {
                Point point = PointToClient(MouseHelper.GetCursorPosition());
                int int_ = point.X;
                int int_2 = point.Y;
                method_151(ref int_, ref int_2);
                if (selectionMenu.Items == null || selectionMenu.Items.Count == 0)
                {
                    VxFuqrlDb8(int_, int_2);
                }
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void selectionMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
            {
                method_225();
                method_208(e.ClickedItem.Tag);
            }
        }

        private void method_356()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlGameMenu.Size = new Size(220, 408);
            pnlGameMenu.Location = new Point((mainView.Width - pnlGameMenu.Width) / 2, (mainView.Height - pnlGameMenu.Height) / 2);
            picGameMenuHeader.Size = new Size(196, 118);
            picGameMenuHeader.Location = new Point(12, 10);
            lblGameMenuTitle.Visible = false;
            btnGameMenuQuit.Size = new Size(200, 30);
            btnGameMenuQuit.Location = new Point(10, 137);
            btnGameMenuQuit.Font = font_2;
            btnGameMenuStartMenu.Size = new Size(200, 30);
            btnGameMenuStartMenu.Location = new Point(10, 170);
            btnGameMenuStartMenu.Font = font_2;
            btnGameMenuLoad.Size = new Size(200, 30);
            btnGameMenuLoad.Location = new Point(10, 203);
            btnGameMenuLoad.Font = font_2;
            btnGameMenuSave.Size = new Size(200, 30);
            btnGameMenuSave.Location = new Point(10, 236);
            btnGameMenuSave.Font = font_2;
            btnGameMenuSaveAs.Size = new Size(200, 30);
            btnGameMenuSaveAs.Location = new Point(10, 269);
            btnGameMenuSaveAs.Font = font_2;
            btnGameMenuOptions.Size = new Size(200, 30);
            btnGameMenuOptions.Location = new Point(10, 302);
            btnGameMenuOptions.Font = font_2;
            btnGameMenuEditor.Size = new Size(200, 30);
            btnGameMenuEditor.Location = new Point(10, 335);
            btnGameMenuEditor.Font = font_2;
            btnGameMenuCancel.Text = TextResolver.GetText("Resume Playing");
            btnGameMenuCancel.Size = new Size(200, 30);
            btnGameMenuCancel.Location = new Point(10, 368);
            btnGameMenuCancel.Font = font_2;
            pnlGameMenu.Visible = true;
            pnlGameMenu.BringToFront();
        }

        private void method_357()
        {
            pnlGameMenu.SendToBack();
            pnlGameMenu.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void FjJumqgwNe(object object_7)
        {
            try
            {
                GC.Collect();
                string string_ = string.Empty;
                Stream stream = null;
                if (object_7 is object[])
                {
                    object[] array = (object[])object_7;
                    if (array.Length == 2)
                    {
                        if (array[0] is string)
                        {
                            string_ = (string)array[0];
                        }
                        if (array[1] is Stream)
                        {
                            stream = (Stream)array[1];
                        }
                    }
                }
                method_359(stream, string_);
                stream.Dispose();
            }
            catch (Exception ex)
            {
                string text = "Distant Worlds could not save this game.";
                text += "\n\nError:\n\n";
                text += ex.ToString();
                text = text + "\n\nWorking Set: " + Environment.WorkingSet + " bytes";
                ShowMessageBox(text, "Cannot save game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                method_367();
            }
            finally
            {
                bool_0 = true;
            }
        }

        internal List<Type> method_358()
        {
            List<Type> list = new List<Type>();
            list.Add(typeof(BuiltObject));
            list.Add(typeof(BuiltObjectList));
            list.Add(typeof(Habitat));
            list.Add(typeof(HabitatList));
            list.Add(typeof(StellarObject));
            list.Add(typeof(StellarObjectList));
            list.Add(typeof(Empire));
            list.Add(typeof(Galaxy));
            list.Add(typeof(Cargo));
            list.Add(typeof(CargoList));
            list.Add(typeof(Resource));
            list.Add(typeof(ResourceList));
            list.Add(typeof(HabitatResource));
            list.Add(typeof(HabitatResourceList));
            list.Add(typeof(DistantWorlds.Types.Component));
            list.Add(typeof(ComponentList));
            list.Add(typeof(BuiltObjectComponent));
            list.Add(typeof(BuiltObjectComponentList));
            list.Add(typeof(Population));
            list.Add(typeof(PopulationList));
            list.Add(typeof(Troop));
            list.Add(typeof(TroopList));
            list.Add(typeof(Creature));
            list.Add(typeof(CreatureList));
            list.Add(typeof(DockingBay));
            list.Add(typeof(DockingBayList));
            list.Add(typeof(ConstructionYard));
            list.Add(typeof(ConstructionYardList));
            list.Add(typeof(Command));
            list.Add(typeof(Order));
            list.Add(typeof(OrderList));
            list.Add(typeof(Contract));
            list.Add(typeof(ContractList));
            list.Add(typeof(Design));
            list.Add(typeof(DesignList));
            list.Add(typeof(BuiltObjectMission));
            list.Add(typeof(BuiltObjectMissionList));
            list.Add(typeof(ConstructionQueue));
            list.Add(typeof(Manufacturer));
            list.Add(typeof(ManufacturerList));
            list.Add(typeof(ManufacturingQueue));
            list.Add(typeof(Race));
            list.Add(typeof(RaceList));
            list.Add(typeof(Blockade));
            list.Add(typeof(BlockadeList));
            list.Add(typeof(Character));
            list.Add(typeof(CharacterList));
            list.Add(typeof(CommandQueue));
            list.Add(typeof(ComponentDefinition));
            list.Add(typeof(ComponentDefinitionList));
            list.Add(typeof(ComponentResource));
            list.Add(typeof(ComponentResourceList));
            list.Add(typeof(DiplomaticRelation));
            list.Add(typeof(DiplomaticRelationList));
            list.Add(typeof(EmpireEvaluation));
            list.Add(typeof(EmpireEvaluationList));
            list.Add(typeof(ResearchArea));
            list.Add(typeof(ResearchAreaList));
            list.Add(typeof(Explosion));
            list.Add(typeof(ExplosionList));
            list.Add(typeof(Weapon));
            list.Add(typeof(WeaponList));
            list.Add(typeof(Ruin));
            list.Add(typeof(Point));
            list.Add(typeof(GalaxyLocation));
            list.Add(typeof(GalaxyLocationList));
            list.Add(typeof(ShipGroup));
            list.Add(typeof(ShipGroupList));
            list.Add(typeof(IntelligenceMission));
            list.Add(typeof(IntelligenceMissionList));
            list.Add(typeof(SystemVisibility));
            list.Add(typeof(SystemVisibilityList));
            list.Add(typeof(SystemInfo));
            list.Add(typeof(SystemInfoList));
            list.Add(typeof(EmpireMessage));
            list.Add(typeof(EmpireMessageList));
            list.Add(typeof(EmpireSystemSummary));
            list.Add(typeof(EmpireSystemSummaryList));
            return list;
        }

        private void method_359(Stream stream_0, string string_30)
        {
            //IL_0055: Unknown result type (might be due to invalid IL or missing references)
            //IL_005c: Expected O, but got Unknown
            GalaxySummary summary = _Game.Galaxy.GenerateSummary();
            GalaxySummary.WriteGalaxySummary(stream_0, summary, string_30);
            CompactSerializer compactSerializer = new CompactSerializer(typeof(Game), method_358());
            compactSerializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            ICryptoTransform transform = CreateEncryptor(byte_0, byte_1);
            CryptoStream cryptoStream = new CryptoStream(stream_0, transform, CryptoStreamMode.Write);
            ParallelDeflateOutputStream val = new ParallelDeflateOutputStream((Stream)(object)cryptoStream, (CompressionLevel)1, (CompressionStrategy)0, true);
            XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter((Stream)(object)val, null, null, ownsStream: false);
            compactSerializer.WriteObject(xmlDictionaryWriter, _Game);
            xmlDictionaryWriter.Close();
            ((Stream)(object)val).Close();
            cryptoStream.FlushFinalBlock();
            stream_0.Flush();
            stream_0.Close();
        }

        private void method_360(object object_7)
        {
            try
            {
                GC.Collect();
                Stream stream_ = null;
                if (object_7 is Stream)
                {
                    stream_ = (Stream)object_7;
                }
                LoadGameFromStream(stream_);
            }
            catch (SerializationException)
            {
                string text = TextResolver.GetText("This is not a valid Distant Worlds game file");
                ShowMessageBox(text, TextResolver.GetText("Cannot load file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                method_361();
            }
            catch (OutOfMemoryException)
            {
                string text2 = "There was not enough memory to load this Distant Worlds game. Please close all other open applications and try again.";
                text2 = text2 + "\n\nWorking Set: " + Environment.WorkingSet + " bytes";
                ShowMessageBox(text2, "Cannot load file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                method_361();
            }
            catch (Exception ex3)
            {
                string text3 = "Distant Worlds could not load this game.";
                text3 += "\n\nError:\n\n";
                text3 += ex3.ToString();
                text3 = text3 + "\n\nWorking Set: " + Environment.WorkingSet + " bytes";
                ShowMessageBox(text3, "Cannot load file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                method_361();
            }
            finally
            {
                bool_0 = true;
            }
        }

        private void method_361()
        {
            RecoverFromLoadSaveErrorDelegate method = method_362;
            Invoke(method);
        }

        private void method_362()
        {
            Application.UseWaitCursor = false;
            Cursor.Current = cursor_0;
            method_92();
            bool_4 = true;
            base.Enabled = true;
            Cursor.Current = cursor_0;
            base.KeyDown += Main_KeyDown;
            base.KeyUp += Main_KeyUp;
            pnlDetailInfo.ClearData();
            pnlHabitatInfo.ClearData();
            pnlBuiltObjectDetail.ClearData();
            pnlColonyHabitatInfo.ClearData();
            pnlSaveLoadProgress.Visible = false;
            method_382();
            method_357();
            VkjezoZtWa = true;
        }

        private void LoadGameFromStream(Stream stream_0)
        {
            //IL_0142: Unknown result type (might be due to invalid IL or missing references)
            //IL_0149: Expected O, but got Unknown
            Galaxy.CopyBackupGalaxyStaticDataToStatic();
            GalaxySummary galaxySummary = GalaxySummary.ReadGalaxySummary(stream_0);
            string text = galaxySummary.ThemeName;
            string empty = string_3;
            if (string.IsNullOrEmpty(text))
            {
                text = string.Empty;
            }
            if (string.IsNullOrEmpty(empty))
            {
                empty = string.Empty;
            }
            if (text != empty && delegate1_0 != null)
            {
                Application.DoEvents();
                string text2 = galaxySummary.ThemeName;
                if (string.IsNullOrEmpty(text2))
                {
                    text2 = "(" + TextResolver.GetText("Default") + ")";
                }
                Invoke(delegate2_0, string.Format(TextResolver.GetText("Switching to THEMENAME theme"), text2));
                Application.DoEvents();
                Invoke(delegate1_0, galaxySummary.ThemeName, true);
                Application.DoEvents();
                Invoke(delegate2_0, TextResolver.GetText("Loading the Galaxy..."));
                Application.DoEvents();
            }
            CompactSerializer compactSerializer = new CompactSerializer(typeof(Game), method_358());
            ICryptoTransform transform = CreateDecryptor(byte_0, byte_1);
            CryptoStream cryptoStream = new CryptoStream(stream_0, transform, CryptoStreamMode.Read);
            DeflateStream val = new DeflateStream((Stream)(object)cryptoStream, (CompressionMode)1, (CompressionLevel)1, true);
            val.BufferSize = 4194304;
            XmlDictionaryReaderQuotas max = XmlDictionaryReaderQuotas.Max;
            XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader((Stream)(object)val, max);
            _Game = (Game)compactSerializer.ReadObject(xmlDictionaryReader);
            Main._ExpModMain.FixAllDesignRepairTemplates(_Game, false);
            xmlDictionaryReader.Close();
            ((Stream)(object)val).Close();
            cryptoStream.Close();
            stream_0.Close();
            ((Stream)(object)val).Dispose();
            cryptoStream.Dispose();
            stream_0.Dispose();
        }

        private void method_363()
        {
            mainView.DisposePirateFlagTextures();
            Galaxy.SetGalaxyPhysicalDimensions(_Game.Galaxy.SectorWidth, _Game.Galaxy.SectorHeight);
            Galaxy.AssignGalaxyDataToStatic(_Game.Galaxy.ResourceSystem, _Game.Galaxy.PlanetaryFacilityDefinitions, _Game.Galaxy.ComponentDefinitions, _Game.Galaxy.FighterSpecifications, _Game.Galaxy.ResearchNodeDefinitions, _Game.Galaxy.Governments, _Game.Galaxy.RaceFamilies, _Game.Galaxy.Plagues);
            Galaxy.SetResearchRaceSpecialProjects(_Game.Galaxy.Races);
            Galaxy.SetResearchComponentMaxTechPoints(_Game.Galaxy.BaseTechCost);
            _Game.Galaxy.RebuildIndexes();
            _Game.Galaxy.IndependentEmpire.SetPirateRelationEmpires(_Game.Galaxy);
            _Game.Galaxy.IndependentEmpire.UpdateEmpireRefuellingLocations();
            foreach (Empire empire in _Game.Galaxy.Empires)
            {
                empire.SetDefaultsForLists();
                empire.ExtendLatestDesignsWithNewSubRoles();
                empire.SetPirateRelationEmpires(_Game.Galaxy);
                empire.EvaluateSystemLinks();
                empire.UpdateSystemFuelSourceStatus();
                empire.UpdateEmpireRefuellingLocations();
                empire.ReviewBuiltObjectWeaponsComponentValues();
                empire.ReviewUnpersistedColonyData();
            }
            foreach (Empire pirateEmpire in _Game.Galaxy.PirateEmpires)
            {
                pirateEmpire.SetDefaultsForLists();
                pirateEmpire.ExtendLatestDesignsWithNewSubRoles();
                pirateEmpire.SetPirateRelationEmpires(_Game.Galaxy);
                pirateEmpire.UpdateSystemFuelSourceStatus();
                pirateEmpire.UpdateEmpireRefuellingLocations();
                pirateEmpire.ReviewBuiltObjectWeaponsComponentValues();
                pirateEmpire.ReviewUnpersistedColonyData();
            }
            if (!string.IsNullOrEmpty(_Game.CustomizationSetName) && (string.IsNullOrEmpty(string_3) || _Game.CustomizationSetName != string_3))
            {
                method_66(_Game.CustomizationSetName, bool_28: false);
            }
            else
            {
                method_65(200);
            }
            method_56(Application.StartupPath + "\\images\\", _Game.CustomizationSetName, _Game.PlayerEmpire.DominantRace);
            _Game.Galaxy.ApplicationStartupPath = Application.StartupPath;
            _Game.Galaxy.CustomizationSetPath = GetCustomizationPath();
            _Game.PlayerEmpire.MessageRecipient = this;
            _Game.PlayerEmpire.AutomationAuthorizer = this;
            _Game.PlayerEmpire.EventMessageRecipient = this;
            _Game.Galaxy.SystemsUpdated += _Galaxy_SystemsUpdated;
            _Game.Galaxy.LocationPinged += method_365;
            _Game.Galaxy.GameEnd += Galaxy_GameEnd;
            _Game.Galaxy.RefreshView += _Galaxy_RefreshView;
            _Game.Galaxy.CharacterImageChanged += method_364;
            pnlDetailInfo.ClearData();
            lstMessages.ClearItems();
            method_83();
            list_5.Clear();
            int_22 = 0;
            habitatList_0.Clear();
            int_24 = 0;
            tutorial_0 = null;
            mainView.method_3();
            int_13 = (int)_Game.ViewX;
            int_14 = (int)_Game.ViewY;
            double_0 = _Game.ZoomFactor;
            mainView.double_14 = _Game.ZoomFactor;
            method_208(_Game.SelectedObject);
            method_258(gameOptions_0, _Game.PlayerEmpire);
            dateTime_1 = _Game.LastSystemMapUpdate;
            dateTime_2 = _Game.LastInfoPanelUpdate;
            method_149();
            bool_20 = true;
            btnEmpireSummary.Image = PrecacheScaledBitmap(_Game.PlayerEmpire.LargeFlagPicture, 50, 30);
            Cursor.Current = cursor_0;
            method_382();
            base.Enabled = true;
            Cursor.Current = cursor_0;
            base.KeyDown += Main_KeyDown;
            base.KeyUp += Main_KeyUp;
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused && !gameOptions_0.LoadedGamesPaused)
            {
                method_155();
            }
            else if (gameOptions_0.LoadedGamesPaused && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                method_154();
            }
            method_90();
        }

        private void method_364(object sender, CharacterImageChangedEventArgs e)
        {
            if (e.Character != null && characterImageCache_0 != null)
            {
                characterImageCache_0.ClearCharacterImages(e.Character);
            }
        }

        private void method_365(object sender, EventArgs e)
        {
            if (sender != null && sender is StellarObject)
            {
                StellarObject stellarObject = (StellarObject)sender;
                if (_Game != null && _Game.Galaxy != null && _Game.Galaxy.PlayerEmpire != null)
                {
                    _Game.Galaxy.PlayerEmpire.AddLocationHint(new Point((int)stellarObject.Xpos, (int)stellarObject.Ypos));
                }
            }
        }

        private void method_366()
        {
            while (!bool_0)
            {
                Application.DoEvents();
                Application.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                if (panel_0 == null)
                {
                    Thread.Sleep(500);
                }
            }
            Application.UseWaitCursor = false;
            Cursor.Current = cursor_0;
        }

        private void btnGameMenuLoad_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                {
                    method_154();
                }
                method_92();
                string text = GetGameSavesFolderCreateIfNeeded();
                if (gameOptions_0 != null && !string.IsNullOrEmpty(gameOptions_0.SaveGamePath))
                {
                    text = gameOptions_0.SaveGamePath;
                }
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = text;
                openFileDialog.Filter = TextResolver.GetText("Distant Worlds saved game files") + " (*.dwg)|*.dwg";
                openFileDialog.DefaultExt = "dwg";
                openFileDialog.Title = TextResolver.GetText("Load Distant Worlds game");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stream parameter;
                    if ((parameter = openFileDialog.OpenFile()) != null)
                    {
                        string_2 = openFileDialog.FileName;
                        if (gameOptions_0 != null)
                        {
                            DirectoryInfo directoryInfo = Directory.GetParent(string_2);
                            if (directoryInfo != null)
                            {
                                gameOptions_0.SaveGamePath = directoryInfo.FullName;
                            }
                        }
                        openFileDialog.Dispose();
                        method_357();
                        method_383(TextResolver.GetText("Loading the Galaxy..."));
                        Application.DoEvents();
                        Cursor.Current = Cursors.WaitCursor;
                        _Game.PlayerEmpire.MessageRecipient = null;
                        _Game.PlayerEmpire.AutomationAuthorizer = null;
                        _Game.PlayerEmpire.EventMessageRecipient = null;
                        _Game.Galaxy.SystemsUpdated -= _Galaxy_SystemsUpdated;
                        _Game.Galaxy.LocationPinged -= method_365;
                        _Game.Galaxy.GameEnd -= Galaxy_GameEnd;
                        _Game.Galaxy.RefreshView -= _Galaxy_RefreshView;
                        _Game.Galaxy.CharacterImageChanged -= method_364;
                        method_3();
                        method_64();
                        _Game = null;
                        GC.Collect();
                        base.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        Application.UseWaitCursor = true;
                        base.KeyDown -= Main_KeyDown;
                        base.KeyUp -= Main_KeyUp;
                        flag = true;
                        bool_0 = false;
                        thread_0 = new Thread(method_360, 8400000);
                        thread_0.Start(parameter);
                        method_366();
                        if (!VkjezoZtWa)
                        {
                            method_363();
                        }
                        VkjezoZtWa = false;
                        bool_0 = true;
                        Application.UseWaitCursor = false;
                    }
                    else
                    {
                        openFileDialog.Dispose();
                        method_357();
                    }
                }
                else
                {
                    openFileDialog.Dispose();
                    method_357();
                }
                if (!flag)
                {
                    method_382();
                    if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused)
                    {
                        method_155();
                    }
                    method_90();
                }
            }
            catch (Exception ex)
            {
                CrashDump(ex);
                throw;
            }
        }

        public static void CrashDump(Exception ex)
        {
            try
            {
                string path = GetGameFilesFolderCreateIfNeeded() + "DW_CrashDump.txt";
                using FileStream stream = new FileStream(path, FileMode.Create);
                using StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.WriteLine("Distant Worlds - Crash Dump - " + Application.ProductVersion);
                streamWriter.WriteLine();
                streamWriter.WriteLine(DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString());
                streamWriter.WriteLine();
                streamWriter.WriteLine();
                streamWriter.WriteLine(ex.ToString());
                streamWriter.WriteLine();
                streamWriter.WriteLine();
                if (ex is ApplicationException)
                {
                    streamWriter.WriteLine("Application info follows:");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(ex.Message);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine();
                }
                if (ex.InnerException != null)
                {
                    streamWriter.WriteLine("Further info follows:");
                    streamWriter.WriteLine();
                    if (ex.InnerException is ApplicationException)
                    {
                        streamWriter.WriteLine(ex.InnerException.Message);
                    }
                    else
                    {
                        streamWriter.WriteLine(ex.InnerException.ToString());
                    }
                    streamWriter.WriteLine();
                    streamWriter.WriteLine();
                }
                streamWriter.WriteLine("--- COMPLETE ---");
            }
            catch (Exception)
            {
                throw;
            }
            Class5._IsExiting = true;
        }

        public static void LogError(string text)
        {
            try
            {
                string path = GetGameFilesFolderCreateIfNeeded() + "DW_Errors.txt";
                if (Main._streamWriterErrors == null)
                {
                    lock (Main._errorLogLock)
                    {
                        if (Main._streamWriterErrors == null)
                        {
                            FileStream stream = new FileStream(path, FileMode.Append, FileAccess.ReadWrite);
                            Main._streamWriterErrors = (StreamWriter)TextWriter.Synchronized(new StreamWriter(stream));
                        }
                    }
                }
                Main._streamWriterErrors.WriteLineAsync($"{DateTime.Now.ToString()} {text}");
                Main._streamWriterErrors.FlushAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }



        private void method_367()
        {
            bool_0 = true;
            _Game.PlayerEmpire.MessageRecipient = this;
            _Game.PlayerEmpire.AutomationAuthorizer = this;
            _Game.PlayerEmpire.EventMessageRecipient = this;
            _Game.Galaxy.SystemsUpdated += _Galaxy_SystemsUpdated;
            _Game.Galaxy.LocationPinged += method_365;
            _Game.Galaxy.GameEnd += Galaxy_GameEnd;
            _Game.Galaxy.RefreshView += _Galaxy_RefreshView;
            _Game.Galaxy.CharacterImageChanged += method_364;
            method_382();
            method_357();
            base.Enabled = true;
            Cursor.Current = cursor_0;
            Cursor = cursor_0;
            base.KeyDown += Main_KeyDown;
            base.KeyUp += Main_KeyUp;
            if (galaxyTimeState_0 == GalaxyTimeState.Running)
            {
                method_155();
            }
            method_90();
        }

        private void btnGameMenuSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(string_2))
            {
                btnGameMenuSaveAs_Click(sender, e);
                return;
            }
            bool flag = false;
            FileStream fileStream = new FileStream(string_2, FileMode.Create);
            if (fileStream != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                galaxyTimeState_0 = _Game.Galaxy.TimeState;
                if (gameOptions_0 != null)
                {
                    DirectoryInfo directoryInfo = Directory.GetParent(string_2);
                    if (directoryInfo != null)
                    {
                        gameOptions_0.SaveGamePath = directoryInfo.FullName;
                    }
                }
                method_357();
                method_383(TextResolver.GetText("Saving the Galaxy..."));
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                {
                    method_154();
                    bool_11 = true;
                }
                method_92();
                _Game.PlayerEmpire.MessageRecipient = null;
                _Game.PlayerEmpire.AutomationAuthorizer = null;
                _Game.PlayerEmpire.EventMessageRecipient = null;
                _Game.Galaxy.SystemsUpdated -= _Galaxy_SystemsUpdated;
                _Game.Galaxy.LocationPinged -= method_365;
                _Game.Galaxy.GameEnd -= Galaxy_GameEnd;
                _Game.Galaxy.RefreshView -= _Galaxy_RefreshView;
                _Game.Galaxy.CharacterImageChanged -= method_364;
                _Game.ViewX = int_13;
                _Game.ViewY = int_14;
                _Game.ZoomFactor = double_0;
                _Game.LastSystemMapUpdate = dateTime_1;
                _Game.LastInfoPanelUpdate = dateTime_2;
                base.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                base.KeyDown -= Main_KeyDown;
                base.KeyUp -= Main_KeyUp;
                flag = true;
                bool_0 = false;
                thread_1 = new Thread(FjJumqgwNe, 33554432);
                thread_1.Start(new object[2] { _Game.CustomizationSetName, fileStream });
                bool_0 = false;
                method_366();
                if (!bool_1)
                {
                    method_367();
                }
                bool_1 = false;
                bool_0 = true;
                Application.UseWaitCursor = false;
                dateTime_6 = DateTime.Now;
            }
            if (!flag)
            {
                method_382();
                if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused)
                {
                    method_155();
                }
                method_90();
            }
        }

        private ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
        {
            var rijndael = Aes.Create();
            rijndael.KeySize = 128;
            rijndael.BlockSize = 128;
            rijndael.Padding = PaddingMode.Zeros;
            rijndael.Mode = CipherMode.CBC;
            return rijndael.CreateDecryptor(key, iv);
        }

        private ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
        {
            var rijndael = Aes.Create();
            rijndael.KeySize = 128;
            rijndael.BlockSize = 128;
            rijndael.Padding = PaddingMode.Zeros;
            rijndael.Mode = CipherMode.CBC;
            return rijndael.CreateEncryptor(key, iv);
        }

        private void btnGameMenuSaveAs_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string text = GetGameSavesFolderCreateIfNeeded();
            if (gameOptions_0 != null && !string.IsNullOrEmpty(gameOptions_0.SaveGamePath))
            {
                text = gameOptions_0.SaveGamePath;
            }
            if (!Directory.Exists(text))
            {
                try
                {
                    Directory.CreateDirectory(text);
                }
                catch (Exception)
                {
                    string string_ = string.Format(TextResolver.GetText("Could not create save game folder at X"), text);
                    MessageBoxEx messageBoxEx = method_371(string_, TextResolver.GetText("Could not set save game folder"), MessageBoxExIcon.Exclamation);
                    messageBoxEx.Show(this);
                    text = Application.ExecutablePath;
                }
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = text;
            saveFileDialog.Filter = TextResolver.GetText("Distant Worlds saved game files") + " (*.dwg)|*.dwg";
            saveFileDialog.DefaultExt = "dwg";
            saveFileDialog.Title = TextResolver.GetText("Save Distant Worlds game");
            string text2 = _Game.PlayerEmpire.Name + " " + Galaxy.ResolveStarDateDescription(_Game.Galaxy.CurrentStarDate, "-");
            text2 = text2.Replace(".", "");
            text2 = text2.Replace(",", "");
            text2 = text2.Replace("/", "");
            text2 = text2.Replace("\\", "");
            text2 = text2.Replace("'", "");
            text2 = text2.Replace("\"", "");
            text2 = text2.Replace("*", "");
            text2 = text2.Replace("!", "");
            text2 = text2.Replace("~", "");
            text2 = text2.Replace("$", "");
            text2 = text2.Replace("%", "");
            text2 = text2.Replace(":", "");
            text2 = text2.Replace(";", "");
            text2 = (saveFileDialog.FileName = text2.Replace("?", ""));
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream;
                if ((stream = saveFileDialog.OpenFile()) != null)
                {
                    string_2 = saveFileDialog.FileName;
                    if (gameOptions_0 != null)
                    {
                        DirectoryInfo directoryInfo = Directory.GetParent(string_2);
                        if (directoryInfo != null)
                        {
                            gameOptions_0.SaveGamePath = directoryInfo.FullName;
                        }
                    }
                    galaxyTimeState_0 = _Game.Galaxy.TimeState;
                    saveFileDialog.Dispose();
                    method_357();
                    method_383(TextResolver.GetText("Saving the Galaxy..."));
                    Application.DoEvents();
                    Cursor.Current = Cursors.WaitCursor;
                    if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                    {
                        method_154();
                        bool_11 = true;
                    }
                    method_92();
                    _Game.PlayerEmpire.MessageRecipient = null;
                    _Game.PlayerEmpire.AutomationAuthorizer = null;
                    _Game.PlayerEmpire.EventMessageRecipient = null;
                    _Game.Galaxy.SystemsUpdated -= _Galaxy_SystemsUpdated;
                    _Game.Galaxy.LocationPinged -= method_365;
                    _Game.Galaxy.GameEnd -= Galaxy_GameEnd;
                    _Game.Galaxy.RefreshView -= _Galaxy_RefreshView;
                    _Game.Galaxy.CharacterImageChanged -= method_364;
                    _Game.ViewX = int_13;
                    _Game.ViewY = int_14;
                    _Game.ZoomFactor = double_0;
                    _Game.LastSystemMapUpdate = dateTime_1;
                    _Game.LastInfoPanelUpdate = dateTime_2;
                    base.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                    base.KeyDown -= Main_KeyDown;
                    base.KeyUp -= Main_KeyUp;
                    flag = true;
                    bool_0 = false;
                    thread_1 = new Thread(FjJumqgwNe, 33554432);
                    thread_1.Start(new object[2] { _Game.CustomizationSetName, stream });
                    bool_0 = false;
                    method_366();
                    if (!bool_1)
                    {
                        method_367();
                    }
                    bool_1 = false;
                    bool_0 = true;
                    Application.UseWaitCursor = false;
                    dateTime_6 = DateTime.Now;
                }
                else
                {
                    saveFileDialog.Dispose();
                    method_357();
                }
            }
            else
            {
                saveFileDialog.Dispose();
                method_357();
            }
            if (!flag)
            {
                method_382();
                if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused)
                {
                    method_155();
                }
                method_90();
            }
        }

        private void btnGameMenuEditor_Click(object sender, EventArgs e)
        {
            method_357();
            method_92();
            if (!string.IsNullOrEmpty(_Game.EditorPassword))
            {
                if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                {
                    method_154();
                }
                method_506();
            }
            else
            {
                method_476();
            }
        }

        private void btnGameMenuOptions_Click(object sender, EventArgs e)
        {
            method_402();
        }

        private MessageBoxEx method_370(string string_30, string string_31)
        {
            return method_371(string_30, string_31, MessageBoxExIcon.Warning);
        }

        private MessageBoxEx method_371(string string_30, string string_31, MessageBoxExIcon messageBoxExIcon_0)
        {
            MessageBoxEx messageBoxEx = MessageBoxExManager.CreateMessageBox(null, font_6);
            string_30 = string_30.Replace("\n", Environment.NewLine);
            messageBoxEx.Text = string_30;
            messageBoxEx.Caption = string_31;
            messageBoxEx.AddButton(MessageBoxExButtons.Ok);
            messageBoxEx.Icon = messageBoxExIcon_0;
            return messageBoxEx;
        }

        internal MessageBoxEx method_372(string string_30, string string_31)
        {
            MessageBoxEx messageBoxEx = MessageBoxExManager.CreateMessageBox(null, font_6);
            string_30 = string_30.Replace("\n", Environment.NewLine);
            messageBoxEx.Text = string_30;
            messageBoxEx.Caption = string_31;
            messageBoxEx.AddButton(MessageBoxExButtons.Yes);
            messageBoxEx.AddButton(MessageBoxExButtons.No);
            messageBoxEx.Icon = MessageBoxExIcon.Question;
            return messageBoxEx;
        }

        internal bool method_373()
        {
            int int_ = 0;
            SystemParametersInfo_1(74u, 0u, ref int_, 0u);
            if (int_ > 0)
            {
                return true;
            }
            return false;
        }

        internal void method_374()
        {
            int int_ = 0;
            SystemParametersInfo_1(75u, 0u, ref int_, 1u);
        }

        internal void method_375()
        {
            int int_ = 0;
            SystemParametersInfo_1(75u, 1u, ref int_, 1u);
        }

        private void btnGameMenuQuit_Click(object sender, EventArgs e)
        {
            MessageBoxEx messageBoxEx = method_372(TextResolver.GetText("Are you sure that you wish to exit this game?"), TextResolver.GetText("Exit Distant Worlds"));
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                musicPlayer_0.Stop();
                musicPlayer_1.Stop();
                string filename = GetGameFilesFolderCreateIfNeeded() + "automationPrefs";
                MessageBoxExManager.WriteSavedResponses(filename);
                method_257();
                method_255();
                ToggleScreenSaverActive(active: true);
                if (!bool_2)
                {
                    method_374();
                }
                Hide();
                Environment.Exit(-1);
            }
            Focus();
        }

        private Bitmap method_376(Bitmap bitmap_225, float float_2)
        {
            Bitmap bitmap = new Bitmap(bitmap_225.Width, bitmap_225.Height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle destRect = new Rectangle(0, 0, bitmap_225.Width, bitmap_225.Height);
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5] { 0f, 0f, 0f, float_2, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(newColorMatrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(bitmap_225, destRect, 0f, 0f, bitmap_225.Width, bitmap_225.Height, GraphicsUnit.Pixel, imageAttributes);
            return bitmap;
        }

        private void method_377(System.Windows.Forms.Panel panel_1)
        {
            if (Environment.ProcessorCount > 1)
            {
                bitmap_0 = PrecacheScaledBitmap(bitmap_47, 270, 270);
                panel_0 = panel_1;
                size_0 = picSaveLoadGalaxy.Size;
                float_0 = (float)Math.PI * 2f;
                timer_0.AutoReset = false;
                timer_0.Interval = 75.0;
                timer_0.Start();
            }
            else
            {
                picSaveLoadGalaxy.Image = bitmap_47;
            }
        }

        private void timer_0_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer_0.Stop();
            if (panel_0 != null)
            {
                float_0 -= (float)Math.PI / 160f;
                Bitmap bitmap = method_381(bitmap_0, float_0, bool_28: false);
                Bitmap bitmap2 = CropImageToSize(bitmap, size_0);
                if (bitmap2 != bitmap)
                {
                    bitmap?.Dispose();
                }
                if (bitmap2 != null && bitmap2.PixelFormat != 0)
                {
                    Invoke(delegate0_0, bitmap2);
                }
                Application.DoEvents();
            }
            timer_0.Start();
        }

        private void method_378(Bitmap bitmap_225)
        {
            Image image = picSaveLoadGalaxy.Image;
            picSaveLoadGalaxy.SizeMode = PictureBoxSizeMode.CenterImage;
            picSaveLoadGalaxy.Image = bitmap_225;
            image?.Dispose();
        }

        private void method_379()
        {
            timer_0.Stop();
            panel_0 = null;
            picSaveLoadGalaxy.Image = null;
        }

        private Bitmap method_380(Image image_0, float float_2)
        {
            return method_381(image_0, float_2, bool_28: false);
        }

        private Bitmap method_381(Image image_0, float float_2, bool bool_28)
        {
            if (image_0 == null)
            {
                throw new ArgumentNullException("image");
            }
            float num = image_0.Width;
            float num2 = image_0.Height;
            float_2 *= -1f;
            float_2 *= 57.29578f;
            float_2 %= 360f;
            if ((double)float_2 < 0.0)
            {
                float_2 += 360f;
            }
            PointF[] array = new PointF[4];
            array[1].X = num;
            array[2].X = num;
            array[2].Y = num2;
            array[3].Y = num2;
            using Matrix matrix = new Matrix();
            matrix.Rotate(float_2);
            matrix.TransformPoints(array);
            double num3 = double.MinValue;
            double num4 = double.MinValue;
            double num5 = double.MaxValue;
            double num6 = double.MaxValue;
            PointF[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                PointF pointF = array2[i];
                num3 = Math.Max(num3, pointF.X);
                num5 = Math.Min(num5, pointF.X);
                num4 = Math.Max(num4, pointF.Y);
                num6 = Math.Min(num6, pointF.Y);
            }
            double val = Math.Ceiling(num3 - num5);
            double val2 = Math.Ceiling(num4 - num6);
            val = Math.Max(1.0, val);
            val2 = Math.Max(1.0, val2);
            Bitmap bitmap = new Bitmap((int)val, (int)val2);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (bool_28)
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                }
                else
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.Bilinear;
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                }
                PointF point = new PointF((float)(val / 2.0), (float)(val2 / 2.0));
                PointF point2 = new PointF(point.X - num / 2f, point.Y - num / 2f);
                matrix.Reset();
                matrix.RotateAt(float_2, point);
                graphics.Transform = matrix;
                graphics.DrawImage(image_0, point2);
            }
            return bitmap;
        }

        public Bitmap CropImageToSize(Bitmap image, Size size)
        {
            if (image.Width <= size.Width && image.Height <= size.Height)
            {
                return image;
            }
            int num = Math.Min(image.Width, size.Width);
            int num2 = Math.Min(image.Height, size.Height);
            Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.None;
            float num3 = (float)(num - image.Width) / 2f;
            float num4 = (float)(num2 - image.Height) / 2f;
            PointF point = new PointF(num3, num4);
            graphics.DrawImage(image, point);
            return bitmap;
        }

        private void method_382()
        {
            method_379();
            pnlSaveLoadProgress.Visible = false;
            pnlSaveLoadProgress.SendToBack();
        }

        private void method_383(string string_30)
        {
            pnlSaveLoadProgress.Size = new Size(320, 330);
            pnlSaveLoadProgress.Location = new Point((mainView.Width - pnlSaveLoadProgress.Width) / 2, (mainView.Height - pnlSaveLoadProgress.Height) / 2);
            pnlSaveLoadProgress.Font = new Font("Verdana", 11f, FontStyle.Bold);
            Point location = new Point((pnlSaveLoadProgress.Width - bitmap_47.Width) / 2, (pnlSaveLoadProgress.Height - bitmap_47.Height) / 2 + 15);
            picSaveLoadGalaxy.Size = new Size(bitmap_47.Width, bitmap_47.Height);
            picSaveLoadGalaxy.Location = location;
            picSaveLoadGalaxy.BringToFront();
            int num = 0;
            int num2 = 0;
            using (Graphics graphics = lblSaveLoadMessage.CreateGraphics())
            {
                num = (int)graphics.MeasureString(string_30, font_2, 320, StringFormat.GenericTypographic).Width;
                num2 = (int)graphics.MeasureString(string_30, font_2, 320, StringFormat.GenericTypographic).Height;
            }
            lblSaveLoadMessage.Size = new Size(num + 2, num2 + 2);
            lblSaveLoadMessage.Location = new Point((pnlSaveLoadProgress.Width - num) / 2, 13);
            lblSaveLoadMessage.Font = font_2;
            lblSaveLoadMessage.ForeColor = Color.White;
            lblSaveLoadMessage.BackColor = Color.Transparent;
            lblSaveLoadMessage.Text = string_30;
            lblSaveLoadMessage.BringToFront();
            pnlSaveLoadProgress.BringToFront();
            pnlSaveLoadProgress.Visible = true;
            method_377(pnlSaveLoadProgress);
        }

        private void btnGameMenuStartMenu_Click(object sender, EventArgs e)
        {
            MessageBoxEx messageBoxEx = method_372(TextResolver.GetText("Are you sure that you wish to exit to the main menu?"), TextResolver.GetText("Exit to main menu"));
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                method_92();
                bool_4 = true;
                _Game.SelectedObject = null;
                int_28 = 0;
                int_29 = 0;
                tutorial_0 = null;
                list_5.Clear();
                int_22 = 0;
                habitatList_0.Clear();
                int_24 = 0;
                mainView.method_3();
                pnlDetailInfo.ClearData();
                pnlHabitatInfo.ClearData();
                hoverPanel_0.ClearData();
                pnlBuiltObjectDetail.ClearData();
                pnlColonyHabitatInfo.ClearData();
                picSystem.Extinguish();
                picSystemMap.Extinguish();
                if (pnlGameEditorPassword.Visible)
                {
                    method_505();
                }
                if (pnlGameEditor.Visible)
                {
                    method_478(bool_28: true);
                }
                if (_Game != null)
                {
                    _Game.PlayerEmpire.MessageRecipient = null;
                    _Game.PlayerEmpire.AutomationAuthorizer = null;
                    _Game.PlayerEmpire.EventMessageRecipient = null;
                    _Game.Galaxy.SystemsUpdated -= _Galaxy_SystemsUpdated;
                    _Game.Galaxy.LocationPinged -= method_365;
                    _Game.Galaxy.GameEnd -= Galaxy_GameEnd;
                    _Game.Galaxy.RefreshView -= _Galaxy_RefreshView;
                    _Game.Galaxy.CharacterImageChanged -= method_364;
                    method_3();
                    method_64();
                }
                Galaxy.CopyBackupGalaxyStaticDataToStatic();
                method_65(200);
                method_357();
            }
            Focus();
        }

        private void btnGameMenuCancel_Click(object sender, EventArgs e)
        {
            method_357();
        }

        private void btnDesignsEdit_Click(object sender, EventArgs e)
        {
            Design selectedDesign = ctlDesignsList.SelectedDesign;
            if (_Game.PlayerEmpire.ControlDesigns && _Game.PlayerEmpire.CheckDesignSubRoleShouldBeUpgraded(selectedDesign.SubRole) && GenerateAutomationMessageBox(TextResolver.GetText("Ship Design")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                _Game.PlayerEmpire.ControlDesigns = false;
            }
            design_2 = null;
            if (selectedDesign == null)
            {
                return;
            }
            int num = 0;
            foreach (BuiltObject builtObject in selectedDesign.Empire.BuiltObjects)
            {
                if (builtObject.Design == selectedDesign)
                {
                    num++;
                }
            }
            foreach (BuiltObject privateBuiltObject in selectedDesign.Empire.PrivateBuiltObjects)
            {
                if (privateBuiltObject.Design == selectedDesign)
                {
                    num++;
                }
            }
            design_1 = selectedDesign.Clone();
            if (num > 0)
            {
                string_16 = "view";
            }
            else if (selectedDesign.Empire != null && selectedDesign.Empire.CheckDesignInUseForConstructionOrRetrofits(selectedDesign))
            {
                string_16 = "view";
            }
            else
            {
                string_16 = "edit";
            }
            OpenDesignEditor(selectedDesign);
            if (string_16 == "view")
            {
                string string_ = TextResolver.GetText("Cannot edit this design");
                string string_2 = TextResolver.GetText("You cannot edit this design because it is already in use");
                MessageBoxEx messageBoxEx = method_371(string_2, string_, MessageBoxExIcon.Information);
                messageBoxEx.Show(this);
            }
        }

        private void btnDesignsCancel_Click(object sender, EventArgs e)
        {
            design_2 = null;
            if (design_1 != null && design_0 != null)
            {
                design_0.Components = design_1.Components;
                design_0.TacticsInvasion = design_1.TacticsInvasion;
                design_0.TacticsStrongerShips = design_1.TacticsStrongerShips;
                design_0.TacticsWeakerShips = design_1.TacticsWeakerShips;
                design_0.FleeWhen = design_1.FleeWhen;
                design_0.Stance = design_1.Stance;
                design_0.ReDefine();
                design_1 = null;
            }
            method_293();
        }

        private void cmbDesignsPicture_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrepareDesignForEditor();
        }

        private void method_384(int int_64)
        {
            Bitmap[] imagesSmall = builtObjectImageCache_0.GetImagesSmall();
            if (int_64 >= 0 && int_64 < imagesSmall.Length)
            {
                Bitmap image = imagesSmall[int_64];
                image = PrepareBuiltObjectImage(null, image, _Game.PlayerEmpire.MainColor, _Game.PlayerEmpire.SecondaryColor, 1.0, 1);
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                picDesignPicture.SizeMode = PictureBoxSizeMode.Zoom;
                picDesignPicture.Image = image;
            }
            else
            {
                picDesignPicture.Image = null;
            }
        }

        private void cmbDesignsPicture_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 119;
        }

        private void cmbDesignsPicture_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Bitmap[] imagesSmall = builtObjectImageCache_0.GetImagesSmall();
            if (e.Index >= 0 && e.Index < imagesSmall.Length)
            {
                Bitmap original = builtObjectImageCache_0.ObtainImage(e.Index);
                Bitmap bitmap = new Bitmap(original);
                bitmap.MakeTransparent(Color.Black);
                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                int num = e.Bounds.Height - 2;
                double num2 = (double)num / (double)bitmap.Height;
                int num3 = (int)((double)bitmap.Width * num2);
                int num4 = (e.Bounds.Width - num3) / 2;
                int num5 = e.Bounds.Y + 1;
                Rectangle rect = new Rectangle(num4, num5, num3, num);
                e.Graphics.DrawImage(bitmap, rect);
                e.DrawFocusRectangle();
            }
        }

        private void txtDesignName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDesignName.Text.Trim()))
            {
                lblDesignDetailTitle.Text = TextResolver.GetText("Edit Design") + ": " + txtDesignName.Text;
                design_0.Name = txtDesignName.Text;
                PrepareDesignForEditor();
                List<string> list_ = new List<string>();
                List<string> list_2 = new List<string>();
                GetDesignWarningMessages(design_0, out list_, out list_2);
                pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
            }
        }

        private void method_385(Design design_3)
        {
            string text = "0";
            string text2 = "0";
            if (design_3 != null)
            {
                text = design_3.CalculateCurrentPurchasePrice(_Game.Galaxy).ToString("#####0");
                text2 = design_3.CalculateMaintenanceCosts(_Game.Galaxy, design_3.Empire).ToString("####0");
            }
            lblDesignDetailPurchaseCost.Text = TextResolver.GetText("Purchase Cost") + ": " + text;
            lblDesignDetailMaintenanceCost.Text = TextResolver.GetText("Maintenance Cost") + ": " + text2;
        }

        private void btnAddComponentToDesignMultiple_Click(object sender, EventArgs e)
        {
            _ = ctlDesignComponentToolbox.Grid.SelectedRows[0].Index;
            DistantWorlds.Types.Component selectedComponent = ctlDesignComponentToolbox.SelectedComponent;
            if (selectedComponent == null)
            {
                return;
            }
            ComponentList components = ctlDesignComponents.Components;
            for (int i = 0; i < 5; i++)
            {
                int num = components.LastIndexById(selectedComponent);
                if (num >= 0 && components.Count > num + 1)
                {
                    components.Insert(num + 1, selectedComponent);
                }
                else
                {
                    components.Add(selectedComponent);
                }
            }
            design_0.Components = components;
            design_0.ReDefine();
            method_292(design_0, bool_28: false);
            ctlDesignComponents.SelectComponent(selectedComponent);
            List<string> list_ = new List<string>();
            List<string> list_2 = new List<string>();
            GetDesignWarningMessages(design_0, out list_, out list_2);
            pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
        }

        private void btnAddComponentToDesign_Click(object sender, EventArgs e)
        {
            _ = ctlDesignComponentToolbox.Grid.SelectedRows[0].Index;
            DistantWorlds.Types.Component selectedComponent = ctlDesignComponentToolbox.SelectedComponent;
            if (selectedComponent != null)
            {
                ComponentList components = ctlDesignComponents.Components;
                int num = components.LastIndexById(selectedComponent);
                if (num >= 0 && components.Count > num + 1)
                {
                    components.Insert(num + 1, selectedComponent);
                }
                else
                {
                    components.Add(selectedComponent);
                }
                design_0.ImageScalingFactor = (float)numDesignImageScalingAmount.Value;
                design_0.ImageScalingType = cmbDesignImageScalingMode.SelectedScalingMode;
                design_0.Components = components;
                design_0.ReDefine();
                method_292(design_0, bool_28: false);
                ctlDesignComponents.SelectComponent(selectedComponent);
                List<string> list_ = new List<string>();
                List<string> list_2 = new List<string>();
                GetDesignWarningMessages(design_0, out list_, out list_2);
                pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
            }
        }

        private void btnRemoveComponentFromDesignMultiple_Click(object sender, EventArgs e)
        {
            if (ctlDesignComponentToolbox.SelectedComponent != null)
            {
                _ = ctlDesignComponentToolbox.Grid.SelectedRows[0].Index;
            }
            DistantWorlds.Types.Component selectedComponent = ctlDesignComponents.SelectedComponent;
            if (selectedComponent == null)
            {
                return;
            }
            int index = ctlDesignComponents.Grid.SelectedRows[0].Index;
            int num = ctlDesignComponents.SelectedAmount;
            for (int i = 0; i < 5; i++)
            {
                ctlDesignComponents.Components.RemoveByComponentId(selectedComponent);
                num--;
                if (num < 0)
                {
                    break;
                }
            }
            ComponentList components = ctlDesignComponents.Components.Clone();
            design_0.Components = components;
            design_0.ReDefine();
            method_292(design_0, bool_28: false);
            int num2 = index - 1;
            if (ctlDesignComponents.SummarizedMode && num > 0)
            {
                num2 = index;
            }
            if (num2 < 0 && ctlDesignComponents.Grid.Rows.Count > 0)
            {
                num2 = 0;
            }
            if (num2 >= 0)
            {
                ctlDesignComponents.SelectRow(num2);
            }
            List<string> list_ = new List<string>();
            List<string> list_2 = new List<string>();
            GetDesignWarningMessages(design_0, out list_, out list_2);
            pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
        }

        private void btnRemoveComponentFromDesign_Click(object sender, EventArgs e)
        {
            if (ctlDesignComponentToolbox.SelectedComponent != null)
            {
                _ = ctlDesignComponentToolbox.Grid.SelectedRows[0].Index;
            }
            DistantWorlds.Types.Component selectedComponent = ctlDesignComponents.SelectedComponent;
            if (selectedComponent != null)
            {
                int index = ctlDesignComponents.Grid.SelectedRows[0].Index;
                int selectedAmount = ctlDesignComponents.SelectedAmount;
                ctlDesignComponents.Components.Remove(selectedComponent);
                ComponentList components = ctlDesignComponents.Components.Clone();
                design_0.Components = components;
                design_0.ReDefine();
                method_292(design_0, bool_28: false);
                int num = index - 1;
                if (ctlDesignComponents.SummarizedMode && selectedAmount > 1)
                {
                    num = index;
                }
                if (num < 0 && ctlDesignComponents.Grid.Rows.Count > 0)
                {
                    num = 0;
                }
                if (num >= 0)
                {
                    ctlDesignComponents.SelectRow(num);
                }
                List<string> list_ = new List<string>();
                List<string> list_2 = new List<string>();
                GetDesignWarningMessages(design_0, out list_, out list_2);
                pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
            }
        }
        private void btnRepairPriorityEdit_Click(object sender, EventArgs e)
        {
            _ExpModMain.SelectRepairPriority(null);
        }



    }

}