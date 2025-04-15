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

        private void PrepareDesignForEditor()
        {
            if (design_0 == null)
            {
                design_0 = new Design(string.Empty);
                design_0.DateCreated = _Game.Galaxy.CurrentStarDate;
                design_0.Empire = _Game.PlayerEmpire;
                design_0.BuildCount = 0;
            }
            ComponentList components = new ComponentList();
            if (ctlDesignComponents != null && ctlDesignComponents.Components != null)
            {
                components = ctlDesignComponents.Components.Clone();
            }
            design_0.Components = components;
            design_0.Name = txtDesignName.Text;
            design_0.IsObsolete = chkDesignObsolete.Checked;
            if (cmbDesignsSubRole.SelectedItem != null)
            {
                string string_ = cmbDesignsSubRole.SelectedItem.ToString();
                design_0.SubRole = zirgUhrfvq(string_);
                design_0.Role = DesignSpecification.ResolveRole(design_0.SubRole);
            }
            int selectedIndex = cmbDesignsPicture.SelectedIndex;
            if (selectedIndex >= 0)
            {
                design_0.PictureRef = selectedIndex;
            }
            if (cmbDesignTacticsStrongerShips.SelectedItem != null)
            {
                string string_2 = cmbDesignTacticsStrongerShips.SelectedItem.ToString();
                design_0.TacticsStrongerShips = method_277(string_2);
            }
            if (cmbDesignTacticsWeakerShips.SelectedItem != null)
            {
                string string_3 = cmbDesignTacticsWeakerShips.SelectedItem.ToString();
                design_0.TacticsWeakerShips = method_277(string_3);
            }
            if (cmbDesignTacticsInvasion.SelectedItem != null)
            {
                string string_4 = cmbDesignTacticsInvasion.SelectedItem.ToString();
                design_0.TacticsInvasion = NtLyzCjnsr(string_4);
                if (design_0.TacticsInvasion == InvasionTactics.Undefined)
                {
                    design_0.TacticsInvasion = InvasionTactics.InvadeWhenClear;
                }
            }
            if (cmbDesignsStance.SelectedItem != null)
            {
                string string_5 = cmbDesignsStance.SelectedItem.ToString();
                design_0.Stance = method_276(string_5);
            }
            if (cmbDesignsFleeWhen.SelectedItem != null)
            {
                string string_6 = cmbDesignsFleeWhen.SelectedItem.ToString();
                design_0.FleeWhen = gxQyEgZoWT(string_6);
            }
            design_0.ImageScalingFactor = (float)numDesignImageScalingAmount.Value;
            design_0.ImageScalingType = cmbDesignImageScalingMode.SelectedScalingMode;
            if (cmbDesignDetailAutoRetrofit.SelectedItem != null)
            {
                string string_7 = cmbDesignDetailAutoRetrofit.SelectedItem.ToString();
                design_0.AllowAutoRetrofit = method_278(string_7);
            }
            design_0.ReDefine();
            switch (design_0.SubRole)
            {
                case BuiltObjectSubRole.Escort:
                case BuiltObjectSubRole.Frigate:
                case BuiltObjectSubRole.Destroyer:
                case BuiltObjectSubRole.Cruiser:
                case BuiltObjectSubRole.CapitalShip:
                case BuiltObjectSubRole.TroopTransport:
                case BuiltObjectSubRole.Carrier:
                    design_0.Stance = BuiltObjectStance.AttackEnemies;
                    break;
                case BuiltObjectSubRole.ResupplyShip:
                    design_0.Stance = BuiltObjectStance.DoNotAttack;
                    break;
                case BuiltObjectSubRole.ExplorationShip:
                    design_0.Stance = BuiltObjectStance.AttackIfAttacked;
                    break;
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.ColonyShip:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.ConstructionShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    if (design_0.FirepowerRaw > 0)
                    {
                        design_0.Stance = BuiltObjectStance.AttackIfAttacked;
                    }
                    else
                    {
                        design_0.Stance = BuiltObjectStance.DoNotAttack;
                    }
                    break;
                case BuiltObjectSubRole.GasMiningStation:
                case BuiltObjectSubRole.MiningStation:
                    design_0.Stance = BuiltObjectStance.AttackIfAttacked;
                    break;
                case BuiltObjectSubRole.Outpost:
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                case BuiltObjectSubRole.ResortBase:
                case BuiltObjectSubRole.GenericBase:
                case BuiltObjectSubRole.EnergyResearchStation:
                case BuiltObjectSubRole.WeaponsResearchStation:
                case BuiltObjectSubRole.HighTechResearchStation:
                case BuiltObjectSubRole.MonitoringStation:
                case BuiltObjectSubRole.DefensiveBase:
                    design_0.Stance = BuiltObjectStance.AttackEnemies;
                    break;
            }
            design_0.ReDefine();
        }

        private void btnDesignsSaveDesign_Click(object sender, EventArgs e)
        {
            PrepareDesignForEditor();
            if (string_16.ToLower(CultureInfo.InvariantCulture) != "view")
            {
                design_0.IsManuallyCreated = true;
            }
            List<string> list_ = new List<string>();
            List<string> list_2 = new List<string>();
            GetDesignWarningMessages(design_0, out list_, out list_2);
            pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
            if (list_.Count > 0)
            {
                MessageBoxEx messageBoxEx = method_370(TextResolver.GetText("All warnings in red must be resolved before this design can be saved"), TextResolver.GetText("Cannot Save Design"));
                messageBoxEx.Show(this);
                Focus();
                return;
            }
            if (design_2 != null)
            {
                design_2.IsObsolete = true;
                ctlDesignsList.SelectDesign(design_2);
                ctlDesignsList.SetObsolete(ctlDesignsList.Grid.SelectedRows[0], obsolete: true);
            }
            design_2 = null;
            switch (string_16.ToLower(CultureInfo.InvariantCulture))
            {
                case "addnew":
                    design_0.DateCreated = _Game.Galaxy.CurrentStarDate;
                    design_0.ReDefine();
                    _Game.PlayerEmpire.Designs.Add(design_0);
                    method_307(design_0);
                    method_293();
                    break;
                case "copyasnew":
                    design_0.DateCreated = _Game.Galaxy.CurrentStarDate;
                    design_0.ReDefine();
                    _Game.PlayerEmpire.Designs.Add(design_0);
                    method_307(design_0);
                    method_293();
                    break;
                case "edit":
                case "view":
                    {
                        int num = _Game.PlayerEmpire.Designs.IndexOf(design_0);
                        if (num < 0 && pnlDesigns.Visible)
                        {
                            Design selectedDesign = ctlDesignsList.SelectedDesign;
                            num = _Game.PlayerEmpire.Designs.IndexOf(selectedDesign);
                        }
                        if (num >= 0 && pnlDesigns.Visible)
                        {
                            _Game.PlayerEmpire.Designs[num] = design_0;
                            _Game.PlayerEmpire.Designs[num].ReDefine();
                            method_307(_Game.PlayerEmpire.Designs[num]);
                        }
                        method_293();
                        break;
                    }
            }
        }

        private void GetDesignWarningMessages(Design objectDesign, out List<string> mustDo, out List<string> shouldDo)
        {
            mustDo = new List<string>();
            shouldDo = new List<string>();
            ComponentResourceList componentResourceList = _Game.PlayerEmpire.ResolveResourcesFromComponents(objectDesign.Components);
            ResourceList resourceList = _Game.PlayerEmpire.DetermineResourcesEmpireSupplies();
            ResourceList resourceList2 = new ResourceList();
            for (int i = 0; i < componentResourceList.Count; i++)
            {
                if (!resourceList.Contains(componentResourceList[i]))
                {
                    resourceList2.Add(componentResourceList[i]);
                }
            }
            if (resourceList2.Count > 0)
            {
                string text = string.Empty;
                for (int j = 0; j < resourceList2.Count; j++)
                {
                    if (j > 0)
                    {
                        text += ", ";
                    }
                    text += resourceList2[j].Name;
                }
                string item = string.Format(TextResolver.GetText("We do not have a supply of all required resources"), "(" + text + ")");
                shouldDo.Add(item);
            }
            List<ComponentType> list = new List<ComponentType>();
            List<ComponentCategoryType> list2 = new List<ComponentCategoryType>();
            List<ComponentType> list3 = new List<ComponentType>();
            List<ComponentCategoryType> list4 = new List<ComponentCategoryType>();
            List<ComponentType> list5 = new List<ComponentType>();
            List<ComponentCategoryType> list6 = new List<ComponentCategoryType>();
            list.Add(ComponentType.ComputerCommandCenter);
            list.Add(ComponentType.StorageFuel);
            list2.Add(ComponentCategoryType.Reactor);
            if (objectDesign.Role != BuiltObjectRole.Colony)
            {
                list3.Add(ComponentType.HabitationColonization);
            }
            switch (objectDesign.Role)
            {
                case BuiltObjectRole.Military:
                case BuiltObjectRole.Exploration:
                case BuiltObjectRole.Freight:
                case BuiltObjectRole.Passenger:
                case BuiltObjectRole.Colony:
                case BuiltObjectRole.Build:
                case BuiltObjectRole.Resource:
                    list.Add(ComponentType.EngineMainThrust);
                    list.Add(ComponentType.EngineVectoring);
                    list6.Add(ComponentCategoryType.HyperDrive);
                    break;
                case BuiltObjectRole.Base:
                    list.Add(ComponentType.StorageDockingBay);
                    list4.Add(ComponentCategoryType.Engine);
                    list4.Add(ComponentCategoryType.HyperDrive);
                    break;
            }
            switch (objectDesign.Role)
            {
                case BuiltObjectRole.Military:
                    list6.Add(ComponentCategoryType.Shields);
                    list6.Add(ComponentCategoryType.Armor);
                    break;
                case BuiltObjectRole.Exploration:
                    list.Add(ComponentType.SensorResourceProfileSensor);
                    break;
                case BuiltObjectRole.Freight:
                    list.Add(ComponentType.StorageCargo);
                    break;
                case BuiltObjectRole.Passenger:
                    list.Add(ComponentType.StoragePassenger);
                    break;
                case BuiltObjectRole.Colony:
                    list.Add(ComponentType.HabitationColonization);
                    break;
                case BuiltObjectRole.Build:
                    list.Add(ComponentType.StorageDockingBay);
                    list.Add(ComponentType.StorageCargo);
                    list.Add(ComponentType.ConstructionBuild);
                    list.Add(ComponentType.ManufacturerEnergyPlant);
                    list.Add(ComponentType.ManufacturerHighTechPlant);
                    list.Add(ComponentType.ManufacturerWeaponsPlant);
                    break;
                case BuiltObjectRole.Resource:
                    list.Add(ComponentType.StorageCargo);
                    list2.Add(ComponentCategoryType.Extractor);
                    break;
            }
            switch (objectDesign.SubRole)
            {
                case BuiltObjectSubRole.Destroyer:
                    list5.Add(ComponentType.StorageTroop);
                    break;
                case BuiltObjectSubRole.Cruiser:
                    list5.Add(ComponentType.StorageTroop);
                    break;
                case BuiltObjectSubRole.CapitalShip:
                    list5.Add(ComponentType.StorageTroop);
                    break;
                case BuiltObjectSubRole.TroopTransport:
                    list.Add(ComponentType.StorageTroop);
                    break;
                case BuiltObjectSubRole.Carrier:
                    list.Add(ComponentType.FighterBay);
                    break;
                case BuiltObjectSubRole.ResupplyShip:
                    list.Add(ComponentType.ExtractorGasExtractor);
                    list.Add(ComponentType.StorageCargo);
                    list.Add(ComponentType.StorageDockingBay);
                    list5.Add(ComponentType.EnergyCollector);
                    list5.Add(ComponentType.SensorResourceProfileSensor);
                    break;
                case BuiltObjectSubRole.GasMiningStation:
                    list.Add(ComponentType.ExtractorGasExtractor);
                    list.Add(ComponentType.ComputerCommerceCenter);
                    list.Add(ComponentType.StorageCargo);
                    break;
                case BuiltObjectSubRole.MiningStation:
                    list.Add(ComponentType.ExtractorMine);
                    list.Add(ComponentType.ComputerCommerceCenter);
                    list.Add(ComponentType.StorageCargo);
                    break;
                case BuiltObjectSubRole.Outpost:
                    list.Add(ComponentType.StorageCargo);
                    list.Add(ComponentType.HabitationRecreationCenter);
                    list.Add(ComponentType.HabitationMedicalCenter);
                    list5.Add(ComponentType.EnergyCollector);
                    list4.Add(ComponentCategoryType.WeaponArea);
                    list4.Add(ComponentCategoryType.AssaultPod);
                    list4.Add(ComponentCategoryType.Manufacturer);
                    list4.Add(ComponentCategoryType.Engine);
                    list4.Add(ComponentCategoryType.Fighter);
                    list4.Add(ComponentCategoryType.Extractor);
                    list4.Add(ComponentCategoryType.Labs);
                    list4.Add(ComponentCategoryType.HyperDisrupt);
                    list4.Add(ComponentCategoryType.Sensor);
                    list4.Add(ComponentCategoryType.ShieldRecharge);
                    list4.Add(ComponentCategoryType.Shields);
                    list4.Add(ComponentCategoryType.WeaponBeam);
                    list4.Add(ComponentCategoryType.WeaponGravity);
                    list4.Add(ComponentCategoryType.WeaponIon);
                    list4.Add(ComponentCategoryType.WeaponPointDefense);
                    list4.Add(ComponentCategoryType.WeaponSuperTorpedo);
                    list4.Add(ComponentCategoryType.WeaponTorpedo);
                    break;
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                    list.Add(ComponentType.ComputerCommerceCenter);
                    list.Add(ComponentType.ConstructionBuild);
                    list.Add(ComponentType.ManufacturerEnergyPlant);
                    list.Add(ComponentType.ManufacturerHighTechPlant);
                    list.Add(ComponentType.ManufacturerWeaponsPlant);
                    list5.Add(ComponentType.EnergyCollector);
                    list6.Add(ComponentCategoryType.Labs);
                    list6.Add(ComponentCategoryType.Shields);
                    list6.Add(ComponentCategoryType.Armor);
                    break;
                case BuiltObjectSubRole.ResortBase:
                    list.Add(ComponentType.ComputerCommerceCenter);
                    list.Add(ComponentType.HabitationRecreationCenter);
                    list.Add(ComponentType.StoragePassenger);
                    list5.Add(ComponentType.EnergyCollector);
                    list5.Add(ComponentType.StorageCargo);
                    list6.Add(ComponentCategoryType.Shields);
                    list6.Add(ComponentCategoryType.Armor);
                    break;
                case BuiltObjectSubRole.GenericBase:
                    list.Add(ComponentType.StorageCargo);
                    break;
                case BuiltObjectSubRole.EnergyResearchStation:
                    list.Add(ComponentType.LabsEnergyLab);
                    list5.Add(ComponentType.EnergyCollector);
                    break;
                case BuiltObjectSubRole.WeaponsResearchStation:
                    list.Add(ComponentType.LabsWeaponsLab);
                    list5.Add(ComponentType.EnergyCollector);
                    break;
                case BuiltObjectSubRole.HighTechResearchStation:
                    list.Add(ComponentType.LabsHighTechLab);
                    list5.Add(ComponentType.EnergyCollector);
                    break;
                case BuiltObjectSubRole.MonitoringStation:
                    list.Add(ComponentType.SensorLongRange);
                    list5.Add(ComponentType.EnergyCollector);
                    break;
                case BuiltObjectSubRole.DefensiveBase:
                    list6.Add(ComponentCategoryType.Shields);
                    list6.Add(ComponentCategoryType.Armor);
                    list5.Add(ComponentType.EnergyCollector);
                    break;
            }
            for (int k = 0; k < list2.Count; k++)
            {
                ComponentCategoryType componentCategoryType = list2[k];
                bool flag = false;
                for (int l = 0; l < objectDesign.Components.Count; l++)
                {
                    DistantWorlds.Types.Component component = objectDesign.Components[l];
                    if (component.Category == componentCategoryType)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    string item2 = string.Format(TextResolver.GetText("Must have a X component"), Galaxy.ResolveDescription(componentCategoryType));
                    mustDo.Add(item2);
                }
            }
            for (int m = 0; m < list.Count; m++)
            {
                ComponentType componentType = list[m];
                bool flag2 = false;
                for (int n = 0; n < objectDesign.Components.Count; n++)
                {
                    DistantWorlds.Types.Component component2 = objectDesign.Components[n];
                    if (component2.Type == componentType)
                    {
                        flag2 = true;
                        break;
                    }
                }
                if (!flag2)
                {
                    string item3 = string.Format(TextResolver.GetText("Must have a X component"), Galaxy.ResolveDescription(componentType));
                    mustDo.Add(item3);
                }
            }
            for (int num = 0; num < list4.Count; num++)
            {
                ComponentCategoryType componentCategoryType2 = list4[num];
                bool flag3 = false;
                for (int num2 = 0; num2 < objectDesign.Components.Count; num2++)
                {
                    DistantWorlds.Types.Component component3 = objectDesign.Components[num2];
                    if (component3.Category == componentCategoryType2)
                    {
                        flag3 = true;
                        break;
                    }
                }
                if (flag3)
                {
                    string item4 = string.Format(TextResolver.GetText("Must NOT have X components"), Galaxy.ResolveDescription(componentCategoryType2));
                    mustDo.Add(item4);
                }
            }
            for (int num3 = 0; num3 < list3.Count; num3++)
            {
                ComponentType componentType2 = list3[num3];
                bool flag4 = false;
                for (int num4 = 0; num4 < objectDesign.Components.Count; num4++)
                {
                    DistantWorlds.Types.Component component4 = objectDesign.Components[num4];
                    if (component4.Type == componentType2)
                    {
                        flag4 = true;
                        break;
                    }
                }
                if (flag4)
                {
                    string item5 = string.Format(TextResolver.GetText("Must NOT have X components"), Galaxy.ResolveDescription(componentType2));
                    mustDo.Add(item5);
                }
            }
            for (int num5 = 0; num5 < list6.Count; num5++)
            {
                ComponentCategoryType componentCategoryType3 = list6[num5];
                bool flag5 = false;
                for (int num6 = 0; num6 < objectDesign.Components.Count; num6++)
                {
                    DistantWorlds.Types.Component component5 = objectDesign.Components[num6];
                    if (component5.Category == componentCategoryType3)
                    {
                        flag5 = true;
                        break;
                    }
                }
                if (!flag5)
                {
                    string item6 = string.Format(TextResolver.GetText("Consider adding X components"), Galaxy.ResolveDescription(componentCategoryType3));
                    shouldDo.Add(item6);
                }
            }
            for (int num7 = 0; num7 < list5.Count; num7++)
            {
                ComponentType componentType3 = list5[num7];
                bool flag6 = false;
                for (int num8 = 0; num8 < objectDesign.Components.Count; num8++)
                {
                    DistantWorlds.Types.Component component6 = objectDesign.Components[num8];
                    if (component6.Type == componentType3)
                    {
                        flag6 = true;
                        break;
                    }
                }
                if (!flag6)
                {
                    string item7 = string.Format(TextResolver.GetText("Consider adding X components"), Galaxy.ResolveDescription(componentType3));
                    shouldDo.Add(item7);
                }
            }
            switch (objectDesign.SubRole)
            {
                case BuiltObjectSubRole.Escort:
                case BuiltObjectSubRole.Frigate:
                case BuiltObjectSubRole.Destroyer:
                case BuiltObjectSubRole.Cruiser:
                case BuiltObjectSubRole.CapitalShip:
                case BuiltObjectSubRole.DefensiveBase:
                    {
                        bool flag7 = false;
                        for (int num13 = 0; num13 < objectDesign.Components.Count; num13++)
                        {
                            DistantWorlds.Types.Component component8 = objectDesign.Components[num13];
                            if (component8.Category == ComponentCategoryType.WeaponBeam || component8.Category == ComponentCategoryType.WeaponTorpedo || component8.Category == ComponentCategoryType.WeaponArea || component8.Category == ComponentCategoryType.WeaponIon || component8.Category == ComponentCategoryType.WeaponSuperArea || component8.Category == ComponentCategoryType.WeaponSuperBeam || component8.Category == ComponentCategoryType.WeaponSuperTorpedo || component8.Type == ComponentType.WeaponGravityBeam || component8.Type == ComponentType.WeaponAreaGravity || component8.Type == ComponentType.FighterBay)
                            {
                                flag7 = true;
                                break;
                            }
                        }
                        if (!flag7)
                        {
                            string item11 = TextResolver.GetText("Military ships must have weapons");
                            if (objectDesign.SubRole == BuiltObjectSubRole.DefensiveBase)
                            {
                                item11 = TextResolver.GetText("Defensive bases must have weapons");
                            }
                            mustDo.Add(item11);
                        }
                        break;
                    }
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    {
                        int num9 = 0;
                        int num10 = 0;
                        int num11 = 0;
                        for (int num12 = 0; num12 < objectDesign.Components.Count; num12++)
                        {
                            DistantWorlds.Types.Component component7 = objectDesign.Components[num12];
                            if (component7.Category == ComponentCategoryType.WeaponBeam || component7.Category == ComponentCategoryType.WeaponTorpedo || component7.Category == ComponentCategoryType.WeaponArea)
                            {
                                num9++;
                            }
                            if (component7.Type == ComponentType.WeaponIonCannon || component7.Type == ComponentType.WeaponIonPulse || component7.Type == ComponentType.WeaponAreaGravity || component7.Type == ComponentType.WeaponGravityBeam)
                            {
                                num9++;
                            }
                            if (component7.Category == ComponentCategoryType.Fighter)
                            {
                                num11++;
                            }
                            if (component7.Category == ComponentCategoryType.WeaponSuperArea || component7.Category == ComponentCategoryType.WeaponSuperBeam || component7.Category == ComponentCategoryType.WeaponSuperTorpedo)
                            {
                                num10++;
                            }
                        }
                        if (num9 > 10)
                        {
                            string item8 = TextResolver.GetText("Civilian ships may not have more than 10 weapons.");
                            mustDo.Add(item8);
                        }
                        if (num10 > 0)
                        {
                            string item9 = TextResolver.GetText("Civilian ships cannot have super weapons");
                            mustDo.Add(item9);
                        }
                        if (num11 > 10)
                        {
                            string item10 = TextResolver.GetText("Civilian ships cannot have fighter bays");
                            mustDo.Add(item10);
                        }
                        break;
                    }
            }
            switch (objectDesign.SubRole)
            {
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                case BuiltObjectSubRole.ResupplyShip:
                case BuiltObjectSubRole.MiningStation:
                case BuiltObjectSubRole.GasMiningStation:
                    {
                        var exctractorCol = objectDesign.Components.Where(x => x.Category == ComponentCategoryType.Extractor);
                        if (exctractorCol.Count() > 0)
                        {
                            IEnumerable<DistantWorlds.Types.Component>[] typesCol = new IEnumerable<DistantWorlds.Types.Component>[3];
                            var gasCol = exctractorCol.Where(x => x.Type == ComponentType.ExtractorGasExtractor);
                            var miningCol = exctractorCol.Where(x => x.Type == ComponentType.ExtractorMine);
                            var luxCol = exctractorCol.Where(x => x.Type == ComponentType.ExtractorLuxury);
                            typesCol[0] = gasCol;
                            typesCol[1] = miningCol;
                            typesCol[2] = luxCol;
                            foreach (var item in typesCol)
                            {
                                if (item.Count() > 0)
                                {
                                    int max = _Game.PlayerEmpire.Research.ResolveImprovedComponentValues(item.First()).Value2;
                                    int currentMiningValue = item.Select(x => _Game.PlayerEmpire.Research.ResolveImprovedComponentValues(x)).Sum(y => y.Value1);
                                    if (currentMiningValue > max)
                                    {
                                        shouldDo.Add($"Current amount of {Galaxy.ResolveDescription(item.First().Type)} is above mining limit of {max}");
                                    }
                                    else if (currentMiningValue < max)
                                    {
                                        shouldDo.Add($"Current amount of {Galaxy.ResolveDescription(item.First().Type)} is below mining limit of {max}");
                                    }
                                }
                            }
                        }
                        break;
                    }
            }
            int num14 = 0;
            for (int num15 = 0; num15 < objectDesign.Components.Count; num15++)
            {
                DistantWorlds.Types.Component component9 = objectDesign.Components[num15];
                ComponentImprovement componentImprovement = _Game.PlayerEmpire.Research.ResolveImprovedComponentValues(objectDesign.Components[num15]);
                switch (component9.Type)
                {
                    case ComponentType.WeaponSuperBeam:
                    case ComponentType.WeaponSuperArea:
                    case ComponentType.WeaponSuperTorpedo:
                    case ComponentType.WeaponSuperMissile:
                    case ComponentType.WeaponSuperPhaser:
                    case ComponentType.WeaponSuperRailGun:
                        num14 += component9.EnergyUsed;
                        num14 += componentImprovement.Value3;
                        break;
                }
            }
            if (num14 > 0 && objectDesign.ReactorStorageCapacity < objectDesign.StaticEnergyConsumption + objectDesign.TopSpeedFuelBurn + num14)
            {
                shouldDo.Add(TextResolver.GetText("Superweapon may not fire because total reactor energy output and storage is low"));
            }
            if (objectDesign.SubRole == BuiltObjectSubRole.Carrier && !_Game.PlayerEmpire.CanBuildCarriers)
            {
                shouldDo.Add(string.Format(TextResolver.GetText("Cannot currently build a design of this ship type"), Galaxy.ResolveDescription(objectDesign.SubRole)));
            }
            else if (objectDesign.SubRole == BuiltObjectSubRole.ResupplyShip && !_Game.PlayerEmpire.CanBuildResupplyShips)
            {
                shouldDo.Add(string.Format(TextResolver.GetText("Cannot currently build a design of this ship type"), Galaxy.ResolveDescription(objectDesign.SubRole)));
            }
            if (objectDesign.TopSpeed > 0)
            {
                if (objectDesign.Role != BuiltObjectRole.Colony && objectDesign.Role != BuiltObjectRole.Build && objectDesign.SubRole != BuiltObjectSubRole.ResupplyShip)
                {
                    int num16 = _Game.PlayerEmpire.MaximumConstructionSize(objectDesign.SubRole);
                    if (objectDesign.IsPlanetDestroyer)
                    {
                        num16 = _Game.PlayerEmpire.MaximumConstructionSizeBase();
                    }
                    if (objectDesign.Size > num16)
                    {
                        shouldDo.Add(string.Format(TextResolver.GetText("Cannot currently build a design of this size maximum size"), objectDesign.Size.ToString(), num16.ToString()));
                    }
                }
                else
                {
                    int num17 = _Game.PlayerEmpire.MaximumConstructionSizeBase(objectDesign.SubRole);
                    if (objectDesign.Size > num17)
                    {
                        shouldDo.Add(string.Format(TextResolver.GetText("Cannot currently build a design of this size maximum size"), objectDesign.Size.ToString(), num17.ToString()));
                    }
                }
            }
            else
            {
                switch (objectDesign.SubRole)
                {
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                        if (objectDesign.Size > _Game.PlayerEmpire.MaximumConstructionSizeBase(objectDesign.SubRole))
                        {
                            shouldDo.Add(string.Format(TextResolver.GetText("Cannot currently build a design of this size"), objectDesign.Size.ToString()));
                        }
                        break;
                    case BuiltObjectSubRole.ResortBase:
                    case BuiltObjectSubRole.GenericBase:
                    case BuiltObjectSubRole.EnergyResearchStation:
                    case BuiltObjectSubRole.WeaponsResearchStation:
                    case BuiltObjectSubRole.HighTechResearchStation:
                    case BuiltObjectSubRole.MonitoringStation:
                    case BuiltObjectSubRole.DefensiveBase:
                        if (objectDesign.Size > _Game.PlayerEmpire.MaximumConstructionSizeBase(objectDesign.SubRole))
                        {
                            shouldDo.Add(string.Format(TextResolver.GetText("Cannot currently build a design of this size unless at colony"), objectDesign.Size.ToString()));
                        }
                        break;
                }
            }
            if (objectDesign.SubRole == BuiltObjectSubRole.ResupplyShip || objectDesign.SubRole == BuiltObjectSubRole.ConstructionShip || objectDesign.SubRole == BuiltObjectSubRole.ColonyShip || objectDesign.SubRole == BuiltObjectSubRole.Carrier)
            {
                int num18 = 0;
                int num19 = 0;
                int num20 = 0;
                int num21 = 0;
                int num22 = 0;
                int num23 = 0;
                for (int num24 = 0; num24 < objectDesign.Components.Count; num24++)
                {
                    if (objectDesign.Components[num24].Type == ComponentType.FighterBay)
                    {
                        num23 += objectDesign.Components[num24].Size;
                    }
                    if (objectDesign.Components[num24].Type == ComponentType.HabitationColonization)
                    {
                        num22 += objectDesign.Components[num24].Size;
                    }
                    if (objectDesign.Components[num24].Type == ComponentType.StorageCargo)
                    {
                        num18 += objectDesign.Components[num24].Size;
                    }
                    if (objectDesign.Components[num24].Type == ComponentType.ConstructionBuild || objectDesign.Components[num24].Category == ComponentCategoryType.Manufacturer)
                    {
                        num20 += objectDesign.Components[num24].Size;
                    }
                    if (objectDesign.Components[num24].Type == ComponentType.StorageDockingBay)
                    {
                        num19 += objectDesign.Components[num24].Size;
                    }
                    if (objectDesign.Components[num24].Type == ComponentType.ExtractorGasExtractor)
                    {
                        num21 += objectDesign.Components[num24].Size;
                    }
                }
                if (objectDesign.SubRole == BuiltObjectSubRole.ResupplyShip)
                {
                    double num25 = ((double)num18 + (double)num19 + (double)num21) / (double)objectDesign.Size;
                    if (num25 < 0.2)
                    {
                        mustDo.Add(string.Format(TextResolver.GetText("Resupply Ships: min X% cargo storage, docking bays, gas extractors"), "20"));
                    }
                }
                else if (objectDesign.SubRole == BuiltObjectSubRole.Carrier)
                {
                    double num26 = (double)num23 / (double)objectDesign.Size;
                    if (num26 < 0.4)
                    {
                        mustDo.Add(string.Format(TextResolver.GetText("Carriers: min X% fighter bays"), "40"));
                    }
                }
                else if (objectDesign.SubRole == BuiltObjectSubRole.ConstructionShip)
                {
                    double num27 = ((double)num18 + (double)num20) / (double)objectDesign.Size;
                    if (num27 < 0.35)
                    {
                        mustDo.Add(string.Format(TextResolver.GetText("Construction Ships: min X% cargo storage, construction, manufacturers"), "35"));
                    }
                }
                else if (objectDesign.SubRole == BuiltObjectSubRole.ColonyShip)
                {
                    double num28 = (double)num22 / (double)objectDesign.Size;
                    if (num28 < 0.5)
                    {
                        mustDo.Add(string.Format(TextResolver.GetText("Colony Ships: min X% colonization module"), "50"));
                    }
                }
            }
            int num29 = 0;
            int num30 = 0;
            int num31 = 0;
            for (int num32 = 0; num32 < objectDesign.Components.Count; num32++)
            {
                DistantWorlds.Types.Component component10 = objectDesign.Components[num32];
                ComponentImprovement componentImprovement2 = _Game.PlayerEmpire.Research.ResolveImprovedComponentValues(objectDesign.Components[num32]);
                if (component10.Type == ComponentType.HabitationHabModule)
                {
                    num30 += componentImprovement2.Value1;
                }
                else if (component10.Type == ComponentType.HabitationLifeSupport)
                {
                    num31 += componentImprovement2.Value1;
                }
                else
                {
                    num29 += component10.Size;
                }
            }
            if (objectDesign.Role == BuiltObjectRole.Base)
            {
                num29 /= 2;
            }
            if (num29 > num30)
            {
                mustDo.Add(TextResolver.GetText("Need more Habitation Modules"));
            }
            if (num29 > num31)
            {
                mustDo.Add(TextResolver.GetText("Need more Life Support components"));
            }
            if (objectDesign.ReactorPowerOutput < objectDesign.StaticEnergyConsumption)
            {
                if (objectDesign.Role == BuiltObjectRole.Base)
                {
                    shouldDo.Add(TextResolver.GetText("Reactor power output inadequate to supply static energy requirements"));
                }
                else
                {
                    mustDo.Add(TextResolver.GetText("Reactor power output inadequate to supply static energy requirements"));
                }
            }
            int num33 = 0;
            for (int num34 = 0; num34 < objectDesign.Components.Count; num34++)
            {
                DistantWorlds.Types.Component component11 = objectDesign.Components[num34];
                if (component11.Category == ComponentCategoryType.HyperDrive)
                {
                    num33++;
                }
            }
            if (num33 > 1)
            {
                shouldDo.Add(TextResolver.GetText("Only one HyperDrive component is required"));
            }
            if (string.IsNullOrEmpty(objectDesign.Name))
            {
                mustDo.Add(TextResolver.GetText("Design must have a name"));
            }
            if (objectDesign.SubRole == BuiltObjectSubRole.Undefined)
            {
                mustDo.Add(TextResolver.GetText("Must set Role for Design"));
            }
            if (objectDesign.FleeWhen == BuiltObjectFleeWhen.Undefined)
            {
                mustDo.Add(TextResolver.GetText("Must set FleeWhen for Design"));
            }
            if (objectDesign.TacticsInvasion == InvasionTactics.Undefined)
            {
                mustDo.Add(TextResolver.GetText("Must set Invasion Tactics for Design"));
            }
            if (objectDesign.TacticsStrongerShips == BattleTactics.Undefined)
            {
                mustDo.Add(TextResolver.GetText("Must set Battle Tactics against stronger opponents for this Design"));
            }
            if (objectDesign.TacticsWeakerShips == BattleTactics.Undefined)
            {
                mustDo.Add(TextResolver.GetText("Must set Battle Tactics against weaker opponents for this Design"));
            }
            switch (objectDesign.SubRole)
            {
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    if (objectDesign.TacticsStrongerShips != BattleTactics.Evade || objectDesign.TacticsWeakerShips != BattleTactics.Evade)
                    {
                        mustDo.Add(TextResolver.GetText("Battle Tactics for civilian ships must be Evade"));
                    }
                    if (objectDesign.TacticsInvasion != InvasionTactics.DoNotInvade)
                    {
                        mustDo.Add(TextResolver.GetText("Invasion Tactics for civilian ships must be Do Not Invade"));
                    }
                    break;
                case BuiltObjectSubRole.ColonyShip:
                case BuiltObjectSubRole.ConstructionShip:
                    break;
            }
        }

        private void chkDesignComponentsShowLatest_CheckedChanged(object sender, EventArgs e)
        {
            method_291(design_0);
        }

        private void cmbDesignsSubRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = false;
            if (design_0 == null || design_0.SubRole == BuiltObjectSubRole.Undefined)
            {
                flag = true;
            }
            PrepareDesignForEditor();
            if (flag && design_0 != null)
            {
                method_388(design_0.SubRole, out var battleTactics_, out var battleTactics_2, out var invasionTactics_, out var builtObjectStance_, out var builtObjectFleeWhen_);
                design_0.TacticsStrongerShips = battleTactics_;
                design_0.TacticsWeakerShips = battleTactics_2;
                design_0.TacticsInvasion = invasionTactics_;
                design_0.Stance = builtObjectStance_;
                design_0.FleeWhen = builtObjectFleeWhen_;
                int pictureRef = ShipImageHelper.ResolveNewShipImageIndex(design_0.SubRole, _Game.PlayerEmpire.DominantRace, _Game.PlayerEmpire.PirateEmpireBaseHabitat != null);
                design_0.PictureRef = pictureRef;
                method_291(design_0);
            }
            List<string> list_ = new List<string>();
            List<string> list_2 = new List<string>();
            GetDesignWarningMessages(design_0, out list_, out list_2);
            pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
        }

        private void method_388(BuiltObjectSubRole builtObjectSubRole_0, out BattleTactics battleTactics_0, out BattleTactics battleTactics_1, out InvasionTactics invasionTactics_0, out BuiltObjectStance builtObjectStance_0, out BuiltObjectFleeWhen builtObjectFleeWhen_0)
        {
            builtObjectStance_0 = BuiltObjectStance.DoNotAttack;
            builtObjectFleeWhen_0 = BuiltObjectFleeWhen.Attacked;
            battleTactics_0 = BattleTactics.Standoff;
            battleTactics_1 = BattleTactics.AllWeapons;
            invasionTactics_0 = InvasionTactics.DoNotInvade;
            switch (builtObjectSubRole_0)
            {
                default:
                    builtObjectStance_0 = BuiltObjectStance.DoNotAttack;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.Attacked;
                    battleTactics_0 = BattleTactics.Standoff;
                    battleTactics_1 = BattleTactics.AllWeapons;
                    invasionTactics_0 = InvasionTactics.DoNotInvade;
                    break;
                case BuiltObjectSubRole.Escort:
                case BuiltObjectSubRole.Frigate:
                case BuiltObjectSubRole.Destroyer:
                case BuiltObjectSubRole.Cruiser:
                case BuiltObjectSubRole.CapitalShip:
                    builtObjectStance_0 = BuiltObjectStance.AttackEnemies;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.Shields20;
                    if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Policy != null)
                    {
                        builtObjectFleeWhen_0 = _Game.PlayerEmpire.Policy.DefaultMilitaryFleeWhen;
                    }
                    battleTactics_0 = BattleTactics.Standoff;
                    battleTactics_1 = BattleTactics.AllWeapons;
                    invasionTactics_0 = InvasionTactics.InvadeWhenClear;
                    break;
                case BuiltObjectSubRole.TroopTransport:
                    builtObjectStance_0 = BuiltObjectStance.AttackEnemies;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.Shields50;
                    battleTactics_0 = BattleTactics.Evade;
                    battleTactics_1 = BattleTactics.AllWeapons;
                    invasionTactics_0 = InvasionTactics.InvadeImmediately;
                    break;
                case BuiltObjectSubRole.Carrier:
                    builtObjectStance_0 = BuiltObjectStance.AttackEnemies;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.Shields50;
                    battleTactics_0 = BattleTactics.Evade;
                    battleTactics_1 = BattleTactics.AllWeapons;
                    invasionTactics_0 = InvasionTactics.InvadeWhenClear;
                    break;
                case BuiltObjectSubRole.ResupplyShip:
                    builtObjectStance_0 = BuiltObjectStance.AttackIfAttacked;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.Shields50;
                    battleTactics_0 = BattleTactics.Evade;
                    battleTactics_1 = BattleTactics.AllWeapons;
                    invasionTactics_0 = InvasionTactics.DoNotInvade;
                    break;
                case BuiltObjectSubRole.ExplorationShip:
                    builtObjectStance_0 = BuiltObjectStance.AttackIfAttacked;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.EnemyMilitarySighted;
                    battleTactics_0 = BattleTactics.Evade;
                    battleTactics_1 = BattleTactics.Evade;
                    invasionTactics_0 = InvasionTactics.DoNotInvade;
                    break;
                case BuiltObjectSubRole.ColonyShip:
                    builtObjectStance_0 = BuiltObjectStance.DoNotAttack;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.EnemyMilitarySighted;
                    battleTactics_0 = BattleTactics.Evade;
                    battleTactics_1 = BattleTactics.Evade;
                    invasionTactics_0 = InvasionTactics.DoNotInvade;
                    break;
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.PassengerShip:
                    builtObjectStance_0 = BuiltObjectStance.DoNotAttack;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.EnemyMilitarySighted;
                    battleTactics_0 = BattleTactics.Evade;
                    battleTactics_1 = BattleTactics.Evade;
                    invasionTactics_0 = InvasionTactics.DoNotInvade;
                    break;
                case BuiltObjectSubRole.ConstructionShip:
                    builtObjectStance_0 = BuiltObjectStance.DoNotAttack;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.EnemyMilitarySighted;
                    battleTactics_0 = BattleTactics.Evade;
                    battleTactics_1 = BattleTactics.Evade;
                    invasionTactics_0 = InvasionTactics.DoNotInvade;
                    break;
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    builtObjectStance_0 = BuiltObjectStance.DoNotAttack;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.EnemyMilitarySighted;
                    battleTactics_0 = BattleTactics.Evade;
                    battleTactics_1 = BattleTactics.Evade;
                    invasionTactics_0 = InvasionTactics.DoNotInvade;
                    break;
                case BuiltObjectSubRole.GasMiningStation:
                case BuiltObjectSubRole.MiningStation:
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                case BuiltObjectSubRole.ResortBase:
                case BuiltObjectSubRole.GenericBase:
                case BuiltObjectSubRole.EnergyResearchStation:
                case BuiltObjectSubRole.WeaponsResearchStation:
                case BuiltObjectSubRole.HighTechResearchStation:
                case BuiltObjectSubRole.MonitoringStation:
                case BuiltObjectSubRole.DefensiveBase:
                    builtObjectStance_0 = BuiltObjectStance.AttackEnemies;
                    builtObjectFleeWhen_0 = BuiltObjectFleeWhen.Never;
                    battleTactics_0 = BattleTactics.PointBlank;
                    battleTactics_1 = BattleTactics.PointBlank;
                    invasionTactics_0 = InvasionTactics.DoNotInvade;
                    break;
            }
        }

        private void ctlDesignComponentToolbox_SelectionChanged(object sender, EventArgs e)
        {
            DistantWorlds.Types.Component selectedComponent = ctlDesignComponentToolbox.SelectedComponent;
            if (selectedComponent != null)
            {
                pnlDesignComponentDetail.Ignite(_Game.Galaxy, selectedComponent, bitmap_21[selectedComponent.PictureRef]);
            }
            else
            {
                pnlDesignComponentDetail.Ignite(_Game.Galaxy, null, null);
            }
        }

        private void ctlDesignComponents_SelectionChanged(object sender, EventArgs e)
        {
            DistantWorlds.Types.Component selectedComponent = ctlDesignComponents.SelectedComponent;
            if (selectedComponent != null)
            {
                pnlDesignComponentDetail.Ignite(_Game.Galaxy, selectedComponent, bitmap_21[selectedComponent.PictureRef]);
                BaconDesign.ctlDesignComponents_SelectionChanged(this, sender, e);
            }
            else
            {
                pnlDesignComponentDetail.Ignite(_Game.Galaxy, null, null);
            }
        }

        private void btnDesignsAddNew_Click(object sender, EventArgs e)
        {
            if (_Game.PlayerEmpire.ControlDesigns && GenerateAutomationMessageBox(TextResolver.GetText("Ship Design")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                _Game.PlayerEmpire.ControlDesigns = false;
            }
            design_2 = null;
            string_16 = "addnew";
            design_0 = new Design(string.Empty);
            design_0.Empire = _Game.PlayerEmpire;
            OpenDesignEditor(design_0);
        }

        private void btnDesignsCopyAsNew_Click(object sender, EventArgs e)
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
            string_16 = "copyasnew";
            design_0 = selectedDesign.Clone();
            design_0.IsObsolete = false;
            design_0.BuildCount = 0;
            design_0.DateCreated = _Game.Galaxy.CurrentStarDate;
            string text = design_0.Name;
            if (text.Contains(" "))
            {
                bool flag = false;
                int num = text.LastIndexOf(" ");
                string text2 = string.Empty;
                string text3 = string.Empty;
                if (num >= 0)
                {
                    text3 = text.Substring(0, num);
                    text2 = text.Substring(num, text.Length - num).Trim();
                }
                if (text2.Contains("Mk") && text2.Length > 2)
                {
                    string s = text2.Substring(2, text2.Length - 2).Trim();
                    int result = 0;
                    int.TryParse(s, out result);
                    if (result > 0)
                    {
                        string text4 = "Mk" + (result + 1);
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
            design_0.Name = text;
            OpenDesignEditor(design_0);
        }

        private void btnDesignsDelete_Click(object sender, EventArgs e)
        {
            if (_Game.PlayerEmpire.ControlDesigns && GenerateAutomationMessageBox(TextResolver.GetText("Ship Design")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                _Game.PlayerEmpire.ControlDesigns = false;
            }
            DesignList selectedDesigns = ctlDesignsList.SelectedDesigns;
            design_2 = null;
            if (selectedDesigns == null || selectedDesigns.Count <= 0)
            {
                return;
            }
            if (selectedDesigns.Count == 1)
            {
                bool flag = false;
                foreach (BuiltObject builtObject in _Game.PlayerEmpire.BuiltObjects)
                {
                    if (builtObject.Design == selectedDesigns[0] || builtObject.RetrofitDesign == selectedDesigns[0])
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    foreach (BuiltObject privateBuiltObject in _Game.PlayerEmpire.PrivateBuiltObjects)
                    {
                        if (privateBuiltObject.Design == selectedDesigns[0] || privateBuiltObject.RetrofitDesign == selectedDesigns[0])
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    MessageBoxEx messageBoxEx = method_372(TextResolver.GetText("Are you sure that you want to delete this design?"), TextResolver.GetText("Delete Design"));
                    if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
                    {
                        int num = _Game.PlayerEmpire.Designs.IndexOf(selectedDesigns[0]);
                        if (num >= 0)
                        {
                            _Game.PlayerEmpire.Designs.RemoveAt(num);
                        }
                        ctlDesignsList.SelectionChanged -= ctlDesignsList_SelectionChanged;
                        foreach (DataGridViewRow selectedRow in ctlDesignsList.Grid.SelectedRows)
                        {
                            selectedRow.Selected = false;
                        }
                        ctlDesignsList.SelectionChanged += ctlDesignsList_SelectionChanged;
                        method_307(null);
                    }
                }
                else
                {
                    MessageBoxEx messageBoxEx2 = method_370(TextResolver.GetText("This design is in use and cannot be deleted"), TextResolver.GetText("Cannot Delete Design"));
                    messageBoxEx2.Show(this);
                }
            }
            else
            {
                DesignList designList = new DesignList();
                bool flag2 = false;
                for (int i = 0; i < selectedDesigns.Count; i++)
                {
                    bool flag3 = false;
                    foreach (BuiltObject builtObject2 in _Game.PlayerEmpire.BuiltObjects)
                    {
                        if (builtObject2.Design == selectedDesigns[i] || builtObject2.RetrofitDesign == selectedDesigns[i])
                        {
                            flag3 = true;
                            break;
                        }
                    }
                    if (!flag3)
                    {
                        foreach (BuiltObject privateBuiltObject2 in _Game.PlayerEmpire.PrivateBuiltObjects)
                        {
                            if (privateBuiltObject2.Design == selectedDesigns[i] || privateBuiltObject2.RetrofitDesign == selectedDesigns[i])
                            {
                                flag3 = true;
                                break;
                            }
                        }
                    }
                    if (!flag3)
                    {
                        designList.Add(selectedDesigns[i]);
                    }
                    else
                    {
                        flag2 = true;
                    }
                }
                if (designList.Count > 0)
                {
                    MessageBoxEx messageBoxEx3 = method_372(TextResolver.GetText("Are you sure that you want to delete these designs?"), TextResolver.GetText("Delete Designs"));
                    if (messageBoxEx3.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
                    {
                        for (int j = 0; j < designList.Count; j++)
                        {
                            int num2 = _Game.PlayerEmpire.Designs.IndexOf(designList[j]);
                            if (num2 >= 0)
                            {
                                _Game.PlayerEmpire.Designs.RemoveAt(num2);
                            }
                        }
                        ctlDesignsList.SelectionChanged -= ctlDesignsList_SelectionChanged;
                        foreach (DataGridViewRow selectedRow2 in ctlDesignsList.Grid.SelectedRows)
                        {
                            selectedRow2.Selected = false;
                        }
                        ctlDesignsList.SelectionChanged += ctlDesignsList_SelectionChanged;
                        method_307(null);
                        if (flag2)
                        {
                            MessageBoxEx messageBoxEx4 = method_370(TextResolver.GetText("Some of the selected designs were in use and could not be deleted"), TextResolver.GetText("Could Not Delete Some Designs"));
                            messageBoxEx4.Show(this);
                        }
                    }
                }
                else if (flag2)
                {
                    MessageBoxEx messageBoxEx5 = method_370(TextResolver.GetText("Some of the selected designs were in use and could not be deleted"), TextResolver.GetText("Could Not Delete Some Designs"));
                    messageBoxEx5.Show(this);
                }
            }
            Focus();
        }

        private void cmbDesignsFleeWhen_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrepareDesignForEditor();
            List<string> list_ = new List<string>();
            List<string> list_2 = new List<string>();
            GetDesignWarningMessages(design_0, out list_, out list_2);
            pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
        }

        private void cmbDesignTacticsStrongerShips_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrepareDesignForEditor();
            List<string> list_ = new List<string>();
            List<string> list_2 = new List<string>();
            GetDesignWarningMessages(design_0, out list_, out list_2);
            pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
        }

        private void cmbDesignTacticsWeakerShips_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrepareDesignForEditor();
            List<string> list_ = new List<string>();
            List<string> list_2 = new List<string>();
            GetDesignWarningMessages(design_0, out list_, out list_2);
            pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
        }

        private void cmbDesignTacticsInvasion_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrepareDesignForEditor();
            List<string> list_ = new List<string>();
            List<string> list_2 = new List<string>();
            GetDesignWarningMessages(design_0, out list_, out list_2);
            pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
        }

        private void btnResearchTreeWeapons_Click(object sender, EventArgs e)
        {
            method_395(IndustryType.Weapon, null, empire_3);
        }

        private void btnResearchTreeEnergy_Click(object sender, EventArgs e)
        {
            method_395(IndustryType.Energy, null, empire_3);
        }

        private void btnResearchTreeHighTech_Click(object sender, EventArgs e)
        {
            method_395(IndustryType.HighTech, null, empire_3);
        }

        private void btnResearchFacilities_Click(object sender, EventArgs e)
        {
            method_393(IndustryType.Undefined);
        }

        private Size method_389(int int_64, int int_65)
        {
            int num = int_64;
            int num2 = int_65;
            Rectangle rectangle = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
            num = ((rectangle.Right >= 1900) ? 1880 : ((rectangle.Right >= 1600) ? 1580 : ((rectangle.Right >= 1340) ? 1320 : ((rectangle.Right < 1280) ? int_64 : 1260))));
            num2 = ((rectangle.Bottom >= 1200) ? 1180 : ((rectangle.Bottom >= 1080) ? 1060 : ((rectangle.Bottom >= 1024) ? 1000 : ((rectangle.Bottom < 800) ? int_65 : 780))));
            return new Size(num, num2);
        }

        private void method_390()
        {
            if (method_391() != 0)
            {
                method_392();
                return;
            }
            tbtnResearch.ResetDefaultColors(resetRegion: false);
            tbtnResearch.Invalidate();
        }

        private IndustryType method_391()
        {
            IndustryType result = IndustryType.Undefined;
            if (_Game != null && _Game.PlayerEmpire != null && _Game.PlayerEmpire.Research != null)
            {
                if (_Game.PlayerEmpire.Research.ResearchQueueEnergy.Count == 0 && _Game.PlayerEmpire.Research.ResolveNextProjects(_Game.Galaxy, null, IndustryType.Energy).Count > 0)
                {
                    result = IndustryType.Energy;
                }
                else if (_Game.PlayerEmpire.Research.ResearchQueueHighTech.Count == 0 && _Game.PlayerEmpire.Research.ResolveNextProjects(_Game.Galaxy, null, IndustryType.HighTech).Count > 0)
                {
                    result = IndustryType.HighTech;
                }
                else if (_Game.PlayerEmpire.Research.ResearchQueueWeapons.Count == 0 && _Game.PlayerEmpire.Research.ResolveNextProjects(_Game.Galaxy, null, IndustryType.Weapon).Count > 0)
                {
                    result = IndustryType.Weapon;
                }
            }
            return result;
        }

        private void method_392()
        {
            tbtnResearch.SetBackColor(Color.FromArgb(0, 0, 48), resetRegion: false);
            tbtnResearch.SetOuterBorderColor(Color.FromArgb(0, 0, 64), resetRegion: false);
            tbtnResearch.SetShineColor(Color.FromArgb(128, 128, 255), resetRegion: false);
        }

        private void method_393(IndustryType industryType_0)
        {
            method_394(industryType_0, null);
        }

        private void method_394(IndustryType industryType_0, ResearchNode researchNode_0)
        {
            method_395(industryType_0, researchNode_0, _Game.PlayerEmpire);
        }

        private void method_395(IndustryType industryType_0, ResearchNode researchNode_0, Empire empire_5)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            empire_3 = empire_5;
            Size size = method_389(1020, 767);
            pnlResearch.Size = size;
            pnlResearch.Location = new Point((mainView.Width - pnlResearch.Width) / 2, (mainView.Height - pnlResearch.Height) / 2);
            if (pnlResearchTree.EditMode)
            {
                pnlResearch.HeaderTitle = TextResolver.GetText("Edit Research") + ": " + empire_5.Name;
            }
            else
            {
                pnlResearch.HeaderTitle = TextResolver.GetText("Research");
            }
            pnlResearch.DoLayout();
            btnResearchGotoFacility.Size = new Size(315, 30);
            btnResearchGotoFacility.Location = new Point(640, 30);
            btnResearchGotoFacility.Text = TextResolver.GetText("Go to Research Station");
            string text = "- " + TextResolver.GetText("Click a project to add it to the research queue (previous projects must already be researched)") + "\n";
            text = text + "- " + TextResolver.GetText("Right-click a queued project to cancel research, moving up subsequent projects in the queue") + "\n";
            text = text + "- " + TextResolver.GetText("Click the current project to initiate crash-research, spending credits to shorten the research time") + "\n";
            lblResearchInstructions.Location = new Point(10, 10);
            lblResearchInstructions.MaximumSize = new Size(800, 60);
            lblResearchInstructions.Font = font_7;
            lblResearchInstructions.Text = text;
            lblResearchEmpireLabel.Location = new Point(640, 75);
            lblResearchEmpireLabel.Font = font_6;
            lblResearchEmpireLabel.Text = TextResolver.GetText("Total Empire Research Potential") + ":";
            double annualResearchPotential = empire_5.AnnualResearchPotential;
            lblResearchEmpirePotential.Text = annualResearchPotential.ToString("#######0,K");
            lblResearchEmpirePotential.Font = font_7;
            lblResearchEmpirePotential.BackColor = Color.FromArgb(96, 255, 64, 128);
            lblResearchEmpirePotential.Location = new Point(825, 73);
            Color foreColor = Color.FromArgb(170, 170, 170);
            double num = 1.0;
            string arg = string.Empty;
            if (empire_5.GovernmentAttributes != null)
            {
                num = empire_5.GovernmentAttributes.ResearchSpeed;
                arg = empire_5.GovernmentAttributes.Name;
            }
            string text2 = (num - 1.0).ToString("+#0%;-#0%;+0%");
            if (num < 1.0)
            {
                foreColor = Color.Red;
            }
            else if (num > 1.0)
            {
                foreColor = Color.Green;
            }
            lblResearchEmpireGovernmentModifier.Text = text2;
            lblResearchEmpireGovernmentModifier.Font = font_7;
            lblResearchEmpireGovernmentModifier.ForeColor = foreColor;
            lblResearchEmpireGovernmentModifier.Location = new Point(640, 96);
            lblResearchEmpireGovernment.Text = string.Format(TextResolver.GetText("X% from Y government style"), "", arg);
            lblResearchEmpireGovernment.Font = font_6;
            lblResearchEmpireGovernment.Location = new Point(676, 98);
            if (empire_5.PirateEmpireBaseHabitat != null)
            {
                lblResearchEmpireGovernment.Visible = false;
                lblResearchEmpireGovernmentModifier.Visible = false;
            }
            else
            {
                lblResearchEmpireGovernment.Visible = true;
                lblResearchEmpireGovernmentModifier.Visible = true;
            }
            if (empire_5.ResearchBonus > 0.0 && empire_5.ResearchBonusRace != null)
            {
                lblResearchEmpireRaceBonusModifier.Text = empire_5.ResearchBonus.ToString("+#0%;-#0%;+0%");
                lblResearchEmpireRaceBonusModifier.Font = font_7;
                lblResearchEmpireRaceBonusModifier.Location = new Point(640, 119);
                lblResearchEmpireRaceBonusModifier.ForeColor = Color.Green;
                lblResearchEmpireRaceBonus.Text = string.Format(TextResolver.GetText("X% from Y race"), "", empire_5.ResearchBonusRace.Name);
                lblResearchEmpireRaceBonus.Font = font_6;
                lblResearchEmpireRaceBonus.Location = new Point(676, 121);
                lblResearchEmpireRaceBonus.Visible = true;
                lblResearchEmpireRaceBonusModifier.Visible = true;
            }
            else
            {
                lblResearchEmpireRaceBonus.Visible = false;
                lblResearchEmpireRaceBonusModifier.Visible = false;
            }
            lblResearchMaximumSize.Location = new Point(640, 150);
            lblResearchMaximumSize.Size = new Size(315, 50);
            lblResearchMaximumSize.MaximumSize = new Size(290, 50);
            lblResearchMaximumSize.Font = font_6;
            lblResearchMaximumSize.Text = method_305();
            lblResearchEmpireSpecialBonuses.Location = new Point(640, 195);
            lblResearchEmpireSpecialBonuses.MaximumSize = new Size(315, 255);
            lblResearchEmpireSpecialBonuses.Font = font_6;
            string text3 = empire_5.SummarizeSpecialResearchBonuses();
            lblResearchEmpireSpecialBonuses.Text = text3;
            lblResearchEmpireSpecialBonuses.BringToFront();
            lblResearchEmpireSpecialBonuses.Visible = true;
            lnkResearch.Location = new Point(845, 10);
            ctlResearchFacilities.BringToFront();
            ctlResearchFacilities.Size = new Size(610, 320);
            ctlResearchFacilities.Location = new Point(20, 60);
            ctlResearchFacilities.Grid.Columns["Empire"].Width = 25;
            ctlResearchFacilities.Grid.Columns["Picture"].Width = 50;
            ctlResearchFacilities.Grid.Columns["Name"].Width = 280;
            ctlResearchFacilities.Grid.Columns["Energy"].Width = 85;
            ctlResearchFacilities.Grid.Columns["HighTech"].Width = 85;
            ctlResearchFacilities.Grid.Columns["Weapons"].Width = 85;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            foreach (BuiltObject builtObject in empire_5.BuiltObjects)
            {
                if (builtObject.ResearchEnergy > 0 || builtObject.ResearchHighTech > 0 || builtObject.ResearchWeapons > 0)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            foreach (BuiltObject privateBuiltObject in empire_5.PrivateBuiltObjects)
            {
                if (privateBuiltObject.ResearchEnergy > 0 || privateBuiltObject.ResearchHighTech > 0 || privateBuiltObject.ResearchWeapons > 0)
                {
                    builtObjectList.Add(privateBuiltObject);
                }
            }
            ctlResearchFacilities.BindData(builtObjectList, builtObjectImageCache_0.GetImagesSmall());
            pnlResearchSummary.Size = new Size(570, 60);
            pnlResearchSummary.Location = new Point(50, 330);
            pnlResearchSummary.Ignite(_Game.Galaxy, empire_5);
            TwUstMvCeX.Size = new Size(size.Width - 44, size.Height - 192);
            TwUstMvCeX.Location = new Point(13, 115);
            ctlResearchFacilities.Location = new Point(10, 10);
            WdosRcovZt.Size = new Size(size.Width - 44, size.Height - 192);
            WdosRcovZt.Location = new Point(13, 115);
            WdosRcovZt.BackColor = Color.Black;
            WdosRcovZt.AutoScroll = true;
            WdosRcovZt.AutoScrollPosition = new Point(0, 0);
            WdosRcovZt.BringToFront();
            pnlResearchTree.Location = new Point(0, 0);
            pnlResearchTree.BringToFront();
            btnResearchTreeWeapons.Size = new Size(230, 40);
            btnResearchTreeWeapons.Location = new Point(40, 75);
            btnResearchTreeWeapons.SetCornerCurves(topLeft: true, topRight: true, bottomRight: false, bottomLeft: false);
            method_398(IndustryType.Weapon);
            btnResearchTreeEnergy.Size = new Size(230, 40);
            btnResearchTreeEnergy.Location = new Point(270, 75);
            btnResearchTreeEnergy.SetCornerCurves(topLeft: true, topRight: true, bottomRight: false, bottomLeft: false);
            method_398(IndustryType.Energy);
            btnResearchTreeHighTech.Size = new Size(230, 40);
            btnResearchTreeHighTech.Location = new Point(500, 75);
            btnResearchTreeHighTech.SetCornerCurves(topLeft: true, topRight: true, bottomRight: false, bottomLeft: false);
            method_398(IndustryType.HighTech);
            btnResearchFacilities.Visible = true;
            btnResearchFacilities.Size = new Size(230, 40);
            btnResearchFacilities.Location = new Point(730, 75);
            btnResearchFacilities.SetCornerCurves(topLeft: true, topRight: true, bottomRight: false, bottomLeft: false);
            btnResearchFacilities.Text = TextResolver.GetText("Research Stations");
            btnResearchFacilities.IntensifyColors = true;
            switch (industryType_0)
            {
                case IndustryType.Undefined:
                    TwUstMvCeX.BringToFront();
                    TwUstMvCeX.Visible = true;
                    WdosRcovZt.SendToBack();
                    WdosRcovZt.Visible = false;
                    btnResearchFacilities.Height = 43;
                    break;
                case IndustryType.Weapon:
                    TwUstMvCeX.SendToBack();
                    TwUstMvCeX.Visible = false;
                    WdosRcovZt.BringToFront();
                    WdosRcovZt.Visible = true;
                    btnResearchTreeWeapons.Height = 43;
                    break;
                case IndustryType.Energy:
                    TwUstMvCeX.SendToBack();
                    TwUstMvCeX.Visible = false;
                    WdosRcovZt.BringToFront();
                    WdosRcovZt.Visible = true;
                    btnResearchTreeEnergy.Height = 43;
                    break;
                case IndustryType.HighTech:
                    TwUstMvCeX.SendToBack();
                    TwUstMvCeX.Visible = false;
                    WdosRcovZt.BringToFront();
                    WdosRcovZt.Visible = true;
                    btnResearchTreeHighTech.Height = 43;
                    break;
            }
            method_396();
            int pictureRef = ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.Carrier, empire_5.DominantRace, empire_5.PirateEmpireBaseHabitat != null);
            Bitmap carrierImage = builtObjectImageCache_0.ObtainImageSmall(pictureRef);
            int pictureRef2 = ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.ResupplyShip, empire_5.DominantRace, empire_5.PirateEmpireBaseHabitat != null);
            Bitmap resupplyShipImage = builtObjectImageCache_0.ObtainImageSmall(pictureRef2);
            pnlResearchTree.BindData(_Game.Galaxy, empire_5, empire_5.Research, industryType_0, size.Width - 44, size.Height - 137, WdosRcovZt, researchNode_0, carrierImage, resupplyShipImage, cursor_0);
            pnlResearchTree.StartPainting();
            pnlResearch.Visible = true;
            pnlResearch.BringToFront();
            btnResearchTreeWeapons.Invalidate();
            btnResearchTreeEnergy.Invalidate();
            btnResearchTreeHighTech.Invalidate();
            btnResearchFacilities.Invalidate();
        }

        private List<Bitmap> method_396()
        {
            List<Bitmap> list = new List<Bitmap>();
            list.Add(habitatImageCache_0.ObtainImageSmall(GalaxyImages.HabitatImageOffsetContinental));
            list.Add(habitatImageCache_0.ObtainImageSmall(GalaxyImages.HabitatImageOffsetMarshySwamp));
            list.Add(habitatImageCache_0.ObtainImageSmall(GalaxyImages.HabitatImageOffsetOcean));
            list.Add(habitatImageCache_0.ObtainImageSmall(GalaxyImages.HabitatImageOffsetDesert));
            list.Add(habitatImageCache_0.ObtainImageSmall(GalaxyImages.HabitatImageOffsetIce));
            list.Add(habitatImageCache_0.ObtainImageSmall(GalaxyImages.HabitatImageOffsetVolcanic));
            return list;
        }

        private void method_397(object object_7, ResearchNode researchNode_0)
        {
            method_398(pnlResearchTree.Industry);
        }

        private void method_398(IndustryType industryType_0)
        {
            ResearchNode researchNode = null;
            GlassButton glassButton = null;
            string empty = string.Empty;
            Color backColor = Color.FromArgb(80, 10, 20);
            Color outerBorderColor = Color.FromArgb(32, 0, 16);
            Color shineColor = Color.FromArgb(208, 80, 112);
            Color glowColor = Color.FromArgb(255, 64, 96);
            switch (industryType_0)
            {
                case IndustryType.Weapon:
                    glassButton = btnResearchTreeWeapons;
                    empty = TextResolver.GetText("Weapons");
                    if (_Game.PlayerEmpire.Research.ResearchQueueWeapons != null && _Game.PlayerEmpire.Research.ResearchQueueWeapons.Count > 0)
                    {
                        researchNode = _Game.PlayerEmpire.Research.ResearchQueueWeapons[0];
                    }
                    backColor = Color.FromArgb(80, 10, 20);
                    outerBorderColor = Color.FromArgb(32, 0, 16);
                    shineColor = Color.FromArgb(208, 80, 112);
                    glowColor = Color.FromArgb(255, 64, 96);
                    break;
                case IndustryType.Energy:
                    glassButton = btnResearchTreeEnergy;
                    empty = TextResolver.GetText("Energy & Construction");
                    if (_Game.PlayerEmpire.Research.ResearchQueueEnergy != null && _Game.PlayerEmpire.Research.ResearchQueueEnergy.Count > 0)
                    {
                        researchNode = _Game.PlayerEmpire.Research.ResearchQueueEnergy[0];
                    }
                    backColor = Color.FromArgb(20, 10, 80);
                    outerBorderColor = Color.FromArgb(16, 0, 32);
                    shineColor = Color.FromArgb(112, 80, 208);
                    glowColor = Color.FromArgb(96, 64, 255);
                    break;
                case IndustryType.HighTech:
                    glassButton = btnResearchTreeHighTech;
                    empty = TextResolver.GetText("HighTech & Industrial");
                    if (_Game.PlayerEmpire.Research.ResearchQueueHighTech != null && _Game.PlayerEmpire.Research.ResearchQueueHighTech.Count > 0)
                    {
                        researchNode = _Game.PlayerEmpire.Research.ResearchQueueHighTech[0];
                    }
                    backColor = Color.FromArgb(10, 80, 20);
                    outerBorderColor = Color.FromArgb(0, 32, 16);
                    shineColor = Color.FromArgb(80, 208, 112);
                    glowColor = Color.FromArgb(64, 255, 96);
                    break;
            }
            if (researchNode == null && glassButton != null)
            {
                glassButton.Text = empty;
                glassButton.MinorText = "(" + TextResolver.GetText("No project") + ")";
            }
            else if (glassButton != null)
            {
                glassButton.Text = empty;
                string text = "(" + researchNode.Name + "  ";
                text = (glassButton.MinorText = text + (researchNode.Progress / researchNode.Cost).ToString("0%") + ")");
            }
            glassButton.BackColor = backColor;
            glassButton.OuterBorderColor = outerBorderColor;
            glassButton.ShineColor = shineColor;
            glassButton.GlowColor = glowColor;
            glassButton.Invalidate();
        }

        private void method_399()
        {
            if (pnlResearchTree.EditMode)
            {
                empire_0.Research.Update(empire_0.DominantRace);
                empire_0.ReviewResearchAbilities();
                empire_0.ReviewDesignsBuiltObjectsImprovedComponents();
                empire_0.ReviewColonizationTypes();
                empire_0.ReviewPopulationGrowthRates();
                int newSize = 0;
                empire_0.ReviewMaximumConstructionSize(out newSize);
                empire_0.ReviewCanBuildShipTypes();
                empire_0.ReviewTroopTypes();
                pnlResearchTree.EditMode = false;
            }
            pnlResearch.SendToBack();
            pnlResearch.Visible = false;
            pnlResearchTree.StopPainting();
            method_545();
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void btnResearchGotoFacility_Click(object sender, EventArgs e)
        {
            BuiltObject selectedBuiltObject = ctlResearchFacilities.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                method_157(selectedBuiltObject);
                method_4(1.0);
                method_399();
            }
        }

        private void method_400()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            vHfFsoqMev.Size = new Size(925, 785);
            vHfFsoqMev.Location = new Point((mainView.Width - vHfFsoqMev.Width) / 2, (mainView.Height - vHfFsoqMev.Height) / 2);
            vHfFsoqMev.DoLayout();
            tabEmpireComparisonGraphs.Size = new Size(890, 705);
            tabEmpireComparisonGraphs.Location = new Point(10, 10);
            tabEmpireComparisonGraphs.TabPages["tabPopulation"].Text = TextResolver.GetText("Population");
            tabEmpireComparisonGraphs.TabPages["tabTerritory"].Text = TextResolver.GetText("Territory");
            tabEmpireComparisonGraphs.TabPages["tabEconomy"].Text = TextResolver.GetText("Economy");
            tabEmpireComparisonGraphs.TabPages["tabStrategicValue"].Text = TextResolver.GetText("Strategic Value");
            tabEmpireComparisonGraphs.TabPages["tabMilitaryStrength"].Text = TextResolver.GetText("Military Strength");
            tabEmpireComparisonGraphs.TabPages["tabTopColonies"].Text = TextResolver.GetText("Top Colonies");
            tabEmpireComparisonGraphs.TabPages["tabVictoryConditions"].Text = TextResolver.GetText("Victory Conditions");
            tabEmpireComparisonGraphs.TabPages["tabAchievements"].Text = TextResolver.GetText("Achievements");
            pnlEmpireComparisonPopulation.Size = new Size(860, 660);
            pnlEmpireComparisonPopulation.Location = new Point(10, 10);
            pnlEmpireComparisonPopulation.Ignite(_Game, EmpireComparisonType.Population);
            pnlEmpireComparisonTerritory.Size = new Size(860, 660);
            pnlEmpireComparisonTerritory.Location = new Point(10, 10);
            pnlEmpireComparisonTerritory.Ignite(_Game, EmpireComparisonType.Territory);
            pnlEmpireComparisonEconomy.Size = new Size(860, 660);
            pnlEmpireComparisonEconomy.Location = new Point(10, 10);
            pnlEmpireComparisonEconomy.Ignite(_Game, EmpireComparisonType.Economy);
            pnlEmpireComparisonStrategicValue.Size = new Size(860, 660);
            pnlEmpireComparisonStrategicValue.Location = new Point(10, 10);
            pnlEmpireComparisonStrategicValue.Ignite(_Game, EmpireComparisonType.StrategicValue);
            pnlEmpireComparisonMilitaryStrength.Size = new Size(860, 660);
            pnlEmpireComparisonMilitaryStrength.Location = new Point(10, 10);
            pnlEmpireComparisonMilitaryStrength.Ignite(_Game, EmpireComparisonType.MilitaryStrength);
            pnlEmpireComparisonTopColonies.Size = new Size(860, 660);
            pnlEmpireComparisonTopColonies.Location = new Point(10, 10);
            pnlEmpireComparisonTopColonies.Ignite(_Game, habitatImageCache_0.GetImagesSmall());
            pnlGameRaceVictoryConditionsContainer.Location = new Point(10, 10);
            pnlGameRaceVictoryConditionsContainer.Size = new Size(860, 660);
            pnlGameRaceVictoryConditionsContainer.AutoScroll = true;
            pnlGameRaceVictoryConditionsContainer.SetAutoScrollMargin(0, 0);
            pnlGameRaceVictoryConditionsContainer.AutoScrollPosition = new Point(0, 0);
            pnlGameRaceVictoryConditions.Location = new Point(0, 0);
            pnlGameRaceVictoryConditions.Size = new Size(840, 0);
            pnlGameRaceVictoryConditions.MaximumSize = new Size(840, 1000);
            pnlGameRaceVictoryConditions.MinimumSize = new Size(840, 660);
            pnlGameRaceVictoryConditions.AutoSize = true;
            pnlGameRaceVictoryConditions.BindData(_Game, _Game.Galaxy, _Game.GlobalVictoryConditions);
            pnlGameVictoryConditions.Visible = false;
            pnlGameVictoryConditions.SendToBack();
            pnlGameSummary.Size = new Size(860, 660);
            pnlGameSummary.Location = new Point(10, 10);
            pnlGameSummary.BindData(gameSummaryList_0, _Game, raceImageCache_0, font_3, font_7, font_2, font_0);
            pnlGameSummary.OverlayTextLines.Clear();
            pnlGameSummary.Visible = true;
            vHfFsoqMev.Visible = true;
            vHfFsoqMev.BringToFront();
            tabEmpireComparisonGraphs.Focus();
        }

        private void method_401()
        {
            vHfFsoqMev.SendToBack();
            vHfFsoqMev.Visible = false;
            pnlGameSummary.ClearData();
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_402()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            method_421();
            pnlGameOptions.Size = new Size(700, 696);
            pnlGameOptions.Location = new Point((base.Width - pnlGameOptions.Width) / 2, (base.Height - pnlGameOptions.Height) / 2);
            pnlGameOptions.DoLayout();
            lblOptionsMainViewScrollSpeed.Font = font_4;
            lblOptionsMainViewZoomSpeed.Font = font_4;
            lblOptionsMainViewStarFieldSize.Font = font_4;
            lblOptionsMainViewStarFieldSize.BringToFront();
            lblOptionsMainViewScrollSpeed.Location = new Point(lblOptionsMainViewScrollSpeed.Location.X, 24);
            lblOptionsMainViewZoomSpeed.Location = new Point(lblOptionsMainViewZoomSpeed.Location.X, 49);
            lblOptionsMainViewStarFieldSize.Location = new Point(lblOptionsMainViewStarFieldSize.Location.X, 74);
            sldOptionsMainViewScrollSpeed.Location = new Point(130, 26);
            sldOptionsMainViewZoomSpeed.Location = new Point(130, 51);
            sldOptionsMainViewStarFieldSize.Location = new Point(130, 76);
            sldOptionsMainViewGuiScale.Location = new Point(130, 76);
            sldOptionsMainViewGuiScale.Size = new Size(515, 16);
            sldOptionsMainViewScrollSpeed.Size = new Size(515, 16);
            sldOptionsMainViewZoomSpeed.Minimum = 1;
            sldOptionsMainViewZoomSpeed.Size = new Size(515, 16);
            sldOptionsMainViewStarFieldSize.Size = new Size(515, 16);
            btnGameOptionsAdvancedDisplaySettings.Location = new Point(395, 103);
            btnGameOptionsAdvancedDisplaySettings.Size = new Size(250, 26);
            btnHotKeys.Location = new Point(15, 103);
            btnHotKeys.Size = new Size(250, 26);
            grpOptionsControl.Font = font_2;
            grpOptionsDisplaySettings.Font = font_2;
            grpOptionsPopupMessages.Font = font_2;
            grpOptionsScrollingMessages.Font = font_2;
            grpOptionsVolume.Font = font_2;
            grpOptionsControl.Size = new Size(659, 291);
            grpOptionsDisplaySettings.Size = new Size(659, 134);
            grpOptionsVolume.Size = new Size(659, 74);
            grpOptionsScrollingMessages.Size = new Size(285, 230);
            grpOptionsPopupMessages.Size = new Size(285, 230);
            grpOptionsScrollingMessages.Location = new Point(12, 492);
            grpOptionsPopupMessages.Location = new Point(307, 492);
            grpOptionsDisplaySettings.Location = new Point(12, 7);
            grpOptionsVolume.Location = new Point(12, 147);
            grpOptionsControl.Location = new Point(12, 288);
            lblOptionsMusicVolume.Font = font_4;
            lblOptionsSoundEffectsVolume.Font = font_4;
            lblOptionsMusicVolume.Location = new Point(17, 22);
            lblOptionsSoundEffectsVolume.Location = new Point(17, 47);
            sldOptionsMusicVolume.Location = new Point(81, 24);
            sldOptionsMusicVolume.Size = new Size(568, 16);
            sldOptionsSoundEffectsVolume.Location = new Point(81, 49);
            sldOptionsSoundEffectsVolume.Size = new Size(568, 16);
            lblOptionsMouseScrollMode.Font = font_4;
            chkOptionsLoadedGamesPaused.Font = font_4;
            cmbOptionsMouseScrollWheelBehaviour.Font = font_4;
            chkOptionsAutoSave.Font = font_4;
            lblOptionsMouseScrollMode.Location = new Point(195, 246);
            cmbOptionsMouseScrollWheelBehaviour.Size = new Size(250, 21);
            cmbOptionsMouseScrollWheelBehaviour.Location = new Point(341, 242);
            grpOptionsAutoSave.Location = new Point(12, 228);
            grpOptionsAutoSave.Font = font_2;
            grpOptionsAutoSave.Size = new Size(180, 54);
            chkOptionsAutoSave.Text = string.Format(TextResolver.GetText("Every X minutes"), "              ");
            chkOptionsAutoSave.Location = new Point(7, 19);
            numOptionsAutoSaveMinutes.Location = new Point(72, 20);
            chkOptionsLoadedGamesPaused.Location = new Point(352, 228);
            chkOptionsLoadedGamesPaused.CheckAlign = ContentAlignment.MiddleRight;
            chkOptionsLoadedGamesPaused.TextAlign = ContentAlignment.MiddleRight;
            chkOptionsLoadedGamesPaused.Location = new Point(pnlGameOptions.Width - (chkOptionsLoadedGamesPaused.Width + 30), 228);
            lblOptionsMouseScrollMode.Location = new Point(223, 258);
            cmbOptionsMouseScrollWheelBehaviour.Size = new Size(240, 21);
            cmbOptionsMouseScrollWheelBehaviour.Location = new Point(chkOptionsLoadedGamesPaused.Location.X + chkOptionsLoadedGamesPaused.Width - cmbOptionsMouseScrollWheelBehaviour.Width, 254);
            cmbOptionsMouseScrollWheelBehaviour.BringToFront();
            lblOptionsAutomationMode.Font = font_4;
            lblOptionsControlAgentMissions.Font = font_4;
            lblOptionsControlAttacks.Font = font_4;
            lblOptionsControlColonization.Font = font_4;
            lblOptionsControlColonyFacilities.Font = font_4;
            lblOptionsControlConstruction.Font = font_4;
            lblOptionsControlDiplomacyGifts.Font = font_4;
            lblOptionsControlDiplomacyOffense.Font = font_4;
            lblOptionsControlDiplomacyTreaties.Font = font_4;
            lblOptionsControlOfferPirateMissions.Font = font_4;
            cmbOptionsControlAgentMissions.Font = font_4;
            fYpVlWkAfp.Font = font_4;
            cmbOptionsControlColonization.Font = font_4;
            cmbOptionsControlColonyFacilities.Font = font_4;
            cmbOptionsControlConstruction.Font = font_4;
            cmbOptionsControlDiplomacyGifts.Font = font_4;
            cmbOptionsControlDiplomacyOffense.Font = font_4;
            cmbOptionsControlDiplomacyTreaties.Font = font_4;
            cmbOptionsControlOfferPirateMissions.Font = font_4;
            chkOptionsControlCharacterLocations.Font = font_4;
            chkOptionsControlColonyTaxRates.Font = font_4;
            chkOptionsControlDesigns.Font = font_4;
            chkOptionsControlFleets.Font = font_4;
            chkOptionsControlPopulationPolicy.Font = font_4;
            chkOptionsControlResearch.Font = font_4;
            chkOptionsControlTroops.Font = font_4;
            cmbOptionsAutomationMode.Font = font_4;
            pnlOptionsAutomationMode.Size = new Size(217, 41);
            pnlOptionsAutomationMode.Location = new Point(10, 21);
            pnlOptionsAutomationMode.BackColor = Color.FromArgb(128, 192, 0, 128);
            lblOptionsAutomationMode.Font = font_7;
            lblOptionsAutomationMode.Location = new Point(5, 9);
            cmbOptionsAutomationMode.Size = new Size(162, 24);
            cmbOptionsAutomationMode.Location = new Point(58, 25);
            lblOptionsControlColonization.Location = new Point(260, 23);
            lblOptionsControlConstruction.Location = new Point(259, 52);
            lblOptionsControlAgentMissions.Location = new Point(247, 81);
            lblOptionsControlAttacks.Location = new Point(202, 110);
            lblOptionsControlDiplomacyGifts.Location = new Point(207, 139);
            lblOptionsControlDiplomacyTreaties.Location = new Point(279, 168);
            lblOptionsControlDiplomacyOffense.Location = new Point(210, 197);
            lblOptionsControlColonyFacilities.Location = new Point(215, 226);
            lblOptionsControlOfferPirateMissions.Location = new Point(215, 255);
            Size size_ = new Size(236, 21);
            method_404(lblOptionsControlColonization, 191, size_);
            method_404(lblOptionsControlConstruction, 191, size_);
            method_404(lblOptionsControlAgentMissions, 186, new Size(241, 21));
            method_404(lblOptionsControlAttacks, 191, size_);
            method_404(lblOptionsControlDiplomacyGifts, 191, size_);
            method_404(lblOptionsControlDiplomacyTreaties, 191, size_);
            method_404(lblOptionsControlDiplomacyOffense, 191, size_);
            method_404(lblOptionsControlColonyFacilities, 191, size_);
            method_404(lblOptionsControlOfferPirateMissions, 191, size_);
            cmbOptionsControlColonization.Location = new Point(429, 19);
            cmbOptionsControlConstruction.Location = new Point(429, 48);
            cmbOptionsControlAgentMissions.Location = new Point(429, 77);
            fYpVlWkAfp.Location = new Point(429, 106);
            cmbOptionsControlDiplomacyGifts.Location = new Point(429, 135);
            cmbOptionsControlDiplomacyTreaties.Location = new Point(429, 164);
            cmbOptionsControlDiplomacyOffense.Location = new Point(429, 193);
            cmbOptionsControlColonyFacilities.Location = new Point(429, 222);
            cmbOptionsControlOfferPirateMissions.Location = new Point(429, 251);
            cmbOptionsControlAgentMissions.Size = new Size(220, 24);
            fYpVlWkAfp.Size = new Size(220, 24);
            cmbOptionsControlColonization.Size = new Size(220, 24);
            cmbOptionsControlConstruction.Size = new Size(220, 24);
            cmbOptionsControlDiplomacyGifts.Size = new Size(220, 24);
            cmbOptionsControlDiplomacyOffense.Size = new Size(220, 24);
            cmbOptionsControlDiplomacyTreaties.Size = new Size(220, 24);
            cmbOptionsControlColonyFacilities.Size = new Size(220, 24);
            cmbOptionsControlOfferPirateMissions.Size = new Size(220, 24);
            lblOptionsControlColonization.SendToBack();
            lblOptionsControlConstruction.SendToBack();
            lblOptionsControlAgentMissions.SendToBack();
            lblOptionsControlAttacks.SendToBack();
            lblOptionsControlDiplomacyGifts.SendToBack();
            lblOptionsControlDiplomacyTreaties.SendToBack();
            lblOptionsControlDiplomacyOffense.SendToBack();
            lblOptionsControlColonyFacilities.SendToBack();
            lblOptionsControlOfferPirateMissions.SendToBack();
            chkOptionsControlColonyTaxRates.Location = new Point(9, 73);
            chkOptionsControlColonyTaxRates.BringToFront();
            chkOptionsControlPopulationPolicy.Location = new Point(9, 96);
            chkOptionsControlPopulationPolicy.BringToFront();
            chkOptionsControlDesigns.Location = new Point(9, 119);
            chkOptionsControlDesigns.BringToFront();
            chkOptionsControlTroops.Location = new Point(9, 142);
            chkOptionsControlTroops.BringToFront();
            chkOptionsControlFleets.Location = new Point(9, 165);
            chkOptionsControlFleets.BringToFront();
            chkOptionsControlResearch.Location = new Point(9, 188);
            chkOptionsControlResearch.BringToFront();
            chkOptionsControlCharacterLocations.Location = new Point(9, 211);
            chkOptionsControlCharacterLocations.BringToFront();
            btnGameOptionsResetAutomationMessages.Text = TextResolver.GetText("Reset Warnings");
            btnGameOptionsResetAutomationMessages.Size = new Size(73, 40);
            btnGameOptionsResetAutomationMessages.Location = new Point(228, 20);
            btnGameOptionsEmpireSettings.Text = TextResolver.GetText("Empire Settings");
            btnGameOptionsEmpireSettings.Size = new Size(179, 35);
            btnGameOptionsEmpireSettings.Location = new Point(7, 250);
            btnGameOptionsShowMessages.Size = new Size(660, 35);
            btnGameOptionsShowMessages.Location = new Point(12, 589);
            pnlGameOptions.Visible = true;
            pnlGameOptions.BringToFront();
        }

        private void method_403(LinkLabel linkLabel_0, int int_64, Size size_1)
        {
            linkLabel_0.AutoSize = false;
            linkLabel_0.TextAlign = ContentAlignment.MiddleRight;
            linkLabel_0.Size = size_1;
            linkLabel_0.MaximumSize = size_1;
            linkLabel_0.Location = new Point(int_64, linkLabel_0.Location.Y);
        }

        private void method_404(Label label_0, int int_64, Size size_1)
        {
            label_0.AutoSize = false;
            label_0.TextAlign = ContentAlignment.MiddleRight;
            label_0.Size = size_1;
            label_0.MaximumSize = size_1;
            label_0.Location = new Point(int_64, label_0.Location.Y);
        }

        private void cmbOptionsAutomationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = cmbOptionsAutomationMode.Text;
            GameOptions gameOptions = null;
            if (text == TextResolver.GetText("Default"))
            {
                gameOptions = method_408();
            }
            else if (text == TextResolver.GetText("Expansion"))
            {
                gameOptions = method_409();
            }
            else if (text == TextResolver.GetText("War and Combat"))
            {
                gameOptions = LrAjSoHuFL();
            }
            else if (text == TextResolver.GetText("Diplomacy"))
            {
                gameOptions = method_410();
            }
            else if (text == TextResolver.GetText("Spy Master"))
            {
                gameOptions = method_411();
            }
            else if (text == TextResolver.GetText("Expert") + " (" + TextResolver.GetText("none") + ")")
            {
                gameOptions = method_407();
            }
            else if (text == TextResolver.GetText("Rule in Absence") + " (" + TextResolver.GetText("full") + ")")
            {
                gameOptions = method_406();
            }
            if (gameOptions != null)
            {
                method_405(gameOptions);
            }
        }

        private void UhvjHxwqlt()
        {
            GameOptions other = method_406();
            GameOptions other2 = LrAjSoHuFL();
            GameOptions other3 = method_408();
            GameOptions other4 = method_410();
            GameOptions other5 = method_409();
            GameOptions other6 = method_407();
            GameOptions other7 = method_411();
            GameOptions gameOptions = method_412();
            int selectedIndex = 0;
            if (gameOptions.CompareAutomationEquality(other3))
            {
                selectedIndex = 1;
            }
            else if (gameOptions.CompareAutomationEquality(other6))
            {
                selectedIndex = 2;
            }
            else if (gameOptions.CompareAutomationEquality(other))
            {
                selectedIndex = 3;
            }
            else if (gameOptions.CompareAutomationEquality(other5))
            {
                selectedIndex = 4;
            }
            else if (gameOptions.CompareAutomationEquality(other2))
            {
                selectedIndex = 5;
            }
            else if (gameOptions.CompareAutomationEquality(other4))
            {
                selectedIndex = 6;
            }
            else if (gameOptions.CompareAutomationEquality(other7))
            {
                selectedIndex = 7;
            }
            cmbOptionsAutomationMode.SelectedIndexChanged -= cmbOptionsAutomationMode_SelectedIndexChanged;
            cmbOptionsAutomationMode.SelectedIndex = selectedIndex;
            cmbOptionsAutomationMode.SelectedIndexChanged += cmbOptionsAutomationMode_SelectedIndexChanged;
        }

        private void chkOptionsControlColonyTaxRates_CheckedChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void chkOptionsControlDesigns_CheckedChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void chkOptionsControlCharacterLocations_CheckedChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void chkOptionsControlTroops_CheckedChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void chkOptionsControlFleets_CheckedChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void cmbOptionsControlColonization_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void cmbOptionsControlConstruction_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void cmbOptionsControlOfferPirateMissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void cmbOptionsControlAgentMissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void cmbOptionsControlColonyFacilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void fYpVlWkAfp_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void cmbOptionsControlDiplomacyGifts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void cmbOptionsControlDiplomacyTreaties_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void cmbOptionsControlDiplomacyOffense_SelectedIndexChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void chkOptionsControlPopulationPolicy_CheckedChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void chkOptionsControlResearch_CheckedChanged(object sender, EventArgs e)
        {
            UhvjHxwqlt();
        }

        private void method_405(GameOptions gameOptions_1)
        {
            fYpVlWkAfp.SelectedIndexChanged -= fYpVlWkAfp_SelectedIndexChanged;
            cmbOptionsControlColonization.SelectedIndexChanged -= cmbOptionsControlColonization_SelectedIndexChanged;
            chkOptionsControlColonyTaxRates.CheckedChanged -= chkOptionsControlColonyTaxRates_CheckedChanged;
            cmbOptionsControlConstruction.SelectedIndexChanged -= cmbOptionsControlConstruction_SelectedIndexChanged;
            chkOptionsControlDesigns.CheckedChanged -= chkOptionsControlDesigns_CheckedChanged;
            cmbOptionsControlDiplomacyGifts.SelectedIndexChanged -= cmbOptionsControlDiplomacyGifts_SelectedIndexChanged;
            cmbOptionsControlDiplomacyOffense.SelectedIndexChanged -= cmbOptionsControlDiplomacyOffense_SelectedIndexChanged;
            cmbOptionsControlDiplomacyTreaties.SelectedIndexChanged -= cmbOptionsControlDiplomacyTreaties_SelectedIndexChanged;
            chkOptionsControlFleets.CheckedChanged -= chkOptionsControlFleets_CheckedChanged;
            chkOptionsControlTroops.CheckedChanged -= chkOptionsControlTroops_CheckedChanged;
            cmbOptionsControlAgentMissions.SelectedIndexChanged -= cmbOptionsControlAgentMissions_SelectedIndexChanged;
            chkOptionsControlResearch.CheckedChanged -= chkOptionsControlResearch_CheckedChanged;
            cmbOptionsControlColonyFacilities.SelectedIndexChanged -= cmbOptionsControlColonyFacilities_SelectedIndexChanged;
            chkOptionsControlPopulationPolicy.CheckedChanged -= chkOptionsControlPopulationPolicy_CheckedChanged;
            chkOptionsControlCharacterLocations.CheckedChanged -= chkOptionsControlCharacterLocations_CheckedChanged;
            cmbOptionsControlOfferPirateMissions.SelectedIndexChanged -= cmbOptionsControlOfferPirateMissions_SelectedIndexChanged;
            fYpVlWkAfp.SelectedIndex = method_420(gameOptions_1.ControlAttacksOnEnemiesDefault);
            cmbOptionsControlColonization.SelectedIndex = method_420(gameOptions_1.ControlColonizationDefault);
            chkOptionsControlColonyTaxRates.Checked = gameOptions_1.ControlColonyTaxRatesDefault;
            cmbOptionsControlConstruction.SelectedIndex = method_420(gameOptions_1.ControlShipBuildingDefault);
            chkOptionsControlDesigns.Checked = gameOptions_1.ControlShipDesignDefault;
            cmbOptionsControlDiplomacyGifts.SelectedIndex = method_420(gameOptions_1.ControlDiplomaticGiftsDefault);
            cmbOptionsControlDiplomacyOffense.SelectedIndex = method_420(gameOptions_1.ControlWarTradeSanctionsDefault);
            cmbOptionsControlDiplomacyTreaties.SelectedIndex = method_420(gameOptions_1.ControlTreatyNegotiationDefault);
            chkOptionsControlFleets.Checked = gameOptions_1.ControlFleetFormationDefault;
            chkOptionsControlTroops.Checked = gameOptions_1.ControlTroopRecruitmentDefault;
            chkOptionsControlCharacterLocations.Checked = gameOptions_1.ControlCharacterLocationsDefault;
            cmbOptionsControlAgentMissions.SelectedIndex = method_420(gameOptions_1.ControlAgentAssignmentDefault);
            chkOptionsControlResearch.Checked = gameOptions_1.ControlResearchDefault;
            cmbOptionsControlColonyFacilities.SelectedIndex = method_420(gameOptions_1.ControlColonyFacilitiesDefault);
            chkOptionsControlPopulationPolicy.Checked = gameOptions_1.ControlPopulationPolicyDefault;
            cmbOptionsControlOfferPirateMissions.SelectedIndex = method_420(gameOptions_1.ControlOfferPirateMissionsDefault);
            fYpVlWkAfp.SelectedIndexChanged += fYpVlWkAfp_SelectedIndexChanged;
            cmbOptionsControlColonization.SelectedIndexChanged += cmbOptionsControlColonization_SelectedIndexChanged;
            chkOptionsControlColonyTaxRates.CheckedChanged += chkOptionsControlColonyTaxRates_CheckedChanged;
            cmbOptionsControlConstruction.SelectedIndexChanged += cmbOptionsControlConstruction_SelectedIndexChanged;
            chkOptionsControlDesigns.CheckedChanged += chkOptionsControlDesigns_CheckedChanged;
            cmbOptionsControlDiplomacyGifts.SelectedIndexChanged += cmbOptionsControlDiplomacyGifts_SelectedIndexChanged;
            cmbOptionsControlDiplomacyOffense.SelectedIndexChanged += cmbOptionsControlDiplomacyOffense_SelectedIndexChanged;
            cmbOptionsControlDiplomacyTreaties.SelectedIndexChanged += cmbOptionsControlDiplomacyTreaties_SelectedIndexChanged;
            chkOptionsControlFleets.CheckedChanged += chkOptionsControlFleets_CheckedChanged;
            chkOptionsControlTroops.CheckedChanged += chkOptionsControlTroops_CheckedChanged;
            cmbOptionsControlAgentMissions.SelectedIndexChanged += cmbOptionsControlAgentMissions_SelectedIndexChanged;
            chkOptionsControlResearch.CheckedChanged += chkOptionsControlResearch_CheckedChanged;
            cmbOptionsControlColonyFacilities.SelectedIndexChanged += cmbOptionsControlColonyFacilities_SelectedIndexChanged;
            chkOptionsControlPopulationPolicy.CheckedChanged += chkOptionsControlPopulationPolicy_CheckedChanged;
            chkOptionsControlCharacterLocations.CheckedChanged += chkOptionsControlCharacterLocations_CheckedChanged;
            cmbOptionsControlOfferPirateMissions.SelectedIndexChanged += cmbOptionsControlOfferPirateMissions_SelectedIndexChanged;
        }

        private GameOptions method_406()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAttacksOnEnemiesDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlColonizationDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlColonyTaxRatesDefault = true;
            gameOptions.ControlShipBuildingDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlShipDesignDefault = true;
            gameOptions.ControlDiplomaticGiftsDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlWarTradeSanctionsDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlTreatyNegotiationDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlFleetFormationDefault = true;
            gameOptions.ControlTroopRecruitmentDefault = true;
            gameOptions.ControlAgentAssignmentDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlResearchDefault = true;
            gameOptions.ControlColonyFacilitiesDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlPopulationPolicyDefault = true;
            gameOptions.ControlCharacterLocationsDefault = true;
            gameOptions.ControlOfferPirateMissionsDefault = AutomationLevel.FullyAutomated;
            return gameOptions;
        }

        private GameOptions method_407()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAttacksOnEnemiesDefault = AutomationLevel.Manual;
            gameOptions.ControlColonizationDefault = AutomationLevel.Manual;
            gameOptions.ControlColonyTaxRatesDefault = false;
            gameOptions.ControlShipBuildingDefault = AutomationLevel.Manual;
            gameOptions.ControlShipDesignDefault = false;
            gameOptions.ControlDiplomaticGiftsDefault = AutomationLevel.Manual;
            gameOptions.ControlWarTradeSanctionsDefault = AutomationLevel.Manual;
            gameOptions.ControlTreatyNegotiationDefault = AutomationLevel.Manual;
            gameOptions.ControlFleetFormationDefault = false;
            gameOptions.ControlTroopRecruitmentDefault = false;
            gameOptions.ControlAgentAssignmentDefault = AutomationLevel.Manual;
            gameOptions.ControlResearchDefault = false;
            gameOptions.ControlColonyFacilitiesDefault = AutomationLevel.Manual;
            gameOptions.ControlPopulationPolicyDefault = false;
            gameOptions.ControlCharacterLocationsDefault = false;
            gameOptions.ControlOfferPirateMissionsDefault = AutomationLevel.Manual;
            return gameOptions;
        }

        private GameOptions method_408()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAgentAssignmentDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlAttacksOnEnemiesDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlColonizationDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlColonyTaxRatesDefault = true;
            gameOptions.ControlDiplomaticGiftsDefault = AutomationLevel.Manual;
            gameOptions.ControlFleetFormationDefault = true;
            gameOptions.ControlShipBuildingDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlShipDesignDefault = true;
            gameOptions.ControlTreatyNegotiationDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlTroopRecruitmentDefault = true;
            gameOptions.ControlWarTradeSanctionsDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlResearchDefault = true;
            gameOptions.ControlColonyFacilitiesDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlPopulationPolicyDefault = false;
            gameOptions.ControlCharacterLocationsDefault = true;
            gameOptions.ControlOfferPirateMissionsDefault = AutomationLevel.SemiAutomated;
            return gameOptions;
        }

        private GameOptions method_409()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAttacksOnEnemiesDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlColonizationDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlColonyTaxRatesDefault = true;
            gameOptions.ControlShipBuildingDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlShipDesignDefault = true;
            gameOptions.ControlDiplomaticGiftsDefault = AutomationLevel.Manual;
            gameOptions.ControlWarTradeSanctionsDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlTreatyNegotiationDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlFleetFormationDefault = true;
            gameOptions.ControlTroopRecruitmentDefault = true;
            gameOptions.ControlAgentAssignmentDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlResearchDefault = true;
            gameOptions.ControlColonyFacilitiesDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlPopulationPolicyDefault = false;
            gameOptions.ControlCharacterLocationsDefault = true;
            gameOptions.ControlOfferPirateMissionsDefault = AutomationLevel.SemiAutomated;
            return gameOptions;
        }

        private GameOptions LrAjSoHuFL()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAttacksOnEnemiesDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlColonizationDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlColonyTaxRatesDefault = true;
            gameOptions.ControlShipBuildingDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlShipDesignDefault = true;
            gameOptions.ControlDiplomaticGiftsDefault = AutomationLevel.Manual;
            gameOptions.ControlWarTradeSanctionsDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlTreatyNegotiationDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlFleetFormationDefault = true;
            gameOptions.ControlTroopRecruitmentDefault = true;
            gameOptions.ControlAgentAssignmentDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlResearchDefault = true;
            gameOptions.ControlColonyFacilitiesDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlPopulationPolicyDefault = true;
            gameOptions.ControlCharacterLocationsDefault = true;
            gameOptions.ControlOfferPirateMissionsDefault = AutomationLevel.SemiAutomated;
            return gameOptions;
        }

        private GameOptions method_410()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAttacksOnEnemiesDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlColonizationDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlColonyTaxRatesDefault = true;
            gameOptions.ControlShipBuildingDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlShipDesignDefault = true;
            gameOptions.ControlDiplomaticGiftsDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlWarTradeSanctionsDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlTreatyNegotiationDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlFleetFormationDefault = true;
            gameOptions.ControlTroopRecruitmentDefault = true;
            gameOptions.ControlAgentAssignmentDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlResearchDefault = true;
            gameOptions.ControlColonyFacilitiesDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlPopulationPolicyDefault = false;
            gameOptions.ControlCharacterLocationsDefault = true;
            gameOptions.ControlOfferPirateMissionsDefault = AutomationLevel.SemiAutomated;
            return gameOptions;
        }

        private GameOptions method_411()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAttacksOnEnemiesDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlColonizationDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlColonyTaxRatesDefault = true;
            gameOptions.ControlShipBuildingDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlShipDesignDefault = true;
            gameOptions.ControlDiplomaticGiftsDefault = AutomationLevel.Manual;
            gameOptions.ControlWarTradeSanctionsDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlTreatyNegotiationDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlFleetFormationDefault = true;
            gameOptions.ControlTroopRecruitmentDefault = true;
            gameOptions.ControlAgentAssignmentDefault = AutomationLevel.SemiAutomated;
            gameOptions.ControlResearchDefault = true;
            gameOptions.ControlColonyFacilitiesDefault = AutomationLevel.FullyAutomated;
            gameOptions.ControlPopulationPolicyDefault = true;
            gameOptions.ControlCharacterLocationsDefault = true;
            gameOptions.ControlOfferPirateMissionsDefault = AutomationLevel.SemiAutomated;
            return gameOptions;
        }

        private GameOptions method_412()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAttacksOnEnemiesDefault = method_419(fYpVlWkAfp.SelectedIndex);
            gameOptions.ControlColonizationDefault = method_419(cmbOptionsControlColonization.SelectedIndex);
            gameOptions.ControlColonyTaxRatesDefault = chkOptionsControlColonyTaxRates.Checked;
            gameOptions.ControlShipBuildingDefault = method_419(cmbOptionsControlConstruction.SelectedIndex);
            gameOptions.ControlShipDesignDefault = chkOptionsControlDesigns.Checked;
            gameOptions.ControlDiplomaticGiftsDefault = method_419(cmbOptionsControlDiplomacyGifts.SelectedIndex);
            gameOptions.ControlWarTradeSanctionsDefault = method_419(cmbOptionsControlDiplomacyOffense.SelectedIndex);
            gameOptions.ControlTreatyNegotiationDefault = method_419(cmbOptionsControlDiplomacyTreaties.SelectedIndex);
            gameOptions.ControlFleetFormationDefault = chkOptionsControlFleets.Checked;
            gameOptions.ControlTroopRecruitmentDefault = chkOptionsControlTroops.Checked;
            gameOptions.ControlAgentAssignmentDefault = method_419(cmbOptionsControlAgentMissions.SelectedIndex);
            gameOptions.ControlResearchDefault = chkOptionsControlResearch.Checked;
            gameOptions.ControlColonyFacilitiesDefault = method_419(cmbOptionsControlColonyFacilities.SelectedIndex);
            gameOptions.ControlPopulationPolicyDefault = chkOptionsControlPopulationPolicy.Checked;
            gameOptions.ControlCharacterLocationsDefault = chkOptionsControlCharacterLocations.Checked;
            gameOptions.ControlOfferPirateMissionsDefault = method_419(cmbOptionsControlOfferPirateMissions.SelectedIndex);
            return gameOptions;
        }

        private void method_413()
        {
            method_418();
            if (pnlGameOptionsAdvancedDisplaySettings.Visible)
            {
                method_569();
            }
            if (pnlGameOptionsEmpireSettings.Visible)
            {
                method_565();
            }
            if (pnlGameOptionsMessages.Visible)
            {
                method_567();
            }
            pnlGameOptions.SendToBack();
            pnlGameOptions.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private int method_414()
        {
            return cmbOptionsMouseScrollWheelBehaviour.SelectedIndex switch
            {
                -1 => 2,
                0 => 0,
                1 => 1,
                2 => 2,
                _ => 0,
            };
        }

        private void method_415(int int_64)
        {
            switch (int_64)
            {
                default:
                    cmbOptionsMouseScrollWheelBehaviour.SelectedIndex = 2;
                    break;
                case 0:
                    cmbOptionsMouseScrollWheelBehaviour.SelectedIndex = 0;
                    break;
                case 1:
                    cmbOptionsMouseScrollWheelBehaviour.SelectedIndex = 1;
                    break;
                case 2:
                    cmbOptionsMouseScrollWheelBehaviour.SelectedIndex = 2;
                    break;
            }
        }

        private void method_416()
        {
            _Game.DisplayMessageUnderAttackCivilianShips = chkOptionsScrollingMessageUnderAttackCivilianShips.Checked;
            _Game.DisplayMessageUnderAttackCivilianBases = chkOptionsScrollingMessageUnderAttackCivilianBases.Checked;
            _Game.DisplayMessageUnderAttackExplorationShips = chkOptionsScrollingMessageUnderAttackExplorationShips.Checked;
            _Game.DisplayMessageUnderAttackColonyConstructionShips = chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Checked;
            _Game.DisplayMessageUnderAttackMilitaryShips = chkOptionsScrollingMessageUnderAttackMilitaryShips.Checked;
            _Game.DisplayMessageUnderAttackOtherStateBases = chkOptionsScrollingMessageUnderAttackOtherStateBases.Checked;
            _Game.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases = chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Checked;
            _Game.DisplayMessageBuiltObjectBuilt = chkOptionsScrollingMessageNewShipBuilt.Checked;
            _Game.DisplayMessageColonyInvaded = chkOptionsScrollingMessageColonyGainLoss.Checked;
            _Game.DisplayMessageDiplomacyEmpireMetDestroyed = chkOptionsScrollingMessageEmpireMetDestroyed.Checked;
            _Game.DisplayMessageDiplomacyGift = chkOptionsScrollingMessageRequestWarning.Checked;
            _Game.DisplayMessageDiplomacyRequestWarning = chkOptionsScrollingMessageRequestWarning.Checked;
            _Game.DisplayMessageDiplomacyTreaty = chkOptionsScrollingMessageDiplomacyTreaties.Checked;
            _Game.DisplayMessageDiplomacyWarTradeSanctions = chkOptionsScrollingMessageWarTradeSanctions.Checked;
            _Game.DisplayMessageNewColony = chkOptionsScrollingMessageColonyGainLoss.Checked;
            _Game.DisplayMessageResearchNewComponent = chkOptionsScrollingMessageResearchBreakthrough.Checked;
            _Game.DisplayMessageIntelligenceMissions = chkOptionsScrollingMessageIntelligenceMissions.Checked;
            _Game.DisplayMessageExploration = chkOptionsScrollingMessageExploration.Checked;
            _Game.DisplayMessageShipMissionComplete = chkOptionsScrollingMessageShipMissionComplete.Checked;
            _Game.DisplayMessageShipNeedsRefuelling = chkOptionsScrollingMessageShipNeedsRefuelling.Checked;
            _Game.DisplayMessageConstructionResourceShortage = chkOptionsScrollingMessageConstructionResourceShortage.Checked;
            _Game.DisplayPopupUnderAttackCivilianShips = chkOptionsPopupMessageUnderAttackCivilianShips.Checked;
            _Game.DisplayPopupUnderAttackCivilianBases = gcbeGaamXG.Checked;
            _Game.DisplayPopupUnderAttackExplorationShips = chkOptionsPopupMessageUnderAttackExplorationShips.Checked;
            _Game.DisplayPopupUnderAttackColonyConstructionShips = chkOptionsPopupMessageUnderAttackColonyConstructionShips.Checked;
            _Game.DisplayPopupUnderAttackMilitaryShips = chkOptionsPopupMessageUnderAttackMilitaryShips.Checked;
            _Game.DisplayPopupUnderAttackOtherStateBases = ltFewaOdau.Checked;
            _Game.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases = chkOptionsPopupMessageUnderAttackColoniesSpaceports.Checked;
            _Game.DisplayPopupBuiltObjectBuilt = chkOptionsPopupMessageShipBuilt.Checked;
            _Game.DisplayPopupColonyInvaded = chkOptionsPopupMessageColonyGainLoss.Checked;
            _Game.DisplayPopupDiplomacyEmpireMetDestroyed = XwwejKaRdv.Checked;
            _Game.DisplayPopupDiplomacyGift = chkOptionsPopupMessageRequestWarning.Checked;
            _Game.DisplayPopupDiplomacyRequestWarning = chkOptionsPopupMessageRequestWarning.Checked;
            _Game.DisplayPopupDiplomacyTreaty = chkOptionsPopupMessageDiplomacyTreaties.Checked;
            _Game.DisplayPopupDiplomacyWarTradeSanctions = chkOptionsPopupMessageDiplomacyWarTradeSanctions.Checked;
            _Game.DisplayPopupNewColony = chkOptionsPopupMessageColonyGainLoss.Checked;
            _Game.DisplayPopupResearchNewComponent = chkOptionsPopupMessageResearchBreakthrough.Checked;
            _Game.DisplayPopupIntelligenceMissions = KpfeuWqjpj.Checked;
            _Game.DisplayPopupExploration = chkOptionsPopupMessageExploration.Checked;
            _Game.DisplayPopupShipMissionComplete = chkOptionsPopupMessageShipMissionComplete.Checked;
            _Game.DisplayPopupShipNeedsRefuelling = chkOptionsPopupMessageShipNeedsRefuelling.Checked;
            _Game.DisplayPopupConstructionResourceShortage = chkOptionsPopupMessageConstructionResourceShortage.Checked;
        }

        private void method_417()
        {
            chkOptionsScrollingMessageUnderAttackCivilianShips.Checked = _Game.DisplayMessageUnderAttackCivilianShips;
            chkOptionsScrollingMessageUnderAttackCivilianBases.Checked = _Game.DisplayMessageUnderAttackCivilianBases;
            chkOptionsScrollingMessageUnderAttackExplorationShips.Checked = _Game.DisplayMessageUnderAttackExplorationShips;
            chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Checked = _Game.DisplayMessageUnderAttackColonyConstructionShips;
            chkOptionsScrollingMessageUnderAttackMilitaryShips.Checked = _Game.DisplayMessageUnderAttackMilitaryShips;
            chkOptionsScrollingMessageUnderAttackOtherStateBases.Checked = _Game.DisplayMessageUnderAttackOtherStateBases;
            chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Checked = _Game.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases;
            chkOptionsScrollingMessageNewShipBuilt.Checked = _Game.DisplayMessageBuiltObjectBuilt;
            chkOptionsScrollingMessageColonyGainLoss.Checked = _Game.DisplayMessageColonyInvaded;
            chkOptionsScrollingMessageEmpireMetDestroyed.Checked = _Game.DisplayMessageDiplomacyEmpireMetDestroyed;
            chkOptionsScrollingMessageRequestWarning.Checked = _Game.DisplayMessageDiplomacyRequestWarning;
            chkOptionsScrollingMessageDiplomacyTreaties.Checked = _Game.DisplayMessageDiplomacyTreaty;
            chkOptionsScrollingMessageWarTradeSanctions.Checked = _Game.DisplayMessageDiplomacyWarTradeSanctions;
            chkOptionsScrollingMessageResearchBreakthrough.Checked = _Game.DisplayMessageResearchNewComponent;
            chkOptionsScrollingMessageIntelligenceMissions.Checked = _Game.DisplayMessageIntelligenceMissions;
            chkOptionsScrollingMessageExploration.Checked = _Game.DisplayMessageExploration;
            chkOptionsScrollingMessageShipMissionComplete.Checked = _Game.DisplayMessageShipMissionComplete;
            chkOptionsScrollingMessageShipNeedsRefuelling.Checked = _Game.DisplayMessageShipNeedsRefuelling;
            chkOptionsScrollingMessageConstructionResourceShortage.Checked = _Game.DisplayMessageConstructionResourceShortage;
            chkOptionsPopupMessageShipBuilt.Checked = _Game.DisplayPopupBuiltObjectBuilt;
            chkOptionsPopupMessageColonyGainLoss.Checked = _Game.DisplayPopupColonyInvaded;
            XwwejKaRdv.Checked = _Game.DisplayPopupDiplomacyEmpireMetDestroyed;
            chkOptionsPopupMessageRequestWarning.Checked = _Game.DisplayPopupDiplomacyRequestWarning;
            chkOptionsPopupMessageDiplomacyTreaties.Checked = _Game.DisplayPopupDiplomacyTreaty;
            chkOptionsPopupMessageDiplomacyWarTradeSanctions.Checked = _Game.DisplayPopupDiplomacyWarTradeSanctions;
            chkOptionsPopupMessageResearchBreakthrough.Checked = _Game.DisplayPopupResearchNewComponent;
            KpfeuWqjpj.Checked = _Game.DisplayPopupIntelligenceMissions;
            chkOptionsPopupMessageExploration.Checked = _Game.DisplayPopupExploration;
            chkOptionsPopupMessageShipMissionComplete.Checked = _Game.DisplayPopupShipMissionComplete;
            chkOptionsPopupMessageShipNeedsRefuelling.Checked = _Game.DisplayPopupShipNeedsRefuelling;
            chkOptionsPopupMessageConstructionResourceShortage.Checked = _Game.DisplayPopupConstructionResourceShortage;
            chkOptionsPopupMessageUnderAttackCivilianShips.Checked = _Game.DisplayPopupUnderAttackCivilianShips;
            gcbeGaamXG.Checked = _Game.DisplayPopupUnderAttackCivilianBases;
            chkOptionsPopupMessageUnderAttackExplorationShips.Checked = _Game.DisplayPopupUnderAttackExplorationShips;
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.Checked = _Game.DisplayPopupUnderAttackColonyConstructionShips;
            chkOptionsPopupMessageUnderAttackMilitaryShips.Checked = _Game.DisplayPopupUnderAttackMilitaryShips;
            ltFewaOdau.Checked = _Game.DisplayPopupUnderAttackOtherStateBases;
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.Checked = _Game.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases;
        }

        private void method_418()
        {
            _Game.AutoPauseWhenInPopupWindow = chkOptionsAutoPauseInPopup.Checked;
            _Game.MainViewScrollSpeed = sldOptionsMainViewScrollSpeed.Value;
            _Game.MainViewZoomSpeed = sldOptionsMainViewZoomSpeed.Value;
            if (_Game.StarFieldSize != sldOptionsMainViewStarFieldSize.Value)
            {
                _Game.StarFieldSize = sldOptionsMainViewStarFieldSize.Value;
                mainView.ClearMain();
                mainView.method_14(_Game.StarFieldSize);
            }
            _Game.ShowSystemNebulae = chkOptionsShowSystemNebulae.Checked;
            _Game.MusicVolume = (double)sldOptionsMusicVolume.Value / 100.0;
            _Game.SoundEffectsVolume = (double)sldOptionsSoundEffectsVolume.Value / 100.0;
            MusicPlayer.SetVolume(_Game.MusicVolume);
            EffectsPlayer.Volume = _Game.SoundEffectsVolume;
            GlassButton.Volume = _Game.SoundEffectsVolume;
            GlassButton.Volume = _Game.SoundEffectsVolume;
            CloseButton.Volume = _Game.SoundEffectsVolume;
            ListViewBase.Volume = _Game.SoundEffectsVolume;
            HoverButton.Volume = _Game.SoundEffectsVolume;
            HoverMenuItem.Volume = _Game.SoundEffectsVolume;
            _Game.MouseScrollWheelBehaviour = method_414();
            gameOptions_0.LoadedGamesPaused = chkOptionsLoadedGamesPaused.Checked;
            if (chkOptionsAutoSave.Checked)
            {
                gameOptions_0.AutoSaveInterval = (int)numOptionsAutoSaveMinutes.Value;
            }
            else
            {
                gameOptions_0.AutoSaveInterval = 0;
            }
            _Game.PlayerEmpire.ControlMilitaryAttacks = method_419(fYpVlWkAfp.SelectedIndex);
            _Game.PlayerEmpire.ControlColonization = method_419(cmbOptionsControlColonization.SelectedIndex);
            _Game.PlayerEmpire.ControlColonyTaxRates = chkOptionsControlColonyTaxRates.Checked;
            _Game.PlayerEmpire.ControlStateConstruction = method_419(cmbOptionsControlConstruction.SelectedIndex);
            _Game.PlayerEmpire.ControlDesigns = chkOptionsControlDesigns.Checked;
            _Game.PlayerEmpire.ControlDiplomacyGifts = method_419(cmbOptionsControlDiplomacyGifts.SelectedIndex);
            _Game.PlayerEmpire.ControlDiplomacyOffense = method_419(cmbOptionsControlDiplomacyOffense.SelectedIndex);
            _Game.PlayerEmpire.ControlDiplomacyTreaties = method_419(cmbOptionsControlDiplomacyTreaties.SelectedIndex);
            _Game.PlayerEmpire.ControlMilitaryFleets = chkOptionsControlFleets.Checked;
            _Game.PlayerEmpire.ControlTroopGeneration = chkOptionsControlTroops.Checked;
            _Game.PlayerEmpire.ControlAgentAssignment = method_419(cmbOptionsControlAgentMissions.SelectedIndex);
            _Game.PlayerEmpire.ControlResearch = chkOptionsControlResearch.Checked;
            _Game.PlayerEmpire.ControlColonyFacilities = method_419(cmbOptionsControlColonyFacilities.SelectedIndex);
            _Game.PlayerEmpire.ControlPopulationPolicy = chkOptionsControlPopulationPolicy.Checked;
            _Game.PlayerEmpire.ControlCharacterLocations = chkOptionsControlCharacterLocations.Checked;
            _Game.PlayerEmpire.ControlOfferPirateMissions = method_419(cmbOptionsControlOfferPirateMissions.SelectedIndex);
            YxwyUefOyQ();
            method_257();
        }

        private AutomationLevel method_419(int int_64)
        {
            AutomationLevel result = AutomationLevel.Manual;
            switch (int_64)
            {
                case 0:
                    result = AutomationLevel.Manual;
                    break;
                case 1:
                    result = AutomationLevel.SemiAutomated;
                    break;
                case 2:
                    result = AutomationLevel.FullyAutomated;
                    break;
            }
            return result;
        }

        private int method_420(AutomationLevel automationLevel_0)
        {
            int result = 0;
            switch (automationLevel_0)
            {
                case AutomationLevel.Manual:
                    result = 0;
                    break;
                case AutomationLevel.SemiAutomated:
                    result = 1;
                    break;
                case AutomationLevel.FullyAutomated:
                    result = 2;
                    break;
            }
            return result;
        }

        private void method_421()
        {
            sldOptionsMainViewZoomSpeed.Minimum = 1;
            chkOptionsAutoPauseInPopup.Checked = _Game.AutoPauseWhenInPopupWindow;
            sldOptionsMainViewScrollSpeed.Value = _Game.MainViewScrollSpeed;
            sldOptionsMainViewStarFieldSize.Value = _Game.StarFieldSize;
            sldOptionsMainViewZoomSpeed.Value = _Game.MainViewZoomSpeed;
            chkOptionsShowSystemNebulae.Checked = _Game.ShowSystemNebulae;
            sldOptionsMusicVolume.Value = (int)(_Game.MusicVolume * 100.0);
            sldOptionsSoundEffectsVolume.Value = (int)(_Game.SoundEffectsVolume * 100.0);
            method_415(_Game.MouseScrollWheelBehaviour);
            chkOptionsLoadedGamesPaused.Checked = gameOptions_0.LoadedGamesPaused;
            if (gameOptions_0.AutoSaveInterval > 0)
            {
                chkOptionsAutoSave.Checked = true;
                numOptionsAutoSaveMinutes.Value = Math.Max(10, gameOptions_0.AutoSaveInterval);
                numOptionsAutoSaveMinutes.Enabled = true;
            }
            else
            {
                chkOptionsAutoSave.Checked = false;
                numOptionsAutoSaveMinutes.Enabled = false;
            }
            fYpVlWkAfp.SelectedIndex = method_420(_Game.PlayerEmpire.ControlMilitaryAttacks);
            cmbOptionsControlColonization.SelectedIndex = method_420(_Game.PlayerEmpire.ControlColonization);
            chkOptionsControlColonyTaxRates.Checked = _Game.PlayerEmpire.ControlColonyTaxRates;
            cmbOptionsControlConstruction.SelectedIndex = method_420(_Game.PlayerEmpire.ControlStateConstruction);
            chkOptionsControlDesigns.Checked = _Game.PlayerEmpire.ControlDesigns;
            cmbOptionsControlDiplomacyGifts.SelectedIndex = method_420(_Game.PlayerEmpire.ControlDiplomacyGifts);
            cmbOptionsControlDiplomacyOffense.SelectedIndex = method_420(_Game.PlayerEmpire.ControlDiplomacyOffense);
            cmbOptionsControlDiplomacyTreaties.SelectedIndex = method_420(_Game.PlayerEmpire.ControlDiplomacyTreaties);
            chkOptionsControlFleets.Checked = _Game.PlayerEmpire.ControlMilitaryFleets;
            chkOptionsControlTroops.Checked = _Game.PlayerEmpire.ControlTroopGeneration;
            cmbOptionsControlAgentMissions.SelectedIndex = method_420(_Game.PlayerEmpire.ControlAgentAssignment);
            chkOptionsControlResearch.Checked = _Game.PlayerEmpire.ControlResearch;
            cmbOptionsControlColonyFacilities.SelectedIndex = method_420(_Game.PlayerEmpire.ControlColonyFacilities);
            chkOptionsControlPopulationPolicy.Checked = _Game.PlayerEmpire.ControlPopulationPolicy;
            chkOptionsControlCharacterLocations.Checked = _Game.PlayerEmpire.ControlCharacterLocations;
            cmbOptionsControlOfferPirateMissions.SelectedIndex = method_420(_Game.PlayerEmpire.ControlOfferPirateMissions);
        }

        private void sldOptionsMusicVolume_Scroll(object sender, ScrollEventArgs e)
        {
            _Game.MusicVolume = (double)sldOptionsMusicVolume.Value / 100.0;
            MusicPlayer.SetVolume(_Game.MusicVolume);
        }

        private void sldOptionsSoundEffectsVolume_Scroll(object sender, ScrollEventArgs e)
        {
            _Game.SoundEffectsVolume = (double)sldOptionsSoundEffectsVolume.Value / 100.0;
            EffectsPlayer.Volume = _Game.SoundEffectsVolume;
        }

        private void pnlMessagePopup_Click(object sender, EventArgs e)
        {
            EmpireMessage message = pnlMessagePopup.Message;
            object obj = null;
            obj = message.Subject;
            if (obj is DiplomaticRelationType)
            {
                obj = message.Sender;
            }
            if (message.Money > 0)
            {
                obj = message.Sender;
            }
            if (obj == null)
            {
                return;
            }
            if (obj is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)obj;
                method_157(builtObject);
                method_4(1.0);
                method_208(builtObject);
                if (builtObject.Role != BuiltObjectRole.Base && builtObject.CurrentSpeed >= (float)builtObject.WarpSpeed && builtObject.WarpSpeed > 0)
                {
                    UhvLmNjli7 = true;
                }
            }
            else if (obj is ShipGroup)
            {
                ShipGroup object_ = (ShipGroup)obj;
                method_157(object_);
                method_4(3000.0);
                method_208(object_);
            }
            else if (obj is Habitat)
            {
                Habitat habitat = (Habitat)obj;
                if (message.MessageType == EmpireMessageType.NewColony)
                {
                    method_208(habitat);
                    method_166(habitat);
                }
                else if (message.MessageType == EmpireMessageType.BattleUnderAttack)
                {
                    method_157(habitat);
                    method_4(1.0);
                    method_208(habitat);
                    method_164(habitat);
                }
                else
                {
                    method_157(habitat);
                    method_4(1.0);
                    method_208(habitat);
                }
            }
            else if (obj is Character)
            {
                Character character = (Character)obj;
                if (character.Empire == _Game.PlayerEmpire)
                {
                    method_424(character);
                }
                else
                {
                    method_195(character.Empire);
                }
            }
            else if (obj is Resource)
            {
                Resource resource = (Resource)obj;
                method_456(resource.Name);
            }
            else if (obj is Empire)
            {
                method_195((Empire)obj);
            }
            else if (obj is ResearchNode)
            {
                ResearchNode researchNode = (ResearchNode)obj;
                if (researchNode.Components != null && researchNode.Components.Count > 0)
                {
                    method_394(researchNode.Industry, researchNode);
                    method_543(researchNode.Components[0]);
                }
                else if (researchNode.ComponentImprovements != null && researchNode.ComponentImprovements.Count > 0)
                {
                    method_394(researchNode.Industry, researchNode);
                    method_543(researchNode.ComponentImprovements[0].ImprovedComponent);
                }
                else if (researchNode.Abilities != null && researchNode.Abilities.Count > 0)
                {
                    method_394(researchNode.Industry, researchNode);
                }
                else if (researchNode.Fighters != null && researchNode.Fighters.Count > 0)
                {
                    method_394(researchNode.Industry, researchNode);
                }
                else if (researchNode.PlanetaryFacility != null)
                {
                    method_394(researchNode.Industry, researchNode);
                }
            }
            else if (obj is Point)
            {
                Point point = (Point)obj;
                method_156(point.X, point.Y);
                method_4(1.0);
            }
        }

        private void jLijmZicsW(object sender, EventArgs e)
        {
            if (sender != null && sender is Empire)
            {
                Empire empire = (Empire)sender;
                diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire);
            }
            ctlEmpireDiplomaticRelationList.Invalidate();
        }

        private void btnColonyShowOnGalaxyMap_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat != null)
            {
                method_131(selectedHabitat);
            }
        }

        private void ctlEmpireDiplomaticRelationList_SelectionChanged_1(object sender, EventArgs e)
        {
            Empire selectedEmpire = ctlEmpireDiplomaticRelationList.SelectedEmpire;
            if (selectedEmpire != _Game.PlayerEmpire && selectedEmpire != null)
            {
                btnEmpireTalk.Enabled = true;
            }
            else
            {
                btnEmpireTalk.Enabled = false;
            }
        }

        private void btnBuiltObjectViewShipGroup_Click(object sender, EventArgs e)
        {
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                if (selectedBuiltObject.ShipGroup != null)
                {
                    method_268(selectedBuiltObject.ShipGroup);
                }
                return;
            }
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            if (selectedBuiltObjects != null && selectedBuiltObjects.Count > 0 && selectedBuiltObjects[0].ShipGroup != null)
            {
                method_268(selectedBuiltObjects[0].ShipGroup);
            }
        }

        private void btnBuiltObjectViewDesign_Click(object sender, EventArgs e)
        {
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                design_1 = selectedBuiltObject.Design.Clone();
                if (selectedBuiltObject.Design.BuildCount > 0)
                {
                    string_16 = "view";
                }
                else if (selectedBuiltObject.Design.Empire != null && selectedBuiltObject.Design.Empire.CheckDesignInUseForConstructionOrRetrofits(selectedBuiltObject.Design))
                {
                    string_16 = "view";
                }
                else
                {
                    string_16 = "edit";
                }
                OpenDesignEditor(selectedBuiltObject.Design);
            }
        }

        private void ctlBuiltObjectList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(ctlBuiltObjectList.Grid.Columns[e.ColumnIndex].Name == "Automated") || e.RowIndex < 0)
            {
                return;
            }
            StellarObject stellarObject = ctlBuiltObjectList.ResolveStellarObject(ctlBuiltObjectList.Rows[e.RowIndex]);
            if (stellarObject == null || !(stellarObject is BuiltObject))
            {
                return;
            }
            BuiltObject builtObject = (BuiltObject)stellarObject;
            if (builtObject.Owner == null || builtObject.Owner != _Game.PlayerEmpire || builtObject.Role == BuiltObjectRole.Base)
            {
                return;
            }
            builtObject.IsAutoControlled = !builtObject.IsAutoControlled;
            ctlBuiltObjectList.SetAutomation(ctlBuiltObjectList.Rows[e.RowIndex], builtObject.IsAutoControlled);
            if (builtObject.IsAutoControlled && builtObject.Empire != null)
            {
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

        private void ctlBuiltObjectList_SelectionChanged_1(object sender, EventArgs e)
        {
            if (cmbBuiltObjectSetFleet.Items != null && cmbBuiltObjectSetFleet.Items.Count > 0)
            {
                cmbBuiltObjectSetFleet.SelectedIndex = 0;
            }
            BuiltObject selectedBuiltObject = ctlBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                if (selectedBuiltObject.ShipGroup == null)
                {
                    btnBuiltObjectViewShipGroup.Enabled = false;
                }
                else
                {
                    btnBuiltObjectViewShipGroup.Enabled = true;
                }
                if (selectedBuiltObject.Role == BuiltObjectRole.Military)
                {
                    cmbBuiltObjectSetFleet.Enabled = true;
                }
                else
                {
                    cmbBuiltObjectSetFleet.Enabled = false;
                }
                btnBuiltObjectScrapSelected.Enabled = false;
                btnBuiltObjectRetireSelected.Enabled = false;
                btnBuiltObjectRetrofitSelected.Enabled = false;
                btnBuiltObjectRefuelSelected.Enabled = false;
                btnBuiltObjectRepairSelected.Enabled = false;
                btnBuiltObjectScrapSelected.Enabled = true;
                if (selectedBuiltObject.Owner != null && selectedBuiltObject.TopSpeed > 0 && selectedBuiltObject.Role != BuiltObjectRole.Base)
                {
                    btnBuiltObjectRetireSelected.Enabled = true;
                }
                if (selectedBuiltObject.RetrofitDesign == null)
                {
                    btnBuiltObjectRetrofitSelected.Enabled = true;
                }
                if (selectedBuiltObject.TopSpeed > 0 && selectedBuiltObject.Role != BuiltObjectRole.Base)
                {
                    btnBuiltObjectRefuelSelected.Enabled = true;
                }
                if (selectedBuiltObject.Role != BuiltObjectRole.Base && selectedBuiltObject.TopSpeed > 0 && selectedBuiltObject.DamagedComponentCount > 0)
                {
                    btnBuiltObjectRepairSelected.Enabled = true;
                }
            }
        }

        private void cmbBuiltObjectSetFleet_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = cmbBuiltObjectSetFleet.Items[cmbBuiltObjectSetFleet.SelectedIndex].ToString();
            BuiltObjectList selectedBuiltObjects = ctlBuiltObjectList.SelectedBuiltObjects;
            if (selectedBuiltObjects == null || selectedBuiltObjects.Count <= 0)
            {
                return;
            }
            if (text == "(" + TextResolver.GetText("New Fleet") + ")")
            {
                if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                {
                    _Game.PlayerEmpire.ControlMilitaryFleets = false;
                }
                ShipGroup shipGroup = new ShipGroup(_Game.Galaxy);
                shipGroup.Empire = selectedBuiltObjects[0].Empire;
                shipGroup.GatherPoint = null;
                string nextFleetNumberDescription = selectedBuiltObjects[0].Empire.GetNextFleetNumberDescription();
                shipGroup.Name = string.Format(TextResolver.GetText("Nth Fleet"), nextFleetNumberDescription);
                selectedBuiltObjects[0].Empire.ShipGroups.Add(shipGroup);
                for (int i = 0; i < selectedBuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = selectedBuiltObjects[i];
                    if (builtObject.Role == BuiltObjectRole.Military)
                    {
                        shipGroup.AddShipToFleet(builtObject);
                    }
                }
                shipGroup.Update();
                selectedBuiltObjects[0].Empire.ShipGroups.Sort();
                btnBuiltObjectViewShipGroup.Enabled = true;
                foreach (DataGridViewRow selectedRow in ctlBuiltObjectList.Grid.SelectedRows)
                {
                    BuiltObject builtObject2 = ctlBuiltObjectList.ResolveBuiltObject(selectedRow);
                    if (builtObject2 != null && builtObject2.Role == BuiltObjectRole.Military)
                    {
                        selectedRow.Cells[9].Value = shipGroup.Name;
                    }
                }
                method_182();
                cmbBuiltObjectSetFleet.SelectedIndexChanged -= cmbBuiltObjectSetFleet_SelectedIndexChanged;
                cmbBuiltObjectSetFleet.SelectedIndex = cmbBuiltObjectSetFleet.Items.Count - 1;
                cmbBuiltObjectSetFleet.SelectedIndexChanged += cmbBuiltObjectSetFleet_SelectedIndexChanged;
            }
            else
            {
                if (!(text != TextResolver.GetText("Set Fleet") + "..."))
                {
                    return;
                }
                if (_Game.PlayerEmpire.ControlMilitaryFleets && GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
                {
                    _Game.PlayerEmpire.ControlMilitaryFleets = false;
                }
                ShipGroup shipGroup2 = null;
                for (int j = 0; j < _Game.PlayerEmpire.ShipGroups.Count; j++)
                {
                    ShipGroup shipGroup3 = _Game.PlayerEmpire.ShipGroups[j];
                    if (shipGroup3.Name == text)
                    {
                        shipGroup2 = shipGroup3;
                        break;
                    }
                }
                if (shipGroup2 != null)
                {
                    for (int k = 0; k < selectedBuiltObjects.Count; k++)
                    {
                        BuiltObject builtObject3 = selectedBuiltObjects[k];
                        if (builtObject3.Role == BuiltObjectRole.Military && builtObject3.ShipGroup != shipGroup2)
                        {
                            shipGroup2.AddShipToFleet(builtObject3);
                            btnBuiltObjectViewShipGroup.Enabled = true;
                        }
                    }
                    foreach (DataGridViewRow selectedRow2 in ctlBuiltObjectList.Grid.SelectedRows)
                    {
                        BuiltObject builtObject4 = ctlBuiltObjectList.ResolveBuiltObject(selectedRow2);
                        if (builtObject4 != null && builtObject4.Role == BuiltObjectRole.Military)
                        {
                            selectedRow2.Cells[9].Value = shipGroup2.Name;
                        }
                    }
                }
                else
                {
                    for (int l = 0; l < selectedBuiltObjects.Count; l++)
                    {
                        BuiltObject builtObject5 = selectedBuiltObjects[l];
                        builtObject5.LeaveShipGroup();
                        btnBuiltObjectViewShipGroup.Enabled = false;
                    }
                    foreach (DataGridViewRow selectedRow3 in ctlBuiltObjectList.Grid.SelectedRows)
                    {
                        selectedRow3.Cells[9].Value = "(" + TextResolver.GetText("None") + ")";
                    }
                }
                method_182();
                cmbBuiltObjectSetFleet.SelectedIndexChanged -= cmbBuiltObjectSetFleet_SelectedIndexChanged;
                if (shipGroup2 != null)
                {
                    int selectedIndex = 0;
                    for (int m = 0; m < cmbBuiltObjectSetFleet.Items.Count; m++)
                    {
                        if (cmbBuiltObjectSetFleet.Items[m].ToString() == shipGroup2.Name)
                        {
                            selectedIndex = m;
                            break;
                        }
                    }
                    cmbBuiltObjectSetFleet.SelectedIndex = selectedIndex;
                }
                else
                {
                    cmbBuiltObjectSetFleet.SelectedIndex = 0;
                }
                cmbBuiltObjectSetFleet.SelectedIndexChanged += cmbBuiltObjectSetFleet_SelectedIndexChanged;
            }
        }

        private int method_422()
        {
            string name = string.Empty;
            if (cmbEmpireSummaryChangeGovernmentType.SelectedIndex >= 0)
            {
                name = cmbEmpireSummaryChangeGovernmentType.Items[cmbEmpireSummaryChangeGovernmentType.SelectedIndex].ToString();
            }
            return _Game.Galaxy.Governments.GetByName(name)?.GovernmentId ?? (-1);
        }

        private void btnEmpireSummaryChangeGovernment_Click(object sender, EventArgs e)
        {
            int num = method_422();
            if (_Game.PlayerEmpire.GovernmentId != num && num >= 0)
            {
                string string_ = string.Format(TextResolver.GetText("Changing your style of government can have serious negative effects on your empire"), _Game.Galaxy.Governments[num].Name);
                MessageBoxEx messageBoxEx = method_372(string_, TextResolver.GetText("Have a Revolution?"));
                if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
                {
                    _Game.PlayerEmpire.HaveRevolution(_Game.PlayerEmpire.DominantRace, num);
                }
            }
            pnlEmpireSummaryColony.Invalidate();
        }

        private StellarObjectList method_423(BuiltObject builtObject_8)
        {
            return BaconMain.method_423(this, builtObject_8);
        }

        private void cmbBuiltObjectFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int_60 = cmbBuiltObjectFilter.SelectedIndex;
            if (int_60 == 1)
            {
                int_60 = 0;
            }
            StellarObjectList stellarObjectList = new StellarObjectList();
            stellarObjectList = ((_Game.SelectedObject == null || !(_Game.SelectedObject is BuiltObject)) ? method_423(null) : method_423((BuiltObject)_Game.SelectedObject));
            ctlBuiltObjectList.BindDataGeneric(stellarObjectList, _Game.Galaxy, showDetails: true);
            if (cmbBuiltObjectFilter.SelectedIndex > 0)
            {
                gmapBuiltObject.SetLocations(stellarObjectList);
            }
            else
            {
                gmapBuiltObject.ClearLocations();
            }
            ctlBuiltObjectList_SelectionChanged(null, null);
        }

        private void btnMainViewDisplayToggle_Click(object sender, EventArgs e)
        {
            switch (int_34)
            {
                default:
                    int_34 = 0;
                    break;
                case 0:
                    int_34 = 1;
                    break;
                case 1:
                    int_34 = 2;
                    break;
                case 2:
                    int_34 = 0;
                    break;
            }
        }

        private void Rxnjobkvid(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Characters"));
        }

        private void method_424(Character character_0)
        {
            method_425(character_0, _Game.PlayerEmpire, bool_28: false);
        }

        private void method_425(Character character_0, Empire empire_5, bool bool_28)
        {
            bool_9 = true;
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            kYdDyYeMls.Size = new Size(1020, 770);
            kYdDyYeMls.Location = new Point((mainView.Width - kYdDyYeMls.Width) / 2, (mainView.Height - kYdDyYeMls.Height) / 2);
            kYdDyYeMls.DoLayout();
            empire_4 = empire_5;
            bool_26 = bool_28;
            if (bool_28)
            {
                string text = TextResolver.GetText("Edit Characters") + ": ";
                if (empire_5 != null)
                {
                    text += empire_5.Name;
                }
                kYdDyYeMls.HeaderTitle = text;
            }
            else
            {
                kYdDyYeMls.HeaderTitle = TextResolver.GetText("Characters");
            }
            lnkCharactersLearnAbout.Text = TextResolver.GetText("Learn about Characters") + "...";
            lnkCharactersLearnAbout.Location = new Point(840, 12);
            method_403(lnkCharactersLearnAbout, 840, new Size(160, 21));
            lblCharacterSummary.Location = new Point(10, 8);
            string text2 = Galaxy.ResolveCharacterSummary(empire_5);
            lblCharacterSummary.AutoSize = false;
            lblCharacterSummary.Size = new Size(570, 35);
            lblCharacterSummary.MaximumSize = new Size(570, 35);
            lblCharacterSummary.Text = text2;
            btnIntelligenceAgentsDisband.Location = new Point(595, 8);
            btnIntelligenceAgentsDisband.Size = new Size(90, 30);
            btnCharacterShowEventHistory.Location = new Point(689, 8);
            btnCharacterShowEventHistory.Size = new Size(146, 30);
            btnCharacterShowEventHistory.Text = TextResolver.GetText("Show Event History");
            ctlIntelligenceAgents.Size = new Size(570, 655);
            ctlIntelligenceAgents.Location = new Point(10, 40);
            ctlIntelligenceAgents.BindData(characterImageCache_0, empire_5.Characters, _Game.Galaxy, bitmap_91);
            ctlIntelligenceAgents.Grid.Columns["Image"].Width = 40;
            ctlIntelligenceAgents.Grid.Columns["Name"].Width = 100;
            ctlIntelligenceAgents.Grid.Columns["Role"].Width = 100;
            ctlIntelligenceAgents.Grid.Columns["Location"].Width = 100;
            ctlIntelligenceAgents.Grid.Columns["Mission"].Width = 230;
            ctlIntelligenceAgents.BringToFront();
            if (character_0 != null && character_0.Empire != empire_5)
            {
                character_0 = null;
            }
            if (character_0 != null)
            {
                ctlIntelligenceAgents.SelectCharacter(character_0);
            }
            if (character_0 == null)
            {
                character_0 = ctlIntelligenceAgents.SelectedCharacter;
            }
            btnIntelligenceAgentsDisband.Text = TextResolver.GetText("Dismiss");
            ctlCharacterSummary.Size = new Size(400, 675);
            ctlCharacterSummary.Location = new Point(595, 40);
            ctlCharacterSummary.BindData(character_0, _Game.Galaxy, characterImageCache_0, bitmap_29, bitmap_31, bitmap_189, bool_28);
            if (character_0 != null)
            {
                string toolTipTitle = character_0.Name + " (" + Galaxy.ResolveDescription(character_0.Role) + ")";
                string caption = Galaxy.ResolveCharacterDescription(character_0, includeName: false);
                toolTip_0.SetToolTip(ctlCharacterSummary, caption);
                toolTip_0.SetToolTip(ctlCharacterSummary.pnlCharacterSkillsTraits, caption);
                toolTip_0.SetToolTip(ctlCharacterSummary.pnlCharacterSkillsTraitsContainer, caption);
                toolTip_0.ToolTipTitle = toolTipTitle;
                toolTip_0.AutoPopDelay = 30000;
                toolTip_0.Active = false;
                toolTip_0.Active = true;
            }
            else
            {
                toolTip_0.RemoveAll();
                toolTip_0.AutoPopDelay = 5000;
                toolTip_0.Active = false;
                toolTip_0.Active = true;
            }
            pnlCharacterMission.Size = new Size(400, 185);
            pnlCharacterMission.Location = new Point(595, 510);
            pnlCharacterMission.BindData(empire_5, _Game.Galaxy, ctlIntelligenceAgents.SelectedCharacter);
            if (ctlIntelligenceAgents.SelectedCharacter != null && ctlIntelligenceAgents.SelectedCharacter.Role == CharacterRole.IntelligenceAgent && !bool_28)
            {
                ctlCharacterSummary.Size = new Size(400, 470);
                pnlCharacterMission.Visible = true;
                pnlCharacterMission.BringToFront();
            }
            else
            {
                ctlCharacterSummary.Size = new Size(400, 655);
                pnlCharacterMission.Visible = false;
                pnlCharacterMission.SendToBack();
            }
            lblIntelligenceAgentHeader.Visible = false;
            lblIntelligenceAgentSummary.Visible = false;
            lblIntelligenceAgentMax.Visible = false;
            btnIntelligenceAgentsRecruit.Visible = false;
            if (empire_5.Characters.Count > 0)
            {
                Character selectedCharacter = ctlIntelligenceAgents.SelectedCharacter;
                if (selectedCharacter != null)
                {
                    btnIntelligenceAgentsDisband.Enabled = true;
                }
            }
            kYdDyYeMls.Visible = true;
            kYdDyYeMls.BringToFront();
            ctlIntelligenceAgents.Focus();
        }

        private void method_426()
        {
            kYdDyYeMls.SendToBack();
            kYdDyYeMls.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
            method_663();
        }

        private void kYdDyYeMls_CloseButtonClicked(object sender, EventArgs e)
        {
            method_426();
        }

        private void tbtnIntelligenceAgents_Click(object sender, EventArgs e)
        {
            if (kYdDyYeMls.Visible)
            {
                method_426();
            }
            else
            {
                method_424(null);
            }
        }

        private void tbtnConstructionYards_Click(object sender, EventArgs e)
        {
            if (pnlBuiltObjectInfo.Visible)
            {
                method_185();
            }
            else
            {
                method_177(null);
                int num = int_60;
                cmbBuiltObjectFilter.SelectedIndex = 3;
                int_60 = num;
            }
            ctlBuiltObjectList.Focus();
        }

        private void ctlIntelligenceAgents_CharacterDoubleClicked(object sender, EventArgs e)
        {
            Character selectedCharacter = ctlIntelligenceAgents.SelectedCharacter;
            if (selectedCharacter != null && selectedCharacter.Location != null)
            {
                method_208(selectedCharacter.Location);
                method_157(selectedCharacter);
                method_4(1.0);
            }
        }

        private void ctlIntelligenceAgents_CharacterChanged(object sender, EventArgs e)
        {
            Character selectedCharacter = ctlIntelligenceAgents.SelectedCharacter;
            pnlCharacterMission.BindData(empire_4, _Game.Galaxy, selectedCharacter);
            ctlCharacterSummary.BindData(selectedCharacter, _Game.Galaxy, characterImageCache_0, bitmap_29, bitmap_31, bitmap_189, bool_26);
            if (selectedCharacter == null)
            {
                btnIntelligenceAgentsDisband.Text = TextResolver.GetText("Disband Selected Character");
                btnIntelligenceAgentsDisband.Enabled = false;
                toolTip_0.RemoveAll();
                toolTip_0.AutoPopDelay = 5000;
                toolTip_0.Active = false;
                toolTip_0.Active = true;
            }
            else
            {
                btnIntelligenceAgentsDisband.Text = string.Format(TextResolver.GetText("Disband CHARACTER"), selectedCharacter.Name);
                btnIntelligenceAgentsDisband.Enabled = true;
                string toolTipTitle = selectedCharacter.Name + " (" + Galaxy.ResolveDescription(selectedCharacter.Role) + ")";
                string caption = Galaxy.ResolveCharacterDescription(selectedCharacter, includeName: false);
                toolTip_0.SetToolTip(ctlCharacterSummary, caption);
                toolTip_0.SetToolTip(ctlCharacterSummary.pnlCharacterSkillsTraits, caption);
                toolTip_0.SetToolTip(ctlCharacterSummary.pnlCharacterSkillsTraitsContainer, caption);
                toolTip_0.ToolTipTitle = toolTipTitle;
                toolTip_0.AutoPopDelay = 30000;
                toolTip_0.Active = false;
                toolTip_0.Active = true;
            }
            btnIntelligenceAgentsDisband.Text = TextResolver.GetText("Dismiss");
            if (selectedCharacter != null && selectedCharacter.Role == CharacterRole.IntelligenceAgent && !bool_26)
            {
                ctlCharacterSummary.Size = new Size(400, 470);
                pnlCharacterMission.Visible = true;
                pnlCharacterMission.BringToFront();
            }
            else
            {
                ctlCharacterSummary.Size = new Size(400, 655);
                pnlCharacterMission.Visible = false;
                pnlCharacterMission.SendToBack();
            }
        }

        private void pnlCharacterMission_MissionCancelled(object sender, EventArgs e)
        {
            Character selectedCharacter = ctlIntelligenceAgents.SelectedCharacter;
            method_424(selectedCharacter);
        }

        private void ctlCharacterSummary_CharacterNameChanged(object sender, EventArgs e)
        {
            Character selectedCharacter = ctlIntelligenceAgents.SelectedCharacter;
            if (ctlIntelligenceAgents.Grid != null && ctlIntelligenceAgents.Grid.SelectedRows != null && ctlIntelligenceAgents.Grid.SelectedRows.Count > 0)
            {
                ctlIntelligenceAgents.Grid.SelectedRows[0].Cells["Name"].Value = selectedCharacter.Name;
            }
        }

        private void ctlCharacterSummary_CharacterTransferInitiated(object sender, EventArgs e)
        {
            Character selectedCharacter = ctlIntelligenceAgents.SelectedCharacter;
            method_425(selectedCharacter, empire_4, bool_26);
        }

        private void btnIntelligenceAgentsRecruit_Click(object sender, EventArgs e)
        {
            int num = _Game.PlayerEmpire.Characters.CountCharactersByRole(CharacterRole.IntelligenceAgent);
            if (num < _Game.PlayerEmpire.MaximumAgentCount)
            {
                double num2 = Galaxy.AgentAnnualMaintenance * 5.0;
                bool isRandomCharacter = false;
                Character character = _Game.PlayerEmpire.GenerateNewCharacter(CharacterRole.IntelligenceAgent, _Game.PlayerEmpire.Capital, out isRandomCharacter);
                _Game.PlayerEmpire.StateMoney -= num2;
                _Game.Galaxy.DoCharacterEventLeader(CharacterEventType.IntelligenceAgentRecruited, character, _Game.PlayerEmpire);
                IntelligenceMission intelligenceMission = new IntelligenceMission(_Game.PlayerEmpire, character, _Game.Galaxy.CurrentStarDate);
                intelligenceMission.TimeLength = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
                character.Mission = intelligenceMission;
                method_424(character);
            }
        }

        private void btnIntelligenceAgentsDisband_Click(object sender, EventArgs e)
        {
            Character selectedCharacter = ctlIntelligenceAgents.SelectedCharacter;
            if (selectedCharacter == null)
            {
                return;
            }
            if ((selectedCharacter.Role == CharacterRole.Leader || selectedCharacter.Role == CharacterRole.PirateLeader) && _Game.PlayerEmpire.LeaderChangeInfluence != 0.0)
            {
                MessageBoxEx messageBoxEx = method_370(string.Format(TextResolver.GetText("You cannot currently dismiss your leader, because your empire already had a recent leadership change"), selectedCharacter.Name), TextResolver.GetText("Cannot Dismiss Leader Now"));
                messageBoxEx.Show(this);
            }
            else
            {
                MessageBoxEx messageBoxEx2 = method_372(string.Format(TextResolver.GetText("Are you sure that you wish to disband this character?"), selectedCharacter.Name), TextResolver.GetText("Disband Character"));
                if (messageBoxEx2.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
                {
                    selectedCharacter.Mission = null;
                    selectedCharacter.Kill(_Game.Galaxy);
                    method_425(null, empire_4, bool_26);
                }
            }
            Focus();
        }

        private void pnlBuiltObjectConstructionYardPurchaser_PurchaseMade(object sender, EventArgs e)
        {
            ctlConstructionYards.BindData(_Game.Galaxy, ctlConstructionYards.ConstructionYards, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
            if (ctlBuiltObjectList.SelectedStellarObject != null)
            {
                if (ctlBuiltObjectList.SelectedStellarObject.ConstructionQueue != null)
                {
                    ctlConstructionYardWaitQueue.BindData(ctlBuiltObjectList.SelectedStellarObject.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                }
                else
                {
                    ctlConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
                }
            }
            else
            {
                ctlConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
            }
        }

        private void pnlColonyConstructionYardPurchaser_PurchaseMade(object sender, EventArgs e)
        {
            ctlColonyConstructionYard.BindData(_Game.Galaxy, ctlColonyConstructionYard.ConstructionYards, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
            if (UnlxwvByxj.SelectedHabitat != null)
            {
                if (UnlxwvByxj.SelectedHabitat.ConstructionQueue != null)
                {
                    ctlColonyConstructionYardWaitQueue.BindData(UnlxwvByxj.SelectedHabitat.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                }
                else
                {
                    ctlColonyConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
                }
            }
            else
            {
                ctlColonyConstructionYardWaitQueue.BindData(null, _Game.Galaxy);
            }
        }

        public void btnColonyTroopsRecruit_Click(object sender, EventArgs e)
        {
            if (_Game.PlayerEmpire.ControlTroopGeneration && GenerateAutomationMessageBox(TextResolver.GetText("Troop Recruitment")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                _Game.PlayerEmpire.ControlTroopGeneration = false;
            }
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat == null || selectedHabitat.TroopsToRecruit == null)
            {
                return;
            }
            Race dominantRace = selectedHabitat.Population.DominantRace;
            int num = (int)(100.0 * ((double)dominantRace.AggressionLevel / 100.0) * ((double)dominantRace.IntelligenceLevel / 100.0));
            if (dominantRace != null)
            {
                num = dominantRace.TroopStrength;
            }
            if (selectedHabitat.Ruin != null)
            {
                num = (int)((double)num * (1.0 + selectedHabitat.Ruin.BonusDefensive));
            }
            bool bool_ = false;
            bool bool_2 = false;
            bool bool_3 = false;
            Troop troop = method_168(out bool_, out bool_2, out bool_3);
            if (troop != null)
            {
                Troop empireBestTroop = selectedHabitat.Empire.IdentifyStrongestRaceAttackTroop();
                Troop item = selectedHabitat.GenerateNewTroop(troop.Type, empireBestTroop, bool_, bool_2, bool_3);
                selectedHabitat.TroopsToRecruit.Add(item);
                if (selectedHabitat.Empire != null && selectedHabitat.Empire.Troops != null)
                {
                    selectedHabitat.Empire.Troops.Add(item);
                }
                ctlColonyCharacterTroops.BindData(selectedHabitat.Empire, selectedHabitat.Characters, selectedHabitat.InvadingCharacters, selectedHabitat.Troops, selectedHabitat.TroopsToRecruit, selectedHabitat.InvadingTroops, characterImageCache_0);
            }
        }

        private void btnColonyTroopsDisband_Click(object sender, EventArgs e)
        {
            if (_Game.PlayerEmpire.ControlTroopGeneration && GenerateAutomationMessageBox(TextResolver.GetText("Troop Recruitment")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                _Game.PlayerEmpire.ControlTroopGeneration = false;
            }
            object selectedObject = ctlColonyCharacterTroops.SelectedObject;
            if (!(selectedObject is Troop))
            {
                return;
            }
            Troop troop = (Troop)selectedObject;
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat == null || troop.Empire != selectedHabitat.Empire)
            {
                return;
            }
            MessageBoxEx messageBoxEx = method_372(string.Format(TextResolver.GetText("Are you sure that you want to disband TROOPNAME?"), troop.Name), TextResolver.GetText("Disband Troop"));
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                if (selectedHabitat.Troops != null && selectedHabitat.Troops.Contains(troop))
                {
                    selectedHabitat.Troops.Remove(troop);
                }
                else if (selectedHabitat.TroopsToRecruit != null && selectedHabitat.TroopsToRecruit.Contains(troop))
                {
                    selectedHabitat.TroopsToRecruit.Remove(troop);
                }
                troop.BuiltObject = null;
                troop.Colony = null;
                troop.Empire.Troops.Remove(troop);
                troop.Empire = null;
                ctlColonyCharacterTroops.BindData(selectedHabitat.Empire, selectedHabitat.Characters, selectedHabitat.InvadingCharacters, selectedHabitat.Troops, selectedHabitat.TroopsToRecruit, selectedHabitat.InvadingTroops, characterImageCache_0);
            }
            Focus();
        }

        private void btnColonyTroopGarrison_Click(object sender, EventArgs e)
        {
            object selectedObject = ctlColonyCharacterTroops.SelectedObject;
            int selectedIndex = ctlColonyCharacterTroops.SelectedIndex;
            if (selectedObject != null && selectedObject is Troop)
            {
                Troop troop = (Troop)selectedObject;
                Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
                if (selectedHabitat != null && troop.Empire == selectedHabitat.Empire && !troop.Garrisoned)
                {
                    troop.Garrisoned = true;
                    ctlColonyCharacterTroops.BindData(selectedHabitat.Empire, selectedHabitat.Characters, selectedHabitat.InvadingCharacters, selectedHabitat.Troops, selectedHabitat.TroopsToRecruit, selectedHabitat.InvadingTroops, characterImageCache_0);
                    ctlColonyCharacterTroops.SetSelectedItem(selectedIndex);
                }
            }
        }

        private void btnColonyTroopsUngarrison_Click(object sender, EventArgs e)
        {
            object selectedObject = ctlColonyCharacterTroops.SelectedObject;
            int selectedIndex = ctlColonyCharacterTroops.SelectedIndex;
            if (selectedObject != null && selectedObject is Troop)
            {
                Troop troop = (Troop)selectedObject;
                Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
                if (selectedHabitat != null && troop.Empire == selectedHabitat.Empire && troop.Garrisoned)
                {
                    troop.Garrisoned = false;
                    ctlColonyCharacterTroops.BindData(selectedHabitat.Empire, selectedHabitat.Characters, selectedHabitat.InvadingCharacters, selectedHabitat.Troops, selectedHabitat.TroopsToRecruit, selectedHabitat.InvadingTroops, characterImageCache_0);
                    ctlColonyCharacterTroops.SetSelectedItem(selectedIndex);
                }
            }
        }

        private void btnColonyTroopTransferTransport_Click(object sender, EventArgs e)
        {
            object selectedObject = ctlColonyCharacterTroops.SelectedObject;
            if (selectedObject is Troop)
            {
                Troop troop = (Troop)selectedObject;
                BuiltObject builtObject = method_428();
                if (builtObject == null || builtObject.Troops == null || builtObject.Empire != _Game.PlayerEmpire)
                {
                    return;
                }
                Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
                if (selectedHabitat != null && selectedHabitat.Empire == _Game.PlayerEmpire && selectedHabitat.Troops != null && selectedHabitat.Troops.Contains(troop) && builtObject.TroopCapacityRemaining >= troop.Size)
                {
                    if (troop.Garrisoned)
                    {
                        troop.Garrisoned = false;
                    }
                    selectedHabitat.Troops.Remove(troop);
                    builtObject.Troops.Add(troop);
                    troop.Colony = null;
                    troop.BuiltObject = builtObject;
                    ctlColonyCharacterTroops.BindData(selectedHabitat.Empire, selectedHabitat.Characters, selectedHabitat.InvadingCharacters, selectedHabitat.Troops, selectedHabitat.TroopsToRecruit, selectedHabitat.InvadingTroops, characterImageCache_0);
                    method_427();
                }
            }
            else
            {
                if (!(selectedObject is Character))
                {
                    return;
                }
                Character character = (Character)selectedObject;
                BuiltObject builtObject2 = method_428();
                if (builtObject2 != null && builtObject2.Troops != null && builtObject2.Owner == _Game.PlayerEmpire)
                {
                    Habitat selectedHabitat2 = UnlxwvByxj.SelectedHabitat;
                    if (selectedHabitat2 != null && selectedHabitat2.Empire == _Game.PlayerEmpire && selectedHabitat2.Characters != null && selectedHabitat2.Characters.Contains(character) && character.Empire == _Game.PlayerEmpire && character.TransferTimeRemaining <= 0f && character.TransferDestination == null)
                    {
                        character.CompleteLocationTransfer(builtObject2, _Game.Galaxy);
                        ctlColonyCharacterTroops.BindData(selectedHabitat2.Empire, selectedHabitat2.Characters, selectedHabitat2.InvadingCharacters, selectedHabitat2.Troops, selectedHabitat2.TroopsToRecruit, selectedHabitat2.InvadingTroops, characterImageCache_0);
                        method_427();
                    }
                }
            }
        }

        private void method_427()
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat == null)
            {
                return;
            }
            BuiltObjectList nearbyBuiltObjects = _Game.Galaxy.GetNearbyBuiltObjects(selectedHabitat.Xpos, selectedHabitat.Ypos, 1000.0);
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < nearbyBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = nearbyBuiltObjects[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Empire == _Game.PlayerEmpire && builtObject.TroopCapacityRemaining >= 100)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            cmbColonyTroopTransferTransport.Items.Clear();
            for (int j = 0; j < builtObjectList.Count; j++)
            {
                cmbColonyTroopTransferTransport.Items.Add(builtObjectList[j]);
            }
            if (cmbColonyTroopTransferTransport.Items.Count > 0)
            {
                cmbColonyTroopTransferTransport.SelectedIndex = 0;
            }
        }

        private BuiltObject method_428()
        {
            BuiltObject result = null;
            int selectedIndex = cmbColonyTroopTransferTransport.SelectedIndex;
            if (selectedIndex >= 0)
            {
                object obj = cmbColonyTroopTransferTransport.Items[selectedIndex];
                if (obj is BuiltObject)
                {
                    result = (BuiltObject)obj;
                }
            }
            return result;
        }

        private void btnColonyFacilityBuild_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            PlanetaryFacilityDefinition selectedPlanetaryFacility = cmbColonyFacilitiesToBuild.SelectedPlanetaryFacility;
            if (selectedHabitat == null || selectedPlanetaryFacility == null)
            {
                return;
            }
            double num = Galaxy.CalculatePlanetaryFacilityCost(selectedPlanetaryFacility, _Game.PlayerEmpire);
            if (!(_Game.PlayerEmpire.StateMoney >= num))
            {
                return;
            }
            if (selectedPlanetaryFacility.Type == PlanetaryFacilityType.Wonder)
            {
                if (selectedHabitat.QueueWonderConstruction(selectedPlanetaryFacility))
                {
                    _Game.PlayerEmpire.StateMoney -= num;
                    _Game.PlayerEmpire.PirateEconomy.PerformExpense(num, PirateExpenseType.FacilityConstruction, _Game.Galaxy.CurrentStarDate);
                    ctlColonyFacilities.BindData(_Game.Galaxy, selectedHabitat.Facilities, selectedHabitat);
                    cmbColonyFacilitiesToBuild.BindData(_Game.PlayerEmpire, selectedHabitat.ResolveBuildableFacilities(), bitmap_8);
                }
                return;
            }
            bool flag = true;
            PirateColonyControl pirateColonyControl = null;
            if (selectedPlanetaryFacility.Type == PlanetaryFacilityType.PirateBase || selectedPlanetaryFacility.Type == PlanetaryFacilityType.PirateFortress || selectedPlanetaryFacility.Type == PlanetaryFacilityType.PirateCriminalNetwork)
            {
                flag = false;
                pirateColonyControl = selectedHabitat.GetPirateControl().GetByFacilityControl();
                if (pirateColonyControl != null && pirateColonyControl.EmpireId == _Game.PlayerEmpire.EmpireId)
                {
                    flag = true;
                    if (selectedPlanetaryFacility.Type == PlanetaryFacilityType.PirateCriminalNetwork)
                    {
                        flag = false;
                        PlanetaryFacility planetaryFacility = selectedHabitat.Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
                        if (planetaryFacility != null && planetaryFacility.Type == PlanetaryFacilityType.PirateFortress && pirateColonyControl.HasFacilityControl && _Game.PlayerEmpire.CountPirateCriminalNetworks() <= 0)
                        {
                            flag = true;
                        }
                    }
                }
                else
                {
                    pirateColonyControl = selectedHabitat.GetPirateControl().GetByFaction(_Game.PlayerEmpire);
                    float num2 = 0.5f;
                    bool flag2 = true;
                    if (selectedPlanetaryFacility.Type == PlanetaryFacilityType.PirateFortress)
                    {
                        num2 = 1f;
                    }
                    if (selectedPlanetaryFacility.Type == PlanetaryFacilityType.PirateCriminalNetwork)
                    {
                        flag2 = false;
                        num2 = 1f;
                        PlanetaryFacility planetaryFacility2 = selectedHabitat.Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
                        if (planetaryFacility2 != null && planetaryFacility2.Type == PlanetaryFacilityType.PirateFortress && pirateColonyControl.HasFacilityControl && _Game.PlayerEmpire.CountPirateCriminalNetworks() <= 0)
                        {
                            flag2 = true;
                        }
                    }
                    if (pirateColonyControl != null && pirateColonyControl.ControlLevel >= num2 && flag2)
                    {
                        flag = true;
                    }
                }
            }
            if (flag && selectedHabitat.QueueFacilityConstruction(selectedPlanetaryFacility.Type))
            {
                if (pirateColonyControl != null)
                {
                    pirateColonyControl.HasFacilityControl = true;
                }
                _Game.PlayerEmpire.StateMoney -= num;
                _Game.PlayerEmpire.PirateEconomy.PerformExpense(num, PirateExpenseType.FacilityConstruction, _Game.Galaxy.CurrentStarDate);
                ctlColonyFacilities.BindData(_Game.Galaxy, selectedHabitat.Facilities, selectedHabitat);
                cmbColonyFacilitiesToBuild.BindData(_Game.PlayerEmpire, selectedHabitat.ResolveBuildableFacilities(), bitmap_8);
            }
        }

        private void btnColonyFacilityScrap_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            PlanetaryFacility selectedFacility = ctlColonyFacilities.SelectedFacility;
            if (selectedHabitat == null || selectedFacility == null || selectedHabitat.Facilities == null || !selectedHabitat.Facilities.Contains(selectedFacility))
            {
                return;
            }
            if (selectedHabitat.CheckFacilityOwnedByColonyOwner(selectedFacility))
            {
                if (selectedHabitat.Owner == Main.Main._Game.PlayerEmpire)
                {
                    MessageBoxEx messageBoxEx = method_372("Are you sure that you want to scrap the " + selectedFacility.Name + " at your colony " + selectedHabitat.Name + "?", "Scrap Facility");
                    if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
                    {
                        selectedHabitat.Facilities.Remove(selectedFacility);
                        selectedHabitat.CheckRemoveFacilityTracking(selectedFacility);
                        selectedHabitat.ReviewPlanetaryFacilities(_Game.PlayerEmpire);
                        ctlColonyFacilities.BindData(_Game.Galaxy, selectedHabitat.Facilities, selectedHabitat);
                        cmbColonyFacilitiesToBuild.BindData(_Game.PlayerEmpire, selectedHabitat.ResolveBuildableFacilities(), bitmap_8);
                    }
                }
                else
                {
                    MessageBoxEx messageBoxEx = method_371("You don't own " + selectedFacility.Name + " facility at colony " + selectedHabitat.Name, "Scrap Facility", MessageBoxExIcon.Exclamation);
                    messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture);
                }
            }
            else
            {
                selectedHabitat.InitiateAttackAgainstPirateFacilities(_Game.PlayerEmpire, selectedFacility);
            }
        }

        private void btnShipGroupInfoSetHomeColony_Click(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup == null || cmbShipGroupInfoHomeColony.SelectedItem == null)
            {
                return;
            }
            string text = cmbShipGroupInfoHomeColony.SelectedItem.ToString();
            if (!(text != "(" + TextResolver.GetText("Select new home colony") + ")"))
            {
                return;
            }
            foreach (Habitat colony in _Game.PlayerEmpire.Colonies)
            {
                if (colony.Name == text)
                {
                    selectedShipGroup.GatherPoint = colony;
                    method_268(selectedShipGroup);
                    break;
                }
            }
        }

        private void btnDesignsUpgrade_Click(object sender, EventArgs e)
        {
            BaconMain.btnDesignsUpgrade_Click(this, sender, e);
        }

        private string method_429(GameEndOutcome gameEndOutcome_1)
        {
            string result = string.Empty;
            string[] array = new string[20]
            {
            TextResolver.GetText("Congratulations to our new galactic ruler!"),
            TextResolver.GetText("I knew you were going to win"),
            TextResolver.GetText("This is outrageous! How could I have been beaten?!"),
            TextResolver.GetText("You must have cheated!"),
            TextResolver.GetText("I'll get you next time!"),
            TextResolver.GetText("You're the big chief now"),
            TextResolver.GetText("Joyful greetings to our new supreme ruler!"),
            TextResolver.GetText("You won!"),
            TextResolver.GetText("You beat us all"),
            TextResolver.GetText("You're a tough competitor"),
            TextResolver.GetText("Welcome to our new Galactic Emperor!"),
            TextResolver.GetText("Arrgghhh! How could YOU beat ME?!"),
            TextResolver.GetText("I can't take it! I don't handle defeat well"),
            TextResolver.GetText("Can you go easy on us next time?"),
            TextResolver.GetText("Did you cheat? I think you cheated..."),
            TextResolver.GetText("Hey wait, I was just getting started!"),
            TextResolver.GetText("What? Are we finished already?"),
            TextResolver.GetText("Your enemies tremble before your mighty fleets!"),
            TextResolver.GetText("Hey, do you need a deputy emperor? I'm right here, no problem"),
            TextResolver.GetText("You know being galactic emperor isn't all fun and games, we expect to see results!")
            };
            string[] array2 = new string[21]
            {
            TextResolver.GetText("Better luck next time"),
            TextResolver.GetText("Your failure is complete!"),
            TextResolver.GetText("You have failed!"),
            TextResolver.GetText("Your breath is smellier than a Teekan's armpit!"),
            TextResolver.GetText("Failure teaches valuable lessons - you'll do better next time"),
            TextResolver.GetText("May you be chased by a thousand hungry Kaltors!"),
            TextResolver.GetText("Defeat suits you well"),
            TextResolver.GetText("Your oppression will never return!"),
            TextResolver.GetText("You lost - ha ha!"),
            TextResolver.GetText("How could you lose when you had such a big headstart?"),
            TextResolver.GetText("Lost again? Maybe you should find a smaller galaxy..."),
            TextResolver.GetText("Perhaps galactic conquest just isn't your thing..."),
            TextResolver.GetText("You move slower than a blind space slug!"),
            TextResolver.GetText("Want to try again?"),
            TextResolver.GetText("How much failure can you take?"),
            TextResolver.GetText("Explore and Expand, NOT Shrink and Collapse!"),
            TextResolver.GetText("You don't even qualify to be in the same galaxy as us!"),
            TextResolver.GetText("The beginner's galaxy is back that way... See you later!"),
            TextResolver.GetText("You're fired!"),
            TextResolver.GetText("Maybe you should switch careers: asteroid miner would suit you better"),
            TextResolver.GetText("Ever thought of trying Solitaire instead?")
            };
            string[] array3 = new string[5]
            {
            TextResolver.GetText("Rematch!"),
            TextResolver.GetText("Let's try again"),
            TextResolver.GetText("We were too good for you!"),
            TextResolver.GetText("You fought us to a standstill"),
            TextResolver.GetText("Shall we have another go?")
            };
            switch (gameEndOutcome_1)
            {
                case GameEndOutcome.Victory:
                    result = array[Galaxy.Rnd.Next(0, array.Length)];
                    break;
                case GameEndOutcome.Defeat:
                    result = array2[Galaxy.Rnd.Next(0, array2.Length)];
                    break;
                case GameEndOutcome.Stalemate:
                    result = array3[Galaxy.Rnd.Next(0, array3.Length)];
                    break;
            }
            return result;
        }

        private DistantWorlds.Types.EmpireList method_430(int int_64, Empire empire_5)
        {
            DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
            int[] array = new int[_Game.Galaxy.Empires.Count];
            for (int i = 0; i < _Game.Galaxy.Empires.Count; i++)
            {
                if (i < _Game.Galaxy.Empires.Count)
                {
                    array[i] = _Game.Galaxy.Empires[i].TotalColonyStrategicValue;
                }
            }
            Empire[] array2 = _Game.Galaxy.Empires.ToArray();
            Array.Sort(array, array2);
            if (empire_5 != _Game.PlayerEmpire)
            {
                empireList.Add(empire_5);
            }
            int_64 -= empireList.Count;
            if (int_64 >= array2.Length)
            {
                int_64 = array2.Length - 1;
            }
            for (int j = 0; j < int_64; j++)
            {
                if (j < array2.Length)
                {
                    if (array2[j] != empire_5 && array2[j] != _Game.PlayerEmpire)
                    {
                        empireList.Add(array2[j]);
                    }
                    else
                    {
                        int_64++;
                    }
                }
            }
            return empireList;
        }

        private void timer_2_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (GameEndMessage != null)
            {
                Invoke(GameEndMessage, null);
            }
        }

        private void method_431()
        {
            string text = method_429(gameEndOutcome_0);
            lblGameEndComment.Text = text;
            lblGameEndCommentDropShadow.Text = text;
            lblGameEndComment.BringToFront();
            List<Point> list = method_434();
            int num = -1;
            while (num < 0 || empireList_0[num] == _Game.PlayerEmpire)
            {
                num = Galaxy.Rnd.Next(0, empireList_0.Count);
            }
            Bitmap image = new Bitmap(250, 120, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(image);
            int num2 = 65 - (int)(graphics.MeasureString(text, lblGameEndComment.Font, new SizeF(200f, 100f), StringFormat.GenericTypographic).Width / 2f);
            lblGameEndComment.Parent = picGameEndPicture;
            lblGameEndComment.Location = new Point(list[num].X + num2, list[num].Y + 60);
            lblGameEndCommentDropShadow.Location = new Point(list[num].X + num2 + 1, list[num].Y + 60 + 1);
        }

        private Bitmap method_432(Empire empire_5, Empire empire_6)
        {
            return method_433(empire_5, empire_6, 130, 60);
        }

        private Bitmap method_433(Empire empire_5, Empire empire_6, int int_64, int int_65)
        {
            int num = 10;
            Bitmap bitmap = new Bitmap(int_64, int_65, PixelFormat.Format32bppPArgb);
            if (empire_5 == empire_6)
            {
                bitmap = new Bitmap(int_64 + num * 2, int_65 + num * 2, PixelFormat.Format32bppPArgb);
            }
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (empire_5 == empire_6)
            {
                Color baseColor = Color.FromArgb(0, 0, 255);
                for (int i = 0; i < num; i++)
                {
                    double num2 = 128.0;
                    int val = (int)((double)(i + 1) * (num2 / (double)num));
                    val = Math.Max(0, Math.Min(255, val));
                    Color color = Color.FromArgb(val, baseColor);
                    SolidBrush brush = new SolidBrush(color);
                    Rectangle rect = new Rectangle(i, i, int_65 + num * 2 - i * 2, int_65 + num * 2 - i * 2);
                    graphics.FillRectangle(brush, rect);
                    rect = new Rectangle(int_65 + num + i, (int_65 - (int)((double)int_65 * 0.6)) / 2 + i, int_65 + num * 2 - i * 2, (int)((double)int_65 * 0.6) + num * 2 - i * 2);
                    graphics.FillRectangle(brush, rect);
                }
            }
            Bitmap empireDominantRaceImage = raceImageCache_0.GetEmpireDominantRaceImage(empire_5);
            Rectangle srcRect = new Rectangle(0, 0, empireDominantRaceImage.Width, empireDominantRaceImage.Height);
            Rectangle destRect = new Rectangle(0, 0, int_65, int_65);
            if (empire_5 == empire_6)
            {
                destRect = new Rectangle(num, num, int_65, int_65);
            }
            graphics.DrawImage(empireDominantRaceImage, destRect, srcRect, GraphicsUnit.Pixel);
            Bitmap largeFlagPicture = empire_5.LargeFlagPicture;
            srcRect = new Rectangle(0, 0, largeFlagPicture.Width, largeFlagPicture.Height);
            destRect = new Rectangle(int_65 + 10, (int_65 - (int)((double)int_65 * 0.6)) / 2, int_65, (int)((double)int_65 * 0.6));
            if (empire_5 == empire_6)
            {
                destRect = new Rectangle(int_65 + 10 + num, (int_65 - (int)((double)int_65 * 0.6)) / 2 + num, int_65, (int)((double)int_65 * 0.6));
            }
            graphics.DrawImage(largeFlagPicture, destRect, srcRect, GraphicsUnit.Pixel);
            string empty = string.Empty;
            int num3 = 0;
            if (empire_5 == empire_6)
            {
                empty = TextResolver.GetText("WINNER!");
                num3 = num;
            }
            else if (empire_5 == _Game.PlayerEmpire)
            {
                _ = _Game.PlayerEmpire;
            }
            if (!string.IsNullOrEmpty(empty))
            {
                SolidBrush brush2 = new SolidBrush(Color.FromArgb(160, Color.Red));
                SolidBrush brush3 = new SolidBrush(Color.FromArgb(160, Color.Black));
                Font font = ((IFontCache)this).GenerateFont(22f, isBold: true);
                SizeF sizeF = graphics.MeasureString(empty, font, 400, StringFormat.GenericTypographic);
                int num4 = (num3 + (int_64 + 10) - (int)sizeF.Width) / 2;
                int num5 = num3 + int_65 / 2 - (int)sizeF.Height / 2;
                Point point = new Point(num4 + 1, num5 + 1);
                graphics.DrawString(empty, font, brush3, point, StringFormat.GenericTypographic);
                Point point2 = new Point(num4, num5);
                graphics.DrawString(empty, font, brush2, point2, StringFormat.GenericTypographic);
            }
            return bitmap;
        }

        private List<Point> method_434()
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(40, 40));
            list.Add(new Point(60, 180));
            list.Add(new Point(80, 320));
            list.Add(new Point(680, 40));
            list.Add(new Point(660, 180));
            list.Add(new Point(640, 320));
            return list;
        }

        private void method_435(Graphics graphics_3, DistantWorlds.Types.EmpireList empireList_1, Empire empire_5)
        {
            List<Point> list = method_434();
            for (int i = 0; i < empireList_1.Count; i++)
            {
                Empire empire = empireList_1[i];
                if (empire != null)
                {
                    Bitmap image = method_432(empire, empire_5);
                    Point point = list[i];
                    if (empire == empire_5)
                    {
                        point.X -= 10;
                        point.Y -= 10;
                    }
                    graphics_3.DrawImage(image, point);
                }
            }
        }

        private void method_436(GameEndEventArgs gameEndEventArgs_0)
        {
            _Game.Galaxy.ReviewAchievements();
            GameSummary gameSummary = _Game.Galaxy.DetermineGameSummary();
            if (gameEndEventArgs_0.OutcomeForPlayer == GameEndOutcome.Victory)
            {
                gameSummary.PlayerVictory = true;
            }
            _Game.Galaxy.GameSummary = gameSummary;
            gameSummaryList_0.Add(gameSummary);
            for (int i = 0; i < gameSummary.PlayerAchievements.Count; i++)
            {
                gameSummaryList_0.PlayerAchievements.AddIfNotExistsOrBetter(gameSummary.PlayerAchievements[i]);
            }
            method_400();
            tabEmpireComparisonGraphs.SelectedIndex = 1;
            List<string> list = new List<string>();
            if (gameEndEventArgs_0.OutcomeForPlayer == GameEndOutcome.Victory)
            {
                list.Add(TextResolver.GetText("Victory").ToUpper(CultureInfo.InvariantCulture) + "!");
            }
            else if (gameEndEventArgs_0.OutcomeForPlayer == GameEndOutcome.Defeat)
            {
                list.Add(TextResolver.GetText("Defeat").ToUpper(CultureInfo.InvariantCulture) + "!");
            }
            list.Add(" ");
            list.Add(gameEndEventArgs_0.Description);
            pnlGameSummary.OverlayTextLines = list;
            if (gameEndEventArgs_0.VictorEmpire != null)
            {
                pnlGameSummary.CurrentGameSelectedFaction = gameEndEventArgs_0.VictorEmpire;
            }
            method_255();
            if (gameEndEventArgs_0.OutcomeForPlayer == GameEndOutcome.Defeat && (_Game.PlayerEmpire.Colonies.Count <= 0 || !_Game.PlayerEmpire.Active))
            {
                btnGameEndContinue.Enabled = false;
            }
        }

        private void method_437()
        {
            pnlGameEnd.SendToBack();
            pnlGameEnd.Visible = false;
            timer_2.Stop();
        }

        private void btnGameEndContinue_Click(object sender, EventArgs e)
        {
            method_437();
            method_155();
        }

        private void btnGameEndExit_Click(object sender, EventArgs e)
        {
            method_92();
            bool_4 = true;
            _Game.SelectedObject = null;
            int_28 = 0;
            int_29 = 0;
            method_437();
            pnlDetailInfo.ClearData();
            pnlHabitatInfo.ClearData();
            hoverPanel_0.ClearData();
            pnlBuiltObjectDetail.ClearData();
            pnlColonyHabitatInfo.ClearData();
            picSystem.Extinguish();
            picSystemMap.Extinguish();
            method_357();
            method_3();
            method_64();
        }

        private void method_438()
        {
            Rectangle rectangle_ = new Rectangle(10, 160, 360, 360);
            method_439(rectangle_);
        }

        private void method_439(Rectangle rectangle_2)
        {
            pnlTutorial.Size = new Size(rectangle_2.Width, rectangle_2.Height);
            pnlTutorial.Location = new Point(rectangle_2.Left, rectangle_2.Top);
            lblTutorialTitle.Font = font_1;
            lblTutorialText.Font = font_8;
            lblTutorialTitle.Location = new Point(10, 10);
            lblTutorialTitle.Text = TextResolver.GetText("Distant Worlds Tutorial");
            lblTutorialText.Location = new Point(10, 40);
            lblTutorialText.MaximumSize = new Size(rectangle_2.Width - 20, rectangle_2.Height - 95);
            btnTutorialContinue.Size = new Size(rectangle_2.Width - 30, 35);
            btnTutorialContinue.Font = font_2;
            btnTutorialContinue.Location = new Point(15, rectangle_2.Height - 50);
            btnTutorialExit.Size = new Size((rectangle_2.Width - 40) / 2, 35);
            btnTutorialExit.Font = font_2;
            btnTutorialExit.Location = new Point(15, rectangle_2.Height - 50);
            btnTutorialExit.Visible = false;
            lblTutorialTitle.Size = new Size(pnlTutorial.Width - 20, 30);
            lblTutorialText.Size = new Size(pnlTutorial.Width - 20, pnlTutorial.Height - 50);
            lblTutorialTitle.Enabled = false;
            lblTutorialText.Enabled = false;
            pnlTutorial.Visible = true;
            pnlTutorial.BringToFront();
        }

        private void method_440()
        {
            pnlTutorial.Visible = false;
            pnlTutorial.SendToBack();
        }

        private void method_441()
        {
            method_442(null);
        }

        private void method_442(System.Windows.Forms.Panel panel_1)
        {
            if (panel_1 != pnlBuiltObjectInfo)
            {
                method_185();
            }
            if (panel_1 != pnlColonyInfo)
            {
                method_186();
            }
            if (panel_1 != pnlDesignDetail)
            {
                method_293();
            }
            if (panel_1 != pnlDesigns)
            {
                method_308();
            }
            if (panel_1 != pnlDiplomacyTalk)
            {
                method_294();
            }
            if (panel_1 != pnlEmpirePolicy)
            {
                method_596();
            }
            if (panel_1 != pnlBuildOrder)
            {
                method_639();
            }
            if (panel_1 != pnlCharacterEventHistory)
            {
                method_663();
            }
            if (panel_1 != pnlAdvisorSuggestion)
            {
                method_660();
            }
            if (panel_1 != vHfFsoqMev)
            {
                method_401();
            }
            if (panel_1 != pnlEmpireInfo)
            {
                method_194();
            }
            if (panel_1 != pnlEmpireSummary)
            {
                method_275();
            }
            if (panel_1 != CaLkaMyrMQ)
            {
                method_132();
            }
            if (panel_1 != pnlGameEnd)
            {
                method_437();
            }
            if (panel_1 != pnlGameMenu)
            {
                method_357();
            }
            if (panel_1 != pnlGameOptions)
            {
                pnlGameOptions.SendToBack();
                pnlGameOptions.Visible = false;
            }
            if (panel_1 != kYdDyYeMls)
            {
                method_426();
            }
            if (panel_1 != pnlResearch)
            {
                method_399();
            }
            if (panel_1 != pnlSaveLoadProgress)
            {
                method_382();
            }
            if (panel_1 != pnlShipGroupInfo)
            {
                method_271();
            }
            if (panel_1 != pnlTroopInfo)
            {
                method_184();
            }
            if (panel_1 != pnlEncyclopedia)
            {
                method_457();
            }
            if (panel_1 != pnlExpansionPlanner)
            {
                method_162();
            }
            bool_9 = false;
        }

        private void method_443(TutorialItemList tutorialItemList_0)
        {
            method_154();
            Tutorial tutorial = new Tutorial();
            tutorial.Items = tutorialItemList_0;
            tutorial_0 = tutorial;
            btnTutorialContinue.Text = TextResolver.GetText("Continue");
            method_455();
            Rectangle rectangle_ = new Rectangle(mainView.Width - 390, 160, 380, 465);
            method_439(rectangle_);
        }

        private void method_444(System.Windows.Forms.Panel panel_1)
        {
            if (panel_1 == pnlBuiltObjectInfo)
            {
                method_177(null);
            }
            if (panel_1 == pnlColonyInfo)
            {
                method_166(null);
            }
            if (panel_1 == pnlBuildOrder)
            {
                method_628();
            }
            if (panel_1 == pnlCharacterEventHistory)
            {
                method_661();
            }
            if (panel_1 == pnlAdvisorSuggestion)
            {
                method_648();
            }
            if (panel_1 == pnlDesignDetail)
            {
                OpenDesignEditor(_Game.PlayerEmpire.Designs[0]);
            }
            if (panel_1 == pnlEmpirePolicy)
            {
                method_595();
            }
            if (panel_1 == pnlDesigns)
            {
                method_307(null);
            }
            if (panel_1 == vHfFsoqMev)
            {
                method_400();
            }
            if (panel_1 == pnlEmpireInfo)
            {
                method_195(null);
            }
            if (panel_1 == pnlEmpireSummary)
            {
                method_274();
            }
            if (panel_1 == CaLkaMyrMQ)
            {
                method_131(_Game.PlayerEmpire.Capital);
            }
            if (panel_1 == pnlGameOptions)
            {
                method_402();
            }
            if (panel_1 == kYdDyYeMls)
            {
                method_424(null);
            }
            if (panel_1 == pnlResearch)
            {
                method_393(IndustryType.Weapon);
            }
            if (panel_1 == pnlShipGroupInfo)
            {
                method_268(null);
            }
            if (panel_1 == pnlTroopInfo)
            {
                method_172(null);
            }
            if (panel_1 == pnlExpansionPlanner)
            {
                method_160("resourcesyou");
            }
        }

        private TutorialItemList method_445(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Pirates"].HighlightObject = pnlTutorial;
            tutorialItemList_0["Pirates - Putting Them To Work"].SelectionObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["Pirates - Putting Them To Work"].ZoomScrollObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["Pirates - Putting Them To Work"].ZoomLevel = 1.0;
            tutorialItemList_0["Pirates - Putting Them To Work"].HighlightObject = itemListCollectionPanel_0;
            tutorialItemList_0["Pirates - Putting Them To Work"].HighlightEmpireNavigationToolPanelTitle = "Pirate Missions";
            tutorialItemList_0["Pirates - Putting Them To Work"].HighlightOpenEmpireNavigationToolPanel = true;
            tutorialItemList_0["Pirates - Putting Them To Work"].HighlightActionButtonNumber = 5;
            BuiltObject builtObject = null;
            Empire empire = null;
            PirateRelationList relationsByType = _Game.PlayerEmpire.PirateRelations.GetRelationsByType(PirateRelationType.None);
            if (relationsByType != null && relationsByType.Count > 0)
            {
                empire = relationsByType[0].OtherEmpire;
            }
            else
            {
                Empire empire2 = _Game.Galaxy.FindNearestPirateFaction(_Game.PlayerEmpire.Colonies[0].Xpos, _Game.PlayerEmpire.Colonies[0].Ypos, _Game.PlayerEmpire, includeSuperPirates: false);
                if (empire2 != null)
                {
                    _Game.PlayerEmpire.ChangePirateRelation(empire2, PirateRelationType.None, _Game.Galaxy.CurrentStarDate);
                    empire = empire2;
                }
            }
            if (empire != null)
            {
                Habitat habitat = null;
                if (empire.PirateEmpireBaseHabitat != null)
                {
                    habitat = _Game.Galaxy.FindNearestHabitatEmptySystem(empire.PirateEmpireBaseHabitat.Xpos, empire.PirateEmpireBaseHabitat.Ypos, HabitatType.GasGiant);
                }
                else if (empire.Capital != null)
                {
                    habitat = _Game.Galaxy.FindNearestHabitatEmptySystem(empire.Capital.Xpos, empire.Capital.Ypos, HabitatType.GasGiant);
                }
                if (habitat != null)
                {
                    Design design = empire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.GasMiningStation);
                    if (design != null)
                    {
                        builtObject = empire.GenerateNewBuiltObject(design, habitat, habitat.Xpos, habitat.Ypos);
                    }
                }
            }
            if (builtObject != null && builtObject.NearestSystemStar != null)
            {
                _Game.PlayerEmpire.SetSystemVisibility(builtObject.NearestSystemStar, SystemVisibilityStatus.Visible);
                tutorialItemList_0["Pirates - Attack Missions"].SelectionObject = builtObject;
                tutorialItemList_0["Pirates - Attack Missions"].ZoomScrollObject = builtObject;
                tutorialItemList_0["Pirates - Attack Missions"].ZoomLevel = 1.0;
                tutorialItemList_0["Pirates - Attack Missions"].HighlightObject = itemListCollectionPanel_0;
                tutorialItemList_0["Pirates - Attack Missions"].HighlightEmpireNavigationToolPanelTitle = "Pirate Missions";
                tutorialItemList_0["Pirates - Attack Missions"].HighlightOpenEmpireNavigationToolPanel = true;
                tutorialItemList_0["Pirates - Attack Missions"].HighlightActionButtonNumber = 2;
            }
            tutorialItemList_0["Pirates - Defend Missions"].SelectionObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["Pirates - Defend Missions"].ZoomScrollObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["Pirates - Defend Missions"].ZoomLevel = 1.0;
            tutorialItemList_0["Pirates - Defend Missions"].HighlightObject = itemListCollectionPanel_0;
            tutorialItemList_0["Pirates - Defend Missions"].HighlightEmpireNavigationToolPanelTitle = "Pirate Missions";
            tutorialItemList_0["Pirates - Defend Missions"].HighlightOpenEmpireNavigationToolPanel = true;
            tutorialItemList_0["Pirates - Defend Missions"].HighlightActionButtonNumber = 4;
            tutorialItemList_0["Clearing out Pirates"].ZoomScrollObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["Clearing out Pirates"].ZoomLevel = 3000.0;
            tutorialItemList_0["Eradicating Pirates at your Colonies"].SelectionObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["Eradicating Pirates at your Colonies"].ZoomScrollObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["Eradicating Pirates at your Colonies"].ZoomLevel = 3000.0;
            tutorialItemList_0["Eradicating Pirates at your Colonies"].OpenScreen = pnlColonyInfo;
            tutorialItemList_0["Eradicating Pirates at your Colonies"].ScreenTabName = "tabColony_Facilities";
            tutorialItemList_0["Eradicating Pirates at your Colonies"].HighlightObject = tabColonyData;
            return tutorialItemList_0;
        }

        private TutorialItemList method_446(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Welcome"].HighlightObject = pnlTutorial;
            tutorialItemList_0["PreWarp Empire"].SelectionObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["PreWarp Empire"].HighlightObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["PreWarp Empire"].ZoomScrollObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["PreWarp Empire"].ZoomLevel = 1.0;
            tutorialItemList_0["Exploring Your Home System"].ZoomScrollObject = Galaxy.DetermineHabitatSystemStar(_Game.PlayerEmpire.Colonies[0]);
            tutorialItemList_0["Exploring Your Home System"].ZoomLevel = 50.0;
            ResourceDefinition byHighestPrevalence = _Game.Galaxy.ResourceSystem.StrategicResources.GetByHighestPrevalence();
            if (byHighestPrevalence != null)
            {
                Habitat habitat = _Game.Galaxy.FindNearestHabitatWithResource(_Game.PlayerEmpire.Colonies[0].Xpos, _Game.PlayerEmpire.Colonies[0].Ypos, byHighestPrevalence.ResourceID, _Game.PlayerEmpire.Colonies[0], null);
                if (habitat != null)
                {
                    _Game.PlayerEmpire.ResourceMap.SetResourcesKnown(habitat, known: true);
                    tutorialItemList_0["Mining"].SelectionObject = habitat;
                    tutorialItemList_0["Mining"].HighlightObject = habitat;
                    tutorialItemList_0["Mining"].ZoomScrollObject = habitat;
                    tutorialItemList_0["Mining"].ZoomLevel = 1.0;
                }
            }
            tutorialItemList_0["Research"].OpenScreen = pnlResearch;
            tutorialItemList_0["Breakthrough Technologies"].OpenScreen = pnlResearch;
            tutorialItemList_0["Breakthrough Technologies"].SelectionObject = null;
            tutorialItemList_0["Breakthrough Technologies"].HighlightObject = null;
            tutorialItemList_0["Breakthrough Technologies"].ZoomScrollObject = null;
            tutorialItemList_0["Expanding Across the Stars"].ZoomScrollObject = _Game.PlayerEmpire.Colonies[0];
            tutorialItemList_0["Expanding Across the Stars"].ZoomLevel = 3000.0;
            return tutorialItemList_0;
        }

        private TutorialItemList method_447(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Welcome"].HighlightObject = pnlTutorial;
            tutorialItemList_0["Welcome"].ZoomScrollObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Welcome"].ZoomLevel = 1.0;
            tutorialItemList_0["Pirates are different"].SelectionObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Pirates are different"].HighlightObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Pirate Spaceport"].SelectionObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Pirate Spaceport"].HighlightObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Construction Ships"].SelectionObject = _Game.PlayerEmpire.ConstructionShips[0];
            tutorialItemList_0["Construction Ships"].HighlightObject = _Game.PlayerEmpire.ConstructionShips[0];
            tutorialItemList_0["Construction Ships"].ZoomScrollObject = _Game.PlayerEmpire.ConstructionShips[0];
            tutorialItemList_0["Construction Ships"].ZoomLevel = 1.0;
            List<Control> list = new List<Control>();
            list.Add(pnlEmpireSummaryEconomy);
            tutorialItemList_0["Pirate Income"].OpenScreen = pnlEmpireSummary;
            tutorialItemList_0["Pirate Income"].HighlightObject = list;
            Habitat habitat = _Game.Galaxy.FindNearestIndependentHabitat(_Game.PlayerEmpire.PirateEmpireBaseHabitat.Xpos, _Game.PlayerEmpire.PirateEmpireBaseHabitat.Ypos);
            if (habitat != null && _Game.PlayerEmpire.CheckSystemExplored(habitat.SystemIndex))
            {
                habitat.SetPirateControl(_Game.PlayerEmpire, 0.75f);
                Habitat systemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                _Game.PlayerEmpire.SetSystemVisibility(systemStar, SystemVisibilityStatus.Visible);
                tutorialItemList_0["Controlled Colonies"].HighlightObject = habitat;
                tutorialItemList_0["Controlled Colonies"].SelectionObject = habitat;
                tutorialItemList_0["Controlled Colonies"].ZoomScrollObject = habitat;
                tutorialItemList_0["Controlled Colonies"].ZoomLevel = 1.0;
                tutorialItemList_0["Pirate Bases at Controlled Colonies"].HighlightObject = habitat;
                tutorialItemList_0["Pirate Bases at Controlled Colonies"].SelectionObject = habitat;
                tutorialItemList_0["Pirate Bases at Controlled Colonies"].ZoomScrollObject = habitat;
                tutorialItemList_0["Pirate Bases at Controlled Colonies"].ZoomLevel = 1.0;
            }
            tutorialItemList_0["Protection Agreements"].SelectionObject = null;
            tutorialItemList_0["Protection Agreements"].HighlightObject = null;
            tutorialItemList_0["Protection Agreements"].ZoomScrollObject = null;
            tutorialItemList_0["Protection Agreements"].OpenScreen = pnlEmpireInfo;
            tutorialItemList_0["Pirate Missions"].HighlightObject = itemListCollectionPanel_0;
            tutorialItemList_0["Pirate Missions"].HighlightOpenEmpireNavigationToolPanel = true;
            tutorialItemList_0["Pirate Missions"].HighlightEmpireNavigationToolPanelTitle = "Pirate Missions";
            tutorialItemList_0["Pirate Missions"].SelectionObject = null;
            tutorialItemList_0["Pirate Missions"].ZoomScrollObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Pirate Missions"].ZoomLevel = 1.0;
            tutorialItemList_0["Pirate Smuggling Missions"].HighlightObject = itemListCollectionPanel_0;
            tutorialItemList_0["Pirate Smuggling Missions"].HighlightOpenEmpireNavigationToolPanel = true;
            tutorialItemList_0["Pirate Smuggling Missions"].HighlightEmpireNavigationToolPanelTitle = "Pirate Missions";
            tutorialItemList_0["Pirate Smuggling Missions"].HighlightActionButtonNumber = 7;
            tutorialItemList_0["Pirate Smuggling Missions"].SelectionObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Pirate Attack Missions"].HighlightObject = itemListCollectionPanel_0;
            tutorialItemList_0["Pirate Attack Missions"].HighlightOpenEmpireNavigationToolPanel = true;
            tutorialItemList_0["Pirate Attack Missions"].HighlightEmpireNavigationToolPanelTitle = "Pirate Missions";
            tutorialItemList_0["Pirate Attack Missions"].SelectionObject = null;
            tutorialItemList_0["Pirate Defense Missions"].HighlightObject = itemListCollectionPanel_0;
            tutorialItemList_0["Pirate Defense Missions"].HighlightOpenEmpireNavigationToolPanel = true;
            tutorialItemList_0["Pirate Defense Missions"].HighlightEmpireNavigationToolPanelTitle = "Pirate Missions";
            tutorialItemList_0["Pirate Defense Missions"].SelectionObject = null;
            BuiltObject builtObject = null;
            if (_Game.PlayerEmpire != null)
            {
                Habitat habitat2 = _Game.Galaxy.FindNearestHabitatEmptySystem(_Game.PlayerEmpire.PirateEmpireBaseHabitat.Xpos, _Game.PlayerEmpire.PirateEmpireBaseHabitat.Ypos, HabitatType.GasGiant);
                if (habitat2 != null)
                {
                    Design design = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.GasMiningStation);
                    if (design != null)
                    {
                        builtObject = _Game.PlayerEmpire.GenerateNewBuiltObject(design, habitat2, habitat2.Xpos, habitat2.Ypos);
                    }
                }
            }
            if (builtObject != null)
            {
                _Game.PlayerEmpire.SetSystemVisibility(builtObject.NearestSystemStar, SystemVisibilityStatus.Visible);
                _Game.PlayerEmpire.ResourceMap.SetResourcesKnown(builtObject.ParentHabitat, known: true);
                tutorialItemList_0["Mining"].SelectionObject = builtObject;
                tutorialItemList_0["Mining"].HighlightObject = builtObject;
                tutorialItemList_0["Mining"].ZoomScrollObject = builtObject;
                tutorialItemList_0["Mining"].ZoomLevel = 1.0;
            }
            if (habitat != null)
            {
                tutorialItemList_0["Raids"].SelectionObject = habitat;
                tutorialItemList_0["Raids"].HighlightObject = habitat;
                tutorialItemList_0["Raids"].ZoomScrollObject = habitat;
                tutorialItemList_0["Raids"].ZoomLevel = 1.0;
            }
            BuiltObject builtObject2 = null;
            BuiltObject builtObject3 = null;
            BuiltObject builtObject4 = null;
            BuiltObject builtObject5 = null;
            BuiltObject builtObject6 = null;
            Empire empire = null;
            PirateRelationList relationsByType = _Game.PlayerEmpire.PirateRelations.GetRelationsByType(PirateRelationType.None);
            if (relationsByType != null && relationsByType.Count > 0)
            {
                empire = relationsByType[0].OtherEmpire;
            }
            else
            {
                Empire empire2 = _Game.Galaxy.FindNearestPirateFaction(_Game.PlayerEmpire.PirateEmpireBaseHabitat.Xpos, _Game.PlayerEmpire.PirateEmpireBaseHabitat.Ypos, _Game.PlayerEmpire, includeSuperPirates: false);
                if (empire2 != null)
                {
                    _Game.PlayerEmpire.ChangePirateRelation(empire2, PirateRelationType.None, _Game.Galaxy.CurrentStarDate);
                    empire = empire2;
                }
            }
            if (empire != null)
            {
                Habitat habitat3 = null;
                if (empire.PirateEmpireBaseHabitat != null)
                {
                    habitat3 = _Game.Galaxy.FindNearestHabitatEmptySystem(empire.PirateEmpireBaseHabitat.Xpos, empire.PirateEmpireBaseHabitat.Ypos, HabitatType.GasGiant);
                }
                else if (empire.Capital != null)
                {
                    habitat3 = _Game.Galaxy.FindNearestHabitatEmptySystem(empire.Capital.Xpos, empire.Capital.Ypos, HabitatType.GasGiant);
                }
                if (habitat3 != null)
                {
                    Design design2 = empire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.GasMiningStation);
                    if (design2 != null)
                    {
                        builtObject2 = empire.GenerateNewBuiltObject(design2, habitat3, habitat3.Xpos, habitat3.Ypos);
                    }
                }
            }
            if (builtObject2 != null)
            {
                Design design3 = _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.Frigate);
                if (design3 != null)
                {
                    double num = 0.0;
                    double num2 = 0.0;
                    _Game.Galaxy.SelectRelativePoint(400.0, out num, out num2);
                    builtObject3 = _Game.PlayerEmpire.GenerateNewBuiltObject(design3, null, builtObject2.Xpos + num, builtObject2.Ypos + num2);
                    _Game.Galaxy.SelectRelativePoint(400.0, out num, out num2);
                    builtObject4 = _Game.PlayerEmpire.GenerateNewBuiltObject(design3, null, builtObject2.Xpos + num, builtObject2.Ypos + num2);
                    _Game.Galaxy.SelectRelativePoint(400.0, out num, out num2);
                    builtObject5 = _Game.PlayerEmpire.GenerateNewBuiltObject(design3, null, builtObject2.Xpos + num, builtObject2.Ypos + num2);
                    _Game.Galaxy.SelectRelativePoint(400.0, out num, out num2);
                    builtObject6 = _Game.PlayerEmpire.GenerateNewBuiltObject(design3, null, builtObject2.Xpos + num, builtObject2.Ypos + num2);
                    builtObject3.AssignMission(BuiltObjectMissionType.Capture, builtObject2, null, BuiltObjectMissionPriority.High);
                    builtObject4.AssignMission(BuiltObjectMissionType.Capture, builtObject2, null, BuiltObjectMissionPriority.High);
                    builtObject5.AssignMission(BuiltObjectMissionType.Capture, builtObject2, null, BuiltObjectMissionPriority.High);
                    builtObject6.AssignMission(BuiltObjectMissionType.Capture, builtObject2, null, BuiltObjectMissionPriority.High);
                    tutorialItemList_0["Capturing Ships and Bases"].SelectionObject = builtObject3;
                    tutorialItemList_0["Capturing Ships and Bases"].ZoomScrollObject = builtObject3;
                    tutorialItemList_0["Capturing Ships and Bases"].ZoomLevel = 1.0;
                    _Game.PlayerEmpire.SetSystemVisibility(builtObject2.NearestSystemStar, SystemVisibilityStatus.Visible);
                }
                tutorialItemList_0["Boarding and Capture"].SelectionObject = builtObject2;
                tutorialItemList_0["Boarding and Capture"].ZoomScrollObject = builtObject2;
                tutorialItemList_0["Boarding and Capture"].ZoomLevel = 1.0;
                tutorialItemList_0["Boarding and Capture"].UnpauseGame = true;
            }
            if (builtObject3 != null && !builtObject3.HasBeenDestroyed)
            {
                tutorialItemList_0["Tractor Beams"].SelectionObject = builtObject3;
                tutorialItemList_0["Tractor Beams"].ZoomScrollObject = builtObject3;
                tutorialItemList_0["Tractor Beams"].ZoomLevel = 1.0;
            }
            tutorialItemList_0["Pirate Play Styles"].SelectionObject = null;
            tutorialItemList_0["Pirate Play Styles"].HighlightObject = null;
            tutorialItemList_0["Pirate Play Styles"].ZoomScrollObject = null;
            tutorialItemList_0["Pirate Victory Conditions"].OpenScreen = vHfFsoqMev;
            tutorialItemList_0["Pirate Victory Conditions"].ScreenTabName = "tabVictoryConditions";
            tutorialItemList_0["Pirate Criminal Network"].SelectionObject = habitat;
            tutorialItemList_0["Pirate Criminal Network"].ZoomScrollObject = habitat;
            tutorialItemList_0["Pirate Criminal Network"].ZoomLevel = 1.0;
            tutorialItemList_0["Defending against Attack"].SelectionObject = habitat;
            tutorialItemList_0["Defending against Attack"].ZoomScrollObject = habitat;
            tutorialItemList_0["Defending against Attack"].ZoomLevel = 1.0;
            tutorialItemList_0["Defending against Attack"].OpenScreen = pnlColonyInfo;
            tutorialItemList_0["Defending against Attack"].ScreenTabName = "tabColony_Facilities";
            tutorialItemList_0["Defending against Attack"].HighlightObject = tabColonyData;
            return tutorialItemList_0;
        }

        private TutorialItemList method_448(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Welcome"].HighlightObject = pnlTutorial;
            tutorialItemList_0["Welcome"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["Welcome"].ZoomLevel = 1.0;
            tutorialItemList_0["Galaxy Map"].HighlightObject = tbtnGalaxyMap;
            tutorialItemList_0["Galaxy Map - Select a System"].HighlightObject = gmapMain;
            tutorialItemList_0["Galaxy Map - Select a System"].OpenScreen = CaLkaMyrMQ;
            tutorialItemList_0["Galaxy Map - Select a planet"].HighlightObject = picSystemMap;
            tutorialItemList_0["Galaxy Map - Select a planet"].OpenScreen = CaLkaMyrMQ;
            tutorialItemList_0["Galaxy Map - Info Panel"].HighlightObject = pnlHabitatInfo;
            tutorialItemList_0["Galaxy Map - Info Panel"].OpenScreen = CaLkaMyrMQ;
            tutorialItemList_0["Galaxy Map - Planet Surface"].HighlightObject = pnlGalaxyMapHabitatPicture;
            tutorialItemList_0["Galaxy Map - Planet Surface"].OpenScreen = CaLkaMyrMQ;
            tutorialItemList_0["Galaxy Map - Selecting View"].HighlightObject = cmbGalaxyMapViewMode;
            tutorialItemList_0["Galaxy Map - Selecting View"].OpenScreen = CaLkaMyrMQ;
            tutorialItemList_0["Expansion Planner"].HighlightObject = btnExpansionPlanner;
            tutorialItemList_0["Expansion Planner - Resources"].HighlightObject = ctlExpansionPlannerResources;
            tutorialItemList_0["Expansion Planner - Resources"].OpenScreen = pnlExpansionPlanner;
            List<Control> list = new List<Control>();
            list.Add(cmbExpansionPlannerMode);
            list.Add(pnlExpansionPlannerTargetGroup);
            list.Add(gmapExpansionPlanner);
            tutorialItemList_0["Expansion Planner - Targets"].HighlightObject = list;
            tutorialItemList_0["Expansion Planner - Targets"].OpenScreen = pnlExpansionPlanner;
            tutorialItemList_0["Diplomacy & Other Empires"].HighlightObject = tbtnEmpires;
            tutorialItemList_0["Diplomacy - Influences"].HighlightObject = ctlEmpireDiplomaticRelationList;
            tutorialItemList_0["Diplomacy - Influences"].OpenScreen = pnlEmpireInfo;
            tutorialItemList_0["Diplomacy - Influences"].ListSelection = _Game.Galaxy.Empires[1];
            List<Control> list2 = new List<Control>();
            list2.Add(pnlEmpireDetailInfo);
            list2.Add(btnEmpireTalk);
            tutorialItemList_0["Diplomacy - Selected Empire"].HighlightObject = list2;
            tutorialItemList_0["Diplomacy - Selected Empire"].OpenScreen = pnlEmpireInfo;
            tutorialItemList_0["Diplomacy - Selected Empire"].ListSelection = _Game.Galaxy.Empires[1];
            tutorialItemList_0["Diplomacy - Conversations"].HighlightObject = ctlDiplomacyConversation;
            tutorialItemList_0["Diplomacy - Conversations"].OpenScreen = pnlDiplomacyTalk;
            tutorialItemList_0["Diplomacy - Conversations"].ListSelection = _Game.Galaxy.Empires[1];
            tutorialItemList_0["Trade Sanctions & Blockades"].OpenScreen = pnlEmpireInfo;
            tutorialItemList_0["Empire Comparisons"].HighlightObject = btnEmpireGraphs;
            tutorialItemList_0["Victory Conditions"].OpenScreen = vHfFsoqMev;
            tutorialItemList_0["Victory Conditions"].ScreenTabName = "tabVictoryConditions";
            tutorialItemList_0["Empire Comparison Graphs"].OpenScreen = vHfFsoqMev;
            tutorialItemList_0["Empire Comparison Graphs"].ScreenTabName = "tabPopulation";
            return tutorialItemList_0;
        }

        private TutorialItemList method_449(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Welcome"].HighlightObject = pnlTutorial;
            tutorialItemList_0["Welcome"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["Welcome"].ZoomLevel = 1.0;
            tutorialItemList_0["Research"].HighlightObject = tbtnResearch;
            tutorialItemList_0["Research Stations"].OpenScreen = pnlResearch;
            tutorialItemList_0["Designs"].HighlightObject = tbtnDesigns;
            tutorialItemList_0["Designs Screen"].OpenScreen = pnlDesigns;
            Design listSelection = null;
            foreach (Design design in _Game.PlayerEmpire.Designs)
            {
                if (design.SubRole == BuiltObjectSubRole.Frigate)
                {
                    listSelection = design;
                    break;
                }
            }
            tutorialItemList_0["Design Detail Screen"].OpenScreen = pnlDesignDetail;
            tutorialItemList_0["Design Detail Screen"].ListSelection = listSelection;
            tutorialItemList_0["Designs - Role and Tactics"].OpenScreen = pnlDesignDetail;
            tutorialItemList_0["Designs - Role and Tactics"].ListSelection = listSelection;
            tutorialItemList_0["Designs - Role and Tactics"].HighlightObject = pnlDesignBasics;
            tutorialItemList_0["Designs - Warnings"].OpenScreen = pnlDesignDetail;
            tutorialItemList_0["Designs - Warnings"].ListSelection = listSelection;
            tutorialItemList_0["Designs - Warnings"].HighlightObject = pnlDesignWarnings;
            tutorialItemList_0["Available Components"].OpenScreen = pnlDesignDetail;
            tutorialItemList_0["Available Components"].ListSelection = listSelection;
            tutorialItemList_0["Available Components"].HighlightObject = ctlDesignComponentToolbox;
            List<Control> list = new List<Control>();
            list.Add(pnlDesignComponentsHighlight);
            list.Add(btnAddComponentToDesign);
            list.Add(btnRemoveComponentFromDesign);
            tutorialItemList_0["Design Components"].OpenScreen = pnlDesignDetail;
            tutorialItemList_0["Design Components"].ListSelection = listSelection;
            tutorialItemList_0["Design Components"].HighlightObject = list;
            tutorialItemList_0["Designs - Component Detail"].OpenScreen = pnlDesignDetail;
            tutorialItemList_0["Designs - Component Detail"].ListSelection = listSelection;
            tutorialItemList_0["Designs - Component Detail"].HighlightObject = pnlDesignComponentDetail;
            tutorialItemList_0["Designs - Saving Your Design"].OpenScreen = pnlDesignDetail;
            tutorialItemList_0["Designs - Saving Your Design"].ListSelection = listSelection;
            tutorialItemList_0["Designs - Saving Your Design"].HighlightObject = btnDesignsSaveDesign;
            tutorialItemList_0["Construction - Main View"].HighlightObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Construction - Main View"].SelectionObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Construction - Main View"].ZoomScrollObject = _Game.PlayerEmpire.SpacePorts[0];
            tutorialItemList_0["Construction - Main View"].ZoomLevel = 1.0;
            List<Control> list2 = new List<Control>();
            list2.Add(tabBuiltObjectData);
            list2.Add(tbtnConstructionYards);
            tutorialItemList_0["Construction Yards screen"].HighlightObject = list2;
            tutorialItemList_0["Construction Yards screen"].OpenScreen = pnlBuiltObjectInfo;
            tutorialItemList_0["Construction Yards screen"].ScreenTabName = "tabBuiltObject_ConstructionYards";
            tutorialItemList_0["Build Order screen"].OpenScreen = pnlBuildOrder;
            tutorialItemList_0["Build Order screen"].HighlightObject = btnBuildOrder;
            return tutorialItemList_0;
        }

        private TutorialItemList method_450(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Welcome"].HighlightObject = pnlTutorial;
            tutorialItemList_0["Welcome"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["Welcome"].ZoomLevel = 1.0;
            BuiltObject builtObject = null;
            foreach (BuiltObject builtObject2 in _Game.PlayerEmpire.BuiltObjects)
            {
                if (builtObject2.Role == BuiltObjectRole.Military && builtObject2.BuiltAt == null && builtObject2.UnbuiltComponentCount == 0 && builtObject2.ShipGroup == null)
                {
                    builtObject = builtObject2;
                }
            }
            tutorialItemList_0["Fleets - Assigning Ships"].HighlightObject = builtObject;
            tutorialItemList_0["Fleets - Assigning Ships"].SelectionObject = builtObject;
            tutorialItemList_0["Fleets - Assigning Ships"].ZoomScrollObject = builtObject;
            tutorialItemList_0["Fleets - Assigning Ships"].ZoomLevel = 1.0;
            ShipGroup shipGroup = _Game.PlayerEmpire.ShipGroups[0];
            tutorialItemList_0["Fleets - Appearance"].HighlightObject = shipGroup;
            tutorialItemList_0["Fleets - Appearance"].SelectionObject = shipGroup;
            tutorialItemList_0["Fleets - Appearance"].ZoomScrollObject = shipGroup;
            tutorialItemList_0["Fleets - Appearance"].ZoomLevel = 1500.0;
            tutorialItemList_0["Fleets - Selecting"].HighlightObject = shipGroup;
            tutorialItemList_0["Fleets - Selecting"].ZoomScrollObject = shipGroup;
            tutorialItemList_0["Fleets - Selecting"].ZoomLevel = 1.0;
            tutorialItemList_0["Troops - Recruiting"].HighlightObject = tabColonyData;
            tutorialItemList_0["Troops - Recruiting"].OpenScreen = pnlColonyInfo;
            tutorialItemList_0["Troops - Recruiting"].ScreenTabName = "tabColony_Troops";
            tutorialItemList_0["Troops - Transporting"].HighlightObject = tbtnTroops;
            tutorialItemList_0["Intelligence Missions"].HighlightObject = tbtnIntelligenceAgents;
            Character listSelection = null;
            CharacterList charactersByRole = _Game.PlayerEmpire.Characters.GetCharactersByRole(CharacterRole.IntelligenceAgent);
            if (charactersByRole.Count > 0)
            {
                listSelection = charactersByRole[0];
            }
            tutorialItemList_0["Assigning missions"].HighlightObject = pnlCharacterMission;
            tutorialItemList_0["Assigning missions"].OpenScreen = kYdDyYeMls;
            tutorialItemList_0["Assigning missions"].ListSelection = listSelection;
            tutorialItemList_0["Counter Intelligence"].OpenScreen = kYdDyYeMls;
            tutorialItemList_0["Counter Intelligence"].ListSelection = listSelection;
            return tutorialItemList_0;
        }

        private TutorialItemList method_451(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Welcome"].HighlightObject = pnlTutorial;
            tutorialItemList_0["Welcome"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["Welcome"].ZoomLevel = 1.0;
            tutorialItemList_0["Moving Around"].HighlightObject = mainView;
            tutorialItemList_0["Moving Around - Zooming"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["Moving Around - Zooming"].ZoomLevel = 60.0;
            List<Control> list = new List<Control>();
            list.Add(btnZoomIn);
            list.Add(btnZoomOut);
            tutorialItemList_0["Zoom buttons"].HighlightObject = list;
            tutorialItemList_0["System view"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["System view"].ZoomLevel = 25.0;
            tutorialItemList_0["Sector view"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["Sector view"].ZoomLevel = 2800.0;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                Habitat habitat = _Game.Galaxy.FindNearestUncolonizedExploredSystem(_Game.PlayerEmpire.Capital.Xpos, _Game.PlayerEmpire.Capital.Ypos, _Game.PlayerEmpire);
                SystemInfo systemInfo = _Game.Galaxy.Systems[habitat.SystemIndex];
                tutorialItemList_0["Explored Systems"].HighlightObject = systemInfo;
                tutorialItemList_0["Explored Systems"].ZoomScrollObject = systemInfo;
                tutorialItemList_0["Explored Systems"].ZoomLevel = 1000.0;
            }
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                Habitat habitat2 = _Game.Galaxy.FindNearestUnexploredSystem(_Game.PlayerEmpire.Capital.Xpos, _Game.PlayerEmpire.Capital.Ypos, _Game.PlayerEmpire);
                SystemInfo systemInfo2 = null;
                if (habitat2 != null)
                {
                    systemInfo2 = _Game.Galaxy.Systems[habitat2.SystemIndex];
                }
                tutorialItemList_0["Unexplored Systems"].HighlightObject = systemInfo2;
                tutorialItemList_0["Unexplored Systems"].ZoomScrollObject = systemInfo2;
                tutorialItemList_0["Unexplored Systems"].ZoomLevel = 1000.0;
            }
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                SystemInfo systemInfo3 = _Game.Galaxy.Systems[_Game.PlayerEmpire.Capital.SystemIndex];
                tutorialItemList_0["Colonized Systems"].HighlightObject = systemInfo3;
                tutorialItemList_0["Colonized Systems"].ZoomScrollObject = systemInfo3;
                tutorialItemList_0["Colonized Systems"].ZoomLevel = 1000.0;
            }
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                Habitat habitat3 = _Game.Galaxy.FastFindNearestUncolonizedOwnedSystem(_Game.PlayerEmpire.Capital);
                if (habitat3 != null)
                {
                    SystemInfo systemInfo4 = _Game.Galaxy.Systems[habitat3.SystemIndex];
                    if (systemInfo4 != null)
                    {
                        tutorialItemList_0["Empire Territory"].HighlightObject = systemInfo4;
                        tutorialItemList_0["Empire Territory"].ZoomScrollObject = systemInfo4;
                        tutorialItemList_0["Empire Territory"].ZoomLevel = 1000.0;
                    }
                }
            }
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                int num = 0;
                Habitat habitat4 = _Game.Galaxy.FindNearestIndependentHabitat(_Game.PlayerEmpire.Capital.Xpos, _Game.PlayerEmpire.Capital.Ypos);
                while (habitat4 != null && (habitat4 == null || !_Game.PlayerEmpire.CheckSystemExplored(habitat4.SystemIndex) || (_Game.Galaxy.Systems[habitat4.SystemIndex].DominantEmpire != null && _Game.Galaxy.Systems[habitat4.SystemIndex].DominantEmpire.Empire != null)) && num < 50)
                {
                    double num2 = Galaxy.Rnd.NextDouble() * 4000000.0 - 2000000.0;
                    double num3 = Galaxy.Rnd.NextDouble() * 4000000.0 - 2000000.0;
                    habitat4 = _Game.Galaxy.FindNearestIndependentHabitat(_Game.PlayerEmpire.Capital.Xpos + num2, _Game.PlayerEmpire.Capital.Ypos + num3);
                    num++;
                }
                if (habitat4 != null && _Game.PlayerEmpire.CheckSystemExplored(habitat4.SystemIndex))
                {
                    SystemInfo systemInfo5 = _Game.Galaxy.Systems[habitat4.SystemIndex];
                    tutorialItemList_0["Independent Systems"].HighlightObject = systemInfo5;
                    tutorialItemList_0["Independent Systems"].ZoomScrollObject = systemInfo5;
                    tutorialItemList_0["Independent Systems"].ZoomLevel = 1000.0;
                }
            }
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                int num4 = 0;
                Habitat habitat5 = _Game.Galaxy.FindNearestColonizableHabitat(_Game.PlayerEmpire.Capital.Xpos, _Game.PlayerEmpire.Capital.Ypos, _Game.PlayerEmpire);
                while (habitat5 != null && (!_Game.PlayerEmpire.CheckSystemExplored(habitat5.SystemIndex) || (_Game.Galaxy.Systems[habitat5.SystemIndex].DominantEmpire != null && _Game.Galaxy.Systems[habitat5.SystemIndex].DominantEmpire.Empire != null)) && num4 < 50)
                {
                    double num5 = Galaxy.Rnd.NextDouble() * 4000000.0 - 2000000.0;
                    double num6 = Galaxy.Rnd.NextDouble() * 4000000.0 - 2000000.0;
                    habitat5 = _Game.Galaxy.FindNearestColonizableHabitat(_Game.PlayerEmpire.Capital.Xpos + num5, _Game.PlayerEmpire.Capital.Ypos + num6, _Game.PlayerEmpire);
                    num4++;
                }
                if (habitat5 != null && _Game.PlayerEmpire.CheckSystemExplored(habitat5.SystemIndex))
                {
                    SystemInfo systemInfo6 = _Game.Galaxy.Systems[habitat5.SystemIndex];
                    tutorialItemList_0["Systems with Potential Colonies"].HighlightObject = systemInfo6;
                    tutorialItemList_0["Systems with Potential Colonies"].ZoomScrollObject = systemInfo6;
                    tutorialItemList_0["Systems with Potential Colonies"].ZoomLevel = 1000.0;
                }
            }
            List<Control> list2 = new List<Control>();
            list2.Add(btnZoomColony);
            list2.Add(btnZoomRegion);
            list2.Add(jQaYpdpkDs);
            list2.Add(btnZoomSystem);
            tutorialItemList_0["Zoom presets"].HighlightObject = list2;
            tutorialItemList_0["Galactopedia Help"].HighlightObject = btnHelp;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                tutorialItemList_0["Main Screen elements"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Main Screen elements"].ZoomLevel = 1.0;
                tutorialItemList_0["Selection Panel"].HighlightObject = pnlInfoPanel;
                tutorialItemList_0["Selection Panel"].SelectionObject = _Game.PlayerEmpire.Capital;
            }
            tutorialItemList_0["Empire Navigation Tool"].HighlightObject = itemListCollectionPanel_0;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                tutorialItemList_0["Empire Navigation Tool contd."].HighlightObject = itemListCollectionPanel_0;
                tutorialItemList_0["Empire Navigation Tool contd."].HighlightOpenEmpireNavigationToolPanel = true;
                tutorialItemList_0["Empire Navigation Tool contd."].HighlightActionButtonNumber = -1;
                tutorialItemList_0["Empire Navigation Tool contd."].SelectionObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Empire Navigation Tool contd."].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Empire Navigation Tool contd."].ZoomLevel = 3000.0;
            }
            tutorialItemList_0["Map"].HighlightObject = pnlSystemMap;
            tutorialItemList_0["Scrolling message list"].HighlightObject = lstMessages;
            tutorialItemList_0["Pause/Resume"].HighlightObject = btnPlayPause;
            tutorialItemList_0["Game Menu"].HighlightObject = btnGameMenu;
            tutorialItemList_0["Game Options"].OpenScreen = pnlGameOptions;
            tutorialItemList_0["Game Options"].HighlightObject = grpOptionsControl;
            return tutorialItemList_0;
        }

        private TutorialItemList method_452(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Welcome"].HighlightObject = pnlTutorial;
            tutorialItemList_0["Welcome"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["Welcome"].ZoomLevel = 1.0;
            tutorialItemList_0["Your Empire"].HighlightObject = btnEmpireSummary;
            List<Control> list = new List<Control>();
            list.Add(pnlEmpireSummaryEconomy);
            tutorialItemList_0["State vs Private"].OpenScreen = pnlEmpireSummary;
            tutorialItemList_0["State vs Private"].HighlightObject = list;
            tutorialItemList_0["Private Citizens"].OpenScreen = pnlEmpireSummary;
            tutorialItemList_0["State Activities"].OpenScreen = pnlEmpireSummary;
            List<Control> list2 = new List<Control>();
            list2.Add(cmbEmpireSummaryChangeGovernmentType);
            list2.Add(btnEmpireSummaryChangeGovernment);
            tutorialItemList_0["Government Style"].OpenScreen = pnlEmpireSummary;
            tutorialItemList_0["Government Style"].HighlightObject = list2;
            tutorialItemList_0["State Income"].OpenScreen = pnlEmpireSummary;
            tutorialItemList_0["State Income"].HighlightObject = list;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Capital != null)
            {
                tutorialItemList_0["Colonies"].HighlightObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Colonies"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Colonies"].ZoomLevel = 1.0;
                tutorialItemList_0["Colonies - Tax"].HighlightObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Colonies - Tax"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Colonies - Tax"].ZoomLevel = 1.0;
                tutorialItemList_0["Colonies - Tax"].SelectionObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Colonies - Growth"].HighlightObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Colonies - Growth"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Colonies - Growth"].ZoomLevel = 1.0;
                tutorialItemList_0["Colonies - Growth"].SelectionObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Population Indicator"].HighlightObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Population Indicator"].HighlightHabitatPopulationGraph = true;
                tutorialItemList_0["Population Indicator"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Population Indicator"].ZoomLevel = 1.0;
                tutorialItemList_0["Population Indicator"].SelectionObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Dominant Race"].HighlightObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Dominant Race"].HighlightHabitatDominantRace = true;
                tutorialItemList_0["Dominant Race"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Dominant Race"].ZoomLevel = 1.0;
                tutorialItemList_0["Dominant Race"].SelectionObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Empire and Capital"].HighlightObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Empire and Capital"].HighlightHabitatEmpire = true;
                tutorialItemList_0["Empire and Capital"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Empire and Capital"].ZoomLevel = 1.0;
                tutorialItemList_0["Empire and Capital"].SelectionObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Resources"].HighlightObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Resources"].HighlightHabitatResources = true;
                tutorialItemList_0["Resources"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
                tutorialItemList_0["Resources"].ZoomLevel = 1.0;
                tutorialItemList_0["Resources"].SelectionObject = _Game.PlayerEmpire.Capital;
            }
            tutorialItemList_0["Colony List"].HighlightObject = tbtnColonies;
            tutorialItemList_0["Colonies - Cycling"].HighlightObject = btnCycleColonies;
            return tutorialItemList_0;
        }

        private TutorialItemList method_453(TutorialItemList tutorialItemList_0)
        {
            tutorialItemList_0["Welcome"].HighlightObject = pnlTutorial;
            tutorialItemList_0["Welcome"].ZoomScrollObject = _Game.PlayerEmpire.Capital;
            tutorialItemList_0["Welcome"].ZoomLevel = 1.0;
            tutorialItemList_0["Ships and Bases"].HighlightObject = tbtnBuiltObjects;
            tutorialItemList_0["Ships and Bases - Designing"].HighlightObject = tbtnDesigns;
            BuiltObject builtObject = null;
            BuiltObject builtObject2 = null;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.BuiltObjects != null)
            {
                foreach (BuiltObject builtObject9 in _Game.PlayerEmpire.BuiltObjects)
                {
                    if (builtObject9.SubRole == BuiltObjectSubRole.ExplorationShip)
                    {
                        builtObject = builtObject9;
                        break;
                    }
                }
                foreach (BuiltObject builtObject10 in _Game.PlayerEmpire.BuiltObjects)
                {
                    if (builtObject10.Role == BuiltObjectRole.Military)
                    {
                        builtObject2 = builtObject10;
                        break;
                    }
                }
            }
            tutorialItemList_0["Assigning a Mission"].HighlightObject = builtObject2;
            tutorialItemList_0["Assigning a Mission"].ZoomScrollObject = builtObject2;
            tutorialItemList_0["Assigning a Mission"].ZoomLevel = 1.0;
            tutorialItemList_0["Assigning a Mission"].SelectionObject = builtObject2;
            tutorialItemList_0["Missions - Popup Menu"].HighlightObject = builtObject2;
            tutorialItemList_0["Missions - Popup Menu"].ZoomScrollObject = builtObject2;
            tutorialItemList_0["Missions - Popup Menu"].ZoomLevel = 1.0;
            tutorialItemList_0["Missions - Popup Menu"].SelectionObject = builtObject2;
            List<object> list = new List<object>();
            list.Add(builtObject2);
            list.Add(pnlInfoPanel);
            tutorialItemList_0["Automating Missions"].HighlightObject = list;
            tutorialItemList_0["Automating Missions"].ZoomScrollObject = builtObject2;
            tutorialItemList_0["Automating Missions"].ZoomLevel = 1.0;
            tutorialItemList_0["Automating Missions"].SelectionObject = builtObject2;
            BuiltObject builtObject3 = null;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.SpacePorts != null && _Game.PlayerEmpire.SpacePorts.Count > 0)
            {
                builtObject3 = _Game.PlayerEmpire.SpacePorts[0];
                tutorialItemList_0["Missions for Bases"].HighlightObject = builtObject3;
                tutorialItemList_0["Missions for Bases"].ZoomScrollObject = builtObject3;
                tutorialItemList_0["Missions for Bases"].ZoomLevel = 1.0;
                tutorialItemList_0["Missions for Bases"].SelectionObject = builtObject3;
            }
            List<object> list2 = new List<object>();
            list2.Add(builtObject);
            list2.Add(btnCycleOther);
            tutorialItemList_0["Exploration Ships"].HighlightObject = list2;
            tutorialItemList_0["Exploration Ships"].ZoomScrollObject = builtObject;
            tutorialItemList_0["Exploration Ships"].ZoomLevel = 1.0;
            tutorialItemList_0["Exploration Ships"].SelectionObject = builtObject;
            tutorialItemList_0["Colony Ships contd."].HighlightObject = btnCycleOther;
            tutorialItemList_0["Space Ports"].HighlightObject = builtObject3;
            tutorialItemList_0["Space Ports"].ZoomScrollObject = builtObject3;
            tutorialItemList_0["Space Ports"].ZoomLevel = 1.0;
            tutorialItemList_0["Space Ports"].SelectionObject = builtObject3;
            List<object> list3 = new List<object>();
            if (builtObject3 != null)
            {
                list3.Add(builtObject3);
            }
            list3.Add(btnCycleBases);
            tutorialItemList_0["Space Ports contd."].HighlightObject = list3;
            tutorialItemList_0["Space Ports contd."].ZoomScrollObject = builtObject3;
            tutorialItemList_0["Space Ports contd."].ZoomLevel = 1.0;
            tutorialItemList_0["Space Ports contd."].SelectionObject = builtObject3;
            BuiltObject builtObject4 = null;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.PrivateBuiltObjects != null)
            {
                foreach (BuiltObject privateBuiltObject in _Game.PlayerEmpire.PrivateBuiltObjects)
                {
                    if (privateBuiltObject.Role == BuiltObjectRole.Freight && privateBuiltObject.UnbuiltComponentCount == 0 && privateBuiltObject.BuiltAt == null)
                    {
                        builtObject4 = privateBuiltObject;
                        break;
                    }
                }
            }
            tutorialItemList_0["Freighters"].HighlightObject = builtObject4;
            tutorialItemList_0["Freighters"].ZoomScrollObject = builtObject4;
            tutorialItemList_0["Freighters"].ZoomLevel = 1.0;
            tutorialItemList_0["Freighters"].SelectionObject = builtObject4;
            List<BuiltObjectSubRole> list4 = new List<BuiltObjectSubRole>();
            BuiltObject builtObject5 = null;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.PrivateBuiltObjects != null)
            {
                list4.Add(BuiltObjectSubRole.MiningShip);
                if (_Game.PlayerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list4) != null && _Game.PlayerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list4).Count > 0)
                {
                    builtObject5 = _Game.PlayerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(list4)[0];
                }
            }
            if (builtObject5 != null)
            {
                tutorialItemList_0["Mining Ships"].HighlightObject = builtObject5;
                tutorialItemList_0["Mining Ships"].ZoomScrollObject = builtObject5;
                tutorialItemList_0["Mining Ships"].ZoomLevel = 1.0;
                tutorialItemList_0["Mining Ships"].SelectionObject = builtObject5;
            }
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.MiningStations != null && _Game.PlayerEmpire.MiningStations.Count > 0)
            {
                BuiltObject builtObject6 = _Game.PlayerEmpire.MiningStations[0];
                tutorialItemList_0["Mining Stations"].HighlightObject = builtObject6;
                tutorialItemList_0["Mining Stations"].ZoomScrollObject = builtObject6;
                tutorialItemList_0["Mining Stations"].ZoomLevel = 1.0;
                tutorialItemList_0["Mining Stations"].SelectionObject = builtObject6;
            }
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.ConstructionShips != null && _Game.PlayerEmpire.ConstructionShips.Count > 0)
            {
                BuiltObject builtObject7 = _Game.PlayerEmpire.ConstructionShips[0];
                List<object> list5 = new List<object>();
                list5.Add(list5);
                list5.Add(btnCycleConstruction);
                tutorialItemList_0["Construction Ships"].HighlightObject = list5;
                tutorialItemList_0["Construction Ships"].ZoomScrollObject = builtObject7;
                tutorialItemList_0["Construction Ships"].ZoomLevel = 1.0;
                tutorialItemList_0["Construction Ships"].SelectionObject = builtObject7;
            }
            BuiltObject builtObject8 = null;
            if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.BuiltObjects != null)
            {
                list4.Clear();
                list4.Add(BuiltObjectSubRole.Frigate);
                if (_Game.PlayerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list4) != null && _Game.PlayerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list4).Count > 0)
                {
                    builtObject8 = _Game.PlayerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(list4)[0];
                }
            }
            List<object> list6 = new List<object>();
            list6.Add(btnCycleMilitary);
            if (builtObject8 != null)
            {
                list6.Add(builtObject8);
            }
            tutorialItemList_0["Military Ships"].HighlightObject = list6;
            tutorialItemList_0["Military Ships"].ZoomScrollObject = builtObject8;
            tutorialItemList_0["Military Ships"].ZoomLevel = 1.0;
            tutorialItemList_0["Military Ships"].SelectionObject = builtObject8;
            tutorialItemList_0["Find Idle Ships"].HighlightObject = btnCycleIdleShips;
            return tutorialItemList_0;
        }


  }

}