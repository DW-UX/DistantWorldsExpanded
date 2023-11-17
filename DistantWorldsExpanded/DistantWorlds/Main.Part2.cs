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
using Ionic.Zlib;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Graphics;
//using SlimDX.DirectSound;
using ExpansionMod.HotKeyMapping;
using ExpansionMod.Objects;
using System.Collections.Concurrent;

namespace DistantWorlds {

  public partial class Main {


        private void method_610(ref int int_64)
        {
            int_64 += 25;
        }

        private void method_611(System.Windows.Forms.Panel panel_1, string string_30, Font font_9, ref int int_64)
        {
            Panel panel = new Panel();
            panel.BackColor = Color.FromArgb(96, 32, 64);
            panel.Parent = panel_1;
            panel_1.Controls.Add(panel);
            panel.Size = new Size(panel_1.Width, 34);
            panel.Location = new Point(0, int_64 - 4);
            Label label = new Label();
            label.Text = string_30;
            label.Font = font_9;
            label.ForeColor = Color.FromArgb(220, 220, 220);
            label.Parent = panel;
            panel.Controls.Add(label);
            label.Location = new Point(5, 5);
            label.Size = new Size(350, 30);
            label.BringToFront();
            int_64 += 35;
        }

        private void method_612(System.Windows.Forms.Panel panel_1, string string_30, Font font_9, ref int int_64)
        {
            SizeF sizeF = SizeF.Empty;
            using (Graphics graphics = panel_1.CreateGraphics())
            {
                sizeF = graphics.MeasureString(string_30, font_9, panel_1.Width, StringFormat.GenericDefault);
            }
            Label label = new Label();
            label.Size = new Size(panel_1.Width, (int)sizeF.Height + 2);
            label.MaximumSize = new Size(panel_1.Width, (int)sizeF.Height + 2);
            label.Text = string_30;
            label.Font = font_9;
            label.ForeColor = Color.FromArgb(170, 170, 170);
            label.Parent = panel_1;
            panel_1.Controls.Add(label);
            label.Location = new Point(0, int_64);
            label.BringToFront();
            int_64 += (int)sizeF.Height + 2;
        }

        private void method_613(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, Resource resource_0, ref int int_65)
        {
            ResourceDropDown resourceDropDown = new ResourceDropDown();
            resourceDropDown.BindData(font_3, _Game.Galaxy.ResourceSystem.Resources, _uiResourcesBitmaps, allowNullResource: true, allowCriticalResources: false);
            resourceDropDown.AllowNullResourceText = "(" + TextResolver.GetText("None") + ")";
            if (resourceDropDown.Items != null)
            {
                resourceDropDown.SetSelectedResource(resource_0);
            }
            method_626(panel_1, resourceDropDown, int_64, 160, string_30, string_31, string_32, ref int_65);
            int_65 += 25;
        }

        private void method_614(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, ColonyPopulationPolicy colonyPopulationPolicy_0, ref int int_65)
        {
            ColonyPopulationPolicyDropDown colonyPopulationPolicyDropDown = new ColonyPopulationPolicyDropDown();
            colonyPopulationPolicyDropDown.BindData();
            colonyPopulationPolicyDropDown.Font = font_6;
            if (colonyPopulationPolicyDropDown.Items != null)
            {
                colonyPopulationPolicyDropDown.SetSelectedPolicy(colonyPopulationPolicy_0);
            }
            method_626(panel_1, colonyPopulationPolicyDropDown, int_64, 250, string_30, string_31, string_32, ref int_65);
            int_65 += 25;
        }

        private void method_615(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, DesignList designList_0, Design design_3, ref int int_65)
        {
            DesignDropDown designDropDown = new DesignDropDown();
            designDropDown.BindData(designList_0, builtObjectImageCache_0.GetImagesSmall(), _Game.Galaxy.IndependentEmpire, allowNullDesign: true);
            designDropDown.Font = font_6;
            if (designDropDown.Items != null)
            {
                designDropDown.SetSelectedDesign(design_3);
            }
            method_626(panel_1, designDropDown, int_64, 250, string_30, string_31, string_32, ref int_65);
            int_65 += 25;
        }

        private void method_616(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, string[] string_33, BuiltObjectFleeWhen builtObjectFleeWhen_0, ref int int_65)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Popup;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(string_33);
            int num = 1;
            switch (builtObjectFleeWhen_0)
            {
                case BuiltObjectFleeWhen.Shields50:
                    num = 2;
                    break;
                case BuiltObjectFleeWhen.Shields20:
                    num = 1;
                    break;
                case BuiltObjectFleeWhen.Never:
                    num = 0;
                    break;
            }
            if (comboBox.Items != null && comboBox.Items.Count > num)
            {
                comboBox.SelectedIndex = num;
            }
            method_626(panel_1, comboBox, int_64, 250, string_30, string_31, string_32, ref int_65);
            int_65 += 25;
        }

