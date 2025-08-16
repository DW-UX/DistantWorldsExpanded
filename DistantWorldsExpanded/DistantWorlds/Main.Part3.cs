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

namespace DistantWorlds
{

    public partial class Main
    {


        private void btnGalacticHistory_Click(object sender, EventArgs e)
        {
            if (pnlMessageHistory.Visible)
            {
                method_529();
            }
            else
            {
                method_528("galactichistory");
            }
        }

        private void cmbMessageHistoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            method_542();
            method_531(ctlMessageHistoryMessages.SelectedMessage);
        }

        private void method_574(BuiltObjectList builtObjectList_1)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlRetrofit.Size = new Size(400, 279);
            pnlRetrofit.Location = new Point((mainView.Width - pnlRetrofit.Width) / 2, (mainView.Height - pnlRetrofit.Height) / 2);
            pnlRetrofit.DoLayout();
            lblRetrofitCost.Visible = true;
            lblRetrofitCost.Location = new Point(80, 10);
            double num = _Game.PlayerEmpire.CalculateRetrofitCost(builtObjectList_1, null);
            string text = TextResolver.GetText("Total retrofit cost") + ": " + num.ToString("###,###,##0") + " " + TextResolver.GetText("credits");
            if (num > _Game.PlayerEmpire.StateMoney)
            {
                text = text + "  (" + TextResolver.GetText("Cannot afford this retrofit") + ")";
            }
            lblRetrofitCost.Text = text;
            if (num > _Game.PlayerEmpire.StateMoney)
            {
                btnRetrofitGo.Enabled = false;
            }
            else
            {
                btnRetrofitGo.Enabled = true;
            }
            lblRetrofitDesignLabel.Location = new Point(10, 29);
            cmbRetrofitDesign.Size = new Size(295, 21);
            cmbRetrofitDesign.Location = new Point(80, 25);
            cmbRetrofitDesign.BackColor = Color.FromArgb(48, 48, 64);
            DesignList designs = null;
            BuiltObjectSubRole builtObjectSubRole_ = BuiltObjectSubRole.Undefined;
            if (!method_581(builtObjectList_1, out builtObjectSubRole_))
            {
                cmbRetrofitDesign.Enabled = false;
            }
            else
            {
                cmbRetrofitDesign.Enabled = true;
                List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                list.Add(builtObjectSubRole_);
                designs = _Game.PlayerEmpire.Designs.GetBuildableDesignsBySubRoles(list, _Game.PlayerEmpire);
            }
            cmbRetrofitDesign.BindData(designs, builtObjectImageCache_0.GetImagesSmall(), _Game.Galaxy.IndependentEmpire);
            lblRetrofitMessage.Location = new Point(10, 60);
            lblRetrofitMessage.MaximumSize = new Size(380, 100);
            string text2 = "";
            text2 = method_576(builtObjectList_1);
            lblRetrofitMessage.Text = text2;
            btnRetrofitGo.Size = new Size(365, 40);
            btnRetrofitGo.Location = new Point(10, 165);
            pnlRetrofit.Visible = true;
            pnlRetrofit.BringToFront();
        }

        private void cmbRetrofitDesign_SelectedIndexChanged(object sender, EventArgs e)
        {
            Design selectedDesign = cmbRetrofitDesign.SelectedDesign;
            double num = _Game.PlayerEmpire.CalculateRetrofitCost(ctlBuiltObjectList.SelectedBuiltObjects, selectedDesign);
            string text = TextResolver.GetText("Total retrofit cost") + ": " + num.ToString("###,###,##0") + " " + TextResolver.GetText("credits");
            if (num > _Game.PlayerEmpire.StateMoney)
            {
                text = text + "  (" + TextResolver.GetText("Cannot afford this retrofit") + ")";
            }
            lblRetrofitCost.Text = text;
            if (num > _Game.PlayerEmpire.StateMoney)
            {
                btnRetrofitGo.Enabled = false;
            }
            else
            {
                btnRetrofitGo.Enabled = true;
            }
        }

        private void method_575()
        {
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            Design selectedDesign = cmbRetrofitDesign.SelectedDesign;
            BuiltObjectSubRole builtObjectSubRole_ = BuiltObjectSubRole.Undefined;
            if (method_581(selectedBuiltObjects, out builtObjectSubRole_))
            {
                for (int i = 0; i < selectedBuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = selectedBuiltObjects[i];
                    if (method_579(builtObject) && builtObject.Empire != null)
                    {
                        builtObject.Empire.AssignRetrofitMission(builtObject, selectedDesign, null, forceUseOfYard: true);
                    }
                }
            }
            else
            {
                for (int j = 0; j < selectedBuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = selectedBuiltObjects[j];
                    if (method_579(builtObject2))
                    {
                        selectedDesign = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(builtObject2.SubRole, builtObject2.ParentHabitat);
                        if (selectedDesign != null)
                        {
                            _Game.PlayerEmpire.AssignRetrofitMission(builtObject2, selectedDesign, null, forceUseOfYard: true);
                        }
                    }
                }
            }
            foreach (DataGridViewRow selectedRow in ctlBuiltObjectList.Grid.SelectedRows)
            {
                BuiltObject builtObject3 = ctlBuiltObjectList.ResolveBuiltObject(selectedRow);
                if (builtObject3 != null)
                {
                    string value = "(" + TextResolver.GetText("None") + ")";
                    if (builtObject3.Mission != null && builtObject3.Mission.Type != 0)
                    {
                        value = Galaxy.ResolveDescription(builtObject3.Mission.Type);
                    }
                    selectedRow.Cells[4].Value = value;
                }
            }
        }

        private string method_576(BuiltObjectList builtObjectList_1)
        {
            string text = string.Empty;
            BuiltObjectSubRole builtObjectSubRole_ = BuiltObjectSubRole.Undefined;
            if (!method_580(builtObjectList_1))
            {
                text = text + TextResolver.GetText("Some of the selected items cannot be retrofitted") + "\n\n";
            }
            if (!method_581(builtObjectList_1, out builtObjectSubRole_))
            {
                text = text + TextResolver.GetText("The selected items are not of the same type, thus you cannot retrofit them to a specific design") + "\n\n";
            }
            if (method_577(builtObjectList_1))
            {
                text = text + TextResolver.GetText("Some of the selected items must be retrofitted at a colony") + "\n\n";
            }
            return text;
        }

        private bool method_577(BuiltObjectList builtObjectList_1)
        {
            int num = 0;
            while (true)
            {
                if (num < builtObjectList_1.Count)
                {
                    if (method_578(builtObjectList_1[num]))
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return false;
            }
            return true;
        }

        private bool method_578(BuiltObject builtObject_8)
        {
            if (builtObject_8.Role != BuiltObjectRole.Base && builtObject_8.SubRole != BuiltObjectSubRole.ColonyShip && builtObject_8.SubRole != BuiltObjectSubRole.ConstructionShip && builtObject_8.SubRole != BuiltObjectSubRole.ResupplyShip)
            {
                return false;
            }
            return true;
        }

        private bool method_579(BuiltObject builtObject_8)
        {
            if (builtObject_8.Role == BuiltObjectRole.Base)
            {
            }
            if (builtObject_8.Owner == null && builtObject_8.Role != BuiltObjectRole.Base)
            {
                return false;
            }
            if (builtObject_8.RetrofitDesign != null)
            {
                return false;
            }
            return true;
        }

        private bool method_580(BuiltObjectList builtObjectList_1)
        {
            int num = 0;
            while (true)
            {
                if (num < builtObjectList_1.Count)
                {
                    if (!method_579(builtObjectList_1[num]))
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return true;
            }
            return false;
        }

        private bool method_581(BuiltObjectList builtObjectList_1, out BuiltObjectSubRole builtObjectSubRole_0)
        {
            builtObjectSubRole_0 = BuiltObjectSubRole.Undefined;
            bool flag = false;
            if (builtObjectList_1.Count > 1)
            {
                for (int i = 0; i < builtObjectList_1.Count; i++)
                {
                    if (builtObjectList_1[i].SubRole == BuiltObjectSubRole.Outpost || builtObjectList_1[i].SubRole == BuiltObjectSubRole.SmallSpacePort || builtObjectList_1[i].SubRole == BuiltObjectSubRole.MediumSpacePort || builtObjectList_1[i].SubRole == BuiltObjectSubRole.LargeSpacePort)
                    {
                        flag = true;
                    }
                }
                int num = 0;
                while (true)
                {
                    if (num < builtObjectList_1.Count)
                    {
                        if (!flag || builtObjectList_1[num].SubRole == BuiltObjectSubRole.Outpost || builtObjectList_1[num].SubRole == BuiltObjectSubRole.SmallSpacePort || builtObjectList_1[num].SubRole == BuiltObjectSubRole.MediumSpacePort || builtObjectList_1[num].SubRole == BuiltObjectSubRole.LargeSpacePort)
                        {
                            if (builtObjectSubRole_0 != 0 && builtObjectList_1[num].SubRole != builtObjectSubRole_0)
                            {
                                break;
                            }
                            builtObjectSubRole_0 = builtObjectList_1[num].SubRole;
                            num++;
                            continue;
                        }
                        return false;
                    }
                    return true;
                }
                return false;
            }
            if (builtObjectList_1.Count == 1)
            {
                builtObjectSubRole_0 = builtObjectList_1[0].SubRole;
            }
            return true;
        }

        private void method_582()
        {
            pnlRetrofit.SendToBack();
            pnlRetrofit.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void pnlRetrofit_CloseButtonClicked(object sender, EventArgs e)
        {
            method_582();
        }

        private void btnBuiltObjectRetrofitSelected_Click(object sender, EventArgs e)
        {
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            if (selectedBuiltObjects != null && selectedBuiltObjects.Count > 0)
            {
                method_574(selectedBuiltObjects);
            }
        }

        private void btnRetrofitGo_Click(object sender, EventArgs e)
        {
            method_575();
            method_582();
        }

        private void btnBuiltObjectRepairSelected_Click(object sender, EventArgs e)
        {
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            for (int i = 0; i < selectedBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = selectedBuiltObjects[i];
                if (builtObject.DamagedComponentCount > 0 && builtObject.TopSpeed > 0)
                {
                    StellarObject stellarObject = builtObject.Empire.FindNearestShipYard(builtObject, canRepairOrBuild: true, includeVerySmallYards: true);
                    if (stellarObject != null)
                    {
                        builtObject.AssignMission(BuiltObjectMissionType.Repair, stellarObject, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                        builtObject.IsAutoControlled = false;
                    }
                }
            }
            foreach (DataGridViewRow selectedRow in ctlBuiltObjectList.Grid.SelectedRows)
            {
                BuiltObject builtObject2 = ctlBuiltObjectList.ResolveBuiltObject(selectedRow);
                if (builtObject2 != null && builtObject2.Role == BuiltObjectRole.Military)
                {
                    string value = "(" + TextResolver.GetText("None") + ")";
                    if (builtObject2.Mission != null && builtObject2.Mission.Type != 0)
                    {
                        value = Galaxy.ResolveDescription(builtObject2.Mission.Type);
                    }
                    selectedRow.Cells[4].Value = value;
                }
            }
        }

        private void btnBuiltObjectRefuelSelected_Click(object sender, EventArgs e)
        {
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            for (int i = 0; i < selectedBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = selectedBuiltObjects[i];
                if (builtObject.TopSpeed > 0 && builtObject.Owner != null && builtObject.Role != BuiltObjectRole.Base)
                {
                    ResourceList fuelTypes = builtObject.DetermineFuelRequired(setFuelLevelToZero: true);
                    StellarObject stellarObject = _Game.Galaxy.FastFindNearestRefuellingPoint(builtObject.Xpos, builtObject.Ypos, fuelTypes, builtObject.ActualEmpire, builtObject);
                    if (stellarObject != null)
                    {
                        builtObject.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                    }
                }
            }
            foreach (DataGridViewRow selectedRow in ctlBuiltObjectList.Grid.SelectedRows)
            {
                BuiltObject builtObject2 = ctlBuiltObjectList.ResolveBuiltObject(selectedRow);
                if (builtObject2 != null)
                {
                    string value = "(" + TextResolver.GetText("None") + ")";
                    if (builtObject2.Mission != null && builtObject2.Mission.Type != 0)
                    {
                        value = Galaxy.ResolveDescription(builtObject2.Mission.Type);
                    }
                    selectedRow.Cells[4].Value = value;
                }
            }
        }

        private void btnBuiltObjectScrapSelected_Click(object sender, EventArgs e)
        {
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            if (selectedBuiltObjects == null || selectedBuiltObjects.Count <= 0)
            {
                return;
            }
            string string_ = TextResolver.GetText("Scrapping ships and bases permanently and immediately removes them from the game");
            MessageBoxEx messageBoxEx = method_372(string_, TextResolver.GetText("Scrap selected ships and bases?"));
            if (!(messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes"))
            {
                return;
            }
            BuiltObject builtObject_ = null;
            if (ctlBuiltObjectList.Grid.SelectedRows != null && ctlBuiltObjectList.Grid.SelectedRows.Count > 0)
            {
                int num = ctlBuiltObjectList.Grid.Rows.IndexOf(ctlBuiltObjectList.Grid.SelectedRows[0]);
                if (num > 0)
                {
                    builtObject_ = ctlBuiltObjectList.ResolveBuiltObject(ctlBuiltObjectList.Grid.Rows[num - 1]);
                }
                else if (num == 0 && ctlBuiltObjectList.Grid.Rows.Count > 1)
                {
                    builtObject_ = ctlBuiltObjectList.ResolveBuiltObject(ctlBuiltObjectList.Grid.Rows[num + 1]);
                }
            }
            for (int i = 0; i < selectedBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = selectedBuiltObjects[i];
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
                            constructionYard2.ShipUnderConstruction.CompleteTeardown(_Game.Galaxy, removeFromEmpire: true);
                            constructionYard2.ShipUnderConstruction = null;
                        }
                    }
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    builtObjectList.AddRange(builtObject.ConstructionQueue.ConstructionWaitQueue);
                    foreach (BuiltObject item in builtObjectList)
                    {
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
                builtObject.CompleteTeardown(_Game.Galaxy);
            }
            int selectedIndex = cmbBuiltObjectFilter.SelectedIndex;
            method_178(builtObject_, selectedIndex);
        }

        private void btnBuiltObjectRetireSelected_Click(object sender, EventArgs e)
        {
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            if (selectedBuiltObjects == null || selectedBuiltObjects.Count <= 0)
            {
                return;
            }
            string string_ = TextResolver.GetText("Retiring ships permanently removes them from the game");
            MessageBoxEx messageBoxEx = method_372(string_, TextResolver.GetText("Retire selected ships?"));
            if (!(messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes"))
            {
                return;
            }
            for (int i = 0; i < selectedBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = selectedBuiltObjects[i];
                if (builtObject.TopSpeed > 0 && builtObject.Owner != null && builtObject.Role != BuiltObjectRole.Base)
                {
                    StellarObject stellarObject = builtObject.Empire.FindNearestShipYard(builtObject, canRepairOrBuild: true, includeVerySmallYards: true);
                    if (stellarObject != null)
                    {
                        builtObject.AssignMission(BuiltObjectMissionType.Retire, stellarObject, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                    }
                }
            }
            foreach (DataGridViewRow selectedRow in ctlBuiltObjectList.Grid.SelectedRows)
            {
                BuiltObject builtObject2 = ctlBuiltObjectList.ResolveBuiltObject(selectedRow);
                if (builtObject2 != null)
                {
                    string value = "(" + TextResolver.GetText("None") + ")";
                    if (builtObject2.Mission != null && builtObject2.Mission.Type != 0)
                    {
                        value = Galaxy.ResolveDescription(builtObject2.Mission.Type);
                    }
                    selectedRow.Cells[4].Value = value;
                }
            }
        }

        private void XxYlcNpSu4_Click(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            selectedShipGroup?.Empire.AssignFleetLoadTroops(selectedShipGroup, manuallyAssigned: true);
            method_268(selectedShipGroup);
        }

        private void btnShipGroupRepairAndRefuel_Click(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup == null)
            {
                return;
            }
            StellarObject stellarObject = selectedShipGroup.Empire.FindNearestShipYard(selectedShipGroup.LeadShip, canRepairOrBuild: true, includeVerySmallYards: false);
            ResourceList fuelTypes = selectedShipGroup.CalculateRequiredFuel();
            StellarObject stellarObject2 = _Game.Galaxy.FastFindNearestRefuellingPoint(selectedShipGroup.LeadShip.Xpos, selectedShipGroup.LeadShip.Ypos, fuelTypes, selectedShipGroup.Empire, selectedShipGroup.LeadShip, includeResupplyShips: true, null, selectedShipGroup.Ships.Count);
            if (selectedShipGroup.TotalDamage <= 0)
            {
                if (stellarObject2 != null)
                {
                    selectedShipGroup.ForceCompleteMission();
                    if (stellarObject2 is BuiltObject)
                    {
                        selectedShipGroup.AssignMission(BuiltObjectMissionType.Refuel, (BuiltObject)stellarObject2, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                    }
                    else if (stellarObject2 is Habitat)
                    {
                        selectedShipGroup.AssignMission(BuiltObjectMissionType.Refuel, (Habitat)stellarObject2, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                    }
                }
            }
            else if (stellarObject != null)
            {
                selectedShipGroup.ForceCompleteMission();
                selectedShipGroup.AssignMission(BuiltObjectMissionType.Repair, stellarObject, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                for (int i = 0; i < selectedShipGroup.Ships.Count; i++)
                {
                    BuiltObject builtObject = selectedShipGroup.Ships[i];
                    if (builtObject.DamagedComponentCount > 0)
                    {
                        double num = _Game.Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, stellarObject.Xpos, stellarObject.Ypos);
                        if ((builtObject.TopSpeed > 0 && num < 2000.0) || (builtObject.WarpSpeed > 0 && builtObject.TopSpeed > 0))
                        {
                            builtObject.ClearPreviousMissionRequirements();
                            builtObject.AssignMission(BuiltObjectMissionType.Repair, stellarObject, null, BuiltObjectMissionPriority.VeryHigh);
                        }
                    }
                    else if (builtObject.TopSpeed > 0 && builtObject.WarpSpeed > 0)
                    {
                        builtObject.ClearPreviousMissionRequirements();
                        builtObject.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.VeryHigh);
                    }
                }
            }
            method_268(selectedShipGroup);
        }

        private void btnShipGroupRetrofit_Click(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup != null)
            {
                selectedShipGroup.Empire.AssignFleetRetrofit(selectedShipGroup, isAutoRetrofit: false);
                method_268(selectedShipGroup);
            }
        }

        private void chkOptionsAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOptionsAutoSave.Checked)
            {
                numOptionsAutoSaveMinutes.Enabled = true;
            }
            else
            {
                numOptionsAutoSaveMinutes.Enabled = false;
            }
        }

        private void btnCycleShipStance_Click(object sender, EventArgs e)
        {
            RrhupiLdOr(_Game.SelectedObject);
        }

        public void SetControlLocalizedLabels()
        {
            btnResearchFacilities.Text = TextResolver.GetText("Research Stations");
            btnResearchTreeHighTech.Text = TextResolver.GetText("HighTech");
            btnResearchTreeEnergy.Text = TextResolver.GetText("Energy");
            btnResearchTreeWeapons.Text = TextResolver.GetText("Weapons");
            btnEditEmpireApplyTechLevel.Text = TextResolver.GetText("Apply New Tech Level");
            btnEditEmpireSelectTechs.Text = TextResolver.GetText("Select Techs Individually");
            tbarEmpireTechLevel.LabelText = TextResolver.GetText("Tech Level");
            chkOptionsControlResearch.Text = TextResolver.GetText("Research");
            lblDesignWeaponFleetTargetting.Text = TextResolver.GetText("Fleet Targeting");
            lblDesignWeaponHyperStop.Text = TextResolver.GetText("Hyper Disruption");
            lblDesignWeaponPointDefense.Text = TextResolver.GetText("Point Defense");
            lblDesignWeaponFighters.Text = TextResolver.GetText("Fighter Capacity");
            tabColony_Facilities.Text = TextResolver.GetText("Facilities");
            btnColonyFacilityBuild.Text = TextResolver.GetText("Build Facility");
            btnColonyFacilityScrap.Text = TextResolver.GetText("Scrap Facility");
            btnGameMenuEditor.Text = TextResolver.GetText("Enter Game Editor");
            lblOptionsControlColonyFacilities.Text = TextResolver.GetText("Colony Facility Building");
            cmbOptionsControlColonyFacilities.Items.Clear();
            cmbOptionsControlColonyFacilities.Items.AddRange(new string[3]
            {
            TextResolver.GetText("Control manually"),
            TextResolver.GetText("Suggest new colony facilities"),
            TextResolver.GetText("Fully automate")
            });
            lblOptionsControlOfferPirateMissions.Text = TextResolver.GetText("Offer Pirate Missions");
            cmbOptionsControlOfferPirateMissions.Items.Clear();
            cmbOptionsControlOfferPirateMissions.Items.AddRange(new string[3]
            {
            TextResolver.GetText("Control manually"),
            TextResolver.GetText("Suggest pirate missions"),
            TextResolver.GetText("Fully automate")
            });
            grpGameOptionsDiscoveries.Text = TextResolver.GetText("Discoveries");
            cmbGameOptionsEncounterAbandonedShipOrBase.Items.Clear();
            cmbGameOptionsEncounterAbandonedShipOrBase.Items.AddRange(new string[3]
            {
            TextResolver.GetText("Ask what to do"),
            TextResolver.GetText("Investigate - show all results"),
            TextResolver.GetText("Investigate - do not show results")
            });
            cmbGameOptionsEncounterRuins.Items.Clear();
            cmbGameOptionsEncounterRuins.Items.AddRange(new string[5]
            {
            TextResolver.GetText("Ask what to do"),
            TextResolver.GetText("Investigate - show all results"),
            TextResolver.GetText("Investigate - report discoveries"),
            TextResolver.GetText("Investigate - report major discoveries"),
            TextResolver.GetText("Investigate - do not show results")
            });
            lblGameOptionsEncounterAbandonedShipOrBase.Text = TextResolver.GetText("When encounter Abandoned Ship or Base");
            lblGameOptionsEncounterRuins.Text = TextResolver.GetText("When encounter Ruins");
            chkOptionsNewShipsAutomated.Text = TextResolver.GetText("Newly built ships are automated");
            chkOptionsLoadedGamesPaused.Text = TextResolver.GetText("Loaded games are paused");
            chkOptionsSuppressAllPopups.Text = TextResolver.GetText("Suppress all pop-up screens");
            XgxsOtuAmD.Text = TextResolver.GetText("Cancel");
            WqesexberY.Text = TextResolver.GetText("Apply Policy");
            btnBuildOrderPurchase.Text = TextResolver.GetText("Purchase");
            btnBuildOrderCancel.Text = TextResolver.GetText("Cancel");
            lblBuildOrderAdvisorRecommendation.Text = TextResolver.GetText("Advisor Suggest");
            lblBuildOrderCurrentAmount.Text = TextResolver.GetText("Current Amount");
            lblBuildOrderCost.Text = TextResolver.GetText("Purchase Costs");
            lblBuildOrderDesign.Text = TextResolver.GetText("Design");
            lblBuildOrderPurchaseAmount.Text = TextResolver.GetText("Order Amount");
            lblBuildOrderMaintenance.Text = TextResolver.GetText("Maintenance Costs Abbreviated");
            lblBuildOrderAvailableFundsLabel.Text = TextResolver.GetText("Available Money and Cashflow");
            lblBuildOrderAvailableCashflowLabel.Text = TextResolver.GetText("TOTAL Purchase and Maintenance Costs");
            lblEditHabitatScenery.Text = TextResolver.GetText("Scenery Bonus");
            lblEditHabitatResearchBonus.Text = TextResolver.GetText("Research Bonus");
            lblEditHabitatQuality.Text = TextResolver.GetText("Quality");
            chkEditHabitatHasRings.Text = TextResolver.GetText("Has Rings");
            ExUfgIlqCu.Items.Clear();
            ExUfgIlqCu.Items.AddRange(new string[4]
            {
            "(" + TextResolver.GetText("None") + ")",
            TextResolver.GetText("Weapons"),
            TextResolver.GetText("Energy"),
            TextResolver.GetText("HighTech")
            });
            lblBuiltObjectComponentsResources.Text = TextResolver.GetText("Resources");
            lblBuiltObjectDockingWaitQueue.Text = TextResolver.GetText("Ships waiting to dock");
            lblBuiltObjectGalaxyMapTitle.Text = TextResolver.GetText("Location of selected item in Galaxy");
            lblBuiltObjectName.Text = TextResolver.GetText("Name");
            lblColonyCount.Text = TextResolver.GetText("Count");
            lblColonyDockingBayWaitQueue.Text = TextResolver.GetText("Ships waiting to dock");
            lblColonyGalaxyMapTitle.Text = TextResolver.GetText("Location of selected Colony in Galaxy");
            lblColonyName.Text = TextResolver.GetText("Name");
            lblColonyTaxRate.Text = TextResolver.GetText("Tax Rate") + " (%)";
            lblComponentGuideResources.Text = TextResolver.GetText("Resources required to manufacture");
            lblConstructionSummaryComponents.Text = TextResolver.GetText("Components Required");
            lblConstructionSummaryOverview.Text = TextResolver.GetText("Overview");
            lblConstructionSummaryResources.Text = TextResolver.GetText("Resources Required");
            lblConstructionYardManufacturers.Text = TextResolver.GetText("Manufacturing Plants");
            lblConstructionYardManufacturerWaitQueue.Text = TextResolver.GetText("Components waiting to be manufactured");
            lblConstructionYardWaitQueue.Text = TextResolver.GetText("Ships waiting to be constructed");
            lblDesignDetailMaintenanceCost.Text = TextResolver.GetText("Maintenance Cost");
            lblDesignDetailPurchaseCost.Text = TextResolver.GetText("Purchase Cost");
            lblDesignDetailTitle.Text = TextResolver.GetText("Edit Design");
            lblDesignName.Text = TextResolver.GetText("Name");
            lblDesignsBasicsSubRole.Text = TextResolver.GetText("Role");
            lblDesignsFleeWhen.Text = TextResolver.GetText("Flee When");
            lblDesignsPicture.Text = TextResolver.GetText("Picture");
            lblDesignsSize.Text = TextResolver.GetText("Size");
            lblDesignsStance.Text = TextResolver.GetText("Stance");
            lblDesignsWeaponsTitle.Text = TextResolver.GetText("Weapons");
            lblDesignTacticsInvasion.Text = TextResolver.GetText("Invasion");
            lblDesignTacticsStrongerShips.Text = TextResolver.GetText("Stronger Opponents");
            lblDesignTacticsWeakerShips.Text = TextResolver.GetText("Weaker Opponents");
            lblDesignWeaponBombardPower.Text = TextResolver.GetText("Bombard Power");
            lblDesignWeaponFirepower.Text = TextResolver.GetText("Firepower");
            lblDesignWeaponHyperDeny.Text = TextResolver.GetText("HyperDeny");
            lblDesignWeaponRangeMaximum.Text = TextResolver.GetText("Longest Range");
            lblDesignWeaponRangeMinimum.Text = TextResolver.GetText("Shortest Range");
            lblDesignWeaponTargetting.Text = TextResolver.GetText("Targetting");
            FqNlrHsjje.Text = TextResolver.GetText("Maximum Weapons Energy use per second");
            lblDesignWeaponTroopCapacity.Text = TextResolver.GetText("Troops");
            lblDesignWeaponBoardingAssault.Text = TextResolver.GetText("Boarding Assault");
            lblDiplomacyGalaxyMapTitle.Text = TextResolver.GetText("Known Systems for this Empire");
            lblDockingBayWaitQueue.Text = TextResolver.GetText("Ships waiting for a Docking Bay");
            lblEditBuiltObjectEmpire.Text = TextResolver.GetText("Empire");
            lblEditBuiltObjectEncounterEvent.Text = TextResolver.GetText("Encounter");
            lblEditBuiltObjectFleet.Text = TextResolver.GetText("Fleet");
            lblEditBuiltObjectFuel.Text = TextResolver.GetText("Fuel");
            lblEditBuiltObjectName.Text = TextResolver.GetText("Name");
            lblEditBuiltObjectTitle.Text = TextResolver.GetText("Edit Ship or Base");
            lblEditBuiltObjectTroops.Text = TextResolver.GetText("Troops");
            lblEditCreatureAttackStrength.Text = TextResolver.GetText("Strength");
            lblEditCreatureDamage.Text = TextResolver.GetText("Damage");
            lblEditCreatureName.Text = TextResolver.GetText("Name");
            lblEditCreatureSize.Text = TextResolver.GetText("Size");
            lblEditCreatureTitle.Text = TextResolver.GetText("Edit Creature");
            lblEditEmpireFlagShape.Text = TextResolver.GetText("Flag Shape");
            lblEditEmpireGovernmentStyle.Text = TextResolver.GetText("Government");
            lblEditEmpireListTitle.Text = TextResolver.GetText("Edit Empires");
            lblEditEmpireMoney.Text = TextResolver.GetText("Money");
            lblEditEmpireName.Text = TextResolver.GetText("Name");
            lblEditEmpirePrimaryColor.Text = TextResolver.GetText("Main Color");
            lblEditEmpireRace.Text = TextResolver.GetText("Race");
            lblEditEmpireRelationshipsTitle.Text = TextResolver.GetText("Other Empires");
            lblEditEmpireReputation.Text = TextResolver.GetText("Reputation");
            lblEditEmpireSecondaryColor.Text = TextResolver.GetText("Secondary Color");
            lblEditEmpireTitle.Text = TextResolver.GetText("Edit Empire");
            lblEditEmpireWarWeariness.Text = TextResolver.GetText("War weariness");
            lblEditHabitatDevelopmentLevel.Text = TextResolver.GetText("Development");
            lblEditHabitatEmpire.Text = TextResolver.GetText("Empire");
            lblEditHabitatMicrowaveRadiation.Text = TextResolver.GetText("Microwave Radiation");
            lblEditHabitatName.Text = TextResolver.GetText("Name");
            lblEditHabitatPopulation.Text = TextResolver.GetText("Population");
            lblEditHabitatResources.Text = TextResolver.GetText("Resources");
            lblEditHabitatSize.Text = TextResolver.GetText("Size");
            lblEditHabitatSolarRadiation.Text = TextResolver.GetText("Solar Radiation");
            lblEditHabitatTitle.Text = TextResolver.GetText("Edit Planet or Moon");
            lblEditHabitatTroops.Text = TextResolver.GetText("Troops");
            lblEditHabitatXrayRadiation.Text = TextResolver.GetText("X-Ray Radiation");
            lblEmpireSummaryName.Text = TextResolver.GetText("Name");
            lblExpansionPlannerAvailableBuiltObjects.Text = TextResolver.GetText("Available Construction ships");
            lblExpansionPlannerCurrentlyShowing.Text = TextResolver.GetText("Currently Showing");
            lblExpansionPlannerGalaxyMap.Text = TextResolver.GetText("Location in Galaxy");
            lblExpansionPlannerResourceFilter.Text = TextResolver.GetText("Filter by");
            lblExpansionPlannerResources.Text = TextResolver.GetText("Resources");
            lblGalaxyMapKeyTitle.Text = TextResolver.GetText("Map Key");
            lblGalaxyMapViewModeLabel.Text = TextResolver.GetText("View");
            lblGameEditorEnterPassword.Text = TextResolver.GetText("Password");
            lblGameEditorEnterPasswordTitle.Text = TextResolver.GetText("Enter Editor Password");
            lblGameEditorPasswordTitle.Text = TextResolver.GetText("Editor Password");
            lblGameEditorTitle.Text = TextResolver.GetText("Game Editor");
            lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Text = TextResolver.GetText("fps");
            lblGameOptionsEngagementStanceAttack.Text = TextResolver.GetText("Attack/Bombard");
            lblGameOptionsEngagementStanceEscort.Text = TextResolver.GetText("Mission Escort");
            lblGameOptionsEngagementStanceOther.Text = TextResolver.GetText("Other");
            lblGameOptionsEngagementStancePatrol.Text = TextResolver.GetText("Mission Patrol");
            lblGameOptionsFleetAttackGather.Text = TextResolver.GetText("First assemble when this percentage of fleet dispersed");
            lblGameOptionsFleetAttackRefuel.Text = TextResolver.GetText("First assemble when this percentage of fleet need fuel");
            lblIntroductionTitle.Text = TextResolver.GetText("Welcome to Your Empire");
            lblIntroductionVictoryConditions.Text = TextResolver.GetText("To win this game");
            lblOptionsAutomationMode.Text = TextResolver.GetText("Mode");
            lblOptionsControlAgentMissions.Text = TextResolver.GetText("Intelligence Missions");
            lblOptionsControlAttacks.Text = TextResolver.GetText("Attacks Against Enemies");
            lblOptionsControlColonization.Text = TextResolver.GetText("Colonization");
            lblOptionsControlConstruction.Text = TextResolver.GetText("Ship Building");
            lblOptionsControlDiplomacyGifts.Text = TextResolver.GetText("Sending Diplomatic Gifts");
            lblOptionsControlDiplomacyOffense.Text = TextResolver.GetText("War and Trade Sanctions");
            lblOptionsControlDiplomacyTreaties.Text = TextResolver.GetText("Treaties");
            lblOptionsMainViewScrollSpeed.Text = TextResolver.GetText("Scroll Speed");
            lblOptionsMainViewStarFieldSize.Text = TextResolver.GetText("Star Density");
            lblOptionsMainViewZoomSpeed.Text = TextResolver.GetText("Zoom Speed");
            lblOptionsMouseScrollMode.Text = TextResolver.GetText("Mouse scroll-wheel behavior");
            lblOptionsMusicVolume.Text = TextResolver.GetText("Music");
            lblOptionsSoundEffectsVolume.Text = TextResolver.GetText("Effects");
            lblRetrofitCost.Text = TextResolver.GetText("Cost");
            lblRetrofitDesignLabel.Text = TextResolver.GetText("Retrofit To");
            lblRuinDetailName.Text = TextResolver.GetText("Name");
            lblShipGroupGalaxyMapTitle.Text = TextResolver.GetText("Location of selected Fleet in Galaxy");
            lblShipGroupName.Text = TextResolver.GetText("Name");
            lblTroopInfoName.Text = TextResolver.GetText("Name");
            lblTroopsGalaxyMapTitle.Text = TextResolver.GetText("Location of selected Troops in Galaxy");
            lnkEventMessageLink.Text = TextResolver.GetText("Read more about this race") + "...";
            btnBuiltObjectConstructionRemoveFromQueue.Text = TextResolver.GetText("Remove from Queue");
            btnBuiltObjectConstructionScrap.Text = TextResolver.GetText("Scrap Ship");
            btnBuiltObjectConstructionShowSummary.Text = TextResolver.GetText("Show Construction Summary");
            awqrtdiblI.Text = TextResolver.GetText("Move Down");
            btnBuiltObjectConstructionYardMoveToBottom.Text = TextResolver.GetText("Move to Bottom");
            btnBuiltObjectConstructionYardMoveToTop.Text = TextResolver.GetText("Move to Top");
            btnBuiltObjectConstructionYardMoveUp.Text = TextResolver.GetText("Move Up");
            btnBuiltObjectGoto.Text = TextResolver.GetText("Go to Ship");
            btnBuiltObjectRefuelSelected.Text = TextResolver.GetText("Refuel");
            btnBuiltObjectRepairSelected.Text = TextResolver.GetText("Repair");
            btnBuiltObjectRetireSelected.Text = TextResolver.GetText("Retire");
            btnBuiltObjectRetrofitSelected.Text = TextResolver.GetText("Retrofit");
            btnBuiltObjectScrapSelected.Text = TextResolver.GetText("Scrap");
            btnBuiltObjectSelect.Text = TextResolver.GetText("Select Ship");
            btnBuiltObjectViewDesign.Text = TextResolver.GetText("View Design");
            btnBuiltObjectViewShipGroup.Text = TextResolver.GetText("View Fleet");
            btnCharacterInfoClose.Text = TextResolver.GetText("Close");
            btnColonyConstructionRemoveFromQueue.Text = TextResolver.GetText("Remove Ship");
            btnColonyConstructionScrap.Text = TextResolver.GetText("Scrap Ship");
            btnColonyConstructionShowSummary.Text = TextResolver.GetText("Show Construction Summary");
            btnColonyConstructionYardMoveDown.Text = TextResolver.GetText("Move Down");
            btnColonyConstructionYardMoveToBottom.Text = TextResolver.GetText("Move to Bottom");
            btnColonyConstructionYardMoveToTop.Text = TextResolver.GetText("Move to Top");
            btnColonyConstructionYardMoveUp.Text = TextResolver.GetText("Move Up");
            btnColonyGotoHabitat.Text = TextResolver.GetText("Go to Colony");
            btnColonyMakeCapital.Text = TextResolver.GetText("Set as Capital");
            btnColonySelect.Text = TextResolver.GetText("Select Colony");
            btnColonyShowExpansionPlanner.Text = TextResolver.GetText("Show Expansion Planner");
            btnColonyShowOnGalaxyMap.Text = TextResolver.GetText("Show On Galaxy Map");
            btnColonyShowRuin.Text = TextResolver.GetText("Show Ruin Details");
            btnColonyTroopsDisband.Text = TextResolver.GetText("Disband");
            btnColonyTroopsRecruit.Text = TextResolver.GetText("Recruit");
            btnColonyTroopGarrison.Text = TextResolver.GetText("Garrison");
            btnColonyTroopsUngarrison.Text = TextResolver.GetText("Ungarrison");
            btnColonyTroopTransferTransport.Text = TextResolver.GetText("Transfer");
            btnDesignsAddNew.Text = TextResolver.GetText("Add New");
            btnDesignsCancel.Text = TextResolver.GetText("Cancel");
            btnDesignsCopyAsNew.Text = TextResolver.GetText("Copy As New");
            btnDesignsDelete.Text = TextResolver.GetText("Delete");
            btnDesignsEdit.Text = TextResolver.GetText("Edit");
            btnDesignsLoad.Text = TextResolver.GetText("Load Designs...");
            btnDesignsSave.Text = TextResolver.GetText("Save Selected Designs") + "...";
            btnDesignsSaveDesign.Text = TextResolver.GetText("Save");
            btnDesignsShowComponentGuide.Text = TextResolver.GetText("Show Component Guide");
            btnDesignsShowConstructionSummary.Text = TextResolver.GetText("Show Construction Summary");
            btnDesignsUpgrade.Text = TextResolver.GetText("Upgrade");
            btnEditBuiltObjectAddTroop.Text = TextResolver.GetText("Add Troop");
            btnEditBuiltObjectRemoveTroop.Text = TextResolver.GetText("Remove Troop");
            btnEditEmpireBuiltObjectGoto.Text = TextResolver.GetText("Go to Ship or Base");
            btnEditEmpireClose.Text = TextResolver.GetText("Close");
            btnEditEmpireColonyGoto.Text = TextResolver.GetText("Go to Colony");
            btnEditEmpireListAdd.Text = TextResolver.GetText("Add New Empire");
            btnEditEmpireListClose.Text = TextResolver.GetText("Close");
            btnEditEmpireListEdit.Text = TextResolver.GetText("Edit Empire");
            btnEditEmpireListRemove.Text = TextResolver.GetText("Remove Empire");
            btnEditHabitatAddTroop.Text = TextResolver.GetText("Add Troop");
            btnEditHabitatRemoveTroop.Text = TextResolver.GetText("Remove Troop");
            btnEmpireSummaryChangeGovernment.Text = TextResolver.GetText("Have Revolution");
            btnEmpireSummaryShowExpansionPlanner.Text = TextResolver.GetText("Show Expansion Planner");
            btnEmpireTalk.Text = TextResolver.GetText("Speak");
            btnEventMessageClose.Text = TextResolver.GetText("Close");
            btnEventMessageGoto.Text = TextResolver.GetText("Go to Event Location");
            btnEventMessageInvestigate.Text = TextResolver.GetText("Investigate");
            btnExpansionPlannerSortResources.Text = TextResolver.GetText("Sort by Your Empire Priority");
            btnGalaxyMapGoto.Text = TextResolver.GetText("Go to selected item");
            btnGalaxyMapKey.Text = TextResolver.GetText("Map Key");
            btnGalaxyMapKeyClose.Text = TextResolver.GetText("Close");
            btnGameEditorEditEmpires.Text = TextResolver.GetText("Edit Empires");
            btnGameEditorEnterPasswordCancel.Text = TextResolver.GetText("Cancel");
            btnGameEditorEnterPasswordOk.Text = TextResolver.GetText("Ok");
            btnGameEditorExit.Text = TextResolver.GetText("Exit Editor");
            btnGameEditorPasswordCancel.Text = TextResolver.GetText("Clear");
            btnGameEditorPasswordOk.Text = TextResolver.GetText("Save");
            btnGameEditorSave.Text = TextResolver.GetText("Save");
            btnGameEditorSaveAs.Text = TextResolver.GetText("Save As");
            btnGameEndContinue.Text = TextResolver.GetText("Continue Playing...");
            btnGameEndExit.Text = TextResolver.GetText("Exit to main menu");
            btnGameMenuCancel.Text = TextResolver.GetText("Cancel");
            btnGameMenuLoad.Text = TextResolver.GetText("Load Game");
            btnGameMenuOptions.Text = TextResolver.GetText("Options");
            btnGameMenuQuit.Text = TextResolver.GetText("Exit Distant Worlds");
            btnGameMenuSave.Text = TextResolver.GetText("Save Game");
            btnGameMenuSaveAs.Text = TextResolver.GetText("Save Game As");
            btnGameMenuStartMenu.Text = TextResolver.GetText("Exit to Main Menu");
            btnGameOptionsAdvancedDisplaySettings.Text = TextResolver.GetText("Advanced Settings...");
            btnGameOptionsEmpireSettings.Text = TextResolver.GetText("Empire Settings");
            btnGameOptionsResetAutomationMessages.Text = TextResolver.GetText("Reset Warnings");
            btnIntelligenceAgentsDisband.Text = TextResolver.GetText("Disband Agent");
            btnIntelligenceAgentsRecruit.Text = TextResolver.GetText("Recruit Agent");
            btnIntroductionStart.Text = TextResolver.GetText("Start Playing");
            btnMessageHistoryGoto.Text = TextResolver.GetText("Go to Location");
            btnResearchGotoFacility.Text = TextResolver.GetText("Go to Facility");
            btnRetrofitGo.Text = TextResolver.GetText("Retrofit Selected Items to New Design");
            btnShipGroupGoto.Text = TextResolver.GetText("Go to Fleet");
            btnShipGroupInfoSetHomeColony.Text = TextResolver.GetText("Set Home Colony");
            XxYlcNpSu4.Text = TextResolver.GetText("Load Troops");
            btnShipGroupRepairAndRefuel.Text = TextResolver.GetText("Repair and Refuel");
            btnShipGroupRetrofit.Text = TextResolver.GetText("Retrofit to latest designs");
            btnShipGroupSelect.Text = TextResolver.GetText("Select Fleet");
            btnStoryEventClose.Text = TextResolver.GetText("Close");
            btnTroopDisband.Text = TextResolver.GetText("Disband Selected Troops");
            btnTroopGoto.Text = TextResolver.GetText("Go to Troop");
            btnTutorialContinue.Text = TextResolver.GetText("Continue");
            btnTutorialExit.Text = TextResolver.GetText("Exit to Main Menu");
            chkDesignComponentsShowLatest.Text = TextResolver.GetText("Only Show Latest Components");
            chkDesignObsolete.Text = TextResolver.GetText("Mark as Obsolete");
            chkEditBuiltObjectAutomated.Text = TextResolver.GetText("Automated");
            chkEditCreatureAnchoredToParent.Text = TextResolver.GetText("Anchored to Parent");
            chkEditCreatureVisible.Text = TextResolver.GetText("Visible");
            chkEditHabitatCapital.Text = TextResolver.GetText("Empire Capital");
            chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Text = TextResolver.GetText("Unlimited");
            chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Text = TextResolver.GetText("Always show enemy Fleets");
            chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Text = TextResolver.GetText("Always show enemy Military ships");
            chkGameOptionsGalaxyDisplayAlwaysPirates.Text = TextResolver.GetText("Always show Pirates");
            chkGameOptionsGalaxyDisplayCivilianShips.Text = TextResolver.GetText("Civilian ships");
            chkGameOptionsGalaxyDisplayColonyShips.Text = TextResolver.GetText("Colony Ships");
            chkGameOptionsGalaxyDisplayConstructionShips.Text = TextResolver.GetText("Construction Ships");
            chkGameOptionsGalaxyDisplayExplorationShips.Text = TextResolver.GetText("Exploration Ships");
            chkGameOptionsGalaxyDisplayFleets.Text = TextResolver.GetText("Fleets");
            chkGameOptionsGalaxyDisplayMilitaryShips.Text = TextResolver.GetText("Military Ships");
            chkGameOptionsGalaxyDisplayOtherBases.Text = TextResolver.GetText("Other Bases");
            chkGameOptionsGalaxyDisplayResupplyShips.Text = TextResolver.GetText("Resupply Ships");
            chkGameOptionsGalaxyDisplaySpacePorts.Text = TextResolver.GetText("Space Ports");
            chkOptionsAllowSameSystemAsOtherEmpires.Text = TextResolver.GetText("Allow colonization and mining stations in other empires systems");
            chkOptionsAutoPauseInPopup.Text = TextResolver.GetText("Auto Pause in Game Screens");
            chkOptionsAutoSave.Text = TextResolver.GetText("Every (SPACER) minutes");
            chkOptionsControlColonyTaxRates.Text = TextResolver.GetText("Colony Tax Rates");
            chkOptionsControlCharacterLocations.Text = TextResolver.GetText("Character Locations");
            chkOptionsControlDesigns.Text = TextResolver.GetText("Ship Design");
            chkOptionsControlFleets.Text = TextResolver.GetText("Fleet Formation");
            chkOptionsControlTroops.Text = TextResolver.GetText("Troop Recruitment");
            chkOptionsControlPopulationPolicy.Text = TextResolver.GetText("Colony Population Policies");
            chkOptionsPopupMessageColonyGainLoss.Text = TextResolver.GetText("Colony Gain or Loss");
            chkOptionsPopupMessageDiplomacyTreaties.Text = TextResolver.GetText("Treaty offers");
            chkOptionsPopupMessageDiplomacyWarTradeSanctions.Text = TextResolver.GetText("War and Trade Sanctions");
            XwwejKaRdv.Text = TextResolver.GetText("Empire Discovery");
            chkOptionsPopupMessageExploration.Text = TextResolver.GetText("Exploration discoveries");
            KpfeuWqjpj.Text = TextResolver.GetText("Intelligence Missions");
            chkOptionsPopupMessageRequestWarning.Text = TextResolver.GetText("Requests, Warnings and Gifts");
            chkOptionsPopupMessageResearchBreakthrough.Text = TextResolver.GetText("Research Breakthrough");
            chkOptionsPopupMessageShipBuilt.Text = TextResolver.GetText("New Ship Built");
            chkOptionsPopupMessageShipMissionComplete.Text = TextResolver.GetText("Ship Mission Complete");
            chkOptionsPopupMessageShipNeedsRefuelling.Text = TextResolver.GetText("Ship Needs Refuelling or Repair");
            chkOptionsPopupMessageConstructionResourceShortage.Text = TextResolver.GetText("Construction Resource Shortage");
            chkOptionsScrollingMessageColonyGainLoss.Text = TextResolver.GetText("Colony Gain or Loss");
            chkOptionsScrollingMessageDiplomacyTreaties.Text = TextResolver.GetText("Treaty offers");
            chkOptionsScrollingMessageEmpireMetDestroyed.Text = TextResolver.GetText("Empire Discovery");
            chkOptionsScrollingMessageExploration.Text = TextResolver.GetText("Exploration discoveries");
            chkOptionsScrollingMessageIntelligenceMissions.Text = TextResolver.GetText("Intelligence Missions");
            chkOptionsScrollingMessageNewShipBuilt.Text = TextResolver.GetText("New Ship Built");
            chkOptionsScrollingMessageRequestWarning.Text = TextResolver.GetText("Requests, Warnings and Gifts");
            chkOptionsScrollingMessageResearchBreakthrough.Text = TextResolver.GetText("Research Breakthrough");
            chkOptionsScrollingMessageShipMissionComplete.Text = TextResolver.GetText("Ship Mission Complete");
            chkOptionsScrollingMessageShipNeedsRefuelling.Text = TextResolver.GetText("Ship Needs Refuelling or Repair");
            chkOptionsScrollingMessageWarTradeSanctions.Text = TextResolver.GetText("War and Trade Sanctions");
            chkOptionsScrollingMessageConstructionResourceShortage.Text = TextResolver.GetText("Construction Resource Shortage");
            chkOptionsShowSystemNebulae.Text = TextResolver.GetText("Display nebulae clouds in systems");
            gcbeGaamXG.Text = TextResolver.GetText("Under Attack - Civilian Bases");
            chkOptionsPopupMessageUnderAttackCivilianShips.Text = TextResolver.GetText("Under Attack - Civilian Ships");
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.Text = TextResolver.GetText("Under Attack - Colonies && Spaceports");
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.Text = TextResolver.GetText("Under Attack - Colony && Construction Ships");
            chkOptionsPopupMessageUnderAttackExplorationShips.Text = TextResolver.GetText("Under Attack - Exploration Ships");
            chkOptionsPopupMessageUnderAttackMilitaryShips.Text = TextResolver.GetText("Under Attack - Military Ships");
            ltFewaOdau.Text = TextResolver.GetText("Under Attack - Research, Monitoring, Resorts");
            chkOptionsScrollingMessageUnderAttackCivilianBases.Text = TextResolver.GetText("Under Attack - Civilian Bases");
            chkOptionsScrollingMessageUnderAttackCivilianShips.Text = TextResolver.GetText("Under Attack - Civilian Ships");
            chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Text = TextResolver.GetText("Under Attack - Colonies && Spaceports");
            chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Text = TextResolver.GetText("Under Attack - Colony && Construction Ships");
            chkOptionsScrollingMessageUnderAttackExplorationShips.Text = TextResolver.GetText("Under Attack - Exploration Ships");
            chkOptionsScrollingMessageUnderAttackMilitaryShips.Text = TextResolver.GetText("Under Attack - Military Ships");
            chkOptionsScrollingMessageUnderAttackOtherStateBases.Text = TextResolver.GetText("Under Attack - Research, Monitoring, Resorts");
            pnlGameOptionsMessages.HeaderTitle = TextResolver.GetText("Message Settings");
            btnGameOptionsShowMessages.Text = TextResolver.GetText("Show Message Settings");
            pnlBuiltObjectInfo.HeaderTitle = TextResolver.GetText("Ships and Bases");
            pnlColonyInfo.HeaderTitle = TextResolver.GetText("Colonies");
            pnlComponentGuide.HeaderTitle = TextResolver.GetText("Component Guide");
            pnlConstructionSummary.HeaderTitle = TextResolver.GetText("Construction Summary");
            pnlDesigns.HeaderTitle = TextResolver.GetText("Designs");
            vHfFsoqMev.HeaderTitle = TextResolver.GetText("Empire Comparison");
            pnlEmpireInfo.HeaderTitle = TextResolver.GetText("Diplomacy");
            pnlExpansionPlanner.HeaderTitle = TextResolver.GetText("Expansion Planner");
            CaLkaMyrMQ.HeaderTitle = TextResolver.GetText("Galaxy Map");
            pnlGameOptions.HeaderTitle = TextResolver.GetText("Options");
            pnlGameOptionsAdvancedDisplaySettings.HeaderTitle = TextResolver.GetText("Advanced Display Settings");
            pnlGameOptionsEmpireSettings.HeaderTitle = TextResolver.GetText("Other Empire Settings");
            kYdDyYeMls.HeaderTitle = TextResolver.GetText("Characters");
            pnlMessageHistory.HeaderTitle = TextResolver.GetText("Messages");
            pnlResearch.HeaderTitle = TextResolver.GetText("Research");
            pnlRetrofit.HeaderTitle = TextResolver.GetText("Retrofit Selected Items");
            pnlRuinDetail.HeaderTitle = TextResolver.GetText("Ruin Summary");
            pnlShipGroupInfo.HeaderTitle = TextResolver.GetText("Fleets");
            pnlTroopInfo.HeaderTitle = TextResolver.GetText("Troops");
            tabBuiltObject_Cargo.Text = TextResolver.GetText("Cargo");
            kuvxnccAc2.Text = TextResolver.GetText("Components");
            tabBuiltObject_ConstructionYards.Text = TextResolver.GetText("Construction Yards");
            tabBuiltObject_DockingBays.Text = TextResolver.GetText("Docking Bays");
            tabBuiltObject_Troops.Text = TextResolver.GetText("Troops");
            tabBuiltObject_Weapons.Text = TextResolver.GetText("Weapons");
            tabColony_Cargo.Tag = TextResolver.GetText("Cargo");
            tabColony_ConstructionYard.Text = TextResolver.GetText("Construction Yards");
            tabColony_DockingBay.Text = TextResolver.GetText("Docking Bays");
            tabColony_Population.Text = TextResolver.GetText("Population");
            tabColony_Resources.Text = TextResolver.GetText("Resources");
            tabColony_Troops.Text = TextResolver.GetText("Troops");
            cmbBuiltObjectFilter.Items.Clear();
            cmbBuiltObjectFilter.Items.AddRange(new string[18]
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
            TextResolver.GetText("Troop Carriers")
            });
            cmbExpansionPlannerMode.Items.Clear();
            cmbExpansionPlannerMode.Items.AddRange(new string[4]
            {
            TextResolver.GetText("Potential Colonies"),
            TextResolver.GetText("Resource Targets by Your Empire Priority"),
            TextResolver.GetText("Resource Targets by Galaxy Priority"),
            TextResolver.GetText("Your Empire Resource Locations")
            });
            cmbGalaxyMapViewMode.Items.Clear();
            cmbGalaxyMapViewMode.Items.AddRange(new string[11]
            {
            "(" + TextResolver.GetText("Default") + ")",
            TextResolver.GetText("Our Systems"),
            TextResolver.GetText("Potential Colonies"),
            TextResolver.GetText("Known Resources"),
            TextResolver.GetText("Explored Systems"),
            TextResolver.GetText("Independent Populations"),
            TextResolver.GetText("Enemy Systems"),
            TextResolver.GetText("Pirate Bases"),
            TextResolver.GetText("Ancient Ruins"),
            TextResolver.GetText("Scenic Locations"),
            TextResolver.GetText("Research Locations")
            });
            method_583(cmbGameOptionsEngagementStanceAttack);
            method_583(cmbGameOptionsEngagementStanceEscort);
            method_583(cmbGameOptionsEngagementStanceOther);
            method_583(cmbGameOptionsEngagementStancePatrol);
            method_583(cmbGameOptionsEngagementStanceAttackManual);
            method_583(cmbGameOptionsEngagementStanceEscortManual);
            method_583(cmbGameOptionsEngagementStanceOtherManual);
            method_583(cmbGameOptionsEngagementStancePatrolManual);
            cmbOptionsAutomationMode.Items.Clear();
            cmbOptionsAutomationMode.Items.AddRange(new string[8]
            {
            "(" + TextResolver.GetText("Custom") + ")",
            TextResolver.GetText("Default"),
            TextResolver.GetText("Expert") + " (" + TextResolver.GetText("none") + ")",
            TextResolver.GetText("Rule in Absence") + " (" + TextResolver.GetText("full") + ")",
            TextResolver.GetText("Expansion"),
            TextResolver.GetText("War and Combat"),
            TextResolver.GetText("Diplomacy"),
            TextResolver.GetText("Spy Master")
            });
            method_584(cmbOptionsControlAgentMissions, "Suggest offensive missions");
            method_584(fYpVlWkAfp, "Suggest attack targets");
            method_584(cmbOptionsControlColonization, "Suggest new colonies");
            method_584(cmbOptionsControlConstruction, "Suggest new ships and bases");
            method_584(cmbOptionsControlDiplomacyGifts, "Suggest gifts to empires");
            method_584(cmbOptionsControlDiplomacyOffense, "Suggest war and trade sanctions");
            method_584(cmbOptionsControlDiplomacyTreaties, "Suggest new treaties");
            sldGameOptionsAttackOvermatch.LabelText = TextResolver.GetText("Attack Overmatch");
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.LabelText = TextResolver.GetText("System Nebulae Detail");
            cmbOptionsMouseScrollWheelBehaviour.Items.Clear();
            cmbOptionsMouseScrollWheelBehaviour.Items.AddRange(new string[3]
            {
            TextResolver.GetText("No movement"),
            TextResolver.GetText("Move to selected item"),
            TextResolver.GetText("Move to mouse cursor location")
            });
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Text = TextResolver.GetText("Galaxy View - Ship Display");
            grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Text = TextResolver.GetText("Maximum Framerate");
            grpGameOptionsDefaultEngagementStances.Text = TextResolver.GetText("Default Engagement Stances - Auto");
            grpGameOptionsDefaultEngagementStancesManual.Text = TextResolver.GetText("Default Engagement Stances - Manual");
            grpGameOptionsFleetAttackSettings.Text = TextResolver.GetText("Fleet Attack Settings");
            grpOptionsAutoSave.Text = TextResolver.GetText("Auto Save");
            grpOptionsControl.Text = TextResolver.GetText("Automation");
            grpOptionsDisplaySettings.Text = TextResolver.GetText("Display Settings");
            grpOptionsPopupMessages.Text = TextResolver.GetText("Popup Messages");
            grpOptionsScrollingMessages.Text = TextResolver.GetText("Scrolling Messages");
            grpOptionsVolume.Text = TextResolver.GetText("Sound Volume");
        }

        private void method_583(ComboBox comboBox_0)
        {
            comboBox_0.Items.Clear();
            comboBox_0.Items.AddRange(new string[4]
            {
            TextResolver.GetText("No default stance"),
            TextResolver.GetText("Engage when attacked"),
            TextResolver.GetText("Engage nearby targets"),
            TextResolver.GetText("Engage system targets")
            });
        }

        private void method_584(ComboBox comboBox_0, string string_30)
        {
            comboBox_0.Items.Clear();
            comboBox_0.Items.Add(TextResolver.GetText("Control manually"));
            comboBox_0.Items.Add(TextResolver.GetText(string_30));
            comboBox_0.Items.Add(TextResolver.GetText("Fully automate"));
        }

        private void btnSelectionAction1_Click(object sender, EventArgs e)
        {
            method_594(btnSelectionAction1.Tag);
        }

        private void btnSelectionAction2_Click(object sender, EventArgs e)
        {
            method_594(btnSelectionAction2.Tag);
        }

        private void btnSelectionAction3_Click(object sender, EventArgs e)
        {
            method_594(btnSelectionAction3.Tag);
        }

        private void btnSelectionAction4_Click(object sender, EventArgs e)
        {
            method_594(btnSelectionAction4.Tag);
        }

        private void btnSelectionAction5_Click(object sender, EventArgs e)
        {
            method_594(btnSelectionAction5.Tag);
        }

        private void btnSelectionAction6_Click(object sender, EventArgs e)
        {
            method_594(btnSelectionAction6.Tag);
        }

        private void btnSelectionAction7_Click(object sender, EventArgs e)
        {
            method_594(btnSelectionAction7.Tag);
        }

        private void btnSelectionAction8_Click(object sender, EventArgs e)
        {
            method_594(btnSelectionAction8.Tag);
        }

        private void method_585(ShipAction shipAction_1, ShipAction shipAction_2, ShipAction shipAction_3, ShipAction shipAction_4, ShipAction shipAction_5, ShipAction shipAction_6, ShipAction shipAction_7, ShipAction shipAction_8)
        {
            method_588(btnSelectionAction1, shipAction_1);
            method_588(btnSelectionAction2, shipAction_2);
            method_588(btnSelectionAction3, shipAction_3);
            method_588(btnSelectionAction4, shipAction_4);
            method_588(btnSelectionAction5, shipAction_5);
            method_588(btnSelectionAction6, shipAction_6);
            method_588(btnSelectionAction7, shipAction_7);
            method_588(btnSelectionAction8, shipAction_8);
        }

        private Bitmap method_586(Bitmap bitmap_225, int int_64, int int_65)
        {
            Bitmap bitmap = null;
            double val = (double)int_64 / (double)bitmap_225.Width;
            double val2 = (double)int_65 / (double)bitmap_225.Height;
            double num = Math.Min(val, val2);
            if (num < 1.0)
            {
                int num2 = (int)((double)bitmap_225.Width * num);
                int num3 = (int)((double)bitmap_225.Height * num);
                bitmap = new Bitmap(num2, num3, PixelFormat.Format32bppPArgb);
                using Graphics graphics = Graphics.FromImage(bitmap);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle srcRect = new Rectangle(0, 0, bitmap_225.Width, bitmap_225.Height);
                Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawImage(bitmap_225, destRect, srcRect, GraphicsUnit.Pixel);
                return bitmap;
            }
            return new Bitmap(bitmap_225);
        }

        private void method_587(GlassButton glassButton_0, Bitmap bitmap_225)
        {
            if (glassButton_0.Image != null)
            {
                Image image = glassButton_0.Image;
                glassButton_0.Image = null;
                image.Dispose();
            }
            if (bitmap_225 != null)
            {
                bitmap_225 = method_586(bitmap_225, 25, 19);
            }
            glassButton_0.Image = bitmap_225;
        }

        private void method_588(GlassButton glassButton_0, ShipAction shipAction_1)
        {
            glassButton_0.SuspendLayout();
            glassButton_0.Hint = string.Empty;
            glassButton_0.EmphasizeDisabled = true;
            glassButton_0.ClearText();
            if (shipAction_1 != null)
            {
                glassButton_0.Enabled = true;
                glassButton_0.Tag = shipAction_1;
                if (!string.IsNullOrEmpty(shipAction_1.Hint))
                {
                    glassButton_0.Hint = shipAction_1.Hint;
                }
                if (shipAction_1.ActionType != 0)
                {
                    switch (shipAction_1.ActionType)
                    {
                        case ShipActionType.RecruitTroops:
                            {
                                Troop troop = null;
                                Bitmap bitmap_2 = bitmap_23[0];
                                if (shipAction_1.Target2 != null && shipAction_1.Target2 is Troop)
                                {
                                    troop = (Troop)shipAction_1.Target2;
                                    switch (troop.Type)
                                    {
                                        case TroopType.Infantry:
                                            bitmap_2 = bitmap_23[troop.PictureRef];
                                            break;
                                        case TroopType.Armored:
                                            bitmap_2 = bitmap_24[troop.PictureRef];
                                            break;
                                        case TroopType.Artillery:
                                            bitmap_2 = bitmap_25[troop.PictureRef];
                                            break;
                                        case TroopType.SpecialForces:
                                            bitmap_2 = bitmap_26[troop.PictureRef];
                                            break;
                                        case TroopType.PirateRaider:
                                            bitmap_2 = bitmap_27[troop.PictureRef];
                                            break;
                                    }
                                }
                                method_587(glassButton_0, bitmap_2);
                                method_589(glassButton_0, "");
                                break;
                            }
                        case ShipActionType.AutomateShip:
                            method_587(glassButton_0, bitmap_53);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.JoinShipGroup:
                            method_587(glassButton_0, bitmap_64);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.LeaveShipGroup:
                            method_587(glassButton_0, bitmap_65);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.BuildColonize:
                            method_587(glassButton_0, bitmap_71);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.FighterOptions:
                            method_587(glassButton_0, bitmap_58);
                            method_589(glassButton_0, "fighter");
                            break;
                        case ShipActionType.FighterBuildFighter:
                            method_587(glassButton_0, bitmap_69);
                            method_589(glassButton_0, "fighter");
                            break;
                        case ShipActionType.FighterBuildBomber:
                            method_587(glassButton_0, bitmap_70);
                            method_589(glassButton_0, "fighter");
                            break;
                        case ShipActionType.FighterLaunchFighters:
                            method_587(glassButton_0, bitmap_59);
                            method_589(glassButton_0, "fighter");
                            break;
                        case ShipActionType.FighterLaunchBombers:
                            method_587(glassButton_0, bitmap_60);
                            method_589(glassButton_0, "fighter");
                            break;
                        case ShipActionType.FighterRetrieveFighters:
                            method_587(glassButton_0, bitmap_61);
                            method_589(glassButton_0, "fighter");
                            break;
                        case ShipActionType.FighterRetrieveBombers:
                            method_587(glassButton_0, bitmap_62);
                            method_589(glassButton_0, "fighter");
                            break;
                        case ShipActionType.BuildOptions:
                            method_587(glassButton_0, bitmap_72);
                            method_589(glassButton_0, "build");
                            break;
                        case ShipActionType.ReturnToTop:
                            method_587(glassButton_0, bitmap_68);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.UnautomateShip:
                            method_587(glassButton_0, bitmap_54);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.CreateNewFleet:
                            method_587(glassButton_0, bitmap_76);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.ColonyBuildOptions:
                            method_587(glassButton_0, bitmap_72);
                            method_589(glassButton_0, "facility");
                            break;
                        case ShipActionType.BuildPlanetaryFacility:
                            if (shipAction_1.Target != null && shipAction_1.Target is PlanetaryFacilityDefinition)
                            {
                                PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)shipAction_1.Target;
                                int pictureRef = planetaryFacilityDefinition.PictureRef;
                                Bitmap bitmap_4 = new Bitmap(bitmap_8[pictureRef]);
                                method_587(glassButton_0, bitmap_4);
                                if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.Wonder)
                                {
                                    method_589(glassButton_0, "wonder");
                                }
                                else
                                {
                                    method_589(glassButton_0, "facility");
                                }
                            }
                            break;
                        case ShipActionType.AssignAttack:
                            method_587(glassButton_0, bitmap_82);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.FighterUpgradeAll:
                            method_587(glassButton_0, bitmap_63);
                            method_589(glassButton_0, "fighter");
                            break;
                        case ShipActionType.SetFleetPosture:
                            if (shipAction_1.Target != null && shipAction_1.Target is ShipGroup)
                            {
                                ShipGroup shipGroup2 = (ShipGroup)shipAction_1.Target;
                                Bitmap bitmap_3 = null;
                                switch (shipGroup2.Posture)
                                {
                                    case FleetPosture.Attack:
                                        bitmap_3 = bitmap_126;
                                        break;
                                    case FleetPosture.Defend:
                                        bitmap_3 = bitmap_127;
                                        break;
                                }
                                method_587(glassButton_0, bitmap_3);
                                method_589(glassButton_0, "");
                            }
                            break;
                        case ShipActionType.SetFleetRange:
                            if (shipAction_1.Target != null && shipAction_1.Target is ShipGroup)
                            {
                                ShipGroup shipGroup = (ShipGroup)shipAction_1.Target;
                                Bitmap bitmap = null;
                                bitmap = ((shipGroup.PostureRangeSquared <= 2250000.0) ? bitmap_128 : ((shipGroup.PostureRangeSquared <= 2304000000.0) ? bitmap_129 : ((shipGroup.PostureRangeSquared <= 250000000000.0) ? bitmap_130 : ((!(shipGroup.PostureRangeSquared <= 1000000000000.0)) ? bitmap_132 : bitmap_131))));
                                method_587(glassButton_0, bitmap);
                                method_589(glassButton_0, "");
                            }
                            break;
                        case ShipActionType.SetFleetAttackPoint:
                            method_587(glassButton_0, bitmap_133);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.SetFleetHomeBase:
                            method_587(glassButton_0, bitmap_134);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.ColonyBuildWonder:
                            method_587(glassButton_0, bitmap_72);
                            method_589(glassButton_0, "wonder");
                            break;
                        case ShipActionType.BuildOptionsPrivate:
                            method_587(glassButton_0, bitmap_72);
                            method_589(glassButton_0, "buildprivate");
                            break;
                        case ShipActionType.GeneratePirateMissionAttack:
                            method_587(glassButton_0, bitmap_100);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.GeneratePirateMissionDefend:
                            method_587(glassButton_0, bitmap_101);
                            method_589(glassButton_0, "");
                            break;
                        case ShipActionType.GeneratePirateMissionSmuggling:
                            method_587(glassButton_0, bitmap_102);
                            method_589(glassButton_0, "");
                            break;
                        default:
                            method_589(glassButton_0, "");
                            glassButton_0.Enabled = false;
                            glassButton_0.Tag = null;
                            method_587(glassButton_0, bitmap_91);
                            break;
                        case ShipActionType.DeployVirus:
                            {
                                Bitmap bitmap_ = null;
                                if (shipAction_1.Target2 is Plague)
                                {
                                    Plague plague = (Plague)shipAction_1.Target2;
                                    bitmap_ = bitmap_9[plague.PictureRef];
                                }
                                method_587(glassButton_0, bitmap_);
                                method_589(glassButton_0, "plague");
                                break;
                            }
                    }
                    glassButton_0.Enabled = shipAction_1.Enabled;
                }
                else if (shipAction_1.MissionType != 0)
                {
                    switch (shipAction_1.MissionType)
                    {
                        case BuiltObjectMissionType.Escape:
                            method_587(glassButton_0, bitmap_57);
                            method_589(glassButton_0, "");
                            break;
                        case BuiltObjectMissionType.Retire:
                            if (shipAction_1.Target is BuiltObject)
                            {
                                method_587(glassButton_0, bitmap_56);
                            }
                            else if (shipAction_1.Target is Fighter)
                            {
                                method_587(glassButton_0, bitmap_77);
                            }
                            method_589(glassButton_0, "");
                            break;
                        case BuiltObjectMissionType.Retrofit:
                            if (_Game.SelectedObject == null)
                            {
                                break;
                            }
                            if (_Game.SelectedObject is BuiltObject)
                            {
                                BuiltObject builtObject3 = (BuiltObject)_Game.SelectedObject;
                                if (builtObject3.Role == BuiltObjectRole.Base)
                                {
                                    method_587(glassButton_0, bitmap_67);
                                }
                                else
                                {
                                    method_587(glassButton_0, bitmap_66);
                                }
                                method_589(glassButton_0, "");
                            }
                            else if (_Game.SelectedObject is ShipGroup)
                            {
                                method_587(glassButton_0, bitmap_66);
                                method_589(glassButton_0, "");
                            }
                            break;
                        case BuiltObjectMissionType.Hold:
                            method_587(glassButton_0, bitmap_75);
                            method_589(glassButton_0, "");
                            break;
                        case BuiltObjectMissionType.Explore:
                            method_589(glassButton_0, "");
                            break;
                        case BuiltObjectMissionType.Build:
                            if (shipAction_1.Design != null)
                            {
                                StellarObject constructionYard = null;
                                int num = 0;
                                bool flag = false;
                                if (_Game.SelectedObject is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)_Game.SelectedObject;
                                    constructionYard = builtObject;
                                    flag = true;
                                    if (builtObject.ConstructionQueue != null)
                                    {
                                        num = builtObject.ConstructionQueue.CountUnderConstruction(shipAction_1.Design.SubRole);
                                    }
                                }
                                else if (_Game.SelectedObject is Habitat)
                                {
                                    Habitat habitat = (Habitat)_Game.SelectedObject;
                                    constructionYard = habitat;
                                    if (habitat.ConstructionQueue != null)
                                    {
                                        num = habitat.ConstructionQueue.CountUnderConstruction(shipAction_1.Design.SubRole);
                                        if (habitat.Empire == _Game.PlayerEmpire)
                                        {
                                            flag = true;
                                        }
                                    }
                                }
                                int pictureRef2 = shipAction_1.Design.PictureRef;
                                Bitmap bitmap2 = new Bitmap(builtObjectImageCache_0.ObtainImageSmall(pictureRef2));
                                bitmap2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                if (flag)
                                {
                                    ResourceList deficientResources = new ResourceList();
                                    if (!_Game.PlayerEmpire.CheckDesignResourcesAtConstructionYard(shipAction_1.Design, constructionYard, out deficientResources))
                                    {
                                        bitmap2 = method_653(bitmap2, bitmap_86, bitmap2.Height);
                                        string text = string.Empty;
                                        for (int i = 0; i < deficientResources.Count; i++)
                                        {
                                            if (i > 0)
                                            {
                                                text += ", ";
                                            }
                                            text += deficientResources[i].Name;
                                        }
                                        string hint = shipAction_1.Hint;
                                        shipAction_1.Hint = hint + " (" + TextResolver.GetText("Resource shortage").ToUpper(CultureInfo.InvariantCulture) + ": " + text + ")";
                                        glassButton_0.Hint = shipAction_1.Hint;
                                    }
                                }
                                method_587(glassButton_0, bitmap2);
                                if (num > 0)
                                {
                                    glassButton_0.TextColor = Color.FromArgb(255, 255, 0);
                                    glassButton_0.Text = num.ToString();
                                }
                                method_589(glassButton_0, "build");
                            }
                            else if (shipAction_1.Target != null && shipAction_1.Target is BuiltObject)
                            {
                                BuiltObject builtObject2 = (BuiltObject)shipAction_1.Target;
                                if (builtObject2.DamagedComponentCount > 0 && builtObject2.Empire == _Game.PlayerEmpire)
                                {
                                    method_587(glassButton_0, bitmap_72);
                                    method_589(glassButton_0, "");
                                    break;
                                }
                                method_589(glassButton_0, "");
                                glassButton_0.Enabled = false;
                                glassButton_0.Tag = null;
                                method_587(glassButton_0, bitmap_91);
                            }
                            else
                            {
                                method_589(glassButton_0, "");
                                glassButton_0.Enabled = false;
                                glassButton_0.Tag = null;
                                method_587(glassButton_0, bitmap_91);
                            }
                            break;
                        case BuiltObjectMissionType.Repair:
                            method_587(glassButton_0, bitmap_73);
                            method_589(glassButton_0, "");
                            break;
                        case BuiltObjectMissionType.Move:
                            method_587(glassButton_0, bitmap_37);
                            method_589(glassButton_0, "");
                            break;
                        case BuiltObjectMissionType.Refuel:
                            method_587(glassButton_0, bitmap_55);
                            method_589(glassButton_0, "");
                            break;
                        default:
                            method_589(glassButton_0, "");
                            glassButton_0.Enabled = false;
                            glassButton_0.Tag = null;
                            method_587(glassButton_0, bitmap_91);
                            break;
                        case BuiltObjectMissionType.LoadTroops:
                            method_587(glassButton_0, bitmap_74);
                            method_589(glassButton_0, "");
                            break;
                    }
                    glassButton_0.Enabled = shipAction_1.Enabled;
                }
                else
                {
                    method_589(glassButton_0, "");
                    glassButton_0.Enabled = false;
                    glassButton_0.Tag = null;
                    method_587(glassButton_0, bitmap_91);
                }
            }
            else
            {
                method_589(glassButton_0, "");
                glassButton_0.Enabled = false;
                glassButton_0.Tag = null;
                method_587(glassButton_0, bitmap_91);
            }
            glassButton_0.ResumeLayout();
        }

        private void method_589(GlassButton glassButton_0, string string_30)
        {
            glassButton_0.DelayFrameRefresh = true;
            switch (string_30.ToLower(CultureInfo.InvariantCulture))
            {
                case "":
                case "default":
                    glassButton_0.BackColor = Color.FromArgb(0, 0, 48);
                    glassButton_0.OuterBorderColor = Color.FromArgb(0, 0, 64);
                    glassButton_0.ShineColor = Color.FromArgb(112, 112, 176);
                    glassButton_0.GlowColor = Color.FromArgb(128, 128, 255);
                    break;
                case "facility":
                    glassButton_0.BackColor = Color.FromArgb(10, 70, 20);
                    glassButton_0.OuterBorderColor = Color.FromArgb(0, 48, 16);
                    glassButton_0.ShineColor = Color.FromArgb(64, 192, 96);
                    glassButton_0.GlowColor = Color.FromArgb(80, 255, 112);
                    break;
                case "wonder":
                    glassButton_0.BackColor = Color.FromArgb(96, 10, 64);
                    glassButton_0.OuterBorderColor = Color.FromArgb(40, 0, 32);
                    glassButton_0.ShineColor = Color.FromArgb(224, 80, 184);
                    glassButton_0.GlowColor = Color.FromArgb(255, 64, 192);
                    break;
                case "plague":
                    glassButton_0.BackColor = Color.FromArgb(96, 10, 0);
                    glassButton_0.OuterBorderColor = Color.FromArgb(40, 0, 0);
                    glassButton_0.ShineColor = Color.FromArgb(224, 80, 0);
                    glassButton_0.GlowColor = Color.FromArgb(255, 64, 0);
                    break;
                case "build":
                    glassButton_0.BackColor = Color.FromArgb(255, 204, 0);
                    glassButton_0.ForeColor = Color.FromArgb(150, 150, 150);
                    glassButton_0.OuterBorderColor = Color.FromArgb(51, 40, 20);
                    glassButton_0.InnerBorderColor = Color.FromArgb(67, 67, 77);
                    glassButton_0.ShineColor = Color.FromArgb(180, 168, 84);
                    glassButton_0.GlowColor = Color.FromArgb(204, 160, 80);
                    if (glassButton_0.IntensifyColors)
                    {
                        glassButton_0.GlowColor = Color.FromArgb(255, 153, 0);
                    }
                    break;
                case "buildprivate":
                    glassButton_0.BackColor = Color.FromArgb(255, 160, 0);
                    glassButton_0.ForeColor = Color.FromArgb(150, 150, 150);
                    glassButton_0.OuterBorderColor = Color.FromArgb(51, 40, 20);
                    glassButton_0.InnerBorderColor = Color.FromArgb(67, 67, 77);
                    glassButton_0.ShineColor = Color.FromArgb(192, 128, 64);
                    glassButton_0.GlowColor = Color.FromArgb(255, 160, 80);
                    if (glassButton_0.IntensifyColors)
                    {
                        glassButton_0.GlowColor = Color.FromArgb(255, 144, 64);
                    }
                    break;
                case "fighter":
                    glassButton_0.BackColor = Color.FromArgb(153, 102, 255);
                    glassButton_0.ForeColor = Color.FromArgb(150, 150, 150);
                    glassButton_0.OuterBorderColor = Color.FromArgb(32, 20, 51);
                    glassButton_0.InnerBorderColor = Color.FromArgb(67, 67, 77);
                    glassButton_0.ShineColor = Color.FromArgb(153, 108, 192);
                    glassButton_0.GlowColor = Color.FromArgb(128, 80, 204);
                    if (glassButton_0.IntensifyColors)
                    {
                        glassButton_0.GlowColor = Color.FromArgb(153, 0, 255);
                    }
                    break;
            }
            glassButton_0.DelayFrameRefresh = false;
        }

        private string method_590(object object_7)
        {
            string text = string.Empty;
            if (object_7 != null && object_7 is ShipAction)
            {
                ShipAction shipAction = (ShipAction)object_7;
                if (shipAction != null)
                {
                    if (shipAction.MissionType != 0)
                    {
                        switch (shipAction.MissionType)
                        {
                            case BuiltObjectMissionType.Escape:
                                text = text + TextResolver.GetText("Escape from attackers") + " (E)";
                                break;
                            case BuiltObjectMissionType.Retire:
                                if (shipAction.Target is BuiltObject)
                                {
                                    text += TextResolver.GetText("Scrap base immediately");
                                }
                                else if (shipAction.Target is Fighter)
                                {
                                    Fighter fighter = (Fighter)shipAction.Target;
                                    text = ((fighter.Specification.Type != FighterType.Bomber) ? (text + TextResolver.GetText("Scrap Fighter immediately")) : (text + TextResolver.GetText("Scrap Bomber immediately")));
                                }
                                break;
                            case BuiltObjectMissionType.Retrofit:
                                if (_Game.SelectedObject != null && _Game.SelectedObject is BuiltObject)
                                {
                                    if (shipAction.Design != null)
                                    {
                                        BuiltObject builtObject2 = (BuiltObject)_Game.SelectedObject;
                                        double cost = 0.0;
                                        ComponentList componentsToProcure = null;
                                        _Game.PlayerEmpire.DetermineRetrofitAffordability(builtObject2, shipAction.Design, out cost, out componentsToProcure);
                                        text = string.Format(TextResolver.GetText("Retrofit to latest design"), shipAction.Design.Name, cost.ToString("###,###,##0"));
                                    }
                                }
                                else if (_Game.SelectedObject != null && _Game.SelectedObject is ShipGroup)
                                {
                                    text = TextResolver.GetText("Retrofit fleet to latest designs");
                                }
                                break;
                            case BuiltObjectMissionType.Colonize:
                                if (shipAction.Target != null && shipAction.Target is Habitat)
                                {
                                    Habitat habitat3 = (Habitat)shipAction.Target;
                                    text += string.Format(TextResolver.GetText("Colonize TYPE CATEGORY NAME"), Galaxy.ResolveDescription(habitat3.Type), Galaxy.ResolveDescription(habitat3.Category).ToLower(CultureInfo.InvariantCulture), habitat3.Name);
                                }
                                break;
                            case BuiltObjectMissionType.Hold:
                                text = text + TextResolver.GetText("Stop") + " (S)";
                                break;
                            case BuiltObjectMissionType.LoadTroops:
                                text += TextResolver.GetText("Load Troops at nearest colony");
                                break;
                            case BuiltObjectMissionType.Refuel:
                            case BuiltObjectMissionType.Repair:
                                text = ((!(_Game.SelectedObject is ShipGroup)) ? (text + TextResolver.GetText("Refuel and Repair ship")) : (text + TextResolver.GetText("Refuel and Repair Fleet")));
                                break;
                            case BuiltObjectMissionType.Move:
                                text += TextResolver.GetText("Return to Base");
                                if (shipAction.Target != null && shipAction.Target is Habitat)
                                {
                                    Habitat habitat2 = (Habitat)shipAction.Target;
                                    text = text + " (" + habitat2.Name + ")";
                                }
                                break;
                            case BuiltObjectMissionType.Explore:
                                text += TextResolver.GetText("Explore nearest system");
                                break;
                            case BuiltObjectMissionType.Build:
                                if (shipAction.Target != null && shipAction.Target is Habitat)
                                {
                                    Habitat habitat = (Habitat)shipAction.Target;
                                    if (shipAction.Design != null)
                                    {
                                        double num = shipAction.Design.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                        text = ((habitat.Owner == _Game.PlayerEmpire) ? string.Format(TextResolver.GetText("Build SHIPTYPE"), Galaxy.ResolveDescription(shipAction.Design.SubRole), shipAction.Design.Name, num.ToString("###,###,##0")) : string.Format(TextResolver.GetText("Queue construction ship to build a DESIGN here"), Galaxy.ResolveDescription(shipAction.Design.SubRole), shipAction.Design.Name, num.ToString("###,###,##0")));
                                    }
                                }
                                else if (shipAction.Target2 != null && shipAction.Target2 is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)shipAction.Target;
                                    if (builtObject.Empire == _Game.PlayerEmpire && builtObject.UnbuiltOrDamagedComponentCount > 0)
                                    {
                                        text = string.Format(TextResolver.GetText("Queue construction ship to Repair X"), builtObject.Name);
                                    }
                                }
                                else if (shipAction.Design != null)
                                {
                                    text = string.Format(arg2: shipAction.Design.CalculateCurrentPurchasePrice(_Game.Galaxy).ToString("###,###,##0"), format: TextResolver.GetText("Build SHIPTYPE"), arg0: Galaxy.ResolveDescription(shipAction.Design.SubRole), arg1: shipAction.Design.Name);
                                }
                                break;
                        }
                    }
                    else if (shipAction.ActionType != 0)
                    {
                        switch (shipAction.ActionType)
                        {
                            case ShipActionType.RecruitTroops:
                                {
                                    text += TextResolver.GetText("Recruit Troops");
                                    if (_Game.SelectedObject == null || !(_Game.SelectedObject is Habitat))
                                    {
                                        break;
                                    }
                                    Habitat habitat5 = (Habitat)_Game.SelectedObject;
                                    if (habitat5.Population != null && habitat5.Population.TotalAmount > 0L && habitat5.Empire == _Game.PlayerEmpire)
                                    {
                                        Troop troop = null;
                                        if (shipAction.Target2 != null && shipAction.Target2 is Troop)
                                        {
                                            troop = (Troop)shipAction.Target2;
                                        }
                                        if (troop == null)
                                        {
                                            troop = habitat5.DetermineTroopTypeForColony();
                                        }
                                        if (troop != null)
                                        {
                                            string text2 = text;
                                            text = text2 + " (" + troop.Name + ", " + Galaxy.ResolveDescription(troop.Type) + ")";
                                        }
                                    }
                                    break;
                                }
                            case ShipActionType.AutomateShip:
                                text = text + TextResolver.GetText("Automate Ship") + " (A)";
                                break;
                            case ShipActionType.JoinShipGroup:
                                text += TextResolver.GetText("Join nearest Fleet");
                                break;
                            case ShipActionType.LeaveShipGroup:
                                text += TextResolver.GetText("Leave Fleet");
                                break;
                            case ShipActionType.BuildColonize:
                                if (shipAction.Target != null && shipAction.Target is Habitat)
                                {
                                    Habitat habitat4 = (Habitat)shipAction.Target;
                                    Design design = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.ColonyShip);
                                    if (design != null)
                                    {
                                        double num2 = design.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                        text += string.Format(TextResolver.GetText("Build a new colony ship and Colonize"), Galaxy.ResolveDescription(habitat4.Type), Galaxy.ResolveDescription(habitat4.Category).ToLower(CultureInfo.InvariantCulture), habitat4.Name, num2.ToString("###,###,##0"));
                                    }
                                }
                                break;
                            case ShipActionType.FighterOptions:
                                text += TextResolver.GetText("Manage Fighters and Bombers");
                                break;
                            case ShipActionType.FighterBuildFighter:
                                text += TextResolver.GetText("Build new fighter");
                                break;
                            case ShipActionType.FighterBuildBomber:
                                text += TextResolver.GetText("Build new bomber");
                                break;
                            case ShipActionType.FighterLaunchFighters:
                                text = ((!(shipAction.Target is Fighter)) ? (text + TextResolver.GetText("Launch available Fighters")) : (text + TextResolver.GetText("Launch this Fighter")));
                                break;
                            case ShipActionType.FighterLaunchBombers:
                                text = ((!(shipAction.Target is Fighter)) ? (text + TextResolver.GetText("Launch available Bombers")) : (text + TextResolver.GetText("Launch this Bomber")));
                                break;
                            case ShipActionType.FighterRetrieveFighters:
                                text = ((!(shipAction.Target is Fighter)) ? (text + TextResolver.GetText("Return all Fighters")) : (text + TextResolver.GetText("Return this Fighter to carrier")));
                                break;
                            case ShipActionType.FighterRetrieveBombers:
                                text = ((!(shipAction.Target is Fighter)) ? (text + TextResolver.GetText("Return all Bombers")) : (text + TextResolver.GetText("Return this Bomber to carrier")));
                                break;
                            case ShipActionType.BuildOptions:
                                text = TextResolver.GetText("Build new ship or base");
                                break;
                            case ShipActionType.ReturnToTop:
                                text = TextResolver.GetText("Return to Top");
                                break;
                            case ShipActionType.UnautomateShip:
                                text += TextResolver.GetText("Turn off automation");
                                break;
                            case ShipActionType.CreateNewFleet:
                                text += TextResolver.GetText("Create new Fleet");
                                break;
                            case ShipActionType.ColonyBuildOptions:
                                text = TextResolver.GetText("Build new planetary facility");
                                break;
                            case ShipActionType.BuildPlanetaryFacility:
                                if (shipAction.Target != null && shipAction.Target is PlanetaryFacilityDefinition)
                                {
                                    PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)shipAction.Target;
                                    text += string.Format(arg1: Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, _Game.PlayerEmpire).ToString("###,##0"), format: TextResolver.GetText("Build FACILITY cost"), arg0: planetaryFacilityDefinition.Name);
                                }
                                break;
                            case ShipActionType.AssignAttack:
                                text = TextResolver.GetText("Attack with nearest available fleet");
                                break;
                            case ShipActionType.FighterUpgradeAll:
                                text += TextResolver.GetText("Upgrade all fighters and bombers to latest");
                                break;
                            case ShipActionType.SetFleetPosture:
                                text = TextResolver.GetText("Set Posture");
                                if (shipAction.Target != null && shipAction.Target is ShipGroup)
                                {
                                    ShipGroup shipGroup2 = (ShipGroup)shipAction.Target;
                                    if (shipGroup2.Posture == FleetPosture.Defend)
                                    {
                                        text = text + "  (" + TextResolver.GetText("Currently Defend") + ")";
                                    }
                                    else if (shipGroup2.Posture == FleetPosture.Attack)
                                    {
                                        text = text + "  (" + TextResolver.GetText("Currently Attack") + ")";
                                    }
                                }
                                break;
                            case ShipActionType.SetFleetRange:
                                {
                                    text = TextResolver.GetText("Set Range");
                                    if (shipAction.Target == null || !(shipAction.Target is ShipGroup))
                                    {
                                        break;
                                    }
                                    ShipGroup shipGroup4 = (ShipGroup)shipAction.Target;
                                    if (shipGroup4.PostureRangeSquared <= 2250000.0)
                                    {
                                        if (shipGroup4.Posture == FleetPosture.Attack)
                                        {
                                            text = text + "  (" + TextResolver.GetText("Currently Target") + ")";
                                        }
                                        else if (shipGroup4.Posture == FleetPosture.Defend)
                                        {
                                            text = text + "  (" + TextResolver.GetText("Currently Home Base") + ")";
                                        }
                                    }
                                    else
                                    {
                                        text = ((!(shipGroup4.PostureRangeSquared <= 2304000000.0)) ? ((!(shipGroup4.PostureRangeSquared <= 250000000000.0)) ? ((!(shipGroup4.PostureRangeSquared <= 1000000000000.0)) ? (text + "  (" + TextResolver.GetText("Currently Anywhere") + ")") : (text + "  (" + TextResolver.GetText("Currently Sector") + ")")) : (text + "  (" + TextResolver.GetText("Currently Nearby Systems") + ")")) : (text + "  (" + TextResolver.GetText("Currently System") + ")"));
                                    }
                                    break;
                                }
                            case ShipActionType.SetFleetAttackPoint:
                                text = TextResolver.GetText("Set Attack Target");
                                if (shipAction.Target != null && shipAction.Target is ShipGroup)
                                {
                                    ShipGroup shipGroup3 = (ShipGroup)shipAction.Target;
                                    text = ((shipGroup3.AttackPoint == null) ? (text + "  (" + string.Format(TextResolver.GetText("Currently X"), TextResolver.GetText("None")) + ")") : (text + "  (" + string.Format(TextResolver.GetText("Currently X"), shipGroup3.AttackPoint.Name) + ")"));
                                }
                                break;
                            case ShipActionType.SetFleetHomeBase:
                                text = TextResolver.GetText("Set Home Base");
                                if (shipAction.Target != null && shipAction.Target is ShipGroup)
                                {
                                    ShipGroup shipGroup = (ShipGroup)shipAction.Target;
                                    text = ((shipGroup.GatherPoint == null) ? (text + "  (" + string.Format(TextResolver.GetText("Currently X"), TextResolver.GetText("None")) + ")") : (text + "  (" + string.Format(TextResolver.GetText("Currently X"), shipGroup.GatherPoint.Name) + ")"));
                                }
                                break;
                            case ShipActionType.ColonyBuildWonder:
                                text = TextResolver.GetText("Build new Wonder");
                                break;
                            case ShipActionType.BuildOptionsPrivate:
                                text = TextResolver.GetText("Build new civilian ship");
                                break;
                            case ShipActionType.GeneratePirateMissionAttack:
                                text = (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent((StellarObject)shipAction.Target, EmpireActivityType.Attack) ? TextResolver.GetText("Cancel Mercenary Attack Mission") : string.Format(arg1: _Game.PlayerEmpire.CalculatePirateAttackPrice((StellarObject)shipAction.Target).ToString("0"), format: TextResolver.GetText("Assign Mercenary Attack Mission"), arg0: ((StellarObject)shipAction.Target).Name));
                                break;
                            case ShipActionType.GeneratePirateMissionDefend:
                                text = (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent((StellarObject)shipAction.Target, EmpireActivityType.Defend) ? TextResolver.GetText("Cancel Mercenary Defend Mission") : string.Format(arg1: _Game.PlayerEmpire.CalculatePirateDefendPrice((StellarObject)shipAction.Target).ToString("0"), format: TextResolver.GetText("Assign Mercenary Defense Mission"), arg0: ((StellarObject)shipAction.Target).Name));
                                break;
                            case ShipActionType.GeneratePirateMissionSmuggling:
                                text = (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent((StellarObject)shipAction.Target, EmpireActivityType.Smuggle) ? TextResolver.GetText("Cancel Mercenary Smuggling Mission") : TextResolver.GetText("Assign Mercenary Smuggling Mission"));
                                break;
                            case ShipActionType.DeployVirus:
                                if (shipAction.Target2 is Plague)
                                {
                                    Plague plague = (Plague)shipAction.Target2;
                                    text = string.Format(TextResolver.GetText("Deploy PLAGUE at this colony"), plague.Name);
                                }
                                break;
                        }
                    }
                }
            }
            return text;
        }

        private string method_591(bool bool_28, bool bool_29)
        {
            string text = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture);
            if (bool_28)
            {
                text = text + " - " + TextResolver.GetText("Missing Tech");
            }
            else if (bool_29)
            {
                text = text + " - " + TextResolver.GetText("Size too big");
            }
            return text + ")";
        }

        private void method_592()
        {
            method_593(null);
        }

        private void method_593(ShipAction shipAction_1)
        {
            if (shipAction_1 != null)
            {
                switch (shipAction_1.ActionType)
                {
                    case ShipActionType.BuildOptions:
                        if (_Game.SelectedObject == null)
                        {
                            break;
                        }
                        if (_Game.SelectedObject is BuiltObject)
                        {
                            BuiltObject builtObject3 = (BuiltObject)_Game.SelectedObject;
                            if (builtObject3.Role != BuiltObjectRole.Base)
                            {
                                break;
                            }
                            Point offset2 = new Point(0, 0);
                            string text10 = string.Empty;
                            string text11 = string.Empty;
                            string text12 = string.Empty;
                            string text13 = string.Empty;
                            string text14 = string.Empty;
                            string text15 = string.Empty;
                            string text16 = string.Empty;
                            string text17 = string.Empty;
                            bool reasonCannotBuildMissingTech = false;
                            bool reasonCannotBuildSizeTooBig = false;
                            Design design7 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.Escort, null, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers: false);
                            if (design7 == null)
                            {
                                text10 = method_591(reasonCannotBuildMissingTech, reasonCannotBuildSizeTooBig);
                            }
                            else if (design7.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design7 = null;
                                text10 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            Design design8 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.Frigate, null, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers: false);
                            if (design8 == null)
                            {
                                text11 = method_591(reasonCannotBuildMissingTech, reasonCannotBuildSizeTooBig);
                            }
                            else if (design8.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design8 = null;
                                text11 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            Design design9 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.Destroyer, null, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers: false);
                            if (design9 == null)
                            {
                                text12 = method_591(reasonCannotBuildMissingTech, reasonCannotBuildSizeTooBig);
                            }
                            else if (design9.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design9 = null;
                                text12 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            Design design10 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.Cruiser, null, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers: false);
                            if (design10 == null)
                            {
                                text13 = method_591(reasonCannotBuildMissingTech, reasonCannotBuildSizeTooBig);
                            }
                            else if (design10.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design10 = null;
                                text13 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            Design design11 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.CapitalShip, null, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers: false);
                            if (design11 == null)
                            {
                                text14 = method_591(reasonCannotBuildMissingTech, reasonCannotBuildSizeTooBig);
                            }
                            else if (design11.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design11 = null;
                                text14 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            Design design12 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.TroopTransport, null, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers: false);
                            if (design12 == null)
                            {
                                text15 = method_591(reasonCannotBuildMissingTech, reasonCannotBuildSizeTooBig);
                            }
                            else if (design12.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design12 = null;
                                text15 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            Design design13 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.Carrier, null, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers: false);
                            if (design13 == null)
                            {
                                text16 = method_591(reasonCannotBuildMissingTech, reasonCannotBuildSizeTooBig);
                            }
                            else if (design13.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design13 = null;
                                text16 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            Design design14 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.ExplorationShip, null, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers: false);
                            if (design14 == null)
                            {
                                text17 = method_591(reasonCannotBuildMissingTech, reasonCannotBuildSizeTooBig);
                            }
                            else if (design14.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design14 = null;
                                text17 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            ShipAction shipAction15 = new ShipAction(BuiltObjectMissionType.Build, builtObject3, offset2, design7);
                            ShipAction shipAction16 = new ShipAction(BuiltObjectMissionType.Build, builtObject3, offset2, design8);
                            ShipAction shipAction17 = new ShipAction(BuiltObjectMissionType.Build, builtObject3, offset2, design9);
                            ShipAction shipAction18 = new ShipAction(BuiltObjectMissionType.Build, builtObject3, offset2, design10);
                            ShipAction shipAction19 = new ShipAction(BuiltObjectMissionType.Build, builtObject3, offset2, design11);
                            ShipAction shipAction20 = new ShipAction(BuiltObjectMissionType.Build, builtObject3, offset2, design12);
                            ShipAction shipAction21 = new ShipAction(BuiltObjectMissionType.Build, builtObject3, offset2, design13);
                            ShipAction shipAction22 = new ShipAction(BuiltObjectMissionType.Build, builtObject3, offset2, design14);
                            if (design7 == null)
                            {
                                shipAction15.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.Escort, includePlanetDestroyers: false);
                                shipAction15.Enabled = false;
                            }
                            if (design8 == null)
                            {
                                shipAction16.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.Frigate, includePlanetDestroyers: false);
                                shipAction16.Enabled = false;
                            }
                            if (design9 == null)
                            {
                                shipAction17.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.Destroyer, includePlanetDestroyers: false);
                                shipAction17.Enabled = false;
                            }
                            if (design10 == null)
                            {
                                shipAction18.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.Cruiser, includePlanetDestroyers: false);
                                shipAction18.Enabled = false;
                            }
                            if (design11 == null)
                            {
                                shipAction19.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.CapitalShip, includePlanetDestroyers: false);
                                shipAction19.Enabled = false;
                            }
                            if (design12 == null)
                            {
                                shipAction20.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.TroopTransport, includePlanetDestroyers: false);
                                shipAction20.Enabled = false;
                            }
                            if (design13 == null)
                            {
                                shipAction21.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.Carrier, includePlanetDestroyers: false);
                                shipAction21.Enabled = false;
                            }
                            if (design14 == null)
                            {
                                shipAction22.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.ExplorationShip, includePlanetDestroyers: false);
                                shipAction22.Enabled = false;
                            }
                            shipAction15.Hint = method_590(shipAction15);
                            shipAction16.Hint = method_590(shipAction16);
                            shipAction17.Hint = method_590(shipAction17);
                            shipAction18.Hint = method_590(shipAction18);
                            shipAction19.Hint = method_590(shipAction19);
                            shipAction20.Hint = method_590(shipAction20);
                            shipAction21.Hint = method_590(shipAction21);
                            shipAction22.Hint = method_590(shipAction22);
                            shipAction15.Hint += text10;
                            shipAction16.Hint += text11;
                            shipAction17.Hint += text12;
                            shipAction18.Hint += text13;
                            shipAction19.Hint += text14;
                            shipAction20.Hint += text15;
                            shipAction21.Hint += text16;
                            shipAction22.Hint += text17;
                            if (design13 != null)
                            {
                                if (design11 == null)
                                {
                                    method_585(shipAction15, shipAction16, shipAction17, shipAction18, shipAction21, shipAction20, shipAction22, new ShipAction(ShipActionType.ReturnToTop, null));
                                }
                                else if (design10 == null)
                                {
                                    method_585(shipAction15, shipAction16, shipAction17, shipAction21, shipAction19, shipAction20, shipAction22, new ShipAction(ShipActionType.ReturnToTop, null));
                                }
                                else if (design12 == null)
                                {
                                    method_585(shipAction15, shipAction16, shipAction17, shipAction18, shipAction19, shipAction21, shipAction22, new ShipAction(ShipActionType.ReturnToTop, null));
                                }
                                else if (design14 == null)
                                {
                                    method_585(shipAction15, shipAction16, shipAction17, shipAction18, shipAction19, shipAction20, shipAction21, new ShipAction(ShipActionType.ReturnToTop, null));
                                }
                                else
                                {
                                    method_585(shipAction15, shipAction16, shipAction17, shipAction18, shipAction19, shipAction20, shipAction22, new ShipAction(ShipActionType.ReturnToTop, null));
                                }
                            }
                            else
                            {
                                method_585(shipAction15, shipAction16, shipAction17, shipAction18, shipAction19, shipAction20, shipAction22, new ShipAction(ShipActionType.ReturnToTop, null));
                            }
                        }
                        else
                        {
                            if (!(_Game.SelectedObject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat3 = (Habitat)_Game.SelectedObject;
                            if (habitat3.Owner != _Game.PlayerEmpire)
                            {
                                break;
                            }
                            double num2 = 0.0;
                            double num3 = 0.0;
                            num2 += habitat3.Xpos;
                            num3 += habitat3.Ypos;
                            Point point = new Point((int)num2, (int)num3);
                            string text18 = string.Empty;
                            Design design15 = null;
                            Design design16 = null;
                            long num4 = _Game.PlayerEmpire.Policy.ConstructionSpaceportLargeColonyPopulationThreshold * 1000000L;
                            long num5 = _Game.PlayerEmpire.Policy.ConstructionSpaceportMediumColonyPopulationThreshold * 1000000L;
                            long num6 = _Game.PlayerEmpire.Policy.ConstructionSpaceportSmallColonyPopulationThreshold;
                            long num61 = _Game.PlayerEmpire.Policy.ConstructionOutpostColonyPopulationThreshold;
                            if (habitat3.Population.TotalAmount > num4)
                            {
                                design15 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.LargeSpacePort, habitat3);
                                design16 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.LargeSpacePort);
                            }
                            else if (habitat3.Population.TotalAmount > num5)
                            {
                                design15 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.MediumSpacePort, habitat3);
                                design16 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.MediumSpacePort);
                            }
                            else if (habitat3.Population.TotalAmount > num6)
                            {
                                design15 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.SmallSpacePort, habitat3);
                                design16 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.SmallSpacePort);
                            }
                            else
                            {
                                design15 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.Outpost, habitat3);
                                design16 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.Outpost);
                            }
                            Design design17 = null;
                            Design design18 = null;
                            if (design15 == null)
                            {

                                design17 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.Outpost, habitat3);
                                design18 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.Outpost);
                                if (design17 == null)
                                {
                                    design17 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.SmallSpacePort, habitat3);
                                    design18 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.SmallSpacePort);
                                    if (design17 == null)
                                    {
                                        design17 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.MediumSpacePort, habitat3);
                                        design18 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.MediumSpacePort);
                                        if (design17 == null)
                                        {
                                            design17 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.LargeSpacePort, habitat3);
                                            design18 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.LargeSpacePort);
                                        }
                                    }
                                }
                                if (design17 != null && design18 != null)
                                {
                                    design15 = design17;
                                    design16 = design18;
                                }
                            }
                            if (_Game.Galaxy.DetermineSpacePortAtColony(habitat3) != null)
                            {
                                design15 = null;
                                text18 = " (" + TextResolver.GetText("Spaceport Already At Colony").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            if (design15 != null && design15.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design15 = null;
                                text18 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            string text19 = string.Empty;
                            Design design19 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.DefensiveBase, habitat3);
                            if (design19 == null)
                            {
                                text19 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            else if (design19.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design19 = null;
                                text19 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            string text20 = string.Empty;
                            Design design20 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.ColonyShip);
                            if (design20 == null)
                            {
                                text20 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            else if (design20.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design20 = null;
                                text20 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            string text21 = string.Empty;
                            Design design21 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.ConstructionShip);
                            if (design21 == null)
                            {
                                text21 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            else if (design21.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design21 = null;
                                text21 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            string text22 = string.Empty;
                            Design design22 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.ResupplyShip);
                            if (design22 == null)
                            {
                                text22 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            else if (design22.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design22 = null;
                                text22 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            string text23 = string.Empty;
                            Design design23 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.MonitoringStation, habitat3);
                            if (design23 == null)
                            {
                                text23 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            else if (design23.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                            {
                                design23 = null;
                                text23 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            _Game.Galaxy.SelectRelativeHabitatSurfacePoint(habitat3, out num2, out num3);
                            num2 += habitat3.Xpos;
                            num3 += habitat3.Ypos;
                            point = new Point((int)num2, (int)num3);
                            ShipAction shipAction23 = new ShipAction(BuiltObjectMissionType.Build, habitat3, point, design15);
                            _Game.PlayerEmpire.DetermineOrbitalBaseLocation(habitat3, out num2, out num3);
                            num2 += habitat3.Xpos;
                            num3 += habitat3.Ypos;
                            point = new Point((int)num2, (int)num3);
                            ShipAction shipAction24 = new ShipAction(BuiltObjectMissionType.Build, habitat3, point, design19);
                            point = new Point((int)habitat3.Xpos, (int)habitat3.Ypos);
                            ShipAction shipAction25 = new ShipAction(BuiltObjectMissionType.Build, habitat3, point, design20);
                            ShipAction shipAction26 = new ShipAction(BuiltObjectMissionType.Build, habitat3, point, design21);
                            ShipAction shipAction27 = new ShipAction(BuiltObjectMissionType.Build, habitat3, point, design22);
                            _Game.Galaxy.SelectRelativeHabitatSurfacePoint(habitat3, out num2, out num3);
                            num2 += habitat3.Xpos;
                            num3 += habitat3.Ypos;
                            point = new Point((int)num2, (int)num3);
                            ShipAction shipAction28 = new ShipAction(BuiltObjectMissionType.Build, habitat3, point, design23);
                            if (design15 == null)
                            {
                                shipAction23.Design = design16;
                                shipAction23.Enabled = false;
                            }
                            if (design19 == null)
                            {
                                shipAction24.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.DefensiveBase);
                                shipAction24.Enabled = false;
                            }
                            if (design20 == null || habitat3.Population.TotalAmount < Galaxy.BuildColonyShipPopulationRequirement)
                            {
                                shipAction25.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.ColonyShip);
                                shipAction25.Enabled = false;
                            }
                            if (design21 == null)
                            {
                                shipAction26.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.ConstructionShip);
                                shipAction26.Enabled = false;
                            }
                            if (design22 == null)
                            {
                                shipAction27.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.ResupplyShip);
                                shipAction27.Enabled = false;
                            }
                            if (design23 == null)
                            {
                                shipAction28.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.MonitoringStation);
                                shipAction28.Enabled = false;
                            }
                            shipAction23.Hint = method_590(shipAction23);
                            shipAction24.Hint = method_590(shipAction24);
                            shipAction25.Hint = method_590(shipAction25);
                            shipAction26.Hint = method_590(shipAction26);
                            shipAction27.Hint = method_590(shipAction27);
                            shipAction28.Hint = method_590(shipAction28);
                            shipAction23.Hint += text18;
                            shipAction24.Hint += text19;
                            shipAction25.Hint += text20;
                            shipAction26.Hint += text21;
                            shipAction27.Hint += text22;
                            shipAction28.Hint += text23;
                            method_585(shipAction23, shipAction24, shipAction25, shipAction26, shipAction27, shipAction28, null, new ShipAction(ShipActionType.ReturnToTop, null));
                        }
                        break;
                    case ShipActionType.FighterOptions:
                        {
                            if (shipAction_1.Target == null || !(shipAction_1.Target is BuiltObject))
                            {
                                break;
                            }
                            BuiltObject builtObject = (BuiltObject)shipAction_1.Target;
                            int num = 0;
                            if (builtObject.Fighters != null)
                            {
                                num = builtObject.FighterCapacity - builtObject.Fighters.TotalSize;
                            }
                            ShipAction shipAction3 = new ShipAction(ShipActionType.FighterBuildFighter, builtObject);
                            ShipAction shipAction4 = new ShipAction(ShipActionType.FighterBuildBomber, builtObject);
                            ShipAction shipAction5 = new ShipAction(ShipActionType.FighterLaunchFighters, builtObject);
                            ShipAction shipAction6 = new ShipAction(ShipActionType.FighterLaunchBombers, builtObject);
                            ShipAction shipAction7 = new ShipAction(ShipActionType.FighterUpgradeAll, builtObject);
                            shipAction3.Hint = method_590(shipAction3);
                            shipAction4.Hint = method_590(shipAction4);
                            shipAction7.Hint = method_590(shipAction7);
                            if (num < 10)
                            {
                                shipAction3.Enabled = false;
                                shipAction4.Enabled = false;
                                shipAction3.Hint = shipAction3.Hint + " (" + TextResolver.GetText("Carrier Full").ToUpper(CultureInfo.InvariantCulture) + ")";
                                shipAction4.Hint = shipAction4.Hint + " (" + TextResolver.GetText("Carrier Full").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            string text = string.Empty;
                            string text2 = string.Empty;
                            if (builtObject.CheckFightersAvailableForLaunch())
                            {
                                shipAction5.ActionType = ShipActionType.FighterLaunchFighters;
                            }
                            else if (builtObject.CheckFightersNeedReturning())
                            {
                                shipAction5.ActionType = ShipActionType.FighterRetrieveFighters;
                            }
                            else
                            {
                                shipAction5.Enabled = false;
                                text = " (" + TextResolver.GetText("No Fighters Available").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            if (builtObject.CheckBombersAvailableForLaunch())
                            {
                                shipAction6.ActionType = ShipActionType.FighterLaunchBombers;
                            }
                            else if (builtObject.CheckBombersNeedReturning())
                            {
                                shipAction6.ActionType = ShipActionType.FighterRetrieveBombers;
                            }
                            else
                            {
                                shipAction6.Enabled = false;
                                text2 = " (" + TextResolver.GetText("No Bombers Available").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            shipAction5.Hint = method_590(shipAction5) + text;
                            shipAction6.Hint = method_590(shipAction6) + text2;
                            string text3 = string.Empty;
                            bool flag = false;
                            if (builtObject.Fighters != null && builtObject.Fighters.Count > 0)
                            {
                                FighterSpecification fighterSpecification = null;
                                FighterSpecification fighterSpecification2 = null;
                                if (builtObject.Empire != null && builtObject.Empire.Research != null)
                                {
                                    fighterSpecification = builtObject.Empire.Research.IdentifyLatestFighterSpecification();
                                    fighterSpecification2 = builtObject.Empire.Research.IdentifyLatestBomberSpecification();
                                }
                                for (int k = 0; k < builtObject.Fighters.Count; k++)
                                {
                                    if (builtObject.Fighters[k].Specification == null)
                                    {
                                        continue;
                                    }
                                    if (fighterSpecification != null && builtObject.Fighters[k].Specification.Type == FighterType.Interceptor)
                                    {
                                        if (builtObject.Fighters[k].Specification != fighterSpecification && builtObject.Fighters[k].OnboardCarrier)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    else if (fighterSpecification2 != null && builtObject.Fighters[k].Specification.Type == FighterType.Bomber && builtObject.Fighters[k].Specification != fighterSpecification2 && builtObject.Fighters[k].OnboardCarrier)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (!flag)
                            {
                                text3 = " (" + TextResolver.GetText("No Onboard Fighters Need Upgrading").ToUpper(CultureInfo.InvariantCulture) + ")";
                                shipAction7.Enabled = false;
                            }
                            shipAction7.Hint += text3;
                            method_585(shipAction3, shipAction4, shipAction5, shipAction6, null, null, shipAction7, new ShipAction(ShipActionType.ReturnToTop, null));
                            break;
                        }
                    case ShipActionType.ColonyBuildWonder:
                        {
                            if (_Game.SelectedObject == null || !(_Game.SelectedObject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat2 = (Habitat)_Game.SelectedObject;
                            if (habitat2.Empire != _Game.PlayerEmpire || habitat2.Population == null || habitat2.Population.TotalAmount <= 0L)
                            {
                                break;
                            }
                            ShipAction shipAction_16 = null;
                            ShipAction shipAction_17 = null;
                            ShipAction shipAction_18 = null;
                            ShipAction shipAction_19 = null;
                            ShipAction shipAction_20 = null;
                            ShipAction shipAction_21 = null;
                            ShipAction shipAction_22 = null;
                            PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList3 = habitat2.ResolveBuildableWonders();
                            if (planetaryFacilityDefinitionList3.Count > 0)
                            {
                                for (int l = 0; l < planetaryFacilityDefinitionList3.Count; l++)
                                {
                                    ShipAction shipAction14 = new ShipAction(ShipActionType.BuildPlanetaryFacility, planetaryFacilityDefinitionList3[l]);
                                    shipAction14.Hint = method_590(shipAction14);
                                    if (Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinitionList3[l], _Game.PlayerEmpire) > _Game.PlayerEmpire.StateMoney)
                                    {
                                        shipAction14.Enabled = false;
                                        shipAction14.Hint = shipAction14.Hint + " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                    }
                                    switch (l)
                                    {
                                        case 0:
                                            shipAction_16 = shipAction14;
                                            break;
                                        case 1:
                                            shipAction_17 = shipAction14;
                                            break;
                                        case 2:
                                            shipAction_18 = shipAction14;
                                            break;
                                        case 3:
                                            shipAction_19 = shipAction14;
                                            break;
                                        case 4:
                                            shipAction_20 = shipAction14;
                                            break;
                                        case 5:
                                            shipAction_21 = shipAction14;
                                            break;
                                        case 6:
                                            shipAction_22 = shipAction14;
                                            break;
                                    }
                                }
                            }
                            method_585(shipAction_16, shipAction_17, shipAction_18, shipAction_19, shipAction_20, shipAction_21, shipAction_22, new ShipAction(ShipActionType.ReturnToTop, null));
                            break;
                        }
                    case ShipActionType.BuildOptionsPrivate:
                        {
                            if (_Game.SelectedObject == null || !(_Game.SelectedObject is BuiltObject))
                            {
                                break;
                            }
                            BuiltObject builtObject2 = (BuiltObject)_Game.SelectedObject;
                            if (builtObject2.Role == BuiltObjectRole.Base)
                            {
                                Point offset = new Point(0, 0);
                                string text4 = string.Empty;
                                string text5 = string.Empty;
                                string text6 = string.Empty;
                                string text7 = string.Empty;
                                string text8 = string.Empty;
                                string text9 = string.Empty;
                                Design design = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.SmallFreighter);
                                if (design == null)
                                {
                                    text4 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                else if (design.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                                {
                                    design = null;
                                    text4 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                Design design2 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.MediumFreighter);
                                if (design2 == null)
                                {
                                    text5 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                else if (design2.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                                {
                                    design2 = null;
                                    text5 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                Design design3 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.LargeFreighter);
                                if (design3 == null)
                                {
                                    text6 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                else if (design3.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                                {
                                    design3 = null;
                                    text6 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                Design design4 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.PassengerShip);
                                if (design4 == null)
                                {
                                    text7 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                else if (design4.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                                {
                                    design4 = null;
                                    text7 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                Design design5 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.MiningShip);
                                if (design5 == null)
                                {
                                    text8 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                else if (design5.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                                {
                                    design5 = null;
                                    text8 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                Design design6 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.GasMiningShip);
                                if (design6 == null)
                                {
                                    text9 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                else if (design6.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                                {
                                    design6 = null;
                                    text9 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                ShipAction shipAction8 = new ShipAction(BuiltObjectMissionType.Build, builtObject2, offset, design);
                                ShipAction shipAction9 = new ShipAction(BuiltObjectMissionType.Build, builtObject2, offset, design2);
                                ShipAction shipAction10 = new ShipAction(BuiltObjectMissionType.Build, builtObject2, offset, design3);
                                ShipAction shipAction11 = new ShipAction(BuiltObjectMissionType.Build, builtObject2, offset, design4);
                                ShipAction shipAction12 = new ShipAction(BuiltObjectMissionType.Build, builtObject2, offset, design5);
                                ShipAction shipAction13 = new ShipAction(BuiltObjectMissionType.Build, builtObject2, offset, design6);
                                if (design == null)
                                {
                                    shipAction8.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.SmallFreighter);
                                    shipAction8.Enabled = false;
                                }
                                if (design2 == null)
                                {
                                    shipAction9.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.MediumFreighter);
                                    shipAction9.Enabled = false;
                                }
                                if (design3 == null)
                                {
                                    shipAction10.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.LargeFreighter);
                                    shipAction10.Enabled = false;
                                }
                                if (design4 == null)
                                {
                                    shipAction11.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.PassengerShip);
                                    shipAction11.Enabled = false;
                                }
                                if (design5 == null)
                                {
                                    shipAction12.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.MiningShip);
                                    shipAction12.Enabled = false;
                                }
                                if (design6 == null)
                                {
                                    shipAction13.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.GasMiningShip);
                                    shipAction13.Enabled = false;
                                }
                                shipAction8.Hint = method_590(shipAction8);
                                shipAction9.Hint = method_590(shipAction9);
                                shipAction10.Hint = method_590(shipAction10);
                                shipAction11.Hint = method_590(shipAction11);
                                shipAction12.Hint = method_590(shipAction12);
                                shipAction13.Hint = method_590(shipAction13);
                                shipAction8.Hint += text4;
                                shipAction9.Hint += text5;
                                shipAction10.Hint += text6;
                                shipAction11.Hint += text7;
                                shipAction12.Hint += text8;
                                shipAction13.Hint += text9;
                                method_585(shipAction8, shipAction9, shipAction10, shipAction11, shipAction12, shipAction13, null, new ShipAction(ShipActionType.ReturnToTop, null));
                            }
                            break;
                        }
                    case ShipActionType.ColonyBuildOptions:
                        {
                            if (_Game.SelectedObject == null || !(_Game.SelectedObject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat = (Habitat)_Game.SelectedObject;
                            if (habitat.Empire == _Game.PlayerEmpire && habitat.Population != null && habitat.Population.TotalAmount > 0L)
                            {
                                ShipAction shipAction_2 = null;
                                ShipAction shipAction_3 = null;
                                ShipAction shipAction_4 = null;
                                ShipAction shipAction_5 = null;
                                ShipAction shipAction_6 = null;
                                ShipAction shipAction_7 = null;
                                ShipAction shipAction_8 = null;
                                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = habitat.ResolveBuildableFacilities();
                                if (planetaryFacilityDefinitionList.Count > 0)
                                {
                                    for (int i = 0; i < planetaryFacilityDefinitionList.Count; i++)
                                    {
                                        ShipAction shipAction = new ShipAction(ShipActionType.BuildPlanetaryFacility, planetaryFacilityDefinitionList[i]);
                                        shipAction.Hint = method_590(shipAction);
                                        if (Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinitionList[i], _Game.PlayerEmpire) > _Game.PlayerEmpire.StateMoney)
                                        {
                                            shipAction.Enabled = false;
                                            shipAction.Hint = shipAction.Hint + " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                        }
                                        switch (i)
                                        {
                                            case 0:
                                                shipAction_2 = shipAction;
                                                break;
                                            case 1:
                                                shipAction_3 = shipAction;
                                                break;
                                            case 2:
                                                shipAction_4 = shipAction;
                                                break;
                                            case 3:
                                                shipAction_5 = shipAction;
                                                break;
                                            case 4:
                                                shipAction_6 = shipAction;
                                                break;
                                            case 5:
                                                shipAction_7 = shipAction;
                                                break;
                                            case 6:
                                                shipAction_8 = shipAction;
                                                break;
                                        }
                                    }
                                }
                                method_585(shipAction_2, shipAction_3, shipAction_4, shipAction_5, shipAction_6, shipAction_7, shipAction_8, new ShipAction(ShipActionType.ReturnToTop, null));
                            }
                            else
                            {
                                if (!habitat.GetPirateControl().CheckFactionHasControl(_Game.PlayerEmpire) || habitat.Population == null || habitat.Population.TotalAmount <= 0L)
                                {
                                    break;
                                }
                                ShipAction shipAction_9 = null;
                                ShipAction shipAction_10 = null;
                                ShipAction shipAction_11 = null;
                                ShipAction shipAction_12 = null;
                                ShipAction shipAction_13 = null;
                                ShipAction shipAction_14 = null;
                                ShipAction shipAction_15 = null;
                                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = habitat.ResolveBuildableFacilitiesPirates(_Game.PlayerEmpire);
                                if (planetaryFacilityDefinitionList2.Count > 0)
                                {
                                    for (int j = 0; j < planetaryFacilityDefinitionList2.Count; j++)
                                    {
                                        ShipAction shipAction2 = new ShipAction(ShipActionType.BuildPlanetaryFacility, planetaryFacilityDefinitionList2[j]);
                                        shipAction2.Hint = method_590(shipAction2);
                                        if (Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinitionList2[j], _Game.PlayerEmpire) > _Game.PlayerEmpire.StateMoney)
                                        {
                                            shipAction2.Enabled = false;
                                            shipAction2.Hint = shipAction2.Hint + " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                        }
                                        switch (j)
                                        {
                                            case 0:
                                                shipAction_9 = shipAction2;
                                                break;
                                            case 1:
                                                shipAction_10 = shipAction2;
                                                break;
                                            case 2:
                                                shipAction_11 = shipAction2;
                                                break;
                                            case 3:
                                                shipAction_12 = shipAction2;
                                                break;
                                            case 4:
                                                shipAction_13 = shipAction2;
                                                break;
                                            case 5:
                                                shipAction_14 = shipAction2;
                                                break;
                                            case 6:
                                                shipAction_15 = shipAction2;
                                                break;
                                        }
                                    }
                                }
                                method_585(shipAction_9, shipAction_10, shipAction_11, shipAction_12, shipAction_13, shipAction_14, shipAction_15, new ShipAction(ShipActionType.ReturnToTop, null));
                            }
                            break;
                        }
                }
            }
            else if (_Game.SelectedObject != null)
            {
                if (_Game.SelectedObject is Habitat)
                {
                    Habitat habitat4 = (Habitat)_Game.SelectedObject;
                    if (habitat4.Owner == _Game.PlayerEmpire)
                    {
                        int cloneIndex = -1;
                        int roboticIndex = -1;
                        int eliteIndex = -1;
                        ShipAction shipAction29 = null;
                        ShipAction shipAction30 = null;
                        ShipAction shipAction31 = null;
                        ShipAction shipAction32 = null;
                        ShipAction shipAction33 = null;
                        TroopList troopList = habitat4.ResolveRecruitableTroopsForColony(out cloneIndex, out roboticIndex, out eliteIndex);
                        for (int m = 0; m < troopList.Count; m++)
                        {
                            if (troopList[m] == null)
                            {
                                continue;
                            }
                            switch (m)
                            {
                                case 0:
                                    shipAction29 = new ShipAction(ShipActionType.RecruitTroops, habitat4);
                                    shipAction29.Target2 = troopList[m];
                                    if (cloneIndex == m)
                                    {
                                        shipAction29.ExtraData = "clone";
                                    }
                                    else if (roboticIndex == m)
                                    {
                                        shipAction29.ExtraData = "robotic";
                                    }
                                    else if (eliteIndex == m)
                                    {
                                        shipAction29.ExtraData = "elite";
                                    }
                                    break;
                                case 1:
                                    shipAction30 = new ShipAction(ShipActionType.RecruitTroops, habitat4);
                                    shipAction30.Target2 = troopList[m];
                                    if (cloneIndex == m)
                                    {
                                        shipAction30.ExtraData = "clone";
                                    }
                                    else if (roboticIndex == m)
                                    {
                                        shipAction30.ExtraData = "robotic";
                                    }
                                    else if (eliteIndex == m)
                                    {
                                        shipAction30.ExtraData = "elite";
                                    }
                                    break;
                                case 2:
                                    shipAction31 = new ShipAction(ShipActionType.RecruitTroops, habitat4);
                                    shipAction31.Target2 = troopList[m];
                                    if (cloneIndex == m)
                                    {
                                        shipAction31.ExtraData = "clone";
                                    }
                                    else if (roboticIndex == m)
                                    {
                                        shipAction31.ExtraData = "robotic";
                                    }
                                    else if (eliteIndex == m)
                                    {
                                        shipAction31.ExtraData = "elite";
                                    }
                                    break;
                                case 3:
                                    shipAction32 = new ShipAction(ShipActionType.RecruitTroops, habitat4);
                                    shipAction32.Target2 = troopList[m];
                                    if (cloneIndex == m)
                                    {
                                        shipAction32.ExtraData = "clone";
                                    }
                                    else if (roboticIndex == m)
                                    {
                                        shipAction32.ExtraData = "robotic";
                                    }
                                    else if (eliteIndex == m)
                                    {
                                        shipAction32.ExtraData = "elite";
                                    }
                                    break;
                                case 4:
                                    shipAction33 = new ShipAction(ShipActionType.RecruitTroops, habitat4);
                                    shipAction33.Target2 = troopList[m];
                                    if (cloneIndex == m)
                                    {
                                        shipAction33.ExtraData = "clone";
                                    }
                                    else if (roboticIndex == m)
                                    {
                                        shipAction33.ExtraData = "robotic";
                                    }
                                    else if (eliteIndex == m)
                                    {
                                        shipAction33.ExtraData = "elite";
                                    }
                                    break;
                            }
                        }
                        ShipAction shipAction34 = new ShipAction(ShipActionType.ColonyBuildOptions, habitat4);
                        ShipAction shipAction35 = new ShipAction(ShipActionType.ColonyBuildWonder, habitat4);
                        shipAction34.Hint = method_590(shipAction34);
                        shipAction35.Hint = method_590(shipAction35);
                        if (habitat4.ResolveBuildableFacilities().Count <= 0)
                        {
                            shipAction34.Enabled = false;
                            shipAction34.Hint = shipAction34.Hint + " (" + TextResolver.GetText("No buildable facilities") + ")";
                        }
                        if (habitat4.ResolveBuildableWonders().Count <= 0)
                        {
                            shipAction35.Enabled = false;
                            shipAction35.Hint = shipAction35.Hint + " (" + TextResolver.GetText("No buildable wonders") + ")";
                        }
                        ShipAction shipAction36 = new ShipAction(ShipActionType.GeneratePirateMissionDefend, habitat4);
                        shipAction36.Hint = string.Format(arg1: _Game.PlayerEmpire.CalculatePirateDefendPrice(habitat4).ToString("0"), format: TextResolver.GetText("Assign Mercenary Defense Mission"), arg0: habitat4.Name);
                        if (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(habitat4, EmpireActivityType.Defend))
                        {
                            shipAction36.Enabled = false;
                            shipAction36.Hint = shipAction36.Hint + " (" + TextResolver.GetText("Mission Already Assigned").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        if (shipAction32 == null)
                        {
                            shipAction32 = shipAction36;
                        }
                        ShipAction shipAction37 = new ShipAction(ShipActionType.GeneratePirateMissionSmuggling, habitat4);
                        shipAction37.Hint = string.Format(TextResolver.GetText("Assign Mercenary Smuggling Mission"), habitat4.Name);
                        if (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(habitat4, EmpireActivityType.Smuggle))
                        {
                            shipAction37.Enabled = false;
                            shipAction37.Hint = shipAction37.Hint + " (" + TextResolver.GetText("Mission Already Assigned").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        if (shipAction33 == null)
                        {
                            shipAction33 = shipAction37;
                        }
                        method_585(shipAction29, shipAction30, shipAction31, shipAction32, shipAction33, shipAction35, shipAction34, new ShipAction(ShipActionType.BuildOptions, habitat4));
                        return;
                    }
                    if (habitat4.Owner != _Game.Galaxy.IndependentEmpire && habitat4.Owner != null)
                    {
                        if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                        {
                            if (habitat4.GetPirateControl().CheckFactionHasControl(_Game.PlayerEmpire))
                            {
                                ShipAction shipAction38 = new ShipAction(ShipActionType.ColonyBuildOptions, habitat4);
                                shipAction38.Hint = method_590(shipAction38);
                                if (habitat4.ResolveBuildableFacilitiesPirates(_Game.PlayerEmpire).Count <= 0)
                                {
                                    shipAction38.Enabled = false;
                                    shipAction38.Hint = shipAction38.Hint + " (" + TextResolver.GetText("No buildable facilities") + ")";
                                }
                                method_585(null, null, null, null, null, null, null, shipAction38);
                            }
                            else
                            {
                                method_585(null, null, null, null, null, null, null, null);
                            }
                            return;
                        }
                        ShipAction shipAction39 = null;
                        if (habitat4.Population != null && habitat4.Population.TotalAmount > 0L && habitat4.Empire != null)
                        {
                            Plague xaraktorVirus = null;
                            string cannotDeployReason = string.Empty;
                            bool flag2 = _Game.PlayerEmpire.CanDeployXaraktorVirus(out xaraktorVirus, out cannotDeployReason);
                            if (xaraktorVirus != null)
                            {
                                shipAction39 = new ShipAction(ShipActionType.DeployVirus, habitat4);
                                shipAction39.Target2 = xaraktorVirus;
                                shipAction39.Hint = method_590(shipAction39);
                                if (!flag2)
                                {
                                    ShipAction shipAction40 = shipAction39;
                                    shipAction40.Hint = shipAction40.Hint + " (" + cannotDeployReason.ToUpper(CultureInfo.InvariantCulture) + ")";
                                    shipAction39.Enabled = false;
                                }
                            }
                        }
                        method_585(null, null, null, null, null, null, shipAction39, null);
                        return;
                    }
                    double num6 = 0.0;
                    double num7 = 0.0;
                    _Game.Galaxy.SelectRelativeHabitatSurfacePoint(habitat4, out num6, out num7);
                    Point offset3 = new Point((int)num6, (int)num7);
                    if (habitat4.Category == HabitatCategoryType.Star)
                    {
                        double minimumDistance = (double)(habitat4.Diameter / 2) + 600.0;
                        if (habitat4.Type == HabitatType.BlackHole)
                        {
                            minimumDistance = (double)(habitat4.Diameter / 2) + 5000.0;
                        }
                        else if (habitat4.Type == HabitatType.SuperNova)
                        {
                            minimumDistance = 3000.0;
                        }
                        _Game.Galaxy.SelectRelativeParkingPoint(minimumDistance, out num6, out num7);
                        offset3 = new Point((int)num6, (int)num7);
                    }
                    ShipAction shipAction41 = null;
                    ShipAction shipAction42 = null;
                    string text24 = string.Empty;
                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                    {
                        string text25 = string.Empty;
                        Design design24 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.SmallSpacePort);
                        Design design25 = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.SmallSpacePort);
                        if (_Game.Galaxy.DetermineSpacePortAtHabitat(habitat4) != null)
                        {
                            design24 = null;
                            text25 = " (" + TextResolver.GetText("Spaceport Already Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        if (design24 != null && design24.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                        {
                            design24 = null;
                            text25 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        shipAction41 = new ShipAction(BuiltObjectMissionType.Build, habitat4, offset3, design24);
                        if (design24 == null)
                        {
                            shipAction41.Design = design25;
                            shipAction41.Enabled = false;
                        }
                        shipAction41.Hint = method_590(shipAction41);
                        shipAction41.Hint += text25;
                    }
                    else
                    {
                        shipAction42 = new ShipAction(ShipActionType.BuildColonize, habitat4);
                        BuiltObject builtObject4 = _Game.PlayerEmpire.CheckColonizingHabitat(habitat4);
                        if (builtObject4 != null)
                        {
                            shipAction42.Enabled = false;
                            text24 = " (" + TextResolver.GetText("Already Colonizing").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        bool canColonizeBecauseAtWar = false;
                        if (!_Game.Galaxy.CheckEmpireTerritoryCanColonizeHabitat(_Game.PlayerEmpire, habitat4, out canColonizeBecauseAtWar))
                        {
                            shipAction42.Enabled = false;
                            text24 = " (" + TextResolver.GetText("In another empire's territory").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                    }
                    DesignList designList = _Game.PlayerEmpire.CheckBasesToBeBuiltAtHabitat(habitat4);
                    string text26 = string.Empty;
                    Design design26 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.MiningStation);
                    ResourceList resourceList = _Game.Galaxy.ResourceSystem.Resources.ResolveValidResourcesForHabitatExcludeManufactured(habitat4);
                    if (resourceList.ContainsGroup(ResourceGroup.Gas))
                    {
                        design26 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.GasMiningStation);
                    }
                    string text27 = string.Empty;
                    Design design27 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.EnergyResearchStation);
                    if (habitat4.ResearchBonus > 0)
                    {
                        switch (habitat4.ResearchBonusIndustry)
                        {
                            case IndustryType.Weapon:
                                design27 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.WeaponsResearchStation);
                                break;
                            case IndustryType.Energy:
                                design27 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.EnergyResearchStation);
                                break;
                            case IndustryType.HighTech:
                                design27 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.HighTechResearchStation);
                                break;
                        }
                    }
                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                    {
                        design27 = null;
                    }
                    string text28 = string.Empty;
                    Design design28 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.MonitoringStation);
                    string empty = string.Empty;
                    Design design29 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.ResortBase);
                    ShipAction shipAction43 = new ShipAction(BuiltObjectMissionType.Build, habitat4, offset3, design26);
                    if (design26 == null)
                    {
                        shipAction43.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.MiningStation);
                        text26 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    else if (design26.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.GetPrivateFunds())
                    {
                        shipAction43.Enabled = false;
                        text26 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    ShipAction shipAction44 = new ShipAction(BuiltObjectMissionType.Build, habitat4, offset3, design27);
                    if (design27 == null)
                    {
                        shipAction44.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.EnergyResearchStation);
                        shipAction44.Enabled = false;
                        text27 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    else if (habitat4.ResearchBonus <= 0)
                    {
                        shipAction44.Enabled = false;
                        text27 = " (" + TextResolver.GetText("No Research Bonus Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    else if (_Game.PlayerEmpire.CheckResearchStationAtLocation(habitat4))
                    {
                        shipAction44.Enabled = false;
                        text27 = " (" + TextResolver.GetText("Research Station Already Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    else if (design27.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                    {
                        shipAction44.Enabled = false;
                        text27 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    ShipAction shipAction45 = new ShipAction(BuiltObjectMissionType.Build, habitat4, offset3, design28);
                    if (design28 == null)
                    {
                        shipAction45.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.MonitoringStation);
                        shipAction45.Enabled = false;
                        text28 = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    else if (design28.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                    {
                        shipAction45.Enabled = false;
                        text28 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    ShipAction shipAction46 = new ShipAction(BuiltObjectMissionType.Build, habitat4, offset3, design29);
                    if (design29 == null)
                    {
                        shipAction46.Design = _Game.PlayerEmpire.Designs.FindNewestIncludingObsolete(BuiltObjectSubRole.ResortBase);
                        shipAction46.Enabled = false;
                        empty = " (" + TextResolver.GetText("Cannot Build").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    else if (habitat4.ScenicFactor <= 0f && habitat4.Ruin == null && (habitat4.Facilities == null || habitat4.Facilities.CountCompletedByType(PlanetaryFacilityType.Wonder) <= 0))
                    {
                        shipAction46.Enabled = false;
                        empty = " (" + TextResolver.GetText("No Scenery Bonus Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    else if (design29.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                    {
                        shipAction46.Enabled = false;
                        empty = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    else
                    {
                        string empty2 = string.Empty;
                        if (habitat4.ScenicFactor > 0f)
                        {
                            empty2 = TextResolver.GetText("Natural scenery");
                        }
                        else if (habitat4.Ruin != null)
                        {
                            empty2 = TextResolver.GetText("Ancient Ruins");
                        }
                        else if (habitat4.Facilities != null && habitat4.Facilities.CountCompletedByType(PlanetaryFacilityType.Wonder) > 0)
                        {
                            empty2 = TextResolver.GetText("Planetary Wonders");
                        }
                        empty = " (" + string.Format(TextResolver.GetText("Tourism from X"), empty2) + ")";
                    }
                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null && !_Game.Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(_Game.PlayerEmpire, habitat4))
                    {
                        shipAction43.Enabled = false;
                        text26 = " (" + TextResolver.GetText("In another empire's territory").ToUpper(CultureInfo.InvariantCulture) + ")";
                        shipAction44.Enabled = false;
                        text27 = " (" + TextResolver.GetText("In another empire's territory").ToUpper(CultureInfo.InvariantCulture) + ")";
                        shipAction45.Enabled = false;
                        text28 = " (" + TextResolver.GetText("In another empire's territory").ToUpper(CultureInfo.InvariantCulture) + ")";
                        shipAction46.Enabled = false;
                        empty = " (" + TextResolver.GetText("In another empire's territory").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    if (shipAction42 != null)
                    {
                        shipAction42.Hint = method_590(shipAction42);
                    }
                    shipAction43.Hint = method_590(shipAction43);
                    shipAction44.Hint = method_590(shipAction44);
                    shipAction45.Hint = method_590(shipAction45);
                    shipAction46.Hint = method_590(shipAction46);
                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                    {
                        List<HabitatType> colonizableHabitatTypes = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpire(_Game.PlayerEmpire);
                        Design design30 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.ColonyShip);
                        if (!_Game.PlayerEmpire.CanEmpireColonizeHabitat(_Game.PlayerEmpire, habitat4, colonizableHabitatTypes, design30))
                        {
                            shipAction42.Enabled = false;
                            text24 = " (" + TextResolver.GetText("Cannot Colonize").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        else if (design30 == null)
                        {
                            shipAction42.Enabled = false;
                            text24 = " (" + TextResolver.GetText("No Colony Ship Design").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        else if (design30.CalculateCurrentPurchasePrice(_Game.Galaxy) > _Game.PlayerEmpire.StateMoney)
                        {
                            shipAction42.Enabled = false;
                            text24 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                    }
                    if (designList.ContainsSubRole(BuiltObjectSubRole.MiningStation) || designList.ContainsSubRole(BuiltObjectSubRole.GasMiningStation))
                    {
                        shipAction43.Enabled = false;
                        text26 = " (" + TextResolver.GetText("Already Building Mining Station Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    if (designList.ContainsSubRole(BuiltObjectSubRole.MonitoringStation))
                    {
                        shipAction45.Enabled = false;
                        text28 = " (" + TextResolver.GetText("Already Building Monitoring Station Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    if (designList.ContainsSubRole(BuiltObjectSubRole.ResortBase))
                    {
                        shipAction46.Enabled = false;
                        empty = " (" + TextResolver.GetText("Already Building Resort Base Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    if ((designList.ContainsSubRole(BuiltObjectSubRole.EnergyResearchStation) || designList.ContainsSubRole(BuiltObjectSubRole.WeaponsResearchStation) || designList.ContainsSubRole(BuiltObjectSubRole.HighTechResearchStation)) && shipAction44 != null)
                    {
                        shipAction44.Enabled = false;
                        text27 = " (" + TextResolver.GetText("Already Building Research Station Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    if (_Game.Galaxy.CheckAlreadyHaveMiningStationAtHabitat(habitat4, _Game.PlayerEmpire))
                    {
                        shipAction43.Enabled = false;
                        text26 = " (" + TextResolver.GetText("Mining Station Already Here").ToUpper(CultureInfo.InvariantCulture) + ")";
                    }
                    if (shipAction42 != null)
                    {
                        shipAction42.Hint += text24;
                    }
                    shipAction43.Hint += text26;
                    shipAction44.Hint += text27;
                    shipAction45.Hint += text28;
                    shipAction46.Hint += empty;
                    if (habitat4.Category == HabitatCategoryType.Star)
                    {
                        shipAction43 = null;
                    }
                    if (habitat4.Category != HabitatCategoryType.Planet && habitat4.Category != HabitatCategoryType.Moon)
                    {
                        shipAction42 = null;
                    }
                    if (habitat4.Type == HabitatType.GasGiant || habitat4.Type == HabitatType.FrozenGasGiant || habitat4.Type == HabitatType.BarrenRock)
                    {
                        shipAction42 = null;
                    }
                    if (shipAction41 != null && shipAction41.Design == null)
                    {
                        shipAction41 = null;
                    }
                    if (shipAction44.Design == null)
                    {
                        shipAction44 = null;
                    }
                    if (shipAction45.Design == null)
                    {
                        shipAction45 = null;
                    }
                    if (shipAction46.Design == null)
                    {
                        shipAction46 = null;
                    }
                    ShipAction shipAction47 = null;
                    if (habitat4.Population != null && habitat4.Population.TotalAmount > 0L && habitat4.Empire != null)
                    {
                        Plague xaraktorVirus2 = null;
                        string cannotDeployReason2 = string.Empty;
                        bool flag3 = _Game.PlayerEmpire.CanDeployXaraktorVirus(out xaraktorVirus2, out cannotDeployReason2);
                        if (xaraktorVirus2 != null)
                        {
                            shipAction47 = new ShipAction(ShipActionType.DeployVirus, habitat4);
                            shipAction47.Target2 = xaraktorVirus2;
                            shipAction47.Hint = method_590(shipAction47);
                            if (!flag3)
                            {
                                ShipAction shipAction48 = shipAction47;
                                shipAction48.Hint = shipAction48.Hint + " (" + cannotDeployReason2.ToUpper(CultureInfo.InvariantCulture) + ")";
                                shipAction47.Enabled = false;
                            }
                        }
                    }
                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                    {
                        method_585(shipAction42, shipAction43, shipAction44, shipAction45, shipAction46, null, shipAction47, null);
                    }
                    else if (habitat4.GetPirateControl().CheckFactionHasControl(_Game.PlayerEmpire))
                    {
                        ShipAction shipAction49 = new ShipAction(ShipActionType.ColonyBuildOptions, habitat4);
                        shipAction49.Hint = method_590(shipAction49);
                        if (habitat4.ResolveBuildableFacilitiesPirates(_Game.PlayerEmpire).Count <= 0)
                        {
                            shipAction49.Enabled = false;
                            shipAction49.Hint = shipAction49.Hint + " (" + TextResolver.GetText("No buildable facilities") + ")";
                        }
                        method_585(shipAction41, shipAction43, shipAction44, shipAction45, shipAction46, null, shipAction47, shipAction49);
                    }
                    else
                    {
                        method_585(shipAction41, shipAction43, shipAction44, shipAction45, shipAction46, null, shipAction47, null);
                    }
                }
                else if (_Game.SelectedObject is BuiltObject)
                {
                    BuiltObject builtObject5 = (BuiltObject)_Game.SelectedObject;
                    if (builtObject5.Owner == _Game.PlayerEmpire)
                    {
                        if (builtObject5.IsFunctional && builtObject5.BuiltAt == null)
                        {
                            if (builtObject5.Role == BuiltObjectRole.Base)
                            {
                                ShipAction shipAction50 = null;
                                ShipAction shipAction_23 = null;
                                ShipAction shipAction51 = null;
                                ShipAction shipAction52 = null;
                                if (builtObject5.IsShipYard && builtObject5.ConstructionQueue != null && builtObject5.ConstructionQueue.ConstructionYards != null && builtObject5.ConstructionQueue.ConstructionYards.Count > 0)
                                {
                                    shipAction_23 = new ShipAction(ShipActionType.BuildOptions, builtObject5);
                                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                                    {
                                        shipAction51 = new ShipAction(ShipActionType.BuildOptionsPrivate, builtObject5);
                                    }
                                }
                                if (builtObject5.FighterCapacity > 0 || (builtObject5.Fighters != null && builtObject5.Fighters.Count > 0))
                                {
                                    shipAction52 = new ShipAction(ShipActionType.FighterOptions, builtObject5);
                                }
                                string text29 = string.Empty;
                                Design design31 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(builtObject5.SubRole, builtObject5.ParentHabitat);
                                ShipAction shipAction53 = new ShipAction(BuiltObjectMissionType.Retrofit, builtObject5, new Point(0, 0), design31);
                                double cost = 0.0;
                                ComponentList componentsToProcure = null;
                                _Game.PlayerEmpire.DetermineRetrofitAffordability(builtObject5, design31, out cost, out componentsToProcure);
                                if (design31 != null && design31 != builtObject5.Design)
                                {
                                    if (cost > _Game.PlayerEmpire.StateMoney)
                                    {
                                        shipAction53.Enabled = false;
                                        text29 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                    }
                                    else if (builtObject5.RetrofitDesign != null || builtObject5.BuiltAt != null)
                                    {
                                        shipAction53.Enabled = false;
                                        text29 = " (" + TextResolver.GetText("Already Retrofitting").ToUpper(CultureInfo.InvariantCulture) + ")";
                                    }
                                }
                                else
                                {
                                    shipAction53.Enabled = false;
                                    text29 = " (" + TextResolver.GetText("Already Latest Design").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                shipAction53.Hint = method_590(shipAction53);
                                shipAction53.Hint += text29;
                                if (builtObject5.DamagedComponentCount > 0 && (builtObject5.ParentHabitat == null || builtObject5.ParentHabitat.Empire != builtObject5.Empire))
                                {
                                    shipAction50 = new ShipAction(BuiltObjectMissionType.Build, builtObject5);
                                    shipAction50.Hint = string.Format(TextResolver.GetText("Queue construction ship to Repair X"), builtObject5.Name);
                                    if (builtObject5.Empire.CheckTargetOfRepairMission(builtObject5))
                                    {
                                        shipAction50.Enabled = false;
                                        ShipAction shipAction54 = shipAction50;
                                        shipAction54.Hint = shipAction54.Hint + " (" + TextResolver.GetText("Already Repairing").ToUpper(CultureInfo.InvariantCulture) + ")";
                                    }
                                }
                                ShipAction shipAction55 = new ShipAction(ShipActionType.GeneratePirateMissionDefend, builtObject5);
                                shipAction55.Hint = string.Format(arg1: _Game.PlayerEmpire.CalculatePirateDefendPrice(builtObject5).ToString("0"), format: TextResolver.GetText("Assign Mercenary Defense Mission"), arg0: builtObject5.Name);
                                if (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(builtObject5, EmpireActivityType.Defend))
                                {
                                    shipAction55.Enabled = false;
                                    shipAction55.Hint = shipAction55.Hint + " (" + TextResolver.GetText("Mission Already Assigned").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                if (shipAction51 != null)
                                {
                                    method_585(shipAction53, shipAction50, shipAction55, null, null, shipAction52, shipAction51, shipAction_23);
                                }
                                else
                                {
                                    method_585(shipAction53, shipAction50, shipAction55, null, null, null, shipAction52, shipAction_23);
                                }
                                return;
                            }
                            ShipAction shipAction56 = null;
                            ShipAction shipAction57 = null;
                            ShipAction shipAction_24 = null;
                            ShipAction shipAction_25 = null;
                            ShipAction shipAction58 = null;
                            ShipAction shipAction59 = null;
                            shipAction56 = ((!builtObject5.IsAutoControlled) ? new ShipAction(ShipActionType.AutomateShip, builtObject5) : new ShipAction(ShipActionType.UnautomateShip, builtObject5));
                            if (builtObject5.SubRole == BuiltObjectSubRole.ExplorationShip)
                            {
                                new ShipAction(BuiltObjectMissionType.Explore, builtObject5.NearestSystemStar);
                            }
                            string text30 = string.Empty;
                            if (builtObject5.TroopCapacity > 0)
                            {
                                shipAction57 = new ShipAction(BuiltObjectMissionType.LoadTroops, null);
                                if (builtObject5.TroopCapacityRemaining < 100)
                                {
                                    shipAction57.Enabled = false;
                                    text30 = " (" + TextResolver.GetText("Troop Carrier Full").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                shipAction57.Hint = method_590(shipAction57);
                                shipAction57.Hint += text30;
                            }
                            if (builtObject5.FighterCapacity > 0 || (builtObject5.Fighters != null && builtObject5.Fighters.Count > 0))
                            {
                                shipAction_24 = new ShipAction(ShipActionType.FighterOptions, builtObject5);
                            }
                            if (builtObject5.Role == BuiltObjectRole.Military && (builtObject5.FirepowerRaw > 0 || builtObject5.FighterCapacity > 0))
                            {
                                if (builtObject5.ShipGroup == null)
                                {
                                    ShipGroup target = null;
                                    double num8 = double.MaxValue;
                                    if (_Game.PlayerEmpire.ShipGroups != null && _Game.PlayerEmpire.ShipGroups.Count > 0)
                                    {
                                        for (int n = 0; n < _Game.PlayerEmpire.ShipGroups.Count; n++)
                                        {
                                            if (_Game.PlayerEmpire.ShipGroups[n].LeadShip != null)
                                            {
                                                double num9 = _Game.Galaxy.CalculateDistance(builtObject5.Xpos, builtObject5.Ypos, _Game.PlayerEmpire.ShipGroups[n].LeadShip.Xpos, _Game.PlayerEmpire.ShipGroups[n].LeadShip.Ypos);
                                                if (num9 < num8)
                                                {
                                                    target = _Game.PlayerEmpire.ShipGroups[n];
                                                    num8 = num9;
                                                }
                                            }
                                        }
                                    }
                                    shipAction_25 = new ShipAction(ShipActionType.JoinShipGroup, target);
                                }
                                else
                                {
                                    shipAction_25 = new ShipAction(ShipActionType.LeaveShipGroup, null);
                                }
                            }
                            ResourceList fuelTypes = builtObject5.DetermineFuelRequired();
                            StellarObject stellarObject = _Game.Galaxy.FastFindNearestRefuellingPoint(builtObject5.Xpos, builtObject5.Ypos, fuelTypes, builtObject5.ActualEmpire, builtObject5);
                            StellarObject stellarObject2 = _Game.PlayerEmpire.FindNearestShipYard(builtObject5, canRepairOrBuild: true, includeVerySmallYards: true);
                            if (builtObject5.UnbuiltOrDamagedComponentCount > 0)
                            {
                                shipAction58 = new ShipAction(BuiltObjectMissionType.Repair, stellarObject2);
                                if (stellarObject2 == null)
                                {
                                    shipAction58.Enabled = false;
                                }
                            }
                            else
                            {
                                shipAction58 = new ShipAction(BuiltObjectMissionType.Refuel, stellarObject);
                                if (stellarObject == null)
                                {
                                    shipAction58.Enabled = false;
                                }
                            }
                            if (builtObject5.DamagedComponentCount > 0)
                            {
                                if (builtObject5.TopSpeed == 0 || (builtObject5.WarpSpeed == 0 && builtObject5.Design != null && builtObject5.Design.WarpSpeed > 0))
                                {
                                    shipAction59 = new ShipAction(BuiltObjectMissionType.Build, builtObject5);
                                    shipAction59.Hint = string.Format(TextResolver.GetText("Queue construction ship to Repair X"), builtObject5.Name);
                                    if (builtObject5.Empire.CheckTargetOfRepairMission(builtObject5))
                                    {
                                        shipAction59.Enabled = false;
                                        ShipAction shipAction60 = shipAction59;
                                        shipAction60.Hint = shipAction60.Hint + " (" + TextResolver.GetText("Already Repairing").ToUpper(CultureInfo.InvariantCulture) + ")";
                                    }
                                }
                            }
                            else
                            {
                                string text31 = string.Empty;
                                Design design32 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(builtObject5.SubRole, builtObject5.ParentHabitat);
                                shipAction59 = new ShipAction(BuiltObjectMissionType.Retrofit, stellarObject2, new Point(0, 0), design32);
                                double cost2 = 0.0;
                                ComponentList componentsToProcure2 = null;
                                _Game.PlayerEmpire.DetermineRetrofitAffordability(builtObject5, design32, out cost2, out componentsToProcure2);
                                if (design32 != null && design32 != builtObject5.Design)
                                {
                                    if (cost2 > _Game.PlayerEmpire.StateMoney)
                                    {
                                        shipAction59.Enabled = false;
                                        text31 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                    }
                                    else if (builtObject5.RetrofitDesign != null || builtObject5.BuiltAt != null || (builtObject5.Mission != null && builtObject5.Mission.Type == BuiltObjectMissionType.Retrofit))
                                    {
                                        shipAction59.Enabled = false;
                                        text31 = " (" + TextResolver.GetText("Already Retrofitting").ToUpper(CultureInfo.InvariantCulture) + ")";
                                    }
                                }
                                else
                                {
                                    shipAction59.Enabled = false;
                                    text31 = " (" + TextResolver.GetText("Already Latest Design").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                if (stellarObject2 == null)
                                {
                                    shipAction59.Enabled = false;
                                }
                                shipAction59.Hint = method_590(shipAction59);
                                shipAction59.Hint += text31;
                            }
                            method_585(new ShipAction(BuiltObjectMissionType.Hold, _Game.SelectedObject), shipAction58, shipAction59, new ShipAction(BuiltObjectMissionType.Escape, null), shipAction57, shipAction_25, shipAction56, shipAction_24);
                            return;
                        }
                        ShipAction shipAction61 = null;
                        if (builtObject5.DamagedComponentCount > 0)
                        {
                            if (builtObject5.Role == BuiltObjectRole.Base && (builtObject5.ParentHabitat == null || builtObject5.ParentHabitat.Empire != builtObject5.Empire))
                            {
                                shipAction61 = new ShipAction(BuiltObjectMissionType.Build, builtObject5);
                            }
                            else if (builtObject5.Role != BuiltObjectRole.Base && builtObject5.Owner == _Game.PlayerEmpire)
                            {
                                if (builtObject5.TopSpeed != 0 && (builtObject5.WarpSpeed != 0 || builtObject5.Design == null || builtObject5.Design.WarpSpeed <= 0))
                                {
                                    shipAction61 = new ShipAction(BuiltObjectMissionType.Repair, null);
                                    StellarObject stellarObject3 = builtObject5.Empire.FindNearestShipYard(builtObject5, canRepairOrBuild: true, includeVerySmallYards: true);
                                    if (stellarObject3 != null)
                                    {
                                        shipAction61.Target = stellarObject3;
                                        shipAction61.Hint = string.Format(TextResolver.GetText("Repair at X"), stellarObject3.Name);
                                    }
                                    else
                                    {
                                        shipAction61 = new ShipAction(BuiltObjectMissionType.Build, builtObject5);
                                    }
                                }
                                else
                                {
                                    shipAction61 = new ShipAction(BuiltObjectMissionType.Build, builtObject5);
                                }
                            }
                        }
                        if (shipAction61 != null && string.IsNullOrEmpty(shipAction61.Hint))
                        {
                            shipAction61.Hint = string.Format(TextResolver.GetText("Queue construction ship to Repair X"), builtObject5.Name);
                            if (builtObject5.Empire.CheckTargetOfRepairMission(builtObject5))
                            {
                                shipAction61.Enabled = false;
                                ShipAction shipAction62 = shipAction61;
                                shipAction62.Hint = shipAction62.Hint + " (" + TextResolver.GetText("Already Repairing").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                        }
                        method_585(shipAction61, null, null, null, null, null, null, null);
                        return;
                    }
                    if (builtObject5.Empire == _Game.PlayerEmpire)
                    {
                        if (builtObject5.Role == BuiltObjectRole.Base)
                        {
                            ShipAction shipAction_26 = new ShipAction(BuiltObjectMissionType.Retire, builtObject5);
                            string text32 = string.Empty;
                            Design design33 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(builtObject5.SubRole, builtObject5.ParentHabitat);
                            ShipAction shipAction63 = new ShipAction(BuiltObjectMissionType.Retrofit, builtObject5, new Point(0, 0), design33);
                            double cost3 = 0.0;
                            ComponentList componentsToProcure3 = null;
                            _Game.PlayerEmpire.DetermineRetrofitAffordability(builtObject5, design33, out cost3, out componentsToProcure3);
                            if (design33 != null && design33 != builtObject5.Design)
                            {
                                if (cost3 > _Game.PlayerEmpire.StateMoney)
                                {
                                    shipAction63.Enabled = false;
                                    text32 = " (" + TextResolver.GetText("Not Enough Money").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                                else if (builtObject5.RetrofitDesign != null || builtObject5.BuiltAt != null)
                                {
                                    shipAction63.Enabled = false;
                                    text32 = " (" + TextResolver.GetText("Already Retrofitting").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                            }
                            else
                            {
                                shipAction63.Enabled = false;
                                text32 = " (" + TextResolver.GetText("Already Latest Design").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            shipAction63.Hint = method_590(shipAction63);
                            shipAction63.Hint += text32;
                            ShipAction shipAction64 = null;
                            if (builtObject5.DamagedComponentCount > 0)
                            {
                                if (builtObject5.Role == BuiltObjectRole.Base && (builtObject5.ParentHabitat == null || builtObject5.ParentHabitat.Empire != builtObject5.Empire))
                                {
                                    shipAction64 = new ShipAction(BuiltObjectMissionType.Build, builtObject5);
                                }
                                else if (builtObject5.Role != BuiltObjectRole.Base && builtObject5.Owner == _Game.PlayerEmpire && (builtObject5.TopSpeed == 0 || builtObject5.WarpSpeed == 0))
                                {
                                    shipAction64 = new ShipAction(BuiltObjectMissionType.Build, builtObject5);
                                }
                            }
                            if (shipAction64 != null)
                            {
                                shipAction64.Hint = string.Format(TextResolver.GetText("Queue construction ship to Repair X"), builtObject5.Name);
                                if (builtObject5.Empire.CheckTargetOfRepairMission(builtObject5))
                                {
                                    shipAction64.Enabled = false;
                                    ShipAction shipAction65 = shipAction64;
                                    shipAction65.Hint = shipAction65.Hint + " (" + TextResolver.GetText("Already Repairing").ToUpper(CultureInfo.InvariantCulture) + ")";
                                }
                            }
                            ShipAction shipAction66 = new ShipAction(ShipActionType.GeneratePirateMissionDefend, builtObject5);
                            shipAction66.Hint = string.Format(arg1: _Game.PlayerEmpire.CalculatePirateDefendPrice(builtObject5).ToString("0"), format: TextResolver.GetText("Assign Mercenary Defense Mission"), arg0: builtObject5.Name);
                            if (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(builtObject5, EmpireActivityType.Defend))
                            {
                                shipAction66.Enabled = false;
                                shipAction66.Hint = shipAction66.Hint + " (" + TextResolver.GetText("Mission Already Assigned").ToUpper(CultureInfo.InvariantCulture) + ")";
                            }
                            method_585(shipAction_26, shipAction63, shipAction64, shipAction66, null, null, null, null);
                        }
                        else
                        {
                            method_585(null, null, null, null, null, null, null, null);
                        }
                        return;
                    }
                    if (builtObject5.Empire != null && builtObject5.Empire.PirateEmpireBaseHabitat != null && builtObject5.Role == BuiltObjectRole.Base)
                    {
                        ShipAction shipAction67 = new ShipAction(ShipActionType.AssignAttack, builtObject5);
                        int missionQueueIndex = -1;
                        ShipGroup shipGroup = itemListCollectionPanel_0.ResolveAssignedFleet(new PrioritizedTarget(builtObject5, 1000), out missionQueueIndex);
                        if (shipGroup != null)
                        {
                            shipAction67.Hint = TextResolver.GetText("Attack with nearest available fleet") + " (" + shipGroup.Name.ToUpper(CultureInfo.InvariantCulture) + " " + TextResolver.GetText("Already Attacking").ToUpper(CultureInfo.InvariantCulture) + ")";
                            shipAction67.Enabled = false;
                        }
                        ShipAction shipAction68 = new ShipAction(ShipActionType.GeneratePirateMissionAttack, builtObject5);
                        shipAction68.Hint = string.Format(arg1: _Game.PlayerEmpire.CalculatePirateAttackPrice(builtObject5).ToString("0"), format: TextResolver.GetText("Assign Mercenary Attack Mission"), arg0: builtObject5.Name);
                        if (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(builtObject5, EmpireActivityType.Attack))
                        {
                            shipAction68.Enabled = false;
                            shipAction68.Hint = shipAction68.Hint + " (" + TextResolver.GetText("Mission Already Assigned").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        method_585(shipAction67, shipAction68, null, null, null, null, null, null);
                        return;
                    }
                    ShipAction shipAction69 = null;
                    if (builtObject5.Role == BuiltObjectRole.Base)
                    {
                        shipAction69 = new ShipAction(ShipActionType.GeneratePirateMissionAttack, builtObject5);
                        shipAction69.Hint = string.Format(arg1: _Game.PlayerEmpire.CalculatePirateAttackPrice(builtObject5).ToString("0"), format: TextResolver.GetText("Assign Mercenary Attack Mission"), arg0: builtObject5.Name);
                        if (_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(builtObject5, EmpireActivityType.Attack))
                        {
                            shipAction69.Enabled = false;
                            ShipAction shipAction70 = shipAction69;
                            shipAction70.Hint = shipAction70.Hint + " (" + TextResolver.GetText("Mission Already Assigned").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                    }
                    method_585(shipAction69, null, null, null, null, null, null, null);
                }
                else if (_Game.SelectedObject is Fighter)
                {
                    Fighter fighter = (Fighter)_Game.SelectedObject;
                    if (fighter.Empire == _Game.PlayerEmpire)
                    {
                        ShipAction shipAction_27 = null;
                        if (!fighter.UnderConstruction)
                        {
                            shipAction_27 = (fighter.OnboardCarrier ? ((fighter.Specification.Type != FighterType.Bomber) ? new ShipAction(ShipActionType.FighterLaunchFighters, fighter) : new ShipAction(ShipActionType.FighterLaunchBombers, fighter)) : ((fighter.Specification.Type != FighterType.Bomber) ? new ShipAction(ShipActionType.FighterRetrieveFighters, fighter) : new ShipAction(ShipActionType.FighterRetrieveBombers, fighter)));
                        }
                        ShipAction shipAction_28 = new ShipAction(BuiltObjectMissionType.Retire, fighter);
                        method_585(shipAction_27, null, null, null, null, null, null, shipAction_28);
                    }
                }
                else if (_Game.SelectedObject is Creature)
                {
                    _ = (Creature)_Game.SelectedObject;
                    method_585(null, null, null, null, null, null, null, null);
                }
                else if (_Game.SelectedObject is ShipGroup)
                {
                    ShipGroup shipGroup2 = (ShipGroup)_Game.SelectedObject;
                    if (shipGroup2.Empire == _Game.PlayerEmpire)
                    {
                        ShipAction shipAction71 = new ShipAction(ShipActionType.AutomateShip, shipGroup2);
                        if (shipGroup2.LeadShip.IsAutoControlled)
                        {
                            shipAction71.ActionType = ShipActionType.UnautomateShip;
                        }
                        ResourceList resourceList2 = shipGroup2.CalculateRequiredFuel();
                        StellarObject stellarObject4 = _Game.PlayerEmpire.DecideBestFleetRefuelPoint(shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos, shipGroup2.Empire, resourceList2, null);
                        ShipAction shipAction72 = new ShipAction(BuiltObjectMissionType.Refuel, stellarObject4);
                        if (shipGroup2.TotalDamage > 0)
                        {
                            StellarObject stellarObject5 = _Game.PlayerEmpire.FindNearestShipYard(shipGroup2.LeadShip, canRepairOrBuild: true, includeVerySmallYards: false);
                            shipAction72 = new ShipAction(BuiltObjectMissionType.Repair, stellarObject5);
                            if (stellarObject5 == null)
                            {
                                shipAction72.Enabled = false;
                            }
                        }
                        else if (stellarObject4 == null)
                        {
                            stellarObject4 = _Game.Galaxy.FastFindNearestRefuellingPoint(shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos, resourceList2, shipGroup2.Empire, shipGroup2.LeadShip, includeResupplyShips: true, null, shipGroup2.Ships.Count);
                            shipAction72 = new ShipAction(BuiltObjectMissionType.Refuel, stellarObject4);
                            if (stellarObject4 == null)
                            {
                                shipAction72.Enabled = false;
                            }
                        }
                        new ShipAction(BuiltObjectMissionType.Retrofit, shipGroup2);
                        ShipAction shipAction73 = new ShipAction(BuiltObjectMissionType.LoadTroops, shipGroup2);
                        ShipAction shipAction74 = new ShipAction(BuiltObjectMissionType.Move, shipGroup2.GatherPoint);
                        if (shipGroup2.GatherPoint == null)
                        {
                            shipAction74.Enabled = false;
                        }
                        string text33 = string.Empty;
                        if (shipGroup2.TotalTroopSpaceRemaining < 100)
                        {
                            shipAction73.Enabled = false;
                            text33 = " (" + TextResolver.GetText("All Troop Carriers Full").ToUpper(CultureInfo.InvariantCulture) + ")";
                        }
                        shipAction73.Hint = method_590(shipAction73);
                        shipAction73.Hint += text33;
                        ShipAction shipAction75 = new ShipAction(ShipActionType.SetFleetHomeBase, shipGroup2);
                        shipAction75.Hint = method_590(shipAction75);
                        ShipAction shipAction76 = new ShipAction(ShipActionType.SetFleetAttackPoint, shipGroup2);
                        shipAction76.Hint = method_590(shipAction76);
                        ShipAction shipAction77 = new ShipAction(ShipActionType.SetFleetPosture, shipGroup2);
                        shipAction77.Hint = method_590(shipAction77);
                        ShipAction shipAction78 = new ShipAction(ShipActionType.SetFleetRange, shipGroup2);
                        shipAction78.Hint = method_590(shipAction78);
                        method_585(new ShipAction(BuiltObjectMissionType.Hold, shipGroup2), shipAction72, shipAction73, shipAction75, shipAction76, shipAction77, shipAction78, shipAction71);
                    }
                    else
                    {
                        method_585(null, null, null, null, null, null, null, null);
                    }
                }
                else if (_Game.SelectedObject is BuiltObjectList)
                {
                    BuiltObjectList builtObjectList = (BuiltObjectList)_Game.SelectedObject;
                    if (builtObjectList == null || builtObjectList.Count <= 0)
                    {
                        return;
                    }
                    _Game.PlayerEmpire.FindNearestRefuellingPoint(builtObjectList[0].Xpos, builtObjectList[0].Ypos, builtObjectList[0].FuelType, 3);
                    ShipAction shipAction_29 = new ShipAction(BuiltObjectMissionType.Refuel, builtObjectList);
                    int num10 = 0;
                    for (int num11 = 0; num11 < builtObjectList.Count; num11++)
                    {
                        if (builtObjectList[num11] != null && builtObjectList[num11].DamagedComponentCount > 0)
                        {
                            num10 += builtObjectList[num11].DamagedComponentCount;
                        }
                    }
                    if (num10 > 0)
                    {
                        shipAction_29 = new ShipAction(BuiltObjectMissionType.Repair, builtObjectList);
                    }
                    method_585(new ShipAction(BuiltObjectMissionType.Hold, builtObjectList), shipAction_29, new ShipAction(BuiltObjectMissionType.Escape, builtObjectList), new ShipAction(ShipActionType.CreateNewFleet, builtObjectList), null, null, null, null);
                }
                else if (_Game.SelectedObject is Fighter)
                {
                    Fighter fighter2 = (Fighter)_Game.SelectedObject;
                    if (fighter2.Empire == _Game.PlayerEmpire)
                    {
                        method_585(null, null, null, null, null, null, null, null);
                    }
                }
                else if (_Game.SelectedObject is SystemInfo)
                {
                    _ = (SystemInfo)_Game.SelectedObject;
                    method_585(null, null, null, null, null, null, null, null);
                }
            }
            else
            {
                method_585(null, null, null, null, null, null, null, null);
            }
        }

        private void method_594(object object_7)
        {
            if (object_7 != null && object_7 is ShipAction)
            {
                ShipAction shipAction = (ShipAction)object_7;
                method_347(shipAction, bool_28: false);
                if (shipAction.ActionType != 0)
                {
                    switch (shipAction.ActionType)
                    {
                        case ShipActionType.FighterBuildFighter:
                        case ShipActionType.FighterBuildBomber:
                        case ShipActionType.FighterLaunchFighters:
                        case ShipActionType.FighterLaunchBombers:
                        case ShipActionType.FighterRetrieveFighters:
                        case ShipActionType.FighterRetrieveBombers:
                            if (!(shipAction.Target is Fighter))
                            {
                                method_593(new ShipAction(ShipActionType.FighterOptions, null));
                            }
                            break;
                        case ShipActionType.BuildPlanetaryFacility:
                            if (shipAction.Target is PlanetaryFacilityDefinition)
                            {
                                PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)shipAction.Target;
                                if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.Wonder)
                                {
                                    method_593(new ShipAction(ShipActionType.ColonyBuildWonder, null));
                                }
                                else
                                {
                                    method_593(new ShipAction(ShipActionType.ColonyBuildOptions, null));
                                }
                            }
                            break;
                        case ShipActionType.SetFleetPosture:
                        case ShipActionType.SetFleetRange:
                            method_592();
                            pnlDetailInfo.Invalidate();
                            break;
                        case ShipActionType.AutomateShip:
                        case ShipActionType.JoinShipGroup:
                        case ShipActionType.LeaveShipGroup:
                        case ShipActionType.BuildColonize:
                        case ShipActionType.UnautomateShip:
                            method_592();
                            break;
                    }
                }
                else
                {
                    _ = shipAction.MissionType;
                    switch (shipAction.MissionType)
                    {
                        case BuiltObjectMissionType.Retrofit:
                            method_592();
                            break;
                        case BuiltObjectMissionType.Build:
                            if (shipAction.Design != null)
                            {
                                switch (shipAction.Design.SubRole)
                                {
                                    default:
                                        method_593(new ShipAction(ShipActionType.BuildOptions, null));
                                        break;
                                    case BuiltObjectSubRole.SmallFreighter:
                                    case BuiltObjectSubRole.MediumFreighter:
                                    case BuiltObjectSubRole.LargeFreighter:
                                    case BuiltObjectSubRole.PassengerShip:
                                    case BuiltObjectSubRole.GasMiningShip:
                                    case BuiltObjectSubRole.MiningShip:
                                        method_593(new ShipAction(ShipActionType.BuildOptionsPrivate, null));
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
            Focus();
        }

        private void btnEmpirePolicyLoad_Click(object sender, EventArgs e)
        {
            if (_Game == null || _Game.PlayerEmpire == null)
            {
                return;
            }
            bool flag = false;
            string text = Application.StartupPath + "\\Policy\\";
            string text2 = GetCustomizationPath();
            string text3 = text;
            if (!string.IsNullOrEmpty(text2))
            {
                text3 = text2 + "Policy\\";
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = text3;
            method_524(text3);
            openFileDialog.Filter = TextResolver.GetText("Distant Worlds empire policy files") + " (*.txt)|*.txt";
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Title = TextResolver.GetText("Load Distant Worlds empire policy");
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    openFileDialog.Dispose();
                    if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                    {
                        flag = true;
                        method_154();
                    }
                    method_92();
                    Cursor.Current = Cursors.WaitCursor;
                    _Game.PlayerEmpire.Policy.LoadFromFile(openFileDialog.FileName);
                    Galaxy.ApplyDesignUpgradePoliciesToGameOptions(gameOptions_0, _Game.PlayerEmpire.Policy);
                    method_595();
                }
                else
                {
                    openFileDialog.Dispose();
                }
            }
            else
            {
                openFileDialog.Dispose();
            }
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused && flag)
            {
                method_155();
            }
            method_90();
        }

        private void btnEmpirePolicySave_Click(object sender, EventArgs e)
        {
            if (_Game == null || _Game.PlayerEmpire == null)
            {
                return;
            }
            bool flag = false;
            string text = Application.StartupPath + "\\Policy\\";
            string text2 = GetCustomizationPath();
            string text3 = text;
            if (!string.IsNullOrEmpty(text2))
            {
                text3 = text2 + "Policy\\";
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = text3;
            method_524(text3);
            saveFileDialog.Filter = TextResolver.GetText("Distant Worlds empire policy files") + " (*.txt)|*.txt";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Title = TextResolver.GetText("Save Distant Worlds empire policy");
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    saveFileDialog.Dispose();
                    if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                    {
                        flag = true;
                        method_154();
                    }
                    method_92();
                    Cursor.Current = Cursors.WaitCursor;
                    _Game.PlayerEmpire.Policy = method_597(nDrsqatloR, _Game.PlayerEmpire);
                    _Game.PlayerEmpire.Policy.SaveToFile(saveFileDialog.FileName);
                }
                else
                {
                    saveFileDialog.Dispose();
                }
            }
            else
            {
                saveFileDialog.Dispose();
            }
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused && flag)
            {
                method_155();
            }
            method_90();
        }

        private void method_595()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlEmpirePolicy.Size = new Size(965, 760);
            pnlEmpirePolicy.Location = new Point((mainView.Width - pnlEmpirePolicy.Width) / 2, (mainView.Height - pnlEmpirePolicy.Height) / 2);
            pnlEmpirePolicy.DoLayout();
            pnlEmpirePolicyContainer.AutoSize = false;
            pnlEmpirePolicyContainer.Size = new Size(930, 635);
            pnlEmpirePolicyContainer.Location = new Point(10, 10);
            pnlEmpirePolicyContainer.AutoScroll = false;
            pnlEmpirePolicyContainer.AutoScrollPosition = new Point(0, 0);
            pnlEmpirePolicyContainer.HorizontalScroll.Maximum = 0;
            pnlEmpirePolicyContainer.VerticalScroll.Visible = false;
            pnlEmpirePolicyContainer.AutoScroll = true;
            pnlEmpirePolicyContainer.BackColor = Color.FromArgb(32, 32, 32);
            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
            {
                nDrsqatloR.Size = new Size(910, 4860);
            }
            else
            {
                bool flag = false;
                if (_Game.PlayerEmpire.CheckEmpireHasOwnedColonies(_Game.PlayerEmpire))
                {
                    flag = true;
                }
                nDrsqatloR.Size = new Size(910, 2860);
                if (flag)
                {
                    nDrsqatloR.Size = new Size(910, 4560);
                }
            }
            nDrsqatloR.Location = new Point(0, 0);
            int num = (pnlEmpirePolicy.Width - 630) / 2;
            WqesexberY.Size = new Size(180, 30);
            WqesexberY.Location = new Point(num, 656);
            XgxsOtuAmD.Size = new Size(180, 30);
            XgxsOtuAmD.Location = new Point(num + 190, 656);
            btnEmpirePolicyLoad.Size = new Size(110, 30);
            btnEmpirePolicyLoad.Location = new Point(num + 400, 656);
            btnEmpirePolicySave.Size = new Size(110, 30);
            btnEmpirePolicySave.Location = new Point(num + 520, 656);
            method_609(nDrsqatloR, _Game.PlayerEmpire.Policy, _Game.PlayerEmpire);
            pnlEmpirePolicy.Visible = true;
            pnlEmpirePolicy.BringToFront();
        }

        private void method_596()
        {
            pnlEmpirePolicy.SendToBack();
            pnlEmpirePolicy.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
            SetMainFocus();
        }

        private EmpirePolicy method_597(System.Windows.Forms.Panel panel_1, Empire empire_5)
        {
            EmpirePolicy empirePolicy = new EmpirePolicy();
            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
            {
                empire_5.ControlColonization = (AutomationLevel)method_605(panel_1, "AutomationColonization");
                empire_5.ControlColonyFacilities = (AutomationLevel)method_605(panel_1, "AutomationColonyFacilityBuilding");
                empire_5.ControlColonyTaxRates = Convert.ToBoolean(method_605(panel_1, "AutomationColonyTaxRates"));
                empire_5.ControlDiplomacyGifts = (AutomationLevel)method_605(panel_1, "AutomationDiplomacyGifts");
                empire_5.ControlDiplomacyOffense = (AutomationLevel)method_605(panel_1, "AutomationWarTradeSanctions");
                empire_5.ControlDiplomacyTreaties = (AutomationLevel)method_605(panel_1, "AutomationTreaties");
                empire_5.ControlTroopGeneration = Convert.ToBoolean(method_605(panel_1, "AutomationTroopRecruitment"));
            }
            empire_5.ControlAgentAssignment = (AutomationLevel)method_605(panel_1, "AutomationAgentAssignment");
            empire_5.ControlDesigns = Convert.ToBoolean(method_605(panel_1, "AutomationDesigns"));
            empire_5.ControlMilitaryAttacks = (AutomationLevel)method_605(panel_1, "AutomationAttackTargets");
            empire_5.ControlMilitaryFleets = Convert.ToBoolean(method_605(panel_1, "AutomationFleets"));
            empire_5.ControlResearch = Convert.ToBoolean(method_605(panel_1, "AutomationResearch"));
            empire_5.ControlStateConstruction = (AutomationLevel)method_605(panel_1, "AutomationConstruction");
            empirePolicy.IntelligenceCounterIntelligenceProportion = method_604(panel_1, "IntelligenceCounterIntelligenceProportion");
            empirePolicy.DiplomacySendGiftsUpToAmount = (int)method_604(panel_1, "DiplomacySendGiftsUpToAmount");
            empirePolicy.ColonyFacilityPopulationThresholdFortifiedBunker = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdFortifiedBunker");
            empirePolicy.ColonyFacilityPopulationThresholdTroopTrainingCenter = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdTroopTrainingCenter");
            empirePolicy.ColonyFacilityPopulationThresholdRoboticTroopFoundry = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdRoboticTroopFoundry");
            empirePolicy.ColonyFacilityPopulationThresholdCloningFacility = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdCloningFacility");
            empirePolicy.ColonyFacilityPopulationThresholdPlanetaryShield = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdPlanetaryShield");
            empirePolicy.ColonyFacilityPopulationThresholdGiantIonCannon = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdGiantIonCannon");
            empirePolicy.ColonyFacilityPopulationThresholdRegionalCapital = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdRegionalCapital");
            empirePolicy.ColonyFacilityPopulationThresholdTerraformingFacility = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdTerraformingFacility");
            empirePolicy.ColonyFacilityPopulationThresholdArmoredFactory = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdArmoredFactory");
            empirePolicy.ColonyFacilityPopulationThresholdSpyAcademy = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdSpyAcademy");
            empirePolicy.ColonyFacilityPopulationThresholdScienceAcademy = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdScienceAcademy");
            empirePolicy.ColonyFacilityPopulationThresholdNavalAcademy = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdNavalAcademy");
            empirePolicy.ColonyFacilityPopulationThresholdMilitaryAcademy = (int)method_604(panel_1, "ColonyFacilityPopulationThresholdMilitaryAcademy");
            empirePolicy.ColonyPopulationThresholdTroopRecruitment = (int)method_604(panel_1, "ColonyPopulationThresholdTroopRecruitment");
            empirePolicy.ConstructionMilitaryEscort = method_604(panel_1, "ConstructionMilitaryEscort");
            empirePolicy.ConstructionMilitaryFrigate = method_604(panel_1, "ConstructionMilitaryFrigate");
            empirePolicy.ConstructionMilitaryDestroyer = method_604(panel_1, "ConstructionMilitaryDestroyer");
            empirePolicy.ConstructionMilitaryCruiser = method_604(panel_1, "ConstructionMilitaryCruiser");
            empirePolicy.ConstructionMilitaryCapitalShip = method_604(panel_1, "ConstructionMilitaryCapitalShip");
            empirePolicy.ConstructionMilitaryCarrier = method_604(panel_1, "ConstructionMilitaryCarrier");
            empirePolicy.ConstructionMilitaryTroopTransport = method_604(panel_1, "ConstructionMilitaryTroopTransport");
            empirePolicy.ConstructionMilitaryCarrier = method_604(panel_1, "ConstructionMilitaryCarrier");
            empirePolicy.ConstructionSpaceportMinimumDistance = (int)method_604(panel_1, "ConstructionSpaceportMinimumDistance");
            empirePolicy.ConstructionSpaceportSmallColonyPopulationThreshold = (int)method_604(panel_1, "ConstructionSpaceportSmallColonyPopulationThreshold");
            empirePolicy.ConstructionSpaceportMediumColonyPopulationThreshold = (int)method_604(panel_1, "ConstructionSpaceportMediumColonyPopulationThreshold");
            empirePolicy.ConstructionSpaceportLargeColonyPopulationThreshold = (int)method_604(panel_1, "ConstructionSpaceportLargeColonyPopulationThreshold");
            empirePolicy.FleetMilitaryProportionForFleets = method_604(panel_1, "FleetMilitaryProportionForFleets");
            empirePolicy.FleetTypicalSize = (int)method_604(panel_1, "FleetTypicalSize");
            empirePolicy.FleetStrikeForceTypicalSize = (int)method_604(panel_1, "FleetStrikeForceTypicalSize");
            empirePolicy.IntelligenceAllowMissionStealTerritoryMap = method_603(panel_1, "IntelligenceAllowMissionStealTerritoryMap");
            empirePolicy.IntelligenceAllowMissionStealGalaxyMap = method_603(panel_1, "IntelligenceAllowMissionStealGalaxyMap");
            empirePolicy.IntelligenceAllowMissionStealOperationsMap = method_603(panel_1, "IntelligenceAllowMissionStealOperationsMap");
            empirePolicy.IntelligenceAllowMissionSabotageColony = method_603(panel_1, "IntelligenceAllowMissionSabotageColony");
            empirePolicy.IntelligenceAllowMissionSabotageConstruction = method_603(panel_1, "IntelligenceAllowMissionSabotageConstruction");
            empirePolicy.IntelligenceAllowMissionStealTechData = method_603(panel_1, "IntelligenceAllowMissionStealTechData");
            empirePolicy.IntelligenceAllowMissionInciteRevolution = method_603(panel_1, "IntelligenceAllowMissionInciteRevolution");
            empirePolicy.IntelligenceAllowMissionDeepCover = method_603(panel_1, "IntelligenceAllowMissionDeepCover");
            empirePolicy.IntelligenceAllowMissionAssassinateCharacter = method_603(panel_1, "IntelligenceAllowMissionAssassinateCharacter");
            empirePolicy.IntelligenceAllowMissionDestroyBase = method_603(panel_1, "IntelligenceAllowMissionDestroyBase");
            empirePolicy.DiplomacyTradeSanctionsUseBlockades = method_603(panel_1, "DiplomacyTradeSanctionsUseBlockades");
            empirePolicy.ColonyActionForNewTroopRecruitment = method_603(panel_1, "ColonyActionForNewTroopRecruitment");
            empirePolicy.ColonyAllowFacilityFortifiedBunker = method_603(panel_1, "ColonyAllowFacilityFortifiedBunker");
            empirePolicy.ColonyAllowFacilityTroopTrainingCenter = method_603(panel_1, "ColonyAllowFacilityTroopTrainingCenter");
            empirePolicy.ColonyAllowFacilityRoboticTroopFoundry = method_603(panel_1, "ColonyAllowFacilityRoboticTroopFoundry");
            empirePolicy.ColonyAllowFacilityCloningFacility = method_603(panel_1, "ColonyAllowFacilityCloningFacility");
            empirePolicy.ColonyAllowFacilityPlanetaryShield = method_603(panel_1, "ColonyAllowFacilityPlanetaryShield");
            empirePolicy.ColonyAllowFacilityGiantIonCannon = method_603(panel_1, "ColonyAllowFacilityGiantIonCannon");
            empirePolicy.ColonyAllowFacilityRegionalCapital = method_603(panel_1, "ColonyAllowFacilityRegionalCapital");
            empirePolicy.ColonyAllowFacilityTerraformingFacility = method_603(panel_1, "ColonyAllowFacilityTerraformingFacility");
            empirePolicy.ColonyAllowFacilityArmoredFactory = method_603(panel_1, "ColonyAllowFacilityArmoredFactory");
            empirePolicy.ColonyAllowFacilitySpyAcademy = method_603(panel_1, "ColonyAllowFacilitySpyAcademy");
            empirePolicy.ColonyAllowFacilityScienceAcademy = method_603(panel_1, "ColonyAllowFacilityScienceAcademy");
            empirePolicy.ColonyAllowFacilityNavalAcademy = method_603(panel_1, "ColonyAllowFacilityNavalAcademy");
            empirePolicy.ColonyAllowFacilityMilitaryAcademy = method_603(panel_1, "ColonyAllowFacilityMilitaryAcademy");
            empirePolicy.ColonyTaxRateIncreaseWhenAtWar = method_603(panel_1, "ColonyTaxRateIncreaseWhenAtWar");
            empirePolicy.ResearchDesignAutoRetrofit = method_603(panel_1, "ResearchDesignAutoRetrofit");
            empirePolicy.WarAttacksHarassEnemies = method_603(panel_1, "WarAttacksHarassEnemies");
            empirePolicy.ResearchDesignAutoUpgradeFighters = method_603(panel_1, "ResearchDesignAutoUpgradeFighters");
            empirePolicy.TradeWithOtherEmpires = method_603(panel_1, "EconomyTradeWithOtherEmpires");
            empirePolicy.EngageInTourism = method_603(panel_1, "EconomyEngageInTourism");
            empirePolicy.ImplementEnslavementWithPenalColonies = method_603(panel_1, "ImplementEnslavementWithPenalColonies");
            empirePolicy.IntelligenceUseEspionageAgainstEmpireWhen = method_605(panel_1, "IntelligenceUseEspionageAgainstEmpireWhen");
            empirePolicy.IntelligenceUseSabotageAgainstEmpireWhen = method_605(panel_1, "IntelligenceUseSabotageAgainstEmpireWhen");
            empirePolicy.ColonyActionForNewBuildDesign = dFwNhteflw(panel_1, "ColonyActionForNewBuildDesign");
            empirePolicy.NewColonyPopulationPolicyYourRaceFamily = method_601(panel_1, "NewColonyPopulationPolicyYourRaceFamily");
            empirePolicy.NewColonyPopulationPolicyAllRaces = method_601(panel_1, "NewColonyPopulationPolicyAllRaces");
            empirePolicy.ColonyTaxRateSmallColony = method_605(panel_1, "ColonyTaxRateSmallColony");
            empirePolicy.ColonyTaxRateMediumColony = method_605(panel_1, "ColonyTaxRateMediumColony");
            empirePolicy.ColonyTaxRateLargeColony = method_605(panel_1, "ColonyTaxRateLargeColony");
            empirePolicy.ResearchDesignOverallFocus = (ShipDesignFocus)method_605(panel_1, "ResearchDesignOverallFocus");
            ComponentCategoryType category = ComponentCategoryType.Undefined;
            ComponentType type = ComponentType.Undefined;
            Galaxy.ResolveTechFocus(method_605(panel_1, "ResearchDesignTechFocus1"), out category, out type);
            empirePolicy.ResearchDesignTechFocus1 = category;
            empirePolicy.ResearchDesignTechFocusType1 = type;
            ComponentCategoryType category2 = ComponentCategoryType.Undefined;
            ComponentType type2 = ComponentType.Undefined;
            Galaxy.ResolveTechFocus(method_605(panel_1, "ResearchDesignTechFocus2"), out category2, out type2);
            empirePolicy.ResearchDesignTechFocus2 = category2;
            empirePolicy.ResearchDesignTechFocusType2 = type2;
            ComponentCategoryType category3 = ComponentCategoryType.Undefined;
            ComponentType type3 = ComponentType.Undefined;
            Galaxy.ResolveTechFocus(method_605(panel_1, "ResearchDesignTechFocus3"), out category3, out type3);
            empirePolicy.ResearchDesignTechFocus3 = category3;
            empirePolicy.ResearchDesignTechFocusType3 = type3;
            ComponentCategoryType category4 = ComponentCategoryType.Undefined;
            ComponentType type4 = ComponentType.Undefined;
            Galaxy.ResolveTechFocus(method_605(panel_1, "ResearchDesignTechFocus4"), out category4, out type4);
            empirePolicy.ResearchDesignTechFocus4 = category4;
            empirePolicy.ResearchDesignTechFocusType4 = type4;
            ComponentCategoryType category5 = ComponentCategoryType.Undefined;
            ComponentType type5 = ComponentType.Undefined;
            Galaxy.ResolveTechFocus(method_605(panel_1, "ResearchDesignTechFocus5"), out category5, out type5);
            empirePolicy.ResearchDesignTechFocus5 = category5;
            empirePolicy.ResearchDesignTechFocusType5 = type5;
            ComponentCategoryType category6 = ComponentCategoryType.Undefined;
            ComponentType type6 = ComponentType.Undefined;
            Galaxy.ResolveTechFocus(method_605(panel_1, "ResearchDesignTechFocus6"), out category6, out type6);
            empirePolicy.ResearchDesignTechFocus6 = category6;
            empirePolicy.ResearchDesignTechFocusType6 = type6;
            empirePolicy.ConstructionMilitary = method_605(panel_1, "ConstructionMilitary");
            empirePolicy.WarAttacksAllowColonyBombardment = method_605(panel_1, "WarAttacksAllowColonyBombardment");
            empirePolicy.WarAttacksAllowPlanetDestroying = method_605(panel_1, "WarAttacksAllowPlanetDestroying");
            empirePolicy.HomeworldDefensePriority = method_608(panel_1, "HomeworldDefensePriority");
            empirePolicy.ProtectLeaderAtAllCosts = method_603(panel_1, "ProtectLeaderAtAllCosts");
            empirePolicy.PrioritizeBuildWonderId = method_598(panel_1, "PrioritizeBuildWonderId");
            empirePolicy.ColonizeContinentalPriority = method_608(panel_1, "ColonizeContinentalPriority");
            empirePolicy.ColonizeMarshySwampPriority = method_608(panel_1, "ColonizeMarshySwampPriority");
            empirePolicy.ColonizeOceanPriority = method_608(panel_1, "ColonizeOceanPriority");
            empirePolicy.ColonizeDesertPriority = method_608(panel_1, "ColonizeDesertPriority");
            empirePolicy.ColonizeIcePriority = method_608(panel_1, "ColonizeIcePriority");
            empirePolicy.ColonizeVolcanicPriority = method_608(panel_1, "ColonizeVolcanicPriority");
            empirePolicy.ColonizeRuinsPriority = method_608(panel_1, "ColonizeRuinsPriority");
            empirePolicy.ControlRestrictedResourcesPriority = method_608(panel_1, "ControlRestrictedResourcesPriority");
            empirePolicy.ResearchIndustryFocus = method_600(panel_1, "ResearchIndustryFocus");
            empirePolicy.ResearchPriority = method_608(panel_1, "ResearchPriority");
            empirePolicy.TradePriority = method_608(panel_1, "TradePriority");
            empirePolicy.AlliancePriority = method_608(panel_1, "AlliancePriority");
            empirePolicy.SubjugationPriority = method_608(panel_1, "SubjugationPriority");
            empirePolicy.TourismPriority = method_608(panel_1, "TourismPriority");
            empirePolicy.ExplorationPriority = method_608(panel_1, "ExplorationPriority");
            empirePolicy.WarWillingness = method_608(panel_1, "WarWillingness");
            empirePolicy.BreakTreatyWillingness = method_608(panel_1, "BreakTreatyWillingness");
            empirePolicy.InvasionOverkillFactor = method_608(panel_1, "InvasionOverkillFactor");
            empirePolicy.ShipBattleCautionFactor = method_608(panel_1, "ShipBattleCautionFactor");
            empirePolicy.DefaultMilitaryFleeWhen = method_599(panel_1, "DefaultMilitaryFleeWhen");
            empirePolicy.DesignUpgradeEscort = method_603(panel_1, "DesignUpgradeEscort");
            empirePolicy.DesignUpgradeFrigate = method_603(panel_1, "DesignUpgradeFrigate");
            empirePolicy.DesignUpgradeDestroyer = method_603(panel_1, "DesignUpgradeDestroyer");
            empirePolicy.DesignUpgradeCruiser = method_603(panel_1, "DesignUpgradeCruiser");
            empirePolicy.DesignUpgradeCapitalShip = method_603(panel_1, "DesignUpgradeCapitalShip");
            empirePolicy.DesignUpgradeTroopTransport = method_603(panel_1, "DesignUpgradeTroopTransport");
            empirePolicy.DesignUpgradeCarrier = method_603(panel_1, "DesignUpgradeCarrier");
            empirePolicy.DesignUpgradeResupplyShip = method_603(panel_1, "DesignUpgradeResupplyShip");
            empirePolicy.DesignUpgradeExplorationShip = method_603(panel_1, "DesignUpgradeExplorationShip");
            empirePolicy.DesignUpgradeColonyShip = method_603(panel_1, "DesignUpgradeColonyShip");
            empirePolicy.DesignUpgradeConstructionShip = method_603(panel_1, "DesignUpgradeConstructionShip");
            empirePolicy.DesignUpgradeOutpost= method_603(panel_1, "DesignUpgradeOutpost");
            empirePolicy.DesignUpgradeSmallSpacePort = method_603(panel_1, "DesignUpgradeSmallSpacePort");
            empirePolicy.DesignUpgradeMediumSpacePort = method_603(panel_1, "DesignUpgradeMediumSpacePort");
            empirePolicy.DesignUpgradeLargeSpacePort = method_603(panel_1, "DesignUpgradeLargeSpacePort");
            empirePolicy.DesignUpgradeResortBase = method_603(panel_1, "DesignUpgradeResortBase");
            empirePolicy.DesignUpgradeGenericBase = method_603(panel_1, "DesignUpgradeGenericBase");
            empirePolicy.DesignUpgradeEnergyResearchStation = method_603(panel_1, "DesignUpgradeEnergyResearchStation");
            empirePolicy.DesignUpgradeWeaponsResearchStation = method_603(panel_1, "DesignUpgradeWeaponsResearchStation");
            empirePolicy.DesignUpgradeHighTechResearchStation = method_603(panel_1, "DesignUpgradeHighTechResearchStation");
            empirePolicy.DesignUpgradeMonitoringStation = method_603(panel_1, "DesignUpgradeMonitoringStation");
            empirePolicy.DesignUpgradeDefensiveBase = method_603(panel_1, "DesignUpgradeDefensiveBase");
            empirePolicy.DesignUpgradeSmallFreighter = method_603(panel_1, "DesignUpgradeSmallFreighter");
            empirePolicy.DesignUpgradeMediumFreighter = method_603(panel_1, "DesignUpgradeMediumFreighter");
            empirePolicy.DesignUpgradeLargeFreighter = method_603(panel_1, "DesignUpgradeLargeFreighter");
            empirePolicy.DesignUpgradePassengerShip = method_603(panel_1, "DesignUpgradePassengerShip");
            empirePolicy.DesignUpgradeGasMiningShip = method_603(panel_1, "DesignUpgradeGasMiningShip");
            empirePolicy.DesignUpgradeMiningShip = method_603(panel_1, "DesignUpgradeMiningShip");
            empirePolicy.DesignUpgradeGasMiningStation = method_603(panel_1, "DesignUpgradeGasMiningStation");
            empirePolicy.DesignUpgradeMiningStation = method_603(panel_1, "DesignUpgradeMiningStation");
            empirePolicy.CaptureTargetConditionShip = method_605(panel_1, "CaptureTargetConditionShip");
            empirePolicy.CaptureTargetConditionBase = method_605(panel_1, "CaptureTargetConditionBase");
            empirePolicy.OfferPirateAttackMissions = method_605(panel_1, "OfferPirateAttackMissions");
            empirePolicy.BidOnPirateAttackMissions = method_603(panel_1, "BidOnPirateAttackMissions");
            empirePolicy.CaptureEnlistMilitaryShip = method_605(panel_1, "CaptureEnlistMilitaryShip");
            empirePolicy.CaptureDisassembleMilitaryShip = method_605(panel_1, "CaptureDisassembleMilitaryShip");
            empirePolicy.CaptureEnlistCivilianShip = method_605(panel_1, "CaptureEnlistCivilianShip");
            empirePolicy.CaptureDisassembleCivilianShip = method_605(panel_1, "CaptureDisassembleCivilianShip");
            empirePolicy.CaptureEnlistBase = method_605(panel_1, "CaptureEnlistBase");
            empirePolicy.UpgradeEnlistedMilitaryShips = method_603(panel_1, "UpgradeEnlistedMilitaryShips");
            empirePolicy.UpgradeEnlistedCivilianShips = method_603(panel_1, "UpgradeEnlistedCivilianShips");
            empirePolicy.BidOnPirateDefendMissions = method_603(panel_1, "BidOnPirateDefendMissions");
            empirePolicy.AcceptPirateSmugglingMissions = method_603(panel_1, "AcceptPirateSmugglingMissions");
            empirePolicy.OfferDefensivePirateMissionsSituation = method_605(panel_1, "OfferDefensivePirateMissionsSituation");
            empirePolicy.OfferSmugglingPirateMissions = method_605(panel_1, "OfferSmugglingPirateMissions");
            empirePolicy.OfferDefensivePirateMissions = method_605(panel_1, "OfferDefensivePirateMissions");
            empirePolicy.PirateSmugglerFreighterLevel = method_606(panel_1, "PirateSmugglerFreighterLevel");
            empirePolicy.PirateSmugglerMiningLevel = method_606(panel_1, "PirateSmugglerMiningLevel");
            empirePolicy.PirateSmugglerPassengerLevel = method_606(panel_1, "PirateSmugglerPassengerLevel");
            empirePolicy.TroopRecruitInfantryLevel = method_608(panel_1, "TroopRecruitInfantryLevel");
            empirePolicy.TroopRecruitArmorLevel = method_608(panel_1, "TroopRecruitArmorLevel");
            empirePolicy.TroopRecruitArtilleryLevel = method_608(panel_1, "TroopRecruitArtilleryLevel");
            empirePolicy.TroopRecruitSpecialForcesLevel = method_608(panel_1, "TroopRecruitSpecialForcesLevel");
            empirePolicy.TroopUseDefaultTransportLoadout = method_603(panel_1, "TroopUseDefaultTransportLoadout");
            empirePolicy.TroopDefaultTransportLoadoutInfantry = method_604(panel_1, "TroopDefaultTransportLoadoutInfantry") / 100f;
            empirePolicy.TroopDefaultTransportLoadoutArmor = method_604(panel_1, "TroopDefaultTransportLoadoutArmor") / 100f;
            empirePolicy.TroopDefaultTransportLoadoutArtillery = method_604(panel_1, "TroopDefaultTransportLoadoutArtillery") / 100f;
            empirePolicy.TroopDefaultTransportLoadoutSpecialForces = method_604(panel_1, "TroopDefaultTransportLoadoutSpecialForces") / 100f;
            empirePolicy.TroopGarrisonMinimumPerColony = (int)method_604(panel_1, "TroopGarrisonMinimumPerColony");
            empirePolicy.TroopGarrisonLevel = method_606(panel_1, "TroopGarrisonLevel");
            empirePolicy.UseExplorationShipsToScoutEnemySystems = method_603(panel_1, "UseExplorationShipsToScoutEnemySystems");
            empirePolicy.BuildPlanetDestroyers = method_603(panel_1, "BuildPlanetDestroyers");
            if (gameOptions_0 != null)
            {
                gameOptions_0.ControlAgentAssignmentDefault = empire_5.ControlAgentAssignment;
                gameOptions_0.ControlAttacksOnEnemiesDefault = empire_5.ControlMilitaryAttacks;
                gameOptions_0.ControlColonizationDefault = empire_5.ControlColonization;
                gameOptions_0.ControlColonyFacilitiesDefault = empire_5.ControlColonyFacilities;
                gameOptions_0.ControlColonyTaxRatesDefault = empire_5.ControlColonyTaxRates;
                gameOptions_0.ControlDiplomaticGiftsDefault = empire_5.ControlDiplomacyGifts;
                gameOptions_0.ControlFleetFormationDefault = empire_5.ControlMilitaryFleets;
                gameOptions_0.ControlResearchDefault = empire_5.ControlResearch;
                gameOptions_0.ControlShipBuildingDefault = empire_5.ControlStateConstruction;
                gameOptions_0.ControlShipDesignDefault = empire_5.ControlDesigns;
                gameOptions_0.ControlTreatyNegotiationDefault = empire_5.ControlDiplomacyTreaties;
                gameOptions_0.ControlTroopRecruitmentDefault = empire_5.ControlTroopGeneration;
                gameOptions_0.ControlWarTradeSanctionsDefault = empire_5.ControlDiplomacyOffense;
                gameOptions_0.ControlCharacterLocationsDefault = empire_5.ControlCharacterLocations;
                Galaxy.ApplyDesignUpgradePoliciesToGameOptions(gameOptions_0, empirePolicy);
                EmpirePolicy empirePolicy2 = empirePolicy.Clone();
                empirePolicy2.ColonyActionForNewBuildDesign = null;
                gameOptions_0.DefaultEmpirePolicy = empirePolicy2;
            }
            return empirePolicy;
        }

        private int method_598(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ComboBox)
                {
                    int selectedIndex = ((ComboBox)control).SelectedIndex;
                    if (selectedIndex == 0)
                    {
                        return -1;
                    }
                    if (_Game.Galaxy.PlanetaryFacilityDefinitions != null && selectedIndex > 0 && selectedIndex <= _Game.Galaxy.PlanetaryFacilityDefinitions.Count)
                    {
                        return _Game.Galaxy.PlanetaryFacilityDefinitions[selectedIndex - 1].PlanetaryFacilityDefinitionId;
                    }
                }
            }
            return -1;
        }

        private BuiltObjectFleeWhen method_599(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ComboBox)
                {
                    switch (((ComboBox)control).SelectedIndex)
                    {
                        case 0:
                            return BuiltObjectFleeWhen.Never;
                        case 1:
                            return BuiltObjectFleeWhen.Shields20;
                        case 2:
                            return BuiltObjectFleeWhen.Shields50;
                    }
                }
            }
            return BuiltObjectFleeWhen.Shields20;
        }

        private IndustryType method_600(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ComboBox)
                {
                    switch (((ComboBox)control).SelectedIndex)
                    {
                        case 0:
                            return IndustryType.Undefined;
                        case 1:
                            return IndustryType.Weapon;
                        case 2:
                            return IndustryType.Energy;
                        case 3:
                            return IndustryType.HighTech;
                    }
                }
            }
            return IndustryType.Undefined;
        }

        private ColonyPopulationPolicy method_601(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ColonyPopulationPolicyDropDown)
                {
                    return ((ColonyPopulationPolicyDropDown)control).SelectedPolicy;
                }
            }
            return ColonyPopulationPolicy.Assimilate;
        }

        private Design dFwNhteflw(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is DesignDropDown)
                {
                    return ((DesignDropDown)control).SelectedDesign;
                }
            }
            return null;
        }

        private Resource method_602(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ResourceDropDown)
                {
                    return ((ResourceDropDown)control).SelectedResource;
                }
            }
            return null;
        }

        private bool method_603(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is CheckBox)
                {
                    return ((CheckBox)control).Checked;
                }
            }
            return false;
        }

        private float method_604(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is NumericUpDown)
                {
                    return (float)((NumericUpDown)control).Value;
                }
            }
            return 0f;
        }

        private int method_605(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ComboBox)
                {
                    return ((ComboBox)control).SelectedIndex;
                }
            }
            return 0;
        }

        private double method_606(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    int selectedIndex = comboBox.SelectedIndex;
                    if (comboBox.Items != null)
                    {
                        switch (comboBox.Items.Count)
                        {
                            case 4:
                                switch (selectedIndex)
                                {
                                    case 0:
                                        return 0.0;
                                    case 1:
                                        return 0.5;
                                    case 2:
                                        return 1.0;
                                    case 3:
                                        return 1.5;
                                }
                                break;
                            case 5:
                                switch (selectedIndex)
                                {
                                    case 0:
                                        return 0.0;
                                    case 1:
                                        return 0.5;
                                    case 2:
                                        return 1.0;
                                    case 3:
                                        return 1.5;
                                    case 4:
                                        return 2.0;
                                }
                                break;
                        }
                    }
                }
            }
            return 1.0;
        }

        private double method_607(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    int selectedIndex = comboBox.SelectedIndex;
                    if (comboBox.Items != null)
                    {
                        int count = comboBox.Items.Count;
                        int num = count;
                        if (num == 5)
                        {
                            switch (selectedIndex)
                            {
                                case 0:
                                    return 0.5;
                                case 1:
                                    return 0.75;
                                case 2:
                                    return 1.0;
                                case 3:
                                    return 1.25;
                                case 4:
                                    return 1.5;
                            }
                        }
                    }
                }
            }
            return 1.0;
        }

        private double method_608(System.Windows.Forms.Panel panel_1, string string_30)
        {
            if (panel_1 != null && panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                Control control = panel_1.Controls[string_30];
                if (control != null && control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    int selectedIndex = comboBox.SelectedIndex;
                    if (comboBox.Items != null)
                    {
                        switch (comboBox.Items.Count)
                        {
                            case 3:
                                switch (selectedIndex)
                                {
                                    case 0:
                                        return 0.5;
                                    case 1:
                                        return 1.0;
                                    case 2:
                                        return 1.5;
                                }
                                break;
                            case 4:
                                switch (selectedIndex)
                                {
                                    case 0:
                                        return 0.5;
                                    case 1:
                                        return 1.0;
                                    case 2:
                                        return 1.5;
                                    case 3:
                                        return 2.0;
                                }
                                break;
                            case 5:
                                switch (selectedIndex)
                                {
                                    case 0:
                                        return 0.5;
                                    case 1:
                                        return 1.0;
                                    case 2:
                                        return 1.5;
                                    case 3:
                                        return 2.0;
                                    case 4:
                                        return 4.0;
                                }
                                break;
                        }
                    }
                }
            }
            return 1.0;
        }

        private void method_609(System.Windows.Forms.Panel panel_1, EmpirePolicy empirePolicy_0, Empire empire_5)
        {
            int num = 0;
            int int_ = 350;
            bool flag = false;
            if (empire_5.PirateEmpireBaseHabitat != null)
            {
                flag = true;
            }
            bool flag2 = false;
            if (flag && empire_5.CheckEmpireHasOwnedColonies(empire_5))
            {
                flag2 = true;
            }
            int num2 = 715;
            panel_1.Font = font_6;
            List<Control> list = new List<Control>();
            if (panel_1.Controls != null && panel_1.Controls.Count > 0)
            {
                for (int i = 0; i < panel_1.Controls.Count; i++)
                {
                    if (panel_1.Controls[i].Controls != null && panel_1.Controls[i].Controls.Count > 0)
                    {
                        for (int j = 0; j < panel_1.Controls[i].Controls.Count; j++)
                        {
                            list.Add(panel_1.Controls[i].Controls[j]);
                        }
                        panel_1.Controls[i].Controls.Clear();
                    }
                    list.Add(panel_1.Controls[i]);
                }
                panel_1.Controls.Clear();
            }
            for (int k = 0; k < list.Count; k++)
            {
                list[k].Parent = null;
                list[k].Dispose();
            }
            num += 5;
            if (!flag)
            {
                method_622(panel_1, "AutomationTreaties", new string[3]
                {
                TextResolver.GetText("Control manually"),
                TextResolver.GetText("Suggest new treaties"),
                TextResolver.GetText("Fully automate")
                }, (int)empire_5.ControlDiplomacyTreaties, num2, num);
                method_611(panel_1, TextResolver.GetText("Diplomacy - Treaties"), font_2, ref num);
                method_618(panel_1, int_, "TradePriority", TextResolver.GetText("Free Trade Agreement Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.TradePriority, ref num);
                method_618(panel_1, int_, "AlliancePriority", TextResolver.GetText("Mutual Defense Pact Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.AlliancePriority, ref num);
                method_618(panel_1, int_, "BreakTreatyWillingness", TextResolver.GetText("Willingness to Break Treaties"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.BreakTreatyWillingness, ref num);
                method_610(ref num);
                method_622(panel_1, "AutomationWarTradeSanctions", new string[3]
                {
                TextResolver.GetText("Control manually"),
                TextResolver.GetText("Suggest war and trade sanctions"),
                TextResolver.GetText("Fully automate")
                }, (int)empire_5.ControlDiplomacyOffense, num2, num);
                method_611(panel_1, TextResolver.GetText("Diplomacy - War and Trade Sanctions"), font_2, ref num);
                method_624(panel_1, int_, "DiplomacyTradeSanctionsUseBlockades", TextResolver.GetText("Use Blockades when have Trade Sanctions against an empire"), "", empirePolicy_0.DiplomacyTradeSanctionsUseBlockades, ref num);
                method_618(panel_1, int_, "SubjugationPriority", TextResolver.GetText("Subjugation Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.SubjugationPriority, ref num);
                method_618(panel_1, int_, "WarWillingness", TextResolver.GetText("Willingness to Go To War"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.WarWillingness, ref num);
                method_610(ref num);
                method_622(panel_1, "AutomationDiplomacyGifts", new string[3]
                {
                TextResolver.GetText("Control manually"),
                TextResolver.GetText("Suggest gifts to empires"),
                TextResolver.GetText("Fully automate")
                }, (int)empire_5.ControlDiplomacyGifts, num2, num);
                method_611(panel_1, TextResolver.GetText("Diplomacy - Gifts"), font_2, ref num);
                method_625(panel_1, int_, "DiplomacySendGiftsUpToAmount", TextResolver.GetText("Send appropriate monetary gifts up to limit of"), TextResolver.GetText("credits"), 0f, 100000f, empirePolicy_0.DiplomacySendGiftsUpToAmount, ref num);
            }
            method_610(ref num);
            method_611(panel_1, TextResolver.GetText("Economy and Trade"), font_2, ref num);
            method_624(panel_1, int_, "EconomyTradeWithOtherEmpires", TextResolver.GetText("Trade with other Empires"), "", empirePolicy_0.TradeWithOtherEmpires, ref num);
            method_618(panel_1, int_, "ControlRestrictedResourcesPriority", TextResolver.GetText("Control Restricted Resources Priority"), "", new string[4]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("High"),
            TextResolver.GetText("Very High")
            }, empirePolicy_0.ControlRestrictedResourcesPriority, ref num);
            method_624(panel_1, int_, "EconomyEngageInTourism", TextResolver.GetText("Engage in Tourism"), "", empirePolicy_0.EngageInTourism, ref num);
            method_618(panel_1, int_, "TourismPriority", TextResolver.GetText("Tourism Priority"), "", new string[4]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("High"),
            TextResolver.GetText("Very High")
            }, empirePolicy_0.TourismPriority, ref num);
            method_618(panel_1, int_, "ExplorationPriority", TextResolver.GetText("Exploration Priority"), "", new string[4]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("High"),
            TextResolver.GetText("Very High")
            }, empirePolicy_0.ExplorationPriority, ref num);
            method_610(ref num);
            method_622(panel_1, "AutomationAgentAssignment", new string[3]
            {
            TextResolver.GetText("Control manually"),
            TextResolver.GetText("Suggest offensive missions"),
            TextResolver.GetText("Fully automate")
            }, (int)empire_5.ControlAgentAssignment, num2, num);
            method_611(panel_1, TextResolver.GetText("Intelligence - Mission Assignment"), font_2, ref num);
            method_625(panel_1, int_, "IntelligenceCounterIntelligenceProportion", TextResolver.GetText("Proportion of Agents devoted to Counterintelligence"), "%", 0f, 100f, empirePolicy_0.IntelligenceCounterIntelligenceProportion, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionStealTerritoryMap", TextResolver.GetText("Allow Espionage mission: Steal Territory Map"), "", empirePolicy_0.IntelligenceAllowMissionStealTerritoryMap, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionStealGalaxyMap", TextResolver.GetText("Allow Espionage mission: Steal Galaxy Map"), "", empirePolicy_0.IntelligenceAllowMissionStealGalaxyMap, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionStealOperationsMap", TextResolver.GetText("Allow Espionage mission: Steal Operations Map"), "", empirePolicy_0.IntelligenceAllowMissionStealOperationsMap, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionStealTechData", TextResolver.GetText("Allow Espionage mission: Steal Tech"), "", empirePolicy_0.IntelligenceAllowMissionStealTechData, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionSabotageColony", TextResolver.GetText("Allow Sabotage mission: Sabotage Colony"), "", empirePolicy_0.IntelligenceAllowMissionSabotageColony, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionSabotageConstruction", TextResolver.GetText("Allow Sabotage mission: Sabotage construction"), "", empirePolicy_0.IntelligenceAllowMissionSabotageConstruction, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionDestroyBase", TextResolver.GetText("Allow Sabotage mission: Destroy Base"), "", empirePolicy_0.IntelligenceAllowMissionDestroyBase, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionInciteRevolution", TextResolver.GetText("Allow Sabotage mission: Incite Revolution"), "", empirePolicy_0.IntelligenceAllowMissionInciteRevolution, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionAssassinateCharacter", TextResolver.GetText("Allow Sabotage mission: Assassinate Character"), "", empirePolicy_0.IntelligenceAllowMissionAssassinateCharacter, ref num);
            method_624(panel_1, int_, "IntelligenceAllowMissionDeepCover", TextResolver.GetText("Allow Espionage mission: Plant agent in Deep Cover"), "", empirePolicy_0.IntelligenceAllowMissionDeepCover, ref num);
            method_620(panel_1, int_, "IntelligenceUseEspionageAgainstEmpireWhen", TextResolver.GetText("Assign Espionage missions against empire when"), "", new string[5]
            {
            TextResolver.GetText("Anytime"),
            TextResolver.GetText("Disliked"),
            TextResolver.GetText("No Treaty"),
            TextResolver.GetText("Trade Sanctions or War"),
            TextResolver.GetText("At War")
            }, empirePolicy_0.IntelligenceUseEspionageAgainstEmpireWhen, ref num);
            method_620(panel_1, int_, "IntelligenceUseSabotageAgainstEmpireWhen", TextResolver.GetText("Assign Sabotage missions against empire when"), "", new string[5]
            {
            TextResolver.GetText("Anytime"),
            TextResolver.GetText("Disliked"),
            TextResolver.GetText("No Treaty"),
            TextResolver.GetText("Trade Sanctions or War"),
            TextResolver.GetText("At War")
            }, empirePolicy_0.IntelligenceUseSabotageAgainstEmpireWhen, ref num);
            if (!flag || flag2)
            {
                method_610(ref num);
                method_622(panel_1, "AutomationColonization", new string[3]
                {
                TextResolver.GetText("Control manually"),
                TextResolver.GetText("Suggest new colonies"),
                TextResolver.GetText("Fully automate")
                }, (int)empire_5.ControlColonization, num2, num);
                method_611(panel_1, "Colonization", font_2, ref num);
                method_618(panel_1, int_, "ColonizeContinentalPriority", TextResolver.GetText("Continental Planet Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.ColonizeContinentalPriority, ref num);
                method_618(panel_1, int_, "ColonizeMarshySwampPriority", TextResolver.GetText("Marshy Swamp Planet Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.ColonizeMarshySwampPriority, ref num);
                method_618(panel_1, int_, "ColonizeOceanPriority", TextResolver.GetText("Ocean Planet Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.ColonizeOceanPriority, ref num);
                method_618(panel_1, int_, "ColonizeDesertPriority", TextResolver.GetText("Desert Planet Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.ColonizeDesertPriority, ref num);
                method_618(panel_1, int_, "ColonizeIcePriority", TextResolver.GetText("Ice Planet Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.ColonizeIcePriority, ref num);
                method_618(panel_1, int_, "ColonizeVolcanicPriority", TextResolver.GetText("Volcanic Planet Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.ColonizeVolcanicPriority, ref num);
                method_618(panel_1, int_, "ColonizeRuinsPriority", TextResolver.GetText("Planets with Ruins Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.ColonizeRuinsPriority, ref num);
                method_624(panel_1, int_, "ColonyActionForNewTroopRecruitment", TextResolver.GetText("When establish new colony, always recruit new Troops"), "", empirePolicy_0.ColonyActionForNewTroopRecruitment, ref num);
                DesignList designsByRoles = _Game.PlayerEmpire.Designs.GetDesignsByRoles(new List<BuiltObjectRole> { BuiltObjectRole.Base });
                designsByRoles.StripUnbuildableDesigns(_Game.PlayerEmpire);
                if (empirePolicy_0.ColonyActionForNewBuildDesign != null && !designsByRoles.Contains(empirePolicy_0.ColonyActionForNewBuildDesign))
                {
                    designsByRoles.Add(empirePolicy_0.ColonyActionForNewBuildDesign);
                }
                method_615(panel_1, int_, "ColonyActionForNewBuildDesign", TextResolver.GetText("When establish new colony, immediately build this base"), "", designsByRoles, empirePolicy_0.ColonyActionForNewBuildDesign, ref num);
                method_614(panel_1, int_, "NewColonyPopulationPolicyYourRaceFamily", TextResolver.GetText("Default Population Policy: Your Race Family"), "", empirePolicy_0.NewColonyPopulationPolicyYourRaceFamily, ref num);
                method_614(panel_1, int_, "NewColonyPopulationPolicyAllRaces", TextResolver.GetText("Default Population Policy: All Other Races"), "", empirePolicy_0.NewColonyPopulationPolicyAllRaces, ref num);
                method_624(panel_1, int_, "ImplementEnslavementWithPenalColonies", TextResolver.GetText("Use Penal Colonies to implement 'Enslave' policy"), "", empirePolicy_0.ImplementEnslavementWithPenalColonies, ref num);
                method_610(ref num);
                method_622(panel_1, "AutomationColonyFacilityBuilding", new string[3]
                {
                TextResolver.GetText("Control manually"),
                TextResolver.GetText("Suggest new colony facilities"),
                TextResolver.GetText("Fully automate")
                }, (int)empire_5.ControlColonyFacilities, num2, num);
                method_611(panel_1, TextResolver.GetText("Colonies - Facility Building"), font_2, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityFortifiedBunker", TextResolver.GetText("Allow building facility: Fortified Bunker"), "", empirePolicy_0.ColonyAllowFacilityFortifiedBunker, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityTroopTrainingCenter", TextResolver.GetText("Allow building facility: Troop Academy"), "", empirePolicy_0.ColonyAllowFacilityTroopTrainingCenter, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityMilitaryAcademy", TextResolver.GetText("Allow building facility: Military Academy"), "", empirePolicy_0.ColonyAllowFacilityMilitaryAcademy, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityRoboticTroopFoundry", TextResolver.GetText("Allow building facility: Robotic Troop Foundry"), "", empirePolicy_0.ColonyAllowFacilityRoboticTroopFoundry, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityCloningFacility", TextResolver.GetText("Allow building facility: Cloning Facility"), "", empirePolicy_0.ColonyAllowFacilityCloningFacility, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityArmoredFactory", TextResolver.GetText("Allow building facility: Armored Factory"), "", empirePolicy_0.ColonyAllowFacilityArmoredFactory, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityPlanetaryShield", TextResolver.GetText("Allow building facility: Planetary Shield"), "", empirePolicy_0.ColonyAllowFacilityPlanetaryShield, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityGiantIonCannon", TextResolver.GetText("Allow building facility: Giant Ion Cannon"), "", empirePolicy_0.ColonyAllowFacilityGiantIonCannon, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityNavalAcademy", TextResolver.GetText("Allow building facility: Naval Academy"), "", empirePolicy_0.ColonyAllowFacilityNavalAcademy, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilitySpyAcademy", TextResolver.GetText("Allow building facility: Spy Academy"), "", empirePolicy_0.ColonyAllowFacilitySpyAcademy, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityScienceAcademy", TextResolver.GetText("Allow building facility: Science Academy"), "", empirePolicy_0.ColonyAllowFacilityScienceAcademy, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityTerraformingFacility", TextResolver.GetText("Allow building facility: Terraforming Facility"), "", empirePolicy_0.ColonyAllowFacilityTerraformingFacility, ref num);
                method_624(panel_1, int_, "ColonyAllowFacilityRegionalCapital", TextResolver.GetText("Allow building facility: Regional Capital"), "", empirePolicy_0.ColonyAllowFacilityRegionalCapital, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdFortifiedBunker", TextResolver.GetText("Do not build Fortified Bunker until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdFortifiedBunker, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdTroopTrainingCenter", TextResolver.GetText("Do not build Troop Academy until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdTroopTrainingCenter, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdMilitaryAcademy", TextResolver.GetText("Do not build Military Academy until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdMilitaryAcademy, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdRoboticTroopFoundry", TextResolver.GetText("Do not build Robotic Troop Foundry until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdRoboticTroopFoundry, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdCloningFacility", TextResolver.GetText("Do not build Cloning Facility until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdCloningFacility, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdArmoredFactory", TextResolver.GetText("Do not build Armored Factory until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdArmoredFactory, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdPlanetaryShield", TextResolver.GetText("Do not build Planetary Shield until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdPlanetaryShield, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdGiantIonCannon", TextResolver.GetText("Do not build Giant Ion Cannon until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdGiantIonCannon, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdNavalAcademy", TextResolver.GetText("Do not build Naval Academy until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdNavalAcademy, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdSpyAcademy", TextResolver.GetText("Do not build Spy Academy until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdSpyAcademy, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdScienceAcademy", TextResolver.GetText("Do not build Science Academy until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdScienceAcademy, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdTerraformingFacility", TextResolver.GetText("Do not build Terraforming Facility until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdTerraformingFacility, ref num);
                method_625(panel_1, int_, "ColonyFacilityPopulationThresholdRegionalCapital", TextResolver.GetText("Do not build Regional Capital until population reaches"), "M", 0f, 20000f, empirePolicy_0.ColonyFacilityPopulationThresholdRegionalCapital, ref num);
                string[] array = new string[_Game.Galaxy.PlanetaryFacilityDefinitions.Count + 1];
                array[0] = TextResolver.GetText("None");
                for (int l = 0; l < _Game.Galaxy.PlanetaryFacilityDefinitions.Count; l++)
                {
                    array[l + 1] = _Game.Galaxy.PlanetaryFacilityDefinitions[l].Name;
                }
                method_619(panel_1, int_, "PrioritizeBuildWonderId", TextResolver.GetText("Build Special Wonder"), "", array, empirePolicy_0.PrioritizeBuildWonderId, ref num);
                method_610(ref num);
                method_622(panel_1, "AutomationColonyTaxRates", new string[2]
                {
                TextResolver.GetText("Control manually"),
                TextResolver.GetText("Fully automate")
                }, Convert.ToInt32(empire_5.ControlColonyTaxRates), num2, num);
                method_611(panel_1, TextResolver.GetText("Colonies - Tax Rates"), font_2, ref num);
                method_620(panel_1, int_, "ColonyTaxRateSmallColony", string.Format(TextResolver.GetText("Tax Rate policy for small colonies (below X)"), "200M"), "", new string[4]
                {
                TextResolver.GetText("Zero"),
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.ColonyTaxRateSmallColony, ref num);
                method_620(panel_1, int_, "ColonyTaxRateMediumColony", string.Format(TextResolver.GetText("Tax Rate policy for medium colonies (X - Y)"), "200M", "2000M"), "", new string[4]
                {
                TextResolver.GetText("Zero"),
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.ColonyTaxRateMediumColony, ref num);
                method_620(panel_1, int_, "ColonyTaxRateLargeColony", string.Format(TextResolver.GetText("Tax Rate policy for large colonies (above X)"), "2000M"), "", new string[4]
                {
                TextResolver.GetText("Zero"),
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.ColonyTaxRateLargeColony, ref num);
                method_624(panel_1, int_, "ColonyTaxRateIncreaseWhenAtWar", TextResolver.GetText("Increase colony tax rates when at War"), "", empirePolicy_0.ColonyTaxRateIncreaseWhenAtWar, ref num);
            }
            method_610(ref num);
            method_622(panel_1, "AutomationResearch", new string[2]
            {
            TextResolver.GetText("Control Research manually"),
            TextResolver.GetText("Fully automate Research")
            }, Convert.ToInt32(empire_5.ControlResearch), num2 - 200, num);
            method_622(panel_1, "AutomationDesigns", new string[2]
            {
            TextResolver.GetText("Control Ship Design manually"),
            TextResolver.GetText("Fully automate Ship Design")
            }, Convert.ToInt32(empire_5.ControlDesigns), num2, num);
            method_611(panel_1, "Research && Design", font_2, ref num);
            method_618(panel_1, int_, "ResearchPriority", TextResolver.GetText("Research Priority"), "", new string[4]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("High"),
            TextResolver.GetText("Very High")
            }, empirePolicy_0.ResearchPriority, ref num);
            method_620(panel_1, int_, "ResearchDesignOverallFocus", TextResolver.GetText("Overall focus"), "", new string[4]
            {
            TextResolver.GetText("Balanced"),
            TextResolver.GetText("Speed and Agility"),
            TextResolver.GetText("Raw Power"),
            TextResolver.GetText("Energy Efficiency")
            }, (int)empirePolicy_0.ResearchDesignOverallFocus, ref num);
            method_620(panel_1, int_, "ResearchIndustryFocus", TextResolver.GetText("Area focus"), "", new string[4]
            {
            TextResolver.GetText("Balanced"),
            TextResolver.GetText("Weapons"),
            TextResolver.GetText("Energy and Construction"),
            TextResolver.GetText("HighTech and Industrial")
            }, (int)empirePolicy_0.ResearchIndustryFocus, ref num);
            //int num3 = 0;
            method_620(int_65: (empirePolicy_0.ResearchDesignTechFocus1 == ComponentCategoryType.Undefined) ? Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocusType1) : Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocus1), panel_1: panel_1, int_64: int_, string_30: "ResearchDesignTechFocus1", string_31: TextResolver.GetText("Tech emphasis 1"), string_32: "", string_33: new string[34]
            {
            TextResolver.GetText("None"),
            TextResolver.GetText("Beams"),
            TextResolver.GetText("Phasers"),
            TextResolver.GetText("Rail Guns"),
            TextResolver.GetText("Torpedoes"),
            TextResolver.GetText("Bombard Weapons"),
            TextResolver.GetText("Missiles"),
            TextResolver.GetText("Area Weapons"),
            TextResolver.GetText("Ion Weapons"),
            TextResolver.GetText("Fighters"),
            TextResolver.GetText("Armor"),
            TextResolver.GetText("Shields"),
            TextResolver.GetText("Reactors"),
            TextResolver.GetText("Main Thrust Engines"),
            TextResolver.GetText("Vectoring Engines"),
            TextResolver.GetText("HyperDrives"),
            TextResolver.GetText("Hyper Disruption"),
            TextResolver.GetText("Construction"),
            TextResolver.GetText("Damage Control"),
            TextResolver.GetText("Combat Targetting"),
            TextResolver.GetText("Countermeasures"),
            TextResolver.GetText("Sensors"),
            TextResolver.GetText("Medicine"),
            TextResolver.GetText("Recreation"),
            TextResolver.GetText("Tractor Beams"),
            TextResolver.GetText("Assault Pods"),
            TextResolver.GetText("Component Type Gravity Beam Weapon"),
            TextResolver.GetText("Component Type Gravity Area Weapon"),
            TextResolver.GetText("Component Type Super Beam Weapon"),
            TextResolver.GetText("Component Type Super Area Weapon"),
            TextResolver.GetText("Component Type Super Torpedo Weapon"),
            TextResolver.GetText("Component Type Super Missile Weapon"),
            TextResolver.GetText("Component Type Super RailGun Weapon"),
            TextResolver.GetText("Component Type Super Phaser Weapon")
            }, int_66: ref num);
            //int num4 = 0;
            method_620(int_65: (empirePolicy_0.ResearchDesignTechFocus2 == ComponentCategoryType.Undefined) ? Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocusType2) : Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocus2), panel_1: panel_1, int_64: int_, string_30: "ResearchDesignTechFocus2", string_31: TextResolver.GetText("Tech emphasis 2"), string_32: "", string_33: new string[34]
            {
            TextResolver.GetText("None"),
            TextResolver.GetText("Beams"),
            TextResolver.GetText("Phasers"),
            TextResolver.GetText("Rail Guns"),
            TextResolver.GetText("Torpedoes"),
            TextResolver.GetText("Bombard Weapons"),
            TextResolver.GetText("Missiles"),
            TextResolver.GetText("Area Weapons"),
            TextResolver.GetText("Ion Weapons"),
            TextResolver.GetText("Fighters"),
            TextResolver.GetText("Armor"),
            TextResolver.GetText("Shields"),
            TextResolver.GetText("Reactors"),
            TextResolver.GetText("Main Thrust Engines"),
            TextResolver.GetText("Vectoring Engines"),
            TextResolver.GetText("HyperDrives"),
            TextResolver.GetText("Hyper Disruption"),
            TextResolver.GetText("Construction"),
            TextResolver.GetText("Damage Control"),
            TextResolver.GetText("Combat Targetting"),
            TextResolver.GetText("Countermeasures"),
            TextResolver.GetText("Sensors"),
            TextResolver.GetText("Medicine"),
            TextResolver.GetText("Recreation"),
            TextResolver.GetText("Tractor Beams"),
            TextResolver.GetText("Assault Pods"),
            TextResolver.GetText("Component Type Gravity Beam Weapon"),
            TextResolver.GetText("Component Type Gravity Area Weapon"),
            TextResolver.GetText("Component Type Super Beam Weapon"),
            TextResolver.GetText("Component Type Super Area Weapon"),
            TextResolver.GetText("Component Type Super Torpedo Weapon"),
            TextResolver.GetText("Component Type Super Missile Weapon"),
            TextResolver.GetText("Component Type Super RailGun Weapon"),
            TextResolver.GetText("Component Type Super Phaser Weapon")
            }, int_66: ref num);
            //int num5 = 0;
            method_620(int_65: (empirePolicy_0.ResearchDesignTechFocus3 == ComponentCategoryType.Undefined) ? Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocusType3) : Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocus3), panel_1: panel_1, int_64: int_, string_30: "ResearchDesignTechFocus3", string_31: TextResolver.GetText("Tech emphasis 3"), string_32: "", string_33: new string[34]
            {
            TextResolver.GetText("None"),
            TextResolver.GetText("Beams"),
            TextResolver.GetText("Phasers"),
            TextResolver.GetText("Rail Guns"),
            TextResolver.GetText("Torpedoes"),
            TextResolver.GetText("Bombard Weapons"),
            TextResolver.GetText("Missiles"),
            TextResolver.GetText("Area Weapons"),
            TextResolver.GetText("Ion Weapons"),
            TextResolver.GetText("Fighters"),
            TextResolver.GetText("Armor"),
            TextResolver.GetText("Shields"),
            TextResolver.GetText("Reactors"),
            TextResolver.GetText("Main Thrust Engines"),
            TextResolver.GetText("Vectoring Engines"),
            TextResolver.GetText("HyperDrives"),
            TextResolver.GetText("Hyper Disruption"),
            TextResolver.GetText("Construction"),
            TextResolver.GetText("Damage Control"),
            TextResolver.GetText("Combat Targetting"),
            TextResolver.GetText("Countermeasures"),
            TextResolver.GetText("Sensors"),
            TextResolver.GetText("Medicine"),
            TextResolver.GetText("Recreation"),
            TextResolver.GetText("Tractor Beams"),
            TextResolver.GetText("Assault Pods"),
            TextResolver.GetText("Component Type Gravity Beam Weapon"),
            TextResolver.GetText("Component Type Gravity Area Weapon"),
            TextResolver.GetText("Component Type Super Beam Weapon"),
            TextResolver.GetText("Component Type Super Area Weapon"),
            TextResolver.GetText("Component Type Super Torpedo Weapon"),
            TextResolver.GetText("Component Type Super Missile Weapon"),
            TextResolver.GetText("Component Type Super RailGun Weapon"),
            TextResolver.GetText("Component Type Super Phaser Weapon")
            }, int_66: ref num);
            //int num6 = 0;
            method_620(int_65: (empirePolicy_0.ResearchDesignTechFocus4 == ComponentCategoryType.Undefined) ? Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocusType4) : Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocus4), panel_1: panel_1, int_64: int_, string_30: "ResearchDesignTechFocus4", string_31: TextResolver.GetText("Tech emphasis 4"), string_32: "", string_33: new string[34]
            {
            TextResolver.GetText("None"),
            TextResolver.GetText("Beams"),
            TextResolver.GetText("Phasers"),
            TextResolver.GetText("Rail Guns"),
            TextResolver.GetText("Torpedoes"),
            TextResolver.GetText("Bombard Weapons"),
            TextResolver.GetText("Missiles"),
            TextResolver.GetText("Area Weapons"),
            TextResolver.GetText("Ion Weapons"),
            TextResolver.GetText("Fighters"),
            TextResolver.GetText("Armor"),
            TextResolver.GetText("Shields"),
            TextResolver.GetText("Reactors"),
            TextResolver.GetText("Main Thrust Engines"),
            TextResolver.GetText("Vectoring Engines"),
            TextResolver.GetText("HyperDrives"),
            TextResolver.GetText("Hyper Disruption"),
            TextResolver.GetText("Construction"),
            TextResolver.GetText("Damage Control"),
            TextResolver.GetText("Combat Targetting"),
            TextResolver.GetText("Countermeasures"),
            TextResolver.GetText("Sensors"),
            TextResolver.GetText("Medicine"),
            TextResolver.GetText("Recreation"),
            TextResolver.GetText("Tractor Beams"),
            TextResolver.GetText("Assault Pods"),
            TextResolver.GetText("Component Type Gravity Beam Weapon"),
            TextResolver.GetText("Component Type Gravity Area Weapon"),
            TextResolver.GetText("Component Type Super Beam Weapon"),
            TextResolver.GetText("Component Type Super Area Weapon"),
            TextResolver.GetText("Component Type Super Torpedo Weapon"),
            TextResolver.GetText("Component Type Super Missile Weapon"),
            TextResolver.GetText("Component Type Super RailGun Weapon"),
            TextResolver.GetText("Component Type Super Phaser Weapon")
            }, int_66: ref num);
            //int сnum7 = 0;
            method_620(int_65: (empirePolicy_0.ResearchDesignTechFocus5 == ComponentCategoryType.Undefined) ? Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocusType5) : Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocus5), panel_1: panel_1, int_64: int_, string_30: "ResearchDesignTechFocus5", string_31: TextResolver.GetText("Tech emphasis 5"), string_32: "", string_33: new string[34]
            {
            TextResolver.GetText("None"),
            TextResolver.GetText("Beams"),
            TextResolver.GetText("Phasers"),
            TextResolver.GetText("Rail Guns"),
            TextResolver.GetText("Torpedoes"),
            TextResolver.GetText("Bombard Weapons"),
            TextResolver.GetText("Missiles"),
            TextResolver.GetText("Area Weapons"),
            TextResolver.GetText("Ion Weapons"),
            TextResolver.GetText("Fighters"),
            TextResolver.GetText("Armor"),
            TextResolver.GetText("Shields"),
            TextResolver.GetText("Reactors"),
            TextResolver.GetText("Main Thrust Engines"),
            TextResolver.GetText("Vectoring Engines"),
            TextResolver.GetText("HyperDrives"),
            TextResolver.GetText("Hyper Disruption"),
            TextResolver.GetText("Construction"),
            TextResolver.GetText("Damage Control"),
            TextResolver.GetText("Combat Targetting"),
            TextResolver.GetText("Countermeasures"),
            TextResolver.GetText("Sensors"),
            TextResolver.GetText("Medicine"),
            TextResolver.GetText("Recreation"),
            TextResolver.GetText("Tractor Beams"),
            TextResolver.GetText("Assault Pods"),
            TextResolver.GetText("Component Type Gravity Beam Weapon"),
            TextResolver.GetText("Component Type Gravity Area Weapon"),
            TextResolver.GetText("Component Type Super Beam Weapon"),
            TextResolver.GetText("Component Type Super Area Weapon"),
            TextResolver.GetText("Component Type Super Torpedo Weapon"),
            TextResolver.GetText("Component Type Super Missile Weapon"),
            TextResolver.GetText("Component Type Super RailGun Weapon"),
            TextResolver.GetText("Component Type Super Phaser Weapon")
            }, int_66: ref num);
            //int num8 = 0;
            method_620(int_65: (empirePolicy_0.ResearchDesignTechFocus6 == ComponentCategoryType.Undefined) ? Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocusType6) : Galaxy.ResolveTechFocusIndex(empirePolicy_0.ResearchDesignTechFocus6), panel_1: panel_1, int_64: int_, string_30: "ResearchDesignTechFocus6", string_31: TextResolver.GetText("Tech emphasis 6"), string_32: "", string_33: new string[34]
            {
            TextResolver.GetText("None"),
            TextResolver.GetText("Beams"),
            TextResolver.GetText("Phasers"),
            TextResolver.GetText("Rail Guns"),
            TextResolver.GetText("Torpedoes"),
            TextResolver.GetText("Bombard Weapons"),
            TextResolver.GetText("Missiles"),
            TextResolver.GetText("Area Weapons"),
            TextResolver.GetText("Ion Weapons"),
            TextResolver.GetText("Fighters"),
            TextResolver.GetText("Armor"),
            TextResolver.GetText("Shields"),
            TextResolver.GetText("Reactors"),
            TextResolver.GetText("Main Thrust Engines"),
            TextResolver.GetText("Vectoring Engines"),
            TextResolver.GetText("HyperDrives"),
            TextResolver.GetText("Hyper Disruption"),
            TextResolver.GetText("Construction"),
            TextResolver.GetText("Damage Control"),
            TextResolver.GetText("Combat Targetting"),
            TextResolver.GetText("Countermeasures"),
            TextResolver.GetText("Sensors"),
            TextResolver.GetText("Medicine"),
            TextResolver.GetText("Recreation"),
            TextResolver.GetText("Tractor Beams"),
            TextResolver.GetText("Assault Pods"),
            TextResolver.GetText("Component Type Gravity Beam Weapon"),
            TextResolver.GetText("Component Type Gravity Area Weapon"),
            TextResolver.GetText("Component Type Super Beam Weapon"),
            TextResolver.GetText("Component Type Super Area Weapon"),
            TextResolver.GetText("Component Type Super Torpedo Weapon"),
            TextResolver.GetText("Component Type Super Missile Weapon"),
            TextResolver.GetText("Component Type Super RailGun Weapon"),
            TextResolver.GetText("Component Type Super Phaser Weapon")
            }, int_66: ref num);
            method_624(panel_1, int_, "ResearchDesignAutoRetrofit", TextResolver.GetText("Prompt for Retrofit when new tech becomes available"), "", empirePolicy_0.ResearchDesignAutoRetrofit, ref num);
            method_624(panel_1, int_, "ResearchDesignAutoUpgradeFighters", TextResolver.GetText("Automatically upgrade fighters to latest"), "(" + TextResolver.GetText("when not in battle") + ")", empirePolicy_0.ResearchDesignAutoUpgradeFighters, ref num);
            method_610(ref num);
            method_612(panel_1, TextResolver.GetText("Design Upgrade Explanation"), font_6, ref num);
            method_624(panel_1, int_, "DesignUpgradeEscort", Galaxy.ResolveDescription(BuiltObjectSubRole.Escort), "", empirePolicy_0.DesignUpgradeEscort, ref num);
            method_624(panel_1, int_, "DesignUpgradeFrigate", Galaxy.ResolveDescription(BuiltObjectSubRole.Frigate), "", empirePolicy_0.DesignUpgradeFrigate, ref num);
            method_624(panel_1, int_, "DesignUpgradeDestroyer", Galaxy.ResolveDescription(BuiltObjectSubRole.Destroyer), "", empirePolicy_0.DesignUpgradeDestroyer, ref num);
            method_624(panel_1, int_, "DesignUpgradeCruiser", Galaxy.ResolveDescription(BuiltObjectSubRole.Cruiser), "", empirePolicy_0.DesignUpgradeCruiser, ref num);
            method_624(panel_1, int_, "DesignUpgradeCapitalShip", Galaxy.ResolveDescription(BuiltObjectSubRole.CapitalShip), "", empirePolicy_0.DesignUpgradeCapitalShip, ref num);
            method_624(panel_1, int_, "DesignUpgradeTroopTransport", Galaxy.ResolveDescription(BuiltObjectSubRole.TroopTransport), "", empirePolicy_0.DesignUpgradeTroopTransport, ref num);
            method_624(panel_1, int_, "DesignUpgradeCarrier", Galaxy.ResolveDescription(BuiltObjectSubRole.Carrier), "", empirePolicy_0.DesignUpgradeCarrier, ref num);
            method_624(panel_1, int_, "DesignUpgradeResupplyShip", Galaxy.ResolveDescription(BuiltObjectSubRole.ResupplyShip), "", empirePolicy_0.DesignUpgradeResupplyShip, ref num);
            method_624(panel_1, int_, "DesignUpgradeExplorationShip", Galaxy.ResolveDescription(BuiltObjectSubRole.ExplorationShip), "", empirePolicy_0.DesignUpgradeExplorationShip, ref num);
            method_624(panel_1, int_, "DesignUpgradeColonyShip", Galaxy.ResolveDescription(BuiltObjectSubRole.ColonyShip), "", empirePolicy_0.DesignUpgradeColonyShip, ref num);
            method_624(panel_1, int_, "DesignUpgradeConstructionShip", Galaxy.ResolveDescription(BuiltObjectSubRole.ConstructionShip), "", empirePolicy_0.DesignUpgradeConstructionShip, ref num);
            method_624(panel_1, int_, "DesignUpgradeOutpost", "Small base to provide bonuses to planet", "", empirePolicy_0.DesignUpgradeOutpost, ref num);
            method_624(panel_1, int_, "DesignUpgradeSmallSpacePort", Galaxy.ResolveDescription(BuiltObjectSubRole.SmallSpacePort), "", empirePolicy_0.DesignUpgradeSmallSpacePort, ref num);
            method_624(panel_1, int_, "DesignUpgradeMediumSpacePort", Galaxy.ResolveDescription(BuiltObjectSubRole.MediumSpacePort), "", empirePolicy_0.DesignUpgradeMediumSpacePort, ref num);
            method_624(panel_1, int_, "DesignUpgradeLargeSpacePort", Galaxy.ResolveDescription(BuiltObjectSubRole.LargeSpacePort), "", empirePolicy_0.DesignUpgradeLargeSpacePort, ref num);
            method_624(panel_1, int_, "DesignUpgradeResortBase", Galaxy.ResolveDescription(BuiltObjectSubRole.ResortBase), "", empirePolicy_0.DesignUpgradeResortBase, ref num);
            method_624(panel_1, int_, "DesignUpgradeGenericBase", Galaxy.ResolveDescription(BuiltObjectSubRole.GenericBase), "", empirePolicy_0.DesignUpgradeGenericBase, ref num);
            method_624(panel_1, int_, "DesignUpgradeEnergyResearchStation", Galaxy.ResolveDescription(BuiltObjectSubRole.EnergyResearchStation), "", empirePolicy_0.DesignUpgradeEnergyResearchStation, ref num);
            method_624(panel_1, int_, "DesignUpgradeWeaponsResearchStation", Galaxy.ResolveDescription(BuiltObjectSubRole.WeaponsResearchStation), "", empirePolicy_0.DesignUpgradeWeaponsResearchStation, ref num);
            method_624(panel_1, int_, "DesignUpgradeHighTechResearchStation", Galaxy.ResolveDescription(BuiltObjectSubRole.HighTechResearchStation), "", empirePolicy_0.DesignUpgradeHighTechResearchStation, ref num);
            method_624(panel_1, int_, "DesignUpgradeMonitoringStation", Galaxy.ResolveDescription(BuiltObjectSubRole.MonitoringStation), "", empirePolicy_0.DesignUpgradeMonitoringStation, ref num);
            method_624(panel_1, int_, "DesignUpgradeDefensiveBase", Galaxy.ResolveDescription(BuiltObjectSubRole.DefensiveBase), "", empirePolicy_0.DesignUpgradeDefensiveBase, ref num);
            method_624(panel_1, int_, "DesignUpgradeSmallFreighter", Galaxy.ResolveDescription(BuiltObjectSubRole.SmallFreighter), "", empirePolicy_0.DesignUpgradeSmallFreighter, ref num);
            method_624(panel_1, int_, "DesignUpgradeMediumFreighter", Galaxy.ResolveDescription(BuiltObjectSubRole.MediumFreighter), "", empirePolicy_0.DesignUpgradeMediumFreighter, ref num);
            method_624(panel_1, int_, "DesignUpgradeLargeFreighter", Galaxy.ResolveDescription(BuiltObjectSubRole.LargeFreighter), "", empirePolicy_0.DesignUpgradeLargeFreighter, ref num);
            method_624(panel_1, int_, "DesignUpgradePassengerShip", Galaxy.ResolveDescription(BuiltObjectSubRole.PassengerShip), "", empirePolicy_0.DesignUpgradePassengerShip, ref num);
            method_624(panel_1, int_, "DesignUpgradeGasMiningShip", Galaxy.ResolveDescription(BuiltObjectSubRole.GasMiningShip), "", empirePolicy_0.DesignUpgradeGasMiningShip, ref num);
            method_624(panel_1, int_, "DesignUpgradeMiningShip", Galaxy.ResolveDescription(BuiltObjectSubRole.MiningShip), "", empirePolicy_0.DesignUpgradeMiningShip, ref num);
            method_624(panel_1, int_, "DesignUpgradeGasMiningStation", Galaxy.ResolveDescription(BuiltObjectSubRole.GasMiningStation), "", empirePolicy_0.DesignUpgradeGasMiningStation, ref num);
            method_624(panel_1, int_, "DesignUpgradeMiningStation", Galaxy.ResolveDescription(BuiltObjectSubRole.MiningStation), "", empirePolicy_0.DesignUpgradeMiningStation, ref num);
            method_610(ref num);
            method_622(panel_1, "AutomationConstruction", new string[3]
            {
            TextResolver.GetText("Control manually"),
            TextResolver.GetText("Suggest new ships and bases"),
            TextResolver.GetText("Fully automate")
            }, (int)empire_5.ControlStateConstruction, num2, num);
            method_611(panel_1, TextResolver.GetText("Construction"), font_2, ref num);
            method_620(panel_1, int_, "ConstructionMilitary", TextResolver.GetText("Military Construction Level"), "", new string[3]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("High")
            }, empirePolicy_0.ConstructionMilitary, ref num);
            method_625(panel_1, int_, "ConstructionMilitaryEscort", TextResolver.GetText("Military construction proportion: Escorts"), "%", 0f, 100f, empirePolicy_0.ConstructionMilitaryEscort, ref num);
            method_625(panel_1, int_, "ConstructionMilitaryFrigate", TextResolver.GetText("Military construction proportion: Frigates"), "%", 0f, 100f, empirePolicy_0.ConstructionMilitaryFrigate, ref num);
            method_625(panel_1, int_, "ConstructionMilitaryDestroyer", TextResolver.GetText("Military construction proportion: Destroyers"), "%", 0f, 100f, empirePolicy_0.ConstructionMilitaryDestroyer, ref num);
            method_625(panel_1, int_, "ConstructionMilitaryCruiser", TextResolver.GetText("Military construction proportion: Cruisers"), "%", 0f, 100f, empirePolicy_0.ConstructionMilitaryCruiser, ref num);
            method_625(panel_1, int_, "ConstructionMilitaryCapitalShip", TextResolver.GetText("Military construction proportion: Capital Ships"), "%", 0f, 100f, empirePolicy_0.ConstructionMilitaryCapitalShip, ref num);
            method_625(panel_1, int_, "ConstructionMilitaryTroopTransport", TextResolver.GetText("Military construction proportion: Troop Transports"), "%", 0f, 100f, empirePolicy_0.ConstructionMilitaryTroopTransport, ref num);
            method_625(panel_1, int_, "ConstructionMilitaryCarrier", TextResolver.GetText("Military construction proportion: Carriers"), "%", 0f, 100f, empirePolicy_0.ConstructionMilitaryCarrier, ref num);
            method_625(panel_1, int_, "ConstructionSpaceportMinimumDistance", TextResolver.GetText("Minimum distance between new spaceports"), "K", 0f, 2000f, empirePolicy_0.ConstructionSpaceportMinimumDistance, ref num);
            if (!flag)
            {
                method_625(panel_1, int_, "ConstructionSpaceportSmallColonyPopulationThreshold", TextResolver.GetText("Minimum population for Small spaceport"), "M", 0f, 1000f, empirePolicy_0.ConstructionSpaceportSmallColonyPopulationThreshold, ref num);
                method_625(panel_1, int_, "ConstructionSpaceportMediumColonyPopulationThreshold", TextResolver.GetText("Minimum population for Medium spaceport"), "M", 0f, 5000f, empirePolicy_0.ConstructionSpaceportMediumColonyPopulationThreshold, ref num);
                method_625(panel_1, int_, "ConstructionSpaceportLargeColonyPopulationThreshold", TextResolver.GetText("Minimum population for Large spaceport"), "M", 0f, 20000f, empirePolicy_0.ConstructionSpaceportLargeColonyPopulationThreshold, ref num);
            }
            else
            {
                if (flag2)
                {
                    method_625(panel_1, int_, "ConstructionSpaceportSmallColonyPopulationThreshold", TextResolver.GetText("Minimum population for Small spaceport"), "M", 0f, 1000f, empirePolicy_0.ConstructionSpaceportSmallColonyPopulationThreshold, ref num);
                    method_625(panel_1, int_, "ConstructionSpaceportMediumColonyPopulationThreshold", TextResolver.GetText("Minimum population for Medium spaceport"), "M", 0f, 5000f, empirePolicy_0.ConstructionSpaceportMediumColonyPopulationThreshold, ref num);
                    method_625(panel_1, int_, "ConstructionSpaceportLargeColonyPopulationThreshold", TextResolver.GetText("Minimum population for Large spaceport"), "M", 0f, 20000f, empirePolicy_0.ConstructionSpaceportLargeColonyPopulationThreshold, ref num);
                }
                method_617(panel_1, int_, "PirateSmugglerFreighterLevel", TextResolver.GetText("Pirate Smuggler Freighter Construction Level"), "", new string[4]
                {
                TextResolver.GetText("None"),
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.PirateSmugglerFreighterLevel, ref num);
                method_617(panel_1, int_, "PirateSmugglerMiningLevel", TextResolver.GetText("Pirate Smuggler Miner Construction Level"), "", new string[4]
                {
                TextResolver.GetText("None"),
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.PirateSmugglerMiningLevel, ref num);
                method_617(panel_1, int_, "PirateSmugglerPassengerLevel", TextResolver.GetText("Pirate Smuggler Passenger Construction Level"), "", new string[4]
                {
                TextResolver.GetText("None"),
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.PirateSmugglerPassengerLevel, ref num);
            }
            if (!flag || flag2)
            {
                method_610(ref num);
                method_622(panel_1, "AutomationTroopRecruitment", new string[2]
                {
                TextResolver.GetText("Control manually"),
                TextResolver.GetText("Fully automate")
                }, Convert.ToInt32(empire_5.ControlTroopGeneration), num2, num);
                method_611(panel_1, TextResolver.GetText("Troop Recruitment"), font_2, ref num);
                method_625(panel_1, int_, "ColonyPopulationThresholdTroopRecruitment", TextResolver.GetText("Never recruit Troops until colony population reaches"), "M", 0f, 10000f, empirePolicy_0.ColonyPopulationThresholdTroopRecruitment, ref num);
                method_625(panel_1, int_, "TroopGarrisonMinimumPerColony", TextResolver.GetText("Minimum number of Troop Units per Colony"), "(" + TextResolver.GetText("overrides recruitment setting above") + ")", 0f, 100f, empirePolicy_0.TroopGarrisonMinimumPerColony, ref num);
                method_617(panel_1, int_, "TroopGarrisonLevel", TextResolver.GetText("Troop Garrison Level at Colonies"), "", new string[4]
                {
                TextResolver.GetText("None"),
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.TroopGarrisonLevel, ref num);
                method_618(panel_1, int_, "TroopRecruitInfantryLevel", TextResolver.GetText("Infantry Recruitment Level"), "", new string[3]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.TroopRecruitInfantryLevel, ref num);
                method_618(panel_1, int_, "TroopRecruitArmorLevel", TextResolver.GetText("Armor Recruitment Level"), "", new string[3]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.TroopRecruitArmorLevel, ref num);
                method_618(panel_1, int_, "TroopRecruitArtilleryLevel", TextResolver.GetText("Artillery Recruitment Level"), "", new string[3]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.TroopRecruitArtilleryLevel, ref num);
                method_618(panel_1, int_, "TroopRecruitSpecialForcesLevel", TextResolver.GetText("Special Forces Recruitment Level"), "", new string[3]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High")
                }, empirePolicy_0.TroopRecruitSpecialForcesLevel, ref num);
                method_624(panel_1, int_, "TroopUseDefaultTransportLoadout", TextResolver.GetText("Use Default Troop Transport Loadouts"), "", empirePolicy_0.TroopUseDefaultTransportLoadout, ref num);
                method_625(panel_1, int_, "TroopDefaultTransportLoadoutInfantry", TextResolver.GetText("Default Infantry Loadout"), "%", 0f, 100f, empirePolicy_0.TroopDefaultTransportLoadoutInfantry * 100f, ref num);
                method_625(panel_1, int_, "TroopDefaultTransportLoadoutArmor", TextResolver.GetText("Default Armor Loadout"), "%", 0f, 100f, empirePolicy_0.TroopDefaultTransportLoadoutArmor * 100f, ref num);
                method_625(panel_1, int_, "TroopDefaultTransportLoadoutArtillery", TextResolver.GetText("Default Artillery Loadout"), "%", 0f, 100f, empirePolicy_0.TroopDefaultTransportLoadoutArtillery * 100f, ref num);
                method_625(panel_1, int_, "TroopDefaultTransportLoadoutSpecialForces", TextResolver.GetText("Default Special Forces Loadout"), "%", 0f, 100f, empirePolicy_0.TroopDefaultTransportLoadoutSpecialForces * 100f, ref num);
            }
            method_610(ref num);
            method_622(panel_1, "AutomationAttackTargets", new string[3]
            {
            TextResolver.GetText("Control manually"),
            TextResolver.GetText("Suggest attack targets"),
            TextResolver.GetText("Fully automate")
            }, (int)empire_5.ControlMilitaryAttacks, num2, num);
            method_611(panel_1, TextResolver.GetText("War && Attacks"), font_2, ref num);
            method_620(panel_1, int_, "WarAttacksAllowColonyBombardment", TextResolver.GetText("Use bombardment against enemy colonies"), "", new string[4]
            {
            TextResolver.GetText("At every opportunity"),
            TextResolver.GetText("Against empires we intensely dislike"),
            TextResolver.GetText("Against empires with Diabolical reputation"),
            TextResolver.GetText("Never")
            }, empirePolicy_0.WarAttacksAllowColonyBombardment, ref num);
            method_620(panel_1, int_, "WarAttacksAllowPlanetDestroying", TextResolver.GetText("Use planet destroyers against enemy colonies"), "", new string[4]
            {
            TextResolver.GetText("At every opportunity"),
            TextResolver.GetText("Against empires we intensely dislike"),
            TextResolver.GetText("Against empires with Diabolical reputation"),
            TextResolver.GetText("Never")
            }, empirePolicy_0.WarAttacksAllowPlanetDestroying, ref num);
            if (!flag)
            {
                method_624(panel_1, int_, "WarAttacksHarassEnemies", TextResolver.GetText("Harass enemies with attacks of opportunity"), "", empirePolicy_0.WarAttacksHarassEnemies, ref num);
                method_620(panel_1, int_, "OfferPirateAttackMissions", TextResolver.GetText("Offer Pirate Attack Missions"), "", new string[4]
                {
                TextResolver.GetText("Never"),
                TextResolver.GetText("When at War with empire"),
                TextResolver.GetText("When dislike empire"),
                TextResolver.GetText("Whenever opportune target available")
                }, empirePolicy_0.OfferPirateAttackMissions, ref num);
                method_620(panel_1, int_, "OfferDefensivePirateMissionsSituation", TextResolver.GetText("When Offer Pirate Defense Missions"), "", new string[3]
                {
                TextResolver.GetText("Never"),
                TextResolver.GetText("When at War"),
                TextResolver.GetText("At any time")
                }, empirePolicy_0.OfferDefensivePirateMissionsSituation, ref num);
                method_620(panel_1, int_, "OfferDefensivePirateMissions", TextResolver.GetText("Who Offer Pirate Defense Missions To"), "", new string[3]
                {
                TextResolver.GetText("Never"),
                TextResolver.GetText("To pirates we trust"),
                TextResolver.GetText("To any pirates with protection arrangement")
                }, empirePolicy_0.OfferDefensivePirateMissions, ref num);
                method_620(panel_1, int_, "OfferSmugglingPirateMissions", TextResolver.GetText("Offer Pirate Smuggling Missions"), "", new string[3]
                {
                TextResolver.GetText("Never"),
                TextResolver.GetText("When at War"),
                TextResolver.GetText("At any time")
                }, empirePolicy_0.OfferSmugglingPirateMissions, ref num);
                method_618(panel_1, int_, "HomeworldDefensePriority", TextResolver.GetText("Homeworld Defense Priority"), "", new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.HomeworldDefensePriority, ref num);
                method_624(panel_1, int_, "ProtectLeaderAtAllCosts", TextResolver.GetText("Protect Leader At All Costs"), "", empirePolicy_0.ProtectLeaderAtAllCosts, ref num);
                method_618(panel_1, int_, "InvasionOverkillFactor", TextResolver.GetText("Colony Invasion Overkill Factor"), TextResolver.GetText("Invasion Overkill Explanation"), new string[4]
                {
                TextResolver.GetText("Low"),
                TextResolver.GetText("Normal"),
                TextResolver.GetText("High"),
                TextResolver.GetText("Very High")
                }, empirePolicy_0.InvasionOverkillFactor, ref num);
            }
            method_618(panel_1, int_, "ShipBattleCautionFactor", TextResolver.GetText("Ship Battle Caution Factor"), TextResolver.GetText("Ship Battle Caution Explanation"), new string[4]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("High"),
            TextResolver.GetText("Very High")
            }, empirePolicy_0.ShipBattleCautionFactor, ref num);
            method_616(panel_1, int_, "DefaultMilitaryFleeWhen", TextResolver.GetText("Default 'Flee When' Stance for Military ships"), TextResolver.GetText("Flee When Explanation"), new string[3]
            {
            TextResolver.GetText("Flee When Never"),
            TextResolver.GetText("Flee When Shields 20"),
            TextResolver.GetText("Flee When Shields 50")
            }, empirePolicy_0.DefaultMilitaryFleeWhen, ref num);
            method_624(panel_1, int_, "UseExplorationShipsToScoutEnemySystems", TextResolver.GetText("Use Exploration Ships to scout enemy systems"), "", empirePolicy_0.UseExplorationShipsToScoutEnemySystems, ref num);
            method_624(panel_1, int_, "BuildPlanetDestroyers", TextResolver.GetText("Build Planet Destroyers when able"), "", empirePolicy_0.BuildPlanetDestroyers, ref num);
            if (flag)
            {
                method_610(ref num);
                method_611(panel_1, TextResolver.GetText("Pirates"), font_2, ref num);
                method_624(panel_1, int_, "BidOnPirateAttackMissions", TextResolver.GetText("Bid on Pirate Attack Missions"), "", empirePolicy_0.BidOnPirateAttackMissions, ref num);
                method_624(panel_1, int_, "BidOnPirateDefendMissions", TextResolver.GetText("Bid on Pirate Defend Missions"), "", empirePolicy_0.BidOnPirateDefendMissions, ref num);
                method_624(panel_1, int_, "AcceptPirateSmugglingMissions", TextResolver.GetText("Accept Pirate Smuggling Missions"), "", empirePolicy_0.AcceptPirateSmugglingMissions, ref num);
            }
            method_610(ref num);
            method_611(panel_1, TextResolver.GetText("Boarding && Capture"), font_2, ref num);
            method_621(panel_1, int_, "CaptureTargetConditionShip", TextResolver.GetText("Capture targeted ships"), "", new string[4]
            {
            TextResolver.GetText("Never (always destroy)"),
            TextResolver.GetText("When high tech or larger than we can build"),
            TextResolver.GetText("When stronger than target"),
            TextResolver.GetText("Always capture")
            }, empirePolicy_0.CaptureTargetConditionShip, 550, ref num);
            method_621(panel_1, int_, "CaptureTargetConditionBase", TextResolver.GetText("Capture targeted bases"), "", new string[5]
            {
            TextResolver.GetText("Never (always destroy)"),
            TextResolver.GetText("When base in own territory"),
            TextResolver.GetText("When base in own or neutral territory"),
            TextResolver.GetText("When stronger than target"),
            TextResolver.GetText("Always capture")
            }, empirePolicy_0.CaptureTargetConditionBase, 550, ref num);
            method_621(panel_1, int_, "CaptureEnlistMilitaryShip", TextResolver.GetText("Enlist captured Military ships"), "", new string[4]
            {
            TextResolver.GetText("Always Enlist"),
            TextResolver.GetText("When high tech or larger than we can build"),
            TextResolver.GetText("When NOT high tech or larger than we can build"),
            TextResolver.GetText("Never Enlist (always disassemble)")
            }, empirePolicy_0.CaptureEnlistMilitaryShip, 550, ref num);
            method_621(panel_1, int_, "CaptureDisassembleMilitaryShip", TextResolver.GetText("How Disassemble captured Military ships"), "", new string[3]
            {
            TextResolver.GetText("Always immediately scrap for money"),
            TextResolver.GetText("Disassemble at base when high tech or larger than we can build"),
            TextResolver.GetText("Always disassemble at base for tech and resources")
            }, empirePolicy_0.CaptureDisassembleMilitaryShip, 550, ref num);
            method_624(panel_1, int_, "UpgradeEnlistedMilitaryShips", TextResolver.GetText("Upgrade enlisted Military ships to latest design"), "", empirePolicy_0.UpgradeEnlistedMilitaryShips, ref num);
            method_621(panel_1, int_, "CaptureEnlistCivilianShip", TextResolver.GetText("Enlist captured Civilian ships"), "", new string[4]
            {
            TextResolver.GetText("Always Enlist"),
            TextResolver.GetText("When high tech or larger than we can build"),
            TextResolver.GetText("When NOT high tech or larger than we can build"),
            TextResolver.GetText("Never Enlist (always disassemble)")
            }, empirePolicy_0.CaptureEnlistCivilianShip, 550, ref num);
            method_621(panel_1, int_, "CaptureDisassembleCivilianShip", TextResolver.GetText("How Disassemble captured Civilian ships"), "", new string[3]
            {
            TextResolver.GetText("Always immediately scrap for money"),
            TextResolver.GetText("Disassemble at base when high tech or larger than we can build"),
            TextResolver.GetText("Always disassemble at base for tech and resources")
            }, empirePolicy_0.CaptureDisassembleCivilianShip, 550, ref num);
            method_624(panel_1, int_, "UpgradeEnlistedCivilianShips", TextResolver.GetText("Upgrade enlisted Civilian ships to latest design"), "", empirePolicy_0.UpgradeEnlistedCivilianShips, ref num);
            method_621(panel_1, int_, "CaptureEnlistBase", TextResolver.GetText("Enlist captured Bases"), "", new string[3]
            {
            TextResolver.GetText("Always Enlist"),
            TextResolver.GetText("Scrap when not Research Station"),
            TextResolver.GetText("Always Scrap")
            }, empirePolicy_0.CaptureEnlistBase, 550, ref num);
            method_610(ref num);
            method_622(panel_1, "AutomationFleets", new string[2]
            {
            TextResolver.GetText("Control manually"),
            TextResolver.GetText("Fully automate")
            }, Convert.ToInt32(empire_5.ControlMilitaryFleets), num2, num);
            method_611(panel_1, "Fleet Formation", font_2, ref num);
            method_625(panel_1, int_, "FleetMilitaryProportionForFleets", TextResolver.GetText("Proportion of Military ships assigned to Fleets && Strike Forces"), "%", 20f, 80f, empirePolicy_0.FleetMilitaryProportionForFleets, ref num);
            method_625(panel_1, int_, "FleetTypicalSize", TextResolver.GetText("Typical number of ships in Fleet"), "", 2f, 50f, empirePolicy_0.FleetTypicalSize, ref num);
            method_625(panel_1, int_, "FleetStrikeForceTypicalSize", TextResolver.GetText("Typical number of ships in Strike Force"), "", 2f, 15f, empirePolicy_0.FleetStrikeForceTypicalSize, ref num);
        }

    }

}