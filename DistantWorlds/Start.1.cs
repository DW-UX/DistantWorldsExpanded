using BaconDistantWorlds;
using DistantWorlds.Controls;
using DistantWorlds.Types;
using ExpansionMod;
using Ionic.Zlib;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
//using WMPLib;
using static System.Windows.Forms.AxHost;
using EmpireList = DistantWorlds.Types.EmpireList;
using LinearGradientMode = DistantWorlds.Controls.LinearGradientMode;

namespace DistantWorlds
{
    public partial class Start
    {
        private void lnkTutorialFleetsTroops_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "FleetsTroops";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.3;
            string string_ = TextResolver.GetText("Expanding");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 == null)
            {
                return;
            }
            Habitat capital = game_0.PlayerEmpire.Capital;
            ShipGroup shipGroup = new ShipGroup(game_0.Galaxy);
            shipGroup.Name = string.Format(TextResolver.GetText("Nth Fleet"), game_0.PlayerEmpire.GetNextFleetNumberDescription());
            shipGroup.Empire = game_0.PlayerEmpire;
            shipGroup.GatherPoint = capital;
            Design design = game_0.PlayerEmpire.Designs.FindNewest(BuiltObjectSubRole.Frigate);
            Design design2 = game_0.PlayerEmpire.Designs.FindNewest(BuiltObjectSubRole.Destroyer);
            BuiltObjectList items = new BuiltObjectList();
            for (int j = 0; j < 5; j++)
            {
                double num11 = 0.0;
                double num12 = 0.0;
                design.BuildCount++;
                BuiltObject builtObject = game_0.PlayerEmpire.GenerateBuiltObjectFromDesign(design, game_0.Galaxy.GenerateBuiltObjectName(design), isState: true, capital.Xpos + 50.0, capital.Ypos + 100.0);
                builtObject.ParentHabitat = capital;
                builtObject.DateBuilt = game_0.Galaxy.CurrentStarDate;
                builtObject.DateRetrofit = game_0.Galaxy.CurrentStarDate;
                game_0.Galaxy.SelectRelativeParkingPoint(out num11, out num12);
                builtObject.ParentOffsetX = num11;
                builtObject.ParentOffsetY = num12;
                builtObject.Xpos = capital.Xpos + num11;
                builtObject.Ypos = capital.Ypos + num12;
                builtObject.Heading = game_0.Galaxy.SelectRandomHeading();
                builtObject.CurrentFuel = builtObject.FuelCapacity;
                builtObject.CurrentEnergy = builtObject.ReactorStorageCapacity;
                builtObject.CurrentShields = builtObject.ShieldsCapacity;
                shipGroup.AddShipToFleet(builtObject);
            }
            for (int k = 0; k < 3; k++)
            {
                double num13 = 0.0;
                double num14 = 0.0;
                design2.BuildCount++;
                BuiltObject builtObject2 = game_0.PlayerEmpire.GenerateBuiltObjectFromDesign(design2, game_0.Galaxy.GenerateBuiltObjectName(design2), isState: true, capital.Xpos + 50.0, capital.Ypos + 100.0);
                builtObject2.ParentHabitat = capital;
                builtObject2.DateBuilt = game_0.Galaxy.CurrentStarDate;
                builtObject2.DateRetrofit = game_0.Galaxy.CurrentStarDate;
                game_0.Galaxy.SelectRelativeParkingPoint(out num13, out num14);
                builtObject2.ParentOffsetX = num13;
                builtObject2.ParentOffsetY = num14;
                builtObject2.Xpos = capital.Xpos + num13;
                builtObject2.Ypos = capital.Ypos + num14;
                builtObject2.Heading = game_0.Galaxy.SelectRandomHeading();
                builtObject2.CurrentFuel = builtObject2.FuelCapacity;
                builtObject2.CurrentEnergy = builtObject2.ReactorStorageCapacity;
                builtObject2.CurrentShields = builtObject2.ShieldsCapacity;
                shipGroup.AddShipToFleet(builtObject2);
            }
            shipGroup.Ships.AddRange(items);
            shipGroup.Update();
            game_0.PlayerEmpire.ShipGroups.Add(shipGroup);
            for (int l = 0; l < 2; l++)
            {
                bool isRandomCharacter = false;
                game_0.PlayerEmpire.GenerateNewCharacter(CharacterRole.IntelligenceAgent, game_0.PlayerEmpire.Capital, out isRandomCharacter);
            }
            DiplomaticRelation diplomaticRelation = game_0.PlayerEmpire.DiplomaticRelations[game_0.Galaxy.Empires[1]];
            if (diplomaticRelation == null)
            {
                diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], tradeRestrictedResources: false);
                game_0.PlayerEmpire.DiplomaticRelations.Add(diplomaticRelation);
                DiplomaticRelation diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], game_0.PlayerEmpire, tradeRestrictedResources: false);
                game_0.Galaxy.Empires[1].DiplomaticRelations.Add(diplomaticRelation2);
            }
            else if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
            {
                diplomaticRelation.Type = DiplomaticRelationType.None;
                DiplomaticRelation diplomaticRelation3 = game_0.Galaxy.Empires[1].DiplomaticRelations[game_0.PlayerEmpire];
                if (diplomaticRelation3 != null)
                {
                    diplomaticRelation3.Type = DiplomaticRelationType.None;
                }
                else
                {
                    diplomaticRelation3 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], game_0.PlayerEmpire, tradeRestrictedResources: false);
                    game_0.Galaxy.Empires[1].DiplomaticRelations.Add(diplomaticRelation3);
                }
            }
            diplomaticRelation = game_0.PlayerEmpire.DiplomaticRelations[game_0.Galaxy.Empires[2]];
            if (diplomaticRelation == null)
            {
                diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], tradeRestrictedResources: false);
                game_0.PlayerEmpire.DiplomaticRelations.Add(diplomaticRelation);
                DiplomaticRelation diplomaticRelation4 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], game_0.PlayerEmpire, tradeRestrictedResources: false);
                game_0.Galaxy.Empires[2].DiplomaticRelations.Add(diplomaticRelation4);
            }
            else if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
            {
                diplomaticRelation.Type = DiplomaticRelationType.None;
                DiplomaticRelation diplomaticRelation5 = game_0.Galaxy.Empires[2].DiplomaticRelations[game_0.PlayerEmpire];
                if (diplomaticRelation5 != null)
                {
                    diplomaticRelation5.Type = DiplomaticRelationType.None;
                }
                else
                {
                    diplomaticRelation5 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], game_0.PlayerEmpire, tradeRestrictedResources: false);
                    game_0.Galaxy.Empires[2].DiplomaticRelations.Add(diplomaticRelation5);
                }
            }
            method_77(game_0);
        }

        private void lnkTutorialExpansionDiplomacy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "ExpansionDiplomacy";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.3;
            string string_ = TextResolver.GetText("Expanding");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 == null)
            {
                return;
            }
            Habitat capital = game_0.PlayerEmpire.Capital;
            ShipGroup shipGroup = new ShipGroup(game_0.Galaxy);
            shipGroup.Name = string.Format(TextResolver.GetText("Nth Fleet"), game_0.PlayerEmpire.GetNextFleetNumberDescription());
            shipGroup.Empire = game_0.PlayerEmpire;
            shipGroup.GatherPoint = capital;
            Design design = game_0.PlayerEmpire.Designs.FindNewest(BuiltObjectSubRole.Frigate);
            Design design2 = game_0.PlayerEmpire.Designs.FindNewest(BuiltObjectSubRole.Destroyer);
            BuiltObjectList items = new BuiltObjectList();
            for (int j = 0; j < 5; j++)
            {
                double num11 = 0.0;
                double num12 = 0.0;
                design.BuildCount++;
                BuiltObject builtObject = game_0.PlayerEmpire.GenerateBuiltObjectFromDesign(design, game_0.Galaxy.GenerateBuiltObjectName(design), isState: true, capital.Xpos + 50.0, capital.Ypos + 100.0);
                builtObject.ParentHabitat = capital;
                builtObject.DateBuilt = game_0.Galaxy.CurrentStarDate;
                builtObject.DateRetrofit = game_0.Galaxy.CurrentStarDate;
                game_0.Galaxy.SelectRelativeParkingPoint(out num11, out num12);
                builtObject.ParentOffsetX = num11;
                builtObject.ParentOffsetY = num12;
                builtObject.Xpos = capital.Xpos + num11;
                builtObject.Ypos = capital.Ypos + num12;
                builtObject.Heading = game_0.Galaxy.SelectRandomHeading();
                builtObject.CurrentFuel = builtObject.FuelCapacity;
                builtObject.CurrentEnergy = builtObject.ReactorStorageCapacity;
                builtObject.CurrentShields = builtObject.ShieldsCapacity;
                shipGroup.AddShipToFleet(builtObject);
            }
            for (int k = 0; k < 3; k++)
            {
                double num13 = 0.0;
                double num14 = 0.0;
                design2.BuildCount++;
                BuiltObject builtObject2 = game_0.PlayerEmpire.GenerateBuiltObjectFromDesign(design2, game_0.Galaxy.GenerateBuiltObjectName(design2), isState: true, capital.Xpos + 50.0, capital.Ypos + 100.0);
                builtObject2.ParentHabitat = capital;
                builtObject2.DateBuilt = game_0.Galaxy.CurrentStarDate;
                builtObject2.DateRetrofit = game_0.Galaxy.CurrentStarDate;
                game_0.Galaxy.SelectRelativeParkingPoint(out num13, out num14);
                builtObject2.ParentOffsetX = num13;
                builtObject2.ParentOffsetY = num14;
                builtObject2.Xpos = capital.Xpos + num13;
                builtObject2.Ypos = capital.Ypos + num14;
                builtObject2.Heading = game_0.Galaxy.SelectRandomHeading();
                builtObject2.CurrentFuel = builtObject2.FuelCapacity;
                builtObject2.CurrentEnergy = builtObject2.ReactorStorageCapacity;
                builtObject2.CurrentShields = builtObject2.ShieldsCapacity;
                shipGroup.AddShipToFleet(builtObject2);
            }
            shipGroup.Ships.AddRange(items);
            shipGroup.Update();
            game_0.PlayerEmpire.ShipGroups.Add(shipGroup);
            for (int l = 0; l < 2; l++)
            {
                bool isRandomCharacter = false;
                game_0.PlayerEmpire.GenerateNewCharacter(CharacterRole.IntelligenceAgent, game_0.PlayerEmpire.Capital, out isRandomCharacter);
            }
            DiplomaticRelation diplomaticRelation = game_0.PlayerEmpire.DiplomaticRelations[game_0.Galaxy.Empires[1]];
            if (diplomaticRelation == null)
            {
                diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], tradeRestrictedResources: false);
                game_0.PlayerEmpire.DiplomaticRelations.Add(diplomaticRelation);
                DiplomaticRelation diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], game_0.PlayerEmpire, tradeRestrictedResources: false);
                game_0.Galaxy.Empires[1].DiplomaticRelations.Add(diplomaticRelation2);
            }
            else if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
            {
                diplomaticRelation.Type = DiplomaticRelationType.None;
                DiplomaticRelation diplomaticRelation3 = game_0.Galaxy.Empires[1].DiplomaticRelations[game_0.PlayerEmpire];
                if (diplomaticRelation3 != null)
                {
                    diplomaticRelation3.Type = DiplomaticRelationType.None;
                }
                else
                {
                    diplomaticRelation3 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], game_0.PlayerEmpire, tradeRestrictedResources: false);
                    game_0.Galaxy.Empires[1].DiplomaticRelations.Add(diplomaticRelation3);
                }
            }
            diplomaticRelation = game_0.PlayerEmpire.DiplomaticRelations[game_0.Galaxy.Empires[2]];
            if (diplomaticRelation == null)
            {
                diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], tradeRestrictedResources: false);
                game_0.PlayerEmpire.DiplomaticRelations.Add(diplomaticRelation);
                DiplomaticRelation diplomaticRelation4 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], game_0.PlayerEmpire, tradeRestrictedResources: false);
                game_0.Galaxy.Empires[2].DiplomaticRelations.Add(diplomaticRelation4);
            }
            else if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
            {
                diplomaticRelation.Type = DiplomaticRelationType.None;
                DiplomaticRelation diplomaticRelation5 = game_0.Galaxy.Empires[2].DiplomaticRelations[game_0.PlayerEmpire];
                if (diplomaticRelation5 != null)
                {
                    diplomaticRelation5.Type = DiplomaticRelationType.None;
                }
                else
                {
                    diplomaticRelation5 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], game_0.PlayerEmpire, tradeRestrictedResources: false);
                    game_0.Galaxy.Empires[2].DiplomaticRelations.Add(diplomaticRelation5);
                }
            }
            method_77(game_0);
        }

        private void btnTutorialStartCancel_Click(object sender, EventArgs e)
        {
            method_120();
        }

        private void method_124(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 19;
        }

        private void method_125(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Galaxy") + ": " + TextResolver.GetText("Creatures"), TextResolver.GetText("Space creatures are distributed throughout the galaxy at the start of the game"));
        }

        private void btnEncyclopediaBack_Click(object sender, EventArgs e)
        {
            if (int_0 > 0)
            {
                int_0--;
                method_129(encyclopediaItemList_1[int_0]);
            }
            method_126();
        }

        private void btnEncyclopediaForward_Click(object sender, EventArgs e)
        {
            if (int_0 < encyclopediaItemList_1.Count - 1)
            {
                int_0++;
                method_129(encyclopediaItemList_1[int_0]);
            }
            method_126();
        }

        private void btnEncyclopediaHome_Click(object sender, EventArgs e)
        {
            method_129(null);
        }

        private void method_126()
        {
            if (int_0 < encyclopediaItemList_1.Count - 1)
            {
                btnEncyclopediaForward.Enabled = true;
            }
            else
            {
                btnEncyclopediaForward.Enabled = false;
            }
            if (int_0 > 0)
            {
                btnEncyclopediaBack.Enabled = true;
            }
            else
            {
                btnEncyclopediaBack.Enabled = false;
            }
        }

        private void method_127(string string_2)
        {
            encyclopediaItemList_1.Clear();
            int_0 = -1;
            pnlEncyclopedia.Size = new Size(995, 730);
            pnlEncyclopedia.Location = new Point((base.Width - pnlEncyclopedia.Width) / 2, (base.Height - pnlEncyclopedia.Height) / 2);
            pnlEncyclopedia.DoLayout();
            btnEncyclopediaBack.Location = new Point(730, 10);
            btnEncyclopediaForward.Location = new Point(775, 10);
            btnEncyclopediaHome.Location = new Point(930, 10);
            webEncyclopediaContent.Size = new Size(705, 505);
            webEncyclopediaContent.Location = new Point(10, 10);
            pnlEncyclopediaTopics.Size = new Size(240, 579);
            pnlEncyclopediaTopics.Location = new Point(730, 48);
            pnlEncyclopediaTopics.BindData(encyclopediaItemList_0);
            pnlEncyclopediaRelatedItems.Size = new Size(705, 100);
            pnlEncyclopediaRelatedItems.Location = new Point(10, 527);
            pnlEncyclopediaRelatedItems.KickStart(this);
            chkEncyclopediaShowAtStart.Location = new Point(10, 638);
            if (main_0 != null && main_0.gameOptions_0 != null)
            {
                if (main_0.gameOptions_0.ShowEncyclopediaAtStart)
                {
                    chkEncyclopediaShowAtStart.Checked = true;
                }
                else
                {
                    chkEncyclopediaShowAtStart.Checked = false;
                }
            }
            EncyclopediaItem encyclopediaItem = encyclopediaItemList_0[string_2];
            if (encyclopediaItem != null)
            {
                encyclopediaItemList_1.Add(encyclopediaItem);
                int_0++;
            }
            method_129(encyclopediaItem);
            method_126();
            pnlEncyclopedia.Visible = true;
            pnlEncyclopedia.BringToFront();
        }

        private void method_128()
        {
            if (main_0 != null && main_0.gameOptions_0 != null)
            {
                main_0.gameOptions_0.ShowEncyclopediaAtStart = chkEncyclopediaShowAtStart.Checked;
            }
            pnlEncyclopedia.Visible = false;
            pnlEncyclopedia.SendToBack();
        }

        private void method_129(EncyclopediaItem encyclopediaItem_0)
        {
            encyclopediaItem_0 = method_130(encyclopediaItem_0);
            method_136(encyclopediaItem_0);
            method_137(encyclopediaItem_0);
        }

        private EncyclopediaItem method_130(EncyclopediaItem encyclopediaItem_0)
        {
            webEncyclopediaContent.AllowWebBrowserDrop = false;
            //webEncyclopediaContent.ContextMenu = null;
            webEncyclopediaContent.ContextMenuStrip = null;
            webEncyclopediaContent.IsWebBrowserContextMenuEnabled = false;
            webEncyclopediaContent.WebBrowserShortcutsEnabled = false;
            if (encyclopediaItem_0 != null)
            {
                pnlEncyclopedia.HeaderTitle = TextResolver.GetText("Galactopedia") + ": " + encyclopediaItem_0.Title;
                string text = Application.StartupPath + "\\help\\";
                string text2 = string.Empty;
                if (!string.IsNullOrEmpty(main_0.string_3) && main_0.string_3.ToLower(CultureInfo.InvariantCulture) != "(default)")
                {
                    text2 = Application.StartupPath + "\\Customization\\" + main_0.string_3 + "\\help\\";
                }
                string text3 = text2 + encyclopediaItem_0.Filename;
                if (!File.Exists(text3))
                {
                    text3 = text + encyclopediaItem_0.Filename;
                    if (!File.Exists(text3))
                    {
                        text3 = text + "DE_" + encyclopediaItem_0.Filename;
                        if (!File.Exists(text3))
                        {
                            text3 = text + "FR_" + encyclopediaItem_0.Filename;
                            if (!File.Exists(text3))
                            {
                                text3 = text + "ES_" + encyclopediaItem_0.Filename;
                            }
                        }
                    }
                }
                if (File.Exists(text3))
                {
                    if (encyclopediaItem_0.Category == EncyclopediaCategory.Races && encyclopediaItem_0.Title != TextResolver.GetText("Alien Races"))
                    {
                        Race race_ = raceList_1[encyclopediaItem_0.Title];
                        string value = method_131(race_);
                        string text4 = File.ReadAllText(text3, Encoding.Default);
                        int num = text4.ToLower(CultureInfo.InvariantCulture).LastIndexOf("</body>");
                        if (num >= 0)
                        {
                            text4 = text4.Insert(num, value);
                        }
                        string value2 = "<meta http-equiv=3DContent-Type";
                        int num2 = text4.IndexOf(value2);
                        if (num2 >= 0)
                        {
                            int num3 = text4.IndexOf(">", num2 + 1);
                            if (num3 >= 0)
                            {
                                text4 = text4.Remove(num2, num3 - num2 + 1);
                            }
                        }
                        string text5 = "Nav.mht";
                        File.WriteAllText(Application.UserAppDataPath + "\\" + text5, text4, Encoding.Default);
                        webEncyclopediaContent.Refresh(WebBrowserRefreshOption.Completely);
                        webEncyclopediaContent.Navigate2(Application.UserAppDataPath + "\\" + text5);
                        webEncyclopediaContent.Refresh(WebBrowserRefreshOption.Completely);
                        webEncyclopediaContent.Document.Encoding = "utf-8";
                    }
                    else if (encyclopediaItem_0.Category == EncyclopediaCategory.Resources && encyclopediaItem_0.Title == TextResolver.GetText("Resources Visual Index"))
                    {
                        string text6 = "href=3D" + '"' + "file:///";
                        string value3 = '"' + ">";
                        string text7 = Application.StartupPath + "\\Help";
                        string text8 = File.ReadAllText(text3, Encoding.Default);
                        int num4 = 0;
                        for (num4 = text8.IndexOf(text6, 0); num4 >= 0; num4 = text8.IndexOf(text6, num4 + 1))
                        {
                            int num5 = text8.IndexOf(value3, num4);
                            int num6 = text8.LastIndexOf("\\", num5);
                            string text9 = text8.Substring(num6, num5 - num6);
                            text8 = text8.Remove(num4, num5 - num4);
                            text8 = text8.Insert(num4, text6 + text7 + text9);
                        }
                        string value4 = "<meta http-equiv=3DContent-Type";
                        int num7 = text8.IndexOf(value4);
                        if (num7 >= 0)
                        {
                            int num8 = text8.IndexOf(">", num7 + 1);
                            if (num8 >= 0)
                            {
                                text8 = text8.Remove(num7, num8 - num7 + 1);
                            }
                        }
                        string text10 = "Nav.mht";
                        File.WriteAllText(Application.UserAppDataPath + text10, text8, Encoding.Default);
                        webEncyclopediaContent.Refresh(WebBrowserRefreshOption.Completely);
                        webEncyclopediaContent.Navigate2(Application.UserAppDataPath + text10);
                        webEncyclopediaContent.Refresh(WebBrowserRefreshOption.Completely);
                        webEncyclopediaContent.Document.Encoding = "utf-8";
                    }
                    else {
                        webEncyclopediaContent.Navigate2(text3);
                        //webEncyclopediaContent.Navigate(text3);
                    }
                }
                else
                {
                    string text11 = Application.StartupPath + "\\help\\default.mht";
                    if (!File.Exists(text11))
                    {
                        text11 = Application.StartupPath + "\\help\\DE_default.mht";
                        if (!File.Exists(text11))
                        {
                            text11 = Application.StartupPath + "\\help\\FR_default.mht";
                            if (!File.Exists(text11))
                            {
                                text11 = Application.StartupPath + "\\help\\ES_default.mht";
                            }
                        }
                    }
                    webEncyclopediaContent.Navigate2(text11);
                }
            }
            else
            {
                bool flag = false;
                if (!string.IsNullOrEmpty(main_0.string_3))
                {
                    string title = string.Format(TextResolver.GetText("THEMENAME Theme"), main_0.string_3);
                    encyclopediaItem_0 = encyclopediaItemList_0[title];
                    if (encyclopediaItem_0 != null)
                    {
                        encyclopediaItem_0 = method_130(encyclopediaItem_0);
                        flag = true;
                    }
                }
                if (!flag)
                {
                    string text12 = Application.StartupPath + "\\help\\default.mht";
                    if (!File.Exists(text12))
                    {
                        text12 = Application.StartupPath + "\\help\\DE_default.mht";
                        if (!File.Exists(text12))
                        {
                            text12 = Application.StartupPath + "\\help\\FR_default.mht";
                            if (!File.Exists(text12))
                            {
                                text12 = Application.StartupPath + "\\help\\ES_default.mht";
                            }
                        }
                    }
                    webEncyclopediaContent.Navigate2(text12);
                    pnlEncyclopedia.HeaderTitle = TextResolver.GetText("Galactopedia") + ": " + TextResolver.GetText("Introduction");
                }
            }
            pnlEncyclopedia.Invalidate();
            return encyclopediaItem_0;
        }

        private string method_131(Race race_0)
        {
            string text = string.Empty;
            if (race_0 != null)
            {
                RaceSummary raceSummary = Galaxy.GenerateRaceSummary(race_0);
                string text2 = "<span lang=3DEN-NZ style=3D'font-size:12.0pt;font-family:\"Verdana\",\"sans-serif\";mso-bidi-font-family:Verdana;color:#aaaaaa;mso-ansi-language:EN-NZ'>";
                string text3 = "</span>";
                for (int i = 0; i < raceSummary.Sections.Count; i++)
                {
                    RaceSummarySection raceSummarySection = raceSummary.Sections[i];
                    text += "<DIV class=3DSection1>";
                    if (!string.IsNullOrEmpty(raceSummarySection.Heading))
                    {
                        string text4 = text;
                        text = text4 + text2 + "<B>" + raceSummarySection.Heading + "</B>" + text3;
                    }
                    text += "<UL class=3DMsoNormal style=3D'margin-top:0cm' type=3Ddisc>";
                    for (int j = 0; j < raceSummarySection.Items.Count; j++)
                    {
                        text = text + "<LI style=3D'font-size:10.0pt;font-family:\"Verdana\",\"sans-serif\";color:#aaaaaa;mso-list:l1 level1 lfo4;tab-stops:list 10.0pt'>" + raceSummarySection.Items[j] + "</LI>";
                    }
                    text += "</UL>";
                    text += "</DIV>";
                }
            }
            return text;
        }

        private List<string> method_132(Race race_0)
        {
            List<string> list = new List<string>();
            string empty = string.Empty;
            empty = TextResolver.GetText("Default Reproduction Rate") + ": " + (race_0.ReproductiveRate - 1.0).ToString("#0%");
            list.Add(empty);
            string text = method_133(race_0.IntelligenceLevel);
            if (text == TextResolver.GetText("Slightly") && race_0.IntelligenceLevel >= 100)
            {
                text = TextResolver.GetText("Moderately");
            }
            empty = string.Format(arg1: (race_0.IntelligenceLevel < 100) ? TextResolver.GetText("Stupid") : TextResolver.GetText("Intelligent"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_0.IntelligenceLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            text = method_133(race_0.AggressionLevel);
            empty = string.Format(arg1: (race_0.AggressionLevel < 100) ? TextResolver.GetText("Passive") : TextResolver.GetText("Aggressive"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_0.AggressionLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            text = method_133(race_0.CautionLevel);
            empty = string.Format(arg1: (race_0.CautionLevel < 100) ? TextResolver.GetText("Reckless") : TextResolver.GetText("Cautious"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_0.CautionLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            text = method_133(race_0.FriendlinessLevel);
            empty = string.Format(arg1: (race_0.FriendlinessLevel < 100) ? TextResolver.GetText("Unfriendly") : TextResolver.GetText("Friendly"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_0.FriendlinessLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            text = method_133(race_0.LoyaltyLevel);
            empty = string.Format(arg1: (race_0.LoyaltyLevel < 100) ? TextResolver.GetText("Unreliable") : TextResolver.GetText("Dependable"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_0.LoyaltyLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            return list;
        }

        private string method_133(int int_1)
        {
            string empty = string.Empty;
            int_1 -= 100;
            int_1 = Math.Abs(int_1);
            if (int_1 >= 30)
            {
                empty = TextResolver.GetText("Extremely");
            }
            else if (int_1 >= 17)
            {
                empty = TextResolver.GetText("Very");
            }
            else if (int_1 >= 6)
            {
                empty = TextResolver.GetText("Quite");
            }
            else if (int_1 < 6)
            {
                empty = TextResolver.GetText("Slightly");
            }
            return empty;
        }

        internal void method_134(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            object linkData = e.Link.LinkData;
            if (!(linkData is EncyclopediaItem))
            {
                return;
            }
            EncyclopediaItem encyclopediaItem = (EncyclopediaItem)linkData;
            method_129(encyclopediaItem);
            if (int_0 < encyclopediaItemList_1.Count - 1)
            {
                int_0++;
                encyclopediaItemList_1[int_0] = encyclopediaItem;
                if (int_0 < encyclopediaItemList_1.Count - 1)
                {
                    encyclopediaItemList_1.RemoveRange(int_0 + 1, encyclopediaItemList_1.Count - (int_0 + 1));
                }
            }
            else
            {
                encyclopediaItemList_1.Add(encyclopediaItem);
                int_0++;
            }
            method_126();
        }

        private void method_135(object sender, EncyclopediaItemChangedEventArgs e)
        {
            EncyclopediaItem item = e.Item;
            if (item == null)
            {
                return;
            }
            method_130(item);
            method_136(item);
            method_137(item);
            if (int_0 < encyclopediaItemList_1.Count - 1)
            {
                int_0++;
                encyclopediaItemList_1[int_0] = item;
                if (int_0 < encyclopediaItemList_1.Count - 1)
                {
                    encyclopediaItemList_1.RemoveRange(int_0 + 1, encyclopediaItemList_1.Count - (int_0 + 1));
                }
            }
            else
            {
                encyclopediaItemList_1.Add(item);
                int_0++;
            }
            method_126();
        }

        private void method_136(EncyclopediaItem encyclopediaItem_0)
        {
            if (encyclopediaItem_0 != null)
            {
                pnlEncyclopediaRelatedItems.Items = encyclopediaItem_0.RelatedItems;
            }
            else
            {
                pnlEncyclopediaRelatedItems.Items = null;
            }
        }

        private void method_137(EncyclopediaItem encyclopediaItem_0)
        {
            if (encyclopediaItem_0 != null)
            {
                pnlEncyclopediaTopics.OnEncyclopediaItemSelected -= method_135;
                pnlEncyclopediaTopics.SetSelectedItem(encyclopediaItem_0);
                pnlEncyclopediaTopics.OnEncyclopediaItemSelected += method_135;
            }
            else
            {
                pnlEncyclopediaTopics.OnEncyclopediaItemSelected -= method_135;
                pnlEncyclopediaTopics.SetSelectedItem(null);
                pnlEncyclopediaTopics.CollapseAll();
                pnlEncyclopediaTopics.OnEncyclopediaItemSelected += method_135;
            }
        }

        private void Start_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (((Control)(object)mediaPlayer).Visible)
            {
                return;
            }*/
            if (pnlEncyclopedia.Visible && e.KeyCode != Keys.F1)
            {
                e.Handled = false;
                return;
            }
            Keys keyCode = e.KeyCode;
            if (keyCode == Keys.F1)
            {
                string empty = string.Empty;
                if (pnlNewGame.Visible)
                {
                    empty = TextResolver.GetText("Start New Game Screen");
                }
                else if (pnlQuickStart.Visible)
                {
                    empty = TextResolver.GetText("Quick Start Screen");
                }
                else if (pnlThemes.Visible)
                {
                    empty = TextResolver.GetText("Change Theme Screen");
                }
                else if (pnlGameOptionsAdvancedDisplaySettings.Visible)
                {
                    empty = TextResolver.GetText("Game Options - Advanced Display Settings Screen");
                }
                else if (pnlGameOptionsEmpireSettings.Visible)
                {
                    empty = TextResolver.GetText("Game Options - Your Empire Settings Screen");
                }
                else if (pnlGameOptions.Visible)
                {
                    empty = TextResolver.GetText("Game Options");
                }
                else if (pnlEncyclopedia.Visible)
                {
                    method_128();
                }
                else
                {
                    empty = string.Empty;
                }
                if (string.IsNullOrEmpty(empty))
                {
                    empty = TextResolver.GetText("Introduction");
                }
                method_127(empty);
            }
        }

        private void lnkGalactopedia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_127("");
        }

        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            if (!pnlAboutCredits.Visible)
            {
                method_138();
            }
        }

        private void method_138()
        {
            pnlAbout.Size = new Size(360, 530);
            pnlAbout.Location = new Point((base.Width - pnlAbout.Width) / 2, (base.Height - pnlAbout.Height) / 2);
            picAbout.Visible = false;
            upEzpZsgAK.Visible = false;
            lblAboutTitle.Visible = false;
            pnlAboutCredits.Size = new Size(330, base.Height);
            pnlAboutCredits.Location = new Point(base.Width - 350, 0);
            method_139();
            pnlAboutCredits.ScrollSpeed = 30.0;
            pnlAboutCredits.SetScrollPosition(0.0);
            pnlAboutCredits.StartScroll();
            Bitmap desktopImage = GetDesktopImage();
            Bitmap bitmap = new Bitmap(pnlAboutCredits.Width, pnlAboutCredits.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Black);
                Rectangle srcRect = new Rectangle(pnlAboutCredits.Location, pnlAboutCredits.Size);
                Rectangle destRect = new Rectangle(0, 0, pnlAboutCredits.Width, pnlAboutCredits.Height);
                graphics.DrawImage(desktopImage, destRect, srcRect, GraphicsUnit.Pixel);
            }
            pnlAboutCredits.BackgroundImage = bitmap;
            pnlAboutCredits.Parent = this;
            btnAboutClose.Parent = pnlAboutCredits;
            btnAboutClose.Font = font_7;
            btnAboutClose.Text = TextResolver.GetText("Close Credits");
            btnAboutClose.Size = new Size(300, 35);
            btnAboutClose.Location = new Point((pnlAboutCredits.Width - 300) / 2, pnlAboutCredits.Height - 55);
            btnAboutClose.Visible = true;
            pnlAboutCredits.BringToFront();
            pnlAboutCredits.Visible = true;
            btnAboutClose.BringToFront();
        }

        public static Bitmap GetDesktopImage()
        {
            IntPtr dC = User32.GetDC(User32.GetDesktopWindow());
            IntPtr intPtr = (IntPtr)Gdi32.CreateCompatibleDC((int)dC);
            SIZE sIZE = default(SIZE);
            sIZE.cx = User32.GetSystemMetrics(0);
            sIZE.cy = User32.GetSystemMetrics(1);
            IntPtr intPtr2 = (IntPtr)Gdi32.CreateCompatibleBitmap((int)dC, sIZE.cx, sIZE.cy);
            if (intPtr2 != IntPtr.Zero)
            {
                IntPtr intPtr3 = (IntPtr)Gdi32.SelectObject((int)intPtr, (int)intPtr2);
                Gdi32.BitBlt((int)intPtr, 0, 0, sIZE.cx, sIZE.cy, (int)dC, 0, 0, 13369376u);
                Gdi32.SelectObject((int)intPtr, (int)intPtr3);
                Gdi32.DeleteDC((int)intPtr);
                User32.ReleaseDC(User32.GetDesktopWindow(), dC);
                Bitmap result = Image.FromHbitmap(intPtr2);
                Gdi32.DeleteObject((int)intPtr2);
                GC.Collect();
                return result;
            }
            return null;
        }

        private void method_139()
        {
            pnlAboutCredits.ClearAll();
            pnlAboutCredits.DefaultFont = font_7;
            int num = base.Height / 20;
            for (int i = 0; i < num; i++)
            {
                pnlAboutCredits.AddSpacer();
            }
            pnlAboutCredits.AddImage(bitmap_1);
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Design && Development"));
            pnlAboutCredits.AddText("ELLIOT GIBBS");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Art Designer"));
            pnlAboutCredits.AddText("JASON BARISH");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Additional Artwork"));
            pnlAboutCredits.AddText("RICHARD EVANS");
            pnlAboutCredits.AddText("PETR JACH");
            pnlAboutCredits.AddText("MARTIN WOOD");
            pnlAboutCredits.AddText("MARC VON MARTIAL");
            pnlAboutCredits.AddText("ELLIOT GIBBS");
            pnlAboutCredits.AddText("THE LORDZ GAME STUDIO");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Concept Reviewer"));
            pnlAboutCredits.AddText("CHITOSE GIBBS");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Copyright Only") + " ©2014");
            pnlAboutCredits.AddText("CODEFORCE LIMITED");
            pnlAboutCredits.AddImage(bitmap_2);
            pnlAboutCredits.AddText("www.codeforce.co.nz");
            pnlAboutCredits.AddText(TextResolver.GetText("All rights reserved"));
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("With special thanks to"));
            pnlAboutCredits.AddText("Chitose, Natasha, Jessica and Benjamin");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText("SLITHERINE GROUP");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("PRODUCERS"));
            pnlAboutCredits.AddText("Erik Rutins");
            pnlAboutCredits.AddText("JD McNeil");
            pnlAboutCredits.AddText("Iain McNeil");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("ADDITIONAL DESIGN"));
            pnlAboutCredits.AddText("Erik Rutins");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("BOX ART"));
            pnlAboutCredits.AddText("Gunaars Miezis");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("BOX AND LOGO DESIGN"));
            pnlAboutCredits.AddText("Myriam Bell");
            pnlAboutCredits.AddText("Marc von Martial");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("MANUAL EDITING AND CONTENT"));
            pnlAboutCredits.AddText("Erik Rutins");
            pnlAboutCredits.AddText("Elliot Gibbs");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("MANUAL DESIGN AND LAYOUT"));
            pnlAboutCredits.AddText("Myriam Bell");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("GRAPHIC ARTISTS"));
            pnlAboutCredits.AddText("Richard Evans");
            pnlAboutCredits.AddText("Petr Jach");
            pnlAboutCredits.AddText("Marc von Martial");
            pnlAboutCredits.AddText("Martin Wood");
            pnlAboutCredits.AddText("The Lordz Game Studio");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("PUBLIC RELATIONS && MARKETING"));
            pnlAboutCredits.AddText("Marco Minoli");
            pnlAboutCredits.AddText("Filippo Chianetta");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("PRODUCTION ASSISTANTS"));
            pnlAboutCredits.AddText("Andrew Loveridge");
            pnlAboutCredits.AddText("Gerry Edwards");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("ADMINISTRATION"));
            pnlAboutCredits.AddText("Liz Stoltz");
            pnlAboutCredits.AddText("Dean Walker");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("BETA TEST COORDINATION"));
            pnlAboutCredits.AddText("Karlis Rutins");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("CUSTOMER SUPPORT STAFF"));
            pnlAboutCredits.AddText("Christian Bassani");
            pnlAboutCredits.AddText("Paulo Costa");
            pnlAboutCredits.AddText("Andrew Loveridge");
            pnlAboutCredits.AddText("Erik Rutins");
            pnlAboutCredits.AddText("Iain McNeil");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("FORUM ADMINISTRATION"));
            pnlAboutCredits.AddText("Erik Rutins");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("TECHNICAL DIRECTOR"));
            pnlAboutCredits.AddText("Phil Veale");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("WEB DATABASE DESIGN && DEVELOPMENT"));
            pnlAboutCredits.AddText("Andrea Nicola");
            pnlAboutCredits.AddText("Valery Vidershpan");
            pnlAboutCredits.AddText("Phil Veale");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("NETWORK AND SYSTEM ADMINISTRATOR"));
            pnlAboutCredits.AddText("Valery Vidershpan");
            pnlAboutCredits.AddText("Andrea Nicola");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("QUALITY ASSURANCE LEAD"));
            pnlAboutCredits.AddText("Erik Rutins");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("TESTING TEAM"));
            pnlAboutCredits.AddText("DISTANT WORLDS - UNIVERSE");
            pnlAboutCredits.AddText("A. S. Boles");
            pnlAboutCredits.AddText("Aldo Malago");
            pnlAboutCredits.AddText("Antiscamp");
            pnlAboutCredits.AddText("Apox");
            pnlAboutCredits.AddText("Arkblade");
            pnlAboutCredits.AddText("BigWolf");
            pnlAboutCredits.AddText("Bingeling");
            pnlAboutCredits.AddText("blackraider");
            pnlAboutCredits.AddText("Chet Guiles");
            pnlAboutCredits.AddText("clone61279");
            pnlAboutCredits.AddText("Doink9731");
            pnlAboutCredits.AddText("DumDum2002");
            pnlAboutCredits.AddText("Edward Sumrell");
            pnlAboutCredits.AddText("elanaagain");
            pnlAboutCredits.AddText("fireball60");
            pnlAboutCredits.AddText("General Patton");
            pnlAboutCredits.AddText("hadberz");
            pnlAboutCredits.AddText("Haree78");
            pnlAboutCredits.AddText("Iwbtone");
            pnlAboutCredits.AddText("Jeeves");
            pnlAboutCredits.AddText("KEBW1144");
            pnlAboutCredits.AddText("Kevin Richardson");
            pnlAboutCredits.AddText("Larry Monte");
            pnlAboutCredits.AddText("Litjan");
            pnlAboutCredits.AddText("Malevolence");
            pnlAboutCredits.AddText("Manuel Kraft");
            pnlAboutCredits.AddText("Michael 'nim8or' Lange");
            pnlAboutCredits.AddText("Paul 'Chop' Arnold");
            pnlAboutCredits.AddText("Petri Turunen");
            pnlAboutCredits.AddText("Phil Brutton");
            pnlAboutCredits.AddText("Robin Kraak");
            pnlAboutCredits.AddText("Sithuk");
            pnlAboutCredits.AddText("sventhebold");
            pnlAboutCredits.AddText("Wadym Zasko");
            pnlAboutCredits.AddText("Xmudder");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Font typefaces by"));
            pnlAboutCredits.AddText("LARABIE FONTS");
            pnlAboutCredits.AddText("www.larabiefonts.com");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Uses compression technology by"));
            pnlAboutCredits.AddText("DotNetZip");
            pnlAboutCredits.AddText("dotnetzip.codeplex.com");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Scientific and Technical Advisor"));
            pnlAboutCredits.AddText("WIKIPEDIA");
            pnlAboutCredits.AddText("www.wikipedia.org");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("Catering by"));
            pnlAboutCredits.AddText("EIFFEL EN EDEN");
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddImage(bitmap_0);
            pnlAboutCredits.AddSpacer();
            pnlAboutCredits.AddText(TextResolver.GetText("No Giant Kaltors were harmed in the making of this game"));
        }

        private void method_140()
        {
            pnlAboutCredits.StopScroll();
            pnlAboutCredits.ClearAll();
            pnlAbout.Visible = false;
            pnlAbout.SendToBack();
            pnlAboutCredits.Visible = false;
            menuCredits.Visible = true;
        }

        private void btnAboutClose_Click(object sender, EventArgs e)
        {
            method_140();
        }

        private void method_141(string[] string_2)
        {
            /*if (mediaPlayer.settings != null)
            {
                mediaPlayer.settings.autoStart = false;
            }
            ((Control)(object)mediaPlayer).Location = new Point(0, 0);
            ((Control)(object)mediaPlayer).Size = base.Size;
            IWMPPlaylist val = mediaPlayer.newPlaylist("", "");
            foreach (string text in string_2)
            {
                if (File.Exists(text))
                {
                    val.appendItem(mediaPlayer.newMedia(text));
                }
            }
            SetErrorMode(ErrorModes.SEM_FAILCRITICALERRORS);
            mediaPlayer.currentPlaylist = val;
            mediaPlayer.uiMode = "none";
            ((Control)(object)mediaPlayer).Visible = true;
            ((Control)(object)mediaPlayer).BringToFront();
            mediaPlayer.stretchToFit = true;
            mediaPlayer.settings.volume = 100;
            mediaPlayer.Ctlcontrols.currentPosition = 0.0;
            if (mediaPlayer.Ctlcontrols != null)
            {
                while (!mediaPlayer.Ctlcontrols.get_isAvailable("play"))
                {
                    Thread.Sleep(200);
                }
                mediaPlayer.Ctlcontrols.play();
            }*/
        }

        /*private void mediaPlayer_MediaError(object object_0, _WMPOCXEvents_MediaErrorEvent _WMPOCXEvents_MediaErrorEvent_0)
        {
            method_143();
        }*/

        private void method_142(bool bool_5)
        {
            lnkAbout.Enabled = bool_5;
            lnkCheckForUpdates.Enabled = bool_5;
            lnkCopyright.Enabled = bool_5;
            lnkExit.Enabled = bool_5;
            lnkGalactopedia.Enabled = bool_5;
            lnkLoadGame.Enabled = bool_5;
            lnkNewGame.Enabled = bool_5;
            lnkOptions.Enabled = bool_5;
            lnkPlayScenario.Enabled = bool_5;
            lnkTutorial.Enabled = bool_5;
            lnkThemes.Enabled = bool_5;
        }

        private void method_143()
        {
            //IL_0012: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Expected O, but got Unknown
            //IL_0029: Unknown result type (might be due to invalid IL or missing references)
            //IL_0033: Expected O, but got Unknown
            Cursor.Show();
            //this.mediaPlayer.PlayStateChange -= new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.mediaPlayer_PlayStateChange);
            //this.mediaPlayer.MediaError -= new AxWMPLib._WMPOCXEvents_MediaErrorEventHandler(this.mediaPlayer_MediaError);
            //mediaPlayer.Ctlcontrols.stop();
            /*((Control)(object)mediaPlayer).Visible = false;
            ((Control)(object)mediaPlayer).SendToBack();*/
            SetErrorMode(ErrorModes.SYSTEM_DEFAULT);
            main_0.method_262(main_0.gameOptions_0);
            main_0.MusicPlayer.StartTheme();
            //((Control)(object)mediaPlayer).SendToBack();
            BringToFront();
            method_142(bool_5: true);
        }

        /*private void mediaPlayer_PlayStateChange(object object_0, _WMPOCXEvents_PlayStateChangeEvent _WMPOCXEvents_PlayStateChangeEvent_0)
        {
            switch (_WMPOCXEvents_PlayStateChangeEvent_0.newState)
            {
                case 1:
                case 2:
                case 8:
                    if (mediaPlayer.currentMedia != null && mediaPlayer.currentMedia.sourceURL == string_0[string_0.Length - 1])
                    {
                        method_143();
                    }
                    break;
                case 0:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 9:
                case 10:
                case 11:
                case 12:
                    break;
            }
        }*/

        /*private void mediaPlayer_ClickEvent(object object_0, _WMPOCXEvents_ClickEvent _WMPOCXEvents_ClickEvent_0)
        {
        }

        private void mediaPlayer_MouseDownEvent(object object_0, _WMPOCXEvents_MouseDownEvent _WMPOCXEvents_MouseDownEvent_0)
        {
            method_143();
        }*/

        /*private void method_144()
        {
            //IL_000d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Expected O, but got Unknown
            //IL_0024: Unknown result type (might be due to invalid IL or missing references)
            //IL_002e: Expected O, but got Unknown
            //IL_00fe: Unknown result type (might be due to invalid IL or missing references)
            //IL_0108: Expected O, but got Unknown
            //IL_0115: Unknown result type (might be due to invalid IL or missing references)
            //IL_011f: Expected O, but got Unknown
            this.mediaPlayer.PlayStateChange -= new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.mediaPlayer_PlayStateChange);
            this.mediaPlayer.MediaError -= new AxWMPLib._WMPOCXEvents_MediaErrorEventHandler(this.mediaPlayer_MediaError);
            try
            {
                if (mediaPlayer.currentMedia != null)
                {
                    mediaPlayer.Ctlcontrols.next();
                }
            }
            catch (Exception ex)
            {
                string text = "";
                try
                {
                    text = text + "Current position: " + mediaPlayer.Ctlcontrols.currentPosition + "\n";
                    text = text + "Current item duration: " + mediaPlayer.Ctlcontrols.currentItem.duration;
                    text = text + "Current media: " + mediaPlayer.currentMedia.sourceURL;
                }
                catch
                {
                }
                text += "\n\n";
                text += ex.ToString();
                MessageBox.Show(text, "Error while moving to next intro movie");
            }
            this.mediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.mediaPlayer_PlayStateChange);
            this.mediaPlayer.MediaError += new AxWMPLib._WMPOCXEvents_MediaErrorEventHandler(this.mediaPlayer_MediaError);
        }

        private void mediaPlayer_KeyDownEvent(object object_0, _WMPOCXEvents_KeyDownEvent _WMPOCXEvents_KeyDownEvent_0)
        {
        }

        private void mediaPlayer_KeyPressEvent(object object_0, _WMPOCXEvents_KeyPressEvent _WMPOCXEvents_KeyPressEvent_0)
        {
        }

        private void mediaPlayer_MouseUpEvent(object object_0, _WMPOCXEvents_MouseUpEvent _WMPOCXEvents_MouseUpEvent_0)
        {
        }*/

        private void method_145(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Galaxy") + ": " + TextResolver.GetText("Pirates"), TextResolver.GetText("Pirates are distributed throughout the galaxy at the start of the game"));
        }

        private void method_146(string string_2)
        {
            lblMenuHints.BackColor = Color.FromArgb(208, 37, 35, 49);
            lblMenuHints.Font = font_8;
            lblMenuHints.ForeColor = Color.FromArgb(194, 194, 194);
            int num = (int)(250f * float_1);
            int num2 = (int)(350f * float_1);
            lblMenuHints.MaximumSize = new Size(num, num2);
            lblMenuHints.Text = string_2;
            lblMenuHints.Visible = true;
        }

        private void method_147()
        {
            lblMenuHints.Visible = false;
        }

        private void lnkNewGame_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Start New Game") + ":\n" + TextResolver.GetText("Click here to start a fully configurable game"));
        }

        private void lnkNewGame_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void lnkTutorial_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Tutorials") + ":\n" + TextResolver.GetText("Select one of the interactive tutorials to help you learn how to play Distant Worlds"));
        }

        private void lnkTutorial_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void lnkPlayScenario_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Quick Start") + ":\n" + TextResolver.GetText("Jump straight into a preconfigured game with no setup"));
        }

        private void lnkPlayScenario_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void lnkLoadGame_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Load Game") + ":\n" + TextResolver.GetText("Load a previously saved game"));
        }

        private void lnkLoadGame_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void menuGalactopedia_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Galactopedia") + ":\n" + TextResolver.GetText("Browse the built-in galactic encyclopedia"));
        }

        private void menuGalactopedia_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void menuCheckForUpdates_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Check For Updates") + ":\n" + string.Format(TextResolver.GetText("Visit CODEFORCE to check for updates to Distant Worlds"), "www.codeforce.co.nz"));
        }

        private void menuCheckForUpdates_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void menuCredits_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Credits") + ":\n" + TextResolver.GetText("Displays the credits for Distant Worlds"));
        }

        private void menuCredits_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void lnkExit_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Exit") + ":\n" + TextResolver.GetText("Exit Distant Worlds to the Windows desktop"));
        }

        private void lnkExit_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void lnkQuickStartRaceHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            Race selectedRace = cmbQuickStartRace.SelectedRace;
            if (selectedRace != null)
            {
                method_127(selectedRace.Name);
            }
            else
            {
                method_127(TextResolver.GetText("Alien Races"));
            }
        }

        private void method_148(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            Race race = raceList_0[0];
            method_127(race.Name);
        }

        private void method_149()
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = string_1;
            soundPlayer.Load();
            if (main_0.gameOptions_0.SoundEffectsVolume > 0.0)
            {
                soundPlayer.Play();
            }
        }

        private IntPtr method_150(string string_2)
        {
            if (privateFontCollection_0 == null)
            {
                privateFontCollection_0 = new PrivateFontCollection();
            }
            Stream manifestResourceStream = GetType().Assembly.GetManifestResourceStream(string_2);
            byte[] array = new byte[manifestResourceStream.Length];
            manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
            IntPtr zero = IntPtr.Zero;
            if (array == null)
            {
            }
            GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            privateFontCollection_0.AddMemoryFont(gCHandle.AddrOfPinnedObject(), (int)manifestResourceStream.Length);
            int cnt = 0;
            zero = Gdi32.AddFontMemResourceEx(gCHandle.AddrOfPinnedObject(), (int)manifestResourceStream.Length, IntPtr.Zero, ref cnt);
            manifestResourceStream.Close();
            return zero;
        }

        Font IFontCache.GenerateFont(float pixelSize, bool isBold)
        {
            FontFamily family = privateFontCollection_0.Families[0];
            FontStyle style = FontStyle.Regular;
            if (isBold)
            {
                style = FontStyle.Bold;
            }
            return new Font(family, pixelSize, style, GraphicsUnit.Pixel);
        }

        private void method_151(Font font_11, Control control_0)
        {
            method_152(font_11, control_0, null);
        }

        private void method_152(Font font_11, Control control_0, Type type_0)
        {
            if (type_0 == null || control_0.GetType() == type_0)
            {
                control_0.Font = font_11;
            }
            if (control_0.Controls == null)
            {
                return;
            }
            foreach (Control control in control_0.Controls)
            {
                method_152(font_11, control, type_0);
            }
        }

        private void Start_FormClosed(object sender, FormClosedEventArgs e)
        {
            Gdi32.RemoveFontMemResourceEx(intptr_0);
            Gdi32.RemoveFontMemResourceEx(intptr_1);
        }

        private void pnlEncyclopedia_CloseButtonClicked(object sender, EventArgs e)
        {
            method_128();
        }

        private void pnlNewGame_CloseButtonClicked(object sender, EventArgs e)
        {
            switch (wjhRtsSwmsa)
            {
                case "CustomStandard":
                case "CustomPirate":
                    main_0.gameOptions_0.StartGameOptions = method_195();
                    break;
                default:
                    main_0.gameOptions_0.StartGameOptions = method_194();
                    break;
            }
            main_0.YxwyUefOyQ();
            main_0.method_257();
            method_46();
        }

        private void pnlQuickStart_CloseButtonClicked(object sender, EventArgs e)
        {
            method_25();
        }

        private void FtIzCrmve5_CloseButtonClicked(object sender, EventArgs e)
        {
            method_120();
        }

        private void method_153()
        {
            PopulateOptionsValues();
            pnlGameOptions.Size = new Size(700, 696);
            pnlGameOptions.Location = new Point((base.Width - pnlGameOptions.Width) / 2, (base.Height - pnlGameOptions.Height) / 2);
            pnlGameOptions.DoLayout();
            lblOptionsMainViewScrollSpeed.Font = font_1;
            lblOptionsMainViewZoomSpeed.Font = font_1;
            lblOptionsMainViewStarFieldSize.Font = font_1;
            lblOptionsMainViewStarFieldSize.BringToFront();
            
            var sliderLabelX = lblOptionsMainViewScrollSpeed.Location.X;
            var sliderLabelY = 24;
            var sliderLineHeight = 16;
            var sliderYGap = 10;
            var sliderWidth = 515;
            var sliderX = 130;
            var sliderY = 26;
            
            lblOptionsMainViewScrollSpeed.Location = new Point(sliderLabelX, sliderLabelY);
            sliderLabelY += 16 + sliderYGap;
            lblOptionsMainViewZoomSpeed.Location = new Point(sliderLabelX, sliderLabelY);
            sliderLabelY += 16 + sliderYGap;
            lblOptionsMainViewStarFieldSize.Location = new Point(sliderLabelX, sliderLabelY);
            sliderLabelY += 16 + sliderYGap;
            lblOptionsMainViewGuiScale.Location = new Point(sliderLabelX, sliderLabelY);

            sldOptionsMainViewScrollSpeed.Location = new Point(sliderX, sliderY);
            sldOptionsMainViewScrollSpeed.Size = new Size(sliderWidth, sliderLineHeight);

            sliderY += 16 + sliderYGap;
            sldOptionsMainViewZoomSpeed.Minimum = 1;
            sldOptionsMainViewZoomSpeed.Location = new Point(sliderX, sliderY);
            sldOptionsMainViewZoomSpeed.Size = new Size(sliderWidth, sliderLineHeight);
            
            sliderY += 16 + sliderYGap;
            sldOptionsMainViewStarFieldSize.Location = new Point(sliderX, sliderY);
            sldOptionsMainViewStarFieldSize.Size = new Size(sliderWidth, sliderLineHeight);
            
            sliderY += 16 + sliderYGap;
            sldOptionsMainViewGuiScale.Location = new Point(sliderX, sliderY);
            sldOptionsMainViewGuiScale.Size = new Size(sliderWidth, sliderLineHeight);
            
            sliderY += 16 + sliderYGap;
                
            // maybe move to straddling the group panel border?
            btnGameOptionsAdvancedDisplaySettings.Location = new Point(395, sliderY);
            btnGameOptionsAdvancedDisplaySettings.Size = new Size(250, 26);
            
            grpOptionsControl.Font = font_7;
            grpOptionsDisplaySettings.Font = font_7;
            grpOptionsPopupMessages.Font = font_7;
            grpOptionsScrollingMessages.Font = font_7;
            grpOptionsVolume.Font = font_7;
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
            lblOptionsMusicVolume.Font = font_1;
            lblOptionsSoundEffectsVolume.Font = font_1;
            lblOptionsMusicVolume.Location = new Point(17, 22);
            lblOptionsSoundEffectsVolume.Location = new Point(17, 47);
            sldOptionsMusicVolume.Location = new Point(81, 24);
            sldOptionsMusicVolume.Size = new Size(568, 16);
            sldOptionsSoundEffectsVolume.Location = new Point(81, 49);
            sldOptionsSoundEffectsVolume.Size = new Size(568, 16);
            lblOptionsMouseScrollMode.Font = font_1;
            chkOptionsLoadedGamesPaused.Font = font_1;
            cmbOptionsMouseScrollWheelBehaviour.Font = font_1;
            chkOptionsAutoSave.Font = font_1;
            lblOptionsMouseScrollMode.Location = new Point(195, 246);
            cmbOptionsMouseScrollWheelBehaviour.Size = new Size(250, 21);
            cmbOptionsMouseScrollWheelBehaviour.Location = new Point(341, 242);
            grpOptionsAutoSave.Location = new Point(12, 228);
            grpOptionsAutoSave.Font = font_7;
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
            lblOptionsAutomationMode.Font = font_1;
            lblOptionsControlAgentMissions.Font = font_1;
            lblOptionsControlAttacks.Font = font_1;
            lblOptionsControlColonization.Font = font_1;
            lblOptionsControlColonyFacilities.Font = font_1;
            lblOptionsControlConstruction.Font = font_1;
            lblOptionsControlDiplomacyGifts.Font = font_1;
            lblOptionsControlDiplomacyOffense.Font = font_1;
            lblOptionsControlDiplomacyTreaties.Font = font_1;
            lblOptionsControlOfferPirateMissions.Font = font_1;
            cmbOptionsControlAgentMissions.Font = font_1;
            cmbOptionsControlAttacks.Font = font_1;
            cmbOptionsControlColonization.Font = font_1;
            cmbOptionsControlColonyFacilities.Font = font_1;
            cmbOptionsControlConstruction.Font = font_1;
            cmbOptionsControlDiplomacyGifts.Font = font_1;
            cmbOptionsControlDiplomacyOffense.Font = font_1;
            cmbOptionsControlDiplomacyTreaties.Font = font_1;
            cmbOptionsControlOfferPirateMissions.Font = font_1;
            chkOptionsControlCharacterLocations.Font = font_1;
            chkOptionsControlColonyTaxRates.Font = font_1;
            chkOptionsControlDesigns.Font = font_1;
            chkOptionsControlFleets.Font = font_1;
            chkOptionsControlPopulationPolicy.Font = font_1;
            chkOptionsControlResearch.Font = font_1;
            chkOptionsControlTroops.Font = font_1;
            cmbOptionsAutomationMode.Font = font_1;
            pnlOptionsAutomationMode.Size = new Size(217, 41);
            pnlOptionsAutomationMode.Location = new Point(10, 21);
            pnlOptionsAutomationMode.BackColor = Color.FromArgb(128, 192, 0, 128);
            lblOptionsAutomationMode.Font = main_0.font_7;
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
            method_154(lblOptionsControlColonization, 191, size_);
            method_154(lblOptionsControlConstruction, 191, size_);
            method_154(lblOptionsControlAgentMissions, 186, new Size(241, 21));
            method_154(lblOptionsControlAttacks, 191, size_);
            method_154(lblOptionsControlDiplomacyGifts, 191, size_);
            method_154(lblOptionsControlDiplomacyTreaties, 191, size_);
            method_154(lblOptionsControlDiplomacyOffense, 191, size_);
            method_154(lblOptionsControlColonyFacilities, 191, size_);
            method_154(lblOptionsControlOfferPirateMissions, 191, size_);
            cmbOptionsControlColonization.Location = new Point(429, 19);
            cmbOptionsControlConstruction.Location = new Point(429, 48);
            cmbOptionsControlAgentMissions.Location = new Point(429, 77);
            cmbOptionsControlAttacks.Location = new Point(429, 106);
            cmbOptionsControlDiplomacyGifts.Location = new Point(429, 135);
            cmbOptionsControlDiplomacyTreaties.Location = new Point(429, 164);
            cmbOptionsControlDiplomacyOffense.Location = new Point(429, 193);
            cmbOptionsControlColonyFacilities.Location = new Point(429, 222);
            cmbOptionsControlOfferPirateMissions.Location = new Point(429, 251);
            cmbOptionsControlAgentMissions.Size = new Size(220, 24);
            cmbOptionsControlAttacks.Size = new Size(220, 24);
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

        private void method_154(Label label_0, int int_1, Size size_1)
        {
            label_0.AutoSize = false;
            label_0.TextAlign = ContentAlignment.MiddleRight;
            label_0.Size = size_1;
            label_0.MaximumSize = size_1;
            label_0.Location = new Point(int_1, label_0.Location.Y);
        }

        private void cmbOptionsAutomationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = cmbOptionsAutomationMode.Text;
            GameOptions gameOptions = null;
            if (text == TextResolver.GetText("Default"))
            {
                gameOptions = method_158();
            }
            else if (text == TextResolver.GetText("Expansion"))
            {
                gameOptions = method_159();
            }
            else if (text == TextResolver.GetText("War and Combat"))
            {
                gameOptions = method_160();
            }
            else if (text == TextResolver.GetText("Diplomacy"))
            {
                gameOptions = method_161();
            }
            else if (text == TextResolver.GetText("Spy Master"))
            {
                gameOptions = method_162();
            }
            else if (text == TextResolver.GetText("Expert") + " (" + TextResolver.GetText("none") + ")")
            {
                gameOptions = method_157();
            }
            else if (text == TextResolver.GetText("Rule in Absence") + " (" + TextResolver.GetText("full") + ")")
            {
                gameOptions = method_156();
            }
            if (gameOptions != null)
            {
                method_155(gameOptions);
            }
        }

        private void uwcbgxAbxH()
        {
            GameOptions other = method_156();
            GameOptions other2 = method_160();
            GameOptions other3 = method_158();
            GameOptions other4 = method_161();
            GameOptions other5 = method_159();
            GameOptions other6 = method_157();
            GameOptions other7 = method_162();
            GameOptions gameOptions = method_163();
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
            uwcbgxAbxH();
        }

        private void chkOptionsControlPopulationPolicy_CheckedChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void chkOptionsControlDesigns_CheckedChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void chkOptionsControlTroops_CheckedChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void sOmbQcqjdd(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void chkOptionsControlCharacterLocations_CheckedChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlColonization_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlConstruction_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlAgentMissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlColonyFacilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlDiplomacyGifts_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlDiplomacyTreaties_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlOfferPirateMissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void cmbOptionsControlDiplomacyOffense_SelectedIndexChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void chkOptionsControlResearch_CheckedChanged(object sender, EventArgs e)
        {
            uwcbgxAbxH();
        }

        private void btnGameOptionsResetAutomationMessages_Click(object sender, EventArgs e)
        {
            string string_ = TextResolver.GetText("Reenable All Automation Prompts");
            MessageBoxEx messageBoxEx = main_0.method_372(string_, TextResolver.GetText("Reset Automation Messages?"));
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                MessageBoxExManager.ResetAllSavedResponses();
                string filename = Main.GetGameFilesFolderCreateIfNeeded() + "automationPrefs";
                MessageBoxExManager.WriteSavedResponses(filename);
            }
        }

        private void method_155(GameOptions gameOptions_0)
        {
            cmbOptionsControlAttacks.SelectedIndexChanged -= cmbOptionsControlAttacks_SelectedIndexChanged;
            cmbOptionsControlColonization.SelectedIndexChanged -= cmbOptionsControlColonization_SelectedIndexChanged;
            chkOptionsControlColonyTaxRates.CheckedChanged -= chkOptionsControlColonyTaxRates_CheckedChanged;
            cmbOptionsControlConstruction.SelectedIndexChanged -= cmbOptionsControlConstruction_SelectedIndexChanged;
            chkOptionsControlDesigns.CheckedChanged -= chkOptionsControlDesigns_CheckedChanged;
            cmbOptionsControlDiplomacyGifts.SelectedIndexChanged -= cmbOptionsControlDiplomacyGifts_SelectedIndexChanged;
            cmbOptionsControlDiplomacyOffense.SelectedIndexChanged -= cmbOptionsControlDiplomacyOffense_SelectedIndexChanged;
            cmbOptionsControlDiplomacyTreaties.SelectedIndexChanged -= cmbOptionsControlDiplomacyTreaties_SelectedIndexChanged;
            chkOptionsControlFleets.CheckedChanged -= sOmbQcqjdd;
            chkOptionsControlTroops.CheckedChanged -= chkOptionsControlTroops_CheckedChanged;
            cmbOptionsControlAgentMissions.SelectedIndexChanged -= cmbOptionsControlAgentMissions_SelectedIndexChanged;
            chkOptionsControlResearch.CheckedChanged -= chkOptionsControlResearch_CheckedChanged;
            cmbOptionsControlColonyFacilities.SelectedIndexChanged -= cmbOptionsControlColonyFacilities_SelectedIndexChanged;
            chkOptionsControlPopulationPolicy.CheckedChanged -= chkOptionsControlPopulationPolicy_CheckedChanged;
            chkOptionsControlCharacterLocations.CheckedChanged -= chkOptionsControlCharacterLocations_CheckedChanged;
            cmbOptionsControlOfferPirateMissions.SelectedIndexChanged -= cmbOptionsControlOfferPirateMissions_SelectedIndexChanged;
            cmbOptionsControlAttacks.SelectedIndex = method_171(gameOptions_0.ControlAttacksOnEnemiesDefault);
            cmbOptionsControlColonization.SelectedIndex = method_171(gameOptions_0.ControlColonizationDefault);
            chkOptionsControlColonyTaxRates.Checked = gameOptions_0.ControlColonyTaxRatesDefault;
            cmbOptionsControlConstruction.SelectedIndex = method_171(gameOptions_0.ControlShipBuildingDefault);
            chkOptionsControlDesigns.Checked = gameOptions_0.ControlShipDesignDefault;
            cmbOptionsControlDiplomacyGifts.SelectedIndex = method_171(gameOptions_0.ControlDiplomaticGiftsDefault);
            cmbOptionsControlDiplomacyOffense.SelectedIndex = method_171(gameOptions_0.ControlWarTradeSanctionsDefault);
            cmbOptionsControlDiplomacyTreaties.SelectedIndex = method_171(gameOptions_0.ControlTreatyNegotiationDefault);
            chkOptionsControlFleets.Checked = gameOptions_0.ControlFleetFormationDefault;
            chkOptionsControlTroops.Checked = gameOptions_0.ControlTroopRecruitmentDefault;
            chkOptionsControlCharacterLocations.Checked = gameOptions_0.ControlCharacterLocationsDefault;
            cmbOptionsControlAgentMissions.SelectedIndex = method_171(gameOptions_0.ControlAgentAssignmentDefault);
            chkOptionsControlResearch.Checked = gameOptions_0.ControlResearchDefault;
            cmbOptionsControlColonyFacilities.SelectedIndex = method_171(gameOptions_0.ControlColonyFacilitiesDefault);
            chkOptionsControlPopulationPolicy.Checked = gameOptions_0.ControlPopulationPolicyDefault;
            cmbOptionsControlOfferPirateMissions.SelectedIndex = method_171(gameOptions_0.ControlOfferPirateMissionsDefault);
            cmbOptionsControlAttacks.SelectedIndexChanged += cmbOptionsControlAttacks_SelectedIndexChanged;
            cmbOptionsControlColonization.SelectedIndexChanged += cmbOptionsControlColonization_SelectedIndexChanged;
            chkOptionsControlColonyTaxRates.CheckedChanged += chkOptionsControlColonyTaxRates_CheckedChanged;
            cmbOptionsControlConstruction.SelectedIndexChanged += cmbOptionsControlConstruction_SelectedIndexChanged;
            chkOptionsControlDesigns.CheckedChanged += chkOptionsControlDesigns_CheckedChanged;
            cmbOptionsControlDiplomacyGifts.SelectedIndexChanged += cmbOptionsControlDiplomacyGifts_SelectedIndexChanged;
            cmbOptionsControlDiplomacyOffense.SelectedIndexChanged += cmbOptionsControlDiplomacyOffense_SelectedIndexChanged;
            cmbOptionsControlDiplomacyTreaties.SelectedIndexChanged += cmbOptionsControlDiplomacyTreaties_SelectedIndexChanged;
            chkOptionsControlFleets.CheckedChanged += sOmbQcqjdd;
            chkOptionsControlTroops.CheckedChanged += chkOptionsControlTroops_CheckedChanged;
            cmbOptionsControlAgentMissions.SelectedIndexChanged += cmbOptionsControlAgentMissions_SelectedIndexChanged;
            chkOptionsControlResearch.CheckedChanged += chkOptionsControlResearch_CheckedChanged;
            cmbOptionsControlColonyFacilities.SelectedIndexChanged += cmbOptionsControlColonyFacilities_SelectedIndexChanged;
            chkOptionsControlPopulationPolicy.CheckedChanged += chkOptionsControlPopulationPolicy_CheckedChanged;
            chkOptionsControlCharacterLocations.CheckedChanged += chkOptionsControlCharacterLocations_CheckedChanged;
            cmbOptionsControlOfferPirateMissions.SelectedIndexChanged += cmbOptionsControlOfferPirateMissions_SelectedIndexChanged;
        }

        private GameOptions method_156()
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

        private GameOptions method_157()
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

        private GameOptions method_158()
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

        private GameOptions method_159()
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

        private GameOptions method_160()
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

        private GameOptions method_161()
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

        private GameOptions method_162()
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

        private GameOptions method_163()
        {
            GameOptions gameOptions = new GameOptions();
            gameOptions.ControlAttacksOnEnemiesDefault = method_170(cmbOptionsControlAttacks.SelectedIndex);
            gameOptions.ControlColonizationDefault = method_170(cmbOptionsControlColonization.SelectedIndex);
            gameOptions.ControlColonyTaxRatesDefault = chkOptionsControlColonyTaxRates.Checked;
            gameOptions.ControlShipBuildingDefault = method_170(cmbOptionsControlConstruction.SelectedIndex);
            gameOptions.ControlShipDesignDefault = chkOptionsControlDesigns.Checked;
            gameOptions.ControlDiplomaticGiftsDefault = method_170(cmbOptionsControlDiplomacyGifts.SelectedIndex);
            gameOptions.ControlWarTradeSanctionsDefault = method_170(cmbOptionsControlDiplomacyOffense.SelectedIndex);
            gameOptions.ControlTreatyNegotiationDefault = method_170(cmbOptionsControlDiplomacyTreaties.SelectedIndex);
            gameOptions.ControlFleetFormationDefault = chkOptionsControlFleets.Checked;
            gameOptions.ControlTroopRecruitmentDefault = chkOptionsControlTroops.Checked;
            gameOptions.ControlAgentAssignmentDefault = method_170(cmbOptionsControlAgentMissions.SelectedIndex);
            gameOptions.ControlResearchDefault = chkOptionsControlResearch.Checked;
            gameOptions.ControlColonyFacilitiesDefault = method_170(cmbOptionsControlColonyFacilities.SelectedIndex);
            gameOptions.ControlPopulationPolicyDefault = chkOptionsControlPopulationPolicy.Checked;
            gameOptions.ControlCharacterLocationsDefault = chkOptionsControlCharacterLocations.Checked;
            gameOptions.ControlOfferPirateMissionsDefault = method_170(cmbOptionsControlOfferPirateMissions.SelectedIndex);
            return gameOptions;
        }

        private void method_164()
        {
            ApplyOptionsValues();
            if (pnlGameOptionsAdvancedDisplaySettings.Visible)
            {
                method_185();
            }
            if (pnlGameOptionsEmpireSettings.Visible)
            {
                method_181();
            }
            if (pnlGameOptionsMessages.Visible)
            {
                method_183();
            }
            pnlGameOptions.SendToBack();
            pnlGameOptions.Visible = false;
        }

        private int method_165()
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

        private void method_166(int int_1)
        {
            switch (int_1)
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

        private void method_167()
        {
            main_0.gameOptions_0.DisplayMessageUnderAttackCivilianShips = chkOptionsScrollingMessageUnderAttackCivilianShips.Checked;
            main_0.gameOptions_0.DisplayMessageUnderAttackCivilianBases = chkOptionsScrollingMessageUnderAttackCivilianBases.Checked;
            main_0.gameOptions_0.DisplayMessageUnderAttackExplorationShips = chkOptionsScrollingMessageUnderAttackExplorationShips.Checked;
            main_0.gameOptions_0.DisplayMessageUnderAttackColonyConstructionShips = chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Checked;
            main_0.gameOptions_0.DisplayMessageUnderAttackMilitaryShips = chkOptionsScrollingMessageUnderAttackMilitaryShips.Checked;
            main_0.gameOptions_0.DisplayMessageUnderAttackOtherStateBases = chkOptionsScrollingMessageUnderAttackOtherStateBases.Checked;
            main_0.gameOptions_0.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases = chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Checked;
            main_0.gameOptions_0.DisplayMessageBuiltObjectBuilt = chkOptionsScrollingMessageNewShipBuilt.Checked;
            main_0.gameOptions_0.DisplayMessageColonyInvaded = chkOptionsScrollingMessageColonyGainLoss.Checked;
            main_0.gameOptions_0.DisplayMessageDiplomacyEmpireMetDestroyed = chkOptionsScrollingMessageEmpireMetDestroyed.Checked;
            main_0.gameOptions_0.DisplayMessageDiplomacyGift = chkOptionsScrollingMessageRequestWarning.Checked;
            main_0.gameOptions_0.DisplayMessageDiplomacyRequestWarning = chkOptionsScrollingMessageRequestWarning.Checked;
            main_0.gameOptions_0.DisplayMessageDiplomacyTreaty = chkOptionsScrollingMessageDiplomacyTreaties.Checked;
            main_0.gameOptions_0.DisplayMessageDiplomacyWarTradeSanctions = chkOptionsScrollingMessageWarTradeSanctions.Checked;
            main_0.gameOptions_0.DisplayMessageNewColony = chkOptionsScrollingMessageColonyGainLoss.Checked;
            main_0.gameOptions_0.DisplayMessageResearchNewComponent = chkOptionsScrollingMessageResearchBreakthrough.Checked;
            main_0.gameOptions_0.DisplayMessageIntelligenceMissions = chkOptionsScrollingMessageIntelligenceMissions.Checked;
            main_0.gameOptions_0.DisplayMessageExploration = chkOptionsScrollingMessageExploration.Checked;
            main_0.gameOptions_0.DisplayMessageShipMissionComplete = chkOptionsScrollingMessageShipMissionComplete.Checked;
            main_0.gameOptions_0.DisplayMessageShipNeedsRefuelling = chkOptionsScrollingMessageShipNeedsRefuelling.Checked;
            main_0.gameOptions_0.DisplayMessageConstructionResourceShortage = chkOptionsScrollingMessageConstructionResourceShortage.Checked;
            main_0.gameOptions_0.DisplayPopupUnderAttackCivilianShips = chkOptionsPopupMessageUnderAttackCivilianShips.Checked;
            main_0.gameOptions_0.DisplayPopupUnderAttackCivilianBases = chkOptionsPopupMessageUnderAttackCivilianBases.Checked;
            main_0.gameOptions_0.DisplayPopupUnderAttackExplorationShips = chkOptionsPopupMessageUnderAttackExplorationShips.Checked;
            main_0.gameOptions_0.DisplayPopupUnderAttackColonyConstructionShips = chkOptionsPopupMessageUnderAttackColonyConstructionShips.Checked;
            main_0.gameOptions_0.DisplayPopupUnderAttackMilitaryShips = chkOptionsPopupMessageUnderAttackMilitaryShips.Checked;
            main_0.gameOptions_0.DisplayPopupUnderAttackOtherStateBases = chkOptionsPopupMessageUnderAttackOtherStateBases.Checked;
            main_0.gameOptions_0.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases = chkOptionsPopupMessageUnderAttackColoniesSpaceports.Checked;
            main_0.gameOptions_0.DisplayPopupBuiltObjectBuilt = chkOptionsPopupMessageShipBuilt.Checked;
            main_0.gameOptions_0.DisplayPopupColonyInvaded = chkOptionsPopupMessageColonyGainLoss.Checked;
            main_0.gameOptions_0.DisplayPopupDiplomacyEmpireMetDestroyed = chkOptionsPopupMessageEmpireMetDestroyed.Checked;
            main_0.gameOptions_0.DisplayPopupDiplomacyGift = chkOptionsPopupMessageRequestWarning.Checked;
            main_0.gameOptions_0.DisplayPopupDiplomacyRequestWarning = chkOptionsPopupMessageRequestWarning.Checked;
            main_0.gameOptions_0.DisplayPopupDiplomacyTreaty = chkOptionsPopupMessageDiplomacyTreaties.Checked;
            main_0.gameOptions_0.DisplayPopupDiplomacyWarTradeSanctions = chkOptionsPopupMessageDiplomacyWarTradeSanctions.Checked;
            main_0.gameOptions_0.DisplayPopupNewColony = chkOptionsPopupMessageColonyGainLoss.Checked;
            main_0.gameOptions_0.DisplayPopupResearchNewComponent = chkOptionsPopupMessageResearchBreakthrough.Checked;
            main_0.gameOptions_0.DisplayPopupIntelligenceMissions = chkOptionsPopupMessageIntelligenceMissions.Checked;
            main_0.gameOptions_0.DisplayPopupExploration = chkOptionsPopupMessageExploration.Checked;
            main_0.gameOptions_0.DisplayPopupShipMissionComplete = chkOptionsPopupMessageShipMissionComplete.Checked;
            main_0.gameOptions_0.DisplayPopupShipNeedsRefuelling = chkOptionsPopupMessageShipNeedsRefuelling.Checked;
            main_0.gameOptions_0.DisplayPopupConstructionResourceShortage = chkOptionsPopupMessageConstructionResourceShortage.Checked;
        }

        private void method_168()
        {
            chkOptionsScrollingMessageUnderAttackCivilianShips.Checked = main_0.gameOptions_0.DisplayMessageUnderAttackCivilianShips;
            chkOptionsScrollingMessageUnderAttackCivilianBases.Checked = main_0.gameOptions_0.DisplayMessageUnderAttackCivilianBases;
            chkOptionsScrollingMessageUnderAttackExplorationShips.Checked = main_0.gameOptions_0.DisplayMessageUnderAttackExplorationShips;
            chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Checked = main_0.gameOptions_0.DisplayMessageUnderAttackColonyConstructionShips;
            chkOptionsScrollingMessageUnderAttackMilitaryShips.Checked = main_0.gameOptions_0.DisplayMessageUnderAttackMilitaryShips;
            chkOptionsScrollingMessageUnderAttackOtherStateBases.Checked = main_0.gameOptions_0.DisplayMessageUnderAttackOtherStateBases;
            chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Checked = main_0.gameOptions_0.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases;
            chkOptionsScrollingMessageNewShipBuilt.Checked = main_0.gameOptions_0.DisplayMessageBuiltObjectBuilt;
            chkOptionsScrollingMessageColonyGainLoss.Checked = main_0.gameOptions_0.DisplayMessageColonyInvaded;
            chkOptionsScrollingMessageEmpireMetDestroyed.Checked = main_0.gameOptions_0.DisplayMessageDiplomacyEmpireMetDestroyed;
            chkOptionsScrollingMessageRequestWarning.Checked = main_0.gameOptions_0.DisplayMessageDiplomacyRequestWarning;
            chkOptionsScrollingMessageDiplomacyTreaties.Checked = main_0.gameOptions_0.DisplayMessageDiplomacyTreaty;
            chkOptionsScrollingMessageWarTradeSanctions.Checked = main_0.gameOptions_0.DisplayMessageDiplomacyWarTradeSanctions;
            chkOptionsScrollingMessageResearchBreakthrough.Checked = main_0.gameOptions_0.DisplayMessageResearchNewComponent;
            chkOptionsScrollingMessageIntelligenceMissions.Checked = main_0.gameOptions_0.DisplayMessageIntelligenceMissions;
            chkOptionsScrollingMessageExploration.Checked = main_0.gameOptions_0.DisplayMessageExploration;
            chkOptionsScrollingMessageShipMissionComplete.Checked = main_0.gameOptions_0.DisplayMessageShipMissionComplete;
            chkOptionsScrollingMessageShipNeedsRefuelling.Checked = main_0.gameOptions_0.DisplayMessageShipNeedsRefuelling;
            chkOptionsScrollingMessageConstructionResourceShortage.Checked = main_0.gameOptions_0.DisplayMessageConstructionResourceShortage;
            chkOptionsPopupMessageShipBuilt.Checked = main_0.gameOptions_0.DisplayPopupBuiltObjectBuilt;
            chkOptionsPopupMessageColonyGainLoss.Checked = main_0.gameOptions_0.DisplayPopupColonyInvaded;
            chkOptionsPopupMessageEmpireMetDestroyed.Checked = main_0.gameOptions_0.DisplayPopupDiplomacyEmpireMetDestroyed;
            chkOptionsPopupMessageRequestWarning.Checked = main_0.gameOptions_0.DisplayPopupDiplomacyRequestWarning;
            chkOptionsPopupMessageDiplomacyTreaties.Checked = main_0.gameOptions_0.DisplayPopupDiplomacyTreaty;
            chkOptionsPopupMessageDiplomacyWarTradeSanctions.Checked = main_0.gameOptions_0.DisplayPopupDiplomacyWarTradeSanctions;
            chkOptionsPopupMessageResearchBreakthrough.Checked = main_0.gameOptions_0.DisplayPopupResearchNewComponent;
            chkOptionsPopupMessageIntelligenceMissions.Checked = main_0.gameOptions_0.DisplayPopupIntelligenceMissions;
            chkOptionsPopupMessageExploration.Checked = main_0.gameOptions_0.DisplayPopupExploration;
            chkOptionsPopupMessageShipMissionComplete.Checked = main_0.gameOptions_0.DisplayPopupShipMissionComplete;
            chkOptionsPopupMessageShipNeedsRefuelling.Checked = main_0.gameOptions_0.DisplayPopupShipNeedsRefuelling;
            chkOptionsPopupMessageConstructionResourceShortage.Checked = main_0.gameOptions_0.DisplayPopupConstructionResourceShortage;
            chkOptionsPopupMessageUnderAttackCivilianShips.Checked = main_0.gameOptions_0.DisplayPopupUnderAttackCivilianShips;
            chkOptionsPopupMessageUnderAttackCivilianBases.Checked = main_0.gameOptions_0.DisplayPopupUnderAttackCivilianBases;
            chkOptionsPopupMessageUnderAttackExplorationShips.Checked = main_0.gameOptions_0.DisplayPopupUnderAttackExplorationShips;
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.Checked = main_0.gameOptions_0.DisplayPopupUnderAttackColonyConstructionShips;
            chkOptionsPopupMessageUnderAttackMilitaryShips.Checked = main_0.gameOptions_0.DisplayPopupUnderAttackMilitaryShips;
            chkOptionsPopupMessageUnderAttackOtherStateBases.Checked = main_0.gameOptions_0.DisplayPopupUnderAttackOtherStateBases;
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.Checked = main_0.gameOptions_0.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases;
        }

        private void ApplyOptionsValues()
        {
            var options = main_0.gameOptions_0;
            options.AutoPauseWhenInPopupWindow = chkOptionsAutoPauseInPopup.Checked;
            options.MainViewScrollSpeed = sldOptionsMainViewScrollSpeed.Value;
            options.MainViewZoomSpeed = sldOptionsMainViewZoomSpeed.Value;
            if (options.StarFieldSize != sldOptionsMainViewStarFieldSize.Value)
            {
                options.StarFieldSize = sldOptionsMainViewStarFieldSize.Value;
                if (main_0.mainView.main_0 != null)
                {
                    main_0.mainView.method_14(options.StarFieldSize);
                }
            }

            options.GuiScale = sldOptionsMainViewGuiScale.Value / 1000.0;
            var guiScale = (float)options.GuiScale;
            main_0.Scale(new SizeF(guiScale, guiScale));
            
            options.ShowSystemNebulae = chkOptionsShowSystemNebulae.Checked;
            options.MusicVolume = (double)sldOptionsMusicVolume.Value / 100.0;
            options.SoundEffectsVolume = (double)sldOptionsSoundEffectsVolume.Value / 100.0;
            main_0.MusicPlayer.SetVolume(options.MusicVolume);
            main_0.EffectsPlayer.Volume = options.SoundEffectsVolume;
            GlassButton.Volume = options.SoundEffectsVolume;
            CloseButton.Volume = options.SoundEffectsVolume;
            ListViewBase.Volume = options.SoundEffectsVolume;
            HoverButton.Volume = options.SoundEffectsVolume;
            HoverMenuItem.Volume = options.SoundEffectsVolume;
            options.MouseScrollWheelBehaviour = method_165();
            options.LoadedGamesPaused = chkOptionsLoadedGamesPaused.Checked;
            if (chkOptionsAutoSave.Checked)
            {
                options.AutoSaveInterval = (int)numOptionsAutoSaveMinutes.Value;
            }
            else
            {
                options.AutoSaveInterval = 0;
            }
            options.ControlAttacksOnEnemiesDefault = method_170(cmbOptionsControlAttacks.SelectedIndex);
            options.ControlColonizationDefault = method_170(cmbOptionsControlColonization.SelectedIndex);
            options.ControlColonyTaxRatesDefault = chkOptionsControlColonyTaxRates.Checked;
            options.ControlShipBuildingDefault = method_170(cmbOptionsControlConstruction.SelectedIndex);
            options.ControlShipDesignDefault = chkOptionsControlDesigns.Checked;
            options.ControlDiplomaticGiftsDefault = method_170(cmbOptionsControlDiplomacyGifts.SelectedIndex);
            options.ControlWarTradeSanctionsDefault = method_170(cmbOptionsControlDiplomacyOffense.SelectedIndex);
            options.ControlTreatyNegotiationDefault = method_170(cmbOptionsControlDiplomacyTreaties.SelectedIndex);
            options.ControlFleetFormationDefault = chkOptionsControlFleets.Checked;
            options.ControlTroopRecruitmentDefault = chkOptionsControlTroops.Checked;
            options.ControlAgentAssignmentDefault = method_170(cmbOptionsControlAgentMissions.SelectedIndex);
            options.ControlResearchDefault = chkOptionsControlResearch.Checked;
            options.ControlColonyFacilitiesDefault = method_170(cmbOptionsControlColonyFacilities.SelectedIndex);
            options.ControlPopulationPolicyDefault = chkOptionsControlPopulationPolicy.Checked;
            options.ControlCharacterLocationsDefault = chkOptionsControlCharacterLocations.Checked;
            options.ControlOfferPirateMissionsDefault = method_170(cmbOptionsControlOfferPirateMissions.SelectedIndex);
            main_0.YxwyUefOyQ();
            main_0.method_257();
        }

        private AutomationLevel method_170(int int_1)
        {
            AutomationLevel result = AutomationLevel.Manual;
            switch (int_1)
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

        private int method_171(AutomationLevel automationLevel_0)
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

        private void PopulateOptionsValues()
        {
            sldOptionsMainViewZoomSpeed.Minimum = 1;
            chkOptionsAutoPauseInPopup.Checked = main_0.gameOptions_0.AutoPauseWhenInPopupWindow;
            sldOptionsMainViewScrollSpeed.Value = main_0.gameOptions_0.MainViewScrollSpeed;
            sldOptionsMainViewStarFieldSize.Value = main_0.gameOptions_0.StarFieldSize;
            sldOptionsMainViewZoomSpeed.Value = main_0.gameOptions_0.MainViewZoomSpeed;
            sldOptionsMainViewGuiScale.Value = (int)(main_0.gameOptions_0.GuiScale * 1000.0);
            chkOptionsShowSystemNebulae.Checked = main_0.gameOptions_0.ShowSystemNebulae;
            sldOptionsMusicVolume.Value = (int)(main_0.gameOptions_0.MusicVolume * 100.0);
            sldOptionsSoundEffectsVolume.Value = (int)(main_0.gameOptions_0.SoundEffectsVolume * 100.0);
            method_166(main_0.gameOptions_0.MouseScrollWheelBehaviour);
            chkOptionsLoadedGamesPaused.Checked = main_0.gameOptions_0.LoadedGamesPaused;
            if (main_0.gameOptions_0.AutoSaveInterval > 0)
            {
                chkOptionsAutoSave.Checked = true;
                numOptionsAutoSaveMinutes.Value = Math.Max(10, main_0.gameOptions_0.AutoSaveInterval);
                numOptionsAutoSaveMinutes.Enabled = true;
            }
            else
            {
                chkOptionsAutoSave.Checked = false;
                numOptionsAutoSaveMinutes.Enabled = false;
            }
            cmbOptionsControlAttacks.SelectedIndex = method_171(main_0.gameOptions_0.ControlAttacksOnEnemiesDefault);
            cmbOptionsControlColonization.SelectedIndex = method_171(main_0.gameOptions_0.ControlColonizationDefault);
            chkOptionsControlColonyTaxRates.Checked = main_0.gameOptions_0.ControlColonyTaxRatesDefault;
            cmbOptionsControlConstruction.SelectedIndex = method_171(main_0.gameOptions_0.ControlShipBuildingDefault);
            chkOptionsControlDesigns.Checked = main_0.gameOptions_0.ControlShipDesignDefault;
            cmbOptionsControlDiplomacyGifts.SelectedIndex = method_171(main_0.gameOptions_0.ControlDiplomaticGiftsDefault);
            cmbOptionsControlDiplomacyOffense.SelectedIndex = method_171(main_0.gameOptions_0.ControlWarTradeSanctionsDefault);
            cmbOptionsControlDiplomacyTreaties.SelectedIndex = method_171(main_0.gameOptions_0.ControlTreatyNegotiationDefault);
            chkOptionsControlFleets.Checked = main_0.gameOptions_0.ControlFleetFormationDefault;
            chkOptionsControlTroops.Checked = main_0.gameOptions_0.ControlTroopRecruitmentDefault;
            cmbOptionsControlAgentMissions.SelectedIndex = method_171(main_0.gameOptions_0.ControlAgentAssignmentDefault);
            chkOptionsControlResearch.Checked = main_0.gameOptions_0.ControlResearchDefault;
            cmbOptionsControlColonyFacilities.SelectedIndex = method_171(main_0.gameOptions_0.ControlColonyFacilitiesDefault);
            chkOptionsControlPopulationPolicy.Checked = main_0.gameOptions_0.ControlPopulationPolicyDefault;
            chkOptionsControlCharacterLocations.Checked = main_0.gameOptions_0.ControlCharacterLocationsDefault;
            cmbOptionsControlOfferPirateMissions.SelectedIndex = method_171(main_0.gameOptions_0.ControlOfferPirateMissionsDefault);
        }

        private void sldOptionsMusicVolume_Scroll(object sender, ScrollEventArgs e)
        {
            main_0.gameOptions_0.MusicVolume = (double)sldOptionsMusicVolume.Value / 100.0;
            main_0.MusicPlayer.SetVolume(main_0.gameOptions_0.MusicVolume);
        }

        private void sldOptionsSoundEffectsVolume_Scroll(object sender, ScrollEventArgs e)
        {
            main_0.gameOptions_0.SoundEffectsVolume = (double)sldOptionsSoundEffectsVolume.Value / 100.0;
            main_0.EffectsPlayer.Volume = main_0.gameOptions_0.SoundEffectsVolume;
        }

        private void pnlGameOptions_CloseButtonClicked(object sender, EventArgs e)
        {
            method_164();
        }

        private void lnkOptions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_153();
        }

        private void lnkOptions_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Options") + ":\n" + TextResolver.GetText("Control display, sound and other game settings"));
        }

        private void lnkOptions_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void method_173(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string name = TextResolver.GetText("Government Types");
            string text = "";
            if (text != "(" + TextResolver.GetText("Random") + ")")
            {
                GovernmentAttributes byName = Galaxy.GovernmentsStatic.GetByName(text);
                if (byName != null)
                {
                    name = byName.Name;
                }
            }
            method_127(name);
        }

        private void pnlGameOptionsEmpireSettings_CloseButtonClicked(object sender, EventArgs e)
        {
            method_181();
        }

        private void btnGameOptionsEmpireSettings_Click(object sender, EventArgs e)
        {
            method_174();
        }

        private void method_174()
        {
            pnlGameOptionsEmpireSettings.Size = new Size(500, 769);
            pnlGameOptionsEmpireSettings.Location = new Point((base.Width - pnlGameOptionsEmpireSettings.Width) / 2, (base.Height - pnlGameOptionsEmpireSettings.Height) / 2);
            pnlGameOptionsEmpireSettings.DoLayout();
            pnlGameOptionsEmpireSettings.HeaderTitle = TextResolver.GetText("Your Empire Settings");
            if (!grpGameOptionsDefaultEngagementStances.Font.Bold)
            {
                grpGameOptionsDefaultEngagementStances.Font = new Font(grpGameOptionsDefaultEngagementStances.Font.FontFamily, 19f, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            grpGameOptionsDefaultEngagementStances.Size = new Size(465, 156);
            grpGameOptionsDefaultEngagementStances.Location = new Point(10, 10);
            lblGameOptionsEngagementStancePatrol.Font = font_1;
            lblGameOptionsEngagementStanceEscort.Font = font_1;
            lblGameOptionsEngagementStanceAttack.Font = font_1;
            lblGameOptionsEngagementStanceOther.Font = font_1;
            cmbGameOptionsEngagementStancePatrol.Font = font_1;
            cmbGameOptionsEngagementStanceEscort.Font = font_1;
            cmbGameOptionsEngagementStanceAttack.Font = font_1;
            cmbGameOptionsEngagementStanceOther.Font = font_1;
            lblGameOptionsEngagementStancePatrol.Location = new Point(10, 29);
            lblGameOptionsEngagementStanceEscort.Location = new Point(10, 60);
            lblGameOptionsEngagementStanceAttack.Location = new Point(10, 91);
            lblGameOptionsEngagementStanceOther.Location = new Point(10, 122);
            cmbGameOptionsEngagementStancePatrol.Size = new Size(230, 21);
            cmbGameOptionsEngagementStanceEscort.Size = new Size(230, 21);
            cmbGameOptionsEngagementStanceAttack.Size = new Size(230, 21);
            cmbGameOptionsEngagementStanceOther.Size = new Size(230, 21);
            cmbGameOptionsEngagementStancePatrol.Location = new Point(140, 25);
            cmbGameOptionsEngagementStanceEscort.Location = new Point(140, 56);
            cmbGameOptionsEngagementStanceAttack.Location = new Point(140, 87);
            cmbGameOptionsEngagementStanceOther.Location = new Point(140, 118);
            if (!grpGameOptionsDefaultEngagementStancesManual.Font.Bold)
            {
                grpGameOptionsDefaultEngagementStancesManual.Font = new Font(grpGameOptionsDefaultEngagementStancesManual.Font.FontFamily, 19f, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            grpGameOptionsDefaultEngagementStancesManual.Size = new Size(465, 156);
            grpGameOptionsDefaultEngagementStancesManual.Location = new Point(10, 177);
            lblGameOptionsEngagementStancePatrolManual.Font = font_1;
            lblGameOptionsEngagementStanceEscortManual.Font = font_1;
            lblGameOptionsEngagementStanceAttackManual.Font = font_1;
            lblGameOptionsEngagementStanceOtherManual.Font = font_1;
            cmbGameOptionsEngagementStancePatrolManual.Font = font_1;
            cmbGameOptionsEngagementStanceEscortManual.Font = font_1;
            cmbGameOptionsEngagementStanceAttackManual.Font = font_1;
            cmbGameOptionsEngagementStanceOtherManual.Font = font_1;
            lblGameOptionsEngagementStancePatrolManual.Location = new Point(10, 29);
            lblGameOptionsEngagementStanceEscortManual.Location = new Point(10, 60);
            lblGameOptionsEngagementStanceAttackManual.Location = new Point(10, 91);
            lblGameOptionsEngagementStanceOtherManual.Location = new Point(10, 122);
            cmbGameOptionsEngagementStancePatrolManual.Size = new Size(230, 21);
            cmbGameOptionsEngagementStanceEscortManual.Size = new Size(230, 21);
            cmbGameOptionsEngagementStanceAttackManual.Size = new Size(230, 21);
            cmbGameOptionsEngagementStanceOtherManual.Size = new Size(230, 21);
            cmbGameOptionsEngagementStancePatrolManual.Location = new Point(140, 25);
            cmbGameOptionsEngagementStanceEscortManual.Location = new Point(140, 56);
            cmbGameOptionsEngagementStanceAttackManual.Location = new Point(140, 87);
            cmbGameOptionsEngagementStanceOtherManual.Location = new Point(140, 118);
            if (!grpGameOptionsFleetAttackSettings.Font.Bold)
            {
                grpGameOptionsFleetAttackSettings.Font = new Font(grpGameOptionsFleetAttackSettings.Font.FontFamily, 19f, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            grpGameOptionsFleetAttackSettings.Size = new Size(465, 90);
            grpGameOptionsFleetAttackSettings.Location = new Point(10, 344);
            numGameOptionsFleetAttackRefuel.Location = new Point(10, 25);
            numGameOptionsFleetAttackGather.Location = new Point(10, 56);
            lblGameOptionsFleetAttackRefuel.Location = new Point(60, 27);
            lblGameOptionsFleetAttackRefuel.Font = font_1;
            lblGameOptionsFleetAttackGather.Location = new Point(60, 58);
            lblGameOptionsFleetAttackGather.Font = font_1;
            sldGameOptionsAttackOvermatch.Size = new Size(465, 62);
            sldGameOptionsAttackOvermatch.Font = font_1;
            sldGameOptionsAttackOvermatch.LabelWidth = 120;
            sldGameOptionsAttackOvermatch.Location = new Point(10, 458);
            sldGameOptionsAttackOvermatch.Setup();
            sldGameOptionsAttackOvermatch.SetLabels(new string[5] { "1:1", "1.5:1", "2:1", "3:1", "5:1" });
            sldGameOptionsAttackOvermatch.LinkWidth = 0;
            sldGameOptionsAttackOvermatch.Size = new Size(465, 62);
            chkOptionsAllowSameSystemAsOtherEmpires.CheckAlign = ContentAlignment.MiddleLeft;
            chkOptionsAllowSameSystemAsOtherEmpires.Location = new Point(10, 513);
            chkOptionsAllowSameSystemAsOtherEmpires.Visible = false;
            if (!grpGameOptionsDiscoveries.Font.Bold)
            {
                grpGameOptionsDiscoveries.Font = new Font(grpGameOptionsDefaultEngagementStances.Font.FontFamily, 19f, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            grpGameOptionsDiscoveries.Size = new Size(465, 99);
            grpGameOptionsDiscoveries.Location = new Point(10, 537);
            lblGameOptionsEncounterRuins.Location = new Point(7, 23);
            lblGameOptionsEncounterRuins.Font = font_1;
            cmbGameOptionsEncounterRuins.Size = new Size(260, 21);
            cmbGameOptionsEncounterRuins.Font = font_1;
            cmbGameOptionsEncounterRuins.Location = new Point(198, 18);
            lblGameOptionsEncounterAbandonedShipOrBase.Location = new Point(7, 50);
            lblGameOptionsEncounterAbandonedShipOrBase.MaximumSize = new Size(188, 40);
            lblGameOptionsEncounterAbandonedShipOrBase.Font = font_1;
            cmbGameOptionsEncounterAbandonedShipOrBase.Size = new Size(260, 21);
            cmbGameOptionsEncounterAbandonedShipOrBase.Location = new Point(198, 58);
            cmbGameOptionsEncounterAbandonedShipOrBase.Font = font_1;
            chkOptionsNewShipsAutomated.Location = new Point(10, 645);
            chkOptionsNewShipsAutomated.Font = font_1;
            chkOptionsNewShipsAutomated.CheckAlign = ContentAlignment.TopLeft;
            chkOptionsSuppressAllPopups.Location = new Point(10, 670);
            chkOptionsSuppressAllPopups.BringToFront();
            chkOptionsSuppressAllPopups.Font = font_1;
            chkOptionsSuppressAllPopups.CheckAlign = ContentAlignment.TopLeft;
            method_175();
            pnlGameOptionsEmpireSettings.Visible = true;
            pnlGameOptionsEmpireSettings.BringToFront();
        }

        private void method_175()
        {
            if (main_0.gameOptions_0 == null)
            {
                return;
            }
            cmbGameOptionsEngagementStancePatrol.SelectedIndex = method_180(main_0.gameOptions_0.AttackRangePatrol);
            cmbGameOptionsEngagementStanceEscort.SelectedIndex = method_180(main_0.gameOptions_0.AttackRangeEscort);
            cmbGameOptionsEngagementStanceAttack.SelectedIndex = method_180(main_0.gameOptions_0.AttackRangeAttack);
            cmbGameOptionsEngagementStanceOther.SelectedIndex = method_180(main_0.gameOptions_0.AttackRangeOther);
            cmbGameOptionsEngagementStancePatrolManual.SelectedIndex = method_180(main_0.gameOptions_0.AttackRangePatrolManual);
            cmbGameOptionsEngagementStanceEscortManual.SelectedIndex = method_180(main_0.gameOptions_0.AttackRangeEscortManual);
            cmbGameOptionsEngagementStanceAttackManual.SelectedIndex = method_180(main_0.gameOptions_0.AttackRangeAttackManual);
            cmbGameOptionsEngagementStanceOtherManual.SelectedIndex = method_180(main_0.gameOptions_0.AttackRangeOtherManual);
            sldGameOptionsAttackOvermatch.Value = method_178(main_0.gameOptions_0.AttackOverMatchFactor);
            numGameOptionsFleetAttackRefuel.Value = main_0.method_559(main_0.gameOptions_0.FleetAttackRefuelPortion);
            numGameOptionsFleetAttackGather.Value = main_0.method_559(main_0.gameOptions_0.FleetAttackGatherPortion);
            cmbGameOptionsEncounterRuins.SelectedIndex = main_0.gameOptions_0.DiscoveryActionRuin;
            cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex = main_0.gameOptions_0.DiscoveryActionAbandonedShipBase;
            chkOptionsNewShipsAutomated.Checked = main_0.gameOptions_0.NewShipsAutomated;
            chkOptionsSuppressAllPopups.Checked = main_0.gameOptions_0.SuppressAllPopups;
            if (chkOptionsSuppressAllPopups.Checked)
            {
                if (cmbGameOptionsEncounterRuins.SelectedIndex < 1)
                {
                    cmbGameOptionsEncounterRuins.SelectedIndex = Math.Max(1, main_0.gameOptions_0.DiscoveryActionRuin);
                }
                if (cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex < 1)
                {
                    cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex = Math.Max(1, main_0.gameOptions_0.DiscoveryActionAbandonedShipBase);
                }
            }
        }

        private void method_176()
        {
            if (main_0.gameOptions_0 != null)
            {
                main_0.gameOptions_0.AttackRangePatrol = method_179(cmbGameOptionsEngagementStancePatrol.SelectedIndex);
                main_0.gameOptions_0.AttackRangeEscort = method_179(cmbGameOptionsEngagementStanceEscort.SelectedIndex);
                main_0.gameOptions_0.AttackRangeAttack = method_179(cmbGameOptionsEngagementStanceAttack.SelectedIndex);
                main_0.gameOptions_0.AttackRangeOther = method_179(cmbGameOptionsEngagementStanceOther.SelectedIndex);
                main_0.gameOptions_0.AttackRangePatrolManual = method_179(cmbGameOptionsEngagementStancePatrolManual.SelectedIndex);
                main_0.gameOptions_0.AttackRangeEscortManual = method_179(cmbGameOptionsEngagementStanceEscortManual.SelectedIndex);
                main_0.gameOptions_0.AttackRangeAttackManual = method_179(cmbGameOptionsEngagementStanceAttackManual.SelectedIndex);
                main_0.gameOptions_0.AttackRangeOtherManual = method_179(cmbGameOptionsEngagementStanceOtherManual.SelectedIndex);
                main_0.gameOptions_0.AttackOverMatchFactor = method_177(sldGameOptionsAttackOvermatch.Value);
                main_0.gameOptions_0.FleetAttackRefuelPortion = main_0.method_560(numGameOptionsFleetAttackRefuel.Value);
                main_0.gameOptions_0.FleetAttackGatherPortion = main_0.method_560(numGameOptionsFleetAttackGather.Value);
                main_0.gameOptions_0.DiscoveryActionRuin = cmbGameOptionsEncounterRuins.SelectedIndex;
                main_0.gameOptions_0.DiscoveryActionAbandonedShipBase = cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex;
                main_0.gameOptions_0.NewShipsAutomated = chkOptionsNewShipsAutomated.Checked;
                main_0.gameOptions_0.SuppressAllPopups = chkOptionsSuppressAllPopups.Checked;
            }
        }

        private float method_177(int int_1)
        {
            float result = 2f;
            switch (int_1)
            {
                case 0:
                    result = 1f;
                    break;
                case 1:
                    result = 1.5f;
                    break;
                case 2:
                    result = 2f;
                    break;
                case 3:
                    result = 3f;
                    break;
                case 4:
                    result = 5f;
                    break;
            }
            return result;
        }

        private int method_178(float float_2)
        {
            int result = -1;
            if (float_2 == 1f)
            {
                result = 0;
            }
            else if (float_2 == 1.5f)
            {
                result = 1;
            }
            else if (float_2 == 2f)
            {
                result = 2;
            }
            else if (float_2 == 3f)
            {
                result = 3;
            }
            else if (float_2 == 5f)
            {
                result = 4;
            }
            return result;
        }

        private int method_179(int int_1)
        {
            int result = 0;
            switch (int_1)
            {
                case 0:
                    result = -1;
                    break;
                case 1:
                    result = 0;
                    break;
                case 2:
                    result = 2000;
                    break;
                case 3:
                    result = 48000;
                    break;
            }
            return result;
        }

        private int method_180(int int_1)
        {
            int result = -1;
            if (int_1 < 0)
            {
                result = 0;
            }
            else if (int_1 == 0)
            {
                result = 1;
            }
            else if (int_1 >= 0 && int_1 <= 2000)
            {
                result = 2;
            }
            else if (int_1 > 2000 && int_1 <= 48000)
            {
                result = 3;
            }
            return result;
        }

        private void method_181()
        {
            method_176();
            pnlGameOptionsEmpireSettings.SendToBack();
            pnlGameOptionsEmpireSettings.Visible = false;
        }

        private void btnGameOptionsShowMessages_Click(object sender, EventArgs e)
        {
            method_182();
        }

        private void pnlGameOptionsMessages_CloseButtonClicked(object sender, EventArgs e)
        {
            method_183();
        }

        private void method_182()
        {
            pnlGameOptionsMessages.Size = new Size(735, 502);
            pnlGameOptionsMessages.Location = new Point((base.Width - pnlGameOptionsMessages.Width) / 2, (base.Height - pnlGameOptionsMessages.Height) / 2);
            pnlGameOptionsMessages.DoLayout();
            grpOptionsPopupMessages.Visible = true;
            grpOptionsScrollingMessages.Visible = true;
            grpOptionsPopupMessages.Font = font_7;
            grpOptionsScrollingMessages.Font = font_7;
            grpOptionsPopupMessages.BringToFront();
            grpOptionsScrollingMessages.BringToFront();
            grpOptionsScrollingMessages.Location = new Point(12, 10);
            grpOptionsPopupMessages.Location = new Point(367, 10);
            grpOptionsScrollingMessages.Size = new Size(340, 412);
            grpOptionsPopupMessages.Size = new Size(340, 412);
            chkOptionsScrollingMessageNewShipBuilt.Font = font_1;
            chkOptionsScrollingMessageRequestWarning.Font = font_1;
            chkOptionsScrollingMessageDiplomacyTreaties.Font = font_1;
            chkOptionsScrollingMessageWarTradeSanctions.Font = font_1;
            chkOptionsScrollingMessageColonyGainLoss.Font = font_1;
            chkOptionsScrollingMessageEmpireMetDestroyed.Font = font_1;
            chkOptionsScrollingMessageResearchBreakthrough.Font = font_1;
            chkOptionsScrollingMessageIntelligenceMissions.Font = font_1;
            chkOptionsScrollingMessageExploration.Font = font_1;
            chkOptionsScrollingMessageShipMissionComplete.Font = font_1;
            chkOptionsScrollingMessageShipNeedsRefuelling.Font = font_1;
            chkOptionsScrollingMessageUnderAttackCivilianShips.Font = font_1;
            chkOptionsScrollingMessageUnderAttackCivilianBases.Font = font_1;
            chkOptionsScrollingMessageUnderAttackExplorationShips.Font = font_1;
            chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Font = font_1;
            chkOptionsScrollingMessageUnderAttackMilitaryShips.Font = font_1;
            chkOptionsScrollingMessageUnderAttackOtherStateBases.Font = font_1;
            chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Font = font_1;
            chkOptionsScrollingMessageConstructionResourceShortage.Font = font_1;
            chkOptionsScrollingMessageNewShipBuilt.Location = new Point(7, 22);
            chkOptionsScrollingMessageRequestWarning.Location = new Point(7, 42);
            chkOptionsScrollingMessageDiplomacyTreaties.Location = new Point(7, 62);
            chkOptionsScrollingMessageWarTradeSanctions.Location = new Point(7, 82);
            chkOptionsScrollingMessageColonyGainLoss.Location = new Point(7, 102);
            chkOptionsScrollingMessageEmpireMetDestroyed.Location = new Point(7, 122);
            chkOptionsScrollingMessageResearchBreakthrough.Location = new Point(7, 142);
            chkOptionsScrollingMessageIntelligenceMissions.Location = new Point(7, 162);
            chkOptionsScrollingMessageExploration.Location = new Point(7, 182);
            chkOptionsScrollingMessageShipMissionComplete.Location = new Point(7, 202);
            chkOptionsScrollingMessageShipNeedsRefuelling.Location = new Point(7, 222);
            chkOptionsScrollingMessageUnderAttackCivilianShips.Location = new Point(7, 242);
            chkOptionsScrollingMessageUnderAttackCivilianBases.Location = new Point(7, 262);
            chkOptionsScrollingMessageUnderAttackExplorationShips.Location = new Point(7, 282);
            chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Location = new Point(7, 302);
            chkOptionsScrollingMessageUnderAttackMilitaryShips.Location = new Point(7, 322);
            chkOptionsScrollingMessageUnderAttackOtherStateBases.Location = new Point(7, 342);
            chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Location = new Point(7, 362);
            chkOptionsScrollingMessageConstructionResourceShortage.Location = new Point(7, 382);
            chkOptionsPopupMessageShipBuilt.Font = font_1;
            chkOptionsPopupMessageRequestWarning.Font = font_1;
            chkOptionsPopupMessageDiplomacyTreaties.Font = font_1;
            chkOptionsPopupMessageDiplomacyWarTradeSanctions.Font = font_1;
            chkOptionsPopupMessageColonyGainLoss.Font = font_1;
            chkOptionsPopupMessageEmpireMetDestroyed.Font = font_1;
            chkOptionsPopupMessageResearchBreakthrough.Font = font_1;
            chkOptionsPopupMessageIntelligenceMissions.Font = font_1;
            chkOptionsPopupMessageExploration.Font = font_1;
            chkOptionsPopupMessageShipMissionComplete.Font = font_1;
            chkOptionsPopupMessageShipNeedsRefuelling.Font = font_1;
            chkOptionsPopupMessageUnderAttackCivilianShips.Font = font_1;
            chkOptionsPopupMessageUnderAttackCivilianBases.Font = font_1;
            chkOptionsPopupMessageUnderAttackExplorationShips.Font = font_1;
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.Font = font_1;
            chkOptionsPopupMessageUnderAttackMilitaryShips.Font = font_1;
            chkOptionsPopupMessageUnderAttackOtherStateBases.Font = font_1;
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.Font = font_1;
            chkOptionsPopupMessageConstructionResourceShortage.Font = font_1;
            chkOptionsPopupMessageShipBuilt.Location = new Point(7, 22);
            chkOptionsPopupMessageRequestWarning.Location = new Point(7, 42);
            chkOptionsPopupMessageDiplomacyTreaties.Location = new Point(7, 62);
            chkOptionsPopupMessageDiplomacyWarTradeSanctions.Location = new Point(7, 82);
            chkOptionsPopupMessageColonyGainLoss.Location = new Point(7, 102);
            chkOptionsPopupMessageEmpireMetDestroyed.Location = new Point(7, 122);
            chkOptionsPopupMessageResearchBreakthrough.Location = new Point(7, 142);
            chkOptionsPopupMessageIntelligenceMissions.Location = new Point(7, 162);
            chkOptionsPopupMessageExploration.Location = new Point(7, 182);
            chkOptionsPopupMessageShipMissionComplete.Location = new Point(7, 202);
            chkOptionsPopupMessageShipNeedsRefuelling.Location = new Point(7, 222);
            chkOptionsPopupMessageUnderAttackCivilianShips.Location = new Point(7, 242);
            chkOptionsPopupMessageUnderAttackCivilianBases.Location = new Point(7, 262);
            chkOptionsPopupMessageUnderAttackExplorationShips.Location = new Point(7, 282);
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.Location = new Point(7, 302);
            chkOptionsPopupMessageUnderAttackMilitaryShips.Location = new Point(7, 322);
            chkOptionsPopupMessageUnderAttackOtherStateBases.Location = new Point(7, 342);
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.Location = new Point(7, 362);
            chkOptionsPopupMessageConstructionResourceShortage.Location = new Point(7, 382);
            chkOptionsPopupMessageUnderAttackCivilianShips.BringToFront();
            chkOptionsPopupMessageUnderAttackCivilianBases.BringToFront();
            chkOptionsPopupMessageUnderAttackExplorationShips.BringToFront();
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.BringToFront();
            chkOptionsPopupMessageUnderAttackMilitaryShips.BringToFront();
            chkOptionsPopupMessageUnderAttackOtherStateBases.BringToFront();
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.BringToFront();
            chkOptionsPopupMessageConstructionResourceShortage.BringToFront();
            chkOptionsScrollingMessageUnderAttackCivilianShips.BringToFront();
            chkOptionsScrollingMessageUnderAttackCivilianBases.BringToFront();
            chkOptionsScrollingMessageUnderAttackExplorationShips.BringToFront();
            chkOptionsScrollingMessageUnderAttackColonyConstructionShips.BringToFront();
            chkOptionsScrollingMessageUnderAttackMilitaryShips.BringToFront();
            chkOptionsScrollingMessageUnderAttackOtherStateBases.BringToFront();
            chkOptionsScrollingMessageUnderAttackColoniesSpaceports.BringToFront();
            chkOptionsScrollingMessageConstructionResourceShortage.BringToFront();
            method_168();
            pnlGameOptionsMessages.Visible = true;
            pnlGameOptionsMessages.BringToFront();
        }

        private void method_183()
        {
            method_167();
            pnlGameOptionsMessages.SendToBack();
            pnlGameOptionsMessages.Visible = false;
        }

        private void method_184()
        {
            pnlGameOptionsAdvancedDisplaySettings.Size = new Size(440, 520);
            pnlGameOptionsAdvancedDisplaySettings.Location = new Point((base.Width - pnlGameOptionsAdvancedDisplaySettings.Width) / 2, (base.Height - pnlGameOptionsAdvancedDisplaySettings.Height) / 2);
            pnlGameOptionsAdvancedDisplaySettings.DoLayout();
            grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Size = new Size(400, 60);
            grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Font = font_7;
            lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Location = new Point(177, 22);
            numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Location = new Point(127, 23);
            lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Font = font_1;
            chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Font = font_1;
            chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Location = new Point(8, 22);
            chkOptionsShowSystemNebulae.Location = new Point(15, 87);
            chkOptionsShowSystemNebulae.Font = font_1;
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Font = font_1;
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Size = new Size(400, 52);
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Location = new Point(12, 115);
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.LabelWidth = 160;
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Setup();
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.LinkWidth = 0;
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.LinkText = string.Empty;
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.SetLabels(new string[3]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Medium"),
            TextResolver.GetText("High")
            });
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Location = new Point(12, 184);
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Width = 400;
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Height = 231;
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Font = font_7;
            chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Font = font_1;
            chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Font = font_1;
            chkGameOptionsGalaxyDisplayAlwaysPirates.Font = font_1;
            chkGameOptionsGalaxyDisplayCivilianShips.Font = font_1;
            chkGameOptionsGalaxyDisplayColonyShips.Font = font_1;
            chkGameOptionsGalaxyDisplayConstructionShips.Font = font_1;
            chkGameOptionsGalaxyDisplayExplorationShips.Font = font_1;
            chkGameOptionsGalaxyDisplayFleets.Font = font_1;
            chkGameOptionsGalaxyDisplayMilitaryShips.Font = font_1;
            chkGameOptionsGalaxyDisplayOtherBases.Font = font_1;
            chkGameOptionsGalaxyDisplayResupplyShips.Font = font_1;
            chkGameOptionsGalaxyDisplaySpacePorts.Font = font_1;
            chkGameOptionsGalaxyDisplayFleets.Location = new Point(10, 22);
            chkGameOptionsGalaxyDisplayFleets.BringToFront();
            chkGameOptionsGalaxyDisplayResupplyShips.Location = new Point(10, 44);
            chkGameOptionsGalaxyDisplayResupplyShips.BringToFront();
            chkGameOptionsGalaxyDisplayMilitaryShips.Location = new Point(10, 66);
            chkGameOptionsGalaxyDisplayMilitaryShips.BringToFront();
            chkGameOptionsGalaxyDisplaySpacePorts.Location = new Point(10, 88);
            chkGameOptionsGalaxyDisplaySpacePorts.BringToFront();
            chkGameOptionsGalaxyDisplayOtherBases.Location = new Point(10, 110);
            chkGameOptionsGalaxyDisplayOtherBases.BringToFront();
            chkGameOptionsGalaxyDisplayExplorationShips.Location = new Point(180, 22);
            chkGameOptionsGalaxyDisplayExplorationShips.BringToFront();
            chkGameOptionsGalaxyDisplayColonyShips.Location = new Point(180, 44);
            chkGameOptionsGalaxyDisplayColonyShips.BringToFront();
            chkGameOptionsGalaxyDisplayConstructionShips.Location = new Point(180, 66);
            chkGameOptionsGalaxyDisplayConstructionShips.BringToFront();
            chkGameOptionsGalaxyDisplayCivilianShips.Location = new Point(180, 88);
            chkGameOptionsGalaxyDisplayCivilianShips.BringToFront();
            chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Location = new Point(10, 154);
            chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.BringToFront();
            chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Location = new Point(10, 176);
            chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.BringToFront();
            chkGameOptionsGalaxyDisplayAlwaysPirates.Location = new Point(10, 198);
            chkGameOptionsGalaxyDisplayAlwaysPirates.BringToFront();
            chkGameOptionsGalaxyDisplayCleanGalaxyView.Location = new Point(12, 425);
            chkGameOptionsGalaxyDisplayCleanGalaxyView.Font = font_1;
            if (main_0.gameOptions_0 != null)
            {
                chkOptionsShowSystemNebulae.Checked = main_0.gameOptions_0.ShowSystemNebulae;
                int systemNebulaeDetail = main_0.gameOptions_0.SystemNebulaeDetail;
                systemNebulaeDetail = Math.Max(0, Math.Min(2, systemNebulaeDetail));
                tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Value = systemNebulaeDetail;
                if (main_0.gameOptions_0.MaximumFramerate <= 0)
                {
                    chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Checked = true;
                    numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Enabled = false;
                }
                else
                {
                    chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Checked = false;
                    int maximumFramerate = main_0.gameOptions_0.MaximumFramerate;
                    maximumFramerate = Math.Max((int)numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Minimum, maximumFramerate);
                    maximumFramerate = Math.Min((int)numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Maximum, maximumFramerate);
                    numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Value = maximumFramerate;
                }
                chkGameOptionsGalaxyDisplayFleets.Checked = main_0.gameOptions_0.GalaxyViewDisplayFleets;
                chkGameOptionsGalaxyDisplayResupplyShips.Checked = main_0.gameOptions_0.GalaxyViewDisplayResupplyShips;
                chkGameOptionsGalaxyDisplayMilitaryShips.Checked = main_0.gameOptions_0.GalaxyViewDisplayMilitaryShips;
                chkGameOptionsGalaxyDisplaySpacePorts.Checked = main_0.gameOptions_0.GalaxyViewDisplaySpacePorts;
                chkGameOptionsGalaxyDisplayOtherBases.Checked = main_0.gameOptions_0.GalaxyViewDisplayOtherBases;
                chkGameOptionsGalaxyDisplayExplorationShips.Checked = main_0.gameOptions_0.GalaxyViewDisplayExplorationShips;
                chkGameOptionsGalaxyDisplayColonyShips.Checked = main_0.gameOptions_0.GalaxyViewDisplayColonyShips;
                chkGameOptionsGalaxyDisplayConstructionShips.Checked = main_0.gameOptions_0.GalaxyViewDisplayConstructionShips;
                chkGameOptionsGalaxyDisplayCivilianShips.Checked = main_0.gameOptions_0.GalaxyViewDisplayCivilianShips;
                chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Checked = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets;
                chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Checked = main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips;
                chkGameOptionsGalaxyDisplayAlwaysPirates.Checked = main_0.gameOptions_0.GalaxyViewDisplayAlwaysPirates;
                chkGameOptionsGalaxyDisplayCleanGalaxyView.Checked = main_0.gameOptions_0.CleanGalaxyView;
            }
            pnlGameOptionsAdvancedDisplaySettings.Visible = true;
            pnlGameOptionsAdvancedDisplaySettings.BringToFront();
        }

        private void method_185()
        {
            if (main_0.gameOptions_0 != null)
            {
                main_0.gameOptions_0.ShowSystemNebulae = chkOptionsShowSystemNebulae.Checked;
                main_0.gameOptions_0.SystemNebulaeDetail = tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.Value;
                main_0.SastWuBaXc(main_0.gameOptions_0);
                if (chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Checked)
                {
                    main_0.gameOptions_0.MaximumFramerate = -1;
                }
                else
                {
                    main_0.gameOptions_0.MaximumFramerate = (int)numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Value;
                }
                main_0.gameOptions_0.GalaxyViewDisplayFleets = chkGameOptionsGalaxyDisplayFleets.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayResupplyShips = chkGameOptionsGalaxyDisplayResupplyShips.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayMilitaryShips = chkGameOptionsGalaxyDisplayMilitaryShips.Checked;
                main_0.gameOptions_0.GalaxyViewDisplaySpacePorts = chkGameOptionsGalaxyDisplaySpacePorts.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayOtherBases = chkGameOptionsGalaxyDisplayOtherBases.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayExplorationShips = chkGameOptionsGalaxyDisplayExplorationShips.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayColonyShips = chkGameOptionsGalaxyDisplayColonyShips.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayConstructionShips = chkGameOptionsGalaxyDisplayConstructionShips.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayCivilianShips = chkGameOptionsGalaxyDisplayCivilianShips.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets = chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips = chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Checked;
                main_0.gameOptions_0.GalaxyViewDisplayAlwaysPirates = chkGameOptionsGalaxyDisplayAlwaysPirates.Checked;
                main_0.gameOptions_0.CleanGalaxyView = chkGameOptionsGalaxyDisplayCleanGalaxyView.Checked;
            }
            pnlGameOptionsAdvancedDisplaySettings.SendToBack();
            pnlGameOptionsAdvancedDisplaySettings.Visible = false;
        }

        private void btnGameOptionsAdvancedDisplaySettings_Click(object sender, EventArgs e)
        {
            method_184();
        }

        private void pnlGameOptionsAdvancedDisplaySettings_CloseButtonClicked(object sender, EventArgs e)
        {
            method_185();
        }

        private void chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited_CheckedChanged(object sender, EventArgs e)
        {
            if (main_0.gameOptions_0 != null)
            {
                if (chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Checked)
                {
                    main_0.gameOptions_0.MaximumFramerate = -1;
                    numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Enabled = false;
                }
                else
                {
                    main_0.gameOptions_0.MaximumFramerate = (int)numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Value;
                    numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Enabled = true;
                }
            }
        }

        private void btnStartNewGameColonizationTerritoryNext_Click(object sender, EventArgs e)
        {
            pnlStartNewGameColonizationTerritory.Visible = false;
            pnlStartNewGameYourRace.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Your Race");
            cmbStartNewGameYourEmpireRace.Focus();
            pnlStartNewGameYourRace.BringToFront();
        }

        private void btnStartNewGameColonizationTerritoryPrevious_Click(object sender, EventArgs e)
        {
            pnlStartNewGameColonizationTerritory.Visible = false;
            pnlStartNewGameTheGalaxy.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: The Galaxy");
            if (radStartNewGameGalaxyShapeElliptical.Checked)
            {
                radStartNewGameGalaxyShapeElliptical.Focus();
            }
            else if (radStartNewGameGalaxyShapeSpiral.Checked)
            {
                radStartNewGameGalaxyShapeSpiral.Focus();
            }
            else if (radStartNewGameGalaxyShapeRing.Checked)
            {
                radStartNewGameGalaxyShapeRing.Focus();
            }
            else if (radStartNewGameGalaxyShapeIrregular.Checked)
            {
                radStartNewGameGalaxyShapeIrregular.Focus();
            }
            else if (radStartNewGameGalaxyShapeClustersEven.Checked)
            {
                radStartNewGameGalaxyShapeClustersEven.Focus();
            }
            else if (radStartNewGameGalaxyShapeClustersVaried.Checked)
            {
                radStartNewGameGalaxyShapeClustersVaried.Focus();
            }
            pnlStartNewGameTheGalaxy.BringToFront();
        }

        private void btnStartNewGameYourRacePrevious_Click(object sender, EventArgs e)
        {
            pnlStartNewGameYourRace.Visible = false;
            pnlStartNewGameColonizationTerritory.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Colonization and Territory");
            method_187();
            tbarStartNewGameTheGalaxyColonyPrevalence.Focus();
            pnlStartNewGameColonizationTerritory.BringToFront();
        }

        private void btnStartNewGameYourRaceNext_Click(object sender, EventArgs e)
        {
            if (bool_2)
            {
                Race selectedRace = cmbStartNewGameYourEmpireRace.SelectedRace;
                PiratePlayStyle piratePlayStyle = method_193(cmbVictoryPiratePlayStyle.SelectedIndex);
                if (piratePlayStyle == PiratePlayStyle.Undefined && selectedRace != null)
                {
                    piratePlayStyle = selectedRace.DefaultPiratePlaystyle;
                }
                method_207(piratePlayStyle, bool_5: false);
                method_101(piratePlayStyle, bool_5: false);
            }
            pnlStartNewGameYourRace.Visible = false;
            pnlStartNewGameYourEmpire.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Your Empire");
            txtYourEmpireName.Focus();
            pnlStartNewGameYourEmpire.BringToFront();
        }

        private void btnStartNewGameOtherEmpiresNext_Click(object sender, EventArgs e)
        {
            if (tbarStartNewGameTheGalaxyExpansion.Value != 0)
            {
                chkStoryShadows.Checked = false;
                chkStoryShadows.Enabled = false;
            }
            else
            {
                chkStoryShadows.Enabled = true;
            }
            pnlStartNewGameOtherEmpires.Visible = false;
            pnlStartNewGameVictoryConditions.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Victory Conditions");
            chkVictoryTerritory.Focus();
            pnlStartNewGameVictoryConditions.BringToFront();
        }

        private void btnStartNewGameOtherEmpiresPrevious_Click(object sender, EventArgs e)
        {
            pnlStartNewGameOtherEmpires.Visible = false;
            pnlStartNewGameYourEmpire.Visible = true;
            if (bool_2)
            {
                pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Your Pirate Empire");
            }
            else
            {
                pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Your Empire");
            }
            txtYourEmpireName.Focus();
            pnlStartNewGameYourEmpire.BringToFront();
        }

        private bool method_186(int int_1)
        {
            if (int_1 > 700)
            {
                ComputerInfo computerInfo = new ComputerInfo();
                ulong totalPhysicalMemory = computerInfo.TotalPhysicalMemory;
                if (totalPhysicalMemory < 1992294400L)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnStartNewGameTheGalaxyNext_Click(object sender, EventArgs e)
        {
            int int_ = method_60(tbarStartNewGameTheGalaxyStarDensity.Value);
            if (!method_186(int_))
            {
                string text = TextResolver.GetText("Your computer does not have enough memory to play a galaxy of this size");
                MessageBox.Show(text, TextResolver.GetText("Not Enough Memory for this Galaxy Size"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            pnlStartNewGameTheGalaxy.Visible = false;
            pnlStartNewGameColonizationTerritory.Visible = true;
            method_187();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Colonization and Territory");
            tbarStartNewGameTheGalaxyColonyPrevalence.Focus();
            pnlStartNewGameColonizationTerritory.BringToFront();
        }

        private void method_187()
        {
            int int_ = method_60(tbarStartNewGameTheGalaxyStarDensity.Value);
            Size size = method_69(tbarStartNewGameTheGalaxyDimensions.Value);
            method_188(int_, size.Width, size.Height);
        }

        private void method_188(int int_1, int int_2, int int_3)
        {
            double num = method_189(int_1, int_2, int_3);
            string text = string.Format(TextResolver.GetText("Colony Influence Range Suggestion"), int_1, int_2, int_3, num.ToString("0%"));
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeSuggestion.Text = text;
        }

        private double method_189(int int_1, int int_2, int int_3)
        {
            double val = Math.Sqrt((double)(int_2 * int_3) / (double)int_1 * 7.0);
            return Math.Max(0.5, Math.Min(2.0, val));
        }

        private int method_190()
        {
            //int num = 0;
            return cmbStartNewGameTheGalaxyPirateProximity.SelectedIndex switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                _ => 0,
            };
        }

        private PiratePlayStyle method_191()
        {
            //PiratePlayStyle piratePlayStyle = PiratePlayStyle.Undefined;
            return cmbVictoryPiratePlayStyle.SelectedIndex switch
            {
                0 => PiratePlayStyle.Balanced,
                1 => PiratePlayStyle.Pirate,
                2 => PiratePlayStyle.Mercenary,
                3 => PiratePlayStyle.Smuggler,
                _ => PiratePlayStyle.Balanced,
            };
        }

        private PiratePlayStyle method_192()
        {
            //PiratePlayStyle piratePlayStyle = PiratePlayStyle.Undefined;
            return cmbJumpStartVictoryPiratePlayStyle.SelectedIndex switch
            {
                0 => PiratePlayStyle.Balanced,
                1 => PiratePlayStyle.Pirate,
                2 => PiratePlayStyle.Mercenary,
                3 => PiratePlayStyle.Smuggler,
                _ => PiratePlayStyle.Balanced,
            };
        }

        private PiratePlayStyle method_193(int int_1)
        {
            //PiratePlayStyle piratePlayStyle = PiratePlayStyle.Undefined;
            return int_1 switch
            {
                0 => PiratePlayStyle.Balanced,
                1 => PiratePlayStyle.Pirate,
                2 => PiratePlayStyle.Mercenary,
                3 => PiratePlayStyle.Smuggler,
                _ => PiratePlayStyle.Undefined,
            };
        }

        private StartGameOptions method_194()
        {
            StartGameOptions startGameOptions = method_195();
            startGameOptions.GalaxySize = tbarJumpStartTheGalaxyStarDensity.Value;
            startGameOptions.GalaxyDimensions = tbarJumpStartTheGalaxyDimensions.Value;
            startGameOptions.GalaxyDifficulty = tbarJumpStartTheGalaxyDifficulty.Value;
            startGameOptions.GalaxyDifficultyScaling = chkJumpStartTheGalaxyDifficultyScaling.Checked;
            startGameOptions.YourEmpireRace = cmbJumpStartYourEmpireRace.SelectedIndex;
            if (bool_2)
            {
                startGameOptions.PiratePlayStyle = (int)(method_192() - 1);
            }
            else
            {
                startGameOptions.YourEmpireGovernmentStyle = cmbJumpStartYourEmpireGovernment.SelectedGovernmentId;
            }
            if (radJumpStartGalaxyShapeElliptical.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.Elliptical;
            }
            else if (radJumpStartGalaxyShapeIrregular.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.Irregular;
            }
            else if (radJumpStartGalaxyShapeRing.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.Ring;
            }
            else if (radJumpStartGalaxyShapeSpiral.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.Spiral;
            }
            else if (radJumpStartGalaxyShapeEvenClusters.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.ClustersEven;
            }
            else if (radJumpStartGalaxyShapeVariedClusters.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.ClustersVaried;
            }
            return startGameOptions;
        }

        private StartGameOptions method_195()
        {
            StartGameOptions startGameOptions = new StartGameOptions();
            startGameOptions.GalaxyAggression = tbarStartNewGameTheGalaxyAggression.Value;
            startGameOptions.GalaxyHabitatQuality = tbarStartNewGameTheGalaxyColonyPrevalence.Value;
            startGameOptions.GalaxyAlienLifePrevalence = tbarStartNewGameTheGalaxyAlienLife.Value;
            startGameOptions.GalaxyExpansion = tbarStartNewGameTheGalaxyExpansion.Value;
            startGameOptions.GalaxyPirates = tbarStartNewGameTheGalaxyPirates.Value;
            startGameOptions.GalaxyPirateStrength = tbarStartNewGameTheGalaxyPirateStrength.Value;
            startGameOptions.GalaxyPirateProximity = method_190();
            startGameOptions.GalaxyResearchSpeed = (int)numStartNewGameTheGalaxyResearchBaseTech.Value;
            if (radStartNewGameGalaxyShapeElliptical.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.Elliptical;
            }
            else if (radStartNewGameGalaxyShapeIrregular.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.Irregular;
            }
            else if (radStartNewGameGalaxyShapeRing.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.Ring;
            }
            else if (radStartNewGameGalaxyShapeSpiral.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.Spiral;
            }
            else if (radStartNewGameGalaxyShapeClustersEven.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.ClustersEven;
            }
            else if (radStartNewGameGalaxyShapeClustersVaried.Checked)
            {
                startGameOptions.GalaxyShape = GalaxyShape.ClustersVaried;
            }
            startGameOptions.GalaxySize = tbarStartNewGameTheGalaxyStarDensity.Value;
            startGameOptions.GalaxyDimensions = tbarStartNewGameTheGalaxyDimensions.Value;
            startGameOptions.GalaxySpaceCreatures = tbarStartNewGameTheGalaxySpaceCreatures.Value;
            startGameOptions.ColonizationInfluenceRangeFactor = (float)sldStartNewGameColonizationTerritoryColonyInfluenceRange.Value / 100f;
            startGameOptions.ColonizationRangeEnforceLimit = chkStartNewGameColonizationTerritoryEnforceColonizationRange.Checked;
            startGameOptions.ColonizationRange = (float)sldStartNewGameColonizationTerritoryColonizationRange.Value / 1000f;
            startGameOptions.YourEmpireExpansion = tbarStartNewGameYourEmpireSize.Value;
            startGameOptions.YourEmpireFlagShape = cmbFlagShape.SelectedIndex;
            startGameOptions.YourEmpireGalaxyStartLocation = cmbYourEmpireStartLocation.SelectedIndex;
            _ = raceList_0[0];
            if (cmbStartNewGameYourEmpireRace.SelectedRace != null)
            {
                _ = cmbStartNewGameYourEmpireRace.SelectedRace;
            }
            startGameOptions.YourEmpireGovernmentStyle = cmbStartNewGameYourEmpireGovernment.SelectedGovernmentId;
            startGameOptions.YourEmpireHomeSystem = tbarStartNewGameYourEmpireHomeSystem.Value;
            startGameOptions.YourEmpireMainColor = cmbPrimaryColor.SelectedIndex;
            startGameOptions.YourEmpireName = txtYourEmpireName.Text;
            startGameOptions.YourEmpireRace = cmbStartNewGameYourEmpireRace.SelectedIndex;
            startGameOptions.YourEmpireSecondaryColor = cmbSecondaryColor.SelectedIndex;
            startGameOptions.YourEmpireTechLevel = tbarStartNewGameYourEmpireTechLevel.Value;
            startGameOptions.YourEmpireCorruption = tbarStartNewGameYourEmpireCorruption.Value;
            string string_ = method_58(startGameOptions.GalaxyExpansion);
            string string_2 = method_55(method_89(startGameOptions.GalaxyExpansion));
            startGameOptions.OtherEmpires = method_199(string_, string_2);
            startGameOptions.OtherEmpiresAllowNewEmpiresFromIndependentColonies = chkGalaxyNewEmpiresDuringGame.Checked;
            startGameOptions.OtherEmpiresAutoGen = chkOtherEmpiresAutogenerate.Checked;
            startGameOptions.OtherEmpiresAutoGenAmount = (int)numAutogenerateEmpiresAmount.Value;
            startGameOptions.VictoryConditionsApplyWhen = chkVictoryTimeStart.Checked;
            startGameOptions.VictoryConditionsApplyWhenYears = (int)numVictoryTimeStartYears.Value;
            startGameOptions.VictoryConditionsEconomy = chkVictoryEconomy.Checked;
            startGameOptions.VictoryConditionsEconomyPercent = (int)numVictoryEconomyPercent.Value;
            startGameOptions.VictoryConditionsPopulation = chkVictoryPopulation.Checked;
            startGameOptions.VictoryConditionsPopulationPercent = (int)numVictoryPopulationPercent.Value;
            startGameOptions.VictoryConditionsTerritory = chkVictoryTerritory.Checked;
            startGameOptions.VictoryConditionsTerritoryPercent = (int)numVictoryTerritoryPercent.Value;
            startGameOptions.VictoryConditionsTimeLimit = chkVictoryTimeLimit.Checked;
            startGameOptions.VictoryConditionsTimeLimitYears = (int)numVictoryTimeLimitYears.Value;
            if (wjhRtsSwmsa == "CustomPirate")
            {
                startGameOptions.VictoryConditionsStoryEvents = main_0.gameOptions_0.StartGameOptions.VictoryConditionsStoryEvents;
            }
            else
            {
                startGameOptions.VictoryConditionsStoryEvents = chkStoryReturnOfTheShakturi.Checked;
            }
            startGameOptions.VictoryConditionsStoryEventsOriginal = chkStoryDistantWorlds.Checked;
            startGameOptions.PiratePlayStyle = (int)(method_191() - 1);
            startGameOptions.VictoryConditionsRaceSpecific = chkVictoryEnableRaceSpecificConditions.Checked;
            startGameOptions.VictoryConditionsDisasterEvents = chkVictoryEnableDisasterEvents.Checked;
            startGameOptions.GalaxyDifficulty = tbarStartNewGameTheGalaxyDifficulty.Value;
            startGameOptions.GalaxyDifficultyScaling = chkStartNewGameTheGalaxyDifficultyScaling.Checked;
            startGameOptions.DestroyedPiratesDoNotRespawn = chkStartNewGameTheGalaxyPiratesRespawn.Checked;
            startGameOptions.VictoryConditionsVictoryThresholdPercent = cmbVictoryThresholdPercentage.SelectedIndex;
            startGameOptions.VictoryConditionsRaceSpecificEvents = chkVictoryEnableRaceSpecificEvents.Checked;
            if ((wjhRtsSwmsa == "CustomStandard" || wjhRtsSwmsa == "CustomPirate") && tbarStartNewGameTheGalaxyExpansion.Value == 0)
            {
                startGameOptions.VictoryConditionsStoryEventsShadows = chkStoryShadows.Checked;
            }
            else
            {
                startGameOptions.VictoryConditionsStoryEventsShadows = main_0.gameOptions_0.StartGameOptions.VictoryConditionsStoryEventsShadows;
            }
            startGameOptions.AllowTechTrading = chkStartNewGameEnableTechTrading.Checked;
            startGameOptions.AllowGiantKaltorGeneration = chkStartNewGameEnableGiantKaltors.Checked;
            return startGameOptions;
        }

        private void method_196(StartGameOptions startGameOptions_0)
        {
            //bool_4 = false;
            tbarJumpStartTheGalaxyStarDensity.Value = startGameOptions_0.GalaxySize;
            tbarJumpStartTheGalaxyDimensions.Value = startGameOptions_0.GalaxyDimensions;
            tbarJumpStartTheGalaxyDifficulty.Value = startGameOptions_0.GalaxyDifficulty;
            chkJumpStartTheGalaxyDifficultyScaling.Checked = startGameOptions_0.GalaxyDifficultyScaling;
            if (startGameOptions_0.YourEmpireRace >= 0 && startGameOptions_0.YourEmpireRace < cmbJumpStartYourEmpireRace.Items.Count)
            {
                cmbJumpStartYourEmpireRace.SelectedIndex = startGameOptions_0.YourEmpireRace;
            }
            cmbJumpStartYourEmpireGovernment.SetSelectedGovernmentStyle(startGameOptions_0.YourEmpireGovernmentStyle);
            cmbJumpStartVictoryPiratePlayStyle.SelectedIndex = startGameOptions_0.PiratePlayStyle;
            tbarStartNewGameTheGalaxyAggression.Value = startGameOptions_0.GalaxyAggression;
            tbarStartNewGameTheGalaxyDifficulty.Value = startGameOptions_0.GalaxyDifficulty;
            chkStartNewGameTheGalaxyDifficultyScaling.Checked = startGameOptions_0.GalaxyDifficultyScaling;
            chkStartNewGameTheGalaxyPiratesRespawn.Checked = startGameOptions_0.DestroyedPiratesDoNotRespawn;
            tbarStartNewGameTheGalaxyColonyPrevalence.Value = startGameOptions_0.GalaxyHabitatQuality;
            tbarStartNewGameTheGalaxyAlienLife.Value = startGameOptions_0.GalaxyAlienLifePrevalence;
            tbarStartNewGameTheGalaxyExpansion.Value = startGameOptions_0.GalaxyExpansion;
            tbarStartNewGameTheGalaxyPirates.Value = startGameOptions_0.GalaxyPirates;
            tbarStartNewGameTheGalaxyPirateStrength.Value = startGameOptions_0.GalaxyPirateStrength;
            cmbStartNewGameTheGalaxyPirateProximity.SelectedIndex = startGameOptions_0.GalaxyPirateProximity;
            tbarStartNewGameTheGalaxyResearchSpeed.Value = method_209(startGameOptions_0.GalaxyResearchSpeed * 1000);
            numStartNewGameTheGalaxyResearchBaseTech.Value = startGameOptions_0.GalaxyResearchSpeed;
            switch (startGameOptions_0.GalaxyShape)
            {
                case GalaxyShape.Spiral:
                    radStartNewGameGalaxyShapeSpiral.Checked = true;
                    radJumpStartGalaxyShapeSpiral.Checked = true;
                    break;
                case GalaxyShape.Elliptical:
                    radStartNewGameGalaxyShapeElliptical.Checked = true;
                    radJumpStartGalaxyShapeElliptical.Checked = true;
                    break;
                case GalaxyShape.Irregular:
                    radStartNewGameGalaxyShapeIrregular.Checked = true;
                    radJumpStartGalaxyShapeIrregular.Checked = true;
                    break;
                case GalaxyShape.Ring:
                    radStartNewGameGalaxyShapeRing.Checked = true;
                    radJumpStartGalaxyShapeRing.Checked = true;
                    break;
                case GalaxyShape.ClustersEven:
                    radStartNewGameGalaxyShapeClustersEven.Checked = true;
                    radJumpStartGalaxyShapeEvenClusters.Checked = true;
                    break;
                case GalaxyShape.ClustersVaried:
                    radStartNewGameGalaxyShapeClustersVaried.Checked = true;
                    radJumpStartGalaxyShapeVariedClusters.Checked = true;
                    break;
            }
            tbarStartNewGameTheGalaxyDimensions.Value = startGameOptions_0.GalaxyDimensions;
            tbarStartNewGameTheGalaxyStarDensity.Value = startGameOptions_0.GalaxySize;
            tbarStartNewGameTheGalaxySpaceCreatures.Value = startGameOptions_0.GalaxySpaceCreatures;
            int val = (int)(startGameOptions_0.ColonizationInfluenceRangeFactor * 100f);
            sldStartNewGameColonizationTerritoryColonyInfluenceRange.Value = Math.Max(sldStartNewGameColonizationTerritoryColonyInfluenceRange.Minimum, Math.Min(sldStartNewGameColonizationTerritoryColonyInfluenceRange.Maximum, val));
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeValue.Text = sldStartNewGameColonizationTerritoryColonyInfluenceRange.Value + "%";
            chkStartNewGameColonizationTerritoryEnforceColonizationRange.Checked = startGameOptions_0.ColonizationRangeEnforceLimit;
            grpStartNewGameColonizationTerritoryColonizationRange.Enabled = startGameOptions_0.ColonizationRangeEnforceLimit;
            int val2 = (int)(startGameOptions_0.ColonizationRange * 1000f);
            sldStartNewGameColonizationTerritoryColonizationRange.Value = Math.Max(sldStartNewGameColonizationTerritoryColonizationRange.Minimum, Math.Min(sldStartNewGameColonizationTerritoryColonizationRange.Maximum, val2));
            lblStartNewGameColonizationTerritoryColonizationRangeValue.Text = ((float)sldStartNewGameColonizationTerritoryColonizationRange.Value / 1000f).ToString("#0.00") + "  " + TextResolver.GetText("sectors");
            tbarStartNewGameYourEmpireSize.Value = startGameOptions_0.YourEmpireExpansion;
            if (startGameOptions_0.YourEmpireFlagShape >= 0 && startGameOptions_0.YourEmpireFlagShape < cmbFlagShape.Items.Count)
            {
                cmbFlagShape.SelectedIndex = startGameOptions_0.YourEmpireFlagShape;
            }
            if (startGameOptions_0.YourEmpireGalaxyStartLocation >= 0 && startGameOptions_0.YourEmpireGalaxyStartLocation < cmbYourEmpireStartLocation.Items.Count)
            {
                cmbYourEmpireStartLocation.SelectedIndex = startGameOptions_0.YourEmpireGalaxyStartLocation;
            }
            tbarStartNewGameYourEmpireHomeSystem.Value = startGameOptions_0.YourEmpireHomeSystem;
            if (startGameOptions_0.YourEmpireMainColor >= 0 && startGameOptions_0.YourEmpireMainColor < cmbPrimaryColor.Items.Count)
            {
                cmbPrimaryColor.SelectedIndex = startGameOptions_0.YourEmpireMainColor;
            }
            txtYourEmpireName.Text = startGameOptions_0.YourEmpireName;
            if (startGameOptions_0.YourEmpireRace >= 0 && startGameOptions_0.YourEmpireRace < cmbStartNewGameYourEmpireRace.Items.Count)
            {
                cmbStartNewGameYourEmpireRace.SelectedIndex = startGameOptions_0.YourEmpireRace;
            }
            if (startGameOptions_0.YourEmpireSecondaryColor >= 0 && startGameOptions_0.YourEmpireSecondaryColor < cmbSecondaryColor.Items.Count)
            {
                cmbSecondaryColor.SelectedIndex = startGameOptions_0.YourEmpireSecondaryColor;
            }
            tbarStartNewGameYourEmpireTechLevel.Value = startGameOptions_0.YourEmpireTechLevel;
            tbarStartNewGameYourEmpireCorruption.Value = startGameOptions_0.YourEmpireCorruption;
            cmbStartNewGameYourEmpireGovernment.SetSelectedGovernmentStyle(startGameOptions_0.YourEmpireGovernmentStyle);
            method_198(startGameOptions_0.OtherEmpires);
            chkGalaxyNewEmpiresDuringGame.Checked = startGameOptions_0.OtherEmpiresAllowNewEmpiresFromIndependentColonies;
            chkOtherEmpiresAutogenerate.Checked = startGameOptions_0.OtherEmpiresAutoGen;
            numAutogenerateEmpiresAmount.Value = Math.Min((int)numAutogenerateEmpiresAmount.Maximum, startGameOptions_0.OtherEmpiresAutoGenAmount);
            method_197();
            chkVictoryTimeStart.Checked = startGameOptions_0.VictoryConditionsApplyWhen;
            numVictoryTimeStartYears.Value = startGameOptions_0.VictoryConditionsApplyWhenYears;
            chkVictoryEconomy.Checked = startGameOptions_0.VictoryConditionsEconomy;
            numVictoryEconomyPercent.Value = startGameOptions_0.VictoryConditionsEconomyPercent;
            chkVictoryPopulation.Checked = startGameOptions_0.VictoryConditionsPopulation;
            numVictoryPopulationPercent.Value = startGameOptions_0.VictoryConditionsPopulationPercent;
            chkVictoryTerritory.Checked = startGameOptions_0.VictoryConditionsTerritory;
            numVictoryTerritoryPercent.Value = startGameOptions_0.VictoryConditionsTerritoryPercent;
            chkVictoryTimeLimit.Checked = startGameOptions_0.VictoryConditionsTimeLimit;
            numVictoryTimeLimitYears.Value = startGameOptions_0.VictoryConditionsTimeLimitYears;
            chkStoryReturnOfTheShakturi.Checked = startGameOptions_0.VictoryConditionsStoryEvents;
            chkStoryDistantWorlds.Checked = startGameOptions_0.VictoryConditionsStoryEventsOriginal;
            chkStoryShadows.Checked = startGameOptions_0.VictoryConditionsStoryEventsShadows;
            chkStartNewGameEnableTechTrading.Checked = startGameOptions_0.AllowTechTrading;
            chkStartNewGameEnableGiantKaltors.Checked = startGameOptions_0.AllowGiantKaltorGeneration;
            cmbVictoryPiratePlayStyle.SelectedIndex = startGameOptions_0.PiratePlayStyle;
            chkVictoryEnableDisasterEvents.Checked = startGameOptions_0.VictoryConditionsDisasterEvents;
            chkVictoryEnableRaceSpecificConditions.Checked = startGameOptions_0.VictoryConditionsRaceSpecific;
            chkVictoryEnableRaceSpecificEvents.Checked = startGameOptions_0.VictoryConditionsRaceSpecificEvents;
            if (startGameOptions_0.VictoryConditionsVictoryThresholdPercent >= 0 && startGameOptions_0.VictoryConditionsVictoryThresholdPercent < cmbVictoryThresholdPercentage.Items.Count)
            {
                cmbVictoryThresholdPercentage.SelectedIndex = startGameOptions_0.VictoryConditionsVictoryThresholdPercent;
            }
            //bool_4 = true;
        }

        private void method_197()
        {
            if (chkOtherEmpiresAutogenerate.Checked)
            {
                ctlStartingEmpiresList.Enabled = false;
                method_99();
                btnAddNewEmpire.Enabled = false;
                numAutogenerateEmpiresAmount.Enabled = true;
            }
            else
            {
                numAutogenerateEmpiresAmount.Enabled = false;
                btnAddNewEmpire.Enabled = true;
                ctlStartingEmpiresList.Enabled = true;
                method_99();
            }
        }

        private void method_198(EmpireStartList empireStartList_0)
        {
            ctlStartingEmpiresList.Grid.Rows.Clear();
            foreach (EmpireStart item in empireStartList_0)
            {
                int index = ctlStartingEmpiresList.Grid.Rows.Add();
                ctlStartingEmpiresList.Grid.Rows[index].Cells["Name"].Value = item.Name;
                if (((DataGridViewComboBoxCell)ctlStartingEmpiresList.Grid.Rows[index].Cells["Government"]).Items.Contains(item.GovernmentStyle))
                {
                    ctlStartingEmpiresList.Grid.Rows[index].Cells["Government"].Value = item.GovernmentStyle;
                }
                else
                {
                    ctlStartingEmpiresList.Grid.Rows[index].Cells["Government"].Value = "(" + TextResolver.GetText("Random") + ")";
                }
                if (((DataGridViewComboBoxCell)ctlStartingEmpiresList.Grid.Rows[index].Cells["Proximity"]).Items.Contains(item.ProximityDistance))
                {
                    ctlStartingEmpiresList.Grid.Rows[index].Cells["Proximity"].Value = item.ProximityDistance;
                }
                else
                {
                    ctlStartingEmpiresList.Grid.Rows[index].Cells["Proximity"].Value = "(" + TextResolver.GetText("Random") + ")";
                }
                ctlStartingEmpiresList.Grid.Rows[index].Cells["HomeSystem"].Value = item.HomeSystemFavourability;
                ctlStartingEmpiresList.Grid.Rows[index].Cells["Size"].Value = method_58(item.Age);
                ctlStartingEmpiresList.Grid.Rows[index].Cells["TechLevel"].Value = method_55(item.TechLevel);
                DataGridViewComboBoxCell dataGridViewComboBoxCell = (DataGridViewComboBoxCell)ctlStartingEmpiresList.Grid.Rows[index].Cells["Race"];
                if (dataGridViewComboBoxCell.Items != null && dataGridViewComboBoxCell.Items.Count > 0 && dataGridViewComboBoxCell.Items.Contains(item.Race))
                {
                    ctlStartingEmpiresList.Grid.Rows[index].Cells["Race"].Value = item.Race;
                }
            }
        }

        private EmpireStartList method_199(string string_2, string string_3)
        {
            EmpireStartList empireStartList = new EmpireStartList();
            int count = ctlStartingEmpiresList.Grid.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                EmpireStart empireStart = new EmpireStart();
                object obj = ctlStartingEmpiresList.Grid.Rows[i].Cells["Name"].Value;
                if (obj == null)
                {
                    obj = string.Empty;
                }
                empireStart.Name = obj.ToString();
                empireStart.GovernmentStyle = ctlStartingEmpiresList.Grid.Rows[i].Cells["Government"].Value.ToString();
                empireStart.ProximityDistance = ctlStartingEmpiresList.Grid.Rows[i].Cells["Proximity"].Value.ToString();
                empireStart.HomeSystemFavourability = ctlStartingEmpiresList.Grid.Rows[i].Cells["HomeSystem"].Value.ToString();
                string text = ctlStartingEmpiresList.Grid.Rows[i].Cells["Size"].Value.ToString();
                if (text.ToLower(CultureInfo.InvariantCulture) == "(" + TextResolver.GetText("random") + ")")
                {
                    empireStart.Age = -1;
                }
                else
                {
                    empireStart.Age = method_57(text);
                }
                string text2 = ctlStartingEmpiresList.Grid.Rows[i].Cells["TechLevel"].Value.ToString();
                if (text2.ToLower(CultureInfo.InvariantCulture) == "(" + TextResolver.GetText("random") + ")")
                {
                    empireStart.TechLevel = -1.0;
                }
                else
                {
                    if (text2 == TextResolver.GetText("Starting"))
                    {
                        text2 = TextResolver.GetText("Normal");
                    }
                    empireStart.TechLevel = method_54(text2);
                }
                empireStart.Race = ctlStartingEmpiresList.Grid.Rows[i].Cells["Race"].Value.ToString();
                empireStartList.Add(empireStart);
            }
            return empireStartList;
        }

        private EmpireStartList method_200(EmpireStartList empireStartList_0, int int_1, bool bool_5)
        {
            if (empireStartList_0 != null)
            {
                for (int i = 0; i < empireStartList_0.Count; i++)
                {
                    empireStartList_0[i].DifficultyLevel = method_201(int_1);
                }
            }
            return empireStartList_0;
        }

        private double method_201(int int_1)
        {
            double result = 1.0;
            switch (int_1)
            {
                case 0:
                    result = 0.7;
                    break;
                case 1:
                    result = 1.0;
                    break;
                case 2:
                    result = 1.25;
                    break;
                case 3:
                    result = 1.6;
                    break;
                case 4:
                    result = 2.0;
                    break;
            }
            return result;
        }

        private EmpireStart method_202(EmpireStart empireStart_0, int int_1, bool bool_5)
        {
            if (empireStart_0 != null)
            {
                switch (int_1)
                {
                    case 0:
                        if (bool_5)
                        {
                            empireStart_0.ColonyRevenueFactor = 1.4;
                        }
                        break;
                    case 1:
                        if (bool_5)
                        {
                            empireStart_0.ColonyRevenueFactor = 1.2;
                        }
                        break;
                    case 3:
                        if (!bool_5)
                        {
                            empireStart_0.ColonyRevenueFactor = 1.2;
                        }
                        break;
                    case 4:
                        if (!bool_5)
                        {
                            empireStart_0.ColonyRevenueFactor = 1.4;
                        }
                        break;
                }
            }
            return empireStart_0;
        }

        private void btnStartNewGameStart_Click(object sender, EventArgs e)
        {
            method_140();
            main_0.gameOptions_0.StartGameOptions = method_195();
            main_0.YxwyUefOyQ();
            main_0.method_257();
            method_46();
            Random random_ = new Random((int)DateTime.Now.Ticks);
            GalaxyShape galaxyShape = method_59(method_96());
            int num = method_60(tbarStartNewGameTheGalaxyStarDensity.Value);
            int num2 = method_61(tbarStartNewGameTheGalaxyStarDensity.Value, raceList_0);
            bool @checked = chkGalaxyNewEmpiresDuringGame.Checked;
            double num3 = method_64(tbarStartNewGameTheGalaxyColonyPrevalence.Value);
            int num4 = method_67(tbarStartNewGameTheGalaxyAlienLife.Value);
            double num5 = method_62(tbarStartNewGameTheGalaxySpaceCreatures.Value);
            double num6 = method_66(tbarStartNewGameTheGalaxyPirates.Value);
            int num7 = method_190();
            double num8 = (double)(numStartNewGameTheGalaxyResearchBaseTech.Value * 1000m);
            double num9 = method_71(tbarStartNewGameTheGalaxyAggression.Value);
            int value = tbarStartNewGameTheGalaxyExpansion.Value;
            string string_ = method_58(value);
            EmpireStart empireStart = new EmpireStart();
            empireStart.Name = txtYourEmpireName.Text;
            if (cmbStartNewGameYourEmpireRace.SelectedRace != null)
            {
                empireStart.Race = cmbStartNewGameYourEmpireRace.SelectedRace.Name;
            }
            else
            {
                empireStart.Race = "(" + TextResolver.GetText("Random") + ")";
            }
            empireStart.GovernmentStyle = method_72();
            empireStart.StartLocation = cmbYourEmpireStartLocation.SelectedItem.ToString();
            empireStart.HomeSystemFavourability = ahrJhtHrDu();
            string text = method_74();
            if (text == TextResolver.GetText("Starting") && value == 0)
            {
                text = TextResolver.GetText("PreWarp");
            }
            empireStart.Age = method_57(text);
            empireStart.TechLevel = method_54(method_75());
            empireStart.PrimaryColor = cmbPrimaryColor.SelectedColor;
            empireStart.SecondaryColor = cmbSecondaryColor.SelectedColor;
            empireStart.FlagShape = cmbFlagShape.SelectedIndex;
            empireStart.CorruptionMultiplier = method_63(tbarStartNewGameYourEmpireCorruption.Value);
            empireStart.PiratePlayStyle = method_191();
            switch (tbarStartNewGameTheGalaxyPirateStrength.Value)
            {
                case 0:
                    empireStart.PirateShipMaintenanceFactor = 1.0;
                    break;
                case 1:
                    empireStart.PirateShipMaintenanceFactor = 0.7;
                    break;
                case 2:
                    empireStart.PirateShipMaintenanceFactor = 0.4;
                    break;
                case 3:
                    empireStart.PirateShipMaintenanceFactor = 0.25;
                    break;
            }
            empireStart.AllowTechTrading = chkStartNewGameEnableTechTrading.Checked;
            empireStart.AllowGiantKaltorGeneration = chkStartNewGameEnableGiantKaltors.Checked;
            empireStart.DifficultyLevel = method_201(tbarStartNewGameTheGalaxyDifficulty.Value);
            empireStart.DifficultyScaling = chkStartNewGameTheGalaxyDifficultyScaling.Checked;
            empireStart.DestroyedPiratesDoNotRespawn = chkStartNewGameTheGalaxyPiratesRespawn.Checked;
            Size size = method_69(tbarStartNewGameTheGalaxyDimensions.Value);
            empireStart.GalaxySectorX = size.Width;
            empireStart.GalaxySectorY = size.Height;
            empireStart.EmpireTerritoryColonyInfluenceRangeFactor = (float)sldStartNewGameColonizationTerritoryColonyInfluenceRange.Value / 100f;
            empireStart.ColonizationRangeEnforceLimit = chkStartNewGameColonizationTerritoryEnforceColonizationRange.Checked;
            empireStart.ColonizationRange = (float)sldStartNewGameColonizationTerritoryColonizationRange.Value / 1000f * (float)Galaxy.SectorSize;
            EmpireStartList empireStartList = new EmpireStartList();
            if (chkOtherEmpiresAutogenerate.Checked)
            {
                int num10 = (int)numAutogenerateEmpiresAmount.Value;
                for (int i = 0; i < num10; i++)
                {
                    EmpireStart empireStart2 = new EmpireStart();
                    empireStart2.Name = string.Empty;
                    empireStart2.GovernmentStyle = "(" + TextResolver.GetText("Random") + ")";
                    empireStart2.ProximityDistance = "(" + TextResolver.GetText("Random") + ")";
                    empireStart2.HomeSystemFavourability = TextResolver.GetText("Normal");
                    string string_2 = method_109(string_, random_);
                    empireStart2.Age = method_57(string_2);
                    empireStart2.TechLevel = method_89(value);
                    empireStart2.Race = "(" + TextResolver.GetText("Random") + ")";
                    empireStartList.Add(empireStart2);
                }
            }
            else
            {
                string string_3 = method_55(method_89(value));
                empireStartList = method_199(string_, string_3);
            }
            empireStartList = method_200(empireStartList, tbarStartNewGameTheGalaxyDifficulty.Value, bool_5: false);
            long startStarDate = Galaxy.StartStarDate;
            startStarDate += value * 30000000;
            VictoryConditions victoryConditions = new VictoryConditions();
            victoryConditions.Economy = chkVictoryEconomy.Checked;
            if (victoryConditions.Economy)
            {
                victoryConditions.EconomyPercent = (double)numVictoryEconomyPercent.Value;
            }
            victoryConditions.Population = chkVictoryPopulation.Checked;
            if (victoryConditions.Population)
            {
                victoryConditions.PopulationPercent = (double)numVictoryPopulationPercent.Value;
            }
            victoryConditions.Territory = chkVictoryTerritory.Checked;
            if (victoryConditions.Territory)
            {
                victoryConditions.TerritoryPercent = (double)numVictoryTerritoryPercent.Value;
            }
            victoryConditions.TimeLimit = chkVictoryTimeLimit.Checked;
            victoryConditions.TimeLimitDate = startStarDate + (int)numVictoryTimeLimitYears.Value * Galaxy.RealSecondsInGalacticYear * 1000;
            if (chkVictoryTimeStart.Checked)
            {
                victoryConditions.StartDate = startStarDate + (int)numVictoryTimeStartYears.Value * Galaxy.RealSecondsInGalacticYear * 1000;
            }
            else
            {
                victoryConditions.StartDate = 0L;
            }
            victoryConditions.EnableStoryEvents = chkStoryReturnOfTheShakturi.Checked;
            victoryConditions.EnableDisasterEvents = chkVictoryEnableDisasterEvents.Checked;
            victoryConditions.EnableRaceSpecificEvents = chkVictoryEnableRaceSpecificEvents.Checked;
            victoryConditions.EnableRaceSpecificVictoryConditions = chkVictoryEnableRaceSpecificConditions.Checked;
            victoryConditions.VictoryThresholdPercentage = method_203(cmbVictoryThresholdPercentage.SelectedIndex);
            victoryConditions.EnableStoryEventsShadows = chkStoryShadows.Checked;
            string customizationSetName = string.Empty;
            if (main_0.gameOptions_0 != null)
            {
                customizationSetName = main_0.gameOptions_0.CustomizationSetName;
            }
            RaceList raceList = Galaxy.LoadRaces(Application.StartupPath, customizationSetName);
            raceList = raceList.ResolvePlayableRaces();
            string string_4 = string.Empty;
            if (cmbStartNewGameYourEmpireRace.SelectedRace != null)
            {
                string_4 = cmbStartNewGameYourEmpireRace.SelectedRace.Name;
            }
            if (chkOtherEmpiresAutogenerate.Checked)
            {
                empireStartList = method_104(empireStartList, raceList, num9, string_4);
            }
            GameStartResets gameStartResets = new GameStartResets();
            if (!string.IsNullOrEmpty(lblStartNewGameTheGalaxyLoadExistingFilepath.Text) && lblStartNewGameTheGalaxyLoadExistingFilepath.Text != "(" + TextResolver.GetText("No Galaxy Map specified") + ")")
            {
                string text2 = lblStartNewGameTheGalaxyLoadExistingFilepath.Text;
                if (File.Exists(text2))
                {
                    gameStartResets.GalaxyFilepath = text2;
                    gameStartResets.ResetResources = chkStartNewGameTheGalaxyLoadExistingResources.Checked;
                    gameStartResets.ResetSceneryResearch = chkStartNewGameTheGalaxyLoadExistingSceneryResearch.Checked;
                    gameStartResets.ResetCreatures = chkStartNewGameTheGalaxyLoadExistingCreatures.Checked;
                    gameStartResets.ResetRuins = chkStartNewGameTheGalaxyLoadExistingRuins.Checked;
                    gameStartResets.ResetSpecialLocationsAndAbandonedShips = chkStartNewGameTheGalaxyLoadExistingSpecialLocations.Checked;
                }
            }
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num);
            list.Add(num2);
            list.Add(@checked);
            list.Add(num3);
            list.Add(num4);
            list.Add(num5);
            list.Add(num6);
            list.Add(num8);
            list.Add(value);
            list.Add(num9);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(victoryConditions);
            list.Add(null);
            list.Add(null);
            list.Add(false);
            list.Add(num7);
            list.Add(double_0);
            list.Add(chkStoryDistantWorlds.Checked);
            list.Add(gameStartResets);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
                return;
            }
            Cursor.Current = Cursors.Default;
            method_9();
            method_46();
            method_25();
            base.Enabled = true;
            Show();
            main_0.Visible = false;
        }

        private double method_203(int int_1)
        {
            double result = 1.0;
            switch (int_1)
            {
                case 0:
                    result = 0.75;
                    break;
                case 1:
                    result = 0.8;
                    break;
                case 2:
                    result = 0.85;
                    break;
                case 3:
                    result = 0.9;
                    break;
                case 4:
                    result = 0.95;
                    break;
                case 5:
                    result = 1.0;
                    break;
            }
            return result;
        }

        private void method_204(bool bool_5)
        {
            cmbFlagShape.Items.Clear();
            if (bool_5)
            {
                List<string> list = new List<string>();
                for (int i = 0; i < Galaxy.FlagShapesPirates.Count; i++)
                {
                    list.Add(" ");
                }
                cmbFlagShape.Items.AddRange(list.ToArray());
            }
            else
            {
                List<string> list2 = new List<string>();
                for (int j = 0; j < Galaxy.FlagShapes.Count; j++)
                {
                    list2.Add(" ");
                }
                cmbFlagShape.Items.AddRange(list2.ToArray());
            }
            if (main_0.gameOptions_0 != null && main_0.gameOptions_0.StartGameOptions != null)
            {
                if (main_0.gameOptions_0.StartGameOptions.YourEmpireFlagShape >= 0 && main_0.gameOptions_0.StartGameOptions.YourEmpireFlagShape < cmbFlagShape.Items.Count)
                {
                    cmbFlagShape.SelectedIndex = main_0.gameOptions_0.StartGameOptions.YourEmpireFlagShape;
                }
                if (main_0.gameOptions_0.StartGameOptions.YourEmpireMainColor >= 0 && main_0.gameOptions_0.StartGameOptions.YourEmpireMainColor < cmbPrimaryColor.Items.Count)
                {
                    cmbPrimaryColor.SelectedIndex = main_0.gameOptions_0.StartGameOptions.YourEmpireMainColor;
                }
                if (main_0.gameOptions_0.StartGameOptions.YourEmpireSecondaryColor >= 0 && main_0.gameOptions_0.StartGameOptions.YourEmpireSecondaryColor < cmbSecondaryColor.Items.Count)
                {
                    cmbSecondaryColor.SelectedIndex = main_0.gameOptions_0.StartGameOptions.YourEmpireSecondaryColor;
                }
            }
            else if (cmbFlagShape.Items.Count > 0)
            {
                cmbFlagShape.SelectedIndex = 0;
            }
        }

        private void btnStartNewGameTheGalaxyPrevious_Click(object sender, EventArgs e)
        {
            pnlStartNewGameTheGalaxy.Visible = false;
            pnlStartNewGameYourEmpireType.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Playstyle");
            btnStartNewGameYourEmpireTypeNormalShadows.Focus();
            pnlStartNewGameYourEmpireType.BringToFront();
            lblHelpTitle.Text = string.Empty;
            lblHelpDescription.Text = string.Empty;
        }

        private void btnStartNewGameYourEmpirePrevious_Click(object sender, EventArgs e)
        {
            pnlStartNewGameYourEmpireType.Visible = false;
            pnlStartNewGameYourRace.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Your Race");
            cmbStartNewGameYourEmpireRace.Focus();
            pnlStartNewGameYourRace.BringToFront();
        }

        private void btnStartNewGameYourEmpireNext_Click(object sender, EventArgs e)
        {
            pnlStartNewGameYourEmpire.Visible = false;
            pnlStartNewGameOtherEmpires.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Other Empires");
            method_197();
            int num = method_61(tbarStartNewGameTheGalaxyStarDensity.Value, raceList_0);
            numAutogenerateEmpiresAmount.Maximum = num - 1;
            if (ctlStartingEmpiresList.Grid.Rows.Count >= num - 1)
            {
                btnAddNewEmpire.Enabled = false;
            }
            else if (!chkOtherEmpiresAutogenerate.Checked)
            {
                btnAddNewEmpire.Enabled = true;
            }
            chkOtherEmpiresAutogenerate.Focus();
            pnlStartNewGameOtherEmpires.BringToFront();
        }

        private void btnStartNewGameVictoryConditionsPrevious_Click(object sender, EventArgs e)
        {
            pnlStartNewGameVictoryConditions.Visible = false;
            pnlStartNewGameOtherEmpires.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Other Empires");
            chkOtherEmpiresAutogenerate.Focus();
            pnlStartNewGameOtherEmpires.BringToFront();
        }

        private void method_205(string string_2)
        {
            List<string> list = new List<string>();
            lblStartNewGameGalaxyShapeTitle.Text = string.Format(TextResolver.GetText("SHAPE Galaxy"), string_2);
            string empty = string.Empty;
            if (string_2 == TextResolver.GetText("Elliptical"))
            {
                list.Add("(" + TextResolver.GetText("Random") + ")");
                list.Add(TextResolver.GetText("Deep Core"));
                list.Add(TextResolver.GetText("Outer Core"));
                list.Add(TextResolver.GetText("Inner Rim"));
                list.Add(TextResolver.GetText("Outer Rim"));
                picStartNewGameTheGalaxyPreview.Image = bitmap_4;
                empty = TextResolver.GetText("Elliptical galaxies have a classic spiral shape");
            }
            else if (string_2 == TextResolver.GetText("Spiral"))
            {
                list.Add("(" + TextResolver.GetText("Random") + ")");
                list.Add(TextResolver.GetText("Deep Core"));
                list.Add(TextResolver.GetText("Outer Core"));
                list.Add(TextResolver.GetText("Far Regions"));
                picStartNewGameTheGalaxyPreview.Image = bitmap_5;
                empty = TextResolver.GetText("Spiral galaxies have a distinctive shape");
            }
            else if (string_2 == TextResolver.GetText("Ring"))
            {
                list.Add("(" + TextResolver.GetText("Random") + ")");
                list.Add(TextResolver.GetText("Core"));
                list.Add(TextResolver.GetText("Void"));
                list.Add(TextResolver.GetText("Rim"));
                picStartNewGameTheGalaxyPreview.Image = bitmap_6;
                empty = TextResolver.GetText("Ring galaxies contain most of their stars");
            }
            else if (string_2 == TextResolver.GetText("Irregular"))
            {
                list.Add("(" + TextResolver.GetText("Random") + ")");
                list.Add(TextResolver.GetText("Center"));
                list.Add(TextResolver.GetText("Edge"));
                picStartNewGameTheGalaxyPreview.Image = bitmap_7;
                empty = TextResolver.GetText("Irregular galaxies have no fixed shape or structure");
            }
            else if (string_2 == TextResolver.GetText("Even Clusters"))
            {
                list.Add("(" + TextResolver.GetText("Random") + ")");
                list.Add(TextResolver.GetText("Center"));
                list.Add(TextResolver.GetText("Edge"));
                picStartNewGameTheGalaxyPreview.Image = bitmap_8;
                empty = TextResolver.GetText("Cluster galaxies have groups of stars clustered together");
            }
            else if (string_2 == TextResolver.GetText("Varied Clusters"))
            {
                list.Add("(" + TextResolver.GetText("Random") + ")");
                list.Add(TextResolver.GetText("Center"));
                list.Add(TextResolver.GetText("Edge"));
                picStartNewGameTheGalaxyPreview.Image = bitmap_9;
                empty = TextResolver.GetText("Cluster galaxies have groups of stars clustered together Varied");
            }
            lblStartNewGameGalaxyShapeDescription.Text = empty;
            bitmap_10 = (Bitmap)picStartNewGameTheGalaxyPreview.Image;
            cmbYourEmpireStartLocation.Items.Clear();
            cmbYourEmpireStartLocation.Items.AddRange(list.ToArray());
            cmbYourEmpireStartLocation.SelectedIndex = 0;
        }

        private void method_206(string string_2)
        {
            lblJumpStartGalaxyShapeTitle.Text = string.Format(TextResolver.GetText("SHAPE Galaxy"), string_2);
            string empty = string.Empty;
            if (string_2 == TextResolver.GetText("Elliptical"))
            {
                picJumpStartTheGalaxyPreview.Image = bitmap_4;
                empty = TextResolver.GetText("Elliptical galaxies have a classic spiral shape");
            }
            else if (string_2 == TextResolver.GetText("Spiral"))
            {
                picJumpStartTheGalaxyPreview.Image = bitmap_5;
                empty = TextResolver.GetText("Spiral galaxies have a distinctive shape");
            }
            else if (string_2 == TextResolver.GetText("Ring"))
            {
                picJumpStartTheGalaxyPreview.Image = bitmap_6;
                empty = TextResolver.GetText("Ring galaxies contain most of their stars");
            }
            else if (string_2 == TextResolver.GetText("Irregular"))
            {
                picJumpStartTheGalaxyPreview.Image = bitmap_7;
                empty = TextResolver.GetText("Irregular galaxies have no fixed shape or structure");
            }
            else if (string_2 == TextResolver.GetText("Even Clusters"))
            {
                picJumpStartTheGalaxyPreview.Image = bitmap_8;
                empty = TextResolver.GetText("Cluster galaxies have groups of stars clustered together");
            }
            else if (string_2 == TextResolver.GetText("Varied Clusters"))
            {
                picJumpStartTheGalaxyPreview.Image = bitmap_9;
                empty = TextResolver.GetText("Cluster galaxies have groups of stars clustered together Varied");
            }
            lblJumpStartGalaxyShapeDescription.Text = empty;
            bitmap_10 = (Bitmap)picJumpStartTheGalaxyPreview.Image;
        }

        private void tbarStartNewGameTheGalaxyAlienLife_LinkClicked(object sender, EventArgs e)
        {
            method_127(TextResolver.GetText("Independent planets and Traders"));
        }

        private void tbarStartNewGameTheGalaxyResearchSpeed_LinkClicked(object sender, EventArgs e)
        {
            method_127(TextResolver.GetText("Research"));
        }

        private void tbarStartNewGameTheGalaxySpaceCreatures_LinkClicked(object sender, EventArgs e)
        {
            method_127(TextResolver.GetText("Space Creatures"));
        }

        private void tbarStartNewGameTheGalaxyPirates_LinkClicked(object sender, EventArgs e)
        {
            method_127(TextResolver.GetText("Pirates"));
        }

        private void radStartNewGameGalaxyShapeElliptical_CheckedChanged(object sender, EventArgs e)
        {
            method_205(TextResolver.GetText("Elliptical"));
        }

        private void radStartNewGameGalaxyShapeSpiral_CheckedChanged(object sender, EventArgs e)
        {
            method_205(TextResolver.GetText("Spiral"));
        }

        private void radStartNewGameGalaxyShapeRing_CheckedChanged(object sender, EventArgs e)
        {
            method_205(TextResolver.GetText("Ring"));
        }

        private void radStartNewGameGalaxyShapeIrregular_CheckedChanged(object sender, EventArgs e)
        {
            method_205(TextResolver.GetText("Irregular"));
        }

        private void radStartNewGameGalaxyShapeClustersEven_CheckedChanged(object sender, EventArgs e)
        {
            method_205(TextResolver.GetText("Even Clusters"));
        }

        private void radStartNewGameGalaxyShapeClustersVaried_CheckedChanged(object sender, EventArgs e)
        {
            method_205(TextResolver.GetText("Varied Clusters"));
        }

        private void cmbStartNewGameYourEmpireRace_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empty = string.Empty;
            Race selectedRace = cmbStartNewGameYourEmpireRace.SelectedRace;
            RaceSummary summary = null;
            if (selectedRace == null)
            {
                empty = "(" + TextResolver.GetText("Random") + ")";
                _ = "(" + TextResolver.GetText("Race randomly selected") + ")";
                lnkStartNewGameYourEmpireRace.Visible = false;
            }
            else
            {
                empty = selectedRace.Name;
                summary = Galaxy.GenerateRaceSummary(selectedRace);
                lnkStartNewGameYourEmpireRace.Visible = true;
            }
            IdyEbrKpy3(selectedRace, bool_5: false);
            if (bool_2)
            {
                if (selectedRace != null)
                {
                    method_207(selectedRace.DefaultPiratePlaystyle, bool_5: false);
                }
                else
                {
                    method_207(PiratePlayStyle.Balanced, bool_5: false);
                }
            }
            lblStartNewGameYourEmpireRaceTitle.Text = TextResolver.GetText("Your Race") + ": " + empty;
            lblStartNewGameYourEmpireRaceName.Visible = true;
            lblStartNewGameYourEmpireRaceName.Font = font_9;
            lblStartNewGameYourEmpireRaceName.Text = empty;
            Bitmap bitmap = (Bitmap)picStartNewGameYourEmpireRace.Image;
            Bitmap image = main_0.method_118(null, selectedRace, picStartNewGameYourEmpireRace.Width, picStartNewGameYourEmpireRace.Height, main_0.bitmap_31, 6, bool_28: false);
            picStartNewGameYourEmpireRace.Image = image;
            if (bitmap != null && bitmap.PixelFormat != 0)
            {
                bitmap.Dispose();
            }
            pnlStartNewGameYourEmpireRaceAttributes.BindData(summary, font_3, font_7);
            pnlStartNewGameYourEmpireRaceAttributesContainer.AutoScrollPosition = new Point(0, 0);
        }

        private void method_207(PiratePlayStyle piratePlayStyle_0, bool bool_5)
        {
            int selectedIndex = -1;
            switch (piratePlayStyle_0)
            {
                case PiratePlayStyle.Balanced:
                    selectedIndex = 0;
                    break;
                case PiratePlayStyle.Pirate:
                    selectedIndex = 1;
                    break;
                case PiratePlayStyle.Mercenary:
                    selectedIndex = 2;
                    break;
                case PiratePlayStyle.Smuggler:
                    selectedIndex = 3;
                    break;
            }
            if (bool_5)
            {
                cmbJumpStartVictoryPiratePlayStyle.SelectedIndex = selectedIndex;
            }
            else
            {
                cmbVictoryPiratePlayStyle.SelectedIndex = selectedIndex;
            }
        }

        private void IdyEbrKpy3(Race race_0, bool bool_5)
        {
            List<int> list = Empire.ResolveDefaultAllowableGovernmentTypes(race_0, forceIncludeSpecialTypesIfRaceAllows: true);
            if (bool_5)
            {
                cmbJumpStartYourEmpireGovernment.Ignite(list);
            }
            else
            {
                cmbStartNewGameYourEmpireGovernment.Ignite(list);
            }
            int selectedGovernmentStyle = -1;
            for (int i = 0; i < list.Count; i++)
            {
                GovernmentAttributes governmentAttributes = Galaxy.GovernmentsStatic[list[i]];
                if (governmentAttributes != null && race_0 != null && race_0.PreferredStartingGovernmentId == governmentAttributes.GovernmentId)
                {
                    selectedGovernmentStyle = governmentAttributes.GovernmentId;
                    break;
                }
            }
            if (bool_5)
            {
                cmbJumpStartYourEmpireGovernment.SetSelectedGovernmentStyle(selectedGovernmentStyle);
            }
            else
            {
                cmbStartNewGameYourEmpireGovernment.SetSelectedGovernmentStyle(selectedGovernmentStyle);
            }
        }

        private void method_208(int int_1, bool bool_5)
        {
            string text = string.Empty;
            string text2 = string.Empty;
            if (int_1 < 0)
            {
                text = "(" + TextResolver.GetText("Random") + ")";
                text2 = "(" + TextResolver.GetText("Government randomly selected") + ")";
            }
            else
            {
                GovernmentAttributes governmentAttributes = null;
                if (int_1 >= 0 && int_1 < Galaxy.GovernmentsStatic.Count)
                {
                    governmentAttributes = Galaxy.GovernmentsStatic[int_1];
                    text = governmentAttributes.Name;
                }
                if (governmentAttributes != null)
                {
                    text2 = text2 + TextResolver.GetText("Approval") + ": " + (governmentAttributes.ApprovalRating - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal"));
                    text2 += "\n";
                    text2 = text2 + TextResolver.GetText("Corruption") + ": " + (governmentAttributes.Corruption - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal"));
                    text2 += "\n";
                    text2 = text2 + TextResolver.GetText("Growth rate") + ": " + (governmentAttributes.PopulationGrowth - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal"));
                    text2 += "\n";
                    text2 = text2 + TextResolver.GetText("Research speed") + ": " + (governmentAttributes.ResearchSpeed - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal"));
                    text2 += "\n";
                    text2 = text2 + TextResolver.GetText("Colony Income") + ": " + (governmentAttributes.TradeBonus - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal"));
                    text2 += "\n";
                    text2 = text2 + TextResolver.GetText("Maintenance costs") + ": " + (governmentAttributes.MaintenanceCosts - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal"));
                    text2 += "\n";
                    text2 = text2 + TextResolver.GetText("Troop recruitment") + ": " + (governmentAttributes.TroopRecruitment - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal"));
                    text2 += "\n";
                    text2 = text2 + TextResolver.GetText("War weariness") + ": " + (governmentAttributes.WarWeariness - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal"));
                }
            }
            if (bool_5)
            {
                lblJumpStartYourEmpireGovernmentTitle.Text = TextResolver.GetText("Your Government") + ": " + text;
                lblJumpStartYourEmpireGovernmentAttributes.Text = text2;
            }
            else
            {
                lblStartNewGameYourEmpireGovernmentTitle.Text = TextResolver.GetText("Your Government") + ": " + text;
                lblStartNewGameYourEmpireGovernmentAttributes.Text = text2;
            }
        }

        private void cmbStartNewGameYourEmpireGovernment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedGovernmentId = cmbStartNewGameYourEmpireGovernment.SelectedGovernmentId;
            method_208(selectedGovernmentId, bool_5: false);
        }

        private void cmbStartNewGameYourEmpireGovernment_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void cmbPrimaryColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFlagShape.Invalidate();
        }

        private void cmbSecondaryColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFlagShape.Invalidate();
        }

        private void cTwaUmbdtf(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Race selectedRace = cmbStartNewGameYourEmpireRace.SelectedRace;
            if (selectedRace == null)
            {
                method_127(TextResolver.GetText("Alien Races"));
            }
            else
            {
                method_127(selectedRace.Name);
            }
        }

        private void lnkStartNewGameYourEmpireGovernment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int selectedGovernmentId = cmbStartNewGameYourEmpireGovernment.SelectedGovernmentId;
            GovernmentAttributes governmentAttributes = null;
            if (selectedGovernmentId >= 0 && selectedGovernmentId < Galaxy.GovernmentsStatic.Count)
            {
                governmentAttributes = Galaxy.GovernmentsStatic[selectedGovernmentId];
            }
            if (governmentAttributes == null)
            {
                method_127(TextResolver.GetText("Government Types"));
            }
            else
            {
                method_127(governmentAttributes.Name);
            }
        }

        private void chkOtherEmpiresAutogenerate_CheckedChanged(object sender, EventArgs e)
        {
            method_197();
        }

        private void btnAddNewEmpire_Click(object sender, EventArgs e)
        {
            int num = method_61(tbarStartNewGameTheGalaxyStarDensity.Value, raceList_0);
            if (ctlStartingEmpiresList.Grid.Rows.Count < num - 1)
            {
                ctlStartingEmpiresList.Grid.Rows.Add();
            }
        }

        private int method_209(int int_1)
        {
            //int num = 0;
            if (int_1 <= 30000)
            {
                return 4;
            }
            if (int_1 <= 60000)
            {
                return 3;
            }
            if (int_1 <= 120000)
            {
                return 2;
            }
            if (int_1 <= 240000)
            {
                return 1;
            }
            return 0;
        }

        private int meEawywtba(int int_1)
        {
            //int num = 120000;
            return int_1 switch
            {
                0 => 480000,
                1 => 240000,
                2 => 120000,
                3 => 60000,
                4 => 30000,
                _ => 120000,
            };
        }

        private void tbarStartNewGameTheGalaxyResearchSpeed_ValueChanged(object sender, EventArgs e)
        {
            int value = tbarStartNewGameTheGalaxyResearchSpeed.Value;
            int num = meEawywtba(value);
            numStartNewGameTheGalaxyResearchBaseTech.Minimum = 1m;
            numStartNewGameTheGalaxyResearchBaseTech.Maximum = 999m;
            numStartNewGameTheGalaxyResearchBaseTech.Value = num / 1000;
        }

        private void tbarStartNewGameYourEmpireTechLevel_ValueChanged(object sender, EventArgs e)
        {
            if (bool_2 && tbarStartNewGameYourEmpireTechLevel.Value == 0)
            {
                tbarStartNewGameYourEmpireTechLevel.Value = 1;
            }
        }

        private void tbarStartNewGameTheGalaxyStarDensity_ValueChanged(object sender, EventArgs e)
        {
            method_211(bool_5: false);
        }

        private void tbarStartNewGameTheGalaxyDimensions_ValueChanged(object sender, EventArgs e)
        {
            method_211(bool_5: false);
            method_210();
        }

        private void method_210()
        {
            Size size = method_69(tbarStartNewGameTheGalaxyDimensions.Value);
            ctlStartingEmpiresList.SetProximityValues(size.Width, size.Height);
            foreach (EmpireStart otherEmpire in main_0.gameOptions_0.StartGameOptions.OtherEmpires)
            {
                DataGridViewRow rowByEmpireName = ctlStartingEmpiresList.GetRowByEmpireName(otherEmpire.Name);
                if (rowByEmpireName != null)
                {
                    if (((DataGridViewComboBoxCell)rowByEmpireName.Cells["Proximity"]).Items.Contains(otherEmpire.ProximityDistance))
                    {
                        rowByEmpireName.Cells["Proximity"].Value = otherEmpire.ProximityDistance;
                    }
                    else
                    {
                        rowByEmpireName.Cells["Proximity"].Value = "(" + TextResolver.GetText("Random") + ")";
                    }
                }
            }
        }

        private void method_211(bool bool_5)
        {
        }

        private void txtYourEmpireName_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Name"), TextResolver.GetText("Type the name of your empire here"));
        }

        private void cmbPrimaryColor_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Main Color"), TextResolver.GetText("The primary color for your empire"));
        }

        private void cmbSecondaryColor_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Secondary Color"), TextResolver.GetText("The secondary color for your empire"));
        }

        private void cmbStartNewGameYourEmpireRace_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Race") + ": " + TextResolver.GetText("Race"), TextResolver.GetText("The dominant race at your empire's home colony"));
        }

        private void radStartNewGameGalaxyShapeElliptical_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radStartNewGameGalaxyShapeSpiral_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radStartNewGameGalaxyShapeRing_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radStartNewGameGalaxyShapeIrregular_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radStartNewGameGalaxyShapeClustersEven_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radStartNewGameGalaxyShapeClustersVaried_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void tbarStartNewGameTheGalaxyStarDensity_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Star Amount"), TextResolver.GetText("Determines how many stars are in the galaxy"));
        }

        private void tbarStartNewGameTheGalaxyDimensions_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Physical Size"), TextResolver.GetText("Determines the physical dimensions of the galaxy"));
        }

        private void sldStartNewGameColonizationTerritoryColonyInfluenceRange_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Colony Influence Range"), TextResolver.GetText("Controls the size of influence circles that are projected out from colonies. Colony influence determines your empire territory"));
        }

        private void chkStartNewGameColonizationTerritoryEnforceColonizationRange_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Enforce Colonization Range Limits"), TextResolver.GetText("Determines whether colonization range limits are applied"));
        }

        private void grpStartNewGameColonizationTerritoryColonizationRange_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Enforce Colonization Range Limits"), TextResolver.GetText("Determines whether colonization range limits are applied"));
        }

        private void sldStartNewGameColonizationTerritoryColonizationRange_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Colonization Range"), TextResolver.GetText("Specifies the maximum allowable distance of new colonies from one of your existing colonies"));
        }

        private void tbarStartNewGameTheGalaxyColonyPrevalence_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Colony Prevalence"), TextResolver.GetText("Influences the number of colonizable planets and moons in the galaxy"));
        }

        private void tbarStartNewGameTheGalaxyAlienLife_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Alien Life"), TextResolver.GetText("Independent alien populations are distributed throughout the galaxy"));
        }

        private void tbarStartNewGameTheGalaxyExpansion_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Expansion"), TextResolver.GetText("Determines how old and developed the entire galaxy is"));
        }

        private void chkStartNewGameTheGalaxyDifficultyScaling_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Difficulty Scaling"), TextResolver.GetText("Difficulty Scaling Description"));
        }

        private void chkStartNewGameTheGalaxyPiratesRespawn_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Destroyed Pirates do not respawn"), TextResolver.GetText("Destroyed Pirates do not respawn Description"));
        }

        private void tbarStartNewGameTheGalaxyAggression_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Aggression"), TextResolver.GetText("Determines how aggressive computer players are in the game"));
        }

        private void tbarStartNewGameTheGalaxyResearchSpeed_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Research Costs"), TextResolver.GetText("Determines how fast research occurs in the galaxy"));
        }

        private void numStartNewGameTheGalaxyResearchBaseTech_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Research Costs"), TextResolver.GetText("Determines how fast research occurs in the galaxy"));
        }

        private void RmWafgkJeh(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Space Creatures"), TextResolver.GetText("Determines how many space creatures are present in the galaxy"));
        }

        private void tbarStartNewGameTheGalaxyPirates_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Pirates"), TextResolver.GetText("Determines how many pirates are present in the galaxy"));
        }

        private void tbarStartNewGameTheGalaxyPirateStrength_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Pirate Strength"), TextResolver.GetText("Determines how strong pirates are and how fast they grow"));
        }

        private void method_212(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void method_213(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void method_214(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void method_215(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void method_216(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void method_217(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void method_218(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void tbarStartNewGameYourEmpireHomeSystem_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Home System"), TextResolver.GetText("The favorability of your home system"));
        }

        private void tbarStartNewGameYourEmpireSize_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Size"), TextResolver.GetText("Determines how many colonies you have and how well established your empire is"));
        }

        private void tbarStartNewGameYourEmpireTechLevel_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Tech Level"), TextResolver.GetText("Determines how advanced your research is"));
        }

        private void tbarStartNewGameYourEmpireCorruption_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Corruption"), TextResolver.GetText("Determines the level of corruption"));
        }

        private void chkOtherEmpiresAutogenerate_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Other Empires") + ": " + TextResolver.GetText("Auto-Generate"), TextResolver.GetText("When this is turned on the specified number of empires are automatically generated"));
        }

        private void numAutogenerateEmpiresAmount_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Other Empires") + ": " + TextResolver.GetText("Amount"), TextResolver.GetText("The number of starting empires to auto-generate"));
        }

        private void ctlStartingEmpiresList_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Other Empires") + ": " + TextResolver.GetText("List"), TextResolver.GetText("Here you can manually specify all of the starting empires in the galaxy"));
        }

        private void chkGalaxyNewEmpiresDuringGame_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("New Empires appear during game"), TextResolver.GetText("If this is turned on then independent alien populations can eventually become empires"));
        }

        private void lnkThemes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            if (!pnlThemes.Visible)
            {
                method_26();
            }
        }

        private void lnkThemes_MouseEnter(object sender, EventArgs e)
        {
            method_146(TextResolver.GetText("Change Theme DESCRIPTION"));
        }

        private void lnkThemes_MouseLeave(object sender, EventArgs e)
        {
            method_147();
        }

        private void btnThemeCancel_Click(object sender, EventArgs e)
        {
            method_27();
        }

        private void btnThemeSwitch_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            Cursor.Current = Cursors.WaitCursor;
            method_2(lblThemeTitle.Text, bool_5: true, bool_6: true);
            Application.UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
            method_27();
        }

        private void pnlThemes_CloseButtonClicked(object sender, EventArgs e)
        {
            method_27();
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

        public void SetControlLocalizedLabels()
        {
            lnkAbout.Text = TextResolver.GetText("Credits");
            lnkCheckForUpdates.Text = TextResolver.GetText("Check For Updates");
            lnkExit.Text = TextResolver.GetText("Exit");
            lnkGalactopedia.Text = TextResolver.GetText("Galactopedia");
            lnkLoadGame.Text = TextResolver.GetText("Load Game");
            lnkNewGame.Text = TextResolver.GetText("Start New Game");
            lnkOptions.Text = TextResolver.GetText("Options");
            lnkPlayScenario.Text = TextResolver.GetText("Quick Start");
            lnkQuickStartRaceHelp.Text = TextResolver.GetText("About this Race") + "...";
            lnkStartNewGameYourEmpireGovernment.Text = TextResolver.GetText("Read more about this Government type") + "...";
            lnkStartNewGameYourEmpireRace.Text = TextResolver.GetText("Read more about this race") + "...";
            lnkThemes.Text = TextResolver.GetText("Change Theme");
            lnkTutorial.Text = TextResolver.GetText("Tutorials");
            radioRandom.Text = TextResolver.GetText("Random");
            radioSmall.Text = TextResolver.GetText("QuickStart Title Fast");
            radioEpic.Text = TextResolver.GetText("QuickStart Title Epic");
            radioRingRace.Text = TextResolver.GetText("QuickStart Title Ring Race");
            radioConflict.Text = TextResolver.GetText("QuickStart Title Conflict");
            radioExpandingSettlements.Text = TextResolver.GetText("QuickStart Title Expanding Settlements");
            radioExpandingFromTheCore.Text = TextResolver.GetText("QuickStart Title Expanding From The Core");
            radioFullyDevelopedSmall.Text = TextResolver.GetText("QuickStart Title Fully Developed - Small");
            radioFullyDevelopedStandard.Text = TextResolver.GetText("QuickStart Title Fully Developed - Standard");
            radioFullyDevelopedLarge.Text = TextResolver.GetText("QuickStart Title Fully Developed - Large");
            radioGalacticRepublicSupremeRuler.Text = TextResolver.GetText("QuickStart Title Galactic Republic - Supreme Ruler");
            radioGalacticRepublicWildFrontiers.Text = TextResolver.GetText("QuickStart Title Galactic Republic - Wild Frontiers");
            radioSovereignTerritoriesRegionalRuler.Text = TextResolver.GetText("QuickStart Title Sovereign Territories - Regional Ruler");
            radioSovereignTerritoriesMinorFaction.Text = TextResolver.GetText("QuickStart Title Sovereign Territories - Minor Faction");
            radStartNewGameGalaxyShapeElliptical.Text = TextResolver.GetText("Elliptical");
            radStartNewGameGalaxyShapeIrregular.Text = TextResolver.GetText("Irregular");
            radStartNewGameGalaxyShapeRing.Text = TextResolver.GetText("Ring");
            radStartNewGameGalaxyShapeSpiral.Text = TextResolver.GetText("Spiral");
            radStartNewGameGalaxyShapeClustersEven.Text = TextResolver.GetText("Even Clusters");
            radStartNewGameGalaxyShapeClustersVaried.Text = TextResolver.GetText("Varied Clusters");
            chkOptionsControlResearch.Text = TextResolver.GetText("Research");
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
            cmbStartNewGameTheGalaxyPirateProximity.Items.AddRange(new string[3]
            {
            TextResolver.GetText("Nearby"),
            TextResolver.GetText("Average"),
            TextResolver.GetText("Distant")
            });
            lblStartNewGameTheGalaxyPirateProximityLabel.Text = TextResolver.GetText("Pirate Proximity");
            chkOptionsLoadedGamesPaused.Text = TextResolver.GetText("Loaded games are paused");
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
            chkOptionsSuppressAllPopups.Text = TextResolver.GetText("Suppress all pop-up screens");
            chkStoryReturnOfTheShakturi.Text = TextResolver.GetText("Enable Return Of The Shakturi story events and victory conditions");
            chkQuickStartReturnOfTheShakturiStoryEvents.Text = TextResolver.GetText("Return Of The Shakturi storyline");
            chkStoryShadows.Text = TextResolver.GetText("Enable Shadows story events");
            chkStoryDistantWorlds.Text = TextResolver.GetText("Enable original Distant Worlds story events");
            chkQuickStartDistantWorldsStoryEvents.Text = TextResolver.GetText("Distant Worlds original storyline");
            cmbVictoryPiratePlayStyle.Items.AddRange(new string[4]
            {
            Galaxy.ResolveDescription(PiratePlayStyle.Balanced),
            Galaxy.ResolveDescription(PiratePlayStyle.Pirate),
            Galaxy.ResolveDescription(PiratePlayStyle.Mercenary),
            Galaxy.ResolveDescription(PiratePlayStyle.Smuggler)
            });
            cmbJumpStartVictoryPiratePlayStyle.Items.AddRange(new string[4]
            {
            Galaxy.ResolveDescription(PiratePlayStyle.Balanced),
            Galaxy.ResolveDescription(PiratePlayStyle.Pirate),
            Galaxy.ResolveDescription(PiratePlayStyle.Mercenary),
            Galaxy.ResolveDescription(PiratePlayStyle.Smuggler)
            });
            chkStartNewGameEnableTechTrading.Text = TextResolver.GetText("Allow Tech Trading");
            chkStartNewGameEnableGiantKaltors.Text = TextResolver.GetText("Allow Giant Kaltors at game start");
            sldGameOptionsAttackOvermatch.LabelText = TextResolver.GetText("Attack Overmatch");
            tbarStartNewGameYourEmpireCorruption.LabelText = TextResolver.GetText("Corruption");
            tbarStartNewGameYourEmpireHomeSystem.LabelText = TextResolver.GetText("Home System");
            tbarStartNewGameYourEmpireSize.LabelText = TextResolver.GetText("Size");
            tbarStartNewGameYourEmpireTechLevel.LabelText = TextResolver.GetText("Tech Level");
            tbrGameOptionsAdvancedDisplaySettingsNebulaeDetail.LabelText = TextResolver.GetText("System Nebulae Detail");
            method_219(cmbGameOptionsEngagementStanceAttack);
            method_219(cmbGameOptionsEngagementStanceEscort);
            method_219(cmbGameOptionsEngagementStanceOther);
            method_219(cmbGameOptionsEngagementStancePatrol);
            method_219(cmbGameOptionsEngagementStanceAttackManual);
            method_219(cmbGameOptionsEngagementStanceEscortManual);
            method_219(cmbGameOptionsEngagementStanceOtherManual);
            method_219(cmbGameOptionsEngagementStancePatrolManual);
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
            method_220(cmbOptionsControlAgentMissions, "Suggest offensive missions");
            method_220(cmbOptionsControlAttacks, "Suggest attack targets");
            method_220(cmbOptionsControlColonization, "Suggest new colonies");
            method_220(cmbOptionsControlConstruction, "Suggest new ships and bases");
            method_220(cmbOptionsControlDiplomacyGifts, "Suggest gifts to empires");
            method_220(cmbOptionsControlDiplomacyOffense, "Suggest war and trade sanctions");
            method_220(cmbOptionsControlDiplomacyTreaties, "Suggest new treaties");
            cmbOptionsMouseScrollWheelBehaviour.Items.Clear();
            cmbOptionsMouseScrollWheelBehaviour.Items.AddRange(new string[3]
            {
            TextResolver.GetText("No movement"),
            TextResolver.GetText("Move to selected item"),
            TextResolver.GetText("Move to mouse cursor location")
            });
            lblCopyright.Text = TextResolver.GetText("Copyright");
            lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Text = TextResolver.GetText("fps");
            lblGameOptionsEngagementStanceAttack.Text = TextResolver.GetText("Attack/Bombard");
            lblGameOptionsEngagementStanceEscort.Text = TextResolver.GetText("Mission Escort");
            lblGameOptionsEngagementStanceOther.Text = TextResolver.GetText("Other");
            lblGameOptionsEngagementStancePatrol.Text = TextResolver.GetText("Mission Patrol");
            lblGameOptionsFleetAttackGather.Text = TextResolver.GetText("First assemble when this percentage of fleet dispersed");
            lblGameOptionsFleetAttackRefuel.Text = TextResolver.GetText("First assemble when this percentage of fleet need fuel");
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
            lblQuickStartRace.Text = TextResolver.GetText("Race");
            lblStartNewGameOtherEmpiresAutoGenNumberDescrip1.Text = TextResolver.GetText("Generate");
            lblStartNewGameOtherEmpiresAutoGenNumberDescrip2.Text = TextResolver.GetText("starting empires");
            lblStartNewGameOtherEmpiresOR.Text = TextResolver.GetText("OR specify the starting empires below");
            lblStartNewGameYourEmpireGalaxyLocation.Text = TextResolver.GetText("Galaxy Starting Location");
            lblStartNewGameYourEmpireGovernmentTitle.Text = TextResolver.GetText("Your Government");
            lblStartNewGameYourEmpireMainColor.Text = TextResolver.GetText("Main Color");
            lblStartNewGameYourEmpireName.Text = TextResolver.GetText("Empire Name");
            lblStartNewGameYourEmpireSecondaryColor.Text = TextResolver.GetText("Secondary Color");
            lblVersion.Text = TextResolver.GetText("Version");
            lblVictorySandbox.Text = TextResolver.GetText("Victory Conditions Explanation");
            btnAboutClose.Text = TextResolver.GetText("Close Credits");
            btnAddNewEmpire.Text = TextResolver.GetText("Add New Empire");
            btnGameOptionsAdvancedDisplaySettings.Text = TextResolver.GetText("Advanced Settings...");
            btnGameOptionsEmpireSettings.Text = TextResolver.GetText("Empire Settings");
            btnGameOptionsResetAutomationMessages.Text = TextResolver.GetText("Reset Warnings");
            btnQuickStart.Text = TextResolver.GetText("Start Game");
            btnQuickStartCancel.Text = TextResolver.GetText("Cancel");
            btnStartNewGameOtherEmpiresNext.Text = TextResolver.GetText("Next: Victory Conditions") + " >>";
            btnStartNewGameOtherEmpiresPrevious.Text = "<< " + TextResolver.GetText("Previous: Your Empire");
            btnStartNewGameStart.Text = TextResolver.GetText("Start the Game!");
            btnStartNewGameTheGalaxyNext.Text = TextResolver.GetText("Next: Colonization and Territory") + " >>";
            btnStartNewGameTheGalaxyPrevious.Text = "<< " + TextResolver.GetText("Previous: Playstyle");
            btnStartNewGameVictoryConditionsPrevious.Text = "<< " + TextResolver.GetText("Previous: Other Empires");
            btnStartNewGameYourEmpireNext.Text = TextResolver.GetText("Next: Other Empires") + " >>";
            btnStartNewGameYourEmpirePrevious.Text = "<< " + TextResolver.GetText("Previous: Your Race");
            btnStartNewGameYourRaceNext.Text = TextResolver.GetText("Next: Your Empire") + " >>";
            btnStartNewGameYourRacePrevious.Text = "<< " + TextResolver.GetText("Previous: Colonization and Territory");
            btnThemeCancel.Text = TextResolver.GetText("Cancel");
            btnThemeSwitch.Text = TextResolver.GetText("Switch Theme");
            btnTutorialStartCancel.Text = TextResolver.GetText("Cancel");
            chkEncyclopediaShowAtStart.Text = TextResolver.GetText("Show this screen at startup");
            chkGalaxyNewEmpiresDuringGame.Text = TextResolver.GetText("Allow new Empires to appear during the game");
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
            chkOptionsPopupMessageEmpireMetDestroyed.Text = TextResolver.GetText("Empire Discovery");
            chkOptionsPopupMessageExploration.Text = TextResolver.GetText("Exploration discoveries");
            chkOptionsPopupMessageIntelligenceMissions.Text = TextResolver.GetText("Intelligence Missions");
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
            chkOptionsScrollingMessageUnderAttackCivilianShips.Text = TextResolver.GetText("Under Attack");
            chkOptionsScrollingMessageWarTradeSanctions.Text = TextResolver.GetText("War and Trade Sanctions");
            chkOptionsScrollingMessageConstructionResourceShortage.Text = TextResolver.GetText("Construction Resource Shortage");
            chkOptionsShowSystemNebulae.Text = TextResolver.GetText("Display nebulae clouds in systems");
            chkOtherEmpiresAutogenerate.Text = TextResolver.GetText("Auto-Generate Starting Empires");
            chkOptionsPopupMessageUnderAttackCivilianBases.Text = TextResolver.GetText("Under Attack - Civilian Bases");
            chkOptionsPopupMessageUnderAttackCivilianShips.Text = TextResolver.GetText("Under Attack - Civilian Ships");
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.Text = TextResolver.GetText("Under Attack - Colonies && Spaceports");
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.Text = TextResolver.GetText("Under Attack - Colony && Construction Ships");
            chkOptionsPopupMessageUnderAttackExplorationShips.Text = TextResolver.GetText("Under Attack - Exploration Ships");
            chkOptionsPopupMessageUnderAttackMilitaryShips.Text = TextResolver.GetText("Under Attack - Military Ships");
            chkOptionsPopupMessageUnderAttackOtherStateBases.Text = TextResolver.GetText("Under Attack - Research, Monitoring, Resorts");
            chkOptionsScrollingMessageUnderAttackCivilianBases.Text = TextResolver.GetText("Under Attack - Civilian Bases");
            chkOptionsScrollingMessageUnderAttackCivilianShips.Text = TextResolver.GetText("Under Attack - Civilian Ships");
            chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Text = TextResolver.GetText("Under Attack - Colonies && Spaceports");
            chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Text = TextResolver.GetText("Under Attack - Colony && Construction Ships");
            chkOptionsScrollingMessageUnderAttackExplorationShips.Text = TextResolver.GetText("Under Attack - Exploration Ships");
            chkOptionsScrollingMessageUnderAttackMilitaryShips.Text = TextResolver.GetText("Under Attack - Military Ships");
            chkOptionsScrollingMessageUnderAttackOtherStateBases.Text = TextResolver.GetText("Under Attack - Research, Monitoring, Resorts");
            pnlGameOptionsMessages.HeaderTitle = TextResolver.GetText("Message Settings");
            btnGameOptionsShowMessages.Text = TextResolver.GetText("Show Message Settings");
            pnlGameOptions.HeaderTitle = TextResolver.GetText("Options");
            pnlGameOptionsAdvancedDisplaySettings.HeaderTitle = TextResolver.GetText("Advanced Display Settings");
            pnlGameOptionsEmpireSettings.HeaderTitle = TextResolver.GetText("Other Empire Settings");
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game");
            pnlQuickStart.HeaderTitle = TextResolver.GetText("Quick Start");
            pnlThemes.HeaderTitle = TextResolver.GetText("Change Theme");
            FtIzCrmve5.HeaderTitle = TextResolver.GetText("Tutorials");
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

        private void method_219(ComboBox comboBox_0)
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

        private void method_220(ComboBox comboBox_0, string string_2)
        {
            comboBox_0.Items.Clear();
            comboBox_0.Items.Add(TextResolver.GetText("Control manually"));
            comboBox_0.Items.Add(TextResolver.GetText(string_2));
            comboBox_0.Items.Add(TextResolver.GetText("Fully automate"));
        }

        private void pnlStartNewGameYourEmpireRaceAttributes_Enter(object sender, EventArgs e)
        {
            pnlStartNewGameYourEmpireRaceAttributesContainer.Focus();
        }

        private void pnlStartNewGameYourEmpireRaceAttributes_MouseEnter(object sender, EventArgs e)
        {
            pnlStartNewGameYourEmpireRaceAttributesContainer.Focus();
        }

        private void sldStartNewGameColonizationTerritoryColonyInfluenceRange_Scroll(object sender, ScrollEventArgs e)
        {
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeValue.Text = sldStartNewGameColonizationTerritoryColonyInfluenceRange.Value + "%";
        }

        private void chkStartNewGameColonizationTerritoryEnforceColonizationRange_CheckedChanged(object sender, EventArgs e)
        {
            grpStartNewGameColonizationTerritoryColonizationRange.Enabled = chkStartNewGameColonizationTerritoryEnforceColonizationRange.Checked;
        }

        private void sldStartNewGameColonizationTerritoryColonizationRange_Scroll(object sender, ScrollEventArgs e)
        {
            lblStartNewGameColonizationTerritoryColonizationRangeValue.Text = ((float)sldStartNewGameColonizationTerritoryColonizationRange.Value / 1000f).ToString("#0.00") + "  " + TextResolver.GetText("sectors");
        }

        private void chkVictoryEnableRaceSpecificConditions_CheckedChanged(object sender, EventArgs e)
        {
            cmbVictoryPiratePlayStyle.Enabled = chkVictoryEnableRaceSpecificConditions.Checked;
        }

        private void chkOptionsSuppressAllPopups_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOptionsSuppressAllPopups.Checked)
            {
                if (cmbGameOptionsEncounterRuins.SelectedIndex < 1)
                {
                    cmbGameOptionsEncounterRuins.SelectedIndex = Math.Max(1, main_0.gameOptions_0.DiscoveryActionRuin);
                }
                if (cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex < 1)
                {
                    cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex = Math.Max(1, main_0.gameOptions_0.DiscoveryActionAbandonedShipBase);
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
                    cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex = Math.Max(1, main_0.gameOptions_0.DiscoveryActionAbandonedShipBase);
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
                    cmbGameOptionsEncounterRuins.SelectedIndex = Math.Max(1, main_0.gameOptions_0.DiscoveryActionRuin);
                }
                cmbGameOptionsEncounterRuins.SelectedIndexChanged += cmbGameOptionsEncounterRuins_SelectedIndexChanged;
            }
        }
    }
}