        private void method_617(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, string[] string_33, double double_7, ref int int_65)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Popup;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(string_33);
            int num = 2;
            num = ((!(double_7 <= 0.0)) ? ((double_7 > 0.0 && double_7 <= 0.5) ? 1 : ((double_7 > 0.5 && double_7 <= 1.0) ? 2 : ((!(double_7 > 1.0) || !(double_7 <= 1.5)) ? 4 : 3))) : 0);
            if (comboBox.Items != null && comboBox.Items.Count > num)
            {
                comboBox.SelectedIndex = num;
            }
            method_626(panel_1, comboBox, int_64, 250, string_30, string_31, string_32, ref int_65);
            int_65 += 25;
        }

        private void method_618(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, string[] string_33, double double_7, ref int int_65)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Popup;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(string_33);
            int num = 1;
            num = ((!(double_7 <= 0.5)) ? ((double_7 > 0.5 && double_7 < 1.5) ? 1 : ((!(double_7 >= 1.5) || !(double_7 < 2.0)) ? 3 : 2)) : 0);
            if (comboBox.Items != null && comboBox.Items.Count > num)
            {
                comboBox.SelectedIndex = num;
            }
            method_626(panel_1, comboBox, int_64, 250, string_30, string_31, string_32, ref int_65);
            int_65 += 25;
        }

        private void method_619(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, string[] string_33, int int_65, ref int int_66)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Popup;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(string_33);
            int num = 0;
            num = int_65 + 1;
            if (comboBox.Items != null && comboBox.Items.Count > num)
            {
                comboBox.SelectedIndex = num;
            }
            method_626(panel_1, comboBox, int_64, 250, string_30, string_31, string_32, ref int_66);
            int_66 += 25;
        }

        private void method_620(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, string[] string_33, int int_65, ref int int_66)
        {
            method_621(panel_1, int_64, string_30, string_31, string_32, string_33, int_65, 250, ref int_66);
        }

        private void method_621(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, string[] string_33, int int_65, int int_66, ref int int_67)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Popup;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(string_33);
            if (comboBox.Items != null && comboBox.Items.Count > int_65)
            {
                comboBox.SelectedIndex = int_65;
            }
            method_626(panel_1, comboBox, int_64, int_66, string_30, string_31, string_32, ref int_67);
            int_67 += 25;
        }

        private void method_622(System.Windows.Forms.Panel panel_1, string string_30, string[] string_31, int int_64, int int_65, int int_66)
        {
            int num = 190;
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Popup;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(string_31);
            if (comboBox.Items != null && comboBox.Items.Count > int_64)
            {
                comboBox.SelectedIndex = int_64;
            }
            comboBox.Name = string_30;
            comboBox.Parent = panel_1;
            panel_1.Controls.Add(comboBox);
            comboBox.Location = new Point(int_65, int_66);
            comboBox.Size = new Size(num, comboBox.Height);
            comboBox.ForeColor = Color.FromArgb(170, 170, 170);
            comboBox.BackColor = Color.FromArgb(51, 51, 51);
            comboBox.BringToFront();
        }

        private void method_623(System.Windows.Forms.Panel panel_1, string string_30, string string_31, int int_64, bool bool_28, int int_65, int int_66)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = string_30;
            checkBox.Parent = panel_1;
            panel_1.Controls.Add(checkBox);
            checkBox.Location = new Point(int_65 - 15, int_66);
            checkBox.ForeColor = Color.FromArgb(170, 170, 170);
            checkBox.BackColor = Color.Transparent;
            checkBox.BringToFront();
            Label label = new Label();
            label.Text = string_31;
            label.Font = panel_1.Font;
            label.ForeColor = Color.FromArgb(170, 170, 170);
            label.Parent = panel_1;
            panel_1.Controls.Add(label);
            label.Size = new Size(int_64, 30);
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Location = new Point(int_65 - (15 + int_64), int_66);
            label.BringToFront();
        }

        private void method_624(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, bool bool_28, ref int int_65)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Checked = bool_28;
            method_626(panel_1, checkBox, int_64, 25, string_30, string_31, string_32, ref int_65);
            int_65 += 25;
        }

        private void method_625(System.Windows.Forms.Panel panel_1, int int_64, string string_30, string string_31, string string_32, float float_2, float float_3, float float_4, ref int int_65)
        {
            NumericUpDown numericUpDown = new NumericUpDown();
            numericUpDown.Minimum = (decimal)float_2;
            numericUpDown.Maximum = (decimal)float_3;
            float_4 = Math.Min(float_3, Math.Max(float_4, float_2));
            numericUpDown.Value = (decimal)float_4;
            method_626(panel_1, numericUpDown, int_64, 55, string_30, string_31, string_32, ref int_65);
            int_65 += 25;
        }

        private void method_626(System.Windows.Forms.Panel panel_1, Control control_1, int int_64, int int_65, string string_30, string string_31, string string_32, ref int int_66)
        {
            control_1.Name = string_30;
            control_1.Parent = panel_1;
            panel_1.Controls.Add(control_1);
            int num = 20;
            int num2 = int_66;
            if (control_1 is CheckBox)
            {
                num2--;
            }
            else if (!(control_1 is ComboBox) && !(control_1 is DesignDropDown) && !(control_1 is ResourceDropDown))
            {
                if (control_1 is NumericUpDown)
                {
                    num2 -= 2;
                    num = 17;
                }
            }
            else
            {
                num2 -= 2;
                num = 21;
            }
            control_1.Size = new Size(int_65, num);
            control_1.Location = new Point(int_64 + 2, num2);
            control_1.ForeColor = Color.FromArgb(170, 170, 170);
            if (!(control_1 is CheckBox))
            {
                control_1.BackColor = Color.FromArgb(51, 51, 51);
            }
            control_1.BringToFront();
            Label label = new Label();
            label.Text = string_31;
            label.Font = panel_1.Font;
            label.ForeColor = Color.FromArgb(170, 170, 170);
            label.Parent = panel_1;
            panel_1.Controls.Add(label);
            label.Size = new Size(int_64, 30);
            label.TextAlign = ContentAlignment.TopRight;
            label.Location = new Point(0, int_66);
            label.BringToFront();
            if (!string.IsNullOrEmpty(string_32))
            {
                if (string_32.Length > 30 && (control_1 is ComboBox || control_1 is DesignDropDown || control_1 is ResourceDropDown))
                {
                    toolTip_0.SetToolTip(control_1, string_32);
                    return;
                }
                Label label2 = new Label();
                label2.Text = string_32;
                label2.Font = panel_1.Font;
                label2.ForeColor = Color.FromArgb(170, 170, 170);
                label2.Parent = panel_1;
                panel_1.Controls.Add(label2);
                label2.Location = new Point(int_64 + 2 + int_65, int_66);
                label2.Size = new Size(300, 30);
                label2.BringToFront();
            }
        }

        private void WqesexberY_Click(object sender, EventArgs e)
        {
            _Game.PlayerEmpire.Policy = method_597(nDrsqatloR, _Game.PlayerEmpire);
            method_596();
        }

        private void XgxsOtuAmD_Click(object sender, EventArgs e)
        {
            method_596();
        }

        private void pnlEmpirePolicy_CloseButtonClicked(object sender, EventArgs e)
        {
            method_596();
        }

        private void btnEmpireSummaryShowEmpirePolicy_Click(object sender, EventArgs e)
        {
            method_595();
        }

        private void method_627(Control control_1, string string_30)
        {
            List<Control> list = new List<Control>();
            if (control_1.Controls != null && control_1.Controls.Count > 0)
            {
                for (int i = 0; i < control_1.Controls.Count; i++)
                {
                    if (control_1.Controls[i].Name.StartsWith(string_30))
                    {
                        list.Add(control_1.Controls[i]);
                    }
                }
            }
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j] is NumericUpDown)
                {
                    ((NumericUpDown)list[j]).ValueChanged -= method_634;
                }
                else if (list[j] is DesignDropDown)
                {
                    ((DesignDropDown)list[j]).SelectedValueChanged -= method_634;
                }
                list[j].Enter -= method_635;
                list[j].Parent = null;
                control_1.Controls.Remove(list[j]);
                list[j].Dispose();
            }
        }

        private void method_628()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            int num = 27;
            int num2 = 11;
            int num3 = 605;
            int num4 = 300;
            num3 += num2;
            num3 += num * 6;
            num4 += num2;
            num4 += num * 6;
            pnlBuildOrder.Size = new Size(810, num3);
            pnlBuildOrder.Location = new Point((mainView.Width - pnlBuildOrder.Width) / 2, (mainView.Height - pnlBuildOrder.Height) / 2);
            pnlBuildOrder.DoLayout();
            pnlBuildOrder.SuspendLayout();
            int num5 = 10;
            int int_ = 10;
            int int_2 = 140;
            int int_3 = 200;
            int int_4 = 260;
            int int_5 = 330;
            int num6 = 615;
            int num7 = 695;
            int int_6 = 120;
            int num8 = 50;
            int num9 = 50;
            int num10 = 60;
            int num11 = 280;
            int num12 = 70;
            int int_7 = 65;
            lblBuildOrderExplanation.Font = font_6;
            lblBuildOrderExplanation.Location = new Point(10, 10);
            lblBuildOrderExplanation.MaximumSize = new Size(720, 35);
            string text = TextResolver.GetText("Build Order Explanation");
            lblBuildOrderExplanation.Text = text;
            num5 = 60;
            pnlBuildOrderCurrentAmountColumn.Size = new Size(60, 235);
            pnlBuildOrderCurrentAmountColumn.Location = new Point(140, 60);
            pnlBuildOrderPurchaseAmountColumn.Size = new Size(60, 235);
            pnlBuildOrderPurchaseAmountColumn.Location = new Point(260, 60);
            pnlBuildOrderCostColumn.Size = new Size(60, 235);
            pnlBuildOrderCostColumn.Location = new Point(615, 60);
            pnlBuildOrderCurrentAmountColumn.Visible = false;
            pnlBuildOrderPurchaseAmountColumn.Visible = false;
            pnlBuildOrderCostColumn.Visible = false;
            if (!lblBuildOrderCurrentAmount.Font.Bold)
            {
                Font font = new Font(font_3, FontStyle.Bold);
                lblBuildOrderCurrentAmount.Font = font;
                lblBuildOrderAdvisorRecommendation.Font = font;
                lblBuildOrderPurchaseAmount.Font = font;
                lblBuildOrderDesign.Font = font;
                lblBuildOrderCost.Font = font;
                lblBuildOrderMaintenance.Font = font;
            }
            lblBuildOrderCurrentAmount.Text = TextResolver.GetText("Current Amount");
            lblBuildOrderAdvisorRecommendation.Text = TextResolver.GetText("Advisor Suggest");
            lblBuildOrderPurchaseAmount.Text = TextResolver.GetText("Order Amount");
            lblBuildOrderDesign.Text = TextResolver.GetText("Design");
            lblBuildOrderCost.Text = TextResolver.GetText("Purchase Costs");
            lblBuildOrderCost.Size = new Size(num12, lblBuildOrderCost.Height);
            lblBuildOrderCost.TextAlign = ContentAlignment.MiddleCenter;
            lblBuildOrderMaintenance.Text = TextResolver.GetText("Maintenance Costs Abbreviated");
            lblBuildOrderMaintenance.Size = new Size(int_7, lblBuildOrderMaintenance.Height);
            lblBuildOrderMaintenance.TextAlign = ContentAlignment.MiddleCenter;
            lblBuildOrderCurrentAmount.Location = new Point(int_2, num5);
            lblBuildOrderCurrentAmount.MaximumSize = new Size(num8 + 10, 40);
            lblBuildOrderCurrentAmount.TextAlign = ContentAlignment.MiddleCenter;
            lblBuildOrderAdvisorRecommendation.Location = new Point(int_3, num5);
            lblBuildOrderAdvisorRecommendation.MaximumSize = new Size(num9 + 10, 40);
            lblBuildOrderAdvisorRecommendation.TextAlign = ContentAlignment.MiddleCenter;
            lblBuildOrderPurchaseAmount.Location = new Point(int_4, num5);
            lblBuildOrderPurchaseAmount.MaximumSize = new Size(num10 + 10, 40);
            lblBuildOrderPurchaseAmount.TextAlign = ContentAlignment.MiddleCenter;
            lblBuildOrderDesign.AutoSize = false;
            lblBuildOrderDesign.Location = new Point(int_5, num5);
            lblBuildOrderDesign.MaximumSize = new Size(num11 + 10, 40);
            lblBuildOrderDesign.Size = new Size(num11, 40);
            lblBuildOrderDesign.TextAlign = ContentAlignment.MiddleLeft;
            lblBuildOrderCost.AutoSize = false;
            lblBuildOrderCost.Location = new Point(num6 + 5, num5);
            lblBuildOrderCost.Size = new Size(num12, 40);
            lblBuildOrderCost.TextAlign = ContentAlignment.MiddleRight;
            lblBuildOrderMaintenance.AutoSize = false;
            lblBuildOrderMaintenance.Location = new Point(num7 + 5, num5);
            lblBuildOrderMaintenance.Size = new Size(int_7, 40);
            lblBuildOrderMaintenance.TextAlign = ContentAlignment.MiddleRight;
            num5 = 0;
            pnlBuildOrderContainer.Size = new Size(760, num4);
            pnlBuildOrderContainer.Location = new Point(0, 105);
            pnlBuildOrderContainer.SuspendLayout();
            method_627(pnlBuildOrderContainer, "_");
            double annualSupportCosts = 0.0;
            ForceStructureProjectionList forceStructureProjectionList = _Game.PlayerEmpire.CurrentStateForceStructure(out annualSupportCosts);
            ForceStructureProjectionList forceStructureProjectionList2 = new ForceStructureProjectionList();
            if (_Game.PlayerEmpire.StateForceStructureProjections != null)
            {
                forceStructureProjectionList2 = _Game.PlayerEmpire.StateForceStructureProjections.Clone();
                double annualSupportCosts2 = 0.0;
                ForceStructureProjectionList items = _Game.PlayerEmpire.CurrentPrivateForceStructure(out annualSupportCosts2);
                forceStructureProjectionList.AddRange(items);
                ForceStructureProjectionList items2 = _Game.PlayerEmpire.PrivateForceStructureProjections.Clone();
                forceStructureProjectionList2.AddRange(items2);
            }
            forceStructureProjectionList2.Sort();
            double totalSupportCosts = 0.0;
            double totalPurchaseCosts = 0.0;
            ForceStructureProjectionList forceStructureProjectionList3 = _Game.PlayerEmpire.RefactorForceStructureProjectionsToCosts(forceStructureProjectionList2, includeCashflowCheck: false, out totalSupportCosts, out totalPurchaseCosts, randomizedOrder: false);
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            int num22 = 0;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            int num26 = 0;
            int num27 = 0;
            int num28 = 0;
            if (forceStructureProjectionList3 != null && forceStructureProjectionList != null)
            {
                ForceStructureProjection bySubRole = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.Escort);
                ForceStructureProjection bySubRole2 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.Frigate);
                ForceStructureProjection bySubRole3 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.Destroyer);
                ForceStructureProjection bySubRole4 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.Cruiser);
                ForceStructureProjection bySubRole5 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.CapitalShip);
                ForceStructureProjection bySubRole6 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.TroopTransport);
                ForceStructureProjection bySubRole7 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.Carrier);
                ForceStructureProjection bySubRole8 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.ResupplyShip);
                ForceStructureProjection bySubRole9 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.ExplorationShip);
                ForceStructureProjection bySubRole10 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.ConstructionShip);
                ForceStructureProjection bySubRole11 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.SmallFreighter);
                ForceStructureProjection bySubRole12 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.MediumFreighter);
                ForceStructureProjection bySubRole13 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.LargeFreighter);
                ForceStructureProjection bySubRole14 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.MiningShip);
                ForceStructureProjection bySubRole15 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.GasMiningShip);
                ForceStructureProjection bySubRole16 = forceStructureProjectionList3.GetBySubRole(BuiltObjectSubRole.PassengerShip);
                ForceStructureProjection forceStructureProjection = null;
                ForceStructureProjection forceStructureProjection2 = null;
                ForceStructureProjection forceStructureProjection3 = null;
                ForceStructureProjection forceStructureProjection4 = null;
                ForceStructureProjection forceStructureProjection5 = null;
                ForceStructureProjection forceStructureProjection6 = null;
                ForceStructureProjection forceStructureProjection7 = null;
                ForceStructureProjection forceStructureProjection8 = null;
                ForceStructureProjection forceStructureProjection9 = null;
                ForceStructureProjection forceStructureProjection10 = null;
                ForceStructureProjection forceStructureProjection11 = null;
                ForceStructureProjection forceStructureProjection12 = null;
                ForceStructureProjection forceStructureProjection13 = null;
                ForceStructureProjection forceStructureProjection14 = null;
                ForceStructureProjection forceStructureProjection15 = null;
                ForceStructureProjection forceStructureProjection16 = null;
                forceStructureProjection = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.Escort);
                forceStructureProjection2 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.Frigate);
                forceStructureProjection3 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.Destroyer);
                forceStructureProjection4 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.Cruiser);
                forceStructureProjection5 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.CapitalShip);
                forceStructureProjection6 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.TroopTransport);
                forceStructureProjection7 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.Carrier);
                forceStructureProjection8 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.ResupplyShip);
                forceStructureProjection9 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.ExplorationShip);
                forceStructureProjection10 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.ConstructionShip);
                forceStructureProjection11 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.SmallFreighter);
                forceStructureProjection12 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.MediumFreighter);
                forceStructureProjection13 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.LargeFreighter);
                forceStructureProjection14 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.MiningShip);
                forceStructureProjection15 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.GasMiningShip);
                forceStructureProjection16 = forceStructureProjectionList.GetBySubRole(BuiltObjectSubRole.PassengerShip);
                if (bySubRole != null)
                {
                    num13 = bySubRole.Amount;
                    if (forceStructureProjection != null)
                    {
                        num13 += forceStructureProjection.Amount;
                    }
                }
                if (bySubRole2 != null)
                {
                    num14 = bySubRole2.Amount;
                    if (forceStructureProjection2 != null)
                    {
                        num14 += forceStructureProjection2.Amount;
                    }
                }
                if (bySubRole3 != null)
                {
                    num15 = bySubRole3.Amount;
                    if (forceStructureProjection3 != null)
                    {
                        num15 += forceStructureProjection3.Amount;
                    }
                }
                if (bySubRole4 != null)
                {
                    num16 = bySubRole4.Amount;
                    if (forceStructureProjection4 != null)
                    {
                        num16 += forceStructureProjection4.Amount;
                    }
                }
                if (bySubRole5 != null)
                {
                    num17 = bySubRole5.Amount;
                    if (forceStructureProjection5 != null)
                    {
                        num17 += forceStructureProjection5.Amount;
                    }
                }
                if (bySubRole6 != null)
                {
                    num18 = bySubRole6.Amount;
                    if (forceStructureProjection6 != null)
                    {
                        num18 += forceStructureProjection6.Amount;
                    }
                }
                if (bySubRole7 != null)
                {
                    num19 = bySubRole7.Amount;
                    if (forceStructureProjection7 != null)
                    {
                        num19 += forceStructureProjection7.Amount;
                    }
                }
                if (bySubRole8 != null)
                {
                    num20 = bySubRole8.Amount;
                    if (forceStructureProjection8 != null)
                    {
                        num20 += forceStructureProjection8.Amount;
                    }
                }
                if (bySubRole9 != null)
                {
                    num21 = bySubRole9.Amount;
                    if (forceStructureProjection9 != null)
                    {
                        num21 += forceStructureProjection9.Amount;
                    }
                }
                if (bySubRole10 != null)
                {
                    num22 = bySubRole10.Amount;
                    if (forceStructureProjection10 != null)
                    {
                        num22 += forceStructureProjection10.Amount;
                    }
                }
                if (bySubRole11 != null)
                {
                    num23 = bySubRole11.Amount;
                    if (forceStructureProjection11 != null)
                    {
                        num23 += forceStructureProjection11.Amount;
                    }
                }
                if (bySubRole12 != null)
                {
                    num24 = bySubRole12.Amount;
                    if (forceStructureProjection12 != null)
                    {
                        num24 += forceStructureProjection12.Amount;
                    }
                }
                if (bySubRole13 != null)
                {
                    num25 = bySubRole13.Amount;
                    if (forceStructureProjection13 != null)
                    {
                        num25 += forceStructureProjection13.Amount;
                    }
                }
                if (bySubRole14 != null)
                {
                    num26 = bySubRole14.Amount;
                    if (forceStructureProjection14 != null)
                    {
                        num26 += forceStructureProjection14.Amount;
                    }
                }
                if (bySubRole15 != null)
                {
                    num27 = bySubRole15.Amount;
                    if (forceStructureProjection15 != null)
                    {
                        num27 += forceStructureProjection15.Amount;
                    }
                }
                if (bySubRole16 != null)
                {
                    num28 = bySubRole16.Amount;
                    if (forceStructureProjection16 != null)
                    {
                        num28 += forceStructureProjection16.Amount;
                    }
                }
            }
            bool bool_ = true;
            if (_Game.PlayerEmpire.ControlStateConstruction == AutomationLevel.Manual)
            {
                bool_ = false;
            }
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.Escort, num13, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.Frigate, num14, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.Destroyer, num15, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.Cruiser, num16, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.CapitalShip, num17, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.TroopTransport, num18, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.Carrier, num19, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            num5 += num2;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.ResupplyShip, num20, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            num5 += num2;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.ExplorationShip, num21, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.ConstructionShip, num22, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            int num29 = 420;
            num29 += num2;
            num29 += num * 6;
            num5 += num2;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.SmallFreighter, num23, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.MediumFreighter, num24, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.LargeFreighter, num25, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.MiningShip, num26, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.GasMiningShip, num27, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            method_629(pnlBuildOrderContainer, num5, BuiltObjectSubRole.PassengerShip, num28, bool_, int_, int_2, int_3, int_4, int_5, num6, num7, int_6, num8, num9, num10, num11, num12, int_7);
            num5 += num;
            lblBuildOrderAvailableCashflowLabel.Visible = true;
            lblBuildOrderAvailableCashflowLabel.Text = TextResolver.GetText("TOTAL Purchase and Maintenance Costs");
            lblBuildOrderAvailableCashflowLabel.Font = font_7;
            lblBuildOrderAvailableCashflowLabel.Location = new Point(357, num29);
            FfJsLkoYvX.AutoSize = false;
            FfJsLkoYvX.Location = new Point(num6 - 5, num29 + 4);
            FfJsLkoYvX.TextAlign = ContentAlignment.MiddleRight;
            FfJsLkoYvX.Size = new Size(num12 + 5, FfJsLkoYvX.Height);
            FfJsLkoYvX.Font = font_7;
            FfJsLkoYvX.ForeColor = Color.FromArgb(255, 255, 0);
            FfJsLkoYvX.BringToFront();
            lblBuildOrderTotalMaintenance.AutoSize = false;
            lblBuildOrderTotalMaintenance.Location = new Point(num7, num29 + 4);
            lblBuildOrderTotalMaintenance.TextAlign = ContentAlignment.MiddleRight;
            lblBuildOrderTotalMaintenance.Size = new Size(int_7, lblBuildOrderTotalMaintenance.Height);
            lblBuildOrderTotalMaintenance.Font = font_6;
            lblBuildOrderTotalMaintenance.ForeColor = Color.FromArgb(255, 255, 0);
            lblBuildOrderTotalMaintenance.BringToFront();
            lblBuildOrderAvailableFundsLabel.Text = TextResolver.GetText("Available Money and Cashflow");
            lblBuildOrderAvailableFundsLabel.Font = font_7;
            lblBuildOrderAvailableFundsLabel.Location = new Point(414, num29 + 30);
            lblBuildOrderAvailableFunds.Location = new Point(num6 - 5, num29 + 34);
            lblBuildOrderAvailableFunds.Font = font_7;
            lblBuildOrderAvailableFunds.TextAlign = ContentAlignment.MiddleRight;
            lblBuildOrderAvailableFunds.Text = _Game.PlayerEmpire.StateMoney.ToString("###,###,###,##0");
            lblBuildOrderAvailableFunds.AutoSize = false;
            lblBuildOrderAvailableFunds.Size = new Size(num12 + 5, lblBuildOrderAvailableFunds.Height);
            iqosaeoiKu.Location = new Point(num7, num29 + 34);
            iqosaeoiKu.Font = font_6;
            iqosaeoiKu.TextAlign = ContentAlignment.MiddleRight;
            iqosaeoiKu.Text = AheLexjQsu.ToString("###,###,###,##0");
            iqosaeoiKu.AutoSize = false;
            iqosaeoiKu.Size = new Size(int_7, iqosaeoiKu.Height);
            method_631();
            btnBuildOrderCancel.Size = new Size(238, 40);
            btnBuildOrderCancel.Location = new Point(12, num29 + 67);
            btnBuildOrderPurchase.Size = new Size(520, 40);
            btnBuildOrderPurchase.Location = new Point(260, num29 + 67);
            pnlBuildOrderContainer.ResumeLayout();
            pnlBuildOrder.ResumeLayout();
            pnlBuildOrder.Visible = true;
            pnlBuildOrder.BringToFront();
        }

        private void method_629(Control control_1, int int_64, BuiltObjectSubRole builtObjectSubRole_0, int int_65, bool bool_28, int int_66, int int_67, int int_68, int int_69, int int_70, int int_71, int int_72, int int_73, int int_74, int int_75, int int_76, int int_77, int int_78, int int_79)
        {
            string text = Galaxy.ResolveDescription(builtObjectSubRole_0);
            string text2 = "_" + text.Replace(" ", "");
            Label label = method_636(control_1, int_66, int_64, text2 + "Type", text);
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Size = new Size(int_73, label.Height);
            label.Font = font_7;
            int num = 0;
            BuiltObjectList builtObjectList = null;
            switch (builtObjectSubRole_0)
            {
                default:
                    builtObjectList = _Game.PlayerEmpire.BuiltObjects.GetBuiltObjectsBySubRole(builtObjectSubRole_0);
                    if (builtObjectList != null)
                    {
                        num = builtObjectList.Count;
                    }
                    break;
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    builtObjectList = _Game.PlayerEmpire.PrivateBuiltObjects.GetBuiltObjectsBySubRole(builtObjectSubRole_0);
                    if (builtObjectList != null)
                    {
                        num = builtObjectList.Count;
                    }
                    break;
            }
            num = BaconMain.GetNumberOfShipsOfSubRole(this, builtObjectSubRole_0);
            Label label2 = method_636(control_1, int_67, int_64, text2 + "CurrentAmount", num.ToString());
            label2.Size = new Size(int_74, label2.Height);
            int_65 = Math.Min(int_65, 1000);
            int num2 = Math.Max(0, int_65 - num);
            Label label3 = method_636(control_1, int_68, int_64, text2 + "AdvisorRecommendation", num2.ToString());
            label3.Size = new Size(int_75, label3.Height);
            NumericUpDown numericUpDown = null;
            numericUpDown = ((!bool_28) ? method_637(control_1, int_69, int_64, text2 + "OrderAmount", 0, 1000, 0) : method_637(control_1, int_69, int_64, text2 + "OrderAmount", 0, 1000, num2));
            numericUpDown.TextAlign = HorizontalAlignment.Center;
            bool flag = true;
            if ((builtObjectSubRole_0 == BuiltObjectSubRole.ResupplyShip || builtObjectSubRole_0 == BuiltObjectSubRole.ConstructionShip) && _Game.PlayerEmpire.PirateEmpireBaseHabitat != null && !_Game.PlayerEmpire.CheckPirateEmpireHasCriminalNetwork(_Game.PlayerEmpire) && !_Game.PlayerEmpire.CheckEmpireHasOwnedColonies(_Game.PlayerEmpire))
            {
                flag = false;
                numericUpDown.Value = 0m;
                numericUpDown.Enabled = false;
            }
            if (flag && num2 > 0 && bool_28)
            {
                numericUpDown.BackColor = Color.FromArgb(51, 51, 51);
                numericUpDown.ForeColor = Color.Yellow;
                numericUpDown.Font = font_7;
            }
            DesignList buildableDesignsBySubRoles = _Game.PlayerEmpire.Designs.GetBuildableDesignsBySubRoles(new List<BuiltObjectSubRole> { builtObjectSubRole_0 }, _Game.PlayerEmpire);
            Design design_ = _Game.PlayerEmpire.Designs.FindNewestCanBuild(builtObjectSubRole_0);
            Control control = method_630(control_1, int_70, int_64, text2 + "Design", buildableDesignsBySubRoles, design_);
            control.Size = new Size(int_77, control.Height);
            if (control is Label)
            {
                numericUpDown.Value = 0m;
                numericUpDown.Enabled = false;
            }
            Label label4 = method_636(control_1, int_71, int_64, text2 + "Cost", "0");
            label4.TextAlign = ContentAlignment.MiddleRight;
            label4.Size = new Size(int_78, label4.Height);
            label4.Font = font_7;
            Label label5 = method_636(control_1, int_72, int_64, text2 + "Maintenance", "0");
            label5.TextAlign = ContentAlignment.MiddleRight;
            label5.Size = new Size(int_79, label5.Height);
            label5.Font = font_6;
        }

        private Control method_630(Control control_1, int int_64, int int_65, string string_30, DesignList designList_0, Design design_3)
        {
            Control control = null;
            if (designList_0 != null && designList_0.Count > 0)
            {
                if ((_Game.PlayerEmpire.ConstructionYards == null || _Game.PlayerEmpire.ConstructionYards.Count <= 0) && designList_0[0].SubRole != BuiltObjectSubRole.ColonyShip && designList_0[0].SubRole != BuiltObjectSubRole.ConstructionShip && designList_0[0].SubRole != BuiltObjectSubRole.ResupplyShip)
                {
                    control = new Label();
                    control.BackColor = Color.Transparent;
                    control.Font = font_6;
                    ((Label)control).TextAlign = ContentAlignment.MiddleCenter;
                    control.Text = "(" + TextResolver.GetText("No construction yards for this ship type") + ")";
                }
                else
                {
                    DesignDropDown designDropDown = new DesignDropDown();
                    designDropDown.BindData(designList_0, builtObjectImageCache_0.GetImagesSmall(), _Game.Galaxy.IndependentEmpire, allowNullDesign: false);
                    designDropDown.Font = font_6;
                    designDropDown.Size = new Size(250, designDropDown.Height);
                    if (designDropDown.Items != null)
                    {
                        designDropDown.SetSelectedDesign(design_3);
                    }
                    designDropDown.SelectedValueChanged += method_634;
                    control = designDropDown;
                }
            }
            else
            {
                control = new Label();
                control.BackColor = Color.Transparent;
                control.Font = font_6;
                ((Label)control).TextAlign = ContentAlignment.MiddleCenter;
                control.Text = "(" + TextResolver.GetText("No buildable designs") + ")";
            }
            return method_638(control_1, control, int_64, int_65, string_30);
        }

        private void method_631()
        {
            double double_ = 0.0;
            double num = method_632(out double_);
            FfJsLkoYvX.Text = num.ToString("###,###,###,##0");
            lblBuildOrderTotalMaintenance.Text = double_.ToString("###,###,###,##0");
            if (num > 0.0)
            {
                btnBuildOrderPurchase.Text = string.Format(TextResolver.GetText("Purchase for X credits"), num.ToString("###,###,###,##0"));
                btnBuildOrderPurchase.Enabled = true;
            }
            else
            {
                btnBuildOrderPurchase.Text = TextResolver.GetText("Purchase");
                btnBuildOrderPurchase.Enabled = false;
            }
        }

        private double method_632(out double double_7)
        {
            double num = 0.0;
            double_7 = 0.0;
            Control control_ = pnlBuildOrderContainer;
            num += method_633(control_, BuiltObjectSubRole.Escort, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.Frigate, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.Destroyer, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.Cruiser, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.CapitalShip, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.TroopTransport, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.Carrier, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.ResupplyShip, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.ExplorationShip, ref double_7);
            num += method_633(control_, BuiltObjectSubRole.ConstructionShip, ref double_7);
            double double_8 = 0.0;
            num += method_633(control_, BuiltObjectSubRole.SmallFreighter, ref double_8);
            num += method_633(control_, BuiltObjectSubRole.MediumFreighter, ref double_8);
            num += method_633(control_, BuiltObjectSubRole.LargeFreighter, ref double_8);
            num += method_633(control_, BuiltObjectSubRole.MiningShip, ref double_8);
            num += method_633(control_, BuiltObjectSubRole.GasMiningShip, ref double_8);
            return num + method_633(control_, BuiltObjectSubRole.PassengerShip, ref double_8);
        }

        private double method_633(Control control_1, BuiltObjectSubRole builtObjectSubRole_0, ref double double_7)
        {
            int num = method_640(control_1, builtObjectSubRole_0);
            double result = (double)num * method_641(control_1, builtObjectSubRole_0);
            string text = Galaxy.ResolveDescription(builtObjectSubRole_0);
            string text2 = "_" + text.Replace(" ", "");
            string key = text2 + "Cost";
            if (control_1 != null && control_1.Controls != null && control_1.Controls.Count > 0)
            {
                Control control = control_1.Controls[key];
                if (control != null)
                {
                    control.Text = result.ToString("###,###,###,##0");
                }
            }
            double num2 = 0.0;
            bool flag = true;
            switch (builtObjectSubRole_0)
            {
                default:
                    flag = true;
                    break;
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    flag = false;
                    break;
            }
            Design design = method_642(control_1, builtObjectSubRole_0);
            if (flag && design != null)
            {
                num2 = design.CalculateMaintenanceCosts(_Game.Galaxy, _Game.PlayerEmpire);
                num2 *= (double)num;
            }
            key = text2 + "Maintenance";
            if (control_1 != null && control_1.Controls != null && control_1.Controls.Count > 0)
            {
                Control control2 = control_1.Controls[key];
                if (control2 != null)
                {
                    control2.Text = num2.ToString("###,###,###,##0");
                }
            }
            double_7 += num2;
            return result;
        }

        private void method_634(object sender, EventArgs e)
        {
            method_631();
            if (sender is NumericUpDown)
            {
                NumericUpDown numericUpDown = (NumericUpDown)sender;
                if (numericUpDown.Value > 0m)
                {
                    numericUpDown.BackColor = Color.FromArgb(51, 51, 51);
                    numericUpDown.ForeColor = Color.Yellow;
                    numericUpDown.Font = font_7;
                }
                else
                {
                    numericUpDown.BackColor = Color.FromArgb(51, 51, 51);
                    numericUpDown.ForeColor = Color.FromArgb(170, 170, 170);
                    numericUpDown.Font = font_6;
                }
            }
        }

        private void method_635(object sender, EventArgs e)
        {
            if (sender is NumericUpDown)
            {
                NumericUpDown numericUpDown = (NumericUpDown)sender;
                numericUpDown.Select(0, numericUpDown.ToString().Length);
            }
        }

        private Label method_636(Control control_1, int int_64, int int_65, string string_30, string string_31)
        {
            Label label = new Label();
            label.BackColor = Color.Transparent;
            label.Font = font_6;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = string_31;
            return (Label)method_638(control_1, label, int_64, int_65, string_30);
        }

        private NumericUpDown method_637(Control control_1, int int_64, int int_65, string string_30, int int_66, int int_67, int int_68)
        {
            NumericUpDown numericUpDown = new NumericUpDown();
            numericUpDown.Size = new Size(60, numericUpDown.Height);
            numericUpDown.Font = font_6;
            numericUpDown.Minimum = int_66;
            numericUpDown.Maximum = int_67;
            numericUpDown.Value = int_68;
            if (int_68 > 0)
            {
                numericUpDown.BackColor = Color.FromArgb(51, 51, 51);
                numericUpDown.ForeColor = Color.Yellow;
                numericUpDown.Font = font_7;
            }
            numericUpDown.ValueChanged += method_634;
            numericUpDown.Enter += method_635;
            return (NumericUpDown)method_638(control_1, numericUpDown, int_64, int_65, string_30);
        }

        private Control method_638(Control control_1, Control control_2, int int_64, int int_65, string string_30)
        {
            control_2.Name = string_30;
            control_2.Parent = control_1;
            control_1.Controls.Add(control_2);
            if (control_2 is ComboBox || control_2 is DesignDropDown || control_2 is ResourceDropDown)
            {
            }
            control_2.Location = new Point(int_64, int_65);
            control_2.ForeColor = Color.FromArgb(170, 170, 170);
            if (!(control_2 is Label))
            {
                control_2.BackColor = Color.FromArgb(51, 51, 51);
            }
            control_2.BringToFront();
            return control_2;
        }

        private void method_639()
        {
            pnlBuildOrder.SendToBack();
            pnlBuildOrder.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
            SetMainFocus();
        }

        private void pnlBuildOrder_CloseButtonClicked(object sender, EventArgs e)
        {
            method_639();
        }

        private int method_640(Control control_1, BuiltObjectSubRole builtObjectSubRole_0)
        {
            string text = Galaxy.ResolveDescription(builtObjectSubRole_0);
            string text2 = "_" + text.Replace(" ", "");
            string key = text2 + "OrderAmount";
            if (control_1 != null && control_1.Controls != null && control_1.Controls.Count > 0)
            {
                Control control = control_1.Controls[key];
                if (control != null && control is NumericUpDown)
                {
                    return (int)((NumericUpDown)control).Value;
                }
            }
            return 0;
        }

        private double method_641(Control control_1, BuiltObjectSubRole builtObjectSubRole_0)
        {
            return method_642(control_1, builtObjectSubRole_0)?.CalculateCurrentPurchasePrice(_Game.Galaxy) ?? 0.0;
        }

        private Design method_642(Control control_1, BuiltObjectSubRole builtObjectSubRole_0)
        {
            string text = Galaxy.ResolveDescription(builtObjectSubRole_0);
            string text2 = "_" + text.Replace(" ", "");
            string key = text2 + "Design";
            if (control_1 != null && control_1.Controls != null && control_1.Controls.Count > 0)
            {
                Control control = control_1.Controls[key];
                if (control != null && control is DesignDropDown)
                {
                    DesignDropDown designDropDown = (DesignDropDown)control;
                    return designDropDown.SelectedDesign;
                }
            }
            return null;
        }

        private void btnBuildOrderPurchase_Click(object sender, EventArgs e)
        {
            double double_ = 0.0;
            double num = method_632(out double_);
            if (num > _Game.PlayerEmpire.StateMoney)
            {
                string string_ = string.Format(TextResolver.GetText("Build Order Purchase Cannot Afford"), num.ToString("###,###,###,##0"), _Game.PlayerEmpire.StateMoney.ToString("###,###,###,##0"));
                MessageBoxEx messageBoxEx = method_371(string_, TextResolver.GetText("Cannot afford build order"), MessageBoxExIcon.Hand);
                messageBoxEx.Show(this);
                return;
            }
            DesignList designList_ = new DesignList();
            List<int> list_ = new List<int>();
            method_643(BuiltObjectSubRole.Escort, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.Frigate, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.Destroyer, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.Cruiser, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.CapitalShip, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.TroopTransport, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.Carrier, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.ResupplyShip, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.ExplorationShip, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.ConstructionShip, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.SmallFreighter, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.MediumFreighter, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.LargeFreighter, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.MiningShip, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.GasMiningShip, ref designList_, ref list_);
            method_643(BuiltObjectSubRole.PassengerShip, ref designList_, ref list_);
            _Game.PlayerEmpire.BuildNewShips(designList_, list_);
            method_639();
        }

        private void method_643(BuiltObjectSubRole builtObjectSubRole_0, ref DesignList designList_0, ref List<int> list_7)
        {
            int num = method_640(pnlBuildOrderContainer, builtObjectSubRole_0);
            Design design = method_642(pnlBuildOrderContainer, builtObjectSubRole_0);
            if (design != null && num > 0)
            {
                designList_0.Add(design);
                list_7.Add(num);
            }
        }

        private void btnBuildOrderCancel_Click(object sender, EventArgs e)
        {
            method_639();
        }

        private void btnEmpirePolicy_Click(object sender, EventArgs e)
        {
            if (pnlEmpirePolicy.Visible)
            {
                method_596();
            }
            else
            {
                method_595();
            }
        }

        private void btnBuildOrder_Click(object sender, EventArgs e)
        {
            if (pnlBuildOrder.Visible)
            {
                method_639();
            }
            else
            {
                method_628();
            }
        }

        private void btnMapCivilianFade_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayFadeCivilianShips = !gameOptions_0.MapOverlayFadeCivilianShips;
                btnMapCivilianFade.ToggledOn = gameOptions_0.MapOverlayFadeCivilianShips;
                if (gameOptions_0.MapOverlayFadeCivilianShips)
                {
                    btnMapCivilianFade.Hint = TextResolver.GetText("Fade civilian ships and bases") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapCivilianFade.Hint = TextResolver.GetText("Fade civilian ships and bases") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
            }
        }

        private void btnMapOverlay1_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayFleetPostures = !gameOptions_0.MapOverlayFleetPostures;
                btnMapOverlay1.ToggledOn = gameOptions_0.MapOverlayFleetPostures;
                if (gameOptions_0.MapOverlayFleetPostures)
                {
                    btnMapOverlay1.Hint = TextResolver.GetText("Show Fleet Postures") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay1.Hint = TextResolver.GetText("Show Fleet Postures") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
            }
        }

        private void btnMapOverlay2_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayTravelVectorsState = !gameOptions_0.MapOverlayTravelVectorsState;
                btnMapOverlay2.ToggledOn = gameOptions_0.MapOverlayTravelVectorsState;
                if (gameOptions_0.MapOverlayTravelVectorsState)
                {
                    btnMapOverlay2.Hint = TextResolver.GetText("Show Travel Vectors State") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay2.Hint = TextResolver.GetText("Show Travel Vectors State") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
            }
        }

        private void btnMapOverlay3_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayTravelVectorsPrivate = !gameOptions_0.MapOverlayTravelVectorsPrivate;
                btnMapOverlay3.ToggledOn = gameOptions_0.MapOverlayTravelVectorsPrivate;
                if (gameOptions_0.MapOverlayTravelVectorsPrivate)
                {
                    btnMapOverlay3.Hint = TextResolver.GetText("Show Travel Vectors Private") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay3.Hint = TextResolver.GetText("Show Travel Vectors Private") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
            }
        }

        private void btnMapOverlay4_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayPotentialColonies = !gameOptions_0.MapOverlayPotentialColonies;
                btnMapOverlay4.ToggledOn = gameOptions_0.MapOverlayPotentialColonies;
                if (gameOptions_0.MapOverlayPotentialColonies)
                {
                    btnMapOverlay4.Hint = TextResolver.GetText("Show Potential Colonies") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay4.Hint = TextResolver.GetText("Show Potential Colonies") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
            }
        }

        private void btnMapOverlay5_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayScenicLocations = !gameOptions_0.MapOverlayScenicLocations;
                btnMapOverlay5.ToggledOn = gameOptions_0.MapOverlayScenicLocations;
                if (gameOptions_0.MapOverlayScenicLocations)
                {
                    btnMapOverlay5.Hint = TextResolver.GetText("Show Scenic Locations") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay5.Hint = TextResolver.GetText("Show Scenic Locations") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
            }
        }

        private void btnMapOverlay6_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayResearchLocations = !gameOptions_0.MapOverlayResearchLocations;
                btnMapOverlay6.ToggledOn = gameOptions_0.MapOverlayResearchLocations;
                if (gameOptions_0.MapOverlayResearchLocations)
                {
                    btnMapOverlay6.Hint = TextResolver.GetText("Show Research Locations") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay6.Hint = TextResolver.GetText("Show Research Locations") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
            }
        }

        private void btnMapOverlay7_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayLongRangeScanners = !gameOptions_0.MapOverlayLongRangeScanners;
                btnMapOverlay7.ToggledOn = gameOptions_0.MapOverlayLongRangeScanners;
                if (gameOptions_0.MapOverlayLongRangeScanners)
                {
                    btnMapOverlay7.Hint = TextResolver.GetText("Show Long Range Scanners") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay7.Hint = TextResolver.GetText("Show Long Range Scanners") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                ResetGalaxyBackdropsBackgroundThread();
            }
        }

        private void btnMapOverlay8_Click(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.MapOverlayEmpireTerritory = !gameOptions_0.MapOverlayEmpireTerritory;
                btnMapOverlay8.ToggledOn = gameOptions_0.MapOverlayEmpireTerritory;
                if (gameOptions_0.MapOverlayEmpireTerritory)
                {
                    btnMapOverlay8.Hint = TextResolver.GetText("Show Empire Territory") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay8.Hint = TextResolver.GetText("Show Empire Territory") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                ResetGalaxyBackdropsBackgroundThread();
            }
        }

        private void pnlAdvisorSuggestion_CloseButtonClicked(object sender, EventArgs e)
        {
            method_644();
            method_660();
        }

        private void btnAdvisorSuggestionApprove_Click(object sender, EventArgs e)
        {
            if (empireMessage_0 != null && empireMessage_0.MessageType == EmpireMessageType.AdvisorSuggestion)
            {
                switch (empireMessage_0.AdvisorMessageType)
                {
                    case AdvisorMessageType.BuildOneOff:
                        {
                            if (!(empireMessage_0.Subject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat6 = (Habitat)empireMessage_0.Subject;
                            if (habitat6 == null || empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is Design))
                            {
                                break;
                            }
                            Design design3 = (Design)empireMessage_0.AdvisorMessageData;
                            if (design3 == null)
                            {
                                break;
                            }
                            double num7 = design3.CalculateCurrentPurchasePrice(_Game.Galaxy);
                            _Game.PlayerEmpire.CalculateSupportCost(design3);
                            _Game.PlayerEmpire.CalculateSpareAnnualRevenueComplete();
                            if (!(num7 <= _Game.PlayerEmpire.StateMoney))
                            {
                                break;
                            }
                            design3.BuildCount++;
                            BuiltObject builtObject6 = new BuiltObject(design3, _Game.Galaxy.GenerateBuiltObjectName(design3), _Game.Galaxy);
                            builtObject6.PurchasePrice = num7;
                            if (habitat6.ConstructionQueue != null && habitat6.ConstructionQueue.AddBuiltObjectToConstruct(builtObject6))
                            {
                                if (design3.SubRole == BuiltObjectSubRole.DefensiveBase)
                                {
                                    string[] array = new string[5]
                                    {
                            TextResolver.GetText("Ship SubRole DefensiveBase"),
                            TextResolver.GetText("Weapons Platform"),
                            TextResolver.GetText("Defense Platform"),
                            TextResolver.GetText("Defense Battery"),
                            TextResolver.GetText("Orbital Battery")
                                    };
                                    builtObject6.Name = habitat6.Name + " " + array[Galaxy.Rnd.Next(0, array.Length)];
                                }
                                else
                                {
                                    builtObject6.Name = _Game.Galaxy.GenerateBuiltObjectName(design3, habitat6);
                                }
                                double offsetX = 0.0;
                                double offsetY = 0.0;
                                _Game.PlayerEmpire.DetermineOrbitalBaseLocation(habitat6, out offsetX, out offsetY);
                                if (design3.SubRole == BuiltObjectSubRole.SmallSpacePort || design3.SubRole == BuiltObjectSubRole.MediumSpacePort || design3.SubRole == BuiltObjectSubRole.LargeSpacePort)
                                {
                                    double range = (double)(habitat6.Diameter / 6) + 15.0;
                                    _Game.Galaxy.SelectRelativePoint(range, out offsetX, out offsetY);
                                }
                                builtObject6.Heading = _Game.Galaxy.SelectRandomHeading();
                                builtObject6.TargetHeading = builtObject6.Heading;
                                _Game.PlayerEmpire.AddBuiltObjectToGalaxy(builtObject6, habitat6, offsetLocationFromParent: false, isStateOwned: true, (int)offsetX, (int)offsetY);
                                _Game.PlayerEmpire.StateMoney -= num7;
                                _Game.PlayerEmpire.PirateEconomy.PerformExpense(num7, PirateExpenseType.Construction, _Game.Galaxy.CurrentStarDate);
                                builtObject6.BuiltAt = habitat6;
                                CargoList resourcesToOrder3 = null;
                                _Game.PlayerEmpire.ProcureConstructionComponents(builtObject6, habitat6, out resourcesToOrder3);
                                foreach (Cargo item in resourcesToOrder3)
                                {
                                    _Game.PlayerEmpire.CreateOrder(habitat6, item.CommodityResource, item.Amount, isState: false, OrderType.ConstructionShortage);
                                }
                            }
                            else
                            {
                                design3.BuildCount--;
                            }
                            break;
                        }
                    case AdvisorMessageType.Colonization:
                        {
                            if (!(empireMessage_0.Subject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat = (Habitat)empireMessage_0.Subject;
                            string explanation = string.Empty;
                            if (!_Game.PlayerEmpire.CanEmpireColonizeHabitat(habitat, out explanation))
                            {
                                break;
                            }
                            if (empireMessage_0.AdvisorMessageData != null)
                            {
                                if (empireMessage_0.AdvisorMessageData is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)empireMessage_0.AdvisorMessageData;
                                    if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip)
                                    {
                                        builtObject.AssignMission(BuiltObjectMissionType.Colonize, habitat, null, BuiltObjectMissionPriority.Normal);
                                    }
                                }
                                else
                                {
                                    if (!(empireMessage_0.AdvisorMessageData is Habitat))
                                    {
                                        break;
                                    }
                                    Habitat habitat2 = (Habitat)empireMessage_0.AdvisorMessageData;
                                    if (habitat2 == null)
                                    {
                                        break;
                                    }
                                    Design design = _Game.PlayerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                                    if (design == null)
                                    {
                                        design = _Game.PlayerEmpire.Designs.FindNewest(BuiltObjectSubRole.ColonyShip);
                                    }
                                    if (design == null)
                                    {
                                        break;
                                    }
                                    List<HabitatType> list = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpire(_Game.PlayerEmpire);
                                    bool flag;
                                    if (!(flag = _Game.PlayerEmpire.CanDesignColonizeHabitat(design, habitat)) && !list.Contains(habitat.Type))
                                    {
                                        break;
                                    }
                                    double num3 = design.CalculateCurrentPurchasePrice(_Game.Galaxy);
                                    if (!(num3 <= _Game.PlayerEmpire.StateMoney))
                                    {
                                        break;
                                    }
                                    design.BuildCount++;
                                    BuiltObject builtObject2 = new BuiltObject(design, _Game.Galaxy.GenerateBuiltObjectName(design), _Game.Galaxy);
                                    builtObject2.PurchasePrice = num3;
                                    if (habitat2 == null)
                                    {
                                        double shortestWaitQueueTime;
                                        if (flag)
                                        {
                                            habitat2 = _Game.PlayerEmpire.Colonies.FindShortestConstructionWaitQueue(builtObject2, out shortestWaitQueueTime);
                                        }
                                        else
                                        {
                                            HabitatList habitatList = new HabitatList();
                                            foreach (Habitat colony2 in _Game.PlayerEmpire.Colonies)
                                            {
                                                Race dominantRace = colony2.Population.DominantRace;
                                                if (dominantRace.NativeHabitatType == habitat.Type)
                                                {
                                                    habitatList.Add(colony2);
                                                }
                                            }
                                            habitat2 = habitatList.FindShortestConstructionWaitQueue(builtObject2, out shortestWaitQueueTime);
                                        }
                                    }
                                    if (habitat2 != null && habitat2.ConstructionQueue != null && habitat2.ConstructionQueue.AddBuiltObjectToConstruct(builtObject2))
                                    {
                                        builtObject2.Name = _Game.Galaxy.GenerateBuiltObjectName(design, habitat2);
                                        _Game.PlayerEmpire.AddBuiltObjectToGalaxy(builtObject2, habitat2, offsetLocationFromParent: false, isStateOwned: true);
                                        _Game.PlayerEmpire.StateMoney -= num3;
                                        _Game.PlayerEmpire.PirateEconomy.PerformExpense(num3, PirateExpenseType.Construction, _Game.Galaxy.CurrentStarDate);
                                        builtObject2.AssignMission(BuiltObjectMissionType.Colonize, habitat, null, BuiltObjectMissionPriority.Normal);
                                        builtObject2.BuiltAt = habitat2;
                                        CargoList resourcesToOrder = null;
                                        _Game.PlayerEmpire.ProcureConstructionComponents(builtObject2, habitat2, out resourcesToOrder);
                                        foreach (Cargo item2 in resourcesToOrder)
                                        {
                                            _Game.PlayerEmpire.CreateOrder(habitat2, item2.CommodityResource, item2.Amount, isState: false, OrderType.ConstructionShortage);
                                        }
                                    }
                                    else
                                    {
                                        design.BuildCount--;
                                    }
                                }
                                break;
                            }
                            Design design2 = _Game.PlayerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                            if (design2 == null)
                            {
                                design2 = _Game.PlayerEmpire.Designs.FindNewest(BuiltObjectSubRole.ColonyShip);
                            }
                            if (design2 == null)
                            {
                                break;
                            }
                            List<HabitatType> list2 = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpire(_Game.PlayerEmpire);
                            bool flag2;
                            if (!(flag2 = _Game.PlayerEmpire.CanDesignColonizeHabitat(design2, habitat)) && !list2.Contains(habitat.Type))
                            {
                                break;
                            }
                            double num4 = design2.CalculateCurrentPurchasePrice(_Game.Galaxy);
                            if (!(num4 <= _Game.PlayerEmpire.StateMoney))
                            {
                                break;
                            }
                            design2.BuildCount++;
                            BuiltObject builtObject3 = new BuiltObject(design2, _Game.Galaxy.GenerateBuiltObjectName(design2), _Game.Galaxy);
                            builtObject3.PurchasePrice = num4;
                            Habitat habitat3 = null;
                            double shortestWaitQueueTime2;
                            if (flag2)
                            {
                                habitat3 = _Game.PlayerEmpire.Colonies.FindShortestConstructionWaitQueue(builtObject3, out shortestWaitQueueTime2);
                            }
                            else
                            {
                                HabitatList habitatList2 = new HabitatList();
                                foreach (Habitat colony3 in _Game.PlayerEmpire.Colonies)
                                {
                                    Race dominantRace2 = colony3.Population.DominantRace;
                                    if (dominantRace2.NativeHabitatType == habitat.Type)
                                    {
                                        habitatList2.Add(colony3);
                                    }
                                }
                                habitat3 = habitatList2.FindShortestConstructionWaitQueue(builtObject3, out shortestWaitQueueTime2);
                            }
                            if (habitat3 != null && habitat3.ConstructionQueue != null && habitat3.ConstructionQueue.AddBuiltObjectToConstruct(builtObject3))
                            {
                                builtObject3.Name = _Game.Galaxy.GenerateBuiltObjectName(design2, habitat3);
                                _Game.PlayerEmpire.AddBuiltObjectToGalaxy(builtObject3, habitat3, offsetLocationFromParent: false, isStateOwned: true);
                                _Game.PlayerEmpire.StateMoney -= num4;
                                _Game.PlayerEmpire.PirateEconomy.PerformExpense(num4, PirateExpenseType.Construction, _Game.Galaxy.CurrentStarDate);
                                builtObject3.AssignMission(BuiltObjectMissionType.Colonize, habitat, null, BuiltObjectMissionPriority.Normal);
                                builtObject3.BuiltAt = habitat3;
                                CargoList resourcesToOrder2 = null;
                                _Game.PlayerEmpire.ProcureConstructionComponents(builtObject3, habitat3, out resourcesToOrder2);
                                foreach (Cargo item3 in resourcesToOrder2)
                                {
                                    _Game.PlayerEmpire.CreateOrder(habitat3, item3.CommodityResource, item3.Amount, isState: false, OrderType.ConstructionShortage);
                                }
                            }
                            else
                            {
                                design2.BuildCount--;
                            }
                            break;
                        }
                    case AdvisorMessageType.IntelligenceMission:
                        {
                            if (!(empireMessage_0.Subject is IntelligenceMission))
                            {
                                break;
                            }
                            IntelligenceMission intelligenceMission = (IntelligenceMission)empireMessage_0.Subject;
                            if (intelligenceMission != null && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is Character)
                            {
                                Character character = (Character)empireMessage_0.AdvisorMessageData;
                                if (character != null)
                                {
                                    intelligenceMission.ResetStartDate(_Game.Galaxy.CurrentStarDate);
                                    intelligenceMission.Agent = character;
                                    character.Mission = intelligenceMission;
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.EnemyAttack:
                        {
                            if (empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is ShipGroup))
                            {
                                break;
                            }
                            ShipGroup shipGroup6 = (ShipGroup)empireMessage_0.AdvisorMessageData;
                            if (shipGroup6 == null)
                            {
                                break;
                            }
                            StellarObject stellarObject2 = null;
                            long starDate4 = _Game.Galaxy.CurrentStarDate;
                            if (empireMessage_0.AdvisorMessageData2 != null && empireMessage_0.AdvisorMessageData2 is StellarObject)
                            {
                                stellarObject2 = (StellarObject)empireMessage_0.AdvisorMessageData2;
                                int num9 = 2;
                                if (stellarObject2.DockingBays != null)
                                {
                                    num9 = stellarObject2.DockingBays.Count;
                                }
                                long num10 = shipGroup6.Ships.Count * Galaxy.FleetAssembleAttackWaitPeriodPerShip / num9;
                                starDate4 = _Game.PlayerEmpire.DetermineLatestArrivalAtDestination(shipGroup6, stellarObject2) + num10;
                            }
                            if (empireMessage_0.Subject == null)
                            {
                                break;
                            }
                            if (empireMessage_0.Subject is BuiltObject)
                            {
                                BuiltObject target4 = (BuiltObject)empireMessage_0.Subject;
                                if (stellarObject2 != null)
                                {
                                    shipGroup6.ForceCompleteMission();
                                    shipGroup6.AssignMission(BuiltObjectMissionType.WaitAndAttack, target4, stellarObject2, starDate4, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    break;
                                }
                                shipGroup6.ForceCompleteMission();
                                BuiltObjectMissionType missionType = BuiltObjectMissionType.Attack;
                                if (shipGroup6.Empire != null)
                                {
                                    missionType = shipGroup6.Empire.DetermineDestroyOrCaptureTarget(shipGroup6, target4);
                                }
                                shipGroup6.AssignMission(missionType, target4, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            }
                            else if (empireMessage_0.Subject is Habitat)
                            {
                                Habitat target5 = (Habitat)empireMessage_0.Subject;
                                if (stellarObject2 != null)
                                {
                                    shipGroup6.ForceCompleteMission();
                                    shipGroup6.AssignMission(BuiltObjectMissionType.WaitAndAttack, target5, stellarObject2, starDate4, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                                else
                                {
                                    shipGroup6.ForceCompleteMission();
                                    shipGroup6.AssignMission(BuiltObjectMissionType.Attack, target5, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                            }
                            else if (empireMessage_0.Subject is Creature)
                            {
                                Creature target6 = (Creature)empireMessage_0.Subject;
                                if (stellarObject2 != null)
                                {
                                    shipGroup6.ForceCompleteMission();
                                    shipGroup6.AssignMission(BuiltObjectMissionType.WaitAndAttack, target6, stellarObject2, starDate4, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                                else
                                {
                                    shipGroup6.ForceCompleteMission();
                                    shipGroup6.AssignMission(BuiltObjectMissionType.Attack, target6, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                            }
                            else if (empireMessage_0.Subject is ShipGroup)
                            {
                                ShipGroup target7 = (ShipGroup)empireMessage_0.Subject;
                                if (stellarObject2 != null)
                                {
                                    shipGroup6.ForceCompleteMission();
                                    shipGroup6.AssignMission(BuiltObjectMissionType.WaitAndAttack, target7, stellarObject2, starDate4, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                                else
                                {
                                    shipGroup6.ForceCompleteMission();
                                    shipGroup6.AssignMission(BuiltObjectMissionType.Attack, target7, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.EnemyBombard:
                        {
                            if (empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is ShipGroup))
                            {
                                break;
                            }
                            ShipGroup shipGroup2 = (ShipGroup)empireMessage_0.AdvisorMessageData;
                            if (shipGroup2 == null)
                            {
                                break;
                            }
                            StellarObject stellarObject = null;
                            long starDate3 = _Game.Galaxy.CurrentStarDate;
                            if (empireMessage_0.AdvisorMessageData2 != null && empireMessage_0.AdvisorMessageData2 is StellarObject)
                            {
                                stellarObject = (StellarObject)empireMessage_0.AdvisorMessageData2;
                                int num = 2;
                                if (stellarObject.DockingBays != null)
                                {
                                    num = stellarObject.DockingBays.Count;
                                }
                                long num2 = shipGroup2.Ships.Count * Galaxy.FleetAssembleAttackWaitPeriodPerShip / num;
                                starDate3 = _Game.PlayerEmpire.DetermineLatestArrivalAtDestination(shipGroup2, stellarObject) + num2;
                            }
                            if (empireMessage_0.Subject != null && empireMessage_0.Subject is Habitat)
                            {
                                Habitat target3 = (Habitat)empireMessage_0.Subject;
                                if (stellarObject != null)
                                {
                                    shipGroup2.ForceCompleteMission();
                                    shipGroup2.AssignMission(BuiltObjectMissionType.WaitAndBombard, target3, stellarObject, starDate3, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                                else
                                {
                                    shipGroup2.ForceCompleteMission();
                                    shipGroup2.AssignMission(BuiltObjectMissionType.Bombard, target3, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.EnemyBlockade:
                        {
                            if (empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is ShipGroup))
                            {
                                break;
                            }
                            ShipGroup shipGroup3 = (ShipGroup)empireMessage_0.AdvisorMessageData;
                            if (shipGroup3 != null && empireMessage_0.Subject != null)
                            {
                                int refusalCount = 0;
                                if (empireMessage_0.Subject is BuiltObject)
                                {
                                    BuiltObject builtObject4 = (BuiltObject)empireMessage_0.Subject;
                                    _Game.PlayerEmpire.ImplementBlockade(builtObject4, sendFleet: true, performAuthorizationCheck: false, ref refusalCount, shipGroup3);
                                }
                                else if (empireMessage_0.Subject is Habitat)
                                {
                                    Habitat colony = (Habitat)empireMessage_0.Subject;
                                    _Game.PlayerEmpire.ImplementBlockade(colony, sendFleet: true, performAuthorizationCheck: false, ref refusalCount, shipGroup3);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.EnemyAttackPlanetDestroyer:
                        {
                            if (empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is BuiltObject))
                            {
                                break;
                            }
                            BuiltObject builtObject5 = (BuiltObject)empireMessage_0.AdvisorMessageData;
                            if (builtObject5 != null && empireMessage_0.Subject is Habitat)
                            {
                                Habitat habitat4 = (Habitat)empireMessage_0.Subject;
                                if (!habitat4.HasBeenDestroyed && habitat4.Empire != _Game.PlayerEmpire)
                                {
                                    builtObject5.AssignMission(BuiltObjectMissionType.Attack, habitat4, null, BuiltObjectMissionPriority.Normal);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.InvadeIndependent:
                        {
                            if (!(empireMessage_0.Subject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat7 = (Habitat)empireMessage_0.Subject;
                            if (habitat7 != null && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is ShipGroup)
                            {
                                ShipGroup shipGroup4 = (ShipGroup)empireMessage_0.AdvisorMessageData;
                                if (shipGroup4 != null && shipGroup4.TotalTroopAttackStrength > 0)
                                {
                                    shipGroup4.AssignMission(BuiltObjectMissionType.Attack, habitat7, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.PrepareRaid:
                        if (empireMessage_0.Subject is Empire)
                        {
                            Empire empire15 = (Empire)empireMessage_0.Subject;
                            if (empire15 != null && empire15 != _Game.PlayerEmpire && empire15.Active)
                            {
                                _Game.PlayerEmpire.EmpiresToAttack.Add(empire15);
                            }
                        }
                        break;
                    case AdvisorMessageType.DiplomaticGift:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire10 = (Empire)empireMessage_0.Subject;
                            if (empire10 != null)
                            {
                                double num6 = empireMessage_0.Money;
                                if (_Game.PlayerEmpire.StateMoney >= num6)
                                {
                                    EmpireMessage empireMessage2 = new EmpireMessage(_Game.PlayerEmpire, EmpireMessageType.GiveGift, null);
                                    empireMessage2.Money = (int)num6;
                                    _Game.PlayerEmpire.StateMoney -= num6;
                                    _Game.PlayerEmpire.PirateEconomy.PerformExpense(num6, PirateExpenseType.Undefined, _Game.Galaxy.CurrentStarDate);
                                    empireMessage2.Description = string.Format(TextResolver.GetText("Please accept our gift of X credits"), num6.ToString("###,###,###,##0"));
                                    _Game.PlayerEmpire.SendMessageToEmpire(empireMessage2, empire10);
                                    _Game.PlayerEmpire.ObtainDiplomaticRelation(empire10);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.TreatyOffer:
                    case AdvisorMessageType.WarTradeSanctions:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire6 = (Empire)empireMessage_0.Subject;
                            if (empire6 == null || empire6 == _Game.PlayerEmpire || !empire6.Active)
                            {
                                break;
                            }
                            if (empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is DiplomaticRelationType)
                            {
                                DiplomaticRelationType diplomaticRelationType = (DiplomaticRelationType)empireMessage_0.AdvisorMessageData;
                                DiplomaticRelation diplomaticRelation4 = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire6);
                                bool flag3 = false;
                                string description3 = string.Empty;
                                string messageHint = string.Empty;
                                switch (diplomaticRelationType)
                                {
                                    case DiplomaticRelationType.None:
                                        if (diplomaticRelation4.Type == DiplomaticRelationType.War)
                                        {
                                            flag3 = false;
                                            description3 = _Game.PlayerEmpire.GenerateMessageDescriptionEndWarRequest();
                                            messageHint = Galaxy.ResolveDescription(DiplomaticRelationType.War);
                                        }
                                        else if (diplomaticRelation4.Type == DiplomaticRelationType.TradeSanctions)
                                        {
                                            flag3 = ((diplomaticRelation4.Initiator == _Game.PlayerEmpire) ? true : false);
                                        }
                                        else if (diplomaticRelation4.Type == DiplomaticRelationType.SubjugatedDominion)
                                        {
                                            if (diplomaticRelation4.Initiator == _Game.PlayerEmpire)
                                            {
                                                flag3 = true;
                                                description3 = _Game.PlayerEmpire.GenerateMessageDescriptionEndSubjugation();
                                                messageHint = Galaxy.ResolveDescription(DiplomaticRelationType.SubjugatedDominion);
                                            }
                                            else
                                            {
                                                flag3 = false;
                                            }
                                        }
                                        else
                                        {
                                            flag3 = true;
                                            description3 = TextResolver.GetText("We cancel our treaty with you.");
                                            messageHint = Galaxy.ResolveDescription(diplomaticRelation4.Type);
                                        }
                                        break;
                                    case DiplomaticRelationType.SubjugatedDominion:
                                        flag3 = false;
                                        break;
                                    case DiplomaticRelationType.FreeTradeAgreement:
                                    case DiplomaticRelationType.MutualDefensePact:
                                    case DiplomaticRelationType.Protectorate:
                                        flag3 = false;
                                        break;
                                    case DiplomaticRelationType.TradeSanctions:
                                    case DiplomaticRelationType.War:
                                        flag3 = true;
                                        break;
                                }
                                if (flag3)
                                {
                                    switch (diplomaticRelationType)
                                    {
                                        default:
                                            _Game.PlayerEmpire.ChangeDiplomaticRelation(diplomaticRelation4, diplomaticRelationType);
                                            diplomaticRelation4.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                                            _Game.PlayerEmpire.SendMessageToEmpire(empire6, EmpireMessageType.DiplomaticRelationChange, diplomaticRelationType, description3, messageHint);
                                            diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire6);
                                            break;
                                        case DiplomaticRelationType.TradeSanctions:
                                            {
                                                EmpireEvaluation empireEvaluation = empire6.ObtainEmpireEvaluation(_Game.PlayerEmpire);
                                                _Game.PlayerEmpire.ChangeDiplomaticRelation(diplomaticRelation4, DiplomaticRelationType.TradeSanctions);
                                                empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 20.0;
                                                diplomaticRelation4.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                                                int ourPotencyVersusThem2 = _Game.PlayerEmpire.DetermineRelativeStrength(_Game.PlayerEmpire.MilitaryPotency, empire6);
                                                string description5 = _Game.PlayerEmpire.GenerateMessageDescription(diplomaticRelation4, DiplomaticRelationType.TradeSanctions, ourPotencyVersusThem2);
                                                _Game.PlayerEmpire.SendMessageToEmpire(empire6, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.TradeSanctions, description5);
                                                diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire6);
                                                break;
                                            }
                                        case DiplomaticRelationType.War:
                                            _Game.PlayerEmpire.DeclareWar(empire6);
                                            diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire6);
                                            break;
                                        case DiplomaticRelationType.None:
                                            if (diplomaticRelation4.Type == DiplomaticRelationType.TradeSanctions)
                                            {
                                                _Game.PlayerEmpire.ChangeDiplomaticRelation(diplomaticRelation4, DiplomaticRelationType.None);
                                                _Game.PlayerEmpire.CancelBlockades(empire6);
                                                empire6.CancelBlockades(_Game.PlayerEmpire);
                                                diplomaticRelation4.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                                                string description4 = _Game.PlayerEmpire.GenerateMessageDescriptionLiftTradeSanctions();
                                                _Game.PlayerEmpire.SendMessageToEmpire(empire6, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, description4, Galaxy.ResolveDescription(DiplomaticRelationType.TradeSanctions));
                                                diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire6);
                                            }
                                            else
                                            {
                                                _Game.PlayerEmpire.ChangeDiplomaticRelation(diplomaticRelation4, diplomaticRelationType);
                                                diplomaticRelation4.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                                                _Game.PlayerEmpire.SendMessageToEmpire(empire6, EmpireMessageType.DiplomaticRelationChange, diplomaticRelationType, description3, messageHint);
                                                diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire6);
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    DiplomaticRelation diplomaticRelation5 = new DiplomaticRelation(diplomaticRelationType, _Game.PlayerEmpire, _Game.PlayerEmpire, empire6, _Game.Galaxy.CurrentStarDate, diplomaticRelation4.SupplyRestrictedResources);
                                    int ourPotencyVersusThem3 = _Game.PlayerEmpire.DetermineRelativeStrength(_Game.PlayerEmpire.MilitaryPotency, empire6);
                                    empire6.ProposedDiplomaticRelations.Add(diplomaticRelation5);
                                    string description6 = _Game.PlayerEmpire.GenerateMessageDescription(diplomaticRelation4, diplomaticRelationType, ourPotencyVersusThem3);
                                    diplomaticRelation4.LastDiplomacyTradeOfferDate = _Game.Galaxy.CurrentStarDate;
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire6, EmpireMessageType.ProposeDiplomaticRelation, diplomaticRelationType, description6);
                                }
                            }
                            else
                            {
                                if (empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is PirateRelationType) || empire6 == null)
                                {
                                    break;
                                }
                                PirateRelationType pirateRelationType = (PirateRelationType)empireMessage_0.AdvisorMessageData;
                                PirateRelation pirateRelation = _Game.PlayerEmpire.ObtainPirateRelation(empire6);
                                string empty = string.Empty;
                                switch (pirateRelationType)
                                {
                                    case PirateRelationType.None:
                                        _Game.PlayerEmpire.ChangePirateRelation(empire6, PirateRelationType.None, _Game.Galaxy.CurrentStarDate);
                                        empty = TextResolver.GetText("Cancel Pirate Protection");
                                        _Game.PlayerEmpire.SendMessageToEmpire(empire6, EmpireMessageType.CancelPirateProtection, _Game.PlayerEmpire, empty);
                                        break;
                                    case PirateRelationType.Protection:
                                        {
                                            empty = TextResolver.GetText("Pirate Offer Protection");
                                            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && empire6.PirateEmpireBaseHabitat != null)
                                            {
                                                empty = TextResolver.GetText("Pirate Offer Protection Other Pirate");
                                            }
                                            EmpireMessage empireMessage = new EmpireMessage(_Game.PlayerEmpire, EmpireMessageType.PirateOfferProtection, null);
                                            empireMessage.Description = empty;
                                            if (empire6.PirateEmpireBaseHabitat == null)
                                            {
                                                empireMessage.Money = (int)_Game.PlayerEmpire.CalculatePirateProtectionPricePerMonth(empire6);
                                            }
                                            _Game.PlayerEmpire.SendMessageToEmpire(empireMessage, empire6);
                                            pirateRelation.LastOfferDate = _Game.Galaxy.CurrentStarDate;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.ColonyFacility:
                        {
                            if (!(empireMessage_0.Subject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat8 = (Habitat)empireMessage_0.Subject;
                            if (habitat8 == null || empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is PlanetaryFacilityDefinition))
                            {
                                break;
                            }
                            PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)empireMessage_0.AdvisorMessageData;
                            if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.Undefined)
                            {
                                break;
                            }
                            double num8 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, _Game.PlayerEmpire);
                            if (!(_Game.PlayerEmpire.StateMoney >= num8))
                            {
                                break;
                            }
                            if (planetaryFacilityDefinition.Type == PlanetaryFacilityType.Wonder)
                            {
                                if (habitat8.Facilities.CountWonderByType(planetaryFacilityDefinition.WonderType) <= 0 && habitat8.QueueWonderConstruction(planetaryFacilityDefinition))
                                {
                                    _Game.PlayerEmpire.StateMoney -= num8;
                                    _Game.PlayerEmpire.PirateEconomy.PerformExpense(num8, PirateExpenseType.FacilityConstruction, _Game.Galaxy.CurrentStarDate);
                                }
                            }
                            else
                            {
                                if (habitat8.Facilities.CountByType(planetaryFacilityDefinition.Type) > 0)
                                {
                                    break;
                                }
                                bool flag4 = true;
                                PirateColonyControl pirateColonyControl = null;
                                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                                {
                                    pirateColonyControl = habitat8.GetPirateControl().GetByFaction(_Game.PlayerEmpire);
                                    if (pirateColonyControl != null)
                                    {
                                        switch (planetaryFacilityDefinition.Type)
                                        {
                                            case PlanetaryFacilityType.PirateCriminalNetwork:
                                                flag4 = false;
                                                if (pirateColonyControl.ControlLevel >= 1f && habitat8.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateFortress) > 0)
                                                {
                                                    flag4 = true;
                                                }
                                                break;
                                            case PlanetaryFacilityType.PirateBase:
                                                flag4 = false;
                                                if (pirateColonyControl.ControlLevel >= 0.5f)
                                                {
                                                    flag4 = true;
                                                }
                                                break;
                                            case PlanetaryFacilityType.PirateFortress:
                                                flag4 = false;
                                                if (pirateColonyControl.ControlLevel >= 1f && habitat8.Facilities != null && habitat8.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateBase) > 0)
                                                {
                                                    flag4 = true;
                                                }
                                                break;
                                        }
                                    }
                                }
                                if (flag4 && habitat8.QueueFacilityConstruction(planetaryFacilityDefinition.Type))
                                {
                                    _Game.PlayerEmpire.StateMoney -= num8;
                                    _Game.PlayerEmpire.PirateEconomy.PerformExpense(num8, PirateExpenseType.FacilityConstruction, _Game.Galaxy.CurrentStarDate);
                                    if (pirateColonyControl != null)
                                    {
                                        pirateColonyControl.HasFacilityControl = true;
                                    }
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.OfferMilitaryRefueling:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire3 = (Empire)empireMessage_0.Subject;
                            if (empire3 != null && empire3 != _Game.PlayerEmpire && empire3.Active)
                            {
                                DiplomaticRelation diplomaticRelation = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire3);
                                if (diplomaticRelation.Type != 0 && diplomaticRelation.Type != DiplomaticRelationType.War)
                                {
                                    _Game.PlayerEmpire.OfferMilitaryRefueling(empire3);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.CancelMilitaryRefueling:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire17 = (Empire)empireMessage_0.Subject;
                            if (empire17 != null && empire17 != _Game.PlayerEmpire && empire17.Active)
                            {
                                DiplomaticRelation diplomaticRelation8 = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire17);
                                if (diplomaticRelation8.MilitaryRefuelingToOther)
                                {
                                    diplomaticRelation8.MilitaryRefuelingToOther = false;
                                    string description10 = TextResolver.GetText("Military Refueling Blocked");
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire17, EmpireMessageType.MilitaryRefuelingBlocked, _Game.PlayerEmpire, description10);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.OfferMiningRights:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire4 = (Empire)empireMessage_0.Subject;
                            if (empire4 != null && empire4 != _Game.PlayerEmpire && empire4.Active)
                            {
                                DiplomaticRelation diplomaticRelation2 = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire4);
                                if (diplomaticRelation2.Type != 0 && diplomaticRelation2.Type != DiplomaticRelationType.War)
                                {
                                    _Game.PlayerEmpire.OfferMiningRights(empire4);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.CancelMiningRights:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire16 = (Empire)empireMessage_0.Subject;
                            if (empire16 != null && empire16 != _Game.PlayerEmpire && empire16.Active)
                            {
                                DiplomaticRelation diplomaticRelation7 = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire16);
                                if (diplomaticRelation7.MiningRightsToOther)
                                {
                                    diplomaticRelation7.MiningRightsToOther = false;
                                    string description9 = TextResolver.GetText("Mining Rights Blocked");
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire16, EmpireMessageType.MiningRightsBlocked, _Game.PlayerEmpire, description9);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.AllowTradeRestrictedResources:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire9 = (Empire)empireMessage_0.Subject;
                            if (empire9 != null && empire9 != _Game.PlayerEmpire && empire9.Active)
                            {
                                DiplomaticRelation diplomaticRelation6 = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire9);
                                if (!diplomaticRelation6.SupplyRestrictedResources)
                                {
                                    diplomaticRelation6.SupplyRestrictedResources = true;
                                    string description7 = string.Format(TextResolver.GetText("Trade Restricted Resource EMPIRE"), _Game.PlayerEmpire.Name);
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire9, EmpireMessageType.RestrictedResourceTradingAllowed, _Game.PlayerEmpire, description7);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.DisallowTradeRestrictedResources:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire5 = (Empire)empireMessage_0.Subject;
                            if (empire5 != null && empire5 != _Game.PlayerEmpire && empire5.Active)
                            {
                                DiplomaticRelation diplomaticRelation3 = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire5);
                                if (diplomaticRelation3.SupplyRestrictedResources)
                                {
                                    diplomaticRelation3.SupplyRestrictedResources = false;
                                    string description2 = string.Format(TextResolver.GetText("Trade Restricted Resource Refuse EMPIRE"), _Game.PlayerEmpire.Name);
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire5, EmpireMessageType.RestrictedResourceTradingBlocked, _Game.PlayerEmpire, description2);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.ComplyTradeSanctionsOther:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire13 = (Empire)empireMessage_0.Subject;
                            if (empire13 != null && empire13 != _Game.PlayerEmpire && empire13.Active && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is Empire)
                            {
                                Empire empire14 = (Empire)empireMessage_0.AdvisorMessageData;
                                if (empire14 != null)
                                {
                                    DiplomaticRelation currentDiplomaticRelation = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire13);
                                    _Game.PlayerEmpire.ChangeDiplomaticRelation(currentDiplomaticRelation, DiplomaticRelationType.TradeSanctions);
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire13, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.TradeSanctions, TextResolver.GetText("We terminate all trade with you effective immediately!"));
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire14, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We join you in trade embargo against the EMPIRE"), empire13.Name));
                                    EmpireEvaluation empireEvaluation3 = empire14.ObtainEmpireEvaluation(_Game.PlayerEmpire);
                                    empireEvaluation3.IncidentEvaluation = empireEvaluation3.IncidentEvaluationRaw + 5.0;
                                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire13);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.ComplyWarOther:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire7 = (Empire)empireMessage_0.Subject;
                            if (empire7 != null && empire7 != _Game.PlayerEmpire && empire7.Active && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is Empire)
                            {
                                Empire empire8 = (Empire)empireMessage_0.AdvisorMessageData;
                                if (empire8 != null)
                                {
                                    _Game.PlayerEmpire.DeclareWar(empire7);
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire8, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We join you in battle against the EMPIRE"), empire7.Name));
                                    EmpireEvaluation empireEvaluation2 = empire8.ObtainEmpireEvaluation(_Game.PlayerEmpire);
                                    empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw + 10.0;
                                    diplomaticMessageQueue_0.ExpireDiplomacyMessagesForEmpire(empire7);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.DefendTerritory:
                        if (empireMessage_0.Subject is ShipGroup)
                        {
                            ShipGroup shipGroup5 = (ShipGroup)empireMessage_0.Subject;
                            if (shipGroup5 != null && shipGroup5.Empire != _Game.PlayerEmpire && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is ShipGroup)
                            {
                                ((ShipGroup)empireMessage_0.AdvisorMessageData)?.AssignMission(BuiltObjectMissionType.Attack, shipGroup5, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            }
                        }
                        else
                        {
                            if (!(empireMessage_0.Subject is BuiltObject))
                            {
                                break;
                            }
                            BuiltObject builtObject7 = (BuiltObject)empireMessage_0.Subject;
                            if (builtObject7 == null || builtObject7.Empire == _Game.PlayerEmpire || empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is BuiltObject))
                            {
                                break;
                            }
                            BuiltObject builtObject8 = (BuiltObject)empireMessage_0.AdvisorMessageData;
                            if (builtObject8 != null)
                            {
                                BuiltObjectMissionType builtObjectMissionType = BuiltObjectMissionType.Attack;
                                if (builtObject8.Empire != null)
                                {
                                    builtObjectMissionType = builtObject8.Empire.DetermineDestroyOrCaptureTarget(builtObject8, builtObject7, attackingAsGroup: false);
                                }
                                builtObject8.RecordRevertMission(builtObjectMissionType, evenWhenAutomated: true);
                                builtObject8.AssignMission(builtObjectMissionType, builtObject7, null, BuiltObjectMissionPriority.High);
                            }
                        }
                        break;
                    case AdvisorMessageType.Retrofit:
                        {
                            BuiltObjectList builtObjectList = new BuiltObjectList();
                            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
                            if (empireMessage_0.Subject != null && empireMessage_0.Subject is BuiltObjectList)
                            {
                                BuiltObjectList items = (BuiltObjectList)empireMessage_0.Subject;
                                builtObjectList.AddRange(items);
                            }
                            else
                            {
                                builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
                            }
                            long num5 = Galaxy.RealSecondsInGalacticYear * 1000 * Galaxy.RetrofitYears;
                            long val = num5;
                            val = Math.Max(val, Galaxy.RealSecondsInGalacticYear * 2000);
                            val = 0L;
                            _Game.PlayerEmpire.DoRetrofit(builtObjectList, _Game.Galaxy.CurrentStarDate, 0L, 0L, stateRetrofitPermitted: true, breakthroughInitiated: true, manuallyInitiated: true);
                            break;
                        }
                    case AdvisorMessageType.RequestLiftTradeSanctionsOther:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire = (Empire)empireMessage_0.Subject;
                            if (empire != null && empire != _Game.PlayerEmpire && empire.Active && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is Empire)
                            {
                                Empire empire2 = (Empire)empireMessage_0.AdvisorMessageData;
                                if (empire2 != null)
                                {
                                    int weightedMilitaryPotency = _Game.PlayerEmpire.WeightedMilitaryPotency;
                                    int weightedMilitaryPotency2 = empire.WeightedMilitaryPotency;
                                    int ourPotencyVersusThem = _Game.PlayerEmpire.EvaluateMilitaryPotency(weightedMilitaryPotency, weightedMilitaryPotency2, empire);
                                    string description = _Game.PlayerEmpire.GenerateMessageDescription(EmpireMessageType.RequestLiftTradeSanctions, ourPotencyVersusThem, empire2);
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire, EmpireMessageType.RequestLiftTradeSanctions, empire2, description);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.RequestEndWarOther:
                        {
                            if (!(empireMessage_0.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire11 = (Empire)empireMessage_0.Subject;
                            if (empire11 != null && empire11 != _Game.PlayerEmpire && empire11.Active && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is Empire)
                            {
                                Empire empire12 = (Empire)empireMessage_0.AdvisorMessageData;
                                if (empire12 != null)
                                {
                                    int weightedMilitaryPotency3 = _Game.PlayerEmpire.WeightedMilitaryPotency;
                                    int weightedMilitaryPotency4 = empire11.WeightedMilitaryPotency;
                                    int ourPotencyVersusThem4 = _Game.PlayerEmpire.EvaluateMilitaryPotency(weightedMilitaryPotency3, weightedMilitaryPotency4, empire11);
                                    string description8 = _Game.PlayerEmpire.GenerateMessageDescription(EmpireMessageType.RequestStopWar, ourPotencyVersusThem4, empire12);
                                    _Game.PlayerEmpire.SendMessageToEmpire(empire11, EmpireMessageType.RequestStopWar, empire12, description8);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.OfferPirateAttackMission:
                        if (empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is EmpireActivity)
                        {
                            EmpireActivity empireActivity4 = (EmpireActivity)empireMessage_0.AdvisorMessageData;
                            if (empireActivity4 != null && empireActivity4.Type == EmpireActivityType.Attack && !_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity4.Target, empireActivity4.Type))
                            {
                                _Game.PlayerEmpire.PirateMissions.Add(empireActivity4);
                                _Game.Galaxy.PirateMissions.Add(empireActivity4);
                            }
                        }
                        break;
                    case AdvisorMessageType.OfferPirateDefendMission:
                        if (empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is EmpireActivity)
                        {
                            EmpireActivity empireActivity2 = (EmpireActivity)empireMessage_0.AdvisorMessageData;
                            if (empireActivity2 != null && empireActivity2.Type == EmpireActivityType.Defend && !_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity2.Target, empireActivity2.Type))
                            {
                                _Game.PlayerEmpire.PirateMissions.Add(empireActivity2);
                                _Game.Galaxy.PirateMissions.Add(empireActivity2);
                            }
                        }
                        break;
                    case AdvisorMessageType.OfferPirateSmuggleMission:
                        if (empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is EmpireActivity)
                        {
                            EmpireActivity empireActivity = (EmpireActivity)empireMessage_0.AdvisorMessageData;
                            if (empireActivity != null && empireActivity.Type == EmpireActivityType.Smuggle && !_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity.Target, empireActivity.Type))
                            {
                                _Game.PlayerEmpire.PirateMissions.Add(empireActivity);
                                _Game.Galaxy.PirateMissions.Add(empireActivity);
                            }
                        }
                        break;
                    case AdvisorMessageType.PirateRaid:
                        {
                            if (empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is ShipGroup))
                            {
                                break;
                            }
                            ShipGroup shipGroup7 = (ShipGroup)empireMessage_0.AdvisorMessageData;
                            if (shipGroup7 != null && empireMessage_0.Subject != null)
                            {
                                if (empireMessage_0.Subject is BuiltObject)
                                {
                                    BuiltObject target8 = (BuiltObject)empireMessage_0.Subject;
                                    shipGroup7.ForceCompleteMission();
                                    shipGroup7.AssignMission(BuiltObjectMissionType.Raid, target8, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                                else if (empireMessage_0.Subject is Habitat)
                                {
                                    Habitat target9 = (Habitat)empireMessage_0.Subject;
                                    shipGroup7.ForceCompleteMission();
                                    shipGroup7.AssignMission(BuiltObjectMissionType.Raid, target9, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.PirateFacilityEradicate:
                        {
                            if (empireMessage_0.Subject == null || !(empireMessage_0.Subject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat5 = (Habitat)empireMessage_0.Subject;
                            if (habitat5 != null && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is PlanetaryFacility)
                            {
                                PlanetaryFacility planetaryFacility = (PlanetaryFacility)empireMessage_0.AdvisorMessageData;
                                if (planetaryFacility != null && habitat5.CheckCanInitiateAttackAgainstPirateFacilities(_Game.PlayerEmpire, planetaryFacility))
                                {
                                    habitat5.InitiateAttackAgainstPirateFacilities(_Game.PlayerEmpire, planetaryFacility);
                                }
                            }
                            break;
                        }
                    case AdvisorMessageType.AcceptPirateSmugglingMission:
                        if (empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is EmpireActivity)
                        {
                            EmpireActivity empireActivity3 = (EmpireActivity)empireMessage_0.AdvisorMessageData;
                            if (empireActivity3 != null && empireActivity3.Type == EmpireActivityType.Smuggle && empireActivity3.RequestingEmpire != _Game.PlayerEmpire)
                            {
                                _Game.PlayerEmpire.PirateMissions.Add(empireActivity3);
                            }
                        }
                        break;
                    case AdvisorMessageType.DefendTarget:
                        {
                            if (empireMessage_0.AdvisorMessageData == null || !(empireMessage_0.AdvisorMessageData is ShipGroup))
                            {
                                break;
                            }
                            ShipGroup shipGroup = (ShipGroup)empireMessage_0.AdvisorMessageData;
                            if (shipGroup == null || empireMessage_0.Subject == null)
                            {
                                break;
                            }
                            if (empireMessage_0.Subject is BuiltObject)
                            {
                                BuiltObject target = (BuiltObject)empireMessage_0.Subject;
                                EmpireActivity firstByTargetAndType = _Game.PlayerEmpire.PirateMissions.GetFirstByTargetAndType(target, EmpireActivityType.Defend);
                                long starDate = _Game.Galaxy.CurrentStarDate;
                                if (firstByTargetAndType != null)
                                {
                                    starDate = firstByTargetAndType.ExpiryDate;
                                }
                                shipGroup.ForceCompleteMission();
                                shipGroup.AssignMission(BuiltObjectMissionType.MoveAndWait, target, null, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            }
                            else if (empireMessage_0.Subject is Habitat)
                            {
                                Habitat target2 = (Habitat)empireMessage_0.Subject;
                                EmpireActivity firstByTargetAndType2 = _Game.PlayerEmpire.PirateMissions.GetFirstByTargetAndType(target2, EmpireActivityType.Defend);
                                long starDate2 = _Game.Galaxy.CurrentStarDate;
                                if (firstByTargetAndType2 != null)
                                {
                                    starDate2 = firstByTargetAndType2.ExpiryDate;
                                }
                                shipGroup.ForceCompleteMission();
                                shipGroup.AssignMission(BuiltObjectMissionType.MoveAndWait, target2, null, starDate2, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            }
                            break;
                        }
                }
                diplomaticMessageQueue_0.RemoveMessage(empireMessage_0);
            }
            method_644();
            method_660();
            empireMessage_0 = null;
        }

        private void method_644()
        {
            if (bool_27)
            {
                method_194();
            }
            if (double_6 > 0.0)
            {
                method_4(double_6);
            }
            if (int_62 >= 0 && int_63 >= 0)
            {
                method_156(int_62, int_63);
            }
            if (object_6 != null)
            {
                method_208(object_6);
                pnlDetailInfo.Invalidate();
                picSystem.Invalidate();
                UhvLmNjli7 = UeqnAxbxfb;
            }
            mainView.Invalidate();
            double_6 = -1.0;
            int_62 = -1;
            int_63 = -1;
            object_6 = null;
            bool_27 = false;
        }

        private void method_645(EmpireMessage empireMessage_1)
        {
            if (empireMessage_1 != null)
            {
                Empire empire_ = null;
                if (empireMessage_1.Subject is Empire)
                {
                    empire_ = (Empire)empireMessage_1.Subject;
                }
                else if (empireMessage_1.Subject is IntelligenceMission)
                {
                    IntelligenceMission intelligenceMission = (IntelligenceMission)empireMessage_1.Subject;
                    empire_ = intelligenceMission.TargetEmpire;
                }
                else if (empireMessage_1.AdvisorMessageData != null && empireMessage_1.AdvisorMessageData is Empire)
                {
                    empire_ = (Empire)empireMessage_1.AdvisorMessageData;
                }
                method_195(empire_);
                pnlAdvisorSuggestion.BringToFront();
                bool_27 = true;
            }
        }

        private void method_646(EmpireMessage empireMessage_1)
        {
            if (empireMessage_1 != null)
            {
                Habitat habitat = null;
                if (empireMessage_1.Subject is Habitat)
                {
                    habitat = (Habitat)empireMessage_1.Subject;
                }
                else if (empireMessage_1.AdvisorMessageData != null && empireMessage_1.AdvisorMessageData is Habitat)
                {
                    habitat = (Habitat)empireMessage_1.AdvisorMessageData;
                }
                if (habitat != null)
                {
                    int_62 = int_13;
                    int_63 = int_14;
                    double_6 = double_0;
                    object_6 = _Game.SelectedObject;
                    UeqnAxbxfb = UhvLmNjli7;
                    method_4(3500.0);
                    method_156(habitat.Xpos, habitat.Ypos);
                    method_208(habitat);
                    pnlDetailInfo.Invalidate();
                    picSystem.Invalidate();
                    mainView.Invalidate();
                }
            }
        }

        private void method_647(EmpireMessage empireMessage_1)
        {
            if (empireMessage_1 == null)
            {
                return;
            }
            ShipGroup shipGroup = null;
            BuiltObject builtObject = null;
            Habitat habitat = null;
            Creature creature = null;
            object obj = null;
            double double_ = int_13;
            double double_2 = int_14;
            double double_3 = 3500.0;
            if (empireMessage_1.Subject is BuiltObject)
            {
                builtObject = (BuiltObject)empireMessage_1.Subject;
                obj = builtObject;
                double_ = builtObject.Xpos;
                double_2 = builtObject.Ypos;
            }
            if (empireMessage_1.Subject is Habitat)
            {
                habitat = (Habitat)empireMessage_1.Subject;
                obj = habitat;
                double_ = habitat.Xpos;
                double_2 = habitat.Ypos;
            }
            if (empireMessage_1.Subject is Creature)
            {
                creature = (Creature)empireMessage_1.Subject;
                obj = creature;
                double_ = creature.Xpos;
                double_2 = creature.Ypos;
            }
            if (empireMessage_1.Subject is ShipGroup)
            {
                shipGroup = (ShipGroup)empireMessage_1.Subject;
                obj = shipGroup;
                if (shipGroup.LeadShip != null)
                {
                    double_ = shipGroup.LeadShip.Xpos;
                    double_2 = shipGroup.LeadShip.Ypos;
                }
            }
            if (empireMessage_1.Subject is EmpireActivity)
            {
                EmpireActivity empireActivity = (EmpireActivity)empireMessage_1.Subject;
                if (empireActivity != null && empireActivity.Target != null)
                {
                    if (empireActivity.Target is Habitat)
                    {
                        habitat = (Habitat)empireActivity.Target;
                        double_ = habitat.Xpos;
                        double_2 = habitat.Ypos;
                        obj = habitat;
                    }
                    else if (empireActivity.Target is BuiltObject)
                    {
                        builtObject = (BuiltObject)empireActivity.Target;
                        double_ = builtObject.Xpos;
                        double_2 = builtObject.Ypos;
                        obj = builtObject;
                    }
                    else if (empireActivity.Target is Creature)
                    {
                        creature = (Creature)empireActivity.Target;
                        double_ = creature.Xpos;
                        double_2 = creature.Ypos;
                        obj = creature;
                    }
                }
            }
            if (obj != null)
            {
                int_62 = int_13;
                int_63 = int_14;
                double_6 = double_0;
                object_6 = _Game.SelectedObject;
                UeqnAxbxfb = UhvLmNjli7;
                method_4(double_3);
                method_156(double_, double_2);
                method_208(obj);
                pnlDetailInfo.Invalidate();
                picSystem.Invalidate();
                mainView.Invalidate();
            }
        }

        private void btnAdvisorSuggestionShow_Click(object sender, EventArgs e)
        {
            if (empireMessage_0 != null && empireMessage_0.MessageType == EmpireMessageType.AdvisorSuggestion)
            {
                switch (empireMessage_0.AdvisorMessageType)
                {
                    case AdvisorMessageType.BuildOneOff:
                        method_646(empireMessage_0);
                        break;
                    case AdvisorMessageType.Colonization:
                        method_646(empireMessage_0);
                        break;
                    case AdvisorMessageType.IntelligenceMission:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.EnemyAttack:
                        method_647(empireMessage_0);
                        break;
                    case AdvisorMessageType.EnemyBombard:
                        method_646(empireMessage_0);
                        break;
                    case AdvisorMessageType.EnemyBlockade:
                        method_647(empireMessage_0);
                        break;
                    case AdvisorMessageType.EnemyAttackPlanetDestroyer:
                        method_646(empireMessage_0);
                        break;
                    case AdvisorMessageType.InvadeIndependent:
                        method_646(empireMessage_0);
                        break;
                    case AdvisorMessageType.PrepareRaid:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.DiplomaticGift:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.TreatyOffer:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.WarTradeSanctions:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.ColonyFacility:
                        method_646(empireMessage_0);
                        break;
                    case AdvisorMessageType.OfferMilitaryRefueling:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.CancelMilitaryRefueling:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.OfferMiningRights:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.CancelMiningRights:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.AllowTradeRestrictedResources:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.DisallowTradeRestrictedResources:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.ComplyTradeSanctionsOther:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.ComplyWarOther:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.DefendTerritory:
                        method_647(empireMessage_0);
                        break;
                    case AdvisorMessageType.RequestLiftTradeSanctionsOther:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.RequestEndWarOther:
                        method_645(empireMessage_0);
                        break;
                    case AdvisorMessageType.PirateRaid:
                        method_647(empireMessage_0);
                        break;
                    case AdvisorMessageType.PirateFacilityEradicate:
                        method_647(empireMessage_0);
                        break;
                    case AdvisorMessageType.OfferPirateAttackMission:
                    case AdvisorMessageType.OfferPirateDefendMission:
                    case AdvisorMessageType.OfferPirateSmuggleMission:
                    case AdvisorMessageType.AcceptPirateSmugglingMission:
                    case AdvisorMessageType.DefendTarget:
                        method_647(empireMessage_0);
                        break;
                }
                btnAdvisorSuggestionShow.Enabled = false;
                pnlAdvisorSuggestion.Location = new Point(20, 20);
            }
        }

        private void btnAdvisorSuggestionDecline_Click(object sender, EventArgs e)
        {
            if (empireMessage_0 != null && empireMessage_0.MessageType == EmpireMessageType.AdvisorSuggestion)
            {
                diplomaticMessageQueue_0.RemoveMessage(empireMessage_0);
                Empire empire = null;
                if (empireMessage_0.AdvisorMessageType == AdvisorMessageType.EnemyAttack || empireMessage_0.AdvisorMessageType == AdvisorMessageType.EnemyAttackPlanetDestroyer || empireMessage_0.AdvisorMessageType == AdvisorMessageType.EnemyBlockade || empireMessage_0.AdvisorMessageType == AdvisorMessageType.EnemyBombard)
                {
                    empire = empireMessage_0.ResolveTargetEmpireFromSubject();
                }
                if (empire != null)
                {
                    long expiryDate = _Game.Galaxy.CurrentStarDate + 240000L;
                    _Game.PlayerEmpire.DeclinedTasks.Add(new DeclinedTask(expiryDate, empire));
                }
                object subject = empireMessage_0.Subject;
                if (subject != null)
                {
                    long expiryDate2 = _Game.Galaxy.CurrentStarDate + 600000L;
                    if (subject is BuiltObject)
                    {
                        _Game.PlayerEmpire.DeclinedTasks.Add(new DeclinedTask((BuiltObject)subject, expiryDate2));
                    }
                    else if (subject is Habitat)
                    {
                        _Game.PlayerEmpire.DeclinedTasks.Add(new DeclinedTask((Habitat)subject, expiryDate2));
                    }
                    else if (subject is IntelligenceMission)
                    {
                        _Game.PlayerEmpire.DeclinedTasks.Add(new DeclinedTask((IntelligenceMission)subject, expiryDate2));
                    }
                    else if (subject is Empire)
                    {
                        _Game.PlayerEmpire.DeclinedTasks.Add(new DeclinedTask((Empire)subject, expiryDate2));
                    }
                }
            }
            method_644();
            method_660();
            empireMessage_0 = null;
        }

        private void method_648()
        {
            method_649(null);
        }

        private void method_649(EmpireMessage empireMessage_1)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            empireMessage_0 = empireMessage_1;
            pnlAdvisorSuggestion.Size = new Size(355, 448);
            pnlAdvisorSuggestion.Location = new Point((mainView.Width - pnlAdvisorSuggestion.Width) / 2, (mainView.Height - pnlAdvisorSuggestion.Height) / 2);
            pnlAdvisorSuggestion.DoLayout();
            pnlAdvisorSuggestion.SuspendLayout();
            picAdvisorSuggestionImage.Size = new Size(300, 105);
            picAdvisorSuggestionImage.Location = new Point(20, 10);
            picAdvisorSuggestionImage.SizeMode = PictureBoxSizeMode.Zoom;
            Image image = picAdvisorSuggestionImage.Image;
            if (image != null && image.PixelFormat != 0)
            {
                picAdvisorSuggestionImage.Image = null;
                image.Dispose();
            }
            picAdvisorSuggestionImage.Image = method_659(empireMessage_1);
            picAdvisorSuggestionImage.Visible = true;
            lblAdvisorSuggestionTitle.Location = new Point(10, 125);
            lblAdvisorSuggestionTitle.Size = new Size(320, 32);
            lblAdvisorSuggestionTitle.AutoSize = false;
            lblAdvisorSuggestionTitle.Font = font_1;
            lblAdvisorSuggestionTitle.TextAlign = ContentAlignment.MiddleCenter;
            if (empireMessage_0 != null)
            {
                string text = Galaxy.ResolveDescription(empireMessage_0.AdvisorMessageType, empireMessage_0);
                if (empireMessage_0.AdvisorMessageType == AdvisorMessageType.ColonyFacility && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is PlanetaryFacilityDefinition)
                {
                    PlanetaryFacilityDefinition planetaryFacility = (PlanetaryFacilityDefinition)empireMessage_0.AdvisorMessageData;
                    if (Galaxy.CalculatePlanetaryFacilityCost(planetaryFacility, _Game.PlayerEmpire) > _Game.PlayerEmpire.StateMoney)
                    {
                        text = TextResolver.GetText("Advisor Message ColonyFacility Need Funds");
                    }
                }
                lblAdvisorSuggestionTitle.Text = text;
            }
            else
            {
                lblAdvisorSuggestionTitle.Text = string.Empty;
            }
            pnlAdvisorSuggestionDescriptionContainer.Location = new Point(10, 155);
            pnlAdvisorSuggestionDescriptionContainer.Size = new Size(320, 170);
            lblAdvisorSuggestionDescription.Location = new Point(0, 0);
            lblAdvisorSuggestionDescription.Size = new Size(302, 155);
            lblAdvisorSuggestionDescription.MaximumSize = new Size(302, 600);
            lblAdvisorSuggestionDescription.Text = empireMessage_1.Description;
            lblAdvisorSuggestionDescription.Font = font_6;
            lblAdvisorSuggestionDescription.Location = new Point(0, 0);
            btnAdvisorSuggestionApprove.Size = new Size(100, 40);
            btnAdvisorSuggestionApprove.Location = new Point(10, 335);
            btnAdvisorSuggestionApprove.Text = TextResolver.GetText("Approve");
            btnAdvisorSuggestionApprove.Font = font_7;
            btnAdvisorSuggestionShow.Size = new Size(110, 40);
            btnAdvisorSuggestionShow.Location = new Point(115, 335);
            btnAdvisorSuggestionShow.Text = TextResolver.GetText("Show me first");
            btnAdvisorSuggestionShow.Font = font_7;
            btnAdvisorSuggestionShow.Enabled = true;
            btnAdvisorSuggestionDecline.Size = new Size(100, 40);
            btnAdvisorSuggestionDecline.Location = new Point(230, 335);
            btnAdvisorSuggestionDecline.Text = TextResolver.GetText("Decline");
            btnAdvisorSuggestionDecline.Font = font_7;
            pnlAdvisorSuggestion.ResumeLayout();
            pnlAdvisorSuggestion.Visible = true;
            pnlAdvisorSuggestion.BringToFront();
        }

        private Bitmap method_650(Bitmap bitmap_225, Bitmap bitmap_226, int int_64)
        {
            bitmap_225 = GraphicsHelper.ScaleLimitImage(bitmap_225, int_64, int_64, 1f);
            return method_653(bitmap_225, bitmap_226, int_64);
        }

        private Bitmap method_651(Empire empire_5, Bitmap bitmap_225, int int_64)
        {
            Bitmap bitmap_226 = method_652(empire_5, int_64);
            return method_653(bitmap_226, bitmap_225, int_64);
        }

        private Bitmap method_652(Empire empire_5, int int_64)
        {
            if (empire_5 != null && empire_5.LargeFlagPicture != null && empire_5.LargeFlagPicture.PixelFormat != 0)
            {
                int num = (int)((double)int_64 * 0.6);
                int num2 = int_64 / 3;
                int num3 = int_64 + int_64 - num2;
                Bitmap bitmap = new Bitmap(num3, int_64, PixelFormat.Format32bppPArgb);
                using Graphics graphics = Graphics.FromImage(bitmap);
                GraphicsHelper.SetGraphicsQualityToHigh(graphics);
                graphics.DrawImage(srcRect: new Rectangle(0, 0, empire_5.LargeFlagPicture.Width, empire_5.LargeFlagPicture.Height), destRect: new Rectangle(0, (int_64 - num) / 2, int_64, num), image: empire_5.LargeFlagPicture, srcUnit: GraphicsUnit.Pixel);
                Bitmap empireDominantRaceImage = raceImageCache_0.GetEmpireDominantRaceImage(empire_5);
                if (empireDominantRaceImage != null)
                {
                    if (empireDominantRaceImage.PixelFormat != 0)
                    {
                        Rectangle srcRect2 = new Rectangle(0, 0, empireDominantRaceImage.Width, empireDominantRaceImage.Height);
                        Rectangle destRect2 = new Rectangle(int_64 - num2, (int_64 - int_64) / 2, int_64, int_64);
                        graphics.DrawImage(empireDominantRaceImage, destRect2, srcRect2, GraphicsUnit.Pixel);
                        return bitmap;
                    }
                    return bitmap;
                }
                return bitmap;
            }
            return null;
        }

        private Bitmap method_653(Bitmap bitmap_225, Bitmap bitmap_226, int int_64)
        {
            return method_654(bitmap_225, bitmap_226, int_64, int_64);
        }

        private Bitmap method_654(Bitmap bitmap_225, Bitmap bitmap_226, int int_64, int int_65)
        {
            bitmap_226 = GraphicsHelper.ScaleImageMaximum(bitmap_226, int_65, int_65, 1f);
            int num = bitmap_225.Width + bitmap_226.Width;
            Bitmap bitmap = new Bitmap(num, int_64, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            GraphicsHelper.SetGraphicsQualityToHigh(graphics);
            graphics.DrawImage(bitmap_225, new Point(0, GraphicsHelper.MeasureCenterForImage(bitmap_225, num, int_64).Y));
            graphics.DrawImage(bitmap_226, new Point(y: GraphicsHelper.MeasureCenterForImage(bitmap_226, num, int_64).Y, x: bitmap_225.Width));
            return bitmap;
        }

        private Bitmap method_655(Bitmap bitmap_225, int int_64)
        {
            if (bitmap_225 != null && bitmap_225.Height < int_64)
            {
                double num = (double)int_64 / (double)bitmap_225.Height;
                int num2 = (int)((double)bitmap_225.Width * num);
                bitmap_225 = GraphicsHelper.ScaleImage(bitmap_225, num2, int_64, 1f);
            }
            return bitmap_225;
        }

        private Bitmap method_656(BuiltObject builtObject_8, int int_64)
        {
            return method_658(builtObject_8.PictureRef, int_64);
        }

        private Bitmap method_657(Design design_3, int int_64)
        {
            return method_658(design_3.PictureRef, int_64);
        }

        private Bitmap method_658(int int_64, int int_65)
        {
            if (int_64 >= 0)
            {
                Bitmap bitmap = new Bitmap(builtObjectImageCache_0.ObtainImage(int_64));
                if (bitmap != null)
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    return method_655(bitmap, int_65);
                }
            }
            return null;
        }

        private Bitmap method_659(EmpireMessage empireMessage_1)
        {
            int maxWidth = 300;
            int num = 105;
            int int_ = 90;
            Bitmap result = null;
            switch (empireMessage_1.AdvisorMessageType)
            {
                case AdvisorMessageType.BuildOneOff:
                    {
                        if (!(empireMessage_1.Subject is Habitat))
                        {
                            break;
                        }
                        Habitat habitat12 = (Habitat)empireMessage_1.Subject;
                        if (habitat12 != null && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is Design)
                        {
                            Design design = (Design)empireMessage_0.AdvisorMessageData;
                            if (design != null)
                            {
                                result = method_650(habitatImageCache_0.ObtainImage(habitat12), method_657(design, int_), num);
                            }
                        }
                        break;
                    }
                case AdvisorMessageType.Colonization:
                    {
                        if (!(empireMessage_1.Subject is Habitat))
                        {
                            break;
                        }
                        Habitat habitat9 = (Habitat)empireMessage_1.Subject;
                        if (habitat9 == null)
                        {
                            break;
                        }
                        Bitmap bitmap9 = null;
                        if (empireMessage_1.AdvisorMessageData != null && empireMessage_1.AdvisorMessageData is BuiltObject)
                        {
                            BuiltObject builtObject6 = (BuiltObject)empireMessage_1.AdvisorMessageData;
                            if (builtObject6 != null)
                            {
                                bitmap9 = method_656(builtObject6, int_);
                                bitmap9 = GraphicsHelper.ScaleLimitImage(bitmap9, num, num, 1f);
                            }
                        }
                        Bitmap bitmap10 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat9), num, num, 1f);
                        result = ((bitmap9 == null) ? bitmap10 : method_650(bitmap10, bitmap9, num));
                        break;
                    }
                case AdvisorMessageType.IntelligenceMission:
                    {
                        if (!(empireMessage_1.Subject is IntelligenceMission))
                        {
                            break;
                        }
                        IntelligenceMission intelligenceMission = (IntelligenceMission)empireMessage_1.Subject;
                        if (intelligenceMission == null)
                        {
                            break;
                        }
                        Bitmap bitmap6 = null;
                        Empire empire11 = null;
                        if (intelligenceMission != null)
                        {
                            empire11 = intelligenceMission.TargetEmpire;
                            if (empireMessage_1.AdvisorMessageData != null && empireMessage_1.AdvisorMessageData is Character)
                            {
                                Character character = (Character)empireMessage_1.AdvisorMessageData;
                                if (character != null)
                                {
                                    bitmap6 = characterImageCache_0.ObtainCharacterImage(character);
                                    bitmap6 = GraphicsHelper.ScaleLimitImage(bitmap6, num, num, 1f);
                                }
                            }
                        }
                        if (bitmap6 != null)
                        {
                            result = ((empire11 == null) ? bitmap6 : method_651(empire11, bitmap6, num));
                        }
                        else if (empire11 != null)
                        {
                            result = method_652(empire11, num);
                        }
                        break;
                    }
                case AdvisorMessageType.EnemyBombard:
                    if (empireMessage_1.Subject is Habitat)
                    {
                        Habitat habitat3 = (Habitat)empireMessage_1.Subject;
                        if (habitat3 != null)
                        {
                            Bitmap bitmap3 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat3), num, num, 1f);
                            result = ((habitat3.Empire == null) ? bitmap3 : method_651(habitat3.Empire, bitmap3, num));
                        }
                    }
                    break;
                case AdvisorMessageType.EnemyBlockade:
                    {
                        Empire empire10 = null;
                        Bitmap bitmap5 = null;
                        if (empireMessage_1.Subject is Habitat)
                        {
                            Habitat habitat7 = (Habitat)empireMessage_1.Subject;
                            if (habitat7 != null)
                            {
                                empire10 = habitat7.Empire;
                                bitmap5 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat7), num, num, 1f);
                            }
                        }
                        else if (empireMessage_1.Subject is BuiltObject)
                        {
                            BuiltObject builtObject5 = (BuiltObject)empireMessage_1.Subject;
                            if (builtObject5 != null)
                            {
                                empire10 = builtObject5.Empire;
                                bitmap5 = method_656(builtObject5, int_);
                                bitmap5 = GraphicsHelper.ScaleLimitImage(bitmap5, num, num, 1f);
                            }
                        }
                        if (bitmap5 != null && empire10 != null)
                        {
                            result = method_651(empire10, bitmap5, num);
                        }
                        break;
                    }
                case AdvisorMessageType.EnemyAttackPlanetDestroyer:
                    {
                        if (!(empireMessage_1.Subject is Habitat))
                        {
                            break;
                        }
                        Habitat habitat10 = (Habitat)empireMessage_1.Subject;
                        if (habitat10 != null && empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is BuiltObject)
                        {
                            BuiltObject builtObject7 = (BuiltObject)empireMessage_0.AdvisorMessageData;
                            if (builtObject7 != null)
                            {
                                Bitmap bitmap12 = GraphicsHelper.ScaleLimitImage(builtObjectImageCache_0.ObtainImage(builtObject7), num, num, 1f);
                                result = ((habitat10.Empire == null) ? bitmap12 : method_651(habitat10.Empire, bitmap12, num));
                            }
                        }
                        break;
                    }
                case AdvisorMessageType.InvadeIndependent:
                    {
                        if (!(empireMessage_1.Subject is Habitat))
                        {
                            break;
                        }
                        Habitat habitat11 = (Habitat)empireMessage_1.Subject;
                        if (habitat11 != null)
                        {
                            Bitmap bitmap13 = null;
                            if (habitat11.Population != null && habitat11.Population.DominantRace != null)
                            {
                                bitmap13 = raceImageCache_0.GetRaceImage(habitat11.Population.DominantRace.PictureRef);
                            }
                            result = ((bitmap13 == null) ? GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat11), num, num, 1f) : method_650(habitatImageCache_0.ObtainImage(habitat11), bitmap13, num));
                        }
                        break;
                    }
                case AdvisorMessageType.PrepareRaid:
                    if (empireMessage_1.Subject is Empire)
                    {
                        Empire empire4 = (Empire)empireMessage_1.Subject;
                        if (empire4 != null)
                        {
                            result = method_652(empire4, num);
                        }
                    }
                    break;
                case AdvisorMessageType.DiplomaticGift:
                    if (empireMessage_1.Subject is Empire)
                    {
                        Empire empire15 = (Empire)empireMessage_1.Subject;
                        if (empire15 != null)
                        {
                            result = method_651(empire15, bitmap_52, num);
                        }
                    }
                    break;
                case AdvisorMessageType.TreatyOffer:
                    if (empireMessage_1.Subject is Empire)
                    {
                        Empire empire14 = (Empire)empireMessage_1.Subject;
                        if (empire14 != null)
                        {
                            result = method_652(empire14, num);
                        }
                    }
                    break;
                case AdvisorMessageType.WarTradeSanctions:
                    if (empireMessage_1.Subject is Empire)
                    {
                        Empire empire8 = (Empire)empireMessage_1.Subject;
                        if (empire8 != null)
                        {
                            result = method_652(empire8, num);
                        }
                    }
                    break;
                case AdvisorMessageType.ColonyFacility:
                    {
                        if (!(empireMessage_1.Subject is Habitat))
                        {
                            break;
                        }
                        Habitat habitat8 = (Habitat)empireMessage_1.Subject;
                        if (habitat8 == null)
                        {
                            break;
                        }
                        Bitmap bitmap7 = null;
                        if (empireMessage_1.AdvisorMessageData != null && empireMessage_1.AdvisorMessageData is PlanetaryFacilityDefinition)
                        {
                            PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)empireMessage_1.AdvisorMessageData;
                            if (planetaryFacilityDefinition != null)
                            {
                                bitmap7 = GraphicsHelper.ScaleLimitImage(bitmap_8[planetaryFacilityDefinition.PictureRef], num, num, 1f);
                            }
                        }
                        Bitmap bitmap8 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat8), num, num, 1f);
                        result = ((bitmap8 == null) ? bitmap7 : method_650(bitmap7, bitmap8, num));
                        break;
                    }
                case AdvisorMessageType.OfferMilitaryRefueling:
                    if (empireMessage_1.Subject is Empire)
                    {
                        Empire empire7 = (Empire)empireMessage_1.Subject;
                        if (empire7 != null)
                        {
                            result = method_651(empire7, bitmap_55, num);
                        }
                    }
                    break;
                case AdvisorMessageType.CancelMilitaryRefueling:
                    if (empireMessage_1.Subject is Empire)
                    {
                        Empire empire5 = (Empire)empireMessage_1.Subject;
                        if (empire5 != null)
                        {
                            result = method_651(empire5, bitmap_55, num);
                        }
                    }
                    break;
                case AdvisorMessageType.OfferMiningRights:
                    if (empireMessage_1.Subject is Empire)
                    {
                        Empire empire9 = (Empire)empireMessage_1.Subject;
                        if (empire9 != null)
                        {
                            result = method_651(empire9, bitmap_81, num);
                        }
                    }
                    break;
                case AdvisorMessageType.CancelMiningRights:
                    if (empireMessage_1.Subject is Empire)
                    {
                        Empire empire6 = (Empire)empireMessage_1.Subject;
                        if (empire6 != null)
                        {
                            result = method_651(empire6, bitmap_81, num);
                        }
                    }
                    break;
                case AdvisorMessageType.AllowTradeRestrictedResources:
                case AdvisorMessageType.DisallowTradeRestrictedResources:
                    {
                        ResourceList resourceList = _Game.PlayerEmpire.DetermineResourcesEmpireSupplies();
                        if (resourceList == null || resourceList.Count <= 0)
                        {
                            break;
                        }
                        for (int i = 0; i < _Game.Galaxy.ResourceSystem.SuperLuxuryResources.Count; i++)
                        {
                            ResourceDefinition resourceDefinition = _Game.Galaxy.ResourceSystem.SuperLuxuryResources[i];
                            if (resourceDefinition != null)
                            {
                                Resource resource = new Resource(resourceDefinition.ResourceID);
                                if (resourceList.Contains(resource))
                                {
                                    result = new Bitmap(_uiResourcesBitmaps[resource.PictureRef]);
                                }
                            }
                        }
                        break;
                    }
                case AdvisorMessageType.DefendTerritory:
                    {
                        if (!(empireMessage_1.Subject is Empire))
                        {
                            break;
                        }
                        Empire empire2 = (Empire)empireMessage_1.Subject;
                        if (empire2 == null)
                        {
                            break;
                        }
                        Bitmap bitmap2 = null;
                        if (empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is ShipGroup)
                        {
                            ShipGroup shipGroup2 = (ShipGroup)empireMessage_0.AdvisorMessageData;
                            if (shipGroup2 != null && shipGroup2.LeadShip != null)
                            {
                                bitmap2 = method_656(shipGroup2.LeadShip, int_);
                            }
                        }
                        else if (empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is BuiltObject)
                        {
                            BuiltObject builtObject3 = (BuiltObject)empireMessage_0.AdvisorMessageData;
                            if (builtObject3 != null)
                            {
                                bitmap2 = method_656(builtObject3, int_);
                            }
                        }
                        result = ((bitmap2 == null) ? method_652(empire2, num) : method_651(empire2, bitmap2, num));
                        break;
                    }
                case AdvisorMessageType.Retrofit:
                    result = GraphicsHelper.ScaleLimitImage(bitmap_73, maxWidth, num, 1f);
                    break;
                case AdvisorMessageType.ComplyTradeSanctionsOther:
                case AdvisorMessageType.ComplyWarOther:
                case AdvisorMessageType.RequestLiftTradeSanctionsOther:
                case AdvisorMessageType.RequestEndWarOther:
                    {
                        if (!(empireMessage_1.Subject is Empire))
                        {
                            break;
                        }
                        Empire empire12 = (Empire)empireMessage_1.Subject;
                        if (empire12 == null)
                        {
                            break;
                        }
                        Bitmap bitmap11 = null;
                        if (empireMessage_0.AdvisorMessageData != null && empireMessage_0.AdvisorMessageData is Empire)
                        {
                            Empire empire13 = (Empire)empireMessage_0.AdvisorMessageData;
                            if (empire13 != null)
                            {
                                bitmap11 = empire13.LargeFlagPicture;
                            }
                        }
                        result = ((bitmap11 == null) ? method_652(empire12, num) : method_651(empire12, bitmap11, num));
                        break;
                    }
                case AdvisorMessageType.OfferPirateAttackMission:
                    if (empireMessage_1.Subject is EmpireActivity)
                    {
                        EmpireActivity empireActivity3 = (EmpireActivity)empireMessage_1.Subject;
                        Bitmap bitmap_2 = null;
                        if (empireActivity3.Target is Habitat)
                        {
                            Habitat habitat6 = (Habitat)empireActivity3.Target;
                            bitmap_2 = habitatImageCache_0.ObtainImage(habitat6);
                        }
                        else if (empireActivity3.Target is BuiltObject)
                        {
                            BuiltObject builtObject4 = (BuiltObject)empireActivity3.Target;
                            bitmap_2 = builtObjectImageCache_0.ObtainImage(builtObject4);
                        }
                        result = method_650(bitmap_49, bitmap_2, num);
                    }
                    break;
                case AdvisorMessageType.OfferPirateDefendMission:
                    if (empireMessage_1.Subject is EmpireActivity)
                    {
                        EmpireActivity empireActivity = (EmpireActivity)empireMessage_1.Subject;
                        Bitmap bitmap_ = null;
                        if (empireActivity.Target is Habitat)
                        {
                            Habitat habitat2 = (Habitat)empireActivity.Target;
                            bitmap_ = habitatImageCache_0.ObtainImage(habitat2);
                        }
                        else if (empireActivity.Target is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)empireActivity.Target;
                            bitmap_ = builtObjectImageCache_0.ObtainImage(builtObject2);
                        }
                        result = method_650(bitmap_49, bitmap_, num);
                    }
                    break;
                case AdvisorMessageType.OfferPirateSmuggleMission:
                    {
                        if (!(empireMessage_1.Subject is EmpireActivity))
                        {
                            break;
                        }
                        EmpireActivity empireActivity4 = (EmpireActivity)empireMessage_1.Subject;
                        Habitat habitat13 = null;
                        if (empireActivity4.Target is Habitat)
                        {
                            habitat13 = (Habitat)empireActivity4.Target;
                            if (empireActivity4.ResourceId == byte.MaxValue)
                            {
                                result = habitatImageCache_0.ObtainImage(habitat13);
                                result = GraphicsHelper.ScaleLimitImage(result, num, num, 1f);
                            }
                            else
                            {
                                result = method_650(habitatImageCache_0.ObtainImage(habitat13), _uiResourcesBitmaps[new Resource(empireActivity4.ResourceId).PictureRef], num);
                            }
                        }
                        break;
                    }
                case AdvisorMessageType.PirateFacilityEradicate:
                    if (empireMessage_1.Subject != null && empireMessage_1.Subject is Habitat)
                    {
                        Empire empire3 = null;
                        Bitmap bitmap4 = null;
                        Habitat habitat5 = (Habitat)empireMessage_1.Subject;
                        if (habitat5 != null)
                        {
                            empire3 = habitat5.Empire;
                            bitmap4 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat5), num, num, 1f);
                        }
                        if (empireMessage_1.AdvisorMessageData2 != null && empireMessage_1.AdvisorMessageData2 is Empire)
                        {
                            empire3 = (Empire)empireMessage_1.AdvisorMessageData2;
                        }
                        if (bitmap4 != null)
                        {
                            result = ((empire3 == null || empire3 == _Game.Galaxy.IndependentEmpire) ? bitmap4 : method_651(empire3, bitmap4, num));
                        }
                        else if (empire3 != null && empire3 != _Game.Galaxy.IndependentEmpire)
                        {
                            result = method_652(empire3, num);
                        }
                    }
                    break;
                case AdvisorMessageType.AcceptPirateSmugglingMission:
                    {
                        if (!(empireMessage_1.Subject is EmpireActivity))
                        {
                            break;
                        }
                        EmpireActivity empireActivity2 = (EmpireActivity)empireMessage_1.Subject;
                        Habitat habitat4 = null;
                        if (empireActivity2.Target is Habitat)
                        {
                            habitat4 = (Habitat)empireActivity2.Target;
                            if (empireActivity2.ResourceId == byte.MaxValue)
                            {
                                result = habitatImageCache_0.ObtainImage(habitat4);
                                result = GraphicsHelper.ScaleLimitImage(result, num, num, 1f);
                            }
                            else
                            {
                                result = method_650(habitatImageCache_0.ObtainImage(habitat4), _uiResourcesBitmaps[new Resource(empireActivity2.ResourceId).PictureRef], num);
                            }
                        }
                        break;
                    }
                case AdvisorMessageType.EnemyAttack:
                case AdvisorMessageType.PirateRaid:
                case AdvisorMessageType.DefendTarget:
                    {
                        if (empireMessage_1.Subject == null || (!(empireMessage_1.Subject is BuiltObject) && !(empireMessage_1.Subject is Habitat) && !(empireMessage_1.Subject is ShipGroup) && !(empireMessage_1.Subject is Creature)))
                        {
                            break;
                        }
                        Empire empire = null;
                        Bitmap bitmap = null;
                        if (empireMessage_1.Subject != null)
                        {
                            if (empireMessage_1.Subject is BuiltObject)
                            {
                                BuiltObject builtObject = (BuiltObject)empireMessage_1.Subject;
                                if (builtObject != null)
                                {
                                    empire = builtObject.Empire;
                                    bitmap = method_656(builtObject, int_);
                                    bitmap = GraphicsHelper.ScaleLimitImage(bitmap, num, num, 1f);
                                }
                            }
                            else if (empireMessage_1.Subject is Habitat)
                            {
                                Habitat habitat = (Habitat)empireMessage_1.Subject;
                                if (habitat != null)
                                {
                                    empire = habitat.Empire;
                                    bitmap = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat), num, num, 1f);
                                }
                            }
                            else if (empireMessage_1.Subject is Creature)
                            {
                                Creature creature = (Creature)empireMessage_1.Subject;
                                if (creature != null)
                                {
                                    bitmap = GraphicsHelper.ScaleLimitImage(bitmap_10[creature.PictureRef][0], num, num, 1f);
                                }
                            }
                            else if (empireMessage_1.Subject is ShipGroup)
                            {
                                ShipGroup shipGroup = (ShipGroup)empireMessage_1.Subject;
                                if (shipGroup != null && shipGroup.LeadShip != null)
                                {
                                    empire = shipGroup.Empire;
                                    bitmap = method_656(shipGroup.LeadShip, int_);
                                    bitmap = GraphicsHelper.ScaleLimitImage(bitmap, num, num, 1f);
                                }
                            }
                        }
                        if (bitmap != null)
                        {
                            result = ((empire == null || empire == _Game.Galaxy.IndependentEmpire) ? bitmap : method_651(empire, bitmap, num));
                        }
                        else if (empire != null && empire != _Game.Galaxy.IndependentEmpire)
                        {
                            result = method_652(empire, num);
                        }
                        break;
                    }
            }
            return result;
        }

        private void method_660()
        {
            pnlAdvisorSuggestion.SendToBack();
            pnlAdvisorSuggestion.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_661()
        {
            method_662(null);
        }

        private void method_662(Character character_0)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            string text = TextResolver.GetText("Character Event History");
            if (character_0 != null)
            {
                text = text + ": " + character_0.Name;
            }
            pnlCharacterEventHistory.HeaderTitle = text;
            pnlCharacterEventHistory.Size = new Size(670, 454);
            pnlCharacterEventHistory.Location = new Point((mainView.Width - pnlCharacterEventHistory.Width) / 2, (mainView.Height - pnlCharacterEventHistory.Height) / 2);
            pnlCharacterEventHistory.DoLayout();
            pnlCharacterEventHistory.SuspendLayout();
            ctlCharacterEvents.Location = new Point(10, 10);
            ctlCharacterEvents.Size = new Size(280, 370);
            ctlCharacterEvents.Grid.Columns["Star Date"].Width = 70;
            ctlCharacterEvents.Grid.Columns["Title"].Width = 210;
            ctlCharacterEvents.Grid.Columns["Title"].HeaderText = TextResolver.GetText("Event");
            if (character_0 != null && character_0.EventHistory != null)
            {
                CharacterEventList characterEventList = character_0.EventHistory.ObtainPublicEvents();
                ctlCharacterEvents.BindData(characterEventList, _Game.PlayerEmpire);
                if (characterEventList.Count > 0)
                {
                    method_664(characterEventList[0], _Game.PlayerEmpire);
                }
                else
                {
                    method_664(null, _Game.PlayerEmpire);
                }
            }
            else
            {
                method_664(null, _Game.PlayerEmpire);
                ctlCharacterEvents.ClearData();
            }
            lblCharacterEventHistoryTitle.Location = new Point(300, 10);
            lblCharacterEventHistoryTitle.Font = font_7;
            txtCharacterEventHistoryDescription.Location = new Point(300, 32);
            txtCharacterEventHistoryDescription.Size = new Size(343, 348);
            txtCharacterEventHistoryDescription.Font = font_6;
            pnlCharacterEventHistory.ResumeLayout();
            pnlCharacterEventHistory.Visible = true;
            pnlCharacterEventHistory.BringToFront();
        }

        private void method_663()
        {
            pnlCharacterEventHistory.SendToBack();
            pnlCharacterEventHistory.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_664(CharacterEvent characterEvent_0, Empire empire_5)
        {
            if (characterEvent_0 != null)
            {
                string title = string.Empty;
                string text = Galaxy.ResolveDescription(characterEvent_0, empire_5, out title);
                lblCharacterEventHistoryTitle.Text = title;
                txtCharacterEventHistoryDescription.Text = text.Replace("\n", Environment.NewLine);
            }
            else
            {
                lblCharacterEventHistoryTitle.Text = "";
                txtCharacterEventHistoryDescription.Text = "";
            }
        }

        private void ctlCharacterEvents_SelectionChanged(object sender, EventArgs e)
        {
            method_664(ctlCharacterEvents.SelectedCharacterEvent, _Game.PlayerEmpire);
        }

        private void pnlCharacterEventHistory_CloseButtonClicked(object sender, EventArgs e)
        {
            method_663();
        }

        private void btnCharacterShowEventHistory_Click(object sender, EventArgs e)
        {
            Character selectedCharacter = ctlIntelligenceAgents.SelectedCharacter;
            method_662(selectedCharacter);
        }

        private void pnlColonyPopulationAttitudeSummary_MouseEnter(object sender, EventArgs e)
        {
            control_0 = base.ActiveControl;
            pnlColonyPopulationAttitudeSummaryContainer.Focus();
        }

        private void pnlColonyPopulationAttitudeSummary_Enter(object sender, EventArgs e)
        {
            control_0 = base.ActiveControl;
            pnlColonyPopulationAttitudeSummaryContainer.Focus();
        }

        private void nDrsqatloR_MouseEnter(object sender, EventArgs e)
        {
            control_0 = base.ActiveControl;
            pnlEmpirePolicyContainer.Focus();
        }

        private void pnlResearchTree_MouseEnter(object sender, EventArgs e)
        {
            control_0 = base.ActiveControl;
            WdosRcovZt.Focus();
        }

        private void pnlResearchTree_MouseLeave(object sender, EventArgs e)
        {
            control_0 = null;
        }

        private void pnlColonyPopulationAttitudeSummary_MouseLeave(object sender, EventArgs e)
        {
            if (control_0 != null)
            {
                control_0.Focus();
            }
            control_0 = null;
        }

        private void nDrsqatloR_MouseLeave(object sender, EventArgs e)
        {
            control_0 = null;
        }

        private void QxrIvWcaOp()
        {
            SuspendLayout();
            pnlDetailInfo.Size = new Size(280, 240);
            pnlDetailInfo.Location = new Point(44, 5);
            pnlDetailInfo.Kickstart(isLargeSize: false);
            pnlDetailInfo.Reset();
            pnlDetailInfo.BringToFront();
            pnlInfoPanel.Size = new Size(370, 250);
            pnlInfoPanel.Location = new Point(26, mainView.Size.Height - (pnlDetailInfo.Size.Height + 50));
            pnlInfoPanel.BringToFront();
            int num = mainView.Size.Height - (pnlDetailInfo.Height + 45) + 1;
            Size size = new Size(56, 28);
            btnLockView.Size = size;
            btnCycleColoniesBack.Size = size;
            btnCycleBasesBack.Size = size;
            btnCycleMilitaryBack.Size = size;
            btnCycleConstructionBack.Size = size;
            btnCycleOtherBack.Size = size;
            btnCycleShipGroupsBack.Size = size;
            btnCycleIdleShipsBack.Size = size;
            btnCycleShipStance.Size = size;
            btnSelectNearestMilitary.Size = size;
            btnCycleColonies.Size = size;
            btnCycleBases.Size = size;
            btnCycleMilitary.Size = size;
            btnCycleConstruction.Size = size;
            btnCycleOther.Size = size;
            btnCycleShipGroups.Size = size;
            btnCycleIdleShips.Size = size;
            btnSelectionPanelSize.Size = size;
            btnLockView.Location = new Point(10, num);
            btnCycleColoniesBack.Location = new Point(10, num + 30);
            btnCycleColoniesBack.BringToFront();
            btnCycleBasesBack.Location = new Point(10, num + 60);
            btnCycleBasesBack.BringToFront();
            btnCycleMilitaryBack.Location = new Point(10, num + 90);
            btnCycleMilitaryBack.BringToFront();
            btnCycleConstructionBack.Location = new Point(10, num + 120);
            btnCycleConstructionBack.BringToFront();
            btnCycleOtherBack.Location = new Point(10, num + 150);
            btnCycleOtherBack.BringToFront();
            btnCycleShipGroupsBack.Location = new Point(10, num + 180);
            btnCycleShipGroupsBack.BringToFront();
            btnCycleIdleShipsBack.Location = new Point(10, num + 210);
            btnCycleIdleShipsBack.BringToFront();
            btnSelectionPanelSize.Location = new Point(353, num - 36);
            btnSelectionPanelSize.BringToFront();
            btnSelectNearestMilitary.Location = new Point(353, num);
            btnSelectNearestMilitary.BringToFront();
            btnCycleColonies.Location = new Point(353, num + 30);
            btnCycleColonies.BringToFront();
            btnCycleBases.Location = new Point(353, num + 60);
            btnCycleBases.BringToFront();
            btnCycleMilitary.Location = new Point(353, num + 90);
            btnCycleMilitary.BringToFront();
            btnCycleConstruction.Location = new Point(353, num + 120);
            btnCycleConstruction.BringToFront();
            btnCycleOther.Location = new Point(353, num + 150);
            btnCycleOther.BringToFront();
            btnCycleShipGroups.Location = new Point(353, num + 180);
            btnCycleShipGroups.BringToFront();
            btnCycleIdleShips.Location = new Point(353, num + 210);
            btnCycleIdleShips.BringToFront();
            btnCycleShipStance.Location = new Point(353, num + 240);
            btnCycleShipStance.BringToFront();
            Size size2 = new Size(35, 28);
            btnSelectionAction1.Size = size2;
            btnSelectionAction2.Size = size2;
            btnSelectionAction3.Size = size2;
            btnSelectionAction4.Size = size2;
            btnSelectionAction5.Size = size2;
            btnSelectionAction6.Size = size2;
            btnSelectionAction7.Size = size2;
            btnSelectionAction8.Size = size2;
            int num2 = pnlInfoPanel.Bottom + 2;
            btnSelectionAction1.Location = new Point(70, num2);
            btnSelectionAction2.Location = new Point(105, num2);
            btnSelectionAction3.Location = new Point(140, num2);
            btnSelectionAction4.Location = new Point(175, num2);
            btnSelectionAction5.Location = new Point(210, num2);
            btnSelectionAction6.Location = new Point(245, num2);
            btnSelectionAction7.Location = new Point(280, num2);
            btnSelectionAction8.Location = new Point(315, num2);
            Size size3 = new Size(138, 28);
            btnSelectionBack.Size = size3;
            btnSelectionForward.Size = size3;
            btnSelectionBack.Location = new Point(71, num - 36);
            btnSelectionBack.BringToFront();
            btnSelectionForward.Location = new Point(213, num - 36);
            btnSelectionForward.BringToFront();
            method_212();
            btnLockView.Location = new Point(10, num);
            btnLockView.BringToFront();
            mainView.HoverMessageLocation = new Point(10, mainView.Size.Height - (pnlInfoPanel.Height + btnSelectionForward.Height + btnSelectionAction1.Height + 4 + 35));
            pnlColonyInvasionContainer.Location = new Point(pnlInfoPanel.Right + 20, base.ClientRectangle.Height - (pnlColonyInvasionContainer.Height + 10));
            method_666();
            method_208(_Game.SelectedObject);
            ResumeLayout();
        }

        private void method_665()
        {
            SuspendLayout();
            pnlDetailInfo.Size = new Size(392, 360);
            pnlDetailInfo.Location = new Point(44, 5);
            pnlDetailInfo.Kickstart(isLargeSize: true);
            pnlDetailInfo.Reset();
            pnlDetailInfo.BringToFront();
            pnlInfoPanel.Size = new Size(482, 370);
            pnlInfoPanel.Location = new Point(26, mainView.Size.Height - (pnlDetailInfo.Size.Height + 50));
            pnlInfoPanel.BringToFront();
            int num = mainView.Size.Height - (pnlDetailInfo.Height + 45) + 1;
            Size size = new Size(56, 43);
            btnLockView.Size = size;
            btnCycleColoniesBack.Size = size;
            btnCycleBasesBack.Size = size;
            btnCycleMilitaryBack.Size = size;
            btnCycleConstructionBack.Size = size;
            btnCycleOtherBack.Size = size;
            btnCycleShipGroupsBack.Size = size;
            btnCycleIdleShipsBack.Size = size;
            btnCycleShipStance.Size = size;
            btnSelectNearestMilitary.Size = size;
            btnCycleColonies.Size = size;
            btnCycleBases.Size = size;
            btnCycleMilitary.Size = size;
            btnCycleConstruction.Size = size;
            btnCycleOther.Size = size;
            btnCycleShipGroups.Size = size;
            btnCycleIdleShips.Size = size;
            btnLockView.Location = new Point(10, num);
            btnCycleColoniesBack.Location = new Point(10, num + (size.Height + 2));
            btnCycleColoniesBack.BringToFront();
            btnCycleBasesBack.Location = new Point(10, num + (size.Height + 2) * 2);
            btnCycleBasesBack.BringToFront();
            btnCycleMilitaryBack.Location = new Point(10, num + (size.Height + 2) * 3);
            btnCycleMilitaryBack.BringToFront();
            btnCycleConstructionBack.Location = new Point(10, num + (size.Height + 2) * 4);
            btnCycleConstructionBack.BringToFront();
            btnCycleOtherBack.Location = new Point(10, num + (size.Height + 2) * 5);
            btnCycleOtherBack.BringToFront();
            btnCycleShipGroupsBack.Location = new Point(10, num + (size.Height + 2) * 6);
            btnCycleShipGroupsBack.BringToFront();
            btnCycleIdleShipsBack.Location = new Point(10, num + (size.Height + 2) * 7);
            btnCycleIdleShipsBack.BringToFront();
            btnSelectionPanelSize.Location = new Point(465, num - 36);
            btnSelectionPanelSize.BringToFront();
            btnSelectNearestMilitary.Location = new Point(465, num);
            btnSelectNearestMilitary.BringToFront();
            btnCycleColonies.Location = new Point(465, num + (size.Height + 2));
            btnCycleColonies.BringToFront();
            btnCycleBases.Location = new Point(465, num + (size.Height + 2) * 2);
            btnCycleBases.BringToFront();
            btnCycleMilitary.Location = new Point(465, num + (size.Height + 2) * 3);
            btnCycleMilitary.BringToFront();
            btnCycleConstruction.Location = new Point(465, num + (size.Height + 2) * 4);
            btnCycleConstruction.BringToFront();
            btnCycleOther.Location = new Point(465, num + (size.Height + 2) * 5);
            btnCycleOther.BringToFront();
            btnCycleShipGroups.Location = new Point(465, num + (size.Height + 2) * 6);
            btnCycleShipGroups.BringToFront();
            btnCycleIdleShips.Location = new Point(465, num + (size.Height + 2) * 7);
            btnCycleIdleShips.BringToFront();
            btnCycleShipStance.Location = new Point(465, num + (size.Height + 2) * 8);
            btnCycleShipStance.BringToFront();
            Size size2 = new Size(49, 28);
            btnSelectionAction1.Size = size2;
            btnSelectionAction2.Size = size2;
            btnSelectionAction3.Size = size2;
            btnSelectionAction4.Size = size2;
            btnSelectionAction5.Size = size2;
            btnSelectionAction6.Size = size2;
            btnSelectionAction7.Size = size2;
            btnSelectionAction8.Size = size2;
            int num2 = pnlInfoPanel.Bottom + 2;
            btnSelectionAction1.Location = new Point(70, num2);
            btnSelectionAction2.Location = new Point(119, num2);
            btnSelectionAction3.Location = new Point(168, num2);
            btnSelectionAction4.Location = new Point(217, num2);
            btnSelectionAction5.Location = new Point(266, num2);
            btnSelectionAction6.Location = new Point(315, num2);
            btnSelectionAction7.Location = new Point(364, num2);
            btnSelectionAction8.Location = new Point(413, num2);
            Size size3 = new Size(194, 28);
            btnSelectionBack.Size = size3;
            btnSelectionForward.Size = size3;
            btnSelectionBack.Location = new Point(71, num - 36);
            btnSelectionBack.BringToFront();
            btnSelectionForward.Location = new Point(269, num - 36);
            btnSelectionForward.BringToFront();
            method_212();
            btnLockView.Location = new Point(10, num);
            btnLockView.BringToFront();
            mainView.HoverMessageLocation = new Point(10, mainView.Size.Height - (pnlInfoPanel.Height + btnSelectionForward.Height + btnSelectionAction1.Height + 4 + 35));
            pnlColonyInvasionContainer.Location = new Point(pnlInfoPanel.Right + 20, base.ClientRectangle.Height - (pnlColonyInvasionContainer.Height + 10));
            method_666();
            method_208(_Game.SelectedObject);
            ResumeLayout();
        }

        private void btnSelectionPanelSize_Click(object sender, EventArgs e)
        {
            SuspendLayout();
            if (pnlDetailInfo.ContentSizeIsLarge)
            {
                pnlDetailInfo.Size = new Size(280, 240);
                pnlDetailInfo.Location = new Point(44, 5);
                pnlDetailInfo.Kickstart(isLargeSize: false);
                pnlDetailInfo.Reset();
                pnlDetailInfo.BringToFront();
                pnlInfoPanel.Size = new Size(370, 250);
                pnlInfoPanel.Location = new Point(26, mainView.Size.Height - (pnlDetailInfo.Size.Height + 50));
                pnlInfoPanel.BringToFront();
                int num = mainView.Size.Height - (pnlDetailInfo.Height + 45) + 1;
                Size size = new Size(56, 28);
                btnLockView.Size = size;
                btnCycleColoniesBack.Size = size;
                btnCycleBasesBack.Size = size;
                btnCycleMilitaryBack.Size = size;
                btnCycleConstructionBack.Size = size;
                btnCycleOtherBack.Size = size;
                btnCycleShipGroupsBack.Size = size;
                btnCycleIdleShipsBack.Size = size;
                btnCycleShipStance.Size = size;
                btnSelectNearestMilitary.Size = size;
                btnCycleColonies.Size = size;
                btnCycleBases.Size = size;
                btnCycleMilitary.Size = size;
                btnCycleConstruction.Size = size;
                btnCycleOther.Size = size;
                btnCycleShipGroups.Size = size;
                btnCycleIdleShips.Size = size;
                btnSelectionPanelSize.Size = size;
                btnLockView.Location = new Point(10, num);
                btnCycleColoniesBack.Location = new Point(10, num + 30);
                btnCycleColoniesBack.BringToFront();
                btnCycleBasesBack.Location = new Point(10, num + 60);
                btnCycleBasesBack.BringToFront();
                btnCycleMilitaryBack.Location = new Point(10, num + 90);
                btnCycleMilitaryBack.BringToFront();
                btnCycleConstructionBack.Location = new Point(10, num + 120);
                btnCycleConstructionBack.BringToFront();
                btnCycleOtherBack.Location = new Point(10, num + 150);
                btnCycleOtherBack.BringToFront();
                btnCycleShipGroupsBack.Location = new Point(10, num + 180);
                btnCycleShipGroupsBack.BringToFront();
                btnCycleIdleShipsBack.Location = new Point(10, num + 210);
                btnCycleIdleShipsBack.BringToFront();
                btnSelectionPanelSize.Location = new Point(353, num - 36);
                btnSelectionPanelSize.BringToFront();
                btnSelectNearestMilitary.Location = new Point(353, num);
                btnSelectNearestMilitary.BringToFront();
                btnCycleColonies.Location = new Point(353, num + 30);
                btnCycleColonies.BringToFront();
                btnCycleBases.Location = new Point(353, num + 60);
                btnCycleBases.BringToFront();
                btnCycleMilitary.Location = new Point(353, num + 90);
                btnCycleMilitary.BringToFront();
                btnCycleConstruction.Location = new Point(353, num + 120);
                btnCycleConstruction.BringToFront();
                btnCycleOther.Location = new Point(353, num + 150);
                btnCycleOther.BringToFront();
                btnCycleShipGroups.Location = new Point(353, num + 180);
                btnCycleShipGroups.BringToFront();
                btnCycleIdleShips.Location = new Point(353, num + 210);
                btnCycleIdleShips.BringToFront();
                btnCycleShipStance.Location = new Point(353, num + 240);
                btnCycleShipStance.BringToFront();
                Size size2 = new Size(35, 28);
                btnSelectionAction1.Size = size2;
                btnSelectionAction2.Size = size2;
                btnSelectionAction3.Size = size2;
                btnSelectionAction4.Size = size2;
                btnSelectionAction5.Size = size2;
                btnSelectionAction6.Size = size2;
                btnSelectionAction7.Size = size2;
                btnSelectionAction8.Size = size2;
                int num2 = pnlInfoPanel.Bottom + 2;
                btnSelectionAction1.Location = new Point(70, num2);
                btnSelectionAction2.Location = new Point(105, num2);
                btnSelectionAction3.Location = new Point(140, num2);
                btnSelectionAction4.Location = new Point(175, num2);
                btnSelectionAction5.Location = new Point(210, num2);
                btnSelectionAction6.Location = new Point(245, num2);
                btnSelectionAction7.Location = new Point(280, num2);
                btnSelectionAction8.Location = new Point(315, num2);
                Size size3 = new Size(138, 28);
                btnSelectionBack.Size = size3;
                btnSelectionForward.Size = size3;
                btnSelectionBack.Location = new Point(71, num - 36);
                btnSelectionBack.BringToFront();
                btnSelectionForward.Location = new Point(213, num - 36);
                btnSelectionForward.BringToFront();
                method_212();
                btnLockView.Location = new Point(10, num);
                btnLockView.BringToFront();
            }
            else
            {
                pnlDetailInfo.Size = new Size(392, 360);
                pnlDetailInfo.Location = new Point(44, 5);
                pnlDetailInfo.Kickstart(isLargeSize: true);
                pnlDetailInfo.Reset();
                pnlDetailInfo.BringToFront();
                pnlInfoPanel.Size = new Size(482, 370);
                pnlInfoPanel.Location = new Point(26, mainView.Size.Height - (pnlDetailInfo.Size.Height + 50));
                pnlInfoPanel.BringToFront();
                int num3 = mainView.Size.Height - (pnlDetailInfo.Height + 45) + 1;
                Size size4 = new Size(56, 43);
                btnLockView.Size = size4;
                btnCycleColoniesBack.Size = size4;
                btnCycleBasesBack.Size = size4;
                btnCycleMilitaryBack.Size = size4;
                btnCycleConstructionBack.Size = size4;
                btnCycleOtherBack.Size = size4;
                btnCycleShipGroupsBack.Size = size4;
                btnCycleIdleShipsBack.Size = size4;
                btnCycleShipStance.Size = size4;
                btnSelectNearestMilitary.Size = size4;
                btnCycleColonies.Size = size4;
                btnCycleBases.Size = size4;
                btnCycleMilitary.Size = size4;
                btnCycleConstruction.Size = size4;
                btnCycleOther.Size = size4;
                btnCycleShipGroups.Size = size4;
                btnCycleIdleShips.Size = size4;
                btnLockView.Location = new Point(10, num3);
                btnCycleColoniesBack.Location = new Point(10, num3 + (size4.Height + 2));
                btnCycleColoniesBack.BringToFront();
                btnCycleBasesBack.Location = new Point(10, num3 + (size4.Height + 2) * 2);
                btnCycleBasesBack.BringToFront();
                btnCycleMilitaryBack.Location = new Point(10, num3 + (size4.Height + 2) * 3);
                btnCycleMilitaryBack.BringToFront();
                btnCycleConstructionBack.Location = new Point(10, num3 + (size4.Height + 2) * 4);
                btnCycleConstructionBack.BringToFront();
                btnCycleOtherBack.Location = new Point(10, num3 + (size4.Height + 2) * 5);
                btnCycleOtherBack.BringToFront();
                btnCycleShipGroupsBack.Location = new Point(10, num3 + (size4.Height + 2) * 6);
                btnCycleShipGroupsBack.BringToFront();
                btnCycleIdleShipsBack.Location = new Point(10, num3 + (size4.Height + 2) * 7);
                btnCycleIdleShipsBack.BringToFront();
                btnSelectionPanelSize.Location = new Point(465, num3 - 36);
                btnSelectionPanelSize.BringToFront();
                btnSelectNearestMilitary.Location = new Point(465, num3);
                btnSelectNearestMilitary.BringToFront();
                btnCycleColonies.Location = new Point(465, num3 + (size4.Height + 2));
                btnCycleColonies.BringToFront();
                btnCycleBases.Location = new Point(465, num3 + (size4.Height + 2) * 2);
                btnCycleBases.BringToFront();
                btnCycleMilitary.Location = new Point(465, num3 + (size4.Height + 2) * 3);
                btnCycleMilitary.BringToFront();
                btnCycleConstruction.Location = new Point(465, num3 + (size4.Height + 2) * 4);
                btnCycleConstruction.BringToFront();
                btnCycleOther.Location = new Point(465, num3 + (size4.Height + 2) * 5);
                btnCycleOther.BringToFront();
                btnCycleShipGroups.Location = new Point(465, num3 + (size4.Height + 2) * 6);
                btnCycleShipGroups.BringToFront();
                btnCycleIdleShips.Location = new Point(465, num3 + (size4.Height + 2) * 7);
                btnCycleIdleShips.BringToFront();
                btnCycleShipStance.Location = new Point(465, num3 + (size4.Height + 2) * 8);
                btnCycleShipStance.BringToFront();
                Size size5 = new Size(49, 28);
                btnSelectionAction1.Size = size5;
                btnSelectionAction2.Size = size5;
                btnSelectionAction3.Size = size5;
                btnSelectionAction4.Size = size5;
                btnSelectionAction5.Size = size5;
                btnSelectionAction6.Size = size5;
                btnSelectionAction7.Size = size5;
                btnSelectionAction8.Size = size5;
                int num4 = pnlInfoPanel.Bottom + 2;
                btnSelectionAction1.Location = new Point(70, num4);
                btnSelectionAction2.Location = new Point(119, num4);
                btnSelectionAction3.Location = new Point(168, num4);
                btnSelectionAction4.Location = new Point(217, num4);
                btnSelectionAction5.Location = new Point(266, num4);
                btnSelectionAction6.Location = new Point(315, num4);
                btnSelectionAction7.Location = new Point(364, num4);
                btnSelectionAction8.Location = new Point(413, num4);
                Size size6 = new Size(194, 28);
                btnSelectionBack.Size = size6;
                btnSelectionForward.Size = size6;
                btnSelectionBack.Location = new Point(71, num3 - 36);
                btnSelectionBack.BringToFront();
                btnSelectionForward.Location = new Point(269, num3 - 36);
                btnSelectionForward.BringToFront();
                method_212();
                btnLockView.Location = new Point(10, num3);
                btnLockView.BringToFront();
            }
            if (pnlDetailInfo.ContentSizeIsLarge)
            {
                gameOptions_0.SelectionPanelSize = 1;
            }
            else
            {
                gameOptions_0.SelectionPanelSize = 0;
            }
            mainView.HoverMessageLocation = new Point(10, mainView.Size.Height - (pnlInfoPanel.Height + btnSelectionForward.Height + btnSelectionAction1.Height + 4 + 35));
            pnlColonyInvasionContainer.Location = new Point(pnlInfoPanel.Right + 20, base.ClientRectangle.Height - (pnlColonyInvasionContainer.Height + 10));
            method_666();
            method_208(_Game.SelectedObject);
            ResumeLayout();
        }

        private void method_666()
        {
            Size clientSize = base.ClientSize;
            int num = pnlInfoPanel.Top - 200;
            int val = (int)(26f * itemListCollectionPanel_0.SizeFactor);
            int num2 = Math.Min(val, num / 15);
            int num3 = 41;
            int num4 = 18;
            int num5 = 14;
            int num6 = 14;
            int num7 = 2;
            if (itemListCollectionPanel_0.Panels != null && itemListCollectionPanel_0.Panels.Count > 0)
            {
                ItemListPanel itemListPanel = itemListCollectionPanel_0.Panels[0];
                if (itemListPanel != null)
                {
                    num3 = itemListPanel.DefaultItemHeight;
                    num4 = itemListPanel.TitleBarHeight;
                    num5 = itemListPanel.ScrollUpHeight;
                    num6 = itemListPanel.ScrollDownHeight;
                    num7 = itemListPanel.ItemGap;
                }
            }
            int num8 = num4 + 1 + num5 + 1 + num6 + num7;
            int num9 = num3 + num7;
            int num10 = Math.Min(10, 5 + (clientSize.Height - 793) / num9);
            int val2 = num8 + num10 * num9;
            int num11 = Math.Min(num, val2);
            int num12 = 150 + (num - num11) / 2;
            int num13 = 300;
            num13 = (int)(300f * itemListCollectionPanel_0.SizeFactor);
            itemListCollectionPanel_0.SelectionButtonHeight = num2;
            itemListCollectionPanel_0.SelectionButtonWidth = num2;
            if (itemListCollectionPanel_0.Panels != null)
            {
                for (int i = 0; i < itemListCollectionPanel_0.Panels.Count; i++)
                {
                    ItemListPanel itemListPanel2 = itemListCollectionPanel_0.Panels[i];
                    if (itemListPanel2 != null && itemListPanel2.IconImageOriginal != null && itemListPanel2.IconImageOriginal.PixelFormat != 0)
                    {
                        int val3 = Math.Min(itemListCollectionPanel_0.SelectionButtonHeight, itemListCollectionPanel_0.SelectionButtonWidth) - 6;
                        int val4 = (int)(16f * itemListCollectionPanel_0.SizeFactor);
                        val3 = Math.Min(val4, val3);
                        Bitmap bitmap = (itemListPanel2.IconImage = itemListCollectionPanel_0.ScaleLimitImage(itemListPanel2.IconImageOriginal, val3, val3, itemListCollectionPanel_0.AlphaTransparency));
                    }
                }
            }
            Rectangle area = new Rectangle(8, num12, num13, num11);
            itemListCollectionPanel_0.Initialize(this, area);
            if (itemListCollectionPanel_0.ActivePanel != null)
            {
                itemListCollectionPanel_0.ActivePanelArea = new Rectangle(itemListCollectionPanel_0.Area.Left + itemListCollectionPanel_0.SelectionButtonWidth + 2, itemListCollectionPanel_0.Area.Top, itemListCollectionPanel_0.Area.Width - (itemListCollectionPanel_0.SelectionButtonWidth + 2), itemListCollectionPanel_0.Area.Height);
            }
        }

        public void ChangeItemPanelSize(int size)
        {
            size = Math.Min(2, Math.Max(0, size));
            switch (size)
            {
                case 0:
                    itemListCollectionPanel_0.SetSizeFactor(1f);
                    gameOptions_0.EmpireNavigationToolSize = 0;
                    break;
                case 1:
                    itemListCollectionPanel_0.SetSizeFactor(1.33f);
                    gameOptions_0.EmpireNavigationToolSize = 1;
                    break;
                case 2:
                    itemListCollectionPanel_0.SetSizeFactor(1.77f);
                    gameOptions_0.EmpireNavigationToolSize = 2;
                    break;
            }
            itemListCollectionPanel_0.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall(), raceImageCache_0, _uiResourcesBitmaps, bitmap_8, bitmap_79, bitmap_80, bitmap_128, bitmap_129, bitmap_130, bitmap_131, bitmap_132, bitmap_126, bitmap_127, bitmap_28[30]);
            method_666();
        }

        private void chkOptionsSuppressAllPopups_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOptionsSuppressAllPopups.Checked)
            {
                if (cmbGameOptionsEncounterRuins.SelectedIndex < 1)
                {
                    cmbGameOptionsEncounterRuins.SelectedIndex = Math.Max(1, _Game.PlayerEmpire.DiscoveryActionRuin);
                }
                if (cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex < 1)
                {
                    cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex = Math.Max(1, _Game.PlayerEmpire.DiscoveryActionAbandonedShipBase);
                }
            }
        }

        private void cmbGameOptionsEncounterAbandonedShipOrBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkOptionsSuppressAllPopups.Checked)
            {
                cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndexChanged -= cmbGameOptionsEncounterAbandonedShipOrBase_SelectedIndexChanged;
                if (cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex < 1)
                {
                    cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex = Math.Max(1, _Game.PlayerEmpire.DiscoveryActionAbandonedShipBase);
                }
                cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndexChanged += cmbGameOptionsEncounterAbandonedShipOrBase_SelectedIndexChanged;
            }
        }

        private void cmbGameOptionsEncounterRuins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkOptionsSuppressAllPopups.Checked)
            {
                cmbGameOptionsEncounterRuins.SelectedIndexChanged -= cmbGameOptionsEncounterRuins_SelectedIndexChanged;
                if (cmbGameOptionsEncounterRuins.SelectedIndex < 1)
                {
                    cmbGameOptionsEncounterRuins.SelectedIndex = Math.Max(1, _Game.PlayerEmpire.DiscoveryActionRuin);
                }
                cmbGameOptionsEncounterRuins.SelectedIndexChanged += cmbGameOptionsEncounterRuins_SelectedIndexChanged;
            }
        }

        private void RemIoawDot(Habitat habitat_9, int int_64)
        {
            if (habitat_9 != null)
            {
                int num = int_64;
                if (num >= habitatImageCache_0.GetImagesSmall().Length)
                {
                    num = habitatImageCache_0.GetImagesSmall().Length - 1;
                }
                else if (num < 0)
                {
                    num = 0;
                }
                if (picEditHabitatPicture.Image != null && picEditHabitatPicture.Image is Bitmap)
                {
                    Bitmap bitmap = (Bitmap)picEditHabitatPicture.Image;
                    picEditHabitatPicture.Image = null;
                    bitmap.Dispose();
                }
                habitat_9.PictureRef = (short)num;
                picEditHabitatPicture.Image = new Bitmap(habitatImageCache_0.ObtainImage(habitat_9.PictureRef));
                picEditHabitatPicture.Refresh();
                mainView.ClearPrecachedHabitatBitmaps();
            }
        }

        private void scrEditHabitatPicture_Scroll(object sender, ScrollEventArgs e)
        {
            RemIoawDot(habitat_4, e.NewValue);
        }

        private void method_667(Habitat habitat_9, int int_64)
        {
            if (habitat_9 != null)
            {
                int num = int_64;
                if (num >= bitmap_29.Length)
                {
                    num = bitmap_29.Length - 1;
                }
                else if (num < 0)
                {
                    num = 0;
                }
                habitat_9.LandscapePictureRef = (short)num;
                picEditHabitatPictureLandscape.Image = bitmap_29[habitat_9.LandscapePictureRef];
                picEditHabitatPictureLandscape.Refresh();
            }
        }

        private void scrEditHabitatPictureLandscape_Scroll(object sender, ScrollEventArgs e)
        {
            method_667(habitat_4, e.NewValue);
        }

        private void btnGameEditorEditGalaxy_Click(object sender, EventArgs e)
        {
            method_668();
        }

        private void method_668()
        {
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                method_154();
            }
            pnlEditGalaxy.Size = new Size(500, 435);
            pnlEditGalaxy.Location = new Point((mainView.Width - pnlEditGalaxy.Width) / 2, (mainView.Height - pnlEditGalaxy.Height) / 2);
            lblEditGalaxyHeading.Font = font_1;
            lblEditGalaxyHeading.Location = new Point(10, 10);
            lblEditGalaxyHeading.Text = TextResolver.GetText("Edit Galaxy");
            lblEditGalaxyExplanation.Location = new Point(10, 45);
            lblEditGalaxyExplanation.MaximumSize = new Size(480, 35);
            lblEditGalaxyExplanation.Text = TextResolver.GetText("Galaxy Description Label");
            lblEditGalaxyTitle.Location = new Point(10, 90);
            txtEditGalaxyTitle.Location = new Point(10, 105);
            txtEditGalaxyTitle.Size = new Size(480, 21);
            lblEditGalaxyDescription.Location = new Point(10, 140);
            txtEditGalaxyDescription.Location = new Point(10, 155);
            txtEditGalaxyDescription.Size = new Size(480, 235);
            btnEditGalaxyClose.Size = new Size(150, 25);
            btnEditGalaxyClose.Location = new Point(340, 10);
            btnEditGalaxyClose.Text = TextResolver.GetText("Close");
            btnEditGalaxyShowEvents.Size = new Size(480, 25);
            btnEditGalaxyShowEvents.Location = new Point(10, 400);
            btnEditGalaxyShowEvents.Text = TextResolver.GetText("Show Game Events for this Galaxy");
            method_669();
            pnlEditGalaxy.BringToFront();
            pnlEditGalaxy.Visible = true;
        }

        private void method_669()
        {
            txtEditGalaxyTitle.Text = _Game.Galaxy.Title;
            txtEditGalaxyDescription.Text = _Game.Galaxy.Description;
        }

        private void method_670()
        {
            _Game.Galaxy.Title = txtEditGalaxyTitle.Text;
            _Game.Galaxy.Description = txtEditGalaxyDescription.Text;
        }

        private void method_671()
        {
            method_670();
            pnlEditGalaxy.SendToBack();
            pnlEditGalaxy.Visible = false;
        }

        private void btnEditGalaxyClose_Click(object sender, EventArgs e)
        {
            method_671();
        }

        private void method_672()
        {
            method_673(null);
        }

        private void method_673(GameEvent gameEvent_1)
        {
            ynbOfkDbGY.Size = new Size(450, 650);
            ynbOfkDbGY.Location = new Point(mainView.Width - ynbOfkDbGY.Width - 60, 90);
            ctlGameEvent.Location = new Point(5, 5);
            ctlGameEvent.Size = new Size(440, 640);
            StellarObject stellarObject = null;
            if (gameEvent_0 != null)
            {
                stellarObject = gameEvent_0.TriggerObject;
                if (gameEvent_1 == null)
                {
                    gameEvent_1 = gameEvent_0;
                }
            }
            else if (builtObject_7 != null)
            {
                stellarObject = builtObject_7;
            }
            else if (creature_1 != null)
            {
                stellarObject = creature_1;
            }
            else if (habitat_4 != null)
            {
                stellarObject = habitat_4;
            }
            if (gameEvent_1 == null && stellarObject != null && stellarObject.GameEventId >= 0)
            {
                gameEvent_1 = _Game.Galaxy.GameEvents.GetById(stellarObject.GameEventId);
            }
            if (gameEvent_1 != null)
            {
                ctlGameEvent.BindData(_Game.Galaxy, gameEvent_1, bitmap_8, font_6, font_1, characterImageCache_0);
            }
            else if (stellarObject != null)
            {
                short nextId = _Game.Galaxy.GameEvents.GetNextId();
                gameEvent_1 = new GameEvent(_Game.Galaxy, nextId, stellarObject);
                _Game.Galaxy.GameEvents.Add(gameEvent_1);
                stellarObject.GameEventId = nextId;
                ctlGameEvent.BindData(_Game.Galaxy, gameEvent_1, bitmap_8, font_6, font_1, characterImageCache_0);
            }
            ynbOfkDbGY.Visible = true;
            ynbOfkDbGY.BringToFront();
        }

        private void method_674()
        {
            ctlGameEvent.UnbindData();
            method_677();
            method_680();
            ynbOfkDbGY.Visible = false;
            ynbOfkDbGY.SendToBack();
        }

        private void method_675(GameEvent gameEvent_1, EventAction eventAction_0)
        {
            pnlEditEventAction.Size = new Size(460, 632);
            pnlEditEventAction.Location = new Point(mainView.Width - pnlEditEventAction.Width - 500, 180);
            ctlEventAction.Visible = true;
            ctlEventAction.Location = new Point(5, 5);
            ctlEventAction.Size = new Size(450, 622);
            ctlEventAction.BindData(eventAction_0, gameEvent_1, font_6, font_7, font_1, _Game.Galaxy, bitmap_8, raceImageCache_0.GetRaceImages(), _uiResourcesBitmaps, characterImageCache_0);
            pnlEditEventAction.Visible = true;
            pnlEditEventAction.BringToFront();
        }

        private void ctlEventAction_ActionDeleted(object sender, EventArgs e)
        {
        }

        private void ctlEventAction_GotoTarget(object sender, EventArgs e)
        {
            if (sender != null && sender is StellarObject)
            {
                StellarObject stellarObject = (StellarObject)sender;
                if (stellarObject != null)
                {
                    method_157(stellarObject);
                }
            }
        }

        private void method_676()
        {
            ctlEventAction.UnbindData();
            ctlGameEvent.BindActions();
            pnlEditEventAction.Visible = false;
            pnlEditEventAction.SendToBack();
        }

        private void ctlEventAction_PanelClosed(object sender, EventArgs e)
        {
            method_676();
        }

        private void ctlGameEvent_PanelClosed(object sender, EventArgs e)
        {
            method_674();
        }

        private void method_677()
        {
            if (builtObject_7 != null && builtObject_7.GameEventId >= 0)
            {
                GameEvent byId = _Game.Galaxy.GameEvents.GetById(builtObject_7.GameEventId);
                lblEditBuiltObjectGameEvent.Text = Galaxy.ResolveDescription(byId);
                lblEditBuiltObjectGameEvent.ForeColor = Color.Yellow;
                btnEditBuiltObjectGameEvent.Text = TextResolver.GetText("Edit Event");
            }
            else
            {
                lblEditBuiltObjectGameEvent.Text = "(" + TextResolver.GetText("No Event set") + ")";
                lblEditBuiltObjectGameEvent.ForeColor = Color.FromArgb(170, 170, 170);
                btnEditBuiltObjectGameEvent.Text = TextResolver.GetText("Add Event");
            }
            if (habitat_4 != null && habitat_4.GameEventId >= 0)
            {
                GameEvent byId2 = _Game.Galaxy.GameEvents.GetById(habitat_4.GameEventId);
                lblEditHabitatGameEvent.Text = Galaxy.ResolveDescription(byId2);
                lblEditHabitatGameEvent.ForeColor = Color.Yellow;
                btnEditHabitatGameEvent.Text = TextResolver.GetText("Edit Event");
            }
            else
            {
                lblEditHabitatGameEvent.Text = "(" + TextResolver.GetText("No Event set") + ")";
                lblEditHabitatGameEvent.ForeColor = Color.FromArgb(170, 170, 170);
                btnEditHabitatGameEvent.Text = TextResolver.GetText("Add Event");
            }
            if (creature_1 != null && creature_1.GameEventId >= 0)
            {
                GameEvent byId3 = _Game.Galaxy.GameEvents.GetById(creature_1.GameEventId);
                lblEditCreatureGameEvent.Text = Galaxy.ResolveDescription(byId3);
                lblEditCreatureGameEvent.ForeColor = Color.Yellow;
                btnEditCreatureGameEvent.Text = TextResolver.GetText("Edit Event");
            }
            else
            {
                lblEditCreatureGameEvent.Text = "(" + TextResolver.GetText("No Event set") + ")";
                lblEditCreatureGameEvent.ForeColor = Color.FromArgb(170, 170, 170);
                btnEditCreatureGameEvent.Text = TextResolver.GetText("Add Event");
            }
        }

        private void btnEditBuiltObjectGameEvent_Click(object sender, EventArgs e)
        {
            method_672();
        }

        private void btnEditCreatureGameEvent_Click(object sender, EventArgs e)
        {
            method_672();
        }

        private void btnEditHabitatGameEvent_Click(object sender, EventArgs e)
        {
            method_672();
        }

        private void ctlGameEvent_AddNewBlankAction(object sender, EventArgs e)
        {
            EventAction eventAction = new EventAction(null, EventActionType.FindMoneyTreasure);
            if (ctlGameEvent.GameEvent != null)
            {
                ctlGameEvent.GameEvent.Actions.Add(eventAction);
                method_675(ctlGameEvent.GameEvent, eventAction);
            }
        }

        private void ctlGameEvent_EditAction(object sender, EventArgs e)
        {
            method_675(ctlGameEvent.GameEvent, ctlGameEvent.SelectedEventAction);
        }

        private void ctlGameEvent_SelectActionTarget(object sender, EventArgs e)
        {
            mouseHoverMode_0 = MouseHoverMode.SetEventActionTarget;
        }

        private void btnEditBuiltObjectClose_Click(object sender, EventArgs e)
        {
            method_488();
        }

        private void btnEditCreatureClose_Click(object sender, EventArgs e)
        {
            method_482();
        }

        private void btnEditHabitatClose_Click(object sender, EventArgs e)
        {
            method_494();
        }

        private void method_678()
        {
            pnlEditGameEvents.Size = new Size(620, 680);
            pnlEditGameEvents.Location = new Point(mainView.Width - pnlEditGameEvents.Width - 20, 60);
            EfcOvcsSlw.Location = new Point(10, 10);
            EfcOvcsSlw.Font = font_1;
            EfcOvcsSlw.Text = TextResolver.GetText("Game Events for this Galaxy");
            btnEditGameEventsClose.Size = new Size(75, 25);
            btnEditGameEventsClose.Location = new Point(535, 10);
            btnEditGameEventsClose.Text = TextResolver.GetText("Close");
            ctlGameEvents.Location = new Point(10, 40);
            ctlGameEvents.Size = new Size(600, 600);
            ctlGameEvents.BringToFront();
            btnEditGameEventsEdit.Size = new Size(100, 25);
            btnEditGameEventsEdit.Location = new Point(10, 645);
            btnEditGameEventsEdit.Text = TextResolver.GetText("Edit Event");
            btnEditGameEventsAddNew.Size = new Size(150, 25);
            btnEditGameEventsAddNew.Location = new Point(115, 645);
            btnEditGameEventsAddNew.Text = TextResolver.GetText("Add New Event");
            btnEditGameEventsDelete.Size = new Size(100, 25);
            btnEditGameEventsDelete.Location = new Point(270, 645);
            btnEditGameEventsDelete.Text = TextResolver.GetText("Delete Event");
            btnEditGameEventsGoto.Size = new Size(150, 25);
            btnEditGameEventsGoto.Location = new Point(375, 645);
            btnEditGameEventsGoto.Text = TextResolver.GetText("Go to Event Target");
            ctlGameEvents.BindData(_Game.Galaxy.GameEvents);
            pnlEditGameEvents.Visible = true;
            pnlEditGameEvents.BringToFront();
        }

        private void method_679()
        {
            pnlEditGameEvents.Visible = false;
            pnlEditGameEvents.SendToBack();
        }

        private void btnEditGalaxyShowEvents_Click(object sender, EventArgs e)
        {
            method_678();
        }

        private void ctlGameEvents_RowDoubleClick(object sender, EventArgs e)
        {
            gameEvent_0 = ctlGameEvents.SelectedGameEvent;
            if (gameEvent_0 != null)
            {
                method_672();
            }
        }

        private void btnEditGameEventsClose_Click(object sender, EventArgs e)
        {
            gameEvent_0 = null;
            method_679();
        }

        private void method_680()
        {
            GameEvent selectedGameEvent = ctlGameEvents.SelectedGameEvent;
            ctlGameEvents.BindData(_Game.Galaxy.GameEvents);
            if (selectedGameEvent != null)
            {
                ctlGameEvents.SelectGameEvent(selectedGameEvent);
            }
        }

        private void btnEditGameEventsAddNew_Click(object sender, EventArgs e)
        {
            short nextId = _Game.Galaxy.GameEvents.GetNextId();
            GameEvent gameEvent = new GameEvent(_Game.Galaxy, nextId, null);
            _Game.Galaxy.GameEvents.Add(gameEvent);
            method_673(gameEvent);
        }

        private void btnEditGameEventsDelete_Click(object sender, EventArgs e)
        {
            GameEvent selectedGameEvent = ctlGameEvents.SelectedGameEvent;
            if (selectedGameEvent == null)
            {
                return;
            }
            selectedGameEvent = _Game.Galaxy.GameEvents.GetById(selectedGameEvent.GameEventId);
            if (selectedGameEvent != null)
            {
                if (selectedGameEvent.TriggerObject != null)
                {
                    selectedGameEvent.TriggerObject.GameEventId = -1;
                }
                int num = _Game.Galaxy.GameEvents.IndexOf(selectedGameEvent);
                _Game.Galaxy.GameEvents.Remove(selectedGameEvent);
                ctlGameEvents.BindData(_Game.Galaxy.GameEvents);
                if (num > 0)
                {
                    ctlGameEvents.SelectGameEvent(_Game.Galaxy.GameEvents[num - 1]);
                }
            }
        }

        private void btnEditGameEventsGoto_Click(object sender, EventArgs e)
        {
            GameEvent selectedGameEvent = ctlGameEvents.SelectedGameEvent;
            if (selectedGameEvent != null && selectedGameEvent.TriggerObject != null)
            {
                method_157(selectedGameEvent.TriggerObject);
            }
        }

        private void btnEditGameEventsEdit_Click(object sender, EventArgs e)
        {
            gameEvent_0 = ctlGameEvents.SelectedGameEvent;
            if (gameEvent_0 != null)
            {
                method_672();
            }
        }

        private void cmbDesignImageScalingMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbDesignImageScalingMode.SelectedScalingMode)
            {
                case DesignImageScalingMode.None:
                    numDesignImageScalingAmount.Enabled = false;
                    numDesignImageScalingAmount.DecimalPlaces = 2;
                    numDesignImageScalingAmount.Maximum = 1000m;
                    numDesignImageScalingAmount.Minimum = 0m;
                    numDesignImageScalingAmount.Value = 1m;
                    numDesignImageScalingAmount.Increment = 0.01m;
                    break;
                case DesignImageScalingMode.Absolute:
                    numDesignImageScalingAmount.Enabled = true;
                    numDesignImageScalingAmount.DecimalPlaces = 0;
                    numDesignImageScalingAmount.Maximum = 1000m;
                    numDesignImageScalingAmount.Value = 100m;
                    numDesignImageScalingAmount.Minimum = 10m;
                    numDesignImageScalingAmount.Increment = 1m;
                    break;
                case DesignImageScalingMode.Scaled:
                    numDesignImageScalingAmount.Enabled = true;
                    numDesignImageScalingAmount.DecimalPlaces = 2;
                    numDesignImageScalingAmount.Minimum = 0.05m;
                    numDesignImageScalingAmount.Value = 1.0m;
                    numDesignImageScalingAmount.Maximum = 10.0m;
                    numDesignImageScalingAmount.Increment = 0.01m;
                    break;
            }
            if (design_0 != null)
            {
                design_0.ImageScalingType = cmbDesignImageScalingMode.SelectedScalingMode;
                design_0.ImageScalingFactor = (float)numDesignImageScalingAmount.Value;
            }
        }

        private void btnEditEmpireSelectTechs_Click(object sender, EventArgs e)
        {
            if (empire_0 != null && empire_0.Research != null && empire_0.Research.TechTree != null)
            {
                pnlResearchTree.EditMode = true;
                method_395(IndustryType.Weapon, null, empire_0);
            }
        }

        private void method_681()
        {
            method_682(bool_28: false);
        }

        private void method_682(bool bool_28)
        {
            if (bool_28)
            {
                pnlRelationAllianceName.Size = new Size(335, 60);
                chkRelationAllianceNameLocked.Enabled = true;
                chkRelationAllianceNameLocked.Visible = true;
            }
            else
            {
                pnlRelationAllianceName.Size = new Size(335, 40);
                chkRelationAllianceNameLocked.Enabled = false;
                chkRelationAllianceNameLocked.Visible = false;
            }
            pnlRelationAllianceName.Location = new Point((mainView.Width - pnlRelationAllianceName.Width) / 2, (mainView.Height - pnlRelationAllianceName.Height) / 2);
            lblRelationAllianceName.Location = new Point(10, 12);
            lblRelationAllianceName.Font = font_7;
            lblRelationAllianceName.Text = TextResolver.GetText("Alliance Name");
            btnRelationAllianceNameApply.Text = TextResolver.GetText("Apply");
            chkRelationAllianceNameLocked.Text = TextResolver.GetText("Locked");
            if (diplomaticRelation_0 != null)
            {
                txtRelationAllianceName.Text = diplomaticRelation_0.AllianceName;
                if (bool_28)
                {
                    chkRelationAllianceNameLocked.Checked = diplomaticRelation_0.Locked;
                }
            }
            pnlRelationAllianceName.BringToFront();
            pnlRelationAllianceName.Visible = true;
        }

        private void method_683()
        {
            if (diplomaticRelation_0 != null)
            {
                diplomaticRelation_0.AllianceName = txtRelationAllianceName.Text;
                if (chkRelationAllianceNameLocked.Enabled)
                {
                    diplomaticRelation_0.Locked = chkRelationAllianceNameLocked.Checked;
                }
                DiplomaticRelation diplomaticRelation = diplomaticRelation_0.OtherEmpire.ObtainDiplomaticRelation(diplomaticRelation_0.ThisEmpire);
                if (diplomaticRelation != null)
                {
                    diplomaticRelation.AllianceName = txtRelationAllianceName.Text;
                    if (chkRelationAllianceNameLocked.Enabled)
                    {
                        diplomaticRelation.Locked = chkRelationAllianceNameLocked.Checked;
                    }
                }
            }
            ctlEmpireDiplomaticRelationList.Invalidate();
            pnlRelationAllianceName.Visible = false;
            pnlRelationAllianceName.SendToBack();
        }

        private void ctlEmpireDiplomaticRelationList__RelationDoubleClicked(object sender, EventArgs e)
        {
            if (sender is Empire)
            {
                Empire empire = (Empire)sender;
                diplomaticRelation_0 = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire);
                if (diplomaticRelation_0 != null)
                {
                    method_681();
                }
            }
        }

        private void btnRelationAllianceNameApply_Click(object sender, EventArgs e)
        {
            method_683();
        }

        private void ctlEditEmpireRelationList_RowDoubleClick(object sender, EventArgs e)
        {
            DiplomaticRelation selectedRelation = ctlEditEmpireRelationList.SelectedRelation;
            if (selectedRelation != null)
            {
                diplomaticRelation_0 = selectedRelation;
                method_682(bool_28: true);
            }
        }

        private void btnEditHabitatAddRuins_Click(object sender, EventArgs e)
        {
            if (habitat_4 != null && habitat_4.Ruin == null)
            {
                _Game.Galaxy.SelectRuins(habitat_4, definitePlacement: true, assignCreatures: false, allowNegativeEffects: false);
                method_491(habitat_4);
            }
        }

        private void btnEditHabitatRemoveRuins_Click(object sender, EventArgs e)
        {
            if (habitat_4 != null && habitat_4.Ruin != null)
            {
                habitat_4.Ruin = null;
                _Game.Galaxy.RuinsHabitats.Remove(habitat_4);
                _Game.Galaxy.RuinCount--;
                method_491(habitat_4);
            }
        }

        private void method_684()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            string text = TextResolver.GetText("Edit Character Skills and Traits");
            if (ctlCharacterSummary.Character != null)
            {
                text = text + ": " + ctlCharacterSummary.Character.Name;
                pnlCharacterSkillTraitEditor.BindData(ctlCharacterSummary.Character);
            }
            pnlCharacterEditSkillsTraits.HeaderTitle = text;
            pnlCharacterEditSkillsTraits.Size = new Size(500, 475);
            pnlCharacterEditSkillsTraits.Location = new Point((mainView.Width - pnlCharacterEditSkillsTraits.Width) / 2, (mainView.Height - pnlCharacterEditSkillsTraits.Height) / 2);
            pnlCharacterEditSkillsTraits.DoLayout();
            pnlCharacterEditSkillsTraits.SuspendLayout();
            pnlCharacterSkillTraitEditor.Size = new Size(400, 345);
            pnlCharacterSkillTraitEditor.Location = new Point(73, 8);
            btnCharacterEditSkillsTraitsApply.Size = new Size(250, 30);
            btnCharacterEditSkillsTraitsApply.Location = new Point(120, 365);
            pnlCharacterEditSkillsTraits.ResumeLayout();
            pnlCharacterEditSkillsTraits.Visible = true;
            pnlCharacterEditSkillsTraits.BringToFront();
        }

        private void method_685()
        {
            pnlCharacterEditSkillsTraits.SendToBack();
            pnlCharacterEditSkillsTraits.Visible = false;
        }

        private void pnlCharacterEditSkillsTraits_CloseButtonClicked(object sender, EventArgs e)
        {
            method_685();
        }

        private void btnCharacterEditSkillsTraitsApply_Click(object sender, EventArgs e)
        {
            if (ctlCharacterSummary.Character != null)
            {
                Character character = ctlCharacterSummary.Character;
                pnlCharacterSkillTraitEditor.UnbindData(ref character);
                ctlCharacterSummary.BindData(character, _Game.Galaxy, characterImageCache_0, bitmap_29, bitmap_31, bitmap_103, bool_26);
                method_425(character, empire_4, bool_26);
            }
            method_685();
        }

        private void method_686()
        {
            try
            {
                Bitmap bitmap = null;
                if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                {
                    method_154();
                }
                method_92();
                string initialDirectory = Application.StartupPath + "\\images\\units\\characters\\";
                string text = GetCustomizationPath();
                if (!string.IsNullOrEmpty(text) && Directory.Exists(text + "images\\units\\characters\\"))
                {
                    initialDirectory = text + "images\\units\\characters\\";
                }
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = TextResolver.GetText("PNG image files") + " (*.png)|*.png";
                openFileDialog.DefaultExt = "png";
                openFileDialog.Title = TextResolver.GetText("Load Character image");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stream stream;
                    if ((stream = openFileDialog.OpenFile()) != null)
                    {
                        Application.DoEvents();
                        Cursor.Current = Cursors.WaitCursor;
                        Application.UseWaitCursor = true;
                        base.Enabled = false;
                        bitmap = (Bitmap)Image.FromStream(stream);
                        if (bitmap != null && ctlCharacterSummary.Character != null)
                        {
                            ctlCharacterSummary.Character.PictureFilename = openFileDialog.SafeFileName;
                            bitmap.Dispose();
                        }
                        openFileDialog.Dispose();
                        base.Enabled = true;
                        Application.UseWaitCursor = false;
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
            }
            catch (Exception ex)
            {
                ShowMessageBox(TextResolver.GetText("There was an error while loading the image") + ": " + ex.ToString(), TextResolver.GetText("Error loading Character image"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void ctlCharacterSummary_CharacterChangeEmpire(object sender, EventArgs e)
        {
            method_425(ctlIntelligenceAgents.SelectedCharacter, empire_4, bool_26);
        }

        private void ctlCharacterSummary_CharacterChangeImage(object sender, EventArgs e)
        {
            method_686();
            method_425(ctlIntelligenceAgents.SelectedCharacter, empire_4, bool_26);
        }

        private void ctlCharacterSummary_CharacterChangeRole(object sender, EventArgs e)
        {
            method_425(ctlIntelligenceAgents.SelectedCharacter, empire_4, bool_26);
        }

        private void ctlCharacterSummary_CharacterEditSkillsTraits(object sender, EventArgs e)
        {
            method_684();
        }

        private void btnEditEmpireCharacters_Click(object sender, EventArgs e)
        {
            method_425(null, empire_0, bool_28: true);
        }

        [CompilerGenerated]
        private static int smethod_1(KeyValuePair<string, int> obj1, KeyValuePair<string, int> obj2)
        {
            return obj1.Key.CompareTo(obj2.Key);
        }
        public static void ShowMessageBox(string message, string caption, MessageBoxButtons btn, MessageBoxIcon icon)
        {
            if (Class5._Splash != null && !Class5._Splash.SplashClosing)
            {
                Class5._Splash.ShowMessageBox(message, caption, btn, icon);
            }
            else
            {
                MessageBox.Show(message, caption, btn, icon);
            }
        }
        static Main()
        {

            byte_0 = new byte[16]
            {
            208, 141, 168, 238, 207, 138, 254, 115, 152, 155,
            93, 70, 16, 207, 158, 239
            };
            byte_1 = new byte[16]
            {
            65, 7, 17, 80, 166, 57, 65, 255, 123, 186,
            118, 197, 197, 146, 196, 198
            };
        }

        private void AddMenuItems(Point coord)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Show Energy collection at this point for current tech");
            toolStripMenuItem.Click += ToolStripMenuItem_Click;
            toolStripMenuItem.Tag = coord;
            actionMenu.Items.Add(toolStripMenuItem);
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GalaxyLocationList locations = null;
            if (sender is ToolStripMenuItem obj && obj.Tag is Point coord)
            {
                int xCoord = coord.X;
                int yCoord = coord.Y;
                method_151(ref xCoord, ref yCoord);
                double energyCollection = 0;
                Habitat nearestSystem = this._Game.Galaxy.FastFindNearestSystem(xCoord, yCoord);
                int currentCollectionValue = 0;
                var collerctorList = _Game.PlayerEmpire.Research.ResearchedComponents.Where(x => x.Category == ComponentCategoryType.EnergyCollector);
                if (collerctorList.Count() > 0)
                { currentCollectionValue = collerctorList.Max(y => y.Value1); }

                if (currentCollectionValue > 0)
                {
                    if (nearestSystem != null)
                    {
                        double num = (double)Galaxy.MaxSolarSystemSize + 500.0;
                        if (nearestSystem.Category == HabitatCategoryType.GasCloud)
                        {
                            num = (double)(nearestSystem.Diameter / 2) + 500.0;
                        }
                        double distanceToNearesSystem = _Game.Galaxy.CalculateDistance(xCoord, yCoord, nearestSystem.Xpos, nearestSystem.Ypos);
                        if (distanceToNearesSystem <= num)
                        {
                            //double num3 = (double)EnergyCollection * (double)(int)nearestSystem.SolarRadiation * 10.0 / 100.0;
                            double solarRad = (double)currentCollectionValue * (double)(int)nearestSystem.SolarRadiation * 10.0 / 100.0;
                            double microRad = (double)currentCollectionValue * (double)(int)nearestSystem.MicrowaveRadiation * 10.0 / 100.0;
                            double xrayRad = (double)currentCollectionValue * (double)(int)nearestSystem.XrayRadiation * 10.0 / 100.0;
                            double totalRad = solarRad + microRad + xrayRad;
                            energyCollection = totalRad * (num - distanceToNearesSystem + 2000.0) / num;
                        }
                    }
                    //else if (_HyperjumpDisabledLocation)
                    else if (_Game.Galaxy.DetermineGalaxyLocationsAtPoint(xCoord, yCoord, GalaxyLocationType.Undefined, ref locations) &&
                        locations.Any(x => x.Effect == GalaxyLocationEffectType.HyperjumpDisabled))
                    {
                        //bool flag = _Game.Galaxy.DetermineGalaxyLocationsAtPoint(Xpos, Ypos, GalaxyLocationType.Undefined, ref locations);
                        double num7 = 100.0;
                        //double num8 = (double)EnergyCollection * num7 * timePassed / 100.0;
                        energyCollection = (double)currentCollectionValue * num7 / 100.0;
                    }
                }
                MessageBoxEx messageBox = MessageBoxExManager.CreateMessageBox((string)null, new Font("Verdana", 9f, FontStyle.Regular));
                messageBox.Text = energyCollection.ToString("N2");
                messageBox.Caption = "Location energy collection";
                messageBox.AddButton(MessageBoxExButtons.Ok);
                messageBox.Icon = MessageBoxExIcon.None;
                bool flag = _Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                if (!flag)
                    _Game.Galaxy.Pause();
                messageBox.Show(this);
                if (!flag)
                    _Game.Galaxy.Resume();
            }
        }

        private void ShowHotKeys()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            _ExpModMain.ShowHotKeyEditor(this);
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }


  }

}