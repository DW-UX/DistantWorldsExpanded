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


        private void OpenDesignEditor(Design design_3)
        {
            bool_9 = true;
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            cmbDesignsSubRole.SelectedIndexChanged -= cmbDesignsSubRole_SelectedIndexChanged;
            cmbDesignsPicture.SelectedIndexChanged -= cmbDesignsPicture_SelectedIndexChanged;
            cmbDesignsFleeWhen.SelectedIndexChanged -= cmbDesignsFleeWhen_SelectedIndexChanged;
            cmbDesignTacticsInvasion.SelectedIndexChanged -= cmbDesignTacticsInvasion_SelectedIndexChanged;
            cmbDesignTacticsStrongerShips.SelectedIndexChanged -= cmbDesignTacticsStrongerShips_SelectedIndexChanged;
            cmbDesignTacticsWeakerShips.SelectedIndexChanged -= cmbDesignTacticsWeakerShips_SelectedIndexChanged;
            pnlDesignDetail.Size = new Size(1010, 783);
            pnlDesignDetail.Location = new Point((mainView.Width - pnlDesignDetail.Width) / 2, (mainView.Height - pnlDesignDetail.Height) / 2);
            lblDesignDetailTitle.Font = font_1;
            lblDesignDetailTitle.ForeColor = color_0;
            lblDesignDetailTitle.BackColor = Color.Transparent;
            string text = TextResolver.GetText("Edit Design");
            if (design_3 != null)
            {
                text = ((!(string_16 == "view")) ? (text + ": " + design_3.Name) : (TextResolver.GetText("View Design") + ": " + design_3.Name));
            }
            lblDesignDetailTitle.Text = text;
            lblDesignDetailTitle.Location = new Point(10, 10);
            lblDesignDetailPurchaseCost.Location = new Point(458, 7);
            lblDesignDetailMaintenanceCost.Location = new Point(445, 21);
            chkDesignObsolete.Location = new Point(610, 12);
            btnDesignsSaveDesign.BringToFront();
            btnDesignsSaveDesign.Size = new Size(120, 25);
            btnDesignsSaveDesign.Location = new Point(750, 8);
            btnDesignsCancel.Size = new Size(120, 25);
            btnDesignsCancel.Location = new Point(880, 8);
            lblDesignDetailAutoRetrofit.Location = new Point(5, 42);
            lblDesignDetailAutoRetrofit.Text = TextResolver.GetText("Default Retrofit Stance");
            cmbDesignDetailAutoRetrofit.Size = new Size(230, 21);
            cmbDesignDetailAutoRetrofit.Location = new Point(125, 40);
            cmbDesignDetailAutoRetrofit.BringToFront();
            cmbDesignDetailAutoRetrofit.Items.Clear();
            cmbDesignDetailAutoRetrofit.Items.Add(TextResolver.GetText("Auto Retrofit (including advisor suggestions)"));
            cmbDesignDetailAutoRetrofit.Items.Add(TextResolver.GetText("Only Retrofit When Manually Ordered"));
            switch (design_3.SubRole)
            {
                default:
                    cmbDesignDetailAutoRetrofit.Enabled = true;
                    break;
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    cmbDesignDetailAutoRetrofit.Enabled = false;
                    break;
            }
            lblDesignDetailUpgradeRolesExplanation.Text = string.Empty;
            lblDesignDetailUpgradeRolesExplanation.Location = new Point(370, 38);
            lblDesignDetailUpgradeRolesExplanation.MaximumSize = new Size(630, 30);
            lblDesignDetailUpgradeRolesExplanation.Size = new Size(630, 30);
            lblDesignDetailUpgradeRolesExplanation.TextAlign = ContentAlignment.MiddleLeft;
            if (design_3 != null)
            {
                if (_Game.PlayerEmpire.CheckDesignSubRoleShouldBeUpgraded(design_3.SubRole))
                {
                    lblDesignDetailUpgradeRolesExplanation.Text = TextResolver.GetText("Design Upgrade Affirmative Explanation");
                }
                else
                {
                    lblDesignDetailUpgradeRolesExplanation.Text = TextResolver.GetText("Design Upgrade Negative Explanation");
                }
            }
            pnlDesignWarningsBackground.Size = new Size(410, 170);
            pnlDesignWarningsBackground.Location = new Point(370, 71);
            pnlDesignWarningsContainer.Size = new Size(395, 150);
            pnlDesignWarningsContainer.Location = new Point(8, 8);
            pnlDesignWarningsContainer.AutoScroll = true;
            pnlDesignWarningsContainer.SetAutoScrollMargin(0, 0);
            pnlDesignWarningsContainer.AutoScrollPosition = new Point(0, 0);
            pnlDesignWarningsContainer.BackColor = Color.Transparent;
            pnlDesignWarnings.Size = new Size(375, 0);
            pnlDesignWarnings.Location = new Point(0, 0);
            pnlDesignWarnings.MaximumSize = new Size(375, 1000);
            pnlDesignWarnings.MinimumSize = new Size(375, 149);
            pnlDesignWarnings.AutoSize = true;
            pnlDesignWarnings.BringToFront();
            pnlDesignEnergy.Size = new Size(210, 170);
            pnlDesignEnergy.Location = new Point(790, 71);
            pnlDesignBasics.BringToFront();
            pnlDesignBasics.Size = new Size(350, 170);
            pnlDesignBasics.Location = new Point(10, 71);
            lblDesignsSize.Location = new Point(4, 12);
            lblDesignsSizeValue.Location = new Point(50, 7);
            lblDesignsSizeValue.Font = font_2;
            lblDesignsBasicsSubRole.Location = new Point(5, 37);
            cmbDesignsSubRole.Size = new Size(120, 20);
            cmbDesignsSubRole.Location = new Point(83, 35);
            cmbDesignsSubRole.BringToFront();
            cmbDesignsSubRole.Items.Clear();
            BuiltObjectSubRole[] array = (BuiltObjectSubRole[])Enum.GetValues(typeof(BuiltObjectSubRole));
            BuiltObjectSubRole[] array2 = array;
            foreach (BuiltObjectSubRole builtObjectSubRole in array2)
            {
                if (builtObjectSubRole != 0)
                {
                    cmbDesignsSubRole.Items.Add(Galaxy.ResolveDescription(builtObjectSubRole));
                }
            }
            lblDesignTacticsStrongerShips.Location = new Point(-1, 62);
            cmbDesignTacticsStrongerShips.Size = new Size(70, 20);
            cmbDesignTacticsStrongerShips.Location = new Point(133, 60);
            cmbDesignTacticsStrongerShips.Items.Clear();
            BattleTactics[] array3 = (BattleTactics[])Enum.GetValues(typeof(BattleTactics));
            BattleTactics[] array4 = array3;
            foreach (BattleTactics battleTactics in array4)
            {
                if (battleTactics != 0)
                {
                    cmbDesignTacticsStrongerShips.Items.Add(Galaxy.ResolveDescription(battleTactics));
                }
            }
            lblDesignTacticsWeakerShips.Location = new Point(-2, 87);
            cmbDesignTacticsWeakerShips.Size = new Size(70, 20);
            cmbDesignTacticsWeakerShips.Location = new Point(133, 85);
            cmbDesignTacticsWeakerShips.Items.Clear();
            BattleTactics[] array5 = array3;
            foreach (BattleTactics battleTactics2 in array5)
            {
                if (battleTactics2 != 0)
                {
                    cmbDesignTacticsWeakerShips.Items.Add(Galaxy.ResolveDescription(battleTactics2));
                }
            }
            lblDesignTacticsInvasion.Location = new Point(-1, 112);
            cmbDesignTacticsInvasion.Size = new Size(120, 20);
            cmbDesignTacticsInvasion.Location = new Point(83, 110);
            cmbDesignTacticsInvasion.Items.Clear();
            InvasionTactics[] array6 = (InvasionTactics[])Enum.GetValues(typeof(InvasionTactics));
            InvasionTactics[] array7 = array6;
            foreach (InvasionTactics invasionTactics in array7)
            {
                if (invasionTactics != 0)
                {
                    cmbDesignTacticsInvasion.Items.Add(Galaxy.ResolveDescription(invasionTactics));
                }
            }
            lblDesignsStance.Visible = false;
            cmbDesignsStance.Visible = false;
            lblDesignsFleeWhen.Location = new Point(-4, 137);
            cmbDesignsFleeWhen.BringToFront();
            cmbDesignsFleeWhen.Size = new Size(120, 20);
            cmbDesignsFleeWhen.DropDownWidth = 160;
            cmbDesignsFleeWhen.Location = new Point(83, 135);
            cmbDesignsFleeWhen.Items.Clear();
            List<BuiltObjectFleeWhen> list = new List<BuiltObjectFleeWhen>();
            list.Add(BuiltObjectFleeWhen.EnemyMilitarySighted);
            list.Add(BuiltObjectFleeWhen.Attacked);
            list.Add(BuiltObjectFleeWhen.Shields50);
            list.Add(BuiltObjectFleeWhen.Shields20);
            list.Add(BuiltObjectFleeWhen.Armor50);
            list.Add(BuiltObjectFleeWhen.Never);
            foreach (BuiltObjectFleeWhen item in list)
            {
                if (item != 0)
                {
                    cmbDesignsFleeWhen.Items.Add(Galaxy.ResolveDescription(item));
                }
            }
            lblDesignsPicture.Visible = false;
            cmbDesignsPicture.IntegralHeight = false;
            cmbDesignsPicture.ItemHeight = 119;
            cmbDesignsPicture.MaxDropDownItems = 6;
            cmbDesignsPicture.Size = new Size(130, 119);
            cmbDesignsPicture.Location = new Point(210, 34);
            cmbDesignsPicture.Items.Clear();
            Bitmap[] imagesSmall = builtObjectImageCache_0.GetImagesSmall();
            for (int m = 0; m < imagesSmall.Length; m++)
            {
                cmbDesignsPicture.Items.Add(" ");
            }
            picDesignPicture.Visible = false;
            picDesignPicture.Size = new Size(95, 95);
            picDesignPicture.Location = new Point(240, 37);
            lblDesignImageScalingMode.Location = new Point(130, 9);
            lblDesignImageScalingMode.Text = TextResolver.GetText("Image Scaling Mode");
            cmbDesignImageScalingMode.Size = new Size(70, 21);
            cmbDesignImageScalingMode.Location = new Point(210, 7);
            cmbDesignImageScalingMode.Ignite();
            lblDesignImageScalingAmount.Location = new Point(230, 9);
            lblDesignImageScalingAmount.Text = TextResolver.GetText("Amount");
            lblDesignImageScalingAmount.Visible = false;
            numDesignImageScalingAmount.Size = new Size(55, 21);
            numDesignImageScalingAmount.Location = new Point(285, 7);
            ctlDesignComponents.SummarizedMode = true;
            ctlDesignComponentToolbox.SummarizedMode = false;
            chkDesignComponentsShowLatest.Location = new Point(10, 249);
            chkDesignComponentsShowLatest.Checked = true;
            ctlDesignComponentToolbox.BringToFront();
            ctlDesignComponentToolbox.Size = new Size(320, 278);
            ctlDesignComponentToolbox.Location = new Point(10, 269);
            ctlDesignComponentToolbox.Grid.Columns["TechPoints"].Visible = false;
            ctlDesignComponentToolbox.Grid.Columns["Category"].Width = 35;
            ctlDesignComponentToolbox.Grid.Columns["Picture"].Width = 35;
            ctlDesignComponentToolbox.Grid.Columns["Name"].Width = 210;
            ctlDesignComponentToolbox.Grid.Columns["Size"].Width = 50;
            ComponentList componentList = new ComponentList();
            componentList = ((!chkDesignComponentsShowLatest.Checked) ? method_290() : Kdxguwronl());
            ctlDesignComponentToolbox.BindData(componentList, bitmap_21, _Game.Galaxy);
            btnAddComponentToDesign.Size = new Size(30, 90);
            btnAddComponentToDesign.Location = new Point(335, 316);
            btnRemoveComponentFromDesign.Size = new Size(30, 90);
            btnRemoveComponentFromDesign.Location = new Point(335, 414);
            _btnRepairPrioritySelect.Size = new Size(150, 20);
            _btnRepairPrioritySelect.Location = new Point(550, 245);
            _lblCurrentRepairPriorityTemplate.Location = new Point(335, 245);
            _lblCurrentRepairPriorityTemplate.Text = $"Current template: {design_3.RepaitPriorityTemplateName ?? "Original"}";
            btnAddComponentToDesignMultiple.Text = ">\nx5";
            btnRemoveComponentFromDesignMultiple.Text = "<\nx5";
            btnAddComponentToDesignMultiple.Size = new Size(30, 45);
            btnAddComponentToDesignMultiple.Location = new Point(335, 269);
            btnRemoveComponentFromDesignMultiple.Size = new Size(30, 45);
            btnRemoveComponentFromDesignMultiple.Location = new Point(335, 507);
            pnlDesignComponentsHighlight.Size = new Size(329, 285);
            pnlDesignComponentsHighlight.Location = new Point(366, 269);
            lblDesignName.Location = new Point(370, 275);
            lblDesignName.Font = font_7;
            txtDesignName.Size = new Size(275, 12);
            txtDesignName.Location = new Point(415, 275);
            txtDesignName.Font = font_7;
            ctlDesignComponents.Size = new Size(320, 250);
            ctlDesignComponents.Location = new Point(370, 299);
            ctlDesignComponents.Grid.Columns["Category"].Visible = false;
            ctlDesignComponents.Grid.Columns["TechPoints"].Visible = false;
            ctlDesignComponents.Grid.Columns["Picture"].Width = 35;
            ctlDesignComponents.Grid.Columns["Name"].Width = 210;
            ctlDesignComponents.Grid.Columns["Size"].Width = 45;
            btnDesignsShowComponentGuide.Size = new Size(320, 25);
            btnDesignsShowComponentGuide.Location = new Point(10, 555);
            btnDesignsShowConstructionSummary.Size = new Size(320, 25);
            btnDesignsShowConstructionSummary.Location = new Point(370, 555);
            pnlDesignMovement.Size = new Size(300, 188);
            pnlDesignMovement.Location = new Point(700, 249);
            pnlDesignIndustry.Size = new Size(300, 132);
            pnlDesignIndustry.Location = new Point(700, 445);
            pnlDesignComponentDetail.Size = new Size(210, 190);
            pnlDesignComponentDetail.Location = new Point(10, 585);
            pnlDesignComponentDetail.SetTextPositions(5, 135, 150, 60);
            pnlDesignWeapons.Size = new Size(550, 190);
            pnlDesignWeapons.Location = new Point(230, 585);
            lblDesignsWeaponsTitle.Location = new Point(10, 10);
            lblDesignsWeaponsTitle.Font = ((IFontCache)this).GenerateFont(13f, isBold: true);
            ctlDesignWeapons.Size = new Size(330, 145);
            ctlDesignWeapons.Location = new Point(10, 35);
            ctlDesignWeapons.BringToFront();
            ctlDesignWeapons.DamageGraphWidth = 150;
            ctlDesignWeapons.DamageGraphHeight = 17;
            ctlDesignWeapons.RowHeight = 17;
            ctlDesignWeapons.Grid.Columns["Speed"].Visible = false;
            ctlDesignWeapons.Grid.Columns["EnergyRequired"].Visible = false;
            ctlDesignWeapons.Grid.Columns["FireRate"].Visible = false;
            ctlDesignWeapons.Grid.Columns["Picture"].Width = 35;
            ctlDesignWeapons.Grid.Columns["Name"].Width = 145;
            ctlDesignWeapons.Grid.Columns["DamageGraph"].Width = 150;
            lblDesignsWeaponsTitle.Font = font_7;
            lblDesignWeaponFirepower.Location = new Point(347, 4);
            lblDesignWeaponFirepowerValue.Location = new Point(480, 4);
            lblDesignWeaponRangeMinimum.Location = new Point(340, 19);
            lblDesignWeaponRangeMinimum.Text = TextResolver.GetText("Range Shortest/Longest");
            lblDesignWeaponRangeMinimumValue.Location = new Point(480, 19);
            lblDesignWeaponRangeMaximum.Location = new Point(345, 34);
            lblDesignWeaponRangeMaximum.Text = TextResolver.GetText("Fighter Firepower");
            lblDesignWeaponRangeMaximumValue.Location = new Point(480, 34);
            lblDesignWeaponFighters.Location = new Point(345, 49);
            lblDesignWeaponFightersValue.Location = new Point(480, 49);
            lblDesignWeaponTargetting.Location = new Point(345, 66);
            lblDesignWeaponTargettingValue.Location = new Point(480, 66);
            lblDesignWeaponFleetTargetting.Location = new Point(344, 81);
            lblDesignWeaponFleetTargettingValue.Location = new Point(480, 81);
            lblDesignWeaponTroopCapacity.Location = new Point(346, 96);
            lblDesignWeaponTroopCapacityValue.Location = new Point(480, 96);
            lblDesignWeaponBoardingAssault.Location = new Point(343, 111);
            lblDesignWeaponBoardingAssaultValue.Location = new Point(480, 111);
            lblDesignWeaponHyperDeny.Location = new Point(345, 126);
            lblDesignWeaponHyperDenyValue.Location = new Point(480, 126);
            lblDesignWeaponHyperStop.Location = new Point(343, 141);
            lblDesignWeaponHyperStopValue.Location = new Point(480, 141);
            lblDesignWeaponPointDefense.Location = new Point(343, 156);
            lblDesignWeaponPointDefense.Text = TextResolver.GetText("Component Type Point Defense");
            lblDesignWeaponPointDefenseValue.Location = new Point(480, 156);
            lblDesignWeaponBombardPower.Location = new Point(346, 171);
            lblDesignWeaponBombardPower.Text = TextResolver.GetText("Bombard");
            lblDesignWeaponBombardPowerValue.Location = new Point(480, 171);
            lblDesignWeaponFirepower.BringToFront();
            lblDesignWeaponFirepowerValue.BringToFront();
            lblDesignWeaponRangeMinimum.BringToFront();
            lblDesignWeaponRangeMinimumValue.BringToFront();
            lblDesignWeaponRangeMaximum.BringToFront();
            lblDesignWeaponRangeMaximumValue.BringToFront();
            lblDesignWeaponTargetting.BringToFront();
            lblDesignWeaponTargettingValue.BringToFront();
            lblDesignWeaponFleetTargetting.BringToFront();
            lblDesignWeaponFleetTargettingValue.BringToFront();
            lblDesignWeaponFighters.BringToFront();
            lblDesignWeaponFightersValue.BringToFront();
            lblDesignWeaponTroopCapacity.BringToFront();
            lblDesignWeaponTroopCapacityValue.BringToFront();
            lblDesignWeaponBoardingAssault.BringToFront();
            lblDesignWeaponBoardingAssaultValue.BringToFront();
            lblDesignWeaponHyperDeny.BringToFront();
            lblDesignWeaponHyperDenyValue.BringToFront();
            lblDesignWeaponHyperStop.BringToFront();
            lblDesignWeaponHyperStopValue.BringToFront();
            lblDesignWeaponPointDefense.BringToFront();
            lblDesignWeaponPointDefenseValue.BringToFront();
            lblDesignWeaponBombardPower.BringToFront();
            lblDesignWeaponBombardPowerValue.BringToFront();
            FqNlrHsjje.Location = new Point(80, 10);
            FqNlrHsjje.Text = TextResolver.GetText("Maximum Weapons Energy use per second");
            lblDesignWeaponTotalEnergyValue.Location = new Point(310, 8);
            if (!lblDesignWeaponTotalEnergyValue.Font.Bold)
            {
                lblDesignWeaponTotalEnergyValue.Font = new Font(lblDesignWeaponTotalEnergyValue.Font, FontStyle.Bold);
            }
            pnlDesignDefense.Size = new Size(210, 190);
            pnlDesignDefense.Location = new Point(790, 585);
            cmbDesignsPicture.SelectedIndexChanged += cmbDesignsPicture_SelectedIndexChanged;
            method_291(design_3);
            if (string_16 == "view")
            {
                btnDesignsSaveDesign.Enabled = true;
                cmbDesignsSubRole.Enabled = false;
                cmbDesignsPicture.Enabled = false;
                txtDesignName.Enabled = false;
                btnAddComponentToDesign.Enabled = false;
                btnAddComponentToDesignMultiple.Enabled = false;
                btnRemoveComponentFromDesign.Enabled = false;
                btnRemoveComponentFromDesignMultiple.Enabled = false;
                _btnRepairPrioritySelect.Enabled = false;
            }
            else
            {
                btnDesignsSaveDesign.Enabled = true;
                cmbDesignsSubRole.Enabled = true;
                cmbDesignsPicture.Enabled = true;
                txtDesignName.Enabled = true;
                btnAddComponentToDesign.Enabled = true;
                btnAddComponentToDesignMultiple.Enabled = true;
                btnRemoveComponentFromDesign.Enabled = true;
                btnRemoveComponentFromDesignMultiple.Enabled = true;
                _btnRepairPrioritySelect.Enabled = true;
            }
            ctlDesignComponentToolbox_SelectionChanged(this, new EventArgs());
            cmbDesignsSubRole.SelectedIndexChanged += cmbDesignsSubRole_SelectedIndexChanged;
            cmbDesignsFleeWhen.SelectedIndexChanged += cmbDesignsFleeWhen_SelectedIndexChanged;
            cmbDesignTacticsInvasion.SelectedIndexChanged += cmbDesignTacticsInvasion_SelectedIndexChanged;
            cmbDesignTacticsStrongerShips.SelectedIndexChanged += cmbDesignTacticsStrongerShips_SelectedIndexChanged;
            cmbDesignTacticsWeakerShips.SelectedIndexChanged += cmbDesignTacticsWeakerShips_SelectedIndexChanged;
            int pictureRef = design_3.PictureRef;
            cmbDesignsPicture.SelectedIndex = 0;
            cmbDesignsPicture.SelectedIndex = pictureRef;
            pnlDesignDetail.Visible = true;
            pnlDesignDetail.BringToFront();
        }

        private void method_293()
        {
            pnlDesignDetail.SendToBack();
            pnlDesignDetail.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
            method_545();
            method_549();
        }

        private void method_294()
        {
            method_299();
            pnlDiplomacyTalk.SendToBack();
            pnlDiplomacyTalk.Visible = false;
            ctlEmpireDiplomaticRelationList.Invalidate();
            pnlEmpireDetailInfo.Invalidate();
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
            method_522();
        }

        private void method_295(Empire empire_5)
        {
            ConversationOption conversationOption_ = new ConversationOption(method_236(empire_5, _Game.PlayerEmpire), string.Empty, _Game.PlayerEmpire);
            if (empire_5.PirateEmpireBaseHabitat == null && !_Game.PlayerEmpireFirstDialogDoneEmpireIds.Contains(empire_5.EmpireId))
            {
                _Game.PlayerEmpireFirstDialogDoneEmpireIds.Add(empire_5.EmpireId);
                conversationOption_ = new ConversationOption(DialogPartType.GREETING_INTRODUCTION, string.Empty, empire_5, 0.0, _Game.PlayerEmpire);
            }
            method_296(empire_5, conversationOption_);
        }

        private void method_296(Empire empire_5, ConversationOption conversationOption_0)
        {
            method_299();
            if (empire_5 == null)
            {
                return;
            }
            bool_9 = true;
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            _ = empire_5.PirateEmpireBaseHabitat;
            if (empire_5.PirateEmpireBaseHabitat != null && _Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
            {
                DiplomaticRelation diplomaticRelation = empire_5.ObtainDiplomaticRelation(_Game.PlayerEmpire);
                if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                {
                    diplomaticRelation.Type = DiplomaticRelationType.None;
                }
            }
            if (!pnlDiplomacyTalk.Visible && empire_5 != null)
            {
                method_521(empire_5);
            }
            pnlDiplomacyTalk.SuspendLayout();
            pnlDiplomacyTalk.Size = new Size(430, 778);
            pnlDiplomacyTalkPanel.Size = new Size(410, 758);
            pnlDiplomacyTalk.Location = new Point((mainView.Width - pnlDiplomacyTalk.Width) / 2, (mainView.Height - pnlDiplomacyTalk.Height) / 2);
            pnlDiplomacyTalkPanel.Location = new Point(10, 10);
            pnlDiplomacyTalkPanel.BackColor2 = BaconMain.SetColorForDiplomacyBackground(empire_5);
            lblDiplomacyTalkTitle.Font = font_1;
            lblDiplomacyTalkTitle.ForeColor = color_0;
            lblDiplomacyTalkTitle.BackColor = Color.Transparent;
            string name = empire_5.Name;
            lblDiplomacyTalkTitle.Text = name;
            Graphics graphics = lblDiplomacyTalkTitle.CreateGraphics();
            int num = 55 + (int)graphics.MeasureString(name, lblDiplomacyTalkTitle.Font, 410, StringFormat.GenericDefault).Width;
            int num2 = (pnlDiplomacyTalkPanel.Width - num) / 2;
            picFlag.Size = new Size(50, 30);
            picFlag.Location = new Point(num2, 8);
            picFlag.SizeMode = PictureBoxSizeMode.Zoom;
            Bitmap image = PrecacheScaledBitmap(empire_5.LargeFlagPicture, 50, 30, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
            picFlag.Image = image;
            lblDiplomacyTalkTitle.Location = new Point(num2 + 55, 10);
            picRace.Size = new Size(280, 280);
            picRace.Location = new Point((pnlDiplomacyTalkPanel.Width - 280) / 2, 45);
            picRace.BackColor = Color.Transparent;
            picRace.SizeMode = PictureBoxSizeMode.CenterImage;
            Bitmap unscaledBitmap = method_118(empire_5, empire_5.DominantRace, 280, 280, bitmap_31, 7, empire_5.PirateEmpireBaseHabitat != null);
            Bitmap image2 = PrecacheScaledBitmap(unscaledBitmap, 282, 282, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
            picRace.Image = image2;
            ctlDiplomacyTradeThem.SetFont(19f);
            ctlDiplomacyTradeUs.SetFont(19f);
            ctlDiplomacyTradeThem.Size = new Size(280, 705);
            ctlDiplomacyTradeThem.Location = new Point(-340, 40);
            ctlDiplomacyTradeThem.DoBind(_Game.Galaxy, empire_5, _Game.PlayerEmpire, allowDiplomaticThreats: false, refactorValuesForEmpire: true);
            ctlDiplomacyTradeUs.Size = new Size(280, 705);
            ctlDiplomacyTradeUs.Location = new Point(690, 40);
            ctlDiplomacyTradeUs.DoBind(_Game.Galaxy, _Game.PlayerEmpire, empire_5, allowDiplomaticThreats: true, refactorValuesForEmpire: false);
            if (conversationOption_0.RelatedInfo is object[])
            {
                object[] array = (object[])conversationOption_0.RelatedInfo;
                TradeableItemList selectedItems = (TradeableItemList)array[0];
                TradeableItemList selectedItems2 = (TradeableItemList)array[1];
                ctlDiplomacyTradeThem.SetSelectedItems(selectedItems);
                ctlDiplomacyTradeUs.SetSelectedItems(selectedItems2);
            }
            pnlDiplomacyEmpireSummary.Visible = false;
            pnlDiplomaticConversationResponse.Size = new Size(390, 188);
            pnlDiplomaticConversationResponse.Location = new Point(10, 335);
            pnlDiplomaticConversationResponse.BackColor = Color.FromArgb(80, 0, 0, 0);
            method_230(conversationOption_0);
            ctlDiplomacyConversation.Size = new Size(390, 220);
            ctlDiplomacyConversation.Location = new Point(10, 528);
            ctlDiplomacyConversation.BackColor = Color.FromArgb(64, 0, 0, 0);
            ctlDiplomacyConversation.KickStart(this, _Game.Galaxy, cursor_0);
            method_238(conversationOption_0);
            pnlDiplomacyTalk.ResumeLayout();
            pnlDiplomacyTalk.Visible = true;
            pnlDiplomacyTalk.BringToFront();
        }

        private void method_297(Empire empire_5)
        {
            method_299();
            if (empire_5 != null)
            {
                _ = empire_5.PirateEmpireBaseHabitat;
                pnlDiplomacyTalk.Visible = false;
                SuspendLayout();
                pnlDiplomacyTalk.SuspendLayout();
                pnlDiplomacyTalk.Size = new Size(430, 778);
                pnlDiplomacyTalkPanel.Size = new Size(410, 758);
                pnlDiplomacyTalk.Location = new Point((mainView.Width - pnlDiplomacyTalk.Width) / 2, (mainView.Height - pnlDiplomacyTalk.Height) / 2);
                pnlDiplomacyTalkPanel.Location = new Point(10, 10);
                pnlDiplomacyTalkPanel.BackColor2 = BaconMain.SetColorForDiplomacyBackground(empire_5);
                lblDiplomacyTalkTitle.Font = font_1;
                lblDiplomacyTalkTitle.ForeColor = color_0;
                lblDiplomacyTalkTitle.BackColor = Color.Transparent;
                string name = empire_5.Name;
                lblDiplomacyTalkTitle.Text = name;
                Graphics graphics = lblDiplomacyTalkTitle.CreateGraphics();
                int num = 55 + (int)graphics.MeasureString(name, lblDiplomacyTalkTitle.Font, 410, StringFormat.GenericDefault).Width;
                int num2 = (pnlDiplomacyTalkPanel.Width - num) / 2;
                picFlag.Size = new Size(50, 30);
                picFlag.Location = new Point(num2, 8);
                picFlag.SizeMode = PictureBoxSizeMode.Zoom;
                Bitmap image = PrecacheScaledBitmap(empire_5.LargeFlagPicture, 50, 30, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                picFlag.Image = image;
                lblDiplomacyTalkTitle.Location = new Point(num2 + 55, 10);
                picRace.Size = new Size(280, 280);
                picRace.Location = new Point((pnlDiplomacyTalkPanel.Width - 280) / 2, 45);
                picRace.SizeMode = PictureBoxSizeMode.CenterImage;
                Bitmap unscaledBitmap = method_118(empire_5, empire_5.DominantRace, 280, 280, bitmap_31, 7, empire_5.PirateEmpireBaseHabitat != null);
                Bitmap image2 = PrecacheScaledBitmap(unscaledBitmap, 282, 282, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                picRace.Image = image2;
                ctlDiplomacyTradeThem.Size = new Size(280, 710);
                ctlDiplomacyTradeThem.Location = new Point(-340, 30);
                ctlDiplomacyTradeUs.Size = new Size(280, 710);
                ctlDiplomacyTradeUs.Location = new Point(690, 30);
                pnlDiplomaticConversationResponse.Size = new Size(390, 188);
                pnlDiplomaticConversationResponse.Location = new Point(10, 335);
                pnlDiplomaticConversationResponse.BackColor = Color.FromArgb(80, 0, 0, 0);
                ctlDiplomacyConversation.Size = new Size(390, 220);
                ctlDiplomacyConversation.Location = new Point(10, 528);
                ctlDiplomacyConversation.BackColor = Color.FromArgb(64, 0, 0, 0);
                pnlDiplomacyTalk.ResumeLayout();
                ResumeLayout();
                pnlEmpireDetailInfo.Invalidate();
                pnlDiplomacyTalk.Visible = true;
            }
        }

        private void method_298(int int_64, int int_65, object object_7, bool bool_28)
        {
            pnlHoverDetail.Location = new Point(int_64, int_65);
            pnlHoverDetail.BindData(_Game.Galaxy, _Game.PlayerEmpire, object_7, bool_28);
            pnlHoverDetail.BringToFront();
            pnlHoverDetail.Visible = true;
        }

        private void method_299()
        {
            pnlHoverDetail.Visible = false;
            pnlHoverDetail.SendToBack();
        }

        private void method_300(object sender, ResearchProjectHoveredEventArgs e)
        {
            if (pnlDiplomacyTalk.Visible)
            {
                ctlDiplomacyTradeThem.AcceptingMouseMoveEvents = false;
                if (e.ResearchProject != null)
                {
                    int int_ = pnlDiplomacyTalk.Location.X + ctlDiplomacyTradeThem.Location.X + e.NodeRelativeRectangle.Right;
                    int int_2 = pnlDiplomacyTalk.Location.Y + ctlDiplomacyTradeThem.Location.Y + e.NodeRelativeRectangle.Y;
                    method_298(int_, int_2, e.ResearchProject, bool_28: false);
                }
                else
                {
                    method_299();
                }
                ctlDiplomacyTradeThem.AcceptingMouseMoveEvents = true;
            }
        }

        private void method_301(object sender, ResearchProjectHoveredEventArgs e)
        {
            if (pnlDiplomacyTalk.Visible)
            {
                ctlDiplomacyTradeUs.AcceptingMouseMoveEvents = false;
                if (e.ResearchProject != null)
                {
                    int int_ = pnlDiplomacyTalk.Location.X + ctlDiplomacyTradeUs.Location.X + e.NodeRelativeRectangle.X - pnlHoverDetail.Width;
                    int int_2 = pnlDiplomacyTalk.Location.Y + ctlDiplomacyTradeUs.Location.Y + e.NodeRelativeRectangle.Y;
                    method_298(int_, int_2, e.ResearchProject, bool_28: false);
                }
                else
                {
                    method_299();
                }
                ctlDiplomacyTradeUs.AcceptingMouseMoveEvents = true;
            }
        }

        private void method_302(Empire empire_5, TradeableItemList tradeableItemList_6, TradeableItemList tradeableItemList_7, TradeableItemList tradeableItemList_8, TradeableItemList tradeableItemList_9, TradeableItemList tradeableItemList_10, TradeableItemList tradeableItemList_11)
        {
            method_299();
            if (pnlDiplomacyTalk.Visible && empire_5 != null)
            {
                pnlDiplomacyTalk.Visible = false;
                SuspendLayout();
                pnlDiplomacyTalk.SuspendLayout();
                pnlDiplomacyTalk.Size = new Size(1010, 778);
                pnlDiplomacyTalk.Location = new Point((mainView.Width - pnlDiplomacyTalk.Width) / 2, (mainView.Height - pnlDiplomacyTalk.Height) / 2);
                pnlDiplomacyTalkPanel.Size = new Size(410, 758);
                pnlDiplomacyTalkPanel.Location = new Point(300, 10);
                pnlDiplomacyTalkPanel.BackColor2 = BaconMain.SetColorForDiplomacyBackground(empire_5);
                lblDiplomacyTalkTitle.Font = font_1;
                lblDiplomacyTalkTitle.ForeColor = color_0;
                lblDiplomacyTalkTitle.BackColor = Color.Transparent;
                string name = empire_5.Name;
                lblDiplomacyTalkTitle.Text = name;
                Graphics graphics = lblDiplomacyTalkTitle.CreateGraphics();
                int num = 55 + (int)graphics.MeasureString(name, lblDiplomacyTalkTitle.Font, 410, StringFormat.GenericDefault).Width;
                int num2 = (pnlDiplomacyTalkPanel.Width - num) / 2;
                picFlag.Size = new Size(50, 30);
                picFlag.Location = new Point(num2, 8);
                picFlag.SizeMode = PictureBoxSizeMode.Zoom;
                Bitmap image = PrecacheScaledBitmap(empire_5.LargeFlagPicture, 50, 30, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                picFlag.Image = image;
                lblDiplomacyTalkTitle.Location = new Point(num2 + 55, 10);
                picRace.Size = new Size(280, 280);
                picRace.Location = new Point((pnlDiplomacyTalkPanel.Width - 280) / 2, 45);
                picRace.SizeMode = PictureBoxSizeMode.CenterImage;
                Bitmap unscaledBitmap = method_118(empire_5, empire_5.DominantRace, 280, 280, bitmap_31, 7, empire_5.PirateEmpireBaseHabitat != null);
                Bitmap image2 = PrecacheScaledBitmap(unscaledBitmap, 282, 282, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                picRace.Image = image2;
                ctlDiplomacyTradeThem.SetFont(19f);
                ctlDiplomacyTradeUs.SetFont(19f);
                ctlDiplomacyTradeThem.Size = new Size(280, 710);
                ctlDiplomacyTradeThem.Location = new Point(10, 30);
                ctlDiplomacyTradeUs.Size = new Size(280, 710);
                ctlDiplomacyTradeUs.Location = new Point(720, 30);
                ctlDiplomacyTradeThem.Reset();
                ctlDiplomacyTradeUs.Reset();
                if ((tradeableItemList_6 != null && tradeableItemList_6.Count > 0) || (tradeableItemList_8 != null && tradeableItemList_8.Count > 0) || (tradeableItemList_10 != null && tradeableItemList_10.Count > 0))
                {
                    ctlDiplomacyTradeThem.SetSelectedItems(tradeableItemList_6, tradeableItemList_8, tradeableItemList_10);
                }
                if ((tradeableItemList_7 != null && tradeableItemList_7.Count > 0) || (tradeableItemList_9 != null && tradeableItemList_9.Count > 0) || (tradeableItemList_11 != null && tradeableItemList_11.Count > 0))
                {
                    ctlDiplomacyTradeUs.SetSelectedItems(tradeableItemList_7, tradeableItemList_9, tradeableItemList_11);
                }
                pnlDiplomaticConversationResponse.Size = new Size(390, 188);
                pnlDiplomaticConversationResponse.Location = new Point(10, 335);
                pnlDiplomaticConversationResponse.BackColor = Color.FromArgb(80, 0, 0, 0);
                ctlDiplomacyConversation.Size = new Size(390, 220);
                ctlDiplomacyConversation.Location = new Point(10, 528);
                ctlDiplomacyConversation.BackColor = Color.FromArgb(64, 0, 0, 0);
                pnlDiplomacyTalk.ResumeLayout();
                ResumeLayout();
                pnlDiplomacyTalk.Visible = true;
            }
        }

        private DesignList method_303()
        {
            DesignList designList = new DesignList();
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            list.Add(BuiltObjectSubRole.Escort);
            list.Add(BuiltObjectSubRole.Frigate);
            list.Add(BuiltObjectSubRole.Destroyer);
            list.Add(BuiltObjectSubRole.Cruiser);
            list.Add(BuiltObjectSubRole.CapitalShip);
            list.Add(BuiltObjectSubRole.TroopTransport);
            list.Add(BuiltObjectSubRole.Carrier);
            list.Add(BuiltObjectSubRole.ResupplyShip);
            list.Add(BuiltObjectSubRole.ExplorationShip);
            list.Add(BuiltObjectSubRole.SmallFreighter);
            list.Add(BuiltObjectSubRole.MediumFreighter);
            list.Add(BuiltObjectSubRole.LargeFreighter);
            list.Add(BuiltObjectSubRole.ColonyShip);
            list.Add(BuiltObjectSubRole.PassengerShip);
            list.Add(BuiltObjectSubRole.ConstructionShip);
            list.Add(BuiltObjectSubRole.GasMiningShip);
            list.Add(BuiltObjectSubRole.MiningShip);
            list.Add(BuiltObjectSubRole.GasMiningStation);
            list.Add(BuiltObjectSubRole.MiningStation);
            list.Add(BuiltObjectSubRole.Outpost);
            list.Add(BuiltObjectSubRole.SmallSpacePort);
            list.Add(BuiltObjectSubRole.MediumSpacePort);
            list.Add(BuiltObjectSubRole.LargeSpacePort);
            list.Add(BuiltObjectSubRole.ResortBase);
            list.Add(BuiltObjectSubRole.EnergyResearchStation);
            list.Add(BuiltObjectSubRole.WeaponsResearchStation);
            list.Add(BuiltObjectSubRole.HighTechResearchStation);
            list.Add(BuiltObjectSubRole.MonitoringStation);
            list.Add(BuiltObjectSubRole.DefensiveBase);
            foreach (BuiltObjectSubRole item in list)
            {
                Design design = null;
                design = ((_Game.PlayerEmpire.PirateEmpireBaseHabitat == null) ? _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(item, _Game.PlayerEmpire.Capital, includePlanetDestroyers: false) : _Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(item, null, includePlanetDestroyers: false));
                if (design != null)
                {
                    designList.Add(design);
                }
            }
            Design design2 = _Game.PlayerEmpire.Designs.FindNewestPlanetDestroyer();
            if (design2 != null && _Game.PlayerEmpire.CanBuildDesign(design2))
            {
                designList.Add(design2);
            }
            foreach (Design design3 in _Game.PlayerEmpire.Designs)
            {
                if (design3.SubRole == BuiltObjectSubRole.GenericBase && !design3.IsObsolete && _Game.PlayerEmpire.CanBuildDesign(design3))
                {
                    designList.Add(design3);
                }
            }
            return designList;
        }

        private DesignList method_304()
        {
            DesignList designList = new DesignList();
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            list.Add(BuiltObjectSubRole.Escort);
            list.Add(BuiltObjectSubRole.Frigate);
            list.Add(BuiltObjectSubRole.Destroyer);
            list.Add(BuiltObjectSubRole.Cruiser);
            list.Add(BuiltObjectSubRole.CapitalShip);
            list.Add(BuiltObjectSubRole.TroopTransport);
            list.Add(BuiltObjectSubRole.Carrier);
            list.Add(BuiltObjectSubRole.ResupplyShip);
            list.Add(BuiltObjectSubRole.ExplorationShip);
            list.Add(BuiltObjectSubRole.SmallFreighter);
            list.Add(BuiltObjectSubRole.MediumFreighter);
            list.Add(BuiltObjectSubRole.LargeFreighter);
            list.Add(BuiltObjectSubRole.ColonyShip);
            list.Add(BuiltObjectSubRole.PassengerShip);
            list.Add(BuiltObjectSubRole.ConstructionShip);
            list.Add(BuiltObjectSubRole.GasMiningShip);
            list.Add(BuiltObjectSubRole.MiningShip);
            list.Add(BuiltObjectSubRole.GasMiningStation);
            list.Add(BuiltObjectSubRole.MiningStation);
            list.Add(BuiltObjectSubRole.Outpost);
            list.Add(BuiltObjectSubRole.SmallSpacePort);
            list.Add(BuiltObjectSubRole.MediumSpacePort);
            list.Add(BuiltObjectSubRole.LargeSpacePort);
            list.Add(BuiltObjectSubRole.ResortBase);
            list.Add(BuiltObjectSubRole.EnergyResearchStation);
            list.Add(BuiltObjectSubRole.WeaponsResearchStation);
            list.Add(BuiltObjectSubRole.HighTechResearchStation);
            list.Add(BuiltObjectSubRole.MonitoringStation);
            list.Add(BuiltObjectSubRole.DefensiveBase);
            foreach (BuiltObjectSubRole item in list)
            {
                Design design = _Game.PlayerEmpire.Designs.FindNewestNonPlanetDestroyer(item);
                if (design != null)
                {
                    designList.Add(design);
                }
            }
            Design design2 = _Game.PlayerEmpire.Designs.FindNewestPlanetDestroyer();
            if (design2 != null)
            {
                designList.Add(design2);
            }
            foreach (Design design3 in _Game.PlayerEmpire.Designs)
            {
                if (design3.SubRole == BuiltObjectSubRole.GenericBase && !design3.IsObsolete)
                {
                    designList.Add(design3);
                }
            }
            return designList;
        }

        private string method_305()
        {
            string empty = string.Empty;
            int num = _Game.PlayerEmpire.MaximumConstructionSize();
            int num2 = _Game.PlayerEmpire.MaximumConstructionSize(BuiltObjectSubRole.SmallFreighter);
            int num3 = _Game.PlayerEmpire.MaximumConstructionSize(BuiltObjectSubRole.Frigate);
            string text = num.ToString();
            if (num2 != num)
            {
                text = text + ", C:" + num2;
            }
            if (num3 != num)
            {
                text = text + ", M:" + num3;
            }
            string text2 = empty;
            empty = text2 + TextResolver.GetText("Maximum Ship size") + ": " + text + "\n";
            string text3 = empty;
            return text3 + TextResolver.GetText("Maximum Base size") + ": " + _Game.PlayerEmpire.MaximumConstructionSizeBase() + " (" + TextResolver.GetText("when not at colony") + ")";
        }

        private void cmbDesignsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DesignList designs = method_306();
            ctlDesignsList.BindData(_Game.Galaxy, designs, builtObjectImageCache_0.GetImagesSmall(), allowMultiSelect: true);
        }

        private DesignList method_306()
        {
            DesignList designList = new DesignList();
            if (cmbDesignsFilter.Items != null && cmbDesignsFilter.Items.Count > 0)
            {
                switch (cmbDesignsFilter.SelectedIndex)
                {
                    case -1:
                    case 0:
                        designList = method_304();
                        break;
                    case 1:
                        designList = method_303();
                        break;
                    case 2:
                        designList = _Game.PlayerEmpire.Designs.GetCurrentDesigns();
                        break;
                    case 3:
                        designList = _Game.PlayerEmpire.Designs.GetCurrentDesignsBuildable(_Game.PlayerEmpire.Capital);
                        break;
                    case 4:
                        designList = _Game.PlayerEmpire.Designs;
                        break;
                }
                if (cmbDesignsFilterTypes.Items != null && cmbDesignsFilterTypes.Items.Count > 0)
                {
                    switch (cmbDesignsFilterTypes.SelectedIndex)
                    {
                        case 1:
                            designList = designList.GetDesignsByRolesNoObsoleteFilter(new List<BuiltObjectRole>
                    {
                        BuiltObjectRole.Military,
                        BuiltObjectRole.Exploration,
                        BuiltObjectRole.Colony,
                        BuiltObjectRole.Build
                    });
                            break;
                        case 2:
                            designList = designList.GetDesignsBySubRolesNoObsoleteFilter(new List<BuiltObjectSubRole>
                    {
                        BuiltObjectSubRole.DefensiveBase,
                        BuiltObjectSubRole.EnergyResearchStation,
                        BuiltObjectSubRole.GenericBase,
                        BuiltObjectSubRole.HighTechResearchStation,
                        BuiltObjectSubRole.LargeSpacePort,
                        BuiltObjectSubRole.MediumSpacePort,
                        BuiltObjectSubRole.MonitoringStation,
                        BuiltObjectSubRole.ResortBase,
                        BuiltObjectSubRole.Outpost,
                        BuiltObjectSubRole.SmallSpacePort,
                        BuiltObjectSubRole.WeaponsResearchStation
                    });
                            break;
                        case 3:
                            designList = designList.GetDesignsBySubRolesNoObsoleteFilter(new List<BuiltObjectSubRole>
                    {
                        BuiltObjectSubRole.GasMiningShip,
                        BuiltObjectSubRole.LargeFreighter,
                        BuiltObjectSubRole.MediumFreighter,
                        BuiltObjectSubRole.MiningShip,
                        BuiltObjectSubRole.PassengerShip,
                        BuiltObjectSubRole.SmallFreighter
                    });
                            break;
                        case 4:
                            designList = designList.GetDesignsBySubRolesNoObsoleteFilter(new List<BuiltObjectSubRole>
                    {
                        BuiltObjectSubRole.GasMiningStation,
                        BuiltObjectSubRole.MiningStation
                    });
                            break;
                    }
                }
            }
            else
            {
                designList = method_303();
            }
            return designList;
        }

        private void cmbDesignsFilterTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DesignList designs = method_306();
            ctlDesignsList.BindData(_Game.Galaxy, designs, builtObjectImageCache_0.GetImagesSmall(), allowMultiSelect: true);
        }

        private void btnDesignsShowEmpirePolicy_Click(object sender, EventArgs e)
        {
            method_595();
        }

        private void btnDesignsUpgradeManual_Click(object sender, EventArgs e)
        {
            DesignList selectedDesigns = ctlDesignsList.SelectedDesigns;
            if (selectedDesigns == null || selectedDesigns.Count != 1 || ctlDesignsList.Grid.SelectedRows == null || ctlDesignsList.Grid.SelectedRows.Count != 1)
            {
                return;
            }
            if (_Game.PlayerEmpire.ControlDesigns && _Game.PlayerEmpire.CheckDesignSubRoleShouldBeUpgraded(selectedDesigns[0].SubRole) && GenerateAutomationMessageBox(TextResolver.GetText("Ship Design")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                _Game.PlayerEmpire.ControlDesigns = false;
            }
            string_16 = "copyasnew";
            design_0 = selectedDesigns[0].Clone();
            design_0.IsObsolete = false;
            design_0.BuildCount = 0;
            design_0.DateCreated = _Game.Galaxy.CurrentStarDate;
            design_2 = selectedDesigns[0];
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

        public void method_307(Design design_3)
        {
            bool_9 = true;
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlDesigns.Size = new Size(980, 690);
            pnlDesigns.Location = new Point((mainView.Width - pnlDesigns.Width) / 2, (mainView.Height - pnlDesigns.Height) / 2);
            pnlDesigns.DoLayout();
            btnDesignsLoad.Size = new Size(155, 40);
            btnDesignsLoad.Location = new Point(635, 10);
            btnDesignsSave.Size = new Size(155, 40);
            btnDesignsSave.Location = new Point(800, 10);
            lblDesignsMaximumSize.Location = new Point(370, 15);
            lblDesignsMaximumSize.Text = method_305();
            cmbDesignsFilter.Size = new Size(165, 21);
            cmbDesignsFilter.Location = new Point(10, 18);
            cmbDesignsFilterTypes.Size = new Size(165, 21);
            cmbDesignsFilterTypes.Location = new Point(190, 18);
            ctlDesignsList.Size = new Size(945, 450);
            ctlDesignsList.Location = new Point(10, 122);
            if (cmbDesignsFilter.Items == null || cmbDesignsFilter.Items.Count <= 0)
            {
                cmbDesignsFilter.SelectedIndexChanged -= cmbDesignsFilter_SelectedIndexChanged;
                cmbDesignsFilter.Items.AddRange(new object[5]
                {
                TextResolver.GetText("Show Latest Designs"),
                TextResolver.GetText("Show Latest Buildable Designs"),
                TextResolver.GetText("Show Non-Obsolete Designs"),
                TextResolver.GetText("Show Buildable Non-Obsolete Designs"),
                TextResolver.GetText("Show All Designs")
                });
                cmbDesignsFilter.SelectedIndex = 1;
                cmbDesignsFilter.SelectedIndexChanged += cmbDesignsFilter_SelectedIndexChanged;
            }
            if (cmbDesignsFilterTypes.Items == null || cmbDesignsFilterTypes.Items.Count <= 0)
            {
                cmbDesignsFilterTypes.SelectedIndexChanged -= cmbDesignsFilterTypes_SelectedIndexChanged;
                cmbDesignsFilterTypes.Items.AddRange(new object[5]
                {
                TextResolver.GetText("Show All Design Types"),
                TextResolver.GetText("Show State Ships"),
                TextResolver.GetText("Show State Bases"),
                TextResolver.GetText("Show Private Ships"),
                TextResolver.GetText("Show Private Bases")
                });
                cmbDesignsFilterTypes.SelectedIndex = 0;
                cmbDesignsFilterTypes.SelectedIndexChanged += cmbDesignsFilterTypes_SelectedIndexChanged;
            }
            lblDesignsUpgradeRolesExplanation.Font = font_3;
            lblDesignsUpgradeRolesExplanation.Text = TextResolver.GetText("Designs Upgrade Roles Explanation");
            lblDesignsUpgradeRolesExplanation.Location = new Point(10, 60);
            lblDesignsUpgradeRolesExplanation.MaximumSize = new Size(615, 45);
            lblDesignsUpgradeRolesExplanation.Size = new Size(615, 45);
            lblDesignsUpgradeRolesExplanation.TextAlign = ContentAlignment.MiddleLeft;
            btnDesignsShowEmpirePolicy.Text = TextResolver.GetText("Show Empire Policy");
            btnDesignsShowEmpirePolicy.Size = new Size(320, 40);
            btnDesignsShowEmpirePolicy.Location = new Point(635, 62);
            DesignList designs = method_306();
            ctlDesignsList.BindData(_Game.Galaxy, designs, builtObjectImageCache_0.GetImagesSmall(), allowMultiSelect: true);
            ctlDesignsList.Grid.Columns["EmpirePicture"].Width = 22;
            ctlDesignsList.Grid.Columns["Picture"].Width = 30;
            ctlDesignsList.Grid.Columns["Name"].Width = 145;
            ctlDesignsList.Grid.Columns["Role"].Width = 80;
            ctlDesignsList.Grid.Columns["SubRole"].Width = 140;
            ctlDesignsList.Grid.Columns["Cost"].Width = 50;
            ctlDesignsList.Grid.Columns["Maintenance"].Width = 50;
            ctlDesignsList.Grid.Columns["DateCreated"].Width = 80;
            ctlDesignsList.Grid.Columns["Size"].Width = 45;
            ctlDesignsList.Grid.Columns["BuildCount"].Width = 55;
            ctlDesignsList.Grid.Columns["Upgrade"].Width = 65;
            ctlDesignsList.Grid.Columns["AutoRetrofit"].Width = 65;
            ctlDesignsList.Grid.Columns["Optimized"].Width = 70;
            ctlDesignsList.Grid.Columns["Obsolete"].Width = 40;
            ctlDesignsList.BringToFront();
            btnDesignsEdit.Size = new Size(128, 40);
            btnDesignsEdit.Location = new Point(10, 580);
            btnDesignsAddNew.Size = new Size(128, 40);
            btnDesignsAddNew.Location = new Point(148, 580);
            btnDesignsCopyAsNew.Size = new Size(128, 40);
            btnDesignsCopyAsNew.Location = new Point(286, 580);
            btnDesignsUpgradeManual.Text = TextResolver.GetText("Manually Upgrade Design");
            btnDesignsUpgradeManual.Size = new Size(170, 40);
            btnDesignsUpgradeManual.Location = new Point(424, 580);
            btnDesignsUpgrade.Text = TextResolver.GetText("Auto Upgrade Selected Designs");
            btnDesignsUpgrade.Size = new Size(170, 40);
            btnDesignsUpgrade.Location = new Point(604, 580);
            btnDesignsDelete.Text = TextResolver.GetText("Delete Selected Designs");
            btnDesignsDelete.Size = new Size(171, 40);
            btnDesignsDelete.Location = new Point(784, 580);
            if (design_3 != null)
            {
                ctlDesignsList.SelectDesign(design_3);
            }
            ctlDesignsList_SelectionChanged(null, null);
            pnlDesigns.Visible = true;
            pnlDesigns.BringToFront();
            ctlDesignsList.Focus();
        }

        private void ctlDesignsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ctlDesignsList.Grid.Columns[e.ColumnIndex].Name == "Obsolete")
            {
                if (e.RowIndex >= 0)
                {
                    Design design = ctlDesignsList.ResolveDesign(ctlDesignsList.Rows[e.RowIndex]);
                    if (design != null)
                    {
                        design.IsObsolete = !design.IsObsolete;
                        ctlDesignsList.SetObsolete(ctlDesignsList.Rows[e.RowIndex], design.IsObsolete);
                    }
                }
            }
            else if (ctlDesignsList.Grid.Columns[e.ColumnIndex].Name == "Upgrade")
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                Design design2 = ctlDesignsList.ResolveDesign(ctlDesignsList.Rows[e.RowIndex]);
                if (design2 == null)
                {
                    return;
                }
                bool flag = !(flag = _Game.PlayerEmpire.CheckDesignSubRoleShouldBeUpgraded(design2.SubRole));
                _Game.PlayerEmpire.SetDesignSubRoleShouldBeUpgraded(design2.SubRole, flag);
                string value = TextResolver.GetText("Automatic");
                if (!flag)
                {
                    value = TextResolver.GetText("Manual");
                }
                for (int i = 0; i < ctlDesignsList.Rows.Count; i++)
                {
                    Design design3 = ctlDesignsList.ResolveDesign(ctlDesignsList.Rows[i]);
                    if (design3 != null && design3.SubRole == design2.SubRole)
                    {
                        ctlDesignsList.Rows[i].Cells["Upgrade"].Value = value;
                    }
                }
            }
            else
            {
                if (!(ctlDesignsList.Grid.Columns[e.ColumnIndex].Name == "AutoRetrofit") || e.RowIndex < 0)
                {
                    return;
                }
                Design design4 = ctlDesignsList.ResolveDesign(ctlDesignsList.Rows[e.RowIndex]);
                if (design4 == null)
                {
                    return;
                }
                bool flag2 = true;
                switch (design4.SubRole)
                {
                    case BuiltObjectSubRole.SmallFreighter:
                    case BuiltObjectSubRole.MediumFreighter:
                    case BuiltObjectSubRole.LargeFreighter:
                    case BuiltObjectSubRole.PassengerShip:
                    case BuiltObjectSubRole.GasMiningShip:
                    case BuiltObjectSubRole.MiningShip:
                        flag2 = false;
                        break;
                }
                if (!flag2)
                {
                    return;
                }
                design4.AllowAutoRetrofit = !design4.AllowAutoRetrofit;
                Empire empire = design4.Empire;
                if (empire != null)
                {
                    if (empire.BuiltObjects != null)
                    {
                        for (int j = 0; j < empire.BuiltObjects.Count; j++)
                        {
                            BuiltObject builtObject = empire.BuiltObjects[j];
                            if (builtObject != null && builtObject.Design == design4)
                            {
                                builtObject.SuppressAutoRetrofit = !design4.AllowAutoRetrofit;
                            }
                        }
                    }
                    if (empire.PrivateBuiltObjects != null)
                    {
                        for (int k = 0; k < empire.PrivateBuiltObjects.Count; k++)
                        {
                            BuiltObject builtObject2 = empire.PrivateBuiltObjects[k];
                            if (builtObject2 != null && builtObject2.Design == design4)
                            {
                                builtObject2.SuppressAutoRetrofit = !design4.AllowAutoRetrofit;
                            }
                        }
                    }
                }
                string value2 = TextResolver.GetText("Automatic");
                if (!design4.AllowAutoRetrofit)
                {
                    value2 = TextResolver.GetText("Manual");
                }
                ctlDesignsList.Rows[e.RowIndex].Cells["AutoRetrofit"].Value = value2;
            }
        }

        private void method_308()
        {
            _Game.PlayerEmpire.ReviewLatestDesigns();
            DesignList currentDesigns = _Game.PlayerEmpire.Designs.GetCurrentDesigns();
            string text = string.Empty;
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Freight, BuiltObjectSubRole.SmallFreighter) && currentDesigns.FindNewest(BuiltObjectSubRole.SmallFreighter) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.SmallFreighter) + "\n";
            }
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Freight, BuiltObjectSubRole.MediumFreighter) && currentDesigns.FindNewest(BuiltObjectSubRole.MediumFreighter) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.MediumFreighter) + "\n";
            }
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Freight, BuiltObjectSubRole.LargeFreighter) && currentDesigns.FindNewest(BuiltObjectSubRole.LargeFreighter) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.LargeFreighter) + "\n";
            }
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Resource, BuiltObjectSubRole.GasMiningShip) && currentDesigns.FindNewest(BuiltObjectSubRole.GasMiningShip) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.GasMiningShip) + "\n";
            }
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Resource, BuiltObjectSubRole.MiningShip) && currentDesigns.FindNewest(BuiltObjectSubRole.MiningShip) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.MiningShip) + "\n";
            }
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.GasMiningStation) && currentDesigns.FindNewest(BuiltObjectSubRole.GasMiningStation) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.GasMiningStation) + "\n";
            }
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.MiningStation) && currentDesigns.FindNewest(BuiltObjectSubRole.MiningStation) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.MiningStation) + "\n";
            }
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Passenger, BuiltObjectSubRole.PassengerShip) && currentDesigns.FindNewest(BuiltObjectSubRole.PassengerShip) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.PassengerShip) + "\n";
            }
            if (_Game.PlayerEmpire.CheckDesignComponentsAvailable(BuiltObjectRole.Colony, BuiltObjectSubRole.ColonyShip) && currentDesigns.FindNewest(BuiltObjectSubRole.ColonyShip) == null)
            {
                text = text + Galaxy.ResolveDescription(BuiltObjectSubRole.ColonyShip) + "\n";
            }
            if (!string.IsNullOrEmpty(text))
            {
                text = string.Format(TextResolver.GetText("Warning - missing critical designs detail"), text);
                MessageBoxEx messageBoxEx = method_371(text, TextResolver.GetText("Warning - missing critical designs"), MessageBoxExIcon.Warning);
                messageBoxEx.Show(this);
            }
            Galaxy.ApplyDesignUpgradePoliciesToGameOptions(gameOptions_0, _Game.PlayerEmpire.Policy);
            pnlDesigns.SendToBack();
            pnlDesigns.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void btnCycleShipGroups_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (shipGroup_0 != null)
            {
                num = _Game.PlayerEmpire.ShipGroups.IndexOf(shipGroup_0);
                num++;
                if (num >= _Game.PlayerEmpire.ShipGroups.Count)
                {
                    num = 0;
                }
            }
            if (num >= 0 && _Game.PlayerEmpire.ShipGroups.Count > num)
            {
                shipGroup_0 = _Game.PlayerEmpire.ShipGroups[num];
            }
            else
            {
                shipGroup_0 = null;
            }
            if (shipGroup_0 != null)
            {
                method_208(shipGroup_0);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            double num = double_0;
            num -= num * ((double)_Game.MainViewZoomSpeed / 100.0);
            if (num < double_4)
            {
                num = double_4;
            }
            if (num > 10000.0)
            {
                num = 10000.0;
            }
            method_4(num);
            Focus();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            double num = double_0;
            num += num * ((double)_Game.MainViewZoomSpeed / 100.0);
            if (num < double_4)
            {
                num = double_4;
            }
            if (num > double_5)
            {
                num = double_5;
            }
            method_4(num);
            Focus();
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            CaLkaMyrMQ.Select();
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                method_154();
            }
            else
            {
                method_155();
            }
        }

        private void btnEmpireSummary_Click(object sender, EventArgs e)
        {
            if (pnlEmpireSummary.Visible)
            {
                method_275();
            }
            else
            {
                method_274();
            }
        }

        private void actionMenu_Opening(object sender, CancelEventArgs e)
        {
            Point point = PointToClient(MouseHelper.GetCursorPosition());
            if (!bool_9 && !bool_10 && !bool_16)
            {
                Keyboard keyboard = new Keyboard();
                if (shipAction_0 != null && !keyboard.CtrlKeyDown)
                {
                    e.Cancel = true;
                    return;
                }
                if (itemListCollectionPanel_0.DetectHoveredElement(point))
                {
                    e.Cancel = true;
                    return;
                }
                Empire empire = null;
                bool flag = false;
                if (_Game.SelectedObject != null)
                {
                    if (_Game.SelectedObject is BuiltObject)
                    {
                        empire = ((BuiltObject)_Game.SelectedObject).Empire;
                    }
                    if (_Game.SelectedObject is BuiltObjectList)
                    {
                        empire = ((BuiltObjectList)_Game.SelectedObject)[0].Empire;
                    }
                    else if (_Game.SelectedObject is Habitat)
                    {
                        Habitat habitat = (Habitat)_Game.SelectedObject;
                        empire = habitat.Empire;
                        if (_Game.PlayerEmpire != null)
                        {
                            List<HabitatType> colonizableHabitatTypes = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpire(_Game.PlayerEmpire);
                            Design latestColonyShip = _Game.PlayerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                            flag = _Game.PlayerEmpire.CanEmpireColonizeHabitat(_Game.PlayerEmpire, habitat, colonizableHabitatTypes, latestColonyShip);
                        }
                    }
                    else if (_Game.SelectedObject is ShipGroup)
                    {
                        empire = ((ShipGroup)_Game.SelectedObject).Empire;
                    }
                }
                if (flag || empire != null)
                {
                    method_344();
                    BaconMain.GenerateToolStripItems(this);
                }
                else
                { actionMenu.Items.Clear(); }
                AddMenuItems(point);
                //e.Cancel = true;
                //if (_Game.SelectedObject != null && (flag || empire != null) && actionMenu.Items != null && actionMenu.Items.Count > 0)
                //{
                //    e.Cancel = false;
                //}
            }
            else
            {
                e.Cancel = true;
            }
        }

        private ToolStripMenuItem method_309(string string_30, object object_7)
        {
            return method_311(string_30, string.Empty, object_7, bool_28: false);
        }

        private ToolStripMenuItem method_310(string string_30, object object_7, bool bool_28)
        {
            return method_311(string_30, string.Empty, object_7, bool_28);
        }

        private ToolStripMenuItem method_311(string string_30, string string_31, object object_7, bool bool_28)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            string empty = string.Empty;
            object obj = object_7;
            if (object_7 is ShipAction)
            {
                ShipAction shipAction = (ShipAction)object_7;
                obj = shipAction.Target;
                if (obj == null)
                {
                    if (shipAction.Position.X != 0 && shipAction.Position.Y != 0)
                    {
                        obj = shipAction.Position;
                    }
                    else
                    {
                        toolStripMenuItem = new ToolStripMenuItem(string_30);
                    }
                }
            }
            if (obj != null)
            {
                if (obj is Habitat)
                {
                    Habitat habitat = (Habitat)obj;
                    if (habitat.Category != 0)
                    {
                        empty = Galaxy.ResolveDescription(habitat.Category);
                        empty = empty + " " + habitat.Name;
                    }
                    else
                    {
                        SystemVisibilityStatus systemVisibilityStatus = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                        empty = ((systemVisibilityStatus != SystemVisibilityStatus.Unexplored) ? habitat.Name : TextResolver.GetText("Unknown star"));
                    }
                    toolStripMenuItem = new ToolStripMenuItem(string.Format(string_30, empty));
                }
                else if (obj is BuiltObject)
                {
                    empty = ((BuiltObject)obj).Name;
                    toolStripMenuItem = new ToolStripMenuItem(string.Format(string_30, empty));
                }
                else if (obj is Creature)
                {
                    empty = ((Creature)obj).Name;
                    toolStripMenuItem = new ToolStripMenuItem(string.Format(string_30, empty));
                }
                else if (obj is ShipGroup)
                {
                    empty = ((ShipGroup)obj).Name;
                    toolStripMenuItem = new ToolStripMenuItem(string.Format(string_30, empty));
                }
                else if (!(obj is SystemInfo))
                {
                    toolStripMenuItem = ((!(obj is Point)) ? new ToolStripMenuItem(string_30) : new ToolStripMenuItem(string.Format(string_31, empty)));
                }
                else
                {
                    SystemInfo systemInfo = (SystemInfo)obj;
                    SystemVisibilityStatus systemVisibilityStatus2 = _Game.PlayerEmpire.CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
                    empty = ((systemVisibilityStatus2 != SystemVisibilityStatus.Unexplored) ? systemInfo.SystemStar.Name : TextResolver.GetText("Unknown system"));
                    toolStripMenuItem = new ToolStripMenuItem(string.Format(string_30, empty));
                }
            }
            if (object_7 != null && toolStripMenuItem != null && bool_28)
            {
                toolStripMenuItem.Tag = object_7;
            }
            if (toolStripMenuItem != null)
            {
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
                toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_312(string string_30)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(string_30);
            if (toolStripMenuItem != null)
            {
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
                toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            }
            return toolStripMenuItem;
        }

        private ShipAction method_313(ShipActionType shipActionType_0, object object_7)
        {
            return new ShipAction(shipActionType_0, object_7);
        }

        private ShipAction method_314(BuiltObjectMissionType builtObjectMissionType_0)
        {
            return new ShipAction(builtObjectMissionType_0, null, new Point(0, 0), null);
        }

        public ShipAction method_315(BuiltObjectMissionType builtObjectMissionType_0, object object_7)
        {
            if (object_7 != null)
            {
                int num = 0;
                int num2 = 0;
                if (object_7 is Habitat)
                {
                    num = int_15 - (int)((Habitat)object_7).Xpos;
                    num2 = int_16 - (int)((Habitat)object_7).Ypos;
                }
                else if (object_7 is SystemInfo)
                {
                    num = int_15 - (int)((SystemInfo)object_7).SystemStar.Xpos;
                    num2 = int_16 - (int)((SystemInfo)object_7).SystemStar.Ypos;
                    object_7 = ((SystemInfo)object_7).SystemStar;
                }
                else if (object_7 is Creature)
                {
                    num = int_15 - (int)((Creature)object_7).Xpos;
                    num2 = int_16 - (int)((Creature)object_7).Ypos;
                }
                Point offset = new Point(num, num2);
                return new ShipAction(builtObjectMissionType_0, object_7, offset, null);
            }
            int num3 = int_15;
            int num4 = int_16;
            Point offset2 = new Point(num3, num4);
            return new ShipAction(builtObjectMissionType_0, null, offset2, null);
        }

        private ToolStripMenuItem method_316(BuiltObject builtObject_8)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            if (builtObject_8 != null)
            {
                DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                {
                    for (int i = 0; i < _Game.PlayerEmpire.DiplomaticRelations.Count; i++)
                    {
                        DiplomaticRelation diplomaticRelation = _Game.PlayerEmpire.DiplomaticRelations[i];
                        if (diplomaticRelation.Type != 0 && diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire != _Game.PlayerEmpire && !empireList.Contains(diplomaticRelation.OtherEmpire))
                        {
                            empireList.Add(diplomaticRelation.OtherEmpire);
                        }
                    }
                }
                for (int j = 0; j < _Game.PlayerEmpire.PirateRelations.Count; j++)
                {
                    PirateRelation pirateRelation = _Game.PlayerEmpire.PirateRelations[j];
                    if (pirateRelation.Type != 0 && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire != _Game.PlayerEmpire && !empireList.Contains(pirateRelation.OtherEmpire) && !empireList.Contains(pirateRelation.OtherEmpire))
                    {
                        empireList.Add(pirateRelation.OtherEmpire);
                    }
                }
                empireList.Add(_Game.Galaxy.IndependentEmpire);
                empireList.Sort();
                if (empireList.Count > 0)
                {
                    toolStripMenuItem = method_312(TextResolver.GetText("Give to"));
                    ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = true;
                    for (int k = 0; k < empireList.Count; k++)
                    {
                        ShipAction shipAction = new ShipAction(ShipActionType.GiveBuiltObject, builtObject_8);
                        shipAction.Target2 = empireList[k];
                        ToolStripMenuItem toolStripMenuItem2 = method_311(empireList[k].Name, empireList[k].Name, shipAction, bool_28: true);
                        toolStripMenuItem2.Image = empireList[k].SmallFlagPicture;
                        toolStripMenuItem2.ImageScaling = ToolStripItemImageScaling.None;
                        toolStripMenuItem2.ImageAlign = ContentAlignment.MiddleRight;
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                }
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_317(BuiltObject builtObject_8)
        {
            ToolStripMenuItem result = null;
            if (builtObject_8 != null && builtObject_8.Role == BuiltObjectRole.Base && builtObject_8.Empire != _Game.PlayerEmpire)
            {
                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(builtObject_8, EmpireActivityType.Attack))
                {
                    ShipAction object_ = new ShipAction(ShipActionType.GeneratePirateMissionAttack, builtObject_8);
                    string text = string.Format(arg1: _Game.PlayerEmpire.CalculatePirateAttackPrice(builtObject_8).ToString("0"), format: TextResolver.GetText("Assign Mercenary Attack Mission"), arg0: builtObject_8.Name);
                    result = method_311(text, text, object_, bool_28: true);
                }
                else if (_Game.Galaxy.PirateMissions.ContainsEquivalent(builtObject_8, EmpireActivityType.Attack))
                {
                    string text2 = string.Format(TextResolver.GetText("Cancel Mercenary Attack Mission"), builtObject_8.Name);
                    ShipAction object_2 = new ShipAction(ShipActionType.GeneratePirateMissionAttack, builtObject_8);
                    result = method_311(text2, text2, object_2, bool_28: true);
                }
            }
            return result;
        }

        private ToolStripMenuItem method_318(Habitat habitat_9)
        {
            ToolStripMenuItem result = null;
            if (habitat_9 != null && habitat_9.Empire == _Game.PlayerEmpire)
            {
                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(habitat_9, EmpireActivityType.Defend))
                {
                    ShipAction object_ = new ShipAction(ShipActionType.GeneratePirateMissionDefend, habitat_9);
                    string text = string.Format(arg1: _Game.PlayerEmpire.CalculatePirateDefendPrice(habitat_9).ToString("0"), format: TextResolver.GetText("Assign Mercenary Defense Mission"), arg0: habitat_9.Name);
                    result = method_311(text, text, object_, bool_28: true);
                }
                else if (_Game.Galaxy.PirateMissions.ContainsEquivalent(habitat_9, EmpireActivityType.Defend))
                {
                    string text2 = string.Format(TextResolver.GetText("Cancel Mercenary Defense Mission"), habitat_9.Name);
                    ShipAction object_2 = new ShipAction(ShipActionType.GeneratePirateMissionDefend, habitat_9);
                    result = method_311(text2, text2, object_2, bool_28: true);
                }
            }
            return result;
        }

        private ToolStripMenuItem method_319(BuiltObject builtObject_8)
        {
            ToolStripMenuItem result = null;
            if (builtObject_8 != null && builtObject_8.Role == BuiltObjectRole.Base && builtObject_8.Empire == _Game.PlayerEmpire)
            {
                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(builtObject_8, EmpireActivityType.Defend))
                {
                    ShipAction object_ = new ShipAction(ShipActionType.GeneratePirateMissionDefend, builtObject_8);
                    string text = string.Format(arg1: _Game.PlayerEmpire.CalculatePirateDefendPrice(builtObject_8).ToString("0"), format: TextResolver.GetText("Assign Mercenary Defense Mission"), arg0: builtObject_8.Name);
                    result = method_311(text, text, object_, bool_28: true);
                }
                else if (_Game.Galaxy.PirateMissions.ContainsEquivalent(builtObject_8, EmpireActivityType.Defend))
                {
                    string text2 = string.Format(TextResolver.GetText("Cancel Mercenary Defense Mission"), builtObject_8.Name);
                    ShipAction object_2 = new ShipAction(ShipActionType.GeneratePirateMissionDefend, builtObject_8);
                    result = method_311(text2, text2, object_2, bool_28: true);
                }
            }
            return result;
        }

        private ToolStripMenuItem method_320(Habitat habitat_9)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            if (habitat_9 != null && habitat_9.Empire == _Game.PlayerEmpire)
            {
                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(habitat_9, EmpireActivityType.Smuggle))
                {
                    string string_ = string.Format(TextResolver.GetText("Assign Mercenary Smuggling Mission"), habitat_9.Name);
                    toolStripMenuItem = method_312(string_);
                    ShipAction shipAction = new ShipAction(ShipActionType.GeneratePirateMissionSmuggling, habitat_9);
                    shipAction.Target2 = byte.MaxValue;
                    string_ = string.Format(TextResolver.GetText("Smuggling Mission for RESOURCE for PRICE"), "100.0", TextResolver.GetText("All Resources"));
                    ToolStripMenuItem value = method_311(string_, string_, shipAction, bool_28: true);
                    toolStripMenuItem.DropDownItems.Add(value);
                    for (int i = 0; i < _Game.Galaxy.ResourceSystem.Resources.Count; i++)
                    {
                        shipAction = new ShipAction(ShipActionType.GeneratePirateMissionSmuggling, habitat_9);
                        shipAction.Target2 = _Game.Galaxy.ResourceSystem.Resources[i].ResourceID;
                        double num = _Game.PlayerEmpire.CalculatePirateSmugglePricePerUnit(habitat_9, _Game.Galaxy.ResourceSystem.Resources[i].ResourceID);
                        string_ = string.Format(TextResolver.GetText("Smuggling Mission for RESOURCE for PRICE"), (num * 100.0).ToString("0.0"), _Game.Galaxy.ResourceSystem.Resources[i].Name);
                        value = method_311(string_, string_, shipAction, bool_28: true);
                        toolStripMenuItem.DropDownItems.Add(value);
                    }
                }
                else
                {
                    string text = string.Format(TextResolver.GetText("Cancel Mercenary Smuggling Mission"), habitat_9.Name);
                    ShipAction shipAction2 = new ShipAction(ShipActionType.GeneratePirateMissionSmuggling, habitat_9);
                    shipAction2.Target2 = byte.MaxValue;
                    toolStripMenuItem = method_311(text, text, shipAction2, bool_28: true);
                }
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_321(ShipGroup shipGroup_3)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            ShipAction shipAction = new ShipAction(ShipActionType.TransferCharacter, shipGroup_3.LeadShip);
            string text = string.Format(TextResolver.GetText("Transfer Character to LOCATION"), shipGroup_3.Name);
            toolStripMenuItem = method_311(text, text, shipAction, bool_28: false);
            CharacterList characterList = new CharacterList();
            CharacterList characterList2 = new CharacterList();
            if (shipGroup_3 != null && shipGroup_3.Empire == _Game.PlayerEmpire && shipGroup_3.Empire.Characters != null)
            {
                characterList2 = shipGroup_3.Empire.Characters.GetFleetAdmiralsAndGenerals(shipGroup_3);
                characterList.AddRange(shipGroup_3.Empire.Characters.GetNonTransferringCharacters(CharacterRole.FleetAdmiral));
                characterList.AddRange(shipGroup_3.Empire.Characters.GetNonTransferringCharacters(CharacterRole.TroopGeneral));
                characterList.AddRange(shipGroup_3.Empire.Characters.GetNonTransferringCharacters(CharacterRole.PirateLeader));
            }
            for (int i = 0; i < characterList.Count; i++)
            {
                Character character = characterList[i];
                if (characterList2.Contains(character))
                {
                    continue;
                }
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                toolStripMenuItem2.Text = character.Name + "  (" + Galaxy.ResolveDescription(character.Role) + ")";
                ShipAction shipAction2 = shipAction.Clone();
                shipAction2.Target2 = character;
                if (character.Role == CharacterRole.TroopGeneral)
                {
                    BuiltObject builtObject = shipGroup_3.DetermineStrongestTroopTransport();
                    if (builtObject != null)
                    {
                        shipAction2.Target = builtObject;
                    }
                }
                toolStripMenuItem2.Tag = shipAction2;
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
            }
            if (characterList.Count <= 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_322(StellarObject stellarObject_0)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            ShipAction shipAction = new ShipAction(ShipActionType.TransferCharacter, stellarObject_0);
            string text = string.Format(TextResolver.GetText("Transfer Character to LOCATION"), stellarObject_0.Name);
            toolStripMenuItem = method_311(text, text, shipAction, bool_28: false);
            CharacterList characterList = _Game.Galaxy.ResolveCharactersValidForLocation(stellarObject_0, _Game.PlayerEmpire);
            for (int i = 0; i < characterList.Count; i++)
            {
                Character character = characterList[i];
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                toolStripMenuItem2.Text = character.Name + "  (" + Galaxy.ResolveDescription(character.Role) + ")";
                ShipAction shipAction2 = shipAction.Clone();
                shipAction2.Target2 = character;
                toolStripMenuItem2.Tag = shipAction2;
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
            }
            if (characterList.Count <= 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_323(BuiltObject builtObject_8)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            ShipAction shipAction = method_315(BuiltObjectMissionType.Build, null);
            toolStripMenuItem = method_311(TextResolver.GetText("Build here"), TextResolver.GetText("Build here"), shipAction, bool_28: false);
            object obj = method_143(int_15, int_16, bool_28: true);
            bool flag = false;
            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
            {
                Habitat habitat = null;
                bool flag2 = true;
                List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                if (obj != null && (obj is Habitat || obj is SystemInfo))
                {
                    bool flag3 = true;
                    bool flag4 = true;
                    if (obj is Habitat)
                    {
                        Habitat habitat2 = (Habitat)obj;
                        if (habitat2.Population != null && habitat2.Population.Count > 0 && habitat2.Empire != _Game.PlayerEmpire)
                        {
                            flag3 = false;
                            flag4 = false;
                            flag2 = false;
                        }
                        if (_Game.Galaxy.CheckAlreadyHaveMiningStationAtHabitat(habitat2, builtObject_8.Empire))
                        {
                            flag4 = false;
                        }
                        if (_Game.Galaxy.DetermineSpacePortAtHabitat(habitat2) != null)
                        {
                            flag3 = false;
                        }
                        habitat = habitat2;
                    }
                    else if (obj is SystemInfo)
                    {
                        SystemInfo systemInfo = (SystemInfo)obj;
                        if (systemInfo.SystemStar != null && systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                        {
                            if (_Game.Galaxy.CheckAlreadyHaveMiningStationAtHabitat(systemInfo.SystemStar, builtObject_8.Empire))
                            {
                                flag4 = false;
                            }
                            if (_Game.Galaxy.DetermineSpacePortAtHabitat(systemInfo.SystemStar) != null)
                            {
                                flag3 = false;
                            }
                            habitat = systemInfo.SystemStar;
                        }
                    }
                    if (flag3)
                    {
                        list.Add(BuiltObjectSubRole.Outpost);
                        list.Add(BuiltObjectSubRole.SmallSpacePort);
                        list.Add(BuiltObjectSubRole.MediumSpacePort);
                        list.Add(BuiltObjectSubRole.LargeSpacePort);
                    }
                    if (flag4)
                    {
                        list.Add(BuiltObjectSubRole.MiningStation);
                        list.Add(BuiltObjectSubRole.GasMiningStation);
                    }
                }
                if (flag2)
                {
                    list.Add(BuiltObjectSubRole.ResortBase);
                    list.Add(BuiltObjectSubRole.GenericBase);
                    list.Add(BuiltObjectSubRole.MonitoringStation);
                    list.Add(BuiltObjectSubRole.DefensiveBase);
                }
                DesignList buildableDesignsBySubRoles = builtObject_8.Empire.Designs.GetBuildableDesignsBySubRoles(list, _Game.PlayerEmpire);
                DesignList buildablePlanetDestroyerDesigns = builtObject_8.Empire.Designs.GetBuildablePlanetDestroyerDesigns(_Game.PlayerEmpire);
                if (buildablePlanetDestroyerDesigns != null && buildablePlanetDestroyerDesigns.Count > 0)
                {
                    buildableDesignsBySubRoles.AddRange(buildablePlanetDestroyerDesigns);
                }
                foreach (Design item in buildableDesignsBySubRoles)
                {
                    if (builtObject_8.Empire.CanBuildDesign(item) && item.Size <= _Game.PlayerEmpire.MaximumConstructionSizeBase(item.SubRole))
                    {
                        flag = true;
                        ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                        string text = Galaxy.ResolveDescription(item.SubRole) + ": " + item.Name;
                        double num = item.CalculateCurrentPurchasePrice(_Game.Galaxy);
                        string text2 = text;
                        text = (toolStripMenuItem2.Text = text2 + " (" + num.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                        ShipAction shipAction2 = shipAction.Clone();
                        shipAction2.Design = item;
                        if (habitat != null)
                        {
                            shipAction2.Target = habitat;
                            _Game.Galaxy.SelectRelativePoint((double)habitat.Diameter / 2.5, out var num2, out var num3);
                            shipAction2.Position = new Point((int)num2, (int)num3);
                        }
                        toolStripMenuItem2.Tag = shipAction2;
                        if (num > _Game.PlayerEmpire.StateMoney)
                        {
                            toolStripMenuItem2.Enabled = false;
                        }
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                }
            }
            else if (_Game.Galaxy.CheckEmpireTerritoryCanBuildAtLocation(builtObject_8.Empire, int_15, int_16))
            {
                Habitat habitat3 = null;
                List<BuiltObjectSubRole> list2 = new List<BuiltObjectSubRole>();
                if (obj != null && (obj is Habitat || obj is SystemInfo))
                {
                    bool flag5 = true;
                    if (obj is Habitat)
                    {
                        if (_Game.Galaxy.CheckAlreadyHaveMiningStationAtHabitat((Habitat)obj, builtObject_8.Empire))
                        {
                            flag5 = false;
                        }
                        habitat3 = (Habitat)obj;
                    }
                    else if (obj is SystemInfo)
                    {
                        SystemInfo systemInfo2 = (SystemInfo)obj;
                        if (systemInfo2.SystemStar != null && systemInfo2.SystemStar.Category == HabitatCategoryType.GasCloud)
                        {
                            if (_Game.Galaxy.CheckAlreadyHaveMiningStationAtHabitat(systemInfo2.SystemStar, builtObject_8.Empire))
                            {
                                flag5 = false;
                            }
                            habitat3 = systemInfo2.SystemStar;
                        }
                    }
                    if (flag5)
                    {
                        list2.Add(BuiltObjectSubRole.MiningStation);
                        list2.Add(BuiltObjectSubRole.GasMiningStation);
                    }
                }
                list2.Add(BuiltObjectSubRole.ResortBase);
                list2.Add(BuiltObjectSubRole.GenericBase);
                list2.Add(BuiltObjectSubRole.EnergyResearchStation);
                list2.Add(BuiltObjectSubRole.WeaponsResearchStation);
                list2.Add(BuiltObjectSubRole.HighTechResearchStation);
                list2.Add(BuiltObjectSubRole.MonitoringStation);
                list2.Add(BuiltObjectSubRole.DefensiveBase);
                DesignList buildableDesignsBySubRoles2 = builtObject_8.Empire.Designs.GetBuildableDesignsBySubRoles(list2, _Game.PlayerEmpire);
                DesignList buildablePlanetDestroyerDesigns2 = builtObject_8.Empire.Designs.GetBuildablePlanetDestroyerDesigns(_Game.PlayerEmpire);
                if (buildablePlanetDestroyerDesigns2 != null && buildablePlanetDestroyerDesigns2.Count > 0)
                {
                    buildableDesignsBySubRoles2.AddRange(buildablePlanetDestroyerDesigns2);
                }
                foreach (Design item2 in buildableDesignsBySubRoles2)
                {
                    if (builtObject_8.Empire.CanBuildDesign(item2) && item2.Size <= _Game.PlayerEmpire.MaximumConstructionSizeBase(item2.SubRole))
                    {
                        flag = true;
                        ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                        string text4 = Galaxy.ResolveDescription(item2.SubRole) + ": " + item2.Name;
                        double num4 = item2.CalculateCurrentPurchasePrice(_Game.Galaxy);
                        string text5 = text4;
                        text4 = (toolStripMenuItem3.Text = text5 + " (" + num4.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                        ShipAction shipAction3 = shipAction.Clone();
                        shipAction3.Design = item2;
                        if (habitat3 != null)
                        {
                            shipAction3.Target = habitat3;
                            _Game.Galaxy.SelectRelativePoint((double)habitat3.Diameter / 2.5, out var num5, out var num6);
                            shipAction3.Position = new Point((int)num5, (int)num6);
                        }
                        toolStripMenuItem3.Tag = shipAction3;
                        if (num4 > _Game.PlayerEmpire.StateMoney)
                        {
                            toolStripMenuItem3.Enabled = false;
                        }
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                    }
                }
            }
            if (!flag)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_324()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int num = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Move to"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            Habitat habitat = _Game.Galaxy.FastFindNearestSystem(int_15, int_16);
            if (habitat != null)
            {
                SystemVisibilityStatus systemVisibilityStatus = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                ShipAction tag = new ShipAction(BuiltObjectMissionType.Move, habitat);
                string empty = string.Empty;
                empty = ((systemVisibilityStatus != SystemVisibilityStatus.Unexplored) ? habitat.Name : TextResolver.GetText("Unknown"));
                switch (habitat.Category)
                {
                    case HabitatCategoryType.GasCloud:
                        empty = empty + " " + TextResolver.GetText("Gas Cloud");
                        break;
                    case HabitatCategoryType.Star:
                        empty = empty + " " + TextResolver.GetText("System");
                        break;
                }
                toolStripMenuItem2.Text = empty;
                toolStripMenuItem2.Tag = tag;
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
            }
            HabitatList habitatList = method_136(int_15, int_16, num);
            foreach (Habitat item in habitatList)
            {
                ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                ShipAction tag2 = new ShipAction(BuiltObjectMissionType.Move, item);
                toolStripMenuItem3.Text = item.Name;
                toolStripMenuItem3.Tag = tag2;
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
            }
            BuiltObjectList builtObjectList = method_135(int_15, int_16, num);
            foreach (BuiltObject item2 in builtObjectList)
            {
                ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
                ShipAction tag3 = new ShipAction(BuiltObjectMissionType.Move, item2);
                toolStripMenuItem4.Text = item2.Name;
                toolStripMenuItem4.Tag = tag3;
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem4);
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_325()
        {
            return method_326(BuiltObjectMissionType.Bombard);
        }

        private ToolStripMenuItem method_326(BuiltObjectMissionType builtObjectMissionType_0)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int int_ = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            method_315(builtObjectMissionType_0, null);
            switch (builtObjectMissionType_0)
            {
                case BuiltObjectMissionType.Bombard:
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Bombard"));
                    break;
                case BuiltObjectMissionType.WaitAndBombard:
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Prepare and Bombard"));
                    break;
            }
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            HabitatList habitatList = method_136(int_15, int_16, int_);
            foreach (Habitat item in habitatList)
            {
                if (item.Empire != _Game.PlayerEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(builtObjectMissionType_0, item);
                    string name = TextResolver.GetText("Lost");
                    if (item.Empire != null)
                    {
                        name = item.Empire.Name;
                    }
                    toolStripMenuItem2.Text = item.Name + " (" + name + ")";
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_327()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int int_ = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            method_315(BuiltObjectMissionType.Capture, null);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Capture"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            BuiltObjectList builtObjectList = method_135(int_15, int_16, int_);
            foreach (BuiltObject item in builtObjectList)
            {
                if (item.Empire != _Game.PlayerEmpire && item.Empire != _Game.Galaxy.IndependentEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(BuiltObjectMissionType.Capture, item);
                    string name = TextResolver.GetText("Abandoned");
                    if (item.Empire != null)
                    {
                        name = item.Empire.Name;
                    }
                    toolStripMenuItem2.Text = item.Name + " (" + name + ")";
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_328()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int num = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            method_315(BuiltObjectMissionType.Raid, null);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Raid"));
            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
            {
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
                toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
                HabitatList habitatList = method_138(int_15, int_16, num, _Game.PlayerEmpire);
                foreach (Habitat item in habitatList)
                {
                    if (item.Empire != _Game.PlayerEmpire)
                    {
                        ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                        ShipAction tag = new ShipAction(BuiltObjectMissionType.Raid, item);
                        string name = TextResolver.GetText("Abandoned");
                        if (item.Empire != null)
                        {
                            name = item.Empire.Name;
                        }
                        toolStripMenuItem2.Text = item.Name + " (" + name + ")";
                        toolStripMenuItem2.Tag = tag;
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                }
                BuiltObjectList builtObjectList = method_135(int_15, int_16, num);
                foreach (BuiltObject item2 in builtObjectList)
                {
                    if (item2.Empire != _Game.PlayerEmpire && item2.Empire != _Game.Galaxy.IndependentEmpire)
                    {
                        ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                        ShipAction tag2 = new ShipAction(BuiltObjectMissionType.Raid, item2);
                        string name2 = TextResolver.GetText("Abandoned");
                        if (item2.Empire != null)
                        {
                            name2 = item2.Empire.Name;
                        }
                        toolStripMenuItem3.Text = item2.Name + " (" + name2 + ")";
                        toolStripMenuItem3.Tag = tag2;
                        if (item2.RaidCountdown > 0)
                        {
                            toolStripMenuItem3.Enabled = false;
                        }
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                    }
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_329()
        {
            return method_330(BuiltObjectMissionType.Attack);
        }

        private ToolStripMenuItem method_330(BuiltObjectMissionType builtObjectMissionType_0)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int num = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            method_315(builtObjectMissionType_0, null);
            switch (builtObjectMissionType_0)
            {
                case BuiltObjectMissionType.Attack:
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Attack"));
                    break;
                case BuiltObjectMissionType.WaitAndAttack:
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Prepare and Attack"));
                    break;
            }
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            HabitatList habitatList = method_136(int_15, int_16, num);
            foreach (Habitat item in habitatList)
            {
                if (item.Empire != _Game.PlayerEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(builtObjectMissionType_0, item);
                    string name = TextResolver.GetText("Lost");
                    if (item.Empire != null)
                    {
                        name = item.Empire.Name;
                    }
                    toolStripMenuItem2.Text = item.Name + " (" + name + ")";
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            BuiltObjectList builtObjectList = method_135(int_15, int_16, num);
            foreach (BuiltObject item2 in builtObjectList)
            {
                if (item2.Empire != _Game.PlayerEmpire && item2.Empire != _Game.Galaxy.IndependentEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                    ShipAction tag2 = new ShipAction(builtObjectMissionType_0, item2);
                    string name2 = TextResolver.GetText("Abandoned");
                    if (item2.Empire != null)
                    {
                        name2 = item2.Empire.Name;
                    }
                    toolStripMenuItem3.Text = item2.Name + " (" + name2 + ")";
                    toolStripMenuItem3.Tag = tag2;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                }
            }
            ShipGroupList shipGroupList = method_133(int_15, int_16, num);
            foreach (ShipGroup item3 in shipGroupList)
            {
                if (item3.Empire != _Game.PlayerEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
                    ShipAction tag3 = new ShipAction(builtObjectMissionType_0, item3);
                    string name3 = TextResolver.GetText("Abandoned");
                    if (item3.Empire != null)
                    {
                        name3 = item3.Empire.Name;
                    }
                    toolStripMenuItem4.Text = item3.Name + " (" + name3 + ")";
                    toolStripMenuItem4.Tag = tag3;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem4);
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_331()
        {
            return method_332(BuiltObjectMissionType.Attack);
        }

        private ToolStripMenuItem method_332(BuiltObjectMissionType builtObjectMissionType_0)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int int_ = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            switch (builtObjectMissionType_0)
            {
                case BuiltObjectMissionType.Attack:
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Attack"));
                    break;
                case BuiltObjectMissionType.WaitAndAttack:
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Prepare and Attack"));
                    break;
            }
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            HabitatList habitatList = method_136(int_15, int_16, int_);
            foreach (Habitat item in habitatList)
            {
                if (item.Empire != _Game.PlayerEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(builtObjectMissionType_0, item);
                    toolStripMenuItem2.Text = item.Name;
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_333()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int num = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Patrol"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            object obj = method_143(int_15, int_16, bool_28: false);
            if (obj != null && obj is SystemInfo)
            {
                SystemInfo systemInfo = (SystemInfo)obj;
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                ShipAction tag = new ShipAction(BuiltObjectMissionType.Patrol, systemInfo);
                toolStripMenuItem2.Text = systemInfo.SystemStar.Name + " " + TextResolver.GetText("system");
                toolStripMenuItem2.Tag = tag;
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
            }
            HabitatList habitatList = method_136(int_15, int_16, num);
            foreach (Habitat item in habitatList)
            {
                if (item.Empire == _Game.PlayerEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                    ShipAction tag2 = new ShipAction(BuiltObjectMissionType.Patrol, item);
                    toolStripMenuItem3.Text = item.Name;
                    toolStripMenuItem3.Tag = tag2;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                }
            }
            BuiltObjectList builtObjectList = method_135(int_15, int_16, num);
            foreach (BuiltObject item2 in builtObjectList)
            {
                if (item2.Empire == _Game.PlayerEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
                    ShipAction tag3 = new ShipAction(BuiltObjectMissionType.Patrol, item2);
                    toolStripMenuItem4.Text = item2.Name;
                    toolStripMenuItem4.Tag = tag3;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem4);
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_334()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int num = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Blockade"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            HabitatList habitatList = method_137(int_15, int_16, num, bool_28: false);
            foreach (Habitat item in habitatList)
            {
                if (item.Empire != _Game.PlayerEmpire && _Game.PlayerEmpire.CanSendShipToBlockadeColony(item))
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(BuiltObjectMissionType.Blockade, item);
                    string name = TextResolver.GetText("Lost");
                    if (item.Empire != null)
                    {
                        name = item.Empire.Name;
                    }
                    toolStripMenuItem2.Text = item.Name + " (" + name + ")";
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            BuiltObjectList builtObjectList = method_135(int_15, int_16, num);
            foreach (BuiltObject item2 in builtObjectList)
            {
                if (item2.Empire != _Game.PlayerEmpire && _Game.PlayerEmpire.CanSendShipToBlockadeBuiltObject(item2))
                {
                    ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                    ShipAction tag2 = new ShipAction(BuiltObjectMissionType.Blockade, item2);
                    string name2 = TextResolver.GetText("Abandoned");
                    if (item2.Empire != null)
                    {
                        name2 = item2.Empire.Name;
                    }
                    toolStripMenuItem3.Text = item2.Name + " (" + name2 + ")";
                    toolStripMenuItem3.Tag = tag2;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_335()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int int_ = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Colonize"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            if (_Game.SelectedObject is BuiltObject)
            {
                BuiltObject builtObject_ = (BuiltObject)_Game.SelectedObject;
                HabitatList habitatList = method_139(builtObject_, int_15, int_16, int_);
                foreach (Habitat item in habitatList)
                {
                    if (item != null && _Game.PlayerEmpire.CheckSystemExplored(item.SystemIndex) && item.Owner != _Game.PlayerEmpire)
                    {
                        ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                        ShipAction tag = new ShipAction(BuiltObjectMissionType.Colonize, item);
                        toolStripMenuItem2.Text = item.Name;
                        toolStripMenuItem2.Tag = tag;
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_336()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int num = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Refuel at"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            BuiltObjectList builtObjectList = method_135(int_15, int_16, num);
            foreach (BuiltObject item in builtObjectList)
            {
                if (item.Owner == _Game.PlayerEmpire && item.IsRefuellingDepot)
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(BuiltObjectMissionType.Refuel, item);
                    toolStripMenuItem2.Text = item.Name;
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            HabitatList habitatList = method_136(int_15, int_16, num);
            foreach (Habitat item2 in habitatList)
            {
                if (item2.Owner == _Game.PlayerEmpire && item2.IsRefuellingDepot)
                {
                    ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                    ShipAction tag2 = new ShipAction(BuiltObjectMissionType.Refuel, item2);
                    toolStripMenuItem3.Text = item2.Name;
                    toolStripMenuItem3.Tag = tag2;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_337()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int int_ = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Escort"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            BuiltObjectList builtObjectList = method_134(int_15, int_16, int_);
            foreach (BuiltObject item in builtObjectList)
            {
                if (item.Owner == _Game.PlayerEmpire && (item.Role == BuiltObjectRole.Build || item.Role == BuiltObjectRole.Colony || item.Role == BuiltObjectRole.Exploration))
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(BuiltObjectMissionType.Escort, item);
                    toolStripMenuItem2.Text = item.Name;
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_338(BuiltObject builtObject_8, bool bool_28)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int int_ = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Load Troops"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            HabitatList habitatList = method_136(int_15, int_16, int_);
            foreach (Habitat item in habitatList)
            {
                if (item.Owner == _Game.PlayerEmpire)
                {
                    if (item.Troops == null || item.Troops.Count <= 0)
                    {
                        continue;
                    }
                    foreach (Troop troop in item.Troops)
                    {
                        if (troop != null && troop.Empire == _Game.PlayerEmpire && !troop.Garrisoned)
                        {
                            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                            ShipAction tag = new ShipAction(BuiltObjectMissionType.LoadTroops, item);
                            toolStripMenuItem2.Text = string.Format(TextResolver.GetText("At X"), item.Name);
                            toolStripMenuItem2.Tag = tag;
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                            break;
                        }
                    }
                }
                else
                {
                    if (item.Owner == _Game.PlayerEmpire || item.InvadingTroops == null || item.InvadingTroops.Count <= 0 || item.InvadingTroops[0].Empire != _Game.PlayerEmpire || item.InvadingTroops == null || item.InvadingTroops.Count <= 0)
                    {
                        continue;
                    }
                    foreach (Troop invadingTroop in item.InvadingTroops)
                    {
                        if (invadingTroop != null && invadingTroop.Empire == _Game.PlayerEmpire)
                        {
                            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                            ShipAction tag2 = new ShipAction(BuiltObjectMissionType.LoadTroops, item);
                            toolStripMenuItem3.Text = string.Format(TextResolver.GetText("At X"), item.Name);
                            toolStripMenuItem3.Tag = tag2;
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                            break;
                        }
                    }
                }
            }
            Habitat habitat = builtObject_8.Empire.FindNearestColonyWithExcessTroops(builtObject_8, enforceMinimumTroopLimits: false);
            if (habitat != null)
            {
                ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
                ShipAction tag3 = new ShipAction(BuiltObjectMissionType.LoadTroops, habitat);
                toolStripMenuItem4.Text = TextResolver.GetText("At nearest colony with available troops");
                if (bool_28)
                {
                    tag3 = new ShipAction(BuiltObjectMissionType.LoadTroops, null);
                }
                toolStripMenuItem4.Tag = tag3;
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem4);
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_339()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            int int_ = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Unload Troops at"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            HabitatList habitatList = method_136(int_15, int_16, int_);
            foreach (Habitat item in habitatList)
            {
                if (item.Empire == _Game.PlayerEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(BuiltObjectMissionType.UnloadTroops, item);
                    toolStripMenuItem2.Text = item.Name;
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    break;
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_340()
        {
            ToolStripMenuItem toolStripMenuItem = null;
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Change Colony Tax"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            int int_ = (int)((double)Galaxy.MaxSolarSystemSize * 3.0);
            HabitatList habitatList = method_136(int_15, int_16, int_);
            foreach (Habitat item in habitatList)
            {
                if (item.Empire == _Game.PlayerEmpire)
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                    ShipAction tag = new ShipAction(ShipActionType.ColonyTaxUp5, item);
                    toolStripMenuItem2.Text = "+5%";
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    toolStripMenuItem2 = new ToolStripMenuItem();
                    tag = new ShipAction(ShipActionType.ColonyTaxUp1, item);
                    toolStripMenuItem2.Text = "+1%";
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    toolStripMenuItem2 = new ToolStripMenuItem();
                    tag = new ShipAction(ShipActionType.ColonyTaxDown1, item);
                    toolStripMenuItem2.Text = "-1%";
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    toolStripMenuItem2 = new ToolStripMenuItem();
                    tag = new ShipAction(ShipActionType.ColonyTaxDown5, item);
                    toolStripMenuItem2.Text = "-5%";
                    toolStripMenuItem2.Tag = tag;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    break;
                }
            }
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private void method_341(ToolStripMenuItem toolStripMenuItem_0)
        {
            if (toolStripMenuItem_0.Tag is ShipAction)
            {
                ShipAction shipAction = (ShipAction)toolStripMenuItem_0.Tag;
                shipAction.IsSubsequentAction = true;
            }
            if (toolStripMenuItem_0.DropDownItems == null || toolStripMenuItem_0.DropDownItems.Count <= 0)
            {
                return;
            }
            foreach (ToolStripItem dropDownItem in toolStripMenuItem_0.DropDownItems)
            {
                if (dropDownItem is ToolStripMenuItem)
                {
                    ToolStripMenuItem toolStripMenuItem_ = (ToolStripMenuItem)dropDownItem;
                    method_341(toolStripMenuItem_);
                }
            }
        }

        private ToolStripMenuItem method_342(BuiltObject builtObject_8)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Queue Next Mission"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            toolStripMenuItem.Font = font_3;
            ToolStripMenuItem toolStripMenuItem2 = null;
            if (builtObject_8.Owner == _Game.PlayerEmpire && builtObject_8.SubRole != BuiltObjectSubRole.ColonyShip)
            {
                object obj = method_143(int_15, int_16, bool_28: true);
                if (double_0 > 100.0)
                {
                    toolStripMenuItem2 = method_324();
                    if (toolStripMenuItem2 != null)
                    {
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                    if (builtObject_8.ConstructionQueue != null && builtObject_8.ConstructionQueue.ConstructionYards.Count > 0)
                    {
                        toolStripMenuItem2 = method_323(builtObject_8);
                        if (toolStripMenuItem2 != null)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                    bool flag = false;
                    if (builtObject_8.FirepowerRaw > 0 || builtObject_8.FighterCapacity > 0)
                    {
                        toolStripMenuItem2 = method_329();
                        if (toolStripMenuItem2 != null)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                            flag = true;
                        }
                        toolStripMenuItem2 = method_333();
                        if (toolStripMenuItem2 != null)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                        toolStripMenuItem2 = method_337();
                        if (toolStripMenuItem2 != null)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                        toolStripMenuItem2 = method_334();
                        if (toolStripMenuItem2 != null)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                    if (builtObject_8.Troops != null && builtObject_8.Troops.TotalAttackStrength > 0)
                    {
                        if (!flag)
                        {
                            toolStripMenuItem2 = method_331();
                            if (toolStripMenuItem2 != null)
                            {
                                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                            }
                        }
                        toolStripMenuItem2 = method_339();
                        if (toolStripMenuItem2 != null)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                    if (builtObject_8.TroopCapacityRemaining >= 100)
                    {
                        toolStripMenuItem2 = method_338(builtObject_8, bool_28: false);
                        if (toolStripMenuItem2 != null)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                    if (builtObject_8.SensorResourceProfileSensorRange > 0)
                    {
                        Habitat habitat = _Game.Galaxy.FastFindNearestSystem(int_15, int_16);
                        double num = _Game.Galaxy.CalculateDistance(int_15, int_16, habitat.Xpos, habitat.Ypos);
                        if (num > (double)Galaxy.MaxSolarSystemSize)
                        {
                            habitat = null;
                        }
                        toolStripMenuItem2 = method_309(TextResolver.GetText("Explore"), BuiltObjectMissionType.Explore);
                        ShipAction shipAction = method_314(BuiltObjectMissionType.Explore);
                        if (habitat != null)
                        {
                            ShipAction shipAction2 = shipAction.Clone();
                            shipAction2.Target = habitat;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("This system") + " ({0})", shipAction2, bool_28: true));
                        }
                        else if (obj != null && obj is SystemInfo)
                        {
                            ShipAction shipAction3 = shipAction.Clone();
                            shipAction3.Target = ((SystemInfo)obj).SystemStar;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("This system") + " ({0})", shipAction3, bool_28: true));
                        }
                        Habitat habitat2 = _Game.Galaxy.FastFindNearestUnexploredHabitat(builtObject_8.Xpos, builtObject_8.Ypos, builtObject_8.ActualEmpire);
                        if (habitat2 != null)
                        {
                            habitat2 = Galaxy.DetermineHabitatSystemStar(habitat2);
                            ShipAction shipAction4 = shipAction.Clone();
                            shipAction4.Target = habitat2;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("Nearest unexplored system"), shipAction4, bool_28: true));
                        }
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                    if (builtObject_8.SubRole == BuiltObjectSubRole.ColonyShip && builtObject_8.Empire != null && builtObject_8.Empire.PirateEmpireBaseHabitat == null)
                    {
                        toolStripMenuItem2 = method_335();
                        if (toolStripMenuItem2 != null)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                    if (builtObject_8.DamagedComponentCount > 0)
                    {
                        toolStripMenuItem2 = method_309(TextResolver.GetText("Repair"), BuiltObjectMissionType.Repair);
                        ShipAction shipAction5 = method_314(BuiltObjectMissionType.Repair);
                        if (obj != null && obj is BuiltObject && ((BuiltObject)obj).Empire == builtObject_8.Empire && ((BuiltObject)obj).IsShipYard && builtObject_8 != obj)
                        {
                            ShipAction shipAction6 = shipAction5.Clone();
                            shipAction6.Target = obj;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction6, bool_28: true));
                        }
                        StellarObject stellarObject = builtObject_8.Empire.FindNearestShipYard(builtObject_8, canRepairOrBuild: true, includeVerySmallYards: true);
                        if (stellarObject != null)
                        {
                            ShipAction shipAction7 = shipAction5.Clone();
                            shipAction7.Target = stellarObject;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction7, bool_28: true));
                        }
                        if (toolStripMenuItem2.DropDownItems != null && toolStripMenuItem2.DropDownItems.Count > 0)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                }
                else
                {
                    Habitat habitat3 = _Game.Galaxy.FastFindNearestSystem(int_15, int_16);
                    double num2 = _Game.Galaxy.CalculateDistance(int_15, int_16, habitat3.Xpos, habitat3.Ypos);
                    if (num2 > (double)Galaxy.MaxSolarSystemSize)
                    {
                        habitat3 = null;
                    }
                    bool flag2 = false;
                    //ShipAction shipAction8 = null;
                    toolStripMenuItem2 = method_311(object_7: (obj == builtObject_8) ? method_315(BuiltObjectMissionType.Move, null) : method_315(BuiltObjectMissionType.Move, obj), string_30: TextResolver.GetText("Move to X"), string_31: TextResolver.GetText("Move here"), bool_28: true);
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    if ((builtObject_8.FirepowerRaw > 0 || builtObject_8.FighterCapacity > 0) && obj != builtObject_8)
                    {
                        if (obj != null && ((obj is Habitat && ((Habitat)obj).Empire != null && ((Habitat)obj).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj).Empire != builtObject_8.Empire) || (obj is BuiltObject && ((BuiltObject)obj).Empire != builtObject_8.Empire) || (obj is ShipGroup && ((ShipGroup)obj).Empire != builtObject_8.Empire) || obj is Creature))
                        {
                            ShipAction object_2 = method_315(BuiltObjectMissionType.Attack, obj);
                            toolStripMenuItem2 = method_310(TextResolver.GetText("Attack X"), object_2, bool_28: true);
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                            flag2 = true;
                        }
                        if (obj != null && (obj is Habitat || (obj is BuiltObject && ((BuiltObject)obj).Role == BuiltObjectRole.Base)))
                        {
                            ShipAction object_3 = method_315(BuiltObjectMissionType.Patrol, obj);
                            toolStripMenuItem2 = method_310(TextResolver.GetText("Patrol X"), object_3, bool_28: true);
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                        if (obj != null && obj is BuiltObject && ((BuiltObject)obj).Role != BuiltObjectRole.Base && ((BuiltObject)obj).Empire == builtObject_8.Empire)
                        {
                            ShipAction object_4 = method_315(BuiltObjectMissionType.Escort, obj);
                            toolStripMenuItem2 = method_310(TextResolver.GetText("Escort X"), object_4, bool_28: true);
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                        if (obj != null && ((obj is Habitat && ((Habitat)obj).Empire != null && ((Habitat)obj).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj).Empire != builtObject_8.Empire) || (obj is BuiltObject && ((BuiltObject)obj).Role == BuiltObjectRole.Base && ((BuiltObject)obj).Empire != builtObject_8.Empire)))
                        {
                            bool flag3 = true;
                            if (obj is Habitat)
                            {
                                flag3 = builtObject_8.Empire.CanSendShipToBlockadeColony((Habitat)obj);
                            }
                            else if (obj is BuiltObject)
                            {
                                flag3 = builtObject_8.Empire.CanSendShipToBlockadeBuiltObject((BuiltObject)obj);
                            }
                            if (flag3)
                            {
                                ShipAction object_5 = method_315(BuiltObjectMissionType.Blockade, obj);
                                toolStripMenuItem2 = method_310(TextResolver.GetText("Blockade X"), object_5, bool_28: true);
                                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                            }
                        }
                    }
                    if (builtObject_8.Troops != null && builtObject_8.Troops.TotalAttackStrength > 0 && obj != builtObject_8)
                    {
                        if (!flag2 && obj != null && ((obj is Habitat && ((Habitat)obj).Empire != builtObject_8.Empire) || (obj is BuiltObject && ((BuiltObject)obj).Empire != builtObject_8.Empire) || (obj is ShipGroup && ((ShipGroup)obj).Empire != builtObject_8.Empire)))
                        {
                            ShipAction object_6 = method_315(BuiltObjectMissionType.Attack, obj);
                            toolStripMenuItem2 = method_310(TextResolver.GetText("Attack X"), object_6, bool_28: true);
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                        if (obj != null && obj is Habitat && ((Habitat)obj).Empire == builtObject_8.Empire)
                        {
                            ShipAction object_7 = method_315(BuiltObjectMissionType.UnloadTroops, obj);
                            toolStripMenuItem2 = method_310(TextResolver.GetText("Unload Troops at X"), object_7, bool_28: true);
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                    if (builtObject_8.TroopCapacityRemaining >= 100)
                    {
                        toolStripMenuItem2 = method_309(TextResolver.GetText("Load Troops"), BuiltObjectMissionType.LoadTroops);
                        ShipAction shipAction9 = method_314(BuiltObjectMissionType.LoadTroops);
                        if (obj != null && obj is Habitat && ((Habitat)obj).Empire == builtObject_8.Empire)
                        {
                            ShipAction shipAction10 = shipAction9.Clone();
                            shipAction10.Target = obj;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction10, bool_28: true));
                        }
                        Habitat habitat4 = builtObject_8.Empire.FindNearestColonyWithExcessTroops(builtObject_8, enforceMinimumTroopLimits: false);
                        if (habitat4 != null)
                        {
                            ShipAction shipAction11 = shipAction9.Clone();
                            shipAction11.Target = habitat4;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At nearest colony"), shipAction11, bool_28: true));
                        }
                        if (toolStripMenuItem2.DropDownItems != null && toolStripMenuItem2.DropDownItems.Count > 0)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                    if (builtObject_8.SensorResourceProfileSensorRange > 0)
                    {
                        toolStripMenuItem2 = method_309(TextResolver.GetText("Explore"), BuiltObjectMissionType.Explore);
                        ShipAction shipAction12 = method_314(BuiltObjectMissionType.Explore);
                        if (obj != null && obj is Habitat)
                        {
                            ShipAction shipAction13 = shipAction12.Clone();
                            shipAction13.Target = obj;
                            toolStripMenuItem2.DropDownItems.Add(method_310("{0}", shipAction13, bool_28: true));
                        }
                        if (habitat3 != null)
                        {
                            ShipAction shipAction14 = shipAction12.Clone();
                            shipAction14.Target = habitat3;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("This system") + " ({0})", shipAction14, bool_28: true));
                        }
                        else if (obj != null && obj is SystemInfo)
                        {
                            ShipAction shipAction15 = shipAction12.Clone();
                            shipAction15.Target = ((SystemInfo)obj).SystemStar;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("This system") + " ({0})", shipAction15, bool_28: true));
                        }
                        Habitat habitat5 = _Game.Galaxy.FindNearestUnexploredHabitat(builtObject_8.Xpos, builtObject_8.Ypos, builtObject_8.ActualEmpire, includeAsteroids: true);
                        if (habitat5 != null)
                        {
                            habitat5 = Galaxy.DetermineHabitatSystemStar(habitat5);
                            ShipAction shipAction16 = shipAction12.Clone();
                            shipAction16.Target = habitat5;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("Nearest unexplored system"), shipAction16, bool_28: true));
                        }
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                    if (builtObject_8.ConstructionQueue != null && builtObject_8.ConstructionQueue.ConstructionYards.Count > 0)
                    {
                        ShipAction shipAction17 = null;
                        if (obj != builtObject_8)
                        {
                            if (obj is Habitat)
                            {
                                if (_Game.Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(builtObject_8.Empire, (Habitat)obj))
                                {
                                    shipAction17 = method_315(BuiltObjectMissionType.Build, obj);
                                    toolStripMenuItem2 = method_310(TextResolver.GetText("Build at X"), shipAction17, bool_28: false);
                                    List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                                    bool flag4 = true;
                                    if (_Game.Galaxy.CheckAlreadyHaveMiningStationAtHabitat((Habitat)obj, builtObject_8.Empire))
                                    {
                                        flag4 = false;
                                    }
                                    if (flag4)
                                    {
                                        list.Add(BuiltObjectSubRole.MiningStation);
                                        list.Add(BuiltObjectSubRole.GasMiningStation);
                                    }
                                    list.Add(BuiltObjectSubRole.GenericBase);
                                    list.Add(BuiltObjectSubRole.ResortBase);
                                    list.Add(BuiltObjectSubRole.EnergyResearchStation);
                                    list.Add(BuiltObjectSubRole.WeaponsResearchStation);
                                    list.Add(BuiltObjectSubRole.HighTechResearchStation);
                                    list.Add(BuiltObjectSubRole.MonitoringStation);
                                    list.Add(BuiltObjectSubRole.DefensiveBase);
                                    DesignList buildableDesignsBySubRoles = builtObject_8.Empire.Designs.GetBuildableDesignsBySubRoles(list, _Game.PlayerEmpire);
                                    foreach (Design item in buildableDesignsBySubRoles)
                                    {
                                        if (flag4 || (item.ExtractionGas <= 0 && item.ExtractionLuxury <= 0 && item.ExtractionMine <= 0))
                                        {
                                            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                                            string text = Galaxy.ResolveDescription(item.SubRole) + ": " + item.Name;
                                            double num3 = item.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                            string text2 = text;
                                            text = (toolStripMenuItem3.Text = text2 + " (" + num3.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                                            ShipAction shipAction18 = shipAction17.Clone();
                                            shipAction18.Design = item;
                                            toolStripMenuItem3.Tag = shipAction18;
                                            if (num3 > _Game.PlayerEmpire.StateMoney)
                                            {
                                                toolStripMenuItem3.Enabled = false;
                                            }
                                            toolStripMenuItem2.DropDownItems.Add(toolStripMenuItem3);
                                        }
                                    }
                                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                                }
                            }
                            else if (obj == null)
                            {
                                shipAction17 = method_315(BuiltObjectMissionType.Build, null);
                                toolStripMenuItem2 = method_311(TextResolver.GetText("Build here"), TextResolver.GetText("Build here"), shipAction17, bool_28: false);
                                bool flag5 = false;
                                if (_Game.Galaxy.CheckEmpireTerritoryCanBuildAtLocation(builtObject_8.Empire, int_15, int_16))
                                {
                                    List<BuiltObjectSubRole> list2 = new List<BuiltObjectSubRole>();
                                    list2.Add(BuiltObjectSubRole.GenericBase);
                                    list2.Add(BuiltObjectSubRole.EnergyResearchStation);
                                    list2.Add(BuiltObjectSubRole.WeaponsResearchStation);
                                    list2.Add(BuiltObjectSubRole.HighTechResearchStation);
                                    list2.Add(BuiltObjectSubRole.MonitoringStation);
                                    list2.Add(BuiltObjectSubRole.DefensiveBase);
                                    DesignList buildableDesignsBySubRoles2 = builtObject_8.Empire.Designs.GetBuildableDesignsBySubRoles(list2, _Game.PlayerEmpire);
                                    foreach (Design item2 in buildableDesignsBySubRoles2)
                                    {
                                        if (item2.Size <= _Game.PlayerEmpire.MaximumConstructionSizeBase(item2.SubRole))
                                        {
                                            flag5 = true;
                                            ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
                                            string text4 = Galaxy.ResolveDescription(item2.SubRole) + ": " + item2.Name;
                                            double num4 = item2.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                            string text2 = text4;
                                            text4 = (toolStripMenuItem4.Text = text2 + " (" + num4.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                                            ShipAction shipAction19 = shipAction17.Clone();
                                            shipAction19.Design = item2;
                                            toolStripMenuItem4.Tag = shipAction19;
                                            if (num4 > _Game.PlayerEmpire.StateMoney)
                                            {
                                                toolStripMenuItem4.Enabled = false;
                                            }
                                            toolStripMenuItem2.DropDownItems.Add(toolStripMenuItem4);
                                        }
                                    }
                                }
                                if (flag5)
                                {
                                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                                }
                            }
                        }
                    }
                    if (builtObject_8.SubRole == BuiltObjectSubRole.ColonyShip && builtObject_8.Components[ComponentType.HabitationColonization, ComponentStatus.Normal] != null && obj != null && obj is Habitat && (((Habitat)obj).Empire == null || ((Habitat)obj).Empire == _Game.Galaxy.IndependentEmpire))
                    {
                        Habitat habitat6 = (Habitat)obj;
                        int newPopulationAmount = 0;
                        if (builtObject_8.Empire.CanBuiltObjectColonizeHabitat(builtObject_8, habitat6, out newPopulationAmount))
                        {
                            ShipAction object_8 = method_315(BuiltObjectMissionType.Colonize, obj);
                            toolStripMenuItem2 = method_310(TextResolver.GetText("Colonize X"), object_8, bool_28: true);
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                    if ((builtObject_8.ExtractionGas > 0 || builtObject_8.ExtractionLuxury > 0 || builtObject_8.ExtractionMine > 0) && obj != null && obj is Habitat && ((Habitat)obj).Category != 0 && (((Habitat)obj).Empire == null || ((Habitat)obj).Empire == _Game.Galaxy.IndependentEmpire))
                    {
                        ShipAction object_9 = method_315(BuiltObjectMissionType.ExtractResources, obj);
                        toolStripMenuItem2 = method_310(TextResolver.GetText("Mine X"), object_9, bool_28: true);
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                    if (builtObject_8.DamagedComponentCount > 0)
                    {
                        toolStripMenuItem2 = method_309(TextResolver.GetText("Repair"), BuiltObjectMissionType.Repair);
                        ShipAction shipAction20 = method_314(BuiltObjectMissionType.Repair);
                        if (obj != null && obj is BuiltObject && ((BuiltObject)obj).Empire == builtObject_8.Empire && ((BuiltObject)obj).IsShipYard && builtObject_8 != obj)
                        {
                            ShipAction shipAction21 = shipAction20.Clone();
                            shipAction21.Target = obj;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction21, bool_28: true));
                        }
                        StellarObject stellarObject2 = builtObject_8.Empire.FindNearestShipYard(builtObject_8, canRepairOrBuild: true, includeVerySmallYards: true);
                        if (stellarObject2 != null)
                        {
                            ShipAction shipAction22 = shipAction20.Clone();
                            shipAction22.Target = stellarObject2;
                            toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction22, bool_28: true));
                        }
                        if (toolStripMenuItem2.DropDownItems != null && toolStripMenuItem2.DropDownItems.Count > 0)
                        {
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                        }
                    }
                }
                toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
                toolStripMenuItem2 = method_309(TextResolver.GetText("Refuel"), BuiltObjectMissionType.Refuel);
                ShipAction shipAction23 = method_314(BuiltObjectMissionType.Refuel);
                if (obj != null && ((obj is BuiltObject && ((BuiltObject)obj).IsRefuellingDepot) || (obj is Habitat && ((Habitat)obj).IsRefuellingDepot)) && builtObject_8 != obj)
                {
                    ShipAction shipAction24 = shipAction23.Clone();
                    shipAction24.Target = obj;
                    toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction24, bool_28: true));
                }
                StellarObject stellarObject3 = null;
                ResourceList fuelTypes = builtObject_8.DetermineFuelRequired(setFuelLevelToZero: true);
                stellarObject3 = ((builtObject_8.Role != BuiltObjectRole.Military) ? _Game.Galaxy.FastFindNearestRefuellingPoint(builtObject_8.Xpos, builtObject_8.Ypos, fuelTypes, builtObject_8.ActualEmpire, builtObject_8) : _Game.Galaxy.FastFindNearestRefuellingPoint(builtObject_8.Xpos, builtObject_8.Ypos, fuelTypes, builtObject_8.ActualEmpire, builtObject_8, includeResupplyShips: true, null));
                if (stellarObject3 != null)
                {
                    if (stellarObject3 is BuiltObject)
                    {
                        ShipAction shipAction25 = shipAction23.Clone();
                        shipAction25.Target = (BuiltObject)stellarObject3;
                        toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction25, bool_28: true));
                    }
                    else if (stellarObject3 is Habitat)
                    {
                        ShipAction shipAction26 = shipAction23.Clone();
                        shipAction26.Target = (Habitat)stellarObject3;
                        toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction26, bool_28: true));
                    }
                }
                if (toolStripMenuItem2.DropDownItems != null && toolStripMenuItem2.DropDownItems.Count > 0)
                {
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
                ShipAction object_10 = new ShipAction(ShipActionType.ClearQueuedMissions, null);
                toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("Clear All Queued Missions"), object_10, bool_28: true));
            }
            method_341(toolStripMenuItem);
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private ToolStripMenuItem method_343(ShipGroup shipGroup_3)
        {
            ToolStripMenuItem toolStripMenuItem = null;
            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Queue Next Mission"));
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
            ToolStripMenuItem toolStripMenuItem2 = null;
            object obj = method_143(int_15, int_16, bool_28: true);
            if (double_0 > 100.0)
            {
                toolStripMenuItem2 = method_324();
                if (toolStripMenuItem2 != null)
                {
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
                toolStripMenuItem2 = method_329();
                if (toolStripMenuItem2 != null)
                {
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
                toolStripMenuItem2 = method_330(BuiltObjectMissionType.WaitAndAttack);
                if (toolStripMenuItem2 != null)
                {
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
                toolStripMenuItem2 = method_333();
                if (toolStripMenuItem2 != null)
                {
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
                toolStripMenuItem2 = method_337();
                if (toolStripMenuItem2 != null)
                {
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
                toolStripMenuItem2 = method_334();
                if (toolStripMenuItem2 != null)
                {
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            else
            {
                //ShipAction shipAction = null;
                toolStripMenuItem2 = method_311(object_7: (obj == shipGroup_3) ? method_315(BuiltObjectMissionType.Move, null) : method_315(BuiltObjectMissionType.Move, obj), string_30: TextResolver.GetText("Move to X"), string_31: TextResolver.GetText("Move here"), bool_28: true);
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                if (obj != null && ((obj is Habitat && ((Habitat)obj).Empire != null && ((Habitat)obj).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj).Empire != shipGroup_3.Empire) || (obj is BuiltObject && ((BuiltObject)obj).Empire != shipGroup_3.Empire) || (obj is ShipGroup && ((ShipGroup)obj).Empire != shipGroup_3.Empire) || obj is Creature))
                {
                    ShipAction object_2 = method_315(BuiltObjectMissionType.Attack, obj);
                    toolStripMenuItem2 = method_310(TextResolver.GetText("Attack X"), object_2, bool_28: true);
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    object_2 = method_315(BuiltObjectMissionType.WaitAndAttack, obj);
                    toolStripMenuItem2 = method_310(TextResolver.GetText("Prepare and Attack X"), object_2, bool_28: true);
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
                if (obj != null && (obj is Habitat || (obj is BuiltObject && ((BuiltObject)obj).Role == BuiltObjectRole.Base)))
                {
                    ShipAction object_3 = method_315(BuiltObjectMissionType.Patrol, obj);
                    toolStripMenuItem2 = method_310(TextResolver.GetText("Patrol X"), object_3, bool_28: true);
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
                if (obj != null && ((obj is Habitat && ((Habitat)obj).Empire != null && ((Habitat)obj).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj).Empire != shipGroup_3.Empire) || (obj is BuiltObject && ((BuiltObject)obj).Role == BuiltObjectRole.Base && ((BuiltObject)obj).Empire != shipGroup_3.Empire)))
                {
                    bool flag = true;
                    if (obj is Habitat)
                    {
                        flag = shipGroup_3.Empire.CanSendShipToBlockadeColony((Habitat)obj);
                    }
                    else if (obj is BuiltObject)
                    {
                        flag = shipGroup_3.Empire.CanSendShipToBlockadeBuiltObject((BuiltObject)obj);
                    }
                    if (flag)
                    {
                        ShipAction object_4 = method_315(BuiltObjectMissionType.Blockade, obj);
                        toolStripMenuItem2 = method_310(TextResolver.GetText("Blockade X"), object_4, bool_28: true);
                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                    }
                }
            }
            toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            toolStripMenuItem2 = method_309(TextResolver.GetText("Refuel all ships"), BuiltObjectMissionType.Refuel);
            ShipAction shipAction2 = method_314(BuiltObjectMissionType.Refuel);
            if (obj != null && ((obj is BuiltObject && ((BuiltObject)obj).IsRefuellingDepot) || (obj is Habitat && ((Habitat)obj).IsRefuellingDepot)) && shipGroup_3 != obj)
            {
                ShipAction shipAction3 = shipAction2.Clone();
                shipAction3.Target = obj;
                toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction3, bool_28: true));
            }
            ResourceList fuelTypes = new ResourceList();
            if (shipGroup_3.Empire != null)
            {
                fuelTypes = shipGroup_3.Empire.DetermineFuelRequiredForFleet(shipGroup_3, setFuelLevelToZero: true);
            }
            StellarObject stellarObject = _Game.Galaxy.FastFindNearestRefuellingPoint(shipGroup_3.LeadShip.Xpos, shipGroup_3.LeadShip.Ypos, fuelTypes, shipGroup_3.Empire, shipGroup_3.LeadShip, includeResupplyShips: true, null, shipGroup_3.Ships.Count);
            if (stellarObject != null)
            {
                if (stellarObject is BuiltObject)
                {
                    ShipAction shipAction4 = shipAction2.Clone();
                    shipAction4.Target = (BuiltObject)stellarObject;
                    toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction4, bool_28: true));
                }
                else if (stellarObject is Habitat)
                {
                    ShipAction shipAction5 = shipAction2.Clone();
                    shipAction5.Target = (Habitat)stellarObject;
                    toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction5, bool_28: true));
                }
            }
            BuiltObject builtObject = _Game.Galaxy.FastFindNearestSpacePort(shipGroup_3.LeadShip.Xpos, shipGroup_3.LeadShip.Ypos, shipGroup_3.Empire, false);
            if (builtObject != null)
            {
                ShipAction shipAction6 = shipAction2.Clone();
                shipAction6.Target = builtObject;
                toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At your nearest Space Port"), shipAction6, bool_28: true));
            }
            if (toolStripMenuItem2.DropDownItems != null && toolStripMenuItem2.DropDownItems.Count > 0)
            {
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
            }
            bool flag2 = false;
            foreach (BuiltObject ship in shipGroup_3.Ships)
            {
                if (ship.DamagedComponentCount > 0)
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag2)
            {
                toolStripMenuItem2 = method_309(TextResolver.GetText("Repair and Refuel damaged ships"), BuiltObjectMissionType.Repair);
                ShipAction shipAction7 = method_314(BuiltObjectMissionType.Repair);
                if (obj != null && obj is BuiltObject && ((BuiltObject)obj).Empire == shipGroup_3.Empire && ((BuiltObject)obj).IsShipYard && shipGroup_3 != obj)
                {
                    ShipAction shipAction8 = shipAction7.Clone();
                    shipAction8.Target = obj;
                    toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction8, bool_28: true));
                }
                StellarObject stellarObject2 = shipGroup_3.Empire.FindNearestShipYard(shipGroup_3.LeadShip, canRepairOrBuild: true, includeVerySmallYards: false);
                if (stellarObject2 != null)
                {
                    ShipAction shipAction9 = shipAction7.Clone();
                    shipAction9.Target = stellarObject2;
                    toolStripMenuItem2.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction9, bool_28: true));
                }
                if (toolStripMenuItem2.DropDownItems != null && toolStripMenuItem2.DropDownItems.Count > 0)
                {
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                }
            }
            ShipAction shipAction10 = method_315(BuiltObjectMissionType.Move, shipGroup_3.GatherPoint);
            shipAction10.Position = new Point(0, 0);
            toolStripMenuItem2 = method_310(TextResolver.GetText("Return to base") + " ({0})", shipAction10, bool_28: true);
            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
            ShipAction object_5 = new ShipAction(ShipActionType.ClearQueuedMissions, null);
            toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("Clear All Queued Missions"), object_5, bool_28: true));
            method_341(toolStripMenuItem);
            if (toolStripMenuItem.DropDownItems.Count == 0)
            {
                toolStripMenuItem = null;
            }
            return toolStripMenuItem;
        }

        private void method_344()
        {
            _ = actionMenu.SourceControl;
            _ = actionMenu.OwnerItem;
            actionMenu.Items.Clear();
            actionMenu.Renderer = new CustomToolStripRenderer(font_3);
            ToolStripMenuItem toolStripMenuItem = null;
            if (_Game.SelectedObject == null)
            {
                return;
            }
            if (_Game.SelectedObject is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)_Game.SelectedObject;
                object obj = method_143(int_15, int_16, bool_28: true);
                if (double_0 > 100.0)
                {
                    toolStripMenuItem = method_324();
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = method_329();
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = method_330(BuiltObjectMissionType.WaitAndAttack);
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    if (shipGroup.TotalBombardPower > 0)
                    {
                        toolStripMenuItem = method_325();
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        toolStripMenuItem = method_326(BuiltObjectMissionType.WaitAndBombard);
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                    if (shipGroup.TotalAvailableBoardingAssaultStrength(_Game.Galaxy.CurrentDateTime) > 0)
                    {
                        toolStripMenuItem = method_327();
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        toolStripMenuItem = method_328();
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                    toolStripMenuItem = method_333();
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = method_337();
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = method_334();
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    if (shipGroup.TotalTroopSpaceRemaining > 0)
                    {
                        toolStripMenuItem = method_338(shipGroup.LeadShip, bool_28: true);
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                }
                else
                {
                    //ShipAction shipAction = null;
                    toolStripMenuItem = method_311(object_7: (obj == shipGroup) ? method_315(BuiltObjectMissionType.Move, null) : method_315(BuiltObjectMissionType.Move, obj), string_30: TextResolver.GetText("Move to X"), string_31: TextResolver.GetText("Move here"), bool_28: true);
                    actionMenu.Items.Add(toolStripMenuItem);
                    if (obj != null && ((obj is Habitat && ((Habitat)obj).Empire != null && ((Habitat)obj).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj).Empire != shipGroup.Empire) || (obj is BuiltObject && ((BuiltObject)obj).Empire != shipGroup.Empire) || (obj is ShipGroup && ((ShipGroup)obj).Empire != shipGroup.Empire) || obj is Creature))
                    {
                        ShipAction object_2 = method_315(BuiltObjectMissionType.Attack, obj);
                        toolStripMenuItem = method_310(TextResolver.GetText("Attack X"), object_2, bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                        object_2 = method_315(BuiltObjectMissionType.WaitAndAttack, obj);
                        toolStripMenuItem = method_310(TextResolver.GetText("Prepare and Attack X"), object_2, bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                        if (shipGroup.TotalBombardPower > 0 && obj is Habitat && ((Habitat)obj).Empire != null && ((Habitat)obj).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj).Empire != shipGroup.Empire)
                        {
                            object_2 = method_315(BuiltObjectMissionType.Bombard, obj);
                            toolStripMenuItem = method_310(TextResolver.GetText("Bombard X"), object_2, bool_28: true);
                            actionMenu.Items.Add(toolStripMenuItem);
                            object_2 = method_315(BuiltObjectMissionType.WaitAndBombard, obj);
                            toolStripMenuItem = method_310(TextResolver.GetText("Prepare and Bombard X"), object_2, bool_28: true);
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                    if (obj != null && obj is BuiltObject && ((BuiltObject)obj).Empire != shipGroup.Empire && shipGroup.TotalAvailableBoardingAssaultStrength(_Game.Galaxy.CurrentDateTime) > 0)
                    {
                        ShipAction object_3 = method_315(BuiltObjectMissionType.Capture, obj);
                        toolStripMenuItem = method_310(TextResolver.GetText("Capture X"), object_3, bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    if (obj != null && ((obj is BuiltObject && ((BuiltObject)obj).Role == BuiltObjectRole.Base && ((BuiltObject)obj).Empire != shipGroup.Empire) || (obj is Habitat && ((Habitat)obj).Population != null && ((Habitat)obj).Population.Count > 0 && ((Habitat)obj).Empire != shipGroup.Empire)) && shipGroup.TotalAvailableBoardingAssaultStrength(_Game.Galaxy.CurrentDateTime) > 0 && shipGroup.Empire != null && shipGroup.Empire.PirateEmpireBaseHabitat != null)
                    {
                        ShipAction object_4 = method_315(BuiltObjectMissionType.Raid, obj);
                        toolStripMenuItem = method_310(TextResolver.GetText("Raid X"), object_4, bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    if (obj != null && (obj is Habitat || (obj is BuiltObject && ((BuiltObject)obj).Role == BuiltObjectRole.Base)))
                    {
                        ShipAction object_5 = method_315(BuiltObjectMissionType.Patrol, obj);
                        toolStripMenuItem = method_310(TextResolver.GetText("Patrol X"), object_5, bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    if (obj != null && ((obj is Habitat && ((Habitat)obj).Empire != null && ((Habitat)obj).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj).Empire != shipGroup.Empire) || (obj is BuiltObject && ((BuiltObject)obj).Role == BuiltObjectRole.Base && ((BuiltObject)obj).Empire != shipGroup.Empire)))
                    {
                        bool flag = true;
                        if (obj is Habitat)
                        {
                            flag = shipGroup.Empire.CanSendShipToBlockadeColony((Habitat)obj);
                        }
                        else if (obj is BuiltObject)
                        {
                            flag = shipGroup.Empire.CanSendShipToBlockadeBuiltObject((BuiltObject)obj);
                        }
                        if (flag)
                        {
                            ShipAction object_6 = method_315(BuiltObjectMissionType.Blockade, obj);
                            toolStripMenuItem = method_310(TextResolver.GetText("Blockade X"), object_6, bool_28: true);
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                    if (shipGroup.TotalTroopSpaceRemaining > 0)
                    {
                        toolStripMenuItem = method_338(shipGroup.LeadShip, bool_28: true);
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                }
                actionMenu.Items.Add(new ToolStripSeparator());
                if (shipGroup.Mission != null && shipGroup.Mission.Type != 0)
                {
                    ShipAction tag = method_315(BuiltObjectMissionType.Hold, null);
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Stop"));
                    toolStripMenuItem.Tag = tag;
                    actionMenu.Items.Add(toolStripMenuItem);
                }
                StellarObject stellarObject = null;
                if (shipGroup.LeadShip.Attackers.Count > 0)
                {
                    stellarObject = shipGroup.LeadShip.Attackers[0];
                }
                else
                {
                    int[] threatLevel;
                    StellarObject[] array = _Game.Galaxy.EvaluateThreats(shipGroup.LeadShip, out threatLevel);
                    if (array != null && array.Length > 0)
                    {
                        stellarObject = array[0];
                    }
                }
                if (stellarObject != null)
                {
                    ShipAction tag2 = method_315(BuiltObjectMissionType.Escape, null);
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Escape"));
                    toolStripMenuItem.Tag = tag2;
                    actionMenu.Items.Add(toolStripMenuItem);
                }
                ShipAction shipAction2 = method_314(BuiltObjectMissionType.Undefined);
                shipAction2.ActionType = ShipActionType.AssignShipGroupHomeColony;
                if (obj is Habitat)
                {
                    Habitat habitat = (Habitat)obj;
                    if (habitat.Empire == shipGroup.Empire)
                    {
                        shipAction2.Target = habitat;
                        toolStripMenuItem = method_310(TextResolver.GetText("Assign X as base"), shipAction2, bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
                toolStripMenuItem = method_321(shipGroup);
                if (toolStripMenuItem != null)
                {
                    actionMenu.Items.Add(toolStripMenuItem);
                }
                toolStripMenuItem = method_309(TextResolver.GetText("Refuel all ships"), BuiltObjectMissionType.Refuel);
                ShipAction shipAction3 = method_314(BuiltObjectMissionType.Refuel);
                if (obj != null && ((obj is BuiltObject && ((BuiltObject)obj).IsRefuellingDepot) || (obj is Habitat && ((Habitat)obj).IsRefuellingDepot)))
                {
                    Empire refuelingEmpire = null;
                    if (obj is BuiltObject)
                    {
                        refuelingEmpire = ((BuiltObject)obj).Empire;
                    }
                    else if (obj is Habitat)
                    {
                        refuelingEmpire = ((Habitat)obj).Empire;
                    }
                    if (shipGroup != obj && _Game.Galaxy.CheckEmpireCanRefuelAtEmpire(shipGroup.LeadShip, _Game.PlayerEmpire, refuelingEmpire))
                    {
                        ShipAction shipAction4 = shipAction3.Clone();
                        shipAction4.Target = obj;
                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction4, bool_28: true));
                    }
                }
                ResourceList resourceList = shipGroup.CalculateRequiredFuel();
                StellarObject stellarObject2 = _Game.PlayerEmpire.DecideBestFleetRefuelPoint(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, shipGroup.Empire, resourceList, null);
                if (stellarObject2 == null)
                {
                    stellarObject2 = _Game.Galaxy.FastFindNearestRefuellingPoint(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, resourceList, shipGroup.Empire, shipGroup.LeadShip, includeResupplyShips: true, null, shipGroup.Ships.Count);
                }
                if (stellarObject2 != null)
                {
                    if (stellarObject2 is BuiltObject)
                    {
                        ShipAction shipAction5 = shipAction3.Clone();
                        shipAction5.Target = (BuiltObject)stellarObject2;
                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction5, bool_28: true));
                    }
                    else if (stellarObject2 is Habitat)
                    {
                        ShipAction shipAction6 = shipAction3.Clone();
                        shipAction6.Target = (Habitat)stellarObject2;
                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction6, bool_28: true));
                    }
                }
                BuiltObject builtObject = _Game.Galaxy.FastFindNearestSpacePort(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, shipGroup.Empire, false);
                if (builtObject != null)
                {
                    ShipAction shipAction7 = shipAction3.Clone();
                    shipAction7.Target = builtObject;
                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At your nearest Space Port"), shipAction7, bool_28: true));
                }
                if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                {
                    actionMenu.Items.Add(toolStripMenuItem);
                }
                bool flag2 = false;
                foreach (BuiltObject ship in shipGroup.Ships)
                {
                    if (ship.DamagedComponentCount > 0)
                    {
                        flag2 = true;
                        break;
                    }
                }
                if (flag2)
                {
                    toolStripMenuItem = method_309(TextResolver.GetText("Repair and Refuel damaged ships"), BuiltObjectMissionType.Repair);
                    ShipAction shipAction8 = method_314(BuiltObjectMissionType.Repair);
                    if (obj != null && obj is BuiltObject && ((BuiltObject)obj).Empire == shipGroup.Empire && ((BuiltObject)obj).IsShipYard && shipGroup != obj)
                    {
                        ShipAction shipAction9 = shipAction8.Clone();
                        shipAction9.Target = obj;
                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction9, bool_28: true));
                    }
                    StellarObject stellarObject3 = shipGroup.Empire.FindNearestShipYard(shipGroup.LeadShip, canRepairOrBuild: true, includeVerySmallYards: false);
                    if (stellarObject3 != null)
                    {
                        ShipAction shipAction10 = shipAction8.Clone();
                        shipAction10.Target = stellarObject3;
                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction10, bool_28: true));
                    }
                    if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
                toolStripMenuItem = method_309(TextResolver.GetText("Retrofit to latest designs"), BuiltObjectMissionType.Retrofit);
                ShipAction shipAction11 = method_314(BuiltObjectMissionType.Retrofit);
                if (obj != null && obj is BuiltObject && ((BuiltObject)obj).Empire == shipGroup.Empire && ((BuiltObject)obj).IsShipYard && shipGroup != obj)
                {
                    ShipAction shipAction12 = shipAction11.Clone();
                    shipAction12.Target = obj;
                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction12, bool_28: true));
                }
                StellarObject stellarObject4 = shipGroup.Empire.FindNearestShipYard(shipGroup.LeadShip, canRepairOrBuild: true, includeVerySmallYards: false);
                if (stellarObject4 != null)
                {
                    ShipAction shipAction13 = shipAction11.Clone();
                    shipAction13.Target = stellarObject4;
                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction13, bool_28: true));
                }
                if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                {
                    actionMenu.Items.Add(toolStripMenuItem);
                }
                ShipAction object_7 = method_313(ShipActionType.DisbandShipGroup, shipGroup);
                toolStripMenuItem = method_310(TextResolver.GetText("Disband Fleet"), object_7, bool_28: true);
                actionMenu.Items.Add(toolStripMenuItem);
                ShipAction shipAction14 = method_315(BuiltObjectMissionType.Move, shipGroup.GatherPoint);
                shipAction14.Position = new Point(0, 0);
                toolStripMenuItem = method_310(TextResolver.GetText("Return to base") + " ({0})", shipAction14, bool_28: true);
                actionMenu.Items.Add(toolStripMenuItem);
                if (shipGroup.Mission != null && shipGroup.Mission.Type != 0)
                {
                    toolStripMenuItem = method_343(shipGroup);
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
            }
            else if (_Game.SelectedObject is Habitat)
            {
                Habitat habitat2 = (Habitat)_Game.SelectedObject;
                if (habitat2 != null && _Game != null && _Game.PlayerEmpire != null && _Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                {
                    BuiltObject builtObject2 = null;
                    foreach (BuiltObject builtObject11 in _Game.PlayerEmpire.BuiltObjects)
                    {
                        if (builtObject11.SubRole == BuiltObjectSubRole.ColonyShip && builtObject11.Mission != null && builtObject11.Mission.Type == BuiltObjectMissionType.Colonize && builtObject11.Mission.TargetHabitat == habitat2)
                        {
                            builtObject2 = builtObject11;
                            break;
                        }
                    }
                    List<HabitatType> colonizableHabitatTypes = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpire(_Game.PlayerEmpire);
                    Design latestColonyShip = _Game.PlayerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                    if (_Game.PlayerEmpire.CanEmpireColonizeHabitat(_Game.PlayerEmpire, habitat2, colonizableHabitatTypes, latestColonyShip))
                    {
                        ShipAction object_8 = new ShipAction(ShipActionType.BuildColonize, habitat2);
                        if (builtObject2 != null)
                        {
                            string string_ = string.Format(TextResolver.GetText("SHIPNAME colonizing PLANETNAME"), builtObject2.Name, "{0}");
                            if (builtObject2.BuiltAt != null)
                            {
                                string_ = string.Format(TextResolver.GetText("SHIPNAME building at COLONY to colonize PLANETNAME"), builtObject2.Name, builtObject2.BuiltAt.Name, "{0}");
                            }
                            toolStripMenuItem = method_310(string_, object_8, bool_28: true);
                            toolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            toolStripMenuItem = method_310(TextResolver.GetText("Build new Colony Ship and Colonize X"), object_8, bool_28: true);
                        }
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
                if (habitat2.Population.TotalAmount > 0L && habitat2.Empire == _Game.PlayerEmpire)
                {
                    if (habitat2.ConstructionQueue != null && habitat2.ConstructionQueue.ConstructionYards.Count > 0)
                    {
                        ShipAction shipAction15 = method_315(BuiltObjectMissionType.Build, habitat2);
                        toolStripMenuItem = method_310(TextResolver.GetText("Build at X"), shipAction15, bool_28: false);
                        List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                        list.Add(BuiltObjectSubRole.ColonyShip);
                        list.Add(BuiltObjectSubRole.ConstructionShip);
                        list.Add(BuiltObjectSubRole.ResupplyShip);
                        list.Add(BuiltObjectSubRole.GenericBase);
                        list.Add(BuiltObjectSubRole.EnergyResearchStation);
                        list.Add(BuiltObjectSubRole.WeaponsResearchStation);
                        list.Add(BuiltObjectSubRole.HighTechResearchStation);
                        list.Add(BuiltObjectSubRole.MonitoringStation);
                        list.Add(BuiltObjectSubRole.DefensiveBase);
                        BuiltObject builtObject3 = _Game.Galaxy.DetermineSpacePortAtColonyIncludingUnderConstruction(habitat2);
                        if (builtObject3 == null)
                        {
                            list.Add(BuiltObjectSubRole.Outpost);
                            list.Add(BuiltObjectSubRole.SmallSpacePort);
                            list.Add(BuiltObjectSubRole.MediumSpacePort);
                            list.Add(BuiltObjectSubRole.LargeSpacePort);
                        }
                        DesignList buildableDesignsBySubRoles = habitat2.Empire.Designs.GetBuildableDesignsBySubRoles(list, _Game.PlayerEmpire, habitat2);
                        foreach (Design item in buildableDesignsBySubRoles)
                        {
                            BuiltObject builtObject4 = new BuiltObject(item, "", _Game.Galaxy);
                            if (habitat2.Empire.CanBuildBuiltObject(builtObject4, habitat2) && habitat2.Empire.CanBuildDesign(item, includeSizeCheck: false))
                            {
                                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                                string text = Galaxy.ResolveDescription(item.SubRole) + ": " + item.Name;
                                double num = item.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                string text2 = text;
                                text = (toolStripMenuItem2.Text = text2 + " (" + num.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                                ShipAction shipAction16 = shipAction15.Clone();
                                shipAction16.Design = item;
                                toolStripMenuItem2.Tag = shipAction16;
                                if (num > _Game.PlayerEmpire.StateMoney)
                                {
                                    toolStripMenuItem2.Enabled = false;
                                }
                                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
                            }
                        }
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    if (_Game.PlayerEmpire.TroopCanRecruitInfantry)
                    {
                        ShipAction shipAction17 = method_314(BuiltObjectMissionType.Undefined);
                        shipAction17.ActionType = ShipActionType.RecruitTroops;
                        shipAction17.Target = habitat2;
                        toolStripMenuItem = method_310(TextResolver.GetText("Recruit Troops at X"), shipAction17, bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    ShipAction object_9 = method_313(ShipActionType.ColonyBuildOptions, habitat2);
                    toolStripMenuItem = method_310(TextResolver.GetText("Build planetary facilities"), object_9, bool_28: true);
                    PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = habitat2.ResolveBuildableFacilities();
                    if (planetaryFacilityDefinitionList.Count > 0)
                    {
                        for (int i = 0; i < planetaryFacilityDefinitionList.Count; i++)
                        {
                            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
                            string name = planetaryFacilityDefinitionList[i].Name;
                            double num2 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinitionList[i], _Game.PlayerEmpire);
                            name = (toolStripMenuItem3.Text = name + " (" + num2.ToString("######0") + " credits)");
                            ShipAction shipAction19 = (ShipAction)(toolStripMenuItem3.Tag = new ShipAction(ShipActionType.BuildPlanetaryFacility, planetaryFacilityDefinitionList[i]));
                            if (num2 > _Game.PlayerEmpire.StateMoney)
                            {
                                toolStripMenuItem3.Enabled = false;
                            }
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
                        }
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    ShipAction object_10 = method_313(ShipActionType.ColonyBuildWonder, habitat2);
                    toolStripMenuItem = method_310(TextResolver.GetText("Build Wonders"), object_10, bool_28: true);
                    PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = habitat2.ResolveBuildableWonders();
                    if (planetaryFacilityDefinitionList2.Count > 0)
                    {
                        for (int j = 0; j < planetaryFacilityDefinitionList2.Count; j++)
                        {
                            ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
                            string name2 = planetaryFacilityDefinitionList2[j].Name;
                            double num3 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinitionList2[j], _Game.PlayerEmpire);
                            name2 = (toolStripMenuItem4.Text = name2 + " (" + num3.ToString("######0") + " credits)");
                            ShipAction shipAction21 = (ShipAction)(toolStripMenuItem4.Tag = new ShipAction(ShipActionType.BuildPlanetaryFacility, planetaryFacilityDefinitionList2[j]));
                            if (num3 > _Game.PlayerEmpire.StateMoney)
                            {
                                toolStripMenuItem4.Enabled = false;
                            }
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem4);
                        }
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = method_318(habitat2);
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = method_320(habitat2);
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = method_340();
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && habitat2.Empire != _Game.PlayerEmpire)
                {
                    ShipAction object_11 = method_313(ShipActionType.ColonyBuildOptions, habitat2);
                    toolStripMenuItem = method_310(TextResolver.GetText("Build planetary facilities"), object_11, bool_28: true);
                    PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList3 = habitat2.ResolveBuildableFacilitiesPirates(_Game.PlayerEmpire);
                    if (planetaryFacilityDefinitionList3.Count > 0)
                    {
                        for (int k = 0; k < planetaryFacilityDefinitionList3.Count; k++)
                        {
                            ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem();
                            string name3 = planetaryFacilityDefinitionList3[k].Name;
                            double num4 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinitionList3[k], _Game.PlayerEmpire);
                            name3 = (toolStripMenuItem5.Text = name3 + " (" + num4.ToString("######0") + " credits)");
                            ShipAction shipAction23 = (ShipAction)(toolStripMenuItem5.Tag = new ShipAction(ShipActionType.BuildPlanetaryFacility, planetaryFacilityDefinitionList3[k]));
                            if (num4 > _Game.PlayerEmpire.StateMoney)
                            {
                                toolStripMenuItem5.Enabled = false;
                            }
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem5);
                        }
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
                toolStripMenuItem = method_322(habitat2);
                if (toolStripMenuItem != null)
                {
                    actionMenu.Items.Add(toolStripMenuItem);
                }
            }
            else if (_Game.SelectedObject is BuiltObjectList)
            {
                BuiltObjectList builtObjectList = (BuiltObjectList)_Game.SelectedObject;
                if (builtObjectList != null && builtObjectList.Count > 0 && builtObjectList[0].Owner == _Game.PlayerEmpire)
                {
                    int num5 = 0;
                    int num6 = 0;
                    int num7 = 0;
                    int num8 = 0;
                    int num9 = 0;
                    int num10 = 0;
                    foreach (BuiltObject item2 in builtObjectList)
                    {
                        num5 += item2.FirepowerRaw;
                        num6 += item2.FighterCapacity;
                        num7 += item2.BombardWeaponPower;
                        num8 += item2.DamagedComponentCount;
                        num9 += item2.TroopCapacity;
                        num10 += item2.AssaultStrength;
                    }
                    object obj2 = method_143(int_15, int_16, bool_28: true);
                    if (double_0 > 100.0)
                    {
                        toolStripMenuItem = method_324();
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        if (num5 > 0 || num6 > 0)
                        {
                            toolStripMenuItem = method_329();
                            if (toolStripMenuItem != null)
                            {
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (num10 > 0)
                        {
                            toolStripMenuItem = method_327();
                            if (toolStripMenuItem != null)
                            {
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                            toolStripMenuItem = method_328();
                            if (toolStripMenuItem != null)
                            {
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (num7 > 0)
                        {
                            toolStripMenuItem = method_325();
                            if (toolStripMenuItem != null)
                            {
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (num8 > 0)
                        {
                            toolStripMenuItem = method_309(TextResolver.GetText("Repair damaged ships"), BuiltObjectMissionType.Repair);
                            ShipAction shipAction24 = method_314(BuiltObjectMissionType.Repair);
                            if (obj2 != null && obj2 is BuiltObject && ((BuiltObject)obj2).Empire == builtObjectList[0].Empire && ((BuiltObject)obj2).IsShipYard && builtObjectList != obj2)
                            {
                                ShipAction shipAction25 = shipAction24.Clone();
                                shipAction25.Target = obj2;
                                toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction25, bool_28: true));
                            }
                            StellarObject stellarObject5 = builtObjectList[0].Empire.FindNearestShipYard(builtObjectList[0], canRepairOrBuild: true, includeVerySmallYards: false);
                            if (stellarObject5 != null)
                            {
                                ShipAction shipAction26 = shipAction24.Clone();
                                shipAction26.Target = stellarObject5;
                                toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction26, bool_28: true));
                            }
                            if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                            {
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                    }
                    else
                    {
                        Habitat habitat3 = _Game.Galaxy.FastFindNearestSystem(int_15, int_16);
                        double num11 = _Game.Galaxy.CalculateDistance(int_15, int_16, habitat3.Xpos, habitat3.Ypos);
                        if (num11 > (double)Galaxy.MaxSolarSystemSize)
                        {
                            habitat3 = null;
                        }
                        //ShipAction shipAction27 = null;
                        toolStripMenuItem = method_311(object_7: (obj2 == builtObjectList) ? method_315(BuiltObjectMissionType.Move, null) : method_315(BuiltObjectMissionType.Move, obj2), string_30: TextResolver.GetText("Move to X"), string_31: TextResolver.GetText("Move here"), bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                        if ((num5 > 0 || num6 > 0) && obj2 != builtObjectList)
                        {
                            bool flag3 = false;
                            if (obj2 != null && ((obj2 is Habitat && ((Habitat)obj2).Empire != null && ((Habitat)obj2).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj2).Empire != builtObjectList[0].Empire) || (obj2 is BuiltObject && ((BuiltObject)obj2).Empire != builtObjectList[0].Empire) || (obj2 is ShipGroup && ((ShipGroup)obj2).Empire != builtObjectList[0].Empire) || obj2 is Creature))
                            {
                                flag3 = true;
                            }
                            if (flag3)
                            {
                                ShipAction object_13 = method_315(BuiltObjectMissionType.Attack, obj2);
                                toolStripMenuItem = method_310(TextResolver.GetText("Attack X"), object_13, bool_28: true);
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (num10 > 0 && obj2 != builtObjectList && obj2 is BuiltObject)
                        {
                            bool flag4 = false;
                            if (obj2 != null && obj2 is BuiltObject && ((BuiltObject)obj2).Empire != builtObjectList[0].Empire)
                            {
                                flag4 = true;
                            }
                            if (flag4)
                            {
                                ShipAction object_14 = method_315(BuiltObjectMissionType.Capture, obj2);
                                toolStripMenuItem = method_310(TextResolver.GetText("Capture X"), object_14, bool_28: true);
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (num10 > 0 && obj2 != null && ((obj2 is BuiltObject && ((BuiltObject)obj2).Role == BuiltObjectRole.Base && ((BuiltObject)obj2).Empire != builtObjectList[0].Empire) || (obj2 is Habitat && ((Habitat)obj2).Population != null && ((Habitat)obj2).Population.Count > 0 && ((Habitat)obj2).Empire != builtObjectList[0].Empire)) && builtObjectList[0].Empire != null && builtObjectList[0].Empire.PirateEmpireBaseHabitat != null)
                        {
                            ShipAction object_15 = method_315(BuiltObjectMissionType.Raid, obj2);
                            toolStripMenuItem = method_310(TextResolver.GetText("Raid X"), object_15, bool_28: true);
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        if (num7 > 0 && obj2 != null && obj2 is Habitat)
                        {
                            bool flag5 = false;
                            if (((Habitat)obj2).Empire != null && ((Habitat)obj2).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj2).Empire != builtObjectList[0].Empire)
                            {
                                flag5 = true;
                            }
                            if (flag5)
                            {
                                ShipAction object_16 = method_315(BuiltObjectMissionType.Bombard, obj2);
                                toolStripMenuItem = method_310(TextResolver.GetText("Bombard X"), object_16, bool_28: true);
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (num8 > 0)
                        {
                            toolStripMenuItem = method_309(TextResolver.GetText("Repair damaged ships"), BuiltObjectMissionType.Repair);
                            ShipAction shipAction28 = method_314(BuiltObjectMissionType.Repair);
                            if (obj2 != null && obj2 is BuiltObject && ((BuiltObject)obj2).Empire == builtObjectList[0].Empire && ((BuiltObject)obj2).IsShipYard && builtObjectList != obj2)
                            {
                                ShipAction shipAction29 = shipAction28.Clone();
                                shipAction29.Target = obj2;
                                toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction29, bool_28: true));
                            }
                            StellarObject stellarObject6 = builtObjectList[0].Empire.FindNearestShipYard(builtObjectList[0], canRepairOrBuild: true, includeVerySmallYards: false);
                            if (stellarObject6 != null)
                            {
                                ShipAction shipAction30 = shipAction28.Clone();
                                shipAction30.Target = stellarObject6;
                                toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction30, bool_28: true));
                            }
                            if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                            {
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (actionMenu.Items != null && actionMenu.Items.Count > 0)
                        {
                            actionMenu.Items.Add(new ToolStripSeparator());
                        }
                        ShipAction tag3 = method_314(BuiltObjectMissionType.Hold);
                        toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Stop"));
                        toolStripMenuItem.Tag = tag3;
                        actionMenu.Items.Add(toolStripMenuItem);
                        ShipAction tag4 = method_314(BuiltObjectMissionType.Escape);
                        toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Escape"));
                        toolStripMenuItem.Tag = tag4;
                        actionMenu.Items.Add(toolStripMenuItem);
                        if (num5 > 0 || num9 > 0 || num7 > 0 || num6 > 0)
                        {
                            ShipAction shipAction31 = method_314(BuiltObjectMissionType.Undefined);
                            shipAction31.ActionType = ShipActionType.JoinShipGroup;
                            toolStripMenuItem = method_309(TextResolver.GetText("Join Fleet"), BuiltObjectMissionType.Undefined);
                            if (builtObjectList[0].Empire.ShipGroups.Count < builtObjectList[0].Empire.FleetMaximumCount)
                            {
                                ToolStripMenuItem value = method_310("(" + TextResolver.GetText("New Fleet") + ")", shipAction31, bool_28: true);
                                toolStripMenuItem.DropDownItems.Add(value);
                            }
                            foreach (ShipGroup shipGroup2 in builtObjectList[0].Empire.ShipGroups)
                            {
                                ShipAction shipAction32 = shipAction31.Clone();
                                shipAction32.Target = shipGroup2;
                                ToolStripMenuItem value2 = method_310(shipGroup2.Name, shipAction32, bool_28: true);
                                toolStripMenuItem.DropDownItems.Add(value2);
                            }
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        ShipAction shipAction33 = method_314(BuiltObjectMissionType.Undefined);
                        shipAction33.ActionType = ShipActionType.LeaveShipGroup;
                        shipAction33.Target = null;
                        toolStripMenuItem = method_310(TextResolver.GetText("Leave Current Fleet"), shipAction33, bool_28: true);
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    BuiltObject shipToRefuel = builtObjectList[0];
                    for (int l = 0; l < builtObjectList.Count; l++)
                    {
                        if (builtObjectList[l].Role == BuiltObjectRole.Military)
                        {
                            shipToRefuel = builtObjectList[l];
                            break;
                        }
                    }
                    toolStripMenuItem = method_309(TextResolver.GetText("Refuel all ships"), BuiltObjectMissionType.Refuel);
                    ShipAction shipAction34 = method_314(BuiltObjectMissionType.Refuel);
                    if (obj2 != null && ((obj2 is BuiltObject && ((BuiltObject)obj2).IsRefuellingDepot) || (obj2 is Habitat && ((Habitat)obj2).IsRefuellingDepot)))
                    {
                        Empire refuelingEmpire2 = null;
                        if (obj2 is BuiltObject)
                        {
                            refuelingEmpire2 = ((BuiltObject)obj2).Empire;
                        }
                        else if (obj2 is Habitat)
                        {
                            refuelingEmpire2 = ((Habitat)obj2).Empire;
                        }
                        if (builtObjectList != obj2 && _Game.Galaxy.CheckEmpireCanRefuelAtEmpire(shipToRefuel, _Game.PlayerEmpire, refuelingEmpire2))
                        {
                            ShipAction shipAction35 = shipAction34.Clone();
                            shipAction35.Target = obj2;
                            toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction35, bool_28: true));
                        }
                    }
                    ResourceList fuelTypes = builtObjectList.DetermineFuelRequired(setFuelLevelToZero: true);
                    StellarObject stellarObject7 = _Game.Galaxy.FastFindNearestRefuellingPoint(builtObjectList[0].Xpos, builtObjectList[0].Ypos, fuelTypes, builtObjectList[0].ActualEmpire, shipToRefuel, includeResupplyShips: false, null, builtObjectList.Count);
                    if (stellarObject7 != null)
                    {
                        if (stellarObject7 is BuiltObject)
                        {
                            ShipAction shipAction36 = shipAction34.Clone();
                            shipAction36.Target = (BuiltObject)stellarObject7;
                            toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction36, bool_28: true));
                        }
                        else if (stellarObject7 is Habitat)
                        {
                            ShipAction shipAction37 = shipAction34.Clone();
                            shipAction37.Target = (Habitat)stellarObject7;
                            toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction37, bool_28: true));
                        }
                    }
                    if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    toolStripMenuItem = method_309(TextResolver.GetText("Retire all ships"), BuiltObjectMissionType.Retire);
                    ShipAction shipAction38 = method_314(BuiltObjectMissionType.Retire);
                    if (obj2 != null && obj2 is BuiltObject && ((BuiltObject)obj2).IsShipYard && builtObjectList != obj2)
                    {
                        ShipAction shipAction39 = shipAction38.Clone();
                        shipAction39.Target = obj2;
                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction39, bool_28: true));
                    }
                    BuiltObject builtObject5 = builtObjectList[0].Empire.FindNearestShipYardBase(builtObjectList[0]);
                    if (builtObject5 != null)
                    {
                        ShipAction shipAction40 = shipAction38.Clone();
                        shipAction40.Target = builtObject5;
                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction40, bool_28: true));
                    }
                    ShipAction shipAction41 = method_315(BuiltObjectMissionType.Retire, null);
                    shipAction41.Position = new Point(0, 0);
                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("Scrap Ships Immediately"), shipAction41, bool_28: true));
                    if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
                ShipAction shipAction42 = method_314(BuiltObjectMissionType.Undefined);
                shipAction42.ActionType = ShipActionType.AutomateShip;
                toolStripMenuItem = method_310(TextResolver.GetText("Automate all ships"), shipAction42, bool_28: true);
                actionMenu.Items.Add(toolStripMenuItem);
            }
            else
            {
                if (!(_Game.SelectedObject is BuiltObject))
                {
                    return;
                }
                BuiltObject builtObject6 = (BuiltObject)_Game.SelectedObject;
                if (builtObject6.Owner == _Game.PlayerEmpire)
                {
                    if (builtObject6.Role == BuiltObjectRole.Base)
                    {
                        if (builtObject6.IsShipYard && builtObject6.ConstructionQueue != null && builtObject6.ConstructionQueue.ConstructionYards.Count > 0)
                        {
                            ShipAction shipAction43 = method_315(BuiltObjectMissionType.Build, builtObject6);
                            toolStripMenuItem = method_310(TextResolver.GetText("Build at X"), shipAction43, bool_28: false);
                            List<BuiltObjectSubRole> list2 = new List<BuiltObjectSubRole>();
                            list2.Add(BuiltObjectSubRole.ExplorationShip);
                            list2.Add(BuiltObjectSubRole.Escort);
                            list2.Add(BuiltObjectSubRole.Frigate);
                            list2.Add(BuiltObjectSubRole.Destroyer);
                            list2.Add(BuiltObjectSubRole.Cruiser);
                            list2.Add(BuiltObjectSubRole.CapitalShip);
                            list2.Add(BuiltObjectSubRole.Carrier);
                            list2.Add(BuiltObjectSubRole.TroopTransport);
                            list2.Add(BuiltObjectSubRole.SmallFreighter);
                            list2.Add(BuiltObjectSubRole.MediumFreighter);
                            list2.Add(BuiltObjectSubRole.LargeFreighter);
                            list2.Add(BuiltObjectSubRole.PassengerShip);
                            list2.Add(BuiltObjectSubRole.MiningShip);
                            list2.Add(BuiltObjectSubRole.GasMiningShip);
                            DesignList designsBySubRoles = builtObject6.Empire.Designs.GetDesignsBySubRoles(list2);
                            foreach (Design item3 in designsBySubRoles)
                            {
                                BuiltObject builtObject7 = new BuiltObject(item3, "", _Game.Galaxy);
                                if (builtObject6.Empire.CanBuildBuiltObject(builtObject7) && builtObject6.Empire.CanBuildDesign(item3) && !item3.IsPlanetDestroyer)
                                {
                                    ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem();
                                    string text7 = Galaxy.ResolveDescription(item3.SubRole) + ": " + item3.Name;
                                    double num12 = item3.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                    string text2 = text7;
                                    text7 = (toolStripMenuItem6.Text = text2 + " (" + num12.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                                    ShipAction shipAction44 = shipAction43.Clone();
                                    shipAction44.Design = item3;
                                    toolStripMenuItem6.Tag = shipAction44;
                                    if (num12 > _Game.PlayerEmpire.StateMoney)
                                    {
                                        toolStripMenuItem6.Enabled = false;
                                    }
                                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem6);
                                }
                            }
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        if (builtObject6.Owner.PirateEmpireBaseHabitat != null && (builtObject6.SubRole == BuiltObjectSubRole.Outpost | builtObject6.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject6.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject6.SubRole == BuiltObjectSubRole.LargeSpacePort) && builtObject6.ParentHabitat != null && builtObject6.ParentHabitat != builtObject6.Owner.PirateEmpireBaseHabitat)
                        {
                            ShipAction tag5 = method_313(ShipActionType.ChangePirateHomeBase, builtObject6.ParentHabitat);
                            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Set as Home Base"));
                            toolStripMenuItem.Tag = tag5;
                            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
                            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        if ((builtObject6.ParentHabitat == null || builtObject6.ParentHabitat.Population.TotalAmount <= 0L || builtObject6.ParentHabitat.Empire != builtObject6.Empire) && builtObject6.ParentHabitat != null && builtObject6.ParentHabitat.Population != null && builtObject6.ParentHabitat.Population.Count != 0 && builtObject6.ParentHabitat.Population.TotalAmount > 0L)
                        {
                            if (builtObject6.ParentHabitat == null || builtObject6.ParentHabitat.Population == null || builtObject6.ParentHabitat.Population.TotalAmount <= 0L || builtObject6.ParentHabitat.Owner == _Game.Galaxy.IndependentEmpire)
                            {
                                ShipAction tag6 = method_315(BuiltObjectMissionType.Retire, builtObject6);
                                toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Retire"));
                                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                                ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
                                toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
                                ToolStripMenuItem toolStripMenuItem7 = new ToolStripMenuItem();
                                toolStripMenuItem7.Text = TextResolver.GetText("Scrap Base Immediately");
                                toolStripMenuItem7.Tag = tag6;
                                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem7);
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        else
                        {
                            List<BuiltObjectSubRole> list3 = new List<BuiltObjectSubRole>();
                            if (builtObject6.SubRole == BuiltObjectSubRole.GasMiningStation)
                            {
                                list3.Add(BuiltObjectSubRole.GasMiningStation);
                            }
                            else if (builtObject6.SubRole == BuiltObjectSubRole.MiningStation)
                            {
                                list3.Add(BuiltObjectSubRole.MiningStation);
                            }
                            else if (builtObject6.SubRole != BuiltObjectSubRole.Outpost && builtObject6.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject6.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject6.SubRole != BuiltObjectSubRole.LargeSpacePort)
                            {
                                if (builtObject6.SubRole == BuiltObjectSubRole.DefensiveBase)
                                {
                                    list3.Add(BuiltObjectSubRole.DefensiveBase);
                                }
                                else if (builtObject6.SubRole == BuiltObjectSubRole.EnergyResearchStation)
                                {
                                    list3.Add(BuiltObjectSubRole.EnergyResearchStation);
                                }
                                else if (builtObject6.SubRole == BuiltObjectSubRole.WeaponsResearchStation)
                                {
                                    list3.Add(BuiltObjectSubRole.WeaponsResearchStation);
                                }
                                else if (builtObject6.SubRole == BuiltObjectSubRole.HighTechResearchStation)
                                {
                                    list3.Add(BuiltObjectSubRole.HighTechResearchStation);
                                }
                                else if (builtObject6.SubRole == BuiltObjectSubRole.ResortBase)
                                {
                                    list3.Add(BuiltObjectSubRole.ResortBase);
                                }
                                else if (builtObject6.SubRole == BuiltObjectSubRole.MonitoringStation)
                                {
                                    list3.Add(BuiltObjectSubRole.MonitoringStation);
                                }
                                else if (builtObject6.SubRole == BuiltObjectSubRole.GenericBase)
                                {
                                    list3.Add(BuiltObjectSubRole.GenericBase);
                                }
                            }
                            else
                            {
                                list3.Add(BuiltObjectSubRole.Outpost);
                                list3.Add(BuiltObjectSubRole.SmallSpacePort);
                                list3.Add(BuiltObjectSubRole.MediumSpacePort);
                                list3.Add(BuiltObjectSubRole.LargeSpacePort);
                            }
                            DesignList buildableDesignsBySubRoles2 = builtObject6.Empire.Designs.GetBuildableDesignsBySubRoles(list3, _Game.PlayerEmpire, builtObject6.ParentHabitat);
                            if (buildableDesignsBySubRoles2.Contains(builtObject6.Design))
                            {
                                buildableDesignsBySubRoles2.Remove(builtObject6.Design);
                            }
                            if (buildableDesignsBySubRoles2.Count > 0 && builtObject6.RetrofitDesign == null && builtObject6.BuiltAt == null)
                            {
                                toolStripMenuItem = method_309(TextResolver.GetText("Retrofit"), BuiltObjectMissionType.Retrofit);
                                ShipAction shipAction45 = method_315(BuiltObjectMissionType.Retrofit, builtObject6);
                                foreach (Design item4 in buildableDesignsBySubRoles2)
                                {
                                    ToolStripMenuItem toolStripMenuItem8 = new ToolStripMenuItem();
                                    double cost = 0.0;
                                    ComponentList componentsToProcure = null;
                                    bool flag6 = builtObject6.Empire.DetermineRetrofitAffordability(builtObject6, item4, out cost, out componentsToProcure);
                                    string text10 = (toolStripMenuItem8.Text = string.Format(TextResolver.GetText("To X for Y credits"), item4.Name + " (" + Galaxy.ResolveDescription(item4.SubRole) + ")", cost.ToString("######0")));
                                    if (!flag6)
                                    {
                                        toolStripMenuItem8.Enabled = false;
                                    }
                                    ShipAction shipAction46 = shipAction45.Clone();
                                    shipAction46.Design = item4;
                                    toolStripMenuItem8.Tag = shipAction46;
                                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem8);
                                }
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                            ShipAction tag7 = method_315(BuiltObjectMissionType.Retire, builtObject6);
                            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Retire"));
                            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                            ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
                            toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
                            ToolStripMenuItem toolStripMenuItem9 = new ToolStripMenuItem();
                            toolStripMenuItem9.Text = TextResolver.GetText("Scrap Base Immediately");
                            toolStripMenuItem9.Tag = tag7;
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem9);
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                    else
                    {
                        object obj3 = method_143(int_15, int_16, bool_28: true);
                        if (double_0 > 100.0)
                        {
                            if (builtObject6.UnbuiltComponentCount <= 0)
                            {
                                toolStripMenuItem = method_324();
                                if (toolStripMenuItem != null)
                                {
                                    actionMenu.Items.Add(toolStripMenuItem);
                                }
                                if (builtObject6.IsShipYard && builtObject6.ConstructionQueue != null && builtObject6.ConstructionQueue.ConstructionYards.Count > 0)
                                {
                                    toolStripMenuItem = method_323(builtObject6);
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                bool flag7 = false;
                                if (builtObject6.FirepowerRaw > 0 || builtObject6.FighterCapacity > 0)
                                {
                                    toolStripMenuItem = method_329();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                        flag7 = true;
                                    }
                                }
                                if (builtObject6.BombardWeaponPower > 0)
                                {
                                    toolStripMenuItem = method_325();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                        flag7 = true;
                                    }
                                }
                                if (builtObject6.AssaultStrength > 0 && builtObject6.AssaultRange > 0)
                                {
                                    toolStripMenuItem = method_327();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                        flag7 = true;
                                    }
                                }
                                if (builtObject6.AssaultStrength > 0 && builtObject6.AssaultRange > 0)
                                {
                                    toolStripMenuItem = method_328();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                        flag7 = true;
                                    }
                                }
                                if (builtObject6.FirepowerRaw > 0 || builtObject6.FighterCapacity > 0)
                                {
                                    toolStripMenuItem = method_333();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                    toolStripMenuItem = method_337();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                    toolStripMenuItem = method_334();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                if (builtObject6.Troops != null && builtObject6.Troops.TotalAttackStrength > 0)
                                {
                                    if (!flag7)
                                    {
                                        toolStripMenuItem = method_331();
                                        if (toolStripMenuItem != null)
                                        {
                                            actionMenu.Items.Add(toolStripMenuItem);
                                        }
                                    }
                                    toolStripMenuItem = method_339();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                if (builtObject6.TroopCapacityRemaining >= 100)
                                {
                                    toolStripMenuItem = method_338(builtObject6, bool_28: false);
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                if (builtObject6.SensorResourceProfileSensorRange > 0)
                                {
                                    Habitat habitat4 = _Game.Galaxy.FastFindNearestSystem(int_15, int_16);
                                    double num13 = _Game.Galaxy.CalculateDistance(int_15, int_16, habitat4.Xpos, habitat4.Ypos);
                                    if (num13 > (double)Galaxy.MaxSolarSystemSize)
                                    {
                                        habitat4 = null;
                                    }
                                    toolStripMenuItem = method_309(TextResolver.GetText("Explore"), BuiltObjectMissionType.Explore);
                                    ShipAction shipAction47 = method_314(BuiltObjectMissionType.Explore);
                                    if (habitat4 != null)
                                    {
                                        ShipAction shipAction48 = shipAction47.Clone();
                                        shipAction48.Target = habitat4;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("This system") + " ({0})", shipAction48, bool_28: true));
                                    }
                                    else if (obj3 != null && obj3 is SystemInfo)
                                    {
                                        ShipAction shipAction49 = shipAction47.Clone();
                                        shipAction49.Target = ((SystemInfo)obj3).SystemStar;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("This system") + " ({0})", shipAction49, bool_28: true));
                                    }
                                    Habitat habitat5 = _Game.Galaxy.FastFindNearestUnexploredHabitat(builtObject6.Xpos, builtObject6.Ypos, builtObject6.ActualEmpire);
                                    if (habitat5 != null)
                                    {
                                        habitat5 = Galaxy.DetermineHabitatSystemStar(habitat5);
                                        ShipAction shipAction50 = shipAction47.Clone();
                                        shipAction50.Target = habitat5;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("Nearest unexplored system"), shipAction50, bool_28: true));
                                    }
                                    Sector sector = _Game.Galaxy.ResolveSector((double)int_15, (double)int_16);
                                    Habitat habitat6 = _Game.Galaxy.FastFindNearestUnexploredHabitatInSector(builtObject6.Xpos, builtObject6.Ypos, builtObject6.ActualEmpire, sector);
                                    if (habitat6 != null)
                                    {
                                        ShipAction shipAction51 = shipAction47.Clone();
                                        shipAction51.Target = sector;
                                        toolStripMenuItem.DropDownItems.Add(method_310(string.Format(TextResolver.GetText("All systems in sector X"), Galaxy.ResolveSectorDescription(sector)), shipAction51, bool_28: true));
                                    }
                                    actionMenu.Items.Add(toolStripMenuItem);
                                }
                                if (builtObject6.SubRole == BuiltObjectSubRole.ColonyShip && builtObject6.Empire != null && builtObject6.Empire.PirateEmpireBaseHabitat == null)
                                {
                                    toolStripMenuItem = method_335();
                                    if (toolStripMenuItem != null)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                if (builtObject6.DamagedComponentCount > 0)
                                {
                                    toolStripMenuItem = method_309(TextResolver.GetText("Repair"), BuiltObjectMissionType.Repair);
                                    ShipAction shipAction52 = method_314(BuiltObjectMissionType.Repair);
                                    if (obj3 != null && obj3 is BuiltObject && ((BuiltObject)obj3).Empire == builtObject6.Empire && ((BuiltObject)obj3).IsShipYard && builtObject6 != obj3)
                                    {
                                        ShipAction shipAction53 = shipAction52.Clone();
                                        shipAction53.Target = obj3;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction53, bool_28: true));
                                    }
                                    StellarObject stellarObject8 = builtObject6.Empire.FindNearestShipYard(builtObject6, canRepairOrBuild: true, includeVerySmallYards: true);
                                    if (stellarObject8 != null)
                                    {
                                        ShipAction shipAction54 = shipAction52.Clone();
                                        shipAction54.Target = stellarObject8;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction54, bool_28: true));
                                    }
                                    if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Habitat habitat7 = _Game.Galaxy.FastFindNearestSystem(int_15, int_16);
                            double num14 = _Game.Galaxy.CalculateDistance(int_15, int_16, habitat7.Xpos, habitat7.Ypos);
                            if (num14 > (double)Galaxy.MaxSolarSystemSize)
                            {
                                habitat7 = null;
                            }
                            bool flag8 = false;
                            if (builtObject6.UnbuiltComponentCount <= 0)
                            {
                                //ShipAction shipAction55 = null;
                                toolStripMenuItem = method_311(object_7: (obj3 == builtObject6) ? method_315(BuiltObjectMissionType.Move, null) : method_315(BuiltObjectMissionType.Move, obj3), string_30: TextResolver.GetText("Move to X"), string_31: TextResolver.GetText("Move here"), bool_28: true);
                                actionMenu.Items.Add(toolStripMenuItem);
                                if ((builtObject6.FirepowerRaw > 0 || builtObject6.FighterCapacity > 0) && obj3 != builtObject6)
                                {
                                    bool flag9 = false;
                                    if (obj3 != null && ((obj3 is Habitat && ((Habitat)obj3).Empire != null && ((Habitat)obj3).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj3).Empire != builtObject6.Empire) || (obj3 is BuiltObject && ((BuiltObject)obj3).Empire != builtObject6.Empire) || (obj3 is ShipGroup && ((ShipGroup)obj3).Empire != builtObject6.Empire) || obj3 is Creature))
                                    {
                                        flag9 = true;
                                    }
                                    else if (builtObject6.IsPlanetDestroyer && obj3 != null && obj3 is Habitat && _Game.Galaxy.CanDestroyHabitat(builtObject6, (Habitat)obj3))
                                    {
                                        flag9 = true;
                                    }
                                    if (flag9)
                                    {
                                        ShipAction object_18 = method_315(BuiltObjectMissionType.Attack, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Attack X"), object_18, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                        flag8 = true;
                                    }
                                    if (obj3 != null && obj3 is BuiltObject && ((BuiltObject)obj3).Empire != builtObject6.Empire && builtObject6.AssaultStrength > 0 && builtObject6.AssaultRange > 0)
                                    {
                                        ShipAction object_19 = method_315(BuiltObjectMissionType.Capture, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Capture X"), object_19, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                        flag8 = true;
                                    }
                                    if (obj3 != null && ((obj3 is BuiltObject && ((BuiltObject)obj3).Role == BuiltObjectRole.Base && ((BuiltObject)obj3).Empire != builtObject6.Empire) || (obj3 is Habitat && ((Habitat)obj3).Population != null && ((Habitat)obj3).Population.Count > 0 && ((Habitat)obj3).Empire != builtObject6.Empire)) && builtObject6.AssaultStrength > 0 && builtObject6.AssaultRange > 0 && builtObject6.Empire != null && builtObject6.Empire.PirateEmpireBaseHabitat != null)
                                    {
                                        ShipAction object_20 = method_315(BuiltObjectMissionType.Raid, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Raid X"), object_20, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                        flag8 = true;
                                    }
                                }
                                if (builtObject6.BombardWeaponPower > 0 && obj3 != null && obj3 is Habitat)
                                {
                                    bool flag10 = false;
                                    if (((Habitat)obj3).Empire != null && ((Habitat)obj3).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj3).Empire != builtObject6.Empire)
                                    {
                                        flag10 = true;
                                    }
                                    if (flag10)
                                    {
                                        ShipAction object_21 = method_315(BuiltObjectMissionType.Bombard, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Bombard X"), object_21, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                        flag8 = true;
                                    }
                                }
                                if ((builtObject6.FirepowerRaw > 0 || builtObject6.FighterCapacity > 0) && obj3 != builtObject6)
                                {
                                    if (obj3 != null && (obj3 is Habitat || (obj3 is BuiltObject && ((BuiltObject)obj3).Role == BuiltObjectRole.Base)))
                                    {
                                        ShipAction object_22 = method_315(BuiltObjectMissionType.Patrol, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Patrol X"), object_22, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                    if (obj3 != null && obj3 is BuiltObject && ((BuiltObject)obj3).Role != BuiltObjectRole.Base && ((BuiltObject)obj3).Empire == builtObject6.Empire)
                                    {
                                        ShipAction object_23 = method_315(BuiltObjectMissionType.Escort, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Escort X"), object_23, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                    if (obj3 != null && ((obj3 is Habitat && ((Habitat)obj3).Empire != null && ((Habitat)obj3).Empire != _Game.Galaxy.IndependentEmpire && ((Habitat)obj3).Empire != builtObject6.Empire) || (obj3 is BuiltObject && ((BuiltObject)obj3).Role == BuiltObjectRole.Base && ((BuiltObject)obj3).Empire != builtObject6.Empire)))
                                    {
                                        bool flag11 = true;
                                        if (obj3 is Habitat)
                                        {
                                            flag11 = builtObject6.Empire.CanSendShipToBlockadeColony((Habitat)obj3);
                                        }
                                        else if (obj3 is BuiltObject)
                                        {
                                            flag11 = builtObject6.Empire.CanSendShipToBlockadeBuiltObject((BuiltObject)obj3);
                                        }
                                        if (flag11)
                                        {
                                            ShipAction object_24 = method_315(BuiltObjectMissionType.Blockade, obj3);
                                            toolStripMenuItem = method_310(TextResolver.GetText("Blockade X"), object_24, bool_28: true);
                                            actionMenu.Items.Add(toolStripMenuItem);
                                        }
                                    }
                                }
                                if (builtObject6.Troops != null && builtObject6.Troops.TotalAttackStrength > 0 && obj3 != builtObject6)
                                {
                                    if (!flag8 && obj3 != null && ((obj3 is Habitat && ((Habitat)obj3).Empire != builtObject6.Empire) || (obj3 is BuiltObject && ((BuiltObject)obj3).Empire != builtObject6.Empire) || (obj3 is ShipGroup && ((ShipGroup)obj3).Empire != builtObject6.Empire)))
                                    {
                                        ShipAction object_25 = method_315(BuiltObjectMissionType.Attack, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Attack X"), object_25, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                    if (obj3 != null && obj3 is Habitat && ((Habitat)obj3).Empire == builtObject6.Empire)
                                    {
                                        ShipAction object_26 = method_315(BuiltObjectMissionType.UnloadTroops, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Unload Troops at X"), object_26, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                if (builtObject6.TroopCapacityRemaining >= 100)
                                {
                                    toolStripMenuItem = method_309(TextResolver.GetText("Load Troops"), BuiltObjectMissionType.LoadTroops);
                                    ShipAction shipAction56 = method_314(BuiltObjectMissionType.LoadTroops);
                                    if (obj3 != null && obj3 is Habitat && ((Habitat)obj3).Empire == builtObject6.Empire)
                                    {
                                        ShipAction shipAction57 = shipAction56.Clone();
                                        shipAction57.Target = obj3;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction57, bool_28: true));
                                    }
                                    Habitat habitat8 = builtObject6.Empire.FindNearestColonyWithExcessTroops(builtObject6, enforceMinimumTroopLimits: false);
                                    if (habitat8 != null)
                                    {
                                        ShipAction shipAction58 = shipAction56.Clone();
                                        shipAction58.Target = habitat8;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest colony"), shipAction58, bool_28: true));
                                    }
                                    if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                                    {
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                if (builtObject6.SensorResourceProfileSensorRange > 0)
                                {
                                    toolStripMenuItem = method_309(TextResolver.GetText("Explore"), BuiltObjectMissionType.Explore);
                                    ShipAction shipAction59 = method_314(BuiltObjectMissionType.Explore);
                                    if (obj3 != null && obj3 is Habitat)
                                    {
                                        ShipAction shipAction60 = shipAction59.Clone();
                                        shipAction60.Target = obj3;
                                        toolStripMenuItem.DropDownItems.Add(method_310("{0}", shipAction60, bool_28: true));
                                    }
                                    if (habitat7 != null)
                                    {
                                        ShipAction shipAction61 = shipAction59.Clone();
                                        shipAction61.Target = habitat7;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("This system") + " ({0})", shipAction61, bool_28: true));
                                    }
                                    else if (obj3 != null && obj3 is SystemInfo)
                                    {
                                        ShipAction shipAction62 = shipAction59.Clone();
                                        shipAction62.Target = ((SystemInfo)obj3).SystemStar;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("This system") + " ({0})", shipAction62, bool_28: true));
                                    }
                                    Habitat habitat9 = _Game.Galaxy.FindNearestUnexploredHabitat(builtObject6.Xpos, builtObject6.Ypos, builtObject6.ActualEmpire, includeAsteroids: true);
                                    if (habitat9 != null)
                                    {
                                        habitat9 = Galaxy.DetermineHabitatSystemStar(habitat9);
                                        ShipAction shipAction63 = shipAction59.Clone();
                                        shipAction63.Target = habitat9;
                                        toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("Nearest unexplored system"), shipAction63, bool_28: true));
                                    }
                                    Sector sector2 = _Game.Galaxy.ResolveSector((double)int_15, (double)int_16);
                                    Habitat habitat10 = _Game.Galaxy.FastFindNearestUnexploredHabitatInSector(builtObject6.Xpos, builtObject6.Ypos, builtObject6.ActualEmpire, sector2);
                                    if (habitat10 != null)
                                    {
                                        ShipAction shipAction64 = shipAction59.Clone();
                                        shipAction64.Target = sector2;
                                        toolStripMenuItem.DropDownItems.Add(method_310(string.Format(TextResolver.GetText("All systems in sector X"), Galaxy.ResolveSectorDescription(sector2)), shipAction64, bool_28: true));
                                    }
                                    actionMenu.Items.Add(toolStripMenuItem);
                                }
                                if (builtObject6.IsShipYard && builtObject6.ConstructionQueue != null && builtObject6.ConstructionQueue.ConstructionYards.Count > 0)
                                {
                                    ShipAction shipAction65 = null;
                                    if (obj3 != builtObject6)
                                    {
                                        if (obj3 is Habitat)
                                        {
                                            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                                            {
                                                shipAction65 = method_315(BuiltObjectMissionType.Build, obj3);
                                                toolStripMenuItem = method_310(TextResolver.GetText("Build at X"), shipAction65, bool_28: false);
                                                List<BuiltObjectSubRole> list4 = new List<BuiltObjectSubRole>();
                                                bool flag12 = true;
                                                bool flag13 = true;
                                                bool flag14 = true;
                                                Habitat habitat11 = (Habitat)obj3;
                                                if (habitat11.Population != null && habitat11.Population.Count > 0 && habitat11.Empire != _Game.PlayerEmpire)
                                                {
                                                    flag14 = false;
                                                    flag13 = false;
                                                    flag12 = false;
                                                }
                                                if (_Game.Galaxy.CheckAlreadyHaveMiningStationAtHabitat((Habitat)obj3, builtObject6.Empire))
                                                {
                                                    flag13 = false;
                                                }
                                                if (_Game.Galaxy.DetermineSpacePortAtHabitat((Habitat)obj3) != null)
                                                {
                                                    flag14 = false;
                                                }
                                                if (flag14)
                                                {
                                                    list4.Add(BuiltObjectSubRole.Outpost);
                                                    list4.Add(BuiltObjectSubRole.SmallSpacePort);
                                                    list4.Add(BuiltObjectSubRole.MediumSpacePort);
                                                    list4.Add(BuiltObjectSubRole.LargeSpacePort);
                                                }
                                                if (flag13)
                                                {
                                                    list4.Add(BuiltObjectSubRole.MiningStation);
                                                    list4.Add(BuiltObjectSubRole.GasMiningStation);
                                                }
                                                if (flag12)
                                                {
                                                    list4.Add(BuiltObjectSubRole.ResortBase);
                                                    list4.Add(BuiltObjectSubRole.GenericBase);
                                                    list4.Add(BuiltObjectSubRole.MonitoringStation);
                                                    list4.Add(BuiltObjectSubRole.DefensiveBase);
                                                }
                                                DesignList buildableDesignsBySubRoles3 = builtObject6.Empire.Designs.GetBuildableDesignsBySubRoles(list4, _Game.PlayerEmpire);
                                                DesignList buildablePlanetDestroyerDesigns = builtObject6.Empire.Designs.GetBuildablePlanetDestroyerDesigns(_Game.PlayerEmpire);
                                                if (buildablePlanetDestroyerDesigns != null && buildablePlanetDestroyerDesigns.Count > 0)
                                                {
                                                    buildableDesignsBySubRoles3.AddRange(buildablePlanetDestroyerDesigns);
                                                }
                                                foreach (Design item5 in buildableDesignsBySubRoles3)
                                                {
                                                    if (flag13 || (item5.ExtractionGas <= 0 && item5.ExtractionLuxury <= 0 && item5.ExtractionMine <= 0))
                                                    {
                                                        ToolStripMenuItem toolStripMenuItem10 = new ToolStripMenuItem();
                                                        string text11 = Galaxy.ResolveDescription(item5.SubRole) + ": " + item5.Name;
                                                        double num15 = item5.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                                        string text2 = text11;
                                                        text11 = (toolStripMenuItem10.Text = text2 + " (" + num15.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                                                        ShipAction shipAction66 = shipAction65.Clone();
                                                        shipAction66.Design = item5;
                                                        toolStripMenuItem10.Tag = shipAction66;
                                                        if (num15 > _Game.PlayerEmpire.StateMoney)
                                                        {
                                                            toolStripMenuItem10.Enabled = false;
                                                        }
                                                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem10);
                                                    }
                                                }
                                                if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                                                {
                                                    actionMenu.Items.Add(toolStripMenuItem);
                                                }
                                            }
                                            else if (_Game.Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(builtObject6.Empire, (Habitat)obj3))
                                            {
                                                shipAction65 = method_315(BuiltObjectMissionType.Build, obj3);
                                                toolStripMenuItem = method_310(TextResolver.GetText("Build at X"), shipAction65, bool_28: false);
                                                List<BuiltObjectSubRole> list5 = new List<BuiltObjectSubRole>();
                                                bool flag15 = true;
                                                if (_Game.Galaxy.CheckAlreadyHaveMiningStationAtHabitat((Habitat)obj3, builtObject6.Empire))
                                                {
                                                    flag15 = false;
                                                }
                                                if (flag15)
                                                {
                                                    list5.Add(BuiltObjectSubRole.MiningStation);
                                                    list5.Add(BuiltObjectSubRole.GasMiningStation);
                                                }
                                                list5.Add(BuiltObjectSubRole.ResortBase);
                                                list5.Add(BuiltObjectSubRole.GenericBase);
                                                list5.Add(BuiltObjectSubRole.EnergyResearchStation);
                                                list5.Add(BuiltObjectSubRole.WeaponsResearchStation);
                                                list5.Add(BuiltObjectSubRole.HighTechResearchStation);
                                                list5.Add(BuiltObjectSubRole.MonitoringStation);
                                                list5.Add(BuiltObjectSubRole.DefensiveBase);
                                                DesignList buildableDesignsBySubRoles4 = builtObject6.Empire.Designs.GetBuildableDesignsBySubRoles(list5, _Game.PlayerEmpire);
                                                DesignList buildablePlanetDestroyerDesigns2 = builtObject6.Empire.Designs.GetBuildablePlanetDestroyerDesigns(_Game.PlayerEmpire);
                                                if (buildablePlanetDestroyerDesigns2 != null && buildablePlanetDestroyerDesigns2.Count > 0)
                                                {
                                                    buildableDesignsBySubRoles4.AddRange(buildablePlanetDestroyerDesigns2);
                                                }
                                                foreach (Design item6 in buildableDesignsBySubRoles4)
                                                {
                                                    if (flag15 || (item6.ExtractionGas <= 0 && item6.ExtractionLuxury <= 0 && item6.ExtractionMine <= 0))
                                                    {
                                                        ToolStripMenuItem toolStripMenuItem11 = new ToolStripMenuItem();
                                                        string text13 = Galaxy.ResolveDescription(item6.SubRole) + ": " + item6.Name;
                                                        double num16 = item6.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                                        string text2 = text13;
                                                        text13 = (toolStripMenuItem11.Text = text2 + " (" + num16.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                                                        ShipAction shipAction67 = shipAction65.Clone();
                                                        shipAction67.Design = item6;
                                                        toolStripMenuItem11.Tag = shipAction67;
                                                        if (num16 > _Game.PlayerEmpire.StateMoney)
                                                        {
                                                            toolStripMenuItem11.Enabled = false;
                                                        }
                                                        toolStripMenuItem.DropDownItems.Add(toolStripMenuItem11);
                                                    }
                                                }
                                                actionMenu.Items.Add(toolStripMenuItem);
                                            }
                                        }
                                        else if (obj3 == null)
                                        {
                                            shipAction65 = method_315(BuiltObjectMissionType.Build, null);
                                            toolStripMenuItem = method_311(TextResolver.GetText("Build here"), TextResolver.GetText("Build here"), shipAction65, bool_28: false);
                                            List<BuiltObjectSubRole> list6 = new List<BuiltObjectSubRole>();
                                            list6.Add(BuiltObjectSubRole.ResortBase);
                                            list6.Add(BuiltObjectSubRole.GenericBase);
                                            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                                            {
                                                list6.Add(BuiltObjectSubRole.EnergyResearchStation);
                                                list6.Add(BuiltObjectSubRole.WeaponsResearchStation);
                                                list6.Add(BuiltObjectSubRole.HighTechResearchStation);
                                            }
                                            list6.Add(BuiltObjectSubRole.MonitoringStation);
                                            list6.Add(BuiltObjectSubRole.DefensiveBase);
                                            bool flag16 = false;
                                            DesignList buildableDesignsBySubRoles5 = builtObject6.Empire.Designs.GetBuildableDesignsBySubRoles(list6, _Game.PlayerEmpire);
                                            DesignList buildablePlanetDestroyerDesigns3 = builtObject6.Empire.Designs.GetBuildablePlanetDestroyerDesigns(_Game.PlayerEmpire);
                                            if (buildablePlanetDestroyerDesigns3 != null && buildablePlanetDestroyerDesigns3.Count > 0)
                                            {
                                                buildableDesignsBySubRoles5.AddRange(buildablePlanetDestroyerDesigns3);
                                            }
                                            foreach (Design item7 in buildableDesignsBySubRoles5)
                                            {
                                                if (item7.Size <= _Game.PlayerEmpire.MaximumConstructionSizeBase(item7.SubRole))
                                                {
                                                    flag16 = true;
                                                    ToolStripMenuItem toolStripMenuItem12 = new ToolStripMenuItem();
                                                    string text15 = Galaxy.ResolveDescription(item7.SubRole) + ": " + item7.Name;
                                                    double num17 = item7.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                                    string text2 = text15;
                                                    text15 = (toolStripMenuItem12.Text = text2 + " (" + num17.ToString("######0") + " " + TextResolver.GetText("credits") + ")");
                                                    ShipAction shipAction68 = shipAction65.Clone();
                                                    shipAction68.Design = item7;
                                                    toolStripMenuItem12.Tag = shipAction68;
                                                    if (num17 > _Game.PlayerEmpire.StateMoney)
                                                    {
                                                        toolStripMenuItem12.Enabled = false;
                                                    }
                                                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem12);
                                                }
                                            }
                                            if (flag16)
                                            {
                                                actionMenu.Items.Add(toolStripMenuItem);
                                            }
                                        }
                                    }
                                }
                                if (builtObject6.SubRole == BuiltObjectSubRole.ColonyShip && builtObject6.Empire != null && builtObject6.Empire.PirateEmpireBaseHabitat == null && builtObject6.Components[ComponentType.HabitationColonization, ComponentStatus.Normal] != null && obj3 != null && obj3 is Habitat && (((Habitat)obj3).Empire == null || ((Habitat)obj3).Empire == _Game.Galaxy.IndependentEmpire))
                                {
                                    Habitat habitat12 = (Habitat)obj3;
                                    int newPopulationAmount = 0;
                                    if (builtObject6.Empire.CanBuiltObjectColonizeHabitat(builtObject6, habitat12, out newPopulationAmount) && builtObject6.Empire.CanEmpireColonizeHabitatRange(builtObject6.Empire, habitat12))
                                    {
                                        ShipAction object_27 = method_315(BuiltObjectMissionType.Colonize, obj3);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Colonize X"), object_27, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                if ((builtObject6.ExtractionGas > 0 || builtObject6.ExtractionLuxury > 0 || builtObject6.ExtractionMine > 0) && builtObject6.SubRole != BuiltObjectSubRole.ResupplyShip && obj3 != null && obj3 is Habitat && ((Habitat)obj3).Category != 0 && (((Habitat)obj3).Empire == null || ((Habitat)obj3).Empire == _Game.Galaxy.IndependentEmpire))
                                {
                                    ShipAction object_28 = method_315(BuiltObjectMissionType.ExtractResources, obj3);
                                    toolStripMenuItem = method_310(TextResolver.GetText("Mine X"), object_28, bool_28: true);
                                    actionMenu.Items.Add(toolStripMenuItem);
                                }
                            }
                            if (builtObject6.SubRole == BuiltObjectSubRole.ResupplyShip)
                            {
                                if (!builtObject6.IsDeployed && builtObject6.DeployProgress == 0.0 && obj3 != null && obj3 is Habitat)
                                {
                                    Habitat habitat13 = (Habitat)obj3;
                                    if (habitat13.Empire == null || habitat13.Empire == _Game.Galaxy.IndependentEmpire)
                                    {
                                        ShipAction object_29 = method_315(BuiltObjectMissionType.Deploy, habitat13);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Deploy at X"), object_29, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                                else if (builtObject6.IsDeployed && builtObject6.DeployProgress == 0.0)
                                {
                                    ShipAction object_30 = method_314(BuiltObjectMissionType.Undeploy);
                                    toolStripMenuItem = method_310(TextResolver.GetText("Undeploy"), object_30, bool_28: true);
                                    actionMenu.Items.Add(toolStripMenuItem);
                                }
                            }
                            if (obj3 is Habitat)
                            {
                                Habitat habitat14 = (Habitat)obj3;
                                if (habitat14.Ruin != null && habitat14.Ruin.PlayerEmpireEncountered && _Game.Galaxy.CheckRuinsHaveBenefit(habitat14.Ruin, _Game.PlayerEmpire))
                                {
                                    double num18 = _Game.Galaxy.CalculateDistance(builtObject6.Xpos, builtObject6.Ypos, habitat14.Xpos, habitat14.Ypos);
                                    if (num18 <= 500.0)
                                    {
                                        ShipAction object_31 = new ShipAction(ShipActionType.InvestigateRuins, habitat14);
                                        toolStripMenuItem = method_310(TextResolver.GetText("Investigate Ruins"), object_31, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                            }
                            if (obj3 is BuiltObject)
                            {
                                BuiltObject builtObject8 = (BuiltObject)obj3;
                                if (builtObject8.Empire == null && builtObject8.UnbuiltComponentCount <= 0 && builtObject8.DamagedComponentCount <= 0)
                                {
                                    double num19 = _Game.Galaxy.CalculateDistance(builtObject6.Xpos, builtObject6.Ypos, builtObject8.Xpos, builtObject8.Ypos);
                                    if (num19 <= 500.0)
                                    {
                                        ShipAction object_32 = new ShipAction(ShipActionType.InvestigateBuiltObject, builtObject8);
                                        string empty = string.Empty;
                                        empty = ((builtObject8.Role != BuiltObjectRole.Base) ? TextResolver.GetText("Investigate Ship") : TextResolver.GetText("Investigate Base"));
                                        toolStripMenuItem = method_310(empty, object_32, bool_28: true);
                                        actionMenu.Items.Add(toolStripMenuItem);
                                    }
                                }
                            }
                            if (builtObject6.UnbuiltComponentCount <= 0 && builtObject6.DamagedComponentCount > 0)
                            {
                                toolStripMenuItem = method_309(TextResolver.GetText("Repair"), BuiltObjectMissionType.Repair);
                                ShipAction shipAction69 = method_314(BuiltObjectMissionType.Repair);
                                if (obj3 != null && obj3 is BuiltObject && ((BuiltObject)obj3).Empire == builtObject6.Empire && ((BuiltObject)obj3).IsShipYard && builtObject6 != obj3)
                                {
                                    ShipAction shipAction70 = shipAction69.Clone();
                                    shipAction70.Target = obj3;
                                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction70, bool_28: true));
                                }
                                StellarObject stellarObject9 = builtObject6.Empire.FindNearestShipYard(builtObject6, canRepairOrBuild: true, includeVerySmallYards: true);
                                if (stellarObject9 != null)
                                {
                                    ShipAction shipAction71 = shipAction69.Clone();
                                    shipAction71.Target = stellarObject9;
                                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction71, bool_28: true));
                                }
                                if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                                {
                                    actionMenu.Items.Add(toolStripMenuItem);
                                }
                            }
                        }
                        if (actionMenu.Items != null && actionMenu.Items.Count > 0)
                        {
                            actionMenu.Items.Add(new ToolStripSeparator());
                        }
                        if (builtObject6.BuiltAt == null && builtObject6.Mission != null && builtObject6.Mission.Type != 0)
                        {
                            ShipAction tag8 = method_314(BuiltObjectMissionType.Hold);
                            toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Stop"));
                            toolStripMenuItem.Tag = tag8;
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        if (builtObject6.UnbuiltComponentCount <= 0)
                        {
                            StellarObject stellarObject10 = null;
                            if (builtObject6.Attackers.Count > 0)
                            {
                                stellarObject10 = builtObject6.Attackers[0];
                            }
                            else
                            {
                                int[] threatLevel2;
                                StellarObject[] array2 = _Game.Galaxy.EvaluateThreats(builtObject6, out threatLevel2);
                                if (array2 != null && array2.Length > 0)
                                {
                                    stellarObject10 = array2[0];
                                }
                            }
                            if (stellarObject10 != null)
                            {
                                ShipAction tag9 = method_314(BuiltObjectMissionType.Escape);
                                toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Escape"));
                                toolStripMenuItem.Tag = tag9;
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (builtObject6.ShipGroup == null)
                        {
                            if (builtObject6.FirepowerRaw > 0 || builtObject6.TroopCapacity > 0 || builtObject6.BombardWeaponPower > 0 || builtObject6.FighterCapacity > 0)
                            {
                                ShipAction shipAction72 = method_314(BuiltObjectMissionType.Undefined);
                                shipAction72.ActionType = ShipActionType.JoinShipGroup;
                                toolStripMenuItem = method_309(TextResolver.GetText("Join Fleet"), BuiltObjectMissionType.Undefined);
                                if (builtObject6.Empire.ShipGroups.Count < builtObject6.Empire.FleetMaximumCount)
                                {
                                    ToolStripMenuItem value3 = method_310("(" + TextResolver.GetText("New Fleet") + ")", shipAction72, bool_28: true);
                                    toolStripMenuItem.DropDownItems.Add(value3);
                                }
                                foreach (ShipGroup shipGroup3 in builtObject6.Empire.ShipGroups)
                                {
                                    ShipAction shipAction73 = shipAction72.Clone();
                                    shipAction73.Target = shipGroup3;
                                    ToolStripMenuItem value4 = method_310(shipGroup3.Name, shipAction73, bool_28: true);
                                    toolStripMenuItem.DropDownItems.Add(value4);
                                }
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        else
                        {
                            ShipAction shipAction74 = method_314(BuiltObjectMissionType.Undefined);
                            shipAction74.ActionType = ShipActionType.LeaveShipGroup;
                            shipAction74.Target = builtObject6.ShipGroup;
                            toolStripMenuItem = method_310(string.Format(TextResolver.GetText("Leave FLEETNAME"), "{0}"), shipAction74, bool_28: true);
                            actionMenu.Items.Add(toolStripMenuItem);
                            if (builtObject6.ShipGroup.LeadShip != builtObject6)
                            {
                                ShipAction shipAction75 = method_314(BuiltObjectMissionType.Undefined);
                                shipAction75.ActionType = ShipActionType.SetAsLeadShipInGroup;
                                shipAction75.Target = builtObject6.ShipGroup;
                                toolStripMenuItem = method_310(string.Format(TextResolver.GetText("Make lead ship for FLEETNAME"), "{0}"), shipAction75, bool_28: true);
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                        }
                        if (builtObject6.UnbuiltComponentCount <= 0)
                        {
                            toolStripMenuItem = method_309(TextResolver.GetText("Refuel"), BuiltObjectMissionType.Refuel);
                            ShipAction shipAction76 = method_314(BuiltObjectMissionType.Refuel);
                            if (obj3 != null && ((obj3 is BuiltObject && ((BuiltObject)obj3).IsRefuellingDepot) || (obj3 is Habitat && ((Habitat)obj3).IsRefuellingDepot)))
                            {
                                Empire refuelingEmpire3 = null;
                                if (obj3 is BuiltObject)
                                {
                                    refuelingEmpire3 = ((BuiltObject)obj3).Empire;
                                }
                                else if (obj3 is Habitat)
                                {
                                    refuelingEmpire3 = ((Habitat)obj3).Empire;
                                }
                                if (builtObject6 != obj3 && _Game.Galaxy.CheckEmpireCanRefuelAtEmpire(builtObject6, _Game.PlayerEmpire, refuelingEmpire3))
                                {
                                    ShipAction shipAction77 = shipAction76.Clone();
                                    shipAction77.Target = obj3;
                                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction77, bool_28: true));
                                }
                            }
                            StellarObject stellarObject11 = null;
                            ResourceList fuelTypes2 = builtObject6.DetermineFuelRequired(setFuelLevelToZero: true);
                            stellarObject11 = ((builtObject6.Role != BuiltObjectRole.Military) ? _Game.Galaxy.FastFindNearestRefuellingPoint(builtObject6.Xpos, builtObject6.Ypos, fuelTypes2, builtObject6.ActualEmpire, builtObject6) : _Game.Galaxy.FastFindNearestRefuellingPoint(builtObject6.Xpos, builtObject6.Ypos, fuelTypes2, builtObject6.ActualEmpire, builtObject6, includeResupplyShips: true, null));
                            if (stellarObject11 != null)
                            {
                                if (stellarObject11 is BuiltObject)
                                {
                                    ShipAction shipAction78 = shipAction76.Clone();
                                    shipAction78.Target = (BuiltObject)stellarObject11;
                                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction78, bool_28: true));
                                }
                                else if (stellarObject11 is Habitat)
                                {
                                    ShipAction shipAction79 = shipAction76.Clone();
                                    shipAction79.Target = (Habitat)stellarObject11;
                                    toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest refuelling point"), shipAction79, bool_28: true));
                                }
                            }
                            if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                            {
                                actionMenu.Items.Add(toolStripMenuItem);
                            }
                            builtObject6.Empire.ReviewLatestDesigns();
                            List<BuiltObjectSubRole> list7 = new List<BuiltObjectSubRole>();
                            list7.Add(builtObject6.SubRole);
                            DesignList designList = new DesignList();
                            designList = ((builtObject6.ParentHabitat == null) ? builtObject6.Empire.Designs.GetBuildableDesignsBySubRoles(list7, _Game.PlayerEmpire) : builtObject6.Empire.Designs.GetBuildableDesignsBySubRoles(list7, _Game.PlayerEmpire, builtObject6.ParentHabitat));
                            if (designList.Contains(builtObject6.Design))
                            {
                                designList.Remove(builtObject6.Design);
                            }
                            if (designList != null && designList.Count > 0 && builtObject6.RetrofitDesign == null && (builtObject6.Mission == null || builtObject6.Mission.Type != BuiltObjectMissionType.Retrofit))
                            {
                                toolStripMenuItem = method_309(TextResolver.GetText("Retrofit"), BuiltObjectMissionType.Retrofit);
                                ShipAction shipAction80 = method_314(BuiltObjectMissionType.Retrofit);
                                if (obj3 != null && obj3 is BuiltObject && ((BuiltObject)obj3).IsShipYard && builtObject6 != obj3)
                                {
                                    foreach (Design item8 in designList)
                                    {
                                        ShipAction shipAction81 = shipAction80.Clone();
                                        shipAction81.Target = obj3;
                                        ShipAction shipAction82 = shipAction81.Clone();
                                        shipAction82.Design = item8;
                                        toolStripMenuItem.DropDownItems.Add(method_310(string.Format(TextResolver.GetText("To X at Y"), item8.Name, "{0}"), shipAction82, bool_28: true));
                                    }
                                }
                                BuiltObject builtObject9 = builtObject6.Empire.FindNearestShipYardBase(builtObject6);
                                if (builtObject9 != null)
                                {
                                    foreach (Design item9 in designList)
                                    {
                                        ShipAction shipAction83 = shipAction80.Clone();
                                        shipAction83.Target = builtObject9;
                                        ShipAction shipAction84 = shipAction83.Clone();
                                        shipAction84.Design = item9;
                                        toolStripMenuItem.DropDownItems.Add(method_310(string.Format(TextResolver.GetText("To X at nearest ship yard"), item9.Name), shipAction84, bool_28: true));
                                    }
                                }
                                if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                                {
                                    actionMenu.Items.Add(toolStripMenuItem);
                                }
                            }
                        }
                        toolStripMenuItem = method_309(TextResolver.GetText("Retire"), BuiltObjectMissionType.Retire);
                        ShipAction shipAction85 = method_314(BuiltObjectMissionType.Retire);
                        if (builtObject6.UnbuiltComponentCount <= 0)
                        {
                            if (obj3 != null && obj3 is BuiltObject && ((BuiltObject)obj3).IsShipYard && builtObject6 != obj3)
                            {
                                ShipAction shipAction86 = shipAction85.Clone();
                                shipAction86.Target = obj3;
                                toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At X"), shipAction86, bool_28: true));
                            }
                            BuiltObject builtObject10 = builtObject6.Empire.FindNearestShipYardBase(builtObject6);
                            if (builtObject10 != null)
                            {
                                ShipAction shipAction87 = shipAction85.Clone();
                                shipAction87.Target = builtObject10;
                                toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("At nearest ship yard"), shipAction87, bool_28: true));
                            }
                        }
                        if (builtObject6.BuiltAt == null)
                        {
                            ShipAction object_33 = method_315(BuiltObjectMissionType.Retire, builtObject6);
                            toolStripMenuItem.DropDownItems.Add(method_310(TextResolver.GetText("Scrap Ship Immediately"), object_33, bool_28: true));
                        }
                        if (toolStripMenuItem.DropDownItems != null && toolStripMenuItem.DropDownItems.Count > 0)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                        if (!builtObject6.IsAutoControlled)
                        {
                            ShipAction shipAction88 = method_314(BuiltObjectMissionType.Undefined);
                            shipAction88.ActionType = ShipActionType.AutomateShip;
                            toolStripMenuItem = method_310(TextResolver.GetText("Automate"), shipAction88, bool_28: true);
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                }
                else if (builtObject6.Empire == _Game.PlayerEmpire && builtObject6.Role == BuiltObjectRole.Base)
                {
                    List<BuiltObjectSubRole> list8 = new List<BuiltObjectSubRole>();
                    if (builtObject6.SubRole == BuiltObjectSubRole.GasMiningStation)
                    {
                        list8.Add(BuiltObjectSubRole.GasMiningStation);
                    }
                    else if (builtObject6.SubRole == BuiltObjectSubRole.MiningStation)
                    {
                        list8.Add(BuiltObjectSubRole.MiningStation);
                    }
                    else if (builtObject6.SubRole != BuiltObjectSubRole.Outpost && builtObject6.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject6.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject6.SubRole != BuiltObjectSubRole.LargeSpacePort)
                    {
                        if (builtObject6.SubRole == BuiltObjectSubRole.DefensiveBase)
                        {
                            list8.Add(BuiltObjectSubRole.DefensiveBase);
                        }
                        else if (builtObject6.SubRole == BuiltObjectSubRole.EnergyResearchStation)
                        {
                            list8.Add(BuiltObjectSubRole.EnergyResearchStation);
                        }
                        else if (builtObject6.SubRole == BuiltObjectSubRole.WeaponsResearchStation)
                        {
                            list8.Add(BuiltObjectSubRole.WeaponsResearchStation);
                        }
                        else if (builtObject6.SubRole == BuiltObjectSubRole.HighTechResearchStation)
                        {
                            list8.Add(BuiltObjectSubRole.HighTechResearchStation);
                        }
                        else if (builtObject6.SubRole == BuiltObjectSubRole.ResortBase)
                        {
                            list8.Add(BuiltObjectSubRole.ResortBase);
                        }
                        else if (builtObject6.SubRole == BuiltObjectSubRole.MonitoringStation)
                        {
                            list8.Add(BuiltObjectSubRole.MonitoringStation);
                        }
                        else if (builtObject6.SubRole == BuiltObjectSubRole.GenericBase)
                        {
                            list8.Add(BuiltObjectSubRole.GenericBase);
                        }
                    }
                    else
                    {
                        list8.Add(BuiltObjectSubRole.Outpost);
                        list8.Add(BuiltObjectSubRole.SmallSpacePort);
                        list8.Add(BuiltObjectSubRole.MediumSpacePort);
                        list8.Add(BuiltObjectSubRole.LargeSpacePort);
                    }
                    DesignList buildableDesignsBySubRoles6 = builtObject6.Empire.Designs.GetBuildableDesignsBySubRoles(list8, _Game.PlayerEmpire);
                    if (buildableDesignsBySubRoles6.Contains(builtObject6.Design))
                    {
                        buildableDesignsBySubRoles6.Remove(builtObject6.Design);
                    }
                    if (buildableDesignsBySubRoles6.Count > 0 && builtObject6.RetrofitDesign == null && builtObject6.BuiltAt == null)
                    {
                        toolStripMenuItem = method_309(TextResolver.GetText("Retrofit"), BuiltObjectMissionType.Retrofit);
                        ShipAction shipAction89 = method_315(BuiltObjectMissionType.Retrofit, builtObject6);
                        foreach (Design item10 in buildableDesignsBySubRoles6)
                        {
                            ToolStripMenuItem toolStripMenuItem13 = new ToolStripMenuItem();
                            double cost2 = 0.0;
                            ComponentList componentsToProcure2 = null;
                            bool flag17 = builtObject6.Empire.DetermineRetrofitAffordability(builtObject6, item10, out cost2, out componentsToProcure2);
                            string text18 = (toolStripMenuItem13.Text = string.Format(TextResolver.GetText("To X for Y credits"), item10.Name + " (" + Galaxy.ResolveDescription(item10.SubRole) + ")", cost2.ToString("######0")));
                            if (!flag17)
                            {
                                toolStripMenuItem13.Enabled = false;
                            }
                            ShipAction shipAction90 = shipAction89.Clone();
                            shipAction90.Design = item10;
                            toolStripMenuItem13.Tag = shipAction90;
                            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem13);
                        }
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                    ShipAction tag10 = method_315(BuiltObjectMissionType.Retire, builtObject6);
                    toolStripMenuItem = new ToolStripMenuItem(TextResolver.GetText("Retire"));
                    ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowCheckMargin = false;
                    ((ToolStripDropDownMenu)toolStripMenuItem.DropDown).ShowImageMargin = false;
                    toolStripMenuItem.DropDownItemClicked += actionMenu_ItemClicked;
                    ToolStripMenuItem toolStripMenuItem14 = new ToolStripMenuItem();
                    toolStripMenuItem14.Text = TextResolver.GetText("Scrap Base Immediately");
                    toolStripMenuItem14.Tag = tag10;
                    toolStripMenuItem.DropDownItems.Add(toolStripMenuItem14);
                    actionMenu.Items.Add(toolStripMenuItem);
                }
                toolStripMenuItem = method_322(builtObject6);
                if (toolStripMenuItem != null)
                {
                    actionMenu.Items.Add(toolStripMenuItem);
                }
                if (builtObject6.Role == BuiltObjectRole.Base)
                {
                    if (builtObject6.Empire == _Game.PlayerEmpire)
                    {
                        toolStripMenuItem = method_319(builtObject6);
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                    else
                    {
                        toolStripMenuItem = method_317(builtObject6);
                        if (toolStripMenuItem != null)
                        {
                            actionMenu.Items.Add(toolStripMenuItem);
                        }
                    }
                }
                if (builtObject6.Empire == _Game.PlayerEmpire || builtObject6.Owner == _Game.PlayerEmpire)
                {
                    toolStripMenuItem = method_316(builtObject6);
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
                if (builtObject6.Mission != null && builtObject6.Mission.Type != 0)
                {
                    toolStripMenuItem = method_342(builtObject6);
                    if (toolStripMenuItem != null)
                    {
                        actionMenu.Items.Add(toolStripMenuItem);
                    }
                }
            }
        }

        private void method_345()
        {
            pnlPirateSmugglingMissionResourceSelection.Size = new Size(330, 75);
            pnlPirateSmugglingMissionResourceSelection.Location = new Point(60, base.ClientRectangle.Height - 115);
            lblPirateSmugglingMissionResourcePriceDescription.Location = new Point(170, 15);
            lblPirateSmugglingMissionResourcePriceDescription.Text = string.Format(TextResolver.GetText("for X credits per 100 units"), "0");
            cmbPirateSmugglingMissionResourceSelection.BindData(font_6, _Game.Galaxy.ResourceSystem.Resources, _uiResourcesBitmaps, allowNullResource: true, allowCriticalResources: false);
            cmbPirateSmugglingMissionResourceSelection.AllowNullResourceText = TextResolver.GetText("All Resources");
            cmbPirateSmugglingMissionResourceSelection.SelectedIndex = 0;
            cmbPirateSmugglingMissionResourceSelection.Size = new Size(150, 21);
            cmbPirateSmugglingMissionResourceSelection.Location = new Point(10, 10);
            btnPirateSmugglingMissionAssign.Size = new Size(150, 25);
            btnPirateSmugglingMissionAssign.Location = new Point(10, 40);
            btnPirateSmugglingMissionAssign.Text = TextResolver.GetText("Assign Mission");
            btnPirateSmugglingMissionCancel.Size = new Size(150, 25);
            btnPirateSmugglingMissionCancel.Location = new Point(170, 40);
            btnPirateSmugglingMissionCancel.Text = TextResolver.GetText("Cancel");
            pnlPirateSmugglingMissionResourceSelection.Visible = true;
            pnlPirateSmugglingMissionResourceSelection.BringToFront();
        }

        private void method_346()
        {
            pnlPirateSmugglingMissionResourceSelection.Visible = false;
            pnlPirateSmugglingMissionResourceSelection.SendToBack();
        }

        private void btnPirateSmugglingMissionAssign_Click(object sender, EventArgs e)
        {
            if (_Game.SelectedObject is Habitat)
            {
                Habitat habitat = (Habitat)_Game.SelectedObject;
                Resource selectedResource = cmbPirateSmugglingMissionResourceSelection.SelectedResource;
                double attackPrice = 1.0;
                if (selectedResource != null)
                {
                    attackPrice = _Game.PlayerEmpire.CalculatePirateSmugglePricePerUnit(habitat, selectedResource.ResourceID);
                }
                long expiryDate = _Game.Galaxy.CurrentStarDate + (long)(3.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                EmpireActivity empireActivity = new EmpireActivity(habitat.Empire, habitat, attackPrice, _Game.PlayerEmpire, expiryDate, EmpireActivityType.Smuggle);
                if (selectedResource != null)
                {
                    empireActivity.ResourceId = selectedResource.ResourceID;
                }
                if (selectedResource != null)
                {
                    Order order = (empireActivity.RelatedOrder = _Game.PlayerEmpire.CreateOrder(habitat, selectedResource, 10000, isState: true, OrderType.Standard, expiryDate));
                }
                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity))
                {
                    _Game.PlayerEmpire.PirateMissions.Add(empireActivity);
                    if (!_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity))
                    {
                        _Game.Galaxy.PirateMissions.Add(empireActivity);
                    }
                }
            }
            method_346();
        }

        private void btnPirateSmugglingMissionCancel_Click(object sender, EventArgs e)
        {
            method_346();
        }

        private void cmbPirateSmugglingMissionResourceSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = string.Format(TextResolver.GetText("for X credits per 100 units"), "0");
            if (_Game.SelectedObject is Habitat)
            {
                Habitat colony = (Habitat)_Game.SelectedObject;
                Resource selectedResource = cmbPirateSmugglingMissionResourceSelection.SelectedResource;
                //double num = 0.0;
                text = string.Format(arg0: (((selectedResource != null) ? _Game.PlayerEmpire.CalculatePirateSmugglePricePerUnit(colony, selectedResource.ResourceID) : 1.0) * 100.0).ToString("#.0"), format: TextResolver.GetText("for X credits per 100 units"));
            }
            lblPirateSmugglingMissionResourcePriceDescription.Text = text;
        }



    }

}